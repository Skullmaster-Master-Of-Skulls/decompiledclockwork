using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Security.Permissions;
using System.Windows.Forms.Layout;

namespace System.Windows.Forms
{
	// Token: 0x020003D1 RID: 977
	[Editor("System.Windows.Forms.Design.ToolStripCollectionEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
	[ListBindable(false)]
	[UIPermission(SecurityAction.InheritanceDemand, Window = UIPermissionWindow.AllWindows)]
	public class ToolStripItemCollection : ArrangedElementCollection, IList, ICollection, IEnumerable
	{
		// Token: 0x06004318 RID: 17176 RVA: 0x0011C890 File Offset: 0x0011AA90
		internal ToolStripItemCollection(ToolStrip owner, bool itemsCollection) : this(owner, itemsCollection, false)
		{
		}

		// Token: 0x06004319 RID: 17177 RVA: 0x0011C89B File Offset: 0x0011AA9B
		internal ToolStripItemCollection(ToolStrip owner, bool itemsCollection, bool isReadOnly)
		{
			this.lastAccessedIndex = -1;
			base..ctor();
			this.owner = owner;
			this.itemsCollection = itemsCollection;
			this.isReadOnly = isReadOnly;
		}

		// Token: 0x0600431A RID: 17178 RVA: 0x0011C8BF File Offset: 0x0011AABF
		public ToolStripItemCollection(ToolStrip owner, ToolStripItem[] value)
		{
			this.lastAccessedIndex = -1;
			base..ctor();
			if (owner == null)
			{
				throw new ArgumentNullException("owner");
			}
			this.owner = owner;
			this.AddRange(value);
		}

		// Token: 0x17001062 RID: 4194
		public virtual ToolStripItem this[int index]
		{
			get
			{
				return (ToolStripItem)base.InnerList[index];
			}
		}

		// Token: 0x17001063 RID: 4195
		public virtual ToolStripItem this[string key]
		{
			get
			{
				if (key == null || key.Length == 0)
				{
					return null;
				}
				int index = this.IndexOfKey(key);
				if (this.IsValidIndex(index))
				{
					return (ToolStripItem)base.InnerList[index];
				}
				return null;
			}
		}

		// Token: 0x0600431D RID: 17181 RVA: 0x0011C93E File Offset: 0x0011AB3E
		public ToolStripItem Add(string text)
		{
			return this.Add(text, null, null);
		}

		// Token: 0x0600431E RID: 17182 RVA: 0x0011C949 File Offset: 0x0011AB49
		public ToolStripItem Add(Image image)
		{
			return this.Add(null, image, null);
		}

		// Token: 0x0600431F RID: 17183 RVA: 0x0011C954 File Offset: 0x0011AB54
		public ToolStripItem Add(string text, Image image)
		{
			return this.Add(text, image, null);
		}

		// Token: 0x06004320 RID: 17184 RVA: 0x0011C960 File Offset: 0x0011AB60
		public ToolStripItem Add(string text, Image image, EventHandler onClick)
		{
			ToolStripItem toolStripItem = this.owner.CreateDefaultItem(text, image, onClick);
			this.Add(toolStripItem);
			return toolStripItem;
		}

		// Token: 0x06004321 RID: 17185 RVA: 0x0011C988 File Offset: 0x0011AB88
		public int Add(ToolStripItem value)
		{
			this.CheckCanAddOrInsertItem(value);
			this.SetOwner(value);
			int result = base.InnerList.Add(value);
			if (this.itemsCollection && this.owner != null)
			{
				this.owner.OnItemAddedInternal(value);
				this.owner.OnItemAdded(new ToolStripItemEventArgs(value));
			}
			return result;
		}

		// Token: 0x06004322 RID: 17186 RVA: 0x0011C9E0 File Offset: 0x0011ABE0
		public void AddRange(ToolStripItem[] toolStripItems)
		{
			if (toolStripItems == null)
			{
				throw new ArgumentNullException("toolStripItems");
			}
			if (this.IsReadOnly)
			{
				throw new NotSupportedException(SR.GetString("ToolStripItemCollectionIsReadOnly"));
			}
			using (new LayoutTransaction(this.owner, this.owner, PropertyNames.Items))
			{
				for (int i = 0; i < toolStripItems.Length; i++)
				{
					this.Add(toolStripItems[i]);
				}
			}
		}

		// Token: 0x06004323 RID: 17187 RVA: 0x0011CA60 File Offset: 0x0011AC60
		public void AddRange(ToolStripItemCollection toolStripItems)
		{
			if (toolStripItems == null)
			{
				throw new ArgumentNullException("toolStripItems");
			}
			if (this.IsReadOnly)
			{
				throw new NotSupportedException(SR.GetString("ToolStripItemCollectionIsReadOnly"));
			}
			using (new LayoutTransaction(this.owner, this.owner, PropertyNames.Items))
			{
				int count = toolStripItems.Count;
				for (int i = 0; i < count; i++)
				{
					this.Add(toolStripItems[i]);
				}
			}
		}

		// Token: 0x06004324 RID: 17188 RVA: 0x0011CAE8 File Offset: 0x0011ACE8
		public bool Contains(ToolStripItem value)
		{
			return base.InnerList.Contains(value);
		}

		// Token: 0x06004325 RID: 17189 RVA: 0x0011CAF8 File Offset: 0x0011ACF8
		public virtual void Clear()
		{
			if (this.IsReadOnly)
			{
				throw new NotSupportedException(SR.GetString("ToolStripItemCollectionIsReadOnly"));
			}
			if (this.Count == 0)
			{
				return;
			}
			ToolStripOverflow toolStripOverflow = null;
			if (this.owner != null && !this.owner.IsDisposingItems)
			{
				this.owner.SuspendLayout();
				toolStripOverflow = this.owner.GetOverflow();
				if (toolStripOverflow != null)
				{
					toolStripOverflow.SuspendLayout();
				}
			}
			try
			{
				while (this.Count != 0)
				{
					this.RemoveAt(this.Count - 1);
				}
			}
			finally
			{
				if (toolStripOverflow != null)
				{
					toolStripOverflow.ResumeLayout(false);
				}
				if (this.owner != null && !this.owner.IsDisposingItems)
				{
					this.owner.ResumeLayout();
				}
			}
		}

		// Token: 0x06004326 RID: 17190 RVA: 0x0011CBB4 File Offset: 0x0011ADB4
		public virtual bool ContainsKey(string key)
		{
			return this.IsValidIndex(this.IndexOfKey(key));
		}

		// Token: 0x06004327 RID: 17191 RVA: 0x0011CBC4 File Offset: 0x0011ADC4
		private void CheckCanAddOrInsertItem(ToolStripItem value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (this.IsReadOnly)
			{
				throw new NotSupportedException(SR.GetString("ToolStripItemCollectionIsReadOnly"));
			}
			ToolStripDropDown toolStripDropDown = this.owner as ToolStripDropDown;
			if (toolStripDropDown != null)
			{
				if (toolStripDropDown.OwnerItem == value)
				{
					throw new NotSupportedException(SR.GetString("ToolStripItemCircularReference"));
				}
				if (value is ToolStripControlHost && !(value is ToolStripScrollButton) && toolStripDropDown.IsRestrictedWindow)
				{
					IntSecurity.AllWindows.Demand();
				}
			}
		}

		// Token: 0x06004328 RID: 17192 RVA: 0x0011CC44 File Offset: 0x0011AE44
		public ToolStripItem[] Find(string key, bool searchAllChildren)
		{
			if (key == null || key.Length == 0)
			{
				throw new ArgumentNullException("key", SR.GetString("FindKeyMayNotBeEmptyOrNull"));
			}
			ArrayList arrayList = this.FindInternal(key, searchAllChildren, this, new ArrayList());
			ToolStripItem[] array = new ToolStripItem[arrayList.Count];
			arrayList.CopyTo(array, 0);
			return array;
		}

		// Token: 0x06004329 RID: 17193 RVA: 0x0011CC98 File Offset: 0x0011AE98
		private ArrayList FindInternal(string key, bool searchAllChildren, ToolStripItemCollection itemsToLookIn, ArrayList foundItems)
		{
			if (itemsToLookIn == null || foundItems == null)
			{
				return null;
			}
			try
			{
				for (int i = 0; i < itemsToLookIn.Count; i++)
				{
					if (itemsToLookIn[i] != null && WindowsFormsUtils.SafeCompareStrings(itemsToLookIn[i].Name, key, true))
					{
						foundItems.Add(itemsToLookIn[i]);
					}
				}
				if (searchAllChildren)
				{
					for (int j = 0; j < itemsToLookIn.Count; j++)
					{
						ToolStripDropDownItem toolStripDropDownItem = itemsToLookIn[j] as ToolStripDropDownItem;
						if (toolStripDropDownItem != null && toolStripDropDownItem.HasDropDownItems)
						{
							foundItems = this.FindInternal(key, searchAllChildren, toolStripDropDownItem.DropDownItems, foundItems);
						}
					}
				}
			}
			catch (Exception ex)
			{
				if (ClientUtils.IsCriticalException(ex))
				{
					throw;
				}
			}
			return foundItems;
		}

		// Token: 0x17001064 RID: 4196
		// (get) Token: 0x0600432A RID: 17194 RVA: 0x0011CD4C File Offset: 0x0011AF4C
		public override bool IsReadOnly
		{
			get
			{
				return this.isReadOnly;
			}
		}

		// Token: 0x0600432B RID: 17195 RVA: 0x0011CD54 File Offset: 0x0011AF54
		void IList.Clear()
		{
			this.Clear();
		}

		// Token: 0x17001065 RID: 4197
		// (get) Token: 0x0600432C RID: 17196 RVA: 0x0011CD5C File Offset: 0x0011AF5C
		bool IList.IsFixedSize
		{
			get
			{
				return base.InnerList.IsFixedSize;
			}
		}

		// Token: 0x0600432D RID: 17197 RVA: 0x0011CAE8 File Offset: 0x0011ACE8
		bool IList.Contains(object value)
		{
			return base.InnerList.Contains(value);
		}

		// Token: 0x0600432E RID: 17198 RVA: 0x0011CD69 File Offset: 0x0011AF69
		void IList.RemoveAt(int index)
		{
			this.RemoveAt(index);
		}

		// Token: 0x0600432F RID: 17199 RVA: 0x0011CD72 File Offset: 0x0011AF72
		void IList.Remove(object value)
		{
			this.Remove(value as ToolStripItem);
		}

		// Token: 0x06004330 RID: 17200 RVA: 0x0011CD80 File Offset: 0x0011AF80
		int IList.Add(object value)
		{
			return this.Add(value as ToolStripItem);
		}

		// Token: 0x06004331 RID: 17201 RVA: 0x0011CD8E File Offset: 0x0011AF8E
		int IList.IndexOf(object value)
		{
			return this.IndexOf(value as ToolStripItem);
		}

		// Token: 0x06004332 RID: 17202 RVA: 0x0011CD9C File Offset: 0x0011AF9C
		void IList.Insert(int index, object value)
		{
			this.Insert(index, value as ToolStripItem);
		}

		// Token: 0x17001066 RID: 4198
		object IList.this[int index]
		{
			get
			{
				return base.InnerList[index];
			}
			set
			{
				throw new NotSupportedException(SR.GetString("ToolStripCollectionMustInsertAndRemove"));
			}
		}

		// Token: 0x06004335 RID: 17205 RVA: 0x0011CDCC File Offset: 0x0011AFCC
		public void Insert(int index, ToolStripItem value)
		{
			this.CheckCanAddOrInsertItem(value);
			this.SetOwner(value);
			base.InnerList.Insert(index, value);
			if (this.itemsCollection && this.owner != null)
			{
				if (this.owner.IsHandleCreated)
				{
					LayoutTransaction.DoLayout(this.owner, value, PropertyNames.Parent);
				}
				else
				{
					CommonProperties.xClearPreferredSizeCache(this.owner);
				}
				this.owner.OnItemAddedInternal(value);
				this.owner.OnItemAdded(new ToolStripItemEventArgs(value));
			}
		}

		// Token: 0x06004336 RID: 17206 RVA: 0x0011CE4C File Offset: 0x0011B04C
		public int IndexOf(ToolStripItem value)
		{
			return base.InnerList.IndexOf(value);
		}

		// Token: 0x06004337 RID: 17207 RVA: 0x0011CE5C File Offset: 0x0011B05C
		public virtual int IndexOfKey(string key)
		{
			if (key == null || key.Length == 0)
			{
				return -1;
			}
			if (this.IsValidIndex(this.lastAccessedIndex) && WindowsFormsUtils.SafeCompareStrings(this[this.lastAccessedIndex].Name, key, true))
			{
				return this.lastAccessedIndex;
			}
			for (int i = 0; i < this.Count; i++)
			{
				if (WindowsFormsUtils.SafeCompareStrings(this[i].Name, key, true))
				{
					this.lastAccessedIndex = i;
					return i;
				}
			}
			this.lastAccessedIndex = -1;
			return -1;
		}

		// Token: 0x06004338 RID: 17208 RVA: 0x0011CEDC File Offset: 0x0011B0DC
		private bool IsValidIndex(int index)
		{
			return index >= 0 && index < this.Count;
		}

		// Token: 0x06004339 RID: 17209 RVA: 0x0011CEF0 File Offset: 0x0011B0F0
		private void OnAfterRemove(ToolStripItem item)
		{
			if (this.itemsCollection)
			{
				ToolStrip toolStrip = null;
				if (item != null)
				{
					toolStrip = item.ParentInternal;
					item.SetOwner(null);
				}
				if (this.owner != null)
				{
					this.owner.OnItemRemovedInternal(item);
					if (!this.owner.IsDisposingItems)
					{
						ToolStripItemEventArgs e = new ToolStripItemEventArgs(item);
						this.owner.OnItemRemoved(e);
						if (toolStrip != null && toolStrip != this.owner)
						{
							toolStrip.OnItemVisibleChanged(e, false);
						}
					}
				}
			}
		}

		// Token: 0x0600433A RID: 17210 RVA: 0x0011CF60 File Offset: 0x0011B160
		public void Remove(ToolStripItem value)
		{
			if (this.IsReadOnly)
			{
				throw new NotSupportedException(SR.GetString("ToolStripItemCollectionIsReadOnly"));
			}
			base.InnerList.Remove(value);
			this.OnAfterRemove(value);
		}

		// Token: 0x0600433B RID: 17211 RVA: 0x0011CF90 File Offset: 0x0011B190
		public void RemoveAt(int index)
		{
			if (this.IsReadOnly)
			{
				throw new NotSupportedException(SR.GetString("ToolStripItemCollectionIsReadOnly"));
			}
			ToolStripItem item = null;
			if (index < this.Count && index >= 0)
			{
				item = (ToolStripItem)base.InnerList[index];
			}
			base.InnerList.RemoveAt(index);
			this.OnAfterRemove(item);
		}

		// Token: 0x0600433C RID: 17212 RVA: 0x0011CFEC File Offset: 0x0011B1EC
		public virtual void RemoveByKey(string key)
		{
			if (this.IsReadOnly)
			{
				throw new NotSupportedException(SR.GetString("ToolStripItemCollectionIsReadOnly"));
			}
			int index = this.IndexOfKey(key);
			if (this.IsValidIndex(index))
			{
				this.RemoveAt(index);
			}
		}

		// Token: 0x0600433D RID: 17213 RVA: 0x0011D029 File Offset: 0x0011B229
		public void CopyTo(ToolStripItem[] array, int index)
		{
			base.InnerList.CopyTo(array, index);
		}

		// Token: 0x0600433E RID: 17214 RVA: 0x0011D038 File Offset: 0x0011B238
		internal void MoveItem(ToolStripItem value)
		{
			if (value.ParentInternal != null)
			{
				int num = value.ParentInternal.Items.IndexOf(value);
				if (num >= 0)
				{
					value.ParentInternal.Items.RemoveAt(num);
				}
			}
			this.Add(value);
		}

		// Token: 0x0600433F RID: 17215 RVA: 0x0011D07C File Offset: 0x0011B27C
		internal void MoveItem(int index, ToolStripItem value)
		{
			if (index == this.Count)
			{
				this.MoveItem(value);
				return;
			}
			if (value.ParentInternal != null)
			{
				int num = value.ParentInternal.Items.IndexOf(value);
				if (num >= 0)
				{
					value.ParentInternal.Items.RemoveAt(num);
					if (value.ParentInternal == this.owner && index > num)
					{
						index--;
					}
				}
			}
			this.Insert(index, value);
		}

		// Token: 0x06004340 RID: 17216 RVA: 0x0011D0E8 File Offset: 0x0011B2E8
		private void SetOwner(ToolStripItem item)
		{
			if (this.itemsCollection && item != null)
			{
				if (item.Owner != null)
				{
					item.Owner.Items.Remove(item);
				}
				item.SetOwner(this.owner);
				if (item.Renderer != null)
				{
					item.Renderer.InitializeItem(item);
				}
			}
		}

		// Token: 0x040025A2 RID: 9634
		private ToolStrip owner;

		// Token: 0x040025A3 RID: 9635
		private bool itemsCollection;

		// Token: 0x040025A4 RID: 9636
		private bool isReadOnly;

		// Token: 0x040025A5 RID: 9637
		private int lastAccessedIndex;
	}
}
