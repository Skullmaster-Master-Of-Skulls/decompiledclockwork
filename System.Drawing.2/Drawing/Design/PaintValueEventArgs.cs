using System;
using System.ComponentModel;
using System.Security.Permissions;

namespace System.Drawing.Design
{
	// Token: 0x02000077 RID: 119
	[PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
	[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
	public class PaintValueEventArgs : EventArgs
	{
		// Token: 0x06000857 RID: 2135 RVA: 0x00020D58 File Offset: 0x0001EF58
		public PaintValueEventArgs(ITypeDescriptorContext context, object value, Graphics graphics, Rectangle bounds)
		{
			this.context = context;
			this.valueToPaint = value;
			this.graphics = graphics;
			if (graphics == null)
			{
				throw new ArgumentNullException("graphics");
			}
			this.bounds = bounds;
		}

		// Token: 0x1700031D RID: 797
		// (get) Token: 0x06000858 RID: 2136 RVA: 0x00020D8B File Offset: 0x0001EF8B
		public Rectangle Bounds
		{
			get
			{
				return this.bounds;
			}
		}

		// Token: 0x1700031E RID: 798
		// (get) Token: 0x06000859 RID: 2137 RVA: 0x00020D93 File Offset: 0x0001EF93
		public ITypeDescriptorContext Context
		{
			get
			{
				return this.context;
			}
		}

		// Token: 0x1700031F RID: 799
		// (get) Token: 0x0600085A RID: 2138 RVA: 0x00020D9B File Offset: 0x0001EF9B
		public Graphics Graphics
		{
			get
			{
				return this.graphics;
			}
		}

		// Token: 0x17000320 RID: 800
		// (get) Token: 0x0600085B RID: 2139 RVA: 0x00020DA3 File Offset: 0x0001EFA3
		public object Value
		{
			get
			{
				return this.valueToPaint;
			}
		}

		// Token: 0x04000705 RID: 1797
		private readonly ITypeDescriptorContext context;

		// Token: 0x04000706 RID: 1798
		private readonly object valueToPaint;

		// Token: 0x04000707 RID: 1799
		private readonly Graphics graphics;

		// Token: 0x04000708 RID: 1800
		private readonly Rectangle bounds;
	}
}
