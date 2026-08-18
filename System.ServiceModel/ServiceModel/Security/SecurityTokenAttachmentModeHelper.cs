using System;
using System.ComponentModel;

namespace System.ServiceModel.Security
{
	// Token: 0x020002C5 RID: 709
	internal static class SecurityTokenAttachmentModeHelper
	{
		// Token: 0x060016F3 RID: 5875 RVA: 0x00057198 File Offset: 0x00055398
		internal static bool IsDefined(SecurityTokenAttachmentMode value)
		{
			return value == SecurityTokenAttachmentMode.Endorsing || value == SecurityTokenAttachmentMode.Signed || value == SecurityTokenAttachmentMode.SignedEncrypted || value == SecurityTokenAttachmentMode.SignedEndorsing;
		}

		// Token: 0x060016F4 RID: 5876 RVA: 0x000571AB File Offset: 0x000553AB
		internal static void Validate(SecurityTokenAttachmentMode value)
		{
			if (!SecurityTokenAttachmentModeHelper.IsDefined(value))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidEnumArgumentException("value", (int)value, typeof(SecurityTokenAttachmentMode)));
			}
		}

		// Token: 0x060016F5 RID: 5877 RVA: 0x000571D8 File Offset: 0x000553D8
		internal static void Categorize(SecurityTokenAttachmentMode value, out bool isBasic, out bool isSignedButNotBasic, out ReceiveSecurityHeaderBindingModes mode)
		{
			SecurityTokenAttachmentModeHelper.Validate(value);
			switch (value)
			{
			case SecurityTokenAttachmentMode.Signed:
				isBasic = false;
				isSignedButNotBasic = true;
				mode = ReceiveSecurityHeaderBindingModes.Signed;
				return;
			case SecurityTokenAttachmentMode.Endorsing:
				isBasic = false;
				isSignedButNotBasic = false;
				mode = ReceiveSecurityHeaderBindingModes.Endorsing;
				return;
			case SecurityTokenAttachmentMode.SignedEndorsing:
				isBasic = false;
				isSignedButNotBasic = true;
				mode = ReceiveSecurityHeaderBindingModes.SignedEndorsing;
				return;
			case SecurityTokenAttachmentMode.SignedEncrypted:
				isBasic = true;
				isSignedButNotBasic = false;
				mode = ReceiveSecurityHeaderBindingModes.Basic;
				return;
			default:
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("value"));
			}
		}
	}
}
