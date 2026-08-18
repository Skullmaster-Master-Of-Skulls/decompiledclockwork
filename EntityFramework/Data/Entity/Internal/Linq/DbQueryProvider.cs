using System;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Utilities;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace System.Data.Entity.Internal.Linq
{
	// Token: 0x0200078B RID: 1931
	internal class DbQueryProvider : IDbAsyncQueryProvider, IQueryProvider
	{
		// Token: 0x06005780 RID: 22400 RVA: 0x0017933F File Offset: 0x0017753F
		public DbQueryProvider(InternalContext internalContext, IInternalQuery internalQuery)
		{
			this._internalContext = internalContext;
			this._internalQuery = internalQuery;
		}

		// Token: 0x06005781 RID: 22401 RVA: 0x00179358 File Offset: 0x00177558
		public virtual IQueryable<TElement> CreateQuery<TElement>(Expression expression)
		{
			Check.NotNull<Expression>(expression, "expression");
			ObjectQuery objectQuery = this.CreateObjectQuery(expression);
			if (typeof(TElement) != ((IQueryable)objectQuery).ElementType)
			{
				return (IQueryable<TElement>)this.CreateQuery(objectQuery);
			}
			return new DbQuery<TElement>(new InternalQuery<TElement>(this._internalContext, objectQuery));
		}

		// Token: 0x06005782 RID: 22402 RVA: 0x001793AE File Offset: 0x001775AE
		public virtual IQueryable CreateQuery(Expression expression)
		{
			Check.NotNull<Expression>(expression, "expression");
			return this.CreateQuery(this.CreateObjectQuery(expression));
		}

		// Token: 0x06005783 RID: 22403 RVA: 0x001793C9 File Offset: 0x001775C9
		public virtual TResult Execute<TResult>(Expression expression)
		{
			Check.NotNull<Expression>(expression, "expression");
			this._internalContext.Initialize();
			return ((IQueryProvider)this._internalQuery.ObjectQueryProvider).Execute<TResult>(expression);
		}

		// Token: 0x06005784 RID: 22404 RVA: 0x001793F3 File Offset: 0x001775F3
		public virtual object Execute(Expression expression)
		{
			Check.NotNull<Expression>(expression, "expression");
			this._internalContext.Initialize();
			return ((IQueryProvider)this._internalQuery.ObjectQueryProvider).Execute(expression);
		}

		// Token: 0x06005785 RID: 22405 RVA: 0x0017941D File Offset: 0x0017761D
		Task<TResult> IDbAsyncQueryProvider.ExecuteAsync<TResult>(Expression expression, CancellationToken cancellationToken)
		{
			Check.NotNull<Expression>(expression, "expression");
			cancellationToken.ThrowIfCancellationRequested();
			this._internalContext.Initialize();
			return ((IDbAsyncQueryProvider)this._internalQuery.ObjectQueryProvider).ExecuteAsync<TResult>(expression, cancellationToken);
		}

		// Token: 0x06005786 RID: 22406 RVA: 0x0017944F File Offset: 0x0017764F
		Task<object> IDbAsyncQueryProvider.ExecuteAsync(Expression expression, CancellationToken cancellationToken)
		{
			Check.NotNull<Expression>(expression, "expression");
			cancellationToken.ThrowIfCancellationRequested();
			this._internalContext.Initialize();
			return ((IDbAsyncQueryProvider)this._internalQuery.ObjectQueryProvider).ExecuteAsync(expression, cancellationToken);
		}

		// Token: 0x06005787 RID: 22407 RVA: 0x00179484 File Offset: 0x00177684
		private IQueryable CreateQuery(ObjectQuery objectQuery)
		{
			IInternalQuery internalQuery = this.CreateInternalQuery(objectQuery);
			Type type = typeof(DbQuery<>).MakeGenericType(new Type[]
			{
				internalQuery.ElementType
			});
			ConstructorInfo constructorInfo = type.GetConstructors(BindingFlags.Instance | BindingFlags.NonPublic).Single<ConstructorInfo>();
			return (IQueryable)constructorInfo.Invoke(new object[]
			{
				internalQuery
			});
		}

		// Token: 0x06005788 RID: 22408 RVA: 0x001794E2 File Offset: 0x001776E2
		protected ObjectQuery CreateObjectQuery(Expression expression)
		{
			expression = new DbQueryVisitor().Visit(expression);
			return (ObjectQuery)((IQueryProvider)this._internalQuery.ObjectQueryProvider).CreateQuery(expression);
		}

		// Token: 0x06005789 RID: 22409 RVA: 0x00179508 File Offset: 0x00177708
		protected IInternalQuery CreateInternalQuery(ObjectQuery objectQuery)
		{
			Type type = typeof(InternalQuery<>).MakeGenericType(new Type[]
			{
				((IQueryable)objectQuery).ElementType
			});
			ConstructorInfo declaredConstructor = type.GetDeclaredConstructor(new Type[]
			{
				typeof(InternalContext),
				typeof(ObjectQuery)
			});
			return (IInternalQuery)declaredConstructor.Invoke(new object[]
			{
				this._internalContext,
				objectQuery
			});
		}

		// Token: 0x17000F4F RID: 3919
		// (get) Token: 0x0600578A RID: 22410 RVA: 0x00179583 File Offset: 0x00177783
		public InternalContext InternalContext
		{
			get
			{
				return this._internalContext;
			}
		}

		// Token: 0x04002344 RID: 9028
		private readonly InternalContext _internalContext;

		// Token: 0x04002345 RID: 9029
		private readonly IInternalQuery _internalQuery;
	}
}
