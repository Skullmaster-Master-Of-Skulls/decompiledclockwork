using System;
using System.Collections.Generic;
using System.Xml.Schema;

namespace System.Xml.XPath
{
	// Token: 0x020002EC RID: 748
	internal class XPathNavigatorReader : XmlReader, IXmlNamespaceResolver
	{
		// Token: 0x06002CE6 RID: 11494 RVA: 0x000EAF5A File Offset: 0x000E915A
		internal static XmlNodeType ToXmlNodeType(XPathNodeType typ)
		{
			return XPathNavigatorReader.convertFromXPathNodeType[(int)typ];
		}

		// Token: 0x170009CF RID: 2511
		// (get) Token: 0x06002CE7 RID: 11495 RVA: 0x000EAF63 File Offset: 0x000E9163
		internal object UnderlyingObject
		{
			get
			{
				return this.nav.UnderlyingObject;
			}
		}

		// Token: 0x06002CE8 RID: 11496 RVA: 0x000EAF70 File Offset: 0x000E9170
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

		// Token: 0x06002CE9 RID: 11497 RVA: 0x000EAFA8 File Offset: 0x000E91A8
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

		// Token: 0x170009D0 RID: 2512
		// (get) Token: 0x06002CEA RID: 11498 RVA: 0x000EAFFF File Offset: 0x000E91FF
		protected bool IsReading
		{
			get
			{
				return this.state > XPathNavigatorReader.State.Initial && this.state < XPathNavigatorReader.State.EOF;
			}
		}

		// Token: 0x170009D1 RID: 2513
		// (get) Token: 0x06002CEB RID: 11499 RVA: 0x000EB015 File Offset: 0x000E9215
		internal override XmlNamespaceManager NamespaceManager
		{
			get
			{
				return XPathNavigator.GetNamespaces(this);
			}
		}

		// Token: 0x170009D2 RID: 2514
		// (get) Token: 0x06002CEC RID: 11500 RVA: 0x000EB01D File Offset: 0x000E921D
		public override XmlNameTable NameTable
		{
			get
			{
				return this.navToRead.NameTable;
			}
		}

		// Token: 0x06002CED RID: 11501 RVA: 0x000EB02A File Offset: 0x000E922A
		IDictionary<string, string> IXmlNamespaceResolver.GetNamespacesInScope(XmlNamespaceScope scope)
		{
			return this.nav.GetNamespacesInScope(scope);
		}

		// Token: 0x06002CEE RID: 11502 RVA: 0x000EB038 File Offset: 0x000E9238
		string IXmlNamespaceResolver.LookupNamespace(string prefix)
		{
			return this.nav.LookupNamespace(prefix);
		}

		// Token: 0x06002CEF RID: 11503 RVA: 0x000EB046 File Offset: 0x000E9246
		string IXmlNamespaceResolver.LookupPrefix(string namespaceName)
		{
			return this.nav.LookupPrefix(namespaceName);
		}

		// Token: 0x170009D3 RID: 2515
		// (get) Token: 0x06002CF0 RID: 11504 RVA: 0x000EB054 File Offset: 0x000E9254
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

		// Token: 0x170009D4 RID: 2516
		// (get) Token: 0x06002CF1 RID: 11505 RVA: 0x000EB089 File Offset: 0x000E9289
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

		// Token: 0x170009D5 RID: 2517
		// (get) Token: 0x06002CF2 RID: 11506 RVA: 0x000EB0A1 File Offset: 0x000E92A1
		public override Type ValueType
		{
			get
			{
				return this.nav.ValueType;
			}
		}

		// Token: 0x170009D6 RID: 2518
		// (get) Token: 0x06002CF3 RID: 11507 RVA: 0x000EB0AE File Offset: 0x000E92AE
		public override XmlNodeType NodeType
		{
			get
			{
				return this.nodeType;
			}
		}

		// Token: 0x170009D7 RID: 2519
		// (get) Token: 0x06002CF4 RID: 11508 RVA: 0x000EB0B6 File Offset: 0x000E92B6
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

		// Token: 0x170009D8 RID: 2520
		// (get) Token: 0x06002CF5 RID: 11509 RVA: 0x000EB0F4 File Offset: 0x000E92F4
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

		// Token: 0x170009D9 RID: 2521
		// (get) Token: 0x06002CF6 RID: 11510 RVA: 0x000EB14C File Offset: 0x000E934C
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

		// Token: 0x170009DA RID: 2522
		// (get) Token: 0x06002CF7 RID: 11511 RVA: 0x000EB1A4 File Offset: 0x000E93A4
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

		// Token: 0x170009DB RID: 2523
		// (get) Token: 0x06002CF8 RID: 11512 RVA: 0x000EB1C5 File Offset: 0x000E93C5
		public override bool IsEmptyElement
		{
			get
			{
				return this.nav.IsEmptyElement;
			}
		}

