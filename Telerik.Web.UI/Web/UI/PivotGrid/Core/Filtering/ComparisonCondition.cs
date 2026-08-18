using System;
using System.Runtime.Serialization;

namespace Telerik.Web.UI.PivotGrid.Core.Filtering
{
	// Token: 0x020006BE RID: 1726
	[DataContract]
	public sealed class ComparisonCondition : LocalCondition, IComparisonCondition
	{
		// Token: 0x17001447 RID: 5191
		// (get) Token: 0x06003DF7 RID: 15863 RVA: 0x000C7374 File Offset: 0x000C5574
		// (set) Token: 0x06003DF8 RID: 15864 RVA: 0x000C737C File Offset: 0x000C557C
		object IComparisonCondition.Than
		{
			get
			{
				return this.Than;
			}
			set
			{
				this.Than = value;
			}
		}

		// Token: 0x17001448 RID: 5192
		// (get) Token: 0x06003DF9 RID: 15865 RVA: 0x000C7385 File Offset: 0x000C5585
		// (set) Token: 0x06003DFA RID: 15866 RVA: 0x000C738D File Offset: 0x000C558D
		Comparison IComparisonCondition.Condition
		{
			get
			{
				return this.Condition;
			}
			set
			{
				this.Condition = value;
			}
		}

		// Token: 0x17001449 RID: 5193
		// (get) Token: 0x06003DFB RID: 15867 RVA: 0x000C7396 File Offset: 0x000C5596
		// (set) Token: 0x06003DFC RID: 15868 RVA: 0x000C739E File Offset: 0x000C559E
		bool IComparisonCondition.IgnoreCase
		{
			get
			{
				return this.IgnoreCase;
			}
			set
			{
				this.IgnoreCase = value;
			}
		}

		// Token: 0x1700144A RID: 5194
		// (get) Token: 0x06003DFE RID: 15870 RVA: 0x000C73AF File Offset: 0x000C55AF
		public override bool IsActive
		{
			get
			{
				return this.Than != null;
			}
		}

		// Token: 0x1700144B RID: 5195
		// (get) Token: 0x06003DFF RID: 15871 RVA: 0x000C73BD File Offset: 0x000C55BD
		// (set) Token: 0x06003E00 RID: 15872 RVA: 0x000C73C5 File Offset: 0x000C55C5
		[DataMember]
		public bool IgnoreCase
		{
			get
			{
				return this.ignoreCase;
			}
			set
			{
				if (this.ignoreCase != value)
				{
					this.ignoreCase = value;
					base.OnPropertyChanged("IgnoreCase");
				}
			}
		}

		// Token: 0x1700144C RID: 5196
		// (get) Token: 0x06003E01 RID: 15873 RVA: 0x000C73E2 File Offset: 0x000C55E2
		// (set) Token: 0x06003E02 RID: 15874 RVA: 0x000C73EA File Offset: 0x000C55EA
		[DataMember]
		public object Than
		{
			get
			{
				return this.than;
			}
			set
			{
				if (this.than != value)
				{
					this.than = value;
					base.OnPropertyChanged("Than");
				}
			}
		}

		// Token: 0x1700144D RID: 5197
		// (get) Token: 0x06003E03 RID: 15875 RVA: 0x000C7407 File Offset: 0x000C5607
		// (set) Token: 0x06003E04 RID: 15876 RVA: 0x000C740F File Offset: 0x000C560F
		[DataMember]
		public Comparison Condition
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

		// Token: 0x06003E05 RID: 15877 RVA: 0x000C742C File Offset: 0x000C562C
		public override bool PassesFilter(object item)
		{
			DefaultComparer defaultComparer = new DefaultComparer
			{
				IgnoreCase = this.IgnoreCase
			};
			switch (this.Condition)
			{
			case Comparison.DoesNotEqual:
				return defaultComparer.Compare(item, this.Than) != 0;
			case Comparison.IsGreaterThan:
				return defaultComparer.Compare(item, this.Than) > 0;
			case Comparison.IsGreaterThanOrEqualTo:
				return defaultComparer.Compare(item, this.Than) >= 0;
			case Comparison.IsLessThan:
				return defaultComparer.Compare(item, this.Than) < 0;
			case Comparison.IsLessThanOrEqualTo:
				return defaultComparer.Compare(item, this.Than) <= 0;
			}
			return defaultComparer.Compare(item, this.Than) == 0;
		}

		// Token: 0x06003E06 RID: 15878 RVA: 0x000C74E4 File Offset: 0x000C56E4
		protected override void CloneCore(Cloneable source)
		{
			ComparisonCondition comparisonCondition = source as ComparisonCondition;
			if (comparisonCondition != null)
			{
				this.Than = comparisonCondition.Than;
				this.Condition = comparisonCondition.Condition;
				this.IgnoreCase = comparisonCondition.IgnoreCase;
			}
		}

		// Token: 0x06003E07 RID: 15879 RVA: 0x000C751F File Offset: 0x000C571F
		protected override Cloneable CreateInstanceCore()
		{
			return new ComparisonCondition();
		}

		// Token: 0x04001098 RID: 4248
		private object than;

		// Token: 0x04001099 RID: 4249
		private Comparison condition;

		// Token: 0x0400109A RID: 4250
		private bool ignoreCase;
	}
}
