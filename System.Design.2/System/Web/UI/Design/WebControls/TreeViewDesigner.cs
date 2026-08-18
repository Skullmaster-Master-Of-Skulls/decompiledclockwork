using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Design;
using System.Globalization;
using System.Web.UI.Design.Util;
using System.Web.UI.WebControls;
using System.Windows.Forms;

namespace System.Web.UI.Design.WebControls
{
	// Token: 0x0200012D RID: 301
	public class TreeViewDesigner : HierarchicalDataBoundControlDesigner
	{
		// Token: 0x1700026B RID: 619
		// (get) Token: 0x06000ADE RID: 2782 RVA: 0x00045A2C File Offset: 0x00043C2C
		public override DesignerActionListCollection ActionLists
		{
			get
			{
				DesignerActionListCollection designerActionListCollection = new DesignerActionListCollection();
				designerActionListCollection.AddRange(base.ActionLists);
				designerActionListCollection.Add(new TreeViewDesigner.TreeViewDesignerActionList(this));
				return designerActionListCollection;
			}
		}

		// Token: 0x1700026C RID: 620
		// (get) Token: 0x06000ADF RID: 2783 RVA: 0x00045A59 File Offset: 0x00043C59
		public override DesignerAutoFormatCollection AutoFormats
		{
			get
			{
				if (TreeViewDesigner._autoFormats == null)
				{
					TreeViewDesigner._autoFormats = ControlDesigner.CreateAutoFormats(AutoFormatSchemes.TREEVIEW_SCHEME_NAMES, (string schemeName) => new ReflectionBasedAutoFormat(schemeName, "<Schemes>\r\n<xsd:schema id=\"Schemes\" xmlns=\"\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns:msdata=\"urn:schemas-microsoft-com:xml-msdata\">\r\n  <xsd:element name=\"Scheme\">\r\n     <xsd:complexType>\r\n       <xsd:all>\r\n        <xsd:element name=\"SchemeName\" type=\"xsd:string\"/>\r\n        <xsd:element name=\"ImageSet\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n        <xsd:element name=\"NodeIndent\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n        <xsd:element name=\"ShowLines\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n        <xsd:element name=\"ShowExpandCollapse\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n        <xsd:element name=\"NodeStyle-Font-Size\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n        <xsd:element name=\"NodeStyle-Font-Names\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n        <xsd:element name=\"NodeStyle-Font--ClearDefaults\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n        <xsd:element name=\"NodeStyle-ForeColor\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n        <xsd:element name=\"NodeStyle-HorizontalPadding\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n        <xsd:element name=\"NodeStyle-NodeSpacing\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n        <xsd:element name=\"NodeStyle-VerticalPadding\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n        <xsd:element name=\"ParentNodeStyle-Font-Bold\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n        <xsd:element name=\"ParentNodeStyle-Font--ClearDefaults\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n        <xsd:element name=\"ParentNodeStyle-ForeColor\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n        <xsd:element name=\"SelectedNodeStyle-BackColor\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n        <xsd:element name=\"SelectedNodeStyle-BorderColor\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n        <xsd:element name=\"SelectedNodeStyle-BorderStyle\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n        <xsd:element name=\"SelectedNodeStyle-BorderWidth\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n        <xsd:element name=\"SelectedNodeStyle-Font-Underline\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n        <xsd:element name=\"SelectedNodeStyle-Font--ClearDefaults\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n        <xsd:element name=\"SelectedNodeStyle-ForeColor\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n        <xsd:element name=\"SelectedNodeStyle-HorizontalPadding\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n        <xsd:element name=\"SelectedNodeStyle-VerticalPadding\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n        <xsd:element name=\"HoverNodeStyle-BackColor\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n        <xsd:element name=\"HoverNodeStyle-BorderColor\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n        <xsd:element name=\"HoverNodeStyle-BorderStyle\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n        <xsd:element name=\"HoverNodeStyle-BorderWidth\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n        <xsd:element name=\"HoverNodeStyle-Font-Underline\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n        <xsd:element name=\"HoverNodeStyle-Font--ClearDefaults\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n        <xsd:element name=\"HoverNodeStyle-ForeColor\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n      </xsd:all>\r\n    </xsd:complexType>\r\n  </xsd:element>\r\n  <xsd:element name=\"Schemes\" msdata:IsDataSet=\"true\">\r\n    <xsd:complexType>\r\n      <xsd:choice maxOccurs=\"unbounded\">\r\n        <xsd:element ref=\"Scheme\"/>\r\n      </xsd:choice>\r\n    </xsd:complexType>\r\n  </xsd:element>\r\n</xsd:schema>\r\n<Scheme>\r\n  <SchemeName>TVScheme_Empty</SchemeName>\r\n  <ImageSet>Custom</ImageSet>\r\n  <NodeIndent>20</NodeIndent>\r\n  <ShowLines>false</ShowLines>\r\n  <ShowExpandCollapse>true</ShowExpandCollapse>\r\n  <NodeStyle-Font-Size></NodeStyle-Font-Size>\r\n  <NodeStyle-Font-Names></NodeStyle-Font-Names>\r\n  <NodeStyle-Font--ClearDefaults>true</NodeStyle-Font--ClearDefaults>\r\n  <NodeStyle-ForeColor></NodeStyle-ForeColor>\r\n  <NodeStyle-HorizontalPadding></NodeStyle-HorizontalPadding>\r\n  <NodeStyle-NodeSpacing></NodeStyle-NodeSpacing>\r\n  <NodeStyle-VerticalPadding></NodeStyle-VerticalPadding>\r\n  <ParentNodeStyle-Font-Bold>false</ParentNodeStyle-Font-Bold>\r\n  <ParentNodeStyle-Font--ClearDefaults>true</ParentNodeStyle-Font--ClearDefaults>\r\n  <ParentNodeStyle-ForeColor></ParentNodeStyle-ForeColor>\r\n  <SelectedNodeStyle-BackColor></SelectedNodeStyle-BackColor>\r\n  <SelectedNodeStyle-BorderColor></SelectedNodeStyle-BorderColor>\r\n  <SelectedNodeStyle-BorderStyle>NotSet</SelectedNodeStyle-BorderStyle>\r\n  <SelectedNodeStyle-BorderWidth></SelectedNodeStyle-BorderWidth>\r\n  <SelectedNodeStyle-Font-Underline>false</SelectedNodeStyle-Font-Underline>\r\n  <SelectedNodeStyle-Font--ClearDefaults>true</SelectedNodeStyle-Font--ClearDefaults>\r\n  <SelectedNodeStyle-ForeColor></SelectedNodeStyle-ForeColor>\r\n  <SelectedNodeStyle-HorizontalPadding></SelectedNodeStyle-HorizontalPadding>\r\n  <SelectedNodeStyle-VerticalPadding></SelectedNodeStyle-VerticalPadding>\r\n  <HoverNodeStyle-BackColor></HoverNodeStyle-BackColor>\r\n  <HoverNodeStyle-BorderColor></HoverNodeStyle-BorderColor>\r\n  <HoverNodeStyle-BorderStyle>NotSet</HoverNodeStyle-BorderStyle>\r\n  <HoverNodeStyle-BorderWidth></HoverNodeStyle-BorderWidth>\r\n  <HoverNodeStyle-Font-Underline>false</HoverNodeStyle-Font-Underline>\r\n  <HoverNodeStyle-Font--ClearDefaults>true</HoverNodeStyle-Font--ClearDefaults>\r\n  <HoverNodeStyle-ForeColor></HoverNodeStyle-ForeColor>\r\n</Scheme>\r\n<Scheme>\r\n  <SchemeName>TVScheme_Arrows</SchemeName>\r\n  <ImageSet>Arrows</ImageSet>\r\n  <NodeIndent>20</NodeIndent>\r\n  <ShowLines>false</ShowLines>\r\n  <ShowExpandCollapse>true</ShowExpandCollapse>\r\n  <NodeStyle-Font-Size>8</NodeStyle-Font-Size>\r\n  <NodeStyle-Font-Names>Verdana</NodeStyle-Font-Names>\r\n  <NodeStyle-Font--ClearDefaults>false</NodeStyle-Font--ClearDefaults>\r\n  <NodeStyle-ForeColor>Black</NodeStyle-ForeColor>\r\n  <NodeStyle-HorizontalPadding>5</NodeStyle-HorizontalPadding>\r\n  <NodeStyle-NodeSpacing>0</NodeStyle-NodeSpacing>\r\n  <NodeStyle-VerticalPadding>0</NodeStyle-VerticalPadding>\r\n  <ParentNodeStyle-Font-Bold>false</ParentNodeStyle-Font-Bold>\r\n  <ParentNodeStyle-Font--ClearDefaults>false</ParentNodeStyle-Font--ClearDefaults>\r\n  <ParentNodeStyle-ForeColor></ParentNodeStyle-ForeColor>\r\n  <SelectedNodeStyle-BackColor></SelectedNodeStyle-BackColor>\r\n  <SelectedNodeStyle-BorderColor></SelectedNodeStyle-BorderColor>\r\n  <SelectedNodeStyle-BorderStyle>NotSet</SelectedNodeStyle-BorderStyle>\r\n  <SelectedNodeStyle-BorderWidth></SelectedNodeStyle-BorderWidth>\r\n  <SelectedNodeStyle-Font-Underline>true</SelectedNodeStyle-Font-Underline>\r\n  <SelectedNodeStyle-Font--ClearDefaults>false</SelectedNodeStyle-Font--ClearDefaults>\r\n  <SelectedNodeStyle-ForeColor>#5555DD</SelectedNodeStyle-ForeColor>\r\n  <SelectedNodeStyle-HorizontalPadding>0</SelectedNodeStyle-HorizontalPadding>\r\n  <SelectedNodeStyle-VerticalPadding>0</SelectedNodeStyle-VerticalPadding>\r\n  <HoverNodeStyle-BackColor></HoverNodeStyle-BackColor>\r\n  <HoverNodeStyle-BorderColor></HoverNodeStyle-BorderColor>\r\n  <HoverNodeStyle-BorderStyle>NotSet</HoverNodeStyle-BorderStyle>\r\n  <HoverNodeStyle-BorderWidth></HoverNodeStyle-BorderWidth>\r\n  <HoverNodeStyle-Font-Underline>true</HoverNodeStyle-Font-Underline>\r\n  <HoverNodeStyle-Font--ClearDefaults>false</HoverNodeStyle-Font--ClearDefaults>\r\n  <HoverNodeStyle-ForeColor>#5555DD</HoverNodeStyle-ForeColor>\r\n</Scheme>\r\n<Scheme>\r\n  <SchemeName>TVScheme_Arrows2</SchemeName>\r\n  <ImageSet>Arrows</ImageSet>\r\n  <NodeIndent>20</NodeIndent>\r\n  <ShowLines>false</ShowLines>\r\n  <ShowExpandCollapse>true</ShowExpandCollapse>\r\n  <NodeStyle-Font-Size>10</NodeStyle-Font-Size>\r\n  <NodeStyle-Font-Names>Tahoma</NodeStyle-Font-Names>\r\n  <NodeStyle-Font--ClearDefaults>false</NodeStyle-Font--ClearDefaults>\r\n  <NodeStyle-ForeColor>Black</NodeStyle-ForeColor>\r\n  <NodeStyle-HorizontalPadding>5</NodeStyle-HorizontalPadding>\r\n  <NodeStyle-NodeSpacing>0</NodeStyle-NodeSpacing>\r\n  <NodeStyle-VerticalPadding>0</NodeStyle-VerticalPadding>\r\n  <ParentNodeStyle-Font-Bold>false</ParentNodeStyle-Font-Bold>\r\n  <ParentNodeStyle-Font--ClearDefaults>false</ParentNodeStyle-Font--ClearDefaults>\r\n  <ParentNodeStyle-ForeColor></ParentNodeStyle-ForeColor>\r\n  <SelectedNodeStyle-BackColor></SelectedNodeStyle-BackColor>\r\n  <SelectedNodeStyle-BorderColor></SelectedNodeStyle-BorderColor>\r\n  <SelectedNodeStyle-BorderStyle>NotSet</SelectedNodeStyle-BorderStyle>\r\n  <SelectedNodeStyle-BorderWidth></SelectedNodeStyle-BorderWidth>\r\n  <SelectedNodeStyle-Font-Underline>true</SelectedNodeStyle-Font-Underline>\r\n  <SelectedNodeStyle-Font--ClearDefaults>false</SelectedNodeStyle-Font--ClearDefaults>\r\n  <SelectedNodeStyle-ForeColor>#5555DD</SelectedNodeStyle-ForeColor>\r\n  <SelectedNodeStyle-HorizontalPadding>0</SelectedNodeStyle-HorizontalPadding>\r\n  <SelectedNodeStyle-VerticalPadding>0</SelectedNodeStyle-VerticalPadding>\r\n  <HoverNodeStyle-BackColor></HoverNodeStyle-BackColor>\r\n  <HoverNodeStyle-BorderColor></HoverNodeStyle-BorderColor>\r\n  <HoverNodeStyle-BorderStyle>NotSet</HoverNodeStyle-BorderStyle>\r\n  <HoverNodeStyle-BorderWidth></HoverNodeStyle-BorderWidth>\r\n  <HoverNodeStyle-Font-Underline>true</HoverNodeStyle-Font-Underline>\r\n  <HoverNodeStyle-Font--ClearDefaults>false</HoverNodeStyle-Font--ClearDefaults>\r\n  <HoverNodeStyle-ForeColor>#5555DD</HoverNodeStyle-ForeColor>\r\n</Scheme>\r\n<Scheme>\r\n  <SchemeName>TVScheme_BulletedList</SchemeName>\r\n  <ImageSet>BulletedList</ImageSet>\r\n  <NodeIndent>20</NodeIndent>\r\n  <ShowLines>false</ShowLines>\r\n  <ShowExpandCollapse>false</ShowExpandCollapse>\r\n  <NodeStyle-Font-Size>8</NodeStyle-Font-Size>\r\n  <NodeStyle-Font-Names>Verdana</NodeStyle-Font-Names>\r\n  <NodeStyle-Font--ClearDefaults>false</NodeStyle-Font--ClearDefaults>\r\n  <NodeStyle-ForeColor>Black</NodeStyle-ForeColor>\r\n  <NodeStyle-HorizontalPadding>0</NodeStyle-HorizontalPadding>\r\n  <NodeStyle-NodeSpacing>0</NodeStyle-NodeSpacing>\r\n  <NodeStyle-VerticalPadding>0</NodeStyle-VerticalPadding>\r\n  <ParentNodeStyle-Font-Bold>false</ParentNodeStyle-Font-Bold>\r\n  <ParentNodeStyle-Font--ClearDefaults>false</ParentNodeStyle-Font--ClearDefaults>\r\n  <ParentNodeStyle-ForeColor></ParentNodeStyle-ForeColor>\r\n  <SelectedNodeStyle-BackColor></SelectedNodeStyle-BackColor>\r\n  <SelectedNodeStyle-BorderColor></SelectedNodeStyle-BorderColor>\r\n  <SelectedNodeStyle-BorderStyle>NotSet</SelectedNodeStyle-BorderStyle>\r\n  <SelectedNodeStyle-BorderWidth></SelectedNodeStyle-BorderWidth>\r\n  <SelectedNodeStyle-Font-Underline>true</SelectedNodeStyle-Font-Underline>\r\n  <SelectedNodeStyle-Font--ClearDefaults>false</SelectedNodeStyle-Font--ClearDefaults>\r\n  <SelectedNodeStyle-ForeColor>#5555DD</SelectedNodeStyle-ForeColor>\r\n  <SelectedNodeStyle-HorizontalPadding>0</SelectedNodeStyle-HorizontalPadding>\r\n  <SelectedNodeStyle-VerticalPadding>0</SelectedNodeStyle-VerticalPadding>\r\n  <HoverNodeStyle-BackColor></HoverNodeStyle-BackColor>\r\n  <HoverNodeStyle-BorderColor></HoverNodeStyle-BorderColor>\r\n  <HoverNodeStyle-BorderStyle>NotSet</HoverNodeStyle-BorderStyle>\r\n  <HoverNodeStyle-BorderWidth></HoverNodeStyle-BorderWidth>\r\n  <HoverNodeStyle-Font-Underline>true</HoverNodeStyle-Font-Underline>\r\n  <HoverNodeStyle-Font--ClearDefaults>false</HoverNodeStyle-Font--ClearDefaults>\r\n  <HoverNodeStyle-ForeColor>#5555DD</HoverNodeStyle-ForeColor>\r\n</Scheme>\r\n<Scheme>\r\n  <SchemeName>TVScheme_BulletedList2</SchemeName>\r\n  <ImageSet>BulletedList2</ImageSet>\r\n  <NodeIndent>20</NodeIndent>\r\n  <ShowLines>false</ShowLines>\r\n  <ShowExpandCollapse>false</ShowExpandCollapse>\r\n  <NodeStyle-Font-Size>8</NodeStyle-Font-Size>\r\n  <NodeStyle-Font-Names>Verdana</NodeStyle-Font-Names>\r\n  <NodeStyle-Font--ClearDefaults>false</NodeStyle-Font--ClearDefaults>\r\n  <NodeStyle-ForeColor>Black</NodeStyle-ForeColor>\r\n  <NodeStyle-HorizontalPadding>0</NodeStyle-HorizontalPadding>\r\n  <NodeStyle-NodeSpacing>0</NodeStyle-NodeSpacing>\r\n  <NodeStyle-VerticalPadding>0</NodeStyle-VerticalPadding>\r\n  <ParentNodeStyle-Font-Bold>false</ParentNodeStyle-Font-Bold>\r\n  <ParentNodeStyle-Font--ClearDefaults>false</ParentNodeStyle-Font--ClearDefaults>\r\n  <ParentNodeStyle-ForeColor></ParentNodeStyle-ForeColor>\r\n  <SelectedNodeStyle-BackColor></SelectedNodeStyle-BackColor>\r\n  <SelectedNodeStyle-BorderColor></SelectedNodeStyle-BorderColor>\r\n  <SelectedNodeStyle-BorderStyle>NotSet</SelectedNodeStyle-BorderStyle>\r\n  <SelectedNodeStyle-BorderWidth></SelectedNodeStyle-BorderWidth>\r\n  <SelectedNodeStyle-Font-Underline>true</SelectedNodeStyle-Font-Underline>\r\n  <SelectedNodeStyle-Font--ClearDefaults>false</SelectedNodeStyle-Font--ClearDefaults>\r\n  <SelectedNodeStyle-ForeColor>#5555DD</SelectedNodeStyle-ForeColor>\r\n  <SelectedNodeStyle-HorizontalPadding>0</SelectedNodeStyle-HorizontalPadding>\r\n  <SelectedNodeStyle-VerticalPadding>0</SelectedNodeStyle-VerticalPadding>\r\n  <HoverNodeStyle-BackColor></HoverNodeStyle-BackColor>\r\n  <HoverNodeStyle-BorderColor></HoverNodeStyle-BorderColor>\r\n  <HoverNodeStyle-BorderStyle>NotSet</HoverNodeStyle-BorderStyle>\r\n  <HoverNodeStyle-BorderWidth></HoverNodeStyle-BorderWidth>\r\n  <HoverNodeStyle-Font-Underline>true</HoverNodeStyle-Font-Underline>\r\n  <HoverNodeStyle-Font--ClearDefaults>false</HoverNodeStyle-Font--ClearDefaults>\r\n  <HoverNodeStyle-ForeColor>#5555DD</HoverNodeStyle-ForeColor>\r\n</Scheme>\r\n<Scheme>\r\n  <SchemeName>TVScheme_BulletedList3</SchemeName>\r\n  <ImageSet>BulletedList3</ImageSet>\r\n  <NodeIndent>20</NodeIndent>\r\n  <ShowLines>false</ShowLines>\r\n  <ShowExpandCollapse>false</ShowExpandCollapse>\r\n  <NodeStyle-Font-Size>8</NodeStyle-Font-Size>\r\n  <NodeStyle-Font-Names>Verdana</NodeStyle-Font-Names>\r\n  <NodeStyle-Font--ClearDefaults>false</NodeStyle-Font--ClearDefaults>\r\n  <NodeStyle-ForeColor>Black</NodeStyle-ForeColor>\r\n  <NodeStyle-HorizontalPadding>5</NodeStyle-HorizontalPadding>\r\n  <NodeStyle-NodeSpacing>0</NodeStyle-NodeSpacing>\r\n  <NodeStyle-VerticalPadding>0</NodeStyle-VerticalPadding>\r\n  <ParentNodeStyle-Font-Bold>false</ParentNodeStyle-Font-Bold>\r\n  <ParentNodeStyle-Font--ClearDefaults>false</ParentNodeStyle-Font--ClearDefaults>\r\n  <ParentNodeStyle-ForeColor></ParentNodeStyle-ForeColor>\r\n  <SelectedNodeStyle-BackColor></SelectedNodeStyle-BackColor>\r\n  <SelectedNodeStyle-BorderColor></SelectedNodeStyle-BorderColor>\r\n  <SelectedNodeStyle-BorderStyle>NotSet</SelectedNodeStyle-BorderStyle>\r\n  <SelectedNodeStyle-BorderWidth></SelectedNodeStyle-BorderWidth>\r\n  <SelectedNodeStyle-Font-Underline>true</SelectedNodeStyle-Font-Underline>\r\n  <SelectedNodeStyle-Font--ClearDefaults>false</SelectedNodeStyle-Font--ClearDefaults>\r\n  <SelectedNodeStyle-ForeColor>#5555DD</SelectedNodeStyle-ForeColor>\r\n  <SelectedNodeStyle-HorizontalPadding>0</SelectedNodeStyle-HorizontalPadding>\r\n  <SelectedNodeStyle-VerticalPadding>0</SelectedNodeStyle-VerticalPadding>\r\n  <HoverNodeStyle-BackColor></HoverNodeStyle-BackColor>\r\n  <HoverNodeStyle-BorderColor></HoverNodeStyle-BorderColor>\r\n  <HoverNodeStyle-BorderStyle>NotSet</HoverNodeStyle-BorderStyle>\r\n  <HoverNodeStyle-BorderWidth></HoverNodeStyle-BorderWidth>\r\n  <HoverNodeStyle-Font-Underline>true</HoverNodeStyle-Font-Underline>\r\n  <HoverNodeStyle-Font--ClearDefaults>false</HoverNodeStyle-Font--ClearDefaults>\r\n  <HoverNodeStyle-ForeColor>#5555DD</HoverNodeStyle-ForeColor>\r\n</Scheme>\r\n<Scheme>\r\n  <SchemeName>TVScheme_BulletedList4</SchemeName>\r\n  <ImageSet>BulletedList</ImageSet>\r\n  <NodeIndent>20</NodeIndent>\r\n  <ShowLines>false</ShowLines>\r\n  <ShowExpandCollapse>false</ShowExpandCollapse>\r\n  <NodeStyle-Font-Size>10</NodeStyle-Font-Size>\r\n  <NodeStyle-Font-Names>Tahoma</NodeStyle-Font-Names>\r\n  <NodeStyle-Font--ClearDefaults>false</NodeStyle-Font--ClearDefaults>\r\n  <NodeStyle-ForeColor>Black</NodeStyle-ForeColor>\r\n  <NodeStyle-HorizontalPadding>5</NodeStyle-HorizontalPadding>\r\n  <NodeStyle-NodeSpacing>0</NodeStyle-NodeSpacing>\r\n  <NodeStyle-VerticalPadding>0</NodeStyle-VerticalPadding>\r\n  <ParentNodeStyle-Font-Bold>false</ParentNodeStyle-Font-Bold>\r\n  <ParentNodeStyle-Font--ClearDefaults>false</ParentNodeStyle-Font--ClearDefaults>\r\n  <ParentNodeStyle-ForeColor></ParentNodeStyle-ForeColor>\r\n  <SelectedNodeStyle-BackColor></SelectedNodeStyle-BackColor>\r\n  <SelectedNodeStyle-BorderColor></SelectedNodeStyle-BorderColor>\r\n  <SelectedNodeStyle-BorderStyle>NotSet</SelectedNodeStyle-BorderStyle>\r\n  <SelectedNodeStyle-BorderWidth></SelectedNodeStyle-BorderWidth>\r\n  <SelectedNodeStyle-Font-Underline>true</SelectedNodeStyle-Font-Underline>\r\n  <SelectedNodeStyle-Font--ClearDefaults>false</SelectedNodeStyle-Font--ClearDefaults>\r\n  <SelectedNodeStyle-ForeColor>#5555DD</SelectedNodeStyle-ForeColor>\r\n  <SelectedNodeStyle-HorizontalPadding>0</SelectedNodeStyle-HorizontalPadding>\r\n  <SelectedNodeStyle-VerticalPadding>0</SelectedNodeStyle-VerticalPadding>\r\n  <HoverNodeStyle-BackColor></HoverNodeStyle-BackColor>\r\n  <HoverNodeStyle-BorderColor></HoverNodeStyle-BorderColor>\r\n  <HoverNodeStyle-BorderStyle>NotSet</HoverNodeStyle-BorderStyle>\r\n  <HoverNodeStyle-BorderWidth></HoverNodeStyle-BorderWidth>\r\n  <HoverNodeStyle-Font-Underline>true</HoverNodeStyle-Font-Underline>\r\n  <HoverNodeStyle-Font--ClearDefaults>false</HoverNodeStyle-Font--ClearDefaults>\r\n  <HoverNodeStyle-ForeColor>#5555DD</HoverNodeStyle-ForeColor>\r\n</Scheme>\r\n<Scheme>\r\n  <SchemeName>TVScheme_BulletedList5</SchemeName>\r\n  <ImageSet>BulletedList2</ImageSet>\r\n  <NodeIndent>20</NodeIndent>\r\n  <ShowLines>false</ShowLines>\r\n  <ShowExpandCollapse>false</ShowExpandCollapse>\r\n  <NodeStyle-Font-Size>10</NodeStyle-Font-Size>\r\n  <NodeStyle-Font-Names>Tahoma</NodeStyle-Font-Names>\r\n  <NodeStyle-Font--ClearDefaults>false</NodeStyle-Font--ClearDefaults>\r\n  <NodeStyle-ForeColor>Black</NodeStyle-ForeColor>\r\n  <NodeStyle-HorizontalPadding>5</NodeStyle-HorizontalPadding>\r\n  <NodeStyle-NodeSpacing>0</NodeStyle-NodeSpacing>\r\n  <NodeStyle-VerticalPadding>0</NodeStyle-VerticalPadding>\r\n  <ParentNodeStyle-Font-Bold>false</ParentNodeStyle-Font-Bold>\r\n  <ParentNodeStyle-Font--ClearDefaults>false</ParentNodeStyle-Font--ClearDefaults>\r\n  <ParentNodeStyle-ForeColor></ParentNodeStyle-ForeColor>\r\n  <SelectedNodeStyle-BackColor></SelectedNodeStyle-BackColor>\r\n  <SelectedNodeStyle-BorderColor></SelectedNodeStyle-BorderColor>\r\n  <SelectedNodeStyle-BorderStyle>NotSet</SelectedNodeStyle-BorderStyle>\r\n  <SelectedNodeStyle-BorderWidth></SelectedNodeStyle-BorderWidth>\r\n  <SelectedNodeStyle-Font-Underline>true</SelectedNodeStyle-Font-Underline>\r\n  <SelectedNodeStyle-Font--ClearDefaults>false</SelectedNodeStyle-Font--ClearDefaults>\r\n  <SelectedNodeStyle-ForeColor>#5555DD</SelectedNodeStyle-ForeColor>\r\n  <SelectedNodeStyle-HorizontalPadding>0</SelectedNodeStyle-HorizontalPadding>\r\n  <SelectedNodeStyle-VerticalPadding>0</SelectedNodeStyle-VerticalPadding>\r\n  <HoverNodeStyle-BackColor></HoverNodeStyle-BackColor>\r\n  <HoverNodeStyle-BorderColor></HoverNodeStyle-BorderColor>\r\n  <HoverNodeStyle-BorderStyle>NotSet</HoverNodeStyle-BorderStyle>\r\n  <HoverNodeStyle-BorderWidth></HoverNodeStyle-BorderWidth>\r\n  <HoverNodeStyle-Font-Underline>true</HoverNodeStyle-Font-Underline>\r\n  <HoverNodeStyle-Font--ClearDefaults>false</HoverNodeStyle-Font--ClearDefaults>\r\n  <HoverNodeStyle-ForeColor>#5555DD</HoverNodeStyle-ForeColor>\r\n</Scheme>\r\n<Scheme>\r\n  <SchemeName>TVScheme_BulletedList6</SchemeName>\r\n  <ImageSet>BulletedList4</ImageSet>\r\n  <NodeIndent>20</NodeIndent>\r\n  <ShowLines>false</ShowLines>\r\n  <ShowExpandCollapse>false</ShowExpandCollapse>\r\n  <NodeStyle-Font-Size>10</NodeStyle-Font-Size>\r\n  <NodeStyle-Font-Names>Tahoma</NodeStyle-Font-Names>\r\n  <NodeStyle-Font--ClearDefaults>false</NodeStyle-Font--ClearDefaults>\r\n  <NodeStyle-ForeColor>Black</NodeStyle-ForeColor>\r\n  <NodeStyle-HorizontalPadding>5</NodeStyle-HorizontalPadding>\r\n  <NodeStyle-NodeSpacing>0</NodeStyle-NodeSpacing>\r\n  <NodeStyle-VerticalPadding>0</NodeStyle-VerticalPadding>\r\n  <ParentNodeStyle-Font-Bold>false</ParentNodeStyle-Font-Bold>\r\n  <ParentNodeStyle-Font--ClearDefaults>false</ParentNodeStyle-Font--ClearDefaults>\r\n  <ParentNodeStyle-ForeColor></ParentNodeStyle-ForeColor>\r\n  <SelectedNodeStyle-BackColor></SelectedNodeStyle-BackColor>\r\n  <SelectedNodeStyle-BorderColor></SelectedNodeStyle-BorderColor>\r\n  <SelectedNodeStyle-BorderStyle>NotSet</SelectedNodeStyle-BorderStyle>\r\n  <SelectedNodeStyle-BorderWidth></SelectedNodeStyle-BorderWidth>\r\n  <SelectedNodeStyle-Font-Underline>true</SelectedNodeStyle-Font-Underline>[...string is too long...]"));
				}
				return TreeViewDesigner._autoFormats;
			}
		}

