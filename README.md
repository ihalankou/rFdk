# FdkRLib
Added the SoftFX R wrapper package over FDK (Financial Development Kit)

# Prerequisites
If you see this error: "You are probably missing the Visual C++ Redistributable for Visual Studio 2013", then please download it from here:
https://www.microsoft.com/en-us/download/details.aspx?id=40784

# How to install it?
```
# Run it before you install packages. Should be run once
installBinaryHttr <- function(fdkRLibPackage){
  basicUrl = "https://github.com/SoftFx/FdkRLib/raw/master/dist/"
  fullUrl = paste(basicUrl, fdkRLibPackage, sep = "")
  download.file(fullUrl,destfile = fdkRLibPackage, method = "libcurl")
  
  install.packages(fdkRLibPackage, repos = NULL, type = "source", dependencies = TRUE)
  file.remove(fdkRLibPackage)
}
installBinaryHttr("rClr_0.7-4.zip")
installBinaryHttr("rFdk_1.0.20151204.zip")
```

# How to test it?
You have sample code inside examples/sample_bars.r with various snippets of code. 

A simple code sample code is the following:
```

ttConnect()

#Get configuration information of your account
head(ttConf.Symbol())
head(ttConf.Currency())

#Quotes in the last 5 minutes
now <-as.POSIXct(Sys.time(), tz="GMT")
# 300 seconds from present
prevNow <-as.POSIXct(now-(5*60))
ttFeed.TickBestHistory("EURUSD", startTime = prevNow, endTime=now)
```
Follow this link with expanded example and output:

Configuration:
http://rpubs.com/ciplogic/107672

Feed History:
http://rpubs.com/ciplogic/107673

Trades:
http://rpubs.com/ciplogic/107674

Real-time feed sample
http://rpubs.com/ciplogic/130650
