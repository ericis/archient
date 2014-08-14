namespace Archient.Leap

module Program =
    
    open System
    open System.Windows

    open System.Reactive
    open System.Reactive.Linq
    open System.Reactive.Subjects

    let run (argv:string[]) =
        
        // create WPF application
        let app = new Application()

        // create WPF window
        let win = new Window(Title = "Archient Leap Motion Example", WindowState = WindowState.Maximized)
        
        // listen to Leap Motion controller
        use listener = Leap.listen()

        // gesture closure of window scope
        let onGesture f gesture =
            Gestures.onGesture gesture win f

        // subscribe to gestures:
        //  - Swipes
        //  - Circles
        //  - Key Taps
        //  - Screen Taps
        // *Note: variables are scoped to this function.
        //        Although 'unused', storing them this way 
        //        means they'll be auto-disposed of when 
        //        the app exits without having to send them 
        //        anywhere.
        use swipesSubscription = listener.Swipes |> Leap.subscribe (onGesture Gestures.onSwipe)
        use circlesSubscription = listener.Circles |> Leap.subscribe (onGesture Gestures.onCircle)
        use keyTapsSubscription = listener.KeyTaps |> Leap.subscribe (onGesture Gestures.onKeyTap)
        use screenTapsSubscription = listener.ScreenTaps |> Leap.subscribe (onGesture Gestures.onScreenTap)
        
        // run the WPF application
        let returnCode = app.Run(win)

        returnCode