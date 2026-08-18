using System;

namespace System.Xml.Serialization
{
	// Token: 0x02000151 RID: 337
	internal class EnumMapping : PrimitiveMapping
	{
		// Token: 0x170004E0 RID: 1248
		// (get) Token: 0x06001778 RID: 6008 RVA: 0x00067697 File Offset: 0x00065897
		// (set) Token: 0x06001779 RID: 6009 RVA: 0x0006769F File Offset: 0x0006589F
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

		// Token: 0x170004E1 RID: 1249
		// (get) Token: 0x0600177A RID: 6010 RVA: 0x000676A8 File Offset: 0x000658A8
		// (set) Token: 0x0600177B RID: 6011 RVA: 0x000676B0 File Offset: 0x000658B0
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

		// Token: 0x04000AE2 RID: 2786
		private ConstantMapping[] constants;

		// Token: 0x04000AE3 RID: 2787
		private bool isFlags;
	}
}
