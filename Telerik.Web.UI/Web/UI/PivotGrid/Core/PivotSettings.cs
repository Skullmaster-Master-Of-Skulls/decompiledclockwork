using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using Telerik.Web.UI.PivotGrid.Core.Fields;

namespace Telerik.Web.UI.PivotGrid.Core
{
	// Token: 0x02000C95 RID: 3221
	internal sealed class PivotSettings<TFilter, TGroup, TAggregate> : SettingsNode, IPivotSettings, ISupportInitialize, INotifyPropertyChanged where TFilter : FilterDescription where TGroup : SettingsNode where TAggregate : SettingsNode
	{
		// Token: 0x06007916 RID: 30998 RVA: 0x001BDD08 File Offset: 0x001BBF08
		internal PivotSettings()
		{
			this.FilterDescriptions = new PivotSettings<TFilter, TGroup, TAggregate>.DescriptionsSettingsList<TFilter>(this);
			this.RowGroupDescriptions = new PivotSettings<TFilter, TGroup, TAggregate>.DescriptionsSettingsList<TGroup>(this);
			this.ColumnGroupDescriptions = new PivotSettings<TFilter, TGroup, TAggregate>.DescriptionsSettingsList<TGroup>(this);
			this.AggregateDescriptions = new PivotSettings<TFilter, TGroup, TAggregate>.DescriptionsSettingsList<TAggregate>(this);
			this.aggregatesLevel = -1;
			this.aggregatesPosition = PivotAxis.Columns;
		}

		// Token: 0x14000129 RID: 297
		// (add) Token: 0x06007917 RID: 30999 RVA: 0x001BDD5C File Offset: 0x001BBF5C
		// (remove) Token: 0x06007918 RID: 31000 RVA: 0x001BDD94 File Offset: 0x001BBF94
		public event EventHandler<EventArgs> DescriptionsChanged;

		// Token: 0x1400012A RID: 298
		// (add) Token: 0x06007919 RID: 31001 RVA: 0x001BDDCC File Offset: 0x001BBFCC
		// (remove) Token: 0x0600791A RID: 31002 RVA: 0x001BDE04 File Offset: 0x001BC004
		public event EventHandler<PivotSettingsDescriptionAddedEventArgs> DescriptionAdded;

		// Token: 0x1700270E RID: 9998
		// (get) Token: 0x0600791B RID: 31003 RVA: 0x001BDE39 File Offset: 0x001BC039
		// (set) Token: 0x0600791C RID: 31004 RVA: 0x001BDE41 File Offset: 0x001BC041
		public SettingsNodeCollection<TFilter> FilterDescriptions { get; private set; }

		// Token: 0x1700270F RID: 9999
		// (get) Token: 0x0600791D RID: 31005 RVA: 0x001BDE4A File Offset: 0x001BC04A
		// (set) Token: 0x0600791E RID: 31006 RVA: 0x001BDE52 File Offset: 0x001BC052
		public SettingsNodeCollection<TGroup> RowGroupDescriptions { get; private set; }

		// Token: 0x17002710 RID: 10000
		// (get) Token: 0x0600791F RID: 31007 RVA: 0x001BDE5B File Offset: 0x001BC05B
		// (set) Token: 0x06007920 RID: 31008 RVA: 0x001BDE63 File Offset: 0x001BC063
		public SettingsNodeCollection<TGroup> ColumnGroupDescriptions { get; private set; }

		// Token: 0x17002711 RID: 10001
		// (get) Token: 0x06007921 RID: 31009 RVA: 0x001BDE6C File Offset: 0x001BC06C
		// (set) Token: 0x06007922 RID: 31010 RVA: 0x001BDE74 File Offset: 0x001BC074
		public SettingsNodeCollection<TAggregate> AggregateDescriptions { get; private set; }

