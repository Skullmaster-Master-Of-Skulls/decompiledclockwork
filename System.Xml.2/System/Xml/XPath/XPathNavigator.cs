using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Text;
using System.Xml.Schema;
using MS.Internal.Xml.XPath;

namespace System.Xml.XPath
{
	// Token: 0x020002EA RID: 746
	[DebuggerDisplay("{debuggerDisplayProxy}")]
	public abstract class XPathNavigator : XPathItem, ICloneable, IXPathNavigable, IXmlNamespaceResolver
	{
		// Token: 0x06002C59 RID: 11353 RVA: 0x000E917C File Offset: 0x000E737C
		public override string ToString()
		{
			return this.Value;
		}

		// Token: 0x170009B2 RID: 2482
		// (get) Token: 0x06002C5A RID: 11354 RVA: 0x000E9184 File Offset: 0x000E7384
		public sealed override bool IsNode
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170009B3 RID: 2483
		// (get) Token: 0x06002C5B RID: 11355 RVA: 0x000E9188 File Offset: 0x000E7388
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

		// Token: 0x06002C5C RID: 11356 RVA: 0x000E91BC File Offset: 0x000E73BC
		public virtual void SetValue(string value)
		{
			throw new NotSupportedException();
		}

		// Token: 0x170009B4 RID: 2484
		// (get) Token: 0x06002C5D RID: 11357 RVA: 0x000E91C4 File Offset: 0x000E73C4
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

		// Token: 0x06002C5E RID: 11358 RVA: 0x000E925C File Offset: 0x000E745C
		public virtual void SetTypedValue(object typedValue)
		{
			if (typedValue == null)
			{
				throw new ArgumentNullException("typedValue");
			}
			XPathNodeType nodeType = this.NodeType;
			if (nodeType - XPathNodeType.Element > 1)
			{
				throw new InvalidOperationException(Res.GetString("Xpn_BadPosition"));
			}
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
		}

		// Token: 0x170009B5 RID: 2485
		// (get) Token: 0x06002C5F RID: 11359 RVA: 0x000E92EC File Offset: 0x000E74EC
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

		// Token: 0x170009B6 RID: 2486
		// (get) Token: 0x06002C60 RID: 11360 RVA: 0x000E9358 File Offset: 0x000E7558
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

		// Token: 0x170009B7 RID: 2487
		// (get) Token: 0x06002C61 RID: 11361 RVA: 0x000E93E4 File Offset: 0x000E75E4
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

		// Token: 0x170009B8 RID: 2488
		// (get) Token: 0x06002C62 RID: 11362 RVA: 0x000E9470 File Offset: 0x000E7670
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

		// Token: 0x170009B9 RID: 2489
		// (get) Token: 0x06002C63 RID: 11363 RVA: 0x000E94FC File Offset: 0x000E76FC
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

		// Token: 0x170009BA RID: 2490
		// (get) Token: 0x06002C64 RID: 11364 RVA: 0x000E9588 File Offset: 0x000E7788
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

		// Token: 0x06002C65 RID: 11365 RVA: 0x000E9614 File Offset: 0x000E7814
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

		// Token: 0x06002C66 RID: 11366 RVA: 0x000E96A9 File Offset: 0x000E78A9
		object ICloneable.Clone()
		{
			return this.Clone();
		}

		// Token: 0x06002C67 RID: 11367 RVA: 0x000E96B1 File Offset: 0x000E78B1
		public virtual XPathNavigator CreateNavigator()
		{
			return this.Clone();
		}

		// Token: 0x170009BB RID: 2491
		// (get) Token: 0x06002C68 RID: 11368
		public abstract XmlNameTable NameTable { get; }

		// Token: 0x06002C69 RID: 11369 RVA: 0x000E96BC File Offset: 0x000E78BC
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

		// Token: 0x06002C6A RID: 11370 RVA: 0x000E973C File Offset: 0x000E793C
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

		// Token: 0x06002C6B RID: 11371 RVA: 0x000E97D8 File Offset: 0x000E79D8
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

		// Token: 0x170009BC RID: 2492
		// (get) Token: 0x06002C6C RID: 11372 RVA: 0x000E9873 File Offset: 0x000E7A73
		public static IEqualityComparer NavigatorComparer
		{
			get
			{
				return XPathNavigator.comparer;
			}
		}

