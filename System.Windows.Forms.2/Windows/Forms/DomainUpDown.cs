using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Windows.Forms.Layout;

namespace System.Windows.Forms
{
	// Token: 0x02000233 RID: 563
	[ComVisible(true)]
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[DefaultProperty("Items")]
	[DefaultEvent("SelectedItemChanged")]
	[DefaultBindingProperty("SelectedItem")]
	[SRDescription("DescriptionDomainUpDown")]
	public class DomainUpDown : UpDownBase
	{
		// Token: 0x0600247A RID: 9338 RVA: 0x000AC459 File Offset: 0x000AA659
		public DomainUpDown()
		{
			base.SetState2(2048, true);
			this.Text = string.Empty;
		}

		// Token: 0x17000850 RID: 2128
		// (get) Token: 0x0600247B RID: 9339 RVA: 0x000AC495 File Offset: 0x000AA695
		[SRCategory("CatData")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[SRDescription("DomainUpDownItemsDescr")]
		[Localizable(true)]
		[Editor("System.Windows.Forms.Design.StringCollectionEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		public DomainUpDown.DomainUpDownItemCollection Items
		{
			get
			{
				if (this.domainItems == null)
				{
					this.domainItems = new DomainUpDown.DomainUpDownItemCollection(this);
				}
				return this.domainItems;
			}
		}

		// Token: 0x17000851 RID: 2129
		// (get) Token: 0x0600247C RID: 9340 RVA: 0x00013656 File Offset: 0x00011856
		// (set) Token: 0x0600247D RID: 9341 RVA: 0x0001365E File Offset: 0x0001185E
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public new Padding Padding
		{
			get
			{
				return base.Padding;
			}
			set
			{
				base.Padding = value;
			}
		}

		// Token: 0x14000197 RID: 407
		// (add) Token: 0x0600247E RID: 9342 RVA: 0x00013667 File Offset: 0x00011867
		// (remove) Token: 0x0600247F RID: 9343 RVA: 0x00013670 File Offset: 0x00011870
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler PaddingChanged
		{
			add
			{
				base.PaddingChanged += value;
			}
			remove
			{
				base.PaddingChanged -= value;
			}
		}

		// Token: 0x17000852 RID: 2130
		// (get) Token: 0x06002480 RID: 9344 RVA: 0x000AC4B1 File Offset: 0x000AA6B1
		// (set) Token: 0x06002481 RID: 9345 RVA: 0x000AC4C4 File Offset: 0x000AA6C4
		[Browsable(false)]
		[DefaultValue(-1)]
		[SRCategory("CatAppearance")]
		[SRDescription("DomainUpDownSelectedIndexDescr")]
		public int SelectedIndex
		{
			get
			{
				if (base.UserEdit)
				{
					return -1;
				}
				return this.domainIndex;
			}
			set
			{
				if (value < -1 || value >= this.Items.Count)
				{
					throw new ArgumentOutOfRangeException("SelectedIndex", SR.GetString("InvalidArgument", new object[]
					{
						"SelectedIndex",
						value.ToString(CultureInfo.CurrentCulture)
					}));
				}
				if (value != this.SelectedIndex)
				{
					this.SelectIndex(value);
				}
			}
		}

		// Token: 0x17000853 RID: 2131
		// (get) Token: 0x06002482 RID: 9346 RVA: 0x000AC528 File Offset: 0x000AA728
		// (set) Token: 0x06002483 RID: 9347 RVA: 0x000AC550 File Offset: 0x000AA750
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[SRDescription("DomainUpDownSelectedItemDescr")]
		public object SelectedItem
		{
			get
			{
				int selectedIndex = this.SelectedIndex;
				if (selectedIndex != -1)
				{
					return this.Items[selectedIndex];
				}
				return null;
			}
			set
			{
				if (value == null)
				{
					this.SelectedIndex = -1;
					return;
				}
				for (int i = 0; i < this.Items.Count; i++)
				{
					if (value != null && value.Equals(this.Items[i]))
					{
						this.SelectedIndex = i;
						return;
					}
				}
			}
		}

		// Token: 0x17000854 RID: 2132
		// (get) Token: 0x06002484 RID: 9348 RVA: 0x000AC59D File Offset: 0x000AA79D
		// (set) Token: 0x06002485 RID: 9349 RVA: 0x000AC5A5 File Offset: 0x000AA7A5
		[SRCategory("CatBehavior")]
		[DefaultValue(false)]
		[SRDescription("DomainUpDownSortedDescr")]
		public bool Sorted
		{
			get
			{
				return this.sorted;
			}
			set
			{
				this.sorted = value;
				if (this.sorted)
				{
					this.SortDomainItems();
				}
			}
		}

		// Token: 0x17000855 RID: 2133
		// (get) Token: 0x06002486 RID: 9350 RVA: 0x000AC5BC File Offset: 0x000AA7BC
		// (set) Token: 0x06002487 RID: 9351 RVA: 0x000AC5C4 File Offset: 0x000AA7C4
		[SRCategory("CatBehavior")]
		[Localizable(true)]
		[DefaultValue(false)]
		[SRDescription("DomainUpDownWrapDescr")]
		public bool Wrap
		{
			get
			{
				return this.wrap;
			}
			set
			{
				this.wrap = value;
			}
		}

		// Token: 0x14000198 RID: 408
		// (add) Token: 0x06002488 RID: 9352 RVA: 0x000AC5CD File Offset: 0x000AA7CD
		// (remove) Token: 0x06002489 RID: 9353 RVA: 0x000AC5E6 File Offset: 0x000AA7E6
		[SRCategory("CatBehavior")]
		[SRDescription("DomainUpDownOnSelectedItemChangedDescr")]
		public event EventHandler SelectedItemChanged
		{
			add
			{
				this.onSelectedItemChanged = (EventHandler)Delegate.Combine(this.onSelectedItemChanged, value);
			}
			remove
			{
				this.onSelectedItemChanged = (EventHandler)Delegate.Remove(this.onSelectedItemChanged, value);
			}
		}

		// Token: 0x0600248A RID: 9354 RVA: 0x000AC5FF File Offset: 0x000AA7FF
		protected override AccessibleObject CreateAccessibilityInstance()
		{
			return new DomainUpDown.DomainUpDownAccessibleObject(this);
		}

		// Token: 0x0600248B RID: 9355 RVA: 0x000AC608 File Offset: 0x000AA808
		public override void DownButton()
		{
			if (this.domainItems == null)
			{
				return;
			}
			if (this.domainItems.Count <= 0)
			{
				return;
			}
			int num = -1;
			if (base.UserEdit)
			{
				num = this.MatchIndex(this.Text, false, this.domainIndex);
			}
			if (num != -1)
			{
				if (LocalAppContextSwitches.UseLegacyDomainUpDownControlScrolling)
				{
					this.SelectIndex(num);
					return;
				}
				this.domainIndex = num;
			}
			if (this.domainIndex < this.domainItems.Count - 1)
			{
				this.SelectIndex(this.domainIndex + 1);
				return;
			}
			if (this.Wrap)
			{
				this.SelectIndex(0);
			}
		}

		// Token: 0x0600248C RID: 9356 RVA: 0x000AC69A File Offset: 0x000AA89A
		internal int MatchIndex(string text, bool complete)
		{
			return this.MatchIndex(text, complete, this.domainIndex);
		}

		// Token: 0x0600248D RID: 9357 RVA: 0x000AC6AC File Offset: 0x000AA8AC
		internal int MatchIndex(string text, bool complete, int startPosition)
		{
			if (this.domainItems == null)
			{
				return -1;
			}
			if (text.Length < 1)
			{
				return -1;
			}
			if (this.domainItems.Count <= 0)
			{
				return -1;
			}
			if (startPosition < 0)
			{
				startPosition = this.domainItems.Count - 1;
			}
			if (startPosition >= this.domainItems.Count)
			{
				startPosition = 0;
			}
			int num = startPosition;
			int result = -1;
			if (!complete)
			{
				text = text.ToUpper(CultureInfo.InvariantCulture);
			}
			bool flag;
			do
			{
				if (complete)
				{
					flag = this.Items[num].ToString().Equals(text);
				}
				else
				{
					flag = this.Items[num].ToString().ToUpper(CultureInfo.InvariantCulture).StartsWith(text);
				}
				if (flag)
				{
					result = num;
				}
				num++;
				if (num >= this.domainItems.Count)
				{
					num = 0;
				}
			}
			while (!flag && num != startPosition);
			return result;
		}

		// Token: 0x0600248E RID: 9358 RVA: 0x000AC778 File Offset: 0x000AA978
		protected override void OnChanged(object source, EventArgs e)
		{
			this.OnSelectedItemChanged(source, e);
		}

		// Token: 0x0600248F RID: 9359 RVA: 0x000AC784 File Offset: 0x000AA984
		protected override void OnTextBoxKeyPress(object source, KeyPressEventArgs e)
		{
			if (base.ReadOnly)
			{
				char[] array = new char[]
				{
					e.KeyChar
				};
				UnicodeCategory unicodeCategory = char.GetUnicodeCategory(array[0]);
				if (unicodeCategory == UnicodeCategory.LetterNumber || unicodeCategory == UnicodeCategory.LowercaseLetter || unicodeCategory == UnicodeCategory.DecimalDigitNumber || unicodeCategory == UnicodeCategory.MathSymbol || unicodeCategory == UnicodeCategory.OtherLetter || unicodeCategory == UnicodeCategory.OtherNumber || unicodeCategory == UnicodeCategory.UppercaseLetter)
				{
					int num = this.MatchIndex(new string(array), false, this.domainIndex + 1);
					if (num != -1)
					{
						this.SelectIndex(num);
					}
					e.Handled = true;
				}
			}
			base.OnTextBoxKeyPress(source, e);
		}

		// Token: 0x06002490 RID: 9360 RVA: 0x000AC800 File Offset: 0x000AAA00
		protected void OnSelectedItemChanged(object source, EventArgs e)
		{
			if (this.onSelectedItemChanged != null)
			{
				this.onSelectedItemChanged(this, e);
			}
		}

		// Token: 0x06002491 RID: 9361 RVA: 0x000AC818 File Offset: 0x000AAA18
		private void SelectIndex(int index)
		{
			if (this.domainItems == null || index < -1 || index >= this.domainItems.Count)
			{
				index = -1;
				return;
			}
			this.domainIndex = index;
			if (this.domainIndex >= 0)
			{
				this.stringValue = this.domainItems[this.domainIndex].ToString();
				base.UserEdit = false;
				this.UpdateEditText();
				return;
			}
			base.UserEdit = true;
		}

		// Token: 0x06002492 RID: 9362 RVA: 0x000AC884 File Offset: 0x000AAA84
		private void SortDomainItems()
		{
			if (this.inSort)
			{
				return;
			}
			this.inSort = true;
			try
			{
				if (this.sorted)
				{
					if (this.domainItems != null)
					{
						ArrayList.Adapter(this.domainItems).Sort(new DomainUpDown.DomainUpDownItemCompare());
						if (!base.UserEdit)
						{
							int num = this.MatchIndex(this.stringValue, true);
							if (num != -1)
							{
								this.SelectIndex(num);
							}
						}
					}
				}
			}
			finally
			{
				this.inSort = false;
			}
		}

		// Token: 0x06002493 RID: 9363 RVA: 0x000AC904 File Offset: 0x000AAB04
		public override string ToString()
		{
			string text = base.ToString();
			if (this.Items != null)
			{
				text = text + ", Items.Count: " + this.Items.Count.ToString(CultureInfo.CurrentCulture);
				text = text + ", SelectedIndex: " + this.SelectedIndex.ToString(CultureInfo.CurrentCulture);
			}
			return text;
		}

		// Token: 0x06002494 RID: 9364 RVA: 0x000AC964 File Offset: 0x000AAB64
		public override void UpButton()
		{
			if (this.domainItems == null)
			{
				return;
			}
			if (this.domainItems.Count <= 0)
			{
				return;
			}
			if (this.domainIndex == -1 && LocalAppContextSwitches.UseLegacyDomainUpDownControlScrolling)
			{
				return;
			}
			int num = -1;
			if (base.UserEdit)
			{
				num = this.MatchIndex(this.Text, false, this.domainIndex);
			}
			if (num != -1)
			{
				if (LocalAppContextSwitches.UseLegacyDomainUpDownControlScrolling)
				{
					this.SelectIndex(num);
					return;
				}
				this.domainIndex = num;
			}
			if (this.domainIndex > 0)
			{
				this.SelectIndex(this.domainIndex - 1);
				return;
			}
			if (this.Wrap)
			{
				this.SelectIndex(this.domainItems.Count - 1);
			}
		}

		// Token: 0x06002495 RID: 9365 RVA: 0x000ACA07 File Offset: 0x000AAC07
		protected override void UpdateEditText()
		{
			base.UserEdit = false;
			base.ChangingText = true;
			this.Text = this.stringValue;
		}

		// Token: 0x06002496 RID: 9366 RVA: 0x000ACA24 File Offset: 0x000AAC24
		internal override Size GetPreferredSizeCore(Size proposedConstraints)
		{
			int preferredHeight = base.PreferredHeight;
			int width = LayoutUtils.OldGetLargestStringSizeInCollection(this.Font, this.Items).Width;
			width = base.SizeFromClientSize(width, preferredHeight).Width + this.upDownButtons.Width;
			return new Size(width, preferredHeight) + this.Padding.Size;
		}

		// Token: 0x04000F0C RID: 3852
		private static readonly string DefaultValue = "";

		// Token: 0x04000F0D RID: 3853
		private static readonly bool DefaultWrap = false;

		// Token: 0x04000F0E RID: 3854
		private DomainUpDown.DomainUpDownItemCollection domainItems;

		// Token: 0x04000F0F RID: 3855
		private string stringValue = DomainUpDown.DefaultValue;

		// Token: 0x04000F10 RID: 3856
		private int domainIndex = -1;

		// Token: 0x04000F11 RID: 3857
		private bool sorted;

		// Token: 0x04000F12 RID: 3858
		private bool wrap = DomainUpDown.DefaultWrap;

		// Token: 0x04000F13 RID: 3859
		private EventHandler onSelectedItemChanged;

		// Token: 0x04000F14 RID: 3860
		private bool inSort;

		// Token: 0x02000684 RID: 1668
		public class DomainUpDownItemCollection : ArrayList
		{
			// Token: 0x06006723 RID: 26403 RVA: 0x001823A2 File Offset: 0x001805A2
			internal DomainUpDownItemCollection(DomainUpDown owner)
			{
				this.owner = owner;
			}

			// Token: 0x17001673 RID: 5747
			[Browsable(false)]
			[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
			public override object this[int index]
			{
				get
				{
					return base[index];
				}
				set
				{
					base[index] = value;
					if (this.owner.SelectedIndex == index)
					{
						this.owner.SelectIndex(index);
					}
					if (this.owner.Sorted)
					{
						this.owner.SortDomainItems();
					}
				}
			}

			// Token: 0x06006726 RID: 26406 RVA: 0x001823F8 File Offset: 0x001805F8
			public override int Add(object item)
			{
				int result = base.Add(item);
				if (this.owner.Sorted)
				{
					this.owner.SortDomainItems();
				}
				return result;
			}

			// Token: 0x06006727 RID: 26407 RVA: 0x00182428 File Offset: 0x00180628
			public override void Remove(object item)
			{
				int num = this.IndexOf(item);
				if (num == -1)
				{
					throw new ArgumentOutOfRangeException("item", SR.GetString("InvalidArgument", new object[]
					{
						"item",
						item.ToString()
					}));
				}
				this.RemoveAt(num);
			}

			// Token: 0x06006728 RID: 26408 RVA: 0x00182474 File Offset: 0x00180674
			public override void RemoveAt(int item)
			{
				base.RemoveAt(item);
				if (item < this.owner.domainIndex)
				{
					this.owner.SelectIndex(this.owner.domainIndex - 1);
					return;
				}
				if (item == this.owner.domainIndex)
				{
					this.owner.SelectIndex(-1);
				}
			}

			// Token: 0x06006729 RID: 26409 RVA: 0x001824C9 File Offset: 0x001806C9
			public override void Insert(int index, object item)
			{
				base.Insert(index, item);
				if (this.owner.Sorted)
				{
					this.owner.SortDomainItems();
				}
			}

			// Token: 0x04003A8C RID: 14988
			private DomainUpDown owner;
		}

		// Token: 0x02000685 RID: 1669
		private sealed class DomainUpDownItemCompare : IComparer
		{
			// Token: 0x0600672A RID: 26410 RVA: 0x001824EB File Offset: 0x001806EB
			public int Compare(object p, object q)
			{
				if (p == q)
				{
					return 0;
				}
				if (p == null || q == null)
				{
					return 0;
				}
				return string.Compare(p.ToString(), q.ToString(), false, CultureInfo.CurrentCulture);
			}
		}

		// Token: 0x02000686 RID: 1670
		[ComVisible(true)]
		public class DomainUpDownAccessibleObject : Control.ControlAccessibleObject
		{
			// Token: 0x0600672C RID: 26412 RVA: 0x0009B963 File Offset: 0x00099B63
			public DomainUpDownAccessibleObject(Control owner) : base(owner)
			{
			}

			// Token: 0x17001674 RID: 5748
			// (get) Token: 0x0600672D RID: 26413 RVA: 0x00182514 File Offset: 0x00180714
			// (set) Token: 0x0600672E RID: 26414 RVA: 0x0001106B File Offset: 0x0000F26B
			public override string Name
			{
				get
				{
					if (base.IsOwnerControlDestroyed())
					{
						return string.Empty;
					}
					string name = base.Name;
					return ((DomainUpDown)base.Owner).GetAccessibleName(name);
				}
				set
				{
					base.Name = value;
				}
			}

			// Token: 0x17001675 RID: 5749
			// (get) Token: 0x0600672F RID: 26415 RVA: 0x00182547 File Offset: 0x00180747
			private DomainUpDown.DomainItemListAccessibleObject ItemList
			{
				get
				{
					if (this.itemList == null)
					{
						this.itemList = new DomainUpDown.DomainItemListAccessibleObject(this);
					}
					return this.itemList;
				}
			}

			// Token: 0x17001676 RID: 5750
			// (get) Token: 0x06006730 RID: 26416 RVA: 0x00182564 File Offset: 0x00180764
			public override AccessibleRole Role
			{
				get
				{
					if (base.IsOwnerControlDestroyed())
					{
						return AccessibleRole.SpinButton;
					}
					AccessibleRole accessibleRole = base.Owner.AccessibleRole;
					if (accessibleRole != AccessibleRole.Default)
					{
						return accessibleRole;
					}
					if (AccessibilityImprovements.Level1)
					{
						return AccessibleRole.SpinButton;
					}
					return AccessibleRole.ComboBox;
				}
			}

			// Token: 0x06006731 RID: 26417 RVA: 0x0018259C File Offset: 0x0018079C
			public override AccessibleObject GetChild(int index)
			{
				if (base.IsOwnerControlDestroyed())
				{
					return null;
				}
				switch (index)
				{
				case 0:
					return ((UpDownBase)base.Owner).TextBox.AccessibilityObject.Parent;
				case 1:
					return ((UpDownBase)base.Owner).UpDownButtonsInternal.AccessibilityObject.Parent;
				case 2:
					return this.ItemList;
				default:
					return null;
				}
			}

			// Token: 0x06006732 RID: 26418 RVA: 0x00023D73 File Offset: 0x00021F73
			public override int GetChildCount()
			{
				return 3;
			}

			// Token: 0x04003A8D RID: 14989
			private DomainUpDown.DomainItemListAccessibleObject itemList;
		}

		// Token: 0x02000687 RID: 1671
		internal class DomainItemListAccessibleObject : AccessibleObject
		{
			// Token: 0x06006733 RID: 26419 RVA: 0x00182605 File Offset: 0x00180805
			public DomainItemListAccessibleObject(DomainUpDown.DomainUpDownAccessibleObject parent)
			{
				this.parent = parent;
			}

			// Token: 0x17001677 RID: 5751
			// (get) Token: 0x06006734 RID: 26420 RVA: 0x00182614 File Offset: 0x00180814
			// (set) Token: 0x06006735 RID: 26421 RVA: 0x0017012F File Offset: 0x0016E32F
			public override string Name
			{
				get
				{
					string name = base.Name;
					if (name == null || name.Length == 0)
					{
						return "Items";
					}
					return name;
				}
				set
				{
					base.Name = value;
				}
			}

			// Token: 0x17001678 RID: 5752
			// (get) Token: 0x06006736 RID: 26422 RVA: 0x0018263A File Offset: 0x0018083A
			public override AccessibleObject Parent
			{
				[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
				get
				{
					return this.parent;
				}
			}

			// Token: 0x17001679 RID: 5753
			// (get) Token: 0x06006737 RID: 26423 RVA: 0x00177E5D File Offset: 0x0017605D
			public override AccessibleRole Role
			{
				get
				{
					return AccessibleRole.List;
				}
			}

			// Token: 0x1700167A RID: 5754
			// (get) Token: 0x06006738 RID: 26424 RVA: 0x00182642 File Offset: 0x00180842
			public override AccessibleStates State
			{
				get
				{
					return AccessibleStates.Invisible | AccessibleStates.Offscreen;
				}
			}

			// Token: 0x06006739 RID: 26425 RVA: 0x0018264C File Offset: 0x0018084C
			public override AccessibleObject GetChild(int index)
			{
				if (this.parent.IsOwnerControlDestroyed())
				{
					return null;
				}
				if (index >= 0 && index < this.GetChildCount())
				{
					return new DomainUpDown.DomainItemAccessibleObject(((DomainUpDown)this.parent.Owner).Items[index].ToString(), this);
				}
				return null;
			}

			// Token: 0x0600673A RID: 26426 RVA: 0x0018269D File Offset: 0x0018089D
			public override int GetChildCount()
			{
				if (this.parent.IsOwnerControlDestroyed())
				{
					return 0;
				}
				return ((DomainUpDown)this.parent.Owner).Items.Count;
			}

			// Token: 0x04003A8E RID: 14990
			private DomainUpDown.DomainUpDownAccessibleObject parent;
		}

		// Token: 0x02000688 RID: 1672
		[ComVisible(true)]
		public class DomainItemAccessibleObject : AccessibleObject
		{
			// Token: 0x0600673B RID: 26427 RVA: 0x001826C8 File Offset: 0x001808C8
			public DomainItemAccessibleObject(string name, AccessibleObject parent)
			{
				this.name = name;
				this.parent = (DomainUpDown.DomainItemListAccessibleObject)parent;
			}

			// Token: 0x1700167B RID: 5755
			// (get) Token: 0x0600673C RID: 26428 RVA: 0x001826E3 File Offset: 0x001808E3
			// (set) Token: 0x0600673D RID: 26429 RVA: 0x001826EB File Offset: 0x001808EB
			public override string Name
			{
				get
				{
					return this.name;
				}
				set
				{
					this.name = value;
				}
			}

			// Token: 0x1700167C RID: 5756
			// (get) Token: 0x0600673E RID: 26430 RVA: 0x001826F4 File Offset: 0x001808F4
			public override AccessibleObject Parent
			{
				[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
				get
				{
					return this.parent;
				}
			}

			// Token: 0x1700167D RID: 5757
			// (get) Token: 0x0600673F RID: 26431 RVA: 0x0001612D File Offset: 0x0001432D
			public override AccessibleRole Role
			{
				get
				{
					return AccessibleRole.ListItem;
				}
			}

			// Token: 0x1700167E RID: 5758
			// (get) Token: 0x06006740 RID: 26432 RVA: 0x001826FC File Offset: 0x001808FC
			public override AccessibleStates State
			{
				get
				{
					return AccessibleStates.Selectable;
				}
			}

			// Token: 0x1700167F RID: 5759
			// (get) Token: 0x06006741 RID: 26433 RVA: 0x001826E3 File Offset: 0x001808E3
			public override string Value
			{
				[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
				get
				{
					return this.name;
				}
			}

			// Token: 0x04003A8F RID: 14991
			private string name;

			// Token: 0x04003A90 RID: 14992
			private DomainUpDown.DomainItemListAccessibleObject parent;
		}
	}
}
