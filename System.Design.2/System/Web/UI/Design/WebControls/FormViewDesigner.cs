using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Design;
using System.Globalization;
using System.Text;
using System.Web.UI.Design.Util;
using System.Web.UI.WebControls;
using System.Windows.Forms;

namespace System.Web.UI.Design.WebControls
{
	// Token: 0x020000CA RID: 202
	public class FormViewDesigner : DataBoundControlDesigner
	{
		// Token: 0x1700018D RID: 397
		// (get) Token: 0x060006A2 RID: 1698 RVA: 0x000241C8 File Offset: 0x000223C8
		public override DesignerActionListCollection ActionLists
		{
			get
			{
				DesignerActionListCollection designerActionListCollection = new DesignerActionListCollection();
				designerActionListCollection.AddRange(base.ActionLists);
				if (this._actionLists == null)
				{
					this._actionLists = new FormViewActionList(this);
				}
				bool inTemplateMode = base.InTemplateMode;
				DesignerDataSourceView designerView = base.DesignerView;
				this._actionLists.AllowPaging = (!inTemplateMode && designerView != null);
				this._actionLists.AllowDynamicData = this.CheckDynamicDataAllowed();
				designerActionListCollection.Add(this._actionLists);
				return designerActionListCollection;
			}
		}

		// Token: 0x060006A3 RID: 1699 RVA: 0x00024240 File Offset: 0x00022440
		private bool CheckDynamicDataAllowed()
		{
			IDesignerHost designerHost = (IDesignerHost)base.Component.Site.GetService(typeof(IDesignerHost));
			WebFormsRootDesigner webFormsRootDesigner = designerHost.GetDesigner(designerHost.RootComponent) as WebFormsRootDesigner;
			if (webFormsRootDesigner != null)
			{
				WebFormsReferenceManager referenceManager = webFormsRootDesigner.ReferenceManager;
				return referenceManager.GetType("asp", "DynamicControl") != null;
			}
			return false;
		}

