using System;
using System.ComponentModel;

namespace Telerik.Web.UI
{
	// Token: 0x020000F0 RID: 240
	[ToolboxItem(false)]
	public class ButtonToggleStateCollection : StronglyTypedStateManagedCollection<ButtonToggleState>
	{
		// Token: 0x06000A0C RID: 2572 RVA: 0x000248F7 File Offset: 0x00022AF7
		public ButtonToggleStateCollection(RadToggleButton container)
		{
			this.Container = container;
		}

		// Token: 0x17000371 RID: 881
		// (get) Token: 0x06000A0D RID: 2573 RVA: 0x00024906 File Offset: 0x00022B06
		// (set) Token: 0x06000A0E RID: 2574 RVA: 0x0002490E File Offset: 0x00022B0E
		internal RadToggleButton Container
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

		// Token: 0x06000A0F RID: 2575 RVA: 0x00024918 File Offset: 0x00022B18
		public virtual void Add(string text)
		{
			ButtonToggleState item = new ButtonToggleState(text);
			this.Add(item);
		}

		// Token: 0x06000A10 RID: 2576 RVA: 0x00024933 File Offset: 0x00022B33
		public override void Remove(ButtonToggleState item)
		{
			base.Remove(item);
		}

		// Token: 0x06000A11 RID: 2577 RVA: 0x0002493C File Offset: 0x00022B3C
		protected override void OnInsertComplete(int index, object value)
		{
			ButtonToggleState buttonToggleState = value as ButtonToggleState;
			buttonToggleState.Container = this._container;
			if (buttonToggleState.Selected && this._container != null)
			{
				this._container.ClearSelection();
				buttonToggleState.Selected = true;
			}
		}

		// Token: 0x06000A12 RID: 2578 RVA: 0x00024980 File Offset: 0x00022B80
		protected override void OnClear()
		{
			foreach (object obj in this)
			{
				ButtonToggleState buttonToggleState = (ButtonToggleState)obj;
				buttonToggleState.Container = null;
			}
			base.OnClear();
		}

		// Token: 0x06000A13 RID: 2579 RVA: 0x000249DC File Offset: 0x00022BDC
		protected override void OnRemoveComplete(int index, object value)
		{
			((ButtonToggleState)value).Container = null;
		}

		// Token: 0x06000A14 RID: 2580 RVA: 0x000249EC File Offset: 0x00022BEC
		protected override void SetDirtyObject(object o)
		{
			StateManager stateManager = o as StateManager;
			if (stateManager != null)
			{
				stateManager.SetDirty();
			}
		}

		// Token: 0x04000278 RID: 632
		private RadToggleButton _container;
	}
}
