using System;

namespace Telerik.Web.UI.Barcode
{
	// Token: 0x020009C6 RID: 2502
	internal abstract class SymbologyBase
	{
		// Token: 0x06006013 RID: 24595 RVA: 0x001249CE File Offset: 0x00122BCE
		public SymbologyBase()
		{
		}

		// Token: 0x17001FBA RID: 8122
		// (get) Token: 0x06006014 RID: 24596 RVA: 0x001249D6 File Offset: 0x00122BD6
		// (set) Token: 0x06006015 RID: 24597 RVA: 0x001249DE File Offset: 0x00122BDE
		public bool CalculateCheckSum
		{
			get
			{
				return this.calculateCheckSum;
			}
			set
			{
				this.calculateCheckSum = value;
			}
		}

		// Token: 0x04001738 RID: 5944
		private bool calculateCheckSum;
	}
}
