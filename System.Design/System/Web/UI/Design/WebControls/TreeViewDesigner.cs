using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Data;
using System.Design;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Web.UI.Design.Util;
using System.Web.UI.WebControls;
using System.Windows.Forms;

namespace System.Web.UI.Design.WebControls
{
	// Token: 0x020004F4 RID: 1268
	public class TreeViewDesigner : HierarchicalDataBoundControlDesigner
	{
		// Token: 0x17000882 RID: 2178
		// (get) Token: 0x06002D57 RID: 11607 RVA: 0x00100F18 File Offset: 0x000FFF18
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

		// Token: 0x17000883 RID: 2179
		// (get) Token: 0x06002D58 RID: 11608 RVA: 0x00100F4D File Offset: 0x000FFF4D
		public override DesignerAutoFormatCollection AutoFormats
		{
			get
			{
				if (TreeViewDesigner._autoFormats == null)
				{
					TreeViewDesigner._autoFormats = ControlDesigner.CreateAutoFormats("<Schemes>\r\n<xsd:schema id=\"Schemes\" xmlns=\"\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns:msdata=\"urn:schemas-microsoft-com:xml-msdata\">\r\n  <xsd:element name=\"Scheme\">\r\n     <xsd:complexType>\r\n       <xsd:all>\r\n        <xsd:element name=\"SchemeName\" type=\"xsd:string\"/>\r\n        <xsd:element name=\"ImageSet\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n        <xsd:element name=\"NodeIndent\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n        <xsd:element name=\"ShowLines\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n        <xsd:element name=\"ShowExpandCollapse\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n        <xsd:element name=\"NodeStyle-Font-Size\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n        <xsd:element name=\"NodeStyle-Font-Names\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n        <xsd:element name=\"NodeStyle-Font--ClearDefaults\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n        <xsd:element name=\"NodeStyle-ForeColor\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n        <xsd:element name=\"NodeStyle-HorizontalPadding\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n        <xsd:element name=\"NodeStyle-NodeSpacing\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n        <xsd:element name=\"NodeStyle-VerticalPadding\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n        <xsd:element name=\"ParentNodeStyle-Font-Bold\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n        <xsd:element name=\"ParentNodeStyle-Font--ClearDefaults\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n        <xsd:element name=\"ParentNodeStyle-ForeColor\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n        <xsd:element name=\"SelectedNodeStyle-BackColor\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n        <xsd:element name=\"SelectedNodeStyle-BorderColor\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n        <xsd:element name=\"SelectedNodeStyle-BorderStyle\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n        <xsd:element name=\"SelectedNodeStyle-BorderWidth\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n        <xsd:element name=\"SelectedNodeStyle-Font-Underline\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n        <xsd:element name=\"SelectedNodeStyle-Font--ClearDefaults\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n        <xsd:element name=\"SelectedNodeStyle-ForeColor\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n        <xsd:element name=\"SelectedNodeStyle-HorizontalPadding\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n        <xsd:element name=\"SelectedNodeStyle-VerticalPadding\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n        <xsd:element name=\"HoverNodeStyle-BackColor\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n        <xsd:element name=\"HoverNodeStyle-BorderColor\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n        <xsd:element name=\"HoverNodeStyle-BorderStyle\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n        <xsd:element name=\"HoverNodeStyle-BorderWidth\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n        <xsd:element name=\"HoverNodeStyle-Font-Underline\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n        <xsd:element name=\"HoverNodeStyle-Font--ClearDefaults\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n        <xsd:element name=\"HoverNodeStyle-ForeColor\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n      </xsd:all>\r\n    </xsd:complexType>\r\n  </xsd:element>\r\n  <xsd:element name=\"Schemes\" msdata:IsDataSet=\"true\">\r\n    <xsd:complexType>\r\n      <xsd:choice maxOccurs=\"unbounded\">\r\n        <xsd:element ref=\"Scheme\"/>\r\n      </xsd:choice>\r\n    </xsd:complexType>\r\n  </xsd:element>\r\n</xsd:schema>\r\n<Scheme>\r\n  <SchemeName>TVScheme_Empty</SchemeName>\r\n  <ImageSet>Custom</ImageSet>\r\n  <NodeIndent>20</NodeIndent>\r\n  <ShowLines>false</ShowLines>\r\n  <ShowExpandCollapse>true</ShowExpandCollapse>\r\n  <NodeStyle-Font-Size></NodeStyle-Font-Size>\r\n  <NodeStyle-Font-Names></NodeStyle-Font-Names>\r\n  <NodeStyle-Font--ClearDefaults>true</NodeStyle-Font--ClearDefaults>\r\n  <NodeStyle-ForeColor></NodeStyle-ForeColor>\r\n  <NodeStyle-HorizontalPadding></NodeStyle-HorizontalPadding>\r\n  <NodeStyle-NodeSpacing></NodeStyle-NodeSpacing>\r\n  <NodeStyle-VerticalPadding></NodeStyle-VerticalPadding>\r\n  <ParentNodeStyle-Font-Bold>false</ParentNodeStyle-Font-Bold>\r\n  <ParentNodeStyle-Font--ClearDefaults>true</ParentNodeStyle-Font--ClearDefaults>\r\n  <ParentNodeStyle-ForeColor></ParentNodeStyle-ForeColor>\r\n  <SelectedNodeStyle-BackColor></SelectedNodeStyle-BackColor>\r\n  <SelectedNodeStyle-BorderColor></SelectedNodeStyle-BorderColor>\r\n  <SelectedNodeStyle-BorderStyle>NotSet</SelectedNodeStyle-BorderStyle>\r\n  <SelectedNodeStyle-BorderWidth></SelectedNodeStyle-BorderWidth>\r\n  <SelectedNodeStyle-Font-Underline>false</SelectedNodeStyle-Font-Underline>\r\n  <SelectedNodeStyle-Font--ClearDefaults>true</SelectedNodeStyle-Font--ClearDefaults>\r\n  <SelectedNodeStyle-ForeColor></SelectedNodeStyle-ForeColor>\r\n  <SelectedNodeStyle-HorizontalPadding></SelectedNodeStyle-HorizontalPadding>\r\n  <SelectedNodeStyle-VerticalPadding></SelectedNodeStyle-VerticalPadding>\r\n  <HoverNodeStyle-BackColor></HoverNodeStyle-BackColor>\r\n  <HoverNodeStyle-BorderColor></HoverNodeStyle-BorderColor>\r\n  <HoverNodeStyle-BorderStyle>NotSet</HoverNodeStyle-BorderStyle>\r\n  <HoverNodeStyle-BorderWidth></HoverNodeStyle-BorderWidth>\r\n  <HoverNodeStyle-Font-Underline>false</HoverNodeStyle-Font-Underline>\r\n  <HoverNodeStyle-Font--ClearDefaults>true</HoverNodeStyle-Font--ClearDefaults>\r\n  <HoverNodeStyle-ForeColor></HoverNodeStyle-ForeColor>\r\n</Scheme>\r\n<Scheme>\r\n  <SchemeName>TVScheme_Arrows</SchemeName>\r\n  <ImageSet>Arrows</ImageSet>\r\n  <NodeIndent>20</NodeIndent>\r\n  <ShowLines>false</ShowLines>\r\n  <ShowExpandCollapse>true</ShowExpandCollapse>\r\n  <NodeStyle-Font-Size>8</NodeStyle-Font-Size>\r\n  <NodeStyle-Font-Names>Verdana</NodeStyle-Font-Names>\r\n  <NodeStyle-Font--ClearDefaults>false</NodeStyle-Font--ClearDefaults>\r\n  <NodeStyle-ForeColor>Black</NodeStyle-ForeColor>\r\n  <NodeStyle-HorizontalPadding>5</NodeStyle-HorizontalPadding>\r\n  <NodeStyle-NodeSpacing>0</NodeStyle-NodeSpacing>\r\n  <NodeStyle-VerticalPadding>0</NodeStyle-VerticalPadding>\r\n  <ParentNodeStyle-Font-Bold>false</ParentNodeStyle-Font-Bold>\r\n  <ParentNodeStyle-Font--ClearDefaults>false</ParentNodeStyle-Font--ClearDefaults>\r\n  <ParentNodeStyle-ForeColor></ParentNodeStyle-ForeColor>\r\n  <SelectedNodeStyle-BackColor></SelectedNodeStyle-BackColor>\r\n  <SelectedNodeStyle-BorderColor></SelectedNodeStyle-BorderColor>\r\n  <SelectedNodeStyle-BorderStyle>NotSet</SelectedNodeStyle-BorderStyle>\r\n  <SelectedNodeStyle-BorderWidth></SelectedNodeStyle-BorderWidth>\r\n  <SelectedNodeStyle-Font-Underline>true</SelectedNodeStyle-Font-Underline>\r\n  <SelectedNodeStyle-Font--ClearDefaults>false</SelectedNodeStyle-Font--ClearDefaults>\r\n  <SelectedNodeStyle-ForeColor>#5555DD</SelectedNodeStyle-ForeColor>\r\n  <SelectedNodeStyle-HorizontalPadding>0</SelectedNodeStyle-HorizontalPadding>\r\n  <SelectedNodeStyle-VerticalPadding>0</SelectedNodeStyle-VerticalPadding>\r\n  <HoverNodeStyle-BackColor></HoverNodeStyle-BackColor>\r\n  <HoverNodeStyle-BorderColor></HoverNodeStyle-BorderColor>\r\n  <HoverNodeStyle-BorderStyle>NotSet</HoverNodeStyle-BorderStyle>\r\n  <HoverNodeStyle-BorderWidth></HoverNodeStyle-BorderWidth>\r\n  <HoverNodeStyle-Font-Underline>true</HoverNodeStyle-Font-Underline>\r\n  <HoverNodeStyle-Font--ClearDefaults>false</HoverNodeStyle-Font--ClearDefaults>\r\n  <HoverNodeStyle-ForeColor>#5555DD</HoverNodeStyle-ForeColor>\r\n</Scheme>\r\n<Scheme>\r\n  <SchemeName>TVScheme_Arrows2</SchemeName>\r\n  <ImageSet>Arrows</ImageSet>\r\n  <NodeIndent>20</NodeIndent>\r\n  <ShowLines>false</ShowLines>\r\n  <ShowExpandCollapse>true</ShowExpandCollapse>\r\n  <NodeStyle-Font-Size>10</NodeStyle-Font-Size>\r\n  <NodeStyle-Font-Names>Tahoma</NodeStyle-Font-Names>\r\n  <NodeStyle-Font--ClearDefaults>false</NodeStyle-Font--ClearDefaults>\r\n  <NodeStyle-ForeColor>Black</NodeStyle-ForeColor>\r\n  <NodeStyle-HorizontalPadding>5</NodeStyle-HorizontalPadding>\r\n  <NodeStyle-NodeSpacing>0</NodeStyle-NodeSpacing>\r\n  <NodeStyle-VerticalPadding>0</NodeStyle-VerticalPadding>\r\n  <ParentNodeStyle-Font-Bold>false</ParentNodeStyle-Font-Bold>\r\n  <ParentNodeStyle-Font--ClearDefaults>false</ParentNodeStyle-Font--ClearDefaults>\r\n  <ParentNodeStyle-ForeColor></ParentNodeStyle-ForeColor>\r\n  <SelectedNodeStyle-BackColor></SelectedNodeStyle-BackColor>\r\n  <SelectedNodeStyle-BorderColor></SelectedNodeStyle-BorderColor>\r\n  <SelectedNodeStyle-BorderStyle>NotSet</SelectedNodeStyle-BorderStyle>\r\n  <SelectedNodeStyle-BorderWidth></SelectedNodeStyle-BorderWidth>\r\n  <SelectedNodeStyle-Font-Underline>true</SelectedNodeStyle-Font-Underline>\r\n  <SelectedNodeStyle-Font--ClearDefaults>false</SelectedNodeStyle-Font--ClearDefaults>\r\n  <SelectedNodeStyle-ForeColor>#5555DD</SelectedNodeStyle-ForeColor>\r\n  <SelectedNodeStyle-HorizontalPadding>0</SelectedNodeStyle-HorizontalPadding>\r\n  <SelectedNodeStyle-VerticalPadding>0</SelectedNodeStyle-VerticalPadding>\r\n  <HoverNodeStyle-BackColor></HoverNodeStyle-BackColor>\r\n  <HoverNodeStyle-BorderColor></HoverNodeStyle-BorderColor>\r\n  <HoverNodeStyle-BorderStyle>NotSet</HoverNodeStyle-BorderStyle>\r\n  <HoverNodeStyle-BorderWidth></HoverNodeStyle-BorderWidth>\r\n  <HoverNodeStyle-Font-Underline>true</HoverNodeStyle-Font-Underline>\r\n  <HoverNodeStyle-Font--ClearDefaults>false</HoverNodeStyle-Font--ClearDefaults>\r\n  <HoverNodeStyle-ForeColor>#5555DD</HoverNodeStyle-ForeColor>\r\n</Scheme>\r\n<Scheme>\r\n  <SchemeName>TVScheme_BulletedList</SchemeName>\r\n  <ImageSet>BulletedList</ImageSet>\r\n  <NodeIndent>20</NodeIndent>\r\n  <ShowLines>false</ShowLines>\r\n  <ShowExpandCollapse>false</ShowExpandCollapse>\r\n  <NodeStyle-Font-Size>8</NodeStyle-Font-Size>\r\n  <NodeStyle-Font-Names>Verdana</NodeStyle-Font-Names>\r\n  <NodeStyle-Font--ClearDefaults>false</NodeStyle-Font--ClearDefaults>\r\n  <NodeStyle-ForeColor>Black</NodeStyle-ForeColor>\r\n  <NodeStyle-HorizontalPadding>0</NodeStyle-HorizontalPadding>\r\n  <NodeStyle-NodeSpacing>0</NodeStyle-NodeSpacing>\r\n  <NodeStyle-VerticalPadding>0</NodeStyle-VerticalPadding>\r\n  <ParentNodeStyle-Font-Bold>false</ParentNodeStyle-Font-Bold>\r\n  <ParentNodeStyle-Font--ClearDefaults>false</ParentNodeStyle-Font--ClearDefaults>\r\n  <ParentNodeStyle-ForeColor></ParentNodeStyle-ForeColor>\r\n  <SelectedNodeStyle-BackColor></SelectedNodeStyle-BackColor>\r\n  <SelectedNodeStyle-BorderColor></SelectedNodeStyle-BorderColor>\r\n  <SelectedNodeStyle-BorderStyle>NotSet</SelectedNodeStyle-BorderStyle>\r\n  <SelectedNodeStyle-BorderWidth></SelectedNodeStyle-BorderWidth>\r\n  <SelectedNodeStyle-Font-Underline>true</SelectedNodeStyle-Font-Underline>\r\n  <SelectedNodeStyle-Font--ClearDefaults>false</SelectedNodeStyle-Font--ClearDefaults>\r\n  <SelectedNodeStyle-ForeColor>#5555DD</SelectedNodeStyle-ForeColor>\r\n  <SelectedNodeStyle-HorizontalPadding>0</SelectedNodeStyle-HorizontalPadding>\r\n  <SelectedNodeStyle-VerticalPadding>0</SelectedNodeStyle-VerticalPadding>\r\n  <HoverNodeStyle-BackColor></HoverNodeStyle-BackColor>\r\n  <HoverNodeStyle-BorderColor></HoverNodeStyle-BorderColor>\r\n  <HoverNodeStyle-BorderStyle>NotSet</HoverNodeStyle-BorderStyle>\r\n  <HoverNodeStyle-BorderWidth></HoverNodeStyle-BorderWidth>\r\n  <HoverNodeStyle-Font-Underline>true</HoverNodeStyle-Font-Underline>\r\n  <HoverNodeStyle-Font--ClearDefaults>false</HoverNodeStyle-Font--ClearDefaults>\r\n  <HoverNodeStyle-ForeColor>#5555DD</HoverNodeStyle-ForeColor>\r\n</Scheme>\r\n<Scheme>\r\n  <SchemeName>TVScheme_BulletedList2</SchemeName>\r\n  <ImageSet>BulletedList2</ImageSet>\r\n  <NodeIndent>20</NodeIndent>\r\n  <ShowLines>false</ShowLines>\r\n  <ShowExpandCollapse>false</ShowExpandCollapse>\r\n  <NodeStyle-Font-Size>8</NodeStyle-Font-Size>\r\n  <NodeStyle-Font-Names>Verdana</NodeStyle-Font-Names>\r\n  <NodeStyle-Font--ClearDefaults>false</NodeStyle-Font--ClearDefaults>\r\n  <NodeStyle-ForeColor>Black</NodeStyle-ForeColor>\r\n  <NodeStyle-HorizontalPadding>0</NodeStyle-HorizontalPadding>\r\n  <NodeStyle-NodeSpacing>0</NodeStyle-NodeSpacing>\r\n  <NodeStyle-VerticalPadding>0</NodeStyle-VerticalPadding>\r\n  <ParentNodeStyle-Font-Bold>false</ParentNodeStyle-Font-Bold>\r\n  <ParentNodeStyle-Font--ClearDefaults>false</ParentNodeStyle-Font--ClearDefaults>\r\n  <ParentNodeStyle-ForeColor></ParentNodeStyle-ForeColor>\r\n  <SelectedNodeStyle-BackColor></SelectedNodeStyle-BackColor>\r\n  <SelectedNodeStyle-BorderColor></SelectedNodeStyle-BorderColor>\r\n  <SelectedNodeStyle-BorderStyle>NotSet</SelectedNodeStyle-BorderStyle>\r\n  <SelectedNodeStyle-BorderWidth></SelectedNodeStyle-BorderWidth>\r\n  <SelectedNodeStyle-Font-Underline>true</SelectedNodeStyle-Font-Underline>\r\n  <SelectedNodeStyle-Font--ClearDefaults>false</SelectedNodeStyle-Font--ClearDefaults>\r\n  <SelectedNodeStyle-ForeColor>#5555DD</SelectedNodeStyle-ForeColor>\r\n  <SelectedNodeStyle-HorizontalPadding>0</SelectedNodeStyle-HorizontalPadding>\r\n  <SelectedNodeStyle-VerticalPadding>0</SelectedNodeStyle-VerticalPadding>\r\n  <HoverNodeStyle-BackColor></HoverNodeStyle-BackColor>\r\n  <HoverNodeStyle-BorderColor></HoverNodeStyle-BorderColor>\r\n  <HoverNodeStyle-BorderStyle>NotSet</HoverNodeStyle-BorderStyle>\r\n  <HoverNodeStyle-BorderWidth></HoverNodeStyle-BorderWidth>\r\n  <HoverNodeStyle-Font-Underline>true</HoverNodeStyle-Font-Underline>\r\n  <HoverNodeStyle-Font--ClearDefaults>false</HoverNodeStyle-Font--ClearDefaults>\r\n  <HoverNodeStyle-ForeColor>#5555DD</HoverNodeStyle-ForeColor>\r\n</Scheme>\r\n<Scheme>\r\n  <SchemeName>TVScheme_BulletedList3</SchemeName>\r\n  <ImageSet>BulletedList3</ImageSet>\r\n  <NodeIndent>20</NodeIndent>\r\n  <ShowLines>false</ShowLines>\r\n  <ShowExpandCollapse>false</ShowExpandCollapse>\r\n  <NodeStyle-Font-Size>8</NodeStyle-Font-Size>\r\n  <NodeStyle-Font-Names>Verdana</NodeStyle-Font-Names>\r\n  <NodeStyle-Font--ClearDefaults>false</NodeStyle-Font--ClearDefaults>\r\n  <NodeStyle-ForeColor>Black</NodeStyle-ForeColor>\r\n  <NodeStyle-HorizontalPadding>5</NodeStyle-HorizontalPadding>\r\n  <NodeStyle-NodeSpacing>0</NodeStyle-NodeSpacing>\r\n  <NodeStyle-VerticalPadding>0</NodeStyle-VerticalPadding>\r\n  <ParentNodeStyle-Font-Bold>false</ParentNodeStyle-Font-Bold>\r\n  <ParentNodeStyle-Font--ClearDefaults>false</ParentNodeStyle-Font--ClearDefaults>\r\n  <ParentNodeStyle-ForeColor></ParentNodeStyle-ForeColor>\r\n  <SelectedNodeStyle-BackColor></SelectedNodeStyle-BackColor>\r\n  <SelectedNodeStyle-BorderColor></SelectedNodeStyle-BorderColor>\r\n  <SelectedNodeStyle-BorderStyle>NotSet</SelectedNodeStyle-BorderStyle>\r\n  <SelectedNodeStyle-BorderWidth></SelectedNodeStyle-BorderWidth>\r\n  <SelectedNodeStyle-Font-Underline>true</SelectedNodeStyle-Font-Underline>\r\n  <SelectedNodeStyle-Font--ClearDefaults>false</SelectedNodeStyle-Font--ClearDefaults>\r\n  <SelectedNodeStyle-ForeColor>#5555DD</SelectedNodeStyle-ForeColor>\r\n  <SelectedNodeStyle-HorizontalPadding>0</SelectedNodeStyle-HorizontalPadding>\r\n  <SelectedNodeStyle-VerticalPadding>0</SelectedNodeStyle-VerticalPadding>\r\n  <HoverNodeStyle-BackColor></HoverNodeStyle-BackColor>\r\n  <HoverNodeStyle-BorderColor></HoverNodeStyle-BorderColor>\r\n  <HoverNodeStyle-BorderStyle>NotSet</HoverNodeStyle-BorderStyle>\r\n  <HoverNodeStyle-BorderWidth></HoverNodeStyle-BorderWidth>\r\n  <HoverNodeStyle-Font-Underline>true</HoverNodeStyle-Font-Underline>\r\n  <HoverNodeStyle-Font--ClearDefaults>false</HoverNodeStyle-Font--ClearDefaults>\r\n  <HoverNodeStyle-ForeColor>#5555DD</HoverNodeStyle-ForeColor>\r\n</Scheme>\r\n<Scheme>\r\n  <SchemeName>TVScheme_BulletedList4</SchemeName>\r\n  <ImageSet>BulletedList</ImageSet>\r\n  <NodeIndent>20</NodeIndent>\r\n  <ShowLines>false</ShowLines>\r\n  <ShowExpandCollapse>false</ShowExpandCollapse>\r\n  <NodeStyle-Font-Size>10</NodeStyle-Font-Size>\r\n  <NodeStyle-Font-Names>Tahoma</NodeStyle-Font-Names>\r\n  <NodeStyle-Font--ClearDefaults>false</NodeStyle-Font--ClearDefaults>\r\n  <NodeStyle-ForeColor>Black</NodeStyle-ForeColor>\r\n  <NodeStyle-HorizontalPadding>5</NodeStyle-HorizontalPadding>\r\n  <NodeStyle-NodeSpacing>0</NodeStyle-NodeSpacing>\r\n  <NodeStyle-VerticalPadding>0</NodeStyle-VerticalPadding>\r\n  <ParentNodeStyle-Font-Bold>false</ParentNodeStyle-Font-Bold>\r\n  <ParentNodeStyle-Font--ClearDefaults>false</ParentNodeStyle-Font--ClearDefaults>\r\n  <ParentNodeStyle-ForeColor></ParentNodeStyle-ForeColor>\r\n  <SelectedNodeStyle-BackColor></SelectedNodeStyle-BackColor>\r\n  <SelectedNodeStyle-BorderColor></SelectedNodeStyle-BorderColor>\r\n  <SelectedNodeStyle-BorderStyle>NotSet</SelectedNodeStyle-BorderStyle>\r\n  <SelectedNodeStyle-BorderWidth></SelectedNodeStyle-BorderWidth>\r\n  <SelectedNodeStyle-Font-Underline>true</SelectedNodeStyle-Font-Underline>\r\n  <SelectedNodeStyle-Font--ClearDefaults>false</SelectedNodeStyle-Font--ClearDefaults>\r\n  <SelectedNodeStyle-ForeColor>#5555DD</SelectedNodeStyle-ForeColor>\r\n  <SelectedNodeStyle-HorizontalPadding>0</SelectedNodeStyle-HorizontalPadding>\r\n  <SelectedNodeStyle-VerticalPadding>0</SelectedNodeStyle-VerticalPadding>\r\n  <HoverNodeStyle-BackColor></HoverNodeStyle-BackColor>\r\n  <HoverNodeStyle-BorderColor></HoverNodeStyle-BorderColor>\r\n  <HoverNodeStyle-BorderStyle>NotSet</HoverNodeStyle-BorderStyle>\r\n  <HoverNodeStyle-BorderWidth></HoverNodeStyle-BorderWidth>\r\n  <HoverNodeStyle-Font-Underline>true</HoverNodeStyle-Font-Underline>\r\n  <HoverNodeStyle-Font--ClearDefaults>false</HoverNodeStyle-Font--ClearDefaults>\r\n  <HoverNodeStyle-ForeColor>#5555DD</HoverNodeStyle-ForeColor>\r\n</Scheme>\r\n<Scheme>\r\n  <SchemeName>TVScheme_BulletedList5</SchemeName>\r\n  <ImageSet>BulletedList2</ImageSet>\r\n  <NodeIndent>20</NodeIndent>\r\n  <ShowLines>false</ShowLines>\r\n  <ShowExpandCollapse>false</ShowExpandCollapse>\r\n  <NodeStyle-Font-Size>10</NodeStyle-Font-Size>\r\n  <NodeStyle-Font-Names>Tahoma</NodeStyle-Font-Names>\r\n  <NodeStyle-Font--ClearDefaults>false</NodeStyle-Font--ClearDefaults>\r\n  <NodeStyle-ForeColor>Black</NodeStyle-ForeColor>\r\n  <NodeStyle-HorizontalPadding>5</NodeStyle-HorizontalPadding>\r\n  <NodeStyle-NodeSpacing>0</NodeStyle-NodeSpacing>\r\n  <NodeStyle-VerticalPadding>0</NodeStyle-VerticalPadding>\r\n  <ParentNodeStyle-Font-Bold>false</ParentNodeStyle-Font-Bold>\r\n  <ParentNodeStyle-Font--ClearDefaults>false</ParentNodeStyle-Font--ClearDefaults>\r\n  <ParentNodeStyle-ForeColor></ParentNodeStyle-ForeColor>\r\n  <SelectedNodeStyle-BackColor></SelectedNodeStyle-BackColor>\r\n  <SelectedNodeStyle-BorderColor></SelectedNodeStyle-BorderColor>\r\n  <SelectedNodeStyle-BorderStyle>NotSet</SelectedNodeStyle-BorderStyle>\r\n  <SelectedNodeStyle-BorderWidth></SelectedNodeStyle-BorderWidth>\r\n  <SelectedNodeStyle-Font-Underline>true</SelectedNodeStyle-Font-Underline>\r\n  <SelectedNodeStyle-Font--ClearDefaults>false</SelectedNodeStyle-Font--ClearDefaults>\r\n  <SelectedNodeStyle-ForeColor>#5555DD</SelectedNodeStyle-ForeColor>\r\n  <SelectedNodeStyle-HorizontalPadding>0</SelectedNodeStyle-HorizontalPadding>\r\n  <SelectedNodeStyle-VerticalPadding>0</SelectedNodeStyle-VerticalPadding>\r\n  <HoverNodeStyle-BackColor></HoverNodeStyle-BackColor>\r\n  <HoverNodeStyle-BorderColor></HoverNodeStyle-BorderColor>\r\n  <HoverNodeStyle-BorderStyle>NotSet</HoverNodeStyle-BorderStyle>\r\n  <HoverNodeStyle-BorderWidth></HoverNodeStyle-BorderWidth>\r\n  <HoverNodeStyle-Font-Underline>true</HoverNodeStyle-Font-Underline>\r\n  <HoverNodeStyle-Font--ClearDefaults>false</HoverNodeStyle-Font--ClearDefaults>\r\n  <HoverNodeStyle-ForeColor>#5555DD</HoverNodeStyle-ForeColor>\r\n</Scheme>\r\n<Scheme>\r\n  <SchemeName>TVScheme_BulletedList6</SchemeName>\r\n  <ImageSet>BulletedList4</ImageSet>\r\n  <NodeIndent>20</NodeIndent>\r\n  <ShowLines>false</ShowLines>\r\n  <ShowExpandCollapse>false</ShowExpandCollapse>\r\n  <NodeStyle-Font-Size>10</NodeStyle-Font-Size>\r\n  <NodeStyle-Font-Names>Tahoma</NodeStyle-Font-Names>\r\n  <NodeStyle-Font--ClearDefaults>false</NodeStyle-Font--ClearDefaults>\r\n  <NodeStyle-ForeColor>Black</NodeStyle-ForeColor>\r\n  <NodeStyle-HorizontalPadding>5</NodeStyle-HorizontalPadding>\r\n  <NodeStyle-NodeSpacing>0</NodeStyle-NodeSpacing>\r\n  <NodeStyle-VerticalPadding>0</NodeStyle-VerticalPadding>\r\n  <ParentNodeStyle-Font-Bold>false</ParentNodeStyle-Font-Bold>\r\n  <ParentNodeStyle-Font--ClearDefaults>false</ParentNodeStyle-Font--ClearDefaults>\r\n  <ParentNodeStyle-ForeColor></ParentNodeStyle-ForeColor>\r\n  <SelectedNodeStyle-BackColor></SelectedNodeStyle-BackColor>\r\n  <SelectedNodeStyle-BorderColor></SelectedNodeStyle-BorderColor>\r\n  <SelectedNodeStyle-BorderStyle>NotSet</SelectedNodeStyle-BorderStyle>\r\n  <SelectedNodeStyle-BorderWidth></SelectedNodeStyle-BorderWidth>\r\n  <SelectedNodeStyle-Font-Underline>true</SelectedNodeStyle-Font-Underline>[...string is too long...]", (DataRow schemeData) => new BaseAutoFormat(schemeData));
				}
				return TreeViewDesigner._autoFormats;
			}
		}

