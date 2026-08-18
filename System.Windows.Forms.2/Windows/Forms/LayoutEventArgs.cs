using System;
using System.ComponentModel;

namespace System.Windows.Forms
{
	// Token: 0x020002BE RID: 702
	public sealed class LayoutEventArgs : EventArgs
	{
		// Token: 0x06002B29 RID: 11049 RVA: 0x000C2350 File Offset: 0x000C0550
		public LayoutEventArgs(IComponent affectedComponent, string affectedProperty)
		{
			this.affectedComponent = affectedComponent;
			this.affectedProperty = affectedProperty;
		}

		// Token: 0x06002B2A RID: 11050 RVA: 0x000C2366 File Offset: 0x000C0566
		public LayoutEventArgs(Control affectedControl, string affectedProperty) : this(affectedControl, affectedProperty)
		{
		}

		// Token: 0x17000A20 RID: 2592
		// (get) Token: 0x06002B2B RID: 11051 RVA: 0x000C2370 File Offset: 0x000C0570
		public IComponent AffectedComponent
		{
			get
			{
				return this.affectedComponent;
			}
		}

		// Token: 0x17000A21 RID: 2593
		// (get) Token: 0x06002B2C RID: 11052 RVA: 0x000C2378 File Offset: 0x000C0578
		public Control AffectedControl
		{
			get
			{
				return this.affectedComponent as Control;
			}
		}

		// Token: 0x17000A22 RID: 2594
		// (get) Token: 0x06002B2D RID: 11053 RVA: 0x000C2385 File Offset: 0x000C0585
		public string AffectedProperty
		{
			get
			{
				return this.affectedProperty;
			}
		}

		// Token: 0x0400122B RID: 4651
		private readonly IComponent affectedComponent;

		// Token: 0x0400122C RID: 4652
		private readonly string affectedProperty;
	}
}
