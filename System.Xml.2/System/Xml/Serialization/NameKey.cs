using System;

namespace System.Xml.Serialization
{
	// Token: 0x02000164 RID: 356
	internal class NameKey
	{
		// Token: 0x0600181E RID: 6174 RVA: 0x00069384 File Offset: 0x00067584
		internal NameKey(string name, string ns)
		{
			this.name = name;
			this.ns = ns;
		}

		// Token: 0x0600181F RID: 6175 RVA: 0x0006939C File Offset: 0x0006759C
		public override bool Equals(object other)
		{
			if (!(other is NameKey))
			{
				return false;
			}
			NameKey nameKey = (NameKey)other;
			return this.name == nameKey.name && this.ns == nameKey.ns;
		}

		// Token: 0x06001820 RID: 6176 RVA: 0x000693E0 File Offset: 0x000675E0
		public override int GetHashCode()
		{
			return ((this.ns == null) ? "<null>".GetHashCode() : this.ns.GetHashCode()) ^ ((this.name == null) ? 0 : this.name.GetHashCode());
		}

		// Token: 0x04000B2D RID: 2861
		private string ns;

		// Token: 0x04000B2E RID: 2862
		private string name;
	}
}
