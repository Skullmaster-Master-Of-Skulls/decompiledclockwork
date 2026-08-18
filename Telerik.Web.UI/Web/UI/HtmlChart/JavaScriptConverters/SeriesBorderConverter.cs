using System;
using System.Collections.Generic;
using Telerik.Web.UI.HtmlChart.Appearance;

namespace Telerik.Web.UI.HtmlChart.JavaScriptConverters
{
	// Token: 0x02000053 RID: 83
	internal class SeriesBorderConverter : DashedBorderConverter
	{
		// Token: 0x06000286 RID: 646 RVA: 0x00006EF0 File Offset: 0x000050F0
		protected override void PopulateProperties(IDictionary<string, object> state, object obj)
		{
			base.PopulateProperties(state, obj);
			SeriesBorderAppearance seriesBorderAppearance = (SeriesBorderAppearance)obj;
			state["width"] = seriesBorderAppearance.Width.Value;
			ExplicitJavaScriptConverter.AddProperty(state, "opacity", seriesBorderAppearance.Opacity, 1.0);
		}

		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x06000287 RID: 647 RVA: 0x00006F50 File Offset: 0x00005150
		public override IEnumerable<Type> SupportedTypes
		{
			get
			{
				return new Type[]
				{
					typeof(SeriesBorderAppearance)
				};
			}
		}
	}
}
