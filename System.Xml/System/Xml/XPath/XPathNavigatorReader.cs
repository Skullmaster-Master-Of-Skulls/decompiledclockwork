using System;
using System.Collections.Generic;
using System.Xml.Schema;

namespace System.Xml.XPath
{
	// Token: 0x0200011A RID: 282
	internal class XPathNavigatorReader : XmlReader, IXmlNamespaceResolver
	{
		// Token: 0x060010CE RID: 4302 RVA: 0x0004C2BA File Offset: 0x0004B2BA
		internal static XmlNodeType ToXmlNodeType(XPathNodeType typ)
		{
			return XPathNavigatorReader.convertFromXPathNodeType[(int)typ];
		}

		// Token: 0x17000407 RID: 1031
		// (get) Token: 0x060010CF RID: 4303 RVA: 0x0004C2C3 File Offset: 0x0004B2C3
		internal object UnderlyingObject
		{
			get
			{
				return this.nav.UnderlyingObject;
			}
		}

		// Token: 0x060010D0 RID: 4304 RVA: 0x0004C2D0 File Offset: 0x0004B2D0
		public static XPathNavigatorReader Create(XPathNavigator navToRead)
		{
			XPathNavigator xpathNavigator = navToRead.Clone();
			IXmlLineInfo xli = xpathNavigator as IXmlLineInfo;
			IXmlSchemaInfo xmlSchemaInfo = xpathNavigator as IXmlSchemaInfo;
			if (xmlSchemaInfo == null)
			{
				return new XPathNavigatorReader(xpathNavigator, xli, xmlSchemaInfo);
			}
			return new XPathNavigatorReaderWithSI(xpathNavigator, xli, xmlSchemaInfo);
		}

		// Token: 0x060010D1 RID: 4305 RVA: 0x0004C308 File Offset: 0x0004B308
		protected XPathNavigatorReader(XPathNavigator navToRead, IXmlLineInfo xli, IXmlSchemaInfo xsi)
		{
			this.navToRead = navToRead;
			this.lineInfo = xli;
			this.schemaInfo = xsi;
			this.nav = XmlEmptyNavigator.Singleton;
			this.state = XPathNavigatorReader.State.Initial;
			this.depth = 0;
			this.nodeType = XPathNavigatorReader.ToXmlNodeType(this.nav.NodeType);
		}

		// Token: 0x17000408 RID: 1032
		// (get) Token: 0x060010D2 RID: 4306 RVA: 0x0004C35F File Offset: 0x0004B35F
		protected bool IsReading
		{
			get
			{
				return this.state > XPathNavigatorReader.State.Initial && this.state < XPathNavigatorReader.State.EOF;
			}
		}

		// Token: 0x17000409 RID: 1033
		// (get) Token: 0x060010D3 RID: 4307 RVA: 0x0004C375 File Offset: 0x0004B375
		internal override XmlNamespaceManager NamespaceManager
		{
			get
			{
				return XPathNavigator.GetNamespaces(this);
			}
		}

		// Token: 0x1700040A RID: 1034
		// (get) Token: 0x060010D4 RID: 4308 RVA: 0x0004C37D File Offset: 0x0004B37D
		public override XmlNameTable NameTable
		{
			get
			{
				return this.navToRead.NameTable;
			}
		}

		// Token: 0x060010D5 RID: 4309 RVA: 0x0004C38A File Offset: 0x0004B38A
		IDictionary<string, string> IXmlNamespaceResolver.GetNamespacesInScope(XmlNamespaceScope scope)
		{
			return this.nav.GetNamespacesInScope(scope);
		}

		// Token: 0x060010D6 RID: 4310 RVA: 0x0004C398 File Offset: 0x0004B398
		string IXmlNamespaceResolver.LookupNamespace(string prefix)
		{
			return this.nav.LookupNamespace(prefix);
		}

		// Token: 0x060010D7 RID: 4311 RVA: 0x0004C3A6 File Offset: 0x0004B3A6
		string IXmlNamespaceResolver.LookupPrefix(string namespaceName)
		{
			return this.nav.LookupPrefix(namespaceName);
		}

		// Token: 0x1700040B RID: 1035
		// (get) Token: 0x060010D8 RID: 4312 RVA: 0x0004C3B4 File Offset: 0x0004B3B4
		public override XmlReaderSettings Settings
		{
			get
			{
				return new XmlReaderSettings
				{
					NameTable = this.NameTable,
					ConformanceLevel = ConformanceLevel.Fragment,
					CheckCharacters = false,
					ReadOnly = true
				};
			}
		}

