#Log4NetToSignalR
**Log4NetToSignalR** is a Log4Net appender for delivering log4Net notification via SignalR framework in form of notification. It supports both PersistentConnection and Hub to deliver notification. 

## Current Version
Alpha.


## Code
Code can be found at https://github.com/Hdesai/Log4NetToSignalR

## Quick start

###4 Easy Steps


1. Put Log4Net.Appender.SignalR in the same folder as your App.Config/ Web.Config file. (or just use nuget package)

2. Change Log4Net Configuration.
  e.g. <log4net>
    <!--
    +====================================
    | Appenders
    +==================================== 
    -->
    <appender name="SignalRAppender" type="Log4Net.Appender.SignalR.SignalRAppender,Log4Net.Appender.SignalR">
      <Uri value="http://localhost/SignalR" />
      <CallViaPersistentConnection value="false" />
      <HubName value="messenger" />
      <GroupName value="sourcing" />
      <MethodToCall value="broadCastMessage" />
      <layout type="log4net.Layout.PatternLayout">
        <conversionPattern value="%date [%thread] %-5level %logger [%property{NDC}] - %message%newline" />
      </layout>
    </appender>

    <logger name="ConsoleApp">
      <level value="ALL" />
      <appender-ref ref="SignalRAppender" />
    </logger>

  </log4net>

3. Deploy the SignalR Server (see similiar to NLog.Targets.SignalR.AspNetServer on my other project https://github.com/hdesai/NLogToSignalR) on local IIS.

4. Browse the website after deployment and start logging!
        

## Authors

**Himanshu Desai**

+ http://twitter.com/h_desai

## Credits
Special thanks to three open source projects.
Log4Net,SignalR and toastr


## Copyright

Copyright © 2012 [Himanshu Desai](http://twitter.com/h_desai) 

## License 

Log4NetToSignalR is under MIT license - http://www.opensource.org/licenses/mit-license.php