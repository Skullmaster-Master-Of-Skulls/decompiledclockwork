using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Security;
using System.Threading;

namespace System.Windows.Forms
{
	// Token: 0x020002F5 RID: 757
	[ToolboxItem(false)]
	[DesignTimeVisible(false)]
	[DefaultEvent("Click")]
	[DefaultProperty("Text")]
	public class MenuItem : Menu
	{
		// Token: 0x06002FC9 RID: 12233 RVA: 0x000D77D8 File Offset: 0x000D59D8
		public MenuItem() : this(MenuMerge.Add, 0, Shortcut.None, null, null, null, null, null)
		{
		}

		// Token: 0x06002FCA RID: 12234 RVA: 0x000D77F4 File Offset: 0x000D59F4
		public MenuItem(string text) : this(MenuMerge.Add, 0, Shortcut.None, text, null, null, null, null)
		{
		}

		// Token: 0x06002FCB RID: 12235 RVA: 0x000D7810 File Offset: 0x000D5A10
		public MenuItem(string text, EventHandler onClick) : this(MenuMerge.Add, 0, Shortcut.None, text, onClick, null, null, null)
		{
		}

		// Token: 0x06002FCC RID: 12236 RVA: 0x000D782C File Offset: 0x000D5A2C
		public MenuItem(string text, EventHandler onClick, Shortcut shortcut) : this(MenuMerge.Add, 0, shortcut, text, onClick, null, null, null)
		{
		}

		// Token: 0x06002FCD RID: 12237 RVA: 0x000D7848 File Offset: 0x000D5A48
		public MenuItem(string text, MenuItem[] items) : this(MenuMerge.Add, 0, Shortcut.None, text, null, null, null, items)
		{
		}

		// Token: 0x06002FCE RID: 12238 RVA: 0x000D7863 File Offset: 0x000D5A63
		internal MenuItem(MenuItem.MenuItemData data)
		{
			this.msaaMenuInfoPtr = IntPtr.Zero;
			base..ctor(null);
			data.AddItem(this);
		}

		// Token: 0x06002FCF RID: 12239 RVA: 0x000D7880 File Offset: 0x000D5A80
		public MenuItem(MenuMerge mergeType, int mergeOrder, Shortcut shortcut, string text, EventHandler onClick, EventHandler onPopup, EventHandler onSelect, MenuItem[] items)
		{
			this.msaaMenuInfoPtr = IntPtr.Zero;
			base..ctor(items);
			new MenuItem.MenuItemData(this, mergeType, mergeOrder, shortcut, true, text, onClick, onPopup, onSelect, null, null);
		}

		// Token: 0x17000B3F RID: 2879
		// (get) Token: 0x06002FD0 RID: 12240 RVA: 0x000D78B5 File Offset: 0x000D5AB5
		// (set) Token: 0x06002FD1 RID: 12241 RVA: 0x000D78C8 File Offset: 0x000D5AC8
		[Browsable(false)]
		[DefaultValue(false)]
		public bool BarBreak
		{
			get
			{
				return (this.data.State & 32) != 0;
			}
			set
			{
				this.data.SetState(32, value);
			}
		}

		// Token: 0x17000B40 RID: 2880
		// (get) Token: 0x06002FD2 RID: 12242 RVA: 0x000D78D8 File Offset: 0x000D5AD8
		// (set) Token: 0x06002FD3 RID: 12243 RVA: 0x000D78EB File Offset: 0x000D5AEB
		[Browsable(false)]
		[DefaultValue(false)]
		public bool Break
		{
			get
			{
				return (this.data.State & 64) != 0;
			}
			set
			{
				this.data.SetState(64, value);
			}
		}

		// Token: 0x17000B41 RID: 2881
		// (get) Token: 0x06002FD4 RID: 12244 RVA: 0x000D78FB File Offset: 0x000D5AFB
		// (set) Token: 0x06002FD5 RID: 12245 RVA: 0x000D790D File Offset: 0x000D5B0D
		[DefaultValue(false)]
		[SRDescription("MenuItemCheckedDescr")]
		public bool Checked
		{
			get
			{
				return (this.data.State & 8) != 0;
			}
			set
			{
				if (value && (base.ItemCount != 0 || (this.Parent != null && this.Parent is MainMenu)))
				{
					throw new ArgumentException(SR.GetString("MenuItemInvalidCheckProperty"));
				}
				this.data.SetState(8, value);
			}
		}

		// Token: 0x17000B42 RID: 2882
		// (get) Token: 0x06002FD6 RID: 12246 RVA: 0x000D794C File Offset: 0x000D5B4C
		// (set) Token: 0x06002FD7 RID: 12247 RVA: 0x000D7964 File Offset: 0x000D5B64
		[DefaultValue(false)]
		[SRDescription("MenuItemDefaultDescr")]
		public bool DefaultItem
		{
			get
			{
				return (this.data.State & 4096) != 0;
			}
			set
			{
				if (this.menu != null)
				{
					if (value)
					{
						UnsafeNativeMethods.SetMenuDefaultItem(new HandleRef(this.menu, this.menu.handle), this.MenuID, false);
					}
					else if (this.DefaultItem)
					{
						UnsafeNativeMethods.SetMenuDefaultItem(new HandleRef(this.menu, this.menu.handle), -1, false);
					}
				}
				this.data.SetState(4096, value);
			}
		}

		// Token: 0x17000B43 RID: 2883
		// (get) Token: 0x06002FD8 RID: 12248 RVA: 0x000D79D8 File Offset: 0x000D5BD8
		// (set) Token: 0x06002FD9 RID: 12249 RVA: 0x000D79EE File Offset: 0x000D5BEE
		[SRCategory("CatBehavior")]
		[DefaultValue(false)]
		[SRDescription("MenuItemOwnerDrawDescr")]
		public bool OwnerDraw
		{
			get
			{
				return (this.data.State & 256) != 0;
			}
			set
			{
				this.data.SetState(256, value);
			}
		}

		// Token: 0x17000B44 RID: 2884
		// (get) Token: 0x06002FDA RID: 12250 RVA: 0x000D7A01 File Offset: 0x000D5C01
		// (set) Token: 0x06002FDB RID: 12251 RVA: 0x000D7A13 File Offset: 0x000D5C13
		[Localizable(true)]
		[DefaultValue(true)]
		[SRDescription("MenuItemEnabledDescr")]
		public bool Enabled
		{
			get
			{
				return (this.data.State & 3) == 0;
			}
			set
			{
				this.data.SetState(3, !value);
			}
		}

		// Token: 0x17000B45 RID: 2885
		// (get) Token: 0x06002FDC RID: 12252 RVA: 0x000D7A28 File Offset: 0x000D5C28
		// (set) Token: 0x06002FDD RID: 12253 RVA: 0x000D7A68 File Offset: 0x000D5C68
		[Browsable(false)]
		public int Index
		{
			get
			{
				if (this.menu != null)
				{
					for (int i = 0; i < this.menu.ItemCount; i++)
					{
						if (this.menu.items[i] == this)
						{
							return i;
						}
					}
				}
				return -1;
			}
			set
			{
				int index = this.Index;
				if (index >= 0)
				{
					if (value < 0 || value >= this.menu.ItemCount)
					{
						throw new ArgumentOutOfRangeException("Index", SR.GetString("InvalidArgument", new object[]
						{
							"Index",
							value.ToString(CultureInfo.CurrentCulture)
						}));
					}
					if (value != index)
					{
						Menu menu = this.menu;
						menu.MenuItems.RemoveAt(index);
						menu.MenuItems.Add(value, this);
					}
				}
			}
		}

