using System;
using System.Xml.XPath;

namespace MS.Internal.Xml.XPath
{
	// Token: 0x02000025 RID: 37
	internal class IteratorFilter : XPathNodeIterator
	{
		// Token: 0x060000F5 RID: 245 RVA: 0x000046E7 File Offset: 0x000028E7
		internal IteratorFilter(XPathNodeIterator innerIterator, string name)
		{
			this.innerIterator = innerIterator;
			this.name = name;
		}

		// Token: 0x060000F6 RID: 246 RVA: 0x000046FD File Offset: 0x000028FD
		private IteratorFilter(IteratorFilter it)
		{
			this.innerIterator = it.innerIterator.Clone();
			this.name = it.name;
			this.position = it.position;
		}

		// Token: 0x060000F7 RID: 247 RVA: 0x0000472E File Offset: 0x0000292E
		public override XPathNodeIterator Clone()
		{
			return new IteratorFilter(this);
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x060000F8 RID: 248 RVA: 0x00004736 File Offset: 0x00002936
		public override XPathNavigator Current
		{
			get
			{
				return this.innerIterator.Current;
			}
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x060000F9 RID: 249 RVA: 0x00004743 File Offset: 0x00002943
		public override int CurrentPosition
		{
			get
			{
				return this.position;
			}
		}

		// Token: 0x060000FA RID: 250 RVA: 0x0000474B File Offset: 0x0000294B
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

		// Token: 0x04000092 RID: 146
		private XPathNodeIterator innerIterator;

		// Token: 0x04000093 RID: 147
		private string name;

		// Token: 0x04000094 RID: 148
		private int position;
	}
}
