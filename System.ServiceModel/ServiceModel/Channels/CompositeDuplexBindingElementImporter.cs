using System;
using System.ServiceModel.Description;

namespace System.ServiceModel.Channels
{
	// Token: 0x0200089C RID: 2204
	public class CompositeDuplexBindingElementImporter : IPolicyImportExtension
	{
		// Token: 0x060053D4 RID: 21460 RVA: 0x00134CE0 File Offset: 0x00132EE0
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
			if (PolicyConversionContext.FindAssertion(context.GetBindingAssertions(), "CompositeDuplex", "http://schemas.microsoft.com/net/2006/06/duplex", true) != null || WsdlImporter.WSAddressingHelper.DetermineSupportedAddressingMode(importer, context) == SupportedAddressingMode.NonAnonymous)
			{
				context.BindingElements.Add(new CompositeDuplexBindingElement());
			}
		}
	}
}
