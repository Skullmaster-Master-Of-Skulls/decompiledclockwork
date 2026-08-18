using System;

namespace System.Xml.Serialization
{
	// Token: 0x020002D3 RID: 723
	internal class SpecialMapping : TypeMapping
	{
		// Token: 0x17000850 RID: 2128
		// (get) Token: 0x0600222D RID: 8749 RVA: 0x0009FF68 File Offset: 0x0009EF68
		// (set) Token: 0x0600222E RID: 8750 RVA: 0x0009FF70 File Offset: 0x0009EF70
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

		// Token: 0x0400149F RID: 5279
		private bool namedAny;
	}
}
