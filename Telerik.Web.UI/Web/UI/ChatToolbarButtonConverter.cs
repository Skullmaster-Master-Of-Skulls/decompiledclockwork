using System;
using System.Collections.Generic;

namespace Telerik.Web.UI
{
	// Token: 0x02000081 RID: 129
	public class ChatToolbarButtonConverter : ExplicitJavaScriptConverter
	{
		// Token: 0x0600053D RID: 1341 RVA: 0x0000D0CC File Offset: 0x0000B2CC
		protected override void PopulateProperties(IDictionary<string, object> state, object obj)
		{
			ChatToolbarButton chatToolbarButton = obj as ChatToolbarButton;
			ExplicitJavaScriptConverter.AddProperty(state, "name", chatToolbarButton.Name, "");
			ExplicitJavaScriptConverter.AddProperty(state, "text", chatToolbarButton.Text, "");
			ExplicitJavaScriptConverter.AddProperty(state, "iconClass", chatToolbarButton.IconClass, "");
		}

		// Token: 0x170001F4 RID: 500
		// (get) Token: 0x0600053E RID: 1342 RVA: 0x0000D124 File Offset: 0x0000B324
		public override IEnumerable<Type> SupportedTypes
		{
			get
			{
				return new Type[]
				{
					typeof(ChatToolbarButton)
				};
			}
		}
	}
}
