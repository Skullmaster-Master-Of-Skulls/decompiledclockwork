using System;
using System.Collections.Generic;

namespace Telerik.Web.UI.Diagram
{
	// Token: 0x02000266 RID: 614
	public class ShapeFillConverter : ExplicitJavaScriptConverter
	{
		// Token: 0x06001645 RID: 5701 RVA: 0x0004BC78 File Offset: 0x00049E78
		protected override void PopulateProperties(IDictionary<string, object> state, object obj)
		{
			ShapeFill shapeFill = obj as ShapeFill;
			ExplicitJavaScriptConverter.AddProperty(state, "color", shapeFill.Color, "");
			ExplicitJavaScriptConverter.AddProperty(state, "opacity", shapeFill.Opacity, 1.0);
			ExplicitJavaScriptConverter.AddProperty(state, "gradient", shapeFill.GradientSettings, null);
		}

		// Token: 0x170007A0 RID: 1952
		// (get) Token: 0x06001646 RID: 5702 RVA: 0x0004BCD8 File Offset: 0x00049ED8
		public override IEnumerable<Type> SupportedTypes
		{
			get
			{
				return new Type[]
				{
					typeof(ShapeFill)
				};
			}
		}
	}
}
