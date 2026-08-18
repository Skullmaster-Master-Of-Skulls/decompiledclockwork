using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.CommandTrees.Internal;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Utilities;
using System.Diagnostics.CodeAnalysis;

namespace System.Data.Entity.Core.Common.CommandTrees
{
	// Token: 0x020000EF RID: 239
	public class DbFunctionExpression : DbExpression
	{
		// Token: 0x06000617 RID: 1559 RVA: 0x0002588E File Offset: 0x00023A8E
		internal DbFunctionExpression()
		{
		}

		// Token: 0x06000618 RID: 1560 RVA: 0x00025896 File Offset: 0x00023A96
		internal DbFunctionExpression(TypeUsage resultType, EdmFunction function, DbExpressionList arguments) : base(DbExpressionKind.Function, resultType, true)
		{
			this._functionInfo = function;
			this._arguments = arguments;
		}

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x06000619 RID: 1561 RVA: 0x000258B0 File Offset: 0x00023AB0
		[SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "Function")]
		public virtual EdmFunction Function
		{
			get
			{
				return this._functionInfo;
			}
		}

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x0600061A RID: 1562 RVA: 0x000258B8 File Offset: 0x00023AB8
		public virtual IList<DbExpression> Arguments
		{
			get
			{
				return this._arguments;
			}
		}

		// Token: 0x0600061B RID: 1563 RVA: 0x000258C0 File Offset: 0x00023AC0
		public override void Accept(DbExpressionVisitor visitor)
		{
			Check.NotNull<DbExpressionVisitor>(visitor, "visitor");
			visitor.Visit(this);
		}

		// Token: 0x0600061C RID: 1564 RVA: 0x000258D5 File Offset: 0x00023AD5
		public override TResultType Accept<TResultType>(DbExpressionVisitor<TResultType> visitor)
		{
			Check.NotNull<DbExpressionVisitor<TResultType>>(visitor, "visitor");
			return visitor.Visit(this);
		}

		// Token: 0x040001D5 RID: 469
		private readonly EdmFunction _functionInfo;

		// Token: 0x040001D6 RID: 470
		private readonly DbExpressionList _arguments;
	}
}
