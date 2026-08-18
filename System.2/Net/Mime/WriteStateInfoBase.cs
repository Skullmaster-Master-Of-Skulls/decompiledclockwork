using System;

namespace System.Net.Mime
{
	// Token: 0x02000253 RID: 595
	internal class WriteStateInfoBase
	{
		// Token: 0x0600169E RID: 5790 RVA: 0x0007545C File Offset: 0x0007365C
		internal WriteStateInfoBase()
		{
			this.buffer = new byte[1024];
			this._header = new byte[0];
			this._footer = new byte[0];
			this._maxLineLength = EncodedStreamFactory.DefaultMaxLineLength;
			this._currentLineLength = 0;
			this._currentBufferUsed = 0;
		}

		// Token: 0x0600169F RID: 5791 RVA: 0x000754B0 File Offset: 0x000736B0
		internal WriteStateInfoBase(int bufferSize, byte[] header, byte[] footer, int maxLineLength) : this(bufferSize, header, footer, maxLineLength, 0)
		{
		}

		// Token: 0x060016A0 RID: 5792 RVA: 0x000754BE File Offset: 0x000736BE
		internal WriteStateInfoBase(int bufferSize, byte[] header, byte[] footer, int maxLineLength, int mimeHeaderLength)
		{
			this.buffer = new byte[bufferSize];
			this._header = header;
			this._footer = footer;
			this._maxLineLength = maxLineLength;
			this._currentLineLength = mimeHeaderLength;
			this._currentBufferUsed = 0;
		}

		// Token: 0x170004B9 RID: 1209
		// (get) Token: 0x060016A1 RID: 5793 RVA: 0x000754F7 File Offset: 0x000736F7
		internal int FooterLength
		{
			get
			{
				return this._footer.Length;
			}
		}

		// Token: 0x170004BA RID: 1210
		// (get) Token: 0x060016A2 RID: 5794 RVA: 0x00075501 File Offset: 0x00073701
		internal byte[] Footer
		{
			get
			{
				return this._footer;
			}
		}

		// Token: 0x170004BB RID: 1211
		// (get) Token: 0x060016A3 RID: 5795 RVA: 0x00075509 File Offset: 0x00073709
		internal byte[] Header
		{
			get
			{
				return this._header;
			}
		}

		// Token: 0x170004BC RID: 1212
		// (get) Token: 0x060016A4 RID: 5796 RVA: 0x00075511 File Offset: 0x00073711
		internal byte[] Buffer
		{
			get
			{
				return this.buffer;
			}
		}

		// Token: 0x170004BD RID: 1213
		// (get) Token: 0x060016A5 RID: 5797 RVA: 0x00075519 File Offset: 0x00073719
		internal int Length
		{
			get
			{
				return this._currentBufferUsed;
			}
		}

		// Token: 0x170004BE RID: 1214
		// (get) Token: 0x060016A6 RID: 5798 RVA: 0x00075521 File Offset: 0x00073721
		internal int CurrentLineLength
		{
			get
			{
				return this._currentLineLength;
			}
		}

		// Token: 0x060016A7 RID: 5799 RVA: 0x0007552C File Offset: 0x0007372C
		private void EnsureSpaceInBuffer(int moreBytes)
		{
			int num = this.Buffer.Length;
			while (this._currentBufferUsed + moreBytes >= num)
			{
				num *= 2;
			}
			if (num > this.Buffer.Length)
			{
				byte[] array = new byte[num];
				this.buffer.CopyTo(array, 0);
				this.buffer = array;
			}
		}

		// Token: 0x060016A8 RID: 5800 RVA: 0x0007557C File Offset: 0x0007377C
		internal void Append(byte aByte)
		{
			this.EnsureSpaceInBuffer(1);
			byte[] array = this.Buffer;
			int currentBufferUsed = this._currentBufferUsed;
			this._currentBufferUsed = currentBufferUsed + 1;
			array[currentBufferUsed] = aByte;
			this._currentLineLength++;
		}

		// Token: 0x060016A9 RID: 5801 RVA: 0x000755B7 File Offset: 0x000737B7
		internal void Append(params byte[] bytes)
		{
			this.EnsureSpaceInBuffer(bytes.Length);
			bytes.CopyTo(this.buffer, this.Length);
			this._currentLineLength += bytes.Length;
			this._currentBufferUsed += bytes.Length;
		}

		// Token: 0x060016AA RID: 5802 RVA: 0x000755F4 File Offset: 0x000737F4
		internal void AppendCRLF(bool includeSpace)
		{
			this.AppendFooter();
			this.Append(new byte[]
			{
				13,
				10
			});
			this._currentLineLength = 0;
			if (includeSpace)
			{
				this.Append(32);
			}
			this.AppendHeader();
		}

		// Token: 0x060016AB RID: 5803 RVA: 0x0007562A File Offset: 0x0007382A
		internal void AppendHeader()
		{
			if (this.Header != null && this.Header.Length != 0)
			{
				this.Append(this.Header);
			}
		}

		// Token: 0x060016AC RID: 5804 RVA: 0x00075649 File Offset: 0x00073849
		internal void AppendFooter()
		{
			if (this.Footer != null && this.Footer.Length != 0)
			{
				this.Append(this.Footer);
			}
		}

		// Token: 0x170004BF RID: 1215
		// (get) Token: 0x060016AD RID: 5805 RVA: 0x00075668 File Offset: 0x00073868
		internal int MaxLineLength
		{
			get
			{
				return this._maxLineLength;
			}
		}

		// Token: 0x060016AE RID: 5806 RVA: 0x00075670 File Offset: 0x00073870
		internal void Reset()
		{
			this._currentBufferUsed = 0;
			this._currentLineLength = 0;
		}

		// Token: 0x060016AF RID: 5807 RVA: 0x00075680 File Offset: 0x00073880
		internal void BufferFlushed()
		{
			this._currentBufferUsed = 0;
		}

		// Token: 0x04001769 RID: 5993
		protected byte[] _header;

		// Token: 0x0400176A RID: 5994
		protected byte[] _footer;

		// Token: 0x0400176B RID: 5995
		protected int _maxLineLength;

		// Token: 0x0400176C RID: 5996
		protected byte[] buffer;

		// Token: 0x0400176D RID: 5997
		protected int _currentLineLength;

		// Token: 0x0400176E RID: 5998
		protected int _currentBufferUsed;

		// Token: 0x0400176F RID: 5999
		protected const int defaultBufferSize = 1024;
	}
}
