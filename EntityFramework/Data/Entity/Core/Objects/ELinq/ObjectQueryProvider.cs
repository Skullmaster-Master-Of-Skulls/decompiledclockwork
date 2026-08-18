using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects.Internal;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace System.Data.Entity.Core.Objects.ELinq
{
	// Token: 0x02000566 RID: 1382
	internal class ObjectQueryProvider : IDbAsyncQueryProvider, IQueryProvider
	{
		// Token: 0x06003551 RID: 13649 RVA: 0x000FC5D2 File Offset: 0x000FA7D2
		internal ObjectQueryProvider(ObjectContext context)
		{
			this._context = context;
		}

		// Token: 0x06003552 RID: 13650 RVA: 0x000FC5E1 File Offset: 0x000FA7E1
		internal ObjectQueryProvider(ObjectQuery query) : this(query.Context)
		{
			this._query = query;
		}

		// Token: 0x06003553 RID: 13651 RVA: 0x000FC5F6 File Offset: 0x000FA7F6
		internal virtual ObjectQuery<TElement> CreateQuery<TElement>(Expression expression)
		{
			return this.GetObjectQueryState(this._query, expression, typeof(TElement)).CreateObjectQuery<TElement>();
		}

		// Token: 0x06003554 RID: 13652 RVA: 0x000FC614 File Offset: 0x000FA814
		internal virtual ObjectQuery CreateQuery(Expression expression, Type ofType)
		{
			return this.GetObjectQueryState(this._query, expression, ofType).CreateQuery();
		}

		// Token: 0x06003555 RID: 13653 RVA: 0x000FC629 File Offset: 0x000FA829
		private ObjectQueryState GetObjectQueryState(ObjectQuery query, Expression expression, Type ofType)
		{
			if (query != null)
			{
				return new ELinqQueryState(ofType, this._query, expression, null);
			}
			return new ELinqQueryState(ofType, this._context, expression, null);
		}

		// Token: 0x06003556 RID: 13654 RVA: 0x000FC64B File Offset: 0x000FA84B
		IQueryable<TElement> IQueryProvider.CreateQuery<TElement>(Expression expression)
		{
			Check.NotNull<Expression>(expression, "expression");
			if (!typeof(IQueryable<TElement>).IsAssignableFrom(expression.Type))
			{
				throw new ArgumentException(Strings.ELinq_ExpressionMustBeIQueryable, "expression");
			}
			return this.CreateQuery<TElement>(expression);
		}

		// Token: 0x06003557 RID: 13655 RVA: 0x000FC688 File Offset: 0x000FA888
		TResult IQueryProvider.Execute<TResult>(Expression expression)
		{
			Check.NotNull<Expression>(expression, "expression");
			ObjectQuery<TResult> query = this.CreateQuery<TResult>(expression);
			return ObjectQueryProvider.ExecuteSingle<TResult>(query, expression);
		}

		// Token: 0x06003558 RID: 13656 RVA: 0x000FC6B0 File Offset: 0x000FA8B0
		IQueryable IQueryProvider.CreateQuery(Expression expression)
		{
			Check.NotNull<Expression>(expression, "expression");
			if (!typeof(IQueryable).IsAssignableFrom(expression.Type))
			{
				throw new ArgumentException(Strings.ELinq_ExpressionMustBeIQueryable, "expression");
			}
			Type elementType = TypeSystem.GetElementType(expression.Type);
			return this.CreateQuery(expression, elementType);
		}

		// Token: 0x06003559 RID: 13657 RVA: 0x000FC704 File Offset: 0x000FA904
		object IQueryProvider.Execute(Expression expression)
		{
			Check.NotNull<Expression>(expression, "expression");
			ObjectQuery source = this.CreateQuery(expression, expression.Type);
			IEnumerable<object> query = source.Cast<object>();
			return ObjectQueryProvider.ExecuteSingle<object>(query, expression);
		}

		// Token: 0x0600355A RID: 13658 RVA: 0x000FC73C File Offset: 0x000FA93C
		Task<TResult> IDbAsyncQueryProvider.ExecuteAsync<TResult>(Expression expression, CancellationToken cancellationToken)
		{
			Check.NotNull<Expression>(expression, "expression");
			cancellationToken.ThrowIfCancellationRequested();
			ObjectQuery<TResult> query = this.CreateQuery<TResult>(expression);
			return ObjectQueryProvider.ExecuteSingleAsync<TResult>(query, expression, cancellationToken);
		}

		// Token: 0x0600355B RID: 13659 RVA: 0x000FC76C File Offset: 0x000FA96C
		Task<object> IDbAsyncQueryProvider.ExecuteAsync(Expression expression, CancellationToken cancellationToken)
		{
			Check.NotNull<Expression>(expression, "expression");
			cancellationToken.ThrowIfCancellationRequested();
			ObjectQuery source = this.CreateQuery(expression, expression.Type);
			IDbAsyncEnumerable<object> query = source.Cast<object>();
			return ObjectQueryProvider.ExecuteSingleAsync<object>(query, expression, cancellationToken);
		}

		// Token: 0x0600355C RID: 13660 RVA: 0x000FC7A9 File Offset: 0x000FA9A9
		internal static TResult ExecuteSingle<TResult>(IEnumerable<TResult> query, Expression queryRoot)
		{
			return ObjectQueryProvider.GetElementFunction<TResult>(queryRoot)(query);
		}

		// Token: 0x0600355D RID: 13661 RVA: 0x000FC7D8 File Offset: 0x000FA9D8
		private static Func<IEnumerable<TResult>, TResult> GetElementFunction<TResult>(Expression queryRoot)
		{
			Func<IEnumerable<TResult>, TResult> func = null;
			Func<IEnumerable<TResult>, TResult> func2 = null;
			Func<IEnumerable<TResult>, TResult> func3 = null;
			SequenceMethod sequenceMethod;
			if (ReflectionUtil.TryIdentifySequenceMethod(queryRoot, true, out sequenceMethod))
			{
				SequenceMethod sequenceMethod2 = sequenceMethod;
				switch (sequenceMethod2)
				{
				case SequenceMethod.First:
				case SequenceMethod.FirstPredicate:
					if (func == null)
					{
						func = ((IEnumerable<TResult> sequence) => sequence.First<TResult>());
					}
					return func;
				case SequenceMethod.FirstOrDefault:
				case SequenceMethod.FirstOrDefaultPredicate:
					if (func2 == null)
					{
						func2 = ((IEnumerable<TResult> sequence) => sequence.FirstOrDefault<TResult>());
					}
					return func2;
				default:
					switch (sequenceMethod2)
					{
					case SequenceMethod.SingleOrDefault:
					case SequenceMethod.SingleOrDefaultPredicate:
						if (func3 == null)
						{
							func3 = ((IEnumerable<TResult> sequence) => sequence.SingleOrDefault<TResult>());
						}
						return func3;
					}
					break;
				}
			}
			return (IEnumerable<TResult> sequence) => sequence.Single<TResult>();
		}

		// Token: 0x0600355E RID: 13662 RVA: 0x000FC869 File Offset: 0x000FAA69
		internal static Task<TResult> ExecuteSingleAsync<TResult>(IDbAsyncEnumerable<TResult> query, Expression queryRoot, CancellationToken cancellationToken)
		{
			return ObjectQueryProvider.GetAsyncElementFunction<TResult>(queryRoot)(query, cancellationToken);
		}

		// Token: 0x0600355F RID: 13663 RVA: 0x000FC89C File Offset: 0x000FAA9C
		private static Func<IDbAsyncEnumerable<TResult>, CancellationToken, Task<TResult>> GetAsyncElementFunction<TResult>(Expression queryRoot)
		{
			Func<IDbAsyncEnumerable<TResult>, CancellationToken, Task<TResult>> func = null;
			Func<IDbAsyncEnumerable<TResult>, CancellationToken, Task<TResult>> func2 = null;
			Func<IDbAsyncEnumerable<TResult>, CancellationToken, Task<TResult>> func3 = null;
			SequenceMethod sequenceMethod;
			if (ReflectionUtil.TryIdentifySequenceMethod(queryRoot, true, out sequenceMethod))
			{
				SequenceMethod sequenceMethod2 = sequenceMethod;
				switch (sequenceMethod2)
				{
				case SequenceMethod.First:
				case SequenceMethod.FirstPredicate:
					if (func == null)
					{
						func = ((IDbAsyncEnumerable<TResult> sequence, CancellationToken cancellationToken) => sequence.FirstAsync(cancellationToken));
					}
					return func;
				case SequenceMethod.FirstOrDefault:
				case SequenceMethod.FirstOrDefaultPredicate:
					if (func2 == null)
					{
						func2 = ((IDbAsyncEnumerable<TResult> sequence, CancellationToken cancellationToken) => sequence.FirstOrDefaultAsync(cancellationToken));
					}
					return func2;
				default:
					switch (sequenceMethod2)
					{
					case SequenceMethod.SingleOrDefault:
					case SequenceMethod.SingleOrDefaultPredicate:
						if (func3 == null)
						{
							func3 = ((IDbAsyncEnumerable<TResult> sequence, CancellationToken cancellationToken) => sequence.SingleOrDefaultAsync(cancellationToken));
						}
						return func3;
					}
					break;
				}
			}
			return (IDbAsyncEnumerable<TResult> sequence, CancellationToken cancellationToken) => sequence.SingleAsync(cancellationToken);
		}

		// Token: 0x040013FF RID: 5119
		private readonly ObjectContext _context;

		// Token: 0x04001400 RID: 5120
		private readonly ObjectQuery _query;
	}
}
