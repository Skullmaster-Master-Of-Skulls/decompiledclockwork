using System;

namespace System.ServiceModel.Dispatcher
{
	// Token: 0x02000518 RID: 1304
	internal class XPathStepExpr : XPathExpr
	{
		// Token: 0x0600317D RID: 12669 RVA: 0x000BE34D File Offset: 0x000BC54D
		internal XPathStepExpr(NodeSelectCriteria desc) : this(desc, null)
		{
		}

		// Token: 0x0600317E RID: 12670 RVA: 0x000BE357 File Offset: 0x000BC557
		internal XPathStepExpr(NodeSelectCriteria desc, XPathExprList predicates) : base(XPathExprType.PathStep, ValueDataType.Sequence, predicates)
		{
			this.selectDesc = desc;
		}

		// Token: 0x17000BB6 RID: 2998
		// (get) Token: 0x0600317F RID: 12671 RVA: 0x000BE369 File Offset: 0x000BC569
		internal NodeSelectCriteria SelectDesc
		{
			get
			{
				return this.selectDesc;
			}
		}

		// Token: 0x04002667 RID: 9831
		private NodeSelectCriteria selectDesc;
	}
}
