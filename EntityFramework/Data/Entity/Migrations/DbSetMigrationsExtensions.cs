using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Internal.Linq;
using System.Data.Entity.ModelConfiguration.Utilities;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace System.Data.Entity.Migrations
{
	// Token: 0x020006D1 RID: 1745
	public static class DbSetMigrationsExtensions
	{
		// Token: 0x060045CC RID: 17868 RVA: 0x00148EC4 File Offset: 0x001470C4
		public static void AddOrUpdate<TEntity>(this IDbSet<TEntity> set, params TEntity[] entities) where TEntity : class
		{
			Check.NotNull<IDbSet<TEntity>>(set, "set");
			Check.NotNull<TEntity[]>(entities, "entities");
			DbSet<TEntity> dbSet = set as DbSet<TEntity>;
			if (dbSet != null)
			{
				InternalSet<TEntity> internalSet = (InternalSet<TEntity>)((IInternalSetAdapter)dbSet).InternalSet;
				if (internalSet != null)
				{
					dbSet.AddOrUpdate(DbSetMigrationsExtensions.GetKeyProperties<TEntity>(typeof(TEntity), internalSet), internalSet, entities);
					return;
				}
			}
			Type type = set.GetType();
			MethodInfo declaredMethod = type.GetDeclaredMethod("AddOrUpdate", new Type[]
			{
				typeof(TEntity[])
			});
			if (declaredMethod == null)
			{
				throw Error.UnableToDispatchAddOrUpdate(type);
			}
			declaredMethod.Invoke(set, (object[])new TEntity[][]
			{
				entities
			});
		}

		// Token: 0x060045CD RID: 17869 RVA: 0x00148F74 File Offset: 0x00147174
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		public static void AddOrUpdate<TEntity>(this IDbSet<TEntity> set, Expression<Func<TEntity, object>> identifierExpression, params TEntity[] entities) where TEntity : class
		{
			Check.NotNull<IDbSet<TEntity>>(set, "set");
			Check.NotNull<Expression<Func<TEntity, object>>>(identifierExpression, "identifierExpression");
			Check.NotNull<TEntity[]>(entities, "entities");
			DbSet<TEntity> dbSet = set as DbSet<TEntity>;
			if (dbSet != null)
			{
				InternalSet<TEntity> internalSet = (InternalSet<TEntity>)((IInternalSetAdapter)dbSet).InternalSet;
				if (internalSet != null)
				{
					IEnumerable<PropertyPath> simplePropertyAccessList = identifierExpression.GetSimplePropertyAccessList();
					dbSet.AddOrUpdate(simplePropertyAccessList, internalSet, entities);
					return;
				}
			}
			Type type = set.GetType();
			MethodInfo declaredMethod = type.GetDeclaredMethod("AddOrUpdate", new Type[]
			{
				typeof(Expression<Func<TEntity, object>>),
				typeof(TEntity[])
			});
			if (declaredMethod == null)
			{
				throw Error.UnableToDispatchAddOrUpdate(type);
			}
			declaredMethod.Invoke(set, new object[]
			{
				identifierExpression,
				entities
			});
		}

		// Token: 0x060045CE RID: 17870 RVA: 0x00149090 File Offset: 0x00147290
		private static void AddOrUpdate<TEntity>(this DbSet<TEntity> set, IEnumerable<PropertyPath> identifyingProperties, InternalSet<TEntity> internalSet, params TEntity[] entities) where TEntity : class
		{
			Func<Expression, BinaryExpression, Expression> func = null;
			IEnumerable<PropertyPath> keyProperties = DbSetMigrationsExtensions.GetKeyProperties<TEntity>(typeof(TEntity), internalSet);
			ParameterExpression parameter = Expression.Parameter(typeof(TEntity));
			for (int i = 0; i < entities.Length; i++)
			{
				TEntity entity = entities[i];
				IEnumerable<BinaryExpression> source = from pi in identifyingProperties
				select Expression.Equal(Expression.Property(parameter, pi.Single<PropertyInfo>()), Expression.Constant(pi.Last<PropertyInfo>().GetValue(entity, null)));
				Expression seed = null;
				if (func == null)
				{
					func = delegate(Expression current, BinaryExpression predicate)
					{
						if (current != null)
						{
							return Expression.AndAlso(current, predicate);
						}
						return predicate;
					};
				}
				Expression body = source.Aggregate(seed, func);
				TEntity tentity = set.SingleOrDefault(Expression.Lambda<Func<TEntity, bool>>(body, new ParameterExpression[]
				{
					parameter
				}));
				if (tentity != null)
				{
					foreach (PropertyPath source2 in keyProperties)
					{
						source2.Single<PropertyInfo>().GetPropertyInfoForSet().SetValue(entity, source2.Single<PropertyInfo>().GetValue(tentity, null), null);
					}
					internalSet.InternalContext.Owner.Entry<TEntity>(tentity).CurrentValues.SetValues(entity);
				}
				else
				{
					internalSet.Add(entity);
				}
			}
		}

		// Token: 0x060045CF RID: 17871 RVA: 0x00149234 File Offset: 0x00147434
		private static IEnumerable<PropertyPath> GetKeyProperties<TEntity>(Type entityType, InternalSet<TEntity> internalSet) where TEntity : class
		{
			return from km in internalSet.InternalContext.GetEntitySetAndBaseTypeForType(typeof(TEntity)).EntitySet.ElementType.KeyMembers
			select new PropertyPath(entityType.GetAnyProperty(km.Name));
		}
	}
}
