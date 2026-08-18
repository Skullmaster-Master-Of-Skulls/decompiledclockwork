using System;
using System.Collections.Generic;
using System.Linq;

namespace Telerik.Web.UI.PivotGrid.Core.Olap.Expressions
{
	// Token: 0x02000706 RID: 1798
	internal class OlapWrapperExpression : OlapExpression
	{
		// Token: 0x06003FD7 RID: 16343 RVA: 0x000C9E10 File Offset: 0x000C8010
		internal OlapWrapperExpression(IEnumerable<OlapExpression> memberExpressions, OlapWrapperExpressionType wrapperType)
		{
			if (memberExpressions == null)
			{
				throw new ArgumentNullException("memberExpressions");
			}
			List<OlapExpression> list = memberExpressions.ToList<OlapExpression>();
			this.expressions = list;
			this.WrapperType = wrapperType;
		}

		// Token: 0x170014CD RID: 5325
		// (get) Token: 0x06003FD8 RID: 16344 RVA: 0x000C9E46 File Offset: 0x000C8046
		public IEnumerable<OlapExpression> Expressions
		{
			get
			{
				return this.expressions;
			}
		}

		// Token: 0x170014CE RID: 5326
		// (get) Token: 0x06003FD9 RID: 16345 RVA: 0x000C9E4E File Offset: 0x000C804E
		public override OlapExpressionType NodeType
		{
			get
			{
				return OlapExpressionType.Wrapper;
			}
		}

		// Token: 0x170014CF RID: 5327
		// (get) Token: 0x06003FDA RID: 16346 RVA: 0x000C9E52 File Offset: 0x000C8052
		// (set) Token: 0x06003FDB RID: 16347 RVA: 0x000C9E5A File Offset: 0x000C805A
		public OlapWrapperExpressionType WrapperType { get; private set; }

		// Token: 0x06003FDC RID: 16348 RVA: 0x000C9E63 File Offset: 0x000C8063
		protected internal override OlapExpression Accept(OlapExpressionVisitor visitor)
		{
			return visitor.VisitWrapper(this);
		}

		// Token: 0x040010F5 RID: 4341
		private readonly IEnumerable<OlapExpression> expressions;
	}
}
