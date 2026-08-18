using System;
using System.Resources;

namespace a.b
{
	// Token: 0x020003B8 RID: 952
	internal sealed class cg : c4
	{
		// Token: 0x06002255 RID: 8789 RVA: 0x0008C3EE File Offset: 0x0008B3EE
		public static string k()
		{
			return cg.a.GetString("ArgumentMayNotBeEmpty");
		}

		// Token: 0x06002256 RID: 8790 RVA: 0x0008C3FF File Offset: 0x0008B3FF
		public static string a(string A_0, string A_1, string A_2)
		{
			return c4.a(cg.a.GetString("CollectionToolInvalidEnum"), new object[]
			{
				A_0,
				A_1,
				A_2
			});
		}

		// Token: 0x06002257 RID: 8791 RVA: 0x0008C427 File Offset: 0x0008B427
		public static string j()
		{
			return cg.a.GetString("LoggerNameMayNotBeEmpty");
		}

		// Token: 0x06002258 RID: 8792 RVA: 0x0008C438 File Offset: 0x0008B438
		public static string i()
		{
			return cg.a.GetString("LoggerFactoryConfigError");
		}

		// Token: 0x06002259 RID: 8793 RVA: 0x0008C449 File Offset: 0x0008B449
		public static string h()
		{
			return cg.a.GetString("ProgramPressAnyKeyToQuit");
		}

		// Token: 0x0600225A RID: 8794 RVA: 0x0008C45A File Offset: 0x0008B45A
		public static string g()
		{
			return cg.a.GetString("StringToolSeparatorIncludesQuoteOrEscapeChar");
		}

		// Token: 0x0600225B RID: 8795 RVA: 0x0008C46B File Offset: 0x0008B46B
		public static string f()
		{
			return cg.a.GetString("StringToolMissingEscapedHexCode");
		}

		// Token: 0x0600225C RID: 8796 RVA: 0x0008C47C File Offset: 0x0008B47C
		public static string e()
		{
			return cg.a.GetString("StringToolMissingEscapedChar");
		}

		// Token: 0x0600225D RID: 8797 RVA: 0x0008C48D File Offset: 0x0008B48D
		public static string d()
		{
			return cg.a.GetString("StringToolUnbalancedQuotes");
		}

		// Token: 0x0600225E RID: 8798 RVA: 0x0008C49E File Offset: 0x0008B49E
		public static string c()
		{
			return cg.a.GetString("StringToolContainsInvalidHexChar");
		}

		// Token: 0x0600225F RID: 8799 RVA: 0x0008C4AF File Offset: 0x0008B4AF
		public static string a(string A_0)
		{
			return c4.a(cg.a.GetString("LoggerLogFileNotSupportedByType"), new object[]
			{
				A_0
			});
		}

		// Token: 0x06002260 RID: 8800 RVA: 0x0008C4CF File Offset: 0x0008B4CF
		public static string b()
		{
			return cg.a.GetString("LoggerLoggingLevelXmlError");
		}

		// Token: 0x06002261 RID: 8801 RVA: 0x0008C4E0 File Offset: 0x0008B4E0
		public static string a()
		{
			return cg.a.GetString("LoggerLoggingLevelRepository");
		}

		// Token: 0x04001690 RID: 5776
		private new static readonly ResourceManager a = c4.a(typeof(cg));
	}
}
