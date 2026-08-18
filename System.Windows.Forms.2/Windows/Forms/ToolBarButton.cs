using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Text;

namespace System.Windows.Forms
{
	// Token: 0x020003AB RID: 939
	[Designer("System.Windows.Forms.Design.ToolBarButtonDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[DefaultProperty("Text")]
	[ToolboxItem(false)]
	[DesignTimeVisible(false)]
	public class ToolBarButton : Component
	{
		// Token: 0x06003D71 RID: 15729 RVA: 0x0010AE01 File Offset: 0x00109001
		public ToolBarButton()
		{
		}

		// Token: 0x06003D72 RID: 15730 RVA: 0x0010AE31 File Offset: 0x00109031
		public ToolBarButton(string text)
		{
			this.Text = text;
		}

		// Token: 0x17000EF7 RID: 3831
		// (get) Token: 0x06003D73 RID: 15731 RVA: 0x0010AE68 File Offset: 0x00109068
		internal ToolBarButton.ToolBarButtonImageIndexer ImageIndexer
		{
			get
			{
				if (this.imageIndexer == null)
				{
					this.imageIndexer = new ToolBarButton.ToolBarButtonImageIndexer(this);
				}
				return this.imageIndexer;
			}
		}

		// Token: 0x17000EF8 RID: 3832
		// (get) Token: 0x06003D74 RID: 15732 RVA: 0x0010AE84 File Offset: 0x00109084
		// (set) Token: 0x06003D75 RID: 15733 RVA: 0x0010AE8C File Offset: 0x0010908C
		[DefaultValue(null)]
		[TypeConverter(typeof(ReferenceConverter))]
		[SRDescription("ToolBarButtonMenuDescr")]
		public Menu DropDownMenu
		{
			get
			{
				return this.dropDownMenu;
			}
			set
			{
				if (value != null && !(value is ContextMenu))
				{
					throw new ArgumentException(SR.GetString("ToolBarButtonInvalidDropDownMenuType"));
				}
				this.dropDownMenu = value;
			}
		}

		// Token: 0x17000EF9 RID: 3833
		// (get) Token: 0x06003D76 RID: 15734 RVA: 0x0010AEB0 File Offset: 0x001090B0
		// (set) Token: 0x06003D77 RID: 15735 RVA: 0x0010AEB8 File Offset: 0x001090B8
		[DefaultValue(true)]
		[Localizable(true)]
		[SRDescription("ToolBarButtonEnabledDescr")]
		public bool Enabled
		{
			get
			{
				return this.enabled;
			}
			set
			{
				if (this.enabled != value)
				{
					this.enabled = value;
					if (this.parent != null && this.parent.IsHandleCreated)
					{
						this.parent.SendMessage(1025, this.FindButtonIndex(), this.enabled ? 1 : 0);
					}
				}
			}
		}

		// Token: 0x17000EFA RID: 3834
		// (get) Token: 0x06003D78 RID: 15736 RVA: 0x0010AF0D File Offset: 0x0010910D
		// (set) Token: 0x06003D79 RID: 15737 RVA: 0x0010AF1C File Offset: 0x0010911C
		[TypeConverter(typeof(ImageIndexConverter))]
		[Editor("System.Windows.Forms.Design.ImageIndexEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[DefaultValue(-1)]
		[RefreshProperties(RefreshProperties.Repaint)]
		[Localizable(true)]
		[SRDescription("ToolBarButtonImageIndexDescr")]
		public int ImageIndex
		{
			get
			{
				return this.ImageIndexer.Index;
			}
			set
			{
				if (this.ImageIndexer.Index != value)
				{
					if (value < -1)
					{
						throw new ArgumentOutOfRangeException("ImageIndex", SR.GetString("InvalidLowBoundArgumentEx", new object[]
						{
							"ImageIndex",
							value.ToString(CultureInfo.CurrentCulture),
							-1
						}));
					}
					this.ImageIndexer.Index = value;
					this.UpdateButton(false);
				}
			}
		}

		// Token: 0x17000EFB RID: 3835
		// (get) Token: 0x06003D7A RID: 15738 RVA: 0x0010AF89 File Offset: 0x00109189
		// (set) Token: 0x06003D7B RID: 15739 RVA: 0x0010AF96 File Offset: 0x00109196
		[TypeConverter(typeof(ImageKeyConverter))]
		[Editor("System.Windows.Forms.Design.ImageIndexEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[DefaultValue("")]
		[Localizable(true)]
		[RefreshProperties(RefreshProperties.Repaint)]
		[SRDescription("ToolBarButtonImageIndexDescr")]
		public string ImageKey
		{
			get
			{
				return this.ImageIndexer.Key;
			}
			set
			{
				if (this.ImageIndexer.Key != value)
				{
					this.ImageIndexer.Key = value;
					this.UpdateButton(false);
				}
			}
		}

		// Token: 0x17000EFC RID: 3836
		// (get) Token: 0x06003D7C RID: 15740 RVA: 0x0010AFBE File Offset: 0x001091BE
		// (set) Token: 0x06003D7D RID: 15741 RVA: 0x0010AFCC File Offset: 0x001091CC
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

		// Token: 0x17000EFD RID: 3837
		// (get) Token: 0x06003D7E RID: 15742 RVA: 0x0010B002 File Offset: 0x00109202
		[Browsable(false)]
		public ToolBar Parent
		{
			get
			{
				return this.parent;
			}
		}

		// Token: 0x17000EFE RID: 3838
		// (get) Token: 0x06003D7F RID: 15743 RVA: 0x0010B00C File Offset: 0x0010920C
		// (set) Token: 0x06003D80 RID: 15744 RVA: 0x0010B069 File Offset: 0x00109269
		[DefaultValue(false)]
		[SRDescription("ToolBarButtonPartialPushDescr")]
		public bool PartialPush
		{
			get
			{
				if (this.parent == null || !this.parent.IsHandleCreated)
				{
					return this.partialPush;
				}
				if ((int)this.parent.SendMessage(1037, this.FindButtonIndex(), 0) != 0)
				{
					this.partialPush = true;
				}
				else
				{
					this.partialPush = false;
				}
				return this.partialPush;
			}
			set
			{
				if (this.partialPush != value)
				{
					this.partialPush = value;
					this.UpdateButton(false);
				}
			}
		}

		// Token: 0x17000EFF RID: 3839
		// (get) Token: 0x06003D81 RID: 15745 RVA: 0x0010B082 File Offset: 0x00109282
		// (set) Token: 0x06003D82 RID: 15746 RVA: 0x0010B0A6 File Offset: 0x001092A6
		[DefaultValue(false)]
		[SRDescription("ToolBarButtonPushedDescr")]
		public bool Pushed
		{
			get
			{
				if (this.parent == null || !this.parent.IsHandleCreated)
				{
					return this.pushed;
				}
				return this.GetPushedState();
			}
			set
			{
				if (value != this.Pushed)
				{
					this.pushed = value;
					this.UpdateButton(false, false, false);
				}
			}
		}

		// Token: 0x17000F00 RID: 3840
		// (get) Token: 0x06003D83 RID: 15747 RVA: 0x0010B0C4 File Offset: 0x001092C4
		public Rectangle Rectangle
		{
			get
			{
				if (this.parent != null)
				{
					NativeMethods.RECT rect = default(NativeMethods.RECT);
					UnsafeNativeMethods.SendMessage(new HandleRef(this.parent, this.parent.Handle), 1075, this.FindButtonIndex(), ref rect);
					return Rectangle.FromLTRB(rect.left, rect.top, rect.right, rect.bottom);
				}
				return Rectangle.Empty;
			}
		}

		// Token: 0x17000F01 RID: 3841
		// (get) Token: 0x06003D84 RID: 15748 RVA: 0x0010B12D File Offset: 0x0010932D
		// (set) Token: 0x06003D85 RID: 15749 RVA: 0x0010B135 File Offset: 0x00109335
		[DefaultValue(ToolBarButtonStyle.PushButton)]
		[SRDescription("ToolBarButtonStyleDescr")]
		[RefreshProperties(RefreshProperties.Repaint)]
		public ToolBarButtonStyle Style
		{
			get
			{
				return this.style;
			}
			set
			{
				if (!ClientUtils.IsEnumValid(value, (int)value, 1, 4))
				{
					throw new InvalidEnumArgumentException("value", (int)value, typeof(ToolBarButtonStyle));
				}
				if (this.style == value)
				{
					return;
				}
				this.style = value;
				this.UpdateButton(true);
			}
		}

		// Token: 0x17000F02 RID: 3842
		// (get) Token: 0x06003D86 RID: 15750 RVA: 0x0010B175 File Offset: 0x00109375
		// (set) Token: 0x06003D87 RID: 15751 RVA: 0x0010B17D File Offset: 0x0010937D
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

		// Token: 0x17000F03 RID: 3843
		// (get) Token: 0x06003D88 RID: 15752 RVA: 0x0010B186 File Offset: 0x00109386
		// (set) Token: 0x06003D89 RID: 15753 RVA: 0x0010B19C File Offset: 0x0010939C
		[Localizable(true)]
		[DefaultValue("")]
		[SRDescription("ToolBarButtonTextDescr")]
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
				if (string.IsNullOrEmpty(value))
				{
					value = null;
				}
				if ((value == null && this.text != null) || (value != null && (this.text == null || !this.text.Equals(value))))
				{
					this.text = value;
					this.UpdateButton(WindowsFormsUtils.ContainsMnemonic(this.text), true, true);
				}
			}
		}

		// Token: 0x17000F04 RID: 3844
		// (get) Token: 0x06003D8A RID: 15754 RVA: 0x0010B1F2 File Offset: 0x001093F2
		// (set) Token: 0x06003D8B RID: 15755 RVA: 0x0010B208 File Offset: 0x00109408
		[Localizable(true)]
		[DefaultValue("")]
		[SRDescription("ToolBarButtonToolTipTextDescr")]
		public string ToolTipText
		{
			get
			{
				if (this.tooltipText != null)
				{
					return this.tooltipText;
				}
				return "";
			}
			set
			{
				this.tooltipText = value;
			}
		}

		// Token: 0x17000F05 RID: 3845
		// (get) Token: 0x06003D8C RID: 15756 RVA: 0x0010B211 File Offset: 0x00109411
		// (set) Token: 0x06003D8D RID: 15757 RVA: 0x0010B219 File Offset: 0x00109419
		[DefaultValue(true)]
		[Localizable(true)]
		[SRDescription("ToolBarButtonVisibleDescr")]
		public bool Visible
		{
			get
			{
				return this.visible;
			}
			set
			{
				if (this.visible != value)
				{
					this.visible = value;
					this.UpdateButton(false);
				}
			}
		}

		// Token: 0x17000F06 RID: 3846
		// (get) Token: 0x06003D8E RID: 15758 RVA: 0x0010B234 File Offset: 0x00109434
		internal short Width
		{
			get
			{
				int num = 0;
				ToolBarButtonStyle toolBarButtonStyle = this.Style;
				Size border3DSize = SystemInformation.Border3DSize;
				if (toolBarButtonStyle != ToolBarButtonStyle.Separator)
				{
					using (Graphics graphics = this.parent.CreateGraphicsInternal())
					{
						Size buttonSize = this.parent.buttonSize;
						if (!buttonSize.IsEmpty)
						{
							num = buttonSize.Width;
							goto IL_14D;
						}
						if (this.parent.ImageList != null || !string.IsNullOrEmpty(this.Text))
						{
							Size imageSize = this.parent.ImageSize;
							Size size = Size.Ceiling(graphics.MeasureString(this.Text, this.parent.Font));
							if (this.parent.TextAlign == ToolBarTextAlign.Right)
							{
								if (size.Width == 0)
								{
									num = imageSize.Width + border3DSize.Width * 4;
								}
								else
								{
									num = imageSize.Width + size.Width + border3DSize.Width * 6;
								}
							}
							else if (imageSize.Width > size.Width)
							{
								num = imageSize.Width + border3DSize.Width * 4;
							}
							else
							{
								num = size.Width + border3DSize.Width * 4;
							}
							if (toolBarButtonStyle == ToolBarButtonStyle.DropDownButton && this.parent.DropDownArrows)
							{
								num += 15;
							}
						}
						else
						{
							num = this.parent.ButtonSize.Width;
						}
						goto IL_14D;
					}
				}
				num = border3DSize.Width * 2;
				IL_14D:
				return (short)num;
			}
		}

		// Token: 0x06003D8F RID: 15759 RVA: 0x0010B3AC File Offset: 0x001095AC
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.parent != null)
			{
				int num = this.FindButtonIndex();
				if (num != -1)
				{
					this.parent.Buttons.RemoveAt(num);
				}
			}
			base.Dispose(disposing);
		}

		// Token: 0x06003D90 RID: 15760 RVA: 0x0010B3E8 File Offset: 0x001095E8
		private int FindButtonIndex()
		{
			for (int i = 0; i < this.parent.Buttons.Count; i++)
			{
				if (this.parent.Buttons[i] == this)
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x06003D91 RID: 15761 RVA: 0x0010B428 File Offset: 0x00109628
		internal int GetButtonWidth()
		{
			int result = this.Parent.ButtonSize.Width;
			NativeMethods.TBBUTTONINFO tbbuttoninfo = default(NativeMethods.TBBUTTONINFO);
			tbbuttoninfo.cbSize = Marshal.SizeOf(typeof(NativeMethods.TBBUTTONINFO));
			tbbuttoninfo.dwMask = 64;
			int num = (int)UnsafeNativeMethods.SendMessage(new HandleRef(this.Parent, this.Parent.Handle), NativeMethods.TB_GETBUTTONINFO, this.commandId, ref tbbuttoninfo);
			if (num != -1)
			{
				result = (int)tbbuttoninfo.cx;
			}
			return result;
		}

		// Token: 0x06003D92 RID: 15762 RVA: 0x0010B4AA File Offset: 0x001096AA
		private bool GetPushedState()
		{
			if ((int)this.parent.SendMessage(1034, this.FindButtonIndex(), 0) != 0)
			{
				this.pushed = true;
			}
			else
			{
				this.pushed = false;
			}
			return this.pushed;
		}

		// Token: 0x06003D93 RID: 15763 RVA: 0x0010B4E0 File Offset: 0x001096E0
		internal NativeMethods.TBBUTTON GetTBBUTTON(int commandId)
		{
			NativeMethods.TBBUTTON result = default(NativeMethods.TBBUTTON);
			result.iBitmap = this.ImageIndexer.ActualIndex;
			result.fsState = 0;
			if (this.enabled)
			{
				result.fsState |= 4;
			}
			if (this.partialPush && this.style == ToolBarButtonStyle.ToggleButton)
			{
				result.fsState |= 16;
			}
			if (this.pushed)
			{
				result.fsState |= 1;
			}
			if (!this.visible)
			{
				result.fsState |= 8;
			}
			switch (this.style)
			{
			case ToolBarButtonStyle.PushButton:
				result.fsStyle = 0;
				break;
			case ToolBarButtonStyle.ToggleButton:
				result.fsStyle = 2;
				break;
			case ToolBarButtonStyle.Separator:
				result.fsStyle = 1;
				break;
			case ToolBarButtonStyle.DropDownButton:
				result.fsStyle = 8;
				break;
			}
			result.dwData = (IntPtr)0;
			result.iString = this.stringIndex;
			this.commandId = commandId;
			result.idCommand = commandId;
			return result;
		}

		// Token: 0x06003D94 RID: 15764 RVA: 0x0010B5E0 File Offset: 0x001097E0
		internal NativeMethods.TBBUTTONINFO GetTBBUTTONINFO(bool updateText, int newCommandId)
		{
			NativeMethods.TBBUTTONINFO result = default(NativeMethods.TBBUTTONINFO);
			result.cbSize = Marshal.SizeOf(typeof(NativeMethods.TBBUTTONINFO));
			result.dwMask = 13;
			if (updateText)
			{
				result.dwMask |= 2;
			}
			result.iImage = this.ImageIndexer.ActualIndex;
			if (newCommandId != this.commandId)
			{
				this.commandId = newCommandId;
				result.idCommand = newCommandId;
				result.dwMask |= 32;
			}
			result.fsState = 0;
			if (this.enabled)
			{
				result.fsState |= 4;
			}
			if (this.partialPush && this.style == ToolBarButtonStyle.ToggleButton)
			{
				result.fsState |= 16;
			}
			if (this.pushed)
			{
				result.fsState |= 1;
			}
			if (!this.visible)
			{
				result.fsState |= 8;
			}
			switch (this.style)
			{
			case ToolBarButtonStyle.PushButton:
				result.fsStyle = 0;
				break;
			case ToolBarButtonStyle.ToggleButton:
				result.fsStyle = 2;
				break;
			case ToolBarButtonStyle.Separator:
				result.fsStyle = 1;
				break;
			}
			if (this.text == null)
			{
				result.pszText = Marshal.StringToHGlobalAuto("\0\0");
			}
			else
			{
				string s = this.text;
				this.PrefixAmpersands(ref s);
				result.pszText = Marshal.StringToHGlobalAuto(s);
			}
			return result;
		}

		// Token: 0x06003D95 RID: 15765 RVA: 0x0010B734 File Offset: 0x00109934
		private void PrefixAmpersands(ref string value)
		{
			if (value == null || value.Length == 0)
			{
				return;
			}
			if (value.IndexOf('&') < 0)
			{
				return;
			}
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < value.Length; i++)
			{
				if (value[i] == '&')
				{
					if (i < value.Length - 1 && value[i + 1] == '&')
					{
						i++;
					}
					stringBuilder.Append("&&");
				}
				else
				{
					stringBuilder.Append(value[i]);
				}
			}
			value = stringBuilder.ToString();
		}

		// Token: 0x06003D96 RID: 15766 RVA: 0x0010B7C3 File Offset: 0x001099C3
		public override string ToString()
		{
			return "ToolBarButton: " + this.Text + ", Style: " + this.Style.ToString("G");
		}

		// Token: 0x06003D97 RID: 15767 RVA: 0x0010B7EF File Offset: 0x001099EF
		internal void UpdateButton(bool recreate)
		{
			this.UpdateButton(recreate, false, true);
		}

		// Token: 0x06003D98 RID: 15768 RVA: 0x0010B7FC File Offset: 0x001099FC
		private void UpdateButton(bool recreate, bool updateText, bool updatePushedState)
		{
			if (this.style == ToolBarButtonStyle.DropDownButton && this.parent != null && this.parent.DropDownArrows)
			{
				recreate = true;
			}
			if (updatePushedState && this.parent != null && this.parent.IsHandleCreated)
			{
				this.GetPushedState();
			}
			if (this.parent != null)
			{
				int num = this.FindButtonIndex();
				if (num != -1)
				{
					this.parent.InternalSetButton(num, this, recreate, updateText);
				}
			}
		}

		// Token: 0x04002420 RID: 9248
		private string text;

		// Token: 0x04002421 RID: 9249
		private string name;

		// Token: 0x04002422 RID: 9250
		private string tooltipText;

		// Token: 0x04002423 RID: 9251
		private bool enabled = true;

		// Token: 0x04002424 RID: 9252
		private bool visible = true;

		// Token: 0x04002425 RID: 9253
		private bool pushed;

		// Token: 0x04002426 RID: 9254
		private bool partialPush;

		// Token: 0x04002427 RID: 9255
		private int commandId = -1;

		// Token: 0x04002428 RID: 9256
		private ToolBarButton.ToolBarButtonImageIndexer imageIndexer;

		// Token: 0x04002429 RID: 9257
		private ToolBarButtonStyle style = ToolBarButtonStyle.PushButton;

		// Token: 0x0400242A RID: 9258
		private object userData;

		// Token: 0x0400242B RID: 9259
		internal IntPtr stringIndex = (IntPtr)(-1);

		// Token: 0x0400242C RID: 9260
		internal ToolBar parent;

		// Token: 0x0400242D RID: 9261
		internal Menu dropDownMenu;

		// Token: 0x020007F6 RID: 2038
		internal class ToolBarButtonImageIndexer : ImageList.Indexer
		{
			// Token: 0x06006EA4 RID: 28324 RVA: 0x001959CE File Offset: 0x00193BCE
			public ToolBarButtonImageIndexer(ToolBarButton button)
			{
				this.owner = button;
			}

			// Token: 0x17001827 RID: 6183
			// (get) Token: 0x06006EA5 RID: 28325 RVA: 0x001959DD File Offset: 0x00193BDD
			// (set) Token: 0x06006EA6 RID: 28326 RVA: 0x000072B6 File Offset: 0x000054B6
			public override ImageList ImageList
			{
				get
				{
					if (this.owner != null && this.owner.parent != null)
					{
						return this.owner.parent.ImageList;
					}
					return null;
				}
				set
				{
				}
			}

			// Token: 0x040042EB RID: 17131
			private ToolBarButton owner;
		}
	}
}
