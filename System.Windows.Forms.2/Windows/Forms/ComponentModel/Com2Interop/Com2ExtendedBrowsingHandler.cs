using System;

namespace System.Windows.Forms.ComponentModel.Com2Interop
{
	// Token: 0x02000498 RID: 1176
	internal abstract class Com2ExtendedBrowsingHandler
	{
		// Token: 0x1700134A RID: 4938
		// (get) Token: 0x06004E80 RID: 20096
		public abstract Type Interface { get; }

		// Token: 0x06004E81 RID: 20097 RVA: 0x00143381 File Offset: 0x00141581
		public virtual void SetupPropertyHandlers(Com2PropertyDescriptor propDesc)
		{
			this.SetupPropertyHandlers(new Com2PropertyDescriptor[]
			{
				propDesc
			});
		}

		// Token: 0x06004E82 RID: 20098
		public abstract void SetupPropertyHandlers(Com2PropertyDescriptor[] propDesc);
	}
}
