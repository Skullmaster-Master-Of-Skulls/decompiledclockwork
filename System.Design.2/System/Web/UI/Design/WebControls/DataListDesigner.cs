using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Design;
using System.Diagnostics;
using System.Globalization;
using System.Security.Permissions;
using System.Text;
using System.Web.UI.Design.Util;
using System.Web.UI.WebControls;
using System.Windows.Forms;

namespace System.Web.UI.Design.WebControls
{
	// Token: 0x020000BF RID: 191
	[SupportsPreviewControl(true)]
	[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
	public class DataListDesigner : BaseDataListDesigner
	{
		// Token: 0x0600060A RID: 1546 RVA: 0x000204BD File Offset: 0x0001E6BD
		public DataListDesigner()
		{
			this.templateVerbsDirty = true;
		}

		// Token: 0x1700016A RID: 362
		// (get) Token: 0x0600060B RID: 1547 RVA: 0x000204CC File Offset: 0x0001E6CC
		public override bool AllowResize
		{
			get
			{
				return this.TemplatesExist || base.InTemplateModeInternal;
			}
		}

		// Token: 0x1700016B RID: 363
		// (get) Token: 0x0600060C RID: 1548 RVA: 0x000204DE File Offset: 0x0001E6DE
		public override DesignerAutoFormatCollection AutoFormats
		{
			get
			{
				if (DataListDesigner._autoFormats == null)
				{
					DataListDesigner._autoFormats = ControlDesigner.CreateAutoFormats(AutoFormatSchemes.BDL_SCHEME_NAMES, (string schemeName) => new DataListAutoFormat(schemeName, "<Schemes>\r\n        <xsd:schema id=\"Schemes\" xmlns=\"\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns:msdata=\"urn:schemas-microsoft-com:xml-msdata\">\r\n          <xsd:element name=\"Scheme\">\r\n            <xsd:complexType>\r\n              <xsd:all>\r\n                <xsd:element name=\"SchemeName\" type=\"xsd:string\"/>\r\n                <xsd:element name=\"ForeColor\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n                <xsd:element name=\"BackColor\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n                <xsd:element name=\"BorderColor\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n                <xsd:element name=\"BorderWidth\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n                <xsd:element name=\"BorderStyle\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n                <xsd:element name=\"GridLines\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n                <xsd:element name=\"CellPadding\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n                <xsd:element name=\"CellSpacing\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n                <xsd:element name=\"ItemForeColor\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n                <xsd:element name=\"ItemBackColor\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n                <xsd:element name=\"ItemFont\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n                <xsd:element name=\"AltItemForeColor\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n                <xsd:element name=\"AltItemBackColor\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n                <xsd:element name=\"AltItemFont\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n                <xsd:element name=\"SelItemForeColor\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n                <xsd:element name=\"SelItemBackColor\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n                <xsd:element name=\"SelItemFont\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n                <xsd:element name=\"HeaderForeColor\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n                <xsd:element name=\"HeaderBackColor\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n                <xsd:element name=\"HeaderFont\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n                <xsd:element name=\"FooterForeColor\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n                <xsd:element name=\"FooterBackColor\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n                <xsd:element name=\"FooterFont\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n                <xsd:element name=\"PagerForeColor\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n                <xsd:element name=\"PagerBackColor\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n                <xsd:element name=\"PagerFont\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n                <xsd:element name=\"PagerAlign\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n                <xsd:element name=\"PagerMode\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n                <xsd:element name=\"EditItemForeColor\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n                <xsd:element name=\"EditItemBackColor\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n                <xsd:element name=\"EditItemFont\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n              </xsd:all>\r\n            </xsd:complexType>\r\n          </xsd:element>\r\n          <xsd:element name=\"Schemes\" msdata:IsDataSet=\"true\">\r\n            <xsd:complexType>\r\n              <xsd:choice maxOccurs=\"unbounded\">\r\n                <xsd:element ref=\"Scheme\"/>\r\n              </xsd:choice>\r\n            </xsd:complexType>\r\n          </xsd:element>\r\n        </xsd:schema>\r\n        <Scheme>\r\n          <SchemeName>BDLScheme_Empty</SchemeName>\r\n        </Scheme>\r\n        <Scheme>\r\n          <SchemeName>BDLScheme_Consistent1</SchemeName>\r\n          <AltItemBackColor>White</AltItemBackColor>\r\n          <GridLines>0</GridLines>\r\n          <CellPadding>4</CellPadding>\r\n          <ForeColor>#333333</ForeColor>\r\n          <ItemForeColor>#333333</ItemForeColor>\r\n          <ItemBackColor>#FFFBD6</ItemBackColor>\r\n          <SelItemForeColor>Navy</SelItemForeColor>\r\n          <SelItemBackColor>#FFCC66</SelItemBackColor>\r\n          <SelItemFont>1</SelItemFont>\r\n          <HeaderForeColor>White</HeaderForeColor>\r\n          <HeaderBackColor>#990000</HeaderBackColor>\r\n          <HeaderFont>1</HeaderFont>\r\n          <FooterForeColor>White</FooterForeColor>\r\n          <FooterBackColor>#990000</FooterBackColor>\r\n          <FooterFont>1</FooterFont>\r\n          <PagerForeColor>#333333</PagerForeColor>\r\n          <PagerBackColor>#FFCC66</PagerBackColor>\r\n          <PagerAlign>2</PagerAlign>\r\n        </Scheme>\r\n        <Scheme>\r\n          <SchemeName>BDLScheme_Consistent2</SchemeName>\r\n            <AltItemBackColor>White</AltItemBackColor>\r\n            <GridLines>0</GridLines>\r\n            <CellPadding>4</CellPadding>\r\n            <ForeColor>#333333</ForeColor>\r\n            <ItemBackColor>#EFF3FB</ItemBackColor>\r\n            <SelItemForeColor>#333333</SelItemForeColor>\r\n            <SelItemBackColor>#D1DDF1</SelItemBackColor>\r\n            <SelItemFont>1</SelItemFont>\r\n            <HeaderForeColor>White</HeaderForeColor>\r\n            <HeaderBackColor>#507CD1</HeaderBackColor>\r\n            <HeaderFont>1</HeaderFont>\r\n            <FooterForeColor>White</FooterForeColor>\r\n            <FooterBackColor>#507CD1</FooterBackColor>\r\n            <FooterFont>1</FooterFont>\r\n            <PagerForeColor>White</PagerForeColor>\r\n            <PagerBackColor>#2461BF</PagerBackColor>\r\n            <PagerAlign>2</PagerAlign>\r\n            <EditItemBackColor>#2461BF</EditItemBackColor>\r\n        </Scheme>\r\n        <Scheme>\r\n          <SchemeName>BDLScheme_Consistent3</SchemeName>\r\n            <AltItemBackColor>White</AltItemBackColor>\r\n            <GridLines>0</GridLines>\r\n            <CellPadding>4</CellPadding>\r\n            <ForeColor>#333333</ForeColor>\r\n            <ItemBackColor>#E3EAEB</ItemBackColor>\r\n            <SelItemForeColor>#333333</SelItemForeColor>\r\n            <SelItemBackColor>#C5BBAF</SelItemBackColor>\r\n            <SelItemFont>1</SelItemFont>\r\n            <HeaderForeColor>White</HeaderForeColor>\r\n            <HeaderBackColor>#1C5E55</HeaderBackColor>\r\n            <HeaderFont>1</HeaderFont>\r\n            <FooterForeColor>White</FooterForeColor>\r\n            <FooterBackColor>#1C5E55</FooterBackColor>\r\n            <FooterFont>1</FooterFont>\r\n            <PagerForeColor>White</PagerForeColor>\r\n            <PagerBackColor>#666666</PagerBackColor>\r\n            <PagerAlign>2</PagerAlign>\r\n            <EditItemBackColor>#7C6F57</EditItemBackColor>\r\n        </Scheme>\r\n        <Scheme>\r\n          <SchemeName>BDLScheme_Consistent4</SchemeName>\r\n            <AltItemBackColor>White</AltItemBackColor>\r\n            <AltItemForeColor>#284775</AltItemForeColor>\r\n            <GridLines>0</GridLines>\r\n            <CellPadding>4</CellPadding>\r\n            <ForeColor>#333333</ForeColor>\r\n            <ItemForeColor>#333333</ItemForeColor>\r\n            <ItemBackColor>#F7F6F3</ItemBackColor>\r\n            <SelItemForeColor>#333333</SelItemForeColor>\r\n            <SelItemBackColor>#E2DED6</SelItemBackColor>\r\n            <SelItemFont>1</SelItemFont>\r\n            <HeaderForeColor>White</HeaderForeColor>\r\n            <HeaderBackColor>#5D7B9D</HeaderBackColor>\r\n            <HeaderFont>1</HeaderFont>\r\n            <FooterForeColor>White</FooterForeColor>\r\n            <FooterBackColor>#5D7B9D</FooterBackColor>\r\n            <FooterFont>1</FooterFont>\r\n            <PagerForeColor>White</PagerForeColor>\r\n            <PagerBackColor>#284775</PagerBackColor>\r\n            <PagerAlign>2</PagerAlign>\r\n            <EditItemBackColor>#999999</EditItemBackColor>\r\n        </Scheme>\r\n        <Scheme>\r\n          <SchemeName>BDLScheme_Colorful1</SchemeName>\r\n          <BackColor>White</BackColor>\r\n          <BorderColor>#CC9966</BorderColor>\r\n          <BorderWidth>1px</BorderWidth>\r\n          <BorderStyle>1</BorderStyle>\r\n          <GridLines>3</GridLines>\r\n          <CellPadding>4</CellPadding>\r\n          <CellSpacing>0</CellSpacing>\r\n          <ItemForeColor>#330099</ItemForeColor>\r\n          <ItemBackColor>White</ItemBackColor>\r\n          <SelItemForeColor>#663399</SelItemForeColor>\r\n          <SelItemBackColor>#FFCC66</SelItemBackColor>\r\n          <SelItemFont>1</SelItemFont>\r\n          <HeaderForeColor>#FFFFCC</HeaderForeColor>\r\n          <HeaderBackColor>#990000</HeaderBackColor>\r\n          <HeaderFont>1</HeaderFont>\r\n          <FooterForeColor>#330099</FooterForeColor>\r\n          <FooterBackColor>#FFFFCC</FooterBackColor>\r\n          <PagerForeColor>#330099</PagerForeColor>\r\n          <PagerBackColor>#FFFFCC</PagerBackColor>\r\n          <PagerAlign>2</PagerAlign>\r\n        </Scheme>\r\n        <Scheme>\r\n          <SchemeName>BDLScheme_Colorful2</SchemeName>\r\n          <BackColor>White</BackColor>\r\n          <BorderColor>#3366CC</BorderColor>\r\n          <BorderWidth>1px</BorderWidth>\r\n          <BorderStyle>1</BorderStyle>\r\n          <GridLines>3</GridLines>\r\n          <CellPadding>4</CellPadding>\r\n          <CellSpacing>0</CellSpacing>\r\n          <ItemForeColor>#003399</ItemForeColor>\r\n          <ItemBackColor>White</ItemBackColor>\r\n          <SelItemForeColor>#CCFF99</SelItemForeColor>\r\n          <SelItemBackColor>#009999</SelItemBackColor>\r\n          <SelItemFont>1</SelItemFont>\r\n          <HeaderForeColor>#CCCCFF</HeaderForeColor>\r\n          <HeaderBackColor>#003399</HeaderBackColor>\r\n          <HeaderFont>1</HeaderFont>\r\n          <FooterForeColor>#003399</FooterForeColor>\r\n          <FooterBackColor>#99CCCC</FooterBackColor>\r\n          <PagerForeColor>#003399</PagerForeColor>\r\n          <PagerBackColor>#99CCCC</PagerBackColor>\r\n          <PagerAlign>1</PagerAlign>\r\n          <PagerMode>1</PagerMode>\r\n        </Scheme>\r\n        <Scheme>\r\n          <SchemeName>BDLScheme_Colorful3</SchemeName>\r\n          <BackColor>#DEBA84</BackColor>\r\n          <BorderColor>#DEBA84</BorderColor>\r\n          <BorderWidth>1px</BorderWidth>\r\n          <BorderStyle>1</BorderStyle>\r\n          <GridLines>3</GridLines>\r\n          <CellPadding>3</CellPadding>\r\n          <CellSpacing>2</CellSpacing>\r\n          <ItemForeColor>#8C4510</ItemForeColor>\r\n          <ItemBackColor>#FFF7E7</ItemBackColor>\r\n          <SelItemForeColor>White</SelItemForeColor>\r\n          <SelItemBackColor>#738A9C</SelItemBackColor>\r\n          <SelItemFont>1</SelItemFont>\r\n          <HeaderForeColor>White</HeaderForeColor>\r\n          <HeaderBackColor>#A55129</HeaderBackColor>\r\n          <HeaderFont>1</HeaderFont>\r\n          <FooterForeColor>#8C4510</FooterForeColor>\r\n          <FooterBackColor>#F7DFB5</FooterBackColor>\r\n          <PagerForeColor>#8C4510</PagerForeColor>\r\n          <PagerAlign>2</PagerAlign>\r\n          <PagerMode>1</PagerMode>\r\n        </Scheme>\r\n        <Scheme>\r\n          <SchemeName>BDLScheme_Colorful4</SchemeName>\r\n          <BackColor>White</BackColor>\r\n          <BorderColor>#E7E7FF</BorderColor>\r\n          <BorderWidth>1px</BorderWidth>\r\n          <BorderStyle>1</BorderStyle>\r\n          <GridLines>1</GridLines>\r\n          <CellPadding>3</CellPadding>\r\n          <CellSpacing>0</CellSpacing>\r\n          <ItemForeColor>#4A3C8C</ItemForeColor>\r\n          <ItemBackColor>#E7E7FF</ItemBackColor>\r\n          <AltItemBackColor>#F7F7F7</AltItemBackColor>\r\n          <SelItemForeColor>#F7F7F7</SelItemForeColor>\r\n          <SelItemBackColor>#738A9C</SelItemBackColor>\r\n          <SelItemFont>1</SelItemFont>\r\n          <HeaderForeColor>#F7F7F7</HeaderForeColor>\r\n          <HeaderBackColor>#4A3C8C</HeaderBackColor>\r\n          <HeaderFont>1</HeaderFont>\r\n          <FooterForeColor>#4A3C8C</FooterForeColor>\r\n          <FooterBackColor>#B5C7DE</FooterBackColor>\r\n          <PagerForeColor>#4A3C8C</PagerForeColor>\r\n          <PagerBackColor>#E7E7FF</PagerBackColor>\r\n          <PagerAlign>3</PagerAlign>\r\n          <PagerMode>1</PagerMode>\r\n        </Scheme>\r\n        <Scheme>\r\n          <SchemeName>BDLScheme_Colorful5</SchemeName>\r\n          <ForeColor>Black</ForeColor>\r\n          <BackColor>LightGoldenRodYellow</BackColor>\r\n          <BorderColor>Tan</BorderColor>\r\n          <BorderWidth>1px</BorderWidth>\r\n          <GridLines>0</GridLines>\r\n          <CellPadding>2</CellPadding>\r\n          <AltItemBackColor>PaleGoldenRod</AltItemBackColor>\r\n          <HeaderBackColor>Tan</HeaderBackColor>\r\n          <HeaderFont>1</HeaderFont>\r\n          <FooterBackColor>Tan</FooterBackColor>\r\n          <SelItemBackColor>DarkSlateBlue</SelItemBackColor>\r\n          <SelItemForeColor>GhostWhite</SelItemForeColor>\r\n          <PagerBackColor>PaleGoldenrod</PagerBackColor>\r\n          <PagerForeColor>DarkSlateBlue</PagerForeColor>\r\n          <PagerAlign>2</PagerAlign>\r\n        </Scheme>\r\n        <Scheme>\r\n          <SchemeName>BDLScheme_Professional1</SchemeName>\r\n          <BackColor>White</BackColor>\r\n          <BorderColor>#999999</BorderColor>\r\n          <BorderWidth>1px</BorderWidth>\r\n          <BorderStyle>1</BorderStyle>\r\n          <GridLines>2</GridLines>\r\n          <CellPadding>3</CellPadding>\r\n          <CellSpacing>0</CellSpacing>\r\n          <ItemForeColor>Black</ItemForeColor>\r\n          <ItemBackColor>#EEEEEE</ItemBackColor>\r\n          <AltItemBackColor>#DCDCDC</AltItemBackColor>\r\n          <SelItemForeColor>White</SelItemForeColor>\r\n          <SelItemBackColor>#008A8C</SelItemBackColor>\r\n          <SelItemFont>1</SelItemFont>\r\n          <HeaderForeColor>White</HeaderForeColor>\r\n          <HeaderBackColor>#000084</HeaderBackColor>\r\n          <HeaderFont>1</HeaderFont>\r\n          <FooterForeColor>Black</FooterForeColor>\r\n          <FooterBackColor>#CCCCCC</FooterBackColor>\r\n          <PagerForeColor>Black</PagerForeColor>\r\n          <PagerBackColor>#999999</PagerBackColor>\r\n          <PagerAlign>2</PagerAlign>\r\n          <PagerMode>1</PagerMode>\r\n        </Scheme>\r\n        <Scheme>\r\n          <SchemeName>BDLScheme_Professional2</SchemeName>\r\n          <BackColor>White</BackColor>\r\n          <BorderColor>#CCCCCC</BorderColor>\r\n          <BorderWidth>1px</BorderWidth>\r\n          <BorderStyle>1</BorderStyle>\r\n          <GridLines>3</GridLines>\r\n          <CellPadding>3</CellPadding>\r\n          <CellSpacing>0</CellSpacing>\r\n          <ItemForeColor>#000066</ItemForeColor>\r\n          <SelItemForeColor>White</SelItemForeColor>\r\n          <SelItemBackColor>#669999</SelItemBackColor>\r\n          <SelItemFont>1</SelItemFont>\r\n          <HeaderForeColor>White</HeaderForeColor>\r\n          <HeaderBackColor>#006699</HeaderBackColor>\r\n          <HeaderFont>1</HeaderFont>\r\n          <FooterForeColor>#000066</FooterForeColor>\r\n          <FooterBackColor>White</FooterBackColor>\r\n          <PagerForeColor>#000066</PagerForeColor>\r\n          <PagerBackColor>White</PagerBackColor>\r\n          <PagerAlign>1</PagerAlign>\r\n          <PagerMode>1</PagerMode>\r\n        </Scheme>\r\n        <Scheme>\r\n          <SchemeName>BDLScheme_Professional3</SchemeName>\r\n          <BackColor>White</BackColor>\r\n          <BorderColor>White</BorderColor>\r\n          <BorderWidth>2px</BorderWidth>\r\n          <BorderStyle>7</BorderStyle>\r\n          <GridLines>0</GridLines>\r\n          <CellPadding>3</CellPadding>\r\n          <CellSpacing>1</CellSpacing>\r\n          <ItemForeColor>Black</ItemForeColor>\r\n          <ItemBackColor>#DEDFDE</ItemBackColor>\r\n          <SelItemForeColor>White</SelItemForeColor>\r\n          <SelItemBackColor>#9471DE</SelItemBackColor>\r\n          <SelItemFont>1</SelItemFont>\r\n          <HeaderForeColor>#E7E7FF</HeaderForeColor>\r\n          <HeaderBackColor>#4A3C8C</HeaderBackColor>\r\n          <HeaderFont>1</HeaderFont>\r\n          <FooterForeColor>Black</FooterForeColor>\r\n          <FooterBackColor>#C6C3C6</FooterBackColor>\r\n          <PagerForeColor>Black</PagerForeColor>\r\n          <PagerBackColor>#C6C3C6</PagerBackColor>\r\n          <PagerAlign>3</PagerAlign>\r\n        </Scheme>\r\n        <Scheme>\r\n          <SchemeName>BDLScheme_Simple1</SchemeName>\r\n          <ForeColor>Black</ForeColor>\r\n          <BackColor>White</BackColor>\r\n          <BorderColor>#999999</BorderColor>\r\n          <BorderWidth>1px</BorderWidth>\r\n          <BorderStyle>4</BorderStyle>\r\n          <GridLines>2</GridLines>\r\n          <CellPadding>3</CellPadding>\r\n          <CellSpacing>0</CellSpacing>\r\n          <AltItemBackColor>#CCCCCC</AltItemBackColor>\r\n          <SelItemForeColor>White</SelItemForeColor>\r\n          <SelItemBackColor>#000099</SelItemBackColor>\r\n          <SelItemFont>1</SelItemFont>\r\n          <HeaderForeColor>White</HeaderForeColor>\r\n          <HeaderBackColor>Black</HeaderBackColor>\r\n          <HeaderFont>1</HeaderFont>\r\n          <FooterBackColor>#CCCCCC</FooterBackColor>\r\n          <PagerForeColor>Black</PagerForeColor>\r\n          <PagerBackColor>#999999</PagerBackColor>\r\n          <PagerAlign>2</PagerAlign>\r\n        </Scheme>\r\n        <Scheme>\r\n          <SchemeName>BDLScheme_Simple2</SchemeName>\r\n          <ForeColor>Black</ForeColor>\r\n          <BackColor>#CCCCCC</BackColor>\r\n          <BorderColor>#999999</BorderColor>\r\n          <BorderWidth>3px</BorderWidth>\r\n          <BorderStyle>4</BorderStyle>\r\n          <GridLines>3</GridLines>\r\n          <CellPadding>4</CellPadding>\r\n          <CellSpacing>2</CellSpacing>\r\n          <ItemBackColor>White</ItemBackColor>\r\n          <SelItemForeColor>White</SelItemForeColor>\r\n          <SelItemBackColor>#000099</SelItemBackColor>\r\n          <SelItemFont>1</SelItemFont>\r\n          <HeaderForeColor>White</HeaderForeColor>\r\n          <HeaderBackColor>Black</HeaderBackColor>\r\n          <HeaderFont>1</HeaderFont>\r\n          <FooterBackColor>#CCCCCC</FooterBackColor>\r\n          <PagerForeColor>Black</PagerForeColor>\r\n          <PagerBackColor>#CCCCCC</PagerBackColor>\r\n          <PagerAlign>1</PagerAlign>\r\n          <PagerMode>1</PagerMode>\r\n        </Scheme>\r\n        <Scheme>\r\n          <SchemeName>BDLScheme_Simple3</SchemeName>\r\n          <BackColor>White</BackColor>\r\n          <BorderColor>#336666</BorderColor>\r\n          <BorderWidth>3px</BorderWidth>\r\n          <BorderStyle>5</BorderStyle>\r\n          <GridLines>1</GridLines>\r\n          <CellPadding>4</CellPadding>\r\n          <CellSpacing>0</CellSpacing>\r\n          <ItemForeColor>#333333</ItemForeColor>\r\n          <ItemBackColor>White</ItemBackColor>\r\n          <SelItemForeColor>White</SelItemForeColor>\r\n          <SelItemBackColor>#339966</SelItemBackColor>\r\n          <SelItemFont>1</SelItemFont>\r\n          <HeaderForeColor>White</HeaderForeColor>\r\n          <HeaderBackColor>#336666</HeaderBackColor>\r\n          <HeaderFont>1</HeaderFont>\r\n          <FooterForeColor>#333333</FooterForeColor>\r\n          <FooterBackColor>White</FooterBackColor>\r\n          <PagerForeColor>White</PagerForeColor>\r\n          <PagerBackColor>#336666</PagerBackColor>\r\n          <PagerAlign>2</PagerAlign>\r\n          <PagerMode>1</PagerMode>\r\n        </Scheme>\r\n        <Scheme>\r\n          <SchemeName>BDLScheme_Classic1</SchemeName>\r\n          <ForeColor>Black</ForeColor>\r\n          <BackColor>White</BackColor>\r\n          <BorderColor>#CCCCCC</BorderColor>\r\n          <BorderWidth>1px</BorderWidth>\r\n          <BorderStyle>1</BorderStyle>\r\n          <GridLines>1</GridLines>\r\n          <CellPadding>4</CellPadding>\r\n          <CellSpacing>0</CellSpacing>\r\n          <SelItemForeColor>White</SelItemForeColor>\r\n          <SelItemBackColor>#CC3333</SelItemBackColor>\r\n          <SelItemFont>1</SelItemFont>\r\n          <HeaderForeColor>White</HeaderForeColor>\r\n          <HeaderBackColor>#333333</HeaderBackColor>\r\n          <HeaderFont>1</HeaderFont>\r\n          <FooterForeColor>Black</FooterForeColor>\r\n          <FooterBackColor>#CCCC99</FooterBackColor>\r\n          <PagerForeColor>Black</PagerForeColor>\r\n          <PagerBackColor>White</PagerBackColor>\r\n          <PagerAlign>3</PagerAlign>\r\n        </Scheme>\r\n        <Scheme>\r\n          <SchemeName>BDLScheme_Classic2</SchemeName>\r\n          <ForeColor>Black</ForeColor>\r\n          <BackColor>White</BackColor>\r\n          <BorderColor>#DEDFDE</BorderColor>\r\n          <BorderWidth>1px</BorderWidth>\r\n          <BorderStyle>1</BorderStyle>\r\n          <GridLines>2</GridLines>\r\n          <CellPadding>4</CellPadding>\r\n          <CellSpacing>0</CellSpacing>\r\n          <ItemBackColor>#F7F7DE</ItemBackColor>\r\n       [...string is too long...]"));
				}
				return DataListDesigner._autoFormats;
			}
		}

		// Token: 0x1700016C RID: 364
		// (get) Token: 0x0600060D RID: 1549 RVA: 0x0002051C File Offset: 0x0001E71C
		protected bool TemplatesExist
		{
			get
			{
				DataList dataList = (DataList)base.ViewControl;
				ITemplate itemTemplate = dataList.ItemTemplate;
				if (itemTemplate != null)
				{
					string textFromTemplate = base.GetTextFromTemplate(itemTemplate);
					return textFromTemplate != null && textFromTemplate.Length > 0;
				}
				return false;
			}
		}

		// Token: 0x0600060E RID: 1550 RVA: 0x0002055C File Offset: 0x0001E75C
		private void CreateDefaultTemplate()
		{
			string text = string.Empty;
			StringBuilder stringBuilder = new StringBuilder();
			DataList dataList = (DataList)base.Component;
			IDataSourceViewSchema dataSourceSchema = this.GetDataSourceSchema();
			IDataSourceFieldSchema[] array = null;
			if (dataSourceSchema != null)
			{
				array = dataSourceSchema.GetFields();
			}
			if (array != null && array.Length != 0)
			{
				foreach (IDataSourceFieldSchema dataSourceFieldSchema in array)
				{
					string name = dataSourceFieldSchema.Name;
					char[] array3 = new char[name.Length];
					for (int j = 0; j < name.Length; j++)
					{
						char c = name[j];
						if (char.IsLetterOrDigit(c) || c == '_')
						{
							array3[j] = c;
						}
						else
						{
							array3[j] = '_';
						}
					}
					string text2 = new string(array3);
					stringBuilder.Append(string.Format(CultureInfo.InvariantCulture, "{0}: <asp:Label Text='<%# {1} %>' runat=\"server\" id=\"{2}Label\"/><br />", new object[]
					{
						name,
						DesignTimeDataBinding.CreateEvalExpression(name, string.Empty),
						text2
					}));
					stringBuilder.Append(Environment.NewLine);
					if (dataSourceFieldSchema.PrimaryKey && dataList.DataKeyField.Length == 0)
					{
						dataList.DataKeyField = name;
					}
				}
				stringBuilder.Append("<br />");
				stringBuilder.Append(Environment.NewLine);
				text = stringBuilder.ToString();
			}
			if (text != null && text.Length > 0)
			{
				try
				{
					dataList.ItemTemplate = base.GetTemplateFromText(text, dataList.ItemTemplate);
				}
				catch
				{
				}
			}
		}

		// Token: 0x0600060F RID: 1551 RVA: 0x000206DC File Offset: 0x0001E8DC
		[Obsolete("Use of this method is not recommended because template editing is handled in ControlDesigner. To support template editing expose template data in the TemplateGroups property and call SetViewFlags(ViewFlags.TemplateEditing, true). http://go.microsoft.com/fwlink/?linkid=14202")]
		protected override ITemplateEditingFrame CreateTemplateEditingFrame(TemplateEditingVerb verb)
		{
			ITemplateEditingService templateEditingService = (ITemplateEditingService)this.GetService(typeof(ITemplateEditingService));
			DataList dataList = (DataList)base.ViewControl;
			string[] templateNames = null;
			Style[] templateStyles = null;
			switch (verb.Index)
			{
			case 0:
				templateNames = DataListDesigner.ItemTemplateNames;
				templateStyles = new Style[]
				{
					dataList.ItemStyle,
					dataList.AlternatingItemStyle,
					dataList.SelectedItemStyle,
					dataList.EditItemStyle
				};
				break;
			case 1:
				templateNames = DataListDesigner.HeaderFooterTemplateNames;
				templateStyles = new Style[]
				{
					dataList.HeaderStyle,
					dataList.FooterStyle
				};
				break;
			case 2:
				templateNames = DataListDesigner.SeparatorTemplateNames;
				templateStyles = new Style[]
				{
					dataList.SeparatorStyle
				};
				break;
			}
			return templateEditingService.CreateFrame(this, verb.Text, templateNames, dataList.ControlStyle, templateStyles);
		}

		// Token: 0x06000610 RID: 1552 RVA: 0x000207AF File Offset: 0x0001E9AF
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				this.DisposeTemplateVerbs();
			}
			base.Dispose(disposing);
		}

