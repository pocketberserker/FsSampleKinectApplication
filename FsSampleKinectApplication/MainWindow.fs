module FsSampleKinectApplication

open System
open System.Windows
open Microsoft.Research.Kinect.Nui

type MainWindow() =

  let window =
    Application.LoadComponent(new System.Uri("/FsSampleKinectApplication;component/MainWindow.xaml", System.UriKind.Relative)) :?> Window

  let nui = Runtime.Kinects.[0]

  do window.Loaded
     |> Observable.subscribe begin
         fun _ ->
           nui.Initialize(RuntimeOptions.UseColor)
           nui.VideoStream.Open(ImageStreamType.Video, 2, ImageResolution.Resolution640x480, ImageType.Color)
       end
     |> ignore

  member this.Window = window

[<STAThread>]
(new MainWindow()).Window |> (new Application()).Run |> ignore