using System;
using System.Collections.Generic;

namespace Telerik.Web.UI.MultiColumnComboBox
{
	// Token: 0x020005F3 RID: 1523
	public class PopupConverter : ExplicitJavaScriptConverter
	{
		// Token: 0x0600371D RID: 14109 RVA: 0x000B6538 File Offset: 0x000B4738
		protected override void PopulateProperties(IDictionary<string, object> state, object obj)
		{
			Popup popup = obj as Popup;
			ExplicitJavaScriptConverter.AddProperty(state, "appendTo", popup.AppendTo, "");
			ExplicitJavaScriptConverter.AddProperty(state, "origin", popup.Origin, "");
			ExplicitJavaScriptConverter.AddProperty(state, "position", popup.Position, "");
		}

		// Token: 0x17001213 RID: 4627
		// (get) Token: 0x0600371E RID: 14110 RVA: 0x000B6590 File Offset: 0x000B4790
		public override IEnumerable<Type> SupportedTypes
		{
			get
			{
				return new Type[]
				{
					typeof(Popup)
				};
			}
		}
	}
}
