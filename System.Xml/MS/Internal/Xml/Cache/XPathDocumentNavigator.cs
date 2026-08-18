using System;
using System.Text;
using System.Xml;
using System.Xml.XPath;

namespace MS.Internal.Xml.Cache
{
	// Token: 0x02000109 RID: 265
	internal sealed class XPathDocumentNavigator : XPathNavigator, IXmlLineInfo
	{
		// Token: 0x06001004 RID: 4100 RVA: 0x00049825 File Offset: 0x00048825
		public XPathDocumentNavigator(XPathNode[] pageCurrent, int idxCurrent, XPathNode[] pageParent, int idxParent)
		{
			this.pageCurrent = pageCurrent;
			this.pageParent = pageParent;
			this.idxCurrent = idxCurrent;
			this.idxParent = idxParent;
		}

		// Token: 0x06001005 RID: 4101 RVA: 0x0004984A File Offset: 0x0004884A
		public XPathDocumentNavigator(XPathDocumentNavigator nav) : this(nav.pageCurrent, nav.idxCurrent, nav.pageParent, nav.idxParent)
		{
			this.atomizedLocalName = nav.atomizedLocalName;
		}

		// Token: 0x170003C8 RID: 968
		// (get) Token: 0x06001006 RID: 4102 RVA: 0x00049878 File Offset: 0x00048878
		public override string Value
		{
			get
			{
				string value = this.pageCurrent[this.idxCurrent].Value;
				if (value != null)
				{
					return value;
				}
				if (this.idxParent != 0)
				{
					return this.pageParent[this.idxParent].Value;
				}
				string text = string.Empty;
				StringBuilder stringBuilder = null;
				XPathNode[] array;
				XPathNode[] pageEnd = array = this.pageCurrent;
				int num;
				int idxEnd = num = this.idxCurrent;
				if (!XPathNodeHelper.GetNonDescendant(ref pageEnd, ref idxEnd))
				{
					pageEnd = null;
					idxEnd = 0;
				}
				while (XPathNodeHelper.GetTextFollowing(ref array, ref num, pageEnd, idxEnd))
				{
					if (text.Length == 0)
					{
						text = array[num].Value;
					}
					else
					{
						if (stringBuilder == null)
						{
							stringBuilder = new StringBuilder();
							stringBuilder.Append(text);
						}
						stringBuilder.Append(array[num].Value);
					}
				}
				if (stringBuilder == null)
				{
					return text;
				}
				return stringBuilder.ToString();
			}
		}

		// Token: 0x06001007 RID: 4103 RVA: 0x0004994D File Offset: 0x0004894D
		public override XPathNavigator Clone()
		{
			return new XPathDocumentNavigator(this.pageCurrent, this.idxCurrent, this.pageParent, this.idxParent);
		}

		// Token: 0x170003C9 RID: 969
		// (get) Token: 0x06001008 RID: 4104 RVA: 0x0004996C File Offset: 0x0004896C
		public override XPathNodeType NodeType
		{
			get
			{
				return this.pageCurrent[this.idxCurrent].NodeType;
			}
		}

		// Token: 0x170003CA RID: 970
		// (get) Token: 0x06001009 RID: 4105 RVA: 0x00049984 File Offset: 0x00048984
		public override string LocalName
		{
			get
			{
				return this.pageCurrent[this.idxCurrent].LocalName;
			}
		}

		// Token: 0x170003CB RID: 971
		// (get) Token: 0x0600100A RID: 4106 RVA: 0x0004999C File Offset: 0x0004899C
		public override string NamespaceURI
		{
			get
			{
				return this.pageCurrent[this.idxCurrent].NamespaceUri;
			}
		}

		// Token: 0x170003CC RID: 972
		// (get) Token: 0x0600100B RID: 4107 RVA: 0x000499B4 File Offset: 0x000489B4
		public override string Name
		{
			get
			{
				return this.pageCurrent[this.idxCurrent].Name;
			}
		}

		// Token: 0x170003CD RID: 973
		// (get) Token: 0x0600100C RID: 4108 RVA: 0x000499CC File Offset: 0x000489CC
		public override string Prefix
		{
			get
			{
				return this.pageCurrent[this.idxCurrent].Prefix;
			}
		}

