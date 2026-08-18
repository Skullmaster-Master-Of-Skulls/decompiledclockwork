using System;
using System.Collections.Generic;

namespace Telerik.Web.UI.Diagram
{
	// Token: 0x0200024E RID: 590
	public class DragConverter : ExplicitJavaScriptConverter
	{
		// Token: 0x06001590 RID: 5520 RVA: 0x00049CE0 File Offset: 0x00047EE0
		protected override void PopulateProperties(IDictionary<string, object> state, object obj)
		{
			Drag drag = obj as Drag;
			ExplicitJavaScriptConverter.AddProperty(state, "snap", drag.Snap, true);
			if (drag.Snap)
			{
				ExplicitJavaScriptConverter.AddProperty(state, "snap", drag.SnapSettings, null);
			}
		}

		// Token: 0x17000758 RID: 1880
		// (get) Token: 0x06001591 RID: 5521 RVA: 0x00049D2C File Offset: 0x00047F2C
		public override IEnumerable<Type> SupportedTypes
		{
			get
			{
				return new Type[]
				{
					typeof(Drag)
				};
			}
		}
	}
}