		// Token: 0x17000884 RID: 2180
		// (get) Token: 0x06002D59 RID: 11609 RVA: 0x00100F87 File Offset: 0x000FFF87
		protected override bool UsePreviewControl
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06002D5A RID: 11610 RVA: 0x00100F8A File Offset: 0x000FFF8A
		protected void CreateLineImages()
		{
			ControlDesigner.InvokeTransactedChange(base.Component, new TransactedChangeCallback(this.CreateLineImagesCallBack), null, SR.GetString("TreeViewDesigner_CreateLineImagesTransactionDescription"));
		}

		// Token: 0x06002D5B RID: 11611 RVA: 0x00100FB0 File Offset: 0x000FFFB0
		private bool CreateLineImagesCallBack(object context)
		{
			TreeViewImageGenerator form = new TreeViewImageGenerator(this._treeView);
			return UIServiceHelper.ShowDialog(base.Component.Site, form) == DialogResult.OK;
		}

		// Token: 0x06002D5C RID: 11612 RVA: 0x00100FE0 File Offset: 0x000FFFE0
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

		// Token: 0x06002D5D RID: 11613 RVA: 0x00101074 File Offset: 0x00100074
		protected void EditBindings()
		{
			IServiceProvider site = this._treeView.Site;
			TreeViewBindingsEditorForm form = new TreeViewBindingsEditorForm(site, this._treeView, this);
			UIServiceHelper.ShowDialog(site, form);
		}

