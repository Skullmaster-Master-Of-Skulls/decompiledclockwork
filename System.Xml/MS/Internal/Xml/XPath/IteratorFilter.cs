using System;
using System.Xml.XPath;

namespace MS.Internal.Xml.XPath
{
	// Token: 0x02000146 RID: 326
	internal class IteratorFilter : XPathNodeIterator
	{
		// Token: 0x0600124A RID: 4682 RVA: 0x0004FF23 File Offset: 0x0004EF23
		internal IteratorFilter(XPathNodeIterator innerIterator, string name)
		{
			this.innerIterator = innerIterator;
			this.name = name;
		}

		// Token: 0x0600124B RID: 4683 RVA: 0x0004FF39 File Offset: 0x0004EF39
		private IteratorFilter(IteratorFilter it)
		{
			this.innerIterator = it.innerIterator.Clone();
			this.name = it.name;
			this.position = it.position;
		}

		// Token: 0x0600124C RID: 4684 RVA: 0x0004FF6A File Offset: 0x0004EF6A
		public override XPathNodeIterator Clone()
		{
			return new IteratorFilter(this);
		}

		// Token: 0x1700047F RID: 1151
		// (get) Token: 0x0600124D RID: 4685 RVA: 0x0004FF72 File Offset: 0x0004EF72
		public override XPathNavigator Current
		{
			get
			{
				return this.innerIterator.Current;
			}
		}

		// Token: 0x17000480 RID: 1152
		// (get) Token: 0x0600124E RID: 4686 RVA: 0x0004FF7F File Offset: 0x0004EF7F
		public override int CurrentPosition
		{
			get
			{
				return this.position;
			}
		}

		// Token: 0x0600124F RID: 4687 RVA: 0x0004FF87 File Offset: 0x0004EF87
		public override bool MoveNext()
		{
			while (this.innerIterator.MoveNext())
			{
				if (this.innerIterator.Current.LocalName == this.name)
				{
					this.position++;
					return true;
				}
			}
			return false;
		}

		// Token: 0x04000B89 RID: 2953
		private XPathNodeIterator innerIterator;

		// Token: 0x04000B8A RID: 2954
		private string name;

		// Token: 0x04000B8B RID: 2955
		private int position;
	}
}
