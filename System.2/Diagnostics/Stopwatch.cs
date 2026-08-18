using System;
using Microsoft.Win32;

namespace System.Diagnostics
{
	// Token: 0x02000507 RID: 1287
	[__DynamicallyInvokable]
	public class Stopwatch
	{
		// Token: 0x060030FD RID: 12541 RVA: 0x000DEAB8 File Offset: 0x000DCCB8
		static Stopwatch()
		{
			if (!SafeNativeMethods.QueryPerformanceFrequency(out Stopwatch.Frequency))
			{
				Stopwatch.IsHighResolution = false;
				Stopwatch.Frequency = 10000000L;
				Stopwatch.tickFrequency = 1.0;
				return;
			}
			Stopwatch.IsHighResolution = true;
			Stopwatch.tickFrequency = 10000000.0;
			Stopwatch.tickFrequency /= (double)Stopwatch.Frequency;
		}

		// Token: 0x060030FE RID: 12542 RVA: 0x000DEB18 File Offset: 0x000DCD18
		[__DynamicallyInvokable]
		public Stopwatch()
		{
			this.Reset();
		}

		// Token: 0x060030FF RID: 12543 RVA: 0x000DEB26 File Offset: 0x000DCD26
		[__DynamicallyInvokable]
		public void Start()
		{
			if (!this.isRunning)
			{
				this.startTimeStamp = Stopwatch.GetTimestamp();
				this.isRunning = true;
			}
		}

		// Token: 0x06003100 RID: 12544 RVA: 0x000DEB44 File Offset: 0x000DCD44
		[__DynamicallyInvokable]
		public static Stopwatch StartNew()
		{
			Stopwatch stopwatch = new Stopwatch();
			stopwatch.Start();
			return stopwatch;
		}

		// Token: 0x06003101 RID: 12545 RVA: 0x000DEB60 File Offset: 0x000DCD60
		[__DynamicallyInvokable]
		public void Stop()
		{
			if (this.isRunning)
			{
				long timestamp = Stopwatch.GetTimestamp();
				long num = timestamp - this.startTimeStamp;
				this.elapsed += num;
				this.isRunning = false;
				if (this.elapsed < 0L)
				{
					this.elapsed = 0L;
				}
			}
		}

		// Token: 0x06003102 RID: 12546 RVA: 0x000DEBAB File Offset: 0x000DCDAB
		[__DynamicallyInvokable]
		public void Reset()
		{
			this.elapsed = 0L;
			this.isRunning = false;
			this.startTimeStamp = 0L;
		}

		// Token: 0x06003103 RID: 12547 RVA: 0x000DEBC4 File Offset: 0x000DCDC4
		[__DynamicallyInvokable]
		public void Restart()
		{
			this.elapsed = 0L;
			this.startTimeStamp = Stopwatch.GetTimestamp();
			this.isRunning = true;
		}

		// Token: 0x17000BFF RID: 3071
		// (get) Token: 0x06003104 RID: 12548 RVA: 0x000DEBE0 File Offset: 0x000DCDE0
		[__DynamicallyInvokable]
		public bool IsRunning
		{
			[__DynamicallyInvokable]
			get
			{
				return this.isRunning;
			}
		}

		// Token: 0x17000C00 RID: 3072
		// (get) Token: 0x06003105 RID: 12549 RVA: 0x000DEBE8 File Offset: 0x000DCDE8
		[__DynamicallyInvokable]
		public TimeSpan Elapsed
		{
			[__DynamicallyInvokable]
			get
			{
				return new TimeSpan(this.GetElapsedDateTimeTicks());
			}
		}

		// Token: 0x17000C01 RID: 3073
		// (get) Token: 0x06003106 RID: 12550 RVA: 0x000DEBF5 File Offset: 0x000DCDF5
		[__DynamicallyInvokable]
		public long ElapsedMilliseconds
		{
			[__DynamicallyInvokable]
			get
			{
				return this.GetElapsedDateTimeTicks() / 10000L;
			}
		}

		// Token: 0x17000C02 RID: 3074
		// (get) Token: 0x06003107 RID: 12551 RVA: 0x000DEC04 File Offset: 0x000DCE04
		[__DynamicallyInvokable]
		public long ElapsedTicks
		{
			[__DynamicallyInvokable]
			get
			{
				return this.GetRawElapsedTicks();
			}
		}

		// Token: 0x06003108 RID: 12552 RVA: 0x000DEC0C File Offset: 0x000DCE0C
		[__DynamicallyInvokable]
		public static long GetTimestamp()
		{
			if (Stopwatch.IsHighResolution)
			{
				long result = 0L;
				SafeNativeMethods.QueryPerformanceCounter(out result);
				return result;
			}
			return DateTime.UtcNow.Ticks;
		}

		// Token: 0x06003109 RID: 12553 RVA: 0x000DEC3C File Offset: 0x000DCE3C
		private long GetRawElapsedTicks()
		{
			long num = this.elapsed;
			if (this.isRunning)
			{
				long timestamp = Stopwatch.GetTimestamp();
				long num2 = timestamp - this.startTimeStamp;
				num += num2;
			}
			return num;
		}

		// Token: 0x0600310A RID: 12554 RVA: 0x000DEC6C File Offset: 0x000DCE6C
		private long GetElapsedDateTimeTicks()
		{
			long rawElapsedTicks = this.GetRawElapsedTicks();
			if (Stopwatch.IsHighResolution)
			{
				double num = (double)rawElapsedTicks;
				num *= Stopwatch.tickFrequency;
				return (long)num;
			}
			return rawElapsedTicks;
		}

		// Token: 0x040028E0 RID: 10464
		private const long TicksPerMillisecond = 10000L;

		// Token: 0x040028E1 RID: 10465
		private const long TicksPerSecond = 10000000L;

		// Token: 0x040028E2 RID: 10466
		private long elapsed;

		// Token: 0x040028E3 RID: 10467
		private long startTimeStamp;

		// Token: 0x040028E4 RID: 10468
		private bool isRunning;

		// Token: 0x040028E5 RID: 10469
		[__DynamicallyInvokable]
		public static readonly long Frequency;

		// Token: 0x040028E6 RID: 10470
		[__DynamicallyInvokable]
		public static readonly bool IsHighResolution;

		// Token: 0x040028E7 RID: 10471
		private static readonly double tickFrequency;
	}
}
