using System;

namespace iTextSharp.text.pdf.parser
{
	// Token: 0x020004A5 RID: 1189
	public class Matrix
	{
		// Token: 0x06002836 RID: 10294 RVA: 0x000F285C File Offset: 0x000F185C
		public Matrix()
		{
		}

		// Token: 0x06002837 RID: 10295 RVA: 0x000F28A4 File Offset: 0x000F18A4
		public Matrix(float tx, float ty)
		{
			this.vals[6] = tx;
			this.vals[7] = ty;
		}

		// Token: 0x06002838 RID: 10296 RVA: 0x000F28FC File Offset: 0x000F18FC
		public Matrix(float a, float b, float c, float d, float e, float f)
		{
			this.vals[0] = a;
			this.vals[1] = b;
			this.vals[2] = 0f;
			this.vals[3] = c;
			this.vals[4] = d;
			this.vals[5] = 0f;
			this.vals[6] = e;
			this.vals[7] = f;
			this.vals[8] = 1f;
		}

		// Token: 0x17000701 RID: 1793
		public float this[int index]
		{
			get
			{
				return this.vals[index];
			}
		}

		// Token: 0x0600283A RID: 10298 RVA: 0x000F2994 File Offset: 0x000F1994
		public Matrix Multiply(Matrix by)
		{
			Matrix matrix = new Matrix();
			float[] array = this.vals;
			float[] array2 = by.vals;
			float[] array3 = matrix.vals;
			array3[0] = array[0] * array2[0] + array[1] * array2[3] + array[2] * array2[6];
			array3[1] = array[0] * array2[1] + array[1] * array2[4] + array[2] * array2[7];
			array3[2] = array[0] * array2[2] + array[1] * array2[5] + array[2] * array2[8];
			array3[3] = array[3] * array2[0] + array[4] * array2[3] + array[5] * array2[6];
			array3[4] = array[3] * array2[1] + array[4] * array2[4] + array[5] * array2[7];
			array3[5] = array[3] * array2[2] + array[4] * array2[5] + array[5] * array2[8];
			array3[6] = array[6] * array2[0] + array[7] * array2[3] + array[8] * array2[6];
			array3[7] = array[6] * array2[1] + array[7] * array2[4] + array[8] * array2[7];
			array3[8] = array[6] * array2[2] + array[7] * array2[5] + array[8] * array2[8];
			return matrix;
		}

		// Token: 0x0600283B RID: 10299 RVA: 0x000F2AA8 File Offset: 0x000F1AA8
		public Matrix Subtract(Matrix arg)
		{
			Matrix matrix = new Matrix();
			float[] array = this.vals;
			float[] array2 = arg.vals;
			float[] array3 = matrix.vals;
			array3[0] = array[0] - array2[0];
			array3[1] = array[1] - array2[1];
			array3[2] = array[2] - array2[2];
			array3[3] = array[3] - array2[3];
			array3[4] = array[4] - array2[4];
			array3[5] = array[5] - array2[5];
			array3[6] = array[6] - array2[6];
			array3[7] = array[7] - array2[7];
			array3[8] = array[8] - array2[8];
			return matrix;
		}

		// Token: 0x0600283C RID: 10300 RVA: 0x000F2B2C File Offset: 0x000F1B2C
		public float GetDeterminant()
		{
			return this.vals[0] * this.vals[4] * this.vals[8] + this.vals[1] * this.vals[5] * this.vals[6] + this.vals[2] * this.vals[3] * this.vals[7] - this.vals[0] * this.vals[5] * this.vals[7] - this.vals[1] * this.vals[3] * this.vals[8] - this.vals[2] * this.vals[4] * this.vals[6];
		}

		// Token: 0x0600283D RID: 10301 RVA: 0x000F2BDC File Offset: 0x000F1BDC
		public override bool Equals(object obj)
		{
			if (!(obj is Matrix))
			{
				return false;
			}
			Matrix matrix = (Matrix)obj;
			for (int i = 0; i < this.vals.Length; i++)
			{
				if (this.vals[i] != matrix.vals[i])
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600283E RID: 10302 RVA: 0x000F2C24 File Offset: 0x000F1C24
		public override int GetHashCode()
		{
			int num = 1;
			for (int i = 0; i < this.vals.Length; i++)
			{
				num = 31 * num + this.vals[i].GetHashCode();
			}
			return num;
		}

		// Token: 0x0600283F RID: 10303 RVA: 0x000F2C60 File Offset: 0x000F1C60
		public override string ToString()
		{
			return string.Concat(new object[]
			{
				this.vals[0],
				"\t",
				this.vals[1],
				"\t",
				this.vals[2],
				"\n",
				this.vals[3],
				"\t",
				this.vals[4],
				"\t",
				this.vals[2],
				"\n",
				this.vals[6],
				"\t",
				this.vals[7],
				"\t",
				this.vals[8]
			});
		}

		// Token: 0x04001B98 RID: 7064
		public const int I11 = 0;

		// Token: 0x04001B99 RID: 7065
		public const int I12 = 1;

		// Token: 0x04001B9A RID: 7066
		public const int I13 = 2;

		// Token: 0x04001B9B RID: 7067
		public const int I21 = 3;

		// Token: 0x04001B9C RID: 7068
		public const int I22 = 4;

		// Token: 0x04001B9D RID: 7069
		public const int I23 = 5;

		// Token: 0x04001B9E RID: 7070
		public const int I31 = 6;

		// Token: 0x04001B9F RID: 7071
		public const int I32 = 7;

		// Token: 0x04001BA0 RID: 7072
		public const int I33 = 8;

		// Token: 0x04001BA1 RID: 7073
		private float[] vals = new float[]
		{
			1f,
			0f,
			0f,
			0f,
			1f,
			0f,
			0f,
			0f,
			1f
		};
	}
}
