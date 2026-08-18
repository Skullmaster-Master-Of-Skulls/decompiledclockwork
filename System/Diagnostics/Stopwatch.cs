using System;
using Microsoft.Win32;

namespace System.Diagnostics
{
	// Token: 0x0200079A RID: 1946
	public class Stopwatch
	{
		// Token: 0x06003C17 RID: 15383 RVA: 0x00100F6C File Offset: 0x000FFF6C
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

		// Token: 0x06003C18 RID: 15384 RVA: 0x00100FCC File Offset: 0x000FFFCC
		public Stopwatch()
		{
			this.Reset();
		}

		// Token: 0x06003C19 RID: 15385 RVA: 0x00100FDA File Offset: 0x000FFFDA
		public void Start()
		{
			if (!this.isRunning)
			{
				this.startTimeStamp = Stopwatch.GetTimestamp();
				this.isRunning = true;
			}
		}

		// Token: 0x06003C1A RID: 15386 RVA: 0x00100FF8 File Offset: 0x000FFFF8
		public static Stopwatch StartNew()
		{
			Stopwatch stopwatch = new Stopwatch();
			stopwatch.Start();
			return stopwatch;
		}

		// Token: 0x06003C1B RID: 15387 RVA: 0x00101014 File Offset: 0x00100014
		public void Stop()
		{
			if (this.isRunning)
			{
				long timestamp = Stopwatch.GetTimestamp();
				long num = timestamp - this.startTimeStamp;
				this.elapsed += num;
				this.isRunning = false;
			}
		}

		// Token: 0x06003C1C RID: 15388 RVA: 0x0010104D File Offset: 0x0010004D
		public void Reset()
		{
			this.elapsed = 0L;
			this.isRunning = false;
			this.startTimeStamp = 0L;
		}

		// Token: 0x17000E1E RID: 3614
		// (get) Token: 0x06003C1D RID: 15389 RVA: 0x00101066 File Offset: 0x00100066
		public bool IsRunning
		{
			get
			{
				return this.isRunning;
			}
		}

		// Token: 0x17000E1F RID: 3615
		// (get) Token: 0x06003C1E RID: 15390 RVA: 0x0010106E File Offset: 0x0010006E
		public TimeSpan Elapsed
		{
			get
			{
				return new TimeSpan(this.GetElapsedDateTimeTicks());
			}
		}

		// Token: 0x17000E20 RID: 3616
		// (get) Token: 0x06003C1F RID: 15391 RVA: 0x0010107B File Offset: 0x0010007B
		public long ElapsedMilliseconds
		{
			get
			{
				return this.GetElapsedDateTimeTicks() / 10000L;
			}
		}

		// Token: 0x17000E21 RID: 3617
		// (get) Token: 0x06003C20 RID: 15392 RVA: 0x0010108A File Offset: 0x0010008A
		public long ElapsedTicks
		{
			get
			{
				return this.GetRawElapsedTicks();
			}
		}

		// Token: 0x06003C21 RID: 15393 RVA: 0x00101094 File Offset: 0x00100094
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

		// Token: 0x06003C22 RID: 15394 RVA: 0x001010C4 File Offset: 0x001000C4
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

		// Token: 0x06003C23 RID: 15395 RVA: 0x001010F4 File Offset: 0x001000F4
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

		// Token: 0x040034AA RID: 13482
		private const long TicksPerMillisecond = 10000L;

		// Token: 0x040034AB RID: 13483
		private const long TicksPerSecond = 10000000L;

		// Token: 0x040034AC RID: 13484
		private long elapsed;

		// Token: 0x040034AD RID: 13485
		private long startTimeStamp;

		// Token: 0x040034AE RID: 13486
		private bool isRunning;

		// Token: 0x040034AF RID: 13487
		public static readonly long Frequency;

		// Token: 0x040034B0 RID: 13488
		public static readonly bool IsHighResolution;

		// Token: 0x040034B1 RID: 13489
		private static readonly double tickFrequency;
	}
}
