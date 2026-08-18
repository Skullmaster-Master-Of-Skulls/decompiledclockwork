using System;

namespace System.ServiceModel.Security
{
	// Token: 0x020002AD RID: 685
	internal struct ReceiveSecurityHeaderEntry
	{
		// Token: 0x06001533 RID: 5427 RVA: 0x0004FB10 File Offset: 0x0004DD10
		public bool MatchesId(string id, bool requiresEncryptedFormId)
		{
			if (this.doubleEncrypted)
			{
				return this.encryptedFormId == id || this.encryptedFormWsuId == id;
			}
			if (requiresEncryptedFormId)
			{
				return this.encryptedFormId == id;
			}
			return this.id == id;
		}

		// Token: 0x06001534 RID: 5428 RVA: 0x0004FB5E File Offset: 0x0004DD5E
		public void PreserveIdBeforeDecryption()
		{
			this.encryptedFormId = this.id;
		}

		// Token: 0x06001535 RID: 5429 RVA: 0x0004FB6C File Offset: 0x0004DD6C
		public void SetElement(ReceiveSecurityHeaderElementCategory elementCategory, object element, ReceiveSecurityHeaderBindingModes bindingMode, string id, bool encrypted, byte[] decryptedBuffer, TokenTracker supportingTokenTracker)
		{
			this.elementCategory = elementCategory;
			this.element = element;
			this.bindingMode = bindingMode;
			this.encrypted = encrypted;
			this.decryptedBuffer = decryptedBuffer;
			this.supportingTokenTracker = supportingTokenTracker;
			this.id = id;
		}

		// Token: 0x04001B25 RID: 6949
		internal ReceiveSecurityHeaderElementCategory elementCategory;

		// Token: 0x04001B26 RID: 6950
		internal object element;

		// Token: 0x04001B27 RID: 6951
		internal ReceiveSecurityHeaderBindingModes bindingMode;

		// Token: 0x04001B28 RID: 6952
		internal string id;

		// Token: 0x04001B29 RID: 6953
		internal string encryptedFormId;

		// Token: 0x04001B2A RID: 6954
		internal string encryptedFormWsuId;

		// Token: 0x04001B2B RID: 6955
		internal bool signed;

		// Token: 0x04001B2C RID: 6956
		internal bool encrypted;

		// Token: 0x04001B2D RID: 6957
		internal byte[] decryptedBuffer;

		// Token: 0x04001B2E RID: 6958
		internal TokenTracker supportingTokenTracker;

		// Token: 0x04001B2F RID: 6959
		internal bool doubleEncrypted;
	}
}
