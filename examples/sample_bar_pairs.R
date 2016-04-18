endTime <- as.POSIXlt(Sys.time())
startTime <- as.POSIXct(0, origin=ISOdatetime(2015,3,2,11,16,16))
st1 <- as.POSIXct(startTime)
et1 <- as.POSIXct(endTime)

barPairs = ttFeed.BarHistory(symbol = "EURUSD", barPeriodStr = "M1", startTime = st1)

barPairsRange = ttFeed.BarHistory(symbol = "EURUSD", barPeriodStr = "M1", startTime= st1, endTime = et1)
