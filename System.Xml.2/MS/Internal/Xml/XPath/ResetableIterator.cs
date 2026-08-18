using System;
using System.Xml.XPath;

namespace MS.Internal.Xml.XPath
{
	// Token: 0x02000036 RID: 54
	internal abstract class ResetableIterator : XPathNodeIterator
	{
		// Token: 0x060001A2 RID: 418 RVA: 0x00006E34 File Offset: 0x00005034
		public ResetableIterator()
		{
			this.count = -1;
		}

		// Token: 0x060001A3 RID: 419 RVA: 0x00006E43 File Offset: 0x00005043
		protected ResetableIterator(ResetableIterator other)
		{
			this.count = other.count;
		}

		// Token: 0x060001A4 RID: 420 RVA: 0x00006E57 File Offset: 0x00005057
		protected void ResetCount()
		{
			this.count = -1;
		}

		// Token: 0x060001A5 RID: 421
		public abstract void Reset();

		// Token: 0x060001A6 RID: 422 RVA: 0x00006E60 File Offset: 0x00005060
		public virtual bool MoveToPosition(int pos)
		{
			this.Reset();
			for (int i = this.CurrentPosition; i < pos; i++)
			{
				if (!this.MoveNext())
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x060001A7 RID: 423
		public abstract override int CurrentPosition { get; }
	}
}
