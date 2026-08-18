using System;

namespace iTextSharp.text.pdf.hyphenation
{
	// Token: 0x0200063D RID: 1597
	public class ByteVector
	{
		// Token: 0x06003603 RID: 13827 RVA: 0x0014F6EC File Offset: 0x0014E6EC
		public ByteVector() : this(ByteVector.DEFAULT_BLOCK_SIZE)
		{
		}

		// Token: 0x06003604 RID: 13828 RVA: 0x0014F6F9 File Offset: 0x0014E6F9
		public ByteVector(int capacity)
		{
			if (capacity > 0)
			{
				this.BLOCK_SIZE = capacity;
			}
			else
			{
				this.BLOCK_SIZE = ByteVector.DEFAULT_BLOCK_SIZE;
			}
			this.arr = new byte[this.BLOCK_SIZE];
			this.n = 0;
		}

		// Token: 0x06003605 RID: 13829 RVA: 0x0014F731 File Offset: 0x0014E731
		public ByteVector(byte[] a)
		{
			this.BLOCK_SIZE = ByteVector.DEFAULT_BLOCK_SIZE;
			this.arr = a;
			this.n = 0;
		}

		// Token: 0x06003606 RID: 13830 RVA: 0x0014F752 File Offset: 0x0014E752
		public ByteVector(byte[] a, int capacity)
		{
			if (capacity > 0)
			{
				this.BLOCK_SIZE = capacity;
			}
			else
			{
				this.BLOCK_SIZE = ByteVector.DEFAULT_BLOCK_SIZE;
			}
			this.arr = a;
			this.n = 0;
		}

		// Token: 0x1700095B RID: 2395
		// (get) Token: 0x06003607 RID: 13831 RVA: 0x0014F780 File Offset: 0x0014E780
		public byte[] Arr
		{
			get
			{
				return this.arr;
			}
		}

		// Token: 0x1700095C RID: 2396
		// (get) Token: 0x06003608 RID: 13832 RVA: 0x0014F788 File Offset: 0x0014E788
		public int Length
		{
			get
			{
				return this.n;
			}
		}

		// Token: 0x1700095D RID: 2397
		// (get) Token: 0x06003609 RID: 13833 RVA: 0x0014F790 File Offset: 0x0014E790
		public int Capacity
		{
			get
			{
				return this.arr.Length;
			}
		}

		// Token: 0x1700095E RID: 2398
		public byte this[int index]
		{
			get
			{
				return this.arr[index];
			}
			set
			{
				this.arr[index] = value;
			}
		}

		// Token: 0x0600360C RID: 13836 RVA: 0x0014F7B0 File Offset: 0x0014E7B0
		public int Alloc(int size)
		{
			int result = this.n;
			int num = this.arr.Length;
			if (this.n + size >= num)
			{
				byte[] destinationArray = new byte[num + this.BLOCK_SIZE];
				Array.Copy(this.arr, 0, destinationArray, 0, num);
				this.arr = destinationArray;
			}
			this.n += size;
			return result;
		}

		// Token: 0x0600360D RID: 13837 RVA: 0x0014F80C File Offset: 0x0014E80C
		public void TrimToSize()
		{
			if (this.n < this.arr.Length)
			{
				byte[] destinationArray = new byte[this.n];
				Array.Copy(this.arr, 0, destinationArray, 0, this.n);
				this.arr = destinationArray;
			}
		}

		// Token: 0x0400244C RID: 9292
		private static int DEFAULT_BLOCK_SIZE = 2048;

		// Token: 0x0400244D RID: 9293
		private int BLOCK_SIZE;

		// Token: 0x0400244E RID: 9294
		private byte[] arr;

		// Token: 0x0400244F RID: 9295
		private int n;
	}
}
