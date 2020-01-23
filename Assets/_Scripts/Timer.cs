using System;

[Serializable]
public class Timer
{
    private float _currentTime;

    public float Interval { get; set; }
    public bool AutoReset { get; set; }
    public float ElapsedTime 
    { 
        get => _currentTime;
        private set 
        {
            if (value > Interval &&AutoReset)
            {
                _currentTime -= Interval;
            }
            else
            {
                _currentTime = value;
            }
        }
    }
    public bool Elapsed { get => ElapsedTime >= Interval; }

    public Timer(float waitingTime, bool autoReset)
    {
        Interval = waitingTime;
        AutoReset = autoReset;
    }

    public void Tick(float deltaTime)
    {
        ElapsedTime += deltaTime;
    }

    public void Reset()
    {
        ElapsedTime = 0;
    }
}