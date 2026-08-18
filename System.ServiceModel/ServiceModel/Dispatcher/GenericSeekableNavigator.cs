using System;
using System.Xml;
using System.Xml.XPath;

namespace System.ServiceModel.Dispatcher
{
	// Token: 0x020004F3 RID: 1267
	internal class GenericSeekableNavigator : SeekableXPathNavigator
	{
		// Token: 0x06002FFE RID: 12286 RVA: 0x000B7C53 File Offset: 0x000B5E53
		internal GenericSeekableNavigator(XPathNavigator navigator)
		{
			this.navigator = navigator;
			this.nodes = new QueryBuffer<XPathNavigator>(4);
			this.currentPosition = -1L;
			this.dom = this;
		}

		// Token: 0x06002FFF RID: 12287 RVA: 0x000B7C7D File Offset: 0x000B5E7D
		internal GenericSeekableNavigator(GenericSeekableNavigator navigator)
		{
			this.navigator = navigator.navigator.Clone();
			this.nodes = default(QueryBuffer<XPathNavigator>);
			this.currentPosition = navigator.currentPosition;
			this.dom = navigator.dom;
		}

		// Token: 0x17000B62 RID: 2914
		// (get) Token: 0x06003000 RID: 12288 RVA: 0x000B7CBA File Offset: 0x000B5EBA
		public override string BaseURI
		{
			get
			{
				return this.navigator.BaseURI;
			}
		}

		// Token: 0x17000B63 RID: 2915
		// (get) Token: 0x06003001 RID: 12289 RVA: 0x000B7CC7 File Offset: 0x000B5EC7
		public override bool HasAttributes
		{
			get
			{
				return this.navigator.HasAttributes;
			}
		}

		// Token: 0x17000B64 RID: 2916
		// (get) Token: 0x06003002 RID: 12290 RVA: 0x000B7CD4 File Offset: 0x000B5ED4
		public override bool HasChildren
		{
			get
			{
				return this.navigator.HasChildren;
			}
		}

		// Token: 0x17000B65 RID: 2917
		// (get) Token: 0x06003003 RID: 12291 RVA: 0x000B7CE1 File Offset: 0x000B5EE1
		public override bool IsEmptyElement
		{
			get
			{
				return this.navigator.IsEmptyElement;
			}
		}

		// Token: 0x17000B66 RID: 2918
		// (get) Token: 0x06003004 RID: 12292 RVA: 0x000B7CEE File Offset: 0x000B5EEE
		public override string LocalName
		{
			get
			{
				return this.navigator.LocalName;
			}
		}

		// Token: 0x17000B67 RID: 2919
		// (get) Token: 0x06003005 RID: 12293 RVA: 0x000B7CFB File Offset: 0x000B5EFB
		public override string Name
		{
			get
			{
				return this.navigator.Name;
			}
		}

		// Token: 0x17000B68 RID: 2920
		// (get) Token: 0x06003006 RID: 12294 RVA: 0x000B7D08 File Offset: 0x000B5F08
		public override string NamespaceURI
		{
			get
			{
				return this.navigator.NamespaceURI;
			}
		}

		// Token: 0x17000B69 RID: 2921
		// (get) Token: 0x06003007 RID: 12295 RVA: 0x000B7D15 File Offset: 0x000B5F15
		public override XmlNameTable NameTable
		{
			get
			{
				return this.navigator.NameTable;
			}
		}

		// Token: 0x17000B6A RID: 2922
		// (get) Token: 0x06003008 RID: 12296 RVA: 0x000B7D22 File Offset: 0x000B5F22
		public override XPathNodeType NodeType
		{
			get
			{
				return this.navigator.NodeType;
			}
		}

		// Token: 0x17000B6B RID: 2923
		// (get) Token: 0x06003009 RID: 12297 RVA: 0x000B7D2F File Offset: 0x000B5F2F
		public override string Prefix
		{
			get
			{
				return this.navigator.Prefix;
			}
		}

		// Token: 0x17000B6C RID: 2924
		// (get) Token: 0x0600300A RID: 12298 RVA: 0x000B7D3C File Offset: 0x000B5F3C
		public override string Value
		{
			get
			{
				return this.navigator.Value;
			}
		}

		// Token: 0x17000B6D RID: 2925
		// (get) Token: 0x0600300B RID: 12299 RVA: 0x000B7D49 File Offset: 0x000B5F49
		public override string XmlLang
		{
			get
			{
				return this.navigator.XmlLang;
			}
		}

