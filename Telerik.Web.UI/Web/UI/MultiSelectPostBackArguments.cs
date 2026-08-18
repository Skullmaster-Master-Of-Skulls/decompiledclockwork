using System;
using System.Collections.Generic;
using System.ComponentModel;
using Telerik.Web.UI.MultiSelect;

namespace Telerik.Web.UI
{
	// Token: 0x02000600 RID: 1536
	[EditorBrowsable(EditorBrowsableState.Never)]
	public class MultiSelectPostBackArguments
	{
		// Token: 0x17001222 RID: 4642
		// (get) Token: 0x06003768 RID: 14184 RVA: 0x000B7715 File Offset: 0x000B5915
		// (set) Token: 0x06003769 RID: 14185 RVA: 0x000B771D File Offset: 0x000B591D
		public string Command
		{
			get
			{
				return this._command;
			}
			set
			{
				this._command = value;
			}
		}

		// Token: 0x17001223 RID: 4643
		// (get) Token: 0x0600376A RID: 14186 RVA: 0x000B7726 File Offset: 0x000B5926
		// (set) Token: 0x0600376B RID: 14187 RVA: 0x000B772E File Offset: 0x000B592E
		public RadMultiSelectClientState ClientState
		{
			get
			{
				return this._clientState;
			}
			set
			{
				this._clientState = value;
			}
		}

		// Token: 0x17001224 RID: 4644
		// (get) Token: 0x0600376C RID: 14188 RVA: 0x000B7737 File Offset: 0x000B5937
		// (set) Token: 0x0600376D RID: 14189 RVA: 0x000B773F File Offset: 0x000B593F
		public string Text
		{
			get
			{
				return this._text;
			}
			set
			{
				this._text = value;
			}
		}

		// Token: 0x17001225 RID: 4645
		// (get) Token: 0x0600376E RID: 14190 RVA: 0x000B7748 File Offset: 0x000B5948
		// (set) Token: 0x0600376F RID: 14191 RVA: 0x000B7750 File Offset: 0x000B5950
		public string Value
		{
			get
			{
				return this._value;
			}
			set
			{
				this._value = value;
			}
		}

		// Token: 0x17001226 RID: 4646
		// (get) Token: 0x06003770 RID: 14192 RVA: 0x000B7759 File Offset: 0x000B5959
		// (set) Token: 0x06003771 RID: 14193 RVA: 0x000B7761 File Offset: 0x000B5961
		public Dictionary<string, object> DataItem
		{
			get
			{
				return this._dataItem;
			}
			set
			{
				this._dataItem = value;
			}
		}

		// Token: 0x04000ED7 RID: 3799
		private string _command;

		// Token: 0x04000ED8 RID: 3800
		private RadMultiSelectClientState _clientState;

		// Token: 0x04000ED9 RID: 3801
		private string _text;

		// Token: 0x04000EDA RID: 3802
		private string _value;

		// Token: 0x04000EDB RID: 3803
		private Dictionary<string, object> _dataItem;
	}
}
