using System;
using System.Collections.Generic;
using System.Drawing;
using System.Web.UI.WebControls;
using Telerik.Web.UI.HtmlChart.Appearance;

namespace Telerik.Web.UI.HtmlChart.JavaScriptSerializers
{
	// Token: 0x02000051 RID: 81
	internal class BorderAppearanceConverter : ExplicitJavaScriptConverter
	{
		// Token: 0x06000280 RID: 640 RVA: 0x00006DC8 File Offset: 0x00004FC8
		protected override void PopulateProperties(IDictionary<string, object> state, object obj)
		{
			BorderAppearance borderAppearance = obj as BorderAppearance;
			if (borderAppearance.Color != Color.Empty)
			{
				ExplicitJavaScriptConverter.AddProperty(state, "color", HtmlChartHelper.ColorToHex(borderAppearance.Color), Color.Empty);
			}
			if (borderAppearance.Width != Unit.Empty)
			{
				ExplicitJavaScriptConverter.AddProperty(state, "width", borderAppearance.Width.ToString(), Unit.Empty);
			}
		}

		// Token: 0x170000EF RID: 239
		// (get) Token: 0x06000281 RID: 641 RVA: 0x00006E4C File Offset: 0x0000504C
		public override IEnumerable<Type> SupportedTypes
		{
			get
			{
				return new Type[]
				{
					typeof(BorderAppearance)
				};
			}
		}
	}
}
