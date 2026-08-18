using System;
using System.Collections;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Globalization;
using System.Reflection;
using System.Web.Compilation;

namespace System.Web.UI.WebControls
{
	// Token: 0x02000492 RID: 1170
	public class ObjectDataSourceView : DataSourceView, IStateManager
	{
		// Token: 0x060039CA RID: 14794 RVA: 0x000BAEF5 File Offset: 0x000B90F5
		public ObjectDataSourceView(ObjectDataSource owner, string name, HttpContext context) : base(owner, name)
		{
			this._owner = owner;
			this._context = context;
		}

		// Token: 0x170010D6 RID: 4310
		// (get) Token: 0x060039CB RID: 14795 RVA: 0x000BAF0D File Offset: 0x000B910D
		public override bool CanDelete
		{
			get
			{
				return this.DeleteMethod.Length != 0;
			}
		}

		// Token: 0x170010D7 RID: 4311
		// (get) Token: 0x060039CC RID: 14796 RVA: 0x000BAF1D File Offset: 0x000B911D
		public override bool CanInsert
		{
			get
			{
				return this.InsertMethod.Length != 0;
			}
		}

		// Token: 0x170010D8 RID: 4312
		// (get) Token: 0x060039CD RID: 14797 RVA: 0x000BAF2D File Offset: 0x000B912D
		public override bool CanPage
		{
			get
			{
				return this.EnablePaging;
			}
		}

		// Token: 0x170010D9 RID: 4313
		// (get) Token: 0x060039CE RID: 14798 RVA: 0x000BAF35 File Offset: 0x000B9135
		public override bool CanRetrieveTotalRowCount
		{
			get
			{
				return this.SelectCountMethod.Length > 0 || !this.EnablePaging;
			}
		}

		// Token: 0x170010DA RID: 4314
		// (get) Token: 0x060039CF RID: 14799 RVA: 0x000097B7 File Offset: 0x000079B7
		public override bool CanSort
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170010DB RID: 4315
		// (get) Token: 0x060039D0 RID: 14800 RVA: 0x000BAF50 File Offset: 0x000B9150
		public override bool CanUpdate
		{
			get
			{
				return this.UpdateMethod.Length != 0;
			}
		}

		// Token: 0x170010DC RID: 4316
		// (get) Token: 0x060039D1 RID: 14801 RVA: 0x000BAF60 File Offset: 0x000B9160
		// (set) Token: 0x060039D2 RID: 14802 RVA: 0x000BAF68 File Offset: 0x000B9168
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

		// Token: 0x170010DD RID: 4317
		// (get) Token: 0x060039D3 RID: 14803 RVA: 0x000BAF8F File Offset: 0x000B918F
		// (set) Token: 0x060039D4 RID: 14804 RVA: 0x000BAF97 File Offset: 0x000B9197
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

		// Token: 0x170010DE RID: 4318
		// (get) Token: 0x060039D5 RID: 14805 RVA: 0x000BAFA0 File Offset: 0x000B91A0
		// (set) Token: 0x060039D6 RID: 14806 RVA: 0x000BAFB6 File Offset: 0x000B91B6
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

		// Token: 0x170010DF RID: 4319
		// (get) Token: 0x060039D7 RID: 14807 RVA: 0x000BAFD8 File Offset: 0x000B91D8
		// (set) Token: 0x060039D8 RID: 14808 RVA: 0x000BAFEE File Offset: 0x000B91EE
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

		// Token: 0x170010E0 RID: 4320
		// (get) Token: 0x060039D9 RID: 14809 RVA: 0x000BAFF7 File Offset: 0x000B91F7
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

		// Token: 0x170010E1 RID: 4321
		// (get) Token: 0x060039DA RID: 14810 RVA: 0x000BB012 File Offset: 0x000B9212
		// (set) Token: 0x060039DB RID: 14811 RVA: 0x000BB01A File Offset: 0x000B921A
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

		// Token: 0x170010E2 RID: 4322
		// (get) Token: 0x060039DC RID: 14812 RVA: 0x000BB037 File Offset: 0x000B9237
		// (set) Token: 0x060039DD RID: 14813 RVA: 0x000BB04D File Offset: 0x000B924D
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

