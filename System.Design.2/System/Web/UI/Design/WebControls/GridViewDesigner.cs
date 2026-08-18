using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Design;
using System.Globalization;
using System.Web.UI.Design.Util;
using System.Web.UI.WebControls;
using System.Windows.Forms;

namespace System.Web.UI.Design.WebControls
{
	// Token: 0x020000CD RID: 205
	public class GridViewDesigner : DataBoundControlDesigner
	{
		// Token: 0x170001A4 RID: 420
		// (get) Token: 0x060006E1 RID: 1761 RVA: 0x00025C6C File Offset: 0x00023E6C
		public override DesignerActionListCollection ActionLists
		{
			get
			{
				DesignerActionListCollection designerActionListCollection = new DesignerActionListCollection();
				designerActionListCollection.AddRange(base.ActionLists);
				if (this._actionLists == null)
				{
					this._actionLists = new GridViewActionList(this);
				}
				bool inTemplateMode = base.InTemplateMode;
				int selectedFieldIndex = this.SelectedFieldIndex;
				this.UpdateFieldsCurrentState();
				this._actionLists.AllowRemoveField = (((GridView)base.Component).Columns.Count > 0 && selectedFieldIndex >= 0 && !inTemplateMode);
				this._actionLists.AllowMoveLeft = (((GridView)base.Component).Columns.Count > 0 && selectedFieldIndex > 0 && !inTemplateMode);
				this._actionLists.AllowMoveRight = (((GridView)base.Component).Columns.Count > 0 && selectedFieldIndex >= 0 && ((GridView)base.Component).Columns.Count > selectedFieldIndex + 1 && !inTemplateMode);
				DesignerDataSourceView designerView = base.DesignerView;
				this._actionLists.AllowPaging = (!inTemplateMode && designerView != null);
				this._actionLists.AllowSorting = (!inTemplateMode && designerView != null && designerView.CanSort);
				this._actionLists.AllowEditing = (!inTemplateMode && designerView != null && designerView.CanUpdate);
				this._actionLists.AllowDeleting = (!inTemplateMode && designerView != null && designerView.CanDelete);
				this._actionLists.AllowSelection = (!inTemplateMode && designerView != null);
				designerActionListCollection.Add(this._actionLists);
				return designerActionListCollection;
			}
		}

