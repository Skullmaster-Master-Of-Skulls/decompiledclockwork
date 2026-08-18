using System;

namespace AjaxControlToolkit
{
	// Token: 0x020001A1 RID: 417
	public class Slide
	{
		// Token: 0x06000C11 RID: 3089 RVA: 0x0001FD05 File Offset: 0x0001DF05
		public Slide() : this(null, null, null)
		{
		}

		// Token: 0x06000C12 RID: 3090 RVA: 0x0001FD10 File Offset: 0x0001DF10
		public Slide(string imagePath, string name, string description)
		{
			this._imagePath = imagePath;
			this._name = name;
			this._description = description;
		}

		// Token: 0x06000C13 RID: 3091 RVA: 0x0001FD2D File Offset: 0x0001DF2D
		public Slide(string imagePath, string name, string description, string url)
		{
			this._imagePath = imagePath;
			this._name = name;
			this._description = description;
			this._url = url;
		}

		// Token: 0x1700047C RID: 1148
		// (get) Token: 0x06000C14 RID: 3092 RVA: 0x0001FD52 File Offset: 0x0001DF52
		// (set) Token: 0x06000C15 RID: 3093 RVA: 0x0001FD5A File Offset: 0x0001DF5A
		public string ImagePath
		{
			get
			{
				return this._imagePath;
			}
			set
			{
				this._imagePath = value;
			}
		}

		// Token: 0x1700047D RID: 1149
		// (get) Token: 0x06000C16 RID: 3094 RVA: 0x0001FD63 File Offset: 0x0001DF63
		// (set) Token: 0x06000C17 RID: 3095 RVA: 0x0001FD6B File Offset: 0x0001DF6B
		public string Name
		{
			get
			{
				return this._name;
			}
			set
			{
				this._name = value;
			}
		}

		// Token: 0x1700047E RID: 1150
		// (get) Token: 0x06000C18 RID: 3096 RVA: 0x0001FD74 File Offset: 0x0001DF74
		// (set) Token: 0x06000C19 RID: 3097 RVA: 0x0001FD7C File Offset: 0x0001DF7C
		public string Description
		{
			get
			{
				return this._description;
			}
			set
			{
				this._description = value;
			}
		}

		// Token: 0x1700047F RID: 1151
		// (get) Token: 0x06000C1A RID: 3098 RVA: 0x0001FD85 File Offset: 0x0001DF85
		// (set) Token: 0x06000C1B RID: 3099 RVA: 0x0001FD8D File Offset: 0x0001DF8D
		public string Url
		{
			get
			{
				return this._url;
			}
			set
			{
				this._url = value;
			}
		}

		// Token: 0x04000473 RID: 1139
		private string _imagePath;

		// Token: 0x04000474 RID: 1140
		private string _name;

		// Token: 0x04000475 RID: 1141
		private string _description;

		// Token: 0x04000476 RID: 1142
		private string _url;
	}
}
