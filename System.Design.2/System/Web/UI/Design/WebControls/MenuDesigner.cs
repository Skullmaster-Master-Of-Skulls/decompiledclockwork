using System;
using System.Collections;
using System.Collections.Specialized;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Design;
using System.Globalization;
using System.Web.UI.Design.Util;
using System.Web.UI.WebControls;
using System.Windows.Forms;

namespace System.Web.UI.Design.WebControls
{
	// Token: 0x020000E7 RID: 231
	public class MenuDesigner : HierarchicalDataBoundControlDesigner, IDataBindingSchemaProvider
	{
		// Token: 0x170001DA RID: 474
		// (get) Token: 0x060007D1 RID: 2001 RVA: 0x0002B560 File Offset: 0x00029760
		public override DesignerActionListCollection ActionLists
		{
			get
			{
				DesignerActionListCollection designerActionListCollection = new DesignerActionListCollection();
				designerActionListCollection.AddRange(base.ActionLists);
				designerActionListCollection.Add(new MenuDesigner.MenuDesignerActionList(this));
				return designerActionListCollection;
			}
		}

		// Token: 0x170001DB RID: 475
		// (get) Token: 0x060007D2 RID: 2002 RVA: 0x0002B58D File Offset: 0x0002978D
		public override DesignerAutoFormatCollection AutoFormats
		{
			get
			{
				if (MenuDesigner._autoFormats == null)
				{
					MenuDesigner._autoFormats = ControlDesigner.CreateAutoFormats(AutoFormatSchemes.MENU_SCHEME_NAMES, (string schemeName) => new MenuAutoFormat(schemeName, "<Schemes>\r\n<xsd:schema id=\"Schemes\" xmlns=\"\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns:msdata=\"urn:schemas-microsoft-com:xml-msdata\">\r\n  <xsd:element name=\"Scheme\">\r\n     <xsd:complexType>\r\n       <xsd:all>\r\n        <xsd:element name=\"SchemeName\" type=\"xsd:string\"/>\r\n        <xsd:element name=\"BackColor\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n        <xsd:element name=\"BorderColor\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n        <xsd:element name=\"BorderWidth\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n        <xsd:element name=\"BorderStyle\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n        <xsd:element name=\"DynamicHorizontalOffset\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n        <xsd:element name=\"DynamicHoverStyle-BackColor\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n        <xsd:element name=\"DynamicHoverStyle-Font--ClearDefaults\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n        <xsd:element name=\"DynamicHoverStyle-ForeColor\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n        <xsd:element name=\"DynamicMenuItemStyle-HorizontalPadding\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n        <xsd:element name=\"DynamicMenuItemStyle-VerticalPadding\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n        <xsd:element name=\"DynamicMenuStyle-BackColor\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n        <xsd:element name=\"DynamicSelectedStyle-BackColor\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n        <xsd:element name=\"Font-Size\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n        <xsd:element name=\"Font-Names\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n        <xsd:element name=\"ForeColor\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n        <xsd:element name=\"StaticHoverStyle-BackColor\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n        <xsd:element name=\"StaticHoverStyle-Font--ClearDefaults\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n        <xsd:element name=\"StaticHoverStyle-ForeColor\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n        <xsd:element name=\"StaticMenuItemStyle-HorizontalPadding\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n        <xsd:element name=\"StaticMenuItemStyle-VerticalPadding\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n        <xsd:element name=\"StaticSelectedStyle-BackColor\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n        <xsd:element name=\"StaticSubMenuIndent\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n      </xsd:all>\r\n    </xsd:complexType>\r\n  </xsd:element>\r\n  <xsd:element name=\"Schemes\" msdata:IsDataSet=\"true\">\r\n    <xsd:complexType>\r\n      <xsd:choice maxOccurs=\"unbounded\">\r\n        <xsd:element ref=\"Scheme\"/>\r\n      </xsd:choice>\r\n    </xsd:complexType>\r\n  </xsd:element>\r\n</xsd:schema>\r\n<Scheme>\r\n    <SchemeName>MenuScheme_Empty</SchemeName>\r\n    <BackColor></BackColor>\r\n    <BorderColor></BorderColor>\r\n    <BorderWidth></BorderWidth>\r\n    <BorderStyle>notset</BorderStyle>\r\n    <DynamicHorizontalOffset>0</DynamicHorizontalOffset>\r\n    <DynamicHoverStyle-BackColor></DynamicHoverStyle-BackColor>\r\n    <DynamicHoverStyle-Font--ClearDefaults>true</DynamicHoverStyle-Font--ClearDefaults>\r\n    <DynamicHoverStyle-ForeColor></DynamicHoverStyle-ForeColor>\r\n    <DynamicMenuItemStyle-HorizontalPadding></DynamicMenuItemStyle-HorizontalPadding>\r\n    <DynamicMenuItemStyle-VerticalPadding></DynamicMenuItemStyle-VerticalPadding>\r\n    <DynamicMenuStyle-BackColor></DynamicMenuStyle-BackColor>\r\n    <DynamicSelectedStyle-BackColor></DynamicSelectedStyle-BackColor>\r\n    <Font-Size></Font-Size>\r\n    <Font-Names></Font-Names>\r\n    <ForeColor></ForeColor>\r\n    <StaticHoverStyle-BackColor></StaticHoverStyle-BackColor>\r\n    <StaticHoverStyle-Font--ClearDefaults>true</StaticHoverStyle-Font--ClearDefaults>\r\n    <StaticHoverStyle-ForeColor></StaticHoverStyle-ForeColor>\r\n    <StaticMenuItemStyle-HorizontalPadding></StaticMenuItemStyle-HorizontalPadding>\r\n    <StaticMenuItemStyle-VerticalPadding></StaticMenuItemStyle-VerticalPadding>\r\n    <StaticSelectedStyle-BackColor></StaticSelectedStyle-BackColor>\r\n    <StaticSubMenuIndent>16px</StaticSubMenuIndent>\r\n</Scheme>\r\n  <Scheme>\r\n    <SchemeName>MenuScheme_Classic</SchemeName>\r\n    <BackColor>#B5C7DE</BackColor>\r\n    <BorderColor></BorderColor>\r\n    <BorderWidth></BorderWidth>\r\n    <BorderStyle>notset</BorderStyle>\r\n    <DynamicHorizontalOffset>2</DynamicHorizontalOffset>\r\n    <DynamicHoverStyle-BackColor>#284E98</DynamicHoverStyle-BackColor>\r\n    <DynamicHoverStyle-Font--ClearDefaults>false</DynamicHoverStyle-Font--ClearDefaults>\r\n    <DynamicHoverStyle-ForeColor>White</DynamicHoverStyle-ForeColor>\r\n    <DynamicMenuItemStyle-HorizontalPadding>5</DynamicMenuItemStyle-HorizontalPadding>\r\n    <DynamicMenuItemStyle-VerticalPadding>2</DynamicMenuItemStyle-VerticalPadding>\r\n    <DynamicMenuStyle-BackColor>#B5C7DE</DynamicMenuStyle-BackColor>\r\n    <DynamicSelectedStyle-BackColor>#507CD1</DynamicSelectedStyle-BackColor>\r\n    <Font-Names>Verdana</Font-Names>\r\n    <Font-Size>0.8em</Font-Size>\r\n    <ForeColor>#284E98</ForeColor>\r\n    <StaticHoverStyle-BackColor>#284E98</StaticHoverStyle-BackColor>\r\n    <StaticHoverStyle-Font--ClearDefaults>false</StaticHoverStyle-Font--ClearDefaults>\r\n    <StaticHoverStyle-ForeColor>White</StaticHoverStyle-ForeColor>\r\n    <StaticMenuItemStyle-HorizontalPadding>5</StaticMenuItemStyle-HorizontalPadding>\r\n    <StaticMenuItemStyle-VerticalPadding>2</StaticMenuItemStyle-VerticalPadding>\r\n    <StaticSelectedStyle-BackColor>#507CD1</StaticSelectedStyle-BackColor>\r\n    <StaticSubMenuIndent>10px</StaticSubMenuIndent>\r\n  </Scheme>\r\n<Scheme>\r\n    <SchemeName>MenuScheme_Colorful</SchemeName>\r\n    <BackColor>#FFFBD6</BackColor>\r\n    <BorderColor></BorderColor>\r\n    <BorderWidth></BorderWidth>\r\n    <BorderStyle>notset</BorderStyle>\r\n    <DynamicHorizontalOffset>2</DynamicHorizontalOffset>\r\n    <DynamicHoverStyle-BackColor>#990000</DynamicHoverStyle-BackColor>\r\n    <DynamicHoverStyle-Font--ClearDefaults>false</DynamicHoverStyle-Font--ClearDefaults>\r\n    <DynamicHoverStyle-ForeColor>White</DynamicHoverStyle-ForeColor>\r\n    <DynamicMenuItemStyle-HorizontalPadding>5</DynamicMenuItemStyle-HorizontalPadding>\r\n    <DynamicMenuItemStyle-VerticalPadding>2</DynamicMenuItemStyle-VerticalPadding>\r\n    <DynamicMenuStyle-BackColor>#FFFBD6</DynamicMenuStyle-BackColor>\r\n    <DynamicSelectedStyle-BackColor>#FFCC66</DynamicSelectedStyle-BackColor>\r\n    <Font-Names>Verdana</Font-Names>\r\n    <Font-Size>0.8em</Font-Size>\r\n    <ForeColor>#990000</ForeColor>\r\n    <StaticHoverStyle-BackColor>#990000</StaticHoverStyle-BackColor>\r\n    <StaticHoverStyle-Font--ClearDefaults>false</StaticHoverStyle-Font--ClearDefaults>\r\n    <StaticHoverStyle-ForeColor>White</StaticHoverStyle-ForeColor>\r\n    <StaticMenuItemStyle-HorizontalPadding>5</StaticMenuItemStyle-HorizontalPadding>\r\n    <StaticMenuItemStyle-VerticalPadding>2</StaticMenuItemStyle-VerticalPadding>\r\n    <StaticSelectedStyle-BackColor>#FFCC66</StaticSelectedStyle-BackColor>\r\n    <StaticSubMenuIndent>10px</StaticSubMenuIndent>\r\n</Scheme>\r\n<Scheme>\r\n    <SchemeName>MenuScheme_Professional</SchemeName>\r\n    <BackColor>#F7F6F3</BackColor>\r\n    <BorderColor></BorderColor>\r\n    <BorderWidth></BorderWidth>\r\n    <BorderStyle>notset</BorderStyle>\r\n    <DynamicHorizontalOffset>2</DynamicHorizontalOffset>\r\n    <DynamicHoverStyle-BackColor>#7C6F57</DynamicHoverStyle-BackColor>\r\n    <DynamicHoverStyle-Font--ClearDefaults>false</DynamicHoverStyle-Font--ClearDefaults>\r\n    <DynamicHoverStyle-ForeColor>White</DynamicHoverStyle-ForeColor>\r\n    <DynamicMenuItemStyle-HorizontalPadding>5</DynamicMenuItemStyle-HorizontalPadding>\r\n    <DynamicMenuItemStyle-VerticalPadding>2</DynamicMenuItemStyle-VerticalPadding>\r\n    <DynamicMenuStyle-BackColor>#F7F6F3</DynamicMenuStyle-BackColor>\r\n    <DynamicSelectedStyle-BackColor>#5D7B9D</DynamicSelectedStyle-BackColor>\r\n    <Font-Names>Verdana</Font-Names>\r\n    <Font-Size>0.8em</Font-Size>\r\n    <ForeColor>#7C6F57</ForeColor>\r\n    <StaticHoverStyle-BackColor>#7C6F57</StaticHoverStyle-BackColor>\r\n    <StaticHoverStyle-Font--ClearDefaults>false</StaticHoverStyle-Font--ClearDefaults>\r\n    <StaticHoverStyle-ForeColor>White</StaticHoverStyle-ForeColor>\r\n    <StaticMenuItemStyle-HorizontalPadding>5</StaticMenuItemStyle-HorizontalPadding>\r\n    <StaticMenuItemStyle-VerticalPadding>2</StaticMenuItemStyle-VerticalPadding>\r\n    <StaticSelectedStyle-BackColor>#5D7B9D</StaticSelectedStyle-BackColor>\r\n    <StaticSubMenuIndent>10px</StaticSubMenuIndent>\r\n</Scheme>\r\n  <Scheme>\r\n    <SchemeName>MenuScheme_Simple</SchemeName>\r\n    <BackColor>#E3EAEB</BackColor>\r\n    <BorderColor></BorderColor>\r\n    <BorderWidth></BorderWidth>\r\n    <BorderStyle>notset</BorderStyle>\r\n    <DynamicHorizontalOffset>2</DynamicHorizontalOffset>\r\n    <DynamicHoverStyle-BackColor>#666666</DynamicHoverStyle-BackColor>\r\n    <DynamicHoverStyle-Font--ClearDefaults>false</DynamicHoverStyle-Font--ClearDefaults>\r\n    <DynamicHoverStyle-ForeColor>White</DynamicHoverStyle-ForeColor>\r\n    <DynamicMenuItemStyle-HorizontalPadding>5</DynamicMenuItemStyle-HorizontalPadding>\r\n    <DynamicMenuItemStyle-VerticalPadding>2</DynamicMenuItemStyle-VerticalPadding>\r\n    <DynamicMenuStyle-BackColor>#E3EAEB</DynamicMenuStyle-BackColor>\r\n    <DynamicSelectedStyle-BackColor>#1C5E55</DynamicSelectedStyle-BackColor>\r\n    <Font-Names>Verdana</Font-Names>\r\n    <Font-Size>0.8em</Font-Size>\r\n    <ForeColor>#666666</ForeColor>\r\n    <StaticHoverStyle-BackColor>#666666</StaticHoverStyle-BackColor>\r\n    <StaticHoverStyle-Font--ClearDefaults>false</StaticHoverStyle-Font--ClearDefaults>\r\n    <StaticHoverStyle-ForeColor>White</StaticHoverStyle-ForeColor>\r\n    <StaticMenuItemStyle-HorizontalPadding>5</StaticMenuItemStyle-HorizontalPadding>\r\n    <StaticMenuItemStyle-VerticalPadding>2</StaticMenuItemStyle-VerticalPadding>\r\n    <StaticSelectedStyle-BackColor>#1C5E55</StaticSelectedStyle-BackColor>\r\n    <StaticSubMenuIndent>10px</StaticSubMenuIndent>\r\n  </Scheme>\r\n</Schemes>\r\n"));
				}
				return MenuDesigner._autoFormats;
			}
		}

