using System;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace System.Net.Http.Internal
{
	// Token: 0x0200001B RID: 27
	internal class ReadOnlyStreamWithEncodingPreamble : Stream
	{
		// Token: 0x060000E1 RID: 225 RVA: 0x00004B44 File Offset: 0x00002D44
		public ReadOnlyStreamWithEncodingPreamble(Stream innerStream, Encoding encoding)
		{
			this._innerStream = innerStream;
			byte[] preamble = encoding.GetPreamble();
			int num = preamble.Length;
			if (num <= 0)
			{
				return;
			}
			int num2 = num * 2;
			byte[] array = new byte[num2];
			int i = num;
			preamble.CopyTo(array, 0);
			while (i < num2)
			{
				int num3 = innerStream.ReadByte();
				if (num3 == -1)
				{
					break;
				}
				array[i] = (byte)num3;
				i++;
			}
			if (i == num2)
			{
				bool flag = true;
				for (int j = 0; j < num; j++)
				{
					if (array[j] != array[j + num])
					{
						flag = false;
						break;
					}
				}
				if (flag)
				{
					i = num;
				}
			}
			this._remainingBytes = new ArraySegment<byte>(array, 0, i);
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x060000E2 RID: 226 RVA: 0x00004BE4 File Offset: 0x00002DE4
		public override bool CanRead
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x060000E3 RID: 227 RVA: 0x00004BE7 File Offset: 0x00002DE7
		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x060000E4 RID: 228 RVA: 0x00004BEA File Offset: 0x00002DEA
		public override bool CanWrite
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x060000E5 RID: 229 RVA: 0x00004BED File Offset: 0x00002DED
		public override long Length
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x060000E6 RID: 230 RVA: 0x00004BF4 File Offset: 0x00002DF4
		// (set) Token: 0x060000E7 RID: 231 RVA: 0x00004BFB File Offset: 0x00002DFB
		public override long Position
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x060000E8 RID: 232 RVA: 0x00004C02 File Offset: 0x00002E02
		public override void Flush()
		{
			throw new NotImplementedException();
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x00004C0C File Offset: 0x00002E0C
		private static Task<int> GetCancelledTask()
		{
			TaskCompletionSource<int> taskCompletionSource = new TaskCompletionSource<int>();
			taskCompletionSource.SetCanceled();
			return taskCompletionSource.Task;
		}

		// Token: 0x060000EA RID: 234 RVA: 0x00004C2C File Offset: 0x00002E2C
		public override int Read(byte[] buffer, int offset, int count)
		{
			byte[] array = this._remainingBytes.Array;
			if (array == null)
			{
				return this._innerStream.Read(buffer, offset, count);
			}
			int count2 = this._remainingBytes.Count;
			int offset2 = this._remainingBytes.Offset;
			int num = Math.Min(count, count2);
			for (int i = 0; i < num; i++)
			{
				buffer[offset + i] = array[offset2 + i];
			}
			if (num == count2)
			{
				this._remainingBytes = default(ArraySegment<byte>);
			}
			else
			{
				this._remainingBytes = new ArraySegment<byte>(array, offset2 + num, count2 - num);
			}
			return num;
		}

		// Token: 0x060000EB RID: 235 RVA: 0x00004CB8 File Offset: 0x00002EB8
		public override Task<int> ReadAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
		{
			if (this._remainingBytes.Array == null)
			{
				return this._innerStream.ReadAsync(buffer, offset, count, cancellationToken);
			}
			if (cancellationToken.IsCancellationRequested)
			{
				return ReadOnlyStreamWithEncodingPreamble._cancelledTask;
			}
			return Task.FromResult<int>(this.Read(buffer, offset, count));
		}

		// Token: 0x060000EC RID: 236 RVA: 0x00004CF5 File Offset: 0x00002EF5
		public override long Seek(long offset, SeekOrigin origin)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060000ED RID: 237 RVA: 0x00004CFC File Offset: 0x00002EFC
		public override void SetLength(long value)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060000EE RID: 238 RVA: 0x00004D03 File Offset: 0x00002F03
		public override void Write(byte[] buffer, int offset, int count)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0400003D RID: 61
		private static Task<int> _cancelledTask = ReadOnlyStreamWithEncodingPreamble.GetCancelledTask();

		// Token: 0x0400003E RID: 62
		private Stream _innerStream;

		// Token: 0x0400003F RID: 63
		private ArraySegment<byte> _remainingBytes;
	}
}
