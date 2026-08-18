using System;

namespace System.Xml.Schema
{
	// Token: 0x020001E7 RID: 487
	internal class KSStruct
	{
		// Token: 0x0600206B RID: 8299 RVA: 0x000B1FCF File Offset: 0x000B01CF
		public KSStruct(KeySequence ks, int dim)
		{
			this.ks = ks;
			this.fields = new LocatedActiveAxis[dim];
		}

		// Token: 0x04000DA1 RID: 3489
		public int depth;

		// Token: 0x04000DA2 RID: 3490
		public KeySequence ks;

		// Token: 0x04000DA3 RID: 3491
		public LocatedActiveAxis[] fields;
	}
}
