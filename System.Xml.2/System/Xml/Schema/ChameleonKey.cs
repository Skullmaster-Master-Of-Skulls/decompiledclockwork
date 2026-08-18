using System;

namespace System.Xml.Schema
{
	// Token: 0x020001E1 RID: 481
	internal class ChameleonKey
	{
		// Token: 0x0600200C RID: 8204 RVA: 0x000AC9CF File Offset: 0x000AABCF
		public ChameleonKey(string ns, XmlSchema originalSchema)
		{
			this.targetNS = ns;
			this.chameleonLocation = originalSchema.BaseUri;
			if (this.chameleonLocation.OriginalString.Length == 0)
			{
				this.originalSchema = originalSchema;
			}
		}

		// Token: 0x0600200D RID: 8205 RVA: 0x000ACA04 File Offset: 0x000AAC04
		public override int GetHashCode()
		{
			if (this.hashCode == 0)
			{
				this.hashCode = this.targetNS.GetHashCode() + this.chameleonLocation.GetHashCode() + ((this.originalSchema == null) ? 0 : this.originalSchema.GetHashCode());
			}
			return this.hashCode;
		}

		// Token: 0x0600200E RID: 8206 RVA: 0x000ACA54 File Offset: 0x000AAC54
		public override bool Equals(object obj)
		{
			if (this == obj)
			{
				return true;
			}
			ChameleonKey chameleonKey = obj as ChameleonKey;
			return chameleonKey != null && (this.targetNS.Equals(chameleonKey.targetNS) && this.chameleonLocation.Equals(chameleonKey.chameleonLocation)) && this.originalSchema == chameleonKey.originalSchema;
		}

		// Token: 0x04000D7E RID: 3454
		internal string targetNS;

		// Token: 0x04000D7F RID: 3455
		internal Uri chameleonLocation;

		// Token: 0x04000D80 RID: 3456
		internal XmlSchema originalSchema;

		// Token: 0x04000D81 RID: 3457
		private int hashCode;
	}
}
