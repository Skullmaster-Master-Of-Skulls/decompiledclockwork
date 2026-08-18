using System;
using System.Collections;
using System.Collections.Generic;

namespace System.Text.RegularExpressions
{
	// Token: 0x020006A4 RID: 1700
	internal sealed class RegexReplacement
	{
		// Token: 0x06003F9E RID: 16286 RVA: 0x0010B1D8 File Offset: 0x001093D8
		internal RegexReplacement(string rep, RegexNode concat, Hashtable _caps)
		{
			this._rep = rep;
			if (concat.Type() != 25)
			{
				throw new ArgumentException(SR.GetString("ReplacementError"));
			}
			StringBuilder stringBuilder = new StringBuilder();
			List<string> list = new List<string>();
			List<int> list2 = new List<int>();
			int i = 0;
			while (i < concat.ChildCount())
			{
				RegexNode regexNode = concat.Child(i);
				switch (regexNode.Type())
				{
				case 9:
					stringBuilder.Append(regexNode._ch);
					break;
				case 10:
				case 11:
					goto IL_E9;
				case 12:
					stringBuilder.Append(regexNode._str);
					break;
				case 13:
				{
					if (stringBuilder.Length > 0)
					{
						list2.Add(list.Count);
						list.Add(stringBuilder.ToString());
						stringBuilder.Length = 0;
					}
					int num = regexNode._m;
					if (_caps != null && num >= 0)
					{
						num = (int)_caps[num];
					}
					list2.Add(-5 - num);
					break;
				}
				default:
					goto IL_E9;
				}
				i++;
				continue;
				IL_E9:
				throw new ArgumentException(SR.GetString("ReplacementError"));
			}
			if (stringBuilder.Length > 0)
			{
				list2.Add(list.Count);
				list.Add(stringBuilder.ToString());
			}
			this._strings = list;
			this._rules = list2;
		}

		// Token: 0x06003F9F RID: 16287 RVA: 0x0010B320 File Offset: 0x00109520
		private void ReplacementImpl(StringBuilder sb, Match match)
		{
			for (int i = 0; i < this._rules.Count; i++)
			{
				int num = this._rules[i];
				if (num >= 0)
				{
					sb.Append(this._strings[num]);
				}
				else if (num < -4)
				{
					sb.Append(match.GroupToStringImpl(-5 - num));
				}
				else
				{
					switch (-5 - num)
					{
					case -4:
						sb.Append(match.GetOriginalString());
						break;
					case -3:
						sb.Append(match.LastGroupToStringImpl());
						break;
					case -2:
						sb.Append(match.GetRightSubstring());
						break;
					case -1:
						sb.Append(match.GetLeftSubstring());
						break;
					}
				}
			}
		}

		// Token: 0x06003FA0 RID: 16288 RVA: 0x0010B3E4 File Offset: 0x001095E4
		private void ReplacementImplRTL(List<string> al, Match match)
		{
			for (int i = this._rules.Count - 1; i >= 0; i--)
			{
				int num = this._rules[i];
				if (num >= 0)
				{
					al.Add(this._strings[num]);
				}
				else if (num < -4)
				{
					al.Add(match.GroupToStringImpl(-5 - num));
				}
				else
				{
					switch (-5 - num)
					{
					case -4:
						al.Add(match.GetOriginalString());
						break;
					case -3:
						al.Add(match.LastGroupToStringImpl());
						break;
					case -2:
						al.Add(match.GetRightSubstring());
						break;
					case -1:
						al.Add(match.GetLeftSubstring());
						break;
					}
				}
			}
		}

		// Token: 0x17000EDB RID: 3803
		// (get) Token: 0x06003FA1 RID: 16289 RVA: 0x0010B4A1 File Offset: 0x001096A1
		internal string Pattern
		{
			get
			{
				return this._rep;
			}
		}

		// Token: 0x06003FA2 RID: 16290 RVA: 0x0010B4AC File Offset: 0x001096AC
		internal string Replacement(Match match)
		{
			StringBuilder stringBuilder = new StringBuilder();
			this.ReplacementImpl(stringBuilder, match);
			return stringBuilder.ToString();
		}

