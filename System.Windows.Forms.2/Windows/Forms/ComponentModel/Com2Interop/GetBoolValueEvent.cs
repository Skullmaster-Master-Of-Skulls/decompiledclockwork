using System;

namespace System.Windows.Forms.ComponentModel.Com2Interop
{
	// Token: 0x020004AA RID: 1194
	internal class GetBoolValueEvent : EventArgs
	{
		// Token: 0x06004F3E RID: 20286 RVA: 0x0014642E File Offset: 0x0014462E
		public GetBoolValueEvent(bool defValue)
		{
			this.value = defValue;
		}

		// Token: 0x17001372 RID: 4978
		// (get) Token: 0x06004F3F RID: 20287 RVA: 0x0014643D File Offset: 0x0014463D
		// (set) Token: 0x06004F40 RID: 20288 RVA: 0x00146445 File Offset: 0x00144645
		public bool Value
		{
			get
			{
				return this.value;
			}
			set
			{
				this.value = value;
			}
		}

		// Token: 0x0400344E RID: 13390
		private bool value;
	}
}
