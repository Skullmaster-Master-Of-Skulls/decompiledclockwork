using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using MS.Internal.Xml.Linq.ComponentModel;

namespace System.Xml.Linq
{
	// Token: 0x0200002A RID: 42
	[TypeDescriptionProvider(typeof(XTypeDescriptionProvider<XAttribute>))]
	[__DynamicallyInvokable]
	public class XAttribute : XObject
	{
		// Token: 0x17000042 RID: 66
		// (get) Token: 0x060001E6 RID: 486 RVA: 0x00008CCD File Offset: 0x00006ECD
		[__DynamicallyInvokable]
		public static IEnumerable<XAttribute> EmptySequence
		{
			[__DynamicallyInvokable]
			get
			{
				if (XAttribute.emptySequence == null)
				{
					XAttribute.emptySequence = new XAttribute[0];
				}
				return XAttribute.emptySequence;
			}
		}

		// Token: 0x060001E7 RID: 487 RVA: 0x00008CE8 File Offset: 0x00006EE8
		[__DynamicallyInvokable]
		public XAttribute(XName name, object value)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			string stringValue = XContainer.GetStringValue(value);
			XAttribute.ValidateAttribute(name, stringValue);
			this.name = name;
			this.value = stringValue;
		}

		// Token: 0x060001E8 RID: 488 RVA: 0x00008D39 File Offset: 0x00006F39
		[__DynamicallyInvokable]
		public XAttribute(XAttribute other)
		{
			if (other == null)
			{
				throw new ArgumentNullException("other");
			}
			this.name = other.name;
			this.value = other.value;
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x060001E9 RID: 489 RVA: 0x00008D68 File Offset: 0x00006F68
		[__DynamicallyInvokable]
		public bool IsNamespaceDeclaration
		{
			[__DynamicallyInvokable]
			get
			{
				string namespaceName = this.name.NamespaceName;
				if (namespaceName.Length == 0)
				{
					return this.name.LocalName == "xmlns";
				}
				return namespaceName == "http://www.w3.org/2000/xmlns/";
			}
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x060001EA RID: 490 RVA: 0x00008DA7 File Offset: 0x00006FA7
		[__DynamicallyInvokable]
		public XName Name
		{
			[__DynamicallyInvokable]
			get
			{
				return this.name;
			}
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x060001EB RID: 491 RVA: 0x00008DAF File Offset: 0x00006FAF
		[__DynamicallyInvokable]
		public XAttribute NextAttribute
		{
			[__DynamicallyInvokable]
			get
			{
				if (this.parent == null || ((XElement)this.parent).lastAttr == this)
				{
					return null;
				}
				return this.next;
			}
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x060001EC RID: 492 RVA: 0x00008DD4 File Offset: 0x00006FD4
		[__DynamicallyInvokable]
		public override XmlNodeType NodeType
		{
			[__DynamicallyInvokable]
			get
			{
				return XmlNodeType.Attribute;
			}
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x060001ED RID: 493 RVA: 0x00008DD8 File Offset: 0x00006FD8
		[__DynamicallyInvokable]
		public XAttribute PreviousAttribute
		{
			[__DynamicallyInvokable]
			get
			{
				if (this.parent == null)
				{
					return null;
				}
				XAttribute lastAttr = ((XElement)this.parent).lastAttr;
				while (lastAttr.next != this)
				{
					lastAttr = lastAttr.next;
				}
				if (lastAttr == ((XElement)this.parent).lastAttr)
				{
					return null;
				}
				return lastAttr;
			}
		}

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x060001EE RID: 494 RVA: 0x00008E28 File Offset: 0x00007028
		// (set) Token: 0x060001EF RID: 495 RVA: 0x00008E30 File Offset: 0x00007030
		[__DynamicallyInvokable]
		public string Value
		{
			[__DynamicallyInvokable]
			get
			{
				return this.value;
			}
			[__DynamicallyInvokable]
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				XAttribute.ValidateAttribute(this.name, value);
				bool flag = base.NotifyChanging(this, XObjectChangeEventArgs.Value);
				this.value = value;
				if (flag)
				{
					base.NotifyChanged(this, XObjectChangeEventArgs.Value);
				}
			}
		}

		// Token: 0x060001F0 RID: 496 RVA: 0x00008E7B File Offset: 0x0000707B
		[__DynamicallyInvokable]
		public void Remove()
		{
			if (this.parent == null)
			{
				throw new InvalidOperationException(Res.GetString("InvalidOperation_MissingParent"));
			}
			((XElement)this.parent).RemoveAttribute(this);
		}

		// Token: 0x060001F1 RID: 497 RVA: 0x00008EA6 File Offset: 0x000070A6
		[__DynamicallyInvokable]
		public void SetValue(object value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			this.Value = XContainer.GetStringValue(value);
		}

		// Token: 0x060001F2 RID: 498 RVA: 0x00008EC4 File Offset: 0x000070C4
		[__DynamicallyInvokable]
		public override string ToString()
		{
			string result;
			using (StringWriter stringWriter = new StringWriter(CultureInfo.InvariantCulture))
			{
				using (XmlWriter xmlWriter = XmlWriter.Create(stringWriter, new XmlWriterSettings
				{
					ConformanceLevel = ConformanceLevel.Fragment
				}))
				{
					xmlWriter.WriteAttributeString(this.GetPrefixOfNamespace(this.name.Namespace), this.name.LocalName, this.name.NamespaceName, this.value);
				}
				result = stringWriter.ToString().Trim();
			}
			return result;
		}

		// Token: 0x060001F3 RID: 499 RVA: 0x00008F68 File Offset: 0x00007168
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static explicit operator string(XAttribute attribute)
		{
			if (attribute == null)
			{
				return null;
			}
			return attribute.value;
		}

		// Token: 0x060001F4 RID: 500 RVA: 0x00008F75 File Offset: 0x00007175
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static explicit operator bool(XAttribute attribute)
		{
			if (attribute == null)
			{
				throw new ArgumentNullException("attribute");
			}
			return XmlConvert.ToBoolean(attribute.value.ToLower(CultureInfo.InvariantCulture));
		}

		// Token: 0x060001F5 RID: 501 RVA: 0x00008F9C File Offset: 0x0000719C
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static explicit operator bool?(XAttribute attribute)
		{
			if (attribute == null)
			{
				return null;
			}
			return new bool?(XmlConvert.ToBoolean(attribute.value.ToLower(CultureInfo.InvariantCulture)));
		}

		// Token: 0x060001F6 RID: 502 RVA: 0x00008FD0 File Offset: 0x000071D0
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static explicit operator int(XAttribute attribute)
		{
			if (attribute == null)
			{
				throw new ArgumentNullException("attribute");
			}
			return XmlConvert.ToInt32(attribute.value);
		}

		// Token: 0x060001F7 RID: 503 RVA: 0x00008FEC File Offset: 0x000071EC
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static explicit operator int?(XAttribute attribute)
		{
			if (attribute == null)
			{
				return null;
			}
			return new int?(XmlConvert.ToInt32(attribute.value));
		}

		// Token: 0x060001F8 RID: 504 RVA: 0x00009016 File Offset: 0x00007216
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static explicit operator uint(XAttribute attribute)
		{
			if (attribute == null)
			{
				throw new ArgumentNullException("attribute");
			}
			return XmlConvert.ToUInt32(attribute.value);
		}

		// Token: 0x060001F9 RID: 505 RVA: 0x00009034 File Offset: 0x00007234
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static explicit operator uint?(XAttribute attribute)
		{
			if (attribute == null)
			{
				return null;
			}
			return new uint?(XmlConvert.ToUInt32(attribute.value));
		}

		// Token: 0x060001FA RID: 506 RVA: 0x0000905E File Offset: 0x0000725E
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static explicit operator long(XAttribute attribute)
		{
			if (attribute == null)
			{
				throw new ArgumentNullException("attribute");
			}
			return XmlConvert.ToInt64(attribute.value);
		}

		// Token: 0x060001FB RID: 507 RVA: 0x0000907C File Offset: 0x0000727C
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static explicit operator long?(XAttribute attribute)
		{
			if (attribute == null)
			{
				return null;
			}
			return new long?(XmlConvert.ToInt64(attribute.value));
		}

		// Token: 0x060001FC RID: 508 RVA: 0x000090A6 File Offset: 0x000072A6
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static explicit operator ulong(XAttribute attribute)
		{
			if (attribute == null)
			{
				throw new ArgumentNullException("attribute");
			}
			return XmlConvert.ToUInt64(attribute.value);
		}

		// Token: 0x060001FD RID: 509 RVA: 0x000090C4 File Offset: 0x000072C4
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static explicit operator ulong?(XAttribute attribute)
		{
			if (attribute == null)
			{
				return null;
			}
			return new ulong?(XmlConvert.ToUInt64(attribute.value));
		}

		// Token: 0x060001FE RID: 510 RVA: 0x000090EE File Offset: 0x000072EE
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static explicit operator float(XAttribute attribute)
		{
			if (attribute == null)
			{
				throw new ArgumentNullException("attribute");
			}
			return XmlConvert.ToSingle(attribute.value);
		}

		// Token: 0x060001FF RID: 511 RVA: 0x0000910C File Offset: 0x0000730C
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static explicit operator float?(XAttribute attribute)
		{
			if (attribute == null)
			{
				return null;
			}
			return new float?(XmlConvert.ToSingle(attribute.value));
		}

		// Token: 0x06000200 RID: 512 RVA: 0x00009136 File Offset: 0x00007336
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static explicit operator double(XAttribute attribute)
		{
			if (attribute == null)
			{
				throw new ArgumentNullException("attribute");
			}
			return XmlConvert.ToDouble(attribute.value);
		}

		// Token: 0x06000201 RID: 513 RVA: 0x00009154 File Offset: 0x00007354
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static explicit operator double?(XAttribute attribute)
		{
			if (attribute == null)
			{
				return null;
			}
			return new double?(XmlConvert.ToDouble(attribute.value));
		}

		// Token: 0x06000202 RID: 514 RVA: 0x0000917E File Offset: 0x0000737E
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static explicit operator decimal(XAttribute attribute)
		{
			if (attribute == null)
			{
				throw new ArgumentNullException("attribute");
			}
			return XmlConvert.ToDecimal(attribute.value);
		}

		// Token: 0x06000203 RID: 515 RVA: 0x0000919C File Offset: 0x0000739C
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static explicit operator decimal?(XAttribute attribute)
		{
			if (attribute == null)
			{
				return null;
			}
			return new decimal?(XmlConvert.ToDecimal(attribute.value));
		}

		// Token: 0x06000204 RID: 516 RVA: 0x000091C6 File Offset: 0x000073C6
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static explicit operator DateTime(XAttribute attribute)
		{
			if (attribute == null)
			{
				throw new ArgumentNullException("attribute");
			}
			return DateTime.Parse(attribute.value, CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind);
		}

		// Token: 0x06000205 RID: 517 RVA: 0x000091EC File Offset: 0x000073EC
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static explicit operator DateTime?(XAttribute attribute)
		{
			if (attribute == null)
			{
				return null;
			}
			return new DateTime?(DateTime.Parse(attribute.value, CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind));
		}

		// Token: 0x06000206 RID: 518 RVA: 0x00009220 File Offset: 0x00007420
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static explicit operator DateTimeOffset(XAttribute attribute)
		{
			if (attribute == null)
			{
				throw new ArgumentNullException("attribute");
			}
			return XmlConvert.ToDateTimeOffset(attribute.value);
		}

		// Token: 0x06000207 RID: 519 RVA: 0x0000923C File Offset: 0x0000743C
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static explicit operator DateTimeOffset?(XAttribute attribute)
		{
			if (attribute == null)
			{
				return null;
			}
			return new DateTimeOffset?(XmlConvert.ToDateTimeOffset(attribute.value));
		}

		// Token: 0x06000208 RID: 520 RVA: 0x00009266 File Offset: 0x00007466
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static explicit operator TimeSpan(XAttribute attribute)
		{
			if (attribute == null)
			{
				throw new ArgumentNullException("attribute");
			}
			return XmlConvert.ToTimeSpan(attribute.value);
		}

		// Token: 0x06000209 RID: 521 RVA: 0x00009284 File Offset: 0x00007484
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static explicit operator TimeSpan?(XAttribute attribute)
		{
			if (attribute == null)
			{
				return null;
			}
			return new TimeSpan?(XmlConvert.ToTimeSpan(attribute.value));
		}

		// Token: 0x0600020A RID: 522 RVA: 0x000092AE File Offset: 0x000074AE
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static explicit operator Guid(XAttribute attribute)
		{
			if (attribute == null)
			{
				throw new ArgumentNullException("attribute");
			}
			return XmlConvert.ToGuid(attribute.value);
		}

		// Token: 0x0600020B RID: 523 RVA: 0x000092CC File Offset: 0x000074CC
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static explicit operator Guid?(XAttribute attribute)
		{
			if (attribute == null)
			{
				return null;
			}
			return new Guid?(XmlConvert.ToGuid(attribute.value));
		}

		// Token: 0x0600020C RID: 524 RVA: 0x000092F6 File Offset: 0x000074F6
		internal int GetDeepHashCode()
		{
			return this.name.GetHashCode() ^ this.value.GetHashCode();
		}

		// Token: 0x0600020D RID: 525 RVA: 0x00009310 File Offset: 0x00007510
		internal string GetPrefixOfNamespace(XNamespace ns)
		{
			string namespaceName = ns.NamespaceName;
			if (namespaceName.Length == 0)
			{
				return string.Empty;
			}
			if (this.parent != null)
			{
				return ((XElement)this.parent).GetPrefixOfNamespace(ns);
			}
			if (namespaceName == "http://www.w3.org/XML/1998/namespace")
			{
				return "xml";
			}
			if (namespaceName == "http://www.w3.org/2000/xmlns/")
			{
				return "xmlns";
			}
			return null;
		}

		// Token: 0x0600020E RID: 526 RVA: 0x0000936C File Offset: 0x0000756C
		private static void ValidateAttribute(XName name, string value)
		{
			string namespaceName = name.NamespaceName;
			if (namespaceName == "http://www.w3.org/2000/xmlns/")
			{
				if (value.Length == 0)
				{
					throw new ArgumentException(Res.GetString("Argument_NamespaceDeclarationPrefixed", new object[]
					{
						name.LocalName
					}));
				}
				if (value == "http://www.w3.org/XML/1998/namespace")
				{
					if (name.LocalName != "xml")
					{
						throw new ArgumentException(Res.GetString("Argument_NamespaceDeclarationXml"));
					}
				}
				else
				{
					if (value == "http://www.w3.org/2000/xmlns/")
					{
						throw new ArgumentException(Res.GetString("Argument_NamespaceDeclarationXmlns"));
					}
					string localName = name.LocalName;
					if (localName == "xml")
					{
						throw new ArgumentException(Res.GetString("Argument_NamespaceDeclarationXml"));
					}
					if (localName == "xmlns")
					{
						throw new ArgumentException(Res.GetString("Argument_NamespaceDeclarationXmlns"));
					}
				}
			}
			else if (namespaceName.Length == 0 && name.LocalName == "xmlns")
			{
				if (value == "http://www.w3.org/XML/1998/namespace")
				{
					throw new ArgumentException(Res.GetString("Argument_NamespaceDeclarationXml"));
				}
				if (value == "http://www.w3.org/2000/xmlns/")
				{
					throw new ArgumentException(Res.GetString("Argument_NamespaceDeclarationXmlns"));
				}
			}
		}

		// Token: 0x040000AD RID: 173
		private static IEnumerable<XAttribute> emptySequence;

		// Token: 0x040000AE RID: 174
		internal XAttribute next;

		// Token: 0x040000AF RID: 175
		internal XName name;

		// Token: 0x040000B0 RID: 176
		internal string value;
	}
}
