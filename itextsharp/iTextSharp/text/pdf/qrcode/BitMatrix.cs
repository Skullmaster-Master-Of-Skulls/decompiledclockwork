using System;
using System.Text;

namespace iTextSharp.text.pdf.qrcode
{
	// Token: 0x0200015E RID: 350
	public sealed class BitMatrix
	{
		// Token: 0x06000D1E RID: 3358 RVA: 0x00048404 File Offset: 0x00047404
		public BitMatrix(int dimension) : this(dimension, dimension)
		{
		}

		// Token: 0x06000D1F RID: 3359 RVA: 0x00048410 File Offset: 0x00047410
		public BitMatrix(int width, int height)
		{
			if (width < 1 || height < 1)
			{
				throw new ArgumentException("Both dimensions must be greater than 0");
			}
			this.width = width;
			this.height = height;
			int num = width >> 5;
			if ((width & 31) != 0)
			{
				num++;
			}
			this.rowSize = num;
			this.bits = new int[num * height];
		}

		// Token: 0x06000D20 RID: 3360 RVA: 0x00048468 File Offset: 0x00047468
		public bool Get(int x, int y)
		{
			int num = y * this.rowSize + (x >> 5);
			return (this.bits[num] >> x & 1) != 0;
		}

		// Token: 0x06000D21 RID: 3361 RVA: 0x0004849C File Offset: 0x0004749C
		public void Set(int x, int y)
		{
			int num = y * this.rowSize + (x >> 5);
			this.bits[num] |= 1 << x;
		}

		// Token: 0x06000D22 RID: 3362 RVA: 0x000484D8 File Offset: 0x000474D8
		public void Flip(int x, int y)
		{
			int num = y * this.rowSize + (x >> 5);
			this.bits[num] ^= 1 << x;
		}

		// Token: 0x06000D23 RID: 3363 RVA: 0x00048514 File Offset: 0x00047514
		public void Clear()
		{
			int num = this.bits.Length;
			for (int i = 0; i < num; i++)
			{
				this.bits[i] = 0;
			}
		}

		// Token: 0x06000D24 RID: 3364 RVA: 0x00048540 File Offset: 0x00047540
		public void SetRegion(int left, int top, int width, int height)
		{
			if (top < 0 || left < 0)
			{
				throw new ArgumentException("Left and top must be nonnegative");
			}
			if (height < 1 || width < 1)
			{
				throw new ArgumentException("Height and width must be at least 1");
			}
			int num = left + width;
			int num2 = top + height;
			if (num2 > this.height || num > this.width)
			{
				throw new ArgumentException("The region must fit inside the matrix");
			}
			for (int i = top; i < num2; i++)
			{
				int num3 = i * this.rowSize;
				for (int j = left; j < num; j++)
				{
					this.bits[num3 + (j >> 5)] |= 1 << j;
				}
			}
		}

		// Token: 0x06000D25 RID: 3365 RVA: 0x000485E8 File Offset: 0x000475E8
		public BitArray GetRow(int y, BitArray row)
		{
			if (row == null || row.GetSize() < this.width)
			{
				row = new BitArray(this.width);
			}
			int num = y * this.rowSize;
			for (int i = 0; i < this.rowSize; i++)
			{
				row.SetBulk(i << 5, this.bits[num + i]);
			}
			return row;
		}

		// Token: 0x06000D26 RID: 3366 RVA: 0x00048641 File Offset: 0x00047641
		public int GetWidth()
		{
			return this.width;
		}

		// Token: 0x06000D27 RID: 3367 RVA: 0x00048649 File Offset: 0x00047649
		public int GetHeight()
		{
			return this.height;
		}

		// Token: 0x06000D28 RID: 3368 RVA: 0x00048651 File Offset: 0x00047651
		public int GetDimension()
		{
			if (this.width != this.height)
			{
				throw new InvalidOperationException("Can't call GetDimension() on a non-square matrix");
			}
			return this.width;
		}

		// Token: 0x06000D29 RID: 3369 RVA: 0x00048674 File Offset: 0x00047674
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(this.height * (this.width + 1));
			for (int i = 0; i < this.height; i++)
			{
				for (int j = 0; j < this.width; j++)
				{
					stringBuilder.Append(this.Get(j, i) ? "X " : "  ");
				}
				stringBuilder.Append('\n');
			}
			return stringBuilder.ToString();
		}

		// Token: 0x040009D3 RID: 2515
		public int width;

		// Token: 0x040009D4 RID: 2516
		public int height;

		// Token: 0x040009D5 RID: 2517
		public int rowSize;

		// Token: 0x040009D6 RID: 2518
		public int[] bits;
	}
}
