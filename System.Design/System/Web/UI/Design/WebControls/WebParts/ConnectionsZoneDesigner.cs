using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Runtime.CompilerServices;
using System.Security.Permissions;
using System.Web.UI.WebControls.WebParts;

namespace System.Web.UI.Design.WebControls.WebParts
{
	// Token: 0x02000536 RID: 1334
	[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
	public class ConnectionsZoneDesigner : ToolZoneDesigner
	{
		// Token: 0x170008ED RID: 2285
		// (get) Token: 0x06002F3F RID: 12095 RVA: 0x0010DCCA File Offset: 0x0010CCCA
		public override DesignerAutoFormatCollection AutoFormats
		{
			get
			{
				if (ConnectionsZoneDesigner._autoFormats == null)
				{
					ConnectionsZoneDesigner._autoFormats = ControlDesigner.CreateAutoFormats("<Schemes>\r\n<xsd:schema id=\"Schemes\" xmlns=\"\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns:msdata=\"urn:schemas-microsoft-com:xml-msdata\">\r\n  <xsd:element name=\"Scheme\">\r\n     <xsd:complexType>\r\n       <xsd:all>\r\n        <xsd:element name=\"SchemeName\" type=\"xsd:string\"/>\r\n        <xsd:element name=\"BackColor\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n        <xsd:element name=\"BorderColor\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n        <xsd:element name=\"BorderWidth\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n        <xsd:element name=\"EditUIStyle-Font-Names\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n        <xsd:element name=\"EditUIStyle-Font-Size\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n        <xsd:element name=\"EditUIStyle-ForeColor\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n        <xsd:element name=\"Font-Names\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n        <xsd:element name=\"FooterStyle-BackColor\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n        <xsd:element name=\"FooterStyle-HorizontalAlign\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n        <xsd:element name=\"HeaderStyle-BackColor\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n        <xsd:element name=\"HeaderStyle-Font-Bold\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n        <xsd:element name=\"HeaderStyle-Font-Size\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n        <xsd:element name=\"HeaderStyle-Font--ClearDefaults\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n        <xsd:element name=\"HeaderStyle-ForeColor\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n        <xsd:element name=\"HeaderVerbStyle-Font-Bold\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n        <xsd:element name=\"HeaderVerbStyle-Font-Size\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n        <xsd:element name=\"HeaderVerbStyle-Font-Underline\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n        <xsd:element name=\"HeaderVerbStyle-Font--ClearDefaults\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n        <xsd:element name=\"HeaderVerbStyle-ForeColor\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n        <xsd:element name=\"InstructionTextStyle-Font-Size\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n        <xsd:element name=\"InstructionTextStyle-ForeColor\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n        <xsd:element name=\"LabelStyle-Font-Size\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n        <xsd:element name=\"LabelStyle-ForeColor\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n        <xsd:element name=\"Padding\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n        <xsd:element name=\"VerbStyle-Font-Names\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n        <xsd:element name=\"VerbStyle-Font-Size\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n        <xsd:element name=\"VerbStyle-ForeColor\" minOccurs=\"0\" type=\"xsd:string\"/>\r\n      </xsd:all>\r\n    </xsd:complexType>\r\n  </xsd:element>\r\n  <xsd:element name=\"Schemes\" msdata:IsDataSet=\"true\">\r\n    <xsd:complexType>\r\n      <xsd:choice maxOccurs=\"unbounded\">\r\n        <xsd:element ref=\"Scheme\"/>\r\n      </xsd:choice>\r\n    </xsd:complexType>\r\n  </xsd:element>\r\n</xsd:schema>\r\n<Scheme>\r\n  <SchemeName>WebPartScheme_Empty</SchemeName>\r\n  <HeaderStyle-Font-Bold>False</HeaderStyle-Font-Bold>\r\n  <HeaderStyle-Font--ClearDefaults>True</HeaderStyle-Font--ClearDefaults>\r\n  <HeaderVerbStyle-Font-Bold>False</HeaderVerbStyle-Font-Bold>\r\n  <HeaderVerbStyle-Font-Underline>False</HeaderVerbStyle-Font-Underline>\r\n  <HeaderVerbStyle-Font--ClearDefaults>True</HeaderVerbStyle-Font--ClearDefaults>\r\n  <Padding>2</Padding>\r\n</Scheme>\r\n<Scheme>\r\n  <SchemeName>WebPartScheme_Professional</SchemeName>\r\n  <BackColor>#F7F6F3</BackColor>\r\n  <BorderColor>#CCCCCC</BorderColor>\r\n  <BorderWidth>1px</BorderWidth>\r\n  <EditUIStyle-Font-Names>Verdana</EditUIStyle-Font-Names>\r\n  <EditUIStyle-Font-Size>0.8em</EditUIStyle-Font-Size>\r\n  <EditUIStyle-ForeColor>#333333</EditUIStyle-ForeColor>\r\n  <Font-Names>Verdana</Font-Names>\r\n  <FooterStyle-BackColor>#E2DED6</FooterStyle-BackColor>\r\n  <FooterStyle-HorizontalAlign>Right</FooterStyle-HorizontalAlign>\r\n  <HeaderStyle-BackColor>#E2DED6</HeaderStyle-BackColor>\r\n  <HeaderStyle-Font-Bold>True</HeaderStyle-Font-Bold>\r\n  <HeaderStyle-Font-Size>0.8em</HeaderStyle-Font-Size>\r\n  <HeaderStyle-ForeColor>#333333</HeaderStyle-ForeColor>\r\n  <HeaderVerbStyle-Font-Bold>False</HeaderVerbStyle-Font-Bold>\r\n  <HeaderVerbStyle-Font-Size>0.8em</HeaderVerbStyle-Font-Size>\r\n  <HeaderVerbStyle-Font-Underline>False</HeaderVerbStyle-Font-Underline>\r\n  <HeaderVerbStyle-ForeColor>#333333</HeaderVerbStyle-ForeColor>\r\n  <InstructionTextStyle-Font-Size>0.8em</InstructionTextStyle-Font-Size>\r\n  <InstructionTextStyle-ForeColor>#333333</InstructionTextStyle-ForeColor>\r\n  <LabelStyle-Font-Size>0.8em</LabelStyle-Font-Size>\r\n  <LabelStyle-ForeColor>#333333</LabelStyle-ForeColor>\r\n  <Padding>6</Padding>\r\n  <VerbStyle-Font-Names>Verdana</VerbStyle-Font-Names>\r\n  <VerbStyle-Font-Size>0.8em</VerbStyle-Font-Size>\r\n  <VerbStyle-ForeColor>#333333</VerbStyle-ForeColor>\r\n</Scheme>\r\n<Scheme>\r\n  <SchemeName>WebPartScheme_Simple</SchemeName>\r\n  <BackColor>#E3EAEB</BackColor>\r\n  <BorderColor>#CCCCCC</BorderColor>\r\n  <BorderWidth>1px</BorderWidth>\r\n  <EditUIStyle-Font-Names>Verdana</EditUIStyle-Font-Names>\r\n  <EditUIStyle-Font-Size>0.8em</EditUIStyle-Font-Size>\r\n  <EditUIStyle-ForeColor>#333333</EditUIStyle-ForeColor>\r\n  <Font-Names>Verdana</Font-Names>\r\n  <FooterStyle-BackColor>#C5BBAF</FooterStyle-BackColor>\r\n  <FooterStyle-HorizontalAlign>Right</FooterStyle-HorizontalAlign>\r\n  <HeaderStyle-BackColor>#C5BBAF</HeaderStyle-BackColor>\r\n  <HeaderStyle-Font-Bold>True</HeaderStyle-Font-Bold>\r\n  <HeaderStyle-Font-Size>0.8em</HeaderStyle-Font-Size>\r\n  <HeaderStyle-ForeColor>#333333</HeaderStyle-ForeColor>\r\n  <HeaderVerbStyle-Font-Bold>False</HeaderVerbStyle-Font-Bold>\r\n  <HeaderVerbStyle-Font-Size>0.8em</HeaderVerbStyle-Font-Size>\r\n  <HeaderVerbStyle-Font-Underline>False</HeaderVerbStyle-Font-Underline>\r\n  <HeaderVerbStyle-ForeColor>#333333</HeaderVerbStyle-ForeColor>\r\n  <InstructionTextStyle-Font-Size>0.8em</InstructionTextStyle-Font-Size>\r\n  <InstructionTextStyle-ForeColor>#333333</InstructionTextStyle-ForeColor>\r\n  <LabelStyle-Font-Size>0.8em</LabelStyle-Font-Size>\r\n  <LabelStyle-ForeColor>#333333</LabelStyle-ForeColor>\r\n  <Padding>6</Padding>\r\n  <VerbStyle-Font-Names>Verdana</VerbStyle-Font-Names>\r\n  <VerbStyle-Font-Size>0.8em</VerbStyle-Font-Size>\r\n  <VerbStyle-ForeColor>#333333</VerbStyle-ForeColor>\r\n</Scheme>\r\n<Scheme>\r\n  <SchemeName>WebPartScheme_Classic</SchemeName>\r\n  <BackColor>#EFF3FB</BackColor>\r\n  <BorderColor>#CCCCCC</BorderColor>\r\n  <BorderWidth>1px</BorderWidth>\r\n  <EditUIStyle-Font-Names>Verdana</EditUIStyle-Font-Names>\r\n  <EditUIStyle-Font-Size>0.8em</EditUIStyle-Font-Size>\r\n  <EditUIStyle-ForeColor>#333333</EditUIStyle-ForeColor>\r\n  <Font-Names>Verdana</Font-Names>\r\n  <FooterStyle-BackColor>#D1DDF1</FooterStyle-BackColor>\r\n  <FooterStyle-HorizontalAlign>Right</FooterStyle-HorizontalAlign>\r\n  <HeaderStyle-BackColor>#D1DDF1</HeaderStyle-BackColor>\r\n  <HeaderStyle-Font-Bold>True</HeaderStyle-Font-Bold>\r\n  <HeaderStyle-Font-Size>0.8em</HeaderStyle-Font-Size>\r\n  <HeaderStyle-ForeColor>#333333</HeaderStyle-ForeColor>\r\n  <HeaderVerbStyle-Font-Bold>False</HeaderVerbStyle-Font-Bold>\r\n  <HeaderVerbStyle-Font-Size>0.8em</HeaderVerbStyle-Font-Size>\r\n  <HeaderVerbStyle-Font-Underline>False</HeaderVerbStyle-Font-Underline>\r\n  <HeaderVerbStyle-ForeColor>#333333</HeaderVerbStyle-ForeColor>\r\n  <InstructionTextStyle-Font-Size>0.8em</InstructionTextStyle-Font-Size>\r\n  <InstructionTextStyle-ForeColor>#333333</InstructionTextStyle-ForeColor>\r\n  <LabelStyle-Font-Size>0.8em</LabelStyle-Font-Size>\r\n  <LabelStyle-ForeColor>#333333</LabelStyle-ForeColor>\r\n  <Padding>6</Padding>\r\n  <VerbStyle-Font-Names>Verdana</VerbStyle-Font-Names>\r\n  <VerbStyle-Font-Size>0.8em</VerbStyle-Font-Size>\r\n  <VerbStyle-ForeColor>#333333</VerbStyle-ForeColor>\r\n</Scheme>\r\n<Scheme>\r\n  <SchemeName>WebPartScheme_Colorful</SchemeName>\r\n  <BackColor>#FFFBD6</BackColor>\r\n  <BorderColor>#CCCCCC</BorderColor>\r\n  <BorderWidth>1px</BorderWidth>\r\n  <EditUIStyle-Font-Names>Verdana</EditUIStyle-Font-Names>\r\n  <EditUIStyle-Font-Size>0.8em</EditUIStyle-Font-Size>\r\n  <EditUIStyle-ForeColor>#333333</EditUIStyle-ForeColor>\r\n  <Font-Names>Verdana</Font-Names>\r\n  <FooterStyle-BackColor>#FFCC66</FooterStyle-BackColor>\r\n  <FooterStyle-HorizontalAlign>Right</FooterStyle-HorizontalAlign>\r\n  <HeaderStyle-BackColor>#FFCC66</HeaderStyle-BackColor>\r\n  <HeaderStyle-Font-Bold>True</HeaderStyle-Font-Bold>\r\n  <HeaderStyle-Font-Size>0.8em</HeaderStyle-Font-Size>\r\n  <HeaderStyle-ForeColor>#333333</HeaderStyle-ForeColor>\r\n  <HeaderVerbStyle-Font-Bold>False</HeaderVerbStyle-Font-Bold>\r\n  <HeaderVerbStyle-Font-Size>0.8em</HeaderVerbStyle-Font-Size>\r\n  <HeaderVerbStyle-Font-Underline>False</HeaderVerbStyle-Font-Underline>\r\n  <HeaderVerbStyle-ForeColor>#333333</HeaderVerbStyle-ForeColor>\r\n  <InstructionTextStyle-Font-Size>0.8em</InstructionTextStyle-Font-Size>\r\n  <InstructionTextStyle-ForeColor>#333333</InstructionTextStyle-ForeColor>\r\n  <LabelStyle-Font-Size>0.8em</LabelStyle-Font-Size>\r\n  <LabelStyle-ForeColor>#333333</LabelStyle-ForeColor>\r\n  <Padding>6</Padding>\r\n  <VerbStyle-Font-Names>Verdana</VerbStyle-Font-Names>\r\n  <VerbStyle-Font-Size>0.8em</VerbStyle-Font-Size>\r\n  <VerbStyle-ForeColor>#333333</VerbStyle-ForeColor>\r\n</Scheme>\r\n</Schemes>\r\n", (DataRow schemeData) => new ConnectionsZoneAutoFormat(schemeData));
				}
				return ConnectionsZoneDesigner._autoFormats;
			}
		}

		// Token: 0x06002F40 RID: 12096 RVA: 0x0010DD04 File Offset: 0x0010CD04
		public override string GetDesignTimeHtml()
		{
			string result;
			try
			{
				ConnectionsZone connectionsZone = (ConnectionsZone)base.ViewControl;
				result = base.GetDesignTimeHtml();
				if (base.ViewInBrowseMode && connectionsZone.ID != "AutoFormatPreviewControl")
				{
					result = base.CreatePlaceHolderDesignTimeHtml();
				}
			}
			catch (Exception e)
			{
				result = this.GetErrorDesignTimeHtml(e);
			}
			return result;
		}

		// Token: 0x06002F41 RID: 12097 RVA: 0x0010DD64 File Offset: 0x0010CD64
		public override void Initialize(IComponent component)
		{
			ControlDesigner.VerifyInitializeArgument(component, typeof(ConnectionsZone));
			base.Initialize(component);
			this._zone = (ConnectionsZone)component;
		}

		// Token: 0x06002F42 RID: 12098 RVA: 0x0010DD8C File Offset: 0x0010CD8C
		protected override void PreFilterProperties(IDictionary properties)
		{
			base.PreFilterProperties(properties);
			Attribute[] attributes = new Attribute[]
			{
				new BrowsableAttribute(false),
				new EditorBrowsableAttribute(EditorBrowsableState.Never),
				new ThemeableAttribute(false)
			};
			foreach (string key in ConnectionsZoneDesigner._hiddenProperties)
			{
				PropertyDescriptor propertyDescriptor = (PropertyDescriptor)properties[key];
				if (propertyDescriptor != null)
				{
					properties[key] = TypeDescriptor.CreateProperty(propertyDescriptor.ComponentType, propertyDescriptor, attributes);
				}
			}
		}

		// Token: 0x04002035 RID: 8245
		private static readonly string[] _hiddenProperties = new string[]
		{
			"EmptyZoneTextStyle",
			"PartChromeStyle",
			"PartStyle",
			"PartTitleStyle"
		};

		// Token: 0x04002036 RID: 8246
		private static DesignerAutoFormatCollection _autoFormats;

		// Token: 0x04002037 RID: 8247
		private ConnectionsZone _zone;

		// Token: 0x04002038 RID: 8248
		[CompilerGenerated]
		private static ControlDesigner.CreateAutoFormatDelegate <>9__CachedAnonymousMethodDelegate1;
	}
}
