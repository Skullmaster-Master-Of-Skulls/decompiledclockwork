using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Design;
using System.Security.Permissions;
using System.Web.UI.WebControls;

namespace System.Web.UI.Design.WebControls
{
	// Token: 0x02000104 RID: 260
	[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
	public class SiteMapPathDesigner : ControlDesigner
	{
		// Token: 0x17000223 RID: 547
		// (get) Token: 0x0600092F RID: 2351 RVA: 0x00034EB1 File Offset: 0x000330B1
		public override DesignerAutoFormatCollection AutoFormats
		{
			get
			{
				if (this._autoFormats == null)
				{
					this._autoFormats = ControlDesigner.CreateAutoFormats(AutoFormatSchemes.SITEMAPPATH_SCHEME_NAMES, (string schemeName) => new SiteMapPathAutoFormat(schemeName, "<Schemes>\r\n        <xsd:schema id=\"Schemes\" xmlns=\"\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns:msdata=\"urn:schemas-microsoft-com:xml-msdata\">\r\n          <xsd:element name=\"Scheme\">\r\n            <xsd:complexType>\r\n              <xsd:all>\r\n                <xsd:element name=\"SchemeName\" type=\"xsd:string\"/>\r\n                <xsd:element name=\"FontName\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n                <xsd:element name=\"FontSize\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n                <xsd:element name=\"PathSeparator\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n                <xsd:element name=\"NodeStyleFontBold\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n                <xsd:element name=\"NodeStyleForeColor\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n                <xsd:element name=\"RootNodeStyleFontBold\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n                <xsd:element name=\"RootNodeStyleForeColor\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n                <xsd:element name=\"CurrentNodeStyleForeColor\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n                <xsd:element name=\"PathSeparatorStyleForeColor\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n                <xsd:element name=\"PathSeparatorStyleFontBold\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n              </xsd:all>\r\n            </xsd:complexType>\r\n          </xsd:element>\r\n          <xsd:element name=\"Schemes\" msdata:IsDataSet=\"true\">\r\n            <xsd:complexType>\r\n              <xsd:choice maxOccurs=\"unbounded\">\r\n                <xsd:element ref=\"Scheme\"/>\r\n              </xsd:choice>\r\n            </xsd:complexType>\r\n          </xsd:element>\r\n        </xsd:schema>\r\n        <Scheme>\r\n          <SchemeName>SiteMapPathAFmt_Scheme_Default</SchemeName>\r\n        </Scheme>\r\n        <Scheme>\r\n          <SchemeName>SiteMapPathAFmt_Scheme_Colorful</SchemeName>\r\n          <FontName>Verdana</FontName>\r\n          <FontSize>0.8em</FontSize>\r\n          <PathSeparator> : </PathSeparator>\r\n          <NodeStyleFontBold>True</NodeStyleFontBold>\r\n          <NodeStyleForeColor>#990000</NodeStyleForeColor>\r\n          <RootNodeStyleFontBold>True</RootNodeStyleFontBold>\r\n          <RootNodeStyleForeColor>#FF8000</RootNodeStyleForeColor>\r\n          <CurrentNodeStyleForeColor>#333333</CurrentNodeStyleForeColor>\r\n          <PathSeparatorStyleFontBold>True</PathSeparatorStyleFontBold>\r\n          <PathSeparatorStyleForeColor>#990000</PathSeparatorStyleForeColor>\r\n        </Scheme>\r\n        <Scheme>\r\n          <SchemeName>SiteMapPathAFmt_Scheme_Simple</SchemeName>\r\n          <FontName>Verdana</FontName>\r\n          <FontSize>0.8em</FontSize>\r\n          <PathSeparator> : </PathSeparator>\r\n          <NodeStyleFontBold>True</NodeStyleFontBold>\r\n          <NodeStyleForeColor>#666666</NodeStyleForeColor>\r\n          <RootNodeStyleFontBold>True</RootNodeStyleFontBold>\r\n          <RootNodeStyleForeColor>#1C5E55</RootNodeStyleForeColor>\r\n          <CurrentNodeStyleForeColor>#333333</CurrentNodeStyleForeColor>\r\n          <PathSeparatorStyleFontBold>True</PathSeparatorStyleFontBold>\r\n          <PathSeparatorStyleForeColor>#1C5E55</PathSeparatorStyleForeColor>\r\n        </Scheme>\r\n        <Scheme>\r\n          <SchemeName>SiteMapPathAFmt_Scheme_Professional</SchemeName>\r\n          <FontName>Verdana</FontName>\r\n          <FontSize>0.8em</FontSize>\r\n          <PathSeparator> : </PathSeparator>\r\n          <NodeStyleFontBold>True</NodeStyleFontBold>\r\n          <NodeStyleForeColor>#7C6F57</NodeStyleForeColor>\r\n          <RootNodeStyleFontBold>True</RootNodeStyleFontBold>\r\n          <RootNodeStyleForeColor>#5D7B9D</RootNodeStyleForeColor>\r\n          <CurrentNodeStyleForeColor>#333333</CurrentNodeStyleForeColor>\r\n          <PathSeparatorStyleFontBold>True</PathSeparatorStyleFontBold>\r\n          <PathSeparatorStyleForeColor>#5D7B9D</PathSeparatorStyleForeColor>\r\n        </Scheme>\r\n        <Scheme>\r\n          <SchemeName>SiteMapPathAFmt_Scheme_Classic</SchemeName>\r\n          <FontName>Verdana</FontName>\r\n          <FontSize>0.8em</FontSize>\r\n          <PathSeparator> : </PathSeparator>\r\n          <NodeStyleFontBold>True</NodeStyleFontBold>\r\n          <NodeStyleForeColor>#284E98</NodeStyleForeColor>\r\n          <RootNodeStyleFontBold>True</RootNodeStyleFontBold>\r\n          <RootNodeStyleForeColor>#507CD1</RootNodeStyleForeColor>\r\n          <CurrentNodeStyleForeColor>#333333</CurrentNodeStyleForeColor>\r\n          <PathSeparatorStyleFontBold>True</PathSeparatorStyleFontBold>\r\n          <PathSeparatorStyleForeColor>#507CD1</PathSeparatorStyleForeColor>\r\n        </Scheme>\r\n      </Schemes>"));
				}
				return this._autoFormats;
			}
		}

		// Token: 0x17000224 RID: 548
		// (get) Token: 0x06000930 RID: 2352 RVA: 0x00034EF0 File Offset: 0x000330F0
		private SiteMapProvider DesignTimeSiteMapProvider
		{
			get
			{
				if (this._siteMapProvider == null)
				{
					IDesignerHost host = (IDesignerHost)this.GetService(typeof(IDesignerHost));
					this._siteMapProvider = new DesignTimeSiteMapProvider(host);
				}
				return this._siteMapProvider;
			}
		}

		// Token: 0x17000225 RID: 549
		// (get) Token: 0x06000931 RID: 2353 RVA: 0x00034F30 File Offset: 0x00033130
		public override TemplateGroupCollection TemplateGroups
		{
			get
			{
				TemplateGroupCollection templateGroups = base.TemplateGroups;
				for (int i = 0; i < SiteMapPathDesigner._controlTemplateNames.Length; i++)
				{
					string text = SiteMapPathDesigner._controlTemplateNames[i];
					TemplateGroup templateGroup = new TemplateGroup(text);
					templateGroup.AddTemplateDefinition(new TemplateDefinition(this, text, base.Component, text, this.TemplateStyleArray[i]));
					templateGroups.Add(templateGroup);
				}
				return templateGroups;
			}
		}

		// Token: 0x17000226 RID: 550
		// (get) Token: 0x06000932 RID: 2354 RVA: 0x00034F8C File Offset: 0x0003318C
		private Style[] TemplateStyleArray
		{
			get
			{
				if (SiteMapPathDesigner._templateStyleArray == null)
				{
					SiteMapPathDesigner._templateStyleArray = new Style[]
					{
						((SiteMapPath)base.ViewControl).NodeStyle,
						((SiteMapPath)base.ViewControl).CurrentNodeStyle,
						((SiteMapPath)base.ViewControl).RootNodeStyle,
						((SiteMapPath)base.ViewControl).PathSeparatorStyle
					};
				}
				return SiteMapPathDesigner._templateStyleArray;
			}
		}

		// Token: 0x17000227 RID: 551
		// (get) Token: 0x06000933 RID: 2355 RVA: 0x00003B0F File Offset: 0x00001D0F
		protected override bool UsePreviewControl
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06000934 RID: 2356 RVA: 0x00034FFC File Offset: 0x000331FC
		public override string GetDesignTimeHtml()
		{
			string result = null;
			SiteMapPath siteMapPath = (SiteMapPath)base.ViewControl;
			try
			{
				siteMapPath.Provider = this.DesignTimeSiteMapProvider;
				ICompositeControlDesignerAccessor compositeControlDesignerAccessor = siteMapPath;
				compositeControlDesignerAccessor.RecreateChildControls();
				result = base.GetDesignTimeHtml();
			}
			catch (Exception e)
			{
				result = this.GetErrorDesignTimeHtml(e);
			}
			return result;
		}

		// Token: 0x06000935 RID: 2357 RVA: 0x00035050 File Offset: 0x00033250
		protected override string GetErrorDesignTimeHtml(Exception e)
		{
			return base.CreatePlaceHolderDesignTimeHtml(SR.GetString("Control_ErrorRendering") + e.Message);
		}

		// Token: 0x06000936 RID: 2358 RVA: 0x0003506D File Offset: 0x0003326D
		public override void Initialize(IComponent component)
		{
			ControlDesigner.VerifyInitializeArgument(component, typeof(SiteMapPath));
			base.Initialize(component);
			this._navigationPath = (SiteMapPath)component;
			if (base.View != null)
			{
				base.View.SetFlags(ViewFlags.TemplateEditing, true);
			}
		}

		// Token: 0x0400055D RID: 1373
		private SiteMapPath _navigationPath;

		// Token: 0x0400055E RID: 1374
		private SiteMapProvider _siteMapProvider;

		// Token: 0x0400055F RID: 1375
		private DesignerAutoFormatCollection _autoFormats;

		// Token: 0x04000560 RID: 1376
		private static string[] _controlTemplateNames = new string[]
		{
			"NodeTemplate",
			"CurrentNodeTemplate",
			"RootNodeTemplate",
			"PathSeparatorTemplate"
		};

		// Token: 0x04000561 RID: 1377
		private static Style[] _templateStyleArray;
	}
}