		// Token: 0x170010E3 RID: 4323
		// (get) Token: 0x060039DE RID: 14814 RVA: 0x000BB070 File Offset: 0x000B9270
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

		// Token: 0x170010E4 RID: 4324
		// (get) Token: 0x060039DF RID: 14815 RVA: 0x000BB0C0 File Offset: 0x000B92C0
		// (set) Token: 0x060039E0 RID: 14816 RVA: 0x000BB0D6 File Offset: 0x000B92D6
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

		// Token: 0x170010E5 RID: 4325
		// (get) Token: 0x060039E1 RID: 14817 RVA: 0x000BB0DF File Offset: 0x000B92DF
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

		// Token: 0x170010E6 RID: 4326
		// (get) Token: 0x060039E2 RID: 14818 RVA: 0x000BB0FA File Offset: 0x000B92FA
		protected bool IsTrackingViewState
		{
			get
			{
				return this._tracking;
			}
		}

		// Token: 0x170010E7 RID: 4327
		// (get) Token: 0x060039E3 RID: 14819 RVA: 0x000BB102 File Offset: 0x000B9302
		// (set) Token: 0x060039E4 RID: 14820 RVA: 0x000BB118 File Offset: 0x000B9318
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

		// Token: 0x170010E8 RID: 4328
		// (get) Token: 0x060039E5 RID: 14821 RVA: 0x000BB13A File Offset: 0x000B933A
		// (set) Token: 0x060039E6 RID: 14822 RVA: 0x000BB150 File Offset: 0x000B9350
		[DefaultValue("{0}")]
		[WebCategory("Data")]
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

		// Token: 0x170010E9 RID: 4329
		// (get) Token: 0x060039E7 RID: 14823 RVA: 0x000BB164 File Offset: 0x000B9364
		// (set) Token: 0x060039E8 RID: 14824 RVA: 0x000BB17A File Offset: 0x000B937A
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

		// Token: 0x170010EA RID: 4330
		// (get) Token: 0x060039E9 RID: 14825 RVA: 0x000BB19C File Offset: 0x000B939C
		// (set) Token: 0x060039EA RID: 14826 RVA: 0x000BB1B2 File Offset: 0x000B93B2
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

		// Token: 0x170010EB RID: 4331
		// (get) Token: 0x060039EB RID: 14827 RVA: 0x000BB1D4 File Offset: 0x000B93D4
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

		// Token: 0x170010EC RID: 4332
		// (get) Token: 0x060039EC RID: 14828 RVA: 0x000BB224 File Offset: 0x000B9424
		// (set) Token: 0x060039ED RID: 14829 RVA: 0x000BB23A File Offset: 0x000B943A
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

		// Token: 0x170010ED RID: 4333
		// (get) Token: 0x060039EE RID: 14830 RVA: 0x000BB25C File Offset: 0x000B945C
		// (set) Token: 0x060039EF RID: 14831 RVA: 0x000BB272 File Offset: 0x000B9472
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

		// Token: 0x170010EE RID: 4334
		// (get) Token: 0x060039F0 RID: 14832 RVA: 0x000BB294 File Offset: 0x000B9494
		// (set) Token: 0x060039F1 RID: 14833 RVA: 0x000BB2AA File Offset: 0x000B94AA
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

		// Token: 0x170010EF RID: 4335
		// (get) Token: 0x060039F2 RID: 14834 RVA: 0x000BB2CC File Offset: 0x000B94CC
		// (set) Token: 0x060039F3 RID: 14835 RVA: 0x000BB2E2 File Offset: 0x000B94E2
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

		// Token: 0x170010F0 RID: 4336
		// (get) Token: 0x060039F4 RID: 14836 RVA: 0x000BB2EB File Offset: 0x000B94EB
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

		// Token: 0x170010F1 RID: 4337
		// (get) Token: 0x060039F5 RID: 14837 RVA: 0x000BB306 File Offset: 0x000B9506
		// (set) Token: 0x060039F6 RID: 14838 RVA: 0x000BB30E File Offset: 0x000B950E
		public ParsingCulture ParsingCulture { get; set; }

		// Token: 0x140000CD RID: 205
		// (add) Token: 0x060039F7 RID: 14839 RVA: 0x000BB317 File Offset: 0x000B9517
		// (remove) Token: 0x060039F8 RID: 14840 RVA: 0x000BB32A File Offset: 0x000B952A
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

