using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Xml.Schema;
using MS.Internal.Xml.XPath;

namespace System.Xml.XPath
{
	// Token: 0x020000BA RID: 186
	[DebuggerDisplay("{debuggerDisplayProxy}")]
	public abstract class XPathNavigator : XPathItem, ICloneable, IXPathNavigable, IXmlNamespaceResolver
	{
		// Token: 0x06000A71 RID: 2673 RVA: 0x00030AD3 File Offset: 0x0002FAD3
		public override string ToString()
		{
			return this.Value;
		}

		// Token: 0x17000244 RID: 580
		// (get) Token: 0x06000A72 RID: 2674 RVA: 0x00030ADB File Offset: 0x0002FADB
		public sealed override bool IsNode
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000245 RID: 581
		// (get) Token: 0x06000A73 RID: 2675 RVA: 0x00030AE0 File Offset: 0x0002FAE0
		public override XmlSchemaType XmlType
		{
			get
			{
				IXmlSchemaInfo schemaInfo = this.SchemaInfo;
				if (schemaInfo == null || schemaInfo.Validity != XmlSchemaValidity.Valid)
				{
					return null;
				}
				XmlSchemaType memberType = schemaInfo.MemberType;
				if (memberType != null)
				{
					return memberType;
				}
				return schemaInfo.SchemaType;
			}
		}

		// Token: 0x06000A74 RID: 2676 RVA: 0x00030B14 File Offset: 0x0002FB14
		public virtual void SetValue(string value)
		{
			throw new NotSupportedException();
		}

		// Token: 0x17000246 RID: 582
		// (get) Token: 0x06000A75 RID: 2677 RVA: 0x00030B1C File Offset: 0x0002FB1C
		public override object TypedValue
		{
			get
			{
				IXmlSchemaInfo schemaInfo = this.SchemaInfo;
				if (schemaInfo != null)
				{
					if (schemaInfo.Validity == XmlSchemaValidity.Valid)
					{
						XmlSchemaType xmlSchemaType = schemaInfo.MemberType;
						if (xmlSchemaType == null)
						{
							xmlSchemaType = schemaInfo.SchemaType;
						}
						if (xmlSchemaType != null)
						{
							XmlSchemaDatatype datatype = xmlSchemaType.Datatype;
							if (datatype != null)
							{
								return xmlSchemaType.ValueConverter.ChangeType(this.Value, datatype.ValueType, this);
							}
						}
					}
					else
					{
						XmlSchemaType xmlSchemaType = schemaInfo.SchemaType;
						if (xmlSchemaType != null)
						{
							XmlSchemaDatatype datatype = xmlSchemaType.Datatype;
							if (datatype != null)
							{
								return xmlSchemaType.ValueConverter.ChangeType(datatype.ParseValue(this.Value, this.NameTable, this), datatype.ValueType, this);
							}
						}
					}
				}
				return this.Value;
			}
		}

		// Token: 0x06000A76 RID: 2678 RVA: 0x00030BB4 File Offset: 0x0002FBB4
		public virtual void SetTypedValue(object typedValue)
		{
			if (typedValue == null)
			{
				throw new ArgumentNullException("typedValue");
			}
			switch (this.NodeType)
			{
			case XPathNodeType.Element:
			case XPathNodeType.Attribute:
			{
				string text = null;
				IXmlSchemaInfo schemaInfo = this.SchemaInfo;
				if (schemaInfo != null)
				{
					XmlSchemaType schemaType = schemaInfo.SchemaType;
					if (schemaType != null)
					{
						text = schemaType.ValueConverter.ToString(typedValue, this);
						XmlSchemaDatatype datatype = schemaType.Datatype;
						if (datatype != null)
						{
							datatype.ParseValue(text, this.NameTable, this);
						}
					}
				}
				if (text == null)
				{
					text = XmlUntypedConverter.Untyped.ToString(typedValue, this);
				}
				this.SetValue(text);
				return;
			}
			default:
				throw new InvalidOperationException(Res.GetString("Xpn_BadPosition"));
			}
		}

		// Token: 0x17000247 RID: 583
		// (get) Token: 0x06000A77 RID: 2679 RVA: 0x00030C4C File Offset: 0x0002FC4C
		public override Type ValueType
		{
			get
			{
				IXmlSchemaInfo schemaInfo = this.SchemaInfo;
				if (schemaInfo != null)
				{
					if (schemaInfo.Validity == XmlSchemaValidity.Valid)
					{
						XmlSchemaType xmlSchemaType = schemaInfo.MemberType;
						if (xmlSchemaType == null)
						{
							xmlSchemaType = schemaInfo.SchemaType;
						}
						if (xmlSchemaType != null)
						{
							XmlSchemaDatatype datatype = xmlSchemaType.Datatype;
							if (datatype != null)
							{
								return datatype.ValueType;
							}
						}
					}
					else
					{
						XmlSchemaType xmlSchemaType = schemaInfo.SchemaType;
						if (xmlSchemaType != null)
						{
							XmlSchemaDatatype datatype = xmlSchemaType.Datatype;
							if (datatype != null)
							{
								return datatype.ValueType;
							}
						}
					}
				}
				return typeof(string);
			}
		}

		// Token: 0x17000248 RID: 584
		// (get) Token: 0x06000A78 RID: 2680 RVA: 0x00030CB8 File Offset: 0x0002FCB8
		public override bool ValueAsBoolean
		{
			get
			{
				IXmlSchemaInfo schemaInfo = this.SchemaInfo;
				if (schemaInfo != null)
				{
					if (schemaInfo.Validity == XmlSchemaValidity.Valid)
					{
						XmlSchemaType xmlSchemaType = schemaInfo.MemberType;
						if (xmlSchemaType == null)
						{
							xmlSchemaType = schemaInfo.SchemaType;
						}
						if (xmlSchemaType != null)
						{
							return xmlSchemaType.ValueConverter.ToBoolean(this.Value);
						}
					}
					else
					{
						XmlSchemaType xmlSchemaType = schemaInfo.SchemaType;
						if (xmlSchemaType != null)
						{
							XmlSchemaDatatype datatype = xmlSchemaType.Datatype;
							if (datatype != null)
							{
								return xmlSchemaType.ValueConverter.ToBoolean(datatype.ParseValue(this.Value, this.NameTable, this));
							}
						}
					}
				}
				return XmlUntypedConverter.Untyped.ToBoolean(this.Value);
			}
		}

		// Token: 0x17000249 RID: 585
		// (get) Token: 0x06000A79 RID: 2681 RVA: 0x00030D44 File Offset: 0x0002FD44
		public override DateTime ValueAsDateTime
		{
			get
			{
				IXmlSchemaInfo schemaInfo = this.SchemaInfo;
				if (schemaInfo != null)
				{
					if (schemaInfo.Validity == XmlSchemaValidity.Valid)
					{
						XmlSchemaType xmlSchemaType = schemaInfo.MemberType;
						if (xmlSchemaType == null)
						{
							xmlSchemaType = schemaInfo.SchemaType;
						}
						if (xmlSchemaType != null)
						{
							return xmlSchemaType.ValueConverter.ToDateTime(this.Value);
						}
					}
					else
					{
						XmlSchemaType xmlSchemaType = schemaInfo.SchemaType;
						if (xmlSchemaType != null)
						{
							XmlSchemaDatatype datatype = xmlSchemaType.Datatype;
							if (datatype != null)
							{
								return xmlSchemaType.ValueConverter.ToDateTime(datatype.ParseValue(this.Value, this.NameTable, this));
							}
						}
					}
				}
				return XmlUntypedConverter.Untyped.ToDateTime(this.Value);
			}
		}

		// Token: 0x1700024A RID: 586
		// (get) Token: 0x06000A7A RID: 2682 RVA: 0x00030DD0 File Offset: 0x0002FDD0
		public override double ValueAsDouble
		{
			get
			{
				IXmlSchemaInfo schemaInfo = this.SchemaInfo;
				if (schemaInfo != null)
				{
					if (schemaInfo.Validity == XmlSchemaValidity.Valid)
					{
						XmlSchemaType xmlSchemaType = schemaInfo.MemberType;
						if (xmlSchemaType == null)
						{
							xmlSchemaType = schemaInfo.SchemaType;
						}
						if (xmlSchemaType != null)
						{
							return xmlSchemaType.ValueConverter.ToDouble(this.Value);
						}
					}
					else
					{
						XmlSchemaType xmlSchemaType = schemaInfo.SchemaType;
						if (xmlSchemaType != null)
						{
							XmlSchemaDatatype datatype = xmlSchemaType.Datatype;
							if (datatype != null)
							{
								return xmlSchemaType.ValueConverter.ToDouble(datatype.ParseValue(this.Value, this.NameTable, this));
							}
						}
					}
				}
				return XmlUntypedConverter.Untyped.ToDouble(this.Value);
			}
		}