		// Token: 0x170003CE RID: 974
		// (get) Token: 0x0600100D RID: 4109 RVA: 0x000499E4 File Offset: 0x000489E4
		public override string BaseURI
		{
			get
			{
				XPathNode[] array;
				int parent;
				if (this.idxParent != 0)
				{
					array = this.pageParent;
					parent = this.idxParent;
				}
				else
				{
					array = this.pageCurrent;
					parent = this.idxCurrent;
				}
				for (;;)
				{
					XPathNodeType nodeType = array[parent].NodeType;
					switch (nodeType)
					{
					case XPathNodeType.Root:
					case XPathNodeType.Element:
						goto IL_45;
					default:
						if (nodeType == XPathNodeType.ProcessingInstruction)
						{
							goto IL_45;
						}
						parent = array[parent].GetParent(out array);
						if (parent == 0)
						{
							goto Block_3;
						}
						break;
					}
				}
				IL_45:
				return array[parent].BaseUri;
				Block_3:
				return string.Empty;
			}
		}

		// Token: 0x170003CF RID: 975
		// (get) Token: 0x0600100E RID: 4110 RVA: 0x00049A5A File Offset: 0x00048A5A
		public override bool IsEmptyElement
		{
			get
			{
				return this.pageCurrent[this.idxCurrent].AllowShortcutTag;
			}
		}

		// Token: 0x170003D0 RID: 976
		// (get) Token: 0x0600100F RID: 4111 RVA: 0x00049A72 File Offset: 0x00048A72
		public override XmlNameTable NameTable
		{
			get
			{
				return this.pageCurrent[this.idxCurrent].Document.NameTable;
			}
		}

		// Token: 0x06001010 RID: 4112 RVA: 0x00049A90 File Offset: 0x00048A90
		public override bool MoveToFirstAttribute()
		{
			XPathNode[] array = this.pageCurrent;
			int num = this.idxCurrent;
			if (XPathNodeHelper.GetFirstAttribute(ref this.pageCurrent, ref this.idxCurrent))
			{
				this.pageParent = array;
				this.idxParent = num;
				return true;
			}
			return false;
		}

		// Token: 0x06001011 RID: 4113 RVA: 0x00049ACF File Offset: 0x00048ACF
		public override bool MoveToNextAttribute()
		{
			return XPathNodeHelper.GetNextAttribute(ref this.pageCurrent, ref this.idxCurrent);
		}

		// Token: 0x170003D1 RID: 977
		// (get) Token: 0x06001012 RID: 4114 RVA: 0x00049AE2 File Offset: 0x00048AE2
		public override bool HasAttributes
		{
			get
			{
				return this.pageCurrent[this.idxCurrent].HasAttribute;
			}
		}

		// Token: 0x06001013 RID: 4115 RVA: 0x00049AFC File Offset: 0x00048AFC
		public override bool MoveToAttribute(string localName, string namespaceURI)
		{
			XPathNode[] array = this.pageCurrent;
			int num = this.idxCurrent;
			if (localName != this.atomizedLocalName)
			{
				this.atomizedLocalName = ((localName != null) ? this.NameTable.Get(localName) : null);
			}
			if (XPathNodeHelper.GetAttribute(ref this.pageCurrent, ref this.idxCurrent, this.atomizedLocalName, namespaceURI))
			{
				this.pageParent = array;
				this.idxParent = num;
				return true;
			}
			return false;
		}

		// Token: 0x06001014 RID: 4116 RVA: 0x00049B64 File Offset: 0x00048B64
		public override bool MoveToFirstNamespace(XPathNamespaceScope namespaceScope)
		{
			XPathNode[] array;
			int num;
			if (namespaceScope == XPathNamespaceScope.Local)
			{
				num = XPathNodeHelper.GetLocalNamespaces(this.pageCurrent, this.idxCurrent, out array);
			}
			else
			{
				num = XPathNodeHelper.GetInScopeNamespaces(this.pageCurrent, this.idxCurrent, out array);
			}
			while (num != 0)
			{
				if (namespaceScope != XPathNamespaceScope.ExcludeXml || !array[num].IsXmlNamespaceNode)
				{
					this.pageParent = this.pageCurrent;
					this.idxParent = this.idxCurrent;
					this.pageCurrent = array;
					this.idxCurrent = num;
					return true;
				}
				num = array[num].GetSibling(out array);
			}
			return false;
		}

