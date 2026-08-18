using System;
using System.Collections.Generic;

namespace Telerik.Web.UI.Diagram
{
	// Token: 0x02000255 RID: 597
	public class GradientConverter : ExplicitJavaScriptConverter
	{
		// Token: 0x060015C1 RID: 5569 RVA: 0x0004A398 File Offset: 0x00048598
		protected override void PopulateProperties(IDictionary<string, object> state, object obj)
		{
			Gradient gradient = obj as Gradient;
			ExplicitJavaScriptConverter.AddProperty(state, "center", gradient.Center, null);
			ExplicitJavaScriptConverter.AddProperty(state, "radius", gradient.Radius, 1.0);
			ExplicitJavaScriptConverter.AddProperty(state, "start", gradient.Start, null);
			ExplicitJavaScriptConverter.AddProperty(state, "end", gradient.End, null);
			if (gradient.StopsCollection.Count != 0)
			{
				ExplicitJavaScriptConverter.AddProperty(state, "stops", gradient.StopsCollection.ItemsList, null);
			}
			ExplicitJavaScriptConverter.AddProperty(state, "type", StringHelpers.ToCamelCase(gradient.Type.ToString()), StringHelpers.ToCamelCase(GradientType.Linear.ToString()));
		}

		// Token: 0x1700076C RID: 1900
		// (get) Token: 0x060015C2 RID: 5570 RVA: 0x0004A45C File Offset: 0x0004865C
		public override IEnumerable<Type> SupportedTypes
		{
			get
			{
				return new Type[]
				{
					typeof(Gradient)
				};
			}
		}
	}
}