		// Token: 0x17000B46 RID: 2886
		// (get) Token: 0x06002FDE RID: 12254 RVA: 0x000D7AEC File Offset: 0x000D5CEC
		[Browsable(false)]
		public override bool IsParent
		{
			get
			{
				bool flag = false;
				if (this.data != null && this.MdiList)
				{
					for (int i = 0; i < base.ItemCount; i++)
					{
						if (!(this.items[i].data.UserData is MenuItem.MdiListUserData))
						{
							flag = true;
							break;
						}
					}
					if (!flag && this.FindMdiForms().Length != 0)
					{
						flag = true;
					}
					if (!flag && this.menu != null && !(this.menu is MenuItem))
					{
						flag = true;
					}
				}
				else
				{
					flag = base.IsParent;
				}
				return flag;
			}
		}

		// Token: 0x17000B47 RID: 2887
		// (get) Token: 0x06002FDF RID: 12255 RVA: 0x000D7B6B File Offset: 0x000D5D6B
		// (set) Token: 0x06002FE0 RID: 12256 RVA: 0x000D7B81 File Offset: 0x000D5D81
		[DefaultValue(false)]
		[SRDescription("MenuItemMDIListDescr")]
		public bool MdiList
		{
			get
			{
				return (this.data.State & 131072) != 0;
			}
			set
			{
				this.data.MdiList = value;
				MenuItem.CleanListItems(this);
			}
		}

		// Token: 0x17000B48 RID: 2888
		// (get) Token: 0x06002FE1 RID: 12257 RVA: 0x000D7B95 File Offset: 0x000D5D95
		// (set) Token: 0x06002FE2 RID: 12258 RVA: 0x000D7B9D File Offset: 0x000D5D9D
		internal Menu Menu
		{
			get
			{
				return this.menu;
			}
			set
			{
				this.menu = value;
			}
		}

		// Token: 0x17000B49 RID: 2889
		// (get) Token: 0x06002FE3 RID: 12259 RVA: 0x000D7BA6 File Offset: 0x000D5DA6
		protected int MenuID
		{
			get
			{
				return this.data.GetMenuID();
			}
		}

		// Token: 0x17000B4A RID: 2890
		// (get) Token: 0x06002FE4 RID: 12260 RVA: 0x000D7BB4 File Offset: 0x000D5DB4
		internal bool Selected
		{
			get
			{
				if (this.menu == null)
				{
					return false;
				}
				NativeMethods.MENUITEMINFO_T menuiteminfo_T = new NativeMethods.MENUITEMINFO_T();
				menuiteminfo_T.cbSize = Marshal.SizeOf(typeof(NativeMethods.MENUITEMINFO_T));
				menuiteminfo_T.fMask = 1;
				UnsafeNativeMethods.GetMenuItemInfo(new HandleRef(this.menu, this.menu.handle), this.MenuID, false, menuiteminfo_T);
				return (menuiteminfo_T.fState & 128) != 0;
			}
		}

		// Token: 0x17000B4B RID: 2891
		// (get) Token: 0x06002FE5 RID: 12261 RVA: 0x000D7C20 File Offset: 0x000D5E20
		internal int MenuIndex
		{
			get
			{
				if (this.menu == null)
				{
					return -1;
				}
				int menuItemCount = UnsafeNativeMethods.GetMenuItemCount(new HandleRef(this.menu, this.menu.Handle));
				int menuID = this.MenuID;
				NativeMethods.MENUITEMINFO_T menuiteminfo_T = new NativeMethods.MENUITEMINFO_T();
				menuiteminfo_T.cbSize = Marshal.SizeOf(typeof(NativeMethods.MENUITEMINFO_T));
				menuiteminfo_T.fMask = 6;
				for (int i = 0; i < menuItemCount; i++)
				{
					UnsafeNativeMethods.GetMenuItemInfo(new HandleRef(this.menu, this.menu.handle), i, true, menuiteminfo_T);
					if ((menuiteminfo_T.hSubMenu == IntPtr.Zero || menuiteminfo_T.hSubMenu == base.Handle) && menuiteminfo_T.wID == menuID)
					{
						return i;
					}
				}
				return -1;
			}
		}

		// Token: 0x17000B4C RID: 2892
		// (get) Token: 0x06002FE6 RID: 12262 RVA: 0x000D7CD8 File Offset: 0x000D5ED8
		// (set) Token: 0x06002FE7 RID: 12263 RVA: 0x000D7CE5 File Offset: 0x000D5EE5
		[DefaultValue(MenuMerge.Add)]
		[SRDescription("MenuItemMergeTypeDescr")]
		public MenuMerge MergeType
		{
			get
			{
				return this.data.mergeType;
			}
			set
			{
				if (!ClientUtils.IsEnumValid(value, (int)value, 0, 3))
				{
					throw new InvalidEnumArgumentException("value", (int)value, typeof(MenuMerge));
				}
				this.data.MergeType = value;
			}
		}

		// Token: 0x17000B4D RID: 2893
		// (get) Token: 0x06002FE8 RID: 12264 RVA: 0x000D7D19 File Offset: 0x000D5F19
		// (set) Token: 0x06002FE9 RID: 12265 RVA: 0x000D7D26 File Offset: 0x000D5F26
		[DefaultValue(0)]
		[SRDescription("MenuItemMergeOrderDescr")]
		public int MergeOrder
		{
			get
			{
				return this.data.mergeOrder;
			}
			set
			{
				this.data.MergeOrder = value;
			}
		}

		// Token: 0x17000B4E RID: 2894
		// (get) Token: 0x06002FEA RID: 12266 RVA: 0x000D7D34 File Offset: 0x000D5F34
		[Browsable(false)]
		public char Mnemonic
		{
			get
			{
				return this.data.Mnemonic;
			}
		}

		// Token: 0x17000B4F RID: 2895
		// (get) Token: 0x06002FEB RID: 12267 RVA: 0x000D7B95 File Offset: 0x000D5D95
		[Browsable(false)]
		public Menu Parent
		{
			get
			{
				return this.menu;
			}
		}

		// Token: 0x17000B50 RID: 2896
		// (get) Token: 0x06002FEC RID: 12268 RVA: 0x000D7D41 File Offset: 0x000D5F41
		// (set) Token: 0x06002FED RID: 12269 RVA: 0x000D7D57 File Offset: 0x000D5F57
		[DefaultValue(false)]
		[SRDescription("MenuItemRadioCheckDescr")]
		public bool RadioCheck
		{
			get
			{
				return (this.data.State & 512) != 0;
			}
			set
			{
				this.data.SetState(512, value);
			}
		}

		// Token: 0x17000B51 RID: 2897
		// (get) Token: 0x06002FEE RID: 12270 RVA: 0x000D7D6A File Offset: 0x000D5F6A
		internal override bool RenderIsRightToLeft
		{
			get
			{
				return this.Parent != null && this.Parent.RenderIsRightToLeft;
			}
		}

		// Token: 0x17000B52 RID: 2898
		// (get) Token: 0x06002FEF RID: 12271 RVA: 0x000D7D81 File Offset: 0x000D5F81
		// (set) Token: 0x06002FF0 RID: 12272 RVA: 0x000D7D8E File Offset: 0x000D5F8E
		[Localizable(true)]
		[SRDescription("MenuItemTextDescr")]
		public string Text
		{
			get
			{
				return this.data.caption;
			}
			set
			{
				this.data.SetCaption(value);
			}
		}

