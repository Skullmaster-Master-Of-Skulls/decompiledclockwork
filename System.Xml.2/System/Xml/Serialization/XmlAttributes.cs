using System;
using System.ComponentModel;
using System.Reflection;

namespace System.Xml.Serialization
{
	// Token: 0x0200018D RID: 397
	[__DynamicallyInvokable]
	public class XmlAttributes
	{
		// Token: 0x060019F0 RID: 6640 RVA: 0x000735A3 File Offset: 0x000717A3
		[__DynamicallyInvokable]
		public XmlAttributes()
		{
		}

		// Token: 0x170005A2 RID: 1442
		// (get) Token: 0x060019F1 RID: 6641 RVA: 0x000735CC File Offset: 0x000717CC
		internal XmlAttributeFlags XmlFlags
		{
			get
			{
				XmlAttributeFlags xmlAttributeFlags = (XmlAttributeFlags)0;
				if (this.xmlElements.Count > 0)
				{
					xmlAttributeFlags |= XmlAttributeFlags.Elements;
				}
				if (this.xmlArrayItems.Count > 0)
				{
					xmlAttributeFlags |= XmlAttributeFlags.ArrayItems;
				}
				if (this.xmlAnyElements.Count > 0)
				{
					xmlAttributeFlags |= XmlAttributeFlags.AnyElements;
				}
				if (this.xmlArray != null)
				{
					xmlAttributeFlags |= XmlAttributeFlags.Array;
				}
				if (this.xmlAttribute != null)
				{
					xmlAttributeFlags |= XmlAttributeFlags.Attribute;
				}
				if (this.xmlText != null)
				{
					xmlAttributeFlags |= XmlAttributeFlags.Text;
				}
				if (this.xmlEnum != null)
				{
					xmlAttributeFlags |= XmlAttributeFlags.Enum;
				}
				if (this.xmlRoot != null)
				{
					xmlAttributeFlags |= XmlAttributeFlags.Root;
				}
				if (this.xmlType != null)
				{
					xmlAttributeFlags |= XmlAttributeFlags.Type;
				}
				if (this.xmlAnyAttribute != null)
				{
					xmlAttributeFlags |= XmlAttributeFlags.AnyAttribute;
				}
				if (this.xmlChoiceIdentifier != null)
				{
					xmlAttributeFlags |= XmlAttributeFlags.ChoiceIdentifier;
				}
				if (this.xmlns)
				{
					xmlAttributeFlags |= XmlAttributeFlags.XmlnsDeclarations;
				}
				return xmlAttributeFlags;
			}
		}

		// Token: 0x170005A3 RID: 1443
		// (get) Token: 0x060019F2 RID: 6642 RVA: 0x00073698 File Offset: 0x00071898
		private static Type IgnoreAttribute
		{
			get
			{
				if (XmlAttributes.ignoreAttributeType == null)
				{
					XmlAttributes.ignoreAttributeType = typeof(object).Assembly.GetType("System.XmlIgnoreMemberAttribute");
					if (XmlAttributes.ignoreAttributeType == null)
					{
						XmlAttributes.ignoreAttributeType = typeof(XmlIgnoreAttribute);
					}
				}
				return XmlAttributes.ignoreAttributeType;
			}
		}

