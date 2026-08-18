using System;
using System.Collections;

namespace a.b
{
	// Token: 0x02000244 RID: 580
	internal class e
	{
		// Token: 0x06001371 RID: 4977 RVA: 0x00057C14 File Offset: 0x00056C14
		public void b(int A_0)
		{
			this.c = A_0;
			byte[] array = new byte[32];
			array[8] = (byte)this.d;
			array[12] = (byte)this.c;
			array[16] = (byte)this.d;
			array[20] = (byte)this.c;
			byte[] array2 = array;
			this.a = array2;
		}

		// Token: 0x06001372 RID: 4978 RVA: 0x00057C64 File Offset: 0x00056C64
		public void a(int A_0)
		{
			this.d = A_0;
			byte[] array = new byte[32];
			array[8] = (byte)this.d;
			array[12] = (byte)this.c;
			array[16] = (byte)this.d;
			array[20] = (byte)this.c;
			byte[] array2 = array;
			this.a = array2;
		}

		// Token: 0x06001373 RID: 4979 RVA: 0x00057CB4 File Offset: 0x00056CB4
		public virtual byte[] du()
		{
			byte[] array = new byte[this.b.Count * 16 + 32];
			for (int i = 0; i < this.a.Length; i++)
			{
				array[i] = this.a[i];
			}
			int num = 0;
			while (this.b.Count != 0)
			{
				byte[] array2 = (byte[])this.b.Pop();
				for (int j = num * 16; j < array2.Length + num * 16; j++)
				{
					array[j + this.a.Length] = array2[j - num * 16];
				}
				num++;
			}
			return array;
		}

		// Token: 0x06001374 RID: 4980 RVA: 0x00057D4D File Offset: 0x00056D4D
		public e()
		{
			this.b(0);
			this.b = new Stack();
		}

		// Token: 0x06001375 RID: 4981 RVA: 0x00057D68 File Offset: 0x00056D68
		public void a(long A_0, long A_1, bool A_2, long A_3)
		{
			byte[] array = new byte[16];
			byte[] array2 = new byte[4];
			int num = 0;
			for (int i = array2.Length - 1; i >= 0; i--)
			{
				int num2 = (array2.Length - i - 1) * 8;
				array2[num++] = (byte)eo.a(A_0 & 255L << (num2 & 31), num2);
			}
			byte[] array3 = new byte[4];
			num = 0;
			for (int j = array3.Length - 1; j >= 0; j--)
			{
				int num3 = (array3.Length - j - 1) * 8;
				array3[num++] = (byte)eo.a(A_1 & 255L << (num3 & 31), num3);
			}
			if (A_1 != Convert.ToInt64("0040", 16))
			{
				byte[] array4 = new byte[2];
				if (A_2)
				{
					array4[0] = 6;
					array4[1] = 0;
				}
				else
				{
					array4[0] = 2;
					array4[1] = 0;
				}
				byte[] array5 = new byte[4];
				num = 0;
				for (int k = array5.Length - 1; k >= 0; k--)
				{
					int num4 = (array5.Length - k - 1) * 8;
					array5[num++] = (byte)eo.a(A_3 & 255L << (num4 & 31), num4);
				}
				array[1] = array3[1];
				array[0] = array3[0];
				array[3] = array2[1];
				array[2] = array2[0];
				array[4] = array4[0];
				array[5] = array4[1];
				array[6] = 0;
				array[7] = 0;
				array[8] = array5[0];
				array[9] = array5[1];
				array[10] = array5[2];
				array[11] = array5[3];
				array[12] = 3;
				array[13] = 0;
				array[14] = 0;
				array[15] = 0;
				this.b.Push(array);
				return;
			}
			byte[] array6 = new byte[2];
			if (A_2)
			{
				array6[0] = 6;
				array6[1] = 0;
			}
			else
			{
				array6[0] = 2;
				array6[1] = 0;
			}
			byte[] array7 = new byte[8];
			for (int l = 0; l < 8; l++)
			{
				array7[l] = 0;
			}
			int m = 0;
			int num5 = 24;
			while (m < 8)
			{
				byte b = (byte)(255L & A_3 >> num5);
				array7[m] = b;
				m++;
				num5 -= 8;
			}
			array[1] = array3[1];
			array[0] = array3[0];
			array[3] = array2[1];
			array[2] = array2[0];
			array[4] = array6[0];
			array[5] = array6[1];
			array[6] = 0;
			array[7] = 0;
			array[8] = array7[3];
			array[9] = array7[2];
			array[10] = array7[1];
			array[11] = array7[0];
			array[12] = array7[7];
			array[13] = array7[6];
			array[14] = array7[5];
			array[15] = array7[4];
			this.b.Push(array);
		}

