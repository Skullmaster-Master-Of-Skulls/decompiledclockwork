using System;
using System.Collections.Generic;

namespace Telerik.Web.UI.MultiSelect
{
	// Token: 0x02000615 RID: 1557
	public class VirtualConverter : ExplicitJavaScriptConverter
	{
		// Token: 0x06003887 RID: 14471 RVA: 0x000BA284 File Offset: 0x000B8484
		protected override void PopulateProperties(IDictionary<string, object> state, object obj)
		{
			Virtual @virtual = obj as Virtual;
			ExplicitJavaScriptConverter.AddProperty(state, "itemHeight", @virtual.ItemHeight, 0.0);
			ExplicitJavaScriptConverter.AddProperty(state, "mapValueTo", @virtual.MapValueTo, "index");
			base.AddScript(state, "valueMapper", @virtual.ValueMapper);
		}

		// Token: 0x17001292 RID: 4754
		// (get) Token: 0x06003888 RID: 14472 RVA: 0x000BA2E4 File Offset: 0x000B84E4
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
