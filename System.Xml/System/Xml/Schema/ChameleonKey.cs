using System;

namespace System.Xml.Schema
{
	// Token: 0x02000187 RID: 391
	internal class ChameleonKey
	{
		// Token: 0x060014BC RID: 5308 RVA: 0x0005879F File Offset: 0x0005779F
		public ChameleonKey(string ns, Uri location)
		{
			this.targetNS = ns;
			this.chameleonLocation = location;
		}

		// Token: 0x060014BD RID: 5309 RVA: 0x000587B5 File Offset: 0x000577B5
		public override int GetHashCode()
		{
			if (this.hashCode == 0)
			{
				this.hashCode = this.targetNS.GetHashCode() + this.chameleonLocation.GetHashCode();
			}
			return this.hashCode;
		}

		// Token: 0x060014BE RID: 5310 RVA: 0x000587E4 File Offset: 0x000577E4
		public override bool Equals(object obj)
		{
			if (object.ReferenceEquals(this, obj))
			{
				return true;
			}
			ChameleonKey chameleonKey = obj as ChameleonKey;
			return chameleonKey != null && this.targetNS.Equals(chameleonKey.targetNS) && this.chameleonLocation.Equals(chameleonKey.chameleonLocation);
		}

		// Token: 0x04000C84 RID: 3204
		internal string targetNS;

		// Token: 0x04000C85 RID: 3205
		internal Uri chameleonLocation;

		// Token: 0x04000C86 RID: 3206
		private int hashCode;
	}
}
