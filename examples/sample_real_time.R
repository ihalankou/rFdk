ttConnect()

sym = ttFeed.Subscribe("EURUSD", 2)
ttFeed.GetLevel2(sym)
Sys.sleep(3)

ttFeed.GetLevel2(sym)
Sys.sleep(3)

ttFeed.GetLevel2(sym)
Sys.sleep(3)

ttFeed.Unsubscribe(sym)
