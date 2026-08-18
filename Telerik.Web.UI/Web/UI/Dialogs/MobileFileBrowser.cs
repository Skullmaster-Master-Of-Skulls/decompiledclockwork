using System;
using System.Reflection;
using Telerik.Web.UI.Editor.DialogControls;
using Telerik.Web.UI.Widgets;

namespace Telerik.Web.UI.Dialogs
{
	// Token: 0x0200026F RID: 623
	[ClientScriptResource("Telerik.Web.UI.Editor.DialogControls.MobileFileBrowser", "Telerik.Web.UI.Dialogs.MobileFileBrowser.js")]
	[RequiredScript(typeof(jQueryPlugins))]
	[RequiredScript(typeof(DialogControlInitializer))]
	public abstract class MobileFileBrowser : MobileDialogBase, IClientParameterConsumer
	{
		// Token: 0x170007B8 RID: 1976
		// (get) Token: 0x06001687 RID: 5767 RVA: 0x0004C8B4 File Offset: 0x0004AAB4
		public override string DialogName
		{
			get
			{
				return "MobileFileBrowser";
			}
		}

		// Token: 0x170007B9 RID: 1977
		// (get) Token: 0x06001688 RID: 5768 RVA: 0x0004C8BB File Offset: 0x0004AABB
		protected virtual string[] DefaultSearchPatterns
		{
			get
			{
				return new string[0];
			}
		}

		// Token: 0x170007BA RID: 1978
		// (get) Token: 0x06001689 RID: 5769 RVA: 0x0004C8C3 File Offset: 0x0004AAC3
		// (set) Token: 0x0600168A RID: 5770 RVA: 0x0004C8D5 File Offset: 0x0004AAD5
		public virtual string[] SearchPatterns
		{
			get
			{
				return this.FileBrowser.Configuration.SearchPatterns;
			}
			set
			{
				this.FileBrowser.Configuration.SearchPatterns = value;
			}
		}

		// Token: 0x170007BB RID: 1979
		// (get) Token: 0x0600168B RID: 5771 RVA: 0x0004C8E8 File Offset: 0x0004AAE8
		// (set) Token: 0x0600168C RID: 5772 RVA: 0x0004C8FA File Offset: 0x0004AAFA
		public virtual string[] ViewPaths
		{
			get
			{
				return this.FileBrowser.Configuration.ViewPaths;
			}
			set
			{
				this.FileBrowser.Configuration.ViewPaths = value;
			}
		}

		// Token: 0x170007BC RID: 1980
		// (get) Token: 0x0600168D RID: 5773 RVA: 0x0004C90D File Offset: 0x0004AB0D
		// (set) Token: 0x0600168E RID: 5774 RVA: 0x0004C91F File Offset: 0x0004AB1F
		public virtual string[] UploadPaths
		{
			get
			{
				return this.FileBrowser.Configuration.UploadPaths;
			}
			set
			{
				this.FileBrowser.Configuration.UploadPaths = value;
			}
		}

		// Token: 0x170007BD RID: 1981
		// (get) Token: 0x0600168F RID: 5775 RVA: 0x0004C932 File Offset: 0x0004AB32
		// (set) Token: 0x06001690 RID: 5776 RVA: 0x0004C944 File Offset: 0x0004AB44
		public virtual string[] DeletePaths
		{
			get
			{
				return this.FileBrowser.Configuration.DeletePaths;
			}
			set
			{
				this.FileBrowser.Configuration.DeletePaths = value;
			}
		}

		// Token: 0x170007BE RID: 1982
		// (get) Token: 0x06001691 RID: 5777 RVA: 0x0004C957 File Offset: 0x0004AB57
		// (set) Token: 0x06001692 RID: 5778 RVA: 0x0004C969 File Offset: 0x0004AB69
		public int MaxFileSize
		{
			get
			{
				return this.FileBrowser.Configuration.MaxUploadFileSize;
			}
			set
			{
				this.FileBrowser.Configuration.MaxUploadFileSize = value;
			}
		}

		// Token: 0x170007BF RID: 1983
		// (get) Token: 0x06001693 RID: 5779 RVA: 0x0004C97C File Offset: 0x0004AB7C
		// (set) Token: 0x06001694 RID: 5780 RVA: 0x0004C98E File Offset: 0x0004AB8E
		public string FileBrowserContentProviderTypeName
		{
			get
			{
				return this.FileBrowser.Configuration.ContentProviderTypeName;
			}
			set
			{
				this.FileBrowser.Configuration.ContentProviderTypeName = value;
			}
		}

		// Token: 0x170007C0 RID: 1984
		// (get) Token: 0x06001695 RID: 5781 RVA: 0x0004C9A1 File Offset: 0x0004ABA1
		// (set) Token: 0x06001696 RID: 5782 RVA: 0x0004C9B3 File Offset: 0x0004ABB3
		public bool AllowMultipleSelection
		{
			get
			{
				return this.FileBrowser.Configuration.AllowMultipleSelection;
			}
			set
			{
				this.FileBrowser.Configuration.AllowMultipleSelection = value;
			}
		}

