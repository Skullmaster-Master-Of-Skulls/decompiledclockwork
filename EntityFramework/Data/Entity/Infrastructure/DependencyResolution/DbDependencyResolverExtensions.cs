using System;
using System.Collections.Generic;
using System.Data.Entity.Utilities;
using System.Linq;

namespace System.Data.Entity.Infrastructure.DependencyResolution
{
	// Token: 0x0200015F RID: 351
	public static class DbDependencyResolverExtensions
	{
		// Token: 0x06000B5A RID: 2906 RVA: 0x00038C96 File Offset: 0x00036E96
		public static T GetService<T>(this IDbDependencyResolver resolver, object key)
		{
			Check.NotNull<IDbDependencyResolver>(resolver, "resolver");
			return (T)((object)resolver.GetService(typeof(T), key));
		}

		// Token: 0x06000B5B RID: 2907 RVA: 0x00038CBA File Offset: 0x00036EBA
		public static T GetService<T>(this IDbDependencyResolver resolver)
		{
			Check.NotNull<IDbDependencyResolver>(resolver, "resolver");
			return (T)((object)resolver.GetService(typeof(T), null));
		}

		// Token: 0x06000B5C RID: 2908 RVA: 0x00038CDE File Offset: 0x00036EDE
		public static object GetService(this IDbDependencyResolver resolver, Type type)
		{
			Check.NotNull<IDbDependencyResolver>(resolver, "resolver");
			Check.NotNull<Type>(type, "type");
			return resolver.GetService(type, null);
		}

		// Token: 0x06000B5D RID: 2909 RVA: 0x00038D00 File Offset: 0x00036F00
		public static IEnumerable<T> GetServices<T>(this IDbDependencyResolver resolver, object key)
		{
			Check.NotNull<IDbDependencyResolver>(resolver, "resolver");
			return resolver.GetServices(typeof(T), key).OfType<T>();
		}

		// Token: 0x06000B5E RID: 2910 RVA: 0x00038D24 File Offset: 0x00036F24
		public static IEnumerable<T> GetServices<T>(this IDbDependencyResolver resolver)
		{
			Check.NotNull<IDbDependencyResolver>(resolver, "resolver");
			return resolver.GetServices(typeof(T), null).OfType<T>();
		}

		// Token: 0x06000B5F RID: 2911 RVA: 0x00038D48 File Offset: 0x00036F48
		public static IEnumerable<object> GetServices(this IDbDependencyResolver resolver, Type type)
		{
			Check.NotNull<IDbDependencyResolver>(resolver, "resolver");
			Check.NotNull<Type>(type, "type");
			return resolver.GetServices(type, null);
		}

		// Token: 0x06000B60 RID: 2912 RVA: 0x00038D6C File Offset: 0x00036F6C
		internal static IEnumerable<object> GetServiceAsServices(this IDbDependencyResolver resolver, Type type, object key)
		{
			object service = resolver.GetService(type, key);
			if (service != null)
			{
				return new object[]
				{
					service
				};
			}
			return Enumerable.Empty<object>();
		}
	}
}
