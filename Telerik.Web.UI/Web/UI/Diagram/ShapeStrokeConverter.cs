using System;
using System.Collections.Generic;

namespace Telerik.Web.UI.Diagram
{
	// Token: 0x02000454 RID: 1108
	public class ShapeStrokeConverter : ExplicitJavaScriptConverter
	{
		// Token: 0x060027F0 RID: 10224 RVA: 0x00081A3C File Offset: 0x0007FC3C
		protected override void PopulateProperties(IDictionary<string, object> state, object obj)
		{
			ShapeStroke shapeStroke = obj as ShapeStroke;
			ExplicitJavaScriptConverter.AddProperty(state, "color", shapeStroke.Color, "");
			ExplicitJavaScriptConverter.AddProperty(state, "width", shapeStroke.Width, 1.0);
			ExplicitJavaScriptConverter.AddProperty(state, "dashType", StringHelpers.ToCamelCase(shapeStroke.DashType.ToString()), StringHelpers.ToCamelCase(StrokeDashType.Solid.ToString()));
		}

		// Token: 0x17000CF2 RID: 3314
		// (get) Token: 0x060027F1 RID: 10225 RVA: 0x00081ABC File Offset: 0x0007FCBC
		public override IEnumerable<Type> SupportedTypes
		{
			get
			{
				return new Type[]
				{
					typeof(ShapeStroke)
				};
			}
		}
	}
}
