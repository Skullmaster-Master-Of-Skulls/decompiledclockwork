using System;
using System.IO;

namespace System.Net
{
	// Token: 0x02000683 RID: 1667
	internal class BufferedReadStream : DelegatedStream
	{
		// Token: 0x060033A3 RID: 13219 RVA: 0x000DA03A File Offset: 0x000D903A
		internal BufferedReadStream(Stream stream) : this(stream, false)
		{
		}

		// Token: 0x060033A4 RID: 13220 RVA: 0x000DA044 File Offset: 0x000D9044
		internal BufferedReadStream(Stream stream, bool readMore) : base(stream)
		{
			this.readMore = readMore;
		}

		// Token: 0x17000C20 RID: 3104
		// (get) Token: 0x060033A5 RID: 13221 RVA: 0x000DA054 File Offset: 0x000D9054
		public override bool CanWrite
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000C21 RID: 3105
		// (get) Token: 0x060033A6 RID: 13222 RVA: 0x000DA057 File Offset: 0x000D9057
		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060033A7 RID: 13223 RVA: 0x000DA05C File Offset: 0x000D905C
		public override IAsyncResult BeginRead(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
		{
			BufferedReadStream.ReadAsyncResult readAsyncResult = new BufferedReadStream.ReadAsyncResult(this, callback, state);
			readAsyncResult.Read(buffer, offset, count);
			return readAsyncResult;
		}

		// Token: 0x060033A8 RID: 13224 RVA: 0x000DA080 File Offset: 0x000D9080
		public override int EndRead(IAsyncResult asyncResult)
		{
			return BufferedReadStream.ReadAsyncResult.End(asyncResult);
		}

		// Token: 0x060033A9 RID: 13225 RVA: 0x000DA098 File Offset: 0x000D9098
		public override int Read(byte[] buffer, int offset, int count)
		{
			int num = 0;
			if (this.storedOffset < this.storedLength)
			{
				num = Math.Min(count, this.storedLength - this.storedOffset);
				Buffer.BlockCopy(this.storedBuffer, this.storedOffset, buffer, offset, num);
				this.storedOffset += num;
				if (num == count || !this.readMore)
				{
					return num;
				}
				offset += num;
				count -= num;
			}
			return num + base.Read(buffer, offset, count);
		}

		// Token: 0x060033AA RID: 13226 RVA: 0x000DA110 File Offset: 0x000D9110
		public override int ReadByte()
		{
			if (this.storedOffset < this.storedLength)
			{
				return (int)this.storedBuffer[this.storedOffset++];
			}
			return base.ReadByte();
		}

		// Token: 0x060033AB RID: 13227 RVA: 0x000DA14C File Offset: 0x000D914C
		internal void Push(byte[] buffer, int offset, int count)
		{
			if (count == 0)
			{
				return;
			}
			if (this.storedOffset == this.storedLength)
			{
				if (this.storedBuffer == null || this.storedBuffer.Length < count)
				{
					this.storedBuffer = new byte[count];
				}
				this.storedOffset = 0;
				this.storedLength = count;
			}
			else if (count <= this.storedOffset)
			{
				this.storedOffset -= count;
			}
			else if (count <= this.storedBuffer.Length - this.storedLength + this.storedOffset)
			{
				Buffer.BlockCopy(this.storedBuffer, this.storedOffset, this.storedBuffer, count, this.storedLength - this.storedOffset);
				this.storedLength += count - this.storedOffset;
				this.storedOffset = 0;
			}
			else
			{
				byte[] dst = new byte[count + this.storedLength - this.storedOffset];
				Buffer.BlockCopy(this.storedBuffer, this.storedOffset, dst, count, this.storedLength - this.storedOffset);
				this.storedLength += count - this.storedOffset;
				this.storedOffset = 0;
				this.storedBuffer = dst;
			}
			Buffer.BlockCopy(buffer, offset, this.storedBuffer, this.storedOffset, count);
		}

		// Token: 0x04002FAE RID: 12206
		private byte[] storedBuffer;

		// Token: 0x04002FAF RID: 12207
		private int storedLength;

		// Token: 0x04002FB0 RID: 12208
		private int storedOffset;

		// Token: 0x04002FB1 RID: 12209
		private bool readMore;

		// Token: 0x02000684 RID: 1668
		private class ReadAsyncResult : LazyAsyncResult
		{
			// Token: 0x060033AC RID: 13228 RVA: 0x000DA281 File Offset: 0x000D9281
			internal ReadAsyncResult(BufferedReadStream parent, AsyncCallback callback, object state) : base(null, state, callback)
			{
				this.parent = parent;
			}

			// Token: 0x060033AD RID: 13229 RVA: 0x000DA294 File Offset: 0x000D9294
			internal void Read(byte[] buffer, int offset, int count)
			{
				if (this.parent.storedOffset < this.parent.storedLength)
				{
					this.read = Math.Min(count, this.parent.storedLength - this.parent.storedOffset);
					Buffer.BlockCopy(this.parent.storedBuffer, this.parent.storedOffset, buffer, offset, this.read);
					this.parent.storedOffset += this.read;
					if (this.read == count || !this.parent.readMore)
					{
						base.InvokeCallback();
						return;
					}
					count -= this.read;
					offset += this.read;
				}
				IAsyncResult asyncResult = this.parent.BaseStream.BeginRead(buffer, offset, count, BufferedReadStream.ReadAsyncResult.onRead, this);
				if (asyncResult.CompletedSynchronously)
				{
					this.read += this.parent.BaseStream.EndRead(asyncResult);
					base.InvokeCallback();
				}
			}

			// Token: 0x060033AE RID: 13230 RVA: 0x000DA394 File Offset: 0x000D9394
			internal static int End(IAsyncResult result)
			{
				BufferedReadStream.ReadAsyncResult readAsyncResult = (BufferedReadStream.ReadAsyncResult)result;
				readAsyncResult.InternalWaitForCompletion();
				return readAsyncResult.read;
			}

			// Token: 0x060033AF RID: 13231 RVA: 0x000DA3B8 File Offset: 0x000D93B8
			private static void OnRead(IAsyncResult result)
			{
				if (!result.CompletedSynchronously)
				{
					BufferedReadStream.ReadAsyncResult readAsyncResult = (BufferedReadStream.ReadAsyncResult)result.AsyncState;
					try
					{
						readAsyncResult.read += readAsyncResult.parent.BaseStream.EndRead(result);
						readAsyncResult.InvokeCallback();
					}
					catch (Exception result2)
					{
						if (readAsyncResult.IsCompleted)
						{
							throw;
						}
						readAsyncResult.InvokeCallback(result2);
					}
					catch
					{
						if (readAsyncResult.IsCompleted)
						{
							throw;
						}
						readAsyncResult.InvokeCallback(new Exception(SR.GetString("net_nonClsCompliantException")));
					}
				}
			}

			// Token: 0x04002FB2 RID: 12210
			private BufferedReadStream parent;

			// Token: 0x04002FB3 RID: 12211
			private int read;

			// Token: 0x04002FB4 RID: 12212
			private static AsyncCallback onRead = new AsyncCallback(BufferedReadStream.ReadAsyncResult.OnRead);
		}
	}
}