		// Token: 0x170001A5 RID: 421
		// (get) Token: 0x060006E2 RID: 1762 RVA: 0x00025DED File Offset: 0x00023FED
		public override DesignerAutoFormatCollection AutoFormats
		{
			get
			{
				if (GridViewDesigner._autoFormats == null)
				{
					GridViewDesigner._autoFormats = ControlDesigner.CreateAutoFormats(AutoFormatSchemes.GRIDVIEW_SCHEME_NAMES, (string schemeName) => new GridViewAutoFormat(schemeName, "<Schemes>\r\n        <xsd:schema id=\"Schemes\" xmlns=\"\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns:msdata=\"urn:schemas-microsoft-com:xml-msdata\">\r\n          <xsd:element name=\"Scheme\">\r\n            <xsd:complexType>\r\n              <xsd:all>\r\n                <xsd:element name=\"SchemeName\" type=\"xsd:string\"/>\r\n                <xsd:element name=\"ForeColor\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n                <xsd:element name=\"BackColor\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n                <xsd:element name=\"BorderColor\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n                <xsd:element name=\"BorderWidth\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n                <xsd:element name=\"BorderStyle\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n                <xsd:element name=\"GridLines\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n                <xsd:element name=\"CellPadding\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n                <xsd:element name=\"CellSpacing\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n                <xsd:element name=\"ItemForeColor\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n                <xsd:element name=\"ItemBackColor\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n                <xsd:element name=\"ItemFont\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n                <xsd:element name=\"AltItemForeColor\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n                <xsd:element name=\"AltItemBackColor\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n                <xsd:element name=\"AltItemFont\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n                <xsd:element name=\"SelItemForeColor\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n                <xsd:element name=\"SelItemBackColor\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n                <xsd:element name=\"SelItemFont\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n                <xsd:element name=\"HeaderForeColor\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n                <xsd:element name=\"HeaderBackColor\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n                <xsd:element name=\"HeaderFont\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n                <xsd:element name=\"FooterForeColor\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n                <xsd:element name=\"FooterBackColor\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n                <xsd:element name=\"FooterFont\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n                <xsd:element name=\"PagerForeColor\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n                <xsd:element name=\"PagerBackColor\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n                <xsd:element name=\"PagerFont\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n                <xsd:element name=\"PagerAlign\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n                <xsd:element name=\"PagerButtons\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n                <xsd:element name=\"EditItemForeColor\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n                <xsd:element name=\"EditItemBackColor\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n                <xsd:element name=\"EditItemFont\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n                <xsd:element name=\"SortedDescendingCellBackColor\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n                <xsd:element name=\"SortedDescendingHeaderBackColor\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n                <xsd:element name=\"SortedAscendingCellBackColor\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n                <xsd:element name=\"SortedAscendingHeaderBackColor\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n              </xsd:all>\r\n            </xsd:complexType>\r\n          </xsd:element>\r\n          <xsd:element name=\"Schemes\" msdata:IsDataSet=\"true\">\r\n            <xsd:complexType>\r\n              <xsd:choice maxOccurs=\"unbounded\">\r\n                <xsd:element ref=\"Scheme\"/>\r\n              </xsd:choice>\r\n            </xsd:complexType>\r\n          </xsd:element>\r\n        </xsd:schema>\r\n        <Scheme>\r\n          <SchemeName>BDLScheme_Empty</SchemeName>\r\n        </Scheme>\r\n        <Scheme>\r\n          <SchemeName>BDLScheme_Consistent1</SchemeName>\r\n          <AltItemBackColor>White</AltItemBackColor>\r\n          <GridLines>0</GridLines>\r\n          <CellPadding>4</CellPadding>\r\n          <ForeColor>#333333</ForeColor>\r\n          <ItemForeColor>#333333</ItemForeColor>\r\n          <ItemBackColor>#FFFBD6</ItemBackColor>\r\n          <SelItemForeColor>Navy</SelItemForeColor>\r\n          <SelItemBackColor>#FFCC66</SelItemBackColor>\r\n          <SelItemFont>1</SelItemFont>\r\n          <HeaderForeColor>White</HeaderForeColor>\r\n          <HeaderBackColor>#990000</HeaderBackColor>\r\n          <HeaderFont>1</HeaderFont>\r\n          <FooterForeColor>White</FooterForeColor>\r\n          <FooterBackColor>#990000</FooterBackColor>\r\n          <FooterFont>1</FooterFont>\r\n          <PagerForeColor>#333333</PagerForeColor>\r\n          <PagerBackColor>#FFCC66</PagerBackColor>\r\n          <PagerAlign>2</PagerAlign>\r\n          <SortedDescendingCellBackColor>#FCF6C0</SortedDescendingCellBackColor>\r\n          <SortedDescendingHeaderBackColor>#820000</SortedDescendingHeaderBackColor>\r\n          <SortedAscendingCellBackColor>#FDF5AC</SortedAscendingCellBackColor>\r\n          <SortedAscendingHeaderBackColor>#4D0000</SortedAscendingHeaderBackColor>\r\n        </Scheme>\r\n        <Scheme>\r\n          <SchemeName>BDLScheme_Consistent2</SchemeName>\r\n            <AltItemBackColor>White</AltItemBackColor>\r\n            <GridLines>0</GridLines>\r\n            <CellPadding>4</CellPadding>\r\n            <ForeColor>#333333</ForeColor>\r\n            <ItemBackColor>#EFF3FB</ItemBackColor>\r\n            <SelItemForeColor>#333333</SelItemForeColor>\r\n            <SelItemBackColor>#D1DDF1</SelItemBackColor>\r\n            <SelItemFont>1</SelItemFont>\r\n            <HeaderForeColor>White</HeaderForeColor>\r\n            <HeaderBackColor>#507CD1</HeaderBackColor>\r\n            <HeaderFont>1</HeaderFont>\r\n            <FooterForeColor>White</FooterForeColor>\r\n            <FooterBackColor>#507CD1</FooterBackColor>\r\n            <FooterFont>1</FooterFont>\r\n            <PagerForeColor>White</PagerForeColor>\r\n            <PagerBackColor>#2461BF</PagerBackColor>\r\n            <PagerAlign>2</PagerAlign>\r\n            <EditItemBackColor>#2461BF</EditItemBackColor>\r\n            <SortedDescendingCellBackColor>#E9EBEF</SortedDescendingCellBackColor>\r\n            <SortedDescendingHeaderBackColor>#4870BE</SortedDescendingHeaderBackColor>\r\n            <SortedAscendingCellBackColor>#F5F7FB</SortedAscendingCellBackColor>\r\n            <SortedAscendingHeaderBackColor>#6D95E1</SortedAscendingHeaderBackColor>\r\n        </Scheme>\r\n        <Scheme>\r\n          <SchemeName>BDLScheme_Consistent3</SchemeName>\r\n            <AltItemBackColor>White</AltItemBackColor>\r\n            <GridLines>0</GridLines>\r\n            <CellPadding>4</CellPadding>\r\n            <ForeColor>#333333</ForeColor>\r\n            <ItemBackColor>#E3EAEB</ItemBackColor>\r\n            <SelItemForeColor>#333333</SelItemForeColor>\r\n            <SelItemBackColor>#C5BBAF</SelItemBackColor>\r\n            <SelItemFont>1</SelItemFont>\r\n            <HeaderForeColor>White</HeaderForeColor>\r\n            <HeaderBackColor>#1C5E55</HeaderBackColor>\r\n            <HeaderFont>1</HeaderFont>\r\n            <FooterForeColor>White</FooterForeColor>\r\n            <FooterBackColor>#1C5E55</FooterBackColor>\r\n            <FooterFont>1</FooterFont>\r\n            <PagerForeColor>White</PagerForeColor>\r\n            <PagerBackColor>#666666</PagerBackColor>\r\n            <PagerAlign>2</PagerAlign>\r\n            <EditItemBackColor>#7C6F57</EditItemBackColor>\r\n            <SortedDescendingCellBackColor>#D4DFE1</SortedDescendingCellBackColor>\r\n            <SortedDescendingHeaderBackColor>#15524A</SortedDescendingHeaderBackColor>\r\n            <SortedAscendingCellBackColor>#F8FAFA</SortedAscendingCellBackColor>\r\n            <SortedAscendingHeaderBackColor>#246B61</SortedAscendingHeaderBackColor>\r\n        </Scheme>\r\n        <Scheme>\r\n          <SchemeName>BDLScheme_Consistent4</SchemeName>\r\n            <AltItemBackColor>White</AltItemBackColor>\r\n            <AltItemForeColor>#284775</AltItemForeColor>\r\n            <GridLines>0</GridLines>\r\n            <CellPadding>4</CellPadding>\r\n            <ForeColor>#333333</ForeColor>\r\n            <ItemForeColor>#333333</ItemForeColor>\r\n            <ItemBackColor>#F7F6F3</ItemBackColor>\r\n            <SelItemForeColor>#333333</SelItemForeColor>\r\n            <SelItemBackColor>#E2DED6</SelItemBackColor>\r\n            <SelItemFont>1</SelItemFont>\r\n            <HeaderForeColor>White</HeaderForeColor>\r\n            <HeaderBackColor>#5D7B9D</HeaderBackColor>\r\n            <HeaderFont>1</HeaderFont>\r\n            <FooterForeColor>White</FooterForeColor>\r\n            <FooterBackColor>#5D7B9D</FooterBackColor>\r\n            <FooterFont>1</FooterFont>\r\n            <PagerForeColor>White</PagerForeColor>\r\n            <PagerBackColor>#284775</PagerBackColor>\r\n            <PagerAlign>2</PagerAlign>\r\n            <EditItemBackColor>#999999</EditItemBackColor>\r\n            <SortedDescendingCellBackColor>#FFFDF8</SortedDescendingCellBackColor>\r\n            <SortedDescendingHeaderBackColor>#6F8DAE</SortedDescendingHeaderBackColor>\r\n            <SortedAscendingCellBackColor>#E9E7E2</SortedAscendingCellBackColor>\r\n            <SortedAscendingHeaderBackColor>#506C8C</SortedAscendingHeaderBackColor>\r\n        </Scheme>\r\n        <Scheme>\r\n          <SchemeName>BDLScheme_Colorful1</SchemeName>\r\n          <BackColor>White</BackColor>\r\n          <BorderColor>#CC9966</BorderColor>\r\n          <BorderWidth>1px</BorderWidth>\r\n          <BorderStyle>1</BorderStyle>\r\n          <GridLines>3</GridLines>\r\n          <CellPadding>4</CellPadding>\r\n          <CellSpacing>0</CellSpacing>\r\n          <ItemForeColor>#330099</ItemForeColor>\r\n          <ItemBackColor>White</ItemBackColor>\r\n          <SelItemForeColor>#663399</SelItemForeColor>\r\n          <SelItemBackColor>#FFCC66</SelItemBackColor>\r\n          <SelItemFont>1</SelItemFont>\r\n          <HeaderForeColor>#FFFFCC</HeaderForeColor>\r\n          <HeaderBackColor>#990000</HeaderBackColor>\r\n          <HeaderFont>1</HeaderFont>\r\n          <FooterForeColor>#330099</FooterForeColor>\r\n          <FooterBackColor>#FFFFCC</FooterBackColor>\r\n          <PagerForeColor>#330099</PagerForeColor>\r\n          <PagerBackColor>#FFFFCC</PagerBackColor>\r\n          <PagerAlign>2</PagerAlign>\r\n          <SortedDescendingCellBackColor>#F6F0C0</SortedDescendingCellBackColor>\r\n          <SortedDescendingHeaderBackColor>#7E0000</SortedDescendingHeaderBackColor>\r\n          <SortedAscendingCellBackColor>#FEFCEB</SortedAscendingCellBackColor>\r\n          <SortedAscendingHeaderBackColor>#AF0101</SortedAscendingHeaderBackColor>\r\n        </Scheme>\r\n        <Scheme>\r\n          <SchemeName>BDLScheme_Colorful2</SchemeName>\r\n          <BackColor>White</BackColor>\r\n          <BorderColor>#3366CC</BorderColor>\r\n          <BorderWidth>1px</BorderWidth>\r\n          <BorderStyle>1</BorderStyle>\r\n          <GridLines>3</GridLines>\r\n          <CellPadding>4</CellPadding>\r\n          <CellSpacing>0</CellSpacing>\r\n          <ItemForeColor>#003399</ItemForeColor>\r\n          <ItemBackColor>White</ItemBackColor>\r\n          <SelItemForeColor>#CCFF99</SelItemForeColor>\r\n          <SelItemBackColor>#009999</SelItemBackColor>\r\n          <SelItemFont>1</SelItemFont>\r\n          <HeaderForeColor>#CCCCFF</HeaderForeColor>\r\n          <HeaderBackColor>#003399</HeaderBackColor>\r\n          <HeaderFont>1</HeaderFont>\r\n          <FooterForeColor>#003399</FooterForeColor>\r\n          <FooterBackColor>#99CCCC</FooterBackColor>\r\n          <PagerForeColor>#003399</PagerForeColor>\r\n          <PagerBackColor>#99CCCC</PagerBackColor>\r\n          <PagerAlign>1</PagerAlign>\r\n          <PagerButtons>1</PagerButtons>\r\n          <SortedDescendingCellBackColor>#D6DFDF</SortedDescendingCellBackColor>\r\n          <SortedDescendingHeaderBackColor>#002876</SortedDescendingHeaderBackColor>\r\n          <SortedAscendingCellBackColor>#EDF6F6</SortedAscendingCellBackColor>\r\n          <SortedAscendingHeaderBackColor>#0D4AC4</SortedAscendingHeaderBackColor>\r\n        </Scheme>\r\n        <Scheme>\r\n          <SchemeName>BDLScheme_Colorful3</SchemeName>\r\n          <BackColor>#DEBA84</BackColor>\r\n          <BorderColor>#DEBA84</BorderColor>\r\n          <BorderWidth>1px</BorderWidth>\r\n          <BorderStyle>1</BorderStyle>\r\n          <GridLines>3</GridLines>\r\n          <CellPadding>3</CellPadding>\r\n          <CellSpacing>2</CellSpacing>\r\n          <ItemForeColor>#8C4510</ItemForeColor>\r\n          <ItemBackColor>#FFF7E7</ItemBackColor>\r\n          <SelItemForeColor>White</SelItemForeColor>\r\n          <SelItemBackColor>#738A9C</SelItemBackColor>\r\n          <SelItemFont>1</SelItemFont>\r\n          <HeaderForeColor>White</HeaderForeColor>\r\n          <HeaderBackColor>#A55129</HeaderBackColor>\r\n          <HeaderFont>1</HeaderFont>\r\n          <FooterForeColor>#8C4510</FooterForeColor>\r\n          <FooterBackColor>#F7DFB5</FooterBackColor>\r\n          <PagerForeColor>#8C4510</PagerForeColor>\r\n          <PagerAlign>2</PagerAlign>\r\n          <PagerButtons>1</PagerButtons>\r\n          <SortedDescendingCellBackColor>#F1E5CE</SortedDescendingCellBackColor>\r\n          <SortedDescendingHeaderBackColor>#93451F</SortedDescendingHeaderBackColor>\r\n          <SortedAscendingCellBackColor>#FFF1D4</SortedAscendingCellBackColor>\r\n          <SortedAscendingHeaderBackColor>#B95C30</SortedAscendingHeaderBackColor>\r\n        </Scheme>\r\n        <Scheme>\r\n          <SchemeName>BDLScheme_Colorful4</SchemeName>\r\n          <BackColor>White</BackColor>\r\n          <BorderColor>#E7E7FF</BorderColor>\r\n          <BorderWidth>1px</BorderWidth>\r\n          <BorderStyle>1</BorderStyle>\r\n          <GridLines>1</GridLines>\r\n          <CellPadding>3</CellPadding>\r\n          <CellSpacing>0</CellSpacing>\r\n          <ItemForeColor>#4A3C8C</ItemForeColor>\r\n          <ItemBackColor>#E7E7FF</ItemBackColor>\r\n          <AltItemBackColor>#F7F7F7</AltItemBackColor>\r\n          <SelItemForeColor>#F7F7F7</SelItemForeColor>\r\n          <SelItemBackColor>#738A9C</SelItemBackColor>\r\n          <SelItemFont>1</SelItemFont>\r\n          <HeaderForeColor>#F7F7F7</HeaderForeColor>\r\n          <HeaderBackColor>#4A3C8C</HeaderBackColor>\r\n          <HeaderFont>1</HeaderFont>\r\n          <FooterForeColor>#4A3C8C</FooterForeColor>\r\n          <FooterBackColor>#B5C7DE</FooterBackColor>\r\n          <PagerForeColor>#4A3C8C</PagerForeColor>\r\n          <PagerBackColor>#E7E7FF</PagerBackColor>\r\n          <PagerAlign>3</PagerAlign>\r\n          <PagerButtons>1</PagerButtons>\r\n          <SortedDescendingCellBackColor>#D8D8F0</SortedDescendingCellBackColor>\r\n          <SortedDescendingHeaderBackColor>#3E3277</SortedDescendingHeaderBackColor>\r\n          <SortedAscendingCellBackColor>#F4F4FD</SortedAscendingCellBackColor>\r\n          <SortedAscendingHeaderBackColor>#5A4C9D</SortedAscendingHeaderBackColor>\r\n        </Scheme>\r\n        <Scheme>\r\n          <SchemeName>BDLScheme_Colorful5</SchemeName>\r\n          <ForeColor>Black</ForeColor>\r\n          <BackColor>LightGoldenRodYellow</BackColor>\r\n          <BorderColor>Tan</BorderColor>\r\n          <BorderWidth>1px</BorderWidth>\r\n          <GridLines>0</GridLines>\r\n          <CellPadding>2</CellPadding>\r\n          <AltItemBackColor>PaleGoldenRod</AltItemBackColor>\r\n          <HeaderBackColor>Tan</HeaderBackColor>\r\n          <HeaderFont>1</HeaderFont>\r\n          <FooterBackColor>Tan</FooterBackColor>\r\n          <SelItemBackColor>DarkSlateBlue</SelItemBackColor>\r\n          <SelItemForeColor>GhostWhite</SelItemForeColor>\r\n          <PagerBackColor>PaleGoldenrod</PagerBackColor>\r\n          <PagerForeColor>DarkSlateBlue</PagerForeColor>\r\n          <PagerAlign>2</PagerAlign>\r\n          <SortedDescendingCellBackColor>#E1DB9C</SortedDescendingCellBackColor>\r\n          <SortedDescendingHeaderBackColor>#C2A47B</SortedDescendingHeaderBackColor>\r\n          <SortedAscendingCellBackColor>#FAFAE7</SortedAscendingCellBackColor>\r\n          <SortedAscendingHeaderBackColor>#DAC09E</SortedAscendingHeaderBackColor>\r\n        </Scheme>\r\n        <Scheme>\r\n          <SchemeName>BDLScheme_Professional1</SchemeName>\r\n          <BackColor>White</BackColor>\r\n          <BorderColor>#999999</BorderColor>\r\n          <BorderWidth>1px</BorderWidth>\r\n          <BorderStyle>1</BorderStyle>\r\n          <GridLines>2</GridLines>\r\n          <CellPadding>3</CellPadding>\r\n          <CellSpacing>0</CellSpacing>\r\n          <ItemForeColor>Black</ItemForeColor>\r\n          <ItemBackColor>#EEEEEE</ItemBackColor>\r\n          <AltItemBackColor>#DCDCDC</AltItemBackColor>\r\n          <SelItemForeColor>White</SelItemForeColor>\r\n          <SelItemBackColor>#008A8C</SelItemBackColor>\r\n          <SelItemFont>1</SelItemFont>\r\n          <HeaderForeColor>White</HeaderForeColor>\r\n          <HeaderBackColor>#000084</HeaderBackColor>\r\n          <HeaderFont>1</HeaderFont>\r\n          <FooterForeColor>Black</FooterForeColor>\r\n          <FooterBackColor>#CCCCCC</FooterBackColor>\r\n          <PagerForeColor>Black</PagerForeColor>\r\n          <PagerBackColor>#999999</PagerBackColor>\r\n          <PagerAlign>2</PagerAlign>\r\n          <PagerButtons>1</PagerButtons>\r\n          <SortedDescendingCellBackColor>#CAC9C9</SortedDescendingCellBackColor>\r\n          <SortedDescendingHeaderBackColor>#000065</SortedDescendingHeaderBackColor>\r\n          <SortedAscendingCellBackColor>#F1F1F1</SortedAscendingCellBackColor>\r\n          <SortedAscendingHeaderBackColor>#0000A9</SortedAscendingHeaderBackColor>\r\n        </Scheme>\r\n        <Scheme>\r\n          <SchemeName>BDLScheme_Professional2</SchemeName>\r\n          <BackColor>White</BackColor>\r\n          <BorderColor>#CCCCCC</BorderColor>\r\n          <BorderWidth>1px</BorderWidth>\r\n          <BorderStyle>1</BorderStyle>\r\n          <GridLines>3</GridLines>\r\n          <CellPadding>3</CellPadding>\r\n          <CellSpacing>0</CellSpacing>\r\n          <ItemForeColor>#000066</ItemForeColor>\r\n          <SelItemForeColor>White</SelItemForeColor>\r\n          <SelItemBackColor>#669999</SelItemBackColor>\r\n          <SelItemFont>1</SelItemFont>\r\n          <HeaderForeColor>White</HeaderForeColor>\r\n          <HeaderBackColor>#006699</HeaderBackColor>\r\n          <HeaderFont>1</HeaderFont>\r\n          <FooterForeColor>#000066</FooterForeColor>\r\n          <FooterBackColor>White</FooterBackColor>\r\n          <PagerForeColor>#000066</PagerForeColor>\r\n          <PagerBackColor>White</PagerBackColor>\r\n          <PagerAlign>1</PagerAlign>\r\n          <PagerButtons>1</PagerButtons>\r\n          <SortedDescendingCellBackColor>#CAC9C9</SortedDescendingCellBackColor>\r\n          <SortedDescendingHeaderBackColor>#00547E</SortedDescendingHeaderBackColor>\r\n          <SortedAscendingCellBackColor>#F1F1F1</SortedAscendingCellBackColor>\r\n          <SortedAscendingHeaderBackColor>#007DBB</SortedAscendingHeaderBackColor>\r\n        </Scheme>\r\n        <Scheme>\r\n          <SchemeName>BDLScheme_Professional3</SchemeName>\r\n          <BackColor>White</BackColor>\r\n          <BorderColor>White</BorderColor>\r\n          <BorderWidth>2px</BorderWidth>\r\n          <BorderStyle>7</BorderStyle>\r\n          <GridLines>0</GridLines>\r\n          <CellPadding>3</CellPadding>\r\n          <CellSpacing>1</CellSpacing>\r\n          <ItemForeColor>Black</ItemForeColor>\r\n          <ItemBackColor>#DEDFDE</ItemBackColor>\r\n          <SelItemForeColor>White</SelItemForeColor>\r\n          <SelItemBackColor>#9471DE</SelItemBackColor>\r\n          <SelItemFont>1</SelItemFont>\r\n          <HeaderForeColor>#E7E7FF</HeaderForeColor>\r\n          <HeaderBackColor>#4A3C8C</HeaderBackColor>\r\n          <HeaderFont>1</HeaderFont>\r\n          <FooterForeColor>Black</FooterForeColor>\r\n          <FooterBackColor>#C6C3C6</FooterBackColor>\r\n          <PagerForeColor>Black</PagerForeColor>\r\n          <PagerBackColor>#C6C3C6</PagerBackColor>\r\n          <PagerAlign>3</PagerAlign>\r\n          <SortedDescendingCellBackColor>#CAC9C9</SortedDescendingCellBackColor>\r\n          <SortedDescendingHeaderBackColor>#33276A</SortedDescendingHeaderBackColor>\r\n          <SortedAscendingCellBackColor>#F1F1F1</SortedAscendingCellBackColor>\r\n          <SortedAscendingHeaderBackColor>#594B9C</Sorted[...string is too long...]"));
				}
				return GridViewDesigner._autoFormats;
			}
		}

