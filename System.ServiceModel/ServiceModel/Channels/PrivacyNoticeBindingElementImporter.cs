using System;
using System.ServiceModel.Description;
using System.Xml;

namespace System.ServiceModel.Channels
{
	// Token: 0x020008CD RID: 2253
	public sealed class PrivacyNoticeBindingElementImporter : IPolicyImportExtension
	{
		// Token: 0x060055FA RID: 22010 RVA: 0x0013AA88 File Offset: 0x00138C88
		void IPolicyImportExtension.ImportPolicy(MetadataImporter importer, PolicyConversionContext policyContext)
		{
			if (policyContext == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("policyContext");
			}
			XmlElement xmlElement = PolicyConversionContext.FindAssertion(policyContext.GetBindingAssertions(), "PrivacyNotice", "http://schemas.xmlsoap.org/ws/2005/05/identity", true);
			if (xmlElement != null)
			{
				PrivacyNoticeBindingElement privacyNoticeBindingElement = policyContext.BindingElements.Find<PrivacyNoticeBindingElement>();
				if (privacyNoticeBindingElement == null)
				{
					privacyNoticeBindingElement = new PrivacyNoticeBindingElement();
					policyContext.BindingElements.Add(privacyNoticeBindingElement);
				}
				privacyNoticeBindingElement.Url = new Uri(xmlElement.InnerText);
				string attribute = xmlElement.GetAttribute("Version", "http://schemas.xmlsoap.org/ws/2005/05/identity");
				if (string.IsNullOrEmpty(attribute))
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("CannotImportPrivacyNoticeElementWithoutVersionAttribute")));
				}
				int version = 0;
				if (!int.TryParse(attribute, out version))
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("PrivacyNoticeElementVersionAttributeInvalid")));
				}
				privacyNoticeBindingElement.Version = version;
			}
		}
	}
}
