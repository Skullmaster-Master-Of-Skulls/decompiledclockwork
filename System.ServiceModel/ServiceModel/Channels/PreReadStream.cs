using System;
using System.IO;
using System.Runtime;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000866 RID: 2150
	internal class PreReadStream : DelegatingStream
	{
		// Token: 0x060050F0 RID: 20720 RVA: 0x00129CFC File Offset: 0x00127EFC
		public PreReadStream(Stream stream, byte[] preReadBuffer) : base(stream)
		{
			this.preReadBuffer = preReadBuffer;
		}

		// Token: 0x060050F1 RID: 20721 RVA: 0x00129D0C File Offset: 0x00127F0C
		private bool ReadFromBuffer(byte[] buffer, int offset, int count, out int bytesRead)
		{
			if (this.preReadBuffer == null)
			{
				bytesRead = -1;
				return false;
			}
			if (buffer == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("buffer");
			}
			if (offset >= buffer.Length)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("offset", offset, SR.GetString("OffsetExceedsBufferBound", new object[]
				{
					buffer.Length - 1
				})));
			}
			if (count < 0)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("count", count, SR.GetString("ValueMustBeNonNegative")));
			}
			if (count == 0)
			{
				bytesRead = 0;
			}
			else
			{
				buffer[offset] = this.preReadBuffer[0];
				this.preReadBuffer = null;
				bytesRead = 1;
			}
			return true;
		}

		// Token: 0x060050F2 RID: 20722 RVA: 0x00129DC4 File Offset: 0x00127FC4
		public override int Read(byte[] buffer, int offset, int count)
		{
			int result;
			if (this.ReadFromBuffer(buffer, offset, count, out result))
			{
				return result;
			}
			return base.Read(buffer, offset, count);
		}

		// Token: 0x060050F3 RID: 20723 RVA: 0x00129DEC File Offset: 0x00127FEC
		public override int ReadByte()
		{
			if (this.preReadBuffer != null)
			{
				byte[] array = new byte[1];
				int num;
				if (this.ReadFromBuffer(array, 0, 1, out num))
				{
					return (int)array[0];
				}
			}
			return base.ReadByte();
		}

		// Token: 0x060050F4 RID: 20724 RVA: 0x00129E20 File Offset: 0x00128020
		public override IAsyncResult BeginRead(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
		{
			int data;
			if (this.ReadFromBuffer(buffer, offset, count, out data))
			{
				return new CompletedAsyncResult<int>(data, callback, state);
			}
			return base.BeginRead(buffer, offset, count, callback, state);
		}

		// Token: 0x060050F5 RID: 20725 RVA: 0x00129E52 File Offset: 0x00128052
		public override int EndRead(IAsyncResult result)
		{
			if (result is CompletedAsyncResult<int>)
			{
				return CompletedAsyncResult<int>.End(result);
			}
			return base.EndRead(result);
		}

		// Token: 0x040031ED RID: 12781
		private byte[] preReadBuffer;
	}
}
