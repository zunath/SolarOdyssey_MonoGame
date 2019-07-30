namespace SolarOdyssey.Component
{
    internal class HealthComponent
    {
        private int _current;
        private int _maximum;

        public int Current
        {
            get => _current;
            set
            {
                _current = value;
                if (_current < 0) _current = 0;
                if (_current > _maximum) _current = _maximum;
            }
        }

        public int Maximum
        {
            get => _maximum;
            set
            {
                _maximum = value;
                if (_maximum < 1) _maximum = 1;
                if (_current > _maximum) _current = _maximum;
            }
        }

        public HealthComponent(int current, int maximum)
        {
            Maximum = maximum;
            Current = current;
        }
    }
}
