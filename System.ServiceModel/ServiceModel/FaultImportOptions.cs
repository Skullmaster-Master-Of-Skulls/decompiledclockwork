using System;

namespace System.ServiceModel
{
	// Token: 0x02000119 RID: 281
	public class FaultImportOptions
	{
		// Token: 0x170001D6 RID: 470
		// (get) Token: 0x06000735 RID: 1845 RVA: 0x0001E565 File Offset: 0x0001C765
		// (set) Token: 0x06000736 RID: 1846 RVA: 0x0001E56D File Offset: 0x0001C76D
		public bool UseMessageFormat
		{
			get
			{
				return this.useMessageFormat;
			}
			set
			{
				this.useMessageFormat = value;
			}
		}

		// Token: 0x04000ABA RID: 2746
		private bool useMessageFormat;
	}
}
