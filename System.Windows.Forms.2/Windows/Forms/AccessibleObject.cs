using System;
using System.Drawing;
using System.Globalization;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using System.Windows.Forms.Automation;
using Accessibility;

namespace System.Windows.Forms
{
	// Token: 0x02000119 RID: 281
	[ComVisible(true)]
	public class AccessibleObject : StandardOleMarshalObject, IReflect, IAccessible, UnsafeNativeMethods.IAccessibleEx, UnsafeNativeMethods.IServiceProvider, UnsafeNativeMethods.IRawElementProviderSimple, UnsafeNativeMethods.IRawElementProviderFragment, UnsafeNativeMethods.IRawElementProviderFragmentRoot, UnsafeNativeMethods.IInvokeProvider, UnsafeNativeMethods.IValueProvider, UnsafeNativeMethods.IRangeValueProvider, UnsafeNativeMethods.IExpandCollapseProvider, UnsafeNativeMethods.IToggleProvider, UnsafeNativeMethods.ITableProvider, UnsafeNativeMethods.ITableItemProvider, UnsafeNativeMethods.IGridProvider, UnsafeNativeMethods.IGridItemProvider, UnsafeNativeMethods.IEnumVariant, UnsafeNativeMethods.IOleWindow, UnsafeNativeMethods.ILegacyIAccessibleProvider, UnsafeNativeMethods.ISelectionProvider, UnsafeNativeMethods.ISelectionItemProvider, UnsafeNativeMethods.IRawElementProviderHwndOverride, UnsafeNativeMethods.IScrollItemProvider, UnsafeNativeMethods.UiaCore.ITextProvider, UnsafeNativeMethods.UiaCore.ITextProvider2
	{
		// Token: 0x06000784 RID: 1924 RVA: 0x00015B2F File Offset: 0x00013D2F
		public AccessibleObject()
		{
		}

		// Token: 0x06000785 RID: 1925 RVA: 0x00015B3F File Offset: 0x00013D3F
		private AccessibleObject(IAccessible iAcc)
		{
			this.systemIAccessible = iAcc;
			this.systemWrapper = true;
		}

		// Token: 0x170001DA RID: 474
		// (get) Token: 0x06000786 RID: 1926 RVA: 0x00015B60 File Offset: 0x00013D60
		public virtual Rectangle Bounds
		{
			get
			{
				if (this.systemIAccessible != null)
				{
					int x = 0;
					int y = 0;
					int width = 0;
					int height = 0;
					try
					{
						this.systemIAccessible.accLocation(out x, out y, out width, out height, 0);
						return new Rectangle(x, y, width, height);
					}
					catch (COMException ex)
					{
						if (ex.ErrorCode != -2147352573)
						{
							throw ex;
						}
					}
				}
				return Rectangle.Empty;
			}
		}

		// Token: 0x170001DB RID: 475
		// (get) Token: 0x06000787 RID: 1927 RVA: 0x00015BD0 File Offset: 0x00013DD0
		public virtual string DefaultAction
		{
			get
			{
				if (this.systemIAccessible != null)
				{
					try
					{
						return this.systemIAccessible.get_accDefaultAction(0);
					}
					catch (COMException ex)
					{
						if (ex.ErrorCode != -2147352573)
						{
							throw ex;
						}
					}
				}
				return null;
			}
		}

		// Token: 0x170001DC RID: 476
		// (get) Token: 0x06000788 RID: 1928 RVA: 0x00015C20 File Offset: 0x00013E20
		public virtual string Description
		{
			get
			{
				if (this.systemIAccessible != null)
				{
					try
					{
						return this.systemIAccessible.get_accDescription(0);
					}
					catch (COMException ex)
					{
						if (ex.ErrorCode != -2147352573)
						{
							throw ex;
						}
					}
				}
				return null;
			}
		}

		// Token: 0x170001DD RID: 477
		// (get) Token: 0x06000789 RID: 1929 RVA: 0x00015C70 File Offset: 0x00013E70
		private UnsafeNativeMethods.IEnumVariant EnumVariant
		{
			get
			{
				if (this.enumVariant == null)
				{
					this.enumVariant = new AccessibleObject.EnumVariantObject(this);
				}
				return this.enumVariant;
			}
		}

		// Token: 0x170001DE RID: 478
		// (get) Token: 0x0600078A RID: 1930 RVA: 0x00015C8C File Offset: 0x00013E8C
		public virtual string Help
		{
			get
			{
				if (this.systemIAccessible != null)
				{
					try
					{
						return this.systemIAccessible.get_accHelp(0);
					}
					catch (COMException ex)
					{
						if (ex.ErrorCode != -2147352573)
						{
							throw ex;
						}
					}
				}
				return null;
			}
		}

		// Token: 0x170001DF RID: 479
		// (get) Token: 0x0600078B RID: 1931 RVA: 0x00015CDC File Offset: 0x00013EDC
		public virtual string KeyboardShortcut
		{
			get
			{
				if (this.systemIAccessible != null)
				{
					try
					{
						return this.systemIAccessible.get_accKeyboardShortcut(0);
					}
					catch (COMException ex)
					{
						if (ex.ErrorCode != -2147352573)
						{
							throw ex;
						}
					}
				}
				return null;
			}
		}

		// Token: 0x170001E0 RID: 480
		// (get) Token: 0x0600078C RID: 1932 RVA: 0x00015D2C File Offset: 0x00013F2C
		// (set) Token: 0x0600078D RID: 1933 RVA: 0x00015D7C File Offset: 0x00013F7C
		public virtual string Name
		{
			get
			{
				if (this.systemIAccessible != null)
				{
					try
					{
						return this.systemIAccessible.get_accName(0);
					}
					catch (COMException ex)
					{
						if (ex.ErrorCode != -2147352573)
						{
							throw ex;
						}
					}
				}
				return null;
			}
			set
			{
				if (this.systemIAccessible != null)
				{
					try
					{
						this.systemIAccessible.set_accName(0, value);
					}
					catch (COMException ex)
					{
						if (ex.ErrorCode != -2147352573)
						{
							throw ex;
						}
					}
				}
			}
		}

