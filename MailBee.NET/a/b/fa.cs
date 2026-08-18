using System;
using System.Resources;

namespace a.b
{
	// Token: 0x02000389 RID: 905
	internal sealed class fa : c4
	{
		// Token: 0x060020AB RID: 8363 RVA: 0x00087907 File Offset: 0x00086907
		public static string h(string A_0)
		{
			return c4.a(fa.a.GetString("ColorTableUnsupportedText"), new object[]
			{
				A_0
			});
		}

		// Token: 0x060020AC RID: 8364 RVA: 0x00087927 File Offset: 0x00086927
		public static string g(string A_0)
		{
			return c4.a(fa.a.GetString("DuplicateFont"), new object[]
			{
				A_0
			});
		}

		// Token: 0x060020AD RID: 8365 RVA: 0x00087947 File Offset: 0x00086947
		public static string l()
		{
			return fa.a.GetString("EmptyDocument");
		}

		// Token: 0x060020AE RID: 8366 RVA: 0x00087958 File Offset: 0x00086958
		public static string k()
		{
			return fa.a.GetString("MissingDocumentStartTag");
		}

		// Token: 0x060020AF RID: 8367 RVA: 0x00087969 File Offset: 0x00086969
		public static string f(string A_0)
		{
			return c4.a(fa.a.GetString("InvalidDocumentStartTag"), new object[]
			{
				A_0
			});
		}

		// Token: 0x060020B0 RID: 8368 RVA: 0x00087989 File Offset: 0x00086989
		public static string j()
		{
			return fa.a.GetString("MissingRtfVersion");
		}

		// Token: 0x060020B1 RID: 8369 RVA: 0x0008799A File Offset: 0x0008699A
		public static string e(string A_0)
		{
			return c4.a(fa.a.GetString("InvalidInitTagState"), new object[]
			{
				A_0
			});
		}

		// Token: 0x060020B2 RID: 8370 RVA: 0x000879BA File Offset: 0x000869BA
		public static string d(string A_0)
		{
			return c4.a(fa.a.GetString("UndefinedFont"), new object[]
			{
				A_0
			});
		}

		// Token: 0x060020B3 RID: 8371 RVA: 0x000879DA File Offset: 0x000869DA
		public static string m(int A_0)
		{
			return c4.a(fa.a.GetString("InvalidFontSize"), new object[]
			{
				A_0
			});
		}

		// Token: 0x060020B4 RID: 8372 RVA: 0x000879FF File Offset: 0x000869FF
		public static string l(int A_0)
		{
			return c4.a(fa.a.GetString("UndefinedColor"), new object[]
			{
				A_0
			});
		}

		// Token: 0x060020B5 RID: 8373 RVA: 0x00087A24 File Offset: 0x00086A24
		public static string c(string A_0)
		{
			return c4.a(fa.a.GetString("InvalidInitGroupState"), new object[]
			{
				A_0
			});
		}

		// Token: 0x060020B6 RID: 8374 RVA: 0x00087A44 File Offset: 0x00086A44
		public static string b(string A_0)
		{
			return c4.a(fa.a.GetString("InvalidGeneratorGroup"), new object[]
			{
				A_0
			});
		}

		// Token: 0x060020B7 RID: 8375 RVA: 0x00087A64 File Offset: 0x00086A64
		public static string a(string A_0)
		{
			return c4.a(fa.a.GetString("InvalidInitTextState"), new object[]
			{
				A_0
			});
		}

		// Token: 0x060020B8 RID: 8376 RVA: 0x00087A84 File Offset: 0x00086A84
		public static string a(string A_0, string A_1)
		{
			return c4.a(fa.a.GetString("InvalidDefaultFont"), new object[]
			{
				A_0,
				A_1
			});
		}

		// Token: 0x060020B9 RID: 8377 RVA: 0x00087AA8 File Offset: 0x00086AA8
		public static string i()
		{
			return fa.a.GetString("InvalidTextContextState");
		}

		// Token: 0x060020BA RID: 8378 RVA: 0x00087AB9 File Offset: 0x00086AB9
		public static string k(int A_0)
		{
			return c4.a(fa.a.GetString("UnsupportedRtfVersion"), new object[]
			{
				A_0
			});
		}

