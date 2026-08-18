using System;
using System.ServiceModel.Description;
using System.Xml;

namespace System.ServiceModel.Channels
{
	// Token: 0x020008D0 RID: 2256
	public sealed class UseManagedPresentationBindingElementImporter : IPolicyImportExtension
	{
		// Token: 0x06005600 RID: 22016 RVA: 0x0013ABEC File Offset: 0x00138DEC
		void IPolicyImportExtension.ImportPolicy(MetadataImporter importer, PolicyConversionContext policyContext)
		{
			if (policyContext == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("policyContext");
			}
			XmlElement xmlElement = PolicyConversionContext.FindAssertion(policyContext.GetBindingAssertions(), "RequireFederatedIdentityProvisioning", "http://schemas.xmlsoap.org/ws/2005/05/identity", true);
			if (xmlElement != null && policyContext.BindingElements.Find<UseManagedPresentationBindingElement>() == null)
			{
				UseManagedPresentationBindingElement item = new UseManagedPresentationBindingElement();
				policyContext.BindingElements.Add(item);
			}
		}
	}
}
