using System;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Security;
using System.Security.Permissions;
using System.Web.Util;
using System.Xml;

namespace System.Web.UI.WebControls.WebParts
{
	// Token: 0x02000540 RID: 1344
	public sealed class ImportCatalogPart : CatalogPart
	{
		// Token: 0x1700142F RID: 5167
		// (get) Token: 0x060044A9 RID: 17577 RVA: 0x000E3664 File Offset: 0x000E1864
		// (set) Token: 0x060044AA RID: 17578 RVA: 0x000E3696 File Offset: 0x000E1896
		[WebCategory("Appearance")]
		[WebSysDefaultValue("ImportCatalogPart_Browse")]
		[WebSysDescription("ImportCatalogPart_BrowseHelpText")]
		public string BrowseHelpText
		{
			get
			{
				object obj = this.ViewState["BrowseHelpText"];
				if (obj == null)
				{
					return SR.GetString("ImportCatalogPart_Browse");
				}
				return (string)obj;
			}
			set
			{
				this.ViewState["BrowseHelpText"] = value;
			}
		}

		// Token: 0x17001430 RID: 5168
		// (get) Token: 0x060044AB RID: 17579 RVA: 0x000D9E7A File Offset: 0x000D807A
		// (set) Token: 0x060044AC RID: 17580 RVA: 0x000D9E82 File Offset: 0x000D8082
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Themeable(false)]
		public override string DefaultButton
		{
			get
			{
				return base.DefaultButton;
			}
			set
			{
				base.DefaultButton = value;
			}
		}

		// Token: 0x17001431 RID: 5169
		// (get) Token: 0x060044AD RID: 17581 RVA: 0x000E36AC File Offset: 0x000E18AC
		// (set) Token: 0x060044AE RID: 17582 RVA: 0x000E36DE File Offset: 0x000E18DE
		[WebCategory("Appearance")]
		[WebSysDefaultValue("ImportCatalogPart_ImportedPartLabel")]
		[WebSysDescription("ImportCatalogPart_ImportedPartLabelText")]
		public string ImportedPartLabelText
		{
			get
			{
				object obj = this.ViewState["ImportedPartLabelText"];
				if (obj == null)
				{
					return SR.GetString("ImportCatalogPart_ImportedPartLabel");
				}
				return (string)obj;
			}
			set
			{
				this.ViewState["ImportedPartLabelText"] = value;
			}
		}

		// Token: 0x17001432 RID: 5170
		// (get) Token: 0x060044AF RID: 17583 RVA: 0x000E36F4 File Offset: 0x000E18F4
		// (set) Token: 0x060044B0 RID: 17584 RVA: 0x000E3726 File Offset: 0x000E1926
		[WebCategory("Appearance")]
		[WebSysDefaultValue("ImportCatalogPart_ImportedPartErrorLabel")]
		[WebSysDescription("ImportCatalogPart_PartImportErrorLabelText")]
		public string PartImportErrorLabelText
		{
			get
			{
				object obj = this.ViewState["PartImportErrorLabelText"];
				if (obj == null)
				{
					return SR.GetString("ImportCatalogPart_ImportedPartErrorLabel");
				}
				return (string)obj;
			}
			set
			{
				this.ViewState["PartImportErrorLabelText"] = value;
			}
		}

		// Token: 0x17001433 RID: 5171
		// (get) Token: 0x060044B1 RID: 17585 RVA: 0x000E373C File Offset: 0x000E193C
		// (set) Token: 0x060044B2 RID: 17586 RVA: 0x000D9EF2 File Offset: 0x000D80F2
		[WebSysDefaultValue("ImportCatalogPart_PartTitle")]
		public override string Title
		{
			get
			{
				string text = (string)this.ViewState["Title"];
				if (text == null)
				{
					return SR.GetString("ImportCatalogPart_PartTitle");
				}
				return text;
			}
			set
			{
				this.ViewState["Title"] = value;
			}
		}

