using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Telerik.Web.UI.PivotGrid.Core
{
	// Token: 0x02000C98 RID: 3224
	public class SettingsNodeCollection<T> : Collection<T>, IList<T>, ICollection<!0>, IList, ICollection, IEnumerable<!0>, IEnumerable where T : SettingsNode
	{
		// Token: 0x06007944 RID: 31044 RVA: 0x001BE3C5 File Offset: 0x001BC5C5
		public SettingsNodeCollection(SettingsNode parent)
		{
			if (parent == null)
			{
				throw new ArgumentNullException("parent");
			}
			this.Parent = parent;
		}

		// Token: 0x1700271D RID: 10013
		// (get) Token: 0x06007945 RID: 31045 RVA: 0x001BE3E2 File Offset: 0x001BC5E2
		// (set) Token: 0x06007946 RID: 31046 RVA: 0x001BE3EA File Offset: 0x001BC5EA
		public SettingsNode Parent { get; private set; }

		// Token: 0x06007947 RID: 31047 RVA: 0x001BE3F3 File Offset: 0x001BC5F3
		protected override void SetItem(int index, T item)
		{
			this.Parent.RemoveSettingsChild(base[index]);
			this.Parent.AddSettingsChild(item);
			base.SetItem(index, item);
		}

		// Token: 0x06007948 RID: 31048 RVA: 0x001BE425 File Offset: 0x001BC625
		protected override void RemoveItem(int index)
		{
			this.Parent.RemoveSettingsChild(base[index]);
			base.RemoveItem(index);
		}

		// Token: 0x06007949 RID: 31049 RVA: 0x001BE445 File Offset: 0x001BC645
		protected override void InsertItem(int index, T item)
		{
			this.Parent.AddSettingsChild(item);
			base.InsertItem(index, item);
		}

		// Token: 0x0600794A RID: 31050 RVA: 0x001BE460 File Offset: 0x001BC660
		protected override void ClearItems()
		{
			foreach (T t in this)
			{
				this.Parent.RemoveSettingsChild(t);
			}
			base.ClearItems();
		}

		// Token: 0x0600794B RID: 31051 RVA: 0x001BE4B8 File Offset: 0x001BC6B8
		internal void CloneItemsFrom(SettingsNodeCollection<T> original)
		{
			base.Clear();
			foreach (T t in original)
			{
				base.Add((T)((object)t.Clone()));
			}
		}

		// Token: 0x0600794C RID: 31052 RVA: 0x001BE518 File Offset: 0x001BC718
		protected void NotifyChange(SettingsChangedEventArgs settingsEventArgs)
		{
			this.Parent.NotifySettingsChanged(settingsEventArgs);
		}
	}
}