		// Token: 0x1700018E RID: 398
		// (get) Token: 0x060006A4 RID: 1700 RVA: 0x000242A1 File Offset: 0x000224A1
		public override DesignerAutoFormatCollection AutoFormats
		{
			get
			{
				if (FormViewDesigner._autoFormats == null)
				{
					FormViewDesigner._autoFormats = ControlDesigner.CreateAutoFormats(AutoFormatSchemes.FORMVIEW_SCHEME_NAMES, (string schemeName) => new FormViewAutoFormat(schemeName, "<Schemes>\r\n        <xsd:schema id=\"Schemes\" xmlns=\"\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns:msdata=\"urn:schemas-microsoft-com:xml-msdata\">\r\n          <xsd:element name=\"Scheme\">\r\n            <xsd:complexType>\r\n              <xsd:all>\r\n                <xsd:element name=\"SchemeName\" type=\"xsd:string\"/>\r\n                <xsd:element name=\"ForeColor\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n                <xsd:element name=\"BackColor\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n                <xsd:element name=\"BorderColor\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n                <xsd:element name=\"BorderWidth\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n                <xsd:element name=\"BorderStyle\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n                <xsd:element name=\"GridLines\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n                <xsd:element name=\"CellPadding\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n                <xsd:element name=\"CellSpacing\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n                <xsd:element name=\"RowForeColor\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n                <xsd:element name=\"RowBackColor\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n                <xsd:element name=\"RowFont\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n                <xsd:element name=\"EditRowForeColor\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n                <xsd:element name=\"EditRowBackColor\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n                <xsd:element name=\"EditRowFont\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n                <xsd:element name=\"HeaderForeColor\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n                <xsd:element name=\"HeaderBackColor\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n                <xsd:element name=\"HeaderFont\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n                <xsd:element name=\"FooterForeColor\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n                <xsd:element name=\"FooterBackColor\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n                <xsd:element name=\"FooterFont\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n                <xsd:element name=\"PagerForeColor\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n                <xsd:element name=\"PagerBackColor\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n                <xsd:element name=\"PagerFont\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n                <xsd:element name=\"PagerAlign\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n                <xsd:element name=\"PagerButtons\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n                <xsd:element name=\"RenderOuterTable\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n              </xsd:all>\r\n            </xsd:complexType>\r\n          </xsd:element>\r\n          <xsd:element name=\"Schemes\" msdata:IsDataSet=\"true\">\r\n            <xsd:complexType>\r\n              <xsd:choice maxOccurs=\"unbounded\">\r\n                <xsd:element ref=\"Scheme\"/>\r\n              </xsd:choice>\r\n            </xsd:complexType>\r\n          </xsd:element>\r\n        </xsd:schema>\r\n        <Scheme>\r\n          <SchemeName>FVScheme_Empty</SchemeName>\r\n          <RenderOuterTable>True</RenderOuterTable>\r\n        </Scheme>\r\n        <Scheme>\r\n          <SchemeName>DVScheme_Consistent1</SchemeName>\r\n          <AltRowBackColor>White</AltRowBackColor>\r\n          <GridLines>0</GridLines>\r\n          <CellPadding>4</CellPadding>\r\n          <ForeColor>#333333</ForeColor>\r\n          <RowForeColor>#333333</RowForeColor>\r\n          <RowBackColor>#FFFBD6</RowBackColor>\r\n          <HeaderForeColor>White</HeaderForeColor>\r\n          <HeaderBackColor>#990000</HeaderBackColor>\r\n          <HeaderFont>1</HeaderFont>\r\n          <FooterForeColor>White</FooterForeColor>\r\n          <FooterBackColor>#990000</FooterBackColor>\r\n          <FooterFont>1</FooterFont>\r\n          <PagerForeColor>#333333</PagerForeColor>\r\n          <PagerBackColor>#FFCC66</PagerBackColor>\r\n          <PagerAlign>2</PagerAlign>\r\n          <RenderOuterTable>True</RenderOuterTable>\r\n        </Scheme>\r\n        <Scheme>\r\n          <SchemeName>DVScheme_Consistent2</SchemeName>\r\n            <AltRowBackColor>White</AltRowBackColor>\r\n            <GridLines>0</GridLines>\r\n            <CellPadding>4</CellPadding>\r\n            <ForeColor>#333333</ForeColor>\r\n            <RowBackColor>#EFF3FB</RowBackColor>\r\n            <HeaderForeColor>White</HeaderForeColor>\r\n            <HeaderBackColor>#507CD1</HeaderBackColor>\r\n            <HeaderFont>1</HeaderFont>\r\n            <FooterForeColor>White</FooterForeColor>\r\n            <FooterBackColor>#507CD1</FooterBackColor>\r\n            <FooterFont>1</FooterFont>\r\n            <PagerForeColor>White</PagerForeColor>\r\n            <PagerBackColor>#2461BF</PagerBackColor>\r\n            <PagerAlign>2</PagerAlign>\r\n            <EditRowBackColor>#2461BF</EditRowBackColor>\r\n            <RenderOuterTable>True</RenderOuterTable>\r\n        </Scheme>\r\n        <Scheme>\r\n          <SchemeName>DVScheme_Consistent3</SchemeName>\r\n            <AltRowBackColor>White</AltRowBackColor>\r\n            <GridLines>0</GridLines>\r\n            <CellPadding>4</CellPadding>\r\n            <ForeColor>#333333</ForeColor>\r\n            <RowBackColor>#E3EAEB</RowBackColor>\r\n            <HeaderForeColor>White</HeaderForeColor>\r\n            <HeaderBackColor>#1C5E55</HeaderBackColor>\r\n            <HeaderFont>1</HeaderFont>\r\n            <FooterForeColor>White</FooterForeColor>\r\n            <FooterBackColor>#1C5E55</FooterBackColor>\r\n            <FooterFont>1</FooterFont>\r\n            <PagerForeColor>White</PagerForeColor>\r\n            <PagerBackColor>#666666</PagerBackColor>\r\n            <PagerAlign>2</PagerAlign>\r\n            <EditRowBackColor>#7C6F57</EditRowBackColor>\r\n            <RenderOuterTable>True</RenderOuterTable>\r\n        </Scheme>\r\n        <Scheme>\r\n          <SchemeName>DVScheme_Consistent4</SchemeName>\r\n            <AltRowBackColor>White</AltRowBackColor>\r\n            <AltRowForeColor>#284775</AltRowForeColor>\r\n            <GridLines>0</GridLines>\r\n            <CellPadding>4</CellPadding>\r\n            <ForeColor>#333333</ForeColor>\r\n            <RowForeColor>#333333</RowForeColor>\r\n            <RowBackColor>#F7F6F3</RowBackColor>\r\n            <HeaderForeColor>White</HeaderForeColor>\r\n            <HeaderBackColor>#5D7B9D</HeaderBackColor>\r\n            <HeaderFont>1</HeaderFont>\r\n            <FooterForeColor>White</FooterForeColor>\r\n            <FooterBackColor>#5D7B9D</FooterBackColor>\r\n            <FooterFont>1</FooterFont>\r\n            <PagerForeColor>White</PagerForeColor>\r\n            <PagerBackColor>#284775</PagerBackColor>\r\n            <PagerAlign>2</PagerAlign>\r\n            <EditRowBackColor>#999999</EditRowBackColor>\r\n            <RenderOuterTable>True</RenderOuterTable>\r\n        </Scheme>\r\n        <Scheme>\r\n          <SchemeName>FVScheme_Colorful1</SchemeName>\r\n          <BackColor>White</BackColor>\r\n          <BorderColor>#CC9966</BorderColor>\r\n          <BorderWidth>1px</BorderWidth>\r\n          <BorderStyle>1</BorderStyle>\r\n          <GridLines>3</GridLines>\r\n          <CellPadding>4</CellPadding>\r\n          <CellSpacing>0</CellSpacing>\r\n          <RowForeColor>#330099</RowForeColor>\r\n          <RowBackColor>White</RowBackColor>\r\n          <EditRowForeColor>#663399</EditRowForeColor>\r\n          <EditRowBackColor>#FFCC66</EditRowBackColor>\r\n          <EditRowFont>1</EditRowFont>\r\n          <HeaderForeColor>#FFFFCC</HeaderForeColor>\r\n          <HeaderBackColor>#990000</HeaderBackColor>\r\n          <HeaderFont>1</HeaderFont>\r\n          <FooterForeColor>#330099</FooterForeColor>\r\n          <FooterBackColor>#FFFFCC</FooterBackColor>\r\n          <PagerForeColor>#330099</PagerForeColor>\r\n          <PagerBackColor>#FFFFCC</PagerBackColor>\r\n          <PagerAlign>2</PagerAlign>\r\n          <RenderOuterTable>True</RenderOuterTable>\r\n        </Scheme>\r\n        <Scheme>\r\n          <SchemeName>FVScheme_Colorful2</SchemeName>\r\n          <BackColor>White</BackColor>\r\n          <BorderColor>#3366CC</BorderColor>\r\n          <BorderWidth>1px</BorderWidth>\r\n          <BorderStyle>1</BorderStyle>\r\n          <GridLines>3</GridLines>\r\n          <CellPadding>4</CellPadding>\r\n          <CellSpacing>0</CellSpacing>\r\n          <RowForeColor>#003399</RowForeColor>\r\n          <RowBackColor>White</RowBackColor>\r\n          <EditRowForeColor>#CCFF99</EditRowForeColor>\r\n          <EditRowBackColor>#009999</EditRowBackColor>\r\n          <EditRowFont>1</EditRowFont>\r\n          <HeaderForeColor>#CCCCFF</HeaderForeColor>\r\n          <HeaderBackColor>#003399</HeaderBackColor>\r\n          <HeaderFont>1</HeaderFont>\r\n          <FooterForeColor>#003399</FooterForeColor>\r\n          <FooterBackColor>#99CCCC</FooterBackColor>\r\n          <PagerForeColor>#003399</PagerForeColor>\r\n          <PagerBackColor>#99CCCC</PagerBackColor>\r\n          <PagerAlign>1</PagerAlign>\r\n          <PagerButtons>1</PagerButtons>\r\n          <RenderOuterTable>True</RenderOuterTable>\r\n        </Scheme>\r\n        <Scheme>\r\n          <SchemeName>FVScheme_Colorful3</SchemeName>\r\n          <BackColor>#DEBA84</BackColor>\r\n          <BorderColor>#DEBA84</BorderColor>\r\n          <BorderWidth>1px</BorderWidth>\r\n          <BorderStyle>1</BorderStyle>\r\n          <GridLines>3</GridLines>\r\n          <CellPadding>3</CellPadding>\r\n          <CellSpacing>2</CellSpacing>\r\n          <RowForeColor>#8C4510</RowForeColor>\r\n          <RowBackColor>#FFF7E7</RowBackColor>\r\n          <EditRowForeColor>White</EditRowForeColor>\r\n          <EditRowBackColor>#738A9C</EditRowBackColor>\r\n          <EditRowFont>1</EditRowFont>\r\n          <HeaderForeColor>White</HeaderForeColor>\r\n          <HeaderBackColor>#A55129</HeaderBackColor>\r\n          <HeaderFont>1</HeaderFont>\r\n          <FooterForeColor>#8C4510</FooterForeColor>\r\n          <FooterBackColor>#F7DFB5</FooterBackColor>\r\n          <PagerForeColor>#8C4510</PagerForeColor>\r\n          <PagerAlign>2</PagerAlign>\r\n          <PagerButtons>1</PagerButtons>\r\n          <RenderOuterTable>True</RenderOuterTable>\r\n        </Scheme>\r\n        <Scheme>\r\n          <SchemeName>FVScheme_Colorful4</SchemeName>\r\n          <BackColor>White</BackColor>\r\n          <BorderColor>#E7E7FF</BorderColor>\r\n          <BorderWidth>1px</BorderWidth>\r\n          <BorderStyle>1</BorderStyle>\r\n          <GridLines>1</GridLines>\r\n          <CellPadding>3</CellPadding>\r\n          <CellSpacing>0</CellSpacing>\r\n          <RowForeColor>#4A3C8C</RowForeColor>\r\n          <RowBackColor>#E7E7FF</RowBackColor>\r\n          <EditRowForeColor>#F7F7F7</EditRowForeColor>\r\n          <EditRowBackColor>#738A9C</EditRowBackColor>\r\n          <EditRowFont>1</EditRowFont>\r\n          <HeaderForeColor>#F7F7F7</HeaderForeColor>\r\n          <HeaderBackColor>#4A3C8C</HeaderBackColor>\r\n          <HeaderFont>1</HeaderFont>\r\n          <FooterForeColor>#4A3C8C</FooterForeColor>\r\n          <FooterBackColor>#B5C7DE</FooterBackColor>\r\n          <PagerForeColor>#4A3C8C</PagerForeColor>\r\n          <PagerBackColor>#E7E7FF</PagerBackColor>\r\n          <PagerAlign>3</PagerAlign>\r\n          <PagerButtons>1</PagerButtons>\r\n          <RenderOuterTable>True</RenderOuterTable>\r\n        </Scheme>\r\n        <Scheme>\r\n          <SchemeName>FVScheme_Colorful5</SchemeName>\r\n          <ForeColor>Black</ForeColor>\r\n          <BackColor>LightGoldenRodYellow</BackColor>\r\n          <BorderColor>Tan</BorderColor>\r\n          <BorderWidth>1px</BorderWidth>\r\n          <GridLines>0</GridLines>\r\n          <CellPadding>2</CellPadding>\r\n          <HeaderBackColor>Tan</HeaderBackColor>\r\n          <HeaderFont>1</HeaderFont>\r\n          <FooterBackColor>Tan</FooterBackColor>\r\n          <EditRowBackColor>DarkSlateBlue</EditRowBackColor>\r\n          <EditRowForeColor>GhostWhite</EditRowForeColor>\r\n          <PagerBackColor>PaleGoldenrod</PagerBackColor>\r\n          <PagerForeColor>DarkSlateBlue</PagerForeColor>\r\n          <PagerAlign>2</PagerAlign>\r\n          <RenderOuterTable>True</RenderOuterTable>\r\n        </Scheme>\r\n        <Scheme>\r\n          <SchemeName>FVScheme_Professional1</SchemeName>\r\n          <BackColor>White</BackColor>\r\n          <BorderColor>#999999</BorderColor>\r\n          <BorderWidth>1px</BorderWidth>\r\n          <BorderStyle>1</BorderStyle>\r\n          <GridLines>2</GridLines>\r\n          <CellPadding>3</CellPadding>\r\n          <CellSpacing>0</CellSpacing>\r\n          <RowForeColor>Black</RowForeColor>\r\n          <RowBackColor>#EEEEEE</RowBackColor>\r\n          <EditRowForeColor>White</EditRowForeColor>\r\n          <EditRowBackColor>#008A8C</EditRowBackColor>\r\n          <EditRowFont>1</EditRowFont>\r\n          <HeaderForeColor>White</HeaderForeColor>\r\n          <HeaderBackColor>#000084</HeaderBackColor>\r\n          <HeaderFont>1</HeaderFont>\r\n          <FooterForeColor>Black</FooterForeColor>\r\n          <FooterBackColor>#CCCCCC</FooterBackColor>\r\n          <PagerForeColor>Black</PagerForeColor>\r\n          <PagerBackColor>#999999</PagerBackColor>\r\n          <PagerAlign>2</PagerAlign>\r\n          <PagerButtons>1</PagerButtons>\r\n          <RenderOuterTable>True</RenderOuterTable>\r\n        </Scheme>\r\n        <Scheme>\r\n          <SchemeName>FVScheme_Professional2</SchemeName>\r\n          <BackColor>White</BackColor>\r\n          <BorderColor>#CCCCCC</BorderColor>\r\n          <BorderWidth>1px</BorderWidth>\r\n          <BorderStyle>1</BorderStyle>\r\n          <GridLines>3</GridLines>\r\n          <CellPadding>3</CellPadding>\r\n          <CellSpacing>0</CellSpacing>\r\n          <RowForeColor>#000066</RowForeColor>\r\n          <EditRowForeColor>White</EditRowForeColor>\r\n          <EditRowBackColor>#669999</EditRowBackColor>\r\n          <EditRowFont>1</EditRowFont>\r\n          <HeaderForeColor>White</HeaderForeColor>\r\n          <HeaderBackColor>#006699</HeaderBackColor>\r\n          <HeaderFont>1</HeaderFont>\r\n          <FooterForeColor>#000066</FooterForeColor>\r\n          <FooterBackColor>White</FooterBackColor>\r\n          <PagerForeColor>#000066</PagerForeColor>\r\n          <PagerBackColor>White</PagerBackColor>\r\n          <PagerAlign>1</PagerAlign>\r\n          <PagerButtons>1</PagerButtons>\r\n          <RenderOuterTable>True</RenderOuterTable>\r\n        </Scheme>\r\n        <Scheme>\r\n          <SchemeName>FVScheme_Professional3</SchemeName>\r\n          <BackColor>White</BackColor>\r\n          <BorderColor>White</BorderColor>\r\n          <BorderWidth>2px</BorderWidth>\r\n          <BorderStyle>7</BorderStyle>\r\n          <GridLines>0</GridLines>\r\n          <CellPadding>3</CellPadding>\r\n          <CellSpacing>1</CellSpacing>\r\n          <RowForeColor>Black</RowForeColor>\r\n          <RowBackColor>#DEDFDE</RowBackColor>\r\n          <EditRowForeColor>White</EditRowForeColor>\r\n          <EditRowBackColor>#9471DE</EditRowBackColor>\r\n          <EditRowFont>1</EditRowFont>\r\n          <HeaderForeColor>#E7E7FF</HeaderForeColor>\r\n          <HeaderBackColor>#4A3C8C</HeaderBackColor>\r\n          <HeaderFont>1</HeaderFont>\r\n          <FooterForeColor>Black</FooterForeColor>\r\n          <FooterBackColor>#C6C3C6</FooterBackColor>\r\n          <PagerForeColor>Black</PagerForeColor>\r\n          <PagerBackColor>#C6C3C6</PagerBackColor>\r\n          <PagerAlign>3</PagerAlign>\r\n          <RenderOuterTable>True</RenderOuterTable>\r\n        </Scheme>\r\n        <Scheme>\r\n          <SchemeName>FVScheme_Simple1</SchemeName>\r\n          <ForeColor>Black</ForeColor>\r\n          <BackColor>White</BackColor>\r\n          <BorderColor>#999999</BorderColor>\r\n          <BorderWidth>1px</BorderWidth>\r\n          <BorderStyle>4</BorderStyle>\r\n          <GridLines>2</GridLines>\r\n          <CellPadding>3</CellPadding>\r\n          <CellSpacing>0</CellSpacing>\r\n          <EditRowForeColor>White</EditRowForeColor>\r\n          <EditRowBackColor>#000099</EditRowBackColor>\r\n          <EditRowFont>1</EditRowFont>\r\n          <HeaderForeColor>White</HeaderForeColor>\r\n          <HeaderBackColor>Black</HeaderBackColor>\r\n          <HeaderFont>1</HeaderFont>\r\n          <FooterBackColor>#CCCCCC</FooterBackColor>\r\n          <PagerForeColor>Black</PagerForeColor>\r\n          <PagerBackColor>#999999</PagerBackColor>\r\n          <PagerAlign>2</PagerAlign>\r\n          <RenderOuterTable>True</RenderOuterTable>\r\n        </Scheme>\r\n        <Scheme>\r\n          <SchemeName>FVScheme_Simple2</SchemeName>\r\n          <ForeColor>Black</ForeColor>\r\n          <BackColor>#CCCCCC</BackColor>\r\n          <BorderColor>#999999</BorderColor>\r\n          <BorderWidth>3px</BorderWidth>\r\n          <BorderStyle>4</BorderStyle>\r\n          <GridLines>3</GridLines>\r\n          <CellPadding>4</CellPadding>\r\n          <CellSpacing>2</CellSpacing>\r\n          <RowBackColor>White</RowBackColor>\r\n          <EditRowForeColor>White</EditRowForeColor>\r\n          <EditRowBackColor>#000099</EditRowBackColor>\r\n          <EditRowFont>1</EditRowFont>\r\n          <HeaderForeColor>White</HeaderForeColor>\r\n          <HeaderBackColor>Black</HeaderBackColor>\r\n          <HeaderFont>1</HeaderFont>\r\n          <FooterBackColor>#CCCCCC</FooterBackColor>\r\n          <PagerForeColor>Black</PagerForeColor>\r\n          <PagerBackColor>#CCCCCC</PagerBackColor>\r\n          <PagerAlign>1</PagerAlign>\r\n          <PagerButtons>1</PagerButtons>\r\n          <RenderOuterTable>True</RenderOuterTable>\r\n        </Scheme>\r\n        <Scheme>\r\n          <SchemeName>FVScheme_Simple3</SchemeName>\r\n          <BackColor>White</BackColor>\r\n          <BorderColor>#336666</BorderColor>\r\n          <BorderWidth>3px</BorderWidth>\r\n          <BorderStyle>5</BorderStyle>\r\n          <GridLines>1</GridLines>\r\n          <CellPadding>4</CellPadding>\r\n          <CellSpacing>0</CellSpacing>\r\n          <RowForeColor>#333333</RowForeColor>\r\n          <RowBackColor>White</RowBackColor>\r\n          <EditRowForeColor>White</EditRowForeColor>\r\n          <EditRowBackColor>#339966</EditRowBackColor>\r\n          <EditRowFont>1</EditRowFont>\r\n          <HeaderForeColor>White</HeaderForeColor>\r\n          <HeaderBackColor>#336666</HeaderBackColor>\r\n          <HeaderFont>1</HeaderFont>\r\n          <FooterForeColor>#333333</FooterForeColor>\r\n          <FooterBackColor>White</FooterBackColor>\r\n          <PagerForeColor>White</PagerForeColor>\r\n          <PagerBackColor>#336666</PagerBackColor>\r\n          <PagerAlign>2</PagerAlign>\r\n          <PagerButtons>1</PagerButtons>\r\n          <RenderOuterTable>True</RenderOuterTable>\r\n        </Scheme>\r\n        <Scheme>\r\n          <SchemeName>FVScheme_Classic1</SchemeName>\r\n          <ForeColor>Black</ForeColor>\r\n          <BackColor>White</BackColor>\r\n          <BorderColor>#CCCCCC</BorderColor>\r\n          <BorderWidth>1px</BorderWidth>\r\n          <BorderStyle>1</BorderStyle>\r\n          <GridLines>1</GridLines>\r\n          <CellPadding>4</CellPadding>\r\n          <CellSpacing>0</CellSpacing>\r\n          <EditRowForeColor>White</EditRowForeColor>\r\n          <EditRowBackColor>#CC3333</EditRowBackColor>\r\n          <EditRowFont>1</EditRowFont>\r\n          <HeaderForeColor>White</HeaderForeColor>\r\n          <HeaderBackColor>#333333</HeaderBackColor>\r\n          <HeaderFont>1</HeaderFont>\r\n          <FooterForeColor>Black</FooterForeColor>\r\n          <FooterBackColor>#CCCC99</FooterBackColor>\r\n          <PagerForeColor>Black</PagerForeColor>\r\n          <PagerBackColor>White</PagerBackColor>\r\n          <PagerAlign>3</PagerAlign>\r\n          <RenderOuterTable>True</RenderOuterTable>\r\n        </Scheme>\r\n        <Scheme>\r\n          <SchemeName>FVScheme_Classic2</SchemeName>\r\n          <ForeColor>Black</ForeColor>\r\n          <BackColor>White</BackColor>\r\n          <BorderColor>#DEDFDE</BorderColor>\r\n          <BorderWidth>1px</BorderWidth>\r\n          <BorderStyle>1</BorderStyle>\r\n          <GridLines>2</GridLines>\r\n          <CellPadding>4</CellPadding>\r\n          <CellSpacing>0</CellSpacing>\r\n          <RowBackColor>#F7F7DE</RowBackColor>\r\n          <EditRowForeColor>White</EditRowForeColor>\r\n          <EditRowBackColor>#CE5D5A</EditRowBackColor>\r\n          <EditRowFont>1</EditRowFont>\r\n          <HeaderForeColor>White</HeaderForeColor>\r\n          <HeaderBackColor>#6B696B</HeaderBackColor>\r\n          <HeaderFont>1</HeaderFont>\r\n          <FooterBackColor>#CCCC99</FooterBackColor>\r\n          <PagerForeColor>Black</PagerForeColor>\r\n          <PagerBackColor>[...string is too long...]"));
				}
				return FormViewDesigner._autoFormats;
			}
		}

