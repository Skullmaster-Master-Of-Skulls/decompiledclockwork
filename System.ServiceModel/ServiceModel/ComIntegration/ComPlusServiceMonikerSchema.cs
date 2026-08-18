using System;
using System.Runtime.Diagnostics;
using System.Runtime.Serialization;
using System.Web.Services.Description;
using System.Xml;

namespace System.ServiceModel.ComIntegration
{
	// Token: 0x02000206 RID: 518
	[DataContract(Name = "ComPlusServiceMoniker")]
	internal class ComPlusServiceMonikerSchema : TraceRecord
	{
		// Token: 0x170003C9 RID: 969
		// (get) Token: 0x06001003 RID: 4099 RVA: 0x00039306 File Offset: 0x00037506
		internal override string EventId
		{
			get
			{
				return "http://schemas.microsoft.com/2006/08/ServiceModel/ComPlusServiceMonikerTraceRecord";
			}
		}

		// Token: 0x06001004 RID: 4100 RVA: 0x0003930D File Offset: 0x0003750D
		internal override void WriteTo(XmlWriter xmlWriter)
		{
			ComPlusTraceRecord.SerializeRecord(xmlWriter, this);
		}

		// Token: 0x06001005 RID: 4101 RVA: 0x00039318 File Offset: 0x00037518
		public ComPlusServiceMonikerSchema(string address, string contract, string contractNamespace, ServiceDescription wsdl, string spnIdentity, string upnIdentity, string dnsIdentity, string binding, string bindingConfiguration, string bindingNamespace, string mexAddress, string mexBinding, string mexBindingConfiguration, string mexSpnIdentity, string mexUpnIdentity, string mexDnsIdentity)
		{
			this.address = address;
			this.contract = contract;
			this.contractNamespace = contractNamespace;
			this.wsdlWrapper = new WsdlWrapper(wsdl);
			this.spnIdentity = spnIdentity;
			this.upnIdentity = spnIdentity;
			this.dnsIdentity = spnIdentity;
			this.binding = binding;
			this.bindingConfiguration = bindingConfiguration;
			this.bindingNamespace = bindingNamespace;
			this.mexSpnIdentity = mexSpnIdentity;
			this.mexUpnIdentity = mexUpnIdentity;
			this.mexDnsIdentity = mexDnsIdentity;
			this.mexAddress = mexAddress;
			this.mexBinding = mexBinding;
			this.mexBindingConfiguration = mexBindingConfiguration;
		}

		// Token: 0x04001820 RID: 6176
		private const string schemaId = "http://schemas.microsoft.com/2006/08/ServiceModel/ComPlusServiceMonikerTraceRecord";

		// Token: 0x04001821 RID: 6177
		[DataMember(Name = "Address")]
		private string address;

		// Token: 0x04001822 RID: 6178
		[DataMember(Name = "Contract")]
		private string contract;

		// Token: 0x04001823 RID: 6179
		[DataMember(Name = "ContractNamespace")]
		private string contractNamespace;

		// Token: 0x04001824 RID: 6180
		[DataMember(Name = "Wsdl")]
		private WsdlWrapper wsdlWrapper;

		// Token: 0x04001825 RID: 6181
		[DataMember(Name = "SpnIdentity")]
		private string spnIdentity;

		// Token: 0x04001826 RID: 6182
		[DataMember(Name = "UpnIdentity")]
		private string upnIdentity;

		// Token: 0x04001827 RID: 6183
		[DataMember(Name = "DnsIdentity")]
		private string dnsIdentity;

		// Token: 0x04001828 RID: 6184
		[DataMember(Name = "Binding")]
		private string binding;

		// Token: 0x04001829 RID: 6185
		[DataMember(Name = "BindingConfiguration")]
		private string bindingConfiguration;

		// Token: 0x0400182A RID: 6186
		[DataMember(Name = "BindingNamespace")]
		private string bindingNamespace;

		// Token: 0x0400182B RID: 6187
		[DataMember(Name = "mexSpnIdentity")]
		private string mexSpnIdentity;

		// Token: 0x0400182C RID: 6188
		[DataMember(Name = "mexUpnIdentity")]
		private string mexUpnIdentity;

		// Token: 0x0400182D RID: 6189
		[DataMember(Name = "mexDnsIdentity")]
		private string mexDnsIdentity;

		// Token: 0x0400182E RID: 6190
		[DataMember(Name = "mexAddress")]
		private string mexAddress;

		// Token: 0x0400182F RID: 6191
		[DataMember(Name = "mexBinding")]
		private string mexBinding;

		// Token: 0x04001830 RID: 6192
		[DataMember(Name = "mexBindingConfiguration")]
		private string mexBindingConfiguration;
	}
}
