using System;
using System.Collections.Generic;
using System.ComponentModel;
using Telerik.Web.UI.PivotGrid.Core.Design;
using Telerik.Web.UI.PivotGrid.Core.Engine;
using Telerik.Web.UI.PivotGrid.Core.Fields;

namespace Telerik.Web.UI.PivotGrid.Core.DataProviders
{
	// Token: 0x02000C8B RID: 3211
	public abstract class DataProviderBase : IDataProvider, INotifyPropertyChanged, ISupportInitialize
	{
		// Token: 0x06007868 RID: 30824 RVA: 0x001BC614 File Offset: 0x001BA814
		internal DataProviderBase(IPivotSettings settings, IFieldDescriptionProvider fieldInfoProvider)
		{
			this.Settings = settings;
			this.Settings.SettingsChanged += this.OnPivotSettingsChanged;
			this.Settings.PropertyChanged += this.OnPivotSettingsPropertyChanged;
			this.Status = DataProviderStatus.Uninitialized;
			this.fieldDescriptionsProvider = fieldInfoProvider;
			this.ExecutionStrategy = GlobalOptions.PreferredExecutionStrategy;
		}

		// Token: 0x06007869 RID: 30825 RVA: 0x001BC676 File Offset: 0x001BA876
		internal DataProviderBase(IPivotSettings settings) : this(settings, null)
		{
		}

		// Token: 0x14000123 RID: 291
		// (add) Token: 0x0600786A RID: 30826 RVA: 0x001BC680 File Offset: 0x001BA880
		// (remove) Token: 0x0600786B RID: 30827 RVA: 0x001BC6B8 File Offset: 0x001BA8B8
		public event EventHandler<DataProviderStatusChangedEventArgs> StatusChanged;

		// Token: 0x14000124 RID: 292
		// (add) Token: 0x0600786C RID: 30828 RVA: 0x001BC6F0 File Offset: 0x001BA8F0
		// (remove) Token: 0x0600786D RID: 30829 RVA: 0x001BC728 File Offset: 0x001BA928
		public event PropertyChangedEventHandler PropertyChanged;

		// Token: 0x14000125 RID: 293
		// (add) Token: 0x0600786E RID: 30830 RVA: 0x001BC760 File Offset: 0x001BA960
		// (remove) Token: 0x0600786F RID: 30831 RVA: 0x001BC798 File Offset: 0x001BA998
		public event EventHandler<PrepareDescriptionForFieldEventArgs> PrepareDescriptionForField;

		// Token: 0x14000126 RID: 294
		// (add) Token: 0x06007870 RID: 30832 RVA: 0x001BC7CD File Offset: 0x001BA9CD
		// (remove) Token: 0x06007871 RID: 30833 RVA: 0x001BC7D6 File Offset: 0x001BA9D6
		event EventHandler<DataProviderStatusChangedEventArgs> IDataProvider.StatusChanged
		{
			add
			{
				this.StatusChanged += value;
			}
			remove
			{
				this.StatusChanged -= value;
			}
		}

		// Token: 0x170026E3 RID: 9955
		// (get) Token: 0x06007872 RID: 30834 RVA: 0x001BC7DF File Offset: 0x001BA9DF
		// (set) Token: 0x06007873 RID: 30835 RVA: 0x001BC7E7 File Offset: 0x001BA9E7
		internal OperationExecutionStrategy ExecutionStrategy { get; set; }

		// Token: 0x170026E4 RID: 9956
		// (get) Token: 0x06007874 RID: 30836 RVA: 0x001BC7F0 File Offset: 0x001BA9F0
		// (set) Token: 0x06007875 RID: 30837 RVA: 0x001BC7F8 File Offset: 0x001BA9F8
		public IFieldInfoData FieldInfos { get; protected set; }

