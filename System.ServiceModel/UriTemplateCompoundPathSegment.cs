using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ServiceModel;
using System.Text;

namespace System
{
	// Token: 0x02000011 RID: 17
	internal class UriTemplateCompoundPathSegment : UriTemplatePathSegment, IComparable<UriTemplateCompoundPathSegment>
	{
		// Token: 0x06000088 RID: 136 RVA: 0x00004D2A File Offset: 0x00002F2A
		private UriTemplateCompoundPathSegment(string originalSegment, bool endsWithSlash, string firstLiteral) : base(originalSegment, UriTemplatePartType.Compound, endsWithSlash)
		{
			this.firstLiteral = firstLiteral;
			this.varLitPairs = new List<UriTemplateCompoundPathSegment.VarAndLitPair>();
		}

		// Token: 0x06000089 RID: 137 RVA: 0x00004D48 File Offset: 0x00002F48
		public new static UriTemplateCompoundPathSegment CreateFromUriTemplate(string segment, UriTemplate template)
		{
			string text = segment;
			bool flag = segment.EndsWith("/", StringComparison.Ordinal);
			if (flag)
			{
				segment = segment.Remove(segment.Length - 1);
			}
			int num = segment.IndexOf("{", StringComparison.Ordinal);
			string text2 = (num > 0) ? segment.Substring(0, num) : string.Empty;
			if (text2.IndexOf("*", StringComparison.Ordinal) != -1)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new FormatException(SR.GetString("UTInvalidWildcardInVariableOrLiteral", new object[]
				{
					template.originalTemplate,
					"*"
				})));
			}
			UriTemplateCompoundPathSegment uriTemplateCompoundPathSegment = new UriTemplateCompoundPathSegment(text, flag, (text2 != string.Empty) ? Uri.UnescapeDataString(text2) : string.Empty);
			string text3;
			for (;;)
			{
				int num2 = segment.IndexOf("}", num + 1, StringComparison.Ordinal);
				if (num2 < num + 2)
				{
					break;
				}
				bool flag2;
				text3 = template.AddPathVariable(UriTemplatePartType.Compound, segment.Substring(num + 1, num2 - num - 1), out flag2);
				if (flag2)
				{
					goto Block_6;
				}
				num = segment.IndexOf("{", num2 + 1, StringComparison.Ordinal);
				string text4;
				if (num > 0)
				{
					if (num == num2 + 1)
					{
						goto Block_8;
					}
					text4 = segment.Substring(num2 + 1, num - num2 - 1);
				}
				else if (num2 + 1 < segment.Length)
				{
					text4 = segment.Substring(num2 + 1);
				}
				else
				{
					text4 = string.Empty;
				}
				if (text4.IndexOf("*", StringComparison.Ordinal) != -1)
				{
					goto Block_10;
				}
				if (text4.IndexOf('}') != -1)
				{
					goto Block_11;
				}
				uriTemplateCompoundPathSegment.varLitPairs.Add(new UriTemplateCompoundPathSegment.VarAndLitPair(text3, (text4 == string.Empty) ? string.Empty : Uri.UnescapeDataString(text4)));
				if (num <= 0)
				{
					goto Block_13;
				}
			}
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new FormatException(SR.GetString("UTInvalidFormatSegmentOrQueryPart", new object[]
			{
				segment
			})));
			Block_6:
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("UTDefaultValueToCompoundSegmentVar", new object[]
			{
				template,
				text,
				text3
			})));
			Block_8:
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument("template", SR.GetString("UTDoesNotSupportAdjacentVarsInCompoundSegment", new object[]
			{
				template,
				segment
			}));
			Block_10:
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new FormatException(SR.GetString("UTInvalidWildcardInVariableOrLiteral", new object[]
			{
				template.originalTemplate,
				"*"
			})));
			Block_11:
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new FormatException(SR.GetString("UTInvalidFormatSegmentOrQueryPart", new object[]
			{
				segment
			})));
			Block_13:
			if (string.IsNullOrEmpty(uriTemplateCompoundPathSegment.firstLiteral))
			{
				if (string.IsNullOrEmpty(uriTemplateCompoundPathSegment.varLitPairs[uriTemplateCompoundPathSegment.varLitPairs.Count - 1].Literal))
				{
					uriTemplateCompoundPathSegment.csClass = UriTemplateCompoundPathSegment.CompoundSegmentClass.HasNoPrefixNorSuffix;
				}
				else
				{
					uriTemplateCompoundPathSegment.csClass = UriTemplateCompoundPathSegment.CompoundSegmentClass.HasOnlySuffix;
				}
			}
			else if (string.IsNullOrEmpty(uriTemplateCompoundPathSegment.varLitPairs[uriTemplateCompoundPathSegment.varLitPairs.Count - 1].Literal))
			{
				uriTemplateCompoundPathSegment.csClass = UriTemplateCompoundPathSegment.CompoundSegmentClass.HasOnlyPrefix;
			}
			else
			{
				uriTemplateCompoundPathSegment.csClass = UriTemplateCompoundPathSegment.CompoundSegmentClass.HasPrefixAndSuffix;
			}
			return uriTemplateCompoundPathSegment;
		}

		// Token: 0x0600008A RID: 138 RVA: 0x0000502C File Offset: 0x0000322C
		public override void Bind(string[] values, ref int valueIndex, StringBuilder path)
		{
			path.Append(this.firstLiteral);
			for (int i = 0; i < this.varLitPairs.Count; i++)
			{
				int num = valueIndex;
				valueIndex = num + 1;
				path.Append(values[num]);
				path.Append(this.varLitPairs[i].Literal);
			}
			if (base.EndsWithSlash)
			{
				path.Append("/");
			}
		}

		// Token: 0x0600008B RID: 139 RVA: 0x000050A0 File Offset: 0x000032A0
		public override bool IsEquivalentTo(UriTemplatePathSegment other, bool ignoreTrailingSlash)
		{
			if (other == null)
			{
				return false;
			}
			if (!ignoreTrailingSlash && base.EndsWithSlash != other.EndsWithSlash)
			{
				return false;
			}
			UriTemplateCompoundPathSegment uriTemplateCompoundPathSegment = other as UriTemplateCompoundPathSegment;
			if (uriTemplateCompoundPathSegment == null)
			{
				return false;
			}
			if (this.varLitPairs.Count != uriTemplateCompoundPathSegment.varLitPairs.Count)
			{
				return false;
			}
			if (StringComparer.OrdinalIgnoreCase.Compare(this.firstLiteral, uriTemplateCompoundPathSegment.firstLiteral) != 0)
			{
				return false;
			}
			for (int i = 0; i < this.varLitPairs.Count; i++)
			{
				if (StringComparer.OrdinalIgnoreCase.Compare(this.varLitPairs[i].Literal, uriTemplateCompoundPathSegment.varLitPairs[i].Literal) != 0)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600008C RID: 140 RVA: 0x00005152 File Offset: 0x00003352
		public override bool IsMatch(UriTemplateLiteralPathSegment segment, bool ignoreTrailingSlash)
		{
			return (ignoreTrailingSlash || base.EndsWithSlash == segment.EndsWithSlash) && this.TryLookup(segment.AsUnescapedString(), null);
		}

		// Token: 0x0600008D RID: 141 RVA: 0x00005174 File Offset: 0x00003374
		public override void Lookup(string segment, NameValueCollection boundParameters)
		{
			if (!this.TryLookup(segment, boundParameters))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("UTCSRLookupBeforeMatch")));
			}
		}

		// Token: 0x0600008E RID: 142 RVA: 0x0000519C File Offset: 0x0000339C
		private bool TryLookup(string segment, NameValueCollection boundParameters)
		{
			int num = 0;
			if (!string.IsNullOrEmpty(this.firstLiteral))
			{
				if (!segment.StartsWith(this.firstLiteral, StringComparison.Ordinal))
				{
					return false;
				}
				num = this.firstLiteral.Length;
			}
			for (int i = 0; i < this.varLitPairs.Count - 1; i++)
			{
				int num2 = segment.IndexOf(this.varLitPairs[i].Literal, num, StringComparison.Ordinal);
				if (num2 < num + 1)
				{
					return false;
				}
				if (boundParameters != null)
				{
					string value = segment.Substring(num, num2 - num);
					boundParameters.Add(this.varLitPairs[i].VarName, value);
				}
				num = num2 + this.varLitPairs[i].Literal.Length;
			}
			if (num >= segment.Length)
			{
				return false;
			}
			if (string.IsNullOrEmpty(this.varLitPairs[this.varLitPairs.Count - 1].Literal))
			{
				if (boundParameters != null)
				{
					boundParameters.Add(this.varLitPairs[this.varLitPairs.Count - 1].VarName, segment.Substring(num));
				}
				return true;
			}
			if (num + this.varLitPairs[this.varLitPairs.Count - 1].Literal.Length < segment.Length && segment.EndsWith(this.varLitPairs[this.varLitPairs.Count - 1].Literal, StringComparison.Ordinal))
			{
				if (boundParameters != null)
				{
					boundParameters.Add(this.varLitPairs[this.varLitPairs.Count - 1].VarName, segment.Substring(num, segment.Length - num - this.varLitPairs[this.varLitPairs.Count - 1].Literal.Length));
				}
				return true;
			}
			return false;
		}

		// Token: 0x0600008F RID: 143 RVA: 0x00005380 File Offset: 0x00003580
		int IComparable<UriTemplateCompoundPathSegment>.CompareTo(UriTemplateCompoundPathSegment other)
		{
			switch (this.csClass)
			{
			case UriTemplateCompoundPathSegment.CompoundSegmentClass.HasPrefixAndSuffix:
			{
				UriTemplateCompoundPathSegment.CompoundSegmentClass compoundSegmentClass = other.csClass;
				if (compoundSegmentClass == UriTemplateCompoundPathSegment.CompoundSegmentClass.HasPrefixAndSuffix)
				{
					return this.CompareToOtherThatHasPrefixAndSuffix(other);
				}
				if (compoundSegmentClass - UriTemplateCompoundPathSegment.CompoundSegmentClass.HasOnlyPrefix > 2)
				{
					return 0;
				}
				return -1;
			}
			case UriTemplateCompoundPathSegment.CompoundSegmentClass.HasOnlyPrefix:
				switch (other.csClass)
				{
				case UriTemplateCompoundPathSegment.CompoundSegmentClass.HasPrefixAndSuffix:
					return 1;
				case UriTemplateCompoundPathSegment.CompoundSegmentClass.HasOnlyPrefix:
					return this.CompareToOtherThatHasOnlyPrefix(other);
				case UriTemplateCompoundPathSegment.CompoundSegmentClass.HasOnlySuffix:
				case UriTemplateCompoundPathSegment.CompoundSegmentClass.HasNoPrefixNorSuffix:
					return -1;
				default:
					return 0;
				}
				break;
			case UriTemplateCompoundPathSegment.CompoundSegmentClass.HasOnlySuffix:
				switch (other.csClass)
				{
				case UriTemplateCompoundPathSegment.CompoundSegmentClass.HasPrefixAndSuffix:
				case UriTemplateCompoundPathSegment.CompoundSegmentClass.HasOnlyPrefix:
					return 1;
				case UriTemplateCompoundPathSegment.CompoundSegmentClass.HasOnlySuffix:
					return this.CompareToOtherThatHasOnlySuffix(other);
				case UriTemplateCompoundPathSegment.CompoundSegmentClass.HasNoPrefixNorSuffix:
					return -1;
				default:
					return 0;
				}
				break;
			case UriTemplateCompoundPathSegment.CompoundSegmentClass.HasNoPrefixNorSuffix:
			{
				UriTemplateCompoundPathSegment.CompoundSegmentClass compoundSegmentClass2 = other.csClass;
				if (compoundSegmentClass2 - UriTemplateCompoundPathSegment.CompoundSegmentClass.HasPrefixAndSuffix <= 2)
				{
					return 1;
				}
				if (compoundSegmentClass2 != UriTemplateCompoundPathSegment.CompoundSegmentClass.HasNoPrefixNorSuffix)
				{
					return 0;
				}
				return this.CompareToOtherThatHasNoPrefixNorSuffix(other);
			}
			default:
				return 0;
			}
		}

		// Token: 0x06000090 RID: 144 RVA: 0x00005454 File Offset: 0x00003654
		private int CompareToOtherThatHasPrefixAndSuffix(UriTemplateCompoundPathSegment other)
		{
			int num = this.ComparePrefixToOtherPrefix(other);
			if (num != 0)
			{
				return num;
			}
			int num2 = this.CompareSuffixToOtherSuffix(other);
			if (num2 == 0)
			{
				return other.varLitPairs.Count - this.varLitPairs.Count;
			}
			return num2;
		}

		// Token: 0x06000091 RID: 145 RVA: 0x00005494 File Offset: 0x00003694
		private int CompareToOtherThatHasOnlyPrefix(UriTemplateCompoundPathSegment other)
		{
			int num = this.ComparePrefixToOtherPrefix(other);
			if (num == 0)
			{
				return other.varLitPairs.Count - this.varLitPairs.Count;
			}
			return num;
		}

		// Token: 0x06000092 RID: 146 RVA: 0x000054C8 File Offset: 0x000036C8
		private int CompareToOtherThatHasOnlySuffix(UriTemplateCompoundPathSegment other)
		{
			int num = this.CompareSuffixToOtherSuffix(other);
			if (num == 0)
			{
				return other.varLitPairs.Count - this.varLitPairs.Count;
			}
			return num;
		}

		// Token: 0x06000093 RID: 147 RVA: 0x000054F9 File Offset: 0x000036F9
		private int CompareToOtherThatHasNoPrefixNorSuffix(UriTemplateCompoundPathSegment other)
		{
			return other.varLitPairs.Count - this.varLitPairs.Count;
		}

		// Token: 0x06000094 RID: 148 RVA: 0x00005512 File Offset: 0x00003712
		private int ComparePrefixToOtherPrefix(UriTemplateCompoundPathSegment other)
		{
			return string.Compare(other.firstLiteral, this.firstLiteral, StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x06000095 RID: 149 RVA: 0x00005528 File Offset: 0x00003728
		private int CompareSuffixToOtherSuffix(UriTemplateCompoundPathSegment other)
		{
			string strB = UriTemplateCompoundPathSegment.ReverseString(this.varLitPairs[this.varLitPairs.Count - 1].Literal);
			string strA = UriTemplateCompoundPathSegment.ReverseString(other.varLitPairs[other.varLitPairs.Count - 1].Literal);
			return string.Compare(strA, strB, StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x06000096 RID: 150 RVA: 0x0000558C File Offset: 0x0000378C
		private static string ReverseString(string stringToReverse)
		{
			char[] array = new char[stringToReverse.Length];
			for (int i = 0; i < stringToReverse.Length; i++)
			{
				array[i] = stringToReverse[stringToReverse.Length - i - 1];
			}
			return new string(array);
		}

		// Token: 0x04000081 RID: 129
		private readonly string firstLiteral;

		// Token: 0x04000082 RID: 130
		private readonly List<UriTemplateCompoundPathSegment.VarAndLitPair> varLitPairs;

		// Token: 0x04000083 RID: 131
		private UriTemplateCompoundPathSegment.CompoundSegmentClass csClass;

		// Token: 0x02000AB9 RID: 2745
		private enum CompoundSegmentClass
		{
			// Token: 0x04003EE8 RID: 16104
			Undefined,
			// Token: 0x04003EE9 RID: 16105
			HasPrefixAndSuffix,
			// Token: 0x04003EEA RID: 16106
			HasOnlyPrefix,
			// Token: 0x04003EEB RID: 16107
			HasOnlySuffix,
			// Token: 0x04003EEC RID: 16108
			HasNoPrefixNorSuffix
		}

		// Token: 0x02000ABA RID: 2746
		private struct VarAndLitPair
		{
			// Token: 0x06006E05 RID: 28165 RVA: 0x0019B39E File Offset: 0x0019959E
			public VarAndLitPair(string varName, string literal)
			{
				this.varName = varName;
				this.literal = literal;
			}

			// Token: 0x170019AC RID: 6572
			// (get) Token: 0x06006E06 RID: 28166 RVA: 0x0019B3AE File Offset: 0x001995AE
			public string Literal
			{
				get
				{
					return this.literal;
				}
			}

			// Token: 0x170019AD RID: 6573
			// (get) Token: 0x06006E07 RID: 28167 RVA: 0x0019B3B6 File Offset: 0x001995B6
			public string VarName
			{
				get
				{
					return this.varName;
				}
			}

			// Token: 0x04003EED RID: 16109
			private readonly string literal;

			// Token: 0x04003EEE RID: 16110
			private readonly string varName;
		}
	}
}
