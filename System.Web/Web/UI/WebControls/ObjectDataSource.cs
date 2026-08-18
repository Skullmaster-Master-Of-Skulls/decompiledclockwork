using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Globalization;
using System.Security.Permissions;
using System.Text;
using System.Web.Caching;

namespace System.Web.UI.WebControls
{
	// Token: 0x020005F2 RID: 1522
	[WebSysDescription("ObjectDataSource_Description")]
	[WebSysDisplayName("ObjectDataSource_DisplayName")]
	[DefaultEvent("Selecting")]
	[DefaultProperty("TypeName")]
	[Designer("System.Web.UI.Design.WebControls.ObjectDataSourceDesigner, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[ParseChildren(true)]
	[PersistChildren(false)]
	[ToolboxBitmap(typeof(ObjectDataSource))]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class ObjectDataSource : DataSourceControl
	{
		// Token: 0x06004B34 RID: 19252 RVA: 0x00132E9E File Offset: 0x00131E9E
		public ObjectDataSource()
		{
		}

		// Token: 0x06004B35 RID: 19253 RVA: 0x00132EA6 File Offset: 0x00131EA6
		public ObjectDataSource(string typeName, string selectMethod)
		{
			this.TypeName = typeName;
			this.SelectMethod = selectMethod;
		}

		// Token: 0x170012D4 RID: 4820
		// (get) Token: 0x06004B36 RID: 19254 RVA: 0x00132EBC File Offset: 0x00131EBC
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

		// Token: 0x170012D5 RID: 4821
		// (get) Token: 0x06004B37 RID: 19255 RVA: 0x00132ED7 File Offset: 0x00131ED7
		// (set) Token: 0x06004B38 RID: 19256 RVA: 0x00132EE4 File Offset: 0x00131EE4
		[DefaultValue(0)]
		[WebSysDescription("DataSourceCache_Duration")]
		[TypeConverter(typeof(DataSourceCacheDurationConverter))]
		[WebCategory("Cache")]
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

		// Token: 0x170012D6 RID: 4822
		// (get) Token: 0x06004B39 RID: 19257 RVA: 0x00132EF2 File Offset: 0x00131EF2
		// (set) Token: 0x06004B3A RID: 19258 RVA: 0x00132EFF File Offset: 0x00131EFF
		[WebCategory("Cache")]
		[DefaultValue(DataSourceCacheExpiry.Absolute)]
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

		// Token: 0x170012D7 RID: 4823
		// (get) Token: 0x06004B3B RID: 19259 RVA: 0x00132F0D File Offset: 0x00131F0D
		// (set) Token: 0x06004B3C RID: 19260 RVA: 0x00132F1A File Offset: 0x00131F1A
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

		// Token: 0x170012D8 RID: 4824
		// (get) Token: 0x06004B3D RID: 19261 RVA: 0x00132F28 File Offset: 0x00131F28
		// (set) Token: 0x06004B3E RID: 19262 RVA: 0x00132F35 File Offset: 0x00131F35
		[DefaultValue(ConflictOptions.OverwriteChanges)]
		[WebSysDescription("ObjectDataSource_ConflictDetection")]
		[WebCategory("Data")]
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

		// Token: 0x170012D9 RID: 4825
		// (get) Token: 0x06004B3F RID: 19263 RVA: 0x00132F43 File Offset: 0x00131F43
		// (set) Token: 0x06004B40 RID: 19264 RVA: 0x00132F50 File Offset: 0x00131F50
		[DefaultValue(false)]
		[WebSysDescription("ObjectDataSource_ConvertNullToDBNull")]
		[WebCategory("Data")]
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

		// Token: 0x170012DA RID: 4826
		// (get) Token: 0x06004B41 RID: 19265 RVA: 0x00132F5E File Offset: 0x00131F5E
		// (set) Token: 0x06004B42 RID: 19266 RVA: 0x00132F6B File Offset: 0x00131F6B
		[WebSysDescription("ObjectDataSource_DataObjectTypeName")]
		[DefaultValue("")]
		[WebCategory("Data")]
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

		// Token: 0x170012DB RID: 4827
		// (get) Token: 0x06004B43 RID: 19267 RVA: 0x00132F79 File Offset: 0x00131F79
		// (set) Token: 0x06004B44 RID: 19268 RVA: 0x00132F86 File Offset: 0x00131F86
		[DefaultValue("")]
		[WebSysDescription("ObjectDataSource_DeleteMethod")]
		[WebCategory("Data")]
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

		// Token: 0x170012DC RID: 4828
		// (get) Token: 0x06004B45 RID: 19269 RVA: 0x00132F94 File Offset: 0x00131F94
		[MergableProperty(false)]
		[DefaultValue(null)]
		[Editor("System.Web.UI.Design.WebControls.ParameterCollectionEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[WebCategory("Data")]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[WebSysDescription("ObjectDataSource_DeleteParameters")]
		public ParameterCollection DeleteParameters
		{
			get
			{
				return this.GetView().DeleteParameters;
			}
		}

		// Token: 0x170012DD RID: 4829
		// (get) Token: 0x06004B46 RID: 19270 RVA: 0x00132FA1 File Offset: 0x00131FA1
		// (set) Token: 0x06004B47 RID: 19271 RVA: 0x00132FAE File Offset: 0x00131FAE
		[WebSysDescription("DataSourceCache_Enabled")]
		[DefaultValue(false)]
		[WebCategory("Cache")]
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

		// Token: 0x170012DE RID: 4830
		// (get) Token: 0x06004B48 RID: 19272 RVA: 0x00132FBC File Offset: 0x00131FBC
		// (set) Token: 0x06004B49 RID: 19273 RVA: 0x00132FC9 File Offset: 0x00131FC9
		[WebCategory("Paging")]
		[WebSysDescription("ObjectDataSource_EnablePaging")]
		[DefaultValue(false)]
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

		// Token: 0x170012DF RID: 4831
		// (get) Token: 0x06004B4A RID: 19274 RVA: 0x00132FD7 File Offset: 0x00131FD7
		// (set) Token: 0x06004B4B RID: 19275 RVA: 0x00132FE4 File Offset: 0x00131FE4
		[WebSysDescription("ObjectDataSource_FilterExpression")]
		[DefaultValue("")]
		[WebCategory("Data")]
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

		// Token: 0x170012E0 RID: 4832
		// (get) Token: 0x06004B4C RID: 19276 RVA: 0x00132FF2 File Offset: 0x00131FF2
		[Editor("System.Web.UI.Design.WebControls.ParameterCollectionEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[WebCategory("Data")]
		[WebSysDescription("ObjectDataSource_FilterParameters")]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DefaultValue(null)]
		[MergableProperty(false)]
		public ParameterCollection FilterParameters
		{
			get
			{
				return this.GetView().FilterParameters;
			}
		}

		// Token: 0x170012E1 RID: 4833
		// (get) Token: 0x06004B4D RID: 19277 RVA: 0x00132FFF File Offset: 0x00131FFF
		// (set) Token: 0x06004B4E RID: 19278 RVA: 0x0013300C File Offset: 0x0013200C
		[WebCategory("Data")]
		[WebSysDescription("ObjectDataSource_InsertMethod")]
		[DefaultValue("")]
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

		// Token: 0x170012E2 RID: 4834
		// (get) Token: 0x06004B4F RID: 19279 RVA: 0x0013301A File Offset: 0x0013201A
		[DefaultValue(null)]
		[MergableProperty(false)]
		[WebSysDescription("ObjectDataSource_InsertParameters")]
		[Editor("System.Web.UI.Design.WebControls.ParameterCollectionEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[WebCategory("Data")]
		public ParameterCollection InsertParameters
		{
			get
			{
				return this.GetView().InsertParameters;
			}
		}

		// Token: 0x170012E3 RID: 4835
		// (get) Token: 0x06004B50 RID: 19280 RVA: 0x00133027 File Offset: 0x00132027
		// (set) Token: 0x06004B51 RID: 19281 RVA: 0x00133034 File Offset: 0x00132034
		[WebCategory("Paging")]
		[WebSysDescription("ObjectDataSource_MaximumRowsParameterName")]
		[DefaultValue("maximumRows")]
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

		// Token: 0x170012E4 RID: 4836
		// (get) Token: 0x06004B52 RID: 19282 RVA: 0x00133042 File Offset: 0x00132042
		// (set) Token: 0x06004B53 RID: 19283 RVA: 0x0013304F File Offset: 0x0013204F
		[WebCategory("Data")]
		[WebSysDescription("DataSource_OldValuesParameterFormatString")]
		[DefaultValue("{0}")]
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

		// Token: 0x170012E5 RID: 4837
		// (get) Token: 0x06004B54 RID: 19284 RVA: 0x0013305D File Offset: 0x0013205D
		// (set) Token: 0x06004B55 RID: 19285 RVA: 0x0013306A File Offset: 0x0013206A
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

		// Token: 0x170012E6 RID: 4838
		// (get) Token: 0x06004B56 RID: 19286 RVA: 0x00133078 File Offset: 0x00132078
		// (set) Token: 0x06004B57 RID: 19287 RVA: 0x00133085 File Offset: 0x00132085
		[WebSysDescription("ObjectDataSource_SelectMethod")]
		[WebCategory("Data")]
		[DefaultValue("")]
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

		// Token: 0x170012E7 RID: 4839
		// (get) Token: 0x06004B58 RID: 19288 RVA: 0x00133093 File Offset: 0x00132093
		[DefaultValue(null)]
		[Editor("System.Web.UI.Design.WebControls.ParameterCollectionEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[MergableProperty(false)]
		[WebSysDescription("ObjectDataSource_SelectParameters")]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[WebCategory("Data")]
		public ParameterCollection SelectParameters
		{
			get
			{
				return this.GetView().SelectParameters;
			}
		}

		// Token: 0x170012E8 RID: 4840
		// (get) Token: 0x06004B59 RID: 19289 RVA: 0x001330A0 File Offset: 0x001320A0
		// (set) Token: 0x06004B5A RID: 19290 RVA: 0x001330AD File Offset: 0x001320AD
		[WebSysDescription("ObjectDataSource_SortParameterName")]
		[DefaultValue("")]
		[WebCategory("Data")]
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

		// Token: 0x170012E9 RID: 4841
		// (get) Token: 0x06004B5B RID: 19291 RVA: 0x001330BB File Offset: 0x001320BB
		// (set) Token: 0x06004B5C RID: 19292 RVA: 0x001330C8 File Offset: 0x001320C8
		[WebSysDescription("SqlDataSourceCache_SqlCacheDependency")]
		[DefaultValue("")]
		[WebCategory("Cache")]
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

		// Token: 0x170012EA RID: 4842
		// (get) Token: 0x06004B5D RID: 19293 RVA: 0x001330D6 File Offset: 0x001320D6
		// (set) Token: 0x06004B5E RID: 19294 RVA: 0x001330E3 File Offset: 0x001320E3
		[WebCategory("Paging")]
		[DefaultValue("startRowIndex")]
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

		// Token: 0x170012EB RID: 4843
		// (get) Token: 0x06004B5F RID: 19295 RVA: 0x001330F1 File Offset: 0x001320F1
		// (set) Token: 0x06004B60 RID: 19296 RVA: 0x001330FE File Offset: 0x001320FE
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

		// Token: 0x170012EC RID: 4844
		// (get) Token: 0x06004B61 RID: 19297 RVA: 0x0013310C File Offset: 0x0013210C
		// (set) Token: 0x06004B62 RID: 19298 RVA: 0x00133119 File Offset: 0x00132119
		[WebSysDescription("ObjectDataSource_UpdateMethod")]
		[WebCategory("Data")]
		[DefaultValue("")]
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

		// Token: 0x170012ED RID: 4845
		// (get) Token: 0x06004B63 RID: 19299 RVA: 0x00133127 File Offset: 0x00132127
		[Editor("System.Web.UI.Design.WebControls.ParameterCollectionEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[WebSysDescription("ObjectDataSource_UpdateParameters")]
		[MergableProperty(false)]
		[DefaultValue(null)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[WebCategory("Data")]
		public ParameterCollection UpdateParameters
		{
			get
			{
				return this.GetView().UpdateParameters;
			}
		}

		// Token: 0x140000D9 RID: 217
		// (add) Token: 0x06004B64 RID: 19300 RVA: 0x00133134 File Offset: 0x00132134
		// (remove) Token: 0x06004B65 RID: 19301 RVA: 0x00133142 File Offset: 0x00132142
		[WebSysDescription("DataSource_Deleted")]
		[WebCategory("Data")]
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

		// Token: 0x140000DA RID: 218
		// (add) Token: 0x06004B66 RID: 19302 RVA: 0x00133150 File Offset: 0x00132150
		// (remove) Token: 0x06004B67 RID: 19303 RVA: 0x0013315E File Offset: 0x0013215E
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

		// Token: 0x140000DB RID: 219
		// (add) Token: 0x06004B68 RID: 19304 RVA: 0x0013316C File Offset: 0x0013216C
		// (remove) Token: 0x06004B69 RID: 19305 RVA: 0x0013317A File Offset: 0x0013217A
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

		// Token: 0x140000DC RID: 220
		// (add) Token: 0x06004B6A RID: 19306 RVA: 0x00133188 File Offset: 0x00132188
		// (remove) Token: 0x06004B6B RID: 19307 RVA: 0x00133196 File Offset: 0x00132196
		[WebSysDescription("DataSource_Inserted")]
		[WebCategory("Data")]
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

		// Token: 0x140000DD RID: 221
		// (add) Token: 0x06004B6C RID: 19308 RVA: 0x001331A4 File Offset: 0x001321A4
		// (remove) Token: 0x06004B6D RID: 19309 RVA: 0x001331B2 File Offset: 0x001321B2
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

		// Token: 0x140000DE RID: 222
		// (add) Token: 0x06004B6E RID: 19310 RVA: 0x001331C0 File Offset: 0x001321C0
		// (remove) Token: 0x06004B6F RID: 19311 RVA: 0x001331CE File Offset: 0x001321CE
		[WebSysDescription("ObjectDataSource_ObjectCreated")]
		[WebCategory("Data")]
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

		// Token: 0x140000DF RID: 223
		// (add) Token: 0x06004B70 RID: 19312 RVA: 0x001331DC File Offset: 0x001321DC
		// (remove) Token: 0x06004B71 RID: 19313 RVA: 0x001331EA File Offset: 0x001321EA
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

		// Token: 0x140000E0 RID: 224
		// (add) Token: 0x06004B72 RID: 19314 RVA: 0x001331F8 File Offset: 0x001321F8
		// (remove) Token: 0x06004B73 RID: 19315 RVA: 0x00133206 File Offset: 0x00132206
		[WebSysDescription("ObjectDataSource_ObjectDisposing")]
		[WebCategory("Data")]
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

		// Token: 0x140000E1 RID: 225
		// (add) Token: 0x06004B74 RID: 19316 RVA: 0x00133214 File Offset: 0x00132214
		// (remove) Token: 0x06004B75 RID: 19317 RVA: 0x00133222 File Offset: 0x00132222
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

		// Token: 0x140000E2 RID: 226
		// (add) Token: 0x06004B76 RID: 19318 RVA: 0x00133230 File Offset: 0x00132230
		// (remove) Token: 0x06004B77 RID: 19319 RVA: 0x0013323E File Offset: 0x0013223E
		[WebSysDescription("ObjectDataSource_Selecting")]
		[WebCategory("Data")]
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

		// Token: 0x140000E3 RID: 227
		// (add) Token: 0x06004B78 RID: 19320 RVA: 0x0013324C File Offset: 0x0013224C
		// (remove) Token: 0x06004B79 RID: 19321 RVA: 0x0013325A File Offset: 0x0013225A
		[WebSysDescription("DataSource_Updated")]
		[WebCategory("Data")]
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

		// Token: 0x140000E4 RID: 228
		// (add) Token: 0x06004B7A RID: 19322 RVA: 0x00133268 File Offset: 0x00132268
		// (remove) Token: 0x06004B7B RID: 19323 RVA: 0x00133276 File Offset: 0x00132276
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

		// Token: 0x06004B7C RID: 19324 RVA: 0x00133284 File Offset: 0x00132284
		internal string CreateCacheKey(int startRowIndex, int maximumRows)
		{
			StringBuilder stringBuilder = this.CreateRawCacheKey();
			stringBuilder.Append(':');
			stringBuilder.Append(startRowIndex.ToString(CultureInfo.InvariantCulture));
			stringBuilder.Append(':');
			stringBuilder.Append(maximumRows.ToString(CultureInfo.InvariantCulture));
			return stringBuilder.ToString();
		}

		// Token: 0x06004B7D RID: 19325 RVA: 0x001332D6 File Offset: 0x001322D6
		internal string CreateMasterCacheKey()
		{
			return this.CreateRawCacheKey().ToString();
		}

		// Token: 0x06004B7E RID: 19326 RVA: 0x001332E4 File Offset: 0x001322E4
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

		// Token: 0x06004B7F RID: 19327 RVA: 0x001334B4 File Offset: 0x001324B4
		public int Delete()
		{
			return this.GetView().Delete(null, null);
		}

		// Token: 0x06004B80 RID: 19328 RVA: 0x001334C3 File Offset: 0x001324C3
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

		// Token: 0x06004B81 RID: 19329 RVA: 0x00133500 File Offset: 0x00132500
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

		// Token: 0x06004B82 RID: 19330 RVA: 0x0013355C File Offset: 0x0013255C
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

		// Token: 0x06004B83 RID: 19331 RVA: 0x0013358D File Offset: 0x0013258D
		public int Insert()
		{
			return this.GetView().Insert(null);
		}

		// Token: 0x06004B84 RID: 19332 RVA: 0x0013359C File Offset: 0x0013259C
		internal void InvalidateCacheEntry()
		{
			string key = this.CreateMasterCacheKey();
			this.Cache.Invalidate(key);
		}

		// Token: 0x06004B85 RID: 19333 RVA: 0x001335BC File Offset: 0x001325BC
		private void LoadCompleteEventHandler(object sender, EventArgs e)
		{
			this.SelectParameters.UpdateValues(this.Context, this);
			this.FilterParameters.UpdateValues(this.Context, this);
		}

		// Token: 0x06004B86 RID: 19334 RVA: 0x001335E4 File Offset: 0x001325E4
		internal object LoadDataFromCache(int startRowIndex, int maximumRows)
		{
			string key = this.CreateCacheKey(startRowIndex, maximumRows);
			return this.Cache.LoadDataFromCache(key);
		}

		// Token: 0x06004B87 RID: 19335 RVA: 0x00133608 File Offset: 0x00132608
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

		// Token: 0x06004B88 RID: 19336 RVA: 0x0013363C File Offset: 0x0013263C
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

		// Token: 0x06004B89 RID: 19337 RVA: 0x00133680 File Offset: 0x00132680
		protected internal override void OnInit(EventArgs e)
		{
			base.OnInit(e);
			if (this.Page != null)
			{
				this.Page.LoadComplete += this.LoadCompleteEventHandler;
			}
		}

		// Token: 0x06004B8A RID: 19338 RVA: 0x001336A8 File Offset: 0x001326A8
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

		// Token: 0x06004B8B RID: 19339 RVA: 0x0013370C File Offset: 0x0013270C
		internal void SaveTotalRowCountToCache(int totalRowCount)
		{
			string key = this.CreateMasterCacheKey();
			this.Cache.SaveDataToCache(key, totalRowCount);
		}

		// Token: 0x06004B8C RID: 19340 RVA: 0x00133734 File Offset: 0x00132734
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

		// Token: 0x06004B8D RID: 19341 RVA: 0x0013377F File Offset: 0x0013277F
		public IEnumerable Select()
		{
			return this.GetView().Select(DataSourceSelectArguments.Empty);
		}

		// Token: 0x06004B8E RID: 19342 RVA: 0x00133791 File Offset: 0x00132791
		protected override void TrackViewState()
		{
			base.TrackViewState();
			if (this._view != null)
			{
				((IStateManager)this._view).TrackViewState();
			}
		}

		// Token: 0x06004B8F RID: 19343 RVA: 0x001337AC File Offset: 0x001327AC
		public int Update()
		{
			return this.GetView().Update(null, null, null);
		}

		// Token: 0x04002BA7 RID: 11175
		private const string DefaultViewName = "DefaultView";

		// Token: 0x04002BA8 RID: 11176
		private SqlDataSourceCache _cache;

		// Token: 0x04002BA9 RID: 11177
		private ObjectDataSourceView _view;

		// Token: 0x04002BAA RID: 11178
		private ICollection _viewNames;
	}
}
