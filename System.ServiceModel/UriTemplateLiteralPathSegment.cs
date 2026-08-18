using System;
using System.Collections.Specialized;
using System.ServiceModel;
using System.Text;

namespace System
{
	// Token: 0x02000010 RID: 16
	internal class UriTemplateLiteralPathSegment : UriTemplatePathSegment, IComparable<UriTemplateLiteralPathSegment>
	{
		// Token: 0x0600007B RID: 123 RVA: 0x00004AF5 File Offset: 0x00002CF5
		private UriTemplateLiteralPathSegment(string segment) : base(segment, UriTemplatePartType.Literal, segment.EndsWith("/", StringComparison.Ordinal))
		{
			if (base.EndsWithSlash)
			{
				this.segment = segment.Remove(segment.Length - 1);
				return;
			}
			this.segment = segment;
		}

		// Token: 0x0600007C RID: 124 RVA: 0x00004B30 File Offset: 0x00002D30
		public new static UriTemplateLiteralPathSegment CreateFromUriTemplate(string segment, UriTemplate template)
		{
			if (string.Compare(segment, "/", StringComparison.Ordinal) == 0)
			{
				return new UriTemplateLiteralPathSegment("/");
			}
			if (segment.IndexOf("*", StringComparison.Ordinal) != -1)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new FormatException(SR.GetString("UTInvalidWildcardInVariableOrLiteral", new object[]
				{
					template.originalTemplate,
					"*"
				})));
			}
			segment = segment.Replace("%2a", "*").Replace("%2A", "*");
			string a = new UriBuilder(UriTemplateLiteralPathSegment.dummyUri)
			{
				Path = segment
			}.Uri.AbsolutePath.Substring(1);
			if (a == string.Empty)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument("segment", SR.GetString("UTInvalidFormatSegmentOrQueryPart", new object[]
				{
					segment
				}));
			}
			return new UriTemplateLiteralPathSegment(a);
		}

		// Token: 0x0600007D RID: 125 RVA: 0x00004C12 File Offset: 0x00002E12
		public static UriTemplateLiteralPathSegment CreateFromWireData(string segment)
		{
			return new UriTemplateLiteralPathSegment(segment);
		}

		// Token: 0x0600007E RID: 126 RVA: 0x00004C1A File Offset: 0x00002E1A
		public string AsUnescapedString()
		{
			return Uri.UnescapeDataString(this.segment);
		}

		// Token: 0x0600007F RID: 127 RVA: 0x00004C27 File Offset: 0x00002E27
		public override void Bind(string[] values, ref int valueIndex, StringBuilder path)
		{
			if (base.EndsWithSlash)
			{
				path.AppendFormat("{0}/", this.AsUnescapedString());
				return;
			}
			path.Append(this.AsUnescapedString());
		}

		// Token: 0x06000080 RID: 128 RVA: 0x00004C51 File Offset: 0x00002E51
		public int CompareTo(UriTemplateLiteralPathSegment other)
		{
			return StringComparer.OrdinalIgnoreCase.Compare(this.segment, other.segment);
		}

		// Token: 0x06000081 RID: 129 RVA: 0x00004C6C File Offset: 0x00002E6C
		public override bool Equals(object obj)
		{
			UriTemplateLiteralPathSegment uriTemplateLiteralPathSegment = obj as UriTemplateLiteralPathSegment;
			return uriTemplateLiteralPathSegment != null && base.EndsWithSlash == uriTemplateLiteralPathSegment.EndsWithSlash && StringComparer.OrdinalIgnoreCase.Equals(this.segment, uriTemplateLiteralPathSegment.segment);
		}

		// Token: 0x06000082 RID: 130 RVA: 0x00004CAB File Offset: 0x00002EAB
		public override int GetHashCode()
		{
			return StringComparer.OrdinalIgnoreCase.GetHashCode(this.segment);
		}

		// Token: 0x06000083 RID: 131 RVA: 0x00004CC0 File Offset: 0x00002EC0
		public override bool IsEquivalentTo(UriTemplatePathSegment other, bool ignoreTrailingSlash)
		{
			if (other == null)
			{
				return false;
			}
			if (other.Nature != UriTemplatePartType.Literal)
			{
				return false;
			}
			UriTemplateLiteralPathSegment uriTemplateLiteralPathSegment = other as UriTemplateLiteralPathSegment;
			return this.IsMatch(uriTemplateLiteralPathSegment, ignoreTrailingSlash);
		}

		// Token: 0x06000084 RID: 132 RVA: 0x00004CEB File Offset: 0x00002EEB
		public override bool IsMatch(UriTemplateLiteralPathSegment segment, bool ignoreTrailingSlash)
		{
			return (ignoreTrailingSlash || segment.EndsWithSlash == base.EndsWithSlash) && this.CompareTo(segment) == 0;
		}

		// Token: 0x06000085 RID: 133 RVA: 0x00004D0A File Offset: 0x00002F0A
		public bool IsNullOrEmpty()
		{
			return string.IsNullOrEmpty(this.segment);
		}

		// Token: 0x06000086 RID: 134 RVA: 0x00004D17 File Offset: 0x00002F17
		public override void Lookup(string segment, NameValueCollection boundParameters)
		{
		}

		// Token: 0x0400007F RID: 127
		private readonly string segment;

		// Token: 0x04000080 RID: 128
		private static Uri dummyUri = new Uri("http://localhost");
	}
}
