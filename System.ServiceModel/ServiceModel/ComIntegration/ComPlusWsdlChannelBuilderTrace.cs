using System;
using System.Diagnostics;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Diagnostics;
using System.Web.Services.Description;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace System.ServiceModel.ComIntegration
{
	// Token: 0x020001ED RID: 493
	internal static class ComPlusWsdlChannelBuilderTrace
	{
		// Token: 0x06000FC3 RID: 4035 RVA: 0x00038A28 File Offset: 0x00036C28
		public static void Trace(TraceEventType type, int traceCode, string description, XmlQualifiedName bindingQname, XmlQualifiedName contractQname, System.Web.Services.Description.ServiceDescription wsdl, ContractDescription contract, System.ServiceModel.Channels.Binding binding, XmlSchemas schemas)
		{
			if (DiagnosticUtility.ShouldTrace(type))
			{
				string name = "Service";
				if (wsdl.Name != null)
				{
					name = wsdl.Name;
				}
				Type contractType = contract.ContractType;
				XmlQualifiedName serviceQname = new XmlQualifiedName(name, wsdl.TargetNamespace);
				foreach (object obj in schemas)
				{
					XmlSchema schema = (XmlSchema)obj;
					ComPlusWsdlChannelBuilderSchema extendedData = new ComPlusWsdlChannelBuilderSchema(bindingQname, contractQname, serviceQname, (contractType != null) ? contractType.ToString() : null, (binding != null) ? binding.GetType().ToString() : null, schema);
					TraceUtility.TraceEvent(type, traceCode, SR.GetString(description), extendedData);
				}
			}
		}
	}
}
