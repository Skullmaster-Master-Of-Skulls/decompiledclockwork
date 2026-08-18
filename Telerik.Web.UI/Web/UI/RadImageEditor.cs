using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Net;
using System.Reflection;
using System.Text;
using System.Web;
using System.Web.Caching;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.Design;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Xml;
using Telerik.Licensing;
using Telerik.Web.UI.Common;
using Telerik.Web.UI.ImageEditor;
using Telerik.Web.UI.ImageEditor.Serialization;
using Telerik.Web.UI.Widgets;

namespace Telerik.Web.UI
{
	// Token: 0x02000BAD RID: 2989
	[ClientScriptResource("Telerik.Web.UI.RadImageEditor", "Telerik.Web.UI.Common.Core.js")]
	[DefaultProperty("ImageUrl")]
	[Description("Telerik Image Editor component")]
	[ToolboxData("<{0}:RadImageEditor runat=\"server\"></{0}:RadImageEditor>")]
	[TelerikToolboxCategory("Miscellaneous")]
	[ToolboxBitmap(typeof(RadImageEditor), "Telerik.Web.UI.ImageEditor.png")]
	[SupportsEventValidation]
	[DefaultEvent("ImageChanged")]
	[Designer("Telerik.Web.Design.RadImageEditorDesigner, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4")]
	[LicenseProvider(typeof(TelerikLicenseProvider))]
	[LightweightRendering]
	[EmbeddedSkin("ImageEditor")]
	[EmbeddedSkin("ImageEditor", "Default")]
	[RequiredCss("Telerik.Web.UI.Skins.Common.fonticons.css", RenderMode.Lightweight, typeof(RadImageEditor))]
	[RequiredScript(typeof(RadImageEditorScripts))]
	public class RadImageEditor : RadWebControl, ILocalizableControl, INamingContainer
	{
		// Token: 0x0600709B RID: 28827 RVA: 0x001A4520 File Offset: 0x001A2720
		protected virtual EditableImage ApplyServerOperation(Dictionary<string, object> data)
		{
			if (!data.ContainsKey("name"))
			{
				throw new NotSupportedException("The requested operation is not supported");
			}
			if (data["name"].ToString() == "SaveCanvas")
			{
				return this.ApplyServerOperation_Canvas(data);
			}
			return this.ApplyServerOperation_IMG(data);
		}

		// Token: 0x0600709C RID: 28828 RVA: 0x001A480C File Offset: 0x001A2A0C
		private EditableImage ApplyServerOperation_IMG(Dictionary<string, object> data)
		{
			ICacheImageProvider cacheImageProvider = this.GetCacheImageProvider();
			string text = data["name"].ToString();
			if (text == "Reset")
			{
				cacheImageProvider.ClearImages();
				this.CurrentImageKey = "";
			}
			EditableImage editableImage = data.ContainsKey("key") ? this.GetKeyedEditableImage(data["key"].ToString()) : this.GetCurrentEditableImage();
			if (editableImage == null)
			{
				editableImage = this.GetCurrentEditableImage();
			}
			if (editableImage == null)
			{
				throw new MissingEditableImageException();
			}
			bool flag = false;
			string alertMessage = "";
			if (text == "Save")
			{
				this.UndoStack.Clear();
				this.UndoStack.Deserialize(data["clientOps"].ToString());
				if (data.ContainsKey("save") && data["save"].ToString() == "1")
				{
					if (this.UndoStack != null && this.UndoStack.Count > 0)
					{
						this.ApplyClientOperations(editableImage);
					}
					flag = true;
				}
				else
				{
					editableImage = this.ApplyClientOperations(editableImage);
				}
			}
			else if (text != "Reset")
			{
				editableImage = (this.ApplyClientOperations(editableImage, this.GetClientOperations(data)) ?? editableImage);
				string argument = data.ContainsKey("commandArgument") ? data["commandArgument"].ToString() : string.Empty;
				ImageEditorEditingEventArgs imageEditorEditingEventArgs = new ImageEditorEditingEventArgs(editableImage, text, argument, data);
				this.OnImageEditing(imageEditorEditingEventArgs);
				this._argument = imageEditorEditingEventArgs.Argument;
				if (!imageEditorEditingEventArgs.Cancel)
				{
					string a;
					if ((a = text) != null)
					{
						if (a == "Crop")
						{
							editableImage.Crop(new Rectangle((int)data["x"], (int)data["y"], (int)data["width"], (int)data["height"]));
							goto IL_335;
						}
						if (a == "AddText")
						{
							editableImage.AddText(new Point((int)data["x"], (int)data["y"]), new ImageText
							{
								Color = (string)data["color"],
								FontFamily = (string)data["font"],
								Size = (float)((int)data["size"]),
								Value = HttpUtility.HtmlDecode((string)data["text"])
							});
							goto IL_335;
						}
						if (a == "InsertImage")
						{
							System.Drawing.Image imageToInsert = this.GetImageToInsert(data["value"].ToString());
							if (imageToInsert != null)
							{
								EditableImage editableImage2 = new EditableImage(imageToInsert);
								editableImage2 = (this.ApplyClientOperations(editableImage2, this.GetClientOperations(data, "arrayOps")) ?? editableImage2);
								editableImage.InsertImage(new Point((int)data["x"], (int)data["y"]), editableImage2.Image);
								goto IL_335;
							}
							alertMessage = "Could not Insert Image";
							goto IL_335;
						}
					}
					throw new NotSupportedException("The requested operation is not supported");
				}
			}
			IL_335:
			string key = string.Empty;
			string serverUrl = string.Empty;
			if (flag)
			{
				bool flag2 = data.ContainsKey("ext") && this.IsEditableImageFormat(data["ext"].ToString());
				if (flag2)
				{
					editableImage.ConvertTo(this.ReadFormatFromExtension(data["ext"].ToString()));
				}
				string text2 = this.SaveEditableImage(editableImage, cacheImageProvider, data["fName"].ToString(), data["overwrite"].ToString() == "1", flag2);
				if (string.IsNullOrEmpty(text2))
				{
					serverUrl = this.ImageUrl;
				}
				else
				{
					alertMessage = this.Localization.Dialogs.GetString("Common_" + text2);
				}
			}
			string text3 = data["undoLimitKey"].ToString();
			if (!string.IsNullOrEmpty(text3))
			{
				cacheImageProvider.ClearImages(text3);
			}
			key = this.StoreEditableImage(editableImage, cacheImageProvider);
			this._editableImageXHPanel.Controls.Add(new Literal
			{
				Text = this.SerializeDataToJson(new
				{
					url = base.ResolveUrl(this.CurrentImageUrl),
					key = key,
					serverUrl = serverUrl,
					alertMessage = alertMessage,
					fileName = this._fileName,
					args = this._argument
				})
			});
			return editableImage;
		}

		// Token: 0x0600709D RID: 28829 RVA: 0x001A4EC0 File Offset: 0x001A30C0
		private EditableImage ApplyServerOperation_Canvas(Dictionary<string, object> data)
		{
			string text = data.ContainsKey("base64") ? data["base64"].ToString() : string.Empty;
			if (string.IsNullOrEmpty(text))
			{
				return null;
			}
			byte[] array = Convert.FromBase64String(text);
			MemoryStream memoryStream = new MemoryStream();
			memoryStream.Write(array, 0, array.Length);
			EditableImage editableImage = new EditableImage(memoryStream);
			bool flag = true;
			string alertMessage = "";
			if (flag)
			{
				bool flag2 = data.ContainsKey("ext") && this.IsEditableImageFormat(data["ext"].ToString());
				if (flag2)
				{
					editableImage.ConvertTo(this.ReadFormatFromExtension(data["ext"].ToString()));
				}
				string text2 = this.SaveEditableImage(editableImage, this.GetCacheImageProvider(), data["fName"].ToString(), data["overwrite"].ToString() == "1", flag2);
				if (!string.IsNullOrEmpty(text2))
				{
					alertMessage = this.Localization.Dialogs.GetString("Common_" + text2);
				}
			}
			this._editableImageXHPanel.Controls.Add(new Literal
			{
				Text = this.SerializeDataToJson(new
				{
					url = "",
					key = "",
					alertMessage = alertMessage,
					fileName = this._fileName,
					args = this._argument
				})
			});
			return editableImage;
		}

		// Token: 0x0600709E RID: 28830 RVA: 0x001A501A File Offset: 0x001A321A
		private ImageOperationCollection GetClientOperations(Dictionary<string, object> data)
		{
			return this.GetClientOperations(data, "clientOps");
		}

		// Token: 0x0600709F RID: 28831 RVA: 0x001A5028 File Offset: 0x001A3228
		private ImageOperationCollection GetClientOperations(Dictionary<string, object> data, string key)
		{
			ImageOperationCollection imageOperationCollection = new ImageOperationCollection();
			if (data.ContainsKey(key))
			{
				imageOperationCollection.Deserialize(data[key].ToString());
			}
			return imageOperationCollection;
		}

		// Token: 0x060070A0 RID: 28832 RVA: 0x001A5058 File Offset: 0x001A3258
		private System.Drawing.Image GetImageToInsert(string url)
		{
			if (string.IsNullOrEmpty(url))
			{
				return null;
			}
			if (url.StartsWith("http://") || url.StartsWith("https://"))
			{
				System.Drawing.Image result;
				try
				{
					HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(new Uri(url, UriKind.RelativeOrAbsolute));
					httpWebRequest.AllowWriteStreamBuffering = true;
					httpWebRequest.UserAgent = "Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 5.1)";
					httpWebRequest.Method = "GET";
					httpWebRequest.Timeout = 20000;
					WebResponse response = httpWebRequest.GetResponse();
					Stream responseStream = response.GetResponseStream();
					System.Drawing.Image image = new Bitmap(responseStream);
					responseStream.Close();
					response.Close();
					result = image;
				}
				catch
				{
					result = null;
				}
				return result;
			}
			if (url.Contains("WebResource.axd?"))
			{
				return new Bitmap(Assembly.GetExecutingAssembly().GetManifestResourceStream(this.defaultInsertedImageUrl));
			}
			return new Bitmap(base.MapPathSecure(url));
		}

		// Token: 0x060070A1 RID: 28833 RVA: 0x001A5134 File Offset: 0x001A3334
		private EditableFormat ReadFormatFromExtension(string extension)
		{
			return (EditableFormat)Enum.Parse(typeof(EditableFormat), extension, true);
		}

		// Token: 0x060070A2 RID: 28834 RVA: 0x001A514C File Offset: 0x001A334C
		private bool IsEditableImageFormat(string extension)
		{
			bool result;
			try
			{
				this.ReadFormatFromExtension(extension);
				result = true;
			}
			catch
			{
				result = false;
			}
			return result;
		}

		// Token: 0x060070A3 RID: 28835 RVA: 0x001A517C File Offset: 0x001A337C
		protected virtual bool IsBuiltInCommand(string commandName)
		{
			return this._builtInCommandNames.Contains(commandName + ",");
		}

		// Token: 0x060070A4 RID: 28836 RVA: 0x001A5194 File Offset: 0x001A3394
		protected override void DescribeComponent(IScriptDescriptor descriptor)
		{
			base.DescribeComponent(descriptor);
			if (this._postbackButton != null)
			{
				descriptor.AddProperty("postbackButton", this._postbackButton.ClientID);
			}
			descriptor.AddProperty("toolGroupsLength", this.Tools.Count);
			descriptor.AddProperty("clientImageUrl", base.ResolveClientUrl(this.ImageUrl));
			descriptor.AddProperty("toolsLoadPanelType", this.ToolsLoadPanelType.ToString());
			descriptor.AddProperty("defaultInsertedImageUrl", base.ResolveClientUrl(SkinRegistrar.GetWebResourceUrl(this.Page, typeof(RadImageEditor), this.defaultInsertedImageUrl)));
			descriptor.AddScriptProperty("shortcuts", this.SerializeShortCuts());
			descriptor.AddProperty("_tabIndex", this.TabIndex);
			descriptor.AddProperty("_accessKey", this.AccessKey);
			descriptor.AddProperty("_imageFormat", this.GetImageFormatName());
			descriptor.AddProperty("_downloadKey", this.SerializeDownloadKey());
			descriptor.AddProperty("_canvasNotSupportedString", this.Localization.Main.CanvasNotSupported);
			descriptor.AddProperty("renderMode", this.ResolvedRenderMode);
		}

		// Token: 0x060070A5 RID: 28837 RVA: 0x001A52CC File Offset: 0x001A34CC
		protected override void LoadClientState(Dictionary<string, object> clientState)
		{
			base.LoadClientState(clientState);
			this.ActiveCommand = (clientState["currentCommand"] as string);
			if (clientState.ContainsKey("clientOps"))
			{
				this.UndoStack.Deserialize(clientState["clientOps"].ToString());
			}
			if (clientState.ContainsKey("imageUrl"))
			{
				this.ImageUrl = clientState["imageUrl"].ToString();
			}
			this.CurrentImageKey = (string)clientState["imageKey"];
			if (!string.IsNullOrEmpty(this.CurrentImageKey))
			{
				this.CurrentImageUrl = this.GetCacheImageHandlerUrl(this.CurrentImageKey);
			}
			else
			{
				this.CurrentImageUrl = this.ImageUrl;
			}
			if (clientState["height"] != null)
			{
				this.Height = Unit.Parse(clientState["height"].ToString(), CultureInfo.InvariantCulture);
			}
			if (clientState["width"] != null)
			{
				this.Width = Unit.Parse(clientState["width"].ToString(), CultureInfo.InvariantCulture);
			}
			if (clientState["enableResize"] != null)
			{
				this.EnableResize = (bool)clientState["enableResize"];
			}
			if (clientState["toolBarPosition"] != null)
			{
				this.ToolBarPosition = (ToolBarPosition)clientState["toolBarPosition"];
			}
			if (clientState["isf"] != null)
			{
				this._isUndocked = (string)clientState["isf"];
			}
		}

