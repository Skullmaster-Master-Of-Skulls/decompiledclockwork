using System;

namespace System.ComponentModel
{
	// Token: 0x02000523 RID: 1315
	[AttributeUsage(AttributeTargets.All)]
	public class CategoryAttribute : Attribute
	{
		// Token: 0x17000C36 RID: 3126
		// (get) Token: 0x060031E1 RID: 12769 RVA: 0x000E0562 File Offset: 0x000DE762
		public static CategoryAttribute Action
		{
			get
			{
				if (CategoryAttribute.action == null)
				{
					CategoryAttribute.action = new CategoryAttribute("Action");
				}
				return CategoryAttribute.action;
			}
		}

		// Token: 0x17000C37 RID: 3127
		// (get) Token: 0x060031E2 RID: 12770 RVA: 0x000E0585 File Offset: 0x000DE785
		public static CategoryAttribute Appearance
		{
			get
			{
				if (CategoryAttribute.appearance == null)
				{
					CategoryAttribute.appearance = new CategoryAttribute("Appearance");
				}
				return CategoryAttribute.appearance;
			}
		}

		// Token: 0x17000C38 RID: 3128
		// (get) Token: 0x060031E3 RID: 12771 RVA: 0x000E05A8 File Offset: 0x000DE7A8
		public static CategoryAttribute Asynchronous
		{
			get
			{
				if (CategoryAttribute.asynchronous == null)
				{
					CategoryAttribute.asynchronous = new CategoryAttribute("Asynchronous");
				}
				return CategoryAttribute.asynchronous;
			}
		}

		// Token: 0x17000C39 RID: 3129
		// (get) Token: 0x060031E4 RID: 12772 RVA: 0x000E05CB File Offset: 0x000DE7CB
		public static CategoryAttribute Behavior
		{
			get
			{
				if (CategoryAttribute.behavior == null)
				{
					CategoryAttribute.behavior = new CategoryAttribute("Behavior");
				}
				return CategoryAttribute.behavior;
			}
		}

		// Token: 0x17000C3A RID: 3130
		// (get) Token: 0x060031E5 RID: 12773 RVA: 0x000E05EE File Offset: 0x000DE7EE
		public static CategoryAttribute Data
		{
			get
			{
				if (CategoryAttribute.data == null)
				{
					CategoryAttribute.data = new CategoryAttribute("Data");
				}
				return CategoryAttribute.data;
			}
		}

		// Token: 0x17000C3B RID: 3131
		// (get) Token: 0x060031E6 RID: 12774 RVA: 0x000E0611 File Offset: 0x000DE811
		public static CategoryAttribute Default
		{
			get
			{
				if (CategoryAttribute.defAttr == null)
				{
					CategoryAttribute.defAttr = new CategoryAttribute();
				}
				return CategoryAttribute.defAttr;
			}
		}

		// Token: 0x17000C3C RID: 3132
		// (get) Token: 0x060031E7 RID: 12775 RVA: 0x000E062F File Offset: 0x000DE82F
		public static CategoryAttribute Design
		{
			get
			{
				if (CategoryAttribute.design == null)
				{
					CategoryAttribute.design = new CategoryAttribute("Design");
				}
				return CategoryAttribute.design;
			}
		}

		// Token: 0x17000C3D RID: 3133
		// (get) Token: 0x060031E8 RID: 12776 RVA: 0x000E0652 File Offset: 0x000DE852
		public static CategoryAttribute DragDrop
		{
			get
			{
				if (CategoryAttribute.dragDrop == null)
				{
					CategoryAttribute.dragDrop = new CategoryAttribute("DragDrop");
				}
				return CategoryAttribute.dragDrop;
			}
		}

		// Token: 0x17000C3E RID: 3134
		// (get) Token: 0x060031E9 RID: 12777 RVA: 0x000E0675 File Offset: 0x000DE875
		public static CategoryAttribute Focus
		{
			get
			{
				if (CategoryAttribute.focus == null)
				{
					CategoryAttribute.focus = new CategoryAttribute("Focus");
				}
				return CategoryAttribute.focus;
			}
		}

		// Token: 0x17000C3F RID: 3135
		// (get) Token: 0x060031EA RID: 12778 RVA: 0x000E0698 File Offset: 0x000DE898
		public static CategoryAttribute Format
		{
			get
			{
				if (CategoryAttribute.format == null)
				{
					CategoryAttribute.format = new CategoryAttribute("Format");
				}
				return CategoryAttribute.format;
			}
		}

		// Token: 0x17000C40 RID: 3136
		// (get) Token: 0x060031EB RID: 12779 RVA: 0x000E06BB File Offset: 0x000DE8BB
		public static CategoryAttribute Key
		{
			get
			{
				if (CategoryAttribute.key == null)
				{
					CategoryAttribute.key = new CategoryAttribute("Key");
				}
				return CategoryAttribute.key;
			}
		}