		// Token: 0x06002C6D RID: 11373
		public abstract XPathNavigator Clone();

		// Token: 0x170009BD RID: 2493
		// (get) Token: 0x06002C6E RID: 11374
		public abstract XPathNodeType NodeType { get; }

		// Token: 0x170009BE RID: 2494
		// (get) Token: 0x06002C6F RID: 11375
		public abstract string LocalName { get; }

		// Token: 0x170009BF RID: 2495
		// (get) Token: 0x06002C70 RID: 11376
		public abstract string Name { get; }

		// Token: 0x170009C0 RID: 2496
		// (get) Token: 0x06002C71 RID: 11377
		public abstract string NamespaceURI { get; }

		// Token: 0x170009C1 RID: 2497
		// (get) Token: 0x06002C72 RID: 11378
		public abstract string Prefix { get; }

		// Token: 0x170009C2 RID: 2498
		// (get) Token: 0x06002C73 RID: 11379
		public abstract string BaseURI { get; }

		// Token: 0x170009C3 RID: 2499
		// (get) Token: 0x06002C74 RID: 11380
		public abstract bool IsEmptyElement { get; }

		// Token: 0x170009C4 RID: 2500
		// (get) Token: 0x06002C75 RID: 11381 RVA: 0x000E987C File Offset: 0x000E7A7C
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

		// Token: 0x06002C76 RID: 11382 RVA: 0x000E98B8 File Offset: 0x000E7AB8
		public virtual XmlReader ReadSubtree()
		{
			XPathNodeType nodeType = this.NodeType;
			if (nodeType > XPathNodeType.Element)
			{
				throw new InvalidOperationException(Res.GetString("Xpn_BadPosition"));
			}
			return this.CreateReader();
		}

		// Token: 0x06002C77 RID: 11383 RVA: 0x000E98E6 File Offset: 0x000E7AE6
		public virtual void WriteSubtree(XmlWriter writer)
		{
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			writer.WriteNode(this, true);
		}

		// Token: 0x170009C5 RID: 2501
		// (get) Token: 0x06002C78 RID: 11384 RVA: 0x000E98FE File Offset: 0x000E7AFE
		public virtual object UnderlyingObject
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170009C6 RID: 2502
		// (get) Token: 0x06002C79 RID: 11385 RVA: 0x000E9901 File Offset: 0x000E7B01
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

		// Token: 0x06002C7A RID: 11386 RVA: 0x000E9918 File Offset: 0x000E7B18
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

		// Token: 0x06002C7B RID: 11387 RVA: 0x000E9944 File Offset: 0x000E7B44
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

		// Token: 0x06002C7C RID: 11388
		public abstract bool MoveToFirstAttribute();

		// Token: 0x06002C7D RID: 11389
		public abstract bool MoveToNextAttribute();

		// Token: 0x06002C7E RID: 11390 RVA: 0x000E997C File Offset: 0x000E7B7C
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

		// Token: 0x06002C7F RID: 11391 RVA: 0x000E99CD File Offset: 0x000E7BCD
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

		// Token: 0x06002C80 RID: 11392
		public abstract bool MoveToFirstNamespace(XPathNamespaceScope namespaceScope);

		// Token: 0x06002C81 RID: 11393
		public abstract bool MoveToNextNamespace(XPathNamespaceScope namespaceScope);

		// Token: 0x06002C82 RID: 11394 RVA: 0x000E99F9 File Offset: 0x000E7BF9
		public bool MoveToFirstNamespace()
		{
			return this.MoveToFirstNamespace(XPathNamespaceScope.All);
		}

		// Token: 0x06002C83 RID: 11395 RVA: 0x000E9A02 File Offset: 0x000E7C02
		public bool MoveToNextNamespace()
		{
			return this.MoveToNextNamespace(XPathNamespaceScope.All);
		}

		// Token: 0x06002C84 RID: 11396
		public abstract bool MoveToNext();

		// Token: 0x06002C85 RID: 11397
		public abstract bool MoveToPrevious();

		// Token: 0x06002C86 RID: 11398 RVA: 0x000E9A0C File Offset: 0x000E7C0C
		public virtual bool MoveToFirst()
		{
			XPathNodeType nodeType = this.NodeType;
			return nodeType - XPathNodeType.Attribute > 1 && this.MoveToParent() && this.MoveToFirstChild();
		}

