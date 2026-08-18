using System;
using System.Collections.Generic;

namespace Telerik.Web.UI.Diagram
{
	// Token: 0x020002B4 RID: 692
	public class StrokeConverter : ExplicitJavaScriptConverter
	{
		// Token: 0x06001853 RID: 6227 RVA: 0x00050448 File Offset: 0x0004E648
		protected override void PopulateProperties(IDictionary<string, object> state, object obj)
		{
			Stroke stroke = obj as Stroke;
			ExplicitJavaScriptConverter.AddProperty(state, "color", stroke.Color, "Black");
			ExplicitJavaScriptConverter.AddProperty(state, "dashType", stroke.DashType, "");
			ExplicitJavaScriptConverter.AddProperty(state, "width", stroke.Width, 1.0);
		}

		// Token: 0x1700084F RID: 2127
		// (get) Token: 0x06001854 RID: 6228 RVA: 0x000504AC File Offset: 0x0004E6AC
		public override IEnumerable<Type> SupportedTypes
		{
			get
			{
				return new Type[]
				{
					typeof(Stroke)
				};
			}
		}
	}
}
