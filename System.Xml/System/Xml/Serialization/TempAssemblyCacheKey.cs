using System;

namespace System.Xml.Serialization
{
	// Token: 0x020002B6 RID: 694
	internal class TempAssemblyCacheKey
	{
		// Token: 0x0600213F RID: 8511 RVA: 0x0009D831 File Offset: 0x0009C831
		internal TempAssemblyCacheKey(string ns, object type)
		{
			this.type = type;
			this.ns = ns;
		}

		// Token: 0x06002140 RID: 8512 RVA: 0x0009D848 File Offset: 0x0009C848
		public override bool Equals(object o)
		{
			TempAssemblyCacheKey tempAssemblyCacheKey = o as TempAssemblyCacheKey;
			return tempAssemblyCacheKey != null && tempAssemblyCacheKey.type == this.type && tempAssemblyCacheKey.ns == this.ns;
		}

		// Token: 0x06002141 RID: 8513 RVA: 0x0009D882 File Offset: 0x0009C882
		public override int GetHashCode()
		{
			return ((this.ns != null) ? this.ns.GetHashCode() : 0) ^ ((this.type != null) ? this.type.GetHashCode() : 0);
		}

		// Token: 0x04001445 RID: 5189
		private string ns;

		// Token: 0x04001446 RID: 5190
		private object type;
	}
}
