using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace System.Linq
{
	// Token: 0x02000164 RID: 356
	[__DynamicallyInvokable]
	public class EnumerableQuery<T> : EnumerableQuery, IOrderedQueryable<T>, IQueryable<T>, IEnumerable<T>, IEnumerable, IQueryable, IOrderedQueryable, IQueryProvider
	{
		// Token: 0x1700022F RID: 559
		// (get) Token: 0x06000C44 RID: 3140 RVA: 0x0002D524 File Offset: 0x0002B724
		[__DynamicallyInvokable]
		IQueryProvider IQueryable.Provider
		{
			[__DynamicallyInvokable]
			get
			{
				return this;
			}
		}

		// Token: 0x06000C45 RID: 3141 RVA: 0x0002D527 File Offset: 0x0002B727
		[__DynamicallyInvokable]
		public EnumerableQuery(IEnumerable<T> enumerable)
		{
			this.enumerable = enumerable;
			this.expression = Expression.Constant(this);
		}

		// Token: 0x06000C46 RID: 3142 RVA: 0x0002D542 File Offset: 0x0002B742
		[__DynamicallyInvokable]
		public EnumerableQuery(Expression expression)
		{
			this.expression = expression;
		}

		// Token: 0x17000230 RID: 560
		// (get) Token: 0x06000C47 RID: 3143 RVA: 0x0002D551 File Offset: 0x0002B751
		internal override Expression Expression
		{
			get
			{
				return this.expression;
			}
		}

		// Token: 0x17000231 RID: 561
		// (get) Token: 0x06000C48 RID: 3144 RVA: 0x0002D559 File Offset: 0x0002B759
		internal override IEnumerable Enumerable
		{
			get
			{
				return this.enumerable;
			}
		}

		// Token: 0x17000232 RID: 562
		// (get) Token: 0x06000C49 RID: 3145 RVA: 0x0002D561 File Offset: 0x0002B761
		[__DynamicallyInvokable]
		Expression IQueryable.Expression
		{
			[__DynamicallyInvokable]
			get
			{
				return this.expression;
			}
		}

		// Token: 0x17000233 RID: 563
		// (get) Token: 0x06000C4A RID: 3146 RVA: 0x0002D569 File Offset: 0x0002B769
		[__DynamicallyInvokable]
		Type IQueryable.ElementType
		{
			[__DynamicallyInvokable]
			get
			{
				return typeof(T);
			}
		}

		// Token: 0x06000C4B RID: 3147 RVA: 0x0002D578 File Offset: 0x0002B778
		[__DynamicallyInvokable]
		IQueryable IQueryProvider.CreateQuery(Expression expression)
		{
			if (expression == null)
			{
				throw Error.ArgumentNull("expression");
			}
			Type type = TypeHelper.FindGenericType(typeof(IQueryable<>), expression.Type);
			if (type == null)
			{
				throw Error.ArgumentNotValid("expression");
			}
			return EnumerableQuery.Create(type.GetGenericArguments()[0], expression);
		}

		// Token: 0x06000C4C RID: 3148 RVA: 0x0002D5CB File Offset: 0x0002B7CB
		[__DynamicallyInvokable]
		IQueryable<S> IQueryProvider.CreateQuery<S>(Expression expression)
		{
			if (expression == null)
			{
				throw Error.ArgumentNull("expression");
			}
			if (!typeof(IQueryable<S>).IsAssignableFrom(expression.Type))
			{
				throw Error.ArgumentNotValid("expression");
			}
			return new EnumerableQuery<S>(expression);
		}

		// Token: 0x06000C4D RID: 3149 RVA: 0x0002D604 File Offset: 0x0002B804
		[__DynamicallyInvokable]
		object IQueryProvider.Execute(Expression expression)
		{
			if (expression == null)
			{
				throw Error.ArgumentNull("expression");
			}
			Type type = typeof(EnumerableExecutor<>).MakeGenericType(new Type[]
			{
				expression.Type
			});
			return EnumerableExecutor.Create(expression).ExecuteBoxed();
		}

		// Token: 0x06000C4E RID: 3150 RVA: 0x0002D649 File Offset: 0x0002B849
		[__DynamicallyInvokable]
		S IQueryProvider.Execute<S>(Expression expression)
		{
			if (expression == null)
			{
				throw Error.ArgumentNull("expression");
			}
			if (!typeof(S).IsAssignableFrom(expression.Type))
			{
				throw Error.ArgumentNotValid("expression");
			}
			return new EnumerableExecutor<S>(expression).Execute();
		}

		// Token: 0x06000C4F RID: 3151 RVA: 0x0002D686 File Offset: 0x0002B886
		[__DynamicallyInvokable]
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x06000C50 RID: 3152 RVA: 0x0002D68E File Offset: 0x0002B88E
		[__DynamicallyInvokable]
		IEnumerator<T> IEnumerable<!0>.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x06000C51 RID: 3153 RVA: 0x0002D698 File Offset: 0x0002B898
		private IEnumerator<T> GetEnumerator()
		{
			if (this.enumerable == null)
			{
				EnumerableRewriter enumerableRewriter = new EnumerableRewriter();
				Expression body = enumerableRewriter.Visit(this.expression);
				Expression<Func<IEnumerable<T>>> expression = Expression.Lambda<Func<IEnumerable<T>>>(body, null);
				this.enumerable = expression.Compile()();
			}
			return this.enumerable.GetEnumerator();
		}

		// Token: 0x06000C52 RID: 3154 RVA: 0x0002D6E4 File Offset: 0x0002B8E4
		[__DynamicallyInvokable]
		public override string ToString()
		{
			ConstantExpression constantExpression = this.expression as ConstantExpression;
			if (constantExpression == null || constantExpression.Value != this)
			{
				return this.expression.ToString();
			}
			if (this.enumerable != null)
			{
				return this.enumerable.ToString();
			}
			return "null";
		}

		// Token: 0x040007A2 RID: 1954
		private Expression expression;

		// Token: 0x040007A3 RID: 1955
		private IEnumerable<T> enumerable;
	}
}
