using System;
using System.ServiceModel.Description;
using System.Xml;

namespace System.ServiceModel.Channels
{
	// Token: 0x020008A4 RID: 2212
	public class OneWayBindingElementImporter : IPolicyImportExtension
	{
		// Token: 0x0600545D RID: 21597 RVA: 0x00136AC0 File Offset: 0x00134CC0
		void IPolicyImportExtension.ImportPolicy(MetadataImporter importer, PolicyConversionContext context)
		{
			if (importer == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("importer");
			}
			if (context == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("context");
			}
			XmlElement xmlElement = PolicyConversionContext.FindAssertion(context.GetBindingAssertions(), "OneWay", "http://schemas.microsoft.com/ws/2005/05/routing/policy", true);
			if (xmlElement != null)
			{
				OneWayBindingElement oneWayBindingElement = new OneWayBindingElement();
				context.BindingElements.Add(oneWayBindingElement);
				for (int i = 0; i < xmlElement.ChildNodes.Count; i++)
				{
					XmlNode xmlNode = xmlElement.ChildNodes[i];
					if (xmlNode != null && xmlNode.NodeType == XmlNodeType.Element && xmlNode.NamespaceURI == "http://schemas.microsoft.com/ws/2005/05/routing/policy" && xmlNode.LocalName == "PacketRoutable")
					{
						oneWayBindingElement.PacketRoutable = true;
						return;
					}
				}
				return;
			}
			if (WsdlImporter.WSAddressingHelper.DetermineSupportedAddressingMode(importer, context) == SupportedAddressingMode.NonAnonymous)
			{
				context.BindingElements.Add(new OneWayBindingElement());
			}
		}
	}
}
