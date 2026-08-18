using System;
using System.Collections;
using System.Collections.Generic;
using System.Web.UI;

namespace Telerik.Web
{
	// Token: 0x0200000E RID: 14
	public abstract class StronglyTypedStateManagedCollection<ItemType> : StateManagedCollection where ItemType : IMarkableStateManager
	{
		// Token: 0x1700005F RID: 95
		public virtual ItemType this[int index]
		{
			get
			{
				return (ItemType)((object)this.List[index]);
			}
			set
			{
				this.List[index] = value;
			}
		}

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x060000FE RID: 254 RVA: 0x0000382D File Offset: 0x00001A2D
		protected IList List
		{
			get
			{
				return this;
			}
		}

		// Token: 0x060000FF RID: 255 RVA: 0x00003830 File Offset: 0x00001A30
		public virtual void Add(ItemType item)
		{
			this.List.Add(item);
		}

		// Token: 0x06000100 RID: 256 RVA: 0x00003844 File Offset: 0x00001A44
		public virtual bool Contains(ItemType item)
		{
			return this.List.Contains(item);
		}

		// Token: 0x06000101 RID: 257 RVA: 0x00003857 File Offset: 0x00001A57
		public virtual void CopyTo(ItemType[] array, int index)
		{
			this.List.CopyTo(array, index);
		}

		// Token: 0x06000102 RID: 258 RVA: 0x00003868 File Offset: 0x00001A68
		public virtual void AddRange(IEnumerable<ItemType> items)
		{
			foreach (ItemType item in items)
			{
				this.Add(item);
			}
		}

		// Token: 0x06000103 RID: 259 RVA: 0x000038B0 File Offset: 0x00001AB0
		public virtual int IndexOf(ItemType item)
		{
			return this.List.IndexOf(item);
		}

		// Token: 0x06000104 RID: 260 RVA: 0x000038C3 File Offset: 0x00001AC3
		public virtual void Insert(int index, ItemType item)
		{
			this.List.Insert(index, item);
		}

		// Token: 0x06000105 RID: 261 RVA: 0x000038D7 File Offset: 0x00001AD7
		public virtual void Remove(ItemType item)
		{
			this.List.Remove(item);
		}

		// Token: 0x06000106 RID: 262 RVA: 0x000038EA File Offset: 0x00001AEA
		public virtual void RemoveAt(int index)
		{
			this.List.RemoveAt(index);
		}

		// Token: 0x06000107 RID: 263 RVA: 0x000038F8 File Offset: 0x00001AF8
		protected override void SetDirtyObject(object o)
		{
			IMarkableStateManager markableStateManager = o as IMarkableStateManager;
			if (markableStateManager != null)
			{
				markableStateManager.SetDirty();
			}
		}
	}
}
