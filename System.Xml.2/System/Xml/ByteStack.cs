using System;

namespace System.Xml
{
	// Token: 0x0200006A RID: 106
	internal class ByteStack
	{
		// Token: 0x060003A4 RID: 932 RVA: 0x0000E894 File Offset: 0x0000CA94
		public ByteStack(int growthRate)
		{
			this.growthRate = growthRate;
			this.top = 0;
			this.stack = new byte[growthRate];
			this.size = growthRate;
		}

		// Token: 0x060003A5 RID: 933 RVA: 0x0000E8C0 File Offset: 0x0000CAC0
		public void Push(byte data)
		{
			if (this.size == this.top)
			{
				byte[] dst = new byte[this.size + this.growthRate];
				if (this.top > 0)
				{
					Buffer.BlockCopy(this.stack, 0, dst, 0, this.top);
				}
				this.stack = dst;
				this.size += this.growthRate;
			}
			byte[] array = this.stack;
			int num = this.top;
			this.top = num + 1;
			array[num] = data;
		}

		// Token: 0x060003A6 RID: 934 RVA: 0x0000E940 File Offset: 0x0000CB40
		public byte Pop()
		{
			if (this.top > 0)
			{
				byte[] array = this.stack;
				int num = this.top - 1;
				this.top = num;
				return array[num];
			}
			return 0;
		}

		// Token: 0x060003A7 RID: 935 RVA: 0x0000E970 File Offset: 0x0000CB70
		public byte Peek()
		{
			if (this.top > 0)
			{
				return this.stack[this.top - 1];
			}
			return 0;
		}

		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x060003A8 RID: 936 RVA: 0x0000E98C File Offset: 0x0000CB8C
		public int Length
		{
			get
			{
				return this.top;
			}
		}

		// Token: 0x040001B1 RID: 433
		private byte[] stack;

		// Token: 0x040001B2 RID: 434
		private int growthRate;

		// Token: 0x040001B3 RID: 435
		private int top;

		// Token: 0x040001B4 RID: 436
		private int size;
	}
}