		// Token: 0x170026E5 RID: 9957
		// (get) Token: 0x06007876 RID: 30838 RVA: 0x001BC801 File Offset: 0x001BAA01
		// (set) Token: 0x06007877 RID: 30839 RVA: 0x001BC809 File Offset: 0x001BAA09
		public bool DeferUpdates
		{
			get
			{
				return this.deferUpdates;
			}
			set
			{
				if (this.deferUpdates != value)
				{
					this.deferUpdates = value;
					this.OnPropertyChanged("DeferUpdates");
				}
			}
		}

		// Token: 0x170026E6 RID: 9958
		// (get) Token: 0x06007878 RID: 30840 RVA: 0x001BC826 File Offset: 0x001BAA26
		// (set) Token: 0x06007879 RID: 30841 RVA: 0x001BC82E File Offset: 0x001BAA2E
		public DataProviderStatus Status
		{
			get
			{
				return this.status;
			}
			private set
			{
				if (this.status != value)
				{
					this.status = value;
					this.OnPropertyChanged("Status");
				}
			}
		}

		// Token: 0x170026E7 RID: 9959
		// (get) Token: 0x0600787A RID: 30842 RVA: 0x001BC84B File Offset: 0x001BAA4B
		// (set) Token: 0x0600787B RID: 30843 RVA: 0x001BC858 File Offset: 0x001BAA58
		public PivotAxis AggregatesPosition
		{
			get
			{
				return this.Settings.AggregatesPosition;
			}
			set
			{
				this.Settings.AggregatesPosition = value;
			}
		}

		// Token: 0x170026E8 RID: 9960
		// (get) Token: 0x0600787C RID: 30844 RVA: 0x001BC866 File Offset: 0x001BAA66
		// (set) Token: 0x0600787D RID: 30845 RVA: 0x001BC873 File Offset: 0x001BAA73
		public int AggregatesLevel
		{
			get
			{
				return this.Settings.AggregatesLevel;
			}
			set
			{
				this.Settings.AggregatesLevel = value;
			}
		}

		// Token: 0x170026E9 RID: 9961
		// (get) Token: 0x0600787E RID: 30846 RVA: 0x001BC881 File Offset: 0x001BAA81
		// (set) Token: 0x0600787F RID: 30847 RVA: 0x001BC8A0 File Offset: 0x001BAAA0
		public IFieldDescriptionProvider FieldDescriptionsProvider
		{
			get
			{
				if (this.fieldDescriptionsProvider == null)
				{
					this.fieldDescriptionsProvider = this.CreateFieldDescriptionsProvider();
				}
				return this.fieldDescriptionsProvider;
			}
			set
			{
				IFieldDescriptionProvider oldProvider = this.fieldDescriptionsProvider;
				this.fieldDescriptionsProvider = value;
				this.OnFieldDescriptionsProviderChanged(oldProvider, value);
			}
		}

		// Token: 0x170026EA RID: 9962
		// (get) Token: 0x06007880 RID: 30848 RVA: 0x001BC8C3 File Offset: 0x001BAAC3
		public bool HasPendingChanges
		{
			get
			{
				return this.hasPendingChanges;
			}
		}

		// Token: 0x170026EB RID: 9963
		// (get) Token: 0x06007881 RID: 30849
		protected abstract IPivotResults Results { get; }

		// Token: 0x170026EC RID: 9964
		// (get) Token: 0x06007882 RID: 30850 RVA: 0x001BC8CB File Offset: 0x001BAACB
		// (set) Token: 0x06007883 RID: 30851 RVA: 0x001BC8D3 File Offset: 0x001BAAD3
		private protected IPivotSettings Settings { protected get; private set; }

		// Token: 0x170026ED RID: 9965
		// (get) Token: 0x06007884 RID: 30852
		public abstract object State { get; }

		// Token: 0x170026EE RID: 9966
		// (get) Token: 0x06007885 RID: 30853 RVA: 0x001BC8DC File Offset: 0x001BAADC
		private bool IsChanging
		{
			get
			{
				return this.isInitializing || this.deferLevel > 0;
			}
		}

