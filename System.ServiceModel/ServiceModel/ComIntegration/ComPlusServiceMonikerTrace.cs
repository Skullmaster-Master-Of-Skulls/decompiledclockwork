using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.ServiceModel.Diagnostics;
using System.Web.Services.Description;

namespace System.ServiceModel.ComIntegration
{
	// Token: 0x020001EC RID: 492
	internal static class ComPlusServiceMonikerTrace
	{
		// Token: 0x06000FC2 RID: 4034 RVA: 0x000388EC File Offset: 0x00036AEC
		public static void Trace(TraceEventType type, int traceCode, string description, Dictionary<MonikerHelper.MonikerAttribute, string> propertyTable)
		{
			if (DiagnosticUtility.ShouldTrace(type))
			{
				string address = null;
				string contract = null;
				string binding = null;
				string bindingConfiguration = null;
				string spnIdentity = null;
				string upnIdentity = null;
				string dnsIdentity = null;
				string text = null;
				string mexAddress = null;
				string mexBinding = null;
				string mexBindingConfiguration = null;
				string mexSpnIdentity = null;
				string mexUpnIdentity = null;
				string mexDnsIdentity = null;
				string contractNamespace = null;
				string bindingNamespace = null;
				ServiceDescription wsdl = null;
				propertyTable.TryGetValue(MonikerHelper.MonikerAttribute.Wsdl, out text);
				propertyTable.TryGetValue(MonikerHelper.MonikerAttribute.Contract, out contract);
				propertyTable.TryGetValue(MonikerHelper.MonikerAttribute.Address, out address);
				propertyTable.TryGetValue(MonikerHelper.MonikerAttribute.Binding, out binding);
				propertyTable.TryGetValue(MonikerHelper.MonikerAttribute.BindingConfiguration, out bindingConfiguration);
				propertyTable.TryGetValue(MonikerHelper.MonikerAttribute.SpnIdentity, out spnIdentity);
				propertyTable.TryGetValue(MonikerHelper.MonikerAttribute.UpnIdentity, out upnIdentity);
				propertyTable.TryGetValue(MonikerHelper.MonikerAttribute.DnsIdentity, out dnsIdentity);
				propertyTable.TryGetValue(MonikerHelper.MonikerAttribute.MexAddress, out mexAddress);
				propertyTable.TryGetValue(MonikerHelper.MonikerAttribute.MexBinding, out mexBinding);
				propertyTable.TryGetValue(MonikerHelper.MonikerAttribute.MexBindingConfiguration, out mexBindingConfiguration);
				propertyTable.TryGetValue(MonikerHelper.MonikerAttribute.MexSpnIdentity, out mexSpnIdentity);
				propertyTable.TryGetValue(MonikerHelper.MonikerAttribute.MexUpnIdentity, out mexUpnIdentity);
				propertyTable.TryGetValue(MonikerHelper.MonikerAttribute.MexDnsIdentity, out mexDnsIdentity);
				propertyTable.TryGetValue(MonikerHelper.MonikerAttribute.ContractNamespace, out contractNamespace);
				propertyTable.TryGetValue(MonikerHelper.MonikerAttribute.BindingNamespace, out bindingNamespace);
				if (!string.IsNullOrEmpty(text))
				{
					TextReader textReader = new StringReader(text);
					wsdl = ServiceDescription.Read(textReader);
				}
				ComPlusServiceMonikerSchema extendedData = new ComPlusServiceMonikerSchema(address, contract, contractNamespace, wsdl, spnIdentity, upnIdentity, dnsIdentity, binding, bindingConfiguration, bindingNamespace, mexAddress, mexBinding, mexBindingConfiguration, mexSpnIdentity, mexUpnIdentity, mexDnsIdentity);
				TraceUtility.TraceEvent(type, traceCode, SR.GetString(description), extendedData);
			}
		}
	}
}
