using System;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using NLog.Config;

namespace NLog.LayoutRenderers.Wrappers
{
	// Token: 0x02000103 RID: 259
	[LayoutRenderer("replace")]
	[ThreadAgnostic]
	public sealed class ReplaceLayoutRendererWrapper : WrapperLayoutRendererBase
	{
		// Token: 0x1700013E RID: 318
		// (get) Token: 0x0600073B RID: 1851 RVA: 0x00010105 File Offset: 0x0000E305
		// (set) Token: 0x0600073C RID: 1852 RVA: 0x0001010D File Offset: 0x0000E30D
		public string SearchFor { get; set; }

		// Token: 0x1700013F RID: 319
		// (get) Token: 0x0600073D RID: 1853 RVA: 0x00010116 File Offset: 0x0000E316
		// (set) Token: 0x0600073E RID: 1854 RVA: 0x0001011E File Offset: 0x0000E31E
		public bool Regex { get; set; }

		// Token: 0x17000140 RID: 320
		// (get) Token: 0x0600073F RID: 1855 RVA: 0x00010127 File Offset: 0x0000E327
		// (set) Token: 0x06000740 RID: 1856 RVA: 0x0001012F File Offset: 0x0000E32F
		public string ReplaceWith { get; set; }

		// Token: 0x17000141 RID: 321
		// (get) Token: 0x06000741 RID: 1857 RVA: 0x00010138 File Offset: 0x0000E338
		// (set) Token: 0x06000742 RID: 1858 RVA: 0x00010140 File Offset: 0x0000E340
		public string ReplaceGroupName { get; set; }

		// Token: 0x17000142 RID: 322
		// (get) Token: 0x06000743 RID: 1859 RVA: 0x00010149 File Offset: 0x0000E349
		// (set) Token: 0x06000744 RID: 1860 RVA: 0x00010151 File Offset: 0x0000E351
		public bool IgnoreCase { get; set; }

		// Token: 0x17000143 RID: 323
		// (get) Token: 0x06000745 RID: 1861 RVA: 0x0001015A File Offset: 0x0000E35A
		// (set) Token: 0x06000746 RID: 1862 RVA: 0x00010162 File Offset: 0x0000E362
		public bool WholeWords { get; set; }

		// Token: 0x06000747 RID: 1863 RVA: 0x0001016C File Offset: 0x0000E36C
		protected override void InitializeLayoutRenderer()
		{
			base.InitializeLayoutRenderer();
			string text = this.SearchFor;
			if (!this.Regex)
			{
				text = System.Text.RegularExpressions.Regex.Escape(text);
			}
			RegexOptions regexOptions = RegexOptions.Compiled;
			if (this.IgnoreCase)
			{
				regexOptions |= RegexOptions.IgnoreCase;
			}
			if (this.WholeWords)
			{
				text = "\\b" + text + "\\b";
			}
			this.regex = new Regex(text, regexOptions);
		}

		// Token: 0x06000748 RID: 1864 RVA: 0x000101CC File Offset: 0x0000E3CC
		protected override string Transform(string text)
		{
			ReplaceLayoutRendererWrapper.Replacer @object = new ReplaceLayoutRendererWrapper.Replacer(text, this.ReplaceGroupName, this.ReplaceWith);
			if (!string.IsNullOrEmpty(this.ReplaceGroupName))
			{
				return this.regex.Replace(text, new MatchEvaluator(@object.EvaluateMatch));
			}
			return this.regex.Replace(text, this.ReplaceWith);
		}

		// Token: 0x06000749 RID: 1865 RVA: 0x0001022C File Offset: 0x0000E42C
		public static string ReplaceNamedGroup(string input, string groupName, string replacement, Match match)
		{
			StringBuilder stringBuilder = new StringBuilder(input);
			int index = match.Index;
			int num = match.Length;
			IOrderedEnumerable<Capture> orderedEnumerable = from c in match.Groups[groupName].Captures.OfType<Capture>()
			orderby c.Index descending
			select c;
			foreach (Capture capture in orderedEnumerable)
			{
				if (capture != null)
				{
					num += replacement.Length - capture.Length;
					stringBuilder.Remove(capture.Index, capture.Length);
					stringBuilder.Insert(capture.Index, replacement);
				}
			}
			int num2 = index + num;
			stringBuilder.Remove(num2, stringBuilder.Length - num2);
			stringBuilder.Remove(0, index);
			return stringBuilder.ToString();
		}

		// Token: 0x04000218 RID: 536
		private Regex regex;

		// Token: 0x02000104 RID: 260
		[ThreadAgnostic]
		public class Replacer
		{
			// Token: 0x0600074C RID: 1868 RVA: 0x00010328 File Offset: 0x0000E528
			internal Replacer(string text, string replaceGroupName, string replaceWith)
			{
				this.text = text;
				this.replaceGroupName = replaceGroupName;
				this.replaceWith = replaceWith;
			}

			// Token: 0x0600074D RID: 1869 RVA: 0x00010345 File Offset: 0x0000E545
			internal string EvaluateMatch(Match match)
			{
				return ReplaceLayoutRendererWrapper.ReplaceNamedGroup(this.text, this.replaceGroupName, this.replaceWith, match);
			}

			// Token: 0x04000220 RID: 544
			private readonly string text;

			// Token: 0x04000221 RID: 545
			private readonly string replaceGroupName;

			// Token: 0x04000222 RID: 546
			private readonly string replaceWith;
		}
	}
}
