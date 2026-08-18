using System;
using System.Configuration;
using System.Diagnostics.CodeAnalysis;

namespace System.Data.Entity.Internal.ConfigFile
{
	// Token: 0x020006BE RID: 1726
	internal class EntityFrameworkSection : ConfigurationSection
	{
		// Token: 0x17000A5D RID: 2653
		// (get) Token: 0x060044A5 RID: 17573 RVA: 0x001445E6 File Offset: 0x001427E6
		// (set) Token: 0x060044A6 RID: 17574 RVA: 0x001445F8 File Offset: 0x001427F8
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
		[ConfigurationProperty("defaultConnectionFactory")]
		public virtual DefaultConnectionFactoryElement DefaultConnectionFactory
		{
			get
			{
				return (DefaultConnectionFactoryElement)base["defaultConnectionFactory"];
			}
			set
			{
				base["defaultConnectionFactory"] = value;
			}
		}

		// Token: 0x17000A5E RID: 2654
		// (get) Token: 0x060044A7 RID: 17575 RVA: 0x00144606 File Offset: 0x00142806
		// (set) Token: 0x060044A8 RID: 17576 RVA: 0x00144618 File Offset: 0x00142818
		[ConfigurationProperty("codeConfigurationType")]
		public virtual string ConfigurationTypeName
		{
			get
			{
				return (string)base["codeConfigurationType"];
			}
			set
			{
				base["codeConfigurationType"] = value;
			}
		}

		// Token: 0x17000A5F RID: 2655
		// (get) Token: 0x060044A9 RID: 17577 RVA: 0x00144626 File Offset: 0x00142826
		[ConfigurationProperty("providers")]
		public virtual ProviderCollection Providers
		{
			get
			{
				return (ProviderCollection)base["providers"];
			}
		}

		// Token: 0x17000A60 RID: 2656
		// (get) Token: 0x060044AA RID: 17578 RVA: 0x00144638 File Offset: 0x00142838
		[ConfigurationProperty("contexts")]
		public virtual ContextCollection Contexts
		{
			get
			{
				return (ContextCollection)base["contexts"];
			}
		}

		// Token: 0x17000A61 RID: 2657
		// (get) Token: 0x060044AB RID: 17579 RVA: 0x0014464A File Offset: 0x0014284A
		[ConfigurationProperty("interceptors")]
		public virtual InterceptorsCollection Interceptors
		{
			get
			{
				return (InterceptorsCollection)base["interceptors"];
			}
		}

		// Token: 0x17000A62 RID: 2658
		// (get) Token: 0x060044AC RID: 17580 RVA: 0x0014465C File Offset: 0x0014285C
		// (set) Token: 0x060044AD RID: 17581 RVA: 0x0014466E File Offset: 0x0014286E
		[ConfigurationProperty("queryCache")]
		public virtual QueryCacheElement QueryCache
		{
			get
			{
				return (QueryCacheElement)base["queryCache"];
			}
			set
			{
				base["queryCache"] = value;
			}
		}

		// Token: 0x04001948 RID: 6472
		private const string DefaultConnectionFactoryKey = "defaultConnectionFactory";

		// Token: 0x04001949 RID: 6473
		private const string ContextsKey = "contexts";

		// Token: 0x0400194A RID: 6474
		private const string ProviderKey = "providers";

		// Token: 0x0400194B RID: 6475
		private const string ConfigurationTypeKey = "codeConfigurationType";

		// Token: 0x0400194C RID: 6476
		private const string InterceptorsKey = "interceptors";

		// Token: 0x0400194D RID: 6477
		private const string QueryCacheKey = "queryCache";
	}
}
