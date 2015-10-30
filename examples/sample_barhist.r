ttConnect()
startTime <- as.POSIXct(0, origin=ISOdatetime(2015,10,14,8,00,16))
endTime <- as.POSIXct(0, origin=ISOdatetime(2015,10,14,18,00,16))
st1 <- as.POSIXct(startTime)
et1 <- as.POSIXct(endTime)
now <-as.POSIXct(Sys.time())
y = ttFeed.BarHistory("#AUS200","BidAsk","M1", st1, et1, barCount = 1000)

View(ttFeed.BarHistory(symbol = "EURUSD", barPeriodStr = "S1", priceTypeStr ="BidAsk", startTime = now-5, endTime = now))
View(ttFeed.BarHistory(symbol = "EURUSD", barPeriodStr = "S1", priceTypeStr ="Bid", startTime = now-5, endTime = now))


ttConnect("tp.st.soft-fx.eu", "100163","123qwe!")
ttConnect()
startTime <- as.POSIXct(0, origin=ISOdatetime(2015,10,14,8,00,16))
endTime <- as.POSIXct(0, origin=ISOdatetime(2015,10,14,18,00,16))
st1 <- as.POSIXct(startTime)
et1 <- as.POSIXct(endTime)
y = ttFeed.BarHistory("#AUS200","BidAsk","M1", st1, et1, barCount = 1000)

x <- 1:12
# a random permutation
sample(x)
y <- 0:1
sample(y)
