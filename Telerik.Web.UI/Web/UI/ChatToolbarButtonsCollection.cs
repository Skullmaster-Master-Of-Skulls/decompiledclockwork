using System;
using System.Collections;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x02000082 RID: 130
	[ParseChildren(typeof(ChatToolbarButton))]
	public class ChatToolbarButtonsCollection : StronglyTypedStateManagedCollection<ChatToolbarButton>
	{
		// Token: 0x170001F5 RID: 501
		// (get) Token: 0x06000540 RID: 1344 RVA: 0x0000D14E File Offset: 0x0000B34E
		internal IList ItemsList
		{
			get
			{
				return base.List;
			}
		}
	}
}
