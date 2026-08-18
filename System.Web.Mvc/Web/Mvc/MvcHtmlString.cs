using System;

namespace System.Web.Mvc
{
	// Token: 0x02000141 RID: 321
	public sealed class MvcHtmlString : HtmlString
	{
		// Token: 0x0600083B RID: 2107 RVA: 0x00016967 File Offset: 0x00014B67
		public MvcHtmlString(string value) : base(value ?? string.Empty)
		{
			this._value = (value ?? string.Empty);
		}

		// Token: 0x0600083C RID: 2108 RVA: 0x00016989 File Offset: 0x00014B89
		public static MvcHtmlString Create(string value)
		{
			return new MvcHtmlString(value);
		}

		// Token: 0x0600083D RID: 2109 RVA: 0x00016991 File Offset: 0x00014B91
		public static bool IsNullOrEmpty(MvcHtmlString value)
		{
			return value == null || value._value.Length == 0;
		}

		// Token: 0x04000248 RID: 584
		public static readonly MvcHtmlString Empty = MvcHtmlString.Create(string.Empty);

		// Token: 0x04000249 RID: 585
		private readonly string _value;
	}
}