		// Token: 0x06001376 RID: 4982 RVA: 0x00057FE0 File Offset: 0x00056FE0
		public void a(long A_0, bool A_1)
		{
			byte[] array = new byte[16];
			byte[] array2 = new byte[4];
			int num = 0;
			for (int i = array2.Length - 1; i >= 0; i--)
			{
				int num2 = (array2.Length - i - 1) * 8;
				array2[num++] = (byte)eo.a(A_0 & 255L << (num2 & 31), num2);
			}
			int num3 = 0;
			array[num3++] = 11;
			array[num3++] = 0;
			array[num3++] = array2[0];
			array[num3++] = array2[1];
			array[num3++] = 2;
			array[num3++] = 0;
			array[num3++] = 0;
			array[num3++] = 0;
			if (A_1)
			{
				array[num3++] = 1;
			}
			else
			{
				array[num3++] = 0;
			}
			array[num3++] = 0;
			array[num3++] = 0;
			array[num3++] = 0;
			array[num3++] = 0;
			array[num3++] = 0;
			array[num3++] = 0;
			array[num3++] = 0;
			this.b.Push(array);
		}

		// Token: 0x06001377 RID: 4983 RVA: 0x000580DC File Offset: 0x000570DC
		public void a(long A_0, long A_1, bool A_2, long A_3, byte A_4, byte A_5)
		{
			byte[] array = new byte[16];
			byte[] array2 = new byte[4];
			int num = 0;
			for (int i = array2.Length - 1; i >= 0; i--)
			{
				int num2 = (array2.Length - i - 1) * 8;
				array2[num++] = (byte)eo.a(A_0 & 255L << (num2 & 31), num2);
			}
			byte[] array3 = new byte[4];
			num = 0;
			for (int j = array3.Length - 1; j >= 0; j--)
			{
				int num3 = (array3.Length - j - 1) * 8;
				array3[num++] = (byte)eo.a(A_1 & 255L << (num3 & 31), num3);
			}
			if (A_1 != Convert.ToInt64("0040", 16))
			{
				byte[] array4 = new byte[2];
				if (A_2)
				{
					array4[0] = A_4;
					array4[1] = 0;
				}
				else
				{
					array4[0] = A_4;
					array4[1] = 0;
				}
				byte[] array5 = new byte[4];
				num = 0;
				for (int k = array5.Length - 1; k >= 0; k--)
				{
					int num4 = (array5.Length - k - 1) * 8;
					array5[num++] = (byte)eo.a(A_3 & 255L << (num4 & 31), num4);
				}
				array[1] = array3[1];
				array[0] = array3[0];
				array[3] = array2[1];
				array[2] = array2[0];
				array[4] = array4[0];
				array[5] = array4[1];
				array[6] = 0;
				array[7] = 0;
				array[8] = array5[0];
				array[9] = array5[1];
				array[10] = array5[2];
				array[11] = array5[3];
				array[12] = A_5;
				array[13] = 0;
				array[14] = 0;
				array[15] = 0;
				this.b.Push(array);
				return;
			}
			byte[] array6 = new byte[2];
			if (A_2)
			{
				array6[0] = A_4;
				array6[1] = 0;
			}
			else
			{
				array6[0] = A_4;
				array6[1] = 0;
			}
			byte[] array7 = new byte[8];
			for (int l = 0; l < 8; l++)
			{
				array7[l] = 0;
			}
			int m = 0;
			int num5 = 24;
			while (m < 8)
			{
				byte b = (byte)(255L & A_3 >> num5);
				array7[m] = b;
				m++;
				num5 -= 8;
			}
			array[1] = array3[1];
			array[0] = array3[0];
			array[3] = array2[1];
			array[2] = array2[0];
			array[4] = array6[0];
			array[5] = array6[1];
			array[6] = 0;
			array[7] = 0;
			array[8] = array7[3];
			array[9] = array7[2];
			array[10] = array7[1];
			array[11] = array7[0];
			array[12] = array7[7];
			array[13] = array7[6];
			array[14] = array7[5];
			array[15] = array7[4];
			this.b.Push(array);
		}

