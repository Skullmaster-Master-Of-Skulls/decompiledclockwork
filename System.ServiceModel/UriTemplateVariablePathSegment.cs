using System;
using System.Collections.Specialized;
using System.Text;

namespace System
{
	// Token: 0x02000012 RID: 18
	internal class UriTemplateVariablePathSegment : UriTemplatePathSegment
	{
		// Token: 0x06000097 RID: 151 RVA: 0x000055CF File Offset: 0x000037CF
		public UriTemplateVariablePathSegment(string originalSegment, bool endsWithSlash, string varName) : base(originalSegment, UriTemplatePartType.Variable, endsWithSlash)
		{
			this.varName = varName;
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000098 RID: 152 RVA: 0x000055E1 File Offset: 0x000037E1
		public string VarName
		{
			get
			{
				return this.varName;
			}
		}

		// Token: 0x06000099 RID: 153 RVA: 0x000055EC File Offset: 0x000037EC
		public override void Bind(string[] values, ref int valueIndex, StringBuilder path)
		{
			int num;
			if (base.EndsWithSlash)
			{
				string format = "{0}/";
				num = valueIndex;
				valueIndex = num + 1;
				path.AppendFormat(format, values[num]);
				return;
			}
			num = valueIndex;
			valueIndex = num + 1;
			path.Append(values[num]);
		}

		// Token: 0x0600009A RID: 154 RVA: 0x0000562B File Offset: 0x0000382B
		public override bool IsEquivalentTo(UriTemplatePathSegment other, bool ignoreTrailingSlash)
		{
			return other != null && (ignoreTrailingSlash || base.EndsWithSlash == other.EndsWithSlash) && other.Nature == UriTemplatePartType.Variable;
		}

		// Token: 0x0600009B RID: 155 RVA: 0x0000564E File Offset: 0x0000384E
		public override bool IsMatch(UriTemplateLiteralPathSegment segment, bool ignoreTrailingSlash)
		{
			return (ignoreTrailingSlash || base.EndsWithSlash == segment.EndsWithSlash) && !segment.IsNullOrEmpty();
		}

		// Token: 0x0600009C RID: 156 RVA: 0x0000566C File Offset: 0x0000386C
		public override void Lookup(string segment, NameValueCollection boundParameters)
		{
			boundParameters.Add(this.varName, segment);
		}

		// Token: 0x04000084 RID: 132
		private readonly string varName;
	}
}