		// Token: 0x060019F3 RID: 6643 RVA: 0x000736FC File Offset: 0x000718FC
		public XmlAttributes(ICustomAttributeProvider provider)
		{
			object[] customAttributes = provider.GetCustomAttributes(false);
			XmlAnyElementAttribute xmlAnyElementAttribute = null;
			for (int i = 0; i < customAttributes.Length; i++)
			{
				if (customAttributes[i] is XmlIgnoreAttribute || customAttributes[i] is ObsoleteAttribute || customAttributes[i].GetType() == XmlAttributes.IgnoreAttribute)
				{
					this.xmlIgnore = true;
					break;
				}
				if (customAttributes[i] is XmlElementAttribute)
				{
					this.xmlElements.Add((XmlElementAttribute)customAttributes[i]);
				}
				else if (customAttributes[i] is XmlArrayItemAttribute)
				{
					this.xmlArrayItems.Add((XmlArrayItemAttribute)customAttributes[i]);
				}
				else if (customAttributes[i] is XmlAnyElementAttribute)
				{
					XmlAnyElementAttribute xmlAnyElementAttribute2 = (XmlAnyElementAttribute)customAttributes[i];
					if ((xmlAnyElementAttribute2.Name == null || xmlAnyElementAttribute2.Name.Length == 0) && xmlAnyElementAttribute2.NamespaceSpecified && xmlAnyElementAttribute2.Namespace == null)
					{
						xmlAnyElementAttribute = xmlAnyElementAttribute2;
					}
					else
					{
						this.xmlAnyElements.Add((XmlAnyElementAttribute)customAttributes[i]);
					}
				}
				else if (customAttributes[i] is DefaultValueAttribute)
				{
					this.xmlDefaultValue = ((DefaultValueAttribute)customAttributes[i]).Value;
				}
				else if (customAttributes[i] is XmlAttributeAttribute)
				{
					this.xmlAttribute = (XmlAttributeAttribute)customAttributes[i];
				}
				else if (customAttributes[i] is XmlArrayAttribute)
				{
					this.xmlArray = (XmlArrayAttribute)customAttributes[i];
				}
				else if (customAttributes[i] is XmlTextAttribute)
				{
					this.xmlText = (XmlTextAttribute)customAttributes[i];
				}
				else if (customAttributes[i] is XmlEnumAttribute)
				{
					this.xmlEnum = (XmlEnumAttribute)customAttributes[i];
				}
				else if (customAttributes[i] is XmlRootAttribute)
				{
					this.xmlRoot = (XmlRootAttribute)customAttributes[i];
				}
				else if (customAttributes[i] is XmlTypeAttribute)
				{
					this.xmlType = (XmlTypeAttribute)customAttributes[i];
				}
				else if (customAttributes[i] is XmlAnyAttributeAttribute)
				{
					this.xmlAnyAttribute = (XmlAnyAttributeAttribute)customAttributes[i];
				}
				else if (customAttributes[i] is XmlChoiceIdentifierAttribute)
				{
					this.xmlChoiceIdentifier = (XmlChoiceIdentifierAttribute)customAttributes[i];
				}
				else if (customAttributes[i] is XmlNamespaceDeclarationsAttribute)
				{
					this.xmlns = true;
				}
			}
			if (this.xmlIgnore)
			{
				this.xmlElements.Clear();
				this.xmlArrayItems.Clear();
				this.xmlAnyElements.Clear();
				this.xmlDefaultValue = null;
				this.xmlAttribute = null;
				this.xmlArray = null;
				this.xmlText = null;
				this.xmlEnum = null;
				this.xmlType = null;
				this.xmlAnyAttribute = null;
				this.xmlChoiceIdentifier = null;
				this.xmlns = false;
				return;
			}
			if (xmlAnyElementAttribute != null)
			{
				this.xmlAnyElements.Add(xmlAnyElementAttribute);
			}
		}

		// Token: 0x060019F4 RID: 6644 RVA: 0x000739A8 File Offset: 0x00071BA8
		internal static object GetAttr(ICustomAttributeProvider provider, Type attrType)
		{
			object[] customAttributes = provider.GetCustomAttributes(attrType, false);
			if (customAttributes.Length == 0)
			{
				return null;
			}
			return customAttributes[0];
		}

		// Token: 0x170005A4 RID: 1444
		// (get) Token: 0x060019F5 RID: 6645 RVA: 0x000739C7 File Offset: 0x00071BC7
		[__DynamicallyInvokable]
		public XmlElementAttributes XmlElements
		{
			[__DynamicallyInvokable]
			get
			{
				return this.xmlElements;
			}
		}

		// Token: 0x170005A5 RID: 1445
		// (get) Token: 0x060019F6 RID: 6646 RVA: 0x000739CF File Offset: 0x00071BCF
		// (set) Token: 0x060019F7 RID: 6647 RVA: 0x000739D7 File Offset: 0x00071BD7
		[__DynamicallyInvokable]
		public XmlAttributeAttribute XmlAttribute
		{
			[__DynamicallyInvokable]
			get
			{
				return this.xmlAttribute;
			}
			[__DynamicallyInvokable]
			set
			{
				this.xmlAttribute = value;
			}
		}

