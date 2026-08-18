using System;
using System.Collections.Generic;

namespace Telerik.Web.UI.Diagram
{
	// Token: 0x0200021F RID: 543
	public class ConnectionStrokeConverter : ExplicitJavaScriptConverter
	{
		// Token: 0x060013DC RID: 5084 RVA: 0x000459F0 File Offset: 0x00043BF0
		protected override void PopulateProperties(IDictionary<string, object> state, object obj)
		{
			ConnectionStroke connectionStroke = obj as ConnectionStroke;
			ExplicitJavaScriptConverter.AddProperty(state, "color", connectionStroke.Color, "");
			ExplicitJavaScriptConverter.AddProperty(state, "width", connectionStroke.Width, 1.0);
			ExplicitJavaScriptConverter.AddProperty(state, "dashType", StringHelpers.ToCamelCase(connectionStroke.DashType.ToString()), StringHelpers.ToCamelCase(StrokeDashType.Solid.ToString()));
		}

		// Token: 0x1700068D RID: 1677
		// (get) Token: 0x060013DD RID: 5085 RVA: 0x00045A70 File Offset: 0x00043C70
		public override IEnumerable<Type> SupportedTypes
		{
			get
			{
				return new Type[]
				{
					typeof(ConnectionStroke)
				};
			}
		}
	}
}
