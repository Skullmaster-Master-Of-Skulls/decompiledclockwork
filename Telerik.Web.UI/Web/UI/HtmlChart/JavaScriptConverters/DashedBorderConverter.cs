using System;
using System.Collections.Generic;
using Telerik.Web.UI.HtmlChart.Appearance;
using Telerik.Web.UI.HtmlChart.Enums;
using Telerik.Web.UI.HtmlChart.JavaScriptSerializers;

namespace Telerik.Web.UI.HtmlChart.JavaScriptConverters
{
	// Token: 0x02000052 RID: 82
	internal class DashedBorderConverter : BorderAppearanceConverter
	{
		// Token: 0x06000283 RID: 643 RVA: 0x00006E78 File Offset: 0x00005078
		protected override void PopulateProperties(IDictionary<string, object> state, object obj)
		{
			base.PopulateProperties(state, obj);
			DashedBorderAppearance dashedBorderAppearance = (DashedBorderAppearance)obj;
			ExplicitJavaScriptConverter.AddProperty(state, "dashType", HtmlChartHelper.StringToLowerCamelCase(dashedBorderAppearance.DashType.ToString()), DashType.Solid.ToString().ToLower());
		}

		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x06000284 RID: 644 RVA: 0x00006EC4 File Offset: 0x000050C4
		public override IEnumerable<Type> SupportedTypes
		{
			get
			{
				return new Type[]
				{
					typeof(DashedBorderAppearance)
				};
			}
		}
	}
}
