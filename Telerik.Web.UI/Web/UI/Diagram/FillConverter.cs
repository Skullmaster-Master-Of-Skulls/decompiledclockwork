using System;
using System.Collections.Generic;

namespace Telerik.Web.UI.Diagram
{
	// Token: 0x02000253 RID: 595
	public class FillConverter : ExplicitJavaScriptConverter
	{
		// Token: 0x060015AD RID: 5549 RVA: 0x0004A110 File Offset: 0x00048310
		protected override void PopulateProperties(IDictionary<string, object> state, object obj)
		{
			Fill fill = obj as Fill;
			ExplicitJavaScriptConverter.AddProperty(state, "color", fill.Color, "");
			ExplicitJavaScriptConverter.AddProperty(state, "opacity", fill.Opacity, 1.0);
		}

		// Token: 0x17000764 RID: 1892
		// (get) Token: 0x060015AE RID: 5550 RVA: 0x0004A160 File Offset: 0x00048360
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
