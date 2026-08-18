using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.Web.UI.Design;

namespace Telerik.Web.UI
{
	// Token: 0x020005C3 RID: 1475
	[Serializable]
	public class MediaPlayerSource : StateManager
	{
		// Token: 0x17001137 RID: 4407
		// (get) Token: 0x060034B9 RID: 13497 RVA: 0x000AE4D0 File Offset: 0x000AC6D0
		// (set) Token: 0x060034BA RID: 13498 RVA: 0x000AE50C File Offset: 0x000AC70C
		[Category("Behavior")]
		[Editor(typeof(UrlEditor), typeof(UITypeEditor))]
		public string Path
		{
			get
			{
				string result = string.Empty;
				if (base.ViewState["Path"] != null)
				{
					result = base.ViewState["Path"].ToString();
				}
				return result;
			}
			set
			{
				base.ViewState["Path"] = value;
			}
		}

		// Token: 0x17001138 RID: 4408
		// (get) Token: 0x060034BB RID: 13499 RVA: 0x000AE520 File Offset: 0x000AC720
		// (set) Token: 0x060034BC RID: 13500 RVA: 0x000AE55C File Offset: 0x000AC75C
		[Category("Behavior")]
		[Editor(typeof(UrlEditor), typeof(UITypeEditor))]
		public string MimeType
		{
			get
			{
				string result = string.Empty;
				if (base.ViewState["MimeType"] != null)
				{
					result = base.ViewState["MimeType"].ToString();
				}
				return result;
			}
			set
			{
				base.ViewState["MimeType"] = value;
			}
		}

		// Token: 0x17001139 RID: 4409
		// (get) Token: 0x060034BD RID: 13501 RVA: 0x000AE570 File Offset: 0x000AC770
		// (set) Token: 0x060034BE RID: 13502 RVA: 0x000AE5A8 File Offset: 0x000AC7A8
		public bool IsHD
		{
			get
			{
				bool result = false;
				if (base.ViewState["IsHD"] != null)
				{
					result = (bool)base.ViewState["IsHD"];
				}
				return result;
			}
			set
			{
				base.ViewState["IsHD"] = value;
			}
		}
	}
}
