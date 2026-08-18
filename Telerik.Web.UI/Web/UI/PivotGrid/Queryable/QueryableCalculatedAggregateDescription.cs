using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;
using Telerik.Web.UI.PivotGrid.Core;
using Telerik.Web.UI.PivotGrid.Core.Fields;

namespace Telerik.Web.UI.PivotGrid.Queryable
{
	// Token: 0x02000728 RID: 1832
	[DataContract]
	public sealed class QueryableCalculatedAggregateDescription : QueryableAggregateDescriptionBase, IInitializeDescription, IDataFieldDescription, ICalculatedAggregateDescription
	{
		// Token: 0x17001536 RID: 5430
		// (get) Token: 0x060040EF RID: 16623 RVA: 0x000CC6AA File Offset: 0x000CA8AA
		// (set) Token: 0x060040F0 RID: 16624 RVA: 0x000CC6B2 File Offset: 0x000CA8B2
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

		// Token: 0x17001537 RID: 5431
		// (get) Token: 0x060040F1 RID: 16625 RVA: 0x000CC6DF File Offset: 0x000CA8DF
		// (set) Token: 0x060040F2 RID: 16626 RVA: 0x000CC6E7 File Offset: 0x000CA8E7
		public CalculatedField CalculatedField { get; private set; }

		// Token: 0x17001538 RID: 5432
		// (get) Token: 0x060040F3 RID: 16627 RVA: 0x000CC6F0 File Offset: 0x000CA8F0
		// (set) Token: 0x060040F4 RID: 16628 RVA: 0x000CC6F8 File Offset: 0x000CA8F8
		internal PropertyFieldInfo FieldInfo { get; set; }

		// Token: 0x17001539 RID: 5433
		// (get) Token: 0x060040F5 RID: 16629 RVA: 0x000CC701 File Offset: 0x000CA901
		bool IInitializeDescription.Initialized
		{
			get
			{
				return this.CalculatedField != null;
			}
		}

		// Token: 0x060040F6 RID: 16630 RVA: 0x000CC70F File Offset: 0x000CA90F
		public override string GetUniqueName()
		{
			return this.CalculatedFieldName;
		}

		// Token: 0x060040F7 RID: 16631 RVA: 0x000CC717 File Offset: 0x000CA917
		internal override RequiredField GetRequiredField()
		{
			return RequiredField.ForCalculatedField(this.calculatedFieldName);
		}

		// Token: 0x060040F8 RID: 16632 RVA: 0x000CC724 File Offset: 0x000CA924
		internal override IPivotFieldInfo GetFieldInfo()
		{
			return this.FieldInfo;
		}

		// Token: 0x060040F9 RID: 16633 RVA: 0x000CC72C File Offset: 0x000CA92C
		internal override AggregateValue CreateAggregate()
		{
			return null;
		}

		// Token: 0x060040FA RID: 16634 RVA: 0x000CC730 File Offset: 0x000CA930
		[SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Justification = "Design choice.")]
		internal override string GetEffectiveFormat()
		{
			Type dataType = (this.FieldInfo == null) ? null : this.FieldInfo.DataType;
			if (base.StringFormatSelector != null)
			{
				return base.StringFormatSelector.SelectStringFormat();
			}
			string stringFormat = base.StringFormat;
			if (base.TotalFormat != null)
			{
				stringFormat = base.TotalFormat.GetStringFormat(dataType, stringFormat);
			}
			return stringFormat;
		}

		// Token: 0x060040FB RID: 16635 RVA: 0x000CC788 File Offset: 0x000CA988
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
			this.FieldInfo = calculatedPivotFieldInfo;
			this.CalculatedField = calculatedPivotFieldInfo.CalculatedField;
		}

		// Token: 0x060040FC RID: 16636 RVA: 0x000CC7D4 File Offset: 0x000CA9D4
		Type IDataFieldDescription.GetDataType()
		{
			if (this.FieldInfo != null)
			{
				return this.FieldInfo.DataType;
			}
			return null;
		}

		// Token: 0x060040FD RID: 16637 RVA: 0x000CC7EC File Offset: 0x000CA9EC
		protected override string GetDisplayName()
		{
			string displayName = base.GetDisplayName();
			if (displayName != null)
			{
				return displayName;
			}
			string displayName2 = this.CalculatedFieldName;
			if (this.FieldInfo != null && this.FieldInfo.DisplayName != null)
			{
				displayName2 = this.FieldInfo.DisplayName;
			}
			return displayName2;
		}

		// Token: 0x060040FE RID: 16638 RVA: 0x000CC82E File Offset: 0x000CAA2E
		protected override Cloneable CreateInstanceCore()
		{
			return new QueryableCalculatedAggregateDescription();
		}

		// Token: 0x060040FF RID: 16639 RVA: 0x000CC838 File Offset: 0x000CAA38
		protected override void CloneCore(Cloneable source)
		{
			base.CloneCore(source);
			QueryableCalculatedAggregateDescription queryableCalculatedAggregateDescription = source as QueryableCalculatedAggregateDescription;
			if (queryableCalculatedAggregateDescription != null)
			{
				this.CalculatedFieldName = queryableCalculatedAggregateDescription.CalculatedFieldName;
				this.CalculatedField = queryableCalculatedAggregateDescription.CalculatedField;
				this.FieldInfo = queryableCalculatedAggregateDescription.FieldInfo;
			}
		}

		// Token: 0x0400113D RID: 4413
		private string calculatedFieldName;
	}
}