		// Token: 0x17001434 RID: 5172
		// (get) Token: 0x060044B3 RID: 17587 RVA: 0x000E3770 File Offset: 0x000E1970
		// (set) Token: 0x060044B4 RID: 17588 RVA: 0x000E37A2 File Offset: 0x000E19A2
		[WebCategory("Appearance")]
		[WebSysDefaultValue("ImportCatalogPart_UploadButton")]
		[WebSysDescription("ImportCatalogPart_UploadButtonText")]
		public string UploadButtonText
		{
			get
			{
				object obj = this.ViewState["UploadButtonText"];
				if (obj == null)
				{
					return SR.GetString("ImportCatalogPart_UploadButton");
				}
				return (string)obj;
			}
			set
			{
				this.ViewState["UploadButtonText"] = value;
			}
		}

		// Token: 0x17001435 RID: 5173
		// (get) Token: 0x060044B5 RID: 17589 RVA: 0x000E37B8 File Offset: 0x000E19B8
		// (set) Token: 0x060044B6 RID: 17590 RVA: 0x000E37EA File Offset: 0x000E19EA
		[WebCategory("Appearance")]
		[WebSysDefaultValue("ImportCatalogPart_Upload")]
		[WebSysDescription("ImportCatalogPart_UploadHelpText")]
		public string UploadHelpText
		{
			get
			{
				object obj = this.ViewState["UploadHelpText"];
				if (obj == null)
				{
					return SR.GetString("ImportCatalogPart_Upload");
				}
				return (string)obj;
			}
			set
			{
				this.ViewState["UploadHelpText"] = value;
			}
		}

		// Token: 0x060044B7 RID: 17591 RVA: 0x000E3800 File Offset: 0x000E1A00
		protected internal override void CreateChildControls()
		{
			this.Controls.Clear();
			this._upload = new FileUpload();
			this.Controls.Add(this._upload);
			this._uploadButton = new Button();
			this._uploadButton.ID = "Upload";
			this._uploadButton.CommandName = "upload";
			this._uploadButton.Click += this.OnUpload;
			this.Controls.Add(this._uploadButton);
			if (!base.DesignMode && this.Page != null)
			{
				IScriptManager scriptManager = this.Page.ScriptManager;
				if (scriptManager != null)
				{
					scriptManager.RegisterPostBackControl(this._uploadButton);
				}
			}
		}

		// Token: 0x060044B8 RID: 17592 RVA: 0x000E38B2 File Offset: 0x000E1AB2
		public override WebPartDescriptionCollection GetAvailableWebPartDescriptions()
		{
			if (base.DesignMode)
			{
				return ImportCatalogPart.DesignModeAvailableWebPart;
			}
			this.CreateAvailableWebPartDescriptions();
			return this._availableWebPartDescriptions;
		}

