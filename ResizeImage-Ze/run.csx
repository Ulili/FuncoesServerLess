using ImageResizer;
 
public static void Run(
    Stream image,                           // input blob, large size
    Stream imageSmall, Stream imageMedium, Stream imageThumbnail)  // output blobs
{
    var imageBuilder = ImageResizer.ImageBuilder.Current;
    var size = imageDimensionsTable[ImageSize.Small];

    imageBuilder.Build(
        image, imageSmall, 
        new ResizeSettings(size.Item1, size.Item2, FitMode.Max, null), false, true);

    image.Position = 0;
    size = imageDimensionsTable[ImageSize.Medium];

    imageBuilder.Build(
        image, imageMedium,
        new ResizeSettings(size.Item1, size.Item2, FitMode.Max, null), false, true);

    image.Position = 0;
    imageBuilder.Build(
        image, imageThumbnail,
        new ResizeSettings("width=100&height=100&crop=auto"), false, true);
}

public enum ImageSize
{
    ExtraSmall, Small, Medium
}

private static Dictionary<ImageSize, Tuple<int, int>> imageDimensionsTable = new Dictionary<ImageSize, Tuple<int, int>>()
{
    { ImageSize.ExtraSmall, Tuple.Create(320, 200) },
    { ImageSize.Small,      Tuple.Create(640, 400) },
    { ImageSize.Medium,     Tuple.Create(800, 600) }
};
