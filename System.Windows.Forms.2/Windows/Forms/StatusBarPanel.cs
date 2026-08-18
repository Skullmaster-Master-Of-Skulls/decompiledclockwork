using System;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
	// Token: 0x02000378 RID: 888
	[ToolboxItem(false)]
	[DesignTimeVisible(false)]
	[DefaultProperty("Text")]
	public class StatusBarPanel : Component, ISupportInitialize
	{
		// Token: 0x17000DDC RID: 3548
		// (get) Token: 0x06003A30 RID: 14896 RVA: 0x00100F25 File Offset: 0x000FF125
		// (set) Token: 0x06003A31 RID: 14897 RVA: 0x00100F2D File Offset: 0x000FF12D
		[SRCategory("CatAppearance")]
		[DefaultValue(HorizontalAlignment.Left)]
		[Localizable(true)]
		[SRDescription("StatusBarPanelAlignmentDescr")]
		public HorizontalAlignment Alignment
		{
			get
			{
				return this.alignment;
			}
			set
			{
				if (!ClientUtils.IsEnumValid(value, (int)value, 0, 2))
				{
					throw new InvalidEnumArgumentException("value", (int)value, typeof(HorizontalAlignment));
				}
				if (this.alignment != value)
				{
					this.alignment = value;
					this.Realize();
				}
			}
		}

		// Token: 0x17000DDD RID: 3549
		// (get) Token: 0x06003A32 RID: 14898 RVA: 0x00100F6B File Offset: 0x000FF16B
		// (set) Token: 0x06003A33 RID: 14899 RVA: 0x00100F73 File Offset: 0x000FF173
		[SRCategory("CatAppearance")]
		[DefaultValue(StatusBarPanelAutoSize.None)]
		[RefreshProperties(RefreshProperties.All)]
		[SRDescription("StatusBarPanelAutoSizeDescr")]
		public StatusBarPanelAutoSize AutoSize
		{
			get
			{
				return this.autoSize;
			}
			set
			{
				if (!ClientUtils.IsEnumValid(value, (int)value, 1, 3))
				{
					throw new InvalidEnumArgumentException("value", (int)value, typeof(StatusBarPanelAutoSize));
				}
				if (this.autoSize != value)
				{
					this.autoSize = value;
					this.UpdateSize();
				}
			}
		}

		// Token: 0x17000DDE RID: 3550
		// (get) Token: 0x06003A34 RID: 14900 RVA: 0x00100FB1 File Offset: 0x000FF1B1
		// (set) Token: 0x06003A35 RID: 14901 RVA: 0x00100FBC File Offset: 0x000FF1BC
		[SRCategory("CatAppearance")]
		[DefaultValue(StatusBarPanelBorderStyle.Sunken)]
		[DispId(-504)]
		[SRDescription("StatusBarPanelBorderStyleDescr")]
		public StatusBarPanelBorderStyle BorderStyle
		{
			get
			{
				return this.borderStyle;
			}
			set
			{
				if (!ClientUtils.IsEnumValid(value, (int)value, 1, 3))
				{
					throw new InvalidEnumArgumentException("value", (int)value, typeof(StatusBarPanelBorderStyle));
				}
				if (this.borderStyle != value)
				{
					this.borderStyle = value;
					this.Realize();
					if (this.Created)
					{
						this.parent.Invalidate();
					}
				}
			}
		}

		// Token: 0x17000DDF RID: 3551
		// (get) Token: 0x06003A36 RID: 14902 RVA: 0x00101018 File Offset: 0x000FF218
		internal bool Created
		{
			get
			{
				return this.parent != null && this.parent.ArePanelsRealized();
			}
		}

		// Token: 0x17000DE0 RID: 3552
		// (get) Token: 0x06003A37 RID: 14903 RVA: 0x0010102F File Offset: 0x000FF22F
		// (set) Token: 0x06003A38 RID: 14904 RVA: 0x00101038 File Offset: 0x000FF238
		[SRCategory("CatAppearance")]
		[DefaultValue(null)]
		[Localizable(true)]
		[SRDescription("StatusBarPanelIconDescr")]
		public Icon Icon
		{
			get
			{
				return this.icon;
			}
			set
			{
				if (value != null && (value.Height > SystemInformation.SmallIconSize.Height || value.Width > SystemInformation.SmallIconSize.Width))
				{
					this.icon = new Icon(value, SystemInformation.SmallIconSize);
				}
				else
				{
					this.icon = value;
				}
				if (this.Created)
				{
					IntPtr lparam = (this.icon == null) ? IntPtr.Zero : this.icon.Handle;
					this.parent.SendMessage(1039, (IntPtr)this.GetIndex(), lparam);
				}
				this.UpdateSize();
				if (this.Created)
				{
					this.parent.Invalidate();
				}
			}
		}

		// Token: 0x17000DE1 RID: 3553
		// (get) Token: 0x06003A39 RID: 14905 RVA: 0x001010E5 File Offset: 0x000FF2E5
		// (set) Token: 0x06003A3A RID: 14906 RVA: 0x001010ED File Offset: 0x000FF2ED
		internal int Index
		{
			get
			{
				return this.index;
			}
			set
			{
				this.index = value;
			}
		}

		// Token: 0x17000DE2 RID: 3554
		// (get) Token: 0x06003A3B RID: 14907 RVA: 0x001010F6 File Offset: 0x000FF2F6
		// (set) Token: 0x06003A3C RID: 14908 RVA: 0x00101100 File Offset: 0x000FF300
		[SRCategory("CatBehavior")]
		[DefaultValue(10)]
		[Localizable(true)]
		[RefreshProperties(RefreshProperties.All)]
		[SRDescription("StatusBarPanelMinWidthDescr")]
		public int MinWidth
		{
			get
			{
				return this.minWidth;
			}
			set
			{
				if (value < 0)
				{
					throw new ArgumentOutOfRangeException("MinWidth", SR.GetString("InvalidLowBoundArgumentEx", new object[]
					{
						"MinWidth",
						value.ToString(CultureInfo.CurrentCulture),
						0.ToString(CultureInfo.CurrentCulture)
					}));
				}
				if (value != this.minWidth)
				{
					this.minWidth = value;
					this.UpdateSize();
					if (this.minWidth > this.Width)
					{
						this.Width = value;
					}
				}
			}
		}

		// Token: 0x17000DE3 RID: 3555
		// (get) Token: 0x06003A3D RID: 14909 RVA: 0x0010117F File Offset: 0x000FF37F
		// (set) Token: 0x06003A3E RID: 14910 RVA: 0x0010118D File Offset: 0x000FF38D
		[SRCategory("CatAppearance")]
		[Localizable(true)]
		[SRDescription("StatusBarPanelNameDescr")]
		public string Name
		{
			get
			{
				return WindowsFormsUtils.GetComponentName(this, this.name);
			}
			set
			{
				this.name = value;
				if (this.Site != null)
				{
					this.Site.Name = this.name;
				}
			}
		}

		// Token: 0x17000DE4 RID: 3556
		// (get) Token: 0x06003A3F RID: 14911 RVA: 0x001011AF File Offset: 0x000FF3AF
		[Browsable(false)]
		public StatusBar Parent
		{
			get
			{
				return this.parent;
			}
		}

		// Token: 0x17000DE5 RID: 3557
		// (set) Token: 0x06003A40 RID: 14912 RVA: 0x001011B7 File Offset: 0x000FF3B7
		internal StatusBar ParentInternal
		{
			set
			{
				this.parent = value;
			}
		}

		// Token: 0x17000DE6 RID: 3558
		// (get) Token: 0x06003A41 RID: 14913 RVA: 0x001011C0 File Offset: 0x000FF3C0
		// (set) Token: 0x06003A42 RID: 14914 RVA: 0x001011C8 File Offset: 0x000FF3C8
		internal int Right
		{
			get
			{
				return this.right;
			}
			set
			{
				this.right = value;
			}
		}

		// Token: 0x17000DE7 RID: 3559
		// (get) Token: 0x06003A43 RID: 14915 RVA: 0x001011D1 File Offset: 0x000FF3D1
		// (set) Token: 0x06003A44 RID: 14916 RVA: 0x001011DC File Offset: 0x000FF3DC
		[SRCategory("CatAppearance")]
		[DefaultValue(StatusBarPanelStyle.Text)]
		[SRDescription("StatusBarPanelStyleDescr")]
		public StatusBarPanelStyle Style
		{
			get
			{
				return this.style;
			}
			set
			{
				if (!ClientUtils.IsEnumValid(value, (int)value, 1, 2))
				{
					throw new InvalidEnumArgumentException("value", (int)value, typeof(StatusBarPanelStyle));
				}
				if (this.style != value)
				{
					this.style = value;
					this.Realize();
					if (this.Created)
					{
						this.parent.Invalidate();
					}
				}
			}
		}

		// Token: 0x17000DE8 RID: 3560
		// (get) Token: 0x06003A45 RID: 14917 RVA: 0x00101238 File Offset: 0x000FF438
		// (set) Token: 0x06003A46 RID: 14918 RVA: 0x00101240 File Offset: 0x000FF440
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

		// Token: 0x17000DE9 RID: 3561
		// (get) Token: 0x06003A47 RID: 14919 RVA: 0x00101249 File Offset: 0x000FF449
		// (set) Token: 0x06003A48 RID: 14920 RVA: 0x0010125F File Offset: 0x000FF45F
		[SRCategory("CatAppearance")]
		[Localizable(true)]
		[DefaultValue("")]
		[SRDescription("StatusBarPanelTextDescr")]
		public string Text
		{
			get
			{
				if (this.text == null)
				{
					return "";
				}
				return this.text;
			}
			set
			{
				if (value == null)
				{
					value = "";
				}
				if (!this.Text.Equals(value))
				{
					if (value.Length == 0)
					{
						this.text = null;
					}
					else
					{
						this.text = value;
					}
					this.Realize();
					this.UpdateSize();
				}
			}
		}

		// Token: 0x17000DEA RID: 3562
		// (get) Token: 0x06003A49 RID: 14921 RVA: 0x0010129D File Offset: 0x000FF49D
		// (set) Token: 0x06003A4A RID: 14922 RVA: 0x001012B4 File Offset: 0x000FF4B4
		[SRCategory("CatAppearance")]
		[Localizable(true)]
		[DefaultValue("")]
		[SRDescription("StatusBarPanelToolTipTextDescr")]
		public string ToolTipText
		{
			get
			{
				if (this.toolTipText == null)
				{
					return "";
				}
				return this.toolTipText;
			}
			set
			{
				if (value == null)
				{
					value = "";
				}
				if (!this.ToolTipText.Equals(value))
				{
					if (value.Length == 0)
					{
						this.toolTipText = null;
					}
					else
					{
						this.toolTipText = value;
					}
					if (this.Created)
					{
						this.parent.UpdateTooltip(this);
					}
				}
			}
		}

		// Token: 0x17000DEB RID: 3563
		// (get) Token: 0x06003A4B RID: 14923 RVA: 0x00101305 File Offset: 0x000FF505
		// (set) Token: 0x06003A4C RID: 14924 RVA: 0x0010130D File Offset: 0x000FF50D
		[Localizable(true)]
		[SRCategory("CatAppearance")]
		[DefaultValue(100)]
		[SRDescription("StatusBarPanelWidthDescr")]
		public int Width
		{
			get
			{
				return this.width;
			}
			set
			{
				if (!this.initializing && value < this.minWidth)
				{
					throw new ArgumentOutOfRangeException("Width", SR.GetString("WidthGreaterThanMinWidth"));
				}
				this.width = value;
				this.UpdateSize();
			}
		}

		// Token: 0x06003A4D RID: 14925 RVA: 0x00101342 File Offset: 0x000FF542
		public void BeginInit()
		{
			this.initializing = true;
		}

		// Token: 0x06003A4E RID: 14926 RVA: 0x0010134C File Offset: 0x000FF54C
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.parent != null)
			{
				int num = this.GetIndex();
				if (num != -1)
				{
					this.parent.Panels.RemoveAt(num);
				}
			}
			base.Dispose(disposing);
		}

		// Token: 0x06003A4F RID: 14927 RVA: 0x00101387 File Offset: 0x000FF587
		public void EndInit()
		{
			this.initializing = false;
			if (this.Width < this.MinWidth)
			{
				this.Width = this.MinWidth;
			}
		}

		// Token: 0x06003A50 RID: 14928 RVA: 0x001013AC File Offset: 0x000FF5AC
		internal int GetContentsWidth(bool newPanel)
		{
			string text;
			if (newPanel)
			{
				if (this.text == null)
				{
					text = "";
				}
				else
				{
					text = this.text;
				}
			}
			else
			{
				text = this.Text;
			}
			Graphics graphics = this.parent.CreateGraphicsInternal();
			Size size = Size.Ceiling(graphics.MeasureString(text, this.parent.Font));
			if (this.icon != null)
			{
				size.Width += this.icon.Size.Width + 5;
			}
			graphics.Dispose();
			int val = size.Width + SystemInformation.BorderSize.Width * 2 + 6 + 2;
			return Math.Max(val, this.minWidth);
		}

		// Token: 0x06003A51 RID: 14929 RVA: 0x001010E5 File Offset: 0x000FF2E5
		private int GetIndex()
		{
			return this.index;
		}

		// Token: 0x06003A52 RID: 14930 RVA: 0x0010145C File Offset: 0x000FF65C
		internal void Realize()
		{
			if (this.Created)
			{
				int num = 0;
				string text;
				if (this.text == null)
				{
					text = "";
				}
				else
				{
					text = this.text;
				}
				HorizontalAlignment horizontalAlignment = this.alignment;
				if (this.parent.RightToLeft == RightToLeft.Yes)
				{
					if (horizontalAlignment != HorizontalAlignment.Left)
					{
						if (horizontalAlignment == HorizontalAlignment.Right)
						{
							horizontalAlignment = HorizontalAlignment.Left;
						}
					}
					else
					{
						horizontalAlignment = HorizontalAlignment.Right;
					}
				}
				string lParam;
				if (horizontalAlignment != HorizontalAlignment.Right)
				{
					if (horizontalAlignment == HorizontalAlignment.Center)
					{
						lParam = "\t" + text;
					}
					else
					{
						lParam = text;
					}
				}
				else
				{
					lParam = "\t\t" + text;
				}
				switch (this.borderStyle)
				{
				case StatusBarPanelBorderStyle.None:
					num |= 256;
					break;
				case StatusBarPanelBorderStyle.Raised:
					num |= 512;
					break;
				}
				StatusBarPanelStyle statusBarPanelStyle = this.style;
				if (statusBarPanelStyle != StatusBarPanelStyle.Text && statusBarPanelStyle == StatusBarPanelStyle.OwnerDraw)
				{
					num |= 4096;
				}
				int num2 = this.GetIndex() | num;
				if (this.parent.RightToLeft == RightToLeft.Yes)
				{
					num2 |= 1024;
				}
				if ((int)UnsafeNativeMethods.SendMessage(new HandleRef(this.parent, this.parent.Handle), NativeMethods.SB_SETTEXT, (IntPtr)num2, lParam) == 0)
				{
					throw new InvalidOperationException(SR.GetString("UnableToSetPanelText"));
				}
				if (this.icon != null && this.style != StatusBarPanelStyle.OwnerDraw)
				{
					this.parent.SendMessage(1039, (IntPtr)this.GetIndex(), this.icon.Handle);
				}
				else
				{
					this.parent.SendMessage(1039, (IntPtr)this.GetIndex(), IntPtr.Zero);
				}
				if (this.style == StatusBarPanelStyle.OwnerDraw)
				{
					NativeMethods.RECT rect = default(NativeMethods.RECT);
					int num3 = (int)UnsafeNativeMethods.SendMessage(new HandleRef(this.parent, this.parent.Handle), 1034, (IntPtr)this.GetIndex(), ref rect);
					if (num3 != 0)
					{
						this.parent.Invalidate(Rectangle.FromLTRB(rect.left, rect.top, rect.right, rect.bottom));
					}
				}
			}
		}

		// Token: 0x06003A53 RID: 14931 RVA: 0x00101657 File Offset: 0x000FF857
		private void UpdateSize()
		{
			if (this.autoSize == StatusBarPanelAutoSize.Contents)
			{
				this.ApplyContentSizing();
				return;
			}
			if (this.Created)
			{
				this.parent.DirtyLayout();
				this.parent.PerformLayout();
			}
		}

		// Token: 0x06003A54 RID: 14932 RVA: 0x00101688 File Offset: 0x000FF888
		private void ApplyContentSizing()
		{
			if (this.autoSize == StatusBarPanelAutoSize.Contents && this.parent != null)
			{
				int contentsWidth = this.GetContentsWidth(false);
				if (contentsWidth != this.Width)
				{
					this.Width = contentsWidth;
					if (this.Created)
					{
						this.parent.DirtyLayout();
						this.parent.PerformLayout();
					}
				}
			}
		}

		// Token: 0x06003A55 RID: 14933 RVA: 0x001016DC File Offset: 0x000FF8DC
		public override string ToString()
		{
			return "StatusBarPanel: {" + this.Text + "}";
		}

		// Token: 0x040022ED RID: 8941
		private const int DEFAULTWIDTH = 100;

		// Token: 0x040022EE RID: 8942
		private const int DEFAULTMINWIDTH = 10;

		// Token: 0x040022EF RID: 8943
		private const int PANELTEXTINSET = 3;

		// Token: 0x040022F0 RID: 8944
		private const int PANELGAP = 2;

		// Token: 0x040022F1 RID: 8945
		private string text = "";

		// Token: 0x040022F2 RID: 8946
		private string name = "";

		// Token: 0x040022F3 RID: 8947
		private string toolTipText = "";

		// Token: 0x040022F4 RID: 8948
		private Icon icon;

		// Token: 0x040022F5 RID: 8949
		private HorizontalAlignment alignment;

		// Token: 0x040022F6 RID: 8950
		private StatusBarPanelBorderStyle borderStyle = StatusBarPanelBorderStyle.Sunken;

		// Token: 0x040022F7 RID: 8951
		private StatusBarPanelStyle style = StatusBarPanelStyle.Text;

		// Token: 0x040022F8 RID: 8952
		private StatusBar parent;

		// Token: 0x040022F9 RID: 8953
		private int width = 100;

		// Token: 0x040022FA RID: 8954
		private int right;

		// Token: 0x040022FB RID: 8955
		private int minWidth = 10;

		// Token: 0x040022FC RID: 8956
		private int index;

		// Token: 0x040022FD RID: 8957
		private StatusBarPanelAutoSize autoSize = StatusBarPanelAutoSize.None;

		// Token: 0x040022FE RID: 8958
		private bool initializing;

		// Token: 0x040022FF RID: 8959
		private object userData;
	}
}
