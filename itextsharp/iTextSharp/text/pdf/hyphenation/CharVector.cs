using System;

namespace iTextSharp.text.pdf.hyphenation
{
	// Token: 0x020003A1 RID: 929
	public class CharVector : ICloneable
	{
		// Token: 0x06002024 RID: 8228 RVA: 0x000BF888 File Offset: 0x000BE888
		public CharVector() : this(CharVector.DEFAULT_BLOCK_SIZE)
		{
		}

		// Token: 0x06002025 RID: 8229 RVA: 0x000BF895 File Offset: 0x000BE895
		public CharVector(int capacity)
		{
			if (capacity > 0)
			{
				this.BLOCK_SIZE = capacity;
			}
			else
			{
				this.BLOCK_SIZE = CharVector.DEFAULT_BLOCK_SIZE;
			}
			this.array = new char[this.BLOCK_SIZE];
			this.n = 0;
		}

		// Token: 0x06002026 RID: 8230 RVA: 0x000BF8CD File Offset: 0x000BE8CD
		public CharVector(char[] a)
		{
			this.BLOCK_SIZE = CharVector.DEFAULT_BLOCK_SIZE;
			this.array = a;
			this.n = a.Length;
		}

		// Token: 0x06002027 RID: 8231 RVA: 0x000BF8F0 File Offset: 0x000BE8F0
		public CharVector(char[] a, int capacity)
		{
			if (capacity > 0)
			{
				this.BLOCK_SIZE = capacity;
			}
			else
			{
				this.BLOCK_SIZE = CharVector.DEFAULT_BLOCK_SIZE;
			}
			this.array = a;
			this.n = a.Length;
		}

		// Token: 0x06002028 RID: 8232 RVA: 0x000BF920 File Offset: 0x000BE920
		public void Clear()
		{
			this.n = 0;
		}

		// Token: 0x06002029 RID: 8233 RVA: 0x000BF92C File Offset: 0x000BE92C
		public object Clone()
		{
			return new CharVector((char[])this.array.Clone(), this.BLOCK_SIZE)
			{
				n = this.n
			};
		}

		// Token: 0x1700057B RID: 1403
		// (get) Token: 0x0600202A RID: 8234 RVA: 0x000BF962 File Offset: 0x000BE962
		public char[] Arr
		{
			get
			{
				return this.array;
			}
		}

		// Token: 0x1700057C RID: 1404
		// (get) Token: 0x0600202B RID: 8235 RVA: 0x000BF96A File Offset: 0x000BE96A
		public int Length
		{
			get
			{
				return this.n;
			}
		}

		// Token: 0x1700057D RID: 1405
		// (get) Token: 0x0600202C RID: 8236 RVA: 0x000BF972 File Offset: 0x000BE972
		public int Capacity
		{
			get
			{
				return this.array.Length;
			}
		}

		// Token: 0x1700057E RID: 1406
		public char this[int index]
		{
			get
			{
				return this.array[index];
			}
			set
			{
				this.array[index] = value;
			}
		}

		// Token: 0x0600202F RID: 8239 RVA: 0x000BF994 File Offset: 0x000BE994
		public int Alloc(int size)
		{
			int result = this.n;
			int num = this.array.Length;
			if (this.n + size >= num)
			{
				char[] destinationArray = new char[num + this.BLOCK_SIZE];
				Array.Copy(this.array, 0, destinationArray, 0, num);
				this.array = destinationArray;
			}
			this.n += size;
			return result;
		}

		// Token: 0x06002030 RID: 8240 RVA: 0x000BF9F0 File Offset: 0x000BE9F0
		public void TrimToSize()
		{
			if (this.n < this.array.Length)
			{
				char[] destinationArray = new char[this.n];
				Array.Copy(this.array, 0, destinationArray, 0, this.n);
				this.array = destinationArray;
			}
		}

		// Token: 0x04001623 RID: 5667
		private static int DEFAULT_BLOCK_SIZE = 2048;

		// Token: 0x04001624 RID: 5668
		private int BLOCK_SIZE;

		// Token: 0x04001625 RID: 5669
		private char[] array;

		// Token: 0x04001626 RID: 5670
		private int n;
	}
}