		// Token: 0x1700040C RID: 1036
		// (get) Token: 0x060010D9 RID: 4313 RVA: 0x0004C3E9 File Offset: 0x0004B3E9
		public override IXmlSchemaInfo SchemaInfo
		{
			get
			{
				if (this.nodeType == XmlNodeType.Text)
				{
					return null;
				}
				return this.nav.SchemaInfo;
			}
		}

		// Token: 0x1700040D RID: 1037
		// (get) Token: 0x060010DA RID: 4314 RVA: 0x0004C401 File Offset: 0x0004B401
		public override Type ValueType
		{
			get
			{
				return this.nav.ValueType;
			}
		}

		// Token: 0x1700040E RID: 1038
		// (get) Token: 0x060010DB RID: 4315 RVA: 0x0004C40E File Offset: 0x0004B40E
		public override XmlNodeType NodeType
		{
			get
			{
				return this.nodeType;
			}
		}

		// Token: 0x1700040F RID: 1039
		// (get) Token: 0x060010DC RID: 4316 RVA: 0x0004C416 File Offset: 0x0004B416
		public override string NamespaceURI
		{
			get
			{
				if (this.nav.NodeType == XPathNodeType.Namespace)
				{
					return this.NameTable.Add("http://www.w3.org/2000/xmlns/");
				}
				if (this.NodeType == XmlNodeType.Text)
				{
					return string.Empty;
				}
				return this.nav.NamespaceURI;
			}
		}

		// Token: 0x17000410 RID: 1040
		// (get) Token: 0x060010DD RID: 4317 RVA: 0x0004C454 File Offset: 0x0004B454
		public override string LocalName
		{
			get
			{
				if (this.nav.NodeType == XPathNodeType.Namespace && this.nav.LocalName.Length == 0)
				{
					return this.NameTable.Add("xmlns");
				}
				if (this.NodeType == XmlNodeType.Text)
				{
					return string.Empty;
				}
				return this.nav.LocalName;
			}
		}

		// Token: 0x17000411 RID: 1041
		// (get) Token: 0x060010DE RID: 4318 RVA: 0x0004C4AC File Offset: 0x0004B4AC
		public override string Prefix
		{
			get
			{
				if (this.nav.NodeType == XPathNodeType.Namespace && this.nav.LocalName.Length != 0)
				{
					return this.NameTable.Add("xmlns");
				}
				if (this.NodeType == XmlNodeType.Text)
				{
					return string.Empty;
				}
				return this.nav.Prefix;
			}
		}

		// Token: 0x17000412 RID: 1042
		// (get) Token: 0x060010DF RID: 4319 RVA: 0x0004C504 File Offset: 0x0004B504
		public override string BaseURI
		{
			get
			{
				if (this.state == XPathNavigatorReader.State.Initial)
				{
					return this.navToRead.BaseURI;
				}
				return this.nav.BaseURI;
			}
		}

		// Token: 0x17000413 RID: 1043
		// (get) Token: 0x060010E0 RID: 4320 RVA: 0x0004C525 File Offset: 0x0004B525
		public override bool IsEmptyElement
		{
			get
			{
				return this.nav.IsEmptyElement;
			}
		}

		// Token: 0x17000414 RID: 1044
		// (get) Token: 0x060010E1 RID: 4321 RVA: 0x0004C534 File Offset: 0x0004B534
		public override XmlSpace XmlSpace
		{
			get
			{
				string a = string.Empty;
				XPathNavigator xpathNavigator = this.nav.Clone();
				for (;;)
				{
					if (xpathNavigator.MoveToAttribute("space", "http://www.w3.org/XML/1998/namespace"))
					{
						a = xpathNavigator.Value;
						if (a == "default")
						{
							break;
						}
						if (a == "preserve")
						{
							return XmlSpace.Preserve;
						}
					}
					if (!xpathNavigator.MoveToParent())
					{
						return XmlSpace.None;
					}
				}
				return XmlSpace.Default;
			}
		}

		// Token: 0x17000415 RID: 1045
		// (get) Token: 0x060010E2 RID: 4322 RVA: 0x0004C593 File Offset: 0x0004B593
		public override string XmlLang
		{
			get
			{
				return this.nav.XmlLang;
			}
		}

		// Token: 0x17000416 RID: 1046
		// (get) Token: 0x060010E3 RID: 4323 RVA: 0x0004C5A0 File Offset: 0x0004B5A0
		public override bool HasValue
		{
			get
			{
				return this.nodeType != XmlNodeType.Element && this.nodeType != XmlNodeType.Document && this.nodeType != XmlNodeType.EndElement && this.nodeType != XmlNodeType.None;
			}
		}

