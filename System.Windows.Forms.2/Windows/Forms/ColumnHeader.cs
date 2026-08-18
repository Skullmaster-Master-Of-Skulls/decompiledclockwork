using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.Globalization;
using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
	// Token: 0x02000156 RID: 342
	[ToolboxItem(false)]
	[DesignTimeVisible(false)]
	[DefaultProperty("Text")]
	[TypeConverter(typeof(ColumnHeaderConverter))]
	public class ColumnHeader : Component, ICloneable
	{
		// Token: 0x17000355 RID: 853
		// (get) Token: 0x06000DA1 RID: 3489 RVA: 0x00027382 File Offset: 0x00025582
		// (set) Token: 0x06000DA2 RID: 3490 RVA: 0x0002738C File Offset: 0x0002558C
		internal ListView OwnerListview
		{
			get
			{
				return this.listview;
			}
			set
			{
				int num = this.Width;
				this.listview = value;
				this.Width = num;
			}
		}

		// Token: 0x06000DA3 RID: 3491 RVA: 0x000273AE File Offset: 0x000255AE
		public ColumnHeader()
		{
			this.imageIndexer = new ColumnHeader.ColumnHeaderImageListIndexer(this);
		}

		// Token: 0x06000DA4 RID: 3492 RVA: 0x000273D8 File Offset: 0x000255D8
		public ColumnHeader(int imageIndex) : this()
		{
			this.ImageIndex = imageIndex;
		}

		// Token: 0x06000DA5 RID: 3493 RVA: 0x000273E7 File Offset: 0x000255E7
		public ColumnHeader(string imageKey) : this()
		{
			this.ImageKey = imageKey;
		}

		// Token: 0x17000356 RID: 854
		// (get) Token: 0x06000DA6 RID: 3494 RVA: 0x000273F8 File Offset: 0x000255F8
		internal int ActualImageIndex_Internal
		{
			get
			{
				int actualIndex = this.imageIndexer.ActualIndex;
				if (this.ImageList == null || this.ImageList.Images == null || actualIndex >= this.ImageList.Images.Count)
				{
					return -1;
				}
				return actualIndex;
			}
		}

		// Token: 0x17000357 RID: 855
		// (get) Token: 0x06000DA7 RID: 3495 RVA: 0x0002743C File Offset: 0x0002563C
		// (set) Token: 0x06000DA8 RID: 3496 RVA: 0x00027444 File Offset: 0x00025644
		[Localizable(true)]
		[RefreshProperties(RefreshProperties.Repaint)]
		[SRCategory("CatBehavior")]
		[SRDescription("ColumnHeaderDisplayIndexDescr")]
		public int DisplayIndex
		{
			get
			{
				return this.DisplayIndexInternal;
			}
			set
			{
				if (this.listview == null)
				{
					this.DisplayIndexInternal = value;
					return;
				}
				if (value < 0 || value > this.listview.Columns.Count - 1)
				{
					throw new ArgumentOutOfRangeException("DisplayIndex", SR.GetString("ColumnHeaderBadDisplayIndex"));
				}
				int num = Math.Min(this.DisplayIndexInternal, value);
				int num2 = Math.Max(this.DisplayIndexInternal, value);
				int[] array = new int[this.listview.Columns.Count];
				bool flag = value > this.DisplayIndexInternal;
				ColumnHeader columnHeader = null;
				for (int i = 0; i < this.listview.Columns.Count; i++)
				{
					ColumnHeader columnHeader2 = this.listview.Columns[i];
					if (columnHeader2.DisplayIndex == this.DisplayIndexInternal)
					{
						columnHeader = columnHeader2;
					}
					else if (columnHeader2.DisplayIndex >= num && columnHeader2.DisplayIndex <= num2)
					{
						columnHeader2.DisplayIndexInternal -= (flag ? 1 : -1);
					}
					if (i != this.Index)
					{
						array[columnHeader2.DisplayIndexInternal] = i;
					}
				}
				columnHeader.DisplayIndexInternal = value;
				array[columnHeader.DisplayIndexInternal] = columnHeader.Index;
				this.SetDisplayIndices(array);
			}
		}

		// Token: 0x17000358 RID: 856
		// (get) Token: 0x06000DA9 RID: 3497 RVA: 0x00027574 File Offset: 0x00025774
		// (set) Token: 0x06000DAA RID: 3498 RVA: 0x0002757C File Offset: 0x0002577C
		internal int DisplayIndexInternal
		{
			get
			{
				return this.displayIndexInternal;
			}
			set
			{
				this.displayIndexInternal = value;
			}
		}

		// Token: 0x17000359 RID: 857
		// (get) Token: 0x06000DAB RID: 3499 RVA: 0x00027585 File Offset: 0x00025785
		[Browsable(false)]
		public int Index
		{
			get
			{
				if (this.listview != null)
				{
					return this.listview.GetColumnIndex(this);
				}
				return -1;
			}
		}

		// Token: 0x1700035A RID: 858
		// (get) Token: 0x06000DAC RID: 3500 RVA: 0x000275A0 File Offset: 0x000257A0
		// (set) Token: 0x06000DAD RID: 3501 RVA: 0x00027600 File Offset: 0x00025800
		[DefaultValue(-1)]
		[TypeConverter(typeof(ImageIndexConverter))]
		[Editor("System.Windows.Forms.Design.ImageIndexEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[RefreshProperties(RefreshProperties.Repaint)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public int ImageIndex
		{
			get
			{
				if (this.imageIndexer.Index != -1 && this.ImageList != null && this.imageIndexer.Index >= this.ImageList.Images.Count)
				{
					return this.ImageList.Images.Count - 1;
				}
				return this.imageIndexer.Index;
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
				if (this.imageIndexer.Index != value)
				{
					this.imageIndexer.Index = value;
					if (this.ListView != null && this.ListView.IsHandleCreated)
					{
						this.ListView.SetColumnInfo(16, this);
					}
				}
			}
		}

		// Token: 0x1700035B RID: 859
		// (get) Token: 0x06000DAE RID: 3502 RVA: 0x00027691 File Offset: 0x00025891
		[Browsable(false)]
		public ImageList ImageList
		{
			get
			{
				return this.imageIndexer.ImageList;
			}
		}

		// Token: 0x1700035C RID: 860
		// (get) Token: 0x06000DAF RID: 3503 RVA: 0x0002769E File Offset: 0x0002589E
		// (set) Token: 0x06000DB0 RID: 3504 RVA: 0x000276AC File Offset: 0x000258AC
		[DefaultValue("")]
		[TypeConverter(typeof(ImageKeyConverter))]
		[Editor("System.Windows.Forms.Design.ImageIndexEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[RefreshProperties(RefreshProperties.Repaint)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public string ImageKey
		{
			get
			{
				return this.imageIndexer.Key;
			}
			set
			{
				if (value != this.imageIndexer.Key)
				{
					this.imageIndexer.Key = value;
					if (this.ListView != null && this.ListView.IsHandleCreated)
					{
						this.ListView.SetColumnInfo(16, this);
					}
				}
			}
		}

		// Token: 0x1700035D RID: 861
		// (get) Token: 0x06000DB1 RID: 3505 RVA: 0x00027382 File Offset: 0x00025582
		[Browsable(false)]
		public ListView ListView
		{
			get
			{
				return this.listview;
			}
		}

		// Token: 0x1700035E RID: 862
		// (get) Token: 0x06000DB2 RID: 3506 RVA: 0x000276FB File Offset: 0x000258FB
		// (set) Token: 0x06000DB3 RID: 3507 RVA: 0x00027709 File Offset: 0x00025909
		[Browsable(false)]
		[SRDescription("ColumnHeaderNameDescr")]
		public string Name
		{
			get
			{
				return WindowsFormsUtils.GetComponentName(this, this.name);
			}
			set
			{
				if (value == null)
				{
					this.name = "";
				}
				else
				{
					this.name = value;
				}
				if (this.Site != null)
				{
					this.Site.Name = value;
				}
			}
		}

		// Token: 0x1700035F RID: 863
		// (get) Token: 0x06000DB4 RID: 3508 RVA: 0x00027736 File Offset: 0x00025936
		// (set) Token: 0x06000DB5 RID: 3509 RVA: 0x0002774C File Offset: 0x0002594C
		[Localizable(true)]
		[SRDescription("ColumnCaption")]
		public string Text
		{
			get
			{
				if (this.text == null)
				{
					return "ColumnHeader";
				}
				return this.text;
			}
			set
			{
				if (value == null)
				{
					this.text = "";
				}
				else
				{
					this.text = value;
				}
				if (this.listview != null)
				{
					this.listview.SetColumnInfo(4, this);
				}
			}
		}

		// Token: 0x17000360 RID: 864
		// (get) Token: 0x06000DB6 RID: 3510 RVA: 0x0002777C File Offset: 0x0002597C
		// (set) Token: 0x06000DB7 RID: 3511 RVA: 0x000277D0 File Offset: 0x000259D0
		[SRDescription("ColumnAlignment")]
		[Localizable(true)]
		[DefaultValue(HorizontalAlignment.Left)]
		public HorizontalAlignment TextAlign
		{
			get
			{
				if (!this.textAlignInitialized && this.listview != null)
				{
					this.textAlignInitialized = true;
					if (this.Index != 0 && this.listview.RightToLeft == RightToLeft.Yes && !this.listview.IsMirrored)
					{
						this.textAlign = HorizontalAlignment.Right;
					}
				}
				return this.textAlign;
			}
			set
			{
				if (!ClientUtils.IsEnumValid(value, (int)value, 0, 2))
				{
					throw new InvalidEnumArgumentException("value", (int)value, typeof(HorizontalAlignment));
				}
				this.textAlign = value;
				if (this.Index == 0 && this.textAlign != HorizontalAlignment.Left)
				{
					this.textAlign = HorizontalAlignment.Left;
				}
				if (this.listview != null)
				{
					this.listview.SetColumnInfo(1, this);
					this.listview.Invalidate();
				}
			}
		}

		// Token: 0x17000361 RID: 865
		// (get) Token: 0x06000DB8 RID: 3512 RVA: 0x00027841 File Offset: 0x00025A41
		// (set) Token: 0x06000DB9 RID: 3513 RVA: 0x00027849 File Offset: 0x00025A49
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

		// Token: 0x17000362 RID: 866
		// (get) Token: 0x06000DBA RID: 3514 RVA: 0x00027852 File Offset: 0x00025A52
		internal int WidthInternal
		{
			get
			{
				return this.width;
			}
		}

		// Token: 0x17000363 RID: 867
		// (get) Token: 0x06000DBB RID: 3515 RVA: 0x0002785C File Offset: 0x00025A5C
		// (set) Token: 0x06000DBC RID: 3516 RVA: 0x00027934 File Offset: 0x00025B34
		[SRDescription("ColumnWidth")]
		[Localizable(true)]
		[DefaultValue(60)]
		public int Width
		{
			get
			{
				if (this.listview != null && this.listview.IsHandleCreated && !this.listview.Disposing && this.listview.View == View.Details)
				{
					IntPtr intPtr = UnsafeNativeMethods.SendMessage(new HandleRef(this.listview, this.listview.Handle), 4127, 0, 0);
					if (intPtr != IntPtr.Zero)
					{
						int num = (int)UnsafeNativeMethods.SendMessage(new HandleRef(this.listview, intPtr), 4608, 0, 0);
						if (this.Index < num)
						{
							this.width = (int)UnsafeNativeMethods.SendMessage(new HandleRef(this.listview, this.listview.Handle), 4125, this.Index, 0);
						}
					}
				}
				return this.width;
			}
			set
			{
				this.width = value;
				if (this.listview != null)
				{
					this.listview.SetColumnWidth(this.Index, ColumnHeaderAutoResizeStyle.None);
				}
			}
		}

		// Token: 0x06000DBD RID: 3517 RVA: 0x00027957 File Offset: 0x00025B57
		public void AutoResize(ColumnHeaderAutoResizeStyle headerAutoResize)
		{
			if (headerAutoResize < ColumnHeaderAutoResizeStyle.None || headerAutoResize > ColumnHeaderAutoResizeStyle.ColumnContent)
			{
				throw new InvalidEnumArgumentException("headerAutoResize", (int)headerAutoResize, typeof(ColumnHeaderAutoResizeStyle));
			}
			if (this.listview != null)
			{
				this.listview.AutoResizeColumn(this.Index, headerAutoResize);
			}
		}

		// Token: 0x06000DBE RID: 3518 RVA: 0x00027994 File Offset: 0x00025B94
		public object Clone()
		{
			Type type = base.GetType();
			ColumnHeader columnHeader;
			if (type == typeof(ColumnHeader))
			{
				columnHeader = new ColumnHeader();
			}
			else
			{
				columnHeader = (ColumnHeader)Activator.CreateInstance(type);
			}
			columnHeader.text = this.text;
			columnHeader.Width = this.width;
			columnHeader.textAlign = this.TextAlign;
			return columnHeader;
		}

		// Token: 0x06000DBF RID: 3519 RVA: 0x000279F8 File Offset: 0x00025BF8
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.listview != null)
			{
				int num = this.Index;
				if (num != -1)
				{
					this.listview.Columns.RemoveAt(num);
				}
			}
			base.Dispose(disposing);
		}

		// Token: 0x06000DC0 RID: 3520 RVA: 0x00027A33 File Offset: 0x00025C33
		private void ResetText()
		{
			this.Text = null;
		}

		// Token: 0x06000DC1 RID: 3521 RVA: 0x00027A3C File Offset: 0x00025C3C
		private void SetDisplayIndices(int[] cols)
		{
			if (this.listview.IsHandleCreated && !this.listview.Disposing)
			{
				UnsafeNativeMethods.SendMessage(new HandleRef(this.listview, this.listview.Handle), 4154, cols.Length, cols);
			}
		}

		// Token: 0x06000DC2 RID: 3522 RVA: 0x00027A88 File Offset: 0x00025C88
		private bool ShouldSerializeName()
		{
			return !string.IsNullOrEmpty(this.name);
		}

		// Token: 0x06000DC3 RID: 3523 RVA: 0x00027A98 File Offset: 0x00025C98
		private bool ShouldSerializeDisplayIndex()
		{
			return this.DisplayIndex != this.Index;
		}

		// Token: 0x06000DC4 RID: 3524 RVA: 0x00027AAB File Offset: 0x00025CAB
		internal bool ShouldSerializeText()
		{
			return this.text != null;
		}

		// Token: 0x06000DC5 RID: 3525 RVA: 0x00027AB6 File Offset: 0x00025CB6
		public override string ToString()
		{
			return "ColumnHeader: Text: " + this.Text;
		}

		// Token: 0x0400079B RID: 1947
		internal int index = -1;

		// Token: 0x0400079C RID: 1948
		internal string text;

		// Token: 0x0400079D RID: 1949
		internal string name;

		// Token: 0x0400079E RID: 1950
		internal int width = 60;

		// Token: 0x0400079F RID: 1951
		private HorizontalAlignment textAlign;

		// Token: 0x040007A0 RID: 1952
		private bool textAlignInitialized;

		// Token: 0x040007A1 RID: 1953
		private int displayIndexInternal = -1;

		// Token: 0x040007A2 RID: 1954
		private ColumnHeader.ColumnHeaderImageListIndexer imageIndexer;

		// Token: 0x040007A3 RID: 1955
		private object userData;

		// Token: 0x040007A4 RID: 1956
		private ListView listview;

		// Token: 0x02000623 RID: 1571
		internal class ColumnHeaderImageListIndexer : ImageList.Indexer
		{
			// Token: 0x06006350 RID: 25424 RVA: 0x0016EF3C File Offset: 0x0016D13C
			public ColumnHeaderImageListIndexer(ColumnHeader ch)
			{
				this.owner = ch;
			}

			// Token: 0x17001534 RID: 5428
			// (get) Token: 0x06006351 RID: 25425 RVA: 0x0016EF4B File Offset: 0x0016D14B
			// (set) Token: 0x06006352 RID: 25426 RVA: 0x000072B6 File Offset: 0x000054B6
			public override ImageList ImageList
			{
				get
				{
					if (this.owner != null && this.owner.ListView != null)
					{
						return this.owner.ListView.SmallImageList;
					}
					return null;
				}
				set
				{
				}
			}

			// Token: 0x0400392C RID: 14636
			private ColumnHeader owner;
		}
	}
}