		// Token: 0x17002712 RID: 10002
		// (get) Token: 0x06007923 RID: 31011 RVA: 0x001BDE7D File Offset: 0x001BC07D
		// (set) Token: 0x06007924 RID: 31012 RVA: 0x001BDE85 File Offset: 0x001BC085
		public int AggregatesLevel
		{
			get
			{
				return this.aggregatesLevel;
			}
			set
			{
				if (this.aggregatesLevel != value)
				{
					this.aggregatesLevel = value;
					base.OnPropertyChanged("AggregatesLevel");
					this.NotifyLayoutChanged();
					base.NotifySettingsChanged(new SettingsChangedEventArgs());
				}
			}
		}

		// Token: 0x17002713 RID: 10003
		// (get) Token: 0x06007925 RID: 31013 RVA: 0x001BDEB3 File Offset: 0x001BC0B3
		// (set) Token: 0x06007926 RID: 31014 RVA: 0x001BDEBB File Offset: 0x001BC0BB
		public PivotAxis AggregatesPosition
		{
			get
			{
				return this.aggregatesPosition;
			}
			set
			{
				if (this.aggregatesPosition != value)
				{
					this.aggregatesPosition = value;
					base.OnPropertyChanged("AggregatesPosition");
					this.NotifyLayoutChanged();
					base.NotifySettingsChanged(new SettingsChangedEventArgs());
				}
			}
		}

		// Token: 0x17002714 RID: 10004
		// (get) Token: 0x06007927 RID: 31015 RVA: 0x001BDEE9 File Offset: 0x001BC0E9
		// (set) Token: 0x06007928 RID: 31016 RVA: 0x001BDEF1 File Offset: 0x001BC0F1
		internal IDataProvider DataProvider { get; set; }

		// Token: 0x17002715 RID: 10005
		// (get) Token: 0x06007929 RID: 31017 RVA: 0x001BDEFA File Offset: 0x001BC0FA
		IList IPivotSettings.FilterDescriptions
		{
			get
			{
				return this.FilterDescriptions;
			}
		}

		// Token: 0x17002716 RID: 10006
		// (get) Token: 0x0600792A RID: 31018 RVA: 0x001BDF02 File Offset: 0x001BC102
		IList IPivotSettings.RowGroupDescriptions
		{
			get
			{
				return this.RowGroupDescriptions;
			}
		}

		// Token: 0x17002717 RID: 10007
		// (get) Token: 0x0600792B RID: 31019 RVA: 0x001BDF0A File Offset: 0x001BC10A
		IList IPivotSettings.ColumnGroupDescriptions
		{
			get
			{
				return this.ColumnGroupDescriptions;
			}
		}

		// Token: 0x17002718 RID: 10008
		// (get) Token: 0x0600792C RID: 31020 RVA: 0x001BDF12 File Offset: 0x001BC112
		IList IPivotSettings.AggregateDescriptions
		{
			get
			{
				return this.AggregateDescriptions;
			}
		}

		// Token: 0x0600792D RID: 31021 RVA: 0x001BDF1A File Offset: 0x001BC11A
		internal void NotifyLayoutChanged()
		{
			if (this.DescriptionsChanged != null)
			{
				this.DescriptionsChanged(this, EventArgs.Empty);
			}
		}

		// Token: 0x0600792E RID: 31022 RVA: 0x001BDF35 File Offset: 0x001BC135
		private void OnDescriptionAdded(PivotSettingsDescriptionAddedEventArgs args)
		{
			if (this.DescriptionAdded != null)
			{
				this.DescriptionAdded(this, args);
			}
		}

		// Token: 0x0600792F RID: 31023 RVA: 0x001BDF4C File Offset: 0x001BC14C
		protected override void OnEnteredEditScope()
		{
			base.OnEnteredEditScope();
			this.map = new PivotSettings<TFilter, TGroup, TAggregate>.DescriptionsMap();
			this.map.CaptureInitialState(this);
		}

