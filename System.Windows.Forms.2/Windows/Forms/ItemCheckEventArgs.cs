using System;
using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
	// Token: 0x020002AC RID: 684
	[ComVisible(true)]
	public class ItemCheckEventArgs : EventArgs
	{
		// Token: 0x06002A4B RID: 10827 RVA: 0x000BFB90 File Offset: 0x000BDD90
		public ItemCheckEventArgs(int index, CheckState newCheckValue, CheckState currentValue)
		{
			this.index = index;
			this.newValue = newCheckValue;
			this.currentValue = currentValue;
		}

		// Token: 0x170009E3 RID: 2531
		// (get) Token: 0x06002A4C RID: 10828 RVA: 0x000BFBAD File Offset: 0x000BDDAD
		public int Index
		{
			get
			{
				return this.index;
			}
		}

		// Token: 0x170009E4 RID: 2532
		// (get) Token: 0x06002A4D RID: 10829 RVA: 0x000BFBB5 File Offset: 0x000BDDB5
		// (set) Token: 0x06002A4E RID: 10830 RVA: 0x000BFBBD File Offset: 0x000BDDBD
		public CheckState NewValue
		{
			get
			{
				return this.newValue;
			}
			set
			{
				this.newValue = value;
			}
		}

		// Token: 0x170009E5 RID: 2533
		// (get) Token: 0x06002A4F RID: 10831 RVA: 0x000BFBC6 File Offset: 0x000BDDC6
		public CheckState CurrentValue
		{
			get
			{
				return this.currentValue;
			}
		}

		// Token: 0x04001130 RID: 4400
		private readonly int index;

		// Token: 0x04001131 RID: 4401
		private CheckState newValue;

		// Token: 0x04001132 RID: 4402
		private readonly CheckState currentValue;
	}
}
