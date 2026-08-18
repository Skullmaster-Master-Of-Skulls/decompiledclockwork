using System;

namespace System.Xml.Serialization
{
	// Token: 0x020002DF RID: 735
	internal class NameKey
	{
		// Token: 0x06002269 RID: 8809 RVA: 0x000A0FC0 File Offset: 0x0009FFC0
		internal NameKey(string name, string ns)
		{
			this.name = name;
			this.ns = ns;
		}

		// Token: 0x0600226A RID: 8810 RVA: 0x000A0FD8 File Offset: 0x0009FFD8
		public override bool Equals(object other)
		{
			if (!(other is NameKey))
			{
				return false;
			}
			NameKey nameKey = (NameKey)other;
			return this.name == nameKey.name && this.ns == nameKey.ns;
		}

		// Token: 0x0600226B RID: 8811 RVA: 0x000A101C File Offset: 0x000A001C
		public override int GetHashCode()
		{
			return ((this.ns == null) ? "<null>".GetHashCode() : this.ns.GetHashCode()) ^ ((this.name == null) ? 0 : this.name.GetHashCode());
		}

		// Token: 0x040014C1 RID: 5313
		private string ns;

		// Token: 0x040014C2 RID: 5314
		private string name;
	}
}
