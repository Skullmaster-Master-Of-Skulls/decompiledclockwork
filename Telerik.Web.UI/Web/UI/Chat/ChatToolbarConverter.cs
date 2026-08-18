using System;
using System.Collections.Generic;

namespace Telerik.Web.UI.Chat
{
	// Token: 0x02000083 RID: 131
	public class ChatToolbarConverter : ExplicitJavaScriptConverter
	{
		// Token: 0x06000542 RID: 1346 RVA: 0x0000D160 File Offset: 0x0000B360
		protected override void PopulateProperties(IDictionary<string, object> state, object obj)
		{
			ChatToolbar chatToolbar = obj as ChatToolbar;
			ExplicitJavaScriptConverter.AddProperty(state, "animation", chatToolbar.Animation, false);
			ExplicitJavaScriptConverter.AddProperty(state, "animation", chatToolbar.AnimationSettings, null);
			if (chatToolbar.ButtonsCollection.Count != 0)
			{
				ExplicitJavaScriptConverter.AddProperty(state, "buttons", chatToolbar.ButtonsCollection.ItemsList, null);
			}
			ExplicitJavaScriptConverter.AddProperty(state, "scrollable", chatToolbar.Scrollable, false);
			ExplicitJavaScriptConverter.AddProperty(state, "toggleable", chatToolbar.Toggleable, false);
		}

		// Token: 0x170001F6 RID: 502
		// (get) Token: 0x06000543 RID: 1347 RVA: 0x0000D200 File Offset: 0x0000B400
		public override IEnumerable<Type> SupportedTypes
		{
			get
			{
				return new Type[]
				{
					typeof(ChatToolbar)
				};
			}
		}
	}
}
