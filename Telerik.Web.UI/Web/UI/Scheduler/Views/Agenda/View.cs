using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Telerik.Web.UI.Scheduler.Views.Agenda
{
	// Token: 0x02000829 RID: 2089
	internal class View : ViewBase
	{
		// Token: 0x17001943 RID: 6467
		// (get) Token: 0x06004D47 RID: 19783 RVA: 0x000F2F68 File Offset: 0x000F1168
		public string CssClass
		{
			get
			{
				return "rsMonthView";
			}
		}

		// Token: 0x17001944 RID: 6468
		// (get) Token: 0x06004D48 RID: 19784 RVA: 0x000F2F6F File Offset: 0x000F116F
		// (set) Token: 0x06004D49 RID: 19785 RVA: 0x000F2F77 File Offset: 0x000F1177
		public override ISchedulerModel Model
		{
			get
			{
				return this._model;
			}
			protected set
			{
				this._model = value;
			}
		}

		// Token: 0x17001945 RID: 6469
		// (get) Token: 0x06004D4A RID: 19786 RVA: 0x000F2F80 File Offset: 0x000F1180
		public override RadScheduler Owner
		{
			get
			{
				return this.Model.Owner as RadScheduler;
			}
		}

		// Token: 0x06004D4B RID: 19787 RVA: 0x000F2F92 File Offset: 0x000F1192
		[SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
		public View(ModelBase model)
		{
			this.Model = model;
		}

		// Token: 0x06004D4C RID: 19788 RVA: 0x000F2FA1 File Offset: 0x000F11A1
		protected override void InitializeColumnHeaders()
		{
			if (!this.Owner.AgendaView.ShowColumnHeaders)
			{
				return;
			}
			base.ColumnHeaders = this.CreateColumnHeaders();
		}

		// Token: 0x06004D4D RID: 19789 RVA: 0x000F2FC4 File Offset: 0x000F11C4
		protected virtual IList<ViewHeader> CreateColumnHeaders()
		{
			List<ViewHeader> list = new List<ViewHeader>();
			this.AddTimeHeader(list);
			this.AddAppointmentHeader(list);
			return list;
		}

		// Token: 0x06004D4E RID: 19790 RVA: 0x000F2FE6 File Offset: 0x000F11E6
		protected void AddDateHeader(IList<ViewHeader> headers)
		{
			if (this.Owner.AgendaView.ShowDateHeadersResolved)
			{
				this.AddHeader(headers, this.Owner.Localization.HeaderAgendaDate);
			}
		}

		// Token: 0x06004D4F RID: 19791 RVA: 0x000F3011 File Offset: 0x000F1211
		protected void AddTimeHeader(IList<ViewHeader> headers)
		{
			this.AddHeader(headers, this.Owner.Localization.HeaderAgendaTime);
		}

		// Token: 0x06004D50 RID: 19792 RVA: 0x000F302A File Offset: 0x000F122A
		protected void AddAppointmentHeader(IList<ViewHeader> headers)
		{
			this.AddHeader(headers, this.Owner.Localization.HeaderAgendaAppointment);
		}

		// Token: 0x06004D51 RID: 19793 RVA: 0x000F3044 File Offset: 0x000F1244
		protected void AddHeader(IList<ViewHeader> headers, string headerText)
		{
			headers.Add(new ViewHeader
			{
				Text = headerText
			});
		}

		// Token: 0x06004D52 RID: 19794 RVA: 0x000F3065 File Offset: 0x000F1265
		protected override void InitializeRowHeaders()
		{
		}

		// Token: 0x0400135A RID: 4954
		private ISchedulerModel _model;
	}
}
