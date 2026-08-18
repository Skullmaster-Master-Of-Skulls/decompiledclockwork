using System;
using System.Collections.Specialized;
using System.Runtime;
using System.ServiceModel;
using System.Text;

namespace System
{
	// Token: 0x02000013 RID: 19
	internal abstract class UriTemplateQueryValue
	{
		// Token: 0x0600009D RID: 157 RVA: 0x0000567B File Offset: 0x0000387B
		protected UriTemplateQueryValue(UriTemplatePartType nature)
		{
			this.nature = nature;
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x0600009E RID: 158 RVA: 0x0000568A File Offset: 0x0000388A
		public static UriTemplateQueryValue Empty
		{
			get
			{
				return UriTemplateQueryValue.empty;
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x0600009F RID: 159 RVA: 0x00005691 File Offset: 0x00003891
		public UriTemplatePartType Nature
		{
			get
			{
				return this.nature;
			}
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x0000569C File Offset: 0x0000389C
		public static UriTemplateQueryValue CreateFromUriTemplate(string value, UriTemplate template)
		{
			if (value == null)
			{
				return UriTemplateQueryValue.Empty;
			}
			switch (UriTemplateHelpers.IdentifyPartType(value))
			{
			case UriTemplatePartType.Literal:
				return UriTemplateLiteralQueryValue.CreateFromUriTemplate(value);
			case UriTemplatePartType.Compound:
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("UTQueryCannotHaveCompoundValue", new object[]
				{
					template.originalTemplate
				})));
			case UriTemplatePartType.Variable:
				return new UriTemplateVariableQueryValue(template.AddQueryVariable(value.Substring(1, value.Length - 2)));
			default:
				return null;
			}
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x00005719 File Offset: 0x00003919
		public static bool IsNullOrEmpty(UriTemplateQueryValue utqv)
		{
			return utqv == null || utqv == UriTemplateQueryValue.Empty;
		}

		// Token: 0x060000A2 RID: 162
		public abstract void Bind(string keyName, string[] values, ref int valueIndex, StringBuilder query);

		// Token: 0x060000A3 RID: 163
		public abstract bool IsEquivalentTo(UriTemplateQueryValue other);

		// Token: 0x060000A4 RID: 164
		public abstract void Lookup(string value, NameValueCollection boundParameters);

		// Token: 0x04000085 RID: 133
		private readonly UriTemplatePartType nature;

		// Token: 0x04000086 RID: 134
		private static UriTemplateQueryValue empty = new UriTemplateQueryValue.EmptyUriTemplateQueryValue();

		// Token: 0x02000ABB RID: 2747
		private class EmptyUriTemplateQueryValue : UriTemplateQueryValue
		{
			// Token: 0x06006E08 RID: 28168 RVA: 0x0019B3BE File Offset: 0x001995BE
			public EmptyUriTemplateQueryValue() : base(UriTemplatePartType.Literal)
			{
			}

			// Token: 0x06006E09 RID: 28169 RVA: 0x0019B3C7 File Offset: 0x001995C7
			public override void Bind(string keyName, string[] values, ref int valueIndex, StringBuilder query)
			{
				query.AppendFormat("&{0}", UrlUtility.UrlEncode(keyName, Encoding.UTF8));
			}

			// Token: 0x06006E0A RID: 28170 RVA: 0x0019B3E1 File Offset: 0x001995E1
			public override bool IsEquivalentTo(UriTemplateQueryValue other)
			{
				return other == UriTemplateQueryValue.Empty;
			}

			// Token: 0x06006E0B RID: 28171 RVA: 0x0019B3EB File Offset: 0x001995EB
			public override void Lookup(string value, NameValueCollection boundParameters)
			{
			}
		}
	}
}
