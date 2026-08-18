using System;
using System.Collections.Generic;

namespace Telerik.Web.UI.Diagram
{
	// Token: 0x0200025F RID: 607
	public class MarginConverter : ExplicitJavaScriptConverter
	{
		// Token: 0x060015F5 RID: 5621 RVA: 0x0004AC5C File Offset: 0x00048E5C
		protected override void PopulateProperties(IDictionary<string, object> state, object obj)
		{
			Margin margin = obj as Margin;
			ExplicitJavaScriptConverter.AddProperty(state, "bottom", margin.Bottom, 0.0);
			ExplicitJavaScriptConverter.AddProperty(state, "left", margin.Left, 0.0);
			ExplicitJavaScriptConverter.AddProperty(state, "right", margin.Right, 0.0);
			ExplicitJavaScriptConverter.AddProperty(state, "top", margin.Top, 0.0);
		}

		// Token: 0x17000780 RID: 1920
		// (get) Token: 0x060015F6 RID: 5622 RVA: 0x0004AD00 File Offset: 0x00048F00
		public override IEnumerable<Type> SupportedTypes
		{
			get
			{
				return new Type[]
				{
					typeof(Margin)
				};
			}
		}
	}
}
