using System;
using System.ComponentModel;
using Telerik.Web.UI.AsyncUpload;
using Telerik.Web.UI.Dialogs;
using Telerik.Web.UI.FileExplorer;

namespace Telerik.Web.UI.Editor.DialogControls
{
	// Token: 0x0200027C RID: 636
	[RequiredScript(typeof(jQuery))]
	[ClientScriptResource("Telerik.Web.UI.Dialogs.MobileImageManager", "Telerik.Web.UI.Common.Core.js")]
	[ToolboxItem(false)]
	[RequiredScript(typeof(DialogControlInitializer))]
	public class MobileImageManagerDialog : MobileFileBrowser
	{
		// Token: 0x170007D5 RID: 2005
		// (get) Token: 0x060016EB RID: 5867 RVA: 0x0004D940 File Offset: 0x0004BB40
		public override string DialogName
		{
			get
			{
				return "MobileImageManager";
			}
		}

		// Token: 0x170007D6 RID: 2006
		// (get) Token: 0x060016EC RID: 5868 RVA: 0x0004D947 File Offset: 0x0004BB47
		public override string ControlName
		{
			get
			{
				return "Image";
			}
		}

		// Token: 0x170007D7 RID: 2007
		// (get) Token: 0x060016ED RID: 5869 RVA: 0x0004D94E File Offset: 0x0004BB4E
		protected override FileManagerDialogParameters Parameters
		{
			get
			{
				return ImageManagerDialogParameters.Convert(base.DialogParameters);
			}
		}

		// Token: 0x060016EE RID: 5870 RVA: 0x0004D95C File Offset: 0x0004BB5C
		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
			ImageManagerDialogParameters imageManagerDialogParameters = (ImageManagerDialogParameters)this.Parameters;
			base.FileBrowser.ExplorerMode = ((imageManagerDialogParameters.ViewMode == ImageManagerViewMode.Grid) ? FileExplorerMode.Default : FileExplorerMode.Thumbnails);
			RadAsyncUpload asyncUpload = base.FileBrowser.AsyncUpload;
			if (asyncUpload != null)
			{
				asyncUpload.MaxFileSize = base.MaxFileSize;
				asyncUpload.MultipleFileSelection = MultipleFileSelection.Automatic;
			}
			base.FileBrowser.DisplayUpFolderItem = true;
			base.FileBrowser.VisibleControls = FileExplorerControls.FileList;
			base.FileBrowser.AllowPaging = false;
			RadButton radButton = (RadButton)this.FindControl("UploadButton");
			if (radButton != null)
			{
				radButton.Text = this.Localization.GetString("Upload");
			}
		}

		// Token: 0x170007D8 RID: 2008
		// (get) Token: 0x060016EF RID: 5871 RVA: 0x0004DA08 File Offset: 0x0004BC08
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
	}
}