		// Token: 0x1700024B RID: 587
		// (get) Token: 0x06000A7B RID: 2683 RVA: 0x00030E5C File Offset: 0x0002FE5C
		public override int ValueAsInt
		{
			get
			{
				IXmlSchemaInfo schemaInfo = this.SchemaInfo;
				if (schemaInfo != null)
				{
					if (schemaInfo.Validity == XmlSchemaValidity.Valid)
					{
						XmlSchemaType xmlSchemaType = schemaInfo.MemberType;
						if (xmlSchemaType == null)
						{
							xmlSchemaType = schemaInfo.SchemaType;
						}
						if (xmlSchemaType != null)
						{
							return xmlSchemaType.ValueConverter.ToInt32(this.Value);
						}
					}
					else
					{
						XmlSchemaType xmlSchemaType = schemaInfo.SchemaType;
						if (xmlSchemaType != null)
						{
							XmlSchemaDatatype datatype = xmlSchemaType.Datatype;
							if (datatype != null)
							{
								return xmlSchemaType.ValueConverter.ToInt32(datatype.ParseValue(this.Value, this.NameTable, this));
							}
						}
					}
				}
				return XmlUntypedConverter.Untyped.ToInt32(this.Value);
			}
		}

		// Token: 0x1700024C RID: 588
		// (get) Token: 0x06000A7C RID: 2684 RVA: 0x00030EE8 File Offset: 0x0002FEE8
		public override long ValueAsLong
		{
			get
			{
				IXmlSchemaInfo schemaInfo = this.SchemaInfo;
				if (schemaInfo != null)
				{
					if (schemaInfo.Validity == XmlSchemaValidity.Valid)
					{
						XmlSchemaType xmlSchemaType = schemaInfo.MemberType;
						if (xmlSchemaType == null)
						{
							xmlSchemaType = schemaInfo.SchemaType;
						}
						if (xmlSchemaType != null)
						{
							return xmlSchemaType.ValueConverter.ToInt64(this.Value);
						}
					}
					else
					{
						XmlSchemaType xmlSchemaType = schemaInfo.SchemaType;
						if (xmlSchemaType != null)
						{
							XmlSchemaDatatype datatype = xmlSchemaType.Datatype;
							if (datatype != null)
							{
								return xmlSchemaType.ValueConverter.ToInt64(datatype.ParseValue(this.Value, this.NameTable, this));
							}
						}
					}
				}
				return XmlUntypedConverter.Untyped.ToInt64(this.Value);
			}
		}

		// Token: 0x06000A7D RID: 2685 RVA: 0x00030F74 File Offset: 0x0002FF74
		public override object ValueAs(Type returnType, IXmlNamespaceResolver nsResolver)
		{
			if (nsResolver == null)
			{
				nsResolver = this;
			}
			IXmlSchemaInfo schemaInfo = this.SchemaInfo;
			if (schemaInfo != null)
			{
				if (schemaInfo.Validity == XmlSchemaValidity.Valid)
				{
					XmlSchemaType xmlSchemaType = schemaInfo.MemberType;
					if (xmlSchemaType == null)
					{
						xmlSchemaType = schemaInfo.SchemaType;
					}
					if (xmlSchemaType != null)
					{
						return xmlSchemaType.ValueConverter.ChangeType(this.Value, returnType, nsResolver);
					}
				}
				else
				{
					XmlSchemaType xmlSchemaType = schemaInfo.SchemaType;
					if (xmlSchemaType != null)
					{
						XmlSchemaDatatype datatype = xmlSchemaType.Datatype;
						if (datatype != null)
						{
							return xmlSchemaType.ValueConverter.ChangeType(datatype.ParseValue(this.Value, this.NameTable, nsResolver), returnType, nsResolver);
						}
					}
				}
			}
			return XmlUntypedConverter.Untyped.ChangeType(this.Value, returnType, nsResolver);
		}

		// Token: 0x06000A7E RID: 2686 RVA: 0x00031009 File Offset: 0x00030009
		object ICloneable.Clone()
		{
			return this.Clone();
		}

		// Token: 0x06000A7F RID: 2687 RVA: 0x00031011 File Offset: 0x00030011
		public virtual XPathNavigator CreateNavigator()
		{
			return this.Clone();
		}

		// Token: 0x1700024D RID: 589
		// (get) Token: 0x06000A80 RID: 2688
		public abstract XmlNameTable NameTable { get; }

		// Token: 0x06000A81 RID: 2689 RVA: 0x0003101C File Offset: 0x0003001C
		public virtual string LookupNamespace(string prefix)
		{
			if (prefix == null)
			{
				return null;
			}
			if (this.NodeType != XPathNodeType.Element)
			{
				XPathNavigator xpathNavigator = this.Clone();
				if (xpathNavigator.MoveToParent())
				{
					return xpathNavigator.LookupNamespace(prefix);
				}
			}
			else if (this.MoveToNamespace(prefix))
			{
				string value = this.Value;
				this.MoveToParent();
				return value;
			}
			if (prefix.Length == 0)
			{
				return string.Empty;
			}
			if (prefix == "xml")
			{
				return "http://www.w3.org/XML/1998/namespace";
			}
			if (prefix == "xmlns")
			{
				return "http://www.w3.org/2000/xmlns/";
			}
			return null;
		}

		// Token: 0x06000A82 RID: 2690 RVA: 0x0003109C File Offset: 0x0003009C
		public virtual string LookupPrefix(string namespaceURI)
		{
			if (namespaceURI == null)
			{
				return null;
			}
			XPathNavigator xpathNavigator = this.Clone();
			if (this.NodeType != XPathNodeType.Element)
			{
				if (xpathNavigator.MoveToParent())
				{
					return xpathNavigator.LookupPrefix(namespaceURI);
				}
			}
			else if (xpathNavigator.MoveToFirstNamespace(XPathNamespaceScope.All))
			{
				while (!(namespaceURI == xpathNavigator.Value))
				{
					if (!xpathNavigator.MoveToNextNamespace(XPathNamespaceScope.All))
					{
						goto IL_4C;
					}
				}
				return xpathNavigator.LocalName;
			}
			IL_4C:
			if (namespaceURI == this.LookupNamespace(string.Empty))
			{
				return string.Empty;
			}
			if (namespaceURI == "http://www.w3.org/XML/1998/namespace")
			{
				return "xml";
			}
			if (namespaceURI == "http://www.w3.org/2000/xmlns/")
			{
				return "xmlns";
			}
			return null;
		}

		// Token: 0x06000A83 RID: 2691 RVA: 0x00031138 File Offset: 0x00030138
		public virtual IDictionary<string, string> GetNamespacesInScope(XmlNamespaceScope scope)
		{
			XPathNodeType nodeType = this.NodeType;
			if ((nodeType != XPathNodeType.Element && scope != XmlNamespaceScope.Local) || nodeType == XPathNodeType.Attribute || nodeType == XPathNodeType.Namespace)
			{
				XPathNavigator xpathNavigator = this.Clone();
				if (xpathNavigator.MoveToParent())
				{
					return xpathNavigator.GetNamespacesInScope(scope);
				}
			}
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			if (scope == XmlNamespaceScope.All)
			{
				dictionary["xml"] = "http://www.w3.org/XML/1998/namespace";
			}
			if (this.MoveToFirstNamespace((XPathNamespaceScope)scope))
			{
				do
				{
					string localName = this.LocalName;
					string value = this.Value;
					if (localName.Length != 0 || value.Length != 0 || scope == XmlNamespaceScope.Local)
					{
						dictionary[localName] = value;
					}
				}
				while (this.MoveToNextNamespace((XPathNamespaceScope)scope));
				this.MoveToParent();
			}
			return dictionary;
		}

		// Token: 0x1700024E RID: 590
		// (get) Token: 0x06000A84 RID: 2692 RVA: 0x000311D3 File Offset: 0x000301D3
		public static IEqualityComparer NavigatorComparer
		{
			get
			{
				return XPathNavigator.comparer;
			}
		}

		// Token: 0x06000A85 RID: 2693
		public abstract XPathNavigator Clone();

		// Token: 0x1700024F RID: 591
		// (get) Token: 0x06000A86 RID: 2694
		public abstract XPathNodeType NodeType { get; }

		// Token: 0x17000250 RID: 592
		// (get) Token: 0x06000A87 RID: 2695
		public abstract string LocalName { get; }

		// Token: 0x17000251 RID: 593
		// (get) Token: 0x06000A88 RID: 2696
		public abstract string Name { get; }

		// Token: 0x17000252 RID: 594
		// (get) Token: 0x06000A89 RID: 2697
		public abstract string NamespaceURI { get; }

		// Token: 0x17000253 RID: 595
		// (get) Token: 0x06000A8A RID: 2698
		public abstract string Prefix { get; }

		// Token: 0x17000254 RID: 596
		// (get) Token: 0x06000A8B RID: 2699
		public abstract string BaseURI { get; }

		// Token: 0x17000255 RID: 597
		// (get) Token: 0x06000A8C RID: 2700
		public abstract bool IsEmptyElement { get; }

		// Token: 0x17000256 RID: 598
		// (get) Token: 0x06000A8D RID: 2701 RVA: 0x000311DC File Offset: 0x000301DC
		public virtual string XmlLang
		{
			get
			{
				XPathNavigator xpathNavigator = this.Clone();
				while (!xpathNavigator.MoveToAttribute("lang", "http://www.w3.org/XML/1998/namespace"))
				{
					if (!xpathNavigator.MoveToParent())
					{
						return string.Empty;
					}
				}
				return xpathNavigator.Value;
			}
		}