		// Token: 0x170001A6 RID: 422
		// (get) Token: 0x060006E3 RID: 1763 RVA: 0x00025E29 File Offset: 0x00024029
		// (set) Token: 0x060006E4 RID: 1764 RVA: 0x00025E34 File Offset: 0x00024034
		internal bool EnableDeleting
		{
			get
			{
				return this._currentDeleteState;
			}
			set
			{
				Cursor value2 = Cursor.Current;
				try
				{
					Cursor.Current = Cursors.WaitCursor;
					ControlDesigner.InvokeTransactedChange(base.Component, new TransactedChangeCallback(this.EnableDeletingCallback), value, SR.GetString("GridView_EnableDeletingTransaction"));
				}
				finally
				{
					Cursor.Current = value2;
				}
			}
		}

		// Token: 0x170001A7 RID: 423
		// (get) Token: 0x060006E5 RID: 1765 RVA: 0x00025E94 File Offset: 0x00024094
		// (set) Token: 0x060006E6 RID: 1766 RVA: 0x00025E9C File Offset: 0x0002409C
		internal bool EnableEditing
		{
			get
			{
				return this._currentEditState;
			}
			set
			{
				Cursor value2 = Cursor.Current;
				try
				{
					Cursor.Current = Cursors.WaitCursor;
					ControlDesigner.InvokeTransactedChange(base.Component, new TransactedChangeCallback(this.EnableEditingCallback), value, SR.GetString("GridView_EnableEditingTransaction"));
				}
				finally
				{
					Cursor.Current = value2;
				}
			}
		}

