using System;
using System.Collections;
using System.Text;
using System.Text.RegularExpressions;

namespace System.Web.Configuration
{
	// Token: 0x020006B2 RID: 1714
	internal class CapabilitiesPattern
	{
		// Token: 0x06005312 RID: 21266 RVA: 0x001243DA File Offset: 0x001225DA
		internal CapabilitiesPattern()
		{
			this._strings = new string[1];
			this._strings[0] = string.Empty;
			this._rules = new int[1];
			this._rules[0] = 2;
		}

		// Token: 0x06005313 RID: 21267 RVA: 0x00124410 File Offset: 0x00122610
		internal CapabilitiesPattern(string text)
		{
			ArrayList arrayList = new ArrayList();
			ArrayList arrayList2 = new ArrayList();
			int num = 0;
			Match match;
			for (;;)
			{
				if ((match = CapabilitiesPattern.textPat.Match(text, num)).Success && match.Length > 0)
				{
					arrayList2.Add(0);
					arrayList.Add(Regex.Unescape(match.ToString()));
					num = match.Index + match.Length;
				}
				if (num == text.Length)
				{
					goto IL_130;
				}
				if ((match = CapabilitiesPattern.refPat.Match(text, num)).Success)
				{
					arrayList2.Add(1);
					arrayList.Add(match.Groups["name"].Value);
				}
				else
				{
					if (!(match = CapabilitiesPattern.varPat.Match(text, num)).Success)
					{
						break;
					}
					arrayList2.Add(2);
					arrayList.Add(match.Groups["name"].Value);
				}
				num = match.Index + match.Length;
			}
			match = CapabilitiesPattern.errorPat.Match(text, num);
			throw new ArgumentException(SR.GetString("Unrecognized_construct_in_pattern", new object[]
			{
				match.ToString(),
				text
			}));
			IL_130:
			this._strings = (string[])arrayList.ToArray(typeof(string));
			this._rules = new int[arrayList2.Count];
			for (int i = 0; i < arrayList2.Count; i++)
			{
				this._rules[i] = (int)arrayList2[i];
			}
		}

		// Token: 0x06005314 RID: 21268 RVA: 0x001245A4 File Offset: 0x001227A4
		internal virtual string Expand(CapabilitiesState matchstate)
		{
			StringBuilder stringBuilder = null;
			string text = null;
			for (int i = 0; i < this._rules.Length; i++)
			{
				if (stringBuilder == null && text != null)
				{
					stringBuilder = new StringBuilder(text);
				}
				switch (this._rules[i])
				{
				case 0:
					text = this._strings[i];
					break;
				case 1:
					text = matchstate.ResolveReference(this._strings[i]);
					break;
				case 2:
					text = matchstate.ResolveVariable(this._strings[i]);
					break;
				}
				if (stringBuilder != null && text != null)
				{
					stringBuilder.Append(text);
				}
			}
			if (stringBuilder != null)
			{
				return stringBuilder.ToString();
			}
			if (text != null)
			{
				return text;
			}
			return string.Empty;
		}

		// Token: 0x04002B8A RID: 11146
		internal string[] _strings;

		// Token: 0x04002B8B RID: 11147
		internal int[] _rules;

		// Token: 0x04002B8C RID: 11148
		internal const int Literal = 0;

		// Token: 0x04002B8D RID: 11149
		internal const int Reference = 1;

		// Token: 0x04002B8E RID: 11150
		internal const int Variable = 2;

		// Token: 0x04002B8F RID: 11151
		internal static readonly Regex refPat = new Regex("\\G\\$(?:(?<name>\\d+)|\\{(?<name>\\w+)\\})");

		// Token: 0x04002B90 RID: 11152
		internal static readonly Regex varPat = new Regex("\\G\\%\\{(?<name>\\w+)\\}");

		// Token: 0x04002B91 RID: 11153
		internal static readonly Regex textPat = new Regex("\\G[^$%\\\\]*(?:\\.[^$%\\\\]*)*");

		// Token: 0x04002B92 RID: 11154
		internal static readonly Regex errorPat = new Regex(".{0,8}");

		// Token: 0x04002B93 RID: 11155
		internal static readonly CapabilitiesPattern Default = new CapabilitiesPattern();
	}
}
