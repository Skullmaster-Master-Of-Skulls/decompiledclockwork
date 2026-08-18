using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Globalization;
using System.Text;
using System.Web.Caching;

namespace System.Web.UI.WebControls
{
	// Token: 0x02000485 RID: 1157
	[DefaultEvent("Selecting")]
	[DefaultProperty("TypeName")]
	[Designer("System.Web.UI.Design.WebControls.ObjectDataSourceDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[ParseChildren(true)]
	[PersistChildren(false)]
	[ToolboxBitmap(typeof(ObjectDataSource))]
	[WebSysDescription("ObjectDataSource_Description")]
	[WebSysDisplayName("ObjectDataSource_DisplayName")]
	public class ObjectDataSource : DataSourceControl
	{
		// Token: 0x0600393F RID: 14655 RVA: 0x000BA4DB File Offset: 0x000B86DB
		public ObjectDataSource()
		{
		}

		// Token: 0x06003940 RID: 14656 RVA: 0x000BA4E3 File Offset: 0x000B86E3
		public ObjectDataSource(string typeName, string selectMethod)
		{
			this.TypeName = typeName;
			this.SelectMethod = selectMethod;
		}

		// Token: 0x170010B0 RID: 4272
		// (get) Token: 0x06003941 RID: 14657 RVA: 0x000BA4F9 File Offset: 0x000B86F9
		internal SqlDataSourceCache Cache
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

		// Token: 0x170010B1 RID: 4273
		// (get) Token: 0x06003942 RID: 14658 RVA: 0x000BA514 File Offset: 0x000B8714
		// (set) Token: 0x06003943 RID: 14659 RVA: 0x000BA521 File Offset: 0x000B8721
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

		// Token: 0x170010B2 RID: 4274
		// (get) Token: 0x06003944 RID: 14660 RVA: 0x000BA52F File Offset: 0x000B872F
		// (set) Token: 0x06003945 RID: 14661 RVA: 0x000BA53C File Offset: 0x000B873C
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

		// Token: 0x170010B3 RID: 4275
		// (get) Token: 0x06003946 RID: 14662 RVA: 0x000BA54A File Offset: 0x000B874A
		// (set) Token: 0x06003947 RID: 14663 RVA: 0x000BA557 File Offset: 0x000B8757
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

		// Token: 0x170010B4 RID: 4276
		// (get) Token: 0x06003948 RID: 14664 RVA: 0x000BA565 File Offset: 0x000B8765
		// (set) Token: 0x06003949 RID: 14665 RVA: 0x000BA572 File Offset: 0x000B8772
		[DefaultValue(ConflictOptions.OverwriteChanges)]
		[WebCategory("Data")]
		[WebSysDescription("ObjectDataSource_ConflictDetection")]
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

		// Token: 0x170010B5 RID: 4277
		// (get) Token: 0x0600394A RID: 14666 RVA: 0x000BA580 File Offset: 0x000B8780
		// (set) Token: 0x0600394B RID: 14667 RVA: 0x000BA58D File Offset: 0x000B878D
		[DefaultValue(false)]
		[WebCategory("Data")]
		[WebSysDescription("ObjectDataSource_ConvertNullToDBNull")]
		public bool ConvertNullToDBNull
		{
			get
			{
				return this.GetView().ConvertNullToDBNull;
			}
			set
			{
				this.GetView().ConvertNullToDBNull = value;
			}
		}

		// Token: 0x170010B6 RID: 4278
		// (get) Token: 0x0600394C RID: 14668 RVA: 0x000BA59B File Offset: 0x000B879B
		// (set) Token: 0x0600394D RID: 14669 RVA: 0x000BA5A8 File Offset: 0x000B87A8
		[DefaultValue("")]
		[WebCategory("Data")]
		[WebSysDescription("ObjectDataSource_DataObjectTypeName")]
		public string DataObjectTypeName
		{
			get
			{
				return this.GetView().DataObjectTypeName;
			}
			set
			{
				this.GetView().DataObjectTypeName = value;
			}
		}

		// Token: 0x170010B7 RID: 4279
		// (get) Token: 0x0600394E RID: 14670 RVA: 0x000BA5B6 File Offset: 0x000B87B6
		// (set) Token: 0x0600394F RID: 14671 RVA: 0x000BA5C3 File Offset: 0x000B87C3
		[DefaultValue("")]
		[WebCategory("Data")]
		[WebSysDescription("ObjectDataSource_DeleteMethod")]
		public string DeleteMethod
		{
			get
			{
				return this.GetView().DeleteMethod;
			}
			set
			{
				this.GetView().DeleteMethod = value;
			}
		}

		// Token: 0x170010B8 RID: 4280
		// (get) Token: 0x06003950 RID: 14672 RVA: 0x000BA5D1 File Offset: 0x000B87D1
		[DefaultValue(null)]
		[Editor("System.Web.UI.Design.WebControls.ParameterCollectionEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[MergableProperty(false)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[WebCategory("Data")]
		[WebSysDescription("ObjectDataSource_DeleteParameters")]
		public ParameterCollection DeleteParameters
		{
			get
			{
				return this.GetView().DeleteParameters;
			}
		}

		// Token: 0x170010B9 RID: 4281
		// (get) Token: 0x06003951 RID: 14673 RVA: 0x000BA5DE File Offset: 0x000B87DE
		// (set) Token: 0x06003952 RID: 14674 RVA: 0x000BA5EB File Offset: 0x000B87EB
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

		// Token: 0x170010BA RID: 4282
		// (get) Token: 0x06003953 RID: 14675 RVA: 0x000BA5F9 File Offset: 0x000B87F9
		// (set) Token: 0x06003954 RID: 14676 RVA: 0x000BA606 File Offset: 0x000B8806
		[DefaultValue(false)]
		[WebCategory("Paging")]
		[WebSysDescription("ObjectDataSource_EnablePaging")]
		public bool EnablePaging
		{
			get
			{
				return this.GetView().EnablePaging;
			}
			set
			{
				this.GetView().EnablePaging = value;
			}
		}

		// Token: 0x170010BB RID: 4283
		// (get) Token: 0x06003955 RID: 14677 RVA: 0x000BA614 File Offset: 0x000B8814
		// (set) Token: 0x06003956 RID: 14678 RVA: 0x000BA621 File Offset: 0x000B8821
		[DefaultValue("")]
		[WebCategory("Data")]
		[WebSysDescription("ObjectDataSource_FilterExpression")]
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

		// Token: 0x170010BC RID: 4284
		// (get) Token: 0x06003957 RID: 14679 RVA: 0x000BA62F File Offset: 0x000B882F
		[DefaultValue(null)]
		[Editor("System.Web.UI.Design.WebControls.ParameterCollectionEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[MergableProperty(false)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[WebCategory("Data")]
		[WebSysDescription("ObjectDataSource_FilterParameters")]
		public ParameterCollection FilterParameters
		{
			get
			{
				return this.GetView().FilterParameters;
			}
		}

		// Token: 0x170010BD RID: 4285
		// (get) Token: 0x06003958 RID: 14680 RVA: 0x000BA63C File Offset: 0x000B883C
		// (set) Token: 0x06003959 RID: 14681 RVA: 0x000BA649 File Offset: 0x000B8849
		[DefaultValue("")]
		[WebCategory("Data")]
		[WebSysDescription("ObjectDataSource_InsertMethod")]
		public string InsertMethod
		{
			get
			{
				return this.GetView().InsertMethod;
			}
			set
			{
				this.GetView().InsertMethod = value;
			}
		}

		// Token: 0x170010BE RID: 4286
		// (get) Token: 0x0600395A RID: 14682 RVA: 0x000BA657 File Offset: 0x000B8857
		[DefaultValue(null)]
		[Editor("System.Web.UI.Design.WebControls.ParameterCollectionEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[MergableProperty(false)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[WebCategory("Data")]
		[WebSysDescription("ObjectDataSource_InsertParameters")]
		public ParameterCollection InsertParameters
		{
			get
			{
				return this.GetView().InsertParameters;
			}
		}

		// Token: 0x170010BF RID: 4287
		// (get) Token: 0x0600395B RID: 14683 RVA: 0x000BA664 File Offset: 0x000B8864
		// (set) Token: 0x0600395C RID: 14684 RVA: 0x000BA671 File Offset: 0x000B8871
		[DefaultValue("maximumRows")]
		[WebCategory("Paging")]
		[WebSysDescription("ObjectDataSource_MaximumRowsParameterName")]
		public string MaximumRowsParameterName
		{
			get
			{
				return this.GetView().MaximumRowsParameterName;
			}
			set
			{
				this.GetView().MaximumRowsParameterName = value;
			}
		}

		// Token: 0x170010C0 RID: 4288
		// (get) Token: 0x0600395D RID: 14685 RVA: 0x000BA67F File Offset: 0x000B887F
		// (set) Token: 0x0600395E RID: 14686 RVA: 0x000BA68C File Offset: 0x000B888C
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

		// Token: 0x170010C1 RID: 4289
		// (get) Token: 0x0600395F RID: 14687 RVA: 0x000BA69A File Offset: 0x000B889A
		// (set) Token: 0x06003960 RID: 14688 RVA: 0x000BA6A7 File Offset: 0x000B88A7
		[DefaultValue("")]
		[WebCategory("Paging")]
		[WebSysDescription("ObjectDataSource_SelectCountMethod")]
		public string SelectCountMethod
		{
			get
			{
				return this.GetView().SelectCountMethod;
			}
			set
			{
				this.GetView().SelectCountMethod = value;
			}
		}

		// Token: 0x170010C2 RID: 4290
		// (get) Token: 0x06003961 RID: 14689 RVA: 0x000BA6B5 File Offset: 0x000B88B5
		// (set) Token: 0x06003962 RID: 14690 RVA: 0x000BA6C2 File Offset: 0x000B88C2
		[DefaultValue("")]
		[WebCategory("Data")]
		[WebSysDescription("ObjectDataSource_SelectMethod")]
		public string SelectMethod
		{
			get
			{
				return this.GetView().SelectMethod;
			}
			set
			{
				this.GetView().SelectMethod = value;
			}
		}

		// Token: 0x170010C3 RID: 4291
		// (get) Token: 0x06003963 RID: 14691 RVA: 0x000BA6D0 File Offset: 0x000B88D0
		[DefaultValue(null)]
		[Editor("System.Web.UI.Design.WebControls.ParameterCollectionEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[MergableProperty(false)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[WebCategory("Data")]
		[WebSysDescription("ObjectDataSource_SelectParameters")]
		public ParameterCollection SelectParameters
		{
			get
			{
				return this.GetView().SelectParameters;
			}
		}

		// Token: 0x170010C4 RID: 4292
		// (get) Token: 0x06003964 RID: 14692 RVA: 0x000BA6DD File Offset: 0x000B88DD
		// (set) Token: 0x06003965 RID: 14693 RVA: 0x000BA6EA File Offset: 0x000B88EA
		[DefaultValue("")]
		[WebCategory("Data")]
		[WebSysDescription("ObjectDataSource_SortParameterName")]
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

		// Token: 0x170010C5 RID: 4293
		// (get) Token: 0x06003966 RID: 14694 RVA: 0x000BA6F8 File Offset: 0x000B88F8
		// (set) Token: 0x06003967 RID: 14695 RVA: 0x000BA705 File Offset: 0x000B8905
		[DefaultValue("")]
		[WebCategory("Cache")]
		[WebSysDescription("SqlDataSourceCache_SqlCacheDependency")]
		public virtual string SqlCacheDependency
		{
			get
			{
				return this.Cache.SqlCacheDependency;
			}
			set
			{
				this.Cache.SqlCacheDependency = value;
			}
		}

		// Token: 0x170010C6 RID: 4294
		// (get) Token: 0x06003968 RID: 14696 RVA: 0x000BA713 File Offset: 0x000B8913
		// (set) Token: 0x06003969 RID: 14697 RVA: 0x000BA720 File Offset: 0x000B8920
		[DefaultValue("startRowIndex")]
		[WebCategory("Paging")]
		[WebSysDescription("ObjectDataSource_StartRowIndexParameterName")]
		public string StartRowIndexParameterName
		{
			get
			{
				return this.GetView().StartRowIndexParameterName;
			}
			set
			{
				this.GetView().StartRowIndexParameterName = value;
			}
		}

		// Token: 0x170010C7 RID: 4295
		// (get) Token: 0x0600396A RID: 14698 RVA: 0x000BA72E File Offset: 0x000B892E
		// (set) Token: 0x0600396B RID: 14699 RVA: 0x000BA73B File Offset: 0x000B893B
		[DefaultValue("")]
		[WebCategory("Data")]
		[WebSysDescription("ObjectDataSource_TypeName")]
		public string TypeName
		{
			get
			{
				return this.GetView().TypeName;
			}
			set
			{
				this.GetView().TypeName = value;
			}
		}

		// Token: 0x170010C8 RID: 4296
		// (get) Token: 0x0600396C RID: 14700 RVA: 0x000BA749 File Offset: 0x000B8949
		// (set) Token: 0x0600396D RID: 14701 RVA: 0x000BA756 File Offset: 0x000B8956
		[DefaultValue("")]
		[WebCategory("Data")]
		[WebSysDescription("ObjectDataSource_UpdateMethod")]
		public string UpdateMethod
		{
			get
			{
				return this.GetView().UpdateMethod;
			}
			set
			{
				this.GetView().UpdateMethod = value;
			}
		}

		// Token: 0x170010C9 RID: 4297
		// (get) Token: 0x0600396E RID: 14702 RVA: 0x000BA764 File Offset: 0x000B8964
		[DefaultValue(null)]
		[Editor("System.Web.UI.Design.WebControls.ParameterCollectionEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[MergableProperty(false)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[WebCategory("Data")]
		[WebSysDescription("ObjectDataSource_UpdateParameters")]
		public ParameterCollection UpdateParameters
		{
			get
			{
				return this.GetView().UpdateParameters;
			}
		}

		// Token: 0x170010CA RID: 4298
		// (get) Token: 0x0600396F RID: 14703 RVA: 0x000BA771 File Offset: 0x000B8971
		// (set) Token: 0x06003970 RID: 14704 RVA: 0x000BA77E File Offset: 0x000B897E
		[DefaultValue(ParsingCulture.Invariant)]
		[WebCategory("Behavior")]
		[WebSysDescription("ObjectDataSource_ParsingCulture")]
		public ParsingCulture ParsingCulture
		{
			get
			{
				return this.GetView().ParsingCulture;
			}
			set
			{
				this.GetView().ParsingCulture = value;
			}
		}

		// Token: 0x140000C1 RID: 193
		// (add) Token: 0x06003971 RID: 14705 RVA: 0x000BA78C File Offset: 0x000B898C
		// (remove) Token: 0x06003972 RID: 14706 RVA: 0x000BA79A File Offset: 0x000B899A
		[WebCategory("Data")]
		[WebSysDescription("DataSource_Deleted")]
		public event ObjectDataSourceStatusEventHandler Deleted
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

		// Token: 0x140000C2 RID: 194
		// (add) Token: 0x06003973 RID: 14707 RVA: 0x000BA7A8 File Offset: 0x000B89A8
		// (remove) Token: 0x06003974 RID: 14708 RVA: 0x000BA7B6 File Offset: 0x000B89B6
		[WebCategory("Data")]
		[WebSysDescription("DataSource_Deleting")]
		public event ObjectDataSourceMethodEventHandler Deleting
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

		// Token: 0x140000C3 RID: 195
		// (add) Token: 0x06003975 RID: 14709 RVA: 0x000BA7C4 File Offset: 0x000B89C4
		// (remove) Token: 0x06003976 RID: 14710 RVA: 0x000BA7D2 File Offset: 0x000B89D2
		[WebCategory("Data")]
		[WebSysDescription("DataSource_Filtering")]
		public event ObjectDataSourceFilteringEventHandler Filtering
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

		// Token: 0x140000C4 RID: 196
		// (add) Token: 0x06003977 RID: 14711 RVA: 0x000BA7E0 File Offset: 0x000B89E0
		// (remove) Token: 0x06003978 RID: 14712 RVA: 0x000BA7EE File Offset: 0x000B89EE
		[WebCategory("Data")]
		[WebSysDescription("DataSource_Inserted")]
		public event ObjectDataSourceStatusEventHandler Inserted
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

		// Token: 0x140000C5 RID: 197
		// (add) Token: 0x06003979 RID: 14713 RVA: 0x000BA7FC File Offset: 0x000B89FC
		// (remove) Token: 0x0600397A RID: 14714 RVA: 0x000BA80A File Offset: 0x000B8A0A
		[WebCategory("Data")]
		[WebSysDescription("DataSource_Inserting")]
		public event ObjectDataSourceMethodEventHandler Inserting
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

		// Token: 0x140000C6 RID: 198
		// (add) Token: 0x0600397B RID: 14715 RVA: 0x000BA818 File Offset: 0x000B8A18
		// (remove) Token: 0x0600397C RID: 14716 RVA: 0x000BA826 File Offset: 0x000B8A26
		[WebCategory("Data")]
		[WebSysDescription("ObjectDataSource_ObjectCreated")]
		public event ObjectDataSourceObjectEventHandler ObjectCreated
		{
			add
			{
				this.GetView().ObjectCreated += value;
			}
			remove
			{
				this.GetView().ObjectCreated -= value;
			}
		}

		// Token: 0x140000C7 RID: 199
		// (add) Token: 0x0600397D RID: 14717 RVA: 0x000BA834 File Offset: 0x000B8A34
		// (remove) Token: 0x0600397E RID: 14718 RVA: 0x000BA842 File Offset: 0x000B8A42
		[WebCategory("Data")]
		[WebSysDescription("ObjectDataSource_ObjectCreating")]
		public event ObjectDataSourceObjectEventHandler ObjectCreating
		{
			add
			{
				this.GetView().ObjectCreating += value;
			}
			remove
			{
				this.GetView().ObjectCreating -= value;
			}
		}

		// Token: 0x140000C8 RID: 200
		// (add) Token: 0x0600397F RID: 14719 RVA: 0x000BA850 File Offset: 0x000B8A50
		// (remove) Token: 0x06003980 RID: 14720 RVA: 0x000BA85E File Offset: 0x000B8A5E
		[WebCategory("Data")]
		[WebSysDescription("ObjectDataSource_ObjectDisposing")]
		public event ObjectDataSourceDisposingEventHandler ObjectDisposing
		{
			add
			{
				this.GetView().ObjectDisposing += value;
			}
			remove
			{
				this.GetView().ObjectDisposing -= value;
			}
		}

		// Token: 0x140000C9 RID: 201
		// (add) Token: 0x06003981 RID: 14721 RVA: 0x000BA86C File Offset: 0x000B8A6C
		// (remove) Token: 0x06003982 RID: 14722 RVA: 0x000BA87A File Offset: 0x000B8A7A
		[WebCategory("Data")]
		[WebSysDescription("ObjectDataSource_Selected")]
		public event ObjectDataSourceStatusEventHandler Selected
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

		// Token: 0x140000CA RID: 202
		// (add) Token: 0x06003983 RID: 14723 RVA: 0x000BA888 File Offset: 0x000B8A88
		// (remove) Token: 0x06003984 RID: 14724 RVA: 0x000BA896 File Offset: 0x000B8A96
		[WebCategory("Data")]
		[WebSysDescription("ObjectDataSource_Selecting")]
		public event ObjectDataSourceSelectingEventHandler Selecting
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

		// Token: 0x140000CB RID: 203
		// (add) Token: 0x06003985 RID: 14725 RVA: 0x000BA8A4 File Offset: 0x000B8AA4
		// (remove) Token: 0x06003986 RID: 14726 RVA: 0x000BA8B2 File Offset: 0x000B8AB2
		[WebCategory("Data")]
		[WebSysDescription("DataSource_Updated")]
		public event ObjectDataSourceStatusEventHandler Updated
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

		// Token: 0x140000CC RID: 204
		// (add) Token: 0x06003987 RID: 14727 RVA: 0x000BA8C0 File Offset: 0x000B8AC0
		// (remove) Token: 0x06003988 RID: 14728 RVA: 0x000BA8CE File Offset: 0x000B8ACE
		[WebCategory("Data")]
		[WebSysDescription("DataSource_Updating")]
		public event ObjectDataSourceMethodEventHandler Updating
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

		// Token: 0x06003989 RID: 14729 RVA: 0x000BA8DC File Offset: 0x000B8ADC
		internal string CreateCacheKey(int startRowIndex, int maximumRows)
		{
			StringBuilder stringBuilder = this.CreateRawCacheKey();
			stringBuilder.Append(':');
			stringBuilder.Append(startRowIndex.ToString(CultureInfo.InvariantCulture));
			stringBuilder.Append(':');
			stringBuilder.Append(maximumRows.ToString(CultureInfo.InvariantCulture));
			return stringBuilder.ToString();
		}

		// Token: 0x0600398A RID: 14730 RVA: 0x000BA92E File Offset: 0x000B8B2E
		internal string CreateMasterCacheKey()
		{
			return this.CreateRawCacheKey().ToString();
		}

		// Token: 0x0600398B RID: 14731 RVA: 0x000BA93C File Offset: 0x000B8B3C
		private StringBuilder CreateRawCacheKey()
		{
			StringBuilder stringBuilder = new StringBuilder("u", 1024);
			stringBuilder.Append(base.GetType().GetHashCode().ToString(CultureInfo.InvariantCulture));
			stringBuilder.Append(":");
			stringBuilder.Append(this.CacheDuration.ToString(CultureInfo.InvariantCulture));
			stringBuilder.Append(':');
			stringBuilder.Append(((int)this.CacheExpirationPolicy).ToString(CultureInfo.InvariantCulture));
			stringBuilder.Append(":");
			stringBuilder.Append(this.SqlCacheDependency);
			stringBuilder.Append(":");
			stringBuilder.Append(this.TypeName);
			stringBuilder.Append(":");
			stringBuilder.Append(this.SelectMethod);
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

		// Token: 0x0600398C RID: 14732 RVA: 0x000BAB08 File Offset: 0x000B8D08
		public int Delete()
		{
			return this.GetView().Delete(null, null);
		}

		// Token: 0x0600398D RID: 14733 RVA: 0x000BAB17 File Offset: 0x000B8D17
		private ObjectDataSourceView GetView()
		{
			if (this._view == null)
			{
				this._view = new ObjectDataSourceView(this, "DefaultView", this.Context);
				if (base.IsTrackingViewState)
				{
					((IStateManager)this._view).TrackViewState();
				}
			}
			return this._view;
		}

		// Token: 0x0600398E RID: 14734 RVA: 0x000BAB54 File Offset: 0x000B8D54
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

		// Token: 0x0600398F RID: 14735 RVA: 0x000BABAC File Offset: 0x000B8DAC
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

		// Token: 0x06003990 RID: 14736 RVA: 0x000BABD0 File Offset: 0x000B8DD0
		public int Insert()
		{
			return this.GetView().Insert(null);
		}

		// Token: 0x06003991 RID: 14737 RVA: 0x000BABE0 File Offset: 0x000B8DE0
		internal void InvalidateCacheEntry()
		{
			string key = this.CreateMasterCacheKey();
			this.Cache.Invalidate(key);
		}

		// Token: 0x06003992 RID: 14738 RVA: 0x000BAC00 File Offset: 0x000B8E00
		private void LoadCompleteEventHandler(object sender, EventArgs e)
		{
			this.SelectParameters.UpdateValues(this.Context, this);
			this.FilterParameters.UpdateValues(this.Context, this);
		}

		// Token: 0x06003993 RID: 14739 RVA: 0x000BAC28 File Offset: 0x000B8E28
		internal object LoadDataFromCache(int startRowIndex, int maximumRows)
		{
			string key = this.CreateCacheKey(startRowIndex, maximumRows);
			return this.Cache.LoadDataFromCache(key);
		}

		// Token: 0x06003994 RID: 14740 RVA: 0x000BAC4C File Offset: 0x000B8E4C
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

		// Token: 0x06003995 RID: 14741 RVA: 0x000BAC80 File Offset: 0x000B8E80
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

		// Token: 0x06003996 RID: 14742 RVA: 0x000BACC4 File Offset: 0x000B8EC4
		protected internal override void OnInit(EventArgs e)
		{
			base.OnInit(e);
			if (this.Page != null)
			{
				this.Page.LoadComplete += this.LoadCompleteEventHandler;
			}
		}

		// Token: 0x06003997 RID: 14743 RVA: 0x000BACEC File Offset: 0x000B8EEC
		internal void SaveDataToCache(int startRowIndex, int maximumRows, object data)
		{
			string key = this.CreateCacheKey(startRowIndex, maximumRows);
			string text = this.CreateMasterCacheKey();
			if (this.Cache.LoadDataFromCache(text) == null)
			{
				this.Cache.SaveDataToCache(text, -1);
			}
			CacheDependency dependency = new CacheDependency(0, new string[0], new string[]
			{
				text
			});
			this.Cache.SaveDataToCache(key, data, dependency);
		}

		// Token: 0x06003998 RID: 14744 RVA: 0x000BAD50 File Offset: 0x000B8F50
		internal void SaveTotalRowCountToCache(int totalRowCount)
		{
			string key = this.CreateMasterCacheKey();
			this.Cache.SaveDataToCache(key, totalRowCount);
		}

		// Token: 0x06003999 RID: 14745 RVA: 0x000BAD78 File Offset: 0x000B8F78
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

		// Token: 0x0600399A RID: 14746 RVA: 0x000BADC3 File Offset: 0x000B8FC3
		public IEnumerable Select()
		{
			return this.GetView().Select(DataSourceSelectArguments.Empty);
		}

		// Token: 0x0600399B RID: 14747 RVA: 0x000BADD5 File Offset: 0x000B8FD5
		protected override void TrackViewState()
		{
			base.TrackViewState();
			if (this._view != null)
			{
				((IStateManager)this._view).TrackViewState();
			}
		}

		// Token: 0x0600399C RID: 14748 RVA: 0x000BADF0 File Offset: 0x000B8FF0
		public int Update()
		{
			return this.GetView().Update(null, null, null);
		}

		// Token: 0x040022B6 RID: 8886
		private const string DefaultViewName = "DefaultView";

		// Token: 0x040022B7 RID: 8887
		private SqlDataSourceCache _cache;

		// Token: 0x040022B8 RID: 8888
		private ObjectDataSourceView _view;

		// Token: 0x040022B9 RID: 8889
		private ICollection _viewNames;
	}
}
