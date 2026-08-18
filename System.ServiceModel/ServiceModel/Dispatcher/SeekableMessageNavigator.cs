using System;
using System.Collections.Generic;
using System.ServiceModel.Channels;
using System.Text;
using System.Xml;
using System.Xml.XPath;

namespace System.ServiceModel.Dispatcher
{
	// Token: 0x0200050C RID: 1292
	internal class SeekableMessageNavigator : SeekableXPathNavigator, INodeCounter
	{
		// Token: 0x060030F0 RID: 12528 RVA: 0x000BB750 File Offset: 0x000B9950
		static SeekableMessageNavigator()
		{
			SeekableMessageNavigator.BlankDom[1].type = XPathNodeType.Root;
			SeekableMessageNavigator.BlankDom[1].firstChild = 2;
			SeekableMessageNavigator.BlankDom[1].prefix = string.Empty;
			SeekableMessageNavigator.BlankDom[1].name = string.Empty;
			SeekableMessageNavigator.BlankDom[1].val = string.Empty;
			SeekableMessageNavigator.BlankDom[2].type = XPathNodeType.Element;
			SeekableMessageNavigator.BlankDom[2].prefix = "s";
			SeekableMessageNavigator.BlankDom[2].name = "Envelope";
			SeekableMessageNavigator.BlankDom[2].parent = 1;
			SeekableMessageNavigator.BlankDom[2].firstChild = 5;
			SeekableMessageNavigator.BlankDom[2].firstNamespace = 3;
			SeekableMessageNavigator.BlankDom[3].type = XPathNodeType.Namespace;
			SeekableMessageNavigator.BlankDom[3].name = "s";
			SeekableMessageNavigator.BlankDom[3].nextSibling = 4;
			SeekableMessageNavigator.BlankDom[3].parent = 2;
			SeekableMessageNavigator.BlankDom[4].type = XPathNodeType.Namespace;
			SeekableMessageNavigator.BlankDom[4].name = "xml";
			SeekableMessageNavigator.BlankDom[4].val = "http://www.w3.org/XML/1998/namespace";
			SeekableMessageNavigator.BlankDom[4].prevSibling = 3;
			SeekableMessageNavigator.BlankDom[4].parent = 1;
			SeekableMessageNavigator.BlankDom[5].type = XPathNodeType.Element;
			SeekableMessageNavigator.BlankDom[5].prefix = "s";
			SeekableMessageNavigator.BlankDom[5].name = "Header";
			SeekableMessageNavigator.BlankDom[5].parent = 2;
			SeekableMessageNavigator.BlankDom[5].firstNamespace = 3;
		}

		// Token: 0x060030F1 RID: 12529 RVA: 0x000BB93C File Offset: 0x000B9B3C
		internal SeekableMessageNavigator(SeekableMessageNavigator nav)
		{
			this.counter = nav.counter;
			this.dom = nav.dom;
			this.location = nav.location;
			this.specialParent = nav.specialParent;
			if (this.specialParent != 0)
			{
				this.nsStack = nav.CloneNSStack();
			}
		}

		// Token: 0x060030F2 RID: 12530 RVA: 0x000BB993 File Offset: 0x000B9B93
		internal SeekableMessageNavigator(Message msg, int countMax, XmlSpace space, bool includeBody, bool atomize)
		{
			this.Init(msg, countMax, space, includeBody, atomize);
		}

		// Token: 0x17000B93 RID: 2963
		// (get) Token: 0x060030F3 RID: 12531 RVA: 0x000BB9A8 File Offset: 0x000B9BA8
		public override string BaseURI
		{
			get
			{
				this.LoadOnDemand();
				string baseUri = this.dom.nodes[this.location].baseUri;
				if (baseUri != null)
				{
					return baseUri;
				}
				return string.Empty;
			}
		}

		// Token: 0x17000B94 RID: 2964
		// (get) Token: 0x060030F4 RID: 12532 RVA: 0x000BB9E4 File Offset: 0x000B9BE4
		// (set) Token: 0x060030F5 RID: 12533 RVA: 0x000BBA0C File Offset: 0x000B9C0C
		public override long CurrentPosition
		{
			get
			{
				long num = (long)this.specialParent;
				num <<= 32;
				return num + (long)this.location;
			}
			set
			{
				SeekableMessageNavigator.Position position = this.dom.DecodePosition(value);
				if (position.parent != 0)
				{
					if (this.nsStack == null)
					{
						this.nsStack = new Stack<string>();
					}
					else
					{
						this.nsStack.Clear();
					}
					for (int num = this.dom.nodes[position.parent].firstNamespace; num != position.elem; num = this.dom.nodes[num].nextSibling)
					{
						if (num == 0)
						{
							throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new QueryProcessingException(QueryProcessingError.InvalidNavigatorPosition, SR.GetString("SeekableMessageNavInvalidPosition")));
						}
						this.nsStack.Push(this.dom.nodes[num].name);
					}
				}
				this.location = position.elem;
				this.specialParent = position.parent;
			}
		}

		// Token: 0x17000B95 RID: 2965
		// (get) Token: 0x060030F6 RID: 12534 RVA: 0x000BBAE6 File Offset: 0x000B9CE6
		public override bool HasAttributes
		{
			get
			{
				this.LoadOnDemand();
				return this.dom.nodes[this.location].firstAttribute != 0;
			}
		}

		// Token: 0x17000B96 RID: 2966
		// (get) Token: 0x060030F7 RID: 12535 RVA: 0x000BBB0C File Offset: 0x000B9D0C
		public override bool HasChildren
		{
			get
			{
				this.LoadOnDemand();
				return this.dom.nodes[this.location].firstChild != 0;
			}
		}

		// Token: 0x17000B97 RID: 2967
		// (get) Token: 0x060030F8 RID: 12536 RVA: 0x000BBB32 File Offset: 0x000B9D32
		public override bool IsEmptyElement
		{
			get
			{
				return this.dom.nodes[this.location].empty;
			}
		}

		// Token: 0x17000B98 RID: 2968
		// (get) Token: 0x060030F9 RID: 12537 RVA: 0x000BBB50 File Offset: 0x000B9D50
		public override string LocalName
		{
			get
			{
				string name = this.dom.nodes[this.location].name;
				if (name != null)
				{
					return name;
				}
				return string.Empty;
			}
		}

		// Token: 0x17000B99 RID: 2969
		// (get) Token: 0x060030FA RID: 12538 RVA: 0x000BBB83 File Offset: 0x000B9D83
		internal Message Message
		{
			get
			{
				return this.dom.message;
			}
		}

		// Token: 0x17000B9A RID: 2970
		// (get) Token: 0x060030FB RID: 12539 RVA: 0x000BBB90 File Offset: 0x000B9D90
		public override string Name
		{
			get
			{
				return this.GetName(this.location);
			}
		}

		// Token: 0x17000B9B RID: 2971
		// (get) Token: 0x060030FC RID: 12540 RVA: 0x000BBBA0 File Offset: 0x000B9DA0
		public override string NamespaceURI
		{
			get
			{
				string ns = this.dom.nodes[this.location].ns;
				if (ns != null)
				{
					return ns;
				}
				return string.Empty;
			}
		}