		// Token: 0x06002D5E RID: 11614 RVA: 0x001010A4 File Offset: 0x001000A4
		protected void EditNodes()
		{
			PropertyDescriptor member = TypeDescriptor.GetProperties(base.Component)["Nodes"];
			ControlDesigner.InvokeTransactedChange(base.Component, new TransactedChangeCallback(this.EditNodesChangeCallback), null, SR.GetString("TreeViewDesigner_EditNodesTransactionDescription"), member);
		}

		// Token: 0x06002D5F RID: 11615 RVA: 0x001010EC File Offset: 0x001000EC
		private bool EditNodesChangeCallback(object context)
		{
			IServiceProvider site = this._treeView.Site;
			TreeNodeCollectionEditorDialog form = new TreeNodeCollectionEditorDialog(this._treeView, this);
			DialogResult dialogResult = UIServiceHelper.ShowDialog(site, form);
			return dialogResult == DialogResult.OK;
		}

		// Token: 0x06002D60 RID: 11616 RVA: 0x00101120 File Offset: 0x00100120
		private void ExpandToDepth(System.Web.UI.WebControls.TreeNodeCollection nodes, int depth)
		{
			foreach (object obj in nodes)
			{
				System.Web.UI.WebControls.TreeNode treeNode = (System.Web.UI.WebControls.TreeNode)obj;
				if (treeNode.Expanded != false && (depth == -1 || treeNode.Depth < depth))
				{
					treeNode.Expanded = new bool?(true);
					this.ExpandToDepth(treeNode.ChildNodes, depth);
				}
			}
		}

