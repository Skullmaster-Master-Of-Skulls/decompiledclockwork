using System;
using System.Collections.Generic;
using System.Linq;

namespace TechnoPro.Common.Unity.IoC
{
	// Token: 0x02000002 RID: 2
	public abstract class ConventionInjection : IConventionInjection
	{
		// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		public virtual T ResolveByDefault<T>()
		{
			return (this.DefaultObjectMap != null && this.DefaultObjectMap.ContainsKey(typeof(T))) ? this.DefaultObjectMap[typeof(T)].GetInternalImplementation<T>() : default(T);
		}

		// Token: 0x06000002 RID: 2 RVA: 0x000020A8 File Offset: 0x000002A8
		public virtual bool Contains<T>()
		{
			return this.DefaultObjectMap != null && this.DefaultObjectMap.ContainsKey(typeof(T));
		}

		// Token: 0x06000003 RID: 3 RVA: 0x000020DC File Offset: 0x000002DC
		public virtual T ResolveByDefault<T>(string name)
		{
			NamedType nameType = new NamedType
			{
				Name = name,
				Type = typeof(T)
			};
			return (from key in this.DefaultNameObjectMap.Keys
			where key.Equals(nameType)
			select this.DefaultNameObjectMap[key].GetInternalImplementation<T>()).FirstOrDefault<T>();
		}

		// Token: 0x06000004 RID: 4 RVA: 0x00002154 File Offset: 0x00000354
		public virtual bool Contains<T>(string name)
		{
			NamedType value = new NamedType
			{
				Name = name,
				Type = typeof(T)
			};
			return this.DefaultNameObjectMap != null && this.DefaultNameObjectMap.Keys.Contains(value);
		}

		// Token: 0x06000005 RID: 5 RVA: 0x000021A3 File Offset: 0x000003A3
		public virtual void Initialize()
		{
		}

		// Token: 0x06000006 RID: 6 RVA: 0x000021A8 File Offset: 0x000003A8
		protected virtual IcwObject RetrieveIcwObject<T>(string lifetime)
		{
			return IcwLifetimeManager.GetIcwObject<T>(lifetime);
		}

		// Token: 0x04000001 RID: 1
		protected Dictionary<Type, IcwObject> DefaultObjectMap;

		// Token: 0x04000002 RID: 2
		protected Dictionary<NamedType, IcwObject> DefaultNameObjectMap;
	}
}