		// Token: 0x1700026D RID: 621
		// (get) Token: 0x06000AE0 RID: 2784 RVA: 0x00003B0F File Offset: 0x00001D0F
		protected override bool UsePreviewControl
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06000AE1 RID: 2785 RVA: 0x00045A95 File Offset: 0x00043C95
		protected void CreateLineImages()
		{
			ControlDesigner.InvokeTransactedChange(base.Component, new TransactedChangeCallback(this.CreateLineImagesCallBack), null, SR.GetString("TreeViewDesigner_CreateLineImagesTransactionDescription"));
		}

		// Token: 0x06000AE2 RID: 2786 RVA: 0x00045ABC File Offset: 0x00043CBC
		private bool CreateLineImagesCallBack(object context)
		{
			TreeViewImageGenerator form = new TreeViewImageGenerator(this._treeView);
			return UIServiceHelper.ShowDialog(base.Component.Site, form) == DialogResult.OK;
		}

		// Token: 0x06000AE3 RID: 2787 RVA: 0x00045AEC File Offset: 0x00043CEC
		protected override void DataBind(BaseDataBoundControl dataBoundControl)
		{
			System.Web.UI.WebControls.TreeView treeView = (System.Web.UI.WebControls.TreeView)dataBoundControl;
			this._usingSampleData = false;
			this._emptyDataBinding = false;
			if ((treeView.DataSourceID != null && treeView.DataSourceID.Length > 0) || treeView.DataSource != null || treeView.Nodes.Count == 0)
			{
				treeView.Nodes.Clear();
				base.DataBind(treeView);
			}
			if (this._usingSampleData)
			{
				treeView.ExpandAll();
				return;
			}
			this.ExpandToDepth(treeView.Nodes, treeView.ExpandDepth);
			if (treeView.Nodes.Count == 0)
			{
				this._emptyDataBinding = true;
			}
		}

