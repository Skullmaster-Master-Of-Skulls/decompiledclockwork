using System;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI.Scheduler
{
	// Token: 0x020007EA RID: 2026
	internal class HeaderControlFactory
	{
		// Token: 0x170016AC RID: 5804
		// (get) Token: 0x0600465C RID: 18012 RVA: 0x000DD841 File Offset: 0x000DBA41
		// (set) Token: 0x0600465D RID: 18013 RVA: 0x000DD849 File Offset: 0x000DBA49
		private RadScheduler Owner
		{
			get
			{
				return this._owner;
			}
			set
			{
				this._owner = value;
			}
		}

		// Token: 0x170016AD RID: 5805
		// (get) Token: 0x0600465E RID: 18014 RVA: 0x000DD852 File Offset: 0x000DBA52
		// (set) Token: 0x0600465F RID: 18015 RVA: 0x000DD85A File Offset: 0x000DBA5A
		private string DateLabel
		{
			get
			{
				return this._dateLabel;
			}
			set
			{
				this._dateLabel = value;
			}
		}

		// Token: 0x06004660 RID: 18016 RVA: 0x000DD863 File Offset: 0x000DBA63
		public HeaderControlFactory(string dateLabel, RadScheduler owner)
		{
			this._owner = owner;
			this._dateLabel = dateLabel;
		}

		// Token: 0x06004661 RID: 18017 RVA: 0x000DD87C File Offset: 0x000DBA7C
		public WebControl CreateHeaderControl()
		{
			if (this._owner.ResolvedRenderMode == RenderMode.Mobile)
			{
				return new HeaderControlNative(this.DateLabel, this.Owner);
			}
			if (this._owner.ResolvedRenderMode == RenderMode.Lightweight)
			{
				return new HeaderControlLite(this.DateLabel, this.Owner);
			}
			return new HeaderControl(this.DateLabel, this.Owner);
		}

		// Token: 0x04001233 RID: 4659
		private RadScheduler _owner;

		// Token: 0x04001234 RID: 4660
		private string _dateLabel;
	}
}