		// Token: 0x06007930 RID: 31024 RVA: 0x001BDF6B File Offset: 0x001BC16B
		protected override void OnExitingEditScope()
		{
			base.OnExitingEditScope();
			this.map.CaptureFinalState(this);
			this.UpdateIndices(this.map);
			this.map = null;
		}

		// Token: 0x06007931 RID: 31025 RVA: 0x001BDF94 File Offset: 0x001BC194
		private void UpdateIndices(IDescriptionIndexMap updatemap)
		{
			foreach (IDescriptionsReferencing descriptionsReferencing in this.RowGroupDescriptions.OfType<IDescriptionsReferencing>())
			{
				descriptionsReferencing.TrackDescriptions(updatemap);
			}
			foreach (IDescriptionsReferencing descriptionsReferencing2 in this.ColumnGroupDescriptions.OfType<IDescriptionsReferencing>())
			{
				descriptionsReferencing2.TrackDescriptions(updatemap);
			}
			foreach (IDescriptionsReferencing descriptionsReferencing3 in this.FilterDescriptions.OfType<IDescriptionsReferencing>())
			{
				descriptionsReferencing3.TrackDescriptions(updatemap);
			}
			foreach (IDescriptionsReferencing descriptionsReferencing4 in this.AggregateDescriptions.OfType<IDescriptionsReferencing>())
			{
				descriptionsReferencing4.TrackDescriptions(updatemap);
			}
		}

		// Token: 0x06007932 RID: 31026 RVA: 0x001BE0C4 File Offset: 0x001BC2C4
		protected override object GetServiceOverride(Type serviceType)
		{
			if (serviceType.IsAssignableFrom(typeof(IDataProvider)))
			{
				return this.DataProvider;
			}
			return base.GetServiceOverride(serviceType);
		}

		// Token: 0x06007933 RID: 31027 RVA: 0x001BE0E6 File Offset: 0x001BC2E6
		protected override Cloneable CreateInstanceCore()
		{
			return new PivotSettings<TFilter, TGroup, TAggregate>();
		}

		// Token: 0x06007934 RID: 31028 RVA: 0x001BE0F0 File Offset: 0x001BC2F0
		protected override void CloneCore(Cloneable source)
		{
			PivotSettings<TFilter, TGroup, TAggregate> pivotSettings = source as PivotSettings<TFilter, TGroup, TAggregate>;
			if (pivotSettings != null)
			{
				this.FilterDescriptions.CloneItemsFrom(pivotSettings.FilterDescriptions);
				this.RowGroupDescriptions.CloneItemsFrom(pivotSettings.RowGroupDescriptions);
				this.ColumnGroupDescriptions.CloneItemsFrom(pivotSettings.ColumnGroupDescriptions);
				this.AggregateDescriptions.CloneItemsFrom(pivotSettings.AggregateDescriptions);
			}
		}

		// Token: 0x04002115 RID: 8469
		private int aggregatesLevel;

		// Token: 0x04002116 RID: 8470
		private PivotAxis aggregatesPosition;

		// Token: 0x04002117 RID: 8471
		private PivotSettings<TFilter, TGroup, TAggregate>.DescriptionsMap map;

		// Token: 0x02000C96 RID: 3222
		private sealed class DescriptionsMap : IDescriptionIndexMap
		{
			// Token: 0x06007936 RID: 31030 RVA: 0x001BE153 File Offset: 0x001BC353
			public void CaptureInitialState(IPivotSettings settings)
			{
				this.initialState = new PivotSettings<TFilter, TGroup, TAggregate>.DescriptionsMap.State(settings);
			}

			// Token: 0x06007937 RID: 31031 RVA: 0x001BE161 File Offset: 0x001BC361
			public void CaptureFinalState(IPivotSettings settings)
			{
				this.finalState = new PivotSettings<TFilter, TGroup, TAggregate>.DescriptionsMap.State(settings);
			}

