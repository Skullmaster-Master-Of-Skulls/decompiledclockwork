using System;
using System.ComponentModel;
using System.Globalization;
using System.Web.Resources;

namespace System.Web.UI.WebControls
{
	// Token: 0x020000C8 RID: 200
	public class TemplatePagerField : DataPagerField
	{
		// Token: 0x170002E5 RID: 741
		// (get) Token: 0x060009F1 RID: 2545 RVA: 0x00025D36 File Offset: 0x00023F36
		private EventHandlerList Events
		{
			get
			{
				if (this._events == null)
				{
					this._events = new EventHandlerList();
				}
				return this._events;
			}
		}

		// Token: 0x170002E6 RID: 742
		// (get) Token: 0x060009F2 RID: 2546 RVA: 0x00025D51 File Offset: 0x00023F51
		// (set) Token: 0x060009F3 RID: 2547 RVA: 0x00025D59 File Offset: 0x00023F59
		[Browsable(false)]
		[DefaultValue(null)]
		[ResourceDescription("TemplatePagerField_PagerTemplate")]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[TemplateContainer(typeof(DataPagerFieldItem), BindingDirection.TwoWay)]
		public virtual ITemplate PagerTemplate
		{
			get
			{
				return this._pagerTemplate;
			}
			set
			{
				this._pagerTemplate = value;
				this.OnFieldChanged();
			}
		}

		// Token: 0x14000044 RID: 68
		// (add) Token: 0x060009F4 RID: 2548 RVA: 0x00025D68 File Offset: 0x00023F68
		// (remove) Token: 0x060009F5 RID: 2549 RVA: 0x00025D7B File Offset: 0x00023F7B
		[Category("Action")]
		[ResourceDescription("TemplatePagerField_OnPagerCommand")]
		public event EventHandler<DataPagerCommandEventArgs> PagerCommand
		{
			add
			{
				this.Events.AddHandler(TemplatePagerField.EventPagerCommand, value);
			}
			remove
			{
				this.Events.RemoveHandler(TemplatePagerField.EventPagerCommand, value);
			}
		}

		// Token: 0x060009F6 RID: 2550 RVA: 0x00025D8E File Offset: 0x00023F8E
		protected override void CopyProperties(DataPagerField newField)
		{
			((TemplatePagerField)newField).PagerTemplate = this.PagerTemplate;
			base.CopyProperties(newField);
		}

		// Token: 0x060009F7 RID: 2551 RVA: 0x00025DA8 File Offset: 0x00023FA8
		protected override DataPagerField CreateField()
		{
			return new TemplatePagerField();
		}

		// Token: 0x060009F8 RID: 2552 RVA: 0x00025DB0 File Offset: 0x00023FB0
		public override void HandleEvent(CommandEventArgs e)
		{
			DataPagerFieldItem item = null;
			DataPagerFieldCommandEventArgs dataPagerFieldCommandEventArgs = e as DataPagerFieldCommandEventArgs;
			if (dataPagerFieldCommandEventArgs != null)
			{
				item = dataPagerFieldCommandEventArgs.Item;
			}
			DataPagerCommandEventArgs dataPagerCommandEventArgs = new DataPagerCommandEventArgs(this, this._totalRowCount, e, item);
			this.OnPagerCommand(dataPagerCommandEventArgs);
			if (dataPagerCommandEventArgs.NewStartRowIndex != -1)
			{
				base.DataPager.SetPageProperties(dataPagerCommandEventArgs.NewStartRowIndex, dataPagerCommandEventArgs.NewMaximumRows, true);
			}
		}

		// Token: 0x060009F9 RID: 2553 RVA: 0x00025E07 File Offset: 0x00024007
		public override void CreateDataPagers(DataPagerFieldItem container, int startRowIndex, int maximumRows, int totalRowCount, int fieldIndex)
		{
			this._startRowIndex = startRowIndex;
			this._maximumRows = maximumRows;
			this._totalRowCount = totalRowCount;
			if (this._pagerTemplate != null)
			{
				this._pagerTemplate.InstantiateIn(container);
			}
		}

		// Token: 0x060009FA RID: 2554 RVA: 0x00025E34 File Offset: 0x00024034
		protected virtual void OnPagerCommand(DataPagerCommandEventArgs e)
		{
			EventHandler<DataPagerCommandEventArgs> eventHandler = (EventHandler<DataPagerCommandEventArgs>)this.Events[TemplatePagerField.EventPagerCommand];
			if (eventHandler != null)
			{
				eventHandler(this, e);
				return;
			}
			throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, AtlasWeb.TemplatePagerField_UnhandledEvent, new object[]
			{
				"PagerCommand"
			}));
		}

		// Token: 0x0400033C RID: 828
		private int _startRowIndex;

		// Token: 0x0400033D RID: 829
		private int _maximumRows;

		// Token: 0x0400033E RID: 830
		private int _totalRowCount;

		// Token: 0x0400033F RID: 831
		private ITemplate _pagerTemplate;

		// Token: 0x04000340 RID: 832
		private static readonly object EventPagerCommand = new object();

		// Token: 0x04000341 RID: 833
		private EventHandlerList _events;
	}
}
