using System;

namespace System.Xml.XPath
{
	// Token: 0x0200011D RID: 285
	internal class XmlEmptyNavigator : XPathNavigator
	{
		// Token: 0x06001109 RID: 4361 RVA: 0x0004D3AE File Offset: 0x0004C3AE
		private XmlEmptyNavigator()
		{
		}

		// Token: 0x17000424 RID: 1060
		// (get) Token: 0x0600110A RID: 4362 RVA: 0x0004D3B6 File Offset: 0x0004C3B6
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

		// Token: 0x17000425 RID: 1061
		// (get) Token: 0x0600110B RID: 4363 RVA: 0x0004D3CE File Offset: 0x0004C3CE
		public override XPathNodeType NodeType
		{
			get
			{
				return XPathNodeType.All;
			}
		}

		// Token: 0x17000426 RID: 1062
		// (get) Token: 0x0600110C RID: 4364 RVA: 0x0004D3D2 File Offset: 0x0004C3D2
		public override string NamespaceURI
		{
			get
			{
				return string.Empty;
			}
		}

		// Token: 0x17000427 RID: 1063
		// (get) Token: 0x0600110D RID: 4365 RVA: 0x0004D3D9 File Offset: 0x0004C3D9
		public override string LocalName
		{
			get
			{
				return string.Empty;
			}
		}

		// Token: 0x17000428 RID: 1064
		// (get) Token: 0x0600110E RID: 4366 RVA: 0x0004D3E0 File Offset: 0x0004C3E0
		public override string Name
		{
			get
			{
				return string.Empty;
			}
		}

		// Token: 0x17000429 RID: 1065
		// (get) Token: 0x0600110F RID: 4367 RVA: 0x0004D3E7 File Offset: 0x0004C3E7
		public override string Prefix
		{
			get
			{
				return string.Empty;
			}
		}

		// Token: 0x1700042A RID: 1066
		// (get) Token: 0x06001110 RID: 4368 RVA: 0x0004D3EE File Offset: 0x0004C3EE
		public override string BaseURI
		{
			get
			{
				return string.Empty;
			}
		}

		// Token: 0x1700042B RID: 1067
		// (get) Token: 0x06001111 RID: 4369 RVA: 0x0004D3F5 File Offset: 0x0004C3F5
		public override string Value
		{
			get
			{
				return string.Empty;
			}
		}

		// Token: 0x1700042C RID: 1068
		// (get) Token: 0x06001112 RID: 4370 RVA: 0x0004D3FC File Offset: 0x0004C3FC
		public override bool IsEmptyElement
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700042D RID: 1069
		// (get) Token: 0x06001113 RID: 4371 RVA: 0x0004D3FF File Offset: 0x0004C3FF
		public override string XmlLang
		{
			get
			{
				return string.Empty;
			}
		}

		// Token: 0x1700042E RID: 1070
		// (get) Token: 0x06001114 RID: 4372 RVA: 0x0004D406 File Offset: 0x0004C406
		public override bool HasAttributes
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700042F RID: 1071
		// (get) Token: 0x06001115 RID: 4373 RVA: 0x0004D409 File Offset: 0x0004C409
		public override bool HasChildren
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000430 RID: 1072
		// (get) Token: 0x06001116 RID: 4374 RVA: 0x0004D40C File Offset: 0x0004C40C
		public override XmlNameTable NameTable
		{
			get
			{
				return new NameTable();
			}
		}

		// Token: 0x06001117 RID: 4375 RVA: 0x0004D413 File Offset: 0x0004C413
		public override bool MoveToFirstChild()
		{
			return false;
		}

		// Token: 0x06001118 RID: 4376 RVA: 0x0004D416 File Offset: 0x0004C416
		public override void MoveToRoot()
		{
		}

		// Token: 0x06001119 RID: 4377 RVA: 0x0004D418 File Offset: 0x0004C418
		public override bool MoveToNext()
		{
			return false;
		}

		// Token: 0x0600111A RID: 4378 RVA: 0x0004D41B File Offset: 0x0004C41B
		public override bool MoveToPrevious()
		{
			return false;
		}

		// Token: 0x0600111B RID: 4379 RVA: 0x0004D41E File Offset: 0x0004C41E
		public override bool MoveToFirst()
		{
			return false;
		}

		// Token: 0x0600111C RID: 4380 RVA: 0x0004D421 File Offset: 0x0004C421
		public override bool MoveToFirstAttribute()
		{
			return false;
		}

		// Token: 0x0600111D RID: 4381 RVA: 0x0004D424 File Offset: 0x0004C424
		public override bool MoveToNextAttribute()
		{
			return false;
		}

		// Token: 0x0600111E RID: 4382 RVA: 0x0004D427 File Offset: 0x0004C427
		public override bool MoveToId(string id)
		{
			return false;
		}

		// Token: 0x0600111F RID: 4383 RVA: 0x0004D42A File Offset: 0x0004C42A
		public override string GetAttribute(string localName, string namespaceName)
		{
			return null;
		}

		// Token: 0x06001120 RID: 4384 RVA: 0x0004D42D File Offset: 0x0004C42D
		public override bool MoveToAttribute(string localName, string namespaceName)
		{
			return false;
		}

		// Token: 0x06001121 RID: 4385 RVA: 0x0004D430 File Offset: 0x0004C430
		public override string GetNamespace(string name)
		{
			return null;
		}

		// Token: 0x06001122 RID: 4386 RVA: 0x0004D433 File Offset: 0x0004C433
		public override bool MoveToNamespace(string prefix)
		{
			return false;
		}

		// Token: 0x06001123 RID: 4387 RVA: 0x0004D436 File Offset: 0x0004C436
		public override bool MoveToFirstNamespace(XPathNamespaceScope scope)
		{
			return false;
		}

		// Token: 0x06001124 RID: 4388 RVA: 0x0004D439 File Offset: 0x0004C439
		public override bool MoveToNextNamespace(XPathNamespaceScope scope)
		{
			return false;
		}

		// Token: 0x06001125 RID: 4389 RVA: 0x0004D43C File Offset: 0x0004C43C
		public override bool MoveToParent()
		{
			return false;
		}

		// Token: 0x06001126 RID: 4390 RVA: 0x0004D43F File Offset: 0x0004C43F
		public override bool MoveTo(XPathNavigator other)
		{
			return this == other;
		}

		// Token: 0x06001127 RID: 4391 RVA: 0x0004D445 File Offset: 0x0004C445
		public override XmlNodeOrder ComparePosition(XPathNavigator other)
		{
			if (this != other)
			{
				return XmlNodeOrder.Unknown;
			}
			return XmlNodeOrder.Same;
		}

		// Token: 0x06001128 RID: 4392 RVA: 0x0004D44E File Offset: 0x0004C44E
		public override bool IsSamePosition(XPathNavigator other)
		{
			return this == other;
		}

		// Token: 0x06001129 RID: 4393 RVA: 0x0004D454 File Offset: 0x0004C454
		public override XPathNavigator Clone()
		{
			return this;
		}

		// Token: 0x04000B07 RID: 2823
		private static XmlEmptyNavigator singleton;
	}
}
