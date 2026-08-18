using System;

namespace System.ComponentModel
{
	// Token: 0x02000542 RID: 1346
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
	public sealed class DesignerCategoryAttribute : Attribute
	{
		// Token: 0x060032BD RID: 12989 RVA: 0x000E29C0 File Offset: 0x000E0BC0
		public DesignerCategoryAttribute()
		{
			this.category = string.Empty;
		}

		// Token: 0x060032BE RID: 12990 RVA: 0x000E29D3 File Offset: 0x000E0BD3
		public DesignerCategoryAttribute(string category)
		{
			this.category = category;
		}

		// Token: 0x17000C6A RID: 3178
		// (get) Token: 0x060032BF RID: 12991 RVA: 0x000E29E2 File Offset: 0x000E0BE2
		public string Category
		{
			get
			{
				return this.category;
			}
		}

		// Token: 0x17000C6B RID: 3179
		// (get) Token: 0x060032C0 RID: 12992 RVA: 0x000E29EA File Offset: 0x000E0BEA
		public override object TypeId
		{
			get
			{
				if (this.typeId == null)
				{
					this.typeId = base.GetType().FullName + this.Category;
				}
				return this.typeId;
			}
		}

		// Token: 0x060032C1 RID: 12993 RVA: 0x000E2A18 File Offset: 0x000E0C18
		public override bool Equals(object obj)
		{
			if (obj == this)
			{
				return true;
			}
			DesignerCategoryAttribute designerCategoryAttribute = obj as DesignerCategoryAttribute;
			return designerCategoryAttribute != null && designerCategoryAttribute.category == this.category;
		}

		// Token: 0x060032C2 RID: 12994 RVA: 0x000E2A48 File Offset: 0x000E0C48
		public override int GetHashCode()
		{
			return this.category.GetHashCode();
		}

		// Token: 0x060032C3 RID: 12995 RVA: 0x000E2A55 File Offset: 0x000E0C55
		public override bool IsDefaultAttribute()
		{
			return this.category.Equals(DesignerCategoryAttribute.Default.Category);
		}

		// Token: 0x0400298C RID: 10636
		private string category;

		// Token: 0x0400298D RID: 10637
		private string typeId;

		// Token: 0x0400298E RID: 10638
		public static readonly DesignerCategoryAttribute Component = new DesignerCategoryAttribute("Component");

		// Token: 0x0400298F RID: 10639
		public static readonly DesignerCategoryAttribute Default = new DesignerCategoryAttribute();

		// Token: 0x04002990 RID: 10640
		public static readonly DesignerCategoryAttribute Form = new DesignerCategoryAttribute("Form");

		// Token: 0x04002991 RID: 10641
		public static readonly DesignerCategoryAttribute Generic = new DesignerCategoryAttribute("Designer");
	}
}