		// Token: 0x17000417 RID: 1047
		// (get) Token: 0x060010E4 RID: 4324 RVA: 0x0004C5CA File Offset: 0x0004B5CA
		public override string Value
		{
			get
			{
				if (this.nodeType != XmlNodeType.Element && this.nodeType != XmlNodeType.Document && this.nodeType != XmlNodeType.EndElement && this.nodeType != XmlNodeType.None)
				{
					return this.nav.Value;
				}
				return string.Empty;
			}
		}

		// Token: 0x060010E5 RID: 4325 RVA: 0x0004C604 File Offset: 0x0004B604
		private XPathNavigator GetElemNav()
		{
			switch (this.state)
			{
			case XPathNavigatorReader.State.Content:
				return this.nav.Clone();
			case XPathNavigatorReader.State.Attribute:
			case XPathNavigatorReader.State.AttrVal:
			{
				XPathNavigator xpathNavigator = this.nav.Clone();
				if (xpathNavigator.MoveToParent())
				{
					return xpathNavigator;
				}
				break;
			}
			case XPathNavigatorReader.State.InReadBinary:
			{
				this.state = this.savedState;
				XPathNavigator elemNav = this.GetElemNav();
				this.state = XPathNavigatorReader.State.InReadBinary;
				return elemNav;
			}
			}
			return null;
		}

		// Token: 0x060010E6 RID: 4326 RVA: 0x0004C678 File Offset: 0x0004B678
		private XPathNavigator GetElemNav(out int depth)
		{
			XPathNavigator xpathNavigator = null;
			switch (this.state)
			{
			case XPathNavigatorReader.State.Content:
				if (this.nodeType == XmlNodeType.Element)
				{
					xpathNavigator = this.nav.Clone();
				}
				depth = this.depth;
				return xpathNavigator;
			case XPathNavigatorReader.State.Attribute:
				xpathNavigator = this.nav.Clone();
				xpathNavigator.MoveToParent();
				depth = this.depth - 1;
				return xpathNavigator;
			case XPathNavigatorReader.State.AttrVal:
				xpathNavigator = this.nav.Clone();
				xpathNavigator.MoveToParent();
				depth = this.depth - 2;
				return xpathNavigator;
			case XPathNavigatorReader.State.InReadBinary:
				this.state = this.savedState;
				xpathNavigator = this.GetElemNav(out depth);
				this.state = XPathNavigatorReader.State.InReadBinary;
				return xpathNavigator;
			}
			depth = this.depth;
			return xpathNavigator;
		}

		// Token: 0x060010E7 RID: 4327 RVA: 0x0004C72F File Offset: 0x0004B72F
		private void MoveToAttr(XPathNavigator nav, int depth)
		{
			this.nav.MoveTo(nav);
			this.depth = depth;
			this.nodeType = XmlNodeType.Attribute;
			this.state = XPathNavigatorReader.State.Attribute;
		}

		// Token: 0x17000418 RID: 1048
		// (get) Token: 0x060010E8 RID: 4328 RVA: 0x0004C754 File Offset: 0x0004B754
		public override int AttributeCount
		{
			get
			{
				if (this.attrCount < 0)
				{
					XPathNavigator elemNav = this.GetElemNav();
					int num = 0;
					if (elemNav != null)
					{
						if (elemNav.MoveToFirstNamespace(XPathNamespaceScope.Local))
						{
							do
							{
								num++;
							}
							while (elemNav.MoveToNextNamespace(XPathNamespaceScope.Local));
							elemNav.MoveToParent();
						}
						if (elemNav.MoveToFirstAttribute())
						{
							do
							{
								num++;
							}
							while (elemNav.MoveToNextAttribute());
						}
					}
					this.attrCount = num;
				}
				return this.attrCount;
			}
		}

