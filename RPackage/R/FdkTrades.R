#' Gets the account trades
#' 
#' @param tradeSide '' (unfiltered) or 'Buy' or 'Sell'
#' @param tradeType '' (unfiltered) or 'Market' 'Position' 'Limit' 'Stop'
#' @export
ttTrades <- function(tradeSide = '', tradeType = ''){
  symInfo = GetTradeRecords(tradeSide, tradeType)
  
  agentComission = GetTradeAgentCommission(symInfo)
  tradeClientOrderId = GetTradeClientOrderId(symInfo)
  tradeComment = GetTradeComment(symInfo)
  created = GetTradeCreated(symInfo)
  expiration = GetTradeExpiration(symInfo)
  initialVolume = GetTradeInitialVolume(symInfo)
  isLimitOrder = GetTradeIsLimitOrder(symInfo)
  isPendingOrder = GetTradeIsPendingOrder(symInfo)
  isPosition = GetTradeIsPosition(symInfo)
  isStopOrder = GetTradeIsStopOrder(symInfo)
  modified = GetTradeModified(symInfo)
  orderId = GetTradeOrderId(symInfo)
  price = GetTradePrice(symInfo)
  profit = GetTradeProfit(symInfo)
  side = GetTradeSide(symInfo)
  stopLoss = GetTradeStopLoss(symInfo)
  swap = GetTradeSwap(symInfo)
  takeProfit = GetTradeTakeProfit(symInfo)
  type = GetTradeType(symInfo)
  volume = GetTradeVolume(symInfo)
  
  UnregisterVar(symInfo)
  
  data.frame(agentComission, tradeClientOrderId, tradeComment, created,
	 expiration, initialVolume, isLimitOrder, isPendingOrder,
   isPosition, isStopOrder, modified, orderId, price, profit, 
   side, stopLoss, swap, takeProfit, type, volume)
}
#' Get symbol field
GetTradeRecords <- function(tradeSide, tradeType) {
  clrCallStatic('RHost.FdkTrade', 'GetTradeRecords', tradeSide, tradeType)
}

#' Get trade comission
GetTradeAgentCommission <- function(symInfo) {
  clrCallStatic('RHost.FdkTrade', 'GetTradeAgentCommission', symInfo)
}

#' Get trade trade client order id
GetTradeClientOrderId <- function(symInfo) {
  clrCallStatic('RHost.FdkTrade', 'GetTradeClientOrderId', symInfo)
}

#' Get trade comment
GetTradeComment <- function(symInfo) {
  clrCallStatic('RHost.FdkTrade', 'GetTradeComment', symInfo)
}

#' Get trade create time
GetTradeCreated <- function(symInfo) {
  clrCallStatic('RHost.FdkTrade', 'GetTradeCreated', symInfo)
}

#' Get trade expiratiobn date
GetTradeExpiration <- function(symInfo) {
  clrCallStatic('RHost.FdkTrade', 'GetTradeExpiration', symInfo)
}
#' Get trade initial volume
GetTradeInitialVolume <- function(symInfo) {
  clrCallStatic('RHost.FdkTrade', 'GetTradeInitialVolume', symInfo)
}
#' Get trade comission
GetTradeIsLimitOrder <- function(symInfo) {
  clrCallStatic('RHost.FdkTrade', 'GetTradeIsLimitOrder', symInfo)
}

#' Get trade comission
GetTradeIsPendingOrder <- function(symInfo) {
  clrCallStatic('RHost.FdkTrade', 'GetTradeIsPendingOrder', symInfo)
}

#' Get trade comission
GetTradeIsPosition <- function(symInfo) {
  clrCallStatic('RHost.FdkTrade', 'GetTradeIsPosition', symInfo)
}
#' Get trade comission
GetTradeIsStopOrder <- function(symInfo) {
  clrCallStatic('RHost.FdkTrade', 'GetTradeIsStopOrder', symInfo)
}

#' Get trade comission
GetTradeModified <- function(symInfo) {
  clrCallStatic('RHost.FdkTrade', 'GetTradeModified', symInfo)
}
#' Get trade comission
GetTradeOrderId <- function(symInfo) {
  clrCallStatic('RHost.FdkTrade', 'GetTradeOrderId', symInfo)
}
#' Get trade comission
GetTradePrice <- function(symInfo) {
  clrCallStatic('RHost.FdkTrade', 'GetTradePrice', symInfo)
}
#' Get trade comission
GetTradeProfit <- function(symInfo) {
  clrCallStatic('RHost.FdkTrade', 'GetTradeProfit', symInfo)
}
#' Get trade comission
GetTradeSide <- function(symInfo) {
  clrCallStatic('RHost.FdkTrade', 'GetTradeSide', symInfo)
}
#' Get trade comission
GetTradeStopLoss <- function(symInfo) {
  clrCallStatic('RHost.FdkTrade', 'GetTradeStopLoss', symInfo)
}
#' Get trade comission
GetTradeSwap <- function(symInfo) {
  clrCallStatic('RHost.FdkTrade', 'GetTradeSwap', symInfo)
}
#' Get trade comission
GetTradeAgentCommission <- function(symInfo) {
  clrCallStatic('RHost.FdkTrade', 'GetTradeAgentCommission', symInfo)
}

#' Get trade comission
GetTradeTakeProfit <- function(symInfo) {
  clrCallStatic('RHost.FdkTrade', 'GetTradeTakeProfit', symInfo)
}
#' Get trade comission
GetTradeType <- function(symInfo) {
  clrCallStatic('RHost.FdkTrade', 'GetTradeType', symInfo)
}
#' Get trade comission
GetTradeVolume <- function(symInfo) {
  clrCallStatic('RHost.FdkTrade', 'GetTradeVolume', symInfo)
}
#' Get trade comission
GetTradeAgentCommission <- function(symInfo) {
  clrCallStatic('RHost.FdkTrade', 'GetTradeAgentCommission', symInfo)
}