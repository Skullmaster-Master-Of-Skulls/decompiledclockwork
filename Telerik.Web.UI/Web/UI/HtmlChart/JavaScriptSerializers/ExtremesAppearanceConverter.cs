using System;
using System.Collections.Generic;
using Telerik.Web.UI.HtmlChart.Appearance;

namespace Telerik.Web.UI.HtmlChart.JavaScriptSerializers
{
	// Token: 0x020004EB RID: 1259
	internal class ExtremesAppearanceConverter : OutliersAppearanceConverter
	{
		// Token: 0x06002CFA RID: 11514 RVA: 0x00093CA8 File Offset: 0x00091EA8
		protected override void SerializeMarkersType(OutliersAppearance outliers, IDictionary<string, object> state)
		{
			ExtremesAppearance extremesAppearance = outliers as ExtremesAppearance;
			if (extremesAppearance != null)
			{
				ExplicitJavaScriptConverter.AddProperty(state, "type", extremesAppearance.MarkersType.ToString().ToLower(), OutliersMarkersType.Circle.ToString().ToLower());
			}
		}
	}
}
