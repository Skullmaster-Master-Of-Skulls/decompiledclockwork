using System;
using System.Collections.Generic;
using System.Data.Common.CommandTrees;
using System.Linq;
using System.Linq.Expressions;

namespace System.Data.Objects.ELinq
{
	// Token: 0x0200019F RID: 415
	internal sealed class BindingContext
	{
		// Token: 0x06001E4B RID: 7755 RVA: 0x0006897E File Offset: 0x00066B7E
		internal BindingContext()
		{
			this._scopes = new Stack<Binding>();
		}

		// Token: 0x06001E4C RID: 7756 RVA: 0x00068991 File Offset: 0x00066B91
		internal void PushBindingScope(Binding binding)
		{
			this._scopes.Push(binding);
		}

		// Token: 0x06001E4D RID: 7757 RVA: 0x0006899F File Offset: 0x00066B9F
		internal void PopBindingScope()
		{
			this._scopes.Pop();
		}

		// Token: 0x06001E4E RID: 7758 RVA: 0x000689B0 File Offset: 0x00066BB0
		internal bool TryGetBoundExpression(Expression linqExpression, out DbExpression cqtExpression)
		{
			cqtExpression = (from binding in this._scopes
			where binding.LinqExpression == linqExpression
			select binding.CqtExpression).FirstOrDefault<DbExpression>();
			return cqtExpression != null;
		}

		// Token: 0x04000C15 RID: 3093
		private readonly Stack<Binding> _scopes;
	}
}
