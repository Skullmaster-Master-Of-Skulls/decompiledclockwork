using System;
using System.Security.Util;

namespace System.Security.Permissions
{
	// Token: 0x02000623 RID: 1571
	[Serializable]
	internal class EnvironmentStringExpressionSet : StringExpressionSet
	{
		// Token: 0x0600389D RID: 14493 RVA: 0x000BEF75 File Offset: 0x000BDF75
		public EnvironmentStringExpressionSet() : base(true, null, false)
		{
		}

		// Token: 0x0600389E RID: 14494 RVA: 0x000BEF80 File Offset: 0x000BDF80
		public EnvironmentStringExpressionSet(string str) : base(true, str, false)
		{
		}

		// Token: 0x0600389F RID: 14495 RVA: 0x000BEF8B File Offset: 0x000BDF8B
		protected override StringExpressionSet CreateNewEmpty()
		{
			return new EnvironmentStringExpressionSet();
		}

		// Token: 0x060038A0 RID: 14496 RVA: 0x000BEF92 File Offset: 0x000BDF92
		protected override bool StringSubsetString(string left, string right, bool ignoreCase)
		{
			if (!ignoreCase)
			{
				return string.Compare(left, right, StringComparison.Ordinal) == 0;
			}
			return string.Compare(left, right, StringComparison.OrdinalIgnoreCase) == 0;
		}

		// Token: 0x060038A1 RID: 14497 RVA: 0x000BEFAE File Offset: 0x000BDFAE
		protected override string ProcessWholeString(string str)
		{
			return str;
		}

		// Token: 0x060038A2 RID: 14498 RVA: 0x000BEFB1 File Offset: 0x000BDFB1
		protected override string ProcessSingleString(string str)
		{
			return str;
		}
	}
}
