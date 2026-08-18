using System;
using System.Collections;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace System.Windows.Forms
{
	// Token: 0x020002DD RID: 733
	[TypeConverter(typeof(ListViewItemConverter))]
	[ToolboxItem(false)]
	[DesignTimeVisible(false)]
	[DefaultProperty("Text")]
	[Serializable]
	public class ListViewItem : ICloneable, ISerializable
	{
		// Token: 0x06002E3C RID: 11836 RVA: 0x000D1C0C File Offset: 0x000CFE0C
		public ListViewItem()
		{
			this.StateSelected = false;
			this.UseItemStyleForSubItems = true;
			this.SavedStateImageIndex = -1;
		}

		// Token: 0x06002E3D RID: 11837 RVA: 0x000D1C5A File Offset: 0x000CFE5A
		protected ListViewItem(SerializationInfo info, StreamingContext context) : this()
		{
			this.Deserialize(info, context);
		}

		// Token: 0x06002E3E RID: 11838 RVA: 0x000D1C6A File Offset: 0x000CFE6A
		public ListViewItem(string text) : this(text, -1)
		{
		}

		// Token: 0x06002E3F RID: 11839 RVA: 0x000D1C74 File Offset: 0x000CFE74
		public ListViewItem(string text, int imageIndex) : this()
		{
			this.ImageIndexer.Index = imageIndex;
			this.Text = text;
		}

		// Token: 0x06002E40 RID: 11840 RVA: 0x000D1C8F File Offset: 0x000CFE8F
		public ListViewItem(string[] items) : this(items, -1)
		{
		}

		// Token: 0x06002E41 RID: 11841 RVA: 0x000D1C9C File Offset: 0x000CFE9C
		public ListViewItem(string[] items, int imageIndex) : this()
		{
			this.ImageIndexer.Index = imageIndex;
			if (items != null && items.Length != 0)
			{
				this.subItems = new ListViewItem.ListViewSubItem[items.Length];
				for (int i = 0; i < items.Length; i++)
				{
					this.subItems[i] = new ListViewItem.ListViewSubItem(this, items[i]);
				}
				this.SubItemCount = items.Length;
			}
		}

		// Token: 0x06002E42 RID: 11842 RVA: 0x000D1CF8 File Offset: 0x000CFEF8
		public ListViewItem(string[] items, int imageIndex, Color foreColor, Color backColor, Font font) : this(items, imageIndex)
		{
			this.ForeColor = foreColor;
			this.BackColor = backColor;
			this.Font = font;
		}

		// Token: 0x06002E43 RID: 11843 RVA: 0x000D1D1C File Offset: 0x000CFF1C
		public ListViewItem(ListViewItem.ListViewSubItem[] subItems, int imageIndex) : this()
		{
			this.ImageIndexer.Index = imageIndex;
			this.subItems = subItems;
			this.SubItemCount = this.subItems.Length;
			for (int i = 0; i < subItems.Length; i++)
			{
				subItems[i].owner = this;
			}
		}

		// Token: 0x06002E44 RID: 11844 RVA: 0x000D1D67 File Offset: 0x000CFF67
		public ListViewItem(ListViewGroup group) : this()
		{
			this.Group = group;
		}

		// Token: 0x06002E45 RID: 11845 RVA: 0x000D1D76 File Offset: 0x000CFF76
		public ListViewItem(string text, ListViewGroup group) : this(text)
		{
			this.Group = group;
		}

		// Token: 0x06002E46 RID: 11846 RVA: 0x000D1D86 File Offset: 0x000CFF86
		public ListViewItem(string text, int imageIndex, ListViewGroup group) : this(text, imageIndex)
		{
			this.Group = group;
		}

		// Token: 0x06002E47 RID: 11847 RVA: 0x000D1D97 File Offset: 0x000CFF97
		public ListViewItem(string[] items, ListViewGroup group) : this(items)
		{
			this.Group = group;
		}

		// Token: 0x06002E48 RID: 11848 RVA: 0x000D1DA7 File Offset: 0x000CFFA7
		public ListViewItem(string[] items, int imageIndex, ListViewGroup group) : this(items, imageIndex)
		{
			this.Group = group;
		}

		// Token: 0x06002E49 RID: 11849 RVA: 0x000D1DB8 File Offset: 0x000CFFB8
		public ListViewItem(string[] items, int imageIndex, Color foreColor, Color backColor, Font font, ListViewGroup group) : this(items, imageIndex, foreColor, backColor, font)
		{
			this.Group = group;
		}

		// Token: 0x06002E4A RID: 11850 RVA: 0x000D1DCF File Offset: 0x000CFFCF
		public ListViewItem(ListViewItem.ListViewSubItem[] subItems, int imageIndex, ListViewGroup group) : this(subItems, imageIndex)
		{
			this.Group = group;
		}

		// Token: 0x06002E4B RID: 11851 RVA: 0x000D1DE0 File Offset: 0x000CFFE0
		public ListViewItem(string text, string imageKey) : this()
		{
			this.ImageIndexer.Key = imageKey;
			this.Text = text;
		}

		// Token: 0x06002E4C RID: 11852 RVA: 0x000D1DFC File Offset: 0x000CFFFC
		public ListViewItem(string[] items, string imageKey) : this()
		{
			this.ImageIndexer.Key = imageKey;
			if (items != null && items.Length != 0)
			{
				this.subItems = new ListViewItem.ListViewSubItem[items.Length];
				for (int i = 0; i < items.Length; i++)
				{
					this.subItems[i] = new ListViewItem.ListViewSubItem(this, items[i]);
				}
				this.SubItemCount = items.Length;
			}
		}

		// Token: 0x06002E4D RID: 11853 RVA: 0x000D1E58 File Offset: 0x000D0058
		public ListViewItem(string[] items, string imageKey, Color foreColor, Color backColor, Font font) : this(items, imageKey)
		{
			this.ForeColor = foreColor;
			this.BackColor = backColor;
			this.Font = font;
		}

		// Token: 0x06002E4E RID: 11854 RVA: 0x000D1E7C File Offset: 0x000D007C
		public ListViewItem(ListViewItem.ListViewSubItem[] subItems, string imageKey) : this()
		{
			this.ImageIndexer.Key = imageKey;
			this.subItems = subItems;
			this.SubItemCount = this.subItems.Length;
			for (int i = 0; i < subItems.Length; i++)
			{
				subItems[i].owner = this;
			}
		}

		// Token: 0x06002E4F RID: 11855 RVA: 0x000D1EC7 File Offset: 0x000D00C7
		public ListViewItem(string text, string imageKey, ListViewGroup group) : this(text, imageKey)
		{
			this.Group = group;
		}

		// Token: 0x06002E50 RID: 11856 RVA: 0x000D1ED8 File Offset: 0x000D00D8
		public ListViewItem(string[] items, string imageKey, ListViewGroup group) : this(items, imageKey)
		{
			this.Group = group;
		}

		// Token: 0x06002E51 RID: 11857 RVA: 0x000D1EE9 File Offset: 0x000D00E9
		public ListViewItem(string[] items, string imageKey, Color foreColor, Color backColor, Font font, ListViewGroup group) : this(items, imageKey, foreColor, backColor, font)
		{
			this.Group = group;
		}

		// Token: 0x06002E52 RID: 11858 RVA: 0x000D1F00 File Offset: 0x000D0100
		public ListViewItem(ListViewItem.ListViewSubItem[] subItems, string imageKey, ListViewGroup group) : this(subItems, imageKey)
		{
			this.Group = group;
		}

		// Token: 0x17000ACD RID: 2765
		// (get) Token: 0x06002E53 RID: 11859 RVA: 0x000D1F11 File Offset: 0x000D0111
		// (set) Token: 0x06002E54 RID: 11860 RVA: 0x000D1F42 File Offset: 0x000D0142
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[SRCategory("CatAppearance")]
		public Color BackColor
		{
			get
			{
				if (this.SubItemCount != 0)
				{
					return this.subItems[0].BackColor;
				}
				if (this.listView != null)
				{
					return this.listView.BackColor;
				}
				return SystemColors.Window;
			}
			set
			{
				this.SubItems[0].BackColor = value;
			}
		}

		// Token: 0x17000ACE RID: 2766
		// (get) Token: 0x06002E55 RID: 11861 RVA: 0x000D1F58 File Offset: 0x000D0158
		[Browsable(false)]
		public Rectangle Bounds
		{
			get
			{
				if (this.listView != null)
				{
					return this.listView.GetItemRect(this.Index);
				}
				return default(Rectangle);
			}
		}

		// Token: 0x17000ACF RID: 2767
		// (get) Token: 0x06002E56 RID: 11862 RVA: 0x000D1F88 File Offset: 0x000D0188
		// (set) Token: 0x06002E57 RID: 11863 RVA: 0x000D1F94 File Offset: 0x000D0194
		[DefaultValue(false)]
		[RefreshProperties(RefreshProperties.Repaint)]
		[SRCategory("CatAppearance")]
		public bool Checked
		{
			get
			{
				return this.StateImageIndex > 0;
			}
			set
			{
				if (this.Checked != value)
				{
					if (this.listView != null && this.listView.IsHandleCreated)
					{
						this.StateImageIndex = (value ? 1 : 0);
						if (this.listView != null && !this.listView.UseCompatibleStateImageBehavior && !this.listView.CheckBoxes)
						{
							this.listView.UpdateSavedCheckedItems(this, value);
							return;
						}
					}
					else
					{
						this.SavedStateImageIndex = (value ? 1 : 0);
					}
				}
			}
		}

		// Token: 0x17000AD0 RID: 2768
		// (get) Token: 0x06002E58 RID: 11864 RVA: 0x000D2009 File Offset: 0x000D0209
		// (set) Token: 0x06002E59 RID: 11865 RVA: 0x000D2037 File Offset: 0x000D0237
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public bool Focused
		{
			get
			{
				return this.listView != null && this.listView.IsHandleCreated && this.listView.GetItemState(this.Index, 1) != 0;
			}
			set
			{
				if (this.listView != null && this.listView.IsHandleCreated)
				{
					this.listView.SetItemState(this.Index, value ? 1 : 0, 1);
				}
			}
		}

		// Token: 0x17000AD1 RID: 2769
		// (get) Token: 0x06002E5A RID: 11866 RVA: 0x000D2067 File Offset: 0x000D0267
		// (set) Token: 0x06002E5B RID: 11867 RVA: 0x000D2098 File Offset: 0x000D0298
		[Localizable(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[SRCategory("CatAppearance")]
		public Font Font
		{
			get
			{
				if (this.SubItemCount != 0)
				{
					return this.subItems[0].Font;
				}
				if (this.listView != null)
				{
					return this.listView.Font;
				}
				return Control.DefaultFont;
			}
			set
			{
				this.SubItems[0].Font = value;
			}
		}

		// Token: 0x17000AD2 RID: 2770
		// (get) Token: 0x06002E5C RID: 11868 RVA: 0x000D20AC File Offset: 0x000D02AC
		// (set) Token: 0x06002E5D RID: 11869 RVA: 0x000D20DD File Offset: 0x000D02DD
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[SRCategory("CatAppearance")]
		public Color ForeColor
		{
			get
			{
				if (this.SubItemCount != 0)
				{
					return this.subItems[0].ForeColor;
				}
				if (this.listView != null)
				{
					return this.listView.ForeColor;
				}
				return SystemColors.WindowText;
			}
			set
			{
				this.SubItems[0].ForeColor = value;
			}
		}

		// Token: 0x17000AD3 RID: 2771
		// (get) Token: 0x06002E5E RID: 11870 RVA: 0x000D20F1 File Offset: 0x000D02F1
		// (set) Token: 0x06002E5F RID: 11871 RVA: 0x000D20F9 File Offset: 0x000D02F9
		[DefaultValue(null)]
		[Localizable(true)]
		[SRCategory("CatBehavior")]
		public ListViewGroup Group
		{
			get
			{
				return this.group;
			}
			set
			{
				if (this.group != value)
				{
					if (value != null)
					{
						value.Items.Add(this);
					}
					else
					{
						this.group.Items.Remove(this);
					}
				}
				this.groupName = null;
			}
		}

		// Token: 0x17000AD4 RID: 2772
		// (get) Token: 0x06002E60 RID: 11872 RVA: 0x000D2130 File Offset: 0x000D0330
		// (set) Token: 0x06002E61 RID: 11873 RVA: 0x000D2190 File Offset: 0x000D0390
		[DefaultValue(-1)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Editor("System.Windows.Forms.Design.ImageIndexEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[Localizable(true)]
		[RefreshProperties(RefreshProperties.Repaint)]
		[SRCategory("CatBehavior")]
		[SRDescription("ListViewItemImageIndexDescr")]
		[TypeConverter(typeof(NoneExcludedImageIndexConverter))]
		public int ImageIndex
		{
			get
			{
				if (this.ImageIndexer.Index != -1 && this.ImageList != null && this.ImageIndexer.Index >= this.ImageList.Images.Count)
				{
					return this.ImageList.Images.Count - 1;
				}
				return this.ImageIndexer.Index;
			}
			set
			{
				if (value < -1)
				{
					throw new ArgumentOutOfRangeException("ImageIndex", SR.GetString("InvalidLowBoundArgumentEx", new object[]
					{
						"ImageIndex",
						value.ToString(CultureInfo.CurrentCulture),
						-1.ToString(CultureInfo.CurrentCulture)
					}));
				}
				this.ImageIndexer.Index = value;
				if (this.listView != null && this.listView.IsHandleCreated)
				{
					this.listView.SetItemImage(this.Index, this.ImageIndexer.ActualIndex);
				}
			}
		}

		// Token: 0x17000AD5 RID: 2773
		// (get) Token: 0x06002E62 RID: 11874 RVA: 0x000D2221 File Offset: 0x000D0421
		internal ListViewItem.ListViewItemImageIndexer ImageIndexer
		{
			get
			{
				if (this.imageIndexer == null)
				{
					this.imageIndexer = new ListViewItem.ListViewItemImageIndexer(this);
				}
				return this.imageIndexer;
			}
		}

		// Token: 0x17000AD6 RID: 2774
		// (get) Token: 0x06002E63 RID: 11875 RVA: 0x000D223D File Offset: 0x000D043D
		// (set) Token: 0x06002E64 RID: 11876 RVA: 0x000D224A File Offset: 0x000D044A
		[DefaultValue("")]
		[TypeConverter(typeof(ImageKeyConverter))]
		[Editor("System.Windows.Forms.Design.ImageIndexEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[RefreshProperties(RefreshProperties.Repaint)]
		[SRCategory("CatBehavior")]
		[Localizable(true)]
		public string ImageKey
		{
			get
			{
				return this.ImageIndexer.Key;
			}
			set
			{
				this.ImageIndexer.Key = value;
				if (this.listView != null && this.listView.IsHandleCreated)
				{
					this.listView.SetItemImage(this.Index, this.ImageIndexer.ActualIndex);
				}
			}
		}

		// Token: 0x17000AD7 RID: 2775
		// (get) Token: 0x06002E65 RID: 11877 RVA: 0x000D228C File Offset: 0x000D048C
		[Browsable(false)]
		public ImageList ImageList
		{
			get
			{
				if (this.listView != null)
				{
					switch (this.listView.View)
					{
					case View.LargeIcon:
					case View.Tile:
						return this.listView.LargeImageList;
					case View.Details:
					case View.SmallIcon:
					case View.List:
						return this.listView.SmallImageList;
					}
				}
				return null;
			}
		}

		// Token: 0x17000AD8 RID: 2776
		// (get) Token: 0x06002E66 RID: 11878 RVA: 0x000D22E2 File Offset: 0x000D04E2
		// (set) Token: 0x06002E67 RID: 11879 RVA: 0x000D22EC File Offset: 0x000D04EC
		[DefaultValue(0)]
		[SRDescription("ListViewItemIndentCountDescr")]
		[SRCategory("CatDisplay")]
		public int IndentCount
		{
			get
			{
				return this.indentCount;
			}
			set
			{
				if (value == this.indentCount)
				{
					return;
				}
				if (value < 0)
				{
					throw new ArgumentOutOfRangeException("IndentCount", SR.GetString("ListViewIndentCountCantBeNegative"));
				}
				this.indentCount = value;
				if (this.listView != null && this.listView.IsHandleCreated)
				{
					this.listView.SetItemIndentCount(this.Index, this.indentCount);
				}
			}
		}

		// Token: 0x17000AD9 RID: 2777
		// (get) Token: 0x06002E68 RID: 11880 RVA: 0x000D234F File Offset: 0x000D054F
		[Browsable(false)]
		public int Index
		{
			get
			{
				if (this.listView != null)
				{
					if (!this.listView.VirtualMode)
					{
						this.lastIndex = this.listView.GetDisplayIndex(this, this.lastIndex);
					}
					return this.lastIndex;
				}
				return -1;
			}
		}

		// Token: 0x17000ADA RID: 2778
		// (get) Token: 0x06002E69 RID: 11881 RVA: 0x000D2386 File Offset: 0x000D0586
		[Browsable(false)]
		public ListView ListView
		{
			get
			{
				return this.listView;
			}
		}

		// Token: 0x17000ADB RID: 2779
		// (get) Token: 0x06002E6A RID: 11882 RVA: 0x000D238E File Offset: 0x000D058E
		// (set) Token: 0x06002E6B RID: 11883 RVA: 0x000D23AB File Offset: 0x000D05AB
		[Localizable(true)]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public string Name
		{
			get
			{
				if (this.SubItemCount == 0)
				{
					return string.Empty;
				}
				return this.subItems[0].Name;
			}
			set
			{
				this.SubItems[0].Name = value;
			}
		}

		// Token: 0x17000ADC RID: 2780
		// (get) Token: 0x06002E6C RID: 11884 RVA: 0x000D23BF File Offset: 0x000D05BF
		// (set) Token: 0x06002E6D RID: 11885 RVA: 0x000D23F4 File Offset: 0x000D05F4
		[SRCategory("CatDisplay")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public Point Position
		{
			get
			{
				if (this.listView != null && this.listView.IsHandleCreated)
				{
					this.position = this.listView.GetItemPosition(this.Index);
				}
				return this.position;
			}
			set
			{
				if (value.Equals(this.position))
				{
					return;
				}
				this.position = value;
				if (this.listView != null && this.listView.IsHandleCreated && !this.listView.VirtualMode)
				{
					this.listView.SetItemPosition(this.Index, this.position.X, this.position.Y);
				}
			}
		}

		// Token: 0x17000ADD RID: 2781
		// (get) Token: 0x06002E6E RID: 11886 RVA: 0x000D246C File Offset: 0x000D066C
		internal int RawStateImageIndex
		{
			get
			{
				return this.SavedStateImageIndex + 1 << 12;
			}
		}

		// Token: 0x17000ADE RID: 2782
		// (get) Token: 0x06002E6F RID: 11887 RVA: 0x000D2479 File Offset: 0x000D0679
		// (set) Token: 0x06002E70 RID: 11888 RVA: 0x000D248D File Offset: 0x000D068D
		private int SavedStateImageIndex
		{
			get
			{
				return this.state[ListViewItem.SavedStateImageIndexSection] - 1;
			}
			set
			{
				this.state[ListViewItem.StateImageMaskSet] = ((value == -1) ? 0 : 1);
				this.state[ListViewItem.SavedStateImageIndexSection] = value + 1;
			}
		}

		// Token: 0x17000ADF RID: 2783
		// (get) Token: 0x06002E71 RID: 11889 RVA: 0x000D24BA File Offset: 0x000D06BA
		// (set) Token: 0x06002E72 RID: 11890 RVA: 0x000D24F0 File Offset: 0x000D06F0
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public bool Selected
		{
			get
			{
				if (this.listView != null && this.listView.IsHandleCreated)
				{
					return this.listView.GetItemState(this.Index, 2) != 0;
				}
				return this.StateSelected;
			}
			set
			{
				if (this.listView != null && this.listView.IsHandleCreated)
				{
					this.listView.SetItemState(this.Index, value ? 2 : 0, 2);
					this.listView.SetSelectionMark(this.Index);
					return;
				}
				this.StateSelected = value;
				if (this.listView != null && this.listView.IsHandleCreated)
				{
					this.listView.CacheSelectedStateForItem(this, value);
				}
			}
		}

		// Token: 0x17000AE0 RID: 2784
		// (get) Token: 0x06002E73 RID: 11891 RVA: 0x000D2568 File Offset: 0x000D0768
		// (set) Token: 0x06002E74 RID: 11892 RVA: 0x000D25B0 File Offset: 0x000D07B0
		[Localizable(true)]
		[TypeConverter(typeof(NoneExcludedImageIndexConverter))]
		[DefaultValue(-1)]
		[SRDescription("ListViewItemStateImageIndexDescr")]
		[SRCategory("CatBehavior")]
		[RefreshProperties(RefreshProperties.Repaint)]
		[Editor("System.Windows.Forms.Design.ImageIndexEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[RelatedImageList("ListView.StateImageList")]
		public int StateImageIndex
		{
			get
			{
				if (this.listView != null && this.listView.IsHandleCreated)
				{
					int itemState = this.listView.GetItemState(this.Index, 61440);
					return (itemState >> 12) - 1;
				}
				return this.SavedStateImageIndex;
			}
			set
			{
				if (value < -1 || value > 14)
				{
					throw new ArgumentOutOfRangeException("StateImageIndex", SR.GetString("InvalidArgument", new object[]
					{
						"StateImageIndex",
						value.ToString(CultureInfo.CurrentCulture)
					}));
				}
				if (this.listView != null && this.listView.IsHandleCreated)
				{
					this.state[ListViewItem.StateImageMaskSet] = ((value == -1) ? 0 : 1);
					int num = value + 1 << 12;
					this.listView.SetItemState(this.Index, num, 61440);
				}
				this.SavedStateImageIndex = value;
			}
		}

		// Token: 0x17000AE1 RID: 2785
		// (get) Token: 0x06002E75 RID: 11893 RVA: 0x000D264A File Offset: 0x000D084A
		internal bool StateImageSet
		{
			get
			{
				return this.state[ListViewItem.StateImageMaskSet] != 0;
			}
		}

		// Token: 0x17000AE2 RID: 2786
		// (get) Token: 0x06002E76 RID: 11894 RVA: 0x000D265F File Offset: 0x000D085F
		// (set) Token: 0x06002E77 RID: 11895 RVA: 0x000D2674 File Offset: 0x000D0874
		internal bool StateSelected
		{
			get
			{
				return this.state[ListViewItem.StateSelectedSection] == 1;
			}
			set
			{
				this.state[ListViewItem.StateSelectedSection] = (value ? 1 : 0);
			}
		}

		// Token: 0x17000AE3 RID: 2787
		// (get) Token: 0x06002E78 RID: 11896 RVA: 0x000D268D File Offset: 0x000D088D
		// (set) Token: 0x06002E79 RID: 11897 RVA: 0x000D269F File Offset: 0x000D089F
		private int SubItemCount
		{
			get
			{
				return this.state[ListViewItem.SubItemCountSection];
			}
			set
			{
				this.state[ListViewItem.SubItemCountSection] = value;
			}
		}

		// Token: 0x17000AE4 RID: 2788
		// (get) Token: 0x06002E7A RID: 11898 RVA: 0x000D26B4 File Offset: 0x000D08B4
		[SRCategory("CatData")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[SRDescription("ListViewItemSubItemsDescr")]
		[Editor("System.Windows.Forms.Design.ListViewSubItemCollectionEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		public ListViewItem.ListViewSubItemCollection SubItems
		{
			get
			{
				if (this.SubItemCount == 0)
				{
					this.subItems = new ListViewItem.ListViewSubItem[1];
					this.subItems[0] = new ListViewItem.ListViewSubItem(this, string.Empty);
					this.SubItemCount = 1;
				}
				if (this.listViewSubItemCollection == null)
				{
					this.listViewSubItemCollection = new ListViewItem.ListViewSubItemCollection(this);
				}
				return this.listViewSubItemCollection;
			}
		}

		// Token: 0x17000AE5 RID: 2789
		// (get) Token: 0x06002E7B RID: 11899 RVA: 0x000D2709 File Offset: 0x000D0909
		// (set) Token: 0x06002E7C RID: 11900 RVA: 0x000D2711 File Offset: 0x000D0911
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

		// Token: 0x17000AE6 RID: 2790
		// (get) Token: 0x06002E7D RID: 11901 RVA: 0x000D271A File Offset: 0x000D091A
		// (set) Token: 0x06002E7E RID: 11902 RVA: 0x000D2737 File Offset: 0x000D0937
		[Localizable(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[SRCategory("CatAppearance")]
		public string Text
		{
			get
			{
				if (this.SubItemCount == 0)
				{
					return string.Empty;
				}
				return this.subItems[0].Text;
			}
			set
			{
				this.SubItems[0].Text = value;
			}
		}

		// Token: 0x17000AE7 RID: 2791
		// (get) Token: 0x06002E7F RID: 11903 RVA: 0x000D274B File Offset: 0x000D094B
		// (set) Token: 0x06002E80 RID: 11904 RVA: 0x000D2754 File Offset: 0x000D0954
		[SRCategory("CatAppearance")]
		[DefaultValue("")]
		public string ToolTipText
		{
			get
			{
				return this.toolTipText;
			}
			set
			{
				if (value == null)
				{
					value = string.Empty;
				}
				if (WindowsFormsUtils.SafeCompareStrings(this.toolTipText, value, false))
				{
					return;
				}
				this.toolTipText = value;
				if (this.listView != null && this.listView.IsHandleCreated)
				{
					this.listView.ListViewItemToolTipChanged(this);
				}
			}
		}

		// Token: 0x17000AE8 RID: 2792
		// (get) Token: 0x06002E81 RID: 11905 RVA: 0x000D27A3 File Offset: 0x000D09A3
		// (set) Token: 0x06002E82 RID: 11906 RVA: 0x000D27B8 File Offset: 0x000D09B8
		[DefaultValue(true)]
		[SRCategory("CatAppearance")]
		public bool UseItemStyleForSubItems
		{
			get
			{
				return this.state[ListViewItem.StateWholeRowOneStyleSection] == 1;
			}
			set
			{
				this.state[ListViewItem.StateWholeRowOneStyleSection] = (value ? 1 : 0);
			}
		}

		// Token: 0x06002E83 RID: 11907 RVA: 0x000D27D4 File Offset: 0x000D09D4
		public void BeginEdit()
		{
			if (this.Index >= 0)
			{
				ListView listView = this.ListView;
				if (!listView.LabelEdit)
				{
					throw new InvalidOperationException(SR.GetString("ListViewBeginEditFailed"));
				}
				if (!listView.Focused)
				{
					listView.FocusInternal();
				}
				UnsafeNativeMethods.SendMessage(new HandleRef(listView, listView.Handle), NativeMethods.LVM_EDITLABEL, this.Index, 0);
			}
		}

		// Token: 0x06002E84 RID: 11908 RVA: 0x000D2838 File Offset: 0x000D0A38
		public virtual object Clone()
		{
			ListViewItem.ListViewSubItem[] array = new ListViewItem.ListViewSubItem[this.SubItems.Count];
			for (int i = 0; i < this.SubItems.Count; i++)
			{
				ListViewItem.ListViewSubItem listViewSubItem = this.SubItems[i];
				array[i] = new ListViewItem.ListViewSubItem(null, listViewSubItem.Text, listViewSubItem.ForeColor, listViewSubItem.BackColor, listViewSubItem.Font);
				array[i].Tag = listViewSubItem.Tag;
			}
			Type type = base.GetType();
			ListViewItem listViewItem;
			if (type == typeof(ListViewItem))
			{
				listViewItem = new ListViewItem(array, this.ImageIndexer.Index);
			}
			else
			{
				listViewItem = (ListViewItem)Activator.CreateInstance(type);
			}
			listViewItem.subItems = array;
			listViewItem.ImageIndexer.Index = this.ImageIndexer.Index;
			listViewItem.SubItemCount = this.SubItemCount;
			listViewItem.Checked = this.Checked;
			listViewItem.UseItemStyleForSubItems = this.UseItemStyleForSubItems;
			listViewItem.Tag = this.Tag;
			if (!string.IsNullOrEmpty(this.ImageIndexer.Key))
			{
				listViewItem.ImageIndexer.Key = this.ImageIndexer.Key;
			}
			listViewItem.indentCount = this.indentCount;
			listViewItem.StateImageIndex = this.StateImageIndex;
			listViewItem.toolTipText = this.toolTipText;
			listViewItem.BackColor = this.BackColor;
			listViewItem.ForeColor = this.ForeColor;
			listViewItem.Font = this.Font;
			listViewItem.Text = this.Text;
			listViewItem.Group = this.Group;
			return listViewItem;
		}

		// Token: 0x06002E85 RID: 11909 RVA: 0x000D29BF File Offset: 0x000D0BBF
		public virtual void EnsureVisible()
		{
			if (this.listView != null && this.listView.IsHandleCreated)
			{
				this.listView.EnsureVisible(this.Index);
			}
		}

		// Token: 0x06002E86 RID: 11910 RVA: 0x000D29E8 File Offset: 0x000D0BE8
		public ListViewItem FindNearestItem(SearchDirectionHint searchDirection)
		{
			Rectangle bounds = this.Bounds;
			switch (searchDirection)
			{
			case SearchDirectionHint.Left:
				return this.ListView.FindNearestItem(searchDirection, bounds.Left, bounds.Top);
			case SearchDirectionHint.Up:
				return this.ListView.FindNearestItem(searchDirection, bounds.Left, bounds.Top);
			case SearchDirectionHint.Right:
				return this.ListView.FindNearestItem(searchDirection, bounds.Right, bounds.Top);
			case SearchDirectionHint.Down:
				return this.ListView.FindNearestItem(searchDirection, bounds.Left, bounds.Bottom);
			default:
				return null;
			}
		}

		// Token: 0x06002E87 RID: 11911 RVA: 0x000D2A84 File Offset: 0x000D0C84
		public Rectangle GetBounds(ItemBoundsPortion portion)
		{
			if (this.listView != null && this.listView.IsHandleCreated)
			{
				return this.listView.GetItemRect(this.Index, portion);
			}
			return default(Rectangle);
		}

		// Token: 0x06002E88 RID: 11912 RVA: 0x000D2AC4 File Offset: 0x000D0CC4
		public ListViewItem.ListViewSubItem GetSubItemAt(int x, int y)
		{
			if (this.listView == null || !this.listView.IsHandleCreated || this.listView.View != View.Details)
			{
				return null;
			}
			int num = -1;
			int num2 = -1;
			this.listView.GetSubItemAt(x, y, out num, out num2);
			if (num == this.Index && num2 != -1 && num2 < this.SubItems.Count)
			{
				return this.SubItems[num2];
			}
			return null;
		}

		// Token: 0x06002E89 RID: 11913 RVA: 0x000D2B34 File Offset: 0x000D0D34
		internal void Host(ListView parent, int ID, int index)
		{
			this.ID = ID;
			this.listView = parent;
			if (index != -1)
			{
				this.UpdateStateToListView(index);
			}
		}

		// Token: 0x06002E8A RID: 11914 RVA: 0x000D2B50 File Offset: 0x000D0D50
		internal void UpdateGroupFromName()
		{
			if (string.IsNullOrEmpty(this.groupName))
			{
				return;
			}
			ListViewGroup listViewGroup = this.listView.Groups[this.groupName];
			this.Group = listViewGroup;
			this.groupName = null;
		}

		// Token: 0x06002E8B RID: 11915 RVA: 0x000D2B90 File Offset: 0x000D0D90
		internal void UpdateStateToListView(int index)
		{
			NativeMethods.LVITEM lvitem = default(NativeMethods.LVITEM);
			this.UpdateStateToListView(index, ref lvitem, true);
		}

		// Token: 0x06002E8C RID: 11916 RVA: 0x000D2BB0 File Offset: 0x000D0DB0
		internal void UpdateStateToListView(int index, ref NativeMethods.LVITEM lvItem, bool updateOwner)
		{
			if (index == -1)
			{
				index = this.Index;
			}
			else
			{
				this.lastIndex = index;
			}
			int num = 0;
			int num2 = 0;
			if (this.StateSelected)
			{
				num |= 2;
				num2 |= 2;
			}
			if (this.SavedStateImageIndex > -1)
			{
				num |= this.SavedStateImageIndex + 1 << 12;
				num2 |= 61440;
			}
			lvItem.mask |= 8;
			lvItem.iItem = index;
			lvItem.stateMask |= num2;
			lvItem.state |= num;
			if (this.listView.GroupsEnabled)
			{
				lvItem.mask |= 256;
				lvItem.iGroupId = this.listView.GetNativeGroupId(this);
			}
			if (updateOwner)
			{
				UnsafeNativeMethods.SendMessage(new HandleRef(this.listView, this.listView.Handle), NativeMethods.LVM_SETITEM, 0, ref lvItem);
			}
		}

		// Token: 0x06002E8D RID: 11917 RVA: 0x000D2C84 File Offset: 0x000D0E84
		internal void UpdateStateFromListView(int displayIndex, bool checkSelection)
		{
			if (this.listView != null && this.listView.IsHandleCreated && displayIndex != -1)
			{
				NativeMethods.LVITEM lvitem = default(NativeMethods.LVITEM);
				lvitem.mask = 268;
				if (checkSelection)
				{
					lvitem.stateMask = 2;
				}
				lvitem.stateMask |= 61440;
				if (lvitem.stateMask == 0)
				{
					return;
				}
				lvitem.iItem = displayIndex;
				UnsafeNativeMethods.SendMessage(new HandleRef(this.listView, this.listView.Handle), NativeMethods.LVM_GETITEM, 0, ref lvitem);
				if (checkSelection)
				{
					this.StateSelected = ((lvitem.state & 2) != 0);
				}
				this.SavedStateImageIndex = ((lvitem.state & 61440) >> 12) - 1;
				this.group = null;
				foreach (object obj in this.ListView.Groups)
				{
					ListViewGroup listViewGroup = (ListViewGroup)obj;
					if (listViewGroup.ID == lvitem.iGroupId)
					{
						this.group = listViewGroup;
						break;
					}
				}
			}
		}

		// Token: 0x06002E8E RID: 11918 RVA: 0x000D2DAC File Offset: 0x000D0FAC
		internal void UnHost(bool checkSelection)
		{
			this.UnHost(this.Index, checkSelection);
		}

		// Token: 0x06002E8F RID: 11919 RVA: 0x000D2DBC File Offset: 0x000D0FBC
		internal void UnHost(int displayIndex, bool checkSelection)
		{
			this.UpdateStateFromListView(displayIndex, checkSelection);
			if (this.listView != null && (this.listView.Site == null || !this.listView.Site.DesignMode) && this.group != null)
			{
				this.group.Items.Remove(this);
			}
			this.ID = -1;
			this.listView = null;
		}

		// Token: 0x06002E90 RID: 11920 RVA: 0x000D2E1F File Offset: 0x000D101F
		public virtual void Remove()
		{
			if (this.listView != null)
			{
				this.listView.Items.Remove(this);
			}
		}

		// Token: 0x06002E91 RID: 11921 RVA: 0x000D2E3C File Offset: 0x000D103C
		protected virtual void Deserialize(SerializationInfo info, StreamingContext context)
		{
			bool flag = false;
			string text = null;
			int num = -1;
			foreach (SerializationEntry serializationEntry in info)
			{
				if (serializationEntry.Name == "Text")
				{
					this.Text = info.GetString(serializationEntry.Name);
				}
				else if (serializationEntry.Name == "ImageIndex")
				{
					num = info.GetInt32(serializationEntry.Name);
				}
				else if (serializationEntry.Name == "ImageKey")
				{
					text = info.GetString(serializationEntry.Name);
				}
				else if (serializationEntry.Name == "SubItemCount")
				{
					this.SubItemCount = info.GetInt32(serializationEntry.Name);
					if (this.SubItemCount > 0)
					{
						flag = true;
					}
				}
				else if (serializationEntry.Name == "BackColor")
				{
					this.BackColor = (Color)info.GetValue(serializationEntry.Name, typeof(Color));
				}
				else if (serializationEntry.Name == "Checked")
				{
					this.Checked = info.GetBoolean(serializationEntry.Name);
				}
				else if (serializationEntry.Name == "Font")
				{
					this.Font = (Font)info.GetValue(serializationEntry.Name, typeof(Font));
				}
				else if (serializationEntry.Name == "ForeColor")
				{
					this.ForeColor = (Color)info.GetValue(serializationEntry.Name, typeof(Color));
				}
				else if (serializationEntry.Name == "UseItemStyleForSubItems")
				{
					this.UseItemStyleForSubItems = info.GetBoolean(serializationEntry.Name);
				}
				else if (serializationEntry.Name == "Group")
				{
					ListViewGroup listViewGroup = (ListViewGroup)info.GetValue(serializationEntry.Name, typeof(ListViewGroup));
					this.groupName = listViewGroup.Name;
				}
			}
			if (text != null)
			{
				this.ImageKey = text;
			}
			else if (num != -1)
			{
				this.ImageIndex = num;
			}
			if (flag)
			{
				ListViewItem.ListViewSubItem[] array = new ListViewItem.ListViewSubItem[this.SubItemCount];
				for (int i = 1; i < this.SubItemCount; i++)
				{
					ListViewItem.ListViewSubItem listViewSubItem = (ListViewItem.ListViewSubItem)info.GetValue("SubItem" + i.ToString(CultureInfo.InvariantCulture), typeof(ListViewItem.ListViewSubItem));
					listViewSubItem.owner = this;
					array[i] = listViewSubItem;
				}
				array[0] = this.subItems[0];
				this.subItems = array;
			}
		}

		// Token: 0x06002E92 RID: 11922 RVA: 0x000D30E8 File Offset: 0x000D12E8
		[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		[SecurityPermission(SecurityAction.InheritanceDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		protected virtual void Serialize(SerializationInfo info, StreamingContext context)
		{
			info.AddValue("Text", this.Text);
			info.AddValue("ImageIndex", this.ImageIndexer.Index);
			if (!string.IsNullOrEmpty(this.ImageIndexer.Key))
			{
				info.AddValue("ImageKey", this.ImageIndexer.Key);
			}
			if (this.SubItemCount > 1)
			{
				info.AddValue("SubItemCount", this.SubItemCount);
				for (int i = 1; i < this.SubItemCount; i++)
				{
					info.AddValue("SubItem" + i.ToString(CultureInfo.InvariantCulture), this.subItems[i], typeof(ListViewItem.ListViewSubItem));
				}
			}
			info.AddValue("BackColor", this.BackColor);
			info.AddValue("Checked", this.Checked);
			info.AddValue("Font", this.Font);
			info.AddValue("ForeColor", this.ForeColor);
			info.AddValue("UseItemStyleForSubItems", this.UseItemStyleForSubItems);
			if (this.Group != null)
			{
				info.AddValue("Group", this.Group);
			}
		}

		// Token: 0x06002E93 RID: 11923 RVA: 0x000D3215 File Offset: 0x000D1415
		internal void SetItemIndex(ListView listView, int index)
		{
			this.listView = listView;
			this.lastIndex = index;
		}

		// Token: 0x06002E94 RID: 11924 RVA: 0x00011A20 File Offset: 0x0000FC20
		internal bool ShouldSerializeText()
		{
			return false;
		}

		// Token: 0x06002E95 RID: 11925 RVA: 0x000D3225 File Offset: 0x000D1425
		private bool ShouldSerializePosition()
		{
			return !this.position.Equals(new Point(-1, -1));
		}

		// Token: 0x06002E96 RID: 11926 RVA: 0x000D3247 File Offset: 0x000D1447
		public override string ToString()
		{
			return "ListViewItem: {" + this.Text + "}";
		}

		// Token: 0x06002E97 RID: 11927 RVA: 0x000D325E File Offset: 0x000D145E
		internal void InvalidateListView()
		{
			if (this.listView != null && this.listView.IsHandleCreated)
			{
				this.listView.Invalidate();
			}
		}

		// Token: 0x06002E98 RID: 11928 RVA: 0x000D3280 File Offset: 0x000D1480
		internal void UpdateSubItems(int index)
		{
			this.UpdateSubItems(index, this.SubItemCount);
		}

		// Token: 0x06002E99 RID: 11929 RVA: 0x000D3290 File Offset: 0x000D1490
		internal void UpdateSubItems(int index, int oldCount)
		{
			if (this.listView != null && this.listView.IsHandleCreated)
			{
				int subItemCount = this.SubItemCount;
				int index2 = this.Index;
				if (index != -1)
				{
					this.listView.SetItemText(index2, index, this.subItems[index].Text);
				}
				else
				{
					for (int i = 0; i < subItemCount; i++)
					{
						this.listView.SetItemText(index2, i, this.subItems[i].Text);
					}
				}
				for (int j = subItemCount; j < oldCount; j++)
				{
					this.listView.SetItemText(index2, j, string.Empty);
				}
			}
		}

		// Token: 0x06002E9A RID: 11930 RVA: 0x000D3324 File Offset: 0x000D1524
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			this.Serialize(info, context);
		}

		// Token: 0x0400132D RID: 4909
		private const int MAX_SUBITEMS = 4096;

		// Token: 0x0400132E RID: 4910
		private static readonly BitVector32.Section StateSelectedSection = BitVector32.CreateSection(1);

		// Token: 0x0400132F RID: 4911
		private static readonly BitVector32.Section StateImageMaskSet = BitVector32.CreateSection(1, ListViewItem.StateSelectedSection);

		// Token: 0x04001330 RID: 4912
		private static readonly BitVector32.Section StateWholeRowOneStyleSection = BitVector32.CreateSection(1, ListViewItem.StateImageMaskSet);

		// Token: 0x04001331 RID: 4913
		private static readonly BitVector32.Section SavedStateImageIndexSection = BitVector32.CreateSection(15, ListViewItem.StateWholeRowOneStyleSection);

		// Token: 0x04001332 RID: 4914
		private static readonly BitVector32.Section SubItemCountSection = BitVector32.CreateSection(4096, ListViewItem.SavedStateImageIndexSection);

		// Token: 0x04001333 RID: 4915
		private int indentCount;

		// Token: 0x04001334 RID: 4916
		private Point position = new Point(-1, -1);

		// Token: 0x04001335 RID: 4917
		internal ListView listView;

		// Token: 0x04001336 RID: 4918
		internal ListViewGroup group;

		// Token: 0x04001337 RID: 4919
		private string groupName;

		// Token: 0x04001338 RID: 4920
		private ListViewItem.ListViewSubItemCollection listViewSubItemCollection;

		// Token: 0x04001339 RID: 4921
		private ListViewItem.ListViewSubItem[] subItems;

		// Token: 0x0400133A RID: 4922
		private int lastIndex = -1;

		// Token: 0x0400133B RID: 4923
		internal int ID = -1;

		// Token: 0x0400133C RID: 4924
		private BitVector32 state;

		// Token: 0x0400133D RID: 4925
		private ListViewItem.ListViewItemImageIndexer imageIndexer;

		// Token: 0x0400133E RID: 4926
		private string toolTipText = string.Empty;

		// Token: 0x0400133F RID: 4927
		private object userData;

		// Token: 0x020006CF RID: 1743
		internal class ListViewItemImageIndexer : ImageList.Indexer
		{
			// Token: 0x06006A98 RID: 27288 RVA: 0x0018B451 File Offset: 0x00189651
			public ListViewItemImageIndexer(ListViewItem item)
			{
				this.owner = item;
			}

			// Token: 0x17001718 RID: 5912
			// (get) Token: 0x06006A99 RID: 27289 RVA: 0x0018B460 File Offset: 0x00189660
			// (set) Token: 0x06006A9A RID: 27290 RVA: 0x000072B6 File Offset: 0x000054B6
			public override ImageList ImageList
			{
				get
				{
					if (this.owner != null)
					{
						return this.owner.ImageList;
					}
					return null;
				}
				set
				{
				}
			}

			// Token: 0x04003B47 RID: 15175
			private ListViewItem owner;
		}

		// Token: 0x020006D0 RID: 1744
		[TypeConverter(typeof(ListViewSubItemConverter))]
		[ToolboxItem(false)]
		[DesignTimeVisible(false)]
		[DefaultProperty("Text")]
		[Serializable]
		public class ListViewSubItem
		{
			// Token: 0x06006A9B RID: 27291 RVA: 0x00002843 File Offset: 0x00000A43
			public ListViewSubItem()
			{
			}

			// Token: 0x06006A9C RID: 27292 RVA: 0x0018B477 File Offset: 0x00189677
			public ListViewSubItem(ListViewItem owner, string text)
			{
				this.owner = owner;
				this.text = text;
			}

			// Token: 0x06006A9D RID: 27293 RVA: 0x0018B490 File Offset: 0x00189690
			public ListViewSubItem(ListViewItem owner, string text, Color foreColor, Color backColor, Font font)
			{
				this.owner = owner;
				this.text = text;
				this.style = new ListViewItem.ListViewSubItem.SubItemStyle();
				this.style.foreColor = foreColor;
				this.style.backColor = backColor;
				this.style.font = font;
			}

			// Token: 0x17001719 RID: 5913
			// (get) Token: 0x06006A9E RID: 27294 RVA: 0x0018B4E4 File Offset: 0x001896E4
			// (set) Token: 0x06006A9F RID: 27295 RVA: 0x0018B548 File Offset: 0x00189748
			public Color BackColor
			{
				get
				{
					if (this.style != null && this.style.backColor != Color.Empty)
					{
						return this.style.backColor;
					}
					if (this.owner != null && this.owner.listView != null)
					{
						return this.owner.listView.BackColor;
					}
					return SystemColors.Window;
				}
				set
				{
					if (this.style == null)
					{
						this.style = new ListViewItem.ListViewSubItem.SubItemStyle();
					}
					if (this.style.backColor != value)
					{
						this.style.backColor = value;
						if (this.owner != null)
						{
							this.owner.InvalidateListView();
						}
					}
				}
			}

			// Token: 0x1700171A RID: 5914
			// (get) Token: 0x06006AA0 RID: 27296 RVA: 0x0018B59C File Offset: 0x0018979C
			[Browsable(false)]
			public Rectangle Bounds
			{
				get
				{
					if (this.owner != null && this.owner.listView != null && this.owner.listView.IsHandleCreated)
					{
						return this.owner.listView.GetSubItemRect(this.owner.Index, this.owner.SubItems.IndexOf(this));
					}
					return Rectangle.Empty;
				}
			}

			// Token: 0x1700171B RID: 5915
			// (get) Token: 0x06006AA1 RID: 27297 RVA: 0x0018B602 File Offset: 0x00189802
			internal bool CustomBackColor
			{
				get
				{
					return this.style != null && !this.style.backColor.IsEmpty;
				}
			}

			// Token: 0x1700171C RID: 5916
			// (get) Token: 0x06006AA2 RID: 27298 RVA: 0x0018B621 File Offset: 0x00189821
			internal bool CustomFont
			{
				get
				{
					return this.style != null && this.style.font != null;
				}
			}

			// Token: 0x1700171D RID: 5917
			// (get) Token: 0x06006AA3 RID: 27299 RVA: 0x0018B63B File Offset: 0x0018983B
			internal bool CustomForeColor
			{
				get
				{
					return this.style != null && !this.style.foreColor.IsEmpty;
				}
			}

			// Token: 0x1700171E RID: 5918
			// (get) Token: 0x06006AA4 RID: 27300 RVA: 0x0018B65A File Offset: 0x0018985A
			internal bool CustomStyle
			{
				get
				{
					return this.style != null;
				}
			}

			// Token: 0x1700171F RID: 5919
			// (get) Token: 0x06006AA5 RID: 27301 RVA: 0x0018B668 File Offset: 0x00189868
			// (set) Token: 0x06006AA6 RID: 27302 RVA: 0x0018B6C4 File Offset: 0x001898C4
			[Localizable(true)]
			public Font Font
			{
				get
				{
					if (this.style != null && this.style.font != null)
					{
						return this.style.font;
					}
					if (this.owner != null && this.owner.listView != null)
					{
						return this.owner.listView.Font;
					}
					return Control.DefaultFont;
				}
				set
				{
					if (this.style == null)
					{
						this.style = new ListViewItem.ListViewSubItem.SubItemStyle();
					}
					if (this.style.font != value)
					{
						this.style.font = value;
						if (this.owner != null)
						{
							this.owner.InvalidateListView();
						}
					}
				}
			}

			// Token: 0x17001720 RID: 5920
			// (get) Token: 0x06006AA7 RID: 27303 RVA: 0x0018B714 File Offset: 0x00189914
			// (set) Token: 0x06006AA8 RID: 27304 RVA: 0x0018B778 File Offset: 0x00189978
			public Color ForeColor
			{
				get
				{
					if (this.style != null && this.style.foreColor != Color.Empty)
					{
						return this.style.foreColor;
					}
					if (this.owner != null && this.owner.listView != null)
					{
						return this.owner.listView.ForeColor;
					}
					return SystemColors.WindowText;
				}
				set
				{
					if (this.style == null)
					{
						this.style = new ListViewItem.ListViewSubItem.SubItemStyle();
					}
					if (this.style.foreColor != value)
					{
						this.style.foreColor = value;
						if (this.owner != null)
						{
							this.owner.InvalidateListView();
						}
					}
				}
			}

			// Token: 0x17001721 RID: 5921
			// (get) Token: 0x06006AA9 RID: 27305 RVA: 0x0018B7CA File Offset: 0x001899CA
			// (set) Token: 0x06006AAA RID: 27306 RVA: 0x0018B7D2 File Offset: 0x001899D2
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

			// Token: 0x17001722 RID: 5922
			// (get) Token: 0x06006AAB RID: 27307 RVA: 0x0018B7DB File Offset: 0x001899DB
			// (set) Token: 0x06006AAC RID: 27308 RVA: 0x0018B7F1 File Offset: 0x001899F1
			[Localizable(true)]
			public string Text
			{
				get
				{
					if (this.text != null)
					{
						return this.text;
					}
					return "";
				}
				set
				{
					this.text = value;
					if (this.owner != null)
					{
						this.owner.UpdateSubItems(-1);
					}
				}
			}

			// Token: 0x17001723 RID: 5923
			// (get) Token: 0x06006AAD RID: 27309 RVA: 0x0018B80E File Offset: 0x00189A0E
			// (set) Token: 0x06006AAE RID: 27310 RVA: 0x0018B824 File Offset: 0x00189A24
			[Localizable(true)]
			public string Name
			{
				get
				{
					if (this.name != null)
					{
						return this.name;
					}
					return "";
				}
				set
				{
					this.name = value;
					if (this.owner != null)
					{
						this.owner.UpdateSubItems(-1);
					}
				}
			}

			// Token: 0x06006AAF RID: 27311 RVA: 0x000072B6 File Offset: 0x000054B6
			[OnDeserializing]
			private void OnDeserializing(StreamingContext ctx)
			{
			}

			// Token: 0x06006AB0 RID: 27312 RVA: 0x0018B841 File Offset: 0x00189A41
			[OnDeserialized]
			private void OnDeserialized(StreamingContext ctx)
			{
				this.name = null;
				this.userData = null;
			}

			// Token: 0x06006AB1 RID: 27313 RVA: 0x000072B6 File Offset: 0x000054B6
			[OnSerializing]
			private void OnSerializing(StreamingContext ctx)
			{
			}

			// Token: 0x06006AB2 RID: 27314 RVA: 0x000072B6 File Offset: 0x000054B6
			[OnSerialized]
			private void OnSerialized(StreamingContext ctx)
			{
			}

			// Token: 0x06006AB3 RID: 27315 RVA: 0x0018B851 File Offset: 0x00189A51
			public void ResetStyle()
			{
				if (this.style != null)
				{
					this.style = null;
					if (this.owner != null)
					{
						this.owner.InvalidateListView();
					}
				}
			}

			// Token: 0x06006AB4 RID: 27316 RVA: 0x0018B875 File Offset: 0x00189A75
			public override string ToString()
			{
				return "ListViewSubItem: {" + this.Text + "}";
			}

			// Token: 0x04003B48 RID: 15176
			[NonSerialized]
			internal ListViewItem owner;

			// Token: 0x04003B49 RID: 15177
			private string text;

			// Token: 0x04003B4A RID: 15178
			[OptionalField(VersionAdded = 2)]
			private string name;

			// Token: 0x04003B4B RID: 15179
			private ListViewItem.ListViewSubItem.SubItemStyle style;

			// Token: 0x04003B4C RID: 15180
			[OptionalField(VersionAdded = 2)]
			private object userData;

			// Token: 0x020008C5 RID: 2245
			[Serializable]
			private class SubItemStyle
			{
				// Token: 0x04004545 RID: 17733
				public Color backColor = Color.Empty;

				// Token: 0x04004546 RID: 17734
				public Color foreColor = Color.Empty;

				// Token: 0x04004547 RID: 17735
				public Font font;
			}
		}

		// Token: 0x020006D1 RID: 1745
		public class ListViewSubItemCollection : IList, ICollection, IEnumerable
		{
			// Token: 0x06006AB5 RID: 27317 RVA: 0x0018B88C File Offset: 0x00189A8C
			public ListViewSubItemCollection(ListViewItem owner)
			{
				this.owner = owner;
			}

			// Token: 0x17001724 RID: 5924
			// (get) Token: 0x06006AB6 RID: 27318 RVA: 0x0018B8A2 File Offset: 0x00189AA2
			[Browsable(false)]
			public int Count
			{
				get
				{
					return this.owner.SubItemCount;
				}
			}

			// Token: 0x17001725 RID: 5925
			// (get) Token: 0x06006AB7 RID: 27319 RVA: 0x00006C59 File Offset: 0x00004E59
			object ICollection.SyncRoot
			{
				get
				{
					return this;
				}
			}

			// Token: 0x17001726 RID: 5926
			// (get) Token: 0x06006AB8 RID: 27320 RVA: 0x00013062 File Offset: 0x00011262
			bool ICollection.IsSynchronized
			{
				get
				{
					return true;
				}
			}

			// Token: 0x17001727 RID: 5927
			// (get) Token: 0x06006AB9 RID: 27321 RVA: 0x00011A20 File Offset: 0x0000FC20
			bool IList.IsFixedSize
			{
				get
				{
					return false;
				}
			}

			// Token: 0x17001728 RID: 5928
			// (get) Token: 0x06006ABA RID: 27322 RVA: 0x00011A20 File Offset: 0x0000FC20
			public bool IsReadOnly
			{
				get
				{
					return false;
				}
			}

			// Token: 0x17001729 RID: 5929
			public ListViewItem.ListViewSubItem this[int index]
			{
				get
				{
					if (index < 0 || index >= this.Count)
					{
						throw new ArgumentOutOfRangeException("index", SR.GetString("InvalidArgument", new object[]
						{
							"index",
							index.ToString(CultureInfo.CurrentCulture)
						}));
					}
					return this.owner.subItems[index];
				}
				set
				{
					if (index < 0 || index >= this.Count)
					{
						throw new ArgumentOutOfRangeException("index", SR.GetString("InvalidArgument", new object[]
						{
							"index",
							index.ToString(CultureInfo.CurrentCulture)
						}));
					}
					this.owner.subItems[index] = value;
					this.owner.UpdateSubItems(index);
				}
			}

			// Token: 0x1700172A RID: 5930
			object IList.this[int index]
			{
				get
				{
					return this[index];
				}
				set
				{
					if (value is ListViewItem.ListViewSubItem)
					{
						this[index] = (ListViewItem.ListViewSubItem)value;
						return;
					}
					throw new ArgumentException(SR.GetString("ListViewBadListViewSubItem"), "value");
				}
			}

			// Token: 0x1700172B RID: 5931
			public virtual ListViewItem.ListViewSubItem this[string key]
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

			// Token: 0x06006AC0 RID: 27328 RVA: 0x0018B9DC File Offset: 0x00189BDC
			public ListViewItem.ListViewSubItem Add(ListViewItem.ListViewSubItem item)
			{
				this.EnsureSubItemSpace(1, -1);
				item.owner = this.owner;
				this.owner.subItems[this.owner.SubItemCount] = item;
				ListViewItem listViewItem = this.owner;
				ListViewItem listViewItem2 = this.owner;
				int subItemCount = listViewItem2.SubItemCount;
				listViewItem2.SubItemCount = subItemCount + 1;
				listViewItem.UpdateSubItems(subItemCount);
				return item;
			}

			// Token: 0x06006AC1 RID: 27329 RVA: 0x0018BA38 File Offset: 0x00189C38
			public ListViewItem.ListViewSubItem Add(string text)
			{
				ListViewItem.ListViewSubItem listViewSubItem = new ListViewItem.ListViewSubItem(this.owner, text);
				this.Add(listViewSubItem);
				return listViewSubItem;
			}

			// Token: 0x06006AC2 RID: 27330 RVA: 0x0018BA5C File Offset: 0x00189C5C
			public ListViewItem.ListViewSubItem Add(string text, Color foreColor, Color backColor, Font font)
			{
				ListViewItem.ListViewSubItem listViewSubItem = new ListViewItem.ListViewSubItem(this.owner, text, foreColor, backColor, font);
				this.Add(listViewSubItem);
				return listViewSubItem;
			}

			// Token: 0x06006AC3 RID: 27331 RVA: 0x0018BA84 File Offset: 0x00189C84
			public void AddRange(ListViewItem.ListViewSubItem[] items)
			{
				if (items == null)
				{
					throw new ArgumentNullException("items");
				}
				this.EnsureSubItemSpace(items.Length, -1);
				foreach (ListViewItem.ListViewSubItem listViewSubItem in items)
				{
					if (listViewSubItem != null)
					{
						ListViewItem.ListViewSubItem[] subItems = this.owner.subItems;
						ListViewItem listViewItem = this.owner;
						int subItemCount = listViewItem.SubItemCount;
						listViewItem.SubItemCount = subItemCount + 1;
						subItems[subItemCount] = listViewSubItem;
					}
				}
				this.owner.UpdateSubItems(-1);
			}

			// Token: 0x06006AC4 RID: 27332 RVA: 0x0018BAF0 File Offset: 0x00189CF0
			public void AddRange(string[] items)
			{
				if (items == null)
				{
					throw new ArgumentNullException("items");
				}
				this.EnsureSubItemSpace(items.Length, -1);
				foreach (string text in items)
				{
					if (text != null)
					{
						ListViewItem.ListViewSubItem[] subItems = this.owner.subItems;
						ListViewItem listViewItem = this.owner;
						int subItemCount = listViewItem.SubItemCount;
						listViewItem.SubItemCount = subItemCount + 1;
						subItems[subItemCount] = new ListViewItem.ListViewSubItem(this.owner, text);
					}
				}
				this.owner.UpdateSubItems(-1);
			}

			// Token: 0x06006AC5 RID: 27333 RVA: 0x0018BB68 File Offset: 0x00189D68
			public void AddRange(string[] items, Color foreColor, Color backColor, Font font)
			{
				if (items == null)
				{
					throw new ArgumentNullException("items");
				}
				this.EnsureSubItemSpace(items.Length, -1);
				foreach (string text in items)
				{
					if (text != null)
					{
						ListViewItem.ListViewSubItem[] subItems = this.owner.subItems;
						ListViewItem listViewItem = this.owner;
						int subItemCount = listViewItem.SubItemCount;
						listViewItem.SubItemCount = subItemCount + 1;
						subItems[subItemCount] = new ListViewItem.ListViewSubItem(this.owner, text, foreColor, backColor, font);
					}
				}
				this.owner.UpdateSubItems(-1);
			}

			// Token: 0x06006AC6 RID: 27334 RVA: 0x0018BBE2 File Offset: 0x00189DE2
			int IList.Add(object item)
			{
				if (item is ListViewItem.ListViewSubItem)
				{
					return this.IndexOf(this.Add((ListViewItem.ListViewSubItem)item));
				}
				throw new ArgumentException(SR.GetString("ListViewSubItemCollectionInvalidArgument"));
			}

			// Token: 0x06006AC7 RID: 27335 RVA: 0x0018BC10 File Offset: 0x00189E10
			public void Clear()
			{
				int subItemCount = this.owner.SubItemCount;
				if (subItemCount > 0)
				{
					this.owner.SubItemCount = 0;
					this.owner.UpdateSubItems(-1, subItemCount);
				}
			}

			// Token: 0x06006AC8 RID: 27336 RVA: 0x0018BC46 File Offset: 0x00189E46
			public bool Contains(ListViewItem.ListViewSubItem subItem)
			{
				return this.IndexOf(subItem) != -1;
			}

			// Token: 0x06006AC9 RID: 27337 RVA: 0x0018BC55 File Offset: 0x00189E55
			bool IList.Contains(object subItem)
			{
				return subItem is ListViewItem.ListViewSubItem && this.Contains((ListViewItem.ListViewSubItem)subItem);
			}

			// Token: 0x06006ACA RID: 27338 RVA: 0x0018BC6D File Offset: 0x00189E6D
			public virtual bool ContainsKey(string key)
			{
				return this.IsValidIndex(this.IndexOfKey(key));
			}

			// Token: 0x06006ACB RID: 27339 RVA: 0x0018BC7C File Offset: 0x00189E7C
			private void EnsureSubItemSpace(int size, int index)
			{
				if (this.owner.SubItemCount == 4096)
				{
					throw new InvalidOperationException(SR.GetString("ErrorCollectionFull"));
				}
				if (this.owner.SubItemCount + size <= this.owner.subItems.Length)
				{
					if (index != -1)
					{
						for (int i = this.owner.SubItemCount - 1; i >= index; i--)
						{
							this.owner.subItems[i + size] = this.owner.subItems[i];
						}
					}
					return;
				}
				if (this.owner.subItems == null)
				{
					int num = (size > 4) ? size : 4;
					this.owner.subItems = new ListViewItem.ListViewSubItem[num];
					return;
				}
				int num2 = this.owner.subItems.Length * 2;
				while (num2 - this.owner.SubItemCount < size)
				{
					num2 *= 2;
				}
				ListViewItem.ListViewSubItem[] array = new ListViewItem.ListViewSubItem[num2];
				if (index != -1)
				{
					Array.Copy(this.owner.subItems, 0, array, 0, index);
					Array.Copy(this.owner.subItems, index, array, index + size, this.owner.SubItemCount - index);
				}
				else
				{
					Array.Copy(this.owner.subItems, array, this.owner.SubItemCount);
				}
				this.owner.subItems = array;
			}

			// Token: 0x06006ACC RID: 27340 RVA: 0x0018BDBC File Offset: 0x00189FBC
			public int IndexOf(ListViewItem.ListViewSubItem subItem)
			{
				for (int i = 0; i < this.Count; i++)
				{
					if (this.owner.subItems[i] == subItem)
					{
						return i;
					}
				}
				return -1;
			}

			// Token: 0x06006ACD RID: 27341 RVA: 0x0018BDED File Offset: 0x00189FED
			int IList.IndexOf(object subItem)
			{
				if (subItem is ListViewItem.ListViewSubItem)
				{
					return this.IndexOf((ListViewItem.ListViewSubItem)subItem);
				}
				return -1;
			}

			// Token: 0x06006ACE RID: 27342 RVA: 0x0018BE08 File Offset: 0x0018A008
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

			// Token: 0x06006ACF RID: 27343 RVA: 0x0018BE85 File Offset: 0x0018A085
			private bool IsValidIndex(int index)
			{
				return index >= 0 && index < this.Count;
			}

			// Token: 0x06006AD0 RID: 27344 RVA: 0x0018BE98 File Offset: 0x0018A098
			public void Insert(int index, ListViewItem.ListViewSubItem item)
			{
				if (index < 0 || index > this.Count)
				{
					throw new ArgumentOutOfRangeException("index");
				}
				item.owner = this.owner;
				this.EnsureSubItemSpace(1, index);
				this.owner.subItems[index] = item;
				ListViewItem listViewItem = this.owner;
				int subItemCount = listViewItem.SubItemCount;
				listViewItem.SubItemCount = subItemCount + 1;
				this.owner.UpdateSubItems(-1);
			}

			// Token: 0x06006AD1 RID: 27345 RVA: 0x0018BF00 File Offset: 0x0018A100
			void IList.Insert(int index, object item)
			{
				if (item is ListViewItem.ListViewSubItem)
				{
					this.Insert(index, (ListViewItem.ListViewSubItem)item);
					return;
				}
				throw new ArgumentException(SR.GetString("ListViewBadListViewSubItem"), "item");
			}

			// Token: 0x06006AD2 RID: 27346 RVA: 0x0018BF2C File Offset: 0x0018A12C
			public void Remove(ListViewItem.ListViewSubItem item)
			{
				int num = this.IndexOf(item);
				if (num != -1)
				{
					this.RemoveAt(num);
				}
			}

			// Token: 0x06006AD3 RID: 27347 RVA: 0x0018BF4C File Offset: 0x0018A14C
			void IList.Remove(object item)
			{
				if (item is ListViewItem.ListViewSubItem)
				{
					this.Remove((ListViewItem.ListViewSubItem)item);
				}
			}

			// Token: 0x06006AD4 RID: 27348 RVA: 0x0018BF64 File Offset: 0x0018A164
			public void RemoveAt(int index)
			{
				if (index < 0 || index >= this.Count)
				{
					throw new ArgumentOutOfRangeException("index");
				}
				for (int i = index + 1; i < this.owner.SubItemCount; i++)
				{
					this.owner.subItems[i - 1] = this.owner.subItems[i];
				}
				int subItemCount = this.owner.SubItemCount;
				ListViewItem listViewItem = this.owner;
				int subItemCount2 = listViewItem.SubItemCount;
				listViewItem.SubItemCount = subItemCount2 - 1;
				this.owner.subItems[this.owner.SubItemCount] = null;
				this.owner.UpdateSubItems(-1, subItemCount);
			}

			// Token: 0x06006AD5 RID: 27349 RVA: 0x0018C004 File Offset: 0x0018A204
			public virtual void RemoveByKey(string key)
			{
				int index = this.IndexOfKey(key);
				if (this.IsValidIndex(index))
				{
					this.RemoveAt(index);
				}
			}

			// Token: 0x06006AD6 RID: 27350 RVA: 0x0018C029 File Offset: 0x0018A229
			void ICollection.CopyTo(Array dest, int index)
			{
				if (this.Count > 0)
				{
					Array.Copy(this.owner.subItems, 0, dest, index, this.Count);
				}
			}

			// Token: 0x06006AD7 RID: 27351 RVA: 0x0018C050 File Offset: 0x0018A250
			public IEnumerator GetEnumerator()
			{
				if (this.owner.subItems != null)
				{
					object[] subItems = this.owner.subItems;
					return new WindowsFormsUtils.ArraySubsetEnumerator(subItems, this.owner.SubItemCount);
				}
				return new ListViewItem.ListViewSubItem[0].GetEnumerator();
			}

			// Token: 0x04003B4D RID: 15181
			private ListViewItem owner;

			// Token: 0x04003B4E RID: 15182
			private int lastAccessedIndex = -1;
		}
	}
}
