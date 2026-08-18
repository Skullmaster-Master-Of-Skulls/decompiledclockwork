using System;
using System.Reflection.Emit;

namespace System.Xml.Serialization
{
	// Token: 0x02000132 RID: 306
	internal class ForState
	{
		// Token: 0x06001697 RID: 5783 RVA: 0x00063D62 File Offset: 0x00061F62
		internal ForState(LocalBuilder indexVar, Label beginLabel, Label testLabel, object end)
		{
			this.indexVar = indexVar;
			this.beginLabel = beginLabel;
			this.testLabel = testLabel;
			this.end = end;
		}

		// Token: 0x1700049D RID: 1181
		// (get) Token: 0x06001698 RID: 5784 RVA: 0x00063D87 File Offset: 0x00061F87
		internal LocalBuilder Index
		{
			get
			{
				return this.indexVar;
			}
		}

		// Token: 0x1700049E RID: 1182
		// (get) Token: 0x06001699 RID: 5785 RVA: 0x00063D8F File Offset: 0x00061F8F
		internal Label BeginLabel
		{
			get
			{
				return this.beginLabel;
			}
		}

		// Token: 0x1700049F RID: 1183
		// (get) Token: 0x0600169A RID: 5786 RVA: 0x00063D97 File Offset: 0x00061F97
		internal Label TestLabel
		{
			get
			{
				return this.testLabel;
			}
		}

		// Token: 0x170004A0 RID: 1184
		// (get) Token: 0x0600169B RID: 5787 RVA: 0x00063D9F File Offset: 0x00061F9F
		internal object End
		{
			get
			{
				return this.end;
			}
		}

		// Token: 0x04000A83 RID: 2691
		private LocalBuilder indexVar;

		// Token: 0x04000A84 RID: 2692
		private Label beginLabel;

		// Token: 0x04000A85 RID: 2693
		private Label testLabel;

		// Token: 0x04000A86 RID: 2694
		private object end;
	}
}
