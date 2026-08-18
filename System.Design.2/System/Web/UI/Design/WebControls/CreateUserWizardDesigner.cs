using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Design;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Security.Permissions;
using System.Text;
using System.Web.UI.WebControls;
using System.Windows.Forms;

namespace System.Web.UI.Design.WebControls
{
	// Token: 0x020000B0 RID: 176
	[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
	public class CreateUserWizardDesigner : WizardDesigner
	{
		// Token: 0x0600055E RID: 1374 RVA: 0x0001A724 File Offset: 0x00018924
		static CreateUserWizardDesigner()
		{
			CreateUserWizardDesigner._persistedIDConverter = new Hashtable();
			CreateUserWizardDesigner._persistedIDConverter.Add("CancelButtonImageButton", "CancelButton");
			CreateUserWizardDesigner._persistedIDConverter.Add("CancelButtonButton", "CancelButton");
			CreateUserWizardDesigner._persistedIDConverter.Add("CancelButtonLinkButton", "CancelButton");
			CreateUserWizardDesigner._persistedIDConverter.Add("StepNextButtonImageButton", "StepNextButton");
			CreateUserWizardDesigner._persistedIDConverter.Add("StepNextButtonButton", "StepNextButton");
			CreateUserWizardDesigner._persistedIDConverter.Add("StepNextButtonLinkButton", "StepNextButton");
			CreateUserWizardDesigner._persistedIDConverter.Add("StepPreviousButtonImageButton", "StepNextButton");
			CreateUserWizardDesigner._persistedIDConverter.Add("StepPreviousButton", "StepNextButton");
			CreateUserWizardDesigner._persistedIDConverter.Add("StepPreviousButtonLinkButton", "StepNextButton");
			CreateUserWizardDesigner._completeStepConverter = new Hashtable();
			CreateUserWizardDesigner._completeStepConverter.Add("ContinueButtonImageButton", "ContinueButton");
			CreateUserWizardDesigner._completeStepConverter.Add("ContinueButtonButton", "ContinueButton");
			CreateUserWizardDesigner._completeStepConverter.Add("ContinueButtonLinkButton", "ContinueButton");
		}

		// Token: 0x0600055F RID: 1375 RVA: 0x0001AAB0 File Offset: 0x00018CB0
		private static bool IsStepEmpty(WizardStepBase step)
		{
			if (!(step is CreateUserWizardStep) && !(step is CompleteWizardStep))
			{
				return false;
			}
			TemplatedWizardStep templatedWizardStep = (TemplatedWizardStep)step;
			return templatedWizardStep.ContentTemplate == null;
		}

		// Token: 0x06000560 RID: 1376 RVA: 0x0001AADF File Offset: 0x00018CDF
		internal override bool InRegionEditingMode(Wizard viewControl)
		{
			return !base.SupportsDesignerRegions || CreateUserWizardDesigner.IsStepEmpty(this._createUserWizard.ActiveStep);
		}

		// Token: 0x17000151 RID: 337
		// (get) Token: 0x06000561 RID: 1377 RVA: 0x0001AB00 File Offset: 0x00018D00
		public override DesignerActionListCollection ActionLists
		{
			get
			{
				DesignerActionListCollection designerActionListCollection = new DesignerActionListCollection();
				designerActionListCollection.AddRange(base.ActionLists);
				designerActionListCollection.Add(new CreateUserWizardDesigner.CreateUserWizardDesignerActionList(this));
				return designerActionListCollection;
			}
		}

		// Token: 0x17000152 RID: 338
		// (get) Token: 0x06000562 RID: 1378 RVA: 0x0001AB2D File Offset: 0x00018D2D
		public override DesignerAutoFormatCollection AutoFormats
		{
			get
			{
				if (CreateUserWizardDesigner._autoFormats == null)
				{
					CreateUserWizardDesigner._autoFormats = ControlDesigner.CreateAutoFormats(AutoFormatSchemes.CREATEUSERWIZARD_SCHEME_NAMES, (string schemeName) => new CreateUserWizardAutoFormat(schemeName, "<Schemes>\r\n<xsd:schema id=\"Schemes\" xmlns=\"\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns:msdata=\"urn:schemas-microsoft-com:xml-msdata\">\r\n  <xsd:element name=\"Scheme\">\r\n     <xsd:complexType>\r\n       <xsd:all>\r\n        <xsd:element name=\"SchemeName\" type=\"xsd:string\"/>\r\n        <xsd:element name=\"BackColor\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n        <xsd:element name=\"ForeColor\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n        <xsd:element name=\"BorderColor\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n        <xsd:element name=\"BorderWidth\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n        <xsd:element name=\"BorderStyle\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n        <xsd:element name=\"BorderPadding\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n        <xsd:element name=\"FontSize\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n        <xsd:element name=\"FontName\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n        <xsd:element name=\"TextLayout\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n        <xsd:element name=\"TitleTextBackColor\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n        <xsd:element name=\"TitleTextForeColor\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n        <xsd:element name=\"TitleTextFont\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n        <xsd:element name=\"TitleTextFontSize\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n        <xsd:element name=\"InstructionTextForeColor\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n        <xsd:element name=\"InstructionTextFont\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n        <xsd:element name=\"TextboxFontSize\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n        <xsd:element name=\"NavigationButtonStyleBorderWidth\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n        <xsd:element name=\"NavigationButtonStyleFontName\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n        <xsd:element name=\"NavigationButtonStyleFontSize\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n        <xsd:element name=\"NavigationButtonStyleBorderStyle\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n        <xsd:element name=\"NavigationButtonStyleBorderColor\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n        <xsd:element name=\"NavigationButtonStyleForeColor\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n        <xsd:element name=\"NavigationButtonStyleBackColor\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n        <xsd:element name=\"StepStyleBorderWidth\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n        <xsd:element name=\"StepStyleBorderStyle\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n        <xsd:element name=\"StepStyleBorderColor\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n        <xsd:element name=\"StepStyleForeColor\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n        <xsd:element name=\"StepStyleBackColor\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n        <xsd:element name=\"StepStyleFontSize\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n        <xsd:element name=\"SideBarButtonStyleFontUnderline\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n        <xsd:element name=\"SideBarButtonStyleFontName\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n        <xsd:element name=\"SideBarButtonStyleForeColor\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n        <xsd:element name=\"SideBarButtonStyleBorderWidth\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n        <xsd:element name=\"SideBarButtonStyleBackColor\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n        <xsd:element name=\"HeaderStyleForeColor\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n        <xsd:element name=\"HeaderStyleBorderColor\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n        <xsd:element name=\"HeaderStyleBackColor\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n        <xsd:element name=\"HeaderStyleFontSize\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n        <xsd:element name=\"HeaderStyleFontBold\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n        <xsd:element name=\"HeaderStyleBorderWidth\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n        <xsd:element name=\"HeaderStyleHorizontalAlign\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n        <xsd:element name=\"HeaderStyleBorderStyle\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n        <xsd:element name=\"SideBarStyleBackColor\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n        <xsd:element name=\"SideBarStyleVerticalAlign\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n        <xsd:element name=\"SideBarStyleFontSize\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n        <xsd:element name=\"SideBarStyleFontUnderline\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n        <xsd:element name=\"SideBarStyleFontStrikeout\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n        <xsd:element name=\"SideBarStyleBorderWidth\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n      </xsd:all>\r\n    </xsd:complexType>\r\n  </xsd:element>\r\n  <xsd:element name=\"Schemes\" msdata:IsDataSet=\"true\">\r\n    <xsd:complexType>\r\n      <xsd:choice maxOccurs=\"unbounded\">\r\n        <xsd:element ref=\"Scheme\"/>\r\n      </xsd:choice>\r\n    </xsd:complexType>\r\n  </xsd:element>\r\n</xsd:schema>\r\n<Scheme>\r\n  <SchemeName>CreateUserWizardScheme_Empty</SchemeName>\r\n</Scheme>\r\n<Scheme>\r\n  <SchemeName>CreateUserWizardScheme_Elegant</SchemeName>\r\n  <BackColor>#F7F7DE</BackColor>\r\n  <BorderColor>#CCCC99</BorderColor>\r\n  <BorderWidth>1</BorderWidth>\r\n  <BorderStyle>4</BorderStyle>\r\n  <FontSize>10</FontSize>\r\n  <FontName>Verdana</FontName>\r\n  <TitleTextBackColor>#6B696B</TitleTextBackColor>\r\n  <TitleTextForeColor>#FFFFFF</TitleTextForeColor>\r\n  <TitleTextFont>1</TitleTextFont>\r\n  <StepStyleBorderWidth>0px</StepStyleBorderWidth>\r\n  <NavigationButtonStyleBorderWidth>1px</NavigationButtonStyleBorderWidth>\r\n  <NavigationButtonStyleFontName>Verdana</NavigationButtonStyleFontName>\r\n  <NavigationButtonStyleBorderStyle>4</NavigationButtonStyleBorderStyle>\r\n  <NavigationButtonStyleBorderColor>#CCCCCC</NavigationButtonStyleBorderColor>\r\n  <NavigationButtonStyleForeColor>#284775</NavigationButtonStyleForeColor>\r\n  <NavigationButtonStyleBackColor>#FFFBFF</NavigationButtonStyleBackColor>\r\n  <SideBarButtonStyleFontUnderline>False</SideBarButtonStyleFontUnderline>\r\n  <SideBarButtonStyleFontName>Verdana</SideBarButtonStyleFontName>\r\n  <SideBarButtonStyleForeColor>#FFFFFF</SideBarButtonStyleForeColor>\r\n  <SideBarButtonStyleBorderWidth>0px</SideBarButtonStyleBorderWidth>\r\n  <HeaderStyleForeColor>#FFFFFF</HeaderStyleForeColor>\r\n  <HeaderStyleBackColor>#6B696B</HeaderStyleBackColor>\r\n  <HeaderStyleFontBold>True</HeaderStyleFontBold>\r\n  <HeaderStyleHorizontalAlign>2</HeaderStyleHorizontalAlign>\r\n  <SideBarStyleBackColor>#7C6F57</SideBarStyleBackColor>\r\n  <SideBarStyleVerticalAlign>1</SideBarStyleVerticalAlign>\r\n  <SideBarStyleFontSize>0.9em</SideBarStyleFontSize>\r\n  <SideBarStyleBorderWidth>0px</SideBarStyleBorderWidth>\r\n</Scheme>\r\n<Scheme>\r\n  <SchemeName>CreateUserWizardScheme_Professional</SchemeName>\r\n  <BackColor>#F7F6F3</BackColor>\r\n  <ForeColor>#333333</ForeColor>\r\n  <BorderColor>#E6E2D8</BorderColor>\r\n  <BorderWidth>1</BorderWidth>\r\n  <BorderStyle>4</BorderStyle>\r\n  <BorderPadding>4</BorderPadding>\r\n  <FontSize>0.8em</FontSize>\r\n  <FontName>Verdana</FontName>\r\n  <TitleTextBackColor>#5D7B9D</TitleTextBackColor>\r\n  <TitleTextForeColor>White</TitleTextForeColor>\r\n  <TitleTextFont>1</TitleTextFont>\r\n  <TitleTextFontSize>0.9em</TitleTextFontSize>\r\n  <InstructionTextForeColor>Black</InstructionTextForeColor>\r\n  <InstructionTextFont>2</InstructionTextFont>\r\n  <TextboxFontSize>0.8em</TextboxFontSize>\r\n  <StepStyleBorderWidth>0px</StepStyleBorderWidth>\r\n  <NavigationButtonStyleBorderWidth>1px</NavigationButtonStyleBorderWidth>\r\n  <NavigationButtonStyleFontName>Verdana</NavigationButtonStyleFontName>\r\n  <NavigationButtonStyleBorderStyle>4</NavigationButtonStyleBorderStyle>\r\n  <NavigationButtonStyleBorderColor>#CCCCCC</NavigationButtonStyleBorderColor>\r\n  <NavigationButtonStyleForeColor>#284775</NavigationButtonStyleForeColor>\r\n  <NavigationButtonStyleBackColor>#FFFBFF</NavigationButtonStyleBackColor>\r\n  <SideBarButtonStyleFontUnderline>False</SideBarButtonStyleFontUnderline>\r\n  <SideBarButtonStyleFontName>Verdana</SideBarButtonStyleFontName>\r\n  <SideBarButtonStyleForeColor>White</SideBarButtonStyleForeColor>\r\n  <SideBarButtonStyleBorderWidth>0px</SideBarButtonStyleBorderWidth>\r\n  <HeaderStyleForeColor>White</HeaderStyleForeColor>\r\n  <HeaderStyleBackColor>#5D7B9D</HeaderStyleBackColor>\r\n  <HeaderStyleFontSize>0.9em</HeaderStyleFontSize>\r\n  <HeaderStyleFontBold>True</HeaderStyleFontBold>\r\n  <HeaderStyleHorizontalAlign>2</HeaderStyleHorizontalAlign>\r\n  <HeaderStyleBorderStyle>4</HeaderStyleBorderStyle>\r\n  <SideBarStyleBackColor>#5D7B9D</SideBarStyleBackColor>\r\n  <SideBarStyleVerticalAlign>1</SideBarStyleVerticalAlign>\r\n  <SideBarStyleFontSize>0.9em</SideBarStyleFontSize>\r\n  <SideBarStyleBorderWidth>0px</SideBarStyleBorderWidth>\r\n</Scheme>\r\n<Scheme>\r\n  <SchemeName>CreateUserWizardScheme_Simple</SchemeName>\r\n  <BackColor>#E3EAEB</BackColor>\r\n  <ForeColor>#333333</ForeColor>\r\n  <BorderColor>#E6E2D8</BorderColor>\r\n  <BorderWidth>1</BorderWidth>\r\n  <BorderStyle>4</BorderStyle>\r\n  <BorderPadding>4</BorderPadding>\r\n  <FontSize>0.8em</FontSize>\r\n  <FontName>Verdana</FontName>\r\n  <TextLayout>1</TextLayout>\r\n  <TitleTextBackColor>#1C5E55</TitleTextBackColor>\r\n  <TitleTextForeColor>White</TitleTextForeColor>\r\n  <TitleTextFont>1</TitleTextFont>\r\n  <TitleTextFontSize>0.9em</TitleTextFontSize>\r\n  <InstructionTextForeColor>Black</InstructionTextForeColor>\r\n  <InstructionTextFont>2</InstructionTextFont>\r\n  <TextboxFontSize>0.8em</TextboxFontSize>\r\n  <StepStyleBorderWidth>0px</StepStyleBorderWidth>\r\n  <NavigationButtonStyleBorderWidth>1px</NavigationButtonStyleBorderWidth>\r\n  <NavigationButtonStyleFontName>Verdana</NavigationButtonStyleFontName>\r\n  <NavigationButtonStyleBorderStyle>4</NavigationButtonStyleBorderStyle>\r\n  <NavigationButtonStyleBorderColor>#C5BBAF</NavigationButtonStyleBorderColor>\r\n  <NavigationButtonStyleForeColor>#1C5E55</NavigationButtonStyleForeColor>\r\n  <NavigationButtonStyleBackColor>White</NavigationButtonStyleBackColor>\r\n  <SideBarButtonStyleFontUnderline>False</SideBarButtonStyleFontUnderline>\r\n  <SideBarButtonStyleForeColor>White</SideBarButtonStyleForeColor>\r\n  <HeaderStyleForeColor>White</HeaderStyleForeColor>\r\n  <HeaderStyleBackColor>#666666</HeaderStyleBackColor>\r\n  <HeaderStyleBorderColor>#E6E2D8</HeaderStyleBorderColor>\r\n  <HeaderStyleFontSize>0.9em</HeaderStyleFontSize>\r\n  <HeaderStyleFontBold>True</HeaderStyleFontBold>\r\n  <HeaderStyleHorizontalAlign>2</HeaderStyleHorizontalAlign>\r\n  <HeaderStyleBorderStyle>4</HeaderStyleBorderStyle>\r\n  <HeaderStyleBorderWidth>2px</HeaderStyleBorderWidth>\r\n  <SideBarStyleBackColor>#1C5E55</SideBarStyleBackColor>\r\n  <SideBarStyleVerticalAlign>1</SideBarStyleVerticalAlign>\r\n  <SideBarStyleFontSize>0.9em</SideBarStyleFontSize>\r\n</Scheme>\r\n<Scheme>\r\n  <SchemeName>CreateUserWizardScheme_Classic</SchemeName>\r\n  <BackColor>#EFF3FB</BackColor>\r\n  <ForeColor>#333333</ForeColor>\r\n  <BorderColor>#B5C7DE</BorderColor>\r\n  <BorderWidth>1</BorderWidth>\r\n  <BorderStyle>4</BorderStyle>\r\n  <BorderPadding>4</BorderPadding>\r\n  <FontSize>0.8em</FontSize>\r\n  <FontName>Verdana</FontName>\r\n  <TitleTextBackColor>#507CD1</TitleTextBackColor>\r\n  <TitleTextForeColor>White</TitleTextForeColor>\r\n  <TitleTextFont>1</TitleTextFont>\r\n  <TitleTextFontSize>0.9em</TitleTextFontSize>\r\n  <InstructionTextForeColor>Black</InstructionTextForeColor>\r\n  <InstructionTextFont>2</InstructionTextFont>\r\n  <TextboxFontSize>0.8em</TextboxFontSize>\r\n  <StepStyleFontSize>0.8em</StepStyleFontSize>\r\n  <NavigationButtonStyleBorderWidth>1px</NavigationButtonStyleBorderWidth>\r\n  <NavigationButtonStyleFontName>Verdana</NavigationButtonStyleFontName>\r\n  <NavigationButtonStyleBorderStyle>4</NavigationButtonStyleBorderStyle>\r\n  <NavigationButtonStyleBorderColor>#507CD1</NavigationButtonStyleBorderColor>\r\n  <NavigationButtonStyleForeColor>#284E98</NavigationButtonStyleForeColor>\r\n  <NavigationButtonStyleBackColor>White</NavigationButtonStyleBackColor>\r\n  <SideBarButtonStyleFontUnderline>False</SideBarButtonStyleFontUnderline>\r\n  <SideBarButtonStyleFontName>Verdana</SideBarButtonStyleFontName>\r\n  <SideBarButtonStyleForeColor>White</SideBarButtonStyleForeColor>\r\n  <SideBarButtonStyleBackColor>#507CD1</SideBarButtonStyleBackColor>\r\n  <HeaderStyleForeColor>White</HeaderStyleForeColor>\r\n  <HeaderStyleBorderColor>#EFF3FB</HeaderStyleBorderColor>\r\n  <HeaderStyleBackColor>#284E98</HeaderStyleBackColor>\r\n  <HeaderStyleFontSize>0.9em</HeaderStyleFontSize>\r\n  <HeaderStyleFontBold>True</HeaderStyleFontBold>\r\n  <HeaderStyleBorderWidth>2px</HeaderStyleBorderWidth>\r\n  <HeaderStyleHorizontalAlign>2</HeaderStyleHorizontalAlign>\r\n  <HeaderStyleBorderStyle>4</HeaderStyleBorderStyle>\r\n  <SideBarStyleBackColor>#507CD1</SideBarStyleBackColor>\r\n  <SideBarStyleVerticalAlign>1</SideBarStyleVerticalAlign>\r\n  <SideBarStyleFontSize>0.9em</SideBarStyleFontSize>\r\n</Scheme>\r\n<Scheme>\r\n  <SchemeName>CreateUserWizardScheme_Colorful</SchemeName>\r\n  <BackColor>#FFFBD6</BackColor>\r\n  <ForeColor>#333333</ForeColor>\r\n  <BorderColor>#FFDFAD</BorderColor>\r\n  <BorderWidth>1</BorderWidth>\r\n  <BorderStyle>4</BorderStyle>\r\n  <BorderPadding>4</BorderPadding>\r\n  <FontSize>0.8em</FontSize>\r\n  <FontName>Verdana</FontName>\r\n  <TextLayout>1</TextLayout>\r\n  <TitleTextBackColor>#990000</TitleTextBackColor>\r\n  <TitleTextForeColor>White</TitleTextForeColor>\r\n  <TitleTextFont>1</TitleTextFont>\r\n  <TitleTextFontSize>0.9em</TitleTextFontSize>\r\n  <InstructionTextForeColor>Black</InstructionTextForeColor>\r\n  <InstructionTextFont>2</InstructionTextFont>\r\n  <TextboxFontSize>0.8em</TextboxFontSize>\r\n  <NavigationButtonStyleBorderWidth>1px</NavigationButtonStyleBorderWidth>\r\n  <NavigationButtonStyleFontName>Verdana</NavigationButtonStyleFontName>\r\n  <NavigationButtonStyleBorderStyle>4</NavigationButtonStyleBorderStyle>\r\n  <NavigationButtonStyleBorderColor>#CC9966</NavigationButtonStyleBorderColor>\r\n  <NavigationButtonStyleForeColor>#990000</NavigationButtonStyleForeColor>\r\n  <NavigationButtonStyleBackColor>White</NavigationButtonStyleBackColor>\r\n  <SideBarButtonStyleFontUnderline>False</SideBarButtonStyleFontUnderline>\r\n  <SideBarButtonStyleForeColor>White</SideBarButtonStyleForeColor>\r\n  <HeaderStyleForeColor>#333333</HeaderStyleForeColor>\r\n  <HeaderStyleBorderColor>#FFFBD6</HeaderStyleBorderColor>\r\n  <HeaderStyleBackColor>#FFCC66</HeaderStyleBackColor>\r\n  <HeaderStyleFontSize>0.9em</HeaderStyleFontSize>\r\n  <HeaderStyleFontBold>True</HeaderStyleFontBold>\r\n  <HeaderStyleBorderWidth>2px</HeaderStyleBorderWidth>\r\n  <HeaderStyleHorizontalAlign>2</HeaderStyleHorizontalAlign>\r\n  <HeaderStyleBorderStyle>4</HeaderStyleBorderStyle>\r\n  <SideBarStyleBackColor>#990000</SideBarStyleBackColor>\r\n  <SideBarStyleVerticalAlign>1</SideBarStyleVerticalAlign>\r\n  <SideBarStyleFontSize>0.9em</SideBarStyleFontSize>\r\n  <SideBarStyleFontUnderline>False</SideBarStyleFontUnderline>\r\n</Scheme>\r\n</Schemes>\r\n"));
				}
				return CreateUserWizardDesigner._autoFormats;
			}
		}

		// Token: 0x17000153 RID: 339
		// (get) Token: 0x06000563 RID: 1379 RVA: 0x00003B0F File Offset: 0x00001D0F
		protected override bool UsePreviewControl
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06000564 RID: 1380 RVA: 0x0001AB6C File Offset: 0x00018D6C
		protected override void AddDesignerRegions(DesignerRegionCollection regions)
		{
			if (!base.SupportsDesignerRegions)
			{
				return;
			}
			if (this._createUserWizard.CreateUserStep == null)
			{
				this.CreateChildControls();
				if (this._createUserWizard.CreateUserStep == null)
				{
					return;
				}
			}
			bool flag = this._createUserWizard.CreateUserStep.ContentTemplate == null;
			bool flag2 = this._createUserWizard.CompleteStep.ContentTemplate == null;
			foreach (object obj in this._createUserWizard.WizardSteps)
			{
				WizardStepBase wizardStepBase = (WizardStepBase)obj;
				DesignerRegion designerRegion;
				if ((!flag || !(wizardStepBase is CreateUserWizardStep)) && (!flag2 || !(wizardStepBase is CompleteWizardStep)))
				{
					if (wizardStepBase is TemplatedWizardStep)
					{
						TemplateDefinition templateDefinition = new TemplateDefinition(this, "ContentTemplate", this._createUserWizard, "ContentTemplate", base.TemplateStyleArray[5]);
						designerRegion = new WizardStepTemplatedEditableRegion(templateDefinition, wizardStepBase);
						designerRegion.EnsureSize = false;
					}
					else
					{
						designerRegion = new WizardStepEditableRegion(this, wizardStepBase);
					}
					designerRegion.Description = SR.GetString("ContainerControlDesigner_RegionWatermark");
				}
				else
				{
					designerRegion = new WizardSelectableRegion(this, base.GetRegionName(wizardStepBase), wizardStepBase);
				}
				regions.Add(designerRegion);
			}
			foreach (object obj2 in this._createUserWizard.WizardSteps)
			{
				WizardStepBase wizardStepBase2 = (WizardStepBase)obj2;
				WizardSelectableRegion wizardSelectableRegion = new WizardSelectableRegion(this, "Move to " + base.GetRegionName(wizardStepBase2), wizardStepBase2);
				if (this._createUserWizard.ActiveStep == wizardStepBase2)
				{
					wizardSelectableRegion.Selected = true;
				}
				regions.Add(wizardSelectableRegion);
			}
		}

		// Token: 0x06000565 RID: 1381 RVA: 0x0001AD40 File Offset: 0x00018F40
		protected override void ConvertToCustomNavigationTemplate()
		{
			try
			{
				if (this._createUserWizard.ActiveStep == this._createUserWizard.CreateUserStep)
				{
					IDesignerHost designerHost = (IDesignerHost)this.GetService(typeof(IDesignerHost));
					ITemplate template = ((CreateUserWizard)base.ViewControl).CreateUserStep.CustomNavigationTemplate;
					if (template == null)
					{
						IControlDesignerAccessor createUserWizard = this._createUserWizard;
						IDictionary designModeState = createUserWizard.GetDesignModeState();
						ControlCollection controlCollection = designModeState["CustomNavigationControls"] as ControlCollection;
						if (controlCollection != null)
						{
							string text = string.Empty;
							foreach (object obj in controlCollection)
							{
								Control control = (Control)obj;
								if (control != null && control.Visible)
								{
									foreach (object obj2 in CreateUserWizardDesigner._persistedIDConverter.Keys)
									{
										string text2 = (string)obj2;
										Control control2 = control.FindControl(text2);
										if (control2 != null && control2.Visible)
										{
											control2.ID = (string)CreateUserWizardDesigner._persistedIDConverter[text2];
										}
									}
									if (control is Table)
									{
										text += this.ConvertNavigationTableToHtmlTable((Table)control);
									}
									else
									{
										StringWriter stringWriter = new StringWriter(CultureInfo.CurrentCulture);
										HtmlTextWriter writer = new HtmlTextWriter(stringWriter);
										control.RenderControl(writer);
										text += stringWriter.ToString();
									}
								}
							}
							template = ControlParser.ParseTemplate(designerHost, text);
						}
					}
					ControlDesigner.InvokeTransactedChange(base.Component, new TransactedChangeCallback(base.ConvertToCustomNavigationTemplateCallBack), template, SR.GetString("Wizard_ConvertToCustomNavigationTemplate"));
					this.UpdateDesignTimeHtml();
				}
				else
				{
					base.ConvertToCustomNavigationTemplate();
				}
			}
			catch (Exception ex)
			{
			}
		}

		// Token: 0x06000566 RID: 1382 RVA: 0x0001AF68 File Offset: 0x00019168
		private string ConvertTableToHtmlTable(Table originalTable, Control container)
		{
			return this.ConvertTableToHtmlTable(originalTable, container, null);
		}

		// Token: 0x06000567 RID: 1383 RVA: 0x0001AF74 File Offset: 0x00019174
		private string ConvertTableToHtmlTable(Table originalTable, Control container, IDictionary persistMap)
		{
			IList list = new ArrayList();
			foreach (object obj in originalTable.Controls)
			{
				Control value = (Control)obj;
				list.Add(value);
			}
			Table table = new Table();
			foreach (object obj2 in list)
			{
				Control child = (Control)obj2;
				table.Controls.Add(child);
			}
			if (originalTable.ControlStyleCreated)
			{
				table.ApplyStyle(originalTable.ControlStyle);
			}
			table.Width = ((WebControl)base.ViewControl).Width;
			table.Height = ((WebControl)base.ViewControl).Height;
			if (container != null)
			{
				container.Controls.Add(table);
				container.Controls.Remove(originalTable);
			}
			IDesignerHost host = (IDesignerHost)this.GetService(typeof(IDesignerHost));
			if (persistMap != null)
			{
				foreach (object obj3 in persistMap.Keys)
				{
					string text = (string)obj3;
					Control control = table.FindControl(text);
					if (control != null && control.Visible)
					{
						control.ID = (string)persistMap[text];
						string text2 = ControlPersister.PersistControl(control, host);
						LiteralControl child2 = new LiteralControl(text2);
						control.Parent.Controls.Add(child2);
						control.Parent.Controls.Remove(control);
					}
				}
			}
			foreach (string text3 in CreateUserWizardDesigner._persistedControlIDs)
			{
				Control control2 = table.FindControl(text3);
				if (control2 != null)
				{
					if (Array.IndexOf<string>(CreateUserWizardDesigner._persistedIfNotVisibleControlIDs, text3) >= 0)
					{
						control2.Visible = true;
						control2.Parent.Visible = true;
						control2.Parent.Parent.Visible = true;
					}
					if (text3 == "ErrorMessage")
					{
						TableCell tableCell = (TableCell)control2.Parent;
						tableCell.ForeColor = Color.Red;
						tableCell.ApplyStyle(this._createUserWizard.ErrorMessageStyle);
						control2.EnableViewState = false;
					}
					if (control2.Visible)
					{
						string text4 = ControlPersister.PersistControl(control2, host);
						LiteralControl child3 = new LiteralControl(text4);
						control2.Parent.Controls.Add(child3);
						control2.Parent.Controls.Remove(control2);
					}
				}
			}
			StringWriter stringWriter = new StringWriter(CultureInfo.CurrentCulture);
			HtmlTextWriter writer = new HtmlTextWriter(stringWriter);
			table.RenderControl(writer);
			return stringWriter.ToString();
		}

		// Token: 0x06000568 RID: 1384 RVA: 0x0001B26C File Offset: 0x0001946C
		private string ConvertNavigationTableToHtmlTable(Table table)
		{
			IControlDesignerAccessor createUserWizard = this._createUserWizard;
			IDictionary designModeState = createUserWizard.GetDesignModeState();
			StringWriter stringWriter = new StringWriter(CultureInfo.CurrentCulture);
			HtmlTextWriter htmlTextWriter = new HtmlTextWriter(stringWriter);
			if (table.Width != Unit.Empty)
			{
				htmlTextWriter.AddStyleAttribute(HtmlTextWriterStyle.Width, table.Width.ToString(CultureInfo.CurrentCulture));
			}
			if (table.Height != Unit.Empty)
			{
				htmlTextWriter.AddStyleAttribute(HtmlTextWriterStyle.Height, table.Height.ToString(CultureInfo.CurrentCulture));
			}
			if (table.CellSpacing != 0)
			{
				htmlTextWriter.AddAttribute(HtmlTextWriterAttribute.Cellspacing, table.CellSpacing.ToString(CultureInfo.CurrentCulture));
			}
			string value = "0";
			if (table.BorderWidth != Unit.Empty)
			{
				value = table.BorderWidth.ToString(CultureInfo.CurrentCulture);
			}
			htmlTextWriter.AddAttribute(HtmlTextWriterAttribute.Border, value);
			htmlTextWriter.RenderBeginTag(HtmlTextWriterTag.Table);
			ArrayList arrayList = new ArrayList(table.Rows.Count);
			foreach (object obj in table.Rows)
			{
				TableRow tableRow = (TableRow)obj;
				if (tableRow.Visible)
				{
					ArrayList arrayList2 = new ArrayList(tableRow.Cells.Count);
					foreach (object obj2 in tableRow.Cells)
					{
						TableCell tableCell = (TableCell)obj2;
						if (tableCell.Visible && tableCell.HasControls())
						{
							ArrayList arrayList3 = new ArrayList(tableCell.Controls.Count);
							foreach (object obj3 in tableCell.Controls)
							{
								Control control = (Control)obj3;
								if (control.Visible && (!(control is Literal) || !(control.ID != "ErrorMessage") || ((Literal)control).Text.Length != 0) && (!(control is HyperLink) || ((HyperLink)control).Text.Length != 0) && (!(control is System.Web.UI.WebControls.Image) || ((System.Web.UI.WebControls.Image)control).ImageUrl.Length != 0))
								{
									arrayList3.Add(control);
								}
							}
							if (arrayList3.Count > 0)
							{
								arrayList2.Add(new CreateUserWizardDesigner.CellControls(tableCell, arrayList3));
							}
						}
					}
					if (arrayList2.Count > 0)
					{
						arrayList.Add(new CreateUserWizardDesigner.RowCells(tableRow, arrayList2));
					}
				}
			}
			foreach (object obj4 in arrayList)
			{
				CreateUserWizardDesigner.RowCells rowCells = (CreateUserWizardDesigner.RowCells)obj4;
				HorizontalAlign horizontalAlign = rowCells._row.HorizontalAlign;
				if (horizontalAlign != HorizontalAlign.Center)
				{
					if (horizontalAlign == HorizontalAlign.Right)
					{
						htmlTextWriter.AddAttribute(HtmlTextWriterAttribute.Align, "right");
					}
				}
				else
				{
					htmlTextWriter.AddAttribute(HtmlTextWriterAttribute.Align, "center");
				}
				htmlTextWriter.RenderBeginTag(HtmlTextWriterTag.Tr);
				foreach (object obj5 in rowCells._cells)
				{
					CreateUserWizardDesigner.CellControls cellControls = (CreateUserWizardDesigner.CellControls)obj5;
					HorizontalAlign horizontalAlign2 = cellControls._cell.HorizontalAlign;
					if (horizontalAlign2 != HorizontalAlign.Center)
					{
						if (horizontalAlign2 == HorizontalAlign.Right)
						{
							htmlTextWriter.AddAttribute(HtmlTextWriterAttribute.Align, "right");
						}
					}
					else
					{
						htmlTextWriter.AddAttribute(HtmlTextWriterAttribute.Align, "center");
					}
					htmlTextWriter.AddAttribute(HtmlTextWriterAttribute.Colspan, cellControls._cell.ColumnSpan.ToString(CultureInfo.CurrentCulture));
					StringBuilder stringBuilder = new StringBuilder();
					foreach (object obj6 in cellControls._controls)
					{
						Control control2 = (Control)obj6;
						bool flag = control2.ID == "ErrorMessage";
						if (control2 is Literal && !flag)
						{
							stringBuilder.Append(((Literal)control2).Text);
						}
						else
						{
							if (flag)
							{
								htmlTextWriter.AddStyleAttribute(HtmlTextWriterStyle.Color, "Red");
								control2.EnableViewState = false;
							}
							stringBuilder.Append(ControlPersister.PersistControl(control2));
						}
					}
					htmlTextWriter.RenderBeginTag(HtmlTextWriterTag.Td);
					htmlTextWriter.Write(stringBuilder.ToString());
					htmlTextWriter.RenderEndTag();
				}
				htmlTextWriter.RenderEndTag();
			}
			htmlTextWriter.RenderEndTag();
			return stringWriter.ToString();
		}

		// Token: 0x06000569 RID: 1385 RVA: 0x0001B7A8 File Offset: 0x000199A8
		internal override string GetEditableDesignerRegionContent(IWizardStepEditableRegion region)
		{
			if (region == null)
			{
				throw new ArgumentNullException("region");
			}
			StringBuilder stringBuilder = new StringBuilder();
			if (region.Step == this._createUserWizard.CreateUserStep && ((CreateUserWizardStep)region.Step).ContentTemplate == null && region.Step.Controls[0] is Table)
			{
				Table originalTable = (Table)((Table)region.Step.Controls[0]).Rows[0].Cells[0].Controls[0];
				stringBuilder.Append(this.ConvertTableToHtmlTable(originalTable, ((TemplatedWizardStep)region.Step).ContentTemplateContainer));
				return stringBuilder.ToString();
			}
			if (region.Step == this._createUserWizard.CompleteStep && ((CompleteWizardStep)region.Step).ContentTemplate == null && region.Step.Controls[0] is Table)
			{
				Table originalTable2 = (Table)((Table)region.Step.Controls[0]).Rows[0].Cells[0].Controls[0];
				stringBuilder.Append(this.ConvertTableToHtmlTable(originalTable2, ((TemplatedWizardStep)region.Step).ContentTemplateContainer));
				return stringBuilder.ToString();
			}
			return base.GetEditableDesignerRegionContent(region);
		}

		// Token: 0x0600056A RID: 1386 RVA: 0x00018A79 File Offset: 0x00016C79
		protected override string GetErrorDesignTimeHtml(Exception e)
		{
			return base.CreatePlaceHolderDesignTimeHtml(SR.GetString("Control_ErrorRenderingShort") + "<br />" + e.Message);
		}

		// Token: 0x0600056B RID: 1387 RVA: 0x0001B914 File Offset: 0x00019B14
		public override void Initialize(IComponent component)
		{
			ControlDesigner.VerifyInitializeArgument(component, typeof(CreateUserWizard));
			this._createUserWizard = (CreateUserWizard)component;
			base.Initialize(component);
		}

		// Token: 0x0600056C RID: 1388 RVA: 0x0001B93C File Offset: 0x00019B3C
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

		// Token: 0x0600056D RID: 1389 RVA: 0x0001B984 File Offset: 0x00019B84
		private void CustomizeCompleteStep()
		{
			IComponent completeStep = this._createUserWizard.CompleteStep;
			PropertyDescriptor member = TypeDescriptor.GetProperties(base.Component)["ActiveStepIndex"];
			int num = this._createUserWizard.WizardSteps.IndexOf(this._createUserWizard.CompleteStep);
			ControlDesigner.InvokeTransactedChange(base.Component, new TransactedChangeCallback(this.NavigateToStep), num, SR.GetString("CreateUserWizard_NavigateToStep", new object[]
			{
				num
			}), member);
			PropertyDescriptor member2 = TypeDescriptor.GetProperties(completeStep)["ContentTemplate"];
			ControlDesigner.InvokeTransactedChange(base.Component.Site, completeStep, new TransactedChangeCallback(this.CustomizeCompleteStepCallback), null, SR.GetString("CreateUserWizard_CustomizeCompleteStep"), member2);
		}

		// Token: 0x0600056E RID: 1390 RVA: 0x0001BA40 File Offset: 0x00019C40
		private bool CustomizeCompleteStepCallback(object context)
		{
			IDesignerHost designerHost = (IDesignerHost)this.GetService(typeof(IDesignerHost));
			CreateUserWizard createUserWizard = (CreateUserWizard)base.ViewControl;
			ITemplate template = createUserWizard.CompleteStep.ContentTemplate;
			if (template == null)
			{
				try
				{
					this.SetConvertToTemplateDesignModeState(true);
					this.ViewControlCreated = false;
					this.GetDesignTimeHtml();
					createUserWizard = (CreateUserWizard)base.ViewControl;
					IControlDesignerAccessor controlDesignerAccessor = createUserWizard;
					IDictionary designModeState = controlDesignerAccessor.GetDesignModeState();
					StringBuilder stringBuilder = new StringBuilder();
					TemplatedWizardStep completeStep = createUserWizard.CompleteStep;
					Table styleTableForCustomizedStep = this.GetStyleTableForCustomizedStep(createUserWizard, completeStep);
					this.ApplyStylesToCustomizedStep(createUserWizard, styleTableForCustomizedStep);
					stringBuilder.Append(this.ConvertTableToHtmlTable(styleTableForCustomizedStep, completeStep.ContentTemplateContainer, CreateUserWizardDesigner._completeStepConverter));
					template = ControlParser.ParseTemplate(designerHost, stringBuilder.ToString());
					this.SetConvertToTemplateDesignModeState(false);
				}
				catch (Exception ex)
				{
					return false;
				}
			}
			IComponent completeStep2 = this._createUserWizard.CompleteStep;
			PropertyDescriptor propertyDescriptor = TypeDescriptor.GetProperties(completeStep2)["ContentTemplate"];
			propertyDescriptor.SetValue(completeStep2, template);
			this.UpdateDesignTimeHtml();
			return true;
		}

		// Token: 0x0600056F RID: 1391 RVA: 0x0001BB4C File Offset: 0x00019D4C
		private void CustomizeCreateUserStep()
		{
			IComponent createUserStep = this._createUserWizard.CreateUserStep;
			PropertyDescriptor member = TypeDescriptor.GetProperties(base.Component)["ActiveStepIndex"];
			int num = this._createUserWizard.WizardSteps.IndexOf(this._createUserWizard.CreateUserStep);
			ControlDesigner.InvokeTransactedChange(base.Component, new TransactedChangeCallback(this.NavigateToStep), num, SR.GetString("CreateUserWizard_NavigateToStep", new object[]
			{
				num
			}), member);
			PropertyDescriptor member2 = TypeDescriptor.GetProperties(createUserStep)["ContentTemplate"];
			ControlDesigner.InvokeTransactedChange(base.Component.Site, createUserStep, new TransactedChangeCallback(this.CustomizeCreateUserStepCallback), null, SR.GetString("CreateUserWizard_CustomizeCreateUserStep"), member2);
		}

		// Token: 0x06000570 RID: 1392 RVA: 0x0001BC08 File Offset: 0x00019E08
		private bool NavigateToStep(object context)
		{
			bool result;
			try
			{
				int activeStepIndex = (int)context;
				this._createUserWizard.ActiveStepIndex = activeStepIndex;
				result = true;
			}
			catch (Exception ex)
			{
				result = false;
			}
			return result;
		}

		// Token: 0x06000571 RID: 1393 RVA: 0x0001BC44 File Offset: 0x00019E44
		private Table GetStyleTableForCustomizedStep(CreateUserWizard createUserWizard, TemplatedWizardStep step)
		{
			if (createUserWizard.LayoutTemplate == null)
			{
				return (Table)((Table)step.Controls[0].Controls[0]).Rows[0].Cells[0].Controls[0];
			}
			return (Table)step.Controls[0].Controls[0];
		}

		// Token: 0x06000572 RID: 1394 RVA: 0x0001BCB8 File Offset: 0x00019EB8
		private void ApplyStylesToCustomizedStep(CreateUserWizard createUserWizard, Table table)
		{
			if (createUserWizard.ControlStyleCreated)
			{
				Style controlStyle = createUserWizard.ControlStyle;
				table.ForeColor = controlStyle.ForeColor;
				table.BackColor = controlStyle.BackColor;
				table.Font.CopyFrom(controlStyle.Font);
				table.Font.Size = new FontUnit(Unit.Percentage(100.0));
			}
			Style stepStyle = createUserWizard.StepStyle;
			if (!stepStyle.IsEmpty)
			{
				table.ForeColor = stepStyle.ForeColor;
				table.BackColor = stepStyle.BackColor;
				table.Font.CopyFrom(stepStyle.Font);
				table.Font.Size = new FontUnit(Unit.Percentage(100.0));
			}
		}

		// Token: 0x06000573 RID: 1395 RVA: 0x0001BD74 File Offset: 0x00019F74
		private void SetConvertToTemplateDesignModeState(bool value)
		{
			Hashtable hashtable = new Hashtable(1);
			hashtable.Add("ConvertToTemplate", value);
			((IControlDesignerAccessor)base.ViewControl).SetDesignModeState(hashtable);
		}

		// Token: 0x06000574 RID: 1396 RVA: 0x0001BDA8 File Offset: 0x00019FA8
		private bool CustomizeCreateUserStepCallback(object context)
		{
			bool result;
			try
			{
				IDesignerHost designerHost = (IDesignerHost)this.GetService(typeof(IDesignerHost));
				CreateUserWizard createUserWizard = (CreateUserWizard)base.ViewControl;
				ITemplate template = createUserWizard.CreateUserStep.ContentTemplate;
				if (template == null)
				{
					this.ViewControlCreated = false;
					this.SetConvertToTemplateDesignModeState(true);
					this.GetDesignTimeHtml();
					createUserWizard = (CreateUserWizard)base.ViewControl;
					IControlDesignerAccessor controlDesignerAccessor = createUserWizard;
					IDictionary designModeState = controlDesignerAccessor.GetDesignModeState();
					StringBuilder stringBuilder = new StringBuilder();
					TemplatedWizardStep createUserStep = createUserWizard.CreateUserStep;
					Table styleTableForCustomizedStep = this.GetStyleTableForCustomizedStep(createUserWizard, createUserStep);
					this.ApplyStylesToCustomizedStep(createUserWizard, styleTableForCustomizedStep);
					stringBuilder.Append(this.ConvertTableToHtmlTable(styleTableForCustomizedStep, createUserStep.ContentTemplateContainer));
					template = ControlParser.ParseTemplate(designerHost, stringBuilder.ToString());
					this.SetConvertToTemplateDesignModeState(false);
				}
				IComponent createUserStep2 = this._createUserWizard.CreateUserStep;
				PropertyDescriptor propertyDescriptor = TypeDescriptor.GetProperties(createUserStep2)["ContentTemplate"];
				propertyDescriptor.SetValue(createUserStep2, template);
				this.UpdateDesignTimeHtml();
				result = true;
			}
			catch (Exception ex)
			{
				result = false;
			}
			return result;
		}

		// Token: 0x06000575 RID: 1397 RVA: 0x0001BEB0 File Offset: 0x0001A0B0
		protected override void PreFilterProperties(IDictionary properties)
		{
			base.PreFilterProperties(properties);
			TemplatedWizardStep createUserStep = this._createUserWizard.CreateUserStep;
			bool flag = createUserStep != null && createUserStep.ContentTemplate != null;
			if (flag)
			{
				foreach (string key in CreateUserWizardDesigner._defaultCreateStepProperties)
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
			TemplatedWizardStep completeStep = this._createUserWizard.CompleteStep;
			bool flag2 = completeStep != null && completeStep.ContentTemplate != null;
			if (flag2)
			{
				foreach (string key2 in CreateUserWizardDesigner._defaultCompleteStepProperties)
				{
					PropertyDescriptor propertyDescriptor2 = (PropertyDescriptor)properties[key2];
					if (propertyDescriptor2 != null)
					{
						properties[key2] = TypeDescriptor.CreateProperty(propertyDescriptor2.ComponentType, propertyDescriptor2, new Attribute[]
						{
							BrowsableAttribute.No
						});
					}
				}
			}
			if (createUserStep != null && createUserStep.CustomNavigationTemplate != null)
			{
				foreach (string key3 in CreateUserWizardDesigner._defaultCreateUserNavProperties)
				{
					PropertyDescriptor propertyDescriptor3 = (PropertyDescriptor)properties[key3];
					if (propertyDescriptor3 != null)
					{
						properties[key3] = TypeDescriptor.CreateProperty(propertyDescriptor3.ComponentType, propertyDescriptor3, new Attribute[]
						{
							BrowsableAttribute.No
						});
					}
				}
			}
			if (flag2 && flag)
			{
				PropertyDescriptor propertyDescriptor4 = (PropertyDescriptor)properties["TitleTextStyle"];
				if (propertyDescriptor4 != null)
				{
					properties["TitleTextStyle"] = TypeDescriptor.CreateProperty(propertyDescriptor4.ComponentType, propertyDescriptor4, new Attribute[]
					{
						BrowsableAttribute.No
					});
				}
			}
		}

		// Token: 0x06000576 RID: 1398 RVA: 0x0001C058 File Offset: 0x0001A258
		private bool ResetCallback(object context)
		{
			bool result;
			try
			{
				IComponent component = (IComponent)context;
				PropertyDescriptor propertyDescriptor = TypeDescriptor.GetProperties(component)["ContentTemplate"];
				propertyDescriptor.SetValue(component, null);
				result = true;
			}
			catch (Exception ex)
			{
				result = false;
			}
			return result;
		}

		// Token: 0x06000577 RID: 1399 RVA: 0x0001C0A0 File Offset: 0x0001A2A0
		private void ResetCompleteStep()
		{
			this.UpdateDesignTimeHtml();
			PropertyDescriptor member = TypeDescriptor.GetProperties(base.Component)["WizardSteps"];
			ControlDesigner.InvokeTransactedChange(base.Component, new TransactedChangeCallback(this.ResetCallback), this._createUserWizard.CompleteStep, SR.GetString("CreateUserWizard_ResetCompleteStepVerb"), member);
		}

		// Token: 0x06000578 RID: 1400 RVA: 0x0001C0F8 File Offset: 0x0001A2F8
		private void ResetCreateUserStep()
		{
			this.UpdateDesignTimeHtml();
			PropertyDescriptor member = TypeDescriptor.GetProperties(base.Component)["WizardSteps"];
			ControlDesigner.InvokeTransactedChange(base.Component, new TransactedChangeCallback(this.ResetCallback), this._createUserWizard.CreateUserStep, SR.GetString("CreateUserWizard_ResetCreateUserStepVerb"), member);
		}

		// Token: 0x040002C2 RID: 706
		private CreateUserWizard _createUserWizard;

		// Token: 0x040002C3 RID: 707
		private const string _userNameID = "UserName";

		// Token: 0x040002C4 RID: 708
		private const string _passwordID = "Password";

		// Token: 0x040002C5 RID: 709
		private const string _confirmPasswordID = "ConfirmPassword";

		// Token: 0x040002C6 RID: 710
		private const string _unknownErrorMessageID = "ErrorMessage";

		// Token: 0x040002C7 RID: 711
		private const string _emailID = "Email";

		// Token: 0x040002C8 RID: 712
		private const string _questionID = "Question";

		// Token: 0x040002C9 RID: 713
		private const string _answerID = "Answer";

		// Token: 0x040002CA RID: 714
		private const string _userNameRequiredID = "UserNameRequired";

		// Token: 0x040002CB RID: 715
		private const string _passwordRequiredID = "PasswordRequired";

		// Token: 0x040002CC RID: 716
		private const string _confirmPasswordRequiredID = "ConfirmPasswordRequired";

		// Token: 0x040002CD RID: 717
		private const string _passwordRegExpID = "PasswordRegExp";

		// Token: 0x040002CE RID: 718
		private const string _emailRequiredID = "EmailRequired";

		// Token: 0x040002CF RID: 719
		private const string _emailRegExpID = "EmailRegExp";

		// Token: 0x040002D0 RID: 720
		private const string _questionRequiredID = "QuestionRequired";

		// Token: 0x040002D1 RID: 721
		private const string _answerRequiredID = "AnswerRequired";

		// Token: 0x040002D2 RID: 722
		private const string _passwordCompareID = "PasswordCompare";

		// Token: 0x040002D3 RID: 723
		private const string _cancelButtonID = "CancelButton";

		// Token: 0x040002D4 RID: 724
		private const string _cancelButtonButtonID = "CancelButtonButton";

		// Token: 0x040002D5 RID: 725
		private const string _cancelButtonImageButtonID = "CancelButtonImageButton";

		// Token: 0x040002D6 RID: 726
		private const string _cancelButtonLinkButtonID = "CancelButtonLinkButton";

		// Token: 0x040002D7 RID: 727
		private const string _continueButtonID = "ContinueButton";

		// Token: 0x040002D8 RID: 728
		private const string _continueButtonButtonID = "ContinueButtonButton";

		// Token: 0x040002D9 RID: 729
		private const string _continueButtonImageButtonID = "ContinueButtonImageButton";

		// Token: 0x040002DA RID: 730
		private const string _continueButtonLinkButtonID = "ContinueButtonLinkButton";

		// Token: 0x040002DB RID: 731
		private const string _helpLinkID = "HelpLink";

		// Token: 0x040002DC RID: 732
		private const string _editProfileLinkID = "EditProfileLink";

		// Token: 0x040002DD RID: 733
		private const string _createUserButtonID = "StepNextButton";

		// Token: 0x040002DE RID: 734
		private const string _createUserButtonButtonID = "StepNextButtonButton";

		// Token: 0x040002DF RID: 735
		private const string _createUserButtonImageButtonID = "StepNextButtonImageButton";

		// Token: 0x040002E0 RID: 736
		private const string _createUserButtonLinkButtonID = "StepNextButtonLinkButton";

		// Token: 0x040002E1 RID: 737
		private const string _createUserNavigationTemplateName = "CreateUserNavigationTemplate";

		// Token: 0x040002E2 RID: 738
		private const string _previousButtonID = "StepNextButton";

		// Token: 0x040002E3 RID: 739
		private const string _previousButtonButtonID = "StepPreviousButton";

		// Token: 0x040002E4 RID: 740
		private const string _previousButtonImageButtonID = "StepPreviousButtonImageButton";

		// Token: 0x040002E5 RID: 741
		private const string _previousButtonLinkButtonID = "StepPreviousButtonLinkButton";

		// Token: 0x040002E6 RID: 742
		private static DesignerAutoFormatCollection _autoFormats;

		// Token: 0x040002E7 RID: 743
		private static readonly Hashtable _persistedIDConverter;

		// Token: 0x040002E8 RID: 744
		private static readonly Hashtable _completeStepConverter;

		// Token: 0x040002E9 RID: 745
		private static readonly string[] _persistedControlIDs = new string[]
		{
			"UserName",
			"UserNameRequired",
			"Password",
			"PasswordRequired",
			"ConfirmPassword",
			"Email",
			"Question",
			"Answer",
			"ConfirmPasswordRequired",
			"PasswordRegExp",
			"EmailRegExp",
			"EmailRequired",
			"QuestionRequired",
			"AnswerRequired",
			"PasswordCompare",
			"CancelButton",
			"ContinueButton",
			"StepNextButton",
			"ErrorMessage",
			"HelpLink",
			"EditProfileLink"
		};

		// Token: 0x040002EA RID: 746
		private static readonly string[] _persistedIfNotVisibleControlIDs = new string[]
		{
			"ErrorMessage"
		};

		// Token: 0x040002EB RID: 747
		private static readonly string[] _defaultCreateStepProperties = new string[]
		{
			"AnswerLabelText",
			"ConfirmPasswordLabelText",
			"ConfirmPasswordCompareErrorMessage",
			"ConfirmPasswordRequiredErrorMessage",
			"EmailLabelText",
			"ErrorMessageStyle",
			"HelpPageIconUrl",
			"HelpPageText",
			"HelpPageUrl",
			"HyperLinkStyle",
			"InstructionText",
			"InstructionTextStyle",
			"LabelStyle",
			"PasswordHintText",
			"PasswordHintStyle",
			"PasswordLabelText",
			"PasswordRequiredErrorMessage",
			"QuestionLabelText",
			"TextBoxStyle",
			"UserNameLabelText",
			"UserNameRequiredErrorMessage",
			"AnswerRequiredErrorMessage",
			"EmailRegularExpression",
			"EmailRegularExpressionErrorMessage",
			"EmailRequiredErrorMessage",
			"PasswordRegularExpression",
			"PasswordRegularExpressionErrorMessage",
			"QuestionRequiredErrorMessage",
			"ValidatorTextStyle"
		};

		// Token: 0x040002EC RID: 748
		private static readonly string[] _defaultCreateUserNavProperties = new string[]
		{
			"CancelButtonImageUrl",
			"CancelButtonType",
			"CancelButtonStyle",
			"CancelButtonText",
			"CreateUserButtonImageUrl",
			"CreateUserButtonType",
			"CreateUserButtonStyle",
			"CreateUserButtonText"
		};

		// Token: 0x040002ED RID: 749
		private static readonly string[] _defaultCompleteStepProperties = new string[]
		{
			"CompleteSuccessText",
			"CompleteSuccessTextStyle",
			"ContinueButtonStyle",
			"ContinueButtonText",
			"ContinueButtonType",
			"ContinueButtonImageUrl",
			"EditProfileText",
			"EditProfileIconUrl",
			"EditProfileUrl"
		};

		// Token: 0x020003DD RID: 989
		private class RowCells
		{
			// Token: 0x06002725 RID: 10021 RVA: 0x000F13EF File Offset: 0x000EF5EF
			internal RowCells(TableRow row, ArrayList cells)
			{
				this._row = row;
				this._cells = cells;
			}

			// Token: 0x04001C24 RID: 7204
			internal TableRow _row;

			// Token: 0x04001C25 RID: 7205
			internal ArrayList _cells;
		}

		// Token: 0x020003DE RID: 990
		private class CellControls
		{
			// Token: 0x06002726 RID: 10022 RVA: 0x000F1405 File Offset: 0x000EF605
			internal CellControls(TableCell cell, ArrayList controls)
			{
				this._cell = cell;
				this._controls = controls;
			}

			// Token: 0x04001C26 RID: 7206
			internal TableCell _cell;

			// Token: 0x04001C27 RID: 7207
			internal ArrayList _controls;
		}

		// Token: 0x020003DF RID: 991
		private class CreateUserWizardDesignerActionList : DesignerActionList
		{
			// Token: 0x06002727 RID: 10023 RVA: 0x000F141B File Offset: 0x000EF61B
			public CreateUserWizardDesignerActionList(CreateUserWizardDesigner parent) : base(parent.Component)
			{
				this._parent = parent;
			}

			// Token: 0x1700083C RID: 2108
			// (get) Token: 0x06002728 RID: 10024 RVA: 0x00003B0F File Offset: 0x00001D0F
			// (set) Token: 0x06002729 RID: 10025 RVA: 0x00003937 File Offset: 0x00001B37
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

			// Token: 0x0600272A RID: 10026 RVA: 0x000F1430 File Offset: 0x000EF630
			public void CustomizeCreateUserStep()
			{
				Cursor value = Cursor.Current;
				try
				{
					Cursor.Current = Cursors.WaitCursor;
					this._parent.CustomizeCreateUserStep();
				}
				finally
				{
					Cursor.Current = value;
				}
			}

			// Token: 0x0600272B RID: 10027 RVA: 0x000F1474 File Offset: 0x000EF674
			public void CustomizeCompleteStep()
			{
				Cursor value = Cursor.Current;
				try
				{
					Cursor.Current = Cursors.WaitCursor;
					this._parent.CustomizeCompleteStep();
				}
				finally
				{
					Cursor.Current = value;
				}
			}

			// Token: 0x0600272C RID: 10028 RVA: 0x000F14B8 File Offset: 0x000EF6B8
			public void LaunchWebAdmin()
			{
				this._parent.LaunchWebAdmin();
			}

			// Token: 0x0600272D RID: 10029 RVA: 0x000F14C5 File Offset: 0x000EF6C5
			public void ResetCreateUserStep()
			{
				this._parent.ResetCreateUserStep();
			}

			// Token: 0x0600272E RID: 10030 RVA: 0x000F14D2 File Offset: 0x000EF6D2
			public void ResetCompleteStep()
			{
				this._parent.ResetCompleteStep();
			}

			// Token: 0x0600272F RID: 10031 RVA: 0x000F14E0 File Offset: 0x000EF6E0
			public override DesignerActionItemCollection GetSortedActionItems()
			{
				if (this._parent.InTemplateMode)
				{
					return new DesignerActionItemCollection();
				}
				DesignerActionItemCollection designerActionItemCollection = new DesignerActionItemCollection();
				if (this._parent._createUserWizard.CreateUserStep.ContentTemplate == null)
				{
					designerActionItemCollection.Add(new DesignerActionMethodItem(this, "CustomizeCreateUserStep", SR.GetString("CreateUserWizard_CustomizeCreateUserStep"), string.Empty, SR.GetString("CreateUserWizard_CustomizeCreateUserStepDescription"), true));
				}
				else
				{
					designerActionItemCollection.Add(new DesignerActionMethodItem(this, "ResetCreateUserStep", SR.GetString("CreateUserWizard_ResetCreateUserStepVerb"), string.Empty, SR.GetString("CreateUserWizard_ResetCreateUserStepVerbDescription"), true));
				}
				if (this._parent._createUserWizard.CompleteStep.ContentTemplate == null)
				{
					designerActionItemCollection.Add(new DesignerActionMethodItem(this, "CustomizeCompleteStep", SR.GetString("CreateUserWizard_CustomizeCompleteStep"), string.Empty, SR.GetString("CreateUserWizard_CustomizeCompleteStepDescription"), true));
				}
				else
				{
					designerActionItemCollection.Add(new DesignerActionMethodItem(this, "ResetCompleteStep", SR.GetString("CreateUserWizard_ResetCompleteStepVerb"), string.Empty, SR.GetString("CreateUserWizard_ResetCompleteStepVerbDescription"), true));
				}
				designerActionItemCollection.Add(new DesignerActionMethodItem(this, "LaunchWebAdmin", SR.GetString("Login_LaunchWebAdmin"), string.Empty, SR.GetString("Login_LaunchWebAdminDescription"), true));
				return designerActionItemCollection;
			}

			// Token: 0x04001C28 RID: 7208
			private CreateUserWizardDesigner _parent;
		}
	}
}
