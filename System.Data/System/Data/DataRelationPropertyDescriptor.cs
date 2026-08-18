using System;
using System.ComponentModel;

namespace System.Data
{
	// Token: 0x02000080 RID: 128
	internal sealed class DataRelationPropertyDescriptor : PropertyDescriptor
	{
		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x06000757 RID: 1879 RVA: 0x001F36A8 File Offset: 0x001F2AA8
		internal DataRelation Relation
		{
			get
			{
				return this.relation;
			}
		}

		// Token: 0x06000758 RID: 1880 RVA: 0x001F36C8 File Offset: 0x001F2AC8
		internal DataRelationPropertyDescriptor(DataRelation dataRelation) : base(dataRelation.RelationName, null)
		{
			this.relation = dataRelation;
		}

		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x06000759 RID: 1881 RVA: 0x001F36F8 File Offset: 0x001F2AF8
		public override Type ComponentType
		{
			get
			{
				return typeof(DataRowView);
			}
		}

		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x0600075A RID: 1882 RVA: 0x001F3718 File Offset: 0x001F2B18
		public override bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x0600075B RID: 1883 RVA: 0x001F3728 File Offset: 0x001F2B28
		public override Type PropertyType
		{
			get
			{
				return typeof(IBindingList);
			}
		}

		// Token: 0x0600075C RID: 1884 RVA: 0x001F3748 File Offset: 0x001F2B48
		public override bool Equals(object other)
		{
			if (other is DataRelationPropertyDescriptor)
			{
				DataRelationPropertyDescriptor dataRelationPropertyDescriptor = (DataRelationPropertyDescriptor)other;
				return dataRelationPropertyDescriptor.Relation == this.Relation;
			}
			return false;
		}

		// Token: 0x0600075D RID: 1885 RVA: 0x001F3778 File Offset: 0x001F2B78
		public override int GetHashCode()
		{
			return this.Relation.GetHashCode();
		}

		// Token: 0x0600075E RID: 1886 RVA: 0x001F3798 File Offset: 0x001F2B98
		public override bool CanResetValue(object component)
		{
			return false;
		}

		// Token: 0x0600075F RID: 1887 RVA: 0x001F37A8 File Offset: 0x001F2BA8
		public override object GetValue(object component)
		{
			DataRowView dataRowView = (DataRowView)component;
			return dataRowView.CreateChildView(this.relation);
		}

		// Token: 0x06000760 RID: 1888 RVA: 0x001F37C8 File Offset: 0x001F2BC8
		public override void ResetValue(object component)
		{
		}

		// Token: 0x06000761 RID: 1889 RVA: 0x001F37D8 File Offset: 0x001F2BD8
		public override void SetValue(object component, object value)
		{
		}

		// Token: 0x06000762 RID: 1890 RVA: 0x001F37E8 File Offset: 0x001F2BE8
		public override bool ShouldSerializeValue(object component)
		{
			return false;
		}

		// Token: 0x04000743 RID: 1859
		private DataRelation relation;
	}
}
