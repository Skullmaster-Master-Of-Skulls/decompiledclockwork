using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Telerik.Web.UI
{
	// Token: 0x0200088C RID: 2188
	[DataContract]
	[Serializable]
	public class SearchBoxItemData
	{
		// Token: 0x060050EE RID: 20718 RVA: 0x000FC1E3 File Offset: 0x000FA3E3
		public SearchBoxItemData()
		{
			this.Text = string.Empty;
			this.Value = string.Empty;
			this._dataItem = new Dictionary<string, object>();
		}

		// Token: 0x17001A86 RID: 6790
		// (get) Token: 0x060050EF RID: 20719 RVA: 0x000FC20C File Offset: 0x000FA40C
		// (set) Token: 0x060050F0 RID: 20720 RVA: 0x000FC214 File Offset: 0x000FA414
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

		// Token: 0x17001A87 RID: 6791
		// (get) Token: 0x060050F1 RID: 20721 RVA: 0x000FC21D File Offset: 0x000FA41D
		// (set) Token: 0x060050F2 RID: 20722 RVA: 0x000FC225 File Offset: 0x000FA425
		[DataMember]
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

		// Token: 0x17001A88 RID: 6792
		// (get) Token: 0x060050F3 RID: 20723 RVA: 0x000FC22E File Offset: 0x000FA42E
		// (set) Token: 0x060050F4 RID: 20724 RVA: 0x000FC236 File Offset: 0x000FA436
		[DataMember]
		public IDictionary<string, object> DataItem
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

		// Token: 0x040013F8 RID: 5112
		private string _text;

		// Token: 0x040013F9 RID: 5113
		private string _value;

		// Token: 0x040013FA RID: 5114
		private IDictionary<string, object> _dataItem;
	}
}
