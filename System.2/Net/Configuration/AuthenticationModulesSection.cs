using System;
using System.Configuration;

namespace System.Net.Configuration
{
	// Token: 0x02000326 RID: 806
	public sealed class AuthenticationModulesSection : ConfigurationSection
	{
		// Token: 0x06001CF5 RID: 7413 RVA: 0x0008A950 File Offset: 0x00088B50
		public AuthenticationModulesSection()
		{
			this.properties.Add(this.authenticationModules);
		}

		// Token: 0x06001CF6 RID: 7414 RVA: 0x0008A98C File Offset: 0x00088B8C
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

		// Token: 0x17000724 RID: 1828
		// (get) Token: 0x06001CF7 RID: 7415 RVA: 0x0008A9E4 File Offset: 0x00088BE4
		[ConfigurationProperty("", IsDefaultCollection = true)]
		public AuthenticationModuleElementCollection AuthenticationModules
		{
			get
			{
				return (AuthenticationModuleElementCollection)base[this.authenticationModules];
			}
		}

		// Token: 0x06001CF8 RID: 7416 RVA: 0x0008A9F8 File Offset: 0x00088BF8
		protected override void InitializeDefault()
		{
			this.AuthenticationModules.Add(new AuthenticationModuleElement(typeof(NegotiateClient).AssemblyQualifiedName));
			this.AuthenticationModules.Add(new AuthenticationModuleElement(typeof(KerberosClient).AssemblyQualifiedName));
			this.AuthenticationModules.Add(new AuthenticationModuleElement(typeof(NtlmClient).AssemblyQualifiedName));
			this.AuthenticationModules.Add(new AuthenticationModuleElement(typeof(DigestClient).AssemblyQualifiedName));
			this.AuthenticationModules.Add(new AuthenticationModuleElement(typeof(BasicClient).AssemblyQualifiedName));
		}

		// Token: 0x17000725 RID: 1829
		// (get) Token: 0x06001CF9 RID: 7417 RVA: 0x0008AAA0 File Offset: 0x00088CA0
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return this.properties;
			}
		}

		// Token: 0x04001BC8 RID: 7112
		private ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();

		// Token: 0x04001BC9 RID: 7113
		private readonly ConfigurationProperty authenticationModules = new ConfigurationProperty(null, typeof(AuthenticationModuleElementCollection), null, ConfigurationPropertyOptions.IsDefaultCollection);
	}
}
