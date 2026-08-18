using System;
using System.Collections.Generic;

namespace Telerik.Web.UI.Diagram
{
	// Token: 0x02000258 RID: 600
	public class HandlesConverter : ExplicitJavaScriptConverter
	{
		// Token: 0x060015D3 RID: 5587 RVA: 0x0004A6FC File Offset: 0x000488FC
		protected override void PopulateProperties(IDictionary<string, object> state, object obj)
		{
			Handles handles = obj as Handles;
			ExplicitJavaScriptConverter.AddProperty(state, "fill", handles.Fill, "");
			ExplicitJavaScriptConverter.AddProperty(state, "fill", handles.FillSettings, null);
			ExplicitJavaScriptConverter.AddProperty(state, "height", handles.Height, 0.0);
			ExplicitJavaScriptConverter.AddProperty(state, "hover", handles.HoverSettings, null);
			ExplicitJavaScriptConverter.AddProperty(state, "stroke", handles.StrokeSettings, null);
			ExplicitJavaScriptConverter.AddProperty(state, "width", handles.Width, 0.0);
		}

		// Token: 0x17000774 RID: 1908
		// (get) Token: 0x060015D4 RID: 5588 RVA: 0x0004A7A4 File Offset: 0x000489A4
		public override IEnumerable<Type> SupportedTypes
		{
			get
			{
				return new Type[]
				{
					typeof(Handles)
				};
			}
		}
	}
}
