using System;
using Telerik.Web.UI;

namespace Telerik.Web
{
	// Token: 0x020001D0 RID: 464
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
	public sealed class RequiredCssAttribute : Attribute
	{
		// Token: 0x170005A6 RID: 1446
		// (get) Token: 0x060010CC RID: 4300 RVA: 0x0003D554 File Offset: 0x0003B754
		public string CssResourceName
		{
			get
			{
				return this._styleSheetName;
			}
		}

		// Token: 0x170005A7 RID: 1447
		// (get) Token: 0x060010CD RID: 4301 RVA: 0x0003D55C File Offset: 0x0003B75C
		public RenderMode RenderMode
		{
			get
			{
				return this._renderMode;
			}
		}

		// Token: 0x170005A8 RID: 1448
		// (get) Token: 0x060010CE RID: 4302 RVA: 0x0003D564 File Offset: 0x0003B764
		public Type Type
		{
			get
			{
				return this._type;
			}
		}

		// Token: 0x060010CF RID: 4303 RVA: 0x0003D56C File Offset: 0x0003B76C
		public RequiredCssAttribute(string styleName, RenderMode renderMode, Type type)
		{
			this._styleSheetName = styleName;
			this._renderMode = renderMode;
			this._type = type;
		}

		// Token: 0x040004C4 RID: 1220
		private readonly string _styleSheetName;

		// Token: 0x040004C5 RID: 1221
		private readonly RenderMode _renderMode;

		// Token: 0x040004C6 RID: 1222
		private readonly Type _type;
	}
}
