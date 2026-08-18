using System;

namespace System.Xml.XPath
{
	// Token: 0x020002EE RID: 750
	internal class XmlEmptyNavigator : XPathNavigator
	{
		// Token: 0x06002D21 RID: 11553 RVA: 0x000EC00D File Offset: 0x000EA20D
		private XmlEmptyNavigator()
		{
		}

		// Token: 0x170009EC RID: 2540
		// (get) Token: 0x06002D22 RID: 11554 RVA: 0x000EC015 File Offset: 0x000EA215
		public static XmlEmptyNavigator Singleton
		{
			get
			{
				if (XmlEmptyNavigator.singleton == null)
				{
					XmlEmptyNavigator.singleton = new XmlEmptyNavigator();
				}
				return XmlEmptyNavigator.singleton;
			}
		}

		// Token: 0x170009ED RID: 2541
		// (get) Token: 0x06002D23 RID: 11555 RVA: 0x000EC033 File Offset: 0x000EA233
		public override XPathNodeType NodeType
		{
			get
			{
				return XPathNodeType.All;
			}
		}

		// Token: 0x170009EE RID: 2542
		// (get) Token: 0x06002D24 RID: 11556 RVA: 0x000EC037 File Offset: 0x000EA237
		public override string NamespaceURI
		{
			get
			{
				return string.Empty;
			}
		}

		// Token: 0x170009EF RID: 2543
		// (get) Token: 0x06002D25 RID: 11557 RVA: 0x000EC03E File Offset: 0x000EA23E
		public override string LocalName
		{
			get
			{
				return string.Empty;
			}
		}

		// Token: 0x170009F0 RID: 2544
		// (get) Token: 0x06002D26 RID: 11558 RVA: 0x000EC045 File Offset: 0x000EA245
		public override string Name
		{
			get
			{
				return string.Empty;
			}
		}

		// Token: 0x170009F1 RID: 2545
		// (get) Token: 0x06002D27 RID: 11559 RVA: 0x000EC04C File Offset: 0x000EA24C
		public override string Prefix
		{
			get
			{
				return string.Empty;
			}
		}

		// Token: 0x170009F2 RID: 2546
		// (get) Token: 0x06002D28 RID: 11560 RVA: 0x000EC053 File Offset: 0x000EA253
		public override string BaseURI
		{
			get
			{
				return string.Empty;
			}
		}

		// Token: 0x170009F3 RID: 2547
		// (get) Token: 0x06002D29 RID: 11561 RVA: 0x000EC05A File Offset: 0x000EA25A
		public override string Value
		{
			get
			{
				return string.Empty;
			}
		}

		// Token: 0x170009F4 RID: 2548
		// (get) Token: 0x06002D2A RID: 11562 RVA: 0x000EC061 File Offset: 0x000EA261
		public override bool IsEmptyElement
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170009F5 RID: 2549
		// (get) Token: 0x06002D2B RID: 11563 RVA: 0x000EC064 File Offset: 0x000EA264
		public override string XmlLang
		{
			get
			{
				return string.Empty;
			}
		}

		// Token: 0x170009F6 RID: 2550
		// (get) Token: 0x06002D2C RID: 11564 RVA: 0x000EC06B File Offset: 0x000EA26B
		public override bool HasAttributes
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170009F7 RID: 2551
		// (get) Token: 0x06002D2D RID: 11565 RVA: 0x000EC06E File Offset: 0x000EA26E
		public override bool HasChildren
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170009F8 RID: 2552
		// (get) Token: 0x06002D2E RID: 11566 RVA: 0x000EC071 File Offset: 0x000EA271
		public override XmlNameTable NameTable
		{
			get
			{
				return new NameTable();
			}
		}

		// Token: 0x06002D2F RID: 11567 RVA: 0x000EC078 File Offset: 0x000EA278
		public override bool MoveToFirstChild()
		{
			return false;
		}

		// Token: 0x06002D30 RID: 11568 RVA: 0x000EC07B File Offset: 0x000EA27B
		public override void MoveToRoot()
		{
		}

		// Token: 0x06002D31 RID: 11569 RVA: 0x000EC07D File Offset: 0x000EA27D
		public override bool MoveToNext()
		{
			return false;
		}

		// Token: 0x06002D32 RID: 11570 RVA: 0x000EC080 File Offset: 0x000EA280
		public override bool MoveToPrevious()
		{
			return false;
		}

		// Token: 0x06002D33 RID: 11571 RVA: 0x000EC083 File Offset: 0x000EA283
		public override bool MoveToFirst()
		{
			return false;
		}

		// Token: 0x06002D34 RID: 11572 RVA: 0x000EC086 File Offset: 0x000EA286
		public override bool MoveToFirstAttribute()
		{
			return false;
		}

		// Token: 0x06002D35 RID: 11573 RVA: 0x000EC089 File Offset: 0x000EA289
		public override bool MoveToNextAttribute()
		{
			return false;
		}

		// Token: 0x06002D36 RID: 11574 RVA: 0x000EC08C File Offset: 0x000EA28C
		public override bool MoveToId(string id)
		{
			return false;
		}

		// Token: 0x06002D37 RID: 11575 RVA: 0x000EC08F File Offset: 0x000EA28F
		public override string GetAttribute(string localName, string namespaceName)
		{
			return null;
		}

		// Token: 0x06002D38 RID: 11576 RVA: 0x000EC092 File Offset: 0x000EA292
		public override bool MoveToAttribute(string localName, string namespaceName)
		{
			return false;
		}

		// Token: 0x06002D39 RID: 11577 RVA: 0x000EC095 File Offset: 0x000EA295
		public override string GetNamespace(string name)
		{
			return null;
		}

		// Token: 0x06002D3A RID: 11578 RVA: 0x000EC098 File Offset: 0x000EA298
		public override bool MoveToNamespace(string prefix)
		{
			return false;
		}

		// Token: 0x06002D3B RID: 11579 RVA: 0x000EC09B File Offset: 0x000EA29B
		public override bool MoveToFirstNamespace(XPathNamespaceScope scope)
		{
			return false;
		}

		// Token: 0x06002D3C RID: 11580 RVA: 0x000EC09E File Offset: 0x000EA29E
		public override bool MoveToNextNamespace(XPathNamespaceScope scope)
		{
			return false;
		}

		// Token: 0x06002D3D RID: 11581 RVA: 0x000EC0A1 File Offset: 0x000EA2A1
		public override bool MoveToParent()
		{
			return false;
		}

		// Token: 0x06002D3E RID: 11582 RVA: 0x000EC0A4 File Offset: 0x000EA2A4
		public override bool MoveTo(XPathNavigator other)
		{
			return this == other;
		}

		// Token: 0x06002D3F RID: 11583 RVA: 0x000EC0AA File Offset: 0x000EA2AA
		public override XmlNodeOrder ComparePosition(XPathNavigator other)
		{
			if (this != other)
			{
				return XmlNodeOrder.Unknown;
			}
			return XmlNodeOrder.Same;
		}

		// Token: 0x06002D40 RID: 11584 RVA: 0x000EC0B3 File Offset: 0x000EA2B3
		public override bool IsSamePosition(XPathNavigator other)
		{
			return this == other;
		}

		// Token: 0x06002D41 RID: 11585 RVA: 0x000EC0B9 File Offset: 0x000EA2B9
		public override XPathNavigator Clone()
		{
			return this;
		}

		// Token: 0x04001379 RID: 4985
		private static volatile XmlEmptyNavigator singleton;
	}
}
