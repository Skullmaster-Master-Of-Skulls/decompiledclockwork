using System;
using System.Runtime.Serialization;

namespace Telerik.Web.UI.PivotGrid.Core.Filtering
{
	// Token: 0x020006C5 RID: 1733
	[DataContract]
	public sealed class IntervalCondition : LocalCondition, IIntervalCondition
	{
		// Token: 0x17001456 RID: 5206
		// (get) Token: 0x06003E19 RID: 15897 RVA: 0x000C7526 File Offset: 0x000C5726
		// (set) Token: 0x06003E1A RID: 15898 RVA: 0x000C752E File Offset: 0x000C572E
		object IIntervalCondition.From
		{
			get
			{
				return this.From;
			}
			set
			{
				this.From = value;
			}
		}

		// Token: 0x17001457 RID: 5207
		// (get) Token: 0x06003E1B RID: 15899 RVA: 0x000C7537 File Offset: 0x000C5737
		// (set) Token: 0x06003E1C RID: 15900 RVA: 0x000C753F File Offset: 0x000C573F
		object IIntervalCondition.To
		{
			get
			{
				return this.To;
			}
			set
			{
				this.To = value;
			}
		}

		// Token: 0x17001458 RID: 5208
		// (get) Token: 0x06003E1D RID: 15901 RVA: 0x000C7548 File Offset: 0x000C5748
		// (set) Token: 0x06003E1E RID: 15902 RVA: 0x000C7550 File Offset: 0x000C5750
		IntervalComparison IIntervalCondition.Condition
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

		// Token: 0x17001459 RID: 5209
		// (get) Token: 0x06003E1F RID: 15903 RVA: 0x000C7559 File Offset: 0x000C5759
		// (set) Token: 0x06003E20 RID: 15904 RVA: 0x000C7561 File Offset: 0x000C5761
		bool IIntervalCondition.IgnoreCase
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

		// Token: 0x1700145A RID: 5210
		// (get) Token: 0x06003E22 RID: 15906 RVA: 0x000C7572 File Offset: 0x000C5772
		public override bool IsActive
		{
			get
			{
				return this.From != null && this.To != null;
			}
		}

		// Token: 0x1700145B RID: 5211
		// (get) Token: 0x06003E23 RID: 15907 RVA: 0x000C758A File Offset: 0x000C578A
		// (set) Token: 0x06003E24 RID: 15908 RVA: 0x000C7592 File Offset: 0x000C5792
		[DataMember]
		public bool IgnoreCase { get; set; }

		// Token: 0x1700145C RID: 5212
		// (get) Token: 0x06003E25 RID: 15909 RVA: 0x000C759B File Offset: 0x000C579B
		// (set) Token: 0x06003E26 RID: 15910 RVA: 0x000C75A3 File Offset: 0x000C57A3
		[DataMember]
		public object From
		{
			get
			{
				return this.from;
			}
			set
			{
				if (this.from != value)
				{
					this.from = value;
					base.OnPropertyChanged("From");
				}
			}
		}

		// Token: 0x1700145D RID: 5213
		// (get) Token: 0x06003E27 RID: 15911 RVA: 0x000C75C0 File Offset: 0x000C57C0
		// (set) Token: 0x06003E28 RID: 15912 RVA: 0x000C75C8 File Offset: 0x000C57C8
		[DataMember]
		public object To
		{
			get
			{
				return this.to;
			}
			set
			{
				if (this.to != value)
				{
					this.to = value;
					base.OnPropertyChanged("To");
				}
			}
		}

		// Token: 0x1700145E RID: 5214
		// (get) Token: 0x06003E29 RID: 15913 RVA: 0x000C75E5 File Offset: 0x000C57E5
		// (set) Token: 0x06003E2A RID: 15914 RVA: 0x000C75ED File Offset: 0x000C57ED
		[DataMember]
		public IntervalComparison Condition
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

		// Token: 0x06003E2B RID: 15915 RVA: 0x000C760C File Offset: 0x000C580C
		public override bool PassesFilter(object item)
		{
			DefaultComparer defaultComparer = new DefaultComparer
			{
				IgnoreCase = this.IgnoreCase
			};
			switch (this.Condition)
			{
			case IntervalComparison.IsNotBetween:
				return defaultComparer.Compare(item, this.From) < 0 || defaultComparer.Compare(this.To, item) < 0;
			}
			return defaultComparer.Compare(item, this.From) >= 0 && defaultComparer.Compare(this.To, item) >= 0;
		}

		// Token: 0x06003E2C RID: 15916 RVA: 0x000C768C File Offset: 0x000C588C
		protected override void CloneCore(Cloneable source)
		{
			IntervalCondition intervalCondition = source as IntervalCondition;
			if (intervalCondition != null)
			{
				this.From = intervalCondition.From;
				this.To = intervalCondition.To;
				this.Condition = intervalCondition.Condition;
				this.IgnoreCase = intervalCondition.IgnoreCase;
			}
		}

		// Token: 0x06003E2D RID: 15917 RVA: 0x000C76D3 File Offset: 0x000C58D3
		protected override Cloneable CreateInstanceCore()
		{
			return new IntervalCondition();
		}

		// Token: 0x0400109B RID: 4251
		private object from;

		// Token: 0x0400109C RID: 4252
		private object to;

		// Token: 0x0400109D RID: 4253
		private IntervalComparison condition;
	}
}
