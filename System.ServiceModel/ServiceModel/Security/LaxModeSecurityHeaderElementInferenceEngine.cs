using System;
using System.IdentityModel;
using System.Xml;

namespace System.ServiceModel.Security
{
	// Token: 0x020002A0 RID: 672
	internal class LaxModeSecurityHeaderElementInferenceEngine : SecurityHeaderElementInferenceEngine
	{
		// Token: 0x0600146A RID: 5226 RVA: 0x0004C8A8 File Offset: 0x0004AAA8
		protected LaxModeSecurityHeaderElementInferenceEngine()
		{
		}

		// Token: 0x17000496 RID: 1174
		// (get) Token: 0x0600146B RID: 5227 RVA: 0x0004C8B0 File Offset: 0x0004AAB0
		internal static LaxModeSecurityHeaderElementInferenceEngine Instance
		{
			get
			{
				return LaxModeSecurityHeaderElementInferenceEngine.instance;
			}
		}

		// Token: 0x0600146C RID: 5228 RVA: 0x0004C8B7 File Offset: 0x0004AAB7
		public override void ExecuteProcessingPasses(ReceiveSecurityHeader securityHeader, XmlDictionaryReader reader)
		{
			securityHeader.ExecuteReadingPass(reader);
			securityHeader.ExecuteDerivedKeyTokenStubPass(false);
			securityHeader.ExecuteSubheaderDecryptionPass();
			securityHeader.ExecuteDerivedKeyTokenStubPass(true);
			this.MarkElements(securityHeader.ElementManager, securityHeader.RequireMessageProtection);
			securityHeader.ExecuteSignatureEncryptionProcessingPass();
		}

		// Token: 0x0600146D RID: 5229 RVA: 0x0004C8EC File Offset: 0x0004AAEC
		public override void MarkElements(ReceiveSecurityHeaderElementManager elementManager, bool messageSecurityMode)
		{
			bool flag = false;
			for (int i = 0; i < elementManager.Count; i++)
			{
				ReceiveSecurityHeaderEntry receiveSecurityHeaderEntry;
				elementManager.GetElementEntry(i, out receiveSecurityHeaderEntry);
				if (receiveSecurityHeaderEntry.elementCategory == ReceiveSecurityHeaderElementCategory.Signature)
				{
					if (!messageSecurityMode)
					{
						elementManager.SetBindingMode(i, ReceiveSecurityHeaderBindingModes.Endorsing);
					}
					else
					{
						SignedXml signedXml = (SignedXml)receiveSecurityHeaderEntry.element;
						StandardSignedInfo standardSignedInfo = (StandardSignedInfo)signedXml.Signature.SignedInfo;
						bool flag2 = false;
						if (standardSignedInfo.ReferenceCount == 1)
						{
							string uri = standardSignedInfo[0].Uri;
							if (uri == null || uri.Length <= 1 || uri[0] != '#')
							{
								throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new MessageSecurityException(SR.GetString("UnableToResolveReferenceUriForSignature", new object[]
								{
									uri
								})));
							}
							string b = uri.Substring(1);
							for (int j = 0; j < elementManager.Count; j++)
							{
								ReceiveSecurityHeaderEntry receiveSecurityHeaderEntry2;
								elementManager.GetElementEntry(j, out receiveSecurityHeaderEntry2);
								if (j != i && receiveSecurityHeaderEntry2.elementCategory == ReceiveSecurityHeaderElementCategory.Signature && receiveSecurityHeaderEntry2.id == b)
								{
									flag2 = true;
									break;
								}
							}
						}
						if (flag2)
						{
							elementManager.SetBindingMode(i, ReceiveSecurityHeaderBindingModes.Endorsing);
						}
						else
						{
							if (flag)
							{
								throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new MessageSecurityException(SR.GetString("AtMostOnePrimarySignatureInReceiveSecurityHeader")));
							}
							flag = true;
							elementManager.SetBindingMode(i, ReceiveSecurityHeaderBindingModes.Primary);
						}
					}
				}
			}
		}

		// Token: 0x04001AB6 RID: 6838
		private static LaxModeSecurityHeaderElementInferenceEngine instance = new LaxModeSecurityHeaderElementInferenceEngine();
	}
}