		// Token: 0x170001A8 RID: 424
		// (get) Token: 0x060006E7 RID: 1767 RVA: 0x00025EFC File Offset: 0x000240FC
		// (set) Token: 0x060006E8 RID: 1768 RVA: 0x00025F10 File Offset: 0x00024110
		internal bool EnablePaging
		{
			get
			{
				return ((GridView)base.Component).AllowPaging;
			}
			set
			{
				Cursor value2 = Cursor.Current;
				try
				{
					Cursor.Current = Cursors.WaitCursor;
					ControlDesigner.InvokeTransactedChange(base.Component, new TransactedChangeCallback(this.EnablePagingCallback), value, SR.GetString("GridView_EnablePagingTransaction"));
				}
				finally
				{
					Cursor.Current = value2;
				}
			}
		}

		// Token: 0x170001A9 RID: 425
		// (get) Token: 0x060006E9 RID: 1769 RVA: 0x00025F70 File Offset: 0x00024170
		// (set) Token: 0x060006EA RID: 1770 RVA: 0x00025F78 File Offset: 0x00024178
		internal bool EnableSelection
		{
			get
			{
				return this._currentSelectState;
			}
			set
			{
				Cursor value2 = Cursor.Current;
				try
				{
					Cursor.Current = Cursors.WaitCursor;
					ControlDesigner.InvokeTransactedChange(base.Component, new TransactedChangeCallback(this.EnableSelectionCallback), value, SR.GetString("GridView_EnableSelectionTransaction"));
				}
				finally
				{
					Cursor.Current = value2;
				}
			}
		}

		// Token: 0x170001AA RID: 426
		// (get) Token: 0x060006EB RID: 1771 RVA: 0x00025FD8 File Offset: 0x000241D8
		// (set) Token: 0x060006EC RID: 1772 RVA: 0x00025FEC File Offset: 0x000241EC
		internal bool EnableSorting
		{
			get
			{
				return ((GridView)base.Component).AllowSorting;
			}
			set
			{
				Cursor value2 = Cursor.Current;
				try
				{
					Cursor.Current = Cursors.WaitCursor;
					ControlDesigner.InvokeTransactedChange(base.Component, new TransactedChangeCallback(this.EnableSortingCallback), value, SR.GetString("GridView_EnableSortingTransaction"));
				}
				finally
				{
					Cursor.Current = value2;
				}
			}
		}

