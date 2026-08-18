using System;
using System.Collections;

namespace a.b
{
	// Token: 0x020002A2 RID: 674
	internal class iu
	{
		// Token: 0x060017AB RID: 6059 RVA: 0x0006BCB4 File Offset: 0x0006ACB4
		static iu()
		{
			Hashtable hashtable = new Hashtable();
			hashtable[0] = "VT_EMPTY";
			hashtable[1] = "VT_NULL";
			hashtable[2] = "VT_I2";
			hashtable[3] = "VT_I4";
			hashtable[4] = "VT_R4";
			hashtable[5] = "VT_R8";
			hashtable[6] = "VT_CY";
			hashtable[7] = "VT_DATE";
			hashtable[8] = "VT_BSTR";
			hashtable[9] = "VT_DISPATCH";
			hashtable[10] = "VT_ERROR";
			hashtable[11] = "VT_BOOL";
			hashtable[12] = "VT_VARIANT";
			hashtable[13] = "VT_UNKNOWN";
			hashtable[14] = "VT_DECIMAL";
			hashtable[16] = "VT_I1";
			hashtable[17] = "VT_UI1";
			hashtable[18] = "VT_UI2";
			hashtable[19] = "VT_UI4";
			hashtable[20] = "VT_I8";
			hashtable[21] = "VT_UI8";
			hashtable[22] = "VT_INT";
			hashtable[23] = "VT_UINT";
			hashtable[24] = "VT_VOID";
			hashtable[25] = "VT_HRESULT";
			hashtable[26] = "VT_PTR";
			hashtable[27] = "VT_SAFEARRAY";
			hashtable[28] = "VT_CARRAY";
			hashtable[29] = "VT_USERDEFINED";
			hashtable[30] = "VT_LPSTR";
			hashtable[31] = "VT_LPWSTR";
			hashtable[64] = "VT_FILETIME";
			hashtable[65] = "VT_BLOB";
			hashtable[66] = "VT_STREAM";
			hashtable[67] = "VT_STORAGE";
			hashtable[68] = "VT_STREAMED_OBJECT";
			hashtable[69] = "VT_STORED_OBJECT";
			hashtable[70] = "VT_BLOB_OBJECT";
			hashtable[71] = "VT_CF";
			hashtable[72] = "VT_CLSID";
			iu.aw = hashtable;
			Hashtable hashtable2 = new Hashtable();
			hashtable2[0] = 0;
			hashtable2[1] = -2;
			hashtable2[2] = 2;
			hashtable2[3] = 4;
			hashtable2[4] = 4;
			hashtable2[5] = 8;
			hashtable2[6] = -2;
			hashtable2[7] = -2;
			hashtable2[8] = -2;
			hashtable2[9] = -2;
			hashtable2[10] = -2;
			hashtable2[11] = -2;
			hashtable2[12] = -2;
			hashtable2[13] = -2;
			hashtable2[14] = -2;
			hashtable2[16] = -2;
			hashtable2[17] = -2;
			hashtable2[18] = -2;
			hashtable2[19] = -2;
			hashtable2[20] = -2;
			hashtable2[21] = -2;
			hashtable2[22] = -2;
			hashtable2[23] = -2;
			hashtable2[24] = -2;
			hashtable2[25] = -2;
			hashtable2[26] = -2;
			hashtable2[27] = -2;
			hashtable2[28] = -2;
			hashtable2[29] = -2;
			hashtable2[30] = -1;
			hashtable2[31] = -2;
			hashtable2[64] = 8;
			hashtable2[65] = -2;
			hashtable2[66] = -2;
			hashtable2[67] = -2;
			hashtable2[68] = -2;
			hashtable2[69] = -2;
			hashtable2[70] = -2;
			hashtable2[71] = -2;
			hashtable2[72] = -2;
			iu.ax = hashtable2;
		}

		// Token: 0x060017AC RID: 6060 RVA: 0x0006C2AC File Offset: 0x0006B2AC
		public static string b(long A_0)
		{
			string text = (string)iu.aw[A_0];
			if (text == null)
			{
				return "unknown variant type";
			}
			return text;
		}

		// Token: 0x060017AD RID: 6061 RVA: 0x0006C2DC File Offset: 0x0006B2DC
		public static int a(long A_0)
		{
			long num = (long)((int)A_0);
			if (iu.ax.Contains(num))
			{
				return -2;
			}
			return Convert.ToInt32((long)iu.ax[num]);
		}