		// Token: 0x06002C87 RID: 11399
		public abstract bool MoveToFirstChild();

		// Token: 0x06002C88 RID: 11400
		public abstract bool MoveToParent();

		// Token: 0x06002C89 RID: 11401 RVA: 0x000E9A38 File Offset: 0x000E7C38
		public virtual void MoveToRoot()
		{
			while (this.MoveToParent())
			{
			}
		}

		// Token: 0x06002C8A RID: 11402
		public abstract bool MoveTo(XPathNavigator other);

		// Token: 0x06002C8B RID: 11403
		public abstract bool MoveToId(string id);

		// Token: 0x06002C8C RID: 11404 RVA: 0x000E9A44 File Offset: 0x000E7C44
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

		// Token: 0x06002C8D RID: 11405 RVA: 0x000E9A90 File Offset: 0x000E7C90
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

		// Token: 0x06002C8E RID: 11406 RVA: 0x000E9ACD File Offset: 0x000E7CCD
		public virtual bool MoveToFollowing(string localName, string namespaceURI)
		{
			return this.MoveToFollowing(localName, namespaceURI, null);
		}

		// Token: 0x06002C8F RID: 11407 RVA: 0x000E9AD8 File Offset: 0x000E7CD8
		public virtual bool MoveToFollowing(string localName, string namespaceURI, XPathNavigator end)
		{
			XPathNavigator other = this.Clone();
			if (end != null)
			{
				XPathNodeType nodeType = end.NodeType;
				if (nodeType - XPathNodeType.Attribute <= 1)
				{
					end = end.Clone();
					end.MoveToNonDescendant();
				}
			}
			XPathNodeType nodeType2 = this.NodeType;
			if (nodeType2 - XPathNodeType.Attribute <= 1 && !this.MoveToParent())
			{
				return false;
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

		// Token: 0x06002C90 RID: 11408 RVA: 0x000E9B80 File Offset: 0x000E7D80
		public virtual bool MoveToFollowing(XPathNodeType type)
		{
			return this.MoveToFollowing(type, null);
		}

		// Token: 0x06002C91 RID: 11409 RVA: 0x000E9B8C File Offset: 0x000E7D8C
		public virtual bool MoveToFollowing(XPathNodeType type, XPathNavigator end)
		{
			XPathNavigator other = this.Clone();
			int contentKindMask = XPathNavigator.GetContentKindMask(type);
			if (end != null)
			{
				XPathNodeType nodeType = end.NodeType;
				if (nodeType - XPathNodeType.Attribute <= 1)
				{
					end = end.Clone();
					end.MoveToNonDescendant();
				}
			}
			XPathNodeType nodeType2 = this.NodeType;
			if (nodeType2 - XPathNodeType.Attribute <= 1 && !this.MoveToParent())
			{
				return false;
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

		// Token: 0x06002C92 RID: 11410 RVA: 0x000E9C28 File Offset: 0x000E7E28
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

		// Token: 0x06002C93 RID: 11411 RVA: 0x000E9C78 File Offset: 0x000E7E78
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

		// Token: 0x170009C7 RID: 2503
		// (get) Token: 0x06002C94 RID: 11412 RVA: 0x000E9CB7 File Offset: 0x000E7EB7
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

		// Token: 0x06002C95 RID: 11413
		public abstract bool IsSamePosition(XPathNavigator other);

		// Token: 0x06002C96 RID: 11414 RVA: 0x000E9CCB File Offset: 0x000E7ECB
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

		// Token: 0x06002C97 RID: 11415 RVA: 0x000E9CF0 File Offset: 0x000E7EF0
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

		// Token: 0x170009C8 RID: 2504
		// (get) Token: 0x06002C98 RID: 11416 RVA: 0x000E9DD0 File Offset: 0x000E7FD0
		public virtual IXmlSchemaInfo SchemaInfo
		{
			get
			{
				return this as IXmlSchemaInfo;
			}
		}

		// Token: 0x06002C99 RID: 11417 RVA: 0x000E9DD8 File Offset: 0x000E7FD8
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

		// Token: 0x06002C9A RID: 11418 RVA: 0x000E9F00 File Offset: 0x000E8100
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

		// Token: 0x06002C9B RID: 11419 RVA: 0x000E9F66 File Offset: 0x000E8166
		public virtual XPathExpression Compile(string xpath)
		{
			return XPathExpression.Compile(xpath);
		}

		// Token: 0x06002C9C RID: 11420 RVA: 0x000E9F6E File Offset: 0x000E816E
		public virtual XPathNavigator SelectSingleNode(string xpath)
		{
			return this.SelectSingleNode(XPathExpression.Compile(xpath));
		}

		// Token: 0x06002C9D RID: 11421 RVA: 0x000E9F7C File Offset: 0x000E817C
		public virtual XPathNavigator SelectSingleNode(string xpath, IXmlNamespaceResolver resolver)
		{
			return this.SelectSingleNode(XPathExpression.Compile(xpath, resolver));
		}

		// Token: 0x06002C9E RID: 11422 RVA: 0x000E9F8C File Offset: 0x000E818C
		public virtual XPathNavigator SelectSingleNode(XPathExpression expression)
		{
			XPathNodeIterator xpathNodeIterator = this.Select(expression);
			if (xpathNodeIterator.MoveNext())
			{
				return xpathNodeIterator.Current;
			}
			return null;
		}

		// Token: 0x06002C9F RID: 11423 RVA: 0x000E9FB1 File Offset: 0x000E81B1
		public virtual XPathNodeIterator Select(string xpath)
		{
			return this.Select(XPathExpression.Compile(xpath));
		}

		// Token: 0x06002CA0 RID: 11424 RVA: 0x000E9FBF File Offset: 0x000E81BF
		public virtual XPathNodeIterator Select(string xpath, IXmlNamespaceResolver resolver)
		{
			return this.Select(XPathExpression.Compile(xpath, resolver));
		}

		// Token: 0x06002CA1 RID: 11425 RVA: 0x000E9FD0 File Offset: 0x000E81D0
		public virtual XPathNodeIterator Select(XPathExpression expr)
		{
			XPathNodeIterator xpathNodeIterator = this.Evaluate(expr) as XPathNodeIterator;
			if (xpathNodeIterator == null)
			{
				throw XPathException.Create("Xp_NodeSetExpected");
			}
			return xpathNodeIterator;
		}

		// Token: 0x06002CA2 RID: 11426 RVA: 0x000E9FF9 File Offset: 0x000E81F9
		public virtual object Evaluate(string xpath)
		{
			return this.Evaluate(XPathExpression.Compile(xpath), null);
		}

		// Token: 0x06002CA3 RID: 11427 RVA: 0x000EA008 File Offset: 0x000E8208
		public virtual object Evaluate(string xpath, IXmlNamespaceResolver resolver)
		{
			return this.Evaluate(XPathExpression.Compile(xpath, resolver));
		}

		// Token: 0x06002CA4 RID: 11428 RVA: 0x000EA017 File Offset: 0x000E8217
		public virtual object Evaluate(XPathExpression expr)
		{
			return this.Evaluate(expr, null);
		}

		// Token: 0x06002CA5 RID: 11429 RVA: 0x000EA024 File Offset: 0x000E8224
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

		// Token: 0x06002CA6 RID: 11430 RVA: 0x000EA088 File Offset: 0x000E8288
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

		// Token: 0x06002CA7 RID: 11431 RVA: 0x000EA0E8 File Offset: 0x000E82E8
		public virtual bool Matches(string xpath)
		{
			return this.Matches(XPathNavigator.CompileMatchPattern(xpath));
		}

		// Token: 0x06002CA8 RID: 11432 RVA: 0x000EA0F6 File Offset: 0x000E82F6
		public virtual XPathNodeIterator SelectChildren(XPathNodeType type)
		{
			return new XPathChildIterator(this.Clone(), type);
		}

		// Token: 0x06002CA9 RID: 11433 RVA: 0x000EA104 File Offset: 0x000E8304
		public virtual XPathNodeIterator SelectChildren(string name, string namespaceURI)
		{
			return new XPathChildIterator(this.Clone(), name, namespaceURI);
		}

		// Token: 0x06002CAA RID: 11434 RVA: 0x000EA113 File Offset: 0x000E8313
		public virtual XPathNodeIterator SelectAncestors(XPathNodeType type, bool matchSelf)
		{
			return new XPathAncestorIterator(this.Clone(), type, matchSelf);
		}

		// Token: 0x06002CAB RID: 11435 RVA: 0x000EA122 File Offset: 0x000E8322
		public virtual XPathNodeIterator SelectAncestors(string name, string namespaceURI, bool matchSelf)
		{
			return new XPathAncestorIterator(this.Clone(), name, namespaceURI, matchSelf);
		}

		// Token: 0x06002CAC RID: 11436 RVA: 0x000EA132 File Offset: 0x000E8332
		public virtual XPathNodeIterator SelectDescendants(XPathNodeType type, bool matchSelf)
		{
			return new XPathDescendantIterator(this.Clone(), type, matchSelf);
		}

		// Token: 0x06002CAD RID: 11437 RVA: 0x000EA141 File Offset: 0x000E8341
		public virtual XPathNodeIterator SelectDescendants(string name, string namespaceURI, bool matchSelf)
		{
			return new XPathDescendantIterator(this.Clone(), name, namespaceURI, matchSelf);
		}

		// Token: 0x170009C9 RID: 2505
		// (get) Token: 0x06002CAE RID: 11438 RVA: 0x000EA151 File Offset: 0x000E8351
		public virtual bool CanEdit
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06002CAF RID: 11439 RVA: 0x000EA154 File Offset: 0x000E8354
		public virtual XmlWriter PrependChild()
		{
			throw new NotSupportedException();
		}

		// Token: 0x06002CB0 RID: 11440 RVA: 0x000EA15B File Offset: 0x000E835B
		public virtual XmlWriter AppendChild()
		{
			throw new NotSupportedException();
		}

		// Token: 0x06002CB1 RID: 11441 RVA: 0x000EA162 File Offset: 0x000E8362
		public virtual XmlWriter InsertAfter()
		{
			throw new NotSupportedException();
		}

		// Token: 0x06002CB2 RID: 11442 RVA: 0x000EA169 File Offset: 0x000E8369
		public virtual XmlWriter InsertBefore()
		{
			throw new NotSupportedException();
		}

		// Token: 0x06002CB3 RID: 11443 RVA: 0x000EA170 File Offset: 0x000E8370
		public virtual XmlWriter CreateAttributes()
		{
			throw new NotSupportedException();
		}

		// Token: 0x06002CB4 RID: 11444 RVA: 0x000EA177 File Offset: 0x000E8377
		public virtual XmlWriter ReplaceRange(XPathNavigator lastSiblingToReplace)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06002CB5 RID: 11445 RVA: 0x000EA180 File Offset: 0x000E8380
		public virtual void ReplaceSelf(string newNode)
		{
			XmlReader newNode2 = this.CreateContextReader(newNode, false);
			this.ReplaceSelf(newNode2);
		}

		// Token: 0x06002CB6 RID: 11446 RVA: 0x000EA1A0 File Offset: 0x000E83A0
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

		// Token: 0x06002CB7 RID: 11447 RVA: 0x000EA1F4 File Offset: 0x000E83F4
		public virtual void ReplaceSelf(XPathNavigator newNode)
		{
			if (newNode == null)
			{
				throw new ArgumentNullException("newNode");
			}
			XmlReader newNode2 = newNode.CreateReader();
			this.ReplaceSelf(newNode2);
		}

		// Token: 0x170009CA RID: 2506
		// (get) Token: 0x06002CB8 RID: 11448 RVA: 0x000EA220 File Offset: 0x000E8420
		// (set) Token: 0x06002CB9 RID: 11449 RVA: 0x000EA30C File Offset: 0x000E850C
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

		// Token: 0x170009CB RID: 2507
		// (get) Token: 0x06002CBA RID: 11450 RVA: 0x000EA318 File Offset: 0x000E8518
		// (set) Token: 0x06002CBB RID: 11451 RVA: 0x000EA3B4 File Offset: 0x000E85B4
		public virtual string InnerXml
		{
			get
			{
				XPathNodeType nodeType = this.NodeType;
				if (nodeType <= XPathNodeType.Element)
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
				if (nodeType - XPathNodeType.Attribute > 1)
				{
					return string.Empty;
				}
				return this.Value;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				XPathNodeType nodeType = this.NodeType;
				if (nodeType > XPathNodeType.Element)
				{
					if (nodeType != XPathNodeType.Attribute)
					{
						throw new InvalidOperationException(Res.GetString("Xpn_BadPosition"));
					}
					this.SetValue(value);
					return;
				}
				else
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
			}
		}

		// Token: 0x06002CBC RID: 11452 RVA: 0x000EA420 File Offset: 0x000E8620
		public virtual void AppendChild(string newChild)
		{
			XmlReader newChild2 = this.CreateContextReader(newChild, true);
			this.AppendChild(newChild2);
		}

		// Token: 0x06002CBD RID: 11453 RVA: 0x000EA440 File Offset: 0x000E8640
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

		// Token: 0x06002CBE RID: 11454 RVA: 0x000EA470 File Offset: 0x000E8670
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

		// Token: 0x06002CBF RID: 11455 RVA: 0x000EA4B8 File Offset: 0x000E86B8
		public virtual void PrependChild(string newChild)
		{
			XmlReader newChild2 = this.CreateContextReader(newChild, true);
			this.PrependChild(newChild2);
		}

		// Token: 0x06002CC0 RID: 11456 RVA: 0x000EA4D8 File Offset: 0x000E86D8
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

		// Token: 0x06002CC1 RID: 11457 RVA: 0x000EA508 File Offset: 0x000E8708
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

		// Token: 0x06002CC2 RID: 11458 RVA: 0x000EA550 File Offset: 0x000E8750
		public virtual void InsertBefore(string newSibling)
		{
			XmlReader newSibling2 = this.CreateContextReader(newSibling, false);
			this.InsertBefore(newSibling2);
		}

		// Token: 0x06002CC3 RID: 11459 RVA: 0x000EA570 File Offset: 0x000E8770
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

		// Token: 0x06002CC4 RID: 11460 RVA: 0x000EA5A0 File Offset: 0x000E87A0
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

		// Token: 0x06002CC5 RID: 11461 RVA: 0x000EA5E8 File Offset: 0x000E87E8
		public virtual void InsertAfter(string newSibling)
		{
			XmlReader newSibling2 = this.CreateContextReader(newSibling, false);
			this.InsertAfter(newSibling2);
		}

		// Token: 0x06002CC6 RID: 11462 RVA: 0x000EA608 File Offset: 0x000E8808
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

		// Token: 0x06002CC7 RID: 11463 RVA: 0x000EA638 File Offset: 0x000E8838
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

		// Token: 0x06002CC8 RID: 11464 RVA: 0x000EA67F File Offset: 0x000E887F
		public virtual void DeleteRange(XPathNavigator lastSiblingToDelete)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06002CC9 RID: 11465 RVA: 0x000EA686 File Offset: 0x000E8886
		public virtual void DeleteSelf()
		{
			this.DeleteRange(this);
		}

		// Token: 0x06002CCA RID: 11466 RVA: 0x000EA690 File Offset: 0x000E8890
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

		// Token: 0x06002CCB RID: 11467 RVA: 0x000EA6C8 File Offset: 0x000E88C8
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

		// Token: 0x06002CCC RID: 11468 RVA: 0x000EA700 File Offset: 0x000E8900
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

		// Token: 0x06002CCD RID: 11469 RVA: 0x000EA738 File Offset: 0x000E8938
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

		// Token: 0x06002CCE RID: 11470 RVA: 0x000EA770 File Offset: 0x000E8970
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

		// Token: 0x06002CCF RID: 11471 RVA: 0x000EA7A8 File Offset: 0x000E89A8
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

		// Token: 0x06002CD0 RID: 11472 RVA: 0x000EA808 File Offset: 0x000E8A08
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

		// Token: 0x06002CD1 RID: 11473 RVA: 0x000EA848 File Offset: 0x000E8A48
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
			XPathNodeType nodeType = xpathNavigator.NodeType;
			if (nodeType - XPathNodeType.Attribute <= 1 && this.MoveToFirstChild())
			{
				return true;
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

		// Token: 0x170009CC RID: 2508
		// (get) Token: 0x06002CD2 RID: 11474 RVA: 0x000EA8AC File Offset: 0x000E8AAC
		internal uint IndexInParent
		{
			get
			{
				XPathNavigator xpathNavigator = this.Clone();
				uint num = 0U;
				XPathNodeType nodeType = this.NodeType;
				if (nodeType != XPathNodeType.Attribute)
				{
					if (nodeType != XPathNodeType.Namespace)
					{
						while (xpathNavigator.MoveToNext())
						{
							num += 1U;
						}
					}
					else
					{
						while (xpathNavigator.MoveToNextNamespace())
						{
							num += 1U;
						}
					}
				}
				else
				{
					while (xpathNavigator.MoveToNextAttribute())
					{
						num += 1U;
					}
				}
				return num;
			}
		}

		// Token: 0x170009CD RID: 2509
		// (get) Token: 0x06002CD3 RID: 11475 RVA: 0x000EA8FC File Offset: 0x000E8AFC
		internal virtual string UniqueId
		{
			get
			{
				XPathNavigator xpathNavigator = this.Clone();
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Append(XPathNavigator.NodeTypeLetter[(int)this.NodeType]);
				for (;;)
				{
					uint num = xpathNavigator.IndexInParent;
					if (!xpathNavigator.MoveToParent())
					{
						break;
					}
					if (num <= 31U)
					{
						stringBuilder.Append(XPathNavigator.UniqueIdTbl[(int)num]);
					}
					else
					{
						stringBuilder.Append('0');
						do
						{
							stringBuilder.Append(XPathNavigator.UniqueIdTbl[(int)(num & 31U)]);
							num >>= 5;
						}
						while (num != 0U);
						stringBuilder.Append('0');
					}
				}
				return stringBuilder.ToString();
			}
		}

		// Token: 0x06002CD4 RID: 11476 RVA: 0x000EA980 File Offset: 0x000E8B80
		private static XPathExpression CompileMatchPattern(string xpath)
		{
			bool needContext;
			Query query = new QueryBuilder().BuildPatternQuery(xpath, out needContext);
			return new CompiledXpathExpr(query, xpath, needContext);
		}

		// Token: 0x06002CD5 RID: 11477 RVA: 0x000EA9A4 File Offset: 0x000E8BA4
		private static int GetDepth(XPathNavigator nav)
		{
			int num = 0;
			while (nav.MoveToParent())
			{
				num++;
			}
			return num;
		}

		// Token: 0x06002CD6 RID: 11478 RVA: 0x000EA9C4 File Offset: 0x000E8BC4
		private XmlNodeOrder CompareSiblings(XPathNavigator n1, XPathNavigator n2)
		{
			int num = 0;
			XPathNodeType nodeType = n1.NodeType;
			if (nodeType != XPathNodeType.Attribute)
			{
				if (nodeType != XPathNodeType.Namespace)
				{
					num += 2;
				}
			}
			else
			{
				num++;
			}
			XPathNodeType nodeType2 = n2.NodeType;
			if (nodeType2 != XPathNodeType.Attribute)
			{
				if (nodeType2 == XPathNodeType.Namespace)
				{
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
				}
				else
				{
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
				}
			}
			else
			{
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
			}
			if (num >= 0)
			{
				return XmlNodeOrder.After;
			}
			return XmlNodeOrder.Before;
		}

		// Token: 0x06002CD7 RID: 11479 RVA: 0x000EAA58 File Offset: 0x000E8C58
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

		// Token: 0x06002CD8 RID: 11480 RVA: 0x000EAAD4 File Offset: 0x000E8CD4
		internal static int GetContentKindMask(XPathNodeType type)
		{
			return XPathNavigator.ContentKindMasks[(int)type];
		}

		// Token: 0x06002CD9 RID: 11481 RVA: 0x000EAADD File Offset: 0x000E8CDD
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

		// Token: 0x06002CDA RID: 11482 RVA: 0x000EAAF7 File Offset: 0x000E8CF7
		internal static bool IsText(XPathNodeType type)
		{
			return type - XPathNodeType.Text <= 2;
		}

		// Token: 0x06002CDB RID: 11483 RVA: 0x000EAB04 File Offset: 0x000E8D04
		private bool IsValidChildType(XPathNodeType type)
		{
			XPathNodeType nodeType = this.NodeType;
			if (nodeType != XPathNodeType.Root)
			{
				if (nodeType == XPathNodeType.Element)
				{
					if (type == XPathNodeType.Element || type - XPathNodeType.Text <= 4)
					{
						return true;
					}
				}
			}
			else if (type == XPathNodeType.Element || type - XPathNodeType.SignificantWhitespace <= 3)
			{
				return true;
			}
			return false;
		}

		// Token: 0x06002CDC RID: 11484 RVA: 0x000EAB3C File Offset: 0x000E8D3C
		private bool IsValidSiblingType(XPathNodeType type)
		{
			XPathNodeType nodeType = this.NodeType;
			return (nodeType == XPathNodeType.Element || nodeType - XPathNodeType.Text <= 4) && (type == XPathNodeType.Element || type - XPathNodeType.Text <= 4);
		}

		// Token: 0x06002CDD RID: 11485 RVA: 0x000EAB67 File Offset: 0x000E8D67
		private XmlReader CreateReader()
		{
			return XPathNavigatorReader.Create(this);
		}

		// Token: 0x06002CDE RID: 11486 RVA: 0x000EAB70 File Offset: 0x000E8D70
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

		// Token: 0x06002CDF RID: 11487 RVA: 0x000EABEC File Offset: 0x000E8DEC
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

		// Token: 0x170009CE RID: 2510
		// (get) Token: 0x06002CE0 RID: 11488 RVA: 0x000EAE2A File Offset: 0x000E902A
		private object debuggerDisplayProxy
		{
			get
			{
				return new XPathNavigator.DebuggerDisplayProxy(this);
			}
		}

		// Token: 0x04001365 RID: 4965
		internal static readonly XPathNavigatorKeyComparer comparer = new XPathNavigatorKeyComparer();

		// Token: 0x04001366 RID: 4966
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

		// Token: 0x04001367 RID: 4967
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

		// Token: 0x04001368 RID: 4968
		internal const int AllMask = 2147483647;

		// Token: 0x04001369 RID: 4969
		internal const int NoAttrNmspMask = 2147483635;

		// Token: 0x0400136A RID: 4970
		internal const int TextMask = 112;

		// Token: 0x0400136B RID: 4971
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

		// Token: 0x020004BC RID: 1212
		private class CheckValidityHelper
		{
			// Token: 0x060031A5 RID: 12709 RVA: 0x00120D48 File Offset: 0x0011EF48
			internal CheckValidityHelper(ValidationEventHandler nextEventHandler, XPathNavigatorReader reader)
			{
				this.isValid = true;
				this.nextEventHandler = nextEventHandler;
				this.reader = reader;
			}

			// Token: 0x060031A6 RID: 12710 RVA: 0x00120D68 File Offset: 0x0011EF68
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

			// Token: 0x17000A78 RID: 2680
			// (get) Token: 0x060031A7 RID: 12711 RVA: 0x00120DCF File Offset: 0x0011EFCF
			internal bool IsValid
			{
				get
				{
					return this.isValid;
				}
			}

			// Token: 0x04001F8B RID: 8075
			private bool isValid;

			// Token: 0x04001F8C RID: 8076
			private ValidationEventHandler nextEventHandler;

			// Token: 0x04001F8D RID: 8077
			private XPathNavigatorReader reader;
		}

		// Token: 0x020004BD RID: 1213
		[DebuggerDisplay("{ToString()}")]
		internal struct DebuggerDisplayProxy
		{
			// Token: 0x060031A8 RID: 12712 RVA: 0x00120DD7 File Offset: 0x0011EFD7
			public DebuggerDisplayProxy(XPathNavigator nav)
			{
				this.nav = nav;
			}

			// Token: 0x060031A9 RID: 12713 RVA: 0x00120DE0 File Offset: 0x0011EFE0
			public override string ToString()
			{
				string text = this.nav.NodeType.ToString();
				switch (this.nav.NodeType)
				{
				case XPathNodeType.Element:
					text = text + ", Name=\"" + this.nav.Name + "\"";
					break;
				case XPathNodeType.Attribute:
				case XPathNodeType.Namespace:
				case XPathNodeType.ProcessingInstruction:
					text = text + ", Name=\"" + this.nav.Name + "\"";
					text = text + ", Value=\"" + XmlConvert.EscapeValueForDebuggerDisplay(this.nav.Value) + "\"";
					break;
				case XPathNodeType.Text:
				case XPathNodeType.SignificantWhitespace:
				case XPathNodeType.Whitespace:
				case XPathNodeType.Comment:
					text = text + ", Value=\"" + XmlConvert.EscapeValueForDebuggerDisplay(this.nav.Value) + "\"";
					break;
				}
				return text;
			}

			// Token: 0x04001F8E RID: 8078
			private XPathNavigator nav;
		}
	}
}
