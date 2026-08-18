using System;

namespace System.ComponentModel
{
	// Token: 0x02000544 RID: 1348
	[AttributeUsage(AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Event)]
	public sealed class DesignerSerializationVisibilityAttribute : Attribute
	{
		// Token: 0x060032C5 RID: 12997 RVA: 0x000E2AA5 File Offset: 0x000E0CA5
		public DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility visibility)
		{
			this.visibility = visibility;
		}

		// Token: 0x17000C6C RID: 3180
		// (get) Token: 0x060032C6 RID: 12998 RVA: 0x000E2AB4 File Offset: 0x000E0CB4
		public DesignerSerializationVisibility Visibility
		{
			get
			{
				return this.visibility;
			}
		}

		// Token: 0x060032C7 RID: 12999 RVA: 0x000E2ABC File Offset: 0x000E0CBC
		public override bool Equals(object obj)
		{
			if (obj == this)
			{
				return true;
			}
			DesignerSerializationVisibilityAttribute designerSerializationVisibilityAttribute = obj as DesignerSerializationVisibilityAttribute;
			return designerSerializationVisibilityAttribute != null && designerSerializationVisibilityAttribute.Visibility == this.visibility;
		}

		// Token: 0x060032C8 RID: 13000 RVA: 0x000E2AE9 File Offset: 0x000E0CE9
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x060032C9 RID: 13001 RVA: 0x000E2AF1 File Offset: 0x000E0CF1
		public override bool IsDefaultAttribute()
		{
			return this.Equals(DesignerSerializationVisibilityAttribute.Default);
		}

		// Token: 0x04002996 RID: 10646
		public static readonly DesignerSerializationVisibilityAttribute Content = new DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Content);

		// Token: 0x04002997 RID: 10647
		public static readonly DesignerSerializationVisibilityAttribute Hidden = new DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Hidden);

		// Token: 0x04002998 RID: 10648
		public static readonly DesignerSerializationVisibilityAttribute Visible = new DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Visible);

		// Token: 0x04002999 RID: 10649
		public static readonly DesignerSerializationVisibilityAttribute Default = DesignerSerializationVisibilityAttribute.Visible;

		// Token: 0x0400299A RID: 10650
		private DesignerSerializationVisibility visibility;
	}
}
