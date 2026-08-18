using System;
using System.Runtime.Serialization;
using Telerik.Web.UI.PivotGrid.Core.Aggregates;
using Telerik.Web.UI.PivotGrid.Core.Fields;

namespace Telerik.Web.UI.PivotGrid.Core
{
	// Token: 0x02000688 RID: 1672
	[DataContract]
	public sealed class CalculatedAggregateDescription : LocalAggregateDescription, IInitializeDescription, IDataFieldDescription, ICalculatedAggregateDescription
	{
		// Token: 0x170013FF RID: 5119
		// (get) Token: 0x06003CD0 RID: 15568 RVA: 0x000C4627 File Offset: 0x000C2827
		// (set) Token: 0x06003CD1 RID: 15569 RVA: 0x000C462F File Offset: 0x000C282F
		[DataMember]
		public string CalculatedFieldName
		{
			get
			{
				return this.calculatedFieldName;
			}
			set
			{
				if (this.calculatedFieldName != value)
				{
					this.calculatedFieldName = value;
					base.OnPropertyChanged("CalculatedFieldName");
					base.NotifySettingsChanged(new SettingsChangedEventArgs());
				}
			}
		}

		// Token: 0x17001400 RID: 5120
		// (get) Token: 0x06003CD2 RID: 15570 RVA: 0x000C465C File Offset: 0x000C285C
		// (set) Token: 0x06003CD3 RID: 15571 RVA: 0x000C4664 File Offset: 0x000C2864
		public CalculatedField CalculatedField { get; private set; }

		// Token: 0x17001401 RID: 5121
		// (get) Token: 0x06003CD4 RID: 15572 RVA: 0x000C466D File Offset: 0x000C286D
		bool IInitializeDescription.Initialized
		{
			get
			{
				return this.CalculatedField != null;
			}
		}

		// Token: 0x06003CD5 RID: 15573 RVA: 0x000C467B File Offset: 0x000C287B
		public override string GetUniqueName()
		{
			return this.CalculatedFieldName;
		}

		// Token: 0x06003CD6 RID: 15574 RVA: 0x000C4684 File Offset: 0x000C2884
		void IInitializeDescription.Initialize(IDataProvider provider)
		{
			if (provider == null || string.IsNullOrEmpty(this.CalculatedFieldName))
			{
				return;
			}
			CalculatedPivotFieldInfo calculatedPivotFieldInfo = provider.FieldInfos.GetFieldDescriptionByMember(this.CalculatedFieldName) as CalculatedPivotFieldInfo;
			if (calculatedPivotFieldInfo == null)
			{
				return;
			}
			base.FieldInfo = calculatedPivotFieldInfo;
			this.CalculatedField = calculatedPivotFieldInfo.CalculatedField;
		}

		// Token: 0x06003CD7 RID: 15575 RVA: 0x000C46D0 File Offset: 0x000C28D0
		Type IDataFieldDescription.GetDataType()
		{
			if (base.FieldInfo != null)
			{
				return base.FieldInfo.DataType;
			}
			return null;
		}

		// Token: 0x06003CD8 RID: 15576 RVA: 0x000C46E7 File Offset: 0x000C28E7
		internal override AggregateFunction GetAggregateFunction()
		{
			return new SumAggregateFunction();
		}

		// Token: 0x06003CD9 RID: 15577 RVA: 0x000C46EE File Offset: 0x000C28EE
		internal override RequiredField GetRequiredField()
		{
			return RequiredField.ForCalculatedField(this.calculatedFieldName);
		}

		// Token: 0x06003CDA RID: 15578 RVA: 0x000C46FB File Offset: 0x000C28FB
		protected internal override object GetValueForItem(object item)
		{
			return item;
		}

		// Token: 0x06003CDB RID: 15579 RVA: 0x000C4700 File Offset: 0x000C2900
		protected override string GetDisplayName()
		{
			string displayName = base.GetDisplayName();
			if (displayName != null)
			{
				return displayName;
			}
			string displayName2 = this.CalculatedFieldName;
			if (base.FieldInfo != null && base.FieldInfo.DisplayName != null)
			{
				displayName2 = base.FieldInfo.DisplayName;
			}
			return displayName2;
		}

		// Token: 0x06003CDC RID: 15580 RVA: 0x000C4742 File Offset: 0x000C2942
		protected override Cloneable CreateInstanceCore()
		{
			return new CalculatedAggregateDescription();
		}

		// Token: 0x06003CDD RID: 15581 RVA: 0x000C474C File Offset: 0x000C294C
		protected sealed override void CloneCore(Cloneable source)
		{
			base.CloneCore(source);
			CalculatedAggregateDescription calculatedAggregateDescription = source as CalculatedAggregateDescription;
			if (calculatedAggregateDescription != null)
			{
				this.CalculatedFieldName = calculatedAggregateDescription.CalculatedFieldName;
				this.CalculatedField = calculatedAggregateDescription.CalculatedField;
			}
		}

		// Token: 0x0400104C RID: 4172
		private string calculatedFieldName;
	}
}
