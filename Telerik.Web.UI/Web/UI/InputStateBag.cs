using System;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x020012B0 RID: 4784
	public sealed class InputStateBag
	{
		// Token: 0x0600C836 RID: 51254 RVA: 0x002C9B34 File Offset: 0x002C7D34
		internal InputStateBag(string KeyPrefix, StateBag OwnerStateBag)
		{
			this._ownerStateBag = OwnerStateBag;
			this.keyPrefix = KeyPrefix;
		}

		// Token: 0x170040B2 RID: 16562
		public object this[string Key]
		{
			get
			{
				return this._ownerStateBag[this.keyPrefix + Key];
			}
			set
			{
				this._ownerStateBag[this.keyPrefix + Key] = value;
			}
		}

		// Token: 0x040034BD RID: 13501
		private StateBag _ownerStateBag;

		// Token: 0x040034BE RID: 13502
		private string keyPrefix;
	}
}
