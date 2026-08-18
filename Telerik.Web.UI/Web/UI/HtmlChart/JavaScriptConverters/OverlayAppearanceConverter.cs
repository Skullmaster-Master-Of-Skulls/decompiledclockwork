using System;
using System.Collections.Generic;
using Telerik.Web.UI.HtmlChart.Appearance;
using Telerik.Web.UI.HtmlChart.Enums;

namespace Telerik.Web.UI.HtmlChart.JavaScriptConverters
{
	// Token: 0x020003D5 RID: 981
	public class OverlayAppearanceConverter : ExplicitJavaScriptConverter
	{
		// Token: 0x06002406 RID: 9222 RVA: 0x00077D7C File Offset: 0x00075F7C
		protected override void PopulateProperties(IDictionary<string, object> state, object obj)
		{
			OverlayAppearance overlayAppearance = (OverlayAppearance)obj;
			ExplicitJavaScriptConverter.AddProperty(state, "gradient", StringHelpers.ToCamelCase(overlayAppearance.Gradient.ToString()), StringHelpers.ToCamelCase(Gradients.Glass.ToString()));
		}

		// Token: 0x17000BB4 RID: 2996
		// (get) Token: 0x06002407 RID: 9223 RVA: 0x00077DC0 File Offset: 0x00075FC0
		public override IEnumerable<Type> SupportedTypes
		{
			get
			{
				return new Type[]
				{
					typeof(OverlayAppearance)
				};
			}
		}
	}
}
