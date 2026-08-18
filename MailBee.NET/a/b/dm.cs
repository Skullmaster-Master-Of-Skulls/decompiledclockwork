using System;
using System.Collections;
using System.Globalization;
using System.Text;

namespace a.b
{
	// Token: 0x02000325 RID: 805
	internal abstract class dm
	{
		// Token: 0x06001D07 RID: 7431 RVA: 0x0007DDEE File Offset: 0x0007CDEE
		public dm()
		{
		}

		// Token: 0x06001D08 RID: 7432
		public abstract void iu(string A_0);

		// Token: 0x06001D09 RID: 7433
		public abstract void iv(int A_0, object A_1);

		// Token: 0x06001D0A RID: 7434
		public abstract void i4(int A_0, object A_1, Exception A_2);

		// Token: 0x06001D0B RID: 7435
		public abstract bool iw(int A_0);

		// Token: 0x06001D0C RID: 7436 RVA: 0x0007DDF6 File Offset: 0x0007CDF6
		public virtual void ix(int A_0, object A_1, object A_2)
		{
			if (this.iw(A_0))
			{
				this.iv(A_0, new StringBuilder(32).Append(A_1).Append(A_2));
			}
		}

		// Token: 0x06001D0D RID: 7437 RVA: 0x0007DE1B File Offset: 0x0007CE1B
		public virtual void iy(int A_0, object A_1, object A_2, object A_3)
		{
			if (this.iw(A_0))
			{
				this.iv(A_0, new StringBuilder(48).Append(A_1).Append(A_2).Append(A_3));
			}
		}

		// Token: 0x06001D0E RID: 7438 RVA: 0x0007DE47 File Offset: 0x0007CE47
		public virtual void iz(int A_0, object A_1, object A_2, object A_3, object A_4)
		{
			if (this.iw(A_0))
			{
				this.iv(A_0, new StringBuilder(64).Append(A_1).Append(A_2).Append(A_3).Append(A_4));
			}
		}

		// Token: 0x06001D0F RID: 7439 RVA: 0x0007DE7A File Offset: 0x0007CE7A
		public virtual void i0(int A_0, object A_1, object A_2, object A_3, object A_4, object A_5)
		{
			if (this.iw(A_0))
			{
				this.iv(A_0, new StringBuilder(80).Append(A_1).Append(A_2).Append(A_3).Append(A_4).Append(A_5));
			}
		}

		// Token: 0x06001D10 RID: 7440 RVA: 0x0007DEB4 File Offset: 0x0007CEB4
		public virtual void i1(int A_0, object A_1, object A_2, object A_3, object A_4, object A_5, object A_6)
		{
			if (this.iw(A_0))
			{
				this.iv(A_0, new StringBuilder(96).Append(A_1).Append(A_2).Append(A_3).Append(A_4).Append(A_5).Append(A_6));
			}
		}

		// Token: 0x06001D11 RID: 7441 RVA: 0x0007DF00 File Offset: 0x0007CF00
		public virtual void i2(int A_0, object A_1, object A_2, object A_3, object A_4, object A_5, object A_6, object A_7)
		{
			if (this.iw(A_0))
			{
				this.iv(A_0, new StringBuilder(112).Append(A_1).Append(A_2).Append(A_3).Append(A_4).Append(A_5).Append(A_6).Append(A_7));
			}
		}

		// Token: 0x06001D12 RID: 7442 RVA: 0x0007DF54 File Offset: 0x0007CF54
		public virtual void i3(int A_0, object A_1, object A_2, object A_3, object A_4, object A_5, object A_6, object A_7, object A_8)
		{
			if (this.iw(A_0))
			{
				this.iv(A_0, new StringBuilder(128).Append(A_1).Append(A_2).Append(A_3).Append(A_4).Append(A_5).Append(A_6).Append(A_7).Append(A_8));
			}
		}

		// Token: 0x06001D13 RID: 7443 RVA: 0x0007DFB1 File Offset: 0x0007CFB1
		public virtual void a(int A_0, Exception A_1)
		{
			this.i4(A_0, null, A_1);
		}

