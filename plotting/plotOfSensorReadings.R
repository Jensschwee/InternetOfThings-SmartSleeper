library("rjson")
library("stringr")
library("dplyr")
library("ggplot2")

devices = c("17A7C1", "1CC3D0")

json_file <- "sensorreadings.json"
json_data <- fromJSON(file=json_file)

timeFrom <- as.POSIXlt("2017-05-13 08:00+00:00")
timeTo <- as.POSIXlt("2017-05-13 12:47+00:00")
timeWindow <- as.POSIXlt("2017-05-13 11:05+00:00")
timeWindowClosedLux <- as.POSIXlt("2017-05-13 11:35+00:00")



dataToPlot = data.frame()

for(index in 1:length(json_data))
{
  if(json_data[[index]]$deviceID == "17A7C1" || json_data[[index]]$deviceID == "1CC3D0")
  {
   
    time = as.POSIXlt(str_replace(json_data[[index]]$sensor_time, "T", ""))
    if(timeFrom <= time & time <= timeTo)
    {
      time = time + 2*60*60
      dataToPlot = rbind(dataToPlot, data.frame(json_data[[index]]$deviceID, time, json_data[[index]]$humidity, json_data[[index]]$temperature,json_data[[index]]$lux, json_data[[index]]$pressur))
    }
  }
}

names(dataToPlot)[names(dataToPlot) == "json_data..index...deviceID"] = "Device ID"
vlines <- data.frame(value = c(as.double(timeWindowClosedLux), as.double(timeWindow)),Mean = c("Window Opend", "Window Closed"))

ggplot(data=dataToPlot, aes(x=dataToPlot$time, y=dataToPlot$json_data..index...lux, group= dataToPlot$`Device ID`, color = `Device ID`)) +
  geom_line() +
  geom_point() +
  ylab("Lux in lx") +
  xlab("") + 
  theme(legend.title = element_text(size=16, face="bold")) +  
  geom_vline(mapping =aes(xintercept=as.double(timeWindow), linetype="Window Open"),size=1) +
  geom_vline(mapping =aes(xintercept=as.double(timeWindowClosedLux), linetype="Window Closed and light On") ,size=1) +
  scale_linetype_manual(name ="Events", values = c("Window Open" = "dotted", "Window Closed and light On" = "dashed"))

ggplot(data=dataToPlot, aes(x=dataToPlot$time, y=dataToPlot$json_data..index...humidity, group= dataToPlot$`Device ID`, color = `Device ID`)) +
  geom_line() +
  geom_point() +
  ylab("Humidity in %") +
  xlab("") + 
  theme(legend.title = element_text(size=16, face="bold"))+
  geom_vline(mapping =aes(xintercept=as.double(timeWindow), linetype="Window Open"),size=1) +
  geom_vline(mapping =aes(xintercept=as.double(timeWindowClosedLux), linetype="Window Closed and light On") ,size=1) +
  scale_linetype_manual(name ="Events", values = c("Window Open" = "dotted", "Window Closed and light On" = "dashed"))


ggplot(data=dataToPlot, aes(x=dataToPlot$time, y=dataToPlot$json_data..index...temperature, group= dataToPlot$`Device ID`, color = `Device ID`)) +
  geom_line() +
  geom_point() +
  ylab("Temperature in censius") +
  xlab("") + 
  theme(legend.title = element_text(size=16, face="bold"))+
  geom_vline(mapping =aes(xintercept=as.double(timeWindow), linetype="Window Open"),size=1) +
  geom_vline(mapping =aes(xintercept=as.double(timeWindowClosedLux), linetype="Window Closed and light On") ,size=1) +
  scale_linetype_manual(name ="Events", values = c("Window Open" = "dotted", "Window Closed and light On" = "dashed"))


ggplot(data=dataToPlot, aes(x=dataToPlot$time, y=dataToPlot$json_data..index...pressur, group= dataToPlot$`Device ID`, color = `Device ID`)) +
  geom_line() +
  geom_point() +
  ylab("Pressure  in hPa") +
  xlab("") + 
  theme(legend.title = element_text(size=16, face="bold"))+
  geom_vline(mapping =aes(xintercept=as.double(timeWindow), linetype="Window Open"),size=1) +
  geom_vline(mapping =aes(xintercept=as.double(timeWindowClosedLux), linetype="Window Closed and light On") ,size=1) +
  scale_linetype_manual(name ="Events", values = c("Window Open" = "dotted", "Window Closed and light On" = "dashed"))