		// Token: 0x17000B53 RID: 2899
		// (get) Token: 0x06002FF1 RID: 12273 RVA: 0x000D7D9C File Offset: 0x000D5F9C
		// (set) Token: 0x06002FF2 RID: 12274 RVA: 0x000D7DAC File Offset: 0x000D5FAC
		[Localizable(true)]
		[DefaultValue(Shortcut.None)]
		[SRDescription("MenuItemShortCutDescr")]
		public Shortcut Shortcut
		{
			get
			{
				return this.data.shortcut;
			}
			set
			{
				if (!Enum.IsDefined(typeof(Shortcut), value))
				{
					throw new InvalidEnumArgumentException("value", (int)value, typeof(Shortcut));
				}
				this.data.shortcut = value;
				this.UpdateMenuItem(true);
			}
		}

		// Token: 0x17000B54 RID: 2900
		// (get) Token: 0x06002FF3 RID: 12275 RVA: 0x000D7DF9 File Offset: 0x000D5FF9
		// (set) Token: 0x06002FF4 RID: 12276 RVA: 0x000D7E06 File Offset: 0x000D6006
		[DefaultValue(true)]
		[Localizable(true)]
		[SRDescription("MenuItemShowShortCutDescr")]
		public bool ShowShortcut
		{
			get
			{
				return this.data.showShortcut;
			}
			set
			{
				if (value != this.data.showShortcut)
				{
					this.data.showShortcut = value;
					this.UpdateMenuItem(true);
				}
			}
		}

		// Token: 0x17000B55 RID: 2901
		// (get) Token: 0x06002FF5 RID: 12277 RVA: 0x000D7E29 File Offset: 0x000D6029
		// (set) Token: 0x06002FF6 RID: 12278 RVA: 0x000D7E3F File Offset: 0x000D603F
		[Localizable(true)]
		[DefaultValue(true)]
		[SRDescription("MenuItemVisibleDescr")]
		public bool Visible
		{
			get
			{
				return (this.data.State & 65536) == 0;
			}
			set
			{
				this.data.Visible = value;
			}
		}

		// Token: 0x14000229 RID: 553
		// (add) Token: 0x06002FF7 RID: 12279 RVA: 0x000D7E4D File Offset: 0x000D604D
		// (remove) Token: 0x06002FF8 RID: 12280 RVA: 0x000D7E6B File Offset: 0x000D606B
		[SRDescription("MenuItemOnClickDescr")]
		public event EventHandler Click
		{
			add
			{
				MenuItem.MenuItemData menuItemData = this.data;
				menuItemData.onClick = (EventHandler)Delegate.Combine(menuItemData.onClick, value);
			}
			remove
			{
				MenuItem.MenuItemData menuItemData = this.data;
				menuItemData.onClick = (EventHandler)Delegate.Remove(menuItemData.onClick, value);
			}
		}

		// Token: 0x1400022A RID: 554
		// (add) Token: 0x06002FF9 RID: 12281 RVA: 0x000D7E89 File Offset: 0x000D6089
		// (remove) Token: 0x06002FFA RID: 12282 RVA: 0x000D7EA7 File Offset: 0x000D60A7
		[SRCategory("CatBehavior")]
		[SRDescription("drawItemEventDescr")]
		public event DrawItemEventHandler DrawItem
		{
			add
			{
				MenuItem.MenuItemData menuItemData = this.data;
				menuItemData.onDrawItem = (DrawItemEventHandler)Delegate.Combine(menuItemData.onDrawItem, value);
			}
			remove
			{
				MenuItem.MenuItemData menuItemData = this.data;
				menuItemData.onDrawItem = (DrawItemEventHandler)Delegate.Remove(menuItemData.onDrawItem, value);
			}
		}

		// Token: 0x1400022B RID: 555
		// (add) Token: 0x06002FFB RID: 12283 RVA: 0x000D7EC5 File Offset: 0x000D60C5
		// (remove) Token: 0x06002FFC RID: 12284 RVA: 0x000D7EE3 File Offset: 0x000D60E3
		[SRCategory("CatBehavior")]
		[SRDescription("measureItemEventDescr")]
		public event MeasureItemEventHandler MeasureItem
		{
			add
			{
				MenuItem.MenuItemData menuItemData = this.data;
				menuItemData.onMeasureItem = (MeasureItemEventHandler)Delegate.Combine(menuItemData.onMeasureItem, value);
			}
			remove
			{
				MenuItem.MenuItemData menuItemData = this.data;
				menuItemData.onMeasureItem = (MeasureItemEventHandler)Delegate.Remove(menuItemData.onMeasureItem, value);
			}
		}

		// Token: 0x1400022C RID: 556
		// (add) Token: 0x06002FFD RID: 12285 RVA: 0x000D7F01 File Offset: 0x000D6101
		// (remove) Token: 0x06002FFE RID: 12286 RVA: 0x000D7F1F File Offset: 0x000D611F
		[SRDescription("MenuItemOnInitDescr")]
		public event EventHandler Popup
		{
			add
			{
				MenuItem.MenuItemData menuItemData = this.data;
				menuItemData.onPopup = (EventHandler)Delegate.Combine(menuItemData.onPopup, value);
			}
			remove
			{
				MenuItem.MenuItemData menuItemData = this.data;
				menuItemData.onPopup = (EventHandler)Delegate.Remove(menuItemData.onPopup, value);
			}
		}

		// Token: 0x1400022D RID: 557
		// (add) Token: 0x06002FFF RID: 12287 RVA: 0x000D7F3D File Offset: 0x000D613D
		// (remove) Token: 0x06003000 RID: 12288 RVA: 0x000D7F5B File Offset: 0x000D615B
		[SRDescription("MenuItemOnSelectDescr")]
		public event EventHandler Select
		{
			add
			{
				MenuItem.MenuItemData menuItemData = this.data;
				menuItemData.onSelect = (EventHandler)Delegate.Combine(menuItemData.onSelect, value);
			}
			remove
			{
				MenuItem.MenuItemData menuItemData = this.data;
				menuItemData.onSelect = (EventHandler)Delegate.Remove(menuItemData.onSelect, value);
			}
		}

		// Token: 0x06003001 RID: 12289 RVA: 0x000D7F7C File Offset: 0x000D617C
		private static void CleanListItems(MenuItem senderMenu)
		{
			for (int i = senderMenu.MenuItems.Count - 1; i >= 0; i--)
			{
				MenuItem menuItem = senderMenu.MenuItems[i];
				if (menuItem.data.UserData is MenuItem.MdiListUserData)
				{
					menuItem.Dispose();
				}
			}
		}

		// Token: 0x06003002 RID: 12290 RVA: 0x000D7FC8 File Offset: 0x000D61C8
		public virtual MenuItem CloneMenu()
		{
			MenuItem menuItem = new MenuItem();
			menuItem.CloneMenu(this);
			return menuItem;
		}

		// Token: 0x06003003 RID: 12291 RVA: 0x000D7FE4 File Offset: 0x000D61E4
		protected void CloneMenu(MenuItem itemSrc)
		{
			base.CloneMenu(itemSrc);
			int state = itemSrc.data.State;
			new MenuItem.MenuItemData(this, itemSrc.MergeType, itemSrc.MergeOrder, itemSrc.Shortcut, itemSrc.ShowShortcut, itemSrc.Text, itemSrc.data.onClick, itemSrc.data.onPopup, itemSrc.data.onSelect, itemSrc.data.onDrawItem, itemSrc.data.onMeasureItem);
			this.data.SetState(state & 201579, true);
		}

