using System;

namespace System.Runtime.Remoting.Messaging
{
	// Token: 0x020007A4 RID: 1956
	internal class DisposeSink : IMessageSink
	{
		// Token: 0x060045A3 RID: 17827 RVA: 0x000ECE87 File Offset: 0x000EBE87
		internal DisposeSink(IDisposable iDis, IMessageSink replySink)
		{
			this._iDis = iDis;
			this._replySink = replySink;
		}

		// Token: 0x060045A4 RID: 17828 RVA: 0x000ECEA0 File Offset: 0x000EBEA0
		public virtual IMessage SyncProcessMessage(IMessage reqMsg)
		{
			IMessage result = null;
			try
			{
				if (this._replySink != null)
				{
					result = this._replySink.SyncProcessMessage(reqMsg);
				}
			}
			finally
			{
				this._iDis.Dispose();
			}
			return result;
		}

		// Token: 0x060045A5 RID: 17829 RVA: 0x000ECEE4 File Offset: 0x000EBEE4
		public virtual IMessageCtrl AsyncProcessMessage(IMessage reqMsg, IMessageSink replySink)
		{
			throw new NotSupportedException();
		}

		// Token: 0x17000C3F RID: 3135
		// (get) Token: 0x060045A6 RID: 17830 RVA: 0x000ECEEB File Offset: 0x000EBEEB
		public IMessageSink NextSink
		{
			get
			{
				return this._replySink;
			}
		}

		// Token: 0x0400229E RID: 8862
		private IDisposable _iDis;

		// Token: 0x0400229F RID: 8863
		private IMessageSink _replySink;
	}
}
