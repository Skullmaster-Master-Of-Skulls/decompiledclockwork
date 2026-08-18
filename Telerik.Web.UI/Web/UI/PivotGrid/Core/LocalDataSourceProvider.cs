using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Telerik.Web.UI.PivotGrid.Core.Aggregates;
using Telerik.Web.UI.PivotGrid.Core.DataProviders;
using Telerik.Web.UI.PivotGrid.Core.Design;
using Telerik.Web.UI.PivotGrid.Core.Engine;
using Telerik.Web.UI.PivotGrid.Core.Fields;

namespace Telerik.Web.UI.PivotGrid.Core
{
	// Token: 0x02000C91 RID: 3217
	public class LocalDataSourceProvider : DataProviderBase, IAggregateDescriptionsGenerator
	{
		// Token: 0x060078C9 RID: 30921 RVA: 0x001BCD92 File Offset: 0x001BAF92
		public LocalDataSourceProvider() : this(new PivotEngine())
		{
		}

		// Token: 0x060078CA RID: 30922 RVA: 0x001BCD9F File Offset: 0x001BAF9F
		internal LocalDataSourceProvider(IPivotEngine engine) : this(engine, null)
		{
		}

		// Token: 0x060078CB RID: 30923 RVA: 0x001BCDA9 File Offset: 0x001BAFA9
		internal LocalDataSourceProvider(IPivotEngine engine, IFieldDescriptionProvider fieldInfoProvider) : this(new PivotSettings<PropertyFilterDescriptionBase, PropertyGroupDescriptionBase, LocalAggregateDescription>(), engine, fieldInfoProvider)
		{
		}

		// Token: 0x060078CC RID: 30924 RVA: 0x001BCDB8 File Offset: 0x001BAFB8
		internal LocalDataSourceProvider(PivotSettings<PropertyFilterDescriptionBase, PropertyGroupDescriptionBase, LocalAggregateDescription> settings, IPivotEngine engine, IFieldDescriptionProvider fieldInfoProvider) : base(settings, fieldInfoProvider)
		{
			this.settings = settings;
			this.settings.DataProvider = this;
			this.settings.DescriptionAdded += this.SettingsDescriptionAdded;
			if (engine != null)
			{
				this.engine = engine;
				this.engine.Completed += this.OnCompleted;
			}
			this.Culture = CultureInfo.InvariantCulture;
		}

		// Token: 0x170026FE RID: 9982
		// (get) Token: 0x060078CD RID: 30925 RVA: 0x001BCE2E File Offset: 0x001BB02E
		// (set) Token: 0x060078CE RID: 30926 RVA: 0x001BCE36 File Offset: 0x001BB036
		public CultureInfo Culture { get; set; }

