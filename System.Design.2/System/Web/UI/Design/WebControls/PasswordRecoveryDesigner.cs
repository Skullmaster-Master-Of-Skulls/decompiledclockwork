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
	// Token: 0x020000F9 RID: 249
	[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
	public class PasswordRecoveryDesigner : ControlDesigner
	{
		// Token: 0x17000208 RID: 520
		// (get) Token: 0x060008AF RID: 2223 RVA: 0x00032790 File Offset: 0x00030990
		public override DesignerActionListCollection ActionLists
		{
			get
			{
				DesignerActionListCollection designerActionListCollection = new DesignerActionListCollection();
				designerActionListCollection.AddRange(base.ActionLists);
				designerActionListCollection.Add(new PasswordRecoveryDesigner.PasswordRecoveryDesignerActionList(this));
				return designerActionListCollection;
			}
		}

		// Token: 0x17000209 RID: 521
		// (get) Token: 0x060008B0 RID: 2224 RVA: 0x000327BD File Offset: 0x000309BD
		public override bool AllowResize
		{
			get
			{
				return this.RenderOuterTable && base.AllowResize;
			}
		}

		// Token: 0x1700020A RID: 522
		// (get) Token: 0x060008B1 RID: 2225 RVA: 0x000327CF File Offset: 0x000309CF
		public override DesignerAutoFormatCollection AutoFormats
		{
			get
			{
				if (PasswordRecoveryDesigner._autoFormats == null)
				{
					PasswordRecoveryDesigner._autoFormats = ControlDesigner.CreateAutoFormats(AutoFormatSchemes.PASSWORDRECOVERY_SCHEME_NAMES, (string schemeName) => new PasswordRecoveryAutoFormat(schemeName, "<Schemes>\r\n<xsd:schema id=\"Schemes\" xmlns=\"\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns:msdata=\"urn:schemas-microsoft-com:xml-msdata\">\r\n  <xsd:element name=\"Scheme\">\r\n     <xsd:complexType>\r\n       <xsd:all>\r\n        <xsd:element name=\"SchemeName\" type=\"xsd:string\"/>\r\n        <xsd:element name=\"BackColor\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n        <xsd:element name=\"ForeColor\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n        <xsd:element name=\"BorderColor\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n        <xsd:element name=\"BorderWidth\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n        <xsd:element name=\"BorderStyle\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n        <xsd:element name=\"BorderPadding\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n        <xsd:element name=\"FontSize\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n        <xsd:element name=\"FontName\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n        <xsd:element name=\"TitleTextBackColor\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n        <xsd:element name=\"TitleTextForeColor\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n        <xsd:element name=\"TitleTextFont\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n        <xsd:element name=\"TitleTextFontSize\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n        <xsd:element name=\"SuccessTextForeColor\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n        <xsd:element name=\"SuccessTextFont\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n        <xsd:element name=\"InstructionTextForeColor\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n        <xsd:element name=\"InstructionTextFont\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n        <xsd:element name=\"TextboxFontSize\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n        <xsd:element name=\"SubmitButtonBackColor\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n        <xsd:element name=\"SubmitButtonForeColor\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n        <xsd:element name=\"SubmitButtonFontSize\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n        <xsd:element name=\"SubmitButtonFontName\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n        <xsd:element name=\"SubmitButtonBorderColor\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n        <xsd:element name=\"SubmitButtonBorderWidth\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n        <xsd:element name=\"SubmitButtonBorderStyle\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n        <xsd:element name=\"RenderOuterTable\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n      </xsd:all>\r\n    </xsd:complexType>\r\n  </xsd:element>\r\n  <xsd:element name=\"Schemes\" msdata:IsDataSet=\"true\">\r\n    <xsd:complexType>\r\n      <xsd:choice maxOccurs=\"unbounded\">\r\n        <xsd:element ref=\"Scheme\"/>\r\n      </xsd:choice>\r\n    </xsd:complexType>\r\n  </xsd:element>\r\n</xsd:schema>\r\n<Scheme>\r\n  <SchemeName>PasswordRecoveryScheme_Empty</SchemeName>\r\n  <RenderOuterTable>True</RenderOuterTable>\r\n</Scheme>\r\n<Scheme>\r\n  <SchemeName>PasswordRecoveryScheme_Elegant</SchemeName>\r\n  <BackColor>#F7F7DE</BackColor>\r\n  <BorderColor>#CCCC99</BorderColor>\r\n  <BorderWidth>1</BorderWidth>\r\n  <BorderStyle>4</BorderStyle>\r\n  <FontSize>10</FontSize>\r\n  <FontName>Verdana</FontName>\r\n  <TitleTextBackColor>#6B696B</TitleTextBackColor>\r\n  <TitleTextForeColor>#FFFFFF</TitleTextForeColor>\r\n  <TitleTextFont>1</TitleTextFont>\r\n  <RenderOuterTable>True</RenderOuterTable>\r\n</Scheme>\r\n<Scheme>\r\n  <SchemeName>PasswordRecoveryScheme_Professional</SchemeName>\r\n  <BackColor>#F7F6F3</BackColor>\r\n  <ForeColor>#333333</ForeColor>\r\n  <BorderColor>#E6E2D8</BorderColor>\r\n  <BorderWidth>1</BorderWidth>\r\n  <BorderStyle>4</BorderStyle>\r\n  <BorderPadding>4</BorderPadding>\r\n  <FontSize>0.8em</FontSize>\r\n  <FontName>Verdana</FontName>\r\n  <TitleTextBackColor>#5D7B9D</TitleTextBackColor>\r\n  <TitleTextForeColor>White</TitleTextForeColor>\r\n  <TitleTextFont>1</TitleTextFont>\r\n  <TitleTextFontSize>0.9em</TitleTextFontSize>\r\n  <InstructionTextForeColor>Black</InstructionTextForeColor>\r\n  <InstructionTextFont>2</InstructionTextFont>\r\n  <SuccessTextForeColor>#5D7B9D</SuccessTextForeColor>\r\n  <SuccessTextFont>1</SuccessTextFont>\r\n  <TextboxFontSize>0.8em</TextboxFontSize>\r\n  <SubmitButtonBackColor>#FFFBFF</SubmitButtonBackColor>\r\n  <SubmitButtonForeColor>#284775</SubmitButtonForeColor>\r\n  <SubmitButtonFontSize>0.8em</SubmitButtonFontSize>\r\n  <SubmitButtonFontName>Verdana</SubmitButtonFontName>\r\n  <SubmitButtonBorderColor>#CCCCCC</SubmitButtonBorderColor>\r\n  <SubmitButtonBorderWidth>1</SubmitButtonBorderWidth>\r\n  <SubmitButtonBorderStyle>4</SubmitButtonBorderStyle>\r\n  <RenderOuterTable>True</RenderOuterTable>\r\n</Scheme>\r\n<Scheme>\r\n  <SchemeName>PasswordRecoveryScheme_Simple</SchemeName>\r\n  <BackColor>#E3EAEB</BackColor>\r\n  <ForeColor>#333333</ForeColor>\r\n  <BorderColor>#E6E2D8</BorderColor>\r\n  <BorderWidth>1</BorderWidth>\r\n  <BorderStyle>4</BorderStyle>\r\n  <BorderPadding>4</BorderPadding>\r\n  <FontSize>0.8em</FontSize>\r\n  <FontName>Verdana</FontName>\r\n  <TitleTextBackColor>#1C5E55</TitleTextBackColor>\r\n  <TitleTextForeColor>White</TitleTextForeColor>\r\n  <TitleTextFont>1</TitleTextFont>\r\n  <TitleTextFontSize>0.9em</TitleTextFontSize>\r\n  <InstructionTextForeColor>Black</InstructionTextForeColor>\r\n  <InstructionTextFont>2</InstructionTextFont>\r\n  <SuccessTextForeColor>#1C5E55</SuccessTextForeColor>\r\n  <SuccessTextFont>1</SuccessTextFont>\r\n  <TextboxFontSize>0.8em</TextboxFontSize>\r\n  <SubmitButtonBackColor>White</SubmitButtonBackColor>\r\n  <SubmitButtonForeColor>#1C5E55</SubmitButtonForeColor>\r\n  <SubmitButtonFontSize>0.8em</SubmitButtonFontSize>\r\n  <SubmitButtonFontName>Verdana</SubmitButtonFontName>\r\n  <SubmitButtonBorderColor>#C5BBAF</SubmitButtonBorderColor>\r\n  <SubmitButtonBorderWidth>1</SubmitButtonBorderWidth>\r\n  <SubmitButtonBorderStyle>4</SubmitButtonBorderStyle>\r\n  <RenderOuterTable>True</RenderOuterTable>\r\n</Scheme>\r\n<Scheme>\r\n  <SchemeName>PasswordRecoveryScheme_Classic</SchemeName>\r\n  <BackColor>#EFF3FB</BackColor>\r\n  <ForeColor>#333333</ForeColor>\r\n  <BorderColor>#B5C7DE</BorderColor>\r\n  <BorderWidth>1</BorderWidth>\r\n  <BorderStyle>4</BorderStyle>\r\n  <BorderPadding>4</BorderPadding>\r\n  <FontSize>0.8em</FontSize>\r\n  <FontName>Verdana</FontName>\r\n  <TitleTextBackColor>#507CD1</TitleTextBackColor>\r\n  <TitleTextForeColor>White</TitleTextForeColor>\r\n  <TitleTextFont>1</TitleTextFont>\r\n  <TitleTextFontSize>0.9em</TitleTextFontSize>\r\n  <InstructionTextForeColor>Black</InstructionTextForeColor>\r\n  <InstructionTextFont>2</InstructionTextFont>\r\n  <SuccessTextForeColor>#507CD1</SuccessTextForeColor>\r\n  <SuccessTextFont>1</SuccessTextFont>\r\n  <TextboxFontSize>0.8em</TextboxFontSize>\r\n  <SubmitButtonBackColor>White</SubmitButtonBackColor>\r\n  <SubmitButtonForeColor>#284E98</SubmitButtonForeColor>\r\n  <SubmitButtonFontSize>0.8em</SubmitButtonFontSize>\r\n  <SubmitButtonFontName>Verdana</SubmitButtonFontName>\r\n  <SubmitButtonBorderColor>#507CD1</SubmitButtonBorderColor>\r\n  <SubmitButtonBorderWidth>1</SubmitButtonBorderWidth>\r\n  <SubmitButtonBorderStyle>4</SubmitButtonBorderStyle>\r\n  <RenderOuterTable>True</RenderOuterTable>\r\n</Scheme>\r\n<Scheme>\r\n  <SchemeName>PasswordRecoveryScheme_Colorful</SchemeName>\r\n  <BackColor>#FFFBD6</BackColor>\r\n  <ForeColor>#333333</ForeColor>\r\n  <BorderColor>#FFDFAD</BorderColor>\r\n  <BorderWidth>1</BorderWidth>\r\n  <BorderStyle>4</BorderStyle>\r\n  <BorderPadding>4</BorderPadding>\r\n  <FontSize>0.8em</FontSize>\r\n  <FontName>Verdana</FontName>\r\n  <TitleTextBackColor>#990000</TitleTextBackColor>\r\n  <TitleTextForeColor>White</TitleTextForeColor>\r\n  <TitleTextFont>1</TitleTextFont>\r\n  <TitleTextFontSize>0.9em</TitleTextFontSize>\r\n  <InstructionTextForeColor>Black</InstructionTextForeColor>\r\n  <InstructionTextFont>2</InstructionTextFont>\r\n  <SuccessTextForeColor>#990000</SuccessTextForeColor>\r\n  <SuccessTextFont>1</SuccessTextFont>\r\n  <TextboxFontSize>0.8em</TextboxFontSize>\r\n  <SubmitButtonBackColor>White</SubmitButtonBackColor>\r\n  <SubmitButtonForeColor>#990000</SubmitButtonForeColor>\r\n  <SubmitButtonFontSize>0.8em</SubmitButtonFontSize>\r\n  <SubmitButtonFontName>Verdana</SubmitButtonFontName>\r\n  <SubmitButtonBorderColor>#CC9966</SubmitButtonBorderColor>\r\n  <SubmitButtonBorderWidth>1</SubmitButtonBorderWidth>\r\n  <SubmitButtonBorderStyle>4</SubmitButtonBorderStyle>\r\n  <RenderOuterTable>True</RenderOuterTable>\r\n</Scheme>\r\n</Schemes>\r\n"));
				}
				return PasswordRecoveryDesigner._autoFormats;
			}
		}

		// Token: 0x1700020B RID: 523
		// (get) Token: 0x060008B2 RID: 2226 RVA: 0x0003280C File Offset: 0x00030A0C
		// (set) Token: 0x060008B3 RID: 2227 RVA: 0x00032835 File Offset: 0x00030A35
		private PasswordRecoveryDesigner.ViewType CurrentView
		{
			get
			{
				object obj = base.DesignerState["CurrentView"];
				if (obj != null)
				{
					return (PasswordRecoveryDesigner.ViewType)obj;
				}
				return PasswordRecoveryDesigner.ViewType.UserName;
			}
			set
			{
				base.DesignerState["CurrentView"] = value;
			}
		}

		// Token: 0x1700020C RID: 524
		// (get) Token: 0x060008B4 RID: 2228 RVA: 0x0003284D File Offset: 0x00030A4D
		// (set) Token: 0x060008B5 RID: 2229 RVA: 0x000187F3 File Offset: 0x000169F3
		public bool RenderOuterTable
		{
			get
			{
				return ((PasswordRecovery)base.Component).RenderOuterTable;
			}
			set
			{
				RenderOuterTableHelper.SetRenderOuterTable(value, this, false);
			}
		}

		// Token: 0x1700020D RID: 525
		// (get) Token: 0x060008B6 RID: 2230 RVA: 0x0003285F File Offset: 0x00030A5F
		private bool Templated
		{
			get
			{
				return this.GetTemplate(this._passwordRecovery) != null;
			}
		}

		// Token: 0x1700020E RID: 526
		// (get) Token: 0x060008B7 RID: 2231 RVA: 0x00032870 File Offset: 0x00030A70
		private TemplateDefinition TemplateDefinition
		{
			get
			{
				string text = PasswordRecoveryDesigner._templateNames[(int)this.CurrentView];
				return new TemplateDefinition(this, text, this._passwordRecovery, text, ((WebControl)base.ViewControl).ControlStyle);
			}
		}

		// Token: 0x1700020F RID: 527
		// (get) Token: 0x060008B8 RID: 2232 RVA: 0x000328A8 File Offset: 0x00030AA8
		private PropertyDescriptor TemplateDescriptor
		{
			get
			{
				PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(base.Component);
				string name = PasswordRecoveryDesigner._templateNames[(int)this.CurrentView];
				return properties.Find(name, false);
			}
		}

		// Token: 0x17000210 RID: 528
		// (get) Token: 0x060008B9 RID: 2233 RVA: 0x000328D8 File Offset: 0x00030AD8
		public override TemplateGroupCollection TemplateGroups
		{
			get
			{
				TemplateGroupCollection templateGroups = base.TemplateGroups;
				TemplateGroupCollection templateGroupCollection = new TemplateGroupCollection();
				for (int i = 0; i < PasswordRecoveryDesigner._templateNames.Length; i++)
				{
					string text = PasswordRecoveryDesigner._templateNames[i];
					TemplateGroup templateGroup = new TemplateGroup(text, ((WebControl)base.ViewControl).ControlStyle);
					templateGroup.AddTemplateDefinition(new TemplateDefinition(this, text, this._passwordRecovery, text, ((WebControl)base.ViewControl).ControlStyle));
					templateGroupCollection.Add(templateGroup);
				}
				templateGroups.AddRange(templateGroupCollection);
				return templateGroups;
			}
		}

		// Token: 0x17000211 RID: 529
		// (get) Token: 0x060008BA RID: 2234 RVA: 0x00003B0F File Offset: 0x00001D0F
		protected override bool UsePreviewControl
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060008BB RID: 2235 RVA: 0x0003295C File Offset: 0x00030B5C
		private bool ConvertToTemplateChangeCallback(object context)
		{
			bool result;
			try
			{
				IDesignerHost designerHost = (IDesignerHost)this.GetService(typeof(IDesignerHost));
				PasswordRecoveryDesigner.ConvertToTemplateHelper convertToTemplateHelper = new PasswordRecoveryDesigner.ConvertToTemplateHelper(this, designerHost);
				ITemplate value = convertToTemplateHelper.ConvertToTemplate();
				this.TemplateDescriptor.SetValue(this._passwordRecovery, value);
				result = true;
			}
			catch (Exception ex)
			{
				result = false;
			}
			return result;
		}

		// Token: 0x060008BC RID: 2236 RVA: 0x000329BC File Offset: 0x00030BBC
		public override string GetDesignTimeHtml()
		{
			string result;
			try
			{
				IDictionary dictionary = new HybridDictionary(1);
				dictionary["CurrentView"] = this.CurrentView;
				((IControlDesignerAccessor)base.ViewControl).SetDesignModeState(dictionary);
				ICompositeControlDesignerAccessor compositeControlDesignerAccessor = (ICompositeControlDesignerAccessor)base.ViewControl;
				compositeControlDesignerAccessor.RecreateChildControls();
				result = base.GetDesignTimeHtml();
			}
			catch (Exception e)
			{
				result = this.GetErrorDesignTimeHtml(e);
			}
			return result;
		}

		// Token: 0x060008BD RID: 2237 RVA: 0x00032A2C File Offset: 0x00030C2C
		public override string GetDesignTimeHtml(DesignerRegionCollection regions)
		{
			bool flag = base.UseRegions(regions, this.GetTemplate(this._passwordRecovery));
			if (flag)
			{
				regions.Add(new TemplatedEditableDesignerRegion(this.TemplateDefinition)
				{
					Description = SR.GetString("ContainerControlDesigner_RegionWatermark")
				});
				((WebControl)base.ViewControl).Enabled = true;
				IDictionary dictionary = new HybridDictionary(1);
				dictionary.Add("RegionEditing", true);
				((IControlDesignerAccessor)base.ViewControl).SetDesignModeState(dictionary);
			}
			return this.GetDesignTimeHtml();
		}

		// Token: 0x060008BE RID: 2238 RVA: 0x00018A79 File Offset: 0x00016C79
		protected override string GetErrorDesignTimeHtml(Exception e)
		{
			return base.CreatePlaceHolderDesignTimeHtml(SR.GetString("Control_ErrorRenderingShort") + "<br />" + e.Message);
		}

		// Token: 0x060008BF RID: 2239 RVA: 0x00032AB0 File Offset: 0x00030CB0
		private ITemplate GetTemplate(PasswordRecovery passwordRecovery)
		{
			ITemplate result = null;
			switch (this.CurrentView)
			{
			case PasswordRecoveryDesigner.ViewType.UserName:
				result = passwordRecovery.UserNameTemplate;
				break;
			case PasswordRecoveryDesigner.ViewType.Question:
				result = passwordRecovery.QuestionTemplate;
				break;
			case PasswordRecoveryDesigner.ViewType.Success:
				result = passwordRecovery.SuccessTemplate;
				break;
			}
			return result;
		}

		// Token: 0x060008C0 RID: 2240 RVA: 0x00032AF4 File Offset: 0x00030CF4
		public override void Initialize(IComponent component)
		{
			ControlDesigner.VerifyInitializeArgument(component, typeof(PasswordRecovery));
			this._passwordRecovery = (PasswordRecovery)component;
			base.Initialize(component);
		}

		// Token: 0x060008C1 RID: 2241 RVA: 0x00032B1C File Offset: 0x00030D1C
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

		// Token: 0x060008C2 RID: 2242 RVA: 0x00032B62 File Offset: 0x00030D62
		private void ConvertToTemplate()
		{
			ControlDesigner.InvokeTransactedChange(base.Component, new TransactedChangeCallback(this.ConvertToTemplateChangeCallback), null, SR.GetString("WebControls_ConvertToTemplate"), this.TemplateDescriptor);
		}

		// Token: 0x060008C3 RID: 2243 RVA: 0x00032B8C File Offset: 0x00030D8C
		private void Reset()
		{
			this.UpdateDesignTimeHtml();
			ControlDesigner.InvokeTransactedChange(base.Component, new TransactedChangeCallback(this.ResetChangeCallback), null, SR.GetString("WebControls_Reset"), this.TemplateDescriptor);
		}

		// Token: 0x060008C4 RID: 2244 RVA: 0x00032BBC File Offset: 0x00030DBC
		protected override void PreFilterProperties(IDictionary properties)
		{
			base.PreFilterProperties(properties);
			if (this.Templated)
			{
				foreach (string key in PasswordRecoveryDesigner._nonTemplateProperties)
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

		// Token: 0x060008C5 RID: 2245 RVA: 0x00032C34 File Offset: 0x00030E34
		private bool ResetChangeCallback(object context)
		{
			bool result;
			try
			{
				this.TemplateDescriptor.SetValue(this._passwordRecovery, null);
				result = true;
			}
			catch (Exception ex)
			{
				result = false;
			}
			return result;
		}

		// Token: 0x060008C6 RID: 2246 RVA: 0x00032C70 File Offset: 0x00030E70
		public override string GetEditableDesignerRegionContent(EditableDesignerRegion region)
		{
			ITemplate template = this.GetTemplate(this._passwordRecovery);
			if (template == null)
			{
				return string.Empty;
			}
			IDesignerHost host = (IDesignerHost)this.GetService(typeof(IDesignerHost));
			return ControlPersister.PersistTemplate(template, host);
		}

		// Token: 0x060008C7 RID: 2247 RVA: 0x00032CB0 File Offset: 0x00030EB0
		public override void SetEditableDesignerRegionContent(EditableDesignerRegion region, string content)
		{
			IDesignerHost designerHost = (IDesignerHost)base.Component.Site.GetService(typeof(IDesignerHost));
			ITemplate value = ControlParser.ParseTemplate(designerHost, content);
			PropertyDescriptor propertyDescriptor = TypeDescriptor.GetProperties(base.Component)[region.Name];
			using (DesignerTransaction designerTransaction = designerHost.CreateTransaction("SetEditableDesignerRegionContent"))
			{
				propertyDescriptor.SetValue(base.Component, value);
				designerTransaction.Commit();
			}
		}

		// Token: 0x0400052F RID: 1327
		private PasswordRecovery _passwordRecovery;

		// Token: 0x04000530 RID: 1328
		private static DesignerAutoFormatCollection _autoFormats;

		// Token: 0x04000531 RID: 1329
		private const string _failureTextID = "FailureText";

		// Token: 0x04000532 RID: 1330
		private static readonly string[] _userNameViewRegionToPropertyMap = new string[]
		{
			"UserNameLabelText",
			"UserNameTitleText",
			"UserNameInstructionText"
		};

		// Token: 0x04000533 RID: 1331
		private static readonly string[] _questionViewRegionToPropertyMap = new string[]
		{
			"UserNameLabelText",
			"QuestionTitleText",
			"QuestionLabelText",
			"QuestionInstructionText",
			"AnswerLabelText"
		};

		// Token: 0x04000534 RID: 1332
		private static readonly string[] _successViewRegionToPropertyMap = new string[]
		{
			"SuccessText"
		};

		// Token: 0x04000535 RID: 1333
		private static readonly string[] _templateNames = new string[]
		{
			"UserNameTemplate",
			"QuestionTemplate",
			"SuccessTemplate"
		};

		// Token: 0x04000536 RID: 1334
		private static readonly string[] _nonTemplateProperties = new string[]
		{
			"AnswerLabelText",
			"AnswerRequiredErrorMessage",
			"BorderPadding",
			"HelpPageIconUrl",
			"FailureTextStyle",
			"HelpPageText",
			"HelpPageUrl",
			"HyperLinkStyle",
			"InstructionTextStyle",
			"LabelStyle",
			"QuestionInstructionText",
			"QuestionLabelText",
			"QuestionTitleText",
			"SubmitButtonImageUrl",
			"SubmitButtonStyle",
			"SubmitButtonText",
			"SubmitButtonType",
			"SuccessText",
			"SuccessTextStyle",
			"TextBoxStyle",
			"TextLayout",
			"TitleTextStyle",
			"UserNameInstructionText",
			"UserNameLabelText",
			"UserNameRequiredErrorMessage",
			"UserNameTitleText",
			"ValidatorTextStyle"
		};

		// Token: 0x02000424 RID: 1060
		private enum ViewType
		{
			// Token: 0x04001CC7 RID: 7367
			UserName,
			// Token: 0x04001CC8 RID: 7368
			Question,
			// Token: 0x04001CC9 RID: 7369
			Success
		}

		// Token: 0x02000425 RID: 1061
		private class PasswordRecoveryDesignerActionList : DesignerActionList
		{
			// Token: 0x06002871 RID: 10353 RVA: 0x000F7604 File Offset: 0x000F5804
			public PasswordRecoveryDesignerActionList(PasswordRecoveryDesigner designer) : base(designer.Component)
			{
				this._designer = designer;
			}

			// Token: 0x17000874 RID: 2164
			// (get) Token: 0x06002872 RID: 10354 RVA: 0x00003B0F File Offset: 0x00001D0F
			// (set) Token: 0x06002873 RID: 10355 RVA: 0x00003937 File Offset: 0x00001B37
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

			// Token: 0x17000875 RID: 2165
			// (get) Token: 0x06002874 RID: 10356 RVA: 0x000F761C File Offset: 0x000F581C
			// (set) Token: 0x06002875 RID: 10357 RVA: 0x000F7678 File Offset: 0x000F5878
			[TypeConverter(typeof(PasswordRecoveryDesigner.PasswordRecoveryDesignerActionList.PasswordRecoveryViewTypeConverter))]
			public string View
			{
				get
				{
					if (this._designer.CurrentView == PasswordRecoveryDesigner.ViewType.UserName)
					{
						return SR.GetString("PasswordRecovery_UserNameView");
					}
					if (this._designer.CurrentView == PasswordRecoveryDesigner.ViewType.Question)
					{
						return SR.GetString("PasswordRecovery_QuestionView");
					}
					if (this._designer.CurrentView == PasswordRecoveryDesigner.ViewType.Success)
					{
						return SR.GetString("PasswordRecovery_SuccessView");
					}
					return string.Empty;
				}
				set
				{
					if (string.Compare(value, SR.GetString("PasswordRecovery_UserNameView"), StringComparison.Ordinal) == 0)
					{
						this._designer.CurrentView = PasswordRecoveryDesigner.ViewType.UserName;
					}
					else if (string.Compare(value, SR.GetString("PasswordRecovery_QuestionView"), StringComparison.Ordinal) == 0)
					{
						this._designer.CurrentView = PasswordRecoveryDesigner.ViewType.Question;
					}
					else if (string.Compare(value, SR.GetString("PasswordRecovery_SuccessView"), StringComparison.Ordinal) == 0)
					{
						this._designer.CurrentView = PasswordRecoveryDesigner.ViewType.Success;
					}
					TypeDescriptor.Refresh(this._designer.Component);
					this._designer.UpdateDesignTimeHtml();
				}
			}

			// Token: 0x06002876 RID: 10358 RVA: 0x000F7704 File Offset: 0x000F5904
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

			// Token: 0x06002877 RID: 10359 RVA: 0x000F7748 File Offset: 0x000F5948
			public void LaunchWebAdmin()
			{
				this._designer.LaunchWebAdmin();
			}

			// Token: 0x06002878 RID: 10360 RVA: 0x000F7758 File Offset: 0x000F5958
			public override DesignerActionItemCollection GetSortedActionItems()
			{
				DesignerActionItemCollection designerActionItemCollection = new DesignerActionItemCollection();
				designerActionItemCollection.Add(new DesignerActionPropertyItem("View", SR.GetString("WebControls_Views"), string.Empty, SR.GetString("WebControls_ViewsDescription"))
				{
					ShowInSourceView = false
				});
				if (!this._designer.InTemplateMode)
				{
					if (this._designer.Templated)
					{
						designerActionItemCollection.Add(new DesignerActionMethodItem(this, "Reset", SR.GetString("WebControls_Reset"), string.Empty, SR.GetString("WebControls_ResetDescriptionViews"), true));
					}
					else
					{
						designerActionItemCollection.Add(new DesignerActionMethodItem(this, "ConvertToTemplate", SR.GetString("WebControls_ConvertToTemplate"), string.Empty, SR.GetString("WebControls_ConvertToTemplateDescriptionViews"), true));
					}
				}
				designerActionItemCollection.Add(new DesignerActionMethodItem(this, "LaunchWebAdmin", SR.GetString("Login_LaunchWebAdmin"), string.Empty, SR.GetString("Login_LaunchWebAdminDescription"), true));
				return designerActionItemCollection;
			}

			// Token: 0x06002879 RID: 10361 RVA: 0x000F783D File Offset: 0x000F5A3D
			public void Reset()
			{
				this._designer.Reset();
			}

			// Token: 0x04001CCA RID: 7370
			private PasswordRecoveryDesigner _designer;

			// Token: 0x020005C4 RID: 1476
			private class PasswordRecoveryViewTypeConverter : TypeConverter
			{
				// Token: 0x06003404 RID: 13316 RVA: 0x0011C1BC File Offset: 0x0011A3BC
				public override TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
				{
					return new TypeConverter.StandardValuesCollection(new string[]
					{
						SR.GetString("PasswordRecovery_UserNameView"),
						SR.GetString("PasswordRecovery_QuestionView"),
						SR.GetString("PasswordRecovery_SuccessView")
					});
				}

				// Token: 0x06003405 RID: 13317 RVA: 0x00003B0F File Offset: 0x00001D0F
				public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
				{
					return true;
				}

				// Token: 0x06003406 RID: 13318 RVA: 0x00003B0F File Offset: 0x00001D0F
				public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
				{
					return true;
				}
			}
		}

		// Token: 0x02000426 RID: 1062
		private sealed class ConvertToTemplateHelper : LoginDesignerUtil.GenericConvertToTemplateHelper<PasswordRecovery, PasswordRecoveryDesigner>
		{
			// Token: 0x0600287A RID: 10362 RVA: 0x000F784A File Offset: 0x000F5A4A
			public ConvertToTemplateHelper(PasswordRecoveryDesigner designer, IDesignerHost designerHost) : base(designer, designerHost)
			{
			}

			// Token: 0x17000876 RID: 2166
			// (get) Token: 0x0600287B RID: 10363 RVA: 0x000F7854 File Offset: 0x000F5A54
			protected override string[] PersistedControlIDs
			{
				get
				{
					return PasswordRecoveryDesigner.ConvertToTemplateHelper._persistedControlIDs;
				}
			}

			// Token: 0x17000877 RID: 2167
			// (get) Token: 0x0600287C RID: 10364 RVA: 0x000F785B File Offset: 0x000F5A5B
			protected override string[] PersistedIfNotVisibleControlIDs
			{
				get
				{
					return PasswordRecoveryDesigner.ConvertToTemplateHelper._persistedIfNotVisibleControlIDs;
				}
			}

			// Token: 0x0600287D RID: 10365 RVA: 0x000F7862 File Offset: 0x000F5A62
			protected override Style GetFailureTextStyle(PasswordRecovery control)
			{
				return control.FailureTextStyle;
			}

			// Token: 0x0600287E RID: 10366 RVA: 0x000F786C File Offset: 0x000F5A6C
			protected override Control GetDefaultTemplateContents()
			{
				Control control = null;
				switch (base.Designer.CurrentView)
				{
				case PasswordRecoveryDesigner.ViewType.UserName:
					control = base.Designer.ViewControl.Controls[0];
					break;
				case PasswordRecoveryDesigner.ViewType.Question:
					control = base.Designer.ViewControl.Controls[1];
					break;
				case PasswordRecoveryDesigner.ViewType.Success:
					control = base.Designer.ViewControl.Controls[2];
					break;
				}
				return (Table)control.Controls[0];
			}

			// Token: 0x0600287F RID: 10367 RVA: 0x000F78F7 File Offset: 0x000F5AF7
			protected override ITemplate GetTemplate(PasswordRecovery control)
			{
				return base.Designer.GetTemplate(control);
			}

			// Token: 0x04001CCB RID: 7371
			private static readonly string[] _persistedControlIDs = new string[]
			{
				"UserName",
				"UserNameRequired",
				"Question",
				"Answer",
				"AnswerRequired",
				"SubmitButton",
				"SubmitImageButton",
				"SubmitLinkButton",
				"FailureText",
				"HelpLink"
			};

			// Token: 0x04001CCC RID: 7372
			private static readonly string[] _persistedIfNotVisibleControlIDs = new string[]
			{
				"UserName",
				"Question",
				"FailureText"
			};
		}
	}
}
