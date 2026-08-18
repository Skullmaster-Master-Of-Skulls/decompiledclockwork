using System;
using System.ComponentModel;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x02000055 RID: 85
	[TypeConverter(typeof(ExpandableObjectConverter))]
	public class ObjectWithState
	{
		// Token: 0x0600028C RID: 652 RVA: 0x00006FFE File Offset: 0x000051FE
		public ObjectWithState(string keyPrefix, StateBag ownerViewState)
		{
			this._stateBag = new StateBagWithPrefix(keyPrefix, ownerViewState);
			this._ownerStateBag = ownerViewState;
		}

		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x0600028D RID: 653 RVA: 0x0000701A File Offset: 0x0000521A
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public StateBagWithPrefix ViewState
		{
			get
			{
				return this._stateBag;
			}
		}

		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x0600028E RID: 654 RVA: 0x00007022 File Offset: 0x00005222
		protected StateBag OwnerViewState
		{
			get
			{
				return this._ownerStateBag;
			}
		}

		// Token: 0x0600028F RID: 655 RVA: 0x0000702C File Offset: 0x0000522C
		protected T GetViewStateValue<T>(string key, T defaultValue)
		{
			object obj = this.ViewState[key];
			if (obj == null)
			{
				return defaultValue;
			}
			return (T)((object)obj);
		}

		// Token: 0x04000054 RID: 84
		private StateBagWithPrefix _stateBag;

		// Token: 0x04000055 RID: 85
		private StateBag _ownerStateBag;
	}
}
