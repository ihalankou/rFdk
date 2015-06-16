
#' Gets the quotes history
#' 
#' @param symbol Symbol looked
#' @param startTime Starting time. Use ttGetEpochFromText if you want to take from text a valid date.
#' @param endTime Ending time. Use ttGetEpochFromText if you want to take from text a valid date.
#' @export
ttQuotesLevel2 <- function(symbol,startTime, endTime){
  quotesHistory <- GetQuotePacked(symbol,startTime, endTime)
  
  createTime <- QuotesL2CreatingTime(quotesHistory)
  volumeBid <- QuotesVolumme(quotesHistory)
  volumeAsk <- QuotesVolumeAsk(quotesHistory)
  priceBid <- QuotesPriceBid(quotesHistory)
  priceAsk <- QuotesPriceAsk(quotesHistory)
  
  UnregisterVar(quotesHistory)
  df = data.frame(volumeBid=volumeBid, volumeAsk=volumeAsk, priceBid=priceBid, priceAsk=priceAsk, createTime=createTime)       
}

#' Gets the bars' time
#'
#' @param quotesVar RHost variable that stores quotes array
#' 
QuotesL2CreatingTime <- function(quotesVar) {
  clrCallStatic('RHost.FdkLevel2', 'QuotesCreateTime', quotesVar)
}

#' Gets the bars' ask as requested
#' 
#' @param quotesVar RHost variable that stores quotes array
QuotesVolumeBid <- function(quotesVar) {
  clrCallStatic('RHost.FdkLevel2', 'QuotesVolumeBid', quotesVar)
}


#' Gets the bars' ask as requested
#' 
#' @param quotesVar RHost variable that stores quotes array
QuotesVolumeBid <- function(quotesVar) {
  clrCallStatic('RHost.FdkLevel2', 'QuotesVolumeBid', quotesVar)
}

#' Gets the bars' ask as requested
#' 
#' @param quotesVar RHost variable that stores quotes array
QuotesVolumeAsk <- function(quotesVar) {
  clrCallStatic('RHost.FdkLevel2', 'QuotesVolumeAsk', quotesVar)
}
#' Gets the bars' ask as requested
#' 
#' @param quotesVar RHost variable that stores quotes array
QuotesPriceBid <- function(quotesVar) {
  clrCallStatic('RHost.FdkLevel2', 'QuotesPriceBid', quotesVar)
}

#' Gets the bars' ask as requested
#' 
#' @param quotesVar RHost variable that stores quotes array
QuotesPriceAsk <- function(quotesVar) {
  clrCallStatic('RHost.FdkLevel2', 'QuotesPriceAsk', quotesVar)
}