		// Token: 0x170001E1 RID: 481
		// (get) Token: 0x0600078E RID: 1934 RVA: 0x00015DC8 File Offset: 0x00013FC8
		public virtual AccessibleObject Parent
		{
			[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
			get
			{
				if (this.systemIAccessible != null)
				{
					return this.WrapIAccessible(this.systemIAccessible.accParent);
				}
				return null;
			}
		}

		// Token: 0x170001E2 RID: 482
		// (get) Token: 0x0600078F RID: 1935 RVA: 0x00015DE5 File Offset: 0x00013FE5
		public virtual AccessibleRole Role
		{
			get
			{
				if (this.systemIAccessible != null)
				{
					return (AccessibleRole)this.systemIAccessible.get_accRole(0);
				}
				return AccessibleRole.None;
			}
		}

		// Token: 0x170001E3 RID: 483
		// (get) Token: 0x06000790 RID: 1936 RVA: 0x00015E07 File Offset: 0x00014007
		public virtual AccessibleStates State
		{
			get
			{
				if (this.systemIAccessible != null)
				{
					return (AccessibleStates)this.systemIAccessible.get_accState(0);
				}
				return AccessibleStates.None;
			}
		}

		// Token: 0x170001E4 RID: 484
		// (get) Token: 0x06000791 RID: 1937 RVA: 0x00015E2C File Offset: 0x0001402C
		// (set) Token: 0x06000792 RID: 1938 RVA: 0x00015E80 File Offset: 0x00014080
		public virtual string Value
		{
			[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
			get
			{
				if (this.systemIAccessible != null)
				{
					try
					{
						return this.systemIAccessible.get_accValue(0);
					}
					catch (COMException ex)
					{
						if (ex.ErrorCode != -2147352573)
						{
							throw ex;
						}
					}
				}
				return "";
			}
			[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
			set
			{
				if (this.systemIAccessible != null)
				{
					try
					{
						this.systemIAccessible.set_accValue(0, value);
					}
					catch (COMException ex)
					{
						if (ex.ErrorCode != -2147352573)
						{
							throw ex;
						}
					}
				}
			}
		}

		// Token: 0x06000793 RID: 1939 RVA: 0x00015ECC File Offset: 0x000140CC
		public virtual AccessibleObject GetChild(int index)
		{
			return null;
		}

		// Token: 0x06000794 RID: 1940 RVA: 0x00015ECF File Offset: 0x000140CF
		public virtual int GetChildCount()
		{
			return -1;
		}

		// Token: 0x06000795 RID: 1941 RVA: 0x00015ECC File Offset: 0x000140CC
		internal virtual int[] GetSysChildOrder()
		{
			return null;
		}

		// Token: 0x06000796 RID: 1942 RVA: 0x00015ED2 File Offset: 0x000140D2
		internal virtual bool GetSysChild(AccessibleNavigation navdir, out AccessibleObject accessibleObject)
		{
			accessibleObject = null;
			return false;
		}

		// Token: 0x06000797 RID: 1943 RVA: 0x00015ED8 File Offset: 0x000140D8
		public virtual AccessibleObject GetFocused()
		{
			if (this.GetChildCount() < 0)
			{
				if (this.systemIAccessible != null)
				{
					try
					{
						return this.WrapIAccessible(this.systemIAccessible.accFocus);
					}
					catch (COMException ex)
					{
						if (ex.ErrorCode != -2147352573)
						{
							throw ex;
						}
					}
				}
				return null;
			}
			int childCount = this.GetChildCount();
			for (int i = 0; i < childCount; i++)
			{
				AccessibleObject child = this.GetChild(i);
				if (child != null && (child.State & AccessibleStates.Focused) != AccessibleStates.None)
				{
					return child;
				}
			}
			if ((this.State & AccessibleStates.Focused) != AccessibleStates.None)
			{
				return this;
			}
			return null;
		}

		// Token: 0x06000798 RID: 1944 RVA: 0x00015F6C File Offset: 0x0001416C
		public virtual int GetHelpTopic(out string fileName)
		{
			if (this.systemIAccessible != null)
			{
				try
				{
					int result = this.systemIAccessible.get_accHelpTopic(out fileName, 0);
					if (fileName != null && fileName.Length > 0)
					{
						IntSecurity.DemandFileIO(FileIOPermissionAccess.PathDiscovery, fileName);
					}
					return result;
				}
				catch (COMException ex)
				{
					if (ex.ErrorCode != -2147352573)
					{
						throw ex;
					}
				}
			}
			fileName = null;
			return -1;
		}

		// Token: 0x06000799 RID: 1945 RVA: 0x00015FD8 File Offset: 0x000141D8
		public virtual AccessibleObject GetSelected()
		{
			if (this.GetChildCount() < 0)
			{
				if (this.systemIAccessible != null)
				{
					try
					{
						return this.WrapIAccessible(this.systemIAccessible.accSelection);
					}
					catch (COMException ex)
					{
						if (ex.ErrorCode != -2147352573)
						{
							throw ex;
						}
					}
				}
				return null;
			}
			int childCount = this.GetChildCount();
			for (int i = 0; i < childCount; i++)
			{
				AccessibleObject child = this.GetChild(i);
				if (child != null && (child.State & AccessibleStates.Selected) != AccessibleStates.None)
				{
					return child;
				}
			}
			if ((this.State & AccessibleStates.Selected) != AccessibleStates.None)
			{
				return this;
			}
			return null;
		}

		// Token: 0x0600079A RID: 1946 RVA: 0x0001606C File Offset: 0x0001426C
		public virtual AccessibleObject HitTest(int x, int y)
		{
			if (this.GetChildCount() >= 0)
			{
				int childCount = this.GetChildCount();
				for (int i = 0; i < childCount; i++)
				{
					AccessibleObject child = this.GetChild(i);
					if (child != null && child.Bounds.Contains(x, y))
					{
						return child;
					}
				}
				return this;
			}
			if (this.systemIAccessible != null)
			{
				try
				{
					return this.WrapIAccessible(this.systemIAccessible.accHitTest(x, y));
				}
				catch (COMException ex)
				{
					if (ex.ErrorCode != -2147352573)
					{
						throw ex;
					}
				}
			}
			if (this.Bounds.Contains(x, y))
			{
				return this;
			}
			return null;
		}

		// Token: 0x0600079B RID: 1947 RVA: 0x00011A20 File Offset: 0x0000FC20
		internal virtual bool IsIAccessibleExSupported()
		{
			return false;
		}

		// Token: 0x0600079C RID: 1948 RVA: 0x00016114 File Offset: 0x00014314
		internal virtual bool IsPatternSupported(int patternId)
		{
			return AccessibilityImprovements.Level3 && patternId == 10000 && this.IsInvokePatternAvailable;
		}

		// Token: 0x170001E5 RID: 485
		// (get) Token: 0x0600079D RID: 1949 RVA: 0x00015ECC File Offset: 0x000140CC
		internal virtual int[] RuntimeId
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170001E6 RID: 486
		// (get) Token: 0x0600079E RID: 1950 RVA: 0x0001612D File Offset: 0x0001432D
		internal virtual int ProviderOptions
		{
			get
			{
				return 34;
			}
		}

		// Token: 0x170001E7 RID: 487
		// (get) Token: 0x0600079F RID: 1951 RVA: 0x00015ECC File Offset: 0x000140CC
		internal virtual UnsafeNativeMethods.IRawElementProviderSimple HostRawElementProvider
		{
			get
			{
				return null;
			}
		}

		// Token: 0x060007A0 RID: 1952 RVA: 0x00016131 File Offset: 0x00014331
		internal virtual object GetPropertyValue(int propertyID)
		{
			if (AccessibilityImprovements.Level3 && propertyID == 30031)
			{
				return this.IsInvokePatternAvailable;
			}
			return null;
		}

		// Token: 0x170001E8 RID: 488
		// (get) Token: 0x060007A1 RID: 1953 RVA: 0x00016150 File Offset: 0x00014350
		private bool IsInvokePatternAvailable
		{
			get
			{
				AccessibleRole role = this.Role;
				switch (role)
				{
				case AccessibleRole.Default:
				case AccessibleRole.None:
				case AccessibleRole.Sound:
				case AccessibleRole.Cursor:
				case AccessibleRole.Caret:
				case AccessibleRole.Alert:
				case AccessibleRole.Client:
				case AccessibleRole.Chart:
				case AccessibleRole.Dialog:
				case AccessibleRole.Border:
					return false;
				case AccessibleRole.TitleBar:
				case AccessibleRole.MenuBar:
				case AccessibleRole.ScrollBar:
				case AccessibleRole.Grip:
				case AccessibleRole.Window:
				case AccessibleRole.MenuPopup:
				case AccessibleRole.ToolTip:
				case AccessibleRole.Application:
				case AccessibleRole.Document:
				case AccessibleRole.Pane:
					goto IL_10A;
				case AccessibleRole.MenuItem:
					break;
				default:
					switch (role)
					{
					case AccessibleRole.Column:
					case AccessibleRole.Row:
					case AccessibleRole.HelpBalloon:
					case AccessibleRole.Character:
					case AccessibleRole.PageTab:
					case AccessibleRole.PropertyPage:
					case AccessibleRole.DropList:
					case AccessibleRole.Dial:
					case AccessibleRole.HotkeyField:
					case AccessibleRole.Diagram:
					case AccessibleRole.Animation:
					case AccessibleRole.Equation:
					case AccessibleRole.WhiteSpace:
					case AccessibleRole.IpAddress:
					case AccessibleRole.OutlineButton:
						return false;
					case AccessibleRole.Cell:
					case AccessibleRole.List:
					case AccessibleRole.ListItem:
					case AccessibleRole.Outline:
					case AccessibleRole.OutlineItem:
					case AccessibleRole.Indicator:
					case AccessibleRole.Graphic:
					case AccessibleRole.StaticText:
					case AccessibleRole.Text:
					case AccessibleRole.CheckButton:
					case AccessibleRole.RadioButton:
					case AccessibleRole.ComboBox:
					case AccessibleRole.ProgressBar:
					case AccessibleRole.Slider:
					case AccessibleRole.SpinButton:
					case AccessibleRole.PageTabList:
						goto IL_10A;
					case AccessibleRole.Link:
					case AccessibleRole.PushButton:
					case AccessibleRole.ButtonDropDown:
					case AccessibleRole.ButtonMenu:
					case AccessibleRole.ButtonDropDownGrid:
					case AccessibleRole.Clock:
					case AccessibleRole.SplitButton:
						break;
					default:
						goto IL_10A;
					}
					break;
				}
				return true;
				IL_10A:
				return !string.IsNullOrEmpty(this.DefaultAction);
			}
		}

		// Token: 0x060007A2 RID: 1954 RVA: 0x00011A20 File Offset: 0x0000FC20
		internal virtual int GetChildId()
		{
			return 0;
		}

		// Token: 0x060007A3 RID: 1955 RVA: 0x00015ECC File Offset: 0x000140CC
		internal virtual UnsafeNativeMethods.IRawElementProviderFragment FragmentNavigate(UnsafeNativeMethods.NavigateDirection direction)
		{
			return null;
		}

		// Token: 0x060007A4 RID: 1956 RVA: 0x00015ECC File Offset: 0x000140CC
		internal virtual UnsafeNativeMethods.IRawElementProviderSimple[] GetEmbeddedFragmentRoots()
		{
			return null;
		}

		// Token: 0x060007A5 RID: 1957 RVA: 0x000072B6 File Offset: 0x000054B6
		internal virtual void SetFocus()
		{
		}

		// Token: 0x170001E9 RID: 489
		// (get) Token: 0x060007A6 RID: 1958 RVA: 0x00016275 File Offset: 0x00014475
		internal virtual Rectangle BoundingRectangle
		{
			get
			{
				return this.Bounds;
			}
		}

		// Token: 0x170001EA RID: 490
		// (get) Token: 0x060007A7 RID: 1959 RVA: 0x00015ECC File Offset: 0x000140CC
		internal virtual UnsafeNativeMethods.IRawElementProviderFragmentRoot FragmentRoot
		{
			get
			{
				return null;
			}
		}

		// Token: 0x060007A8 RID: 1960 RVA: 0x00006C59 File Offset: 0x00004E59
		internal virtual UnsafeNativeMethods.IRawElementProviderFragment ElementProviderFromPoint(double x, double y)
		{
			return this;
		}

		// Token: 0x060007A9 RID: 1961 RVA: 0x00015ECC File Offset: 0x000140CC
		internal virtual UnsafeNativeMethods.IRawElementProviderFragment GetFocus()
		{
			return null;
		}

		// Token: 0x060007AA RID: 1962 RVA: 0x000072B6 File Offset: 0x000054B6
		internal virtual void Expand()
		{
		}

		// Token: 0x060007AB RID: 1963 RVA: 0x000072B6 File Offset: 0x000054B6
		internal virtual void Collapse()
		{
		}

		// Token: 0x170001EB RID: 491
		// (get) Token: 0x060007AC RID: 1964 RVA: 0x00011A20 File Offset: 0x0000FC20
		internal virtual UnsafeNativeMethods.ExpandCollapseState ExpandCollapseState
		{
			get
			{
				return UnsafeNativeMethods.ExpandCollapseState.Collapsed;
			}
		}

		// Token: 0x060007AD RID: 1965 RVA: 0x000072B6 File Offset: 0x000054B6
		internal virtual void Toggle()
		{
		}

		// Token: 0x170001EC RID: 492
		// (get) Token: 0x060007AE RID: 1966 RVA: 0x0001627D File Offset: 0x0001447D
		internal virtual UnsafeNativeMethods.ToggleState ToggleState
		{
			get
			{
				return UnsafeNativeMethods.ToggleState.ToggleState_Indeterminate;
			}
		}

		// Token: 0x060007AF RID: 1967 RVA: 0x00015ECC File Offset: 0x000140CC
		internal virtual UnsafeNativeMethods.IRawElementProviderSimple[] GetRowHeaders()
		{
			return null;
		}

		// Token: 0x060007B0 RID: 1968 RVA: 0x00015ECC File Offset: 0x000140CC
		internal virtual UnsafeNativeMethods.IRawElementProviderSimple[] GetColumnHeaders()
		{
			return null;
		}

		// Token: 0x170001ED RID: 493
		// (get) Token: 0x060007B1 RID: 1969 RVA: 0x00011A20 File Offset: 0x0000FC20
		internal virtual UnsafeNativeMethods.RowOrColumnMajor RowOrColumnMajor
		{
			get
			{
				return UnsafeNativeMethods.RowOrColumnMajor.RowOrColumnMajor_RowMajor;
			}
		}

		// Token: 0x060007B2 RID: 1970 RVA: 0x00015ECC File Offset: 0x000140CC
		internal virtual UnsafeNativeMethods.IRawElementProviderSimple[] GetRowHeaderItems()
		{
			return null;
		}

		// Token: 0x060007B3 RID: 1971 RVA: 0x00015ECC File Offset: 0x000140CC
		internal virtual UnsafeNativeMethods.IRawElementProviderSimple[] GetColumnHeaderItems()
		{
			return null;
		}

		// Token: 0x060007B4 RID: 1972 RVA: 0x00015ECC File Offset: 0x000140CC
		internal virtual UnsafeNativeMethods.IRawElementProviderSimple GetItem(int row, int column)
		{
			return null;
		}

		// Token: 0x170001EE RID: 494
		// (get) Token: 0x060007B5 RID: 1973 RVA: 0x00015ECF File Offset: 0x000140CF
		internal virtual int RowCount
		{
			get
			{
				return -1;
			}
		}

		// Token: 0x170001EF RID: 495
		// (get) Token: 0x060007B6 RID: 1974 RVA: 0x00015ECF File Offset: 0x000140CF
		internal virtual int ColumnCount
		{
			get
			{
				return -1;
			}
		}

		// Token: 0x170001F0 RID: 496
		// (get) Token: 0x060007B7 RID: 1975 RVA: 0x00015ECF File Offset: 0x000140CF
		internal virtual int Row
		{
			get
			{
				return -1;
			}
		}

		// Token: 0x170001F1 RID: 497
		// (get) Token: 0x060007B8 RID: 1976 RVA: 0x00015ECF File Offset: 0x000140CF
		internal virtual int Column
		{
			get
			{
				return -1;
			}
		}

		// Token: 0x170001F2 RID: 498
		// (get) Token: 0x060007B9 RID: 1977 RVA: 0x00013062 File Offset: 0x00011262
		internal virtual int RowSpan
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x170001F3 RID: 499
		// (get) Token: 0x060007BA RID: 1978 RVA: 0x00013062 File Offset: 0x00011262
		internal virtual int ColumnSpan
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x170001F4 RID: 500
		// (get) Token: 0x060007BB RID: 1979 RVA: 0x00015ECC File Offset: 0x000140CC
		internal virtual UnsafeNativeMethods.IRawElementProviderSimple ContainingGrid
		{
			get
			{
				return null;
			}
		}

		// Token: 0x060007BC RID: 1980 RVA: 0x00016280 File Offset: 0x00014480
		internal virtual void Invoke()
		{
			this.DoDefaultAction();
		}

		// Token: 0x170001F5 RID: 501
		// (get) Token: 0x060007BD RID: 1981 RVA: 0x00015ECC File Offset: 0x000140CC
		internal virtual UnsafeNativeMethods.UiaCore.ITextRangeProvider DocumentRangeInternal
		{
			get
			{
				return null;
			}
		}

		// Token: 0x060007BE RID: 1982 RVA: 0x00015ECC File Offset: 0x000140CC
		internal virtual UnsafeNativeMethods.UiaCore.ITextRangeProvider[] GetTextSelection()
		{
			return null;
		}

		// Token: 0x060007BF RID: 1983 RVA: 0x00015ECC File Offset: 0x000140CC
		internal virtual UnsafeNativeMethods.UiaCore.ITextRangeProvider[] GetTextVisibleRanges()
		{
			return null;
		}

		// Token: 0x060007C0 RID: 1984 RVA: 0x00015ECC File Offset: 0x000140CC
		internal virtual UnsafeNativeMethods.UiaCore.ITextRangeProvider GetTextRangeFromChild(UnsafeNativeMethods.IRawElementProviderSimple childElement)
		{
			return null;
		}

		// Token: 0x060007C1 RID: 1985 RVA: 0x00015ECC File Offset: 0x000140CC
		internal virtual UnsafeNativeMethods.UiaCore.ITextRangeProvider GetTextRangeFromPoint(Point screenLocation)
		{
			return null;
		}

		// Token: 0x170001F6 RID: 502
		// (get) Token: 0x060007C2 RID: 1986 RVA: 0x00011A20 File Offset: 0x0000FC20
		internal virtual UnsafeNativeMethods.UiaCore.SupportedTextSelection SupportedTextSelectionInternal
		{
			get
			{
				return UnsafeNativeMethods.UiaCore.SupportedTextSelection.None;
			}
		}

		// Token: 0x060007C3 RID: 1987 RVA: 0x00016288 File Offset: 0x00014488
		internal virtual UnsafeNativeMethods.UiaCore.ITextRangeProvider GetTextCaretRange(out UnsafeNativeMethods.BOOL isActive)
		{
			isActive = UnsafeNativeMethods.BOOL.FALSE;
			return null;
		}

		// Token: 0x060007C4 RID: 1988 RVA: 0x00015ECC File Offset: 0x000140CC
		internal virtual UnsafeNativeMethods.UiaCore.ITextRangeProvider GetRangeFromAnnotation(UnsafeNativeMethods.IRawElementProviderSimple annotationElement)
		{
			return null;
		}

		// Token: 0x170001F7 RID: 503
		// (get) Token: 0x060007C5 RID: 1989 RVA: 0x00011A20 File Offset: 0x0000FC20
		internal virtual bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060007C6 RID: 1990 RVA: 0x0001628E File Offset: 0x0001448E
		internal virtual void SetValue(string newValue)
		{
			this.Value = newValue;
		}

		// Token: 0x060007C7 RID: 1991 RVA: 0x00015ECC File Offset: 0x000140CC
		internal virtual UnsafeNativeMethods.IRawElementProviderSimple GetOverrideProviderForHwnd(IntPtr hwnd)
		{
			return null;
		}

		// Token: 0x060007C8 RID: 1992 RVA: 0x000072B6 File Offset: 0x000054B6
		internal virtual void SetValue(double newValue)
		{
		}

		// Token: 0x170001F8 RID: 504
		// (get) Token: 0x060007C9 RID: 1993 RVA: 0x00016297 File Offset: 0x00014497
		internal virtual double LargeChange
		{
			get
			{
				return double.NaN;
			}
		}

		// Token: 0x170001F9 RID: 505
		// (get) Token: 0x060007CA RID: 1994 RVA: 0x00016297 File Offset: 0x00014497
		internal virtual double Maximum
		{
			get
			{
				return double.NaN;
			}
		}

		// Token: 0x170001FA RID: 506
		// (get) Token: 0x060007CB RID: 1995 RVA: 0x00016297 File Offset: 0x00014497
		internal virtual double Minimum
		{
			get
			{
				return double.NaN;
			}
		}

		// Token: 0x170001FB RID: 507
		// (get) Token: 0x060007CC RID: 1996 RVA: 0x00016297 File Offset: 0x00014497
		internal virtual double SmallChange
		{
			get
			{
				return double.NaN;
			}
		}

		// Token: 0x170001FC RID: 508
		// (get) Token: 0x060007CD RID: 1997 RVA: 0x00016297 File Offset: 0x00014497
		internal virtual double RangeValue
		{
			get
			{
				return double.NaN;
			}
		}

		// Token: 0x060007CE RID: 1998 RVA: 0x00015ECC File Offset: 0x000140CC
		internal virtual UnsafeNativeMethods.IRawElementProviderSimple[] GetSelection()
		{
			return null;
		}

		// Token: 0x170001FD RID: 509
		// (get) Token: 0x060007CF RID: 1999 RVA: 0x00011A20 File Offset: 0x0000FC20
		internal virtual bool CanSelectMultiple
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170001FE RID: 510
		// (get) Token: 0x060007D0 RID: 2000 RVA: 0x00011A20 File Offset: 0x0000FC20
		internal virtual bool IsSelectionRequired
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060007D1 RID: 2001 RVA: 0x000072B6 File Offset: 0x000054B6
		internal virtual void SelectItem()
		{
		}

		// Token: 0x060007D2 RID: 2002 RVA: 0x000072B6 File Offset: 0x000054B6
		internal virtual void AddToSelection()
		{
		}

		// Token: 0x060007D3 RID: 2003 RVA: 0x000072B6 File Offset: 0x000054B6
		internal virtual void RemoveFromSelection()
		{
		}

		// Token: 0x170001FF RID: 511
		// (get) Token: 0x060007D4 RID: 2004 RVA: 0x00011A20 File Offset: 0x0000FC20
		internal virtual bool IsItemSelected
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000200 RID: 512
		// (get) Token: 0x060007D5 RID: 2005 RVA: 0x00015ECC File Offset: 0x000140CC
		internal virtual UnsafeNativeMethods.IRawElementProviderSimple ItemSelectionContainer
		{
			get
			{
				return null;
			}
		}

		// Token: 0x060007D6 RID: 2006 RVA: 0x000072B6 File Offset: 0x000054B6
		internal virtual void SetParent(AccessibleObject parent)
		{
		}

		// Token: 0x060007D7 RID: 2007 RVA: 0x000072B6 File Offset: 0x000054B6
		internal virtual void SetDetachableChild(AccessibleObject child)
		{
		}

		// Token: 0x060007D8 RID: 2008 RVA: 0x000162A4 File Offset: 0x000144A4
		int UnsafeNativeMethods.IServiceProvider.QueryService(ref Guid service, ref Guid riid, out IntPtr ppvObject)
		{
			int result = -2147467262;
			ppvObject = IntPtr.Zero;
			if (this.IsIAccessibleExSupported() && service.Equals(UnsafeNativeMethods.guid_IAccessibleEx) && riid.Equals(UnsafeNativeMethods.guid_IAccessibleEx))
			{
				ppvObject = Marshal.GetComInterfaceForObject(this, typeof(UnsafeNativeMethods.IAccessibleEx));
				result = 0;
			}
			return result;
		}

		// Token: 0x060007D9 RID: 2009 RVA: 0x000162F5 File Offset: 0x000144F5
		object UnsafeNativeMethods.IAccessibleEx.GetObjectForChild(int childId)
		{
			return this.GetObjectForChild(childId);
		}

		// Token: 0x060007DA RID: 2010 RVA: 0x00015ECC File Offset: 0x000140CC
		internal virtual object GetObjectForChild(int childId)
		{
			return null;
		}

		// Token: 0x060007DB RID: 2011 RVA: 0x000162FE File Offset: 0x000144FE
		int UnsafeNativeMethods.IAccessibleEx.GetIAccessiblePair(out object ppAcc, out int pidChild)
		{
			ppAcc = null;
			pidChild = 0;
			return -2147467261;
		}

		// Token: 0x060007DC RID: 2012 RVA: 0x0001630B File Offset: 0x0001450B
		int[] UnsafeNativeMethods.IAccessibleEx.GetRuntimeId()
		{
			return this.RuntimeId;
		}

		// Token: 0x060007DD RID: 2013 RVA: 0x00016313 File Offset: 0x00014513
		int UnsafeNativeMethods.IAccessibleEx.ConvertReturnedElement(object pIn, out object ppRetValOut)
		{
			ppRetValOut = null;
			return -2147467263;
		}

		// Token: 0x17000201 RID: 513
		// (get) Token: 0x060007DE RID: 2014 RVA: 0x0001631D File Offset: 0x0001451D
		UnsafeNativeMethods.ProviderOptions UnsafeNativeMethods.IRawElementProviderSimple.ProviderOptions
		{
			get
			{
				return (UnsafeNativeMethods.ProviderOptions)this.ProviderOptions;
			}
		}

		// Token: 0x17000202 RID: 514
		// (get) Token: 0x060007DF RID: 2015 RVA: 0x00016325 File Offset: 0x00014525
		UnsafeNativeMethods.IRawElementProviderSimple UnsafeNativeMethods.IRawElementProviderSimple.HostRawElementProvider
		{
			get
			{
				return this.HostRawElementProvider;
			}
		}

		// Token: 0x060007E0 RID: 2016 RVA: 0x0001632D File Offset: 0x0001452D
		object UnsafeNativeMethods.IRawElementProviderSimple.GetPatternProvider(int patternId)
		{
			if (this.IsPatternSupported(patternId))
			{
				return this;
			}
			return null;
		}

		// Token: 0x060007E1 RID: 2017 RVA: 0x0001633B File Offset: 0x0001453B
		object UnsafeNativeMethods.IRawElementProviderSimple.GetPropertyValue(int propertyID)
		{
			return this.GetPropertyValue(propertyID);
		}

		// Token: 0x060007E2 RID: 2018 RVA: 0x00016344 File Offset: 0x00014544
		object UnsafeNativeMethods.IRawElementProviderFragment.Navigate(UnsafeNativeMethods.NavigateDirection direction)
		{
			return this.FragmentNavigate(direction);
		}

		// Token: 0x060007E3 RID: 2019 RVA: 0x0001630B File Offset: 0x0001450B
		int[] UnsafeNativeMethods.IRawElementProviderFragment.GetRuntimeId()
		{
			return this.RuntimeId;
		}

		// Token: 0x060007E4 RID: 2020 RVA: 0x00016350 File Offset: 0x00014550
		object[] UnsafeNativeMethods.IRawElementProviderFragment.GetEmbeddedFragmentRoots()
		{
			return this.GetEmbeddedFragmentRoots();
		}

		// Token: 0x060007E5 RID: 2021 RVA: 0x00016365 File Offset: 0x00014565
		void UnsafeNativeMethods.IRawElementProviderFragment.SetFocus()
		{
			this.SetFocus();
		}

		// Token: 0x17000203 RID: 515
		// (get) Token: 0x060007E6 RID: 2022 RVA: 0x0001636D File Offset: 0x0001456D
		NativeMethods.UiaRect UnsafeNativeMethods.IRawElementProviderFragment.BoundingRectangle
		{
			get
			{
				return new NativeMethods.UiaRect(this.BoundingRectangle);
			}
		}

		// Token: 0x17000204 RID: 516
		// (get) Token: 0x060007E7 RID: 2023 RVA: 0x0001637A File Offset: 0x0001457A
		UnsafeNativeMethods.IRawElementProviderFragmentRoot UnsafeNativeMethods.IRawElementProviderFragment.FragmentRoot
		{
			get
			{
				return this.FragmentRoot;
			}
		}

		// Token: 0x060007E8 RID: 2024 RVA: 0x00016382 File Offset: 0x00014582
		object UnsafeNativeMethods.IRawElementProviderFragmentRoot.ElementProviderFromPoint(double x, double y)
		{
			return this.ElementProviderFromPoint(x, y);
		}

		// Token: 0x060007E9 RID: 2025 RVA: 0x0001638C File Offset: 0x0001458C
		object UnsafeNativeMethods.IRawElementProviderFragmentRoot.GetFocus()
		{
			return this.GetFocus();
		}

		// Token: 0x17000205 RID: 517
		// (get) Token: 0x060007EA RID: 2026 RVA: 0x00016394 File Offset: 0x00014594
		string UnsafeNativeMethods.ILegacyIAccessibleProvider.DefaultAction
		{
			get
			{
				return this.DefaultAction;
			}
		}

		// Token: 0x17000206 RID: 518
		// (get) Token: 0x060007EB RID: 2027 RVA: 0x0001639C File Offset: 0x0001459C
		string UnsafeNativeMethods.ILegacyIAccessibleProvider.Description
		{
			get
			{
				return this.Description;
			}
		}

		// Token: 0x17000207 RID: 519
		// (get) Token: 0x060007EC RID: 2028 RVA: 0x000163A4 File Offset: 0x000145A4
		string UnsafeNativeMethods.ILegacyIAccessibleProvider.Help
		{
			get
			{
				return this.Help;
			}
		}

		// Token: 0x17000208 RID: 520
		// (get) Token: 0x060007ED RID: 2029 RVA: 0x000163AC File Offset: 0x000145AC
		string UnsafeNativeMethods.ILegacyIAccessibleProvider.KeyboardShortcut
		{
			get
			{
				return this.KeyboardShortcut;
			}
		}

		// Token: 0x17000209 RID: 521
		// (get) Token: 0x060007EE RID: 2030 RVA: 0x000163B4 File Offset: 0x000145B4
		string UnsafeNativeMethods.ILegacyIAccessibleProvider.Name
		{
			get
			{
				return this.Name;
			}
		}

		// Token: 0x1700020A RID: 522
		// (get) Token: 0x060007EF RID: 2031 RVA: 0x000163BC File Offset: 0x000145BC
		uint UnsafeNativeMethods.ILegacyIAccessibleProvider.Role
		{
			get
			{
				return (uint)this.Role;
			}
		}

		// Token: 0x1700020B RID: 523
		// (get) Token: 0x060007F0 RID: 2032 RVA: 0x000163C4 File Offset: 0x000145C4
		uint UnsafeNativeMethods.ILegacyIAccessibleProvider.State
		{
			get
			{
				return (uint)this.State;
			}
		}

		// Token: 0x1700020C RID: 524
		// (get) Token: 0x060007F1 RID: 2033 RVA: 0x000163CC File Offset: 0x000145CC
		string UnsafeNativeMethods.ILegacyIAccessibleProvider.Value
		{
			get
			{
				return this.Value;
			}
		}

		// Token: 0x1700020D RID: 525
		// (get) Token: 0x060007F2 RID: 2034 RVA: 0x000163D4 File Offset: 0x000145D4
		int UnsafeNativeMethods.ILegacyIAccessibleProvider.ChildId
		{
			get
			{
				return this.GetChildId();
			}
		}

		// Token: 0x060007F3 RID: 2035 RVA: 0x00016280 File Offset: 0x00014480
		void UnsafeNativeMethods.ILegacyIAccessibleProvider.DoDefaultAction()
		{
			this.DoDefaultAction();
		}

		// Token: 0x060007F4 RID: 2036 RVA: 0x000163DC File Offset: 0x000145DC
		IAccessible UnsafeNativeMethods.ILegacyIAccessibleProvider.GetIAccessible()
		{
			return this.AsIAccessible(this);
		}

		// Token: 0x060007F5 RID: 2037 RVA: 0x000163E8 File Offset: 0x000145E8
		object[] UnsafeNativeMethods.ILegacyIAccessibleProvider.GetSelection()
		{
			return new UnsafeNativeMethods.IRawElementProviderSimple[]
			{
				this.GetSelected()
			};
		}

		// Token: 0x060007F6 RID: 2038 RVA: 0x00016406 File Offset: 0x00014606
		void UnsafeNativeMethods.ILegacyIAccessibleProvider.Select(int flagsSelect)
		{
			this.Select((AccessibleSelection)flagsSelect);
		}

		// Token: 0x060007F7 RID: 2039 RVA: 0x0001640F File Offset: 0x0001460F
		void UnsafeNativeMethods.ILegacyIAccessibleProvider.SetValue(string szValue)
		{
			this.SetValue(szValue);
		}

		// Token: 0x060007F8 RID: 2040 RVA: 0x00016418 File Offset: 0x00014618
		void UnsafeNativeMethods.IExpandCollapseProvider.Expand()
		{
			this.Expand();
		}

		// Token: 0x060007F9 RID: 2041 RVA: 0x00016420 File Offset: 0x00014620
		void UnsafeNativeMethods.IExpandCollapseProvider.Collapse()
		{
			this.Collapse();
		}

		// Token: 0x1700020E RID: 526
		// (get) Token: 0x060007FA RID: 2042 RVA: 0x00016428 File Offset: 0x00014628
		UnsafeNativeMethods.ExpandCollapseState UnsafeNativeMethods.IExpandCollapseProvider.ExpandCollapseState
		{
			get
			{
				return this.ExpandCollapseState;
			}
		}

		// Token: 0x060007FB RID: 2043 RVA: 0x00016430 File Offset: 0x00014630
		void UnsafeNativeMethods.IInvokeProvider.Invoke()
		{
			this.Invoke();
		}

		// Token: 0x1700020F RID: 527
		// (get) Token: 0x060007FC RID: 2044 RVA: 0x00016438 File Offset: 0x00014638
		UnsafeNativeMethods.UiaCore.ITextRangeProvider UnsafeNativeMethods.UiaCore.ITextProvider.DocumentRange
		{
			get
			{
				return this.DocumentRangeInternal;
			}
		}

		// Token: 0x060007FD RID: 2045 RVA: 0x00016440 File Offset: 0x00014640
		UnsafeNativeMethods.UiaCore.ITextRangeProvider[] UnsafeNativeMethods.UiaCore.ITextProvider.GetSelection()
		{
			return this.GetTextSelection();
		}

		// Token: 0x060007FE RID: 2046 RVA: 0x00016448 File Offset: 0x00014648
		UnsafeNativeMethods.UiaCore.ITextRangeProvider[] UnsafeNativeMethods.UiaCore.ITextProvider.GetVisibleRanges()
		{
			return this.GetTextVisibleRanges();
		}

		// Token: 0x060007FF RID: 2047 RVA: 0x00016450 File Offset: 0x00014650
		UnsafeNativeMethods.UiaCore.ITextRangeProvider UnsafeNativeMethods.UiaCore.ITextProvider.RangeFromChild(UnsafeNativeMethods.IRawElementProviderSimple childElement)
		{
			return this.GetTextRangeFromChild(childElement);
		}

		// Token: 0x06000800 RID: 2048 RVA: 0x00016459 File Offset: 0x00014659
		UnsafeNativeMethods.UiaCore.ITextRangeProvider UnsafeNativeMethods.UiaCore.ITextProvider.RangeFromPoint(Point screenLocation)
		{
			return this.GetTextRangeFromPoint(screenLocation);
		}

		// Token: 0x17000210 RID: 528
		// (get) Token: 0x06000801 RID: 2049 RVA: 0x00016462 File Offset: 0x00014662
		UnsafeNativeMethods.UiaCore.SupportedTextSelection UnsafeNativeMethods.UiaCore.ITextProvider.SupportedTextSelection
		{
			get
			{
				return this.SupportedTextSelectionInternal;
			}
		}

		// Token: 0x17000211 RID: 529
		// (get) Token: 0x06000802 RID: 2050 RVA: 0x00016438 File Offset: 0x00014638
		UnsafeNativeMethods.UiaCore.ITextRangeProvider UnsafeNativeMethods.UiaCore.ITextProvider2.DocumentRange
		{
			get
			{
				return this.DocumentRangeInternal;
			}
		}

		// Token: 0x06000803 RID: 2051 RVA: 0x00016440 File Offset: 0x00014640
		UnsafeNativeMethods.UiaCore.ITextRangeProvider[] UnsafeNativeMethods.UiaCore.ITextProvider2.GetSelection()
		{
			return this.GetTextSelection();
		}

		// Token: 0x06000804 RID: 2052 RVA: 0x00016448 File Offset: 0x00014648
		UnsafeNativeMethods.UiaCore.ITextRangeProvider[] UnsafeNativeMethods.UiaCore.ITextProvider2.GetVisibleRanges()
		{
			return this.GetTextVisibleRanges();
		}

		// Token: 0x06000805 RID: 2053 RVA: 0x00016450 File Offset: 0x00014650
		UnsafeNativeMethods.UiaCore.ITextRangeProvider UnsafeNativeMethods.UiaCore.ITextProvider2.RangeFromChild(UnsafeNativeMethods.IRawElementProviderSimple childElement)
		{
			return this.GetTextRangeFromChild(childElement);
		}

		// Token: 0x06000806 RID: 2054 RVA: 0x00016459 File Offset: 0x00014659
		UnsafeNativeMethods.UiaCore.ITextRangeProvider UnsafeNativeMethods.UiaCore.ITextProvider2.RangeFromPoint(Point screenLocation)
		{
			return this.GetTextRangeFromPoint(screenLocation);
		}

		// Token: 0x17000212 RID: 530
		// (get) Token: 0x06000807 RID: 2055 RVA: 0x00016462 File Offset: 0x00014662
		UnsafeNativeMethods.UiaCore.SupportedTextSelection UnsafeNativeMethods.UiaCore.ITextProvider2.SupportedTextSelection
		{
			get
			{
				return this.SupportedTextSelectionInternal;
			}
		}

		// Token: 0x06000808 RID: 2056 RVA: 0x0001646A File Offset: 0x0001466A
		UnsafeNativeMethods.UiaCore.ITextRangeProvider UnsafeNativeMethods.UiaCore.ITextProvider2.GetCaretRange(out UnsafeNativeMethods.BOOL isActive)
		{
			return this.GetTextCaretRange(out isActive);
		}

		// Token: 0x06000809 RID: 2057 RVA: 0x00016473 File Offset: 0x00014673
		UnsafeNativeMethods.UiaCore.ITextRangeProvider UnsafeNativeMethods.UiaCore.ITextProvider2.RangeFromAnnotation(UnsafeNativeMethods.IRawElementProviderSimple annotationElement)
		{
			return this.GetRangeFromAnnotation(annotationElement);
		}

		// Token: 0x17000213 RID: 531
		// (get) Token: 0x0600080A RID: 2058 RVA: 0x0001647C File Offset: 0x0001467C
		bool UnsafeNativeMethods.IValueProvider.IsReadOnly
		{
			get
			{
				return this.IsReadOnly;
			}
		}

		// Token: 0x17000214 RID: 532
		// (get) Token: 0x0600080B RID: 2059 RVA: 0x000163CC File Offset: 0x000145CC
		string UnsafeNativeMethods.IValueProvider.Value
		{
			get
			{
				return this.Value;
			}
		}

		// Token: 0x0600080C RID: 2060 RVA: 0x0001640F File Offset: 0x0001460F
		void UnsafeNativeMethods.IValueProvider.SetValue(string newValue)
		{
			this.SetValue(newValue);
		}

		// Token: 0x0600080D RID: 2061 RVA: 0x00016484 File Offset: 0x00014684
		void UnsafeNativeMethods.IToggleProvider.Toggle()
		{
			this.Toggle();
		}

		// Token: 0x17000215 RID: 533
		// (get) Token: 0x0600080E RID: 2062 RVA: 0x0001648C File Offset: 0x0001468C
		UnsafeNativeMethods.ToggleState UnsafeNativeMethods.IToggleProvider.ToggleState
		{
			get
			{
				return this.ToggleState;
			}
		}

		// Token: 0x0600080F RID: 2063 RVA: 0x00016494 File Offset: 0x00014694
		object[] UnsafeNativeMethods.ITableProvider.GetRowHeaders()
		{
			return this.GetRowHeaders();
		}

		// Token: 0x06000810 RID: 2064 RVA: 0x000164AC File Offset: 0x000146AC
		object[] UnsafeNativeMethods.ITableProvider.GetColumnHeaders()
		{
			return this.GetColumnHeaders();
		}

		// Token: 0x17000216 RID: 534
		// (get) Token: 0x06000811 RID: 2065 RVA: 0x000164C1 File Offset: 0x000146C1
		UnsafeNativeMethods.RowOrColumnMajor UnsafeNativeMethods.ITableProvider.RowOrColumnMajor
		{
			get
			{
				return this.RowOrColumnMajor;
			}
		}

		// Token: 0x06000812 RID: 2066 RVA: 0x000164CC File Offset: 0x000146CC
		object[] UnsafeNativeMethods.ITableItemProvider.GetRowHeaderItems()
		{
			return this.GetRowHeaderItems();
		}

		// Token: 0x06000813 RID: 2067 RVA: 0x000164E4 File Offset: 0x000146E4
		object[] UnsafeNativeMethods.ITableItemProvider.GetColumnHeaderItems()
		{
			return this.GetColumnHeaderItems();
		}

		// Token: 0x06000814 RID: 2068 RVA: 0x000164F9 File Offset: 0x000146F9
		object UnsafeNativeMethods.IGridProvider.GetItem(int row, int column)
		{
			return this.GetItem(row, column);
		}

		// Token: 0x17000217 RID: 535
		// (get) Token: 0x06000815 RID: 2069 RVA: 0x00016503 File Offset: 0x00014703
		int UnsafeNativeMethods.IGridProvider.RowCount
		{
			get
			{
				return this.RowCount;
			}
		}

		// Token: 0x17000218 RID: 536
		// (get) Token: 0x06000816 RID: 2070 RVA: 0x0001650B File Offset: 0x0001470B
		int UnsafeNativeMethods.IGridProvider.ColumnCount
		{
			get
			{
				return this.ColumnCount;
			}
		}

		// Token: 0x17000219 RID: 537
		// (get) Token: 0x06000817 RID: 2071 RVA: 0x00016513 File Offset: 0x00014713
		int UnsafeNativeMethods.IGridItemProvider.Row
		{
			get
			{
				return this.Row;
			}
		}

		// Token: 0x1700021A RID: 538
		// (get) Token: 0x06000818 RID: 2072 RVA: 0x0001651B File Offset: 0x0001471B
		int UnsafeNativeMethods.IGridItemProvider.Column
		{
			get
			{
				return this.Column;
			}
		}

		// Token: 0x1700021B RID: 539
		// (get) Token: 0x06000819 RID: 2073 RVA: 0x00016523 File Offset: 0x00014723
		int UnsafeNativeMethods.IGridItemProvider.RowSpan
		{
			get
			{
				return this.RowSpan;
			}
		}

		// Token: 0x1700021C RID: 540
		// (get) Token: 0x0600081A RID: 2074 RVA: 0x0001652B File Offset: 0x0001472B
		int UnsafeNativeMethods.IGridItemProvider.ColumnSpan
		{
			get
			{
				return this.ColumnSpan;
			}
		}

		// Token: 0x1700021D RID: 541
		// (get) Token: 0x0600081B RID: 2075 RVA: 0x00016533 File Offset: 0x00014733
		UnsafeNativeMethods.IRawElementProviderSimple UnsafeNativeMethods.IGridItemProvider.ContainingGrid
		{
			get
			{
				return this.ContainingGrid;
			}
		}

		// Token: 0x0600081C RID: 2076 RVA: 0x0001653C File Offset: 0x0001473C
		void IAccessible.accDoDefaultAction(object childID)
		{
			IntSecurity.UnmanagedCode.Demand();
			if (this.IsClientObject)
			{
				this.ValidateChildID(ref childID);
				if (childID.Equals(0))
				{
					this.DoDefaultAction();
					return;
				}
				AccessibleObject accessibleChild = this.GetAccessibleChild(childID);
				if (accessibleChild != null)
				{
					accessibleChild.DoDefaultAction();
					return;
				}
			}
			if (this.systemIAccessible != null)
			{
				try
				{
					this.systemIAccessible.accDoDefaultAction(childID);
				}
				catch (COMException ex)
				{
					if (ex.ErrorCode != -2147352573)
					{
						throw ex;
					}
				}
			}
		}

		// Token: 0x0600081D RID: 2077 RVA: 0x000165C4 File Offset: 0x000147C4
		object IAccessible.accHitTest(int xLeft, int yTop)
		{
			if (this.IsClientObject)
			{
				AccessibleObject accessibleObject = this.HitTest(xLeft, yTop);
				if (accessibleObject != null)
				{
					return this.AsVariant(accessibleObject);
				}
			}
			if (this.systemIAccessible != null)
			{
				try
				{
					return this.systemIAccessible.accHitTest(xLeft, yTop);
				}
				catch (COMException ex)
				{
					if (ex.ErrorCode != -2147352573)
					{
						throw ex;
					}
				}
			}
			return null;
		}

		// Token: 0x0600081E RID: 2078 RVA: 0x0001662C File Offset: 0x0001482C
		void IAccessible.accLocation(out int pxLeft, out int pyTop, out int pcxWidth, out int pcyHeight, object childID)
		{
			pxLeft = 0;
			pyTop = 0;
			pcxWidth = 0;
			pcyHeight = 0;
			if (this.IsClientObject)
			{
				this.ValidateChildID(ref childID);
				if (childID.Equals(0))
				{
					Rectangle bounds = this.Bounds;
					pxLeft = bounds.X;
					pyTop = bounds.Y;
					pcxWidth = bounds.Width;
					pcyHeight = bounds.Height;
					return;
				}
				AccessibleObject accessibleChild = this.GetAccessibleChild(childID);
				if (accessibleChild != null)
				{
					Rectangle bounds2 = accessibleChild.Bounds;
					pxLeft = bounds2.X;
					pyTop = bounds2.Y;
					pcxWidth = bounds2.Width;
					pcyHeight = bounds2.Height;
					return;
				}
			}
			if (this.systemIAccessible != null)
			{
				try
				{
					this.systemIAccessible.accLocation(out pxLeft, out pyTop, out pcxWidth, out pcyHeight, childID);
				}
				catch (COMException ex)
				{
					if (ex.ErrorCode != -2147352573)
					{
						throw ex;
					}
				}
				return;
			}
		}

		// Token: 0x0600081F RID: 2079 RVA: 0x0001670C File Offset: 0x0001490C
		object IAccessible.accNavigate(int navDir, object childID)
		{
			IntSecurity.UnmanagedCode.Demand();
			if (this.IsClientObject)
			{
				this.ValidateChildID(ref childID);
				if (childID.Equals(0))
				{
					AccessibleObject accessibleObject = this.Navigate((AccessibleNavigation)navDir);
					if (accessibleObject != null)
					{
						return this.AsVariant(accessibleObject);
					}
				}
				AccessibleObject accessibleChild = this.GetAccessibleChild(childID);
				if (accessibleChild != null)
				{
					return this.AsVariant(accessibleChild.Navigate((AccessibleNavigation)navDir));
				}
			}
			if (this.systemIAccessible != null)
			{
				try
				{
					object result;
					if (!this.SysNavigate(navDir, childID, out result))
					{
						result = this.systemIAccessible.accNavigate(navDir, childID);
					}
					return result;
				}
				catch (COMException ex)
				{
					if (ex.ErrorCode != -2147352573)
					{
						throw ex;
					}
				}
			}
			return null;
		}

		// Token: 0x06000820 RID: 2080 RVA: 0x000167BC File Offset: 0x000149BC
		void IAccessible.accSelect(int flagsSelect, object childID)
		{
			IntSecurity.UnmanagedCode.Demand();
			if (this.IsClientObject)
			{
				this.ValidateChildID(ref childID);
				if (childID.Equals(0))
				{
					this.Select((AccessibleSelection)flagsSelect);
					return;
				}
				AccessibleObject accessibleChild = this.GetAccessibleChild(childID);
				if (accessibleChild != null)
				{
					accessibleChild.Select((AccessibleSelection)flagsSelect);
					return;
				}
			}
			if (this.systemIAccessible != null)
			{
				try
				{
					this.systemIAccessible.accSelect(flagsSelect, childID);
				}
				catch (COMException ex)
				{
					if (ex.ErrorCode != -2147352573)
					{
						throw ex;
					}
				}
				return;
			}
		}

		// Token: 0x06000821 RID: 2081 RVA: 0x00016848 File Offset: 0x00014A48
		[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		public virtual void DoDefaultAction()
		{
			if (this.systemIAccessible != null)
			{
				try
				{
					this.systemIAccessible.accDoDefaultAction(0);
				}
				catch (COMException ex)
				{
					if (ex.ErrorCode != -2147352573)
					{
						throw ex;
					}
				}
				return;
			}
		}

		// Token: 0x06000822 RID: 2082 RVA: 0x00016894 File Offset: 0x00014A94
		object IAccessible.get_accChild(object childID)
		{
			if (this.IsClientObject)
			{
				this.ValidateChildID(ref childID);
				if (childID.Equals(0))
				{
					return this.AsIAccessible(this);
				}
				AccessibleObject accessibleChild = this.GetAccessibleChild(childID);
				if (accessibleChild != null)
				{
					if (accessibleChild == this)
					{
						return null;
					}
					return this.AsIAccessible(accessibleChild);
				}
			}
			if (this.systemIAccessible != null)
			{
				return this.systemIAccessible.get_accChild(childID);
			}
			return null;
		}

		// Token: 0x1700021E RID: 542
		// (get) Token: 0x06000823 RID: 2083 RVA: 0x000168F8 File Offset: 0x00014AF8
		int IAccessible.accChildCount
		{
			get
			{
				int num = -1;
				if (this.IsClientObject)
				{
					num = this.GetChildCount();
				}
				if (num == -1)
				{
					if (this.systemIAccessible != null)
					{
						num = this.systemIAccessible.accChildCount;
					}
					else
					{
						num = 0;
					}
				}
				return num;
			}
		}

		// Token: 0x06000824 RID: 2084 RVA: 0x00016934 File Offset: 0x00014B34
		string IAccessible.get_accDefaultAction(object childID)
		{
			if (this.IsClientObject)
			{
				this.ValidateChildID(ref childID);
				if (childID.Equals(0))
				{
					return this.DefaultAction;
				}
				AccessibleObject accessibleChild = this.GetAccessibleChild(childID);
				if (accessibleChild != null)
				{
					return accessibleChild.DefaultAction;
				}
			}
			if (this.systemIAccessible != null)
			{
				try
				{
					return this.systemIAccessible.get_accDefaultAction(childID);
				}
				catch (COMException ex)
				{
					if (ex.ErrorCode != -2147352573)
					{
						throw ex;
					}
				}
			}
			return null;
		}

		// Token: 0x06000825 RID: 2085 RVA: 0x000169B4 File Offset: 0x00014BB4
		string IAccessible.get_accDescription(object childID)
		{
			if (this.IsClientObject)
			{
				this.ValidateChildID(ref childID);
				if (childID.Equals(0))
				{
					return this.Description;
				}
				AccessibleObject accessibleChild = this.GetAccessibleChild(childID);
				if (accessibleChild != null)
				{
					return accessibleChild.Description;
				}
			}
			if (this.systemIAccessible != null)
			{
				try
				{
					return this.systemIAccessible.get_accDescription(childID);
				}
				catch (COMException ex)
				{
					if (ex.ErrorCode != -2147352573)
					{
						throw ex;
					}
				}
			}
			return null;
		}

		// Token: 0x06000826 RID: 2086 RVA: 0x00016A34 File Offset: 0x00014C34
		private AccessibleObject GetAccessibleChild(object childID)
		{
			if (!childID.Equals(0))
			{
				int num = (int)childID - 1;
				if (num >= 0 && num < this.GetChildCount())
				{
					return this.GetChild(num);
				}
			}
			return null;
		}

		// Token: 0x1700021F RID: 543
		// (get) Token: 0x06000827 RID: 2087 RVA: 0x00016A70 File Offset: 0x00014C70
		object IAccessible.accFocus
		{
			get
			{
				if (this.IsClientObject)
				{
					AccessibleObject focused = this.GetFocused();
					if (focused != null)
					{
						return this.AsVariant(focused);
					}
				}
				if (this.systemIAccessible != null)
				{
					try
					{
						return this.systemIAccessible.accFocus;
					}
					catch (COMException ex)
					{
						if (ex.ErrorCode != -2147352573)
						{
							throw ex;
						}
					}
				}
				return null;
			}
		}

		// Token: 0x06000828 RID: 2088 RVA: 0x00016AD4 File Offset: 0x00014CD4
		string IAccessible.get_accHelp(object childID)
		{
			if (this.IsClientObject)
			{
				this.ValidateChildID(ref childID);
				if (childID.Equals(0))
				{
					return this.Help;
				}
				AccessibleObject accessibleChild = this.GetAccessibleChild(childID);
				if (accessibleChild != null)
				{
					return accessibleChild.Help;
				}
			}
			if (this.systemIAccessible != null)
			{
				try
				{
					return this.systemIAccessible.get_accHelp(childID);
				}
				catch (COMException ex)
				{
					if (ex.ErrorCode != -2147352573)
					{
						throw ex;
					}
				}
			}
			return null;
		}

		// Token: 0x06000829 RID: 2089 RVA: 0x00016B54 File Offset: 0x00014D54
		int IAccessible.get_accHelpTopic(out string pszHelpFile, object childID)
		{
			if (this.IsClientObject)
			{
				this.ValidateChildID(ref childID);
				if (childID.Equals(0))
				{
					return this.GetHelpTopic(out pszHelpFile);
				}
				AccessibleObject accessibleChild = this.GetAccessibleChild(childID);
				if (accessibleChild != null)
				{
					return accessibleChild.GetHelpTopic(out pszHelpFile);
				}
			}
			if (this.systemIAccessible != null)
			{
				try
				{
					return this.systemIAccessible.get_accHelpTopic(out pszHelpFile, childID);
				}
				catch (COMException ex)
				{
					if (ex.ErrorCode != -2147352573)
					{
						throw ex;
					}
				}
			}
			pszHelpFile = null;
			return -1;
		}

		// Token: 0x0600082A RID: 2090 RVA: 0x00016BDC File Offset: 0x00014DDC
		string IAccessible.get_accKeyboardShortcut(object childID)
		{
			return this.get_accKeyboardShortcutInternal(childID);
		}

		// Token: 0x0600082B RID: 2091 RVA: 0x00016BE8 File Offset: 0x00014DE8
		internal virtual string get_accKeyboardShortcutInternal(object childID)
		{
			if (this.IsClientObject)
			{
				this.ValidateChildID(ref childID);
				if (childID.Equals(0))
				{
					return this.KeyboardShortcut;
				}
				AccessibleObject accessibleChild = this.GetAccessibleChild(childID);
				if (accessibleChild != null)
				{
					return accessibleChild.KeyboardShortcut;
				}
			}
			if (this.systemIAccessible != null)
			{
				try
				{
					return this.systemIAccessible.get_accKeyboardShortcut(childID);
				}
				catch (COMException ex)
				{
					if (ex.ErrorCode != -2147352573)
					{
						throw ex;
					}
				}
			}
			return null;
		}

		// Token: 0x0600082C RID: 2092 RVA: 0x00016C68 File Offset: 0x00014E68
		string IAccessible.get_accName(object childID)
		{
			return this.get_accNameInternal(childID);
		}

		// Token: 0x0600082D RID: 2093 RVA: 0x00016C74 File Offset: 0x00014E74
		internal virtual string get_accNameInternal(object childID)
		{
			if (this.IsClientObject)
			{
				this.ValidateChildID(ref childID);
				if (childID.Equals(0))
				{
					return this.Name;
				}
				AccessibleObject accessibleChild = this.GetAccessibleChild(childID);
				if (accessibleChild != null)
				{
					return accessibleChild.Name;
				}
			}
			if (this.systemIAccessible != null)
			{
				string text = this.systemIAccessible.get_accName(childID);
				if (this.IsClientObject && (text == null || text.Length == 0))
				{
					text = this.Name;
				}
				return text;
			}
			return null;
		}

		// Token: 0x17000220 RID: 544
		// (get) Token: 0x0600082E RID: 2094 RVA: 0x00016CEC File Offset: 0x00014EEC
		object IAccessible.accParent
		{
			get
			{
				IntSecurity.UnmanagedCode.Demand();
				AccessibleObject accessibleObject = this.Parent;
				if (accessibleObject != null && accessibleObject == this)
				{
					accessibleObject = null;
				}
				return this.AsIAccessible(accessibleObject);
			}
		}

		// Token: 0x0600082F RID: 2095 RVA: 0x00016D1C File Offset: 0x00014F1C
		object IAccessible.get_accRole(object childID)
		{
			if (this.IsClientObject)
			{
				this.ValidateChildID(ref childID);
				if (childID.Equals(0))
				{
					return (int)this.Role;
				}
				AccessibleObject accessibleChild = this.GetAccessibleChild(childID);
				if (accessibleChild != null)
				{
					return (int)accessibleChild.Role;
				}
			}
			if (this.systemIAccessible != null)
			{
				return this.systemIAccessible.get_accRole(childID);
			}
			return null;
		}

		// Token: 0x17000221 RID: 545
		// (get) Token: 0x06000830 RID: 2096 RVA: 0x00016D80 File Offset: 0x00014F80
		object IAccessible.accSelection
		{
			get
			{
				if (this.IsClientObject)
				{
					AccessibleObject selected = this.GetSelected();
					if (selected != null)
					{
						return this.AsVariant(selected);
					}
				}
				if (this.systemIAccessible != null)
				{
					try
					{
						return this.systemIAccessible.accSelection;
					}
					catch (COMException ex)
					{
						if (ex.ErrorCode != -2147352573)
						{
							throw ex;
						}
					}
				}
				return null;
			}
		}

		// Token: 0x06000831 RID: 2097 RVA: 0x00016DE4 File Offset: 0x00014FE4
		object IAccessible.get_accState(object childID)
		{
			if (this.IsClientObject)
			{
				this.ValidateChildID(ref childID);
				if (childID.Equals(0))
				{
					return (int)this.State;
				}
				AccessibleObject accessibleChild = this.GetAccessibleChild(childID);
				if (accessibleChild != null)
				{
					return (int)accessibleChild.State;
				}
			}
			if (this.systemIAccessible != null)
			{
				return this.systemIAccessible.get_accState(childID);
			}
			return null;
		}

		// Token: 0x06000832 RID: 2098 RVA: 0x00016E48 File Offset: 0x00015048
		string IAccessible.get_accValue(object childID)
		{
			IntSecurity.UnmanagedCode.Demand();
			if (this.IsClientObject)
			{
				this.ValidateChildID(ref childID);
				if (childID.Equals(0))
				{
					return this.Value;
				}
				AccessibleObject accessibleChild = this.GetAccessibleChild(childID);
				if (accessibleChild != null)
				{
					return accessibleChild.Value;
				}
			}
			if (this.systemIAccessible != null)
			{
				try
				{
					return this.systemIAccessible.get_accValue(childID);
				}
				catch (COMException ex)
				{
					if (ex.ErrorCode != -2147352573)
					{
						throw ex;
					}
				}
			}
			return null;
		}

		// Token: 0x06000833 RID: 2099 RVA: 0x00016ED4 File Offset: 0x000150D4
		void IAccessible.set_accName(object childID, string newName)
		{
			if (this.IsClientObject)
			{
				this.ValidateChildID(ref childID);
				if (childID.Equals(0))
				{
					this.Name = newName;
					return;
				}
				AccessibleObject accessibleChild = this.GetAccessibleChild(childID);
				if (accessibleChild != null)
				{
					accessibleChild.Name = newName;
					return;
				}
			}
			if (this.systemIAccessible != null)
			{
				this.systemIAccessible.set_accName(childID, newName);
				return;
			}
		}

		// Token: 0x06000834 RID: 2100 RVA: 0x00016F30 File Offset: 0x00015130
		void IAccessible.set_accValue(object childID, string newValue)
		{
			IntSecurity.UnmanagedCode.Demand();
			if (this.IsClientObject)
			{
				this.ValidateChildID(ref childID);
				if (childID.Equals(0))
				{
					this.Value = newValue;
					return;
				}
				AccessibleObject accessibleChild = this.GetAccessibleChild(childID);
				if (accessibleChild != null)
				{
					accessibleChild.Value = newValue;
					return;
				}
			}
			if (this.systemIAccessible != null)
			{
				try
				{
					this.systemIAccessible.set_accValue(childID, newValue);
				}
				catch (COMException ex)
				{
					if (ex.ErrorCode != -2147352573)
					{
						throw ex;
					}
				}
				return;
			}
		}

		// Token: 0x06000835 RID: 2101 RVA: 0x00016FBC File Offset: 0x000151BC
		[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		int UnsafeNativeMethods.IOleWindow.GetWindow(out IntPtr hwnd)
		{
			if (this.systemIOleWindow != null)
			{
				return this.systemIOleWindow.GetWindow(out hwnd);
			}
			AccessibleObject parent = this.Parent;
			if (parent != null)
			{
				return ((UnsafeNativeMethods.IOleWindow)parent).GetWindow(out hwnd);
			}
			hwnd = IntPtr.Zero;
			return -2147467259;
		}

		// Token: 0x06000836 RID: 2102 RVA: 0x00016FFC File Offset: 0x000151FC
		[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		void UnsafeNativeMethods.IOleWindow.ContextSensitiveHelp(int fEnterMode)
		{
			if (this.systemIOleWindow != null)
			{
				this.systemIOleWindow.ContextSensitiveHelp(fEnterMode);
				return;
			}
			AccessibleObject parent = this.Parent;
			if (parent != null)
			{
				((UnsafeNativeMethods.IOleWindow)parent).ContextSensitiveHelp(fEnterMode);
				return;
			}
		}

		// Token: 0x06000837 RID: 2103 RVA: 0x00017030 File Offset: 0x00015230
		void UnsafeNativeMethods.IEnumVariant.Clone(UnsafeNativeMethods.IEnumVariant[] v)
		{
			this.EnumVariant.Clone(v);
		}

		// Token: 0x06000838 RID: 2104 RVA: 0x0001703E File Offset: 0x0001523E
		int UnsafeNativeMethods.IEnumVariant.Next(int n, IntPtr rgvar, int[] ns)
		{
			return this.EnumVariant.Next(n, rgvar, ns);
		}

		// Token: 0x06000839 RID: 2105 RVA: 0x0001704E File Offset: 0x0001524E
		void UnsafeNativeMethods.IEnumVariant.Reset()
		{
			this.EnumVariant.Reset();
		}

		// Token: 0x0600083A RID: 2106 RVA: 0x0001705B File Offset: 0x0001525B
		void UnsafeNativeMethods.IEnumVariant.Skip(int n)
		{
			this.EnumVariant.Skip(n);
		}

		// Token: 0x0600083B RID: 2107 RVA: 0x0001706C File Offset: 0x0001526C
		[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		public virtual AccessibleObject Navigate(AccessibleNavigation navdir)
		{
			if (LocalAppContextSwitches.FreeControlsForRefCountedAccessibleObjectsInLevel5 && this.Parent == null)
			{
				return null;
			}
			if (this.GetChildCount() >= 0)
			{
				switch (navdir)
				{
				case AccessibleNavigation.Up:
				case AccessibleNavigation.Left:
				case AccessibleNavigation.Previous:
					if (this.Parent.GetChildCount() > 0)
					{
						return null;
					}
					break;
				case AccessibleNavigation.Down:
				case AccessibleNavigation.Right:
				case AccessibleNavigation.Next:
					if (this.Parent.GetChildCount() > 0)
					{
						return null;
					}
					break;
				case AccessibleNavigation.FirstChild:
					return this.GetChild(0);
				case AccessibleNavigation.LastChild:
					return this.GetChild(this.GetChildCount() - 1);
				}
			}
			if (this.systemIAccessible != null)
			{
				try
				{
					object iacc = null;
					if (!this.SysNavigate((int)navdir, 0, out iacc))
					{
						iacc = this.systemIAccessible.accNavigate((int)navdir, 0);
					}
					return this.WrapIAccessible(iacc);
				}
				catch (COMException ex)
				{
					if (ex.ErrorCode != -2147352573)
					{
						throw ex;
					}
				}
			}
			return null;
		}

		// Token: 0x0600083C RID: 2108 RVA: 0x00017154 File Offset: 0x00015354
		[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		public virtual void Select(AccessibleSelection flags)
		{
			if (this.systemIAccessible != null)
			{
				try
				{
					this.systemIAccessible.accSelect((int)flags, 0);
				}
				catch (COMException ex)
				{
					if (ex.ErrorCode != -2147352573)
					{
						throw ex;
					}
				}
				return;
			}
		}

		// Token: 0x0600083D RID: 2109 RVA: 0x000171A0 File Offset: 0x000153A0
		private object AsVariant(AccessibleObject obj)
		{
			if (obj == this)
			{
				return 0;
			}
			return this.AsIAccessible(obj);
		}

		// Token: 0x0600083E RID: 2110 RVA: 0x000171B4 File Offset: 0x000153B4
		private IAccessible AsIAccessible(AccessibleObject obj)
		{
			if (obj != null && obj.systemWrapper)
			{
				return obj.systemIAccessible;
			}
			return obj;
		}

		// Token: 0x17000222 RID: 546
		// (get) Token: 0x0600083F RID: 2111 RVA: 0x000171C9 File Offset: 0x000153C9
		// (set) Token: 0x06000840 RID: 2112 RVA: 0x000171D1 File Offset: 0x000153D1
		internal int AccessibleObjectId
		{
			get
			{
				return this.accObjId;
			}
			set
			{
				this.accObjId = value;
			}
		}

		// Token: 0x17000223 RID: 547
		// (get) Token: 0x06000841 RID: 2113 RVA: 0x000171DA File Offset: 0x000153DA
		internal bool IsClientObject
		{
			get
			{
				return this.AccessibleObjectId == -4;
			}
		}

		// Token: 0x17000224 RID: 548
		// (get) Token: 0x06000842 RID: 2114 RVA: 0x000171E6 File Offset: 0x000153E6
		internal bool IsNonClientObject
		{
			get
			{
				return this.AccessibleObjectId == 0;
			}
		}

		// Token: 0x06000843 RID: 2115 RVA: 0x000171F1 File Offset: 0x000153F1
		[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		internal IAccessible GetSystemIAccessibleInternal()
		{
			return this.systemIAccessible;
		}

		// Token: 0x06000844 RID: 2116 RVA: 0x000171F9 File Offset: 0x000153F9
		[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		protected void UseStdAccessibleObjects(IntPtr handle)
		{
			this.UseStdAccessibleObjects(handle, this.AccessibleObjectId);
		}

		// Token: 0x06000845 RID: 2117 RVA: 0x00017208 File Offset: 0x00015408
		[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		protected void UseStdAccessibleObjects(IntPtr handle, int objid)
		{
			Guid guid = new Guid("{618736E0-3C3D-11CF-810C-00AA00389B71}");
			object obj = null;
			int num = UnsafeNativeMethods.CreateStdAccessibleObject(new HandleRef(this, handle), objid, ref guid, ref obj);
			Guid guid2 = new Guid("{00020404-0000-0000-C000-000000000046}");
			object obj2 = null;
			num = UnsafeNativeMethods.CreateStdAccessibleObject(new HandleRef(this, handle), objid, ref guid2, ref obj2);
			if (obj != null || obj2 != null)
			{
				this.systemIAccessible = (IAccessible)obj;
				this.systemIEnumVariant = (UnsafeNativeMethods.IEnumVariant)obj2;
				this.systemIOleWindow = (obj as UnsafeNativeMethods.IOleWindow);
			}
		}

		// Token: 0x06000846 RID: 2118 RVA: 0x00017284 File Offset: 0x00015484
		private bool SysNavigate(int navDir, object childID, out object retObject)
		{
			retObject = null;
			if (!childID.Equals(0))
			{
				return false;
			}
			AccessibleObject accessibleObject;
			if (!this.GetSysChild((AccessibleNavigation)navDir, out accessibleObject))
			{
				return false;
			}
			retObject = ((accessibleObject == null) ? null : this.AsVariant(accessibleObject));
			return true;
		}

		// Token: 0x06000847 RID: 2119 RVA: 0x000172C1 File Offset: 0x000154C1
		internal void ValidateChildID(ref object childID)
		{
			if (childID == null)
			{
				childID = 0;
				return;
			}
			if (childID.Equals(-2147352572))
			{
				childID = 0;
				return;
			}
			if (!(childID is int))
			{
				childID = 0;
			}
		}

		// Token: 0x06000848 RID: 2120 RVA: 0x00017300 File Offset: 0x00015500
		private AccessibleObject WrapIAccessible(object iacc)
		{
			IAccessible accessible = iacc as IAccessible;
			if (accessible == null)
			{
				return null;
			}
			if (this.systemIAccessible == iacc)
			{
				return this;
			}
			return new AccessibleObject(accessible);
		}

		// Token: 0x06000849 RID: 2121 RVA: 0x0001732A File Offset: 0x0001552A
		MethodInfo IReflect.GetMethod(string name, BindingFlags bindingAttr, Binder binder, Type[] types, ParameterModifier[] modifiers)
		{
			return typeof(IAccessible).GetMethod(name, bindingAttr, binder, types, modifiers);
		}

		// Token: 0x0600084A RID: 2122 RVA: 0x00017342 File Offset: 0x00015542
		MethodInfo IReflect.GetMethod(string name, BindingFlags bindingAttr)
		{
			return typeof(IAccessible).GetMethod(name, bindingAttr);
		}

		// Token: 0x0600084B RID: 2123 RVA: 0x00017355 File Offset: 0x00015555
		MethodInfo[] IReflect.GetMethods(BindingFlags bindingAttr)
		{
			return typeof(IAccessible).GetMethods(bindingAttr);
		}

		// Token: 0x0600084C RID: 2124 RVA: 0x00017367 File Offset: 0x00015567
		FieldInfo IReflect.GetField(string name, BindingFlags bindingAttr)
		{
			return typeof(IAccessible).GetField(name, bindingAttr);
		}

		// Token: 0x0600084D RID: 2125 RVA: 0x0001737A File Offset: 0x0001557A
		FieldInfo[] IReflect.GetFields(BindingFlags bindingAttr)
		{
			return typeof(IAccessible).GetFields(bindingAttr);
		}

		// Token: 0x0600084E RID: 2126 RVA: 0x0001738C File Offset: 0x0001558C
		PropertyInfo IReflect.GetProperty(string name, BindingFlags bindingAttr)
		{
			return typeof(IAccessible).GetProperty(name, bindingAttr);
		}

		// Token: 0x0600084F RID: 2127 RVA: 0x0001739F File Offset: 0x0001559F
		PropertyInfo IReflect.GetProperty(string name, BindingFlags bindingAttr, Binder binder, Type returnType, Type[] types, ParameterModifier[] modifiers)
		{
			return typeof(IAccessible).GetProperty(name, bindingAttr, binder, returnType, types, modifiers);
		}

		// Token: 0x06000850 RID: 2128 RVA: 0x000173B9 File Offset: 0x000155B9
		PropertyInfo[] IReflect.GetProperties(BindingFlags bindingAttr)
		{
			return typeof(IAccessible).GetProperties(bindingAttr);
		}

		// Token: 0x06000851 RID: 2129 RVA: 0x000173CB File Offset: 0x000155CB
		MemberInfo[] IReflect.GetMember(string name, BindingFlags bindingAttr)
		{
			return typeof(IAccessible).GetMember(name, bindingAttr);
		}

		// Token: 0x06000852 RID: 2130 RVA: 0x000173DE File Offset: 0x000155DE
		MemberInfo[] IReflect.GetMembers(BindingFlags bindingAttr)
		{
			return typeof(IAccessible).GetMembers(bindingAttr);
		}

		// Token: 0x06000853 RID: 2131 RVA: 0x000173F0 File Offset: 0x000155F0
		object IReflect.InvokeMember(string name, BindingFlags invokeAttr, Binder binder, object target, object[] args, ParameterModifier[] modifiers, CultureInfo culture, string[] namedParameters)
		{
			if (args.Length == 0)
			{
				MemberInfo[] member = typeof(IAccessible).GetMember(name);
				if (member != null && member.Length != 0 && member[0] is PropertyInfo)
				{
					MethodInfo getMethod = ((PropertyInfo)member[0]).GetGetMethod();
					if (getMethod != null && getMethod.GetParameters().Length != 0)
					{
						args = new object[getMethod.GetParameters().Length];
						for (int i = 0; i < args.Length; i++)
						{
							args[i] = 0;
						}
					}
				}
			}
			return typeof(IAccessible).InvokeMember(name, invokeAttr, binder, target, args, modifiers, culture, namedParameters);
		}

		// Token: 0x17000225 RID: 549
		// (get) Token: 0x06000854 RID: 2132 RVA: 0x00017488 File Offset: 0x00015688
		Type IReflect.UnderlyingSystemType
		{
			get
			{
				return typeof(IAccessible);
			}
		}

		// Token: 0x06000855 RID: 2133 RVA: 0x00017494 File Offset: 0x00015694
		UnsafeNativeMethods.IRawElementProviderSimple UnsafeNativeMethods.IRawElementProviderHwndOverride.GetOverrideProviderForHwnd(IntPtr hwnd)
		{
			return this.GetOverrideProviderForHwnd(hwnd);
		}

		// Token: 0x17000226 RID: 550
		// (get) Token: 0x06000856 RID: 2134 RVA: 0x0001647C File Offset: 0x0001467C
		bool UnsafeNativeMethods.IRangeValueProvider.IsReadOnly
		{
			get
			{
				return this.IsReadOnly;
			}
		}

		// Token: 0x17000227 RID: 551
		// (get) Token: 0x06000857 RID: 2135 RVA: 0x0001749D File Offset: 0x0001569D
		double UnsafeNativeMethods.IRangeValueProvider.LargeChange
		{
			get
			{
				return this.LargeChange;
			}
		}

		// Token: 0x17000228 RID: 552
		// (get) Token: 0x06000858 RID: 2136 RVA: 0x000174A5 File Offset: 0x000156A5
		double UnsafeNativeMethods.IRangeValueProvider.Maximum
		{
			get
			{
				return this.Maximum;
			}
		}

		// Token: 0x17000229 RID: 553
		// (get) Token: 0x06000859 RID: 2137 RVA: 0x000174AD File Offset: 0x000156AD
		double UnsafeNativeMethods.IRangeValueProvider.Minimum
		{
			get
			{
				return this.Minimum;
			}
		}

		// Token: 0x1700022A RID: 554
		// (get) Token: 0x0600085A RID: 2138 RVA: 0x000174B5 File Offset: 0x000156B5
		double UnsafeNativeMethods.IRangeValueProvider.SmallChange
		{
			get
			{
				return this.SmallChange;
			}
		}

		// Token: 0x1700022B RID: 555
		// (get) Token: 0x0600085B RID: 2139 RVA: 0x000174BD File Offset: 0x000156BD
		double UnsafeNativeMethods.IRangeValueProvider.Value
		{
			get
			{
				return this.RangeValue;
			}
		}

		// Token: 0x0600085C RID: 2140 RVA: 0x000174C5 File Offset: 0x000156C5
		void UnsafeNativeMethods.IRangeValueProvider.SetValue(double value)
		{
			this.SetValue(value);
		}

		// Token: 0x0600085D RID: 2141 RVA: 0x000174D0 File Offset: 0x000156D0
		object[] UnsafeNativeMethods.ISelectionProvider.GetSelection()
		{
			return this.GetSelection();
		}

		// Token: 0x1700022C RID: 556
		// (get) Token: 0x0600085E RID: 2142 RVA: 0x000174E5 File Offset: 0x000156E5
		bool UnsafeNativeMethods.ISelectionProvider.CanSelectMultiple
		{
			get
			{
				return this.CanSelectMultiple;
			}
		}

		// Token: 0x1700022D RID: 557
		// (get) Token: 0x0600085F RID: 2143 RVA: 0x000174ED File Offset: 0x000156ED
		bool UnsafeNativeMethods.ISelectionProvider.IsSelectionRequired
		{
			get
			{
				return this.IsSelectionRequired;
			}
		}

		// Token: 0x06000860 RID: 2144 RVA: 0x000174F5 File Offset: 0x000156F5
		void UnsafeNativeMethods.ISelectionItemProvider.Select()
		{
			this.SelectItem();
		}

		// Token: 0x06000861 RID: 2145 RVA: 0x000174FD File Offset: 0x000156FD
		void UnsafeNativeMethods.ISelectionItemProvider.AddToSelection()
		{
			this.AddToSelection();
		}

		// Token: 0x06000862 RID: 2146 RVA: 0x00017505 File Offset: 0x00015705
		void UnsafeNativeMethods.ISelectionItemProvider.RemoveFromSelection()
		{
			this.RemoveFromSelection();
		}

		// Token: 0x1700022E RID: 558
		// (get) Token: 0x06000863 RID: 2147 RVA: 0x0001750D File Offset: 0x0001570D
		bool UnsafeNativeMethods.ISelectionItemProvider.IsSelected
		{
			get
			{
				return this.IsItemSelected;
			}
		}

		// Token: 0x1700022F RID: 559
		// (get) Token: 0x06000864 RID: 2148 RVA: 0x00017515 File Offset: 0x00015715
		UnsafeNativeMethods.IRawElementProviderSimple UnsafeNativeMethods.ISelectionItemProvider.SelectionContainer
		{
			get
			{
				return this.ItemSelectionContainer;
			}
		}

		// Token: 0x06000865 RID: 2149 RVA: 0x00017520 File Offset: 0x00015720
		public bool RaiseAutomationNotification(AutomationNotificationKind notificationKind, AutomationNotificationProcessing notificationProcessing, string notificationText)
		{
			if (!AccessibilityImprovements.Level3 || !AccessibleObject.notificationEventAvailable || LocalAppContextSwitches.NoClientNotifications)
			{
				return false;
			}
			int num = 1;
			try
			{
				num = UnsafeNativeMethods.UiaRaiseNotificationEvent(this, notificationKind, notificationProcessing, notificationText, string.Empty);
			}
			catch (EntryPointNotFoundException)
			{
				AccessibleObject.notificationEventAvailable = false;
			}
			return num == 0;
		}

		// Token: 0x06000866 RID: 2150 RVA: 0x00017574 File Offset: 0x00015774
		public virtual bool RaiseLiveRegionChanged()
		{
			throw new NotSupportedException(SR.GetString("AccessibleObjectLiveRegionNotSupported"));
		}

		// Token: 0x06000867 RID: 2151 RVA: 0x00017588 File Offset: 0x00015788
		internal bool RaiseAutomationEvent(int eventId)
		{
			if (UnsafeNativeMethods.UiaClientsAreListening() && !LocalAppContextSwitches.NoClientNotifications)
			{
				int num = UnsafeNativeMethods.UiaRaiseAutomationEvent(this, eventId);
				return num == 0;
			}
			return false;
		}

		// Token: 0x06000868 RID: 2152 RVA: 0x000175B4 File Offset: 0x000157B4
		internal bool RaiseAutomationPropertyChangedEvent(int propertyId, object oldValue, object newValue)
		{
			if (UnsafeNativeMethods.UiaClientsAreListening() && !LocalAppContextSwitches.NoClientNotifications)
			{
				int num = UnsafeNativeMethods.UiaRaiseAutomationPropertyChangedEvent(this, propertyId, oldValue, newValue);
				return num == 0;
			}
			return false;
		}

		// Token: 0x06000869 RID: 2153 RVA: 0x000175E0 File Offset: 0x000157E0
		internal bool RaiseStructureChangedEvent(UnsafeNativeMethods.StructureChangeType structureChangeType, int[] runtimeId)
		{
			if (UnsafeNativeMethods.UiaClientsAreListening() && !LocalAppContextSwitches.NoClientNotifications)
			{
				int num = UnsafeNativeMethods.UiaRaiseStructureChangedEvent(this, structureChangeType, runtimeId, (runtimeId == null) ? 0 : runtimeId.Length);
				return num == 0;
			}
			return false;
		}

		// Token: 0x0600086A RID: 2154 RVA: 0x00017613 File Offset: 0x00015813
		void UnsafeNativeMethods.IScrollItemProvider.ScrollIntoView()
		{
			this.ScrollIntoView();
		}

		// Token: 0x0600086B RID: 2155 RVA: 0x000072B6 File Offset: 0x000054B6
		internal virtual void ScrollIntoView()
		{
		}

		// Token: 0x04000540 RID: 1344
		private IAccessible systemIAccessible;

		// Token: 0x04000541 RID: 1345
		private UnsafeNativeMethods.IEnumVariant systemIEnumVariant;

		// Token: 0x04000542 RID: 1346
		private UnsafeNativeMethods.IEnumVariant enumVariant;

		// Token: 0x04000543 RID: 1347
		private UnsafeNativeMethods.IOleWindow systemIOleWindow;

		// Token: 0x04000544 RID: 1348
		private bool systemWrapper;

		// Token: 0x04000545 RID: 1349
		private int accObjId = -4;

		// Token: 0x04000546 RID: 1350
		private static bool notificationEventAvailable = true;

		// Token: 0x04000547 RID: 1351
		internal const int RuntimeIDFirstItem = 42;

		// Token: 0x020005FF RID: 1535
		private class EnumVariantObject : UnsafeNativeMethods.IEnumVariant
		{
			// Token: 0x060061C3 RID: 25027 RVA: 0x001693A9 File Offset: 0x001675A9
			public EnumVariantObject(AccessibleObject owner)
			{
				this.owner = owner;
			}

			// Token: 0x060061C4 RID: 25028 RVA: 0x001693B8 File Offset: 0x001675B8
			public EnumVariantObject(AccessibleObject owner, int currentChild)
			{
				this.owner = owner;
				this.currentChild = currentChild;
			}

			// Token: 0x060061C5 RID: 25029 RVA: 0x001693CE File Offset: 0x001675CE
			void UnsafeNativeMethods.IEnumVariant.Clone(UnsafeNativeMethods.IEnumVariant[] v)
			{
				v[0] = new AccessibleObject.EnumVariantObject(this.owner, this.currentChild);
			}

			// Token: 0x060061C6 RID: 25030 RVA: 0x001693E4 File Offset: 0x001675E4
			void UnsafeNativeMethods.IEnumVariant.Reset()
			{
				this.currentChild = 0;
				IntSecurity.UnmanagedCode.Assert();
				try
				{
					if (this.owner.systemIEnumVariant != null)
					{
						this.owner.systemIEnumVariant.Reset();
					}
				}
				finally
				{
					CodeAccessPermission.RevertAssert();
				}
			}

			// Token: 0x060061C7 RID: 25031 RVA: 0x00169438 File Offset: 0x00167638
			void UnsafeNativeMethods.IEnumVariant.Skip(int n)
			{
				this.currentChild += n;
				IntSecurity.UnmanagedCode.Assert();
				try
				{
					if (this.owner.systemIEnumVariant != null)
					{
						this.owner.systemIEnumVariant.Skip(n);
					}
				}
				finally
				{
					CodeAccessPermission.RevertAssert();
				}
			}

			// Token: 0x060061C8 RID: 25032 RVA: 0x00169494 File Offset: 0x00167694
			int UnsafeNativeMethods.IEnumVariant.Next(int n, IntPtr rgvar, int[] ns)
			{
				if (this.owner.IsClientObject)
				{
					int childCount;
					int[] sysChildOrder;
					if ((childCount = this.owner.GetChildCount()) >= 0)
					{
						this.NextFromChildCollection(n, rgvar, ns, childCount);
					}
					else if (this.owner.systemIEnumVariant == null)
					{
						this.NextEmpty(n, rgvar, ns);
					}
					else if ((sysChildOrder = this.owner.GetSysChildOrder()) != null)
					{
						this.NextFromSystemReordered(n, rgvar, ns, sysChildOrder);
					}
					else
					{
						this.NextFromSystem(n, rgvar, ns);
					}
				}
				else
				{
					this.NextFromSystem(n, rgvar, ns);
				}
				if (ns[0] != n)
				{
					return 1;
				}
				return 0;
			}

			// Token: 0x060061C9 RID: 25033 RVA: 0x0016951C File Offset: 0x0016771C
			private void NextFromSystem(int n, IntPtr rgvar, int[] ns)
			{
				IntSecurity.UnmanagedCode.Assert();
				try
				{
					this.owner.systemIEnumVariant.Next(n, rgvar, ns);
				}
				finally
				{
					CodeAccessPermission.RevertAssert();
				}
				this.currentChild += ns[0];
			}

			// Token: 0x060061CA RID: 25034 RVA: 0x00169570 File Offset: 0x00167770
			private void NextFromSystemReordered(int n, IntPtr rgvar, int[] ns, int[] newOrder)
			{
				int num = 0;
				while (num < n && this.currentChild < newOrder.Length && AccessibleObject.EnumVariantObject.GotoItem(this.owner.systemIEnumVariant, newOrder[this.currentChild], AccessibleObject.EnumVariantObject.GetAddressOfVariantAtIndex(rgvar, num)))
				{
					this.currentChild++;
					num++;
				}
				ns[0] = num;
			}

			// Token: 0x060061CB RID: 25035 RVA: 0x001695CC File Offset: 0x001677CC
			private void NextFromChildCollection(int n, IntPtr rgvar, int[] ns, int childCount)
			{
				int num = 0;
				while (num < n && this.currentChild < childCount)
				{
					this.currentChild++;
					Marshal.GetNativeVariantForObject(this.currentChild, AccessibleObject.EnumVariantObject.GetAddressOfVariantAtIndex(rgvar, num));
					num++;
				}
				ns[0] = num;
			}

			// Token: 0x060061CC RID: 25036 RVA: 0x00169618 File Offset: 0x00167818
			private void NextEmpty(int n, IntPtr rgvar, int[] ns)
			{
				ns[0] = 0;
			}

			// Token: 0x060061CD RID: 25037 RVA: 0x00169620 File Offset: 0x00167820
			private static bool GotoItem(UnsafeNativeMethods.IEnumVariant iev, int index, IntPtr variantPtr)
			{
				int[] array = new int[1];
				IntSecurity.UnmanagedCode.Assert();
				try
				{
					iev.Reset();
					iev.Skip(index);
					iev.Next(1, variantPtr, array);
				}
				finally
				{
					CodeAccessPermission.RevertAssert();
				}
				return array[0] == 1;
			}

			// Token: 0x060061CE RID: 25038 RVA: 0x00169674 File Offset: 0x00167874
			private static IntPtr GetAddressOfVariantAtIndex(IntPtr variantArrayPtr, int index)
			{
				int num = 8 + IntPtr.Size * 2;
				return (IntPtr)((long)variantArrayPtr + (long)index * (long)num);
			}

			// Token: 0x040038A7 RID: 14503
			private int currentChild;

			// Token: 0x040038A8 RID: 14504
			private AccessibleObject owner;
		}
	}
}
