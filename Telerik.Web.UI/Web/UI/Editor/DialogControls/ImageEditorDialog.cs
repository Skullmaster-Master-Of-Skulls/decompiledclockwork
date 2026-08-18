using System;
using System.ComponentModel;
using System.Text;
using Telerik.Web.UI.Dialogs;
using Telerik.Web.UI.Widgets;

namespace Telerik.Web.UI.Editor.DialogControls
{
	// Token: 0x020019E3 RID: 6627
	[RequiredScript(typeof(ResizeExtender))]
	[ClientScriptResource("Telerik.Web.UI.Widgets.ImageEditor", "Telerik.Web.UI.Common.Core.js")]
	[ToolboxItem(false)]
	public class ImageEditorDialog : UserControlBase, IClientParameterConsumer
	{
		// Token: 0x17004D6A RID: 19818
		// (get) Token: 0x06010085 RID: 65669 RVA: 0x00398DFF File Offset: 0x00396FFF
		public override string DialogName
		{
			get
			{
				return "ImageEditor";
			}
		}

		// Token: 0x17004D6B RID: 19819
		// (get) Token: 0x06010086 RID: 65670 RVA: 0x00398E06 File Offset: 0x00397006
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

		// Token: 0x17004D6C RID: 19820
		// (get) Token: 0x06010087 RID: 65671 RVA: 0x00398E22 File Offset: 0x00397022
		protected virtual FileManagerDialogParameters Parameters
		{
			get
			{
				return FileManagerDialogParameters.Convert(this.DialogParameters);
			}
		}

		// Token: 0x17004D6D RID: 19821
		// (get) Token: 0x06010088 RID: 65672 RVA: 0x00398E2F File Offset: 0x0039702F
		protected char PathSeparator
		{
			get
			{
				return this.ContentProvider.PathSeparator;
			}
		}

		// Token: 0x17004D6E RID: 19822
		// (get) Token: 0x06010089 RID: 65673 RVA: 0x00398E3C File Offset: 0x0039703C
		private Type FileBrowserContentProviderType
		{
			get
			{
				if (this._fileBrowserContentProviderType == null)
				{
					string typeName = string.IsNullOrEmpty(this.FileBrowserContentProviderTypeName) ? typeof(FileSystemContentProvider).FullName : this.FileBrowserContentProviderTypeName;
					this._fileBrowserContentProviderType = Type.GetType(typeName);
				}
				return this._fileBrowserContentProviderType;
			}
		}

		// Token: 0x17004D6F RID: 19823
		// (get) Token: 0x0601008A RID: 65674 RVA: 0x00398E8E File Offset: 0x0039708E
		// (set) Token: 0x0601008B RID: 65675 RVA: 0x00398EAE File Offset: 0x003970AE
		public string FileBrowserContentProviderTypeName
		{
			get
			{
				return ((string)this.ViewState["FileBrowserContentProviderTypeName"]) ?? string.Empty;
			}
			set
			{
				this.ViewState["FileBrowserContentProviderTypeName"] = value;
			}
		}

		// Token: 0x17004D70 RID: 19824
		// (get) Token: 0x0601008C RID: 65676 RVA: 0x00398EC4 File Offset: 0x003970C4
		private FileBrowserContentProvider ContentProvider
		{
			get
			{
				if (this._contentProvider == null)
				{
					this._contentProvider = (FileBrowserContentProvider)Activator.CreateInstance(this.FileBrowserContentProviderType, new object[]
					{
						this.Context,
						new string[0],
						this.ViewPaths,
						this.UploadPaths,
						this.DeletePaths,
						"",
						""
					});
				}
				return this._contentProvider;
			}
		}

		// Token: 0x17004D71 RID: 19825
		// (get) Token: 0x0601008D RID: 65677 RVA: 0x00398F3A File Offset: 0x0039713A
		// (set) Token: 0x0601008E RID: 65678 RVA: 0x00398F6A File Offset: 0x0039716A
		public string[] ViewPaths
		{
			get
			{
				if (this.ViewState["ViewPaths"] == null)
				{
					return new string[0];
				}
				return (string[])this.ViewState["ViewPaths"];
			}
			set
			{
				this.ViewState["ViewPaths"] = value;
			}
		}

		// Token: 0x17004D72 RID: 19826
		// (get) Token: 0x0601008F RID: 65679 RVA: 0x00398F7D File Offset: 0x0039717D
		// (set) Token: 0x06010090 RID: 65680 RVA: 0x00398FAD File Offset: 0x003971AD
		public string[] UploadPaths
		{
			get
			{
				if (this.ViewState["UploadPaths"] == null)
				{
					return new string[0];
				}
				return (string[])this.ViewState["UploadPaths"];
			}
			set
			{
				this.ViewState["UploadPaths"] = value;
			}
		}

		// Token: 0x17004D73 RID: 19827
		// (get) Token: 0x06010091 RID: 65681 RVA: 0x00398FC0 File Offset: 0x003971C0
		// (set) Token: 0x06010092 RID: 65682 RVA: 0x00398FF0 File Offset: 0x003971F0
		public string[] DeletePaths
		{
			get
			{
				if (this.ViewState["DeletePaths"] == null)
				{
					return new string[0];
				}
				return (string[])this.ViewState["DeletePaths"];
			}
			set
			{
				this.ViewState["DeletePaths"] = value;
			}
		}

