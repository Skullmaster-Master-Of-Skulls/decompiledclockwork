using System;
using System.ComponentModel;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x020012F4 RID: 4852
	[ToolboxItem(false)]
	public class SchedulerAppointmentContainer : Control, IDataItemContainer, INamingContainer
	{
		// Token: 0x0600CBC6 RID: 52166 RVA: 0x002D8702 File Offset: 0x002D6902
		public SchedulerAppointmentContainer(RadScheduler owner)
		{
			this._owner = owner;
		}

		// Token: 0x170041B6 RID: 16822
		// (get) Token: 0x0600CBC7 RID: 52167 RVA: 0x002D8711 File Offset: 0x002D6911
		// (set) Token: 0x0600CBC8 RID: 52168 RVA: 0x002D8741 File Offset: 0x002D6941
		public Appointment Appointment
		{
			get
			{
				if (this._appointment == null)
				{
					return null;
				}
				if (this._appointment.Owner == null)
				{
					this._appointment.Owner = this._owner;
				}
				return this._appointment;
			}
			set
			{
				this._appointment = value;
			}
		}

		// Token: 0x170041B7 RID: 16823
		// (get) Token: 0x0600CBC9 RID: 52169 RVA: 0x002D874A File Offset: 0x002D694A
		// (set) Token: 0x0600CBCA RID: 52170 RVA: 0x002D8752 File Offset: 0x002D6952
		public ITemplate Template
		{
			get
			{
				return this._template;
			}
			set
			{
				this._template = value;
			}
		}

		// Token: 0x170041B8 RID: 16824
		// (get) Token: 0x0600CBCB RID: 52171 RVA: 0x002D875B File Offset: 0x002D695B
		protected RadScheduler Owner
		{
			get
			{
				return this._owner;
			}
		}

		// Token: 0x0600CBCC RID: 52172 RVA: 0x002D8763 File Offset: 0x002D6963
		protected virtual object GetDataItem()
		{
			return this.Appointment;
		}

		// Token: 0x170041B9 RID: 16825
		// (get) Token: 0x0600CBCD RID: 52173 RVA: 0x002D876B File Offset: 0x002D696B
		object IDataItemContainer.DataItem
		{
			get
			{
				return this.GetDataItem();
			}
		}

		// Token: 0x170041BA RID: 16826
		// (get) Token: 0x0600CBCE RID: 52174 RVA: 0x002D8773 File Offset: 0x002D6973
		int IDataItemContainer.DataItemIndex
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x170041BB RID: 16827
		// (get) Token: 0x0600CBCF RID: 52175 RVA: 0x002D8776 File Offset: 0x002D6976
		int IDataItemContainer.DisplayIndex
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x0400357D RID: 13693
		private readonly RadScheduler _owner;

		// Token: 0x0400357E RID: 13694
		private Appointment _appointment;

		// Token: 0x0400357F RID: 13695
		private ITemplate _template;
	}
}