		// Token: 0x170005A6 RID: 1446
		// (get) Token: 0x060019F8 RID: 6648 RVA: 0x000739E0 File Offset: 0x00071BE0
		// (set) Token: 0x060019F9 RID: 6649 RVA: 0x000739E8 File Offset: 0x00071BE8
		[__DynamicallyInvokable]
		public XmlEnumAttribute XmlEnum
		{
			[__DynamicallyInvokable]
			get
			{
				return this.xmlEnum;
			}
			[__DynamicallyInvokable]
			set
			{
				this.xmlEnum = value;
			}
		}

		// Token: 0x170005A7 RID: 1447
		// (get) Token: 0x060019FA RID: 6650 RVA: 0x000739F1 File Offset: 0x00071BF1
		// (set) Token: 0x060019FB RID: 6651 RVA: 0x000739F9 File Offset: 0x00071BF9
		[__DynamicallyInvokable]
		public XmlTextAttribute XmlText
		{
			[__DynamicallyInvokable]
			get
			{
				return this.xmlText;
			}
			[__DynamicallyInvokable]
			set
			{
				this.xmlText = value;
			}
		}

		// Token: 0x170005A8 RID: 1448
		// (get) Token: 0x060019FC RID: 6652 RVA: 0x00073A02 File Offset: 0x00071C02
		// (set) Token: 0x060019FD RID: 6653 RVA: 0x00073A0A File Offset: 0x00071C0A
		[__DynamicallyInvokable]
		public XmlArrayAttribute XmlArray
		{
			[__DynamicallyInvokable]
			get
			{
				return this.xmlArray;
			}
			[__DynamicallyInvokable]
			set
			{
				this.xmlArray = value;
			}
		}

		// Token: 0x170005A9 RID: 1449
		// (get) Token: 0x060019FE RID: 6654 RVA: 0x00073A13 File Offset: 0x00071C13
		[__DynamicallyInvokable]
		public XmlArrayItemAttributes XmlArrayItems
		{
			[__DynamicallyInvokable]
			get
			{
				return this.xmlArrayItems;
			}
		}

		// Token: 0x170005AA RID: 1450
		// (get) Token: 0x060019FF RID: 6655 RVA: 0x00073A1B File Offset: 0x00071C1B
		// (set) Token: 0x06001A00 RID: 6656 RVA: 0x00073A23 File Offset: 0x00071C23
		[__DynamicallyInvokable]
		public object XmlDefaultValue
		{
			[__DynamicallyInvokable]
			get
			{
				return this.xmlDefaultValue;
			}
			[__DynamicallyInvokable]
			set
			{
				this.xmlDefaultValue = value;
			}
		}

		// Token: 0x170005AB RID: 1451
		// (get) Token: 0x06001A01 RID: 6657 RVA: 0x00073A2C File Offset: 0x00071C2C
		// (set) Token: 0x06001A02 RID: 6658 RVA: 0x00073A34 File Offset: 0x00071C34
		[__DynamicallyInvokable]
		public bool XmlIgnore
		{
			[__DynamicallyInvokable]
			get
			{
				return this.xmlIgnore;
			}
			[__DynamicallyInvokable]
			set
			{
				this.xmlIgnore = value;
			}
		}

		// Token: 0x170005AC RID: 1452
		// (get) Token: 0x06001A03 RID: 6659 RVA: 0x00073A3D File Offset: 0x00071C3D
		// (set) Token: 0x06001A04 RID: 6660 RVA: 0x00073A45 File Offset: 0x00071C45
		[__DynamicallyInvokable]
		public XmlTypeAttribute XmlType
		{
			[__DynamicallyInvokable]
			get
			{
				return this.xmlType;
			}
			[__DynamicallyInvokable]
			set
			{
				this.xmlType = value;
			}
		}

