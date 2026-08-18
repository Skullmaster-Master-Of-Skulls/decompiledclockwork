using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace System.Data.Entity.Internal
{
	// Token: 0x02000768 RID: 1896
	internal class DbSetDiscoveryService
	{
		// Token: 0x06005582 RID: 21890 RVA: 0x00174025 File Offset: 0x00172225
		public DbSetDiscoveryService(DbContext context)
		{
			this._context = context;
		}

		// Token: 0x06005583 RID: 21891 RVA: 0x001740B4 File Offset: 0x001722B4
		private Dictionary<Type, List<string>> GetSets()
		{
			DbContextTypesInitializersPair dbContextTypesInitializersPair;
			if (!DbSetDiscoveryService._objectSetInitializers.TryGetValue(this._context.GetType(), out dbContextTypesInitializersPair))
			{
				ParameterExpression parameterExpression = Expression.Parameter(typeof(DbContext), "dbContext");
				List<Action<DbContext>> initDelegates = new List<Action<DbContext>>();
				Dictionary<Type, List<string>> dictionary = new Dictionary<Type, List<string>>();
				foreach (PropertyInfo propertyInfo in from p in this._context.GetType().GetInstanceProperties()
				where p.GetIndexParameters().Length == 0 && p.DeclaringType != typeof(DbContext)
				select p)
				{
					Type setType = DbSetDiscoveryService.GetSetType(propertyInfo.PropertyType);
					if (setType != null)
					{
						if (!setType.IsValidStructuralType())
						{
							throw Error.InvalidEntityType(setType);
						}
						List<string> list;
						if (!dictionary.TryGetValue(setType, out list))
						{
							list = new List<string>();
							dictionary[setType] = list;
						}
						list.Add(propertyInfo.Name);
						if (DbSetDiscoveryService.DbSetPropertyShouldBeInitialized(propertyInfo))
						{
							MethodInfo methodInfo = propertyInfo.Setter();
							if (methodInfo != null && methodInfo.IsPublic)
							{
								MethodInfo method = DbSetDiscoveryService.SetMethod.MakeGenericMethod(new Type[]
								{
									setType
								});
								MethodCallExpression methodCallExpression = Expression.Call(parameterExpression, method);
								MethodCallExpression body = Expression.Call(Expression.Convert(parameterExpression, this._context.GetType()), methodInfo, new Expression[]
								{
									methodCallExpression
								});
								initDelegates.Add(Expression.Lambda<Action<DbContext>>(body, new ParameterExpression[]
								{
									parameterExpression
								}).Compile());
							}
						}
					}
				}
				Action<DbContext> setsInitializer = delegate(DbContext dbContext)
				{
					foreach (Action<DbContext> action in initDelegates)
					{
						action(dbContext);
					}
				};
				dbContextTypesInitializersPair = new DbContextTypesInitializersPair(dictionary, setsInitializer);
				DbSetDiscoveryService._objectSetInitializers.TryAdd(this._context.GetType(), dbContextTypesInitializersPair);
			}
			return dbContextTypesInitializersPair.EntityTypeToPropertyNameMap;
		}

		// Token: 0x06005584 RID: 21892 RVA: 0x001742B4 File Offset: 0x001724B4
		public void InitializeSets()
		{
			this.GetSets();
			DbSetDiscoveryService._objectSetInitializers[this._context.GetType()].SetsInitializer(this._context);
		}

		// Token: 0x06005585 RID: 21893 RVA: 0x001742F4 File Offset: 0x001724F4
		public void RegisterSets(DbModelBuilder modelBuilder)
		{
			IEnumerable<KeyValuePair<Type, List<string>>> enumerable = this.GetSets();
			if (modelBuilder.Version.IsEF6OrHigher())
			{
				enumerable = from s in enumerable
				orderby s.Value[0]
				select s;
			}
			foreach (KeyValuePair<Type, List<string>> keyValuePair in enumerable)
			{
				if (keyValuePair.Value.Count > 1)
				{
					throw Error.Mapping_MESTNotSupported(keyValuePair.Value[0], keyValuePair.Value[1], keyValuePair.Key);
				}
				modelBuilder.Entity(keyValuePair.Key).EntitySetName = keyValuePair.Value[0];
			}
		}

		// Token: 0x06005586 RID: 21894 RVA: 0x001743C4 File Offset: 0x001725C4
		private static bool DbSetPropertyShouldBeInitialized(PropertyInfo propertyInfo)
		{
			return !propertyInfo.GetCustomAttributes(false).Any<SuppressDbSetInitializationAttribute>() && !propertyInfo.DeclaringType.GetCustomAttributes(false).Any<SuppressDbSetInitializationAttribute>();
		}

		// Token: 0x06005587 RID: 21895 RVA: 0x001743EC File Offset: 0x001725EC
		private static Type GetSetType(Type declaredType)
		{
			if (!declaredType.IsArray)
			{
				Type setElementType = DbSetDiscoveryService.GetSetElementType(declaredType);
				if (setElementType != null)
				{
					Type c = typeof(DbSet<>).MakeGenericType(new Type[]
					{
						setElementType
					});
					if (declaredType.IsAssignableFrom(c))
					{
						return setElementType;
					}
				}
			}
			return null;
		}

		// Token: 0x06005588 RID: 21896 RVA: 0x0017443C File Offset: 0x0017263C
		private static Type GetSetElementType(Type setType)
		{
			try
			{
				Type type = (setType.IsGenericType() && typeof(IDbSet<>).IsAssignableFrom(setType.GetGenericTypeDefinition())) ? setType : setType.GetInterface(typeof(IDbSet<>).FullName);
				if (type != null && !type.ContainsGenericParameters())
				{
					return type.GetGenericArguments()[0];
				}
			}
			catch (AmbiguousMatchException)
			{
			}
			return null;
		}

		// Token: 0x040022BE RID: 8894
		private static readonly ConcurrentDictionary<Type, DbContextTypesInitializersPair> _objectSetInitializers = new ConcurrentDictionary<Type, DbContextTypesInitializersPair>();

		// Token: 0x040022BF RID: 8895
		public static readonly MethodInfo SetMethod = typeof(DbContext).GetDeclaredMethod("Set", new Type[0]);

		// Token: 0x040022C0 RID: 8896
		private readonly DbContext _context;
	}
}
