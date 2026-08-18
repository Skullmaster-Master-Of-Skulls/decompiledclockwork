using System;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Utilities;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace System.Data.Entity.Internal.Linq
{
	// Token: 0x02000795 RID: 1941
	internal class NonGenericDbQueryProvider : DbQueryProvider
	{
		// Token: 0x06005806 RID: 22534 RVA: 0x0017AB35 File Offset: 0x00178D35
		public NonGenericDbQueryProvider(InternalContext internalContext, IInternalQuery internalQuery) : base(internalContext, internalQuery)
		{
		}

		// Token: 0x06005807 RID: 22535 RVA: 0x0017AB40 File Offset: 0x00178D40
		public override IQueryable<TElement> CreateQuery<TElement>(Expression expression)
		{
			Check.NotNull<Expression>(expression, "expression");
			ObjectQuery objectQuery = base.CreateObjectQuery(expression);
			if (typeof(TElement) != ((IQueryable)objectQuery).ElementType)
			{
				return (IQueryable<TElement>)this.CreateQuery(objectQuery);
			}
			return new InternalDbQuery<TElement>(new InternalQuery<TElement>(base.InternalContext, objectQuery));
		}

		// Token: 0x06005808 RID: 22536 RVA: 0x0017AB96 File Offset: 0x00178D96
		public override IQueryable CreateQuery(Expression expression)
		{
			Check.NotNull<Expression>(expression, "expression");
			return this.CreateQuery(base.CreateObjectQuery(expression));
		}

		// Token: 0x06005809 RID: 22537 RVA: 0x0017ABB4 File Offset: 0x00178DB4
		private IQueryable CreateQuery(ObjectQuery objectQuery)
		{
			IInternalQuery internalQuery = base.CreateInternalQuery(objectQuery);
			Type type = typeof(InternalDbQuery<>).MakeGenericType(new Type[]
			{
				internalQuery.ElementType
			});
			ConstructorInfo constructorInfo = type.GetConstructors(BindingFlags.Instance | BindingFlags.Public).Single<ConstructorInfo>();
			return (IQueryable)constructorInfo.Invoke(new object[]
			{
				internalQuery
			});
		}
	}
}