		// Token: 0x1700018F RID: 399
		// (get) Token: 0x060006A5 RID: 1701 RVA: 0x000242E0 File Offset: 0x000224E0
		private bool CurrentModeTemplateExists
		{
			get
			{
				ITemplate template = null;
				if (((FormView)base.ViewControl).CurrentMode == FormViewMode.ReadOnly)
				{
					template = ((FormView)base.ViewControl).ItemTemplate;
				}
				if (((FormView)base.ViewControl).CurrentMode == FormViewMode.Insert)
				{
					template = ((FormView)base.ViewControl).InsertItemTemplate;
				}
				if (((FormView)base.ViewControl).CurrentMode == FormViewMode.Edit || (((FormView)base.ViewControl).CurrentMode == FormViewMode.Insert && template == null))
				{
					template = ((FormView)base.ViewControl).EditItemTemplate;
				}
				if (template != null)
				{
					IDesignerHost host = (IDesignerHost)base.ViewControl.Site.GetService(typeof(IDesignerHost));
					string text = ControlPersister.PersistTemplate(template, host);
					return text != null && text.Length > 0;
				}
				return false;
			}
		}

		// Token: 0x17000190 RID: 400
		// (get) Token: 0x060006A6 RID: 1702 RVA: 0x000243AB File Offset: 0x000225AB
		// (set) Token: 0x060006A7 RID: 1703 RVA: 0x000243B4 File Offset: 0x000225B4
		internal bool EnableDynamicData
		{
			get
			{
				return this._enableDynamicData;
			}
			set
			{
				this._enableDynamicData = value;
				IDataSourceViewSchema dataSourceSchema = this.GetDataSourceSchema();
				if (dataSourceSchema != null)
				{
					this.OnSchemaRefreshed();
				}
			}
		}