		// Token: 0x060007D3 RID: 2003 RVA: 0x0002B5C9 File Offset: 0x000297C9
		private void ConvertToDynamicTemplate()
		{
			ControlDesigner.InvokeTransactedChange(base.Component, new TransactedChangeCallback(this.ConvertToDynamicTemplateChangeCallback), null, SR.GetString("MenuDesigner_ConvertToDynamicTemplate"));
		}

		// Token: 0x060007D4 RID: 2004 RVA: 0x0002B5F0 File Offset: 0x000297F0
		private bool ConvertToDynamicTemplateChangeCallback(object context)
		{
			string dynamicItemFormatString = this._menu.DynamicItemFormatString;
			string templateText;
			if (dynamicItemFormatString != null && dynamicItemFormatString.Length != 0)
			{
				templateText = "<%# Eval(\"Text\", \"" + dynamicItemFormatString + "\") %>";
			}
			else
			{
				templateText = "<%# Eval(\"Text\") %>";
			}
			IDesignerHost designerHost = (IDesignerHost)this.GetService(typeof(IDesignerHost));
			if (designerHost != null)
			{
				this._menu.DynamicItemTemplate = ControlParser.ParseTemplate(designerHost, templateText);
			}
			return true;
		}

		// Token: 0x060007D5 RID: 2005 RVA: 0x0002B65B File Offset: 0x0002985B
		private void ConvertToStaticTemplate()
		{
			ControlDesigner.InvokeTransactedChange(base.Component, new TransactedChangeCallback(this.ConvertToStaticTemplateChangeCallback), null, SR.GetString("MenuDesigner_ConvertToStaticTemplate"));
		}