		// Token: 0x060010E9 RID: 4329 RVA: 0x0004C7B4 File Offset: 0x0004B7B4
		public override string GetAttribute(string name)
		{
			XPathNavigator xpathNavigator = this.nav;
			switch (xpathNavigator.NodeType)
			{
			case XPathNodeType.Element:
				break;
			case XPathNodeType.Attribute:
				xpathNavigator = xpathNavigator.Clone();
				if (!xpathNavigator.MoveToParent())
				{
					return null;
				}
				break;
			default:
				return null;
			}
			string text;
			string text2;
			ValidateNames.SplitQName(name, out text, out text2);
			if (text.Length == 0)
			{
				if (text2 == "xmlns")
				{
					return xpathNavigator.GetNamespace(string.Empty);
				}
				if (xpathNavigator == this.nav)
				{
					xpathNavigator = xpathNavigator.Clone();
				}
				if (xpathNavigator.MoveToAttribute(text2, string.Empty))
				{
					return xpathNavigator.Value;
				}
			}
			else
			{
				if (text == "xmlns")
				{
					return xpathNavigator.GetNamespace(text2);
				}
				if (xpathNavigator == this.nav)
				{
					xpathNavigator = xpathNavigator.Clone();
				}
				if (xpathNavigator.MoveToFirstAttribute())
				{
					while (!(xpathNavigator.LocalName == text2) || !(xpathNavigator.Prefix == text))
					{
						if (!xpathNavigator.MoveToNextAttribute())
						{
							goto IL_DB;
						}
					}
					return xpathNavigator.Value;
				}
			}
			IL_DB:
			return null;
		}

		// Token: 0x060010EA RID: 4330 RVA: 0x0004C8A0 File Offset: 0x0004B8A0
		public override string GetAttribute(string localName, string namespaceURI)
		{
			if (localName == null)
			{
				throw new ArgumentNullException("localName");
			}
			XPathNavigator xpathNavigator = this.nav;
			switch (xpathNavigator.NodeType)
			{
			case XPathNodeType.Element:
				break;
			case XPathNodeType.Attribute:
				xpathNavigator = xpathNavigator.Clone();
				if (!xpathNavigator.MoveToParent())
				{
					return null;
				}
				break;
			default:
				return null;
			}
			if (namespaceURI == "http://www.w3.org/2000/xmlns/")
			{
				if (localName == "xmlns")
				{
					localName = string.Empty;
				}
				return xpathNavigator.GetNamespace(localName);
			}
			if (namespaceURI == null)
			{
				namespaceURI = string.Empty;
			}
			if (xpathNavigator == this.nav)
			{
				xpathNavigator = xpathNavigator.Clone();
			}
			if (xpathNavigator.MoveToAttribute(localName, namespaceURI))
			{
				return xpathNavigator.Value;
			}
			return null;
		}

		// Token: 0x060010EB RID: 4331 RVA: 0x0004C944 File Offset: 0x0004B944
		private static string GetNamespaceByIndex(XPathNavigator nav, int index, out int count)
		{
			string value = nav.Value;
			string result = null;
			if (nav.MoveToNextNamespace(XPathNamespaceScope.Local))
			{
				result = XPathNavigatorReader.GetNamespaceByIndex(nav, index, out count);
			}
			else
			{
				count = 0;
			}
			if (count == index)
			{
				result = value;
			}
			count++;
			return result;
		}

		// Token: 0x060010EC RID: 4332 RVA: 0x0004C980 File Offset: 0x0004B980
		public override string GetAttribute(int index)
		{
			if (index >= 0)
			{
				XPathNavigator elemNav = this.GetElemNav();
				if (elemNav != null)
				{
					if (elemNav.MoveToFirstNamespace(XPathNamespaceScope.Local))
					{
						int num;
						string namespaceByIndex = XPathNavigatorReader.GetNamespaceByIndex(elemNav, index, out num);
						if (namespaceByIndex != null)
						{
							return namespaceByIndex;
						}
						index -= num;
						elemNav.MoveToParent();
					}
					if (elemNav.MoveToFirstAttribute())
					{
						while (index != 0)
						{
							index--;
							if (!elemNav.MoveToNextAttribute())
							{
								goto IL_51;
							}
						}
						return elemNav.Value;
					}
				}
			}
			IL_51:
			throw new ArgumentOutOfRangeException("index");
		}

		// Token: 0x060010ED RID: 4333 RVA: 0x0004C9E8 File Offset: 0x0004B9E8
		public override bool MoveToAttribute(string localName, string namespaceName)
		{
			if (localName == null)
			{
				throw new ArgumentNullException("localName");
			}
			int num = this.depth;
			XPathNavigator elemNav = this.GetElemNav(out num);
			if (elemNav != null)
			{
				if (namespaceName == "http://www.w3.org/2000/xmlns/")
				{
					if (localName == "xmlns")
					{
						localName = string.Empty;
					}
					if (!elemNav.MoveToFirstNamespace(XPathNamespaceScope.Local))
					{
						return false;
					}
					while (!(elemNav.LocalName == localName))
					{
						if (!elemNav.MoveToNextNamespace(XPathNamespaceScope.Local))
						{
							return false;
						}
					}
				}
				else
				{
					if (namespaceName == null)
					{
						namespaceName = string.Empty;
					}
					if (!elemNav.MoveToAttribute(localName, namespaceName))
					{
						return false;
					}
				}
				if (this.state == XPathNavigatorReader.State.InReadBinary)
				{
					this.readBinaryHelper.Finish();
					this.state = this.savedState;
				}
				this.MoveToAttr(elemNav, num + 1);
				return true;
			}
			return false;
		}

