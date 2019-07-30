namespace SolarOdyssey.Component
{
    internal class RenderableComponent
    {
        public string FileName { get; set; }
        public bool FlipHorizontal { get; set; }
        public bool FlipVertical { get; set; }
        public float ScaleX { get; set; }
        public float ScaleY { get; set; }

        private float _opacity;

        public float Opacity
        {
            get => _opacity;
            set
            {
                _opacity = value;
                if (_opacity > 1.0f) _opacity = 1.0f;
                else if (_opacity < 0.0f) _opacity = 0.0f;
            }
        }

        public RenderableComponent(string fileName, bool flipHorizontal, bool flipVertical)
        {
            FileName = fileName;
            FlipHorizontal = flipHorizontal;
            FlipVertical = flipVertical;
            ScaleX = 1.0f;
            ScaleY = 1.0f;
            Opacity = 1.0f;
        }
    }
}
