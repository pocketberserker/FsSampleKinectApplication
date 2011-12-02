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

  let image = new System.Windows.Controls.Image()
  do
    image.Height <- 480.0
    image.Width <- 640.0

  let grid = window.FindName "grid" :?> Grid
  do
    image |> grid.Children.Add |> ignore

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
           let img = args.ImageFrame.Image
           let source = BitmapSource.Create(img.Width, img.Height, 96.0, 96.0, Media.PixelFormats.Bgr32, null, img.Bits, img.Width * img.BytesPerPixel)
           image.Source <- source
       end
     |> ignore

  member this.Window = window

[<STAThread>]
(new MainWindow()).Window |> (new Application()).Run |> ignore