		// Token: 0x060020BB RID: 8379 RVA: 0x00087ADE File Offset: 0x00086ADE
		public static string h()
		{
			return fa.a.GetString("ImageFormatText");
		}

		// Token: 0x060020BC RID: 8380 RVA: 0x00087AEF File Offset: 0x00086AEF
		public static string g()
		{
			return fa.a.GetString("LogBeginDocument");
		}

		// Token: 0x060020BD RID: 8381 RVA: 0x00087B00 File Offset: 0x00086B00
		public static string f()
		{
			return fa.a.GetString("LogEndDocument");
		}

		// Token: 0x060020BE RID: 8382 RVA: 0x00087B11 File Offset: 0x00086B11
		public static string e()
		{
			return fa.a.GetString("LogInsertBreak");
		}

		// Token: 0x060020BF RID: 8383 RVA: 0x00087B22 File Offset: 0x00086B22
		public static string d()
		{
			return fa.a.GetString("LogInsertChar");
		}

		// Token: 0x060020C0 RID: 8384 RVA: 0x00087B33 File Offset: 0x00086B33
		public static string c()
		{
			return fa.a.GetString("LogInsertImage");
		}

		// Token: 0x060020C1 RID: 8385 RVA: 0x00087B44 File Offset: 0x00086B44
		public static string b()
		{
			return fa.a.GetString("LogInsertText");
		}

		// Token: 0x060020C2 RID: 8386 RVA: 0x00087B55 File Offset: 0x00086B55
		public static string a()
		{
			return fa.a.GetString("LogOverflowText");
		}

		// Token: 0x060020C3 RID: 8387 RVA: 0x00087B66 File Offset: 0x00086B66
		public static string j(int A_0)
		{
			return c4.a(fa.a.GetString("InvalidColor"), new object[]
			{
				A_0
			});
		}

		// Token: 0x060020C4 RID: 8388 RVA: 0x00087B8B File Offset: 0x00086B8B
		public static string i(int A_0)
		{
			return c4.a(fa.a.GetString("InvalidCharacterSet"), new object[]
			{
				A_0
			});
		}

		// Token: 0x060020C5 RID: 8389 RVA: 0x00087BB0 File Offset: 0x00086BB0
		public static string h(int A_0)
		{
			return c4.a(fa.a.GetString("InvalidCodePage"), new object[]
			{
				A_0
			});
		}

		// Token: 0x060020C6 RID: 8390 RVA: 0x00087BD5 File Offset: 0x00086BD5
		public static string g(int A_0)
		{
			return c4.a(fa.a.GetString("FontSizeOutOfRange"), new object[]
			{
				A_0
			});
		}

		// Token: 0x060020C7 RID: 8391 RVA: 0x00087BFA File Offset: 0x00086BFA
		public static string f(int A_0)
		{
			return c4.a(fa.a.GetString("InvalidImageWidth"), new object[]
			{
				A_0
			});
		}

		// Token: 0x060020C8 RID: 8392 RVA: 0x00087C1F File Offset: 0x00086C1F
		public static string e(int A_0)
		{
			return c4.a(fa.a.GetString("InvalidImageHeight"), new object[]
			{
				A_0
			});
		}

		// Token: 0x060020C9 RID: 8393 RVA: 0x00087C44 File Offset: 0x00086C44
		public static string d(int A_0)
		{
			return c4.a(fa.a.GetString("InvalidImageDesiredHeight"), new object[]
			{
				A_0
			});
		}

		// Token: 0x060020CA RID: 8394 RVA: 0x00087C69 File Offset: 0x00086C69
		public static string c(int A_0)
		{
			return c4.a(fa.a.GetString("InvalidImageDesiredWidth"), new object[]
			{
				A_0
			});
		}

		// Token: 0x060020CB RID: 8395 RVA: 0x00087C8E File Offset: 0x00086C8E
		public static string b(int A_0)
		{
			return c4.a(fa.a.GetString("InvalidImageScaleWidth"), new object[]
			{
				A_0
			});
		}

		// Token: 0x060020CC RID: 8396 RVA: 0x00087CB3 File Offset: 0x00086CB3
		public static string a(int A_0)
		{
			return c4.a(fa.a.GetString("InvalidImageScaleHeight"), new object[]
			{
				A_0
			});
		}

		// Token: 0x040014C0 RID: 5312
		private new static readonly ResourceManager a = c4.a(typeof(fa));
	}
}
