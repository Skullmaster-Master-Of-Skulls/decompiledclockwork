using System;
using System.Collections.Generic;

namespace Telerik.Web.UI.Chat
{
	// Token: 0x02000089 RID: 137
	public class MessagesConverter : ExplicitJavaScriptConverter
	{
		// Token: 0x0600055B RID: 1371 RVA: 0x0000D4B8 File Offset: 0x0000B6B8
		protected override void PopulateProperties(IDictionary<string, object> state, object obj)
		{
			Messages messages = obj as Messages;
			ExplicitJavaScriptConverter.AddProperty(state, "placeholder", messages.Placeholder, "Type a message...");
		}

		// Token: 0x17000201 RID: 513
		// (get) Token: 0x0600055C RID: 1372 RVA: 0x0000D4E4 File Offset: 0x0000B6E4
		public override IEnumerable<Type> SupportedTypes
		{
			get
			{
				return new Type[]
				{
					typeof(Messages)
				};
			}
		}
	}
}