		// Token: 0x06000611 RID: 1553 RVA: 0x000207C4 File Offset: 0x0001E9C4
		private void DisposeTemplateVerbs()
		{
			if (this.templateVerbs != null)
			{
				for (int i = 0; i < this.templateVerbs.Length; i++)
				{
					this.templateVerbs[i].Dispose();
				}
				this.templateVerbs = null;
				this.templateVerbsDirty = true;
			}
		}

		// Token: 0x06000612 RID: 1554 RVA: 0x00020808 File Offset: 0x0001EA08
		[Obsolete("Use of this method is not recommended because template editing is handled in ControlDesigner. To support template editing expose template data in the TemplateGroups property and call SetViewFlags(ViewFlags.TemplateEditing, true). http://go.microsoft.com/fwlink/?linkid=14202")]
		protected override TemplateEditingVerb[] GetCachedTemplateEditingVerbs()
		{
			if (this.templateVerbsDirty)
			{
				this.DisposeTemplateVerbs();
				this.templateVerbs = new TemplateEditingVerb[3];
				this.templateVerbs[0] = new TemplateEditingVerb(SR.GetString("DataList_ItemTemplates"), 0, this);
				this.templateVerbs[1] = new TemplateEditingVerb(SR.GetString("DataList_HeaderFooterTemplates"), 1, this);
				this.templateVerbs[2] = new TemplateEditingVerb(SR.GetString("DataList_SeparatorTemplate"), 2, this);
				this.templateVerbsDirty = false;
			}
			return this.templateVerbs;
		}

