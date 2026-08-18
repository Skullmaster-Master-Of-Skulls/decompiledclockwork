using System;

namespace System.Net
{
	// Token: 0x02000682 RID: 1666
	internal class BufferBuilder
	{
		// Token: 0x06003398 RID: 13208 RVA: 0x000D9EB8 File Offset: 0x000D8EB8
		internal BufferBuilder() : this(256)
		{
		}

		// Token: 0x06003399 RID: 13209 RVA: 0x000D9EC5 File Offset: 0x000D8EC5
		internal BufferBuilder(int initialSize)
		{
			this.buffer = new byte[initialSize];
		}

		// Token: 0x0600339A RID: 13210 RVA: 0x000D9EDC File Offset: 0x000D8EDC
		private void EnsureBuffer(int count)
		{
			if (count > this.buffer.Length - this.offset)
			{
				byte[] dst = new byte[(this.buffer.Length * 2 > this.buffer.Length + count) ? (this.buffer.Length * 2) : (this.buffer.Length + count)];
				Buffer.BlockCopy(this.buffer, 0, dst, 0, this.offset);
				this.buffer = dst;
			}
		}

		// Token: 0x0600339B RID: 13211 RVA: 0x000D9F48 File Offset: 0x000D8F48
		internal void Append(byte value)
		{
			this.EnsureBuffer(1);
			this.buffer[this.offset++] = value;
		}

		// Token: 0x0600339C RID: 13212 RVA: 0x000D9F75 File Offset: 0x000D8F75
		internal void Append(byte[] value)
		{
			this.Append(value, 0, value.Length);
		}

		// Token: 0x0600339D RID: 13213 RVA: 0x000D9F82 File Offset: 0x000D8F82
		internal void Append(byte[] value, int offset, int count)
		{
			this.EnsureBuffer(count);
			Buffer.BlockCopy(value, offset, this.buffer, this.offset, count);
			this.offset += count;
		}

		// Token: 0x0600339E RID: 13214 RVA: 0x000D9FAD File Offset: 0x000D8FAD
		internal void Append(string value)
		{
			this.Append(value, 0, value.Length);
		}

		// Token: 0x0600339F RID: 13215 RVA: 0x000D9FC0 File Offset: 0x000D8FC0
		internal void Append(string value, int offset, int count)
		{
			this.EnsureBuffer(count);
			for (int i = 0; i < count; i++)
			{
				char c = value[offset + i];
				if (c > 'ÿ')
				{
					throw new FormatException(SR.GetString("MailHeaderFieldInvalidCharacter"));
				}
				this.buffer[this.offset + i] = (byte)c;
			}
			this.offset += count;
		}

		// Token: 0x17000C1F RID: 3103
		// (get) Token: 0x060033A0 RID: 13216 RVA: 0x000DA021 File Offset: 0x000D9021
		internal int Length
		{
			get
			{
				return this.offset;
			}
		}

		// Token: 0x060033A1 RID: 13217 RVA: 0x000DA029 File Offset: 0x000D9029
		internal byte[] GetBuffer()
		{
			return this.buffer;
		}

		// Token: 0x060033A2 RID: 13218 RVA: 0x000DA031 File Offset: 0x000D9031
		internal void Reset()
		{
			this.offset = 0;
		}

		// Token: 0x04002FAC RID: 12204
		private byte[] buffer;

		// Token: 0x04002FAD RID: 12205
		private int offset;
	}
}
