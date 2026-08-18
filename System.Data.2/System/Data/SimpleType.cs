using System;
using System.Collections;
using System.Data.Common;
using System.Globalization;
using System.Runtime.Serialization;
using System.Xml;
using System.Xml.Schema;

namespace System.Data
{
	// Token: 0x02000129 RID: 297
	[Serializable]
	internal sealed class SimpleType : ISerializable
	{
		// Token: 0x060011CE RID: 4558 RVA: 0x00088B5C File Offset: 0x00087F5C
		internal SimpleType(string baseType)
		{
			this.baseType = baseType;
		}

		// Token: 0x060011CF RID: 4559 RVA: 0x00088BE4 File Offset: 0x00087FE4
		internal SimpleType(XmlSchemaSimpleType node)
		{
			this.name = node.Name;
			this.ns = ((node.QualifiedName != null) ? node.QualifiedName.Namespace : "");
			this.LoadTypeValues(node);
		}

		// Token: 0x060011D0 RID: 4560 RVA: 0x00088CA0 File Offset: 0x000880A0
		private SimpleType(SerializationInfo info, StreamingContext context)
		{
			this.baseType = info.GetString("SimpleType.BaseType");
			this.baseSimpleType = (SimpleType)info.GetValue("SimpleType.BaseSimpleType", typeof(SimpleType));
			if (info.GetBoolean("SimpleType.XmlBaseType.XmlQualifiedNameExists"))
			{
				string @string = info.GetString("SimpleType.XmlBaseType.Name");
				string string2 = info.GetString("SimpleType.XmlBaseType.Namespace");
				this.xmlBaseType = new XmlQualifiedName(@string, string2);
			}
			else
			{
				this.xmlBaseType = null;
			}
			this.name = info.GetString("SimpleType.Name");
			this.ns = info.GetString("SimpleType.NS");
			this.maxLength = info.GetInt32("SimpleType.MaxLength");
			this.length = info.GetInt32("SimpleType.Length");
		}

		// Token: 0x060011D1 RID: 4561 RVA: 0x00088DD0 File Offset: 0x000881D0
		void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue("SimpleType.BaseType", this.baseType);
			info.AddValue("SimpleType.BaseSimpleType", this.baseSimpleType);
			XmlQualifiedName xmlQualifiedName = this.xmlBaseType;
			info.AddValue("SimpleType.XmlBaseType.XmlQualifiedNameExists", xmlQualifiedName != null);
			info.AddValue("SimpleType.XmlBaseType.Name", (xmlQualifiedName != null) ? xmlQualifiedName.Name : null);
			info.AddValue("SimpleType.XmlBaseType.Namespace", (xmlQualifiedName != null) ? xmlQualifiedName.Namespace : null);
			info.AddValue("SimpleType.Name", this.name);
			info.AddValue("SimpleType.NS", this.ns);
			info.AddValue("SimpleType.MaxLength", this.maxLength);
			info.AddValue("SimpleType.Length", this.length);
		}