		// Token: 0x17000191 RID: 401
		// (get) Token: 0x060006A8 RID: 1704 RVA: 0x000243D8 File Offset: 0x000225D8
		// (set) Token: 0x060006A9 RID: 1705 RVA: 0x000243EC File Offset: 0x000225EC
		internal bool EnablePaging
		{
			get
			{
				return ((FormView)base.Component).AllowPaging;
			}
			set
			{
				Cursor value2 = Cursor.Current;
				try
				{
					Cursor.Current = Cursors.WaitCursor;
					ControlDesigner.InvokeTransactedChange(base.Component, new TransactedChangeCallback(this.EnablePagingCallback), value, SR.GetString("FormView_EnablePagingTransaction"));
				}
				finally
				{
					Cursor.Current = value2;
				}
			}
		}

		// Token: 0x17000192 RID: 402
		// (get) Token: 0x060006AA RID: 1706 RVA: 0x0002444C File Offset: 0x0002264C
		// (set) Token: 0x060006AB RID: 1707 RVA: 0x0002445E File Offset: 0x0002265E
		public bool RenderOuterTable
		{
			get
			{
				return ((FormView)base.Component).RenderOuterTable;
			}
			set
			{
				RenderOuterTableHelper.SetRenderOuterTable(value, this, true);
			}
		}

