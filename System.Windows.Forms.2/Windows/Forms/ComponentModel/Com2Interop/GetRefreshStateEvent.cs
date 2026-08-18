using System;

namespace System.Windows.Forms.ComponentModel.Com2Interop
{
	// Token: 0x020004AC RID: 1196
	internal class GetRefreshStateEvent : GetBoolValueEvent
	{
		// Token: 0x06004F45 RID: 20293 RVA: 0x0014644E File Offset: 0x0014464E
		public GetRefreshStateEvent(Com2ShouldRefreshTypes item, bool defValue) : base(defValue)
		{
			this.item = item;
		}

		// Token: 0x0400344F RID: 13391
		private Com2ShouldRefreshTypes item;
	}
}
