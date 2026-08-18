using System;
using System.Collections.Generic;

namespace Telerik.Web.UI.MultiColumnComboBox
{
	// Token: 0x020005F1 RID: 1521
	public class OpenConverter : ExplicitJavaScriptConverter
	{
		// Token: 0x06003712 RID: 14098 RVA: 0x000B63E4 File Offset: 0x000B45E4
		protected override void PopulateProperties(IDictionary<string, object> state, object obj)
		{
			Open open = obj as Open;
			ExplicitJavaScriptConverter.AddProperty(state, "effects", open.Effects, "");
			ExplicitJavaScriptConverter.AddProperty(state, "duration", open.Duration, 200.0);
		}

		// Token: 0x1700120E RID: 4622
		// (get) Token: 0x06003713 RID: 14099 RVA: 0x000B6434 File Offset: 0x000B4634
		public override IEnumerable<Type> SupportedTypes
		{
			get
			{
				return new Type[]
				{
					typeof(Open)
				};
			}
		}
	}
}
