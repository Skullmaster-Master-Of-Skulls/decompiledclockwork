using System;
using System.Collections;
using System.Collections.Specialized;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Design;
using System.Security.Permissions;
using System.Web.UI.WebControls;
using System.Windows.Forms;

namespace System.Web.UI.Design.WebControls
{
	// Token: 0x020000A7 RID: 167
	[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
	public class ChangePasswordDesigner : ControlDesigner
	{
		// Token: 0x17000140 RID: 320
		// (get) Token: 0x0600050C RID: 1292 RVA: 0x00018724 File Offset: 0x00016924
		public override DesignerActionListCollection ActionLists
		{
			get
			{
				DesignerActionListCollection designerActionListCollection = new DesignerActionListCollection();
				designerActionListCollection.AddRange(base.ActionLists);
				designerActionListCollection.Add(new ChangePasswordDesigner.ChangePasswordDesignerActionList(this));
				return designerActionListCollection;
			}
		}

		// Token: 0x17000141 RID: 321
		// (get) Token: 0x0600050D RID: 1293 RVA: 0x00018751 File Offset: 0x00016951
		public override bool AllowResize
		{
			get
			{
				return this.RenderOuterTable && base.AllowResize;
			}
		}

		// Token: 0x17000142 RID: 322
		// (get) Token: 0x0600050E RID: 1294 RVA: 0x00018763 File Offset: 0x00016963
		public override DesignerAutoFormatCollection AutoFormats
		{
			get
			{
				if (ChangePasswordDesigner._autoFormats == null)
				{
					ChangePasswordDesigner._autoFormats = ControlDesigner.CreateAutoFormats(AutoFormatSchemes.CHANGEPASSWORD_SCHEME_NAMES, (string schemeName) => new ChangePasswordAutoFormat(schemeName, "<Schemes>\r\n<xsd:schema id=\"Schemes\" xmlns=\"\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns:msdata=\"urn:schemas-microsoft-com:xml-msdata\">\r\n  <xsd:element name=\"Scheme\">\r\n     <xsd:complexType>\r\n       <xsd:all>\r\n        <xsd:element name=\"SchemeName\" type=\"xsd:string\"/>\r\n        <xsd:element name=\"BackColor\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n        <xsd:element name=\"ForeColor\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n        <xsd:element name=\"BorderColor\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n        <xsd:element name=\"BorderWidth\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n        <xsd:element name=\"BorderStyle\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n        <xsd:element name=\"BorderPadding\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n        <xsd:element name=\"FontSize\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n        <xsd:element name=\"FontName\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n        <xsd:element name=\"TitleTextBackColor\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n        <xsd:element name=\"TitleTextForeColor\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n        <xsd:element name=\"TitleTextFont\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n        <xsd:element name=\"TitleTextFontSize\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n        <xsd:element name=\"PasswordHintForeColor\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n        <xsd:element name=\"PasswordHintFont\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n        <xsd:element name=\"InstructionTextForeColor\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n        <xsd:element name=\"InstructionTextFont\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n        <xsd:element name=\"TextboxFontSize\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n        <xsd:element name=\"ButtonBackColor\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n        <xsd:element name=\"ButtonForeColor\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n        <xsd:element name=\"ButtonFontSize\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n        <xsd:element name=\"ButtonFontName\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n        <xsd:element name=\"ButtonBorderColor\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n        <xsd:element name=\"ButtonBorderWidth\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n        <xsd:element name=\"ButtonBorderStyle\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n        <xsd:element name=\"RenderOuterTable\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n      </xsd:all>\r\n    </xsd:complexType>\r\n  </xsd:element>\r\n  <xsd:element name=\"Schemes\" msdata:IsDataSet=\"true\">\r\n    <xsd:complexType>\r\n      <xsd:choice maxOccurs=\"unbounded\">\r\n        <xsd:element ref=\"Scheme\"/>\r\n      </xsd:choice>\r\n    </xsd:complexType>\r\n  </xsd:element>\r\n</xsd:schema>\r\n<Scheme>\r\n  <SchemeName>ChangePasswordScheme_Empty</SchemeName>\r\n  <RenderOuterTable>True</RenderOuterTable>\r\n</Scheme>\r\n<Scheme>\r\n  <SchemeName>ChangePasswordScheme_Elegant</SchemeName>\r\n  <BackColor>#F7F7DE</BackColor>\r\n  <BorderColor>#CCCC99</BorderColor>\r\n  <BorderWidth>1</BorderWidth>\r\n  <BorderStyle>4</BorderStyle>\r\n  <FontSize>10</FontSize>\r\n  <FontName>Verdana</FontName>\r\n  <TitleTextBackColor>#6B696B</TitleTextBackColor>\r\n  <TitleTextForeColor>#FFFFFF</TitleTextForeColor>\r\n  <TitleTextFont>1</TitleTextFont>\r\n  <RenderOuterTable>True</RenderOuterTable>\r\n</Scheme>\r\n<Scheme>\r\n  <SchemeName>ChangePasswordScheme_Professional</SchemeName>\r\n  <BackColor>#F7F6F3</BackColor>\r\n  <ForeColor>#333333</ForeColor>\r\n  <BorderColor>#E6E2D8</BorderColor>\r\n  <BorderWidth>1</BorderWidth>\r\n  <BorderStyle>4</BorderStyle>\r\n  <BorderPadding>4</BorderPadding>\r\n  <FontSize>0.8em</FontSize>\r\n  <FontName>Verdana</FontName>\r\n  <TitleTextBackColor>#5D7B9D</TitleTextBackColor>\r\n  <TitleTextForeColor>White</TitleTextForeColor>\r\n  <TitleTextFont>1</TitleTextFont>\r\n  <TitleTextFontSize>0.9em</TitleTextFontSize>\r\n  <InstructionTextForeColor>Black</InstructionTextForeColor>\r\n  <InstructionTextFont>2</InstructionTextFont>\r\n  <PasswordHintForeColor>#888888</PasswordHintForeColor>\r\n  <PasswordHintFont>2</PasswordHintFont>\r\n  <TextboxFontSize>0.8em</TextboxFontSize>\r\n  <ButtonBackColor>#FFFBFF</ButtonBackColor>\r\n  <ButtonForeColor>#284775</ButtonForeColor>\r\n  <ButtonFontSize>0.8em</ButtonFontSize>\r\n  <ButtonFontName>Verdana</ButtonFontName>\r\n  <ButtonBorderColor>#CCCCCC</ButtonBorderColor>\r\n  <ButtonBorderWidth>1</ButtonBorderWidth>\r\n  <ButtonBorderStyle>4</ButtonBorderStyle>\r\n  <RenderOuterTable>True</RenderOuterTable>\r\n</Scheme>\r\n<Scheme>\r\n  <SchemeName>ChangePasswordScheme_Simple</SchemeName>\r\n  <BackColor>#E3EAEB</BackColor>\r\n  <ForeColor>#333333</ForeColor>\r\n  <BorderColor>#E6E2D8</BorderColor>\r\n  <BorderWidth>1</BorderWidth>\r\n  <BorderStyle>4</BorderStyle>\r\n  <BorderPadding>4</BorderPadding>\r\n  <FontSize>0.8em</FontSize>\r\n  <FontName>Verdana</FontName>\r\n  <TitleTextBackColor>#1C5E55</TitleTextBackColor>\r\n  <TitleTextForeColor>White</TitleTextForeColor>\r\n  <TitleTextFont>1</TitleTextFont>\r\n  <TitleTextFontSize>0.9em</TitleTextFontSize>\r\n  <InstructionTextForeColor>Black</InstructionTextForeColor>\r\n  <InstructionTextFont>2</InstructionTextFont>\r\n  <TextboxFontSize>0.8em</TextboxFontSize>\r\n  <ButtonBackColor>White</ButtonBackColor>\r\n  <ButtonForeColor>#1C5E55</ButtonForeColor>\r\n  <ButtonFontSize>0.8em</ButtonFontSize>\r\n  <ButtonFontName>Verdana</ButtonFontName>\r\n  <ButtonBorderColor>#C5BBAF</ButtonBorderColor>\r\n  <ButtonBorderWidth>1</ButtonBorderWidth>\r\n  <ButtonBorderStyle>4</ButtonBorderStyle>\r\n  <PasswordHintForeColor>#1C5E55</PasswordHintForeColor>\r\n  <PasswordHintFont>2</PasswordHintFont>\r\n  <RenderOuterTable>True</RenderOuterTable>\r\n</Scheme>\r\n<Scheme>\r\n  <SchemeName>ChangePasswordScheme_Classic</SchemeName>\r\n  <BackColor>#EFF3FB</BackColor>\r\n  <ForeColor>#333333</ForeColor>\r\n  <BorderColor>#B5C7DE</BorderColor>\r\n  <BorderWidth>1</BorderWidth>\r\n  <BorderStyle>4</BorderStyle>\r\n  <BorderPadding>4</BorderPadding>\r\n  <FontSize>0.8em</FontSize>\r\n  <FontName>Verdana</FontName>\r\n  <TitleTextBackColor>#507CD1</TitleTextBackColor>\r\n  <TitleTextForeColor>White</TitleTextForeColor>\r\n  <TitleTextFont>1</TitleTextFont>\r\n  <TitleTextFontSize>0.9em</TitleTextFontSize>\r\n  <InstructionTextForeColor>Black</InstructionTextForeColor>\r\n  <InstructionTextFont>2</InstructionTextFont>\r\n  <TextboxFontSize>0.8em</TextboxFontSize>\r\n  <ButtonBackColor>White</ButtonBackColor>\r\n  <ButtonForeColor>#284E98</ButtonForeColor>\r\n  <ButtonFontSize>0.8em</ButtonFontSize>\r\n  <ButtonFontName>Verdana</ButtonFontName>\r\n  <ButtonBorderColor>#507CD1</ButtonBorderColor>\r\n  <ButtonBorderWidth>1</ButtonBorderWidth>\r\n  <ButtonBorderStyle>4</ButtonBorderStyle>\r\n  <PasswordHintForeColor>#507CD1</PasswordHintForeColor>\r\n  <PasswordHintFont>2</PasswordHintFont>\r\n  <RenderOuterTable>True</RenderOuterTable>\r\n</Scheme>\r\n<Scheme>\r\n  <SchemeName>ChangePasswordScheme_Colorful</SchemeName>\r\n  <BackColor>#FFFBD6</BackColor>\r\n  <ForeColor>#333333</ForeColor>\r\n  <BorderColor>#FFDFAD</BorderColor>\r\n  <BorderWidth>1</BorderWidth>\r\n  <BorderStyle>4</BorderStyle>\r\n  <BorderPadding>4</BorderPadding>\r\n  <FontSize>0.8em</FontSize>\r\n  <FontName>Verdana</FontName>\r\n  <TitleTextBackColor>#990000</TitleTextBackColor>\r\n  <TitleTextForeColor>White</TitleTextForeColor>\r\n  <TitleTextFont>1</TitleTextFont>\r\n  <TitleTextFontSize>0.9em</TitleTextFontSize>\r\n  <InstructionTextForeColor>Black</InstructionTextForeColor>\r\n  <InstructionTextFont>2</InstructionTextFont>\r\n  <TextboxFontSize>0.8em</TextboxFontSize>\r\n  <ButtonBackColor>White</ButtonBackColor>\r\n  <ButtonForeColor>#990000</ButtonForeColor>\r\n  <ButtonFontSize>0.8em</ButtonFontSize>\r\n  <ButtonFontName>Verdana</ButtonFontName>\r\n  <ButtonBorderColor>#CC9966</ButtonBorderColor>\r\n  <ButtonBorderWidth>1</ButtonBorderWidth>\r\n  <ButtonBorderStyle>4</ButtonBorderStyle>\r\n  <PasswordHintForeColor>#888888</PasswordHintForeColor>\r\n  <PasswordHintFont>2</PasswordHintFont>\r\n  <RenderOuterTable>True</RenderOuterTable>\r\n</Scheme>\r\n</Schemes>\r\n"));
				}
				return ChangePasswordDesigner._autoFormats;
			}
		}

		// Token: 0x17000143 RID: 323
		// (get) Token: 0x0600050F RID: 1295 RVA: 0x000187A0 File Offset: 0x000169A0
		// (set) Token: 0x06000510 RID: 1296 RVA: 0x000187C9 File Offset: 0x000169C9
		private ChangePasswordDesigner.ViewType CurrentView
		{
			get
			{
				object obj = base.DesignerState["CurrentView"];
				if (obj != null)
				{
					return (ChangePasswordDesigner.ViewType)obj;
				}
				return ChangePasswordDesigner.ViewType.ChangePassword;
			}
			set
			{
				base.DesignerState["CurrentView"] = value;
			}
		}

		// Token: 0x17000144 RID: 324
		// (get) Token: 0x06000511 RID: 1297 RVA: 0x000187E1 File Offset: 0x000169E1
		// (set) Token: 0x06000512 RID: 1298 RVA: 0x000187F3 File Offset: 0x000169F3
		public bool RenderOuterTable
		{
			get
			{
				return ((ChangePassword)base.Component).RenderOuterTable;
			}
			set
			{
				RenderOuterTableHelper.SetRenderOuterTable(value, this, false);
			}
		}

		// Token: 0x17000145 RID: 325
		// (get) Token: 0x06000513 RID: 1299 RVA: 0x000187FD File Offset: 0x000169FD
		private bool Templated
		{
			get
			{
				return this.GetTemplate(this._changePassword) != null;
			}
		}

		// Token: 0x17000146 RID: 326
		// (get) Token: 0x06000514 RID: 1300 RVA: 0x00018810 File Offset: 0x00016A10
		private PropertyDescriptor TemplateDescriptor
		{
			get
			{
				PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(base.Component);
				string name = ChangePasswordDesigner._templateNames[(int)this.CurrentView];
				return properties.Find(name, false);
			}
		}

		// Token: 0x17000147 RID: 327
		// (get) Token: 0x06000515 RID: 1301 RVA: 0x00018840 File Offset: 0x00016A40
		private TemplateDefinition TemplateDefinition
		{
			get
			{
				string text = ChangePasswordDesigner._templateNames[(int)this.CurrentView];
				return new TemplateDefinition(this, text, this._changePassword, text, ((WebControl)base.ViewControl).ControlStyle);
			}
		}

		// Token: 0x17000148 RID: 328
		// (get) Token: 0x06000516 RID: 1302 RVA: 0x00018878 File Offset: 0x00016A78
		public override TemplateGroupCollection TemplateGroups
		{
			get
			{
				TemplateGroupCollection templateGroups = base.TemplateGroups;
				TemplateGroupCollection templateGroupCollection = new TemplateGroupCollection();
				for (int i = 0; i < ChangePasswordDesigner._templateNames.Length; i++)
				{
					string text = ChangePasswordDesigner._templateNames[i];
					TemplateGroup templateGroup = new TemplateGroup(text, ((WebControl)base.ViewControl).ControlStyle);
					templateGroup.AddTemplateDefinition(new TemplateDefinition(this, text, this._changePassword, text, ((WebControl)base.ViewControl).ControlStyle));
					templateGroupCollection.Add(templateGroup);
				}
				templateGroups.AddRange(templateGroupCollection);
				return templateGroups;
			}
		}

		// Token: 0x17000149 RID: 329
		// (get) Token: 0x06000517 RID: 1303 RVA: 0x00003B0F File Offset: 0x00001D0F
		protected override bool UsePreviewControl
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06000518 RID: 1304 RVA: 0x000188FC File Offset: 0x00016AFC
		private bool ConvertToTemplateChangeCallback(object context)
		{
			bool result;
			try
			{
				IDesignerHost designerHost = (IDesignerHost)this.GetService(typeof(IDesignerHost));
				ChangePasswordDesigner.ConvertToTemplateHelper convertToTemplateHelper = new ChangePasswordDesigner.ConvertToTemplateHelper(this, designerHost);
				ITemplate value = convertToTemplateHelper.ConvertToTemplate();
				this.TemplateDescriptor.SetValue(this._changePassword, value);
				result = true;
			}
			catch (Exception ex)
			{
				result = false;
			}
			return result;
		}

		// Token: 0x06000519 RID: 1305 RVA: 0x0001895C File Offset: 0x00016B5C
		public override string GetDesignTimeHtml()
		{
			return this.GetDesignTimeHtml(null);
		}

		// Token: 0x0600051A RID: 1306 RVA: 0x00018968 File Offset: 0x00016B68
		public override string GetDesignTimeHtml(DesignerRegionCollection regions)
		{
			IDictionary dictionary = new HybridDictionary(2);
			dictionary["CurrentView"] = this.CurrentView;
			bool flag = base.UseRegions(regions, this.GetTemplate(this._changePassword));
			if (flag)
			{
				((WebControl)base.ViewControl).Enabled = true;
				dictionary.Add("RegionEditing", true);
				regions.Add(new TemplatedEditableDesignerRegion(this.TemplateDefinition)
				{
					Description = SR.GetString("ContainerControlDesigner_RegionWatermark")
				});
			}
			string result = string.Empty;
			try
			{
				((IControlDesignerAccessor)base.ViewControl).SetDesignModeState(dictionary);
				((ICompositeControlDesignerAccessor)base.ViewControl).RecreateChildControls();
				result = base.GetDesignTimeHtml();
			}
			catch (Exception e)
			{
				result = this.GetErrorDesignTimeHtml(e);
			}
			return result;
		}

		// Token: 0x0600051B RID: 1307 RVA: 0x00018A38 File Offset: 0x00016C38
		public override string GetEditableDesignerRegionContent(EditableDesignerRegion region)
		{
			ITemplate template = this.GetTemplate(this._changePassword);
			if (template == null)
			{
				return this.GetEmptyDesignTimeHtml();
			}
			IDesignerHost host = (IDesignerHost)this.GetService(typeof(IDesignerHost));
			return ControlPersister.PersistTemplate(template, host);
		}

		// Token: 0x0600051C RID: 1308 RVA: 0x00018A79 File Offset: 0x00016C79
		protected override string GetErrorDesignTimeHtml(Exception e)
		{
			return base.CreatePlaceHolderDesignTimeHtml(SR.GetString("Control_ErrorRenderingShort") + "<br />" + e.Message);
		}

		// Token: 0x0600051D RID: 1309 RVA: 0x00018A9C File Offset: 0x00016C9C
		private ITemplate GetTemplate(ChangePassword changePassword)
		{
			ITemplate result = null;
			ChangePasswordDesigner.ViewType currentView = this.CurrentView;
			if (currentView != ChangePasswordDesigner.ViewType.ChangePassword)
			{
				if (currentView == ChangePasswordDesigner.ViewType.Success)
				{
					result = changePassword.SuccessTemplate;
				}
			}
			else
			{
				result = changePassword.ChangePasswordTemplate;
			}
			return result;
		}

		// Token: 0x0600051E RID: 1310 RVA: 0x00018ACC File Offset: 0x00016CCC
		public override void Initialize(IComponent component)
		{
			ControlDesigner.VerifyInitializeArgument(component, typeof(ChangePassword));
			this._changePassword = (ChangePassword)component;
			base.Initialize(component);
		}

		// Token: 0x0600051F RID: 1311 RVA: 0x00018AF4 File Offset: 0x00016CF4
		private void LaunchWebAdmin()
		{
			IDesignerHost designerHost = (IDesignerHost)this.GetService(typeof(IDesignerHost));
			if (designerHost != null)
			{
				IWebAdministrationService webAdministrationService = (IWebAdministrationService)designerHost.GetService(typeof(IWebAdministrationService));
				if (webAdministrationService != null)
				{
					webAdministrationService.Start(null);
				}
			}
		}

		// Token: 0x06000520 RID: 1312 RVA: 0x00018B3A File Offset: 0x00016D3A
		private void ConvertToTemplate()
		{
			ControlDesigner.InvokeTransactedChange(base.Component, new TransactedChangeCallback(this.ConvertToTemplateChangeCallback), null, SR.GetString("WebControls_ConvertToTemplate"), this.TemplateDescriptor);
		}

		// Token: 0x06000521 RID: 1313 RVA: 0x00018B64 File Offset: 0x00016D64
		private void Reset()
		{
			this.UpdateDesignTimeHtml();
			ControlDesigner.InvokeTransactedChange(base.Component, new TransactedChangeCallback(this.ResetChangeCallback), null, SR.GetString("WebControls_Reset"), this.TemplateDescriptor);
		}

		// Token: 0x06000522 RID: 1314 RVA: 0x00018B94 File Offset: 0x00016D94
		protected override void PreFilterProperties(IDictionary properties)
		{
			base.PreFilterProperties(properties);
			if (this.Templated)
			{
				foreach (string key in ChangePasswordDesigner._nonTemplateProperties)
				{
					PropertyDescriptor propertyDescriptor = (PropertyDescriptor)properties[key];
					if (propertyDescriptor != null)
					{
						properties[key] = TypeDescriptor.CreateProperty(propertyDescriptor.ComponentType, propertyDescriptor, new Attribute[]
						{
							BrowsableAttribute.No
						});
					}
				}
			}
			RenderOuterTableHelper.SetupRenderOuterTable(properties, base.Component, false, base.GetType());
		}

		// Token: 0x06000523 RID: 1315 RVA: 0x00018C0C File Offset: 0x00016E0C
		private bool ResetChangeCallback(object context)
		{
			this.TemplateDescriptor.SetValue(base.Component, null);
			return true;
		}

		// Token: 0x06000524 RID: 1316 RVA: 0x00018C24 File Offset: 0x00016E24
		public override void SetEditableDesignerRegionContent(EditableDesignerRegion region, string content)
		{
			PropertyDescriptor propertyDescriptor = TypeDescriptor.GetProperties(base.Component)[region.Name];
			IDesignerHost designerHost = (IDesignerHost)this.GetService(typeof(IDesignerHost));
			ITemplate value = ControlParser.ParseTemplate(designerHost, content);
			using (DesignerTransaction designerTransaction = designerHost.CreateTransaction("SetEditableDesignerRegionContent"))
			{
				propertyDescriptor.SetValue(base.Component, value);
				designerTransaction.Commit();
			}
		}

		// Token: 0x0400027E RID: 638
		private static DesignerAutoFormatCollection _autoFormats;

		// Token: 0x0400027F RID: 639
		private ChangePassword _changePassword;

		// Token: 0x04000280 RID: 640
		private static readonly string[] _templateNames = new string[]
		{
			"ChangePasswordTemplate",
			"SuccessTemplate"
		};

		// Token: 0x04000281 RID: 641
		private static readonly string[] _changePasswordViewRegionToPropertyMap = new string[]
		{
			"ChangePasswordTitleText",
			"UserNameLabelText",
			"PasswordLabelText",
			"InstructionText",
			"PasswordHintText",
			"NewPasswordLabelText",
			"ConfirmNewPasswordLabelText"
		};

		// Token: 0x04000282 RID: 642
		private static readonly string[] _successViewRegionToPropertyMap = new string[]
		{
			"SuccessText",
			"SuccessTitleText"
		};

		// Token: 0x04000283 RID: 643
		private const string _failureTextID = "FailureText";

		// Token: 0x04000284 RID: 644
		private static readonly string[] _nonTemplateProperties = new string[]
		{
			"BorderPadding",
			"CancelButtonImageUrl",
			"CancelButtonStyle",
			"CancelButtonText",
			"CancelButtonType",
			"ChangePasswordButtonImageUrl",
			"ChangePasswordButtonStyle",
			"ChangePasswordButtonText",
			"ChangePasswordButtonType",
			"ChangePasswordTitleText",
			"ConfirmNewPasswordLabelText",
			"ConfirmPasswordCompareErrorMessage",
			"ConfirmPasswordRequiredErrorMessage",
			"ContinueButtonImageUrl",
			"ContinueButtonStyle",
			"ContinueButtonText",
			"ContinueButtonType",
			"CreateUserIconUrl",
			"CreateUserText",
			"CreateUserUrl",
			"DisplayUserName",
			"EditProfileText",
			"EditProfileIconUrl",
			"EditProfileUrl",
			"FailureTextStyle",
			"HelpPageIconUrl",
			"HelpPageText",
			"HelpPageUrl",
			"HyperLinkStyle",
			"InstructionText",
			"InstructionTextStyle",
			"LabelStyle",
			"NewPasswordLabelText",
			"NewPasswordRequiredErrorMessage",
			"NewPasswordRegularExpression",
			"NewPasswordRegularExpressionErrorMessage",
			"PasswordHintText",
			"PasswordHintStyle",
			"PasswordLabelText",
			"PasswordRecoveryText",
			"PasswordRecoveryUrl",
			"PasswordRecoveryIconUrl",
			"PasswordRequiredErrorMessage",
			"SuccessTitleText",
			"SuccessText",
			"SuccessTextStyle",
			"TextBoxStyle",
			"TitleTextStyle",
			"UserNameLabelText",
			"UserNameRequiredErrorMessage",
			"ValidatorTextStyle"
		};

		// Token: 0x020003D6 RID: 982
		private enum ViewType
		{
			// Token: 0x04001C1A RID: 7194
			ChangePassword,
			// Token: 0x04001C1B RID: 7195
			Success
		}

		// Token: 0x020003D7 RID: 983
		private class ChangePasswordDesignerActionList : DesignerActionList
		{
			// Token: 0x06002705 RID: 9989 RVA: 0x000F0E38 File Offset: 0x000EF038
			public ChangePasswordDesignerActionList(ChangePasswordDesigner designer) : base(designer.Component)
			{
				this._designer = designer;
			}

			// Token: 0x17000836 RID: 2102
			// (get) Token: 0x06002706 RID: 9990 RVA: 0x00003B0F File Offset: 0x00001D0F
			// (set) Token: 0x06002707 RID: 9991 RVA: 0x00003937 File Offset: 0x00001B37
			public override bool AutoShow
			{
				get
				{
					return true;
				}
				set
				{
				}
			}

			// Token: 0x17000837 RID: 2103
			// (get) Token: 0x06002708 RID: 9992 RVA: 0x000F0E4D File Offset: 0x000EF04D
			// (set) Token: 0x06002709 RID: 9993 RVA: 0x000F0E74 File Offset: 0x000EF074
			[TypeConverter(typeof(ChangePasswordDesigner.ChangePasswordDesignerActionList.ChangePasswordViewTypeConverter))]
			public string View
			{
				get
				{
					if (this._designer.CurrentView == ChangePasswordDesigner.ViewType.ChangePassword)
					{
						return SR.GetString("ChangePassword_ChangePasswordView");
					}
					return SR.GetString("ChangePassword_SuccessView");
				}
				set
				{
					if (string.Compare(value, SR.GetString("ChangePassword_ChangePasswordView"), StringComparison.Ordinal) == 0)
					{
						this._designer.CurrentView = ChangePasswordDesigner.ViewType.ChangePassword;
					}
					else if (string.Compare(value, SR.GetString("ChangePassword_SuccessView"), StringComparison.Ordinal) == 0)
					{
						this._designer.CurrentView = ChangePasswordDesigner.ViewType.Success;
					}
					TypeDescriptor.Refresh(this._designer.Component);
					this._designer.UpdateDesignTimeHtml();
				}
			}

			// Token: 0x0600270A RID: 9994 RVA: 0x000F0EDC File Offset: 0x000EF0DC
			public void ConvertToTemplate()
			{
				Cursor value = Cursor.Current;
				try
				{
					Cursor.Current = Cursors.WaitCursor;
					this._designer.ConvertToTemplate();
				}
				finally
				{
					Cursor.Current = value;
				}
			}

			// Token: 0x0600270B RID: 9995 RVA: 0x000F0F20 File Offset: 0x000EF120
			public void LaunchWebAdmin()
			{
				this._designer.LaunchWebAdmin();
			}

			// Token: 0x0600270C RID: 9996 RVA: 0x000F0F30 File Offset: 0x000EF130
			public override DesignerActionItemCollection GetSortedActionItems()
			{
				DesignerActionItemCollection designerActionItemCollection = new DesignerActionItemCollection();
				designerActionItemCollection.Add(new DesignerActionPropertyItem("View", SR.GetString("WebControls_Views"), string.Empty, SR.GetString("WebControls_ViewsDescription"))
				{
					ShowInSourceView = false
				});
				if (this._designer.Templated)
				{
					designerActionItemCollection.Add(new DesignerActionMethodItem(this, "Reset", SR.GetString("WebControls_Reset"), string.Empty, SR.GetString("WebControls_ResetDescriptionViews"), true));
				}
				else
				{
					designerActionItemCollection.Add(new DesignerActionMethodItem(this, "ConvertToTemplate", SR.GetString("WebControls_ConvertToTemplate"), string.Empty, SR.GetString("WebControls_ConvertToTemplateDescriptionViews"), true));
				}
				designerActionItemCollection.Add(new DesignerActionMethodItem(this, "LaunchWebAdmin", SR.GetString("Login_LaunchWebAdmin"), string.Empty, SR.GetString("Login_LaunchWebAdminDescription"), true));
				return designerActionItemCollection;
			}

			// Token: 0x0600270D RID: 9997 RVA: 0x000F1008 File Offset: 0x000EF208
			public void Reset()
			{
				this._designer.Reset();
			}

			// Token: 0x04001C1C RID: 7196
			private ChangePasswordDesigner _designer;

			// Token: 0x020005BF RID: 1471
			private class ChangePasswordViewTypeConverter : TypeConverter
			{
				// Token: 0x060033F2 RID: 13298 RVA: 0x0011C09C File Offset: 0x0011A29C
				public override TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
				{
					return new TypeConverter.StandardValuesCollection(new string[]
					{
						SR.GetString("ChangePassword_ChangePasswordView"),
						SR.GetString("ChangePassword_SuccessView")
					});
				}

				// Token: 0x060033F3 RID: 13299 RVA: 0x00003B0F File Offset: 0x00001D0F
				public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
				{
					return true;
				}

				// Token: 0x060033F4 RID: 13300 RVA: 0x00003B0F File Offset: 0x00001D0F
				public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
				{
					return true;
				}
			}
		}

		// Token: 0x020003D8 RID: 984
		private sealed class ConvertToTemplateHelper : LoginDesignerUtil.GenericConvertToTemplateHelper<ChangePassword, ChangePasswordDesigner>
		{
			// Token: 0x0600270E RID: 9998 RVA: 0x000F1015 File Offset: 0x000EF215
			public ConvertToTemplateHelper(ChangePasswordDesigner designer, IDesignerHost designerHost) : base(designer, designerHost)
			{
			}

			// Token: 0x17000838 RID: 2104
			// (get) Token: 0x0600270F RID: 9999 RVA: 0x000F101F File Offset: 0x000EF21F
			protected override string[] PersistedControlIDs
			{
				get
				{
					return ChangePasswordDesigner.ConvertToTemplateHelper._persistedControlIDs;
				}
			}

			// Token: 0x17000839 RID: 2105
			// (get) Token: 0x06002710 RID: 10000 RVA: 0x000F1026 File Offset: 0x000EF226
			protected override string[] PersistedIfNotVisibleControlIDs
			{
				get
				{
					return ChangePasswordDesigner.ConvertToTemplateHelper._persistedIfNotVisibleControlIDs;
				}
			}

			// Token: 0x06002711 RID: 10001 RVA: 0x000F102D File Offset: 0x000EF22D
			protected override Style GetFailureTextStyle(ChangePassword control)
			{
				return control.FailureTextStyle;
			}

			// Token: 0x06002712 RID: 10002 RVA: 0x000F1038 File Offset: 0x000EF238
			protected override Control GetDefaultTemplateContents()
			{
				Control control = null;
				ChangePasswordDesigner.ViewType currentView = base.Designer.CurrentView;
				if (currentView != ChangePasswordDesigner.ViewType.ChangePassword)
				{
					if (currentView == ChangePasswordDesigner.ViewType.Success)
					{
						control = base.Designer.ViewControl.Controls[1];
					}
				}
				else
				{
					control = base.Designer.ViewControl.Controls[0];
				}
				return (Table)control.Controls[0];
			}

			// Token: 0x06002713 RID: 10003 RVA: 0x000F109F File Offset: 0x000EF29F
			protected override ITemplate GetTemplate(ChangePassword control)
			{
				return base.Designer.GetTemplate(control);
			}

			// Token: 0x04001C1D RID: 7197
			private static readonly string[] _persistedControlIDs = new string[]
			{
				"UserName",
				"UserNameRequired",
				"CurrentPassword",
				"CurrentPasswordRequired",
				"NewPassword",
				"NewPasswordRequired",
				"NewPasswordRegExp",
				"ConfirmNewPassword",
				"ConfirmNewPasswordRequired",
				"NewPasswordCompare",
				"ChangePasswordPushButton",
				"ChangePasswordImageButton",
				"ChangePasswordLinkButton",
				"CancelPushButton",
				"CancelImageButton",
				"CancelLinkButton",
				"ContinuePushButton",
				"ContinueImageButton",
				"ContinueLinkButton",
				"FailureText",
				"HelpLink",
				"CreateUserLink",
				"PasswordRecoveryLink",
				"EditProfileLink",
				"EditProfileLinkSuccess"
			};

			// Token: 0x04001C1E RID: 7198
			private static readonly string[] _persistedIfNotVisibleControlIDs = new string[]
			{
				"FailureText"
			};
		}
	}
}
