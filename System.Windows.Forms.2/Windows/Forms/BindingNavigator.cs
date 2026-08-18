using System;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
	// Token: 0x0200013A RID: 314
	[ComVisible(true)]
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[DefaultProperty("BindingSource")]
	[DefaultEvent("RefreshItems")]
	[Designer("System.Windows.Forms.Design.BindingNavigatorDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[SRDescription("DescriptionBindingNavigator")]
	public class BindingNavigator : ToolStrip, ISupportInitialize
	{
		// Token: 0x06000B60 RID: 2912 RVA: 0x0002048C File Offset: 0x0001E68C
		[EditorBrowsable(EditorBrowsableState.Never)]
		public BindingNavigator() : this(false)
		{
		}

		// Token: 0x06000B61 RID: 2913 RVA: 0x00020495 File Offset: 0x0001E695
		public BindingNavigator(BindingSource bindingSource) : this(true)
		{
			this.BindingSource = bindingSource;
		}

		// Token: 0x06000B62 RID: 2914 RVA: 0x000204A5 File Offset: 0x0001E6A5
		[EditorBrowsable(EditorBrowsableState.Never)]
		public BindingNavigator(IContainer container) : this(false)
		{
			if (container == null)
			{
				throw new ArgumentNullException("container");
			}
			container.Add(this);
		}

		// Token: 0x06000B63 RID: 2915 RVA: 0x000204C3 File Offset: 0x0001E6C3
		public BindingNavigator(bool addStandardItems)
		{
			if (addStandardItems)
			{
				this.AddStandardItems();
			}
		}

		// Token: 0x06000B64 RID: 2916 RVA: 0x000204F2 File Offset: 0x0001E6F2
		public void BeginInit()
		{
			this.initializing = true;
		}

		// Token: 0x06000B65 RID: 2917 RVA: 0x000204FB File Offset: 0x0001E6FB
		public void EndInit()
		{
			this.initializing = false;
			this.RefreshItemsInternal();
		}

		// Token: 0x06000B66 RID: 2918 RVA: 0x0002050A File Offset: 0x0001E70A
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				this.BindingSource = null;
			}
			base.Dispose(disposing);
		}

		// Token: 0x06000B67 RID: 2919 RVA: 0x00020520 File Offset: 0x0001E720
		public virtual void AddStandardItems()
		{
			this.MoveFirstItem = new ToolStripButton();
			this.MovePreviousItem = new ToolStripButton();
			this.MoveNextItem = new ToolStripButton();
			this.MoveLastItem = new ToolStripButton();
			this.PositionItem = new ToolStripTextBox();
			this.CountItem = new ToolStripLabel();
			this.AddNewItem = new ToolStripButton();
			this.DeleteItem = new ToolStripButton();
			ToolStripSeparator toolStripSeparator = new ToolStripSeparator();
			ToolStripSeparator toolStripSeparator2 = new ToolStripSeparator();
			ToolStripSeparator toolStripSeparator3 = new ToolStripSeparator();
			char c = (string.IsNullOrEmpty(base.Name) || char.IsLower(base.Name[0])) ? 'b' : 'B';
			this.MoveFirstItem.Name = c.ToString() + "indingNavigatorMoveFirstItem";
			this.MovePreviousItem.Name = c.ToString() + "indingNavigatorMovePreviousItem";
			this.MoveNextItem.Name = c.ToString() + "indingNavigatorMoveNextItem";
			this.MoveLastItem.Name = c.ToString() + "indingNavigatorMoveLastItem";
			this.PositionItem.Name = c.ToString() + "indingNavigatorPositionItem";
			this.CountItem.Name = c.ToString() + "indingNavigatorCountItem";
			this.AddNewItem.Name = c.ToString() + "indingNavigatorAddNewItem";
			this.DeleteItem.Name = c.ToString() + "indingNavigatorDeleteItem";
			toolStripSeparator.Name = c.ToString() + "indingNavigatorSeparator";
			toolStripSeparator2.Name = c.ToString() + "indingNavigatorSeparator";
			toolStripSeparator3.Name = c.ToString() + "indingNavigatorSeparator";
			this.MoveFirstItem.Text = SR.GetString("BindingNavigatorMoveFirstItemText");
			this.MovePreviousItem.Text = SR.GetString("BindingNavigatorMovePreviousItemText");
			this.MoveNextItem.Text = SR.GetString("BindingNavigatorMoveNextItemText");
			this.MoveLastItem.Text = SR.GetString("BindingNavigatorMoveLastItemText");
			this.AddNewItem.Text = SR.GetString("BindingNavigatorAddNewItemText");
			this.DeleteItem.Text = SR.GetString("BindingNavigatorDeleteItemText");
			this.CountItem.ToolTipText = SR.GetString("BindingNavigatorCountItemTip");
			this.PositionItem.ToolTipText = SR.GetString("BindingNavigatorPositionItemTip");
			this.CountItem.AutoToolTip = false;
			this.PositionItem.AutoToolTip = false;
			this.PositionItem.AccessibleName = SR.GetString("BindingNavigatorPositionAccessibleName");
			Bitmap bitmap = new Bitmap(typeof(BindingNavigator), "BindingNavigator.MoveFirst.bmp");
			Bitmap bitmap2 = new Bitmap(typeof(BindingNavigator), "BindingNavigator.MovePrevious.bmp");
			Bitmap bitmap3 = new Bitmap(typeof(BindingNavigator), "BindingNavigator.MoveNext.bmp");
			Bitmap bitmap4 = new Bitmap(typeof(BindingNavigator), "BindingNavigator.MoveLast.bmp");
			Bitmap bitmap5 = new Bitmap(typeof(BindingNavigator), "BindingNavigator.AddNew.bmp");
			Bitmap bitmap6 = new Bitmap(typeof(BindingNavigator), "BindingNavigator.Delete.bmp");
			bitmap.MakeTransparent(Color.Magenta);
			bitmap2.MakeTransparent(Color.Magenta);
			bitmap3.MakeTransparent(Color.Magenta);
			bitmap4.MakeTransparent(Color.Magenta);
			bitmap5.MakeTransparent(Color.Magenta);
			bitmap6.MakeTransparent(Color.Magenta);
			this.MoveFirstItem.Image = bitmap;
			this.MovePreviousItem.Image = bitmap2;
			this.MoveNextItem.Image = bitmap3;
			this.MoveLastItem.Image = bitmap4;
			this.AddNewItem.Image = bitmap5;
			this.DeleteItem.Image = bitmap6;
			this.MoveFirstItem.RightToLeftAutoMirrorImage = true;
			this.MovePreviousItem.RightToLeftAutoMirrorImage = true;
			this.MoveNextItem.RightToLeftAutoMirrorImage = true;
			this.MoveLastItem.RightToLeftAutoMirrorImage = true;
			this.AddNewItem.RightToLeftAutoMirrorImage = true;
			this.DeleteItem.RightToLeftAutoMirrorImage = true;
			this.MoveFirstItem.DisplayStyle = ToolStripItemDisplayStyle.Image;
			this.MovePreviousItem.DisplayStyle = ToolStripItemDisplayStyle.Image;
			this.MoveNextItem.DisplayStyle = ToolStripItemDisplayStyle.Image;
			this.MoveLastItem.DisplayStyle = ToolStripItemDisplayStyle.Image;
			this.AddNewItem.DisplayStyle = ToolStripItemDisplayStyle.Image;
			this.DeleteItem.DisplayStyle = ToolStripItemDisplayStyle.Image;
			this.PositionItem.AutoSize = false;
			this.PositionItem.Width = 50;
			this.Items.AddRange(new ToolStripItem[]
			{
				this.MoveFirstItem,
				this.MovePreviousItem,
				toolStripSeparator,
				this.PositionItem,
				this.CountItem,
				toolStripSeparator2,
				this.MoveNextItem,
				this.MoveLastItem,
				toolStripSeparator3,
				this.AddNewItem,
				this.DeleteItem
			});
		}

		// Token: 0x170002D4 RID: 724
		// (get) Token: 0x06000B68 RID: 2920 RVA: 0x000209E3 File Offset: 0x0001EBE3
		// (set) Token: 0x06000B69 RID: 2921 RVA: 0x000209EB File Offset: 0x0001EBEB
		[DefaultValue(null)]
		[SRCategory("CatData")]
		[SRDescription("BindingNavigatorBindingSourcePropDescr")]
		[TypeConverter(typeof(ReferenceConverter))]
		public BindingSource BindingSource
		{
			get
			{
				return this.bindingSource;
			}
			set
			{
				this.WireUpBindingSource(ref this.bindingSource, value);
			}
		}

		// Token: 0x170002D5 RID: 725
		// (get) Token: 0x06000B6A RID: 2922 RVA: 0x000209FA File Offset: 0x0001EBFA
		// (set) Token: 0x06000B6B RID: 2923 RVA: 0x00020A1E File Offset: 0x0001EC1E
		[TypeConverter(typeof(ReferenceConverter))]
		[SRCategory("CatItems")]
		[SRDescription("BindingNavigatorMoveFirstItemPropDescr")]
		public ToolStripItem MoveFirstItem
		{
			get
			{
				if (this.moveFirstItem != null && this.moveFirstItem.IsDisposed)
				{
					this.moveFirstItem = null;
				}
				return this.moveFirstItem;
			}
			set
			{
				this.WireUpButton(ref this.moveFirstItem, value, new EventHandler(this.OnMoveFirst));
			}
		}

		// Token: 0x170002D6 RID: 726
		// (get) Token: 0x06000B6C RID: 2924 RVA: 0x00020A39 File Offset: 0x0001EC39
		// (set) Token: 0x06000B6D RID: 2925 RVA: 0x00020A5D File Offset: 0x0001EC5D
		[TypeConverter(typeof(ReferenceConverter))]
		[SRCategory("CatItems")]
		[SRDescription("BindingNavigatorMovePreviousItemPropDescr")]
		public ToolStripItem MovePreviousItem
		{
			get
			{
				if (this.movePreviousItem != null && this.movePreviousItem.IsDisposed)
				{
					this.movePreviousItem = null;
				}
				return this.movePreviousItem;
			}
			set
			{
				this.WireUpButton(ref this.movePreviousItem, value, new EventHandler(this.OnMovePrevious));
			}
		}

		// Token: 0x170002D7 RID: 727
		// (get) Token: 0x06000B6E RID: 2926 RVA: 0x00020A78 File Offset: 0x0001EC78
		// (set) Token: 0x06000B6F RID: 2927 RVA: 0x00020A9C File Offset: 0x0001EC9C
		[TypeConverter(typeof(ReferenceConverter))]
		[SRCategory("CatItems")]
		[SRDescription("BindingNavigatorMoveNextItemPropDescr")]
		public ToolStripItem MoveNextItem
		{
			get
			{
				if (this.moveNextItem != null && this.moveNextItem.IsDisposed)
				{
					this.moveNextItem = null;
				}
				return this.moveNextItem;
			}
			set
			{
				this.WireUpButton(ref this.moveNextItem, value, new EventHandler(this.OnMoveNext));
			}
		}

		// Token: 0x170002D8 RID: 728
		// (get) Token: 0x06000B70 RID: 2928 RVA: 0x00020AB7 File Offset: 0x0001ECB7
		// (set) Token: 0x06000B71 RID: 2929 RVA: 0x00020ADB File Offset: 0x0001ECDB
		[TypeConverter(typeof(ReferenceConverter))]
		[SRCategory("CatItems")]
		[SRDescription("BindingNavigatorMoveLastItemPropDescr")]
		public ToolStripItem MoveLastItem
		{
			get
			{
				if (this.moveLastItem != null && this.moveLastItem.IsDisposed)
				{
					this.moveLastItem = null;
				}
				return this.moveLastItem;
			}
			set
			{
				this.WireUpButton(ref this.moveLastItem, value, new EventHandler(this.OnMoveLast));
			}
		}

		// Token: 0x170002D9 RID: 729
		// (get) Token: 0x06000B72 RID: 2930 RVA: 0x00020AF6 File Offset: 0x0001ECF6
		// (set) Token: 0x06000B73 RID: 2931 RVA: 0x00020B1C File Offset: 0x0001ED1C
		[TypeConverter(typeof(ReferenceConverter))]
		[SRCategory("CatItems")]
		[SRDescription("BindingNavigatorAddNewItemPropDescr")]
		public ToolStripItem AddNewItem
		{
			get
			{
				if (this.addNewItem != null && this.addNewItem.IsDisposed)
				{
					this.addNewItem = null;
				}
				return this.addNewItem;
			}
			set
			{
				if (this.addNewItem != value && value != null)
				{
					value.InternalEnabledChanged += this.OnAddNewItemEnabledChanged;
					this.addNewItemUserEnabled = value.Enabled;
				}
				this.WireUpButton(ref this.addNewItem, value, new EventHandler(this.OnAddNew));
			}
		}

		// Token: 0x170002DA RID: 730
		// (get) Token: 0x06000B74 RID: 2932 RVA: 0x00020B6C File Offset: 0x0001ED6C
		// (set) Token: 0x06000B75 RID: 2933 RVA: 0x00020B90 File Offset: 0x0001ED90
		[TypeConverter(typeof(ReferenceConverter))]
		[SRCategory("CatItems")]
		[SRDescription("BindingNavigatorDeleteItemPropDescr")]
		public ToolStripItem DeleteItem
		{
			get
			{
				if (this.deleteItem != null && this.deleteItem.IsDisposed)
				{
					this.deleteItem = null;
				}
				return this.deleteItem;
			}
			set
			{
				if (this.deleteItem != value && value != null)
				{
					value.InternalEnabledChanged += this.OnDeleteItemEnabledChanged;
					this.deleteItemUserEnabled = value.Enabled;
				}
				this.WireUpButton(ref this.deleteItem, value, new EventHandler(this.OnDelete));
			}
		}

		// Token: 0x170002DB RID: 731
		// (get) Token: 0x06000B76 RID: 2934 RVA: 0x00020BE0 File Offset: 0x0001EDE0
		// (set) Token: 0x06000B77 RID: 2935 RVA: 0x00020C04 File Offset: 0x0001EE04
		[TypeConverter(typeof(ReferenceConverter))]
		[SRCategory("CatItems")]
		[SRDescription("BindingNavigatorPositionItemPropDescr")]
		public ToolStripItem PositionItem
		{
			get
			{
				if (this.positionItem != null && this.positionItem.IsDisposed)
				{
					this.positionItem = null;
				}
				return this.positionItem;
			}
			set
			{
				this.WireUpTextBox(ref this.positionItem, value, new KeyEventHandler(this.OnPositionKey), new EventHandler(this.OnPositionLostFocus));
			}
		}

		// Token: 0x170002DC RID: 732
		// (get) Token: 0x06000B78 RID: 2936 RVA: 0x00020C2B File Offset: 0x0001EE2B
		// (set) Token: 0x06000B79 RID: 2937 RVA: 0x00020C4F File Offset: 0x0001EE4F
		[TypeConverter(typeof(ReferenceConverter))]
		[SRCategory("CatItems")]
		[SRDescription("BindingNavigatorCountItemPropDescr")]
		public ToolStripItem CountItem
		{
			get
			{
				if (this.countItem != null && this.countItem.IsDisposed)
				{
					this.countItem = null;
				}
				return this.countItem;
			}
			set
			{
				this.WireUpLabel(ref this.countItem, value);
			}
		}

		// Token: 0x170002DD RID: 733
		// (get) Token: 0x06000B7A RID: 2938 RVA: 0x00020C5E File Offset: 0x0001EE5E
		// (set) Token: 0x06000B7B RID: 2939 RVA: 0x00020C66 File Offset: 0x0001EE66
		[SRCategory("CatAppearance")]
		[SRDescription("BindingNavigatorCountItemFormatPropDescr")]
		public string CountItemFormat
		{
			get
			{
				return this.countItemFormat;
			}
			set
			{
				if (this.countItemFormat != value)
				{
					this.countItemFormat = value;
					this.RefreshItemsInternal();
				}
			}
		}

		// Token: 0x14000055 RID: 85
		// (add) Token: 0x06000B7C RID: 2940 RVA: 0x00020C83 File Offset: 0x0001EE83
		// (remove) Token: 0x06000B7D RID: 2941 RVA: 0x00020C9C File Offset: 0x0001EE9C
		[SRCategory("CatBehavior")]
		[SRDescription("BindingNavigatorRefreshItemsEventDescr")]
		public event EventHandler RefreshItems
		{
			add
			{
				this.onRefreshItems = (EventHandler)Delegate.Combine(this.onRefreshItems, value);
			}
			remove
			{
				this.onRefreshItems = (EventHandler)Delegate.Remove(this.onRefreshItems, value);
			}
		}

		// Token: 0x06000B7E RID: 2942 RVA: 0x00020CB8 File Offset: 0x0001EEB8
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void RefreshItemsCore()
		{
			int num;
			int num2;
			bool flag;
			bool flag2;
			if (this.bindingSource == null)
			{
				num = 0;
				num2 = 0;
				flag = false;
				flag2 = false;
			}
			else
			{
				num = this.bindingSource.Count;
				num2 = this.bindingSource.Position + 1;
				flag = ((IBindingList)this.bindingSource).AllowNew;
				flag2 = ((IBindingList)this.bindingSource).AllowRemove;
			}
			if (!base.DesignMode)
			{
				if (this.MoveFirstItem != null)
				{
					this.moveFirstItem.Enabled = (num2 > 1);
				}
				if (this.MovePreviousItem != null)
				{
					this.movePreviousItem.Enabled = (num2 > 1);
				}
				if (this.MoveNextItem != null)
				{
					this.moveNextItem.Enabled = (num2 < num);
				}
				if (this.MoveLastItem != null)
				{
					this.moveLastItem.Enabled = (num2 < num);
				}
				if (this.AddNewItem != null)
				{
					EventHandler value = new EventHandler(this.OnAddNewItemEnabledChanged);
					this.addNewItem.InternalEnabledChanged -= value;
					this.addNewItem.Enabled = (this.addNewItemUserEnabled && flag);
					this.addNewItem.InternalEnabledChanged += value;
				}
				if (this.DeleteItem != null)
				{
					EventHandler value2 = new EventHandler(this.OnDeleteItemEnabledChanged);
					this.deleteItem.InternalEnabledChanged -= value2;
					this.deleteItem.Enabled = (this.deleteItemUserEnabled && flag2 && num > 0);
					this.deleteItem.InternalEnabledChanged += value2;
				}
				if (this.PositionItem != null)
				{
					this.positionItem.Enabled = (num2 > 0 && num > 0);
				}
				if (this.CountItem != null)
				{
					this.countItem.Enabled = (num > 0);
				}
			}
			if (this.positionItem != null)
			{
				this.positionItem.Text = num2.ToString(CultureInfo.CurrentCulture);
			}
			if (this.countItem != null)
			{
				this.countItem.Text = (base.DesignMode ? this.CountItemFormat : string.Format(CultureInfo.CurrentCulture, this.CountItemFormat, new object[]
				{
					num
				}));
			}
		}

		// Token: 0x06000B7F RID: 2943 RVA: 0x00020E95 File Offset: 0x0001F095
		protected virtual void OnRefreshItems()
		{
			this.RefreshItemsCore();
			if (this.onRefreshItems != null)
			{
				this.onRefreshItems(this, EventArgs.Empty);
			}
		}

		// Token: 0x06000B80 RID: 2944 RVA: 0x00020EB8 File Offset: 0x0001F0B8
		public bool Validate()
		{
			bool flag;
			return base.ValidateActiveControl(out flag);
		}

		// Token: 0x06000B81 RID: 2945 RVA: 0x00020ED0 File Offset: 0x0001F0D0
		private void AcceptNewPosition()
		{
			if (this.positionItem == null || this.bindingSource == null)
			{
				return;
			}
			int num = this.bindingSource.Position;
			try
			{
				num = Convert.ToInt32(this.positionItem.Text, CultureInfo.CurrentCulture) - 1;
			}
			catch (FormatException)
			{
			}
			catch (OverflowException)
			{
			}
			if (num != this.bindingSource.Position)
			{
				this.bindingSource.Position = num;
			}
			this.RefreshItemsInternal();
		}

		// Token: 0x06000B82 RID: 2946 RVA: 0x00020F58 File Offset: 0x0001F158
		private void CancelNewPosition()
		{
			this.RefreshItemsInternal();
		}

		// Token: 0x06000B83 RID: 2947 RVA: 0x00020F60 File Offset: 0x0001F160
		private void OnMoveFirst(object sender, EventArgs e)
		{
			if (this.Validate() && this.bindingSource != null)
			{
				this.bindingSource.MoveFirst();
				this.RefreshItemsInternal();
			}
		}

		// Token: 0x06000B84 RID: 2948 RVA: 0x00020F83 File Offset: 0x0001F183
		private void OnMovePrevious(object sender, EventArgs e)
		{
			if (this.Validate() && this.bindingSource != null)
			{
				this.bindingSource.MovePrevious();
				this.RefreshItemsInternal();
			}
		}

		// Token: 0x06000B85 RID: 2949 RVA: 0x00020FA6 File Offset: 0x0001F1A6
		private void OnMoveNext(object sender, EventArgs e)
		{
			if (this.Validate() && this.bindingSource != null)
			{
				this.bindingSource.MoveNext();
				this.RefreshItemsInternal();
			}
		}

		// Token: 0x06000B86 RID: 2950 RVA: 0x00020FC9 File Offset: 0x0001F1C9
		private void OnMoveLast(object sender, EventArgs e)
		{
			if (this.Validate() && this.bindingSource != null)
			{
				this.bindingSource.MoveLast();
				this.RefreshItemsInternal();
			}
		}

		// Token: 0x06000B87 RID: 2951 RVA: 0x00020FEC File Offset: 0x0001F1EC
		private void OnAddNew(object sender, EventArgs e)
		{
			if (this.Validate() && this.bindingSource != null)
			{
				this.bindingSource.AddNew();
				this.RefreshItemsInternal();
			}
		}

		// Token: 0x06000B88 RID: 2952 RVA: 0x00021010 File Offset: 0x0001F210
		private void OnDelete(object sender, EventArgs e)
		{
			if (this.Validate() && this.bindingSource != null)
			{
				this.bindingSource.RemoveCurrent();
				this.RefreshItemsInternal();
			}
		}

		// Token: 0x06000B89 RID: 2953 RVA: 0x00021034 File Offset: 0x0001F234
		private void OnPositionKey(object sender, KeyEventArgs e)
		{
			Keys keyCode = e.KeyCode;
			if (keyCode == Keys.Return)
			{
				this.AcceptNewPosition();
				return;
			}
			if (keyCode != Keys.Escape)
			{
				return;
			}
			this.CancelNewPosition();
		}

		// Token: 0x06000B8A RID: 2954 RVA: 0x00021060 File Offset: 0x0001F260
		private void OnPositionLostFocus(object sender, EventArgs e)
		{
			this.AcceptNewPosition();
		}

		// Token: 0x06000B8B RID: 2955 RVA: 0x00020F58 File Offset: 0x0001F158
		private void OnBindingSourceStateChanged(object sender, EventArgs e)
		{
			this.RefreshItemsInternal();
		}

		// Token: 0x06000B8C RID: 2956 RVA: 0x00020F58 File Offset: 0x0001F158
		private void OnBindingSourceListChanged(object sender, ListChangedEventArgs e)
		{
			this.RefreshItemsInternal();
		}

		// Token: 0x06000B8D RID: 2957 RVA: 0x00021068 File Offset: 0x0001F268
		private void RefreshItemsInternal()
		{
			if (this.initializing)
			{
				return;
			}
			this.OnRefreshItems();
		}

		// Token: 0x06000B8E RID: 2958 RVA: 0x00021079 File Offset: 0x0001F279
		private void ResetCountItemFormat()
		{
			this.countItemFormat = SR.GetString("BindingNavigatorCountItemFormat");
		}

		// Token: 0x06000B8F RID: 2959 RVA: 0x0002108B File Offset: 0x0001F28B
		private bool ShouldSerializeCountItemFormat()
		{
			return this.countItemFormat != SR.GetString("BindingNavigatorCountItemFormat");
		}

		// Token: 0x06000B90 RID: 2960 RVA: 0x000210A2 File Offset: 0x0001F2A2
		private void OnAddNewItemEnabledChanged(object sender, EventArgs e)
		{
			if (this.AddNewItem != null)
			{
				this.addNewItemUserEnabled = this.addNewItem.Enabled;
			}
		}

		// Token: 0x06000B91 RID: 2961 RVA: 0x000210BD File Offset: 0x0001F2BD
		private void OnDeleteItemEnabledChanged(object sender, EventArgs e)
		{
			if (this.DeleteItem != null)
			{
				this.deleteItemUserEnabled = this.deleteItem.Enabled;
			}
		}

		// Token: 0x06000B92 RID: 2962 RVA: 0x000210D8 File Offset: 0x0001F2D8
		private void WireUpButton(ref ToolStripItem oldButton, ToolStripItem newButton, EventHandler clickHandler)
		{
			if (oldButton == newButton)
			{
				return;
			}
			if (oldButton != null)
			{
				oldButton.Click -= clickHandler;
			}
			if (newButton != null)
			{
				newButton.Click += clickHandler;
			}
			oldButton = newButton;
			this.RefreshItemsInternal();
		}

		// Token: 0x06000B93 RID: 2963 RVA: 0x00021100 File Offset: 0x0001F300
		private void WireUpTextBox(ref ToolStripItem oldTextBox, ToolStripItem newTextBox, KeyEventHandler keyUpHandler, EventHandler lostFocusHandler)
		{
			if (oldTextBox != newTextBox)
			{
				ToolStripControlHost toolStripControlHost = oldTextBox as ToolStripControlHost;
				ToolStripControlHost toolStripControlHost2 = newTextBox as ToolStripControlHost;
				if (toolStripControlHost != null)
				{
					toolStripControlHost.KeyUp -= keyUpHandler;
					toolStripControlHost.LostFocus -= lostFocusHandler;
				}
				if (toolStripControlHost2 != null)
				{
					toolStripControlHost2.KeyUp += keyUpHandler;
					toolStripControlHost2.LostFocus += lostFocusHandler;
				}
				oldTextBox = newTextBox;
				this.RefreshItemsInternal();
			}
		}

		// Token: 0x06000B94 RID: 2964 RVA: 0x0002114E File Offset: 0x0001F34E
		private void WireUpLabel(ref ToolStripItem oldLabel, ToolStripItem newLabel)
		{
			if (oldLabel != newLabel)
			{
				oldLabel = newLabel;
				this.RefreshItemsInternal();
			}
		}

		// Token: 0x06000B95 RID: 2965 RVA: 0x00021160 File Offset: 0x0001F360
		private void WireUpBindingSource(ref BindingSource oldBindingSource, BindingSource newBindingSource)
		{
			if (oldBindingSource != newBindingSource)
			{
				if (oldBindingSource != null)
				{
					oldBindingSource.PositionChanged -= this.OnBindingSourceStateChanged;
					oldBindingSource.CurrentChanged -= this.OnBindingSourceStateChanged;
					oldBindingSource.CurrentItemChanged -= this.OnBindingSourceStateChanged;
					oldBindingSource.DataSourceChanged -= this.OnBindingSourceStateChanged;
					oldBindingSource.DataMemberChanged -= this.OnBindingSourceStateChanged;
					oldBindingSource.ListChanged -= this.OnBindingSourceListChanged;
				}
				if (newBindingSource != null)
				{
					newBindingSource.PositionChanged += this.OnBindingSourceStateChanged;
					newBindingSource.CurrentChanged += this.OnBindingSourceStateChanged;
					newBindingSource.CurrentItemChanged += this.OnBindingSourceStateChanged;
					newBindingSource.DataSourceChanged += this.OnBindingSourceStateChanged;
					newBindingSource.DataMemberChanged += this.OnBindingSourceStateChanged;
					newBindingSource.ListChanged += this.OnBindingSourceListChanged;
				}
				oldBindingSource = newBindingSource;
				this.RefreshItemsInternal();
			}
		}

		// Token: 0x040006CE RID: 1742
		private BindingSource bindingSource;

		// Token: 0x040006CF RID: 1743
		private ToolStripItem moveFirstItem;

		// Token: 0x040006D0 RID: 1744
		private ToolStripItem movePreviousItem;

		// Token: 0x040006D1 RID: 1745
		private ToolStripItem moveNextItem;

		// Token: 0x040006D2 RID: 1746
		private ToolStripItem moveLastItem;

		// Token: 0x040006D3 RID: 1747
		private ToolStripItem addNewItem;

		// Token: 0x040006D4 RID: 1748
		private ToolStripItem deleteItem;

		// Token: 0x040006D5 RID: 1749
		private ToolStripItem positionItem;

		// Token: 0x040006D6 RID: 1750
		private ToolStripItem countItem;

		// Token: 0x040006D7 RID: 1751
		private string countItemFormat = SR.GetString("BindingNavigatorCountItemFormat");

		// Token: 0x040006D8 RID: 1752
		private EventHandler onRefreshItems;

		// Token: 0x040006D9 RID: 1753
		private bool initializing;

		// Token: 0x040006DA RID: 1754
		private bool addNewItemUserEnabled = true;

		// Token: 0x040006DB RID: 1755
		private bool deleteItemUserEnabled = true;
	}
}
