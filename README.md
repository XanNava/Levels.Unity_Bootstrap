# Levels.Unity_Bootstrap
A better bootstrap for unity scenes.

---------------------------------------
Just create a scene called "Bootstrap" in
your project, and add it to your build settings.
It should be position 0 in the level order.
Then simply have the Bootstrap scrip in your project.
This script looks at your build settings for the location
of the "Bootstrap" scene, and makes sure it is the first
scene loaded when entering playmode.

Note: Logs for the bootstrap SubsystemReg are put into a
string builder so you can log them with a custom logging
framework after (Like Unity.Logging/Serilog).
