using System;
using System.IO;

namespace System.Net.Mime
{
	// Token: 0x020006B7 RID: 1719
	internal class SevenBitStream : DelegatedStream
	{
		// Token: 0x17000C65 RID: 3173
		// (get) Token: 0x0600351D RID: 13597 RVA: 0x000E1C75 File Offset: 0x000E0C75
		private WriteStateInfoBase WriteState
		{
			get
			{
				if (this.writeState == null)
				{
					this.writeState = new WriteStateInfoBase();
				}
				return this.writeState;
			}
		}

		// Token: 0x0600351E RID: 13598 RVA: 0x000E1C90 File Offset: 0x000E0C90
		internal SevenBitStream(Stream stream, bool shouldEncodeLeadingDots) : base(stream)
		{
			this.shouldEncodeLeadingDots = shouldEncodeLeadingDots;
		}

		// Token: 0x0600351F RID: 13599 RVA: 0x000E1CA0 File Offset: 0x000E0CA0
		public override IAsyncResult BeginWrite(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (offset < 0 || offset >= buffer.Length)
			{
				throw new ArgumentOutOfRangeException("offset");
			}
			if (offset + count > buffer.Length)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			this.CheckBytes(buffer, offset, count);
			IAsyncResult result;
			if (this.shouldEncodeLeadingDots && !ServicePointManager.DisableSmtp7bitEncoding)
			{
				this.EncodeLines(buffer, offset, count);
				result = base.BeginWrite(this.WriteState.Buffer, 0, this.WriteState.Length, callback, state);
				this.WriteState.BufferFlushed();
			}
			else
			{
				result = base.BeginWrite(buffer, offset, count, callback, state);
			}
			return result;
		}

		// Token: 0x06003520 RID: 13600 RVA: 0x000E1D44 File Offset: 0x000E0D44
		public override void Write(byte[] buffer, int offset, int count)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (offset < 0 || offset >= buffer.Length)
			{
				throw new ArgumentOutOfRangeException("offset");
			}
			if (offset + count > buffer.Length)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			this.CheckBytes(buffer, offset, count);
			if (this.shouldEncodeLeadingDots && !ServicePointManager.DisableSmtp7bitEncoding)
			{
				this.EncodeLines(buffer, offset, count);
				base.Write(this.WriteState.Buffer, 0, this.WriteState.Length);
				this.WriteState.BufferFlushed();
				return;
			}
			base.Write(buffer, offset, count);
		}

		// Token: 0x06003521 RID: 13601 RVA: 0x000E1DDC File Offset: 0x000E0DDC
		private void EncodeLines(byte[] buffer, int offset, int count)
		{
			int num = offset;
			while (num < offset + count && num < buffer.Length)
			{
				if (!this.lastWriteEndedWithCr)
				{
					goto IL_35;
				}
				this.lastWriteEndedWithCr = false;
				if (buffer[num] != 10)
				{
					this.WriteState.Append(13);
					goto IL_35;
				}
				this.WriteState.AppendCRLF(false);
				IL_74:
				num++;
				continue;
				IL_35:
				if (this.WriteState.CurrentLineLength == 0 && buffer[num] == 46)
				{
					this.WriteState.Append(46);
				}
				if (buffer[num] == 13)
				{
					this.lastWriteEndedWithCr = true;
					goto IL_74;
				}
				this.WriteState.Append(buffer[num]);
				goto IL_74;
			}
		}

		// Token: 0x06003522 RID: 13602 RVA: 0x000E1E70 File Offset: 0x000E0E70
		private void CheckBytes(byte[] buffer, int offset, int count)
		{
			for (int i = count; i < offset + count; i++)
			{
				if (buffer[i] > 127)
				{
					throw new FormatException(SR.GetString("Mail7BitStreamInvalidCharacter"));
				}
			}
		}

		// Token: 0x040030BA RID: 12474
		private WriteStateInfoBase writeState;

		// Token: 0x040030BB RID: 12475
		private bool shouldEncodeLeadingDots;

		// Token: 0x040030BC RID: 12476
		private bool lastWriteEndedWithCr;
	}
}