		// Token: 0x06001378 RID: 4984 RVA: 0x0005835C File Offset: 0x0005735C
		public void a(long A_0, long A_1, bool A_2, ulong A_3)
		{
			byte[] array = new byte[16];
			byte[] array2 = new byte[4];
			int num = 0;
			for (int i = array2.Length - 1; i >= 0; i--)
			{
				int num2 = (array2.Length - i - 1) * 8;
				array2[num++] = (byte)eo.a(A_0 & 255L << (num2 & 31), num2);
			}
			byte[] array3 = new byte[4];
			num = 0;
			for (int j = array3.Length - 1; j >= 0; j--)
			{
				int num3 = (array3.Length - j - 1) * 8;
				array3[num++] = (byte)eo.a(A_1 & 255L << (num3 & 31), num3);
			}
			byte[] array4 = new byte[2];
			if (A_2)
			{
				array4[0] = 6;
				array4[1] = 0;
			}
			else
			{
				array4[0] = 2;
				array4[1] = 0;
			}
			byte[] array5 = new byte[8];
			for (int k = 0; k < 8; k++)
			{
				array5[k] = 0;
			}
			int l = 0;
			int num4 = 24;
			while (l < 8)
			{
				byte b = (byte)(255UL & A_3 >> num4);
				array5[l] = b;
				l++;
				num4 -= 8;
			}
			array[1] = array3[1];
			array[0] = array3[0];
			array[3] = array2[1];
			array[2] = array2[0];
			array[4] = array4[0];
			array[5] = array4[1];
			array[6] = 0;
			array[7] = 0;
			array[8] = array5[3];
			array[9] = array5[2];
			array[10] = array5[1];
			array[11] = array5[0];
			array[12] = array5[7];
			array[13] = array5[6];
			array[14] = array5[5];
			array[15] = array5[4];
			this.b.Push(array);
		}

		// Token: 0x06001379 RID: 4985 RVA: 0x000584E8 File Offset: 0x000574E8
		public void a(long A_0)
		{
			byte[] array = new byte[4];
			int num = 0;
			for (int i = array.Length - 1; i >= 0; i--)
			{
				int num2 = (array.Length - i - 1) * 8;
				array[num++] = (byte)eo.a(A_0 & 255L << (num2 & 31), num2);
			}
			int count = this.b.Count;
			Stack stack = new Stack();
			for (int j = 0; j < count; j++)
			{
				byte[] array2 = (byte[])this.b.Pop();
				if (array2[2] != array[0] || array2[3] != array[1])
				{
					stack.Push(array2);
				}
			}
			this.b = stack;
		}

		// Token: 0x04000F95 RID: 3989
		protected internal byte[] a;

		// Token: 0x04000F96 RID: 3990
		protected internal Stack b;

		// Token: 0x04000F97 RID: 3991
		private int c;

		// Token: 0x04000F98 RID: 3992
		private int d;
	}
}