		// Token: 0x170009DC RID: 2524
		// (get) Token: 0x06002CF9 RID: 11513 RVA: 0x000EB1D4 File Offset: 0x000E93D4
		public override XmlSpace XmlSpace
		{
			get
			{
				XPathNavigator xpathNavigator = this.nav.Clone();
				for (;;)
				{
					if (xpathNavigator.MoveToAttribute("space", "http://www.w3.org/XML/1998/namespace"))
					{
						string a = XmlConvert.TrimString(xpathNavigator.Value);
						if (a == "default")
						{
							break;
						}
						if (a == "preserve")
						{
							return XmlSpace.Preserve;
						}
						xpathNavigator.MoveToParent();
					}
					if (!xpathNavigator.MoveToParent())
					{
						return XmlSpace.None;
					}
				}
				return XmlSpace.Default;
			}
		}

		// Token: 0x170009DD RID: 2525
		// (get) Token: 0x06002CFA RID: 11514 RVA: 0x000EB23B File Offset: 0x000E943B
		public override string XmlLang
		{
			get
			{
				return this.nav.XmlLang;
			}
		}

		// Token: 0x170009DE RID: 2526
		// (get) Token: 0x06002CFB RID: 11515 RVA: 0x000EB248 File Offset: 0x000E9448
		public override bool HasValue
		{
			get
			{
				return this.nodeType != XmlNodeType.Element && this.nodeType != XmlNodeType.Document && this.nodeType != XmlNodeType.EndElement && this.nodeType != XmlNodeType.None;
			}
		}

		// Token: 0x170009DF RID: 2527
		// (get) Token: 0x06002CFC RID: 11516 RVA: 0x000EB272 File Offset: 0x000E9472
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

		// Token: 0x06002CFD RID: 11517 RVA: 0x000EB2AC File Offset: 0x000E94AC
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

		// Token: 0x06002CFE RID: 11518 RVA: 0x000EB320 File Offset: 0x000E9520
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

		// Token: 0x06002CFF RID: 11519 RVA: 0x000EB3D7 File Offset: 0x000E95D7
		private void MoveToAttr(XPathNavigator nav, int depth)
		{
			this.nav.MoveTo(nav);
			this.depth = depth;
			this.nodeType = XmlNodeType.Attribute;
			this.state = XPathNavigatorReader.State.Attribute;
		}

		// Token: 0x170009E0 RID: 2528
		// (get) Token: 0x06002D00 RID: 11520 RVA: 0x000EB3FC File Offset: 0x000E95FC
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