		// Token: 0x140000CE RID: 206
		// (add) Token: 0x060039F9 RID: 14841 RVA: 0x000BB33D File Offset: 0x000B953D
		// (remove) Token: 0x060039FA RID: 14842 RVA: 0x000BB350 File Offset: 0x000B9550
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

		// Token: 0x140000CF RID: 207
		// (add) Token: 0x060039FB RID: 14843 RVA: 0x000BB363 File Offset: 0x000B9563
		// (remove) Token: 0x060039FC RID: 14844 RVA: 0x000BB376 File Offset: 0x000B9576
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

		// Token: 0x140000D0 RID: 208
		// (add) Token: 0x060039FD RID: 14845 RVA: 0x000BB389 File Offset: 0x000B9589
		// (remove) Token: 0x060039FE RID: 14846 RVA: 0x000BB39C File Offset: 0x000B959C
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

		// Token: 0x140000D1 RID: 209
		// (add) Token: 0x060039FF RID: 14847 RVA: 0x000BB3AF File Offset: 0x000B95AF
		// (remove) Token: 0x06003A00 RID: 14848 RVA: 0x000BB3C2 File Offset: 0x000B95C2
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

		// Token: 0x140000D2 RID: 210
		// (add) Token: 0x06003A01 RID: 14849 RVA: 0x000BB3D5 File Offset: 0x000B95D5
		// (remove) Token: 0x06003A02 RID: 14850 RVA: 0x000BB3E8 File Offset: 0x000B95E8
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

		// Token: 0x140000D3 RID: 211
		// (add) Token: 0x06003A03 RID: 14851 RVA: 0x000BB3FB File Offset: 0x000B95FB
		// (remove) Token: 0x06003A04 RID: 14852 RVA: 0x000BB40E File Offset: 0x000B960E
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

		// Token: 0x140000D4 RID: 212
		// (add) Token: 0x06003A05 RID: 14853 RVA: 0x000BB421 File Offset: 0x000B9621
		// (remove) Token: 0x06003A06 RID: 14854 RVA: 0x000BB434 File Offset: 0x000B9634
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

		// Token: 0x140000D5 RID: 213
		// (add) Token: 0x06003A07 RID: 14855 RVA: 0x000BB447 File Offset: 0x000B9647
		// (remove) Token: 0x06003A08 RID: 14856 RVA: 0x000BB45A File Offset: 0x000B965A
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

		// Token: 0x140000D6 RID: 214
		// (add) Token: 0x06003A09 RID: 14857 RVA: 0x000BB46D File Offset: 0x000B966D
		// (remove) Token: 0x06003A0A RID: 14858 RVA: 0x000BB480 File Offset: 0x000B9680
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

		// Token: 0x140000D7 RID: 215
		// (add) Token: 0x06003A0B RID: 14859 RVA: 0x000BB493 File Offset: 0x000B9693
		// (remove) Token: 0x06003A0C RID: 14860 RVA: 0x000BB4A6 File Offset: 0x000B96A6
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

		// Token: 0x140000D8 RID: 216
		// (add) Token: 0x06003A0D RID: 14861 RVA: 0x000BB4B9 File Offset: 0x000B96B9
		// (remove) Token: 0x06003A0E RID: 14862 RVA: 0x000BB4CC File Offset: 0x000B96CC
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

