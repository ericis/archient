namespace Archient.Leap

module Gestures =
    open System.Windows

    open Leap

    let onGesture<'t when 't :> Gesture> gesture (win:Window) (f:Window->'t->unit) =
        f win gesture

    let onSwipe (win:Window) (swipe:SwipeGesture) =
        // TODO: Show a swipe visual in the window
        ignore <| MessageBox.Show(sprintf "Swipe: %A" swipe)

    let onCircle (win:Window) (circle:CircleGesture) =
        // TODO: Show a circle visual in the window
        ignore <| MessageBox.Show(sprintf "Circle: %A" circle)

    let onKeyTap (win:Window) (keyTap:KeyTapGesture) =
        // TODO: Show a key tap visual in the window
        ignore <| MessageBox.Show(sprintf "Key Tap: %A" keyTap)

    let onScreenTap (win:Window) (screenTap:ScreenTapGesture) =
        // TODO: Show a screen tap visual in the window
        ignore <| MessageBox.Show(sprintf "Swipe: %A" screenTap)