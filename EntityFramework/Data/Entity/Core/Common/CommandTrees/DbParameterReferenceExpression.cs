using System;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Utilities;

namespace System.Data.Entity.Core.Common.CommandTrees
{
	// Token: 0x020000FF RID: 255
	public class DbParameterReferenceExpression : DbExpression
	{
		// Token: 0x0600065E RID: 1630 RVA: 0x00025D30 File Offset: 0x00023F30
		internal DbParameterReferenceExpression()
		{
		}

		// Token: 0x0600065F RID: 1631 RVA: 0x00025D38 File Offset: 0x00023F38
		internal DbParameterReferenceExpression(TypeUsage type, string name) : base(DbExpressionKind.ParameterReference, type, false)
		{
			this._name = name;
		}

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x06000660 RID: 1632 RVA: 0x00025D4B File Offset: 0x00023F4B
		public virtual string ParameterName
		{
			get
			{
				return this._name;
			}
		}

		// Token: 0x06000661 RID: 1633 RVA: 0x00025D53 File Offset: 0x00023F53
		public override void Accept(DbExpressionVisitor visitor)
		{
			Check.NotNull<DbExpressionVisitor>(visitor, "visitor");
			visitor.Visit(this);
		}

		// Token: 0x06000662 RID: 1634 RVA: 0x00025D68 File Offset: 0x00023F68
		public override TResultType Accept<TResultType>(DbExpressionVisitor<TResultType> visitor)
		{
			Check.NotNull<DbExpressionVisitor<TResultType>>(visitor, "visitor");
			return visitor.Visit(this);
		}

		// Token: 0x040001E9 RID: 489
		private readonly string _name;
	}
}
