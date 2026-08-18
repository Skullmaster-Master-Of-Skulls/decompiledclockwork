using System;
using System.Collections;

namespace System.ServiceModel.Dispatcher
{
	// Token: 0x02000511 RID: 1297
	internal class XPathExprList
	{
		// Token: 0x06003167 RID: 12647 RVA: 0x000BE180 File Offset: 0x000BC380
		internal XPathExprList()
		{
			this.list = new ArrayList(2);
		}

		// Token: 0x17000BAB RID: 2987
		// (get) Token: 0x06003168 RID: 12648 RVA: 0x000BE194 File Offset: 0x000BC394
		internal int Count
		{
			get
			{
				return this.list.Count;
			}
		}

		// Token: 0x17000BAC RID: 2988
		internal XPathExpr this[int index]
		{
			get
			{
				return (XPathExpr)this.list[index];
			}
		}

		// Token: 0x0600316A RID: 12650 RVA: 0x000BE1B4 File Offset: 0x000BC3B4
		internal void Add(XPathExpr expr)
		{
			this.list.Add(expr);
		}

		// Token: 0x0400265F RID: 9823
		private ArrayList list;
	}
}