		// Token: 0x17000193 RID: 403
		// (get) Token: 0x060006AC RID: 1708 RVA: 0x00009D4C File Offset: 0x00007F4C
		protected override int SampleRowCount
		{
			get
			{
				return 2;
			}
		}

		// Token: 0x17000194 RID: 404
		// (get) Token: 0x060006AD RID: 1709 RVA: 0x00024468 File Offset: 0x00022668
		public override TemplateGroupCollection TemplateGroups
		{
			get
			{
				TemplateGroupCollection templateGroups = base.TemplateGroups;
				for (int i = 0; i < FormViewDesigner._controlTemplateNames.Length; i++)
				{
					string text = FormViewDesigner._controlTemplateNames[i];
					TemplateGroup templateGroup = new TemplateGroup(FormViewDesigner._controlTemplateNames[i], this.GetTemplateStyle(i));
					templateGroup.AddTemplateDefinition(new TemplateDefinition(this, text, base.Component, text)
					{
						SupportsDataBinding = FormViewDesigner._controlTemplateSupportsDataBinding[i]
					});
					templateGroups.Add(templateGroup);
				}
				return templateGroups;
			}
		}

		// Token: 0x17000195 RID: 405
		// (get) Token: 0x060006AE RID: 1710 RVA: 0x00003B0F File Offset: 0x00001D0F
		protected override bool UsePreviewControl
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060006AF RID: 1711 RVA: 0x000244DC File Offset: 0x000226DC
		private void AddTemplatesAndKeys(IDataSourceViewSchema schema)
		{
			StringBuilder stringBuilder = new StringBuilder();
			StringBuilder stringBuilder2 = new StringBuilder();
			StringBuilder stringBuilder3 = new StringBuilder();
			IDesignerHost designerHost = (IDesignerHost)base.Component.Site.GetService(typeof(IDesignerHost));
			if (schema != null)
			{
				IDataSourceFieldSchema[] fields = schema.GetFields();
				if (fields != null && fields.Length != 0)
				{
					ArrayList arrayList = new ArrayList();
					foreach (IDataSourceFieldSchema dataSourceFieldSchema in fields)
					{
						string name = dataSourceFieldSchema.Name;
						char[] array2 = new char[name.Length];
						for (int j = 0; j < name.Length; j++)
						{
							char c = name[j];
							if (char.IsLetterOrDigit(c) || c == '_')
							{
								array2[j] = c;
							}
							else
							{
								array2[j] = '_';
							}
						}
						string text = new string(array2);
						string text2 = DesignTimeDataBinding.CreateEvalExpression(name, string.Empty);
						string text3 = DesignTimeDataBinding.CreateBindExpression(name, string.Empty);
						if (dataSourceFieldSchema.PrimaryKey || dataSourceFieldSchema.Identity)
						{
							if (this.EnableDynamicData)
							{
								stringBuilder.Append(string.Format(CultureInfo.InvariantCulture, "{0}: <asp:DynamicControl DataField=\"{0}\" runat=\"server\" id=\"{2}DynamicControl\" Mode=\"{1}\" /><br />", new object[]
								{
									name,
									"ReadOnly",
									text
								}));
								stringBuilder2.Append(string.Format(CultureInfo.InvariantCulture, "{0}: <asp:DynamicControl DataField=\"{0}\" runat=\"server\" id=\"{2}DynamicControl\" Mode=\"{1}\" /><br />", new object[]
								{
									name,
									"ReadOnly",
									text
								}));
							}
							else
							{
								stringBuilder.Append(string.Format(CultureInfo.InvariantCulture, "{0}: <asp:Label Text='<%# {1} %>' runat=\"server\" id=\"{2}Label1\" /><br />", new object[]
								{
									name,
									text2,
									text
								}));
								stringBuilder2.Append(string.Format(CultureInfo.InvariantCulture, "{0}: <asp:Label Text='<%# {1} %>' runat=\"server\" id=\"{2}Label\" /><br />", new object[]
								{
									name,
									text2,
									text
								}));
							}
							if (!dataSourceFieldSchema.Identity)
							{
								if (this.EnableDynamicData)
								{
									stringBuilder3.Append(string.Format(CultureInfo.InvariantCulture, "{0}: <asp:DynamicControl DataField=\"{0}\" runat=\"server\" id=\"{1}DynamicControl\" Mode=\"Insert\" ValidationGroup=\"Insert\" /><br />", new object[]
									{
										name,
										text
									}));
								}
								else
								{
									stringBuilder3.Append(string.Format(CultureInfo.InvariantCulture, "{0}: <asp:TextBox Text='<%# {1} %>' runat=\"server\" id=\"{2}TextBox\" /><br />", new object[]
									{
										name,
										text3,
										text
									}));
								}
							}
						}
						else if (this.EnableDynamicData)
						{
							stringBuilder.Append(string.Format(CultureInfo.InvariantCulture, "{0}: <asp:DynamicControl DataField=\"{0}\" runat=\"server\" id=\"{2}DynamicControl\" Mode=\"{1}\" /><br />", new object[]
							{
								name,
								"Edit",
								text
							}));
							stringBuilder2.Append(string.Format(CultureInfo.InvariantCulture, "{0}: <asp:DynamicControl DataField=\"{0}\" runat=\"server\" id=\"{2}DynamicControl\" Mode=\"{1}\" /><br />", new object[]
							{
								name,
								"ReadOnly",
								text
							}));
							stringBuilder3.Append(string.Format(CultureInfo.InvariantCulture, "{0}: <asp:DynamicControl DataField=\"{0}\" runat=\"server\" id=\"{1}DynamicControl\" Mode=\"Insert\" ValidationGroup=\"Insert\" /><br />", new object[]
							{
								name,
								text
							}));
						}
						else if (dataSourceFieldSchema.DataType == typeof(bool) || dataSourceFieldSchema.DataType == typeof(bool?))
						{
							stringBuilder.Append(string.Format(CultureInfo.InvariantCulture, "{0}: <asp:CheckBox Checked='<%# {1} %>' runat=\"server\" id=\"{2}CheckBox\" /><br />", new object[]
							{
								name,
								text3,
								text
							}));
							stringBuilder2.Append(string.Format(CultureInfo.InvariantCulture, "{0}: <asp:CheckBox Checked='<%# {1} %>' runat=\"server\" id=\"{2}CheckBox\" Enabled=\"false\" /><br />", new object[]
							{
								name,
								text3,
								text
							}));
							stringBuilder3.Append(string.Format(CultureInfo.InvariantCulture, "{0}: <asp:CheckBox Checked='<%# {1} %>' runat=\"server\" id=\"{2}CheckBox\" /><br />", new object[]
							{
								name,
								text3,
								text
							}));
						}
						else
						{
							stringBuilder.Append(string.Format(CultureInfo.InvariantCulture, "{0}: <asp:TextBox Text='<%# {1} %>' runat=\"server\" id=\"{2}TextBox\" /><br />", new object[]
							{
								name,
								text3,
								text
							}));
							stringBuilder2.Append(string.Format(CultureInfo.InvariantCulture, "{0}: <asp:Label Text='<%# {1} %>' runat=\"server\" id=\"{2}Label\" /><br />", new object[]
							{
								name,
								text3,
								text
							}));
							stringBuilder3.Append(string.Format(CultureInfo.InvariantCulture, "{0}: <asp:TextBox Text='<%# {1} %>' runat=\"server\" id=\"{2}TextBox\" /><br />", new object[]
							{
								name,
								text3,
								text
							}));
						}
						stringBuilder.Append(Environment.NewLine);
						stringBuilder2.Append(Environment.NewLine);
						stringBuilder3.Append(Environment.NewLine);
						if (dataSourceFieldSchema.PrimaryKey)
						{
							arrayList.Add(name);
						}
					}
					bool flag = true;
					if (base.DesignerView.CanUpdate)
					{
						stringBuilder2.Append(string.Format(CultureInfo.InvariantCulture, "<asp:LinkButton runat=\"server\" Text=\"{3}\" CommandName=\"{0}\" id=\"{1}{0}Button\" CausesValidation=\"{2}\" />", new object[]
						{
							"Edit",
							string.Empty,
							bool.FalseString,
							SR.GetString("FormView_Edit")
						}));
						flag = false;
					}
					if (this.EnableDynamicData)
					{
						stringBuilder.Append(string.Format(CultureInfo.InvariantCulture, "<asp:LinkButton runat=\"server\" Text=\"{3}\" CommandName=\"{0}\" id=\"{1}{0}Button\" CausesValidation=\"{2}\" ValidationGroup=\"Insert\" />", new object[]
						{
							"Update",
							string.Empty,
							bool.TrueString,
							SR.GetString("FormView_Update")
						}));
					}
					else
					{
						stringBuilder.Append(string.Format(CultureInfo.InvariantCulture, "<asp:LinkButton runat=\"server\" Text=\"{3}\" CommandName=\"{0}\" id=\"{1}{0}Button\" CausesValidation=\"{2}\" />", new object[]
						{
							"Update",
							string.Empty,
							bool.TrueString,
							SR.GetString("FormView_Update")
						}));
					}
					stringBuilder.Append("&nbsp;");
					stringBuilder.Append(string.Format(CultureInfo.InvariantCulture, "<asp:LinkButton runat=\"server\" Text=\"{3}\" CommandName=\"{0}\" id=\"{1}{0}Button\" CausesValidation=\"{2}\" />", new object[]
					{
						"Cancel",
						"Update",
						bool.FalseString,
						SR.GetString("FormView_Cancel")
					}));
					if (base.DesignerView.CanDelete)
					{
						if (!flag)
						{
							stringBuilder2.Append("&nbsp;");
						}
						stringBuilder2.Append(string.Format(CultureInfo.InvariantCulture, "<asp:LinkButton runat=\"server\" Text=\"{3}\" CommandName=\"{0}\" id=\"{1}{0}Button\" CausesValidation=\"{2}\" />", new object[]
						{
							"Delete",
							string.Empty,
							bool.FalseString,
							SR.GetString("FormView_Delete")
						}));
						flag = false;
					}
					if (base.DesignerView.CanInsert)
					{
						if (!flag)
						{
							stringBuilder2.Append("&nbsp;");
						}
						stringBuilder2.Append(string.Format(CultureInfo.InvariantCulture, "<asp:LinkButton runat=\"server\" Text=\"{3}\" CommandName=\"{0}\" id=\"{1}{0}Button\" CausesValidation=\"{2}\" />", new object[]
						{
							"New",
							string.Empty,
							bool.FalseString,
							SR.GetString("FormView_New")
						}));
					}
					if (this.EnableDynamicData)
					{
						stringBuilder3.Append(string.Format(CultureInfo.InvariantCulture, "<asp:LinkButton runat=\"server\" Text=\"{3}\" CommandName=\"{0}\" id=\"{1}{0}Button\" CausesValidation=\"{2}\" ValidationGroup=\"Insert\" />", new object[]
						{
							"Insert",
							string.Empty,
							bool.TrueString,
							SR.GetString("FormView_Insert")
						}));
					}
					else
					{
						stringBuilder3.Append(string.Format(CultureInfo.InvariantCulture, "<asp:LinkButton runat=\"server\" Text=\"{3}\" CommandName=\"{0}\" id=\"{1}{0}Button\" CausesValidation=\"{2}\" />", new object[]
						{
							"Insert",
							string.Empty,
							bool.TrueString,
							SR.GetString("FormView_Insert")
						}));
					}
					stringBuilder3.Append("&nbsp;");
					stringBuilder3.Append(string.Format(CultureInfo.InvariantCulture, "<asp:LinkButton runat=\"server\" Text=\"{3}\" CommandName=\"{0}\" id=\"{1}{0}Button\" CausesValidation=\"{2}\" />", new object[]
					{
						"Cancel",
						"Insert",
						bool.FalseString,
						SR.GetString("FormView_Cancel")
					}));
					stringBuilder.Append(Environment.NewLine);
					stringBuilder2.Append(Environment.NewLine);
					stringBuilder3.Append(Environment.NewLine);
					try
					{
						((FormView)base.Component).EditItemTemplate = ControlParser.ParseTemplate(designerHost, stringBuilder.ToString());
						((FormView)base.Component).ItemTemplate = ControlParser.ParseTemplate(designerHost, stringBuilder2.ToString());
						((FormView)base.Component).InsertItemTemplate = ControlParser.ParseTemplate(designerHost, stringBuilder3.ToString());
					}
					catch
					{
					}
					int count = arrayList.Count;
					if (count > 0)
					{
						string[] array3 = new string[count];
						arrayList.CopyTo(array3, 0);
						((FormView)base.Component).DataKeyNames = array3;
					}
				}
			}
		}

