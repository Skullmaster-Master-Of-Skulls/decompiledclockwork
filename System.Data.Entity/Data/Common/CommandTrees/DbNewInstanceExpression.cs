using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Common.CommandTrees.Internal;
using System.Data.Metadata.Edm;

namespace System.Data.Common.CommandTrees
{
	// Token: 0x02000420 RID: 1056
	public sealed class DbNewInstanceExpression : DbExpression
	{
		// Token: 0x0600370D RID: 14093 RVA: 0x000D18C4 File Offset: 0x000CFAC4
		internal DbNewInstanceExpression(TypeUsage type, DbExpressionList args) : base(DbExpressionKind.NewInstance, type)
		{
			this._elements = args;
		}

		// Token: 0x0600370E RID: 14094 RVA: 0x000D18D6 File Offset: 0x000CFAD6
		internal DbNewInstanceExpression(TypeUsage resultType, DbExpressionList attributeValues, ReadOnlyCollection<DbRelatedEntityRef> relationships) : this(resultType, attributeValues)
		{
			this._relatedEntityRefs = ((relationships.Count > 0) ? relationships : null);
		}

		// Token: 0x17000A9A RID: 2714
		// (get) Token: 0x0600370F RID: 14095 RVA: 0x000D18F3 File Offset: 0x000CFAF3
		public IList<DbExpression> Arguments
		{
			get
			{
				return this._elements;
			}
		}

		// Token: 0x06003710 RID: 14096 RVA: 0x000D18FB File Offset: 0x000CFAFB
		public override void Accept(DbExpressionVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.Visit(this);
				return;
			}
			throw EntityUtil.ArgumentNull("visitor");
		}

		// Token: 0x06003711 RID: 14097 RVA: 0x000D1912 File Offset: 0x000CFB12
		public override TResultType Accept<TResultType>(DbExpressionVisitor<TResultType> visitor)
		{
			if (visitor != null)
			{
				return visitor.Visit(this);
			}
			throw EntityUtil.ArgumentNull("visitor");
		}

		// Token: 0x17000A9B RID: 2715
		// (get) Token: 0x06003712 RID: 14098 RVA: 0x000D1929 File Offset: 0x000CFB29
		internal bool HasRelatedEntityReferences
		{
			get
			{
				return this._relatedEntityRefs != null;
			}
		}

		// Token: 0x17000A9C RID: 2716
		// (get) Token: 0x06003713 RID: 14099 RVA: 0x000D1934 File Offset: 0x000CFB34
		internal ReadOnlyCollection<DbRelatedEntityRef> RelatedEntityReferences
		{
			get
			{
				return this._relatedEntityRefs;
			}
		}

		// Token: 0x04001835 RID: 6197
		private readonly DbExpressionList _elements;

		// Token: 0x04001836 RID: 6198
		private readonly ReadOnlyCollection<DbRelatedEntityRef> _relatedEntityRefs;
	}
}