		// Token: 0x060070A6 RID: 28838 RVA: 0x001A5447 File Offset: 0x001A3647
		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);
			this.EnsureChildControls();
			this.Page.RegisterRequiresControlState(this);
			this.SetRenderModeChildRadControls();
		}

		// Token: 0x060070A7 RID: 28839 RVA: 0x001A5468 File Offset: 0x001A3668
		protected override void CreateChildControls()
		{
			base.CreateChildControls();
			this.CreateToolsPanel();
			this.CreateAjaxLoadingPanel();
			if (this.ToolsLoadPanelType == ToolsLoadPanelTypes.RadAjaxPanel)
			{
				this.CreateRadAjaxPanel();
			}
			else
			{
				this.CreateAjaxPanelControls();
			}
			this.CreateXmlHttpPanel();
			this.CreateEditableImageXmlHttpPanel();
			this.CreateFormDecorator();
			this.CreateDockLayout();
			this.CreateToolbarDock();
			this.CreateTopLeftZones();
			this.CreateRightBottomZones();
			this.ConfigureVisibilityOfPanels();
		}

		// Token: 0x060070A8 RID: 28840 RVA: 0x001A54CE File Offset: 0x001A36CE
		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
			this.SetSkinChildRadControls(this.Skin);
		}

		// Token: 0x060070A9 RID: 28841 RVA: 0x001A54E4 File Offset: 0x001A36E4
		protected override void ControlPreRender()
		{
			this.EnsureChildControls();
			this.StoreEditableImage(this.ApplyClientOperations());
			this.LoadToolsFile(true);
			this.CreateToolbar();
			this.SwitchToolBarPosition();
			if (this.ToolBarMode == ToolBarMode.Docked)
			{
				this._dockToolBar.Visible = true;
				this._zoneTop.Visible = true;
				this._zoneRight.Visible = true;
				this._zoneBottom.Visible = true;
				this._zoneLeft.Visible = true;
				this._zoneTop.FitDocks = (this._zoneBottom.FitDocks = (this.Tools.Count < 2));
			}
			else
			{
				this._dockToolBar.Visible = false;
				this._zoneTop.Visible = false;
				this._zoneRight.Visible = false;
				this._zoneBottom.Visible = false;
				this._zoneLeft.Visible = false;
			}
			this._dockToolsPanel.Closed = true;
			this._dockToolsPanel.Commands[0].Text = this.Localization.Main.Common_Close;
			this._ajaxLoadingPanel.Visible = this.ShowAjaxLoadingPanel;
			this._ajaxLoadingPanel.Skin = this.GetRuntimeSkin(false);
			this.ConfigureVisibilityOfPanels();
			this.HandleMetroTouchSkin(base.RuntimeSkin.EndsWith("Touch"));
			this.StoreDownloadKey(this.ImageUrl);
			base.ControlPreRender();
		}

		// Token: 0x060070AA RID: 28842 RVA: 0x001A5644 File Offset: 0x001A3844
		private void StoreDownloadKey(string imageUrl)
		{
			if (string.IsNullOrEmpty(this.DownloadKey))
			{
				this.DownloadKey = Guid.NewGuid().ToString();
			}
			if (!string.IsNullOrEmpty(imageUrl))
			{
				if (this.ImageCacheStorageLocation == ImageStorage.Cache)
				{
					HttpRuntime.Cache.Insert(this.DownloadKey, imageUrl, null, Cache.NoAbsoluteExpiration, TimeSpan.FromMinutes(60.0), CacheItemPriority.NotRemovable, null);
					return;
				}
				if (HttpContext.Current.Session != null)
				{
					HttpContext.Current.Session[this.DownloadKey] = imageUrl;
				}
			}
		}

		// Token: 0x060070AB RID: 28843 RVA: 0x001A56D4 File Offset: 0x001A38D4
		private void SwitchToolBarPosition()
		{
			RadDockZone dockZone = this._zoneTop;
			switch (this.ToolBarPosition)
			{
			case ToolBarPosition.Right:
				dockZone = this._zoneRight;
				this._toolBarContainer.Attributes.Add("class", "rieToolBar rieZoneRight");
				break;
			case ToolBarPosition.Bottom:
				dockZone = this._zoneBottom;
				this._toolBarContainer.ID = "ToolBarContainerBottom";
				break;
			case ToolBarPosition.Left:
				dockZone = this._zoneLeft;
				this._toolBarContainer.Attributes.Add("class", "rieToolBar rieZoneLeft");
				break;
			default:
				dockZone = this._zoneTop;
				break;
			}
			if (this._isUndocked != "1")
			{
				this._dockToolBar.Undock();
				this._dockToolBar.Dock(dockZone);
			}
		}

		// Token: 0x060070AC RID: 28844 RVA: 0x001A5794 File Offset: 0x001A3994
		private void SetRenderModeChildRadControls()
		{
			this.SetControlRenderMode(this._formDecorator);
			this.SetControlRenderMode(this._ajaxLoadingPanel);
			this.SetControlRenderMode(this._dockToolsPanel);
			this.SetControlRenderMode(this._dockToolBar);
			if (this._dockLayout != null)
			{
				foreach (RadDockZone controlRenderMode in this._dockLayout.RegisteredZones)
				{
					this.SetControlRenderMode(controlRenderMode);
				}
			}
		}

		// Token: 0x060070AD RID: 28845 RVA: 0x001A5820 File Offset: 0x001A3A20
		private void SetControlRenderMode(ISkinnableControl control)
		{
			if (control != null)
			{
				control.RenderMode = this.RenderMode;
			}
		}

		// Token: 0x060070AE RID: 28846 RVA: 0x001A5834 File Offset: 0x001A3A34
		protected override void AddAttributesToRender(HtmlTextWriter writer)
		{
			string accessKey = this.AccessKey;
			this.AccessKey = string.Empty;
			short tabIndex = this.TabIndex;
			this.TabIndex = 0;
			base.AddAttributesToRender(writer);
			this.RegisterEventValidationScripts();
			this.AccessKey = accessKey;
			this.TabIndex = tabIndex;
		}

		// Token: 0x060070AF RID: 28847 RVA: 0x001A587C File Offset: 0x001A3A7C
		private void RegisterEventValidationScripts()
		{
			if (this.Page == null || this._postbackButton == null)
			{
				return;
			}
			foreach (object obj in this.Tools)
			{
				ImageEditorToolGroup imageEditorToolGroup = (ImageEditorToolGroup)obj;
				List<ImageEditorToolBase> allTools = imageEditorToolGroup.GetAllTools();
				foreach (ImageEditorToolBase imageEditorToolBase in allTools)
				{
					ImageEditorTool imageEditorTool = imageEditorToolBase as ImageEditorTool;
					if (imageEditorTool != null)
					{
						this.Page.ClientScript.RegisterForEventValidation(this._postbackButton.UniqueID, imageEditorTool.CommandName);
					}
					else
					{
						ImageEditorToolStrip imageEditorToolStrip = imageEditorToolBase as ImageEditorToolStrip;
						if (imageEditorToolStrip != null)
						{
							this.Page.ClientScript.RegisterForEventValidation(this._postbackButton.UniqueID, imageEditorToolStrip.CommandName);
						}
					}
				}
				foreach (string argument in this.customCommands)
				{
					this.Page.ClientScript.RegisterForEventValidation(this._postbackButton.UniqueID, argument);
				}
			}
		}

		// Token: 0x060070B0 RID: 28848 RVA: 0x001A59E0 File Offset: 0x001A3BE0
		protected override void RenderChildren(HtmlTextWriter writer)
		{
			if (this.HasControls())
			{
				foreach (object obj in this.Controls)
				{
					Control control = (Control)obj;
					if (control.ID == "DockLayout" && control.HasControls())
					{
						using (IEnumerator enumerator2 = this._dockLayout.Controls.GetEnumerator())
						{
							while (enumerator2.MoveNext())
							{
								object obj2 = enumerator2.Current;
								Control control2 = (Control)obj2;
								if (control2.ID != "ZoneBottom")
								{
									control2.RenderControl(writer);
								}
							}
							continue;
						}
					}
					if (control.ID != "ToolBarContainerBottom")
					{
						control.RenderControl(writer);
					}
				}
			}
		}

		// Token: 0x060070B1 RID: 28849 RVA: 0x001A5ADC File Offset: 0x001A3CDC
		protected override void RenderContents(HtmlTextWriter writer)
		{
			if (base.DesignMode)
			{
				this.EnsureChildControls();
				writer.Write(SkinRegistrar.GetDesignTimeStyleSheet(this));
				this.LoadToolsFile(true);
				this.CreateToolbar();
				if (this.ToolBarMode == ToolBarMode.Docked)
				{
					this._dockToolBar.Visible = true;
					this._zoneTop.Visible = true;
					this._zoneRight.Visible = true;
					this._zoneBottom.Visible = true;
					this._zoneLeft.Visible = true;
					this._zoneTop.FitDocks = (this._zoneBottom.FitDocks = (this.Tools.Count < 2));
				}
				else
				{
					this._dockToolBar.Visible = false;
					this._zoneTop.Visible = false;
					this._zoneRight.Visible = false;
					this._zoneBottom.Visible = false;
					this._zoneLeft.Visible = false;
				}
				this._dockToolsPanel.Closed = true;
			}
			base.RenderContents(writer);
			if (this.StatusBarMode == StatusBarMode.Top)
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Id, this.ClientID + "_SB");
				writer.AddAttribute(HtmlTextWriterAttribute.Class, "rieStatusBarInfo");
				writer.RenderBeginTag(HtmlTextWriterTag.Div);
				this.RenderStatusBarInfo(writer);
				writer.RenderEndTag();
			}
			writer.AddAttribute(HtmlTextWriterAttribute.Id, string.Format("{0}_Viewport", this.ClientID));
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "rieContentArea");
			writer.AddStyleAttribute("position", "relative");
			writer.RenderBeginTag(HtmlTextWriterTag.Div);
			this.RenderImageContent(writer);
			writer.RenderEndTag();
			if (this._toolBarContainer.ID == "ToolBarContainerBottom")
			{
				this._toolBarContainer.RenderControl(writer);
			}
			this._zoneBottom.RenderControl(writer);
			this.RenderStatusBar(writer);
		}

		// Token: 0x060070B2 RID: 28850 RVA: 0x001A5C94 File Offset: 0x001A3E94
		private void RenderImageContent(HtmlTextWriter writer)
		{
			writer.AddAttribute(HtmlTextWriterAttribute.Id, string.Format("{0}_EditableImage", this.ClientID));
			writer.AddAttribute(HtmlTextWriterAttribute.Src, base.DesignMode ? base.ResolveClientUrl(this.CurrentImageUrl) : base.ResolveUrl(this.CurrentImageUrl));
			writer.AddAttribute(HtmlTextWriterAttribute.Alt, this.AlternateText);
			if (!string.IsNullOrEmpty(this.DescriptionUrl))
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Longdesc, this.DescriptionUrl);
			}
			writer.RenderBeginTag(HtmlTextWriterTag.Img);
			writer.RenderEndTag();
		}

		// Token: 0x060070B3 RID: 28851 RVA: 0x001A5D1C File Offset: 0x001A3F1C
		private void RenderStatusBar(HtmlTextWriter writer)
		{
			bool flag = this.StatusBarMode == StatusBarMode.Bottom;
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "rieStatusBar");
			writer.RenderBeginTag(HtmlTextWriterTag.Div);
			if (flag)
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Id, this.ClientID + "_SB");
			}
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "rieStatusBarInfo");
			writer.RenderBeginTag(HtmlTextWriterTag.Div);
			if (flag)
			{
				this.RenderStatusBarInfo(writer);
			}
			writer.RenderEndTag();
			writer.AddAttribute(HtmlTextWriterAttribute.Id, this.ClientID + "_ResizeHandle");
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "rieResizeHandle");
			writer.RenderBeginTag(HtmlTextWriterTag.Div);
			writer.RenderEndTag();
			writer.RenderEndTag();
		}

		// Token: 0x060070B4 RID: 28852 RVA: 0x001A5DC4 File Offset: 0x001A3FC4
		private void RenderStatusBarInfo(HtmlTextWriter writer)
		{
			this.Render_SB_Label(writer, this.Localization.Main.StatusBar_Zoom + ":");
			writer.Write("<span>");
			this.Render_SB_Span(writer, "100", "sbZoom");
			writer.WriteEncodedText("%");
			writer.Write("</span>");
			this.Render_SB_Label(writer, this.Localization.Main.StatusBar_Size + ":");
			writer.Write("<span>");
			this.Render_SB_Span(writer, "0", "sbW");
			writer.WriteEncodedText("x");
			this.Render_SB_Span(writer, "0", "sbH");
			writer.WriteEncodedText("px");
			writer.Write("</span>");
			this.Render_SB_Label(writer, this.Localization.Main.StatusBar_Position + ":");
			writer.Write("<span>");
			writer.WriteEncodedText("(");
			this.Render_SB_Span(writer, "-", "sbX");
			writer.WriteEncodedText(",");
			this.Render_SB_Span(writer, "-", "sbY");
			writer.WriteEncodedText(")");
			writer.Write("</span>");
			this.Render_SB_Label(writer, this.Localization.Main.StatusBar_LastAction + ":");
			this.Render_SB_Span(writer, "None", "sbAction");
		}

		// Token: 0x060070B5 RID: 28853 RVA: 0x001A5F3F File Offset: 0x001A413F
		private void Render_SB_Label(HtmlTextWriter writer, string text)
		{
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "rieLabel");
			writer.RenderBeginTag(HtmlTextWriterTag.Span);
			writer.WriteEncodedText(text);
			writer.RenderEndTag();
		}

		// Token: 0x060070B6 RID: 28854 RVA: 0x001A5F63 File Offset: 0x001A4163
		private void Render_SB_Span(HtmlTextWriter writer, string defaultText, string id)
		{
			writer.AddAttribute(HtmlTextWriterAttribute.Id, this.ClientID + "_" + id);
			writer.RenderBeginTag(HtmlTextWriterTag.Span);
			writer.WriteEncodedText(defaultText);
			writer.RenderEndTag();
		}

		// Token: 0x060070B7 RID: 28855 RVA: 0x001A5F93 File Offset: 0x001A4193
		private EditableImage GetCurrentEditableImage()
		{
			return this.TryGetEditableImageFromSource(true);
		}

		// Token: 0x060070B8 RID: 28856 RVA: 0x001A5F9C File Offset: 0x001A419C
		private EditableImage TryGetEditableImageFromSource(bool alwaysCreateEditableImage = true)
		{
			if (!string.IsNullOrEmpty(this.CurrentImageKey))
			{
				return this.GetKeyedEditableImage(this.CurrentImageKey);
			}
			if (this.pendingImageLoadingAttempt)
			{
				ImageEditorLoadingEventArgs imageEditorLoadingEventArgs = new ImageEditorLoadingEventArgs(null);
				this.OnImageLoading(imageEditorLoadingEventArgs);
				this.pendingImageLoadingAttempt = false;
				if (imageEditorLoadingEventArgs.Cancel)
				{
					this.StoreEditableImage(imageEditorLoadingEventArgs.Image);
					return imageEditorLoadingEventArgs.Image;
				}
			}
			if (this.ImageManager.EnableContentProvider)
			{
				EditableImage imageFromContentProvider = this.GetImageFromContentProvider(this.ImageUrl);
				this.StoreEditableImage(imageFromContentProvider);
				return imageFromContentProvider;
			}
			if (!alwaysCreateEditableImage && string.IsNullOrEmpty(this.ImageManager.ImageProviderTypeName))
			{
				return null;
			}
			ICacheImageProvider cacheImageProvider = this.GetCacheImageProvider();
			EditableImage editableImage = string.IsNullOrEmpty(this.ImageUrl) ? null : new EditableImage(cacheImageProvider.LoadImage(this.ImageUrl, base.MapPathSecure(this.ImageUrl), this.Context));
			if (!string.IsNullOrEmpty(this.ImageManager.ImageProviderTypeName))
			{
				this.StoreEditableImage(editableImage);
			}
			return editableImage;
		}

		// Token: 0x060070B9 RID: 28857 RVA: 0x001A6090 File Offset: 0x001A4290
		private EditableImage GetImageFromContentProvider(string imageUrl)
		{
			if (!string.IsNullOrEmpty(imageUrl))
			{
				using (Stream file = this.ContentProvider.GetFile(imageUrl))
				{
					if (file == null)
					{
						throw new NullReferenceException(string.Format("The '{0}' path does not point to a valid image. Please specify a valid RadImageEditor.ImageUrl.", imageUrl));
					}
					byte[] array = new byte[file.Length];
					file.Read(array, 0, (int)file.Length);
					MemoryStream memoryStream = new MemoryStream();
					memoryStream.Write(array, 0, array.Length);
					return new EditableImage(memoryStream);
				}
			}
			return null;
		}

		// Token: 0x060070BA RID: 28858 RVA: 0x001A6120 File Offset: 0x001A4320
		private EditableImage GetKeyedEditableImage(string key)
		{
			ICacheImageProvider cacheImageProvider = this.GetCacheImageProvider();
			return cacheImageProvider.Retrieve(key);
		}

		// Token: 0x060070BB RID: 28859 RVA: 0x001A6140 File Offset: 0x001A4340
		protected internal virtual ICacheImageProvider GetCacheImageProvider()
		{
			if (this._cacheImageProvider == null)
			{
				this._cacheImageProvider = RadImageEditor.InitCacheImageProvider(this.ImageManager.ICacheImageProviderType);
			}
			this._cacheImageProvider.ImageStorageKey = this.ImageStorageKey;
			this._cacheImageProvider.Storage = this.ImageCacheStorageLocation;
			return this._cacheImageProvider;
		}

		// Token: 0x060070BC RID: 28860 RVA: 0x001A6193 File Offset: 0x001A4393
		protected internal static ICacheImageProvider InitCacheImageProvider(Type cacheImageProvider)
		{
			return (ICacheImageProvider)Activator.CreateInstance(cacheImageProvider);
		}

		// Token: 0x060070BD RID: 28861 RVA: 0x001A61A0 File Offset: 0x001A43A0
		private EditableImage ApplyClientOperations()
		{
			EditableImage result;
			try
			{
				EditableImage editableImage = this.ApplyClientOperations(this.TryGetEditableImageFromSource(this.UndoStack != null && this.UndoStack.Count > 0), this.UndoStack);
				this.UndoStack.Clear();
				result = editableImage;
			}
			catch (MissingEditableImageException)
			{
				result = null;
			}
			return result;
		}

		// Token: 0x060070BE RID: 28862 RVA: 0x001A6200 File Offset: 0x001A4400
		private EditableImage ApplyClientOperations(EditableImage editableImage)
		{
			EditableImage result = this.ApplyClientOperations(editableImage, this.UndoStack);
			this.UndoStack.Clear();
			return result;
		}

		// Token: 0x060070BF RID: 28863 RVA: 0x001A6227 File Offset: 0x001A4427
		private EditableImage ApplyClientOperations(EditableImage editableImage, ImageOperationCollection operations)
		{
			if (operations != null && operations.Count > 0)
			{
				operations.Sort();
				this.ApplyImageOperations(operations, editableImage);
				return editableImage;
			}
			return null;
		}

		// Token: 0x060070C0 RID: 28864 RVA: 0x001A6247 File Offset: 0x001A4447
		private string StoreEditableImage(EditableImage editableImage)
		{
			return this.StoreEditableImage(editableImage, this.GetCacheImageProvider());
		}

		// Token: 0x060070C1 RID: 28865 RVA: 0x001A6258 File Offset: 0x001A4458
		private string StoreEditableImage(EditableImage editableImage, ICacheImageProvider provider)
		{
			string text = string.Empty;
			if (editableImage != null && !editableImage.IsDisposed)
			{
				text = provider.Store(editableImage);
				this.CurrentImageUrl = this.GetCacheImageHandlerUrl(text);
				this.CurrentImageKey = text;
			}
			return text;
		}

		// Token: 0x060070C2 RID: 28866 RVA: 0x001A6293 File Offset: 0x001A4493
		private string SaveEditableImage(EditableImage editableImage, ICacheImageProvider provider, string imageName, bool overwrite)
		{
			return this.SaveEditableImage(editableImage, provider, imageName, overwrite, false);
		}

		// Token: 0x060070C3 RID: 28867 RVA: 0x001A62A4 File Offset: 0x001A44A4
		private string SaveEditableImage(EditableImage editableImage, ICacheImageProvider provider, string imageName, bool overwrite, bool preferImageFormatOverUrlExtension)
		{
			string text3;
			if (this.AllowedSavingLocation == AllowedSavingLocation.Server || this.AllowedSavingLocation == AllowedSavingLocation.ClientAndServer)
			{
				ImageEditorSavingEventArgs imageEditorSavingEventArgs = new ImageEditorSavingEventArgs(editableImage, string.IsNullOrEmpty(imageName) ? this.ExtractFileNameFromImageUrl() : imageName, overwrite);
				this.OnImageSaving(imageEditorSavingEventArgs);
				this._fileName = imageEditorSavingEventArgs.FileName;
				this._argument = imageEditorSavingEventArgs.Argument;
				if (imageEditorSavingEventArgs.Cancel)
				{
					return "";
				}
				imageName = imageEditorSavingEventArgs.FileName;
				overwrite = imageEditorSavingEventArgs.OverwriteFile;
				char value = this.ImageManager.EnableContentProvider ? this.ContentProvider.PathSeparator : '/';
				string text = preferImageFormatOverUrlExtension ? editableImage.Format : this.ImageUrl.Substring(this.ImageUrl.LastIndexOf('.') + 1).ToLowerInvariant();
				text = ((text == "jpg" || text == "png" || text == "gif" || text == "bmp" || text == "jpeg") ? text : editableImage.Format);
				if (string.IsNullOrEmpty(imageName))
				{
					imageName = this.ExtractFileNameFromImageUrl();
					imageName = (string.IsNullOrEmpty(imageName) ? ("RadImageEditor_" + Guid.NewGuid().ToString().Substring(0, 8)) : imageName) + '.' + text;
				}
				else
				{
					imageName = imageName + '.' + text;
				}
				this._fileName = this.SanitizeFileName(imageName);
				string text2 = (string.IsNullOrEmpty(this.ImageUrl) ? string.Empty : this.ImageUrl.Remove(this.ImageUrl.LastIndexOf(value) + 1)) + this._fileName;
				if (this.ImageManager.EnableContentProvider)
				{
					text2 = this.ContentProvider.GetPath(text2) + this._fileName;
					using (Bitmap bitmap = new Bitmap(editableImage.Image))
					{
						text3 = this.SaveImage(bitmap, editableImage.RawFormat, overwrite, text2);
						goto IL_214;
					}
				}
				string physicalPath = base.MapPathSecure(text2);
				text3 = provider.SaveImage(editableImage, physicalPath, text2, overwrite);
				IL_214:
				if (string.IsNullOrEmpty(text3))
				{
					this.ImageUrl = text2;
					this.CurrentImageUrl = text2;
					this.CurrentImageKey = "";
				}
			}
			else
			{
				text3 = "MessageCannotWriteToFolder";
			}
			return text3;
		}

		// Token: 0x060070C4 RID: 28868 RVA: 0x001A6504 File Offset: 0x001A4704
		private string SanitizeFileName(string fileName)
		{
			string text = fileName;
			List<char> list = new List<char>
			{
				'\\',
				'/'
			};
			if (this.ImageManager.EnableContentProvider)
			{
				list.Add(this.ContentProvider.PathSeparator);
			}
			foreach (char c in list)
			{
				text = text.Replace(c.ToString(), "");
			}
			return text;
		}

		// Token: 0x060070C5 RID: 28869 RVA: 0x001A6598 File Offset: 0x001A4798
		private string SaveImage(Bitmap img, ImageFormat originalImageFormat, bool overwrite, string imagePath)
		{
			string result;
			try
			{
				string folderPath = imagePath.Remove(imagePath.LastIndexOf(this.ContentProvider.PathSeparator));
				if (!this.ContentProvider.CheckWritePermissions(folderPath))
				{
					result = "MessageCannotWriteToFolder";
				}
				else
				{
					using (Stream file = this.ContentProvider.GetFile(imagePath))
					{
						bool flag = file != null && file.Length > 0L;
						if (flag)
						{
							file.Close();
						}
						if (flag && !overwrite)
						{
							return "FileExists";
						}
						if (flag)
						{
							if (!this.ContentProvider.CheckDeletePermissions(folderPath))
							{
								return "NoPermissionsToDeleteFile";
							}
							string value = this.ContentProvider.DeleteFile(imagePath);
							if (!string.IsNullOrEmpty(value))
							{
								return "MessageCannotWriteToFolder";
							}
						}
					}
					this.ContentProvider.StoreBitmap(img, imagePath, originalImageFormat);
					result = string.Empty;
				}
			}
			catch (Exception)
			{
				result = "MessageCannotWriteToFolder";
			}
			return result;
		}

		// Token: 0x060070C6 RID: 28870 RVA: 0x001A6698 File Offset: 0x001A4898
		private void CreateToolbar()
		{
			this._toolBarContainer = new HtmlGenericControl("div");
			this._toolBarContainer.Attributes.Add("class", "rieToolBar");
			this._toolBarContainer.EnableViewState = false;
			this.Controls.Add(this._toolBarContainer);
			this._toolBarContainer.Visible = false;
			ToolBarMode toolBarMode = this.ToolBarMode;
			Control control;
			if (toolBarMode == ToolBarMode.Docked)
			{
				control = this._dockToolBar.ContentContainer;
			}
			else
			{
				control = this._toolBarContainer;
				this._toolBarContainer.Visible = true;
			}
			RadToolBar radToolBar = new RadToolBar();
			radToolBar.EnableImageSprites = true;
			for (int i = 0; i < this.Tools.Count; i++)
			{
				ImageEditorToolGroup imageEditorToolGroup = this.Tools[i];
				radToolBar = new RadToolBar();
				radToolBar.EnableImageSprites = true;
				if (base.IsSkinSet)
				{
					radToolBar.Skin = this.Skin;
				}
				if (this.ViewState["EnableEmbeddedSkins"] != null)
				{
					radToolBar.EnableEmbeddedSkins = this.EnableEmbeddedSkins;
				}
				if (this.ViewState["EnableEmbeddedBaseStylesheet"] != null)
				{
					radToolBar.EnableEmbeddedBaseStylesheet = this.EnableEmbeddedBaseStylesheet;
				}
				radToolBar.EnableViewState = false;
				if (this.ResolvedRenderMode.Equals(RenderMode.Lightweight))
				{
					switch (this.ToolBarPosition)
					{
					case ToolBarPosition.Right:
					case ToolBarPosition.Left:
						radToolBar.Orientation = Orientation.Vertical;
						break;
					}
				}
				radToolBar.CausesValidation = false;
				radToolBar.ID = string.Format("ToolGroup{0}", i);
				control.Controls.Add(radToolBar);
				foreach (object obj in imageEditorToolGroup.Tools)
				{
					ImageEditorToolBase imageEditorToolBase = (ImageEditorToolBase)obj;
					if (imageEditorToolBase.IsSeparator)
					{
						RadToolBarButton radToolBarButton = new RadToolBarButton();
						radToolBarButton.IsSeparator = true;
						radToolBar.Items.Add(radToolBarButton);
					}
					else
					{
						ImageEditorTool imageEditorTool = imageEditorToolBase as ImageEditorTool;
						if (imageEditorTool != null)
						{
							RadToolBarButton radToolBarButton2 = this.CreateToolbarButton(imageEditorTool);
							radToolBarButton2.ShowText = ToolBarShowPosition.OverFlow;
							if (this.ResolvedRenderMode.Equals(RenderMode.Classic))
							{
								RadToolBarButton radToolBarButton3 = radToolBarButton2;
								radToolBarButton3.CssClass += " rtbIconOnly";
							}
							radToolBar.Items.Add(radToolBarButton2);
						}
						else
						{
							ImageEditorToolStrip toolStrip = imageEditorToolBase as ImageEditorToolStrip;
							RadToolBarSplitButton item = this.CreateToolbarSplitButton(toolStrip);
							radToolBar.Items.Add(item);
						}
					}
				}
			}
			if (this.Tools.Count == 1 && radToolBar != null && this.ToolBarMode != ToolBarMode.Docked)
			{
				radToolBar.Width = Unit.Percentage(100.0);
			}
			this.SetControlRenderMode(radToolBar);
		}

		// Token: 0x060070C7 RID: 28871 RVA: 0x001A6954 File Offset: 0x001A4B54
		private RadToolBarButton CreateToolbarButton(ImageEditorTool tool)
		{
			RadToolBarButton radToolBarButton = new RadToolBarButton();
			radToolBarButton.Enabled = tool.Enabled;
			radToolBarButton.Text = this.GetText(tool.CommandName, tool.Text);
			radToolBarButton.ToolTip = this.GetToolTip(tool.ToolTip, radToolBarButton.Text);
			radToolBarButton.CommandName = tool.CommandName;
			radToolBarButton.CausesValidation = false;
			if (!this.IsBuiltInCommand(radToolBarButton.CommandName))
			{
				radToolBarButton.CssClass = "rieCustomizedIconClass";
			}
			radToolBarButton.SpriteCssClass = string.Format("rie{0} rieIcon", tool.CommandName);
			RadToolBarButton radToolBarButton2 = radToolBarButton;
			radToolBarButton2.CssClass = radToolBarButton2.CssClass + " " + tool.CssClass;
			radToolBarButton.ImageUrl = tool.ImageUrl;
			radToolBarButton.CheckOnClick = tool.IsToggleButton;
			this.DisableOpacityForUnsupportedFormats(radToolBarButton, radToolBarButton.CommandName);
			return radToolBarButton;
		}

		// Token: 0x060070C8 RID: 28872 RVA: 0x001A6A2C File Offset: 0x001A4C2C
		private RadToolBarSplitButton CreateToolbarSplitButton(ImageEditorToolStrip toolStrip)
		{
			RadToolBarSplitButton radToolBarSplitButton = new RadToolBarSplitButton();
			radToolBarSplitButton.ShowText = ToolBarShowPosition.OverFlow;
			if (this.ResolvedRenderMode.Equals(RenderMode.Classic))
			{
				RadToolBarSplitButton radToolBarSplitButton2 = radToolBarSplitButton;
				radToolBarSplitButton2.CssClass += " rtbIconOnly";
			}
			radToolBarSplitButton.Enabled = toolStrip.Enabled;
			radToolBarSplitButton.Text = this.GetText(toolStrip.CommandName, toolStrip.Text);
			radToolBarSplitButton.ToolTip = this.GetToolTip(toolStrip.ToolTip, radToolBarSplitButton.Text);
			radToolBarSplitButton.CommandName = toolStrip.CommandName;
			radToolBarSplitButton.CausesValidation = false;
			if (!this.IsBuiltInCommand(radToolBarSplitButton.CommandName))
			{
				radToolBarSplitButton.CssClass = "rieCustomizedIconClass";
			}
			radToolBarSplitButton.SpriteCssClass = string.Format("rie{0} rieIcon", toolStrip.CommandName);
			RadToolBarSplitButton radToolBarSplitButton3 = radToolBarSplitButton;
			radToolBarSplitButton3.CssClass = radToolBarSplitButton3.CssClass + " " + toolStrip.CssClass;
			radToolBarSplitButton.ImageUrl = toolStrip.ImageUrl;
			radToolBarSplitButton.EnableDefaultButton = toolStrip.EnableDefaultTool;
			foreach (object obj in toolStrip.Tools)
			{
				ImageEditorTool tool = (ImageEditorTool)obj;
				radToolBarSplitButton.Buttons.Add(this.CreateToolbarButton(tool));
			}
			if (toolStrip.CommandName == "Undo" || toolStrip.CommandName == "Redo")
			{
				radToolBarSplitButton.DropDownWidth = Unit.Pixel(200);
				radToolBarSplitButton.DropDownHeight = Unit.Pixel(222);
			}
			this.DisableOpacityForUnsupportedFormats(radToolBarSplitButton, radToolBarSplitButton.CommandName);
			return radToolBarSplitButton;
		}

		// Token: 0x060070C9 RID: 28873 RVA: 0x001A6BD0 File Offset: 0x001A4DD0
		private void DisableOpacityForUnsupportedFormats(RadToolBarItem button, string commandName)
		{
			try
			{
				bool flag = commandName == "Opacity" && this.GetCurrentEditableImage().Format == "jpg";
				button.Enabled = !flag;
				if (flag)
				{
					button.ToolTip = this.Localization.Main.OpacityNotSupported;
				}
			}
			catch
			{
			}
		}

		// Token: 0x060070CA RID: 28874 RVA: 0x001A6C3C File Offset: 0x001A4E3C
		private string GetRuntimeSkin(bool forceCalculation)
		{
			if (forceCalculation || string.IsNullOrEmpty(this._skinApplied))
			{
				RadSkinManager current = RadSkinManager.GetCurrent(this.Page);
				this._skinApplied = ((!base.IsSkinSet && current != null && !string.IsNullOrEmpty(current.Skin)) ? current.Skin : base.RuntimeSkin);
			}
			return this._skinApplied;
		}

		// Token: 0x060070CB RID: 28875 RVA: 0x001A6C98 File Offset: 0x001A4E98
		private string GetWebResourceUrl(string skin)
		{
			if (string.IsNullOrEmpty(this._webResourceUrl))
			{
				HttpBrowserCapabilities browser = this.Context.Request.Browser;
				string text = (browser.Type == "IE6" && browser.Version[0] == '6') ? "rieTools{0}IE6.png" : "rieTools{0}.png";
				text = string.Format(text, this.GetSkinShade(skin));
				string resourceName = string.Format("Telerik.Web.UI.Skins.Common.ImageEditor.{0}", text);
				this._webResourceUrl = SkinRegistrar.GetWebResourceUrl(this.Page, typeof(RadImageEditor), resourceName);
			}
			return this._webResourceUrl;
		}

		// Token: 0x060070CC RID: 28876 RVA: 0x001A6D2E File Offset: 0x001A4F2E
		private string GetSkinShade(string skinName)
		{
			if (!this._darkSkins.Contains(skinName))
			{
				return "Light";
			}
			return "Dark";
		}

		// Token: 0x060070CD RID: 28877 RVA: 0x001A6D49 File Offset: 0x001A4F49
		protected virtual string GetToolImageUrl(string imageUrl)
		{
			if (this.EnableEmbeddedSkins && string.IsNullOrEmpty(imageUrl))
			{
				return this.GetWebResourceUrl(this.GetRuntimeSkin(false));
			}
			return imageUrl;
		}

		// Token: 0x060070CE RID: 28878 RVA: 0x001A6D6A File Offset: 0x001A4F6A
		protected virtual string GetToolImageUrl(string imageUrl, bool isBuiltInCommand)
		{
			if (!string.IsNullOrEmpty(imageUrl))
			{
				return imageUrl;
			}
			if (base.DesignMode)
			{
				return string.Empty;
			}
			if (!isBuiltInCommand)
			{
				return string.Empty;
			}
			if (this.EnableEmbeddedSkins)
			{
				return this.GetWebResourceUrl(this.GetRuntimeSkin(false));
			}
			return string.Empty;
		}

		// Token: 0x060070CF RID: 28879 RVA: 0x001A6DA8 File Offset: 0x001A4FA8
		private string GetText(string commandName, string toolText)
		{
			string text;
			if (string.IsNullOrEmpty(toolText))
			{
				text = this.Localization.Main.GetString(commandName);
				if (string.IsNullOrEmpty(text))
				{
					text = commandName;
				}
			}
			else
			{
				text = toolText;
			}
			return text;
		}

		// Token: 0x060070D0 RID: 28880 RVA: 0x001A6DDE File Offset: 0x001A4FDE
		private string GetToolTip(string toolTip, string text)
		{
			if (!string.IsNullOrEmpty(toolTip))
			{
				return toolTip;
			}
			return text;
		}

		// Token: 0x060070D1 RID: 28881 RVA: 0x001A6DEC File Offset: 0x001A4FEC
		private void CreateToolsPanel()
		{
			this._dockToolsPanel = new RadDock();
			this._dockToolsPanel.ID = "ToolsPanel";
			this._dockToolsPanel.Title = "Tool Dialog";
			this._dockToolsPanel.CssClass = "rieDialogs";
			this._dockToolsPanel.EnableRoundedCorners = true;
			this._dockToolsPanel.Commands.Add(new DockCloseCommand());
			this._dockToolsPanel.Closed = true;
			if (this.ResolvedRenderMode.Equals(RenderMode.Classic))
			{
				this._dockToolsPanel.Width = Unit.Pixel(270);
			}
			this._dockToolsPanel.Closed = true;
			this._dockToolsPanel.EnableViewState = false;
			this.Controls.Add(this._dockToolsPanel);
		}

		// Token: 0x060070D2 RID: 28882 RVA: 0x001A6EB8 File Offset: 0x001A50B8
		private void CreateXmlHttpPanel()
		{
			this._toolsXHPanel = new RadXmlHttpPanel();
			this._toolsXHPanel.ID = "XHPanel";
			this._toolsXHPanel.RenderMode = XmlHttpPanelRenderMode.Block;
			this._toolsXHPanel.EnableClientScriptEvaluation = true;
			this._toolsXHPanel.EnableViewState = false;
			this._toolsXHPanel.Visible = true;
			this._toolsXHPanel.ServiceRequest += this.LoadToolCallback;
			this._dockToolsPanel.ContentContainer.Controls.Add(this._toolsXHPanel);
			this.SetDialogContainer(this._toolsXHPanel, ToolsLoadPanelTypes.XmlHttpPanel);
		}

		// Token: 0x060070D3 RID: 28883 RVA: 0x001A6F50 File Offset: 0x001A5150
		private void CreateEditableImageXmlHttpPanel()
		{
			this._editableImageXHPanel = new RadXmlHttpPanel
			{
				ID = "eiXHPanel",
				RenderMode = XmlHttpPanelRenderMode.Inline,
				EnableClientScriptEvaluation = false,
				EnableViewState = false,
				MaxJsonLength = this.EditableImageSettings.MaxJsonLength
			};
			this._editableImageXHPanel.ServiceRequest += this.LoadImageCallback;
			this.Controls.Add(this._editableImageXHPanel);
		}

		// Token: 0x060070D4 RID: 28884 RVA: 0x001A6FC4 File Offset: 0x001A51C4
		private void CreateFormDecorator()
		{
			this._formDecorator = new RadFormDecorator();
			this._formDecorator.ID = "FormDecorator";
			this._formDecorator.DecoratedControls = FormDecoratorDecoratedControls.Textbox;
			this._formDecorator.DecoratedControls |= FormDecoratorDecoratedControls.Textarea;
			this._formDecorator.DecoratedControls |= FormDecoratorDecoratedControls.Select;
			this._formDecorator.DecoratedControls |= FormDecoratorDecoratedControls.RadioButtons;
			this._formDecorator.DecoratedControls |= FormDecoratorDecoratedControls.CheckBoxes;
			this._formDecorator.DecoratedControls |= FormDecoratorDecoratedControls.Label;
			this._formDecorator.DecorationZoneID = this.GetToolsDecoratingZone();
			this._formDecorator.EnableViewState = false;
			this.Controls.Add(this._formDecorator);
		}

		// Token: 0x060070D5 RID: 28885 RVA: 0x001A708F File Offset: 0x001A528F
		private string GetToolsDecoratingZone()
		{
			return this._dialogContainer.ClientID;
		}

		// Token: 0x060070D6 RID: 28886 RVA: 0x001A709C File Offset: 0x001A529C
		private void CreateAjaxPanelControls()
		{
			if (this._toolsAjaxPanel != null)
			{
				return;
			}
			this._toolsAjaxPanel = new UpdatePanel();
			this._toolsAjaxPanel.ID = "AjaxPanel";
			this._toolsAjaxPanel.RenderMode = UpdatePanelRenderMode.Block;
			this._toolsAjaxPanel.UpdateMode = UpdatePanelUpdateMode.Conditional;
			this._toolsAjaxPanel.Visible = true;
			this._dockToolsPanel.ContentContainer.Controls.Add(this._toolsAjaxPanel);
			this._postbackButton = new Button();
			this._postbackButton.ID = "pb";
			this._postbackButton.Text = "pb";
			this._postbackButton.UseSubmitBehavior = false;
			this._postbackButton.TabIndex = -1;
			this._postbackButton.CausesValidation = false;
			this._postbackButton.CssClass = "do_not_decorate_class";
			this._postbackButton.Style.Add(HtmlTextWriterStyle.Display, "none");
			this._postbackButton.Click += this.LoadTool;
			this._toolsAjaxPanel.ContentTemplateContainer.Controls.Add(this._postbackButton);
			Panel panel = new Panel
			{
				ID = "dialogContainer"
			};
			this._toolsAjaxPanel.ContentTemplateContainer.Controls.Add(panel);
			this.SetDialogContainer(panel, ToolsLoadPanelTypes.AjaxPanel);
			AsyncPostBackTrigger asyncPostBackTrigger = new AsyncPostBackTrigger();
			asyncPostBackTrigger.ControlID = this._postbackButton.UniqueID;
			asyncPostBackTrigger.EventName = "click";
			this._toolsAjaxPanel.Triggers.Add(asyncPostBackTrigger);
		}

		// Token: 0x060070D7 RID: 28887 RVA: 0x001A7218 File Offset: 0x001A5418
		private void CreateRadAjaxPanel()
		{
			if (this._radAjaxPanel != null)
			{
				return;
			}
			this._radAjaxPanel = new RadAjaxPanel
			{
				ID = "RadAjaxPanel",
				RenderMode = UpdatePanelRenderMode.Block,
				Visible = true,
				RequestQueueSize = 20,
				EnablePageHeadUpdate = false
			};
			this._radAjaxPanel.AjaxRequest -= this.LoadToolRadAjaxPanel;
			this._radAjaxPanel.AjaxRequest += this.LoadToolRadAjaxPanel;
			this.SetDialogContainer(this._radAjaxPanel, ToolsLoadPanelTypes.RadAjaxPanel);
			this._dockToolsPanel.ContentContainer.Controls.Add(this._radAjaxPanel);
		}

		// Token: 0x060070D8 RID: 28888 RVA: 0x001A72BC File Offset: 0x001A54BC
		private void ConfigureVisibilityOfPanels()
		{
			if (this._radAjaxPanel != null)
			{
				this._radAjaxPanel.Visible = false;
			}
			if (this._toolsAjaxPanel != null)
			{
				this._toolsAjaxPanel.Visible = false;
			}
			this._toolsXHPanel.Visible = false;
			switch (this.ToolsLoadPanelType)
			{
			case ToolsLoadPanelTypes.AjaxPanel:
				this._toolsAjaxPanel.Visible = true;
				return;
			case ToolsLoadPanelTypes.XmlHttpPanel:
				this._toolsXHPanel.Visible = true;
				return;
			case ToolsLoadPanelTypes.RadAjaxPanel:
				this._radAjaxPanel.Visible = true;
				return;
			default:
				return;
			}
		}

		// Token: 0x060070D9 RID: 28889 RVA: 0x001A733D File Offset: 0x001A553D
		private void SetDialogContainer(WebControl container, ToolsLoadPanelTypes containerType)
		{
			if (this.ToolsLoadPanelType == containerType)
			{
				this._dialogContainer = container;
			}
		}

		// Token: 0x060070DA RID: 28890 RVA: 0x001A734F File Offset: 0x001A554F
		internal virtual void UpdateEditableImageHttpPanel_MaxJsonLength()
		{
			if (this._editableImageXHPanel != null)
			{
				this._editableImageXHPanel.MaxJsonLength = this.EditableImageSettings.MaxJsonLength;
			}
		}

		// Token: 0x060070DB RID: 28891 RVA: 0x001A736F File Offset: 0x001A556F
		private void CreateAjaxLoadingPanel()
		{
			this._ajaxLoadingPanel = new RadAjaxLoadingPanel();
			this._ajaxLoadingPanel.ID = "RALP";
			this._ajaxLoadingPanel.EnableViewState = false;
			this.Controls.Add(this._ajaxLoadingPanel);
		}

		// Token: 0x060070DC RID: 28892 RVA: 0x001A73A9 File Offset: 0x001A55A9
		private void CreateDockLayout()
		{
			this._dockLayout = new RadDockLayout();
			this._dockLayout.ID = "DockLayout";
			this._dockLayout.EnableViewState = false;
			this.Controls.Add(this._dockLayout);
		}

		// Token: 0x060070DD RID: 28893 RVA: 0x001A73E4 File Offset: 0x001A55E4
		private void CreateToolbarDock()
		{
			this._dockToolBar = new RadDock();
			this._dockToolBar.UniqueName = this.UniqueID + "_ToolbarPanel";
			this._dockToolBar.ID = "ToolbarPanel";
			Panel contentContainer = this._dockToolBar.ContentContainer;
			contentContainer.CssClass += " RadImageEditor";
			this._dockToolBar.DockHandle = DockHandle.Grip;
			this._dockToolBar.Width = Unit.Pixel(600);
			this._dockToolBar.EnableViewState = false;
			this._dockToolBar.CssClass = "rieToolbarDock";
			this._dockToolBar.EnableRoundedCorners = true;
			this._dockLayout.Controls.Add(this._dockToolBar);
		}

		// Token: 0x060070DE RID: 28894 RVA: 0x001A74A8 File Offset: 0x001A56A8
		private void CreateTopLeftZones()
		{
			this._zoneTop = new RadDockZone();
			this._zoneTop.ID = "ZoneTop";
			this._zoneTop.CssClass = "rieToolBar rieZoneTop";
			this._zoneTop.EnableViewState = false;
			this._zoneTop.HighlightedCssClass = "rieHighlightHorizontal";
			this._zoneTop.MinHeight = Unit.Pixel(3);
			this._zoneTop.AllowedDocks = new string[]
			{
				this._dockToolBar.UniqueName
			};
			this._zoneLeft = new RadDockZone();
			this._zoneLeft.ID = "ZoneLeft";
			this._zoneLeft.CssClass = "rieToolBar rieZoneLeft";
			this._zoneLeft.EnableViewState = false;
			this._zoneLeft.HighlightedCssClass = "rieHighlightVertical";
			this._zoneLeft.AllowedDocks = this._zoneTop.AllowedDocks;
			this._dockLayout.Controls.Add(this._zoneTop);
			this._dockLayout.Controls.Add(this._zoneLeft);
		}

		// Token: 0x060070DF RID: 28895 RVA: 0x001A75B8 File Offset: 0x001A57B8
		private void CreateRightBottomZones()
		{
			this._zoneRight = new RadDockZone();
			this._zoneRight.ID = "ZoneRight";
			this._zoneRight.CssClass = "rieToolBar rieZoneRight";
			this._zoneRight.EnableViewState = false;
			this._zoneRight.HighlightedCssClass = "rieHighlightVertical";
			this._zoneRight.AllowedDocks = new string[]
			{
				this._dockToolBar.UniqueName
			};
			this._zoneBottom = new RadDockZone();
			this._zoneBottom.ID = "ZoneBottom";
			this._zoneBottom.CssClass = "rieToolBar rieZoneBottom";
			this._zoneBottom.EnableViewState = false;
			this._zoneBottom.HighlightedCssClass = "rieHighlightHorizontal";
			this._zoneBottom.MinHeight = Unit.Pixel(3);
			this._zoneBottom.AllowedDocks = this._zoneRight.AllowedDocks;
			this._dockLayout.Controls.Add(this._zoneRight);
			this._dockLayout.Controls.Add(this._zoneBottom);
		}

		// Token: 0x060070E0 RID: 28896 RVA: 0x001A76C7 File Offset: 0x001A58C7
		private void LoadToolRadAjaxPanel(object sender, AjaxRequestEventArgs e)
		{
			this._radAjaxPanel.Visible = true;
			this.LoadDialog(e.Argument);
			this.UndoStack.Clear();
		}

		// Token: 0x060070E1 RID: 28897 RVA: 0x001A76EC File Offset: 0x001A58EC
		private void LoadTool(object sender, EventArgs e)
		{
			string dialogName = HttpContext.Current.Request.Form["__EVENTARGUMENT"];
			this.LoadDialog(dialogName);
			this.UndoStack.Clear();
		}

		// Token: 0x060070E2 RID: 28898 RVA: 0x001A7725 File Offset: 0x001A5925
		private void LoadToolCallback(object sender, RadXmlHttpPanelEventArgs e)
		{
			this._toolsXHPanel.Visible = true;
			this.LoadDialog(e.Value);
		}

		// Token: 0x060070E3 RID: 28899 RVA: 0x001A773F File Offset: 0x001A593F
		private void LoadDialog(string dialogName)
		{
			this._dialogContainer.Controls.Add(this.CreateDialog(dialogName));
			this.OnDialogLoading(dialogName, this._dialogContainer);
		}

		// Token: 0x060070E4 RID: 28900 RVA: 0x001A7765 File Offset: 0x001A5965
		private Control CreateDialog(string dialogName)
		{
			return this.CreateToolWidget(dialogName);
		}

		// Token: 0x060070E5 RID: 28901 RVA: 0x001A7770 File Offset: 0x001A5970
		private ImageEditorDialog CreateToolWidget(string type)
		{
			string runtimeSkin = base.RuntimeSkin;
			ImageEditorDialog imageEditorDialog;
			switch (type)
			{
			case "Opacity":
				imageEditorDialog = new Opacity(runtimeSkin, this);
				goto IL_221;
			case "Flip":
				imageEditorDialog = new Flip(runtimeSkin, this);
				goto IL_221;
			case "Rotate":
				imageEditorDialog = new Rotate(runtimeSkin, this);
				goto IL_221;
			case "Resize":
				imageEditorDialog = new Resize(runtimeSkin, this);
				goto IL_221;
			case "Zoom":
				imageEditorDialog = new Zoom(runtimeSkin, this);
				goto IL_221;
			case "Crop":
				imageEditorDialog = new Crop(runtimeSkin, this);
				goto IL_221;
			case "AddText":
				imageEditorDialog = new AddText(runtimeSkin, this);
				goto IL_221;
			case "Save":
				imageEditorDialog = new Save(runtimeSkin, this);
				goto IL_221;
			case "Export":
				imageEditorDialog = new Export(runtimeSkin, this);
				goto IL_221;
			case "Print":
				imageEditorDialog = new Print(runtimeSkin, this);
				goto IL_221;
			case "InsertImage":
				imageEditorDialog = new InsertImage(runtimeSkin, this);
				goto IL_221;
			case "BrightnessContrast":
				imageEditorDialog = new BrightnessContrast(runtimeSkin, this);
				goto IL_221;
			case "HueSaturation":
				imageEditorDialog = new HueSaturation(runtimeSkin, this);
				goto IL_221;
			case "Pencil":
				imageEditorDialog = new Pencil(runtimeSkin, this);
				goto IL_221;
			case "Line":
				imageEditorDialog = new Line(runtimeSkin, this);
				goto IL_221;
			case "DrawRectangle":
				imageEditorDialog = new DrawRectangle(runtimeSkin, this);
				goto IL_221;
			case "DrawCircle":
				imageEditorDialog = new DrawCircle(runtimeSkin, this);
				goto IL_221;
			}
			imageEditorDialog = new EmptyTool(runtimeSkin, this);
			IL_221:
			imageEditorDialog.EnableViewState = false;
			imageEditorDialog.ID = imageEditorDialog.DialogName;
			return imageEditorDialog;
		}

		// Token: 0x060070E6 RID: 28902 RVA: 0x001A79B4 File Offset: 0x001A5BB4
		private void LoadImageCallback(object sender, RadXmlHttpPanelEventArgs e)
		{
			try
			{
				Dictionary<string, object> data = this.ReadDataFromXmlPanel(e.Value);
				EditableImage image = this.ApplyServerOperation(data);
				this.OnImageChanged(image);
			}
			catch (NotSupportedException ex)
			{
				this._editableImageXHPanel.Controls.Add(new Literal
				{
					Text = ex.Message
				});
			}
			catch
			{
			}
		}

		// Token: 0x060070E7 RID: 28903 RVA: 0x001A7A24 File Offset: 0x001A5C24
		private Dictionary<string, object> ReadDataFromXmlPanel(string value)
		{
			JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer
			{
				MaxJsonLength = this.EditableImageSettings.MaxJsonLength
			};
			object obj = javaScriptSerializer.DeserializeObject(value);
			return obj as Dictionary<string, object>;
		}

		// Token: 0x060070E8 RID: 28904 RVA: 0x001A7A58 File Offset: 0x001A5C58
		private string SerializeDataToJson(object data)
		{
			return new JavaScriptSerializer().Serialize(data);
		}

		// Token: 0x060070E9 RID: 28905 RVA: 0x001A7A68 File Offset: 0x001A5C68
		private string GetCacheImageHandlerUrl(string key)
		{
			string text = this.HttpHandlerUrl;
			text += (text.Contains("?") ? "&" : "?");
			text += string.Format("{0}={1}&pr={2}&key={3}", new object[]
			{
				HandlerRouter.HandlerUrlKey,
				RadImageEditor.HandlerRouterKey,
				this.GetShortProvider(),
				key
			});
			if (!string.IsNullOrEmpty(this.ImageManager.ImageProviderTypeName))
			{
				text += string.Format("&prtype={0}", this.ImageManager.ImageProviderTypeName);
			}
			return text;
		}

		// Token: 0x060070EA RID: 28906 RVA: 0x001A7B04 File Offset: 0x001A5D04
		private string GetShortProvider()
		{
			switch (this.ImageCacheStorageLocation)
			{
			case ImageStorage.Cache:
				return "c";
			case ImageStorage.Session:
				return "s";
			case ImageStorage.FileSystem:
				throw new NotImplementedException();
			default:
				throw new NotSupportedException();
			}
		}

		// Token: 0x060070EB RID: 28907 RVA: 0x001A7B44 File Offset: 0x001A5D44
		protected virtual void HandleMetroTouchSkin(bool isMetroTouch)
		{
			if (isMetroTouch)
			{
				this._dockToolsPanel.EnableRoundedCorners = false;
				if (this.ResolvedRenderMode.Equals(RenderMode.Classic))
				{
					this._dockToolsPanel.Width = Unit.Pixel(360);
				}
				this._dockToolBar.EnableRoundedCorners = false;
				return;
			}
			if (base.RuntimeSkin == "Bootstrap")
			{
				this._dockToolsPanel.EnableRoundedCorners = false;
				if (this.ResolvedRenderMode.Equals(RenderMode.Classic))
				{
					this._dockToolsPanel.Width = Unit.Pixel(310);
				}
				this._dockToolBar.EnableRoundedCorners = false;
				return;
			}
			this._dockToolsPanel.EnableRoundedCorners = true;
			if (this.ResolvedRenderMode.Equals(RenderMode.Classic))
			{
				this._dockToolsPanel.Width = Unit.Pixel(270);
			}
			this._dockToolBar.EnableRoundedCorners = true;
		}

		// Token: 0x060070EC RID: 28908 RVA: 0x001A7C37 File Offset: 0x001A5E37
		public virtual void LoadToolsFile(XmlDocument doc)
		{
			this._toolsFileContent = doc;
			this.EnsureToolsFileLoaded();
		}

		// Token: 0x060070ED RID: 28909 RVA: 0x001A7C46 File Offset: 0x001A5E46
		private void ResetToolsFileContent()
		{
			this._toolsFileContent = null;
			this._toolsFileLoaded = false;
		}

		// Token: 0x060070EE RID: 28910 RVA: 0x001A7C56 File Offset: 0x001A5E56
		public virtual void EnsureToolsFileLoaded()
		{
			if (!this._toolsFileLoaded)
			{
				this.LoadToolsFile(false);
			}
		}

		// Token: 0x060070EF RID: 28911 RVA: 0x001A7C67 File Offset: 0x001A5E67
		protected virtual void LoadToolsFile(bool loadOnlyEmptyCollections)
		{
			if (loadOnlyEmptyCollections && this.Tools.Count > 0)
			{
				return;
			}
			if (!loadOnlyEmptyCollections || (loadOnlyEmptyCollections && this.Tools.Count == 0))
			{
				this.ToolsFileLoader.LoadTools(this.Tools);
			}
			this._toolsFileLoaded = true;
		}

		// Token: 0x060070F0 RID: 28912 RVA: 0x001A7CA8 File Offset: 0x001A5EA8
		private string GetXmlFilePath(string path)
		{
			if (path.StartsWith("http://") || path.StartsWith("https://"))
			{
				return path;
			}
			if (this.Context == null)
			{
				return "";
			}
			string text = this.Context.Request.MapPath(path);
			if (File.Exists(text))
			{
				return text;
			}
			return path;
		}

		// Token: 0x060070F1 RID: 28913 RVA: 0x001A7CFC File Offset: 0x001A5EFC
		protected virtual string GetAppropriateToolsFilePath()
		{
			if (this.CanvasMode != CanvasMode.No)
			{
				return "Telerik.Web.UI.ImageEditor.Resources.CanvasTools.xml";
			}
			return "Telerik.Web.UI.ImageEditor.Resources.ToolsFile.xml";
		}

		// Token: 0x060070F2 RID: 28914 RVA: 0x001A7D14 File Offset: 0x001A5F14
		private string SerializeShortCuts()
		{
			StringBuilder stringBuilder = new StringBuilder();
			bool flag = false;
			stringBuilder.Append("[");
			foreach (object obj in this.Tools)
			{
				ImageEditorToolGroup imageEditorToolGroup = (ImageEditorToolGroup)obj;
				List<ImageEditorToolBase> allTools = imageEditorToolGroup.GetAllTools();
				foreach (ImageEditorToolBase imageEditorToolBase in allTools)
				{
					ImageEditorTool imageEditorTool = imageEditorToolBase as ImageEditorTool;
					if (imageEditorTool != null && !string.IsNullOrEmpty(imageEditorTool.ShortCut))
					{
						if (flag)
						{
							stringBuilder.Append(",");
						}
						stringBuilder.Append(string.Format("[\"{0}\",\"{1}\"]", imageEditorTool.CommandName, imageEditorTool.ShortCut));
						flag = true;
					}
					else
					{
						ImageEditorToolStrip imageEditorToolStrip = imageEditorToolBase as ImageEditorToolStrip;
						if (imageEditorToolStrip != null && !string.IsNullOrEmpty(imageEditorToolStrip.ShortCut))
						{
							if (flag)
							{
								stringBuilder.Append(",");
							}
							stringBuilder.Append(string.Format("[\"{0}\",\"{1}\"]", imageEditorToolStrip.CommandName, imageEditorToolStrip.ShortCut));
							flag = true;
						}
					}
				}
			}
			stringBuilder.Append("]");
			return stringBuilder.ToString();
		}

		// Token: 0x060070F3 RID: 28915 RVA: 0x001A7E78 File Offset: 0x001A6078
		protected override void LoadViewState(object state)
		{
			object[] array = (object[])state;
			base.LoadViewState(array[0]);
			if (array[1] != null)
			{
				((IStateManager)this.Tools).LoadViewState(array[1]);
			}
			if (array[2] != null)
			{
				((IStateManager)this.Localization).LoadViewState(array[2]);
			}
			if (array[3] != null)
			{
				((IStateManager)this.ImageManager).LoadViewState(array[3]);
			}
		}

		// Token: 0x060070F4 RID: 28916 RVA: 0x001A7ED0 File Offset: 0x001A60D0
		protected override object SaveViewState()
		{
			return new object[]
			{
				base.SaveViewState(),
				RadImageEditor.SaveState(this._tools),
				RadImageEditor.SaveState(this._localization),
				RadImageEditor.SaveState(this._imageManager)
			};
		}

		// Token: 0x060070F5 RID: 28917 RVA: 0x001A7F1A File Offset: 0x001A611A
		protected override void TrackViewState()
		{
			base.TrackViewState();
			RadImageEditor.TrackState(this._tools);
			RadImageEditor.TrackState(this._localization);
			RadImageEditor.TrackState(this._imageManager);
		}

		// Token: 0x060070F6 RID: 28918 RVA: 0x001A7F43 File Offset: 0x001A6143
		private static void TrackState(IStateManager obj)
		{
			if (obj != null)
			{
				obj.TrackViewState();
			}
		}

		// Token: 0x060070F7 RID: 28919 RVA: 0x001A7F4E File Offset: 0x001A614E
		private static object SaveState(IStateManager obj)
		{
			if (obj != null)
			{
				return obj.SaveViewState();
			}
			return null;
		}

		// Token: 0x060070F8 RID: 28920 RVA: 0x001A7F5C File Offset: 0x001A615C
		protected override void LoadControlState(object savedState)
		{
			object[] array = savedState as object[];
			if (array != null && array.Length == 3 && array[1] is string)
			{
				base.LoadControlState(array[0]);
				this._imageStorageKey = (string)array[1];
				this.CurrentImageKey = (string)array[2];
				return;
			}
			base.LoadControlState(savedState);
		}

		// Token: 0x060070F9 RID: 28921 RVA: 0x001A7FB0 File Offset: 0x001A61B0
		protected override object SaveControlState()
		{
			if (string.IsNullOrEmpty(this._imageStorageKey))
			{
				this._imageStorageKey = Guid.NewGuid().ToString();
			}
			if (string.IsNullOrEmpty(this._imageStorageKey))
			{
				return base.SaveControlState();
			}
			return new object[]
			{
				base.SaveControlState(),
				this._imageStorageKey,
				this.CurrentImageKey
			};
		}

		// Token: 0x060070FA RID: 28922 RVA: 0x001A801C File Offset: 0x001A621C
		protected virtual void InitContentProvider()
		{
			string text = (!string.IsNullOrEmpty(this.ImageUrl)) ? this.ImageUrl : "/";
			if (this.ImageManager.ViewPaths.Length >= 1 && text == "/")
			{
				text = this.ImageManager.ViewPaths[0];
			}
			this.InitContentProvider(text);
		}

		// Token: 0x060070FB RID: 28923 RVA: 0x001A8078 File Offset: 0x001A6278
		protected virtual void InitContentProvider(string selectedUrl)
		{
			this._contentProvider = (FileBrowserContentProvider)Activator.CreateInstance(this.ImageManager.FileBrowserContentProviderType, new object[]
			{
				this.Context,
				this.ImageManager.SearchPatterns,
				this.ImageManager.ViewPaths,
				this.ImageManager.UploadPaths,
				this.ImageManager.DeletePaths,
				selectedUrl,
				selectedUrl
			});
		}

		// Token: 0x170024D1 RID: 9425
		// (get) Token: 0x060070FC RID: 28924 RVA: 0x001A80F1 File Offset: 0x001A62F1
		// (set) Token: 0x060070FD RID: 28925 RVA: 0x001A8107 File Offset: 0x001A6307
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

		// Token: 0x060070FE RID: 28926 RVA: 0x001A8110 File Offset: 0x001A6310
		public EditableImage ApplyImageOperations(IEnumerable<IImageOperation> operations)
		{
			EditableImage editableImage = this.ApplyImageOperations(operations, this.GetCurrentEditableImage());
			if (editableImage != null)
			{
				this.StoreEditableImage(editableImage);
			}
			return editableImage;
		}

		// Token: 0x060070FF RID: 28927 RVA: 0x001A8137 File Offset: 0x001A6337
		public EditableImage ApplyImageOperations(IEnumerable<IImageOperation> operations, EditableImage editableImage)
		{
			if (editableImage == null)
			{
				return null;
			}
			editableImage.ApplyImageOperations(operations);
			return editableImage;
		}

		// Token: 0x06007100 RID: 28928 RVA: 0x001A8148 File Offset: 0x001A6348
		public string ExtractFileNameFromImageUrl()
		{
			char value = this.ImageManager.EnableContentProvider ? this.ContentProvider.PathSeparator : '/';
			string text = this.ImageUrl.Substring(this.ImageUrl.LastIndexOf(value) + 1);
			int num = text.LastIndexOf('.');
			if (num != -1)
			{
				text = text.Substring(0, num);
			}
			return text;
		}

		// Token: 0x06007101 RID: 28929 RVA: 0x001A81A4 File Offset: 0x001A63A4
		public string GetImageFormatName()
		{
			string result;
			try
			{
				EditableImage currentEditableImage = this.GetCurrentEditableImage();
				result = ((currentEditableImage == null) ? "" : currentEditableImage.Format);
			}
			catch
			{
				result = null;
			}
			return result;
		}

		// Token: 0x06007102 RID: 28930 RVA: 0x001A81E4 File Offset: 0x001A63E4
		public string SaveEditableImage(string imageName, bool overwrite)
		{
			EditableImage editableImage = this.ApplyClientOperations();
			if (editableImage == null)
			{
				editableImage = this.GetCurrentEditableImage();
			}
			string result = this.SaveEditableImage(editableImage, imageName, overwrite);
			this.StoreEditableImage(editableImage);
			return result;
		}

		// Token: 0x06007103 RID: 28931 RVA: 0x001A8215 File Offset: 0x001A6415
		public string SaveEditableImage(EditableImage editableImage, string imageName, bool overwrite)
		{
			return this.SaveEditableImage(editableImage, this.GetCacheImageProvider(), imageName, overwrite);
		}

		// Token: 0x06007104 RID: 28932 RVA: 0x001A8228 File Offset: 0x001A6428
		public void ResetChanges()
		{
			ICacheImageProvider cacheImageProvider = this.GetCacheImageProvider();
			this.UndoStack.Clear();
			cacheImageProvider.ClearImages();
			this.CurrentImageKey = string.Empty;
			this.CurrentImageUrl = string.Empty;
			HttpRuntime.Cache.Remove(this.DownloadKey);
			if (HttpContext.Current.Session != null)
			{
				HttpContext.Current.Session.Remove(this.DownloadKey);
			}
		}

		// Token: 0x06007105 RID: 28933 RVA: 0x001A8295 File Offset: 0x001A6495
		public EditableImage GetEditableImage()
		{
			return this.GetCurrentEditableImage();
		}

		// Token: 0x06007106 RID: 28934 RVA: 0x001A829D File Offset: 0x001A649D
		public void RegisterCustomCommand(string commandName)
		{
			this.customCommands.Add(commandName);
		}

		// Token: 0x06007107 RID: 28935 RVA: 0x001A82AC File Offset: 0x001A64AC
		public static Type GetICacheImageProviderType(string imageProviderTypeName)
		{
			string typeName = string.IsNullOrEmpty(imageProviderTypeName) ? typeof(CacheImageProvider).FullName : imageProviderTypeName;
			return Type.GetType(typeName);
		}

		// Token: 0x06007108 RID: 28936 RVA: 0x001A82DA File Offset: 0x001A64DA
		private string SerializeDownloadKey()
		{
			return this.EncryptDownloadKey();
		}

		// Token: 0x06007109 RID: 28937 RVA: 0x001A82E2 File Offset: 0x001A64E2
		private string EncryptDownloadKey()
		{
			return this.Encrypt(this.DownloadKey);
		}

		// Token: 0x0600710A RID: 28938 RVA: 0x001A82F0 File Offset: 0x001A64F0
		private string Encrypt(string input)
		{
			return HmacEnabledCryptoService.GetService("").Encrypt(input);
		}

		// Token: 0x0600710B RID: 28939 RVA: 0x001A8302 File Offset: 0x001A6502
		protected virtual void OnImageChanged(EditableImage image)
		{
			this.RaiseImageChangedEvent(image, RadImageEditor.imageChangedEvent);
		}

		// Token: 0x0600710C RID: 28940 RVA: 0x001A8310 File Offset: 0x001A6510
		protected void RaiseImageChangedEvent(EditableImage image, object eventKey)
		{
			ImageChangedEventHandler imageChangedEventHandler = (ImageChangedEventHandler)base.Events[eventKey];
			if (imageChangedEventHandler != null)
			{
				imageChangedEventHandler(this, new ImageEditorEventArgs(image));
			}
		}

		// Token: 0x0600710D RID: 28941 RVA: 0x001A833F File Offset: 0x001A653F
		protected virtual void OnDialogLoading(string dialogName, Control panel)
		{
			this.RaiseDialogLoadingEvent(dialogName, panel, RadImageEditor.dialogLoadingEvent);
		}

		// Token: 0x0600710E RID: 28942 RVA: 0x001A8350 File Offset: 0x001A6550
		protected void RaiseDialogLoadingEvent(string dialogName, Control panel, object eventKey)
		{
			ImageEditorDialogEventHandler imageEditorDialogEventHandler = (ImageEditorDialogEventHandler)base.Events[eventKey];
			if (imageEditorDialogEventHandler != null)
			{
				imageEditorDialogEventHandler(this, new ImageEditorDialogEventArgs(dialogName, panel));
			}
		}

		// Token: 0x0600710F RID: 28943 RVA: 0x001A8380 File Offset: 0x001A6580
		protected virtual void OnImageSaving(ImageEditorSavingEventArgs savingEventArgs)
		{
			this.RaiseImageSavingEvent(savingEventArgs, RadImageEditor.imageSavingEvent);
		}

		// Token: 0x06007110 RID: 28944 RVA: 0x001A8390 File Offset: 0x001A6590
		protected void RaiseImageSavingEvent(ImageEditorSavingEventArgs savingEventArgs, object eventKey)
		{
			ImageEditorSavingEventHandler imageEditorSavingEventHandler = (ImageEditorSavingEventHandler)base.Events[eventKey];
			if (imageEditorSavingEventHandler != null)
			{
				imageEditorSavingEventHandler(this, savingEventArgs);
			}
		}

		// Token: 0x06007111 RID: 28945 RVA: 0x001A83BA File Offset: 0x001A65BA
		protected virtual void OnImageLoading(ImageEditorLoadingEventArgs loadingEventArgs)
		{
			this.RaiseImageLoadingEvent(loadingEventArgs, RadImageEditor.imageLoadingEvent);
		}

		// Token: 0x06007112 RID: 28946 RVA: 0x001A83C8 File Offset: 0x001A65C8
		protected void RaiseImageLoadingEvent(ImageEditorLoadingEventArgs loadingEventArgs, object eventKey)
		{
			ImageEditorLoadingEventHandler imageEditorLoadingEventHandler = (ImageEditorLoadingEventHandler)base.Events[eventKey];
			if (imageEditorLoadingEventHandler != null)
			{
				imageEditorLoadingEventHandler(this, loadingEventArgs);
			}
		}

		// Token: 0x06007113 RID: 28947 RVA: 0x001A83F2 File Offset: 0x001A65F2
		protected virtual void OnImageEditing(ImageEditorEditingEventArgs editingEventArgs)
		{
			this.RaiseImageEditingEvent(editingEventArgs, RadImageEditor.imageEditingEvent);
		}

		// Token: 0x06007114 RID: 28948 RVA: 0x001A8400 File Offset: 0x001A6600
		protected void RaiseImageEditingEvent(ImageEditorEditingEventArgs editingEventArgs, object eventKey)
		{
			ImageEditorEditingEventHandler imageEditorEditingEventHandler = (ImageEditorEditingEventHandler)base.Events[eventKey];
			if (imageEditorEditingEventHandler != null)
			{
				imageEditorEditingEventHandler(this, editingEventArgs);
			}
		}

		// Token: 0x170024D2 RID: 9426
		// (get) Token: 0x06007115 RID: 28949 RVA: 0x001A842C File Offset: 0x001A662C
		protected override string CssClassFormatString
		{
			get
			{
				string text = (this.StatusBarMode != StatusBarMode.Bottom) ? " rieNoStatusBar" : string.Empty;
				if (this.ToolBarMode == ToolBarMode.Docked)
				{
					text += " rieDockToolbar";
				}
				return "RadImageEditor RadImageEditor_{0}" + text;
			}
		}

		// Token: 0x170024D3 RID: 9427
		// (get) Token: 0x06007116 RID: 28950 RVA: 0x001A846E File Offset: 0x001A666E
		protected override HtmlTextWriterTag TagKey
		{
			get
			{
				return HtmlTextWriterTag.Div;
			}
		}

		// Token: 0x170024D4 RID: 9428
		// (get) Token: 0x06007117 RID: 28951 RVA: 0x001A8472 File Offset: 0x001A6672
		// (set) Token: 0x06007118 RID: 28952 RVA: 0x001A8492 File Offset: 0x001A6692
		[DefaultValue("")]
		private string DownloadKey
		{
			get
			{
				return ((string)this.ViewState["DownloadKey"]) ?? string.Empty;
			}
			set
			{
				this.ViewState["DownloadKey"] = value;
			}
		}

		// Token: 0x170024D5 RID: 9429
		// (get) Token: 0x06007119 RID: 28953 RVA: 0x001A84A5 File Offset: 0x001A66A5
		CultureInfo ILocalizableControl.Culture
		{
			get
			{
				if (this._culture == null)
				{
					this._culture = CultureInfo.GetCultureInfo(this.Language);
				}
				return this._culture;
			}
		}

		// Token: 0x170024D6 RID: 9430
		// (get) Token: 0x0600711A RID: 28954 RVA: 0x001A84C6 File Offset: 0x001A66C6
		// (set) Token: 0x0600711B RID: 28955 RVA: 0x001A84EB File Offset: 0x001A66EB
		[Category("Appearance")]
		[DefaultValue("en-US")]
		[Description("Gets or sets a string containing the localization language for the RadImageEditor UI.")]
		public virtual string Language
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

		// Token: 0x170024D7 RID: 9431
		// (get) Token: 0x0600711C RID: 28956 RVA: 0x001A8510 File Offset: 0x001A6710
		// (set) Token: 0x0600711D RID: 28957 RVA: 0x001A8518 File Offset: 0x001A6718
		[Description("Gets or sets a bool value that indicates whether the RadImageEditor is used in the RadEditor.")]
		[DefaultValue(false)]
		internal virtual bool IsInRadEditor { get; set; }

		// Token: 0x170024D8 RID: 9432
		// (get) Token: 0x0600711E RID: 28958 RVA: 0x001A8521 File Offset: 0x001A6721
		private ImageEditorToolsFileLoader ToolsFileLoader
		{
			get
			{
				if (this._toolsFileLoader == null)
				{
					this._toolsFileLoader = new ImageEditorToolsFileLoader(this);
				}
				return this._toolsFileLoader;
			}
		}

		// Token: 0x170024D9 RID: 9433
		// (get) Token: 0x0600711F RID: 28959 RVA: 0x001A8540 File Offset: 0x001A6740
		internal XmlDocument ToolsFileContent
		{
			get
			{
				if (this._toolsFileContent == null)
				{
					this._toolsFileContent = new XmlDocument();
					if (!base.DesignMode)
					{
						if (string.IsNullOrEmpty(this.ToolsFile))
						{
							this._toolsFileContent.Load(new XmlTextReader(typeof(RadImageEditor).Assembly.GetManifestResourceStream(this.GetAppropriateToolsFilePath())));
						}
						else
						{
							this._toolsFileContent.Load(this.GetXmlFilePath(this.ToolsFile));
						}
					}
				}
				return this._toolsFileContent;
			}
		}

		// Token: 0x170024DA RID: 9434
		// (get) Token: 0x06007120 RID: 28960 RVA: 0x001A85BE File Offset: 0x001A67BE
		[Browsable(false)]
		public virtual ImageOperationCollection UndoStack
		{
			get
			{
				if (this._undoStack == null)
				{
					this._undoStack = new ImageOperationCollection();
				}
				return this._undoStack;
			}
		}

		// Token: 0x170024DB RID: 9435
		// (get) Token: 0x06007121 RID: 28961 RVA: 0x001A85D9 File Offset: 0x001A67D9
		internal ImageOperationCollection RedoStack
		{
			get
			{
				if (this._redoStack == null)
				{
					this._redoStack = new ImageOperationCollection();
				}
				return this._redoStack;
			}
		}

		// Token: 0x170024DC RID: 9436
		// (get) Token: 0x06007122 RID: 28962 RVA: 0x001A85F4 File Offset: 0x001A67F4
		internal static string HandlerRouterKey
		{
			get
			{
				return "iec";
			}
		}

		// Token: 0x170024DD RID: 9437
		// (get) Token: 0x06007123 RID: 28963 RVA: 0x001A85FB File Offset: 0x001A67FB
		// (set) Token: 0x06007124 RID: 28964 RVA: 0x001A860C File Offset: 0x001A680C
		[DefaultValue("")]
		[ClientControlProperty]
		[ClientPropertyName("imageKey")]
		public virtual string CurrentImageKey
		{
			get
			{
				return this._currentImageKey ?? string.Empty;
			}
			protected set
			{
				this._currentImageKey = value;
			}
		}

		// Token: 0x170024DE RID: 9438
		// (get) Token: 0x06007125 RID: 28965 RVA: 0x001A8618 File Offset: 0x001A6818
		[DefaultValue("")]
		public virtual string ImageStorageKey
		{
			get
			{
				if (string.IsNullOrEmpty(this._imageStorageKey))
				{
					this._imageStorageKey = Guid.NewGuid().ToString();
				}
				return this._imageStorageKey;
			}
		}

		// Token: 0x170024DF RID: 9439
		// (get) Token: 0x06007126 RID: 28966 RVA: 0x001A8651 File Offset: 0x001A6851
		// (set) Token: 0x06007127 RID: 28967 RVA: 0x001A8659 File Offset: 0x001A6859
		public override string Skin
		{
			get
			{
				return base.Skin;
			}
			set
			{
				base.Skin = value;
				this.SetSkinChildRadControls(value);
			}
		}

		// Token: 0x06007128 RID: 28968 RVA: 0x001A866C File Offset: 0x001A686C
		private void SetSkinChildRadControls(string Skin)
		{
			if (this._toolsXHPanel == null)
			{
				return;
			}
			if (base.IsSkinSet)
			{
				this._toolsXHPanel.Skin = Skin;
				this._dockToolsPanel.Skin = Skin;
				this._formDecorator.Skin = Skin;
				this._ajaxLoadingPanel.Skin = Skin;
				this._dockToolBar.Skin = Skin;
				this._dockLayout.Skin = Skin;
				foreach (RadDockZone radDockZone in this._dockLayout.RegisteredZones)
				{
					radDockZone.Skin = Skin;
				}
			}
			if (this.ViewState["EnableEmbeddedSkins"] != null)
			{
				this._toolsXHPanel.EnableEmbeddedSkins = this.EnableEmbeddedSkins;
				this._dockToolsPanel.EnableEmbeddedSkins = this.EnableEmbeddedSkins;
				this._formDecorator.EnableEmbeddedSkins = this.EnableEmbeddedSkins;
				this._ajaxLoadingPanel.EnableEmbeddedSkins = this.EnableEmbeddedSkins;
				this._dockToolBar.EnableEmbeddedSkins = this.EnableEmbeddedSkins;
				this._dockLayout.EnableEmbeddedSkins = this.EnableEmbeddedSkins;
				foreach (RadDockZone radDockZone2 in this._dockLayout.RegisteredZones)
				{
					radDockZone2.EnableEmbeddedSkins = this.EnableEmbeddedSkins;
				}
			}
			if (this.ViewState["EnableEmbeddedBaseStylesheet"] != null)
			{
				this._toolsXHPanel.EnableEmbeddedBaseStylesheet = this.EnableEmbeddedBaseStylesheet;
				this._dockToolsPanel.EnableEmbeddedBaseStylesheet = this.EnableEmbeddedBaseStylesheet;
				this._formDecorator.EnableEmbeddedBaseStylesheet = this.EnableEmbeddedBaseStylesheet;
				this._ajaxLoadingPanel.EnableEmbeddedBaseStylesheet = this.EnableEmbeddedBaseStylesheet;
				this._dockToolBar.EnableEmbeddedBaseStylesheet = this.EnableEmbeddedBaseStylesheet;
				this._dockLayout.EnableEmbeddedBaseStylesheet = this.EnableEmbeddedBaseStylesheet;
				foreach (RadDockZone radDockZone3 in this._dockLayout.RegisteredZones)
				{
					radDockZone3.EnableEmbeddedBaseStylesheet = this.EnableEmbeddedBaseStylesheet;
				}
			}
			Panel contentContainer = this._dockToolBar.ContentContainer;
			contentContainer.CssClass += string.Format(" RadImageEditor_{0}", base.RuntimeSkin);
		}

		// Token: 0x170024E0 RID: 9440
		// (get) Token: 0x06007129 RID: 28969 RVA: 0x001A88D0 File Offset: 0x001A6AD0
		protected internal override bool SupportsRenderingMode
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170024E1 RID: 9441
		// (get) Token: 0x0600712A RID: 28970 RVA: 0x001A88D3 File Offset: 0x001A6AD3
		// (set) Token: 0x0600712B RID: 28971 RVA: 0x001A88DB File Offset: 0x001A6ADB
		[NotifyParentProperty(true)]
		[DefaultValue(RenderMode.Classic)]
		[Category("Appearance")]
		[Description("Specifies the rendering mode of the control")]
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

		// Token: 0x170024E2 RID: 9442
		// (get) Token: 0x0600712C RID: 28972 RVA: 0x001A88F2 File Offset: 0x001A6AF2
		// (set) Token: 0x0600712D RID: 28973 RVA: 0x001A8912 File Offset: 0x001A6B12
		[UrlProperty]
		[Editor("System.Web.UI.Design.ImageUrlEditor", typeof(UITypeEditor))]
		[ClientControlProperty]
		[Category("Appearance")]
		[ClientPropertyName("serverImageUrl")]
		[DefaultValue("")]
		[Description("Gets or sets the location of an image to edit within the Image editor.")]
		[Bindable(true)]
		public virtual string ImageUrl
		{
			get
			{
				return ((string)this.ViewState["ImageUrl"]) ?? string.Empty;
			}
			set
			{
				this.ViewState["ImageUrl"] = value;
				this.CurrentImageUrl = value;
				this.CurrentImageKey = "";
			}
		}

		// Token: 0x170024E3 RID: 9443
		// (get) Token: 0x0600712E RID: 28974 RVA: 0x001A8937 File Offset: 0x001A6B37
		// (set) Token: 0x0600712F RID: 28975 RVA: 0x001A8958 File Offset: 0x001A6B58
		[DefaultValue("")]
		public virtual string CurrentImageUrl
		{
			get
			{
				return ((string)this.ViewState["CurrentImageUrl"]) ?? this.ImageUrl;
			}
			protected set
			{
				this.ViewState["CurrentImageUrl"] = value;
			}
		}

		// Token: 0x170024E4 RID: 9444
		// (get) Token: 0x06007130 RID: 28976 RVA: 0x001A896B File Offset: 0x001A6B6B
		// (set) Token: 0x06007131 RID: 28977 RVA: 0x001A898B File Offset: 0x001A6B8B
		[Category("Appearance")]
		[DefaultValue("")]
		[Description("Gets or sets the alternate text displayed in the edited image when the image is unavailable.")]
		public virtual string AlternateText
		{
			get
			{
				return ((string)this.ViewState["AlternateText"]) ?? string.Empty;
			}
			set
			{
				this.ViewState["AlternateText"] = value;
			}
		}

		// Token: 0x170024E5 RID: 9445
		// (get) Token: 0x06007132 RID: 28978 RVA: 0x001A899E File Offset: 0x001A6B9E
		// (set) Token: 0x06007133 RID: 28979 RVA: 0x001A89BE File Offset: 0x001A6BBE
		[Editor("System.Web.UI.Design.ImageUrlEditor", typeof(UITypeEditor))]
		[Description("Gets or sets the location to a detailed description for the edited image.")]
		[Category("Accessibility")]
		[DefaultValue("")]
		[UrlProperty]
		public virtual string DescriptionUrl
		{
			get
			{
				return ((string)this.ViewState["DescriptionUrl"]) ?? string.Empty;
			}
			set
			{
				this.ViewState["DescriptionUrl"] = value;
			}
		}

		// Token: 0x170024E6 RID: 9446
		// (get) Token: 0x06007134 RID: 28980 RVA: 0x001A89D1 File Offset: 0x001A6BD1
		[Description("Gets the collection containing ImageEditor tools.")]
		[Category("Behavior")]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public ImageEditorToolGroupCollection Tools
		{
			get
			{
				if (this._tools == null)
				{
					this._tools = new ImageEditorToolGroupCollection();
					if (base.IsTrackingViewState)
					{
						((IStateManager)this._tools).TrackViewState();
					}
				}
				return this._tools;
			}
		}

		// Token: 0x170024E7 RID: 9447
		// (get) Token: 0x06007135 RID: 28981 RVA: 0x001A89FF File Offset: 0x001A6BFF
		// (set) Token: 0x06007136 RID: 28982 RVA: 0x001A8A1F File Offset: 0x001A6C1F
		[Editor(typeof(UrlEditor), typeof(UITypeEditor))]
		[Bindable(true)]
		[DefaultValue("")]
		[MergableProperty(true)]
		[Category("Behavior")]
		[Description("Gets or sets a string containing the path to a XML file, containing the editor toolbar configuration settings.")]
		[UrlProperty("*.xml")]
		public string ToolsFile
		{
			get
			{
				return ((string)this.ViewState["ToolsFile"]) ?? string.Empty;
			}
			set
			{
				this.ViewState["ToolsFile"] = value;
				this.ResetToolsFileContent();
				this.LoadToolsFile(false);
			}
		}

		// Token: 0x170024E8 RID: 9448
		// (get) Token: 0x06007137 RID: 28983 RVA: 0x001A8A3F File Offset: 0x001A6C3F
		// (set) Token: 0x06007138 RID: 28984 RVA: 0x001A8A5F File Offset: 0x001A6C5F
		[DefaultValue("")]
		[ClientPropertyName("currentCommand")]
		[Description("Gets the name of the last (active) command executed by the ImageEditor.")]
		[Category("Behavior")]
		[ClientControlProperty]
		public virtual string ActiveCommand
		{
			get
			{
				return ((string)this.ViewState["ActiveCommand"]) ?? string.Empty;
			}
			private set
			{
				this.ViewState["ActiveCommand"] = value;
			}
		}

		// Token: 0x170024E9 RID: 9449
		// (get) Token: 0x06007139 RID: 28985 RVA: 0x001A8A72 File Offset: 0x001A6C72
		// (set) Token: 0x0600713A RID: 28986 RVA: 0x001A8A92 File Offset: 0x001A6C92
		[Description("Specifies the URL of the HTTPHandler that serves the cached image.")]
		[Category("Behavior")]
		[DefaultValue("~/Telerik.Web.UI.WebResource.axd")]
		public virtual string HttpHandlerUrl
		{
			get
			{
				return ((string)this.ViewState["HandlerUrl"]) ?? "~/Telerik.Web.UI.WebResource.axd";
			}
			set
			{
				if (!string.IsNullOrEmpty(value))
				{
					this.ViewState["HandlerUrl"] = value;
				}
			}
		}

		// Token: 0x170024EA RID: 9450
		// (get) Token: 0x0600713B RID: 28987 RVA: 0x001A8AAD File Offset: 0x001A6CAD
		// (set) Token: 0x0600713C RID: 28988 RVA: 0x001A8ACE File Offset: 0x001A6CCE
		[Description("Specifies where the cached imaged from the operation will be stored")]
		[DefaultValue(ImageStorage.Cache)]
		public virtual ImageStorage ImageCacheStorageLocation
		{
			get
			{
				return (ImageStorage)(this.ViewState["ImageCacheStorageLocation"] ?? ImageStorage.Cache);
			}
			set
			{
				this.ViewState["ImageCacheStorageLocation"] = value;
			}
		}

		// Token: 0x170024EB RID: 9451
		// (get) Token: 0x0600713D RID: 28989 RVA: 0x001A8AE6 File Offset: 0x001A6CE6
		// (set) Token: 0x0600713E RID: 28990 RVA: 0x001A8B07 File Offset: 0x001A6D07
		[DefaultValue(ToolsLoadPanelTypes.AjaxPanel)]
		[Description("The panel type to use for loading the tools dialogs' content")]
		[Category("Behavior")]
		public virtual ToolsLoadPanelTypes ToolsLoadPanelType
		{
			get
			{
				return (ToolsLoadPanelTypes)(this.ViewState["ToolsLoadPanelType"] ?? ToolsLoadPanelTypes.AjaxPanel);
			}
			set
			{
				this.ViewState["ToolsLoadPanelType"] = value;
				if (this._dockToolsPanel != null)
				{
					if (value == ToolsLoadPanelTypes.RadAjaxPanel)
					{
						this.CreateRadAjaxPanel();
						return;
					}
					this.CreateAjaxPanelControls();
				}
			}
		}

		// Token: 0x170024EC RID: 9452
		// (get) Token: 0x0600713F RID: 28991 RVA: 0x001A8B38 File Offset: 0x001A6D38
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Category("Misc")]
		public ImageEditorStrings Localization
		{
			get
			{
				if (this._localization == null)
				{
					this._localization = new ImageEditorStrings(this);
					if (base.IsTrackingViewState)
					{
						((IStateManager)this._localization).TrackViewState();
					}
				}
				return this._localization;
			}
		}

		// Token: 0x170024ED RID: 9453
		// (get) Token: 0x06007140 RID: 28992 RVA: 0x001A8B67 File Offset: 0x001A6D67
		// (set) Token: 0x06007141 RID: 28993 RVA: 0x001A8B88 File Offset: 0x001A6D88
		[Category("Misc")]
		[DefaultValue("")]
		[Description("Gets or sets a value indicating where the image editor will look for its .resx localization files.")]
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

		// Token: 0x170024EE RID: 9454
		// (get) Token: 0x06007142 RID: 28994 RVA: 0x001A8BDB File Offset: 0x001A6DDB
		// (set) Token: 0x06007143 RID: 28995 RVA: 0x001A8BFC File Offset: 0x001A6DFC
		[DefaultValue(StatusBarMode.Bottom)]
		[Category("Behavior")]
		[Description("Gets or sets a value that controls the behavior of the RadImageEditor's StatusBar.")]
		public StatusBarMode StatusBarMode
		{
			get
			{
				return (StatusBarMode)(this.ViewState["StatusBarMode"] ?? StatusBarMode.Bottom);
			}
			set
			{
				this.ViewState["StatusBarMode"] = value;
			}
		}

		// Token: 0x170024EF RID: 9455
		// (get) Token: 0x06007144 RID: 28996 RVA: 0x001A8C14 File Offset: 0x001A6E14
		// (set) Token: 0x06007145 RID: 28997 RVA: 0x001A8C35 File Offset: 0x001A6E35
		[ClientPropertyName("enableResize")]
		[DefaultValue(true)]
		[Description("Gets or sets a bool value that indicates whether the control can be resized.")]
		[Category("Behavior")]
		[ClientControlProperty]
		public bool EnableResize
		{
			get
			{
				return (bool)(this.ViewState["EnableResize"] ?? true);
			}
			set
			{
				this.ViewState["EnableResize"] = value;
			}
		}

		// Token: 0x170024F0 RID: 9456
		// (get) Token: 0x06007146 RID: 28998 RVA: 0x001A8C4D File Offset: 0x001A6E4D
		// (set) Token: 0x06007147 RID: 28999 RVA: 0x001A8C55 File Offset: 0x001A6E55
		[DefaultValue(typeof(Unit), "")]
		[ClientPropertyName("height")]
		[NotifyParentProperty(true)]
		[ClientControlProperty]
		public override Unit Height
		{
			get
			{
				return base.Height;
			}
			set
			{
				base.Height = value;
			}
		}

		// Token: 0x170024F1 RID: 9457
		// (get) Token: 0x06007148 RID: 29000 RVA: 0x001A8C5E File Offset: 0x001A6E5E
		// (set) Token: 0x06007149 RID: 29001 RVA: 0x001A8C66 File Offset: 0x001A6E66
		[ClientControlProperty]
		[ClientPropertyName("width")]
		[DefaultValue(typeof(Unit), "")]
		[NotifyParentProperty(true)]
		public override Unit Width
		{
			get
			{
				return base.Width;
			}
			set
			{
				base.Width = value;
			}
		}

		// Token: 0x170024F2 RID: 9458
		// (get) Token: 0x0600714A RID: 29002 RVA: 0x001A8C6F File Offset: 0x001A6E6F
		// (set) Token: 0x0600714B RID: 29003 RVA: 0x001A8C90 File Offset: 0x001A6E90
		[DefaultValue(AllowedSavingLocation.ClientAndServer)]
		[ClientControlProperty]
		[ClientPropertyName("allowedSavingLocation")]
		[Description("Gets or sets a value that indicates where the user is allowed to save the image.")]
		public virtual AllowedSavingLocation AllowedSavingLocation
		{
			get
			{
				return (AllowedSavingLocation)(this.ViewState["AllowedSavingLocation"] ?? AllowedSavingLocation.ClientAndServer);
			}
			set
			{
				this.ViewState["AllowedSavingLocation"] = value;
			}
		}

		// Token: 0x170024F3 RID: 9459
		// (get) Token: 0x0600714C RID: 29004 RVA: 0x001A8CA8 File Offset: 0x001A6EA8
		// (set) Token: 0x0600714D RID: 29005 RVA: 0x001A8CC9 File Offset: 0x001A6EC9
		[ClientPropertyName("toolBarMode")]
		[DefaultValue(ToolBarMode.Default)]
		[Description("Gets or sets value that controls the behavior of the Toolbar.")]
		[ClientControlProperty]
		public virtual ToolBarMode ToolBarMode
		{
			get
			{
				return (ToolBarMode)(this.ViewState["ToolBarMode"] ?? ToolBarMode.Default);
			}
			set
			{
				this.ViewState["ToolBarMode"] = value;
			}
		}

		// Token: 0x170024F4 RID: 9460
		// (get) Token: 0x0600714E RID: 29006 RVA: 0x001A8CE1 File Offset: 0x001A6EE1
		// (set) Token: 0x0600714F RID: 29007 RVA: 0x001A8D02 File Offset: 0x001A6F02
		[DefaultValue(ToolBarPosition.Top)]
		[ClientPropertyName("toolBarPosition")]
		[ClientControlProperty]
		[Description("Gets or sets the position of the Toolbar relative to the edited content (content area).")]
		public virtual ToolBarPosition ToolBarPosition
		{
			get
			{
				return (ToolBarPosition)(this.ViewState["ToolBarPosition"] ?? ToolBarPosition.Top);
			}
			set
			{
				this._isUndocked = "0";
				this.ViewState["ToolBarPosition"] = value;
			}
		}

		// Token: 0x170024F5 RID: 9461
		// (get) Token: 0x06007150 RID: 29008 RVA: 0x001A8D25 File Offset: 0x001A6F25
		[Category("Data")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public ImageManagerConfiguration ImageManager
		{
			get
			{
				if (this._imageManager == null)
				{
					this._imageManager = new ImageManagerConfiguration();
					if (base.IsTrackingViewState)
					{
						((IStateManager)this._imageManager).TrackViewState();
					}
				}
				return this._imageManager;
			}
		}

		// Token: 0x170024F6 RID: 9462
		// (get) Token: 0x06007151 RID: 29009 RVA: 0x001A8D53 File Offset: 0x001A6F53
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[Category("Behavior")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public EditableImageConfiguration EditableImageSettings
		{
			get
			{
				if (this._editableImageSettings == null)
				{
					this._editableImageSettings = new EditableImageConfiguration(this);
					if (base.IsTrackingViewState)
					{
						((IStateManager)this._editableImageSettings).TrackViewState();
					}
				}
				return this._editableImageSettings;
			}
		}

		// Token: 0x170024F7 RID: 9463
		// (get) Token: 0x06007152 RID: 29010 RVA: 0x001A8D82 File Offset: 0x001A6F82
		// (set) Token: 0x06007153 RID: 29011 RVA: 0x001A8DA3 File Offset: 0x001A6FA3
		[Description("Gets or sets the maximal number of operations that will be stored in the Undo stack.")]
		[ClientPropertyName("undoLimit")]
		[ClientControlProperty]
		[DefaultValue(0)]
		public virtual int UndoLimit
		{
			get
			{
				return (int)(this.ViewState["UndoLimit"] ?? 0);
			}
			set
			{
				this.ViewState["UndoLimit"] = value;
			}
		}

		// Token: 0x170024F8 RID: 9464
		// (get) Token: 0x06007154 RID: 29012 RVA: 0x001A8DBB File Offset: 0x001A6FBB
		// (set) Token: 0x06007155 RID: 29013 RVA: 0x001A8DDC File Offset: 0x001A6FDC
		[Category("Behavior")]
		[DefaultValue(false)]
		[Description("Gets or sets a bool value that indicates whether RadAjaxLoadingPanel will be shown over the tools panel.")]
		public bool ShowAjaxLoadingPanel
		{
			get
			{
				return (bool)(this.ViewState["ShowAjaxLoadingPanel"] ?? false);
			}
			set
			{
				this.ViewState["ShowAjaxLoadingPanel"] = value;
			}
		}

		// Token: 0x170024F9 RID: 9465
		// (get) Token: 0x06007156 RID: 29014 RVA: 0x001A8DF4 File Offset: 0x001A6FF4
		// (set) Token: 0x06007157 RID: 29015 RVA: 0x001A8E14 File Offset: 0x001A7014
		[Category("Behavior")]
		[DefaultValue("")]
		[Description("Gets or sets a value indicating where the image editor will look for its dialogs.")]
		public virtual string ExternalDialogsPath
		{
			get
			{
				return ((string)this.ViewState["ExternalDialogsPath"]) ?? string.Empty;
			}
			set
			{
				string text = value.Replace("\\", "/");
				if (!string.IsNullOrEmpty(text) && !text.EndsWith("/"))
				{
					text += "/";
				}
				this.ViewState["ExternalDialogsPath"] = text;
			}
		}

		// Token: 0x170024FA RID: 9466
		// (get) Token: 0x06007158 RID: 29016 RVA: 0x001A8E66 File Offset: 0x001A7066
		// (set) Token: 0x06007159 RID: 29017 RVA: 0x001A8E88 File Offset: 0x001A7088
		[DefaultValue(25)]
		[ClientPropertyName("lowerZoomBound")]
		[Description("Gets or sets the lower limit of the zoom level. This is the lowest percentage value up to which the user can zoom out the image in the RadImageEditor.")]
		[ClientControlProperty]
		public virtual int LowerZoomBound
		{
			get
			{
				return (int)(this.ViewState["LowerZoomBound"] ?? 25);
			}
			set
			{
				this.ViewState["LowerZoomBound"] = value;
			}
		}

		// Token: 0x170024FB RID: 9467
		// (get) Token: 0x0600715A RID: 29018 RVA: 0x001A8EA0 File Offset: 0x001A70A0
		// (set) Token: 0x0600715B RID: 29019 RVA: 0x001A8EC5 File Offset: 0x001A70C5
		[ClientPropertyName("upperZoomBound")]
		[DefaultValue(400)]
		[Description("Gets or sets the upper limit of the zoom level. This is the highest percentage value up to which the user can zoom in the image in the RadImageEditor.")]
		[ClientControlProperty]
		public virtual int UpperZoomBound
		{
			get
			{
				return (int)(this.ViewState["UpperZoomBound"] ?? 400);
			}
			set
			{
				this.ViewState["UpperZoomBound"] = value;
			}
		}

		// Token: 0x170024FC RID: 9468
		// (get) Token: 0x0600715C RID: 29020 RVA: 0x001A8EDD File Offset: 0x001A70DD
		// (set) Token: 0x0600715D RID: 29021 RVA: 0x001A8EFE File Offset: 0x001A70FE
		[DefaultValue(CanvasMode.Automatic)]
		[ClientPropertyName("canvasMode")]
		[ClientControlProperty]
		[Description("Gets or sets a value that indicates whether or not the canvas mode of the ImageEditor will be enabled.")]
		[Category("Behavior")]
		public virtual CanvasMode CanvasMode
		{
			get
			{
				return (CanvasMode)(this.ViewState["CanvasMode"] ?? CanvasMode.Automatic);
			}
			set
			{
				this.ViewState["CanvasMode"] = value;
			}
		}

		// Token: 0x170024FD RID: 9469
		// (get) Token: 0x0600715E RID: 29022 RVA: 0x001A8F16 File Offset: 0x001A7116
		// (set) Token: 0x0600715F RID: 29023 RVA: 0x001A8F36 File Offset: 0x001A7136
		[DefaultValue("")]
		[ClientControlEvent]
		[Description("The name of the javascript function called when the control loads in the browser.")]
		[ClientPropertyName("load")]
		[Category("Client-side events")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
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

		// Token: 0x170024FE RID: 9470
		// (get) Token: 0x06007160 RID: 29024 RVA: 0x001A8F49 File Offset: 0x001A7149
		// (set) Token: 0x06007161 RID: 29025 RVA: 0x001A8F69 File Offset: 0x001A7169
		[ClientPropertyName("imageLoad")]
		[DefaultValue("")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[ClientControlEvent]
		[Category("Client-side events")]
		[Description("The name of the javascript function called when the image in the editor loads in the browser.")]
		public string OnClientImageLoad
		{
			get
			{
				return ((string)this.ViewState["OnClientImageLoad"]) ?? string.Empty;
			}
			set
			{
				this.ViewState["OnClientImageLoad"] = value;
			}
		}

		// Token: 0x170024FF RID: 9471
		// (get) Token: 0x06007162 RID: 29026 RVA: 0x001A8F7C File Offset: 0x001A717C
		// (set) Token: 0x06007163 RID: 29027 RVA: 0x001A8F9C File Offset: 0x001A719C
		[Category("Client-side events")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[ClientPropertyName("resizeStart")]
		[Description("The name of the javascript function called when the resizing is started on the control.")]
		[ClientControlEvent]
		[DefaultValue("")]
		public string OnClientResizeStart
		{
			get
			{
				return ((string)this.ViewState["OnClientResizeStart"]) ?? string.Empty;
			}
			set
			{
				this.ViewState["OnClientResizeStart"] = value;
			}
		}

		// Token: 0x17002500 RID: 9472
		// (get) Token: 0x06007164 RID: 29028 RVA: 0x001A8FAF File Offset: 0x001A71AF
		// (set) Token: 0x06007165 RID: 29029 RVA: 0x001A8FCF File Offset: 0x001A71CF
		[Category("Client-side events")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[DefaultValue("")]
		[Description("The name of the javascript function called when the resizing on the control ends.")]
		[ClientPropertyName("resizeEnd")]
		[ClientControlEvent]
		public string OnClientResizeEnd
		{
			get
			{
				return ((string)this.ViewState["OnClientResizeEnd"]) ?? string.Empty;
			}
			set
			{
				this.ViewState["OnClientResizeEnd"] = value;
			}
		}

		// Token: 0x17002501 RID: 9473
		// (get) Token: 0x06007166 RID: 29030 RVA: 0x001A8FE2 File Offset: 0x001A71E2
		// (set) Token: 0x06007167 RID: 29031 RVA: 0x001A9002 File Offset: 0x001A7202
		[ClientControlEvent]
		[Category("Client-side events")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[Description("The name of the javascript function called when a command is firing on the RadImageEditor.")]
		[ClientPropertyName("commandExecuting")]
		[DefaultValue("")]
		public string OnClientCommandExecuting
		{
			get
			{
				return ((string)this.ViewState["OnClientCommandExecuting"]) ?? string.Empty;
			}
			set
			{
				this.ViewState["OnClientCommandExecuting"] = value;
			}
		}

		// Token: 0x17002502 RID: 9474
		// (get) Token: 0x06007168 RID: 29032 RVA: 0x001A9015 File Offset: 0x001A7215
		// (set) Token: 0x06007169 RID: 29033 RVA: 0x001A9035 File Offset: 0x001A7235
		[DefaultValue("")]
		[ClientPropertyName("commandExecuted")]
		[Description("The name of the javascript function called when a command is fired on the RadImageEditor.")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[ClientControlEvent]
		[Category("Client-side events")]
		public string OnClientCommandExecuted
		{
			get
			{
				return ((string)this.ViewState["OnClientCommandExecuted"]) ?? string.Empty;
			}
			set
			{
				this.ViewState["OnClientCommandExecuted"] = value;
			}
		}

		// Token: 0x17002503 RID: 9475
		// (get) Token: 0x0600716A RID: 29034 RVA: 0x001A9048 File Offset: 0x001A7248
		// (set) Token: 0x0600716B RID: 29035 RVA: 0x001A9068 File Offset: 0x001A7268
		[Category("Client-side events")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[ClientControlEvent]
		[Description("The name of the javascript function called when a tool widget dialog is loaded from the server.")]
		[ClientPropertyName("dialogLoaded")]
		[DefaultValue("")]
		public string OnClientDialogLoaded
		{
			get
			{
				return ((string)this.ViewState["OnClientDialogLoaded"]) ?? string.Empty;
			}
			set
			{
				this.ViewState["OnClientDialogLoaded"] = value;
			}
		}

		// Token: 0x17002504 RID: 9476
		// (get) Token: 0x0600716C RID: 29036 RVA: 0x001A907B File Offset: 0x001A727B
		// (set) Token: 0x0600716D RID: 29037 RVA: 0x001A909B File Offset: 0x001A729B
		[Description("The name of the javascript function called before a change is applied on the image edited. ")]
		[ClientPropertyName("imageChanging")]
		[Category("Client-side events")]
		[ClientControlEvent]
		[DefaultValue("")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		public string OnClientImageChanging
		{
			get
			{
				return ((string)this.ViewState["OnClientImageChanging"]) ?? string.Empty;
			}
			set
			{
				this.ViewState["OnClientImageChanging"] = value;
			}
		}

		// Token: 0x17002505 RID: 9477
		// (get) Token: 0x0600716E RID: 29038 RVA: 0x001A90AE File Offset: 0x001A72AE
		// (set) Token: 0x0600716F RID: 29039 RVA: 0x001A90CE File Offset: 0x001A72CE
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[DefaultValue("")]
		[ClientControlEvent]
		[ClientPropertyName("imageChanged")]
		[Category("Client-side events")]
		[Description("The name of the javascript function called after a change is applied on the image edited.")]
		public string OnClientImageChanged
		{
			get
			{
				return ((string)this.ViewState["OnClientImageChanged"]) ?? string.Empty;
			}
			set
			{
				this.ViewState["OnClientImageChanged"] = value;
			}
		}

		// Token: 0x17002506 RID: 9478
		// (get) Token: 0x06007170 RID: 29040 RVA: 0x001A90E1 File Offset: 0x001A72E1
		// (set) Token: 0x06007171 RID: 29041 RVA: 0x001A9101 File Offset: 0x001A7301
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[ClientPropertyName("saving")]
		[Category("Client-side events")]
		[Description("The name of the javascript function called before the image is saved on the client or the server.")]
		[ClientControlEvent]
		[DefaultValue("")]
		public string OnClientSaving
		{
			get
			{
				return ((string)this.ViewState["OnClientSaving"]) ?? string.Empty;
			}
			set
			{
				this.ViewState["OnClientSaving"] = value;
			}
		}

		// Token: 0x17002507 RID: 9479
		// (get) Token: 0x06007172 RID: 29042 RVA: 0x001A9114 File Offset: 0x001A7314
		// (set) Token: 0x06007173 RID: 29043 RVA: 0x001A9134 File Offset: 0x001A7334
		[Description("The name of the javascript function called after a change is applied on the image edited.")]
		[DefaultValue("")]
		[ClientPropertyName("saved")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[ClientControlEvent]
		[Category("Client-side events")]
		public string OnClientSaved
		{
			get
			{
				return ((string)this.ViewState["OnClientSaved"]) ?? string.Empty;
			}
			set
			{
				this.ViewState["OnClientSaved"] = value;
			}
		}

		// Token: 0x17002508 RID: 9480
		// (get) Token: 0x06007174 RID: 29044 RVA: 0x001A9147 File Offset: 0x001A7347
		// (set) Token: 0x06007175 RID: 29045 RVA: 0x001A9167 File Offset: 0x001A7367
		[ClientPropertyName("toolsDialogClosed")]
		[ClientControlEvent]
		[Category("Client-side events")]
		[Description("The name of the javascript function called when the tool's panel dialog is closed.")]
		[DefaultValue("")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		public string OnClientToolsDialogClosed
		{
			get
			{
				return ((string)this.ViewState["OnClientToolsDialogClosed"]) ?? string.Empty;
			}
			set
			{
				this.ViewState["OnClientToolsDialogClosed"] = value;
			}
		}

		// Token: 0x17002509 RID: 9481
		// (get) Token: 0x06007176 RID: 29046 RVA: 0x001A917A File Offset: 0x001A737A
		// (set) Token: 0x06007177 RID: 29047 RVA: 0x001A919A File Offset: 0x001A739A
		[ClientPropertyName("shortCutHit")]
		[Category("Client-side events")]
		[Description("The name of the javascript function called, when a given Keyboard ShortCut of the RadImageEditor was hit.")]
		[DefaultValue("")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[ClientControlEvent]
		public string OnClientShortCutHit
		{
			get
			{
				return ((string)this.ViewState["OnClientShortCutHit"]) ?? string.Empty;
			}
			set
			{
				this.ViewState["OnClientShortCutHit"] = value;
			}
		}

		// Token: 0x140000FF RID: 255
		// (add) Token: 0x06007178 RID: 29048 RVA: 0x001A91AD File Offset: 0x001A73AD
		// (remove) Token: 0x06007179 RID: 29049 RVA: 0x001A91C0 File Offset: 0x001A73C0
		[Description("Fires when the image has been changed.")]
		[Category("Action")]
		public event ImageChangedEventHandler ImageChanged
		{
			add
			{
				base.Events.AddHandler(RadImageEditor.imageChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadImageEditor.imageChangedEvent, value);
			}
		}

		// Token: 0x14000100 RID: 256
		// (add) Token: 0x0600717A RID: 29050 RVA: 0x001A91D3 File Offset: 0x001A73D3
		// (remove) Token: 0x0600717B RID: 29051 RVA: 0x001A91E6 File Offset: 0x001A73E6
		[Description("Fires when an operation's dialog is loading its content.")]
		[Category("Action")]
		public event ImageEditorDialogEventHandler DialogLoading
		{
			add
			{
				base.Events.AddHandler(RadImageEditor.dialogLoadingEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadImageEditor.dialogLoadingEvent, value);
			}
		}

		// Token: 0x14000101 RID: 257
		// (add) Token: 0x0600717C RID: 29052 RVA: 0x001A91F9 File Offset: 0x001A73F9
		// (remove) Token: 0x0600717D RID: 29053 RVA: 0x001A920C File Offset: 0x001A740C
		[Description("Fires just before the image is saved on the file system. This event can be canceled and the edited image saved into a custom location.")]
		[Category("Action")]
		public event ImageEditorSavingEventHandler ImageSaving
		{
			add
			{
				base.Events.AddHandler(RadImageEditor.imageSavingEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadImageEditor.imageSavingEvent, value);
			}
		}

		// Token: 0x14000102 RID: 258
		// (add) Token: 0x0600717E RID: 29054 RVA: 0x001A921F File Offset: 0x001A741F
		// (remove) Token: 0x0600717F RID: 29055 RVA: 0x001A9232 File Offset: 0x001A7432
		[Description("Fires just before the image is loaded from the file system. This event can be canceled and the edited image loaded from a custom location. The event is fired only when the ImageEditor needs to load the initial image.")]
		[Category("Action")]
		public event ImageEditorLoadingEventHandler ImageLoading
		{
			add
			{
				base.Events.AddHandler(RadImageEditor.imageLoadingEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadImageEditor.imageLoadingEvent, value);
			}
		}

		// Token: 0x14000103 RID: 259
		// (add) Token: 0x06007180 RID: 29056 RVA: 0x001A9245 File Offset: 0x001A7445
		// (remove) Token: 0x06007181 RID: 29057 RVA: 0x001A9258 File Offset: 0x001A7458
		[Description("Fires just before the image is edited on the server. It is fired only during callbacks when the user performs a server operation.")]
		[Category("Action")]
		public event ImageEditorEditingEventHandler ImageEditing
		{
			add
			{
				base.Events.AddHandler(RadImageEditor.imageEditingEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadImageEditor.imageEditingEvent, value);
			}
		}

		// Token: 0x06007182 RID: 29058 RVA: 0x001A926C File Offset: 0x001A746C
		protected internal override void DescribeClientProperties(IScriptDescriptor descriptor)
		{
			base.DescribeProperty<string>(descriptor, "currentCommand", this.ActiveCommand, "");
			base.DescribeProperty<AllowedSavingLocation>(descriptor, "allowedSavingLocation", this.AllowedSavingLocation, AllowedSavingLocation.ClientAndServer);
			base.DescribeProperty<CanvasMode>(descriptor, "canvasMode", this.CanvasMode, CanvasMode.Automatic);
			base.DescribeProperty<string>(descriptor, "imageKey", this.CurrentImageKey, "");
			base.DescribeProperty<bool>(descriptor, "enableResize", this.EnableResize, true);
			base.DescribeProperty<string>(descriptor, "height", this.Height.ToString(CultureInfo.InvariantCulture), "");
			base.DescribeProperty<string>(descriptor, "serverImageUrl", this.ImageUrl, "");
			base.DescribeProperty<int>(descriptor, "lowerZoomBound", this.LowerZoomBound, 25);
			base.DescribeProperty<ToolBarMode>(descriptor, "toolBarMode", this.ToolBarMode, ToolBarMode.Default);
			base.DescribeProperty<ToolBarPosition>(descriptor, "toolBarPosition", this.ToolBarPosition, ToolBarPosition.Top);
			base.DescribeProperty<int>(descriptor, "undoLimit", this.UndoLimit, 0);
			base.DescribeProperty<int>(descriptor, "upperZoomBound", this.UpperZoomBound, 400);
			base.DescribeProperty<string>(descriptor, "width", this.Width.ToString(CultureInfo.InvariantCulture), "");
			base.DescribeClientProperties(descriptor);
		}

		// Token: 0x06007183 RID: 29059 RVA: 0x001A93AC File Offset: 0x001A75AC
		protected internal override void DescribeClientEvents(IScriptDescriptor descriptor)
		{
			RadWebControl.DescribeEvent(descriptor, "commandExecuted", this.OnClientCommandExecuted);
			RadWebControl.DescribeEvent(descriptor, "commandExecuting", this.OnClientCommandExecuting);
			RadWebControl.DescribeEvent(descriptor, "dialogLoaded", this.OnClientDialogLoaded);
			RadWebControl.DescribeEvent(descriptor, "imageChanged", this.OnClientImageChanged);
			RadWebControl.DescribeEvent(descriptor, "imageChanging", this.OnClientImageChanging);
			RadWebControl.DescribeEvent(descriptor, "imageLoad", this.OnClientImageLoad);
			RadWebControl.DescribeEvent(descriptor, "load", this.OnClientLoad);
			RadWebControl.DescribeEvent(descriptor, "resizeEnd", this.OnClientResizeEnd);
			RadWebControl.DescribeEvent(descriptor, "resizeStart", this.OnClientResizeStart);
			RadWebControl.DescribeEvent(descriptor, "saved", this.OnClientSaved);
			RadWebControl.DescribeEvent(descriptor, "saving", this.OnClientSaving);
			RadWebControl.DescribeEvent(descriptor, "shortCutHit", this.OnClientShortCutHit);
			RadWebControl.DescribeEvent(descriptor, "toolsDialogClosed", this.OnClientToolsDialogClosed);
			base.DescribeClientEvents(descriptor);
		}

		// Token: 0x04001E6D RID: 7789
		public const string FlipBothImageUrl = "Telerik.Web.UI.Skins.Common.ImageEditor.flipBoth.png";

		// Token: 0x04001E6E RID: 7790
		public const string FlipHorizontalImageUrl = "Telerik.Web.UI.Skins.Common.ImageEditor.flipHorizontal.png";

		// Token: 0x04001E6F RID: 7791
		public const string FlipNoneImageUrl = "Telerik.Web.UI.Skins.Common.ImageEditor.flipNone.png";

		// Token: 0x04001E70 RID: 7792
		public const string FlipVerticalImageUrl = "Telerik.Web.UI.Skins.Common.ImageEditor.flipVertical.png";

		// Token: 0x04001E71 RID: 7793
		public const string DefaultToolsPath = "Telerik.Web.UI.ImageEditor.Resources.ToolsFile.xml";

		// Token: 0x04001E72 RID: 7794
		public const string CanvasToolsPath = "Telerik.Web.UI.ImageEditor.Resources.CanvasTools.xml";

		// Token: 0x04001E73 RID: 7795
		public const string DefaultHandlerUrl = "~/Telerik.Web.UI.WebResource.axd";

		// Token: 0x04001E74 RID: 7796
		private readonly string defaultInsertedImageUrl = "Telerik.Web.UI.Skins.Common.ImageEditor.rieInsertImageBgr.png";

		// Token: 0x04001E75 RID: 7797
		private string _builtInCommandNames = "Print,Save,Undo,Redo,Reset,Crop,Resize,Zoom,ZoomIn,ZoomOut,Opacity,Rotate,RotateRight,RotateLeft,Flip,FlipVertical,FlipHorizontal,AddText,InsertImage,BrightnessContrast,InvertColor,Sepia,Greyscale,HueSaturation,Pencil,Line,DrawRectangle,DrawCircle,Export,Blur,Sharpen,";

		// Token: 0x04001E76 RID: 7798
		private readonly List<string> _darkSkins = new List<string>
		{
			"Black",
			"BlackMetroTouch",
			"Glow",
			"MetroTouch",
			"Outlook",
			"Sunset",
			"Vista",
			"Web20",
			"WebBlue"
		};

		// Token: 0x04001E77 RID: 7799
		private RadDock _dockToolsPanel;

		// Token: 0x04001E78 RID: 7800
		private RadXmlHttpPanel _toolsXHPanel;

		// Token: 0x04001E79 RID: 7801
		private RadXmlHttpPanel _editableImageXHPanel;

		// Token: 0x04001E7A RID: 7802
		private RadFormDecorator _formDecorator;

		// Token: 0x04001E7B RID: 7803
		private RadAjaxLoadingPanel _ajaxLoadingPanel;

		// Token: 0x04001E7C RID: 7804
		private RadDockLayout _dockLayout;

		// Token: 0x04001E7D RID: 7805
		private RadDockZone _zoneTop;

		// Token: 0x04001E7E RID: 7806
		private RadDockZone _zoneBottom;

		// Token: 0x04001E7F RID: 7807
		private RadDockZone _zoneLeft;

		// Token: 0x04001E80 RID: 7808
		private RadDockZone _zoneRight;

		// Token: 0x04001E81 RID: 7809
		private RadDock _dockToolBar;

		// Token: 0x04001E82 RID: 7810
		private HtmlGenericControl _toolBarContainer;

		// Token: 0x04001E83 RID: 7811
		private WebControl _dialogContainer;

		// Token: 0x04001E84 RID: 7812
		private UpdatePanel _toolsAjaxPanel;

		// Token: 0x04001E85 RID: 7813
		private Button _postbackButton;

		// Token: 0x04001E86 RID: 7814
		private RadAjaxPanel _radAjaxPanel;

		// Token: 0x04001E87 RID: 7815
		private bool _toolsFileLoaded;

		// Token: 0x04001E88 RID: 7816
		private List<string> customCommands = new List<string>();

		// Token: 0x04001E89 RID: 7817
		private string _isUndocked = "0";

		// Token: 0x04001E8A RID: 7818
		private bool pendingImageLoadingAttempt = true;

		// Token: 0x04001E8B RID: 7819
		private ICacheImageProvider _cacheImageProvider;

		// Token: 0x04001E8C RID: 7820
		private string _fileName = string.Empty;

		// Token: 0x04001E8D RID: 7821
		private string _argument = string.Empty;

		// Token: 0x04001E8E RID: 7822
		private string _skinApplied;

		// Token: 0x04001E8F RID: 7823
		private string _webResourceUrl;

		// Token: 0x04001E90 RID: 7824
		private FileBrowserContentProvider _contentProvider;

		// Token: 0x04001E91 RID: 7825
		private static readonly object imageChangedEvent = new object();

		// Token: 0x04001E92 RID: 7826
		private static readonly object dialogLoadingEvent = new object();

		// Token: 0x04001E93 RID: 7827
		private static readonly object imageSavingEvent = new object();

		// Token: 0x04001E94 RID: 7828
		private static readonly object imageLoadingEvent = new object();

		// Token: 0x04001E95 RID: 7829
		private static readonly object imageEditingEvent = new object();

		// Token: 0x04001E96 RID: 7830
		private CultureInfo _culture;

		// Token: 0x04001E97 RID: 7831
		private ImageEditorToolsFileLoader _toolsFileLoader;

		// Token: 0x04001E98 RID: 7832
		private XmlDocument _toolsFileContent;

		// Token: 0x04001E99 RID: 7833
		private ImageOperationCollection _undoStack;

		// Token: 0x04001E9A RID: 7834
		private ImageOperationCollection _redoStack;

		// Token: 0x04001E9B RID: 7835
		private string _currentImageKey;

		// Token: 0x04001E9C RID: 7836
		private string _imageStorageKey;

		// Token: 0x04001E9D RID: 7837
		private ImageEditorToolGroupCollection _tools;

		// Token: 0x04001E9E RID: 7838
		private ImageEditorStrings _localization;

		// Token: 0x04001E9F RID: 7839
		private ImageManagerConfiguration _imageManager;

		// Token: 0x04001EA0 RID: 7840
		private EditableImageConfiguration _editableImageSettings;
	}
}
