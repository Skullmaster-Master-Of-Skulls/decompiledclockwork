using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.Serialization;
using Telerik.Web.UI.PivotGrid.Core.Aggregates;
using Telerik.Web.UI.PivotGrid.Core.Fields;

namespace Telerik.Web.UI.PivotGrid.Core
{
	// Token: 0x02000C51 RID: 3153
	[DataContract]
	public abstract class PropertyAggregateDescriptionBase : LocalAggregateDescription, IAggregateFunctionHost, IInitializeDescription, IDataFieldDescription
	{
		// Token: 0x170026BA RID: 9914
		// (get) Token: 0x0600771E RID: 30494 RVA: 0x001BA364 File Offset: 0x001B8564
		// (set) Token: 0x0600771F RID: 30495 RVA: 0x001BA36C File Offset: 0x001B856C
		[DataMember]
		public string PropertyName
		{
			get
			{
				return this.propertyName;
			}
			set
			{
				if (this.propertyName != value)
				{
					this.propertyName = value;
					base.OnPropertyChanged("PropertyName");
					base.OnPropertyChanged("DisplayName");
					base.NotifySettingsChanged(new SettingsChangedEventArgs());
				}
			}
		}

		// Token: 0x170026BB RID: 9915
		// (get) Token: 0x06007720 RID: 30496 RVA: 0x001BA3A4 File Offset: 0x001B85A4
		// (set) Token: 0x06007721 RID: 30497 RVA: 0x001BA3BF File Offset: 0x001B85BF
		[DataMember]
		public AggregateFunction AggregateFunction
		{
			get
			{
				if (this.aggregateFunction == null)
				{
					this.aggregateFunction = new SumAggregateFunction();
				}
				return this.aggregateFunction;
			}
			set
			{
				if (this.aggregateFunction != value)
				{
					base.ChangeSettingsProperty<AggregateFunction>(ref this.aggregateFunction, value);
					base.OnPropertyChanged("AggregateFunction");
					base.OnPropertyChanged("DisplayName");
					base.NotifySettingsChanged(new SettingsChangedEventArgs());
				}
			}
		}

		// Token: 0x170026BC RID: 9916
		// (get) Token: 0x06007722 RID: 30498 RVA: 0x001BA3F8 File Offset: 0x001B85F8
		// (set) Token: 0x06007723 RID: 30499 RVA: 0x001BA400 File Offset: 0x001B8600
		[DataMember]
		public bool IgnoreNullValues
		{
			get
			{
				return this.ignoreNullValues;
			}
			set
			{
				if (this.ignoreNullValues != value)
				{
					this.ignoreNullValues = value;
					base.OnPropertyChanged("IgnoreNullValues");
					base.NotifySettingsChanged(new SettingsChangedEventArgs());
				}
			}
		}

		// Token: 0x170026BD RID: 9917
		// (get) Token: 0x06007724 RID: 30500 RVA: 0x001BA428 File Offset: 0x001B8628
		protected Type DataType
		{
			get
			{
				if (base.FieldInfo != null)
				{
					return base.FieldInfo.DataType;
				}
				return null;
			}
		}

		// Token: 0x170026BE RID: 9918
		// (get) Token: 0x06007725 RID: 30501 RVA: 0x001BA7D8 File Offset: 0x001B89D8
		protected virtual IEnumerable<object> SupportedAggregateFunctions
		{
			get
			{
				if (base.FieldInfo != null)
				{
					IDataProvider dp = base.GetService(typeof(IDataProvider)) as IDataProvider;
					if (dp != null)
					{
						bool hasCalculatedItem = dp.Settings.RowGroupDescriptions.OfType<PropertyGroupDescriptionBase>().Concat(dp.Settings.ColumnGroupDescriptions.OfType<PropertyGroupDescriptionBase>()).Any((PropertyGroupDescriptionBase d) => d.CalculatedItems != null && d.CalculatedItems.Count > 0);
						if (hasCalculatedItem && PrecisionHelpers.GetPrecision(base.FieldInfo.DataType) != Precision.Unknown)
						{
							yield return AggregateFunctions.Sum;
							yield return AggregateFunctions.Count;
							yield return AggregateFunctions.Max;
							yield return AggregateFunctions.Min;
							yield return AggregateFunctions.Product;
							goto IL_2ED;
						}
					}
					if (PrecisionHelpers.GetPrecision(base.FieldInfo.DataType) != Precision.Unknown)
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
						goto IL_2ED;
					}
				}
				yield return AggregateFunctions.Count;
				IL_2ED:
				yield break;
			}
		}

		// Token: 0x06007726 RID: 30502 RVA: 0x001BA7F5 File Offset: 0x001B89F5
		Type IDataFieldDescription.GetDataType()
		{
			return this.DataType;
		}

