using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace System.Net
{
	// Token: 0x02000225 RID: 549
	internal class BufferedReadStream : DelegatedStream
	{
		// Token: 0x0600143B RID: 5179 RVA: 0x0006B442 File Offset: 0x00069642
		internal BufferedReadStream(Stream stream) : this(stream, false)
		{
		}

		// Token: 0x0600143C RID: 5180 RVA: 0x0006B44C File Offset: 0x0006964C
		internal BufferedReadStream(Stream stream, bool readMore) : base(stream)
		{
			this.readMore = readMore;
		}

		// Token: 0x1700043D RID: 1085
		// (get) Token: 0x0600143D RID: 5181 RVA: 0x0006B45C File Offset: 0x0006965C
		public override bool CanWrite
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700043E RID: 1086
		// (get) Token: 0x0600143E RID: 5182 RVA: 0x0006B45F File Offset: 0x0006965F
		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600143F RID: 5183 RVA: 0x0006B464 File Offset: 0x00069664
		public override IAsyncResult BeginRead(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
		{
			BufferedReadStream.ReadAsyncResult readAsyncResult = new BufferedReadStream.ReadAsyncResult(this, callback, state);
			readAsyncResult.Read(buffer, offset, count);
			return readAsyncResult;
		}

		// Token: 0x06001440 RID: 5184 RVA: 0x0006B488 File Offset: 0x00069688
		public override int EndRead(IAsyncResult asyncResult)
		{
			return BufferedReadStream.ReadAsyncResult.End(asyncResult);
		}

		// Token: 0x06001441 RID: 5185 RVA: 0x0006B4A0 File Offset: 0x000696A0
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

		// Token: 0x06001442 RID: 5186 RVA: 0x0006B518 File Offset: 0x00069718
		public override Task<int> ReadAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
		{
			if (this.storedOffset >= this.storedLength)
			{
				return base.ReadAsync(buffer, offset, count, cancellationToken);
			}
			int num = Math.Min(count, this.storedLength - this.storedOffset);
			Buffer.BlockCopy(this.storedBuffer, this.storedOffset, buffer, offset, num);
			this.storedOffset += num;
			if (num == count || !this.readMore)
			{
				return Task.FromResult<int>(num);
			}
			offset += num;
			count -= num;
			return this.ReadMoreAsync(num, buffer, offset, count, cancellationToken);
		}

		// Token: 0x06001443 RID: 5187 RVA: 0x0006B5A0 File Offset: 0x000697A0
		private Task<int> ReadMoreAsync(int bytesAlreadyRead, byte[] buffer, int offset, int count, CancellationToken cancellationToken)
		{
			BufferedReadStream.<ReadMoreAsync>d__14 <ReadMoreAsync>d__;
			<ReadMoreAsync>d__.<>t__builder = AsyncTaskMethodBuilder<int>.Create();
			<ReadMoreAsync>d__.<>4__this = this;
			<ReadMoreAsync>d__.bytesAlreadyRead = bytesAlreadyRead;
			<ReadMoreAsync>d__.buffer = buffer;
			<ReadMoreAsync>d__.offset = offset;
			<ReadMoreAsync>d__.count = count;
			<ReadMoreAsync>d__.cancellationToken = cancellationToken;
			<ReadMoreAsync>d__.<>1__state = -1;
			<ReadMoreAsync>d__.<>t__builder.Start<BufferedReadStream.<ReadMoreAsync>d__14>(ref <ReadMoreAsync>d__);
			return <ReadMoreAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06001444 RID: 5188 RVA: 0x0006B610 File Offset: 0x00069810
		public override int ReadByte()
		{
			if (this.storedOffset < this.storedLength)
			{
				byte[] array = this.storedBuffer;
				int num = this.storedOffset;
				this.storedOffset = num + 1;
				return array[num];
			}
			return base.ReadByte();
		}

		// Token: 0x06001445 RID: 5189 RVA: 0x0006B64C File Offset: 0x0006984C
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

		// Token: 0x06001446 RID: 5190 RVA: 0x0006B784 File Offset: 0x00069984
		internal void Append(byte[] buffer, int offset, int count)
		{
			if (count == 0)
			{
				return;
			}
			int num;
			if (this.storedOffset == this.storedLength)
			{
				if (this.storedBuffer == null || this.storedBuffer.Length < count)
				{
					this.storedBuffer = new byte[count];
				}
				this.storedOffset = 0;
				this.storedLength = count;
				num = 0;
			}
			else if (count <= this.storedBuffer.Length - this.storedLength)
			{
				num = this.storedLength;
				this.storedLength += count;
			}
			else if (count <= this.storedBuffer.Length - this.storedLength + this.storedOffset)
			{
				Buffer.BlockCopy(this.storedBuffer, this.storedOffset, this.storedBuffer, 0, this.storedLength - this.storedOffset);
				num = this.storedLength - this.storedOffset;
				this.storedOffset = 0;
				this.storedLength = count + num;
			}
			else
			{
				byte[] dst = new byte[count + this.storedLength - this.storedOffset];
				Buffer.BlockCopy(this.storedBuffer, this.storedOffset, dst, 0, this.storedLength - this.storedOffset);
				num = this.storedLength - this.storedOffset;
				this.storedOffset = 0;
				this.storedLength = count + num;
				this.storedBuffer = dst;
			}
			Buffer.BlockCopy(buffer, offset, this.storedBuffer, num, count);
		}

		// Token: 0x04001627 RID: 5671
		private byte[] storedBuffer;

		// Token: 0x04001628 RID: 5672
		private int storedLength;

		// Token: 0x04001629 RID: 5673
		private int storedOffset;

		// Token: 0x0400162A RID: 5674
		private bool readMore;

		// Token: 0x02000768 RID: 1896
		private class ReadAsyncResult : LazyAsyncResult
		{
			// Token: 0x06004254 RID: 16980 RVA: 0x00113304 File Offset: 0x00111504
			internal ReadAsyncResult(BufferedReadStream parent, AsyncCallback callback, object state) : base(null, state, callback)
			{
				this.parent = parent;
			}

			// Token: 0x06004255 RID: 16981 RVA: 0x00113318 File Offset: 0x00111518
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

			// Token: 0x06004256 RID: 16982 RVA: 0x00113418 File Offset: 0x00111618
			internal static int End(IAsyncResult result)
			{
				BufferedReadStream.ReadAsyncResult readAsyncResult = (BufferedReadStream.ReadAsyncResult)result;
				readAsyncResult.InternalWaitForCompletion();
				return readAsyncResult.read;
			}

			// Token: 0x06004257 RID: 16983 RVA: 0x0011343C File Offset: 0x0011163C
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
				}
			}

			// Token: 0x04003256 RID: 12886
			private BufferedReadStream parent;

			// Token: 0x04003257 RID: 12887
			private int read;

			// Token: 0x04003258 RID: 12888
			private static AsyncCallback onRead = new AsyncCallback(BufferedReadStream.ReadAsyncResult.OnRead);
		}
	}
}
