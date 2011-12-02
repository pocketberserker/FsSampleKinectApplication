module FsSampleKinectApplication

open System
open System.Windows

type MainWindow() =

  let window =
    Application.LoadComponent(new System.Uri("/FsSampleKinectApplication;component/MainWindow.xaml", System.UriKind.Relative)) :?> Window

  member this.Window = window

[<STAThread>]
(new MainWindow()).Window |> (new Application()).Run |> ignore