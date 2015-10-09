
#' Gets the bars as requested
#' 
#' @param symbol Symbol looked
#' @param priceTypeStr Bid or Ask or BidAsk
#' @param barPeriodStr Values like: S1, S10, M1, M5, M15, M30, H1, H4, D1, W1, MN1 (default 'M1')
#' @param startTime Start of the time intervals  
#' @param endTime End of time interval. If startTime is not set, the bar count is taken from barCount variable
#' @param barCount Number of items of startTime is not set 
#' @export
ttFeed.BarHistory <- function(symbol, 
     priceTypeStr="Bid", barPeriodStr = "M1", 
     startTime= as.POSIXct(0, origin = "1970-01-02"), endTime = Sys.time(),
     barCount = 0
     ){
  symbolBars <- ComputeBarsRange(symbol, priceTypeStr, barPeriodStr, startTime, endTime, barCount)
  
  high <- BarHighs(symbolBars)
  low <- BarLows(symbolBars)
  open <- BarOpens(symbolBars)
  close <- BarCloses(symbolBars)
  volume <- BarVolumes(symbolBars)
  from <- BarFroms(symbolBars)
  to <- BarTos(symbolBars)
  UnregisterVar(symbolBars)
  
  
  resultData = data.table(high, low, open, close, volume, from, to)
  
  if(priceTypeStr == "BidAsk")
  {
    result = data.frame(split(resultData, sample(1:2)))
    setnames(result, "X1.high", "bidHigh")
    setnames(result, "X1.low", "bidLow")
    setnames(result, "X1.open", "bidOpen")
    setnames(result, "X1.close", "bidClose")
    setnames(result, "X1.from", "bidFrom")
    setnames(result, "X1.to", "bidTo")
    
    setnames(result, "X2.high", "askHigh")
    setnames(result, "X2.low", "askLow")
    setnames(result, "X2.open", "askOpen")
    setnames(result, "X2.close", "askClose")
    setnames(result, "X2.from", "askFrom")
    setnames(result, "X2.to", "askTo")
    
  }
  else
    result = resultData
  
  result
}


#' Gets the bars as requested
#' 
#' @param symbol Symbol looked
#' @param priceTypeStr Ask
#' @param barPeriodStr Values like: M1, H1
#' @param startTime Start of the time range
#' @param endTime End of the time range
#' @param barCount Items used
ComputeBarsRange <- function(symbol, 
      priceTypeStr, barPeriodStr, startTime, endTime, barCount) {
  rClr::clrCallStatic('RHost.FdkBars', 'ComputeBarsRangeTime', symbol, priceTypeStr, barPeriodStr, startTime, endTime, barCount)
}

#' Gets the bars' high  as requested
#' 
#' @param barsVar RHost variable that stores bar array
BarHighs <- function(barsVar) {
  rClr::clrCallStatic('RHost.FdkBars', 'BarHighs', barsVar)
}
#' Gets the bars' low as requested
#' 
#' @param barsVar RHost variable that stores bar array
BarLows <- function(barsVar) {
  rClr::clrCallStatic('RHost.FdkBars', 'BarLows', barsVar)
}
#' Gets the bars' open as requested
#' 
#' @param barsVar RHost variable that stores bar array
BarOpens <- function(barsVar) {
  rClr::clrCallStatic('RHost.FdkBars', 'BarOpens', barsVar)
}

#' Gets the bars' closed as requested
#' 
#' @param barsVar RHost variable that stores bar array
BarCloses <- function(barsVar) {
  rClr::clrCallStatic('RHost.FdkBars', 'BarCloses', barsVar)
}

#' Gets the bars' volume as requested
#' 
#' @param barsVar RHost variable that stores bar array
BarVolumes <- function(barsVar) {
  rClr::clrCallStatic('RHost.FdkBars', 'BarVolumes', barsVar)
}

#' Gets the bars' volume as requested
#' 
#' @param barsVar RHost variable that stores bar array
BarFroms <- function(barsVar) {
  rClr::clrCallStatic('RHost.FdkBars', 'BarFroms', barsVar)
}

#' Gets the bars' volume as requested
#' 
#' @param barsVar RHost variable that stores bar array
BarTos <- function(barsVar) {
  rClr::clrCallStatic('RHost.FdkBars', 'BarTos', barsVar)
}
