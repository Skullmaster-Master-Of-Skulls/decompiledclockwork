using System;
using System.Text;

namespace System.Net.Mail
{
	// Token: 0x02000259 RID: 601
	internal class BufferBuilder
	{
		// Token: 0x060016F5 RID: 5877 RVA: 0x00076265 File Offset: 0x00074465
		internal BufferBuilder() : this(256)
		{
		}

		// Token: 0x060016F6 RID: 5878 RVA: 0x00076272 File Offset: 0x00074472
		internal BufferBuilder(int initialSize)
		{
			this.buffer = new byte[initialSize];
		}

		// Token: 0x060016F7 RID: 5879 RVA: 0x00076288 File Offset: 0x00074488
		private void EnsureBuffer(int count)
		{
			if (count > this.buffer.Length - this.offset)
			{
				byte[] dst = new byte[(this.buffer.Length * 2 > this.buffer.Length + count) ? (this.buffer.Length * 2) : (this.buffer.Length + count)];
				Buffer.BlockCopy(this.buffer, 0, dst, 0, this.offset);
				this.buffer = dst;
			}
		}

		// Token: 0x060016F8 RID: 5880 RVA: 0x000762F4 File Offset: 0x000744F4
		internal void Append(byte value)
		{
			this.EnsureBuffer(1);
			byte[] array = this.buffer;
			int num = this.offset;
			this.offset = num + 1;
			array[num] = value;
		}

		// Token: 0x060016F9 RID: 5881 RVA: 0x00076321 File Offset: 0x00074521
		internal void Append(byte[] value)
		{
			this.Append(value, 0, value.Length);
		}

		// Token: 0x060016FA RID: 5882 RVA: 0x0007632E File Offset: 0x0007452E
		internal void Append(byte[] value, int offset, int count)
		{
			this.EnsureBuffer(count);
			Buffer.BlockCopy(value, offset, this.buffer, this.offset, count);
			this.offset += count;
		}

		// Token: 0x060016FB RID: 5883 RVA: 0x00076359 File Offset: 0x00074559
		internal void Append(string value)
		{
			this.Append(value, false);
		}

		// Token: 0x060016FC RID: 5884 RVA: 0x00076363 File Offset: 0x00074563
		internal void Append(string value, bool allowUnicode)
		{
			if (string.IsNullOrEmpty(value))
			{
				return;
			}
			this.Append(value, 0, value.Length, allowUnicode);
		}

		// Token: 0x060016FD RID: 5885 RVA: 0x00076380 File Offset: 0x00074580
		internal void Append(string value, int offset, int count, bool allowUnicode)
		{
			if (allowUnicode)
			{
				byte[] bytes = Encoding.UTF8.GetBytes(value.ToCharArray(), offset, count);
				this.Append(bytes);
				return;
			}
			this.Append(value, offset, count);
		}

		// Token: 0x060016FE RID: 5886 RVA: 0x000763B8 File Offset: 0x000745B8
		internal void Append(string value, int offset, int count)
		{
			this.EnsureBuffer(count);
			for (int i = 0; i < count; i++)
			{
				char c = value[offset + i];
				if (c > 'ÿ')
				{
					throw new FormatException(SR.GetString("MailHeaderFieldInvalidCharacter", new object[]
					{
						c
					}));
				}
				this.buffer[this.offset + i] = (byte)c;
			}
			this.offset += count;
		}

		// Token: 0x170004CB RID: 1227
		// (get) Token: 0x060016FF RID: 5887 RVA: 0x00076428 File Offset: 0x00074628
		internal int Length
		{
			get
			{
				return this.offset;
			}
		}

		// Token: 0x06001700 RID: 5888 RVA: 0x00076430 File Offset: 0x00074630
		internal byte[] GetBuffer()
		{
			return this.buffer;
		}

		// Token: 0x06001701 RID: 5889 RVA: 0x00076438 File Offset: 0x00074638
		internal void Reset()
		{
			this.offset = 0;
		}

		// Token: 0x04001777 RID: 6007
		private byte[] buffer;

		// Token: 0x04001778 RID: 6008
		private int offset;
	}
}
