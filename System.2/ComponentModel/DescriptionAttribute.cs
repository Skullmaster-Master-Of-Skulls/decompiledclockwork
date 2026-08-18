using System;

namespace System.ComponentModel
{
	// Token: 0x02000540 RID: 1344
	[AttributeUsage(AttributeTargets.All)]
	public class DescriptionAttribute : Attribute
	{
		// Token: 0x060032AA RID: 12970 RVA: 0x000E278F File Offset: 0x000E098F
		public DescriptionAttribute() : this(string.Empty)
		{
		}

		// Token: 0x060032AB RID: 12971 RVA: 0x000E279C File Offset: 0x000E099C
		public DescriptionAttribute(string description)
		{
			this.description = description;
		}

		// Token: 0x17000C65 RID: 3173
		// (get) Token: 0x060032AC RID: 12972 RVA: 0x000E27AB File Offset: 0x000E09AB
		public virtual string Description
		{
			get
			{
				return this.DescriptionValue;
			}
		}

		// Token: 0x17000C66 RID: 3174
		// (get) Token: 0x060032AD RID: 12973 RVA: 0x000E27B3 File Offset: 0x000E09B3
		// (set) Token: 0x060032AE RID: 12974 RVA: 0x000E27BB File Offset: 0x000E09BB
		protected string DescriptionValue
		{
			get
			{
				return this.description;
			}
			set
			{
				this.description = value;
			}
		}

		// Token: 0x060032AF RID: 12975 RVA: 0x000E27C4 File Offset: 0x000E09C4
		public override bool Equals(object obj)
		{
			if (obj == this)
			{
				return true;
			}
			DescriptionAttribute descriptionAttribute = obj as DescriptionAttribute;
			return descriptionAttribute != null && descriptionAttribute.Description == this.Description;
		}

		// Token: 0x060032B0 RID: 12976 RVA: 0x000E27F4 File Offset: 0x000E09F4
		public override int GetHashCode()
		{
			return this.Description.GetHashCode();
		}

		// Token: 0x060032B1 RID: 12977 RVA: 0x000E2801 File Offset: 0x000E0A01
		public override bool IsDefaultAttribute()
		{
			return this.Equals(DescriptionAttribute.Default);
		}

		// Token: 0x04002987 RID: 10631
		public static readonly DescriptionAttribute Default = new DescriptionAttribute();

		// Token: 0x04002988 RID: 10632
		private string description;
	}
}