		// Token: 0x06000AE4 RID: 2788 RVA: 0x00045B80 File Offset: 0x00043D80
		protected void EditBindings()
		{
			IServiceProvider site = this._treeView.Site;
			TreeViewBindingsEditorForm form = new TreeViewBindingsEditorForm(site, this._treeView, this);
			UIServiceHelper.ShowDialog(site, form);
		}

		// Token: 0x06000AE5 RID: 2789 RVA: 0x00045BB0 File Offset: 0x00043DB0
		protected void EditNodes()
		{
			PropertyDescriptor member = TypeDescriptor.GetProperties(base.Component)["Nodes"];
			ControlDesigner.InvokeTransactedChange(base.Component, new TransactedChangeCallback(this.EditNodesChangeCallback), null, SR.GetString("TreeViewDesigner_EditNodesTransactionDescription"), member);
		}

		// Token: 0x06000AE6 RID: 2790 RVA: 0x00045BF8 File Offset: 0x00043DF8
		private bool EditNodesChangeCallback(object context)
		{
			IServiceProvider site = this._treeView.Site;
			TreeNodeCollectionEditorDialog form = new TreeNodeCollectionEditorDialog(this._treeView, this);
			DialogResult dialogResult = UIServiceHelper.ShowDialog(site, form);
			return dialogResult == DialogResult.OK;
		}