		// Token: 0x060011D2 RID: 4562 RVA: 0x00088E9C File Offset: 0x0008829C
		internal void LoadTypeValues(XmlSchemaSimpleType node)
		{
			if (node.Content is XmlSchemaSimpleTypeList || node.Content is XmlSchemaSimpleTypeUnion)
			{
				throw ExceptionBuilder.SimpleTypeNotSupported();
			}
			if (node.Content is XmlSchemaSimpleTypeRestriction)
			{
				XmlSchemaSimpleTypeRestriction xmlSchemaSimpleTypeRestriction = (XmlSchemaSimpleTypeRestriction)node.Content;
				XmlSchemaSimpleType xmlSchemaSimpleType = node.BaseXmlSchemaType as XmlSchemaSimpleType;
				if (xmlSchemaSimpleType != null && xmlSchemaSimpleType.QualifiedName.Namespace != "http://www.w3.org/2001/XMLSchema")
				{
					this.baseSimpleType = new SimpleType(node.BaseXmlSchemaType as XmlSchemaSimpleType);
				}
				if (xmlSchemaSimpleTypeRestriction.BaseTypeName.Namespace == "http://www.w3.org/2001/XMLSchema")
				{
					this.baseType = xmlSchemaSimpleTypeRestriction.BaseTypeName.Name;
				}
				else
				{
					this.baseType = xmlSchemaSimpleTypeRestriction.BaseTypeName.ToString();
				}
				if (this.baseSimpleType != null && this.baseSimpleType.Name != null && this.baseSimpleType.Name.Length > 0)
				{
					this.xmlBaseType = this.baseSimpleType.XmlBaseType;
				}
				else
				{
					this.xmlBaseType = xmlSchemaSimpleTypeRestriction.BaseTypeName;
				}
				if (this.baseType == null || this.baseType.Length == 0)
				{
					this.baseType = xmlSchemaSimpleTypeRestriction.BaseType.Name;
					this.xmlBaseType = null;
				}
				if (this.baseType == "NOTATION")
				{
					this.baseType = "string";
				}
				foreach (XmlSchemaObject xmlSchemaObject in xmlSchemaSimpleTypeRestriction.Facets)
				{
					XmlSchemaFacet xmlSchemaFacet = (XmlSchemaFacet)xmlSchemaObject;
					if (xmlSchemaFacet is XmlSchemaLengthFacet)
					{
						this.length = Convert.ToInt32(xmlSchemaFacet.Value, null);
					}
					if (xmlSchemaFacet is XmlSchemaMinLengthFacet)
					{
						this.minLength = Convert.ToInt32(xmlSchemaFacet.Value, null);
					}
					if (xmlSchemaFacet is XmlSchemaMaxLengthFacet)
					{
						this.maxLength = Convert.ToInt32(xmlSchemaFacet.Value, null);
					}
					if (xmlSchemaFacet is XmlSchemaPatternFacet)
					{
						this.pattern = xmlSchemaFacet.Value;
					}
					if (xmlSchemaFacet is XmlSchemaEnumerationFacet)
					{
						this.enumeration = ((!ADP.IsEmpty(this.enumeration)) ? (this.enumeration + " " + xmlSchemaFacet.Value) : xmlSchemaFacet.Value);
					}
					if (xmlSchemaFacet is XmlSchemaMinExclusiveFacet)
					{
						this.minExclusive = xmlSchemaFacet.Value;
					}
					if (xmlSchemaFacet is XmlSchemaMinInclusiveFacet)
					{
						this.minInclusive = xmlSchemaFacet.Value;
					}
					if (xmlSchemaFacet is XmlSchemaMaxExclusiveFacet)
					{
						this.maxExclusive = xmlSchemaFacet.Value;
					}
					if (xmlSchemaFacet is XmlSchemaMaxInclusiveFacet)
					{
						this.maxInclusive = xmlSchemaFacet.Value;
					}
				}
			}
			string msdataAttribute = XSDSchema.GetMsdataAttribute(node, "targetNamespace");
			if (msdataAttribute != null)
			{
				this.ns = msdataAttribute;
			}
		}

		// Token: 0x060011D3 RID: 4563 RVA: 0x0008914C File Offset: 0x0008854C
		internal bool IsPlainString()
		{
			return XSDSchema.QualifiedName(this.baseType) == XSDSchema.QualifiedName("string") && ADP.IsEmpty(this.name) && this.length == -1 && this.minLength == -1 && this.maxLength == -1 && ADP.IsEmpty(this.pattern) && ADP.IsEmpty(this.maxExclusive) && ADP.IsEmpty(this.maxInclusive) && ADP.IsEmpty(this.minExclusive) && ADP.IsEmpty(this.minInclusive) && ADP.IsEmpty(this.enumeration);
		}

		// Token: 0x170002A5 RID: 677
		// (get) Token: 0x060011D4 RID: 4564 RVA: 0x000891F0 File Offset: 0x000885F0
		internal string BaseType
		{
			get
			{
				return this.baseType;
			}
		}

		// Token: 0x170002A6 RID: 678
		// (get) Token: 0x060011D5 RID: 4565 RVA: 0x00089204 File Offset: 0x00088604
		internal XmlQualifiedName XmlBaseType
		{
			get
			{
				return this.xmlBaseType;
			}
		}

