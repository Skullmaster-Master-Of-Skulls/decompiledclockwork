using System;
using System.Text.RegularExpressions;

namespace Telerik.Web.UI.Editor.Content
{
	// Token: 0x02000278 RID: 632
	public class CssExpressionSanitizer : IContentSanitizer
	{
		// Token: 0x060016D3 RID: 5843 RVA: 0x0004D1CB File Offset: 0x0004B3CB
		public string Sanitize(string input)
		{
			if (input != null)
			{
				input = this.SanitizeTags(input);
				input = this.SanitizeStyleTagContent(input);
			}
			return input;
		}

		// Token: 0x060016D4 RID: 5844 RVA: 0x0004D1E3 File Offset: 0x0004B3E3
		private string SanitizeTags(string input)
		{
			return HtmlTagSanitizer.SanitizeTags(input, new MatchEvaluator(this.SanitizeStyleAttributes));
		}

		// Token: 0x060016D5 RID: 5845 RVA: 0x0004D1F7 File Offset: 0x0004B3F7
		private string SanitizeStyleAttributes(Match m)
		{
			return this.SanitizeMatches(m.Value, CssExpressionSanitizer.styleAttrExpressionPattern.Matches(m.Value));
		}

		// Token: 0x060016D6 RID: 5846 RVA: 0x0004D215 File Offset: 0x0004B415
		private string SanitizeStyleTagContent(string input)
		{
			return HtmlTagSanitizer.Sanitize(input, CssExpressionSanitizer.styleTagContentPattern, new MatchEvaluator(this.SanitizeMatchedContent));
		}

		// Token: 0x060016D7 RID: 5847 RVA: 0x0004D22E File Offset: 0x0004B42E
		private string SanitizeMatchedContent(Match m)
		{
			return this.SanitizeMatches(m.Value, CssExpressionSanitizer.expressionPattern.Matches(m.Value));
		}

		// Token: 0x060016D8 RID: 5848 RVA: 0x0004D24C File Offset: 0x0004B44C
		private string SanitizeMatches(string input, MatchCollection matches)
		{
			for (int i = matches.Count - 1; i >= 0; i--)
			{
				input = this.SanitizeExpression(input, matches[i]);
			}
			return input;
		}

		// Token: 0x060016D9 RID: 5849 RVA: 0x0004D280 File Offset: 0x0004B480
		private string SanitizeExpression(string input, Match m)
		{
			int index = m.Groups["cssRule"].Index;
			int index2 = m.Groups["bracket"].Index;
			input = this.StripMatchingBracketsWithContent(input, index2);
			input = input.Remove(index, index2 - index);
			if (input[index] == ';')
			{
				input = input.Remove(index, 1);
			}
			return input;
		}

		// Token: 0x060016DA RID: 5850 RVA: 0x0004D2E8 File Offset: 0x0004B4E8
		private string StripMatchingBracketsWithContent(string input, int startIndex)
		{
			int num = 0;
			do
			{
				char c = input[startIndex];
				if (c == '"' || c == '\'' || c == '/')
				{
					InPairStringParser inPairStringParser = new InPairStringParser(c);
					input = inPairStringParser.Sanitize(input, startIndex);
					c = input[startIndex];
				}
				input = input.Remove(startIndex, 1);
				if (c == '(')
				{
					num++;
				}
				else if (c == ')')
				{
					num--;
				}
			}
			while (num > 0 && input.Length > startIndex);
			return input;
		}

		// Token: 0x040005F9 RID: 1529
		private static readonly string expressionPatternStr = "(?<cssRule>[a-zA-Z-]+: ?expression(?<bracket>\\())";

		// Token: 0x040005FA RID: 1530
		private static readonly Regex expressionPattern = new Regex(CssExpressionSanitizer.expressionPatternStr, RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.Singleline | RegexOptions.CultureInvariant);

		// Token: 0x040005FB RID: 1531
		private static readonly Regex styleAttrExpressionPattern = new Regex("style=['\"]?.*?" + CssExpressionSanitizer.expressionPatternStr, RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.Singleline | RegexOptions.CultureInvariant);

		// Token: 0x040005FC RID: 1532
		private static readonly Regex styleTagContentPattern = HtmlTagSanitizer.CompileTagContentPattern("style");
	}
}