		// Token: 0x06000AE7 RID: 2791 RVA: 0x00045C2C File Offset: 0x00043E2C
		private void ExpandToDepth(System.Web.UI.WebControls.TreeNodeCollection nodes, int depth)
		{
			foreach (object obj in nodes)
			{
				System.Web.UI.WebControls.TreeNode treeNode = (System.Web.UI.WebControls.TreeNode)obj;
				bool? expanded = treeNode.Expanded;
				bool flag = false;
				if (!(expanded.GetValueOrDefault() == flag & expanded != null) && (depth == -1 || treeNode.Depth < depth))
				{
					treeNode.Expanded = new bool?(true);
					this.ExpandToDepth(treeNode.ChildNodes, depth);
				}
			}
		}

		// Token: 0x06000AE8 RID: 2792 RVA: 0x00045CC0 File Offset: 0x00043EC0
		protected override IHierarchicalEnumerable GetSampleDataSource()
		{
			this._usingSampleData = true;
			((System.Web.UI.WebControls.TreeView)base.ViewControl).AutoGenerateDataBindings = true;
			return base.GetSampleDataSource();
		}

		// Token: 0x06000AE9 RID: 2793 RVA: 0x00045CE0 File Offset: 0x00043EE0
		public override string GetDesignTimeHtml()
		{
			string result = base.GetDesignTimeHtml();
			if (this._emptyDataBinding)
			{
				result = this.GetEmptyDataBindingDesignTimeHtml();
			}
			return result;
		}