		// Token: 0x06001015 RID: 4117 RVA: 0x00049BF0 File Offset: 0x00048BF0
		public override bool MoveToNextNamespace(XPathNamespaceScope scope)
		{
			XPathNode[] array = this.pageCurrent;
			int sibling = this.idxCurrent;
			if (array[sibling].NodeType != XPathNodeType.Namespace)
			{
				return false;
			}
			for (;;)
			{
				sibling = array[sibling].GetSibling(out array);
				if (sibling == 0)
				{
					break;
				}
				switch (scope)
				{
				case XPathNamespaceScope.ExcludeXml:
					if (array[sibling].IsXmlNamespaceNode)
					{
						continue;
					}
					break;
				case XPathNamespaceScope.Local:
					goto IL_49;
				}
				goto Block_3;
			}
			return false;
			Block_3:
			goto IL_7A;
			IL_49:
			XPathNode[] array2;
			int parent = array[sibling].GetParent(out array2);
			if (parent != this.idxParent || array2 != this.pageParent)
			{
				return false;
			}
			IL_7A:
			this.pageCurrent = array;
			this.idxCurrent = sibling;
			return true;
		}

		// Token: 0x06001016 RID: 4118 RVA: 0x00049C86 File Offset: 0x00048C86
		public override bool MoveToNext()
		{
			return XPathNodeHelper.GetContentSibling(ref this.pageCurrent, ref this.idxCurrent);
		}

		// Token: 0x06001017 RID: 4119 RVA: 0x00049C99 File Offset: 0x00048C99
		public override bool MoveToPrevious()
		{
			return this.idxParent == 0 && XPathNodeHelper.GetPreviousContentSibling(ref this.pageCurrent, ref this.idxCurrent);
		}

		// Token: 0x06001018 RID: 4120 RVA: 0x00049CB8 File Offset: 0x00048CB8
		public override bool MoveToFirstChild()
		{
			if (this.pageCurrent[this.idxCurrent].HasCollapsedText)
			{
				this.pageParent = this.pageCurrent;
				this.idxParent = this.idxCurrent;
				this.idxCurrent = this.pageCurrent[this.idxCurrent].Document.GetCollapsedTextNode(out this.pageCurrent);
				return true;
			}
			return XPathNodeHelper.GetContentChild(ref this.pageCurrent, ref this.idxCurrent);
		}

		// Token: 0x06001019 RID: 4121 RVA: 0x00049D30 File Offset: 0x00048D30
		public override bool MoveToParent()
		{
			if (this.idxParent != 0)
			{
				this.pageCurrent = this.pageParent;
				this.idxCurrent = this.idxParent;
				this.pageParent = null;
				this.idxParent = 0;
				return true;
			}
			return XPathNodeHelper.GetParent(ref this.pageCurrent, ref this.idxCurrent);
		}

		// Token: 0x0600101A RID: 4122 RVA: 0x00049D80 File Offset: 0x00048D80
		public override bool MoveTo(XPathNavigator other)
		{
			XPathDocumentNavigator xpathDocumentNavigator = other as XPathDocumentNavigator;
			if (xpathDocumentNavigator != null)
			{
				this.pageCurrent = xpathDocumentNavigator.pageCurrent;
				this.idxCurrent = xpathDocumentNavigator.idxCurrent;
				this.pageParent = xpathDocumentNavigator.pageParent;
				this.idxParent = xpathDocumentNavigator.idxParent;
				return true;
			}
			return false;
		}

		// Token: 0x0600101B RID: 4123 RVA: 0x00049DCC File Offset: 0x00048DCC
		public override bool MoveToId(string id)
		{
			XPathNode[] array;
			int num = this.pageCurrent[this.idxCurrent].Document.LookupIdElement(id, out array);
			if (num != 0)
			{
				this.pageCurrent = array;
				this.idxCurrent = num;
				this.pageParent = null;
				this.idxParent = 0;
				return true;
			}
			return false;
		}