		// Token: 0x170026EF RID: 9967
		// (get) Token: 0x06007886 RID: 30854 RVA: 0x001BC8F1 File Offset: 0x001BAAF1
		DataProviderStatus IDataProvider.Status
		{
			get
			{
				return this.status;
			}
		}

		// Token: 0x06007887 RID: 30855 RVA: 0x001BC8F9 File Offset: 0x001BAAF9
		void IDataProvider.Refresh()
		{
			this.Refresh();
		}

		// Token: 0x170026F0 RID: 9968
		// (get) Token: 0x06007888 RID: 30856 RVA: 0x001BC901 File Offset: 0x001BAB01
		IPivotResults IDataProvider.Results
		{
			get
			{
				return this.Results;
			}
		}

		// Token: 0x170026F1 RID: 9969
		// (get) Token: 0x06007889 RID: 30857 RVA: 0x001BC909 File Offset: 0x001BAB09
		IPivotSettings IDataProvider.Settings
		{
			get
			{
				return this.Settings;
			}
		}

		// Token: 0x170026F2 RID: 9970
		// (get) Token: 0x0600788A RID: 30858 RVA: 0x001BC911 File Offset: 0x001BAB11
		// (set) Token: 0x0600788B RID: 30859 RVA: 0x001BC919 File Offset: 0x001BAB19
		PivotAxis IDataProvider.AggregatesPosition
		{
			get
			{
				return this.AggregatesPosition;
			}
			set
			{
				this.AggregatesPosition = value;
			}
		}

		// Token: 0x170026F3 RID: 9971
		// (get) Token: 0x0600788C RID: 30860 RVA: 0x001BC922 File Offset: 0x001BAB22
		// (set) Token: 0x0600788D RID: 30861 RVA: 0x001BC92A File Offset: 0x001BAB2A
		int IDataProvider.AggregatesLevel
		{
			get
			{
				return this.AggregatesLevel;
			}
			set
			{
				this.AggregatesLevel = value;
			}
		}

		// Token: 0x0600788E RID: 30862 RVA: 0x001BC933 File Offset: 0x001BAB33
		internal bool FlagIsSet(DataProviderFlags flags)
		{
			return (this.currentFlags & flags) == flags;
		}

		// Token: 0x0600788F RID: 30863 RVA: 0x001BC940 File Offset: 0x001BAB40
		internal void SetFlag(DataProviderFlags flags)
		{
			this.currentFlags |= flags;
		}

		// Token: 0x06007890 RID: 30864 RVA: 0x001BC950 File Offset: 0x001BAB50
		internal void UnsetFlag(DataProviderFlags flags)
		{
			this.currentFlags &= ~flags;
		}

		// Token: 0x06007891 RID: 30865 RVA: 0x001BC961 File Offset: 0x001BAB61
		public void BeginInit()
		{
			if (this.isInitializing)
			{
				throw new InvalidOperationException("Nested BeginInit is not supported. Use DeferRefresh() instead.");
			}
			this.isInitializing = true;
		}

		// Token: 0x06007892 RID: 30866 RVA: 0x001BC97D File Offset: 0x001BAB7D
		public void EndInit()
		{
			if (!this.isInitializing)
			{
				throw new InvalidOperationException("EndInit without BeginInit is not supported.");
			}
			this.isInitializing = false;
			this.OnInitializationCompleted();
		}

		// Token: 0x06007893 RID: 30867 RVA: 0x001BC9A0 File Offset: 0x001BABA0
		public void Refresh()
		{
			if (Designer.IsInDesignMode)
			{
				return;
			}
			this.CheckAndUpdateFlags();
			try
			{
				this.isRefreshing = true;
				this.RefreshOverride();
			}
			finally
			{
				this.isRefreshing = false;
				this.ClearPendingChanges();
			}
		}

