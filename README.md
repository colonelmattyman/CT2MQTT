# CT2MQTT
Core Temp to MQTT

Note: The code is messy. I'll probably spend some time in the future tidying it up and commenting it properly.  I wrote this over the space of a few days.

Core Temp to MQTT is an app developed to address the issue of publishing the CPU temperature and load stats to MQTT so that they can be displayed in other applications like Homeassistant (HASS.io).

Prerequisites:
- CoreTemp will need to be running on your machine for this app to function. You can download and install CoreTemp from here 
  (https://www.alcpu.com/CoreTemp/).  
- You will also need to tick "Enable global shared memory (SNMP)" on the Advanced tab under "Options" - "Settings" in Core Temp.
- .NET Framework  4.6.1

Why did I develop this? 
I personally run HomeAssistant on a Windows NUC in a Virtual Box VM and I was having issues with weird restarts and other issues with HA.
It turned out that the fan had died on the NUC which was causing CPU throttling. Additionly the WiFi card in the NUC gets extremely hot when in use and with the lack of airflow it was also causing the M2 SSD (which is directly above it) to operate in hotter than normal temperatures and causing it to behave erratically.

How?
The app uses two external libraries, one provided by the author of CoreTemp (https://www.alcpu.com/CoreTemp/developers.html) and the other provided by M2MQTT (http://m2mqtt.wordpress.com).
M2MQTT essentially just bridges the gap between these two libraries.

Installation.
Run the included installer and then open the app from the Windows menu.
You will need to fill out a few fields.
Address                     - this is the address of your MQTT server -  the second text box is for the port number.
Username                    - Username to access the MQTT server.
Password                    - Password to access the MQTT server.
Poll Core Temp (ms)         - This is the amount of milliseconds CT2MQTT will wait to Poll Core Temp for new data.
MQTT Current (ms)           - This is the amount of milliseconds that CT2MQTT will wait before publishing the data to the MQTT server.
MQTT Average (ms)           - CT2MQTT also calculates a 10 point moving average for each core's Temperate and Load.   This is the amount                               of milliseconds that CT2MQTT waits before publishing that data to the MQTT server.
Run app as windows start up - Ticking this will run the app when Windows starts.
Minimise on start           - Ticking this will minimise the app to the system tray when it starts.
Stat polling on start up    - Ticking this will cause CT2MQTT to start polling Core Temp and publishing the data to the MQTT 
                              server on startup.
Start Polling               - CT2MQTT will start polling Core Temp and publishing the data to the MQTT server.
Stop Polling                - CT2MQTT will stop polling and publishing and will unlock all fields for editing.
Save and Cancel             - No explanation needed. 

Show YAML Config            - For HomeAssistant users.  This will open a text box which will allow the user to cut and paste all of the 
                              sensors that CT2MQTT will publish to MQTT into a format that can be pasted straigt into configuration.yaml.

MQTT Topics.
The followign sensor topics are generated:
 - sensor/hostname/cpu/hostname
 - sensor/hostname/cpu/name
 - sensor/hostname/cpu/count
 - sensor/hostname/cpu/core/count
 - sensor/hostname/cpu/fsbspeed
 - sensor/hostname/cpu/multiplier
 - sensor/hostname/cpu/speed
 - sensor/hostname/cpu/speed/text
 - sensor/hostname/cpu/voltage
 - sensor/hostname/cpu/voltage/text
 - sensor/hostname/cpu/temptype
 - sensor/hostname/cpuX/coreX/temperature (1 for each core)
 - sensor/hostname/allcores/temperatureaverage
 - sensor/hostname/cpuX/coreX/load (1 for each core)
 - sensor/hostname/allcores/loadaverage 
 - sensor/hostname/cpuX/coreX/temperature/average (1 for each core)
 - sensor/hostname/cpuX/coreX/load/average (1 for each core)

HomeAssistant Sensors.

Basic format is :

sensor:
  - platform: mqtt
    state_topic: "sensor/<hostname>/cpuX/coreY/temperature"
    name: hostname_cpuX_coreY_temperature
    unit_of_measurement: '°C'