		// Token: 0x170002A7 RID: 679
		// (get) Token: 0x060011D6 RID: 4566 RVA: 0x00089218 File Offset: 0x00088618
		internal string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x170002A8 RID: 680
		// (get) Token: 0x060011D7 RID: 4567 RVA: 0x0008922C File Offset: 0x0008862C
		internal string Namespace
		{
			get
			{
				return this.ns;
			}
		}

		// Token: 0x170002A9 RID: 681
		// (get) Token: 0x060011D8 RID: 4568 RVA: 0x00089240 File Offset: 0x00088640
		internal int Length
		{
			get
			{
				return this.length;
			}
		}

		// Token: 0x170002AA RID: 682
		// (get) Token: 0x060011D9 RID: 4569 RVA: 0x00089254 File Offset: 0x00088654
		// (set) Token: 0x060011DA RID: 4570 RVA: 0x00089268 File Offset: 0x00088668
		internal int MaxLength
		{
			get
			{
				return this.maxLength;
			}
			set
			{
				this.maxLength = value;
			}
		}

		// Token: 0x170002AB RID: 683
		// (get) Token: 0x060011DB RID: 4571 RVA: 0x0008927C File Offset: 0x0008867C
		internal SimpleType BaseSimpleType
		{
			get
			{
				return this.baseSimpleType;
			}
		}

		// Token: 0x170002AC RID: 684
		// (get) Token: 0x060011DC RID: 4572 RVA: 0x00089290 File Offset: 0x00088690
		public string SimpleTypeQualifiedName
		{
			get
			{
				if (this.ns.Length == 0)
				{
					return this.name;
				}
				return this.ns + ":" + this.name;
			}
		}

		// Token: 0x060011DD RID: 4573 RVA: 0x000892C8 File Offset: 0x000886C8
		internal string QualifiedName(string name)
		{
			int num = name.IndexOf(':');
			if (num == -1)
			{
				return "xs:" + name;
			}
			return name;
		}

		// Token: 0x060011DE RID: 4574 RVA: 0x000892F0 File Offset: 0x000886F0
		internal XmlNode ToNode(XmlDocument dc, Hashtable prefixes, bool inRemoting)
		{
			XmlElement xmlElement = dc.CreateElement("xs", "simpleType", "http://www.w3.org/2001/XMLSchema");
			if (this.name != null && this.name.Length != 0)
			{
				xmlElement.SetAttribute("name", this.name);
				if (inRemoting)
				{
					xmlElement.SetAttribute("targetNamespace", "urn:schemas-microsoft-com:xml-msdata", this.Namespace);
				}
			}
			XmlElement xmlElement2 = dc.CreateElement("xs", "restriction", "http://www.w3.org/2001/XMLSchema");
			if (!inRemoting)
			{
				if (this.baseSimpleType != null)
				{
					if (this.baseSimpleType.Namespace != null && this.baseSimpleType.Namespace.Length > 0)
					{
						string text = (prefixes != null) ? ((string)prefixes[this.baseSimpleType.Namespace]) : null;
						if (text != null)
						{
							xmlElement2.SetAttribute("base", text + ":" + this.baseSimpleType.Name);
						}
						else
						{
							xmlElement2.SetAttribute("base", this.baseSimpleType.Name);
						}
					}
					else
					{
						xmlElement2.SetAttribute("base", this.baseSimpleType.Name);
					}
				}
				else
				{
					xmlElement2.SetAttribute("base", this.QualifiedName(this.baseType));
				}
			}
			else
			{
				xmlElement2.SetAttribute("base", (this.baseSimpleType != null) ? this.baseSimpleType.Name : this.QualifiedName(this.baseType));
			}
			if (this.length >= 0)
			{
				XmlElement xmlElement3 = dc.CreateElement("xs", "length", "http://www.w3.org/2001/XMLSchema");
				xmlElement3.SetAttribute("value", this.length.ToString(CultureInfo.InvariantCulture));
				xmlElement2.AppendChild(xmlElement3);
			}
			if (this.maxLength >= 0)
			{
				XmlElement xmlElement3 = dc.CreateElement("xs", "maxLength", "http://www.w3.org/2001/XMLSchema");
				xmlElement3.SetAttribute("value", this.maxLength.ToString(CultureInfo.InvariantCulture));
				xmlElement2.AppendChild(xmlElement3);
			}
			xmlElement.AppendChild(xmlElement2);
			return xmlElement;
		}

