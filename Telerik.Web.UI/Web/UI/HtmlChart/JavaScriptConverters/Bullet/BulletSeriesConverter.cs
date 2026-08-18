using System;
using System.Collections.Generic;

namespace Telerik.Web.UI.HtmlChart.JavaScriptConverters.Bullet
{
	// Token: 0x020003B7 RID: 951
	internal class BulletSeriesConverter : ExplicitJavaScriptConverter
	{
		// Token: 0x06002323 RID: 8995 RVA: 0x00075A48 File Offset: 0x00073C48
		protected override void PopulateProperties(IDictionary<string, object> state, object obj)
		{
			BulletSeries bulletSeries = (BulletSeries)obj;
			ExplicitJavaScriptConverter.AddProperty(state, "type", HtmlChartHelper.StringToLowerCamelCase(bulletSeries.Type.ToString()));
			ExplicitJavaScriptConverter.AddProperty(state, "appearance", bulletSeries.Appearance, null);
			ExplicitJavaScriptConverter.AddProperty(state, "color", HtmlChartHelper.ToSerializableColor(bulletSeries.Appearance.FillStyle.BackgroundColor), string.Empty);
			base.AddScript(state, "visual", bulletSeries.Appearance.Visual);
			ExplicitJavaScriptConverter.AddProperty(state, "axis", bulletSeries.AxisName, string.Empty);
			ExplicitJavaScriptConverter.AddProperty(state, "colorField", bulletSeries.ColorField, string.Empty);
			if (bulletSeries.IsDataBound)
			{
				ExplicitJavaScriptConverter.AddProperty(state, "data", bulletSeries.Data, string.Empty);
			}
			else if (bulletSeries.SeriesItems.Count > 0)
			{
				ExplicitJavaScriptConverter.AddProperty(state, "data", bulletSeries.SeriesItems);
			}
			ExplicitJavaScriptConverter.AddProperty(state, "name", bulletSeries.Name, string.Empty);
			ExplicitJavaScriptConverter.AddProperty(state, "tooltip", bulletSeries.TooltipsAppearance, null);
			ExplicitJavaScriptConverter.AddProperty(state, "visible", bulletSeries.Visible, true);
			ExplicitJavaScriptConverter.AddProperty(state, "visibleInLegend", bulletSeries.VisibleInLegend, true);
			ExplicitJavaScriptConverter.AddProperty(state, "zIndex", bulletSeries.ZIndex, null);
			ExplicitJavaScriptConverter.AddProperty(state, "target", bulletSeries.Target, null);
			ExplicitJavaScriptConverter.AddProperty(state, "currentField", bulletSeries.DataCurrentField, string.Empty);
			ExplicitJavaScriptConverter.AddProperty(state, "targetField", bulletSeries.DataTargetField, string.Empty);
		}

		// Token: 0x17000B60 RID: 2912
		// (get) Token: 0x06002324 RID: 8996 RVA: 0x00075BEC File Offset: 0x00073DEC
		public override IEnumerable<Type> SupportedTypes
		{
			get
			{
				return new Type[]
				{
					typeof(BulletSeries)
				};
			}
		}
	}
}
