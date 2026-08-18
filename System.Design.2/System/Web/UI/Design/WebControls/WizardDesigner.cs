using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Design;
using System.Drawing.Design;
using System.Globalization;
using System.Security.Permissions;
using System.Text;
using System.Web.UI.WebControls;

namespace System.Web.UI.Design.WebControls
{
	// Token: 0x02000133 RID: 307
	[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
	public class WizardDesigner : CompositeControlDesigner
	{
		// Token: 0x17000276 RID: 630
		// (get) Token: 0x06000B14 RID: 2836 RVA: 0x000478BC File Offset: 0x00045ABC
		public override DesignerActionListCollection ActionLists
		{
			get
			{
				DesignerActionListCollection designerActionListCollection = new DesignerActionListCollection();
				designerActionListCollection.AddRange(base.ActionLists);
				designerActionListCollection.Add(new WizardDesigner.WizardDesignerActionList(this));
				return designerActionListCollection;
			}
		}

		// Token: 0x17000277 RID: 631
		// (get) Token: 0x06000B15 RID: 2837 RVA: 0x000478E9 File Offset: 0x00045AE9
		internal WizardStepBase ActiveStep
		{
			get
			{
				if (this.ActiveStepIndex != -1)
				{
					return this._wizard.WizardSteps[this.ActiveStepIndex];
				}
				return null;
			}
		}

		// Token: 0x17000278 RID: 632
		// (get) Token: 0x06000B16 RID: 2838 RVA: 0x0004790C File Offset: 0x00045B0C
		internal int ActiveStepIndex
		{
			get
			{
				int activeStepIndex = this._wizard.ActiveStepIndex;
				if (activeStepIndex == -1 && this._wizard.WizardSteps.Count > 0)
				{
					return 0;
				}
				return activeStepIndex;
			}
		}

		// Token: 0x17000279 RID: 633
		// (get) Token: 0x06000B17 RID: 2839 RVA: 0x0004793F File Offset: 0x00045B3F
		public override DesignerAutoFormatCollection AutoFormats
		{
			get
			{
				if (this._autoFormats == null)
				{
					this._autoFormats = ControlDesigner.CreateAutoFormats(AutoFormatSchemes.WIZARD_SCHEME_NAMES, (string schemeName) => new WizardAutoFormat(schemeName, "<Schemes>\r\n        <xsd:schema id=\"Schemes\" xmlns=\"\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns:msdata=\"urn:schemas-microsoft-com:xml-msdata\">\r\n          <xsd:element name=\"Scheme\">\r\n            <xsd:complexType>\r\n              <xsd:all>\r\n                <xsd:element name=\"SchemeName\" type=\"xsd:string\"/>\r\n                <xsd:element name=\"FontName\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n                <xsd:element name=\"FontSize\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n                <xsd:element name=\"BackColor\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n                <xsd:element name=\"BorderColor\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n                <xsd:element name=\"BorderWidth\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n                <xsd:element name=\"BorderStyle\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n                <xsd:element name=\"NavigationButtonStyleBorderWidth\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n                <xsd:element name=\"NavigationButtonStyleFontName\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n                <xsd:element name=\"NavigationButtonStyleFontSize\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n                <xsd:element name=\"NavigationButtonStyleBorderStyle\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n                <xsd:element name=\"NavigationButtonStyleBorderColor\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n                <xsd:element name=\"NavigationButtonStyleForeColor\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n                <xsd:element name=\"NavigationButtonStyleBackColor\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n                <xsd:element name=\"StepStyleBorderWidth\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n                <xsd:element name=\"StepStyleBorderStyle\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n                <xsd:element name=\"StepStyleBorderColor\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n                <xsd:element name=\"StepStyleForeColor\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n                <xsd:element name=\"StepStyleBackColor\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n                <xsd:element name=\"StepStyleFontSize\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n                <xsd:element name=\"SideBarButtonStyleFontUnderline\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n                <xsd:element name=\"SideBarButtonStyleFontName\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n                <xsd:element name=\"SideBarButtonStyleForeColor\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n                <xsd:element name=\"SideBarButtonStyleBorderWidth\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n                <xsd:element name=\"SideBarButtonStyleBackColor\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n                <xsd:element name=\"HeaderStyleForeColor\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n                <xsd:element name=\"HeaderStyleBorderColor\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n                <xsd:element name=\"HeaderStyleBackColor\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n                <xsd:element name=\"HeaderStyleFontSize\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n                <xsd:element name=\"HeaderStyleFontBold\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n                <xsd:element name=\"HeaderStyleBorderWidth\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n                <xsd:element name=\"HeaderStyleHorizontalAlign\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n                <xsd:element name=\"HeaderStyleBorderStyle\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n                <xsd:element name=\"SideBarStyleBackColor\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n                <xsd:element name=\"SideBarStyleVerticalAlign\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n                <xsd:element name=\"SideBarStyleFontSize\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n                <xsd:element name=\"SideBarStyleFontUnderline\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n                <xsd:element name=\"SideBarStyleFontStrikeout\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n                <xsd:element name=\"SideBarStyleBorderWidth\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n              </xsd:all>\r\n            </xsd:complexType>\r\n          </xsd:element>\r\n          <xsd:element name=\"Schemes\" msdata:IsDataSet=\"true\">\r\n            <xsd:complexType>\r\n              <xsd:choice maxOccurs=\"unbounded\">\r\n                <xsd:element ref=\"Scheme\"/>\r\n              </xsd:choice>\r\n            </xsd:complexType>\r\n          </xsd:element>\r\n        </xsd:schema>\r\n        <Scheme>\r\n          <SchemeName>WizardAFmt_Scheme_Default</SchemeName>\r\n        </Scheme>\r\n        <Scheme>\r\n          <SchemeName>WizardAFmt_Scheme_Colorful</SchemeName>\r\n          <FontName>Verdana</FontName>\r\n          <FontSize>0.8em</FontSize>\r\n          <BackColor>#FFFBD6</BackColor>\r\n          <BorderColor>#FFDFAD</BorderColor>\r\n          <BorderWidth>1px</BorderWidth>\r\n          <NavigationButtonStyleBorderWidth>1px</NavigationButtonStyleBorderWidth>\r\n          <NavigationButtonStyleFontName>Verdana</NavigationButtonStyleFontName>\r\n          <NavigationButtonStyleFontSize>0.8em</NavigationButtonStyleFontSize>\r\n          <NavigationButtonStyleBorderStyle>4</NavigationButtonStyleBorderStyle>\r\n          <NavigationButtonStyleBorderColor>#CC9966</NavigationButtonStyleBorderColor>\r\n          <NavigationButtonStyleForeColor>#990000</NavigationButtonStyleForeColor>\r\n          <NavigationButtonStyleBackColor>White</NavigationButtonStyleBackColor>\r\n          <SideBarButtonStyleFontUnderline>False</SideBarButtonStyleFontUnderline>\r\n          <SideBarButtonStyleForeColor>White</SideBarButtonStyleForeColor>\r\n          <HeaderStyleForeColor>#333333</HeaderStyleForeColor>\r\n          <HeaderStyleBorderColor>#FFFBD6</HeaderStyleBorderColor>\r\n          <HeaderStyleBackColor>#FFCC66</HeaderStyleBackColor>\r\n          <HeaderStyleFontSize>0.9em</HeaderStyleFontSize>\r\n          <HeaderStyleFontBold>True</HeaderStyleFontBold>\r\n          <HeaderStyleBorderWidth>2px</HeaderStyleBorderWidth>\r\n          <HeaderStyleHorizontalAlign>2</HeaderStyleHorizontalAlign>\r\n          <HeaderStyleBorderStyle>4</HeaderStyleBorderStyle>\r\n          <SideBarStyleBackColor>#990000</SideBarStyleBackColor>\r\n          <SideBarStyleVerticalAlign>1</SideBarStyleVerticalAlign>\r\n          <SideBarStyleFontSize>0.9em</SideBarStyleFontSize>\r\n          <SideBarStyleFontUnderline>False</SideBarStyleFontUnderline>\r\n        </Scheme>\r\n        <Scheme>\r\n          <SchemeName>WizardAFmt_Scheme_Professional</SchemeName>\r\n          <FontName>Verdana</FontName>\r\n          <FontSize>0.8em</FontSize>\r\n          <BackColor>#F7F6F3</BackColor>\r\n          <BorderColor>#CCCCCC</BorderColor>\r\n          <BorderWidth>1px</BorderWidth>\r\n          <BorderStyle>4</BorderStyle>\r\n          <StepStyleForeColor>#5D7B9D</StepStyleForeColor>\r\n          <StepStyleBorderWidth>0px</StepStyleBorderWidth>\r\n          <NavigationButtonStyleBorderWidth>1px</NavigationButtonStyleBorderWidth>\r\n          <NavigationButtonStyleFontName>Verdana</NavigationButtonStyleFontName>\r\n          <NavigationButtonStyleFontSize>0.8em</NavigationButtonStyleFontSize>\r\n          <NavigationButtonStyleBorderStyle>4</NavigationButtonStyleBorderStyle>\r\n          <NavigationButtonStyleBorderColor>#CCCCCC</NavigationButtonStyleBorderColor>\r\n          <NavigationButtonStyleForeColor>#284775</NavigationButtonStyleForeColor>\r\n          <NavigationButtonStyleBackColor>#FFFBFF</NavigationButtonStyleBackColor>\r\n          <SideBarButtonStyleFontUnderline>False</SideBarButtonStyleFontUnderline>\r\n          <SideBarButtonStyleFontName>Verdana</SideBarButtonStyleFontName>\r\n          <SideBarButtonStyleForeColor>White</SideBarButtonStyleForeColor>\r\n          <SideBarButtonStyleBorderWidth>0px</SideBarButtonStyleBorderWidth>\r\n          <HeaderStyleForeColor>White</HeaderStyleForeColor>\r\n          <HeaderStyleBackColor>#5D7B9D</HeaderStyleBackColor>\r\n          <HeaderStyleFontSize>0.9em</HeaderStyleFontSize>\r\n          <HeaderStyleFontBold>True</HeaderStyleFontBold>\r\n          <HeaderStyleHorizontalAlign>1</HeaderStyleHorizontalAlign>\r\n          <HeaderStyleBorderStyle>4</HeaderStyleBorderStyle>\r\n          <SideBarStyleBackColor>#7C6F57</SideBarStyleBackColor>\r\n          <SideBarStyleVerticalAlign>1</SideBarStyleVerticalAlign>\r\n          <SideBarStyleFontSize>0.9em</SideBarStyleFontSize>\r\n          <SideBarStyleBorderWidth>0px</SideBarStyleBorderWidth>\r\n        </Scheme>\r\n        <Scheme>\r\n          <SchemeName>WizardAFmt_Scheme_Classic</SchemeName>\r\n          <FontName>Verdana</FontName>\r\n          <FontSize>0.8em</FontSize>\r\n          <BackColor>#EFF3FB</BackColor>\r\n          <BorderColor>#B5C7DE</BorderColor>\r\n          <BorderWidth>1px</BorderWidth>\r\n          <StepStyleForeColor>#333333</StepStyleForeColor>\r\n          <StepStyleFontSize>0.8em</StepStyleFontSize>\r\n          <NavigationButtonStyleBorderWidth>1px</NavigationButtonStyleBorderWidth>\r\n          <NavigationButtonStyleFontName>Verdana</NavigationButtonStyleFontName>\r\n          <NavigationButtonStyleFontSize>0.8em</NavigationButtonStyleFontSize>\r\n          <NavigationButtonStyleBorderStyle>4</NavigationButtonStyleBorderStyle>\r\n          <NavigationButtonStyleBorderColor>#507CD1</NavigationButtonStyleBorderColor>\r\n          <NavigationButtonStyleForeColor>#284E98</NavigationButtonStyleForeColor>\r\n          <NavigationButtonStyleBackColor>White</NavigationButtonStyleBackColor>\r\n          <SideBarButtonStyleFontUnderline>False</SideBarButtonStyleFontUnderline>\r\n          <SideBarButtonStyleFontName>Verdana</SideBarButtonStyleFontName>\r\n          <SideBarButtonStyleForeColor>White</SideBarButtonStyleForeColor>\r\n          <SideBarButtonStyleBackColor>#507CD1</SideBarButtonStyleBackColor>\r\n          <HeaderStyleForeColor>White</HeaderStyleForeColor>\r\n          <HeaderStyleBorderColor>#EFF3FB</HeaderStyleBorderColor>\r\n          <HeaderStyleBackColor>#284E98</HeaderStyleBackColor>\r\n          <HeaderStyleFontSize>0.9em</HeaderStyleFontSize>\r\n          <HeaderStyleFontBold>True</HeaderStyleFontBold>\r\n          <HeaderStyleBorderWidth>2px</HeaderStyleBorderWidth>\r\n          <HeaderStyleHorizontalAlign>2</HeaderStyleHorizontalAlign>\r\n          <HeaderStyleBorderStyle>4</HeaderStyleBorderStyle>\r\n          <SideBarStyleBackColor>#507CD1</SideBarStyleBackColor>\r\n          <SideBarStyleVerticalAlign>1</SideBarStyleVerticalAlign>\r\n          <SideBarStyleFontSize>0.9em</SideBarStyleFontSize>\r\n        </Scheme>\r\n        <Scheme>\r\n          <SchemeName>WizardAFmt_Scheme_Simple</SchemeName>\r\n          <FontName>Verdana</FontName>\r\n          <FontSize>0.8em</FontSize>\r\n          <BackColor>#E6E2D8</BackColor>\r\n          <BorderColor>#999999</BorderColor>\r\n          <BorderWidth>1px</BorderWidth>\r\n          <BorderStyle>4</BorderStyle>\r\n          <StepStyleBorderStyle>4</StepStyleBorderStyle>\r\n          <StepStyleBorderColor>#E6E2D8</StepStyleBorderColor>\r\n          <StepStyleBackColor>#F7F6F3</StepStyleBackColor>\r\n          <StepStyleBorderWidth>2px</StepStyleBorderWidth>\r\n          <NavigationButtonStyleBorderWidth>1px</NavigationButtonStyleBorderWidth>\r\n          <NavigationButtonStyleFontName>Verdana</NavigationButtonStyleFontName>\r\n          <NavigationButtonStyleFontSize>0.8em</NavigationButtonStyleFontSize>\r\n          <NavigationButtonStyleBorderStyle>4</NavigationButtonStyleBorderStyle>\r\n          <NavigationButtonStyleBorderColor>#C5BBAF</NavigationButtonStyleBorderColor>\r\n          <NavigationButtonStyleForeColor>#1C5E55</NavigationButtonStyleForeColor>\r\n          <NavigationButtonStyleBackColor>White</NavigationButtonStyleBackColor>\r\n          <SideBarButtonStyleFontUnderline>False</SideBarButtonStyleFontUnderline>\r\n          <SideBarButtonStyleForeColor>White</SideBarButtonStyleForeColor>\r\n          <HeaderStyleForeColor>White</HeaderStyleForeColor>\r\n          <HeaderStyleBackColor>#666666</HeaderStyleBackColor>\r\n          <HeaderStyleBorderColor>#E6E2D8</HeaderStyleBorderColor>\r\n          <HeaderStyleFontSize>0.9em</HeaderStyleFontSize>\r\n          <HeaderStyleFontBold>True</HeaderStyleFontBold>\r\n          <HeaderStyleHorizontalAlign>2</HeaderStyleHorizontalAlign>\r\n          <HeaderStyleBorderStyle>4</HeaderStyleBorderStyle>\r\n          <HeaderStyleBorderWidth>2px</HeaderStyleBorderWidth>\r\n          <SideBarStyleBackColor>#1C5E55</SideBarStyleBackColor>\r\n          <SideBarStyleVerticalAlign>1</SideBarStyleVerticalAlign>\r\n          <SideBarStyleFontSize>0.9em</SideBarStyleFontSize>\r\n        </Scheme>\r\n      </Schemes>"));
				}
				return this._autoFormats;
			}
		}

		// Token: 0x1700027A RID: 634
		// (get) Token: 0x06000B18 RID: 2840 RVA: 0x0004797E File Offset: 0x00045B7E
		// (set) Token: 0x06000B19 RID: 2841 RVA: 0x00047990 File Offset: 0x00045B90
		protected bool DisplaySideBar
		{
			get
			{
				return ((Wizard)base.Component).DisplaySideBar;
			}
			set
			{
				TypeDescriptor.Refresh(base.Component);
				((Wizard)base.Component).DisplaySideBar = value;
				TypeDescriptor.Refresh(base.Component);
			}
		}

		// Token: 0x1700027B RID: 635
		// (get) Token: 0x06000B1A RID: 2842 RVA: 0x000479BC File Offset: 0x00045BBC
		internal bool SupportsDesignerRegions
		{
			get
			{
				if (!this._supportsDesignerRegionQueried)
				{
					if (base.View != null)
					{
						this._supportsDesignerRegion = base.View.SupportsRegions;
					}
					this._supportsDesignerRegionQueried = true;
				}
				return this._supportsDesignerRegion && this._wizard.LayoutTemplate == null;
			}
		}

		// Token: 0x06000B1B RID: 2843 RVA: 0x00047A0C File Offset: 0x00045C0C
		internal virtual bool InRegionEditingMode(Wizard viewControl)
		{
			if (!this.SupportsDesignerRegions)
			{
				return true;
			}
			TemplatedWizardStep templatedWizardStep = this.ActiveStep as TemplatedWizardStep;
			if (templatedWizardStep != null && templatedWizardStep.ContentTemplate == null)
			{
				TemplatedWizardStep templatedWizardStep2 = viewControl.WizardSteps[this.ActiveStepIndex] as TemplatedWizardStep;
				if (templatedWizardStep2 != null && templatedWizardStep2.ContentTemplate != null)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x1700027C RID: 636
		// (get) Token: 0x06000B1C RID: 2844 RVA: 0x00047A60 File Offset: 0x00045C60
		public override TemplateGroupCollection TemplateGroups
		{
			get
			{
				TemplateGroupCollection templateGroups = base.TemplateGroups;
				for (int i = 0; i < WizardDesigner._controlTemplateNames.Length; i++)
				{
					string text = WizardDesigner._controlTemplateNames[i];
					TemplateGroup templateGroup = new TemplateGroup(text);
					templateGroup.AddTemplateDefinition(new TemplateDefinition(this, text, this._wizard, text, this.TemplateStyleArray[i]));
					templateGroups.Add(templateGroup);
				}
				foreach (object obj in this._wizard.WizardSteps)
				{
					WizardStepBase wizardStepBase = (WizardStepBase)obj;
					string regionName = this.GetRegionName(wizardStepBase);
					TemplateGroup templateGroup2 = new TemplateGroup(regionName);
					if (wizardStepBase is TemplatedWizardStep)
					{
						for (int j = 0; j < WizardDesigner._stepTemplateNames.Length; j++)
						{
							templateGroup2.AddTemplateDefinition(new TemplateDefinition(this, WizardDesigner._stepTemplateNames[j], wizardStepBase, WizardDesigner._stepTemplateNames[j], this.StepTemplateStyleArray[j]));
						}
					}
					else if (!this.SupportsDesignerRegions)
					{
						templateGroup2.AddTemplateDefinition(new WizardStepBaseTemplateDefinition(this, wizardStepBase, regionName, this.StepTemplateStyleArray[0]));
					}
					if (!templateGroup2.IsEmpty)
					{
						templateGroups.Add(templateGroup2);
					}
				}
				return templateGroups;
			}
		}

		// Token: 0x1700027D RID: 637
		// (get) Token: 0x06000B1D RID: 2845 RVA: 0x00047BA4 File Offset: 0x00045DA4
		internal Style[] TemplateStyleArray
		{
			get
			{
				Style style = new Style();
				Wizard wizard = (Wizard)base.ViewControl;
				style.CopyFrom(wizard.ControlStyle);
				style.CopyFrom(wizard.HeaderStyle);
				Style style2 = new Style();
				style2.CopyFrom(wizard.ControlStyle);
				style2.CopyFrom(wizard.SideBarStyle);
				Style style3 = new Style();
				style3.CopyFrom(wizard.ControlStyle);
				style3.CopyFrom(wizard.NavigationStyle);
				return new Style[]
				{
					style,
					style2,
					style3,
					style3,
					style3,
					style3
				};
			}
		}

		// Token: 0x1700027E RID: 638
		// (get) Token: 0x06000B1E RID: 2846 RVA: 0x00047C3C File Offset: 0x00045E3C
		private Style[] StepTemplateStyleArray
		{
			get
			{
				Style style = new Style();
				Wizard wizard = (Wizard)base.ViewControl;
				style.CopyFrom(wizard.ControlStyle);
				style.CopyFrom(wizard.StepStyle);
				Style style2 = new Style();
				style2.CopyFrom(wizard.ControlStyle);
				style2.CopyFrom(wizard.NavigationStyle);
				return new Style[]
				{
					style,
					style2
				};
			}
		}

		// Token: 0x1700027F RID: 639
		// (get) Token: 0x06000B1F RID: 2847 RVA: 0x00003B0F File Offset: 0x00001D0F
		protected override bool UsePreviewControl
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06000B20 RID: 2848 RVA: 0x00047CA0 File Offset: 0x00045EA0
		protected virtual void AddDesignerRegions(DesignerRegionCollection regions)
		{
			if (!this.SupportsDesignerRegions)
			{
				return;
			}
			foreach (object obj in this._wizard.WizardSteps)
			{
				WizardStepBase wizardStepBase = (WizardStepBase)obj;
				if (wizardStepBase is TemplatedWizardStep)
				{
					TemplateDefinition templateDefinition = new TemplateDefinition(this, "ContentTemplate", this._wizard, "ContentTemplate", this.TemplateStyleArray[5]);
					regions.Add(new WizardStepTemplatedEditableRegion(templateDefinition, wizardStepBase)
					{
						Description = SR.GetString("ContainerControlDesigner_RegionWatermark")
					});
				}
				else
				{
					regions.Add(new WizardStepEditableRegion(this, wizardStepBase)
					{
						Description = SR.GetString("ContainerControlDesigner_RegionWatermark")
					});
				}
			}
			foreach (object obj2 in this._wizard.WizardSteps)
			{
				WizardStepBase wizardStepBase2 = (WizardStepBase)obj2;
				regions.Add(new WizardSelectableRegion(this, "Move to " + this.GetRegionName(wizardStepBase2), wizardStepBase2));
			}
		}

		// Token: 0x06000B21 RID: 2849 RVA: 0x00047DE0 File Offset: 0x00045FE0
		private ITemplate GetTemplateFromDesignModeState(string[] keys)
		{
			IDesignerHost designerHost = (IDesignerHost)this.GetService(typeof(IDesignerHost));
			IControlDesignerAccessor wizard = this._wizard;
			IDictionary designModeState = wizard.GetDesignModeState();
			this.ResetInternalControls(designModeState);
			string text = string.Empty;
			foreach (string text2 in keys)
			{
				Control control = designModeState[text2] as Control;
				if (control != null && control.Visible)
				{
					control.ID = text2;
					text += ControlPersister.PersistControl(control, designerHost);
				}
			}
			return ControlParser.ParseTemplate(designerHost, text);
		}

		// Token: 0x06000B22 RID: 2850 RVA: 0x00047E78 File Offset: 0x00046078
		protected void ConvertToTemplate(string description, IComponent component, string templateName, string[] keys)
		{
			IDesignerHost designerHost = (IDesignerHost)this.GetService(typeof(IDesignerHost));
			ControlDesigner.InvokeTransactedChange(base.Component, new TransactedChangeCallback(this.ConvertToTemplateCallBack), new Triplet(component, templateName, keys), description);
			this.UpdateDesignTimeHtml();
		}

		// Token: 0x06000B23 RID: 2851 RVA: 0x00047EC4 File Offset: 0x000460C4
		private bool ConvertToTemplateCallBack(object context)
		{
			Triplet triplet = (Triplet)context;
			IComponent component = (IComponent)triplet.First;
			string name = (string)triplet.Second;
			string[] keys = (string[])triplet.Third;
			PropertyDescriptor propertyDescriptor = TypeDescriptor.GetProperties(component)[name];
			propertyDescriptor.SetValue(component, this.GetTemplateFromDesignModeState(keys));
			return true;
		}

		// Token: 0x06000B24 RID: 2852 RVA: 0x00047F1C File Offset: 0x0004611C
		protected virtual void ConvertToCustomNavigationTemplate()
		{
			try
			{
				ITemplate context = null;
				string @string = SR.GetString("Wizard_ConvertToCustomNavigationTemplate");
				TemplatedWizardStep templatedWizardStep = this.ActiveStep as TemplatedWizardStep;
				if (templatedWizardStep != null)
				{
					TemplatedWizardStep templatedWizardStep2 = ((Wizard)base.ViewControl).ActiveStep as TemplatedWizardStep;
					if (templatedWizardStep2 != null && templatedWizardStep2.CustomNavigationTemplate != null)
					{
						context = templatedWizardStep2.CustomNavigationTemplate;
					}
					else
					{
						switch (this._wizard.GetStepType(templatedWizardStep, this.ActiveStepIndex))
						{
						case WizardStepType.Finish:
							context = this.GetTemplateFromDesignModeState(WizardDesigner._finishButtonIDs);
							break;
						case WizardStepType.Start:
							context = this.GetTemplateFromDesignModeState(WizardDesigner._startButtonIDs);
							break;
						case WizardStepType.Step:
							context = this.GetTemplateFromDesignModeState(WizardDesigner._stepButtonIDs);
							break;
						}
					}
					ControlDesigner.InvokeTransactedChange(base.Component, new TransactedChangeCallback(this.ConvertToCustomNavigationTemplateCallBack), context, @string);
				}
			}
			catch (Exception ex)
			{
			}
		}

		// Token: 0x06000B25 RID: 2853 RVA: 0x00047FF4 File Offset: 0x000461F4
		internal bool ConvertToCustomNavigationTemplateCallBack(object context)
		{
			ITemplate customNavigationTemplate = (ITemplate)context;
			TemplatedWizardStep templatedWizardStep = this.ActiveStep as TemplatedWizardStep;
			templatedWizardStep.CustomNavigationTemplate = customNavigationTemplate;
			return true;
		}

		// Token: 0x06000B26 RID: 2854 RVA: 0x0004801C File Offset: 0x0004621C
		private void ConvertToStartNavigationTemplate()
		{
			this.ConvertToTemplate(SR.GetString("Wizard_ConvertToStartNavigationTemplate"), base.Component, "StartNavigationTemplate", WizardDesigner._startButtonIDs);
		}

		// Token: 0x06000B27 RID: 2855 RVA: 0x0004803E File Offset: 0x0004623E
		private void ConvertToStepNavigationTemplate()
		{
			this.ConvertToTemplate(SR.GetString("Wizard_ConvertToStepNavigationTemplate"), base.Component, "StepNavigationTemplate", WizardDesigner._stepButtonIDs);
		}

		// Token: 0x06000B28 RID: 2856 RVA: 0x00048060 File Offset: 0x00046260
		private void ConvertToFinishNavigationTemplate()
		{
			this.ConvertToTemplate(SR.GetString("Wizard_ConvertToFinishNavigationTemplate"), base.Component, "FinishNavigationTemplate", WizardDesigner._finishButtonIDs);
		}

		// Token: 0x06000B29 RID: 2857 RVA: 0x00048082 File Offset: 0x00046282
		private void ConvertToSideBarTemplate()
		{
			this.ConvertToTemplate(SR.GetString("Wizard_ConvertToSideBarTemplate"), base.Component, "SideBarTemplate", new string[]
			{
				"SideBarList"
			});
		}

		// Token: 0x06000B2A RID: 2858 RVA: 0x000480B0 File Offset: 0x000462B0
		protected override void CreateChildControls()
		{
			base.CreateChildControls();
			Wizard wizard = (Wizard)base.ViewControl;
			if (wizard.ActiveStepIndex == -1 && wizard.WizardSteps.Count > 0)
			{
				wizard.ActiveStepIndex = 0;
			}
			IControlDesignerAccessor controlDesignerAccessor = wizard;
			IDictionary designModeState = controlDesignerAccessor.GetDesignModeState();
			TemplatedWizardStep templatedWizardStep = wizard.ActiveStep as TemplatedWizardStep;
			if (templatedWizardStep != null && templatedWizardStep.ContentTemplate != null && ((TemplatedWizardStep)this._wizard.WizardSteps[wizard.ActiveStepIndex]).ContentTemplate == null)
			{
				return;
			}
			TableCell tableCell = designModeState["StepTableCell"] as TableCell;
			if (tableCell != null && wizard.ActiveStepIndex != -1)
			{
				tableCell.Attributes["_designerRegion"] = wizard.ActiveStepIndex.ToString(NumberFormatInfo.InvariantInfo);
			}
		}

		// Token: 0x06000B2B RID: 2859 RVA: 0x00048174 File Offset: 0x00046374
		private void DataListItemDataBound(object sender, WizardSideBarListControlItemEventArgs e)
		{
			WizardSideBarListControlItem item = e.Item;
			WebControl webControl = item.FindControl("SideBarButton") as WebControl;
			if (webControl != null)
			{
				int num = item.ItemIndex + ((Wizard)base.ViewControl).WizardSteps.Count;
				webControl.Attributes["_designerRegion"] = num.ToString(NumberFormatInfo.InvariantInfo);
			}
		}

		// Token: 0x06000B2C RID: 2860 RVA: 0x000481D8 File Offset: 0x000463D8
		public override string GetDesignTimeHtml()
		{
			if (this.ActiveStepIndex == -1)
			{
				return this.GetEmptyDesignTimeHtml();
			}
			Wizard wizard = (Wizard)base.ViewControl;
			IControlDesignerAccessor controlDesignerAccessor = wizard;
			IDictionary designModeState = controlDesignerAccessor.GetDesignModeState();
			IWizardSideBarListControl wizardSideBarListControl = designModeState["SideBarList"] as IWizardSideBarListControl;
			if (wizardSideBarListControl != null)
			{
				wizardSideBarListControl.ItemDataBound += this.DataListItemDataBound;
				ICompositeControlDesignerAccessor compositeControlDesignerAccessor = wizard;
				compositeControlDesignerAccessor.RecreateChildControls();
			}
			ArrayList arrayList = new ArrayList(wizard.WizardSteps.Count);
			foreach (object obj in wizard.WizardSteps)
			{
				WizardStepBase wizardStepBase = (WizardStepBase)obj;
				arrayList.Add(wizardStepBase.Title);
				if ((wizardStepBase.Title == null || wizardStepBase.Title.Length == 0) && (wizardStepBase.ID == null || wizardStepBase.ID.Length == 0))
				{
					wizardStepBase.Title = this.GetRegionName(wizardStepBase);
				}
			}
			if (!this.InRegionEditingMode(wizard))
			{
				wizard.Enabled = true;
			}
			string text = base.GetDesignTimeHtml();
			if (text == null || text.Length == 0)
			{
				text = this.GetEmptyDesignTimeHtml();
			}
			return text;
		}

		// Token: 0x06000B2D RID: 2861 RVA: 0x00048314 File Offset: 0x00046514
		public override string GetDesignTimeHtml(DesignerRegionCollection regions)
		{
			this.AddDesignerRegions(regions);
			IControlDesignerAccessor wizard = this._wizard;
			IDictionary dictionary = null;
			try
			{
				dictionary = wizard.GetDesignModeState();
			}
			catch (Exception e)
			{
				return this.GetErrorDesignTimeHtml(e);
			}
			IWizardSideBarListControl wizardSideBarListControl = dictionary["SideBarList"] as IWizardSideBarListControl;
			if (wizardSideBarListControl != null)
			{
				wizardSideBarListControl.ItemDataBound += this.DataListItemDataBound;
			}
			Wizard wizard2 = (Wizard)base.ViewControl;
			IControlDesignerAccessor controlDesignerAccessor = wizard2;
			IDictionary designModeState = controlDesignerAccessor.GetDesignModeState();
			if (designModeState != null)
			{
				designModeState["ShouldRenderWizardSteps"] = this.InRegionEditingMode(wizard2);
			}
			return this.GetDesignTimeHtml();
		}

		// Token: 0x06000B2E RID: 2862 RVA: 0x000483BC File Offset: 0x000465BC
		public override string GetEditableDesignerRegionContent(EditableDesignerRegion region)
		{
			if (region == null)
			{
				throw new ArgumentNullException("region");
			}
			IWizardStepEditableRegion wizardStepEditableRegion = region as IWizardStepEditableRegion;
			if (wizardStepEditableRegion == null)
			{
				throw new ArgumentException(SR.GetString("Wizard_InvalidRegion"));
			}
			return this.GetEditableDesignerRegionContent(wizardStepEditableRegion);
		}

		// Token: 0x06000B2F RID: 2863 RVA: 0x000483F8 File Offset: 0x000465F8
		internal virtual string GetEditableDesignerRegionContent(IWizardStepEditableRegion region)
		{
			StringBuilder stringBuilder = new StringBuilder();
			ControlCollection controls = region.Step.Controls;
			IDesignerHost host = (IDesignerHost)base.Component.Site.GetService(typeof(IDesignerHost));
			if (region.Step is TemplatedWizardStep)
			{
				TemplatedWizardStep templatedWizardStep = (TemplatedWizardStep)region.Step;
				return ControlPersister.PersistTemplate(templatedWizardStep.ContentTemplate, host);
			}
			if (controls.Count == 1 && controls[0] is LiteralControl)
			{
				string text = ((LiteralControl)controls[0]).Text;
				if (text == null || text.Trim().Length == 0)
				{
					return string.Empty;
				}
			}
			foreach (object obj in controls)
			{
				Control control = (Control)obj;
				stringBuilder.Append(ControlPersister.PersistControl(control, host));
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000B30 RID: 2864 RVA: 0x000484FC File Offset: 0x000466FC
		internal string GetRegionName(WizardStepBase step)
		{
			if (step.Title != null && step.Title.Length > 0)
			{
				return step.Title;
			}
			if (step.ID != null && step.ID.Length > 0)
			{
				return step.ID;
			}
			return "[step (" + (step.Wizard.WizardSteps.IndexOf(step) + 1).ToString() + ")]";
		}

		// Token: 0x06000B31 RID: 2865 RVA: 0x0004856D File Offset: 0x0004676D
		public override void Initialize(IComponent component)
		{
			ControlDesigner.VerifyInitializeArgument(component, typeof(Wizard));
			this._wizard = (Wizard)component;
			base.Initialize(component);
			base.SetViewFlags(ViewFlags.TemplateEditing, true);
		}

		// Token: 0x06000B32 RID: 2866 RVA: 0x0004859C File Offset: 0x0004679C
		private void MarkPropertyNonBrowsable(IDictionary properties, string propName)
		{
			PropertyDescriptor propertyDescriptor = (PropertyDescriptor)properties[propName];
			if (propertyDescriptor != null)
			{
				properties[propName] = TypeDescriptor.CreateProperty(propertyDescriptor.ComponentType, propertyDescriptor, new Attribute[]
				{
					BrowsableAttribute.No
				});
			}
		}

		// Token: 0x06000B33 RID: 2867 RVA: 0x000485DC File Offset: 0x000467DC
		protected override void OnClick(DesignerRegionMouseEventArgs e)
		{
			base.OnClick(e);
			IDesignerHost designerHost = (IDesignerHost)this.GetService(typeof(IDesignerHost));
			WizardSelectableRegion wizardSelectableRegion = e.Region as WizardSelectableRegion;
			if (wizardSelectableRegion != null)
			{
				PropertyDescriptor propertyDescriptor = TypeDescriptor.GetProperties(this._wizard)["ActiveStepIndex"];
				int num = this._wizard.WizardSteps.IndexOf(wizardSelectableRegion.Step);
				if (this.ActiveStepIndex != num)
				{
					using (DesignerTransaction designerTransaction = designerHost.CreateTransaction("Update ActiveStepIndex"))
					{
						propertyDescriptor.SetValue(base.Component, num);
						designerTransaction.Commit();
					}
				}
			}
		}

		// Token: 0x06000B34 RID: 2868 RVA: 0x00048690 File Offset: 0x00046890
		public override void OnComponentChanged(object sender, ComponentChangedEventArgs ce)
		{
			if (ce != null && ce.Member != null && ce.Member.Name == "WizardSteps" && this._wizard.ActiveStepIndex >= this._wizard.WizardSteps.Count)
			{
				IDesignerHost designerHost = (IDesignerHost)this.GetService(typeof(IDesignerHost));
				using (DesignerTransaction designerTransaction = designerHost.CreateTransaction("Update ActiveStepIndex"))
				{
					PropertyDescriptor propertyDescriptor = TypeDescriptor.GetProperties(this._wizard)["ActiveStepIndex"];
					propertyDescriptor.SetValue(base.Component, this._wizard.WizardSteps.Count - 1);
					designerTransaction.Commit();
				}
			}
			base.OnComponentChanged(sender, ce);
		}

		// Token: 0x06000B35 RID: 2869 RVA: 0x0004876C File Offset: 0x0004696C
		protected override void PreFilterProperties(IDictionary properties)
		{
			base.PreFilterProperties(properties);
			PropertyDescriptor propertyDescriptor = (PropertyDescriptor)properties["DisplaySideBar"];
			if (propertyDescriptor != null)
			{
				properties["DisplaySideBar"] = TypeDescriptor.CreateProperty(base.GetType(), propertyDescriptor, null);
			}
			if (base.InTemplateMode)
			{
				this.MarkPropertyNonBrowsable(properties, "WizardSteps");
			}
			if (this._wizard.StartNavigationTemplate != null)
			{
				foreach (string propName in WizardDesigner._startNavigationTemplateProperties)
				{
					this.MarkPropertyNonBrowsable(properties, propName);
				}
			}
			if (this._wizard.StepNavigationTemplate != null)
			{
				foreach (string propName2 in WizardDesigner._stepNavigationTemplateProperties)
				{
					this.MarkPropertyNonBrowsable(properties, propName2);
				}
			}
			if (this._wizard.FinishNavigationTemplate != null)
			{
				foreach (string propName3 in WizardDesigner._finishNavigationTemplateProperties)
				{
					this.MarkPropertyNonBrowsable(properties, propName3);
				}
			}
			if (this._wizard.StartNavigationTemplate != null && this._wizard.StepNavigationTemplate != null && this._wizard.FinishNavigationTemplate != null)
			{
				foreach (string propName4 in WizardDesigner._generalNavigationButtonProperties)
				{
					this.MarkPropertyNonBrowsable(properties, propName4);
				}
			}
			if (this._wizard.HeaderTemplate != null)
			{
				foreach (string propName5 in WizardDesigner._headerProperties)
				{
					this.MarkPropertyNonBrowsable(properties, propName5);
				}
			}
			if (this._wizard.SideBarTemplate != null)
			{
				foreach (string propName6 in WizardDesigner._sideBarProperties)
				{
					this.MarkPropertyNonBrowsable(properties, propName6);
				}
			}
		}

		// Token: 0x06000B36 RID: 2870 RVA: 0x0004891C File Offset: 0x00046B1C
		private void ResetInternalControls(IDictionary dictionary)
		{
			IWizardSideBarListControl wizardSideBarListControl = (IWizardSideBarListControl)dictionary["SideBarList"];
			if (wizardSideBarListControl != null)
			{
				wizardSideBarListControl.SelectedIndex = -1;
			}
		}

		// Token: 0x06000B37 RID: 2871 RVA: 0x00048944 File Offset: 0x00046B44
		private void ResetCustomNavigationTemplate()
		{
			WizardStepBase activeStep = this.ActiveStep;
			ControlDesigner.InvokeTransactedChange(base.Component, new TransactedChangeCallback(this.ResetCustomNavigationTemplateCallBack), null, SR.GetString("Wizard_ResetCustomNavigationTemplate"));
		}

		// Token: 0x06000B38 RID: 2872 RVA: 0x0004897C File Offset: 0x00046B7C
		private bool ResetCustomNavigationTemplateCallBack(object context)
		{
			WizardStepBase activeStep = this.ActiveStep;
			PropertyDescriptor propertyDescriptor = TypeDescriptor.GetProperties(activeStep)["CustomNavigationTemplate"];
			propertyDescriptor.ResetValue(activeStep);
			return true;
		}

		// Token: 0x06000B39 RID: 2873 RVA: 0x000489A9 File Offset: 0x00046BA9
		private void ResetStartNavigationTemplate()
		{
			this.ResetTemplate(SR.GetString("Wizard_ResetStartNavigationTemplate"), base.Component, "StartNavigationTemplate");
		}

		// Token: 0x06000B3A RID: 2874 RVA: 0x000489C6 File Offset: 0x00046BC6
		private void ResetStepNavigationTemplate()
		{
			this.ResetTemplate(SR.GetString("Wizard_ResetStepNavigationTemplate"), base.Component, "StepNavigationTemplate");
		}

		// Token: 0x06000B3B RID: 2875 RVA: 0x000489E3 File Offset: 0x00046BE3
		private void ResetFinishNavigationTemplate()
		{
			this.ResetTemplate(SR.GetString("Wizard_ResetFinishNavigationTemplate"), base.Component, "FinishNavigationTemplate");
		}

		// Token: 0x06000B3C RID: 2876 RVA: 0x00048A00 File Offset: 0x00046C00
		private void ResetSideBarTemplate()
		{
			this.ResetTemplate(SR.GetString("Wizard_ResetSideBarTemplate"), base.Component, "SideBarTemplate");
		}

		// Token: 0x06000B3D RID: 2877 RVA: 0x00048A20 File Offset: 0x00046C20
		protected void ResetTemplate(string description, IComponent component, string templateName)
		{
			IDesignerHost designerHost = (IDesignerHost)this.GetService(typeof(IDesignerHost));
			ControlDesigner.InvokeTransactedChange(base.Component, new TransactedChangeCallback(this.ResetTemplateCallBack), new Pair(component, templateName), description);
			this.UpdateDesignTimeHtml();
		}

		// Token: 0x06000B3E RID: 2878 RVA: 0x00048A68 File Offset: 0x00046C68
		private bool ResetTemplateCallBack(object context)
		{
			Pair pair = (Pair)context;
			IComponent component = (IComponent)pair.First;
			string name = (string)pair.Second;
			PropertyDescriptor propertyDescriptor = TypeDescriptor.GetProperties(component)[name];
			propertyDescriptor.ResetValue(component);
			return true;
		}

		// Token: 0x06000B3F RID: 2879 RVA: 0x00048AAC File Offset: 0x00046CAC
		public override void SetEditableDesignerRegionContent(EditableDesignerRegion region, string content)
		{
			if (region == null)
			{
				throw new ArgumentNullException("region");
			}
			IWizardStepEditableRegion wizardStepEditableRegion = region as IWizardStepEditableRegion;
			if (wizardStepEditableRegion == null)
			{
				throw new ArgumentException(SR.GetString("Wizard_InvalidRegion"));
			}
			IDesignerHost designerHost = (IDesignerHost)base.Component.Site.GetService(typeof(IDesignerHost));
			if (wizardStepEditableRegion.Step is TemplatedWizardStep)
			{
				IComponent step = wizardStepEditableRegion.Step;
				ITemplate value = ControlParser.ParseTemplate(designerHost, content);
				PropertyDescriptor propertyDescriptor = TypeDescriptor.GetProperties(step)["ContentTemplate"];
				using (DesignerTransaction designerTransaction = designerHost.CreateTransaction("SetEditableDesignerRegionContent"))
				{
					propertyDescriptor.SetValue(step, value);
					designerTransaction.Commit();
				}
				this.ViewControlCreated = false;
				return;
			}
			this.SetWizardStepContent(wizardStepEditableRegion.Step, content, designerHost);
		}

		// Token: 0x06000B40 RID: 2880 RVA: 0x00048B80 File Offset: 0x00046D80
		private void SetWizardStepContent(WizardStepBase step, string content, IDesignerHost host)
		{
			Control[] array = null;
			if (content != null && content.Length > 0)
			{
				array = ControlParser.ParseControls(host, content);
			}
			step.Controls.Clear();
			if (array == null)
			{
				return;
			}
			foreach (Control child in array)
			{
				step.Controls.Add(child);
			}
		}

		// Token: 0x06000B41 RID: 2881 RVA: 0x00048BD4 File Offset: 0x00046DD4
		private void StartWizardStepCollectionEditor()
		{
			IDesignerHost designerHost = (IDesignerHost)this.GetService(typeof(IDesignerHost));
			PropertyDescriptor propertyDescriptor = TypeDescriptor.GetProperties(base.Component)["WizardSteps"];
			using (DesignerTransaction designerTransaction = designerHost.CreateTransaction(SR.GetString("Wizard_StartWizardStepCollectionEditor")))
			{
				UITypeEditor uitypeEditor = (UITypeEditor)propertyDescriptor.GetEditor(typeof(UITypeEditor));
				object obj = uitypeEditor.EditValue(new TypeDescriptorContext(designerHost, propertyDescriptor, base.Component), new WindowsFormsEditorServiceHelper(this), propertyDescriptor.GetValue(base.Component));
				if (obj != null)
				{
					designerTransaction.Commit();
				}
			}
			if (this._wizard.ActiveStepIndex >= -1 && this._wizard.ActiveStepIndex < this._wizard.WizardSteps.Count)
			{
				try
				{
					this.ViewControlCreated = false;
					this.CreateChildControls();
				}
				catch
				{
				}
			}
		}

		// Token: 0x0400069A RID: 1690
		private Wizard _wizard;

		// Token: 0x0400069B RID: 1691
		private DesignerAutoFormatCollection _autoFormats;

		// Token: 0x0400069C RID: 1692
		private bool _supportsDesignerRegion;

		// Token: 0x0400069D RID: 1693
		private bool _supportsDesignerRegionQueried;

		// Token: 0x0400069E RID: 1694
		private const string _headerTemplateName = "HeaderTemplate";

		// Token: 0x0400069F RID: 1695
		internal const string _customNavigationTemplateName = "CustomNavigationTemplate";

		// Token: 0x040006A0 RID: 1696
		private const string _startNavigationTemplateName = "StartNavigationTemplate";

		// Token: 0x040006A1 RID: 1697
		private const string _stepNavigationTemplateName = "StepNavigationTemplate";

		// Token: 0x040006A2 RID: 1698
		private const string _finishNavigationTemplateName = "FinishNavigationTemplate";

		// Token: 0x040006A3 RID: 1699
		private const string _sideBarTemplateName = "SideBarTemplate";

		// Token: 0x040006A4 RID: 1700
		private const string _activeStepIndexPropName = "ActiveStepIndex";

		// Token: 0x040006A5 RID: 1701
		private const string _activeStepIndexTransactionDescription = "Update ActiveStepIndex";

		// Token: 0x040006A6 RID: 1702
		private const string _startNextButtonID = "StartNextButton";

		// Token: 0x040006A7 RID: 1703
		private const string _cancelButtonID = "CancelButton";

		// Token: 0x040006A8 RID: 1704
		private const string _stepTableCellID = "StepTableCell";

		// Token: 0x040006A9 RID: 1705
		private const string _displaySideBarPropName = "DisplaySideBar";

		// Token: 0x040006AA RID: 1706
		private const string _stepPreviousButtonID = "StepPreviousButton";

		// Token: 0x040006AB RID: 1707
		private const string _stepNextButtonID = "StepNextButton";

		// Token: 0x040006AC RID: 1708
		private const string _finishButtonID = "FinishButton";

		// Token: 0x040006AD RID: 1709
		private const string _finishPreviousButtonID = "FinishPreviousButton";

		// Token: 0x040006AE RID: 1710
		private const string _dataListID = "SideBarList";

		// Token: 0x040006AF RID: 1711
		private const string _sideBarButtonID = "SideBarButton";

		// Token: 0x040006B0 RID: 1712
		internal const string _customNavigationControls = "CustomNavigationControls";

		// Token: 0x040006B1 RID: 1713
		private const string _wizardStepsPropertyName = "WizardSteps";

		// Token: 0x040006B2 RID: 1714
		internal const string _contentTemplateName = "ContentTemplate";

		// Token: 0x040006B3 RID: 1715
		private const string _navigationTemplateName = "CustomNavigationTemplate";

		// Token: 0x040006B4 RID: 1716
		private static string[] _stepTemplateNames = new string[]
		{
			"ContentTemplate",
			"CustomNavigationTemplate"
		};

		// Token: 0x040006B5 RID: 1717
		internal const int _navigationStyleLength = 6;

		// Token: 0x040006B6 RID: 1718
		private static string[] _controlTemplateNames = new string[]
		{
			"HeaderTemplate",
			"SideBarTemplate",
			"StartNavigationTemplate",
			"StepNavigationTemplate",
			"FinishNavigationTemplate"
		};

		// Token: 0x040006B7 RID: 1719
		private static readonly string[] _startNavigationTemplateProperties = new string[]
		{
			"StartNextButtonText",
			"StartNextButtonType",
			"StartNextButtonImageUrl",
			"StartNextButtonStyle"
		};

		// Token: 0x040006B8 RID: 1720
		private static readonly string[] _stepNavigationTemplateProperties = new string[]
		{
			"StepNextButtonText",
			"StepNextButtonType",
			"StepNextButtonImageUrl",
			"StepPreviousButtonText",
			"StepPreviousButtonType",
			"StepPreviousButtonImageUrl",
			"StepPreviousButtonStyle",
			"StepNextButtonStyle"
		};

		// Token: 0x040006B9 RID: 1721
		private static readonly string[] _finishNavigationTemplateProperties = new string[]
		{
			"FinishCompleteButtonText",
			"FinishCompleteButtonType",
			"FinishCompleteButtonImageUrl",
			"FinishPreviousButtonText",
			"FinishPreviousButtonType",
			"FinishPreviousButtonImageUrl",
			"FinishCompleteButtonStyle",
			"FinishPreviousButtonStyle"
		};

		// Token: 0x040006BA RID: 1722
		private static readonly string[] _generalNavigationButtonProperties = new string[]
		{
			"CancelButtonImageUrl",
			"CancelButtonText",
			"CancelButtonType",
			"DisplayCancelButton",
			"CancelButtonStyle",
			"NavigationButtonStyle"
		};

		// Token: 0x040006BB RID: 1723
		private static readonly string[] _headerProperties = new string[]
		{
			"HeaderText"
		};

		// Token: 0x040006BC RID: 1724
		private static readonly string[] _sideBarProperties = new string[]
		{
			"SideBarButtonStyle"
		};

		// Token: 0x040006BD RID: 1725
		private static string[] _startButtonIDs = new string[]
		{
			"StartNextButton",
			"CancelButton"
		};

		// Token: 0x040006BE RID: 1726
		private static string[] _stepButtonIDs = new string[]
		{
			"StepPreviousButton",
			"StepNextButton",
			"CancelButton"
		};

		// Token: 0x040006BF RID: 1727
		private static string[] _finishButtonIDs = new string[]
		{
			"FinishPreviousButton",
			"FinishButton",
			"CancelButton"
		};

		// Token: 0x02000456 RID: 1110
		private class WizardDesignerActionList : DesignerActionList
		{
			// Token: 0x06002963 RID: 10595 RVA: 0x000FA520 File Offset: 0x000F8720
			public WizardDesignerActionList(WizardDesigner designer) : base(designer.Component)
			{
				this._designer = designer;
			}

			// Token: 0x170008C6 RID: 2246
			// (get) Token: 0x06002964 RID: 10596 RVA: 0x00003B0F File Offset: 0x00001D0F
			// (set) Token: 0x06002965 RID: 10597 RVA: 0x00003937 File Offset: 0x00001B37
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

			// Token: 0x170008C7 RID: 2247
			// (get) Token: 0x06002966 RID: 10598 RVA: 0x000FA535 File Offset: 0x000F8735
			// (set) Token: 0x06002967 RID: 10599 RVA: 0x000FA544 File Offset: 0x000F8744
			[TypeConverter(typeof(WizardDesigner.WizardDesignerActionList.WizardStepTypeConverter))]
			public int View
			{
				get
				{
					return this._designer.ActiveStepIndex;
				}
				set
				{
					if (value == this._designer.ActiveStepIndex)
					{
						return;
					}
					IDesignerHost designerHost = (IDesignerHost)this._designer.GetService(typeof(IDesignerHost));
					PropertyDescriptor propertyDescriptor = TypeDescriptor.GetProperties(this._designer.Component)["ActiveStepIndex"];
					using (DesignerTransaction designerTransaction = designerHost.CreateTransaction(SR.GetString("Wizard_OnViewChanged")))
					{
						propertyDescriptor.SetValue(this._designer.Component, value);
						designerTransaction.Commit();
					}
					this._designer.UpdateDesignTimeHtml();
					TypeDescriptor.Refresh(this._designer.Component);
				}
			}

			// Token: 0x06002968 RID: 10600 RVA: 0x000FA5FC File Offset: 0x000F87FC
			public override DesignerActionItemCollection GetSortedActionItems()
			{
				DesignerActionItemCollection designerActionItemCollection = new DesignerActionItemCollection();
				if (!this._designer.InTemplateMode)
				{
					if (this._designer._wizard.WizardSteps.Count > 0)
					{
						designerActionItemCollection.Add(new DesignerActionPropertyItem("View", SR.GetString("Wizard_StepsView"), string.Empty, SR.GetString("Wizard_StepsViewDescription")));
					}
					designerActionItemCollection.Add(new DesignerActionMethodItem(this, "StartWizardStepCollectionEditor", SR.GetString("Wizard_StartWizardStepCollectionEditor"), string.Empty, SR.GetString("Wizard_StartWizardStepCollectionEditorDescription"), true));
					Wizard wizard = this._designer._wizard;
					int activeStepIndex = this._designer.ActiveStepIndex;
					if (activeStepIndex >= 0 && activeStepIndex < wizard.WizardSteps.Count)
					{
						if (wizard.StartNavigationTemplate != null)
						{
							designerActionItemCollection.Add(new DesignerActionMethodItem(this, "ResetStartNavigationTemplate", SR.GetString("Wizard_ResetStartNavigationTemplate"), string.Empty, SR.GetString("Wizard_ResetDescription", new object[]
							{
								"StartNavigation"
							}), true));
						}
						else
						{
							designerActionItemCollection.Add(new DesignerActionMethodItem(this, "ConvertToStartNavigationTemplate", SR.GetString("Wizard_ConvertToStartNavigationTemplate"), string.Empty, SR.GetString("Wizard_ConvertToTemplateDescription", new object[]
							{
								"StartNavigation"
							}), true));
						}
						if (wizard.StepNavigationTemplate != null)
						{
							designerActionItemCollection.Add(new DesignerActionMethodItem(this, "ResetStepNavigationTemplate", SR.GetString("Wizard_ResetStepNavigationTemplate"), string.Empty, SR.GetString("Wizard_ResetDescription", new object[]
							{
								"StepNavigation"
							}), true));
						}
						else
						{
							designerActionItemCollection.Add(new DesignerActionMethodItem(this, "ConvertToStepNavigationTemplate", SR.GetString("Wizard_ConvertToStepNavigationTemplate"), string.Empty, SR.GetString("Wizard_ConvertToTemplateDescription", new object[]
							{
								"StepNavigation"
							}), true));
						}
						if (wizard.FinishNavigationTemplate != null)
						{
							designerActionItemCollection.Add(new DesignerActionMethodItem(this, "ResetFinishNavigationTemplate", SR.GetString("Wizard_ResetFinishNavigationTemplate"), string.Empty, SR.GetString("Wizard_ResetDescription", new object[]
							{
								"FinishNavigation"
							}), true));
						}
						else
						{
							designerActionItemCollection.Add(new DesignerActionMethodItem(this, "ConvertToFinishNavigationTemplate", SR.GetString("Wizard_ConvertToFinishNavigationTemplate"), string.Empty, SR.GetString("Wizard_ConvertToTemplateDescription", new object[]
							{
								"FinishNavigation"
							}), true));
						}
						if (wizard.DisplaySideBar)
						{
							if (wizard.SideBarTemplate != null)
							{
								designerActionItemCollection.Add(new DesignerActionMethodItem(this, "ResetSideBarTemplate", SR.GetString("Wizard_ResetSideBarTemplate"), string.Empty, SR.GetString("Wizard_ResetDescription", new object[]
								{
									"SideBar"
								}), true));
							}
							else
							{
								designerActionItemCollection.Add(new DesignerActionMethodItem(this, "ConvertToSideBarTemplate", SR.GetString("Wizard_ConvertToSideBarTemplate"), string.Empty, SR.GetString("Wizard_ConvertToTemplateDescription", new object[]
								{
									"SideBar"
								}), true));
							}
						}
						TemplatedWizardStep templatedWizardStep = this._designer.ActiveStep as TemplatedWizardStep;
						if (templatedWizardStep != null && templatedWizardStep.StepType != WizardStepType.Complete)
						{
							if (templatedWizardStep.CustomNavigationTemplate != null)
							{
								designerActionItemCollection.Add(new DesignerActionMethodItem(this, "ResetCustomNavigationTemplate", SR.GetString("Wizard_ResetCustomNavigationTemplate"), string.Empty, SR.GetString("Wizard_ResetDescription", new object[]
								{
									"CustomNavigation"
								}), true));
							}
							else
							{
								designerActionItemCollection.Add(new DesignerActionMethodItem(this, "ConvertToCustomNavigationTemplate", SR.GetString("Wizard_ConvertToCustomNavigationTemplate"), string.Empty, SR.GetString("Wizard_ConvertToTemplateDescription", new object[]
								{
									"CustomNavigation"
								}), true));
							}
						}
					}
				}
				return designerActionItemCollection;
			}

			// Token: 0x06002969 RID: 10601 RVA: 0x000FA95C File Offset: 0x000F8B5C
			public void ConvertToCustomNavigationTemplate()
			{
				this._designer.ConvertToCustomNavigationTemplate();
			}

			// Token: 0x0600296A RID: 10602 RVA: 0x000FA969 File Offset: 0x000F8B69
			public void ConvertToFinishNavigationTemplate()
			{
				this._designer.ConvertToFinishNavigationTemplate();
			}

			// Token: 0x0600296B RID: 10603 RVA: 0x000FA976 File Offset: 0x000F8B76
			public void ConvertToSideBarTemplate()
			{
				this._designer.ConvertToSideBarTemplate();
			}

			// Token: 0x0600296C RID: 10604 RVA: 0x000FA983 File Offset: 0x000F8B83
			public void ConvertToStartNavigationTemplate()
			{
				this._designer.ConvertToStartNavigationTemplate();
			}

			// Token: 0x0600296D RID: 10605 RVA: 0x000FA990 File Offset: 0x000F8B90
			public void ConvertToStepNavigationTemplate()
			{
				this._designer.ConvertToStepNavigationTemplate();
			}

			// Token: 0x0600296E RID: 10606 RVA: 0x000FA99D File Offset: 0x000F8B9D
			public void ResetCustomNavigationTemplate()
			{
				this._designer.ResetCustomNavigationTemplate();
			}

			// Token: 0x0600296F RID: 10607 RVA: 0x000FA9AA File Offset: 0x000F8BAA
			public void ResetFinishNavigationTemplate()
			{
				this._designer.ResetFinishNavigationTemplate();
			}

			// Token: 0x06002970 RID: 10608 RVA: 0x000FA9B7 File Offset: 0x000F8BB7
			public void ResetSideBarTemplate()
			{
				this._designer.ResetSideBarTemplate();
			}

			// Token: 0x06002971 RID: 10609 RVA: 0x000FA9C4 File Offset: 0x000F8BC4
			public void ResetStartNavigationTemplate()
			{
				this._designer.ResetStartNavigationTemplate();
			}

			// Token: 0x06002972 RID: 10610 RVA: 0x000FA9D1 File Offset: 0x000F8BD1
			public void ResetStepNavigationTemplate()
			{
				this._designer.ResetStepNavigationTemplate();
			}

			// Token: 0x06002973 RID: 10611 RVA: 0x000FA9DE File Offset: 0x000F8BDE
			public void StartWizardStepCollectionEditor()
			{
				this._designer.StartWizardStepCollectionEditor();
			}

			// Token: 0x04001D45 RID: 7493
			private WizardDesigner _designer;

			// Token: 0x020005C8 RID: 1480
			private class WizardStepTypeConverter : TypeConverter
			{
				// Token: 0x06003408 RID: 13320 RVA: 0x0011C200 File Offset: 0x0011A400
				public override TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
				{
					int[] array = null;
					if (context != null)
					{
						WizardDesigner.WizardDesignerActionList wizardDesignerActionList = (WizardDesigner.WizardDesignerActionList)context.Instance;
						WizardDesigner designer = wizardDesignerActionList._designer;
						WizardStepCollection wizardSteps = designer._wizard.WizardSteps;
						array = new int[wizardSteps.Count];
						for (int i = 0; i < wizardSteps.Count; i++)
						{
							array[i] = i;
						}
					}
					return new TypeConverter.StandardValuesCollection(array);
				}

				// Token: 0x06003409 RID: 13321 RVA: 0x00003B0F File Offset: 0x00001D0F
				public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
				{
					return true;
				}

				// Token: 0x0600340A RID: 13322 RVA: 0x00003B0F File Offset: 0x00001D0F
				public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
				{
					return true;
				}

				// Token: 0x0600340B RID: 13323 RVA: 0x0011C260 File Offset: 0x0011A460
				public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
				{
					if (destinationType == typeof(string))
					{
						if (value is string)
						{
							return value;
						}
						WizardDesigner.WizardDesignerActionList wizardDesignerActionList = (WizardDesigner.WizardDesignerActionList)context.Instance;
						WizardDesigner designer = wizardDesignerActionList._designer;
						WizardStepCollection wizardSteps = designer._wizard.WizardSteps;
						if (value is int)
						{
							int num = (int)value;
							if (num == -1 && wizardSteps.Count > 0)
							{
								num = 0;
							}
							if (num >= wizardSteps.Count)
							{
								return null;
							}
							return designer.GetRegionName(wizardSteps[num]);
						}
					}
					return base.ConvertTo(context, culture, value, destinationType);
				}

				// Token: 0x0600340C RID: 13324 RVA: 0x0011C2EC File Offset: 0x0011A4EC
				public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
				{
					if (value is string)
					{
						WizardDesigner.WizardDesignerActionList wizardDesignerActionList = (WizardDesigner.WizardDesignerActionList)context.Instance;
						WizardDesigner designer = wizardDesignerActionList._designer;
						WizardStepCollection wizardSteps = designer._wizard.WizardSteps;
						for (int i = 0; i < wizardSteps.Count; i++)
						{
							if (string.Compare(designer.GetRegionName(wizardSteps[i]), (string)value, StringComparison.Ordinal) == 0)
							{
								return i;
							}
						}
					}
					return base.ConvertFrom(context, culture, value);
				}

				// Token: 0x0600340D RID: 13325 RVA: 0x00010631 File Offset: 0x0000E831
				public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
				{
					return sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);
				}

				// Token: 0x0600340E RID: 13326 RVA: 0x00010664 File Offset: 0x0000E864
				public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
				{
					return destinationType == typeof(string) || base.CanConvertTo(context, destinationType);
				}
			}
		}
	}
}
