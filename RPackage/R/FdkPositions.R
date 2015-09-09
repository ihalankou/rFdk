#' Gets the account positions
#' 
#' @export
ttTrade.Open <- function(){
  symInfo = GetTradePositions()
  
  agentComission = GetPositionAgentCommission(symInfo)
  buyAmount = GetPositionBuyAmount(symInfo)
  buyPrice = GetPositionBuyPrice(symInfo)
  comission = GetPositionCommission(symInfo)
  profit = GetPositionProfit(symInfo)
  sellAmount = GetPositionSellAmount(symInfo)
  sellPrice = GetPositionSellPrice(symInfo)
  settlementPrice = GetPositionSettlementPrice(symInfo)
  swap = GetPositionSwap(symInfo)
  symbol = GetPositionSymbol(symInfo)
  
  UnregisterVar(symInfo)
  
  data.table(agentComission, buyAmount, buyPrice, comission, profit,
    sellAmount, sellPrice, settlementPrice, swap, symbol
  )
}
#' Get trade history
GetTradePositions <- function() {
  rClr::clrCallStatic('RHost.FdkPosition', 'GetTradePositions')
}

#' Get trade comission
#' @param symInfo RHost variable that stores the array
GetPositionBuyPrice <- function(symInfo) {
  rClr::clrCallStatic('RHost.FdkPosition', 'GetPositionBuyPrice', symInfo)
}

#' Get trade comission
#' @param symInfo RHost variable that stores the array
GetPositionCommission <- function(symInfo) {
  rClr::clrCallStatic('RHost.FdkPosition', 'GetPositionCommission', symInfo)
}

#' Get trade comission
#' @param symInfo RHost variable that stores the array
GetPositionProfit <- function(symInfo) {
  rClr::clrCallStatic('RHost.FdkPosition', 'GetPositionProfit', symInfo)
}

#' Get trade comission
#' @param symInfo RHost variable that stores the array
GetPositionSellAmount <- function(symInfo) {
  rClr::clrCallStatic('RHost.FdkPosition', 'GetPositionSellAmount', symInfo)
}

#' Get trade comission
#' @param symInfo RHost variable that stores the array
GetPositionSellPrice <- function(symInfo) {
  rClr::clrCallStatic('RHost.FdkPosition', 'GetPositionSellPrice', symInfo)
}

#' Get trade comission
#' @param symInfo RHost variable that stores the array
GetPositionSettlementPrice <- function(symInfo) {
  rClr::clrCallStatic('RHost.FdkPosition', 'GetPositionSettlementPrice', symInfo)
}

#' Get trade comission
#' @param symInfo RHost variable that stores the array
GetPositionSwap <- function(symInfo) {
  rClr::clrCallStatic('RHost.FdkPosition', 'GetPositionSwap', symInfo)
}

#' Get trade comission
#' @param symInfo RHost variable that stores the array
GetPositionSymbol <- function(symInfo) {
  rClr::clrCallStatic('RHost.FdkPosition', 'GetPositionSymbol', symInfo)
}
