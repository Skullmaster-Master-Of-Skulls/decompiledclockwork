using System;
using System.ComponentModel;

namespace System.Data
{
	// Token: 0x020000BC RID: 188
	internal sealed class DataRelationPropertyDescriptor : PropertyDescriptor
	{
		// Token: 0x17000188 RID: 392
		// (get) Token: 0x06000AE5 RID: 2789 RVA: 0x0006116C File Offset: 0x0006056C
		internal DataRelation Relation
		{
			get
			{
				return this.relation;
			}
		}

		// Token: 0x06000AE6 RID: 2790 RVA: 0x00061180 File Offset: 0x00060580
		internal DataRelationPropertyDescriptor(DataRelation dataRelation) : base(dataRelation.RelationName, null)
		{
			this.relation = dataRelation;
		}

		// Token: 0x17000189 RID: 393
		// (get) Token: 0x06000AE7 RID: 2791 RVA: 0x000611A4 File Offset: 0x000605A4
		public override Type ComponentType
		{
			get
			{
				return typeof(DataRowView);
			}
		}

		// Token: 0x1700018A RID: 394
		// (get) Token: 0x06000AE8 RID: 2792 RVA: 0x000611BC File Offset: 0x000605BC
		public override bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700018B RID: 395
		// (get) Token: 0x06000AE9 RID: 2793 RVA: 0x000611CC File Offset: 0x000605CC
		public override Type PropertyType
		{
			get
			{
				return typeof(IBindingList);
			}
		}

		// Token: 0x06000AEA RID: 2794 RVA: 0x000611E4 File Offset: 0x000605E4
		public override bool Equals(object other)
		{
			if (other is DataRelationPropertyDescriptor)
			{
				DataRelationPropertyDescriptor dataRelationPropertyDescriptor = (DataRelationPropertyDescriptor)other;
				return dataRelationPropertyDescriptor.Relation == this.Relation;
			}
			return false;
		}

		// Token: 0x06000AEB RID: 2795 RVA: 0x00061210 File Offset: 0x00060610
		public override int GetHashCode()
		{
			return this.Relation.GetHashCode();
		}

		// Token: 0x06000AEC RID: 2796 RVA: 0x00061228 File Offset: 0x00060628
		public override bool CanResetValue(object component)
		{
			return false;
		}

		// Token: 0x06000AED RID: 2797 RVA: 0x00061238 File Offset: 0x00060638
		public override object GetValue(object component)
		{
			DataRowView dataRowView = (DataRowView)component;
			return dataRowView.CreateChildView(this.relation);
		}

		// Token: 0x06000AEE RID: 2798 RVA: 0x00061258 File Offset: 0x00060658
		public override void ResetValue(object component)
		{
		}

		// Token: 0x06000AEF RID: 2799 RVA: 0x00061268 File Offset: 0x00060668
		public override void SetValue(object component, object value)
		{
		}

		// Token: 0x06000AF0 RID: 2800 RVA: 0x00061278 File Offset: 0x00060678
		public override bool ShouldSerializeValue(object component)
		{
			return false;
		}

		// Token: 0x04000346 RID: 838
		private DataRelation relation;
	}
}
