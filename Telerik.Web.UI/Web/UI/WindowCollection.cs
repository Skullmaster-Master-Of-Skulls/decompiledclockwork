using System;

namespace Telerik.Web.UI
{
	// Token: 0x02001369 RID: 4969
	public class WindowCollection : StronglyTypedStateManagedCollection<RadWindow>
	{
		// Token: 0x170042B7 RID: 17079
		// (get) Token: 0x0600CF8B RID: 53131 RVA: 0x002E0C2E File Offset: 0x002DEE2E
		protected RadWindowManager Parent
		{
			get
			{
				return this._parent;
			}
		}

		// Token: 0x0600CF8C RID: 53132 RVA: 0x002E0C36 File Offset: 0x002DEE36
		public new virtual void Add(RadWindow control)
		{
			this._parent.ConfigureWindow(control);
			base.Add(control);
			this._parent.Controls.Add(control);
		}

		// Token: 0x0600CF8D RID: 53133 RVA: 0x002E0C5C File Offset: 0x002DEE5C
		public WindowCollection(RadWindowManager parent)
		{
			this._parent = parent;
		}

		// Token: 0x0600CF8E RID: 53134 RVA: 0x002E0C6B File Offset: 0x002DEE6B
		protected override void SetDirtyObject(object o)
		{
			if (o is RadWindow)
			{
				((IMarkableStateManager)o).SetDirty();
			}
		}

		// Token: 0x0600CF8F RID: 53135 RVA: 0x002E0C80 File Offset: 0x002DEE80
		public new virtual void Remove(RadWindow control)
		{
			base.Remove(control);
			this._parent.Controls.Remove(control);
		}

		// Token: 0x0600CF90 RID: 53136 RVA: 0x002E0C9A File Offset: 0x002DEE9A
		public new virtual void RemoveAt(int index)
		{
			base.RemoveAt(index);
			this._parent.Controls.RemoveAt(index);
		}

		// Token: 0x0600CF91 RID: 53137 RVA: 0x002E0CB4 File Offset: 0x002DEEB4
		public new virtual void Clear()
		{
			base.Clear();
			this._parent.Controls.Clear();
		}

		// Token: 0x040037A4 RID: 14244
		private readonly RadWindowManager _parent;
	}
}
