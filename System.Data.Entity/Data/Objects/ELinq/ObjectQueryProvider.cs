using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Objects.Internal;
using System.Linq;
using System.Linq.Expressions;

namespace System.Data.Objects.ELinq
{
	// Token: 0x0200019E RID: 414
	internal sealed class ObjectQueryProvider : IQueryProvider
	{
		// Token: 0x06001E41 RID: 7745 RVA: 0x00068711 File Offset: 0x00066911
		internal ObjectQueryProvider(ObjectContext context)
		{
			this._context = context;
		}

		// Token: 0x06001E42 RID: 7746 RVA: 0x00068720 File Offset: 0x00066920
		internal ObjectQueryProvider(ObjectQuery query) : this(query.Context)
		{
			this._query = query;
		}

		// Token: 0x06001E43 RID: 7747 RVA: 0x00068738 File Offset: 0x00066938
		IQueryable<S> IQueryProvider.CreateQuery<S>(Expression expression)
		{
			EntityUtil.CheckArgumentNull<Expression>(expression, "expression");
			if (!typeof(IQueryable<S>).IsAssignableFrom(expression.Type))
			{
				throw EntityUtil.Argument(Strings.ELinq_ExpressionMustBeIQueryable, "expression");
			}
			return this.CreateQuery<S>(expression);
		}

		// Token: 0x06001E44 RID: 7748 RVA: 0x00068784 File Offset: 0x00066984
		S IQueryProvider.Execute<S>(Expression expression)
		{
			EntityUtil.CheckArgumentNull<Expression>(expression, "expression");
			ObjectQuery<S> query = this.CreateQuery<S>(expression);
			return ObjectQueryProvider.ExecuteSingle<S>(query, expression);
		}

		// Token: 0x06001E45 RID: 7749 RVA: 0x000687AC File Offset: 0x000669AC
		IQueryable IQueryProvider.CreateQuery(Expression expression)
		{
			EntityUtil.CheckArgumentNull<Expression>(expression, "expression");
			if (!typeof(IQueryable).IsAssignableFrom(expression.Type))
			{
				throw EntityUtil.Argument(Strings.ELinq_ExpressionMustBeIQueryable, "expression");
			}
			Type elementType = TypeSystem.GetElementType(expression.Type);
			return this.CreateQuery(expression, elementType);
		}

		// Token: 0x06001E46 RID: 7750 RVA: 0x00068804 File Offset: 0x00066A04
		object IQueryProvider.Execute(Expression expression)
		{
			EntityUtil.CheckArgumentNull<Expression>(expression, "expression");
			ObjectQuery source = this.CreateQuery(expression, expression.Type);
			IEnumerable<object> query = source.Cast<object>();
			return ObjectQueryProvider.ExecuteSingle<object>(query, expression);
		}

		// Token: 0x06001E47 RID: 7751 RVA: 0x0006883C File Offset: 0x00066A3C
		private ObjectQuery<S> CreateQuery<S>(Expression expression)
		{
			ObjectQueryState queryState;
			if (this._query == null)
			{
				queryState = new ELinqQueryState(typeof(S), this._context, expression);
			}
			else
			{
				queryState = new ELinqQueryState(typeof(S), this._query, expression);
			}
			return new ObjectQuery<S>(queryState);
		}

		// Token: 0x06001E48 RID: 7752 RVA: 0x00068888 File Offset: 0x00066A88
		private ObjectQuery CreateQuery(Expression expression, Type ofType)
		{
			ObjectQueryState objectQueryState;
			if (this._query == null)
			{
				objectQueryState = new ELinqQueryState(ofType, this._context, expression);
			}
			else
			{
				objectQueryState = new ELinqQueryState(ofType, this._query, expression);
			}
			return objectQueryState.CreateQuery();
		}

		// Token: 0x06001E49 RID: 7753 RVA: 0x000688C1 File Offset: 0x00066AC1
		internal static TResult ExecuteSingle<TResult>(IEnumerable<TResult> query, Expression queryRoot)
		{
			return ObjectQueryProvider.GetElementFunction<TResult>(queryRoot)(query);
		}

		// Token: 0x06001E4A RID: 7754 RVA: 0x000688D0 File Offset: 0x00066AD0
		private static Func<IEnumerable<TResult>, TResult> GetElementFunction<TResult>(Expression queryRoot)
		{
			SequenceMethod sequenceMethod;
			if (ReflectionUtil.TryIdentifySequenceMethod(queryRoot, true, out sequenceMethod))
			{
				if (sequenceMethod - SequenceMethod.First <= 1)
				{
					return (IEnumerable<TResult> sequence) => sequence.First<TResult>();
				}
				if (sequenceMethod - SequenceMethod.FirstOrDefault <= 1)
				{
					return (IEnumerable<TResult> sequence) => sequence.FirstOrDefault<TResult>();
				}
				if (sequenceMethod - SequenceMethod.SingleOrDefault <= 1)
				{
					return (IEnumerable<TResult> sequence) => sequence.SingleOrDefault<TResult>();
				}
			}
			return (IEnumerable<TResult> sequence) => sequence.Single<TResult>();
		}

		// Token: 0x04000C13 RID: 3091
		private readonly ObjectContext _context;

		// Token: 0x04000C14 RID: 3092
		private readonly ObjectQuery _query;
	}
}