		// Token: 0x060044B9 RID: 17593 RVA: 0x000E38D0 File Offset: 0x000E1AD0
		private void CreateAvailableWebPartDescriptions()
		{
			if (this._availableWebPartDescriptions != null)
			{
				return;
			}
			if (base.WebPartManager == null || string.IsNullOrEmpty(this._importedPartDescription))
			{
				this._availableWebPartDescriptions = new WebPartDescriptionCollection();
				return;
			}
			PermissionSet permissionSet = new PermissionSet(PermissionState.None);
			permissionSet.AddPermission(new SecurityPermission(SecurityPermissionFlag.Execution));
			permissionSet.AddPermission(new AspNetHostingPermission(AspNetHostingPermissionLevel.Minimal));
			permissionSet.PermitOnly();
			bool flag = true;
			string text = null;
			string text2 = null;
			string text3 = null;
			try
			{
				try
				{
					using (StringReader stringReader = new StringReader(this._importedPartDescription))
					{
						using (XmlReader xmlReader = XmlUtils.CreateXmlReader(stringReader))
						{
							if (xmlReader != null)
							{
								xmlReader.MoveToContent();
								xmlReader.MoveToContent();
								xmlReader.ReadStartElement("webParts");
								xmlReader.ReadStartElement("webPart");
								xmlReader.ReadStartElement("metaData");
								string text4 = null;
								string path = null;
								while (xmlReader.Name != "type")
								{
									xmlReader.Skip();
									if (xmlReader.EOF)
									{
										throw new EndOfStreamException();
									}
								}
								if (xmlReader.Name == "type")
								{
									text4 = xmlReader.GetAttribute("name");
									path = xmlReader.GetAttribute("src");
								}
								bool isShared = base.WebPartManager.Personalization.Scope == PersonalizationScope.Shared;
								if (!string.IsNullOrEmpty(text4))
								{
									PermissionSet permissionSet2 = new PermissionSet(PermissionState.None);
									permissionSet2.AddPermission(new SecurityPermission(SecurityPermissionFlag.Execution));
									permissionSet2.AddPermission(new AspNetHostingPermission(AspNetHostingPermissionLevel.Medium));
									CodeAccessPermission.RevertPermitOnly();
									flag = false;
									permissionSet2.PermitOnly();
									flag = true;
									Type type = WebPartUtil.DeserializeType(text4, true);
									CodeAccessPermission.RevertPermitOnly();
									flag = false;
									permissionSet.PermitOnly();
									flag = true;
									if (!base.WebPartManager.IsAuthorized(type, null, null, isShared))
									{
										this._importErrorMessage = SR.GetString("WebPartManager_ForbiddenType");
										return;
									}
									if (!type.IsSubclassOf(typeof(WebPart)) && !type.IsSubclassOf(typeof(Control)))
									{
										this._importErrorMessage = SR.GetString("WebPartManager_TypeMustDeriveFromControl");
										return;
									}
								}
								else if (!base.WebPartManager.IsAuthorized(typeof(UserControl), path, null, isShared))
								{
									this._importErrorMessage = SR.GetString("WebPartManager_ForbiddenType");
									return;
								}
								while (!xmlReader.EOF)
								{
									while (!xmlReader.EOF && (xmlReader.NodeType != XmlNodeType.Element || !(xmlReader.Name == "property")))
									{
										xmlReader.Read();
									}
									if (xmlReader.EOF)
									{
										break;
									}
									string attribute = xmlReader.GetAttribute("name");
									if (attribute == "Title")
									{
										text = xmlReader.ReadElementString();
									}
									else if (attribute == "Description")
									{
										text2 = xmlReader.ReadElementString();
									}
									else
									{
										if (!(attribute == "CatalogIconImageUrl"))
										{
											xmlReader.Read();
											continue;
										}
										string text5 = xmlReader.ReadElementString().Trim();
										if (!CrossSiteScriptingValidation.IsDangerousUrl(text5))
										{
											text3 = text5;
										}
									}
									if (text != null && text2 != null && text3 != null)
									{
										break;
									}
									xmlReader.Read();
								}
							}
						}
						if (string.IsNullOrEmpty(text))
						{
							text = SR.GetString("Part_Untitled");
						}
						this._availableWebPartDescriptions = new WebPartDescriptionCollection(new WebPartDescription[]
						{
							new WebPartDescription("ImportedWebPart", text, text2, text3)
						});
					}
				}
				catch (XmlException)
				{
					this._importErrorMessage = SR.GetString("WebPartManager_ImportInvalidFormat");
				}
				catch
				{
					this._importErrorMessage = ((!string.IsNullOrEmpty(this._importErrorMessage)) ? this._importErrorMessage : SR.GetString("WebPart_DefaultImportErrorMessage"));
				}
				finally
				{
					if (flag)
					{
						CodeAccessPermission.RevertPermitOnly();
					}
				}
			}
			catch
			{
				throw;
			}
		}

