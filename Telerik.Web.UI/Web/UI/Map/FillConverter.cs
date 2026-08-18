using System;
using System.Collections.Generic;

namespace Telerik.Web.UI.Map
{
	// Token: 0x02000599 RID: 1433
	public class FillConverter : ExplicitJavaScriptConverter
	{
		// Token: 0x06003369 RID: 13161 RVA: 0x000AB2E8 File Offset: 0x000A94E8
		protected override void PopulateProperties(IDictionary<string, object> state, object obj)
		{
			Fill fill = obj as Fill;
			ExplicitJavaScriptConverter.AddProperty(state, "color", fill.Color, "");
			ExplicitJavaScriptConverter.AddProperty(state, "opacity", fill.Opacity, 0.0);
		}

		// Token: 0x170010B1 RID: 4273
		// (get) Token: 0x0600336A RID: 13162 RVA: 0x000AB338 File Offset: 0x000A9538
		public override IEnumerable<Type> SupportedTypes
		{
			get
			{
				return new Type[]
				{
					typeof(Fill)
				};
			}
		}
	}
}