		// Token: 0x060010EE RID: 4334 RVA: 0x0004CA9C File Offset: 0x0004BA9C
		public override bool MoveToFirstAttribute()
		{
			int num;
			XPathNavigator elemNav = this.GetElemNav(out num);
			if (elemNav != null)
			{
				if (elemNav.MoveToFirstNamespace(XPathNamespaceScope.Local))
				{
					while (elemNav.MoveToNextNamespace(XPathNamespaceScope.Local))
					{
					}
				}
				else if (!elemNav.MoveToFirstAttribute())
				{
					return false;
				}
				if (this.state == XPathNavigatorReader.State.InReadBinary)
				{
					this.readBinaryHelper.Finish();
					this.state = this.savedState;
				}
				this.MoveToAttr(elemNav, num + 1);
				return true;
			}
			return false;
		}

		// Token: 0x060010EF RID: 4335 RVA: 0x0004CB00 File Offset: 0x0004BB00
		public override bool MoveToNextAttribute()
		{
			switch (this.state)
			{
			case XPathNavigatorReader.State.Content:
				return this.MoveToFirstAttribute();
			case XPathNavigatorReader.State.Attribute:
			{
				if (XPathNodeType.Attribute == this.nav.NodeType)
				{
					return this.nav.MoveToNextAttribute();
				}
				XPathNavigator xpathNavigator = this.nav.Clone();
				if (!xpathNavigator.MoveToParent())
				{
					return false;
				}
				if (!xpathNavigator.MoveToFirstNamespace(XPathNamespaceScope.Local))
				{
					return false;
				}
				if (!xpathNavigator.IsSamePosition(this.nav))
				{
					XPathNavigator xpathNavigator2 = xpathNavigator.Clone();
					while (xpathNavigator.MoveToNextNamespace(XPathNamespaceScope.Local))
					{
						if (xpathNavigator.IsSamePosition(this.nav))
						{
							this.nav.MoveTo(xpathNavigator2);
							return true;
						}
						xpathNavigator2.MoveTo(xpathNavigator);
					}
					return false;
				}
				xpathNavigator.MoveToParent();
				if (!xpathNavigator.MoveToFirstAttribute())
				{
					return false;
				}
				this.nav.MoveTo(xpathNavigator);
				return true;
			}
			case XPathNavigatorReader.State.AttrVal:
				this.depth--;
				this.state = XPathNavigatorReader.State.Attribute;
				if (!this.MoveToNextAttribute())
				{
					this.depth++;
					this.state = XPathNavigatorReader.State.AttrVal;
					return false;
				}
				this.nodeType = XmlNodeType.Attribute;
				return true;
			case XPathNavigatorReader.State.InReadBinary:
				this.state = this.savedState;
				if (!this.MoveToNextAttribute())
				{
					this.state = XPathNavigatorReader.State.InReadBinary;
					return false;
				}
				this.readBinaryHelper.Finish();
				return true;
			}
			return false;
		}

		// Token: 0x060010F0 RID: 4336 RVA: 0x0004CC48 File Offset: 0x0004BC48
		public override bool MoveToAttribute(string name)
		{
			int num;
			XPathNavigator elemNav = this.GetElemNav(out num);
			if (elemNav == null)
			{
				return false;
			}
			string text;
			string empty;
			ValidateNames.SplitQName(name, out text, out empty);
			bool flag;
			if ((flag = (text.Length == 0 && empty == "xmlns")) || text == "xmlns")
			{
				if (flag)
				{
					empty = string.Empty;
				}
				if (elemNav.MoveToFirstNamespace(XPathNamespaceScope.Local))
				{
					while (!(elemNav.LocalName == empty))
					{
						if (!elemNav.MoveToNextNamespace(XPathNamespaceScope.Local))
						{
							return false;
						}
					}
					goto IL_B5;
				}
			}
			else if (text.Length == 0)
			{
				if (elemNav.MoveToAttribute(empty, string.Empty))
				{
					goto IL_B5;
				}
			}
			else if (elemNav.MoveToFirstAttribute())
			{
				while (!(elemNav.LocalName == empty) || !(elemNav.Prefix == text))
				{
					if (!elemNav.MoveToNextAttribute())
					{
						return false;
					}
				}
				goto IL_B5;
			}
			return false;
			IL_B5:
			if (this.state == XPathNavigatorReader.State.InReadBinary)
			{
				this.readBinaryHelper.Finish();
				this.state = this.savedState;
			}
			this.MoveToAttr(elemNav, num + 1);
			return true;
		}

