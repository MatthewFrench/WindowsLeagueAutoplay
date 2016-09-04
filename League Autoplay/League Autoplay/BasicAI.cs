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
        enum Action
        {
            RunAway, AttackEnemyChampion, AttackEnemyMinion, FollowAllyChampion, FollowAllyMinion, MoveToMid,
            Recall, AttackTower, GoHam, StandStill
        };
        Action lastDecision;
        int moveToLane;

        DetectionDataStruct detectionData;

        Stopwatch lastLevelUpStopwatch, lastShopBuyingStopwatch, lastCameraFocusStopwatch, lastPlacedWardStopwatch,
            lastRunAwayClickStopwatch, lastClickEnemyChampStopwatch, lastMovementClickStopwatch, lastClickAllyMinionStopwatch,
            lastClickEnemyMinionStopwatch, lastClickEnemyTowerStopwatch, lastClickAllyChampionStopwatch, lastMoveMouseStopwatch,
            lastRecallTapStopwatch, lastSpell1UseStopwatch, lastSpell2UseStopwatch, lastSpell3UseStopwatch, lastSpell4UseStopwatch,
            lastSummonerSpell1UseStopwatch, lastSummonerSpell2UseStopwatch, lastItem1UseStopwatch, lastItem2UseStopwatch,
            lastItem3UseStopwatch, lastItem4UseStopwatch, lastItem5UseStopwatch, lastItem6UseStopwatch, activeAutoUseTimeStopwatch,
            moveToLanePathSwitchStopwatch, gameCurrentTimeStopwatch, lastSurrenderStopwatch, lastShopBuyStopwatch,
            lastShopOpenTapStopwatch, lastShopCloseTapStopwatch, lastTimeSawEnemyChampStopwatch, standStillTimeStopwatch,
            healthGainedTimeStopwatch;


        bool boughtStarterItems;
        List<GenericObject> boughtItems;

        PositionDouble baseLocation;

        double healthGainedPerSecond;
        double lastHealthAmount;
        Stopwatch lastHealthtimePassedStopwatch;

        Random random;



        public BasicAI()
        {

            resetAI();
            random = new Random();
        }

        public void resetAI()
        {
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

            activeAutoUseTimeStopwatch = new Stopwatch();
            lastTimeSawEnemyChampStopwatch = new Stopwatch();


            //moveToLane = arc4random_uniform(3) + 1;
            //NSLog(@"Chose lane %d", moveToLane);

            moveToLanePathSwitchStopwatch = new Stopwatch();
            

            boughtItems = new List<GenericObject>();
            gameCurrentTimeStopwatch = new Stopwatch();

            lastSurrenderStopwatch = new Stopwatch();
            lastHealthtimePassedStopwatch = new Stopwatch();
            healthGainedTimeStopwatch = new Stopwatch();

            baseLocation.x = -1;
            baseLocation.y = -1;
            moveToLane = 1; //Top lane
            boughtStarterItems = false;
            healthGainedPerSecond = 0;
            lastHealthAmount = 0.0;

            lastDecision = Action.StandStill;
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
        void levelUpAbility1()
        {
            MotorCortex.typeText("^q");
        }
        void levelUpAbility2()
        {
            MotorCortex.typeText("^w");
        }
        void levelUpAbility3()
        {
            MotorCortex.typeText("^e");
        }
        void levelUpAbility4()
        {
            MotorCortex.typeText("^r");
        }


        void tapStopMoving()
        {
            MotorCortex.typeText("^q");
        }
        void tapShop()
        {
            MotorCortex.typeText("p");
        }
        void tapCameraLock()
        {
            MotorCortex.typeText("y");
        }
        void tapSpell1()
        {
            MotorCortex.typeText("q");
        }
        void tapSpell2()
        {
            MotorCortex.typeText("w");
        }
        void tapSpell3()
        {
            MotorCortex.typeText("e");
        }
        void tapSpell4()
        {
            MotorCortex.typeText("r");
        }
        void tapWard()
        {
            MotorCortex.typeText("4");
        }
        void tapSummonerSpell1()
        {
            MotorCortex.typeText("d");
        }
        void tapSummonerSpell2()
        {
            MotorCortex.typeText("f");
        }
        void tapActive1()
        {
            MotorCortex.typeText("1");
        }
        void tapActive2()
        {
            MotorCortex.typeText("2");
        }
        void tapActive3()
        {
            MotorCortex.typeText("3");
        }
        void tapActive5()
        {
            MotorCortex.typeText("5");
        }
        void tapActive6()
        {
            MotorCortex.typeText("6");
        }
        void tapActive7()
        {
            MotorCortex.typeText("7");
        }
        void tapRecall()
        {
            MotorCortex.typeText("b");
        }
        void tapAttackMove(int x, int y)
        {
            MotorCortex.moveMouseTo(x, y);
            MotorCortex.typeText("a");
        }


        unsafe void handleBuyingItems()
        {
            //if (gameState->detectionManager->getShopBottomLeftCornerVisible()) {
            //    NSLog(@"Shop bottom left visible");
            //}
            bool closeShop = false;
            if (lastShopBuyStopwatch.DurationInMilliseconds() >= 1000 * 60 * 8 || (boughtStarterItems == false))
            {
                Console.WriteLine("Buy Items");
                if (detectionData.shopAvailableShown)
                {
                    Console.WriteLine("Buy Items Shop available");
                    if (detectionData.shopTopLeftCornerShown && detectionData.shopBottomLeftCornerShown)
                    {
                        Console.WriteLine("Buy Items Final");
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
                            Task.Delay(1000 * i).ContinueWith(_ =>
                            {
                                MotorCortex.moveMouseTo(clickX, clickY);
                            });
                            Task.Delay(1000 * i + 200).ContinueWith(_ =>
                            {
                                MotorCortex.clickMouseAt(clickX, clickY);
                            });
                            Task.Delay(1000 * i + 450).ContinueWith(_ =>
                            {
                                MotorCortex.clickMouseTwiceAt(clickX, clickY);
                            });
                            Task.Delay(1000 * i + 700).ContinueWith(_ =>
                            {
                                MotorCortex.moveMouseTo(0, 0);
                            });
                            if (bought < 2)
                            {
                                bought++;
                                boughtItems.Add(item);
                            }
                        }
                        if (boughtItems.Count > 0 && !boughtStarterItems)
                        {
                            boughtStarterItems = true;
                        }
                        lastShopBuyingStopwatch.Reset();
                        //NSLog(@"Bought items");
                    }
                    else
                    { //Open up the shop
                        Console.WriteLine("Buy Items Open Shop");
                        if (lastShopOpenTapStopwatch.DurationInMilliseconds() >= 2000)
                        {
                            lastShopOpenTapStopwatch.Reset();
                            tapStopMoving();
                            tapShop();
                            //NSLog(@"Opening shop for initial buy");
                        }
                    }
                }
                else
                {
                    if (detectionData.shopTopLeftCornerShown && detectionData.shopBottomLeftCornerShown)
                    {
                        Console.WriteLine("Buy Items Shop open but not available");
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
                    Console.WriteLine("Buy Items Shop open but shouldn't be");
                    //Gave a 4 seconds to buy
                    closeShop = true;
                    //NSLog(@"Closing shop because we already bought.");
                }
            }
            if (closeShop)
            {
                if (lastShopCloseTapStopwatch.DurationInMilliseconds() >= 500)
                {
                    Console.WriteLine("Buy Items Closing Shop");
                    lastShopCloseTapStopwatch.Reset();
                    tapShop();
                }
            }
        }
        void handleCameraFocus()
        {
            if (lastCameraFocusStopwatch.DurationInMilliseconds() >= 4000)
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
                }
            }
        }
        unsafe void handlePlacingWard()
        {
            if (detectionData.numberOfSelfChampions > 0 && detectionData.trinketActiveAvailable &&
                lastPlacedWardStopwatch.DurationInMilliseconds() >= 1500)
            {
                Champion champ = ((Champion*)detectionData.selfChampionsArray.ToPointer())[0];
                MotorCortex.moveMouseTo(champ.characterCenter.x, champ.characterCenter.y);
                useTrinket();
                //NSLog(@"Placing ward");
            }
        }


        public unsafe void processAI(DetectionDataStruct data)
        {
            detectionData = data;

            handleAbilityLevelUps();
            handleBuyingItems();
            handleCameraFocus();
            handlePlacingWard();
            handleMovementAndAttacking();

            if (lastSurrenderStopwatch.DurationInMilliseconds() >= 1000 && detectionData.surrenderAvailable)
            {
                lastSurrenderStopwatch.Reset();
                GenericObject* surrender = (GenericObject*)detectionData.surrenderActive.ToPointer();
                MotorCortex.clickMouseAt(surrender->center.x, surrender->center.y);
            }
        }
        

        void castSpell1()
        {
            if (lastSpell1UseStopwatch.DurationInMilliseconds() >= 80)
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
            if (lastSpell2UseStopwatch.DurationInMilliseconds() >= 80)
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
            if (lastSpell3UseStopwatch.DurationInMilliseconds() >= 80)
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
            if (lastSpell4UseStopwatch.DurationInMilliseconds() >= 80)
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
            if (lastItem1UseStopwatch.DurationInMilliseconds() >= 200)
            {
                //if (gameState->detectionManager->getItem1ActiveAvailable()) {
                tapActive1();
                lastItem1UseStopwatch.Reset();
                //}
            }

        }
        void useItem2()
        {
            if (lastItem2UseStopwatch.DurationInMilliseconds() >= 200)
            {
                //if (gameState->detectionManager->getItem2ActiveAvailable()) {
                tapActive2();
                lastItem2UseStopwatch.Reset();
                //}
            }
        }
        void useItem3()
        {
            if (lastItem3UseStopwatch.DurationInMilliseconds() >= 200)
            {
                //if (gameState->detectionManager->getItem3ActiveAvailable()) {
                tapActive3();
                lastItem3UseStopwatch.Reset();
                //}
            }
        }
        void useItem4()
        {
            if (lastItem4UseStopwatch.DurationInMilliseconds() >= 200)
            {
                //if (gameState->detectionManager->getItem4ActiveAvailable()) {
                tapActive5();
                lastItem4UseStopwatch.Reset();
                //}
            }
        }
        void useItem5()
        {
            if (lastItem5UseStopwatch.DurationInMilliseconds() >= 200)
            {
                //if (gameState->detectionManager->getItem5ActiveAvailable()) {
                tapActive6();
                lastItem5UseStopwatch.Reset();
                //}
            }
        }
        void useItem6()
        {
            if (lastItem6UseStopwatch.DurationInMilliseconds() >= 200)
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

            bool earlyGame = gameCurrentTimeStopwatch.DurationInMilliseconds() < 1000 * 60 * 8;

            PositionDouble tempBaseLocation;
            tempBaseLocation.x = 0;
            tempBaseLocation.y = 0;
            if (baseLocation.x == -1 && mapVisible)
            {
                //Set base location to blue side by default
                tempBaseLocation.x = (map->bottomRight.x - map->topLeft.x) * 0.1 + map->topLeft.x;
                tempBaseLocation.y = (map->bottomRight.y - map->topLeft.y) * 0.9 + map->topLeft.y;
            }
            if (baseLocation.x == -1)
            { // Try to set base location
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
            if (baseLocation.x != -1)
            {
                tempBaseLocation = baseLocation;
            }

            //Calculate health gained per second
            //double healthGainedPerSecond;
            //uint64_t healthGainedTime;
            if (detectionData.selfHealthBarVisible)
            {
                double currentHealth = ((SelfHealth*)detectionData.selfHealthBar.ToPointer())->health;
                double gainedHealthInFrame = lastHealthAmount - currentHealth;
                lastHealthAmount = currentHealth;
                healthGainedPerSecond += gainedHealthInFrame * lastHealthtimePassedStopwatch.DurationInSeconds();
                lastHealthtimePassedStopwatch.Reset();
            }


            bool buyingItems = lastShopBuyStopwatch.DurationInMilliseconds() >= 1000 * 60 * 8 &&
                detectionData.shopAvailableShown;

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
                    selfChamp->health = ((SelfHealth*)detectionData.selfHealthBar.ToPointer())->health;
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
                bool enemyChampionWasNear = lastTimeSawEnemyChampStopwatch.DurationInMilliseconds() <= 1000 * 10; //Ten seconds
                                                                                                                  //bool inEarlyGame = getTimeInMilliseconds(mach_absolute_time() - gameCurrentTime) <= 1000*60*8; //Plays safe for first 8 minutes

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
                if (allyMinionsNear)
                {
                    action = Action.FollowAllyMinion;
                }
                //Even better, lets follow an ally champion, see if we can help out
                if (allyChampionsNear)
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
                }

                if (action == Action.AttackEnemyMinion && enemyTowerNear)
                {
                    if (hypot(lowestHealthEnemyMinion->characterCenter.x - nearestEnemyTower->towerCenter.x, lowestHealthEnemyMinion->characterCenter.y - nearestEnemyTower->towerCenter.y) < 430)
                    {
                        action = Action.RunAway;

                        if (allyMinionsNear)
                        {
                            action = Action.FollowAllyMinion;
                        }
                        if (allyChampionsNear)
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
                    }
                }
                else if (selfChamp->health <= 35)
                {
                    action = Action.RunAway;
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
                }

                //int actionSpeed = 0.25;
                lastDecision = action;
                switch (action)
                {
                    case Action.RunAway:
                        {
                            Console.WriteLine("Action: Running Away");
                            if (lastRunAwayClickStopwatch.DurationInMilliseconds() >= 400)
                            {
                                MotorCortex.clickMouseRightAt(Convert.ToInt32(baseLocation.x), Convert.ToInt32(baseLocation.y));
                                lastRunAwayClickStopwatch.Reset();
                            }
                            bool enemyChampWayTooClose = false;
                            if (closestEnemyChampion != null)
                            {
                                enemyChampWayTooClose = (hypot(selfChamp->characterCenter.x - closestEnemyChampion->characterCenter.x, selfChamp->characterCenter.y - closestEnemyChampion->characterCenter.y) < 200);
                            }

                            if ((selfChamp->health < 40 && enemyChampionsNear) || enemyChampWayTooClose)
                            {
                                int enemyX = (closestEnemyChampion->characterCenter.x - selfChamp->characterCenter.x);
                                int enemyY = (closestEnemyChampion->characterCenter.y - selfChamp->characterCenter.y);
                                normalizePoint(ref enemyX, ref enemyY, 300);
                                //enemyX = -enemyX;
                                //enemyY = -enemyY;
                                //Panic
                                if (lastMoveMouseStopwatch.DurationInMilliseconds() >= 1000)
                                {
                                    lastMoveMouseStopwatch.Reset();
                                    MotorCortex.moveMouseTo(enemyX + selfChamp->characterCenter.x, enemyY + selfChamp->characterCenter.y);
                                    castSpell4();
                                    castSpell2();
                                    useTrinket();

                                    Task.Delay(50).ContinueWith(_ =>
                                    {
                                        MotorCortex.moveMouseTo(selfChamp->characterCenter.x - enemyX, selfChamp->characterCenter.y - enemyY);
                                        castSummonerSpell1();
                                        castSummonerSpell2();
                                        useItem1();
                                        useItem2();
                                        useItem3();
                                        useItem4();
                                        useItem5();
                                        useItem6();
                                    });
                                }
                            }
                        }
                        break;
                    case Action.AttackEnemyChampion:
                    case Action.GoHam:
                        {
                            Console.WriteLine("Action: Attacking Enemy Champion");
                            //NSLog(@"\t\tAction: Attacking enemy champion");
                            int x = lowestHealthEnemyChampion->characterCenter.x;
                            int y = lowestHealthEnemyChampion->characterCenter.y;
                            if (lastClickEnemyChampStopwatch.DurationInMilliseconds() >= 500)
                            {
                                lastClickEnemyChampStopwatch.Reset();
                                tapAttackMove(lowestHealthEnemyChampion->characterCenter.x, lowestHealthEnemyChampion->characterCenter.y);
                            }
                            if (lastMoveMouseStopwatch.DurationInMilliseconds() >= 50)
                            {
                                lastMoveMouseStopwatch.Reset();
                                MotorCortex.moveMouseTo(x, y);
                            }
                            castSpell4();
                            castSpell3();
                            castSpell2();
                            castSpell1();
                            castSummonerSpell1();
                            castSummonerSpell2();
                            useItem1();
                            useItem2();
                            useItem3();
                            useItem4();
                            useItem5();
                            useItem6();
                            useTrinket();

                        }
                        break;
                    case Action.AttackEnemyMinion:
                        {
                            Console.WriteLine("Action: Attacking Enemy Minion");
                            //NSLog(@"\t\tAction: Attacking Enemy Minion");
                            if (lastClickEnemyMinionStopwatch.DurationInMilliseconds() >= 100)
                            {
                                lastClickEnemyMinionStopwatch.Reset();
                                tapAttackMove(lowestHealthEnemyMinion->characterCenter.x, lowestHealthEnemyMinion->characterCenter.y);
                            }
                            if (lastMoveMouseStopwatch.DurationInMilliseconds() >= 50)
                            {
                                lastMoveMouseStopwatch.Reset();
                                MotorCortex.moveMouseTo(lowestHealthEnemyMinion->characterCenter.x, lowestHealthEnemyMinion->characterCenter.y);
                            }
                            castSpell1();
                            castSpell3();
                            if (selfChamp->health < 50) { castSpell2(); }
                        }
                        break;
                    case Action.AttackTower:
                        {
                            //NSLog(@"\t\tAction: Attacking Tower");
                            if (lastClickEnemyTowerStopwatch.DurationInMilliseconds() >= 100)
                            {
                                lastClickEnemyTowerStopwatch.Reset();
                                tapAttackMove(nearestEnemyTower->towerCenter.x, nearestEnemyTower->towerCenter.y);
                            }
                            if (lastMoveMouseStopwatch.DurationInMilliseconds() >= 50)
                            {
                                lastMoveMouseStopwatch.Reset();
                                MotorCortex.moveMouseTo(nearestEnemyTower->towerCenter.x, nearestEnemyTower->towerCenter.y);
                            }
                            castSpell1();
                            castSpell3();
                            if (selfChamp->health < 50) { castSpell2(); }
                        }
                        break;
                    case Action.FollowAllyChampion:
                        {
                            Console.WriteLine("Action: Following Ally Champion");
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
                            Console.WriteLine("Action: Following Ally Minion");
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
                            Console.WriteLine("Action: Moving to Mid");
                            //NSLog(@"\t\tAction: Moving to Mid");

                            if (moveToLanePathSwitchStopwatch.DurationInMilliseconds() >= 1000 * 60 * 20)
                            {
                                //Switch to a random lane after 20 min
                                moveToLane = random.Next(3) + 1;
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
                                        x = Convert.ToInt32((map->bottomRight.x - map->topLeft.x) * 0.9 + map->topLeft.x);
                                        y = Convert.ToInt32((map->bottomRight.y - map->topLeft.y) * 0.9 + map->topLeft.y);
                                    }
                                    MotorCortex.clickMouseRightAt(x, y);

                                    Console.WriteLine("Clicked position to move to");
                                }// else {
                                 //    NSLog(@"Map not visible");
                                 //}
                            }
                        }
                        break;
                    case Action.Recall:
                        {
                            Console.WriteLine("Action: Recalling");
                            //NSLog(@"\t\tAction: Recalling");
                            castRecall();
                        }
                        break;
                    case Action.StandStill:
                        {
                            Console.WriteLine("Action: Standing Still");
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
                    MotorCortex.moveMouseTo(selfChamp->characterCenter.x, selfChamp->characterCenter.y);
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
                        MotorCortex.clickMouseRightAt(x, y);
                    }// else {
                     //    NSLog(@"Map not visible");
                     //}
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