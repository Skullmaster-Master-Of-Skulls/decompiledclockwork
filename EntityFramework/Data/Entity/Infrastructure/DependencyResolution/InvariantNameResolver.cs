using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity.Resources;

namespace System.Data.Entity.Infrastructure.DependencyResolution
{
	// Token: 0x02000161 RID: 353
	internal class InvariantNameResolver : IDbDependencyResolver
	{
		// Token: 0x06000B76 RID: 2934 RVA: 0x00039073 File Offset: 0x00037273
		public InvariantNameResolver(DbProviderFactory providerFactory, string invariantName)
		{
			this._invariantName = new ProviderInvariantName(invariantName);
			this._providerFactoryType = providerFactory.GetType();
		}

		// Token: 0x06000B77 RID: 2935 RVA: 0x00039094 File Offset: 0x00037294
		public virtual object GetService(Type type, object key)
		{
			if (type == typeof(IProviderInvariantName))
			{
				if (!(key is DbProviderFactory))
				{
					throw new ArgumentException(Strings.DbDependencyResolver_InvalidKey(typeof(DbProviderFactory).Name, typeof(IProviderInvariantName)));
				}
				if (key.GetType() == this._providerFactoryType)
				{
					return this._invariantName;
				}
			}
			return null;
		}

		// Token: 0x06000B78 RID: 2936 RVA: 0x000390FC File Offset: 0x000372FC
		public override bool Equals(object obj)
		{
			InvariantNameResolver invariantNameResolver = obj as InvariantNameResolver;
			return invariantNameResolver != null && this._providerFactoryType == invariantNameResolver._providerFactoryType && this._invariantName.Name == invariantNameResolver._invariantName.Name;
		}

		// Token: 0x06000B79 RID: 2937 RVA: 0x00039145 File Offset: 0x00037345
		public override int GetHashCode()
		{
			return this._invariantName.Name.GetHashCode();
		}

		// Token: 0x06000B7A RID: 2938 RVA: 0x00039157 File Offset: 0x00037357
		public IEnumerable<object> GetServices(Type type, object key)
		{
			return this.GetServiceAsServices(type, key);
		}

		// Token: 0x04000328 RID: 808
		private readonly IProviderInvariantName _invariantName;

		// Token: 0x04000329 RID: 809
		private readonly Type _providerFactoryType;
	}
}
