using System;
using System.Collections.ObjectModel;

namespace System.Web.UI
{
	// Token: 0x02000088 RID: 136
	public class UpdatePanelTriggerCollection : Collection<UpdatePanelTrigger>
	{
		// Token: 0x060005D7 RID: 1495 RVA: 0x0001AA39 File Offset: 0x00018C39
		public UpdatePanelTriggerCollection(UpdatePanel owner)
		{
			if (owner == null)
			{
				throw new ArgumentNullException("owner");
			}
			this._owner = owner;
		}

		// Token: 0x170001A3 RID: 419
		// (get) Token: 0x060005D8 RID: 1496 RVA: 0x0001AA56 File Offset: 0x00018C56
		public UpdatePanel Owner
		{
			get
			{
				return this._owner;
			}
		}

		// Token: 0x060005D9 RID: 1497 RVA: 0x0001AA60 File Offset: 0x00018C60
		protected override void ClearItems()
		{
			foreach (UpdatePanelTrigger updatePanelTrigger in this)
			{
				updatePanelTrigger.SetOwner(null);
			}
			base.ClearItems();
		}

		// Token: 0x060005DA RID: 1498 RVA: 0x0001AAB0 File Offset: 0x00018CB0
		internal bool HasTriggered()
		{
			foreach (UpdatePanelTrigger updatePanelTrigger in this)
			{
				if (updatePanelTrigger.HasTriggered())
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060005DB RID: 1499 RVA: 0x0001AB00 File Offset: 0x00018D00
		internal void Initialize()
		{
			foreach (UpdatePanelTrigger updatePanelTrigger in this)
			{
				updatePanelTrigger.Initialize();
			}
			this._initialized = true;
		}

		// Token: 0x060005DC RID: 1500 RVA: 0x0001AB50 File Offset: 0x00018D50
		protected override void InsertItem(int index, UpdatePanelTrigger item)
		{
			item.SetOwner(this.Owner);
			if (this._initialized)
			{
				item.Initialize();
			}
			base.InsertItem(index, item);
		}

		// Token: 0x060005DD RID: 1501 RVA: 0x0001AB74 File Offset: 0x00018D74
		protected override void RemoveItem(int index)
		{
			base[index].SetOwner(null);
			base.RemoveItem(index);
		}

		// Token: 0x060005DE RID: 1502 RVA: 0x0001AB8A File Offset: 0x00018D8A
		protected override void SetItem(int index, UpdatePanelTrigger item)
		{
			base[index].SetOwner(null);
			item.SetOwner(this.Owner);
			if (this._initialized)
			{
				item.Initialize();
			}
			base.SetItem(index, item);
		}

		// Token: 0x0400021C RID: 540
		private bool _initialized;

		// Token: 0x0400021D RID: 541
		private UpdatePanel _owner;
	}
}
