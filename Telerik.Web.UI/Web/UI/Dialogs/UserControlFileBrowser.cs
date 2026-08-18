using System;
using System.Reflection;
using System.Web.UI.WebControls;
using Telerik.Web.UI.Editor.DialogControls;
using Telerik.Web.UI.Widgets;

namespace Telerik.Web.UI.Dialogs
{
	// Token: 0x02001063 RID: 4195
	[ClientScriptResource("Telerik.Web.UI.Editor.DialogControls.FileBrowser", "Telerik.Web.UI.Dialogs.UserControlFileBrowser.js")]
	[RequiredScript(typeof(jQueryPlugins))]
	[RequiredScript(typeof(DialogControlInitializer))]
	public abstract class UserControlFileBrowser : UserControlBase, IClientParameterConsumer
	{
		// Token: 0x1700364C RID: 13900
		// (get) Token: 0x0600A932 RID: 43314 RVA: 0x0024BF76 File Offset: 0x0024A176
		public override string DialogName
		{
			get
			{
				return "FileBrowser";
			}
		}

		// Token: 0x1700364D RID: 13901
		// (get) Token: 0x0600A933 RID: 43315 RVA: 0x0024BF7D File Offset: 0x0024A17D
		protected virtual string[] DefaultSearchPatterns
		{
			get
			{
				return new string[0];
			}
		}

		// Token: 0x1700364E RID: 13902
		// (get) Token: 0x0600A934 RID: 43316 RVA: 0x0024BF85 File Offset: 0x0024A185
		// (set) Token: 0x0600A935 RID: 43317 RVA: 0x0024BF97 File Offset: 0x0024A197
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

		// Token: 0x1700364F RID: 13903
		// (get) Token: 0x0600A936 RID: 43318 RVA: 0x0024BFAA File Offset: 0x0024A1AA
		// (set) Token: 0x0600A937 RID: 43319 RVA: 0x0024BFBC File Offset: 0x0024A1BC
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

		// Token: 0x17003650 RID: 13904
		// (get) Token: 0x0600A938 RID: 43320 RVA: 0x0024BFCF File Offset: 0x0024A1CF
		// (set) Token: 0x0600A939 RID: 43321 RVA: 0x0024BFE1 File Offset: 0x0024A1E1
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

		// Token: 0x17003651 RID: 13905
		// (get) Token: 0x0600A93A RID: 43322 RVA: 0x0024BFF4 File Offset: 0x0024A1F4
		// (set) Token: 0x0600A93B RID: 43323 RVA: 0x0024C006 File Offset: 0x0024A206
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

		// Token: 0x17003652 RID: 13906
		// (get) Token: 0x0600A93C RID: 43324 RVA: 0x0024C019 File Offset: 0x0024A219
		// (set) Token: 0x0600A93D RID: 43325 RVA: 0x0024C02B File Offset: 0x0024A22B
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

		// Token: 0x17003653 RID: 13907
		// (get) Token: 0x0600A93E RID: 43326 RVA: 0x0024C03E File Offset: 0x0024A23E
		// (set) Token: 0x0600A93F RID: 43327 RVA: 0x0024C050 File Offset: 0x0024A250
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

		// Token: 0x17003654 RID: 13908
		// (get) Token: 0x0600A940 RID: 43328 RVA: 0x0024C063 File Offset: 0x0024A263
		// (set) Token: 0x0600A941 RID: 43329 RVA: 0x0024C075 File Offset: 0x0024A275
		public bool EnableAsyncUpload
		{
			get
			{
				return this.FileBrowser.Configuration.EnableAsyncUpload;
			}
			set
			{
				this.FileBrowser.Configuration.EnableAsyncUpload = value;
			}
		}

		// Token: 0x17003655 RID: 13909
		// (get) Token: 0x0600A942 RID: 43330 RVA: 0x0024C088 File Offset: 0x0024A288
		// (set) Token: 0x0600A943 RID: 43331 RVA: 0x0024C09A File Offset: 0x0024A29A
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

