using League_Autoplay.AutoQueue;
using League_Autoplay.High_Performance_Timer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace League_Autoplay
{
    class BasicAI
    {
        String[] messages = { "We got dis", "gg", "imma honor you guys after", "oh no", "dont surrender plz",
        "is infinity edge a good first item", "sorry", "just a sec my dog just pooped on the floor and it stinks",
        "anyone else take a dump during the loading screen?", "good luck you need it", "#halp", "whattheheck",
        "honor me plz!!1", "don't die, k?", "lick my dorans blade", "thanks obama", "that team comp tho", "what do", "hi",
        "hewwo", "I'm a cat meow", "how are you guys?", "tell me a story", "FOR DEMACIA", "FOR NOXUS", "I'm new at this game",
        "pie hehe", "imma build guardian angel first, k?", "my mom tells me im handsome", "k thx", "you rock", "be careful!",
        "!!!!!", "dgashdgfasbdgasjdfsbkljdsakj", "I need a new keyboard", "Good game!", "Good luck", "be nice to me",
        "so kawaii", "im trying", ":(", "hi", "gg", "What's up?", "Hi peoples", "Hello peoples", "What's up peoples?", "Where my people at",
        "Can I get a hello?", "HOW IS YOU DOIN", "I rock", "Harambe died for us", "IPHONES are the best phones ever", "Gonna pwn so hard", "let me tell you a story",
        "this is going to go good", "What does the fox say?", "Che che che che che", "Bwahahahahah", "Whaddup my miggars?", "Teemo is death", "Teemo is die",
        "I need a girlfriend", "I need a gf", "You guys are too loud", "Thanks", "Thanks for that", "Geesh", "Oh my", "Let it gooooooo", "C:", ":C", ";)", "C;",
        "I see you", "You can't see me", "why do I even try", "You guys are great", "I like the skin", "omg"};

        enum Action
        {
            RunAway, AttackEnemyChampion, AttackEnemyMinion, FollowAllyChampion, FollowAllyMinion, MoveToMid,
            Recall, AttackTower, GoHam, StandStill
        };

        enum FollowState
        {
            FollowAnything, FollowOnlyMinions, FollowOnlyChampions, FollowNothing
        };

        Action lastDecision;
        int moveToLane;

        DetectionDataStruct detectionData;
        bool newData = true;
        bool firstGameMessage;
        bool firstSawSelf;

        Stopwatch lastLevelUpStopwatch, lastShopBuyingStopwatch, lastCameraFocusStopwatch, lastPlacedWardStopwatch,
            lastRunAwayClickStopwatch, lastClickEnemyChampStopwatch, lastMovementClickStopwatch, lastClickAllyMinionStopwatch,
            lastClickEnemyMinionStopwatch, lastClickEnemyTowerStopwatch, lastClickAllyChampionStopwatch, lastMoveMouseStopwatch,
            lastRecallTapStopwatch, lastSpell1UseStopwatch, lastSpell2UseStopwatch, lastSpell3UseStopwatch, lastSpell4UseStopwatch,
            lastSummonerSpell1UseStopwatch, lastSummonerSpell2UseStopwatch, lastItem1UseStopwatch, lastItem2UseStopwatch,
            lastItem3UseStopwatch, lastItem4UseStopwatch, lastItem5UseStopwatch, lastItem6UseStopwatch, activeAutoUseTimeStopwatch,
            moveToLanePathSwitchStopwatch, gameCurrentTimeStopwatch, lastSurrenderStopwatch, lastShopBuyStopwatch,
            lastShopOpenTapStopwatch, lastShopCloseTapStopwatch, lastTimeSawEnemyChampStopwatch, standStillTimeStopwatch,
            healthGainedTimeStopwatch, attemptSurrenderStopwatch, continueClickStopwatch, afkClickStopwatch, stoppedWorkingStopwatch,
            cantSeeSelfMoveMouseStopwatch, fuzzyLaneMovementStopwatch, runAwayPanicStopwatch;

        double fuzzyLaneMovementX = 0.0, fuzzyLaneMovementY = 0.0;

        Stopwatch printGameTime;

        bool boughtStarterItems;
        bool inProcessOfBuyingItems;
        List<GenericObject> boughtItems;

        PositionDouble baseLocation;

        double healthGainedPerSecond;
        double lastHealthAmount;
        Stopwatch lastHealthtimePassedStopwatch;

        Random random;

        Position lastOnMapLocation;

        FollowState currentFollowState;
        Stopwatch followStopwatch;

        Stopwatch lastTypeMessageStopwatch, typingMessageStopwatch;

        Stopwatch notMovingTimer;

        List<GameAction> gameActions;

        public BasicAI()
        {

            resetAI();
            random = new Random(Environment.TickCount);
        }


        public void updateDetectionData(ref DetectionDataStruct data)
        {
            detectionData = data;
            newData = true;
        }

        public unsafe void processAI()
        {

            if (newData)
            {
                Console.Write("{P:" + gameActions.Count + "}");
            }
            else
            {
                Console.Write("|" + gameActions.Count);
            }
            Console.Out.Flush();

            //lastTypeMessageStopwatch, typingMessageStopwatch
            if (detectionData.selfHealthBarVisible)
            {
                if (firstSawSelf == false)
                {
                    firstSawSelf = true;
                    gameCurrentTimeStopwatch.Reset();
                }
                bool typeMessage = false;
                if (firstGameMessage == false)
                {
                    //Say first game message
                    firstGameMessage = true;
                    typeMessage = true;
                }
                if (lastTypeMessageStopwatch.DurationInMinutes() >= 15 && random.Next(60 * 60 * 5) == 0)
                {
                    typeMessage = true;
                }
                if (typeMessage)
                {
                    typingMessageStopwatch.Reset();
                    String chosenMessage = messages[random.Next(messages.Length)];
                    typeMessageInChat(chosenMessage);
                }
            }

            //Attempt surrender
            if (detectionData.selfHealthBarVisible && typingMessageStopwatch.DurationInSeconds() > 2.0 &&
                gameCurrentTimeStopwatch.DurationInMinutes() > 23 && attemptSurrenderStopwatch.DurationInMinutes() >= 4)
            {
                attemptSurrenderStopwatch.Reset();
                typeMessageInChat("/ff", false);
            }

            if (typingMessageStopwatch.DurationInSeconds() <= 4.0)
            { // Don't do anything when we are typing

                processActions();
                return;
            }

            handleAbilityLevelUps();
            handleBuyingItems();
            handleCameraFocus();
            handlePlacingWard();
            handleMovementAndAttacking();

            //Don't surrender in the first 10 minutes. Don't want remake
            if (lastSurrenderStopwatch.DurationInMilliseconds() >= 1000 && detectionData.surrenderAvailable && gameCurrentTimeStopwatch.DurationInMinutes() >= 10)
            {
                lastSurrenderStopwatch.Reset();
                GenericObject* surrender = (GenericObject*)detectionData.surrenderActive.ToPointer();

                replaceActionWithAnyTag(new GameAction(delegate (GameAction action)
                {
                    MotorCortex.clickMouseAt(surrender->center.x, surrender->center.y, 5);
                    didAction();

                    Task.Delay(50).ContinueWith(_ =>
                    {
                        action.finished();
                    });
                }, "Surrender mouse_move"));
            }

            //Handle continue button
            if (!detectionData.selfHealthBarVisible && detectionData.continueAvailable && continueClickStopwatch.DurationInMilliseconds() >= 1000)
            {
                continueClickStopwatch.Reset();
                GenericObject* continueObject = (GenericObject*)detectionData.continueActive.ToPointer();

                replaceActionWithAnyTag(new GameAction(delegate (GameAction action)
                {

                    MotorCortex.clickMouseAt(continueObject->center.x, continueObject->center.y, 5);
                    didAction();
                    MotorCortex.releaseControlKey();
                    MotorCortex.releaseShiftKey();
                    MotorCortex.pressTabKey();

                    Task.Delay(50).ContinueWith(_ =>
                    {
                        action.finished();
                    });
                }, "Continue mouse_move"));
            }

            //Handle afk and stopped working button
            if (detectionData.afkAvailable && afkClickStopwatch.DurationInMilliseconds() >= 1000)
            {
                afkClickStopwatch.Reset();
                GenericObject* afkObject = (GenericObject*)detectionData.afkActive.ToPointer();

                replaceActionWithAnyTag(new GameAction(delegate (GameAction action)
                {

                    MotorCortex.clickMouseAt(afkObject->center.x, afkObject->center.y, 5);

                    Task.Delay(50).ContinueWith(_ =>
                    {
                        action.finished();
                    });
                }, "afk_button mouse_move"));

            }
            if (detectionData.stoppedWorkingAvailable && stoppedWorkingStopwatch.DurationInMilliseconds() >= 1000)
            {
                stoppedWorkingStopwatch.Reset();
                GenericObject* stoppedWorkingObject = (GenericObject*)detectionData.stoppedWorkingActive.ToPointer();

                replaceActionWithAnyTag(new GameAction(delegate (GameAction action)
                {

                    MotorCortex.clickMouseAt(stoppedWorkingObject->center.x + 20, stoppedWorkingObject->center.y, 5);

                    Task.Delay(50).ContinueWith(_ =>
                    {
                        action.finished();
                    });
                }, "stopped_working mouse_move"));
            }

            //If can't see self champion or self healthbar move mouse to top left
            if (detectionData.selfHealthBarVisible == false || detectionData.numberOfSelfChampions == 0)
            {
                if (cantSeeSelfMoveMouseStopwatch.DurationInSeconds() >= 5)
                {
                    cantSeeSelfMoveMouseStopwatch.Reset();

                    addAction(new GameAction(delegate (GameAction action)
                    {

                        MotorCortex.moveMouseTo(0, 0, 5);
                        Task.Delay(500).ContinueWith(_ =>
                        {
                            action.finished();
                        });
                    }, "can't_see_self mouse_move"));

                }
            }

            if (printGameTime.DurationInMinutes() >= 1.0)
            {
                printGameTime.Reset();
                Console.WriteLine(string.Format("{0:HH:mm:ss tt}", DateTime.Now) + " Game Time: " + gameCurrentTimeStopwatch.DurationInMinutes());
            }

            //Click center of screen if afk
            if (notMovingTimer.DurationInMinutes() >= 1.0)
            {
                notMovingTimer.Reset();

                addAction(new GameAction(delegate (GameAction action)
                {

                    MotorCortex.clickMouseRightAt(1024 / 2, 768 / 2, 5);

                    Task.Delay(50).ContinueWith(_ =>
                    {
                        action.finished();
                    });
                }, "not_moving mouse_move"));
            }

            newData = false;


            processActions();
        }

        void processActions()
        {
            //Process actions
            if (gameActions.Count > 0)
            {
                GameAction action = gameActions[0];
                if (action.isFinished() == false && action.isRunning() == false)
                {
                    action.runAction();
                    //Console.WriteLine("Running Action: " + action.getTags());
                } else if (action.isFinished())
                {
                    gameActions.RemoveAt(0);
                    processActions();
                    //Console.WriteLine("Removing Action" + action.getTags());
                }
            }
        }
        void clearActionsWithAnyTag(String tags)
        {
            String[] targetTagArray = tags.Split(' ');

            for (int i = 0; i < gameActions.Count; i++)
            {
                GameAction action2 = gameActions[i];
                if (action2.isRunning() == false)
                {
                    String[] actionTagArray = action2.getTags().Split(' ');
                    bool remove = false;

                    foreach (String tag1 in targetTagArray)
                    {
                        foreach (String tag2 in actionTagArray)
                        {
                            if (tag1.Equals(tag2))
                            { 
                                remove = true;
                            }
                        }
                    }
                    if (remove) {
                        gameActions.Remove(action2);
                        i--;
                    }
                }
            }
        }
        void addAction(GameAction action)
        {
            gameActions.Add(action);
        }
        void replaceActionWithAnyTag(GameAction action)
        {
            String[] targetTagArray = action.getTags().Split(' ');

            int index = -1;
            for (int i = 0; i < gameActions.Count; i++)
            {
                GameAction action2 = gameActions[i];
                

                if (action2.isRunning() == false && action2.isFinished() == false)
                {

                    String[] actionTagArray = action2.getTags().Split(' ');
                    bool containsTag = false;
                    foreach (String tag1 in targetTagArray)
                    {
                        foreach (String tag2 in actionTagArray)
                        {
                            if (tag1.Equals(tag2))
                            {
                                containsTag = true;
                            }
                        }
                    }
                    if (containsTag) index = i;
                }
            }
            //Remove all actions with same ID
            clearActionsWithAnyTag(action.getTags());
            /*
            for (int i = 0; i < gameActions.Count; i++)
            {
                GameAction action2 = gameActions[i];
                if (action2.getID().Equals(action.getID()) && action2.isRunning() == false && action2.isFinished() == false)
                {
                    gameActions.Remove(action2);
                    i--;
                }
            }*/
            //Insert action
            if (index == -1)
            {
                gameActions.Add(action);
            } else
            {
                gameActions.Insert(index, action);
            }
        }


        public void typeMessageInChat(String message, bool addRandomStuff = true)
        {
            addAction(new GameAction(delegate (GameAction action) {
                didAction();
                if (addRandomStuff)
                {
                    int r = random.Next(3);
                    if (r == 0)
                    { //Insert random letter

                        //int num = random.Next(0, 26); // Zero to 25
                        //char let = (char)('a' + num);
                        //message = message.Insert(random.Next(message.Length), let + "");

                        //Capitalize a random letter
                        int num = random.Next(0, message.Length);

                        StringBuilder sb = new StringBuilder(message);
                        sb[num] = Char.ToUpper(sb[num]);
                        message = sb.ToString();
                    }
                    else if (r == 1)
                    { //Randomly remove a letter
                      //Remove one random letter
                      //message = message.Remove(random.Next(message.Length - 1), 5);
                      //Say in all chat
                        message = "/all " + message;
                    }
                    r = random.Next(20);
                    if (r == 0)
                    {
                        message = message.ToLower();
                    }
                    else if (r == 1)
                    {
                        message = message.ToUpper();
                    }
                    else if (r == 2 && message.Length > 8)
                    {
                        //Add a letter to the end

                        int num = random.Next(0, 26); // Zero to 25
                        char let = (char)('a' + num);
                        message = message + let;
                    }
                }

                Console.WriteLine("Typing message: " + message);

                //Click center of screen
                MotorCortex.moveMouseTo(1024 / 2, 768 / 2, 5);
                typingMessageStopwatch.Reset();

                Task.Delay(200).ContinueWith(_ =>
                {
                    MotorCortex.clickMouseAt(1024 / 2, 768 / 2);
                    typingMessageStopwatch.Reset();

                    Task.Delay(200).ContinueWith(_2 =>
                    {
                        MotorCortex.tapEnterKey();
                        typingMessageStopwatch.Reset();

                        typeNextLetter(message);

                    });

                });


                Task.Delay(200 + 200 + message.Length * 100 + 200).ContinueWith(_ =>
                {
                    action.finished();
                });
                
            }, "text"));

        }

        void typeNextLetter(String message)
        {
            //Console.WriteLine("Typing message with letters left: " + message);
            if (message.Length == 0)
            {
                Task.Delay(100).ContinueWith(_ =>
                {
                    MotorCortex.tapEnterKey();
                    typingMessageStopwatch.Reset();
                    didAction();
                });
                return;
            }
            String letter = message.Substring(0, 1);
            String newMessage = message.Substring(1);

            Task.Delay(200).ContinueWith(_ =>
            {
                MotorCortex.typeText(letter);
                typeNextLetter(newMessage);
                typingMessageStopwatch.Reset();
            });
        }

        public void resetAI(int lane = 1)
        {
            firstSawSelf = false;
            lastLevelUpStopwatch = new Stopwatch();
            lastShopBuyStopwatch = new Stopwatch();
            lastShopOpenTapStopwatch = new Stopwatch();
            lastShopCloseTapStopwatch = new Stopwatch();
            lastShopBuyingStopwatch = new Stopwatch();
            lastCameraFocusStopwatch = new Stopwatch();
            lastPlacedWardStopwatch = new Stopwatch();
            lastRunAwayClickStopwatch = new Stopwatch();
            lastClickEnemyChampStopwatch = new Stopwatch();
            lastMovementClickStopwatch = new Stopwatch();
            lastClickAllyMinionStopwatch = new Stopwatch();

            lastClickEnemyMinionStopwatch = new Stopwatch();
            lastClickEnemyTowerStopwatch = new Stopwatch();
            lastClickAllyChampionStopwatch = new Stopwatch();
            lastMoveMouseStopwatch = new Stopwatch();
            lastRecallTapStopwatch = new Stopwatch();
            lastSpell1UseStopwatch = new Stopwatch();
            lastSpell2UseStopwatch = new Stopwatch();
            lastSpell3UseStopwatch = new Stopwatch();
            lastSpell4UseStopwatch = new Stopwatch();
            lastSummonerSpell1UseStopwatch = new Stopwatch();
            lastSummonerSpell2UseStopwatch = new Stopwatch();
            lastItem1UseStopwatch = new Stopwatch();
            lastItem2UseStopwatch = new Stopwatch();
            lastItem3UseStopwatch = new Stopwatch();
            lastItem4UseStopwatch = new Stopwatch();
            lastItem5UseStopwatch = new Stopwatch();
            lastItem6UseStopwatch = new Stopwatch();
            standStillTimeStopwatch = new Stopwatch();
            runAwayPanicStopwatch = new Stopwatch();

            activeAutoUseTimeStopwatch = new Stopwatch();
            lastTimeSawEnemyChampStopwatch = new Stopwatch();
            fuzzyLaneMovementStopwatch = new Stopwatch();


            //moveToLane = arc4random_uniform(3) + 1;
            //NSLog(@"Chose lane %d", moveToLane);

            moveToLanePathSwitchStopwatch = new Stopwatch();
            printGameTime = new Stopwatch();


            boughtItems = new List<GenericObject>();
            inProcessOfBuyingItems = false;
            gameCurrentTimeStopwatch = new Stopwatch();

            lastSurrenderStopwatch = new Stopwatch();
            lastHealthtimePassedStopwatch = new Stopwatch();
            healthGainedTimeStopwatch = new Stopwatch();

            lastOnMapLocation.x = -1;
            lastOnMapLocation.y = -1;
            baseLocation.x = -1;
            baseLocation.y = -1;
            moveToLane = lane; //Random lane
            boughtStarterItems = false;
            healthGainedPerSecond = 0;
            lastHealthAmount = 0.0;

            lastDecision = Action.StandStill;

            currentFollowState = FollowState.FollowAnything;
            followStopwatch = new Stopwatch();

            lastTypeMessageStopwatch = new Stopwatch();
            typingMessageStopwatch = new Stopwatch();

            firstGameMessage = false;

            attemptSurrenderStopwatch = new Stopwatch();
            continueClickStopwatch = new Stopwatch();
            afkClickStopwatch = new Stopwatch();
            stoppedWorkingStopwatch = new Stopwatch();

            cantSeeSelfMoveMouseStopwatch = new Stopwatch();

            notMovingTimer = new Stopwatch();

            gameActions = new List<GameAction>();
        }

        void handleAbilityLevelUps()
        {
            int[] abilityLevelUpOrder = { 1, 2, 3, 1, 2, 4, 3, 1, 2, 3, 4, 1, 2, 3, 1, 4, 2, 3 };
            //Level up an ability as soon as possible but only one ability every 500 milliseconds
            if (lastLevelUpStopwatch.DurationInMilliseconds() >= 500)
            {
                lastLevelUpStopwatch.Reset();
                bool leveledUp = false;
                if (detectionData.currentLevel < 18)
                {
                    int preferredLevelUp = abilityLevelUpOrder[detectionData.currentLevel];
                    if (detectionData.spell1LevelUpAvailable || detectionData.spell2LevelUpAvailable ||
                        detectionData.spell3LevelUpAvailable || detectionData.spell4LevelUpAvailable)
                    {
                        if (preferredLevelUp == 1)
                        {
                            levelUpAbility1();
                            leveledUp = true;
                        }
                        else if (preferredLevelUp == 2)
                        {
                            levelUpAbility2();
                            leveledUp = true;
                        }
                        else if (preferredLevelUp == 3)
                        {
                            levelUpAbility3();
                            leveledUp = true;
                        }
                        else if (preferredLevelUp == 4)
                        {
                            levelUpAbility4();
                            leveledUp = true;
                        }
                    }
                    if (detectionData.spell4LevelUpAvailable)
                    {
                        levelUpAbility4();
                        leveledUp = true;
                    }
                    else if (detectionData.spell1LevelUpAvailable)
                    {
                        levelUpAbility1();
                        leveledUp = true;
                    }
                    else if (detectionData.spell2LevelUpAvailable)
                    {
                        levelUpAbility2();
                        leveledUp = true;
                    }
                    else if (detectionData.spell3LevelUpAvailable)
                    {
                        levelUpAbility3();
                        leveledUp = true;
                    }
                }
                if (leveledUp)
                {
                    lastLevelUpStopwatch.Reset();
                }
            }
        }
        void didAction()
        {
            notMovingTimer.Reset();
        }
        void levelUpAbility1()
        {

            addAction(new GameAction(delegate (GameAction action) {

                MotorCortex.pressControlKey();
                Task.Delay(50).ContinueWith(_2 =>
                {
                    MotorCortex.typeText("q");

                    Task.Delay(50).ContinueWith(_3 =>
                    {
                        MotorCortex.releaseControlKey();
                        Task.Delay(50).ContinueWith(_4 =>
                        {
                            MotorCortex.releaseControlKey();
                            action.finished();
                        });
                    });
                });
                didAction();
                
            }, "level_up"));
        }
        void levelUpAbility2()
        {

            addAction(new GameAction(delegate (GameAction action) {



                MotorCortex.pressControlKey();
                Task.Delay(50).ContinueWith(_2 =>
                {
                    MotorCortex.typeText("w");
                    Task.Delay(50).ContinueWith(_3 =>
                    {
                        MotorCortex.releaseControlKey();
                        Task.Delay(50).ContinueWith(_4 =>
                        {
                            MotorCortex.releaseControlKey();
                            action.finished();
                        });
                    });
                });
                didAction();

            }, "level_up"));
        }
        void levelUpAbility3()
        {
            addAction(new GameAction(delegate (GameAction action) {
                
                MotorCortex.pressControlKey();
                Task.Delay(50).ContinueWith(_2 =>
                {
                    MotorCortex.typeText("e");
                    Task.Delay(50).ContinueWith(_3 =>
                    {
                        MotorCortex.releaseControlKey();
                        Task.Delay(50).ContinueWith(_4 =>
                        {
                            MotorCortex.releaseControlKey();
                            action.finished();
                        });
                    });
                });
                didAction();

            }, "level_up"));
            
        }
        void levelUpAbility4()
        {
            addAction(new GameAction(delegate (GameAction action) {


                MotorCortex.pressControlKey();
                Task.Delay(50).ContinueWith(_2 =>
                {
                    MotorCortex.typeText("r");
                    Task.Delay(50).ContinueWith(_3 =>
                    {
                        MotorCortex.releaseControlKey();
                        Task.Delay(50).ContinueWith(_4 =>
                        {
                            MotorCortex.releaseControlKey();
                            action.finished();
                        });
                    });
                });
                didAction();

            }, "level_up"));
            
        }


        void tapStopMoving()
        {
            replaceActionWithAnyTag(new GameAction(delegate (GameAction action) {
                
                MotorCortex.typeText("s");
                action.finished();

            }, "stop_moving"));
            didAction();
        }
        void tapShop()
        {
            replaceActionWithAnyTag(new GameAction(delegate (GameAction action) {

                MotorCortex.typeText("p");
                action.finished();

            }, "tap_shop"));
            didAction();
        }
        void tapCameraLock()
        {
            replaceActionWithAnyTag(new GameAction(delegate (GameAction action) {

                MotorCortex.typeText("y");
                action.finished();

            }, "tap_camera_lock"));
        }
        void tapSpell1()
        {
            replaceActionWithAnyTag(new GameAction(delegate (GameAction action) {

                MotorCortex.typeText("q");
                action.finished();

            }, "tap_spell_1"));
            didAction();
        }
        void tapSpell2()
        {
            replaceActionWithAnyTag(new GameAction(delegate (GameAction action) {

                MotorCortex.typeText("w");
                action.finished();

            }, "tap_spell_2"));
            didAction();
        }
        void tapSpell3()
        {
            replaceActionWithAnyTag(new GameAction(delegate (GameAction action) {

                MotorCortex.typeText("e");
                action.finished();

            }, "tap_spell_3"));
            didAction();
        }
        void tapSpell4()
        {
            replaceActionWithAnyTag(new GameAction(delegate (GameAction action) {

                MotorCortex.typeText("r");
                action.finished();

            }, "tap_spell_4"));
            didAction();
        }
        void tapWard()
        {
            replaceActionWithAnyTag(new GameAction(delegate (GameAction action) {

                MotorCortex.typeText("4");
                action.finished();

            }, "tap_ward"));
        }
        void tapSummonerSpell1()
        {
            replaceActionWithAnyTag(new GameAction(delegate (GameAction action) {

                MotorCortex.typeText("d");
                action.finished();

            }, "tap_summoner_spell_1"));
            didAction();
        }
        void tapSummonerSpell2()
        {
            replaceActionWithAnyTag(new GameAction(delegate (GameAction action) {

                MotorCortex.typeText("f");
                action.finished();

            }, "tap_summoner_spell_2"));
            didAction();
        }
        void tapActive1()
        {
            replaceActionWithAnyTag(new GameAction(delegate (GameAction action) {

                MotorCortex.typeText("1");
                action.finished();

            }, "tap_active_1"));
            didAction();
        }
        void tapActive2()
        {
            replaceActionWithAnyTag(new GameAction(delegate (GameAction action) {

                MotorCortex.typeText("2");
                action.finished();

            }, "tap_active_2"));
            didAction();
        }
        void tapActive3()
        {
            replaceActionWithAnyTag(new GameAction(delegate (GameAction action) {

                MotorCortex.typeText("3");
                action.finished();

            }, "tap_active_3"));
            didAction();
        }
        void tapActive5()
        {
            replaceActionWithAnyTag(new GameAction(delegate (GameAction action) {

                MotorCortex.typeText("5");
                action.finished();

            }, "tap_active_5"));
            didAction();
        }
        void tapActive6()
        {
            replaceActionWithAnyTag(new GameAction(delegate (GameAction action) {

                MotorCortex.typeText("6");
                action.finished();

            }, "tap_active_6"));
            didAction();
        }
        void tapActive7()
        {
            replaceActionWithAnyTag(new GameAction(delegate (GameAction action) {

                MotorCortex.typeText("7");
                action.finished();

            }, "tap_active_7"));
            didAction();
        }
        void tapRecall()
        {
            replaceActionWithAnyTag(new GameAction(delegate (GameAction action) {

                MotorCortex.typeText("b");
                action.finished();

            }, "tap_recall"));
            didAction();
        }
        void tapAttackMove(int x, int y, String id = "tap_attack_move mouse_move")
        {
            replaceActionWithAnyTag(new GameAction(delegate (GameAction action) {
                MotorCortex.moveMouseTo(x, y, 5);
                Task.Delay(50).ContinueWith(_ =>
                {
                    MotorCortex.typeText("a");
                    action.finished();
                });

            }, id));
            didAction();
        }


        unsafe void handleBuyingItems()
        {
            //if (gameState->detectionManager->getShopBottomLeftCornerVisible()) {
            //    NSLog(@"Shop bottom left visible");
            //}
            bool closeShop = false;
            if (inProcessOfBuyingItems && detectionData.shopAvailableShown) return;
            if (lastShopBuyStopwatch.DurationInSeconds() >= 60 * 8 || (boughtStarterItems == false))
            {
                if (detectionData.shopAvailableShown)
                {

                    //Console.WriteLine("Buy Items Shop available");
                    if (detectionData.shopTopLeftCornerShown && detectionData.shopBottomLeftCornerShown)
                    {
                        didAction();
                        //Console.WriteLine("Buy Items Final");
                        lastShopBuyStopwatch.Reset();
                        //Buy items
                        int bought = 0;
                        for (int i = 0; i < detectionData.numberOfBuyableItems; i++)
                        {
                            GenericObject* array = (GenericObject*)detectionData.buyableItemsArray.ToPointer();


                            GenericObject item = array[i];
                            int clickX = item.center.x;
                            int clickY = item.center.y;
                            if (boughtStarterItems && clickY < ((GenericObject*)detectionData.shopTopLeftCorner.ToPointer())->topLeft.y + 200)
                            {
                                continue; //Skip buying this item because we already bought starter items. No troll build.
                            }
                            //Skip buying this item if we already bought it once
                            bool skipBuying = false;
                            foreach (GenericObject boughtItem in boughtItems)
                            {
                                if (Math.Abs(boughtItem.topLeft.y - item.topLeft.y) <= 50 &&
                                    Math.Abs(boughtItem.topLeft.x - item.topLeft.x) <= 50)
                                {
                                    skipBuying = true;
                                    break;
                                }
                            }
                            if (skipBuying)
                            {
                                continue;
                            }

                            tapStopMoving();

                            addAction(new GameAction(delegate (GameAction action) {
                                    MotorCortex.moveMouseTo(clickX, clickY, 5);
                                Task.Delay(1500).ContinueWith(_ =>
                                {
                                    MotorCortex.clickMouseAt(clickX, clickY, 5);
                                });

                                Task.Delay(1500 + 100).ContinueWith(_ =>
                                {
                                    MotorCortex.clickMouseRightAt(clickX, clickY, 5);
                                });
                                Task.Delay(1500 + 100 + 500).ContinueWith(_ =>
                                {

                                    MotorCortex.moveMouseTo(0, 0, 5);
                                    Task.Delay(1500).ContinueWith(_2 =>
                                    {
                                        action.finished();
                                    });
                                });

                            }, "buy_item"));

                            
                            if (bought < 2)
                            {
                                bought++;
                                boughtItems.Add(item);
                            }
                        }
                        if (boughtItems.Count > 0)
                        {
                            inProcessOfBuyingItems = true;
                        }
                        if (boughtItems.Count > 0)
                        {
                            addAction(new GameAction(delegate (GameAction action) {

                                boughtStarterItems = true;
                                inProcessOfBuyingItems = false;

                                action.finished();

                            }, "end buy"));
                        }
                        lastShopBuyingStopwatch.Reset();
                        //NSLog(@"Bought items");
                    }
                    else
                    { //Open up the shop
                        //Console.WriteLine("Buy Items Open Shop");
                        if (lastShopOpenTapStopwatch.DurationInMilliseconds() >= 5000)
                        {
                            lastShopOpenTapStopwatch.Reset();
                            tapShop();
                            //NSLog(@"Opening shop for initial buy");
                        }
                    }
                }
                else
                {
                    if (detectionData.shopTopLeftCornerShown && detectionData.shopBottomLeftCornerShown)
                    {
                        //Console.WriteLine("Buy Items Shop open but not available");
                        closeShop = true;
                        //NSLog(@"Shop not available, closing shop");
                    }
                }
            }
            else
            {
                //Close shop
                if (detectionData.shopTopLeftCornerShown && detectionData.shopBottomLeftCornerShown &&
                    lastShopBuyingStopwatch.DurationInMilliseconds() >= 10000)
                {
                    //Console.WriteLine("Buy Items Shop open but shouldn't be");
                    //Gave a 4 seconds to buy
                    closeShop = true;
                    //NSLog(@"Closing shop because we already bought.");
                }
            }
            if (closeShop)
            {
                if (lastShopCloseTapStopwatch.DurationInMilliseconds() >= 3000)
                {
                    //Console.WriteLine("Buy Items Closing Shop");
                    lastShopCloseTapStopwatch.Reset();
                    tapShop();
                }
            }
        }
        unsafe void handleCameraFocus()
        {
            if (lastCameraFocusStopwatch.DurationInMilliseconds() >= 2000)
            {
                if (detectionData.selfHealthBarVisible && !detectionData.shopTopLeftCornerShown)
                {
                    //We see the health bar at the bottom so lets focus camera
                    if (detectionData.numberOfSelfChampions == 0)
                    {
                        lastCameraFocusStopwatch.Reset();
                        tapCameraLock();
                        //NSLog(@"Attempting camera lock cause we don't see ourselves");
                    }
                    else
                    {
                        Champion champ = ((Champion*)detectionData.selfChampionsArray.ToPointer())[0];
                        int centerX = 536;//1024 / 2;
                        int centerY = 348;// 768 / 2;
                        if (hypot(champ.characterCenter.x - centerX, champ.characterCenter.y - centerY) > 250)
                        {
                            lastCameraFocusStopwatch.Reset();
                            tapCameraLock();
                        }
                    }
                }
            }
        }
        unsafe void handlePlacingWard()
        {
            if (gameCurrentTimeStopwatch.DurationInMinutes() >= 6 && detectionData.numberOfSelfChampions > 0 && detectionData.trinketActiveAvailable &&
                lastPlacedWardStopwatch.DurationInMilliseconds() >= 1500)
            {
                Champion champ = ((Champion*)detectionData.selfChampionsArray.ToPointer())[0];


                addAction(new GameAction(delegate (GameAction action) {

                    MotorCortex.moveMouseTo(champ.characterCenter.x, champ.characterCenter.y, 5);
                    Task.Delay(50).ContinueWith(_ =>
                    {
                        useTrinket();
                        action.finished();
                    });
                    

                }, "place_ward mouse_move"));
                
                //NSLog(@"Placing ward");
            }
        }


        void castSpell1()
        {
            if (lastSpell1UseStopwatch.DurationInMilliseconds() >= 100)
            {
                if (detectionData.spell1ActiveAvailable)
                {
                    tapSpell1();
                    lastSpell1UseStopwatch.Reset();
                }
            }
        }
        void castSpell2()
        {
            if (lastSpell2UseStopwatch.DurationInMilliseconds() >= 100)
            {
                if (detectionData.spell2ActiveAvailable)
                {
                    tapSpell2();
                    lastSpell2UseStopwatch.Reset();
                }
            }
        }
        void castSpell3()
        {
            if (lastSpell3UseStopwatch.DurationInMilliseconds() >= 100)
            {
                if (detectionData.spell3ActiveAvailable)
                {
                    tapSpell3();
                    lastSpell3UseStopwatch.Reset();
                }
            }
        }
        void castSpell4()
        {
            if (lastSpell4UseStopwatch.DurationInMilliseconds() >= 100)
            {
                if (detectionData.spell4ActiveAvailable)
                {
                    tapSpell4();
                    lastSpell4UseStopwatch.Reset();
                }
            }
        }
        void useTrinket()
        {
            if (lastPlacedWardStopwatch.DurationInMilliseconds() >= 500)
            {
                if (detectionData.trinketActiveAvailable)
                {
                    tapWard();
                    lastPlacedWardStopwatch.Reset();
                }
            }
        }
        void castSummonerSpell1()
        {
            if (lastSummonerSpell1UseStopwatch.DurationInMilliseconds() >= 200)
            {
                if (detectionData.summonerSpell1ActiveAvailable)
                {
                    tapSummonerSpell1();
                    lastSummonerSpell1UseStopwatch.Reset();
                }
            }
        }
        void castSummonerSpell2()
        {
            if (lastSummonerSpell2UseStopwatch.DurationInMilliseconds() >= 200)
            {
                if (detectionData.summonerSpell2ActiveAvailable)
                {
                    tapSummonerSpell2();
                    lastSummonerSpell2UseStopwatch.Reset();
                }
            }
        }
        void useItem1()
        {
            if (lastItem1UseStopwatch.DurationInMilliseconds() >= 500)
            {
                //if (gameState->detectionManager->getItem1ActiveAvailable()) {
                tapActive1();
                lastItem1UseStopwatch.Reset();
                //}
            }

        }
        void useItem2()
        {
            if (lastItem2UseStopwatch.DurationInMilliseconds() >= 500)
            {
                //if (gameState->detectionManager->getItem2ActiveAvailable()) {
                tapActive2();
                lastItem2UseStopwatch.Reset();
                //}
            }
        }
        void useItem3()
        {
            if (lastItem3UseStopwatch.DurationInMilliseconds() >= 500)
            {
                //if (gameState->detectionManager->getItem3ActiveAvailable()) {
                tapActive3();
                lastItem3UseStopwatch.Reset();
                //}
            }
        }
        void useItem4()
        {
            if (lastItem4UseStopwatch.DurationInMilliseconds() >= 500)
            {
                //if (gameState->detectionManager->getItem4ActiveAvailable()) {
                tapActive5();
                lastItem4UseStopwatch.Reset();
                //}
            }
        }
        void useItem5()
        {
            if (lastItem5UseStopwatch.DurationInMilliseconds() >= 500)
            {
                //if (gameState->detectionManager->getItem5ActiveAvailable()) {
                tapActive6();
                lastItem5UseStopwatch.Reset();
                //}
            }
        }
        void useItem6()
        {
            if (lastItem6UseStopwatch.DurationInMilliseconds() >= 500)
            {
                //if (gameState->detectionManager->getItem6ActiveAvailable()) {
                tapActive7();
                lastItem6UseStopwatch.Reset();
                //}
            }
        }
        void castRecall()
        {
            if (lastRecallTapStopwatch.DurationInMilliseconds() >= 1000 * 11)
            {
                tapRecall();
                lastRecallTapStopwatch.Reset();
            }
        }

        unsafe Champion* getNearestChampion(Champion* championBars, int numOfChamps, int x, int y)
        {
            Champion* closest = null;
            for (int i = 0; i < numOfChamps; i++)
            {
                Champion* cb = &(championBars[i]);

                if (i == 0)
                {
                    closest = cb;
                }
                else if (hypot(closest->characterCenter.x - x, closest->characterCenter.y - y) > hypot(cb->characterCenter.x - x, cb->characterCenter.y - y))
                {
                    closest = cb;
                }
            }
            return closest;
        }
        unsafe Champion* getLowestHealthChampion(Champion* championBars, int numOfChamps, int x, int y)
        {
            Champion* closest = null;
            for (int i = 0; i < numOfChamps; i++)
            {
                Champion* cb = &(championBars[i]);

                if (i == 0)
                {
                    closest = cb;
                }
                else if (closest->health > cb->health)
                {
                    closest = cb;
                }
                else if (closest->health == cb->health && hypot(closest->characterCenter.x - x, closest->characterCenter.y - y) > hypot(cb->characterCenter.x - x, cb->characterCenter.y - y))
                {
                    closest = cb;
                }
            }
            return closest;
        }
        unsafe Minion* getNearestMinion(Minion* minionBars, int numOfMinions, int x, int y)
        {
            Minion* closest = null;
            for (int i = 0; i < numOfMinions; i++)
            {
                Minion* cb = &(minionBars[i]);

                if (i == 0)
                {
                    closest = cb;
                }
                else if (hypot(closest->characterCenter.x - x, closest->characterCenter.y - y) > hypot(cb->characterCenter.x - x, cb->characterCenter.y - y))
                {
                    closest = cb;
                }
            }
            return closest;
        }
        unsafe Minion* getLowestHealthMinion(Minion* minionBars, int numOfMinions, int x, int y)
        {
            Minion* closest = null;
            for (int i = 0; i < numOfMinions; i++)
            {
                Minion* cb = &(minionBars[i]);

                if (i == 0)
                {
                    closest = cb;
                }
                else if (cb->health < closest->health)
                {
                    closest = cb;
                }
                else if (closest->health == cb->health && hypot(closest->characterCenter.x - x, closest->characterCenter.y - y) > hypot(cb->characterCenter.x - x, cb->characterCenter.y - y))
                {
                    closest = cb;
                }
            }
            return closest;
        }
        unsafe Tower* getLowestHealthTower(Tower* towerBars, int numOfTowers, int x, int y)
        {
            Tower* closest = null;
            for (int i = 0; i < numOfTowers; i++)
            {
                Tower* cb = &(towerBars[i]);

                if (i == 0)
                {
                    closest = cb;
                }
                else if (closest->health > cb->health)
                {
                    closest = cb;
                }
                else if (closest->health == cb->health && hypot(closest->towerCenter.x - x, closest->towerCenter.y - y) > hypot(cb->towerCenter.x - x, cb->towerCenter.y - y))
                {
                    closest = cb;
                }
            }
            return closest;
        }
        unsafe Tower* getNearestTower(Tower* towerBars, int numOfTowers, int x, int y)
        {
            Tower* closest = null;
            for (int i = 0; i < numOfTowers; i++)
            {
                Tower* cb = &(towerBars[i]);

                if (i == 0)
                {
                    closest = cb;
                }
                else if (hypot(closest->towerCenter.x - x, closest->towerCenter.y - y) > hypot(cb->towerCenter.x - x, cb->towerCenter.y - y))
                {
                    closest = cb;
                }
            }
            return closest;
        }
        double hypot(double x, double y)
        {
            return Math.Sqrt(x * x + y * y);
        }

        void normalizePoint(ref int x, ref int y, int length)
        {
            double h = hypot(x, y);
            if (length != 0 && h != 0)
            {
                x = Convert.ToInt32(x * (length / h));
                y = Convert.ToInt32(y * (length / h));
            }
        }

        unsafe void handleMovementAndAttacking()
        {
            //If we see our selves and the shop is closed, then lets move around

            Champion* selfChampions = (Champion*)detectionData.selfChampionsArray.ToPointer();
            bool shopTopLeftCornerVisible = detectionData.shopTopLeftCornerShown;
            //bool mapShopVisible = gameState->detectionManager->getMapShopVisible();
            bool mapVisible = detectionData.mapVisible;
            GenericObject* map = (GenericObject*)detectionData.map.ToPointer();
            //GenericObject* mapShop = gameState->detectionManager->getMapShop();

            bool earlyGame = gameCurrentTimeStopwatch.DurationInMinutes() < 20;

            PositionDouble tempBaseLocation;
            tempBaseLocation.x = baseLocation.x;
            tempBaseLocation.y = baseLocation.y;
            if (baseLocation.x == -1 && mapVisible)
            {
                //Set base location to blue side by default
                tempBaseLocation.x = (map->bottomRight.x - map->topLeft.x) * 0.1 + map->topLeft.x;
                tempBaseLocation.y = (map->bottomRight.y - map->topLeft.y) * 0.9 + map->topLeft.y;

                // Try to set base location
                if (detectionData.mapSelfLocationVisible)
                {
                    baseLocation.x = ((GenericObject*)detectionData.mapSelfLocation.ToPointer())->center.x;
                    baseLocation.y = ((GenericObject*)detectionData.mapSelfLocation.ToPointer())->center.y;
                }
                else if (detectionData.mapShopVisible)
                {
                    baseLocation.x = ((GenericObject*)detectionData.mapShop.ToPointer())->center.x;
                    baseLocation.y = ((GenericObject*)detectionData.mapShop.ToPointer())->center.y;
                }
                if (baseLocation.x != -1)
                {
                    double blueSideX = (map->bottomRight.x - map->topLeft.x) * 0.1 + map->topLeft.x;
                    double blueSideY = (map->bottomRight.y - map->topLeft.y) * 0.9 + map->topLeft.y;
                    double redSideX = (map->bottomRight.x - map->topLeft.x) * 0.9 + map->topLeft.x;
                    double redSideY = (map->bottomRight.y - map->topLeft.y) * 0.1 + map->topLeft.y;
                    //Move blue side closer if at blue side, otherwise move to red side if closer
                    if (hypot(blueSideX - baseLocation.x, blueSideY - baseLocation.y) < hypot(redSideX - baseLocation.x, redSideY - baseLocation.y))
                    {
                        baseLocation.x = blueSideX;
                        baseLocation.y = blueSideY;
                    }
                    else
                    {
                        baseLocation.x = redSideX;
                        baseLocation.y = redSideY;
                    }
                }
            }

            if (detectionData.mapSelfLocationVisible)
            {
                lastOnMapLocation.x = ((GenericObject*)detectionData.mapSelfLocation.ToPointer())->center.x;
                lastOnMapLocation.y = ((GenericObject*)detectionData.mapSelfLocation.ToPointer())->center.y;
            }

            //Change follow states
            if (currentFollowState == FollowState.FollowAnything && followStopwatch.DurationInSeconds() >= 30.0)
            {
                followStopwatch.Reset();
                currentFollowState = FollowState.FollowNothing;
            }
            if (currentFollowState == FollowState.FollowNothing && followStopwatch.DurationInSeconds() >= 10.0)
            {
                followStopwatch.Reset();
                currentFollowState = FollowState.FollowOnlyChampions;
            }
            if (currentFollowState == FollowState.FollowOnlyChampions && followStopwatch.DurationInSeconds() >= 30.0)
            {
                followStopwatch.Reset();
                currentFollowState = FollowState.FollowOnlyMinions;
            }
            if (currentFollowState == FollowState.FollowOnlyMinions && followStopwatch.DurationInSeconds() >= 30.0)
            {
                followStopwatch.Reset();
                currentFollowState = FollowState.FollowAnything;
            }

            //if (baseLocation.x != -1)
            //{
            //    tempBaseLocation = baseLocation;
            //}

            //Calculate health gained per second
            //double healthGainedPerSecond;
            //uint64_t healthGainedTime;
            if (detectionData.selfHealthBarVisible)
            {
                SelfHealth* healthBar = (SelfHealth*)detectionData.selfHealthBar.ToPointer();
                if (healthBar != null)
                {
                    double currentHealth = healthBar->health;
                    double gainedHealthInFrame = lastHealthAmount - currentHealth;
                    lastHealthAmount = currentHealth;
                    healthGainedPerSecond += gainedHealthInFrame * lastHealthtimePassedStopwatch.DurationInSeconds();
                    lastHealthtimePassedStopwatch.Reset();
                }
            }


            bool buyingItems = (lastShopBuyStopwatch.DurationInMilliseconds() >= 1000 * 60 * 8 &&
                detectionData.shopAvailableShown) || (detectionData.shopAvailableShown && boughtStarterItems == false) ||
                (lastShopBuyingStopwatch.DurationInMilliseconds() < 10000 && detectionData.shopAvailableShown);

            if (detectionData.numberOfSelfChampions > 0 && !shopTopLeftCornerVisible && !buyingItems)
            {


                int numberOfSelfChamps = detectionData.numberOfSelfChampions;
                if (numberOfSelfChamps == 0)
                {
                    //NSLog(@"We have a problem");
                }
                Champion* selfChamp = &(((Champion*)detectionData.selfChampionsArray.ToPointer())[0]);
                if (detectionData.selfHealthBarVisible)
                {
                    SelfHealth* health = (SelfHealth*)detectionData.selfHealthBar.ToPointer();
                    if (health != null)
                    {
                        selfChamp->health = health->health;
                    }
                }

                Champion* enemyChampions = (Champion*)detectionData.enemyChampionsArray.ToPointer();
                Minion* enemyMinions = (Minion*)detectionData.enemyMinionsArray.ToPointer();
                Minion* allyMinions = (Minion*)detectionData.allyMinionsArray.ToPointer();
                Champion* allyChampions = (Champion*)detectionData.allyChampionsArray.ToPointer();
                Tower* enemyTowers = (Tower*)detectionData.enemyTowersArray.ToPointer();

                bool enemyChampionsNear = detectionData.numberOfEnemyChampions > 0;

                if (enemyChampionsNear)
                {
                    lastTimeSawEnemyChampStopwatch.Reset();
                }

                bool enemyMinionsNear = detectionData.numberOfEnemyMinions > 0;
                bool allyMinionsNear = detectionData.numberOfAllyMinions > 0;
                bool allyChampionsNear = detectionData.numberOfAllyChampions > 0;
                bool enemyTowerNear = detectionData.numberOfEnemyTowers > 0;
                bool underEnemyTower = false;
                bool enemyChampionWasNear = lastTimeSawEnemyChampStopwatch.DurationInMilliseconds() <= 1000 * 6; //Ten seconds
                if (gameCurrentTimeStopwatch.DurationInSeconds() < 50.0) enemyChampionWasNear = false;                                                                                                 //bool inEarlyGame = getTimeInMilliseconds(mach_absolute_time() - gameCurrentTime) <= 1000*60*8; //Plays safe for first 8 minutes

                Champion* lowestHealthEnemyChampion = getLowestHealthChampion(enemyChampions, detectionData.numberOfEnemyChampions, selfChamp->characterCenter.x, selfChamp->characterCenter.y);
                Champion* closestEnemyChampion = getNearestChampion(enemyChampions, detectionData.numberOfEnemyChampions, selfChamp->characterCenter.x, selfChamp->characterCenter.y);
                Minion* lowestHealthEnemyMinion = getLowestHealthMinion(enemyMinions, detectionData.numberOfEnemyMinions, selfChamp->characterCenter.x, selfChamp->characterCenter.y);
                Minion* closestAllyMinion = getNearestMinion(allyMinions, detectionData.numberOfAllyMinions, selfChamp->characterCenter.x, selfChamp->characterCenter.y);
                Champion* nearestAllyChampion = getNearestChampion(allyChampions, detectionData.numberOfAllyChampions, selfChamp->characterCenter.x, selfChamp->characterCenter.y);
                Tower* nearestEnemyTower = getNearestTower(enemyTowers, detectionData.numberOfEnemyTowers, selfChamp->characterCenter.x, selfChamp->characterCenter.y);

                if (enemyTowerNear && hypot(selfChamp->characterCenter.x - nearestEnemyTower->towerCenter.x, selfChamp->characterCenter.y - nearestEnemyTower->towerCenter.y) < 430)
                {
                    underEnemyTower = true;
                }
                //Initial action is to move to middle lane
                Action action = Action.MoveToMid;

                //If an ally minion is nearby, lets follow them
                if (allyMinionsNear && (currentFollowState == FollowState.FollowAnything || currentFollowState == FollowState.FollowOnlyMinions))
                {
                    action = Action.FollowAllyMinion;
                }
                //Even better, lets follow an ally champion, see if we can help out
                if (allyChampionsNear && (currentFollowState == FollowState.FollowAnything || currentFollowState == FollowState.FollowOnlyChampions))
                {
                    //Only follow ally champions if we're not in base
                    if (detectionData.mapSelfLocationVisible)
                    {
                        PositionDouble mapLoc;
                        mapLoc.x = ((GenericObject*)detectionData.mapSelfLocation.ToPointer())->center.x;
                        mapLoc.y = ((GenericObject*)detectionData.mapSelfLocation.ToPointer())->center.y;
                        if (hypot(mapLoc.x - tempBaseLocation.x, mapLoc.y - tempBaseLocation.y) > 60)
                        {
                            action = Action.FollowAllyChampion;
                        }
                    }
                }
                //Oh look free gold, lets get that
                if (enemyMinionsNear)
                {
                    action = Action.AttackEnemyMinion;
                }

                //If on pad stand still
                if (healthGainedPerSecond >= 1.0)
                { //Gaining 1% health per second
                    action = Action.StandStill;
                }
                if (healthGainedPerSecond <= -1.0)
                { //Losing health rapidly
                    action = Action.RunAway;

                    //Console.WriteLine("Running away cause health decreasing fast");
                }

                //Attack enemy if there are more allies than enemies

                //if (enemyChampionsNear &&
                //    ([allyChampions count] > [enemyChampions count] || ([allyChampions count] == [enemyChampions count] && [allyChampions count] > 2)) && !earlyGame && [allyChampions count] > 0) {

                //Only attack if allies are close
                //     if (hypot(nearestAllyChampion->characterCenter.x - selfChamp->characterCenter.x, nearestAllyChampion->characterCenter.y - selfChamp->characterCenter.y) < 400 ) {
                //        action = ACTION_Attack_Enemy_Champion;
                //     } else {
                //         action = ACTION_Run_Away;
                //     }
                // } else 
                if (enemyChampionsNear && selfChamp->health <= 0.6)
                {
                    //Too many baddies, peace.
                    action = Action.RunAway;

                    //Console.WriteLine("Running away cause enemies nearby");
                }

                if (action == Action.AttackEnemyMinion && enemyTowerNear)
                {
                    if (hypot(lowestHealthEnemyMinion->characterCenter.x - nearestEnemyTower->towerCenter.x, lowestHealthEnemyMinion->characterCenter.y - nearestEnemyTower->towerCenter.y) < 430)
                    {
                        action = Action.RunAway;

                        //Console.WriteLine("Running away cause near enemy tower");

                        if (allyMinionsNear && (currentFollowState == FollowState.FollowAnything || currentFollowState == FollowState.FollowOnlyMinions))
                        {
                            action = Action.FollowAllyMinion;
                        }
                        if (allyChampionsNear && (currentFollowState == FollowState.FollowAnything || currentFollowState == FollowState.FollowOnlyChampions))
                        {
                            action = Action.FollowAllyChampion;
                        }
                    }
                }

                //Attack tower if allied minions under tower
                if (enemyTowerNear && (action == Action.MoveToMid || action == Action.FollowAllyMinion || action == Action.FollowAllyChampion
                    || action == Action.AttackEnemyMinion))
                {
                    int minionsUnderTower = 0;
                    for (int i = 0; i < detectionData.numberOfAllyMinions; i++)
                    {
                        Minion* mb = &(((Minion*)detectionData.allyMinionsArray.ToPointer())[i]);
                        if (hypot(mb->characterCenter.x - nearestEnemyTower->towerCenter.x, mb->characterCenter.y - nearestEnemyTower->towerCenter.y) < 430)
                        {
                            minionsUnderTower++;
                        }
                    }
                    if (minionsUnderTower > 1)
                    {
                        action = Action.AttackTower;
                    }
                    else
                    {
                        action = Action.RunAway;

                        //Console.WriteLine("Running away cause under tower and no minions");
                    }
                }

                //bool enemyChampionCloseEnough = false;
                //if ([enemyChampions count] > 0) {
                //    Champion* closeEnemyChampion = getNearestChampion(enemyChampions, selfChamp->characterCenter.x, selfChamp->characterCenter.y);
                //    if (hypot(closeEnemyChampion->characterCenter.x - selfChamp->characterCenter.x, closeEnemyChampion->characterCenter.y - selfChamp->characterCenter.y) < 600) {
                //        enemyChampionCloseEnough = true;
                //    }
                //}
                if (selfChamp->health < 50 && (enemyMinionsNear || underEnemyTower || enemyChampionsNear || enemyChampionWasNear))
                {
                    action = Action.RunAway;

                    //Console.WriteLine("Running away cause low health 3");
                }
                else if (selfChamp->health < 50 && !enemyChampionsNear && !underEnemyTower)
                {
                    if (selfChamp->health > 35 && !enemyChampionWasNear)
                    {
                        action = Action.Recall;
                    }
                    else
                    {
                        action = Action.RunAway;
                        //Console.WriteLine("Running away cause low health 2");
                        //Console.WriteLine("Health: " + selfChamp->health + ", enemyChampionWasNear: " + enemyChampionWasNear + "\n");
                    }
                }
                else if (selfChamp->health <= 35)
                {
                    action = Action.RunAway;
                    //Console.WriteLine("Running away cause low health");
                }
                if (detectionData.potionActiveAvailable && selfChamp->health < 80)
                {
                    if (detectionData.potionOnActive == 1) useItem1();
                    if (detectionData.potionOnActive == 2) useItem2();
                    if (detectionData.potionOnActive == 3) useItem3();
                    if (detectionData.potionOnActive == 4) useItem4();
                    if (detectionData.potionOnActive == 5) useItem5();
                    if (detectionData.potionOnActive == 6) useItem6();
                }

                //Go ham
                if (enemyChampionsNear && lowestHealthEnemyChampion->health < 5 && !earlyGame)
                {
                    action = Action.GoHam;
                }

                if (action == Action.AttackEnemyMinion && detectionData.numberOfAllyMinions + detectionData.numberOfAllyChampions < 2
                    && detectionData.numberOfEnemyMinions > 2)
                {
                    action = Action.RunAway;

                    //Console.WriteLine("Running away cause not enough allies nearby");
                }

                //Ham code
                if (gameCurrentTimeStopwatch.DurationInMinutes() >= 15 && enemyChampionsNear
                    && //1 on 1 or team fight
                    detectionData.numberOfEnemyChampions <= detectionData.numberOfAllyChampions + 1
                    && //Not under tower or the enemy is really low
                    (underEnemyTower == false || (lowestHealthEnemyChampion->health <= 0.25 && lastHealthAmount >= 0.5))
                    && //We think we can take him
                    lowestHealthEnemyChampion->health <= lastHealthAmount
                    && //Make sure we have a fair amount of minions or the enemy is way lower
                    (detectionData.numberOfEnemyMinions <= detectionData.numberOfAllyMinions + 1 ||
                        lastHealthAmount - lowestHealthEnemyChampion->health >= 0.25)
                    && ((hypot(closestEnemyChampion->characterCenter.x - selfChamp->characterCenter.x, closestEnemyChampion->characterCenter.y - selfChamp->characterCenter.y) < 250
                     || (lowestHealthEnemyChampion->health <= 0.25 && detectionData.numberOfEnemyMinions <= 2)))
                        )
                {
                    action = Action.GoHam;
                }
                //Already dead so do damage
                if (gameCurrentTimeStopwatch.DurationInMinutes() >= 14 && enemyChampionsNear && lastHealthAmount <= 0.1)
                {
                    action = Action.GoHam;
                }
                //Attack enemy if they are right next to us
                if (gameCurrentTimeStopwatch.DurationInMinutes() >= 14 && action != Action.RunAway && detectionData.numberOfEnemyChampions > 0)
                {
                    if (hypot(closestEnemyChampion->characterCenter.x - selfChamp->characterCenter.x, closestEnemyChampion->characterCenter.y - selfChamp->characterCenter.y) < 150)
                    {
                        action = Action.GoHam;
                    }
                }

                //Switch action if in base
                if (tempBaseLocation.x == -1 || (hypot(lastOnMapLocation.x - tempBaseLocation.x, lastOnMapLocation.y - tempBaseLocation.y) <= 100))
                {
                    if (action == Action.FollowAllyChampion || action == Action.FollowAllyMinion)
                    {
                        action = Action.MoveToMid;
                    }
                }

                if ((gameCurrentTimeStopwatch.DurationInMinutes() <= 10 && lastHealthAmount < 0.5 && detectionData.numberOfEnemyChampions >= 1) ||
                    (gameCurrentTimeStopwatch.DurationInMinutes() <= 5 && lastHealthAmount < 0.75 && detectionData.numberOfEnemyChampions >= 1))
                {
                    action = Action.RunAway;
                }
                if (gameCurrentTimeStopwatch.DurationInMinutes() <= 10 && (detectionData.numberOfEnemyChampions >= 2 || (detectionData.numberOfEnemyChampions == 1 && detectionData.numberOfAllyMinions <= 2)))
                {
                    action = Action.RunAway;
                }
                if (gameCurrentTimeStopwatch.DurationInMinutes() <= 10 && detectionData.numberOfEnemyChampions >= 1 && detectionData.numberOfEnemyTowers >= 1)
                {
                    action = Action.RunAway;
                }

                //int actionSpeed = 0.25;
                lastDecision = action;
                //Console.WriteLine("");
                switch (action)
                {
                    case Action.RunAway:
                        {
                            //Console.WriteLine("Action: Running Away");
                            didAction();
                            if (lastRunAwayClickStopwatch.DurationInMilliseconds() >= 1000)
                            {
                                //clearActions();
                                replaceActionWithAnyTag(new GameAction(delegate (GameAction gameAction) {

                                    MotorCortex.clickMouseRightAt(Convert.ToInt32(baseLocation.x), Convert.ToInt32(baseLocation.y), 5);
                                    Task.Delay(50).ContinueWith(_ =>
                                    {
                                        useTrinket();
                                        gameAction.finished();
                                    });


                                }, "run_away mouse_move"));
                                
                                lastRunAwayClickStopwatch.Reset();
                            }
                            if (runAwayPanicStopwatch.DurationInSeconds() >= 10.0)
                            {

                                bool enemyChampWayTooClose = false;
                                if (closestEnemyChampion != null)
                                {
                                    enemyChampWayTooClose = (hypot(selfChamp->characterCenter.x - closestEnemyChampion->characterCenter.x, selfChamp->characterCenter.y - closestEnemyChampion->characterCenter.y) < 200);
                                }

                                if ((selfChamp->health < 40 && enemyChampionsNear) || enemyChampWayTooClose)
                                {
                                    runAwayPanicStopwatch.Reset();
                                    int enemyX = (closestEnemyChampion->characterCenter.x - selfChamp->characterCenter.x);
                                    int enemyY = (closestEnemyChampion->characterCenter.y - selfChamp->characterCenter.y);
                                    normalizePoint(ref enemyX, ref enemyY, 300);
                                    //enemyX = -enemyX;
                                    //enemyY = -enemyY;
                                    //Panic
                                    if (lastMoveMouseStopwatch.DurationInMilliseconds() >= 1000 || (lastHealthAmount <= 0.25 && lastMoveMouseStopwatch.DurationInMilliseconds() >= 500))
                                    {
                                        lastMoveMouseStopwatch.Reset();


                                        replaceActionWithAnyTag(new GameAction(delegate (GameAction gameAction) {

                                            MotorCortex.moveMouseTo(enemyX + selfChamp->characterCenter.x, enemyY + selfChamp->characterCenter.y, 5);
                                            Task.Delay(50).ContinueWith(_ =>
                                            {
                                                castSpell4();
                                                castSpell2();
                                                if (lastHealthAmount <= 0.25)
                                                {
                                                    castSpell1();
                                                    castSpell3();
                                                }
                                                useTrinket();

                                                Task.Delay(50).ContinueWith(_2 =>
                                                {
                                                    MotorCortex.moveMouseTo(selfChamp->characterCenter.x - enemyX, selfChamp->characterCenter.y - enemyY, 5);
                                                    castSummonerSpell1();
                                                    castSummonerSpell2();
                                                    useItem1();
                                                    useItem2();
                                                    useItem3();
                                                    useItem4();
                                                    useItem5();
                                                    useItem6();
                                                });


                                                gameAction.finished();
                                            });


                                        }, "panic mouse_move"));

                                        
                                    }
                                }
                            }
                        }
                            
                        break;
                    case Action.AttackEnemyChampion:
                    case Action.GoHam:
                        {
                            didAction();
                            //Console.WriteLine("Action: Attacking Enemy Champion");
                            //NSLog(@"\t\tAction: Attacking enemy champion");
                            int x = lowestHealthEnemyChampion->characterCenter.x;
                            int y = lowestHealthEnemyChampion->characterCenter.y;
                            if (lastClickEnemyChampStopwatch.DurationInMilliseconds() >= 100)
                            {
                                //clearActions();
                                lastClickEnemyChampStopwatch.Reset();
                                tapAttackMove(lowestHealthEnemyChampion->characterCenter.x, lowestHealthEnemyChampion->characterCenter.y);
                            }
                            if (lastMoveMouseStopwatch.DurationInMilliseconds() >= 500)
                            {
                                lastMoveMouseStopwatch.Reset();
                                MotorCortex.moveMouseTo(x, y, 5);

                                replaceActionWithAnyTag(new GameAction(delegate (GameAction gameAction) {

                                    castSpell3();
                                    castSpell2();
                                    castSpell1();
                                    if (enemyChampionsNear || action == Action.GoHam)
                                    {
                                        if (lowestHealthEnemyChampion->health <= 0.35 || action == Action.GoHam)
                                        {
                                            castSummonerSpell1();
                                            castSummonerSpell2();
                                            castSpell4();
                                        }
                                    }
                                    useItem1();
                                    useItem2();
                                    useItem3();
                                    useItem4();
                                    useItem5();
                                    useItem6();
                                    useTrinket();


                                    gameAction.finished();


                                }, "panic mouse_move"));
                            }

                            

                            

                        }
                        break;
                    case Action.AttackEnemyMinion:
                        {
                            didAction();
                            //Console.WriteLine("Action: Attacking Enemy Minion");
                            //NSLog(@"\t\tAction: Attacking Enemy Minion");
                            if (lastClickEnemyMinionStopwatch.DurationInMilliseconds() >= 100)
                            {
                                //clearActions();
                                lastClickEnemyMinionStopwatch.Reset();
                                
                                 tapAttackMove(lowestHealthEnemyMinion->characterCenter.x, lowestHealthEnemyMinion->characterCenter.y);
                                    
                                
                            }
                            if (lastMoveMouseStopwatch.DurationInMilliseconds() >= 500)
                            {
                                lastMoveMouseStopwatch.Reset();

                                replaceActionWithAnyTag(new GameAction(delegate (GameAction gameAction) {

                                    MotorCortex.moveMouseTo(lowestHealthEnemyMinion->characterCenter.x, lowestHealthEnemyMinion->characterCenter.y, 5);
                                    Task.Delay(50).ContinueWith(_ =>
                                    {
                                        gameAction.finished();
                                    });


                                }, "mouse_move"));
                                
                            }
                            castSpell1();
                            castSpell3();
                            if (selfChamp->health < 50) { castSpell2(); }
                        }
                        break;
                    case Action.AttackTower:
                        {
                            didAction();
                            //Console.WriteLine("\t\tAction: Attacking Tower");
                            if (lastClickEnemyTowerStopwatch.DurationInMilliseconds() >= 500)
                            {
                                //clearActions();
                                lastClickEnemyTowerStopwatch.Reset();
                                tapAttackMove(nearestEnemyTower->towerCenter.x, nearestEnemyTower->towerCenter.y);
                            }
                            if (lastMoveMouseStopwatch.DurationInMilliseconds() >= 500)
                            {
                                lastMoveMouseStopwatch.Reset();

                                replaceActionWithAnyTag(new GameAction(delegate (GameAction gameAction) {

                                    MotorCortex.moveMouseTo(nearestEnemyTower->towerCenter.x, nearestEnemyTower->towerCenter.y, 5);
                                    Task.Delay(50).ContinueWith(_ =>
                                    {
                                        gameAction.finished();
                                    });


                                }, "mouse_move"));
                            }
                            castSpell1();
                            castSpell3();
                            if (selfChamp->health < 50) { castSpell2(); }
                        }
                        break;
                    case Action.FollowAllyChampion:
                        {
                            didAction();
                            //Console.WriteLine("Action: Following Ally Champion");
                            //NSLog(@"\t\tAction: Following Ally Champion");
                            if (lastClickAllyChampionStopwatch.DurationInMilliseconds() >= 100)
                            {
                                lastClickAllyChampionStopwatch.Reset();
                                int xMove = (nearestAllyChampion->characterCenter.x - selfChamp->characterCenter.x);
                                int yMove = (nearestAllyChampion->characterCenter.y - selfChamp->characterCenter.y);
                                normalizePoint(ref xMove, ref yMove, 300);
                                tapAttackMove(xMove + selfChamp->characterCenter.x, yMove + selfChamp->characterCenter.y);
                            }
                        }
                        break;
                    case Action.FollowAllyMinion:
                        {
                            didAction();
                            //Console.WriteLine("Action: Following Ally Minion");
                            //NSLog(@"\t\tAction: Following Ally Minion");
                            if (lastClickAllyMinionStopwatch.DurationInMilliseconds() >= 100)
                            {
                                lastClickAllyMinionStopwatch.Reset();
                                int xMove = (closestAllyMinion->characterCenter.x - selfChamp->characterCenter.x);
                                int yMove = (closestAllyMinion->characterCenter.y - selfChamp->characterCenter.y);
                                normalizePoint(ref xMove, ref yMove, 300);
                                tapAttackMove(xMove + selfChamp->characterCenter.x, yMove + selfChamp->characterCenter.y);
                            }
                        }
                        break;
                    case Action.MoveToMid:
                        {
                            didAction();
                            //Console.WriteLine("Action: Moving to Mid");
                            //NSLog(@"\t\tAction: Moving to Mid");

                            if (gameCurrentTimeStopwatch.DurationInMinutes() >= 10 && moveToLanePathSwitchStopwatch.DurationInMinutes() >= 1)
                            {
                                //Console.WriteLine("Switching lane");
                                //Switch to a random lane after 20 min
                                moveToLane += 1;
                                if (moveToLane > 3) moveToLane = 1;
                                moveToLanePathSwitchStopwatch.Reset();
                                /*
                                AppDelegate* appDelegate = (AppDelegate*)[[NSApplication sharedApplication] delegate];
                                if (appDelegate->basicAI->moveToLane == 1)
                                {
                        [appDelegate->topLaneCheckbox setState: NSOnState];
                        [appDelegate->midLaneCheckbox setState: NSOffState];
                        [appDelegate->bottomLaneCheckbox setState: NSOffState];
                        
                    } else if (appDelegate->basicAI->moveToLane == 2) {
                        [appDelegate->topLaneCheckbox setState: NSOffState];
                        [appDelegate->midLaneCheckbox setState: NSOnState];
                        [appDelegate->bottomLaneCheckbox setState: NSOffState];
                    } else if (appDelegate->basicAI->moveToLane == 3) {
                        [appDelegate->topLaneCheckbox setState: NSOffState];
                        [appDelegate->midLaneCheckbox setState: NSOffState];
                        [appDelegate->bottomLaneCheckbox setState: NSOnState];
                    }*/
                            }

                            if (lastMovementClickStopwatch.DurationInMilliseconds() >= 500)
                            {
                                //NSLog(@"Time to move");
                                //Fuzzy movement
                                if (fuzzyLaneMovementStopwatch.DurationInSeconds() >= 1)
                                {
                                    fuzzyLaneMovementStopwatch.Reset();
                                    fuzzyLaneMovementX = random.NextDouble() * 2.0 - 1.0;
                                    fuzzyLaneMovementY = random.NextDouble() * 2.0 - 1.0;
                                }
                                if (mapVisible)
                                {

                                    //NSLog(@"Initating click");
                                    lastMovementClickStopwatch.Reset();
                                    int x = map->center.x;
                                    int y = map->center.y;
                                    if (moveToLane == 1)
                                    {
                                        x = Convert.ToInt32((map->bottomRight.x - map->topLeft.x) * 0.2 + map->topLeft.x);
                                        y = Convert.ToInt32((map->bottomRight.y - map->topLeft.y) * 0.2 + map->topLeft.y);
                                    }
                                    if (moveToLane == 3)
                                    {
                                        x = Convert.ToInt32((map->bottomRight.x - map->topLeft.x) * 0.8 + map->topLeft.x);
                                        y = Convert.ToInt32((map->bottomRight.y - map->topLeft.y) * 0.8 + map->topLeft.y);
                                    }
                                    int fuzzyOffsetX = (int)( Math.Round(10.0 * fuzzyLaneMovementX) );
                                    int fuzzyOffsetY = (int)(Math.Round(10.0 * fuzzyLaneMovementY));

                                    addAction(new GameAction(delegate (GameAction gameAction) {


                                        MotorCortex.clickMouseRightAt(x + fuzzyOffsetX, y + fuzzyOffsetY, 5);
                                        Task.Delay(50).ContinueWith(_ =>
                                        {
                                            gameAction.finished();
                                        });


                                    }, "move_to_lane mouse_move"));

                                    //Console.WriteLine("Clicked position to move to: " + x + ", " + y);
                                }
                                else
                                {
                                    //Console.WriteLine("Trying to move but map not visible");
                                }
                            }
                        }
                        break;
                    case Action.Recall:
                        {
                            didAction();
                            //Console.WriteLine("Action: Recalling");
                            //NSLog(@"\t\tAction: Recalling");
                            castRecall();
                        }
                        break;
                    case Action.StandStill:
                        {
                            //Console.WriteLine("Action: Standing Still");
                            if (standStillTimeStopwatch.DurationInMilliseconds() >= 500)
                            {
                                standStillTimeStopwatch.Reset();
                                tapStopMoving();
                            }
                        }
                        break;

                    default:
                        break;
                }
                if (activeAutoUseTimeStopwatch.DurationInMilliseconds() >= 1000 * 60 * 5)
                {
                    activeAutoUseTimeStopwatch.Reset();
                    MotorCortex.moveMouseTo(selfChamp->characterCenter.x, selfChamp->characterCenter.y, 5);
                    //Use all actives
                    useItem1();
                    useItem2();
                    useItem3();
                    useItem4();
                    useItem5();
                    useItem6();
                    useTrinket();
                }
            }
            else if (mapVisible && !shopTopLeftCornerVisible && !buyingItems)
            {
                if (moveToLanePathSwitchStopwatch.DurationInMilliseconds() >= 1000 * 60 * 20)
                {
                    //Switch to a random lane after 20 min
                    moveToLane = random.Next(3) + 1;
                    moveToLanePathSwitchStopwatch.Reset();
                    /*
AppDelegate* appDelegate = (AppDelegate*)[[NSApplication sharedApplication] delegate];
            if (appDelegate->basicAI->moveToLane == 1) {
                [appDelegate->topLaneCheckbox setState: NSOnState];
                [appDelegate->midLaneCheckbox setState: NSOffState];
                [appDelegate->bottomLaneCheckbox setState: NSOffState];
            } else if (appDelegate->basicAI->moveToLane == 2) {
                [appDelegate->topLaneCheckbox setState: NSOffState];
                [appDelegate->midLaneCheckbox setState: NSOnState];
                [appDelegate->bottomLaneCheckbox setState: NSOffState];
            } else if (appDelegate->basicAI->moveToLane == 3) {
                [appDelegate->topLaneCheckbox setState: NSOffState];
                [appDelegate->midLaneCheckbox setState: NSOffState];
                [appDelegate->bottomLaneCheckbox setState: NSOnState];
            }*/
                }

                if (lastMovementClickStopwatch.DurationInMilliseconds() >= 500)
                {
                    //NSLog(@"Time to move");
                    if (mapVisible)
                    {
                        didAction();
                        //NSLog(@"Initating click");
                        lastMovementClickStopwatch.Reset();
                        int x = map->center.x;
                        int y = map->center.y;
                        if (moveToLane == 1)
                        {
                            x = Convert.ToInt32((map->bottomRight.x - map->topLeft.x) * 0.2 + map->topLeft.x);
                            y = Convert.ToInt32((map->bottomRight.y - map->topLeft.y) * 0.2 + map->topLeft.y);
                        }
                        if (moveToLane == 3)
                        {
                            x = Convert.ToInt32((map->bottomRight.x - map->topLeft.x) * 0.9 + map->topLeft.x);
                            y = Convert.ToInt32((map->bottomRight.y - map->topLeft.y) * 0.9 + map->topLeft.y);
                        }
                        if (fuzzyLaneMovementStopwatch.DurationInSeconds() >= 1)
                        {
                            fuzzyLaneMovementStopwatch.Reset();
                            fuzzyLaneMovementX = random.NextDouble() * 2.0 - 1.0;
                            fuzzyLaneMovementY = random.NextDouble() * 2.0 - 1.0;
                        }

                        int fuzzyOffsetX = (int)(Math.Round(10.0 * fuzzyLaneMovementX));
                        int fuzzyOffsetY = (int)(Math.Round(10.0 * fuzzyLaneMovementY));

                        addAction(new GameAction(delegate (GameAction gameAction) {
                            Console.WriteLine("Moving to lane");

                            Stopwatch testWatch = new Stopwatch();

                            MotorCortex.clickMouseRightAt(x + fuzzyOffsetX, y + fuzzyOffsetY, 5);
                            Task.Delay(50).ContinueWith(_ =>
                            {
                                Console.WriteLine("Finished move mouse to lane in: " + testWatch.DurationInMilliseconds() + " milliseconds");

                                gameAction.finished();
                            });


                        }, "move_to_lane mouse_move"));
                        MotorCortex.clickMouseRightAt(x, y, 5);
                    }// else {
                     //    NSLog(@"Map not visible");
                     //}
                }
            }
            else if (buyingItems)
            {
                if (standStillTimeStopwatch.DurationInMilliseconds() >= 250)
                {
                    standStillTimeStopwatch.Reset();
                    tapStopMoving();
                    didAction();
                }
            }
            if (healthGainedTimeStopwatch.DurationInMilliseconds() >= 1000)
            {
                healthGainedPerSecond = 0.1;
                healthGainedTimeStopwatch.Reset();
            }
        }

    }
}