using System;

namespace iTextSharp.text.pdf.parser
{
	// Token: 0x02000163 RID: 355
	public class Vector
	{
		// Token: 0x06000D7B RID: 3451 RVA: 0x00049E84 File Offset: 0x00048E84
		public Vector(float x, float y, float z)
		{
			float[] array = new float[3];
			this.vals = array;
			base..ctor();
			this.vals[0] = x;
			this.vals[1] = y;
			this.vals[2] = z;
		}

		// Token: 0x1700029A RID: 666
		public float this[int index]
		{
			get
			{
				return this.vals[index];
			}
		}

		// Token: 0x06000D7D RID: 3453 RVA: 0x00049ECC File Offset: 0x00048ECC
		public Vector Cross(Matrix by)
		{
			float x = this.vals[0] * by[0] + this.vals[1] * by[3] + this.vals[2] * by[6];
			float y = this.vals[0] * by[1] + this.vals[1] * by[4] + this.vals[2] * by[7];
			float z = this.vals[0] * by[2] + this.vals[1] * by[5] + this.vals[2] * by[8];
			return new Vector(x, y, z);
		}

		// Token: 0x06000D7E RID: 3454 RVA: 0x00049F7C File Offset: 0x00048F7C
		public Vector Subtract(Vector v)
		{
			float x = this.vals[0] - v.vals[0];
			float y = this.vals[1] - v.vals[1];
			float z = this.vals[2] - v.vals[2];
			return new Vector(x, y, z);
		}

		// Token: 0x06000D7F RID: 3455 RVA: 0x00049FC8 File Offset: 0x00048FC8
		public Vector Cross(Vector with)
		{
			float x = this.vals[1] * with.vals[2] - this.vals[2] * with.vals[1];
			float y = this.vals[2] * with.vals[0] - this.vals[0] * with.vals[2];
			float z = this.vals[0] * with.vals[1] - this.vals[1] * with.vals[0];
			return new Vector(x, y, z);
		}

		// Token: 0x06000D80 RID: 3456 RVA: 0x0004A04C File Offset: 0x0004904C
		public Vector Normalize()
		{
			float length = this.Length;
			float x = this.vals[0] / length;
			float y = this.vals[1] / length;
			float z = this.vals[2] / length;
			return new Vector(x, y, z);
		}

		// Token: 0x06000D81 RID: 3457 RVA: 0x0004A08C File Offset: 0x0004908C
		public Vector Multiply(float by)
		{
			float x = this.vals[0] * by;
			float y = this.vals[1] * by;
			float z = this.vals[2] * by;
			return new Vector(x, y, z);
		}

		// Token: 0x06000D82 RID: 3458 RVA: 0x0004A0C2 File Offset: 0x000490C2
		public float Dot(Vector with)
		{
			return this.vals[0] * with.vals[0] + this.vals[1] * with.vals[1] + this.vals[2] * with.vals[2];
		}

		// Token: 0x1700029B RID: 667
		// (get) Token: 0x06000D83 RID: 3459 RVA: 0x0004A0F9 File Offset: 0x000490F9
		public float Length
		{
			get
			{
				return (float)Math.Sqrt((double)this.LengthSquared);
			}
		}

		// Token: 0x1700029C RID: 668
		// (get) Token: 0x06000D84 RID: 3460 RVA: 0x0004A108 File Offset: 0x00049108
		public float LengthSquared
		{
			get
			{
				return this.vals[0] * this.vals[0] + this.vals[1] * this.vals[1] + this.vals[2] * this.vals[2];
			}
		}

		// Token: 0x06000D85 RID: 3461 RVA: 0x0004A140 File Offset: 0x00049140
		public override string ToString()
		{
			return string.Concat(new object[]
			{
				this.vals[0],
				",",
				this.vals[1],
				",",
				this.vals[2]
			});
		}

		// Token: 0x06000D86 RID: 3462 RVA: 0x0004A19C File Offset: 0x0004919C
		public override bool Equals(object obj)
		{
			Vector vector = (Vector)obj;
			return vector.vals[0] == this.vals[0] && vector.vals[1] == this.vals[1] && vector.vals[2] == this.vals[2];
		}

		// Token: 0x06000D87 RID: 3463 RVA: 0x0004A1E8 File Offset: 0x000491E8
		public override int GetHashCode()
		{
			int num = 1;
			for (int i = 0; i < this.vals.Length; i++)
			{
				num = 31 * num + this.vals[i].GetHashCode();
			}
			return num;
		}

		// Token: 0x040009F5 RID: 2549
		public const int I1 = 0;

		// Token: 0x040009F6 RID: 2550
		public const int I2 = 1;

		// Token: 0x040009F7 RID: 2551
		public const int I3 = 2;

		// Token: 0x040009F8 RID: 2552
		private float[] vals;
	}
}
