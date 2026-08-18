using System;
using System.Data.Entity.Core.Common;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;
using System.Reflection;

namespace System.Data.Entity.Infrastructure.DependencyResolution
{
	// Token: 0x02000163 RID: 355
	internal class ProviderServicesFactory
	{
		// Token: 0x06000B7E RID: 2942 RVA: 0x00039188 File Offset: 0x00037388
		public virtual DbProviderServices TryGetInstance(string providerTypeName)
		{
			Type type = Type.GetType(providerTypeName, false);
			if (!(type == null))
			{
				return ProviderServicesFactory.GetInstance(type);
			}
			return null;
		}

		// Token: 0x06000B7F RID: 2943 RVA: 0x000391B0 File Offset: 0x000373B0
		public virtual DbProviderServices GetInstance(string providerTypeName, string providerInvariantName)
		{
			Type type = Type.GetType(providerTypeName, false);
			if (type == null)
			{
				throw new InvalidOperationException(Strings.EF6Providers_ProviderTypeMissing(providerTypeName, providerInvariantName));
			}
			return ProviderServicesFactory.GetInstance(type);
		}

		// Token: 0x06000B80 RID: 2944 RVA: 0x000391E4 File Offset: 0x000373E4
		private static DbProviderServices GetInstance(Type providerType)
		{
			MemberInfo memberInfo = providerType.GetStaticProperty("Instance") ?? providerType.GetField("Instance", BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
			if (memberInfo == null)
			{
				throw new InvalidOperationException(Strings.EF6Providers_InstanceMissing(providerType.AssemblyQualifiedName));
			}
			DbProviderServices dbProviderServices = memberInfo.GetValue() as DbProviderServices;
			if (dbProviderServices == null)
			{
				throw new InvalidOperationException(Strings.EF6Providers_NotDbProviderServices(providerType.AssemblyQualifiedName));
			}
			return dbProviderServices;
		}
	}
}
