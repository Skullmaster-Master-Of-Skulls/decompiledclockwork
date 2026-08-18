using System;

namespace Google.Apis.Util
{
	// Token: 0x02000005 RID: 5
	public class ExponentialBackOff : IBackOff
	{
		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000011 RID: 17 RVA: 0x000020F6 File Offset: 0x000002F6
		public TimeSpan DeltaBackOff
		{
			get
			{
				return this.deltaBackOff;
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000012 RID: 18 RVA: 0x000020FE File Offset: 0x000002FE
		public int MaxNumOfRetries
		{
			get
			{
				return this.maxNumOfRetries;
			}
		}

		// Token: 0x06000013 RID: 19 RVA: 0x00002106 File Offset: 0x00000306
		public ExponentialBackOff() : this(TimeSpan.FromMilliseconds(250.0), 10)
		{
		}

		// Token: 0x06000014 RID: 20 RVA: 0x00002120 File Offset: 0x00000320
		public ExponentialBackOff(TimeSpan deltaBackOff, int maximumNumOfRetries = 10)
		{
			if (deltaBackOff < TimeSpan.Zero || deltaBackOff > TimeSpan.FromSeconds(1.0))
			{
				throw new ArgumentOutOfRangeException("deltaBackOff");
			}
			if (maximumNumOfRetries < 0 || maximumNumOfRetries > 20)
			{
				throw new ArgumentOutOfRangeException("deltaBackOff");
			}
			this.deltaBackOff = deltaBackOff;
			this.maxNumOfRetries = maximumNumOfRetries;
		}

		// Token: 0x06000015 RID: 21 RVA: 0x00002190 File Offset: 0x00000390
		public TimeSpan GetNextBackOff(int currentRetry)
		{
			if (currentRetry <= 0)
			{
				throw new ArgumentOutOfRangeException("currentRetry");
			}
			if (currentRetry > this.MaxNumOfRetries)
			{
				return TimeSpan.MinValue;
			}
			double num = (double)this.random.Next((int)(this.DeltaBackOff.TotalMilliseconds * -1.0), (int)(this.DeltaBackOff.TotalMilliseconds * 1.0));
			return TimeSpan.FromMilliseconds((double)((int)(Math.Pow(2.0, (double)currentRetry - 1.0) * 1000.0 + num)));
		}

		// Token: 0x04000005 RID: 5
		private const int MaxAllowedNumRetries = 20;

		// Token: 0x04000006 RID: 6
		private readonly TimeSpan deltaBackOff;

		// Token: 0x04000007 RID: 7
		private readonly int maxNumOfRetries;

		// Token: 0x04000008 RID: 8
		private Random random = new Random();
	}
}
