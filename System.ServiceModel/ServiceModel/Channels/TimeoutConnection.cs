using System;
using System.Runtime;
using System.Threading;

namespace System.ServiceModel.Channels
{
	// Token: 0x020007D1 RID: 2001
	internal class TimeoutConnection : DelegatingConnection
	{
		// Token: 0x06004B63 RID: 19299 RVA: 0x00113AA0 File Offset: 0x00111CA0
		public TimeoutConnection(IConnection innerConnection, TimeSpan timeout) : base(innerConnection)
		{
			this.timeoutHelper = new TimeoutHelper(timeout);
		}

		// Token: 0x170012EA RID: 4842
		// (get) Token: 0x06004B64 RID: 19300 RVA: 0x00113AB5 File Offset: 0x00111CB5
		public IConnection InnerConnection
		{
			get
			{
				return base.Connection;
			}
		}

		// Token: 0x06004B65 RID: 19301 RVA: 0x00113AC0 File Offset: 0x00111CC0
		public override AsyncCompletionResult BeginWrite(byte[] buffer, int offset, int size, bool immediate, TimeSpan timeout, WaitCallback callback, object state)
		{
			TimeSpan timeSpan = this.timeoutHelper.RemainingTime();
			if (timeout < timeSpan)
			{
				timeSpan = timeout;
			}
			return base.BeginWrite(buffer, offset, size, immediate, timeSpan, callback, state);
		}

		// Token: 0x06004B66 RID: 19302 RVA: 0x00113AF8 File Offset: 0x00111CF8
		public override void Write(byte[] buffer, int offset, int size, bool immediate, TimeSpan timeout)
		{
			TimeSpan timeSpan = this.timeoutHelper.RemainingTime();
			if (timeout < timeSpan)
			{
				timeSpan = timeout;
			}
			base.Write(buffer, offset, size, immediate, timeSpan);
		}

		// Token: 0x06004B67 RID: 19303 RVA: 0x00113B2C File Offset: 0x00111D2C
		public override void Write(byte[] buffer, int offset, int size, bool immediate, TimeSpan timeout, BufferManager bufferManager)
		{
			TimeSpan timeSpan = this.timeoutHelper.RemainingTime();
			if (timeout < timeSpan)
			{
				timeSpan = timeout;
			}
			base.Write(buffer, offset, size, immediate, timeSpan, bufferManager);
		}

		// Token: 0x06004B68 RID: 19304 RVA: 0x00113B60 File Offset: 0x00111D60
		public override int Read(byte[] buffer, int offset, int size, TimeSpan timeout)
		{
			TimeSpan timeSpan = this.timeoutHelper.RemainingTime();
			if (timeout < timeSpan)
			{
				timeSpan = timeout;
			}
			return base.Read(buffer, offset, size, timeSpan);
		}

		// Token: 0x06004B69 RID: 19305 RVA: 0x00113B90 File Offset: 0x00111D90
		public override AsyncCompletionResult BeginRead(int offset, int size, TimeSpan timeout, WaitCallback callback, object state)
		{
			TimeSpan timeSpan = this.timeoutHelper.RemainingTime();
			if (timeout < timeSpan)
			{
				timeSpan = timeout;
			}
			return base.BeginRead(offset, size, timeSpan, callback, state);
		}

		// Token: 0x04002F43 RID: 12099
		private TimeoutHelper timeoutHelper;
	}
}