		// Token: 0x17000B6E RID: 2926
		// (get) Token: 0x0600300C RID: 12300 RVA: 0x000B7D56 File Offset: 0x000B5F56
		// (set) Token: 0x0600300D RID: 12301 RVA: 0x000B7D6E File Offset: 0x000B5F6E
		public override long CurrentPosition
		{
			get
			{
				if (-1L == this.currentPosition)
				{
					this.SnapshotNavigator();
				}
				return this.currentPosition;
			}
			set
			{
				this.navigator.MoveTo(this[value]);
				this.currentPosition = value;
			}
		}

		// Token: 0x17000B6F RID: 2927
		internal XPathNavigator this[long nodePosition]
		{
			get
			{
				int index = (int)nodePosition;
				return this.dom.nodes[index];
			}
		}

		// Token: 0x0600300F RID: 12303 RVA: 0x000B7DAD File Offset: 0x000B5FAD
		public override XPathNavigator Clone()
		{
			return new GenericSeekableNavigator(this);
		}

		// Token: 0x06003010 RID: 12304 RVA: 0x000B7DB8 File Offset: 0x000B5FB8
		public override XmlNodeOrder ComparePosition(XPathNavigator navigator)
		{
			if (navigator == null)
			{
				return XmlNodeOrder.Unknown;
			}
			GenericSeekableNavigator genericSeekableNavigator = navigator as GenericSeekableNavigator;
			if (genericSeekableNavigator != null)
			{
				return this.navigator.ComparePosition(genericSeekableNavigator.navigator);
			}
			return XmlNodeOrder.Unknown;
		}

		// Token: 0x06003011 RID: 12305 RVA: 0x000B7DE8 File Offset: 0x000B5FE8
		public override XmlNodeOrder ComparePosition(long x, long y)
		{
			XPathNavigator xpathNavigator = this[x];
			XPathNavigator nav = this[y];
			return xpathNavigator.ComparePosition(nav);
		}

		// Token: 0x06003012 RID: 12306 RVA: 0x000B7E0C File Offset: 0x000B600C
		public override string GetLocalName(long nodePosition)
		{
			return this[nodePosition].LocalName;
		}

		// Token: 0x06003013 RID: 12307 RVA: 0x000B7E1A File Offset: 0x000B601A
		public override string GetName(long nodePosition)
		{
			return this[nodePosition].Name;
		}

		// Token: 0x06003014 RID: 12308 RVA: 0x000B7E28 File Offset: 0x000B6028
		public override string GetNamespace(long nodePosition)
		{
			return this[nodePosition].NamespaceURI;
		}

		// Token: 0x06003015 RID: 12309 RVA: 0x000B7E36 File Offset: 0x000B6036
		public override XPathNodeType GetNodeType(long nodePosition)
		{
			return this[nodePosition].NodeType;
		}

		// Token: 0x06003016 RID: 12310 RVA: 0x000B7E44 File Offset: 0x000B6044
		public override string GetValue(long nodePosition)
		{
			return this[nodePosition].Value;
		}

		// Token: 0x06003017 RID: 12311 RVA: 0x000B7E52 File Offset: 0x000B6052
		public override string GetNamespace(string name)
		{
			return this.navigator.GetNamespace(name);
		}

		// Token: 0x06003018 RID: 12312 RVA: 0x000B7E60 File Offset: 0x000B6060
		public override string GetAttribute(string localName, string namespaceURI)
		{
			return this.navigator.GetAttribute(localName, namespaceURI);
		}

		// Token: 0x06003019 RID: 12313 RVA: 0x000B7E70 File Offset: 0x000B6070
		public override bool IsDescendant(XPathNavigator navigator)
		{
			if (navigator == null)
			{
				return false;
			}
			GenericSeekableNavigator genericSeekableNavigator = navigator as GenericSeekableNavigator;
			return genericSeekableNavigator != null && this.navigator.IsDescendant(genericSeekableNavigator.navigator);
		}

		// Token: 0x0600301A RID: 12314 RVA: 0x000B7EA0 File Offset: 0x000B60A0
		public override bool IsSamePosition(XPathNavigator other)
		{
			GenericSeekableNavigator genericSeekableNavigator = other as GenericSeekableNavigator;
			return genericSeekableNavigator != null && this.navigator.IsSamePosition(genericSeekableNavigator.navigator);
		}