		// Token: 0x06007894 RID: 30868 RVA: 0x001BC9E8 File Offset: 0x001BABE8
		private void CheckAndUpdateFlags()
		{
			if (this.FlagIsSet(DataProviderFlags.ResetStatus))
			{
				this.OnStatusChanged(new DataProviderStatusChangedEventArgs(this.Status, DataProviderStatus.Uninitialized, true, null));
			}
			this.UnsetFlag(DataProviderFlags.All);
		}

		// Token: 0x06007895 RID: 30869
		public abstract void BlockUntilRefreshCompletes();

		// Token: 0x06007896 RID: 30870 RVA: 0x001BCA0E File Offset: 0x001BAC0E
		public IDisposable DeferRefresh()
		{
			this.deferLevel++;
			return new DataProviderBase.DeferHelper(this);
		}

		// Token: 0x06007897 RID: 30871 RVA: 0x001BCA24 File Offset: 0x001BAC24
		internal static DataProviderStatus GetDataProviderStatusFromEngineStatus(PivotEngineStatus engineStatus)
		{
			switch (engineStatus)
			{
			case PivotEngineStatus.Completed:
				return DataProviderStatus.Ready;
			case PivotEngineStatus.InProgress:
				return DataProviderStatus.RetrievingData;
			}
			return DataProviderStatus.Faulted;
		}

		// Token: 0x06007898 RID: 30872 RVA: 0x001BCA52 File Offset: 0x001BAC52
		protected void Invalidate()
		{
			this.SetFlag(DataProviderFlags.NeedsRefresh);
			this.AddPendingChange();
			this.OnEditCompleted();
		}

		// Token: 0x06007899 RID: 30873
		protected abstract void RefreshOverride();

		// Token: 0x0600789A RID: 30874 RVA: 0x001BCA67 File Offset: 0x001BAC67
		protected virtual void OnStatusChanged(DataProviderStatusChangedEventArgs args)
		{
			if (this.IsChanging)
			{
				return;
			}
			if (this.Status == args.NewStatus)
			{
				return;
			}
			this.Status = args.NewStatus;
			if (this.StatusChanged != null)
			{
				this.StatusChanged(this, args);
			}
		}

		// Token: 0x0600789B RID: 30875 RVA: 0x001BCAA2 File Offset: 0x001BACA2
		protected virtual void OnPrepareDescriptionForField(PrepareDescriptionForFieldEventArgs args)
		{
			if (this.PrepareDescriptionForField != null)
			{
				this.PrepareDescriptionForField(this, args);
			}
		}

		// Token: 0x0600789C RID: 30876 RVA: 0x001BCAB9 File Offset: 0x001BACB9
		protected virtual void OnFieldDescriptionsProviderChanged(IFieldDescriptionProvider oldProvider, IFieldDescriptionProvider newProvider)
		{
			this.OnPropertyChanged("FieldDescriptionsProvider");
			this.FieldInfos = null;
			this.RefreshOrDefer(DataProviderFlags.ForceRefresh | DataProviderFlags.ResetStatus);
		}

		// Token: 0x0600789D RID: 30877 RVA: 0x001BCAD4 File Offset: 0x001BACD4
		internal void RefreshOrDefer(DataProviderFlags flags)
		{
			if (this.IsChanging)
			{
				this.SetFlag(DataProviderFlags.NeedsRefresh | flags);
				return;
			}
			this.SetFlag(DataProviderFlags.NeedsRefresh | flags);
			this.Refresh();
		}

		// Token: 0x0600789E RID: 30878
		protected abstract IFieldDescriptionProvider CreateFieldDescriptionsProvider();

		// Token: 0x0600789F RID: 30879 RVA: 0x001BCAF7 File Offset: 0x001BACF7
		protected void OnPropertyChanged(string propertyName)
		{
			if (this.PropertyChanged != null)
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}

