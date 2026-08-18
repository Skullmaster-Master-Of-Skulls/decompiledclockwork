using System;

namespace iTextSharp.text.pdf.qrcode
{
	// Token: 0x02000638 RID: 1592
	public sealed class ByteArray
	{
		// Token: 0x060035EA RID: 13802 RVA: 0x0014F30C File Offset: 0x0014E30C
		public ByteArray()
		{
			this.bytes = null;
			this.size = 0;
		}

		// Token: 0x060035EB RID: 13803 RVA: 0x0014F322 File Offset: 0x0014E322
		public ByteArray(int size)
		{
			this.bytes = new byte[size];
			this.size = size;
		}

		// Token: 0x060035EC RID: 13804 RVA: 0x0014F33D File Offset: 0x0014E33D
		public ByteArray(byte[] byteArray)
		{
			this.bytes = byteArray;
			this.size = this.bytes.Length;
		}

		// Token: 0x060035ED RID: 13805 RVA: 0x0014F35A File Offset: 0x0014E35A
		public int At(int index)
		{
			return (int)(this.bytes[index] & byte.MaxValue);
		}

		// Token: 0x060035EE RID: 13806 RVA: 0x0014F36A File Offset: 0x0014E36A
		public void Set(int index, int value)
		{
			this.bytes[index] = (byte)value;
		}

		// Token: 0x060035EF RID: 13807 RVA: 0x0014F376 File Offset: 0x0014E376
		public int Size()
		{
			return this.size;
		}

		// Token: 0x060035F0 RID: 13808 RVA: 0x0014F37E File Offset: 0x0014E37E
		public bool IsEmpty()
		{
			return this.size == 0;
		}

		// Token: 0x060035F1 RID: 13809 RVA: 0x0014F38C File Offset: 0x0014E38C
		public void AppendByte(int value)
		{
			if (this.size == 0 || this.size >= this.bytes.Length)
			{
				int capacity = Math.Max(32, this.size << 1);
				this.Reserve(capacity);
			}
			this.bytes[this.size] = (byte)value;
			this.size++;
		}

		// Token: 0x060035F2 RID: 13810 RVA: 0x0014F3E8 File Offset: 0x0014E3E8
		public void Reserve(int capacity)
		{
			if (this.bytes == null || this.bytes.Length < capacity)
			{
				byte[] destinationArray = new byte[capacity];
				if (this.bytes != null)
				{
					Array.Copy(this.bytes, 0, destinationArray, 0, this.bytes.Length);
				}
				this.bytes = destinationArray;
			}
		}

		// Token: 0x060035F3 RID: 13811 RVA: 0x0014F434 File Offset: 0x0014E434
		public void Set(byte[] source, int offset, int count)
		{
			this.bytes = new byte[count];
			this.size = count;
			for (int i = 0; i < count; i++)
			{
				this.bytes[i] = source[offset + i];
			}
		}

		// Token: 0x04002443 RID: 9283
		private const int INITIAL_SIZE = 32;

		// Token: 0x04002444 RID: 9284
		private byte[] bytes;

		// Token: 0x04002445 RID: 9285
		private int size;
	}
}
