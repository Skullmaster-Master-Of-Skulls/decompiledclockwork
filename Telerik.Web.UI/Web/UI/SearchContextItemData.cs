using System;
using System.Runtime.Serialization;

namespace Telerik.Web.UI
{
	// Token: 0x02000879 RID: 2169
	[DataContract]
	public class SearchContextItemData
	{
		// Token: 0x06005056 RID: 20566 RVA: 0x000FB223 File Offset: 0x000F9423
		public SearchContextItemData()
		{
			this.Text = string.Empty;
			this.Key = string.Empty;
			this.ImageUrl = string.Empty;
		}

		// Token: 0x17001A4A RID: 6730
		// (get) Token: 0x06005057 RID: 20567 RVA: 0x000FB24C File Offset: 0x000F944C
		// (set) Token: 0x06005058 RID: 20568 RVA: 0x000FB254 File Offset: 0x000F9454
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

		// Token: 0x17001A4B RID: 6731
		// (get) Token: 0x06005059 RID: 20569 RVA: 0x000FB25D File Offset: 0x000F945D
		// (set) Token: 0x0600505A RID: 20570 RVA: 0x000FB265 File Offset: 0x000F9465
		[DataMember]
		public string Key
		{
			get
			{
				return this._key;
			}
			set
			{
				this._key = value;
			}
		}

		// Token: 0x17001A4C RID: 6732
		// (get) Token: 0x0600505B RID: 20571 RVA: 0x000FB26E File Offset: 0x000F946E
		// (set) Token: 0x0600505C RID: 20572 RVA: 0x000FB276 File Offset: 0x000F9476
		[DataMember]
		public string ImageUrl
		{
			get
			{
				return this._imageUrl;
			}
			set
			{
				this._imageUrl = value;
			}
		}

		// Token: 0x040013E9 RID: 5097
		private string _text;

		// Token: 0x040013EA RID: 5098
		private string _key;

		// Token: 0x040013EB RID: 5099
		private string _imageUrl;
	}
}
