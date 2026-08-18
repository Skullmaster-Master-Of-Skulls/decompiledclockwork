using System;
using System.Collections;
using System.Runtime.Serialization;

namespace Telerik.Web.UI
{
	// Token: 0x0200088B RID: 2187
	[DataContract]
	public class SearchBoxContext
	{
		// Token: 0x060050E5 RID: 20709 RVA: 0x000FC185 File Offset: 0x000FA385
		public SearchBoxContext()
		{
			this.Text = string.Empty;
			this.ShowAllResults = false;
		}

		// Token: 0x17001A82 RID: 6786
		// (get) Token: 0x060050E6 RID: 20710 RVA: 0x000FC19F File Offset: 0x000FA39F
		// (set) Token: 0x060050E7 RID: 20711 RVA: 0x000FC1A7 File Offset: 0x000FA3A7
		[DataMember]
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

		// Token: 0x17001A83 RID: 6787
		// (get) Token: 0x060050E8 RID: 20712 RVA: 0x000FC1B0 File Offset: 0x000FA3B0
		// (set) Token: 0x060050E9 RID: 20713 RVA: 0x000FC1B8 File Offset: 0x000FA3B8
		[DataMember]
		public bool ShowAllResults
		{
			get
			{
				return this._showAllResults;
			}
			set
			{
				this._showAllResults = value;
			}
		}

		// Token: 0x17001A84 RID: 6788
		// (get) Token: 0x060050EA RID: 20714 RVA: 0x000FC1C1 File Offset: 0x000FA3C1
		// (set) Token: 0x060050EB RID: 20715 RVA: 0x000FC1C9 File Offset: 0x000FA3C9
		[DataMember]
		public SearchContextItemData SelectedContextItem
		{
			get
			{
				return this._selectedContextItem;
			}
			set
			{
				this._selectedContextItem = value;
			}
		}

		// Token: 0x17001A85 RID: 6789
		// (get) Token: 0x060050EC RID: 20716 RVA: 0x000FC1D2 File Offset: 0x000FA3D2
		// (set) Token: 0x060050ED RID: 20717 RVA: 0x000FC1DA File Offset: 0x000FA3DA
		[DataMember]
		public IDictionary UserContext
		{
			get
			{
				return this._userContext;
			}
			set
			{
				this._userContext = value;
			}
		}

		// Token: 0x040013F4 RID: 5108
		private string _text;

		// Token: 0x040013F5 RID: 5109
		private bool _showAllResults;

		// Token: 0x040013F6 RID: 5110
		private SearchContextItemData _selectedContextItem;

		// Token: 0x040013F7 RID: 5111
		private IDictionary _userContext;
	}
}
