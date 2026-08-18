using System;
using System.ComponentModel;
using Telerik.Web.Design;
using Telerik.Web.UI.Widgets;

namespace Telerik.Web.UI.FileExplorer
{
	// Token: 0x02000B59 RID: 2905
	[TypeConverter(typeof(ExpandableObjectConverter))]
	public class FileExplorerConfiguration : StateManager
	{
		// Token: 0x170023E8 RID: 9192
		// (get) Token: 0x06006D8E RID: 28046 RVA: 0x00196C93 File Offset: 0x00194E93
		// (set) Token: 0x06006D8F RID: 28047 RVA: 0x00196CC4 File Offset: 0x00194EC4
		[TypeConverter(typeof(ListConverter))]
		[DefaultValue("")]
		public string[] ViewPaths
		{
			get
			{
				if (base.ViewState["ViewPaths"] == null)
				{
					return new string[0];
				}
				return (string[])base.ViewState["ViewPaths"];
			}
			set
			{
				string[] array = FileExplorerConfiguration.TrimPaths(value);
				base.ViewState["ViewPaths"] = array;
				this.OnPathChange("ViewPaths", array);
			}
		}

		// Token: 0x170023E9 RID: 9193
		// (get) Token: 0x06006D90 RID: 28048 RVA: 0x00196CF5 File Offset: 0x00194EF5
		// (set) Token: 0x06006D91 RID: 28049 RVA: 0x00196D28 File Offset: 0x00194F28
		[DefaultValue("")]
		[TypeConverter(typeof(ListConverter))]
		public string[] UploadPaths
		{
			get
			{
				if (base.ViewState["UploadPaths"] == null)
				{
					return new string[0];
				}
				return (string[])base.ViewState["UploadPaths"];
			}
			set
			{
				string[] array = FileExplorerConfiguration.TrimPaths(value);
				base.ViewState["UploadPaths"] = array;
				this.OnPathChange("UploadPaths", array);
			}
		}

		// Token: 0x170023EA RID: 9194
		// (get) Token: 0x06006D92 RID: 28050 RVA: 0x00196D59 File Offset: 0x00194F59
		// (set) Token: 0x06006D93 RID: 28051 RVA: 0x00196D8C File Offset: 0x00194F8C
		[TypeConverter(typeof(ListConverter))]
		[DefaultValue("")]
		public string[] DeletePaths
		{
			get
			{
				if (base.ViewState["DeletePaths"] == null)
				{
					return new string[0];
				}
				return (string[])base.ViewState["DeletePaths"];
			}
			set
			{
				string[] array = FileExplorerConfiguration.TrimPaths(value);
				base.ViewState["DeletePaths"] = array;
				this.OnPathChange("DeletePaths", array);
			}
		}

		// Token: 0x170023EB RID: 9195
		// (get) Token: 0x06006D94 RID: 28052 RVA: 0x00196DBD File Offset: 0x00194FBD
		// (set) Token: 0x06006D95 RID: 28053 RVA: 0x00196DED File Offset: 0x00194FED
		[DefaultValue("")]
		[TypeConverter(typeof(ListConverter))]
		[Description("Gets or sets the file extension search patterns that control which files are shown in the Document/Flash/Image/Media/Template Manager dialog.")]
		public string[] SearchPatterns
		{
			get
			{
				if (base.ViewState["SearchPatterns"] == null)
				{
					return new string[0];
				}
				return (string[])base.ViewState["SearchPatterns"];
			}
			set
			{
				base.ViewState["SearchPatterns"] = FileExplorerConfiguration.TrimPaths(value);
			}
		}

		// Token: 0x170023EC RID: 9196
		// (get) Token: 0x06006D96 RID: 28054 RVA: 0x00196E05 File Offset: 0x00195005
		// (set) Token: 0x06006D97 RID: 28055 RVA: 0x00196E34 File Offset: 0x00195034
		[NotifyParentProperty(true)]
		[DefaultValue(204800)]
		public int MaxUploadFileSize
		{
			get
			{
				if (base.ViewState["MaxUploadFileSize"] == null)
				{
					return 204800;
				}
				return (int)base.ViewState["MaxUploadFileSize"];
			}
			set
			{
				base.ViewState["MaxUploadFileSize"] = value;
			}
		}