		// Token: 0x060078A0 RID: 30880 RVA: 0x001BCB14 File Offset: 0x001BAD14
		private void OnPivotSettingsPropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			if (string.IsNullOrEmpty(e.PropertyName))
			{
				this.OnPropertyChanged(null);
				return;
			}
			string propertyName;
			if ((propertyName = e.PropertyName) != null)
			{
				if (propertyName == "AggregatesPosition")
				{
					this.OnPropertyChanged("AggregatesPosition");
					return;
				}
				if (!(propertyName == "AggregatesLevel"))
				{
					return;
				}
				this.OnPropertyChanged("AggregatesLevel");
			}
		}

		// Token: 0x060078A1 RID: 30881 RVA: 0x001BCB72 File Offset: 0x001BAD72
		internal virtual void OnPivotSettingsChanged(object sender, SettingsChangedEventArgs e)
		{
			this.Invalidate();
		}

		// Token: 0x060078A2 RID: 30882 RVA: 0x001BCB7A File Offset: 0x001BAD7A
		private void AddPendingChange()
		{
			if (!this.hasPendingChanges)
			{
				this.hasPendingChanges = true;
				this.OnPropertyChanged("HasPendingChanges");
			}
		}

		// Token: 0x060078A3 RID: 30883 RVA: 0x001BCB96 File Offset: 0x001BAD96
		private void ClearPendingChanges()
		{
			if (this.hasPendingChanges)
			{
				this.hasPendingChanges = false;
				this.OnPropertyChanged("HasPendingChanges");
			}
		}

		// Token: 0x060078A4 RID: 30884 RVA: 0x001BCBB2 File Offset: 0x001BADB2
		private void OnInitializationCompleted()
		{
			if (this.deferLevel > 0)
			{
				return;
			}
			if (this.HasPendingChanges || this.FlagIsSet(DataProviderFlags.NeedsRefresh))
			{
				this.Refresh();
			}
		}

		// Token: 0x060078A5 RID: 30885 RVA: 0x001BCBD8 File Offset: 0x001BADD8
		private void OnEditCompleted()
		{
			if (this.deferLevel > 0 || this.isInitializing || this.isRefreshing)
			{
				return;
			}
			if ((this.HasPendingChanges || this.FlagIsSet(DataProviderFlags.NeedsRefresh)) && (!this.DeferUpdates || this.FlagIsSet(DataProviderFlags.ForceRefresh)))
			{
				this.Refresh();
			}
		}

		// Token: 0x060078A6 RID: 30886 RVA: 0x001BCC27 File Offset: 0x001BAE27
		private void EndDefer()
		{
			this.deferLevel--;
			this.OnEditCompleted();
		}

		// Token: 0x060078A7 RID: 30887
		protected abstract IAggregateDescription GetAggregateDescriptionForFieldDescriptionCore(IPivotFieldInfo description);

		// Token: 0x060078A8 RID: 30888
		protected abstract IGroupDescription GetGroupDescriptionForFieldDescriptionCore(IPivotFieldInfo description);

		// Token: 0x060078A9 RID: 30889
		protected abstract FilterDescription GetFilterDescriptionForFieldDescriptionCore(IPivotFieldInfo description);

		// Token: 0x060078AA RID: 30890 RVA: 0x001BCC40 File Offset: 0x001BAE40
		public IAggregateDescription GetAggregateDescriptionForFieldDescription(IPivotFieldInfo info)
		{
			IAggregateDescription aggregateDescriptionForFieldDescriptionCore = this.GetAggregateDescriptionForFieldDescriptionCore(info);
			PrepareDescriptionForFieldEventArgs prepareDescriptionForFieldEventArgs = new PrepareDescriptionForFieldEventArgs(info, aggregateDescriptionForFieldDescriptionCore, DataProviderDescriptionType.Aggregate);
			this.OnPrepareDescriptionForField(prepareDescriptionForFieldEventArgs);
			return prepareDescriptionForFieldEventArgs.Description as IAggregateDescription;
		}

