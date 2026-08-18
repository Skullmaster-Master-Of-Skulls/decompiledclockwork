using System;
using System.Collections;
using System.Design;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Design;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Security.Permissions;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using Microsoft.Win32;

namespace System.ComponentModel.Design
{
	// Token: 0x020001BB RID: 443
	public sealed class MultilineStringEditor : UITypeEditor
	{
		// Token: 0x0600101D RID: 4125 RVA: 0x0005B510 File Offset: 0x00059710
		public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
		{
			if (provider != null)
			{
				IWindowsFormsEditorService windowsFormsEditorService = (IWindowsFormsEditorService)provider.GetService(typeof(IWindowsFormsEditorService));
				if (windowsFormsEditorService != null)
				{
					if (this._editorUI == null)
					{
						this._editorUI = DpiHelper.CreateInstanceInSystemAwareContext<MultilineStringEditor.MultilineStringEditorUI>(() => new MultilineStringEditor.MultilineStringEditorUI());
					}
					this._editorUI.BeginEdit(windowsFormsEditorService, value);
					windowsFormsEditorService.DropDownControl(this._editorUI);
					object value2 = this._editorUI.Value;
					if (this._editorUI.EndEdit())
					{
						value = value2;
					}
				}
			}
			return value;
		}

		// Token: 0x0600101E RID: 4126 RVA: 0x0003DFAE File Offset: 0x0003C1AE
		public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
		{
			return UITypeEditorEditStyle.DropDown;
		}

		// Token: 0x0600101F RID: 4127 RVA: 0x0000445B File Offset: 0x0000265B
		public override bool GetPaintValueSupported(ITypeDescriptorContext context)
		{
			return false;
		}

		// Token: 0x0400095C RID: 2396
		private MultilineStringEditor.MultilineStringEditorUI _editorUI;

		// Token: 0x02000492 RID: 1170
		private class MultilineStringEditorUI : RichTextBox
		{
			// Token: 0x06002B21 RID: 11041 RVA: 0x00102644 File Offset: 0x00100844
			internal MultilineStringEditorUI()
			{
				this.InitializeComponent();
				this._watermarkFormat = new StringFormat();
				this._watermarkFormat.Alignment = StringAlignment.Center;
				this._watermarkFormat.LineAlignment = StringAlignment.Center;
				this._fallbackFonts = new Hashtable(2);
			}

			// Token: 0x06002B22 RID: 11042 RVA: 0x001026A9 File Offset: 0x001008A9
			private void InitializeComponent()
			{
				base.RichTextShortcutsEnabled = false;
				base.WordWrap = false;
				base.BorderStyle = BorderStyle.None;
				this.Multiline = true;
				base.ScrollBars = RichTextBoxScrollBars.Both;
				base.DetectUrls = false;
			}

			// Token: 0x06002B23 RID: 11043 RVA: 0x001026D5 File Offset: 0x001008D5
			protected override void Dispose(bool disposing)
			{
				if (disposing && this._watermarkBrush != null)
				{
					this._watermarkBrush.Dispose();
					this._watermarkBrush = null;
				}
				base.Dispose(disposing);
			}

