using System;
using System.Collections.Generic;

namespace Telerik.Web.UI.Diagram
{
	// Token: 0x0200021A RID: 538
	public class ConnectionEndPointConverter : ExplicitJavaScriptConverter
	{
		// Token: 0x060013C7 RID: 5063 RVA: 0x00045790 File Offset: 0x00043990
		protected override void PopulateProperties(IDictionary<string, object> state, object obj)
		{
			ConnectionEndPoint connectionEndPoint = obj as ConnectionEndPoint;
			ExplicitJavaScriptConverter.AddProperty(state, "shapeId", connectionEndPoint.ShapeId, "");
			ExplicitJavaScriptConverter.AddProperty(state, "connector", connectionEndPoint.Connector, "");
		}

		// Token: 0x17000685 RID: 1669
		// (get) Token: 0x060013C8 RID: 5064 RVA: 0x000457D0 File Offset: 0x000439D0
		public override IEnumerable<Type> SupportedTypes
		{
			get
			{
				return new Type[]
				{
					typeof(ConnectionEndPoint)
				};
			}
		}
	}
}