		// Token: 0x060078AB RID: 30891 RVA: 0x001BCC70 File Offset: 0x001BAE70
		public IGroupDescription GetGroupDescriptionForFieldDescription(IPivotFieldInfo info)
		{
			IGroupDescription groupDescriptionForFieldDescriptionCore = this.GetGroupDescriptionForFieldDescriptionCore(info);
			PrepareDescriptionForFieldEventArgs prepareDescriptionForFieldEventArgs = new PrepareDescriptionForFieldEventArgs(info, groupDescriptionForFieldDescriptionCore, DataProviderDescriptionType.Group);
			this.OnPrepareDescriptionForField(prepareDescriptionForFieldEventArgs);
			return prepareDescriptionForFieldEventArgs.Description as IGroupDescription;
		}

		// Token: 0x060078AC RID: 30892 RVA: 0x001BCCA0 File Offset: 0x001BAEA0
		public FilterDescription GetFilterDescriptionForFieldDescription(IPivotFieldInfo info)
		{
			FilterDescription filterDescriptionForFieldDescriptionCore = this.GetFilterDescriptionForFieldDescriptionCore(info);
			PrepareDescriptionForFieldEventArgs prepareDescriptionForFieldEventArgs = new PrepareDescriptionForFieldEventArgs(info, filterDescriptionForFieldDescriptionCore, DataProviderDescriptionType.Filter);
			this.OnPrepareDescriptionForField(prepareDescriptionForFieldEventArgs);
			return prepareDescriptionForFieldEventArgs.Description as FilterDescription;
		}

		// Token: 0x060078AD RID: 30893
		[Obsolete("Not used. Obsoleted after 2013.Q2.SP1")]
		public abstract IEnumerable<object> GetAggregateFunctionsForAggregateDescription(IAggregateDescription aggregateDescription);

		// Token: 0x060078AE RID: 30894
		[Obsolete("Not used. Obsoleted after 2013.Q2.SP1")]
		public abstract void SetAggregateFunctionToAggregateDescription(IAggregateDescription aggregateDescription, object aggregateFunction);

		// Token: 0x060078AF RID: 30895 RVA: 0x001BCCD0 File Offset: 0x001BAED0
		internal void UpdateStatus(DataProviderStatus newStatus, bool resultsChanged, Exception error)
		{
			this.OnStatusChanged(new DataProviderStatusChangedEventArgs(this.Status, newStatus, resultsChanged, error));
		}

		// Token: 0x040020E5 RID: 8421
		private bool hasPendingChanges;

		// Token: 0x040020E6 RID: 8422
		private int deferLevel;

		// Token: 0x040020E7 RID: 8423
		private bool isInitializing;

		// Token: 0x040020E8 RID: 8424
		private bool isRefreshing;

		// Token: 0x040020E9 RID: 8425
		private bool deferUpdates;

		// Token: 0x040020EA RID: 8426
		private DataProviderFlags currentFlags;

		// Token: 0x040020EB RID: 8427
		private DataProviderStatus status;

		// Token: 0x040020EC RID: 8428
		private IFieldDescriptionProvider fieldDescriptionsProvider;

		// Token: 0x02000C8C RID: 3212
		private sealed class DeferHelper : IDisposable
		{
			// Token: 0x060078B0 RID: 30896 RVA: 0x001BCCE6 File Offset: 0x001BAEE6
			public DeferHelper(DataProviderBase providerBase)
			{
				this.provider = providerBase;
			}

			// Token: 0x060078B1 RID: 30897 RVA: 0x001BCCF5 File Offset: 0x001BAEF5
			public void Dispose()
			{
				this.Dispose(true);
				GC.SuppressFinalize(this);
			}

			// Token: 0x060078B2 RID: 30898 RVA: 0x001BCD04 File Offset: 0x001BAF04
			public void Dispose(bool disposing)
			{
				if (disposing && this.provider != null)
				{
					this.provider.EndDefer();
					this.provider = null;
				}
				GC.SuppressFinalize(this);
			}

			// Token: 0x040020F3 RID: 8435
			private DataProviderBase provider;
		}
	}
}