		// Token: 0x0600101C RID: 4124 RVA: 0x00049E1C File Offset: 0x00048E1C
		public override bool IsSamePosition(XPathNavigator other)
		{
			XPathDocumentNavigator xpathDocumentNavigator = other as XPathDocumentNavigator;
			return xpathDocumentNavigator != null && (this.idxCurrent == xpathDocumentNavigator.idxCurrent && this.pageCurrent == xpathDocumentNavigator.pageCurrent && this.idxParent == xpathDocumentNavigator.idxParent) && this.pageParent == xpathDocumentNavigator.pageParent;
		}

		// Token: 0x170003D2 RID: 978
		// (get) Token: 0x0600101D RID: 4125 RVA: 0x00049E6F File Offset: 0x00048E6F
		public override bool HasChildren
		{
			get
			{
				return this.pageCurrent[this.idxCurrent].HasContentChild;
			}
		}

		// Token: 0x0600101E RID: 4126 RVA: 0x00049E87 File Offset: 0x00048E87
		public override void MoveToRoot()
		{
			if (this.idxParent != 0)
			{
				this.pageParent = null;
				this.idxParent = 0;
			}
			this.idxCurrent = this.pageCurrent[this.idxCurrent].GetRoot(out this.pageCurrent);
		}

		// Token: 0x0600101F RID: 4127 RVA: 0x00049EC1 File Offset: 0x00048EC1
		public override bool MoveToChild(string localName, string namespaceURI)
		{
			if (localName != this.atomizedLocalName)
			{
				this.atomizedLocalName = ((localName != null) ? this.NameTable.Get(localName) : null);
			}
			return XPathNodeHelper.GetElementChild(ref this.pageCurrent, ref this.idxCurrent, this.atomizedLocalName, namespaceURI);
		}

		// Token: 0x06001020 RID: 4128 RVA: 0x00049EFC File Offset: 0x00048EFC
		public override bool MoveToNext(string localName, string namespaceURI)
		{
			if (localName != this.atomizedLocalName)
			{
				this.atomizedLocalName = ((localName != null) ? this.NameTable.Get(localName) : null);
			}
			return XPathNodeHelper.GetElementSibling(ref this.pageCurrent, ref this.idxCurrent, this.atomizedLocalName, namespaceURI);
		}

		// Token: 0x06001021 RID: 4129 RVA: 0x00049F38 File Offset: 0x00048F38
		public override bool MoveToChild(XPathNodeType type)
		{
			if (!this.pageCurrent[this.idxCurrent].HasCollapsedText)
			{
				return XPathNodeHelper.GetContentChild(ref this.pageCurrent, ref this.idxCurrent, type);
			}
			if (type != XPathNodeType.Text && type != XPathNodeType.All)
			{
				return false;
			}
			this.pageParent = this.pageCurrent;
			this.idxParent = this.idxCurrent;
			this.idxCurrent = this.pageCurrent[this.idxCurrent].Document.GetCollapsedTextNode(out this.pageCurrent);
			return true;
		}

		// Token: 0x06001022 RID: 4130 RVA: 0x00049FBB File Offset: 0x00048FBB
		public override bool MoveToNext(XPathNodeType type)
		{
			return XPathNodeHelper.GetContentSibling(ref this.pageCurrent, ref this.idxCurrent, type);
		}

		// Token: 0x06001023 RID: 4131 RVA: 0x00049FD0 File Offset: 0x00048FD0
		public override bool MoveToFollowing(string localName, string namespaceURI, XPathNavigator end)
		{
			if (localName != this.atomizedLocalName)
			{
				this.atomizedLocalName = ((localName != null) ? this.NameTable.Get(localName) : null);
			}
			XPathNode[] pageEnd;
			int followingEnd = this.GetFollowingEnd(end as XPathDocumentNavigator, false, out pageEnd);
			if (this.idxParent == 0)
			{
				return XPathNodeHelper.GetElementFollowing(ref this.pageCurrent, ref this.idxCurrent, pageEnd, followingEnd, this.atomizedLocalName, namespaceURI);
			}
			if (!XPathNodeHelper.GetElementFollowing(ref this.pageParent, ref this.idxParent, pageEnd, followingEnd, this.atomizedLocalName, namespaceURI))
			{
				return false;
			}
			this.pageCurrent = this.pageParent;
			this.idxCurrent = this.idxParent;
			this.pageParent = null;
			this.idxParent = 0;
			return true;
		}

