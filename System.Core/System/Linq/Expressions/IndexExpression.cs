using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Reflection;

namespace System.Linq.Expressions
{
	// Token: 0x02000242 RID: 578
	[DebuggerTypeProxy(typeof(Expression.IndexExpressionProxy))]
	[__DynamicallyInvokable]
	public sealed class IndexExpression : Expression, IArgumentProvider
	{
		// Token: 0x06001532 RID: 5426 RVA: 0x000480FA File Offset: 0x000462FA
		internal IndexExpression(Expression instance, PropertyInfo indexer, IList<Expression> arguments)
		{
			indexer == null;
			this._instance = instance;
			this._indexer = indexer;
			this._arguments = arguments;
		}

		// Token: 0x1700039C RID: 924
		// (get) Token: 0x06001533 RID: 5427 RVA: 0x0004811F File Offset: 0x0004631F
		[__DynamicallyInvokable]
		public sealed override ExpressionType NodeType
		{
			[__DynamicallyInvokable]
			get
			{
				return ExpressionType.Index;
			}
		}

		// Token: 0x1700039D RID: 925
		// (get) Token: 0x06001534 RID: 5428 RVA: 0x00048123 File Offset: 0x00046323
		[__DynamicallyInvokable]
		public sealed override Type Type
		{
			[__DynamicallyInvokable]
			get
			{
				if (this._indexer != null)
				{
					return this._indexer.PropertyType;
				}
				return this._instance.Type.GetElementType();
			}
		}

		// Token: 0x1700039E RID: 926
		// (get) Token: 0x06001535 RID: 5429 RVA: 0x0004814F File Offset: 0x0004634F
		[__DynamicallyInvokable]
		public Expression Object
		{
			[__DynamicallyInvokable]
			get
			{
				return this._instance;
			}
		}

		// Token: 0x1700039F RID: 927
		// (get) Token: 0x06001536 RID: 5430 RVA: 0x00048157 File Offset: 0x00046357
		[__DynamicallyInvokable]
		public PropertyInfo Indexer
		{
			[__DynamicallyInvokable]
			get
			{
				return this._indexer;
			}
		}

		// Token: 0x170003A0 RID: 928
		// (get) Token: 0x06001537 RID: 5431 RVA: 0x0004815F File Offset: 0x0004635F
		[__DynamicallyInvokable]
		public ReadOnlyCollection<Expression> Arguments
		{
			[__DynamicallyInvokable]
			get
			{
				return Expression.ReturnReadOnly<Expression>(ref this._arguments);
			}
		}

		// Token: 0x06001538 RID: 5432 RVA: 0x0004816C File Offset: 0x0004636C
		[__DynamicallyInvokable]
		public IndexExpression Update(Expression @object, IEnumerable<Expression> arguments)
		{
			if (@object == this.Object && arguments == this.Arguments)
			{
				return this;
			}
			return Expression.MakeIndex(@object, this.Indexer, arguments);
		}

		// Token: 0x06001539 RID: 5433 RVA: 0x0004818F File Offset: 0x0004638F
		[__DynamicallyInvokable]
		Expression IArgumentProvider.GetArgument(int index)
		{
			return this._arguments[index];
		}

		// Token: 0x170003A1 RID: 929
		// (get) Token: 0x0600153A RID: 5434 RVA: 0x0004819D File Offset: 0x0004639D
		[__DynamicallyInvokable]
		int IArgumentProvider.ArgumentCount
		{
			[__DynamicallyInvokable]
			get
			{
				return this._arguments.Count;
			}
		}

		// Token: 0x0600153B RID: 5435 RVA: 0x000481AA File Offset: 0x000463AA
		protected internal override Expression Accept(ExpressionVisitor visitor)
		{
			return visitor.VisitIndex(this);
		}

		// Token: 0x0600153C RID: 5436 RVA: 0x000481B4 File Offset: 0x000463B4
		internal Expression Rewrite(Expression instance, Expression[] arguments)
		{
			return Expression.MakeIndex(instance, this._indexer, arguments ?? this._arguments);
		}

		// Token: 0x04000A07 RID: 2567
		private readonly Expression _instance;

		// Token: 0x04000A08 RID: 2568
		private readonly PropertyInfo _indexer;

		// Token: 0x04000A09 RID: 2569
		private IList<Expression> _arguments;
	}
}