		// Token: 0x06000613 RID: 1555 RVA: 0x00020888 File Offset: 0x0001EA88
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

		// Token: 0x06000614 RID: 1556 RVA: 0x00020908 File Offset: 0x0001EB08
		public override string GetDesignTimeHtml()
		{
			bool templatesExist = this.TemplatesExist;
			string result = null;
			if (templatesExist)
			{
				DataList dataList = (DataList)base.ViewControl;
				bool flag = false;
				DesignerDataSourceView designerView = base.DesignerView;
				IEnumerable dataSource;
				if (designerView == null)
				{
					dataSource = base.GetDesignTimeDataSource(5, out flag);
				}
				else
				{
					try
					{
						dataSource = designerView.GetDesignTimeData(5, out flag);
					}
					catch (Exception ex)
					{
						if (base.Component.Site != null)
						{
							IComponentDesignerDebugService componentDesignerDebugService = (IComponentDesignerDebugService)base.Component.Site.GetService(typeof(IComponentDesignerDebugService));
							if (componentDesignerDebugService != null)
							{
								componentDesignerDebugService.Fail(SR.GetString("DataSource_DebugService_FailedCall", new object[]
								{
									"DesignerDataSourceView.GetDesignTimeData",
									ex.Message
								}));
							}
						}
						dataSource = null;
					}
				}
				bool flag2 = false;
				string text = null;
				bool flag3 = false;
				string dataSourceID = null;
				try
				{
					dataList.DataSource = dataSource;
					text = dataList.DataKeyField;
					if (text.Length != 0)
					{
						flag2 = true;
						dataList.DataKeyField = string.Empty;
					}
					dataSourceID = dataList.DataSourceID;
					dataList.DataSourceID = string.Empty;
					flag3 = true;
					dataList.DataBind();
					return base.GetDesignTimeHtml();
				}
				catch (Exception e)
				{
					return this.GetErrorDesignTimeHtml(e);
				}
				finally
				{
					dataList.DataSource = null;
					if (flag2)
					{
						dataList.DataKeyField = text;
					}
					if (flag3)
					{
						dataList.DataSourceID = dataSourceID;
					}
				}
			}
			result = this.GetEmptyDesignTimeHtml();
			return result;
		}

