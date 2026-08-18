using System;
using System.Configuration;
using System.ServiceModel.Configuration;
using System.Xml;
using System.Xml.Linq;

namespace System.ServiceModel.Discovery.Configuration
{
	// Token: 0x020000B8 RID: 184
	public sealed class EndpointDiscoveryElement : BehaviorExtensionElement
	{
		// Token: 0x1700013F RID: 319
		// (get) Token: 0x06000767 RID: 1895 RVA: 0x0001312A File Offset: 0x0001132A
		public override Type BehaviorType
		{
			get
			{
				return typeof(EndpointDiscoveryBehavior);
			}
		}

		// Token: 0x17000140 RID: 320
		// (get) Token: 0x06000768 RID: 1896 RVA: 0x00013136 File Offset: 0x00011336
		// (set) Token: 0x06000769 RID: 1897 RVA: 0x00013148 File Offset: 0x00011348
		[ConfigurationProperty("enabled", DefaultValue = true)]
		public bool Enabled
		{
			get
			{
				return (bool)base["enabled"];
			}
			set
			{
				base["enabled"] = value;
			}
		}

		// Token: 0x17000141 RID: 321
		// (get) Token: 0x0600076A RID: 1898 RVA: 0x0001315B File Offset: 0x0001135B
		[ConfigurationProperty("types")]
		public ContractTypeNameElementCollection ContractTypeNames
		{
			get
			{
				return (ContractTypeNameElementCollection)base["types"];
			}
		}

		// Token: 0x17000142 RID: 322
		// (get) Token: 0x0600076B RID: 1899 RVA: 0x0001316D File Offset: 0x0001136D
		[ConfigurationProperty("scopes")]
		public ScopeElementCollection Scopes
		{
			get
			{
				return (ScopeElementCollection)base["scopes"];
			}
		}

		// Token: 0x17000143 RID: 323
		// (get) Token: 0x0600076C RID: 1900 RVA: 0x0001317F File Offset: 0x0001137F
		[ConfigurationProperty("extensions")]
		public XmlElementElementCollection Extensions
		{
			get
			{
				return (XmlElementElementCollection)base["extensions"];
			}
		}

		// Token: 0x17000144 RID: 324
		// (get) Token: 0x0600076D RID: 1901 RVA: 0x00013194 File Offset: 0x00011394
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				if (this.properties == null)
				{
					this.properties = new ConfigurationPropertyCollection
					{
						new ConfigurationProperty("enabled", typeof(bool), true, null, null, ConfigurationPropertyOptions.None),
						new ConfigurationProperty("types", typeof(ContractTypeNameElementCollection), null, null, null, ConfigurationPropertyOptions.None),
						new ConfigurationProperty("scopes", typeof(ScopeElementCollection), null, null, null, ConfigurationPropertyOptions.None),
						new ConfigurationProperty("extensions", typeof(XmlElementElementCollection), null, null, null, ConfigurationPropertyOptions.None)
					};
				}
				return this.properties;
			}
		}

		// Token: 0x0600076E RID: 1902 RVA: 0x0001323C File Offset: 0x0001143C
		protected internal override object CreateBehavior()
		{
			EndpointDiscoveryBehavior endpointDiscoveryBehavior = new EndpointDiscoveryBehavior();
			endpointDiscoveryBehavior.Enabled = this.Enabled;
			if (this.Scopes != null && this.Scopes.Count > 0)
			{
				foreach (object obj in this.Scopes)
				{
					ScopeElement scopeElement = (ScopeElement)obj;
					endpointDiscoveryBehavior.Scopes.Add(scopeElement.Scope);
				}
			}
			if (this.ContractTypeNames != null)
			{
				foreach (object obj2 in this.ContractTypeNames)
				{
					ContractTypeNameElement contractTypeNameElement = (ContractTypeNameElement)obj2;
					endpointDiscoveryBehavior.ContractTypeNames.Add(new XmlQualifiedName(contractTypeNameElement.Name, contractTypeNameElement.Namespace));
				}
			}
			if (this.Extensions != null && this.Extensions.Count > 0)
			{
				foreach (object obj3 in this.Extensions)
				{
					XmlElementElement xmlElementElement = (XmlElementElement)obj3;
					endpointDiscoveryBehavior.Extensions.Add(XElement.Parse(xmlElementElement.XmlElement.OuterXml));
				}
			}
			return endpointDiscoveryBehavior;
		}

		// Token: 0x040001CC RID: 460
		private ConfigurationPropertyCollection properties;
	}
}