		// Token: 0x06001D14 RID: 7444 RVA: 0x0007DFBC File Offset: 0x0007CFBC
		public virtual void i5(int A_0, object A_1, object A_2, Exception A_3)
		{
			if (this.iw(A_0))
			{
				this.i4(A_0, new StringBuilder(32).Append(A_1).Append(A_2), A_3);
			}
		}

		// Token: 0x06001D15 RID: 7445 RVA: 0x0007DFE3 File Offset: 0x0007CFE3
		public virtual void i6(int A_0, object A_1, object A_2, object A_3, Exception A_4)
		{
			if (this.iw(A_0))
			{
				this.i4(A_0, new StringBuilder(48).Append(A_1).Append(A_2).Append(A_3), A_4);
			}
		}

		// Token: 0x06001D16 RID: 7446 RVA: 0x0007E011 File Offset: 0x0007D011
		public virtual void i7(int A_0, object A_1, object A_2, object A_3, object A_4, Exception A_5)
		{
			if (this.iw(A_0))
			{
				this.i4(A_0, new StringBuilder(64).Append(A_1).Append(A_2).Append(A_3).Append(A_4), A_5);
			}
		}

		// Token: 0x06001D17 RID: 7447 RVA: 0x0007E046 File Offset: 0x0007D046
		public virtual void i8(int A_0, object A_1, object A_2, object A_3, object A_4, object A_5, Exception A_6)
		{
			if (this.iw(A_0))
			{
				this.i4(A_0, new StringBuilder(80).Append(A_1).Append(A_2).Append(A_3).Append(A_4).Append(A_5), A_6);
			}
		}

		// Token: 0x06001D18 RID: 7448 RVA: 0x0007E084 File Offset: 0x0007D084
		public virtual void i9(int A_0, object A_1, object A_2, object A_3, object A_4, object A_5, object A_6, Exception A_7)
		{
			if (this.iw(A_0))
			{
				this.i4(A_0, new StringBuilder(96).Append(A_1).Append(A_2).Append(A_3).Append(A_4).Append(A_5).Append(A_6), A_7);
			}
		}

		// Token: 0x06001D19 RID: 7449 RVA: 0x0007E0D4 File Offset: 0x0007D0D4
		public virtual void ja(int A_0, object A_1, object A_2, object A_3, object A_4, object A_5, object A_6, object A_7, Exception A_8)
		{
			if (this.iw(A_0))
			{
				this.i4(A_0, new StringBuilder(112).Append(A_1).Append(A_2).Append(A_3).Append(A_4).Append(A_5).Append(A_6).Append(A_7), A_8);
			}
		}

		// Token: 0x06001D1A RID: 7450 RVA: 0x0007E12C File Offset: 0x0007D12C
		public virtual void jb(int A_0, object A_1, object A_2, object A_3, object A_4, object A_5, object A_6, object A_7, object A_8, Exception A_9)
		{
			if (this.iw(A_0))
			{
				this.i4(A_0, new StringBuilder(128).Append(A_1).Append(A_2).Append(A_3).Append(A_4).Append(A_5).Append(A_6).Append(A_7).Append(A_8), A_9);
			}
		}

		// Token: 0x06001D1B RID: 7451 RVA: 0x0007E18B File Offset: 0x0007D18B
		public virtual void jc(int A_0, string A_1, object A_2)
		{
			this.a(A_0, A_1, new object[]
			{
				A_2
			});
		}

		// Token: 0x06001D1C RID: 7452 RVA: 0x0007E19F File Offset: 0x0007D19F
		public virtual void jd(int A_0, string A_1, object A_2, object A_3)
		{
			this.a(A_0, A_1, new object[]
			{
				A_2,
				A_3
			});
		}

