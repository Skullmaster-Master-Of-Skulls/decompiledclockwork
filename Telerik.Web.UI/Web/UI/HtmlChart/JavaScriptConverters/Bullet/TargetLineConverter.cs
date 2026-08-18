using System;
using System.Collections.Generic;
using Telerik.Web.UI.HtmlChart.PlotArea.Appearance;

namespace Telerik.Web.UI.HtmlChart.JavaScriptConverters.Bullet
{
	// Token: 0x020003C1 RID: 961
	public class TargetLineConverter : ExplicitJavaScriptConverter
	{
		// Token: 0x06002341 RID: 9025 RVA: 0x000760C8 File Offset: 0x000742C8
		protected override void PopulateProperties(IDictionary<string, object> state, object obj)
		{
			TargetLineAppearance targetLineAppearance = (TargetLineAppearance)obj;
			ExplicitJavaScriptConverter.AddProperty(state, "width", targetLineAppearance.Width, null);
		}

		// Token: 0x17000B6A RID: 2922
		// (get) Token: 0x06002342 RID: 9026 RVA: 0x000760F4 File Offset: 0x000742F4
		public override IEnumerable<Type> SupportedTypes
		{
			get
			{
				return new Type[]
				{
					typeof(TargetLineAppearance)
				};
			}
		}
	}
}
