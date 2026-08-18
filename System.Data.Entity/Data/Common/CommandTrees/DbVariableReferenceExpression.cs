using System;
using System.Data.Metadata.Edm;

namespace System.Data.Common.CommandTrees
{
	// Token: 0x02000419 RID: 1049
	public sealed class DbVariableReferenceExpression : DbExpression
	{
		// Token: 0x060036E9 RID: 14057 RVA: 0x000D1576 File Offset: 0x000CF776
		internal DbVariableReferenceExpression(TypeUsage type, string name) : base(DbExpressionKind.VariableReference, type)
		{
			this._name = name;
		}

		// Token: 0x17000A8B RID: 2699
		// (get) Token: 0x060036EA RID: 14058 RVA: 0x000D1588 File Offset: 0x000CF788
		public string VariableName
		{
			get
			{
				return this._name;
			}
		}

		// Token: 0x060036EB RID: 14059 RVA: 0x000D1590 File Offset: 0x000CF790
		public override void Accept(DbExpressionVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.Visit(this);
				return;
			}
			throw EntityUtil.ArgumentNull("visitor");
		}

		// Token: 0x060036EC RID: 14060 RVA: 0x000D15A7 File Offset: 0x000CF7A7
		public override TResultType Accept<TResultType>(DbExpressionVisitor<TResultType> visitor)
		{
			if (visitor != null)
			{
				return visitor.Visit(this);
			}
			throw EntityUtil.ArgumentNull("visitor");
		}

		// Token: 0x04001826 RID: 6182
		private readonly string _name;
	}
}
