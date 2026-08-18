using System;
using System.Text.RegularExpressions;

namespace Telerik.Web.UI.Editor.Content
{
	// Token: 0x0200027A RID: 634
	internal class HtmlTagSanitizer
	{
		// Token: 0x060016E1 RID: 5857 RVA: 0x0004D805 File Offset: 0x0004BA05
		public static string SanitizeTags(string input, MatchEvaluator evaluator)
		{
			return HtmlTagSanitizer.Sanitize(input, HtmlTagSanitizer.nonEmptyTagPattern, evaluator);
		}

		// Token: 0x060016E2 RID: 5858 RVA: 0x0004D813 File Offset: 0x0004BA13
		public static string SanitizeTagContent(string input, string tagName, MatchEvaluator evaluator)
		{
			return HtmlTagSanitizer.Sanitize(input, HtmlTagSanitizer.CreateTagContentPattern(tagName), evaluator);
		}

		// Token: 0x060016E3 RID: 5859 RVA: 0x0004D822 File Offset: 0x0004BA22
		public static string Sanitize(string input, Regex pattern, MatchEvaluator evaluator)
		{
			return pattern.Replace(input, evaluator);
		}

		// Token: 0x060016E4 RID: 5860 RVA: 0x0004D82C File Offset: 0x0004BA2C
		public static Regex CompileTagContentPattern()
		{
			return HtmlTagSanitizer.tagContentPattern;
		}

		// Token: 0x060016E5 RID: 5861 RVA: 0x0004D833 File Offset: 0x0004BA33
		public static Regex CompileTagContentPattern(string tagName)
		{
			return new Regex(string.Format(HtmlTagSanitizer.tagContentFormat, tagName), HtmlTagSanitizer.patternFlagsCompiled);
		}

		// Token: 0x060016E6 RID: 5862 RVA: 0x0004D84A File Offset: 0x0004BA4A
		public static Regex CreateTagContentPattern(string tagName)
		{
			return new Regex(string.Format(HtmlTagSanitizer.tagContentFormat, tagName), HtmlTagSanitizer.patternFlags);
		}

		// Token: 0x040005FF RID: 1535
		private static readonly string tagPatternFormat = "<{0}(?:=\"(?:\\\\\"|[^\"])*\"|='(?:\\\\'|[^'])*'|[^>])*/?>";

		// Token: 0x04000600 RID: 1536
		private static readonly string tagContentFormat = HtmlTagSanitizer.tagPatternFormat + "(.*?)</{0}>";

		// Token: 0x04000601 RID: 1537
		private static readonly string anyTagNameFormat = "<[\\w:]+";

		// Token: 0x04000602 RID: 1538
		private static readonly RegexOptions patternFlags = RegexOptions.IgnoreCase | RegexOptions.Singleline | RegexOptions.CultureInvariant;

		// Token: 0x04000603 RID: 1539
		private static readonly RegexOptions patternFlagsCompiled = HtmlTagSanitizer.patternFlags | RegexOptions.Compiled;

		// Token: 0x04000604 RID: 1540
		private static readonly Regex nonEmptyTagPattern = new Regex("<[\\w:]+(?=\\s)(?:=\"(?:\\\\\"|[^\"])*\"|='(?:\\\\'|[^'])*'|[^>])*/?>", HtmlTagSanitizer.patternFlags);

		// Token: 0x04000605 RID: 1541
		private static readonly Regex tagContentPattern = new Regex(string.Format(HtmlTagSanitizer.tagContentFormat, HtmlTagSanitizer.anyTagNameFormat), HtmlTagSanitizer.patternFlags);
	}
}
