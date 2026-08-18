using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Drawing.Design;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Telerik.Licensing;
using Telerik.Web.UI.AsyncUpload;
using Telerik.Web.UI.Editor;
using Telerik.Web.UI.Editor.DialogControls;
using Telerik.Web.UI.FileExplorer;
using Telerik.Web.UI.Widgets;

namespace Telerik.Web.UI
{
	// Token: 0x0200184E RID: 6222
	[ClientScriptResource("Telerik.Web.UI.RadFileExplorer", "Telerik.Web.UI.FileExplorer.RadFileExplorer.js")]
	[TelerikToolboxCategory("Miscellaneous")]
	[Designer("Telerik.Web.Design.RadFileExplorerDesigner, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4")]
	[RequiredScript(typeof(RadFileExplorerScripts))]
	[Description("Telerik File Explorer component")]
	[ToolboxData("<{0}:RadFileExplorer Runat=server></{0}:RadFileExplorer>")]
	[ToolboxBitmap(typeof(RadFileExplorer), "Telerik.Web.UI.FileExplorer.png")]
	[EmbeddedSkin("FileExplorer")]
	[LicenseProvider(typeof(TelerikLicenseProvider))]
	[RequiredCss("Telerik.Web.UI.Skins.Common.fonticons.css", RenderMode.Lightweight, typeof(RadFileExplorer))]
	[LightweightRendering]
	[EmbeddedSkin("FileExplorer", "Default")]
	public class RadFileExplorer : RadWebControl, ILocalizableControl, ICallbackEventHandler, INamingContainer, ISkinnableControl, IControl
	{
		// Token: 0x0600F180 RID: 61824 RVA: 0x0036E238 File Offset: 0x0036C438
		protected internal override void DescribeClientProperties(IScriptDescriptor descriptor)
		{
			base.DescribeProperty<bool>(descriptor, "allowPaging", this.AllowPaging, false);
			base.DescribeProperty<bool>(descriptor, "enableFilteringOnEnter", this.EnableFilteringOnEnterPressed, false);
			base.DescribeProperty<bool>(descriptor, "enableOpenFile", this.EnableOpenFile, true);
			base.DescribeProperty<bool>(descriptor, "overwriteExistingFiles", this.OverwriteExistingFiles, false);
			base.DescribeClientProperties(descriptor);
		}

		// Token: 0x0600F181 RID: 61825 RVA: 0x0036E298 File Offset: 0x0036C498
		protected internal override void DescribeClientEvents(IScriptDescriptor descriptor)
		{
			RadWebControl.DescribeEvent(descriptor, "copy", this.OnClientCopy);
			RadWebControl.DescribeEvent(descriptor, "createNewFolder", this.OnClientCreateNewFolder);
			RadWebControl.DescribeEvent(descriptor, "delete", this.OnClientDelete);
			RadWebControl.DescribeEvent(descriptor, "fileOpen", this.OnClientFileOpen);
			RadWebControl.DescribeEvent(descriptor, "filesDropping", this.OnClientFilesDropping);
			RadWebControl.DescribeEvent(descriptor, "filter", this.OnClientFilter);
			RadWebControl.DescribeEvent(descriptor, "folderChange", this.OnClientFolderChange);
			RadWebControl.DescribeEvent(descriptor, "folderLoaded", this.OnClientFolderLoaded);
			RadWebControl.DescribeEvent(descriptor, "init", this.OnClientInit);
			RadWebControl.DescribeEvent(descriptor, "itemSelected", this.OnClientItemSelected);
			RadWebControl.DescribeEvent(descriptor, "load", this.OnClientLoad);
			RadWebControl.DescribeEvent(descriptor, "move", this.OnClientMove);
			base.DescribeClientEvents(descriptor);
		}

		// Token: 0x170048EF RID: 18671
		// (get) Token: 0x0600F182 RID: 61826 RVA: 0x0036E378 File Offset: 0x0036C578
		protected char PathSeparator
		{
			get
			{
				return this.ContentProvider.PathSeparator;
			}
		}

		// Token: 0x0600F183 RID: 61827 RVA: 0x0036E388 File Offset: 0x0036C588
		public RadFileExplorer()
		{
			this._fileList = new FileList
			{
				Configuration = this.Configuration,
				Localization = new DialogsStrings(new LocalizationProvider("RadEditor.Dialogs", this, this.LocalizationPath), "FileExplorer", false),
				ViewMode = this.ExplorerMode,
				FilterTextBoxLabel = this.FilterTextBoxLabel,
				AvailableFileListControls = this.AvailableFileListControls
			};
			this.Configuration.SearchPatterns = new string[]
			{
				"*.*"
			};
			this.pathsChanged = false;
		}

		// Token: 0x0600F184 RID: 61828 RVA: 0x0036E470 File Offset: 0x0036C670
		protected void RaiseFileExplorerItemEvent(RadFileExplorerEventArgs e, object eventKey)
		{
			RadFileExplorerEventHandler radFileExplorerEventHandler = (RadFileExplorerEventHandler)base.Events[eventKey];
			if (radFileExplorerEventHandler != null)
			{
				radFileExplorerEventHandler(this, e);
			}
		}

		// Token: 0x0600F185 RID: 61829 RVA: 0x0036E49C File Offset: 0x0036C69C
		protected void RaiseFileExplorerExplorerPopulated(RadFileExplorerPopulatedEventArgs e, object eventKey)
		{
			RadFileExplorerGridEventHandler radFileExplorerGridEventHandler = (RadFileExplorerGridEventHandler)base.Events[eventKey];
			if (radFileExplorerGridEventHandler != null)
			{
				radFileExplorerGridEventHandler(this, e);
			}
		}

		// Token: 0x0600F186 RID: 61830 RVA: 0x0036E4C6 File Offset: 0x0036C6C6
		protected virtual void OnItemCommand(RadFileExplorerEventArgs e)
		{
			this.RaiseFileExplorerItemEvent(e, RadFileExplorer.itemCommandEvent);
		}

		// Token: 0x0600F187 RID: 61831 RVA: 0x0036E4D4 File Offset: 0x0036C6D4
		protected virtual void OnExplorerPopulated(RadFileExplorerPopulatedEventArgs e)
		{
			this.RaiseFileExplorerExplorerPopulated(e, RadFileExplorer.explorerPopulatedEvent);
		}

		// Token: 0x0600F188 RID: 61832 RVA: 0x0036E4FC File Offset: 0x0036C6FC
		protected override void DescribeComponent(IScriptDescriptor descriptor)
		{
			base.DescribeComponent(descriptor);
			if (this._toolbar.Visible)
			{
				descriptor.AddComponentProperty("toolbar", this._toolbar.ClientID);
			}
			if (this.Grid != null)
			{
				descriptor.AddComponentProperty("grid", this.Grid.ClientID);
			}
			if (this.ListView != null)
			{
				descriptor.AddComponentProperty("listView", this.ListView.ClientID);
			}
			if (this.Splitter != null)
			{
				descriptor.AddComponentProperty("splitter", this.Splitter.ClientID);
			}
			if (this._gridContextMenu.Visible)
			{
				descriptor.AddComponentProperty("gridContextMenu", this._gridContextMenu.ClientID);
			}
			if (this._loadingPanel.Visible)
			{
				descriptor.AddComponentProperty("ajaxLoadingPanel", this._loadingPanel.ClientID);
			}
			if (this._windowManager.Visible)
			{
				descriptor.AddComponentProperty("windowManager", this._windowManager.ClientID);
			}
			if (this._asyncUpload.Visible)
			{
				descriptor.AddComponentProperty("asyncUpload", this._asyncUpload.ClientID);
			}
			if (this.radPaneGrid != null)
			{
				descriptor.AddComponentProperty("gridPane", this.radPaneGrid.ClientID);
			}
			if (this.radpaneTree != null)
			{
				descriptor.AddComponentProperty("treePane", this.radpaneTree.ClientID);
			}
			if (this._tooltip.Visible)
			{
				descriptor.AddComponentProperty("tooltipControl", this._tooltip.ClientID);
			}
			if (this._addressBox.Visible)
			{
				descriptor.AddProperty("addressBox", this._addressBox.ClientID);
			}
			descriptor.AddProperty("postbackButton", this._postbackButton.ClientID);
			descriptor.AddProperty("_treeId", this._tree.ClientID);
			descriptor.AddProperty("_postbackArgumentInputID", this._postbackArgument.ClientID);
			descriptor.AddProperty("_currentDirectoryInputID", this._currentFolderInput.ClientID);
			descriptor.AddProperty("_commandArgumentInputID", this._feCommandArgument.ClientID);
			JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			foreach (string key in this.LocalizationKeys)
			{
				dictionary[key] = this.Localization.GetString(key);
			}
			descriptor.AddScriptProperty("_localization", javaScriptSerializer.Serialize(dictionary));
			if (string.IsNullOrEmpty(this._selectedFile) && !string.IsNullOrEmpty(this.InitialPath) && !this.InitialPath.EndsWith(this.PathSeparator.ToString()))
			{
				string text = this.InitialPath.Substring(this.InitialPath.LastIndexOf(this.PathSeparator.ToString()) + 1);
				if (!string.IsNullOrEmpty(text))
				{
					this._selectedFile = text;
				}
			}
			if (!string.IsNullOrEmpty(this._selectedFile))
			{
				descriptor.AddProperty("_selectedFile", this._selectedFile);
			}
			this.Page.ClientScript.GetCallbackEventReference(this, null, null, null);
			descriptor.AddProperty("_uniqueId", this.UniqueID);
			descriptor.AddProperty("_pathSeparator", this.PathSeparator);
			if (this._fileList.Visible && !string.IsNullOrEmpty(this.CurrentFolder))
			{
				RadFileExplorer.callbackResponseStruct initialGridData = default(RadFileExplorer.callbackResponseStruct);
				int num = this.AllowPaging ? this.PageSize : int.MaxValue;
				if (this.AllowPaging)
				{
					List<FileBrowserItem> explorerData = this.GetExplorerData(this.CurrentFolder, "", 0, int.MaxValue, true, "grid", out initialGridData.count);
					int num2 = explorerData.FindIndex((FileBrowserItem fi) => fi.Path.Equals(this.InitialPath));
					int num3 = (num2 == -1) ? 0 : (num2 / num);
					int num4 = num * num3;
					int count = (num4 + num > explorerData.Count) ? (explorerData.Count - num4) : num;
					initialGridData.data = explorerData.GetRange(num4, count);
					descriptor.AddProperty("_initialGridPageIndex", num3.ToString());
				}
				else
				{
					initialGridData.data = this.GetExplorerData(this.CurrentFolder, "", 0, num, true, "grid", out initialGridData.count);
				}
				descriptor.AddScriptProperty("_initialGridData", RadFileExplorer.SerializeGridData(initialGridData));
			}
			if (this.AllowPaging)
			{
				descriptor.AddProperty("_gridPageSize", this.PageSize);
			}
			descriptor.AddProperty("_enableCopy", this.EnableCopy);
			descriptor.AddProperty("_explorerMode", this.ExplorerMode);
			descriptor.AddProperty("_allowFileExtensionRename", this.Configuration.AllowFileExtensionRename);
			descriptor.AddProperty("_enabled", base.IsEnabled);
			string text2 = this.KeyboardShortcuts.ToString();
			if (text2 != "[]")
			{
				descriptor.AddProperty("shortcuts", text2);
			}
			descriptor.AddProperty("renderMode", this.ResolvedRenderMode);
		}

		// Token: 0x0600F189 RID: 61833 RVA: 0x0036EA20 File Offset: 0x0036CC20
		private static string SerializeGridData(RadFileExplorer.callbackResponseStruct initialGridData)
		{
			StringBuilder stringBuilder = new StringBuilder("{\"count\":");
			stringBuilder.Append(initialGridData.count);
			stringBuilder.Append(",\"data\":[");
			foreach (FileBrowserItem fileBrowserItem in initialGridData.data)
			{
				stringBuilder.Append("{");
				FileItem fileItem = fileBrowserItem as FileItem;
				if (fileItem != null)
				{
					stringBuilder.AppendFormat("\"Extension\":\"{0}\",", RadFileExplorer.SanitizeAttributeValue(fileItem.Extension, false));
					stringBuilder.AppendFormat("\"Length\":{0},", fileItem.Length.ToString());
					stringBuilder.AppendFormat("\"Url\":\"{0}\",", RadFileExplorer.SanitizeAttributeValue(fileItem.Url, false));
				}
				stringBuilder.AppendFormat("\"Path\":\"{0}\",", RadFileExplorer.SanitizeAttributeValue(fileBrowserItem.Path, false));
				stringBuilder.AppendFormat("\"Name\":\"{0}\",", RadFileExplorer.SanitizeAttributeValue(fileBrowserItem.Name, false));
				stringBuilder.AppendFormat("\"Permissions\":{0},", ((int)fileBrowserItem.Permissions).ToString());
				stringBuilder.Append("\"Attributes\":{");
				if (fileBrowserItem.Attributes != null)
				{
					foreach (object obj in fileBrowserItem.Attributes.Keys)
					{
						string text = (string)obj;
						string text2 = RadFileExplorer.SanitizeAttributeValue(fileBrowserItem.Attributes[text], false);
						text2.Replace("\f", " ").Replace("\v", string.Empty).Replace("\t", " ");
						stringBuilder.AppendFormat("\"{0}\" : \"{1}\",", text, text2);
					}
					if (fileBrowserItem.Attributes.Keys.Count > 0)
					{
						stringBuilder.Remove(stringBuilder.Length - 1, 1);
					}
				}
				stringBuilder.Append("}");
				stringBuilder.Append("},");
			}
			if (initialGridData.data.Count > 0)
			{
				stringBuilder.Remove(stringBuilder.Length - 1, 1);
			}
			stringBuilder.Append("]}");
			return stringBuilder.ToString();
		}