		// Token: 0x170001AB RID: 427
		// (get) Token: 0x060006ED RID: 1773 RVA: 0x0002604C File Offset: 0x0002424C
		protected override int SampleRowCount
		{
			get
			{
				int result = 5;
				GridView gridView = (GridView)base.Component;
				if (gridView.AllowPaging && gridView.PageSize != 0)
				{
					result = Math.Min(gridView.PageSize, 100) + 1;
				}
				return result;
			}
		}

		// Token: 0x170001AC RID: 428
		// (get) Token: 0x060006EE RID: 1774 RVA: 0x00026088 File Offset: 0x00024288
		// (set) Token: 0x060006EF RID: 1775 RVA: 0x000226D8 File Offset: 0x000208D8
		private int SelectedFieldIndex
		{
			get
			{
				object obj = base.DesignerState["SelectedFieldIndex"];
				int count = ((GridView)base.Component).Columns.Count;
				if (obj == null || count == 0 || (int)obj < 0 || (int)obj >= count)
				{
					return -1;
				}
				return (int)obj;
			}
			set
			{
				base.DesignerState["SelectedFieldIndex"] = value;
			}
		}

		// Token: 0x170001AD RID: 429
		// (get) Token: 0x060006F0 RID: 1776 RVA: 0x000260DC File Offset: 0x000242DC
		public override TemplateGroupCollection TemplateGroups
		{
			get
			{
				TemplateGroupCollection templateGroups = base.TemplateGroups;
				DataControlFieldCollection columns = ((GridView)base.Component).Columns;
				int count = columns.Count;
				if (count > 0)
				{
					for (int i = 0; i < count; i++)
					{
						TemplateField templateField = columns[i] as TemplateField;
						if (templateField != null)
						{
							string headerText = columns[i].HeaderText;
							string text = SR.GetString("GridView_Field", new object[]
							{
								i.ToString(NumberFormatInfo.InvariantInfo)
							});
							if (headerText != null && headerText.Length != 0)
							{
								text = text + " - " + headerText;
							}
							TemplateGroup templateGroup = new TemplateGroup(text);
							for (int j = 0; j < GridViewDesigner._columnTemplateNames.Length; j++)
							{
								string text2 = GridViewDesigner._columnTemplateNames[j];
								templateGroup.AddTemplateDefinition(new TemplateDefinition(this, text2, columns[i], text2, this.GetTemplateStyle(j + 1000, templateField))
								{
									SupportsDataBinding = GridViewDesigner._columnTemplateSupportsDataBinding[j]
								});
							}
							templateGroups.Add(templateGroup);
						}
					}
				}
				for (int k = 0; k < GridViewDesigner._controlTemplateNames.Length; k++)
				{
					string text3 = GridViewDesigner._controlTemplateNames[k];
					TemplateGroup templateGroup2 = new TemplateGroup(GridViewDesigner._controlTemplateNames[k]);
					templateGroup2.AddTemplateDefinition(new TemplateDefinition(this, text3, base.Component, text3, this.GetTemplateStyle(k, null))
					{
						SupportsDataBinding = GridViewDesigner._controlTemplateSupportsDataBinding[k]
					});
					templateGroups.Add(templateGroup2);
				}
				return templateGroups;
			}
		}

		// Token: 0x170001AE RID: 430
		// (get) Token: 0x060006F1 RID: 1777 RVA: 0x00003B0F File Offset: 0x00001D0F
		protected override bool UsePreviewControl
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060006F2 RID: 1778 RVA: 0x0002625C File Offset: 0x0002445C
		private void AddKeysAndBoundFields(IDataSourceViewSchema schema)
		{
			DataControlFieldCollection columns = ((GridView)base.Component).Columns;
			if (schema != null)
			{
				IDataSourceFieldSchema[] fields = schema.GetFields();
				if (fields != null && fields.Length != 0)
				{
					ArrayList arrayList = new ArrayList();
					foreach (IDataSourceFieldSchema dataSourceFieldSchema in fields)
					{
						if (DataBinder.IsBindableType(dataSourceFieldSchema.DataType))
						{
							BoundField boundField;
							if (dataSourceFieldSchema.DataType == typeof(bool) || dataSourceFieldSchema.DataType == typeof(bool?))
							{
								boundField = new CheckBoxField();
							}
							else
							{
								boundField = new BoundField();
							}
							string name = dataSourceFieldSchema.Name;
							if (dataSourceFieldSchema.PrimaryKey)
							{
								arrayList.Add(name);
							}
							boundField.DataField = name;
							boundField.HeaderText = name;
							boundField.SortExpression = name;
							boundField.ReadOnly = (dataSourceFieldSchema.PrimaryKey || dataSourceFieldSchema.IsReadOnly);
							boundField.InsertVisible = !dataSourceFieldSchema.Identity;
							columns.Add(boundField);
						}
					}
					((GridView)base.Component).AutoGenerateColumns = false;
					int count = arrayList.Count;
					if (count > 0)
					{
						string[] array2 = new string[count];
						arrayList.CopyTo(array2, 0);
						((GridView)base.Component).DataKeyNames = array2;
					}
				}
			}
		}

		// Token: 0x060006F3 RID: 1779 RVA: 0x000263B8 File Offset: 0x000245B8
		internal void AddNewField()
		{
			Cursor value = Cursor.Current;
			try
			{
				Cursor.Current = Cursors.WaitCursor;
				this._ignoreSchemaRefreshedEvent = true;
				ControlDesigner.InvokeTransactedChange(base.Component, new TransactedChangeCallback(this.AddNewFieldChangeCallback), null, SR.GetString("GridView_AddNewFieldTransaction"));
				this._ignoreSchemaRefreshedEvent = false;
				this.UpdateDesignTimeHtml();
			}
			finally
			{
				Cursor.Current = value;
			}
		}

		// Token: 0x060006F4 RID: 1780 RVA: 0x00026424 File Offset: 0x00024624
		private bool AddNewFieldChangeCallback(object context)
		{
			if (base.DataSourceDesigner != null)
			{
				base.DataSourceDesigner.SuppressDataSourceEvents();
			}
			AddDataControlFieldDialog form = new AddDataControlFieldDialog(this);
			DialogResult dialogResult = UIServiceHelper.ShowDialog(base.Component.Site, form);
			if (base.DataSourceDesigner != null)
			{
				base.DataSourceDesigner.ResumeDataSourceEvents();
			}
			return dialogResult == DialogResult.OK;
		}

		// Token: 0x060006F5 RID: 1781 RVA: 0x00026474 File Offset: 0x00024674
		protected override void DataBind(BaseDataBoundControl dataBoundControl)
		{
			GridView gridView = (GridView)dataBoundControl;
			gridView.RowDataBound += this.OnRowDataBound;
			try
			{
				base.DataBind(dataBoundControl);
			}
			finally
			{
				gridView.RowDataBound -= this.OnRowDataBound;
			}
		}

		// Token: 0x060006F6 RID: 1782 RVA: 0x000264C8 File Offset: 0x000246C8
		internal void EditFields()
		{
			Cursor value = Cursor.Current;
			try
			{
				Cursor.Current = Cursors.WaitCursor;
				this._ignoreSchemaRefreshedEvent = true;
				ControlDesigner.InvokeTransactedChange(base.Component, new TransactedChangeCallback(this.EditFieldsChangeCallback), null, SR.GetString("GridView_EditFieldsTransaction"));
				this._ignoreSchemaRefreshedEvent = false;
				this.UpdateDesignTimeHtml();
			}
			finally
			{
				Cursor.Current = value;
			}
		}

