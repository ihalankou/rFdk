installBinaryHttr <- function(fdkRLibPackage){
  basicUrl = "https://github.com/SoftFx/FdkRLib/raw/master/dist/"
  fullUrl = paste(basicUrl, fdkRLibPackage, sep = "")
  localTempFile <- fdkRLibPackage
  r <-GET(fullUrl)
  bin <- content(r, "raw")
  writeBin(bin, localTempFile)
  
  install.packages(localTempFile, repos = NULL, type = "source")
  file.remove(localTempFile)
}

installCranPackage<- function(packageName)
{
  if(!require(packageName, character.only = TRUE))
  {
    install.packages(packageName, repos='http://cran.us.r-project.org', type="win.binary")
    require(packageName, character.only = TRUE)
  }
}

installAllPackages <- function (packageVersion) {
  installBinaryHttr("rClr_0.7-4.zip")
  installBinaryHttr(packageVersion)
}

# Step 1: may require to restart R enviornment. 
# Run it before you install packages. Should be run once
installCranPackage("httr")
installCranPackage("data.table")

if(!require(httr))
{
	install.packages("httr", repos='http://cran.us.r-project.org')
  require(httr)
}
install.packages("data.table", repos='http://cran.us.r-project.org')


# Run it before you install packages. Should be run once
installAllPackages("FdkRLib_1.0.20150817.zip")

library(rClr)
library(FdkRLib)
