using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reflection;

namespace System.Linq.Expressions
{
	// Token: 0x0200025D RID: 605
	internal class InstanceMethodCallExpression3 : MethodCallExpression, IArgumentProvider
	{
		// Token: 0x060015E8 RID: 5608 RVA: 0x00048FA9 File Offset: 0x000471A9
		public InstanceMethodCallExpression3(MethodInfo method, Expression instance, Expression arg0, Expression arg1, Expression arg2) : base(method)
		{
			this._instance = instance;
			this._arg0 = arg0;
			this._arg1 = arg1;
			this._arg2 = arg2;
		}

		// Token: 0x060015E9 RID: 5609 RVA: 0x00048FD0 File Offset: 0x000471D0
		Expression IArgumentProvider.GetArgument(int index)
		{
			switch (index)
			{
			case 0:
				return Expression.ReturnObject<Expression>(this._arg0);
			case 1:
				return this._arg1;
			case 2:
				return this._arg2;
			default:
				throw new InvalidOperationException();
			}
		}

		// Token: 0x170003DF RID: 991
		// (get) Token: 0x060015EA RID: 5610 RVA: 0x00049005 File Offset: 0x00047205
		int IArgumentProvider.ArgumentCount
		{
			get
			{
				return 3;
			}
		}

		// Token: 0x060015EB RID: 5611 RVA: 0x00049008 File Offset: 0x00047208
		internal override Expression GetInstance()
		{
			return this._instance;
		}

		// Token: 0x060015EC RID: 5612 RVA: 0x00049010 File Offset: 0x00047210
		internal override ReadOnlyCollection<Expression> GetOrMakeArguments()
		{
			return Expression.ReturnReadOnly(this, ref this._arg0);
		}

		// Token: 0x060015ED RID: 5613 RVA: 0x00049020 File Offset: 0x00047220
		internal override MethodCallExpression Rewrite(Expression instance, IList<Expression> args)
		{
			if (args != null)
			{
				return Expression.Call(instance, base.Method, args[0], args[1], args[2]);
			}
			return Expression.Call(instance, base.Method, Expression.ReturnObject<Expression>(this._arg0), this._arg1, this._arg2);
		}

		// Token: 0x04000A41 RID: 2625
		private readonly Expression _instance;

		// Token: 0x04000A42 RID: 2626
		private object _arg0;

		// Token: 0x04000A43 RID: 2627
		private readonly Expression _arg1;

		// Token: 0x04000A44 RID: 2628
		private readonly Expression _arg2;
	}
}
