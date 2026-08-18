using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.ServiceModel.Description;
using System.Web.Services.Description;
using System.Xml;
using System.Xml.Schema;

namespace System.ServiceModel.Administration
{
	// Token: 0x0200044B RID: 1099
	internal sealed class ServiceInfo
	{
		// Token: 0x06002ACC RID: 10956 RVA: 0x000A71B4 File Offset: 0x000A53B4
		internal ServiceInfo(ServiceHostBase service)
		{
			this.service = service;
			this.behaviors = service.Description.Behaviors;
			this.serviceName = service.Description.Name;
			this.endpoints = new EndpointInfoCollection(service.Description.Endpoints, this.ServiceName);
		}

		// Token: 0x17000A74 RID: 2676
		// (get) Token: 0x06002ACD RID: 10957 RVA: 0x000A720C File Offset: 0x000A540C
		public string ConfigurationName
		{
			get
			{
				return this.service.Description.ConfigurationName;
			}
		}

		// Token: 0x17000A75 RID: 2677
		// (get) Token: 0x06002ACE RID: 10958 RVA: 0x000A721E File Offset: 0x000A541E
		public string DistinguishedName
		{
			get
			{
				return this.serviceName + "@" + this.FirstAddress;
			}
		}

		// Token: 0x17000A76 RID: 2678
		// (get) Token: 0x06002ACF RID: 10959 RVA: 0x000A7238 File Offset: 0x000A5438
		public string FirstAddress
		{
			get
			{
				string result = "";
				if (this.Service.BaseAddresses.Count > 0)
				{
					result = this.Service.BaseAddresses[0].ToString();
				}
				else if (this.Endpoints.Count > 0)
				{
					Uri address = this.Endpoints[0].Address;
					if (null != address)
					{
						result = address.ToString();
					}
				}
				return result;
			}
		}

		// Token: 0x17000A77 RID: 2679
		// (get) Token: 0x06002AD0 RID: 10960 RVA: 0x000A72A8 File Offset: 0x000A54A8
		public string Name
		{
			get
			{
				return this.serviceName;
			}
		}

		// Token: 0x17000A78 RID: 2680
		// (get) Token: 0x06002AD1 RID: 10961 RVA: 0x000A72B0 File Offset: 0x000A54B0
		public string Namespace
		{
			get
			{
				return this.service.Description.Namespace;
			}
		}

		// Token: 0x17000A79 RID: 2681
		// (get) Token: 0x06002AD2 RID: 10962 RVA: 0x000A72C2 File Offset: 0x000A54C2
		public string ServiceName
		{
			get
			{
				return this.serviceName;
			}
		}

		// Token: 0x17000A7A RID: 2682
		// (get) Token: 0x06002AD3 RID: 10963 RVA: 0x000A72CA File Offset: 0x000A54CA
		public ServiceHostBase Service
		{
			get
			{
				return this.service;
			}
		}

		// Token: 0x17000A7B RID: 2683
		// (get) Token: 0x06002AD4 RID: 10964 RVA: 0x000A72D2 File Offset: 0x000A54D2
		public KeyedByTypeCollection<IServiceBehavior> Behaviors
		{
			get
			{
				return this.behaviors;
			}
		}

		// Token: 0x17000A7C RID: 2684
		// (get) Token: 0x06002AD5 RID: 10965 RVA: 0x000A72DA File Offset: 0x000A54DA
		public CommunicationState State
		{
			get
			{
				return this.Service.State;
			}
		}

		// Token: 0x17000A7D RID: 2685
		// (get) Token: 0x06002AD6 RID: 10966 RVA: 0x000A72E7 File Offset: 0x000A54E7
		public EndpointInfoCollection Endpoints
		{
			get
			{
				return this.endpoints;
			}
		}

		// Token: 0x17000A7E RID: 2686
		// (get) Token: 0x06002AD7 RID: 10967 RVA: 0x000A72F0 File Offset: 0x000A54F0
		public string[] Metadata
		{
			get
			{
				string[] array = null;
				ServiceMetadataExtension serviceMetadataExtension = this.service.Extensions.Find<ServiceMetadataExtension>();
				if (serviceMetadataExtension != null)
				{
					Collection<string> collection = new Collection<string>();
					try
					{
						foreach (MetadataSection metadataSection in serviceMetadataExtension.Metadata.MetadataSections)
						{
							using (StringWriter stringWriter = new StringWriter(CultureInfo.InvariantCulture))
							{
								if (metadataSection.Metadata is System.Web.Services.Description.ServiceDescription)
								{
									System.Web.Services.Description.ServiceDescription serviceDescription = (System.Web.Services.Description.ServiceDescription)metadataSection.Metadata;
									serviceDescription.Write(stringWriter);
									collection.Add(stringWriter.ToString());
								}
								else
								{
									if (metadataSection.Metadata is XmlElement)
									{
										XmlElement xmlElement = (XmlElement)metadataSection.Metadata;
										using (XmlWriter xmlWriter = XmlWriter.Create(stringWriter))
										{
											xmlElement.WriteTo(xmlWriter);
											collection.Add(stringWriter.ToString());
											continue;
										}
									}
									if (metadataSection.Metadata is XmlSchema)
									{
										XmlSchema xmlSchema = (XmlSchema)metadataSection.Metadata;
										xmlSchema.Write(stringWriter);
										collection.Add(stringWriter.ToString());
									}
									else
									{
										collection.Add(metadataSection.Metadata.ToString());
									}
								}
							}
						}
					}
					catch (InvalidOperationException ex)
					{
						collection.Add(ex.ToString());
					}
					array = new string[collection.Count];
					collection.CopyTo(array, 0);
				}
				return array;
			}
		}

		// Token: 0x04002402 RID: 9218
		private KeyedByTypeCollection<IServiceBehavior> behaviors;

		// Token: 0x04002403 RID: 9219
		private EndpointInfoCollection endpoints;

		// Token: 0x04002404 RID: 9220
		private ServiceHostBase service;

		// Token: 0x04002405 RID: 9221
		private string serviceName;
	}
}
