using System;
using System.Reflection.Emit;

namespace System.Xml.Serialization
{
	// Token: 0x02000134 RID: 308
	internal class IfState
	{
		// Token: 0x170004A1 RID: 1185
		// (get) Token: 0x0600169C RID: 5788 RVA: 0x00063DA7 File Offset: 0x00061FA7
		// (set) Token: 0x0600169D RID: 5789 RVA: 0x00063DAF File Offset: 0x00061FAF
		internal Label EndIf
		{
			get
			{
				return this.endIf;
			}
			set
			{
				this.endIf = value;
			}
		}

		// Token: 0x170004A2 RID: 1186
		// (get) Token: 0x0600169E RID: 5790 RVA: 0x00063DB8 File Offset: 0x00061FB8
		// (set) Token: 0x0600169F RID: 5791 RVA: 0x00063DC0 File Offset: 0x00061FC0
		internal Label ElseBegin
		{
			get
			{
				return this.elseBegin;
			}
			set
			{
				this.elseBegin = value;
			}
		}

		// Token: 0x04000A8E RID: 2702
		private Label elseBegin;

		// Token: 0x04000A8F RID: 2703
		private Label endIf;
	}
}
