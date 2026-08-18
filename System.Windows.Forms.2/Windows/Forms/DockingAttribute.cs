using System;

namespace System.Windows.Forms
{
	// Token: 0x02000230 RID: 560
	[AttributeUsage(AttributeTargets.Class)]
	public sealed class DockingAttribute : Attribute
	{
		// Token: 0x06002473 RID: 9331 RVA: 0x000AC3D8 File Offset: 0x000AA5D8
		public DockingAttribute()
		{
			this.dockingBehavior = DockingBehavior.Never;
		}

		// Token: 0x06002474 RID: 9332 RVA: 0x000AC3E7 File Offset: 0x000AA5E7
		public DockingAttribute(DockingBehavior dockingBehavior)
		{
			this.dockingBehavior = dockingBehavior;
		}

		// Token: 0x1700084F RID: 2127
		// (get) Token: 0x06002475 RID: 9333 RVA: 0x000AC3F6 File Offset: 0x000AA5F6
		public DockingBehavior DockingBehavior
		{
			get
			{
				return this.dockingBehavior;
			}
		}

		// Token: 0x06002476 RID: 9334 RVA: 0x000AC400 File Offset: 0x000AA600
		public override bool Equals(object obj)
		{
			if (obj == this)
			{
				return true;
			}
			DockingAttribute dockingAttribute = obj as DockingAttribute;
			return dockingAttribute != null && dockingAttribute.DockingBehavior == this.dockingBehavior;
		}

		// Token: 0x06002477 RID: 9335 RVA: 0x000AC42D File Offset: 0x000AA62D
		public override int GetHashCode()
		{
			return this.dockingBehavior.GetHashCode();
		}

		// Token: 0x06002478 RID: 9336 RVA: 0x000AC440 File Offset: 0x000AA640
		public override bool IsDefaultAttribute()
		{
			return this.Equals(DockingAttribute.Default);
		}

		// Token: 0x04000EFF RID: 3839
		private DockingBehavior dockingBehavior;

		// Token: 0x04000F00 RID: 3840
		public static readonly DockingAttribute Default = new DockingAttribute();
	}
}
