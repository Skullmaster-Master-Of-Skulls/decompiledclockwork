using System;
using System.Collections.Generic;
using System.ServiceModel.Description;
using System.Xml;

namespace System.ServiceModel.Channels
{
	// Token: 0x020008AC RID: 2220
	internal static class StateHelper
	{
		// Token: 0x060054B2 RID: 21682 RVA: 0x00137B90 File Offset: 0x00135D90
		private static Dictionary<XmlQualifiedName, XmlQualifiedName> GetGeneratedTransportBindingElements(MetadataImporter importer)
		{
			object obj;
			if (!importer.State.TryGetValue(StateHelper.StateBagKey, out obj))
			{
				obj = new Dictionary<XmlQualifiedName, XmlQualifiedName>();
				importer.State.Add(StateHelper.StateBagKey, obj);
			}
			return (Dictionary<XmlQualifiedName, XmlQualifiedName>)obj;
		}

		// Token: 0x060054B3 RID: 21683 RVA: 0x00137BCE File Offset: 0x00135DCE
		internal static void RegisterTransportBindingElement(MetadataImporter importer, XmlQualifiedName wsdlBindingQName)
		{
			StateHelper.GetGeneratedTransportBindingElements(importer)[wsdlBindingQName] = wsdlBindingQName;
		}

		// Token: 0x060054B4 RID: 21684 RVA: 0x00137BE0 File Offset: 0x00135DE0
		internal static void RegisterTransportBindingElement(MetadataImporter importer, WsdlEndpointConversionContext context)
		{
			XmlQualifiedName xmlQualifiedName = new XmlQualifiedName(context.WsdlBinding.Name, context.WsdlBinding.ServiceDescription.TargetNamespace);
			StateHelper.GetGeneratedTransportBindingElements(importer)[xmlQualifiedName] = xmlQualifiedName;
		}

		// Token: 0x060054B5 RID: 21685 RVA: 0x00137C1C File Offset: 0x00135E1C
		internal static bool IsRegisteredTransportBindingElement(WsdlImporter importer, WsdlEndpointConversionContext context)
		{
			XmlQualifiedName key = new XmlQualifiedName(context.WsdlBinding.Name, context.WsdlBinding.ServiceDescription.TargetNamespace);
			return StateHelper.GetGeneratedTransportBindingElements(importer).ContainsKey(key);
		}

		// Token: 0x04003322 RID: 13090
		private static readonly object StateBagKey = new object();
	}
}
