using System;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace System.Windows.Forms
{
	// Token: 0x020002D6 RID: 726
	[TypeConverter(typeof(ListViewGroupConverter))]
	[ToolboxItem(false)]
	[DesignTimeVisible(false)]
	[DefaultProperty("Header")]
	[Serializable]
	public sealed class ListViewGroup : ISerializable
	{
		// Token: 0x06002DDB RID: 11739 RVA: 0x000D0BB9 File Offset: 0x000CEDB9
		public ListViewGroup() : this(SR.GetString("ListViewGroupDefaultHeader", new object[]
		{
			ListViewGroup.nextHeader++
		}))
		{
		}

		// Token: 0x06002DDC RID: 11740 RVA: 0x000D0BE6 File Offset: 0x000CEDE6
		private ListViewGroup(SerializationInfo info, StreamingContext context) : this()
		{
			this.Deserialize(info, context);
		}

		// Token: 0x06002DDD RID: 11741 RVA: 0x000D0BF6 File Offset: 0x000CEDF6
		public ListViewGroup(string key, string headerText) : this()
		{
			this.name = key;
			this.header = headerText;
		}

		// Token: 0x06002DDE RID: 11742 RVA: 0x000D0C0C File Offset: 0x000CEE0C
		public ListViewGroup(string header)
		{
			this.header = header;
			this.id = ListViewGroup.nextID++;
		}

		// Token: 0x06002DDF RID: 11743 RVA: 0x000D0C2E File Offset: 0x000CEE2E
		public ListViewGroup(string header, HorizontalAlignment headerAlignment) : this(header)
		{
			this.headerAlignment = headerAlignment;
		}

		// Token: 0x17000AB0 RID: 2736
		// (get) Token: 0x06002DE0 RID: 11744 RVA: 0x000D0C3E File Offset: 0x000CEE3E
		// (set) Token: 0x06002DE1 RID: 11745 RVA: 0x000D0C54 File Offset: 0x000CEE54
		[SRCategory("CatAppearance")]
		public string Header
		{
			get
			{
				if (this.header != null)
				{
					return this.header;
				}
				return "";
			}
			set
			{
				if (this.header != value)
				{
					this.header = value;
					if (this.listView != null)
					{
						this.listView.RecreateHandleInternal();
					}
				}
			}
		}

		// Token: 0x17000AB1 RID: 2737
		// (get) Token: 0x06002DE2 RID: 11746 RVA: 0x000D0C7E File Offset: 0x000CEE7E
		// (set) Token: 0x06002DE3 RID: 11747 RVA: 0x000D0C86 File Offset: 0x000CEE86
		[DefaultValue(HorizontalAlignment.Left)]
		[SRCategory("CatAppearance")]
		public HorizontalAlignment HeaderAlignment
		{
			get
			{
				return this.headerAlignment;
			}
			set
			{
				if (!ClientUtils.IsEnumValid(value, (int)value, 0, 2))
				{
					throw new InvalidEnumArgumentException("value", (int)value, typeof(HorizontalAlignment));
				}
				if (this.headerAlignment != value)
				{
					this.headerAlignment = value;
					this.UpdateListView();
				}
			}
		}

		// Token: 0x17000AB2 RID: 2738
		// (get) Token: 0x06002DE4 RID: 11748 RVA: 0x000D0CC4 File Offset: 0x000CEEC4
		internal int ID
		{
			get
			{
				return this.id;
			}
		}

		// Token: 0x17000AB3 RID: 2739
		// (get) Token: 0x06002DE5 RID: 11749 RVA: 0x000D0CCC File Offset: 0x000CEECC
		[Browsable(false)]
		public ListView.ListViewItemCollection Items
		{
			get
			{
				if (this.items == null)
				{
					this.items = new ListView.ListViewItemCollection(new ListViewGroupItemCollection(this));
				}
				return this.items;
			}
		}

		// Token: 0x17000AB4 RID: 2740
		// (get) Token: 0x06002DE6 RID: 11750 RVA: 0x000D0CED File Offset: 0x000CEEED
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public ListView ListView
		{
			get
			{
				return this.listView;
			}
		}

		// Token: 0x17000AB5 RID: 2741
		// (get) Token: 0x06002DE7 RID: 11751 RVA: 0x000D0CED File Offset: 0x000CEEED
		// (set) Token: 0x06002DE8 RID: 11752 RVA: 0x000D0CF5 File Offset: 0x000CEEF5
		internal ListView ListViewInternal
		{
			get
			{
				return this.listView;
			}
			set
			{
				if (this.listView != value)
				{
					this.listView = value;
				}
			}
		}

		// Token: 0x17000AB6 RID: 2742
		// (get) Token: 0x06002DE9 RID: 11753 RVA: 0x000D0D07 File Offset: 0x000CEF07
		// (set) Token: 0x06002DEA RID: 11754 RVA: 0x000D0D0F File Offset: 0x000CEF0F
		[SRCategory("CatBehavior")]
		[SRDescription("ListViewGroupNameDescr")]
		[Browsable(true)]
		[DefaultValue("")]
		public string Name
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

		// Token: 0x17000AB7 RID: 2743
		// (get) Token: 0x06002DEB RID: 11755 RVA: 0x000D0D18 File Offset: 0x000CEF18
		// (set) Token: 0x06002DEC RID: 11756 RVA: 0x000D0D20 File Offset: 0x000CEF20
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

		// Token: 0x06002DED RID: 11757 RVA: 0x000D0D2C File Offset: 0x000CEF2C
		private void Deserialize(SerializationInfo info, StreamingContext context)
		{
			int num = 0;
			foreach (SerializationEntry serializationEntry in info)
			{
				if (serializationEntry.Name == "Header")
				{
					this.Header = (string)serializationEntry.Value;
				}
				else if (serializationEntry.Name == "HeaderAlignment")
				{
					this.HeaderAlignment = (HorizontalAlignment)serializationEntry.Value;
				}
				else if (serializationEntry.Name == "Tag")
				{
					this.Tag = serializationEntry.Value;
				}
				else if (serializationEntry.Name == "ItemsCount")
				{
					num = (int)serializationEntry.Value;
				}
				else if (serializationEntry.Name == "Name")
				{
					this.Name = (string)serializationEntry.Value;
				}
			}
			if (num > 0)
			{
				ListViewItem[] array = new ListViewItem[num];
				for (int i = 0; i < num; i++)
				{
					array[i] = (ListViewItem)info.GetValue("Item" + i.ToString(), typeof(ListViewItem));
				}
				this.Items.AddRange(array);
			}
		}

		// Token: 0x06002DEE RID: 11758 RVA: 0x000D0E64 File Offset: 0x000CF064
		public override string ToString()
		{
			return this.Header;
		}

		// Token: 0x06002DEF RID: 11759 RVA: 0x000D0E6C File Offset: 0x000CF06C
		private void UpdateListView()
		{
			if (this.listView != null && this.listView.IsHandleCreated)
			{
				this.listView.UpdateGroupNative(this);
			}
		}

		// Token: 0x06002DF0 RID: 11760 RVA: 0x000D0E90 File Offset: 0x000CF090
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue("Header", this.Header);
			info.AddValue("HeaderAlignment", this.HeaderAlignment);
			info.AddValue("Tag", this.Tag);
			if (!string.IsNullOrEmpty(this.Name))
			{
				info.AddValue("Name", this.Name);
			}
			if (this.items != null && this.items.Count > 0)
			{
				info.AddValue("ItemsCount", this.Items.Count);
				for (int i = 0; i < this.Items.Count; i++)
				{
					info.AddValue("Item" + i.ToString(CultureInfo.InvariantCulture), this.Items[i], typeof(ListViewItem));
				}
			}
		}

		// Token: 0x04001310 RID: 4880
		private ListView listView;

		// Token: 0x04001311 RID: 4881
		private int id;

		// Token: 0x04001312 RID: 4882
		private string header;

		// Token: 0x04001313 RID: 4883
		private HorizontalAlignment headerAlignment;

		// Token: 0x04001314 RID: 4884
		private ListView.ListViewItemCollection items;

		// Token: 0x04001315 RID: 4885
		private static int nextID;

		// Token: 0x04001316 RID: 4886
		private static int nextHeader = 1;

		// Token: 0x04001317 RID: 4887
		private object userData;

		// Token: 0x04001318 RID: 4888
		private string name;
	}
}
