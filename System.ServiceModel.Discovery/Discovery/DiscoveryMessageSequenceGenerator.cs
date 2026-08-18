using System;
using System.Threading;

namespace System.ServiceModel.Discovery
{
	// Token: 0x02000022 RID: 34
	public class DiscoveryMessageSequenceGenerator
	{
		// Token: 0x06000196 RID: 406 RVA: 0x00006726 File Offset: 0x00004926
		public DiscoveryMessageSequenceGenerator() : this(DiscoveryMessageSequenceGenerator.CreateInstanceId(), null)
		{
		}

		// Token: 0x06000197 RID: 407 RVA: 0x00006734 File Offset: 0x00004934
		public DiscoveryMessageSequenceGenerator(long instanceId, Uri sequenceId)
		{
			if (instanceId < 0L || instanceId > (long)((ulong)-1))
			{
				throw FxTrace.Exception.ArgumentOutOfRange("instanceId", instanceId, SR.DiscoveryAppSequenceInstanceIdOutOfRange);
			}
			this.instanceId = instanceId;
			this.sequenceId = sequenceId;
		}

		// Token: 0x06000198 RID: 408 RVA: 0x00006770 File Offset: 0x00004970
		private static long CreateInstanceId()
		{
			return (long)DateTime.Now.Subtract(DiscoveryMessageSequenceGenerator.DT1970).TotalSeconds;
		}

		// Token: 0x06000199 RID: 409 RVA: 0x00006798 File Offset: 0x00004998
		public DiscoveryMessageSequence Next()
		{
			return new DiscoveryMessageSequence(this.instanceId, this.sequenceId, Interlocked.Increment(ref this.messageNumber));
		}

		// Token: 0x04000068 RID: 104
		private static readonly DateTime DT1970 = new DateTime(1970, 1, 1);

		// Token: 0x04000069 RID: 105
		private long instanceId;

		// Token: 0x0400006A RID: 106
		private Uri sequenceId;

		// Token: 0x0400006B RID: 107
		private long messageNumber;
	}
}