		// Token: 0x060006B0 RID: 1712 RVA: 0x00024CA8 File Offset: 0x00022EA8
		private bool EnablePagingCallback(object context)
		{
			bool allowPaging = ((FormView)base.Component).AllowPaging;
			bool flag = !allowPaging;
			if (context is bool)
			{
				flag = (bool)context;
			}
			PropertyDescriptor propertyDescriptor = TypeDescriptor.GetProperties(typeof(FormView))["AllowPaging"];
			propertyDescriptor.SetValue(base.Component, flag);
			return true;
		}

		// Token: 0x060006B1 RID: 1713 RVA: 0x00024D08 File Offset: 0x00022F08
		private IDataSourceViewSchema GetDataSourceSchema()
		{
			DesignerDataSourceView designerView = base.DesignerView;
			if (designerView != null)
			{
				try
				{
					return designerView.Schema;
				}
				catch (Exception ex)
				{
					IComponentDesignerDebugService componentDesignerDebugService = (IComponentDesignerDebugService)base.Component.Site.GetService(typeof(IComponentDesignerDebugService));
					if (componentDesignerDebugService != null)
					{
						componentDesignerDebugService.Fail(SR.GetString("DataSource_DebugService_FailedCall", new object[]
						{
							"DesignerDataSourceView.Schema",
							ex.Message
						}));
					}
				}
			}
			return null;
		}

