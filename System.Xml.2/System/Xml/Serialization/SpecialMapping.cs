using System;

namespace System.Xml.Serialization
{
	// Token: 0x02000158 RID: 344
	internal class SpecialMapping : TypeMapping
	{
		// Token: 0x17000510 RID: 1296
		// (get) Token: 0x060017DF RID: 6111 RVA: 0x00068309 File Offset: 0x00066509
		// (set) Token: 0x060017E0 RID: 6112 RVA: 0x00068311 File Offset: 0x00066511
		internal bool NamedAny
		{
			get
			{
				return this.namedAny;
			}
			set
			{
				this.namedAny = value;
			}
		}

		// Token: 0x04000B08 RID: 2824
		private bool namedAny;
	}
}
