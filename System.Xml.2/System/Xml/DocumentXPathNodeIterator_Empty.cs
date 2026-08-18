using System;
using System.Xml.XPath;

namespace System.Xml
{
	// Token: 0x020000F1 RID: 241
	internal sealed class DocumentXPathNodeIterator_Empty : XPathNodeIterator
	{
		// Token: 0x06001103 RID: 4355 RVA: 0x00048620 File Offset: 0x00046820
		internal DocumentXPathNodeIterator_Empty(DocumentXPathNavigator nav)
		{
			this.nav = nav.Clone();
		}

		// Token: 0x06001104 RID: 4356 RVA: 0x00048634 File Offset: 0x00046834
		internal DocumentXPathNodeIterator_Empty(DocumentXPathNodeIterator_Empty other)
		{
			this.nav = other.nav.Clone();
		}

		// Token: 0x06001105 RID: 4357 RVA: 0x0004864D File Offset: 0x0004684D
		public override XPathNodeIterator Clone()
		{
			return new DocumentXPathNodeIterator_Empty(this);
		}

		// Token: 0x06001106 RID: 4358 RVA: 0x00048655 File Offset: 0x00046855
		public override bool MoveNext()
		{
			return false;
		}

		// Token: 0x17000337 RID: 823
		// (get) Token: 0x06001107 RID: 4359 RVA: 0x00048658 File Offset: 0x00046858
		public override XPathNavigator Current
		{
			get
			{
				return this.nav;
			}
		}

		// Token: 0x17000338 RID: 824
		// (get) Token: 0x06001108 RID: 4360 RVA: 0x00048660 File Offset: 0x00046860
		public override int CurrentPosition
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17000339 RID: 825
		// (get) Token: 0x06001109 RID: 4361 RVA: 0x00048663 File Offset: 0x00046863
		public override int Count
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x040004C2 RID: 1218
		private XPathNavigator nav;
	}
}
