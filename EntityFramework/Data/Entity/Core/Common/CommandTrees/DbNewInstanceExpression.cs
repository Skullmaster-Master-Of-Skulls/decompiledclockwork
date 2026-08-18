using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Core.Common.CommandTrees.Internal;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Utilities;

namespace System.Data.Entity.Core.Common.CommandTrees
{
	// Token: 0x020000FA RID: 250
	public sealed class DbNewInstanceExpression : DbExpression
	{
		// Token: 0x06000649 RID: 1609 RVA: 0x00025BC8 File Offset: 0x00023DC8
		internal DbNewInstanceExpression(TypeUsage type, DbExpressionList args) : base(DbExpressionKind.NewInstance, type, true)
		{
			this._elements = args;
		}

		// Token: 0x0600064A RID: 1610 RVA: 0x00025BDB File Offset: 0x00023DDB
		internal DbNewInstanceExpression(TypeUsage resultType, DbExpressionList attributeValues, ReadOnlyCollection<DbRelatedEntityRef> relationships) : this(resultType, attributeValues)
		{
			this._relatedEntityRefs = ((relationships.Count > 0) ? relationships : null);
		}

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x0600064B RID: 1611 RVA: 0x00025BF8 File Offset: 0x00023DF8
		public IList<DbExpression> Arguments
		{
			get
			{
				return this._elements;
			}
		}

		// Token: 0x0600064C RID: 1612 RVA: 0x00025C00 File Offset: 0x00023E00
		public override void Accept(DbExpressionVisitor visitor)
		{
			Check.NotNull<DbExpressionVisitor>(visitor, "visitor");
			visitor.Visit(this);
		}

		// Token: 0x0600064D RID: 1613 RVA: 0x00025C15 File Offset: 0x00023E15
		public override TResultType Accept<TResultType>(DbExpressionVisitor<TResultType> visitor)
		{
			Check.NotNull<DbExpressionVisitor<TResultType>>(visitor, "visitor");
			return visitor.Visit(this);
		}

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x0600064E RID: 1614 RVA: 0x00025C2A File Offset: 0x00023E2A
		internal bool HasRelatedEntityReferences
		{
			get
			{
				return this._relatedEntityRefs != null;
			}
		}

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x0600064F RID: 1615 RVA: 0x00025C38 File Offset: 0x00023E38
		internal ReadOnlyCollection<DbRelatedEntityRef> RelatedEntityReferences
		{
			get
			{
				return this._relatedEntityRefs;
			}
		}

		// Token: 0x040001E6 RID: 486
		private readonly DbExpressionList _elements;

		// Token: 0x040001E7 RID: 487
		private readonly ReadOnlyCollection<DbRelatedEntityRef> _relatedEntityRefs;
	}
}
