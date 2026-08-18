using System;

namespace Telerik.Web.UI
{
	// Token: 0x020019DE RID: 6622
	public class UpdateClientOperation<T> : ClientOperation<T> where T : ControlItem
	{
		// Token: 0x17004D59 RID: 19801
		// (get) Token: 0x06010051 RID: 65617 RVA: 0x00397A8E File Offset: 0x00395C8E
		// (set) Token: 0x06010052 RID: 65618 RVA: 0x00397A96 File Offset: 0x00395C96
		public string PropertyName { get; internal set; }
	}
}
