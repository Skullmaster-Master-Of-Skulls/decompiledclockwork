using System;

namespace System.Diagnostics
{
	// Token: 0x020004C2 RID: 1218
	public struct CounterSample
	{
		// Token: 0x06002D87 RID: 11655 RVA: 0x000CCC49 File Offset: 0x000CAE49
		public CounterSample(long rawValue, long baseValue, long counterFrequency, long systemFrequency, long timeStamp, long timeStamp100nSec, PerformanceCounterType counterType)
		{
			this.rawValue = rawValue;
			this.baseValue = baseValue;
			this.timeStamp = timeStamp;
			this.counterFrequency = counterFrequency;
			this.counterType = counterType;
			this.timeStamp100nSec = timeStamp100nSec;
			this.systemFrequency = systemFrequency;
			this.counterTimeStamp = 0L;
		}

		// Token: 0x06002D88 RID: 11656 RVA: 0x000CCC88 File Offset: 0x000CAE88
		public CounterSample(long rawValue, long baseValue, long counterFrequency, long systemFrequency, long timeStamp, long timeStamp100nSec, PerformanceCounterType counterType, long counterTimeStamp)
		{
			this.rawValue = rawValue;
			this.baseValue = baseValue;
			this.timeStamp = timeStamp;
			this.counterFrequency = counterFrequency;
			this.counterType = counterType;
			this.timeStamp100nSec = timeStamp100nSec;
			this.systemFrequency = systemFrequency;
			this.counterTimeStamp = counterTimeStamp;
		}

		// Token: 0x17000AFE RID: 2814
		// (get) Token: 0x06002D89 RID: 11657 RVA: 0x000CCCC7 File Offset: 0x000CAEC7
		public long RawValue
		{
			get
			{
				return this.rawValue;
			}
		}

		// Token: 0x17000AFF RID: 2815
		// (get) Token: 0x06002D8A RID: 11658 RVA: 0x000CCCCF File Offset: 0x000CAECF
		internal ulong UnsignedRawValue
		{
			get
			{
				return (ulong)this.rawValue;
			}
		}

		// Token: 0x17000B00 RID: 2816
		// (get) Token: 0x06002D8B RID: 11659 RVA: 0x000CCCD7 File Offset: 0x000CAED7
		public long BaseValue
		{
			get
			{
				return this.baseValue;
			}
		}

		// Token: 0x17000B01 RID: 2817
		// (get) Token: 0x06002D8C RID: 11660 RVA: 0x000CCCDF File Offset: 0x000CAEDF
		public long SystemFrequency
		{
			get
			{
				return this.systemFrequency;
			}
		}

		// Token: 0x17000B02 RID: 2818
		// (get) Token: 0x06002D8D RID: 11661 RVA: 0x000CCCE7 File Offset: 0x000CAEE7
		public long CounterFrequency
		{
			get
			{
				return this.counterFrequency;
			}
		}

		// Token: 0x17000B03 RID: 2819
		// (get) Token: 0x06002D8E RID: 11662 RVA: 0x000CCCEF File Offset: 0x000CAEEF
		public long CounterTimeStamp
		{
			get
			{
				return this.counterTimeStamp;
			}
		}

		// Token: 0x17000B04 RID: 2820
		// (get) Token: 0x06002D8F RID: 11663 RVA: 0x000CCCF7 File Offset: 0x000CAEF7
		public long TimeStamp
		{
			get
			{
				return this.timeStamp;
			}
		}

		// Token: 0x17000B05 RID: 2821
		// (get) Token: 0x06002D90 RID: 11664 RVA: 0x000CCCFF File Offset: 0x000CAEFF
		public long TimeStamp100nSec
		{
			get
			{
				return this.timeStamp100nSec;
			}
		}

		// Token: 0x17000B06 RID: 2822
		// (get) Token: 0x06002D91 RID: 11665 RVA: 0x000CCD07 File Offset: 0x000CAF07
		public PerformanceCounterType CounterType
		{
			get
			{
				return this.counterType;
			}
		}

		// Token: 0x06002D92 RID: 11666 RVA: 0x000CCD0F File Offset: 0x000CAF0F
		public static float Calculate(CounterSample counterSample)
		{
			return CounterSampleCalculator.ComputeCounterValue(counterSample);
		}

		// Token: 0x06002D93 RID: 11667 RVA: 0x000CCD17 File Offset: 0x000CAF17
		public static float Calculate(CounterSample counterSample, CounterSample nextCounterSample)
		{
			return CounterSampleCalculator.ComputeCounterValue(counterSample, nextCounterSample);
		}

		// Token: 0x06002D94 RID: 11668 RVA: 0x000CCD20 File Offset: 0x000CAF20
		public override bool Equals(object o)
		{
			return o is CounterSample && this.Equals((CounterSample)o);
		}

		// Token: 0x06002D95 RID: 11669 RVA: 0x000CCD38 File Offset: 0x000CAF38
		public bool Equals(CounterSample sample)
		{
			return this.rawValue == sample.rawValue && this.baseValue == sample.baseValue && this.timeStamp == sample.timeStamp && this.counterFrequency == sample.counterFrequency && this.counterType == sample.counterType && this.timeStamp100nSec == sample.timeStamp100nSec && this.systemFrequency == sample.systemFrequency && this.counterTimeStamp == sample.counterTimeStamp;
		}

		// Token: 0x06002D96 RID: 11670 RVA: 0x000CCDB7 File Offset: 0x000CAFB7
		public override int GetHashCode()
		{
			return this.rawValue.GetHashCode();
		}

		// Token: 0x06002D97 RID: 11671 RVA: 0x000CCDC4 File Offset: 0x000CAFC4
		public static bool operator ==(CounterSample a, CounterSample b)
		{
			return a.Equals(b);
		}

		// Token: 0x06002D98 RID: 11672 RVA: 0x000CCDCE File Offset: 0x000CAFCE
		public static bool operator !=(CounterSample a, CounterSample b)
		{
			return !a.Equals(b);
		}

		// Token: 0x0400272C RID: 10028
		private long rawValue;

		// Token: 0x0400272D RID: 10029
		private long baseValue;

		// Token: 0x0400272E RID: 10030
		private long timeStamp;

		// Token: 0x0400272F RID: 10031
		private long counterFrequency;

		// Token: 0x04002730 RID: 10032
		private PerformanceCounterType counterType;

		// Token: 0x04002731 RID: 10033
		private long timeStamp100nSec;

		// Token: 0x04002732 RID: 10034
		private long systemFrequency;

		// Token: 0x04002733 RID: 10035
		private long counterTimeStamp;

		// Token: 0x04002734 RID: 10036
		public static CounterSample Empty = new CounterSample(0L, 0L, 0L, 0L, 0L, 0L, PerformanceCounterType.NumberOfItems32);
	}
}
