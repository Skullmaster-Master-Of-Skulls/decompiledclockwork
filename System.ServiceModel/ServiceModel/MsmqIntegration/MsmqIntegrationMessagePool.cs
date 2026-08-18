using System;
using System.ServiceModel.Channels;

namespace System.ServiceModel.MsmqIntegration
{
	// Token: 0x020003B3 RID: 947
	internal sealed class MsmqIntegrationMessagePool : SynchronizedDisposablePool<MsmqIntegrationInputMessage>, IMsmqMessagePool, IDisposable
	{
		// Token: 0x06002366 RID: 9062 RVA: 0x00081C16 File Offset: 0x0007FE16
		internal MsmqIntegrationMessagePool(int maxPoolSize) : base(maxPoolSize)
		{
			this.maxPoolSize = maxPoolSize;
		}

		// Token: 0x06002367 RID: 9063 RVA: 0x00081C28 File Offset: 0x0007FE28
		MsmqInputMessage IMsmqMessagePool.TakeMessage()
		{
			MsmqIntegrationInputMessage msmqIntegrationInputMessage = base.Take();
			if (msmqIntegrationInputMessage == null)
			{
				msmqIntegrationInputMessage = new MsmqIntegrationInputMessage();
			}
			return msmqIntegrationInputMessage;
		}

		// Token: 0x06002368 RID: 9064 RVA: 0x00081C46 File Offset: 0x0007FE46
		void IMsmqMessagePool.ReturnMessage(MsmqInputMessage message)
		{
			if (!base.Return(message as MsmqIntegrationInputMessage))
			{
				MsmqDiagnostics.PoolFull(this.maxPoolSize);
				message.Dispose();
			}
		}

		// Token: 0x04002003 RID: 8195
		private int maxPoolSize;
	}
}