		// Token: 0x06000A8E RID: 2702 RVA: 0x00031218 File Offset: 0x00030218
		public virtual XmlReader ReadSubtree()
		{
			switch (this.NodeType)
			{
			case XPathNodeType.Root:
			case XPathNodeType.Element:
				return this.CreateReader();
			default:
				throw new InvalidOperationException(Res.GetString("Xpn_BadPosition"));
			}
		}

		// Token: 0x06000A8F RID: 2703 RVA: 0x00031250 File Offset: 0x00030250
		public virtual void WriteSubtree(XmlWriter writer)
		{
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			writer.WriteNode(this, true);
		}

		// Token: 0x17000257 RID: 599
		// (get) Token: 0x06000A90 RID: 2704 RVA: 0x00031268 File Offset: 0x00030268
		public virtual object UnderlyingObject
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000258 RID: 600
		// (get) Token: 0x06000A91 RID: 2705 RVA: 0x0003126B File Offset: 0x0003026B
		public virtual bool HasAttributes
		{
			get
			{
				if (!this.MoveToFirstAttribute())
				{
					return false;
				}
				this.MoveToParent();
				return true;
			}
		}

		// Token: 0x06000A92 RID: 2706 RVA: 0x00031280 File Offset: 0x00030280
		public virtual string GetAttribute(string localName, string namespaceURI)
		{
			if (!this.MoveToAttribute(localName, namespaceURI))
			{
				return "";
			}
			string value = this.Value;
			this.MoveToParent();
			return value;
		}

		// Token: 0x06000A93 RID: 2707 RVA: 0x000312AC File Offset: 0x000302AC
		public virtual bool MoveToAttribute(string localName, string namespaceURI)
		{
			if (this.MoveToFirstAttribute())
			{
				while (!(localName == this.LocalName) || !(namespaceURI == this.NamespaceURI))
				{
					if (!this.MoveToNextAttribute())
					{
						this.MoveToParent();
						return false;
					}
				}
				return true;
			}
			return false;
		}

		// Token: 0x06000A94 RID: 2708
		public abstract bool MoveToFirstAttribute();

		// Token: 0x06000A95 RID: 2709
		public abstract bool MoveToNextAttribute();

		// Token: 0x06000A96 RID: 2710 RVA: 0x000312E4 File Offset: 0x000302E4
		public virtual string GetNamespace(string name)
		{
			if (this.MoveToNamespace(name))
			{
				string value = this.Value;
				this.MoveToParent();
				return value;
			}
			if (name == "xml")
			{
				return "http://www.w3.org/XML/1998/namespace";
			}
			if (name == "xmlns")
			{
				return "http://www.w3.org/2000/xmlns/";
			}
			return string.Empty;
		}

		// Token: 0x06000A97 RID: 2711 RVA: 0x00031335 File Offset: 0x00030335
		public virtual bool MoveToNamespace(string name)
		{
			if (this.MoveToFirstNamespace(XPathNamespaceScope.All))
			{
				while (!(name == this.LocalName))
				{
					if (!this.MoveToNextNamespace(XPathNamespaceScope.All))
					{
						this.MoveToParent();
						return false;
					}
				}
				return true;
			}
			return false;
		}

		// Token: 0x06000A98 RID: 2712
		public abstract bool MoveToFirstNamespace(XPathNamespaceScope namespaceScope);

		// Token: 0x06000A99 RID: 2713
		public abstract bool MoveToNextNamespace(XPathNamespaceScope namespaceScope);

		// Token: 0x06000A9A RID: 2714 RVA: 0x00031361 File Offset: 0x00030361
		public bool MoveToFirstNamespace()
		{
			return this.MoveToFirstNamespace(XPathNamespaceScope.All);
		}

		// Token: 0x06000A9B RID: 2715 RVA: 0x0003136A File Offset: 0x0003036A
		public bool MoveToNextNamespace()
		{
			return this.MoveToNextNamespace(XPathNamespaceScope.All);
		}

		// Token: 0x06000A9C RID: 2716
		public abstract bool MoveToNext();

		// Token: 0x06000A9D RID: 2717
		public abstract bool MoveToPrevious();

		// Token: 0x06000A9E RID: 2718 RVA: 0x00031374 File Offset: 0x00030374
		public virtual bool MoveToFirst()
		{
			switch (this.NodeType)
			{
			case XPathNodeType.Attribute:
			case XPathNodeType.Namespace:
				return false;
			default:
				return this.MoveToParent() && this.MoveToFirstChild();
			}
		}

		// Token: 0x06000A9F RID: 2719
		public abstract bool MoveToFirstChild();

		// Token: 0x06000AA0 RID: 2720
		public abstract bool MoveToParent();

		// Token: 0x06000AA1 RID: 2721 RVA: 0x000313AC File Offset: 0x000303AC
		public virtual void MoveToRoot()
		{
			while (this.MoveToParent())
			{
			}
		}

		// Token: 0x06000AA2 RID: 2722
		public abstract bool MoveTo(XPathNavigator other);

		// Token: 0x06000AA3 RID: 2723
		public abstract bool MoveToId(string id);

		// Token: 0x06000AA4 RID: 2724 RVA: 0x000313B8 File Offset: 0x000303B8
		public virtual bool MoveToChild(string localName, string namespaceURI)
		{
			if (this.MoveToFirstChild())
			{
				while (this.NodeType != XPathNodeType.Element || !(localName == this.LocalName) || !(namespaceURI == this.NamespaceURI))
				{
					if (!this.MoveToNext())
					{
						this.MoveToParent();
						return false;
					}
				}
				return true;
			}
			return false;
		}

		// Token: 0x06000AA5 RID: 2725 RVA: 0x00031404 File Offset: 0x00030404
		public virtual bool MoveToChild(XPathNodeType type)
		{
			if (this.MoveToFirstChild())
			{
				int contentKindMask = XPathNavigator.GetContentKindMask(type);
				while ((1 << (int)this.NodeType & contentKindMask) == 0)
				{
					if (!this.MoveToNext())
					{
						this.MoveToParent();
						return false;
					}
				}
				return true;
			}
			return false;
		}

		// Token: 0x06000AA6 RID: 2726 RVA: 0x00031441 File Offset: 0x00030441
		public virtual bool MoveToFollowing(string localName, string namespaceURI)
		{
			return this.MoveToFollowing(localName, namespaceURI, null);
		}

		// Token: 0x06000AA7 RID: 2727 RVA: 0x0003144C File Offset: 0x0003044C
		public virtual bool MoveToFollowing(string localName, string namespaceURI, XPathNavigator end)
		{
			XPathNavigator other = this.Clone();
			if (end != null)
			{
				switch (end.NodeType)
				{
				case XPathNodeType.Attribute:
				case XPathNodeType.Namespace:
					end = end.Clone();
					end.MoveToNonDescendant();
					break;
				}
			}
			switch (this.NodeType)
			{
			case XPathNodeType.Attribute:
			case XPathNodeType.Namespace:
				if (!this.MoveToParent())
				{
					return false;
				}
				break;
			}
			for (;;)
			{
				if (!this.MoveToFirstChild())
				{
					while (!this.MoveToNext())
					{
						if (!this.MoveToParent())
						{
							goto Block_6;
						}
					}
				}
				if (end != null && this.IsSamePosition(end))
				{
					goto Block_8;
				}
				if (this.NodeType == XPathNodeType.Element && !(localName != this.LocalName) && !(namespaceURI != this.NamespaceURI))
				{
					return true;
				}
			}
			Block_6:
			this.MoveTo(other);
			return false;
			Block_8:
			this.MoveTo(other);
			return false;
		}

		// Token: 0x06000AA8 RID: 2728 RVA: 0x0003150C File Offset: 0x0003050C
		public virtual bool MoveToFollowing(XPathNodeType type)
		{
			return this.MoveToFollowing(type, null);
		}

		// Token: 0x06000AA9 RID: 2729 RVA: 0x00031518 File Offset: 0x00030518
		public virtual bool MoveToFollowing(XPathNodeType type, XPathNavigator end)
		{
			XPathNavigator other = this.Clone();
			int contentKindMask = XPathNavigator.GetContentKindMask(type);
			if (end != null)
			{
				switch (end.NodeType)
				{
				case XPathNodeType.Attribute:
				case XPathNodeType.Namespace:
					end = end.Clone();
					end.MoveToNonDescendant();
					break;
				}
			}
			switch (this.NodeType)
			{
			case XPathNodeType.Attribute:
			case XPathNodeType.Namespace:
				if (!this.MoveToParent())
				{
					return false;
				}
				break;
			}
			for (;;)
			{
				if (!this.MoveToFirstChild())
				{
					while (!this.MoveToNext())
					{
						if (!this.MoveToParent())
						{
							goto Block_6;
						}
					}
				}
				if (end != null && this.IsSamePosition(end))
				{
					goto Block_8;
				}
				if ((1 << (int)this.NodeType & contentKindMask) != 0)
				{
					return true;
				}
			}
			Block_6:
			this.MoveTo(other);
			return false;
			Block_8:
			this.MoveTo(other);
			return false;
		}

