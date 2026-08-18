using System;
using System.ComponentModel;
using System.Globalization;
using System.Web.UI.WebControls;
using Telerik.Web.UI.Dialogs;
using Telerik.Web.UI.FileExplorer;

namespace Telerik.Web.UI.Editor.DialogControls
{
	// Token: 0x02001067 RID: 4199
	[ToolboxItem(false)]
	public class ImageManagerDialog : UserControlFileBrowser
	{
		// Token: 0x17003667 RID: 13927
		// (get) Token: 0x0600A96D RID: 43373 RVA: 0x0024CADC File Offset: 0x0024ACDC
		public override string ControlName
		{
			get
			{
				return "Image";
			}
		}

		// Token: 0x17003668 RID: 13928
		// (get) Token: 0x0600A96E RID: 43374 RVA: 0x0024CAE3 File Offset: 0x0024ACE3
		protected override FileManagerDialogParameters Parameters
		{
			get
			{
				return ImageManagerDialogParameters.Convert(base.DialogParameters);
			}
		}

		// Token: 0x0600A96F RID: 43375 RVA: 0x0024CAF0 File Offset: 0x0024ACF0
		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
			ImageManagerDialogParameters imageManagerDialogParameters = (ImageManagerDialogParameters)this.Parameters;
			this.ImageEditorFileSuffix = imageManagerDialogParameters.ImageEditorFileSuffix;
			this.EnableImageEditor = imageManagerDialogParameters.EnableImageEditor;
			this.EnableThumbnailLinking = imageManagerDialogParameters.EnableThumbnailLinking;
			this.ViewMode = imageManagerDialogParameters.ViewMode;
			base.AllowMultipleSelection = imageManagerDialogParameters.AllowMultipleSelection;
			RadToolBar radToolBar = (RadToolBar)base.FindControlRecursive("ImagePreviewToolBar");
			radToolBar.RenderMode = this.Parameters.RenderMode;
			if (imageManagerDialogParameters["IsSkinTouch"] != null && (bool)imageManagerDialogParameters["IsSkinTouch"])
			{
				radToolBar.Height = Unit.Parse("44px", CultureInfo.InvariantCulture);
			}
			if (radToolBar != null && radToolBar.Items.Count >= 4)
			{
				radToolBar.Items[0].ToolTip = this.Localization.GetString(radToolBar.Items[0].Value);
				radToolBar.Items[0].Text = this.Localization.GetString(radToolBar.Items[0].Value);
				radToolBar.Items[1].ToolTip = this.Localization.GetString(radToolBar.Items[1].Value);
				radToolBar.Items[2].ToolTip = this.Localization.GetString(radToolBar.Items[2].Value);
				radToolBar.Items[3].ToolTip = this.Localization.GetString(radToolBar.Items[3].Value);
				radToolBar.Items[4].ToolTip = this.Localization.GetString(radToolBar.Items[4].Value);
			}
			RadTabStrip radTabStrip = (RadTabStrip)base.FindControlRecursive("imageTabStrip");
			radTabStrip.RenderMode = this.Parameters.RenderMode;
			if (radTabStrip != null)
			{
				int num = (radTabStrip.Tabs.Count > 2) ? 2 : radTabStrip.Tabs.Count;
				for (int i = 0; i < num; i++)
				{
					radTabStrip.Tabs[i].Text = this.Localization.GetString(radTabStrip.Tabs[i].Value);
					radTabStrip.Tabs[i].ToolTip = this.Localization.GetString(radTabStrip.Tabs[i].Value);
				}
			}
			SetImagePropertiesDialog setImagePropertiesDialog = (SetImagePropertiesDialog)base.FindControlRecursive("ImageProperties");
			if (setImagePropertiesDialog != null)
			{
				setImagePropertiesDialog.EnableThumbnailLinking = this.EnableThumbnailLinking;
			}
			RadFileExplorer radFileExplorer = (RadFileExplorer)base.FindControlRecursive("RadFileExplorer1");
			if (radFileExplorer != null)
			{
				if (this.ViewMode == ImageManagerViewMode.Grid)
				{
					radFileExplorer.ExplorerMode = FileExplorerMode.Default;
					return;
				}
				radFileExplorer.ExplorerMode = FileExplorerMode.Thumbnails;
			}
		}

