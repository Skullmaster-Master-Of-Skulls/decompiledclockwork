using System;

namespace Telerik.Web.UI
{
	// Token: 0x02001846 RID: 6214
	public class ClientOperation<T> where T : ControlItem
	{
		// Token: 0x170048E5 RID: 18661
		// (get) Token: 0x0600F150 RID: 61776 RVA: 0x0036DB74 File Offset: 0x0036BD74
		// (set) Token: 0x0600F151 RID: 61777 RVA: 0x0036DB7C File Offset: 0x0036BD7C
		public T Item
		{
			get
			{
				return this._item;
			}
			internal set
			{
				this._item = value;
			}
		}

		// Token: 0x170048E6 RID: 18662
		// (get) Token: 0x0600F152 RID: 61778 RVA: 0x0036DB85 File Offset: 0x0036BD85
		// (set) Token: 0x0600F153 RID: 61779 RVA: 0x0036DB8D File Offset: 0x0036BD8D
		public ClientOperationType Type
		{
			get
			{
				return this._type;
			}
			internal set
			{
				this._type = value;
			}
		}

		// Token: 0x0400456B RID: 17771
		private T _item;

		// Token: 0x0400456C RID: 17772
		private ClientOperationType _type;
	}
}