		// Token: 0x060044BA RID: 17594 RVA: 0x000E3CF8 File Offset: 0x000E1EF8
		public override WebPart GetWebPart(WebPartDescription description)
		{
			if (description == null)
			{
				throw new ArgumentNullException("description");
			}
			WebPartDescriptionCollection availableWebPartDescriptions = this.GetAvailableWebPartDescriptions();
			if (!availableWebPartDescriptions.Contains(description))
			{
				throw new ArgumentException(SR.GetString("CatalogPart_UnknownDescription"), "description");
			}
			if (this._availableWebPart != null)
			{
				return this._availableWebPart;
			}
			using (XmlReader xmlReader = XmlUtils.CreateXmlReader(new StringReader(this._importedPartDescription)))
			{
				if (xmlReader != null && base.WebPartManager != null)
				{
					this._availableWebPart = base.WebPartManager.ImportWebPart(xmlReader, out this._importErrorMessage);
				}
			}
			if (this._availableWebPart == null)
			{
				this._importedPartDescription = null;
				this._availableWebPartDescriptions = null;
			}
			return this._availableWebPart;
		}

		// Token: 0x060044BB RID: 17595 RVA: 0x000E3DB4 File Offset: 0x000E1FB4
		protected internal override void LoadControlState(object savedState)
		{
			if (savedState == null)
			{
				base.LoadControlState(null);
				return;
			}
			object[] array = (object[])savedState;
			if (array.Length != 2)
			{
				throw new ArgumentException(SR.GetString("Invalid_ControlState"));
			}
			base.LoadControlState(array[0]);
			if (array[1] != null)
			{
				this._importedPartDescription = (string)array[1];
				this.GetAvailableWebPartDescriptions();
			}
		}

		// Token: 0x060044BC RID: 17596 RVA: 0x000E3E0C File Offset: 0x000E200C
		protected internal override void OnInit(EventArgs e)
		{
			base.OnInit(e);
			this.Page.RegisterRequiresControlState(this);
		}

		// Token: 0x060044BD RID: 17597 RVA: 0x000E3E24 File Offset: 0x000E2024
		internal void OnUpload(object sender, EventArgs e)
		{
			string fileName = this._upload.FileName;
			Stream fileContent = this._upload.FileContent;
			if (!string.IsNullOrEmpty(fileName) && fileContent != null)
			{
				using (StreamReader streamReader = new StreamReader(fileContent, true))
				{
					this._importedPartDescription = streamReader.ReadToEnd();
					this._availableWebPart = null;
					this._availableWebPartDescriptions = null;
					this._importErrorMessage = null;
					if (string.IsNullOrEmpty(this._importedPartDescription))
					{
						this._importErrorMessage = SR.GetString("ImportCatalogPart_NoFileName");
						return;
					}
					this.GetAvailableWebPartDescriptions();
					return;
				}
			}
			this._importErrorMessage = SR.GetString("ImportCatalogPart_NoFileName");
		}

		// Token: 0x060044BE RID: 17598 RVA: 0x000E3ED0 File Offset: 0x000E20D0
		protected internal override object SaveControlState()
		{
			object[] array = new object[]
			{
				base.SaveControlState(),
				this._importedPartDescription
			};
			for (int i = 0; i < 2; i++)
			{
				if (array[i] != null)
				{
					return array;
				}
			}
			return null;
		}

		// Token: 0x060044BF RID: 17599 RVA: 0x0009F187 File Offset: 0x0009D387
		protected internal override void Render(HtmlTextWriter writer)
		{
			if (this.Page != null)
			{
				this.Page.VerifyRenderingInServerForm(this);
			}
			base.Render(writer);
		}

