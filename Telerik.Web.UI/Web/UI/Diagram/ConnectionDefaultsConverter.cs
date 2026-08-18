using System;
using System.Collections.Generic;

namespace Telerik.Web.UI.Diagram
{
	// Token: 0x02000217 RID: 535
	public class ConnectionDefaultsConverter : ExplicitJavaScriptConverter
	{
		// Token: 0x060013BE RID: 5054 RVA: 0x00045554 File Offset: 0x00043754
		protected override void PopulateProperties(IDictionary<string, object> state, object obj)
		{
			ConnectionDefaults connectionDefaults = obj as ConnectionDefaults;
			ExplicitJavaScriptConverter.AddProperty(state, "content", connectionDefaults.ContentSettings, null);
			ExplicitJavaScriptConverter.AddProperty(state, "editable", connectionDefaults.Editable, true);
			if (connectionDefaults.Editable)
			{
				ExplicitJavaScriptConverter.AddProperty(state, "editable", connectionDefaults.EditableSettings, null);
			}
			ExplicitJavaScriptConverter.AddProperty(state, "endCap", connectionDefaults.EndCap.ToString(), ConnectionEndCap.None.ToString());
			ExplicitJavaScriptConverter.AddProperty(state, "endCap", connectionDefaults.EndCapSettings, null);
			ExplicitJavaScriptConverter.AddProperty(state, "fromConnector", connectionDefaults.FromConnector, "Auto");
			ExplicitJavaScriptConverter.AddProperty(state, "hover", connectionDefaults.HoverSettings, null);
			ExplicitJavaScriptConverter.AddProperty(state, "selectable", connectionDefaults.Selectable, true);
			ExplicitJavaScriptConverter.AddProperty(state, "selection", connectionDefaults.SelectionSettings, null);
			ExplicitJavaScriptConverter.AddProperty(state, "startCap", connectionDefaults.StartCap.ToString(), ConnectionStartCap.None.ToString());
			ExplicitJavaScriptConverter.AddProperty(state, "startCap", connectionDefaults.StartCapSettings, null);
			ExplicitJavaScriptConverter.AddProperty(state, "stroke", connectionDefaults.StrokeSettings, null);
			ExplicitJavaScriptConverter.AddProperty(state, "toConnector", connectionDefaults.ToConnector, "Auto");
			ExplicitJavaScriptConverter.AddProperty(state, "type", StringHelpers.ToCamelCase(connectionDefaults.Type.ToString()), StringHelpers.ToCamelCase(ConnectionType.Cascading.ToString()));
		}

		// Token: 0x17000681 RID: 1665
		// (get) Token: 0x060013BF RID: 5055 RVA: 0x000456D0 File Offset: 0x000438D0
		public override IEnumerable<Type> SupportedTypes
		{
			get
			{
				return new Type[]
				{
					typeof(ConnectionDefaults)
				};
			}
		}
	}
}
