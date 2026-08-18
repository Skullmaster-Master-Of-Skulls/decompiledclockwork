using System;
using System.Runtime.Serialization;

namespace Telerik.Web.UI.PivotGrid.Core.Filtering
{
	// Token: 0x02000CBF RID: 3263
	[DataContract]
	public class SetCondition : LocalCondition, ISetCondition
	{
		// Token: 0x060079EA RID: 31210 RVA: 0x001BFB71 File Offset: 0x001BDD71
		public SetCondition()
		{
			this.negateSelection = false;
			this.condition = SetComparison.Includes;
		}

		// Token: 0x1700273C RID: 10044
		// (get) Token: 0x060079EB RID: 31211 RVA: 0x001BFB87 File Offset: 0x001BDD87
		// (set) Token: 0x060079EC RID: 31212 RVA: 0x001BFB8F File Offset: 0x001BDD8F
		[DataMember]
		public SetComparison Comparison
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
					this.negateSelection = (value == SetComparison.DoesNotInclude);
					base.OnPropertyChanged("Comparison");
				}
			}
		}

		// Token: 0x1700273D RID: 10045
		// (get) Token: 0x060079ED RID: 31213 RVA: 0x001BFBB6 File Offset: 0x001BDDB6
		[DataMember]
		public SetConditionHashCollection Items
		{
			get
			{
				if (this.items == null)
				{
					this.items = new SetConditionHashCollection();
				}
				return this.items;
			}
		}

		// Token: 0x060079EE RID: 31214 RVA: 0x001BFBD4 File Offset: 0x001BDDD4
		public override bool PassesFilter(object item)
		{
			if (this.items != null)
			{
				bool flag = this.items.Contains(item);
				return flag ^ this.negateSelection;
			}
			return this.negateSelection;
		}

		// Token: 0x060079EF RID: 31215 RVA: 0x001BFC07 File Offset: 0x001BDE07
		protected override Cloneable CreateInstanceCore()
		{
			return new SetCondition();
		}

		// Token: 0x060079F0 RID: 31216 RVA: 0x001BFC10 File Offset: 0x001BDE10
		protected override void CloneCore(Cloneable source)
		{
			SetCondition setCondition = source as SetCondition;
			if (setCondition != null)
			{
				this.Comparison = setCondition.Comparison;
				if (setCondition.items != null)
				{
					this.items = new SetConditionHashCollection(setCondition.items);
				}
			}
		}

		// Token: 0x04002167 RID: 8551
		private SetComparison condition;

		// Token: 0x04002168 RID: 8552
		private SetConditionHashCollection items;

		// Token: 0x04002169 RID: 8553
		private bool negateSelection;
	}
}
