using System;
using System.Globalization;
using System.Runtime.Diagnostics;
using System.ServiceModel.Configuration;
using System.Xml;

namespace System.ServiceModel.Diagnostics
{
	// Token: 0x02000A91 RID: 2705
	internal class ServiceConfigurationTraceRecord : TraceRecord
	{
		// Token: 0x06006B05 RID: 27397 RVA: 0x0018ECAC File Offset: 0x0018CEAC
		internal ServiceConfigurationTraceRecord(ServiceElement serviceElement)
		{
			this.serviceElement = serviceElement;
		}

		// Token: 0x17001969 RID: 6505
		// (get) Token: 0x06006B06 RID: 27398 RVA: 0x0018ECBB File Offset: 0x0018CEBB
		internal override string EventId
		{
			get
			{
				return base.BuildEventId("ServiceConfiguration");
			}
		}

		// Token: 0x06006B07 RID: 27399 RVA: 0x0018ECC8 File Offset: 0x0018CEC8
		internal override void WriteTo(XmlWriter xml)
		{
			xml.WriteElementString("FoundServiceElement", (this.serviceElement != null).ToString(CultureInfo.InvariantCulture));
			if (this.serviceElement != null)
			{
				if (!string.IsNullOrEmpty(this.serviceElement.ElementInformation.Source))
				{
					xml.WriteElementString("ConfigurationFileSource", this.serviceElement.ElementInformation.Source);
					xml.WriteElementString("ConfigurationFileLineNumber", this.serviceElement.ElementInformation.LineNumber.ToString(CultureInfo.InvariantCulture));
				}
				xml.WriteStartElement("ServiceConfigurationInformation");
				this.WriteElementString("ServiceName", this.serviceElement.Name, xml);
				this.WriteElementString("BehaviorConfiguration", this.serviceElement.BehaviorConfiguration, xml);
				xml.WriteStartElement("Host");
				xml.WriteStartElement("Timeouts");
				xml.WriteElementString("OpenTimeout", this.serviceElement.Host.Timeouts.OpenTimeout.ToString());
				xml.WriteElementString("CloseTimeout", this.serviceElement.Host.Timeouts.CloseTimeout.ToString());
				xml.WriteEndElement();
				if (this.serviceElement.Host.BaseAddresses.Count > 0)
				{
					xml.WriteStartElement("BaseAddresses");
					foreach (object obj in this.serviceElement.Host.BaseAddresses)
					{
						BaseAddressElement baseAddressElement = (BaseAddressElement)obj;
						this.WriteElementString("BaseAddress", baseAddressElement.BaseAddress, xml);
					}
					xml.WriteEndElement();
				}
				xml.WriteEndElement();
				xml.WriteStartElement("Endpoints");
				foreach (object obj2 in this.serviceElement.Endpoints)
				{
					ServiceEndpointElement serviceEndpointElement = (ServiceEndpointElement)obj2;
					xml.WriteStartElement("Endpoint");
					if (serviceEndpointElement.Address != null)
					{
						this.WriteElementString("Address", serviceEndpointElement.Address.ToString(), xml);
					}
					this.WriteElementString("Binding", serviceEndpointElement.Binding, xml);
					this.WriteElementString("BindingConfiguration", serviceEndpointElement.BindingConfiguration, xml);
					this.WriteElementString("BindingName", serviceEndpointElement.BindingName, xml);
					this.WriteElementString("BindingNamespace", serviceEndpointElement.BindingNamespace, xml);
					this.WriteElementString("Contract", serviceEndpointElement.Contract, xml);
					if (serviceEndpointElement.ListenUri != null)
					{
						xml.WriteElementString("ListenUri", serviceEndpointElement.ListenUri.ToString());
					}
					xml.WriteElementString("ListenUriMode", serviceEndpointElement.ListenUriMode.ToString());
					this.WriteElementString("Name", serviceEndpointElement.Name, xml);
					xml.WriteEndElement();
				}
				xml.WriteEndElement();
				xml.WriteEndElement();
			}
		}

		// Token: 0x06006B08 RID: 27400 RVA: 0x0018F018 File Offset: 0x0018D218
		private void WriteElementString(string name, string value, XmlWriter xml)
		{
			if (!string.IsNullOrEmpty(value))
			{
				xml.WriteElementString(name, value);
			}
		}

		// Token: 0x04003CC9 RID: 15561
		private ServiceElement serviceElement;
	}
}