			// Token: 0x06007938 RID: 31032 RVA: 0x001BE170 File Offset: 0x001BC370
			public MapResult Map(FieldRoles role, int level)
			{
				FieldRoles fieldRoles = role;
				IList list;
				switch (fieldRoles)
				{
				case FieldRoles.Value:
					list = this.initialState.AggregateDescriptions;
					goto IL_6D;
				case FieldRoles.Row:
					list = this.initialState.RowDescriptions;
					goto IL_6D;
				case FieldRoles.Value | FieldRoles.Row:
					break;
				case FieldRoles.Column:
					list = this.initialState.ColumnDescriptions;
					goto IL_6D;
				default:
					if (fieldRoles == FieldRoles.Filter)
					{
						list = this.initialState.FilterDescriptions;
						goto IL_6D;
					}
					break;
				}
				throw new ArgumentException("Expected Row, Column, Value or Filter value.", "role");
				IL_6D:
				if (level < 0 || level >= list.Count)
				{
					return new MapResult
					{
						Role = FieldRoles.None,
						Level = -1,
						Success = false
					};
				}
				object obj = list[level];
				if (obj == null)
				{
					return new MapResult
					{
						Role = FieldRoles.None,
						Level = -1,
						Success = false
					};
				}
				if (!PivotSettings<TFilter, TGroup, TAggregate>.DescriptionsMap.LookupDescription(FieldRoles.Row, this.finalState.RowDescriptions, obj, ref role, ref level) && !PivotSettings<TFilter, TGroup, TAggregate>.DescriptionsMap.LookupDescription(FieldRoles.Column, this.finalState.ColumnDescriptions, obj, ref role, ref level) && !PivotSettings<TFilter, TGroup, TAggregate>.DescriptionsMap.LookupDescription(FieldRoles.Filter, this.finalState.FilterDescriptions, obj, ref role, ref level) && !PivotSettings<TFilter, TGroup, TAggregate>.DescriptionsMap.LookupDescription(FieldRoles.Value, this.finalState.AggregateDescriptions, obj, ref role, ref level))
				{
					return new MapResult
					{
						Role = FieldRoles.None,
						Level = -1,
						Success = false
					};
				}
				return new MapResult
				{
					Role = role,
					Level = level,
					Success = true
				};
			}

			// Token: 0x06007939 RID: 31033 RVA: 0x001BE2F4 File Offset: 0x001BC4F4
			private static bool LookupDescription(FieldRoles collectionRole, IList descriptionsCollection, object description, ref FieldRoles descriptionRole, ref int descriptionLevel)
			{
				int num = descriptionsCollection.IndexOf(description);
				if (num == -1)
				{
					return false;
				}
				descriptionRole = collectionRole;
				descriptionLevel = num;
				return true;
			}

			// Token: 0x0400211F RID: 8479
			private PivotSettings<TFilter, TGroup, TAggregate>.DescriptionsMap.State initialState;

			// Token: 0x04002120 RID: 8480
			private PivotSettings<TFilter, TGroup, TAggregate>.DescriptionsMap.State finalState;

			// Token: 0x02000C97 RID: 3223
			private class State
			{
				// Token: 0x0600793A RID: 31034 RVA: 0x001BE318 File Offset: 0x001BC518
				public State(IPivotSettings settings)
				{
					this.RowDescriptions = PivotSettings<TFilter, TGroup, TAggregate>.DescriptionsMap.State.CloneToCollection(settings.RowGroupDescriptions);
					this.ColumnDescriptions = PivotSettings<TFilter, TGroup, TAggregate>.DescriptionsMap.State.CloneToCollection(settings.ColumnGroupDescriptions);
					this.AggregateDescriptions = PivotSettings<TFilter, TGroup, TAggregate>.DescriptionsMap.State.CloneToCollection(settings.AggregateDescriptions);
					this.FilterDescriptions = PivotSettings<TFilter, TGroup, TAggregate>.DescriptionsMap.State.CloneToCollection(settings.FilterDescriptions);
				}

