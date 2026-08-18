using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common;
using System.Data.Entity.Resources;
using System.Linq;

namespace System.Data.Entity.Infrastructure.DependencyResolution
{
	// Token: 0x0200015D RID: 349
	internal class DefaultProviderServicesResolver : IDbDependencyResolver
	{
		// Token: 0x06000B53 RID: 2899 RVA: 0x00038B4B File Offset: 0x00036D4B
		public virtual object GetService(Type type, object key)
		{
			if (type == typeof(DbProviderServices))
			{
				throw new InvalidOperationException(Strings.EF6Providers_NoProviderFound(DefaultProviderServicesResolver.CheckKey(key)));
			}
			return null;
		}

		// Token: 0x06000B54 RID: 2900 RVA: 0x00038B74 File Offset: 0x00036D74
		private static string CheckKey(object key)
		{
			string text = key as string;
			if (string.IsNullOrWhiteSpace(text))
			{
				throw new ArgumentException(Strings.DbDependencyResolver_NoProviderInvariantName(typeof(DbProviderServices).Name));
			}
			return text;
		}

		// Token: 0x06000B55 RID: 2901 RVA: 0x00038BAB File Offset: 0x00036DAB
		public virtual IEnumerable<object> GetServices(Type type, object key)
		{
			if (type == typeof(DbProviderServices))
			{
				DefaultProviderServicesResolver.CheckKey(key);
			}
			return Enumerable.Empty<object>();
		}
	}
}
