namespace Archient.Leap

// Documentation is provided in Signature File (.fsi)

module Leap =
    open System
    open System.Reactive.Linq
    open System.Reactive.Subjects
    
    open Leap

    type ILeapGestures =
        inherit IDisposable

        abstract member Gestures : IObservable<Gesture> with get
        abstract member Swipes : IObservable<SwipeGesture> with get
        abstract member Circles : IObservable<CircleGesture> with get
        abstract member KeyTaps : IObservable<KeyTapGesture> with get
        abstract member ScreenTaps : IObservable<ScreenTapGesture> with get

    type private MyListener() =
        inherit Leap.Listener()

        let gestureSubject = new Subject<Gesture>()
        let swipeSubject = new Subject<SwipeGesture>()
        let circleSubject = new Subject<CircleGesture>()
        let keyTapSubject = new Subject<KeyTapGesture>()
        let screenTapSubject = new Subject<ScreenTapGesture>()

        let enable gestureType (controller:Controller) =
            if not (controller.IsGestureEnabled(gestureType)) then
                controller.EnableGesture(gestureType)
            controller

        member me.Gestures with get() = gestureSubject.AsObservable()
        member me.Swipes with get() = swipeSubject.AsObservable()
        member me.Circles with get() = circleSubject.AsObservable()
        member me.KeyTaps with get() = keyTapSubject.AsObservable()
        member me.ScreenTaps with get() = screenTapSubject.AsObservable()

        override me.OnConnect(controller) =
            controller
            |> enable Gesture.GestureType.TYPE_SWIPE
            |> enable Gesture.GestureType.TYPE_CIRCLE
            |> enable Gesture.GestureType.TYPE_SCREEN_TAP
            |> enable Gesture.GestureType.TYPE_KEY_TAP
            |> ignore

        override me.OnFrame(controller) =
            
            let frame = controller.Frame()

            if frame = null then ()
            else
                let gestures = frame.Gestures()

                if gestures.Count = 0 then ()
                else
                    gestures
                    |> Seq.iter (fun gesture -> 
                        
                        gestureSubject.OnNext(gesture)
                        
                        match gesture.Type with
                        | Gesture.GestureType.TYPE_SWIPE -> 

                            let swipe = new SwipeGesture(gesture)

                            swipeSubject.OnNext(swipe)

                        | Gesture.GestureType.TYPE_CIRCLE -> 

                            let circle = new CircleGesture(gesture)

                            circleSubject.OnNext(circle)

                        | Gesture.GestureType.TYPE_SCREEN_TAP ->

                            let screenTap = new ScreenTapGesture(gesture)

                            screenTapSubject.OnNext(screenTap)

                        | Gesture.GestureType.TYPE_KEY_TAP -> 
                            let keyTap = new KeyTapGesture(gesture)

                            keyTapSubject.OnNext(keyTap)

                        | _ -> ()
                    )

    let subscribe<'t> f (observable:IObservable<'t>) =
        observable.Subscribe(fun item -> f item)

    let listen () =
        let listener = new MyListener()
        let controller = new Leap.Controller()
        
        ignore <| controller.AddListener(listener)

        {
            new ILeapGestures with
                override me.Gestures with get() = listener.Gestures
                override me.Swipes with get() = listener.Swipes
                override me.Circles with get() = listener.Circles
                override me.KeyTaps with get() = listener.KeyTaps
                override me.ScreenTaps with get() = listener.ScreenTaps
                
                override me.Dispose() =
                    ignore <| controller.RemoveListener(listener)

                    listener.Dispose()
                    controller.Dispose()
        }