		// Token: 0x06003004 RID: 12292 RVA: 0x000D8074 File Offset: 0x000D6274
		internal virtual void CreateMenuItem()
		{
			if ((this.data.State & 65536) == 0)
			{
				NativeMethods.MENUITEMINFO_T menuiteminfo_T = this.CreateMenuItemInfo();
				UnsafeNativeMethods.InsertMenuItem(new HandleRef(this.menu, this.menu.handle), -1, true, menuiteminfo_T);
				this.hasHandle = (menuiteminfo_T.hSubMenu != IntPtr.Zero);
				this.dataVersion = this.data.version;
				this.menuItemIsCreated = true;
				if (this.RenderIsRightToLeft)
				{
					this.Menu.UpdateRtl(true);
				}
			}
		}

		// Token: 0x06003005 RID: 12293 RVA: 0x000D80FC File Offset: 0x000D62FC
		private NativeMethods.MENUITEMINFO_T CreateMenuItemInfo()
		{
			NativeMethods.MENUITEMINFO_T menuiteminfo_T = new NativeMethods.MENUITEMINFO_T();
			menuiteminfo_T.fMask = 55;
			menuiteminfo_T.fType = (this.data.State & 864);
			bool flag = false;
			if (this.menu == base.GetMainMenu())
			{
				flag = true;
			}
			if (this.data.caption.Equals("-"))
			{
				if (flag)
				{
					this.data.caption = " ";
					menuiteminfo_T.fType |= 64;
				}
				else
				{
					menuiteminfo_T.fType |= 2048;
				}
			}
			menuiteminfo_T.fState = (this.data.State & 4107);
			menuiteminfo_T.wID = this.MenuID;
			if (this.IsParent)
			{
				menuiteminfo_T.hSubMenu = base.Handle;
			}
			menuiteminfo_T.hbmpChecked = IntPtr.Zero;
			menuiteminfo_T.hbmpUnchecked = IntPtr.Zero;
			if (this.uniqueID == 0U)
			{
				Hashtable obj = MenuItem.allCreatedMenuItems;
				lock (obj)
				{
					this.uniqueID = (uint)Interlocked.Increment(ref MenuItem.nextUniqueID);
					MenuItem.allCreatedMenuItems.Add(this.uniqueID, new WeakReference(this));
				}
			}
			if (IntPtr.Size == 4)
			{
				if (this.data.OwnerDraw)
				{
					menuiteminfo_T.dwItemData = this.AllocMsaaMenuInfo();
				}
				else
				{
					menuiteminfo_T.dwItemData = (IntPtr)((int)this.uniqueID);
				}
			}
			else
			{
				menuiteminfo_T.dwItemData = this.AllocMsaaMenuInfo();
			}
			if (this.data.showShortcut && this.data.shortcut != Shortcut.None && !this.IsParent && !flag)
			{
				menuiteminfo_T.dwTypeData = this.data.caption + "\t" + TypeDescriptor.GetConverter(typeof(Keys)).ConvertToString((Keys)this.data.shortcut);
			}
			else
			{
				menuiteminfo_T.dwTypeData = ((this.data.caption.Length == 0) ? " " : this.data.caption);
			}
			menuiteminfo_T.cch = 0;
			return menuiteminfo_T;
		}

