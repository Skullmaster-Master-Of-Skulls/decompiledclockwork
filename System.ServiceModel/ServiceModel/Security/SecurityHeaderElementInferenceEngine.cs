using System;
using System.ServiceModel.Channels;
using System.Xml;

namespace System.ServiceModel.Security
{
	// Token: 0x020002B1 RID: 689
	internal abstract class SecurityHeaderElementInferenceEngine
	{
		// Token: 0x0600156F RID: 5487
		public abstract void ExecuteProcessingPasses(ReceiveSecurityHeader securityHeader, XmlDictionaryReader reader);

		// Token: 0x06001570 RID: 5488
		public abstract void MarkElements(ReceiveSecurityHeaderElementManager elementManager, bool messageSecurityMode);

		// Token: 0x06001571 RID: 5489 RVA: 0x000515A4 File Offset: 0x0004F7A4
		public static SecurityHeaderElementInferenceEngine GetInferenceEngine(SecurityHeaderLayout layout)
		{
			SecurityHeaderLayoutHelper.Validate(layout);
			switch (layout)
			{
			case SecurityHeaderLayout.Strict:
				return StrictModeSecurityHeaderElementInferenceEngine.Instance;
			case SecurityHeaderLayout.Lax:
				return LaxModeSecurityHeaderElementInferenceEngine.Instance;
			case SecurityHeaderLayout.LaxTimestampFirst:
				return LaxTimestampFirstModeSecurityHeaderElementInferenceEngine.Instance;
			case SecurityHeaderLayout.LaxTimestampLast:
				return LaxTimestampLastModeSecurityHeaderElementInferenceEngine.Instance;
			default:
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("layout"));
			}
		}
	}
}