		// Token: 0x06003A0F RID: 14863 RVA: 0x000BB4E0 File Offset: 0x000B96E0
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
				object value = ObjectDataSourceView.BuildObjectValue(dictionaryEntry.Value, propertyDescriptor.PropertyType, text, this.ParsingCulture);
				propertyDescriptor.SetValue(obj, value);
			}
			return obj;
		}

		// Token: 0x06003A10 RID: 14864 RVA: 0x000BB5F4 File Offset: 0x000B97F4
		private static object BuildObjectValue(object value, Type destinationType, string paramName, ParsingCulture parsingCulture)
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
				value = ObjectDataSourceView.ConvertType(value, type, paramName, parsingCulture);
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

		// Token: 0x06003A11 RID: 14865 RVA: 0x000BB6BC File Offset: 0x000B98BC
		private static object ConvertType(object value, Type type, string paramName, ParsingCulture parsingCulture)
		{
			string text = value as string;
			if (text != null)
			{
				TypeConverter converter = TypeDescriptor.GetConverter(type);
				if (converter != null)
				{
					try
					{
						if (parsingCulture == ParsingCulture.Current)
						{
							value = converter.ConvertFromString(null, CultureInfo.CurrentCulture, text);
						}
						else
						{
							value = converter.ConvertFromInvariantString(text);
						}
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

		// Token: 0x06003A12 RID: 14866 RVA: 0x000BB78C File Offset: 0x000B998C
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

		// Token: 0x06003A13 RID: 14867 RVA: 0x000BB864 File Offset: 0x000B9A64
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

		// Token: 0x06003A14 RID: 14868 RVA: 0x000BB8B3 File Offset: 0x000B9AB3
		public int Delete(IDictionary keys, IDictionary oldValues)
		{
			return this.ExecuteDelete(keys, oldValues);
		}

		// Token: 0x06003A15 RID: 14869 RVA: 0x000BB8C0 File Offset: 0x000B9AC0
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

		// Token: 0x06003A16 RID: 14870 RVA: 0x000BBAC8 File Offset: 0x000B9CC8
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

		// Token: 0x06003A17 RID: 14871 RVA: 0x000BBC4C File Offset: 0x000B9E4C
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

		// Token: 0x06003A18 RID: 14872 RVA: 0x000BC19C File Offset: 0x000BA39C
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

		// Token: 0x06003A19 RID: 14873 RVA: 0x000BC4BC File Offset: 0x000BA6BC
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

		// Token: 0x06003A1A RID: 14874 RVA: 0x000BC4E8 File Offset: 0x000BA6E8
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

		// Token: 0x06003A1B RID: 14875 RVA: 0x000BC530 File Offset: 0x000BA730
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

		// Token: 0x06003A1C RID: 14876 RVA: 0x000BC73C File Offset: 0x000BA93C
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
			if (!(methodInfo == null))
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
							obj = ObjectDataSourceView.BuildObjectValue(obj, parameterInfo2.ParameterType, name, this.ParsingCulture);
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

		// Token: 0x06003A1D RID: 14877 RVA: 0x000BC9A8 File Offset: 0x000BABA8
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

		// Token: 0x06003A1E RID: 14878 RVA: 0x000BCA22 File Offset: 0x000BAC22
		public int Insert(IDictionary values)
		{
			return this.ExecuteInsert(values);
		}

		// Token: 0x06003A1F RID: 14879 RVA: 0x000BCA2C File Offset: 0x000BAC2C
		private ObjectDataSourceView.ObjectDataSourceResult InvokeMethod(ObjectDataSourceView.ObjectDataSourceMethod method)
		{
			object obj = null;
			return this.InvokeMethod(method, true, ref obj);
		}

		// Token: 0x06003A20 RID: 14880 RVA: 0x000BCA48 File Offset: 0x000BAC48
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

		// Token: 0x06003A21 RID: 14881 RVA: 0x000BCC7C File Offset: 0x000BAE7C
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

		// Token: 0x06003A22 RID: 14882 RVA: 0x000BCCC6 File Offset: 0x000BAEC6
		private static void MergeDictionaries(ParameterCollection reference, IDictionary source, IDictionary destination)
		{
			ObjectDataSourceView.MergeDictionaries(reference, source, destination, null);
		}

		// Token: 0x06003A23 RID: 14883 RVA: 0x000BCCD4 File Offset: 0x000BAED4
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

		// Token: 0x06003A24 RID: 14884 RVA: 0x000BCDD4 File Offset: 0x000BAFD4
		protected virtual void OnDeleted(ObjectDataSourceStatusEventArgs e)
		{
			ObjectDataSourceStatusEventHandler objectDataSourceStatusEventHandler = base.Events[ObjectDataSourceView.EventDeleted] as ObjectDataSourceStatusEventHandler;
			if (objectDataSourceStatusEventHandler != null)
			{
				objectDataSourceStatusEventHandler(this, e);
			}
		}

		// Token: 0x06003A25 RID: 14885 RVA: 0x000BCE04 File Offset: 0x000BB004
		protected virtual void OnDeleting(ObjectDataSourceMethodEventArgs e)
		{
			ObjectDataSourceMethodEventHandler objectDataSourceMethodEventHandler = base.Events[ObjectDataSourceView.EventDeleting] as ObjectDataSourceMethodEventHandler;
			if (objectDataSourceMethodEventHandler != null)
			{
				objectDataSourceMethodEventHandler(this, e);
			}
		}

		// Token: 0x06003A26 RID: 14886 RVA: 0x000BCE34 File Offset: 0x000BB034
		protected virtual void OnFiltering(ObjectDataSourceFilteringEventArgs e)
		{
			ObjectDataSourceFilteringEventHandler objectDataSourceFilteringEventHandler = base.Events[ObjectDataSourceView.EventFiltering] as ObjectDataSourceFilteringEventHandler;
			if (objectDataSourceFilteringEventHandler != null)
			{
				objectDataSourceFilteringEventHandler(this, e);
			}
		}

		// Token: 0x06003A27 RID: 14887 RVA: 0x000BCE64 File Offset: 0x000BB064
		protected virtual void OnInserted(ObjectDataSourceStatusEventArgs e)
		{
			ObjectDataSourceStatusEventHandler objectDataSourceStatusEventHandler = base.Events[ObjectDataSourceView.EventInserted] as ObjectDataSourceStatusEventHandler;
			if (objectDataSourceStatusEventHandler != null)
			{
				objectDataSourceStatusEventHandler(this, e);
			}
		}

		// Token: 0x06003A28 RID: 14888 RVA: 0x000BCE94 File Offset: 0x000BB094
		protected virtual void OnInserting(ObjectDataSourceMethodEventArgs e)
		{
			ObjectDataSourceMethodEventHandler objectDataSourceMethodEventHandler = base.Events[ObjectDataSourceView.EventInserting] as ObjectDataSourceMethodEventHandler;
			if (objectDataSourceMethodEventHandler != null)
			{
				objectDataSourceMethodEventHandler(this, e);
			}
		}

		// Token: 0x06003A29 RID: 14889 RVA: 0x000BCEC4 File Offset: 0x000BB0C4
		protected virtual void OnObjectCreated(ObjectDataSourceEventArgs e)
		{
			ObjectDataSourceObjectEventHandler objectDataSourceObjectEventHandler = base.Events[ObjectDataSourceView.EventObjectCreated] as ObjectDataSourceObjectEventHandler;
			if (objectDataSourceObjectEventHandler != null)
			{
				objectDataSourceObjectEventHandler(this, e);
			}
		}

		// Token: 0x06003A2A RID: 14890 RVA: 0x000BCEF4 File Offset: 0x000BB0F4
		protected virtual void OnObjectCreating(ObjectDataSourceEventArgs e)
		{
			ObjectDataSourceObjectEventHandler objectDataSourceObjectEventHandler = base.Events[ObjectDataSourceView.EventObjectCreating] as ObjectDataSourceObjectEventHandler;
			if (objectDataSourceObjectEventHandler != null)
			{
				objectDataSourceObjectEventHandler(this, e);
			}
		}

		// Token: 0x06003A2B RID: 14891 RVA: 0x000BCF24 File Offset: 0x000BB124
		protected virtual void OnObjectDisposing(ObjectDataSourceDisposingEventArgs e)
		{
			ObjectDataSourceDisposingEventHandler objectDataSourceDisposingEventHandler = base.Events[ObjectDataSourceView.EventObjectDisposing] as ObjectDataSourceDisposingEventHandler;
			if (objectDataSourceDisposingEventHandler != null)
			{
				objectDataSourceDisposingEventHandler(this, e);
			}
		}

		// Token: 0x06003A2C RID: 14892 RVA: 0x000BCF54 File Offset: 0x000BB154
		protected virtual void OnSelected(ObjectDataSourceStatusEventArgs e)
		{
			ObjectDataSourceStatusEventHandler objectDataSourceStatusEventHandler = base.Events[ObjectDataSourceView.EventSelected] as ObjectDataSourceStatusEventHandler;
			if (objectDataSourceStatusEventHandler != null)
			{
				objectDataSourceStatusEventHandler(this, e);
			}
		}

		// Token: 0x06003A2D RID: 14893 RVA: 0x000BCF84 File Offset: 0x000BB184
		protected virtual void OnSelecting(ObjectDataSourceSelectingEventArgs e)
		{
			ObjectDataSourceSelectingEventHandler objectDataSourceSelectingEventHandler = base.Events[ObjectDataSourceView.EventSelecting] as ObjectDataSourceSelectingEventHandler;
			if (objectDataSourceSelectingEventHandler != null)
			{
				objectDataSourceSelectingEventHandler(this, e);
			}
		}

		// Token: 0x06003A2E RID: 14894 RVA: 0x000BCFB4 File Offset: 0x000BB1B4
		protected virtual void OnUpdated(ObjectDataSourceStatusEventArgs e)
		{
			ObjectDataSourceStatusEventHandler objectDataSourceStatusEventHandler = base.Events[ObjectDataSourceView.EventUpdated] as ObjectDataSourceStatusEventHandler;
			if (objectDataSourceStatusEventHandler != null)
			{
				objectDataSourceStatusEventHandler(this, e);
			}
		}

		// Token: 0x06003A2F RID: 14895 RVA: 0x000BCFE4 File Offset: 0x000BB1E4
		protected virtual void OnUpdating(ObjectDataSourceMethodEventArgs e)
		{
			ObjectDataSourceMethodEventHandler objectDataSourceMethodEventHandler = base.Events[ObjectDataSourceView.EventUpdating] as ObjectDataSourceMethodEventHandler;
			if (objectDataSourceMethodEventHandler != null)
			{
				objectDataSourceMethodEventHandler(this, e);
			}
		}

		// Token: 0x06003A30 RID: 14896 RVA: 0x000BD014 File Offset: 0x000BB214
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

		// Token: 0x06003A31 RID: 14897 RVA: 0x000BD064 File Offset: 0x000BB264
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

		// Token: 0x06003A32 RID: 14898 RVA: 0x000BD0E4 File Offset: 0x000BB2E4
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

		// Token: 0x06003A33 RID: 14899 RVA: 0x000BD118 File Offset: 0x000BB318
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

		// Token: 0x06003A34 RID: 14900 RVA: 0x000BD16C File Offset: 0x000BB36C
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

		// Token: 0x06003A35 RID: 14901 RVA: 0x000B940C File Offset: 0x000B760C
		public IEnumerable Select(DataSourceSelectArguments arguments)
		{
			return this.ExecuteSelect(arguments);
		}

		// Token: 0x06003A36 RID: 14902 RVA: 0x000B9CA8 File Offset: 0x000B7EA8
		private void SelectParametersChangedEventHandler(object o, EventArgs e)
		{
			this.OnDataSourceViewChanged(EventArgs.Empty);
		}

		// Token: 0x06003A37 RID: 14903 RVA: 0x000BD1CA File Offset: 0x000BB3CA
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

		// Token: 0x06003A38 RID: 14904 RVA: 0x000BD1FC File Offset: 0x000BB3FC
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

		// Token: 0x06003A39 RID: 14905 RVA: 0x000B9415 File Offset: 0x000B7615
		public int Update(IDictionary keys, IDictionary values, IDictionary oldValues)
		{
			return this.ExecuteUpdate(keys, values, oldValues);
		}

		// Token: 0x170010F2 RID: 4338
		// (get) Token: 0x06003A3A RID: 14906 RVA: 0x000BD251 File Offset: 0x000BB451
		bool IStateManager.IsTrackingViewState
		{
			get
			{
				return this.IsTrackingViewState;
			}
		}

		// Token: 0x06003A3B RID: 14907 RVA: 0x000BD259 File Offset: 0x000BB459
		void IStateManager.LoadViewState(object savedState)
		{
			this.LoadViewState(savedState);
		}

		// Token: 0x06003A3C RID: 14908 RVA: 0x000BD262 File Offset: 0x000BB462
		object IStateManager.SaveViewState()
		{
			return this.SaveViewState();
		}

		// Token: 0x06003A3D RID: 14909 RVA: 0x000BD26A File Offset: 0x000BB46A
		void IStateManager.TrackViewState()
		{
			this.TrackViewState();
		}

		// Token: 0x040022C5 RID: 8901
		private static readonly object EventDeleted = new object();

		// Token: 0x040022C6 RID: 8902
		private static readonly object EventDeleting = new object();

		// Token: 0x040022C7 RID: 8903
		private static readonly object EventFiltering = new object();

		// Token: 0x040022C8 RID: 8904
		private static readonly object EventInserted = new object();

		// Token: 0x040022C9 RID: 8905
		private static readonly object EventInserting = new object();

		// Token: 0x040022CA RID: 8906
		private static readonly object EventObjectCreated = new object();

		// Token: 0x040022CB RID: 8907
		private static readonly object EventObjectCreating = new object();

		// Token: 0x040022CC RID: 8908
		private static readonly object EventObjectDisposing = new object();

		// Token: 0x040022CD RID: 8909
		private static readonly object EventSelected = new object();

		// Token: 0x040022CE RID: 8910
		private static readonly object EventSelecting = new object();

		// Token: 0x040022CF RID: 8911
		private static readonly object EventUpdated = new object();

		// Token: 0x040022D0 RID: 8912
		private static readonly object EventUpdating = new object();

		// Token: 0x040022D1 RID: 8913
		private HttpContext _context;

		// Token: 0x040022D2 RID: 8914
		private ObjectDataSource _owner;

		// Token: 0x040022D3 RID: 8915
		private bool _tracking;

		// Token: 0x040022D4 RID: 8916
		private ConflictOptions _conflictDetection;

		// Token: 0x040022D5 RID: 8917
		private bool _convertNullToDBNull;

		// Token: 0x040022D6 RID: 8918
		private string _dataObjectTypeName;

		// Token: 0x040022D7 RID: 8919
		private string _deleteMethod;

		// Token: 0x040022D8 RID: 8920
		private ParameterCollection _deleteParameters;

		// Token: 0x040022D9 RID: 8921
		private bool _enablePaging;

		// Token: 0x040022DA RID: 8922
		private string _filterExpression;

		// Token: 0x040022DB RID: 8923
		private ParameterCollection _filterParameters;

		// Token: 0x040022DC RID: 8924
		private string _insertMethod;

		// Token: 0x040022DD RID: 8925
		private ParameterCollection _insertParameters;

		// Token: 0x040022DE RID: 8926
		private string _maximumRowsParameterName;

		// Token: 0x040022DF RID: 8927
		private string _oldValuesParameterFormatString;

		// Token: 0x040022E0 RID: 8928
		private string _selectCountMethod;

		// Token: 0x040022E1 RID: 8929
		private string _selectMethod;

		// Token: 0x040022E2 RID: 8930
		private ParameterCollection _selectParameters;

		// Token: 0x040022E3 RID: 8931
		private string _sortParameterName;

		// Token: 0x040022E4 RID: 8932
		private string _startRowIndexParameterName;

		// Token: 0x040022E5 RID: 8933
		private string _typeName;

		// Token: 0x040022E6 RID: 8934
		private string _updateMethod;

		// Token: 0x040022E7 RID: 8935
		private ParameterCollection _updateParameters;

		// Token: 0x020009B9 RID: 2489
		private struct ObjectDataSourceMethod
		{
			// Token: 0x06006C00 RID: 27648 RVA: 0x001824FF File Offset: 0x001806FF
			internal ObjectDataSourceMethod(DataSourceOperation operation, Type type, MethodInfo methodInfo, OrderedDictionary parameters)
			{
				this.Operation = operation;
				this.Type = type;
				this.Parameters = parameters;
				this.MethodInfo = methodInfo;
			}

			// Token: 0x04003979 RID: 14713
			internal DataSourceOperation Operation;

			// Token: 0x0400397A RID: 14714
			internal Type Type;

			// Token: 0x0400397B RID: 14715
			internal OrderedDictionary Parameters;

			// Token: 0x0400397C RID: 14716
			internal MethodInfo MethodInfo;
		}

		// Token: 0x020009BA RID: 2490
		private class ObjectDataSourceResult
		{
			// Token: 0x06006C01 RID: 27649 RVA: 0x0018251E File Offset: 0x0018071E
			internal ObjectDataSourceResult(object returnValue, int affectedRows)
			{
				this.ReturnValue = returnValue;
				this.AffectedRows = affectedRows;
			}

			// Token: 0x0400397D RID: 14717
			internal object ReturnValue;

			// Token: 0x0400397E RID: 14718
			internal int AffectedRows;
		}
	}
}
