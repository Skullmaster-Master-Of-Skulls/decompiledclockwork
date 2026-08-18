using System;
using System.ComponentModel;
using System.Web.UI;
using Telerik.Web.UI.Editor.DialogControls;
using Telerik.Web.UI.FileExplorer;

namespace Telerik.Web.UI
{
	// Token: 0x02000BAB RID: 2987
	[TypeConverter(typeof(ExpandableObjectConverter))]
	[ParseChildren(true)]
	[PersistChildren(false)]
	public class FileManagerDialogConfiguration : FileExplorerConfiguration
	{
		// Token: 0x170024CD RID: 9421
		// (get) Token: 0x06007090 RID: 28816 RVA: 0x001A43B2 File Offset: 0x001A25B2
		// (set) Token: 0x06007091 RID: 28817 RVA: 0x001A43D3 File Offset: 0x001A25D3
		[DefaultValue(RenderMode.Classic)]
		[NotifyParentProperty(true)]
		public RenderMode RenderMode
		{
			get
			{
				return (RenderMode)(base.ViewState["RenderMode"] ?? RenderMode.Classic);
			}
			set
			{
				base.ViewState["RenderMode"] = value;
			}
		}

		// Token: 0x06007092 RID: 28818 RVA: 0x001A43EB File Offset: 0x001A25EB
		protected virtual FileManagerDialogParameters CreateDialogParameters()
		{
			return new FileManagerDialogParameters();
		}

		// Token: 0x06007093 RID: 28819 RVA: 0x001A43F4 File Offset: 0x001A25F4
		internal FileManagerDialogParameters ToDialogParameters()
		{
			FileManagerDialogParameters fileManagerDialogParameters = this.CreateDialogParameters();
			fileManagerDialogParameters.ViewPaths = base.ViewPaths;
			fileManagerDialogParameters.UploadPaths = base.UploadPaths;
			fileManagerDialogParameters.DeletePaths = base.DeletePaths;
			fileManagerDialogParameters.SearchPatterns = base.SearchPatterns;
			fileManagerDialogParameters.MaxUploadFileSize = base.MaxUploadFileSize;
			fileManagerDialogParameters.EnableAsyncUpload = base.EnableAsyncUpload;
			fileManagerDialogParameters.AllowMultipleSelection = this.AllowMultipleSelection;
			fileManagerDialogParameters.FileBrowserContentProviderTypeName = base.ContentProviderTypeName;
			fileManagerDialogParameters.RenderMode = this.RenderMode;
			return fileManagerDialogParameters;
		}
	}
}
