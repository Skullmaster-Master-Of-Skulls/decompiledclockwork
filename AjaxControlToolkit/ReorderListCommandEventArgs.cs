using System;
using System.Web.UI.WebControls;

namespace AjaxControlToolkit
{
	// Token: 0x02000178 RID: 376
	public class ReorderListCommandEventArgs : CommandEventArgs
	{
		// Token: 0x170003ED RID: 1005
		// (get) Token: 0x06000A72 RID: 2674 RVA: 0x0001B685 File Offset: 0x00019885
		// (set) Token: 0x06000A73 RID: 2675 RVA: 0x0001B68D File Offset: 0x0001988D
		public ReorderListItem Item
		{
			get
			{
				return this._item;
			}
			set
			{
				this._item = value;
			}
		}

		// Token: 0x170003EE RID: 1006
		// (get) Token: 0x06000A74 RID: 2676 RVA: 0x0001B696 File Offset: 0x00019896
		// (set) Token: 0x06000A75 RID: 2677 RVA: 0x0001B69E File Offset: 0x0001989E
		public object Source
		{
			get
			{
				return this._source;
			}
			set
			{
				this._source = value;
			}
		}

		// Token: 0x06000A76 RID: 2678 RVA: 0x0001B6A7 File Offset: 0x000198A7
		internal ReorderListCommandEventArgs(CommandEventArgs ce, object source, ReorderListItem item) : base(ce)
		{
			this._item = item;
			this._source = source;
		}

		// Token: 0x040003F9 RID: 1017
		private ReorderListItem _item;

		// Token: 0x040003FA RID: 1018
		private object _source;
	}
}
