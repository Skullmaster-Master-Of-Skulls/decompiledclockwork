using System;
using System.Collections;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace System.Windows.Forms
{
	// Token: 0x020002F3 RID: 755
	[ToolboxItemFilter("System.Windows.Forms")]
	[ListBindable(false)]
	public abstract class Menu : Component
	{
		// Token: 0x06002FA6 RID: 12198 RVA: 0x000D6F27 File Offset: 0x000D5127
		protected Menu(MenuItem[] items)
		{
			if (items != null)
			{
				this.MenuItems.AddRange(items);
			}
		}

		// Token: 0x17000B36 RID: 2870
		// (get) Token: 0x06002FA7 RID: 12199 RVA: 0x000D6F3E File Offset: 0x000D513E
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[SRDescription("ControlHandleDescr")]
		public IntPtr Handle
		{
			get
			{
				if (this.handle == IntPtr.Zero)
				{
					this.handle = this.CreateMenuHandle();
				}
				this.CreateMenuItems();
				return this.handle;
			}
		}

		// Token: 0x17000B37 RID: 2871
		// (get) Token: 0x06002FA8 RID: 12200 RVA: 0x000D6F6A File Offset: 0x000D516A
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[SRDescription("MenuIsParentDescr")]
		public virtual bool IsParent
		{
			[SecurityPermission(SecurityAction.InheritanceDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
			get
			{
				return this.items != null && this.ItemCount > 0;
			}
		}

		// Token: 0x17000B38 RID: 2872
		// (get) Token: 0x06002FA9 RID: 12201 RVA: 0x000D6F7F File Offset: 0x000D517F
		internal int ItemCount
		{
			get
			{
				return this._itemCount;
			}
		}

		// Token: 0x17000B39 RID: 2873
		// (get) Token: 0x06002FAA RID: 12202 RVA: 0x000D6F88 File Offset: 0x000D5188
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[SRDescription("MenuMDIListItemDescr")]
		public MenuItem MdiListItem
		{
			get
			{
				for (int i = 0; i < this.ItemCount; i++)
				{
					MenuItem menuItem = this.items[i];
					if (menuItem.MdiList)
					{
						return menuItem;
					}
					if (menuItem.IsParent)
					{
						menuItem = menuItem.MdiListItem;
						if (menuItem != null)
						{
							return menuItem;
						}
					}
				}
				return null;
			}
		}

		// Token: 0x17000B3A RID: 2874
		// (get) Token: 0x06002FAB RID: 12203 RVA: 0x000D6FCE File Offset: 0x000D51CE
		// (set) Token: 0x06002FAC RID: 12204 RVA: 0x000D6FDC File Offset: 0x000D51DC
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public string Name
		{
			get
			{
				return WindowsFormsUtils.GetComponentName(this, this.name);
			}
			set
			{
				if (value == null || value.Length == 0)
				{
					this.name = null;
				}
				else
				{
					this.name = value;
				}
				if (this.Site != null)
				{
					this.Site.Name = this.name;
				}
			}
		}

		// Token: 0x17000B3B RID: 2875
		// (get) Token: 0x06002FAD RID: 12205 RVA: 0x000D7012 File Offset: 0x000D5212
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[SRDescription("MenuMenuItemsDescr")]
		[MergableProperty(false)]
		public Menu.MenuItemCollection MenuItems
		{
			get
			{
				if (this.itemsCollection == null)
				{
					this.itemsCollection = new Menu.MenuItemCollection(this);
				}
				return this.itemsCollection;
			}
		}

		// Token: 0x17000B3C RID: 2876
		// (get) Token: 0x06002FAE RID: 12206 RVA: 0x00011A20 File Offset: 0x0000FC20
		internal virtual bool RenderIsRightToLeft
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000B3D RID: 2877
		// (get) Token: 0x06002FAF RID: 12207 RVA: 0x000D702E File Offset: 0x000D522E
		// (set) Token: 0x06002FB0 RID: 12208 RVA: 0x000D7036 File Offset: 0x000D5236
		[SRCategory("CatData")]
		[Localizable(false)]
		[Bindable(true)]
		[SRDescription("ControlTagDescr")]
		[DefaultValue(null)]
		[TypeConverter(typeof(StringConverter))]
		public object Tag
		{
			get
			{
				return this.userData;
			}
			set
			{
				this.userData = value;
			}
		}

		// Token: 0x06002FB1 RID: 12209 RVA: 0x000D7040 File Offset: 0x000D5240
		internal void ClearHandles()
		{
			if (this.handle != IntPtr.Zero)
			{
				UnsafeNativeMethods.DestroyMenu(new HandleRef(this, this.handle));
			}
			this.handle = IntPtr.Zero;
			if (this.created)
			{
				for (int i = 0; i < this.ItemCount; i++)
				{
					this.items[i].ClearHandles();
				}
				this.created = false;
			}
		}

		// Token: 0x06002FB2 RID: 12210 RVA: 0x000D70AC File Offset: 0x000D52AC
		protected internal void CloneMenu(Menu menuSrc)
		{
			MenuItem[] array = null;
			if (menuSrc.items != null)
			{
				int count = menuSrc.MenuItems.Count;
				array = new MenuItem[count];
				for (int i = 0; i < count; i++)
				{
					array[i] = menuSrc.MenuItems[i].CloneMenu();
				}
			}
			this.MenuItems.Clear();
			if (array != null)
			{
				this.MenuItems.AddRange(array);
			}
		}

		// Token: 0x06002FB3 RID: 12211 RVA: 0x000D7110 File Offset: 0x000D5310
		[SecurityPermission(SecurityAction.InheritanceDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		protected virtual IntPtr CreateMenuHandle()
		{
			return UnsafeNativeMethods.CreatePopupMenu();
		}

		// Token: 0x06002FB4 RID: 12212 RVA: 0x000D7118 File Offset: 0x000D5318
		internal void CreateMenuItems()
		{
			if (!this.created)
			{
				for (int i = 0; i < this.ItemCount; i++)
				{
					this.items[i].CreateMenuItem();
				}
				this.created = true;
			}
		}

		// Token: 0x06002FB5 RID: 12213 RVA: 0x000D7154 File Offset: 0x000D5354
		internal void DestroyMenuItems()
		{
			if (this.created)
			{
				for (int i = 0; i < this.ItemCount; i++)
				{
					this.items[i].ClearHandles();
				}
				while (UnsafeNativeMethods.GetMenuItemCount(new HandleRef(this, this.handle)) > 0)
				{
					UnsafeNativeMethods.RemoveMenu(new HandleRef(this, this.handle), 0, 1024);
				}
				this.created = false;
			}
		}

		// Token: 0x06002FB6 RID: 12214 RVA: 0x000D71BC File Offset: 0x000D53BC
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				while (this.ItemCount > 0)
				{
					MenuItem[] array = this.items;
					int num = this._itemCount - 1;
					this._itemCount = num;
					MenuItem menuItem = array[num];
					if (menuItem.Site != null && menuItem.Site.Container != null)
					{
						menuItem.Site.Container.Remove(menuItem);
					}
					menuItem.Menu = null;
					menuItem.Dispose();
				}
				this.items = null;
			}
			if (this.handle != IntPtr.Zero)
			{
				UnsafeNativeMethods.DestroyMenu(new HandleRef(this, this.handle));
				this.handle = IntPtr.Zero;
				if (disposing)
				{
					this.ClearHandles();
				}
			}
			base.Dispose(disposing);
		}

		// Token: 0x06002FB7 RID: 12215 RVA: 0x000D7269 File Offset: 0x000D5469
		public MenuItem FindMenuItem(int type, IntPtr value)
		{
			IntSecurity.ControlFromHandleOrLocation.Demand();
			return this.FindMenuItemInternal(type, value);
		}

		// Token: 0x06002FB8 RID: 12216 RVA: 0x000D7280 File Offset: 0x000D5480
		private MenuItem FindMenuItemInternal(int type, IntPtr value)
		{
			for (int i = 0; i < this.ItemCount; i++)
			{
				MenuItem menuItem = this.items[i];
				if (type != 0)
				{
					if (type == 1)
					{
						if (menuItem.Shortcut == (Shortcut)((int)value))
						{
							return menuItem;
						}
					}
				}
				else if (menuItem.handle == value)
				{
					return menuItem;
				}
				menuItem = menuItem.FindMenuItemInternal(type, value);
				if (menuItem != null)
				{
					return menuItem;
				}
			}
			return null;
		}

		// Token: 0x06002FB9 RID: 12217 RVA: 0x000D72E0 File Offset: 0x000D54E0
		protected int FindMergePosition(int mergeOrder)
		{
			int i = 0;
			int num = this.ItemCount;
			while (i < num)
			{
				int num2 = (i + num) / 2;
				if (this.items[num2].MergeOrder <= mergeOrder)
				{
					i = num2 + 1;
				}
				else
				{
					num = num2;
				}
			}
			return i;
		}

		// Token: 0x06002FBA RID: 12218 RVA: 0x000D731C File Offset: 0x000D551C
		internal int xFindMergePosition(int mergeOrder)
		{
			int result = 0;
			int num = 0;
			while (num < this.ItemCount && this.items[num].MergeOrder <= mergeOrder)
			{
				if (this.items[num].MergeOrder < mergeOrder)
				{
					result = num + 1;
				}
				else if (mergeOrder == this.items[num].MergeOrder)
				{
					result = num;
					break;
				}
				num++;
			}
			return result;
		}

		// Token: 0x06002FBB RID: 12219 RVA: 0x000D7378 File Offset: 0x000D5578
		internal void UpdateRtl(bool setRightToLeftBit)
		{
			foreach (object obj in this.MenuItems)
			{
				MenuItem menuItem = (MenuItem)obj;
				menuItem.UpdateItemRtl(setRightToLeftBit);
				menuItem.UpdateRtl(setRightToLeftBit);
			}
		}

		// Token: 0x06002FBC RID: 12220 RVA: 0x000D73D8 File Offset: 0x000D55D8
		public ContextMenu GetContextMenu()
		{
			Menu menu = this;
			while (!(menu is ContextMenu))
			{
				if (!(menu is MenuItem))
				{
					return null;
				}
				menu = ((MenuItem)menu).Menu;
			}
			return (ContextMenu)menu;
		}

		// Token: 0x06002FBD RID: 12221 RVA: 0x000D7410 File Offset: 0x000D5610
		public MainMenu GetMainMenu()
		{
			Menu menu = this;
			while (!(menu is MainMenu))
			{
				if (!(menu is MenuItem))
				{
					return null;
				}
				menu = ((MenuItem)menu).Menu;
			}
			return (MainMenu)menu;
		}

		// Token: 0x06002FBE RID: 12222 RVA: 0x000D7445 File Offset: 0x000D5645
		internal virtual void ItemsChanged(int change)
		{
			if (change <= 1)
			{
				this.DestroyMenuItems();
			}
		}

		// Token: 0x06002FBF RID: 12223 RVA: 0x000D7454 File Offset: 0x000D5654
		private IntPtr MatchKeyToMenuItem(int startItem, char key, Menu.MenuItemKeyComparer comparer)
		{
			int num = -1;
			bool flag = false;
			int num2 = 0;
			while (num2 < this.items.Length && !flag)
			{
				int num3 = (startItem + num2) % this.items.Length;
				MenuItem menuItem = this.items[num3];
				if (menuItem != null && comparer(menuItem, key))
				{
					if (num < 0)
					{
						num = menuItem.MenuIndex;
					}
					else
					{
						flag = true;
					}
				}
				num2++;
			}
			if (num < 0)
			{
				return IntPtr.Zero;
			}
			int high = flag ? 3 : 2;
			return (IntPtr)NativeMethods.Util.MAKELONG(num, high);
		}

		// Token: 0x06002FC0 RID: 12224 RVA: 0x000D74D4 File Offset: 0x000D56D4
		public virtual void MergeMenu(Menu menuSrc)
		{
			if (menuSrc == this)
			{
				throw new ArgumentException(SR.GetString("MenuMergeWithSelf"), "menuSrc");
			}
			if (menuSrc.items != null && this.items == null)
			{
				this.MenuItems.Clear();
			}
			for (int i = 0; i < menuSrc.ItemCount; i++)
			{
				MenuItem menuItem = menuSrc.items[i];
				MenuMerge mergeType = menuItem.MergeType;
				if (mergeType != MenuMerge.Add)
				{
					if (mergeType - MenuMerge.Replace <= 1)
					{
						int mergeOrder = menuItem.MergeOrder;
						int j = this.xFindMergePosition(mergeOrder);
						while (j < this.ItemCount)
						{
							MenuItem menuItem2 = this.items[j];
							if (menuItem2.MergeOrder != mergeOrder)
							{
								this.MenuItems.Add(j, menuItem.MergeMenu());
								goto IL_11D;
							}
							if (menuItem2.MergeType != MenuMerge.Add)
							{
								if (menuItem.MergeType != MenuMerge.MergeItems || menuItem2.MergeType != MenuMerge.MergeItems)
								{
									menuItem2.Dispose();
									this.MenuItems.Add(j, menuItem.MergeMenu());
									goto IL_11D;
								}
								menuItem2.MergeMenu(menuItem);
								goto IL_11D;
							}
							else
							{
								j++;
							}
						}
						this.MenuItems.Add(j, menuItem.MergeMenu());
					}
				}
				else
				{
					this.MenuItems.Add(this.FindMergePosition(menuItem.MergeOrder), menuItem.MergeMenu());
				}
				IL_11D:;
			}
		}

		// Token: 0x06002FC1 RID: 12225 RVA: 0x000D7610 File Offset: 0x000D5810
		internal virtual bool ProcessInitMenuPopup(IntPtr handle)
		{
			MenuItem menuItem = this.FindMenuItemInternal(0, handle);
			if (menuItem != null)
			{
				menuItem._OnInitMenuPopup(EventArgs.Empty);
				menuItem.CreateMenuItems();
				return true;
			}
			return false;
		}

		// Token: 0x06002FC2 RID: 12226 RVA: 0x000D7640 File Offset: 0x000D5840
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		[SecurityPermission(SecurityAction.InheritanceDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		protected internal virtual bool ProcessCmdKey(ref Message msg, Keys keyData)
		{
			MenuItem menuItem = this.FindMenuItemInternal(1, (IntPtr)((int)keyData));
			return menuItem != null && menuItem.ShortcutClick();
		}

		// Token: 0x17000B3E RID: 2878
		// (get) Token: 0x06002FC3 RID: 12227 RVA: 0x000D7668 File Offset: 0x000D5868
		internal int SelectedMenuItemIndex
		{
			get
			{
				for (int i = 0; i < this.items.Length; i++)
				{
					MenuItem menuItem = this.items[i];
					if (menuItem != null && menuItem.Selected)
					{
						return i;
					}
				}
				return -1;
			}
		}

		// Token: 0x06002FC4 RID: 12228 RVA: 0x000D76A0 File Offset: 0x000D58A0
		public override string ToString()
		{
			string str = base.ToString();
			return str + ", Items.Count: " + this.ItemCount.ToString(CultureInfo.CurrentCulture);
		}

		// Token: 0x06002FC5 RID: 12229 RVA: 0x000D76D4 File Offset: 0x000D58D4
		internal void WmMenuChar(ref Message m)
		{
			Menu menu = (m.LParam == this.handle) ? this : this.FindMenuItemInternal(0, m.LParam);
			if (menu == null)
			{
				return;
			}
			char key = char.ToUpper((char)NativeMethods.Util.LOWORD(m.WParam), CultureInfo.CurrentCulture);
			m.Result = menu.WmMenuCharInternal(key);
		}

		// Token: 0x06002FC6 RID: 12230 RVA: 0x000D7730 File Offset: 0x000D5930
		internal IntPtr WmMenuCharInternal(char key)
		{
			int startItem = (this.SelectedMenuItemIndex + 1) % this.items.Length;
			IntPtr intPtr = this.MatchKeyToMenuItem(startItem, key, new Menu.MenuItemKeyComparer(this.CheckOwnerDrawItemWithMnemonic));
			if (intPtr == IntPtr.Zero)
			{
				intPtr = this.MatchKeyToMenuItem(startItem, key, new Menu.MenuItemKeyComparer(this.CheckOwnerDrawItemNoMnemonic));
			}
			return intPtr;
		}

		// Token: 0x06002FC7 RID: 12231 RVA: 0x000D7787 File Offset: 0x000D5987
		private bool CheckOwnerDrawItemWithMnemonic(MenuItem mi, char key)
		{
			return mi.OwnerDraw && mi.Mnemonic == key;
		}

		// Token: 0x06002FC8 RID: 12232 RVA: 0x000D779C File Offset: 0x000D599C
		private bool CheckOwnerDrawItemNoMnemonic(MenuItem mi, char key)
		{
			return mi.OwnerDraw && mi.Mnemonic == '\0' && mi.Text.Length > 0 && char.ToUpper(mi.Text[0], CultureInfo.CurrentCulture) == key;
		}

		// Token: 0x040013B1 RID: 5041
		internal const int CHANGE_ITEMS = 0;

		// Token: 0x040013B2 RID: 5042
		internal const int CHANGE_VISIBLE = 1;

		// Token: 0x040013B3 RID: 5043
		internal const int CHANGE_MDI = 2;

		// Token: 0x040013B4 RID: 5044
		internal const int CHANGE_MERGE = 3;

		// Token: 0x040013B5 RID: 5045
		internal const int CHANGE_ITEMADDED = 4;

		// Token: 0x040013B6 RID: 5046
		public const int FindHandle = 0;

		// Token: 0x040013B7 RID: 5047
		public const int FindShortcut = 1;

		// Token: 0x040013B8 RID: 5048
		private Menu.MenuItemCollection itemsCollection;

		// Token: 0x040013B9 RID: 5049
		internal MenuItem[] items;

		// Token: 0x040013BA RID: 5050
		private int _itemCount;

		// Token: 0x040013BB RID: 5051
		internal IntPtr handle;

		// Token: 0x040013BC RID: 5052
		internal bool created;

		// Token: 0x040013BD RID: 5053
		private object userData;

		// Token: 0x040013BE RID: 5054
		private string name;

		// Token: 0x020006D6 RID: 1750
		// (Invoke) Token: 0x06006AE3 RID: 27363
		private delegate bool MenuItemKeyComparer(MenuItem mi, char key);

		// Token: 0x020006D7 RID: 1751
		[ListBindable(false)]
		public class MenuItemCollection : IList, ICollection, IEnumerable
		{
			// Token: 0x06006AE6 RID: 27366 RVA: 0x0018C1DA File Offset: 0x0018A3DA
			public MenuItemCollection(Menu owner)
			{
				this.owner = owner;
			}

			// Token: 0x1700172D RID: 5933
			public virtual MenuItem this[int index]
			{
				get
				{
					if (index < 0 || index >= this.owner.ItemCount)
					{
						throw new ArgumentOutOfRangeException("index", SR.GetString("InvalidArgument", new object[]
						{
							"index",
							index.ToString(CultureInfo.CurrentCulture)
						}));
					}
					return this.owner.items[index];
				}
			}

			// Token: 0x1700172E RID: 5934
			object IList.this[int index]
			{
				get
				{
					return this[index];
				}
				set
				{
					throw new NotSupportedException();
				}
			}

			// Token: 0x1700172F RID: 5935
			public virtual MenuItem this[string key]
			{
				get
				{
					if (string.IsNullOrEmpty(key))
					{
						return null;
					}
					int index = this.IndexOfKey(key);
					if (this.IsValidIndex(index))
					{
						return this[index];
					}
					return null;
				}
			}

			// Token: 0x17001730 RID: 5936
			// (get) Token: 0x06006AEB RID: 27371 RVA: 0x0018C289 File Offset: 0x0018A489
			public int Count
			{
				get
				{
					return this.owner.ItemCount;
				}
			}

			// Token: 0x17001731 RID: 5937
			// (get) Token: 0x06006AEC RID: 27372 RVA: 0x00006C59 File Offset: 0x00004E59
			object ICollection.SyncRoot
			{
				get
				{
					return this;
				}
			}

			// Token: 0x17001732 RID: 5938
			// (get) Token: 0x06006AED RID: 27373 RVA: 0x00011A20 File Offset: 0x0000FC20
			bool ICollection.IsSynchronized
			{
				get
				{
					return false;
				}
			}

			// Token: 0x17001733 RID: 5939
			// (get) Token: 0x06006AEE RID: 27374 RVA: 0x00011A20 File Offset: 0x0000FC20
			bool IList.IsFixedSize
			{
				get
				{
					return false;
				}
			}

			// Token: 0x17001734 RID: 5940
			// (get) Token: 0x06006AEF RID: 27375 RVA: 0x00011A20 File Offset: 0x0000FC20
			public bool IsReadOnly
			{
				get
				{
					return false;
				}
			}

			// Token: 0x06006AF0 RID: 27376 RVA: 0x0018C298 File Offset: 0x0018A498
			public virtual MenuItem Add(string caption)
			{
				MenuItem menuItem = new MenuItem(caption);
				this.Add(menuItem);
				return menuItem;
			}

			// Token: 0x06006AF1 RID: 27377 RVA: 0x0018C2B8 File Offset: 0x0018A4B8
			public virtual MenuItem Add(string caption, EventHandler onClick)
			{
				MenuItem menuItem = new MenuItem(caption, onClick);
				this.Add(menuItem);
				return menuItem;
			}

			// Token: 0x06006AF2 RID: 27378 RVA: 0x0018C2D8 File Offset: 0x0018A4D8
			public virtual MenuItem Add(string caption, MenuItem[] items)
			{
				MenuItem menuItem = new MenuItem(caption, items);
				this.Add(menuItem);
				return menuItem;
			}

			// Token: 0x06006AF3 RID: 27379 RVA: 0x0018C2F6 File Offset: 0x0018A4F6
			public virtual int Add(MenuItem item)
			{
				return this.Add(this.owner.ItemCount, item);
			}

			// Token: 0x06006AF4 RID: 27380 RVA: 0x0018C30C File Offset: 0x0018A50C
			public virtual int Add(int index, MenuItem item)
			{
				if (item.Menu != null)
				{
					if (this.owner is MenuItem)
					{
						for (MenuItem menuItem = (MenuItem)this.owner; menuItem != null; menuItem = (MenuItem)menuItem.Parent)
						{
							if (menuItem.Equals(item))
							{
								throw new ArgumentException(SR.GetString("MenuItemAlreadyExists", new object[]
								{
									item.Text
								}), "item");
							}
							if (!(menuItem.Parent is MenuItem))
							{
								break;
							}
						}
					}
					if (item.Menu.Equals(this.owner) && index > 0)
					{
						index--;
					}
					item.Menu.MenuItems.Remove(item);
				}
				if (index < 0 || index > this.owner.ItemCount)
				{
					throw new ArgumentOutOfRangeException("index", SR.GetString("InvalidArgument", new object[]
					{
						"index",
						index.ToString(CultureInfo.CurrentCulture)
					}));
				}
				if (this.owner.items == null || this.owner.items.Length == this.owner.ItemCount)
				{
					MenuItem[] array = new MenuItem[(this.owner.ItemCount < 2) ? 4 : (this.owner.ItemCount * 2)];
					if (this.owner.ItemCount > 0)
					{
						Array.Copy(this.owner.items, 0, array, 0, this.owner.ItemCount);
					}
					this.owner.items = array;
				}
				Array.Copy(this.owner.items, index, this.owner.items, index + 1, this.owner.ItemCount - index);
				this.owner.items[index] = item;
				this.owner._itemCount++;
				item.Menu = this.owner;
				this.owner.ItemsChanged(0);
				if (this.owner is MenuItem)
				{
					((MenuItem)this.owner).ItemsChanged(4, item);
				}
				return index;
			}

			// Token: 0x06006AF5 RID: 27381 RVA: 0x0018C504 File Offset: 0x0018A704
			public virtual void AddRange(MenuItem[] items)
			{
				if (items == null)
				{
					throw new ArgumentNullException("items");
				}
				foreach (MenuItem item in items)
				{
					this.Add(item);
				}
			}

			// Token: 0x06006AF6 RID: 27382 RVA: 0x0018C53B File Offset: 0x0018A73B
			int IList.Add(object value)
			{
				if (value is MenuItem)
				{
					return this.Add((MenuItem)value);
				}
				throw new ArgumentException(SR.GetString("MenuBadMenuItem"), "value");
			}

			// Token: 0x06006AF7 RID: 27383 RVA: 0x0018C566 File Offset: 0x0018A766
			public bool Contains(MenuItem value)
			{
				return this.IndexOf(value) != -1;
			}

			// Token: 0x06006AF8 RID: 27384 RVA: 0x0018C575 File Offset: 0x0018A775
			bool IList.Contains(object value)
			{
				return value is MenuItem && this.Contains((MenuItem)value);
			}

			// Token: 0x06006AF9 RID: 27385 RVA: 0x0018C58D File Offset: 0x0018A78D
			public virtual bool ContainsKey(string key)
			{
				return this.IsValidIndex(this.IndexOfKey(key));
			}

			// Token: 0x06006AFA RID: 27386 RVA: 0x0018C59C File Offset: 0x0018A79C
			public MenuItem[] Find(string key, bool searchAllChildren)
			{
				if (key == null || key.Length == 0)
				{
					throw new ArgumentNullException("key", SR.GetString("FindKeyMayNotBeEmptyOrNull"));
				}
				ArrayList arrayList = this.FindInternal(key, searchAllChildren, this, new ArrayList());
				MenuItem[] array = new MenuItem[arrayList.Count];
				arrayList.CopyTo(array, 0);
				return array;
			}

			// Token: 0x06006AFB RID: 27387 RVA: 0x0018C5F0 File Offset: 0x0018A7F0
			private ArrayList FindInternal(string key, bool searchAllChildren, Menu.MenuItemCollection menuItemsToLookIn, ArrayList foundMenuItems)
			{
				if (menuItemsToLookIn == null || foundMenuItems == null)
				{
					return null;
				}
				for (int i = 0; i < menuItemsToLookIn.Count; i++)
				{
					if (menuItemsToLookIn[i] != null && WindowsFormsUtils.SafeCompareStrings(menuItemsToLookIn[i].Name, key, true))
					{
						foundMenuItems.Add(menuItemsToLookIn[i]);
					}
				}
				if (searchAllChildren)
				{
					for (int j = 0; j < menuItemsToLookIn.Count; j++)
					{
						if (menuItemsToLookIn[j] != null && menuItemsToLookIn[j].MenuItems != null && menuItemsToLookIn[j].MenuItems.Count > 0)
						{
							foundMenuItems = this.FindInternal(key, searchAllChildren, menuItemsToLookIn[j].MenuItems, foundMenuItems);
						}
					}
				}
				return foundMenuItems;
			}

			// Token: 0x06006AFC RID: 27388 RVA: 0x0018C6A0 File Offset: 0x0018A8A0
			public int IndexOf(MenuItem value)
			{
				for (int i = 0; i < this.Count; i++)
				{
					if (this[i] == value)
					{
						return i;
					}
				}
				return -1;
			}

			// Token: 0x06006AFD RID: 27389 RVA: 0x0018C6CB File Offset: 0x0018A8CB
			int IList.IndexOf(object value)
			{
				if (value is MenuItem)
				{
					return this.IndexOf((MenuItem)value);
				}
				return -1;
			}

			// Token: 0x06006AFE RID: 27390 RVA: 0x0018C6E4 File Offset: 0x0018A8E4
			public virtual int IndexOfKey(string key)
			{
				if (string.IsNullOrEmpty(key))
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

			// Token: 0x06006AFF RID: 27391 RVA: 0x0018C761 File Offset: 0x0018A961
			void IList.Insert(int index, object value)
			{
				if (value is MenuItem)
				{
					this.Add(index, (MenuItem)value);
					return;
				}
				throw new ArgumentException(SR.GetString("MenuBadMenuItem"), "value");
			}

			// Token: 0x06006B00 RID: 27392 RVA: 0x0018C78E File Offset: 0x0018A98E
			private bool IsValidIndex(int index)
			{
				return index >= 0 && index < this.Count;
			}

			// Token: 0x06006B01 RID: 27393 RVA: 0x0018C7A0 File Offset: 0x0018A9A0
			public virtual void Clear()
			{
				if (this.owner.ItemCount > 0)
				{
					for (int i = 0; i < this.owner.ItemCount; i++)
					{
						this.owner.items[i].Menu = null;
					}
					this.owner._itemCount = 0;
					this.owner.items = null;
					this.owner.ItemsChanged(0);
					if (this.owner is MenuItem)
					{
						((MenuItem)this.owner).UpdateMenuItem(true);
					}
				}
			}

			// Token: 0x06006B02 RID: 27394 RVA: 0x0018C826 File Offset: 0x0018AA26
			public void CopyTo(Array dest, int index)
			{
				if (this.owner.ItemCount > 0)
				{
					Array.Copy(this.owner.items, 0, dest, index, this.owner.ItemCount);
				}
			}

			// Token: 0x06006B03 RID: 27395 RVA: 0x0018C854 File Offset: 0x0018AA54
			public IEnumerator GetEnumerator()
			{
				object[] items = this.owner.items;
				return new WindowsFormsUtils.ArraySubsetEnumerator(items, this.owner.ItemCount);
			}

			// Token: 0x06006B04 RID: 27396 RVA: 0x0018C880 File Offset: 0x0018AA80
			public virtual void RemoveAt(int index)
			{
				if (index < 0 || index >= this.owner.ItemCount)
				{
					throw new ArgumentOutOfRangeException("index", SR.GetString("InvalidArgument", new object[]
					{
						"index",
						index.ToString(CultureInfo.CurrentCulture)
					}));
				}
				MenuItem menuItem = this.owner.items[index];
				menuItem.Menu = null;
				this.owner._itemCount--;
				Array.Copy(this.owner.items, index + 1, this.owner.items, index, this.owner.ItemCount - index);
				this.owner.items[this.owner.ItemCount] = null;
				this.owner.ItemsChanged(0);
				if (this.owner.ItemCount == 0)
				{
					this.Clear();
				}
			}

			// Token: 0x06006B05 RID: 27397 RVA: 0x0018C95C File Offset: 0x0018AB5C
			public virtual void RemoveByKey(string key)
			{
				int index = this.IndexOfKey(key);
				if (this.IsValidIndex(index))
				{
					this.RemoveAt(index);
				}
			}

			// Token: 0x06006B06 RID: 27398 RVA: 0x0018C981 File Offset: 0x0018AB81
			public virtual void Remove(MenuItem item)
			{
				if (item.Menu == this.owner)
				{
					this.RemoveAt(item.Index);
				}
			}

			// Token: 0x06006B07 RID: 27399 RVA: 0x0018C99D File Offset: 0x0018AB9D
			void IList.Remove(object value)
			{
				if (value is MenuItem)
				{
					this.Remove((MenuItem)value);
				}
			}

			// Token: 0x04003B51 RID: 15185
			private Menu owner;

			// Token: 0x04003B52 RID: 15186
			private int lastAccessedIndex = -1;
		}
	}
}
