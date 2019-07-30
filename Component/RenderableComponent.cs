namespace SolarOdyssey.Component
{
    internal class RenderableComponent
    {
        public string FileName { get; set; }
        public bool FlipHorizontal { get; set; }
        public bool FlipVertical { get; set; }

        public RenderableComponent(string fileName, bool flipHorizontal, bool flipVertical)
        {
            FileName = fileName;
            FlipHorizontal = flipHorizontal;
            FlipVertical = flipVertical;
        }
    }
}
