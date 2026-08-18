using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.ServiceModel.Description;

namespace System.ServiceModel.Configuration
{
	// Token: 0x0200067C RID: 1660
	public class StandardEndpointCollectionElement<TStandardEndpoint, TEndpointConfiguration> : EndpointCollectionElement where TStandardEndpoint : ServiceEndpoint where TEndpointConfiguration : StandardEndpointElement, new()
	{
		// Token: 0x17000FF4 RID: 4084
		// (get) Token: 0x06003FC8 RID: 16328 RVA: 0x000F1B84 File Offset: 0x000EFD84
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				if (this.properties == null)
				{
					this.properties = new ConfigurationPropertyCollection
					{
						new ConfigurationProperty("", typeof(StandardEndpointElementCollection<TEndpointConfiguration>), null, null, null, ConfigurationPropertyOptions.IsDefaultCollection)
					};
				}
				return this.properties;
			}
		}

		// Token: 0x17000FF5 RID: 4085
		// (get) Token: 0x06003FC9 RID: 16329 RVA: 0x000F1BCA File Offset: 0x000EFDCA
		[ConfigurationProperty("", Options = ConfigurationPropertyOptions.IsDefaultCollection)]
		public StandardEndpointElementCollection<TEndpointConfiguration> Endpoints
		{
			get
			{
				return (StandardEndpointElementCollection<TEndpointConfiguration>)base[""];
			}
		}

		// Token: 0x17000FF6 RID: 4086
		// (get) Token: 0x06003FCA RID: 16330 RVA: 0x000F1BDC File Offset: 0x000EFDDC
		public override Type EndpointType
		{
			get
			{
				return typeof(TStandardEndpoint);
			}
		}

		// Token: 0x17000FF7 RID: 4087
		// (get) Token: 0x06003FCB RID: 16331 RVA: 0x000F1BE8 File Offset: 0x000EFDE8
		public override ReadOnlyCollection<StandardEndpointElement> ConfiguredEndpoints
		{
			get
			{
				List<StandardEndpointElement> list = new List<StandardEndpointElement>();
				foreach (object obj in this.Endpoints)
				{
					StandardEndpointElement item = (StandardEndpointElement)obj;
					list.Add(item);
				}
				return new ReadOnlyCollection<StandardEndpointElement>(list);
			}
		}

		// Token: 0x06003FCC RID: 16332 RVA: 0x000F1C50 File Offset: 0x000EFE50
		public override bool ContainsKey(string name)
		{
			return this.Endpoints.ContainsKey(name);
		}

		// Token: 0x06003FCD RID: 16333 RVA: 0x000F1C6B File Offset: 0x000EFE6B
		protected internal override StandardEndpointElement GetDefaultStandardEndpointElement()
		{
			return Activator.CreateInstance<TEndpointConfiguration>();
		}

		// Token: 0x06003FCE RID: 16334 RVA: 0x000F1C78 File Offset: 0x000EFE78
		protected internal override bool TryAdd(string name, ServiceEndpoint endpoint, Configuration config)
		{
			bool flag = endpoint.GetType() == typeof(TStandardEndpoint) && typeof(StandardEndpointElement).IsAssignableFrom(typeof(TEndpointConfiguration));
			if (flag)
			{
				TEndpointConfiguration tendpointConfiguration = Activator.CreateInstance<TEndpointConfiguration>();
				tendpointConfiguration.Name = name;
				tendpointConfiguration.InitializeFrom(endpoint);
				this.Endpoints.Add(tendpointConfiguration);
			}
			return flag;
		}

		// Token: 0x04002CC0 RID: 11456
		private ConfigurationPropertyCollection properties;
	}
}