		// Token: 0x17000B9C RID: 2972
		// (get) Token: 0x060030FD RID: 12541 RVA: 0x000BBBD3 File Offset: 0x000B9DD3
		public override XmlNameTable NameTable
		{
			get
			{
				if (!this.dom.atomize)
				{
					this.dom.Atomize();
				}
				return this.dom.nameTable;
			}
		}

		// Token: 0x17000B9D RID: 2973
		// (get) Token: 0x060030FE RID: 12542 RVA: 0x000BBBF8 File Offset: 0x000B9DF8
		public override XPathNodeType NodeType
		{
			get
			{
				return this.dom.nodes[this.location].type;
			}
		}

		// Token: 0x17000B9E RID: 2974
		// (get) Token: 0x060030FF RID: 12543 RVA: 0x000BBC18 File Offset: 0x000B9E18
		public override string Prefix
		{
			get
			{
				this.LoadOnDemand();
				string prefix = this.dom.nodes[this.location].prefix;
				if (prefix != null)
				{
					return prefix;
				}
				return string.Empty;
			}
		}

		// Token: 0x17000B9F RID: 2975
		// (get) Token: 0x06003100 RID: 12544 RVA: 0x000BBC51 File Offset: 0x000B9E51
		public override string Value
		{
			get
			{
				return this.dom.GetValue(this.location);
			}
		}

		// Token: 0x17000BA0 RID: 2976
		// (get) Token: 0x06003101 RID: 12545 RVA: 0x000BBC64 File Offset: 0x000B9E64
		public override string XmlLang
		{
			get
			{
				this.LoadOnDemand();
				string xmlLang = this.dom.nodes[this.location].xmlLang;
				if (xmlLang != null)
				{
					return xmlLang;
				}
				return string.Empty;
			}
		}

		// Token: 0x06003102 RID: 12546 RVA: 0x000BBC9D File Offset: 0x000B9E9D
		public override XPathNavigator Clone()
		{
			return new SeekableMessageNavigator(this);
		}

		// Token: 0x06003103 RID: 12547 RVA: 0x000BBCA8 File Offset: 0x000B9EA8
		public override XmlNodeOrder ComparePosition(XPathNavigator nav)
		{
			if (nav == null)
			{
				return XmlNodeOrder.Unknown;
			}
			SeekableMessageNavigator seekableMessageNavigator = nav as SeekableMessageNavigator;
			if (seekableMessageNavigator != null)
			{
				return this.ComparePosition(seekableMessageNavigator);
			}
			return XmlNodeOrder.Unknown;
		}

		// Token: 0x06003104 RID: 12548 RVA: 0x000BBCCD File Offset: 0x000B9ECD
		internal XmlNodeOrder ComparePosition(SeekableMessageNavigator nav)
		{
			if (nav == null)
			{
				return XmlNodeOrder.Unknown;
			}
			if (this.dom != nav.dom)
			{
				return XmlNodeOrder.Unknown;
			}
			return this.dom.ComparePosition(this.specialParent, this.location, nav.specialParent, nav.location);
		}

		// Token: 0x06003105 RID: 12549 RVA: 0x000BBD08 File Offset: 0x000B9F08
		public override XmlNodeOrder ComparePosition(long pos1, long pos2)
		{
			SeekableMessageNavigator.Position position = this.dom.DecodePosition(pos1);
			SeekableMessageNavigator.Position position2 = this.dom.DecodePosition(pos2);
			return this.dom.ComparePosition(position.parent, position.elem, position2.parent, position2.elem);
		}

