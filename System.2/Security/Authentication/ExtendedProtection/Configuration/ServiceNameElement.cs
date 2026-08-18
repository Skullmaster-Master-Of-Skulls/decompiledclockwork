using System;
using System.Configuration;

namespace System.Security.Authentication.ExtendedProtection.Configuration
{
	// Token: 0x0200044C RID: 1100
	public sealed class ServiceNameElement : ConfigurationElement
	{
		// Token: 0x060028BF RID: 10431 RVA: 0x000BAD84 File Offset: 0x000B8F84
		public ServiceNameElement()
		{
			this.properties.Add(this.name);
		}

		// Token: 0x17000A04 RID: 2564
		// (get) Token: 0x060028C0 RID: 10432 RVA: 0x000BADC4 File Offset: 0x000B8FC4
		// (set) Token: 0x060028C1 RID: 10433 RVA: 0x000BADD7 File Offset: 0x000B8FD7
		[ConfigurationProperty("name")]
		public string Name
		{
			get
			{
				return (string)base[this.name];
			}
			set
			{
				base[this.name] = value;
			}
		}

		// Token: 0x17000A05 RID: 2565
		// (get) Token: 0x060028C2 RID: 10434 RVA: 0x000BADE6 File Offset: 0x000B8FE6
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return this.properties;
			}
		}

		// Token: 0x17000A06 RID: 2566
		// (get) Token: 0x060028C3 RID: 10435 RVA: 0x000BADEE File Offset: 0x000B8FEE
		internal string Key
		{
			get
			{
				return this.Name;
			}
		}

		// Token: 0x04002280 RID: 8832
		private ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();

		// Token: 0x04002281 RID: 8833
		private readonly ConfigurationProperty name = new ConfigurationProperty("name", typeof(string), null, ConfigurationPropertyOptions.IsRequired);
	}
}
