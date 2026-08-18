using System;
using System.Collections.Generic;
using System.Configuration;

namespace System.Security.Authentication.ExtendedProtection.Configuration
{
	// Token: 0x0200044A RID: 1098
	public sealed class ExtendedProtectionPolicyElement : ConfigurationElement
	{
		// Token: 0x060028A9 RID: 10409 RVA: 0x000BAAFC File Offset: 0x000B8CFC
		public ExtendedProtectionPolicyElement()
		{
			this.properties.Add(this.policyEnforcement);
			this.properties.Add(this.protectionScenario);
			this.properties.Add(this.customServiceNames);
		}

		// Token: 0x170009FD RID: 2557
		// (get) Token: 0x060028AA RID: 10410 RVA: 0x000BABAF File Offset: 0x000B8DAF
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return this.properties;
			}
		}

		// Token: 0x170009FE RID: 2558
		// (get) Token: 0x060028AB RID: 10411 RVA: 0x000BABB7 File Offset: 0x000B8DB7
		// (set) Token: 0x060028AC RID: 10412 RVA: 0x000BABCA File Offset: 0x000B8DCA
		[ConfigurationProperty("policyEnforcement")]
		public PolicyEnforcement PolicyEnforcement
		{
			get
			{
				return (PolicyEnforcement)base[this.policyEnforcement];
			}
			set
			{
				base[this.policyEnforcement] = value;
			}
		}

		// Token: 0x170009FF RID: 2559
		// (get) Token: 0x060028AD RID: 10413 RVA: 0x000BABDE File Offset: 0x000B8DDE
		// (set) Token: 0x060028AE RID: 10414 RVA: 0x000BABF1 File Offset: 0x000B8DF1
		[ConfigurationProperty("protectionScenario", DefaultValue = ProtectionScenario.TransportSelected)]
		public ProtectionScenario ProtectionScenario
		{
			get
			{
				return (ProtectionScenario)base[this.protectionScenario];
			}
			set
			{
				base[this.protectionScenario] = value;
			}
		}

		// Token: 0x17000A00 RID: 2560
		// (get) Token: 0x060028AF RID: 10415 RVA: 0x000BAC05 File Offset: 0x000B8E05
		[ConfigurationProperty("customServiceNames")]
		public ServiceNameElementCollection CustomServiceNames
		{
			get
			{
				return (ServiceNameElementCollection)base[this.customServiceNames];
			}
		}

		// Token: 0x060028B0 RID: 10416 RVA: 0x000BAC18 File Offset: 0x000B8E18
		public ExtendedProtectionPolicy BuildPolicy()
		{
			if (this.PolicyEnforcement == PolicyEnforcement.Never)
			{
				return new ExtendedProtectionPolicy(PolicyEnforcement.Never);
			}
			ServiceNameCollection serviceNameCollection = null;
			ServiceNameElementCollection serviceNameElementCollection = this.CustomServiceNames;
			if (serviceNameElementCollection != null && serviceNameElementCollection.Count > 0)
			{
				List<string> list = new List<string>(serviceNameElementCollection.Count);
				foreach (object obj in serviceNameElementCollection)
				{
					ServiceNameElement serviceNameElement = (ServiceNameElement)obj;
					list.Add(serviceNameElement.Name);
				}
				serviceNameCollection = new ServiceNameCollection(list);
			}
			return new ExtendedProtectionPolicy(this.PolicyEnforcement, this.ProtectionScenario, serviceNameCollection);
		}

		// Token: 0x17000A01 RID: 2561
		// (get) Token: 0x060028B1 RID: 10417 RVA: 0x000BACC0 File Offset: 0x000B8EC0
		private static PolicyEnforcement DefaultPolicyEnforcement
		{
			get
			{
				return PolicyEnforcement.Never;
			}
		}

		// Token: 0x0400227C RID: 8828
		private ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();

		// Token: 0x0400227D RID: 8829
		private readonly ConfigurationProperty policyEnforcement = new ConfigurationProperty("policyEnforcement", typeof(PolicyEnforcement), ExtendedProtectionPolicyElement.DefaultPolicyEnforcement, ConfigurationPropertyOptions.None);

		// Token: 0x0400227E RID: 8830
		private readonly ConfigurationProperty protectionScenario = new ConfigurationProperty("protectionScenario", typeof(ProtectionScenario), ProtectionScenario.TransportSelected, ConfigurationPropertyOptions.None);

		// Token: 0x0400227F RID: 8831
		private readonly ConfigurationProperty customServiceNames = new ConfigurationProperty("customServiceNames", typeof(ServiceNameElementCollection), null, ConfigurationPropertyOptions.None);
	}
}
