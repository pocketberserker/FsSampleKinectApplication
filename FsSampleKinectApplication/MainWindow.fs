module FsSampleKinectApplication

open System
open System.Windows
open System.Windows.Controls
open System.Windows.Media.Imaging
open Microsoft.Research.Kinect.Nui

type MainWindow() =

  let window =
    Application.LoadComponent(new System.Uri("/FsSampleKinectApplication;component/MainWindow.xaml", System.UriKind.Relative)) :?> Window

  let nui = Runtime.Kinects.[0]

  let kinectImage = window.FindName "kinectImage" :?> Image

  do window.Loaded
     |> Observable.subscribe begin
         fun _ ->
           nui.Initialize(RuntimeOptions.UseColor)
           nui.VideoStream.Open(ImageStreamType.Video, 2, ImageResolution.Resolution640x480, ImageType.Color)
       end
     |> ignore

  do window.Unloaded
     |> Observable.subscribe (fun _ -> nui.Uninitialize() )
     |> ignore

  do nui.VideoFrameReady
     |> Observable.subscribe begin
         fun args ->
           let image = args.ImageFrame.Image
           let source = BitmapSource.Create(image.Width, image.Height, 96.0, 96.0, Media.PixelFormats.Bgr32, null, image.Bits, image.Width * image.BytesPerPixel)
           kinectImage.Source <- source
       end
     |> ignore

  member this.Window = window

[<STAThread>]
(new MainWindow()).Window |> (new Application()).Run |> ignore