using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reflection;

namespace System.Linq.Expressions
{
	// Token: 0x02000238 RID: 568
	[__DynamicallyInvokable]
	public sealed class ElementInit : IArgumentProvider
	{
		// Token: 0x060014BC RID: 5308 RVA: 0x00046206 File Offset: 0x00044406
		internal ElementInit(MethodInfo addMethod, ReadOnlyCollection<Expression> arguments)
		{
			this._addMethod = addMethod;
			this._arguments = arguments;
		}

		// Token: 0x17000392 RID: 914
		// (get) Token: 0x060014BD RID: 5309 RVA: 0x0004621C File Offset: 0x0004441C
		[__DynamicallyInvokable]
		public MethodInfo AddMethod
		{
			[__DynamicallyInvokable]
			get
			{
				return this._addMethod;
			}
		}

		// Token: 0x17000393 RID: 915
		// (get) Token: 0x060014BE RID: 5310 RVA: 0x00046224 File Offset: 0x00044424
		[__DynamicallyInvokable]
		public ReadOnlyCollection<Expression> Arguments
		{
			[__DynamicallyInvokable]
			get
			{
				return this._arguments;
			}
		}

		// Token: 0x060014BF RID: 5311 RVA: 0x0004622C File Offset: 0x0004442C
		[__DynamicallyInvokable]
		Expression IArgumentProvider.GetArgument(int index)
		{
			return this._arguments[index];
		}

		// Token: 0x17000394 RID: 916
		// (get) Token: 0x060014C0 RID: 5312 RVA: 0x0004623A File Offset: 0x0004443A
		[__DynamicallyInvokable]
		int IArgumentProvider.ArgumentCount
		{
			[__DynamicallyInvokable]
			get
			{
				return this._arguments.Count;
			}
		}

		// Token: 0x060014C1 RID: 5313 RVA: 0x00046247 File Offset: 0x00044447
		[__DynamicallyInvokable]
		public override string ToString()
		{
			return ExpressionStringBuilder.ElementInitBindingToString(this);
		}

		// Token: 0x060014C2 RID: 5314 RVA: 0x0004624F File Offset: 0x0004444F
		[__DynamicallyInvokable]
		public ElementInit Update(IEnumerable<Expression> arguments)
		{
			if (arguments == this.Arguments)
			{
				return this;
			}
			return Expression.ElementInit(this.AddMethod, arguments);
		}

		// Token: 0x040009A4 RID: 2468
		private MethodInfo _addMethod;

		// Token: 0x040009A5 RID: 2469
		private ReadOnlyCollection<Expression> _arguments;
	}
}
