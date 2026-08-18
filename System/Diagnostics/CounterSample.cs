using System;

namespace System.Diagnostics
{
	// Token: 0x02000745 RID: 1861
	public struct CounterSample
	{
		// Token: 0x060038D0 RID: 14544 RVA: 0x000EF969 File Offset: 0x000EE969
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

		// Token: 0x060038D1 RID: 14545 RVA: 0x000EF9A8 File Offset: 0x000EE9A8
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

		// Token: 0x17000D2B RID: 3371
		// (get) Token: 0x060038D2 RID: 14546 RVA: 0x000EF9E7 File Offset: 0x000EE9E7
		public long RawValue
		{
			get
			{
				return this.rawValue;
			}
		}

		// Token: 0x17000D2C RID: 3372
		// (get) Token: 0x060038D3 RID: 14547 RVA: 0x000EF9EF File Offset: 0x000EE9EF
		internal ulong UnsignedRawValue
		{
			get
			{
				return (ulong)this.rawValue;
			}
		}

		// Token: 0x17000D2D RID: 3373
		// (get) Token: 0x060038D4 RID: 14548 RVA: 0x000EF9F7 File Offset: 0x000EE9F7
		public long BaseValue
		{
			get
			{
				return this.baseValue;
			}
		}

		// Token: 0x17000D2E RID: 3374
		// (get) Token: 0x060038D5 RID: 14549 RVA: 0x000EF9FF File Offset: 0x000EE9FF
		public long SystemFrequency
		{
			get
			{
				return this.systemFrequency;
			}
		}

		// Token: 0x17000D2F RID: 3375
		// (get) Token: 0x060038D6 RID: 14550 RVA: 0x000EFA07 File Offset: 0x000EEA07
		public long CounterFrequency
		{
			get
			{
				return this.counterFrequency;
			}
		}

		// Token: 0x17000D30 RID: 3376
		// (get) Token: 0x060038D7 RID: 14551 RVA: 0x000EFA0F File Offset: 0x000EEA0F
		public long CounterTimeStamp
		{
			get
			{
				return this.counterTimeStamp;
			}
		}

		// Token: 0x17000D31 RID: 3377
		// (get) Token: 0x060038D8 RID: 14552 RVA: 0x000EFA17 File Offset: 0x000EEA17
		public long TimeStamp
		{
			get
			{
				return this.timeStamp;
			}
		}

		// Token: 0x17000D32 RID: 3378
		// (get) Token: 0x060038D9 RID: 14553 RVA: 0x000EFA1F File Offset: 0x000EEA1F
		public long TimeStamp100nSec
		{
			get
			{
				return this.timeStamp100nSec;
			}
		}

		// Token: 0x17000D33 RID: 3379
		// (get) Token: 0x060038DA RID: 14554 RVA: 0x000EFA27 File Offset: 0x000EEA27
		public PerformanceCounterType CounterType
		{
			get
			{
				return this.counterType;
			}
		}

		// Token: 0x060038DB RID: 14555 RVA: 0x000EFA2F File Offset: 0x000EEA2F
		public static float Calculate(CounterSample counterSample)
		{
			return CounterSampleCalculator.ComputeCounterValue(counterSample);
		}

		// Token: 0x060038DC RID: 14556 RVA: 0x000EFA37 File Offset: 0x000EEA37
		public static float Calculate(CounterSample counterSample, CounterSample nextCounterSample)
		{
			return CounterSampleCalculator.ComputeCounterValue(counterSample, nextCounterSample);
		}

		// Token: 0x060038DD RID: 14557 RVA: 0x000EFA40 File Offset: 0x000EEA40
		public override bool Equals(object o)
		{
			return o is CounterSample && this.Equals((CounterSample)o);
		}

		// Token: 0x060038DE RID: 14558 RVA: 0x000EFA58 File Offset: 0x000EEA58
		public bool Equals(CounterSample sample)
		{
			return this.rawValue == sample.rawValue && this.baseValue == sample.baseValue && this.timeStamp == sample.timeStamp && this.counterFrequency == sample.counterFrequency && this.counterType == sample.counterType && this.timeStamp100nSec == sample.timeStamp100nSec && this.systemFrequency == sample.systemFrequency && this.counterTimeStamp == sample.counterTimeStamp;
		}

		// Token: 0x060038DF RID: 14559 RVA: 0x000EFADF File Offset: 0x000EEADF
		public override int GetHashCode()
		{
			return this.rawValue.GetHashCode();
		}

		// Token: 0x060038E0 RID: 14560 RVA: 0x000EFAEC File Offset: 0x000EEAEC
		public static bool operator ==(CounterSample a, CounterSample b)
		{
			return a.Equals(b);
		}

		// Token: 0x060038E1 RID: 14561 RVA: 0x000EFAF6 File Offset: 0x000EEAF6
		public static bool operator !=(CounterSample a, CounterSample b)
		{
			return !a.Equals(b);
		}

		// Token: 0x04003275 RID: 12917
		private long rawValue;

		// Token: 0x04003276 RID: 12918
		private long baseValue;

		// Token: 0x04003277 RID: 12919
		private long timeStamp;

		// Token: 0x04003278 RID: 12920
		private long counterFrequency;

		// Token: 0x04003279 RID: 12921
		private PerformanceCounterType counterType;

		// Token: 0x0400327A RID: 12922
		private long timeStamp100nSec;

		// Token: 0x0400327B RID: 12923
		private long systemFrequency;

		// Token: 0x0400327C RID: 12924
		private long counterTimeStamp;

		// Token: 0x0400327D RID: 12925
		public static CounterSample Empty = new CounterSample(0L, 0L, 0L, 0L, 0L, 0L, PerformanceCounterType.NumberOfItems32);
	}
}
