using System;
using System.Runtime.Serialization;

namespace Telerik.Web.UI.PivotGrid.Core.Filtering
{
	// Token: 0x020006E0 RID: 1760
	[DataContract]
	public sealed class LabelGroupFilter : SingleGroupFilter, ILabelGroupFilter, IConditionFactory
	{
		// Token: 0x17001484 RID: 5252
		// (get) Token: 0x06003ECD RID: 16077 RVA: 0x000C80C1 File Offset: 0x000C62C1
		// (set) Token: 0x06003ECE RID: 16078 RVA: 0x000C80C9 File Offset: 0x000C62C9
		Condition ILabelGroupFilter.Condition
		{
			get
			{
				return this.Condition;
			}
			set
			{
				this.Condition = (value as LocalCondition);
			}
		}

		// Token: 0x06003ECF RID: 16079 RVA: 0x000C80D7 File Offset: 0x000C62D7
		Condition IConditionFactory.CreateCondition(Type conditionType)
		{
			return DescriptionBase.CreateLocalCondition(conditionType);
		}

		// Token: 0x17001485 RID: 5253
		// (get) Token: 0x06003ED0 RID: 16080 RVA: 0x000C80DF File Offset: 0x000C62DF
		// (set) Token: 0x06003ED1 RID: 16081 RVA: 0x000C80E7 File Offset: 0x000C62E7
		[DataMember]
		public LocalCondition Condition
		{
			get
			{
				return this.condition;
			}
			set
			{
				if (this.condition != value)
				{
					this.condition = value;
					base.OnPropertyChanged("Condition");
				}
			}
		}

		// Token: 0x06003ED2 RID: 16082 RVA: 0x000C8104 File Offset: 0x000C6304
		protected internal override bool Filter(IGroup group, IAggregateResultProvider results, PivotAxis axis)
		{
			return this.Condition == null || this.Condition.PassesFilter(group.Name);
		}

		// Token: 0x06003ED3 RID: 16083 RVA: 0x000C8121 File Offset: 0x000C6321
		protected override Cloneable CreateInstanceCore()
		{
			return new LabelGroupFilter();
		}

		// Token: 0x06003ED4 RID: 16084 RVA: 0x000C8128 File Offset: 0x000C6328
		protected override void CloneCore(Cloneable source)
		{
			LabelGroupFilter labelGroupFilter = source as LabelGroupFilter;
			if (labelGroupFilter != null)
			{
				this.Condition = ((labelGroupFilter.Condition == null) ? null : (labelGroupFilter.Condition.Clone() as LocalCondition));
			}
		}

		// Token: 0x040010B0 RID: 4272
		private LocalCondition condition;
	}
}
