using System;
using System.Collections.Generic;

namespace Telerik.Web.UI.Diagram
{
	// Token: 0x0200025B RID: 603
	public class HoverConverter : ExplicitJavaScriptConverter
	{
		// Token: 0x060015E8 RID: 5608 RVA: 0x0004AA6C File Offset: 0x00048C6C
		protected override void PopulateProperties(IDictionary<string, object> state, object obj)
		{
			Hover hover = obj as Hover;
			ExplicitJavaScriptConverter.AddProperty(state, "fill", hover.Fill, "");
			ExplicitJavaScriptConverter.AddProperty(state, "fill", hover.FillSettings, null);
			ExplicitJavaScriptConverter.AddProperty(state, "stroke", hover.Stroke, "");
			ExplicitJavaScriptConverter.AddProperty(state, "stroke", hover.StrokeSettings, null);
		}

		// Token: 0x1700077A RID: 1914
		// (get) Token: 0x060015E9 RID: 5609 RVA: 0x0004AAD0 File Offset: 0x00048CD0
		public override IEnumerable<Type> SupportedTypes
		{
			get
			{
				return new Type[]
				{
					typeof(Hover)
				};
			}
		}
	}
}
