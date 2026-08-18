using System;
using System.Configuration;

namespace System.Net.Configuration
{
	// Token: 0x02000643 RID: 1603
	public sealed class AuthenticationModulesSection : ConfigurationSection
	{
		// Token: 0x060031AC RID: 12716 RVA: 0x000D482E File Offset: 0x000D382E
		public AuthenticationModulesSection()
		{
			this.properties.Add(this.authenticationModules);
		}

		// Token: 0x060031AD RID: 12717 RVA: 0x000D486C File Offset: 0x000D386C
		protected override void PostDeserialize()
		{
			if (base.EvaluationContext.IsMachineLevel)
			{
				return;
			}
			try
			{
				ExceptionHelper.UnmanagedPermission.Demand();
			}
			catch (Exception inner)
			{
				throw new ConfigurationErrorsException(SR.GetString("net_config_section_permission", new object[]
				{
					"authenticationModules"
				}), inner);
			}
		}

		// Token: 0x17000B5A RID: 2906
		// (get) Token: 0x060031AE RID: 12718 RVA: 0x000D48C8 File Offset: 0x000D38C8
		[ConfigurationProperty("", IsDefaultCollection = true)]
		public AuthenticationModuleElementCollection AuthenticationModules
		{
			get
			{
				return (AuthenticationModuleElementCollection)base[this.authenticationModules];
			}
		}

		// Token: 0x060031AF RID: 12719 RVA: 0x000D48DC File Offset: 0x000D38DC
		protected override void InitializeDefault()
		{
			this.AuthenticationModules.Add(new AuthenticationModuleElement(typeof(NegotiateClient).AssemblyQualifiedName));
			this.AuthenticationModules.Add(new AuthenticationModuleElement(typeof(KerberosClient).AssemblyQualifiedName));
			this.AuthenticationModules.Add(new AuthenticationModuleElement(typeof(NtlmClient).AssemblyQualifiedName));
			this.AuthenticationModules.Add(new AuthenticationModuleElement(typeof(DigestClient).AssemblyQualifiedName));
			this.AuthenticationModules.Add(new AuthenticationModuleElement(typeof(BasicClient).AssemblyQualifiedName));
		}

		// Token: 0x17000B5B RID: 2907
		// (get) Token: 0x060031B0 RID: 12720 RVA: 0x000D4984 File Offset: 0x000D3984
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return this.properties;
			}
		}

		// Token: 0x04002EA1 RID: 11937
		private ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();

		// Token: 0x04002EA2 RID: 11938
		private readonly ConfigurationProperty authenticationModules = new ConfigurationProperty(null, typeof(AuthenticationModuleElementCollection), null, ConfigurationPropertyOptions.IsDefaultCollection);
	}
}
