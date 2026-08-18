using System;

namespace System.Web.UI.WebControls.WebParts
{
	// Token: 0x02000591 RID: 1425
	public class WebPartDescription
	{
		// Token: 0x060047F4 RID: 18420 RVA: 0x000030B5 File Offset: 0x000012B5
		private WebPartDescription()
		{
		}

		// Token: 0x060047F5 RID: 18421 RVA: 0x000ECA24 File Offset: 0x000EAC24
		public WebPartDescription(string id, string title, string description, string imageUrl)
		{
			if (string.IsNullOrEmpty(id))
			{
				throw new ArgumentNullException("id");
			}
			if (string.IsNullOrEmpty(title))
			{
				throw new ArgumentNullException("title");
			}
			this._id = id;
			this._title = title;
			this._description = ((description != null) ? description : string.Empty);
			this._imageUrl = ((imageUrl != null) ? imageUrl : string.Empty);
		}

		// Token: 0x060047F6 RID: 18422 RVA: 0x000ECA90 File Offset: 0x000EAC90
		public WebPartDescription(WebPart part)
		{
			string id = part.ID;
			if (string.IsNullOrEmpty(id))
			{
				throw new ArgumentException(SR.GetString("WebPartManager_NoWebPartID"), "part");
			}
			this._id = id;
			string displayTitle = part.DisplayTitle;
			this._title = ((displayTitle != null) ? displayTitle : string.Empty);
			string description = part.Description;
			this._description = ((description != null) ? description : string.Empty);
			string catalogIconImageUrl = part.CatalogIconImageUrl;
			this._imageUrl = ((catalogIconImageUrl != null) ? catalogIconImageUrl : string.Empty);
			this._part = part;
		}

		// Token: 0x17001544 RID: 5444
		// (get) Token: 0x060047F7 RID: 18423 RVA: 0x000ECB1D File Offset: 0x000EAD1D
		public string CatalogIconImageUrl
		{
			get
			{
				return this._imageUrl;
			}
		}

		// Token: 0x17001545 RID: 5445
		// (get) Token: 0x060047F8 RID: 18424 RVA: 0x000ECB25 File Offset: 0x000EAD25
		public string Description
		{
			get
			{
				return this._description;
			}
		}

		// Token: 0x17001546 RID: 5446
		// (get) Token: 0x060047F9 RID: 18425 RVA: 0x000ECB2D File Offset: 0x000EAD2D
		public string ID
		{
			get
			{
				return this._id;
			}
		}

		// Token: 0x17001547 RID: 5447
		// (get) Token: 0x060047FA RID: 18426 RVA: 0x000ECB35 File Offset: 0x000EAD35
		public string Title
		{
			get
			{
				return this._title;
			}
		}

		// Token: 0x17001548 RID: 5448
		// (get) Token: 0x060047FB RID: 18427 RVA: 0x000ECB3D File Offset: 0x000EAD3D
		internal WebPart WebPart
		{
			get
			{
				return this._part;
			}
		}

		// Token: 0x04002718 RID: 10008
		private string _id;

		// Token: 0x04002719 RID: 10009
		private string _title;

		// Token: 0x0400271A RID: 10010
		private string _description;

		// Token: 0x0400271B RID: 10011
		private string _imageUrl;

		// Token: 0x0400271C RID: 10012
		private WebPart _part;
	}
}