		// Token: 0x06001024 RID: 4132 RVA: 0x0004A078 File Offset: 0x00049078
		public override bool MoveToFollowing(XPathNodeType type, XPathNavigator end)
		{
			XPathDocumentNavigator xpathDocumentNavigator = end as XPathDocumentNavigator;
			XPathNode[] array;
			int followingEnd;
			if (type == XPathNodeType.Text || type == XPathNodeType.All)
			{
				if (this.pageCurrent[this.idxCurrent].HasCollapsedText)
				{
					if (xpathDocumentNavigator != null && this.idxCurrent == xpathDocumentNavigator.idxParent && this.pageCurrent == xpathDocumentNavigator.pageParent)
					{
						return false;
					}
					this.pageParent = this.pageCurrent;
					this.idxParent = this.idxCurrent;
					this.idxCurrent = this.pageCurrent[this.idxCurrent].Document.GetCollapsedTextNode(out this.pageCurrent);
					return true;
				}
				else if (type == XPathNodeType.Text)
				{
					followingEnd = this.GetFollowingEnd(xpathDocumentNavigator, true, out array);
					XPathNode[] array2;
					int num;
					if (this.idxParent != 0)
					{
						array2 = this.pageParent;
						num = this.idxParent;
					}
					else
					{
						array2 = this.pageCurrent;
						num = this.idxCurrent;
					}
					if (xpathDocumentNavigator != null && xpathDocumentNavigator.idxParent != 0 && num == followingEnd && array2 == array)
					{
						return false;
					}
					if (!XPathNodeHelper.GetTextFollowing(ref array2, ref num, array, followingEnd))
					{
						return false;
					}
					if (array2[num].NodeType == XPathNodeType.Element)
					{
						this.idxCurrent = array2[num].Document.GetCollapsedTextNode(out this.pageCurrent);
						this.pageParent = array2;
						this.idxParent = num;
					}
					else
					{
						this.pageCurrent = array2;
						this.idxCurrent = num;
						this.pageParent = null;
						this.idxParent = 0;
					}
					return true;
				}
			}
			followingEnd = this.GetFollowingEnd(xpathDocumentNavigator, false, out array);
			if (this.idxParent == 0)
			{
				return XPathNodeHelper.GetContentFollowing(ref this.pageCurrent, ref this.idxCurrent, array, followingEnd, type);
			}
			if (!XPathNodeHelper.GetContentFollowing(ref this.pageParent, ref this.idxParent, array, followingEnd, type))
			{
				return false;
			}
			this.pageCurrent = this.pageParent;
			this.idxCurrent = this.idxParent;
			this.pageParent = null;
			this.idxParent = 0;
			return true;
		}

		// Token: 0x06001025 RID: 4133 RVA: 0x0004A235 File Offset: 0x00049235
		public override XPathNodeIterator SelectChildren(XPathNodeType type)
		{
			return new XPathDocumentKindChildIterator(this, type);
		}

		// Token: 0x06001026 RID: 4134 RVA: 0x0004A23E File Offset: 0x0004923E
		public override XPathNodeIterator SelectChildren(string name, string namespaceURI)
		{
			if (name == null || name.Length == 0)
			{
				return base.SelectChildren(name, namespaceURI);
			}
			return new XPathDocumentElementChildIterator(this, name, namespaceURI);
		}

		// Token: 0x06001027 RID: 4135 RVA: 0x0004A25C File Offset: 0x0004925C
		public override XPathNodeIterator SelectDescendants(XPathNodeType type, bool matchSelf)
		{
			return new XPathDocumentKindDescendantIterator(this, type, matchSelf);
		}