		// Token: 0x04001173 RID: 4467
		public const int a = 0;

		// Token: 0x04001174 RID: 4468
		public const int b = 1;

		// Token: 0x04001175 RID: 4469
		public const int c = 2;

		// Token: 0x04001176 RID: 4470
		public const int d = 3;

		// Token: 0x04001177 RID: 4471
		public const int e = 4;

		// Token: 0x04001178 RID: 4472
		public const int f = 5;

		// Token: 0x04001179 RID: 4473
		public const int g = 6;

		// Token: 0x0400117A RID: 4474
		public const int h = 7;

		// Token: 0x0400117B RID: 4475
		public const int i = 8;

		// Token: 0x0400117C RID: 4476
		public const int j = 9;

		// Token: 0x0400117D RID: 4477
		public const int k = 10;

		// Token: 0x0400117E RID: 4478
		public const int l = 11;

		// Token: 0x0400117F RID: 4479
		public const int m = 12;

		// Token: 0x04001180 RID: 4480
		public const int n = 13;

		// Token: 0x04001181 RID: 4481
		public const int o = 14;

		// Token: 0x04001182 RID: 4482
		public const int p = 16;

		// Token: 0x04001183 RID: 4483
		public const int q = 17;

		// Token: 0x04001184 RID: 4484
		public const int r = 18;

		// Token: 0x04001185 RID: 4485
		public const int s = 19;

		// Token: 0x04001186 RID: 4486
		public const int t = 20;

		// Token: 0x04001187 RID: 4487
		public const int u = 21;

		// Token: 0x04001188 RID: 4488
		public const int v = 22;

		// Token: 0x04001189 RID: 4489
		public const int w = 23;

		// Token: 0x0400118A RID: 4490
		public const int x = 24;

		// Token: 0x0400118B RID: 4491
		public const int y = 25;

		// Token: 0x0400118C RID: 4492
		public const int z = 26;

		// Token: 0x0400118D RID: 4493
		public const int aa = 27;

		// Token: 0x0400118E RID: 4494
		public const int ab = 28;

		// Token: 0x0400118F RID: 4495
		public const int ac = 29;

		// Token: 0x04001190 RID: 4496
		public const int ad = 30;

		// Token: 0x04001191 RID: 4497
		public const int ae = 31;

		// Token: 0x04001192 RID: 4498
		public const int af = 64;

		// Token: 0x04001193 RID: 4499
		public const int ag = 65;

		// Token: 0x04001194 RID: 4500
		public const int ah = 66;

		// Token: 0x04001195 RID: 4501
		public const int ai = 67;

		// Token: 0x04001196 RID: 4502
		public const int aj = 68;

		// Token: 0x04001197 RID: 4503
		public const int ak = 69;

		// Token: 0x04001198 RID: 4504
		public const int al = 70;

		// Token: 0x04001199 RID: 4505
		public const int am = 71;

		// Token: 0x0400119A RID: 4506
		public const int an = 72;

		// Token: 0x0400119B RID: 4507
		public const int ao = 73;

		// Token: 0x0400119C RID: 4508
		public const int ap = 4096;

		// Token: 0x0400119D RID: 4509
		public const int aq = 8192;

		// Token: 0x0400119E RID: 4510
		public const int ar = 16384;

		// Token: 0x0400119F RID: 4511
		public const int @as = 32768;

		// Token: 0x040011A0 RID: 4512
		public const int at = 65535;

		// Token: 0x040011A1 RID: 4513
		public const int au = 4095;

		// Token: 0x040011A2 RID: 4514
		public const int av = 4095;

		// Token: 0x040011A3 RID: 4515
		private static IDictionary aw;

		// Token: 0x040011A4 RID: 4516
		private static IDictionary ax;

		// Token: 0x040011A5 RID: 4517
		public const int ay = -2;

		// Token: 0x040011A6 RID: 4518
		public const int az = -1;

		// Token: 0x040011A7 RID: 4519
		public const int a0 = 0;

		// Token: 0x040011A8 RID: 4520
		public const int a1 = 2;

		// Token: 0x040011A9 RID: 4521
		public const int a2 = 4;

		// Token: 0x040011AA RID: 4522
		public const int a3 = 8;
	}
}
