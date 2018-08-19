# Soulstone 2

A WebAPI, two winforms and a WPF application from 2014

In `original` branch you will find the original source code for this application. In `master` an upgraded, refactored version.

## Description 

Soulstone 2 is the second version of the [Silverlight](https://en.wikipedia.org/wiki/Microsoft_Silverlight) application [written in 2009](https://github.com/mamcer/soulstone). In fact is a complete rewrite, they basically only share the name.

The main goal and functionality in this case is to manage different hosts (endpoints) which contains music. You can remotely contact them, see all the music they offer, create playlists and remotely control the playback of your music.
You can then have a headless endpoint (it just need some speakers attached) in your living room, another in your room, etc and from a single point control the music playback on all of them in real time.

Some technical details:

- The data layer is based in Entity Framework Model first. Should be one of the latest projects before I broadly adopt code first.
- Initially hosted in Visual Studio Online using the TFVC format. There is a get.cmd file which basically contact the repository using tf.exe and get the latest version (you need to save your credentials in the Windows credential manager)
- It uses a SHA2 hash calculator to detect same files with different file names. Also It uses Levenshtein to compare song titles similarities.
- At that moment FXCop was still a thing, at least for me. It was integrated to Visual Studo in 2012, but at that time I continued using it as a separated tool. There is a fxcop.cmd to run it from the command line.
- I use to have a media center Windows 7 based computer. It also hosted a SonarQube instance. Continuous Integration was implemented in a Jenkins instance in the same media center or in Visual Studio online (As an early TF Online adopter, I use to have unlimited CI minutes which at some point were reduced to just 60 monthly free build minutes). But because of the opencover_to_ncover.xslt in the source files to convert from opencover (not supported) to ncover (supported) format to see the code coverage report I think this project was still using Visual Studio online for the builds.

## Screenshot

![screenshot](https://raw.githubusercontent.com/mamcer/soulstone-2/master/doc/screenshot-01.png)

![screenshot](https://raw.githubusercontent.com/mamcer/soulstone-2/master/doc/screenshot-02.png)

![screenshot](https://raw.githubusercontent.com/mamcer/soulstone-2/master/doc/screenshot-03.png)

## Technologies

- Visual Studio 2013
- .NET Framework 4.5
- Entity Framework 6.1
- SignalR 2.0
- WebAPI 5.1
