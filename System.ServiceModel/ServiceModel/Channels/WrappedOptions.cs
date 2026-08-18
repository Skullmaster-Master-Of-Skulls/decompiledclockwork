using System;

namespace System.ServiceModel.Channels
{
	// Token: 0x020009A3 RID: 2467
	public class WrappedOptions
	{
		// Token: 0x17001745 RID: 5957
		// (get) Token: 0x060060D7 RID: 24791 RVA: 0x00169F1A File Offset: 0x0016811A
		// (set) Token: 0x060060D8 RID: 24792 RVA: 0x00169F22 File Offset: 0x00168122
		public bool WrappedFlag
		{
			get
			{
				return this.wrappedFlag;
			}
			set
			{
				this.wrappedFlag = value;
			}
		}

		// Token: 0x040038A6 RID: 14502
		private bool wrappedFlag;
	}
}
