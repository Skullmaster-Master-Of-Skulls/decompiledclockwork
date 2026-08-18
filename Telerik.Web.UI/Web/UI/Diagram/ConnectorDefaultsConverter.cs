using System;
using System.Collections.Generic;

namespace Telerik.Web.UI.Diagram
{
	// Token: 0x02000222 RID: 546
	public class ConnectorDefaultsConverter : ExplicitJavaScriptConverter
	{
		// Token: 0x060013F0 RID: 5104 RVA: 0x00045D50 File Offset: 0x00043F50
		protected override void PopulateProperties(IDictionary<string, object> state, object obj)
		{
			ConnectorDefaults connectorDefaults = obj as ConnectorDefaults;
			ExplicitJavaScriptConverter.AddProperty(state, "width", connectorDefaults.Width, 8.0);
			ExplicitJavaScriptConverter.AddProperty(state, "height", connectorDefaults.Height, 8.0);
			ExplicitJavaScriptConverter.AddProperty(state, "hover", connectorDefaults.HoverSettings, null);
			ExplicitJavaScriptConverter.AddProperty(state, "fill", connectorDefaults.Fill, "");
			ExplicitJavaScriptConverter.AddProperty(state, "fill", connectorDefaults.FillSettings, null);
			ExplicitJavaScriptConverter.AddProperty(state, "stroke", connectorDefaults.Stroke, "");
			ExplicitJavaScriptConverter.AddProperty(state, "stroke", connectorDefaults.StrokeSettings, null);
		}

		// Token: 0x17000696 RID: 1686
		// (get) Token: 0x060013F1 RID: 5105 RVA: 0x00045E10 File Offset: 0x00044010
		public override IEnumerable<Type> SupportedTypes
		{
			get
			{
				return new Type[]
				{
					typeof(ConnectorDefaults)
				};
			}
		}
	}
}
