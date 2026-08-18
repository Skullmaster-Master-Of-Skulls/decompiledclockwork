using System;

namespace System.Data.EntityModel.SchemaObjectModel
{
	// Token: 0x02000306 RID: 774
	internal sealed class ReturnValue<T>
	{
		// Token: 0x06002DDD RID: 11741 RVA: 0x00002050 File Offset: 0x00000250
		internal ReturnValue()
		{
		}

		// Token: 0x170008FA RID: 2298
		// (get) Token: 0x06002DDE RID: 11742 RVA: 0x000AD9F2 File Offset: 0x000ABBF2
		internal bool Succeeded
		{
			get
			{
				return this._succeeded;
			}
		}

		// Token: 0x170008FB RID: 2299
		// (get) Token: 0x06002DDF RID: 11743 RVA: 0x000AD9FA File Offset: 0x000ABBFA
		// (set) Token: 0x06002DE0 RID: 11744 RVA: 0x000ADA02 File Offset: 0x000ABC02
		internal T Value
		{
			get
			{
				return this._value;
			}
			set
			{
				this._value = value;
				this._succeeded = true;
			}
		}

		// Token: 0x040013F7 RID: 5111
		private bool _succeeded;

		// Token: 0x040013F8 RID: 5112
		private T _value;
	}
}
