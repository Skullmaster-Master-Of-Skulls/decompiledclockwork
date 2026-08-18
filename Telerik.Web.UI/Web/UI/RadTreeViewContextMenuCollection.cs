using System;
using System.Collections;
using System.Collections.Generic;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x02001B5A RID: 7002
	public class RadTreeViewContextMenuCollection : StateManagedCollection
	{
		// Token: 0x06010F60 RID: 69472 RVA: 0x003C10AB File Offset: 0x003BF2AB
		public RadTreeViewContextMenuCollection(RadTreeView treeView)
		{
			this._treeView = treeView;
		}

		// Token: 0x170052CB RID: 21195
		public RadTreeViewContextMenu this[int index]
		{
			get
			{
				return (RadTreeViewContextMenu)this.List[index];
			}
			set
			{
				this.List[index] = value;
			}
		}

		// Token: 0x170052CC RID: 21196
		// (get) Token: 0x06010F63 RID: 69475 RVA: 0x003C10DC File Offset: 0x003BF2DC
		private IList List
		{
			get
			{
				return this;
			}
		}

		// Token: 0x06010F64 RID: 69476 RVA: 0x003C10DF File Offset: 0x003BF2DF
		public void Add(RadTreeViewContextMenu target)
		{
			this.List.Add(target);
		}

		// Token: 0x06010F65 RID: 69477 RVA: 0x003C10F0 File Offset: 0x003BF2F0
		protected override void OnInsertComplete(int index, object value)
		{
			base.OnInsertComplete(index, value);
			index = this.AdjustInsertedNodeIndexDependingOnContextMenusCount(index);
			RadTreeViewContextMenu radTreeViewContextMenu = (RadTreeViewContextMenu)value;
			if (string.IsNullOrEmpty(radTreeViewContextMenu.ID))
			{
				radTreeViewContextMenu.ID = radTreeViewContextMenu.GetType().Name + this._treeView.ContextMenus.Count;
			}
			this._treeView.Controls.AddAt(index, radTreeViewContextMenu);
		}

		// Token: 0x06010F66 RID: 69478 RVA: 0x003C115F File Offset: 0x003BF35F
		private int AdjustInsertedNodeIndexDependingOnContextMenusCount(int index)
		{
			if (index > -1 && index < this._treeView.Nodes.Count)
			{
				index += this._treeView.Nodes.Count;
			}
			return index;
		}

		// Token: 0x06010F67 RID: 69479 RVA: 0x003C118D File Offset: 0x003BF38D
		protected override void OnRemoveComplete(int index, object value)
		{
			base.OnRemoveComplete(index, value);
			index = this.AdjustRemovedNodeIndexDependingOnContextMenusCount(index);
			this._treeView.Controls.RemoveAt(index);
		}

		// Token: 0x06010F68 RID: 69480 RVA: 0x003C11B1 File Offset: 0x003BF3B1
		private int AdjustRemovedNodeIndexDependingOnContextMenusCount(int index)
		{
			index += this._treeView.Nodes.Count;
			return index;
		}

		// Token: 0x06010F69 RID: 69481 RVA: 0x003C11C8 File Offset: 0x003BF3C8
		protected override void OnClear()
		{
			foreach (object obj in this)
			{
				RadTreeViewContextMenu value = (RadTreeViewContextMenu)obj;
				this._treeView.Controls.Remove(value);
			}
		}

		// Token: 0x06010F6A RID: 69482 RVA: 0x003C1228 File Offset: 0x003BF428
		public bool Contains(RadTreeViewContextMenu target)
		{
			return this.List.Contains(target);
		}

		// Token: 0x06010F6B RID: 69483 RVA: 0x003C1238 File Offset: 0x003BF438
		internal bool ContainsID(string contextMenuID)
		{
			RadTreeViewContextMenu radTreeViewContextMenu = this.FindById(contextMenuID);
			return radTreeViewContextMenu != null;
		}

		// Token: 0x06010F6C RID: 69484 RVA: 0x003C1254 File Offset: 0x003BF454
		internal RadTreeViewContextMenu FindById(string contextMenuID)
		{
			foreach (object obj in this)
			{
				RadTreeViewContextMenu radTreeViewContextMenu = (RadTreeViewContextMenu)obj;
				if (radTreeViewContextMenu.ID == contextMenuID)
				{
					return radTreeViewContextMenu;
				}
			}
			return null;
		}

		// Token: 0x06010F6D RID: 69485 RVA: 0x003C12B8 File Offset: 0x003BF4B8
		internal RadTreeViewContextMenu FindByClientId(string contextMenuClientID)
		{
			foreach (object obj in this)
			{
				RadTreeViewContextMenu radTreeViewContextMenu = (RadTreeViewContextMenu)obj;
				if (radTreeViewContextMenu.ClientID == contextMenuClientID)
				{
					return radTreeViewContextMenu;
				}
			}
			return null;
		}

		// Token: 0x06010F6E RID: 69486 RVA: 0x003C131C File Offset: 0x003BF51C
		public void CopyTo(RadTreeViewContextMenu[] array, int index)
		{
			this.List.CopyTo(array, index);
		}

		// Token: 0x06010F6F RID: 69487 RVA: 0x003C132C File Offset: 0x003BF52C
		public void AddRange(IEnumerable<RadTreeViewContextMenu> contextMenus)
		{
			foreach (RadTreeViewContextMenu target in contextMenus)
			{
				this.Add(target);
			}
		}

		// Token: 0x06010F70 RID: 69488 RVA: 0x003C1374 File Offset: 0x003BF574
		public int IndexOf(RadTreeViewContextMenu target)
		{
			return this.List.IndexOf(target);
		}

		// Token: 0x06010F71 RID: 69489 RVA: 0x003C1382 File Offset: 0x003BF582
		public void Insert(int index, RadTreeViewContextMenu target)
		{
			this.List.Insert(index, target);
		}

		// Token: 0x06010F72 RID: 69490 RVA: 0x003C1391 File Offset: 0x003BF591
		public void Remove(RadTreeViewContextMenu target)
		{
			this.List.Remove(target);
		}

		// Token: 0x06010F73 RID: 69491 RVA: 0x003C139F File Offset: 0x003BF59F
		public void RemoveAt(int index)
		{
			this.List.RemoveAt(index);
		}

		// Token: 0x06010F74 RID: 69492 RVA: 0x003C13AD File Offset: 0x003BF5AD
		protected override void SetDirtyObject(object o)
		{
			((IMarkableStateManager)o).SetDirty();
		}

		// Token: 0x04004BE1 RID: 19425
		private readonly RadTreeView _treeView;
	}
}