		// Token: 0x06001D1D RID: 7453 RVA: 0x0007E1B8 File Offset: 0x0007D1B8
		public virtual void je(int A_0, string A_1, object A_2, object A_3, object A_4)
		{
			this.a(A_0, A_1, new object[]
			{
				A_2,
				A_3,
				A_4
			});
		}

		// Token: 0x06001D1E RID: 7454 RVA: 0x0007E1D6 File Offset: 0x0007D1D6
		public virtual void jf(int A_0, string A_1, object A_2, object A_3, object A_4, object A_5)
		{
			this.a(A_0, A_1, new object[]
			{
				A_2,
				A_3,
				A_4,
				A_5
			});
		}

		// Token: 0x06001D1F RID: 7455 RVA: 0x0007E1FC File Offset: 0x0007D1FC
		private void a(int A_0, string A_1, object[] A_2)
		{
			if (this.iw(A_0))
			{
				object[] array = (object[])this.a(A_2);
				if (array[array.Length - 1].GetType() == typeof(Exception))
				{
					this.i4(A_0, string.Format(CultureInfo.InvariantCulture, A_1, array), (Exception)array[array.Length - 1]);
					return;
				}
				this.iv(A_0, string.Format(CultureInfo.InvariantCulture, A_1, array));
			}
		}

		// Token: 0x06001D20 RID: 7456 RVA: 0x0007E270 File Offset: 0x0007D270
		private Array a(object[] A_0)
		{
			ArrayList arrayList = new ArrayList();
			for (int i = 0; i < A_0.Length; i++)
			{
				arrayList.AddRange(this.a(A_0[i]));
			}
			return arrayList.ToArray();
		}

		// Token: 0x06001D21 RID: 7457 RVA: 0x0007E2A8 File Offset: 0x0007D2A8
		private ArrayList a(object A_0)
		{
			ArrayList arrayList = new ArrayList();
			if (A_0.GetType() == typeof(char[]))
			{
				byte[] array = (byte[])A_0;
				for (int i = 0; i < array.Length; i++)
				{
					arrayList.Add(array[i]);
				}
			}
			if (A_0.GetType() == typeof(char[]))
			{
				char[] array2 = (char[])A_0;
				for (int j = 0; j < array2.Length; j++)
				{
					arrayList.Add(array2[j]);
				}
			}
			else if (A_0.GetType() == typeof(short[]))
			{
				short[] array3 = (short[])A_0;
				for (int k = 0; k < array3.Length; k++)
				{
					arrayList.Add(array3[k]);
				}
			}
			else if (A_0.GetType() == typeof(int[]))
			{
				int[] array4 = (int[])A_0;
				for (int l = 0; l < array4.Length; l++)
				{
					arrayList.Add(array4[l]);
				}
			}
			else if (A_0.GetType() == typeof(long[]))
			{
				long[] array5 = (long[])A_0;
				for (int m = 0; m < array5.Length; m++)
				{
					arrayList.Add(array5[m]);
				}
			}
			else if (A_0.GetType() == typeof(float[]))
			{
				float[] array6 = (float[])A_0;
				for (int n = 0; n < array6.Length; n++)
				{
					arrayList.Add(array6[n]);
				}
			}
			else if (A_0.GetType() == typeof(double[]))
			{
				double[] array7 = (double[])A_0;
				for (int num = 0; num < array7.Length; num++)
				{
					arrayList.Add(array7[num]);
				}
			}
			else if (A_0.GetType() == typeof(object[]))
			{
				object[] array8 = (object[])A_0;
				for (int num2 = 0; num2 < array8.Length; num2++)
				{
					arrayList.Add(array8[num2]);
				}
			}
			else
			{
				arrayList.Add(A_0);
			}
			return arrayList;
		}

		// Token: 0x04001372 RID: 4978
		public const int a = 1;

		// Token: 0x04001373 RID: 4979
		public const int b = 3;

		// Token: 0x04001374 RID: 4980
		public const int c = 5;

		// Token: 0x04001375 RID: 4981
		public const int d = 7;

		// Token: 0x04001376 RID: 4982
		public const int e = 9;
	}
}
