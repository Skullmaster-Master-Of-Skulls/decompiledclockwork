using System;
using System.Xml;

namespace System.ServiceModel.Security
{
	// Token: 0x020002BA RID: 698
	internal sealed class StrictModeSecurityHeaderElementInferenceEngine : SecurityHeaderElementInferenceEngine
	{
		// Token: 0x06001612 RID: 5650 RVA: 0x00053EF1 File Offset: 0x000520F1
		private StrictModeSecurityHeaderElementInferenceEngine()
		{
		}

		// Token: 0x170004F9 RID: 1273
		// (get) Token: 0x06001613 RID: 5651 RVA: 0x00053EF9 File Offset: 0x000520F9
		internal static StrictModeSecurityHeaderElementInferenceEngine Instance
		{
			get
			{
				return StrictModeSecurityHeaderElementInferenceEngine.instance;
			}
		}

		// Token: 0x06001614 RID: 5652 RVA: 0x00053F00 File Offset: 0x00052100
		public override void ExecuteProcessingPasses(ReceiveSecurityHeader securityHeader, XmlDictionaryReader reader)
		{
			securityHeader.ExecuteFullPass(reader);
		}

		// Token: 0x06001615 RID: 5653 RVA: 0x00053F0C File Offset: 0x0005210C
		public override void MarkElements(ReceiveSecurityHeaderElementManager elementManager, bool messageSecurityMode)
		{
			bool flag = false;
			for (int i = 0; i < elementManager.Count; i++)
			{
				ReceiveSecurityHeaderEntry receiveSecurityHeaderEntry;
				elementManager.GetElementEntry(i, out receiveSecurityHeaderEntry);
				if (receiveSecurityHeaderEntry.elementCategory == ReceiveSecurityHeaderElementCategory.Signature)
				{
					if (!messageSecurityMode || flag)
					{
						elementManager.SetBindingMode(i, ReceiveSecurityHeaderBindingModes.Endorsing);
					}
					else
					{
						elementManager.SetBindingMode(i, ReceiveSecurityHeaderBindingModes.Primary);
						flag = true;
					}
				}
			}
		}

		// Token: 0x04001BAB RID: 7083
		private static StrictModeSecurityHeaderElementInferenceEngine instance = new StrictModeSecurityHeaderElementInferenceEngine();
	}
}