		// Token: 0x06001028 RID: 4136 RVA: 0x0004A266 File Offset: 0x00049266
		public override XPathNodeIterator SelectDescendants(string name, string namespaceURI, bool matchSelf)
		{
			if (name == null || name.Length == 0)
			{
				return base.SelectDescendants(name, namespaceURI, matchSelf);
			}
			return new XPathDocumentElementDescendantIterator(this, name, namespaceURI, matchSelf);
		}

		// Token: 0x06001029 RID: 4137 RVA: 0x0004A288 File Offset: 0x00049288
		public override XmlNodeOrder ComparePosition(XPathNavigator other)
		{
			XPathDocumentNavigator xpathDocumentNavigator = other as XPathDocumentNavigator;
			if (xpathDocumentNavigator != null)
			{
				XPathDocument document = this.pageCurrent[this.idxCurrent].Document;
				XPathDocument document2 = xpathDocumentNavigator.pageCurrent[xpathDocumentNavigator.idxCurrent].Document;
				if (document == document2)
				{
					int num = this.GetPrimaryLocation();
					int num2 = xpathDocumentNavigator.GetPrimaryLocation();
					if (num == num2)
					{
						num = this.GetSecondaryLocation();
						num2 = xpathDocumentNavigator.GetSecondaryLocation();
						if (num == num2)
						{
							return XmlNodeOrder.Same;
						}
					}
					if (num >= num2)
					{
						return XmlNodeOrder.After;
					}
					return XmlNodeOrder.Before;
				}
			}
			return XmlNodeOrder.Unknown;
		}

		// Token: 0x0600102A RID: 4138 RVA: 0x0004A308 File Offset: 0x00049308
		public override bool IsDescendant(XPathNavigator other)
		{
			XPathDocumentNavigator xpathDocumentNavigator = other as XPathDocumentNavigator;
			if (xpathDocumentNavigator != null)
			{
				XPathNode[] array;
				int parent;
				if (xpathDocumentNavigator.idxParent != 0)
				{
					array = xpathDocumentNavigator.pageParent;
					parent = xpathDocumentNavigator.idxParent;
				}
				else
				{
					parent = xpathDocumentNavigator.pageCurrent[xpathDocumentNavigator.idxCurrent].GetParent(out array);
				}
				while (parent != 0)
				{
					if (parent == this.idxCurrent && array == this.pageCurrent)
					{
						return true;
					}
					parent = array[parent].GetParent(out array);
				}
			}
			return false;
		}

		// Token: 0x0600102B RID: 4139 RVA: 0x0004A379 File Offset: 0x00049379
		private int GetPrimaryLocation()
		{
			if (this.idxParent == 0)
			{
				return XPathNodeHelper.GetLocation(this.pageCurrent, this.idxCurrent);
			}
			return XPathNodeHelper.GetLocation(this.pageParent, this.idxParent);
		}

		// Token: 0x0600102C RID: 4140 RVA: 0x0004A3A8 File Offset: 0x000493A8
		private int GetSecondaryLocation()
		{
			if (this.idxParent == 0)
			{
				return int.MinValue;
			}
			switch (this.pageCurrent[this.idxCurrent].NodeType)
			{
			case XPathNodeType.Attribute:
				return XPathNodeHelper.GetLocation(this.pageCurrent, this.idxCurrent);
			case XPathNodeType.Namespace:
				return -2147483647 + XPathNodeHelper.GetLocation(this.pageCurrent, this.idxCurrent);
			default:
				return int.MaxValue;
			}
		}

		// Token: 0x170003D3 RID: 979
		// (get) Token: 0x0600102D RID: 4141 RVA: 0x0004A41C File Offset: 0x0004941C
		internal override string UniqueId
		{
			get
			{
				char[] array = new char[16];
				int length = 0;
				array[length++] = XPathNavigator.NodeTypeLetter[(int)this.pageCurrent[this.idxCurrent].NodeType];
				int num;
				if (this.idxParent != 0)
				{
					num = (this.pageParent[0].PageInfo.PageNumber - 1 << 16 | this.idxParent - 1);
					do
					{
						array[length++] = XPathNavigator.UniqueIdTbl[num & 31];
						num >>= 5;
					}
					while (num != 0);
					array[length++] = '0';
				}
				num = (this.pageCurrent[0].PageInfo.PageNumber - 1 << 16 | this.idxCurrent - 1);
				do
				{
					array[length++] = XPathNavigator.UniqueIdTbl[num & 31];
					num >>= 5;
				}
				while (num != 0);
				return new string(array, 0, length);
			}
		}