		// Token: 0x06003FA3 RID: 16291 RVA: 0x0010B4D0 File Offset: 0x001096D0
		internal string Replace(Regex regex, string input, int count, int startat)
		{
			if (count < -1)
			{
				throw new ArgumentOutOfRangeException("count", SR.GetString("CountTooSmall"));
			}
			if (startat < 0 || startat > input.Length)
			{
				throw new ArgumentOutOfRangeException("startat", SR.GetString("BeginIndexNotNegative"));
			}
			if (count == 0)
			{
				return input;
			}
			Match match = regex.Match(input, startat);
			if (!match.Success)
			{
				return input;
			}
			StringBuilder stringBuilder;
			if (!regex.RightToLeft)
			{
				stringBuilder = new StringBuilder();
				int num = 0;
				do
				{
					if (match.Index != num)
					{
						stringBuilder.Append(input, num, match.Index - num);
					}
					num = match.Index + match.Length;
					this.ReplacementImpl(stringBuilder, match);
					if (--count == 0)
					{
						break;
					}
					match = match.NextMatch();
				}
				while (match.Success);
				if (num < input.Length)
				{
					stringBuilder.Append(input, num, input.Length - num);
				}
			}
			else
			{
				List<string> list = new List<string>();
				int num2 = input.Length;
				do
				{
					if (match.Index + match.Length != num2)
					{
						list.Add(input.Substring(match.Index + match.Length, num2 - match.Index - match.Length));
					}
					num2 = match.Index;
					this.ReplacementImplRTL(list, match);
					if (--count == 0)
					{
						break;
					}
					match = match.NextMatch();
				}
				while (match.Success);
				stringBuilder = new StringBuilder();
				if (num2 > 0)
				{
					stringBuilder.Append(input, 0, num2);
				}
				for (int i = list.Count - 1; i >= 0; i--)
				{
					stringBuilder.Append(list[i]);
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06003FA4 RID: 16292 RVA: 0x0010B660 File Offset: 0x00109860
		internal static string Replace(MatchEvaluator evaluator, Regex regex, string input, int count, int startat)
		{
			if (evaluator == null)
			{
				throw new ArgumentNullException("evaluator");
			}
			if (count < -1)
			{
				throw new ArgumentOutOfRangeException("count", SR.GetString("CountTooSmall"));
			}
			if (startat < 0 || startat > input.Length)
			{
				throw new ArgumentOutOfRangeException("startat", SR.GetString("BeginIndexNotNegative"));
			}
			if (count == 0)
			{
				return input;
			}
			Match match = regex.Match(input, startat);
			if (!match.Success)
			{
				return input;
			}
			StringBuilder stringBuilder;
			if (!regex.RightToLeft)
			{
				stringBuilder = new StringBuilder();
				int num = 0;
				do
				{
					if (match.Index != num)
					{
						stringBuilder.Append(input, num, match.Index - num);
					}
					num = match.Index + match.Length;
					stringBuilder.Append(evaluator(match));
					if (--count == 0)
					{
						break;
					}
					match = match.NextMatch();
				}
				while (match.Success);
				if (num < input.Length)
				{
					stringBuilder.Append(input, num, input.Length - num);
				}
			}
			else
			{
				List<string> list = new List<string>();
				int num2 = input.Length;
				do
				{
					if (match.Index + match.Length != num2)
					{
						list.Add(input.Substring(match.Index + match.Length, num2 - match.Index - match.Length));
					}
					num2 = match.Index;
					list.Add(evaluator(match));
					if (--count == 0)
					{
						break;
					}
					match = match.NextMatch();
				}
				while (match.Success);
				stringBuilder = new StringBuilder();
				if (num2 > 0)
				{
					stringBuilder.Append(input, 0, num2);
				}
				for (int i = list.Count - 1; i >= 0; i--)
				{
					stringBuilder.Append(list[i]);
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06003FA5 RID: 16293 RVA: 0x0010B808 File Offset: 0x00109A08
		internal static string[] Split(Regex regex, string input, int count, int startat)
		{
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", SR.GetString("CountTooSmall"));
			}
			if (startat < 0 || startat > input.Length)
			{
				throw new ArgumentOutOfRangeException("startat", SR.GetString("BeginIndexNotNegative"));
			}
			if (count == 1)
			{
				return new string[]
				{
					input
				};
			}
			count--;
			Match match = regex.Match(input, startat);
			if (!match.Success)
			{
				return new string[]
				{
					input
				};
			}
			List<string> list = new List<string>();
			if (!regex.RightToLeft)
			{
				int num = 0;
				do
				{
					list.Add(input.Substring(num, match.Index - num));
					num = match.Index + match.Length;
					for (int i = 1; i < match.Groups.Count; i++)
					{
						if (match.IsMatched(i))
						{
							list.Add(match.Groups[i].ToString());
						}
					}
					if (--count == 0)
					{
						break;
					}
					match = match.NextMatch();
				}
				while (match.Success);
				list.Add(input.Substring(num, input.Length - num));
			}
			else
			{
				int num2 = input.Length;
				do
				{
					list.Add(input.Substring(match.Index + match.Length, num2 - match.Index - match.Length));
					num2 = match.Index;
					for (int j = 1; j < match.Groups.Count; j++)
					{
						if (match.IsMatched(j))
						{
							list.Add(match.Groups[j].ToString());
						}
					}
					if (--count == 0)
					{
						break;
					}
					match = match.NextMatch();
				}
				while (match.Success);
				list.Add(input.Substring(0, num2));
				list.Reverse(0, list.Count);
			}
			return list.ToArray();
		}

		// Token: 0x04002E60 RID: 11872
		internal string _rep;

		// Token: 0x04002E61 RID: 11873
		internal List<string> _strings;

		// Token: 0x04002E62 RID: 11874
		internal List<int> _rules;

		// Token: 0x04002E63 RID: 11875
		internal const int Specials = 4;

		// Token: 0x04002E64 RID: 11876
		internal const int LeftPortion = -1;

		// Token: 0x04002E65 RID: 11877
		internal const int RightPortion = -2;

		// Token: 0x04002E66 RID: 11878
		internal const int LastGroup = -3;

		// Token: 0x04002E67 RID: 11879
		internal const int WholeString = -4;
	}
}