		// Token: 0x06000615 RID: 1557 RVA: 0x00020A74 File Offset: 0x0001EC74
		protected override string GetEmptyDesignTimeHtml()
		{
			string @string;
			if (base.CanEnterTemplateMode)
			{
				@string = SR.GetString("DataList_NoTemplatesInst");
			}
			else
			{
				@string = SR.GetString("DataList_NoTemplatesInst2");
			}
			return base.CreatePlaceHolderDesignTimeHtml(@string);
		}

		// Token: 0x06000616 RID: 1558 RVA: 0x0001FC84 File Offset: 0x0001DE84
		protected override string GetErrorDesignTimeHtml(Exception e)
		{
			return base.CreatePlaceHolderDesignTimeHtml(SR.GetString("Control_ErrorRendering"));
		}

		// Token: 0x06000617 RID: 1559 RVA: 0x0001FC96 File Offset: 0x0001DE96
		[Obsolete("Use of this method is not recommended because template editing is handled in ControlDesigner. To support template editing expose template data in the TemplateGroups property and call SetViewFlags(ViewFlags.TemplateEditing, true). http://go.microsoft.com/fwlink/?linkid=14202")]
		public override string GetTemplateContainerDataItemProperty(string templateName)
		{
			return "DataItem";
		}

		// Token: 0x06000618 RID: 1560 RVA: 0x00020AA8 File Offset: 0x0001ECA8
		[Obsolete("Use of this method is not recommended because template editing is handled in ControlDesigner. To support template editing expose template data in the TemplateGroups property and call SetViewFlags(ViewFlags.TemplateEditing, true). http://go.microsoft.com/fwlink/?linkid=14202")]
		public override string GetTemplateContent(ITemplateEditingFrame editingFrame, string templateName, out bool allowEditing)
		{
			allowEditing = true;
			DataList dataList = (DataList)base.Component;
			ITemplate template = null;
			string result = string.Empty;
			switch (editingFrame.Verb.Index)
			{
			case 0:
				if (templateName.Equals(DataListDesigner.ItemTemplateNames[0]))
				{
					template = dataList.ItemTemplate;
				}
				else if (templateName.Equals(DataListDesigner.ItemTemplateNames[1]))
				{
					template = dataList.AlternatingItemTemplate;
				}
				else if (templateName.Equals(DataListDesigner.ItemTemplateNames[2]))
				{
					template = dataList.SelectedItemTemplate;
				}
				else if (templateName.Equals(DataListDesigner.ItemTemplateNames[3]))
				{
					template = dataList.EditItemTemplate;
				}
				break;
			case 1:
				if (templateName.Equals(DataListDesigner.HeaderFooterTemplateNames[0]))
				{
					template = dataList.HeaderTemplate;
				}
				else if (templateName.Equals(DataListDesigner.HeaderFooterTemplateNames[1]))
				{
					template = dataList.FooterTemplate;
				}
				break;
			case 2:
				template = dataList.SeparatorTemplate;
				break;
			}
			if (template != null)
			{
				result = base.GetTextFromTemplate(template);
			}
			return result;
		}

