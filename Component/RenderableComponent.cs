namespace SolarOdyssey.Component
{
    internal class RenderableComponent
    {
        public string FileName { get; set; }
        public bool FlipHorizontal { get; set; }
        public bool FlipVertical { get; set; }
        public float ScaleX { get; set; }
        public float ScaleY { get; set; }

        public RenderableComponent(string fileName, bool flipHorizontal, bool flipVertical)
        {
            FileName = fileName;
            FlipHorizontal = flipHorizontal;
            FlipVertical = flipVertical;
            ScaleX = 1.0f;
            ScaleY = 1.0f;
        }
    }
}
