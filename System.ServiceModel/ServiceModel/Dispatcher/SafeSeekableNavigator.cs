using System;
using System.Xml;
using System.Xml.XPath;

namespace System.ServiceModel.Dispatcher
{
	// Token: 0x020004F2 RID: 1266
	internal class SafeSeekableNavigator : SeekableXPathNavigator, INodeCounter
	{
		// Token: 0x06002FCD RID: 12237 RVA: 0x000B7841 File Offset: 0x000B5A41
		internal SafeSeekableNavigator(SafeSeekableNavigator nav)
		{
			this.navigator = (SeekableXPathNavigator)nav.navigator.Clone();
			this.counter = nav.counter;
		}

		// Token: 0x06002FCE RID: 12238 RVA: 0x000B786B File Offset: 0x000B5A6B
		internal SafeSeekableNavigator(SeekableXPathNavigator navigator, int nodeCountMax)
		{
			this.navigator = navigator;
			this.counter = this;
			this.nodeCount = nodeCountMax;
			this.nodeCountMax = nodeCountMax;
		}

		// Token: 0x17000B53 RID: 2899
		// (get) Token: 0x06002FCF RID: 12239 RVA: 0x000B788F File Offset: 0x000B5A8F
		public override string BaseURI
		{
			get
			{
				return this.navigator.BaseURI;
			}
		}

		// Token: 0x17000B54 RID: 2900
		// (get) Token: 0x06002FD0 RID: 12240 RVA: 0x000B789C File Offset: 0x000B5A9C
		// (set) Token: 0x06002FD1 RID: 12241 RVA: 0x000B78A9 File Offset: 0x000B5AA9
		public int CounterMarker
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

		// Token: 0x17000B55 RID: 2901
		// (set) Token: 0x06002FD2 RID: 12242 RVA: 0x000B78B7 File Offset: 0x000B5AB7
		public int MaxCounter
		{
			set
			{
				this.counter.nodeCountMax = value;
			}
		}

		// Token: 0x17000B56 RID: 2902
		// (get) Token: 0x06002FD3 RID: 12243 RVA: 0x000B78C5 File Offset: 0x000B5AC5
		// (set) Token: 0x06002FD4 RID: 12244 RVA: 0x000B78D2 File Offset: 0x000B5AD2
		public override long CurrentPosition
		{
			get
			{
				return this.navigator.CurrentPosition;
			}
			set
			{
				this.navigator.CurrentPosition = value;
			}
		}

		// Token: 0x17000B57 RID: 2903
		// (get) Token: 0x06002FD5 RID: 12245 RVA: 0x000B78E0 File Offset: 0x000B5AE0
		public override bool HasAttributes
		{
			get
			{
				return this.navigator.HasAttributes;
			}
		}

		// Token: 0x17000B58 RID: 2904
		// (get) Token: 0x06002FD6 RID: 12246 RVA: 0x000B78ED File Offset: 0x000B5AED
		public override bool HasChildren
		{
			get
			{
				return this.navigator.HasChildren;
			}
		}

		// Token: 0x17000B59 RID: 2905
		// (get) Token: 0x06002FD7 RID: 12247 RVA: 0x000B78FA File Offset: 0x000B5AFA
		public override bool IsEmptyElement
		{
			get
			{
				return this.navigator.IsEmptyElement;
			}
		}

		// Token: 0x17000B5A RID: 2906
		// (get) Token: 0x06002FD8 RID: 12248 RVA: 0x000B7907 File Offset: 0x000B5B07
		public override string LocalName
		{
			get
			{
				return this.navigator.LocalName;
			}
		}

		// Token: 0x17000B5B RID: 2907
		// (get) Token: 0x06002FD9 RID: 12249 RVA: 0x000B7914 File Offset: 0x000B5B14
		public override string Name
		{
			get
			{
				return this.navigator.Name;
			}
		}

		// Token: 0x17000B5C RID: 2908
		// (get) Token: 0x06002FDA RID: 12250 RVA: 0x000B7921 File Offset: 0x000B5B21
		public override string NamespaceURI
		{
			get
			{
				return this.navigator.NamespaceURI;
			}
		}

		// Token: 0x17000B5D RID: 2909
		// (get) Token: 0x06002FDB RID: 12251 RVA: 0x000B792E File Offset: 0x000B5B2E
		public override XmlNameTable NameTable
		{
			get
			{
				return this.navigator.NameTable;
			}
		}

		// Token: 0x17000B5E RID: 2910
		// (get) Token: 0x06002FDC RID: 12252 RVA: 0x000B793B File Offset: 0x000B5B3B
		public override XPathNodeType NodeType
		{
			get
			{
				return this.navigator.NodeType;
			}
		}