		// Token: 0x06000AAA RID: 2730 RVA: 0x000315CC File Offset: 0x000305CC
		public virtual bool MoveToNext(string localName, string namespaceURI)
		{
			XPathNavigator other = this.Clone();
			while (this.MoveToNext())
			{
				if (this.NodeType == XPathNodeType.Element && localName == this.LocalName && namespaceURI == this.NamespaceURI)
				{
					return true;
				}
			}
			this.MoveTo(other);
			return false;
		}

		// Token: 0x06000AAB RID: 2731 RVA: 0x0003161C File Offset: 0x0003061C
		public virtual bool MoveToNext(XPathNodeType type)
		{
			XPathNavigator other = this.Clone();
			int contentKindMask = XPathNavigator.GetContentKindMask(type);
			while (this.MoveToNext())
			{
				if ((1 << (int)this.NodeType & contentKindMask) != 0)
				{
					return true;
				}
			}
			this.MoveTo(other);
			return false;
		}

		// Token: 0x17000259 RID: 601
		// (get) Token: 0x06000AAC RID: 2732 RVA: 0x0003165B File Offset: 0x0003065B
		public virtual bool HasChildren
		{
			get
			{
				if (this.MoveToFirstChild())
				{
					this.MoveToParent();
					return true;
				}
				return false;
			}
		}

		// Token: 0x06000AAD RID: 2733
		public abstract bool IsSamePosition(XPathNavigator other);