		// Token: 0x06000AEA RID: 2794 RVA: 0x00045D04 File Offset: 0x00043F04
		private string GetEmptyDataBindingDesignTimeHtml()
		{
			string name = this._treeView.Site.Name;
			return string.Format(CultureInfo.CurrentUICulture, "\r\n                <table cellpadding=4 cellspacing=0 style=\"font-family:Tahoma;font-size:8pt;color:buttontext;background-color:buttonface\">\r\n                  <tr><td><span style=\"font-weight:bold\">TreeView</span> - {0}</td></tr>\r\n                  <tr><td>{1}</td></tr>\r\n                </table>\r\n             ", new object[]
			{
				name,
				SR.GetString("TreeViewDesigner_EmptyDataBinding")
			});
		}

		// Token: 0x06000AEB RID: 2795 RVA: 0x00045D48 File Offset: 0x00043F48
		protected override string GetEmptyDesignTimeHtml()
		{
			string name = this._treeView.Site.Name;
			return string.Format(CultureInfo.CurrentUICulture, "\r\n                <table cellpadding=4 cellspacing=0 style=\"font-family:Tahoma;font-size:8pt;color:buttontext;background-color:buttonface\">\r\n                  <tr><td><span style=\"font-weight:bold\">TreeView</span> - {0}</td></tr>\r\n                  <tr><td>{1}</td></tr>\r\n                </table>\r\n             ", new object[]
			{
				name,
				SR.GetString("TreeViewDesigner_Empty")
			});
		}