		// Token: 0x060010F1 RID: 4337 RVA: 0x0004CD38 File Offset: 0x0004BD38
		public override bool MoveToElement()
		{
			switch (this.state)
			{
			case XPathNavigatorReader.State.Attribute:
			case XPathNavigatorReader.State.AttrVal:
				if (!this.nav.MoveToParent())
				{
					return false;
				}
				this.depth--;
				if (this.state == XPathNavigatorReader.State.AttrVal)
				{
					this.depth--;
				}
				this.state = XPathNavigatorReader.State.Content;
				this.nodeType = XmlNodeType.Element;
				return true;
			case XPathNavigatorReader.State.InReadBinary:
				this.state = this.savedState;
				if (!this.MoveToElement())
				{
					this.state = XPathNavigatorReader.State.InReadBinary;
					return false;
				}
				this.readBinaryHelper.Finish();
				break;
			}
			return false;
		}

		// Token: 0x17000419 RID: 1049
		// (get) Token: 0x060010F2 RID: 4338 RVA: 0x0004CDCF File Offset: 0x0004BDCF
		public override bool EOF
		{
			get
			{
				return this.state == XPathNavigatorReader.State.EOF;
			}
		}

		// Token: 0x1700041A RID: 1050
		// (get) Token: 0x060010F3 RID: 4339 RVA: 0x0004CDDC File Offset: 0x0004BDDC
		public override ReadState ReadState
		{
			get
			{
				switch (this.state)
				{
				case XPathNavigatorReader.State.Initial:
					return ReadState.Initial;
				case XPathNavigatorReader.State.Content:
				case XPathNavigatorReader.State.EndElement:
				case XPathNavigatorReader.State.Attribute:
				case XPathNavigatorReader.State.AttrVal:
				case XPathNavigatorReader.State.InReadBinary:
					return ReadState.Interactive;
				case XPathNavigatorReader.State.EOF:
					return ReadState.EndOfFile;
				case XPathNavigatorReader.State.Closed:
					return ReadState.Closed;
				default:
					return ReadState.Error;
				}
			}
		}

		// Token: 0x060010F4 RID: 4340 RVA: 0x0004CE21 File Offset: 0x0004BE21
		public override void ResolveEntity()
		{
			throw new InvalidOperationException(Res.GetString("Xml_InvalidOperation"));
		}

		// Token: 0x060010F5 RID: 4341 RVA: 0x0004CE34 File Offset: 0x0004BE34
		public override bool ReadAttributeValue()
		{
			if (this.state == XPathNavigatorReader.State.InReadBinary)
			{
				this.readBinaryHelper.Finish();
				this.state = this.savedState;
			}
			if (this.state == XPathNavigatorReader.State.Attribute)
			{
				this.state = XPathNavigatorReader.State.AttrVal;
				this.nodeType = XmlNodeType.Text;
				this.depth++;
				return true;
			}
			return false;
		}

		// Token: 0x1700041B RID: 1051
		// (get) Token: 0x060010F6 RID: 4342 RVA: 0x0004CE89 File Offset: 0x0004BE89
		public override bool CanReadBinaryContent
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060010F7 RID: 4343 RVA: 0x0004CE8C File Offset: 0x0004BE8C
		public override int ReadContentAsBase64(byte[] buffer, int index, int count)
		{
			if (this.ReadState != ReadState.Interactive)
			{
				return 0;
			}
			if (this.state != XPathNavigatorReader.State.InReadBinary)
			{
				this.readBinaryHelper = ReadContentAsBinaryHelper.CreateOrReset(this.readBinaryHelper, this);
				this.savedState = this.state;
			}
			this.state = this.savedState;
			int result = this.readBinaryHelper.ReadContentAsBase64(buffer, index, count);
			this.savedState = this.state;
			this.state = XPathNavigatorReader.State.InReadBinary;
			return result;
		}

