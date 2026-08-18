using System;
using System.ComponentModel;

namespace Telerik.Web.UI.DataSourceSettings
{
	// Token: 0x02000106 RID: 262
	public class StaticDataSourceSettings : StateManager
	{
		// Token: 0x06000AF9 RID: 2809 RVA: 0x000270F8 File Offset: 0x000252F8
		public StaticDataSourceSettings(RadClientDataSource dataSource)
		{
			this._ownerDataSource = dataSource;
		}

		// Token: 0x170003C8 RID: 968
		// (get) Token: 0x06000AFB RID: 2811 RVA: 0x0002713B File Offset: 0x0002533B
		// (set) Token: 0x06000AFC RID: 2812 RVA: 0x0002714D File Offset: 0x0002534D
		[DefaultValue(null)]
		[Description("Gets or sets the filter type that is expected from the data service.")]
		[NotifyParentProperty(true)]
		[Category("Data")]
		public virtual object Data
		{
			get
			{
				return base.ViewState["Data"];
			}
			set
			{
				base.ViewState["Data"] = value;
			}
		}

		// Token: 0x14000010 RID: 16
		// (add) Token: 0x06000AFD RID: 2813 RVA: 0x00027160 File Offset: 0x00025360
		// (remove) Token: 0x06000AFE RID: 2814 RVA: 0x00027178 File Offset: 0x00025378
		[Description("Server side event which is fired when existing item is updated on the client and transferred to the server by using sync operation")]
		[Category("Action")]
		public event EventHandler<RadClientDataSourceBaseEventArgs> Update
		{
			add
			{
				this._ownerDataSource.Events.AddHandler(StaticDataSourceSettings.EventUpdate, value);
			}
			remove
			{
				this._ownerDataSource.Events.RemoveHandler(StaticDataSourceSettings.EventUpdate, value);
			}
		}

		// Token: 0x06000AFF RID: 2815 RVA: 0x00027190 File Offset: 0x00025390
		protected virtual void OnUpdate(RadClientDataSourceBaseEventArgs e)
		{
			EventHandler<RadClientDataSourceBaseEventArgs> eventHandler = this._ownerDataSource.Events[StaticDataSourceSettings.EventUpdate] as EventHandler<RadClientDataSourceBaseEventArgs>;
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x14000011 RID: 17
		// (add) Token: 0x06000B00 RID: 2816 RVA: 0x000271C3 File Offset: 0x000253C3
		// (remove) Token: 0x06000B01 RID: 2817 RVA: 0x000271DB File Offset: 0x000253DB
		[Description("Server side event which is fired when new item is created on the client and transferred to the server by using sync operation")]
		[Category("Action")]
		public event EventHandler<RadClientDataSourceBaseEventArgs> Insert
		{
			add
			{
				this._ownerDataSource.Events.AddHandler(StaticDataSourceSettings.EventInsert, value);
			}
			remove
			{
				this._ownerDataSource.Events.RemoveHandler(StaticDataSourceSettings.EventInsert, value);
			}
		}

		// Token: 0x06000B02 RID: 2818 RVA: 0x000271F4 File Offset: 0x000253F4
		protected virtual void OnInsert(RadClientDataSourceBaseEventArgs e)
		{
			EventHandler<RadClientDataSourceBaseEventArgs> eventHandler = this._ownerDataSource.Events[StaticDataSourceSettings.EventInsert] as EventHandler<RadClientDataSourceBaseEventArgs>;
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x14000012 RID: 18
		// (add) Token: 0x06000B03 RID: 2819 RVA: 0x00027227 File Offset: 0x00025427
		// (remove) Token: 0x06000B04 RID: 2820 RVA: 0x0002723F File Offset: 0x0002543F
		[Description("Server side event which is fired when existing item is deleted on the client and sync operation is performed")]
		[Category("Action")]
		public event EventHandler<RadClientDataSourceBaseEventArgs> Delete
		{
			add
			{
				this._ownerDataSource.Events.AddHandler(StaticDataSourceSettings.EventDelete, value);
			}
			remove
			{
				this._ownerDataSource.Events.RemoveHandler(StaticDataSourceSettings.EventDelete, value);
			}
		}

		// Token: 0x06000B05 RID: 2821 RVA: 0x00027258 File Offset: 0x00025458
		protected virtual void OnDelete(RadClientDataSourceBaseEventArgs e)
		{
			EventHandler<RadClientDataSourceBaseEventArgs> eventHandler = this._ownerDataSource.Events[StaticDataSourceSettings.EventDelete] as EventHandler<RadClientDataSourceBaseEventArgs>;
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x14000013 RID: 19
		// (add) Token: 0x06000B06 RID: 2822 RVA: 0x0002728B File Offset: 0x0002548B
		// (remove) Token: 0x06000B07 RID: 2823 RVA: 0x000272A3 File Offset: 0x000254A3
		[Category("Action")]
		[Description("Server side event which is fired when on client side CRUD operation is performed and sync operation is triggered.")]
		public event EventHandler<RadClientDataSourceBaseEventArgs> Batch
		{
			add
			{
				this._ownerDataSource.Events.AddHandler(StaticDataSourceSettings.EventBatch, value);
			}
			remove
			{
				this._ownerDataSource.Events.RemoveHandler(StaticDataSourceSettings.EventBatch, value);
			}
		}

		// Token: 0x06000B08 RID: 2824 RVA: 0x000272BC File Offset: 0x000254BC
		protected virtual void OnBatch(RadClientDataSourceBaseEventArgs e)
		{
			EventHandler<RadClientDataSourceBaseEventArgs> eventHandler = this._ownerDataSource.Events[StaticDataSourceSettings.EventBatch] as EventHandler<RadClientDataSourceBaseEventArgs>;
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x14000014 RID: 20
		// (add) Token: 0x06000B09 RID: 2825 RVA: 0x000272EF File Offset: 0x000254EF
		// (remove) Token: 0x06000B0A RID: 2826 RVA: 0x00027307 File Offset: 0x00025507
		[Category("Action")]
		[Description("Fires when the data source must be assigned on server side.")]
		public event EventHandler<RadClientDataSourceNeedDataSourceEventArgs> NeedDataSource
		{
			add
			{
				this._ownerDataSource.Events.AddHandler(StaticDataSourceSettings.EventNeedDataSource, value);
			}
			remove
			{
				this._ownerDataSource.Events.RemoveHandler(StaticDataSourceSettings.EventNeedDataSource, value);
			}
		}

		// Token: 0x06000B0B RID: 2827 RVA: 0x00027320 File Offset: 0x00025520
		protected virtual void OnBatch(RadClientDataSourceNeedDataSourceEventArgs e)
		{
			EventHandler<RadClientDataSourceNeedDataSourceEventArgs> eventHandler = this._ownerDataSource.Events[StaticDataSourceSettings.EventNeedDataSource] as EventHandler<RadClientDataSourceNeedDataSourceEventArgs>;
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x040002A6 RID: 678
		private RadClientDataSource _ownerDataSource;

		// Token: 0x040002A7 RID: 679
		private static readonly object EventUpdate = new object();

		// Token: 0x040002A8 RID: 680
		private static readonly object EventInsert = new object();

		// Token: 0x040002A9 RID: 681
		private static readonly object EventDelete = new object();

		// Token: 0x040002AA RID: 682
		private static readonly object EventBatch = new object();

		// Token: 0x040002AB RID: 683
		private static readonly object EventNeedDataSource = new object();
	}
}