				// Token: 0x17002719 RID: 10009
				// (get) Token: 0x0600793B RID: 31035 RVA: 0x001BE36F File Offset: 0x001BC56F
				// (set) Token: 0x0600793C RID: 31036 RVA: 0x001BE377 File Offset: 0x001BC577
				public IList RowDescriptions { get; private set; }

				// Token: 0x1700271A RID: 10010
				// (get) Token: 0x0600793D RID: 31037 RVA: 0x001BE380 File Offset: 0x001BC580
				// (set) Token: 0x0600793E RID: 31038 RVA: 0x001BE388 File Offset: 0x001BC588
				public IList ColumnDescriptions { get; private set; }

				// Token: 0x1700271B RID: 10011
				// (get) Token: 0x0600793F RID: 31039 RVA: 0x001BE391 File Offset: 0x001BC591
				// (set) Token: 0x06007940 RID: 31040 RVA: 0x001BE399 File Offset: 0x001BC599
				public IList AggregateDescriptions { get; private set; }

				// Token: 0x1700271C RID: 10012
				// (get) Token: 0x06007941 RID: 31041 RVA: 0x001BE3A2 File Offset: 0x001BC5A2
				// (set) Token: 0x06007942 RID: 31042 RVA: 0x001BE3AA File Offset: 0x001BC5AA
				public IList FilterDescriptions { get; private set; }

				// Token: 0x06007943 RID: 31043 RVA: 0x001BE3B3 File Offset: 0x001BC5B3
				private static ReadOnlyCollection<object> CloneToCollection(IList descriptions)
				{
					return new ReadOnlyCollection<object>(descriptions.OfType<object>().ToList<object>());
				}
			}
		}

		// Token: 0x02000C99 RID: 3225
		private sealed class DescriptionsSettingsList<T> : SettingsNodeCollection<T> where T : SettingsNode
		{
			// Token: 0x0600794D RID: 31053 RVA: 0x001BE526 File Offset: 0x001BC726
			public DescriptionsSettingsList(PivotSettings<TFilter, TGroup, TAggregate> parent) : base(parent)
			{
				this.parent = parent;
			}

			// Token: 0x0600794E RID: 31054 RVA: 0x001BE536 File Offset: 0x001BC736
			protected override void InsertItem(int index, T item)
			{
				base.InsertItem(index, item);
				this.parent.OnDescriptionAdded(new PivotSettingsDescriptionAddedEventArgs(item));
				base.NotifyChange(new SettingsChangedEventArgs());
				this.parent.NotifyLayoutChanged();
			}

			// Token: 0x0600794F RID: 31055 RVA: 0x001BE56C File Offset: 0x001BC76C
			protected override void SetItem(int index, T item)
			{
				base.SetItem(index, item);
				this.parent.OnDescriptionAdded(new PivotSettingsDescriptionAddedEventArgs(item));
				base.NotifyChange(new SettingsChangedEventArgs());
				this.parent.NotifyLayoutChanged();
			}

			// Token: 0x06007950 RID: 31056 RVA: 0x001BE5A2 File Offset: 0x001BC7A2
			protected override void RemoveItem(int index)
			{
				base.RemoveItem(index);
				base.NotifyChange(new SettingsChangedEventArgs());
				this.parent.NotifyLayoutChanged();
			}

			// Token: 0x06007951 RID: 31057 RVA: 0x001BE5C1 File Offset: 0x001BC7C1
			protected override void ClearItems()
			{
				base.ClearItems();
				base.NotifyChange(new SettingsChangedEventArgs());
				this.parent.NotifyLayoutChanged();
			}

			// Token: 0x04002126 RID: 8486
			private PivotSettings<TFilter, TGroup, TAggregate> parent;
		}
	}
}