		// Token: 0x060011DF RID: 4575 RVA: 0x000894E4 File Offset: 0x000888E4
		internal static SimpleType CreateEnumeratedType(string values)
		{
			return new SimpleType("string")
			{
				enumeration = values
			};
		}

		// Token: 0x060011E0 RID: 4576 RVA: 0x00089504 File Offset: 0x00088904
		internal static SimpleType CreateByteArrayType(string encoding)
		{
			return new SimpleType("base64Binary");
		}

		// Token: 0x060011E1 RID: 4577 RVA: 0x00089520 File Offset: 0x00088920
		internal static SimpleType CreateLimitedStringType(int length)
		{
			return new SimpleType("string")
			{
				maxLength = length
			};
		}

		// Token: 0x060011E2 RID: 4578 RVA: 0x00089540 File Offset: 0x00088940
		internal static SimpleType CreateSimpleType(StorageType typeCode, Type type)
		{
			if (typeCode == StorageType.Char && type == typeof(char))
			{
				return new SimpleType("string")
				{
					length = 1
				};
			}
			return null;
		}

		// Token: 0x060011E3 RID: 4579 RVA: 0x00089578 File Offset: 0x00088978
		internal string HasConflictingDefinition(SimpleType otherSimpleType)
		{
			if (otherSimpleType == null)
			{
				return "otherSimpleType";
			}
			if (this.MaxLength != otherSimpleType.MaxLength)
			{
				return "MaxLength";
			}
			if (string.Compare(this.BaseType, otherSimpleType.BaseType, StringComparison.Ordinal) != 0)
			{
				return "BaseType";
			}
			if (this.BaseSimpleType == null && otherSimpleType.BaseSimpleType != null && this.BaseSimpleType.HasConflictingDefinition(otherSimpleType.BaseSimpleType).Length != 0)
			{
				return "BaseSimpleType";
			}
			return string.Empty;
		}

		// Token: 0x060011E4 RID: 4580 RVA: 0x000895F0 File Offset: 0x000889F0
		internal bool CanHaveMaxLength()
		{
			SimpleType simpleType = this;
			while (simpleType.BaseSimpleType != null)
			{
				simpleType = simpleType.BaseSimpleType;
			}
			return string.Compare(simpleType.BaseType, "string", StringComparison.OrdinalIgnoreCase) == 0;
		}

		// Token: 0x060011E5 RID: 4581 RVA: 0x00089628 File Offset: 0x00088A28
		internal void ConvertToAnnonymousSimpleType()
		{
			this.name = null;
			this.ns = string.Empty;
			SimpleType simpleType = this;
			while (simpleType.baseSimpleType != null)
			{
				simpleType = simpleType.baseSimpleType;
			}
			this.baseType = simpleType.baseType;
			this.baseSimpleType = simpleType.baseSimpleType;
			this.xmlBaseType = simpleType.xmlBaseType;
		}

		// Token: 0x04000601 RID: 1537
		private string baseType;

		// Token: 0x04000602 RID: 1538
		private SimpleType baseSimpleType;

		// Token: 0x04000603 RID: 1539
		private XmlQualifiedName xmlBaseType;

		// Token: 0x04000604 RID: 1540
		private string name = "";

		// Token: 0x04000605 RID: 1541
		private int length = -1;

		// Token: 0x04000606 RID: 1542
		private int minLength = -1;

		// Token: 0x04000607 RID: 1543
		private int maxLength = -1;

		// Token: 0x04000608 RID: 1544
		private string pattern = "";

		// Token: 0x04000609 RID: 1545
		private string ns = "";

		// Token: 0x0400060A RID: 1546
		private string maxExclusive = "";

		// Token: 0x0400060B RID: 1547
		private string maxInclusive = "";

		// Token: 0x0400060C RID: 1548
		private string minExclusive = "";

		// Token: 0x0400060D RID: 1549
		private string minInclusive = "";

		// Token: 0x0400060E RID: 1550
		internal string enumeration = "";
	}
}
