using System;
using System.Collections;

namespace a.b
{
	// Token: 0x020002A7 RID: 679
	internal class ch : Hashtable
	{
		// Token: 0x060017C1 RID: 6081 RVA: 0x0006CC9F File Offset: 0x0006BC9F
		public ch(int A_0, float A_1) : base(A_0, A_1)
		{
		}

		// Token: 0x060017C2 RID: 6082 RVA: 0x0006CCA9 File Offset: 0x0006BCA9
		public ch(IDictionary A_0) : base(A_0)
		{
		}

		// Token: 0x060017C3 RID: 6083 RVA: 0x0006CCB4 File Offset: 0x0006BCB4
		public object a(long A_0, string A_1)
		{
			this[A_0] = A_1;
			return A_1;
		}

		// Token: 0x060017C4 RID: 6084 RVA: 0x0006CCD1 File Offset: 0x0006BCD1
		public object a(long A_0)
		{
			return this[A_0];
		}

		// Token: 0x060017C5 RID: 6085 RVA: 0x0006CCE0 File Offset: 0x0006BCE0
		public static ch b()
		{
			if (ch.ak == null)
			{
				ch ch = new ch(18, 1f);
				ch.a(2L, "PID_TITLE");
				ch.a(3L, "PID_SUBJECT");
				ch.a(4L, "PID_AUTHOR");
				ch.a(5L, "PID_KEYWORDS");
				ch.a(6L, "PID_COMMENTS");
				ch.a(7L, "PID_TEMPLATE");
				ch.a(8L, "PID_LASTAUTHOR");
				ch.a(9L, "PID_REVNUMBER");
				ch.a(10L, "PID_EDITTIME");
				ch.a(11L, "PID_LASTPRINTED");
				ch.a(12L, "PID_Create_DTM");
				ch.a(13L, "PID_LASTSAVE_DTM");
				ch.a(14L, "PID_PAGECOUNT");
				ch.a(15L, "PID_WORDCOUNT");
				ch.a(16L, "PID_CHARCOUNT");
				ch.a(17L, "PID_THUMBNAIL");
				ch.a(18L, "PID_APPNAME");
				ch.a(19L, "PID_SECURITY");
				ch.ak = ch;
			}
			return ch.ak;
		}

		// Token: 0x060017C6 RID: 6086 RVA: 0x0006CE14 File Offset: 0x0006BE14
		public static ch a()
		{
			if (ch.al == null)
			{
				ch ch = new ch(17, 1f);
				ch.a(0L, "PID_DICTIONARY");
				ch.a(1L, "PID_CODEPAGE");
				ch.a(2L, "PID_CATEGORY");
				ch.a(3L, "PID_PRESFORMAT");
				ch.a(4L, "PID_BYTECOUNT");
				ch.a(5L, "PID_LINECOUNT");
				ch.a(6L, "PID_PARCOUNT");
				ch.a(7L, "PID_SLIDECOUNT");
				ch.a(8L, "PID_NOTECOUNT");
				ch.a(9L, "PID_HIDDENCOUNT");
				ch.a(10L, "PID_MMCLIPCOUNT");
				ch.a(11L, "PID_SCALE");
				ch.a(12L, "PID_HEADINGPAIR");
				ch.a(13L, "PID_DOCPARTS");
				ch.a(14L, "PID_MANAGER");
				ch.a(15L, "PID_COMPANY");
				ch.a(16L, "PID_LINKSDIRTY");
				ch.al = ch;
			}
			return ch.al;
		}

		// Token: 0x040011B4 RID: 4532
		public const int a = 2;

		// Token: 0x040011B5 RID: 4533
		public const int b = 3;

		// Token: 0x040011B6 RID: 4534
		public const int c = 4;

		// Token: 0x040011B7 RID: 4535
		public const int d = 5;

		// Token: 0x040011B8 RID: 4536
		public const int e = 6;

		// Token: 0x040011B9 RID: 4537
		public const int f = 7;

		// Token: 0x040011BA RID: 4538
		public const int g = 8;

		// Token: 0x040011BB RID: 4539
		public const int h = 9;

		// Token: 0x040011BC RID: 4540
		public const int i = 10;

		// Token: 0x040011BD RID: 4541
		public const int j = 11;

		// Token: 0x040011BE RID: 4542
		public const int k = 12;

		// Token: 0x040011BF RID: 4543
		public const int l = 13;

		// Token: 0x040011C0 RID: 4544
		public const int m = 14;

		// Token: 0x040011C1 RID: 4545
		public const int n = 15;

		// Token: 0x040011C2 RID: 4546
		public const int o = 16;

		// Token: 0x040011C3 RID: 4547
		public const int p = 17;

		// Token: 0x040011C4 RID: 4548
		public const int q = 18;

		// Token: 0x040011C5 RID: 4549
		public const int r = 19;

		// Token: 0x040011C6 RID: 4550
		public const int s = 0;

		// Token: 0x040011C7 RID: 4551
		public const int t = 1;

		// Token: 0x040011C8 RID: 4552
		public const int u = 2;

		// Token: 0x040011C9 RID: 4553
		public const int v = 3;

		// Token: 0x040011CA RID: 4554
		public const int w = 4;

		// Token: 0x040011CB RID: 4555
		public const int x = 5;

		// Token: 0x040011CC RID: 4556
		public const int y = 6;

		// Token: 0x040011CD RID: 4557
		public const int z = 7;

		// Token: 0x040011CE RID: 4558
		public const int aa = 8;

		// Token: 0x040011CF RID: 4559
		public const int ab = 9;

		// Token: 0x040011D0 RID: 4560
		public const int ac = 10;

		// Token: 0x040011D1 RID: 4561
		public const int ad = 11;

		// Token: 0x040011D2 RID: 4562
		public const int ae = 12;

		// Token: 0x040011D3 RID: 4563
		public const int af = 13;

		// Token: 0x040011D4 RID: 4564
		public const int ag = 14;

		// Token: 0x040011D5 RID: 4565
		public const int ah = 15;

		// Token: 0x040011D6 RID: 4566
		public const int ai = 16;

		// Token: 0x040011D7 RID: 4567
		public const int aj = 16;

		// Token: 0x040011D8 RID: 4568
		private static ch ak;

		// Token: 0x040011D9 RID: 4569
		private static ch al;
	}
}