		// Token: 0x060006F7 RID: 1783 RVA: 0x00026534 File Offset: 0x00024734
		private bool EditFieldsChangeCallback(object context)
		{
			if (base.DataSourceDesigner != null)
			{
				base.DataSourceDesigner.SuppressDataSourceEvents();
			}
			DataControlFieldsEditor form = new DataControlFieldsEditor(this);
			DialogResult dialogResult = UIServiceHelper.ShowDialog(base.Component.Site, form);
			if (base.DataSourceDesigner != null)
			{
				base.DataSourceDesigner.ResumeDataSourceEvents();
			}
			return dialogResult == DialogResult.OK;
		}

		// Token: 0x060006F8 RID: 1784 RVA: 0x00026584 File Offset: 0x00024784
		private bool EnableDeletingCallback(object context)
		{
			bool newState = !this._currentDeleteState;
			if (context is bool)
			{
				newState = (bool)context;
			}
			this.SaveManipulationSetting(GridViewDesigner.ManipulationMode.Delete, newState);
			return true;
		}

		// Token: 0x060006F9 RID: 1785 RVA: 0x000265B4 File Offset: 0x000247B4
		private bool EnableEditingCallback(object context)
		{
			bool newState = !this._currentEditState;
			if (context is bool)
			{
				newState = (bool)context;
			}
			this.SaveManipulationSetting(GridViewDesigner.ManipulationMode.Edit, newState);
			return true;
		}

		// Token: 0x060006FA RID: 1786 RVA: 0x000265E4 File Offset: 0x000247E4
		private bool EnablePagingCallback(object context)
		{
			bool allowPaging = ((GridView)base.Component).AllowPaging;
			bool flag = !allowPaging;
			if (context is bool)
			{
				flag = (bool)context;
			}
			PropertyDescriptor propertyDescriptor = TypeDescriptor.GetProperties(typeof(GridView))["AllowPaging"];
			propertyDescriptor.SetValue(base.Component, flag);
			return true;
		}

		// Token: 0x060006FB RID: 1787 RVA: 0x00026644 File Offset: 0x00024844
		private bool EnableSortingCallback(object context)
		{
			bool allowSorting = ((GridView)base.Component).AllowSorting;
			bool flag = !allowSorting;
			if (context is bool)
			{
				flag = (bool)context;
			}
			PropertyDescriptor propertyDescriptor = TypeDescriptor.GetProperties(typeof(GridView))["AllowSorting"];
			propertyDescriptor.SetValue(base.Component, flag);
			return true;
		}

		// Token: 0x060006FC RID: 1788 RVA: 0x000266A4 File Offset: 0x000248A4
		private bool EnableSelectionCallback(object context)
		{
			bool newState = !this._currentEditState;
			if (context is bool)
			{
				newState = (bool)context;
			}
			this.SaveManipulationSetting(GridViewDesigner.ManipulationMode.Select, newState);
			return true;
		}

		// Token: 0x060006FD RID: 1789 RVA: 0x000266D4 File Offset: 0x000248D4
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

		// Token: 0x060006FE RID: 1790 RVA: 0x00026754 File Offset: 0x00024954
		public override string GetDesignTimeHtml()
		{
			GridView gridView = (GridView)base.ViewControl;
			gridView.EnablePersistedSelection = false;
			IDataSourceDesigner dataSourceDesigner = base.DataSourceDesigner;
			this._regionCount = 0;
			bool flag = false;
			IDataSourceViewSchema dataSourceSchema = this.GetDataSourceSchema();
			if (dataSourceSchema != null)
			{
				IDataSourceFieldSchema[] fields = dataSourceSchema.GetFields();
				if (fields != null && fields.Length != 0)
				{
					flag = true;
				}
			}
			if (!flag)
			{
				gridView.DataKeyNames = null;
			}
			if (gridView.Columns.Count == 0)
			{
				gridView.AutoGenerateColumns = true;
			}
			TypeDescriptor.Refresh(base.Component);
			return base.GetDesignTimeHtml();
		}

		// Token: 0x060006FF RID: 1791 RVA: 0x000267D4 File Offset: 0x000249D4
		public override string GetDesignTimeHtml(DesignerRegionCollection regions)
		{
			string designTimeHtml = this.GetDesignTimeHtml();
			GridView gridView = (GridView)base.ViewControl;
			int count = gridView.Columns.Count;
			GridViewRow headerRow = gridView.HeaderRow;
			GridViewRow footerRow = gridView.FooterRow;
			int selectedFieldIndex = this.SelectedFieldIndex;
			if (headerRow != null)
			{
				for (int i = 0; i < count; i++)
				{
					string text = SR.GetString("GridView_Field", new object[]
					{
						i.ToString(NumberFormatInfo.InvariantInfo)
					});
					string headerText = gridView.Columns[i].HeaderText;
					if (headerText.Length == 0)
					{
						text = text + " - " + headerText;
					}
					DesignerRegion designerRegion = new DesignerRegion(this, text, true);
					designerRegion.UserData = i;
					if (i == selectedFieldIndex)
					{
						designerRegion.Highlight = true;
					}
					regions.Add(designerRegion);
				}
			}
			for (int j = 0; j < gridView.Rows.Count; j++)
			{
				GridViewRow gridViewRow = gridView.Rows[j];
				for (int k = 0; k < count; k++)
				{
					DesignerRegion designerRegion2 = new DesignerRegion(this, k.ToString(NumberFormatInfo.InvariantInfo), false);
					designerRegion2.UserData = -1;
					if (k == selectedFieldIndex)
					{
						designerRegion2.Highlight = true;
					}
					regions.Add(designerRegion2);
				}
			}
			if (footerRow != null)
			{
				for (int l = 0; l < count; l++)
				{
					DesignerRegion designerRegion3 = new DesignerRegion(this, l.ToString(NumberFormatInfo.InvariantInfo), false);
					designerRegion3.UserData = -1;
					if (l == selectedFieldIndex)
					{
						designerRegion3.Highlight = true;
					}
					regions.Add(designerRegion3);
				}
			}
			return designTimeHtml;
		}

		// Token: 0x06000700 RID: 1792 RVA: 0x00003930 File Offset: 0x00001B30
		public override string GetEditableDesignerRegionContent(EditableDesignerRegion region)
		{
			return string.Empty;
		}

		// Token: 0x06000701 RID: 1793 RVA: 0x00026974 File Offset: 0x00024B74
		private Style GetTemplateStyle(int templateIndex, TemplateField templateField)
		{
			Style style = new Style();
			style.CopyFrom(((GridView)base.ViewControl).ControlStyle);
			if (templateIndex != 0)
			{
				if (templateIndex != 1)
				{
					switch (templateIndex)
					{
					case 1000:
						style.CopyFrom(((GridView)base.ViewControl).RowStyle);
						style.CopyFrom(templateField.ItemStyle);
						break;
					case 1001:
						style.CopyFrom(((GridView)base.ViewControl).RowStyle);
						style.CopyFrom(((GridView)base.ViewControl).AlternatingRowStyle);
						style.CopyFrom(templateField.ItemStyle);
						break;
					case 1002:
						style.CopyFrom(((GridView)base.ViewControl).RowStyle);
						style.CopyFrom(((GridView)base.ViewControl).EditRowStyle);
						style.CopyFrom(templateField.ItemStyle);
						break;
					case 1003:
						style.CopyFrom(((GridView)base.ViewControl).HeaderStyle);
						style.CopyFrom(templateField.HeaderStyle);
						break;
					case 1004:
						style.CopyFrom(((GridView)base.ViewControl).FooterStyle);
						style.CopyFrom(templateField.FooterStyle);
						break;
					}
				}
				else
				{
					style.CopyFrom(((GridView)base.ViewControl).PagerStyle);
				}
			}
			else
			{
				style.CopyFrom(((GridView)base.ViewControl).EmptyDataRowStyle);
			}
			return style;
		}

