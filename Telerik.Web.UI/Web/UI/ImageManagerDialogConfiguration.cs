using System;
using System.ComponentModel;
using System.Web.UI;
using Telerik.Web.UI.Editor.DialogControls;

namespace Telerik.Web.UI
{
	// Token: 0x02001086 RID: 4230
	[ParseChildren(true)]
	[PersistChildren(false)]
	public class ImageManagerDialogConfiguration : FileManagerDialogConfiguration
	{
		// Token: 0x17003697 RID: 13975
		// (get) Token: 0x0600AA0F RID: 43535 RVA: 0x0024E0D0 File Offset: 0x0024C2D0
		// (set) Token: 0x0600AA10 RID: 43536 RVA: 0x0024E0F0 File Offset: 0x0024C2F0
		[DefaultValue("_thumb")]
		public string ImageEditorFileSuffix
		{
			get
			{
				return ((string)base.ViewState["ImageEditorFileSuffix"]) ?? "_thumb";
			}
			set
			{
				base.ViewState["ImageEditorFileSuffix"] = value;
			}
		}

		// Token: 0x17003698 RID: 13976
		// (get) Token: 0x0600AA11 RID: 43537 RVA: 0x0024E103 File Offset: 0x0024C303
		// (set) Token: 0x0600AA12 RID: 43538 RVA: 0x0024E123 File Offset: 0x0024C323
		[DefaultValue("")]
		public string ImageEditorHttpHandlerUrl
		{
			get
			{
				return ((string)base.ViewState["ImageEditorHttpHandlerUrl"]) ?? "";
			}
			set
			{
				base.ViewState["ImageEditorHttpHandlerUrl"] = value;
			}
		}

		// Token: 0x0600AA13 RID: 43539 RVA: 0x0024E138 File Offset: 0x0024C338
		protected override FileManagerDialogParameters CreateDialogParameters()
		{
			return new ImageManagerDialogParameters
			{
				ImageEditorFileSuffix = this.ImageEditorFileSuffix,
				ImageEditorHttpHandlerUrl = this.ImageEditorHttpHandlerUrl,
				EnableImageEditor = this.EnableImageEditor,
				EnableThumbnailLinking = this.EnableThumbnailLinking,
				ViewMode = this.ViewMode
			};
		}

		// Token: 0x17003699 RID: 13977
		// (get) Token: 0x0600AA14 RID: 43540 RVA: 0x0024E188 File Offset: 0x0024C388
		// (set) Token: 0x0600AA15 RID: 43541 RVA: 0x0024E1B3 File Offset: 0x0024C3B3
		[DefaultValue(true)]
		public bool EnableImageEditor
		{
			get
			{
				return base.ViewState["EnableImageEditor"] == null || (bool)base.ViewState["EnableImageEditor"];
			}
			set
			{
				base.ViewState["EnableImageEditor"] = value;
			}
		}

		// Token: 0x1700369A RID: 13978
		// (get) Token: 0x0600AA16 RID: 43542 RVA: 0x0024E1CB File Offset: 0x0024C3CB
		// (set) Token: 0x0600AA17 RID: 43543 RVA: 0x0024E1F6 File Offset: 0x0024C3F6
		[DefaultValue(true)]
		public bool EnableThumbnailLinking
		{
			get
			{
				return base.ViewState["EnableThumbnailLinking"] == null || (bool)base.ViewState["EnableThumbnailLinking"];
			}
			set
			{
				base.ViewState["EnableThumbnailLinking"] = value;
			}
		}

		// Token: 0x1700369B RID: 13979
		// (get) Token: 0x0600AA18 RID: 43544 RVA: 0x0024E20E File Offset: 0x0024C40E
		// (set) Token: 0x0600AA19 RID: 43545 RVA: 0x0024E239 File Offset: 0x0024C439
		[DefaultValue(ImageManagerViewMode.Thumbnails)]
		public ImageManagerViewMode ViewMode
		{
			get
			{
				if (base.ViewState["ViewMode"] != null)
				{
					return (ImageManagerViewMode)base.ViewState["ViewMode"];
				}
				return ImageManagerViewMode.Thumbnails;
			}
			set
			{
				base.ViewState["ViewMode"] = value;
			}
		}
	}
}
