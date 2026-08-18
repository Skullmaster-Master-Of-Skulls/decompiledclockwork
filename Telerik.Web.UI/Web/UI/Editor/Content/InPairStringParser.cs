using System;
using System.Text.RegularExpressions;

namespace Telerik.Web.UI.Editor.Content
{
	// Token: 0x0200027B RID: 635
	public class InPairStringParser
	{
		// Token: 0x060016E9 RID: 5865 RVA: 0x0004D8E9 File Offset: 0x0004BAE9
		public InPairStringParser(char pairChar)
		{
			this.pattern = new Regex(string.Format("{0}.*?(?<!\\\\){0}", pairChar));
		}

		// Token: 0x060016EA RID: 5866 RVA: 0x0004D90C File Offset: 0x0004BB0C
		public string Sanitize(string input, int startIndex)
		{
			string input2 = input.Substring(startIndex);
			return input.Substring(0, startIndex) + this.pattern.Replace(input2, "", 1);
		}

		// Token: 0x04000606 RID: 1542
		private readonly Regex pattern;
	}
}