		// Token: 0x06000702 RID: 1794 RVA: 0x00026AEC File Offset: 0x00024CEC
		public override void Initialize(IComponent component)
		{
			ControlDesigner.VerifyInitializeArgument(component, typeof(GridView));
			base.Initialize(component);
			if (base.View != null)
			{
				base.View.SetFlags(ViewFlags.TemplateEditing, true);
			}
		}

		// Token: 0x06000703 RID: 1795 RVA: 0x00026B1C File Offset: 0x00024D1C
		internal void MoveLeft()
		{
			Cursor value = Cursor.Current;
			try
			{
				Cursor.Current = Cursors.WaitCursor;
				ControlDesigner.InvokeTransactedChange(base.Component, new TransactedChangeCallback(this.MoveLeftCallback), null, SR.GetString("GridView_MoveLeftTransaction"));
				this.UpdateDesignTimeHtml();
			}
			finally
			{
				Cursor.Current = value;
			}
		}

		// Token: 0x06000704 RID: 1796 RVA: 0x00026B7C File Offset: 0x00024D7C
		private bool MoveLeftCallback(object context)
		{
			DataControlFieldCollection columns = ((GridView)base.Component).Columns;
			int selectedFieldIndex = this.SelectedFieldIndex;
			if (selectedFieldIndex > 0)
			{
				DataControlField field = columns[selectedFieldIndex];
				columns.RemoveAt(selectedFieldIndex);
				columns.Insert(selectedFieldIndex - 1, field);
				int selectedFieldIndex2 = this.SelectedFieldIndex;
				this.SelectedFieldIndex = selectedFieldIndex2 - 1;
				this.UpdateDesignTimeHtml();
				return true;
			}
			return false;
		}

		// Token: 0x06000705 RID: 1797 RVA: 0x00026BD8 File Offset: 0x00024DD8
		internal void MoveRight()
		{
			Cursor value = Cursor.Current;
			try
			{
				Cursor.Current = Cursors.WaitCursor;
				ControlDesigner.InvokeTransactedChange(base.Component, new TransactedChangeCallback(this.MoveRightCallback), null, SR.GetString("GridView_MoveRightTransaction"));
				this.UpdateDesignTimeHtml();
			}
			finally
			{
				Cursor.Current = value;
			}
		}

		// Token: 0x06000706 RID: 1798 RVA: 0x00026C38 File Offset: 0x00024E38
		private bool MoveRightCallback(object context)
		{
			DataControlFieldCollection columns = ((GridView)base.Component).Columns;
			int selectedFieldIndex = this.SelectedFieldIndex;
			if (selectedFieldIndex >= 0 && columns.Count > selectedFieldIndex + 1)
			{
				DataControlField field = columns[selectedFieldIndex];
				columns.RemoveAt(selectedFieldIndex);
				columns.Insert(selectedFieldIndex + 1, field);
				int selectedFieldIndex2 = this.SelectedFieldIndex;
				this.SelectedFieldIndex = selectedFieldIndex2 + 1;
				this.UpdateDesignTimeHtml();
				return true;
			}
			return false;
		}

		// Token: 0x06000707 RID: 1799 RVA: 0x00026C9E File Offset: 0x00024E9E
		protected override void OnClick(DesignerRegionMouseEventArgs e)
		{
			if (e.Region != null)
			{
				this.SelectedFieldIndex = (int)e.Region.UserData;
				this.UpdateDesignTimeHtml();
			}
		}

		// Token: 0x06000708 RID: 1800 RVA: 0x00026CC4 File Offset: 0x00024EC4
		private void OnRowDataBound(object sender, GridViewRowEventArgs e)
		{
			GridViewRow row = e.Row;
			if (row.RowType == DataControlRowType.DataRow || row.RowType == DataControlRowType.Header || row.RowType == DataControlRowType.Footer)
			{
				int count = ((GridView)sender).Columns.Count;
				int num = 0;
				if (((GridView)sender).AutoGenerateDeleteButton || ((GridView)sender).AutoGenerateEditButton || ((GridView)sender).AutoGenerateSelectButton)
				{
					num = 1;
				}
				for (int i = 0; i < count; i++)
				{
					TableCell tableCell = row.Cells[i + num];
					tableCell.Attributes[DesignerRegion.DesignerRegionAttributeName] = this._regionCount.ToString(NumberFormatInfo.InvariantInfo);
					this._regionCount++;
				}
			}
		}

		// Token: 0x06000709 RID: 1801 RVA: 0x00026D7C File Offset: 0x00024F7C
		protected override void OnSchemaRefreshed()
		{
			if (base.InTemplateMode)
			{
				return;
			}
			if (this._ignoreSchemaRefreshedEvent)
			{
				return;
			}
			Cursor value = Cursor.Current;
			try
			{
				Cursor.Current = Cursors.WaitCursor;
				ControlDesigner.InvokeTransactedChange(base.Component, new TransactedChangeCallback(this.SchemaRefreshedCallback), null, SR.GetString("GridView_SchemaRefreshedTransaction"));
				this.UpdateDesignTimeHtml();
			}
			finally
			{
				Cursor.Current = value;
			}
		}

		// Token: 0x0600070A RID: 1802 RVA: 0x00026DEC File Offset: 0x00024FEC
		protected override void PreFilterProperties(IDictionary properties)
		{
			base.PreFilterProperties(properties);
			if (base.InTemplateMode)
			{
				PropertyDescriptor propertyDescriptor = (PropertyDescriptor)properties["Columns"];
				properties["Columns"] = TypeDescriptor.CreateProperty(propertyDescriptor.ComponentType, propertyDescriptor, new Attribute[]
				{
					BrowsableAttribute.No
				});
			}
		}

		// Token: 0x0600070B RID: 1803 RVA: 0x00026E40 File Offset: 0x00025040
		internal void RemoveField()
		{
			Cursor value = Cursor.Current;
			try
			{
				Cursor.Current = Cursors.WaitCursor;
				ControlDesigner.InvokeTransactedChange(base.Component, new TransactedChangeCallback(this.RemoveFieldCallback), null, SR.GetString("GridView_RemoveFieldTransaction"));
				this.UpdateDesignTimeHtml();
			}
			finally
			{
				Cursor.Current = value;
			}
		}

		// Token: 0x0600070C RID: 1804 RVA: 0x00026EA0 File Offset: 0x000250A0
		private bool RemoveFieldCallback(object context)
		{
			int selectedFieldIndex = this.SelectedFieldIndex;
			if (selectedFieldIndex >= 0)
			{
				((GridView)base.Component).Columns.RemoveAt(selectedFieldIndex);
				if (selectedFieldIndex == ((GridView)base.Component).Columns.Count)
				{
					int selectedFieldIndex2 = this.SelectedFieldIndex;
					this.SelectedFieldIndex = selectedFieldIndex2 - 1;
					this.UpdateDesignTimeHtml();
				}
				return true;
			}
			return false;
		}