		// Token: 0x060007D6 RID: 2006 RVA: 0x0002B680 File Offset: 0x00029880
		private bool ConvertToStaticTemplateChangeCallback(object context)
		{
			string staticItemFormatString = this._menu.StaticItemFormatString;
			string templateText;
			if (staticItemFormatString != null && staticItemFormatString.Length != 0)
			{
				templateText = "<%# Eval(\"Text\", \"" + staticItemFormatString + "\") %>";
			}
			else
			{
				templateText = "<%# Eval(\"Text\") %>";
			}
			IDesignerHost designerHost = (IDesignerHost)this.GetService(typeof(IDesignerHost));
			if (designerHost != null)
			{
				this._menu.StaticItemTemplate = ControlParser.ParseTemplate(designerHost, templateText);
			}
			return true;
		}

		// Token: 0x060007D7 RID: 2007 RVA: 0x0002B6EB File Offset: 0x000298EB
		private void ResetDynamicTemplate()
		{
			ControlDesigner.InvokeTransactedChange(base.Component, new TransactedChangeCallback(this.ResetDynamicTemplateChangeCallback), null, SR.GetString("MenuDesigner_ResetDynamicTemplate"));
		}

		// Token: 0x060007D8 RID: 2008 RVA: 0x0002B70F File Offset: 0x0002990F
		private bool ResetDynamicTemplateChangeCallback(object context)
		{
			this._menu.Controls.Clear();
			this._menu.DynamicItemTemplate = null;
			return true;
		}

