using System;

namespace ClockWorkWebAPIWeb
{
	// Token: 0x02000011 RID: 17
	public class ExtenderSet
	{
		// Token: 0x06000118 RID: 280 RVA: 0x0000E5F0 File Offset: 0x0000C7F0
		public ExtenderSet(ExtenderType extenderType, string controlId, string extenderId)
		{
			this.extenderType = extenderType;
			this.controlId = controlId;
			this.extenderId = extenderId;
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x06000119 RID: 281 RVA: 0x0000E610 File Offset: 0x0000C810
		public ExtenderType ExtenderType
		{
			get
			{
				return this.extenderType;
			}
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x0600011A RID: 282 RVA: 0x0000E628 File Offset: 0x0000C828
		public string ControlId
		{
			get
			{
				return this.controlId;
			}
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x0600011B RID: 283 RVA: 0x0000E640 File Offset: 0x0000C840
		public string ExtenderId
		{
			get
			{
				return this.extenderId;
			}
		}

		// Token: 0x04000074 RID: 116
		private string controlId;

		// Token: 0x04000075 RID: 117
		private string extenderId;

		// Token: 0x04000076 RID: 118
		private ExtenderType extenderType;
	}
}