		// Token: 0x06000619 RID: 1561 RVA: 0x00020B92 File Offset: 0x0001ED92
		public override void Initialize(IComponent component)
		{
			ControlDesigner.VerifyInitializeArgument(component, typeof(DataList));
			base.Initialize(component);
		}

		// Token: 0x0600061A RID: 1562 RVA: 0x00020BAB File Offset: 0x0001EDAB
		protected override void OnSchemaRefreshed()
		{
			if (base.InTemplateModeInternal)
			{
				return;
			}
			ControlDesigner.InvokeTransactedChange(base.Component, new TransactedChangeCallback(this.RefreshSchemaCallback), null, SR.GetString("DataList_RefreshSchemaTransaction"));
		}

		// Token: 0x0600061B RID: 1563 RVA: 0x00020BD8 File Offset: 0x0001EDD8
		protected override void OnTemplateEditingVerbsChanged()
		{
			this.templateVerbsDirty = true;
		}

		// Token: 0x0600061C RID: 1564 RVA: 0x00020BE4 File Offset: 0x0001EDE4
		private bool RefreshSchemaCallback(object context)
		{
			DataList dataList = (DataList)base.Component;
			bool flag = dataList.ItemTemplate == null && dataList.EditItemTemplate == null && dataList.AlternatingItemTemplate == null && dataList.SelectedItemTemplate == null;
			IDataSourceViewSchema dataSourceSchema = this.GetDataSourceSchema();
			if (base.DataSourceID.Length > 0 && dataSourceSchema != null)
			{
				if (flag || (!flag && DialogResult.Yes == UIServiceHelper.ShowMessage(base.Component.Site, SR.GetString("DataList_RegenerateTemplates"), SR.GetString("DataList_ClearTemplatesCaption"), MessageBoxButtons.YesNo)))
				{
					dataList.ItemTemplate = null;
					dataList.EditItemTemplate = null;
					dataList.AlternatingItemTemplate = null;
					dataList.SelectedItemTemplate = null;
					dataList.DataKeyField = string.Empty;
					this.CreateDefaultTemplate();
					this.UpdateDesignTimeHtml();
				}
			}
			else if (flag || (!flag && DialogResult.Yes == UIServiceHelper.ShowMessage(base.Component.Site, SR.GetString("DataList_ClearTemplates"), SR.GetString("DataList_ClearTemplatesCaption"), MessageBoxButtons.YesNo)))
			{
				dataList.ItemTemplate = null;
				dataList.EditItemTemplate = null;
				dataList.AlternatingItemTemplate = null;
				dataList.SelectedItemTemplate = null;
				dataList.DataKeyField = string.Empty;
				this.UpdateDesignTimeHtml();
			}
			return true;
		}

