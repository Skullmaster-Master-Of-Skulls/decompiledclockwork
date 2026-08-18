using System;
using System.Collections.Generic;
using System.Linq;

namespace Telerik.Web.UI.PivotGrid.Core.Olap.Expressions
{
	// Token: 0x02000705 RID: 1797
	internal class OlapSelectQueryAxisClauseExpression : OlapExpression
	{
		// Token: 0x06003FCF RID: 16335 RVA: 0x000C9D9B File Offset: 0x000C7F9B
		internal OlapSelectQueryAxisClauseExpression(OlapExpression setExpression, IEnumerable<string> dimensionProperties, bool nonEmpty)
		{
			if (setExpression == null)
			{
				throw new ArgumentNullException("setExpression");
			}
			if (dimensionProperties == null)
			{
				throw new ArgumentNullException("dimensionProperties");
			}
			this.SetExpression = setExpression;
			this.dimensionProperties = dimensionProperties.ToList<string>();
			this.NonEmpty = nonEmpty;
		}

		// Token: 0x170014C9 RID: 5321
		// (get) Token: 0x06003FD0 RID: 16336 RVA: 0x000C9DD9 File Offset: 0x000C7FD9
		public IEnumerable<string> DimensionProperties
		{
			get
			{
				return this.dimensionProperties;
			}
		}

		// Token: 0x170014CA RID: 5322
		// (get) Token: 0x06003FD1 RID: 16337 RVA: 0x000C9DE1 File Offset: 0x000C7FE1
		// (set) Token: 0x06003FD2 RID: 16338 RVA: 0x000C9DE9 File Offset: 0x000C7FE9
		public OlapExpression SetExpression { get; private set; }

		// Token: 0x170014CB RID: 5323
		// (get) Token: 0x06003FD3 RID: 16339 RVA: 0x000C9DF2 File Offset: 0x000C7FF2
		public override OlapExpressionType NodeType
		{
			get
			{
				return OlapExpressionType.SelectQueryAxisClause;
			}
		}

		// Token: 0x170014CC RID: 5324
		// (get) Token: 0x06003FD4 RID: 16340 RVA: 0x000C9DF5 File Offset: 0x000C7FF5
		// (set) Token: 0x06003FD5 RID: 16341 RVA: 0x000C9DFD File Offset: 0x000C7FFD
		public bool NonEmpty { get; private set; }

		// Token: 0x06003FD6 RID: 16342 RVA: 0x000C9E06 File Offset: 0x000C8006
		protected internal override OlapExpression Accept(OlapExpressionVisitor visitor)
		{
			return visitor.VisitSelectQueryAxisClause(this);
		}

		// Token: 0x040010F2 RID: 4338
		private readonly IEnumerable<string> dimensionProperties;
	}
}
