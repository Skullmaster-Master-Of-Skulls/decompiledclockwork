using System;

namespace System.Windows.Forms.Design
{
	// Token: 0x0200048F RID: 1167
	[AttributeUsage(AttributeTargets.Class)]
	public sealed class ToolStripItemDesignerAvailabilityAttribute : Attribute
	{
		// Token: 0x06004E42 RID: 20034 RVA: 0x00142A3F File Offset: 0x00140C3F
		public ToolStripItemDesignerAvailabilityAttribute()
		{
			this.visibility = ToolStripItemDesignerAvailability.None;
		}

		// Token: 0x06004E43 RID: 20035 RVA: 0x00142A4E File Offset: 0x00140C4E
		public ToolStripItemDesignerAvailabilityAttribute(ToolStripItemDesignerAvailability visibility)
		{
			this.visibility = visibility;
		}

		// Token: 0x1700133C RID: 4924
		// (get) Token: 0x06004E44 RID: 20036 RVA: 0x00142A5D File Offset: 0x00140C5D
		public ToolStripItemDesignerAvailability ItemAdditionVisibility
		{
			get
			{
				return this.visibility;
			}
		}

		// Token: 0x06004E45 RID: 20037 RVA: 0x00142A68 File Offset: 0x00140C68
		public override bool Equals(object obj)
		{
			if (obj == this)
			{
				return true;
			}
			ToolStripItemDesignerAvailabilityAttribute toolStripItemDesignerAvailabilityAttribute = obj as ToolStripItemDesignerAvailabilityAttribute;
			return toolStripItemDesignerAvailabilityAttribute != null && toolStripItemDesignerAvailabilityAttribute.ItemAdditionVisibility == this.visibility;
		}

		// Token: 0x06004E46 RID: 20038 RVA: 0x00142A95 File Offset: 0x00140C95
		public override int GetHashCode()
		{
			return this.visibility.GetHashCode();
		}

		// Token: 0x06004E47 RID: 20039 RVA: 0x00142AA8 File Offset: 0x00140CA8
		public override bool IsDefaultAttribute()
		{
			return this.Equals(ToolStripItemDesignerAvailabilityAttribute.Default);
		}

		// Token: 0x04003404 RID: 13316
		private ToolStripItemDesignerAvailability visibility;

		// Token: 0x04003405 RID: 13317
		public static readonly ToolStripItemDesignerAvailabilityAttribute Default = new ToolStripItemDesignerAvailabilityAttribute();
	}
}