		// Token: 0x060007D9 RID: 2009 RVA: 0x0002B72E File Offset: 0x0002992E
		private void ResetStaticTemplate()
		{
			ControlDesigner.InvokeTransactedChange(base.Component, new TransactedChangeCallback(this.ResetStaticTemplateChangeCallback), null, SR.GetString("MenuDesigner_ResetStaticTemplate"));
		}

		// Token: 0x060007DA RID: 2010 RVA: 0x0002B752 File Offset: 0x00029952
		private bool ResetStaticTemplateChangeCallback(object context)
		{
			this._menu.Controls.Clear();
			this._menu.StaticItemTemplate = null;
			return true;
		}

		// Token: 0x170001DC RID: 476
		// (get) Token: 0x060007DB RID: 2011 RVA: 0x0002B771 File Offset: 0x00029971
		private bool DynamicTemplated
		{
			get
			{
				return this._menu.DynamicItemTemplate != null;
			}
		}

		// Token: 0x170001DD RID: 477
		// (get) Token: 0x060007DC RID: 2012 RVA: 0x0002B781 File Offset: 0x00029981
		private bool StaticTemplated
		{
			get
			{
				return this._menu.StaticItemTemplate != null;
			}
		}

		// Token: 0x170001DE RID: 478
		// (get) Token: 0x060007DD RID: 2013 RVA: 0x0002B794 File Offset: 0x00029994
		public override TemplateGroupCollection TemplateGroups
		{
			get
			{
				TemplateGroupCollection templateGroups = base.TemplateGroups;
				if (this._templateGroups == null)
				{
					this._templateGroups = new TemplateGroupCollection();
					TemplateGroup templateGroup = new TemplateGroup("Item Templates", ((WebControl)base.ViewControl).ControlStyle);
					templateGroup.AddTemplateDefinition(new TemplateDefinition(this, MenuDesigner._templateNames[0], this._menu, MenuDesigner._templateNames[0], ((System.Web.UI.WebControls.Menu)base.ViewControl).StaticMenuStyle)
					{
						SupportsDataBinding = true
					});
					templateGroup.AddTemplateDefinition(new TemplateDefinition(this, MenuDesigner._templateNames[1], this._menu, MenuDesigner._templateNames[1], ((System.Web.UI.WebControls.Menu)base.ViewControl).DynamicMenuStyle)
					{
						SupportsDataBinding = true
					});
					this._templateGroups.Add(templateGroup);
				}
				templateGroups.AddRange(this._templateGroups);
				return templateGroups;
			}
		}

		// Token: 0x170001DF RID: 479
		// (get) Token: 0x060007DE RID: 2014 RVA: 0x00003B0F File Offset: 0x00001D0F
		protected override bool UsePreviewControl
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060007DF RID: 2015 RVA: 0x0002B868 File Offset: 0x00029A68
		protected override void DataBind(BaseDataBoundControl dataBoundControl)
		{
			System.Web.UI.WebControls.Menu menu = (System.Web.UI.WebControls.Menu)dataBoundControl;
			if ((menu.DataSourceID != null && menu.DataSourceID.Length > 0) || menu.DataSource != null || menu.Items.Count == 0)
			{
				menu.Items.Clear();
				base.DataBind(menu);
			}
		}

		// Token: 0x060007E0 RID: 2016 RVA: 0x0002B8BC File Offset: 0x00029ABC
		private void EditBindings()
		{
			IServiceProvider site = this._menu.Site;
			MenuBindingsEditorForm form = new MenuBindingsEditorForm(site, this._menu, this);
			UIServiceHelper.ShowDialog(site, form);
		}

		// Token: 0x060007E1 RID: 2017 RVA: 0x0002B8EC File Offset: 0x00029AEC
		private void EditMenuItems()
		{
			PropertyDescriptor member = TypeDescriptor.GetProperties(base.Component)["Items"];
			ControlDesigner.InvokeTransactedChange(base.Component, new TransactedChangeCallback(this.EditMenuItemsChangeCallback), null, SR.GetString("MenuDesigner_EditNodesTransactionDescription"), member);
		}

		// Token: 0x060007E2 RID: 2018 RVA: 0x0002B934 File Offset: 0x00029B34
		private bool EditMenuItemsChangeCallback(object context)
		{
			IServiceProvider site = this._menu.Site;
			MenuItemCollectionEditorDialog form = new MenuItemCollectionEditorDialog(this._menu, this);
			DialogResult dialogResult = UIServiceHelper.ShowDialog(site, form);
			return dialogResult == DialogResult.OK;
		}