		// Token: 0x170026FF RID: 9983
		// (get) Token: 0x060078CF RID: 30927 RVA: 0x001BCE3F File Offset: 0x001BB03F
		[SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists", Justification = "Design choice.")]
		public Collection<PropertyFilterDescriptionBase> FilterDescriptions
		{
			get
			{
				return this.settings.FilterDescriptions;
			}
		}

		// Token: 0x17002700 RID: 9984
		// (get) Token: 0x060078D0 RID: 30928 RVA: 0x001BCE4C File Offset: 0x001BB04C
		[SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists", Justification = "Design choice.")]
		public Collection<PropertyGroupDescriptionBase> RowGroupDescriptions
		{
			get
			{
				return this.settings.RowGroupDescriptions;
			}
		}

		// Token: 0x17002701 RID: 9985
		// (get) Token: 0x060078D1 RID: 30929 RVA: 0x001BCE59 File Offset: 0x001BB059
		[SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists", Justification = "Design choice.")]
		public Collection<PropertyGroupDescriptionBase> ColumnGroupDescriptions
		{
			get
			{
				return this.settings.ColumnGroupDescriptions;
			}
		}

		// Token: 0x17002702 RID: 9986
		// (get) Token: 0x060078D2 RID: 30930 RVA: 0x001BCE66 File Offset: 0x001BB066
		[SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists", Justification = "Design choice.")]
		public Collection<LocalAggregateDescription> AggregateDescriptions
		{
			get
			{
				return this.settings.AggregateDescriptions;
			}
		}

		// Token: 0x17002703 RID: 9987
		// (get) Token: 0x060078D3 RID: 30931 RVA: 0x001BCE73 File Offset: 0x001BB073
		[SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists", Justification = "Design choice.")]
		public Collection<CalculatedField> CalculatedFields
		{
			get
			{
				return this.calculatedFields;
			}
		}

		// Token: 0x17002704 RID: 9988
		// (get) Token: 0x060078D4 RID: 30932 RVA: 0x001BCE7B File Offset: 0x001BB07B
		// (set) Token: 0x060078D5 RID: 30933 RVA: 0x001BCE83 File Offset: 0x001BB083
		internal IDataSourceView DataView { get; private set; }

		// Token: 0x17002705 RID: 9989
		// (get) Token: 0x060078D6 RID: 30934 RVA: 0x001BCE8C File Offset: 0x001BB08C
		protected override IPivotResults Results
		{
			get
			{
				return this.engine;
			}
		}

		// Token: 0x17002706 RID: 9990
		// (get) Token: 0x060078D7 RID: 30935 RVA: 0x001BCE94 File Offset: 0x001BB094
		public override object State
		{
			get
			{
				return this.ItemsSource;
			}
		}

		// Token: 0x17002707 RID: 9991
		// (get) Token: 0x060078D8 RID: 30936 RVA: 0x001BCE9C File Offset: 0x001BB09C
		// (set) Token: 0x060078D9 RID: 30937 RVA: 0x001BCEA4 File Offset: 0x001BB0A4
		public object ItemsSource
		{
			get
			{
				return this.itemsSource;
			}
			set
			{
				if (this.itemsSource != value)
				{
					this.itemsSource = value;
					this.OnItemsSourceChanged(value);
				}
			}
		}

		// Token: 0x060078DA RID: 30938 RVA: 0x001BCEBD File Offset: 0x001BB0BD
		public override void BlockUntilRefreshCompletes()
		{
			this.engine.WaitForParallel();
		}

		// Token: 0x060078DB RID: 30939 RVA: 0x001BCECC File Offset: 0x001BB0CC
		[Obsolete("Not used. Obsoleted after 2013.Q2.SP1")]
		public override void SetAggregateFunctionToAggregateDescription(IAggregateDescription aggregateDescription, object aggregateFunction)
		{
			PropertyAggregateDescriptionBase propertyAggregateDescriptionBase = aggregateDescription as PropertyAggregateDescriptionBase;
			AggregateFunction aggregateFunction2 = aggregateFunction as AggregateFunction;
			if (propertyAggregateDescriptionBase != null && aggregateFunction2 != null)
			{
				propertyAggregateDescriptionBase.AggregateFunction = aggregateFunction2;
			}
		}

		// Token: 0x060078DC RID: 30940 RVA: 0x001BD14C File Offset: 0x001BB34C
		[Obsolete("Not used. Obsoleted after 2013.Q2.SP1")]
		public override IEnumerable<object> GetAggregateFunctionsForAggregateDescription(IAggregateDescription aggregateDescription)
		{
			PropertyAggregateDescriptionBase padb = aggregateDescription as PropertyAggregateDescriptionBase;
			if (padb != null)
			{
				if (padb.FieldInfo != null && PrecisionHelpers.GetPrecision(padb.FieldInfo.DataType) != Precision.Unknown)
				{
					yield return AggregateFunctions.Sum;
					yield return AggregateFunctions.Count;
					yield return AggregateFunctions.Average;
					yield return AggregateFunctions.Max;
					yield return AggregateFunctions.Min;
					yield return AggregateFunctions.Product;
					yield return AggregateFunctions.StdDev;
					yield return AggregateFunctions.StdDevP;
					yield return AggregateFunctions.Var;
					yield return AggregateFunctions.VarP;
				}
				else
				{
					yield return AggregateFunctions.Count;
				}
			}
			yield break;
		}

		// Token: 0x060078DD RID: 30941 RVA: 0x001BD170 File Offset: 0x001BB370
		internal override void OnPivotSettingsChanged(object sender, SettingsChangedEventArgs e)
		{
			base.OnPivotSettingsChanged(sender, e);
			if (base.FieldInfos != null)
			{
				this.InitializeDescriptions();
			}
		}

		// Token: 0x060078DE RID: 30942 RVA: 0x001BD188 File Offset: 0x001BB388
		AggregateDescriptionBase IAggregateDescriptionsGenerator.GenerateAggregateDescription(RequiredField field)
		{
			AggregateDescriptionBase aggregateDescriptionBase;
			if (field.IsCalculated)
			{
				aggregateDescriptionBase = new CalculatedAggregateDescription
				{
					CalculatedFieldName = field.Name
				};
			}
			else
			{
				AggregateFunction aggregateFunction = null;
				if (field != null)
				{
					AggregateFunction aggregateFunction2 = field.AggregateFunction as AggregateFunction;
					if (aggregateFunction2 != null)
					{
						aggregateFunction = (aggregateFunction2.Clone() as AggregateFunction);
					}
				}
				if (aggregateFunction == null)
				{
					aggregateFunction = AggregateFunctions.Sum;
				}
				aggregateDescriptionBase = new PropertyAggregateDescription
				{
					PropertyName = field.Name,
					AggregateFunction = aggregateFunction
				};
			}
			((IInitializeDescription)aggregateDescriptionBase).Initialize(this);
			return aggregateDescriptionBase;
		}

		// Token: 0x060078DF RID: 30943 RVA: 0x001BD208 File Offset: 0x001BB408
		protected override void RefreshOverride()
		{
			if (this.ItemsSource == null)
			{
				return;
			}
			if (base.FieldInfos == null)
			{
				this.refreshRequested = true;
				this.RefreshFieldDescriptions();
				return;
			}
			this.InitializeDescriptions();
			this.GenerateAndExecutePivotEngineRequest();
		}

		// Token: 0x060078E0 RID: 30944 RVA: 0x001BD235 File Offset: 0x001BB435
		protected override IFieldDescriptionProvider CreateFieldDescriptionsProvider()
		{
			return new LocalDataSourceFieldDescriptionsProvider();
		}

		// Token: 0x060078E1 RID: 30945 RVA: 0x001BD23C File Offset: 0x001BB43C
		protected override FilterDescription GetFilterDescriptionForFieldDescriptionCore(IPivotFieldInfo description)
		{
			if (description == null)
			{
				throw new ArgumentNullException("description");
			}
			return new PropertyFilterDescription
			{
				PropertyName = description.Name
			};
		}

		// Token: 0x060078E2 RID: 30946 RVA: 0x001BD26C File Offset: 0x001BB46C
		protected override IAggregateDescription GetAggregateDescriptionForFieldDescriptionCore(IPivotFieldInfo description)
		{
			if (description == null)
			{
				throw new ArgumentNullException("description");
			}
			if (description is CalculatedPivotFieldInfo)
			{
				return new CalculatedAggregateDescription
				{
					CalculatedFieldName = description.Name
				};
			}
			PropertyAggregateDescription propertyAggregateDescription = new PropertyAggregateDescription
			{
				PropertyName = description.Name
			};
			if (!FieldInfoHelper.IsNumericType(description.DataType))
			{
				propertyAggregateDescription.AggregateFunction = AggregateFunctions.Count;
			}
			return propertyAggregateDescription;
		}

		// Token: 0x060078E3 RID: 30947 RVA: 0x001BD2D0 File Offset: 0x001BB4D0
		protected override IGroupDescription GetGroupDescriptionForFieldDescriptionCore(IPivotFieldInfo description)
		{
			if (description == null)
			{
				throw new ArgumentNullException("description");
			}
			if (FieldInfoHelper.IsNumericType(description.DataType))
			{
				return new DoubleGroupDescription
				{
					PropertyName = description.Name,
					Culture = this.Culture
				};
			}
			if (description.GetType() == typeof(DateTimePropertyFieldInfo))
			{
				DateTimePropertyFieldInfo dateTimePropertyFieldInfo = description as DateTimePropertyFieldInfo;
				return new DateTimeGroupDescription
				{
					PropertyName = dateTimePropertyFieldInfo.PropertyName,
					Step = dateTimePropertyFieldInfo.DateTimeStep,
					Culture = this.Culture
				};
			}
			return new PropertyGroupDescription
			{
				PropertyName = description.Name,
				Culture = this.Culture
			};
		}

		// Token: 0x060078E4 RID: 30948 RVA: 0x001BD381 File Offset: 0x001BB581
		private void OnItemsSourceChanged(object newValue)
		{
			if (newValue == null)
			{
				this.engine.Clear();
			}
			base.FieldInfos = null;
			this.DataView = GenericDataSourceView.GetSourceView(this.ItemsSource ?? Enumerable.Empty<object>());
			base.RefreshOrDefer(DataProviderFlags.ResetStatus);
		}

		// Token: 0x060078E5 RID: 30949 RVA: 0x001BD3BC File Offset: 0x001BB5BC
		private void GenerateAndExecutePivotEngineRequest()
		{
			this.OnStatusChanged(new DataProviderStatusChangedEventArgs(base.Status, DataProviderStatus.RetrievingData, false, null));
			ParallelState state = this.GenerateParallelState();
			this.engine.RebuildCubeParallel(state);
			this.refreshRequested = false;
		}

		// Token: 0x060078E6 RID: 30950 RVA: 0x001BD3F8 File Offset: 0x001BB5F8
		private void OnCompleted(object sender, PivotEngineCompletedEventArgs e)
		{
			DataProviderStatus dataProviderStatusFromEngineStatus = DataProviderBase.GetDataProviderStatusFromEngineStatus(e.Status);
			Exception error = e.InnerExceptions.FirstOrDefault<Exception>();
			this.OnStatusChanged(new DataProviderStatusChangedEventArgs(base.Status, dataProviderStatusFromEngineStatus, true, error));
		}

		// Token: 0x060078E7 RID: 30951 RVA: 0x001BD431 File Offset: 0x001BB631
		private void InitializeDescriptions()
		{
			if (base.FieldInfos == null)
			{
				return;
			}
			this.InitializeDescriptionsCollection<PropertyGroupDescriptionBase>(this.RowGroupDescriptions);
			this.InitializeDescriptionsCollection<PropertyGroupDescriptionBase>(this.ColumnGroupDescriptions);
			this.InitializeDescriptionsCollection<LocalAggregateDescription>(this.AggregateDescriptions);
			this.InitializeDescriptionsCollection<PropertyFilterDescriptionBase>(this.FilterDescriptions);
		}

		// Token: 0x060078E8 RID: 30952 RVA: 0x001BD46C File Offset: 0x001BB66C
		private void InitializeDescriptionsCollection<T>(Collection<T> collection) where T : DescriptionBase
		{
			foreach (T t in collection.ToList<T>())
			{
				if (!this.InitializeDescription(t as IInitializeDescription))
				{
					collection.Remove(t);
				}
			}
		}

		// Token: 0x060078E9 RID: 30953 RVA: 0x001BD4D8 File Offset: 0x001BB6D8
		private bool InitializeDescription(IInitializeDescription description)
		{
			if (description == null)
			{
				return false;
			}
			description.Initialize(this);
			return description.Initialized;
		}

		// Token: 0x060078EA RID: 30954 RVA: 0x001BD4EC File Offset: 0x001BB6EC
		private void RefreshFieldDescriptions()
		{
			if (!Designer.IsInDesignMode)
			{
				base.FieldInfos = null;
				bool flag = base.FieldDescriptionsProvider != null && !base.FieldDescriptionsProvider.IsBusy;
				if (flag && this.ItemsSource != null)
				{
					this.OnStatusChanged(new DataProviderStatusChangedEventArgs(base.Status, DataProviderStatus.Initializing, false, null));
					base.FieldDescriptionsProvider.GetDescriptionsDataAsyncCompleted += this.FieldDescriptionsProvider_GetDescriptionsDataAsyncCompleted;
					base.FieldDescriptionsProvider.GetDescriptionsDataAsync(this);
				}
			}
		}

		// Token: 0x060078EB RID: 30955 RVA: 0x001BD564 File Offset: 0x001BB764
		[SuppressMessage("Microsoft.Usage", "CA2201:DoNotRaiseReservedExceptionTypes", Justification = "Design choice.")]
		private void FieldDescriptionsProvider_GetDescriptionsDataAsyncCompleted(object sender, GetDescriptionsDataCompletedEventArgs e)
		{
			IFieldDescriptionProvider fieldDescriptionProvider = sender as IFieldDescriptionProvider;
			this.StopListeningForGetDescriptionsData(fieldDescriptionProvider);
			LocalDataSourceProvider localDataSourceProvider = e.State as LocalDataSourceProvider;
			if (base.FieldDescriptionsProvider != fieldDescriptionProvider || this != localDataSourceProvider || this.ItemsSource != localDataSourceProvider.ItemsSource)
			{
				return;
			}
			if (e.Error != null)
			{
				base.UpdateStatus(DataProviderStatus.Uninitialized, false, e.Error);
				return;
			}
			base.FieldInfos = e.DescriptionsData;
			this.InitializeDescriptions();
			this.OnStatusChanged(new DataProviderStatusChangedEventArgs(base.Status, DataProviderStatus.Ready, false, null));
			if (this.refreshRequested)
			{
				base.Refresh();
			}
		}

		// Token: 0x060078EC RID: 30956 RVA: 0x001BD5F2 File Offset: 0x001BB7F2
		private void StopListeningForGetDescriptionsData(IFieldDescriptionProvider filedDescriptionProvider)
		{
			if (filedDescriptionProvider != null)
			{
				filedDescriptionProvider.GetDescriptionsDataAsyncCompleted -= this.FieldDescriptionsProvider_GetDescriptionsDataAsyncCompleted;
			}
		}

		// Token: 0x060078ED RID: 30957 RVA: 0x001BD648 File Offset: 0x001BB848
		private ParallelState GenerateParallelState()
		{
			List<PropertyGroupDescriptionBase> source = (from l in this.RowGroupDescriptions
			select (PropertyGroupDescriptionBase)l.Clone()).ToList<PropertyGroupDescriptionBase>();
			List<PropertyGroupDescriptionBase> source2 = (from l in this.ColumnGroupDescriptions
			select (PropertyGroupDescriptionBase)l.Clone()).ToList<PropertyGroupDescriptionBase>();
			List<PropertyFilterDescriptionBase> source3 = (from l in this.FilterDescriptions
			select (PropertyFilterDescriptionBase)l.Clone()).ToList<PropertyFilterDescriptionBase>();
			RequiredAggregateDescriptionsGenerator requiredAggregateDescriptionsGenerator = new RequiredAggregateDescriptionsGenerator(this);
			List<AggregateDescriptionBase> list = (from l in this.AggregateDescriptions
			select (AggregateDescriptionBase)l.Clone()).ToList<AggregateDescriptionBase>();
			AggregateDescriptionInfo[] aggregateDescriptionInfos = requiredAggregateDescriptionsGenerator.AddRequiredAggregateDescriptions(list);
			List<LocalAggregateDescription> source4 = (from l in list
			select (LocalAggregateDescription)l).ToList<LocalAggregateDescription>();
			ReadOnlyList<PropertyGroupDescriptionBase, PropertyGroupDescriptionBase> rowGroupDescriptions = new ReadOnlyList<PropertyGroupDescriptionBase, PropertyGroupDescriptionBase>(source);
			ReadOnlyList<PropertyGroupDescriptionBase, PropertyGroupDescriptionBase> columnGroupDescriptions = new ReadOnlyList<PropertyGroupDescriptionBase, PropertyGroupDescriptionBase>(source2);
			ReadOnlyList<LocalAggregateDescription, LocalAggregateDescription> aggregateDescriptions = new ReadOnlyList<LocalAggregateDescription, LocalAggregateDescription>(source4);
			ReadOnlyList<PropertyFilterDescriptionBase, PropertyFilterDescriptionBase> filterDescriptions = new ReadOnlyList<PropertyFilterDescriptionBase, PropertyFilterDescriptionBase>(source3);
			LocalDataSourceProvider.LocalSourceValueProvider valueProvider = new LocalDataSourceProvider.LocalSourceValueProvider
			{
				RowGroupDescriptions = rowGroupDescriptions,
				ColumnGroupDescriptions = columnGroupDescriptions,
				AggregateDescriptions = aggregateDescriptions,
				FilterDescriptions = filterDescriptions
			};
			return new ParallelState
			{
				MaxDegreeOfParallelism = Environment.ProcessorCount,
				TaskScheduler = TaskScheduler.Default,
				RowGroupDescriptions = rowGroupDescriptions,
				ColumnGroupDescriptions = columnGroupDescriptions,
				AggregateDescriptions = aggregateDescriptions,
				FilterDescriptions = filterDescriptions,
				Culture = this.Culture,
				ValueProvider = valueProvider,
				ItemsSource = this.DataView,
				AggregateDescriptionInfos = aggregateDescriptionInfos,
				AggregateDescriptionCount = this.AggregateDescriptions.Count
			};
		}

		// Token: 0x060078EE RID: 30958 RVA: 0x001BD824 File Offset: 0x001BBA24
		private void SettingsDescriptionAdded(object sender, PivotSettingsDescriptionAddedEventArgs e)
		{
			IInitializeDescription initializeDescription = e.Description as IInitializeDescription;
			bool flag = base.Status == DataProviderStatus.Ready;
			if (initializeDescription == null || !flag)
			{
				return;
			}
			this.InitializeDescription(initializeDescription);
		}

		// Token: 0x04002103 RID: 8451
		private bool refreshRequested;

		// Token: 0x04002104 RID: 8452
		private object itemsSource;

		// Token: 0x04002105 RID: 8453
		private IPivotEngine engine;

		// Token: 0x04002106 RID: 8454
		private Collection<CalculatedField> calculatedFields = new Collection<CalculatedField>();

		// Token: 0x04002107 RID: 8455
		private PivotSettings<PropertyFilterDescriptionBase, PropertyGroupDescriptionBase, LocalAggregateDescription> settings;

		// Token: 0x02000C93 RID: 3219
		private class LocalSourceValueProvider : IValueProvider
		{
			// Token: 0x17002708 RID: 9992
			// (get) Token: 0x060078FE RID: 30974 RVA: 0x001BD856 File Offset: 0x001BBA56
			// (set) Token: 0x060078FF RID: 30975 RVA: 0x001BD85E File Offset: 0x001BBA5E
			public IReadOnlyList<PropertyGroupDescriptionBase> RowGroupDescriptions { get; set; }

			// Token: 0x17002709 RID: 9993
			// (get) Token: 0x06007900 RID: 30976 RVA: 0x001BD867 File Offset: 0x001BBA67
			// (set) Token: 0x06007901 RID: 30977 RVA: 0x001BD86F File Offset: 0x001BBA6F
			public IReadOnlyList<PropertyGroupDescriptionBase> ColumnGroupDescriptions { get; set; }

			// Token: 0x1700270A RID: 9994
			// (get) Token: 0x06007902 RID: 30978 RVA: 0x001BD878 File Offset: 0x001BBA78
			// (set) Token: 0x06007903 RID: 30979 RVA: 0x001BD880 File Offset: 0x001BBA80
			public IReadOnlyList<LocalAggregateDescription> AggregateDescriptions { get; set; }

			// Token: 0x1700270B RID: 9995
			// (get) Token: 0x06007904 RID: 30980 RVA: 0x001BD889 File Offset: 0x001BBA89
			// (set) Token: 0x06007905 RID: 30981 RVA: 0x001BD891 File Offset: 0x001BBA91
			public IReadOnlyList<PropertyFilterDescriptionBase> FilterDescriptions { get; set; }

			// Token: 0x06007906 RID: 30982 RVA: 0x001BD9C0 File Offset: 0x001BBBC0
			IEnumerable IValueProvider.GetRowGroupNames(object item)
			{
				for (int level = 0; level < this.RowGroupDescriptions.Count; level++)
				{
					yield return this.RowGroupDescriptions[level].GroupNameFromItem(item, level);
				}
				yield break;
			}

			// Token: 0x06007907 RID: 30983 RVA: 0x001BDB08 File Offset: 0x001BBD08
			IEnumerable IValueProvider.GetColumnGroupNames(object item)
			{
				for (int level = 0; level < this.ColumnGroupDescriptions.Count; level++)
				{
					yield return this.ColumnGroupDescriptions[level].GroupNameFromItem(item, level);
				}
				yield break;
			}

			// Token: 0x06007908 RID: 30984 RVA: 0x001BDB2C File Offset: 0x001BBD2C
			object IValueProvider.GetAggregateValue(int index, object item)
			{
				return this.AggregateDescriptions[index].GetValueForItem(item);
			}

			// Token: 0x06007909 RID: 30985 RVA: 0x001BDB40 File Offset: 0x001BBD40
			AggregateValue IValueProvider.CreateAggregateValue(int index, bool hasCalculatedGroups)
			{
				PropertyAggregateDescriptionBase propertyAggregateDescriptionBase = this.AggregateDescriptions[index] as PropertyAggregateDescriptionBase;
				if (propertyAggregateDescriptionBase != null)
				{
					Type dataType = (propertyAggregateDescriptionBase.FieldInfo == null) ? null : propertyAggregateDescriptionBase.FieldInfo.DataType;
					LocalDataSourceProvider.LocalSourceValueProvider.AggregateContext context = new LocalDataSourceProvider.LocalSourceValueProvider.AggregateContext
					{
						DataType = dataType,
						HasCalculatedGroups = hasCalculatedGroups
					};
					AggregateValue aggregateValue = propertyAggregateDescriptionBase.AggregateFunction.CreateAggregate(context);
					aggregateValue.IgnoreNullValues = propertyAggregateDescriptionBase.IgnoreNullValues;
					return aggregateValue;
				}
				return null;
			}

			// Token: 0x0600790A RID: 30986 RVA: 0x001BDBAF File Offset: 0x001BBDAF
			string IValueProvider.GetAggregateStringFormat(int index)
			{
				return this.AggregateDescriptions[index].GetEffectiveFormat();
			}

			// Token: 0x0600790B RID: 30987 RVA: 0x001BDBC4 File Offset: 0x001BBDC4
			int IValueProvider.GetFiltersCount()
			{
				return (this.FilterDescriptions == null) ? 0 : this.FilterDescriptions.Count;
			}

			// Token: 0x0600790C RID: 30988 RVA: 0x001BDBEC File Offset: 0x001BBDEC
			object[] IValueProvider.GetFilterItems(object fact)
			{
				int num = (this.FilterDescriptions == null) ? 0 : this.FilterDescriptions.Count;
				if (num == 0)
				{
					return null;
				}
				object[] array = new object[num];
				for (int i = 0; i < num; i++)
				{
					PropertyFilterDescriptionBase propertyFilterDescriptionBase = this.FilterDescriptions[i];
					array[i] = propertyFilterDescriptionBase.GetFilterItem(fact);
				}
				return array;
			}

			// Token: 0x0600790D RID: 30989 RVA: 0x001BDC40 File Offset: 0x001BBE40
			bool IValueProvider.PassesFilter(object[] items)
			{
				int num = (this.FilterDescriptions == null) ? 0 : this.FilterDescriptions.Count;
				int num2 = (items == null) ? 0 : items.Length;
				if (num2 != num)
				{
					throw new ArgumentException("Length should be the same as of the FilterDescriptions.", "items");
				}
				for (int i = 0; i < num; i++)
				{
					PropertyFilterDescriptionBase propertyFilterDescriptionBase = this.FilterDescriptions[i];
					object value = items[i];
					if (!propertyFilterDescriptionBase.PassesFilter(value))
					{
						return false;
					}
				}
				return true;
			}

			// Token: 0x0600790E RID: 30990 RVA: 0x001BDCAD File Offset: 0x001BBEAD
			IEnumerable<CalculatedItem> IValueProvider.GetRowCalculatedItems(int level)
			{
				return this.RowGroupDescriptions[level].CalculatedItems;
			}

			// Token: 0x0600790F RID: 30991 RVA: 0x001BDCC0 File Offset: 0x001BBEC0
			IEnumerable<CalculatedItem> IValueProvider.GetColumnCalculatedItems(int level)
			{
				return this.ColumnGroupDescriptions[level].CalculatedItems;
			}

			// Token: 0x02000C94 RID: 3220
			private class AggregateContext : IAggregateContext
			{
				// Token: 0x1700270C RID: 9996
				// (get) Token: 0x06007911 RID: 30993 RVA: 0x001BDCDB File Offset: 0x001BBEDB
				// (set) Token: 0x06007912 RID: 30994 RVA: 0x001BDCE3 File Offset: 0x001BBEE3
				public Type DataType { get; internal set; }

				// Token: 0x1700270D RID: 9997
				// (get) Token: 0x06007913 RID: 30995 RVA: 0x001BDCEC File Offset: 0x001BBEEC
				// (set) Token: 0x06007914 RID: 30996 RVA: 0x001BDCF4 File Offset: 0x001BBEF4
				public bool HasCalculatedGroups { get; internal set; }
			}
		}
	}
}
