using System;
using System.ComponentModel;
using System.Configuration;
using System.ServiceModel.Configuration;
using System.Xml;
using System.Xml.Linq;

namespace System.ServiceModel.Discovery.Configuration
{
	// Token: 0x020000B9 RID: 185
	public sealed class FindCriteriaElement : ConfigurationElement
	{
		// Token: 0x17000145 RID: 325
		// (get) Token: 0x0600076F RID: 1903 RVA: 0x0001315B File Offset: 0x0001135B
		[ConfigurationProperty("types")]
		public ContractTypeNameElementCollection ContractTypeNames
		{
			get
			{
				return (ContractTypeNameElementCollection)base["types"];
			}
		}

		// Token: 0x17000146 RID: 326
		// (get) Token: 0x06000770 RID: 1904 RVA: 0x0001316D File Offset: 0x0001136D
		[ConfigurationProperty("scopes")]
		public ScopeElementCollection Scopes
		{
			get
			{
				return (ScopeElementCollection)base["scopes"];
			}
		}

		// Token: 0x17000147 RID: 327
		// (get) Token: 0x06000771 RID: 1905 RVA: 0x000133AC File Offset: 0x000115AC
		// (set) Token: 0x06000772 RID: 1906 RVA: 0x000133BE File Offset: 0x000115BE
		[ConfigurationProperty("scopeMatchBy")]
		public Uri ScopeMatchBy
		{
			get
			{
				return (Uri)base["scopeMatchBy"];
			}
			set
			{
				if (value == null)
				{
					throw FxTrace.Exception.ArgumentNull("value");
				}
				base["scopeMatchBy"] = value;
			}
		}

		// Token: 0x17000148 RID: 328
		// (get) Token: 0x06000773 RID: 1907 RVA: 0x0001317F File Offset: 0x0001137F
		[ConfigurationProperty("extensions")]
		public XmlElementElementCollection Extensions
		{
			get
			{
				return (XmlElementElementCollection)base["extensions"];
			}
		}

		// Token: 0x17000149 RID: 329
		// (get) Token: 0x06000774 RID: 1908 RVA: 0x000133E5 File Offset: 0x000115E5
		// (set) Token: 0x06000775 RID: 1909 RVA: 0x000133F7 File Offset: 0x000115F7
		[ConfigurationProperty("duration", DefaultValue = "00:00:20")]
		[TypeConverter(typeof(TimeSpanOrInfiniteConverter))]
		[ServiceModelTimeSpanValidator(MinValueString = "00:00:00.001")]
		public TimeSpan Duration
		{
			get
			{
				return (TimeSpan)base["duration"];
			}
			set
			{
				base["duration"] = value;
			}
		}

		// Token: 0x1700014A RID: 330
		// (get) Token: 0x06000776 RID: 1910 RVA: 0x0001340A File Offset: 0x0001160A
		// (set) Token: 0x06000777 RID: 1911 RVA: 0x0001341C File Offset: 0x0001161C
		[ConfigurationProperty("maxResults", DefaultValue = 2147483647)]
		[IntegerValidator(MinValue = 1, MaxValue = 2147483647)]
		public int MaxResults
		{
			get
			{
				return (int)base["maxResults"];
			}
			set
			{
				base["maxResults"] = value;
			}
		}

		// Token: 0x1700014B RID: 331
		// (get) Token: 0x06000778 RID: 1912 RVA: 0x00013430 File Offset: 0x00011630
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				if (this.properties == null)
				{
					this.properties = new ConfigurationPropertyCollection
					{
						new ConfigurationProperty("types", typeof(ContractTypeNameElementCollection), null, null, null, ConfigurationPropertyOptions.None),
						new ConfigurationProperty("scopeMatchBy", typeof(Uri), DiscoveryDefaults.ScopeMatchBy, null, null, ConfigurationPropertyOptions.None),
						new ConfigurationProperty("scopes", typeof(ScopeElementCollection), null, null, null, ConfigurationPropertyOptions.None),
						new ConfigurationProperty("extensions", typeof(XmlElementElementCollection), null, null, null, ConfigurationPropertyOptions.None),
						new ConfigurationProperty("duration", typeof(TimeSpan), TimeSpan.FromSeconds(20.0), new TimeSpanOrInfiniteConverter(), new TimeSpanOrInfiniteValidator(TimeSpan.FromMilliseconds(1.0), TimeSpan.MaxValue), ConfigurationPropertyOptions.None),
						new ConfigurationProperty("maxResults", typeof(int), int.MaxValue, null, new IntegerValidator(1, int.MaxValue), ConfigurationPropertyOptions.None)
					};
				}
				return this.properties;
			}
		}

		// Token: 0x06000779 RID: 1913 RVA: 0x00013554 File Offset: 0x00011754
		internal void ApplyConfiguration(FindCriteria findCriteria)
		{
			foreach (object obj in this.ContractTypeNames)
			{
				ContractTypeNameElement contractTypeNameElement = (ContractTypeNameElement)obj;
				findCriteria.ContractTypeNames.Add(new XmlQualifiedName(contractTypeNameElement.Name, contractTypeNameElement.Namespace));
			}
			foreach (object obj2 in this.Scopes)
			{
				ScopeElement scopeElement = (ScopeElement)obj2;
				findCriteria.Scopes.Add(scopeElement.Scope);
			}
			foreach (object obj3 in this.Extensions)
			{
				XmlElementElement xmlElementElement = (XmlElementElement)obj3;
				findCriteria.Extensions.Add(XElement.Parse(xmlElementElement.XmlElement.OuterXml));
			}
			findCriteria.ScopeMatchBy = this.ScopeMatchBy;
			findCriteria.Duration = this.Duration;
			findCriteria.MaxResults = this.MaxResults;
		}

		// Token: 0x0600077A RID: 1914 RVA: 0x0001369C File Offset: 0x0001189C
		internal void CopyFrom(FindCriteriaElement source)
		{
			foreach (object obj in source.ContractTypeNames)
			{
				ContractTypeNameElement element = (ContractTypeNameElement)obj;
				this.ContractTypeNames.Add(element);
			}
			foreach (object obj2 in source.Scopes)
			{
				ScopeElement element2 = (ScopeElement)obj2;
				this.Scopes.Add(element2);
			}
			foreach (object obj3 in source.Extensions)
			{
				XmlElementElement element3 = (XmlElementElement)obj3;
				this.Extensions.Add(element3);
			}
			this.ScopeMatchBy = source.ScopeMatchBy;
			this.Duration = source.Duration;
			this.MaxResults = source.MaxResults;
		}

		// Token: 0x040001CD RID: 461
		private ConfigurationPropertyCollection properties;
	}
}