		// Token: 0x060006B2 RID: 1714 RVA: 0x00024D88 File Offset: 0x00022F88
		public override string GetDesignTimeHtml()
		{
			FormView formView = (FormView)base.ViewControl;
			bool flag = false;
			string[] dataKeyNames = null;
			string result = null;
			if (this.CurrentModeTemplateExists)
			{
				bool flag2 = false;
				IDataSourceViewSchema dataSourceSchema = this.GetDataSourceSchema();
				if (dataSourceSchema != null)
				{
					IDataSourceFieldSchema[] fields = dataSourceSchema.GetFields();
					if (fields != null && fields.Length != 0)
					{
						flag2 = true;
					}
				}
				try
				{
					if (!flag2)
					{
						dataKeyNames = formView.DataKeyNames;
						formView.DataKeyNames = new string[0];
						flag = true;
					}
					TypeDescriptor.Refresh(base.Component);
					return base.GetDesignTimeHtml();
				}
				finally
				{
					if (flag)
					{
						formView.DataKeyNames = dataKeyNames;
					}
				}
			}
			result = this.GetEmptyDesignTimeHtml();
			return result;
		}

		// Token: 0x060006B3 RID: 1715 RVA: 0x00024E24 File Offset: 0x00023024
		protected override string GetEmptyDesignTimeHtml()
		{
			return base.CreatePlaceHolderDesignTimeHtml(SR.GetString("DataList_NoTemplatesInst"));
		}

		// Token: 0x060006B4 RID: 1716 RVA: 0x00024E38 File Offset: 0x00023038
		private Style GetTemplateStyle(int templateIndex)
		{
			Style style = new Style();
			style.CopyFrom(((FormView)base.ViewControl).ControlStyle);
			switch (templateIndex)
			{
			case 0:
				style.CopyFrom(((FormView)base.ViewControl).RowStyle);
				break;
			case 1:
				style.CopyFrom(((FormView)base.ViewControl).FooterStyle);
				break;
			case 2:
				style.CopyFrom(((FormView)base.ViewControl).RowStyle);
				style.CopyFrom(((FormView)base.ViewControl).EditRowStyle);
				break;
			case 3:
				style.CopyFrom(((FormView)base.ViewControl).RowStyle);
				style.CopyFrom(((FormView)base.ViewControl).InsertRowStyle);
				break;
			case 4:
				style.CopyFrom(((FormView)base.ViewControl).HeaderStyle);
				break;
			case 5:
				style.CopyFrom(((FormView)base.ViewControl).EmptyDataRowStyle);
				break;
			case 6:
				style.CopyFrom(((FormView)base.ViewControl).PagerStyle);
				break;
			}
			return style;
		}

		// Token: 0x060006B5 RID: 1717 RVA: 0x00024F61 File Offset: 0x00023161
		public override void Initialize(IComponent component)
		{
			ControlDesigner.VerifyInitializeArgument(component, typeof(FormView));
			base.Initialize(component);
			if (base.View != null)
			{
				base.View.SetFlags(ViewFlags.TemplateEditing, true);
			}
		}

		// Token: 0x060006B6 RID: 1718 RVA: 0x00024F8F File Offset: 0x0002318F
		protected override void OnSchemaRefreshed()
		{
			if (base.InTemplateMode)
			{
				return;
			}
			ControlDesigner.InvokeTransactedChange(base.Component, new TransactedChangeCallback(this.SchemaRefreshedCallback), null, SR.GetString("DataControls_SchemaRefreshedTransaction"));
		}