		// Token: 0x0600F18A RID: 61834 RVA: 0x0036EC80 File Offset: 0x0036CE80
		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);
			this._fileList.PrefixID = this.ClientID;
			this.EnsureChildControls();
			this.SetRenderModeChildRadControls();
			this.Configuration.PathChange += this.OnPathChanged;
		}

		// Token: 0x0600F18B RID: 61835 RVA: 0x0036ECBD File Offset: 0x0036CEBD
		protected override void RenderChildren(HtmlTextWriter writer)
		{
			if (base.DesignMode)
			{
				writer.Write(SkinRegistrar.GetDesignTimeStyleSheet(this));
			}
			base.RenderChildren(writer);
		}

		// Token: 0x0600F18C RID: 61836 RVA: 0x0036ECDA File Offset: 0x0036CEDA
		private void ApplyConfigurationToAsyncUpload()
		{
			this._asyncUpload.MaxFileSize = this.Configuration.MaxUploadFileSize;
			this._asyncUpload.AllowedFileExtensions = this.GetSearchPatternsForAsyncUpload();
		}

		// Token: 0x0600F18D RID: 61837 RVA: 0x0036ED03 File Offset: 0x0036CF03
		private void ConfigureUpload()
		{
			this._upload.MaxFileSize = this.Configuration.MaxUploadFileSize;
			this._upload.AllowedFileExtensions = this.GetSearchPatternsForAsyncUpload();
		}

		// Token: 0x0600F18E RID: 61838 RVA: 0x0036ED34 File Offset: 0x0036CF34
		protected override void ControlPreRender()
		{
			this.EnsureChildControls();
			this.ConfigureToolbarButtons();
			if (this._tree != null && !this._isTreeMenuCreated)
			{
				this._tree.ContextMenus.Clear();
				this._tree.ContextMenus.Add(this.CreateTreeViewContextMenu());
			}
			this.EnableDisableControls();
			if (this.Page.IsPostBack && ((this._upload != null && (this._upload.UploadedFiles.Count > 0 || this._upload.InvalidFiles.Count > 0)) || (this._asyncUpload != null && this._asyncUpload.UploadedFiles.Count > 0)))
			{
				this.ProcessUploadedFiles();
			}
			if (this._tree != null)
			{
				if (this.pathsChanged || this._tree.Nodes.Count == 0)
				{
					this.BindTreeView(delegate
					{
						this.BindExplorer();
					});
				}
				else
				{
					this._shouldBindTree = true;
				}
			}
			this.EnableDisableCommand("NewFolder", this.EnableCreateNewFolder && this.ContentProvider.CanCreateDirectory);
			this.EnableDisableCommand("Open", this.EnableOpenFile);
			this.ProcessFileListPaging(this.AllowPaging);
			this.ProcessFileListMultipleSelection(this.Configuration.AllowMultipleSelection);
			this._fileList.PageSize = this.PageSize;
			if (base.IsSkinSet || this.ViewState["EnableEmbeddedSkins"] != null || this.ViewState["EnableEmbeddedBaseStylesheet"] != null)
			{
				this.ApplySkin(this, base.RuntimeSkin);
			}
			this.ApplyConfigurationToAsyncUpload();
			this.ConfigureUpload();
			string popupWindowClose = this.KeyboardShortcuts.PopupWindowClose;
			if (string.IsNullOrEmpty(popupWindowClose))
			{
				this._windowManager.EnableAriaSupport = false;
			}
			else
			{
				this._windowManager.EnableAriaSupport = true;
				this._windowManager.Shortcuts.Add("close", popupWindowClose);
			}
			if (this._shouldBindTree)
			{
				this._tree.DataBind();
			}
			base.ControlPreRender();
			if (this._loadingPanel != null && !this._loadingPanel.IsSkinSet)
			{
				this._loadingPanel.Skin = SkinRegistrar.GetRuntimeSkin(this);
			}
			if (this._addressBox != null)
			{
				this._addressBox.Text = this.CurrentFolder;
			}
			this.UpdateLocalization();
			this._fileList.EnableFilter = this.EnableFilterTextBox;
			this._fileList.ControlsPreRender();
			if (this._uploadContainer != null)
			{
				this._uploadContainer.Visible = true;
			}
			if (this.Configuration.UploadPaths.Length == 0)
			{
				if (this._uploadContainer != null)
				{
					this._uploadContainer.Visible = false;
				}
				this.EnableDisableCommand("Upload", false);
				this.EnableDisableCommand("NewFolder", false);
				this.EnableDisableCommand("Rename", false);
			}
			if (this.Configuration.DeletePaths.Length == 0)
			{
				this.EnableDisableCommand("Delete", false);
			}
			this.EnableDisableCommand("Copy", this.EnableCopy);
			this.EnableDisableCommand("Paste", this.EnableCopy);
			this.DisplaySelectedUploadControl();
			this.DisplayDragAndDropRelatedControls();
			this.ProcessVisibleControls();
			this.EnableDisableControls();
			this.SetUploadDialogDimentions();
			this.SyncPreviewModeButtonsState();
		}

		// Token: 0x0600F18F RID: 61839 RVA: 0x0036F048 File Offset: 0x0036D248
		internal virtual void BindTreeView(Action bindExplorer)
		{
			List<RadTreeNode> treeViewExpandState = this.GetTreeViewExpandState();
			List<string> collapsedTreeViewNodesValues = this.GetCollapsedTreeViewNodesValues();
			this.selectedTreeViewNodeValue = this.GetSelectedTreeViewNodeValue();
			string a = (!this.TreeView.IsEmpty) ? this.TreeView.Nodes[0].Value : null;
			bindExplorer();
			string b = (!this.TreeView.IsEmpty) ? this.TreeView.Nodes[0].Value : null;
			if (a == b)
			{
				this.SetTreeViewExpandState(treeViewExpandState);
				this.SetSelectedTreeViewNodeByValue(this.selectedTreeViewNodeValue);
				this.SetCollapsedTreeViewNodesByValue(collapsedTreeViewNodesValues);
			}
		}

		// Token: 0x0600F190 RID: 61840 RVA: 0x0036F0E8 File Offset: 0x0036D2E8
		internal virtual string GetSelectedTreeViewNodeValue()
		{
			RadTreeNode selectedNode = this.TreeView.SelectedNode;
			if (selectedNode == null)
			{
				return string.Empty;
			}
			return selectedNode.Value;
		}

		// Token: 0x0600F191 RID: 61841 RVA: 0x0036F110 File Offset: 0x0036D310
		internal virtual List<RadTreeNode> GetTreeViewExpandState()
		{
			if (this.TreeView.Nodes.Count <= 0)
			{
				return new List<RadTreeNode>();
			}
			return this.GetLeafNodesOfTreeNodesContainer(this.TreeView);
		}

		// Token: 0x0600F192 RID: 61842 RVA: 0x0036F138 File Offset: 0x0036D338
		private List<RadTreeNode> GetLeafNodesOfTreeNodesContainer(IRadTreeNodeContainer nodesContainer)
		{
			List<RadTreeNode> list = new List<RadTreeNode>();
			if (nodesContainer.Nodes.Count > 0)
			{
				using (IEnumerator enumerator = nodesContainer.Nodes.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						object obj = enumerator.Current;
						RadTreeNode nodesContainer2 = (RadTreeNode)obj;
						list.AddRange(this.GetLeafNodesOfTreeNodesContainer(nodesContainer2));
					}
					return list;
				}
			}
			list.Add(nodesContainer as RadTreeNode);
			return list;
		}

		// Token: 0x0600F193 RID: 61843 RVA: 0x0036F1CC File Offset: 0x0036D3CC
		internal virtual List<string> GetCollapsedTreeViewNodesValues()
		{
			return (from n in this.TreeView.GetAllNodes()
			where !n.Expanded
			select n.Value).ToList<string>();
		}

		// Token: 0x0600F194 RID: 61844 RVA: 0x0036F230 File Offset: 0x0036D430
		internal virtual void SetTreeViewExpandState(List<RadTreeNode> leafNodes)
		{
			foreach (RadTreeNode radTreeNode in leafNodes)
			{
				if (this.ShouldExpandNode(radTreeNode))
				{
					this.ExpandNodeByValue(radTreeNode.ParentNode.Value);
				}
			}
		}

		// Token: 0x0600F195 RID: 61845 RVA: 0x0036F294 File Offset: 0x0036D494
		internal virtual bool ShouldExpandNode(RadTreeNode node)
		{
			return node.ParentNode != null && this.TreeView.FindNodeByValue(node.Value) == null;
		}

		// Token: 0x0600F196 RID: 61846 RVA: 0x0036F2B4 File Offset: 0x0036D4B4
		internal virtual void ExpandNodeByValue(string path)
		{
			if (!string.IsNullOrEmpty(path))
			{
				this.ExpandNestedFolder(path);
			}
		}

		// Token: 0x0600F197 RID: 61847 RVA: 0x0036F2C8 File Offset: 0x0036D4C8
		internal virtual void SetSelectedTreeViewNodeByValue(string value)
		{
			RadTreeNode radTreeNode = this.TreeView.FindNodeByValue(value);
			if (radTreeNode != null)
			{
				this.SelectTreeViewNode(radTreeNode);
			}
		}

		// Token: 0x0600F198 RID: 61848 RVA: 0x0036F2EC File Offset: 0x0036D4EC
		internal virtual void SelectTreeViewNode(RadTreeNode node)
		{
			node.Selected = true;
			this._currentFolderInput.Value = (RadFileExplorer.IsNodeADirectory(node) ? node.Value : node.ParentNode.Value);
		}

		// Token: 0x0600F199 RID: 61849 RVA: 0x0036F31C File Offset: 0x0036D51C
		internal virtual void SetCollapsedTreeViewNodesByValue(List<string> collapsedNodesValues)
		{
			foreach (string value in collapsedNodesValues)
			{
				RadTreeNode radTreeNode = this.TreeView.FindNodeByValue(value);
				if (radTreeNode != null)
				{
					radTreeNode.Expanded = false;
				}
			}
		}

		// Token: 0x0600F19A RID: 61850 RVA: 0x0036F37C File Offset: 0x0036D57C
		protected void UpdateLocalization()
		{
			decimal d = this.Configuration.MaxUploadFileSize;
			string text = " bytes";
			if (d > 1024m)
			{
				d /= 1024m;
				text = " KB";
			}
			if (d > 1024m)
			{
				d /= 1024m;
				text = " MB";
			}
			this._infoFields.InnerHtml = string.Format("<dl><dt>{0}:</dt><dd>{1} {2}</dd><dt>{3}:</dt><dd>{4}</dd></dl>", new object[]
			{
				this.Localization.GetString("MaxFileSize"),
				d.ToString("0,0.00"),
				text,
				this.Localization.GetString("AllowedExtensions"),
				string.Join(", ", this.Configuration.SearchPatterns)
			});
			this._upload.Localization.Add = this.Localization.GetString("Add");
			this._upload.Localization.Clear = this.Localization.GetString("Clear");
			this._upload.Localization.Delete = this.Localization.GetString("Delete");
			this._upload.Localization.Remove = this.Localization.GetString("Remove");
			this._upload.Localization.Select = this.Localization.GetString("Select");
			this._asyncUpload.Localization.Cancel = this.Localization.GetString("Cancel");
			this._asyncUpload.Localization.Remove = this.Localization.GetString("Remove");
			this._asyncUpload.Localization.Select = this.Localization.GetString("Select");
			this.chkOverwrite.Text = this.Localization.GetString("OverwriteExisting");
			this.uploadButton.Text = this.Localization.GetString("Upload");
			this._windowManager.Title = this.Localization.GetString("Upload");
			this._windowManager.Localization.Close = this.Localization.GetString("Close");
			this._windowManager.Localization.Cancel = this.Localization.GetString("Cancel");
			this._windowManager.Localization.OK = this.Localization.GetString("OK");
			if (this._tree != null && this._tree.ContextMenus.Count > 0)
			{
				this.UpdateItemsLocalization(this._tree.ContextMenus[0].Items);
			}
			if (this._gridContextMenu != null)
			{
				this.UpdateItemsLocalization(this._gridContextMenu.Items);
			}
			if (this._toolbar != null)
			{
				this.UpdateItemsLocalization(this._toolbar.Items);
				foreach (object obj in this._toolbar.Items)
				{
					RadToolBarItem radToolBarItem = (RadToolBarItem)obj;
					string @string = this.Localization.GetString(radToolBarItem.ToolTip);
					if (!string.IsNullOrEmpty(@string))
					{
						radToolBarItem.ToolTip = @string;
					}
				}
			}
		}

		// Token: 0x0600F19B RID: 61851 RVA: 0x0036F6F4 File Offset: 0x0036D8F4
		private void DisplaySelectedUploadControl()
		{
			this._upload.Visible = !this.Configuration.EnableAsyncUpload;
			this._asyncUpload.Visible = this.Configuration.EnableAsyncUpload;
		}

		// Token: 0x0600F19C RID: 61852 RVA: 0x0036F728 File Offset: 0x0036D928
		private void DisplayDragAndDropRelatedControls()
		{
			this.dropUploadInfoPanel.Visible = (this.dropZone.Visible = this.Configuration.EnableAsyncUpload);
		}

		// Token: 0x0600F19D RID: 61853 RVA: 0x0036F75C File Offset: 0x0036D95C
		private void SetUploadDialogDimentions()
		{
			if (this._windowManager != null)
			{
				this._windowManager.Width = Unit.Pixel(451);
				this._windowManager.Height = Unit.Pixel(340);
				string runtimeSkin;
				if ((runtimeSkin = SkinRegistrar.GetRuntimeSkin(this)) != null)
				{
					if (runtimeSkin == "Silk" || runtimeSkin == "Glow")
					{
						this._upload.InputSize = 35;
						this._asyncUpload.InputSize = 35;
						return;
					}
					if (runtimeSkin == "MetroTouch" || runtimeSkin == "BlackMetroTouch")
					{
						this._upload.InputSize = 40;
						this._asyncUpload.InputSize = 40;
						this._windowManager.Width = Unit.Pixel(534);
						this._windowManager.Height = Unit.Pixel(380);
						return;
					}
					if (!(runtimeSkin == "Bootstrap"))
					{
						return;
					}
					this._windowManager.Height = Unit.Pixel(360);
				}
			}
		}

		// Token: 0x0600F19E RID: 61854 RVA: 0x0036F864 File Offset: 0x0036DA64
		private void UpdateItemsLocalization(ControlItemCollection items)
		{
			foreach (object obj in items)
			{
				ControlItem controlItem = (ControlItem)obj;
				if (controlItem.Text != "&nbsp;")
				{
					string @string = this.Localization.GetString(controlItem.Text);
					if (!string.IsNullOrEmpty(@string))
					{
						controlItem.Text = @string;
					}
				}
			}
		}

		// Token: 0x0600F19F RID: 61855 RVA: 0x0036F8E4 File Offset: 0x0036DAE4
		private void ProcessVisibleControls()
		{
			FileExplorerControls visibleControls = this.VisibleControls;
			if (this._fileList.ControlsAreCreated)
			{
				bool flag = (visibleControls & FileExplorerControls.FileList) != (FileExplorerControls)0 && this.ExplorerMode != FileExplorerMode.FileTree;
				this.radPaneGrid.Width = Unit.Empty;
				this.radPaneGrid.Collapsed = !flag;
				this.splitBar.Visible = flag;
				this._fileList.Visible = flag;
				this._gridContextMenu.Visible = flag;
				if (flag)
				{
					this.radpaneTree.Width = this.TreePaneWidth;
					this.radPaneGrid.SetExpandedSize(Unit.Empty);
				}
			}
			if ((visibleControls & FileExplorerControls.TreeView) == (FileExplorerControls)0 && this._tree != null)
			{
				this.splitBar.Visible = false;
				this._tree.EnableEmbeddedBaseStylesheet = false;
				this._tree.EnableEmbeddedSkins = false;
				this.radpaneTree.Collapsed = true;
			}
			else
			{
				if (this.ExplorerMode == FileExplorerMode.FileTree)
				{
					this._tree.MultipleSelect = true;
				}
				else
				{
					this._tree.MultipleSelect = false;
				}
				this._tree.EnableEmbeddedBaseStylesheet = this.EnableEmbeddedBaseStylesheet;
				this._tree.EnableEmbeddedSkins = this.EnableEmbeddedSkins;
				this.radpaneTree.Collapsed = false;
			}
			if ((visibleControls & FileExplorerControls.Toolbar) == (FileExplorerControls)0 && this._toolbar != null)
			{
				this._toolbar.Visible = false;
			}
			else if (this._toolbar != null && (this.ExplorerMode == FileExplorerMode.FileTree || (visibleControls & FileExplorerControls.Grid) == (FileExplorerControls)0 || (visibleControls & FileExplorerControls.ListView) == (FileExplorerControls)0))
			{
				this.EnableDisableCommand("GridView", false);
				this.EnableDisableCommand("ThumbnailsView", false);
			}
			if ((visibleControls & FileExplorerControls.AddressBox) == (FileExplorerControls)0 && this._addressBox != null)
			{
				this._addressBox.Visible = false;
			}
			if ((visibleControls & FileExplorerControls.ContextMenus) == (FileExplorerControls)0)
			{
				if (this._gridContextMenu != null)
				{
					this._gridContextMenu.Visible = false;
				}
				if (this._tree != null && this._tree.ContextMenus.Count > 0)
				{
					this._tree.ContextMenus.RemoveAt(0);
				}
			}
			else
			{
				if (this._tree != null && this._tree.ContextMenus.Count > 0)
				{
					bool flag2 = false;
					foreach (object obj in this._tree.ContextMenus[0].Items)
					{
						RadMenuItem radMenuItem = (RadMenuItem)obj;
						if (radMenuItem.Visible)
						{
							flag2 = true;
							break;
						}
					}
					if (!flag2)
					{
						this._tree.ContextMenus.RemoveAt(0);
					}
				}
				if (this._gridContextMenu != null)
				{
					bool visible = false;
					foreach (object obj2 in this._gridContextMenu.Items)
					{
						RadMenuItem radMenuItem2 = (RadMenuItem)obj2;
						if (radMenuItem2.Visible)
						{
							visible = true;
							break;
						}
					}
					this._gridContextMenu.Visible = visible;
				}
			}
			this.UpdateWidth();
			this.UpdateHeight();
		}

		// Token: 0x0600F1A0 RID: 61856 RVA: 0x0036FBE8 File Offset: 0x0036DDE8
		private void EnableDisableCommand(string commandName, bool toEnable)
		{
			if (this._toolbar != null)
			{
				RadToolBarItem radToolBarItem = this._toolbar.FindItemByValue(commandName);
				if (radToolBarItem != null)
				{
					radToolBarItem.Visible = toEnable;
				}
			}
			if (this._gridContextMenu != null)
			{
				RadMenuItem radMenuItem = this._gridContextMenu.FindItemByValue(commandName);
				if (radMenuItem != null)
				{
					radMenuItem.Visible = toEnable;
				}
			}
			if (this._tree != null && this._tree.ContextMenus.Count > 0)
			{
				RadMenuItem radMenuItem2 = this._tree.ContextMenus[0].FindItemByValue(commandName);
				if (radMenuItem2 != null)
				{
					radMenuItem2.Visible = toEnable;
				}
			}
		}

		// Token: 0x0600F1A1 RID: 61857 RVA: 0x0036FC70 File Offset: 0x0036DE70
		private void SyncPreviewModeButtonsState()
		{
			RadToolBarButton radToolBarButton = this._toolbar.FindChildByValue<RadToolBarButton>("GridView");
			RadToolBarButton radToolBarButton2 = this._toolbar.FindChildByValue<RadToolBarButton>("ThumbnailsView");
			if (radToolBarButton != null)
			{
				radToolBarButton.Checked = (this.ExplorerMode == FileExplorerMode.Default);
			}
			if (radToolBarButton2 != null)
			{
				radToolBarButton2.Checked = (this.ExplorerMode == FileExplorerMode.Thumbnails);
			}
		}

		// Token: 0x0600F1A2 RID: 61858 RVA: 0x0036FCC4 File Offset: 0x0036DEC4
		private static HtmlGenericControl CreateHiddenDiv()
		{
			HtmlGenericControl htmlGenericControl = new HtmlGenericControl("div");
			htmlGenericControl.Style.Add(HtmlTextWriterStyle.Width, "0px");
			htmlGenericControl.Style.Add(HtmlTextWriterStyle.Height, "0px");
			htmlGenericControl.Style.Add(HtmlTextWriterStyle.Overflow, "hidden");
			if (HttpContext.Current != null)
			{
				HttpBrowserCapabilities browser = HttpContext.Current.Request.Browser;
				if (browser.Browser == "IE" && browser.MajorVersion < 8)
				{
					htmlGenericControl.Style.Add(HtmlTextWriterStyle.Position, "relative");
				}
			}
			return htmlGenericControl;
		}

		// Token: 0x0600F1A3 RID: 61859 RVA: 0x0036FD58 File Offset: 0x0036DF58
		private void CreateUploadControls(ControlCollection container)
		{
			if (this._toolbar != null)
			{
				RadToolBarButton radToolBarButton = this.CreateToolBarButton("Upload", true);
				RadToolBarButton radToolBarButton2 = radToolBarButton;
				radToolBarButton2.OuterCssClass += " rtbGroupEnd";
				this._toolbar.Items.Add(radToolBarButton);
			}
			this._uploadContainer = new HtmlGenericControl("div");
			this._uploadContainer.Attributes["class"] = "rfeUploadContainer";
			HtmlGenericControl htmlGenericControl = RadFileExplorer.CreateHiddenDiv();
			htmlGenericControl.ID = "uploadContainer";
			htmlGenericControl.Controls.Add(this._uploadContainer);
			container.Add(htmlGenericControl);
			this._upload = new RadUpload();
			this._upload.ID = "upload1";
			this._upload.MaxFileSize = this.Configuration.MaxUploadFileSize;
			this._upload.ControlObjectsVisibility = (ControlObjectsVisibility.RemoveButtons | ControlObjectsVisibility.AddButton);
			this._upload.InputSize = 42;
			this._upload.InitialFileInputsCount = 3;
			this._uploadContainer.Controls.Add(this._upload);
			this._asyncUpload = this.CreateAsyncUpload();
			this._asyncUpload.ID = "asyncUpload1";
			this._asyncUpload.MultipleFileSelection = MultipleFileSelection.Automatic;
			this._asyncUpload.InputSize = 42;
			this._asyncUpload.DropZones = new string[]
			{
				"#" + this.radPaneGrid.ClientID
			};
			this._uploadContainer.Controls.Add(this._asyncUpload);
			HtmlGenericControl htmlGenericControl2 = new HtmlGenericControl("div");
			htmlGenericControl2.Attributes["class"] = "rfeCheckBoxContainer";
			this.chkOverwrite = new CheckBox();
			this.chkOverwrite.ID = "chkOverwrite";
			this.chkOverwrite.Checked = this.OverwriteExistingFiles;
			htmlGenericControl2.Controls.Add(this.chkOverwrite);
			this._uploadContainer.Controls.Add(htmlGenericControl2);
			this._infoFields = new HtmlGenericControl();
			this._infoFields.TagName = "div";
			this._infoFields.Attributes["class"] = "rfeUploadInfoPanel";
			this._uploadContainer.Controls.Add(this._infoFields);
			HtmlGenericControl htmlGenericControl3 = new HtmlGenericControl();
			htmlGenericControl3.TagName = "div";
			htmlGenericControl3.Attributes["class"] = "rfeUploadButtonContainer";
			this._uploadContainer.Controls.Add(htmlGenericControl3);
			this.uploadButton = new RadButton();
			this.uploadButton.ID = "btnUpload";
			this.uploadButton.CausesValidation = false;
			htmlGenericControl3.Controls.Add(this.uploadButton);
		}

		// Token: 0x0600F1A4 RID: 61860 RVA: 0x00370002 File Offset: 0x0036E202
		internal virtual RadAsyncUpload CreateAsyncUpload()
		{
			return new RadAsyncUpload();
		}

		// Token: 0x0600F1A5 RID: 61861 RVA: 0x0037000C File Offset: 0x0036E20C
		private string[] GetSearchPatternsForAsyncUpload()
		{
			string[] array = new string[this.Configuration.SearchPatterns.Length];
			this.Configuration.SearchPatterns.CopyTo(array, 0);
			for (int i = 0; i < array.Length; i++)
			{
				if (array[i] == "*.*")
				{
					array = new string[0];
					break;
				}
				array[i] = array[i].Substring(array[i].LastIndexOf(".") + 1);
			}
			return array;
		}

		// Token: 0x0600F1A6 RID: 61862 RVA: 0x00370080 File Offset: 0x0036E280
		private void CreateToolbarControl(ControlCollection container)
		{
			this._toolbar = new RadToolBar();
			this._toolbar.ID = "toolbar";
			this._toolbar.EnableViewState = false;
			container.Add(this._toolbar);
			if (base.DesignMode)
			{
				RadToolBarButton radToolBarButton = new RadToolBarButton("File Explorer Toolbar");
				if (!this.ResolvedRenderMode.Equals(RenderMode.Classic))
				{
					radToolBarButton.ShowText = ToolBarShowPosition.OverFlow;
				}
				this._toolbar.Items.Add(radToolBarButton);
			}
			RadToolBarButton radToolBarButton2 = this.CreateToolBarButton("Back", false);
			RadToolBarButton radToolBarButton3 = radToolBarButton2;
			radToolBarButton3.OuterCssClass += " rtbGroupStart";
			this._toolbar.Items.Add(radToolBarButton2);
			RadToolBarButton radToolBarButton4 = this.CreateToolBarButton("Forward", false);
			RadToolBarButton radToolBarButton5 = radToolBarButton4;
			radToolBarButton5.OuterCssClass += " rtbGroupIn";
			this._toolbar.Items.Add(radToolBarButton4);
			RadToolBarButton radToolBarButton6 = this.CreateToolBarButton("Open", false);
			RadToolBarButton radToolBarButton7 = radToolBarButton6;
			radToolBarButton7.OuterCssClass += " rtbGroupIn";
			this._toolbar.Items.Add(radToolBarButton6);
			RadToolBarButton radToolBarButton8 = this.CreateToolBarButton("Refresh", false);
			RadToolBarButton radToolBarButton9 = radToolBarButton8;
			radToolBarButton9.OuterCssClass += " rtbGroupIn";
			this._toolbar.Items.Add(radToolBarButton8);
			RadToolBarButton radToolBarButton10 = this.CreateToolBarButton("NewFolder", false);
			RadToolBarButton radToolBarButton11 = radToolBarButton10;
			radToolBarButton11.OuterCssClass += " rtbGroupIn";
			this._toolbar.Items.Add(radToolBarButton10);
			RadToolBarButton radToolBarButton12 = this.CreateToolBarButton("Delete", false);
			RadToolBarButton radToolBarButton13 = radToolBarButton12;
			radToolBarButton13.OuterCssClass += " rtbGroupIn";
			this._toolbar.Items.Add(radToolBarButton12);
			RadToolBarButton radToolBarButton14 = this.CreatePreviewModeButton("GridView");
			RadToolBarButton radToolBarButton15 = radToolBarButton14;
			radToolBarButton15.OuterCssClass += " rtbGroupIn";
			this._toolbar.Items.Add(radToolBarButton14);
			RadToolBarButton radToolBarButton16 = this.CreatePreviewModeButton("ThumbnailsView");
			RadToolBarButton radToolBarButton17 = radToolBarButton16;
			radToolBarButton17.OuterCssClass += " rtbGroupIn";
			this._toolbar.Items.Add(radToolBarButton16);
		}

		// Token: 0x0600F1A7 RID: 61863 RVA: 0x003702B0 File Offset: 0x0036E4B0
		private void ConfigureToolbarButtons()
		{
			bool flag = this.RenderMode == RenderMode.Classic;
			if (!flag)
			{
				this._toolbar.EnableImageSprites = true;
			}
			string str = "icn";
			foreach (object obj in this._toolbar.Items)
			{
				RadToolBarItem radToolBarItem = (RadToolBarItem)obj;
				bool flag2 = !string.IsNullOrEmpty(radToolBarItem.Text) && radToolBarItem.Text != "&nbsp;";
				string str2;
				if (radToolBarItem is RadToolBarDropDown)
				{
					str2 = (flag2 ? radToolBarItem.Text : string.Empty);
				}
				else
				{
					str2 = radToolBarItem.Value;
				}
				string text = str + str2;
				if (flag)
				{
					RadToolBarItem radToolBarItem2 = radToolBarItem;
					radToolBarItem2.CssClass += (flag2 ? string.Empty : " rtbIconOnly ");
					RadToolBarItem radToolBarItem3 = radToolBarItem;
					radToolBarItem3.CssClass = radToolBarItem3.CssClass + text + " " + radToolBarItem.SpriteCssClass;
				}
				else
				{
					radToolBarItem.ShowText = (flag2 ? ToolBarShowPosition.Toolbar : ToolBarShowPosition.OverFlow);
					radToolBarItem.SpriteCssClass = text + " " + radToolBarItem.SpriteCssClass;
				}
			}
		}

		// Token: 0x0600F1A8 RID: 61864 RVA: 0x003703EC File Offset: 0x0036E5EC
		private void CreateTreeviewControl()
		{
			RadTreeNodeBinding radTreeNodeBinding = new RadTreeNodeBinding();
			radTreeNodeBinding.TextField = "Name";
			radTreeNodeBinding.ValueField = "Path";
			this._tree = new RadTreeView();
			this._tree.ID = "tree";
			this._tree.EnableDragAndDrop = true;
			this._tree.PersistLoadOnDemandNodes = true;
			this._tree.AccessKey = this.AccessKey;
			this._tree.NodeExpand += this.RadTreeView1_NodeExpand;
			this._tree.NodeEdit += this.RadTreeView1_NodeEdit;
			this._tree.NodeDrop += this.RadTreeView1_NodeDrop;
			this._tree.NodeDataBound += RadFileExplorer.RadTreeView1_NodeDataBound;
			this._tree.NodeTemplate = new RadFileExplorer.TreeNodeTemplate();
			this._tree.DataBindings.Add(radTreeNodeBinding);
		}

		// Token: 0x0600F1A9 RID: 61865 RVA: 0x003704D8 File Offset: 0x0036E6D8
		private RadTreeViewContextMenu CreateTreeViewContextMenu()
		{
			this._isTreeMenuCreated = true;
			return new RadTreeViewContextMenu
			{
				Items = 
				{
					RadFileExplorer.CreateContextMenuItem("Delete", true),
					RadFileExplorer.CreateContextMenuItem("Rename", false),
					RadFileExplorer.CreateContextMenuItem("NewFolder", false),
					RadFileExplorer.CreateContextMenuItem("Upload", false),
					RadFileExplorer.CreateContextMenuItem("Copy", false),
					RadFileExplorer.CreateContextMenuItem("Paste", false)
				}
			};
		}

		// Token: 0x0600F1AA RID: 61866 RVA: 0x00370578 File Offset: 0x0036E778
		private void CreateUpdatePanelControls(ControlCollection container)
		{
			this._loadingPanel = new RadAjaxLoadingPanel();
			this._loadingPanel.ID = "ajaxLoadingPanel";
			this._loadingPanel.CssClass = "rfeLoadingPanel";
			this._loadingPanel.EnableViewState = false;
			this._loadingPanel.ZIndex = 2;
			container.Add(this._loadingPanel);
			this._updatePanel = new UpdatePanel();
			this._updatePanel.ID = "ajaxPanel";
			this._updatePanel.UpdateMode = UpdatePanelUpdateMode.Conditional;
			HtmlGenericControl htmlGenericControl = RadFileExplorer.CreateHiddenDiv();
			htmlGenericControl.ID = "pbContainer";
			this._postbackButton = new Button();
			this._postbackButton.ID = "pb";
			this._postbackButton.Text = "pb";
			this._postbackButton.TabIndex = -1;
			this._postbackButton.UseSubmitBehavior = false;
			this._postbackButton.CausesValidation = false;
			this._postbackButton.Click += this.PostbackButtonClick;
			htmlGenericControl.Controls.Add(this._postbackButton);
			this._postbackArgument = new HtmlInputHidden();
			this._postbackArgument.ID = "postbackArgument";
			htmlGenericControl.Controls.Add(this._postbackArgument);
			this._updatePanel.ContentTemplateContainer.Controls.Add(htmlGenericControl);
			this._updatePanel.ContentTemplateContainer.Controls.Add(this._tree);
			this._currentFolderInput = new HtmlInputHidden();
			this._currentFolderInput.ID = "currentFolder";
			this._updatePanel.ContentTemplateContainer.Controls.Add(this._currentFolderInput);
			this._feCommandArgument = new HtmlInputHidden();
			this._feCommandArgument.ID = "commandArgument";
			this._updatePanel.ContentTemplateContainer.Controls.Add(this._feCommandArgument);
		}

		// Token: 0x0600F1AB RID: 61867 RVA: 0x0037074C File Offset: 0x0036E94C
		private void CreateAddressControl(ControlCollection controls)
		{
			this._addressBox = new TextBox();
			this._addressBox.ID = "address";
			this._addressBox.ReadOnly = true;
			this._addressBox.EnableViewState = false;
			this._addressBox.CssClass = "rfeAddressBox radPreventDecorate";
			this._addressBox.Attributes["title"] = "Address";
			controls.Add(this._addressBox);
		}

		// Token: 0x0600F1AC RID: 61868 RVA: 0x003707C4 File Offset: 0x0036E9C4
		private void CreateSplitterControls(ControlCollection controls)
		{
			this._splitter = new RadSplitter();
			this._splitter.ID = "splitter";
			this._splitter.BorderSize = 0;
			this._splitter.ResizeWithParentPane = false;
			this._splitter.EnableViewState = this.EnableViewState;
			controls.Add(this._splitter);
			this.radpaneTree = new RadPane();
			this.radpaneTree.ID = "paneTree";
			this.radpaneTree.Width = this.TreePaneWidth;
			this.radpaneTree.EnableViewState = this.EnableViewState;
			this._splitter.Items.Add(this.radpaneTree);
			this.splitBar = new RadSplitBar();
			this.splitBar.ID = "splitBar";
			this.splitBar.CollapseMode = SplitBarCollapseMode.Forward;
			this.splitBar.EnableViewState = this.EnableViewState;
			this._splitter.Items.Add(this.splitBar);
			this.radPaneGrid = new RadPane();
			this.radPaneGrid.ID = "paneGrid";
			this.radPaneGrid.Scrolling = SplitterPaneScrolling.Y;
			this.radPaneGrid.CssClass = "rfePaneGrid";
			this._splitter.EnableViewState = this.EnableViewState;
			this._splitter.Items.Add(this.radPaneGrid);
			this.radPaneGrid.Controls.Add(this._fileList.Grid);
			this.radPaneGrid.Controls.Add(this._fileList.ListViewContainer);
			this.dropZone = new HtmlGenericControl("div");
			this.dropZone.Attributes["class"] = "rfeDropZone";
			this.radPaneGrid.Controls.Add(this.dropZone);
			this.AddDropUploadInfoPanelToControlsCollection(controls);
			this.radpaneTree.Controls.Add(this._updatePanel);
		}

		// Token: 0x0600F1AD RID: 61869 RVA: 0x003709B4 File Offset: 0x0036EBB4
		private void AddDropUploadInfoPanelToControlsCollection(ControlCollection controls)
		{
			this.dropUploadInfoPanel = new Panel();
			this.dropUploadInfoPanel.ID = "dropUploadInfoPanel";
			this.dropUploadInfoPanel.CssClass = "rfeDropUploadInfoPanel";
			controls.Add(this.dropUploadInfoPanel);
			this.AddCancelUploadButtonToDropUploadInfoPanel();
			this.AddUploadProgressLabelToDropUploadInfoPanel();
		}

		// Token: 0x0600F1AE RID: 61870 RVA: 0x00370A04 File Offset: 0x0036EC04
		private void AddCancelUploadButtonToDropUploadInfoPanel()
		{
			this.cancelUploadButton = new RadButton();
			this.cancelUploadButton.ID = "btnCancelUpload";
			this.cancelUploadButton.CssClass = "rfeCancelUpload";
			this.cancelUploadButton.Text = "Cancel";
			this.cancelUploadButton.CausesValidation = false;
			this.cancelUploadButton.AutoPostBack = false;
			this.dropUploadInfoPanel.Controls.Add(this.cancelUploadButton);
		}

		// Token: 0x0600F1AF RID: 61871 RVA: 0x00370A7C File Offset: 0x0036EC7C
		private void AddUploadProgressLabelToDropUploadInfoPanel()
		{
			this.dropUploadProgressLabel = new Label();
			this.dropUploadProgressLabel.ID = "lblDropUploadProgress";
			this.dropUploadProgressLabel.CssClass = "rfeDropUploadProgress";
			this.dropUploadInfoPanel.Controls.Add(this.dropUploadProgressLabel);
		}

		// Token: 0x0600F1B0 RID: 61872 RVA: 0x00370ACC File Offset: 0x0036ECCC
		protected override void CreateChildControls()
		{
			base.CreateChildControls();
			if (!base.DesignMode)
			{
				this._windowManager = new RadWindowManager();
				this._windowManager.ID = "windowManager";
				this._windowManager.EnableViewState = false;
				this._windowManager.Behaviors = (WindowBehaviors.Close | WindowBehaviors.Move);
				this._windowManager.Modal = true;
				this._windowManager.VisibleStatusbar = false;
				this.Controls.Add(this._windowManager);
				this._gridContextMenu = new RadContextMenu();
				this._gridContextMenu.ID = "gridMenu";
				this._gridContextMenu.EnableViewState = false;
				this._gridContextMenu.Items.Add(RadFileExplorer.CreateContextMenuItem("Open", false));
				this._gridContextMenu.Items.Add(RadFileExplorer.CreateContextMenuItem("Delete", true));
				this._gridContextMenu.Items.Add(RadFileExplorer.CreateContextMenuItem("Rename", true));
				this._gridContextMenu.Items.Add(RadFileExplorer.CreateContextMenuItem("NewFolder", true));
				this._gridContextMenu.Items.Add(RadFileExplorer.CreateContextMenuItem("Upload", false));
				this._gridContextMenu.Items.Add(RadFileExplorer.CreateContextMenuItem("Copy", false));
				this._gridContextMenu.Items.Add(RadFileExplorer.CreateContextMenuItem("Paste", false));
				this.Controls.Add(this._gridContextMenu);
			}
			else
			{
				this.Controls.Add(new LiteralControl("RadFileExplorer"));
			}
			this.CreateToolbarControl(this.Controls);
			this.CreateAddressControl(this.Controls);
			this.CreateTreeviewControl();
			this._fileList.CreateControls();
			this.CreateTooltipControl();
			if (!base.DesignMode)
			{
				this.CreateUpdatePanelControls(this.Controls);
				this.CreateSplitterControls(this.Controls);
				this.CreateUploadControls(this.Controls);
			}
			this.UpdateHeight();
			this.UpdateWidth();
		}

		// Token: 0x0600F1B1 RID: 61873 RVA: 0x00370CBC File Offset: 0x0036EEBC
		private void CreateTooltipControl()
		{
			this._tooltip = new RadToolTip
			{
				ID = "tooltip"
			};
			this.Controls.Add(this._tooltip);
		}

		// Token: 0x0600F1B2 RID: 61874 RVA: 0x00370CF2 File Offset: 0x0036EEF2
		private static void RadTreeView1_NodeDataBound(object sender, RadTreeNodeEventArgs e)
		{
			RadFileExplorer.SerializeFileBrowserItem((FileBrowserItem)e.Node.DataItem, e.Node.Attributes);
		}

		// Token: 0x0600F1B3 RID: 61875 RVA: 0x00370D14 File Offset: 0x0036EF14
		private static string SanitizeAttributeValue(string attrValue, bool newLinesOnly)
		{
			if (string.IsNullOrEmpty(attrValue))
			{
				return string.Empty;
			}
			string text = attrValue.Replace("\n", " ").Replace("\r", string.Empty);
			if (!newLinesOnly)
			{
				text = text.Replace("\\", "\\\\").Replace("\"", "\\\"");
			}
			return text;
		}

		// Token: 0x0600F1B4 RID: 61876 RVA: 0x00370D74 File Offset: 0x0036EF74
		private static void SerializeFileBrowserItem(FileBrowserItem dataItem, System.Web.UI.AttributeCollection attributes)
		{
			attributes.Add("Name", RadFileExplorer.SanitizeAttributeValue(dataItem.Name, true));
			attributes.Add("Path", RadFileExplorer.SanitizeAttributeValue(dataItem.Path, true));
			if (!string.IsNullOrEmpty(dataItem.Tag))
			{
				attributes.Add("Tag", RadFileExplorer.SanitizeAttributeValue(dataItem.Tag, true));
			}
			attributes.Add("Permissions", ((int)dataItem.Permissions).ToString());
			DirectoryItem directoryItem = dataItem as DirectoryItem;
			if (directoryItem != null)
			{
				if (!string.IsNullOrEmpty(directoryItem.Location))
				{
					attributes.Add("Location", RadFileExplorer.SanitizeAttributeValue(directoryItem.Location, true));
					return;
				}
			}
			else
			{
				FileItem fileItem = dataItem as FileItem;
				if (fileItem != null)
				{
					attributes.Add("Extension", RadFileExplorer.SanitizeAttributeValue(fileItem.Extension, true));
					attributes.Add("Length", fileItem.Length.ToString());
					attributes.Add("Url", RadFileExplorer.SanitizeAttributeValue(fileItem.Url, true));
				}
			}
		}

		// Token: 0x0600F1B5 RID: 61877 RVA: 0x00370E6C File Offset: 0x0036F06C
		protected void RadTreeView1_NodeExpand(object sender, RadTreeNodeEventArgs e)
		{
			RadTreeNode node = e.Node;
			if (node.Nodes.Count == 0)
			{
				this.PopulateTreeNode(node);
			}
		}

		// Token: 0x0600F1B6 RID: 61878 RVA: 0x00370E94 File Offset: 0x0036F094
		private void PostbackButtonClick(object sender, EventArgs e)
		{
			string value = this._postbackArgument.Value;
			string[] array = value.Split(new string[]
			{
				"***"
			}, StringSplitOptions.RemoveEmptyEntries);
			string text = array[0].Substring(0, array[0].IndexOf("_") + 1);
			array[0] = array[0].Substring(text.Length);
			string a;
			if ((a = text) != null)
			{
				if (a == "CallbackFnCreate_")
				{
					this.CreateFolder(array[0], array[1]);
					return;
				}
				if (a == "CallbackFnDelete_")
				{
					this.DeleteItems(array);
					return;
				}
				if (a == "LoadFolder_")
				{
					this.ExpandNestedFolder(array[0]);
					return;
				}
				if (a == "CallbackFnRenameItem_")
				{
					this.RenameItemHandlingFileExtension(array[0], array[1]);
					return;
				}
				if (a == "GridDrag_")
				{
					this.GridDropItems(array);
					return;
				}
				if (!(a == "CallbackFnCopy_"))
				{
					return;
				}
				this.CopyItems(array);
			}
		}

		// Token: 0x0600F1B7 RID: 61879 RVA: 0x00370F88 File Offset: 0x0036F188
		private void ExplorerAlert(string message)
		{
			string @string = this.Localization.GetString(message);
			if (@string.Length > 0)
			{
				message = @string;
			}
			message = message.Replace("\\", "\\\\").Replace("\"", "\\\"").Replace("\\\\r", "\\r").Replace("\\\\n", "\\n");
			if (base.ScriptManager != null)
			{
				ScriptManager.RegisterStartupScript(this._updatePanel, this._updatePanel.GetType(), "ExplorerAlert", string.Format("alert(\"{0}\");", message), true);
			}
		}

		// Token: 0x0600F1B8 RID: 61880 RVA: 0x0037101C File Offset: 0x0036F21C
		private void CopyItems(string[] arguments)
		{
			if (arguments.Length < 2)
			{
				return;
			}
			string text = arguments[0];
			if (!this.ContentProvider.CheckWritePermissions(text))
			{
				this.ExplorerAlert("MessageCannotWriteToFolder");
				return;
			}
			for (int i = 1; i < arguments.Length; i++)
			{
				bool flag = arguments[i][0] == 'D';
				string text2 = arguments[i].Substring(1);
				string folderPath = flag ? text2 : text2.Substring(0, text2.LastIndexOf(this.PathSeparator));
				if (!this.ContentProvider.CheckReadPermissions(folderPath))
				{
					this.ExplorerAlert("NonExistingFolder");
					return;
				}
				string text3 = this.AppendTrailingPathSeparator(text) + text2.Substring(text2.LastIndexOf(this.PathSeparator) + 1);
				string text4 = string.Empty;
				RadFileExplorerEventArgs radFileExplorerEventArgs = new RadFileExplorerEventArgs(flag ? "CopyDirectory" : "CopyFile", text2, text3);
				this.OnItemCommand(radFileExplorerEventArgs);
				if (radFileExplorerEventArgs.Cancel)
				{
					return;
				}
				if (flag)
				{
					text4 = this.ContentProvider.CopyDirectory(text2, text3);
				}
				else
				{
					text4 = this.ContentProvider.CopyFile(text2, text3);
				}
				if (text4.Length > 0)
				{
					this.ExplorerAlert(text4);
					return;
				}
			}
			RadTreeNode currNode = this.FindTreeNodeByPath(this._tree, text);
			this.PopulateTreeNode(currNode);
		}

		// Token: 0x0600F1B9 RID: 61881 RVA: 0x00371154 File Offset: 0x0036F354
		private void GridDropItems(string[] arguments)
		{
			if (arguments.Length < 2)
			{
				return;
			}
			string text = this.AppendTrailingPathSeparator(arguments[0]);
			if (!this.ContentProvider.CheckWritePermissions(text))
			{
				this.ExplorerAlert("MessageCannotWriteToFolder");
				return;
			}
			for (int i = 1; i < arguments.Length; i++)
			{
				string text2 = arguments[i].Substring(1).TrimEnd(new char[]
				{
					this.PathSeparator
				});
				string str = text2.Substring(text2.LastIndexOf(this.PathSeparator) + 1);
				this.RenameItem(text2, text + str);
			}
		}

		// Token: 0x0600F1BA RID: 61882 RVA: 0x003711E0 File Offset: 0x0036F3E0
		protected void RadTreeView1_NodeEdit(object sender, RadTreeNodeEditEventArgs e)
		{
			RadTreeNode radTreeNode = e.Node;
			string value = this.RenameTreeNode(radTreeNode, e.Text);
			if (!string.IsNullOrEmpty(value))
			{
				radTreeNode = this.FindTreeNodeByPath(sender as RadTreeView, value);
				if (radTreeNode != null && RadFileExplorer.IsNodeADirectory(radTreeNode))
				{
					this._tree.UnselectAllNodes();
					radTreeNode.Selected = true;
					this._currentFolderInput.Value = radTreeNode.Value;
					this.PopulateTreeNode(radTreeNode);
				}
			}
		}

		// Token: 0x0600F1BB RID: 61883 RVA: 0x00371250 File Offset: 0x0036F450
		protected virtual void RadTreeView1_NodeDrop(object sender, RadTreeNodeDragDropEventArgs e)
		{
			RadTreeView tree = sender as RadTreeView;
			IList<RadTreeNode> draggedNodes = e.DraggedNodes;
			RadTreeNode radTreeNode = e.DestDragNode;
			if (radTreeNode == null)
			{
				if (e.HtmlElementID != this._fileList.Grid.ClientID)
				{
					return;
				}
				radTreeNode = this.FindTreeNodeByPath(tree, this.CurrentFolder);
			}
			bool flag = this.CommandArgument != null && this.CommandArgument.StartsWith("copy");
			foreach (RadTreeNode radTreeNode2 in draggedNodes)
			{
				if (radTreeNode2 == null || (RadFileExplorer.IsParentNode(radTreeNode2, radTreeNode) && RadFileExplorer.IsNodeADirectory(radTreeNode2)))
				{
					return;
				}
				this.CopyOrMoveTreeNode(radTreeNode2, radTreeNode, flag);
			}
			if (!flag && draggedNodes.Count > 0 && radTreeNode != null)
			{
				this.ExpandNestedFolder(radTreeNode.Value);
			}
			this.InitialPath = this.CurrentFolder;
		}

		// Token: 0x0600F1BC RID: 61884 RVA: 0x00371344 File Offset: 0x0036F544
		private void ProcessUploadedFiles()
		{
			string text = string.Empty;
			string text2 = this.AppendTrailingPathSeparator(this.CurrentFolder);
			UploadedFileCollection uploadedFileCollection = (!this.Configuration.EnableAsyncUpload) ? this._upload.UploadedFiles : this._asyncUpload.UploadedFiles;
			if (this.Upload != null)
			{
				foreach (object obj in this.Upload.InvalidFiles)
				{
					UploadedFile uploadedFile = (UploadedFile)obj;
					text += this.CheckFileValidity(uploadedFile, text2);
				}
			}
			foreach (object obj2 in uploadedFileCollection)
			{
				UploadedFile uploadedFile2 = (UploadedFile)obj2;
				string text3 = string.Empty;
				string text4 = string.Empty;
				string text5 = this.CheckFileValidity(uploadedFile2, text2);
				if (!string.IsNullOrEmpty(text5))
				{
					text += text5;
				}
				else
				{
					text4 = uploadedFile2.GetName();
					string text6 = text2 + text4;
					if (this.ContentProvider.ResolveDirectory(text2) == null)
					{
						text += "NonExistingFolder";
					}
					else
					{
						RadFileExplorerEventArgs radFileExplorerEventArgs = new RadFileExplorerEventArgs("UploadFile", text6, "");
						this.OnItemCommand(radFileExplorerEventArgs);
						if (radFileExplorerEventArgs.Cancel)
						{
							return;
						}
						if (text6 != radFileExplorerEventArgs.Path)
						{
							text6 = radFileExplorerEventArgs.Path;
							if (text6.IndexOf(this.PathSeparator) != -1)
							{
								text2 = text6.Substring(0, text6.LastIndexOf(this.PathSeparator) + 1);
								text4 = text6.Remove(0, text2.Length);
							}
						}
						using (Stream file = this.ContentProvider.GetFile(text6))
						{
							if (file != null && !this.ShouldOverwriteExistingFiles())
							{
								string text7 = text;
								text = string.Concat(new string[]
								{
									text7,
									uploadedFile2.GetName(),
									" :  ",
									this.Localization.GetString("FileExists"),
									"\\r\\n"
								});
								continue;
							}
						}
						text3 = this.ContentProvider.StoreFile(uploadedFile2, text2, text4, new string[0]);
						this.InitialPath = text3;
						this._selectedFile = ((!string.IsNullOrEmpty(text3)) ? text3.Substring(text3.LastIndexOf(this.PathSeparator) + 1) : text4);
					}
				}
			}
			if (text.Length > 0)
			{
				this.ExplorerAlert(text);
				return;
			}
			RadTreeNode currNode = this.FindTreeNodeByPath(this._tree, this.CurrentFolder);
			this.PopulateTreeNode(currNode);
		}

		// Token: 0x0600F1BD RID: 61885 RVA: 0x00371634 File Offset: 0x0036F834
		private bool ShouldOverwriteExistingFiles()
		{
			bool flag = this.chkOverwrite.Checked;
			if (this.Context != null && this.Context.Request != null && this.Context.Request.Params != null)
			{
				flag = !string.IsNullOrEmpty(this.Context.Request.Params[this.chkOverwrite.UniqueID]);
			}
			if (this.OverwriteExistingFiles == flag)
			{
				return this.OverwriteExistingFiles;
			}
			return flag;
		}

		// Token: 0x0600F1BE RID: 61886 RVA: 0x003716B0 File Offset: 0x0036F8B0
		private string CheckFileValidity(UploadedFile uploadedFile, string uploadFolder)
		{
			string text = string.Empty;
			if (string.IsNullOrEmpty(uploadedFile.FileName))
			{
				text = "NoUploadedFile";
			}
			else if (!this.IsValidExtension(uploadedFile.GetExtension()))
			{
				text = "InvalidFileExtension";
			}
			else if (uploadedFile.ContentLength > (long)this.Configuration.MaxUploadFileSize)
			{
				text = "InvalidFileSize";
			}
			else if (!this.ContentProvider.CheckWritePermissions(uploadFolder))
			{
				text = "MessageCannotWriteToFolder";
			}
			if (text.Length > 0)
			{
				text = uploadedFile.FileName.Replace("\\r", "\\ r").Replace("\\n", "\\ n") + " : " + this.Localization.GetString(text) + "\\r\\n";
			}
			return text;
		}

		// Token: 0x0600F1BF RID: 61887 RVA: 0x00371768 File Offset: 0x0036F968
		private bool IsValidExtension(string extension)
		{
			if (Array.IndexOf<string>(this.Configuration.SearchPatterns, "*.*") >= 0)
			{
				return true;
			}
			foreach (string text in this.Configuration.SearchPatterns)
			{
				if (text.Equals("*" + extension, StringComparison.OrdinalIgnoreCase))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600F1C0 RID: 61888 RVA: 0x003717C8 File Offset: 0x0036F9C8
		private string AppendTrailingPathSeparator(string path)
		{
			if (path == null)
			{
				return null;
			}
			int length = path.Length;
			if (length != 0 && path[length - 1] != this.PathSeparator)
			{
				path += this.PathSeparator;
			}
			return path;
		}

		// Token: 0x0600F1C1 RID: 61889 RVA: 0x0037180C File Offset: 0x0036FA0C
		protected virtual void InitContentProvider()
		{
			string text = (!string.IsNullOrEmpty(this.InitialPath)) ? this.InitialPath : "/";
			if (this.Configuration.ViewPaths.Length >= 1 && text == "/")
			{
				text = this.Configuration.ViewPaths[0];
			}
			this.InitContentProvider(text);
		}

		// Token: 0x0600F1C2 RID: 61890 RVA: 0x00371868 File Offset: 0x0036FA68
		protected virtual void InitContentProvider(string selectedUrl)
		{
			this._contentProvider = (FileBrowserContentProvider)Activator.CreateInstance(this.Configuration.FileBrowserContentProviderType, new object[]
			{
				this.Context,
				this.Configuration.SearchPatterns,
				this.Configuration.ViewPaths,
				this.Configuration.UploadPaths,
				this.Configuration.DeletePaths,
				selectedUrl,
				selectedUrl
			});
		}

		// Token: 0x170048F0 RID: 18672
		// (get) Token: 0x0600F1C3 RID: 61891 RVA: 0x003718E1 File Offset: 0x0036FAE1
		// (set) Token: 0x0600F1C4 RID: 61892 RVA: 0x003718F7 File Offset: 0x0036FAF7
		protected virtual FileBrowserContentProvider ContentProvider
		{
			get
			{
				if (this._contentProvider == null)
				{
					this.InitContentProvider();
				}
				return this._contentProvider;
			}
			set
			{
				this._contentProvider = value;
			}
		}

		// Token: 0x0600F1C5 RID: 61893 RVA: 0x00371900 File Offset: 0x0036FB00
		protected virtual bool IsMetroTouchSkin()
		{
			string runtimeSkin = SkinRegistrar.GetRuntimeSkin(this);
			return runtimeSkin.EndsWith("MetroTouch");
		}

		// Token: 0x0600F1C6 RID: 61894 RVA: 0x00371920 File Offset: 0x0036FB20
		private void UpdateHeight()
		{
			Unit setHeight = this.Height.IsEmpty ? Unit.Pixel(500) : this.Height;
			this.UpdateHeight(setHeight);
		}

		// Token: 0x0600F1C7 RID: 61895 RVA: 0x00371958 File Offset: 0x0036FB58
		private void UpdateHeight(Unit setHeight)
		{
			if (setHeight.Value < 40.0)
			{
				return;
			}
			base.Height = setHeight;
			bool flag = this.IsMetroTouchSkin();
			if (this._toolbar != null && this._toolbar.Visible && this.ResolvedRenderMode == RenderMode.Classic)
			{
				if (flag)
				{
					this._toolbar.Height = Unit.Pixel(44);
					setHeight = Unit.Pixel((int)(setHeight.Value - 46.0));
				}
				else
				{
					this._toolbar.Height = Unit.Pixel(26);
					setHeight = Unit.Pixel((int)(setHeight.Value - 28.0));
				}
			}
			else if (this._toolbar != null && this._toolbar.Visible && this.ResolvedRenderMode == RenderMode.Lightweight)
			{
				this._toolbar.Height = Unit.Empty;
			}
			if (this._addressBox != null && this._addressBox.Visible && this.ResolvedRenderMode.Equals(RenderMode.Classic))
			{
				if (flag)
				{
					this._addressBox.Height = Unit.Pixel(34);
					setHeight = Unit.Pixel((int)(setHeight.Value - 36.0));
				}
				else
				{
					this._addressBox.Height = Unit.Pixel(16);
					setHeight = Unit.Pixel((int)(setHeight.Value - 18.0));
				}
			}
			else if (this._addressBox != null && this._addressBox.Visible && this.ResolvedRenderMode == RenderMode.Lightweight)
			{
				this._addressBox.Height = Unit.Empty;
			}
			if (!flag)
			{
				setHeight = Unit.Pixel((int)(setHeight.Value - 4.0));
			}
			if (this._splitter != null && this._splitter.Visible)
			{
				this._splitter.Height = setHeight;
				if (this._fileList.Visible)
				{
					this._fileList.Height = setHeight;
					return;
				}
			}
			else
			{
				if (this._fileList.Visible)
				{
					this._fileList.Height = setHeight;
					return;
				}
				if (this._tree != null && this._tree.Visible)
				{
					this._tree.Height = setHeight;
				}
			}
		}

		// Token: 0x0600F1C8 RID: 61896 RVA: 0x00371B7C File Offset: 0x0036FD7C
		private void UpdateWidth()
		{
			Unit setWidth = this.Width.IsEmpty ? Unit.Pixel(710) : this.Width;
			this.UpdateWidth(setWidth);
		}

		// Token: 0x0600F1C9 RID: 61897 RVA: 0x00371BB4 File Offset: 0x0036FDB4
		private void UpdateWidth(Unit setWidth)
		{
			base.Width = setWidth;
			if (this._splitter != null && this._splitter.Visible)
			{
				this._splitter.Width = Unit.Percentage(100.0);
				this.radPaneGrid.Width = Unit.Empty;
			}
		}

		// Token: 0x0600F1CA RID: 61898 RVA: 0x00371C08 File Offset: 0x0036FE08
		private static bool IsParentNode(RadTreeNode parentNode, RadTreeNode nodeToCheck)
		{
			RadTreeNode radTreeNode = nodeToCheck;
			while (radTreeNode != null && radTreeNode != parentNode)
			{
				radTreeNode = radTreeNode.ParentNode;
			}
			return radTreeNode != null;
		}

		// Token: 0x0600F1CB RID: 61899 RVA: 0x00371C30 File Offset: 0x0036FE30
		private bool HasSubFolders(string virtualPath)
		{
			if (string.IsNullOrEmpty(virtualPath))
			{
				return false;
			}
			DirectoryItem directoryItem = this.ContentProvider.ResolveRootDirectoryAsTree(virtualPath);
			return directoryItem != null && directoryItem.Directories.Length > 0;
		}

		// Token: 0x0600F1CC RID: 61900 RVA: 0x00371C64 File Offset: 0x0036FE64
		protected void BindExplorer()
		{
			this.ContentProvider.ToString();
			List<DirectoryItem> list = new List<DirectoryItem>();
			foreach (string path in this.Configuration.ViewPaths)
			{
				DirectoryItem directoryItem = this.ContentProvider.ResolveRootDirectoryAsTree(path);
				if (directoryItem != null)
				{
					list.Add(directoryItem);
				}
			}
			this._tree.DataSource = list;
			this._tree.DataBind();
			foreach (object obj in this._tree.Nodes)
			{
				RadTreeNode radTreeNode = (RadTreeNode)obj;
				radTreeNode.ExpandMode = TreeNodeExpandMode.ServerSideCallBack;
			}
			if (this._tree.Nodes.Count > 0)
			{
				if (!string.IsNullOrEmpty(this.InitialPath))
				{
					this.ExpandNestedFolder(this.InitialPath);
				}
				if (string.IsNullOrEmpty(this.selectedTreeViewNodeValue))
				{
					if (this._tree.SelectedNode == null)
					{
						this._tree.Nodes[0].Selected = true;
					}
					this._currentFolderInput.Value = (RadFileExplorer.IsNodeADirectory(this._tree.SelectedNode) ? this._tree.SelectedNode.Value : this._tree.SelectedNode.ParentNode.Value);
					this.PopulateTreeNode(this._tree.SelectedNode);
				}
			}
		}

		// Token: 0x0600F1CD RID: 61901 RVA: 0x00371DE4 File Offset: 0x0036FFE4
		private void ApplySkin(Control target, string skin)
		{
			if (!target.Visible)
			{
				return;
			}
			foreach (object obj in target.Controls)
			{
				Control control = (Control)obj;
				ISkinnableControl skinnableControl = control as ISkinnableControl;
				if (skinnableControl != null)
				{
					skinnableControl.EnableEmbeddedBaseStylesheet = this.EnableEmbeddedBaseStylesheet;
					skinnableControl.EnableEmbeddedSkins = this.EnableEmbeddedSkins;
					skinnableControl.EnableAjaxSkinRendering = this.EnableAjaxSkinRendering;
					skinnableControl.Skin = skin;
				}
				this.ApplySkin(control, skin);
			}
		}

		// Token: 0x0600F1CE RID: 61902 RVA: 0x00371E7C File Offset: 0x0037007C
		private void SetRenderModeChildRadControls()
		{
			this.SetControlRenderMode(this._toolbar);
			this.SetControlRenderMode(this._tree);
			this.SetControlRenderMode(this._gridContextMenu);
			this.SetControlRenderMode(this._loadingPanel);
			this.SetControlRenderMode(this._splitter);
			this.SetControlRenderMode(this._windowManager);
			this.SetControlRenderMode(this.splitBar);
			this.SetControlRenderMode(this.radPaneGrid);
			this.SetControlRenderMode(this.radpaneTree);
			this.SetControlRenderMode(this._tooltip);
			this.SetControlRenderMode(this._upload);
			this.SetControlRenderMode(this._asyncUpload);
			this.SetControlRenderMode(this.uploadButton);
			this.SetControlRenderMode(this.cancelUploadButton);
			if (this._fileList != null)
			{
				this.SetControlRenderMode(this._fileList.Grid);
				this.SetControlRenderMode(this._fileList.ListView);
				this.SetControlRenderMode(this._fileList.Slider);
			}
		}

		// Token: 0x0600F1CF RID: 61903 RVA: 0x00371F6C File Offset: 0x0037016C
		private void SetControlRenderMode(ISkinnableControl control)
		{
			if (control != null)
			{
				control.RenderMode = this.ResolvedRenderMode;
			}
		}

		// Token: 0x0600F1D0 RID: 61904 RVA: 0x00371F7D File Offset: 0x0037017D
		private void EnableDisableControls()
		{
			this._fileList.Enabled = base.IsEnabled;
		}

		// Token: 0x0600F1D1 RID: 61905 RVA: 0x00371F90 File Offset: 0x00370190
		protected void PopulateTreeNode(RadTreeNode currNode)
		{
			if (currNode == null)
			{
				return;
			}
			if (!RadFileExplorer.IsNodeADirectory(currNode))
			{
				this.PopulateTreeNode(currNode.ParentNode);
				return;
			}
			this.UpdateTreeNodeContainer(currNode);
			currNode.Nodes.Clear();
			currNode.ExpandMode = TreeNodeExpandMode.ClientSide;
			currNode.Expanded = true;
			bool flag = this.ExplorerMode == FileExplorerMode.FileTree;
			int num = 0;
			List<FileBrowserItem> explorerData = this.GetExplorerData(currNode.Value, "", 0, int.MaxValue, flag, "tree", out num);
			foreach (FileBrowserItem fileBrowserItem in explorerData)
			{
				RadTreeNode radTreeNode = RadFileExplorer.CreateTreeNode(fileBrowserItem);
				if (fileBrowserItem is DirectoryItem && (flag || this.HasSubFolders(fileBrowserItem.Path)))
				{
					radTreeNode.ExpandMode = TreeNodeExpandMode.ServerSideCallBack;
				}
				currNode.Nodes.Add(radTreeNode);
			}
			currNode.DataBind();
		}

		// Token: 0x0600F1D2 RID: 61906 RVA: 0x0037207C File Offset: 0x0037027C
		private void UpdateTreeNodeContainer(RadTreeNode node)
		{
			if (node.ItemContainer == null)
			{
				node.SetItemContainer(this._tree);
			}
		}

		// Token: 0x0600F1D3 RID: 61907 RVA: 0x00372094 File Offset: 0x00370294
		private string CopyOrMoveTreeNode(RadTreeNode sourceNode, RadTreeNode destNode, bool isCopying)
		{
			if (sourceNode == null)
			{
				return string.Empty;
			}
			RadTreeNode parentNode = sourceNode.ParentNode;
			string text = isCopying ? "Copy" : "Move";
			bool flag = RadFileExplorer.IsNodeADirectory(sourceNode);
			text += (flag ? "Directory" : "File");
			string text2 = flag ? sourceNode.Value : parentNode.Value;
			string text3 = this.AppendTrailingPathSeparator(RadFileExplorer.IsNodeADirectory(destNode) ? destNode.Value : destNode.ParentNode.Value);
			if (!this.ContentProvider.CheckWritePermissions(text3))
			{
				if (text.EndsWith("Directory"))
				{
					this.ExplorerAlert("NoPermissionsToCreateFolder");
				}
				else
				{
					this.ExplorerAlert("MessageCannotWriteToFolder");
				}
				return string.Empty;
			}
			if (!isCopying && (!this.ContentProvider.CheckDeletePermissions(text2) || parentNode == null))
			{
				this.ExplorerAlert("NoPermissionsToDeleteFile");
				return string.Empty;
			}
			string text4 = this.AppendTrailingPathSeparator(text2);
			if (text.EndsWith("File"))
			{
				text4 += sourceNode.Text;
			}
			string text5 = this.AppendTrailingPathSeparator(text3) + sourceNode.Text;
			RadFileExplorerEventArgs radFileExplorerEventArgs = new RadFileExplorerEventArgs(text, text4, text5);
			this.OnItemCommand(radFileExplorerEventArgs);
			if (radFileExplorerEventArgs.Cancel)
			{
				return string.Empty;
			}
			text4 = radFileExplorerEventArgs.Path;
			text5 = radFileExplorerEventArgs.NewPath;
			string text6 = string.Empty;
			string a;
			if ((a = text) != null)
			{
				if (!(a == "CopyDirectory"))
				{
					if (!(a == "CopyFile"))
					{
						if (!(a == "MoveDirectory"))
						{
							if (a == "MoveFile")
							{
								text6 = this.ContentProvider.MoveFile(text4, text5);
							}
						}
						else
						{
							text6 = this.ContentProvider.MoveDirectory(text4, text5);
						}
					}
					else
					{
						text6 = this.ContentProvider.CopyFile(text4, text5);
					}
				}
				else
				{
					text6 = this.ContentProvider.CopyDirectory(text4, text5);
				}
			}
			if (text6.Length > 0)
			{
				this.ExplorerAlert(text6);
				return string.Empty;
			}
			if (!isCopying)
			{
				this.PopulateTreeNode(parentNode);
			}
			RadTreeNode radTreeNode = this.FindTreeNodeByPath(this._tree, text3);
			if (radTreeNode != null && radTreeNode.Value != parentNode.Value)
			{
				this.PopulateTreeNode(radTreeNode);
			}
			return text5;
		}

		// Token: 0x0600F1D4 RID: 61908 RVA: 0x003722CC File Offset: 0x003704CC
		private string RenameTreeNode(RadTreeNode node, string newName)
		{
			RadTreeNode parentNode = node.ParentNode;
			if (parentNode == null)
			{
				return string.Empty;
			}
			newName = newName.TrimEnd(new char[]
			{
				this.PathSeparator
			});
			bool flag = RadFileExplorer.IsNodeADirectory(node);
			string text = flag ? node.Value : (this.AppendTrailingPathSeparator(parentNode.Value) + node.Text);
			newName = this.GetNewFileNameByAllowExtensionRename(text, newName);
			string text2 = (newName.IndexOf(this.PathSeparator) != -1) ? newName : (this.AppendTrailingPathSeparator(parentNode.Value) + newName);
			RadFileExplorerEventArgs radFileExplorerEventArgs;
			if (flag)
			{
				radFileExplorerEventArgs = new RadFileExplorerEventArgs("MoveDirectory", text, text2);
			}
			else
			{
				radFileExplorerEventArgs = new RadFileExplorerEventArgs("MoveFile", text, text2);
			}
			this.OnItemCommand(radFileExplorerEventArgs);
			if (radFileExplorerEventArgs.Cancel)
			{
				return string.Empty;
			}
			text = radFileExplorerEventArgs.Path;
			text2 = radFileExplorerEventArgs.NewPath;
			string folderPath = text.Substring(0, text.LastIndexOf(this.PathSeparator));
			string text3 = text2.Substring(0, text2.LastIndexOf(this.PathSeparator));
			if (!this.ContentProvider.CheckDeletePermissions(folderPath))
			{
				if (flag)
				{
					this.ExplorerAlert("NoPermissionsToDeleteFolder");
				}
				else
				{
					this.ExplorerAlert("NoPermissionsToDeleteFile");
				}
				return string.Empty;
			}
			if (!this.ContentProvider.CheckWritePermissions(text3))
			{
				if (flag)
				{
					this.ExplorerAlert("NoPermissionsToCreateFolder");
				}
				else
				{
					this.ExplorerAlert("MessageCannotWriteToFolder");
				}
				return string.Empty;
			}
			string text4;
			if (flag)
			{
				text4 = this.ContentProvider.MoveDirectory(text, text2);
			}
			else
			{
				text4 = this.ContentProvider.MoveFile(text, text2);
			}
			if (text4.Length > 0)
			{
				this.ExplorerAlert(text4);
				return string.Empty;
			}
			this.PopulateTreeNode(parentNode);
			RadTreeNode radTreeNode = this.FindTreeNodeByPath(this._tree, text3);
			if (radTreeNode != null && radTreeNode.Value != parentNode.Value)
			{
				this.PopulateTreeNode(radTreeNode);
			}
			this.InitialPath = this.CurrentFolder;
			return text2;
		}

		// Token: 0x0600F1D5 RID: 61909 RVA: 0x003724B0 File Offset: 0x003706B0
		private static RadTreeNode CreateTreeNode(FileBrowserItem dataItem)
		{
			RadTreeNode radTreeNode = new RadTreeNode();
			radTreeNode.Text = dataItem.Name;
			radTreeNode.Value = dataItem.Path;
			RadFileExplorer.SerializeFileBrowserItem(dataItem, radTreeNode.Attributes);
			return radTreeNode;
		}

		// Token: 0x0600F1D6 RID: 61910 RVA: 0x003724E8 File Offset: 0x003706E8
		private RadTreeNode FindTreeNodeByPath(RadTreeView tree, string value)
		{
			if (string.IsNullOrEmpty(value))
			{
				return null;
			}
			RadTreeNode radTreeNode = tree.FindNodeByValue(value, true);
			if (radTreeNode == null)
			{
				if (!value.EndsWith(this.PathSeparator.ToString()))
				{
					radTreeNode = tree.FindNodeByValue(value + this.PathSeparator, true);
				}
				else
				{
					radTreeNode = tree.FindNodeByValue(value.TrimEnd(new char[]
					{
						this.PathSeparator
					}), true);
				}
			}
			return radTreeNode;
		}

		// Token: 0x0600F1D7 RID: 61911 RVA: 0x0037255C File Offset: 0x0037075C
		private static RadMenuItem CreateContextMenuItem(string value, bool postBack)
		{
			return new RadMenuItem
			{
				Value = value,
				Text = value,
				PostBack = postBack,
				EnableViewState = false,
				SelectedCssClass = "rfeNoClass"
			};
		}

		// Token: 0x0600F1D8 RID: 61912 RVA: 0x00372598 File Offset: 0x00370798
		private RadToolBarButton CreateToolBarButton(string value, bool hasText)
		{
			return new RadToolBarButton
			{
				Value = value,
				Text = (hasText ? value : "&nbsp;"),
				ToolTip = value,
				Enabled = false,
				EnableViewState = false
			};
		}

		// Token: 0x0600F1D9 RID: 61913 RVA: 0x003725DC File Offset: 0x003707DC
		private RadToolBarButton CreatePreviewModeButton(string value)
		{
			RadToolBarButton radToolBarButton = this.CreateToolBarButton(value, false);
			radToolBarButton.Group = "previewModes";
			radToolBarButton.CheckOnClick = true;
			return radToolBarButton;
		}

		// Token: 0x0600F1DA RID: 61914 RVA: 0x00372608 File Offset: 0x00370808
		private static int NameComparer(FileBrowserItem item1, FileBrowserItem item2)
		{
			bool flag = item2 is DirectoryItem;
			if (item1 is DirectoryItem)
			{
				if (flag)
				{
					return StringComparer.OrdinalIgnoreCase.Compare(item1.Name, item2.Name);
				}
				return -1;
			}
			else
			{
				if (flag)
				{
					return 1;
				}
				return StringComparer.OrdinalIgnoreCase.Compare(item1.Name, item2.Name);
			}
		}

		// Token: 0x0600F1DB RID: 61915 RVA: 0x00372660 File Offset: 0x00370860
		private static int SizeComparer(FileBrowserItem item1, FileBrowserItem item2)
		{
			bool flag = item2 is DirectoryItem;
			if (item1 is DirectoryItem)
			{
				if (flag)
				{
					return StringComparer.OrdinalIgnoreCase.Compare(item1.Name, item2.Name);
				}
				return -1;
			}
			else
			{
				if (flag)
				{
					return 1;
				}
				long num = (item1 as FileItem).Length - (item2 as FileItem).Length;
				if (num == 0L)
				{
					return StringComparer.OrdinalIgnoreCase.Compare(item1.Name, item2.Name);
				}
				if (num <= 0L)
				{
					return -1;
				}
				return 1;
			}
		}

		// Token: 0x0600F1DC RID: 61916 RVA: 0x003726DC File Offset: 0x003708DC
		private void CreateFolder(string currNodeValue, string newDirName)
		{
			if (string.IsNullOrEmpty(newDirName))
			{
				return;
			}
			if (this.EnableCreateNewFolder && this.ContentProvider.CanCreateDirectory)
			{
				string text = this.AppendTrailingPathSeparator(currNodeValue).Replace("\\/", "\\");
				if (!this.ContentProvider.CheckWritePermissions(currNodeValue))
				{
					this.ExplorerAlert("NoPermissionsToCreateFolder");
					return;
				}
				RadFileExplorerEventArgs radFileExplorerEventArgs = new RadFileExplorerEventArgs("CreateDirectory", text, newDirName);
				this.OnItemCommand(radFileExplorerEventArgs);
				if (radFileExplorerEventArgs.Cancel)
				{
					return;
				}
				text = radFileExplorerEventArgs.Path;
				newDirName = radFileExplorerEventArgs.NewPath;
				string text2 = this.ContentProvider.CreateDirectory(text, newDirName);
				if (text2.Length > 0)
				{
					this.ExplorerAlert(text2);
					return;
				}
				RadTreeNode radTreeNode = this.FindTreeNodeByPath(this._tree, currNodeValue);
				this.PopulateTreeNode(radTreeNode);
				this._tree.UnselectAllNodes();
				radTreeNode.Selected = true;
				this._currentFolderInput.Value = radTreeNode.Value;
			}
		}

		// Token: 0x0600F1DD RID: 61917 RVA: 0x003727C4 File Offset: 0x003709C4
		private void DeleteItems(string[] arguments)
		{
			foreach (string text in arguments)
			{
				string text2 = text;
				RadTreeNode radTreeNode = this.FindTreeNodeByPath(this._tree, text2);
				bool flag = false;
				if (radTreeNode != null)
				{
					flag = RadFileExplorer.IsNodeADirectory(radTreeNode);
				}
				if (radTreeNode != null && radTreeNode.ParentNode != null && flag)
				{
					RadTreeNode parentNode = radTreeNode.ParentNode;
					if (!this.ContentProvider.CheckDeletePermissions(parentNode.Value))
					{
						this.ExplorerAlert("NoPermissionsToDeleteFolder");
						break;
					}
					RadFileExplorerEventArgs radFileExplorerEventArgs = new RadFileExplorerEventArgs("DeleteDirectory", text2, "");
					this.OnItemCommand(radFileExplorerEventArgs);
					if (radFileExplorerEventArgs.Cancel)
					{
						return;
					}
					text2 = radFileExplorerEventArgs.Path;
					string text3 = this.ContentProvider.DeleteDirectory(text2);
					if (text3.Length > 0)
					{
						this.ExplorerAlert(text3);
						break;
					}
					if (this._currentFolderInput.Value.StartsWith(radTreeNode.Value))
					{
						this._tree.UnselectAllNodes();
						parentNode.Selected = true;
						this._currentFolderInput.Value = parentNode.Value;
					}
					this.PopulateTreeNode(parentNode);
					parentNode.Expanded = true;
				}
				else if (!flag)
				{
					string path = this.ContentProvider.GetPath(text2);
					if (!this.ContentProvider.CheckDeletePermissions(path))
					{
						this.ExplorerAlert("NoPermissionsToDeleteFile");
						break;
					}
					RadFileExplorerEventArgs radFileExplorerEventArgs2 = new RadFileExplorerEventArgs("DeleteFile", text2, "");
					this.OnItemCommand(radFileExplorerEventArgs2);
					if (radFileExplorerEventArgs2.Cancel)
					{
						return;
					}
					text2 = radFileExplorerEventArgs2.Path;
					string text4 = this.ContentProvider.DeleteFile(text2);
					if (text4.Length > 0)
					{
						this.ExplorerAlert(text4);
						break;
					}
					RadTreeNode currNode = this.FindTreeNodeByPath(this._tree, path);
					this.PopulateTreeNode(currNode);
				}
			}
		}

		// Token: 0x0600F1DE RID: 61918 RVA: 0x0037298B File Offset: 0x00370B8B
		private void RenameItemHandlingFileExtension(string path, string newName)
		{
			newName = this.GetNewFileNameByAllowExtensionRename(path, newName);
			this.RenameItem(path, newName);
		}

		// Token: 0x0600F1DF RID: 61919 RVA: 0x003729A0 File Offset: 0x00370BA0
		private void RenameItem(string path, string newName)
		{
			RadTreeNode radTreeNode = this.FindTreeNodeByPath(this._tree, path);
			if (radTreeNode != null)
			{
				if (radTreeNode.ParentNode != null)
				{
					this.RenameTreeNode(radTreeNode, newName);
					return;
				}
			}
			else
			{
				string text = newName;
				string path2 = this.ContentProvider.GetPath(path);
				if (text.IndexOf(this.PathSeparator) == -1)
				{
					text = path2 + newName;
				}
				string fileName = this.ContentProvider.GetFileName(path);
				int startIndex = fileName.LastIndexOf('.');
				if (!this.ContentProvider.CheckDeletePermissions(path2) || !this.IsValidExtension(fileName.Substring(startIndex)))
				{
					this.ExplorerAlert("NoPermissionsToDeleteFile");
					return;
				}
				if (!this.ContentProvider.CheckWritePermissions(this.ContentProvider.GetPath(text)))
				{
					this.ExplorerAlert("NoPermissionsToCreateFolder");
					return;
				}
				int num = text.LastIndexOf('.');
				if (num != -1 && !this.IsValidExtension(text.Substring(num)))
				{
					this.ExplorerAlert("NoPermissionsToCreateFolder");
					return;
				}
				RadFileExplorerEventArgs radFileExplorerEventArgs = new RadFileExplorerEventArgs("MoveFile", path, text);
				this.OnItemCommand(radFileExplorerEventArgs);
				if (radFileExplorerEventArgs.Cancel)
				{
					return;
				}
				path = radFileExplorerEventArgs.Path;
				text = radFileExplorerEventArgs.NewPath;
				string text2 = this.ContentProvider.MoveFile(path, text);
				if (text2.Length > 0)
				{
					this.ExplorerAlert(text2);
				}
			}
		}

		// Token: 0x0600F1E0 RID: 61920 RVA: 0x00372ADC File Offset: 0x00370CDC
		private string GetNewFileNameByAllowExtensionRename(string path, string newName)
		{
			if (!this.Configuration.AllowFileExtensionRename)
			{
				FileItem fileItem = this.ContentProvider.GetFileItem(path);
				if (fileItem != null)
				{
					newName += fileItem.Extension;
				}
			}
			return newName;
		}

		// Token: 0x0600F1E1 RID: 61921 RVA: 0x00372B18 File Offset: 0x00370D18
		private void ExpandNestedFolder(string nodeValue)
		{
			nodeValue = this.DecodeNodeValue(nodeValue);
			if (nodeValue.Length > 1 && nodeValue.EndsWith(this.PathSeparator.ToString()))
			{
				nodeValue = nodeValue.Remove(nodeValue.Length - 1, 1);
			}
			if (!this.ContentProvider.CheckReadPermissions(nodeValue))
			{
				this.ExplorerAlert("NonExistingFolder");
				return;
			}
			RadTreeNode radTreeNode = this.FindTreeNodeByPath(this._tree, nodeValue);
			List<string> list = new List<string>();
			string text = nodeValue;
			char pathSeparator = this.PathSeparator;
			while (radTreeNode == null && text.IndexOf(pathSeparator) != -1)
			{
				string item = text.Substring(text.LastIndexOf(pathSeparator) + 1);
				list.Insert(0, item);
				text = text.Substring(0, text.LastIndexOf(pathSeparator));
				radTreeNode = this.FindTreeNodeByPath(this._tree, nodeValue.Substring(0, text.Length));
			}
			if (radTreeNode == null && this._tree.Nodes.Count == 1 && this._tree.Nodes[0].Value.Length == 1 && this._tree.Nodes[0].Value[0] == this.PathSeparator)
			{
				radTreeNode = this._tree.Nodes[0];
			}
			if (radTreeNode != null)
			{
				this.PopulateTreeNode(radTreeNode);
				for (int i = 0; i < list.Count; i++)
				{
					string str = this.AppendTrailingPathSeparator(radTreeNode.Value);
					string value = str + list[i];
					RadTreeNode radTreeNode2 = this.FindTreeNodeByPath(this._tree, value);
					if (radTreeNode2 == null)
					{
						value = str + list[i];
						radTreeNode2 = this.FindTreeNodeByPath(this._tree, value);
					}
					if (radTreeNode2 == null)
					{
						break;
					}
					radTreeNode = radTreeNode2;
					this.PopulateTreeNode(radTreeNode);
				}
				this._tree.UnselectAllNodes();
				bool flag = !RadFileExplorer.IsNodeADirectory(radTreeNode);
				if (flag)
				{
					radTreeNode = this._tree.FindNodeByValue(radTreeNode.Value);
				}
				radTreeNode.Selected = true;
				this._currentFolderInput.Value = (flag ? radTreeNode.ParentNode.Value : radTreeNode.Value);
			}
		}

		// Token: 0x0600F1E2 RID: 61922 RVA: 0x00372D2B File Offset: 0x00370F2B
		private string DecodeNodeValue(string nodeValue)
		{
			nodeValue = nodeValue.Replace("+", "_-TplaceHolderT-_");
			nodeValue = HttpUtility.UrlDecode(nodeValue);
			nodeValue = nodeValue.Replace("_-TplaceHolderT-_", "+");
			return nodeValue;
		}

		// Token: 0x0600F1E3 RID: 61923 RVA: 0x00372D5C File Offset: 0x00370F5C
		protected virtual List<FileBrowserItem> GetExplorerData(string path, string sortExpression, int startIndex, int maxRowNumber, bool includeFiles, string control, out int itemsCount)
		{
			return this.GetExplorerData(path, sortExpression, startIndex, maxRowNumber, includeFiles, control, out itemsCount, null);
		}

		// Token: 0x0600F1E4 RID: 61924 RVA: 0x00372D7C File Offset: 0x00370F7C
		protected virtual List<FileBrowserItem> GetExplorerData(string path, string sortExpression, int startIndex, int maxRowNumber, bool includeFiles, string control, out int itemsCount, string filterKeyWord)
		{
			string[] array = sortExpression.Split(new char[]
			{
				' '
			});
			string text = (array.Length > 0) ? array[0] : string.Empty;
			string text2 = (array.Length > 1) ? array[1] : string.Empty;
			DirectoryItem directoryItem = new DirectoryItem();
			List<FileBrowserItem> list = new List<FileBrowserItem>();
			if (this.ContentProvider.CheckReadPermissions(path))
			{
				directoryItem = this.ContentProvider.ResolveRootDirectoryAsTree(path);
				if (directoryItem != null)
				{
					if (string.IsNullOrEmpty(filterKeyWord))
					{
						list.AddRange(directoryItem.Directories);
					}
					else
					{
						int num = directoryItem.Directories.Length;
						for (int i = 0; i < num; i++)
						{
							DirectoryItem directoryItem2 = directoryItem.Directories[i];
							if (directoryItem2.Name.IndexOf(filterKeyWord, StringComparison.InvariantCultureIgnoreCase) != -1)
							{
								list.Add(directoryItem2);
							}
						}
					}
				}
				if (includeFiles)
				{
					directoryItem = this.ContentProvider.ResolveDirectory(path);
					if (directoryItem != null)
					{
						if (string.IsNullOrEmpty(filterKeyWord))
						{
							list.AddRange(directoryItem.Files);
						}
						else
						{
							FileItem[] files = directoryItem.Files;
							int num2 = files.Length;
							for (int j = 0; j < num2; j++)
							{
								FileItem fileItem = files[j];
								if (fileItem.Name.IndexOf(filterKeyWord, StringComparison.InvariantCultureIgnoreCase) != -1 || fileItem.Extension.IndexOf(filterKeyWord, StringComparison.InvariantCultureIgnoreCase) != -1)
								{
									list.Add(fileItem);
								}
							}
						}
					}
				}
			}
			bool flag = false;
			string a;
			if ((a = text.ToLowerInvariant()) != null)
			{
				if (!(a == "name"))
				{
					if (a == "size")
					{
						list.Sort(new Comparison<FileBrowserItem>(RadFileExplorer.SizeComparer));
						flag = true;
					}
				}
				else
				{
					list.Sort(new Comparison<FileBrowserItem>(RadFileExplorer.NameComparer));
					flag = true;
				}
			}
			if (flag && text2.IndexOf("DESC", StringComparison.OrdinalIgnoreCase) != -1)
			{
				list.Reverse();
			}
			if (control == "grid" && this.DisplayUpFolderItem)
			{
				bool flag2 = false;
				string text3 = path.TrimEnd(new char[]
				{
					this.PathSeparator
				});
				foreach (string text4 in this.Configuration.ViewPaths)
				{
					string strB = text4.TrimEnd(new char[]
					{
						this.PathSeparator
					});
					if (string.Compare(text3, strB, true) == 0)
					{
						flag2 = true;
						break;
					}
				}
				if (!flag2 && path.LastIndexOf(this.PathSeparator) > 0)
				{
					string text5 = text3.Substring(0, text3.LastIndexOf(this.PathSeparator) + 1);
					if (text3.Length == path.Length)
					{
						text5 = text5.TrimEnd(new char[]
						{
							this.PathSeparator
						});
					}
					PathPermissions permissions = directoryItem.Permissions & ~PathPermissions.Delete;
					list.Insert(0, new DirectoryItem("..", text5, text5, "", permissions, new FileItem[0], new DirectoryItem[0]));
				}
			}
			RadFileExplorerPopulatedEventArgs radFileExplorerPopulatedEventArgs = new RadFileExplorerPopulatedEventArgs(list, text, text2, control, filterKeyWord);
			this.OnExplorerPopulated(radFileExplorerPopulatedEventArgs);
			List<FileBrowserItem> list2 = radFileExplorerPopulatedEventArgs.List;
			itemsCount = list2.Count;
			if (this.AllowPaging)
			{
				if (startIndex + maxRowNumber > list2.Count)
				{
					maxRowNumber = list2.Count - startIndex;
				}
				list2 = list2.GetRange(startIndex, maxRowNumber);
			}
			return list2;
		}

		// Token: 0x0600F1E5 RID: 61925 RVA: 0x003730B3 File Offset: 0x003712B3
		private void ProcessFileListPaging(bool allowPaging)
		{
			this._fileList.AllowPaging = allowPaging;
		}

		// Token: 0x0600F1E6 RID: 61926 RVA: 0x003730C1 File Offset: 0x003712C1
		private void ProcessFileListMultipleSelection(bool value)
		{
			this._fileList.AllowMultipleItemSelect = value;
		}

		// Token: 0x0600F1E7 RID: 61927 RVA: 0x003730CF File Offset: 0x003712CF
		internal static bool IsNodeADirectory(RadTreeNode node)
		{
			return node.Attributes["Length"] == null;
		}

		// Token: 0x170048F1 RID: 18673
		// (get) Token: 0x0600F1E8 RID: 61928 RVA: 0x003730E4 File Offset: 0x003712E4
		CultureInfo ILocalizableControl.Culture
		{
			get
			{
				return this._culture;
			}
		}

		// Token: 0x0600F1E9 RID: 61929 RVA: 0x003730EC File Offset: 0x003712EC
		public string GetCallbackResult()
		{
			if (this._callbackPath == null || this._sortExpression == null)
			{
				return string.Empty;
			}
			if (!this.ContentProvider.CheckReadPermissions(this._callbackPath))
			{
				return string.Empty;
			}
			RadFileExplorer.callbackResponseStruct initialGridData = default(RadFileExplorer.callbackResponseStruct);
			initialGridData.data = this.GetExplorerData(this._callbackPath, this._sortExpression, this._startIndex, this._maxRowNumber, true, "grid", out initialGridData.count, this._filterKeyWord);
			string result = RadFileExplorer.SerializeGridData(initialGridData);
			this._callbackPath = null;
			this._sortExpression = null;
			this._startIndex = 0;
			this._maxRowNumber = 0;
			return result;
		}

		// Token: 0x0600F1EA RID: 61930 RVA: 0x0037318C File Offset: 0x0037138C
		private static Dictionary<string, object> GetCallbackParams(string jsonArgs)
		{
			JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
			return (Dictionary<string, object>)javaScriptSerializer.DeserializeObject(jsonArgs);
		}

		// Token: 0x0600F1EB RID: 61931 RVA: 0x003731B0 File Offset: 0x003713B0
		public void RaiseCallbackEvent(string eventArgument)
		{
			Dictionary<string, object> callbackParams = RadFileExplorer.GetCallbackParams(eventArgument);
			this._callbackPath = (string)callbackParams["path"];
			this._sortExpression = (string)callbackParams["sortExpression"];
			this._filterKeyWord = (callbackParams.ContainsKey("filterKeyWord") ? ((string)callbackParams["filterKeyWord"]) : null);
			this._startIndex = (int)callbackParams["startIndex"];
			this._maxRowNumber = (int)callbackParams["maxRowNumber"];
		}

		// Token: 0x0600F1EC RID: 61932 RVA: 0x00373242 File Offset: 0x00371442
		private void OnPathChanged(object sender, FileExplorerPathsEventArgs args)
		{
			this.pathsChanged = true;
		}

		// Token: 0x0600F1ED RID: 61933 RVA: 0x0037324C File Offset: 0x0037144C
		protected override void LoadViewState(object state)
		{
			object[] array = (object[])state;
			base.LoadViewState(array[0]);
			if (array.Length > 1)
			{
				if (array[1] != null)
				{
					((IStateManager)this.Configuration).LoadViewState(array[1]);
				}
				if (array[2] != null)
				{
					((IStateManager)this.KeyboardShortcuts).LoadViewState(array[2]);
				}
			}
		}

		// Token: 0x0600F1EE RID: 61934 RVA: 0x00373298 File Offset: 0x00371498
		protected override object SaveViewState()
		{
			return new object[]
			{
				base.SaveViewState(),
				RadFileExplorer.SaveState(this._configuration),
				RadFileExplorer.SaveState(this._shortcuts)
			};
		}

		// Token: 0x0600F1EF RID: 61935 RVA: 0x003732D4 File Offset: 0x003714D4
		protected override void TrackViewState()
		{
			base.TrackViewState();
			RadFileExplorer.TrackState(this._configuration);
			RadFileExplorer.TrackState(this._shortcuts);
		}

		// Token: 0x0600F1F0 RID: 61936 RVA: 0x003732F2 File Offset: 0x003714F2
		private static void TrackState(IStateManager obj)
		{
			if (obj != null)
			{
				obj.TrackViewState();
			}
		}

		// Token: 0x0600F1F1 RID: 61937 RVA: 0x003732FD File Offset: 0x003714FD
		private static object SaveState(IStateManager obj)
		{
			if (obj != null)
			{
				return obj.SaveViewState();
			}
			return null;
		}

		// Token: 0x170048F2 RID: 18674
		// (get) Token: 0x0600F1F2 RID: 61938 RVA: 0x0037330A File Offset: 0x0037150A
		protected internal override bool SupportsRenderingMode
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170048F3 RID: 18675
		// (get) Token: 0x0600F1F3 RID: 61939 RVA: 0x0037330D File Offset: 0x0037150D
		protected override string CssClassFormatString
		{
			get
			{
				return "RadFileExplorer RadFileExplorer_{0}";
			}
		}

		// Token: 0x170048F4 RID: 18676
		// (get) Token: 0x0600F1F4 RID: 61940 RVA: 0x00373314 File Offset: 0x00371514
		protected List<string> LocalizationKeys
		{
			get
			{
				return this._localizationKeys;
			}
		}

		// Token: 0x170048F5 RID: 18677
		// (get) Token: 0x0600F1F5 RID: 61941 RVA: 0x0037331C File Offset: 0x0037151C
		protected override HtmlTextWriterTag TagKey
		{
			get
			{
				return HtmlTextWriterTag.Div;
			}
		}

		// Token: 0x170048F6 RID: 18678
		// (get) Token: 0x0600F1F6 RID: 61942 RVA: 0x00373320 File Offset: 0x00371520
		// (set) Token: 0x0600F1F7 RID: 61943 RVA: 0x00373349 File Offset: 0x00371549
		[Description("Gets or sets the current FileExplorerMode (e.g. default, show files in the tree, etc.)")]
		[Category("Behavior")]
		[DefaultValue(FileExplorerMode.Default)]
		public FileExplorerMode ExplorerMode
		{
			get
			{
				object obj = this.ViewState["ExplorerMode"];
				if (obj != null)
				{
					return (FileExplorerMode)obj;
				}
				return FileExplorerMode.Default;
			}
			set
			{
				this.ViewState["ExplorerMode"] = value;
				this._fileList.ViewMode = value;
			}
		}

		// Token: 0x170048F7 RID: 18679
		// (get) Token: 0x0600F1F8 RID: 61944 RVA: 0x0037336D File Offset: 0x0037156D
		// (set) Token: 0x0600F1F9 RID: 61945 RVA: 0x0037338F File Offset: 0x0037158F
		[Description("Gets or sets the current PageSize")]
		[Category("Behavior")]
		[DefaultValue(10)]
		public int PageSize
		{
			get
			{
				return (int)(this.ViewState["GridPageSize"] ?? 10);
			}
			set
			{
				this.ViewState["GridPageSize"] = value;
			}
		}

		// Token: 0x170048F8 RID: 18680
		// (get) Token: 0x0600F1FA RID: 61946 RVA: 0x003733A8 File Offset: 0x003715A8
		// (set) Token: 0x0600F1FB RID: 61947 RVA: 0x003733D1 File Offset: 0x003715D1
		[ClientControlProperty]
		[Category("Behavior")]
		[Description("When set to true, this property will enable paging in the File Explorer's Grid component.")]
		[DefaultValue(false)]
		public bool AllowPaging
		{
			get
			{
				object obj = this.ViewState["AllowPaging"];
				return obj != null && (bool)obj;
			}
			set
			{
				this.ViewState["AllowPaging"] = value;
				if (base.ChildControlsCreated)
				{
					this.ProcessFileListPaging(value);
				}
			}
		}

		// Token: 0x170048F9 RID: 18681
		// (get) Token: 0x0600F1FC RID: 61948 RVA: 0x003733F8 File Offset: 0x003715F8
		// (set) Token: 0x0600F1FD RID: 61949 RVA: 0x00373419 File Offset: 0x00371619
		[Category("Behavior")]
		[Description("When set to true, renders a textbox used to filter files in the grid.")]
		[DefaultValue(false)]
		public bool EnableFilterTextBox
		{
			get
			{
				return (bool)(this.ViewState["EnableFilterTextBox"] ?? false);
			}
			set
			{
				this.ViewState["EnableFilterTextBox"] = value;
			}
		}

		// Token: 0x170048FA RID: 18682
		// (get) Token: 0x0600F1FE RID: 61950 RVA: 0x00373431 File Offset: 0x00371631
		// (set) Token: 0x0600F1FF RID: 61951 RVA: 0x00373452 File Offset: 0x00371652
		[ClientPropertyName("enableFilteringOnEnter")]
		[Category("Behavior")]
		[DefaultValue(false)]
		[Description("When set to true, performs the filtering after the 'Enter' key is pressed.EnableFilterTextBox should be set to true (i.e. filtering enabled) to enable filtering.")]
		[ClientControlProperty]
		public bool EnableFilteringOnEnterPressed
		{
			get
			{
				return (bool)(this.ViewState["EnableFilteringOnEnterPressed"] ?? false);
			}
			set
			{
				this.ViewState["EnableFilteringOnEnterPressed"] = value;
			}
		}

		// Token: 0x170048FB RID: 18683
		// (get) Token: 0x0600F200 RID: 61952 RVA: 0x0037346A File Offset: 0x0037166A
		// (set) Token: 0x0600F201 RID: 61953 RVA: 0x0037348A File Offset: 0x0037168A
		[Category("Behavior")]
		[Description("Gets or sets the label of the Filter TextBox.")]
		[Localizable(true)]
		[DefaultValue("Filter by")]
		public string FilterTextBoxLabel
		{
			get
			{
				return ((string)this.ViewState["FilterTextBoxLabel"]) ?? "Filter by";
			}
			set
			{
				this.ViewState["FilterTextBoxLabel"] = value;
				this._fileList.FilterTextBoxLabel = value;
			}
		}

		// Token: 0x170048FC RID: 18684
		// (get) Token: 0x0600F202 RID: 61954 RVA: 0x003734AC File Offset: 0x003716AC
		// (set) Token: 0x0600F203 RID: 61955 RVA: 0x003734D5 File Offset: 0x003716D5
		[Description("Gets or sets a value indicating whether to allow copying of files/folders")]
		[Category("Behavior")]
		[DefaultValue(false)]
		public bool EnableCopy
		{
			get
			{
				object obj = this.ViewState["EnableCopy"];
				return obj != null && (bool)obj;
			}
			set
			{
				this.ViewState["EnableCopy"] = value;
			}
		}

		// Token: 0x170048FD RID: 18685
		// (get) Token: 0x0600F204 RID: 61956 RVA: 0x003734F0 File Offset: 0x003716F0
		// (set) Token: 0x0600F205 RID: 61957 RVA: 0x0037351C File Offset: 0x0037171C
		[Category("Behavior")]
		[DefaultValue(true)]
		[Description("Gets or sets a value indicating whether to allow creating new folders")]
		public bool EnableCreateNewFolder
		{
			get
			{
				object obj = this.ViewState["EnableCreateNewFolder"];
				return obj == null || (bool)obj;
			}
			set
			{
				this.ViewState["EnableCreateNewFolder"] = value;
				if (base.ChildControlsCreated && this._contentProvider != null)
				{
					this.EnableDisableCommand("NewFolder", value && this.ContentProvider.CanCreateDirectory);
				}
			}
		}

		// Token: 0x170048FE RID: 18686
		// (get) Token: 0x0600F206 RID: 61958 RVA: 0x0037356C File Offset: 0x0037176C
		// (set) Token: 0x0600F207 RID: 61959 RVA: 0x00373595 File Offset: 0x00371795
		[ClientControlProperty]
		[Category("Behavior")]
		[Description("Gets or sets a value indicating whether to allow opening a new window with the file")]
		[DefaultValue(true)]
		public bool EnableOpenFile
		{
			get
			{
				object obj = this.ViewState["EnableOpenFile"];
				return obj == null || (bool)obj;
			}
			set
			{
				this.ViewState["EnableOpenFile"] = value;
				if (base.ChildControlsCreated)
				{
					this.EnableDisableCommand("Open", value);
				}
			}
		}

		// Token: 0x170048FF RID: 18687
		// (get) Token: 0x0600F208 RID: 61960 RVA: 0x003735C1 File Offset: 0x003717C1
		// (set) Token: 0x0600F209 RID: 61961 RVA: 0x003735CE File Offset: 0x003717CE
		[Obsolete("Please use the Configuration-EnableAsyncUpload property instead")]
		[Category("Behavior")]
		[Description("Gets or sets a value indicating whether to use RadAsyncUpload or RadUpload in the Upload window")]
		[DefaultValue(true)]
		public bool EnableAsyncUpload
		{
			get
			{
				return this.Configuration.EnableAsyncUpload;
			}
			set
			{
				this.Configuration.EnableAsyncUpload = value;
			}
		}

		// Token: 0x17004900 RID: 18688
		// (get) Token: 0x0600F20A RID: 61962 RVA: 0x003735DC File Offset: 0x003717DC
		// (set) Token: 0x0600F20B RID: 61963 RVA: 0x003735E4 File Offset: 0x003717E4
		[NotifyParentProperty(true)]
		[DefaultValue(typeof(Unit), "710px")]
		public override Unit Width
		{
			get
			{
				return base.Width;
			}
			set
			{
				this.UpdateWidth(value);
			}
		}

		// Token: 0x17004901 RID: 18689
		// (get) Token: 0x0600F20C RID: 61964 RVA: 0x003735F0 File Offset: 0x003717F0
		// (set) Token: 0x0600F20D RID: 61965 RVA: 0x00373622 File Offset: 0x00371822
		[DefaultValue(typeof(Unit), "222px")]
		[Category("Appearance")]
		public Unit TreePaneWidth
		{
			get
			{
				object obj = this.ViewState["TreePaneWidth"];
				if (obj == null)
				{
					return Unit.Pixel(222);
				}
				return (Unit)obj;
			}
			set
			{
				this.ViewState["TreePaneWidth"] = value;
				if (this.radpaneTree != null)
				{
					this.radpaneTree.Width = value;
				}
			}
		}

		// Token: 0x17004902 RID: 18690
		// (get) Token: 0x0600F20E RID: 61966 RVA: 0x0037364E File Offset: 0x0037184E
		// (set) Token: 0x0600F20F RID: 61967 RVA: 0x00373656 File Offset: 0x00371856
		[NotifyParentProperty(true)]
		[DefaultValue(typeof(Unit), "500px")]
		public override Unit Height
		{
			get
			{
				return base.Height;
			}
			set
			{
				this.UpdateHeight(value);
			}
		}

		// Token: 0x17004903 RID: 18691
		// (get) Token: 0x0600F210 RID: 61968 RVA: 0x0037365F File Offset: 0x0037185F
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public RadToolBar ToolBar
		{
			get
			{
				if (!this._toolbar.Visible)
				{
					return null;
				}
				return this._toolbar;
			}
		}

		// Token: 0x17004904 RID: 18692
		// (get) Token: 0x0600F211 RID: 61969 RVA: 0x00373676 File Offset: 0x00371876
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public RadGrid Grid
		{
			get
			{
				if (!this._fileList.Grid.Visible)
				{
					return null;
				}
				return this._fileList.Grid;
			}
		}

		// Token: 0x17004905 RID: 18693
		// (get) Token: 0x0600F212 RID: 61970 RVA: 0x00373697 File Offset: 0x00371897
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public RadListView ListView
		{
			get
			{
				if (!this._fileList.ListView.Visible)
				{
					return null;
				}
				return this._fileList.ListView;
			}
		}

		// Token: 0x17004906 RID: 18694
		// (get) Token: 0x0600F213 RID: 61971 RVA: 0x003736B8 File Offset: 0x003718B8
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public FileList FileList
		{
			get
			{
				return this._fileList;
			}
		}

		// Token: 0x17004907 RID: 18695
		// (get) Token: 0x0600F214 RID: 61972 RVA: 0x003736C0 File Offset: 0x003718C0
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public virtual RadTreeView TreeView
		{
			get
			{
				if (this._tree != null && !this._isTreeMenuCreated)
				{
					this._tree.ContextMenus.Clear();
					this._tree.ContextMenus.Add(this.CreateTreeViewContextMenu());
				}
				if (!this._tree.Visible)
				{
					return null;
				}
				return this._tree;
			}
		}

		// Token: 0x17004908 RID: 18696
		// (get) Token: 0x0600F215 RID: 61973 RVA: 0x00373718 File Offset: 0x00371918
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public RadContextMenu GridContextMenu
		{
			get
			{
				if (this._gridContextMenu == null || !this._gridContextMenu.Visible)
				{
					return null;
				}
				return this._gridContextMenu;
			}
		}

		// Token: 0x17004909 RID: 18697
		// (get) Token: 0x0600F216 RID: 61974 RVA: 0x00373737 File Offset: 0x00371937
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public RadToolTip TooltipControl
		{
			get
			{
				return this._tooltip;
			}
		}

		// Token: 0x1700490A RID: 18698
		// (get) Token: 0x0600F217 RID: 61975 RVA: 0x0037373F File Offset: 0x0037193F
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public RadUpload Upload
		{
			get
			{
				if (this._upload == null || !this._upload.Visible)
				{
					return null;
				}
				return this._upload;
			}
		}

		// Token: 0x1700490B RID: 18699
		// (get) Token: 0x0600F218 RID: 61976 RVA: 0x0037375E File Offset: 0x0037195E
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public virtual RadAsyncUpload AsyncUpload
		{
			get
			{
				if (this._asyncUpload == null || !this._asyncUpload.Visible)
				{
					return null;
				}
				return this._asyncUpload;
			}
		}

		// Token: 0x1700490C RID: 18700
		// (get) Token: 0x0600F219 RID: 61977 RVA: 0x0037377D File Offset: 0x0037197D
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public RadWindowManager WindowManager
		{
			get
			{
				if (this._windowManager == null || !this._windowManager.Visible)
				{
					return null;
				}
				return this._windowManager;
			}
		}

		// Token: 0x1700490D RID: 18701
		// (get) Token: 0x0600F21A RID: 61978 RVA: 0x0037379C File Offset: 0x0037199C
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public RadSplitter Splitter
		{
			get
			{
				if (this._splitter == null || !this._splitter.Visible)
				{
					return null;
				}
				return this._splitter;
			}
		}

		// Token: 0x1700490E RID: 18702
		// (get) Token: 0x0600F21B RID: 61979 RVA: 0x003737BC File Offset: 0x003719BC
		// (set) Token: 0x0600F21C RID: 61980 RVA: 0x003737E9 File Offset: 0x003719E9
		[Editor("Telerik.Web.Design.Common.FlagEnumUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[Category("Behavior")]
		[DefaultValue(FileExplorerControls.All)]
		public FileExplorerControls VisibleControls
		{
			get
			{
				object obj = this.ViewState["VisibleControls"];
				if (obj == null)
				{
					return FileExplorerControls.All;
				}
				return (FileExplorerControls)obj;
			}
			set
			{
				this.ViewState["VisibleControls"] = value;
			}
		}

		// Token: 0x1700490F RID: 18703
		// (get) Token: 0x0600F21D RID: 61981 RVA: 0x00373804 File Offset: 0x00371A04
		// (set) Token: 0x0600F21E RID: 61982 RVA: 0x00373831 File Offset: 0x00371A31
		[DefaultValue(FileListControls.All)]
		[Editor("Telerik.Web.Design.Common.FlagEnumUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[Category("Behavior")]
		public FileListControls AvailableFileListControls
		{
			get
			{
				object obj = this.ViewState["AvailableFileListControls"];
				if (obj == null)
				{
					return FileListControls.All;
				}
				return (FileListControls)obj;
			}
			set
			{
				this.ViewState["AvailableFileListControls"] = value;
				this._fileList.AvailableFileListControls = value;
			}
		}

		// Token: 0x17004910 RID: 18704
		// (get) Token: 0x0600F21F RID: 61983 RVA: 0x00373855 File Offset: 0x00371A55
		// (set) Token: 0x0600F220 RID: 61984 RVA: 0x0037385D File Offset: 0x00371A5D
		[NotifyParentProperty(true)]
		public override string AccessKey
		{
			get
			{
				return base.AccessKey;
			}
			set
			{
				base.AccessKey = value;
				if (this._tree != null)
				{
					this._tree.AccessKey = value;
				}
			}
		}

		// Token: 0x17004911 RID: 18705
		// (get) Token: 0x0600F221 RID: 61985 RVA: 0x0037387A File Offset: 0x00371A7A
		// (set) Token: 0x0600F222 RID: 61986 RVA: 0x0037389F File Offset: 0x00371A9F
		[DefaultValue("en-US")]
		[MergableProperty(true)]
		[Category("Appearance")]
		[Description("Gets or sets a string containing the localization language for the FileExplorer UI.")]
		public string Language
		{
			get
			{
				return ((string)this.ViewState["Language"]) ?? CultureInfo.CurrentUICulture.Name;
			}
			set
			{
				this.ViewState["Language"] = value;
				this._culture = ((value == null) ? null : CultureInfo.GetCultureInfo(value));
			}
		}

		// Token: 0x17004912 RID: 18706
		// (get) Token: 0x0600F223 RID: 61987 RVA: 0x003738C4 File Offset: 0x00371AC4
		[Browsable(false)]
		[Category("Misc")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public DialogLocalizationStrings Localization
		{
			get
			{
				if (this._localization == null)
				{
					this._fileList.Localization = (this._localization = new DialogsStrings(new LocalizationProvider("RadEditor.Dialogs", this, this.LocalizationPath), "FileExplorer", false));
				}
				return this._localization;
			}
		}

		// Token: 0x17004913 RID: 18707
		// (get) Token: 0x0600F224 RID: 61988 RVA: 0x0037390F File Offset: 0x00371B0F
		// (set) Token: 0x0600F225 RID: 61989 RVA: 0x00373930 File Offset: 0x00371B30
		[Description("Gets or sets a value indicating where the control will look for its .resx localization files.")]
		[DefaultValue("")]
		[Category("Misc")]
		public string LocalizationPath
		{
			get
			{
				return ((string)this.ViewState["LocalizationPath"]) ?? string.Empty;
			}
			set
			{
				string text = value.Replace("\\", "/");
				if (text.Length > 0 && !text.EndsWith("/"))
				{
					text += "/";
				}
				this.ViewState["LocalizationPath"] = text;
			}
		}

		// Token: 0x17004914 RID: 18708
		// (get) Token: 0x0600F226 RID: 61990 RVA: 0x00373983 File Offset: 0x00371B83
		// (set) Token: 0x0600F227 RID: 61991 RVA: 0x0037398B File Offset: 0x00371B8B
		[NotifyParentProperty(true)]
		[DefaultValue("Default")]
		[Category("Appearance")]
		[TypeConverter("Telerik.Web.Design.SkinTypeConverter, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4")]
		[Description("Specifies the skin that will be used by the control")]
		public override string Skin
		{
			get
			{
				return base.Skin;
			}
			set
			{
				base.Skin = value;
				if (base.ChildControlsCreated)
				{
					this.ApplySkin(this, value);
				}
			}
		}

		// Token: 0x17004915 RID: 18709
		// (get) Token: 0x0600F228 RID: 61992 RVA: 0x003739A4 File Offset: 0x00371BA4
		// (set) Token: 0x0600F229 RID: 61993 RVA: 0x003739AC File Offset: 0x00371BAC
		[Category("Appearance")]
		[Description("Specifies the rendering mode of the control")]
		[DefaultValue(RenderMode.Classic)]
		[NotifyParentProperty(true)]
		public override RenderMode RenderMode
		{
			get
			{
				return base.RenderMode;
			}
			set
			{
				base.RenderMode = value;
				if (base.ChildControlsCreated)
				{
					this.SetRenderModeChildRadControls();
				}
			}
		}

		// Token: 0x17004916 RID: 18710
		// (get) Token: 0x0600F22A RID: 61994 RVA: 0x003739C3 File Offset: 0x00371BC3
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[Category("Data")]
		public FileExplorerConfiguration Configuration
		{
			get
			{
				if (this._configuration == null)
				{
					this._configuration = new FileExplorerConfiguration();
					if (base.IsTrackingViewState)
					{
						((IStateManager)this._configuration).TrackViewState();
					}
				}
				return this._configuration;
			}
		}

		// Token: 0x17004917 RID: 18711
		// (get) Token: 0x0600F22B RID: 61995 RVA: 0x003739F1 File Offset: 0x00371BF1
		// (set) Token: 0x0600F22C RID: 61996 RVA: 0x00373A11 File Offset: 0x00371C11
		[Description("Specifies the initial path (folder or file) that will be shown")]
		[Category("Data")]
		[DefaultValue("")]
		public string InitialPath
		{
			get
			{
				return ((string)this.ViewState["InitialPath"]) ?? string.Empty;
			}
			set
			{
				this.ViewState["InitialPath"] = value;
			}
		}

		// Token: 0x17004918 RID: 18712
		// (get) Token: 0x0600F22D RID: 61997 RVA: 0x00373A24 File Offset: 0x00371C24
		// (set) Token: 0x0600F22E RID: 61998 RVA: 0x00373A4D File Offset: 0x00371C4D
		[Category("Behavior")]
		[Description("Gets or sets a value indicating whether to show the up one folder (..) item in the grid if available.")]
		[DefaultValue(false)]
		public bool DisplayUpFolderItem
		{
			get
			{
				object obj = this.ViewState["DisplayUpFolderItem"];
				return obj != null && (bool)obj;
			}
			set
			{
				this.ViewState["DisplayUpFolderItem"] = value;
			}
		}

		// Token: 0x17004919 RID: 18713
		// (get) Token: 0x0600F22F RID: 61999 RVA: 0x00373A68 File Offset: 0x00371C68
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public string CurrentFolder
		{
			get
			{
				string text = this._currentFolderInput.Value;
				if (this.Page != null && this.Page.IsCallback)
				{
					string a = this.Context.Request.Params["__CALLBACKID"];
					if (a == this.UniqueID)
					{
						Dictionary<string, object> callbackParams = RadFileExplorer.GetCallbackParams(this.Context.Request.Params["__CALLBACKPARAM"]);
						text = (string)callbackParams["path"];
					}
				}
				if (string.IsNullOrEmpty(text) && this.Context != null && this.Context.Request != null && this.Context.Request.Params != null)
				{
					text = this.Context.Request.Params[this._currentFolderInput.UniqueID];
				}
				if (!string.IsNullOrEmpty(text) && !this.ContentProvider.CheckReadPermissions(text))
				{
					text = ((this.Configuration.ViewPaths.Length > 0) ? this.Configuration.ViewPaths[0] : string.Empty);
				}
				return text;
			}
		}

		// Token: 0x1700491A RID: 18714
		// (get) Token: 0x0600F230 RID: 62000 RVA: 0x00373B7C File Offset: 0x00371D7C
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[Category("Accessibility")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public FileExplorerShortcut KeyboardShortcuts
		{
			get
			{
				if (this._shortcuts == null)
				{
					this._shortcuts = new FileExplorerShortcut();
				}
				return this._shortcuts;
			}
		}

		// Token: 0x1700491B RID: 18715
		// (get) Token: 0x0600F231 RID: 62001 RVA: 0x00373B97 File Offset: 0x00371D97
		// (set) Token: 0x0600F232 RID: 62002 RVA: 0x00373BB8 File Offset: 0x00371DB8
		[DefaultValue(false)]
		[Category("Behavior")]
		[Description("When set to true, this property will enable overwriting of files that already exist in RadFileExplorer.")]
		[ClientControlProperty]
		[ClientPropertyName("overwriteExistingFiles")]
		public bool OverwriteExistingFiles
		{
			get
			{
				return (bool)(this.ViewState["OverwriteExistingFiles"] ?? false);
			}
			set
			{
				this.ViewState["OverwriteExistingFiles"] = value;
			}
		}

		// Token: 0x1700491C RID: 18716
		// (get) Token: 0x0600F233 RID: 62003 RVA: 0x00373BD0 File Offset: 0x00371DD0
		// (set) Token: 0x0600F234 RID: 62004 RVA: 0x00373BE7 File Offset: 0x00371DE7
		protected string CommandArgument
		{
			get
			{
				if (this._feCommandArgument == null)
				{
					return null;
				}
				return this._feCommandArgument.Value;
			}
			set
			{
				if (this._feCommandArgument != null)
				{
					this._feCommandArgument.Value = value;
				}
			}
		}

		// Token: 0x1700491D RID: 18717
		// (get) Token: 0x0600F235 RID: 62005 RVA: 0x00373BFD File Offset: 0x00371DFD
		// (set) Token: 0x0600F236 RID: 62006 RVA: 0x00373C1D File Offset: 0x00371E1D
		[DefaultValue("")]
		[ClientPropertyName("itemSelected")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[ClientControlEvent]
		[Category("Client-side events")]
		[Description("The name of the javascript function called when the user selects an item in the explorer.")]
		public string OnClientItemSelected
		{
			get
			{
				return ((string)this.ViewState["OnClientItemSelected"]) ?? string.Empty;
			}
			set
			{
				this.ViewState["OnClientItemSelected"] = value;
			}
		}

		// Token: 0x1700491E RID: 18718
		// (get) Token: 0x0600F237 RID: 62007 RVA: 0x00373C30 File Offset: 0x00371E30
		// (set) Token: 0x0600F238 RID: 62008 RVA: 0x00373C50 File Offset: 0x00371E50
		[Description("The name of the javascript function called when a folder is loaded in the grid.")]
		[ClientPropertyName("folderLoaded")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[Category("Client-side events")]
		[DefaultValue("")]
		[ClientControlEvent]
		public string OnClientFolderLoaded
		{
			get
			{
				return ((string)this.ViewState["OnClientFolderLoaded"]) ?? string.Empty;
			}
			set
			{
				this.ViewState["OnClientFolderLoaded"] = value;
			}
		}

		// Token: 0x1700491F RID: 18719
		// (get) Token: 0x0600F239 RID: 62009 RVA: 0x00373C63 File Offset: 0x00371E63
		// (set) Token: 0x0600F23A RID: 62010 RVA: 0x00373C83 File Offset: 0x00371E83
		[Category("Client-side events")]
		[ClientPropertyName("fileOpen")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[Description("The name of the javascript function called when an item is double clicked in the grid.")]
		[DefaultValue("")]
		[ClientControlEvent]
		public string OnClientFileOpen
		{
			get
			{
				return ((string)this.ViewState["OnClientFileOpen"]) ?? string.Empty;
			}
			set
			{
				this.ViewState["OnClientFileOpen"] = value;
			}
		}

		// Token: 0x17004920 RID: 18720
		// (get) Token: 0x0600F23B RID: 62011 RVA: 0x00373C96 File Offset: 0x00371E96
		// (set) Token: 0x0600F23C RID: 62012 RVA: 0x00373CB6 File Offset: 0x00371EB6
		[DefaultValue("")]
		[Description("The name of the javascript function called when the the selected folder in the tree changes.")]
		[ClientPropertyName("folderChange")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[ClientControlEvent]
		[Category("Client-side events")]
		public string OnClientFolderChange
		{
			get
			{
				return ((string)this.ViewState["OnClientFolderChange"]) ?? string.Empty;
			}
			set
			{
				this.ViewState["OnClientFolderChange"] = value;
			}
		}

		// Token: 0x17004921 RID: 18721
		// (get) Token: 0x0600F23D RID: 62013 RVA: 0x00373CC9 File Offset: 0x00371EC9
		// (set) Token: 0x0600F23E RID: 62014 RVA: 0x00373CE9 File Offset: 0x00371EE9
		[ClientPropertyName("init")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[DefaultValue("")]
		[ClientControlEvent]
		[Category("Client-side events")]
		[Description("The name of the javascript function called before the control loads in the browser.")]
		public string OnClientInit
		{
			get
			{
				return ((string)this.ViewState["OnClientInit"]) ?? string.Empty;
			}
			set
			{
				this.ViewState["OnClientInit"] = value;
			}
		}

		// Token: 0x17004922 RID: 18722
		// (get) Token: 0x0600F23F RID: 62015 RVA: 0x00373CFC File Offset: 0x00371EFC
		// (set) Token: 0x0600F240 RID: 62016 RVA: 0x00373D1C File Offset: 0x00371F1C
		[ClientPropertyName("load")]
		[Description("The name of the javascript function called when the control loads in the browser.")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[Category("Client-side events")]
		[DefaultValue("")]
		[ClientControlEvent]
		public string OnClientLoad
		{
			get
			{
				return ((string)this.ViewState["OnClientLoad"]) ?? string.Empty;
			}
			set
			{
				this.ViewState["OnClientLoad"] = value;
			}
		}

		// Token: 0x17004923 RID: 18723
		// (get) Token: 0x0600F241 RID: 62017 RVA: 0x00373D2F File Offset: 0x00371F2F
		// (set) Token: 0x0600F242 RID: 62018 RVA: 0x00373D4F File Offset: 0x00371F4F
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[DefaultValue("")]
		[ClientPropertyName("createNewFolder")]
		[ClientControlEvent]
		[Category("Client-side events")]
		[Description("The name of the javascript function called when the user tries to create a new folder.")]
		public string OnClientCreateNewFolder
		{
			get
			{
				return ((string)this.ViewState["OnClientCreateNewFolder"]) ?? string.Empty;
			}
			set
			{
				this.ViewState["OnClientCreateNewFolder"] = value;
			}
		}

		// Token: 0x17004924 RID: 18724
		// (get) Token: 0x0600F243 RID: 62019 RVA: 0x00373D62 File Offset: 0x00371F62
		// (set) Token: 0x0600F244 RID: 62020 RVA: 0x00373D82 File Offset: 0x00371F82
		[DefaultValue("")]
		[ClientPropertyName("delete")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[ClientControlEvent]
		[Category("Client-side events")]
		[Description("The name of the javascript function called when the user tries to delete a file.")]
		public string OnClientDelete
		{
			get
			{
				return ((string)this.ViewState["OnClientDelete"]) ?? string.Empty;
			}
			set
			{
				this.ViewState["OnClientDelete"] = value;
			}
		}

		// Token: 0x17004925 RID: 18725
		// (get) Token: 0x0600F245 RID: 62021 RVA: 0x00373D95 File Offset: 0x00371F95
		// (set) Token: 0x0600F246 RID: 62022 RVA: 0x00373DB5 File Offset: 0x00371FB5
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[ClientPropertyName("move")]
		[DefaultValue("")]
		[ClientControlEvent]
		[Category("Client-side events")]
		[Description("The name of the javascript function called when the user tries to rename/move a file or folder.")]
		public string OnClientMove
		{
			get
			{
				return ((string)this.ViewState["OnClientMove"]) ?? string.Empty;
			}
			set
			{
				this.ViewState["OnClientMove"] = value;
			}
		}

		// Token: 0x17004926 RID: 18726
		// (get) Token: 0x0600F247 RID: 62023 RVA: 0x00373DC8 File Offset: 0x00371FC8
		// (set) Token: 0x0600F248 RID: 62024 RVA: 0x00373DE8 File Offset: 0x00371FE8
		[ClientPropertyName("copy")]
		[Description("The name of the javascript function called when the user tries to copy a file or folder.")]
		[DefaultValue("")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[ClientControlEvent]
		[Category("Client-side events")]
		public string OnClientCopy
		{
			get
			{
				return ((string)this.ViewState["OnClientCopy"]) ?? string.Empty;
			}
			set
			{
				this.ViewState["OnClientCopy"] = value;
			}
		}

		// Token: 0x17004927 RID: 18727
		// (get) Token: 0x0600F249 RID: 62025 RVA: 0x00373DFB File Offset: 0x00371FFB
		// (set) Token: 0x0600F24A RID: 62026 RVA: 0x00373E1B File Offset: 0x0037201B
		[ClientControlEvent]
		[Description("The name of the javascript function called when the user filters the files in the grid.")]
		[Category("Client-side events")]
		[DefaultValue("")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[ClientPropertyName("filter")]
		public string OnClientFilter
		{
			get
			{
				return ((string)this.ViewState["OnClientFilter"]) ?? string.Empty;
			}
			set
			{
				this.ViewState["OnClientFilter"] = value;
			}
		}

		// Token: 0x17004928 RID: 18728
		// (get) Token: 0x0600F24B RID: 62027 RVA: 0x00373E2E File Offset: 0x0037202E
		// (set) Token: 0x0600F24C RID: 62028 RVA: 0x00373E4E File Offset: 0x0037204E
		[ClientControlEvent]
		[Description("The name of the client-side method called when the user drops files in the file list.")]
		[Category("Client-side events")]
		[ClientPropertyName("filesDropping")]
		[DefaultValue("")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		public string OnClientFilesDropping
		{
			get
			{
				return ((string)this.ViewState["OnClientFilesDropping"]) ?? string.Empty;
			}
			set
			{
				this.ViewState["OnClientFilesDropping"] = value;
			}
		}

		// Token: 0x140001CA RID: 458
		// (add) Token: 0x0600F24D RID: 62029 RVA: 0x00373E61 File Offset: 0x00372061
		// (remove) Token: 0x0600F24E RID: 62030 RVA: 0x00373E74 File Offset: 0x00372074
		[Description("Fired when on all file explorer file and folder operations.")]
		[Category("Behavior")]
		public event RadFileExplorerEventHandler ItemCommand
		{
			add
			{
				base.Events.AddHandler(RadFileExplorer.itemCommandEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadFileExplorer.itemCommandEvent, value);
			}
		}

		// Token: 0x140001CB RID: 459
		// (add) Token: 0x0600F24F RID: 62031 RVA: 0x00373E87 File Offset: 0x00372087
		// (remove) Token: 0x0600F250 RID: 62032 RVA: 0x00373E9A File Offset: 0x0037209A
		[Description("Fired when the grid data is retrieved from the content provider.")]
		[Category("Behavior")]
		public event RadFileExplorerGridEventHandler ExplorerPopulated
		{
			add
			{
				base.Events.AddHandler(RadFileExplorer.explorerPopulatedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadFileExplorer.explorerPopulatedEvent, value);
			}
		}

		// Token: 0x0400458C RID: 17804
		private const string Nbsp = "&nbsp;";

		// Token: 0x0400458D RID: 17805
		private RadToolBar _toolbar;

		// Token: 0x0400458E RID: 17806
		private RadTreeView _tree;

		// Token: 0x0400458F RID: 17807
		private FileList _fileList;

		// Token: 0x04004590 RID: 17808
		private RadContextMenu _gridContextMenu;

		// Token: 0x04004591 RID: 17809
		private UpdatePanel _updatePanel;

		// Token: 0x04004592 RID: 17810
		private Button _postbackButton;

		// Token: 0x04004593 RID: 17811
		private HtmlInputHidden _postbackArgument;

		// Token: 0x04004594 RID: 17812
		private RadAjaxLoadingPanel _loadingPanel;

		// Token: 0x04004595 RID: 17813
		private RadSplitter _splitter;

		// Token: 0x04004596 RID: 17814
		private RadWindowManager _windowManager;

		// Token: 0x04004597 RID: 17815
		private RadSplitBar splitBar;

		// Token: 0x04004598 RID: 17816
		private RadPane radPaneGrid;

		// Token: 0x04004599 RID: 17817
		private RadPane radpaneTree;

		// Token: 0x0400459A RID: 17818
		private TextBox _addressBox;

		// Token: 0x0400459B RID: 17819
		private HtmlInputHidden _currentFolderInput;

		// Token: 0x0400459C RID: 17820
		private HtmlInputHidden _feCommandArgument;

		// Token: 0x0400459D RID: 17821
		private RadToolTip _tooltip;

		// Token: 0x0400459E RID: 17822
		private HtmlGenericControl _uploadContainer;

		// Token: 0x0400459F RID: 17823
		private RadUpload _upload;

		// Token: 0x040045A0 RID: 17824
		private RadAsyncUpload _asyncUpload;

		// Token: 0x040045A1 RID: 17825
		private CheckBox chkOverwrite;

		// Token: 0x040045A2 RID: 17826
		private RadButton uploadButton;

		// Token: 0x040045A3 RID: 17827
		private string _selectedFile;

		// Token: 0x040045A4 RID: 17828
		private HtmlGenericControl _infoFields;

		// Token: 0x040045A5 RID: 17829
		private bool _shouldBindTree;

		// Token: 0x040045A6 RID: 17830
		private HtmlGenericControl dropZone;

		// Token: 0x040045A7 RID: 17831
		private RadButton cancelUploadButton;

		// Token: 0x040045A8 RID: 17832
		private Label dropUploadProgressLabel;

		// Token: 0x040045A9 RID: 17833
		private Panel dropUploadInfoPanel;

		// Token: 0x040045AA RID: 17834
		private string selectedTreeViewNodeValue;

		// Token: 0x040045AB RID: 17835
		private static readonly object itemCommandEvent = new object();

		// Token: 0x040045AC RID: 17836
		private static readonly object explorerPopulatedEvent = new object();

		// Token: 0x040045AD RID: 17837
		private bool _isTreeMenuCreated;

		// Token: 0x040045AE RID: 17838
		private FileBrowserContentProvider _contentProvider;

		// Token: 0x040045AF RID: 17839
		private CultureInfo _culture;

		// Token: 0x040045B0 RID: 17840
		private string _callbackPath;

		// Token: 0x040045B1 RID: 17841
		private string _sortExpression;

		// Token: 0x040045B2 RID: 17842
		private string _filterKeyWord;

		// Token: 0x040045B3 RID: 17843
		private int _startIndex;

		// Token: 0x040045B4 RID: 17844
		private int _maxRowNumber;

		// Token: 0x040045B5 RID: 17845
		private bool pathsChanged;

		// Token: 0x040045B6 RID: 17846
		private List<string> _localizationKeys = new List<string>(new string[]
		{
			"CreateNewFolder",
			"ConfirmDelete",
			"Rename",
			"OK",
			"Cancel",
			"Delete",
			"InvalidFileSize",
			"InvalidFileExtension"
		});

		// Token: 0x040045B7 RID: 17847
		private DialogLocalizationStrings _localization;

		// Token: 0x040045B8 RID: 17848
		private FileExplorerConfiguration _configuration;

		// Token: 0x040045B9 RID: 17849
		private FileExplorerShortcut _shortcuts;

		// Token: 0x0200184F RID: 6223
		[SuppressMessage("Microsoft.Usage", "CA2213:DisposableFieldsShouldBeDisposed")]
		private class FilterTemplate : ITemplate, IDisposable
		{
			// Token: 0x0600F256 RID: 62038 RVA: 0x00373EC4 File Offset: 0x003720C4
			public FilterTemplate(string text)
			{
				string text2 = HttpUtility.HtmlEncode(text);
				this._txt = new TextBox();
				this._txt.ID = "FilterTextBox";
				this._txt.EnableViewState = false;
				this._txt.ToolTip = text2;
				this._txt.CssClass = "rfeFilterTxt";
				this._lbl = new Label();
				this._lbl.EnableViewState = false;
				this._lbl.ID = "FilterLabel";
				this._lbl.AssociatedControlID = "FilterTextBox";
				this._lbl.Text = text2;
				this._lbl.CssClass = "rfeFilterLbl";
				this._pnl = new Panel();
				this._pnl.EnableViewState = false;
				this._pnl.ID = "FilterDiv";
				this._pnl.CssClass = "rfeFilterWrapper";
			}

			// Token: 0x0600F257 RID: 62039 RVA: 0x00373FAB File Offset: 0x003721AB
			public void InstantiateIn(Control container)
			{
				container.Controls.Add(this._pnl);
				this._pnl.Controls.Add(this._lbl);
				this._pnl.Controls.Add(this._txt);
			}

			// Token: 0x0600F258 RID: 62040 RVA: 0x00373FEA File Offset: 0x003721EA
			protected virtual void Dispose(bool disposing)
			{
				if (disposing)
				{
					if (this._pnl != null)
					{
						this._pnl.Dispose();
					}
					if (this._txt != null)
					{
						this._txt.Dispose();
					}
					if (this._lbl != null)
					{
						this._lbl.Dispose();
					}
				}
			}

			// Token: 0x0600F259 RID: 62041 RVA: 0x00374028 File Offset: 0x00372228
			public void Dispose()
			{
				this.Dispose(true);
				GC.SuppressFinalize(this);
			}

			// Token: 0x040045BC RID: 17852
			private TextBox _txt;

			// Token: 0x040045BD RID: 17853
			private Label _lbl;

			// Token: 0x040045BE RID: 17854
			private Panel _pnl;
		}

		// Token: 0x02001850 RID: 6224
		internal struct callbackResponseStruct
		{
			// Token: 0x040045BF RID: 17855
			public int count;

			// Token: 0x040045C0 RID: 17856
			public List<FileBrowserItem> data;
		}

		// Token: 0x02001851 RID: 6225
		protected class TreeNodeTemplate : ITemplate
		{
			// Token: 0x0600F25A RID: 62042 RVA: 0x00374038 File Offset: 0x00372238
			public void InstantiateIn(Control container)
			{
				Label label = new Label();
				label.DataBinding += this.LblImageDataBinding;
				container.Controls.Add(label);
				Label label2 = new Label();
				label2.DataBinding += this.LblTextDataBinding;
				container.Controls.Add(label2);
			}

			// Token: 0x0600F25B RID: 62043 RVA: 0x00374090 File Offset: 0x00372290
			private void LblImageDataBinding(object sender, EventArgs e)
			{
				Label label = sender as Label;
				RadTreeNode radTreeNode = label.Parent as RadTreeNode;
				string text = (radTreeNode.Attributes["Extension"] != null) ? radTreeNode.Attributes["Extension"] : string.Empty;
				text = text.Substring(text.LastIndexOf('.') + 1);
				if (RadFileExplorer.IsNodeADirectory(radTreeNode))
				{
					text = " folder";
				}
				text = text.Replace("\"", "_").Replace("\n", "_").Replace("\r", "_");
				label.Attributes.Add("class", string.Format("rfeFileExtension {0}", text.ToLowerInvariant()));
				label.Text = "&nbsp;";
			}

			// Token: 0x0600F25C RID: 62044 RVA: 0x00374154 File Offset: 0x00372354
			private void LblTextDataBinding(object sender, EventArgs e)
			{
				Label label = sender as Label;
				RadTreeNode radTreeNode = label.Parent as RadTreeNode;
				string text = HttpUtility.HtmlEncode(radTreeNode.Text);
				label.Text = text;
			}
		}
	}
}
