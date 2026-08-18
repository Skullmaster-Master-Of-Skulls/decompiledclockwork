using System;
using System.Xml.XPath;

namespace MS.Internal.Xml.XPath
{
	// Token: 0x02000169 RID: 361
	internal sealed class XPathEmptyIterator : ResetableIterator
	{
		// Token: 0x06001355 RID: 4949 RVA: 0x000537E8 File Offset: 0x000527E8
		private XPathEmptyIterator()
		{
		}

		// Token: 0x06001356 RID: 4950 RVA: 0x000537F0 File Offset: 0x000527F0
		public override XPathNodeIterator Clone()
		{
			return this;
		}

		// Token: 0x170004B2 RID: 1202
		// (get) Token: 0x06001357 RID: 4951 RVA: 0x000537F3 File Offset: 0x000527F3
		public override XPathNavigator Current
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170004B3 RID: 1203
		// (get) Token: 0x06001358 RID: 4952 RVA: 0x000537F6 File Offset: 0x000527F6
		public override int CurrentPosition
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x170004B4 RID: 1204
		// (get) Token: 0x06001359 RID: 4953 RVA: 0x000537F9 File Offset: 0x000527F9
		public override int Count
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x0600135A RID: 4954 RVA: 0x000537FC File Offset: 0x000527FC
		public override bool MoveNext()
		{
			return false;
		}

		// Token: 0x0600135B RID: 4955 RVA: 0x000537FF File Offset: 0x000527FF
		public override void Reset()
		{
		}

		// Token: 0x04000BF0 RID: 3056
		public static XPathEmptyIterator Instance = new XPathEmptyIterator();
	}
}
