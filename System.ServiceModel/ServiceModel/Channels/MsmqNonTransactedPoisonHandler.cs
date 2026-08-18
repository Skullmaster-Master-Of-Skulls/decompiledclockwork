using System;

namespace System.ServiceModel.Channels
{
	// Token: 0x020008D8 RID: 2264
	internal sealed class MsmqNonTransactedPoisonHandler : IPoisonHandlingStrategy, IDisposable
	{
		// Token: 0x0600562B RID: 22059 RVA: 0x0013B894 File Offset: 0x00139A94
		internal MsmqNonTransactedPoisonHandler(MsmqReceiveHelper receiver)
		{
			this.receiver = receiver;
		}

		// Token: 0x0600562C RID: 22060 RVA: 0x0013B8A3 File Offset: 0x00139AA3
		public void Open()
		{
		}

		// Token: 0x0600562D RID: 22061 RVA: 0x0013B8A5 File Offset: 0x00139AA5
		public bool CheckAndHandlePoisonMessage(MsmqMessageProperty messageProperty)
		{
			return false;
		}

		// Token: 0x0600562E RID: 22062 RVA: 0x0013B8A8 File Offset: 0x00139AA8
		public void FinalDisposition(MsmqMessageProperty messageProperty)
		{
			this.receiver.DropOrRejectReceivedMessage(messageProperty, false);
		}

		// Token: 0x0600562F RID: 22063 RVA: 0x0013B8B7 File Offset: 0x00139AB7
		public void Dispose()
		{
		}

		// Token: 0x0400354B RID: 13643
		private MsmqReceiveHelper receiver;
	}
}