		// Token: 0x170023ED RID: 9197
		// (get) Token: 0x06006D98 RID: 28056 RVA: 0x00196E4C File Offset: 0x0019504C
		// (set) Token: 0x06006D99 RID: 28057 RVA: 0x00196E7B File Offset: 0x0019507B
		[NotifyParentProperty(true)]
		[DefaultValue("")]
		public string ContentProviderTypeName
		{
			get
			{
				if (base.ViewState["ContentProviderTypeName"] == null)
				{
					return string.Empty;
				}
				return (string)base.ViewState["ContentProviderTypeName"];
			}
			set
			{
				base.ViewState["ContentProviderTypeName"] = value;
				this._fileBrowserContentProviderType = null;
			}
		}

		// Token: 0x170023EE RID: 9198
		// (get) Token: 0x06006D9A RID: 28058 RVA: 0x00196E98 File Offset: 0x00195098
		[Description("This property gets the current content provider type. To set the content provider type use ContentProviderTypeName")]
		[Bindable(false)]
		[Browsable(false)]
		public Type FileBrowserContentProviderType
		{
			get
			{
				if (this._fileBrowserContentProviderType == null)
				{
					string typeName = string.IsNullOrEmpty(this.ContentProviderTypeName) ? typeof(FileSystemContentProvider).FullName : this.ContentProviderTypeName;
					this._fileBrowserContentProviderType = Type.GetType(typeName);
				}
				return this._fileBrowserContentProviderType;
			}
		}

		// Token: 0x170023EF RID: 9199
		// (get) Token: 0x06006D9B RID: 28059 RVA: 0x00196EEA File Offset: 0x001950EA
		// (set) Token: 0x06006D9C RID: 28060 RVA: 0x00196F0B File Offset: 0x0019510B
		[NotifyParentProperty(true)]
		[DefaultValue(true)]
		public bool EnableAsyncUpload
		{
			get
			{
				return (bool)(base.ViewState["EnableAsyncUpload"] ?? true);
			}
			set
			{
				base.ViewState["EnableAsyncUpload"] = value;
			}
		}

		// Token: 0x170023F0 RID: 9200
		// (get) Token: 0x06006D9D RID: 28061 RVA: 0x00196F23 File Offset: 0x00195123
		// (set) Token: 0x06006D9E RID: 28062 RVA: 0x00196F44 File Offset: 0x00195144
		[NotifyParentProperty(true)]
		[DefaultValue(true)]
		public virtual bool AllowMultipleSelection
		{
			get
			{
				return (bool)(base.ViewState["AllowMultipleSelection"] ?? true);
			}
			set
			{
				base.ViewState["AllowMultipleSelection"] = value;
			}
		}

		// Token: 0x170023F1 RID: 9201
		// (get) Token: 0x06006D9F RID: 28063 RVA: 0x00196F5C File Offset: 0x0019515C
		// (set) Token: 0x06006DA0 RID: 28064 RVA: 0x00196F7D File Offset: 0x0019517D
		[NotifyParentProperty(true)]
		[DefaultValue(true)]
		public virtual bool AllowFileExtensionRename
		{
			get
			{
				return (bool)(base.ViewState["AllowFileExtensionRename"] ?? true);
			}
			set
			{
				base.ViewState["AllowFileExtensionRename"] = value;
			}
		}

		// Token: 0x140000FD RID: 253
		// (add) Token: 0x06006DA1 RID: 28065 RVA: 0x00196F98 File Offset: 0x00195198
		// (remove) Token: 0x06006DA2 RID: 28066 RVA: 0x00196FD0 File Offset: 0x001951D0
		internal event FileExplorerPathsEventHandler PathChange;

		// Token: 0x06006DA3 RID: 28067 RVA: 0x00197008 File Offset: 0x00195208
		private void OnPathChange(string pathType, string[] paths)
		{
			if (this.PathChange != null)
			{
				FileExplorerPathsEventArgs e = new FileExplorerPathsEventArgs(pathType, paths);
				this.PathChange(this, e);
			}
		}

		// Token: 0x06006DA4 RID: 28068 RVA: 0x00197034 File Offset: 0x00195234
		private static string[] TrimPaths(string[] paths)
		{
			if (paths != null)
			{
				for (int i = 0; i < paths.Length; i++)
				{
					paths[i] = paths[i].Trim();
				}
			}
			return paths;
		}

		// Token: 0x04001DA2 RID: 7586
		private Type _fileBrowserContentProviderType;
	}
}
