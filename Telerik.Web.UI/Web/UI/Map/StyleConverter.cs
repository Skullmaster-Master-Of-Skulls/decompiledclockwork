using System;
using System.Collections.Generic;

namespace Telerik.Web.UI.Map
{
	// Token: 0x020005B4 RID: 1460
	public class StyleConverter : ExplicitJavaScriptConverter
	{
		// Token: 0x0600341F RID: 13343 RVA: 0x000ACFE4 File Offset: 0x000AB1E4
		protected override void PopulateProperties(IDictionary<string, object> state, object obj)
		{
			Style style = obj as Style;
			ExplicitJavaScriptConverter.AddProperty(state, "fill", style.FillSettings, null);
			ExplicitJavaScriptConverter.AddProperty(state, "stroke", style.StrokeSettings, null);
		}

		// Token: 0x170010FE RID: 4350
		// (get) Token: 0x06003420 RID: 13344 RVA: 0x000AD01C File Offset: 0x000AB21C
		public override IEnumerable<Type> SupportedTypes
		{
			get
			{
				return new Type[]
				{
					typeof(Style)
				};
			}
		}
	}
}
