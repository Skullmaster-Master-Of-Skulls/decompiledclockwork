using System;
using System.Data.Metadata.Edm;

namespace System.Data.Common.CommandTrees
{
	// Token: 0x0200041A RID: 1050
	public sealed class DbParameterReferenceExpression : DbExpression
	{
		// Token: 0x060036ED RID: 14061 RVA: 0x000D15BE File Offset: 0x000CF7BE
		internal DbParameterReferenceExpression(TypeUsage type, string name) : base(DbExpressionKind.ParameterReference, type)
		{
			this._name = name;
		}

		// Token: 0x17000A8C RID: 2700
		// (get) Token: 0x060036EE RID: 14062 RVA: 0x000D15D0 File Offset: 0x000CF7D0
		public string ParameterName
		{
			get
			{
				return this._name;
			}
		}

		// Token: 0x060036EF RID: 14063 RVA: 0x000D15D8 File Offset: 0x000CF7D8
		public override void Accept(DbExpressionVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.Visit(this);
				return;
			}
			throw EntityUtil.ArgumentNull("visitor");
		}

		// Token: 0x060036F0 RID: 14064 RVA: 0x000D15EF File Offset: 0x000CF7EF
		public override TResultType Accept<TResultType>(DbExpressionVisitor<TResultType> visitor)
		{
			if (visitor != null)
			{
				return visitor.Visit(this);
			}
			throw EntityUtil.ArgumentNull("visitor");
		}

		// Token: 0x04001827 RID: 6183
		private readonly string _name;
	}
}
