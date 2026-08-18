using System;
using System.Xml.XPath;

namespace System.Xml
{
	// Token: 0x020000C0 RID: 192
	internal sealed class DocumentXPathNodeIterator_Empty : XPathNodeIterator
	{
		// Token: 0x06000B65 RID: 2917 RVA: 0x00034DF9 File Offset: 0x00033DF9
		internal DocumentXPathNodeIterator_Empty(DocumentXPathNavigator nav)
		{
			this.nav = nav.Clone();
		}

		// Token: 0x06000B66 RID: 2918 RVA: 0x00034E0D File Offset: 0x00033E0D
		internal DocumentXPathNodeIterator_Empty(DocumentXPathNodeIterator_Empty other)
		{
			this.nav = other.nav.Clone();
		}

		// Token: 0x06000B67 RID: 2919 RVA: 0x00034E26 File Offset: 0x00033E26
		public override XPathNodeIterator Clone()
		{
			return new DocumentXPathNodeIterator_Empty(this);
		}

		// Token: 0x06000B68 RID: 2920 RVA: 0x00034E2E File Offset: 0x00033E2E
		public override bool MoveNext()
		{
			return false;
		}

		// Token: 0x17000278 RID: 632
		// (get) Token: 0x06000B69 RID: 2921 RVA: 0x00034E31 File Offset: 0x00033E31
		public override XPathNavigator Current
		{
			get
			{
				return this.nav;
			}
		}

		// Token: 0x17000279 RID: 633
		// (get) Token: 0x06000B6A RID: 2922 RVA: 0x00034E39 File Offset: 0x00033E39
		public override int CurrentPosition
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x1700027A RID: 634
		// (get) Token: 0x06000B6B RID: 2923 RVA: 0x00034E3C File Offset: 0x00033E3C
		public override int Count
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x040008DF RID: 2271
		private XPathNavigator nav;
	}
}
