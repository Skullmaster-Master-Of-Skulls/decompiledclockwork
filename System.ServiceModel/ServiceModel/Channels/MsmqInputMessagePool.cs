using System;

namespace System.ServiceModel.Channels
{
	// Token: 0x020008EC RID: 2284
	internal sealed class MsmqInputMessagePool : SynchronizedDisposablePool<MsmqInputMessage>, IMsmqMessagePool, IDisposable
	{
		// Token: 0x06005714 RID: 22292 RVA: 0x0013F8AF File Offset: 0x0013DAAF
		internal MsmqInputMessagePool(int maxPoolSize) : base(maxPoolSize)
		{
			this.maxPoolSize = maxPoolSize;
		}

		// Token: 0x06005715 RID: 22293 RVA: 0x0013F8C0 File Offset: 0x0013DAC0
		MsmqInputMessage IMsmqMessagePool.TakeMessage()
		{
			MsmqInputMessage msmqInputMessage = base.Take();
			if (msmqInputMessage == null)
			{
				msmqInputMessage = new MsmqInputMessage();
			}
			return msmqInputMessage;
		}

		// Token: 0x06005716 RID: 22294 RVA: 0x0013F8DE File Offset: 0x0013DADE
		void IMsmqMessagePool.ReturnMessage(MsmqInputMessage message)
		{
			if (!base.Return(message))
			{
				MsmqDiagnostics.PoolFull(this.maxPoolSize);
				message.Dispose();
			}
		}

		// Token: 0x04003594 RID: 13716
		private int maxPoolSize;
	}
}
