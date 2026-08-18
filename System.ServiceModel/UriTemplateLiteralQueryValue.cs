using System;
using System.Collections.Specialized;
using System.Runtime;
using System.Text;

namespace System
{
	// Token: 0x02000014 RID: 20
	internal class UriTemplateLiteralQueryValue : UriTemplateQueryValue, IComparable<UriTemplateLiteralQueryValue>
	{
		// Token: 0x060000A6 RID: 166 RVA: 0x00005737 File Offset: 0x00003937
		private UriTemplateLiteralQueryValue(string value) : base(UriTemplatePartType.Literal)
		{
			this.value = value;
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x00005747 File Offset: 0x00003947
		public static UriTemplateLiteralQueryValue CreateFromUriTemplate(string value)
		{
			return new UriTemplateLiteralQueryValue(UrlUtility.UrlDecode(value, Encoding.UTF8));
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x00005759 File Offset: 0x00003959
		public string AsEscapedString()
		{
			return UrlUtility.UrlEncode(this.value, Encoding.UTF8);
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x0000576B File Offset: 0x0000396B
		public string AsRawUnescapedString()
		{
			return this.value;
		}

		// Token: 0x060000AA RID: 170 RVA: 0x00005773 File Offset: 0x00003973
		public override void Bind(string keyName, string[] values, ref int valueIndex, StringBuilder query)
		{
			query.AppendFormat("&{0}={1}", UrlUtility.UrlEncode(keyName, Encoding.UTF8), this.AsEscapedString());
		}

		// Token: 0x060000AB RID: 171 RVA: 0x00005793 File Offset: 0x00003993
		public int CompareTo(UriTemplateLiteralQueryValue other)
		{
			return string.Compare(this.value, other.value, StringComparison.Ordinal);
		}

		// Token: 0x060000AC RID: 172 RVA: 0x000057A8 File Offset: 0x000039A8
		public override bool Equals(object obj)
		{
			UriTemplateLiteralQueryValue uriTemplateLiteralQueryValue = obj as UriTemplateLiteralQueryValue;
			return uriTemplateLiteralQueryValue != null && this.value == uriTemplateLiteralQueryValue.value;
		}

		// Token: 0x060000AD RID: 173 RVA: 0x000057D2 File Offset: 0x000039D2
		public override int GetHashCode()
		{
			return this.value.GetHashCode();
		}

		// Token: 0x060000AE RID: 174 RVA: 0x000057E0 File Offset: 0x000039E0
		public override bool IsEquivalentTo(UriTemplateQueryValue other)
		{
			if (other == null)
			{
				return false;
			}
			if (other.Nature != UriTemplatePartType.Literal)
			{
				return false;
			}
			UriTemplateLiteralQueryValue other2 = other as UriTemplateLiteralQueryValue;
			return this.CompareTo(other2) == 0;
		}

		// Token: 0x060000AF RID: 175 RVA: 0x0000580D File Offset: 0x00003A0D
		public override void Lookup(string value, NameValueCollection boundParameters)
		{
		}

		// Token: 0x04000087 RID: 135
		private readonly string value;
	}
}
