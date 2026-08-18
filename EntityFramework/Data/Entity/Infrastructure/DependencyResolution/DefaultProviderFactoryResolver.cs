using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity.Resources;
using System.Linq;

namespace System.Data.Entity.Infrastructure.DependencyResolution
{
	// Token: 0x0200015C RID: 348
	internal class DefaultProviderFactoryResolver : IDbDependencyResolver
	{
		// Token: 0x06000B4D RID: 2893 RVA: 0x00038A62 File Offset: 0x00036C62
		public virtual object GetService(Type type, object key)
		{
			return DefaultProviderFactoryResolver.GetService(type, key, delegate(ArgumentException e, string n)
			{
				throw new ArgumentException(Strings.EntityClient_InvalidStoreProvider(n), e);
			});
		}

		// Token: 0x06000B4E RID: 2894 RVA: 0x00038A88 File Offset: 0x00036C88
		private static object GetService(Type type, object key, Func<ArgumentException, string, object> handleFailedLookup)
		{
			if (type == typeof(DbProviderFactory))
			{
				string text = key as string;
				if (string.IsNullOrWhiteSpace(text))
				{
					throw new ArgumentException(Strings.DbDependencyResolver_NoProviderInvariantName(typeof(DbProviderFactory).Name));
				}
				try
				{
					return DbProviderFactories.GetFactory(text);
				}
				catch (ArgumentException arg)
				{
					return handleFailedLookup(arg, text);
				}
			}
			return null;
		}

		// Token: 0x06000B4F RID: 2895 RVA: 0x00038AFC File Offset: 0x00036CFC
		public IEnumerable<object> GetServices(Type type, object key)
		{
			object service = DefaultProviderFactoryResolver.GetService(type, key, (ArgumentException e, string n) => null);
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
