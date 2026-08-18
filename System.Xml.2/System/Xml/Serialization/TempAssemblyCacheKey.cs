using System;

namespace System.Xml.Serialization
{
	// Token: 0x0200013D RID: 317
	internal class TempAssemblyCacheKey
	{
		// Token: 0x060016E9 RID: 5865 RVA: 0x00065A91 File Offset: 0x00063C91
		internal TempAssemblyCacheKey(string ns, object type)
		{
			this.type = type;
			this.ns = ns;
		}

		// Token: 0x060016EA RID: 5866 RVA: 0x00065AA8 File Offset: 0x00063CA8
		public override bool Equals(object o)
		{
			TempAssemblyCacheKey tempAssemblyCacheKey = o as TempAssemblyCacheKey;
			return tempAssemblyCacheKey != null && tempAssemblyCacheKey.type == this.type && tempAssemblyCacheKey.ns == this.ns;
		}

		// Token: 0x060016EB RID: 5867 RVA: 0x00065AE2 File Offset: 0x00063CE2
		public override int GetHashCode()
		{
			return ((this.ns != null) ? this.ns.GetHashCode() : 0) ^ ((this.type != null) ? this.type.GetHashCode() : 0);
		}

		// Token: 0x04000AAA RID: 2730
		private string ns;

		// Token: 0x04000AAB RID: 2731
		private object type;
	}
}