		// Token: 0x060044C0 RID: 17600 RVA: 0x000E3F0C File Offset: 0x000E210C
		protected internal override void RenderContents(HtmlTextWriter writer)
		{
			this.EnsureChildControls();
			CatalogZoneBase zone = base.Zone;
			if (zone != null && !zone.LabelStyle.IsEmpty)
			{
				zone.LabelStyle.AddAttributesToRender(writer, this);
			}
			writer.AddAttribute(HtmlTextWriterAttribute.For, this._upload.ClientID);
			writer.RenderBeginTag(HtmlTextWriterTag.Label);
			writer.Write(this.BrowseHelpText);
			writer.RenderEndTag();
			writer.WriteBreak();
			if (zone != null && !zone.EditUIStyle.IsEmpty)
			{
				this._upload.ApplyStyle(zone.EditUIStyle);
			}
			this._upload.RenderControl(writer);
			writer.WriteBreak();
			if (zone != null && !zone.LabelStyle.IsEmpty)
			{
				zone.LabelStyle.AddAttributesToRender(writer, this);
			}
			writer.RenderBeginTag(HtmlTextWriterTag.Span);
			writer.Write(this.UploadHelpText);
			writer.RenderEndTag();
			writer.WriteBreak();
			if (zone != null && !zone.EditUIStyle.IsEmpty)
			{
				this._uploadButton.ApplyStyle(zone.EditUIStyle);
			}
			this._uploadButton.Text = this.UploadButtonText;
			this._uploadButton.RenderControl(writer);
			if (this._importedPartDescription != null || this._importErrorMessage != null || base.DesignMode)
			{
				writer.WriteBreak();
				if (this._importErrorMessage != null)
				{
					if (zone != null && !zone.ErrorStyle.IsEmpty)
					{
						zone.ErrorStyle.AddAttributesToRender(writer, this);
					}
					writer.RenderBeginTag(HtmlTextWriterTag.Span);
					writer.Write(this.PartImportErrorLabelText);
					writer.RenderEndTag();
					writer.RenderBeginTag(HtmlTextWriterTag.Hr);
					writer.RenderEndTag();
					if (zone != null && !zone.ErrorStyle.IsEmpty)
					{
						zone.ErrorStyle.AddAttributesToRender(writer, this);
					}
					writer.RenderBeginTag(HtmlTextWriterTag.Span);
					writer.WriteEncodedText(this._importErrorMessage);
					writer.RenderEndTag();
					return;
				}
				if (zone != null && !zone.LabelStyle.IsEmpty)
				{
					zone.LabelStyle.AddAttributesToRender(writer, this);
				}
				writer.RenderBeginTag(HtmlTextWriterTag.Span);
				writer.Write(this.ImportedPartLabelText);
				writer.RenderEndTag();
				writer.RenderBeginTag(HtmlTextWriterTag.Hr);
				writer.RenderEndTag();
			}
		}

		// Token: 0x0400263C RID: 9788
		private WebPart _availableWebPart;

		// Token: 0x0400263D RID: 9789
		private string _importedPartDescription;

		// Token: 0x0400263E RID: 9790
		private WebPartDescriptionCollection _availableWebPartDescriptions;

		// Token: 0x0400263F RID: 9791
		private FileUpload _upload;

		// Token: 0x04002640 RID: 9792
		private Button _uploadButton;

		// Token: 0x04002641 RID: 9793
		private string _importErrorMessage;

		// Token: 0x04002642 RID: 9794
		private const int baseIndex = 0;

		// Token: 0x04002643 RID: 9795
		private const int importedPartDescriptionIndex = 1;

		// Token: 0x04002644 RID: 9796
		private const int controlStateArrayLength = 2;

		// Token: 0x04002645 RID: 9797
		private const string TitlePropertyName = "Title";

		// Token: 0x04002646 RID: 9798
		private const string DescriptionPropertyName = "Description";

		// Token: 0x04002647 RID: 9799
		private const string IconPropertyName = "CatalogIconImageUrl";

		// Token: 0x04002648 RID: 9800
		private const string ImportedWebPartID = "ImportedWebPart";

		// Token: 0x04002649 RID: 9801
		private static readonly WebPartDescriptionCollection DesignModeAvailableWebPart = new WebPartDescriptionCollection(new WebPartDescription[]
		{
			new WebPartDescription("webpart1", string.Format(CultureInfo.CurrentCulture, SR.GetString("CatalogPart_SampleWebPartTitle"), new object[]
			{
				"1"
			}), null, null)
		});
	}
}