		// Token: 0x06000AEC RID: 2796 RVA: 0x00045D8C File Offset: 0x00043F8C
		protected override string GetErrorDesignTimeHtml(Exception e)
		{
			string name = this._treeView.Site.Name;
			return string.Format(CultureInfo.CurrentUICulture, "\r\n                <table cellpadding=4 cellspacing=0 style=\"font-family:Tahoma;font-size:8pt;color:buttontext;background-color:buttonface;border: solid 1px;border-top-color:buttonhighlight;border-left-color:buttonhighlight;border-bottom-color:buttonshadow;border-right-color:buttonshadow\">\r\n                  <tr><td><span style=\"font-weight:bold\">TreeView</span> - {0}</td></tr>\r\n                  <tr><td>{1}</td></tr>\r\n                </table>\r\n             ", new object[]
			{
				name,
				SR.GetString("TreeViewDesigner_Error", new object[]
				{
					e.Message
				})
			});
		}

		// Token: 0x06000AED RID: 2797 RVA: 0x00045DDF File Offset: 0x00043FDF
		public override void Initialize(IComponent component)
		{
			ControlDesigner.VerifyInitializeArgument(component, typeof(System.Web.UI.WebControls.TreeView));
			base.Initialize(component);
			this._treeView = (System.Web.UI.WebControls.TreeView)component;
		}