		// Token: 0x17000B5F RID: 2911
		// (get) Token: 0x06002FDD RID: 12253 RVA: 0x000B7948 File Offset: 0x000B5B48
		public override string Prefix
		{
			get
			{
				return this.navigator.Prefix;
			}
		}

		// Token: 0x17000B60 RID: 2912
		// (get) Token: 0x06002FDE RID: 12254 RVA: 0x000B7955 File Offset: 0x000B5B55
		public override string Value
		{
			get
			{
				return this.navigator.Value;
			}
		}

		// Token: 0x17000B61 RID: 2913
		// (get) Token: 0x06002FDF RID: 12255 RVA: 0x000B7962 File Offset: 0x000B5B62
		public override string XmlLang
		{
			get
			{
				return this.navigator.XmlLang;
			}
		}

		// Token: 0x06002FE0 RID: 12256 RVA: 0x000B796F File Offset: 0x000B5B6F
		public override XPathNavigator Clone()
		{
			return new SafeSeekableNavigator(this);
		}

		// Token: 0x06002FE1 RID: 12257 RVA: 0x000B7978 File Offset: 0x000B5B78
		public override XmlNodeOrder ComparePosition(XPathNavigator navigator)
		{
			if (navigator == null)
			{
				return XmlNodeOrder.Unknown;
			}
			SafeSeekableNavigator safeSeekableNavigator = navigator as SafeSeekableNavigator;
			if (safeSeekableNavigator != null)
			{
				return this.navigator.ComparePosition(safeSeekableNavigator.navigator);
			}
			return XmlNodeOrder.Unknown;
		}

		// Token: 0x06002FE2 RID: 12258 RVA: 0x000B79A7 File Offset: 0x000B5BA7
		public override XmlNodeOrder ComparePosition(long x, long y)
		{
			return this.navigator.ComparePosition(x, y);
		}

		// Token: 0x06002FE3 RID: 12259 RVA: 0x000B79B6 File Offset: 0x000B5BB6
		public int ElapsedCount(int marker)
		{
			return marker - this.counter.nodeCount;
		}

		// Token: 0x06002FE4 RID: 12260 RVA: 0x000B79C5 File Offset: 0x000B5BC5
		public override string GetLocalName(long nodePosition)
		{
			return this.navigator.GetLocalName(nodePosition);
		}

		// Token: 0x06002FE5 RID: 12261 RVA: 0x000B79D3 File Offset: 0x000B5BD3
		public override string GetName(long nodePosition)
		{
			return this.navigator.GetName(nodePosition);
		}

		// Token: 0x06002FE6 RID: 12262 RVA: 0x000B79E1 File Offset: 0x000B5BE1
		public override string GetNamespace(long nodePosition)
		{
			return this.navigator.GetNamespace(nodePosition);
		}

		// Token: 0x06002FE7 RID: 12263 RVA: 0x000B79EF File Offset: 0x000B5BEF
		public override XPathNodeType GetNodeType(long nodePosition)
		{
			return this.navigator.GetNodeType(nodePosition);
		}

		// Token: 0x06002FE8 RID: 12264 RVA: 0x000B79FD File Offset: 0x000B5BFD
		public override string GetValue(long nodePosition)
		{
			return this.navigator.GetValue(nodePosition);
		}

		// Token: 0x06002FE9 RID: 12265 RVA: 0x000B7A0B File Offset: 0x000B5C0B
		public override string GetNamespace(string name)
		{
			this.IncrementNodeCount();
			return this.navigator.GetNamespace(name);
		}

		// Token: 0x06002FEA RID: 12266 RVA: 0x000B7A1F File Offset: 0x000B5C1F
		public override string GetAttribute(string localName, string namespaceURI)
		{
			this.IncrementNodeCount();
			return this.navigator.GetAttribute(localName, namespaceURI);
		}

		// Token: 0x06002FEB RID: 12267 RVA: 0x000B7A34 File Offset: 0x000B5C34
		public void Increase()
		{
			this.IncrementNodeCount();
		}

		// Token: 0x06002FEC RID: 12268 RVA: 0x000B7A3C File Offset: 0x000B5C3C
		public void IncreaseBy(int count)
		{
			this.counter.nodeCount -= count - 1;
			this.Increase();
		}

		// Token: 0x06002FED RID: 12269 RVA: 0x000B7A5C File Offset: 0x000B5C5C
		internal void IncrementNodeCount()
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

		// Token: 0x06002FEE RID: 12270 RVA: 0x000B7AC0 File Offset: 0x000B5CC0
		public override bool IsDescendant(XPathNavigator navigator)
		{
			if (navigator == null)
			{
				return false;
			}
			SafeSeekableNavigator safeSeekableNavigator = navigator as SafeSeekableNavigator;
			return safeSeekableNavigator != null && this.navigator.IsDescendant(safeSeekableNavigator.navigator);
		}

