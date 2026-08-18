using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reflection;

namespace System.Linq.Expressions
{
	// Token: 0x0200025C RID: 604
	internal class InstanceMethodCallExpression2 : MethodCallExpression, IArgumentProvider
	{
		// Token: 0x060015E2 RID: 5602 RVA: 0x00048F11 File Offset: 0x00047111
		public InstanceMethodCallExpression2(MethodInfo method, Expression instance, Expression arg0, Expression arg1) : base(method)
		{
			this._instance = instance;
			this._arg0 = arg0;
			this._arg1 = arg1;
		}

		// Token: 0x060015E3 RID: 5603 RVA: 0x00048F30 File Offset: 0x00047130
		Expression IArgumentProvider.GetArgument(int index)
		{
			if (index == 0)
			{
				return Expression.ReturnObject<Expression>(this._arg0);
			}
			if (index != 1)
			{
				throw new InvalidOperationException();
			}
			return this._arg1;
		}

		// Token: 0x170003DE RID: 990
		// (get) Token: 0x060015E4 RID: 5604 RVA: 0x00048F53 File Offset: 0x00047153
		int IArgumentProvider.ArgumentCount
		{
			get
			{
				return 2;
			}
		}

		// Token: 0x060015E5 RID: 5605 RVA: 0x00048F56 File Offset: 0x00047156
		internal override Expression GetInstance()
		{
			return this._instance;
		}

		// Token: 0x060015E6 RID: 5606 RVA: 0x00048F5E File Offset: 0x0004715E
		internal override ReadOnlyCollection<Expression> GetOrMakeArguments()
		{
			return Expression.ReturnReadOnly(this, ref this._arg0);
		}

		// Token: 0x060015E7 RID: 5607 RVA: 0x00048F6C File Offset: 0x0004716C
		internal override MethodCallExpression Rewrite(Expression instance, IList<Expression> args)
		{
			if (args != null)
			{
				return Expression.Call(instance, base.Method, args[0], args[1]);
			}
			return Expression.Call(instance, base.Method, Expression.ReturnObject<Expression>(this._arg0), this._arg1);
		}

		// Token: 0x04000A3E RID: 2622
		private readonly Expression _instance;

		// Token: 0x04000A3F RID: 2623
		private object _arg0;

		// Token: 0x04000A40 RID: 2624
		private readonly Expression _arg1;
	}
}
