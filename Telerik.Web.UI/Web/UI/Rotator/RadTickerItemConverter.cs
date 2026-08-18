using System;
using System.Collections.Generic;

namespace Telerik.Web.UI.Rotator
{
	// Token: 0x020007DA RID: 2010
	public class RadTickerItemConverter : ExplicitJavaScriptConverter
	{
		// Token: 0x170016A0 RID: 5792
		// (get) Token: 0x06004619 RID: 17945 RVA: 0x000DC3BC File Offset: 0x000DA5BC
		public override IEnumerable<Type> SupportedTypes
		{
			get
			{
				return new Type[]
				{
					typeof(RadTickerItem)
				};
			}
		}

		// Token: 0x0600461A RID: 17946 RVA: 0x000DC3E0 File Offset: 0x000DA5E0
		protected override void PopulateProperties(IDictionary<string, object> state, object obj)
		{
			RadTickerItem radTickerItem = obj as RadTickerItem;
			ExplicitJavaScriptConverter.AddProperty(state, "cssClass", radTickerItem.CssClass, string.Empty);
			ExplicitJavaScriptConverter.AddProperty(state, "visible", radTickerItem.Visible, true);
			ExplicitJavaScriptConverter.AddProperty(state, "navigateUrl", radTickerItem.NavigateUrl, string.Empty);
			ExplicitJavaScriptConverter.AddProperty(state, "target", radTickerItem.Target, string.Empty);
			ExplicitJavaScriptConverter.AddProperty(state, "text", radTickerItem.Text, string.Empty);
		}
	}
}
