namespace Archient.Leap

/// Utility library for Leap Motion controller
module Leap =
    open System
    
    open Leap

    /// Contract for tracking Leap Motion Gestures
    type ILeapGestures =
        inherit IDisposable

        /// Gets an observable collection of gestures
        abstract member Gestures : IObservable<Gesture> with get
        
        /// Gets an observable collection of swipe gestures
        abstract member Swipes : IObservable<SwipeGesture> with get
        
        /// Gets an observable collection of circle gestures
        abstract member Circles : IObservable<CircleGesture> with get
        
        /// Gets an observable collection of key tap gestures
        abstract member KeyTaps : IObservable<KeyTapGesture> with get
        
        /// Gets an observable collection of screen tap gestures
        abstract member ScreenTaps : IObservable<ScreenTapGesture> with get

    /// Subscribes to an observable collection item
    val subscribe<'t> : subscription:('t->unit) -> observable:IObservable<'t> -> IDisposable
    
    /// Listen to Leap Motion gestures
    val listen : unit -> ILeapGestures