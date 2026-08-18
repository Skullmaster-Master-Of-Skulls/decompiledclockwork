using System;
using System.ComponentModel.Design;
using System.Design;
using System.Security.Permissions;

namespace System.Web.UI.Design
{
	// Token: 0x02000043 RID: 67
	[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
	public class HierarchicalDataSourceDesigner : ControlDesigner, IHierarchicalDataSourceDesigner
	{
		// Token: 0x14000005 RID: 5
		// (add) Token: 0x0600023F RID: 575 RVA: 0x0000F40C File Offset: 0x0000D60C
		// (remove) Token: 0x06000240 RID: 576 RVA: 0x0000F444 File Offset: 0x0000D644
		private event EventHandler _dataSourceChangedEvent;

		// Token: 0x14000006 RID: 6
		// (add) Token: 0x06000241 RID: 577 RVA: 0x0000F47C File Offset: 0x0000D67C
		// (remove) Token: 0x06000242 RID: 578 RVA: 0x0000F4B4 File Offset: 0x0000D6B4
		private event EventHandler _schemaRefreshedEvent;

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x06000243 RID: 579 RVA: 0x0000F4EC File Offset: 0x0000D6EC
		public override DesignerActionListCollection ActionLists
		{
			get
			{
				DesignerActionListCollection designerActionListCollection = new DesignerActionListCollection();
				designerActionListCollection.AddRange(base.ActionLists);
				designerActionListCollection.Add(new HierarchicalDataSourceDesigner.HierarchicalDataSourceDesignerActionList(this));
				return designerActionListCollection;
			}
		}

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x06000244 RID: 580 RVA: 0x0000445B File Offset: 0x0000265B
		public virtual bool CanConfigure
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x06000245 RID: 581 RVA: 0x0000445B File Offset: 0x0000265B
		public virtual bool CanRefreshSchema
		{
			get
			{
				return false;
			}
		}

		// Token: 0x14000007 RID: 7
		// (add) Token: 0x06000246 RID: 582 RVA: 0x0000F519 File Offset: 0x0000D719
		// (remove) Token: 0x06000247 RID: 583 RVA: 0x0000F522 File Offset: 0x0000D722
		public event EventHandler DataSourceChanged
		{
			add
			{
				this._dataSourceChangedEvent += value;
			}
			remove
			{
				this._dataSourceChangedEvent -= value;
			}
		}

		// Token: 0x14000008 RID: 8
		// (add) Token: 0x06000248 RID: 584 RVA: 0x0000F52B File Offset: 0x0000D72B
		// (remove) Token: 0x06000249 RID: 585 RVA: 0x0000F534 File Offset: 0x0000D734
		public event EventHandler SchemaRefreshed
		{
			add
			{
				this._schemaRefreshedEvent += value;
			}
			remove
			{
				this._schemaRefreshedEvent -= value;
			}
		}

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x0600024A RID: 586 RVA: 0x0000F53D File Offset: 0x0000D73D
		protected bool SuppressingDataSourceEvents
		{
			get
			{
				return this._suppressEventsCount > 0;
			}
		}

		// Token: 0x0600024B RID: 587 RVA: 0x0000C5AC File Offset: 0x0000A7AC
		public virtual void Configure()
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600024C RID: 588 RVA: 0x0000C5B3 File Offset: 0x0000A7B3
		public override string GetDesignTimeHtml()
		{
			return base.CreatePlaceHolderDesignTimeHtml();
		}

		// Token: 0x0600024D RID: 589 RVA: 0x00003598 File Offset: 0x00001798
		public virtual DesignerHierarchicalDataSourceView GetView(string viewPath)
		{
			return null;
		}

		// Token: 0x0600024E RID: 590 RVA: 0x0000F548 File Offset: 0x0000D748
		protected virtual void OnDataSourceChanged(EventArgs e)
		{
			if (this.SuppressingDataSourceEvents)
			{
				this._raiseDataSourceChangedEvent = true;
				return;
			}
			if (this._dataSourceChangedEvent != null)
			{
				this._dataSourceChangedEvent(this, e);
			}
			this._raiseDataSourceChangedEvent = false;
		}

		// Token: 0x0600024F RID: 591 RVA: 0x0000F576 File Offset: 0x0000D776
		protected virtual void OnSchemaRefreshed(EventArgs e)
		{
			if (this.SuppressingDataSourceEvents)
			{
				this._raiseSchemaRefreshedEvent = true;
				return;
			}
			if (this._schemaRefreshedEvent != null)
			{
				this._schemaRefreshedEvent(this, e);
			}
			this._raiseSchemaRefreshedEvent = false;
		}

		// Token: 0x06000250 RID: 592 RVA: 0x0000C5AC File Offset: 0x0000A7AC
		public virtual void RefreshSchema(bool preferSilent)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000251 RID: 593 RVA: 0x0000F5A4 File Offset: 0x0000D7A4
		public virtual void ResumeDataSourceEvents()
		{
			if (this._suppressEventsCount == 0)
			{
				throw new InvalidOperationException(SR.GetString("DataSource_CannotResumeEvents"));
			}
			this._suppressEventsCount--;
			if (this._suppressEventsCount == 0)
			{
				if (this._raiseDataSourceChangedEvent)
				{
					this.OnDataSourceChanged(EventArgs.Empty);
				}
				if (this._raiseSchemaRefreshedEvent)
				{
					this.OnSchemaRefreshed(EventArgs.Empty);
				}
			}
		}

		// Token: 0x06000252 RID: 594 RVA: 0x0000F605 File Offset: 0x0000D805
		public virtual void SuppressDataSourceEvents()
		{
			this._suppressEventsCount++;
		}

		// Token: 0x0400015C RID: 348
		private int _suppressEventsCount;

		// Token: 0x0400015D RID: 349
		private bool _raiseDataSourceChangedEvent;

		// Token: 0x0400015E RID: 350
		private bool _raiseSchemaRefreshedEvent;

		// Token: 0x020003B3 RID: 947
		private class HierarchicalDataSourceDesignerActionList : DesignerActionList
		{
			// Token: 0x0600260A RID: 9738 RVA: 0x000EC5EE File Offset: 0x000EA7EE
			public HierarchicalDataSourceDesignerActionList(HierarchicalDataSourceDesigner parent) : base(parent.Component)
			{
				this._parent = parent;
			}

			// Token: 0x17000804 RID: 2052
			// (get) Token: 0x0600260B RID: 9739 RVA: 0x00003B0F File Offset: 0x00001D0F
			// (set) Token: 0x0600260C RID: 9740 RVA: 0x00003937 File Offset: 0x00001B37
			public override bool AutoShow
			{
				get
				{
					return true;
				}
				set
				{
				}
			}

			// Token: 0x0600260D RID: 9741 RVA: 0x000EC603 File Offset: 0x000EA803
			public void Configure()
			{
				this._parent.Configure();
			}

			// Token: 0x0600260E RID: 9742 RVA: 0x000EC610 File Offset: 0x000EA810
			public void RefreshSchema()
			{
				this._parent.RefreshSchema(false);
			}

			// Token: 0x0600260F RID: 9743 RVA: 0x000EC620 File Offset: 0x000EA820
			public override DesignerActionItemCollection GetSortedActionItems()
			{
				DesignerActionItemCollection designerActionItemCollection = new DesignerActionItemCollection();
				if (this._parent.CanConfigure)
				{
					designerActionItemCollection.Add(new DesignerActionMethodItem(this, "Configure", SR.GetString("DataSourceDesigner_ConfigureDataSourceVerb"), SR.GetString("DataSourceDesigner_DataActionGroup"), SR.GetString("DataSourceDesigner_ConfigureDataSourceVerbDesc"), true)
					{
						AllowAssociate = true
					});
				}
				if (this._parent.CanRefreshSchema)
				{
					designerActionItemCollection.Add(new DesignerActionMethodItem(this, "RefreshSchema", SR.GetString("DataSourceDesigner_RefreshSchemaVerb"), SR.GetString("DataSourceDesigner_DataActionGroup"), SR.GetString("DataSourceDesigner_RefreshSchemaVerbDesc"), false)
					{
						AllowAssociate = true
					});
				}
				return designerActionItemCollection;
			}

			// Token: 0x04001BB0 RID: 7088
			private HierarchicalDataSourceDesigner _parent;
		}
	}
}