		// Token: 0x170007C1 RID: 1985
		// (get) Token: 0x06001697 RID: 5783 RVA: 0x0004C9C6 File Offset: 0x0004ABC6
		// (set) Token: 0x06001698 RID: 5784 RVA: 0x0004C9D3 File Offset: 0x0004ABD3
		public string PreselectedItemUrl
		{
			get
			{
				return this.FileBrowser.InitialPath;
			}
			set
			{
				this.FileBrowser.InitialPath = FileBrowserContentProvider.RemoveProtocolNameAndServerName(value);
			}
		}

		// Token: 0x170007C2 RID: 1986
		// (get) Token: 0x06001699 RID: 5785 RVA: 0x0004C9E8 File Offset: 0x0004ABE8
		protected RadFileExplorer FileBrowser
		{
			get
			{
				if (this._fileBrowser == null)
				{
					this._fileBrowser = (RadFileExplorer)base.FindControlRecursive("RadFileExplorer1");
					this._fileBrowser.Configuration.SearchPatterns = this.DefaultSearchPatterns;
					this._fileBrowser.Configuration.EnableAsyncUpload = true;
					this._fileBrowser.Language = base.Language;
					if (!string.IsNullOrEmpty(base.LocalizationPath))
					{
						this._fileBrowser.LocalizationPath = base.LocalizationPath;
					}
					this._fileBrowser.Skin = base.RuntimeSkin;
					this._fileBrowser.EnableEmbeddedSkins = this.EnableEmbeddedSkins;
					this._fileBrowser.EnableEmbeddedBaseStylesheet = this.EnableEmbeddedBaseStylesheet;
					this._fileBrowser.ItemCommand += this.fileBrowser_ItemCommand;
				}
				return this._fileBrowser;
			}
		}

		// Token: 0x0600169A RID: 5786 RVA: 0x0004CABC File Offset: 0x0004ACBC
		private void fileBrowser_ItemCommand(object sender, RadFileExplorerEventArgs e)
		{
			if (e.Command.StartsWith("Delete"))
			{
				string path = e.Path;
				if (!(bool)this.InvokeMethod("Delete", path))
				{
					e.Cancel = true;
					return;
				}
			}
			else if (e.Command.StartsWith("Upload"))
			{
				string path2 = e.Path;
				if (!(bool)this.InvokeMethod("Upload", path2))
				{
					e.Cancel = true;
				}
			}
		}

		// Token: 0x170007C3 RID: 1987
		// (get) Token: 0x0600169B RID: 5787 RVA: 0x0004CB34 File Offset: 0x0004AD34
		protected DialogParameters DialogParameters
		{
			get
			{
				if (this._dialogParameters == null)
				{
					this._dialogParameters = DialogHandlerNoSession.GetDialogParameters(this);
				}
				return this._dialogParameters;
			}
		}

		// Token: 0x170007C4 RID: 1988
		// (get) Token: 0x0600169C RID: 5788 RVA: 0x0004CB50 File Offset: 0x0004AD50
		protected virtual FileManagerDialogParameters Parameters
		{
			get
			{
				return FileManagerDialogParameters.Convert(this.DialogParameters);
			}
		}

		// Token: 0x0600169D RID: 5789 RVA: 0x0004CB60 File Offset: 0x0004AD60
		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
			this.ViewPaths = this.Parameters.ViewPaths;
			this.UploadPaths = this.Parameters.UploadPaths;
			this.DeletePaths = this.Parameters.DeletePaths;
			if (this.Parameters.SearchPatterns.Length > 0)
			{
				this.SearchPatterns = this.Parameters.SearchPatterns;
			}
			this.MaxFileSize = this.Parameters.MaxUploadFileSize;
			this.AllowMultipleSelection = this.Parameters.AllowMultipleSelection;
			this.FileBrowserContentProviderTypeName = this.Parameters.FileBrowserContentProviderTypeName;
			string text = this.Page.Request.QueryString["PreselectedItemUrl"];
			if (!string.IsNullOrEmpty(text))
			{
				this.PreselectedItemUrl = text;
			}
		}

		// Token: 0x0600169E RID: 5790 RVA: 0x0004CC28 File Offset: 0x0004AE28
		private object InvokeMethod(string action, string path)
		{
			string text = this.DialogParameters["OnFile" + action + "DeclaringClass"] as string;
			string text2 = this.DialogParameters["OnFile" + action] as string;
			if (string.IsNullOrEmpty(text) || string.IsNullOrEmpty(text2))
			{
				return true;
			}
			Type type = Type.GetType(text);
			if (type == null)
			{
				throw new ArgumentException(string.Format("Invalid File{0} event handler. Cannot find {1} class.", action, text));
			}
			MethodInfo method = type.GetMethod(text2, new Type[]
			{
				typeof(object),
				typeof(string)
			});
			if (method == null)
			{
				throw new ArgumentException(string.Format("Invalid File{0} event handler. Cannot find {1} method.", action, text2));
			}
			return method.Invoke(Activator.CreateInstance(type), new object[]
			{
				this,
				path
			});
		}

		// Token: 0x040005F4 RID: 1524
		private RadFileExplorer _fileBrowser;

		// Token: 0x040005F5 RID: 1525
		private DialogParameters _dialogParameters;
	}
}