		// Token: 0x06002D61 RID: 11617 RVA: 0x001011B4 File Offset: 0x001001B4
		protected override IHierarchicalEnumerable GetSampleDataSource()
		{
			this._usingSampleData = true;
			((System.Web.UI.WebControls.TreeView)base.ViewControl).AutoGenerateDataBindings = true;
			return base.GetSampleDataSource();
		}

		// Token: 0x06002D62 RID: 11618 RVA: 0x001011D4 File Offset: 0x001001D4
		public override string GetDesignTimeHtml()
		{
			string result = base.GetDesignTimeHtml();
			if (this._emptyDataBinding)
			{
				result = this.GetEmptyDataBindingDesignTimeHtml();
			}
			return result;
		}

		// Token: 0x06002D63 RID: 11619 RVA: 0x001011F8 File Offset: 0x001001F8
		private string GetEmptyDataBindingDesignTimeHtml()
		{
			string name = this._treeView.Site.Name;
			return string.Format(CultureInfo.CurrentUICulture, "\r\n                <table cellpadding=4 cellspacing=0 style=\"font-family:Tahoma;font-size:8pt;color:buttontext;background-color:buttonface\">\r\n                  <tr><td><span style=\"font-weight:bold\">TreeView</span> - {0}</td></tr>\r\n                  <tr><td>{1}</td></tr>\r\n                </table>\r\n             ", new object[]
			{
				name,
				SR.GetString("TreeViewDesigner_EmptyDataBinding")
			});
		}