		// Token: 0x060010F8 RID: 4344 RVA: 0x0004CEFC File Offset: 0x0004BEFC
		public override int ReadContentAsBinHex(byte[] buffer, int index, int count)
		{
			if (this.ReadState != ReadState.Interactive)
			{
				return 0;
			}
			if (this.state != XPathNavigatorReader.State.InReadBinary)
			{
				this.readBinaryHelper = ReadContentAsBinaryHelper.CreateOrReset(this.readBinaryHelper, this);
				this.savedState = this.state;
			}
			this.state = this.savedState;
			int result = this.readBinaryHelper.ReadContentAsBinHex(buffer, index, count);
			this.savedState = this.state;
			this.state = XPathNavigatorReader.State.InReadBinary;
			return result;
		}

		// Token: 0x060010F9 RID: 4345 RVA: 0x0004CF6C File Offset: 0x0004BF6C
		public override int ReadElementContentAsBase64(byte[] buffer, int index, int count)
		{
			if (this.ReadState != ReadState.Interactive)
			{
				return 0;
			}
			if (this.state != XPathNavigatorReader.State.InReadBinary)
			{
				this.readBinaryHelper = ReadContentAsBinaryHelper.CreateOrReset(this.readBinaryHelper, this);
				this.savedState = this.state;
			}
			this.state = this.savedState;
			int result = this.readBinaryHelper.ReadElementContentAsBase64(buffer, index, count);
			this.savedState = this.state;
			this.state = XPathNavigatorReader.State.InReadBinary;
			return result;
		}

		// Token: 0x060010FA RID: 4346 RVA: 0x0004CFDC File Offset: 0x0004BFDC
		public override int ReadElementContentAsBinHex(byte[] buffer, int index, int count)
		{
			if (this.ReadState != ReadState.Interactive)
			{
				return 0;
			}
			if (this.state != XPathNavigatorReader.State.InReadBinary)
			{
				this.readBinaryHelper = ReadContentAsBinaryHelper.CreateOrReset(this.readBinaryHelper, this);
				this.savedState = this.state;
			}
			this.state = this.savedState;
			int result = this.readBinaryHelper.ReadElementContentAsBinHex(buffer, index, count);
			this.savedState = this.state;
			this.state = XPathNavigatorReader.State.InReadBinary;
			return result;
		}

		// Token: 0x060010FB RID: 4347 RVA: 0x0004D04A File Offset: 0x0004C04A
		public override string LookupNamespace(string prefix)
		{
			return this.nav.LookupNamespace(prefix);
		}

		// Token: 0x1700041C RID: 1052
		// (get) Token: 0x060010FC RID: 4348 RVA: 0x0004D058 File Offset: 0x0004C058
		public override int Depth
		{
			get
			{
				return this.depth;
			}
		}

		// Token: 0x060010FD RID: 4349 RVA: 0x0004D060 File Offset: 0x0004C060
		public override bool Read()
		{
			this.attrCount = -1;
			switch (this.state)
			{
			case XPathNavigatorReader.State.Initial:
				this.nav = this.navToRead;
				this.state = XPathNavigatorReader.State.Content;
				if (this.nav.NodeType == XPathNodeType.Root)
				{
					if (!this.nav.MoveToFirstChild())
					{
						this.SetEOF();
						return false;
					}
					this.readEntireDocument = true;
				}
				else if (XPathNodeType.Attribute == this.nav.NodeType)
				{
					this.state = XPathNavigatorReader.State.Attribute;
				}
				this.nodeType = XPathNavigatorReader.ToXmlNodeType(this.nav.NodeType);
				return true;
			case XPathNavigatorReader.State.Content:
				break;
			case XPathNavigatorReader.State.EndElement:
				goto IL_114;
			case XPathNavigatorReader.State.Attribute:
			case XPathNavigatorReader.State.AttrVal:
				if (!this.nav.MoveToParent())
				{
					this.SetEOF();
					return false;
				}
				this.nodeType = XPathNavigatorReader.ToXmlNodeType(this.nav.NodeType);
				this.depth--;
				if (this.state == XPathNavigatorReader.State.AttrVal)
				{
					this.depth--;
				}
				break;
			case XPathNavigatorReader.State.InReadBinary:
				this.state = this.savedState;
				this.readBinaryHelper.Finish();
				return this.Read();
			case XPathNavigatorReader.State.EOF:
			case XPathNavigatorReader.State.Closed:
			case XPathNavigatorReader.State.Error:
				return false;
			default:
				return true;
			}
			if (this.nav.MoveToFirstChild())
			{
				this.nodeType = XPathNavigatorReader.ToXmlNodeType(this.nav.NodeType);
				this.depth++;
				this.state = XPathNavigatorReader.State.Content;
				return true;
			}
			if (this.nodeType == XmlNodeType.Element && !this.nav.IsEmptyElement)
			{
				this.nodeType = XmlNodeType.EndElement;
				this.state = XPathNavigatorReader.State.EndElement;
				return true;
			}
			IL_114:
			if (this.depth == 0 && !this.readEntireDocument)
			{
				this.SetEOF();
				return false;
			}
			if (this.nav.MoveToNext())
			{
				this.nodeType = XPathNavigatorReader.ToXmlNodeType(this.nav.NodeType);
				this.state = XPathNavigatorReader.State.Content;
			}
			else
			{
				if (this.depth <= 0 || !this.nav.MoveToParent())
				{
					this.SetEOF();
					return false;
				}
				this.nodeType = XmlNodeType.EndElement;
				this.state = XPathNavigatorReader.State.EndElement;
				this.depth--;
			}
			return true;
		}