		// Token: 0x060007E3 RID: 2019 RVA: 0x0002B968 File Offset: 0x00029B68
		public override string GetDesignTimeHtml()
		{
			string result;
			try
			{
				System.Web.UI.WebControls.Menu menu = (System.Web.UI.WebControls.Menu)base.ViewControl;
				ListDictionary listDictionary = new ListDictionary();
				listDictionary.Add("DesignTimeTextWriterType", typeof(DesignTimeHtmlTextWriter));
				((IControlDesignerAccessor)base.ViewControl).SetDesignModeState(listDictionary);
				int maximumDynamicDisplayLevels = menu.MaximumDynamicDisplayLevels;
				if (maximumDynamicDisplayLevels > 10)
				{
					menu.MaximumDynamicDisplayLevels = 10;
				}
				this.DataBind((BaseDataBoundControl)base.ViewControl);
				IDictionary designModeState = ((IControlDesignerAccessor)base.ViewControl).GetDesignModeState();
				MenuDesigner.ViewType currentView = this._currentView;
				if (currentView != MenuDesigner.ViewType.Static)
				{
					if (currentView == MenuDesigner.ViewType.Dynamic)
					{
						result = (string)designModeState["GetDesignTimeDynamicHtml"];
					}
					else
					{
						if (maximumDynamicDisplayLevels > 10)
						{
							menu.MaximumDynamicDisplayLevels = maximumDynamicDisplayLevels;
						}
						result = base.GetDesignTimeHtml();
					}
				}
				else
				{
					result = (string)designModeState["GetDesignTimeStaticHtml"];
				}
			}
			catch (Exception e)
			{
				result = this.GetErrorDesignTimeHtml(e);
			}
			return result;
		}

		// Token: 0x060007E4 RID: 2020 RVA: 0x0002BA48 File Offset: 0x00029C48
		protected override string GetEmptyDesignTimeHtml()
		{
			string name = this._menu.Site.Name;
			return string.Format(CultureInfo.CurrentUICulture, "\r\n                <table cellpadding=4 cellspacing=0 style=\"font-family:Tahoma;font-size:8pt;color:buttontext;background-color:buttonface\">\r\n                  <tr><td><span style=\"font-weight:bold\">Menu</span> - {0}</td></tr>\r\n                  <tr><td>{1}</td></tr>\r\n                </table>\r\n             ", new object[]
			{
				name,
				SR.GetString("MenuDesigner_Empty")
			});
		}

		// Token: 0x060007E5 RID: 2021 RVA: 0x0002BA8C File Offset: 0x00029C8C
		protected override string GetErrorDesignTimeHtml(Exception e)
		{
			string name = this._menu.Site.Name;
			return string.Format(CultureInfo.CurrentUICulture, "\r\n                <table cellpadding=4 cellspacing=0 style=\"font-family:Tahoma;font-size:8pt;color:buttontext;background-color:buttonface;border: solid 1px;border-top-color:buttonhighlight;border-left-color:buttonhighlight;border-bottom-color:buttonshadow;border-right-color:buttonshadow\">\r\n                  <tr><td><span style=\"font-weight:bold\">Menu</span> - {0}</td></tr>\r\n                  <tr><td>{1}</td></tr>\r\n                </table>\r\n             ", new object[]
			{
				name,
				SR.GetString("MenuDesigner_Error", new object[]
				{
					e.Message
				})
			});
		}

		// Token: 0x060007E6 RID: 2022 RVA: 0x0002BADF File Offset: 0x00029CDF
		protected override IHierarchicalEnumerable GetSampleDataSource()
		{
			return new MenuDesigner.MenuSampleData(this._menu, 0, string.Empty);
		}

		// Token: 0x060007E7 RID: 2023 RVA: 0x0002BAF2 File Offset: 0x00029CF2
		public override void Initialize(IComponent component)
		{
			ControlDesigner.VerifyInitializeArgument(component, typeof(System.Web.UI.WebControls.Menu));
			base.Initialize(component);
			this._menu = (System.Web.UI.WebControls.Menu)component;
			base.SetViewFlags(ViewFlags.TemplateEditing, true);
		}

		// Token: 0x060007E8 RID: 2024 RVA: 0x0002BB1F File Offset: 0x00029D1F
		internal void InvokeMenuBindingsEditor()
		{
			this.EditBindings();
		}

		// Token: 0x060007E9 RID: 2025 RVA: 0x0002BB27 File Offset: 0x00029D27
		internal void InvokeMenuItemCollectionEditor()
		{
			this.EditMenuItems();
		}

		// Token: 0x170001E0 RID: 480
		// (get) Token: 0x060007EA RID: 2026 RVA: 0x0002BB2F File Offset: 0x00029D2F
		bool IDataBindingSchemaProvider.CanRefreshSchema
		{
			get
			{
				return this.CanRefreshSchema;
			}
		}

		// Token: 0x170001E1 RID: 481
		// (get) Token: 0x060007EB RID: 2027 RVA: 0x0000445B File Offset: 0x0000265B
		protected bool CanRefreshSchema
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170001E2 RID: 482
		// (get) Token: 0x060007EC RID: 2028 RVA: 0x0002BB37 File Offset: 0x00029D37
		IDataSourceViewSchema IDataBindingSchemaProvider.Schema
		{
			get
			{
				return this.Schema;
			}
		}

		// Token: 0x170001E3 RID: 483
		// (get) Token: 0x060007ED RID: 2029 RVA: 0x0002BB3F File Offset: 0x00029D3F
		protected IDataSourceViewSchema Schema
		{
			get
			{
				return new MenuDesigner.MenuItemSchema();
			}
		}

		// Token: 0x060007EE RID: 2030 RVA: 0x00003937 File Offset: 0x00001B37
		protected void RefreshSchema(bool preferSilent)
		{
		}

		// Token: 0x060007EF RID: 2031 RVA: 0x0002BB46 File Offset: 0x00029D46
		void IDataBindingSchemaProvider.RefreshSchema(bool preferSilent)
		{
			this.RefreshSchema(preferSilent);
		}

		// Token: 0x040004A5 RID: 1189
		private System.Web.UI.WebControls.Menu _menu;

		// Token: 0x040004A6 RID: 1190
		private TemplateGroupCollection _templateGroups;

		// Token: 0x040004A7 RID: 1191
		private static DesignerAutoFormatCollection _autoFormats;