		// Token: 0x0600061D RID: 1565 RVA: 0x00020D00 File Offset: 0x0001EF00
		[Obsolete("Use of this method is not recommended because template editing is handled in ControlDesigner. To support template editing expose template data in the TemplateGroups property and call SetViewFlags(ViewFlags.TemplateEditing, true). http://go.microsoft.com/fwlink/?linkid=14202")]
		public override void SetTemplateContent(ITemplateEditingFrame editingFrame, string templateName, string templateContent)
		{
			ITemplate template = null;
			DataList dataList = (DataList)base.Component;
			if (templateContent != null && templateContent.Length != 0)
			{
				ITemplate currentTemplate = null;
				switch (editingFrame.Verb.Index)
				{
				case 0:
					if (templateName.Equals(DataListDesigner.ItemTemplateNames[0]))
					{
						currentTemplate = dataList.ItemTemplate;
					}
					else if (templateName.Equals(DataListDesigner.ItemTemplateNames[1]))
					{
						currentTemplate = dataList.AlternatingItemTemplate;
					}
					else if (templateName.Equals(DataListDesigner.ItemTemplateNames[2]))
					{
						currentTemplate = dataList.SelectedItemTemplate;
					}
					else if (templateName.Equals(DataListDesigner.ItemTemplateNames[3]))
					{
						currentTemplate = dataList.EditItemTemplate;
					}
					break;
				case 1:
					if (templateName.Equals(DataListDesigner.HeaderFooterTemplateNames[0]))
					{
						currentTemplate = dataList.HeaderTemplate;
					}
					else if (templateName.Equals(DataListDesigner.HeaderFooterTemplateNames[1]))
					{
						currentTemplate = dataList.FooterTemplate;
					}
					break;
				case 2:
					currentTemplate = dataList.SeparatorTemplate;
					break;
				}
				template = base.GetTemplateFromText(templateContent, currentTemplate);
			}
			switch (editingFrame.Verb.Index)
			{
			case 0:
				if (templateName.Equals(DataListDesigner.ItemTemplateNames[0]))
				{
					dataList.ItemTemplate = template;
					return;
				}
				if (templateName.Equals(DataListDesigner.ItemTemplateNames[1]))
				{
					dataList.AlternatingItemTemplate = template;
					return;
				}
				if (templateName.Equals(DataListDesigner.ItemTemplateNames[2]))
				{
					dataList.SelectedItemTemplate = template;
					return;
				}
				if (templateName.Equals(DataListDesigner.ItemTemplateNames[3]))
				{
					dataList.EditItemTemplate = template;
					return;
				}
				break;
			case 1:
				if (templateName.Equals(DataListDesigner.HeaderFooterTemplateNames[0]))
				{
					dataList.HeaderTemplate = template;
					return;
				}
				if (templateName.Equals(DataListDesigner.HeaderFooterTemplateNames[1]))
				{
					dataList.FooterTemplate = template;
					return;
				}
				break;
			case 2:
				dataList.SeparatorTemplate = template;
				break;
			default:
				return;
			}
		}

