using System;

namespace System.Xml
{
	// Token: 0x02000081 RID: 129
	internal abstract class BaseTreeIterator
	{
		// Token: 0x0600060C RID: 1548 RVA: 0x00049658 File Offset: 0x00048A58
		internal BaseTreeIterator(DataSetMapper mapper)
		{
			this.mapper = mapper;
		}

		// Token: 0x0600060D RID: 1549
		internal abstract void Reset();

		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x0600060E RID: 1550
		internal abstract XmlNode CurrentNode { get; }

		// Token: 0x0600060F RID: 1551
		internal abstract bool Next();

		// Token: 0x06000610 RID: 1552
		internal abstract bool NextRight();

		// Token: 0x06000611 RID: 1553 RVA: 0x00049674 File Offset: 0x00048A74
		internal bool NextRowElement()
		{
			while (this.Next())
			{
				if (this.OnRowElement())
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000612 RID: 1554 RVA: 0x00049698 File Offset: 0x00048A98
		internal bool NextRightRowElement()
		{
			return this.NextRight() && (this.OnRowElement() || this.NextRowElement());
		}

		// Token: 0x06000613 RID: 1555 RVA: 0x000496C0 File Offset: 0x00048AC0
		internal bool OnRowElement()
		{
			XmlBoundElement xmlBoundElement = this.CurrentNode as XmlBoundElement;
			return xmlBoundElement != null && xmlBoundElement.Row != null;
		}

		// Token: 0x0400026B RID: 619
		protected DataSetMapper mapper;
	}
}