		// Token: 0x0600070D RID: 1805 RVA: 0x00026F00 File Offset: 0x00025100
		private void SaveManipulationSetting(GridViewDesigner.ManipulationMode mode, bool newState)
		{
			DataControlFieldCollection columns = ((GridView)base.Component).Columns;
			bool flag = false;
			ArrayList arrayList = new ArrayList();
			foreach (object obj in columns)
			{
				DataControlField dataControlField = (DataControlField)obj;
				CommandField commandField = dataControlField as CommandField;
				if (commandField != null)
				{
					switch (mode)
					{
					case GridViewDesigner.ManipulationMode.Edit:
						commandField.ShowEditButton = newState;
						break;
					case GridViewDesigner.ManipulationMode.Delete:
						commandField.ShowDeleteButton = newState;
						break;
					case GridViewDesigner.ManipulationMode.Select:
						commandField.ShowSelectButton = newState;
						break;
					}
					if (!newState && !commandField.ShowEditButton && !commandField.ShowDeleteButton && !commandField.ShowInsertButton && !commandField.ShowSelectButton)
					{
						arrayList.Add(commandField);
					}
					flag = true;
				}
			}
			foreach (object obj2 in arrayList)
			{
				columns.Remove((DataControlField)obj2);
			}
			if (!flag && newState)
			{
				CommandField commandField2 = new CommandField();
				switch (mode)
				{
				case GridViewDesigner.ManipulationMode.Edit:
					commandField2.ShowEditButton = newState;
					break;
				case GridViewDesigner.ManipulationMode.Delete:
					commandField2.ShowDeleteButton = newState;
					break;
				case GridViewDesigner.ManipulationMode.Select:
					commandField2.ShowSelectButton = newState;
					break;
				}
				columns.Insert(0, commandField2);
			}
			if (!newState)
			{
				GridView gridView = (GridView)base.Component;
				switch (mode)
				{
				case GridViewDesigner.ManipulationMode.Edit:
				{
					PropertyDescriptor propertyDescriptor = TypeDescriptor.GetProperties(typeof(GridView))["AutoGenerateEditButton"];
					propertyDescriptor.SetValue(base.Component, newState);
					return;
				}
				case GridViewDesigner.ManipulationMode.Delete:
				{
					PropertyDescriptor propertyDescriptor = TypeDescriptor.GetProperties(typeof(GridView))["AutoGenerateDeleteButton"];
					propertyDescriptor.SetValue(base.Component, newState);
					return;
				}
				case GridViewDesigner.ManipulationMode.Select:
				{
					PropertyDescriptor propertyDescriptor = TypeDescriptor.GetProperties(typeof(GridView))["AutoGenerateSelectButton"];
					propertyDescriptor.SetValue(base.Component, newState);
					break;
				}
				default:
					return;
				}
			}
		}

		// Token: 0x0600070E RID: 1806 RVA: 0x00027120 File Offset: 0x00025320
		private bool SchemaRefreshedCallback(object context)
		{
			IDataSourceViewSchema dataSourceSchema = this.GetDataSourceSchema();
			if (base.DataSourceID.Length > 0 && dataSourceSchema != null)
			{
				if (((GridView)base.Component).Columns.Count > 0 || ((GridView)base.Component).DataKeyNames.Length != 0)
				{
					if (DialogResult.Yes == UIServiceHelper.ShowMessage(base.Component.Site, SR.GetString("DataBoundControl_SchemaRefreshedWarning", new object[]
					{
						SR.GetString("DataBoundControl_GridView"),
						SR.GetString("DataBoundControl_Column")
					}), SR.GetString("DataBoundControl_SchemaRefreshedCaption", new object[]
					{
						((GridView)base.Component).ID
					}), MessageBoxButtons.YesNo))
					{
						((GridView)base.Component).DataKeyNames = new string[0];
						((GridView)base.Component).Columns.Clear();
						this.SelectedFieldIndex = -1;
						this.AddKeysAndBoundFields(dataSourceSchema);
					}
				}
				else
				{
					this.AddKeysAndBoundFields(dataSourceSchema);
				}
			}
			else if ((((GridView)base.Component).Columns.Count > 0 || ((GridView)base.Component).DataKeyNames.Length != 0) && DialogResult.Yes == UIServiceHelper.ShowMessage(base.Component.Site, SR.GetString("DataBoundControl_SchemaRefreshedWarningNoDataSource", new object[]
			{
				SR.GetString("DataBoundControl_GridView"),
				SR.GetString("DataBoundControl_Column")
			}), SR.GetString("DataBoundControl_SchemaRefreshedCaption", new object[]
			{
				((GridView)base.Component).ID
			}), MessageBoxButtons.YesNo))
			{
				((GridView)base.Component).DataKeyNames = new string[0];
				((GridView)base.Component).Columns.Clear();
				this.SelectedFieldIndex = -1;
			}
			return true;
		}

		// Token: 0x0600070F RID: 1807 RVA: 0x00003937 File Offset: 0x00001B37
		public override void SetEditableDesignerRegionContent(EditableDesignerRegion region, string content)
		{
		}

		// Token: 0x06000710 RID: 1808 RVA: 0x000272EC File Offset: 0x000254EC
		private void UpdateFieldsCurrentState()
		{
			this._currentSelectState = ((GridView)base.Component).AutoGenerateSelectButton;
			this._currentEditState = ((GridView)base.Component).AutoGenerateEditButton;
			this._currentDeleteState = ((GridView)base.Component).AutoGenerateDeleteButton;
			foreach (object obj in ((GridView)base.Component).Columns)
			{
				DataControlField dataControlField = (DataControlField)obj;
				CommandField commandField = dataControlField as CommandField;
				if (commandField != null)
				{
					if (commandField.ShowSelectButton)
					{
						this._currentSelectState = true;
					}
					if (commandField.ShowEditButton)
					{
						this._currentEditState = true;
					}
					if (commandField.ShowDeleteButton)
					{
						this._currentDeleteState = true;
					}
				}
			}
		}

		// Token: 0x04000444 RID: 1092
		private static DesignerAutoFormatCollection _autoFormats;

		// Token: 0x04000445 RID: 1093
		private static string[] _columnTemplateNames = new string[]
		{
			"ItemTemplate",
			"AlternatingItemTemplate",
			"EditItemTemplate",
			"HeaderTemplate",
			"FooterTemplate"
		};

		// Token: 0x04000446 RID: 1094
		private static bool[] _columnTemplateSupportsDataBinding = new bool[]
		{
			true,
			true,
			true,
			false,
			false
		};

		// Token: 0x04000447 RID: 1095
		private const int IDX_COLUMN_HEADER_TEMPLATE = 3;

		// Token: 0x04000448 RID: 1096
		private const int IDX_COLUMN_ITEM_TEMPLATE = 0;

		// Token: 0x04000449 RID: 1097
		private const int IDX_COLUMN_ALTITEM_TEMPLATE = 1;

		// Token: 0x0400044A RID: 1098
		private const int IDX_COLUMN_EDITITEM_TEMPLATE = 2;

		// Token: 0x0400044B RID: 1099
		private const int IDX_COLUMN_FOOTER_TEMPLATE = 4;

		// Token: 0x0400044C RID: 1100
		private const int BASE_INDEX = 1000;

		// Token: 0x0400044D RID: 1101
		private static string[] _controlTemplateNames = new string[]
		{
			"EmptyDataTemplate",
			"PagerTemplate"
		};

		// Token: 0x0400044E RID: 1102
		private static bool[] _controlTemplateSupportsDataBinding = new bool[]
		{
			true,
			true
		};

		// Token: 0x0400044F RID: 1103
		private const int IDX_CONTROL_EMPTY_DATA_TEMPLATE = 0;

		// Token: 0x04000450 RID: 1104
		private const int IDX_CONTROL_PAGER_TEMPLATE = 1;

		// Token: 0x04000451 RID: 1105
		private GridViewActionList _actionLists;

		// Token: 0x04000452 RID: 1106
		private int _regionCount;

		// Token: 0x04000453 RID: 1107
		private bool _currentEditState;

		// Token: 0x04000454 RID: 1108
		private bool _currentDeleteState;

		// Token: 0x04000455 RID: 1109
		private bool _currentSelectState;

		// Token: 0x04000456 RID: 1110
		internal bool _ignoreSchemaRefreshedEvent;

		// Token: 0x02000400 RID: 1024
		private enum ManipulationMode
		{
			// Token: 0x04001C62 RID: 7266
			Edit,
			// Token: 0x04001C63 RID: 7267
			Delete,
			// Token: 0x04001C64 RID: 7268
			Select
		}
	}
}
