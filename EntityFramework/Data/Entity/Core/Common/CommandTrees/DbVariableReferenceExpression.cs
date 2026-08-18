using System;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Utilities;

namespace System.Data.Entity.Core.Common.CommandTrees
{
	// Token: 0x0200011A RID: 282
	public class DbVariableReferenceExpression : DbExpression
	{
		// Token: 0x0600076B RID: 1899 RVA: 0x000283C1 File Offset: 0x000265C1
		internal DbVariableReferenceExpression()
		{
		}

		// Token: 0x0600076C RID: 1900 RVA: 0x000283C9 File Offset: 0x000265C9
		internal DbVariableReferenceExpression(TypeUsage type, string name) : base(DbExpressionKind.VariableReference, type, true)
		{
			this._name = name;
		}

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x0600076D RID: 1901 RVA: 0x000283DC File Offset: 0x000265DC
		public virtual string VariableName
		{
			get
			{
				return this._name;
			}
		}

		// Token: 0x0600076E RID: 1902 RVA: 0x000283E4 File Offset: 0x000265E4
		public override void Accept(DbExpressionVisitor visitor)
		{
			Check.NotNull<DbExpressionVisitor>(visitor, "visitor");
			visitor.Visit(this);
		}

		// Token: 0x0600076F RID: 1903 RVA: 0x000283F9 File Offset: 0x000265F9
		public override TResultType Accept<TResultType>(DbExpressionVisitor<TResultType> visitor)
		{
			Check.NotNull<DbExpressionVisitor<TResultType>>(visitor, "visitor");
			return visitor.Visit(this);
		}

		// Token: 0x04000257 RID: 599
		private readonly string _name;
	}
}
