using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;

namespace System.Linq.Expressions
{
	// Token: 0x0200022E RID: 558
	internal class DynamicExpressionN : DynamicExpression, IArgumentProvider
	{
		// Token: 0x06001499 RID: 5273 RVA: 0x00045F56 File Offset: 0x00044156
		internal DynamicExpressionN(Type delegateType, CallSiteBinder binder, IList<Expression> arguments) : base(delegateType, binder)
		{
			this._arguments = arguments;
		}

		// Token: 0x0600149A RID: 5274 RVA: 0x00045F67 File Offset: 0x00044167
		Expression IArgumentProvider.GetArgument(int index)
		{
			return this._arguments[index];
		}

		// Token: 0x17000388 RID: 904
		// (get) Token: 0x0600149B RID: 5275 RVA: 0x00045F75 File Offset: 0x00044175
		int IArgumentProvider.ArgumentCount
		{
			get
			{
				return this._arguments.Count;
			}
		}

		// Token: 0x0600149C RID: 5276 RVA: 0x00045F82 File Offset: 0x00044182
		internal override ReadOnlyCollection<Expression> GetOrMakeArguments()
		{
			return Expression.ReturnReadOnly<Expression>(ref this._arguments);
		}

		// Token: 0x0600149D RID: 5277 RVA: 0x00045F8F File Offset: 0x0004418F
		internal override DynamicExpression Rewrite(Expression[] args)
		{
			return Expression.MakeDynamic(base.DelegateType, base.Binder, args);
		}

		// Token: 0x04000994 RID: 2452
		private IList<Expression> _arguments;
	}
}
