namespace LuxImperium.Services
{
    public class ChannelValueValidator
    {
        public byte Value { get; private set; }

        public void SetValue(byte value)
        {
            Value = value;
        }

        public void AddValue(byte delta)
        {
            if (Value + delta > 255)
            {
                Value = 255;
            }
            else
            {
                Value += delta;
            }
        }

        public void SubtractValue(byte delta)
        {
            if (Value - delta < 0)
            {
                Value = 0;
            }
            else
            {
                Value -= delta;
            }
        }
    }
}