		// Token: 0x06002D64 RID: 11620 RVA: 0x00101240 File Offset: 0x00100240
		protected override string GetEmptyDesignTimeHtml()
		{
			string name = this._treeView.Site.Name;
			return string.Format(CultureInfo.CurrentUICulture, "\r\n                <table cellpadding=4 cellspacing=0 style=\"font-family:Tahoma;font-size:8pt;color:buttontext;background-color:buttonface\">\r\n                  <tr><td><span style=\"font-weight:bold\">TreeView</span> - {0}</td></tr>\r\n                  <tr><td>{1}</td></tr>\r\n                </table>\r\n             ", new object[]
			{
				name,
				SR.GetString("TreeViewDesigner_Empty")
			});
		}

		// Token: 0x06002D65 RID: 11621 RVA: 0x00101288 File Offset: 0x00100288
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

		// Token: 0x06002D66 RID: 11622 RVA: 0x001012DF File Offset: 0x001002DF
		public override void Initialize(IComponent component)
		{
			ControlDesigner.VerifyInitializeArgument(component, typeof(System.Web.UI.WebControls.TreeView));
			base.Initialize(component);
			this._treeView = (System.Web.UI.WebControls.TreeView)component;
		}

		// Token: 0x06002D67 RID: 11623 RVA: 0x00101304 File Offset: 0x00100304
		internal void InvokeTreeNodeCollectionEditor()
		{
			this.EditNodes();
		}

