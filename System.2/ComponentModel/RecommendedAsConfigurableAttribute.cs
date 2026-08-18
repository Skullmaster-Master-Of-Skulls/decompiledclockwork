using System;

namespace System.ComponentModel
{
	// Token: 0x020005A0 RID: 1440
	[AttributeUsage(AttributeTargets.Property)]
	[Obsolete("Use System.ComponentModel.SettingsBindableAttribute instead to work with the new settings model.")]
	public class RecommendedAsConfigurableAttribute : Attribute
	{
		// Token: 0x06003599 RID: 13721 RVA: 0x000E8DE3 File Offset: 0x000E6FE3
		public RecommendedAsConfigurableAttribute(bool recommendedAsConfigurable)
		{
			this.recommendedAsConfigurable = recommendedAsConfigurable;
		}

		// Token: 0x17000D1C RID: 3356
		// (get) Token: 0x0600359A RID: 13722 RVA: 0x000E8DF2 File Offset: 0x000E6FF2
		public bool RecommendedAsConfigurable
		{
			get
			{
				return this.recommendedAsConfigurable;
			}
		}

		// Token: 0x0600359B RID: 13723 RVA: 0x000E8DFC File Offset: 0x000E6FFC
		public override bool Equals(object obj)
		{
			if (obj == this)
			{
				return true;
			}
			RecommendedAsConfigurableAttribute recommendedAsConfigurableAttribute = obj as RecommendedAsConfigurableAttribute;
			return recommendedAsConfigurableAttribute != null && recommendedAsConfigurableAttribute.RecommendedAsConfigurable == this.recommendedAsConfigurable;
		}

		// Token: 0x0600359C RID: 13724 RVA: 0x000E8E29 File Offset: 0x000E7029
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x0600359D RID: 13725 RVA: 0x000E8E31 File Offset: 0x000E7031
		public override bool IsDefaultAttribute()
		{
			return !this.recommendedAsConfigurable;
		}

		// Token: 0x04002A5A RID: 10842
		private bool recommendedAsConfigurable;

		// Token: 0x04002A5B RID: 10843
		public static readonly RecommendedAsConfigurableAttribute No = new RecommendedAsConfigurableAttribute(false);

		// Token: 0x04002A5C RID: 10844
		public static readonly RecommendedAsConfigurableAttribute Yes = new RecommendedAsConfigurableAttribute(true);

		// Token: 0x04002A5D RID: 10845
		public static readonly RecommendedAsConfigurableAttribute Default = RecommendedAsConfigurableAttribute.No;
	}
}
