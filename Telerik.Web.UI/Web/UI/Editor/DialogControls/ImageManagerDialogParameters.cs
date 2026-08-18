using System;
using System.Runtime.Serialization;

namespace Telerik.Web.UI.Editor.DialogControls
{
	// Token: 0x02001068 RID: 4200
	[Serializable]
	public class ImageManagerDialogParameters : FileManagerDialogParameters
	{
		// Token: 0x0600A97B RID: 43387 RVA: 0x0024D028 File Offset: 0x0024B228
		public ImageManagerDialogParameters()
		{
		}

		// Token: 0x0600A97C RID: 43388 RVA: 0x0024D030 File Offset: 0x0024B230
		protected ImageManagerDialogParameters(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600A97D RID: 43389 RVA: 0x0024D03C File Offset: 0x0024B23C
		public new static ImageManagerDialogParameters Convert(DialogParameters dialogParameters)
		{
			ImageManagerDialogParameters imageManagerDialogParameters = new ImageManagerDialogParameters();
			foreach (object key in dialogParameters.Keys)
			{
				imageManagerDialogParameters[key] = dialogParameters[key];
			}
			return imageManagerDialogParameters;
		}

		// Token: 0x1700366E RID: 13934
		// (get) Token: 0x0600A97E RID: 43390 RVA: 0x0024D0A0 File Offset: 0x0024B2A0
		// (set) Token: 0x0600A97F RID: 43391 RVA: 0x0024D0BB File Offset: 0x0024B2BB
		public string ImageEditorFileSuffix
		{
			get
			{
				return ((string)this["ImageEditorFileSuffix"]) ?? "_thumb";
			}
			set
			{
				this["ImageEditorFileSuffix"] = value;
			}
		}

		// Token: 0x1700366F RID: 13935
		// (get) Token: 0x0600A980 RID: 43392 RVA: 0x0024D0C9 File Offset: 0x0024B2C9
		// (set) Token: 0x0600A981 RID: 43393 RVA: 0x0024D0EA File Offset: 0x0024B2EA
		public bool EnableImageEditor
		{
			get
			{
				return this["EnableImageEditor"] == null || (bool)this["EnableImageEditor"];
			}
			set
			{
				this["EnableImageEditor"] = value;
			}
		}

		// Token: 0x17003670 RID: 13936
		// (get) Token: 0x0600A982 RID: 43394 RVA: 0x0024D0FD File Offset: 0x0024B2FD
		// (set) Token: 0x0600A983 RID: 43395 RVA: 0x0024D11E File Offset: 0x0024B31E
		public bool EnableThumbnailLinking
		{
			get
			{
				return this["EnableThumbnailLinking"] == null || (bool)this["EnableThumbnailLinking"];
			}
			set
			{
				this["EnableThumbnailLinking"] = value;
			}
		}

		// Token: 0x17003671 RID: 13937
		// (get) Token: 0x0600A984 RID: 43396 RVA: 0x0024D131 File Offset: 0x0024B331
		// (set) Token: 0x0600A985 RID: 43397 RVA: 0x0024D14C File Offset: 0x0024B34C
		public string ImageEditorHttpHandlerUrl
		{
			get
			{
				return ((string)this["ImageEditorHttpHandlerUrl"]) ?? "";
			}
			set
			{
				this["ImageEditorHttpHandlerUrl"] = value;
			}
		}

		// Token: 0x17003672 RID: 13938
		// (get) Token: 0x0600A986 RID: 43398 RVA: 0x0024D15A File Offset: 0x0024B35A
		// (set) Token: 0x0600A987 RID: 43399 RVA: 0x0024D17B File Offset: 0x0024B37B
		public ImageManagerViewMode ViewMode
		{
			get
			{
				if (this["ViewMode"] != null)
				{
					return (ImageManagerViewMode)this["ViewMode"];
				}
				return ImageManagerViewMode.Thumbnails;
			}
			set
			{
				this["ViewMode"] = value;
			}
		}
	}
}
