using System;
using System.Collections.Generic;

namespace Telerik.Web.UI.PdfViewer
{
	// Token: 0x02000656 RID: 1622
	public class ToolBarConverter : ExplicitJavaScriptConverter
	{
		// Token: 0x06003B97 RID: 15255 RVA: 0x000C20F4 File Offset: 0x000C02F4
		protected override void PopulateProperties(IDictionary<string, object> state, object obj)
		{
			ToolBar toolBar = obj as ToolBar;
			ExplicitJavaScriptConverter.AddProperty(state, "items", toolBar.Items, toolBar.defaultItems);
		}

		// Token: 0x17001397 RID: 5015
		// (get) Token: 0x06003B98 RID: 15256 RVA: 0x000C2120 File Offset: 0x000C0320
		public override IEnumerable<Type> SupportedTypes
		{
			get
			{
				return new Type[]
				{
					typeof(ToolBar)
				};
			}
		}
	}
}
