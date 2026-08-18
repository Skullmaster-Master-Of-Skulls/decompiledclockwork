using System;

namespace System.Windows.Forms
{
	// Token: 0x0200032F RID: 815
	internal class PropertyGridToolStrip : ToolStrip
	{
		// Token: 0x06003526 RID: 13606 RVA: 0x000F1840 File Offset: 0x000EFA40
		public PropertyGridToolStrip(PropertyGrid parentPropertyGrid)
		{
			this._parentPropertyGrid = parentPropertyGrid;
		}

		// Token: 0x17000CD7 RID: 3287
		// (get) Token: 0x06003527 RID: 13607 RVA: 0x00013062 File Offset: 0x00011262
		internal override bool SupportsUiaProviders
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06003528 RID: 13608 RVA: 0x000F184F File Offset: 0x000EFA4F
		protected override AccessibleObject CreateAccessibilityInstance()
		{
			return new PropertyGridToolStripAccessibleObject(this, this._parentPropertyGrid);
		}

		// Token: 0x04001F45 RID: 8005
		private PropertyGrid _parentPropertyGrid;
	}
}
