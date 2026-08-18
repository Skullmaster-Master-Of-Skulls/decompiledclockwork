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
	// Token: 0x020000DE RID: 222
	[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
	public class LoginDesigner : CompositeControlDesigner
	{
		// Token: 0x170001C1 RID: 449
		// (get) Token: 0x06000776 RID: 1910 RVA: 0x000291CC File Offset: 0x000273CC
		public override DesignerActionListCollection ActionLists
		{
			get
			{
				DesignerActionListCollection designerActionListCollection = new DesignerActionListCollection();
				designerActionListCollection.AddRange(base.ActionLists);
				designerActionListCollection.Add(new LoginDesigner.LoginDesignerActionList(this));
				return designerActionListCollection;
			}
		}

		// Token: 0x170001C2 RID: 450
		// (get) Token: 0x06000777 RID: 1911 RVA: 0x000291F9 File Offset: 0x000273F9
		public override bool AllowResize
		{
			get
			{
				return this.RenderOuterTable && base.AllowResize;
			}
		}

		// Token: 0x170001C3 RID: 451
		// (get) Token: 0x06000778 RID: 1912 RVA: 0x0002920B File Offset: 0x0002740B
		// (set) Token: 0x06000779 RID: 1913 RVA: 0x000187F3 File Offset: 0x000169F3
		public bool RenderOuterTable
		{
			get
			{
				return ((Login)base.Component).RenderOuterTable;
			}
			set
			{
				RenderOuterTableHelper.SetRenderOuterTable(value, this, false);
			}
		}

		// Token: 0x170001C4 RID: 452
		// (get) Token: 0x0600077A RID: 1914 RVA: 0x0002921D File Offset: 0x0002741D
		public override DesignerAutoFormatCollection AutoFormats
		{
			get
			{
				if (LoginDesigner._autoFormats == null)
				{
					LoginDesigner._autoFormats = ControlDesigner.CreateAutoFormats(AutoFormatSchemes.LOGIN_SCHEME_NAMES, (string schemeName) => new LoginAutoFormat(schemeName, "<Schemes>\r\n<xsd:schema id=\"Schemes\" xmlns=\"\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns:msdata=\"urn:schemas-microsoft-com:xml-msdata\">\r\n  <xsd:element name=\"Scheme\">\r\n     <xsd:complexType>\r\n       <xsd:all>\r\n        <xsd:element name=\"SchemeName\" type=\"xsd:string\"/>\r\n        <xsd:element name=\"BackColor\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n        <xsd:element name=\"ForeColor\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n        <xsd:element name=\"BorderColor\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n        <xsd:element name=\"BorderWidth\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n        <xsd:element name=\"BorderStyle\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n        <xsd:element name=\"BorderPadding\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n        <xsd:element name=\"FontSize\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n        <xsd:element name=\"FontName\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n        <xsd:element name=\"TextLayout\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n        <xsd:element name=\"TitleTextBackColor\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n        <xsd:element name=\"TitleTextForeColor\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n        <xsd:element name=\"TitleTextFont\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n        <xsd:element name=\"TitleTextFontSize\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n        <xsd:element name=\"InstructionTextForeColor\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n        <xsd:element name=\"InstructionTextFont\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n        <xsd:element name=\"TextboxFontSize\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n        <xsd:element name=\"SubmitButtonBackColor\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n        <xsd:element name=\"SubmitButtonForeColor\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n        <xsd:element name=\"SubmitButtonFontSize\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n        <xsd:element name=\"SubmitButtonFontName\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n        <xsd:element name=\"SubmitButtonBorderColor\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n        <xsd:element name=\"SubmitButtonBorderWidth\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n        <xsd:element name=\"SubmitButtonBorderStyle\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n        <xsd:element name=\"RenderOuterTable\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n      </xsd:all>\r\n    </xsd:complexType>\r\n  </xsd:element>\r\n  <xsd:element name=\"Schemes\" msdata:IsDataSet=\"true\">\r\n    <xsd:complexType>\r\n      <xsd:choice maxOccurs=\"unbounded\">\r\n        <xsd:element ref=\"Scheme\"/>\r\n      </xsd:choice>\r\n    </xsd:complexType>\r\n  </xsd:element>\r\n</xsd:schema>\r\n<Scheme>\r\n  <SchemeName>LoginScheme_Empty</SchemeName>\r\n  <RenderOuterTable>True</RenderOuterTable>\r\n</Scheme>\r\n<Scheme>\r\n  <SchemeName>LoginScheme_Elegant</SchemeName>\r\n  <BackColor>#F7F7DE</BackColor>\r\n  <BorderColor>#CCCC99</BorderColor>\r\n  <BorderWidth>1</BorderWidth>\r\n  <BorderStyle>4</BorderStyle>\r\n  <FontSize>10</FontSize>\r\n  <FontName>Verdana</FontName>\r\n  <TitleTextBackColor>#6B696B</TitleTextBackColor>\r\n  <TitleTextForeColor>#FFFFFF</TitleTextForeColor>\r\n  <TitleTextFont>1</TitleTextFont>\r\n  <RenderOuterTable>True</RenderOuterTable>\r\n</Scheme>\r\n<Scheme>\r\n  <SchemeName>LoginScheme_Professional</SchemeName>\r\n  <BackColor>#F7F6F3</BackColor>\r\n  <ForeColor>#333333</ForeColor>\r\n  <BorderColor>#E6E2D8</BorderColor>\r\n  <BorderWidth>1</BorderWidth>\r\n  <BorderStyle>4</BorderStyle>\r\n  <BorderPadding>4</BorderPadding>\r\n  <FontSize>0.8em</FontSize>\r\n  <FontName>Verdana</FontName>\r\n  <TitleTextBackColor>#5D7B9D</TitleTextBackColor>\r\n  <TitleTextForeColor>White</TitleTextForeColor>\r\n  <TitleTextFont>1</TitleTextFont>\r\n  <TitleTextFontSize>0.9em</TitleTextFontSize>\r\n  <InstructionTextForeColor>Black</InstructionTextForeColor>\r\n  <InstructionTextFont>2</InstructionTextFont>\r\n  <TextboxFontSize>0.8em</TextboxFontSize>\r\n  <SubmitButtonBackColor>#FFFBFF</SubmitButtonBackColor>\r\n  <SubmitButtonForeColor>#284775</SubmitButtonForeColor>\r\n  <SubmitButtonFontSize>0.8em</SubmitButtonFontSize>\r\n  <SubmitButtonFontName>Verdana</SubmitButtonFontName>\r\n  <SubmitButtonBorderColor>#CCCCCC</SubmitButtonBorderColor>\r\n  <SubmitButtonBorderWidth>1</SubmitButtonBorderWidth>\r\n  <SubmitButtonBorderStyle>4</SubmitButtonBorderStyle>\r\n  <RenderOuterTable>True</RenderOuterTable>\r\n</Scheme>\r\n<Scheme>\r\n  <SchemeName>LoginScheme_Simple</SchemeName>\r\n  <BackColor>#E3EAEB</BackColor>\r\n  <ForeColor>#333333</ForeColor>\r\n  <BorderColor>#E6E2D8</BorderColor>\r\n  <BorderWidth>1</BorderWidth>\r\n  <BorderStyle>4</BorderStyle>\r\n  <BorderPadding>4</BorderPadding>\r\n  <FontSize>0.8em</FontSize>\r\n  <FontName>Verdana</FontName>\r\n  <TextLayout>1</TextLayout>\r\n  <TitleTextBackColor>#1C5E55</TitleTextBackColor>\r\n  <TitleTextForeColor>White</TitleTextForeColor>\r\n  <TitleTextFont>1</TitleTextFont>\r\n  <TitleTextFontSize>0.9em</TitleTextFontSize>\r\n  <InstructionTextForeColor>Black</InstructionTextForeColor>\r\n  <InstructionTextFont>2</InstructionTextFont>\r\n  <TextboxFontSize>0.8em</TextboxFontSize>\r\n  <SubmitButtonBackColor>White</SubmitButtonBackColor>\r\n  <SubmitButtonForeColor>#1C5E55</SubmitButtonForeColor>\r\n  <SubmitButtonFontSize>0.8em</SubmitButtonFontSize>\r\n  <SubmitButtonFontName>Verdana</SubmitButtonFontName>\r\n  <SubmitButtonBorderColor>#C5BBAF</SubmitButtonBorderColor>\r\n  <SubmitButtonBorderWidth>1</SubmitButtonBorderWidth>\r\n  <SubmitButtonBorderStyle>4</SubmitButtonBorderStyle>\r\n  <RenderOuterTable>True</RenderOuterTable>\r\n</Scheme>\r\n<Scheme>\r\n  <SchemeName>LoginScheme_Classic</SchemeName>\r\n  <BackColor>#EFF3FB</BackColor>\r\n  <ForeColor>#333333</ForeColor>\r\n  <BorderColor>#B5C7DE</BorderColor>\r\n  <BorderWidth>1</BorderWidth>\r\n  <BorderStyle>4</BorderStyle>\r\n  <BorderPadding>4</BorderPadding>\r\n  <FontSize>0.8em</FontSize>\r\n  <FontName>Verdana</FontName>\r\n  <TitleTextBackColor>#507CD1</TitleTextBackColor>\r\n  <TitleTextForeColor>White</TitleTextForeColor>\r\n  <TitleTextFont>1</TitleTextFont>\r\n  <TitleTextFontSize>0.9em</TitleTextFontSize>\r\n  <InstructionTextForeColor>Black</InstructionTextForeColor>\r\n  <InstructionTextFont>2</InstructionTextFont>\r\n  <TextboxFontSize>0.8em</TextboxFontSize>\r\n  <SubmitButtonBackColor>White</SubmitButtonBackColor>\r\n  <SubmitButtonForeColor>#284E98</SubmitButtonForeColor>\r\n  <SubmitButtonFontSize>0.8em</SubmitButtonFontSize>\r\n  <SubmitButtonFontName>Verdana</SubmitButtonFontName>\r\n  <SubmitButtonBorderColor>#507CD1</SubmitButtonBorderColor>\r\n  <SubmitButtonBorderWidth>1</SubmitButtonBorderWidth>\r\n  <SubmitButtonBorderStyle>4</SubmitButtonBorderStyle>\r\n  <RenderOuterTable>True</RenderOuterTable>\r\n</Scheme>\r\n<Scheme>\r\n  <SchemeName>LoginScheme_Colorful</SchemeName>\r\n  <BackColor>#FFFBD6</BackColor>\r\n  <ForeColor>#333333</ForeColor>\r\n  <BorderColor>#FFDFAD</BorderColor>\r\n  <BorderWidth>1</BorderWidth>\r\n  <BorderStyle>4</BorderStyle>\r\n  <BorderPadding>4</BorderPadding>\r\n  <FontSize>0.8em</FontSize>\r\n  <FontName>Verdana</FontName>\r\n  <TextLayout>1</TextLayout>\r\n  <TitleTextBackColor>#990000</TitleTextBackColor>\r\n  <TitleTextForeColor>White</TitleTextForeColor>\r\n  <TitleTextFont>1</TitleTextFont>\r\n  <TitleTextFontSize>0.9em</TitleTextFontSize>\r\n  <InstructionTextForeColor>Black</InstructionTextForeColor>\r\n  <InstructionTextFont>2</InstructionTextFont>\r\n  <TextboxFontSize>0.8em</TextboxFontSize>\r\n  <SubmitButtonBackColor>White</SubmitButtonBackColor>\r\n  <SubmitButtonForeColor>#990000</SubmitButtonForeColor>\r\n  <SubmitButtonFontSize>0.8em</SubmitButtonFontSize>\r\n  <SubmitButtonFontName>Verdana</SubmitButtonFontName>\r\n  <SubmitButtonBorderColor>#CC9966</SubmitButtonBorderColor>\r\n  <SubmitButtonBorderWidth>1</SubmitButtonBorderWidth>\r\n  <SubmitButtonBorderStyle>4</SubmitButtonBorderStyle>\r\n  <RenderOuterTable>True</RenderOuterTable>\r\n</Scheme>\r\n</Schemes>\r\n"));
				}
				return LoginDesigner._autoFormats;
			}
		}

		// Token: 0x170001C5 RID: 453
		// (get) Token: 0x0600077B RID: 1915 RVA: 0x00029259 File Offset: 0x00027459
		private bool Templated
		{
			get
			{
				return this._login.LayoutTemplate != null;
			}
		}

		// Token: 0x170001C6 RID: 454
		// (get) Token: 0x0600077C RID: 1916 RVA: 0x00029269 File Offset: 0x00027469
		private TemplateDefinition TemplateDefinition
		{
			get
			{
				return new TemplateDefinition(this, "LayoutTemplate", this._login, "LayoutTemplate", ((WebControl)base.ViewControl).ControlStyle);
			}
		}

		// Token: 0x170001C7 RID: 455
		// (get) Token: 0x0600077D RID: 1917 RVA: 0x00029294 File Offset: 0x00027494
		private PropertyDescriptor TemplateDescriptor
		{
			get
			{
				PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(base.Component);
				return properties.Find("LayoutTemplate", false);
			}
		}

		// Token: 0x170001C8 RID: 456
		// (get) Token: 0x0600077E RID: 1918 RVA: 0x000292BC File Offset: 0x000274BC
		public override TemplateGroupCollection TemplateGroups
		{
			get
			{
				TemplateGroupCollection templateGroups = base.TemplateGroups;
				TemplateGroup templateGroup = new TemplateGroup("LayoutTemplate", ((WebControl)base.ViewControl).ControlStyle);
				templateGroup.AddTemplateDefinition(new TemplateDefinition(this, "LayoutTemplate", this._login, "LayoutTemplate", ((WebControl)base.ViewControl).ControlStyle));
				templateGroups.Add(templateGroup);
				return templateGroups;
			}
		}

		// Token: 0x170001C9 RID: 457
		// (get) Token: 0x0600077F RID: 1919 RVA: 0x00003B0F File Offset: 0x00001D0F
		protected override bool UsePreviewControl
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06000780 RID: 1920 RVA: 0x00029320 File Offset: 0x00027520
		private void ConvertToTemplate()
		{
			ControlDesigner.InvokeTransactedChange(base.Component, new TransactedChangeCallback(this.ConvertToTemplateChangeCallback), null, SR.GetString("WebControls_ConvertToTemplate"), this.TemplateDescriptor);
		}

		// Token: 0x06000781 RID: 1921 RVA: 0x0002934C File Offset: 0x0002754C
		private bool ConvertToTemplateChangeCallback(object context)
		{
			bool result;
			try
			{
				IDesignerHost designerHost = (IDesignerHost)this.GetService(typeof(IDesignerHost));
				LoginDesigner.ConvertToTemplateHelper convertToTemplateHelper = new LoginDesigner.ConvertToTemplateHelper(this, designerHost);
				ITemplate value = convertToTemplateHelper.ConvertToTemplate();
				this.TemplateDescriptor.SetValue(this._login, value);
				result = true;
			}
			catch (Exception ex)
			{
				result = false;
			}
			return result;
		}

		// Token: 0x06000782 RID: 1922 RVA: 0x00018A79 File Offset: 0x00016C79
		protected override string GetErrorDesignTimeHtml(Exception e)
		{
			return base.CreatePlaceHolderDesignTimeHtml(SR.GetString("Control_ErrorRenderingShort") + "<br />" + e.Message);
		}

		// Token: 0x06000783 RID: 1923 RVA: 0x000293AC File Offset: 0x000275AC
		public override void Initialize(IComponent component)
		{
			ControlDesigner.VerifyInitializeArgument(component, typeof(Login));
			this._login = (Login)component;
			base.Initialize(component);
		}

		// Token: 0x06000784 RID: 1924 RVA: 0x000293D4 File Offset: 0x000275D4
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

		// Token: 0x06000785 RID: 1925 RVA: 0x0002941C File Offset: 0x0002761C
		protected override void PreFilterProperties(IDictionary properties)
		{
			base.PreFilterProperties(properties);
			if (this.Templated)
			{
				foreach (string key in LoginDesigner._nonTemplateProperties)
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

		// Token: 0x06000786 RID: 1926 RVA: 0x00029494 File Offset: 0x00027694
		private void Reset()
		{
			this.UpdateDesignTimeHtml();
			ControlDesigner.InvokeTransactedChange(base.Component, new TransactedChangeCallback(this.ResetChangeCallback), null, SR.GetString("WebControls_Reset"), this.TemplateDescriptor);
		}

		// Token: 0x06000787 RID: 1927 RVA: 0x000294C4 File Offset: 0x000276C4
		private bool ResetChangeCallback(object context)
		{
			this.TemplateDescriptor.SetValue(this._login, null);
			return true;
		}

		// Token: 0x06000788 RID: 1928 RVA: 0x000294DC File Offset: 0x000276DC
		public override string GetDesignTimeHtml(DesignerRegionCollection regions)
		{
			bool flag = base.UseRegions(regions, this._login.LayoutTemplate);
			if (flag)
			{
				((WebControl)base.ViewControl).Enabled = true;
				IDictionary dictionary = new HybridDictionary(1);
				dictionary.Add("RegionEditing", true);
				((IControlDesignerAccessor)base.ViewControl).SetDesignModeState(dictionary);
				regions.Add(new TemplatedEditableDesignerRegion(this.TemplateDefinition)
				{
					Description = SR.GetString("ContainerControlDesigner_RegionWatermark")
				});
			}
			return this.GetDesignTimeHtml();
		}

		// Token: 0x06000789 RID: 1929 RVA: 0x00029560 File Offset: 0x00027760
		public override string GetEditableDesignerRegionContent(EditableDesignerRegion region)
		{
			IDesignerHost host = (IDesignerHost)this.GetService(typeof(IDesignerHost));
			return ControlPersister.PersistTemplate(this._login.LayoutTemplate, host);
		}

		// Token: 0x0600078A RID: 1930 RVA: 0x00029594 File Offset: 0x00027794
		public override void SetEditableDesignerRegionContent(EditableDesignerRegion region, string content)
		{
			IDesignerHost designerHost = (IDesignerHost)this.GetService(typeof(IDesignerHost));
			ITemplate value = ControlParser.ParseTemplate(designerHost, content);
			PropertyDescriptor propertyDescriptor = TypeDescriptor.GetProperties(base.Component)[region.Name];
			using (DesignerTransaction designerTransaction = designerHost.CreateTransaction("SetEditableDesignerRegionContent"))
			{
				propertyDescriptor.SetValue(base.Component, value);
				designerTransaction.Commit();
			}
		}

		// Token: 0x04000483 RID: 1155
		private Login _login;

		// Token: 0x04000484 RID: 1156
		private static DesignerAutoFormatCollection _autoFormats;

		// Token: 0x04000485 RID: 1157
		private const string _templateName = "LayoutTemplate";

		// Token: 0x04000486 RID: 1158
		private const string _failureTextID = "FailureText";

		// Token: 0x04000487 RID: 1159
		private static readonly string[] _nonTemplateProperties = new string[]
		{
			"BorderPadding",
			"CheckBoxStyle",
			"CreateUserIconUrl",
			"CreateUserText",
			"CreateUserUrl",
			"DisplayRememberMe",
			"FailureTextStyle",
			"HelpPageIconUrl",
			"HelpPageText",
			"HelpPageUrl",
			"HyperLinkStyle",
			"InstructionText",
			"InstructionTextStyle",
			"LabelStyle",
			"Orientation",
			"PasswordLabelText",
			"PasswordRecoveryIconUrl",
			"PasswordRecoveryText",
			"PasswordRecoveryUrl",
			"PasswordRequiredErrorMessage",
			"RememberMeText",
			"LoginButtonImageUrl",
			"LoginButtonStyle",
			"LoginButtonText",
			"LoginButtonType",
			"TextBoxStyle",
			"TextLayout",
			"TitleText",
			"TitleTextStyle",
			"UserNameLabelText",
			"UserNameRequiredErrorMessage",
			"ValidatorTextStyle"
		};

		// Token: 0x02000405 RID: 1029
		private class LoginDesignerActionList : DesignerActionList
		{
			// Token: 0x060027B8 RID: 10168 RVA: 0x000F405A File Offset: 0x000F225A
			public LoginDesignerActionList(LoginDesigner parent) : base(parent.Component)
			{
				this._parent = parent;
			}

			// Token: 0x1700084B RID: 2123
			// (get) Token: 0x060027B9 RID: 10169 RVA: 0x00003B0F File Offset: 0x00001D0F
			// (set) Token: 0x060027BA RID: 10170 RVA: 0x00003937 File Offset: 0x00001B37
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

			// Token: 0x060027BB RID: 10171 RVA: 0x000F4070 File Offset: 0x000F2270
			public void ConvertToTemplate()
			{
				Cursor value = Cursor.Current;
				try
				{
					Cursor.Current = Cursors.WaitCursor;
					this._parent.ConvertToTemplate();
				}
				finally
				{
					Cursor.Current = value;
				}
			}

			// Token: 0x060027BC RID: 10172 RVA: 0x000F40B4 File Offset: 0x000F22B4
			public void LaunchWebAdmin()
			{
				this._parent.LaunchWebAdmin();
			}

			// Token: 0x060027BD RID: 10173 RVA: 0x000F40C1 File Offset: 0x000F22C1
			public void Reset()
			{
				this._parent.Reset();
			}

			// Token: 0x060027BE RID: 10174 RVA: 0x000F40D0 File Offset: 0x000F22D0
			public override DesignerActionItemCollection GetSortedActionItems()
			{
				if (this._parent.InTemplateMode)
				{
					return new DesignerActionItemCollection();
				}
				DesignerActionItemCollection designerActionItemCollection = new DesignerActionItemCollection();
				if (!this._parent.Templated)
				{
					designerActionItemCollection.Add(new DesignerActionMethodItem(this, "ConvertToTemplate", SR.GetString("WebControls_ConvertToTemplate"), string.Empty, SR.GetString("WebControls_ConvertToTemplateDescription"), true));
				}
				else
				{
					designerActionItemCollection.Add(new DesignerActionMethodItem(this, "Reset", SR.GetString("WebControls_Reset"), string.Empty, SR.GetString("WebControls_ResetDescription"), true));
				}
				designerActionItemCollection.Add(new DesignerActionMethodItem(this, "LaunchWebAdmin", SR.GetString("Login_LaunchWebAdmin"), string.Empty, SR.GetString("Login_LaunchWebAdminDescription"), true));
				return designerActionItemCollection;
			}

			// Token: 0x04001C6C RID: 7276
			private LoginDesigner _parent;
		}

		// Token: 0x02000406 RID: 1030
		private sealed class ConvertToTemplateHelper : LoginDesignerUtil.GenericConvertToTemplateHelper<Login, LoginDesigner>
		{
			// Token: 0x060027BF RID: 10175 RVA: 0x000F418A File Offset: 0x000F238A
			public ConvertToTemplateHelper(LoginDesigner designer, IDesignerHost designerHost) : base(designer, designerHost)
			{
			}

			// Token: 0x1700084C RID: 2124
			// (get) Token: 0x060027C0 RID: 10176 RVA: 0x000F4194 File Offset: 0x000F2394
			protected override string[] PersistedControlIDs
			{
				get
				{
					return LoginDesigner.ConvertToTemplateHelper._persistedControlIDs;
				}
			}

			// Token: 0x1700084D RID: 2125
			// (get) Token: 0x060027C1 RID: 10177 RVA: 0x000F419B File Offset: 0x000F239B
			protected override string[] PersistedIfNotVisibleControlIDs
			{
				get
				{
					return LoginDesigner.ConvertToTemplateHelper._persistedIfNotVisibleControlIDs;
				}
			}

			// Token: 0x060027C2 RID: 10178 RVA: 0x000F41A2 File Offset: 0x000F23A2
			protected override Style GetFailureTextStyle(Login control)
			{
				return control.FailureTextStyle;
			}

			// Token: 0x060027C3 RID: 10179 RVA: 0x000F41AC File Offset: 0x000F23AC
			protected override Control GetDefaultTemplateContents()
			{
				Control control = base.Designer.ViewControl.Controls[0];
				return (Table)control.Controls[0];
			}

			// Token: 0x060027C4 RID: 10180 RVA: 0x000F41E3 File Offset: 0x000F23E3
			protected override ITemplate GetTemplate(Login control)
			{
				return control.LayoutTemplate;
			}

			// Token: 0x04001C6D RID: 7277
			private static readonly string[] _persistedControlIDs = new string[]
			{
				"UserName",
				"UserNameRequired",
				"Password",
				"PasswordRequired",
				"RememberMe",
				"LoginButton",
				"LoginImageButton",
				"LoginLinkButton",
				"FailureText",
				"CreateUserLink",
				"PasswordRecoveryLink",
				"HelpLink"
			};

			// Token: 0x04001C6E RID: 7278
			private static readonly string[] _persistedIfNotVisibleControlIDs = new string[]
			{
				"FailureText"
			};
		}
	}
}
