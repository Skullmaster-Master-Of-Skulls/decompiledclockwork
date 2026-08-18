using System;
using System.Collections.Generic;

namespace Telerik.Web.UI
{
	// Token: 0x0200008B RID: 139
	public class RadChatConverter : ExplicitJavaScriptConverter
	{
		// Token: 0x0600056D RID: 1389 RVA: 0x0000D820 File Offset: 0x0000BA20
		protected override void PopulateProperties(IDictionary<string, object> state, object obj)
		{
			RadChat radChat = obj as RadChat;
			ExplicitJavaScriptConverter.AddProperty(state, "theme", radChat.RuntimeSkin, "Default");
			ExplicitJavaScriptConverter.AddProperty(state, "messages", radChat.MessagesSettings, null);
			ExplicitJavaScriptConverter.AddProperty(state, "user", radChat.UserSettings, null);
			ExplicitJavaScriptConverter.AddProperty(state, "toolbar", radChat.ToolbarSettings, null);
			base.AddScript(state, "actionClick", radChat.ClientEvents.OnActionClick);
			base.AddScript(state, "post", radChat.ClientEvents.OnPost);
			base.AddScript(state, "sendMessage", radChat.ClientEvents.OnSendMessage);
			base.AddScript(state, "typingEnd", radChat.ClientEvents.OnTypingEnd);
			base.AddScript(state, "typingStart", radChat.ClientEvents.OnTypingStart);
			base.AddScript(state, "toolClick", radChat.ClientEvents.OnToolClick);
		}

		// Token: 0x17000208 RID: 520
		// (get) Token: 0x0600056E RID: 1390 RVA: 0x0000D90C File Offset: 0x0000BB0C
		public override IEnumerable<Type> SupportedTypes
		{
			get
			{
				return new Type[]
				{
					typeof(RadChat)
				};
			}
		}
	}
}