		// Token: 0x0600301B RID: 12315 RVA: 0x000B7ECA File Offset: 0x000B60CA
		public override void MoveToRoot()
		{
			this.currentPosition = -1L;
			this.navigator.MoveToRoot();
		}

		// Token: 0x0600301C RID: 12316 RVA: 0x000B7EDF File Offset: 0x000B60DF
		public override bool MoveToNextNamespace(XPathNamespaceScope namespaceScope)
		{
			this.currentPosition = -1L;
			return this.navigator.MoveToNextNamespace(namespaceScope);
		}

		// Token: 0x0600301D RID: 12317 RVA: 0x000B7EF5 File Offset: 0x000B60F5
		public override bool MoveToNextAttribute()
		{
			this.currentPosition = -1L;
			return this.navigator.MoveToNextAttribute();
		}

		// Token: 0x0600301E RID: 12318 RVA: 0x000B7F0A File Offset: 0x000B610A
		public override bool MoveToPrevious()
		{
			this.currentPosition = -1L;
			return this.navigator.MoveToPrevious();
		}

		// Token: 0x0600301F RID: 12319 RVA: 0x000B7F1F File Offset: 0x000B611F
		public override bool MoveToFirstAttribute()
		{
			this.currentPosition = -1L;
			return this.navigator.MoveToFirstAttribute();
		}

		// Token: 0x06003020 RID: 12320 RVA: 0x000B7F34 File Offset: 0x000B6134
		public override bool MoveToNamespace(string name)
		{
			this.currentPosition = -1L;
			return this.navigator.MoveToNamespace(name);
		}

		// Token: 0x06003021 RID: 12321 RVA: 0x000B7F4A File Offset: 0x000B614A
		public override bool MoveToParent()
		{
			this.currentPosition = -1L;
			return this.navigator.MoveToParent();
		}

		// Token: 0x06003022 RID: 12322 RVA: 0x000B7F60 File Offset: 0x000B6160
		public override bool MoveTo(XPathNavigator other)
		{
			GenericSeekableNavigator genericSeekableNavigator = other as GenericSeekableNavigator;
			if (genericSeekableNavigator != null && this.navigator.MoveTo(genericSeekableNavigator.navigator))
			{
				this.currentPosition = genericSeekableNavigator.currentPosition;
				return true;
			}
			return false;
		}

		// Token: 0x06003023 RID: 12323 RVA: 0x000B7F99 File Offset: 0x000B6199
		public override bool MoveToId(string id)
		{
			this.currentPosition = -1L;
			return this.navigator.MoveToId(id);
		}

		// Token: 0x06003024 RID: 12324 RVA: 0x000B7FAF File Offset: 0x000B61AF
		public override bool MoveToFirstChild()
		{
			this.currentPosition = -1L;
			return this.navigator.MoveToFirstChild();
		}

		// Token: 0x06003025 RID: 12325 RVA: 0x000B7FC4 File Offset: 0x000B61C4
		public override bool MoveToFirstNamespace(XPathNamespaceScope namespaceScope)
		{
			this.currentPosition = -1L;
			return this.navigator.MoveToFirstNamespace(namespaceScope);
		}

		// Token: 0x06003026 RID: 12326 RVA: 0x000B7FDA File Offset: 0x000B61DA
		public override bool MoveToAttribute(string localName, string namespaceURI)
		{
			this.currentPosition = -1L;
			return this.navigator.MoveToAttribute(localName, namespaceURI);
		}

		// Token: 0x06003027 RID: 12327 RVA: 0x000B7FF1 File Offset: 0x000B61F1
		public override bool MoveToNext()
		{
			this.currentPosition = -1L;
			return this.navigator.MoveToNext();
		}

		// Token: 0x06003028 RID: 12328 RVA: 0x000B8006 File Offset: 0x000B6206
		public override bool MoveToFirst()
		{
			this.currentPosition = -1L;
			return this.navigator.MoveToFirst();
		}

		// Token: 0x06003029 RID: 12329 RVA: 0x000B801B File Offset: 0x000B621B
		internal void SnapshotNavigator()
		{
			this.currentPosition = (long)this.dom.nodes.Count;
			this.dom.nodes.Add(this.navigator.Clone());
		}

		// Token: 0x040025ED RID: 9709
		private QueryBuffer<XPathNavigator> nodes;

		// Token: 0x040025EE RID: 9710
		private long currentPosition;

		// Token: 0x040025EF RID: 9711
		private XPathNavigator navigator;

		// Token: 0x040025F0 RID: 9712
		private GenericSeekableNavigator dom;
	}
}