		// Token: 0x06000AEE RID: 2798 RVA: 0x00045E04 File Offset: 0x00044004
		internal void InvokeTreeNodeCollectionEditor()
		{
			this.EditNodes();
		}

		// Token: 0x06000AEF RID: 2799 RVA: 0x00045E0C File Offset: 0x0004400C
		internal void InvokeTreeViewBindingsEditor()
		{
			this.EditBindings();
		}

		// Token: 0x04000680 RID: 1664
		private System.Web.UI.WebControls.TreeView _treeView;

		// Token: 0x04000681 RID: 1665
		private bool _usingSampleData;

		// Token: 0x04000682 RID: 1666
		private bool _emptyDataBinding;

		// Token: 0x04000683 RID: 1667
		private static DesignerAutoFormatCollection _autoFormats;

		// Token: 0x04000684 RID: 1668
		private const string emptyDesignTimeHtml = "\r\n                <table cellpadding=4 cellspacing=0 style=\"font-family:Tahoma;font-size:8pt;color:buttontext;background-color:buttonface\">\r\n                  <tr><td><span style=\"font-weight:bold\">TreeView</span> - {0}</td></tr>\r\n                  <tr><td>{1}</td></tr>\r\n                </table>\r\n             ";

		// Token: 0x04000685 RID: 1669
		private const string errorDesignTimeHtml = "\r\n                <table cellpadding=4 cellspacing=0 style=\"font-family:Tahoma;font-size:8pt;color:buttontext;background-color:buttonface;border: solid 1px;border-top-color:buttonhighlight;border-left-color:buttonhighlight;border-bottom-color:buttonshadow;border-right-color:buttonshadow\">\r\n                  <tr><td><span style=\"font-weight:bold\">TreeView</span> - {0}</td></tr>\r\n                  <tr><td>{1}</td></tr>\r\n                </table>\r\n             ";

