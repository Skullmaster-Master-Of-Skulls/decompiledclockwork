using System;
using System.Runtime;

namespace System.ServiceModel.Channels
{
	// Token: 0x0200095F RID: 2399
	internal struct MessageAttemptInfo
	{
		// Token: 0x06005D0D RID: 23821 RVA: 0x001579EE File Offset: 0x00155BEE
		public MessageAttemptInfo(Message message, long sequenceNumber, int retryCount, object state)
		{
			this.message = message;
			this.sequenceNumber = sequenceNumber;
			this.retryCount = retryCount;
			this.state = state;
		}

		// Token: 0x1700163A RID: 5690
		// (get) Token: 0x06005D0E RID: 23822 RVA: 0x00157A0D File Offset: 0x00155C0D
		public Message Message
		{
			get
			{
				return this.message;
			}
		}

		// Token: 0x1700163B RID: 5691
		// (get) Token: 0x06005D0F RID: 23823 RVA: 0x00157A15 File Offset: 0x00155C15
		public int RetryCount
		{
			get
			{
				return this.retryCount;
			}
		}

		// Token: 0x1700163C RID: 5692
		// (get) Token: 0x06005D10 RID: 23824 RVA: 0x00157A1D File Offset: 0x00155C1D
		public object State
		{
			get
			{
				return this.state;
			}
		}

		// Token: 0x06005D11 RID: 23825 RVA: 0x00157A25 File Offset: 0x00155C25
		public long GetSequenceNumber()
		{
			if (this.sequenceNumber <= 0L)
			{
				throw Fx.AssertAndThrow("The caller is not allowed to get an invalid SequenceNumber.");
			}
			return this.sequenceNumber;
		}

		// Token: 0x04003762 RID: 14178
		private readonly Message message;

		// Token: 0x04003763 RID: 14179
		private readonly int retryCount;

		// Token: 0x04003764 RID: 14180
		private readonly long sequenceNumber;

		// Token: 0x04003765 RID: 14181
		private readonly object state;
	}
}
