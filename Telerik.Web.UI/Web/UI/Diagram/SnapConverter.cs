using System;
using System.Collections.Generic;

namespace Telerik.Web.UI.Diagram
{
	// Token: 0x02000268 RID: 616
	public class SnapConverter : ExplicitJavaScriptConverter
	{
		// Token: 0x0600164C RID: 5708 RVA: 0x0004BD60 File Offset: 0x00049F60
		protected override void PopulateProperties(IDictionary<string, object> state, object obj)
		{
			Snap snap = obj as Snap;
			ExplicitJavaScriptConverter.AddProperty(state, "size", snap.Size, 10.0);
		}

		// Token: 0x170007A3 RID: 1955
		// (get) Token: 0x0600164D RID: 5709 RVA: 0x0004BD98 File Offset: 0x00049F98
		public override IEnumerable<Type> SupportedTypes
		{
			get
			{
				return new Type[]
				{
					typeof(Snap)
				};
			}
		}
	}
}