		// Token: 0x06000AAE RID: 2734 RVA: 0x0003166F File Offset: 0x0003066F
		public virtual bool IsDescendant(XPathNavigator nav)
		{
			if (nav != null)
			{
				nav = nav.Clone();
				while (nav.MoveToParent())
				{
					if (nav.IsSamePosition(this))
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06000AAF RID: 2735 RVA: 0x00031694 File Offset: 0x00030694
		public virtual XmlNodeOrder ComparePosition(XPathNavigator nav)
		{
			if (nav == null)
			{
				return XmlNodeOrder.Unknown;
			}
			if (this.IsSamePosition(nav))
			{
				return XmlNodeOrder.Same;
			}
			XPathNavigator xpathNavigator = this.Clone();
			XPathNavigator xpathNavigator2 = nav.Clone();
			int i = XPathNavigator.GetDepth(xpathNavigator.Clone());
			int j = XPathNavigator.GetDepth(xpathNavigator2.Clone());
			if (i > j)
			{
				while (i > j)
				{
					xpathNavigator.MoveToParent();
					i--;
				}
				if (xpathNavigator.IsSamePosition(xpathNavigator2))
				{
					return XmlNodeOrder.After;
				}
			}
			if (j > i)
			{
				while (j > i)
				{
					xpathNavigator2.MoveToParent();
					j--;
				}
				if (xpathNavigator.IsSamePosition(xpathNavigator2))
				{
					return XmlNodeOrder.Before;
				}
			}
			XPathNavigator xpathNavigator3 = xpathNavigator.Clone();
			XPathNavigator xpathNavigator4 = xpathNavigator2.Clone();
			while (xpathNavigator3.MoveToParent() && xpathNavigator4.MoveToParent())
			{
				if (xpathNavigator3.IsSamePosition(xpathNavigator4))
				{
					xpathNavigator.GetType().ToString() != "Microsoft.VisualStudio.Modeling.StoreNavigator";
					return this.CompareSiblings(xpathNavigator, xpathNavigator2);
				}
				xpathNavigator.MoveToParent();
				xpathNavigator2.MoveToParent();
			}
			return XmlNodeOrder.Unknown;
		}

		// Token: 0x1700025A RID: 602
		// (get) Token: 0x06000AB0 RID: 2736 RVA: 0x00031774 File Offset: 0x00030774
		public virtual IXmlSchemaInfo SchemaInfo
		{
			get
			{
				return this as IXmlSchemaInfo;
			}
		}

		// Token: 0x06000AB1 RID: 2737 RVA: 0x0003177C File Offset: 0x0003077C
		public virtual bool CheckValidity(XmlSchemaSet schemas, ValidationEventHandler validationEventHandler)
		{
			XmlSchemaType xmlSchemaType = null;
			XmlSchemaElement xmlSchemaElement = null;
			XmlSchemaAttribute xmlSchemaAttribute = null;
			switch (this.NodeType)
			{
			case XPathNodeType.Root:
				if (schemas == null)
				{
					throw new InvalidOperationException(Res.GetString("XPathDocument_MissingSchemas"));
				}
				xmlSchemaType = null;
				break;
			case XPathNodeType.Element:
			{
				if (schemas == null)
				{
					throw new InvalidOperationException(Res.GetString("XPathDocument_MissingSchemas"));
				}
				IXmlSchemaInfo schemaInfo = this.SchemaInfo;
				if (schemaInfo != null)
				{
					xmlSchemaType = schemaInfo.SchemaType;
					xmlSchemaElement = schemaInfo.SchemaElement;
				}
				if (xmlSchemaType == null && xmlSchemaElement == null)
				{
					throw new InvalidOperationException(Res.GetString("XPathDocument_NotEnoughSchemaInfo", null));
				}
				break;
			}
			case XPathNodeType.Attribute:
			{
				if (schemas == null)
				{
					throw new InvalidOperationException(Res.GetString("XPathDocument_MissingSchemas"));
				}
				IXmlSchemaInfo schemaInfo = this.SchemaInfo;
				if (schemaInfo != null)
				{
					xmlSchemaType = schemaInfo.SchemaType;
					xmlSchemaAttribute = schemaInfo.SchemaAttribute;
				}
				if (xmlSchemaType == null && xmlSchemaAttribute == null)
				{
					throw new InvalidOperationException(Res.GetString("XPathDocument_NotEnoughSchemaInfo", null));
				}
				break;
			}
			default:
				throw new InvalidOperationException(Res.GetString("XPathDocument_ValidateInvalidNodeType", null));
			}
			XmlReader xmlReader = this.CreateReader();
			XPathNavigator.CheckValidityHelper checkValidityHelper = new XPathNavigator.CheckValidityHelper(validationEventHandler, xmlReader as XPathNavigatorReader);
			validationEventHandler = new ValidationEventHandler(checkValidityHelper.ValidationCallback);
			XmlReader validatingReader = this.GetValidatingReader(xmlReader, schemas, validationEventHandler, xmlSchemaType, xmlSchemaElement, xmlSchemaAttribute);
			while (validatingReader.Read())
			{
			}
			return checkValidityHelper.IsValid;
		}

		// Token: 0x06000AB2 RID: 2738 RVA: 0x000318A4 File Offset: 0x000308A4
		private XmlReader GetValidatingReader(XmlReader reader, XmlSchemaSet schemas, ValidationEventHandler validationEvent, XmlSchemaType schemaType, XmlSchemaElement schemaElement, XmlSchemaAttribute schemaAttribute)
		{
			if (schemaAttribute != null)
			{
				return schemaAttribute.Validate(reader, null, schemas, validationEvent);
			}
			if (schemaElement != null)
			{
				return schemaElement.Validate(reader, null, schemas, validationEvent);
			}
			if (schemaType != null)
			{
				return schemaType.Validate(reader, null, schemas, validationEvent);
			}
			XmlReaderSettings xmlReaderSettings = new XmlReaderSettings();
			xmlReaderSettings.ConformanceLevel = ConformanceLevel.Auto;
			xmlReaderSettings.ValidationType = ValidationType.Schema;
			xmlReaderSettings.Schemas = schemas;
			xmlReaderSettings.ValidationEventHandler += validationEvent;
			return XmlReader.Create(reader, xmlReaderSettings);
		}

		// Token: 0x06000AB3 RID: 2739 RVA: 0x0003190A File Offset: 0x0003090A
		public virtual XPathExpression Compile(string xpath)
		{
			return XPathExpression.Compile(xpath);
		}

		// Token: 0x06000AB4 RID: 2740 RVA: 0x00031912 File Offset: 0x00030912
		public virtual XPathNavigator SelectSingleNode(string xpath)
		{
			return this.SelectSingleNode(XPathExpression.Compile(xpath));
		}

		// Token: 0x06000AB5 RID: 2741 RVA: 0x00031920 File Offset: 0x00030920
		public virtual XPathNavigator SelectSingleNode(string xpath, IXmlNamespaceResolver resolver)
		{
			return this.SelectSingleNode(XPathExpression.Compile(xpath, resolver));
		}

		// Token: 0x06000AB6 RID: 2742 RVA: 0x00031930 File Offset: 0x00030930
		public virtual XPathNavigator SelectSingleNode(XPathExpression expression)
		{
			XPathNodeIterator xpathNodeIterator = this.Select(expression);
			if (xpathNodeIterator.MoveNext())
			{
				return xpathNodeIterator.Current;
			}
			return null;
		}

		// Token: 0x06000AB7 RID: 2743 RVA: 0x00031955 File Offset: 0x00030955
		public virtual XPathNodeIterator Select(string xpath)
		{
			return this.Select(XPathExpression.Compile(xpath));
		}

		// Token: 0x06000AB8 RID: 2744 RVA: 0x00031963 File Offset: 0x00030963
		public virtual XPathNodeIterator Select(string xpath, IXmlNamespaceResolver resolver)
		{
			return this.Select(XPathExpression.Compile(xpath, resolver));
		}

		// Token: 0x06000AB9 RID: 2745 RVA: 0x00031974 File Offset: 0x00030974
		public virtual XPathNodeIterator Select(XPathExpression expr)
		{
			XPathNodeIterator xpathNodeIterator = this.Evaluate(expr) as XPathNodeIterator;
			if (xpathNodeIterator == null)
			{
				throw XPathException.Create("Xp_NodeSetExpected");
			}
			return xpathNodeIterator;
		}

		// Token: 0x06000ABA RID: 2746 RVA: 0x0003199D File Offset: 0x0003099D
		public virtual object Evaluate(string xpath)
		{
			return this.Evaluate(XPathExpression.Compile(xpath), null);
		}

		// Token: 0x06000ABB RID: 2747 RVA: 0x000319AC File Offset: 0x000309AC
		public virtual object Evaluate(string xpath, IXmlNamespaceResolver resolver)
		{
			return this.Evaluate(XPathExpression.Compile(xpath, resolver));
		}

		// Token: 0x06000ABC RID: 2748 RVA: 0x000319BB File Offset: 0x000309BB
		public virtual object Evaluate(XPathExpression expr)
		{
			return this.Evaluate(expr, null);
		}

		// Token: 0x06000ABD RID: 2749 RVA: 0x000319C8 File Offset: 0x000309C8
		public virtual object Evaluate(XPathExpression expr, XPathNodeIterator context)
		{
			CompiledXpathExpr compiledXpathExpr = expr as CompiledXpathExpr;
			if (compiledXpathExpr == null)
			{
				throw XPathException.Create("Xp_BadQueryObject");
			}
			Query query = Query.Clone(compiledXpathExpr.QueryTree);
			query.Reset();
			if (context == null)
			{
				context = new XPathSingletonIterator(this.Clone(), true);
			}
			object obj = query.Evaluate(context);
			if (obj is XPathNodeIterator)
			{
				return new XPathSelectionIterator(context.Current, query);
			}
			return obj;
		}

		// Token: 0x06000ABE RID: 2750 RVA: 0x00031A2C File Offset: 0x00030A2C
		public virtual bool Matches(XPathExpression expr)
		{
			CompiledXpathExpr compiledXpathExpr = expr as CompiledXpathExpr;
			if (compiledXpathExpr == null)
			{
				throw XPathException.Create("Xp_BadQueryObject");
			}
			Query query = Query.Clone(compiledXpathExpr.QueryTree);
			bool result;
			try
			{
				result = (query.MatchNode(this) != null);
			}
			catch (XPathException)
			{
				throw XPathException.Create("Xp_InvalidPattern", compiledXpathExpr.Expression);
			}
			return result;
		}

		// Token: 0x06000ABF RID: 2751 RVA: 0x00031A90 File Offset: 0x00030A90
		public virtual bool Matches(string xpath)
		{
			return this.Matches(XPathNavigator.CompileMatchPattern(xpath));
		}

		// Token: 0x06000AC0 RID: 2752 RVA: 0x00031A9E File Offset: 0x00030A9E
		public virtual XPathNodeIterator SelectChildren(XPathNodeType type)
		{
			return new XPathChildIterator(this.Clone(), type);
		}

		// Token: 0x06000AC1 RID: 2753 RVA: 0x00031AAC File Offset: 0x00030AAC
		public virtual XPathNodeIterator SelectChildren(string name, string namespaceURI)
		{
			return new XPathChildIterator(this.Clone(), name, namespaceURI);
		}

		// Token: 0x06000AC2 RID: 2754 RVA: 0x00031ABB File Offset: 0x00030ABB
		public virtual XPathNodeIterator SelectAncestors(XPathNodeType type, bool matchSelf)
		{
			return new XPathAncestorIterator(this.Clone(), type, matchSelf);
		}

		// Token: 0x06000AC3 RID: 2755 RVA: 0x00031ACA File Offset: 0x00030ACA
		public virtual XPathNodeIterator SelectAncestors(string name, string namespaceURI, bool matchSelf)
		{
			return new XPathAncestorIterator(this.Clone(), name, namespaceURI, matchSelf);
		}

		// Token: 0x06000AC4 RID: 2756 RVA: 0x00031ADA File Offset: 0x00030ADA
		public virtual XPathNodeIterator SelectDescendants(XPathNodeType type, bool matchSelf)
		{
			return new XPathDescendantIterator(this.Clone(), type, matchSelf);
		}

		// Token: 0x06000AC5 RID: 2757 RVA: 0x00031AE9 File Offset: 0x00030AE9
		public virtual XPathNodeIterator SelectDescendants(string name, string namespaceURI, bool matchSelf)
		{
			return new XPathDescendantIterator(this.Clone(), name, namespaceURI, matchSelf);
		}

		// Token: 0x1700025B RID: 603
		// (get) Token: 0x06000AC6 RID: 2758 RVA: 0x00031AF9 File Offset: 0x00030AF9
		public virtual bool CanEdit
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06000AC7 RID: 2759 RVA: 0x00031AFC File Offset: 0x00030AFC
		public virtual XmlWriter PrependChild()
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000AC8 RID: 2760 RVA: 0x00031B03 File Offset: 0x00030B03
		public virtual XmlWriter AppendChild()
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000AC9 RID: 2761 RVA: 0x00031B0A File Offset: 0x00030B0A
		public virtual XmlWriter InsertAfter()
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000ACA RID: 2762 RVA: 0x00031B11 File Offset: 0x00030B11
		public virtual XmlWriter InsertBefore()
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000ACB RID: 2763 RVA: 0x00031B18 File Offset: 0x00030B18
		public virtual XmlWriter CreateAttributes()
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000ACC RID: 2764 RVA: 0x00031B1F File Offset: 0x00030B1F
		public virtual XmlWriter ReplaceRange(XPathNavigator lastSiblingToReplace)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000ACD RID: 2765 RVA: 0x00031B28 File Offset: 0x00030B28
		public virtual void ReplaceSelf(string newNode)
		{
			XmlReader newNode2 = this.CreateContextReader(newNode, false);
			this.ReplaceSelf(newNode2);
		}

		// Token: 0x06000ACE RID: 2766 RVA: 0x00031B48 File Offset: 0x00030B48
		public virtual void ReplaceSelf(XmlReader newNode)
		{
			if (newNode == null)
			{
				throw new ArgumentNullException("newNode");
			}
			XPathNodeType nodeType = this.NodeType;
			if (nodeType == XPathNodeType.Root || nodeType == XPathNodeType.Attribute || nodeType == XPathNodeType.Namespace)
			{
				throw new InvalidOperationException(Res.GetString("Xpn_BadPosition"));
			}
			XmlWriter xmlWriter = this.ReplaceRange(this);
			this.BuildSubtree(newNode, xmlWriter);
			xmlWriter.Close();
		}

		// Token: 0x06000ACF RID: 2767 RVA: 0x00031B9C File Offset: 0x00030B9C
		public virtual void ReplaceSelf(XPathNavigator newNode)
		{
			if (newNode == null)
			{
				throw new ArgumentNullException("newNode");
			}
			XmlReader newNode2 = newNode.CreateReader();
			this.ReplaceSelf(newNode2);
		}

		// Token: 0x1700025C RID: 604
		// (get) Token: 0x06000AD0 RID: 2768 RVA: 0x00031BC8 File Offset: 0x00030BC8
		// (set) Token: 0x06000AD1 RID: 2769 RVA: 0x00031CB4 File Offset: 0x00030CB4
		public virtual string OuterXml
		{
			get
			{
				if (this.NodeType == XPathNodeType.Attribute)
				{
					return this.Name + "=\"" + this.Value + "\"";
				}
				if (this.NodeType != XPathNodeType.Namespace)
				{
					StringWriter stringWriter = new StringWriter(CultureInfo.InvariantCulture);
					XmlWriter xmlWriter = XmlWriter.Create(stringWriter, new XmlWriterSettings
					{
						Indent = true,
						OmitXmlDeclaration = true,
						ConformanceLevel = ConformanceLevel.Auto
					});
					try
					{
						xmlWriter.WriteNode(this, true);
					}
					finally
					{
						xmlWriter.Close();
					}
					return stringWriter.ToString();
				}
				if (this.LocalName.Length == 0)
				{
					return "xmlns=\"" + this.Value + "\"";
				}
				return string.Concat(new string[]
				{
					"xmlns:",
					this.LocalName,
					"=\"",
					this.Value,
					"\""
				});
			}
			set
			{
				this.ReplaceSelf(value);
			}
		}

		// Token: 0x1700025D RID: 605
		// (get) Token: 0x06000AD2 RID: 2770 RVA: 0x00031CC0 File Offset: 0x00030CC0
		// (set) Token: 0x06000AD3 RID: 2771 RVA: 0x00031D68 File Offset: 0x00030D68
		public virtual string InnerXml
		{
			get
			{
				switch (this.NodeType)
				{
				case XPathNodeType.Root:
				case XPathNodeType.Element:
				{
					StringWriter stringWriter = new StringWriter(CultureInfo.InvariantCulture);
					XmlWriter xmlWriter = XmlWriter.Create(stringWriter, new XmlWriterSettings
					{
						Indent = true,
						OmitXmlDeclaration = true,
						ConformanceLevel = ConformanceLevel.Auto
					});
					try
					{
						if (this.MoveToFirstChild())
						{
							do
							{
								xmlWriter.WriteNode(this, true);
							}
							while (this.MoveToNext());
							this.MoveToParent();
						}
					}
					finally
					{
						xmlWriter.Close();
					}
					return stringWriter.ToString();
				}
				case XPathNodeType.Attribute:
				case XPathNodeType.Namespace:
					return this.Value;
				default:
					return string.Empty;
				}
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				switch (this.NodeType)
				{
				case XPathNodeType.Root:
				case XPathNodeType.Element:
				{
					XPathNavigator xpathNavigator = this.CreateNavigator();
					while (xpathNavigator.MoveToFirstChild())
					{
						xpathNavigator.DeleteSelf();
					}
					if (value.Length != 0)
					{
						xpathNavigator.AppendChild(value);
						return;
					}
					return;
				}
				case XPathNodeType.Attribute:
					this.SetValue(value);
					return;
				default:
					throw new InvalidOperationException(Res.GetString("Xpn_BadPosition"));
				}
			}
		}

		// Token: 0x06000AD4 RID: 2772 RVA: 0x00031DE0 File Offset: 0x00030DE0
		public virtual void AppendChild(string newChild)
		{
			XmlReader newChild2 = this.CreateContextReader(newChild, true);
			this.AppendChild(newChild2);
		}

		// Token: 0x06000AD5 RID: 2773 RVA: 0x00031E00 File Offset: 0x00030E00
		public virtual void AppendChild(XmlReader newChild)
		{
			if (newChild == null)
			{
				throw new ArgumentNullException("newChild");
			}
			XmlWriter xmlWriter = this.AppendChild();
			this.BuildSubtree(newChild, xmlWriter);
			xmlWriter.Close();
		}

		// Token: 0x06000AD6 RID: 2774 RVA: 0x00031E30 File Offset: 0x00030E30
		public virtual void AppendChild(XPathNavigator newChild)
		{
			if (newChild == null)
			{
				throw new ArgumentNullException("newChild");
			}
			if (!this.IsValidChildType(newChild.NodeType))
			{
				throw new InvalidOperationException(Res.GetString("Xpn_BadPosition"));
			}
			XmlReader newChild2 = newChild.CreateReader();
			this.AppendChild(newChild2);
		}

		// Token: 0x06000AD7 RID: 2775 RVA: 0x00031E78 File Offset: 0x00030E78
		public virtual void PrependChild(string newChild)
		{
			XmlReader newChild2 = this.CreateContextReader(newChild, true);
			this.PrependChild(newChild2);
		}

		// Token: 0x06000AD8 RID: 2776 RVA: 0x00031E98 File Offset: 0x00030E98
		public virtual void PrependChild(XmlReader newChild)
		{
			if (newChild == null)
			{
				throw new ArgumentNullException("newChild");
			}
			XmlWriter xmlWriter = this.PrependChild();
			this.BuildSubtree(newChild, xmlWriter);
			xmlWriter.Close();
		}

		// Token: 0x06000AD9 RID: 2777 RVA: 0x00031EC8 File Offset: 0x00030EC8
		public virtual void PrependChild(XPathNavigator newChild)
		{
			if (newChild == null)
			{
				throw new ArgumentNullException("newChild");
			}
			if (!this.IsValidChildType(newChild.NodeType))
			{
				throw new InvalidOperationException(Res.GetString("Xpn_BadPosition"));
			}
			XmlReader newChild2 = newChild.CreateReader();
			this.PrependChild(newChild2);
		}

		// Token: 0x06000ADA RID: 2778 RVA: 0x00031F10 File Offset: 0x00030F10
		public virtual void InsertBefore(string newSibling)
		{
			XmlReader newSibling2 = this.CreateContextReader(newSibling, false);
			this.InsertBefore(newSibling2);
		}

		// Token: 0x06000ADB RID: 2779 RVA: 0x00031F30 File Offset: 0x00030F30
		public virtual void InsertBefore(XmlReader newSibling)
		{
			if (newSibling == null)
			{
				throw new ArgumentNullException("newSibling");
			}
			XmlWriter xmlWriter = this.InsertBefore();
			this.BuildSubtree(newSibling, xmlWriter);
			xmlWriter.Close();
		}

		// Token: 0x06000ADC RID: 2780 RVA: 0x00031F60 File Offset: 0x00030F60
		public virtual void InsertBefore(XPathNavigator newSibling)
		{
			if (newSibling == null)
			{
				throw new ArgumentNullException("newSibling");
			}
			if (!this.IsValidSiblingType(newSibling.NodeType))
			{
				throw new InvalidOperationException(Res.GetString("Xpn_BadPosition"));
			}
			XmlReader newSibling2 = newSibling.CreateReader();
			this.InsertBefore(newSibling2);
		}

		// Token: 0x06000ADD RID: 2781 RVA: 0x00031FA8 File Offset: 0x00030FA8
		public virtual void InsertAfter(string newSibling)
		{
			XmlReader newSibling2 = this.CreateContextReader(newSibling, false);
			this.InsertAfter(newSibling2);
		}

		// Token: 0x06000ADE RID: 2782 RVA: 0x00031FC8 File Offset: 0x00030FC8
		public virtual void InsertAfter(XmlReader newSibling)
		{
			if (newSibling == null)
			{
				throw new ArgumentNullException("newSibling");
			}
			XmlWriter xmlWriter = this.InsertAfter();
			this.BuildSubtree(newSibling, xmlWriter);
			xmlWriter.Close();
		}

		// Token: 0x06000ADF RID: 2783 RVA: 0x00031FF8 File Offset: 0x00030FF8
		public virtual void InsertAfter(XPathNavigator newSibling)
		{
			if (newSibling == null)
			{
				throw new ArgumentNullException("newSibling");
			}
			if (!this.IsValidSiblingType(newSibling.NodeType))
			{
				throw new InvalidOperationException(Res.GetString("Xpn_BadPosition"));
			}
			XmlReader newSibling2 = newSibling.CreateReader();
			this.InsertAfter(newSibling2);
		}

		// Token: 0x06000AE0 RID: 2784 RVA: 0x0003203F File Offset: 0x0003103F
		public virtual void DeleteRange(XPathNavigator lastSiblingToDelete)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000AE1 RID: 2785 RVA: 0x00032046 File Offset: 0x00031046
		public virtual void DeleteSelf()
		{
			this.DeleteRange(this);
		}

		// Token: 0x06000AE2 RID: 2786 RVA: 0x00032050 File Offset: 0x00031050
		public virtual void PrependChildElement(string prefix, string localName, string namespaceURI, string value)
		{
			XmlWriter xmlWriter = this.PrependChild();
			xmlWriter.WriteStartElement(prefix, localName, namespaceURI);
			if (value != null)
			{
				xmlWriter.WriteString(value);
			}
			xmlWriter.WriteEndElement();
			xmlWriter.Close();
		}

		// Token: 0x06000AE3 RID: 2787 RVA: 0x00032088 File Offset: 0x00031088
		public virtual void AppendChildElement(string prefix, string localName, string namespaceURI, string value)
		{
			XmlWriter xmlWriter = this.AppendChild();
			xmlWriter.WriteStartElement(prefix, localName, namespaceURI);
			if (value != null)
			{
				xmlWriter.WriteString(value);
			}
			xmlWriter.WriteEndElement();
			xmlWriter.Close();
		}

		// Token: 0x06000AE4 RID: 2788 RVA: 0x000320C0 File Offset: 0x000310C0
		public virtual void InsertElementBefore(string prefix, string localName, string namespaceURI, string value)
		{
			XmlWriter xmlWriter = this.InsertBefore();
			xmlWriter.WriteStartElement(prefix, localName, namespaceURI);
			if (value != null)
			{
				xmlWriter.WriteString(value);
			}
			xmlWriter.WriteEndElement();
			xmlWriter.Close();
		}

		// Token: 0x06000AE5 RID: 2789 RVA: 0x000320F8 File Offset: 0x000310F8
		public virtual void InsertElementAfter(string prefix, string localName, string namespaceURI, string value)
		{
			XmlWriter xmlWriter = this.InsertAfter();
			xmlWriter.WriteStartElement(prefix, localName, namespaceURI);
			if (value != null)
			{
				xmlWriter.WriteString(value);
			}
			xmlWriter.WriteEndElement();
			xmlWriter.Close();
		}

		// Token: 0x06000AE6 RID: 2790 RVA: 0x00032130 File Offset: 0x00031130
		public virtual void CreateAttribute(string prefix, string localName, string namespaceURI, string value)
		{
			XmlWriter xmlWriter = this.CreateAttributes();
			xmlWriter.WriteStartAttribute(prefix, localName, namespaceURI);
			if (value != null)
			{
				xmlWriter.WriteString(value);
			}
			xmlWriter.WriteEndAttribute();
			xmlWriter.Close();
		}

		// Token: 0x06000AE7 RID: 2791 RVA: 0x00032168 File Offset: 0x00031168
		internal bool MoveToPrevious(string localName, string namespaceURI)
		{
			XPathNavigator other = this.Clone();
			localName = ((localName != null) ? this.NameTable.Get(localName) : null);
			while (this.MoveToPrevious())
			{
				if (this.NodeType == XPathNodeType.Element && localName == this.LocalName && namespaceURI == this.NamespaceURI)
				{
					return true;
				}
			}
			this.MoveTo(other);
			return false;
		}

		// Token: 0x06000AE8 RID: 2792 RVA: 0x000321C8 File Offset: 0x000311C8
		internal bool MoveToPrevious(XPathNodeType type)
		{
			XPathNavigator other = this.Clone();
			int contentKindMask = XPathNavigator.GetContentKindMask(type);
			while (this.MoveToPrevious())
			{
				if ((1 << (int)this.NodeType & contentKindMask) != 0)
				{
					return true;
				}
			}
			this.MoveTo(other);
			return false;
		}

		// Token: 0x06000AE9 RID: 2793 RVA: 0x00032208 File Offset: 0x00031208
		internal bool MoveToNonDescendant()
		{
			if (this.NodeType == XPathNodeType.Root)
			{
				return false;
			}
			if (this.MoveToNext())
			{
				return true;
			}
			XPathNavigator xpathNavigator = this.Clone();
			if (!this.MoveToParent())
			{
				return false;
			}
			switch (xpathNavigator.NodeType)
			{
			case XPathNodeType.Attribute:
			case XPathNodeType.Namespace:
				if (this.MoveToFirstChild())
				{
					return true;
				}
				break;
			}
			while (!this.MoveToNext())
			{
				if (!this.MoveToParent())
				{
					this.MoveTo(xpathNavigator);
					return false;
				}
			}
			return true;
		}

		// Token: 0x1700025E RID: 606
		// (get) Token: 0x06000AEA RID: 2794 RVA: 0x00032278 File Offset: 0x00031278
		internal uint IndexInParent
		{
			get
			{
				XPathNavigator xpathNavigator = this.Clone();
				uint num = 0U;
				switch (this.NodeType)
				{
				case XPathNodeType.Attribute:
					while (xpathNavigator.MoveToNextAttribute())
					{
						num += 1U;
					}
					break;
				case XPathNodeType.Namespace:
					while (xpathNavigator.MoveToNextNamespace())
					{
						num += 1U;
					}
					break;
				default:
					while (xpathNavigator.MoveToNext())
					{
						num += 1U;
					}
					break;
				}
				return num;
			}
		}

		// Token: 0x1700025F RID: 607
		// (get) Token: 0x06000AEB RID: 2795 RVA: 0x000322D0 File Offset: 0x000312D0
		internal virtual string UniqueId
		{
			get
			{
				XPathNavigator xpathNavigator = this.Clone();
				BufferBuilder bufferBuilder = new BufferBuilder();
				bufferBuilder.Append(XPathNavigator.NodeTypeLetter[(int)this.NodeType]);
				for (;;)
				{
					uint num = xpathNavigator.IndexInParent;
					if (!xpathNavigator.MoveToParent())
					{
						break;
					}
					if (num <= 31U)
					{
						bufferBuilder.Append(XPathNavigator.UniqueIdTbl[(int)((UIntPtr)num)]);
					}
					else
					{
						bufferBuilder.Append('0');
						do
						{
							bufferBuilder.Append(XPathNavigator.UniqueIdTbl[(int)((UIntPtr)(num & 31U))]);
							num >>= 5;
						}
						while (num != 0U);
						bufferBuilder.Append('0');
					}
				}
				return bufferBuilder.ToString();
			}
		}

		// Token: 0x06000AEC RID: 2796 RVA: 0x00032350 File Offset: 0x00031350
		private static XPathExpression CompileMatchPattern(string xpath)
		{
			bool needContext;
			Query query = new QueryBuilder().BuildPatternQuery(xpath, out needContext);
			return new CompiledXpathExpr(query, xpath, needContext);
		}

		// Token: 0x06000AED RID: 2797 RVA: 0x00032374 File Offset: 0x00031374
		private static int GetDepth(XPathNavigator nav)
		{
			int num = 0;
			while (nav.MoveToParent())
			{
				num++;
			}
			return num;
		}

		// Token: 0x06000AEE RID: 2798 RVA: 0x00032394 File Offset: 0x00031394
		private XmlNodeOrder CompareSiblings(XPathNavigator n1, XPathNavigator n2)
		{
			int num = 0;
			switch (n1.NodeType)
			{
			case XPathNodeType.Attribute:
				num++;
				break;
			case XPathNodeType.Namespace:
				break;
			default:
				num += 2;
				break;
			}
			switch (n2.NodeType)
			{
			case XPathNodeType.Attribute:
				num--;
				if (num == 0)
				{
					while (n1.MoveToNextAttribute())
					{
						if (n1.IsSamePosition(n2))
						{
							return XmlNodeOrder.Before;
						}
					}
				}
				break;
			case XPathNodeType.Namespace:
				if (num == 0)
				{
					while (n1.MoveToNextNamespace())
					{
						if (n1.IsSamePosition(n2))
						{
							return XmlNodeOrder.Before;
						}
					}
				}
				break;
			default:
				num -= 2;
				if (num == 0)
				{
					while (n1.MoveToNext())
					{
						if (n1.IsSamePosition(n2))
						{
							return XmlNodeOrder.Before;
						}
					}
				}
				break;
			}
			if (num >= 0)
			{
				return XmlNodeOrder.After;
			}
			return XmlNodeOrder.Before;
		}

		// Token: 0x06000AEF RID: 2799 RVA: 0x0003243C File Offset: 0x0003143C
		internal static XmlNamespaceManager GetNamespaces(IXmlNamespaceResolver resolver)
		{
			XmlNamespaceManager xmlNamespaceManager = new XmlNamespaceManager(new NameTable());
			IDictionary<string, string> namespacesInScope = resolver.GetNamespacesInScope(XmlNamespaceScope.All);
			foreach (KeyValuePair<string, string> keyValuePair in namespacesInScope)
			{
				if (keyValuePair.Key != "xmlns")
				{
					xmlNamespaceManager.AddNamespace(keyValuePair.Key, keyValuePair.Value);
				}
			}
			return xmlNamespaceManager;
		}

		// Token: 0x06000AF0 RID: 2800 RVA: 0x000324B8 File Offset: 0x000314B8
		internal static int GetContentKindMask(XPathNodeType type)
		{
			return XPathNavigator.ContentKindMasks[(int)type];
		}

		// Token: 0x06000AF1 RID: 2801 RVA: 0x000324C1 File Offset: 0x000314C1
		internal static int GetKindMask(XPathNodeType type)
		{
			if (type == XPathNodeType.All)
			{
				return int.MaxValue;
			}
			if (type == XPathNodeType.Text)
			{
				return 112;
			}
			return 1 << (int)type;
		}

		// Token: 0x06000AF2 RID: 2802 RVA: 0x000324DB File Offset: 0x000314DB
		internal static bool IsText(XPathNodeType type)
		{
			return (1 << (int)type & 112) != 0;
		}

		// Token: 0x06000AF3 RID: 2803 RVA: 0x000324EC File Offset: 0x000314EC
		private bool IsValidChildType(XPathNodeType type)
		{
			switch (this.NodeType)
			{
			case XPathNodeType.Root:
				switch (type)
				{
				case XPathNodeType.Element:
				case XPathNodeType.SignificantWhitespace:
				case XPathNodeType.Whitespace:
				case XPathNodeType.ProcessingInstruction:
				case XPathNodeType.Comment:
					return true;
				}
				break;
			case XPathNodeType.Element:
				switch (type)
				{
				case XPathNodeType.Element:
				case XPathNodeType.Text:
				case XPathNodeType.SignificantWhitespace:
				case XPathNodeType.Whitespace:
				case XPathNodeType.ProcessingInstruction:
				case XPathNodeType.Comment:
					return true;
				}
				break;
			}
			return false;
		}

		// Token: 0x06000AF4 RID: 2804 RVA: 0x00032570 File Offset: 0x00031570
		private bool IsValidSiblingType(XPathNodeType type)
		{
			switch (this.NodeType)
			{
			case XPathNodeType.Element:
			case XPathNodeType.Text:
			case XPathNodeType.SignificantWhitespace:
			case XPathNodeType.Whitespace:
			case XPathNodeType.ProcessingInstruction:
			case XPathNodeType.Comment:
				switch (type)
				{
				case XPathNodeType.Element:
				case XPathNodeType.Text:
				case XPathNodeType.SignificantWhitespace:
				case XPathNodeType.Whitespace:
				case XPathNodeType.ProcessingInstruction:
				case XPathNodeType.Comment:
					return true;
				}
				break;
			}
			return false;
		}

		// Token: 0x06000AF5 RID: 2805 RVA: 0x000325DD File Offset: 0x000315DD
		private XmlReader CreateReader()
		{
			return XPathNavigatorReader.Create(this);
		}

		// Token: 0x06000AF6 RID: 2806 RVA: 0x000325E8 File Offset: 0x000315E8
		private XmlReader CreateContextReader(string xml, bool fromCurrentNode)
		{
			if (xml == null)
			{
				throw new ArgumentNullException("xml");
			}
			XPathNavigator xpathNavigator = this.CreateNavigator();
			XmlNamespaceManager xmlNamespaceManager = new XmlNamespaceManager(this.NameTable);
			if (!fromCurrentNode)
			{
				xpathNavigator.MoveToParent();
			}
			if (xpathNavigator.MoveToFirstNamespace(XPathNamespaceScope.All))
			{
				do
				{
					xmlNamespaceManager.AddNamespace(xpathNavigator.LocalName, xpathNavigator.Value);
				}
				while (xpathNavigator.MoveToNextNamespace(XPathNamespaceScope.All));
			}
			XmlParserContext context = new XmlParserContext(this.NameTable, xmlNamespaceManager, null, XmlSpace.Default);
			return new XmlTextReader(xml, XmlNodeType.Element, context)
			{
				WhitespaceHandling = WhitespaceHandling.Significant
			};
		}

		// Token: 0x06000AF7 RID: 2807 RVA: 0x00032664 File Offset: 0x00031664
		internal void BuildSubtree(XmlReader reader, XmlWriter writer)
		{
			string text = "http://www.w3.org/2000/xmlns/";
			ReadState readState = reader.ReadState;
			if (readState != ReadState.Initial && readState != ReadState.Interactive)
			{
				throw new ArgumentException(Res.GetString("Xml_InvalidOperation"), "reader");
			}
			int num = 0;
			if (readState == ReadState.Initial)
			{
				if (!reader.Read())
				{
					return;
				}
				num++;
			}
			do
			{
				switch (reader.NodeType)
				{
				case XmlNodeType.Element:
				{
					writer.WriteStartElement(reader.Prefix, reader.LocalName, reader.NamespaceURI);
					bool isEmptyElement = reader.IsEmptyElement;
					while (reader.MoveToNextAttribute())
					{
						if (reader.NamespaceURI == text)
						{
							if (reader.Prefix.Length == 0)
							{
								writer.WriteAttributeString("", "xmlns", text, reader.Value);
							}
							else
							{
								writer.WriteAttributeString("xmlns", reader.LocalName, text, reader.Value);
							}
						}
						else
						{
							writer.WriteStartAttribute(reader.Prefix, reader.LocalName, reader.NamespaceURI);
							writer.WriteString(reader.Value);
							writer.WriteEndAttribute();
						}
					}
					reader.MoveToElement();
					if (isEmptyElement)
					{
						writer.WriteEndElement();
					}
					else
					{
						num++;
					}
					break;
				}
				case XmlNodeType.Attribute:
					if (reader.NamespaceURI == text)
					{
						if (reader.Prefix.Length == 0)
						{
							writer.WriteAttributeString("", "xmlns", text, reader.Value);
						}
						else
						{
							writer.WriteAttributeString("xmlns", reader.LocalName, text, reader.Value);
						}
					}
					else
					{
						writer.WriteStartAttribute(reader.Prefix, reader.LocalName, reader.NamespaceURI);
						writer.WriteString(reader.Value);
						writer.WriteEndAttribute();
					}
					break;
				case XmlNodeType.Text:
				case XmlNodeType.CDATA:
					writer.WriteString(reader.Value);
					break;
				case XmlNodeType.EntityReference:
					reader.ResolveEntity();
					break;
				case XmlNodeType.ProcessingInstruction:
					writer.WriteProcessingInstruction(reader.LocalName, reader.Value);
					break;
				case XmlNodeType.Comment:
					writer.WriteComment(reader.Value);
					break;
				case XmlNodeType.Whitespace:
				case XmlNodeType.SignificantWhitespace:
					writer.WriteString(reader.Value);
					break;
				case XmlNodeType.EndElement:
					writer.WriteFullEndElement();
					num--;
					break;
				}
			}
			while (reader.Read() && num > 0);
		}

		// Token: 0x17000260 RID: 608
		// (get) Token: 0x06000AF8 RID: 2808 RVA: 0x000328A2 File Offset: 0x000318A2
		private object debuggerDisplayProxy
		{
			get
			{
				return new XPathNavigator.DebuggerDisplayProxy(this);
			}
		}

		// Token: 0x040008CC RID: 2252
		internal const int AllMask = 2147483647;

		// Token: 0x040008CD RID: 2253
		internal const int NoAttrNmspMask = 2147483635;

		// Token: 0x040008CE RID: 2254
		internal const int TextMask = 112;

		// Token: 0x040008CF RID: 2255
		internal static readonly XPathNavigatorKeyComparer comparer = new XPathNavigatorKeyComparer();

		// Token: 0x040008D0 RID: 2256
		internal static readonly char[] NodeTypeLetter = new char[]
		{
			'R',
			'E',
			'A',
			'N',
			'T',
			'S',
			'W',
			'P',
			'C',
			'X'
		};

		// Token: 0x040008D1 RID: 2257
		internal static readonly char[] UniqueIdTbl = new char[]
		{
			'A',
			'B',
			'C',
			'D',
			'E',
			'F',
			'G',
			'H',
			'I',
			'J',
			'K',
			'L',
			'M',
			'N',
			'O',
			'P',
			'Q',
			'R',
			'S',
			'T',
			'U',
			'V',
			'W',
			'X',
			'Y',
			'Z',
			'1',
			'2',
			'3',
			'4',
			'5',
			'6'
		};

		// Token: 0x040008D2 RID: 2258
		internal static readonly int[] ContentKindMasks = new int[]
		{
			1,
			2,
			0,
			0,
			112,
			32,
			64,
			128,
			256,
			2147483635
		};

		// Token: 0x020000BB RID: 187
		private class CheckValidityHelper
		{
			// Token: 0x06000AFB RID: 2811 RVA: 0x00032994 File Offset: 0x00031994
			internal CheckValidityHelper(ValidationEventHandler nextEventHandler, XPathNavigatorReader reader)
			{
				this.isValid = true;
				this.nextEventHandler = nextEventHandler;
				this.reader = reader;
			}

			// Token: 0x06000AFC RID: 2812 RVA: 0x000329B4 File Offset: 0x000319B4
			internal void ValidationCallback(object sender, ValidationEventArgs args)
			{
				if (args.Severity == XmlSeverityType.Error)
				{
					this.isValid = false;
				}
				XmlSchemaValidationException ex = args.Exception as XmlSchemaValidationException;
				if (ex != null && this.reader != null)
				{
					ex.SetSourceObject(this.reader.UnderlyingObject);
				}
				if (this.nextEventHandler != null)
				{
					this.nextEventHandler(sender, args);
					return;
				}
				if (ex != null && args.Severity == XmlSeverityType.Error)
				{
					throw ex;
				}
			}

			// Token: 0x17000261 RID: 609
			// (get) Token: 0x06000AFD RID: 2813 RVA: 0x00032A1B File Offset: 0x00031A1B
			internal bool IsValid
			{
				get
				{
					return this.isValid;
				}
			}

			// Token: 0x040008D3 RID: 2259
			private bool isValid;

			// Token: 0x040008D4 RID: 2260
			private ValidationEventHandler nextEventHandler;

			// Token: 0x040008D5 RID: 2261
			private XPathNavigatorReader reader;
		}

		// Token: 0x020000BC RID: 188
		[DebuggerDisplay("{ToString()}")]
		internal struct DebuggerDisplayProxy
		{
			// Token: 0x06000AFE RID: 2814 RVA: 0x00032A23 File Offset: 0x00031A23
			public DebuggerDisplayProxy(XPathNavigator nav)
			{
				this.nav = nav;
			}

			// Token: 0x06000AFF RID: 2815 RVA: 0x00032A2C File Offset: 0x00031A2C
			public override string ToString()
			{
				string text = this.nav.NodeType.ToString();
				switch (this.nav.NodeType)
				{
				case XPathNodeType.Element:
				{
					object obj = text;
					text = string.Concat(new object[]
					{
						obj,
						", Name=\"",
						this.nav.Name,
						'"'
					});
					break;
				}
				case XPathNodeType.Attribute:
				case XPathNodeType.Namespace:
				case XPathNodeType.ProcessingInstruction:
				{
					object obj2 = text;
					text = string.Concat(new object[]
					{
						obj2,
						", Name=\"",
						this.nav.Name,
						'"'
					});
					object obj3 = text;
					text = string.Concat(new object[]
					{
						obj3,
						", Value=\"",
						XmlConvert.EscapeValueForDebuggerDisplay(this.nav.Value),
						'"'
					});
					break;
				}
				case XPathNodeType.Text:
				case XPathNodeType.SignificantWhitespace:
				case XPathNodeType.Whitespace:
				case XPathNodeType.Comment:
				{
					object obj4 = text;
					text = string.Concat(new object[]
					{
						obj4,
						", Value=\"",
						XmlConvert.EscapeValueForDebuggerDisplay(this.nav.Value),
						'"'
					});
					break;
				}
				}
				return text;
			}

			// Token: 0x040008D6 RID: 2262
			private XPathNavigator nav;
		}
	}
}
