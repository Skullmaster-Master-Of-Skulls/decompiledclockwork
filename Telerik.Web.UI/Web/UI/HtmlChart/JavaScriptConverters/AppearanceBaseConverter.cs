using System;
using System.Collections.Generic;
using Telerik.Web.UI.HtmlChart.JavaScriptSerializers;
using Telerik.Web.UI.HtmlChart.PlotArea;

namespace Telerik.Web.UI.HtmlChart.JavaScriptConverters
{
	// Token: 0x020003BB RID: 955
	internal class AppearanceBaseConverter : BorderAppearanceConverter
	{
		// Token: 0x0600232F RID: 9007 RVA: 0x00075DC4 File Offset: 0x00073FC4
		protected override void PopulateProperties(IDictionary<string, object> state, object obj)
		{
			AppearanceBase appearanceBase = (AppearanceBase)obj;
			ExplicitJavaScriptConverter.AddProperty(state, "visible", appearanceBase.Visible, null);
			ExplicitJavaScriptConverter.AddProperty(state, "rotationAngle", appearanceBase.RotationAngle, 0);
		}

		// Token: 0x17000B64 RID: 2916
		// (get) Token: 0x06002330 RID: 9008 RVA: 0x00075E0C File Offset: 0x0007400C
		public override IEnumerable<Type> SupportedTypes
		{
			get
			{
				return new Type[]
				{
					typeof(AppearanceBase)
				};
			}
		}
	}
}
