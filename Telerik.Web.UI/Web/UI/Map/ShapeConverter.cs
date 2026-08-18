using System;
using System.Collections.Generic;

namespace Telerik.Web.UI.Map
{
	// Token: 0x020005B0 RID: 1456
	public class ShapeConverter : ExplicitJavaScriptConverter
	{
		// Token: 0x06003407 RID: 13319 RVA: 0x000ACC40 File Offset: 0x000AAE40
		protected override void PopulateProperties(IDictionary<string, object> state, object obj)
		{
			Shape shape = obj as Shape;
			ExplicitJavaScriptConverter.AddProperty(state, "attribution", shape.Attribution, "");
			ExplicitJavaScriptConverter.AddProperty(state, "opacity", shape.Opacity, 1.0);
			ExplicitJavaScriptConverter.AddProperty(state, "style", shape.StyleSettings, null);
		}

		// Token: 0x170010F4 RID: 4340
		// (get) Token: 0x06003408 RID: 13320 RVA: 0x000ACCA0 File Offset: 0x000AAEA0
		public override IEnumerable<Type> SupportedTypes
		{
			get
			{
				return new Type[]
				{
					typeof(Shape)
				};
			}
		}
	}
}
