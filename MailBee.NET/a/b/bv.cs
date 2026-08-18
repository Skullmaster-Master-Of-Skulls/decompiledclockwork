using System;
using System.Globalization;
using System.Resources;
using System.Text;

namespace a.b
{
	// Token: 0x020003A2 RID: 930
	internal sealed class bv : c4
	{
		// Token: 0x0600218B RID: 8587 RVA: 0x00089D6C File Offset: 0x00088D6C
		public static string b(char A_0)
		{
			return c4.a(bv.a.GetString("InvalidFirstHexDigit"), new object[]
			{
				A_0
			});
		}

		// Token: 0x0600218C RID: 8588 RVA: 0x00089D91 File Offset: 0x00088D91
		public static string a(char A_0)
		{
			return c4.a(bv.a.GetString("InvalidSecondHexDigit"), new object[]
			{
				A_0
			});
		}

		// Token: 0x0600218D RID: 8589 RVA: 0x00089DB6 File Offset: 0x00088DB6
		public static string s()
		{
			return bv.a.GetString("ToManyBraces");
		}

		// Token: 0x0600218E RID: 8590 RVA: 0x00089DC7 File Offset: 0x00088DC7
		public static string r()
		{
			return bv.a.GetString("ToFewBraces");
		}

		// Token: 0x0600218F RID: 8591 RVA: 0x00089DD8 File Offset: 0x00088DD8
		public static string q()
		{
			return bv.a.GetString("NoRtfContent");
		}

		// Token: 0x06002190 RID: 8592 RVA: 0x00089DE9 File Offset: 0x00088DE9
		public static string c(string A_0)
		{
			return c4.a(bv.a.GetString("TagOnRootLevel"), new object[]
			{
				A_0
			});
		}

		// Token: 0x06002191 RID: 8593 RVA: 0x00089E09 File Offset: 0x00088E09
		public static string b(string A_0)
		{
			return c4.a(bv.a.GetString("InvalidUnicodeSkipCount"), new object[]
			{
				A_0
			});
		}

		// Token: 0x06002192 RID: 8594 RVA: 0x00089E29 File Offset: 0x00088E29
		public static string p()
		{
			return bv.a.GetString("UnexpectedEndOfFile");
		}

		// Token: 0x06002193 RID: 8595 RVA: 0x00089E3C File Offset: 0x00088E3C
		public static string a(byte[] A_0, int A_1, Encoding A_2)
		{
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < A_1; i++)
			{
				stringBuilder.Append(string.Format(CultureInfo.InvariantCulture, "{0:X}", new object[]
				{
					A_0[i]
				}));
			}
			return c4.a(bv.a.GetString("InvalidMultiByteEncoding"), new object[]
			{
				stringBuilder.ToString(),
				A_2.EncodingName,
				A_2.CodePage
			});
		}

		// Token: 0x06002194 RID: 8596 RVA: 0x00089EBC File Offset: 0x00088EBC
		public static string o()
		{
			return bv.a.GetString("EndOfFileInvalidCharacter");
		}

		// Token: 0x06002195 RID: 8597 RVA: 0x00089ECD File Offset: 0x00088ECD
		public static string a(string A_0)
		{
			return c4.a(bv.a.GetString("TextOnRootLevel"), new object[]
			{
				A_0
			});
		}

		// Token: 0x06002196 RID: 8598 RVA: 0x00089EED File Offset: 0x00088EED
		public static string n()
		{
			return bv.a.GetString("MissingGroupForNewTag");
		}

		// Token: 0x06002197 RID: 8599 RVA: 0x00089EFE File Offset: 0x00088EFE
		public static string m()
		{
			return bv.a.GetString("MissingGroupForNewText");
		}

		// Token: 0x06002198 RID: 8600 RVA: 0x00089F0F File Offset: 0x00088F0F
		public static string l()
		{
			return bv.a.GetString("MultipleRootLevelGroups");
		}

		// Token: 0x06002199 RID: 8601 RVA: 0x00089F20 File Offset: 0x00088F20
		public static string k()
		{
			return bv.a.GetString("UnclosedGroups");
		}

		// Token: 0x0600219A RID: 8602 RVA: 0x00089F31 File Offset: 0x00088F31
		public static string j()
		{
			return bv.a.GetString("LogGroupBegin");
		}

		// Token: 0x0600219B RID: 8603 RVA: 0x00089F42 File Offset: 0x00088F42
		public static string i()
		{
			return bv.a.GetString("LogGroupEnd");
		}

		// Token: 0x0600219C RID: 8604 RVA: 0x00089F53 File Offset: 0x00088F53
		public static string h()
		{
			return bv.a.GetString("LogOverflowText");
		}

		// Token: 0x0600219D RID: 8605 RVA: 0x00089F64 File Offset: 0x00088F64
		public static string g()
		{
			return bv.a.GetString("LogParseBegin");
		}

		// Token: 0x0600219E RID: 8606 RVA: 0x00089F75 File Offset: 0x00088F75
		public static string f()
		{
			return bv.a.GetString("LogParseEnd");
		}

		// Token: 0x0600219F RID: 8607 RVA: 0x00089F86 File Offset: 0x00088F86
		public static string e()
		{
			return bv.a.GetString("LogParseFail");
		}

		// Token: 0x060021A0 RID: 8608 RVA: 0x00089F97 File Offset: 0x00088F97
		public static string d()
		{
			return bv.a.GetString("LogParseFailUnknown");
		}

		// Token: 0x060021A1 RID: 8609 RVA: 0x00089FA8 File Offset: 0x00088FA8
		public static string c()
		{
			return bv.a.GetString("LogParseSuccess");
		}

		// Token: 0x060021A2 RID: 8610 RVA: 0x00089FB9 File Offset: 0x00088FB9
		public static string b()
		{
			return bv.a.GetString("LogTag");
		}

		// Token: 0x060021A3 RID: 8611 RVA: 0x00089FCA File Offset: 0x00088FCA
		public static string a()
		{
			return bv.a.GetString("LogText");
		}

		// Token: 0x04001591 RID: 5521
		private new static readonly ResourceManager a = c4.a(typeof(bv));
	}
}
