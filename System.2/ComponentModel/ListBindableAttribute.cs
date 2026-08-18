using System;

namespace System.ComponentModel
{
	// Token: 0x02000584 RID: 1412
	[AttributeUsage(AttributeTargets.All)]
	public sealed class ListBindableAttribute : Attribute
	{
		// Token: 0x0600342D RID: 13357 RVA: 0x000E4C3E File Offset: 0x000E2E3E
		public ListBindableAttribute(bool listBindable)
		{
			this.listBindable = listBindable;
		}

		// Token: 0x0600342E RID: 13358 RVA: 0x000E4C4D File Offset: 0x000E2E4D
		public ListBindableAttribute(BindableSupport flags)
		{
			this.listBindable = (flags > BindableSupport.No);
			this.isDefault = (flags == BindableSupport.Default);
		}

		// Token: 0x17000CC0 RID: 3264
		// (get) Token: 0x0600342F RID: 13359 RVA: 0x000E4C69 File Offset: 0x000E2E69
		public bool ListBindable
		{
			get
			{
				return this.listBindable;
			}
		}

		// Token: 0x06003430 RID: 13360 RVA: 0x000E4C74 File Offset: 0x000E2E74
		public override bool Equals(object obj)
		{
			if (obj == this)
			{
				return true;
			}
			ListBindableAttribute listBindableAttribute = obj as ListBindableAttribute;
			return listBindableAttribute != null && listBindableAttribute.ListBindable == this.listBindable;
		}

		// Token: 0x06003431 RID: 13361 RVA: 0x000E4CA1 File Offset: 0x000E2EA1
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x06003432 RID: 13362 RVA: 0x000E4CA9 File Offset: 0x000E2EA9
		public override bool IsDefaultAttribute()
		{
			return this.Equals(ListBindableAttribute.Default) || this.isDefault;
		}

		// Token: 0x040029D7 RID: 10711
		public static readonly ListBindableAttribute Yes = new ListBindableAttribute(true);

		// Token: 0x040029D8 RID: 10712
		public static readonly ListBindableAttribute No = new ListBindableAttribute(false);

		// Token: 0x040029D9 RID: 10713
		public static readonly ListBindableAttribute Default = ListBindableAttribute.Yes;

		// Token: 0x040029DA RID: 10714
		private bool listBindable;

		// Token: 0x040029DB RID: 10715
		private bool isDefault;
	}
}