		// Token: 0x17000C41 RID: 3137
		// (get) Token: 0x060031EC RID: 12780 RVA: 0x000E06DE File Offset: 0x000DE8DE
		public static CategoryAttribute Layout
		{
			get
			{
				if (CategoryAttribute.layout == null)
				{
					CategoryAttribute.layout = new CategoryAttribute("Layout");
				}
				return CategoryAttribute.layout;
			}
		}

		// Token: 0x17000C42 RID: 3138
		// (get) Token: 0x060031ED RID: 12781 RVA: 0x000E0701 File Offset: 0x000DE901
		public static CategoryAttribute Mouse
		{
			get
			{
				if (CategoryAttribute.mouse == null)
				{
					CategoryAttribute.mouse = new CategoryAttribute("Mouse");
				}
				return CategoryAttribute.mouse;
			}
		}

		// Token: 0x17000C43 RID: 3139
		// (get) Token: 0x060031EE RID: 12782 RVA: 0x000E0724 File Offset: 0x000DE924
		public static CategoryAttribute WindowStyle
		{
			get
			{
				if (CategoryAttribute.windowStyle == null)
				{
					CategoryAttribute.windowStyle = new CategoryAttribute("WindowStyle");
				}
				return CategoryAttribute.windowStyle;
			}
		}

		// Token: 0x060031EF RID: 12783 RVA: 0x000E0747 File Offset: 0x000DE947
		public CategoryAttribute() : this("Default")
		{
		}

		// Token: 0x060031F0 RID: 12784 RVA: 0x000E0754 File Offset: 0x000DE954
		public CategoryAttribute(string category)
		{
			this.categoryValue = category;
			this.localized = false;
		}

		// Token: 0x17000C44 RID: 3140
		// (get) Token: 0x060031F1 RID: 12785 RVA: 0x000E076C File Offset: 0x000DE96C
		public string Category
		{
			get
			{
				if (!this.localized)
				{
					this.localized = true;
					string localizedString = this.GetLocalizedString(this.categoryValue);
					if (localizedString != null)
					{
						this.categoryValue = localizedString;
					}
				}
				return this.categoryValue;
			}
		}

		// Token: 0x060031F2 RID: 12786 RVA: 0x000E07A5 File Offset: 0x000DE9A5
		public override bool Equals(object obj)
		{
			return obj == this || (obj is CategoryAttribute && this.Category.Equals(((CategoryAttribute)obj).Category));
		}

		// Token: 0x060031F3 RID: 12787 RVA: 0x000E07CD File Offset: 0x000DE9CD
		public override int GetHashCode()
		{
			return this.Category.GetHashCode();
		}

		// Token: 0x060031F4 RID: 12788 RVA: 0x000E07DA File Offset: 0x000DE9DA
		protected virtual string GetLocalizedString(string value)
		{
			return (string)SR.GetObject("PropertyCategory" + value);
		}

		// Token: 0x060031F5 RID: 12789 RVA: 0x000E07F1 File Offset: 0x000DE9F1
		public override bool IsDefaultAttribute()
		{
			return this.Category.Equals(CategoryAttribute.Default.Category);
		}

		// Token: 0x04002946 RID: 10566
		private static volatile CategoryAttribute appearance;

		// Token: 0x04002947 RID: 10567
		private static volatile CategoryAttribute asynchronous;

		// Token: 0x04002948 RID: 10568
		private static volatile CategoryAttribute behavior;

		// Token: 0x04002949 RID: 10569
		private static volatile CategoryAttribute data;

		// Token: 0x0400294A RID: 10570
		private static volatile CategoryAttribute design;

		// Token: 0x0400294B RID: 10571
		private static volatile CategoryAttribute action;

		// Token: 0x0400294C RID: 10572
		private static volatile CategoryAttribute format;

		// Token: 0x0400294D RID: 10573
		private static volatile CategoryAttribute layout;

		// Token: 0x0400294E RID: 10574
		private static volatile CategoryAttribute mouse;

		// Token: 0x0400294F RID: 10575
		private static volatile CategoryAttribute key;

		// Token: 0x04002950 RID: 10576
		private static volatile CategoryAttribute focus;

		// Token: 0x04002951 RID: 10577
		private static volatile CategoryAttribute windowStyle;

		// Token: 0x04002952 RID: 10578
		private static volatile CategoryAttribute dragDrop;

		// Token: 0x04002953 RID: 10579
		private static volatile CategoryAttribute defAttr;

		// Token: 0x04002954 RID: 10580
		private bool localized;

		// Token: 0x04002955 RID: 10581
		private string categoryValue;
	}
}
