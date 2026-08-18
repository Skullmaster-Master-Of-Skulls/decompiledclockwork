using System;
using Telerik.Web.UI.PivotGrid.Filtering;

namespace Telerik.Web.UI.PivotGrid.Core.Filtering
{
	// Token: 0x020006C7 RID: 1735
	public sealed class ItemsFilterCondition : LocalCondition, IItemsFilterCondition
	{
		// Token: 0x06003E31 RID: 15921 RVA: 0x000C76DA File Offset: 0x000C58DA
		public ItemsFilterCondition()
		{
			this.distinctCondition = new SetCondition();
			this.distinctCondition.Comparison = SetComparison.DoesNotInclude;
			this.condition = new ComparisonCondition();
		}

		// Token: 0x17001461 RID: 5217
		// (get) Token: 0x06003E32 RID: 15922 RVA: 0x000C7704 File Offset: 0x000C5904
		// (set) Token: 0x06003E33 RID: 15923 RVA: 0x000C770C File Offset: 0x000C590C
		ISetCondition IItemsFilterCondition.DistinctCondition
		{
			get
			{
				return this.DistinctCondition;
			}
			set
			{
				this.DistinctCondition = (value as SetCondition);
			}
		}

		// Token: 0x17001462 RID: 5218
		// (get) Token: 0x06003E34 RID: 15924 RVA: 0x000C771A File Offset: 0x000C591A
		// (set) Token: 0x06003E35 RID: 15925 RVA: 0x000C7722 File Offset: 0x000C5922
		Condition IItemsFilterCondition.Condition
		{
			get
			{
				return this.Condition;
			}
			set
			{
				this.condition = (value as LocalCondition);
			}
		}

		// Token: 0x17001463 RID: 5219
		// (get) Token: 0x06003E36 RID: 15926 RVA: 0x000C7730 File Offset: 0x000C5930
		// (set) Token: 0x06003E37 RID: 15927 RVA: 0x000C7738 File Offset: 0x000C5938
		public SetCondition DistinctCondition
		{
			get
			{
				return this.distinctCondition;
			}
			set
			{
				if (this.distinctCondition != value)
				{
					base.ChangeSettingsProperty<SetCondition>(ref this.distinctCondition, value);
					base.OnPropertyChanged("DistinctCondition");
					base.NotifySettingsChanged(new SettingsChangedEventArgs());
				}
			}
		}

		// Token: 0x17001464 RID: 5220
		// (get) Token: 0x06003E38 RID: 15928 RVA: 0x000C7766 File Offset: 0x000C5966
		// (set) Token: 0x06003E39 RID: 15929 RVA: 0x000C776E File Offset: 0x000C596E
		public LocalCondition Condition
		{
			get
			{
				return this.condition;
			}
			set
			{
				this.condition = value;
			}
		}

		// Token: 0x06003E3A RID: 15930 RVA: 0x000C7777 File Offset: 0x000C5977
		protected override Cloneable CreateInstanceCore()
		{
			return new ItemsFilterCondition();
		}

		// Token: 0x06003E3B RID: 15931 RVA: 0x000C7780 File Offset: 0x000C5980
		public override bool PassesFilter(object item)
		{
			bool flag = this.DistinctCondition.PassesFilter(item);
			bool flag2 = true;
			if (this.condition != null && this.condition.IsActive)
			{
				flag2 = (flag2 && this.condition.PassesFilter(item));
			}
			return flag && flag2;
		}

		// Token: 0x06003E3C RID: 15932 RVA: 0x000C77CC File Offset: 0x000C59CC
		protected sealed override void CloneCore(Cloneable source)
		{
			ItemsFilterCondition itemsFilterCondition = source as ItemsFilterCondition;
			if (itemsFilterCondition != null)
			{
				this.DistinctCondition = Cloneable.CloneOrDefault<SetCondition>(itemsFilterCondition.DistinctCondition);
				this.Condition = Cloneable.CloneOrDefault<LocalCondition>(itemsFilterCondition.Condition);
			}
		}

		// Token: 0x0400109F RID: 4255
		private SetCondition distinctCondition;

		// Token: 0x040010A0 RID: 4256
		private LocalCondition condition;
	}
}
