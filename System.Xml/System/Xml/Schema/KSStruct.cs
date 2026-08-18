using System;

namespace System.Xml.Schema
{
	// Token: 0x0200018E RID: 398
	internal class KSStruct
	{
		// Token: 0x0600151B RID: 5403 RVA: 0x0005E2B3 File Offset: 0x0005D2B3
		public KSStruct(KeySequence ks, int dim)
		{
			this.ks = ks;
			this.fields = new LocatedActiveAxis[dim];
		}

		// Token: 0x04000CAA RID: 3242
		public int depth;

		// Token: 0x04000CAB RID: 3243
		public KeySequence ks;

		// Token: 0x04000CAC RID: 3244
		public LocatedActiveAxis[] fields;
	}
}
