using System;

namespace System.ComponentModel
{
	// Token: 0x020005C4 RID: 1476
	[AttributeUsage(AttributeTargets.All)]
	public sealed class RefreshPropertiesAttribute : Attribute
	{
		// Token: 0x06003740 RID: 14144 RVA: 0x000F0321 File Offset: 0x000EE521
		public RefreshPropertiesAttribute(RefreshProperties refresh)
		{
			this.refresh = refresh;
		}

		// Token: 0x17000D4E RID: 3406
		// (get) Token: 0x06003741 RID: 14145 RVA: 0x000F0330 File Offset: 0x000EE530
		public RefreshProperties RefreshProperties
		{
			get
			{
				return this.refresh;
			}
		}

		// Token: 0x06003742 RID: 14146 RVA: 0x000F0338 File Offset: 0x000EE538
		public override bool Equals(object value)
		{
			return value is RefreshPropertiesAttribute && ((RefreshPropertiesAttribute)value).RefreshProperties == this.refresh;
		}

		// Token: 0x06003743 RID: 14147 RVA: 0x000F0357 File Offset: 0x000EE557
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x06003744 RID: 14148 RVA: 0x000F035F File Offset: 0x000EE55F
		public override bool IsDefaultAttribute()
		{
			return this.Equals(RefreshPropertiesAttribute.Default);
		}

		// Token: 0x04002AE2 RID: 10978
		public static readonly RefreshPropertiesAttribute All = new RefreshPropertiesAttribute(RefreshProperties.All);

		// Token: 0x04002AE3 RID: 10979
		public static readonly RefreshPropertiesAttribute Repaint = new RefreshPropertiesAttribute(RefreshProperties.Repaint);

		// Token: 0x04002AE4 RID: 10980
		public static readonly RefreshPropertiesAttribute Default = new RefreshPropertiesAttribute(RefreshProperties.None);

		// Token: 0x04002AE5 RID: 10981
		private RefreshProperties refresh;
	}
}
