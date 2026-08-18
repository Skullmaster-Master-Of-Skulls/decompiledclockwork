using System;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x02001028 RID: 4136
	public sealed class StateBagWithPrefix
	{
		// Token: 0x0600A324 RID: 41764 RVA: 0x00244FAF File Offset: 0x002431AF
		internal StateBagWithPrefix(string keyPrefix, StateBag ownerStateBag)
		{
			this._keyPrefix = keyPrefix;
			this._ownerStateBag = ownerStateBag;
		}

		// Token: 0x1700337F RID: 13183
		public object this[string key]
		{
			get
			{
				return this._ownerStateBag[this._keyPrefix + key];
			}
			set
			{
				this._ownerStateBag[this._keyPrefix + key] = value;
			}
		}

		// Token: 0x04002D5B RID: 11611
		private StateBag _ownerStateBag;

		// Token: 0x04002D5C RID: 11612
		private string _keyPrefix;
	}
}
