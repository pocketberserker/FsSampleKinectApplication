module FsSampleKinectApplication

open System
open System.Windows
open Microsoft.Research.Kinect.Nui

type MainWindow() =

  let window =
    Application.LoadComponent(new System.Uri("/FsSampleKinectApplication;component/MainWindow.xaml", System.UriKind.Relative)) :?> Window

  let nui = Runtime.Kinects.[0]

  member this.Window = window

[<STAThread>]
(new MainWindow()).Window |> (new Application()).Run |> ignore