		// Token: 0x06002D01 RID: 11521 RVA: 0x000EB45C File Offset: 0x000E965C
		public override string GetAttribute(string name)
		{
			XPathNavigator xpathNavigator = this.nav;
			XPathNodeType xpathNodeType = xpathNavigator.NodeType;
			if (xpathNodeType != XPathNodeType.Element)
			{
				if (xpathNodeType != XPathNodeType.Attribute)
				{
					return null;
				}
				xpathNavigator = xpathNavigator.Clone();
				if (!xpathNavigator.MoveToParent())
				{
					return null;
				}
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
							goto IL_D1;
						}
					}
					return xpathNavigator.Value;
				}
			}
			IL_D1:
			return null;
		}

		// Token: 0x06002D02 RID: 11522 RVA: 0x000EB53C File Offset: 0x000E973C
		public override string GetAttribute(string localName, string namespaceURI)
		{
			if (localName == null)
			{
				throw new ArgumentNullException("localName");
			}
			XPathNavigator xpathNavigator = this.nav;
			XPathNodeType xpathNodeType = xpathNavigator.NodeType;
			if (xpathNodeType != XPathNodeType.Element)
			{
				if (xpathNodeType != XPathNodeType.Attribute)
				{
					return null;
				}
				xpathNavigator = xpathNavigator.Clone();
				if (!xpathNavigator.MoveToParent())
				{
					return null;
				}
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

		// Token: 0x06002D03 RID: 11523 RVA: 0x000EB5D8 File Offset: 0x000E97D8
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

		// Token: 0x06002D04 RID: 11524 RVA: 0x000EB614 File Offset: 0x000E9814
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

		// Token: 0x06002D05 RID: 11525 RVA: 0x000EB67C File Offset: 0x000E987C
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

		// Token: 0x06002D06 RID: 11526 RVA: 0x000EB730 File Offset: 0x000E9930
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

		// Token: 0x06002D07 RID: 11527 RVA: 0x000EB794 File Offset: 0x000E9994
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

		// Token: 0x06002D08 RID: 11528 RVA: 0x000EB8DC File Offset: 0x000E9ADC
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

		// Token: 0x06002D09 RID: 11529 RVA: 0x000EB9CC File Offset: 0x000E9BCC
		public override bool MoveToElement()
		{
			XPathNavigatorReader.State state = this.state;
			if (state - XPathNavigatorReader.State.Attribute > 1)
			{
				if (state == XPathNavigatorReader.State.InReadBinary)
				{
					this.state = this.savedState;
					if (!this.MoveToElement())
					{
						this.state = XPathNavigatorReader.State.InReadBinary;
						return false;
					}
					this.readBinaryHelper.Finish();
				}
				return false;
			}
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
		}

		// Token: 0x170009E1 RID: 2529
		// (get) Token: 0x06002D0A RID: 11530 RVA: 0x000EBA59 File Offset: 0x000E9C59
		public override bool EOF
		{
			get
			{
				return this.state == XPathNavigatorReader.State.EOF;
			}
		}

		// Token: 0x170009E2 RID: 2530
		// (get) Token: 0x06002D0B RID: 11531 RVA: 0x000EBA64 File Offset: 0x000E9C64
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

		// Token: 0x06002D0C RID: 11532 RVA: 0x000EBAA9 File Offset: 0x000E9CA9
		public override void ResolveEntity()
		{
			throw new InvalidOperationException(Res.GetString("Xml_InvalidOperation"));
		}

		// Token: 0x06002D0D RID: 11533 RVA: 0x000EBABC File Offset: 0x000E9CBC
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

		// Token: 0x170009E3 RID: 2531
		// (get) Token: 0x06002D0E RID: 11534 RVA: 0x000EBB11 File Offset: 0x000E9D11
		public override bool CanReadBinaryContent
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06002D0F RID: 11535 RVA: 0x000EBB14 File Offset: 0x000E9D14
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

		// Token: 0x06002D10 RID: 11536 RVA: 0x000EBB84 File Offset: 0x000E9D84
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

		// Token: 0x06002D11 RID: 11537 RVA: 0x000EBBF4 File Offset: 0x000E9DF4
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

		// Token: 0x06002D12 RID: 11538 RVA: 0x000EBC64 File Offset: 0x000E9E64
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

		// Token: 0x06002D13 RID: 11539 RVA: 0x000EBCD2 File Offset: 0x000E9ED2
		public override string LookupNamespace(string prefix)
		{
			return this.nav.LookupNamespace(prefix);
		}

		// Token: 0x170009E4 RID: 2532
		// (get) Token: 0x06002D14 RID: 11540 RVA: 0x000EBCE0 File Offset: 0x000E9EE0
		public override int Depth
		{
			get
			{
				return this.depth;
			}
		}

		// Token: 0x06002D15 RID: 11541 RVA: 0x000EBCE8 File Offset: 0x000E9EE8
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

		// Token: 0x06002D16 RID: 11542 RVA: 0x000EBF04 File Offset: 0x000EA104
		public override void Close()
		{
			this.nav = XmlEmptyNavigator.Singleton;
			this.nodeType = XmlNodeType.None;
			this.state = XPathNavigatorReader.State.Closed;
			this.depth = 0;
		}

		// Token: 0x06002D17 RID: 11543 RVA: 0x000EBF26 File Offset: 0x000EA126
		private void SetEOF()
		{
			this.nav = XmlEmptyNavigator.Singleton;
			this.nodeType = XmlNodeType.None;
			this.state = XPathNavigatorReader.State.EOF;
			this.depth = 0;
		}

		// Token: 0x0400136C RID: 4972
		private XPathNavigator nav;

		// Token: 0x0400136D RID: 4973
		private XPathNavigator navToRead;

		// Token: 0x0400136E RID: 4974
		private int depth;

		// Token: 0x0400136F RID: 4975
		private XPathNavigatorReader.State state;

		// Token: 0x04001370 RID: 4976
		private XmlNodeType nodeType;

		// Token: 0x04001371 RID: 4977
		private int attrCount;

		// Token: 0x04001372 RID: 4978
		private bool readEntireDocument;

		// Token: 0x04001373 RID: 4979
		protected IXmlLineInfo lineInfo;

		// Token: 0x04001374 RID: 4980
		protected IXmlSchemaInfo schemaInfo;

		// Token: 0x04001375 RID: 4981
		private ReadContentAsBinaryHelper readBinaryHelper;

		// Token: 0x04001376 RID: 4982
		private XPathNavigatorReader.State savedState;

		// Token: 0x04001377 RID: 4983
		internal const string space = "space";

		// Token: 0x04001378 RID: 4984
		internal static XmlNodeType[] convertFromXPathNodeType = new XmlNodeType[]
		{
			XmlNodeType.Document,
			XmlNodeType.Element,
			XmlNodeType.Attribute,
			XmlNodeType.Attribute,
			XmlNodeType.Text,
			XmlNodeType.SignificantWhitespace,
			XmlNodeType.Whitespace,
			XmlNodeType.ProcessingInstruction,
			XmlNodeType.Comment,
			XmlNodeType.None
		};

		// Token: 0x020004BE RID: 1214
		private enum State
		{
			// Token: 0x04001F90 RID: 8080
			Initial,
			// Token: 0x04001F91 RID: 8081
			Content,
			// Token: 0x04001F92 RID: 8082
			EndElement,
			// Token: 0x04001F93 RID: 8083
			Attribute,
			// Token: 0x04001F94 RID: 8084
			AttrVal,
			// Token: 0x04001F95 RID: 8085
			InReadBinary,
			// Token: 0x04001F96 RID: 8086
			EOF,
			// Token: 0x04001F97 RID: 8087
			Closed,
			// Token: 0x04001F98 RID: 8088
			Error
		}
	}
}
