using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Data;
using System.Design;
using System.Runtime.CompilerServices;
using System.Security.Permissions;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace System.Web.UI.Design.WebControls.WebParts
{
	// Token: 0x02000549 RID: 1353
	[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
	public class WebPartZoneDesigner : WebPartZoneBaseDesigner
	{
		// Token: 0x170008F3 RID: 2291
		// (get) Token: 0x06002F80 RID: 12160 RVA: 0x0010E83A File Offset: 0x0010D83A
		public override DesignerAutoFormatCollection AutoFormats
		{
			get
			{
				if (WebPartZoneDesigner._autoFormats == null)
				{
					WebPartZoneDesigner._autoFormats = ControlDesigner.CreateAutoFormats("<Schemes>\r\n<xsd:schema id=\"Schemes\" xmlns=\"\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns:msdata=\"urn:schemas-microsoft-com:xml-msdata\">\r\n  <xsd:element name=\"Scheme\">\r\n     <xsd:complexType>\r\n       <xsd:all>\r\n        <xsd:element name=\"SchemeName\" type=\"xsd:string\"/>\r\n        <xsd:element name=\"BorderColor\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n        <xsd:element name=\"EmptyZoneTextStyle-Font-Size\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n        <xsd:element name=\"Font-Names\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n        <xsd:element name=\"HeaderStyle-Font-Size\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n        <xsd:element name=\"HeaderStyle-ForeColor\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n        <xsd:element name=\"HeaderStyle-HorizontalAlign\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n        <xsd:element name=\"MenuPopupStyle-BackColor\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n        <xsd:element name=\"MenuPopupStyle-BorderColor\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n        <xsd:element name=\"MenuPopupStyle-BorderWidth\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n        <xsd:element name=\"MenuPopupStyle-Font-Names\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n        <xsd:element name=\"MenuPopupStyle-Font-Size\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n        <xsd:element name=\"MenuLabelStyle-ForeColor\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n        <xsd:element name=\"MenuLabelHoverStyle-ForeColor\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n        <xsd:element name=\"MenuVerbStyle-BorderColor\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n        <xsd:element name=\"MenuVerbStyle-BorderStyle\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n        <xsd:element name=\"MenuVerbStyle-BorderWidth\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n        <xsd:element name=\"MenuVerbStyle-ForeColor\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n        <xsd:element name=\"MenuVerbHoverStyle-BackColor\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n        <xsd:element name=\"MenuVerbHoverStyle-BorderColor\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n        <xsd:element name=\"MenuVerbHoverStyle-BorderStyle\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n        <xsd:element name=\"MenuVerbHoverStyle-BorderWidth\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n        <xsd:element name=\"MenuVerbHoverStyle-ForeColor\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n        <xsd:element name=\"Padding\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n        <xsd:element name=\"PartChromeStyle-BackColor\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n        <xsd:element name=\"PartChromeStyle-BorderColor\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n        <xsd:element name=\"PartChromeStyle-Font-Names\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n        <xsd:element name=\"PartChromeStyle-ForeColor\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n        <xsd:element name=\"PartStyle-Font-Size\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n        <xsd:element name=\"PartStyle-ForeColor\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n        <xsd:element name=\"PartTitleStyle-BackColor\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n        <xsd:element name=\"PartTitleStyle-Font-Bold\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n        <xsd:element name=\"PartTitleStyle-Font-Size\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n        <xsd:element name=\"PartTitleStyle-Font--ClearDefaults\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n        <xsd:element name=\"PartTitleStyle-ForeColor\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n        <xsd:element name=\"TitleBarVerbStyle-Font-Size\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n        <xsd:element name=\"TitleBarVerbStyle-Font-Underline\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n        <xsd:element name=\"TitleBarVerbStyle-Font--ClearDefaults\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n        <xsd:element name=\"TitleBarVerbStyle-ForeColor\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n      </xsd:all>\r\n    </xsd:complexType>\r\n  </xsd:element>\r\n  <xsd:element name=\"Schemes\" msdata:IsDataSet=\"true\">\r\n    <xsd:complexType>\r\n      <xsd:choice maxOccurs=\"unbounded\">\r\n        <xsd:element ref=\"Scheme\"/>\r\n      </xsd:choice>\r\n    </xsd:complexType>\r\n  </xsd:element>\r\n</xsd:schema>\r\n<Scheme>\r\n  <SchemeName>WebPartScheme_Empty</SchemeName>\r\n  <BorderColor>Gray</BorderColor>\r\n  <MenuVerbStyle-BorderStyle>NotSet</MenuVerbStyle-BorderStyle>\r\n  <MenuVerbHoverStyle-BorderStyle>NotSet</MenuVerbHoverStyle-BorderStyle>\r\n  <Padding>2</Padding>\r\n  <PartTitleStyle-Font-Bold>False</PartTitleStyle-Font-Bold>\r\n  <PartTitleStyle-Font--ClearDefaults>True</PartTitleStyle-Font--ClearDefaults>\r\n  <TitleBarVerbStyle-Font-Underline>False</TitleBarVerbStyle-Font-Underline>\r\n  <TitleBarVerbStyle-Font--ClearDefaults>True</TitleBarVerbStyle-Font--ClearDefaults>\r\n</Scheme>\r\n<Scheme>\r\n  <SchemeName>WebPartScheme_Professional</SchemeName>\r\n  <BorderColor>#CCCCCC</BorderColor>\r\n  <EmptyZoneTextStyle-Font-Size>0.8em</EmptyZoneTextStyle-Font-Size>\r\n  <Font-Names>Verdana</Font-Names>\r\n  <HeaderStyle-Font-Size>0.7em</HeaderStyle-Font-Size>\r\n  <HeaderStyle-ForeColor>#CCCCCC</HeaderStyle-ForeColor>\r\n  <HeaderStyle-HorizontalAlign>Center</HeaderStyle-HorizontalAlign>\r\n  <MenuPopupStyle-BackColor>#5D7B9D</MenuPopupStyle-BackColor>\r\n  <MenuPopupStyle-BorderColor>#CCCCCC</MenuPopupStyle-BorderColor>\r\n  <MenuPopupStyle-BorderWidth>1px</MenuPopupStyle-BorderWidth>\r\n  <MenuPopupStyle-Font-Names>Verdana</MenuPopupStyle-Font-Names>\r\n  <MenuPopupStyle-Font-Size>0.6em</MenuPopupStyle-Font-Size>\r\n  <MenuLabelStyle-ForeColor>#FFFFFF</MenuLabelStyle-ForeColor>\r\n  <MenuLabelHoverStyle-ForeColor>#E2DED6</MenuLabelHoverStyle-ForeColor>\r\n  <MenuVerbStyle-BorderColor>#5D7B9D</MenuVerbStyle-BorderColor>\r\n  <MenuVerbStyle-BorderStyle>Solid</MenuVerbStyle-BorderStyle>\r\n  <MenuVerbStyle-BorderWidth>1px</MenuVerbStyle-BorderWidth>\r\n  <MenuVerbStyle-ForeColor>#FFFFFF</MenuVerbStyle-ForeColor>\r\n  <MenuVerbHoverStyle-BackColor>#F7F6F3</MenuVerbHoverStyle-BackColor>\r\n  <MenuVerbHoverStyle-BorderColor>#CCCCCC</MenuVerbHoverStyle-BorderColor>\r\n  <MenuVerbHoverStyle-BorderStyle>Solid</MenuVerbHoverStyle-BorderStyle>\r\n  <MenuVerbHoverStyle-BorderWidth>1px</MenuVerbHoverStyle-BorderWidth>\r\n  <MenuVerbHoverStyle-ForeColor>#333333</MenuVerbHoverStyle-ForeColor>\r\n  <Padding>6</Padding>\r\n  <PartChromeStyle-BackColor>#F7F6F3</PartChromeStyle-BackColor>\r\n  <PartChromeStyle-BorderColor>#E2DED6</PartChromeStyle-BorderColor>\r\n  <PartChromeStyle-Font-Names>Verdana</PartChromeStyle-Font-Names>\r\n  <PartChromeStyle-ForeColor>#FFFFFF</PartChromeStyle-ForeColor>\r\n  <PartStyle-Font-Size>0.8em</PartStyle-Font-Size>\r\n  <PartStyle-ForeColor>#333333</PartStyle-ForeColor>\r\n  <PartTitleStyle-BackColor>#5D7B9D</PartTitleStyle-BackColor>\r\n  <PartTitleStyle-Font-Bold>True</PartTitleStyle-Font-Bold>\r\n  <PartTitleStyle-Font-Size>0.8em</PartTitleStyle-Font-Size>\r\n  <PartTitleStyle-ForeColor>#FFFFFF</PartTitleStyle-ForeColor>\r\n  <TitleBarVerbStyle-Font-Size>0.6em</TitleBarVerbStyle-Font-Size>\r\n  <TitleBarVerbStyle-Font-Underline>False</TitleBarVerbStyle-Font-Underline>\r\n  <TitleBarVerbStyle-ForeColor>#FFFFFF</TitleBarVerbStyle-ForeColor>\r\n</Scheme>\r\n<Scheme>\r\n  <SchemeName>WebPartScheme_Simple</SchemeName>\r\n  <BorderColor>#CCCCCC</BorderColor>\r\n  <EmptyZoneTextStyle-Font-Size>0.8em</EmptyZoneTextStyle-Font-Size>\r\n  <Font-Names>Verdana</Font-Names>\r\n  <HeaderStyle-Font-Size>0.7em</HeaderStyle-Font-Size>\r\n  <HeaderStyle-ForeColor>#CCCCCC</HeaderStyle-ForeColor>\r\n  <HeaderStyle-HorizontalAlign>Center</HeaderStyle-HorizontalAlign>\r\n  <MenuPopupStyle-BackColor>#1C5E55</MenuPopupStyle-BackColor>\r\n  <MenuPopupStyle-BorderColor>#CCCCCC</MenuPopupStyle-BorderColor>\r\n  <MenuPopupStyle-BorderWidth>1px</MenuPopupStyle-BorderWidth>\r\n  <MenuPopupStyle-Font-Names>Verdana</MenuPopupStyle-Font-Names>\r\n  <MenuPopupStyle-Font-Size>0.6em</MenuPopupStyle-Font-Size>\r\n  <MenuLabelStyle-ForeColor>#333333</MenuLabelStyle-ForeColor>\r\n  <MenuLabelHoverStyle-ForeColor>Yellow</MenuLabelHoverStyle-ForeColor>\r\n  <MenuVerbStyle-BorderColor>#1C5E55</MenuVerbStyle-BorderColor>\r\n  <MenuVerbStyle-BorderStyle>Solid</MenuVerbStyle-BorderStyle>\r\n  <MenuVerbStyle-BorderWidth>1px</MenuVerbStyle-BorderWidth>\r\n  <MenuVerbStyle-ForeColor>#FFFFFF</MenuVerbStyle-ForeColor>\r\n  <MenuVerbHoverStyle-BackColor>#E3EAEB</MenuVerbHoverStyle-BackColor>\r\n  <MenuVerbHoverStyle-BorderColor>#CCCCCC</MenuVerbHoverStyle-BorderColor>\r\n  <MenuVerbHoverStyle-BorderStyle>Solid</MenuVerbHoverStyle-BorderStyle>\r\n  <MenuVerbHoverStyle-BorderWidth>1px</MenuVerbHoverStyle-BorderWidth>\r\n  <MenuVerbHoverStyle-ForeColor>#333333</MenuVerbHoverStyle-ForeColor>\r\n  <Padding>6</Padding>\r\n  <PartChromeStyle-BackColor>#E3EAEB</PartChromeStyle-BackColor>\r\n  <PartChromeStyle-BorderColor>#C5BBAF</PartChromeStyle-BorderColor>\r\n  <PartChromeStyle-Font-Names>Verdana</PartChromeStyle-Font-Names>\r\n  <PartChromeStyle-ForeColor>#333333</PartChromeStyle-ForeColor>\r\n  <PartStyle-Font-Size>0.8em</PartStyle-Font-Size>\r\n  <PartStyle-ForeColor>#333333</PartStyle-ForeColor>\r\n  <PartTitleStyle-BackColor>#1C5E55</PartTitleStyle-BackColor>\r\n  <PartTitleStyle-Font-Bold>True</PartTitleStyle-Font-Bold>\r\n  <PartTitleStyle-Font-Size>0.8em</PartTitleStyle-Font-Size>\r\n  <PartTitleStyle-ForeColor>#FFFFFF</PartTitleStyle-ForeColor>\r\n  <TitleBarVerbStyle-Font-Size>0.6em</TitleBarVerbStyle-Font-Size>\r\n  <TitleBarVerbStyle-Font-Underline>False</TitleBarVerbStyle-Font-Underline>\r\n  <TitleBarVerbStyle-ForeColor>#FFFFFF</TitleBarVerbStyle-ForeColor>\r\n</Scheme>\r\n<Scheme>\r\n  <SchemeName>WebPartScheme_Classic</SchemeName>\r\n  <BorderColor>#CCCCCC</BorderColor>\r\n  <EmptyZoneTextStyle-Font-Size>0.8em</EmptyZoneTextStyle-Font-Size>\r\n  <Font-Names>Verdana</Font-Names>\r\n  <HeaderStyle-Font-Size>0.7em</HeaderStyle-Font-Size>\r\n  <HeaderStyle-ForeColor>#CCCCCC</HeaderStyle-ForeColor>\r\n  <HeaderStyle-HorizontalAlign>Center</HeaderStyle-HorizontalAlign>\r\n  <MenuPopupStyle-BackColor>#507CD1</MenuPopupStyle-BackColor>\r\n  <MenuPopupStyle-BorderColor>#CCCCCC</MenuPopupStyle-BorderColor>\r\n  <MenuPopupStyle-BorderWidth>1px</MenuPopupStyle-BorderWidth>\r\n  <MenuPopupStyle-Font-Names>Verdana</MenuPopupStyle-Font-Names>\r\n  <MenuPopupStyle-Font-Size>0.6em</MenuPopupStyle-Font-Size>\r\n  <MenuLabelStyle-ForeColor>#FFFFFF</MenuLabelStyle-ForeColor>\r\n  <MenuLabelHoverStyle-ForeColor>#D1DDF1</MenuLabelHoverStyle-ForeColor>\r\n  <MenuVerbStyle-BorderColor>#507CD1</MenuVerbStyle-BorderColor>\r\n  <MenuVerbStyle-BorderStyle>Solid</MenuVerbStyle-BorderStyle>\r\n  <MenuVerbStyle-BorderWidth>1px</MenuVerbStyle-BorderWidth>\r\n  <MenuVerbStyle-ForeColor>#FFFFFF</MenuVerbStyle-ForeColor>\r\n  <MenuVerbHoverStyle-BackColor>#EFF3FB</MenuVerbHoverStyle-BackColor>\r\n  <MenuVerbHoverStyle-BorderColor>#CCCCCC</MenuVerbHoverStyle-BorderColor>\r\n  <MenuVerbHoverStyle-BorderStyle>Solid</MenuVerbHoverStyle-BorderStyle>\r\n  <MenuVerbHoverStyle-BorderWidth>1px</MenuVerbHoverStyle-BorderWidth>\r\n  <MenuVerbHoverStyle-ForeColor>#333333</MenuVerbHoverStyle-ForeColor>\r\n  <Padding>6</Padding>\r\n  <PartChromeStyle-BackColor>#EFF3FB</PartChromeStyle-BackColor>\r\n  <PartChromeStyle-BorderColor>#D1DDF1</PartChromeStyle-BorderColor>\r\n  <PartChromeStyle-Font-Names>Verdana</PartChromeStyle-Font-Names>\r\n  <PartChromeStyle-ForeColor>#333333</PartChromeStyle-ForeColor>\r\n  <PartStyle-Font-Size>0.8em</PartStyle-Font-Size>\r\n  <PartStyle-ForeColor>#333333</PartStyle-ForeColor>\r\n  <PartTitleStyle-BackColor>#507CD1</PartTitleStyle-BackColor>\r\n  <PartTitleStyle-Font-Bold>True</PartTitleStyle-Font-Bold>\r\n  <PartTitleStyle-Font-Size>0.8em</PartTitleStyle-Font-Size>\r\n  <PartTitleStyle-ForeColor>#FFFFFF</PartTitleStyle-ForeColor>\r\n  <TitleBarVerbStyle-Font-Size>0.6em</TitleBarVerbStyle-Font-Size>\r\n  <TitleBarVerbStyle-Font-Underline>False</TitleBarVerbStyle-Font-Underline>\r\n  <TitleBarVerbStyle-ForeColor>#FFFFFF</TitleBarVerbStyle-ForeColor>\r\n</Scheme>\r\n<Scheme>\r\n  <SchemeName>WebPartScheme_Colorful</SchemeName>\r\n  <BorderColor>#CCCCCC</BorderColor>\r\n  <EmptyZoneTextStyle-Font-Size>0.8em</EmptyZoneTextStyle-Font-Size>\r\n  <Font-Names>Verdana</Font-Names>\r\n  <HeaderStyle-Font-Size>0.7em</HeaderStyle-Font-Size>\r\n  <HeaderStyle-ForeColor>#CCCCCC</HeaderStyle-ForeColor>\r\n  <HeaderStyle-HorizontalAlign>Center</HeaderStyle-HorizontalAlign>\r\n  <MenuPopupStyle-BackColor>#990000</MenuPopupStyle-BackColor>\r\n  <MenuPopupStyle-BorderColor>#CCCCCC</MenuPopupStyle-BorderColor>\r\n  <MenuPopupStyle-BorderWidth>1px</MenuPopupStyle-BorderWidth>\r\n  <MenuPopupStyle-Font-Names>Verdana</MenuPopupStyle-Font-Names>\r\n  <MenuPopupStyle-Font-Size>0.6em</MenuPopupStyle-Font-Size>\r\n  <MenuLabelStyle-ForeColor>#FFFFFF</MenuLabelStyle-ForeColor>\r\n  <MenuLabelHoverStyle-ForeColor>#FFCC66</MenuLabelHoverStyle-ForeColor>\r\n  <MenuVerbStyle-BorderColor>#990000</MenuVerbStyle-BorderColor>\r\n  <MenuVerbStyle-BorderStyle>Solid</MenuVerbStyle-BorderStyle>\r\n  <MenuVerbStyle-BorderWidth>1px</MenuVerbStyle-BorderWidth>\r\n  <MenuVerbStyle-ForeColor>#FFFFFF</MenuVerbStyle-ForeColor>\r\n  <MenuVerbHoverStyle-BackColor>#FFFBD6</MenuVerbHoverStyle-BackColor>\r\n  <MenuVerbHoverStyle-BorderColor>#CCCCCC</MenuVerbHoverStyle-BorderColor>\r\n  <MenuVerbHoverStyle-BorderStyle>Solid</MenuVerbHoverStyle-BorderStyle>\r\n  <MenuVerbHoverStyle-BorderWidth>1px</MenuVerbHoverStyle-BorderWidth>\r\n  <MenuVerbHoverStyle-ForeColor>#333333</MenuVerbHoverStyle-ForeColor>\r\n  <Padding>6</Padding>\r\n  <PartChromeStyle-BackColor>#FFFBD6</PartChromeStyle-BackColor>\r\n  <PartChromeStyle-BorderColor>#FFCC66</PartChromeStyle-BorderColor>\r\n  <PartChromeStyle-Font-Names>Verdana</PartChromeStyle-Font-Names>\r\n  <PartChromeStyle-ForeColor>#333333</PartChromeStyle-ForeColor>\r\n  <PartStyle-Font-Size>0.8em</PartStyle-Font-Size>\r\n  <PartStyle-ForeColor>#333333</PartStyle-ForeColor>\r\n  <PartTitleStyle-BackColor>#990000</PartTitleStyle-BackColor>\r\n  <PartTitleStyle-Font-Bold>True</PartTitleStyle-Font-Bold>\r\n  <PartTitleStyle-Font-Size>0.8em</PartTitleStyle-Font-Size>\r\n  <PartTitleStyle-ForeColor>#FFFFFF</PartTitleStyle-ForeColor>\r\n  <TitleBarVerbStyle-Font-Size>0.6em</TitleBarVerbStyle-Font-Size>\r\n  <TitleBarVerbStyle-Font-Underline>False</TitleBarVerbStyle-Font-Underline>\r\n  <TitleBarVerbStyle-ForeColor>#FFFFFF</TitleBarVerbStyle-ForeColor>\r\n</Scheme>\r\n</Schemes>\r\n", (DataRow schemeData) => new WebPartZoneAutoFormat(schemeData));
				}
				return WebPartZoneDesigner._autoFormats;
			}
		}

		// Token: 0x170008F4 RID: 2292
		// (get) Token: 0x06002F81 RID: 12161 RVA: 0x0010E874 File Offset: 0x0010D874
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

		// Token: 0x06002F82 RID: 12162 RVA: 0x0010E8AA File Offset: 0x0010D8AA
		public override string GetDesignTimeHtml()
		{
			return this.GetDesignTimeHtml(null);
		}

		// Token: 0x06002F83 RID: 12163 RVA: 0x0010E8B4 File Offset: 0x0010D8B4
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

		// Token: 0x06002F84 RID: 12164 RVA: 0x0010E964 File Offset: 0x0010D964
		public override string GetEditableDesignerRegionContent(EditableDesignerRegion region)
		{
			return ControlPersister.PersistTemplate(this._zone.ZoneTemplate, (IDesignerHost)base.Component.Site.GetService(typeof(IDesignerHost)));
		}

		// Token: 0x06002F85 RID: 12165 RVA: 0x0010E995 File Offset: 0x0010D995
		protected override string GetEmptyDesignTimeHtml()
		{
			return base.CreatePlaceHolderDesignTimeHtml(SR.GetString("WebPartZoneDesigner_Empty"));
		}

		// Token: 0x06002F86 RID: 12166 RVA: 0x0010E9A7 File Offset: 0x0010D9A7
		public override void Initialize(IComponent component)
		{
			ControlDesigner.VerifyInitializeArgument(component, typeof(WebPartZone));
			base.Initialize(component);
			this._zone = (WebPartZone)component;
		}

		// Token: 0x06002F87 RID: 12167 RVA: 0x0010E9CC File Offset: 0x0010D9CC
		public override void SetEditableDesignerRegionContent(EditableDesignerRegion region, string content)
		{
			this._zone.ZoneTemplate = ControlParser.ParseTemplate((IDesignerHost)base.Component.Site.GetService(typeof(IDesignerHost)), content);
			base.IsDirtyInternal = true;
		}

		// Token: 0x04002047 RID: 8263
		private static DesignerAutoFormatCollection _autoFormats;

		// Token: 0x04002048 RID: 8264
		private WebPartZone _zone;

		// Token: 0x04002049 RID: 8265
		private TemplateGroup _templateGroup;

		// Token: 0x0400204A RID: 8266
		[CompilerGenerated]
		private static ControlDesigner.CreateAutoFormatDelegate <>9__CachedAnonymousMethodDelegate1;

		// Token: 0x0200054A RID: 1354
		private sealed class WebPartEditableDesignerRegion : TemplatedEditableDesignerRegion
		{
			// Token: 0x06002F8A RID: 12170 RVA: 0x0010EA0D File Offset: 0x0010DA0D
			public WebPartEditableDesignerRegion(WebPartZoneBase zone, TemplateDefinition templateDefinition) : base(templateDefinition)
			{
				this._zone = zone;
			}

			// Token: 0x06002F8B RID: 12171 RVA: 0x0010EA20 File Offset: 0x0010DA20
			public override ViewRendering GetChildViewRendering(Control control)
			{
				if (control == null)
				{
					throw new ArgumentNullException("control");
				}
				DesignerWebPartChrome designerWebPartChrome = new DesignerWebPartChrome(this._zone);
				return designerWebPartChrome.GetViewRendering(control);
			}

			// Token: 0x0400204B RID: 8267
			private WebPartZoneBase _zone;
		}
	}
}
