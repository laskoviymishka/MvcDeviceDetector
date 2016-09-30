# MvcDeviceDetector

Adaptive web site should adjust his behaivior according to current device. Adaptive site should also have preference mechanism, with ability to switch between preferences, not matter for device (like github for example).
This could help a lot of developers to build rich and adaptive websites.

Firstly we need to include `MvcDeviceDetector` package via `Install-Package MvcDeviceDetector -Pre`.
Than we should register required service and specify some options:
You could just add device detector:
```
services.AddDeviceDetector();
```

After this configuration new ViewLocationExpanders will be applied and start to search device specific views from prefixes or subfolders. Device would be detected from user agent according to lite device detection mechanism (simillar to _http://projects.spring.io/spring-mobile/_).

You could also add preference switcher mechanism, to enable switch:
```
services.AddDeviceSwitcher<UrlSwitcher>(switcherOptions => { }, deviceOptions => { });
```
If you specify device switcher, you should also map this switcher to some route:
```
route.MapDeviceSwitcher();
```
After that, you will have an ability to override default agent detetection with some preference, by calling URL in format: `/choose/{device}`.
Mechanism provide following extensibility point:

1. Allow developer to change device codes, and aliases
2. Allow developer to use own device implementation, by adding specific device factory
3. Allow developer to introduce new device types expect regular table and mobile
4. Allow developer to switch between Switcher mechanism (by default, it should store preference in cookies, but also should be have ability to use URL switcher)
5. Allow developer to change URL generation rules

For any questions and bug please file an issue.
Any contributions highly appreciate.
