using System;
using System.Collections.Generic;

namespace Telerik.Web.UI.Diagram
{
	// Token: 0x020002B0 RID: 688
	public class SelectionConverter : ExplicitJavaScriptConverter
	{
		// Token: 0x0600183C RID: 6204 RVA: 0x00050184 File Offset: 0x0004E384
		protected override void PopulateProperties(IDictionary<string, object> state, object obj)
		{
			Selection selection = obj as Selection;
			ExplicitJavaScriptConverter.AddProperty(state, "handles", selection.HandlesSettings, null);
		}

		// Token: 0x17000846 RID: 2118
		// (get) Token: 0x0600183D RID: 6205 RVA: 0x000501AC File Offset: 0x0004E3AC
		public override IEnumerable<Type> SupportedTypes
		{
			get
			{
				return new Type[]
				{
					typeof(Selection)
				};
			}
		}
	}
}