		// Token: 0x06002FEF RID: 12271 RVA: 0x000B7AF0 File Offset: 0x000B5CF0
		public override bool IsSamePosition(XPathNavigator other)
		{
			if (other == null)
			{
				return false;
			}
			SafeSeekableNavigator safeSeekableNavigator = other as SafeSeekableNavigator;
			return safeSeekableNavigator != null && this.navigator.IsSamePosition(safeSeekableNavigator.navigator);
		}

		// Token: 0x06002FF0 RID: 12272 RVA: 0x000B7B1F File Offset: 0x000B5D1F
		public override void MoveToRoot()
		{
			this.IncrementNodeCount();
			this.navigator.MoveToRoot();
		}

		// Token: 0x06002FF1 RID: 12273 RVA: 0x000B7B32 File Offset: 0x000B5D32
		public override bool MoveToNextNamespace(XPathNamespaceScope namespaceScope)
		{
			this.IncrementNodeCount();
			return this.navigator.MoveToNextNamespace(namespaceScope);
		}

		// Token: 0x06002FF2 RID: 12274 RVA: 0x000B7B46 File Offset: 0x000B5D46
		public override bool MoveToNextAttribute()
		{
			this.IncrementNodeCount();
			return this.navigator.MoveToNextAttribute();
		}

		// Token: 0x06002FF3 RID: 12275 RVA: 0x000B7B59 File Offset: 0x000B5D59
		public override bool MoveToPrevious()
		{
			this.IncrementNodeCount();
			return this.navigator.MoveToPrevious();
		}

		// Token: 0x06002FF4 RID: 12276 RVA: 0x000B7B6C File Offset: 0x000B5D6C
		public override bool MoveToFirstAttribute()
		{
			this.IncrementNodeCount();
			return this.navigator.MoveToFirstAttribute();
		}

		// Token: 0x06002FF5 RID: 12277 RVA: 0x000B7B7F File Offset: 0x000B5D7F
		public override bool MoveToNamespace(string name)
		{
			this.IncrementNodeCount();
			return this.navigator.MoveToNamespace(name);
		}

		// Token: 0x06002FF6 RID: 12278 RVA: 0x000B7B93 File Offset: 0x000B5D93
		public override bool MoveToParent()
		{
			this.IncrementNodeCount();
			return this.navigator.MoveToParent();
		}

		// Token: 0x06002FF7 RID: 12279 RVA: 0x000B7BA8 File Offset: 0x000B5DA8
		public override bool MoveTo(XPathNavigator other)
		{
			if (other == null)
			{
				return false;
			}
			this.IncrementNodeCount();
			SafeSeekableNavigator safeSeekableNavigator = other as SafeSeekableNavigator;
			return safeSeekableNavigator != null && this.navigator.MoveTo(safeSeekableNavigator.navigator);
		}

		// Token: 0x06002FF8 RID: 12280 RVA: 0x000B7BDD File Offset: 0x000B5DDD
		public override bool MoveToId(string id)
		{
			this.IncrementNodeCount();
			return this.navigator.MoveToId(id);
		}

		// Token: 0x06002FF9 RID: 12281 RVA: 0x000B7BF1 File Offset: 0x000B5DF1
		public override bool MoveToFirstChild()
		{
			this.IncrementNodeCount();
			return this.navigator.MoveToFirstChild();
		}

		// Token: 0x06002FFA RID: 12282 RVA: 0x000B7C04 File Offset: 0x000B5E04
		public override bool MoveToFirstNamespace(XPathNamespaceScope namespaceScope)
		{
			this.IncrementNodeCount();
			return this.navigator.MoveToFirstNamespace(namespaceScope);
		}

		// Token: 0x06002FFB RID: 12283 RVA: 0x000B7C18 File Offset: 0x000B5E18
		public override bool MoveToAttribute(string localName, string namespaceURI)
		{
			this.IncrementNodeCount();
			return this.navigator.MoveToAttribute(localName, namespaceURI);
		}

		// Token: 0x06002FFC RID: 12284 RVA: 0x000B7C2D File Offset: 0x000B5E2D
		public override bool MoveToNext()
		{
			this.IncrementNodeCount();
			return this.navigator.MoveToNext();
		}

		// Token: 0x06002FFD RID: 12285 RVA: 0x000B7C40 File Offset: 0x000B5E40
		public override bool MoveToFirst()
		{
			this.IncrementNodeCount();
			return this.navigator.MoveToFirst();
		}

		// Token: 0x040025E9 RID: 9705
		private SeekableXPathNavigator navigator;

		// Token: 0x040025EA RID: 9706
		private SafeSeekableNavigator counter;

		// Token: 0x040025EB RID: 9707
		private int nodeCount;

		// Token: 0x040025EC RID: 9708
		private int nodeCountMax;
	}
}