		// Token: 0x060010FE RID: 4350 RVA: 0x0004D27C File Offset: 0x0004C27C
		public override void Close()
		{
			this.nav = XmlEmptyNavigator.Singleton;
			this.nodeType = XmlNodeType.None;
			this.state = XPathNavigatorReader.State.Closed;
			this.depth = 0;
		}

		// Token: 0x060010FF RID: 4351 RVA: 0x0004D29E File Offset: 0x0004C29E
		private void SetEOF()
		{
			this.nav = XmlEmptyNavigator.Singleton;
			this.nodeType = XmlNodeType.None;
			this.state = XPathNavigatorReader.State.EOF;
			this.depth = 0;
		}

		// Token: 0x06001100 RID: 4352 RVA: 0x0004D2C0 File Offset: 0x0004C2C0
		// Note: this type is marked as 'beforefieldinit'.
		static XPathNavigatorReader()
		{
			XmlNodeType[] array = new XmlNodeType[10];
			array[0] = XmlNodeType.Document;
			array[1] = XmlNodeType.Element;
			array[2] = XmlNodeType.Attribute;
			array[3] = XmlNodeType.Attribute;
			array[4] = XmlNodeType.Text;
			array[5] = XmlNodeType.SignificantWhitespace;
			array[6] = XmlNodeType.Whitespace;
			array[7] = XmlNodeType.ProcessingInstruction;
			array[8] = XmlNodeType.Comment;
			XPathNavigatorReader.convertFromXPathNodeType = array;
		}

		// Token: 0x04000AF0 RID: 2800
		internal const string space = "space";

		// Token: 0x04000AF1 RID: 2801
		private XPathNavigator nav;

		// Token: 0x04000AF2 RID: 2802
		private XPathNavigator navToRead;

		// Token: 0x04000AF3 RID: 2803
		private int depth;

		// Token: 0x04000AF4 RID: 2804
		private XPathNavigatorReader.State state;

		// Token: 0x04000AF5 RID: 2805
		private XmlNodeType nodeType;

		// Token: 0x04000AF6 RID: 2806
		private int attrCount;

		// Token: 0x04000AF7 RID: 2807
		private bool readEntireDocument;

		// Token: 0x04000AF8 RID: 2808
		protected IXmlLineInfo lineInfo;

		// Token: 0x04000AF9 RID: 2809
		protected IXmlSchemaInfo schemaInfo;

		// Token: 0x04000AFA RID: 2810
		private ReadContentAsBinaryHelper readBinaryHelper;

		// Token: 0x04000AFB RID: 2811
		private XPathNavigatorReader.State savedState;

		// Token: 0x04000AFC RID: 2812
		internal static XmlNodeType[] convertFromXPathNodeType;

		// Token: 0x0200011B RID: 283
		private enum State
		{
			// Token: 0x04000AFE RID: 2814
			Initial,
			// Token: 0x04000AFF RID: 2815
			Content,
			// Token: 0x04000B00 RID: 2816
			EndElement,
			// Token: 0x04000B01 RID: 2817
			Attribute,
			// Token: 0x04000B02 RID: 2818
			AttrVal,
			// Token: 0x04000B03 RID: 2819
			InReadBinary,
			// Token: 0x04000B04 RID: 2820
			EOF,
			// Token: 0x04000B05 RID: 2821
			Closed,
			// Token: 0x04000B06 RID: 2822
			Error
		}
	}
}
