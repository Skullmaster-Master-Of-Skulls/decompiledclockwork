using System;
using System.ComponentModel.Design;
using System.Design;
using System.Security.Permissions;

namespace System.Web.UI.Design
{
	// Token: 0x0200002C RID: 44
	[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
	public class DataSourceDesigner : ControlDesigner, IDataSourceDesigner
	{
		// Token: 0x14000001 RID: 1
		// (add) Token: 0x06000159 RID: 345 RVA: 0x0000C470 File Offset: 0x0000A670
		// (remove) Token: 0x0600015A RID: 346 RVA: 0x0000C4A8 File Offset: 0x0000A6A8
		private event EventHandler _dataSourceChangedEvent;

		// Token: 0x14000002 RID: 2
		// (add) Token: 0x0600015B RID: 347 RVA: 0x0000C4E0 File Offset: 0x0000A6E0
		// (remove) Token: 0x0600015C RID: 348 RVA: 0x0000C518 File Offset: 0x0000A718
		private event EventHandler _schemaRefreshedEvent;

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x0600015D RID: 349 RVA: 0x0000C550 File Offset: 0x0000A750
		public override DesignerActionListCollection ActionLists
		{
			get
			{
				DesignerActionListCollection designerActionListCollection = new DesignerActionListCollection();
				designerActionListCollection.AddRange(base.ActionLists);
				designerActionListCollection.Add(new DataSourceDesigner.DataSourceDesignerActionList(this));
				return designerActionListCollection;
			}
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x0600015E RID: 350 RVA: 0x0000445B File Offset: 0x0000265B
		public virtual bool CanConfigure
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x0600015F RID: 351 RVA: 0x0000445B File Offset: 0x0000265B
		public virtual bool CanRefreshSchema
		{
			get
			{
				return false;
			}
		}

		// Token: 0x14000003 RID: 3
		// (add) Token: 0x06000160 RID: 352 RVA: 0x0000C57D File Offset: 0x0000A77D
		// (remove) Token: 0x06000161 RID: 353 RVA: 0x0000C586 File Offset: 0x0000A786
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

		// Token: 0x14000004 RID: 4
		// (add) Token: 0x06000162 RID: 354 RVA: 0x0000C58F File Offset: 0x0000A78F
		// (remove) Token: 0x06000163 RID: 355 RVA: 0x0000C598 File Offset: 0x0000A798
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

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x06000164 RID: 356 RVA: 0x0000C5A1 File Offset: 0x0000A7A1
		protected bool SuppressingDataSourceEvents
		{
			get
			{
				return this._suppressEventsCount > 0;
			}
		}

		// Token: 0x06000165 RID: 357 RVA: 0x0000C5AC File Offset: 0x0000A7AC
		public virtual void Configure()
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000166 RID: 358 RVA: 0x0000C5B3 File Offset: 0x0000A7B3
		public override string GetDesignTimeHtml()
		{
			return base.CreatePlaceHolderDesignTimeHtml();
		}

		// Token: 0x06000167 RID: 359 RVA: 0x00003598 File Offset: 0x00001798
		public virtual DesignerDataSourceView GetView(string viewName)
		{
			return null;
		}

		// Token: 0x06000168 RID: 360 RVA: 0x0000C5BB File Offset: 0x0000A7BB
		public virtual string[] GetViewNames()
		{
			return new string[0];
		}

		// Token: 0x06000169 RID: 361 RVA: 0x0000C5C3 File Offset: 0x0000A7C3
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

		// Token: 0x0600016A RID: 362 RVA: 0x0000C5F1 File Offset: 0x0000A7F1
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

		// Token: 0x0600016B RID: 363 RVA: 0x0000C5AC File Offset: 0x0000A7AC
		public virtual void RefreshSchema(bool preferSilent)
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600016C RID: 364 RVA: 0x0000C620 File Offset: 0x0000A820
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

		// Token: 0x0600016D RID: 365 RVA: 0x0000C681 File Offset: 0x0000A881
		public virtual void SuppressDataSourceEvents()
		{
			this._suppressEventsCount++;
		}

		// Token: 0x0600016E RID: 366 RVA: 0x0000C694 File Offset: 0x0000A894
		public static bool SchemasEquivalent(IDataSourceSchema schema1, IDataSourceSchema schema2)
		{
			if (schema1 == null ^ schema2 == null)
			{
				return false;
			}
			if (schema1 == null && schema2 == null)
			{
				return true;
			}
			IDataSourceViewSchema[] views = schema1.GetViews();
			IDataSourceViewSchema[] views2 = schema2.GetViews();
			if (views == null ^ views2 == null)
			{
				return false;
			}
			if (views == null && views2 == null)
			{
				return true;
			}
			int num = views.Length;
			int num2 = views2.Length;
			if (num != num2)
			{
				return false;
			}
			foreach (IDataSourceViewSchema dataSourceViewSchema in views)
			{
				bool flag = false;
				string name = dataSourceViewSchema.Name;
				foreach (IDataSourceViewSchema dataSourceViewSchema2 in views2)
				{
					if (name == dataSourceViewSchema2.Name && DataSourceDesigner.ViewSchemasEquivalent(dataSourceViewSchema, dataSourceViewSchema2))
					{
						flag = true;
						break;
					}
				}
				if (!flag)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600016F RID: 367 RVA: 0x0000C754 File Offset: 0x0000A954
		public static bool ViewSchemasEquivalent(IDataSourceViewSchema viewSchema1, IDataSourceViewSchema viewSchema2)
		{
			if (viewSchema1 == null ^ viewSchema2 == null)
			{
				return false;
			}
			if (viewSchema1 == null && viewSchema2 == null)
			{
				return true;
			}
			IDataSourceFieldSchema[] fields = viewSchema1.GetFields();
			IDataSourceFieldSchema[] fields2 = viewSchema2.GetFields();
			if (fields == null ^ fields2 == null)
			{
				return false;
			}
			if (fields == null && fields2 == null)
			{
				return true;
			}
			int num = fields.Length;
			int num2 = fields2.Length;
			if (num != num2)
			{
				return false;
			}
			foreach (IDataSourceFieldSchema dataSourceFieldSchema in fields)
			{
				bool flag = false;
				string name = dataSourceFieldSchema.Name;
				Type dataType = dataSourceFieldSchema.DataType;
				foreach (IDataSourceFieldSchema dataSourceFieldSchema2 in fields2)
				{
					if (name == dataSourceFieldSchema2.Name && dataType == dataSourceFieldSchema2.DataType)
					{
						flag = true;
						break;
					}
				}
				if (!flag)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x04000115 RID: 277
		private int _suppressEventsCount;

		// Token: 0x04000116 RID: 278
		private bool _raiseDataSourceChangedEvent;

		// Token: 0x04000117 RID: 279
		private bool _raiseSchemaRefreshedEvent;

		// Token: 0x020003AE RID: 942
		private class DataSourceDesignerActionList : DesignerActionList
		{
			// Token: 0x060025F4 RID: 9716 RVA: 0x000EC3FB File Offset: 0x000EA5FB
			public DataSourceDesignerActionList(DataSourceDesigner parent) : base(parent.Component)
			{
				this._parent = parent;
			}

			// Token: 0x170007FD RID: 2045
			// (get) Token: 0x060025F5 RID: 9717 RVA: 0x00003B0F File Offset: 0x00001D0F
			// (set) Token: 0x060025F6 RID: 9718 RVA: 0x00003937 File Offset: 0x00001B37
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

			// Token: 0x060025F7 RID: 9719 RVA: 0x000EC410 File Offset: 0x000EA610
			public void Configure()
			{
				this._parent.Configure();
			}

			// Token: 0x060025F8 RID: 9720 RVA: 0x000EC41D File Offset: 0x000EA61D
			public void RefreshSchema()
			{
				this._parent.RefreshSchema(false);
			}

			// Token: 0x060025F9 RID: 9721 RVA: 0x000EC42C File Offset: 0x000EA62C
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

			// Token: 0x04001BAA RID: 7082
			private DataSourceDesigner _parent;
		}
	}
}
