using System;
using System.Xml.XPath;

namespace MS.Internal.Xml.XPath
{
	// Token: 0x0200011F RID: 287
	internal abstract class ResetableIterator : XPathNodeIterator
	{
		// Token: 0x0600112A RID: 4394 RVA: 0x0004D457 File Offset: 0x0004C457
		public ResetableIterator()
		{
			this.count = -1;
		}

		// Token: 0x0600112B RID: 4395 RVA: 0x0004D466 File Offset: 0x0004C466
		protected ResetableIterator(ResetableIterator other)
		{
			this.count = other.count;
		}

		// Token: 0x0600112C RID: 4396 RVA: 0x0004D47A File Offset: 0x0004C47A
		protected void ResetCount()
		{
			this.count = -1;
		}

		// Token: 0x0600112D RID: 4397
		public abstract void Reset();

		// Token: 0x0600112E RID: 4398 RVA: 0x0004D484 File Offset: 0x0004C484
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

		// Token: 0x17000431 RID: 1073
		// (get) Token: 0x0600112F RID: 4399
		public abstract override int CurrentPosition { get; }
	}
}
