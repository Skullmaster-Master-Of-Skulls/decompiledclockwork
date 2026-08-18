using System;

namespace System.ServiceModel.Security
{
	// Token: 0x020002A2 RID: 674
	internal sealed class LaxTimestampLastModeSecurityHeaderElementInferenceEngine : LaxModeSecurityHeaderElementInferenceEngine
	{
		// Token: 0x06001473 RID: 5235 RVA: 0x0004CAA6 File Offset: 0x0004ACA6
		private LaxTimestampLastModeSecurityHeaderElementInferenceEngine()
		{
		}

		// Token: 0x17000498 RID: 1176
		// (get) Token: 0x06001474 RID: 5236 RVA: 0x0004CAAE File Offset: 0x0004ACAE
		internal new static LaxTimestampLastModeSecurityHeaderElementInferenceEngine Instance
		{
			get
			{
				return LaxTimestampLastModeSecurityHeaderElementInferenceEngine.instance;
			}
		}

		// Token: 0x06001475 RID: 5237 RVA: 0x0004CAB8 File Offset: 0x0004ACB8
		public override void MarkElements(ReceiveSecurityHeaderElementManager elementManager, bool messageSecurityMode)
		{
			for (int i = 0; i < elementManager.Count - 1; i++)
			{
				if (elementManager.GetElementCategory(i) == ReceiveSecurityHeaderElementCategory.Timestamp)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new MessageSecurityException(SR.GetString("TimestampMustOccurLastInSecurityHeaderLayout")));
				}
			}
			base.MarkElements(elementManager, messageSecurityMode);
		}

		// Token: 0x04001AB8 RID: 6840
		private static LaxTimestampLastModeSecurityHeaderElementInferenceEngine instance = new LaxTimestampLastModeSecurityHeaderElementInferenceEngine();
	}
}
