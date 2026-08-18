using System;
using System.Collections.Generic;

namespace Telerik.Web.UI.Diagram
{
	// Token: 0x0200044C RID: 1100
	public class ShapeContentConverter : ExplicitJavaScriptConverter
	{
		// Token: 0x060027A7 RID: 10151 RVA: 0x00080C60 File Offset: 0x0007EE60
		protected override void PopulateProperties(IDictionary<string, object> state, object obj)
		{
			ShapeContent shapeContent = obj as ShapeContent;
			ExplicitJavaScriptConverter.AddProperty(state, "align", shapeContent.Align, "");
			ExplicitJavaScriptConverter.AddProperty(state, "color", shapeContent.Color, "");
			ExplicitJavaScriptConverter.AddProperty(state, "fontFamily", shapeContent.FontFamily, "");
			ExplicitJavaScriptConverter.AddProperty(state, "fontSize", shapeContent.FontSize, 0.0);
			ExplicitJavaScriptConverter.AddProperty(state, "fontStyle", shapeContent.FontStyle, "");
			ExplicitJavaScriptConverter.AddProperty(state, "fontWeight", shapeContent.FontWeight, "");
			ExplicitJavaScriptConverter.AddProperty(state, "text", shapeContent.Text, "");
			ExplicitJavaScriptConverter.AddProperty(state, "html", shapeContent.Html, "");
		}

		// Token: 0x17000CCF RID: 3279
		// (get) Token: 0x060027A8 RID: 10152 RVA: 0x00080D34 File Offset: 0x0007EF34
		public override IEnumerable<Type> SupportedTypes
		{
			get
			{
				return new Type[]
				{
					typeof(ShapeContent)
				};
			}
		}
	}
}
