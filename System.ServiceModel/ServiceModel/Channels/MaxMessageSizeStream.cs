using System;
using System.IO;
using System.ServiceModel.Diagnostics.Application;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000781 RID: 1921
	internal class MaxMessageSizeStream : DelegatingStream
	{
		// Token: 0x06004929 RID: 18729 RVA: 0x0010DA18 File Offset: 0x0010BC18
		public MaxMessageSizeStream(Stream stream, long maxMessageSize) : base(stream)
		{
			this.maxMessageSize = maxMessageSize;
		}

		// Token: 0x0600492A RID: 18730 RVA: 0x0010DA28 File Offset: 0x0010BC28
		public override IAsyncResult BeginRead(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
		{
			count = this.PrepareRead(count);
			return base.BeginRead(buffer, offset, count, callback, state);
		}

		// Token: 0x0600492B RID: 18731 RVA: 0x0010DA40 File Offset: 0x0010BC40
		public override IAsyncResult BeginWrite(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
		{
			this.PrepareWrite(count);
			return base.BeginWrite(buffer, offset, count, callback, state);
		}

		// Token: 0x0600492C RID: 18732 RVA: 0x0010DA56 File Offset: 0x0010BC56
		public override int EndRead(IAsyncResult result)
		{
			return this.FinishRead(base.EndRead(result));
		}

		// Token: 0x0600492D RID: 18733 RVA: 0x0010DA65 File Offset: 0x0010BC65
		public override int Read(byte[] buffer, int offset, int count)
		{
			count = this.PrepareRead(count);
			return this.FinishRead(base.Read(buffer, offset, count));
		}

		// Token: 0x0600492E RID: 18734 RVA: 0x0010DA80 File Offset: 0x0010BC80
		public override int ReadByte()
		{
			this.PrepareRead(1);
			int num = base.ReadByte();
			if (num != -1)
			{
				this.FinishRead(1);
			}
			return num;
		}

		// Token: 0x0600492F RID: 18735 RVA: 0x0010DAA9 File Offset: 0x0010BCA9
		public override void Write(byte[] buffer, int offset, int count)
		{
			this.PrepareWrite(count);
			base.Write(buffer, offset, count);
		}

		// Token: 0x06004930 RID: 18736 RVA: 0x0010DABB File Offset: 0x0010BCBB
		public override void WriteByte(byte value)
		{
			this.PrepareWrite(1);
			base.WriteByte(value);
		}

		// Token: 0x06004931 RID: 18737 RVA: 0x0010DACC File Offset: 0x0010BCCC
		internal static Exception CreateMaxReceivedMessageSizeExceededException(long maxMessageSize)
		{
			string @string = SR.GetString("MaxReceivedMessageSizeExceeded", new object[]
			{
				maxMessageSize
			});
			Exception innerException = new QuotaExceededException(@string);
			if (TD.MaxReceivedMessageSizeExceededIsEnabled())
			{
				TD.MaxReceivedMessageSizeExceeded(@string);
			}
			return new CommunicationException(@string, innerException);
		}

		// Token: 0x06004932 RID: 18738 RVA: 0x0010DB10 File Offset: 0x0010BD10
		internal static Exception CreateMaxSentMessageSizeExceededException(long maxMessageSize)
		{
			string @string = SR.GetString("MaxSentMessageSizeExceeded", new object[]
			{
				maxMessageSize
			});
			Exception innerException = new QuotaExceededException(@string);
			if (TD.MaxSentMessageSizeExceededIsEnabled())
			{
				TD.MaxSentMessageSizeExceeded(@string);
			}
			return new CommunicationException(@string, innerException);
		}

		// Token: 0x06004933 RID: 18739 RVA: 0x0010DB54 File Offset: 0x0010BD54
		private int PrepareRead(int bytesToRead)
		{
			if (this.totalBytesRead >= this.maxMessageSize)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(MaxMessageSizeStream.CreateMaxReceivedMessageSizeExceededException(this.maxMessageSize));
			}
			long num = this.maxMessageSize - this.totalBytesRead;
			if (num > 2147483647L)
			{
				return bytesToRead;
			}
			return Math.Min(bytesToRead, (int)(this.maxMessageSize - this.totalBytesRead));
		}

		// Token: 0x06004934 RID: 18740 RVA: 0x0010DBB2 File Offset: 0x0010BDB2
		private int FinishRead(int bytesRead)
		{
			this.totalBytesRead += (long)bytesRead;
			return bytesRead;
		}

		// Token: 0x06004935 RID: 18741 RVA: 0x0010DBC4 File Offset: 0x0010BDC4
		private void PrepareWrite(int bytesToWrite)
		{
			if (this.bytesWritten + (long)bytesToWrite > this.maxMessageSize)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(MaxMessageSizeStream.CreateMaxSentMessageSizeExceededException(this.maxMessageSize));
			}
			this.bytesWritten += (long)bytesToWrite;
		}

		// Token: 0x04002E1D RID: 11805
		private long maxMessageSize;

		// Token: 0x04002E1E RID: 11806
		private long totalBytesRead;

		// Token: 0x04002E1F RID: 11807
		private long bytesWritten;
	}
}
