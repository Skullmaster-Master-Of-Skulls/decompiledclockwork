using System;

namespace System.ServiceModel.Security
{
	// Token: 0x020002A1 RID: 673
	internal sealed class LaxTimestampFirstModeSecurityHeaderElementInferenceEngine : LaxModeSecurityHeaderElementInferenceEngine
	{
		// Token: 0x0600146F RID: 5231 RVA: 0x0004CA41 File Offset: 0x0004AC41
		private LaxTimestampFirstModeSecurityHeaderElementInferenceEngine()
		{
		}

		// Token: 0x17000497 RID: 1175
		// (get) Token: 0x06001470 RID: 5232 RVA: 0x0004CA49 File Offset: 0x0004AC49
		internal new static LaxTimestampFirstModeSecurityHeaderElementInferenceEngine Instance
		{
			get
			{
				return LaxTimestampFirstModeSecurityHeaderElementInferenceEngine.instance;
			}
		}

		// Token: 0x06001471 RID: 5233 RVA: 0x0004CA50 File Offset: 0x0004AC50
		public override void MarkElements(ReceiveSecurityHeaderElementManager elementManager, bool messageSecurityMode)
		{
			for (int i = 1; i < elementManager.Count; i++)
			{
				if (elementManager.GetElementCategory(i) == ReceiveSecurityHeaderElementCategory.Timestamp)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new MessageSecurityException(SR.GetString("TimestampMustOccurFirstInSecurityHeaderLayout")));
				}
			}
			base.MarkElements(elementManager, messageSecurityMode);
		}

		// Token: 0x04001AB7 RID: 6839
		private static LaxTimestampFirstModeSecurityHeaderElementInferenceEngine instance = new LaxTimestampFirstModeSecurityHeaderElementInferenceEngine();
	}
}
