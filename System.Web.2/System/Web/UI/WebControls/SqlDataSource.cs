using System;
using System.Collections;
using System.ComponentModel;
using System.Data.Common;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Design;
using System.Globalization;
using System.Text;
using System.Web.Caching;

namespace System.Web.UI.WebControls
{
	// Token: 0x020004D0 RID: 1232
	[DefaultEvent("Selecting")]
	[DefaultProperty("SelectQuery")]
	[Designer("System.Web.UI.Design.WebControls.SqlDataSourceDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[ParseChildren(true)]
	[PersistChildren(false)]
	[ToolboxBitmap(typeof(SqlDataSource))]
	[WebSysDescription("SqlDataSource_Description")]
	[WebSysDisplayName("SqlDataSource_DisplayName")]
	public class SqlDataSource : DataSourceControl
	{
		// Token: 0x06003D2B RID: 15659 RVA: 0x000C5A45 File Offset: 0x000C3C45
		public SqlDataSource()
		{
		}

		// Token: 0x06003D2C RID: 15660 RVA: 0x000C5A54 File Offset: 0x000C3C54
		public SqlDataSource(string connectionString, string selectCommand)
		{
			this._connectionString = connectionString;
			this._cachedSelectCommand = selectCommand;
		}

		// Token: 0x06003D2D RID: 15661 RVA: 0x000C5A71 File Offset: 0x000C3C71
		public SqlDataSource(string providerName, string connectionString, string selectCommand) : this(connectionString, selectCommand)
		{
			this._providerName = providerName;
		}

		// Token: 0x170011DF RID: 4575
		// (get) Token: 0x06003D2E RID: 15662 RVA: 0x000C5A82 File Offset: 0x000C3C82
		internal virtual DataSourceCache Cache
		{
			get
			{
				if (this._cache == null)
				{
					this._cache = new SqlDataSourceCache();
				}
				return this._cache;
			}
		}

		// Token: 0x170011E0 RID: 4576
		// (get) Token: 0x06003D2F RID: 15663 RVA: 0x000C5A9D File Offset: 0x000C3C9D
		// (set) Token: 0x06003D30 RID: 15664 RVA: 0x000C5AAA File Offset: 0x000C3CAA
		[DefaultValue(0)]
		[TypeConverter(typeof(DataSourceCacheDurationConverter))]
		[WebCategory("Cache")]
		[WebSysDescription("DataSourceCache_Duration")]
		public virtual int CacheDuration
		{
			get
			{
				return this.Cache.Duration;
			}
			set
			{
				this.Cache.Duration = value;
			}
		}

		// Token: 0x170011E1 RID: 4577
		// (get) Token: 0x06003D31 RID: 15665 RVA: 0x000C5AB8 File Offset: 0x000C3CB8
		// (set) Token: 0x06003D32 RID: 15666 RVA: 0x000C5AC5 File Offset: 0x000C3CC5
		[DefaultValue(DataSourceCacheExpiry.Absolute)]
		[WebCategory("Cache")]
		[WebSysDescription("DataSourceCache_ExpirationPolicy")]
		public virtual DataSourceCacheExpiry CacheExpirationPolicy
		{
			get
			{
				return this.Cache.ExpirationPolicy;
			}
			set
			{
				this.Cache.ExpirationPolicy = value;
			}
		}

		// Token: 0x170011E2 RID: 4578
		// (get) Token: 0x06003D33 RID: 15667 RVA: 0x000C5AD3 File Offset: 0x000C3CD3
		// (set) Token: 0x06003D34 RID: 15668 RVA: 0x000C5AE0 File Offset: 0x000C3CE0
		[DefaultValue("")]
		[WebCategory("Cache")]
		[WebSysDescription("DataSourceCache_KeyDependency")]
		public virtual string CacheKeyDependency
		{
			get
			{
				return this.Cache.KeyDependency;
			}
			set
			{
				this.Cache.KeyDependency = value;
			}
		}

		// Token: 0x170011E3 RID: 4579
		// (get) Token: 0x06003D35 RID: 15669 RVA: 0x000C5AEE File Offset: 0x000C3CEE
		// (set) Token: 0x06003D36 RID: 15670 RVA: 0x000C5AFB File Offset: 0x000C3CFB
		[DefaultValue(true)]
		[WebCategory("Data")]
		[WebSysDescription("SqlDataSource_CancelSelectOnNullParameter")]
		public virtual bool CancelSelectOnNullParameter
		{
			get
			{
				return this.GetView().CancelSelectOnNullParameter;
			}
			set
			{
				this.GetView().CancelSelectOnNullParameter = value;
			}
		}

		// Token: 0x170011E4 RID: 4580
		// (get) Token: 0x06003D37 RID: 15671 RVA: 0x000C5B09 File Offset: 0x000C3D09
		// (set) Token: 0x06003D38 RID: 15672 RVA: 0x000C5B16 File Offset: 0x000C3D16
		[DefaultValue(ConflictOptions.OverwriteChanges)]
		[WebCategory("Data")]
		[WebSysDescription("SqlDataSource_ConflictDetection")]
		public ConflictOptions ConflictDetection
		{
			get
			{
				return this.GetView().ConflictDetection;
			}
			set
			{
				this.GetView().ConflictDetection = value;
			}
		}

		// Token: 0x170011E5 RID: 4581
		// (get) Token: 0x06003D39 RID: 15673 RVA: 0x000C5B24 File Offset: 0x000C3D24
		// (set) Token: 0x06003D3A RID: 15674 RVA: 0x000C5B3A File Offset: 0x000C3D3A
		[DefaultValue("")]
		[Editor("System.Web.UI.Design.WebControls.SqlDataSourceConnectionStringEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[WebCategory("Data")]
		[MergableProperty(false)]
		[WebSysDescription("SqlDataSource_ConnectionString")]
		public virtual string ConnectionString
		{
			get
			{
				if (this._connectionString != null)
				{
					return this._connectionString;
				}
				return string.Empty;
			}
			set
			{
				if (this.ConnectionString != value)
				{
					this._connectionString = value;
					this.RaiseDataSourceChangedEvent(EventArgs.Empty);
				}
			}
		}

		// Token: 0x170011E6 RID: 4582
		// (get) Token: 0x06003D3B RID: 15675 RVA: 0x000C5B5C File Offset: 0x000C3D5C
		// (set) Token: 0x06003D3C RID: 15676 RVA: 0x000C5B64 File Offset: 0x000C3D64
		[DefaultValue(SqlDataSourceMode.DataSet)]
		[WebCategory("Behavior")]
		[WebSysDescription("SqlDataSource_DataSourceMode")]
		public SqlDataSourceMode DataSourceMode
		{
			get
			{
				return this._dataSourceMode;
			}
			set
			{
				if (value < SqlDataSourceMode.DataReader || value > SqlDataSourceMode.DataSet)
				{
					throw new ArgumentOutOfRangeException(SR.GetString("SqlDataSource_InvalidMode", new object[]
					{
						this.ID
					}));
				}
				if (this.DataSourceMode != value)
				{
					this._dataSourceMode = value;
					this.RaiseDataSourceChangedEvent(EventArgs.Empty);
				}
			}
		}

		// Token: 0x170011E7 RID: 4583
		// (get) Token: 0x06003D3D RID: 15677 RVA: 0x000C5BB3 File Offset: 0x000C3DB3
		// (set) Token: 0x06003D3E RID: 15678 RVA: 0x000C5BC0 File Offset: 0x000C3DC0
		[DefaultValue("")]
		[WebCategory("Data")]
		[WebSysDescription("SqlDataSource_DeleteCommand")]
		public string DeleteCommand
		{
			get
			{
				return this.GetView().DeleteCommand;
			}
			set
			{
				this.GetView().DeleteCommand = value;
			}
		}

		// Token: 0x170011E8 RID: 4584
		// (get) Token: 0x06003D3F RID: 15679 RVA: 0x000C5BCE File Offset: 0x000C3DCE
		// (set) Token: 0x06003D40 RID: 15680 RVA: 0x000C5BDB File Offset: 0x000C3DDB
		[DefaultValue(SqlDataSourceCommandType.Text)]
		[WebCategory("Data")]
		[WebSysDescription("SqlDataSource_DeleteCommandType")]
		public SqlDataSourceCommandType DeleteCommandType
		{
			get
			{
				return this.GetView().DeleteCommandType;
			}
			set
			{
				this.GetView().DeleteCommandType = value;
			}
		}

		// Token: 0x170011E9 RID: 4585
		// (get) Token: 0x06003D41 RID: 15681 RVA: 0x000C5BE9 File Offset: 0x000C3DE9
		[DefaultValue(null)]
		[Editor("System.Web.UI.Design.WebControls.ParameterCollectionEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[MergableProperty(false)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[WebCategory("Data")]
		[WebSysDescription("SqlDataSource_DeleteParameters")]
		public ParameterCollection DeleteParameters
		{
			get
			{
				return this.GetView().DeleteParameters;
			}
		}

		// Token: 0x170011EA RID: 4586
		// (get) Token: 0x06003D42 RID: 15682 RVA: 0x000C5BF6 File Offset: 0x000C3DF6
		// (set) Token: 0x06003D43 RID: 15683 RVA: 0x000C5C03 File Offset: 0x000C3E03
		[DefaultValue(false)]
		[WebCategory("Cache")]
		[WebSysDescription("DataSourceCache_Enabled")]
		public virtual bool EnableCaching
		{
			get
			{
				return this.Cache.Enabled;
			}
			set
			{
				this.Cache.Enabled = value;
			}
		}

		// Token: 0x170011EB RID: 4587
		// (get) Token: 0x06003D44 RID: 15684 RVA: 0x000C5C11 File Offset: 0x000C3E11
		// (set) Token: 0x06003D45 RID: 15685 RVA: 0x000C5C1E File Offset: 0x000C3E1E
		[DefaultValue("")]
		[WebCategory("Data")]
		[WebSysDescription("SqlDataSource_FilterExpression")]
		public string FilterExpression
		{
			get
			{
				return this.GetView().FilterExpression;
			}
			set
			{
				this.GetView().FilterExpression = value;
			}
		}

		// Token: 0x170011EC RID: 4588
		// (get) Token: 0x06003D46 RID: 15686 RVA: 0x000C5C2C File Offset: 0x000C3E2C
		[DefaultValue(null)]
		[Editor("System.Web.UI.Design.WebControls.ParameterCollectionEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[MergableProperty(false)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[WebCategory("Data")]
		[WebSysDescription("SqlDataSource_FilterParameters")]
		public ParameterCollection FilterParameters
		{
			get
			{
				return this.GetView().FilterParameters;
			}
		}

		// Token: 0x170011ED RID: 4589
		// (get) Token: 0x06003D47 RID: 15687 RVA: 0x000C5C39 File Offset: 0x000C3E39
		// (set) Token: 0x06003D48 RID: 15688 RVA: 0x000C5C46 File Offset: 0x000C3E46
		[DefaultValue("")]
		[WebCategory("Data")]
		[WebSysDescription("SqlDataSource_InsertCommand")]
		public string InsertCommand
		{
			get
			{
				return this.GetView().InsertCommand;
			}
			set
			{
				this.GetView().InsertCommand = value;
			}
		}

		// Token: 0x170011EE RID: 4590
		// (get) Token: 0x06003D49 RID: 15689 RVA: 0x000C5C54 File Offset: 0x000C3E54
		// (set) Token: 0x06003D4A RID: 15690 RVA: 0x000C5C61 File Offset: 0x000C3E61
		[DefaultValue(SqlDataSourceCommandType.Text)]
		[WebCategory("Data")]
		[WebSysDescription("SqlDataSource_InsertCommandType")]
		public SqlDataSourceCommandType InsertCommandType
		{
			get
			{
				return this.GetView().InsertCommandType;
			}
			set
			{
				this.GetView().InsertCommandType = value;
			}
		}

		// Token: 0x170011EF RID: 4591
		// (get) Token: 0x06003D4B RID: 15691 RVA: 0x000C5C6F File Offset: 0x000C3E6F
		[DefaultValue(null)]
		[Editor("System.Web.UI.Design.WebControls.ParameterCollectionEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[MergableProperty(false)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[WebCategory("Data")]
		[WebSysDescription("SqlDataSource_InsertParameters")]
		public ParameterCollection InsertParameters
		{
			get
			{
				return this.GetView().InsertParameters;
			}
		}

		// Token: 0x170011F0 RID: 4592
		// (get) Token: 0x06003D4C RID: 15692 RVA: 0x000C5C7C File Offset: 0x000C3E7C
		// (set) Token: 0x06003D4D RID: 15693 RVA: 0x000C5C89 File Offset: 0x000C3E89
		[DefaultValue("{0}")]
		[WebCategory("Data")]
		[WebSysDescription("DataSource_OldValuesParameterFormatString")]
		public string OldValuesParameterFormatString
		{
			get
			{
				return this.GetView().OldValuesParameterFormatString;
			}
			set
			{
				this.GetView().OldValuesParameterFormatString = value;
			}
		}

		// Token: 0x170011F1 RID: 4593
		// (get) Token: 0x06003D4E RID: 15694 RVA: 0x000C5C97 File Offset: 0x000C3E97
		// (set) Token: 0x06003D4F RID: 15695 RVA: 0x000C5CAD File Offset: 0x000C3EAD
		[DefaultValue("")]
		[TypeConverter("System.Web.UI.Design.WebControls.DataProviderNameConverter, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[WebCategory("Data")]
		[WebSysDescription("SqlDataSource_ProviderName")]
		public virtual string ProviderName
		{
			get
			{
				if (this._providerName != null)
				{
					return this._providerName;
				}
				return string.Empty;
			}
			set
			{
				if (this.ProviderName != value)
				{
					this._providerFactory = null;
					this._providerName = value;
					this.RaiseDataSourceChangedEvent(EventArgs.Empty);
				}
			}
		}

		// Token: 0x170011F2 RID: 4594
		// (get) Token: 0x06003D50 RID: 15696 RVA: 0x000C5CD6 File Offset: 0x000C3ED6
		// (set) Token: 0x06003D51 RID: 15697 RVA: 0x000C5CE3 File Offset: 0x000C3EE3
		[DefaultValue("")]
		[WebCategory("Data")]
		[WebSysDescription("SqlDataSource_SelectCommand")]
		public string SelectCommand
		{
			get
			{
				return this.GetView().SelectCommand;
			}
			set
			{
				this.GetView().SelectCommand = value;
			}
		}

		// Token: 0x170011F3 RID: 4595
		// (get) Token: 0x06003D52 RID: 15698 RVA: 0x000C5CF1 File Offset: 0x000C3EF1
		// (set) Token: 0x06003D53 RID: 15699 RVA: 0x000C5CFE File Offset: 0x000C3EFE
		[DefaultValue(SqlDataSourceCommandType.Text)]
		[WebCategory("Data")]
		[WebSysDescription("SqlDataSource_SelectCommandType")]
		public SqlDataSourceCommandType SelectCommandType
		{
			get
			{
				return this.GetView().SelectCommandType;
			}
			set
			{
				this.GetView().SelectCommandType = value;
			}
		}

		// Token: 0x170011F4 RID: 4596
		// (get) Token: 0x06003D54 RID: 15700 RVA: 0x000C5D0C File Offset: 0x000C3F0C
		[DefaultValue(null)]
		[Editor("System.Web.UI.Design.WebControls.ParameterCollectionEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[MergableProperty(false)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[WebCategory("Data")]
		[WebSysDescription("SqlDataSource_SelectParameters")]
		public ParameterCollection SelectParameters
		{
			get
			{
				return this.GetView().SelectParameters;
			}
		}

		// Token: 0x170011F5 RID: 4597
		// (get) Token: 0x06003D55 RID: 15701 RVA: 0x000C5D19 File Offset: 0x000C3F19
		// (set) Token: 0x06003D56 RID: 15702 RVA: 0x000C5D26 File Offset: 0x000C3F26
		[DefaultValue("")]
		[WebCategory("Data")]
		[WebSysDescription("SqlDataSource_SortParameterName")]
		public string SortParameterName
		{
			get
			{
				return this.GetView().SortParameterName;
			}
			set
			{
				this.GetView().SortParameterName = value;
			}
		}

		// Token: 0x170011F6 RID: 4598
		// (get) Token: 0x06003D57 RID: 15703 RVA: 0x000C5D34 File Offset: 0x000C3F34
		private SqlDataSourceCache SqlDataSourceCache
		{
			get
			{
				SqlDataSourceCache sqlDataSourceCache = this.Cache as SqlDataSourceCache;
				if (sqlDataSourceCache == null)
				{
					throw new NotSupportedException(SR.GetString("SqlDataSource_SqlCacheDependencyNotSupported", new object[]
					{
						this.ID
					}));
				}
				return sqlDataSourceCache;
			}
		}

		// Token: 0x170011F7 RID: 4599
		// (get) Token: 0x06003D58 RID: 15704 RVA: 0x000C5D70 File Offset: 0x000C3F70
		// (set) Token: 0x06003D59 RID: 15705 RVA: 0x000C5D7D File Offset: 0x000C3F7D
		[DefaultValue("")]
		[WebCategory("Cache")]
		[WebSysDescription("SqlDataSourceCache_SqlCacheDependency")]
		public virtual string SqlCacheDependency
		{
			get
			{
				return this.SqlDataSourceCache.SqlCacheDependency;
			}
			set
			{
				this.SqlDataSourceCache.SqlCacheDependency = value;
			}
		}

		// Token: 0x170011F8 RID: 4600
		// (get) Token: 0x06003D5A RID: 15706 RVA: 0x000C5D8B File Offset: 0x000C3F8B
		// (set) Token: 0x06003D5B RID: 15707 RVA: 0x000C5D98 File Offset: 0x000C3F98
		[DefaultValue("")]
		[WebCategory("Data")]
		[WebSysDescription("SqlDataSource_UpdateCommand")]
		public string UpdateCommand
		{
			get
			{
				return this.GetView().UpdateCommand;
			}
			set
			{
				this.GetView().UpdateCommand = value;
			}
		}

		// Token: 0x170011F9 RID: 4601
		// (get) Token: 0x06003D5C RID: 15708 RVA: 0x000C5DA6 File Offset: 0x000C3FA6
		// (set) Token: 0x06003D5D RID: 15709 RVA: 0x000C5DB3 File Offset: 0x000C3FB3
		[DefaultValue(SqlDataSourceCommandType.Text)]
		[WebCategory("Data")]
		[WebSysDescription("SqlDataSource_UpdateCommandType")]
		public SqlDataSourceCommandType UpdateCommandType
		{
			get
			{
				return this.GetView().UpdateCommandType;
			}
			set
			{
				this.GetView().UpdateCommandType = value;
			}
		}

		// Token: 0x170011FA RID: 4602
		// (get) Token: 0x06003D5E RID: 15710 RVA: 0x000C5DC1 File Offset: 0x000C3FC1
		[DefaultValue(null)]
		[Editor("System.Web.UI.Design.WebControls.ParameterCollectionEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[MergableProperty(false)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[WebCategory("Data")]
		[WebSysDescription("SqlDataSource_UpdateParameters")]
		public ParameterCollection UpdateParameters
		{
			get
			{
				return this.GetView().UpdateParameters;
			}
		}

		// Token: 0x140000EB RID: 235
		// (add) Token: 0x06003D5F RID: 15711 RVA: 0x000C5DCE File Offset: 0x000C3FCE
		// (remove) Token: 0x06003D60 RID: 15712 RVA: 0x000C5DDC File Offset: 0x000C3FDC
		[WebCategory("Data")]
		[WebSysDescription("DataSource_Deleted")]
		public event SqlDataSourceStatusEventHandler Deleted
		{
			add
			{
				this.GetView().Deleted += value;
			}
			remove
			{
				this.GetView().Deleted -= value;
			}
		}

		// Token: 0x140000EC RID: 236
		// (add) Token: 0x06003D61 RID: 15713 RVA: 0x000C5DEA File Offset: 0x000C3FEA
		// (remove) Token: 0x06003D62 RID: 15714 RVA: 0x000C5DF8 File Offset: 0x000C3FF8
		[WebCategory("Data")]
		[WebSysDescription("DataSource_Deleting")]
		public event SqlDataSourceCommandEventHandler Deleting
		{
			add
			{
				this.GetView().Deleting += value;
			}
			remove
			{
				this.GetView().Deleting -= value;
			}
		}

		// Token: 0x140000ED RID: 237
		// (add) Token: 0x06003D63 RID: 15715 RVA: 0x000C5E06 File Offset: 0x000C4006
		// (remove) Token: 0x06003D64 RID: 15716 RVA: 0x000C5E14 File Offset: 0x000C4014
		[WebCategory("Data")]
		[WebSysDescription("DataSource_Filtering")]
		public event SqlDataSourceFilteringEventHandler Filtering
		{
			add
			{
				this.GetView().Filtering += value;
			}
			remove
			{
				this.GetView().Filtering -= value;
			}
		}

		// Token: 0x140000EE RID: 238
		// (add) Token: 0x06003D65 RID: 15717 RVA: 0x000C5E22 File Offset: 0x000C4022
		// (remove) Token: 0x06003D66 RID: 15718 RVA: 0x000C5E30 File Offset: 0x000C4030
		[WebCategory("Data")]
		[WebSysDescription("DataSource_Inserted")]
		public event SqlDataSourceStatusEventHandler Inserted
		{
			add
			{
				this.GetView().Inserted += value;
			}
			remove
			{
				this.GetView().Inserted -= value;
			}
		}

		// Token: 0x140000EF RID: 239
		// (add) Token: 0x06003D67 RID: 15719 RVA: 0x000C5E3E File Offset: 0x000C403E
		// (remove) Token: 0x06003D68 RID: 15720 RVA: 0x000C5E4C File Offset: 0x000C404C
		[WebCategory("Data")]
		[WebSysDescription("DataSource_Inserting")]
		public event SqlDataSourceCommandEventHandler Inserting
		{
			add
			{
				this.GetView().Inserting += value;
			}
			remove
			{
				this.GetView().Inserting -= value;
			}
		}

		// Token: 0x140000F0 RID: 240
		// (add) Token: 0x06003D69 RID: 15721 RVA: 0x000C5E5A File Offset: 0x000C405A
		// (remove) Token: 0x06003D6A RID: 15722 RVA: 0x000C5E68 File Offset: 0x000C4068
		[WebCategory("Data")]
		[WebSysDescription("SqlDataSource_Selected")]
		public event SqlDataSourceStatusEventHandler Selected
		{
			add
			{
				this.GetView().Selected += value;
			}
			remove
			{
				this.GetView().Selected -= value;
			}
		}

		// Token: 0x140000F1 RID: 241
		// (add) Token: 0x06003D6B RID: 15723 RVA: 0x000C5E76 File Offset: 0x000C4076
		// (remove) Token: 0x06003D6C RID: 15724 RVA: 0x000C5E84 File Offset: 0x000C4084
		[WebCategory("Data")]
		[WebSysDescription("SqlDataSource_Selecting")]
		public event SqlDataSourceSelectingEventHandler Selecting
		{
			add
			{
				this.GetView().Selecting += value;
			}
			remove
			{
				this.GetView().Selecting -= value;
			}
		}

		// Token: 0x140000F2 RID: 242
		// (add) Token: 0x06003D6D RID: 15725 RVA: 0x000C5E92 File Offset: 0x000C4092
		// (remove) Token: 0x06003D6E RID: 15726 RVA: 0x000C5EA0 File Offset: 0x000C40A0
		[WebCategory("Data")]
		[WebSysDescription("DataSource_Updated")]
		public event SqlDataSourceStatusEventHandler Updated
		{
			add
			{
				this.GetView().Updated += value;
			}
			remove
			{
				this.GetView().Updated -= value;
			}
		}

		// Token: 0x140000F3 RID: 243
		// (add) Token: 0x06003D6F RID: 15727 RVA: 0x000C5EAE File Offset: 0x000C40AE
		// (remove) Token: 0x06003D70 RID: 15728 RVA: 0x000C5EBC File Offset: 0x000C40BC
		[WebCategory("Data")]
		[WebSysDescription("DataSource_Updating")]
		public event SqlDataSourceCommandEventHandler Updating
		{
			add
			{
				this.GetView().Updating += value;
			}
			remove
			{
				this.GetView().Updating -= value;
			}
		}

		// Token: 0x06003D71 RID: 15729 RVA: 0x000C5ECC File Offset: 0x000C40CC
		internal string CreateCacheKey(int startRowIndex, int maximumRows)
		{
			StringBuilder stringBuilder = this.CreateRawCacheKey();
			stringBuilder.Append(startRowIndex.ToString(CultureInfo.InvariantCulture));
			stringBuilder.Append(':');
			stringBuilder.Append(maximumRows.ToString(CultureInfo.InvariantCulture));
			return stringBuilder.ToString();
		}

		// Token: 0x06003D72 RID: 15730 RVA: 0x000C5F18 File Offset: 0x000C4118
		internal DbConnection CreateConnection(string connectionString)
		{
			DbProviderFactory dbProviderFactorySecure = this.GetDbProviderFactorySecure();
			DbConnection dbConnection = dbProviderFactorySecure.CreateConnection();
			dbConnection.ConnectionString = connectionString;
			return dbConnection;
		}

		// Token: 0x06003D73 RID: 15731 RVA: 0x000C5F3C File Offset: 0x000C413C
		internal DbCommand CreateCommand(string commandText, DbConnection connection)
		{
			DbProviderFactory dbProviderFactorySecure = this.GetDbProviderFactorySecure();
			DbCommand dbCommand = dbProviderFactorySecure.CreateCommand();
			dbCommand.CommandText = commandText;
			dbCommand.Connection = connection;
			return dbCommand;
		}

		// Token: 0x06003D74 RID: 15732 RVA: 0x000C5F68 File Offset: 0x000C4168
		internal DbDataAdapter CreateDataAdapter(DbCommand command)
		{
			DbProviderFactory dbProviderFactorySecure = this.GetDbProviderFactorySecure();
			DbDataAdapter dbDataAdapter = dbProviderFactorySecure.CreateDataAdapter();
			dbDataAdapter.SelectCommand = command;
			return dbDataAdapter;
		}

		// Token: 0x06003D75 RID: 15733 RVA: 0x000C5F8B File Offset: 0x000C418B
		protected virtual SqlDataSourceView CreateDataSourceView(string viewName)
		{
			return new SqlDataSourceView(this, viewName, this.Context);
		}

		// Token: 0x06003D76 RID: 15734 RVA: 0x000C5F9A File Offset: 0x000C419A
		internal string CreateMasterCacheKey()
		{
			return this.CreateRawCacheKey().ToString();
		}

		// Token: 0x06003D77 RID: 15735 RVA: 0x000C5FA8 File Offset: 0x000C41A8
		internal DbParameter CreateParameter(string parameterName, object parameterValue)
		{
			DbProviderFactory dbProviderFactorySecure = this.GetDbProviderFactorySecure();
			DbParameter dbParameter = dbProviderFactorySecure.CreateParameter();
			dbParameter.ParameterName = parameterName;
			dbParameter.Value = parameterValue;
			return dbParameter;
		}

		// Token: 0x06003D78 RID: 15736 RVA: 0x000C5FD4 File Offset: 0x000C41D4
		private StringBuilder CreateRawCacheKey()
		{
			StringBuilder stringBuilder = new StringBuilder("u", 1024);
			stringBuilder.Append(base.GetType().GetHashCode().ToString(CultureInfo.InvariantCulture));
			stringBuilder.Append(this.CacheDuration.ToString(CultureInfo.InvariantCulture));
			stringBuilder.Append(':');
			stringBuilder.Append(((int)this.CacheExpirationPolicy).ToString(CultureInfo.InvariantCulture));
			SqlDataSourceCache sqlDataSourceCache = this.Cache as SqlDataSourceCache;
			if (sqlDataSourceCache != null)
			{
				stringBuilder.Append(":");
				stringBuilder.Append(sqlDataSourceCache.SqlCacheDependency);
			}
			stringBuilder.Append(":");
			stringBuilder.Append(this.ConnectionString);
			stringBuilder.Append(":");
			stringBuilder.Append(this.SelectCommand);
			if (this.SelectParameters.Count > 0)
			{
				stringBuilder.Append("?");
				IDictionary values = this.SelectParameters.GetValues(this.Context, this);
				foreach (object obj in values)
				{
					DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
					stringBuilder.Append(dictionaryEntry.Key.ToString());
					if (dictionaryEntry.Value != null && dictionaryEntry.Value != DBNull.Value)
					{
						stringBuilder.Append("=");
						stringBuilder.Append(dictionaryEntry.Value.ToString());
					}
					else if (dictionaryEntry.Value == DBNull.Value)
					{
						stringBuilder.Append("(dbnull)");
					}
					else
					{
						stringBuilder.Append("(null)");
					}
					stringBuilder.Append("&");
				}
			}
			return stringBuilder;
		}

		// Token: 0x06003D79 RID: 15737 RVA: 0x000C61A8 File Offset: 0x000C43A8
		public int Delete()
		{
			return this.GetView().Delete(null, null);
		}

		// Token: 0x06003D7A RID: 15738 RVA: 0x000C61B8 File Offset: 0x000C43B8
		protected virtual DbProviderFactory GetDbProviderFactory()
		{
			string providerName = this.ProviderName;
			if (string.IsNullOrEmpty(providerName))
			{
				return SqlClientFactory.Instance;
			}
			return DbProviderFactories.GetFactory(providerName);
		}

		// Token: 0x06003D7B RID: 15739 RVA: 0x000C61E0 File Offset: 0x000C43E0
		private DbProviderFactory GetDbProviderFactorySecure()
		{
			if (this._providerFactory == null)
			{
				this._providerFactory = this.GetDbProviderFactory();
				if (!HttpRuntime.DisableProcessRequestInApplicationTrust && !HttpRuntime.ProcessRequestInApplicationTrust && !HttpRuntime.HasDbPermission(this._providerFactory))
				{
					throw new HttpException(SR.GetString("SqlDataSource_NoDbPermission", new object[]
					{
						this._providerFactory.GetType().Name,
						this.ID
					}));
				}
			}
			return this._providerFactory;
		}

		// Token: 0x06003D7C RID: 15740 RVA: 0x000C6254 File Offset: 0x000C4454
		private SqlDataSourceView GetView()
		{
			if (this._view == null)
			{
				this._view = this.CreateDataSourceView("DefaultView");
				if (this._cachedSelectCommand != null)
				{
					this._view.SelectCommand = this._cachedSelectCommand;
				}
				if (base.IsTrackingViewState)
				{
					((IStateManager)this._view).TrackViewState();
				}
			}
			return this._view;
		}

		// Token: 0x06003D7D RID: 15741 RVA: 0x000C62AC File Offset: 0x000C44AC
		protected override DataSourceView GetView(string viewName)
		{
			if (viewName == null || (viewName.Length != 0 && !string.Equals(viewName, "DefaultView", StringComparison.OrdinalIgnoreCase)))
			{
				throw new ArgumentException(SR.GetString("DataSource_InvalidViewName", new object[]
				{
					this.ID,
					"DefaultView"
				}), "viewName");
			}
			return this.GetView();
		}

		// Token: 0x06003D7E RID: 15742 RVA: 0x000C6304 File Offset: 0x000C4504
		protected override ICollection GetViewNames()
		{
			if (this._viewNames == null)
			{
				this._viewNames = new string[]
				{
					"DefaultView"
				};
			}
			return this._viewNames;
		}

		// Token: 0x06003D7F RID: 15743 RVA: 0x000C6328 File Offset: 0x000C4528
		public int Insert()
		{
			return this.GetView().Insert(null);
		}

		// Token: 0x06003D80 RID: 15744 RVA: 0x000C6338 File Offset: 0x000C4538
		internal void InvalidateCacheEntry()
		{
			string key = this.CreateMasterCacheKey();
			DataSourceCache cache = this.Cache;
			cache.Invalidate(key);
		}

		// Token: 0x06003D81 RID: 15745 RVA: 0x000C635A File Offset: 0x000C455A
		private void LoadCompleteEventHandler(object sender, EventArgs e)
		{
			this.SelectParameters.UpdateValues(this.Context, this);
			this.FilterParameters.UpdateValues(this.Context, this);
		}

		// Token: 0x06003D82 RID: 15746 RVA: 0x000C6380 File Offset: 0x000C4580
		internal object LoadDataFromCache(int startRowIndex, int maximumRows)
		{
			string key = this.CreateCacheKey(startRowIndex, maximumRows);
			return this.Cache.LoadDataFromCache(key);
		}

		// Token: 0x06003D83 RID: 15747 RVA: 0x000C63A4 File Offset: 0x000C45A4
		internal int LoadTotalRowCountFromCache()
		{
			string key = this.CreateMasterCacheKey();
			object obj = this.Cache.LoadDataFromCache(key);
			if (obj is int)
			{
				return (int)obj;
			}
			return -1;
		}

		// Token: 0x06003D84 RID: 15748 RVA: 0x000C63D8 File Offset: 0x000C45D8
		protected override void LoadViewState(object savedState)
		{
			Pair pair = (Pair)savedState;
			if (savedState == null)
			{
				base.LoadViewState(null);
				return;
			}
			base.LoadViewState(pair.First);
			if (pair.Second != null)
			{
				((IStateManager)this.GetView()).LoadViewState(pair.Second);
			}
		}

		// Token: 0x06003D85 RID: 15749 RVA: 0x000C641C File Offset: 0x000C461C
		protected internal override void OnInit(EventArgs e)
		{
			base.OnInit(e);
			if (this.Page != null)
			{
				this.Page.LoadComplete += this.LoadCompleteEventHandler;
			}
		}

		// Token: 0x06003D86 RID: 15750 RVA: 0x000C6444 File Offset: 0x000C4644
		internal virtual void SaveDataToCache(int startRowIndex, int maximumRows, object data, CacheDependency dependency)
		{
			string key = this.CreateCacheKey(startRowIndex, maximumRows);
			string text = this.CreateMasterCacheKey();
			if (this.Cache.LoadDataFromCache(text) == null)
			{
				this.Cache.SaveDataToCache(text, -1, dependency);
			}
			CacheDependency dependency2 = new CacheDependency(0, new string[0], new string[]
			{
				text
			});
			this.Cache.SaveDataToCache(key, data, dependency2);
		}

		// Token: 0x06003D87 RID: 15751 RVA: 0x000C64A8 File Offset: 0x000C46A8
		protected override object SaveViewState()
		{
			Pair pair = new Pair();
			pair.First = base.SaveViewState();
			if (this._view != null)
			{
				pair.Second = ((IStateManager)this._view).SaveViewState();
			}
			if (pair.First == null && pair.Second == null)
			{
				return null;
			}
			return pair;
		}

		// Token: 0x06003D88 RID: 15752 RVA: 0x000C64F3 File Offset: 0x000C46F3
		public IEnumerable Select(DataSourceSelectArguments arguments)
		{
			return this.GetView().Select(arguments);
		}

		// Token: 0x06003D89 RID: 15753 RVA: 0x000C6501 File Offset: 0x000C4701
		protected override void TrackViewState()
		{
			base.TrackViewState();
			if (this._view != null)
			{
				((IStateManager)this._view).TrackViewState();
			}
		}

		// Token: 0x06003D8A RID: 15754 RVA: 0x000C651C File Offset: 0x000C471C
		public int Update()
		{
			return this.GetView().Update(null, null, null);
		}

		// Token: 0x040023B7 RID: 9143
		private const string DefaultProviderName = "System.Data.SqlClient";

		// Token: 0x040023B8 RID: 9144
		private const string DefaultViewName = "DefaultView";

		// Token: 0x040023B9 RID: 9145
		private DataSourceCache _cache;

		// Token: 0x040023BA RID: 9146
		private string _cachedSelectCommand;

		// Token: 0x040023BB RID: 9147
		private string _connectionString;

		// Token: 0x040023BC RID: 9148
		private SqlDataSourceMode _dataSourceMode = SqlDataSourceMode.DataSet;

		// Token: 0x040023BD RID: 9149
		private string _providerName;

		// Token: 0x040023BE RID: 9150
		private DbProviderFactory _providerFactory;

		// Token: 0x040023BF RID: 9151
		private SqlDataSourceView _view;

		// Token: 0x040023C0 RID: 9152
		private ICollection _viewNames;
	}
}
