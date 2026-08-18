using System;
using System.ComponentModel;

namespace Telerik.Web.UI
{
	// Token: 0x02000A2A RID: 2602
	[ToolboxItem(false)]
	public class RadButtonToggleStateCollection : StronglyTypedStateManagedCollection<RadButtonToggleState>
	{
		// Token: 0x060062A4 RID: 25252 RVA: 0x0017386C File Offset: 0x00171A6C
		public RadButtonToggleStateCollection(RadButton container)
		{
			this.Container = container;
		}

		// Token: 0x17002059 RID: 8281
		// (get) Token: 0x060062A5 RID: 25253 RVA: 0x0017387B File Offset: 0x00171A7B
		// (set) Token: 0x060062A6 RID: 25254 RVA: 0x00173883 File Offset: 0x00171A83
		internal RadButton Container
		{
			get
			{
				return this._container;
			}
			set
			{
				this._container = value;
			}
		}

		// Token: 0x060062A7 RID: 25255 RVA: 0x0017388C File Offset: 0x00171A8C
		public virtual void Add(string text)
		{
			RadButtonToggleState item = new RadButtonToggleState(text);
			this.Add(item);
		}

		// Token: 0x060062A8 RID: 25256 RVA: 0x001738A7 File Offset: 0x00171AA7
		public override void Remove(RadButtonToggleState item)
		{
			base.Remove(item);
		}

		// Token: 0x060062A9 RID: 25257 RVA: 0x001738B0 File Offset: 0x00171AB0
		protected override void OnInsertComplete(int index, object value)
		{
			RadButtonToggleState radButtonToggleState = value as RadButtonToggleState;
			radButtonToggleState.Container = this._container;
			if (radButtonToggleState.Selected && this._container != null)
			{
				this._container.ClearSelection();
				radButtonToggleState.Selected = true;
			}
		}

		// Token: 0x060062AA RID: 25258 RVA: 0x001738F4 File Offset: 0x00171AF4
		protected override void OnClear()
		{
			foreach (object obj in this)
			{
				RadButtonToggleState radButtonToggleState = (RadButtonToggleState)obj;
				radButtonToggleState.Container = null;
			}
			base.OnClear();
		}

		// Token: 0x060062AB RID: 25259 RVA: 0x00173950 File Offset: 0x00171B50
		protected override void OnRemoveComplete(int index, object value)
		{
			((RadButtonToggleState)value).Container = null;
		}

		// Token: 0x060062AC RID: 25260 RVA: 0x0017395E File Offset: 0x00171B5E
		protected override void SetDirtyObject(object o)
		{
			((StateManager)o).SetDirty();
		}

		// Token: 0x04001810 RID: 6160
		private RadButton _container;
	}
}