		// Token: 0x17003656 RID: 13910
		// (get) Token: 0x0600A944 RID: 43332 RVA: 0x0024C0AD File Offset: 0x0024A2AD
		// (set) Token: 0x0600A945 RID: 43333 RVA: 0x0024C0BA File Offset: 0x0024A2BA
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

		// Token: 0x17003657 RID: 13911
		// (get) Token: 0x0600A946 RID: 43334 RVA: 0x0024C0D0 File Offset: 0x0024A2D0
		protected RadFileExplorer FileBrowser
		{
			get
			{
				if (this._fileBrowser == null)
				{
					this._fileBrowser = (RadFileExplorer)base.FindControlRecursive("RadFileExplorer1");
					if (this._fileBrowser == null)
					{
						this._fileBrowser = new RadFileExplorer();
						this._fileBrowser.ID = "RadFileBrowser1";
					}
					this._fileBrowser.Configuration.SearchPatterns = this.DefaultSearchPatterns;
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

		// Token: 0x0600A947 RID: 43335 RVA: 0x0024C1B8 File Offset: 0x0024A3B8
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

		// Token: 0x17003658 RID: 13912
		// (get) Token: 0x0600A948 RID: 43336 RVA: 0x0024C230 File Offset: 0x0024A430
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

		// Token: 0x17003659 RID: 13913
		// (get) Token: 0x0600A949 RID: 43337 RVA: 0x0024C24C File Offset: 0x0024A44C
		protected virtual FileManagerDialogParameters Parameters
		{
			get
			{
				return FileManagerDialogParameters.Convert(this.DialogParameters);
			}
		}

		// Token: 0x0600A94A RID: 43338 RVA: 0x0024C259 File Offset: 0x0024A459
		protected override void DescribeComponent(IScriptDescriptor descriptor)
		{
			base.DescribeComponent(descriptor);
			descriptor.AddProperty("previewerType", this.ControlName + "Previewer");
		}

		// Token: 0x0600A94B RID: 43339 RVA: 0x0024C280 File Offset: 0x0024A480
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
			this.EnableAsyncUpload = this.Parameters.EnableAsyncUpload;
			this.AllowMultipleSelection = this.Parameters.AllowMultipleSelection;
			this.FileBrowserContentProviderTypeName = this.Parameters.FileBrowserContentProviderTypeName;
			this.FileBrowser.RenderMode = this.Parameters.RenderMode;
			if (!string.IsNullOrEmpty(this.Page.Request.QueryString["PreselectedItemUrl"]))
			{
				this.PreselectedItemUrl = this.Page.Request.QueryString["PreselectedItemUrl"];
			}
			if (this.FileBrowser.RuntimeSkin == "Silk" || this.FileBrowser.RuntimeSkin == "Glow")
			{
				this.FileBrowser.Height = Unit.Pixel(580);
				return;
			}
			if (this.FileBrowser.RuntimeSkin == "MetroTouch" || this.FileBrowser.RuntimeSkin == "BlackMetroTouch")
			{
				this.FileBrowser.Height = Unit.Pixel(743);
				return;
			}
			if (this.FileBrowser.RuntimeSkin == "Bootstrap")
			{
				this.FileBrowser.Height = Unit.Pixel(630);
				return;
			}
			if (this.FileBrowser.RuntimeSkin == "Material")
			{
				this.FileBrowser.Height = Unit.Pixel(659);
				return;
			}
			if (this.RenderMode == RenderMode.Lightweight)
			{
				this.FileBrowser.Height = Unit.Pixel(530);
				return;
			}
			this.FileBrowser.Height = Unit.Pixel(500);
		}

		// Token: 0x0600A94C RID: 43340 RVA: 0x0024C49C File Offset: 0x0024A69C
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

		// Token: 0x04002DB5 RID: 11701
		private RadFileExplorer _fileBrowser;

		// Token: 0x04002DB6 RID: 11702
		private DialogParameters _dialogParameters;
	}
}
