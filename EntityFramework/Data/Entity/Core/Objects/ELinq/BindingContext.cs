using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Linq;
using System.Linq.Expressions;

namespace System.Data.Entity.Core.Objects.ELinq
{
	// Token: 0x0200054E RID: 1358
	internal sealed class BindingContext
	{
		// Token: 0x060034AC RID: 13484 RVA: 0x000F90A2 File Offset: 0x000F72A2
		internal BindingContext()
		{
			this._scopes = new Stack<Binding>();
		}

		// Token: 0x060034AD RID: 13485 RVA: 0x000F90B5 File Offset: 0x000F72B5
		internal void PushBindingScope(Binding binding)
		{
			this._scopes.Push(binding);
		}

		// Token: 0x060034AE RID: 13486 RVA: 0x000F90C3 File Offset: 0x000F72C3
		internal void PopBindingScope()
		{
			this._scopes.Pop();
		}

		// Token: 0x060034AF RID: 13487 RVA: 0x000F90F4 File Offset: 0x000F72F4
		internal bool TryGetBoundExpression(Expression linqExpression, out DbExpression cqtExpression)
		{
			cqtExpression = (from binding in this._scopes
			where binding.LinqExpression == linqExpression
			select binding.CqtExpression).FirstOrDefault<DbExpression>();
			return cqtExpression != null;
		}

		// Token: 0x040013B6 RID: 5046
		private readonly Stack<Binding> _scopes;
	}
}
