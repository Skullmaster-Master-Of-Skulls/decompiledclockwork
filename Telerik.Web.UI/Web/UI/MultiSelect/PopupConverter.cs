using System;
using System.Collections.Generic;

namespace Telerik.Web.UI.MultiSelect
{
	// Token: 0x02000612 RID: 1554
	public class PopupConverter : ExplicitJavaScriptConverter
	{
		// Token: 0x06003879 RID: 14457 RVA: 0x000B9B5C File Offset: 0x000B7D5C
		protected override void PopulateProperties(IDictionary<string, object> state, object obj)
		{
			Popup popup = obj as Popup;
			ExplicitJavaScriptConverter.AddProperty(state, "appendTo", popup.AppendTo, "");
			ExplicitJavaScriptConverter.AddProperty(state, "origin", popup.Origin, "");
			ExplicitJavaScriptConverter.AddProperty(state, "position", popup.Position, "");
		}

		// Token: 0x1700128C RID: 4748
		// (get) Token: 0x0600387A RID: 14458 RVA: 0x000B9BB4 File Offset: 0x000B7DB4
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