		// Token: 0x06003006 RID: 12294 RVA: 0x000D8314 File Offset: 0x000D6514
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (this.menu != null)
				{
					this.menu.MenuItems.Remove(this);
				}
				if (this.data != null)
				{
					this.data.RemoveItem(this);
				}
				Hashtable obj = MenuItem.allCreatedMenuItems;
				lock (obj)
				{
					MenuItem.allCreatedMenuItems.Remove(this.uniqueID);
				}
				this.uniqueID = 0U;
			}
			this.FreeMsaaMenuInfo();
			base.Dispose(disposing);
		}

		// Token: 0x06003007 RID: 12295 RVA: 0x000D83A8 File Offset: 0x000D65A8
		internal static MenuItem GetMenuItemFromUniqueID(uint uniqueID)
		{
			WeakReference weakReference = (WeakReference)MenuItem.allCreatedMenuItems[uniqueID];
			if (weakReference != null && weakReference.IsAlive)
			{
				return (MenuItem)weakReference.Target;
			}
			return null;
		}

		// Token: 0x06003008 RID: 12296 RVA: 0x000D83E4 File Offset: 0x000D65E4
		internal static MenuItem GetMenuItemFromItemData(IntPtr itemData)
		{
			if (itemData == IntPtr.Zero || itemData == (IntPtr)(-1))
			{
				return null;
			}
			uint num = (uint)((long)itemData);
			if (num == 0U)
			{
				return null;
			}
			if (IntPtr.Size == 4)
			{
				if (num < 3221225472U)
				{
					MenuItem.MsaaMenuInfoWithId msaaMenuInfoWithId = (MenuItem.MsaaMenuInfoWithId)Marshal.PtrToStructure(itemData, typeof(MenuItem.MsaaMenuInfoWithId));
					num = msaaMenuInfoWithId.uniqueID;
				}
			}
			else
			{
				MenuItem.MsaaMenuInfoWithId msaaMenuInfoWithId2 = (MenuItem.MsaaMenuInfoWithId)Marshal.PtrToStructure(itemData, typeof(MenuItem.MsaaMenuInfoWithId));
				num = msaaMenuInfoWithId2.uniqueID;
			}
			return MenuItem.GetMenuItemFromUniqueID(num);
		}

		// Token: 0x06003009 RID: 12297 RVA: 0x000D8470 File Offset: 0x000D6670
		private IntPtr AllocMsaaMenuInfo()
		{
			this.FreeMsaaMenuInfo();
			this.msaaMenuInfoPtr = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(MenuItem.MsaaMenuInfoWithId)));
			int size = IntPtr.Size;
			MenuItem.MsaaMenuInfoWithId msaaMenuInfoWithId = new MenuItem.MsaaMenuInfoWithId(this.data.caption, this.uniqueID);
			Marshal.StructureToPtr(msaaMenuInfoWithId, this.msaaMenuInfoPtr, false);
			return this.msaaMenuInfoPtr;
		}

		// Token: 0x0600300A RID: 12298 RVA: 0x000D84D5 File Offset: 0x000D66D5
		private void FreeMsaaMenuInfo()
		{
			if (this.msaaMenuInfoPtr != IntPtr.Zero)
			{
				Marshal.DestroyStructure(this.msaaMenuInfoPtr, typeof(MenuItem.MsaaMenuInfoWithId));
				Marshal.FreeHGlobal(this.msaaMenuInfoPtr);
				this.msaaMenuInfoPtr = IntPtr.Zero;
			}
		}

		// Token: 0x0600300B RID: 12299 RVA: 0x000D8514 File Offset: 0x000D6714
		internal override void ItemsChanged(int change)
		{
			base.ItemsChanged(change);
			if (change == 0)
			{
				if (this.menu != null && this.menu.created)
				{
					this.UpdateMenuItem(true);
					base.CreateMenuItems();
					return;
				}
			}
			else
			{
				if (!this.hasHandle && this.IsParent)
				{
					this.UpdateMenuItem(true);
				}
				MainMenu mainMenu = base.GetMainMenu();
				if (mainMenu != null && (this.data.State & 512) == 0)
				{
					mainMenu.ItemsChanged(change, this);
				}
			}
		}

		// Token: 0x0600300C RID: 12300 RVA: 0x000D858C File Offset: 0x000D678C
		internal void ItemsChanged(int change, MenuItem item)
		{
			if (change == 4 && this.data != null && this.data.baseItem != null && this.data.baseItem.MenuItems.Contains(item))
			{
				if (this.menu != null && this.menu.created)
				{
					this.UpdateMenuItem(true);
					base.CreateMenuItems();
					return;
				}
				if (this.data != null)
				{
					for (MenuItem firstItem = this.data.firstItem; firstItem != null; firstItem = firstItem.nextLinkedItem)
					{
						if (firstItem.created)
						{
							MenuItem item2 = item.CloneMenu();
							item.data.AddItem(item2);
							firstItem.MenuItems.Add(item2);
							return;
						}
					}
				}
			}
		}

		// Token: 0x0600300D RID: 12301 RVA: 0x000D8640 File Offset: 0x000D6840
		internal Form[] FindMdiForms()
		{
			Form[] array = null;
			MainMenu mainMenu = base.GetMainMenu();
			Form form = null;
			if (mainMenu != null)
			{
				form = mainMenu.GetFormUnsafe();
			}
			if (form != null)
			{
				array = form.MdiChildren;
			}
			if (array == null)
			{
				array = new Form[0];
			}
			return array;
		}

		// Token: 0x0600300E RID: 12302 RVA: 0x000D8678 File Offset: 0x000D6878
		private void PopulateMdiList()
		{
			this.data.SetState(512, true);
			try
			{
				MenuItem.CleanListItems(this);
				Form[] array = this.FindMdiForms();
				if (array != null && array.Length != 0)
				{
					Form activeMdiChild = base.GetMainMenu().GetFormUnsafe().ActiveMdiChild;
					if (this.MenuItems.Count > 0)
					{
						MenuItem menuItem = (MenuItem)Activator.CreateInstance(base.GetType());
						menuItem.data.UserData = new MenuItem.MdiListUserData();
						menuItem.Text = "-";
						this.MenuItems.Add(menuItem);
					}
					int num = 0;
					int num2 = 1;
					int num3 = 0;
					bool flag = false;
					for (int i = 0; i < array.Length; i++)
					{
						if (array[i].Visible)
						{
							num++;
							if ((flag && num3 < 9) || (!flag && num3 < 8) || array[i].Equals(activeMdiChild))
							{
								MenuItem menuItem2 = (MenuItem)Activator.CreateInstance(base.GetType());
								menuItem2.data.UserData = new MenuItem.MdiListFormData(this, i);
								if (array[i].Equals(activeMdiChild))
								{
									menuItem2.Checked = true;
									flag = true;
								}
								menuItem2.Text = string.Format(CultureInfo.CurrentUICulture, "&{0} {1}", new object[]
								{
									num2,
									array[i].Text
								});
								num2++;
								num3++;
								this.MenuItems.Add(menuItem2);
							}
						}
					}
					if (num > 9)
					{
						MenuItem menuItem3 = (MenuItem)Activator.CreateInstance(base.GetType());
						menuItem3.data.UserData = new MenuItem.MdiListMoreWindowsData(this);
						menuItem3.Text = SR.GetString("MDIMenuMoreWindows");
						this.MenuItems.Add(menuItem3);
					}
				}
			}
			finally
			{
				this.data.SetState(512, false);
			}
		}

		// Token: 0x0600300F RID: 12303 RVA: 0x000D8868 File Offset: 0x000D6A68
		public virtual MenuItem MergeMenu()
		{
			MenuItem menuItem = (MenuItem)Activator.CreateInstance(base.GetType());
			this.data.AddItem(menuItem);
			menuItem.MergeMenu(this);
			return menuItem;
		}

		// Token: 0x06003010 RID: 12304 RVA: 0x000D889A File Offset: 0x000D6A9A
		public void MergeMenu(MenuItem itemSrc)
		{
			base.MergeMenu(itemSrc);
			itemSrc.data.AddItem(this);
		}

		// Token: 0x06003011 RID: 12305 RVA: 0x000D88B0 File Offset: 0x000D6AB0
		protected virtual void OnClick(EventArgs e)
		{
			if (this.data.UserData is MenuItem.MdiListUserData)
			{
				((MenuItem.MdiListUserData)this.data.UserData).OnClick(e);
				return;
			}
			if (this.data.baseItem != this)
			{
				this.data.baseItem.OnClick(e);
				return;
			}
			if (this.data.onClick != null)
			{
				this.data.onClick(this, e);
			}
		}

		// Token: 0x06003012 RID: 12306 RVA: 0x000D8928 File Offset: 0x000D6B28
		protected virtual void OnDrawItem(DrawItemEventArgs e)
		{
			if (this.data.baseItem != this)
			{
				this.data.baseItem.OnDrawItem(e);
				return;
			}
			if (this.data.onDrawItem != null)
			{
				this.data.onDrawItem(this, e);
			}
		}

		// Token: 0x06003013 RID: 12307 RVA: 0x000D8974 File Offset: 0x000D6B74
		protected virtual void OnMeasureItem(MeasureItemEventArgs e)
		{
			if (this.data.baseItem != this)
			{
				this.data.baseItem.OnMeasureItem(e);
				return;
			}
			if (this.data.onMeasureItem != null)
			{
				this.data.onMeasureItem(this, e);
			}
		}

		// Token: 0x06003014 RID: 12308 RVA: 0x000D89C0 File Offset: 0x000D6BC0
		protected virtual void OnPopup(EventArgs e)
		{
			bool flag = false;
			for (int i = 0; i < base.ItemCount; i++)
			{
				if (this.items[i].MdiList)
				{
					flag = true;
					this.items[i].UpdateMenuItem(true);
				}
			}
			if (flag || (this.hasHandle && !this.IsParent))
			{
				this.UpdateMenuItem(true);
			}
			if (this.data.baseItem != this)
			{
				this.data.baseItem.OnPopup(e);
			}
			else if (this.data.onPopup != null)
			{
				this.data.onPopup(this, e);
			}
			for (int j = 0; j < base.ItemCount; j++)
			{
				this.items[j].UpdateMenuItemIfDirty();
			}
			if (this.MdiList)
			{
				this.PopulateMdiList();
			}
		}

		// Token: 0x06003015 RID: 12309 RVA: 0x000D8A88 File Offset: 0x000D6C88
		protected virtual void OnSelect(EventArgs e)
		{
			if (this.data.baseItem != this)
			{
				this.data.baseItem.OnSelect(e);
				return;
			}
			if (this.data.onSelect != null)
			{
				this.data.onSelect(this, e);
			}
		}

		// Token: 0x06003016 RID: 12310 RVA: 0x000D8AD4 File Offset: 0x000D6CD4
		protected virtual void OnInitMenuPopup(EventArgs e)
		{
			this.OnPopup(e);
		}

		// Token: 0x06003017 RID: 12311 RVA: 0x000D8ADD File Offset: 0x000D6CDD
		internal virtual void _OnInitMenuPopup(EventArgs e)
		{
			this.OnInitMenuPopup(e);
		}

		// Token: 0x06003018 RID: 12312 RVA: 0x000D8AE6 File Offset: 0x000D6CE6
		public void PerformClick()
		{
			this.OnClick(EventArgs.Empty);
		}

		// Token: 0x06003019 RID: 12313 RVA: 0x000D8AF3 File Offset: 0x000D6CF3
		public virtual void PerformSelect()
		{
			this.OnSelect(EventArgs.Empty);
		}

		// Token: 0x0600301A RID: 12314 RVA: 0x000D8B00 File Offset: 0x000D6D00
		internal virtual bool ShortcutClick()
		{
			if (this.menu is MenuItem)
			{
				MenuItem menuItem = (MenuItem)this.menu;
				if (!menuItem.ShortcutClick() || this.menu != menuItem)
				{
					return false;
				}
			}
			if ((this.data.State & 3) != 0)
			{
				return false;
			}
			if (base.ItemCount > 0)
			{
				this.OnPopup(EventArgs.Empty);
			}
			else
			{
				this.OnClick(EventArgs.Empty);
			}
			return true;
		}

		// Token: 0x0600301B RID: 12315 RVA: 0x000D8B6C File Offset: 0x000D6D6C
		public override string ToString()
		{
			string str = base.ToString();
			string str2 = string.Empty;
			if (this.data != null && this.data.caption != null)
			{
				str2 = this.data.caption;
			}
			return str + ", Text: " + str2;
		}

		// Token: 0x0600301C RID: 12316 RVA: 0x000D8BB3 File Offset: 0x000D6DB3
		internal void UpdateMenuItemIfDirty()
		{
			if (this.dataVersion != this.data.version)
			{
				this.UpdateMenuItem(true);
			}
		}

		// Token: 0x0600301D RID: 12317 RVA: 0x000D8BD0 File Offset: 0x000D6DD0
		internal void UpdateItemRtl(bool setRightToLeftBit)
		{
			if (!this.menuItemIsCreated)
			{
				return;
			}
			NativeMethods.MENUITEMINFO_T menuiteminfo_T = new NativeMethods.MENUITEMINFO_T();
			menuiteminfo_T.fMask = 21;
			menuiteminfo_T.dwTypeData = new string('\0', this.Text.Length + 2);
			menuiteminfo_T.cbSize = Marshal.SizeOf(typeof(NativeMethods.MENUITEMINFO_T));
			menuiteminfo_T.cch = menuiteminfo_T.dwTypeData.Length - 1;
			UnsafeNativeMethods.GetMenuItemInfo(new HandleRef(this.menu, this.menu.handle), this.MenuID, false, menuiteminfo_T);
			if (setRightToLeftBit)
			{
				menuiteminfo_T.fType |= 24576;
			}
			else
			{
				menuiteminfo_T.fType &= -24577;
			}
			UnsafeNativeMethods.SetMenuItemInfo(new HandleRef(this.menu, this.menu.handle), this.MenuID, false, menuiteminfo_T);
		}

		// Token: 0x0600301E RID: 12318 RVA: 0x000D8CA8 File Offset: 0x000D6EA8
		internal void UpdateMenuItem(bool force)
		{
			if (this.menu == null || !this.menu.created)
			{
				return;
			}
			if (force || this.menu is MainMenu || this.menu is ContextMenu)
			{
				NativeMethods.MENUITEMINFO_T menuiteminfo_T = this.CreateMenuItemInfo();
				UnsafeNativeMethods.SetMenuItemInfo(new HandleRef(this.menu, this.menu.handle), this.MenuID, false, menuiteminfo_T);
				if (this.hasHandle && menuiteminfo_T.hSubMenu == IntPtr.Zero)
				{
					base.ClearHandles();
				}
				this.hasHandle = (menuiteminfo_T.hSubMenu != IntPtr.Zero);
				this.dataVersion = this.data.version;
				if (this.menu is MainMenu)
				{
					Form formUnsafe = ((MainMenu)this.menu).GetFormUnsafe();
					if (formUnsafe != null)
					{
						SafeNativeMethods.DrawMenuBar(new HandleRef(formUnsafe, formUnsafe.Handle));
					}
				}
			}
		}

		// Token: 0x0600301F RID: 12319 RVA: 0x000D8D90 File Offset: 0x000D6F90
		internal void WmDrawItem(ref Message m)
		{
			NativeMethods.DRAWITEMSTRUCT drawitemstruct = (NativeMethods.DRAWITEMSTRUCT)m.GetLParam(typeof(NativeMethods.DRAWITEMSTRUCT));
			IntPtr intPtr = Control.SetUpPalette(drawitemstruct.hDC, false, false);
			try
			{
				Graphics graphics = Graphics.FromHdcInternal(drawitemstruct.hDC);
				try
				{
					this.OnDrawItem(new DrawItemEventArgs(graphics, SystemInformation.MenuFont, Rectangle.FromLTRB(drawitemstruct.rcItem.left, drawitemstruct.rcItem.top, drawitemstruct.rcItem.right, drawitemstruct.rcItem.bottom), this.Index, (DrawItemState)drawitemstruct.itemState));
				}
				finally
				{
					graphics.Dispose();
				}
			}
			finally
			{
				if (intPtr != IntPtr.Zero)
				{
					SafeNativeMethods.SelectPalette(new HandleRef(null, drawitemstruct.hDC), new HandleRef(null, intPtr), 0);
				}
			}
			m.Result = (IntPtr)1;
		}

		// Token: 0x06003020 RID: 12320 RVA: 0x000D8E74 File Offset: 0x000D7074
		internal void WmMeasureItem(ref Message m)
		{
			NativeMethods.MEASUREITEMSTRUCT measureitemstruct = (NativeMethods.MEASUREITEMSTRUCT)m.GetLParam(typeof(NativeMethods.MEASUREITEMSTRUCT));
			IntPtr dc = UnsafeNativeMethods.GetDC(NativeMethods.NullHandleRef);
			Graphics graphics = Graphics.FromHdcInternal(dc);
			MeasureItemEventArgs measureItemEventArgs = new MeasureItemEventArgs(graphics, this.Index);
			try
			{
				this.OnMeasureItem(measureItemEventArgs);
			}
			finally
			{
				graphics.Dispose();
			}
			UnsafeNativeMethods.ReleaseDC(NativeMethods.NullHandleRef, new HandleRef(null, dc));
			measureitemstruct.itemHeight = measureItemEventArgs.ItemHeight;
			measureitemstruct.itemWidth = measureItemEventArgs.ItemWidth;
			Marshal.StructureToPtr(measureitemstruct, m.LParam, false);
			m.Result = (IntPtr)1;
		}

		// Token: 0x040013C5 RID: 5061
		internal const int STATE_BARBREAK = 32;

		// Token: 0x040013C6 RID: 5062
		internal const int STATE_BREAK = 64;

		// Token: 0x040013C7 RID: 5063
		internal const int STATE_CHECKED = 8;

		// Token: 0x040013C8 RID: 5064
		internal const int STATE_DEFAULT = 4096;

		// Token: 0x040013C9 RID: 5065
		internal const int STATE_DISABLED = 3;

		// Token: 0x040013CA RID: 5066
		internal const int STATE_RADIOCHECK = 512;

		// Token: 0x040013CB RID: 5067
		internal const int STATE_HIDDEN = 65536;

		// Token: 0x040013CC RID: 5068
		internal const int STATE_MDILIST = 131072;

		// Token: 0x040013CD RID: 5069
		internal const int STATE_CLONE_MASK = 201579;

		// Token: 0x040013CE RID: 5070
		internal const int STATE_OWNERDRAW = 256;

		// Token: 0x040013CF RID: 5071
		internal const int STATE_INMDIPOPUP = 512;

		// Token: 0x040013D0 RID: 5072
		internal const int STATE_HILITE = 128;

		// Token: 0x040013D1 RID: 5073
		private Menu menu;

		// Token: 0x040013D2 RID: 5074
		private bool hasHandle;

		// Token: 0x040013D3 RID: 5075
		private MenuItem.MenuItemData data;

		// Token: 0x040013D4 RID: 5076
		private int dataVersion;

		// Token: 0x040013D5 RID: 5077
		private MenuItem nextLinkedItem;

		// Token: 0x040013D6 RID: 5078
		private static Hashtable allCreatedMenuItems = new Hashtable();

		// Token: 0x040013D7 RID: 5079
		private const uint firstUniqueID = 3221225472U;

		// Token: 0x040013D8 RID: 5080
		private static long nextUniqueID = (long)((ulong)-1073741824);

		// Token: 0x040013D9 RID: 5081
		private uint uniqueID;

		// Token: 0x040013DA RID: 5082
		private IntPtr msaaMenuInfoPtr;

		// Token: 0x040013DB RID: 5083
		private bool menuItemIsCreated;

		// Token: 0x020006D8 RID: 1752
		private struct MsaaMenuInfoWithId
		{
			// Token: 0x06006B08 RID: 27400 RVA: 0x0018C9B3 File Offset: 0x0018ABB3
			public MsaaMenuInfoWithId(string text, uint uniqueID)
			{
				this.msaaMenuInfo = new NativeMethods.MSAAMENUINFO(text);
				this.uniqueID = uniqueID;
			}

			// Token: 0x04003B53 RID: 15187
			public NativeMethods.MSAAMENUINFO msaaMenuInfo;

			// Token: 0x04003B54 RID: 15188
			public uint uniqueID;
		}

		// Token: 0x020006D9 RID: 1753
		internal class MenuItemData : ICommandExecutor
		{
			// Token: 0x06006B09 RID: 27401 RVA: 0x0018C9C8 File Offset: 0x0018ABC8
			internal MenuItemData(MenuItem baseItem, MenuMerge mergeType, int mergeOrder, Shortcut shortcut, bool showShortcut, string caption, EventHandler onClick, EventHandler onPopup, EventHandler onSelect, DrawItemEventHandler onDrawItem, MeasureItemEventHandler onMeasureItem)
			{
				this.AddItem(baseItem);
				this.mergeType = mergeType;
				this.mergeOrder = mergeOrder;
				this.shortcut = shortcut;
				this.showShortcut = showShortcut;
				this.caption = ((caption == null) ? "" : caption);
				this.onClick = onClick;
				this.onPopup = onPopup;
				this.onSelect = onSelect;
				this.onDrawItem = onDrawItem;
				this.onMeasureItem = onMeasureItem;
				this.version = 1;
				this.mnemonic = -1;
			}

			// Token: 0x17001735 RID: 5941
			// (get) Token: 0x06006B0A RID: 27402 RVA: 0x0018CA49 File Offset: 0x0018AC49
			// (set) Token: 0x06006B0B RID: 27403 RVA: 0x0018CA5A File Offset: 0x0018AC5A
			internal bool OwnerDraw
			{
				get
				{
					return (this.State & 256) != 0;
				}
				set
				{
					this.SetState(256, value);
				}
			}

			// Token: 0x17001736 RID: 5942
			// (get) Token: 0x06006B0C RID: 27404 RVA: 0x0018CA68 File Offset: 0x0018AC68
			// (set) Token: 0x06006B0D RID: 27405 RVA: 0x0018CA78 File Offset: 0x0018AC78
			internal bool MdiList
			{
				get
				{
					return this.HasState(131072);
				}
				set
				{
					if ((this.state & 131072) != 0 != value)
					{
						this.SetState(131072, value);
						for (MenuItem nextLinkedItem = this.firstItem; nextLinkedItem != null; nextLinkedItem = nextLinkedItem.nextLinkedItem)
						{
							nextLinkedItem.ItemsChanged(2);
						}
					}
				}
			}

			// Token: 0x17001737 RID: 5943
			// (get) Token: 0x06006B0E RID: 27406 RVA: 0x0018CABD File Offset: 0x0018ACBD
			// (set) Token: 0x06006B0F RID: 27407 RVA: 0x0018CAC5 File Offset: 0x0018ACC5
			internal MenuMerge MergeType
			{
				get
				{
					return this.mergeType;
				}
				set
				{
					if (this.mergeType != value)
					{
						this.mergeType = value;
						this.ItemsChanged(3);
					}
				}
			}

			// Token: 0x17001738 RID: 5944
			// (get) Token: 0x06006B10 RID: 27408 RVA: 0x0018CADE File Offset: 0x0018ACDE
			// (set) Token: 0x06006B11 RID: 27409 RVA: 0x0018CAE6 File Offset: 0x0018ACE6
			internal int MergeOrder
			{
				get
				{
					return this.mergeOrder;
				}
				set
				{
					if (this.mergeOrder != value)
					{
						this.mergeOrder = value;
						this.ItemsChanged(3);
					}
				}
			}

			// Token: 0x17001739 RID: 5945
			// (get) Token: 0x06006B12 RID: 27410 RVA: 0x0018CAFF File Offset: 0x0018ACFF
			internal char Mnemonic
			{
				get
				{
					if (this.mnemonic == -1)
					{
						this.mnemonic = (short)WindowsFormsUtils.GetMnemonic(this.caption, true);
					}
					return (char)this.mnemonic;
				}
			}

			// Token: 0x1700173A RID: 5946
			// (get) Token: 0x06006B13 RID: 27411 RVA: 0x0018CB24 File Offset: 0x0018AD24
			internal int State
			{
				get
				{
					return this.state;
				}
			}

			// Token: 0x1700173B RID: 5947
			// (get) Token: 0x06006B14 RID: 27412 RVA: 0x0018CB2C File Offset: 0x0018AD2C
			// (set) Token: 0x06006B15 RID: 27413 RVA: 0x0018CB3D File Offset: 0x0018AD3D
			internal bool Visible
			{
				get
				{
					return (this.state & 65536) == 0;
				}
				set
				{
					if ((this.state & 65536) == 0 != value)
					{
						this.state = (value ? (this.state & -65537) : (this.state | 65536));
						this.ItemsChanged(1);
					}
				}
			}

			// Token: 0x1700173C RID: 5948
			// (get) Token: 0x06006B16 RID: 27414 RVA: 0x0018CB7B File Offset: 0x0018AD7B
			// (set) Token: 0x06006B17 RID: 27415 RVA: 0x0018CB83 File Offset: 0x0018AD83
			internal object UserData
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

			// Token: 0x06006B18 RID: 27416 RVA: 0x0018CB8C File Offset: 0x0018AD8C
			internal void AddItem(MenuItem item)
			{
				if (item.data != this)
				{
					if (item.data != null)
					{
						item.data.RemoveItem(item);
					}
					item.nextLinkedItem = this.firstItem;
					this.firstItem = item;
					if (this.baseItem == null)
					{
						this.baseItem = item;
					}
					item.data = this;
					item.dataVersion = 0;
					item.UpdateMenuItem(false);
				}
			}

			// Token: 0x06006B19 RID: 27417 RVA: 0x0018CBED File Offset: 0x0018ADED
			public void Execute()
			{
				if (this.baseItem != null)
				{
					this.baseItem.OnClick(EventArgs.Empty);
				}
			}

			// Token: 0x06006B1A RID: 27418 RVA: 0x0018CC07 File Offset: 0x0018AE07
			internal int GetMenuID()
			{
				if (this.cmd == null)
				{
					this.cmd = new Command(this);
				}
				return this.cmd.ID;
			}

			// Token: 0x06006B1B RID: 27419 RVA: 0x0018CC28 File Offset: 0x0018AE28
			internal void ItemsChanged(int change)
			{
				for (MenuItem nextLinkedItem = this.firstItem; nextLinkedItem != null; nextLinkedItem = nextLinkedItem.nextLinkedItem)
				{
					if (nextLinkedItem.menu != null)
					{
						nextLinkedItem.menu.ItemsChanged(change);
					}
				}
			}

			// Token: 0x06006B1C RID: 27420 RVA: 0x0018CC5C File Offset: 0x0018AE5C
			internal void RemoveItem(MenuItem item)
			{
				if (item == this.firstItem)
				{
					this.firstItem = item.nextLinkedItem;
				}
				else
				{
					MenuItem nextLinkedItem = this.firstItem;
					while (item != nextLinkedItem.nextLinkedItem)
					{
						nextLinkedItem = nextLinkedItem.nextLinkedItem;
					}
					nextLinkedItem.nextLinkedItem = item.nextLinkedItem;
				}
				item.nextLinkedItem = null;
				item.data = null;
				item.dataVersion = 0;
				if (item == this.baseItem)
				{
					this.baseItem = this.firstItem;
				}
				if (this.firstItem == null)
				{
					this.onClick = null;
					this.onPopup = null;
					this.onSelect = null;
					this.onDrawItem = null;
					this.onMeasureItem = null;
					if (this.cmd != null)
					{
						this.cmd.Dispose();
						this.cmd = null;
					}
				}
			}

			// Token: 0x06006B1D RID: 27421 RVA: 0x0018CD14 File Offset: 0x0018AF14
			internal void SetCaption(string value)
			{
				if (value == null)
				{
					value = "";
				}
				if (!this.caption.Equals(value))
				{
					this.caption = value;
					this.UpdateMenuItems();
				}
			}

			// Token: 0x06006B1E RID: 27422 RVA: 0x0018CD3B File Offset: 0x0018AF3B
			internal bool HasState(int flag)
			{
				return (this.State & flag) == flag;
			}

			// Token: 0x06006B1F RID: 27423 RVA: 0x0018CD48 File Offset: 0x0018AF48
			internal void SetState(int flag, bool value)
			{
				if ((this.state & flag) != 0 != value)
				{
					this.state = (value ? (this.state | flag) : (this.state & ~flag));
					this.UpdateMenuItems();
				}
			}

			// Token: 0x06006B20 RID: 27424 RVA: 0x0018CD7C File Offset: 0x0018AF7C
			internal void UpdateMenuItems()
			{
				this.version++;
				for (MenuItem nextLinkedItem = this.firstItem; nextLinkedItem != null; nextLinkedItem = nextLinkedItem.nextLinkedItem)
				{
					nextLinkedItem.UpdateMenuItem(true);
				}
			}

			// Token: 0x04003B55 RID: 15189
			internal MenuItem baseItem;

			// Token: 0x04003B56 RID: 15190
			internal MenuItem firstItem;

			// Token: 0x04003B57 RID: 15191
			private int state;

			// Token: 0x04003B58 RID: 15192
			internal int version;

			// Token: 0x04003B59 RID: 15193
			internal MenuMerge mergeType;

			// Token: 0x04003B5A RID: 15194
			internal int mergeOrder;

			// Token: 0x04003B5B RID: 15195
			internal string caption;

			// Token: 0x04003B5C RID: 15196
			internal short mnemonic;

			// Token: 0x04003B5D RID: 15197
			internal Shortcut shortcut;

			// Token: 0x04003B5E RID: 15198
			internal bool showShortcut;

			// Token: 0x04003B5F RID: 15199
			internal EventHandler onClick;

			// Token: 0x04003B60 RID: 15200
			internal EventHandler onPopup;

			// Token: 0x04003B61 RID: 15201
			internal EventHandler onSelect;

			// Token: 0x04003B62 RID: 15202
			internal DrawItemEventHandler onDrawItem;

			// Token: 0x04003B63 RID: 15203
			internal MeasureItemEventHandler onMeasureItem;

			// Token: 0x04003B64 RID: 15204
			private object userData;

			// Token: 0x04003B65 RID: 15205
			internal Command cmd;
		}

		// Token: 0x020006DA RID: 1754
		private class MdiListUserData
		{
			// Token: 0x06006B21 RID: 27425 RVA: 0x000072B6 File Offset: 0x000054B6
			public virtual void OnClick(EventArgs e)
			{
			}
		}

		// Token: 0x020006DB RID: 1755
		private class MdiListFormData : MenuItem.MdiListUserData
		{
			// Token: 0x06006B23 RID: 27427 RVA: 0x0018CDB1 File Offset: 0x0018AFB1
			public MdiListFormData(MenuItem parentItem, int boundFormIndex)
			{
				this.boundIndex = boundFormIndex;
				this.parent = parentItem;
			}

			// Token: 0x06006B24 RID: 27428 RVA: 0x0018CDC8 File Offset: 0x0018AFC8
			public override void OnClick(EventArgs e)
			{
				if (this.boundIndex != -1)
				{
					IntSecurity.ModifyFocus.Assert();
					try
					{
						Form[] array = this.parent.FindMdiForms();
						if (array != null && array.Length > this.boundIndex)
						{
							Form form = array[this.boundIndex];
							form.Activate();
							if (form.ActiveControl != null && !form.ActiveControl.Focused)
							{
								form.ActiveControl.Focus();
							}
						}
					}
					finally
					{
						CodeAccessPermission.RevertAssert();
					}
				}
			}

			// Token: 0x04003B66 RID: 15206
			private MenuItem parent;

			// Token: 0x04003B67 RID: 15207
			private int boundIndex;
		}

		// Token: 0x020006DC RID: 1756
		private class MdiListMoreWindowsData : MenuItem.MdiListUserData
		{
			// Token: 0x06006B25 RID: 27429 RVA: 0x0018CE4C File Offset: 0x0018B04C
			public MdiListMoreWindowsData(MenuItem parent)
			{
				this.parent = parent;
			}

			// Token: 0x06006B26 RID: 27430 RVA: 0x0018CE5C File Offset: 0x0018B05C
			public override void OnClick(EventArgs e)
			{
				Form[] array = this.parent.FindMdiForms();
				Form activeMdiChild = this.parent.GetMainMenu().GetFormUnsafe().ActiveMdiChild;
				if (array != null && array.Length != 0 && activeMdiChild != null)
				{
					IntSecurity.AllWindows.Assert();
					try
					{
						using (MdiWindowDialog mdiWindowDialog = new MdiWindowDialog())
						{
							mdiWindowDialog.SetItems(activeMdiChild, array);
							DialogResult dialogResult = mdiWindowDialog.ShowDialog();
							if (dialogResult == DialogResult.OK)
							{
								mdiWindowDialog.ActiveChildForm.Activate();
								if (mdiWindowDialog.ActiveChildForm.ActiveControl != null && !mdiWindowDialog.ActiveChildForm.ActiveControl.Focused)
								{
									mdiWindowDialog.ActiveChildForm.ActiveControl.Focus();
								}
							}
						}
					}
					finally
					{
						CodeAccessPermission.RevertAssert();
					}
				}
			}

			// Token: 0x04003B68 RID: 15208
			private MenuItem parent;
		}
	}
}