		// Token: 0x040004A8 RID: 1192
		private MenuDesigner.ViewType _currentView;

		// Token: 0x040004A9 RID: 1193
		private const string _getDesignTimeStaticHtml = "GetDesignTimeStaticHtml";

		// Token: 0x040004AA RID: 1194
		private const string _getDesignTimeDynamicHtml = "GetDesignTimeDynamicHtml";

		// Token: 0x040004AB RID: 1195
		private const string emptyDesignTimeHtml = "\r\n                <table cellpadding=4 cellspacing=0 style=\"font-family:Tahoma;font-size:8pt;color:buttontext;background-color:buttonface\">\r\n                  <tr><td><span style=\"font-weight:bold\">Menu</span> - {0}</td></tr>\r\n                  <tr><td>{1}</td></tr>\r\n                </table>\r\n             ";

		// Token: 0x040004AC RID: 1196
		private const string errorDesignTimeHtml = "\r\n                <table cellpadding=4 cellspacing=0 style=\"font-family:Tahoma;font-size:8pt;color:buttontext;background-color:buttonface;border: solid 1px;border-top-color:buttonhighlight;border-left-color:buttonhighlight;border-bottom-color:buttonshadow;border-right-color:buttonshadow\">\r\n                  <tr><td><span style=\"font-weight:bold\">Menu</span> - {0}</td></tr>\r\n                  <tr><td>{1}</td></tr>\r\n                </table>\r\n             ";

		// Token: 0x040004AD RID: 1197
		private const int _maxDesignDepth = 10;

		// Token: 0x040004AE RID: 1198
		private static readonly string[] _templateNames = new string[]
		{
			"StaticItemTemplate",
			"DynamicItemTemplate"
		};

		// Token: 0x0200040D RID: 1037
		private enum ViewType
		{
			// Token: 0x04001C7C RID: 7292
			Static,
			// Token: 0x04001C7D RID: 7293
			Dynamic
		}

		// Token: 0x0200040E RID: 1038
		private class MenuDesignerActionList : DesignerActionList
		{
			// Token: 0x060027EB RID: 10219 RVA: 0x000F47E3 File Offset: 0x000F29E3
			public MenuDesignerActionList(MenuDesigner parent) : base(parent.Component)
			{
				this._parent = parent;
			}

			// Token: 0x1700085B RID: 2139
			// (get) Token: 0x060027EC RID: 10220 RVA: 0x00003B0F File Offset: 0x00001D0F
			// (set) Token: 0x060027ED RID: 10221 RVA: 0x00003937 File Offset: 0x00001B37
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

			// Token: 0x1700085C RID: 2140
			// (get) Token: 0x060027EE RID: 10222 RVA: 0x000F47F8 File Offset: 0x000F29F8
			// (set) Token: 0x060027EF RID: 10223 RVA: 0x000F4830 File Offset: 0x000F2A30
			[TypeConverter(typeof(MenuDesigner.MenuDesignerActionList.MenuViewTypeConverter))]
			public string View
			{
				get
				{
					if (this._parent._currentView == MenuDesigner.ViewType.Static)
					{
						return SR.GetString("Menu_StaticView");
					}
					if (this._parent._currentView == MenuDesigner.ViewType.Dynamic)
					{
						return SR.GetString("Menu_DynamicView");
					}
					return string.Empty;
				}
				set
				{
					if (string.Compare(value, SR.GetString("Menu_StaticView"), StringComparison.Ordinal) == 0)
					{
						this._parent._currentView = MenuDesigner.ViewType.Static;
					}
					else if (string.Compare(value, SR.GetString("Menu_DynamicView"), StringComparison.Ordinal) == 0)
					{
						this._parent._currentView = MenuDesigner.ViewType.Dynamic;
					}
					TypeDescriptor.Refresh(this._parent.Component);
					this._parent.UpdateDesignTimeHtml();
				}
			}

			// Token: 0x060027F0 RID: 10224 RVA: 0x000F4898 File Offset: 0x000F2A98
			public void ConvertToDynamicTemplate()
			{
				this._parent.ConvertToDynamicTemplate();
			}

			// Token: 0x060027F1 RID: 10225 RVA: 0x000F48A5 File Offset: 0x000F2AA5
			public void ResetDynamicTemplate()
			{
				this._parent.ResetDynamicTemplate();
			}

			// Token: 0x060027F2 RID: 10226 RVA: 0x000F48B2 File Offset: 0x000F2AB2
			public void ConvertToStaticTemplate()
			{
				this._parent.ConvertToStaticTemplate();
			}

			// Token: 0x060027F3 RID: 10227 RVA: 0x000F48BF File Offset: 0x000F2ABF
			public void ResetStaticTemplate()
			{
				this._parent.ResetStaticTemplate();
			}

			// Token: 0x060027F4 RID: 10228 RVA: 0x000F48CC File Offset: 0x000F2ACC
			public void EditBindings()
			{
				this._parent.EditBindings();
			}

			// Token: 0x060027F5 RID: 10229 RVA: 0x000F48D9 File Offset: 0x000F2AD9
			public void EditMenuItems()
			{
				this._parent.EditMenuItems();
			}

