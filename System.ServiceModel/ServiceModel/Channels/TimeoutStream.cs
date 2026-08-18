using System;
using System.IO;
using System.Runtime;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000782 RID: 1922
	internal class TimeoutStream : DelegatingStream
	{
		// Token: 0x06004936 RID: 18742 RVA: 0x0010DBFC File Offset: 0x0010BDFC
		public TimeoutStream(Stream stream, ref TimeoutHelper timeoutHelper) : base(stream)
		{
			if (!stream.CanTimeout)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument("stream", SR.GetString("StreamDoesNotSupportTimeout"));
			}
			this.timeoutHelper = timeoutHelper;
		}

		// Token: 0x06004937 RID: 18743 RVA: 0x0010DC33 File Offset: 0x0010BE33
		private void UpdateReadTimeout()
		{
			this.ReadTimeout = TimeoutHelper.ToMilliseconds(this.timeoutHelper.RemainingTime());
		}

		// Token: 0x06004938 RID: 18744 RVA: 0x0010DC4B File Offset: 0x0010BE4B
		private void UpdateWriteTimeout()
		{
			this.WriteTimeout = TimeoutHelper.ToMilliseconds(this.timeoutHelper.RemainingTime());
		}

		// Token: 0x06004939 RID: 18745 RVA: 0x0010DC63 File Offset: 0x0010BE63
		public override IAsyncResult BeginRead(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
		{
			this.UpdateReadTimeout();
			return base.BeginRead(buffer, offset, count, callback, state);
		}

		// Token: 0x0600493A RID: 18746 RVA: 0x0010DC78 File Offset: 0x0010BE78
		public override IAsyncResult BeginWrite(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
		{
			this.UpdateWriteTimeout();
			return base.BeginWrite(buffer, offset, count, callback, state);
		}

		// Token: 0x0600493B RID: 18747 RVA: 0x0010DC8D File Offset: 0x0010BE8D
		public override int Read(byte[] buffer, int offset, int count)
		{
			this.UpdateReadTimeout();
			return base.Read(buffer, offset, count);
		}

		// Token: 0x0600493C RID: 18748 RVA: 0x0010DC9E File Offset: 0x0010BE9E
		public override int ReadByte()
		{
			this.UpdateReadTimeout();
			return base.ReadByte();
		}

		// Token: 0x0600493D RID: 18749 RVA: 0x0010DCAC File Offset: 0x0010BEAC
		public override void Write(byte[] buffer, int offset, int count)
		{
			this.UpdateWriteTimeout();
			base.Write(buffer, offset, count);
		}

		// Token: 0x0600493E RID: 18750 RVA: 0x0010DCBD File Offset: 0x0010BEBD
		public override void WriteByte(byte value)
		{
			this.UpdateWriteTimeout();
			base.WriteByte(value);
		}

		// Token: 0x04002E20 RID: 11808
		private TimeoutHelper timeoutHelper;
	}
}
