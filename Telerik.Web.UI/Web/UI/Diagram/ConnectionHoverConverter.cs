using System;
using System.Collections.Generic;

namespace Telerik.Web.UI.Diagram
{
	// Token: 0x0200021C RID: 540
	public class ConnectionHoverConverter : ExplicitJavaScriptConverter
	{
		// Token: 0x060013D1 RID: 5073 RVA: 0x000458B8 File Offset: 0x00043AB8
		protected override void PopulateProperties(IDictionary<string, object> state, object obj)
		{
			ConnectionHover connectionHover = obj as ConnectionHover;
			ExplicitJavaScriptConverter.AddProperty(state, "stroke", connectionHover.StrokeSettings, null);
		}

		// Token: 0x17000688 RID: 1672
		// (get) Token: 0x060013D2 RID: 5074 RVA: 0x000458E0 File Offset: 0x00043AE0
		public override IEnumerable<Type> SupportedTypes
		{
			get
			{
				return new Type[]
				{
					typeof(ConnectionHover)
				};
			}
		}
	}
}