		// Token: 0x0600A970 RID: 43376 RVA: 0x0024CDCD File Offset: 0x0024AFCD
		protected override void DescribeComponent(IScriptDescriptor descriptor)
		{
			base.DescribeComponent(descriptor);
			descriptor.AddProperty("imageEditorFileSuffix", this.ImageEditorFileSuffix);
			descriptor.AddProperty("enableImageEditor", this.EnableImageEditor);
		}

		// Token: 0x17003669 RID: 13929
		// (get) Token: 0x0600A971 RID: 43377 RVA: 0x0024CE00 File Offset: 0x0024B000
		protected override string[] DefaultSearchPatterns
		{
			get
			{
				return new string[]
				{
					"*.gif",
					"*.xbm",
					"*.xpm",
					"*.png",
					"*.ief",
					"*.jpg",
					"*.jpe",
					"*.jpeg",
					"*.tiff",
					"*.tif",
					"*.rgb",
					"*.g3f",
					"*.xwd",
					"*.pict",
					"*.ppm",
					"*.pgm",
					"*.pbm",
					"*.pnm",
					"*.bmp",
					"*.ras",
					"*.pcd",
					"*.cgm",
					"*.mil",
					"*.cal",
					"*.fif",
					"*.dsf",
					"*.cmx",
					"*.wi",
					"*.dwg",
					"*.dxf",
					"*.svf"
				};
			}
		}

		// Token: 0x1700366A RID: 13930
		// (get) Token: 0x0600A972 RID: 43378 RVA: 0x0024CF24 File Offset: 0x0024B124
		// (set) Token: 0x0600A973 RID: 43379 RVA: 0x0024CF44 File Offset: 0x0024B144
		public string ImageEditorFileSuffix
		{
			get
			{
				return ((string)this.ViewState["ImageEditorFileSuffix"]) ?? "_thumb";
			}
			set
			{
				this.ViewState["ImageEditorFileSuffix"] = value;
			}
		}

		// Token: 0x1700366B RID: 13931
		// (get) Token: 0x0600A974 RID: 43380 RVA: 0x0024CF57 File Offset: 0x0024B157
		// (set) Token: 0x0600A975 RID: 43381 RVA: 0x0024CF82 File Offset: 0x0024B182
		public bool EnableImageEditor
		{
			get
			{
				return this.ViewState["EnableImageEditor"] == null || (bool)this.ViewState["EnableImageEditor"];
			}
			set
			{
				this.ViewState["EnableImageEditor"] = value;
			}
		}

		// Token: 0x1700366C RID: 13932
		// (get) Token: 0x0600A976 RID: 43382 RVA: 0x0024CF9A File Offset: 0x0024B19A
		// (set) Token: 0x0600A977 RID: 43383 RVA: 0x0024CFC5 File Offset: 0x0024B1C5
		public bool EnableThumbnailLinking
		{
			get
			{
				return this.ViewState["EnableThumbnailLinking"] == null || (bool)this.ViewState["EnableThumbnailLinking"];
			}
			set
			{
				this.ViewState["EnableThumbnailLinking"] = value;
			}
		}

		// Token: 0x1700366D RID: 13933
		// (get) Token: 0x0600A978 RID: 43384 RVA: 0x0024CFDD File Offset: 0x0024B1DD
		// (set) Token: 0x0600A979 RID: 43385 RVA: 0x0024D008 File Offset: 0x0024B208
		public ImageManagerViewMode ViewMode
		{
			get
			{
				if (this.ViewState["ViewMode"] != null)
				{
					return (ImageManagerViewMode)this.ViewState["ViewMode"];
				}
				return ImageManagerViewMode.Thumbnails;
			}
			set
			{
				this.ViewState["ViewMode"] = value;
			}
		}
	}
}