		// Token: 0x060006B7 RID: 1719 RVA: 0x00024FBC File Offset: 0x000231BC
		protected override void PreFilterProperties(IDictionary properties)
		{
			base.PreFilterProperties(properties);
			RenderOuterTableHelper.SetupRenderOuterTable(properties, base.Component, true, base.GetType());
		}

		// Token: 0x060006B8 RID: 1720 RVA: 0x00024FD8 File Offset: 0x000231D8
		private bool SchemaRefreshedCallback(object context)
		{
			IDataSourceViewSchema dataSourceSchema = this.GetDataSourceSchema();
			if (base.DataSourceID.Length > 0 && dataSourceSchema != null)
			{
				if (((FormView)base.Component).DataKeyNames.Length != 0 || ((FormView)base.Component).ItemTemplate != null || ((FormView)base.Component).EditItemTemplate != null)
				{
					if (DialogResult.Yes == UIServiceHelper.ShowMessage(base.Component.Site, SR.GetString("FormView_SchemaRefreshedWarning"), SR.GetString("FormView_SchemaRefreshedCaption", new object[]
					{
						((FormView)base.Component).ID
					}), MessageBoxButtons.YesNo))
					{
						((FormView)base.Component).DataKeyNames = new string[0];
						this.AddTemplatesAndKeys(dataSourceSchema);
					}
				}
				else
				{
					this.AddTemplatesAndKeys(dataSourceSchema);
				}
			}
			else if ((((FormView)base.Component).DataKeyNames.Length != 0 || ((FormView)base.Component).ItemTemplate != null || ((FormView)base.Component).EditItemTemplate != null) && DialogResult.Yes == UIServiceHelper.ShowMessage(base.Component.Site, SR.GetString("FormView_SchemaRefreshedWarningNoDataSource"), SR.GetString("FormView_SchemaRefreshedCaption", new object[]
			{
				((FormView)base.Component).ID
			}), MessageBoxButtons.YesNo))
			{
				((FormView)base.Component).DataKeyNames = new string[0];
				((FormView)base.Component).ItemTemplate = null;
				((FormView)base.Component).InsertItemTemplate = null;
				((FormView)base.Component).EditItemTemplate = null;
			}
			this.UpdateDesignTimeHtml();
			return true;
		}

		// Token: 0x040003FD RID: 1021
		private static DesignerAutoFormatCollection _autoFormats;

		// Token: 0x040003FE RID: 1022
		private static string[] _controlTemplateNames = new string[]
		{
			"ItemTemplate",
			"FooterTemplate",
			"EditItemTemplate",
			"InsertItemTemplate",
			"HeaderTemplate",
			"EmptyDataTemplate",
			"PagerTemplate"
		};

		// Token: 0x040003FF RID: 1023
		private static bool[] _controlTemplateSupportsDataBinding = new bool[]
		{
			true,
			true,
			true,
			true,
			true,
			true,
			true
		};

		// Token: 0x04000400 RID: 1024
		private const int IDX_CONTROL_HEADER_TEMPLATE = 4;

		// Token: 0x04000401 RID: 1025
		private const int IDX_CONTROL_ITEM_TEMPLATE = 0;

		// Token: 0x04000402 RID: 1026
		private const int IDX_CONTROL_EDITITEM_TEMPLATE = 2;

		// Token: 0x04000403 RID: 1027
		private const int IDX_CONTROL_INSERTITEM_TEMPLATE = 3;

		// Token: 0x04000404 RID: 1028
		private const int IDX_CONTROL_FOOTER_TEMPLATE = 1;

		// Token: 0x04000405 RID: 1029
		private const int IDX_CONTROL_EMPTY_DATA_TEMPLATE = 5;

		// Token: 0x04000406 RID: 1030
		private const int IDX_CONTROL_PAGER_TEMPLATE = 6;

		// Token: 0x04000407 RID: 1031
		private const string itemTemplateFieldString = "{0}: <asp:Label Text='<%# {1} %>' runat=\"server\" id=\"{2}Label\" /><br />";

		// Token: 0x04000408 RID: 1032
		private const string keyItemTemplateFieldString = "{0}: <asp:Label Text='<%# {1} %>' runat=\"server\" id=\"{2}Label\" /><br />";

		// Token: 0x04000409 RID: 1033
		private const string boolItemTemplateFieldString = "{0}: <asp:CheckBox Checked='<%# {1} %>' runat=\"server\" id=\"{2}CheckBox\" Enabled=\"false\" /><br />";

		// Token: 0x0400040A RID: 1034
		private const string dynamicDataItemTemplateFieldString = "{0}: <asp:DynamicControl DataField=\"{0}\" runat=\"server\" id=\"{2}DynamicControl\" Mode=\"{1}\" /><br />";

		// Token: 0x0400040B RID: 1035
		private const string editItemTemplateFieldString = "{0}: <asp:TextBox Text='<%# {1} %>' runat=\"server\" id=\"{2}TextBox\" /><br />";

		// Token: 0x0400040C RID: 1036
		private const string boolEditItemTemplateFieldString = "{0}: <asp:CheckBox Checked='<%# {1} %>' runat=\"server\" id=\"{2}CheckBox\" /><br />";

		// Token: 0x0400040D RID: 1037
		private const string keyEditItemTemplateFieldString = "{0}: <asp:Label Text='<%# {1} %>' runat=\"server\" id=\"{2}Label1\" /><br />";

		// Token: 0x0400040E RID: 1038
		private const string insertItemTemplateFieldString = "{0}: <asp:TextBox Text='<%# {1} %>' runat=\"server\" id=\"{2}TextBox\" /><br />";

		// Token: 0x0400040F RID: 1039
		private const string boolInsertItemTemplateFieldString = "{0}: <asp:CheckBox Checked='<%# {1} %>' runat=\"server\" id=\"{2}CheckBox\" /><br />";

		// Token: 0x04000410 RID: 1040
		private const string dynamicDataInsertItemTemplateFieldString = "{0}: <asp:DynamicControl DataField=\"{0}\" runat=\"server\" id=\"{1}DynamicControl\" Mode=\"Insert\" ValidationGroup=\"Insert\" /><br />";

		// Token: 0x04000411 RID: 1041
		private const string templateButtonString = "<asp:LinkButton runat=\"server\" Text=\"{3}\" CommandName=\"{0}\" id=\"{1}{0}Button\" CausesValidation=\"{2}\" />";

		// Token: 0x04000412 RID: 1042
		private const string dynamicDataInsertTemplateButtonString = "<asp:LinkButton runat=\"server\" Text=\"{3}\" CommandName=\"{0}\" id=\"{1}{0}Button\" CausesValidation=\"{2}\" ValidationGroup=\"Insert\" />";

		// Token: 0x04000413 RID: 1043
		private const string nonBreakingSpace = "&nbsp;";

		// Token: 0x04000414 RID: 1044
		private FormViewActionList _actionLists;

		// Token: 0x04000415 RID: 1045
		private bool _enableDynamicData;
	}
}