		// Token: 0x04000374 RID: 884
		internal static TraceSwitch DataListDesignerSwitch = new TraceSwitch("DATALISTDESIGNER", "Enable DataList designer general purpose traces.");

		// Token: 0x04000375 RID: 885
		private const string templateFieldString = "{0}: <asp:Label Text='<%# {1} %>' runat=\"server\" id=\"{2}Label\"/><br />";

		// Token: 0x04000376 RID: 886
		private const string breakString = "<br />";

		// Token: 0x04000377 RID: 887
		private const int HeaderFooterTemplates = 1;

		// Token: 0x04000378 RID: 888
		private const int ItemTemplates = 0;

		// Token: 0x04000379 RID: 889
		private const int SeparatorTemplate = 2;

		// Token: 0x0400037A RID: 890
		private static string[] HeaderFooterTemplateNames = new string[]
		{
			"HeaderTemplate",
			"FooterTemplate"
		};

		// Token: 0x0400037B RID: 891
		private const int IDX_HEADER_TEMPLATE = 0;

		// Token: 0x0400037C RID: 892
		private const int IDX_FOOTER_TEMPLATE = 1;

		// Token: 0x0400037D RID: 893
		private static string[] ItemTemplateNames = new string[]
		{
			"ItemTemplate",
			"AlternatingItemTemplate",
			"SelectedItemTemplate",
			"EditItemTemplate"
		};

		// Token: 0x0400037E RID: 894
		private const int IDX_ITEM_TEMPLATE = 0;

		// Token: 0x0400037F RID: 895
		private const int IDX_ALTITEM_TEMPLATE = 1;

		// Token: 0x04000380 RID: 896
		private const int IDX_SELITEM_TEMPLATE = 2;

		// Token: 0x04000381 RID: 897
		private const int IDX_EDITITEM_TEMPLATE = 3;

		// Token: 0x04000382 RID: 898
		private static string[] SeparatorTemplateNames = new string[]
		{
			"SeparatorTemplate"
		};

		// Token: 0x04000383 RID: 899
		private const int IDX_SEPARATOR_TEMPLATE = 0;

		// Token: 0x04000384 RID: 900
		private TemplateEditingVerb[] templateVerbs;

		// Token: 0x04000385 RID: 901
		private bool templateVerbsDirty;

		// Token: 0x04000386 RID: 902
		private static DesignerAutoFormatCollection _autoFormats;
	}
}