		// Token: 0x06002D68 RID: 11624 RVA: 0x0010130C File Offset: 0x0010030C
		internal void InvokeTreeViewBindingsEditor()
		{
			this.EditBindings();
		}

		// Token: 0x04001EDB RID: 7899
		private const string emptyDesignTimeHtml = "\r\n                <table cellpadding=4 cellspacing=0 style=\"font-family:Tahoma;font-size:8pt;color:buttontext;background-color:buttonface\">\r\n                  <tr><td><span style=\"font-weight:bold\">TreeView</span> - {0}</td></tr>\r\n                  <tr><td>{1}</td></tr>\r\n                </table>\r\n             ";

		// Token: 0x04001EDC RID: 7900
		private const string errorDesignTimeHtml = "\r\n                <table cellpadding=4 cellspacing=0 style=\"font-family:Tahoma;font-size:8pt;color:buttontext;background-color:buttonface;border: solid 1px;border-top-color:buttonhighlight;border-left-color:buttonhighlight;border-bottom-color:buttonshadow;border-right-color:buttonshadow\">\r\n                  <tr><td><span style=\"font-weight:bold\">TreeView</span> - {0}</td></tr>\r\n                  <tr><td>{1}</td></tr>\r\n                </table>\r\n             ";

		// Token: 0x04001EDD RID: 7901
		private System.Web.UI.WebControls.TreeView _treeView;

