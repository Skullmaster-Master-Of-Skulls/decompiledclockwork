using System;
using System.Collections.Generic;

namespace Telerik.Web.UI.Diagram
{
	// Token: 0x02000450 RID: 1104
	public class ShapeHoverConverter : ExplicitJavaScriptConverter
	{
		// Token: 0x060027DE RID: 10206 RVA: 0x00081830 File Offset: 0x0007FA30
		protected override void PopulateProperties(IDictionary<string, object> state, object obj)
		{
			ShapeHover shapeHover = obj as ShapeHover;
			ExplicitJavaScriptConverter.AddProperty(state, "fill", shapeHover.Fill, "");
			ExplicitJavaScriptConverter.AddProperty(state, "fill", shapeHover.FillSettings, null);
		}

		// Token: 0x17000CEA RID: 3306
		// (get) Token: 0x060027DF RID: 10207 RVA: 0x0008186C File Offset: 0x0007FA6C
		public override IEnumerable<Type> SupportedTypes
		{
			get
			{
				return new Type[]
				{
					typeof(ShapeHover)
				};
			}
		}
	}
}