		// Token: 0x170026BF RID: 9919
		// (get) Token: 0x06007727 RID: 30503 RVA: 0x001BA7FD File Offset: 0x001B89FD
		bool IInitializeDescription.Initialized
		{
			get
			{
				return base.FieldInfo != null;
			}
		}

		// Token: 0x170026C0 RID: 9920
		// (get) Token: 0x06007728 RID: 30504 RVA: 0x001BA80B File Offset: 0x001B8A0B
		// (set) Token: 0x06007729 RID: 30505 RVA: 0x001BA813 File Offset: 0x001B8A13
		object IAggregateFunctionHost.AggregateFunction
		{
			get
			{
				return this.AggregateFunction;
			}
			set
			{
				this.AggregateFunction = (value as AggregateFunction);
			}
		}

		// Token: 0x170026C1 RID: 9921
		// (get) Token: 0x0600772A RID: 30506 RVA: 0x001BA821 File Offset: 0x001B8A21
		IEnumerable<object> IAggregateFunctionHost.SupportedAggregateFunctions
		{
			get
			{
				return this.SupportedAggregateFunctions;
			}
		}

		// Token: 0x0600772B RID: 30507 RVA: 0x001BA829 File Offset: 0x001B8A29
		public override string GetUniqueName()
		{
			return this.PropertyName;
		}

		// Token: 0x0600772C RID: 30508 RVA: 0x001BA831 File Offset: 0x001B8A31
		void IInitializeDescription.Initialize(IDataProvider provider)
		{
			if (provider == null)
			{
				return;
			}
			base.FieldInfo = (provider.FieldInfos.GetFieldDescriptionByMember(this.PropertyName) as PropertyFieldInfo);
		}

		// Token: 0x0600772D RID: 30509 RVA: 0x001BA854 File Offset: 0x001B8A54
		protected internal override object GetValueForItem(object item)
		{
			if (base.FieldInfo == null)
			{
				throw new InvalidOperationException("Member access has not been initialized");
			}
			return base.FieldInfo.GetValue(item);
		}

		// Token: 0x0600772E RID: 30510 RVA: 0x001BA882 File Offset: 0x001B8A82
		internal override AggregateFunction GetAggregateFunction()
		{
			return this.AggregateFunction;
		}

		// Token: 0x0600772F RID: 30511 RVA: 0x001BA88A File Offset: 0x001B8A8A
		internal override RequiredField GetRequiredField()
		{
			return RequiredField.ForProperty(this.propertyName, object.Equals(AggregateFunctions.Sum, this.aggregateFunction) ? null : this.aggregateFunction);
		}

		// Token: 0x06007730 RID: 30512 RVA: 0x001BA8B4 File Offset: 0x001B8AB4
		protected override string GetDisplayName()
		{
			string displayName = base.GetDisplayName();
			if (displayName != null)
			{
				return displayName;
			}
			string displayName2 = this.PropertyName;
			if (base.FieldInfo != null && base.FieldInfo.DisplayName != null)
			{
				displayName2 = base.FieldInfo.DisplayName;
			}
			return PropertyAggregateDescriptionBase.LocalizeDisplayName(this.AggregateFunction, displayName2);
		}

		// Token: 0x06007731 RID: 30513 RVA: 0x001BA904 File Offset: 0x001B8B04
		protected sealed override void CloneCore(Cloneable source)
		{
			this.CloneOverride(source);
			PropertyAggregateDescriptionBase propertyAggregateDescriptionBase = source as PropertyAggregateDescriptionBase;
			if (propertyAggregateDescriptionBase != null)
			{
				this.IgnoreNullValues = propertyAggregateDescriptionBase.IgnoreNullValues;
				this.AggregateFunction = Cloneable.CloneOrDefault<AggregateFunction>(propertyAggregateDescriptionBase.AggregateFunction);
				this.PropertyName = propertyAggregateDescriptionBase.PropertyName;
			}
			base.CloneCore(source);
		}

		// Token: 0x06007732 RID: 30514
		protected abstract void CloneOverride(Cloneable source);

		// Token: 0x06007733 RID: 30515 RVA: 0x001BA954 File Offset: 0x001B8B54
		private static string LocalizeDisplayName(AggregateFunction aggregate, string field)
		{
			string text;
			if (aggregate == null)
			{
				text = Convert.ToString(aggregate, CultureInfo.InvariantCulture);
			}
			else
			{
				text = ((INamed)aggregate).DisplayName;
			}
			string aggregateP0ofP = PivotLocalizationManager.AggregateP0ofP1;
			return string.Format(CultureInfo.InvariantCulture, aggregateP0ofP, new object[]
			{
				text,
				field
			});
		}

		// Token: 0x040020BA RID: 8378
		private string propertyName;

		// Token: 0x040020BB RID: 8379
		private AggregateFunction aggregateFunction;

		// Token: 0x040020BC RID: 8380
		private bool ignoreNullValues;
	}
}