		// Token: 0x04001EDE RID: 7902
		private bool _usingSampleData;

		// Token: 0x04001EDF RID: 7903
		private bool _emptyDataBinding;

		// Token: 0x04001EE0 RID: 7904
		private static DesignerAutoFormatCollection _autoFormats;

		// Token: 0x04001EE1 RID: 7905
		[CompilerGenerated]
		private static ControlDesigner.CreateAutoFormatDelegate <>9__CachedAnonymousMethodDelegate1;

		// Token: 0x020004F5 RID: 1269
		private class TreeViewDesignerActionList : DesignerActionList
		{
			// Token: 0x06002D6B RID: 11627 RVA: 0x0010131C File Offset: 0x0010031C
			public TreeViewDesignerActionList(TreeViewDesigner parent) : base(parent.Component)
			{
				this._parent = parent;
			}

			// Token: 0x17000885 RID: 2181
			// (get) Token: 0x06002D6C RID: 11628 RVA: 0x00101331 File Offset: 0x00100331
			// (set) Token: 0x06002D6D RID: 11629 RVA: 0x00101334 File Offset: 0x00100334
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

			// Token: 0x17000886 RID: 2182
			// (get) Token: 0x06002D6E RID: 11630 RVA: 0x00101336 File Offset: 0x00100336
			// (set) Token: 0x06002D6F RID: 11631 RVA: 0x00101348 File Offset: 0x00100348
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

			// Token: 0x06002D70 RID: 11632 RVA: 0x0010138C File Offset: 0x0010038C
			public void CreateLineImages()
			{
				this._parent.CreateLineImages();
			}

			// Token: 0x06002D71 RID: 11633 RVA: 0x00101399 File Offset: 0x00100399
			public void EditBindings()
			{
				this._parent.EditBindings();
			}

			// Token: 0x06002D72 RID: 11634 RVA: 0x001013A6 File Offset: 0x001003A6
			public void EditNodes()
			{
				this._parent.EditNodes();
			}

			// Token: 0x06002D73 RID: 11635 RVA: 0x001013B4 File Offset: 0x001003B4
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

			// Token: 0x04001EE2 RID: 7906
			private TreeViewDesigner _parent;
		}
	}
}