			// Token: 0x060027F6 RID: 10230 RVA: 0x000F48E8 File Offset: 0x000F2AE8
			public override DesignerActionItemCollection GetSortedActionItems()
			{
				DesignerActionItemCollection designerActionItemCollection = new DesignerActionItemCollection();
				string @string = SR.GetString("MenuDesigner_DataActionGroup");
				designerActionItemCollection.Add(new DesignerActionPropertyItem("View", SR.GetString("WebControls_Views"), @string, SR.GetString("MenuDesigner_ViewsDescription"))
				{
					ShowInSourceView = false
				});
				if (string.IsNullOrEmpty(this._parent.DataSourceID))
				{
					designerActionItemCollection.Add(new DesignerActionMethodItem(this, "EditMenuItems", SR.GetString("MenuDesigner_EditMenuItems"), @string, SR.GetString("MenuDesigner_EditMenuItemsDescription"), true));
				}
				else
				{
					designerActionItemCollection.Add(new DesignerActionMethodItem(this, "EditBindings", SR.GetString("MenuDesigner_EditBindings"), @string, SR.GetString("MenuDesigner_EditBindingsDescription"), true));
				}
				if (this._parent.DynamicTemplated)
				{
					designerActionItemCollection.Add(new DesignerActionMethodItem(this, "ResetDynamicTemplate", SR.GetString("MenuDesigner_ResetDynamicTemplate"), @string, SR.GetString("MenuDesigner_ResetDynamicTemplateDescription"), true));
				}
				else
				{
					designerActionItemCollection.Add(new DesignerActionMethodItem(this, "ConvertToDynamicTemplate", SR.GetString("MenuDesigner_ConvertToDynamicTemplate"), @string, SR.GetString("MenuDesigner_ConvertToDynamicTemplateDescription"), true));
				}
				if (this._parent.StaticTemplated)
				{
					designerActionItemCollection.Add(new DesignerActionMethodItem(this, "ResetStaticTemplate", SR.GetString("MenuDesigner_ResetStaticTemplate"), @string, SR.GetString("MenuDesigner_ResetStaticTemplateDescription"), true));
				}
				else
				{
					designerActionItemCollection.Add(new DesignerActionMethodItem(this, "ConvertToStaticTemplate", SR.GetString("MenuDesigner_ConvertToStaticTemplate"), @string, SR.GetString("MenuDesigner_ConvertToStaticTemplateDescription"), true));
				}
				return designerActionItemCollection;
			}

			// Token: 0x04001C7E RID: 7294
			private MenuDesigner _parent;

			// Token: 0x020005C3 RID: 1475
			private class MenuViewTypeConverter : TypeConverter
			{
				// Token: 0x06003400 RID: 13312 RVA: 0x0011C188 File Offset: 0x0011A388
				public override TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
				{
					return new TypeConverter.StandardValuesCollection(new string[]
					{
						SR.GetString("Menu_StaticView"),
						SR.GetString("Menu_DynamicView")
					});
				}

				// Token: 0x06003401 RID: 13313 RVA: 0x00003B0F File Offset: 0x00001D0F
				public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
				{
					return true;
				}

				// Token: 0x06003402 RID: 13314 RVA: 0x00003B0F File Offset: 0x00001D0F
				public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
				{
					return true;
				}
			}
		}

		// Token: 0x0200040F RID: 1039
		private class MenuSampleData : IHierarchicalEnumerable, IEnumerable
		{
			// Token: 0x060027F7 RID: 10231 RVA: 0x000F4A58 File Offset: 0x000F2C58
			public MenuSampleData(System.Web.UI.WebControls.Menu menu, int depth, string path)
			{
				this._list = new ArrayList();
				this._menu = menu;
				int num = this._menu.StaticDisplayLevels + this._menu.MaximumDynamicDisplayLevels;
				if (num < this._menu.StaticDisplayLevels || num < this._menu.MaximumDynamicDisplayLevels)
				{
					num = int.MaxValue;
				}
				if (depth == 0)
				{
					this._list.Add(new MenuDesigner.MenuSampleDataNode(this._menu, SR.GetString("HierarchicalDataBoundControlDesigner_SampleRoot"), depth, path, false));
					this._list.Add(new MenuDesigner.MenuSampleDataNode(this._menu, SR.GetString("HierarchicalDataBoundControlDesigner_SampleRoot"), depth, path));
					this._list.Add(new MenuDesigner.MenuSampleDataNode(this._menu, SR.GetString("HierarchicalDataBoundControlDesigner_SampleRoot"), depth, path, false));
					this._list.Add(new MenuDesigner.MenuSampleDataNode(this._menu, SR.GetString("HierarchicalDataBoundControlDesigner_SampleRoot"), depth, path, false));
					this._list.Add(new MenuDesigner.MenuSampleDataNode(this._menu, SR.GetString("HierarchicalDataBoundControlDesigner_SampleRoot"), depth, path, false));
					return;
				}
				if (depth <= this._menu.StaticDisplayLevels && depth < 10)
				{
					this._list.Add(new MenuDesigner.MenuSampleDataNode(this._menu, SR.GetString("HierarchicalDataBoundControlDesigner_SampleParent", new object[]
					{
						depth
					}), depth, path));
					return;
				}
				if (depth < num && depth < 10)
				{
					this._list.Add(new MenuDesigner.MenuSampleDataNode(this._menu, SR.GetString("HierarchicalDataBoundControlDesigner_SampleLeaf", new object[]
					{
						1
					}), depth, path));
					this._list.Add(new MenuDesigner.MenuSampleDataNode(this._menu, SR.GetString("HierarchicalDataBoundControlDesigner_SampleLeaf", new object[]
					{
						2
					}), depth, path));
					this._list.Add(new MenuDesigner.MenuSampleDataNode(this._menu, SR.GetString("HierarchicalDataBoundControlDesigner_SampleLeaf", new object[]
					{
						3
					}), depth, path));
					this._list.Add(new MenuDesigner.MenuSampleDataNode(this._menu, SR.GetString("HierarchicalDataBoundControlDesigner_SampleLeaf", new object[]
					{
						4
					}), depth, path));
				}
			}

			// Token: 0x060027F8 RID: 10232 RVA: 0x000F4C8E File Offset: 0x000F2E8E
			public IEnumerator GetEnumerator()
			{
				return this._list.GetEnumerator();
			}