			// Token: 0x06002B24 RID: 11044 RVA: 0x001026FB File Offset: 0x001008FB
			[SecurityPermission(SecurityAction.InheritanceDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
			protected override object CreateRichEditOleCallback()
			{
				return new MultilineStringEditor.OleCallback(this);
			}

			// Token: 0x06002B25 RID: 11045 RVA: 0x00102703 File Offset: 0x00100903
			protected override bool IsInputKey(Keys keyData)
			{
				return ((keyData & Keys.KeyCode) == Keys.Return && this.Multiline && (keyData & Keys.Alt) == Keys.None) || base.IsInputKey(keyData);
			}

			// Token: 0x06002B26 RID: 11046 RVA: 0x0010272C File Offset: 0x0010092C
			protected override bool ProcessDialogKey(Keys keyData)
			{
				if ((keyData & (Keys.Shift | Keys.Alt)) == Keys.None)
				{
					Keys keys = keyData & Keys.KeyCode;
					if (keys == Keys.Escape && (keyData & Keys.Control) == Keys.None)
					{
						this._escapePressed = true;
					}
				}
				return base.ProcessDialogKey(keyData);
			}

			// Token: 0x06002B27 RID: 11047 RVA: 0x00102768 File Offset: 0x00100968
			protected override void OnKeyDown(KeyEventArgs e)
			{
				if (this.ShouldShowWatermark)
				{
					base.Invalidate();
				}
				if (e.Control && e.KeyCode == Keys.Return && e.Modifiers == Keys.Control)
				{
					this._editorService.CloseDropDown();
					this._ctrlEnterPressed = true;
				}
			}

			// Token: 0x1700091A RID: 2330
			// (get) Token: 0x06002B28 RID: 11048 RVA: 0x001027B4 File Offset: 0x001009B4
			internal object Value
			{
				get
				{
					return this.Text;
				}
			}

			// Token: 0x06002B29 RID: 11049 RVA: 0x001027BC File Offset: 0x001009BC
			internal void BeginEdit(IWindowsFormsEditorService editorService, object value)
			{
				this._editing = true;
				this._editorService = editorService;
				this._minimumSize = Size.Empty;
				this._watermarkSize = Size.Empty;
				this._escapePressed = false;
				this._ctrlEnterPressed = false;
				this.Text = (string)value;
			}

			// Token: 0x06002B2A RID: 11050 RVA: 0x001027FC File Offset: 0x001009FC
			internal bool EndEdit()
			{
				this._editing = false;
				this._editorService = null;
				this._ctrlEnterPressed = false;
				this.Text = null;
				return !this._escapePressed;
			}

			// Token: 0x06002B2B RID: 11051 RVA: 0x00102824 File Offset: 0x00100A24
			private void ResizeToContent()
			{
				if (this._firstTimeResizeToContent)
				{
					this._firstTimeResizeToContent = false;
				}
				else if (!base.Visible)
				{
					return;
				}
				Size contentSize = this.ContentSize;
				contentSize.Width += SystemInformation.VerticalScrollBarWidth;
				contentSize.Width = Math.Max(contentSize.Width, this.MinimumSize.Width);
				Rectangle workingArea = Screen.GetWorkingArea(this);
				int val = base.PointToScreen(base.Location).X - workingArea.Left;
				int num = Math.Min(contentSize.Width - base.ClientSize.Width, val);
				base.ClientSize = new Size(base.ClientSize.Width + num, this.MinimumSize.Height);
			}

			// Token: 0x1700091B RID: 2331
			// (get) Token: 0x06002B2C RID: 11052 RVA: 0x001028F8 File Offset: 0x00100AF8
			private Size ContentSize
			{
				get
				{
					NativeMethods.RECT rect = default(NativeMethods.RECT);
					HandleRef hDC = new HandleRef(null, UnsafeNativeMethods.GetDC(NativeMethods.NullHandleRef));
					HandleRef hObject = new HandleRef(null, this.Font.ToHfont());
					HandleRef hObject2 = new HandleRef(null, SafeNativeMethods.SelectObject(hDC, hObject));
					try
					{
						SafeNativeMethods.DrawText(hDC, this.Text, this.Text.Length, ref rect, 1024);
					}
					finally
					{
						NativeMethods.ExternalDeleteObject(hObject);
						SafeNativeMethods.SelectObject(hDC, hObject2);
						UnsafeNativeMethods.ReleaseDC(NativeMethods.NullHandleRef, hDC);
					}
					return new Size(rect.right - rect.left + 3, rect.bottom - rect.top);
				}
			}

			// Token: 0x06002B2D RID: 11053 RVA: 0x001029B0 File Offset: 0x00100BB0
			protected override void OnContentsResized(ContentsResizedEventArgs e)
			{
				this._contentsResizedRaised = true;
				this.ResizeToContent();
				base.OnContentsResized(e);
			}

			// Token: 0x06002B2E RID: 11054 RVA: 0x001029C6 File Offset: 0x00100BC6
			protected override void OnTextChanged(EventArgs e)
			{
				if (!this._contentsResizedRaised)
				{
					this.ResizeToContent();
				}
				this._contentsResizedRaised = false;
				base.OnTextChanged(e);
			}

			// Token: 0x06002B2F RID: 11055 RVA: 0x001029E4 File Offset: 0x00100BE4
			protected override void OnVisibleChanged(EventArgs e)
			{
				if (base.Visible)
				{
					this.ProcessSurrogateFonts(0, this.Text.Length);
					base.Select(this.Text.Length, 0);
				}
				this.ResizeToContent();
				base.OnVisibleChanged(e);
			}

			// Token: 0x1700091C RID: 2332
			// (get) Token: 0x06002B30 RID: 11056 RVA: 0x00102A20 File Offset: 0x00100C20
			public override Size MinimumSize
			{
				get
				{
					if (this._minimumSize == Size.Empty)
					{
						Rectangle workingArea = Screen.GetWorkingArea(this);
						this._minimumSize = new Size((int)Math.Min(Math.Ceiling((double)this.WatermarkSize.Width * 1.75), (double)(workingArea.Width / 3)), Math.Min(this.Font.Height * 10, workingArea.Height / 3));
					}
					return this._minimumSize;
				}
			}

			// Token: 0x1700091D RID: 2333
			// (get) Token: 0x06002B31 RID: 11057 RVA: 0x00102AA1 File Offset: 0x00100CA1
			// (set) Token: 0x06002B32 RID: 11058 RVA: 0x00003937 File Offset: 0x00001B37
			public override Font Font
			{
				get
				{
					return base.Font;
				}
				set
				{
				}
			}

			// Token: 0x06002B33 RID: 11059 RVA: 0x00102AAC File Offset: 0x00100CAC
			public void ProcessSurrogateFonts(int start, int length)
			{
				string text = this.Text;
				if (text == null)
				{
					return;
				}
				int[] array = StringInfo.ParseCombiningCharacters(text);
				if (array.Length != text.Length)
				{
					for (int i = 0; i < array.Length; i++)
					{
						if (array[i] >= start && array[i] < start + length)
						{
							char c = text[array[i]];
							char c2 = '\0';
							if (array[i] + 1 < text.Length)
							{
								c2 = text[array[i] + 1];
							}
							if (c >= '\ud800' && c <= '\udbff' && c2 >= '\udc00' && c2 <= '\udfff')
							{
								int num = (int)(c / '@' - '͠' + '\u0001');
								Font font = this._fallbackFonts[num] as Font;
								if (font == null)
								{
									using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion\\LanguagePack\\SurrogateFallback"))
									{
										if (registryKey != null)
										{
											string text2 = (string)registryKey.GetValue("Plane" + num.ToString());
											if (!string.IsNullOrEmpty(text2))
											{
												font = new Font(text2, base.Font.Size, base.Font.Style);
											}
											this._fallbackFonts[num] = font;
										}
									}
								}
								if (font != null)
								{
									int length2 = (i == array.Length - 1) ? (text.Length - array[i]) : (array[i + 1] - array[i]);
									base.Select(array[i], length2);
									base.SelectionFont = font;
								}
							}
						}
					}
				}
			}

			// Token: 0x1700091E RID: 2334
			// (get) Token: 0x06002B34 RID: 11060 RVA: 0x00102C48 File Offset: 0x00100E48
			// (set) Token: 0x06002B35 RID: 11061 RVA: 0x00102CC1 File Offset: 0x00100EC1
			public override string Text
			{
				get
				{
					if (!base.IsHandleCreated)
					{
						return "";
					}
					int windowTextLength = SafeNativeMethods.GetWindowTextLength(new HandleRef(this, base.Handle));
					StringBuilder stringBuilder = new StringBuilder(windowTextLength + 1);
					UnsafeNativeMethods.GetWindowText(new HandleRef(this, base.Handle), stringBuilder, stringBuilder.Capacity);
					if (!this._ctrlEnterPressed)
					{
						return stringBuilder.ToString();
					}
					string text = stringBuilder.ToString();
					int startIndex = text.LastIndexOf("\r\n");
					return text.Remove(startIndex, 2);
				}
				set
				{
					base.Text = value;
				}
			}

			// Token: 0x1700091F RID: 2335
			// (get) Token: 0x06002B36 RID: 11062 RVA: 0x00102CCC File Offset: 0x00100ECC
			private Size WatermarkSize
			{
				get
				{
					if (this._watermarkSize == Size.Empty)
					{
						SizeF sizeF;
						using (Graphics graphics = base.CreateGraphics())
						{
							sizeF = graphics.MeasureString(SR.GetString("MultilineStringEditorWatermark"), this.Font);
						}
						this._watermarkSize = new Size((int)Math.Ceiling((double)sizeF.Width), (int)Math.Ceiling((double)sizeF.Height));
					}
					return this._watermarkSize;
				}
			}

			// Token: 0x17000920 RID: 2336
			// (get) Token: 0x06002B37 RID: 11063 RVA: 0x00102D54 File Offset: 0x00100F54
			private bool ShouldShowWatermark
			{
				get
				{
					return this.Text.Length == 0 && this.WatermarkSize.Width < base.ClientSize.Width;
				}
			}

			// Token: 0x17000921 RID: 2337
			// (get) Token: 0x06002B38 RID: 11064 RVA: 0x00102D90 File Offset: 0x00100F90
			private Brush WatermarkBrush
			{
				get
				{
					if (this._watermarkBrush == null)
					{
						Color window = SystemColors.Window;
						Color windowText = SystemColors.WindowText;
						Color color = Color.FromArgb((int)((short)((double)windowText.R * 0.3 + (double)window.R * 0.7)), (int)((short)((double)windowText.G * 0.3 + (double)window.G * 0.7)), (int)((short)((double)windowText.B * 0.3 + (double)window.B * 0.7)));
						this._watermarkBrush = new SolidBrush(color);
					}
					return this._watermarkBrush;
				}
			}

			// Token: 0x06002B39 RID: 11065 RVA: 0x00102E40 File Offset: 0x00101040
			protected override void WndProc(ref Message m)
			{
				base.WndProc(ref m);
				int msg = m.Msg;
				if (msg == 15 && this.ShouldShowWatermark)
				{
					using (Graphics graphics = base.CreateGraphics())
					{
						graphics.DrawString(SR.GetString("MultilineStringEditorWatermark"), this.Font, this.WatermarkBrush, new RectangleF(0f, 0f, (float)base.ClientSize.Width, (float)base.ClientSize.Height), this._watermarkFormat);
					}
				}
			}

			// Token: 0x04001E06 RID: 7686
			private IWindowsFormsEditorService _editorService;

			// Token: 0x04001E07 RID: 7687
			private bool _editing;

			// Token: 0x04001E08 RID: 7688
			private bool _escapePressed;

			// Token: 0x04001E09 RID: 7689
			private bool _ctrlEnterPressed;

			// Token: 0x04001E0A RID: 7690
			private SolidBrush _watermarkBrush;

			// Token: 0x04001E0B RID: 7691
			private Hashtable _fallbackFonts;

			// Token: 0x04001E0C RID: 7692
			private bool _firstTimeResizeToContent = true;

			// Token: 0x04001E0D RID: 7693
			private readonly StringFormat _watermarkFormat;

			// Token: 0x04001E0E RID: 7694
			private const int _caretPadding = 3;

			// Token: 0x04001E0F RID: 7695
			private const int _workAreaPadding = 16;

			// Token: 0x04001E10 RID: 7696
			private bool _contentsResizedRaised;

			// Token: 0x04001E11 RID: 7697
			private Size _minimumSize = Size.Empty;

			// Token: 0x04001E12 RID: 7698
			private Size _watermarkSize = Size.Empty;
		}

		// Token: 0x02000493 RID: 1171
		private class OleCallback : UnsafeNativeMethods.IRichTextBoxOleCallback
		{
			// Token: 0x17000922 RID: 2338
			// (get) Token: 0x06002B3A RID: 11066 RVA: 0x00102EDC File Offset: 0x001010DC
			private static TraceSwitch RichTextDbg
			{
				get
				{
					if (MultilineStringEditor.OleCallback.richTextDbg == null)
					{
						MultilineStringEditor.OleCallback.richTextDbg = new TraceSwitch("RichTextDbg", "Debug info about RichTextBox");
					}
					return MultilineStringEditor.OleCallback.richTextDbg;
				}
			}

			// Token: 0x06002B3B RID: 11067 RVA: 0x00102EFE File Offset: 0x001010FE
			internal OleCallback(RichTextBox owner)
			{
				this.owner = owner;
			}

			// Token: 0x06002B3C RID: 11068 RVA: 0x00102F10 File Offset: 0x00101110
			public int GetNewStorage(out UnsafeNativeMethods.IStorage storage)
			{
				UnsafeNativeMethods.ILockBytes iLockBytes = UnsafeNativeMethods.CreateILockBytesOnHGlobal(NativeMethods.NullHandleRef, true);
				storage = UnsafeNativeMethods.StgCreateDocfileOnILockBytes(iLockBytes, 4114, 0);
				return 0;
			}

			// Token: 0x06002B3D RID: 11069 RVA: 0x0005154C File Offset: 0x0004F74C
			public int GetInPlaceContext(IntPtr lplpFrame, IntPtr lplpDoc, IntPtr lpFrameInfo)
			{
				return -2147467263;
			}

			// Token: 0x06002B3E RID: 11070 RVA: 0x0000445B File Offset: 0x0000265B
			public int ShowContainerUI(int fShow)
			{
				return 0;
			}

			// Token: 0x06002B3F RID: 11071 RVA: 0x00102F38 File Offset: 0x00101138
			public int QueryInsertObject(ref Guid lpclsid, IntPtr lpstg, int cp)
			{
				if (this.unrestricted)
				{
					return 0;
				}
				Guid a = default(Guid);
				int hr = UnsafeNativeMethods.ReadClassStg(new HandleRef(null, lpstg), ref a);
				if (!NativeMethods.Succeeded(hr))
				{
					return 1;
				}
				if (a == Guid.Empty)
				{
					a = lpclsid;
				}
				string a2 = a.ToString().ToUpper(CultureInfo.InvariantCulture);
				if (a2 == "00000315-0000-0000-C000-000000000046" || a2 == "00000316-0000-0000-C000-000000000046" || a2 == "00000319-0000-0000-C000-000000000046" || a2 == "0003000A-0000-0000-C000-000000000046")
				{
					return 0;
				}
				return 1;
			}

			// Token: 0x06002B40 RID: 11072 RVA: 0x0000445B File Offset: 0x0000265B
			public int DeleteObject(IntPtr lpoleobj)
			{
				return 0;
			}

			// Token: 0x06002B41 RID: 11073 RVA: 0x00102FD4 File Offset: 0x001011D4
			public int QueryAcceptData(System.Runtime.InteropServices.ComTypes.IDataObject lpdataobj, IntPtr lpcfFormat, int reco, int fReally, IntPtr hMetaPict)
			{
				if (reco != 0)
				{
					return -2147467263;
				}
				DataObject dataObject = new DataObject(lpdataobj);
				if (dataObject != null && (dataObject.GetDataPresent(DataFormats.Text) || dataObject.GetDataPresent(DataFormats.UnicodeText)))
				{
					return 0;
				}
				return -2147467259;
			}

			// Token: 0x06002B42 RID: 11074 RVA: 0x0005154C File Offset: 0x0004F74C
			public int ContextSensitiveHelp(int fEnterMode)
			{
				return -2147467263;
			}

			// Token: 0x06002B43 RID: 11075 RVA: 0x0005154C File Offset: 0x0004F74C
			public int GetClipboardData(NativeMethods.CHARRANGE lpchrg, int reco, IntPtr lplpdataobj)
			{
				return -2147467263;
			}

			// Token: 0x06002B44 RID: 11076 RVA: 0x00103015 File Offset: 0x00101215
			public int GetDragDropEffect(bool fDrag, int grfKeyState, ref int pdwEffect)
			{
				pdwEffect = 0;
				return 0;
			}

			// Token: 0x06002B45 RID: 11077 RVA: 0x0010301C File Offset: 0x0010121C
			public int GetContextMenu(short seltype, IntPtr lpoleobj, NativeMethods.CHARRANGE lpchrg, out IntPtr hmenu)
			{
				ContextMenu contextMenu = new TextBox
				{
					Visible = true
				}.ContextMenu;
				if (contextMenu == null || !this.owner.ShortcutsEnabled)
				{
					hmenu = IntPtr.Zero;
				}
				else
				{
					hmenu = contextMenu.Handle;
				}
				return 0;
			}

			// Token: 0x04001E13 RID: 7699
			private RichTextBox owner;

			// Token: 0x04001E14 RID: 7700
			private bool unrestricted;

			// Token: 0x04001E15 RID: 7701
			private static TraceSwitch richTextDbg;
		}
	}
}
