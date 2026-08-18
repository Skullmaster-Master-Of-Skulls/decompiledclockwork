using System;

namespace System.Xml.Serialization
{
	// Token: 0x020002CA RID: 714
	internal class EnumMapping : PrimitiveMapping
	{
		// Token: 0x17000822 RID: 2082
		// (get) Token: 0x060021CB RID: 8651 RVA: 0x0009F3B3 File Offset: 0x0009E3B3
		// (set) Token: 0x060021CC RID: 8652 RVA: 0x0009F3BB File Offset: 0x0009E3BB
		internal bool IsFlags
		{
			get
			{
				return this.isFlags;
			}
			set
			{
				this.isFlags = value;
			}
		}

		// Token: 0x17000823 RID: 2083
		// (get) Token: 0x060021CD RID: 8653 RVA: 0x0009F3C4 File Offset: 0x0009E3C4
		// (set) Token: 0x060021CE RID: 8654 RVA: 0x0009F3CC File Offset: 0x0009E3CC
		internal ConstantMapping[] Constants
		{
			get
			{
				return this.constants;
			}
			set
			{
				this.constants = value;
			}
		}

		// Token: 0x0400147C RID: 5244
		private ConstantMapping[] constants;

		// Token: 0x0400147D RID: 5245
		private bool isFlags;
	}
}
