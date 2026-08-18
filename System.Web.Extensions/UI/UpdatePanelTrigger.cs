using System;
using System.ComponentModel;

namespace System.Web.UI
{
	// Token: 0x02000087 RID: 135
	public abstract class UpdatePanelTrigger
	{
		// Token: 0x170001A2 RID: 418
		// (get) Token: 0x060005D3 RID: 1491 RVA: 0x0001AA28 File Offset: 0x00018C28
		[Browsable(false)]
		public UpdatePanel Owner
		{
			get
			{
				return this._owner;
			}
		}

		// Token: 0x060005D4 RID: 1492
		protected internal abstract bool HasTriggered();

		// Token: 0x060005D5 RID: 1493 RVA: 0x000032F4 File Offset: 0x000014F4
		protected internal virtual void Initialize()
		{
		}

		// Token: 0x060005D6 RID: 1494 RVA: 0x0001AA30 File Offset: 0x00018C30
		internal void SetOwner(UpdatePanel owner)
		{
			this._owner = owner;
		}

		// Token: 0x0400021B RID: 539
		private UpdatePanel _owner;
	}
}