			// Token: 0x060027F9 RID: 10233 RVA: 0x000F3F91 File Offset: 0x000F2191
			public IHierarchyData GetHierarchyData(object enumeratedItem)
			{
				return (IHierarchyData)enumeratedItem;
			}

			// Token: 0x04001C7F RID: 7295
			private ArrayList _list;

			// Token: 0x04001C80 RID: 7296
			private System.Web.UI.WebControls.Menu _menu;
		}

		// Token: 0x02000410 RID: 1040
		private class MenuSampleDataNode : IHierarchyData
		{
			// Token: 0x060027FA RID: 10234 RVA: 0x000F4C9B File Offset: 0x000F2E9B
			public MenuSampleDataNode(System.Web.UI.WebControls.Menu menu, string text, int depth, string path) : this(menu, text, depth, path, true)
			{
			}

			// Token: 0x060027FB RID: 10235 RVA: 0x000F4CA9 File Offset: 0x000F2EA9
			public MenuSampleDataNode(System.Web.UI.WebControls.Menu menu, string text, int depth, string path, bool hasChildren)
			{
				this._text = text;
				this._depth = depth;
				this._path = path + "\\" + text;
				this._menu = menu;
				this._hasChildren = hasChildren;
			}

			// Token: 0x1700085D RID: 2141
			// (get) Token: 0x060027FC RID: 10236 RVA: 0x000F4CE4 File Offset: 0x000F2EE4
			public bool HasChildren
			{
				get
				{
					if (!this._hasChildren)
					{
						return false;
					}
					int num = this._menu.StaticDisplayLevels + this._menu.MaximumDynamicDisplayLevels;
					if (num < this._menu.StaticDisplayLevels || num < this._menu.MaximumDynamicDisplayLevels)
					{
						num = int.MaxValue;
					}
					return this._depth < num && this._depth < 10;
				}
			}

			// Token: 0x1700085E RID: 2142
			// (get) Token: 0x060027FD RID: 10237 RVA: 0x000F4D4B File Offset: 0x000F2F4B
			public string Path
			{
				get
				{
					return this._path;
				}
			}

			// Token: 0x1700085F RID: 2143
			// (get) Token: 0x060027FE RID: 10238 RVA: 0x0000CA50 File Offset: 0x0000AC50
			public object Item
			{
				get
				{
					return this;
				}
			}

			// Token: 0x17000860 RID: 2144
			// (get) Token: 0x060027FF RID: 10239 RVA: 0x000F3FD7 File Offset: 0x000F21D7
			public string Type
			{
				get
				{
					return "SampleData";
				}
			}

			// Token: 0x06002800 RID: 10240 RVA: 0x000F4D53 File Offset: 0x000F2F53
			public override string ToString()
			{
				return this._text;
			}

			// Token: 0x06002801 RID: 10241 RVA: 0x000F4D5B File Offset: 0x000F2F5B
			public IHierarchicalEnumerable GetChildren()
			{
				return new MenuDesigner.MenuSampleData(this._menu, this._depth + 1, this._path);
			}

			// Token: 0x06002802 RID: 10242 RVA: 0x00003598 File Offset: 0x00001798
			public IHierarchyData GetParent()
			{
				return null;
			}

			// Token: 0x04001C81 RID: 7297
			private string _text;

			// Token: 0x04001C82 RID: 7298
			private int _depth;

			// Token: 0x04001C83 RID: 7299
			private string _path;

			// Token: 0x04001C84 RID: 7300
			private System.Web.UI.WebControls.Menu _menu;

			// Token: 0x04001C85 RID: 7301
			private bool _hasChildren;
		}

		// Token: 0x02000411 RID: 1041
		private class MenuItemSchema : IDataSourceViewSchema
		{
			// Token: 0x06002803 RID: 10243 RVA: 0x000F4D78 File Offset: 0x000F2F78
			static MenuItemSchema()
			{
				PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(System.Web.UI.WebControls.MenuItem));
				MenuDesigner.MenuItemSchema._fieldSchema = new IDataSourceFieldSchema[]
				{
					new TypeFieldSchema(properties["DataPath"]),
					new TypeFieldSchema(properties["Depth"]),
					new TypeFieldSchema(properties["Enabled"]),
					new TypeFieldSchema(properties["ImageUrl"]),
					new TypeFieldSchema(properties["NavigateUrl"]),
					new TypeFieldSchema(properties["PopOutImageUrl"]),
					new TypeFieldSchema(properties["Selectable"]),
					new TypeFieldSchema(properties["Selected"]),
					new TypeFieldSchema(properties["SeparatorImageUrl"]),
					new TypeFieldSchema(properties["Target"]),
					new TypeFieldSchema(properties["Text"]),
					new TypeFieldSchema(properties["ToolTip"]),
					new TypeFieldSchema(properties["Value"]),
					new TypeFieldSchema(properties["ValuePath"])
				};
			}

			// Token: 0x17000861 RID: 2145
			// (get) Token: 0x06002805 RID: 10245 RVA: 0x000F4EB0 File Offset: 0x000F30B0
			string IDataSourceViewSchema.Name
			{
				get
				{
					return "MenuItem";
				}
			}

			// Token: 0x06002806 RID: 10246 RVA: 0x000F4EB7 File Offset: 0x000F30B7
			IDataSourceViewSchema[] IDataSourceViewSchema.GetChildren()
			{
				return new IDataSourceViewSchema[0];
			}

			// Token: 0x06002807 RID: 10247 RVA: 0x000F4EBF File Offset: 0x000F30BF
			IDataSourceFieldSchema[] IDataSourceViewSchema.GetFields()
			{
				return MenuDesigner.MenuItemSchema._fieldSchema;
			}

			// Token: 0x04001C86 RID: 7302
			private static IDataSourceFieldSchema[] _fieldSchema;
		}
	}
}
