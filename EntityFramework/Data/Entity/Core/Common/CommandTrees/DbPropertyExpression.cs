using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Utilities;
using System.Diagnostics.CodeAnalysis;

namespace System.Data.Entity.Core.Common.CommandTrees
{
	// Token: 0x02000101 RID: 257
	public class DbPropertyExpression : DbExpression
	{
		// Token: 0x06000668 RID: 1640 RVA: 0x00025DD1 File Offset: 0x00023FD1
		internal DbPropertyExpression()
		{
		}

		// Token: 0x06000669 RID: 1641 RVA: 0x00025DD9 File Offset: 0x00023FD9
		internal DbPropertyExpression(TypeUsage resultType, EdmMember property, DbExpression instance) : base(DbExpressionKind.Property, resultType, true)
		{
			this._property = property;
			this._instance = instance;
		}

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x0600066A RID: 1642 RVA: 0x00025DF3 File Offset: 0x00023FF3
		[SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "Property")]
		public virtual EdmMember Property
		{
			get
			{
				return this._property;
			}
		}

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x0600066B RID: 1643 RVA: 0x00025DFB File Offset: 0x00023FFB
		public virtual DbExpression Instance
		{
			get
			{
				return this._instance;
			}
		}

		// Token: 0x0600066C RID: 1644 RVA: 0x00025E03 File Offset: 0x00024003
		public override void Accept(DbExpressionVisitor visitor)
		{
			Check.NotNull<DbExpressionVisitor>(visitor, "visitor");
			visitor.Visit(this);
		}

		// Token: 0x0600066D RID: 1645 RVA: 0x00025E18 File Offset: 0x00024018
		public override TResultType Accept<TResultType>(DbExpressionVisitor<TResultType> visitor)
		{
			Check.NotNull<DbExpressionVisitor<TResultType>>(visitor, "visitor");
			return visitor.Visit(this);
		}

		// Token: 0x0600066E RID: 1646 RVA: 0x00025E2D File Offset: 0x0002402D
		public KeyValuePair<string, DbExpression> ToKeyValuePair()
		{
			return new KeyValuePair<string, DbExpression>(this.Property.Name, this);
		}

		// Token: 0x0600066F RID: 1647 RVA: 0x00025E40 File Offset: 0x00024040
		public static implicit operator KeyValuePair<string, DbExpression>(DbPropertyExpression value)
		{
			Check.NotNull<DbPropertyExpression>(value, "value");
			return value.ToKeyValuePair();
		}

		// Token: 0x040001EC RID: 492
		private readonly EdmMember _property;

		// Token: 0x040001ED RID: 493
		private readonly DbExpression _instance;
	}
}
