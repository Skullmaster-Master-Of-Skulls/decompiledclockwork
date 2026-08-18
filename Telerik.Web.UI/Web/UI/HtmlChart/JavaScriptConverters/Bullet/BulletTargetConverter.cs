using System;
using System.Collections.Generic;
using System.Drawing;
using Telerik.Web.UI.HtmlChart.PlotArea.Appearance;

namespace Telerik.Web.UI.HtmlChart.JavaScriptConverters.Bullet
{
	// Token: 0x020003C0 RID: 960
	public class BulletTargetConverter : ExplicitJavaScriptConverter
	{
		// Token: 0x0600233E RID: 9022 RVA: 0x0007603C File Offset: 0x0007423C
		protected override void PopulateProperties(IDictionary<string, object> state, object obj)
		{
			BulletTargetAppearance bulletTargetAppearance = (BulletTargetAppearance)obj;
			ExplicitJavaScriptConverter.AddProperty(state, "border", bulletTargetAppearance.Border, null);
			if (bulletTargetAppearance.Color != Color.Empty)
			{
				ExplicitJavaScriptConverter.AddProperty(state, "color", HtmlChartHelper.ColorToHex(bulletTargetAppearance.Color));
			}
			ExplicitJavaScriptConverter.AddProperty(state, "line", bulletTargetAppearance.Line, null);
		}

		// Token: 0x17000B69 RID: 2921
		// (get) Token: 0x0600233F RID: 9023 RVA: 0x0007609C File Offset: 0x0007429C
		public override IEnumerable<Type> SupportedTypes
		{
			get
			{
				return new Type[]
				{
					typeof(BulletTargetAppearance)
				};
			}
		}
	}
}
