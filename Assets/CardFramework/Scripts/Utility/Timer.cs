using System;
using System.Diagnostics;

class Timer : IDisposable
{
	private readonly string _text;
	private Stopwatch _stopwatch;

	public Timer(string text)
	{
		_text = text;
		_stopwatch = Stopwatch.StartNew();
	}

	public void Dispose()
	{
		_stopwatch.Stop();
		UnityEngine.Debug.Log(string.Format("Profiled {0}: {1:0.00}ms", _text, _stopwatch.ElapsedMilliseconds));
	}
}