		// Token: 0x06003106 RID: 12550 RVA: 0x000BBD52 File Offset: 0x000B9F52
		public override object Evaluate(string xpath)
		{
			if (!this.dom.atomize)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperCritical(new QueryProcessingException(QueryProcessingError.NotAtomized, SR.GetString("SeekableMessageNavNonAtomized", new object[]
				{
					"Evaluate"
				})));
			}
			return base.Evaluate(xpath);
		}

		// Token: 0x06003107 RID: 12551 RVA: 0x000BBD91 File Offset: 0x000B9F91
		public override object Evaluate(XPathExpression expr)
		{
			if (!this.dom.atomize)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperCritical(new QueryProcessingException(QueryProcessingError.NotAtomized, SR.GetString("SeekableMessageNavNonAtomized", new object[]
				{
					"Evaluate"
				})));
			}
			return base.Evaluate(expr);
		}

		// Token: 0x06003108 RID: 12552 RVA: 0x000BBDD0 File Offset: 0x000B9FD0
		public override object Evaluate(XPathExpression expr, XPathNodeIterator context)
		{
			if (!this.dom.atomize)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperCritical(new QueryProcessingException(QueryProcessingError.NotAtomized, SR.GetString("SeekableMessageNavNonAtomized", new object[]
				{
					"Evaluate"
				})));
			}
			return base.Evaluate(expr, context);
		}

		// Token: 0x06003109 RID: 12553 RVA: 0x000BBE10 File Offset: 0x000BA010
		public override string GetAttribute(string name, string ns)
		{
			if (name == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("name");
			}
			if (ns == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("ns");
			}
			if (this.NodeType != XPathNodeType.Element)
			{
				return string.Empty;
			}
			string result = string.Empty;
			this.Increase();
			this.LoadOnDemand();
			for (int num = this.dom.nodes[this.location].firstAttribute; num != 0; num = this.dom.nodes[num].nextSibling)
			{
				if (string.CompareOrdinal(this.dom.nodes[num].name, name) == 0 && string.CompareOrdinal(this.dom.nodes[num].ns, ns) == 0)
				{
					result = this.dom.nodes[num].val;
					break;
				}
				this.Increase();
			}
			return result;
		}

		// Token: 0x0600310A RID: 12554 RVA: 0x000BBEF8 File Offset: 0x000BA0F8
		public override string GetLocalName(long pos)
		{
			string name = this.dom.nodes[this.dom.DecodePosition(pos).elem].name;
			if (name != null)
			{
				return name;
			}
			return string.Empty;
		}

		// Token: 0x0600310B RID: 12555 RVA: 0x000BBF36 File Offset: 0x000BA136
		public override string GetName(long pos)
		{
			return this.GetName(this.dom.DecodePosition(pos).elem);
		}

		// Token: 0x0600310C RID: 12556 RVA: 0x000BBF50 File Offset: 0x000BA150
		public override string GetNamespace(long pos)
		{
			string ns = this.dom.nodes[this.dom.DecodePosition(pos).elem].ns;
			if (ns != null)
			{
				return ns;
			}
			return string.Empty;
		}

		// Token: 0x0600310D RID: 12557 RVA: 0x000BBF90 File Offset: 0x000BA190
		public override string GetNamespace(string name)
		{
			if (name == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("name");
			}
			if (this.NodeType != XPathNodeType.Element)
			{
				return string.Empty;
			}
			this.Increase();
			this.LoadOnDemand();
			int num = this.dom.nodes[this.location].firstNamespace;
			string result = string.Empty;
			while (num != 0)
			{
				this.Increase();
				if (string.CompareOrdinal(this.dom.nodes[num].name, name) == 0)
				{
					result = this.dom.nodes[num].val;
					break;
				}
				num = this.dom.nodes[num].nextSibling;
			}
			return result;
		}

		// Token: 0x0600310E RID: 12558 RVA: 0x000BC047 File Offset: 0x000BA247
		public override XPathNodeType GetNodeType(long pos)
		{
			return this.dom.nodes[this.dom.DecodePosition(pos).elem].type;
		}

		// Token: 0x0600310F RID: 12559 RVA: 0x000BC070 File Offset: 0x000BA270
		public override string GetValue(long pos)
		{
			string value = this.dom.GetValue(this.dom.DecodePosition(pos).elem);
			if (value != null)
			{
				return value;
			}
			return string.Empty;
		}

		// Token: 0x06003110 RID: 12560 RVA: 0x000BC0A4 File Offset: 0x000BA2A4
		public override bool IsDescendant(XPathNavigator nav)
		{
			if (nav == null)
			{
				return false;
			}
			SeekableMessageNavigator seekableMessageNavigator = nav as SeekableMessageNavigator;
			return seekableMessageNavigator != null && this.IsDescendant(seekableMessageNavigator);
		}

		// Token: 0x06003111 RID: 12561 RVA: 0x000BC0CC File Offset: 0x000BA2CC
		internal bool IsDescendant(SeekableMessageNavigator nav)
		{
			if (nav == null)
			{
				return false;
			}
			if (this.dom != nav.dom)
			{
				return false;
			}
			XPathNodeType type = this.dom.nodes[nav.location].type;
			if (type == XPathNodeType.Namespace || type == XPathNodeType.Attribute)
			{
				return false;
			}
			type = this.dom.nodes[this.location].type;
			if (type == XPathNodeType.Namespace || type == XPathNodeType.Attribute)
			{
				return false;
			}
			int parent = nav.location;
			while (parent != 0)
			{
				this.Increase();
				parent = this.dom.nodes[parent].parent;
				if (parent == this.location)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06003112 RID: 12562 RVA: 0x000BC170 File Offset: 0x000BA370
		public override bool IsSamePosition(XPathNavigator nav)
		{
			if (nav == null)
			{
				return false;
			}
			SeekableMessageNavigator seekableMessageNavigator = nav as SeekableMessageNavigator;
			return seekableMessageNavigator != null && this.IsSamePosition(seekableMessageNavigator);
		}

		// Token: 0x06003113 RID: 12563 RVA: 0x000BC195 File Offset: 0x000BA395
		internal bool IsSamePosition(SeekableMessageNavigator nav)
		{
			return nav != null && (this.dom == nav.dom && this.location == nav.location) && this.specialParent == nav.specialParent;
		}

		// Token: 0x06003114 RID: 12564 RVA: 0x000BC1C8 File Offset: 0x000BA3C8
		public override bool Matches(string xpath)
		{
			if (!this.dom.atomize)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperCritical(new QueryProcessingException(QueryProcessingError.NotAtomized, SR.GetString("SeekableMessageNavNonAtomized", new object[]
				{
					"Matches"
				})));
			}
			return base.Matches(xpath);
		}

		// Token: 0x06003115 RID: 12565 RVA: 0x000BC207 File Offset: 0x000BA407
		public override bool Matches(XPathExpression expr)
		{
			if (!this.dom.atomize)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperCritical(new QueryProcessingException(QueryProcessingError.NotAtomized, SR.GetString("SeekableMessageNavNonAtomized", new object[]
				{
					"Matches"
				})));
			}
			return base.Matches(expr);
		}

		// Token: 0x06003116 RID: 12566 RVA: 0x000BC248 File Offset: 0x000BA448
		public override bool MoveTo(XPathNavigator nav)
		{
			if (nav == null)
			{
				return false;
			}
			SeekableMessageNavigator seekableMessageNavigator = nav as SeekableMessageNavigator;
			return seekableMessageNavigator != null && this.MoveTo(seekableMessageNavigator);
		}

		// Token: 0x06003117 RID: 12567 RVA: 0x000BC270 File Offset: 0x000BA470
		internal bool MoveTo(SeekableMessageNavigator nav)
		{
			if (nav == null)
			{
				return false;
			}
			this.dom = nav.dom;
			this.counter = nav.counter;
			this.location = nav.location;
			this.specialParent = nav.specialParent;
			if (this.specialParent != 0)
			{
				this.nsStack = nav.CloneNSStack();
			}
			return true;
		}

		// Token: 0x06003118 RID: 12568 RVA: 0x000BC2C8 File Offset: 0x000BA4C8
		public override bool MoveToAttribute(string localName, string namespaceURI)
		{
			if (localName == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("localName");
			}
			if (namespaceURI == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("namespaceURI");
			}
			this.LoadOnDemand();
			if (this.dom.nodes[this.location].type != XPathNodeType.Element)
			{
				return false;
			}
			this.Increase();
			for (int num = this.dom.nodes[this.location].firstAttribute; num != 0; num = this.dom.nodes[num].nextSibling)
			{
				if (string.CompareOrdinal(this.dom.nodes[num].name, localName) == 0 && string.CompareOrdinal(this.dom.nodes[num].ns, namespaceURI) == 0)
				{
					this.location = num;
					return true;
				}
				this.Increase();
			}
			return false;
		}

		// Token: 0x06003119 RID: 12569 RVA: 0x000BC3AC File Offset: 0x000BA5AC
		public override bool MoveToFirst()
		{
			XPathNodeType type = this.dom.nodes[this.location].type;
			if (type != XPathNodeType.Attribute && type != XPathNodeType.Namespace)
			{
				this.Increase();
				int parent = this.dom.nodes[this.location].parent;
				if (parent != 0)
				{
					this.Increase();
					this.location = this.dom.nodes[parent].firstChild;
				}
				return true;
			}
			return false;
		}

		// Token: 0x0600311A RID: 12570 RVA: 0x000BC428 File Offset: 0x000BA628
		public override bool MoveToFirstAttribute()
		{
			if (this.dom.nodes[this.location].type != XPathNodeType.Element)
			{
				return false;
			}
			this.LoadOnDemand();
			int firstAttribute = this.dom.nodes[this.location].firstAttribute;
			if (firstAttribute != 0)
			{
				this.Increase();
				this.location = firstAttribute;
				return true;
			}
			return false;
		}

		// Token: 0x0600311B RID: 12571 RVA: 0x000BC48C File Offset: 0x000BA68C
		public override bool MoveToFirstChild()
		{
			if (this.location == 1 || this.dom.nodes[this.location].type == XPathNodeType.Element)
			{
				this.LoadOnDemand();
				int firstChild = this.dom.nodes[this.location].firstChild;
				if (firstChild != 0)
				{
					this.Increase();
					this.location = firstChild;
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600311C RID: 12572 RVA: 0x000BC4F8 File Offset: 0x000BA6F8
		public override bool MoveToFirstNamespace(XPathNamespaceScope scope)
		{
			if (this.dom.nodes[this.location].type != XPathNodeType.Element)
			{
				return false;
			}
			if (this.nsStack == null)
			{
				this.nsStack = new Stack<string>();
			}
			else
			{
				this.nsStack.Clear();
			}
			this.LoadOnDemand();
			int num = this.FindNamespace(this.location, this.dom.nodes[this.location].firstNamespace, scope);
			if (num != 0)
			{
				this.specialParent = this.location;
				this.Increase();
				this.location = num;
				return true;
			}
			return false;
		}

		// Token: 0x0600311D RID: 12573 RVA: 0x000BC593 File Offset: 0x000BA793
		public override bool MoveToId(string id)
		{
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new QueryProcessingException(QueryProcessingError.NotSupported, SR.GetString("SeekableMessageNavIDNotSupported")));
		}

		// Token: 0x0600311E RID: 12574 RVA: 0x000BC5B0 File Offset: 0x000BA7B0
		public override bool MoveToNamespace(string name)
		{
			if (name == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("name");
			}
			if (this.dom.nodes[this.location].type != XPathNodeType.Element)
			{
				return false;
			}
			if (this.nsStack == null)
			{
				this.nsStack = new Stack<string>();
			}
			else
			{
				this.nsStack.Clear();
			}
			this.Increase();
			this.LoadOnDemand();
			int num = this.dom.nodes[this.location].firstNamespace;
			int num2 = 0;
			while (num != 0)
			{
				string name2 = this.dom.nodes[num].name;
				if (!this.nsStack.Contains(name2))
				{
					this.nsStack.Push(name2);
					num2++;
					string val = this.dom.nodes[num].val;
					if ((name2.Length > 0 || val.Length > 0) && string.CompareOrdinal(name2, name) == 0)
					{
						this.specialParent = this.location;
						this.location = num;
						return true;
					}
				}
				this.Increase();
				num = this.dom.nodes[num].nextSibling;
			}
			for (int i = 0; i < num2; i++)
			{
				this.nsStack.Pop();
			}
			return false;
		}

		// Token: 0x0600311F RID: 12575 RVA: 0x000BC6FC File Offset: 0x000BA8FC
		public override bool MoveToNext()
		{
			XPathNodeType type = this.dom.nodes[this.location].type;
			if (type == XPathNodeType.Attribute || type == XPathNodeType.Namespace)
			{
				return false;
			}
			int nextSibling = this.dom.nodes[this.location].nextSibling;
			if (nextSibling != 0)
			{
				this.Increase();
				this.location = nextSibling;
				return true;
			}
			return false;
		}

		// Token: 0x06003120 RID: 12576 RVA: 0x000BC760 File Offset: 0x000BA960
		public override bool MoveToNextAttribute()
		{
			if (this.dom.nodes[this.location].type != XPathNodeType.Attribute)
			{
				return false;
			}
			int nextSibling = this.dom.nodes[this.location].nextSibling;
			if (nextSibling != 0)
			{
				this.Increase();
				this.location = nextSibling;
				return true;
			}
			return false;
		}

		// Token: 0x06003121 RID: 12577 RVA: 0x000BC7BC File Offset: 0x000BA9BC
		public override bool MoveToNextNamespace(XPathNamespaceScope scope)
		{
			if (this.dom.nodes[this.location].type != XPathNodeType.Namespace)
			{
				return false;
			}
			int num = this.FindNamespace(this.specialParent, this.dom.nodes[this.location].nextSibling, scope);
			if (num != 0)
			{
				this.Increase();
				this.location = num;
				return true;
			}
			return false;
		}

		// Token: 0x06003122 RID: 12578 RVA: 0x000BC828 File Offset: 0x000BAA28
		public override bool MoveToParent()
		{
			if (this.location == 1)
			{
				return false;
			}
			this.Increase();
			if (this.specialParent != 0)
			{
				this.Increase();
				this.location = this.specialParent;
				this.specialParent = 0;
			}
			else
			{
				this.location = this.dom.nodes[this.location].parent;
			}
			return true;
		}

		// Token: 0x06003123 RID: 12579 RVA: 0x000BC88C File Offset: 0x000BAA8C
		public override bool MoveToPrevious()
		{
			int num = 0;
			XPathNodeType type = this.dom.nodes[this.location].type;
			if (type != XPathNodeType.Attribute && type != XPathNodeType.Namespace)
			{
				num = this.dom.nodes[this.location].prevSibling;
			}
			if (num != 0)
			{
				this.Increase();
				this.location = num;
				return true;
			}
			return false;
		}

		// Token: 0x06003124 RID: 12580 RVA: 0x000BC8EE File Offset: 0x000BAAEE
		public override void MoveToRoot()
		{
			this.Increase();
			this.location = 1;
			this.specialParent = 0;
		}

		// Token: 0x06003125 RID: 12581 RVA: 0x000BC904 File Offset: 0x000BAB04
		public override XPathNodeIterator Select(string xpath)
		{
			if (!this.dom.atomize)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperCritical(new QueryProcessingException(QueryProcessingError.NotAtomized, SR.GetString("SeekableMessageNavNonAtomized", new object[]
				{
					"Select"
				})));
			}
			return base.Select(xpath);
		}

		// Token: 0x06003126 RID: 12582 RVA: 0x000BC943 File Offset: 0x000BAB43
		public override XPathNodeIterator Select(XPathExpression xpath)
		{
			if (!this.dom.atomize)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperCritical(new QueryProcessingException(QueryProcessingError.NotAtomized, SR.GetString("SeekableMessageNavNonAtomized", new object[]
				{
					"Select"
				})));
			}
			return base.Select(xpath);
		}

		// Token: 0x06003127 RID: 12583 RVA: 0x000BC982 File Offset: 0x000BAB82
		public override XPathNodeIterator SelectAncestors(XPathNodeType type, bool matchSelf)
		{
			if (!this.dom.atomize)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperCritical(new QueryProcessingException(QueryProcessingError.NotAtomized, SR.GetString("SeekableMessageNavNonAtomized", new object[]
				{
					"SelectAncestors"
				})));
			}
			return base.SelectAncestors(type, matchSelf);
		}

		// Token: 0x06003128 RID: 12584 RVA: 0x000BC9C4 File Offset: 0x000BABC4
		public override XPathNodeIterator SelectAncestors(string name, string namespaceURI, bool matchSelf)
		{
			if (!this.dom.atomize)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperCritical(new QueryProcessingException(QueryProcessingError.NotAtomized, SR.GetString("SeekableMessageNavNonAtomized", new object[]
				{
					"SelectAncestors"
				})));
			}
			return base.SelectAncestors(name, namespaceURI, matchSelf);
		}

		// Token: 0x06003129 RID: 12585 RVA: 0x000BCA10 File Offset: 0x000BAC10
		public override XPathNodeIterator SelectChildren(XPathNodeType type)
		{
			if (!this.dom.atomize)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperCritical(new QueryProcessingException(QueryProcessingError.NotAtomized, SR.GetString("SeekableMessageNavNonAtomized", new object[]
				{
					"SelectChildren"
				})));
			}
			return base.SelectChildren(type);
		}

		// Token: 0x0600312A RID: 12586 RVA: 0x000BCA4F File Offset: 0x000BAC4F
		public override XPathNodeIterator SelectChildren(string name, string namespaceURI)
		{
			if (!this.dom.atomize)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperCritical(new QueryProcessingException(QueryProcessingError.NotAtomized, SR.GetString("SeekableMessageNavNonAtomized", new object[]
				{
					"SelectChildren"
				})));
			}
			return base.SelectChildren(name, namespaceURI);
		}

		// Token: 0x0600312B RID: 12587 RVA: 0x000BCA8F File Offset: 0x000BAC8F
		public override XPathNodeIterator SelectDescendants(XPathNodeType type, bool matchSelf)
		{
			if (!this.dom.atomize)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperCritical(new QueryProcessingException(QueryProcessingError.NotAtomized, SR.GetString("SeekableMessageNavNonAtomized", new object[]
				{
					"SelectDescendants"
				})));
			}
			return base.SelectDescendants(type, matchSelf);
		}

		// Token: 0x0600312C RID: 12588 RVA: 0x000BCAD0 File Offset: 0x000BACD0
		public override XPathNodeIterator SelectDescendants(string name, string namespaceURI, bool matchSelf)
		{
			if (!this.dom.atomize)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperCritical(new QueryProcessingException(QueryProcessingError.NotAtomized, SR.GetString("SeekableMessageNavNonAtomized", new object[]
				{
					"SelectDescendants"
				})));
			}
			return base.SelectDescendants(name, namespaceURI, matchSelf);
		}

		// Token: 0x0600312D RID: 12589 RVA: 0x000BCB1C File Offset: 0x000BAD1C
		internal void Atomize()
		{
			if (!this.dom.atomize)
			{
				this.dom.atomize = true;
				this.dom.nameTable = new NameTable();
				this.dom.nameTable.Add(string.Empty);
				this.dom.Atomize(1, this.nextFreeIndex);
			}
		}

		// Token: 0x0600312E RID: 12590 RVA: 0x000BCB7A File Offset: 0x000BAD7A
		internal void ForkNodeCount(int count)
		{
			this.nodeCount = count;
			this.nodeCountMax = count;
			this.counter = this;
		}

		// Token: 0x0600312F RID: 12591 RVA: 0x000BCB94 File Offset: 0x000BAD94
		internal void Init(Message msg, int countMax, XmlSpace space, bool includeBody, bool atomize)
		{
			this.counter = this;
			this.nodeCount = countMax;
			this.nodeCountMax = countMax;
			this.dom = this;
			this.location = 1;
			this.specialParent = 0;
			this.includeBody = includeBody;
			this.message = msg;
			this.headers = msg.Headers;
			this.space = space;
			this.atomize = false;
			int num = msg.Headers.Count + 6 + 1;
			if (this.nodes == null || this.nodes.Length < num)
			{
				this.nodes = new SeekableMessageNavigator.Node[num + 50];
			}
			else
			{
				Array.Clear(this.nodes, 1, this.nextFreeIndex - 1);
			}
			this.bodyIndex = num - 1;
			this.nextFreeIndex = num;
			Array.Copy(SeekableMessageNavigator.BlankDom, this.nodes, 6);
			string @namespace = msg.Version.Envelope.Namespace;
			this.nodes[2].ns = @namespace;
			this.nodes[3].val = @namespace;
			this.nodes[5].ns = @namespace;
			this.nodes[5].nextSibling = this.bodyIndex;
			this.nodes[5].firstChild = ((this.bodyIndex != 6) ? 6 : 0);
			if (msg.Headers.Count > 0)
			{
				int num2 = 6;
				for (int i = 0; i < msg.Headers.Count; i++)
				{
					this.nodes[num2].type = XPathNodeType.Element;
					this.nodes[num2].parent = 5;
					this.nodes[num2].nextSibling = num2 + 1;
					this.nodes[num2].prevSibling = num2 - 1;
					MessageHeaderInfo messageHeaderInfo = msg.Headers[i];
					this.nodes[num2].ns = messageHeaderInfo.Namespace;
					this.nodes[num2].name = messageHeaderInfo.Name;
					this.nodes[num2].firstChild = -1;
					num2++;
				}
				this.nodes[6].prevSibling = 0;
				this.nodes[this.bodyIndex - 1].nextSibling = 0;
			}
			this.nodes[this.bodyIndex].type = XPathNodeType.Element;
			this.nodes[this.bodyIndex].prefix = "s";
			this.nodes[this.bodyIndex].ns = @namespace;
			this.nodes[this.bodyIndex].name = "Body";
			this.nodes[this.bodyIndex].parent = 2;
			this.nodes[this.bodyIndex].prevSibling = 5;
			this.nodes[this.bodyIndex].firstNamespace = 3;
			this.nodes[this.bodyIndex].firstChild = -1;
			if (atomize)
			{
				this.Atomize();
			}
		}

		// Token: 0x06003130 RID: 12592 RVA: 0x000BCEA0 File Offset: 0x000BB0A0
		private void AddAttribute(int node, int attr)
		{
			this.nodes[attr].parent = node;
			this.nodes[attr].nextSibling = this.nodes[node].firstAttribute;
			this.nodes[node].firstAttribute = attr;
		}

		// Token: 0x06003131 RID: 12593 RVA: 0x000BCEF4 File Offset: 0x000BB0F4
		private void AddChild(int parent, int child)
		{
			if (this.nodes[parent].firstChild == 0)
			{
				this.nodes[parent].firstChild = child;
				this.nodes[child].parent = parent;
				return;
			}
			this.AddSibling(this.nodes[parent].firstChild, child);
		}

		// Token: 0x06003132 RID: 12594 RVA: 0x000BCF54 File Offset: 0x000BB154
		private void AddNamespace(int node, int ns)
		{
			this.nodes[ns].parent = node;
			this.nodes[ns].nextSibling = this.nodes[node].firstNamespace;
			this.nodes[node].firstNamespace = ns;
		}

		// Token: 0x06003133 RID: 12595 RVA: 0x000BCFA8 File Offset: 0x000BB1A8
		private void AddSibling(int node1, int node2)
		{
			int num = this.LastSibling(node1);
			this.nodes[num].nextSibling = node2;
			this.nodes[node2].prevSibling = num;
			this.nodes[node2].parent = this.nodes[num].parent;
		}

		// Token: 0x06003134 RID: 12596 RVA: 0x000BD004 File Offset: 0x000BB204
		private void Atomize(int first, int bound)
		{
			while (first < bound)
			{
				string text = this.nodes[first].prefix;
				if (text != null)
				{
					this.nodes[first].prefix = this.nameTable.Add(text);
				}
				text = this.nodes[first].name;
				if (text != null)
				{
					this.nodes[first].name = this.nameTable.Add(text);
				}
				text = this.nodes[first].ns;
				if (text != null)
				{
					this.nodes[first].ns = this.nameTable.Add(text);
				}
				first++;
			}
		}

		// Token: 0x06003135 RID: 12597 RVA: 0x000BD0B8 File Offset: 0x000BB2B8
		private Stack<string> CloneNSStack()
		{
			Stack<string> stack = new Stack<string>();
			foreach (string item in this.nsStack)
			{
				stack.Push(item);
			}
			return stack;
		}

		// Token: 0x06003136 RID: 12598 RVA: 0x000BD114 File Offset: 0x000BB314
		private XmlNodeOrder CompareLocation(int loc1, int loc2)
		{
			if (loc1 == loc2)
			{
				return XmlNodeOrder.Same;
			}
			if (loc1 < loc2)
			{
				return XmlNodeOrder.Before;
			}
			return XmlNodeOrder.After;
		}

		// Token: 0x06003137 RID: 12599 RVA: 0x000BD124 File Offset: 0x000BB324
		private XmlNodeOrder ComparePosition(int p1, int loc1, int p2, int loc2)
		{
			if (p1 == p2 && p1 != 0)
			{
				return this.CompareLocation(loc1, loc2);
			}
			int num;
			if (p1 == 0)
			{
				if (this.nodes[loc1].type == XPathNodeType.Attribute)
				{
					num = this.nodes[loc1].parent;
				}
				else
				{
					num = loc1;
				}
			}
			else
			{
				num = p1;
			}
			int num2;
			if (p2 == 0)
			{
				if (this.nodes[loc2].type == XPathNodeType.Attribute)
				{
					num2 = this.nodes[loc2].parent;
				}
				else
				{
					num2 = loc2;
				}
			}
			else
			{
				num2 = p2;
			}
			if (num == num2)
			{
				XPathNodeType type = this.nodes[loc1].type;
				XPathNodeType type2 = this.nodes[loc2].type;
				if (type == XPathNodeType.Namespace)
				{
					if (type2 == XPathNodeType.Attribute)
					{
						return XmlNodeOrder.Before;
					}
					return XmlNodeOrder.After;
				}
				else if (type2 == XPathNodeType.Namespace)
				{
					if (type == XPathNodeType.Attribute)
					{
						return XmlNodeOrder.After;
					}
					return XmlNodeOrder.Before;
				}
			}
			int i;
			for (i = num; i > this.bodyIndex; i = this.nodes[i].parent)
			{
			}
			int j;
			for (j = num2; j > this.bodyIndex; j = this.nodes[j].parent)
			{
			}
			if (i == j)
			{
				return this.CompareLocation(loc1, loc2);
			}
			return this.CompareLocation(i, j);
		}

		// Token: 0x06003138 RID: 12600 RVA: 0x000BD244 File Offset: 0x000BB444
		private SeekableMessageNavigator.Position DecodePosition(long pos)
		{
			SeekableMessageNavigator.Position position = new SeekableMessageNavigator.Position((int)pos, (int)(pos >> 32));
			if (position.elem > 0 && position.elem < this.nextFreeIndex)
			{
				if (position.parent == 0)
				{
					return position;
				}
				if (position.parent > 0 && position.parent < this.nextFreeIndex && this.nodes[position.parent].type == XPathNodeType.Element && this.nodes[position.elem].type == XPathNodeType.Namespace)
				{
					int parent = this.nodes[position.elem].parent;
					int parent2 = position.parent;
					while (parent2 != parent)
					{
						parent2 = this.nodes[parent2].parent;
						if (parent2 == 0)
						{
							goto IL_B7;
						}
					}
					return position;
				}
			}
			IL_B7:
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new QueryProcessingException(QueryProcessingError.InvalidNavigatorPosition, SR.GetString("SeekableMessageNavInvalidPosition")));
		}

		// Token: 0x06003139 RID: 12601 RVA: 0x000BD324 File Offset: 0x000BB524
		private int FindNamespace(int parent, int ns, XPathNamespaceScope scope)
		{
			bool flag = false;
			int num = 0;
			while (ns != 0 && !flag)
			{
				this.Increase();
				string name = this.dom.nodes[ns].name;
				if (this.nsStack.Contains(name))
				{
					ns = this.dom.nodes[ns].nextSibling;
				}
				else
				{
					this.nsStack.Push(name);
					num++;
					string val = this.dom.nodes[ns].val;
					if (name.Length != 0 || val.Length != 0)
					{
						switch (scope)
						{
						case XPathNamespaceScope.All:
							flag = true;
							break;
						case XPathNamespaceScope.ExcludeXml:
							if (string.CompareOrdinal(name, "xml") == 0)
							{
								this.Increase();
								ns = this.dom.nodes[ns].nextSibling;
							}
							else
							{
								flag = true;
							}
							break;
						case XPathNamespaceScope.Local:
							if (this.dom.nodes[ns].parent != parent)
							{
								ns = 0;
							}
							else
							{
								flag = true;
							}
							break;
						}
					}
				}
			}
			if (ns == 0)
			{
				for (int i = 0; i < num; i++)
				{
					this.nsStack.Pop();
				}
			}
			return ns;
		}

		// Token: 0x0600313A RID: 12602 RVA: 0x000BD44C File Offset: 0x000BB64C
		private string GetName(int elem)
		{
			this.LoadOnDemand(elem);
			string prefix = this.dom.nodes[elem].prefix;
			string name = this.dom.nodes[elem].name;
			if (prefix != null && prefix.Length > 0)
			{
				return prefix + ":" + name;
			}
			return name;
		}

		// Token: 0x0600313B RID: 12603 RVA: 0x000BD4A8 File Offset: 0x000BB6A8
		private string GetValue(int elem)
		{
			string val = this.nodes[elem].val;
			if (val == null)
			{
				if (this.stringBuilder == null)
				{
					this.stringBuilder = new StringBuilder();
				}
				else
				{
					this.stringBuilder.Length = 0;
				}
				this.GetValueDriver(elem);
				string text = this.stringBuilder.ToString();
				this.nodes[elem].val = text;
				return text;
			}
			return val;
		}

		// Token: 0x0600313C RID: 12604 RVA: 0x000BD514 File Offset: 0x000BB714
		private void GetValueDriver(int elem)
		{
			this.dom.LoadOnDemand(elem);
			XPathNodeType type = this.nodes[elem].type;
			if (type > XPathNodeType.Element)
			{
				this.stringBuilder.Append(this.nodes[elem].val);
				return;
			}
			string val = this.nodes[elem].val;
			if (val == null)
			{
				for (int num = this.nodes[elem].firstChild; num != 0; num = this.nodes[num].nextSibling)
				{
					this.Increase();
					this.GetValueDriver(num);
				}
				return;
			}
			this.stringBuilder.Append(val);
		}

		// Token: 0x0600313D RID: 12605 RVA: 0x000BD5BA File Offset: 0x000BB7BA
		private int LastChild(int n)
		{
			n = this.nodes[n].firstChild;
			if (n == 0)
			{
				return 0;
			}
			return this.LastSibling(n);
		}

		// Token: 0x0600313E RID: 12606 RVA: 0x000BD5DB File Offset: 0x000BB7DB
		private int LastSibling(int n)
		{
			while (this.nodes[n].nextSibling != 0)
			{
				n = this.nodes[n].nextSibling;
			}
			return n;
		}

		// Token: 0x0600313F RID: 12607 RVA: 0x000BD608 File Offset: 0x000BB808
		private void LoadBody()
		{
			if (!this.message.IsEmpty)
			{
				XmlReader readerAtBodyContents = this.message.GetReaderAtBodyContents();
				if (readerAtBodyContents.ReadState == ReadState.Initial)
				{
					readerAtBodyContents.Read();
				}
				int first = this.nextFreeIndex;
				this.ReadChildNodes(readerAtBodyContents, this.bodyIndex, 3);
				int bound = this.nextFreeIndex;
				if (this.atomize)
				{
					this.Atomize(first, bound);
				}
			}
		}

		// Token: 0x06003140 RID: 12608 RVA: 0x000BD66C File Offset: 0x000BB86C
		private void LoadHeader(int self)
		{
			XmlReader readerAtHeader = this.headers.GetReaderAtHeader(self - 6);
			if (readerAtHeader.ReadState == ReadState.Initial)
			{
				readerAtHeader.Read();
			}
			int first = this.nextFreeIndex;
			this.nodes[self].firstNamespace = 3;
			this.nodes[self].prefix = (this.atomize ? this.nameTable.Add(readerAtHeader.Prefix) : readerAtHeader.Prefix);
			this.nodes[self].baseUri = readerAtHeader.BaseURI;
			this.nodes[self].xmlLang = readerAtHeader.XmlLang;
			if (!readerAtHeader.IsEmptyElement)
			{
				this.ReadAttributes(self, readerAtHeader);
				readerAtHeader.Read();
				this.ReadChildNodes(readerAtHeader, self, this.nodes[self].firstNamespace);
			}
			else
			{
				this.ReadAttributes(self, readerAtHeader);
			}
			int bound = this.nextFreeIndex;
			if (this.atomize)
			{
				this.Atomize(first, bound);
			}
		}

		// Token: 0x06003141 RID: 12609 RVA: 0x000BD762 File Offset: 0x000BB962
		private void LoadOnDemand()
		{
			this.dom.LoadOnDemand(this.location);
		}

		// Token: 0x06003142 RID: 12610 RVA: 0x000BD778 File Offset: 0x000BB978
		private void LoadOnDemand(int elem)
		{
			if (elem > this.bodyIndex || elem < 6)
			{
				return;
			}
			if (this.nodes[elem].firstChild == -1)
			{
				this.nodes[elem].firstChild = 0;
				if (elem != this.bodyIndex)
				{
					this.LoadHeader(elem);
					return;
				}
				if (this.includeBody)
				{
					this.LoadBody();
					return;
				}
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NavigatorInvalidBodyAccessException(SR.GetString("SeekableMessageNavBodyForbidden")));
			}
			else
			{
				if (elem == this.bodyIndex && !this.includeBody)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NavigatorInvalidBodyAccessException(SR.GetString("SeekableMessageNavBodyForbidden")));
				}
				return;
			}
		}

		// Token: 0x06003143 RID: 12611 RVA: 0x000BD820 File Offset: 0x000BBA20
		private int NewNode()
		{
			if (this.nextFreeIndex == this.nodes.Length)
			{
				int num;
				if (this.nodes.Length <= 1000)
				{
					num = this.nodes.Length * 2;
				}
				else
				{
					num = this.nodes.Length + 1000;
				}
				SeekableMessageNavigator.Node[] array = new SeekableMessageNavigator.Node[num];
				this.nodes.CopyTo(array, 0);
				this.nodes = array;
			}
			int num2 = this.nextFreeIndex;
			this.nextFreeIndex = num2 + 1;
			return num2;
		}

		// Token: 0x06003144 RID: 12612 RVA: 0x000BD894 File Offset: 0x000BBA94
		private void ReadAttributes(int elem, XmlReader reader)
		{
			while (reader.MoveToNextAttribute())
			{
				if (QueryDataModel.IsAttribute(reader.NamespaceURI))
				{
					int num = this.NewNode();
					this.nodes[num].type = XPathNodeType.Attribute;
					this.nodes[num].prefix = reader.Prefix;
					this.nodes[num].name = reader.LocalName;
					this.nodes[num].ns = reader.NamespaceURI;
					this.nodes[num].val = reader.Value;
					this.nodes[num].baseUri = reader.BaseURI;
					this.nodes[num].xmlLang = reader.XmlLang;
					this.AddAttribute(elem, num);
				}
				else
				{
					string text = (reader.Prefix.Length == 0) ? string.Empty : reader.LocalName;
					if (string.CompareOrdinal(text, "xml") == 0 || string.CompareOrdinal(text, "xmlns") == 0)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new QueryProcessingException(QueryProcessingError.InvalidNamespacePrefix, SR.GetString("SeekableMessageNavOverrideForbidden", new object[]
						{
							reader.Name
						})));
					}
					int num2 = this.NewNode();
					this.nodes[num2].type = XPathNodeType.Namespace;
					this.nodes[num2].name = text;
					this.nodes[num2].val = reader.Value;
					this.nodes[num2].baseUri = reader.BaseURI;
					this.nodes[num2].xmlLang = reader.XmlLang;
					this.AddNamespace(elem, num2);
				}
			}
		}

		// Token: 0x06003145 RID: 12613 RVA: 0x000BDA4C File Offset: 0x000BBC4C
		private int ReadChildNodes(XmlReader reader, int parent, int parentNS)
		{
			int num = 0;
			for (;;)
			{
				switch (reader.NodeType)
				{
				case XmlNodeType.None:
				case XmlNodeType.EndElement:
				case XmlNodeType.EndEntity:
					return num;
				case XmlNodeType.Element:
					num = this.NewNode();
					this.nodes[num].type = XPathNodeType.Element;
					this.nodes[num].prefix = reader.Prefix;
					this.nodes[num].name = reader.LocalName;
					this.nodes[num].ns = reader.NamespaceURI;
					this.nodes[num].firstNamespace = parentNS;
					this.nodes[num].baseUri = reader.BaseURI;
					this.nodes[num].xmlLang = reader.XmlLang;
					if (!reader.IsEmptyElement)
					{
						this.ReadAttributes(num, reader);
						reader.Read();
						this.ReadChildNodes(reader, num, this.nodes[num].firstNamespace);
					}
					else
					{
						this.ReadAttributes(num, reader);
						this.nodes[num].empty = true;
					}
					this.AddChild(parent, num);
					break;
				case XmlNodeType.Text:
				case XmlNodeType.CDATA:
					num = this.LastChild(parent);
					if (num == 0 || (this.nodes[num].type != XPathNodeType.Text && this.nodes[num].type != XPathNodeType.Whitespace && this.nodes[num].type != XPathNodeType.SignificantWhitespace))
					{
						num = this.NewNode();
						this.nodes[num].baseUri = reader.BaseURI;
						this.nodes[num].xmlLang = reader.XmlLang;
						this.AddChild(parent, num);
					}
					this.nodes[num].type = XPathNodeType.Text;
					this.nodes[num].val = reader.Value;
					break;
				case XmlNodeType.EntityReference:
					reader.ResolveEntity();
					reader.Read();
					this.ReadChildNodes(reader, parent, parentNS);
					break;
				case XmlNodeType.ProcessingInstruction:
					num = this.NewNode();
					this.nodes[num].type = XPathNodeType.ProcessingInstruction;
					this.nodes[num].name = reader.LocalName;
					this.nodes[num].val = reader.Value;
					this.nodes[num].baseUri = reader.BaseURI;
					this.nodes[num].xmlLang = reader.XmlLang;
					this.AddChild(parent, num);
					break;
				case XmlNodeType.Comment:
					num = this.NewNode();
					this.nodes[num].type = XPathNodeType.Comment;
					this.nodes[num].val = reader.Value;
					this.nodes[num].baseUri = reader.BaseURI;
					this.nodes[num].xmlLang = reader.XmlLang;
					this.AddChild(parent, num);
					break;
				case XmlNodeType.Whitespace:
					goto IL_331;
				case XmlNodeType.SignificantWhitespace:
					if (reader.XmlSpace != XmlSpace.Preserve)
					{
						goto IL_331;
					}
					num = this.LastChild(parent);
					if (num != 0 && (this.nodes[num].type == XPathNodeType.Text || this.nodes[num].type == XPathNodeType.Whitespace || this.nodes[num].type == XPathNodeType.SignificantWhitespace))
					{
						this.nodes[num].val = this.nodes[num].val + reader.Value;
					}
					else
					{
						num = this.NewNode();
						this.nodes[num].type = XPathNodeType.SignificantWhitespace;
						this.nodes[num].val = reader.Value;
						this.nodes[num].baseUri = reader.BaseURI;
						this.nodes[num].xmlLang = reader.XmlLang;
						this.AddChild(parent, num);
					}
					break;
				}
				IL_4EB:
				if (!reader.Read())
				{
					return num;
				}
				continue;
				IL_331:
				if (this.space != XmlSpace.Preserve)
				{
					goto IL_4EB;
				}
				num = this.LastChild(parent);
				if (num != 0 && (this.nodes[num].type == XPathNodeType.Text || this.nodes[num].type == XPathNodeType.Whitespace || this.nodes[num].type == XPathNodeType.SignificantWhitespace))
				{
					this.nodes[num].val = this.nodes[num].val + reader.Value;
					goto IL_4EB;
				}
				num = this.NewNode();
				this.nodes[num].type = XPathNodeType.Whitespace;
				this.nodes[num].val = reader.Value;
				this.nodes[num].baseUri = reader.BaseURI;
				this.nodes[num].xmlLang = reader.XmlLang;
				this.AddChild(parent, num);
				goto IL_4EB;
			}
			return num;
		}

		// Token: 0x17000BA1 RID: 2977
		// (get) Token: 0x06003146 RID: 12614 RVA: 0x000BDF50 File Offset: 0x000BC150
		// (set) Token: 0x06003147 RID: 12615 RVA: 0x000BDF5D File Offset: 0x000BC15D
		int INodeCounter.CounterMarker
		{
			get
			{
				return this.counter.nodeCount;
			}
			set
			{
				this.counter.nodeCount = value;
			}
		}

		// Token: 0x17000BA2 RID: 2978
		// (set) Token: 0x06003148 RID: 12616 RVA: 0x000BDF6B File Offset: 0x000BC16B
		int INodeCounter.MaxCounter
		{
			set
			{
				this.counter.nodeCountMax = value;
			}
		}

		// Token: 0x06003149 RID: 12617 RVA: 0x000BDF79 File Offset: 0x000BC179
		int INodeCounter.ElapsedCount(int marker)
		{
			return marker - this.counter.nodeCount;
		}

		// Token: 0x0600314A RID: 12618 RVA: 0x000BDF88 File Offset: 0x000BC188
		private void Increase()
		{
			if (this.counter.nodeCount > 0)
			{
				this.counter.nodeCount--;
				return;
			}
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new XPathNavigatorException(SR.GetString("FilterNodeQuotaExceeded", new object[]
			{
				this.counter.nodeCountMax
			})));
		}

		// Token: 0x0600314B RID: 12619 RVA: 0x000BDFE9 File Offset: 0x000BC1E9
		void INodeCounter.Increase()
		{
			this.Increase();
		}

		// Token: 0x0600314C RID: 12620 RVA: 0x000BDFF1 File Offset: 0x000BC1F1
		void INodeCounter.IncreaseBy(int count)
		{
			this.counter.nodeCount -= count - 1;
			this.Increase();
		}

		// Token: 0x04002623 RID: 9763
		private static SeekableMessageNavigator.Node[] BlankDom = new SeekableMessageNavigator.Node[6];

		// Token: 0x04002624 RID: 9764
		private const string XmlP = "xml";

		// Token: 0x04002625 RID: 9765
		private const string XmlnsP = "xmlns";

		// Token: 0x04002626 RID: 9766
		private const string SoapP = "s";

		// Token: 0x04002627 RID: 9767
		private const string EnvelopeTag = "Envelope";

		// Token: 0x04002628 RID: 9768
		private const string HeaderTag = "Header";

		// Token: 0x04002629 RID: 9769
		private const string BodyTag = "Body";

		// Token: 0x0400262A RID: 9770
		private const int NullIndex = 0;

		// Token: 0x0400262B RID: 9771
		private const int RootIndex = 1;

		// Token: 0x0400262C RID: 9772
		private const int EnvelopeIndex = 2;

		// Token: 0x0400262D RID: 9773
		private const int SoapNSIndex = 3;

		// Token: 0x0400262E RID: 9774
		private const int XmlNSIndex = 4;

		// Token: 0x0400262F RID: 9775
		private const int HeaderIndex = 5;

		// Token: 0x04002630 RID: 9776
		private const int FirstHeaderIndex = 6;

		// Token: 0x04002631 RID: 9777
		private const int StartSize = 50;

		// Token: 0x04002632 RID: 9778
		private const int GrowFactor = 2;

		// Token: 0x04002633 RID: 9779
		private const int StretchMax = 1000;

		// Token: 0x04002634 RID: 9780
		private const int GrowInc = 1000;

		// Token: 0x04002635 RID: 9781
		private Message message;

		// Token: 0x04002636 RID: 9782
		private MessageHeaders headers;

		// Token: 0x04002637 RID: 9783
		private XmlSpace space;

		// Token: 0x04002638 RID: 9784
		private StringBuilder stringBuilder;

		// Token: 0x04002639 RID: 9785
		private SeekableMessageNavigator.Node[] nodes;

		// Token: 0x0400263A RID: 9786
		private int bodyIndex;

		// Token: 0x0400263B RID: 9787
		private int nextFreeIndex;

		// Token: 0x0400263C RID: 9788
		private NameTable nameTable;

		// Token: 0x0400263D RID: 9789
		private bool includeBody;

		// Token: 0x0400263E RID: 9790
		private bool atomize;

		// Token: 0x0400263F RID: 9791
		private int nodeCount;

		// Token: 0x04002640 RID: 9792
		private int nodeCountMax;

		// Token: 0x04002641 RID: 9793
		private SeekableMessageNavigator dom;

		// Token: 0x04002642 RID: 9794
		private SeekableMessageNavigator counter;

		// Token: 0x04002643 RID: 9795
		private Stack<string> nsStack;

		// Token: 0x04002644 RID: 9796
		private int location;

		// Token: 0x04002645 RID: 9797
		private int specialParent;

		// Token: 0x02000C4D RID: 3149
		private struct Node
		{
			// Token: 0x04004459 RID: 17497
			internal int parent;

			// Token: 0x0400445A RID: 17498
			internal int firstAttribute;

			// Token: 0x0400445B RID: 17499
			internal int firstChild;

			// Token: 0x0400445C RID: 17500
			internal int firstNamespace;

			// Token: 0x0400445D RID: 17501
			internal int nextSibling;

			// Token: 0x0400445E RID: 17502
			internal int prevSibling;

			// Token: 0x0400445F RID: 17503
			internal string baseUri;

			// Token: 0x04004460 RID: 17504
			internal bool empty;

			// Token: 0x04004461 RID: 17505
			internal string name;

			// Token: 0x04004462 RID: 17506
			internal string ns;

			// Token: 0x04004463 RID: 17507
			internal string prefix;

			// Token: 0x04004464 RID: 17508
			internal string val;

			// Token: 0x04004465 RID: 17509
			internal string xmlLang;

			// Token: 0x04004466 RID: 17510
			internal XPathNodeType type;
		}

		// Token: 0x02000C4E RID: 3150
		private struct Position
		{
			// Token: 0x0600777C RID: 30588 RVA: 0x001BE0F2 File Offset: 0x001BC2F2
			internal Position(int e, int p)
			{
				this.elem = e;
				this.parent = p;
			}

			// Token: 0x04004467 RID: 17511
			internal int elem;

			// Token: 0x04004468 RID: 17512
			internal int parent;
		}
	}
}
