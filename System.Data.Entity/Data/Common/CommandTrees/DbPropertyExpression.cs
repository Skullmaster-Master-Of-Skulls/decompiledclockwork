using System;
using System.Collections.Generic;
using System.Data.Metadata.Edm;

namespace System.Data.Common.CommandTrees
{
	// Token: 0x0200041B RID: 1051
	public sealed class DbPropertyExpression : DbExpression
	{
		// Token: 0x060036F1 RID: 14065 RVA: 0x000D1606 File Offset: 0x000CF806
		internal DbPropertyExpression(TypeUsage resultType, EdmMember property, DbExpression instance) : base(DbExpressionKind.Property, resultType)
		{
			this._property = property;
			this._instance = instance;
		}

		// Token: 0x17000A8D RID: 2701
		// (get) Token: 0x060036F2 RID: 14066 RVA: 0x000D161F File Offset: 0x000CF81F
		public EdmMember Property
		{
			get
			{
				return this._property;
			}
		}

		// Token: 0x17000A8E RID: 2702
		// (get) Token: 0x060036F3 RID: 14067 RVA: 0x000D1627 File Offset: 0x000CF827
		public DbExpression Instance
		{
			get
			{
				return this._instance;
			}
		}

		// Token: 0x060036F4 RID: 14068 RVA: 0x000D162F File Offset: 0x000CF82F
		public override void Accept(DbExpressionVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.Visit(this);
				return;
			}
			throw EntityUtil.ArgumentNull("visitor");
		}

		// Token: 0x060036F5 RID: 14069 RVA: 0x000D1646 File Offset: 0x000CF846
		public override TResultType Accept<TResultType>(DbExpressionVisitor<TResultType> visitor)
		{
			if (visitor != null)
			{
				return visitor.Visit(this);
			}
			throw EntityUtil.ArgumentNull("visitor");
		}

		// Token: 0x060036F6 RID: 14070 RVA: 0x000D165D File Offset: 0x000CF85D
		public KeyValuePair<string, DbExpression> ToKeyValuePair()
		{
			return new KeyValuePair<string, DbExpression>(this.Property.Name, this);
		}

		// Token: 0x060036F7 RID: 14071 RVA: 0x000D1670 File Offset: 0x000CF870
		public static implicit operator KeyValuePair<string, DbExpression>(DbPropertyExpression value)
		{
			EntityUtil.CheckArgumentNull<DbPropertyExpression>(value, "value");
			return value.ToKeyValuePair();
		}

		// Token: 0x04001828 RID: 6184
		private readonly EdmMember _property;

		// Token: 0x04001829 RID: 6185
		private readonly DbExpression _instance;
	}
}
