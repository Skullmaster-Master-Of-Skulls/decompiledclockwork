using System;
using System.Collections.Generic;

namespace Telerik.Web.UI.MultiColumnComboBox
{
	// Token: 0x020005F7 RID: 1527
	public class VirtualConverter : ExplicitJavaScriptConverter
	{
		// Token: 0x06003734 RID: 14132 RVA: 0x000B6C78 File Offset: 0x000B4E78
		protected override void PopulateProperties(IDictionary<string, object> state, object obj)
		{
			Virtual @virtual = obj as Virtual;
			ExplicitJavaScriptConverter.AddProperty(state, "itemHeight", @virtual.ItemHeight, 0.0);
			ExplicitJavaScriptConverter.AddProperty(state, "mapValueTo", @virtual.MapValueTo, "index");
			base.AddScript(state, "valueMapper", @virtual.ValueMapper);
		}

		// Token: 0x1700121D RID: 4637
		// (get) Token: 0x06003735 RID: 14133 RVA: 0x000B6CD8 File Offset: 0x000B4ED8
		public override IEnumerable<Type> SupportedTypes
		{
			get
			{
				return new Type[]
				{
					typeof(Virtual)
				};
			}
		}
	}
}
