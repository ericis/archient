
Archient Library for Leap Motion
=======================================

Important *MANUAL STEP*

Leap.dll and LeapCSharp.dll are not added as references to your project!

They need to be copied to the output directory post-build.

VS Post-Build Examples:

  Project properties -> build events...

    copy "$(ProjectDir)..\..\packages\LeapMotion.2.0.4.0\Leap.dll" "$(TargetDir)Leap.dll"
    copy "$(ProjectDir)..\..\packages\LeapMotion.2.0.4.0\LeapCSharp.dll" "$(TargetDir)LeapCSharp.dll"
