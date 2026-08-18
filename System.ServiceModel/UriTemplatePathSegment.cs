using System;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Text;

namespace System
{
	// Token: 0x0200000F RID: 15
	[DebuggerDisplay("Segment={originalSegment} Nature={nature}")]
	internal abstract class UriTemplatePathSegment
	{
		// Token: 0x06000071 RID: 113 RVA: 0x00004A2D File Offset: 0x00002C2D
		protected UriTemplatePathSegment(string originalSegment, UriTemplatePartType nature, bool endsWithSlash)
		{
			this.originalSegment = originalSegment;
			this.nature = nature;
			this.endsWithSlash = endsWithSlash;
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000072 RID: 114 RVA: 0x00004A4A File Offset: 0x00002C4A
		public bool EndsWithSlash
		{
			get
			{
				return this.endsWithSlash;
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000073 RID: 115 RVA: 0x00004A52 File Offset: 0x00002C52
		public UriTemplatePartType Nature
		{
			get
			{
				return this.nature;
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000074 RID: 116 RVA: 0x00004A5A File Offset: 0x00002C5A
		public string OriginalSegment
		{
			get
			{
				return this.originalSegment;
			}
		}

		// Token: 0x06000075 RID: 117 RVA: 0x00004A64 File Offset: 0x00002C64
		public static UriTemplatePathSegment CreateFromUriTemplate(string segment, UriTemplate template)
		{
			switch (UriTemplateHelpers.IdentifyPartType(segment))
			{
			case UriTemplatePartType.Literal:
				return UriTemplateLiteralPathSegment.CreateFromUriTemplate(segment, template);
			case UriTemplatePartType.Compound:
				return UriTemplateCompoundPathSegment.CreateFromUriTemplate(segment, template);
			case UriTemplatePartType.Variable:
			{
				if (segment.EndsWith("/", StringComparison.Ordinal))
				{
					string varName = template.AddPathVariable(UriTemplatePartType.Variable, segment.Substring(1, segment.Length - 3));
					return new UriTemplateVariablePathSegment(segment, true, varName);
				}
				string varName2 = template.AddPathVariable(UriTemplatePartType.Variable, segment.Substring(1, segment.Length - 2));
				return new UriTemplateVariablePathSegment(segment, false, varName2);
			}
			default:
				return null;
			}
		}

		// Token: 0x06000076 RID: 118
		public abstract void Bind(string[] values, ref int valueIndex, StringBuilder path);

		// Token: 0x06000077 RID: 119
		public abstract bool IsEquivalentTo(UriTemplatePathSegment other, bool ignoreTrailingSlash);

		// Token: 0x06000078 RID: 120 RVA: 0x00004AEB File Offset: 0x00002CEB
		public bool IsMatch(UriTemplateLiteralPathSegment segment)
		{
			return this.IsMatch(segment, false);
		}

		// Token: 0x06000079 RID: 121
		public abstract bool IsMatch(UriTemplateLiteralPathSegment segment, bool ignoreTrailingSlash);

		// Token: 0x0600007A RID: 122
		public abstract void Lookup(string segment, NameValueCollection boundParameters);

		// Token: 0x0400007C RID: 124
		private readonly bool endsWithSlash;

		// Token: 0x0400007D RID: 125
		private readonly UriTemplatePartType nature;

		// Token: 0x0400007E RID: 126
		private readonly string originalSegment;
	}
}
