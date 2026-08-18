using System;
using System.Diagnostics.CodeAnalysis;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI.Common.Helpers;

namespace Telerik.Web.UI.Scheduling
{
	// Token: 0x02001323 RID: 4899
	internal class AppointmentTemplate : ITemplate, IDisposable
	{
		// Token: 0x0600CCB2 RID: 52402 RVA: 0x002D9F4B File Offset: 0x002D814B
		public AppointmentTemplate(RadScheduler owner)
		{
			this._owner = owner;
			this._isLightweight = (this._owner.ResolvedRenderMode == RenderMode.Lightweight);
			this._isMobile = (this._owner.ResolvedRenderMode == RenderMode.Mobile);
		}

		// Token: 0x0600CCB3 RID: 52403 RVA: 0x002D9F82 File Offset: 0x002D8182
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x0600CCB4 RID: 52404 RVA: 0x002D9F91 File Offset: 0x002D8191
		[SuppressMessage("Microsoft.Usage", "CA2213:DisposableFieldsShouldBeDisposed")]
		protected virtual void Dispose(bool disposing)
		{
		}

		// Token: 0x0600CCB5 RID: 52405 RVA: 0x002D9F98 File Offset: 0x002D8198
		public void InstantiateIn(Control container)
		{
			if (this._isLightweight || this._isMobile)
			{
				this._recurrenceIcon = new WebControl(HtmlTextWriterTag.Span);
				this._reminderIcon = new WebControl(HtmlTextWriterTag.Span);
			}
			else
			{
				this._recurrenceIcon = new WebControl(HtmlTextWriterTag.Div);
				this._reminderIcon = new WebControl(HtmlTextWriterTag.Div);
			}
			container.Controls.Add(this._recurrenceIcon);
			container.Controls.Add(this._reminderIcon);
			this._recurrenceIcon.DataBinding += this._recurrenceIcon_DataBinding;
			this._reminderIcon.DataBinding += this._reminderIcon_DataBinding;
			this._title = new Literal();
			container.Controls.Add(this._title);
			this._title.DataBinding += this.title_DataBinding;
		}

		// Token: 0x0600CCB6 RID: 52406 RVA: 0x002DA070 File Offset: 0x002D8270
		private void _reminderIcon_DataBinding(object sender, EventArgs e)
		{
			IDataItemContainer dataItemContainer = (IDataItemContainer)this._reminderIcon.BindingContainer;
			ReminderCollection reminderCollection = DataBinder.Eval(dataItemContainer.DataItem, "Reminders") as ReminderCollection;
			if (reminderCollection.Count > 0)
			{
				this._reminderIcon.CssClass = "rsAptReminder";
				if (this._isLightweight || this._isMobile)
				{
					this._reminderIcon.Controls.Add(IconHelper.CreateIcon("reminder"));
					return;
				}
			}
			else
			{
				this._reminderIcon.Visible = false;
			}
		}

		// Token: 0x0600CCB7 RID: 52407 RVA: 0x002DA0F4 File Offset: 0x002D82F4
		private void _recurrenceIcon_DataBinding(object sender, EventArgs e)
		{
			IDataItemContainer dataItemContainer = (IDataItemContainer)this._recurrenceIcon.BindingContainer;
			RecurrenceState recurrenceState = (RecurrenceState)DataBinder.Eval(dataItemContainer.DataItem, "RecurrenceState");
			if (recurrenceState != RecurrenceState.NotRecurring)
			{
				if (recurrenceState == RecurrenceState.Exception)
				{
					this._recurrenceIcon.CssClass = "rsAptRecurrenceException";
					if (this._isLightweight || this._isMobile)
					{
						this._recurrenceIcon.Controls.Add(IconHelper.CreateIcon("recurrence-exception"));
						return;
					}
				}
				else
				{
					this._recurrenceIcon.CssClass = "rsAptRecurrence";
					if (this._isLightweight || this._isMobile)
					{
						this._recurrenceIcon.Controls.Add(IconHelper.CreateIcon("recurrence"));
						return;
					}
				}
			}
			else
			{
				this._recurrenceIcon.Visible = false;
			}
		}

		// Token: 0x0600CCB8 RID: 52408 RVA: 0x002DA1B4 File Offset: 0x002D83B4
		private void title_DataBinding(object sender, EventArgs e)
		{
			IDataItemContainer dataItemContainer = (IDataItemContainer)this._title.BindingContainer;
			this._title.Text = HttpUtility.HtmlEncode((string)DataBinder.Eval(dataItemContainer.DataItem, "Subject"));
		}

		// Token: 0x0600CCB9 RID: 52409 RVA: 0x002DA1F8 File Offset: 0x002D83F8
		private WebControl CreateIcon(string cssClass)
		{
			return new WebControl(HtmlTextWriterTag.Span)
			{
				CssClass = string.Format("{0} {1}", "p-icon", cssClass)
			};
		}

		// Token: 0x0400368E RID: 13966
		private RadScheduler _owner;

		// Token: 0x0400368F RID: 13967
		private Literal _title;

		// Token: 0x04003690 RID: 13968
		private WebControl _recurrenceIcon;

		// Token: 0x04003691 RID: 13969
		private WebControl _reminderIcon;

		// Token: 0x04003692 RID: 13970
		private bool _isLightweight;

		// Token: 0x04003693 RID: 13971
		private bool _isMobile;
	}
}
