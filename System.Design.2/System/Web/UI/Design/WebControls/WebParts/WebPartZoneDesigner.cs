using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Design;
using System.Security.Permissions;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace System.Web.UI.Design.WebControls.WebParts
{
	// Token: 0x02000155 RID: 341
	[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
	public class WebPartZoneDesigner : WebPartZoneBaseDesigner
	{
		// Token: 0x170002A0 RID: 672
		// (get) Token: 0x06000BE6 RID: 3046 RVA: 0x0004B5D8 File Offset: 0x000497D8
		public override DesignerAutoFormatCollection AutoFormats
		{
			get
			{
				if (WebPartZoneDesigner._autoFormats == null)
				{
					WebPartZoneDesigner._autoFormats = ControlDesigner.CreateAutoFormats(AutoFormatSchemes.WEBPARTZONE_SCHEME_NAMES, (string schemeName) => new WebPartZoneAutoFormat(schemeName, "<Schemes>\r\n<xsd:schema id=\"Schemes\" xmlns=\"\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns:msdata=\"urn:schemas-microsoft-com:xml-msdata\">\r\n  <xsd:element name=\"Scheme\">\r\n     <xsd:complexType>\r\n       <xsd:all>\r\n        <xsd:element name=\"SchemeName\" type=\"xsd:string\"/>\r\n        <xsd:element name=\"BorderColor\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n        <xsd:element name=\"EmptyZoneTextStyle-Font-Size\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n        <xsd:element name=\"Font-Names\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n        <xsd:element name=\"HeaderStyle-Font-Size\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n        <xsd:element name=\"HeaderStyle-ForeColor\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n        <xsd:element name=\"HeaderStyle-HorizontalAlign\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n        <xsd:element name=\"MenuPopupStyle-BackColor\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n        <xsd:element name=\"MenuPopupStyle-BorderColor\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n        <xsd:element name=\"MenuPopupStyle-BorderWidth\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n        <xsd:element name=\"MenuPopupStyle-Font-Names\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n        <xsd:element name=\"MenuPopupStyle-Font-Size\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n        <xsd:element name=\"MenuLabelStyle-ForeColor\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n        <xsd:element name=\"MenuLabelHoverStyle-ForeColor\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n        <xsd:element name=\"MenuVerbStyle-BorderColor\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n        <xsd:element name=\"MenuVerbStyle-BorderStyle\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n        <xsd:element name=\"MenuVerbStyle-BorderWidth\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n        <xsd:element name=\"MenuVerbStyle-ForeColor\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n        <xsd:element name=\"MenuVerbHoverStyle-BackColor\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n        <xsd:element name=\"MenuVerbHoverStyle-BorderColor\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n        <xsd:element name=\"MenuVerbHoverStyle-BorderStyle\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n        <xsd:element name=\"MenuVerbHoverStyle-BorderWidth\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n        <xsd:element name=\"MenuVerbHoverStyle-ForeColor\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n        <xsd:element name=\"Padding\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n        <xsd:element name=\"PartChromeStyle-BackColor\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n        <xsd:element name=\"PartChromeStyle-BorderColor\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n        <xsd:element name=\"PartChromeStyle-Font-Names\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n        <xsd:element name=\"PartChromeStyle-ForeColor\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n        <xsd:element name=\"PartStyle-Font-Size\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n        <xsd:element name=\"PartStyle-ForeColor\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n        <xsd:element name=\"PartTitleStyle-BackColor\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n        <xsd:element name=\"PartTitleStyle-Font-Bold\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n        <xsd:element name=\"PartTitleStyle-Font-Size\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n        <xsd:element name=\"PartTitleStyle-Font--ClearDefaults\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n        <xsd:element name=\"PartTitleStyle-ForeColor\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n        <xsd:element name=\"TitleBarVerbStyle-Font-Size\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n        <xsd:element name=\"TitleBarVerbStyle-Font-Underline\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n        <xsd:element name=\"TitleBarVerbStyle-Font--ClearDefaults\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n        <xsd:element name=\"TitleBarVerbStyle-ForeColor\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n      </xsd:all>\r\n    </xsd:complexType>\r\n  </xsd:element>\r\n  <xsd:element name=\"Schemes\" msdata:IsDataSet=\"true\">\r\n    <xsd:complexType>\r\n      <xsd:choice maxOccurs=\"unbounded\">\r\n        <xsd:element ref=\"Scheme\"/>\r\n      </xsd:choice>\r\n    </xsd:complexType>\r\n  </xsd:element>\r\n</xsd:schema>\r\n<Scheme>\r\n  <SchemeName>WebPartScheme_Empty</SchemeName>\r\n  <BorderColor>Gray</BorderColor>\r\n  <MenuVerbStyle-BorderStyle>NotSet</MenuVerbStyle-BorderStyle>\r\n  <MenuVerbHoverStyle-BorderStyle>NotSet</MenuVerbHoverStyle-BorderStyle>\r\n  <Padding>2</Padding>\r\n  <PartTitleStyle-Font-Bold>False</PartTitleStyle-Font-Bold>\r\n  <PartTitleStyle-Font--ClearDefaults>True</PartTitleStyle-Font--ClearDefaults>\r\n  <TitleBarVerbStyle-Font-Underline>False</TitleBarVerbStyle-Font-Underline>\r\n  <TitleBarVerbStyle-Font--ClearDefaults>True</TitleBarVerbStyle-Font--ClearDefaults>\r\n</Scheme>\r\n<Scheme>\r\n  <SchemeName>WebPartScheme_Professional</SchemeName>\r\n  <BorderColor>#CCCCCC</BorderColor>\r\n  <EmptyZoneTextStyle-Font-Size>0.8em</EmptyZoneTextStyle-Font-Size>\r\n  <Font-Names>Verdana</Font-Names>\r\n  <HeaderStyle-Font-Size>0.7em</HeaderStyle-Font-Size>\r\n  <HeaderStyle-ForeColor>#CCCCCC</HeaderStyle-ForeColor>\r\n  <HeaderStyle-HorizontalAlign>Center</HeaderStyle-HorizontalAlign>\r\n  <MenuPopupStyle-BackColor>#5D7B9D</MenuPopupStyle-BackColor>\r\n  <MenuPopupStyle-BorderColor>#CCCCCC</MenuPopupStyle-BorderColor>\r\n  <MenuPopupStyle-BorderWidth>1px</MenuPopupStyle-BorderWidth>\r\n  <MenuPopupStyle-Font-Names>Verdana</MenuPopupStyle-Font-Names>\r\n  <MenuPopupStyle-Font-Size>0.6em</MenuPopupStyle-Font-Size>\r\n  <MenuLabelStyle-ForeColor>#FFFFFF</MenuLabelStyle-ForeColor>\r\n  <MenuLabelHoverStyle-ForeColor>#E2DED6</MenuLabelHoverStyle-ForeColor>\r\n  <MenuVerbStyle-BorderColor>#5D7B9D</MenuVerbStyle-BorderColor>\r\n  <MenuVerbStyle-BorderStyle>Solid</MenuVerbStyle-BorderStyle>\r\n  <MenuVerbStyle-BorderWidth>1px</MenuVerbStyle-BorderWidth>\r\n  <MenuVerbStyle-ForeColor>#FFFFFF</MenuVerbStyle-ForeColor>\r\n  <MenuVerbHoverStyle-BackColor>#F7F6F3</MenuVerbHoverStyle-BackColor>\r\n  <MenuVerbHoverStyle-BorderColor>#CCCCCC</MenuVerbHoverStyle-BorderColor>\r\n  <MenuVerbHoverStyle-BorderStyle>Solid</MenuVerbHoverStyle-BorderStyle>\r\n  <MenuVerbHoverStyle-BorderWidth>1px</MenuVerbHoverStyle-BorderWidth>\r\n  <MenuVerbHoverStyle-ForeColor>#333333</MenuVerbHoverStyle-ForeColor>\r\n  <Padding>6</Padding>\r\n  <PartChromeStyle-BackColor>#F7F6F3</PartChromeStyle-BackColor>\r\n  <PartChromeStyle-BorderColor>#E2DED6</PartChromeStyle-BorderColor>\r\n  <PartChromeStyle-Font-Names>Verdana</PartChromeStyle-Font-Names>\r\n  <PartChromeStyle-ForeColor>#FFFFFF</PartChromeStyle-ForeColor>\r\n  <PartStyle-Font-Size>0.8em</PartStyle-Font-Size>\r\n  <PartStyle-ForeColor>#333333</PartStyle-ForeColor>\r\n  <PartTitleStyle-BackColor>#5D7B9D</PartTitleStyle-BackColor>\r\n  <PartTitleStyle-Font-Bold>True</PartTitleStyle-Font-Bold>\r\n  <PartTitleStyle-Font-Size>0.8em</PartTitleStyle-Font-Size>\r\n  <PartTitleStyle-ForeColor>#FFFFFF</PartTitleStyle-ForeColor>\r\n  <TitleBarVerbStyle-Font-Size>0.6em</TitleBarVerbStyle-Font-Size>\r\n  <TitleBarVerbStyle-Font-Underline>False</TitleBarVerbStyle-Font-Underline>\r\n  <TitleBarVerbStyle-ForeColor>#FFFFFF</TitleBarVerbStyle-ForeColor>\r\n</Scheme>\r\n<Scheme>\r\n  <SchemeName>WebPartScheme_Simple</SchemeName>\r\n  <BorderColor>#CCCCCC</BorderColor>\r\n  <EmptyZoneTextStyle-Font-Size>0.8em</EmptyZoneTextStyle-Font-Size>\r\n  <Font-Names>Verdana</Font-Names>\r\n  <HeaderStyle-Font-Size>0.7em</HeaderStyle-Font-Size>\r\n  <HeaderStyle-ForeColor>#CCCCCC</HeaderStyle-ForeColor>\r\n  <HeaderStyle-HorizontalAlign>Center</HeaderStyle-HorizontalAlign>\r\n  <MenuPopupStyle-BackColor>#1C5E55</MenuPopupStyle-BackColor>\r\n  <MenuPopupStyle-BorderColor>#CCCCCC</MenuPopupStyle-BorderColor>\r\n  <MenuPopupStyle-BorderWidth>1px</MenuPopupStyle-BorderWidth>\r\n  <MenuPopupStyle-Font-Names>Verdana</MenuPopupStyle-Font-Names>\r\n  <MenuPopupStyle-Font-Size>0.6em</MenuPopupStyle-Font-Size>\r\n  <MenuLabelStyle-ForeColor>#333333</MenuLabelStyle-ForeColor>\r\n  <MenuLabelHoverStyle-ForeColor>Yellow</MenuLabelHoverStyle-ForeColor>\r\n  <MenuVerbStyle-BorderColor>#1C5E55</MenuVerbStyle-BorderColor>\r\n  <MenuVerbStyle-BorderStyle>Solid</MenuVerbStyle-BorderStyle>\r\n  <MenuVerbStyle-BorderWidth>1px</MenuVerbStyle-BorderWidth>\r\n  <MenuVerbStyle-ForeColor>#FFFFFF</MenuVerbStyle-ForeColor>\r\n  <MenuVerbHoverStyle-BackColor>#E3EAEB</MenuVerbHoverStyle-BackColor>\r\n  <MenuVerbHoverStyle-BorderColor>#CCCCCC</MenuVerbHoverStyle-BorderColor>\r\n  <MenuVerbHoverStyle-BorderStyle>Solid</MenuVerbHoverStyle-BorderStyle>\r\n  <MenuVerbHoverStyle-BorderWidth>1px</MenuVerbHoverStyle-BorderWidth>\r\n  <MenuVerbHoverStyle-ForeColor>#333333</MenuVerbHoverStyle-ForeColor>\r\n  <Padding>6</Padding>\r\n  <PartChromeStyle-BackColor>#E3EAEB</PartChromeStyle-BackColor>\r\n  <PartChromeStyle-BorderColor>#C5BBAF</PartChromeStyle-BorderColor>\r\n  <PartChromeStyle-Font-Names>Verdana</PartChromeStyle-Font-Names>\r\n  <PartChromeStyle-ForeColor>#333333</PartChromeStyle-ForeColor>\r\n  <PartStyle-Font-Size>0.8em</PartStyle-Font-Size>\r\n  <PartStyle-ForeColor>#333333</PartStyle-ForeColor>\r\n  <PartTitleStyle-BackColor>#1C5E55</PartTitleStyle-BackColor>\r\n  <PartTitleStyle-Font-Bold>True</PartTitleStyle-Font-Bold>\r\n  <PartTitleStyle-Font-Size>0.8em</PartTitleStyle-Font-Size>\r\n  <PartTitleStyle-ForeColor>#FFFFFF</PartTitleStyle-ForeColor>\r\n  <TitleBarVerbStyle-Font-Size>0.6em</TitleBarVerbStyle-Font-Size>\r\n  <TitleBarVerbStyle-Font-Underline>False</TitleBarVerbStyle-Font-Underline>\r\n  <TitleBarVerbStyle-ForeColor>#FFFFFF</TitleBarVerbStyle-ForeColor>\r\n</Scheme>\r\n<Scheme>\r\n  <SchemeName>WebPartScheme_Classic</SchemeName>\r\n  <BorderColor>#CCCCCC</BorderColor>\r\n  <EmptyZoneTextStyle-Font-Size>0.8em</EmptyZoneTextStyle-Font-Size>\r\n  <Font-Names>Verdana</Font-Names>\r\n  <HeaderStyle-Font-Size>0.7em</HeaderStyle-Font-Size>\r\n  <HeaderStyle-ForeColor>#CCCCCC</HeaderStyle-ForeColor>\r\n  <HeaderStyle-HorizontalAlign>Center</HeaderStyle-HorizontalAlign>\r\n  <MenuPopupStyle-BackColor>#507CD1</MenuPopupStyle-BackColor>\r\n  <MenuPopupStyle-BorderColor>#CCCCCC</MenuPopupStyle-BorderColor>\r\n  <MenuPopupStyle-BorderWidth>1px</MenuPopupStyle-BorderWidth>\r\n  <MenuPopupStyle-Font-Names>Verdana</MenuPopupStyle-Font-Names>\r\n  <MenuPopupStyle-Font-Size>0.6em</MenuPopupStyle-Font-Size>\r\n  <MenuLabelStyle-ForeColor>#FFFFFF</MenuLabelStyle-ForeColor>\r\n  <MenuLabelHoverStyle-ForeColor>#D1DDF1</MenuLabelHoverStyle-ForeColor>\r\n  <MenuVerbStyle-BorderColor>#507CD1</MenuVerbStyle-BorderColor>\r\n  <MenuVerbStyle-BorderStyle>Solid</MenuVerbStyle-BorderStyle>\r\n  <MenuVerbStyle-BorderWidth>1px</MenuVerbStyle-BorderWidth>\r\n  <MenuVerbStyle-ForeColor>#FFFFFF</MenuVerbStyle-ForeColor>\r\n  <MenuVerbHoverStyle-BackColor>#EFF3FB</MenuVerbHoverStyle-BackColor>\r\n  <MenuVerbHoverStyle-BorderColor>#CCCCCC</MenuVerbHoverStyle-BorderColor>\r\n  <MenuVerbHoverStyle-BorderStyle>Solid</MenuVerbHoverStyle-BorderStyle>\r\n  <MenuVerbHoverStyle-BorderWidth>1px</MenuVerbHoverStyle-BorderWidth>\r\n  <MenuVerbHoverStyle-ForeColor>#333333</MenuVerbHoverStyle-ForeColor>\r\n  <Padding>6</Padding>\r\n  <PartChromeStyle-BackColor>#EFF3FB</PartChromeStyle-BackColor>\r\n  <PartChromeStyle-BorderColor>#D1DDF1</PartChromeStyle-BorderColor>\r\n  <PartChromeStyle-Font-Names>Verdana</PartChromeStyle-Font-Names>\r\n  <PartChromeStyle-ForeColor>#333333</PartChromeStyle-ForeColor>\r\n  <PartStyle-Font-Size>0.8em</PartStyle-Font-Size>\r\n  <PartStyle-ForeColor>#333333</PartStyle-ForeColor>\r\n  <PartTitleStyle-BackColor>#507CD1</PartTitleStyle-BackColor>\r\n  <PartTitleStyle-Font-Bold>True</PartTitleStyle-Font-Bold>\r\n  <PartTitleStyle-Font-Size>0.8em</PartTitleStyle-Font-Size>\r\n  <PartTitleStyle-ForeColor>#FFFFFF</PartTitleStyle-ForeColor>\r\n  <TitleBarVerbStyle-Font-Size>0.6em</TitleBarVerbStyle-Font-Size>\r\n  <TitleBarVerbStyle-Font-Underline>False</TitleBarVerbStyle-Font-Underline>\r\n  <TitleBarVerbStyle-ForeColor>#FFFFFF</TitleBarVerbStyle-ForeColor>\r\n</Scheme>\r\n<Scheme>\r\n  <SchemeName>WebPartScheme_Colorful</SchemeName>\r\n  <BorderColor>#CCCCCC</BorderColor>\r\n  <EmptyZoneTextStyle-Font-Size>0.8em</EmptyZoneTextStyle-Font-Size>\r\n  <Font-Names>Verdana</Font-Names>\r\n  <HeaderStyle-Font-Size>0.7em</HeaderStyle-Font-Size>\r\n  <HeaderStyle-ForeColor>#CCCCCC</HeaderStyle-ForeColor>\r\n  <HeaderStyle-HorizontalAlign>Center</HeaderStyle-HorizontalAlign>\r\n  <MenuPopupStyle-BackColor>#990000</MenuPopupStyle-BackColor>\r\n  <MenuPopupStyle-BorderColor>#CCCCCC</MenuPopupStyle-BorderColor>\r\n  <MenuPopupStyle-BorderWidth>1px</MenuPopupStyle-BorderWidth>\r\n  <MenuPopupStyle-Font-Names>Verdana</MenuPopupStyle-Font-Names>\r\n  <MenuPopupStyle-Font-Size>0.6em</MenuPopupStyle-Font-Size>\r\n  <MenuLabelStyle-ForeColor>#FFFFFF</MenuLabelStyle-ForeColor>\r\n  <MenuLabelHoverStyle-ForeColor>#FFCC66</MenuLabelHoverStyle-ForeColor>\r\n  <MenuVerbStyle-BorderColor>#990000</MenuVerbStyle-BorderColor>\r\n  <MenuVerbStyle-BorderStyle>Solid</MenuVerbStyle-BorderStyle>\r\n  <MenuVerbStyle-BorderWidth>1px</MenuVerbStyle-BorderWidth>\r\n  <MenuVerbStyle-ForeColor>#FFFFFF</MenuVerbStyle-ForeColor>\r\n  <MenuVerbHoverStyle-BackColor>#FFFBD6</MenuVerbHoverStyle-BackColor>\r\n  <MenuVerbHoverStyle-BorderColor>#CCCCCC</MenuVerbHoverStyle-BorderColor>\r\n  <MenuVerbHoverStyle-BorderStyle>Solid</MenuVerbHoverStyle-BorderStyle>\r\n  <MenuVerbHoverStyle-BorderWidth>1px</MenuVerbHoverStyle-BorderWidth>\r\n  <MenuVerbHoverStyle-ForeColor>#333333</MenuVerbHoverStyle-ForeColor>\r\n  <Padding>6</Padding>\r\n  <PartChromeStyle-BackColor>#FFFBD6</PartChromeStyle-BackColor>\r\n  <PartChromeStyle-BorderColor>#FFCC66</PartChromeStyle-BorderColor>\r\n  <PartChromeStyle-Font-Names>Verdana</PartChromeStyle-Font-Names>\r\n  <PartChromeStyle-ForeColor>#333333</PartChromeStyle-ForeColor>\r\n  <PartStyle-Font-Size>0.8em</PartStyle-Font-Size>\r\n  <PartStyle-ForeColor>#333333</PartStyle-ForeColor>\r\n  <PartTitleStyle-BackColor>#990000</PartTitleStyle-BackColor>\r\n  <PartTitleStyle-Font-Bold>True</PartTitleStyle-Font-Bold>\r\n  <PartTitleStyle-Font-Size>0.8em</PartTitleStyle-Font-Size>\r\n  <PartTitleStyle-ForeColor>#FFFFFF</PartTitleStyle-ForeColor>\r\n  <TitleBarVerbStyle-Font-Size>0.6em</TitleBarVerbStyle-Font-Size>\r\n  <TitleBarVerbStyle-Font-Underline>False</TitleBarVerbStyle-Font-Underline>\r\n  <TitleBarVerbStyle-ForeColor>#FFFFFF</TitleBarVerbStyle-ForeColor>\r\n</Scheme>\r\n</Schemes>\r\n"));
				}
				return WebPartZoneDesigner._autoFormats;
			}
		}

		// Token: 0x170002A1 RID: 673
		// (get) Token: 0x06000BE7 RID: 3047 RVA: 0x0004B614 File Offset: 0x00049814
		public override TemplateGroupCollection TemplateGroups
		{
			get
			{
				TemplateGroupCollection templateGroups = base.TemplateGroups;
				if (this._templateGroup == null)
				{
					this._templateGroup = base.CreateZoneTemplateGroup();
				}
				templateGroups.Add(this._templateGroup);
				return templateGroups;
			}
		}

		// Token: 0x06000BE8 RID: 3048 RVA: 0x0001895C File Offset: 0x00016B5C
		public override string GetDesignTimeHtml()
		{
			return this.GetDesignTimeHtml(null);
		}

		// Token: 0x06000BE9 RID: 3049 RVA: 0x0004B64C File Offset: 0x0004984C
		public override string GetDesignTimeHtml(DesignerRegionCollection regions)
		{
			string result;
			try
			{
				WebPartZone webPartZone = (WebPartZone)base.ViewControl;
				bool flag = base.UseRegions(regions, this._zone.ZoneTemplate, webPartZone.ZoneTemplate);
				if (webPartZone.ZoneTemplate == null && !flag)
				{
					result = this.GetEmptyDesignTimeHtml();
				}
				else
				{
					((ICompositeControlDesignerAccessor)webPartZone).RecreateChildControls();
					if (flag)
					{
						webPartZone.Controls.Clear();
						regions.Add(new WebPartZoneDesigner.WebPartEditableDesignerRegion(webPartZone, base.TemplateDefinition)
						{
							IsSingleInstanceTemplate = true,
							Description = SR.GetString("ContainerControlDesigner_RegionWatermark")
						});
					}
					result = base.GetDesignTimeHtml();
				}
			}
			catch (Exception e)
			{
				result = this.GetErrorDesignTimeHtml(e);
			}
			return result;
		}

		// Token: 0x06000BEA RID: 3050 RVA: 0x0004B6FC File Offset: 0x000498FC
		public override string GetEditableDesignerRegionContent(EditableDesignerRegion region)
		{
			return ControlPersister.PersistTemplate(this._zone.ZoneTemplate, (IDesignerHost)base.Component.Site.GetService(typeof(IDesignerHost)));
		}

		// Token: 0x06000BEB RID: 3051 RVA: 0x0004B72D File Offset: 0x0004992D
		protected override string GetEmptyDesignTimeHtml()
		{
			return base.CreatePlaceHolderDesignTimeHtml(SR.GetString("WebPartZoneDesigner_Empty"));
		}

		// Token: 0x06000BEC RID: 3052 RVA: 0x0004B73F File Offset: 0x0004993F
		public override void Initialize(IComponent component)
		{
			ControlDesigner.VerifyInitializeArgument(component, typeof(WebPartZone));
			base.Initialize(component);
			this._zone = (WebPartZone)component;
		}

		// Token: 0x06000BED RID: 3053 RVA: 0x0004B764 File Offset: 0x00049964
		public override void SetEditableDesignerRegionContent(EditableDesignerRegion region, string content)
		{
			this._zone.ZoneTemplate = ControlParser.ParseTemplate((IDesignerHost)base.Component.Site.GetService(typeof(IDesignerHost)), content);
			base.IsDirtyInternal = true;
		}

		// Token: 0x04000715 RID: 1813
		private static DesignerAutoFormatCollection _autoFormats;

		// Token: 0x04000716 RID: 1814
		private WebPartZone _zone;

		// Token: 0x04000717 RID: 1815
		private TemplateGroup _templateGroup;

		// Token: 0x02000461 RID: 1121
		private sealed class WebPartEditableDesignerRegion : TemplatedEditableDesignerRegion
		{
			// Token: 0x06002990 RID: 10640 RVA: 0x000FABB7 File Offset: 0x000F8DB7
			public WebPartEditableDesignerRegion(WebPartZoneBase zone, TemplateDefinition templateDefinition) : base(templateDefinition)
			{
				this._zone = zone;
			}

			// Token: 0x06002991 RID: 10641 RVA: 0x000FABC8 File Offset: 0x000F8DC8
			public override ViewRendering GetChildViewRendering(Control control)
			{
				if (control == null)
				{
					throw new ArgumentNullException("control");
				}
				DesignerWebPartChrome designerWebPartChrome = new DesignerWebPartChrome(this._zone);
				return designerWebPartChrome.GetViewRendering(control);
			}

			// Token: 0x04001D51 RID: 7505
			private WebPartZoneBase _zone;
		}
	}
}