		// Token: 0x0600102E RID: 4142 RVA: 0x0004A4E9 File Offset: 0x000494E9
		public bool HasLineInfo()
		{
			return this.pageCurrent[this.idxCurrent].Document.HasLineInfo;
		}

		// Token: 0x170003D4 RID: 980
		// (get) Token: 0x0600102F RID: 4143 RVA: 0x0004A506 File Offset: 0x00049506
		public int LineNumber
		{
			get
			{
				if (this.idxParent != 0 && this.NodeType == XPathNodeType.Text)
				{
					return this.pageParent[this.idxParent].LineNumber;
				}
				return this.pageCurrent[this.idxCurrent].LineNumber;
			}
		}

		// Token: 0x170003D5 RID: 981
		// (get) Token: 0x06001030 RID: 4144 RVA: 0x0004A546 File Offset: 0x00049546
		public int LinePosition
		{
			get
			{
				if (this.idxParent != 0 && this.NodeType == XPathNodeType.Text)
				{
					return this.pageParent[this.idxParent].CollapsedLinePosition;
				}
				return this.pageCurrent[this.idxCurrent].LinePosition;
			}
		}

		// Token: 0x06001031 RID: 4145 RVA: 0x0004A586 File Offset: 0x00049586
		public int GetPositionHashCode()
		{
			return this.idxCurrent ^ this.idxParent;
		}

		// Token: 0x06001032 RID: 4146 RVA: 0x0004A598 File Offset: 0x00049598
		public bool IsElementMatch(string localName, string namespaceURI)
		{
			if (localName != this.atomizedLocalName)
			{
				this.atomizedLocalName = ((localName != null) ? this.NameTable.Get(localName) : null);
			}
			return this.idxParent == 0 && this.pageCurrent[this.idxCurrent].ElementMatch(this.atomizedLocalName, namespaceURI);
		}

		// Token: 0x06001033 RID: 4147 RVA: 0x0004A5ED File Offset: 0x000495ED
		public bool IsContentKindMatch(XPathNodeType typ)
		{
			return (1 << (int)this.pageCurrent[this.idxCurrent].NodeType & XPathNavigator.GetContentKindMask(typ)) != 0;
		}

		// Token: 0x06001034 RID: 4148 RVA: 0x0004A617 File Offset: 0x00049617
		public bool IsKindMatch(XPathNodeType typ)
		{
			return (1 << (int)this.pageCurrent[this.idxCurrent].NodeType & XPathNavigator.GetKindMask(typ)) != 0;
		}

		// Token: 0x06001035 RID: 4149 RVA: 0x0004A644 File Offset: 0x00049644
		private int GetFollowingEnd(XPathDocumentNavigator end, bool useParentOfVirtual, out XPathNode[] pageEnd)
		{
			if (end == null || this.pageCurrent[this.idxCurrent].Document != end.pageCurrent[end.idxCurrent].Document)
			{
				pageEnd = null;
				return 0;
			}
			if (end.idxParent == 0)
			{
				pageEnd = end.pageCurrent;
				return end.idxCurrent;
			}
			pageEnd = end.pageParent;
			if (!useParentOfVirtual)
			{
				return end.idxParent + 1;
			}
			return end.idxParent;
		}

		// Token: 0x04000A98 RID: 2712
		private XPathNode[] pageCurrent;

		// Token: 0x04000A99 RID: 2713
		private XPathNode[] pageParent;

		// Token: 0x04000A9A RID: 2714
		private int idxCurrent;

		// Token: 0x04000A9B RID: 2715
		private int idxParent;

		// Token: 0x04000A9C RID: 2716
		private string atomizedLocalName;
	}
}
