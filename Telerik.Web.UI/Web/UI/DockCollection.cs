using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Telerik.Web.UI.Dock;

namespace Telerik.Web.UI
{
	// Token: 0x0200103F RID: 4159
	public class DockCollection : Collection<RadDock>
	{
		// Token: 0x0600A3A9 RID: 41897 RVA: 0x002469C5 File Offset: 0x00244BC5
		public DockCollection(RadDockZone zone)
		{
			this._zone = zone;
		}

		// Token: 0x0600A3AA RID: 41898 RVA: 0x002469D4 File Offset: 0x00244BD4
		protected override void InsertItem(int index, RadDock dock)
		{
			if (!this.IsDockAllowed(dock))
			{
				throw new NotAllowedDockException(this._zone, dock);
			}
			base.InsertItem(index, dock);
			dock.DockZone = this._zone;
			this.ResetDockIndices();
		}

		// Token: 0x0600A3AB RID: 41899 RVA: 0x00246A08 File Offset: 0x00244C08
		protected override void RemoveItem(int index)
		{
			RadDock radDock = base.Items[index];
			radDock.DockZone = null;
			base.RemoveItem(index);
			this.ResetDockIndices();
		}

		// Token: 0x0600A3AC RID: 41900 RVA: 0x00246A36 File Offset: 0x00244C36
		protected override void SetItem(int index, RadDock item)
		{
			if (!this.IsDockAllowed(item))
			{
				throw new NotAllowedDockException(this._zone, item);
			}
			base.SetItem(index, item);
			item.DockZone = this._zone;
			this.ResetDockIndices();
		}

		// Token: 0x0600A3AD RID: 41901 RVA: 0x00246A68 File Offset: 0x00244C68
		public void Sort(Comparison<RadDock> comparison)
		{
			(base.Items as List<RadDock>).Sort(comparison);
			this.ResetDockIndices();
		}

		// Token: 0x0600A3AE RID: 41902 RVA: 0x00246A84 File Offset: 0x00244C84
		private void ResetDockIndices()
		{
			for (int i = 0; i < base.Items.Count; i++)
			{
				base.Items[i].Index = i;
			}
		}

		// Token: 0x0600A3AF RID: 41903 RVA: 0x00246ABC File Offset: 0x00244CBC
		private bool IsDockAllowed(RadDock dock)
		{
			string[] allowedDocks = this._zone.AllowedDocks;
			return allowedDocks.Length == 0 || allowedDocks.Contains(dock.UniqueName);
		}

		// Token: 0x04002D8A RID: 11658
		private readonly RadDockZone _zone;
	}
}