		// Token: 0x06010093 RID: 65683 RVA: 0x00399004 File Offset: 0x00397204
		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);
			this.EnsureChildControls();
			this.ajaxPanel = (this.FindControl("RadAjaxPanel1") as RadAjaxPanel);
			this.imgEditor = (this.FindControl("RadImageEditor1") as RadImageEditor);
			this.InitializeImageEditor();
		}

		// Token: 0x06010094 RID: 65684 RVA: 0x00399050 File Offset: 0x00397250
		private void InitializeImageEditor()
		{
			this.imgEditor.Language = base.Language;
			this.imgEditor.IsInRadEditor = true;
			this.imgEditor.ImageManager.EnableContentProvider = true;
			this.imgEditor.ImageManager.ContentProviderTypeName = this.FileBrowserContentProviderTypeName;
			this.imgEditor.ImageLoading += this.imgEditor_ImageLoading;
			string text = (this.Parameters["ImageEditorHttpHandlerUrl"] != null) ? this.Parameters["ImageEditorHttpHandlerUrl"].ToString() : "";
			if (text != "")
			{
				this.imgEditor.HttpHandlerUrl = text;
			}
		}

		// Token: 0x06010095 RID: 65685 RVA: 0x00399100 File Offset: 0x00397300
		private void imgEditor_ImageLoading(object sender, ImageEditorLoadingEventArgs args)
		{
			string text = this.Page.Request.Form["OriginalImageLocation"] ?? string.Empty;
			text = text.Replace('\\', this.PathSeparator);
			if (!string.IsNullOrEmpty(text))
			{
				this.imgEditor.ImageUrl = text;
			}
		}

		// Token: 0x06010096 RID: 65686 RVA: 0x00399154 File Offset: 0x00397354
		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
			if (this.Page.IsPostBack)
			{
				this.imgEditor.ImageManager.ViewPaths = (this.ViewPaths = this.Parameters.ViewPaths);
				this.imgEditor.ImageManager.UploadPaths = (this.UploadPaths = this.Parameters.UploadPaths);
				this.imgEditor.ImageManager.DeletePaths = (this.DeletePaths = this.Parameters.DeletePaths);
				if (this.ajaxPanel.IsAjaxRequest)
				{
					string text = this.Page.Request.Form["OriginalImageLocation"] ?? string.Empty;
					text = text.Replace('\\', this.PathSeparator);
					if (this.Page.Request.Form["SaveData"] != "")
					{
						char[] separator = new char[]
						{
							':'
						};
						string[] array = this.Page.Request.Form["SaveData"].Split(separator, StringSplitOptions.RemoveEmptyEntries);
						if (array.Length < 2)
						{
							this.ajaxPanel.Alert(this.Localization.GetString("MessageNoValidDimensions"));
						}
						string text2 = array[1] ?? string.Empty;
						text2 = text2.Replace('\\', this.PathSeparator);
						int num = text.LastIndexOf(this.PathSeparator);
						if (text2.LastIndexOf(this.PathSeparator) != -1 || num == -1 || !this.ContentProvider.CheckWritePermissions(text.Substring(0, num + 1)))
						{
							this.ajaxPanel.Alert(this.Localization.GetString("MessageCannotWriteToFolder"));
							return;
						}
						bool overwrite = array[0] == "true";
						this.SaveImage(text2, overwrite);
						return;
					}
					else
					{
						this.imgEditor.ResetChanges();
					}
				}
			}
		}

		// Token: 0x06010097 RID: 65687 RVA: 0x00399340 File Offset: 0x00397540
		private void SaveImage(string newImageName, bool overwrite)
		{
			try
			{
				string text = this.imgEditor.SaveEditableImage(newImageName, overwrite);
				if (!string.IsNullOrEmpty(text))
				{
					this.ajaxPanel.Alert(this.Localization.GetString(text));
				}
				else
				{
					StringBuilder stringBuilder = new StringBuilder();
					stringBuilder.Append("Telerik.Web.UI.Dialogs.CommonDialogScript.get_windowReference().close(");
					stringBuilder.Append("{ _newImageSrc : '");
					stringBuilder.Append(this.imgEditor.ImageUrl);
					stringBuilder.Append("', get_newImageSrc : function() { return this._newImageSrc;}}");
					stringBuilder.Append(");");
					this.ajaxPanel.ResponseScripts.Add(stringBuilder.ToString());
				}
			}
			catch (Exception ex)
			{
				this.ajaxPanel.Alert(this.Localization.GetString("MessageCannotWriteToFolder") + ex.Message.Replace("\"", "'"));
			}
		}

		// Token: 0x040048A6 RID: 18598
		private Type _fileBrowserContentProviderType;

		// Token: 0x040048A7 RID: 18599
		private FileBrowserContentProvider _contentProvider;

		// Token: 0x040048A8 RID: 18600
		private DialogParameters _dialogParameters;

		// Token: 0x040048A9 RID: 18601
		private RadImageEditor imgEditor;

		// Token: 0x040048AA RID: 18602
		protected RadAjaxPanel ajaxPanel;
	}
}