		// Token: 0x0200044F RID: 1103
		private class TreeViewDesignerActionList : DesignerActionList
		{
			// Token: 0x06002932 RID: 10546 RVA: 0x000F9CD7 File Offset: 0x000F7ED7
			public TreeViewDesignerActionList(TreeViewDesigner parent) : base(parent.Component)
			{
				this._parent = parent;
			}

			// Token: 0x170008B5 RID: 2229
			// (get) Token: 0x06002933 RID: 10547 RVA: 0x00003B0F File Offset: 0x00001D0F
			// (set) Token: 0x06002934 RID: 10548 RVA: 0x00003937 File Offset: 0x00001B37
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

			// Token: 0x170008B6 RID: 2230
			// (get) Token: 0x06002935 RID: 10549 RVA: 0x000F9CEC File Offset: 0x000F7EEC
			// (set) Token: 0x06002936 RID: 10550 RVA: 0x000F9D00 File Offset: 0x000F7F00
			public bool ShowLines
			{
				get
				{
					return ((System.Web.UI.WebControls.TreeView)base.Component).ShowLines;
				}
				set
				{
					PropertyDescriptor propertyDescriptor = TypeDescriptor.GetProperties(typeof(System.Web.UI.WebControls.TreeView))["ShowLines"];
					propertyDescriptor.SetValue(base.Component, value);
					TypeDescriptor.Refresh(base.Component);
				}
			}

			// Token: 0x06002937 RID: 10551 RVA: 0x000F9D44 File Offset: 0x000F7F44
			public void CreateLineImages()
			{
				this._parent.CreateLineImages();
			}

			// Token: 0x06002938 RID: 10552 RVA: 0x000F9D51 File Offset: 0x000F7F51
			public void EditBindings()
			{
				this._parent.EditBindings();
			}

			// Token: 0x06002939 RID: 10553 RVA: 0x000F9D5E File Offset: 0x000F7F5E
			public void EditNodes()
			{
				this._parent.EditNodes();
			}

			// Token: 0x0600293A RID: 10554 RVA: 0x000F9D6C File Offset: 0x000F7F6C
			public override DesignerActionItemCollection GetSortedActionItems()
			{
				DesignerActionItemCollection designerActionItemCollection = new DesignerActionItemCollection();
				string @string = SR.GetString("TreeViewDesigner_DataActionGroup");
				if (string.IsNullOrEmpty(this._parent.DataSourceID))
				{
					designerActionItemCollection.Add(new DesignerActionMethodItem(this, "EditNodes", SR.GetString("TreeViewDesigner_EditNodes"), @string, SR.GetString("TreeViewDesigner_EditNodesDescription"), true));
				}
				else
				{
					designerActionItemCollection.Add(new DesignerActionMethodItem(this, "EditBindings", SR.GetString("TreeViewDesigner_EditBindings"), @string, SR.GetString("TreeViewDesigner_EditBindingsDescription"), true));
				}
				if (this.ShowLines)
				{
					designerActionItemCollection.Add(new DesignerActionMethodItem(this, "CreateLineImages", SR.GetString("TreeViewDesigner_CreateLineImages"), @string, SR.GetString("TreeViewDesigner_CreateLineImagesDescription"), true));
				}
				designerActionItemCollection.Add(new DesignerActionPropertyItem("ShowLines", SR.GetString("TreeViewDesigner_ShowLines"), "Actions", SR.GetString("TreeViewDesigner_ShowLinesDescription")));
				return designerActionItemCollection;
			}

			// Token: 0x04001D27 RID: 7463
			private TreeViewDesigner _parent;
		}
	}
}
