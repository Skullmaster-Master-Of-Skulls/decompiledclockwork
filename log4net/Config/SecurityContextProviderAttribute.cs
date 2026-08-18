using System;
using System.Reflection;
using log4net.Core;
using log4net.Repository;
using log4net.Util;

namespace log4net.Config
{
	// Token: 0x02000054 RID: 84
	[AttributeUsage(AttributeTargets.Assembly)]
	[Serializable]
	public sealed class SecurityContextProviderAttribute : ConfiguratorAttribute
	{
		// Token: 0x060002C0 RID: 704 RVA: 0x0000959E File Offset: 0x0000779E
		public SecurityContextProviderAttribute(Type providerType) : base(100)
		{
			this.m_providerType = providerType;
		}

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x060002C1 RID: 705 RVA: 0x000095AF File Offset: 0x000077AF
		// (set) Token: 0x060002C2 RID: 706 RVA: 0x000095B7 File Offset: 0x000077B7
		public Type ProviderType
		{
			get
			{
				return this.m_providerType;
			}
			set
			{
				this.m_providerType = value;
			}
		}

		// Token: 0x060002C3 RID: 707 RVA: 0x000095C0 File Offset: 0x000077C0
		public override void Configure(Assembly sourceAssembly, ILoggerRepository targetRepository)
		{
			if (this.m_providerType == null)
			{
				LogLog.Error(SecurityContextProviderAttribute.declaringType, "Attribute specified on assembly [" + sourceAssembly.FullName + "] with null ProviderType.");
				return;
			}
			LogLog.Debug(SecurityContextProviderAttribute.declaringType, "Creating provider of type [" + this.m_providerType.FullName + "]");
			SecurityContextProvider securityContextProvider = Activator.CreateInstance(this.m_providerType) as SecurityContextProvider;
			if (securityContextProvider == null)
			{
				LogLog.Error(SecurityContextProviderAttribute.declaringType, "Failed to create SecurityContextProvider instance of type [" + this.m_providerType.Name + "].");
				return;
			}
			SecurityContextProvider.DefaultProvider = securityContextProvider;
		}

		// Token: 0x0400014F RID: 335
		private Type m_providerType;

		// Token: 0x04000150 RID: 336
		private static readonly Type declaringType = typeof(SecurityContextProviderAttribute);
	}
}