		// Token: 0x170005AD RID: 1453
		// (get) Token: 0x06001A05 RID: 6661 RVA: 0x00073A4E File Offset: 0x00071C4E
		// (set) Token: 0x06001A06 RID: 6662 RVA: 0x00073A56 File Offset: 0x00071C56
		[__DynamicallyInvokable]
		public XmlRootAttribute XmlRoot
		{
			[__DynamicallyInvokable]
			get
			{
				return this.xmlRoot;
			}
			[__DynamicallyInvokable]
			set
			{
				this.xmlRoot = value;
			}
		}

		// Token: 0x170005AE RID: 1454
		// (get) Token: 0x06001A07 RID: 6663 RVA: 0x00073A5F File Offset: 0x00071C5F
		[__DynamicallyInvokable]
		public XmlAnyElementAttributes XmlAnyElements
		{
			[__DynamicallyInvokable]
			get
			{
				return this.xmlAnyElements;
			}
		}

		// Token: 0x170005AF RID: 1455
		// (get) Token: 0x06001A08 RID: 6664 RVA: 0x00073A67 File Offset: 0x00071C67
		// (set) Token: 0x06001A09 RID: 6665 RVA: 0x00073A6F File Offset: 0x00071C6F
		[__DynamicallyInvokable]
		public XmlAnyAttributeAttribute XmlAnyAttribute
		{
			[__DynamicallyInvokable]
			get
			{
				return this.xmlAnyAttribute;
			}
			[__DynamicallyInvokable]
			set
			{
				this.xmlAnyAttribute = value;
			}
		}

		// Token: 0x170005B0 RID: 1456
		// (get) Token: 0x06001A0A RID: 6666 RVA: 0x00073A78 File Offset: 0x00071C78
		[__DynamicallyInvokable]
		public XmlChoiceIdentifierAttribute XmlChoiceIdentifier
		{
			[__DynamicallyInvokable]
			get
			{
				return this.xmlChoiceIdentifier;
			}
		}

		// Token: 0x170005B1 RID: 1457
		// (get) Token: 0x06001A0B RID: 6667 RVA: 0x00073A80 File Offset: 0x00071C80
		// (set) Token: 0x06001A0C RID: 6668 RVA: 0x00073A88 File Offset: 0x00071C88
		[__DynamicallyInvokable]
		public bool Xmlns
		{
			[__DynamicallyInvokable]
			get
			{
				return this.xmlns;
			}
			[__DynamicallyInvokable]
			set
			{
				this.xmlns = value;
			}
		}

		// Token: 0x04000BD5 RID: 3029
		private XmlElementAttributes xmlElements = new XmlElementAttributes();

		// Token: 0x04000BD6 RID: 3030
		private XmlArrayItemAttributes xmlArrayItems = new XmlArrayItemAttributes();

		// Token: 0x04000BD7 RID: 3031
		private XmlAnyElementAttributes xmlAnyElements = new XmlAnyElementAttributes();

		// Token: 0x04000BD8 RID: 3032
		private XmlArrayAttribute xmlArray;

		// Token: 0x04000BD9 RID: 3033
		private XmlAttributeAttribute xmlAttribute;

		// Token: 0x04000BDA RID: 3034
		private XmlTextAttribute xmlText;

		// Token: 0x04000BDB RID: 3035
		private XmlEnumAttribute xmlEnum;

		// Token: 0x04000BDC RID: 3036
		private bool xmlIgnore;

		// Token: 0x04000BDD RID: 3037
		private bool xmlns;

		// Token: 0x04000BDE RID: 3038
		private object xmlDefaultValue;

		// Token: 0x04000BDF RID: 3039
		private XmlRootAttribute xmlRoot;

		// Token: 0x04000BE0 RID: 3040
		private XmlTypeAttribute xmlType;

		// Token: 0x04000BE1 RID: 3041
		private XmlAnyAttributeAttribute xmlAnyAttribute;

		// Token: 0x04000BE2 RID: 3042
		private XmlChoiceIdentifierAttribute xmlChoiceIdentifier;

		// Token: 0x04000BE3 RID: 3043
		private static volatile Type ignoreAttributeType;
	}
}
