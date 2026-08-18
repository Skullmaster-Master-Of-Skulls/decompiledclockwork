using System;
using System.Collections.Generic;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;
using System.Linq;
using System.Reflection;

namespace System.Data.Entity.Infrastructure.DependencyResolution
{
	// Token: 0x02000157 RID: 343
	internal class DbConfigurationFinder
	{
		// Token: 0x06000B2C RID: 2860 RVA: 0x000380B5 File Offset: 0x000362B5
		public virtual Type TryFindConfigurationType(Type contextType, IEnumerable<Type> typesToSearch = null)
		{
			return this.TryFindConfigurationType(contextType.Assembly(), contextType, typesToSearch);
		}

		// Token: 0x06000B2D RID: 2861 RVA: 0x000380F4 File Offset: 0x000362F4
		public virtual Type TryFindConfigurationType(Assembly assemblyHint, Type contextTypeHint, IEnumerable<Type> typesToSearch = null)
		{
			if (contextTypeHint != null)
			{
				Type type = (from a in contextTypeHint.GetCustomAttributes(true)
				select a.ConfigurationType).FirstOrDefault<Type>();
				if (type != null)
				{
					if (!typeof(DbConfiguration).IsAssignableFrom(type))
					{
						throw new InvalidOperationException(Strings.CreateInstance_BadDbConfigurationType(type.ToString(), typeof(DbConfiguration).ToString()));
					}
					return type;
				}
			}
			List<Type> list = (from t in typesToSearch ?? assemblyHint.GetAccessibleTypes()
			where t.IsSubclassOf(typeof(DbConfiguration)) && !t.IsAbstract() && !t.IsGenericType()
			select t).ToList<Type>();
			if (list.Count > 1)
			{
				throw new InvalidOperationException(Strings.MultipleConfigsInAssembly(list.First<Type>().Assembly(), typeof(DbConfiguration).Name));
			}
			return list.FirstOrDefault<Type>();
		}

		// Token: 0x06000B2E RID: 2862 RVA: 0x00038210 File Offset: 0x00036410
		public virtual Type TryFindContextType(Assembly assemblyHint, Type contextTypeHint, IEnumerable<Type> typesToSearch = null)
		{
			if (contextTypeHint != null)
			{
				return contextTypeHint;
			}
			List<Type> list = (from t in typesToSearch ?? assemblyHint.GetAccessibleTypes()
			where t.IsSubclassOf(typeof(DbContext)) && !t.IsAbstract() && !t.IsGenericType() && t.GetCustomAttributes(true).Any<DbConfigurationTypeAttribute>()
			select t).ToList<Type>();
			if (list.Count != 1)
			{
				return null;
			}
			return list[0];
		}
	}
}
