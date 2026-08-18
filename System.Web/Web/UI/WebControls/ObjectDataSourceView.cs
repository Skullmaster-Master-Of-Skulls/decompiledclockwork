using System;
using System.Collections;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Globalization;
using System.Reflection;
using System.Security.Permissions;
using System.Web.Compilation;

namespace System.Web.UI.WebControls
{
	// Token: 0x020005FF RID: 1535
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class ObjectDataSourceView : DataSourceView, IStateManager
	{
		// Token: 0x06004BBD RID: 19389 RVA: 0x001338B1 File Offset: 0x001328B1
		public ObjectDataSourceView(ObjectDataSource owner, string name, HttpContext context) : base(owner, name)
		{
			this._owner = owner;
			this._context = context;
		}

		// Token: 0x170012F9 RID: 4857
		// (get) Token: 0x06004BBE RID: 19390 RVA: 0x001338C9 File Offset: 0x001328C9
		public override bool CanDelete
		{
			get
			{
				return this.DeleteMethod.Length != 0;
			}
		}

		// Token: 0x170012FA RID: 4858
		// (get) Token: 0x06004BBF RID: 19391 RVA: 0x001338DC File Offset: 0x001328DC
		public override bool CanInsert
		{
			get
			{
				return this.InsertMethod.Length != 0;
			}
		}

		// Token: 0x170012FB RID: 4859
		// (get) Token: 0x06004BC0 RID: 19392 RVA: 0x001338EF File Offset: 0x001328EF
		public override bool CanPage
		{
			get
			{
				return this.EnablePaging;
			}
		}

		// Token: 0x170012FC RID: 4860
		// (get) Token: 0x06004BC1 RID: 19393 RVA: 0x001338F7 File Offset: 0x001328F7
		public override bool CanRetrieveTotalRowCount
		{
			get
			{
				return this.SelectCountMethod.Length > 0 || !this.EnablePaging;
			}
		}

		// Token: 0x170012FD RID: 4861
		// (get) Token: 0x06004BC2 RID: 19394 RVA: 0x00133912 File Offset: 0x00132912
		public override bool CanSort
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170012FE RID: 4862
		// (get) Token: 0x06004BC3 RID: 19395 RVA: 0x00133915 File Offset: 0x00132915
		public override bool CanUpdate
		{
			get
			{
				return this.UpdateMethod.Length != 0;
			}
		}

		// Token: 0x170012FF RID: 4863
		// (get) Token: 0x06004BC4 RID: 19396 RVA: 0x00133928 File Offset: 0x00132928
		// (set) Token: 0x06004BC5 RID: 19397 RVA: 0x00133930 File Offset: 0x00132930
		public ConflictOptions ConflictDetection
		{
			get
			{
				return this._conflictDetection;
			}
			set
			{
				if (value < ConflictOptions.OverwriteChanges || value > ConflictOptions.CompareAllValues)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this._conflictDetection = value;
				this.OnDataSourceViewChanged(EventArgs.Empty);
			}
		}

		// Token: 0x17001300 RID: 4864
		// (get) Token: 0x06004BC6 RID: 19398 RVA: 0x00133957 File Offset: 0x00132957
		// (set) Token: 0x06004BC7 RID: 19399 RVA: 0x0013395F File Offset: 0x0013295F
		public bool ConvertNullToDBNull
		{
			get
			{
				return this._convertNullToDBNull;
			}
			set
			{
				this._convertNullToDBNull = value;
			}
		}

		// Token: 0x17001301 RID: 4865
		// (get) Token: 0x06004BC8 RID: 19400 RVA: 0x00133968 File Offset: 0x00132968
		// (set) Token: 0x06004BC9 RID: 19401 RVA: 0x0013397E File Offset: 0x0013297E
		public string DataObjectTypeName
		{
			get
			{
				if (this._dataObjectTypeName == null)
				{
					return string.Empty;
				}
				return this._dataObjectTypeName;
			}
			set
			{
				if (this.DataObjectTypeName != value)
				{
					this._dataObjectTypeName = value;
					this.OnDataSourceViewChanged(EventArgs.Empty);
				}
			}
		}

		// Token: 0x17001302 RID: 4866
		// (get) Token: 0x06004BCA RID: 19402 RVA: 0x001339A0 File Offset: 0x001329A0
		// (set) Token: 0x06004BCB RID: 19403 RVA: 0x001339B6 File Offset: 0x001329B6
		public string DeleteMethod
		{
			get
			{
				if (this._deleteMethod == null)
				{
					return string.Empty;
				}
				return this._deleteMethod;
			}
			set
			{
				this._deleteMethod = value;
			}
		}

		// Token: 0x17001303 RID: 4867
		// (get) Token: 0x06004BCC RID: 19404 RVA: 0x001339BF File Offset: 0x001329BF
		public ParameterCollection DeleteParameters
		{
			get
			{
				if (this._deleteParameters == null)
				{
					this._deleteParameters = new ParameterCollection();
				}
				return this._deleteParameters;
			}
		}

		// Token: 0x17001304 RID: 4868
		// (get) Token: 0x06004BCD RID: 19405 RVA: 0x001339DA File Offset: 0x001329DA
		// (set) Token: 0x06004BCE RID: 19406 RVA: 0x001339E2 File Offset: 0x001329E2
		public bool EnablePaging
		{
			get
			{
				return this._enablePaging;
			}
			set
			{
				if (this.EnablePaging != value)
				{
					this._enablePaging = value;
					this.OnDataSourceViewChanged(EventArgs.Empty);
				}
			}
		}

		// Token: 0x17001305 RID: 4869
		// (get) Token: 0x06004BCF RID: 19407 RVA: 0x001339FF File Offset: 0x001329FF
		// (set) Token: 0x06004BD0 RID: 19408 RVA: 0x00133A15 File Offset: 0x00132A15
		public string FilterExpression
		{
			get
			{
				if (this._filterExpression == null)
				{
					return string.Empty;
				}
				return this._filterExpression;
			}
			set
			{
				if (this.FilterExpression != value)
				{
					this._filterExpression = value;
					this.OnDataSourceViewChanged(EventArgs.Empty);
				}
			}
		}

		// Token: 0x17001306 RID: 4870
		// (get) Token: 0x06004BD1 RID: 19409 RVA: 0x00133A38 File Offset: 0x00132A38
		public ParameterCollection FilterParameters
		{
			get
			{
				if (this._filterParameters == null)
				{
					this._filterParameters = new ParameterCollection();
					this._filterParameters.ParametersChanged += this.SelectParametersChangedEventHandler;
					if (this._tracking)
					{
						((IStateManager)this._filterParameters).TrackViewState();
					}
				}
				return this._filterParameters;
			}
		}

		// Token: 0x17001307 RID: 4871
		// (get) Token: 0x06004BD2 RID: 19410 RVA: 0x00133A88 File Offset: 0x00132A88
		// (set) Token: 0x06004BD3 RID: 19411 RVA: 0x00133A9E File Offset: 0x00132A9E
		public string InsertMethod
		{
			get
			{
				if (this._insertMethod == null)
				{
					return string.Empty;
				}
				return this._insertMethod;
			}
			set
			{
				this._insertMethod = value;
			}
		}

		// Token: 0x17001308 RID: 4872
		// (get) Token: 0x06004BD4 RID: 19412 RVA: 0x00133AA7 File Offset: 0x00132AA7
		public ParameterCollection InsertParameters
		{
			get
			{
				if (this._insertParameters == null)
				{
					this._insertParameters = new ParameterCollection();
				}
				return this._insertParameters;
			}
		}

		// Token: 0x17001309 RID: 4873
		// (get) Token: 0x06004BD5 RID: 19413 RVA: 0x00133AC2 File Offset: 0x00132AC2
		protected bool IsTrackingViewState
		{
			get
			{
				return this._tracking;
			}
		}

		// Token: 0x1700130A RID: 4874
		// (get) Token: 0x06004BD6 RID: 19414 RVA: 0x00133ACA File Offset: 0x00132ACA
		// (set) Token: 0x06004BD7 RID: 19415 RVA: 0x00133AE0 File Offset: 0x00132AE0
		public string MaximumRowsParameterName
		{
			get
			{
				if (this._maximumRowsParameterName == null)
				{
					return "maximumRows";
				}
				return this._maximumRowsParameterName;
			}
			set
			{
				if (this.MaximumRowsParameterName != value)
				{
					this._maximumRowsParameterName = value;
					this.OnDataSourceViewChanged(EventArgs.Empty);
				}
			}
		}

		// Token: 0x1700130B RID: 4875
		// (get) Token: 0x06004BD8 RID: 19416 RVA: 0x00133B02 File Offset: 0x00132B02
		// (set) Token: 0x06004BD9 RID: 19417 RVA: 0x00133B18 File Offset: 0x00132B18
		[WebCategory("Data")]
		[DefaultValue("{0}")]
		[WebSysDescription("DataSource_OldValuesParameterFormatString")]
		public string OldValuesParameterFormatString
		{
			get
			{
				if (this._oldValuesParameterFormatString == null)
				{
					return "{0}";
				}
				return this._oldValuesParameterFormatString;
			}
			set
			{
				this._oldValuesParameterFormatString = value;
				this.OnDataSourceViewChanged(EventArgs.Empty);
			}
		}

		// Token: 0x1700130C RID: 4876
		// (get) Token: 0x06004BDA RID: 19418 RVA: 0x00133B2C File Offset: 0x00132B2C
		// (set) Token: 0x06004BDB RID: 19419 RVA: 0x00133B42 File Offset: 0x00132B42
		public string SelectCountMethod
		{
			get
			{
				if (this._selectCountMethod == null)
				{
					return string.Empty;
				}
				return this._selectCountMethod;
			}
			set
			{
				if (this.SelectCountMethod != value)
				{
					this._selectCountMethod = value;
					this.OnDataSourceViewChanged(EventArgs.Empty);
				}
			}
		}

		// Token: 0x1700130D RID: 4877
		// (get) Token: 0x06004BDC RID: 19420 RVA: 0x00133B64 File Offset: 0x00132B64
		// (set) Token: 0x06004BDD RID: 19421 RVA: 0x00133B7A File Offset: 0x00132B7A
		public string SelectMethod
		{
			get
			{
				if (this._selectMethod == null)
				{
					return string.Empty;
				}
				return this._selectMethod;
			}
			set
			{
				if (this.SelectMethod != value)
				{
					this._selectMethod = value;
					this.OnDataSourceViewChanged(EventArgs.Empty);
				}
			}
		}

		// Token: 0x1700130E RID: 4878
		// (get) Token: 0x06004BDE RID: 19422 RVA: 0x00133B9C File Offset: 0x00132B9C
		public ParameterCollection SelectParameters
		{
			get
			{
				if (this._selectParameters == null)
				{
					this._selectParameters = new ParameterCollection();
					this._selectParameters.ParametersChanged += this.SelectParametersChangedEventHandler;
					if (this._tracking)
					{
						((IStateManager)this._selectParameters).TrackViewState();
					}
				}
				return this._selectParameters;
			}
		}

		// Token: 0x1700130F RID: 4879
		// (get) Token: 0x06004BDF RID: 19423 RVA: 0x00133BEC File Offset: 0x00132BEC
		// (set) Token: 0x06004BE0 RID: 19424 RVA: 0x00133C02 File Offset: 0x00132C02
		public string SortParameterName
		{
			get
			{
				if (this._sortParameterName == null)
				{
					return string.Empty;
				}
				return this._sortParameterName;
			}
			set
			{
				if (this.SortParameterName != value)
				{
					this._sortParameterName = value;
					this.OnDataSourceViewChanged(EventArgs.Empty);
				}
			}
		}

		// Token: 0x17001310 RID: 4880
		// (get) Token: 0x06004BE1 RID: 19425 RVA: 0x00133C24 File Offset: 0x00132C24
		// (set) Token: 0x06004BE2 RID: 19426 RVA: 0x00133C3A File Offset: 0x00132C3A
		public string StartRowIndexParameterName
		{
			get
			{
				if (this._startRowIndexParameterName == null)
				{
					return "startRowIndex";
				}
				return this._startRowIndexParameterName;
			}
			set
			{
				if (this.StartRowIndexParameterName != value)
				{
					this._startRowIndexParameterName = value;
					this.OnDataSourceViewChanged(EventArgs.Empty);
				}
			}
		}

		// Token: 0x17001311 RID: 4881
		// (get) Token: 0x06004BE3 RID: 19427 RVA: 0x00133C5C File Offset: 0x00132C5C
		// (set) Token: 0x06004BE4 RID: 19428 RVA: 0x00133C72 File Offset: 0x00132C72
		public string TypeName
		{
			get
			{
				if (this._typeName == null)
				{
					return string.Empty;
				}
				return this._typeName;
			}
			set
			{
				if (this.TypeName != value)
				{
					this._typeName = value;
					this.OnDataSourceViewChanged(EventArgs.Empty);
				}
			}
		}

		// Token: 0x17001312 RID: 4882
		// (get) Token: 0x06004BE5 RID: 19429 RVA: 0x00133C94 File Offset: 0x00132C94
		// (set) Token: 0x06004BE6 RID: 19430 RVA: 0x00133CAA File Offset: 0x00132CAA
		public string UpdateMethod
		{
			get
			{
				if (this._updateMethod == null)
				{
					return string.Empty;
				}
				return this._updateMethod;
			}
			set
			{
				this._updateMethod = value;
			}
		}

		// Token: 0x17001313 RID: 4883
		// (get) Token: 0x06004BE7 RID: 19431 RVA: 0x00133CB3 File Offset: 0x00132CB3
		public ParameterCollection UpdateParameters
		{
			get
			{
				if (this._updateParameters == null)
				{
					this._updateParameters = new ParameterCollection();
				}
				return this._updateParameters;
			}
		}

		// Token: 0x140000E5 RID: 229
		// (add) Token: 0x06004BE8 RID: 19432 RVA: 0x00133CCE File Offset: 0x00132CCE
		// (remove) Token: 0x06004BE9 RID: 19433 RVA: 0x00133CE1 File Offset: 0x00132CE1
		public event ObjectDataSourceStatusEventHandler Deleted
		{
			add
			{
				base.Events.AddHandler(ObjectDataSourceView.EventDeleted, value);
			}
			remove
			{
				base.Events.RemoveHandler(ObjectDataSourceView.EventDeleted, value);
			}
		}

		// Token: 0x140000E6 RID: 230
		// (add) Token: 0x06004BEA RID: 19434 RVA: 0x00133CF4 File Offset: 0x00132CF4
		// (remove) Token: 0x06004BEB RID: 19435 RVA: 0x00133D07 File Offset: 0x00132D07
		public event ObjectDataSourceMethodEventHandler Deleting
		{
			add
			{
				base.Events.AddHandler(ObjectDataSourceView.EventDeleting, value);
			}
			remove
			{
				base.Events.RemoveHandler(ObjectDataSourceView.EventDeleting, value);
			}
		}

		// Token: 0x140000E7 RID: 231
		// (add) Token: 0x06004BEC RID: 19436 RVA: 0x00133D1A File Offset: 0x00132D1A
		// (remove) Token: 0x06004BED RID: 19437 RVA: 0x00133D2D File Offset: 0x00132D2D
		public event ObjectDataSourceFilteringEventHandler Filtering
		{
			add
			{
				base.Events.AddHandler(ObjectDataSourceView.EventFiltering, value);
			}
			remove
			{
				base.Events.RemoveHandler(ObjectDataSourceView.EventFiltering, value);
			}
		}

		// Token: 0x140000E8 RID: 232
		// (add) Token: 0x06004BEE RID: 19438 RVA: 0x00133D40 File Offset: 0x00132D40
		// (remove) Token: 0x06004BEF RID: 19439 RVA: 0x00133D53 File Offset: 0x00132D53
		public event ObjectDataSourceStatusEventHandler Inserted
		{
			add
			{
				base.Events.AddHandler(ObjectDataSourceView.EventInserted, value);
			}
			remove
			{
				base.Events.RemoveHandler(ObjectDataSourceView.EventInserted, value);
			}
		}

		// Token: 0x140000E9 RID: 233
		// (add) Token: 0x06004BF0 RID: 19440 RVA: 0x00133D66 File Offset: 0x00132D66
		// (remove) Token: 0x06004BF1 RID: 19441 RVA: 0x00133D79 File Offset: 0x00132D79
		public event ObjectDataSourceMethodEventHandler Inserting
		{
			add
			{
				base.Events.AddHandler(ObjectDataSourceView.EventInserting, value);
			}
			remove
			{
				base.Events.RemoveHandler(ObjectDataSourceView.EventInserting, value);
			}
		}

		// Token: 0x140000EA RID: 234
		// (add) Token: 0x06004BF2 RID: 19442 RVA: 0x00133D8C File Offset: 0x00132D8C
		// (remove) Token: 0x06004BF3 RID: 19443 RVA: 0x00133D9F File Offset: 0x00132D9F
		public event ObjectDataSourceObjectEventHandler ObjectCreated
		{
			add
			{
				base.Events.AddHandler(ObjectDataSourceView.EventObjectCreated, value);
			}
			remove
			{
				base.Events.RemoveHandler(ObjectDataSourceView.EventObjectCreated, value);
			}
		}

		// Token: 0x140000EB RID: 235
		// (add) Token: 0x06004BF4 RID: 19444 RVA: 0x00133DB2 File Offset: 0x00132DB2
		// (remove) Token: 0x06004BF5 RID: 19445 RVA: 0x00133DC5 File Offset: 0x00132DC5
		public event ObjectDataSourceObjectEventHandler ObjectCreating
		{
			add
			{
				base.Events.AddHandler(ObjectDataSourceView.EventObjectCreating, value);
			}
			remove
			{
				base.Events.RemoveHandler(ObjectDataSourceView.EventObjectCreating, value);
			}
		}

		// Token: 0x140000EC RID: 236
		// (add) Token: 0x06004BF6 RID: 19446 RVA: 0x00133DD8 File Offset: 0x00132DD8
		// (remove) Token: 0x06004BF7 RID: 19447 RVA: 0x00133DEB File Offset: 0x00132DEB
		public event ObjectDataSourceDisposingEventHandler ObjectDisposing
		{
			add
			{
				base.Events.AddHandler(ObjectDataSourceView.EventObjectDisposing, value);
			}
			remove
			{
				base.Events.RemoveHandler(ObjectDataSourceView.EventObjectDisposing, value);
			}
		}

		// Token: 0x140000ED RID: 237
		// (add) Token: 0x06004BF8 RID: 19448 RVA: 0x00133DFE File Offset: 0x00132DFE
		// (remove) Token: 0x06004BF9 RID: 19449 RVA: 0x00133E11 File Offset: 0x00132E11
		public event ObjectDataSourceStatusEventHandler Selected
		{
			add
			{
				base.Events.AddHandler(ObjectDataSourceView.EventSelected, value);
			}
			remove
			{
				base.Events.RemoveHandler(ObjectDataSourceView.EventSelected, value);
			}
		}

		// Token: 0x140000EE RID: 238
		// (add) Token: 0x06004BFA RID: 19450 RVA: 0x00133E24 File Offset: 0x00132E24
		// (remove) Token: 0x06004BFB RID: 19451 RVA: 0x00133E37 File Offset: 0x00132E37
		public event ObjectDataSourceSelectingEventHandler Selecting
		{
			add
			{
				base.Events.AddHandler(ObjectDataSourceView.EventSelecting, value);
			}
			remove
			{
				base.Events.RemoveHandler(ObjectDataSourceView.EventSelecting, value);
			}
		}

		// Token: 0x140000EF RID: 239
		// (add) Token: 0x06004BFC RID: 19452 RVA: 0x00133E4A File Offset: 0x00132E4A
		// (remove) Token: 0x06004BFD RID: 19453 RVA: 0x00133E5D File Offset: 0x00132E5D
		public event ObjectDataSourceStatusEventHandler Updated
		{
			add
			{
				base.Events.AddHandler(ObjectDataSourceView.EventUpdated, value);
			}
			remove
			{
				base.Events.RemoveHandler(ObjectDataSourceView.EventUpdated, value);
			}
		}

		// Token: 0x140000F0 RID: 240
		// (add) Token: 0x06004BFE RID: 19454 RVA: 0x00133E70 File Offset: 0x00132E70
		// (remove) Token: 0x06004BFF RID: 19455 RVA: 0x00133E83 File Offset: 0x00132E83
		public event ObjectDataSourceMethodEventHandler Updating
		{
			add
			{
				base.Events.AddHandler(ObjectDataSourceView.EventUpdating, value);
			}
			remove
			{
				base.Events.RemoveHandler(ObjectDataSourceView.EventUpdating, value);
			}
		}

		// Token: 0x06004C00 RID: 19456 RVA: 0x00133E98 File Offset: 0x00132E98
		private object BuildDataObject(Type dataObjectType, IDictionary inputParameters)
		{
			object obj = Activator.CreateInstance(dataObjectType);
			PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(obj);
			foreach (object obj2 in inputParameters)
			{
				DictionaryEntry dictionaryEntry = (DictionaryEntry)obj2;
				string text = (dictionaryEntry.Key == null) ? string.Empty : dictionaryEntry.Key.ToString();
				PropertyDescriptor propertyDescriptor = properties.Find(text, true);
				if (propertyDescriptor == null)
				{
					throw new InvalidOperationException(SR.GetString("ObjectDataSourceView_DataObjectPropertyNotFound", new object[]
					{
						text,
						this._owner.ID
					}));
				}
				if (propertyDescriptor.IsReadOnly)
				{
					throw new InvalidOperationException(SR.GetString("ObjectDataSourceView_DataObjectPropertyReadOnly", new object[]
					{
						text,
						this._owner.ID
					}));
				}
				object value = ObjectDataSourceView.BuildObjectValue(dictionaryEntry.Value, propertyDescriptor.PropertyType, text);
				propertyDescriptor.SetValue(obj, value);
			}
			return obj;
		}

		// Token: 0x06004C01 RID: 19457 RVA: 0x00133FB4 File Offset: 0x00132FB4
		private static object BuildObjectValue(object value, Type destinationType, string paramName)
		{
			if (value != null && !destinationType.IsInstanceOfType(value))
			{
				Type type = destinationType;
				bool flag = false;
				if (destinationType.IsGenericType && destinationType.GetGenericTypeDefinition() == typeof(Nullable<>))
				{
					type = destinationType.GetGenericArguments()[0];
					flag = true;
				}
				else if (destinationType.IsByRef)
				{
					type = destinationType.GetElementType();
				}
				value = ObjectDataSourceView.ConvertType(value, type, paramName);
				if (flag)
				{
					Type type2 = value.GetType();
					if (type != type2)
					{
						throw new InvalidOperationException(SR.GetString("ObjectDataSourceView_CannotConvertType", new object[]
						{
							paramName,
							type2.FullName,
							string.Format(CultureInfo.InvariantCulture, "Nullable<{0}>", new object[]
							{
								destinationType.GetGenericArguments()[0].FullName
							})
						}));
					}
				}
			}
			return value;
		}

		// Token: 0x06004C02 RID: 19458 RVA: 0x00134078 File Offset: 0x00133078
		private static object ConvertType(object value, Type type, string paramName)
		{
			string text = value as string;
			if (text != null)
			{
				TypeConverter converter = TypeDescriptor.GetConverter(type);
				if (converter != null)
				{
					try
					{
						value = converter.ConvertFromInvariantString(text);
					}
					catch (NotSupportedException)
					{
						throw new InvalidOperationException(SR.GetString("ObjectDataSourceView_CannotConvertType", new object[]
						{
							paramName,
							typeof(string).FullName,
							type.FullName
						}));
					}
					catch (FormatException)
					{
						throw new InvalidOperationException(SR.GetString("ObjectDataSourceView_CannotConvertType", new object[]
						{
							paramName,
							typeof(string).FullName,
							type.FullName
						}));
					}
				}
			}
			return value;
		}

		// Token: 0x06004C03 RID: 19459 RVA: 0x00134134 File Offset: 0x00133134
		private IEnumerable CreateEnumerableData(object dataObject, DataSourceSelectArguments arguments)
		{
			if (this.FilterExpression.Length > 0)
			{
				throw new NotSupportedException(SR.GetString("ObjectDataSourceView_FilterNotSupported", new object[]
				{
					this._owner.ID
				}));
			}
			if (!string.IsNullOrEmpty(arguments.SortExpression))
			{
				throw new NotSupportedException(SR.GetString("ObjectDataSourceView_SortNotSupportedOnIEnumerable", new object[]
				{
					this._owner.ID
				}));
			}
			IEnumerable enumerable = dataObject as IEnumerable;
			if (enumerable != null)
			{
				if (!this.EnablePaging && arguments.RetrieveTotalRowCount && this.SelectCountMethod.Length == 0)
				{
					ICollection collection = enumerable as ICollection;
					if (collection != null)
					{
						arguments.TotalRowCount = collection.Count;
					}
				}
				return enumerable;
			}
			if (arguments.RetrieveTotalRowCount && this.SelectCountMethod.Length == 0)
			{
				arguments.TotalRowCount = 1;
			}
			return new object[]
			{
				dataObject
			};
		}

		// Token: 0x06004C04 RID: 19460 RVA: 0x00134214 File Offset: 0x00133214
		private IEnumerable CreateFilteredDataView(DataTable dataTable, string sortExpression, string filterExpression)
		{
			IOrderedDictionary values = this.FilterParameters.GetValues(this._context, this._owner);
			if (filterExpression.Length > 0)
			{
				ObjectDataSourceFilteringEventArgs objectDataSourceFilteringEventArgs = new ObjectDataSourceFilteringEventArgs(values);
				this.OnFiltering(objectDataSourceFilteringEventArgs);
				if (objectDataSourceFilteringEventArgs.Cancel)
				{
					return null;
				}
			}
			return FilteredDataSetHelper.CreateFilteredDataView(dataTable, sortExpression, filterExpression, values);
		}

		// Token: 0x06004C05 RID: 19461 RVA: 0x00134263 File Offset: 0x00133263
		public int Delete(IDictionary keys, IDictionary oldValues)
		{
			return this.ExecuteDelete(keys, oldValues);
		}

		// Token: 0x06004C06 RID: 19462 RVA: 0x00134270 File Offset: 0x00133270
		protected override int ExecuteDelete(IDictionary keys, IDictionary oldValues)
		{
			if (!this.CanDelete)
			{
				throw new NotSupportedException(SR.GetString("ObjectDataSourceView_DeleteNotSupported", new object[]
				{
					this._owner.ID
				}));
			}
			Type type = this.GetType(this.TypeName);
			Type type2 = this.TryGetDataObjectType();
			ObjectDataSourceView.ObjectDataSourceMethod resolvedMethodData;
			if (type2 != null)
			{
				IDictionary dictionary = new OrderedDictionary(StringComparer.OrdinalIgnoreCase);
				ObjectDataSourceView.MergeDictionaries(this.DeleteParameters, keys, dictionary);
				if (this.ConflictDetection == ConflictOptions.CompareAllValues)
				{
					if (oldValues == null)
					{
						throw new InvalidOperationException(SR.GetString("ObjectDataSourceView_Pessimistic", new object[]
						{
							SR.GetString("DataSourceView_delete"),
							this._owner.ID,
							"oldValues"
						}));
					}
					ObjectDataSourceView.MergeDictionaries(this.DeleteParameters, oldValues, dictionary);
				}
				object oldDataObject = this.BuildDataObject(type2, dictionary);
				resolvedMethodData = this.GetResolvedMethodData(type, this.DeleteMethod, type2, oldDataObject, null, DataSourceOperation.Delete);
				ObjectDataSourceMethodEventArgs objectDataSourceMethodEventArgs = new ObjectDataSourceMethodEventArgs(resolvedMethodData.Parameters);
				this.OnDeleting(objectDataSourceMethodEventArgs);
				if (objectDataSourceMethodEventArgs.Cancel)
				{
					return 0;
				}
			}
			else
			{
				IOrderedDictionary orderedDictionary = new OrderedDictionary(StringComparer.OrdinalIgnoreCase);
				string oldValuesParameterFormatString = this.OldValuesParameterFormatString;
				ObjectDataSourceView.MergeDictionaries(this.DeleteParameters, this.DeleteParameters.GetValues(this._context, this._owner), orderedDictionary);
				ObjectDataSourceView.MergeDictionaries(this.DeleteParameters, keys, orderedDictionary, oldValuesParameterFormatString);
				if (this.ConflictDetection == ConflictOptions.CompareAllValues)
				{
					if (oldValues == null)
					{
						throw new InvalidOperationException(SR.GetString("ObjectDataSourceView_Pessimistic", new object[]
						{
							SR.GetString("DataSourceView_delete"),
							this._owner.ID,
							"oldValues"
						}));
					}
					ObjectDataSourceView.MergeDictionaries(this.DeleteParameters, oldValues, orderedDictionary, oldValuesParameterFormatString);
				}
				ObjectDataSourceMethodEventArgs objectDataSourceMethodEventArgs2 = new ObjectDataSourceMethodEventArgs(orderedDictionary);
				this.OnDeleting(objectDataSourceMethodEventArgs2);
				if (objectDataSourceMethodEventArgs2.Cancel)
				{
					return 0;
				}
				resolvedMethodData = this.GetResolvedMethodData(type, this.DeleteMethod, orderedDictionary, DataSourceOperation.Delete);
			}
			ObjectDataSourceView.ObjectDataSourceResult objectDataSourceResult = this.InvokeMethod(resolvedMethodData);
			if (this._owner.Cache.Enabled)
			{
				this._owner.InvalidateCacheEntry();
			}
			this.OnDataSourceViewChanged(EventArgs.Empty);
			return objectDataSourceResult.AffectedRows;
		}

		// Token: 0x06004C07 RID: 19463 RVA: 0x00134484 File Offset: 0x00133484
		protected override int ExecuteInsert(IDictionary values)
		{
			if (!this.CanInsert)
			{
				throw new NotSupportedException(SR.GetString("ObjectDataSourceView_InsertNotSupported", new object[]
				{
					this._owner.ID
				}));
			}
			Type type = this.GetType(this.TypeName);
			Type type2 = this.TryGetDataObjectType();
			ObjectDataSourceView.ObjectDataSourceMethod resolvedMethodData;
			if (type2 != null)
			{
				if (values == null || values.Count == 0)
				{
					throw new InvalidOperationException(SR.GetString("ObjectDataSourceView_InsertRequiresValues", new object[]
					{
						this._owner.ID
					}));
				}
				IDictionary dictionary = new OrderedDictionary(StringComparer.OrdinalIgnoreCase);
				ObjectDataSourceView.MergeDictionaries(this.InsertParameters, values, dictionary);
				object newDataObject = this.BuildDataObject(type2, dictionary);
				resolvedMethodData = this.GetResolvedMethodData(type, this.InsertMethod, type2, null, newDataObject, DataSourceOperation.Insert);
				ObjectDataSourceMethodEventArgs objectDataSourceMethodEventArgs = new ObjectDataSourceMethodEventArgs(resolvedMethodData.Parameters);
				this.OnInserting(objectDataSourceMethodEventArgs);
				if (objectDataSourceMethodEventArgs.Cancel)
				{
					return 0;
				}
			}
			else
			{
				IOrderedDictionary orderedDictionary = new OrderedDictionary(StringComparer.OrdinalIgnoreCase);
				ObjectDataSourceView.MergeDictionaries(this.InsertParameters, this.InsertParameters.GetValues(this._context, this._owner), orderedDictionary);
				ObjectDataSourceView.MergeDictionaries(this.InsertParameters, values, orderedDictionary);
				ObjectDataSourceMethodEventArgs objectDataSourceMethodEventArgs2 = new ObjectDataSourceMethodEventArgs(orderedDictionary);
				this.OnInserting(objectDataSourceMethodEventArgs2);
				if (objectDataSourceMethodEventArgs2.Cancel)
				{
					return 0;
				}
				resolvedMethodData = this.GetResolvedMethodData(type, this.InsertMethod, orderedDictionary, DataSourceOperation.Insert);
			}
			ObjectDataSourceView.ObjectDataSourceResult objectDataSourceResult = this.InvokeMethod(resolvedMethodData);
			if (this._owner.Cache.Enabled)
			{
				this._owner.InvalidateCacheEntry();
			}
			this.OnDataSourceViewChanged(EventArgs.Empty);
			return objectDataSourceResult.AffectedRows;
		}

		// Token: 0x06004C08 RID: 19464 RVA: 0x0013460C File Offset: 0x0013360C
		protected internal override IEnumerable ExecuteSelect(DataSourceSelectArguments arguments)
		{
			if (this.SelectMethod.Length == 0)
			{
				throw new InvalidOperationException(SR.GetString("ObjectDataSourceView_SelectNotSupported", new object[]
				{
					this._owner.ID
				}));
			}
			if (this.CanSort)
			{
				arguments.AddSupportedCapabilities(DataSourceCapabilities.Sort);
			}
			if (this.CanPage)
			{
				arguments.AddSupportedCapabilities(DataSourceCapabilities.Page);
			}
			if (this.CanRetrieveTotalRowCount)
			{
				arguments.AddSupportedCapabilities(DataSourceCapabilities.RetrieveTotalRowCount);
			}
			arguments.RaiseUnsupportedCapabilitiesError(this);
			IOrderedDictionary orderedDictionary = new OrderedDictionary(StringComparer.OrdinalIgnoreCase);
			IDictionary values = this.SelectParameters.GetValues(this._context, this._owner);
			foreach (object obj in values)
			{
				DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
				orderedDictionary[dictionaryEntry.Key] = dictionaryEntry.Value;
			}
			bool enabled = this._owner.Cache.Enabled;
			if (enabled)
			{
				object obj2 = this._owner.LoadDataFromCache(arguments.StartRowIndex, arguments.MaximumRows);
				if (obj2 != null)
				{
					DataView dataView = obj2 as DataView;
					if (dataView != null)
					{
						if (arguments.RetrieveTotalRowCount && this.SelectCountMethod.Length == 0)
						{
							arguments.TotalRowCount = dataView.Count;
						}
						if (this.FilterExpression.Length > 0)
						{
							throw new NotSupportedException(SR.GetString("ObjectDataSourceView_FilterNotSupported", new object[]
							{
								this._owner.ID
							}));
						}
						if (string.IsNullOrEmpty(arguments.SortExpression))
						{
							return dataView;
						}
					}
					else
					{
						DataTable dataTable = FilteredDataSetHelper.GetDataTable(this._owner, obj2);
						if (dataTable != null)
						{
							this.ProcessPagingData(arguments, orderedDictionary);
							return this.CreateFilteredDataView(dataTable, arguments.SortExpression, this.FilterExpression);
						}
						IEnumerable result = this.CreateEnumerableData(obj2, arguments);
						this.ProcessPagingData(arguments, orderedDictionary);
						return result;
					}
				}
			}
			ObjectDataSourceSelectingEventArgs objectDataSourceSelectingEventArgs = new ObjectDataSourceSelectingEventArgs(orderedDictionary, arguments, false);
			this.OnSelecting(objectDataSourceSelectingEventArgs);
			if (objectDataSourceSelectingEventArgs.Cancel)
			{
				return null;
			}
			OrderedDictionary orderedDictionary2 = new OrderedDictionary(orderedDictionary.Count);
			foreach (object obj3 in orderedDictionary)
			{
				DictionaryEntry dictionaryEntry2 = (DictionaryEntry)obj3;
				orderedDictionary2.Add(dictionaryEntry2.Key, dictionaryEntry2.Value);
			}
			string sortParameterName = this.SortParameterName;
			if (sortParameterName.Length > 0)
			{
				orderedDictionary[sortParameterName] = arguments.SortExpression;
				arguments.SortExpression = string.Empty;
			}
			if (this.EnablePaging)
			{
				string maximumRowsParameterName = this.MaximumRowsParameterName;
				string startRowIndexParameterName = this.StartRowIndexParameterName;
				if (string.IsNullOrEmpty(maximumRowsParameterName) || string.IsNullOrEmpty(startRowIndexParameterName))
				{
					throw new InvalidOperationException(SR.GetString("ObjectDataSourceView_MissingPagingSettings", new object[]
					{
						this._owner.ID
					}));
				}
				IDictionary dictionary = new OrderedDictionary(StringComparer.OrdinalIgnoreCase);
				dictionary[maximumRowsParameterName] = arguments.MaximumRows;
				dictionary[startRowIndexParameterName] = arguments.StartRowIndex;
				ObjectDataSourceView.MergeDictionaries(this.SelectParameters, dictionary, orderedDictionary);
			}
			Type type = this.GetType(this.TypeName);
			object obj4 = null;
			ObjectDataSourceView.ObjectDataSourceResult objectDataSourceResult = null;
			try
			{
				ObjectDataSourceView.ObjectDataSourceMethod resolvedMethodData = this.GetResolvedMethodData(type, this.SelectMethod, orderedDictionary, DataSourceOperation.Select);
				objectDataSourceResult = this.InvokeMethod(resolvedMethodData, false, ref obj4);
				if (objectDataSourceResult.ReturnValue == null)
				{
					return null;
				}
				if (arguments.RetrieveTotalRowCount && this.SelectCountMethod.Length > 0)
				{
					int num = -1;
					if (enabled)
					{
						num = this._owner.LoadTotalRowCountFromCache();
						if (num >= 0)
						{
							arguments.TotalRowCount = num;
						}
					}
					if (num < 0)
					{
						num = this.QueryTotalRowCount(orderedDictionary2, arguments, true, ref obj4);
						arguments.TotalRowCount = num;
						if (enabled)
						{
							this._owner.SaveTotalRowCountToCache(num);
						}
					}
				}
			}
			finally
			{
				if (obj4 != null)
				{
					this.ReleaseInstance(obj4);
				}
			}
			DataView dataView2 = objectDataSourceResult.ReturnValue as DataView;
			if (dataView2 != null)
			{
				if (arguments.RetrieveTotalRowCount && this.SelectCountMethod.Length == 0)
				{
					arguments.TotalRowCount = dataView2.Count;
				}
				if (this.FilterExpression.Length > 0)
				{
					throw new NotSupportedException(SR.GetString("ObjectDataSourceView_FilterNotSupported", new object[]
					{
						this._owner.ID
					}));
				}
				if (!string.IsNullOrEmpty(arguments.SortExpression))
				{
					if (enabled)
					{
						throw new NotSupportedException(SR.GetString("ObjectDataSourceView_CacheNotSupportedOnSortedDataView", new object[]
						{
							this._owner.ID
						}));
					}
					dataView2.Sort = arguments.SortExpression;
				}
				if (enabled)
				{
					this.SaveDataAndRowCountToCache(arguments, objectDataSourceResult.ReturnValue);
				}
				return dataView2;
			}
			else
			{
				DataTable dataTable2 = FilteredDataSetHelper.GetDataTable(this._owner, objectDataSourceResult.ReturnValue);
				if (dataTable2 != null)
				{
					if (arguments.RetrieveTotalRowCount && this.SelectCountMethod.Length == 0)
					{
						arguments.TotalRowCount = dataTable2.Rows.Count;
					}
					if (enabled)
					{
						this.SaveDataAndRowCountToCache(arguments, objectDataSourceResult.ReturnValue);
					}
					return this.CreateFilteredDataView(dataTable2, arguments.SortExpression, this.FilterExpression);
				}
				IEnumerable enumerable = this.CreateEnumerableData(objectDataSourceResult.ReturnValue, arguments);
				if (enabled)
				{
					if (enumerable is IDataReader)
					{
						throw new NotSupportedException(SR.GetString("ObjectDataSourceView_CacheNotSupportedOnIDataReader", new object[]
						{
							this._owner.ID
						}));
					}
					this.SaveDataAndRowCountToCache(arguments, enumerable);
				}
				return enumerable;
			}
			IEnumerable result2;
			return result2;
		}

		// Token: 0x06004C09 RID: 19465 RVA: 0x00134B7C File Offset: 0x00133B7C
		protected override int ExecuteUpdate(IDictionary keys, IDictionary values, IDictionary oldValues)
		{
			if (!this.CanUpdate)
			{
				throw new NotSupportedException(SR.GetString("ObjectDataSourceView_UpdateNotSupported", new object[]
				{
					this._owner.ID
				}));
			}
			Type type = this.GetType(this.TypeName);
			Type type2 = this.TryGetDataObjectType();
			ObjectDataSourceView.ObjectDataSourceMethod resolvedMethodData;
			if (type2 != null)
			{
				if (this.ConflictDetection == ConflictOptions.CompareAllValues)
				{
					if (oldValues == null)
					{
						throw new InvalidOperationException(SR.GetString("ObjectDataSourceView_Pessimistic", new object[]
						{
							SR.GetString("DataSourceView_update"),
							this._owner.ID,
							"oldValues"
						}));
					}
					IDictionary dictionary = new OrderedDictionary(StringComparer.OrdinalIgnoreCase);
					ObjectDataSourceView.MergeDictionaries(this.UpdateParameters, oldValues, dictionary);
					ObjectDataSourceView.MergeDictionaries(this.UpdateParameters, keys, dictionary);
					ObjectDataSourceView.MergeDictionaries(this.UpdateParameters, values, dictionary);
					if (oldValues == null)
					{
						throw new InvalidOperationException(SR.GetString("ObjectDataSourceView_Pessimistic", new object[]
						{
							SR.GetString("DataSourceView_update"),
							this._owner.ID,
							"oldValues"
						}));
					}
					IDictionary dictionary2 = new OrderedDictionary(StringComparer.OrdinalIgnoreCase);
					ObjectDataSourceView.MergeDictionaries(this.UpdateParameters, oldValues, dictionary2);
					ObjectDataSourceView.MergeDictionaries(this.UpdateParameters, keys, dictionary2);
					object newDataObject = this.BuildDataObject(type2, dictionary);
					object oldDataObject = this.BuildDataObject(type2, dictionary2);
					resolvedMethodData = this.GetResolvedMethodData(type, this.UpdateMethod, type2, oldDataObject, newDataObject, DataSourceOperation.Update);
				}
				else
				{
					IDictionary dictionary3 = new OrderedDictionary(StringComparer.OrdinalIgnoreCase);
					ObjectDataSourceView.MergeDictionaries(this.UpdateParameters, oldValues, dictionary3);
					ObjectDataSourceView.MergeDictionaries(this.UpdateParameters, keys, dictionary3);
					ObjectDataSourceView.MergeDictionaries(this.UpdateParameters, values, dictionary3);
					object newDataObject2 = this.BuildDataObject(type2, dictionary3);
					resolvedMethodData = this.GetResolvedMethodData(type, this.UpdateMethod, type2, null, newDataObject2, DataSourceOperation.Update);
				}
				ObjectDataSourceMethodEventArgs objectDataSourceMethodEventArgs = new ObjectDataSourceMethodEventArgs(resolvedMethodData.Parameters);
				this.OnUpdating(objectDataSourceMethodEventArgs);
				if (objectDataSourceMethodEventArgs.Cancel)
				{
					return 0;
				}
			}
			else
			{
				IOrderedDictionary orderedDictionary = new OrderedDictionary(StringComparer.OrdinalIgnoreCase);
				string oldValuesParameterFormatString = this.OldValuesParameterFormatString;
				IDictionary values2 = this.UpdateParameters.GetValues(this._context, this._owner);
				if (keys != null)
				{
					foreach (object obj in keys)
					{
						DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
						if (values2.Contains(dictionaryEntry.Key))
						{
							values2.Remove(dictionaryEntry.Key);
						}
					}
				}
				ObjectDataSourceView.MergeDictionaries(this.UpdateParameters, values2, orderedDictionary);
				ObjectDataSourceView.MergeDictionaries(this.UpdateParameters, values, orderedDictionary);
				if (this.ConflictDetection == ConflictOptions.CompareAllValues)
				{
					ObjectDataSourceView.MergeDictionaries(this.UpdateParameters, oldValues, orderedDictionary, oldValuesParameterFormatString);
				}
				ObjectDataSourceView.MergeDictionaries(this.UpdateParameters, keys, orderedDictionary, oldValuesParameterFormatString);
				ObjectDataSourceMethodEventArgs objectDataSourceMethodEventArgs2 = new ObjectDataSourceMethodEventArgs(orderedDictionary);
				this.OnUpdating(objectDataSourceMethodEventArgs2);
				if (objectDataSourceMethodEventArgs2.Cancel)
				{
					return 0;
				}
				resolvedMethodData = this.GetResolvedMethodData(type, this.UpdateMethod, orderedDictionary, DataSourceOperation.Update);
			}
			ObjectDataSourceView.ObjectDataSourceResult objectDataSourceResult = this.InvokeMethod(resolvedMethodData);
			if (this._owner.Cache.Enabled)
			{
				this._owner.InvalidateCacheEntry();
			}
			this.OnDataSourceViewChanged(EventArgs.Empty);
			return objectDataSourceResult.AffectedRows;
		}

		// Token: 0x06004C0A RID: 19466 RVA: 0x00134EA8 File Offset: 0x00133EA8
		private static DataObjectMethodType GetMethodTypeFromOperation(DataSourceOperation operation)
		{
			switch (operation)
			{
			case DataSourceOperation.Delete:
				return DataObjectMethodType.Delete;
			case DataSourceOperation.Insert:
				return DataObjectMethodType.Insert;
			case DataSourceOperation.Select:
				return DataObjectMethodType.Select;
			case DataSourceOperation.Update:
				return DataObjectMethodType.Update;
			default:
				throw new ArgumentOutOfRangeException("operation");
			}
		}

		// Token: 0x06004C0B RID: 19467 RVA: 0x00134EE4 File Offset: 0x00133EE4
		private IDictionary GetOutputParameters(ParameterInfo[] parameters, object[] values)
		{
			IDictionary dictionary = new OrderedDictionary(StringComparer.OrdinalIgnoreCase);
			for (int i = 0; i < parameters.Length; i++)
			{
				ParameterInfo parameterInfo = parameters[i];
				if (parameterInfo.ParameterType.IsByRef)
				{
					dictionary[parameterInfo.Name] = values[i];
				}
			}
			return dictionary;
		}

		// Token: 0x06004C0C RID: 19468 RVA: 0x00134F2C File Offset: 0x00133F2C
		private ObjectDataSourceView.ObjectDataSourceMethod GetResolvedMethodData(Type type, string methodName, Type dataObjectType, object oldDataObject, object newDataObject, DataSourceOperation operation)
		{
			MethodInfo[] methods = type.GetMethods(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.FlattenHierarchy);
			MethodInfo methodInfo = null;
			ParameterInfo[] array = null;
			int num;
			if (oldDataObject == null)
			{
				num = 1;
			}
			else if (newDataObject == null)
			{
				num = 1;
			}
			else
			{
				num = 2;
			}
			foreach (MethodInfo methodInfo2 in methods)
			{
				if (string.Equals(methodName, methodInfo2.Name, StringComparison.OrdinalIgnoreCase) && !methodInfo2.IsGenericMethodDefinition)
				{
					ParameterInfo[] parameters = methodInfo2.GetParameters();
					int num2 = parameters.Length;
					if (num2 == num)
					{
						if (num == 1 && parameters[0].ParameterType == dataObjectType)
						{
							methodInfo = methodInfo2;
							array = parameters;
							break;
						}
						if (num == 2 && parameters[0].ParameterType == dataObjectType && parameters[1].ParameterType == dataObjectType)
						{
							methodInfo = methodInfo2;
							array = parameters;
							break;
						}
					}
				}
			}
			if (methodInfo == null)
			{
				throw new InvalidOperationException(SR.GetString("ObjectDataSourceView_MethodNotFoundForDataObject", new object[]
				{
					this._owner.ID,
					methodName,
					dataObjectType.FullName
				}));
			}
			OrderedDictionary orderedDictionary = new OrderedDictionary(2, StringComparer.OrdinalIgnoreCase);
			if (oldDataObject == null)
			{
				orderedDictionary.Add(array[0].Name, newDataObject);
			}
			else if (newDataObject == null)
			{
				orderedDictionary.Add(array[0].Name, oldDataObject);
			}
			else
			{
				string name = array[0].Name;
				string name2 = array[1].Name;
				string b = string.Format(CultureInfo.InvariantCulture, this.OldValuesParameterFormatString, new object[]
				{
					name
				});
				if (string.Equals(name2, b, StringComparison.OrdinalIgnoreCase))
				{
					orderedDictionary.Add(name, newDataObject);
					orderedDictionary.Add(name2, oldDataObject);
				}
				else
				{
					b = string.Format(CultureInfo.InvariantCulture, this.OldValuesParameterFormatString, new object[]
					{
						name2
					});
					if (!string.Equals(name, b, StringComparison.OrdinalIgnoreCase))
					{
						throw new InvalidOperationException(SR.GetString("ObjectDataSourceView_NoOldValuesParams", new object[]
						{
							this._owner.ID
						}));
					}
					orderedDictionary.Add(name, oldDataObject);
					orderedDictionary.Add(name2, newDataObject);
				}
			}
			return new ObjectDataSourceView.ObjectDataSourceMethod(operation, type, methodInfo, orderedDictionary.AsReadOnly());
		}

		// Token: 0x06004C0D RID: 19469 RVA: 0x00135134 File Offset: 0x00134134
		private ObjectDataSourceView.ObjectDataSourceMethod GetResolvedMethodData(Type type, string methodName, IDictionary allParameters, DataSourceOperation operation)
		{
			bool flag = operation == DataSourceOperation.SelectCount;
			DataObjectMethodType dataObjectMethodType = DataObjectMethodType.Select;
			if (!flag)
			{
				dataObjectMethodType = ObjectDataSourceView.GetMethodTypeFromOperation(operation);
			}
			MethodInfo[] methods = type.GetMethods(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.FlattenHierarchy);
			MethodInfo methodInfo = null;
			ParameterInfo[] array = null;
			int num = -1;
			bool flag2 = false;
			int count = allParameters.Count;
			foreach (MethodInfo methodInfo2 in methods)
			{
				if (string.Equals(methodName, methodInfo2.Name, StringComparison.OrdinalIgnoreCase) && !methodInfo2.IsGenericMethodDefinition)
				{
					ParameterInfo[] parameters = methodInfo2.GetParameters();
					int num2 = parameters.Length;
					if (num2 == count)
					{
						bool flag3 = false;
						foreach (ParameterInfo parameterInfo in parameters)
						{
							if (!allParameters.Contains(parameterInfo.Name))
							{
								flag3 = true;
								break;
							}
						}
						if (!flag3)
						{
							int num3 = 0;
							if (!flag)
							{
								DataObjectMethodAttribute dataObjectMethodAttribute = Attribute.GetCustomAttribute(methodInfo2, typeof(DataObjectMethodAttribute), true) as DataObjectMethodAttribute;
								if (dataObjectMethodAttribute != null && dataObjectMethodAttribute.MethodType == dataObjectMethodType)
								{
									if (dataObjectMethodAttribute.IsDefault)
									{
										num3 = 2;
									}
									else
									{
										num3 = 1;
									}
								}
							}
							if (num3 == num)
							{
								flag2 = true;
							}
							else if (num3 > num)
							{
								num = num3;
								flag2 = false;
								methodInfo = methodInfo2;
								array = parameters;
							}
						}
					}
				}
			}
			if (flag2)
			{
				throw new InvalidOperationException(SR.GetString("ObjectDataSourceView_MultipleOverloads", new object[]
				{
					this._owner.ID
				}));
			}
			if (methodInfo != null)
			{
				OrderedDictionary orderedDictionary = null;
				int num4 = array.Length;
				if (num4 > 0)
				{
					orderedDictionary = new OrderedDictionary(num4, StringComparer.OrdinalIgnoreCase);
					bool convertNullToDBNull = this.ConvertNullToDBNull;
					foreach (ParameterInfo parameterInfo2 in array)
					{
						string name = parameterInfo2.Name;
						object obj = allParameters[name];
						if (convertNullToDBNull && obj == null)
						{
							obj = DBNull.Value;
						}
						else
						{
							obj = ObjectDataSourceView.BuildObjectValue(obj, parameterInfo2.ParameterType, name);
						}
						orderedDictionary.Add(name, obj);
					}
				}
				return new ObjectDataSourceView.ObjectDataSourceMethod(operation, type, methodInfo, orderedDictionary);
			}
			if (count == 0)
			{
				throw new InvalidOperationException(SR.GetString("ObjectDataSourceView_MethodNotFoundNoParams", new object[]
				{
					this._owner.ID,
					methodName
				}));
			}
			string[] array4 = new string[count];
			allParameters.Keys.CopyTo(array4, 0);
			string text = string.Join(", ", array4);
			throw new InvalidOperationException(SR.GetString("ObjectDataSourceView_MethodNotFoundWithParams", new object[]
			{
				this._owner.ID,
				methodName,
				text
			}));
		}

		// Token: 0x06004C0E RID: 19470 RVA: 0x001353A8 File Offset: 0x001343A8
		private Type GetType(string typeName)
		{
			if (this.TypeName.Length == 0)
			{
				throw new InvalidOperationException(SR.GetString("ObjectDataSourceView_TypeNotSpecified", new object[]
				{
					this._owner.ID
				}));
			}
			Type type = BuildManager.GetType(this.TypeName, false, true);
			if (type == null)
			{
				throw new InvalidOperationException(SR.GetString("ObjectDataSourceView_TypeNotFound", new object[]
				{
					this._owner.ID
				}));
			}
			return type;
		}

		// Token: 0x06004C0F RID: 19471 RVA: 0x00135420 File Offset: 0x00134420
		public int Insert(IDictionary values)
		{
			return this.ExecuteInsert(values);
		}

		// Token: 0x06004C10 RID: 19472 RVA: 0x0013542C File Offset: 0x0013442C
		private ObjectDataSourceView.ObjectDataSourceResult InvokeMethod(ObjectDataSourceView.ObjectDataSourceMethod method)
		{
			object obj = null;
			return this.InvokeMethod(method, true, ref obj);
		}

		// Token: 0x06004C11 RID: 19473 RVA: 0x00135448 File Offset: 0x00134448
		private ObjectDataSourceView.ObjectDataSourceResult InvokeMethod(ObjectDataSourceView.ObjectDataSourceMethod method, bool disposeInstance, ref object instance)
		{
			if (method.MethodInfo.IsStatic)
			{
				if (instance != null)
				{
					this.ReleaseInstance(instance);
				}
				instance = null;
			}
			else if (instance == null)
			{
				ObjectDataSourceEventArgs objectDataSourceEventArgs = new ObjectDataSourceEventArgs(null);
				this.OnObjectCreating(objectDataSourceEventArgs);
				if (objectDataSourceEventArgs.ObjectInstance == null)
				{
					objectDataSourceEventArgs.ObjectInstance = Activator.CreateInstance(method.Type);
					this.OnObjectCreated(objectDataSourceEventArgs);
				}
				instance = objectDataSourceEventArgs.ObjectInstance;
			}
			object returnValue = null;
			int affectedRows = -1;
			bool flag = false;
			object[] array = null;
			if (method.Parameters != null && method.Parameters.Count > 0)
			{
				array = new object[method.Parameters.Count];
				for (int i = 0; i < method.Parameters.Count; i++)
				{
					array[i] = method.Parameters[i];
				}
			}
			try
			{
				returnValue = method.MethodInfo.Invoke(instance, array);
			}
			catch (Exception exception)
			{
				IDictionary outputParameters = this.GetOutputParameters(method.MethodInfo.GetParameters(), array);
				ObjectDataSourceStatusEventArgs objectDataSourceStatusEventArgs = new ObjectDataSourceStatusEventArgs(returnValue, outputParameters, exception);
				flag = true;
				switch (method.Operation)
				{
				case DataSourceOperation.Delete:
					this.OnDeleted(objectDataSourceStatusEventArgs);
					break;
				case DataSourceOperation.Insert:
					this.OnInserted(objectDataSourceStatusEventArgs);
					break;
				case DataSourceOperation.Select:
					this.OnSelected(objectDataSourceStatusEventArgs);
					break;
				case DataSourceOperation.Update:
					this.OnUpdated(objectDataSourceStatusEventArgs);
					break;
				case DataSourceOperation.SelectCount:
					this.OnSelected(objectDataSourceStatusEventArgs);
					break;
				}
				affectedRows = objectDataSourceStatusEventArgs.AffectedRows;
				if (!objectDataSourceStatusEventArgs.ExceptionHandled)
				{
					throw;
				}
			}
			finally
			{
				try
				{
					if (!flag)
					{
						IDictionary outputParameters2 = this.GetOutputParameters(method.MethodInfo.GetParameters(), array);
						ObjectDataSourceStatusEventArgs objectDataSourceStatusEventArgs2 = new ObjectDataSourceStatusEventArgs(returnValue, outputParameters2);
						switch (method.Operation)
						{
						case DataSourceOperation.Delete:
							this.OnDeleted(objectDataSourceStatusEventArgs2);
							break;
						case DataSourceOperation.Insert:
							this.OnInserted(objectDataSourceStatusEventArgs2);
							break;
						case DataSourceOperation.Select:
							this.OnSelected(objectDataSourceStatusEventArgs2);
							break;
						case DataSourceOperation.Update:
							this.OnUpdated(objectDataSourceStatusEventArgs2);
							break;
						case DataSourceOperation.SelectCount:
							this.OnSelected(objectDataSourceStatusEventArgs2);
							break;
						}
						affectedRows = objectDataSourceStatusEventArgs2.AffectedRows;
					}
				}
				finally
				{
					if (instance != null && disposeInstance)
					{
						this.ReleaseInstance(instance);
						instance = null;
					}
				}
			}
			return new ObjectDataSourceView.ObjectDataSourceResult(returnValue, affectedRows);
		}

		// Token: 0x06004C12 RID: 19474 RVA: 0x00135684 File Offset: 0x00134684
		protected virtual void LoadViewState(object savedState)
		{
			if (savedState == null)
			{
				return;
			}
			Pair pair = (Pair)savedState;
			if (pair.First != null)
			{
				((IStateManager)this.SelectParameters).LoadViewState(pair.First);
			}
			if (pair.Second != null)
			{
				((IStateManager)this.FilterParameters).LoadViewState(pair.Second);
			}
		}

		// Token: 0x06004C13 RID: 19475 RVA: 0x001356CE File Offset: 0x001346CE
		private static void MergeDictionaries(ParameterCollection reference, IDictionary source, IDictionary destination)
		{
			ObjectDataSourceView.MergeDictionaries(reference, source, destination, null);
		}

		// Token: 0x06004C14 RID: 19476 RVA: 0x001356DC File Offset: 0x001346DC
		private static void MergeDictionaries(ParameterCollection reference, IDictionary source, IDictionary destination, string parameterNameFormatString)
		{
			if (source != null)
			{
				foreach (object obj in source)
				{
					DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
					object value = dictionaryEntry.Value;
					Parameter parameter = null;
					string text = (string)dictionaryEntry.Key;
					if (parameterNameFormatString != null)
					{
						text = string.Format(CultureInfo.InvariantCulture, parameterNameFormatString, new object[]
						{
							text
						});
					}
					foreach (object obj2 in reference)
					{
						Parameter parameter2 = (Parameter)obj2;
						if (string.Equals(parameter2.Name, text, StringComparison.OrdinalIgnoreCase))
						{
							parameter = parameter2;
							break;
						}
					}
					if (parameter != null)
					{
						value = parameter.GetValue(value, true);
					}
					destination[text] = value;
				}
			}
		}

		// Token: 0x06004C15 RID: 19477 RVA: 0x001357E0 File Offset: 0x001347E0
		protected virtual void OnDeleted(ObjectDataSourceStatusEventArgs e)
		{
			ObjectDataSourceStatusEventHandler objectDataSourceStatusEventHandler = base.Events[ObjectDataSourceView.EventDeleted] as ObjectDataSourceStatusEventHandler;
			if (objectDataSourceStatusEventHandler != null)
			{
				objectDataSourceStatusEventHandler(this, e);
			}
		}

		// Token: 0x06004C16 RID: 19478 RVA: 0x00135810 File Offset: 0x00134810
		protected virtual void OnDeleting(ObjectDataSourceMethodEventArgs e)
		{
			ObjectDataSourceMethodEventHandler objectDataSourceMethodEventHandler = base.Events[ObjectDataSourceView.EventDeleting] as ObjectDataSourceMethodEventHandler;
			if (objectDataSourceMethodEventHandler != null)
			{
				objectDataSourceMethodEventHandler(this, e);
			}
		}

		// Token: 0x06004C17 RID: 19479 RVA: 0x00135840 File Offset: 0x00134840
		protected virtual void OnFiltering(ObjectDataSourceFilteringEventArgs e)
		{
			ObjectDataSourceFilteringEventHandler objectDataSourceFilteringEventHandler = base.Events[ObjectDataSourceView.EventFiltering] as ObjectDataSourceFilteringEventHandler;
			if (objectDataSourceFilteringEventHandler != null)
			{
				objectDataSourceFilteringEventHandler(this, e);
			}
		}

		// Token: 0x06004C18 RID: 19480 RVA: 0x00135870 File Offset: 0x00134870
		protected virtual void OnInserted(ObjectDataSourceStatusEventArgs e)
		{
			ObjectDataSourceStatusEventHandler objectDataSourceStatusEventHandler = base.Events[ObjectDataSourceView.EventInserted] as ObjectDataSourceStatusEventHandler;
			if (objectDataSourceStatusEventHandler != null)
			{
				objectDataSourceStatusEventHandler(this, e);
			}
		}

		// Token: 0x06004C19 RID: 19481 RVA: 0x001358A0 File Offset: 0x001348A0
		protected virtual void OnInserting(ObjectDataSourceMethodEventArgs e)
		{
			ObjectDataSourceMethodEventHandler objectDataSourceMethodEventHandler = base.Events[ObjectDataSourceView.EventInserting] as ObjectDataSourceMethodEventHandler;
			if (objectDataSourceMethodEventHandler != null)
			{
				objectDataSourceMethodEventHandler(this, e);
			}
		}

		// Token: 0x06004C1A RID: 19482 RVA: 0x001358D0 File Offset: 0x001348D0
		protected virtual void OnObjectCreated(ObjectDataSourceEventArgs e)
		{
			ObjectDataSourceObjectEventHandler objectDataSourceObjectEventHandler = base.Events[ObjectDataSourceView.EventObjectCreated] as ObjectDataSourceObjectEventHandler;
			if (objectDataSourceObjectEventHandler != null)
			{
				objectDataSourceObjectEventHandler(this, e);
			}
		}

		// Token: 0x06004C1B RID: 19483 RVA: 0x00135900 File Offset: 0x00134900
		protected virtual void OnObjectCreating(ObjectDataSourceEventArgs e)
		{
			ObjectDataSourceObjectEventHandler objectDataSourceObjectEventHandler = base.Events[ObjectDataSourceView.EventObjectCreating] as ObjectDataSourceObjectEventHandler;
			if (objectDataSourceObjectEventHandler != null)
			{
				objectDataSourceObjectEventHandler(this, e);
			}
		}

		// Token: 0x06004C1C RID: 19484 RVA: 0x00135930 File Offset: 0x00134930
		protected virtual void OnObjectDisposing(ObjectDataSourceDisposingEventArgs e)
		{
			ObjectDataSourceDisposingEventHandler objectDataSourceDisposingEventHandler = base.Events[ObjectDataSourceView.EventObjectDisposing] as ObjectDataSourceDisposingEventHandler;
			if (objectDataSourceDisposingEventHandler != null)
			{
				objectDataSourceDisposingEventHandler(this, e);
			}
		}

		// Token: 0x06004C1D RID: 19485 RVA: 0x00135960 File Offset: 0x00134960
		protected virtual void OnSelected(ObjectDataSourceStatusEventArgs e)
		{
			ObjectDataSourceStatusEventHandler objectDataSourceStatusEventHandler = base.Events[ObjectDataSourceView.EventSelected] as ObjectDataSourceStatusEventHandler;
			if (objectDataSourceStatusEventHandler != null)
			{
				objectDataSourceStatusEventHandler(this, e);
			}
		}

		// Token: 0x06004C1E RID: 19486 RVA: 0x00135990 File Offset: 0x00134990
		protected virtual void OnSelecting(ObjectDataSourceSelectingEventArgs e)
		{
			ObjectDataSourceSelectingEventHandler objectDataSourceSelectingEventHandler = base.Events[ObjectDataSourceView.EventSelecting] as ObjectDataSourceSelectingEventHandler;
			if (objectDataSourceSelectingEventHandler != null)
			{
				objectDataSourceSelectingEventHandler(this, e);
			}
		}

		// Token: 0x06004C1F RID: 19487 RVA: 0x001359C0 File Offset: 0x001349C0
		protected virtual void OnUpdated(ObjectDataSourceStatusEventArgs e)
		{
			ObjectDataSourceStatusEventHandler objectDataSourceStatusEventHandler = base.Events[ObjectDataSourceView.EventUpdated] as ObjectDataSourceStatusEventHandler;
			if (objectDataSourceStatusEventHandler != null)
			{
				objectDataSourceStatusEventHandler(this, e);
			}
		}

		// Token: 0x06004C20 RID: 19488 RVA: 0x001359F0 File Offset: 0x001349F0
		protected virtual void OnUpdating(ObjectDataSourceMethodEventArgs e)
		{
			ObjectDataSourceMethodEventHandler objectDataSourceMethodEventHandler = base.Events[ObjectDataSourceView.EventUpdating] as ObjectDataSourceMethodEventHandler;
			if (objectDataSourceMethodEventHandler != null)
			{
				objectDataSourceMethodEventHandler(this, e);
			}
		}

		// Token: 0x06004C21 RID: 19489 RVA: 0x00135A20 File Offset: 0x00134A20
		private void ProcessPagingData(DataSourceSelectArguments arguments, IOrderedDictionary parameters)
		{
			if (arguments.RetrieveTotalRowCount)
			{
				int num = this._owner.LoadTotalRowCountFromCache();
				if (num >= 0)
				{
					arguments.TotalRowCount = num;
					return;
				}
				object obj = null;
				num = this.QueryTotalRowCount(parameters, arguments, true, ref obj);
				arguments.TotalRowCount = num;
				this._owner.SaveTotalRowCountToCache(num);
			}
		}

		// Token: 0x06004C22 RID: 19490 RVA: 0x00135A70 File Offset: 0x00134A70
		private int QueryTotalRowCount(IOrderedDictionary mergedParameters, DataSourceSelectArguments arguments, bool disposeInstance, ref object instance)
		{
			if (this.SelectCountMethod.Length > 0)
			{
				ObjectDataSourceSelectingEventArgs objectDataSourceSelectingEventArgs = new ObjectDataSourceSelectingEventArgs(mergedParameters, arguments, true);
				this.OnSelecting(objectDataSourceSelectingEventArgs);
				if (objectDataSourceSelectingEventArgs.Cancel)
				{
					return -1;
				}
				Type type = this.GetType(this.TypeName);
				ObjectDataSourceView.ObjectDataSourceMethod resolvedMethodData = this.GetResolvedMethodData(type, this.SelectCountMethod, mergedParameters, DataSourceOperation.SelectCount);
				ObjectDataSourceView.ObjectDataSourceResult objectDataSourceResult = this.InvokeMethod(resolvedMethodData, disposeInstance, ref instance);
				if (objectDataSourceResult.ReturnValue != null && objectDataSourceResult.ReturnValue is int)
				{
					return (int)objectDataSourceResult.ReturnValue;
				}
			}
			return -1;
		}

		// Token: 0x06004C23 RID: 19491 RVA: 0x00135AF0 File Offset: 0x00134AF0
		private void ReleaseInstance(object instance)
		{
			ObjectDataSourceDisposingEventArgs objectDataSourceDisposingEventArgs = new ObjectDataSourceDisposingEventArgs(instance);
			this.OnObjectDisposing(objectDataSourceDisposingEventArgs);
			if (!objectDataSourceDisposingEventArgs.Cancel)
			{
				IDisposable disposable = instance as IDisposable;
				if (disposable != null)
				{
					disposable.Dispose();
				}
			}
		}

		// Token: 0x06004C24 RID: 19492 RVA: 0x00135B24 File Offset: 0x00134B24
		private void SaveDataAndRowCountToCache(DataSourceSelectArguments arguments, object data)
		{
			if (arguments.RetrieveTotalRowCount)
			{
				int num = this._owner.LoadTotalRowCountFromCache();
				if (num != arguments.TotalRowCount)
				{
					this._owner.SaveTotalRowCountToCache(arguments.TotalRowCount);
				}
			}
			this._owner.SaveDataToCache(arguments.StartRowIndex, arguments.MaximumRows, data);
		}

		// Token: 0x06004C25 RID: 19493 RVA: 0x00135B78 File Offset: 0x00134B78
		protected virtual object SaveViewState()
		{
			Pair pair = new Pair();
			pair.First = ((this._selectParameters != null) ? ((IStateManager)this._selectParameters).SaveViewState() : null);
			pair.Second = ((this._filterParameters != null) ? ((IStateManager)this._filterParameters).SaveViewState() : null);
			if (pair.First == null && pair.Second == null)
			{
				return null;
			}
			return pair;
		}

		// Token: 0x06004C26 RID: 19494 RVA: 0x00135BD6 File Offset: 0x00134BD6
		public IEnumerable Select(DataSourceSelectArguments arguments)
		{
			return this.ExecuteSelect(arguments);
		}

		// Token: 0x06004C27 RID: 19495 RVA: 0x00135BDF File Offset: 0x00134BDF
		private void SelectParametersChangedEventHandler(object o, EventArgs e)
		{
			this.OnDataSourceViewChanged(EventArgs.Empty);
		}

		// Token: 0x06004C28 RID: 19496 RVA: 0x00135BEC File Offset: 0x00134BEC
		protected virtual void TrackViewState()
		{
			this._tracking = true;
			if (this._selectParameters != null)
			{
				((IStateManager)this._selectParameters).TrackViewState();
			}
			if (this._filterParameters != null)
			{
				((IStateManager)this._filterParameters).TrackViewState();
			}
		}

		// Token: 0x06004C29 RID: 19497 RVA: 0x00135C1C File Offset: 0x00134C1C
		private Type TryGetDataObjectType()
		{
			string dataObjectTypeName = this.DataObjectTypeName;
			if (dataObjectTypeName.Length == 0)
			{
				return null;
			}
			Type type = BuildManager.GetType(dataObjectTypeName, false, true);
			if (type == null)
			{
				throw new InvalidOperationException(SR.GetString("ObjectDataSourceView_DataObjectTypeNotFound", new object[]
				{
					this._owner.ID
				}));
			}
			return type;
		}

		// Token: 0x06004C2A RID: 19498 RVA: 0x00135C6D File Offset: 0x00134C6D
		public int Update(IDictionary keys, IDictionary values, IDictionary oldValues)
		{
			return this.ExecuteUpdate(keys, values, oldValues);
		}

		// Token: 0x17001314 RID: 4884
		// (get) Token: 0x06004C2B RID: 19499 RVA: 0x00135C78 File Offset: 0x00134C78
		bool IStateManager.IsTrackingViewState
		{
			get
			{
				return this.IsTrackingViewState;
			}
		}

		// Token: 0x06004C2C RID: 19500 RVA: 0x00135C80 File Offset: 0x00134C80
		void IStateManager.LoadViewState(object savedState)
		{
			this.LoadViewState(savedState);
		}

		// Token: 0x06004C2D RID: 19501 RVA: 0x00135C89 File Offset: 0x00134C89
		object IStateManager.SaveViewState()
		{
			return this.SaveViewState();
		}

		// Token: 0x06004C2E RID: 19502 RVA: 0x00135C91 File Offset: 0x00134C91
		void IStateManager.TrackViewState()
		{
			this.TrackViewState();
		}

		// Token: 0x04002BB6 RID: 11190
		private static readonly object EventDeleted = new object();

		// Token: 0x04002BB7 RID: 11191
		private static readonly object EventDeleting = new object();

		// Token: 0x04002BB8 RID: 11192
		private static readonly object EventFiltering = new object();

		// Token: 0x04002BB9 RID: 11193
		private static readonly object EventInserted = new object();

		// Token: 0x04002BBA RID: 11194
		private static readonly object EventInserting = new object();

		// Token: 0x04002BBB RID: 11195
		private static readonly object EventObjectCreated = new object();

		// Token: 0x04002BBC RID: 11196
		private static readonly object EventObjectCreating = new object();

		// Token: 0x04002BBD RID: 11197
		private static readonly object EventObjectDisposing = new object();

		// Token: 0x04002BBE RID: 11198
		private static readonly object EventSelected = new object();

		// Token: 0x04002BBF RID: 11199
		private static readonly object EventSelecting = new object();

		// Token: 0x04002BC0 RID: 11200
		private static readonly object EventUpdated = new object();

		// Token: 0x04002BC1 RID: 11201
		private static readonly object EventUpdating = new object();

		// Token: 0x04002BC2 RID: 11202
		private HttpContext _context;

		// Token: 0x04002BC3 RID: 11203
		private ObjectDataSource _owner;

		// Token: 0x04002BC4 RID: 11204
		private bool _tracking;

		// Token: 0x04002BC5 RID: 11205
		private ConflictOptions _conflictDetection;

		// Token: 0x04002BC6 RID: 11206
		private bool _convertNullToDBNull;

		// Token: 0x04002BC7 RID: 11207
		private string _dataObjectTypeName;

		// Token: 0x04002BC8 RID: 11208
		private string _deleteMethod;

		// Token: 0x04002BC9 RID: 11209
		private ParameterCollection _deleteParameters;

		// Token: 0x04002BCA RID: 11210
		private bool _enablePaging;

		// Token: 0x04002BCB RID: 11211
		private string _filterExpression;

		// Token: 0x04002BCC RID: 11212
		private ParameterCollection _filterParameters;

		// Token: 0x04002BCD RID: 11213
		private string _insertMethod;

		// Token: 0x04002BCE RID: 11214
		private ParameterCollection _insertParameters;

		// Token: 0x04002BCF RID: 11215
		private string _maximumRowsParameterName;

		// Token: 0x04002BD0 RID: 11216
		private string _oldValuesParameterFormatString;

		// Token: 0x04002BD1 RID: 11217
		private string _selectCountMethod;

		// Token: 0x04002BD2 RID: 11218
		private string _selectMethod;

		// Token: 0x04002BD3 RID: 11219
		private ParameterCollection _selectParameters;

		// Token: 0x04002BD4 RID: 11220
		private string _sortParameterName;

		// Token: 0x04002BD5 RID: 11221
		private string _startRowIndexParameterName;

		// Token: 0x04002BD6 RID: 11222
		private string _typeName;

		// Token: 0x04002BD7 RID: 11223
		private string _updateMethod;

		// Token: 0x04002BD8 RID: 11224
		private ParameterCollection _updateParameters;

		// Token: 0x02000600 RID: 1536
		private struct ObjectDataSourceMethod
		{
			// Token: 0x06004C30 RID: 19504 RVA: 0x00135D21 File Offset: 0x00134D21
			internal ObjectDataSourceMethod(DataSourceOperation operation, Type type, MethodInfo methodInfo, OrderedDictionary parameters)
			{
				this.Operation = operation;
				this.Type = type;
				this.Parameters = parameters;
				this.MethodInfo = methodInfo;
			}

			// Token: 0x04002BD9 RID: 11225
			internal DataSourceOperation Operation;

			// Token: 0x04002BDA RID: 11226
			internal Type Type;

			// Token: 0x04002BDB RID: 11227
			internal OrderedDictionary Parameters;

			// Token: 0x04002BDC RID: 11228
			internal MethodInfo MethodInfo;
		}

		// Token: 0x02000601 RID: 1537
		private class ObjectDataSourceResult
		{
			// Token: 0x06004C31 RID: 19505 RVA: 0x00135D40 File Offset: 0x00134D40
			internal ObjectDataSourceResult(object returnValue, int affectedRows)
			{
				this.ReturnValue = returnValue;
				this.AffectedRows = affectedRows;
			}

			// Token: 0x04002BDD RID: 11229
			internal object ReturnValue;

			// Token: 0x04002BDE RID: 11230
			internal int AffectedRows;
		}
	}
}
