using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Security;
using System.Security.Permissions;
using System.Text;
using System.Windows.Forms.Layout;
using Microsoft.Win32;

namespace System.Windows.Forms
{
	// Token: 0x02000347 RID: 839
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[ComVisible(true)]
	[Docking(DockingBehavior.Ask)]
	[Designer("System.Windows.Forms.Design.RichTextBoxDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[SRDescription("DescriptionRichTextBox")]
	public class RichTextBox : TextBoxBase
	{
		// Token: 0x17000CFB RID: 3323
		// (get) Token: 0x060035F0 RID: 13808 RVA: 0x000F394A File Offset: 0x000F1B4A
		private static TraceSwitch RichTextDbg
		{
			get
			{
				if (RichTextBox.richTextDbg == null)
				{
					RichTextBox.richTextDbg = new TraceSwitch("RichTextDbg", "Debug info about RichTextBox");
				}
				return RichTextBox.richTextDbg;
			}
		}

		// Token: 0x060035F1 RID: 13809 RVA: 0x000F396C File Offset: 0x000F1B6C
		public RichTextBox()
		{
			this.InConstructor = true;
			this.richTextBoxFlags[RichTextBox.autoWordSelectionSection] = 0;
			this.DetectUrls = true;
			this.ScrollBars = RichTextBoxScrollBars.Both;
			this.RichTextShortcutsEnabled = true;
			this.MaxLength = int.MaxValue;
			this.Multiline = true;
			this.AutoSize = false;
			this.curSelStart = (this.curSelEnd = (int)(this.curSelType = -1));
			this.InConstructor = false;
		}

		// Token: 0x17000CFC RID: 3324
		// (get) Token: 0x060035F2 RID: 13810 RVA: 0x000F39FB File Offset: 0x000F1BFB
		// (set) Token: 0x060035F3 RID: 13811 RVA: 0x000F3A10 File Offset: 0x000F1C10
		[Browsable(false)]
		public override bool AllowDrop
		{
			get
			{
				return this.richTextBoxFlags[RichTextBox.allowOleDropSection] != 0;
			}
			set
			{
				if (value)
				{
					try
					{
						IntSecurity.ClipboardRead.Demand();
					}
					catch (Exception innerException)
					{
						throw new InvalidOperationException(SR.GetString("DragDropRegFailed"), innerException);
					}
				}
				this.richTextBoxFlags[RichTextBox.allowOleDropSection] = (value ? 1 : 0);
				this.UpdateOleCallback();
			}
		}

		// Token: 0x17000CFD RID: 3325
		// (get) Token: 0x060035F4 RID: 13812 RVA: 0x000F3A6C File Offset: 0x000F1C6C
		// (set) Token: 0x060035F5 RID: 13813 RVA: 0x000F3A81 File Offset: 0x000F1C81
		internal bool AllowOleObjects
		{
			get
			{
				return this.richTextBoxFlags[RichTextBox.allowOleObjectsSection] != 0;
			}
			set
			{
				this.richTextBoxFlags[RichTextBox.allowOleObjectsSection] = (value ? 1 : 0);
			}
		}

		// Token: 0x17000CFE RID: 3326
		// (get) Token: 0x060035F6 RID: 13814 RVA: 0x000F3A9A File Offset: 0x000F1C9A
		// (set) Token: 0x060035F7 RID: 13815 RVA: 0x000F3AA2 File Offset: 0x000F1CA2
		[DefaultValue(false)]
		[RefreshProperties(RefreshProperties.Repaint)]
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		public override bool AutoSize
		{
			get
			{
				return base.AutoSize;
			}
			set
			{
				base.AutoSize = value;
			}
		}

		// Token: 0x17000CFF RID: 3327
		// (get) Token: 0x060035F8 RID: 13816 RVA: 0x000F3AAB File Offset: 0x000F1CAB
		// (set) Token: 0x060035F9 RID: 13817 RVA: 0x000F3AC0 File Offset: 0x000F1CC0
		[SRCategory("CatBehavior")]
		[DefaultValue(false)]
		[SRDescription("RichTextBoxAutoWordSelection")]
		public bool AutoWordSelection
		{
			get
			{
				return this.richTextBoxFlags[RichTextBox.autoWordSelectionSection] != 0;
			}
			set
			{
				this.richTextBoxFlags[RichTextBox.autoWordSelectionSection] = (value ? 1 : 0);
				if (base.IsHandleCreated)
				{
					base.SendMessage(1101, value ? 2 : 4, 1);
				}
			}
		}

		// Token: 0x17000D00 RID: 3328
		// (get) Token: 0x060035FA RID: 13818 RVA: 0x000F3AF5 File Offset: 0x000F1CF5
		// (set) Token: 0x060035FB RID: 13819 RVA: 0x000F3AFD File Offset: 0x000F1CFD
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override Image BackgroundImage
		{
			get
			{
				return base.BackgroundImage;
			}
			set
			{
				base.BackgroundImage = value;
			}
		}

		// Token: 0x14000289 RID: 649
		// (add) Token: 0x060035FC RID: 13820 RVA: 0x000F3B06 File Offset: 0x000F1D06
		// (remove) Token: 0x060035FD RID: 13821 RVA: 0x000F3B0F File Offset: 0x000F1D0F
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler BackgroundImageChanged
		{
			add
			{
				base.BackgroundImageChanged += value;
			}
			remove
			{
				base.BackgroundImageChanged -= value;
			}
		}

		// Token: 0x17000D01 RID: 3329
		// (get) Token: 0x060035FE RID: 13822 RVA: 0x000F3B18 File Offset: 0x000F1D18
		// (set) Token: 0x060035FF RID: 13823 RVA: 0x000F3B20 File Offset: 0x000F1D20
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override ImageLayout BackgroundImageLayout
		{
			get
			{
				return base.BackgroundImageLayout;
			}
			set
			{
				base.BackgroundImageLayout = value;
			}
		}

		// Token: 0x1400028A RID: 650
		// (add) Token: 0x06003600 RID: 13824 RVA: 0x000F3B29 File Offset: 0x000F1D29
		// (remove) Token: 0x06003601 RID: 13825 RVA: 0x000F3B32 File Offset: 0x000F1D32
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler BackgroundImageLayoutChanged
		{
			add
			{
				base.BackgroundImageLayoutChanged += value;
			}
			remove
			{
				base.BackgroundImageLayoutChanged -= value;
			}
		}

		// Token: 0x17000D02 RID: 3330
		// (get) Token: 0x06003602 RID: 13826 RVA: 0x000F3B3B File Offset: 0x000F1D3B
		// (set) Token: 0x06003603 RID: 13827 RVA: 0x000F3B44 File Offset: 0x000F1D44
		[SRCategory("CatBehavior")]
		[DefaultValue(0)]
		[Localizable(true)]
		[SRDescription("RichTextBoxBulletIndent")]
		public int BulletIndent
		{
			get
			{
				return this.bulletIndent;
			}
			set
			{
				if (value < 0)
				{
					throw new ArgumentOutOfRangeException("BulletIndent", SR.GetString("InvalidArgument", new object[]
					{
						"BulletIndent",
						value.ToString(CultureInfo.CurrentCulture)
					}));
				}
				this.bulletIndent = value;
				if (base.IsHandleCreated && this.SelectionBullet)
				{
					this.SelectionBullet = true;
				}
			}
		}

		// Token: 0x17000D03 RID: 3331
		// (get) Token: 0x06003604 RID: 13828 RVA: 0x000F3BA5 File Offset: 0x000F1DA5
		// (set) Token: 0x06003605 RID: 13829 RVA: 0x000F3BBA File Offset: 0x000F1DBA
		private bool CallOnContentsResized
		{
			get
			{
				return this.richTextBoxFlags[RichTextBox.callOnContentsResizedSection] != 0;
			}
			set
			{
				this.richTextBoxFlags[RichTextBox.callOnContentsResizedSection] = (value ? 1 : 0);
			}
		}

		// Token: 0x17000D04 RID: 3332
		// (get) Token: 0x06003606 RID: 13830 RVA: 0x000F3BD3 File Offset: 0x000F1DD3
		internal override bool CanRaiseTextChangedEvent
		{
			get
			{
				return !this.SuppressTextChangedEvent;
			}
		}

		// Token: 0x17000D05 RID: 3333
		// (get) Token: 0x06003607 RID: 13831 RVA: 0x000F3BE0 File Offset: 0x000F1DE0
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[SRDescription("RichTextBoxCanRedoDescr")]
		public bool CanRedo
		{
			get
			{
				return base.IsHandleCreated && (int)((long)base.SendMessage(1109, 0, 0)) != 0;
			}
		}

		// Token: 0x06003608 RID: 13832 RVA: 0x000F3C0F File Offset: 0x000F1E0F
		protected override AccessibleObject CreateAccessibilityInstance()
		{
			if (!AccessibilityImprovements.Level5)
			{
				return base.CreateAccessibilityInstance();
			}
			return new Control.ControlAccessibleObject(this);
		}

		// Token: 0x17000D06 RID: 3334
		// (get) Token: 0x06003609 RID: 13833 RVA: 0x000F3C28 File Offset: 0x000F1E28
		protected override CreateParams CreateParams
		{
			[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
			get
			{
				if (RichTextBox.moduleHandle == IntPtr.Zero)
				{
					string text = LocalAppContextSwitches.DoNotLoadLatestRichEditControl ? "RichEd20.DLL" : "MsftEdit.DLL";
					RichTextBox.moduleHandle = UnsafeNativeMethods.LoadLibraryFromSystemPathIfAvailable(text);
					int lastWin32Error = Marshal.GetLastWin32Error();
					if ((long)RichTextBox.moduleHandle < 32L)
					{
						throw new Win32Exception(lastWin32Error, SR.GetString("LoadDLLError", new object[]
						{
							text
						}));
					}
					StringBuilder moduleFileNameLongPath = UnsafeNativeMethods.GetModuleFileNameLongPath(new HandleRef(null, RichTextBox.moduleHandle));
					string text2 = moduleFileNameLongPath.ToString();
					new FileIOPermission(FileIOPermissionAccess.Read, text2).Assert();
					FileVersionInfo versionInfo;
					try
					{
						versionInfo = FileVersionInfo.GetVersionInfo(text2);
					}
					finally
					{
						CodeAccessPermission.RevertAssert();
					}
					int num;
					if (versionInfo != null && !string.IsNullOrEmpty(versionInfo.ProductVersion) && int.TryParse(versionInfo.ProductVersion[0].ToString(), out num))
					{
						RichTextBox.richEditMajorVersion = num;
					}
				}
				CreateParams createParams = base.CreateParams;
				if (Marshal.SystemDefaultCharSize == 1)
				{
					createParams.ClassName = (LocalAppContextSwitches.DoNotLoadLatestRichEditControl ? "RichEdit20A" : "RICHEDIT50A");
				}
				else
				{
					createParams.ClassName = (LocalAppContextSwitches.DoNotLoadLatestRichEditControl ? "RichEdit20W" : "RICHEDIT50W");
				}
				if (this.Multiline)
				{
					if ((this.ScrollBars & RichTextBoxScrollBars.Horizontal) != RichTextBoxScrollBars.None && !base.WordWrap)
					{
						createParams.Style |= 1048576;
						if ((this.ScrollBars & (RichTextBoxScrollBars)16) != RichTextBoxScrollBars.None)
						{
							createParams.Style |= 8192;
						}
					}
					if ((this.ScrollBars & RichTextBoxScrollBars.Vertical) != RichTextBoxScrollBars.None)
					{
						createParams.Style |= 2097152;
						if ((this.ScrollBars & (RichTextBoxScrollBars)16) != RichTextBoxScrollBars.None)
						{
							createParams.Style |= 8192;
						}
					}
				}
				if (BorderStyle.FixedSingle == base.BorderStyle && (createParams.Style & 8388608) != 0)
				{
					createParams.Style &= -8388609;
					createParams.ExStyle |= 512;
				}
				return createParams;
			}
		}

		// Token: 0x17000D07 RID: 3335
		// (get) Token: 0x0600360A RID: 13834 RVA: 0x000F3E18 File Offset: 0x000F2018
		// (set) Token: 0x0600360B RID: 13835 RVA: 0x000F3E30 File Offset: 0x000F2030
		[SRCategory("CatBehavior")]
		[DefaultValue(true)]
		[SRDescription("RichTextBoxDetectURLs")]
		public bool DetectUrls
		{
			get
			{
				return this.richTextBoxFlags[RichTextBox.autoUrlDetectSection] != 0;
			}
			set
			{
				if (value != this.DetectUrls)
				{
					this.richTextBoxFlags[RichTextBox.autoUrlDetectSection] = (value ? 1 : 0);
					if (base.IsHandleCreated)
					{
						base.SendMessage(1115, value ? 1 : 0, 0);
						base.RecreateHandle();
					}
				}
			}
		}

		// Token: 0x17000D08 RID: 3336
		// (get) Token: 0x0600360C RID: 13836 RVA: 0x000F3E7F File Offset: 0x000F207F
		protected override Size DefaultSize
		{
			get
			{
				return new Size(100, 96);
			}
		}

		// Token: 0x17000D09 RID: 3337
		// (get) Token: 0x0600360D RID: 13837 RVA: 0x000F3E8A File Offset: 0x000F208A
		// (set) Token: 0x0600360E RID: 13838 RVA: 0x000F3EA0 File Offset: 0x000F20A0
		[SRCategory("CatBehavior")]
		[DefaultValue(false)]
		[SRDescription("RichTextBoxEnableAutoDragDrop")]
		public bool EnableAutoDragDrop
		{
			get
			{
				return this.richTextBoxFlags[RichTextBox.enableAutoDragDropSection] != 0;
			}
			set
			{
				if (value)
				{
					try
					{
						IntSecurity.ClipboardRead.Demand();
					}
					catch (Exception innerException)
					{
						throw new InvalidOperationException(SR.GetString("DragDropRegFailed"), innerException);
					}
				}
				this.richTextBoxFlags[RichTextBox.enableAutoDragDropSection] = (value ? 1 : 0);
				this.UpdateOleCallback();
			}
		}

		// Token: 0x17000D0A RID: 3338
		// (get) Token: 0x0600360F RID: 13839 RVA: 0x000F3EFC File Offset: 0x000F20FC
		// (set) Token: 0x06003610 RID: 13840 RVA: 0x000F3F04 File Offset: 0x000F2104
		public override Color ForeColor
		{
			get
			{
				return base.ForeColor;
			}
			set
			{
				if (base.IsHandleCreated)
				{
					if (this.InternalSetForeColor(value))
					{
						base.ForeColor = value;
						return;
					}
				}
				else
				{
					base.ForeColor = value;
				}
			}
		}

		// Token: 0x17000D0B RID: 3339
		// (get) Token: 0x06003611 RID: 13841 RVA: 0x0001A272 File Offset: 0x00018472
		// (set) Token: 0x06003612 RID: 13842 RVA: 0x000F3F28 File Offset: 0x000F2128
		public override Font Font
		{
			get
			{
				return base.Font;
			}
			set
			{
				if (base.IsHandleCreated)
				{
					if (SafeNativeMethods.GetWindowTextLength(new HandleRef(this, base.Handle)) > 0)
					{
						if (value == null)
						{
							base.Font = null;
							this.SetCharFormatFont(false, this.Font);
							return;
						}
						try
						{
							Font charFormatFont = this.GetCharFormatFont(false);
							if (charFormatFont == null || !charFormatFont.Equals(value))
							{
								this.SetCharFormatFont(false, value);
								this.CallOnContentsResized = true;
								base.Font = this.GetCharFormatFont(false);
							}
							return;
						}
						finally
						{
							this.CallOnContentsResized = false;
						}
					}
					base.Font = value;
					return;
				}
				base.Font = value;
			}
		}

		// Token: 0x06003613 RID: 13843 RVA: 0x000F3FC4 File Offset: 0x000F21C4
		internal override Size GetPreferredSizeCore(Size proposedConstraints)
		{
			Size empty = Size.Empty;
			if (!base.WordWrap && this.Multiline && (this.ScrollBars & RichTextBoxScrollBars.Horizontal) != RichTextBoxScrollBars.None)
			{
				empty.Height += SystemInformation.HorizontalScrollBarHeight;
			}
			if (this.Multiline && (this.ScrollBars & RichTextBoxScrollBars.Vertical) != RichTextBoxScrollBars.None)
			{
				empty.Width += SystemInformation.VerticalScrollBarWidth;
			}
			proposedConstraints -= empty;
			Size preferredSizeCore = base.GetPreferredSizeCore(proposedConstraints);
			return preferredSizeCore + empty;
		}

		// Token: 0x17000D0C RID: 3340
		// (get) Token: 0x06003614 RID: 13844 RVA: 0x000F4041 File Offset: 0x000F2241
		// (set) Token: 0x06003615 RID: 13845 RVA: 0x000F4056 File Offset: 0x000F2256
		private bool InConstructor
		{
			get
			{
				return this.richTextBoxFlags[RichTextBox.fInCtorSection] != 0;
			}
			set
			{
				this.richTextBoxFlags[RichTextBox.fInCtorSection] = (value ? 1 : 0);
			}
		}

		// Token: 0x17000D0D RID: 3341
		// (get) Token: 0x06003616 RID: 13846 RVA: 0x000F4070 File Offset: 0x000F2270
		// (set) Token: 0x06003617 RID: 13847 RVA: 0x000F40AD File Offset: 0x000F22AD
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public RichTextBoxLanguageOptions LanguageOption
		{
			get
			{
				RichTextBoxLanguageOptions result;
				if (base.IsHandleCreated)
				{
					result = (RichTextBoxLanguageOptions)((int)UnsafeNativeMethods.SendMessage(new HandleRef(this, base.Handle), 1145, 0, 0));
				}
				else
				{
					result = this.languageOption;
				}
				return result;
			}
			set
			{
				if (this.LanguageOption != value)
				{
					this.languageOption = value;
					if (base.IsHandleCreated)
					{
						UnsafeNativeMethods.SendMessage(new HandleRef(this, base.Handle), 1144, 0, (int)value);
					}
				}
			}
		}

		// Token: 0x17000D0E RID: 3342
		// (get) Token: 0x06003618 RID: 13848 RVA: 0x000F40E0 File Offset: 0x000F22E0
		// (set) Token: 0x06003619 RID: 13849 RVA: 0x000F40F5 File Offset: 0x000F22F5
		private bool LinkCursor
		{
			get
			{
				return this.richTextBoxFlags[RichTextBox.linkcursorSection] != 0;
			}
			set
			{
				this.richTextBoxFlags[RichTextBox.linkcursorSection] = (value ? 1 : 0);
			}
		}

		// Token: 0x17000D0F RID: 3343
		// (get) Token: 0x0600361A RID: 13850 RVA: 0x000D3F87 File Offset: 0x000D2187
		// (set) Token: 0x0600361B RID: 13851 RVA: 0x000F410E File Offset: 0x000F230E
		[DefaultValue(2147483647)]
		public override int MaxLength
		{
			get
			{
				return base.MaxLength;
			}
			set
			{
				base.MaxLength = value;
			}
		}

		// Token: 0x17000D10 RID: 3344
		// (get) Token: 0x0600361C RID: 13852 RVA: 0x000F4117 File Offset: 0x000F2317
		// (set) Token: 0x0600361D RID: 13853 RVA: 0x000F411F File Offset: 0x000F231F
		[DefaultValue(true)]
		public override bool Multiline
		{
			get
			{
				return base.Multiline;
			}
			set
			{
				base.Multiline = value;
			}
		}

		// Token: 0x17000D11 RID: 3345
		// (get) Token: 0x0600361E RID: 13854 RVA: 0x000F4128 File Offset: 0x000F2328
		// (set) Token: 0x0600361F RID: 13855 RVA: 0x000F413D File Offset: 0x000F233D
		private bool ProtectedError
		{
			get
			{
				return this.richTextBoxFlags[RichTextBox.protectedErrorSection] != 0;
			}
			set
			{
				this.richTextBoxFlags[RichTextBox.protectedErrorSection] = (value ? 1 : 0);
			}
		}

		// Token: 0x17000D12 RID: 3346
		// (get) Token: 0x06003620 RID: 13856 RVA: 0x000F4158 File Offset: 0x000F2358
		[SRCategory("CatBehavior")]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[SRDescription("RichTextBoxRedoActionNameDescr")]
		public string RedoActionName
		{
			get
			{
				if (!this.CanRedo)
				{
					return "";
				}
				int actionID = (int)((long)base.SendMessage(1111, 0, 0));
				return this.GetEditorActionName(actionID);
			}
		}

		// Token: 0x17000D13 RID: 3347
		// (get) Token: 0x06003621 RID: 13857 RVA: 0x000F418E File Offset: 0x000F238E
		// (set) Token: 0x06003622 RID: 13858 RVA: 0x000F41A3 File Offset: 0x000F23A3
		[DefaultValue(true)]
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool RichTextShortcutsEnabled
		{
			get
			{
				return this.richTextBoxFlags[RichTextBox.richTextShortcutsEnabledSection] != 0;
			}
			set
			{
				if (RichTextBox.shortcutsToDisable == null)
				{
					RichTextBox.shortcutsToDisable = new int[]
					{
						131148,
						131154,
						131141,
						131146
					};
				}
				this.richTextBoxFlags[RichTextBox.richTextShortcutsEnabledSection] = (value ? 1 : 0);
			}
		}

		// Token: 0x17000D14 RID: 3348
		// (get) Token: 0x06003623 RID: 13859 RVA: 0x000F41D9 File Offset: 0x000F23D9
		// (set) Token: 0x06003624 RID: 13860 RVA: 0x000F41E4 File Offset: 0x000F23E4
		[SRCategory("CatBehavior")]
		[DefaultValue(0)]
		[Localizable(true)]
		[SRDescription("RichTextBoxRightMargin")]
		public int RightMargin
		{
			get
			{
				return this.rightMargin;
			}
			set
			{
				if (this.rightMargin != value)
				{
					if (value < 0)
					{
						throw new ArgumentOutOfRangeException("RightMargin", SR.GetString("InvalidLowBoundArgumentEx", new object[]
						{
							"RightMargin",
							value.ToString(CultureInfo.CurrentCulture),
							0.ToString(CultureInfo.CurrentCulture)
						}));
					}
					this.rightMargin = value;
					if (value == 0)
					{
						base.RecreateHandle();
						return;
					}
					if (base.IsHandleCreated)
					{
						IntPtr intPtr = UnsafeNativeMethods.CreateIC("DISPLAY", null, null, new HandleRef(null, IntPtr.Zero));
						try
						{
							base.SendMessage(1096, intPtr, (IntPtr)RichTextBox.Pixel2Twip(intPtr, value, true));
						}
						finally
						{
							if (intPtr != IntPtr.Zero)
							{
								UnsafeNativeMethods.DeleteDC(new HandleRef(null, intPtr));
							}
						}
					}
				}
			}
		}

		// Token: 0x17000D15 RID: 3349
		// (get) Token: 0x06003625 RID: 13861 RVA: 0x000F42BC File Offset: 0x000F24BC
		// (set) Token: 0x06003626 RID: 13862 RVA: 0x000F42EC File Offset: 0x000F24EC
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[SRDescription("RichTextBoxRTF")]
		[RefreshProperties(RefreshProperties.All)]
		public string Rtf
		{
			get
			{
				if (base.IsHandleCreated)
				{
					return this.StreamOut(2);
				}
				if (this.textPlain != null)
				{
					this.ForceHandleCreate();
					return this.StreamOut(2);
				}
				return this.textRtf;
			}
			set
			{
				if (value == null)
				{
					value = "";
				}
				if (value.Equals(this.Rtf))
				{
					return;
				}
				this.ForceHandleCreate();
				this.textRtf = value;
				this.StreamIn(value, 2);
				if (this.CanRaiseTextChangedEvent)
				{
					this.OnTextChanged(EventArgs.Empty);
				}
			}
		}

		// Token: 0x17000D16 RID: 3350
		// (get) Token: 0x06003627 RID: 13863 RVA: 0x000F433A File Offset: 0x000F253A
		// (set) Token: 0x06003628 RID: 13864 RVA: 0x000F434C File Offset: 0x000F254C
		[SRCategory("CatAppearance")]
		[DefaultValue(RichTextBoxScrollBars.Both)]
		[Localizable(true)]
		[SRDescription("RichTextBoxScrollBars")]
		public RichTextBoxScrollBars ScrollBars
		{
			get
			{
				return (RichTextBoxScrollBars)this.richTextBoxFlags[RichTextBox.scrollBarsSection];
			}
			set
			{
				if (!ClientUtils.IsEnumValid_NotSequential(value, (int)value, new int[]
				{
					3,
					0,
					1,
					2,
					17,
					18,
					19
				}))
				{
					throw new InvalidEnumArgumentException("value", (int)value, typeof(RichTextBoxScrollBars));
				}
				if (value != this.ScrollBars)
				{
					using (LayoutTransaction.CreateTransactionIf(this.AutoSize, this.ParentInternal, this, PropertyNames.ScrollBars))
					{
						this.richTextBoxFlags[RichTextBox.scrollBarsSection] = (int)value;
						base.RecreateHandle();
					}
				}
			}
		}

		// Token: 0x17000D17 RID: 3351
		// (get) Token: 0x06003629 RID: 13865 RVA: 0x000F43E4 File Offset: 0x000F25E4
		// (set) Token: 0x0600362A RID: 13866 RVA: 0x000F4458 File Offset: 0x000F2658
		[DefaultValue(HorizontalAlignment.Left)]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[SRDescription("RichTextBoxSelAlignment")]
		public HorizontalAlignment SelectionAlignment
		{
			get
			{
				HorizontalAlignment result = HorizontalAlignment.Left;
				this.ForceHandleCreate();
				NativeMethods.PARAFORMAT paraformat = new NativeMethods.PARAFORMAT();
				paraformat.rgxTabs = new int[32];
				UnsafeNativeMethods.SendMessage(new HandleRef(this, base.Handle), 1085, 0, paraformat);
				if ((8 & paraformat.dwMask) != 0)
				{
					switch (paraformat.wAlignment)
					{
					case 1:
						result = HorizontalAlignment.Left;
						break;
					case 2:
						result = HorizontalAlignment.Right;
						break;
					case 3:
						result = HorizontalAlignment.Center;
						break;
					}
				}
				return result;
			}
			set
			{
				if (!ClientUtils.IsEnumValid(value, (int)value, 0, 2))
				{
					throw new InvalidEnumArgumentException("value", (int)value, typeof(HorizontalAlignment));
				}
				this.ForceHandleCreate();
				NativeMethods.PARAFORMAT paraformat = new NativeMethods.PARAFORMAT();
				paraformat.dwMask = 8;
				switch (value)
				{
				case HorizontalAlignment.Left:
					paraformat.wAlignment = 1;
					break;
				case HorizontalAlignment.Right:
					paraformat.wAlignment = 2;
					break;
				case HorizontalAlignment.Center:
					paraformat.wAlignment = 3;
					break;
				}
				UnsafeNativeMethods.SendMessage(new HandleRef(this, base.Handle), 1095, 0, paraformat);
			}
		}

		// Token: 0x17000D18 RID: 3352
		// (get) Token: 0x0600362B RID: 13867 RVA: 0x000F44E4 File Offset: 0x000F26E4
		// (set) Token: 0x0600362C RID: 13868 RVA: 0x000F4544 File Offset: 0x000F2744
		[DefaultValue(false)]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[SRDescription("RichTextBoxSelBullet")]
		public bool SelectionBullet
		{
			get
			{
				RichTextBoxSelectionAttribute richTextBoxSelectionAttribute = RichTextBoxSelectionAttribute.None;
				this.ForceHandleCreate();
				NativeMethods.PARAFORMAT paraformat = new NativeMethods.PARAFORMAT();
				paraformat.rgxTabs = new int[32];
				UnsafeNativeMethods.SendMessage(new HandleRef(this, base.Handle), 1085, 0, paraformat);
				if ((32 & paraformat.dwMask) != 0)
				{
					if (1 == paraformat.wNumbering)
					{
						richTextBoxSelectionAttribute = RichTextBoxSelectionAttribute.All;
					}
					return richTextBoxSelectionAttribute == RichTextBoxSelectionAttribute.All;
				}
				return false;
			}
			set
			{
				this.ForceHandleCreate();
				NativeMethods.PARAFORMAT paraformat = new NativeMethods.PARAFORMAT();
				paraformat.dwMask = 36;
				if (!value)
				{
					paraformat.wNumbering = 0;
					paraformat.dxOffset = 0;
				}
				else
				{
					paraformat.wNumbering = 1;
					paraformat.dxOffset = RichTextBox.Pixel2Twip(IntPtr.Zero, this.bulletIndent, true);
				}
				UnsafeNativeMethods.SendMessage(new HandleRef(this, base.Handle), 1095, 0, paraformat);
			}
		}

		// Token: 0x17000D19 RID: 3353
		// (get) Token: 0x0600362D RID: 13869 RVA: 0x000F45B0 File Offset: 0x000F27B0
		// (set) Token: 0x0600362E RID: 13870 RVA: 0x000F45F8 File Offset: 0x000F27F8
		[DefaultValue(0)]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[SRDescription("RichTextBoxSelCharOffset")]
		public int SelectionCharOffset
		{
			get
			{
				this.ForceHandleCreate();
				NativeMethods.CHARFORMATA charFormat = this.GetCharFormat(true);
				int yOffset;
				if ((charFormat.dwMask & 268435456) != 0)
				{
					yOffset = charFormat.yOffset;
				}
				else
				{
					yOffset = charFormat.yOffset;
				}
				return RichTextBox.Twip2Pixel(IntPtr.Zero, yOffset, false);
			}
			set
			{
				if (value > 2000 || value < -2000)
				{
					throw new ArgumentOutOfRangeException("SelectionCharOffset", SR.GetString("InvalidBoundArgument", new object[]
					{
						"SelectionCharOffset",
						value,
						-2000,
						2000
					}));
				}
				this.ForceHandleCreate();
				NativeMethods.CHARFORMATA charformata = new NativeMethods.CHARFORMATA();
				charformata.dwMask = 268435456;
				charformata.yOffset = RichTextBox.Pixel2Twip(IntPtr.Zero, value, false);
				UnsafeNativeMethods.SendMessage(new HandleRef(this, base.Handle), 1092, 1, charformata);
			}
		}

		// Token: 0x17000D1A RID: 3354
		// (get) Token: 0x0600362F RID: 13871 RVA: 0x000F46A0 File Offset: 0x000F28A0
		// (set) Token: 0x06003630 RID: 13872 RVA: 0x000F46DC File Offset: 0x000F28DC
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[SRDescription("RichTextBoxSelColor")]
		public Color SelectionColor
		{
			get
			{
				Color result = Color.Empty;
				this.ForceHandleCreate();
				NativeMethods.CHARFORMATA charFormat = this.GetCharFormat(true);
				if ((charFormat.dwMask & 1073741824) != 0)
				{
					result = ColorTranslator.FromOle(charFormat.crTextColor);
				}
				return result;
			}
			set
			{
				this.ForceHandleCreate();
				NativeMethods.CHARFORMATA charFormat = this.GetCharFormat(true);
				charFormat.dwMask = 1073741824;
				charFormat.dwEffects = 0;
				charFormat.crTextColor = ColorTranslator.ToWin32(value);
				UnsafeNativeMethods.SendMessage(new HandleRef(this, base.Handle), 1092, 1, charFormat);
			}
		}

		// Token: 0x17000D1B RID: 3355
		// (get) Token: 0x06003631 RID: 13873 RVA: 0x000F4730 File Offset: 0x000F2930
		// (set) Token: 0x06003632 RID: 13874 RVA: 0x000F4790 File Offset: 0x000F2990
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[SRDescription("RichTextBoxSelBackColor")]
		public Color SelectionBackColor
		{
			get
			{
				Color result = Color.Empty;
				if (base.IsHandleCreated)
				{
					NativeMethods.CHARFORMAT2A charFormat = this.GetCharFormat2(true);
					if ((charFormat.dwEffects & 67108864) != 0)
					{
						result = this.BackColor;
					}
					else if ((charFormat.dwMask & 67108864) != 0)
					{
						result = ColorTranslator.FromOle(charFormat.crBackColor);
					}
				}
				else
				{
					result = this.selectionBackColorToSetOnHandleCreated;
				}
				return result;
			}
			set
			{
				this.selectionBackColorToSetOnHandleCreated = value;
				if (base.IsHandleCreated)
				{
					NativeMethods.CHARFORMAT2A charformat2A = new NativeMethods.CHARFORMAT2A();
					if (value == Color.Empty)
					{
						charformat2A.dwEffects = 67108864;
					}
					else
					{
						charformat2A.dwMask = 67108864;
						charformat2A.crBackColor = ColorTranslator.ToWin32(value);
					}
					UnsafeNativeMethods.SendMessage(new HandleRef(this, base.Handle), 1092, 1, charformat2A);
				}
			}
		}

		// Token: 0x17000D1C RID: 3356
		// (get) Token: 0x06003633 RID: 13875 RVA: 0x000F47FC File Offset: 0x000F29FC
		// (set) Token: 0x06003634 RID: 13876 RVA: 0x000F4805 File Offset: 0x000F2A05
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[SRDescription("RichTextBoxSelFont")]
		public Font SelectionFont
		{
			get
			{
				return this.GetCharFormatFont(true);
			}
			set
			{
				this.SetCharFormatFont(true, value);
			}
		}

		// Token: 0x17000D1D RID: 3357
		// (get) Token: 0x06003635 RID: 13877 RVA: 0x000F4810 File Offset: 0x000F2A10
		// (set) Token: 0x06003636 RID: 13878 RVA: 0x000F4870 File Offset: 0x000F2A70
		[DefaultValue(0)]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[SRDescription("RichTextBoxSelHangingIndent")]
		public int SelectionHangingIndent
		{
			get
			{
				int v = 0;
				this.ForceHandleCreate();
				NativeMethods.PARAFORMAT paraformat = new NativeMethods.PARAFORMAT();
				paraformat.rgxTabs = new int[32];
				UnsafeNativeMethods.SendMessage(new HandleRef(this, base.Handle), 1085, 0, paraformat);
				if ((4 & paraformat.dwMask) != 0)
				{
					v = paraformat.dxOffset;
				}
				return RichTextBox.Twip2Pixel(IntPtr.Zero, v, true);
			}
			set
			{
				this.ForceHandleCreate();
				NativeMethods.PARAFORMAT paraformat = new NativeMethods.PARAFORMAT();
				paraformat.dwMask = 4;
				paraformat.dxOffset = RichTextBox.Pixel2Twip(IntPtr.Zero, value, true);
				UnsafeNativeMethods.SendMessage(new HandleRef(this, base.Handle), 1095, 0, paraformat);
			}
		}

		// Token: 0x17000D1E RID: 3358
		// (get) Token: 0x06003637 RID: 13879 RVA: 0x000F48BC File Offset: 0x000F2ABC
		// (set) Token: 0x06003638 RID: 13880 RVA: 0x000F491C File Offset: 0x000F2B1C
		[DefaultValue(0)]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[SRDescription("RichTextBoxSelIndent")]
		public int SelectionIndent
		{
			get
			{
				int v = 0;
				this.ForceHandleCreate();
				NativeMethods.PARAFORMAT paraformat = new NativeMethods.PARAFORMAT();
				paraformat.rgxTabs = new int[32];
				UnsafeNativeMethods.SendMessage(new HandleRef(this, base.Handle), 1085, 0, paraformat);
				if ((1 & paraformat.dwMask) != 0)
				{
					v = paraformat.dxStartIndent;
				}
				return RichTextBox.Twip2Pixel(IntPtr.Zero, v, true);
			}
			set
			{
				this.ForceHandleCreate();
				NativeMethods.PARAFORMAT paraformat = new NativeMethods.PARAFORMAT();
				paraformat.dwMask = 1;
				paraformat.dxStartIndent = RichTextBox.Pixel2Twip(IntPtr.Zero, value, true);
				UnsafeNativeMethods.SendMessage(new HandleRef(this, base.Handle), 1095, 0, paraformat);
			}
		}

		// Token: 0x17000D1F RID: 3359
		// (get) Token: 0x06003639 RID: 13881 RVA: 0x000F4967 File Offset: 0x000F2B67
		// (set) Token: 0x0600363A RID: 13882 RVA: 0x000F4983 File Offset: 0x000F2B83
		[SRCategory("CatAppearance")]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[SRDescription("TextBoxSelectionLengthDescr")]
		public override int SelectionLength
		{
			get
			{
				if (!base.IsHandleCreated)
				{
					return base.SelectionLength;
				}
				return this.SelectedText.Length;
			}
			set
			{
				base.SelectionLength = value;
			}
		}

		// Token: 0x17000D20 RID: 3360
		// (get) Token: 0x0600363B RID: 13883 RVA: 0x000F498C File Offset: 0x000F2B8C
		// (set) Token: 0x0600363C RID: 13884 RVA: 0x000F49A1 File Offset: 0x000F2BA1
		[DefaultValue(false)]
		[SRDescription("RichTextBoxSelProtected")]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public bool SelectionProtected
		{
			get
			{
				this.ForceHandleCreate();
				return this.GetCharFormat(16, 16) == RichTextBoxSelectionAttribute.All;
			}
			set
			{
				this.ForceHandleCreate();
				this.SetCharFormat(16, value ? 16 : 0, RichTextBoxSelectionAttribute.All);
			}
		}

		// Token: 0x17000D21 RID: 3361
		// (get) Token: 0x0600363D RID: 13885 RVA: 0x00011A20 File Offset: 0x0000FC20
		internal override bool SelectionUsesDbcsOffsetsInWin9x
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000D22 RID: 3362
		// (get) Token: 0x0600363E RID: 13886 RVA: 0x000F49BB File Offset: 0x000F2BBB
		// (set) Token: 0x0600363F RID: 13887 RVA: 0x000F49CE File Offset: 0x000F2BCE
		[DefaultValue("")]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[SRDescription("RichTextBoxSelRTF")]
		public string SelectedRtf
		{
			get
			{
				this.ForceHandleCreate();
				return this.StreamOut(32770);
			}
			set
			{
				this.ForceHandleCreate();
				if (value == null)
				{
					value = "";
				}
				this.StreamIn(value, 32770);
			}
		}

		// Token: 0x17000D23 RID: 3363
		// (get) Token: 0x06003640 RID: 13888 RVA: 0x000F49EC File Offset: 0x000F2BEC
		// (set) Token: 0x06003641 RID: 13889 RVA: 0x000F4A4C File Offset: 0x000F2C4C
		[DefaultValue(0)]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[SRDescription("RichTextBoxSelRightIndent")]
		public int SelectionRightIndent
		{
			get
			{
				int v = 0;
				this.ForceHandleCreate();
				NativeMethods.PARAFORMAT paraformat = new NativeMethods.PARAFORMAT();
				paraformat.rgxTabs = new int[32];
				UnsafeNativeMethods.SendMessage(new HandleRef(this, base.Handle), 1085, 0, paraformat);
				if ((2 & paraformat.dwMask) != 0)
				{
					v = paraformat.dxRightIndent;
				}
				return RichTextBox.Twip2Pixel(IntPtr.Zero, v, true);
			}
			set
			{
				if (value < 0)
				{
					throw new ArgumentOutOfRangeException("SelectionRightIndent", SR.GetString("InvalidLowBoundArgumentEx", new object[]
					{
						"SelectionRightIndent",
						value,
						0
					}));
				}
				this.ForceHandleCreate();
				NativeMethods.PARAFORMAT paraformat = new NativeMethods.PARAFORMAT();
				paraformat.dwMask = 2;
				paraformat.dxRightIndent = RichTextBox.Pixel2Twip(IntPtr.Zero, value, true);
				UnsafeNativeMethods.SendMessage(new HandleRef(this, base.Handle), 1095, 0, paraformat);
			}
		}

		// Token: 0x17000D24 RID: 3364
		// (get) Token: 0x06003642 RID: 13890 RVA: 0x000F4AD0 File Offset: 0x000F2CD0
		// (set) Token: 0x06003643 RID: 13891 RVA: 0x000F4B58 File Offset: 0x000F2D58
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[SRDescription("RichTextBoxSelTabs")]
		public int[] SelectionTabs
		{
			get
			{
				int[] array = new int[0];
				this.ForceHandleCreate();
				NativeMethods.PARAFORMAT paraformat = new NativeMethods.PARAFORMAT();
				paraformat.rgxTabs = new int[32];
				UnsafeNativeMethods.SendMessage(new HandleRef(this, base.Handle), 1085, 0, paraformat);
				if ((16 & paraformat.dwMask) != 0)
				{
					array = new int[(int)paraformat.cTabCount];
					for (int i = 0; i < (int)paraformat.cTabCount; i++)
					{
						array[i] = RichTextBox.Twip2Pixel(IntPtr.Zero, paraformat.rgxTabs[i], true);
					}
				}
				return array;
			}
			set
			{
				if (value != null && value.Length > 32)
				{
					throw new ArgumentOutOfRangeException("SelectionTabs", SR.GetString("SelTabCountRange"));
				}
				this.ForceHandleCreate();
				NativeMethods.PARAFORMAT paraformat = new NativeMethods.PARAFORMAT();
				paraformat.rgxTabs = new int[32];
				UnsafeNativeMethods.SendMessage(new HandleRef(this, base.Handle), 1085, 0, paraformat);
				paraformat.cTabCount = (short)((value == null) ? 0 : value.Length);
				paraformat.dwMask = 16;
				for (int i = 0; i < (int)paraformat.cTabCount; i++)
				{
					paraformat.rgxTabs[i] = RichTextBox.Pixel2Twip(IntPtr.Zero, value[i], true);
				}
				UnsafeNativeMethods.SendMessage(new HandleRef(this, base.Handle), 1095, 0, paraformat);
			}
		}

		// Token: 0x17000D25 RID: 3365
		// (get) Token: 0x06003644 RID: 13892 RVA: 0x000F4C10 File Offset: 0x000F2E10
		// (set) Token: 0x06003645 RID: 13893 RVA: 0x000F4C30 File Offset: 0x000F2E30
		[DefaultValue("")]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[SRDescription("RichTextBoxSelText")]
		public override string SelectedText
		{
			get
			{
				this.ForceHandleCreate();
				return this.StreamOut(32785);
			}
			set
			{
				this.ForceHandleCreate();
				this.StreamIn(value, 32785);
			}
		}

		// Token: 0x17000D26 RID: 3366
		// (get) Token: 0x06003646 RID: 13894 RVA: 0x000F4C44 File Offset: 0x000F2E44
		[SRCategory("CatBehavior")]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[SRDescription("RichTextBoxSelTypeDescr")]
		public RichTextBoxSelectionTypes SelectionType
		{
			get
			{
				this.ForceHandleCreate();
				if (this.SelectionLength > 0)
				{
					return (RichTextBoxSelectionTypes)((long)base.SendMessage(1090, 0, 0));
				}
				return RichTextBoxSelectionTypes.Empty;
			}
		}

		// Token: 0x17000D27 RID: 3367
		// (get) Token: 0x06003647 RID: 13895 RVA: 0x000F4C77 File Offset: 0x000F2E77
		// (set) Token: 0x06003648 RID: 13896 RVA: 0x000F4C8C File Offset: 0x000F2E8C
		[SRCategory("CatBehavior")]
		[DefaultValue(false)]
		[SRDescription("RichTextBoxSelMargin")]
		public bool ShowSelectionMargin
		{
			get
			{
				return this.richTextBoxFlags[RichTextBox.showSelBarSection] != 0;
			}
			set
			{
				if (value != this.ShowSelectionMargin)
				{
					this.richTextBoxFlags[RichTextBox.showSelBarSection] = (value ? 1 : 0);
					if (base.IsHandleCreated)
					{
						base.SendMessage(1101, value ? 2 : 4, 16777216);
					}
				}
			}
		}

		// Token: 0x17000D28 RID: 3368
		// (get) Token: 0x06003649 RID: 13897 RVA: 0x000F4CDC File Offset: 0x000F2EDC
		// (set) Token: 0x0600364A RID: 13898 RVA: 0x000F4D44 File Offset: 0x000F2F44
		[Localizable(true)]
		[RefreshProperties(RefreshProperties.All)]
		public override string Text
		{
			get
			{
				if (base.IsDisposed)
				{
					return base.Text;
				}
				if (base.RecreatingHandle || base.GetAnyDisposingInHierarchy())
				{
					return "";
				}
				if (base.IsHandleCreated || this.textRtf != null)
				{
					this.ForceHandleCreate();
					return this.StreamOut(17);
				}
				if (this.textPlain != null)
				{
					return this.textPlain;
				}
				return base.Text;
			}
			set
			{
				using (LayoutTransaction.CreateTransactionIf(this.AutoSize, this.ParentInternal, this, PropertyNames.Text))
				{
					this.textRtf = null;
					if (!base.IsHandleCreated)
					{
						this.textPlain = value;
					}
					else
					{
						this.textPlain = null;
						if (value == null)
						{
							value = "";
						}
						this.StreamIn(value, 17);
						base.SendMessage(185, 0, 0);
					}
				}
			}
		}

		// Token: 0x17000D29 RID: 3369
		// (get) Token: 0x0600364B RID: 13899 RVA: 0x000F4DC8 File Offset: 0x000F2FC8
		// (set) Token: 0x0600364C RID: 13900 RVA: 0x000F4DE0 File Offset: 0x000F2FE0
		private bool SuppressTextChangedEvent
		{
			get
			{
				return this.richTextBoxFlags[RichTextBox.suppressTextChangedEventSection] != 0;
			}
			set
			{
				bool suppressTextChangedEvent = this.SuppressTextChangedEvent;
				if (value != suppressTextChangedEvent)
				{
					this.richTextBoxFlags[RichTextBox.suppressTextChangedEventSection] = (value ? 1 : 0);
					CommonProperties.xClearPreferredSizeCache(this);
				}
			}
		}

		// Token: 0x17000D2A RID: 3370
		// (get) Token: 0x0600364D RID: 13901 RVA: 0x000F4E18 File Offset: 0x000F3018
		[Browsable(false)]
		public override int TextLength
		{
			get
			{
				NativeMethods.GETTEXTLENGTHEX gettextlengthex = new NativeMethods.GETTEXTLENGTHEX();
				gettextlengthex.flags = 8U;
				if (Marshal.SystemDefaultCharSize == 1)
				{
					gettextlengthex.codepage = 0U;
				}
				else
				{
					gettextlengthex.codepage = 1200U;
				}
				return (int)((long)UnsafeNativeMethods.SendMessage(new HandleRef(this, base.Handle), 1119, gettextlengthex, 0));
			}
		}

		// Token: 0x17000D2B RID: 3371
		// (get) Token: 0x0600364E RID: 13902 RVA: 0x000F4E6C File Offset: 0x000F306C
		[SRCategory("CatBehavior")]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[SRDescription("RichTextBoxUndoActionNameDescr")]
		public string UndoActionName
		{
			get
			{
				if (!base.CanUndo)
				{
					return "";
				}
				int actionID = (int)((long)base.SendMessage(1110, 0, 0));
				return this.GetEditorActionName(actionID);
			}
		}

		// Token: 0x0600364F RID: 13903 RVA: 0x000F4EA4 File Offset: 0x000F30A4
		private string GetEditorActionName(int actionID)
		{
			switch (actionID)
			{
			default:
				return SR.GetString("RichTextBox_IDUnknown");
			case 1:
				return SR.GetString("RichTextBox_IDTyping");
			case 2:
				return SR.GetString("RichTextBox_IDDelete");
			case 3:
				return SR.GetString("RichTextBox_IDDragDrop");
			case 4:
				return SR.GetString("RichTextBox_IDCut");
			case 5:
				return SR.GetString("RichTextBox_IDPaste");
			}
		}

		// Token: 0x17000D2C RID: 3372
		// (get) Token: 0x06003650 RID: 13904 RVA: 0x000F4F10 File Offset: 0x000F3110
		// (set) Token: 0x06003651 RID: 13905 RVA: 0x000F4F64 File Offset: 0x000F3164
		[SRCategory("CatBehavior")]
		[DefaultValue(1f)]
		[Localizable(true)]
		[SRDescription("RichTextBoxZoomFactor")]
		public float ZoomFactor
		{
			get
			{
				if (base.IsHandleCreated)
				{
					int num = 0;
					int num2 = 0;
					base.SendMessage(1248, ref num, ref num2);
					if (num != 0 && num2 != 0)
					{
						this.zoomMultiplier = (float)num / (float)num2;
					}
					else
					{
						this.zoomMultiplier = 1f;
					}
					return this.zoomMultiplier;
				}
				return this.zoomMultiplier;
			}
			set
			{
				if (this.zoomMultiplier == value)
				{
					return;
				}
				if (value <= 0.015625f || value >= 64f)
				{
					throw new ArgumentOutOfRangeException("ZoomFactor", SR.GetString("InvalidExBoundArgument", new object[]
					{
						"ZoomFactor",
						value.ToString(CultureInfo.CurrentCulture),
						0.015625f.ToString(CultureInfo.CurrentCulture),
						64f.ToString(CultureInfo.CurrentCulture)
					}));
				}
				this.SendZoomFactor(value);
			}
		}

		// Token: 0x1400028B RID: 651
		// (add) Token: 0x06003652 RID: 13906 RVA: 0x000F4FEE File Offset: 0x000F31EE
		// (remove) Token: 0x06003653 RID: 13907 RVA: 0x000F5001 File Offset: 0x000F3201
		[SRCategory("CatBehavior")]
		[SRDescription("RichTextBoxContentsResized")]
		public event ContentsResizedEventHandler ContentsResized
		{
			add
			{
				base.Events.AddHandler(RichTextBox.EVENT_REQUESTRESIZE, value);
			}
			remove
			{
				base.Events.RemoveHandler(RichTextBox.EVENT_REQUESTRESIZE, value);
			}
		}

		// Token: 0x1400028C RID: 652
		// (add) Token: 0x06003654 RID: 13908 RVA: 0x000F5014 File Offset: 0x000F3214
		// (remove) Token: 0x06003655 RID: 13909 RVA: 0x000F501D File Offset: 0x000F321D
		[Browsable(false)]
		public new event DragEventHandler DragDrop
		{
			add
			{
				base.DragDrop += value;
			}
			remove
			{
				base.DragDrop -= value;
			}
		}

		// Token: 0x1400028D RID: 653
		// (add) Token: 0x06003656 RID: 13910 RVA: 0x000F5026 File Offset: 0x000F3226
		// (remove) Token: 0x06003657 RID: 13911 RVA: 0x000F502F File Offset: 0x000F322F
		[Browsable(false)]
		public new event DragEventHandler DragEnter
		{
			add
			{
				base.DragEnter += value;
			}
			remove
			{
				base.DragEnter -= value;
			}
		}

		// Token: 0x1400028E RID: 654
		// (add) Token: 0x06003658 RID: 13912 RVA: 0x000F5038 File Offset: 0x000F3238
		// (remove) Token: 0x06003659 RID: 13913 RVA: 0x000F5041 File Offset: 0x000F3241
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler DragLeave
		{
			add
			{
				base.DragLeave += value;
			}
			remove
			{
				base.DragLeave -= value;
			}
		}

		// Token: 0x1400028F RID: 655
		// (add) Token: 0x0600365A RID: 13914 RVA: 0x000F504A File Offset: 0x000F324A
		// (remove) Token: 0x0600365B RID: 13915 RVA: 0x000F5053 File Offset: 0x000F3253
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event DragEventHandler DragOver
		{
			add
			{
				base.DragOver += value;
			}
			remove
			{
				base.DragOver -= value;
			}
		}

		// Token: 0x14000290 RID: 656
		// (add) Token: 0x0600365C RID: 13916 RVA: 0x000F505C File Offset: 0x000F325C
		// (remove) Token: 0x0600365D RID: 13917 RVA: 0x000F5065 File Offset: 0x000F3265
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event GiveFeedbackEventHandler GiveFeedback
		{
			add
			{
				base.GiveFeedback += value;
			}
			remove
			{
				base.GiveFeedback -= value;
			}
		}

		// Token: 0x14000291 RID: 657
		// (add) Token: 0x0600365E RID: 13918 RVA: 0x000F506E File Offset: 0x000F326E
		// (remove) Token: 0x0600365F RID: 13919 RVA: 0x000F5077 File Offset: 0x000F3277
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event QueryContinueDragEventHandler QueryContinueDrag
		{
			add
			{
				base.QueryContinueDrag += value;
			}
			remove
			{
				base.QueryContinueDrag -= value;
			}
		}

		// Token: 0x14000292 RID: 658
		// (add) Token: 0x06003660 RID: 13920 RVA: 0x000F5080 File Offset: 0x000F3280
		// (remove) Token: 0x06003661 RID: 13921 RVA: 0x000F5093 File Offset: 0x000F3293
		[SRCategory("CatBehavior")]
		[SRDescription("RichTextBoxHScroll")]
		public event EventHandler HScroll
		{
			add
			{
				base.Events.AddHandler(RichTextBox.EVENT_HSCROLL, value);
			}
			remove
			{
				base.Events.RemoveHandler(RichTextBox.EVENT_HSCROLL, value);
			}
		}

		// Token: 0x14000293 RID: 659
		// (add) Token: 0x06003662 RID: 13922 RVA: 0x000F50A6 File Offset: 0x000F32A6
		// (remove) Token: 0x06003663 RID: 13923 RVA: 0x000F50B9 File Offset: 0x000F32B9
		[SRCategory("CatBehavior")]
		[SRDescription("RichTextBoxLinkClick")]
		public event LinkClickedEventHandler LinkClicked
		{
			add
			{
				base.Events.AddHandler(RichTextBox.EVENT_LINKACTIVATE, value);
			}
			remove
			{
				base.Events.RemoveHandler(RichTextBox.EVENT_LINKACTIVATE, value);
			}
		}

		// Token: 0x14000294 RID: 660
		// (add) Token: 0x06003664 RID: 13924 RVA: 0x000F50CC File Offset: 0x000F32CC
		// (remove) Token: 0x06003665 RID: 13925 RVA: 0x000F50DF File Offset: 0x000F32DF
		[SRCategory("CatBehavior")]
		[SRDescription("RichTextBoxIMEChange")]
		public event EventHandler ImeChange
		{
			add
			{
				base.Events.AddHandler(RichTextBox.EVENT_IMECHANGE, value);
			}
			remove
			{
				base.Events.RemoveHandler(RichTextBox.EVENT_IMECHANGE, value);
			}
		}

		// Token: 0x14000295 RID: 661
		// (add) Token: 0x06003666 RID: 13926 RVA: 0x000F50F2 File Offset: 0x000F32F2
		// (remove) Token: 0x06003667 RID: 13927 RVA: 0x000F5105 File Offset: 0x000F3305
		[SRCategory("CatBehavior")]
		[SRDescription("RichTextBoxProtected")]
		public event EventHandler Protected
		{
			add
			{
				base.Events.AddHandler(RichTextBox.EVENT_PROTECTED, value);
			}
			remove
			{
				base.Events.RemoveHandler(RichTextBox.EVENT_PROTECTED, value);
			}
		}

		// Token: 0x14000296 RID: 662
		// (add) Token: 0x06003668 RID: 13928 RVA: 0x000F5118 File Offset: 0x000F3318
		// (remove) Token: 0x06003669 RID: 13929 RVA: 0x000F512B File Offset: 0x000F332B
		[SRCategory("CatBehavior")]
		[SRDescription("RichTextBoxSelChange")]
		public event EventHandler SelectionChanged
		{
			add
			{
				base.Events.AddHandler(RichTextBox.EVENT_SELCHANGE, value);
			}
			remove
			{
				base.Events.RemoveHandler(RichTextBox.EVENT_SELCHANGE, value);
			}
		}

		// Token: 0x14000297 RID: 663
		// (add) Token: 0x0600366A RID: 13930 RVA: 0x000F513E File Offset: 0x000F333E
		// (remove) Token: 0x0600366B RID: 13931 RVA: 0x000F5151 File Offset: 0x000F3351
		[SRCategory("CatBehavior")]
		[SRDescription("RichTextBoxVScroll")]
		public event EventHandler VScroll
		{
			add
			{
				base.Events.AddHandler(RichTextBox.EVENT_VSCROLL, value);
			}
			remove
			{
				base.Events.RemoveHandler(RichTextBox.EVENT_VSCROLL, value);
			}
		}

		// Token: 0x0600366C RID: 13932 RVA: 0x000F5164 File Offset: 0x000F3364
		public bool CanPaste(DataFormats.Format clipFormat)
		{
			return (int)((long)base.SendMessage(1074, clipFormat.Id, 0)) != 0;
		}

		// Token: 0x0600366D RID: 13933 RVA: 0x0001AC95 File Offset: 0x00018E95
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new void DrawToBitmap(Bitmap bitmap, Rectangle targetBounds)
		{
			base.DrawToBitmap(bitmap, targetBounds);
		}

		// Token: 0x0600366E RID: 13934 RVA: 0x000F5190 File Offset: 0x000F3390
		private unsafe int EditStreamProc(IntPtr dwCookie, IntPtr buf, int cb, out int transferred)
		{
			int result = 0;
			byte[] array = new byte[cb];
			int num = (int)dwCookie;
			transferred = 0;
			try
			{
				int num2 = num & 3;
				if (num2 != 1)
				{
					if (num2 == 2)
					{
						if (this.editStream == null)
						{
							this.editStream = new MemoryStream();
						}
						int num3 = num & 112;
						if (num3 != 16)
						{
							if (num3 == 32 || num3 == 64)
							{
								Marshal.Copy(buf, array, 0, cb);
								this.editStream.Write(array, 0, cb);
							}
						}
						else if ((num & 8) != 0)
						{
							int num4 = cb / 2;
							int num5 = 0;
							try
							{
								byte[] array2;
								byte* ptr;
								if ((array2 = array) == null || array2.Length == 0)
								{
									ptr = null;
								}
								else
								{
									ptr = &array2[0];
								}
								char* ptr2 = (char*)ptr;
								char* ptr3 = (long)buf;
								for (int i = 0; i < num4; i++)
								{
									if (*ptr3 == '\r')
									{
										ptr3++;
									}
									else
									{
										*ptr2 = *ptr3;
										ptr2++;
										ptr3++;
										num5++;
									}
								}
							}
							finally
							{
								byte[] array2 = null;
							}
							this.editStream.Write(array, 0, num5 * 2);
						}
						else
						{
							int num6 = 0;
							try
							{
								byte[] array2;
								byte* ptr4;
								if ((array2 = array) == null || array2.Length == 0)
								{
									ptr4 = null;
								}
								else
								{
									ptr4 = &array2[0];
								}
								byte* ptr5 = ptr4;
								byte* ptr6 = (long)buf;
								for (int j = 0; j < cb; j++)
								{
									if (*ptr6 == 13)
									{
										ptr6++;
									}
									else
									{
										*ptr5 = *ptr6;
										ptr5++;
										ptr6++;
										num6++;
									}
								}
							}
							finally
							{
								byte[] array2 = null;
							}
							this.editStream.Write(array, 0, num6);
						}
						transferred = cb;
					}
				}
				else if (this.editStream != null)
				{
					transferred = this.editStream.Read(array, 0, cb);
					Marshal.Copy(array, 0, buf, transferred);
					if (transferred < 0)
					{
						transferred = 0;
					}
				}
				else
				{
					transferred = 0;
				}
			}
			catch (IOException)
			{
				transferred = 0;
				result = 1;
			}
			return result;
		}

		// Token: 0x0600366F RID: 13935 RVA: 0x000F53A8 File Offset: 0x000F35A8
		public int Find(string str)
		{
			return this.Find(str, 0, 0, RichTextBoxFinds.None);
		}

		// Token: 0x06003670 RID: 13936 RVA: 0x000F53B4 File Offset: 0x000F35B4
		public int Find(string str, RichTextBoxFinds options)
		{
			return this.Find(str, 0, 0, options);
		}

		// Token: 0x06003671 RID: 13937 RVA: 0x000F53C0 File Offset: 0x000F35C0
		public int Find(string str, int start, RichTextBoxFinds options)
		{
			return this.Find(str, start, -1, options);
		}

		// Token: 0x06003672 RID: 13938 RVA: 0x000F53CC File Offset: 0x000F35CC
		public int Find(string str, int start, int end, RichTextBoxFinds options)
		{
			int textLength = this.TextLength;
			if (str == null)
			{
				throw new ArgumentNullException("str");
			}
			if (start < 0 || start > textLength)
			{
				throw new ArgumentOutOfRangeException("start", SR.GetString("InvalidBoundArgument", new object[]
				{
					"start",
					start,
					0,
					textLength
				}));
			}
			if (end < -1)
			{
				throw new ArgumentOutOfRangeException("end", SR.GetString("RichTextFindEndInvalid", new object[]
				{
					end
				}));
			}
			bool flag = true;
			NativeMethods.FINDTEXT findtext = new NativeMethods.FINDTEXT();
			findtext.chrg = new NativeMethods.CHARRANGE();
			findtext.lpstrText = str;
			if (end == -1)
			{
				end = textLength;
			}
			if (start > end)
			{
				throw new ArgumentException(SR.GetString("RichTextFindEndInvalid", new object[]
				{
					end
				}));
			}
			if ((options & RichTextBoxFinds.Reverse) != RichTextBoxFinds.Reverse)
			{
				findtext.chrg.cpMin = start;
				findtext.chrg.cpMax = end;
			}
			else
			{
				findtext.chrg.cpMin = end;
				findtext.chrg.cpMax = start;
			}
			if (findtext.chrg.cpMin == findtext.chrg.cpMax)
			{
				if ((options & RichTextBoxFinds.Reverse) != RichTextBoxFinds.Reverse)
				{
					findtext.chrg.cpMin = 0;
					findtext.chrg.cpMax = -1;
				}
				else
				{
					findtext.chrg.cpMin = textLength;
					findtext.chrg.cpMax = 0;
				}
			}
			int num = 0;
			if ((options & RichTextBoxFinds.WholeWord) == RichTextBoxFinds.WholeWord)
			{
				num |= 2;
			}
			if ((options & RichTextBoxFinds.MatchCase) == RichTextBoxFinds.MatchCase)
			{
				num |= 4;
			}
			if ((options & RichTextBoxFinds.NoHighlight) == RichTextBoxFinds.NoHighlight)
			{
				flag = false;
			}
			if ((options & RichTextBoxFinds.Reverse) != RichTextBoxFinds.Reverse)
			{
				num |= 1;
			}
			int num2 = (int)UnsafeNativeMethods.SendMessage(new HandleRef(this, base.Handle), 1080, num, findtext);
			if (num2 != -1 && flag)
			{
				NativeMethods.CHARRANGE charrange = new NativeMethods.CHARRANGE();
				charrange.cpMin = num2;
				char c = 'ـ';
				string text = this.Text;
				string text2 = text.Substring(num2, str.Length);
				int num3 = text2.IndexOf(c);
				if (num3 == -1)
				{
					charrange.cpMax = num2 + str.Length;
				}
				else
				{
					int i = num3;
					int num4 = num2 + num3;
					while (i < str.Length)
					{
						while (text[num4] == c && str[i] != c)
						{
							num4++;
						}
						i++;
						num4++;
					}
					charrange.cpMax = num4;
				}
				UnsafeNativeMethods.SendMessage(new HandleRef(this, base.Handle), 1079, 0, charrange);
				base.SendMessage(183, 0, 0);
			}
			return num2;
		}

		// Token: 0x06003673 RID: 13939 RVA: 0x000F5651 File Offset: 0x000F3851
		public int Find(char[] characterSet)
		{
			return this.Find(characterSet, 0, -1);
		}

		// Token: 0x06003674 RID: 13940 RVA: 0x000F565C File Offset: 0x000F385C
		public int Find(char[] characterSet, int start)
		{
			return this.Find(characterSet, start, -1);
		}

		// Token: 0x06003675 RID: 13941 RVA: 0x000F5668 File Offset: 0x000F3868
		public int Find(char[] characterSet, int start, int end)
		{
			bool flag = true;
			bool negate = false;
			int textLength = this.TextLength;
			if (characterSet == null)
			{
				throw new ArgumentNullException("characterSet");
			}
			if (start < 0 || start > textLength)
			{
				throw new ArgumentOutOfRangeException("start", SR.GetString("InvalidBoundArgument", new object[]
				{
					"start",
					start,
					0,
					textLength
				}));
			}
			if (end < start && end != -1)
			{
				throw new ArgumentOutOfRangeException("end", SR.GetString("InvalidLowBoundArgumentEx", new object[]
				{
					"end",
					end,
					"start"
				}));
			}
			if (characterSet.Length == 0)
			{
				return -1;
			}
			int windowTextLength = SafeNativeMethods.GetWindowTextLength(new HandleRef(this, base.Handle));
			if (start == end)
			{
				start = 0;
				end = windowTextLength;
			}
			if (end == -1)
			{
				end = windowTextLength;
			}
			NativeMethods.CHARRANGE charrange = new NativeMethods.CHARRANGE();
			charrange.cpMax = (charrange.cpMin = start);
			NativeMethods.TEXTRANGE textrange = new NativeMethods.TEXTRANGE();
			textrange.chrg = new NativeMethods.CHARRANGE();
			textrange.chrg.cpMin = charrange.cpMin;
			textrange.chrg.cpMax = charrange.cpMax;
			UnsafeNativeMethods.CharBuffer charBuffer = UnsafeNativeMethods.CharBuffer.CreateBuffer(513);
			textrange.lpstrText = charBuffer.AllocCoTaskMem();
			if (textrange.lpstrText == IntPtr.Zero)
			{
				throw new OutOfMemoryException();
			}
			try
			{
				bool flag2 = false;
				while (!flag2)
				{
					if (flag)
					{
						textrange.chrg.cpMin = charrange.cpMax;
						textrange.chrg.cpMax += 512;
					}
					else
					{
						textrange.chrg.cpMax = charrange.cpMin;
						textrange.chrg.cpMin -= 512;
						if (textrange.chrg.cpMin < 0)
						{
							textrange.chrg.cpMin = 0;
						}
					}
					if (end != -1)
					{
						textrange.chrg.cpMax = Math.Min(textrange.chrg.cpMax, end);
					}
					int num = (int)UnsafeNativeMethods.SendMessage(new HandleRef(this, base.Handle), 1099, 0, textrange);
					if (num == 0)
					{
						charrange.cpMax = (charrange.cpMin = -1);
						break;
					}
					charBuffer.PutCoTaskMem(textrange.lpstrText);
					string @string = charBuffer.GetString();
					if (flag)
					{
						for (int i = 0; i < num; i++)
						{
							bool charInCharSet = this.GetCharInCharSet(@string[i], characterSet, negate);
							if (charInCharSet)
							{
								flag2 = true;
								break;
							}
							charrange.cpMax++;
						}
					}
					else
					{
						int index = num;
						while (index-- != 0)
						{
							bool charInCharSet2 = this.GetCharInCharSet(@string[index], characterSet, negate);
							if (charInCharSet2)
							{
								flag2 = true;
								break;
							}
							charrange.cpMin--;
						}
					}
				}
			}
			finally
			{
				if (textrange.lpstrText != IntPtr.Zero)
				{
					Marshal.FreeCoTaskMem(textrange.lpstrText);
				}
			}
			return flag ? charrange.cpMax : charrange.cpMin;
		}

		// Token: 0x06003676 RID: 13942 RVA: 0x000F5990 File Offset: 0x000F3B90
		private void ForceHandleCreate()
		{
			if (!base.IsHandleCreated)
			{
				this.CreateHandle();
			}
		}

		// Token: 0x06003677 RID: 13943 RVA: 0x000F59A0 File Offset: 0x000F3BA0
		private bool InternalSetForeColor(Color value)
		{
			NativeMethods.CHARFORMATA charFormat = this.GetCharFormat(false);
			if ((charFormat.dwMask & 1073741824) != 0 && ColorTranslator.ToWin32(value) == charFormat.crTextColor)
			{
				return true;
			}
			charFormat.dwMask = 1073741824;
			charFormat.dwEffects = 0;
			charFormat.crTextColor = ColorTranslator.ToWin32(value);
			return this.SetCharFormat(4, charFormat);
		}

		// Token: 0x06003678 RID: 13944 RVA: 0x000F59FC File Offset: 0x000F3BFC
		private NativeMethods.CHARFORMATA GetCharFormat(bool fSelection)
		{
			NativeMethods.CHARFORMATA charformata = new NativeMethods.CHARFORMATA();
			UnsafeNativeMethods.SendMessage(new HandleRef(this, base.Handle), 1082, fSelection ? 1 : 0, charformata);
			return charformata;
		}

		// Token: 0x06003679 RID: 13945 RVA: 0x000F5A30 File Offset: 0x000F3C30
		private NativeMethods.CHARFORMAT2A GetCharFormat2(bool fSelection)
		{
			NativeMethods.CHARFORMAT2A charformat2A = new NativeMethods.CHARFORMAT2A();
			UnsafeNativeMethods.SendMessage(new HandleRef(this, base.Handle), 1082, fSelection ? 1 : 0, charformat2A);
			return charformat2A;
		}

		// Token: 0x0600367A RID: 13946 RVA: 0x000F5A64 File Offset: 0x000F3C64
		private RichTextBoxSelectionAttribute GetCharFormat(int mask, int effect)
		{
			RichTextBoxSelectionAttribute result = RichTextBoxSelectionAttribute.None;
			if (base.IsHandleCreated)
			{
				NativeMethods.CHARFORMATA charFormat = this.GetCharFormat(true);
				if ((charFormat.dwMask & mask) != 0 && (charFormat.dwEffects & effect) != 0)
				{
					result = RichTextBoxSelectionAttribute.All;
				}
			}
			return result;
		}

		// Token: 0x0600367B RID: 13947 RVA: 0x000F5A9C File Offset: 0x000F3C9C
		private Font GetCharFormatFont(bool selectionOnly)
		{
			this.ForceHandleCreate();
			NativeMethods.CHARFORMATA charFormat = this.GetCharFormat(selectionOnly);
			if ((charFormat.dwMask & 536870912) == 0)
			{
				return null;
			}
			string text = Encoding.Default.GetString(charFormat.szFaceName);
			int num = text.IndexOf('\0');
			if (num != -1)
			{
				text = text.Substring(0, num);
			}
			float num2 = 13f;
			if ((charFormat.dwMask & -2147483648) != 0)
			{
				num2 = (float)charFormat.yHeight / 20f;
				if (num2 == 0f && charFormat.yHeight > 0)
				{
					num2 = 1f;
				}
			}
			FontStyle fontStyle = FontStyle.Regular;
			if ((charFormat.dwMask & 1) != 0 && (charFormat.dwEffects & 1) != 0)
			{
				fontStyle |= FontStyle.Bold;
			}
			if ((charFormat.dwMask & 2) != 0 && (charFormat.dwEffects & 2) != 0)
			{
				fontStyle |= FontStyle.Italic;
			}
			if ((charFormat.dwMask & 8) != 0 && (charFormat.dwEffects & 8) != 0)
			{
				fontStyle |= FontStyle.Strikeout;
			}
			if ((charFormat.dwMask & 4) != 0 && (charFormat.dwEffects & 4) != 0)
			{
				fontStyle |= FontStyle.Underline;
			}
			try
			{
				return new Font(text, num2, fontStyle, GraphicsUnit.Point, charFormat.bCharSet);
			}
			catch
			{
			}
			return null;
		}

		// Token: 0x0600367C RID: 13948 RVA: 0x000F5BC0 File Offset: 0x000F3DC0
		public override int GetCharIndexFromPosition(Point pt)
		{
			NativeMethods.POINT lParam = new NativeMethods.POINT(pt.X, pt.Y);
			int num = (int)UnsafeNativeMethods.SendMessage(new HandleRef(this, base.Handle), 215, 0, lParam);
			string text = this.Text;
			if (num >= text.Length)
			{
				num = Math.Max(text.Length - 1, 0);
			}
			return num;
		}

		// Token: 0x0600367D RID: 13949 RVA: 0x000F5C20 File Offset: 0x000F3E20
		private bool GetCharInCharSet(char c, char[] charSet, bool negate)
		{
			bool flag = false;
			int num = charSet.Length;
			int num2 = 0;
			while (!flag && num2 < num)
			{
				flag = (c == charSet[num2]);
				num2++;
			}
			if (!negate)
			{
				return flag;
			}
			return !flag;
		}

		// Token: 0x0600367E RID: 13950 RVA: 0x000F5C52 File Offset: 0x000F3E52
		public override int GetLineFromCharIndex(int index)
		{
			return (int)((long)base.SendMessage(1078, 0, index));
		}

		// Token: 0x0600367F RID: 13951 RVA: 0x000F5C68 File Offset: 0x000F3E68
		public override Point GetPositionFromCharIndex(int index)
		{
			if (RichTextBox.richEditMajorVersion == 2)
			{
				return base.GetPositionFromCharIndex(index);
			}
			if (index < 0 || index > this.Text.Length)
			{
				return Point.Empty;
			}
			NativeMethods.POINT point = new NativeMethods.POINT();
			UnsafeNativeMethods.SendMessage(new HandleRef(this, base.Handle), 214, point, index);
			return new Point(point.x, point.y);
		}

		// Token: 0x06003680 RID: 13952 RVA: 0x000F5CCD File Offset: 0x000F3ECD
		private bool GetProtectedError()
		{
			if (this.ProtectedError)
			{
				this.ProtectedError = false;
				return true;
			}
			return false;
		}

		// Token: 0x06003681 RID: 13953 RVA: 0x000F5CE1 File Offset: 0x000F3EE1
		public void LoadFile(string path)
		{
			this.LoadFile(path, RichTextBoxStreamType.RichText);
		}

		// Token: 0x06003682 RID: 13954 RVA: 0x000F5CEC File Offset: 0x000F3EEC
		public void LoadFile(string path, RichTextBoxStreamType fileType)
		{
			if (!ClientUtils.IsEnumValid(fileType, (int)fileType, 0, 4))
			{
				throw new InvalidEnumArgumentException("fileType", (int)fileType, typeof(RichTextBoxStreamType));
			}
			Stream stream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read);
			try
			{
				this.LoadFile(stream, fileType);
			}
			finally
			{
				stream.Close();
			}
		}

		// Token: 0x06003683 RID: 13955 RVA: 0x000F5D4C File Offset: 0x000F3F4C
		public void LoadFile(Stream data, RichTextBoxStreamType fileType)
		{
			if (!ClientUtils.IsEnumValid(fileType, (int)fileType, 0, 4))
			{
				throw new InvalidEnumArgumentException("fileType", (int)fileType, typeof(RichTextBoxStreamType));
			}
			int flags;
			switch (fileType)
			{
			case RichTextBoxStreamType.RichText:
				flags = 2;
				goto IL_6A;
			case RichTextBoxStreamType.PlainText:
				this.Rtf = "";
				flags = 1;
				goto IL_6A;
			case RichTextBoxStreamType.UnicodePlainText:
				flags = 17;
				goto IL_6A;
			}
			throw new ArgumentException(SR.GetString("InvalidFileType"));
			IL_6A:
			this.StreamIn(data, flags);
		}

		// Token: 0x06003684 RID: 13956 RVA: 0x000F5DCB File Offset: 0x000F3FCB
		protected override void OnBackColorChanged(EventArgs e)
		{
			if (base.IsHandleCreated)
			{
				base.SendMessage(1091, 0, ColorTranslator.ToWin32(this.BackColor));
			}
			base.OnBackColorChanged(e);
		}

		// Token: 0x06003685 RID: 13957 RVA: 0x000F5DF4 File Offset: 0x000F3FF4
		protected override void OnContextMenuChanged(EventArgs e)
		{
			base.OnContextMenuChanged(e);
			this.UpdateOleCallback();
		}

		// Token: 0x06003686 RID: 13958 RVA: 0x000F5E04 File Offset: 0x000F4004
		protected override void OnRightToLeftChanged(EventArgs e)
		{
			base.OnRightToLeftChanged(e);
			string windowText = this.WindowText;
			base.ForceWindowText(null);
			base.ForceWindowText(windowText);
		}

		// Token: 0x06003687 RID: 13959 RVA: 0x000F5E30 File Offset: 0x000F4030
		protected virtual void OnContentsResized(ContentsResizedEventArgs e)
		{
			ContentsResizedEventHandler contentsResizedEventHandler = (ContentsResizedEventHandler)base.Events[RichTextBox.EVENT_REQUESTRESIZE];
			if (contentsResizedEventHandler != null)
			{
				contentsResizedEventHandler(this, e);
			}
		}

		// Token: 0x06003688 RID: 13960 RVA: 0x000F5E60 File Offset: 0x000F4060
		protected override void OnHandleCreated(EventArgs e)
		{
			this.curSelStart = (this.curSelEnd = (int)(this.curSelType = -1));
			this.UpdateMaxLength();
			base.SendMessage(1093, 0, 79626255);
			int num = this.rightMargin;
			this.rightMargin = 0;
			this.RightMargin = num;
			base.SendMessage(1115, this.DetectUrls ? 1 : 0, 0);
			if (this.selectionBackColorToSetOnHandleCreated != Color.Empty)
			{
				this.SelectionBackColor = this.selectionBackColorToSetOnHandleCreated;
			}
			this.AutoWordSelection = this.AutoWordSelection;
			base.SendMessage(1091, 0, ColorTranslator.ToWin32(this.BackColor));
			this.InternalSetForeColor(this.ForeColor);
			base.OnHandleCreated(e);
			this.UpdateOleCallback();
			try
			{
				this.SuppressTextChangedEvent = true;
				if (this.textRtf != null)
				{
					string rtf = this.textRtf;
					this.textRtf = null;
					this.Rtf = rtf;
				}
				else if (this.textPlain != null)
				{
					string text = this.textPlain;
					this.textPlain = null;
					this.Text = text;
				}
			}
			finally
			{
				this.SuppressTextChangedEvent = false;
			}
			base.SetSelectionOnHandle();
			if (this.ShowSelectionMargin)
			{
				UnsafeNativeMethods.PostMessage(new HandleRef(this, base.Handle), 1101, (IntPtr)2, (IntPtr)16777216);
			}
			if (this.languageOption != this.LanguageOption)
			{
				this.LanguageOption = this.languageOption;
			}
			base.ClearUndo();
			this.SendZoomFactor(this.zoomMultiplier);
			SystemEvents.UserPreferenceChanged += this.UserPreferenceChangedHandler;
		}

		// Token: 0x06003689 RID: 13961 RVA: 0x000F5FFC File Offset: 0x000F41FC
		protected override void OnHandleDestroyed(EventArgs e)
		{
			base.OnHandleDestroyed(e);
			if (!this.InConstructor)
			{
				this.textRtf = this.Rtf;
				if (this.textRtf.Length == 0)
				{
					this.textRtf = null;
				}
			}
			this.oleCallback = null;
			SystemEvents.UserPreferenceChanged -= this.UserPreferenceChangedHandler;
		}

		// Token: 0x0600368A RID: 13962 RVA: 0x000F6050 File Offset: 0x000F4250
		protected virtual void OnHScroll(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[RichTextBox.EVENT_HSCROLL];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x0600368B RID: 13963 RVA: 0x000F6080 File Offset: 0x000F4280
		protected virtual void OnLinkClicked(LinkClickedEventArgs e)
		{
			LinkClickedEventHandler linkClickedEventHandler = (LinkClickedEventHandler)base.Events[RichTextBox.EVENT_LINKACTIVATE];
			if (linkClickedEventHandler != null)
			{
				linkClickedEventHandler(this, e);
			}
		}

		// Token: 0x0600368C RID: 13964 RVA: 0x000F60B0 File Offset: 0x000F42B0
		protected virtual void OnImeChange(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[RichTextBox.EVENT_IMECHANGE];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x0600368D RID: 13965 RVA: 0x000F60E0 File Offset: 0x000F42E0
		protected virtual void OnProtected(EventArgs e)
		{
			this.ProtectedError = true;
			EventHandler eventHandler = (EventHandler)base.Events[RichTextBox.EVENT_PROTECTED];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x0600368E RID: 13966 RVA: 0x000F6118 File Offset: 0x000F4318
		protected virtual void OnSelectionChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[RichTextBox.EVENT_SELCHANGE];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x0600368F RID: 13967 RVA: 0x000F6148 File Offset: 0x000F4348
		protected virtual void OnVScroll(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[RichTextBox.EVENT_VSCROLL];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x06003690 RID: 13968 RVA: 0x000F6176 File Offset: 0x000F4376
		public void Paste(DataFormats.Format clipFormat)
		{
			IntSecurity.ClipboardRead.Demand();
			this.PasteUnsafe(clipFormat, 0);
		}

		// Token: 0x06003691 RID: 13969 RVA: 0x000F618C File Offset: 0x000F438C
		private void PasteUnsafe(DataFormats.Format clipFormat, int hIcon)
		{
			NativeMethods.REPASTESPECIAL repastespecial = null;
			if (hIcon != 0)
			{
				repastespecial = new NativeMethods.REPASTESPECIAL();
				repastespecial.dwAspect = 4;
				repastespecial.dwParam = hIcon;
			}
			UnsafeNativeMethods.SendMessage(new HandleRef(this, base.Handle), 1088, clipFormat.Id, repastespecial);
		}

		// Token: 0x06003692 RID: 13970 RVA: 0x000F61D0 File Offset: 0x000F43D0
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		protected override bool ProcessCmdKey(ref Message m, Keys keyData)
		{
			if (!this.RichTextShortcutsEnabled)
			{
				foreach (int num in RichTextBox.shortcutsToDisable)
				{
					if (keyData == (Keys)num)
					{
						return true;
					}
				}
			}
			return base.ProcessCmdKey(ref m, keyData);
		}

		// Token: 0x06003693 RID: 13971 RVA: 0x000072B6 File Offset: 0x000054B6
		internal override void RaiseAccessibilityTextChangedEvent()
		{
		}

		// Token: 0x06003694 RID: 13972 RVA: 0x000F620B File Offset: 0x000F440B
		public void Redo()
		{
			base.SendMessage(1108, 0, 0);
		}

		// Token: 0x06003695 RID: 13973 RVA: 0x000F621B File Offset: 0x000F441B
		public void SaveFile(string path)
		{
			this.SaveFile(path, RichTextBoxStreamType.RichText);
		}

		// Token: 0x06003696 RID: 13974 RVA: 0x000F6228 File Offset: 0x000F4428
		public void SaveFile(string path, RichTextBoxStreamType fileType)
		{
			if (!ClientUtils.IsEnumValid(fileType, (int)fileType, 0, 4))
			{
				throw new InvalidEnumArgumentException("fileType", (int)fileType, typeof(RichTextBoxStreamType));
			}
			Stream stream = File.Create(path);
			try
			{
				this.SaveFile(stream, fileType);
			}
			finally
			{
				stream.Close();
			}
		}

		// Token: 0x06003697 RID: 13975 RVA: 0x000F6284 File Offset: 0x000F4484
		public void SaveFile(Stream data, RichTextBoxStreamType fileType)
		{
			int flags;
			switch (fileType)
			{
			case RichTextBoxStreamType.RichText:
				flags = 2;
				break;
			case RichTextBoxStreamType.PlainText:
				flags = 1;
				break;
			case RichTextBoxStreamType.RichNoOleObjs:
				flags = 3;
				break;
			case RichTextBoxStreamType.TextTextOleObjs:
				flags = 4;
				break;
			case RichTextBoxStreamType.UnicodePlainText:
				flags = 17;
				break;
			default:
				throw new InvalidEnumArgumentException("fileType", (int)fileType, typeof(RichTextBoxStreamType));
			}
			this.StreamOut(data, flags, true);
		}

		// Token: 0x06003698 RID: 13976 RVA: 0x000F62E4 File Offset: 0x000F44E4
		private void SendZoomFactor(float zoom)
		{
			int num;
			int num2;
			if (zoom == 1f)
			{
				num = 0;
				num2 = 0;
			}
			else
			{
				num = 1000;
				float num3 = 1000f * zoom;
				num2 = (int)Math.Ceiling((double)num3);
				if (num2 >= 64000)
				{
					num2 = (int)Math.Floor((double)num3);
				}
			}
			if (base.IsHandleCreated)
			{
				base.SendMessage(1249, num2, num);
			}
			if (num2 != 0)
			{
				this.zoomMultiplier = (float)num2 / (float)num;
				return;
			}
			this.zoomMultiplier = 1f;
		}

		// Token: 0x06003699 RID: 13977 RVA: 0x000F6358 File Offset: 0x000F4558
		private bool SetCharFormat(int mask, int effect, RichTextBoxSelectionAttribute charFormat)
		{
			if (base.IsHandleCreated)
			{
				NativeMethods.CHARFORMATA charformata = new NativeMethods.CHARFORMATA();
				charformata.dwMask = mask;
				if (charFormat != RichTextBoxSelectionAttribute.None)
				{
					if (charFormat != RichTextBoxSelectionAttribute.All)
					{
						throw new ArgumentException(SR.GetString("UnknownAttr"));
					}
					charformata.dwEffects = effect;
				}
				else
				{
					charformata.dwEffects = 0;
				}
				return IntPtr.Zero != UnsafeNativeMethods.SendMessage(new HandleRef(this, base.Handle), 1092, 1, charformata);
			}
			return false;
		}

		// Token: 0x0600369A RID: 13978 RVA: 0x000F63C7 File Offset: 0x000F45C7
		private bool SetCharFormat(int charRange, NativeMethods.CHARFORMATA cf)
		{
			return IntPtr.Zero != UnsafeNativeMethods.SendMessage(new HandleRef(this, base.Handle), 1092, charRange, cf);
		}

		// Token: 0x0600369B RID: 13979 RVA: 0x000F63EC File Offset: 0x000F45EC
		private void SetCharFormatFont(bool selectionOnly, Font value)
		{
			this.ForceHandleCreate();
			NativeMethods.LOGFONT logfont = new NativeMethods.LOGFONT();
			RichTextBox.FontToLogFont(value, logfont);
			int dwMask = -1476394993;
			int num = 0;
			if (value.Bold)
			{
				num |= 1;
			}
			if (value.Italic)
			{
				num |= 2;
			}
			if (value.Strikeout)
			{
				num |= 8;
			}
			if (value.Underline)
			{
				num |= 4;
			}
			byte[] bytes;
			if (Marshal.SystemDefaultCharSize == 1)
			{
				bytes = Encoding.Default.GetBytes(logfont.lfFaceName);
				NativeMethods.CHARFORMATA charformata = new NativeMethods.CHARFORMATA();
				for (int i = 0; i < bytes.Length; i++)
				{
					charformata.szFaceName[i] = bytes[i];
				}
				charformata.dwMask = dwMask;
				charformata.dwEffects = num;
				charformata.yHeight = (int)(value.SizeInPoints * 20f);
				charformata.bCharSet = logfont.lfCharSet;
				charformata.bPitchAndFamily = logfont.lfPitchAndFamily;
				UnsafeNativeMethods.SendMessage(new HandleRef(this, base.Handle), 1092, selectionOnly ? 1 : 4, charformata);
				return;
			}
			bytes = Encoding.Unicode.GetBytes(logfont.lfFaceName);
			NativeMethods.CHARFORMATW charformatw = new NativeMethods.CHARFORMATW();
			for (int j = 0; j < bytes.Length; j++)
			{
				charformatw.szFaceName[j] = bytes[j];
			}
			charformatw.dwMask = dwMask;
			charformatw.dwEffects = num;
			charformatw.yHeight = (int)(value.SizeInPoints * 20f);
			charformatw.bCharSet = logfont.lfCharSet;
			charformatw.bPitchAndFamily = logfont.lfPitchAndFamily;
			UnsafeNativeMethods.SendMessage(new HandleRef(this, base.Handle), 1092, selectionOnly ? 1 : 4, charformatw);
		}

		// Token: 0x0600369C RID: 13980 RVA: 0x000F657C File Offset: 0x000F477C
		[SecurityPermission(SecurityAction.Assert, Flags = SecurityPermissionFlag.UnmanagedCode)]
		private static void FontToLogFont(Font value, NativeMethods.LOGFONT logfont)
		{
			value.ToLogFont(logfont);
		}

		// Token: 0x0600369D RID: 13981 RVA: 0x000F6588 File Offset: 0x000F4788
		private static void SetupLogPixels(IntPtr hDC)
		{
			bool flag = false;
			if (hDC == IntPtr.Zero)
			{
				hDC = UnsafeNativeMethods.GetDC(NativeMethods.NullHandleRef);
				flag = true;
			}
			if (hDC == IntPtr.Zero)
			{
				return;
			}
			RichTextBox.logPixelsX = UnsafeNativeMethods.GetDeviceCaps(new HandleRef(null, hDC), 88);
			RichTextBox.logPixelsY = UnsafeNativeMethods.GetDeviceCaps(new HandleRef(null, hDC), 90);
			if (flag)
			{
				UnsafeNativeMethods.ReleaseDC(NativeMethods.NullHandleRef, new HandleRef(null, hDC));
			}
		}

		// Token: 0x0600369E RID: 13982 RVA: 0x000F65FC File Offset: 0x000F47FC
		private static int Pixel2Twip(IntPtr hDC, int v, bool xDirection)
		{
			RichTextBox.SetupLogPixels(hDC);
			int num = xDirection ? RichTextBox.logPixelsX : RichTextBox.logPixelsY;
			return (int)((double)v / (double)num * 72.0 * 20.0);
		}

		// Token: 0x0600369F RID: 13983 RVA: 0x000F663C File Offset: 0x000F483C
		private static int Twip2Pixel(IntPtr hDC, int v, bool xDirection)
		{
			RichTextBox.SetupLogPixels(hDC);
			int num = xDirection ? RichTextBox.logPixelsX : RichTextBox.logPixelsY;
			return (int)((double)v / 20.0 / 72.0 * (double)num);
		}

		// Token: 0x060036A0 RID: 13984 RVA: 0x000F667C File Offset: 0x000F487C
		private void StreamIn(string str, int flags)
		{
			if (str.Length != 0)
			{
				int num = str.IndexOf('\0');
				if (num != -1)
				{
					str = str.Substring(0, num);
				}
				byte[] bytes;
				if ((flags & 16) != 0)
				{
					bytes = Encoding.Unicode.GetBytes(str);
				}
				else
				{
					bytes = Encoding.Default.GetBytes(str);
				}
				this.editStream = new MemoryStream(bytes.Length);
				this.editStream.Write(bytes, 0, bytes.Length);
				this.editStream.Position = 0L;
				this.StreamIn(this.editStream, flags);
				return;
			}
			if ((32768 & flags) != 0)
			{
				base.SendMessage(771, 0, 0);
				this.ProtectedError = false;
				return;
			}
			base.SendMessage(12, 0, "");
		}

		// Token: 0x060036A1 RID: 13985 RVA: 0x000F6730 File Offset: 0x000F4930
		private void StreamIn(Stream data, int flags)
		{
			if ((flags & 32768) == 0)
			{
				NativeMethods.CHARRANGE lParam = new NativeMethods.CHARRANGE();
				UnsafeNativeMethods.SendMessage(new HandleRef(this, base.Handle), 1079, 0, lParam);
			}
			try
			{
				this.editStream = data;
				if ((flags & 2) != 0)
				{
					long position = this.editStream.Position;
					byte[] array = new byte[RichTextBox.SZ_RTF_TAG.Length];
					this.editStream.Read(array, (int)position, RichTextBox.SZ_RTF_TAG.Length);
					string @string = Encoding.Default.GetString(array);
					if (!RichTextBox.SZ_RTF_TAG.Equals(@string))
					{
						throw new ArgumentException(SR.GetString("InvalidFileFormat"));
					}
					this.editStream.Position = position;
				}
				NativeMethods.EDITSTREAM editstream = new NativeMethods.EDITSTREAM();
				int num;
				if ((flags & 16) != 0)
				{
					num = 9;
				}
				else
				{
					num = 5;
				}
				if ((flags & 2) != 0)
				{
					num |= 64;
				}
				else
				{
					num |= 16;
				}
				editstream.dwCookie = (IntPtr)num;
				editstream.pfnCallback = new NativeMethods.EditStreamCallback(this.EditStreamProc);
				base.SendMessage(1077, 0, int.MaxValue);
				if (IntPtr.Size == 8)
				{
					NativeMethods.EDITSTREAM64 editstream2 = this.ConvertToEDITSTREAM64(editstream);
					UnsafeNativeMethods.SendMessage(new HandleRef(this, base.Handle), 1097, flags, editstream2);
					editstream.dwError = this.GetErrorValue64(editstream2);
				}
				else
				{
					UnsafeNativeMethods.SendMessage(new HandleRef(this, base.Handle), 1097, flags, editstream);
				}
				this.UpdateMaxLength();
				if (!this.GetProtectedError())
				{
					if (editstream.dwError != 0)
					{
						throw new InvalidOperationException(SR.GetString("LoadTextError"));
					}
					base.SendMessage(185, -1, 0);
					base.SendMessage(186, 0, 0);
				}
			}
			finally
			{
				this.editStream = null;
			}
		}

		// Token: 0x060036A2 RID: 13986 RVA: 0x000F68F8 File Offset: 0x000F4AF8
		private string StreamOut(int flags)
		{
			Stream stream = new MemoryStream();
			this.StreamOut(stream, flags, false);
			stream.Position = 0L;
			int num = (int)stream.Length;
			string text = string.Empty;
			if (num > 0)
			{
				byte[] array = new byte[num];
				stream.Read(array, 0, num);
				if ((flags & 16) != 0)
				{
					text = Encoding.Unicode.GetString(array, 0, array.Length);
				}
				else
				{
					text = Encoding.Default.GetString(array, 0, array.Length);
				}
				if (!string.IsNullOrEmpty(text) && text[text.Length - 1] == '\0')
				{
					text = text.Substring(0, text.Length - 1);
				}
			}
			return text;
		}

		// Token: 0x060036A3 RID: 13987 RVA: 0x000F6990 File Offset: 0x000F4B90
		private void StreamOut(Stream data, int flags, bool includeCrLfs)
		{
			this.editStream = data;
			try
			{
				NativeMethods.EDITSTREAM editstream = new NativeMethods.EDITSTREAM();
				int num;
				if ((flags & 16) != 0)
				{
					num = 10;
				}
				else
				{
					num = 6;
				}
				if ((flags & 2) != 0)
				{
					num |= 64;
				}
				else if (includeCrLfs)
				{
					num |= 32;
				}
				else
				{
					num |= 16;
				}
				editstream.dwCookie = (IntPtr)num;
				editstream.pfnCallback = new NativeMethods.EditStreamCallback(this.EditStreamProc);
				if (IntPtr.Size == 8)
				{
					NativeMethods.EDITSTREAM64 editstream2 = this.ConvertToEDITSTREAM64(editstream);
					UnsafeNativeMethods.SendMessage(new HandleRef(this, base.Handle), 1098, flags, editstream2);
					editstream.dwError = this.GetErrorValue64(editstream2);
				}
				else
				{
					UnsafeNativeMethods.SendMessage(new HandleRef(this, base.Handle), 1098, flags, editstream);
				}
				if (editstream.dwError != 0)
				{
					throw new InvalidOperationException(SR.GetString("SaveTextError"));
				}
			}
			finally
			{
				this.editStream = null;
			}
		}

		// Token: 0x060036A4 RID: 13988 RVA: 0x000F6A78 File Offset: 0x000F4C78
		private unsafe NativeMethods.EDITSTREAM64 ConvertToEDITSTREAM64(NativeMethods.EDITSTREAM es)
		{
			NativeMethods.EDITSTREAM64 editstream = new NativeMethods.EDITSTREAM64();
			fixed (byte* ptr = &editstream.contents[0])
			{
				byte* ptr2 = ptr;
				*(long*)ptr2 = (long)es.dwCookie;
				*(int*)(ptr2 + 8) = es.dwError;
				long num = (long)Marshal.GetFunctionPointerForDelegate(es.pfnCallback);
				byte* ptr3 = (byte*)(&num);
				for (int i = 0; i < 8; i++)
				{
					editstream.contents[i + 12] = ptr3[i];
				}
			}
			return editstream;
		}

		// Token: 0x060036A5 RID: 13989 RVA: 0x000F6AF0 File Offset: 0x000F4CF0
		private unsafe int GetErrorValue64(NativeMethods.EDITSTREAM64 es64)
		{
			int result;
			fixed (byte* ptr = &es64.contents[0])
			{
				byte* ptr2 = ptr;
				result = *(int*)(ptr2 + 8);
			}
			return result;
		}

		// Token: 0x060036A6 RID: 13990 RVA: 0x000F6B18 File Offset: 0x000F4D18
		private void UpdateOleCallback()
		{
			if (base.IsHandleCreated)
			{
				if (this.oleCallback == null)
				{
					bool flag = false;
					try
					{
						IntSecurity.UnmanagedCode.Demand();
						flag = true;
					}
					catch (SecurityException)
					{
						flag = false;
					}
					if (flag)
					{
						this.AllowOleObjects = true;
					}
					else
					{
						this.AllowOleObjects = ((int)((long)base.SendMessage(1294, 0, 1)) != 0);
					}
					this.oleCallback = this.CreateRichEditOleCallback();
					IntPtr iunknownForObject = Marshal.GetIUnknownForObject(this.oleCallback);
					try
					{
						Guid guid = typeof(UnsafeNativeMethods.IRichEditOleCallback).GUID;
						IntPtr intPtr;
						Marshal.QueryInterface(iunknownForObject, ref guid, out intPtr);
						try
						{
							UnsafeNativeMethods.SendCallbackMessage(new HandleRef(this, base.Handle), 1094, IntPtr.Zero, intPtr);
						}
						finally
						{
							Marshal.Release(intPtr);
						}
					}
					finally
					{
						Marshal.Release(iunknownForObject);
					}
				}
				UnsafeNativeMethods.DragAcceptFiles(new HandleRef(this, base.Handle), false);
			}
		}

		// Token: 0x060036A7 RID: 13991 RVA: 0x000F6C14 File Offset: 0x000F4E14
		private void UserPreferenceChangedHandler(object o, UserPreferenceChangedEventArgs e)
		{
			if (base.IsHandleCreated)
			{
				if (this.BackColor.IsSystemColor)
				{
					base.SendMessage(1091, 0, ColorTranslator.ToWin32(this.BackColor));
				}
				if (this.ForeColor.IsSystemColor)
				{
					this.InternalSetForeColor(this.ForeColor);
				}
			}
		}

		// Token: 0x060036A8 RID: 13992 RVA: 0x000F6C6E File Offset: 0x000F4E6E
		[SecurityPermission(SecurityAction.InheritanceDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		protected virtual object CreateRichEditOleCallback()
		{
			return new RichTextBox.OleCallback(this);
		}

		// Token: 0x060036A9 RID: 13993 RVA: 0x000F6C78 File Offset: 0x000F4E78
		private void EnLinkMsgHandler(ref Message m)
		{
			NativeMethods.ENLINK enlink;
			if (IntPtr.Size == 8)
			{
				enlink = RichTextBox.ConvertFromENLINK64((NativeMethods.ENLINK64)m.GetLParam(typeof(NativeMethods.ENLINK64)));
			}
			else
			{
				enlink = (NativeMethods.ENLINK)m.GetLParam(typeof(NativeMethods.ENLINK));
			}
			int msg = enlink.msg;
			if (msg == 32)
			{
				this.LinkCursor = true;
				m.Result = (IntPtr)1;
				return;
			}
			if (msg != 513)
			{
				m.Result = IntPtr.Zero;
				return;
			}
			string text = this.CharRangeToString(enlink.charrange);
			if (!string.IsNullOrEmpty(text))
			{
				this.OnLinkClicked(new LinkClickedEventArgs(text));
			}
			m.Result = (IntPtr)1;
		}

		// Token: 0x060036AA RID: 13994 RVA: 0x000F6D24 File Offset: 0x000F4F24
		private string CharRangeToString(NativeMethods.CHARRANGE c)
		{
			NativeMethods.TEXTRANGE textrange = new NativeMethods.TEXTRANGE();
			textrange.chrg = c;
			if (c.cpMax > this.Text.Length || c.cpMax - c.cpMin <= 0)
			{
				return string.Empty;
			}
			int size = c.cpMax - c.cpMin + 1;
			UnsafeNativeMethods.CharBuffer charBuffer = UnsafeNativeMethods.CharBuffer.CreateBuffer(size);
			IntPtr intPtr = charBuffer.AllocCoTaskMem();
			if (intPtr == IntPtr.Zero)
			{
				throw new OutOfMemoryException(SR.GetString("OutOfMemory"));
			}
			textrange.lpstrText = intPtr;
			int num = (int)UnsafeNativeMethods.SendMessage(new HandleRef(this, base.Handle), 1099, 0, textrange);
			charBuffer.PutCoTaskMem(intPtr);
			if (textrange.lpstrText != IntPtr.Zero)
			{
				Marshal.FreeCoTaskMem(intPtr);
			}
			return charBuffer.GetString();
		}

		// Token: 0x060036AB RID: 13995 RVA: 0x000F6DF1 File Offset: 0x000F4FF1
		internal override void UpdateMaxLength()
		{
			if (base.IsHandleCreated)
			{
				base.SendMessage(1077, 0, this.MaxLength);
			}
		}

		// Token: 0x060036AC RID: 13996 RVA: 0x000F6E10 File Offset: 0x000F5010
		private void WmReflectCommand(ref Message m)
		{
			if (!(m.LParam == base.Handle) || base.GetState(262144))
			{
				base.WndProc(ref m);
				return;
			}
			int num = NativeMethods.Util.HIWORD(m.WParam);
			if (num == 1537)
			{
				this.OnHScroll(EventArgs.Empty);
				return;
			}
			if (num != 1538)
			{
				base.WndProc(ref m);
				return;
			}
			this.OnVScroll(EventArgs.Empty);
		}

		// Token: 0x060036AD RID: 13997 RVA: 0x000F6E84 File Offset: 0x000F5084
		internal void WmReflectNotify(ref Message m)
		{
			if (m.HWnd == base.Handle)
			{
				NativeMethods.NMHDR nmhdr = (NativeMethods.NMHDR)m.GetLParam(typeof(NativeMethods.NMHDR));
				int code = nmhdr.code;
				switch (code)
				{
				case 1793:
					if (!this.CallOnContentsResized)
					{
						NativeMethods.REQRESIZE reqresize = (NativeMethods.REQRESIZE)m.GetLParam(typeof(NativeMethods.REQRESIZE));
						if (base.BorderStyle == BorderStyle.Fixed3D)
						{
							NativeMethods.REQRESIZE reqresize2 = reqresize;
							reqresize2.rc.bottom = reqresize2.rc.bottom + 1;
						}
						this.OnContentsResized(new ContentsResizedEventArgs(Rectangle.FromLTRB(reqresize.rc.left, reqresize.rc.top, reqresize.rc.right, reqresize.rc.bottom)));
						return;
					}
					break;
				case 1794:
				{
					NativeMethods.SELCHANGE selChange = (NativeMethods.SELCHANGE)m.GetLParam(typeof(NativeMethods.SELCHANGE));
					this.WmSelectionChange(selChange);
					return;
				}
				case 1795:
				{
					NativeMethods.ENDROPFILES endropfiles = (NativeMethods.ENDROPFILES)m.GetLParam(typeof(NativeMethods.ENDROPFILES));
					StringBuilder stringBuilder = new StringBuilder(260);
					if (UnsafeNativeMethods.DragQueryFileLongPath(new HandleRef(endropfiles, endropfiles.hDrop), 0, stringBuilder) != 0)
					{
						try
						{
							this.LoadFile(stringBuilder.ToString(), RichTextBoxStreamType.RichText);
						}
						catch
						{
							try
							{
								this.LoadFile(stringBuilder.ToString(), RichTextBoxStreamType.PlainText);
							}
							catch
							{
							}
						}
					}
					m.Result = (IntPtr)1;
					return;
				}
				case 1796:
				{
					NativeMethods.ENPROTECTED enprotected;
					if (IntPtr.Size == 8)
					{
						enprotected = this.ConvertFromENPROTECTED64((NativeMethods.ENPROTECTED64)m.GetLParam(typeof(NativeMethods.ENPROTECTED64)));
					}
					else
					{
						enprotected = (NativeMethods.ENPROTECTED)m.GetLParam(typeof(NativeMethods.ENPROTECTED));
					}
					int msg = enprotected.msg;
					if (msg <= 769)
					{
						if (msg != 12)
						{
							if (msg == 194)
							{
								goto IL_277;
							}
							if (msg != 769)
							{
								goto IL_270;
							}
						}
					}
					else if (msg <= 1092)
					{
						if (msg != 1077)
						{
							if (msg != 1092)
							{
								goto IL_270;
							}
							NativeMethods.CHARFORMATA charformata = (NativeMethods.CHARFORMATA)UnsafeNativeMethods.PtrToStructure(enprotected.lParam, typeof(NativeMethods.CHARFORMATA));
							if ((charformata.dwMask & 16) != 0)
							{
								m.Result = IntPtr.Zero;
								return;
							}
							goto IL_277;
						}
					}
					else
					{
						if (msg == 1095)
						{
							goto IL_277;
						}
						if (msg != 1097)
						{
							goto IL_270;
						}
						if (((int)((long)enprotected.wParam) & 32768) == 0)
						{
							m.Result = IntPtr.Zero;
							return;
						}
						goto IL_277;
					}
					m.Result = IntPtr.Zero;
					return;
					IL_270:
					SafeNativeMethods.MessageBeep(0);
					IL_277:
					this.OnProtected(EventArgs.Empty);
					m.Result = (IntPtr)1;
					return;
				}
				default:
					if (code == 1803)
					{
						this.EnLinkMsgHandler(ref m);
						return;
					}
					base.WndProc(ref m);
					return;
				}
			}
			else
			{
				base.WndProc(ref m);
			}
		}

		// Token: 0x060036AE RID: 13998 RVA: 0x000F714C File Offset: 0x000F534C
		private unsafe NativeMethods.ENPROTECTED ConvertFromENPROTECTED64(NativeMethods.ENPROTECTED64 es64)
		{
			NativeMethods.ENPROTECTED enprotected = new NativeMethods.ENPROTECTED();
			fixed (byte* ptr = &es64.contents[0])
			{
				byte* ptr2 = ptr;
				enprotected.nmhdr = default(NativeMethods.NMHDR);
				enprotected.chrg = new NativeMethods.CHARRANGE();
				enprotected.nmhdr.hwndFrom = Marshal.ReadIntPtr((IntPtr)((void*)ptr2));
				enprotected.nmhdr.idFrom = Marshal.ReadIntPtr((IntPtr)((void*)(ptr2 + 8)));
				enprotected.nmhdr.code = Marshal.ReadInt32((IntPtr)((void*)(ptr2 + 16)));
				enprotected.msg = Marshal.ReadInt32((IntPtr)((void*)(ptr2 + 24)));
				enprotected.wParam = Marshal.ReadIntPtr((IntPtr)((void*)(ptr2 + 28)));
				enprotected.lParam = Marshal.ReadIntPtr((IntPtr)((void*)(ptr2 + 36)));
				enprotected.chrg.cpMin = Marshal.ReadInt32((IntPtr)((void*)(ptr2 + 44)));
				enprotected.chrg.cpMax = Marshal.ReadInt32((IntPtr)((void*)(ptr2 + 48)));
			}
			return enprotected;
		}

		// Token: 0x060036AF RID: 13999 RVA: 0x000F7240 File Offset: 0x000F5440
		private unsafe static NativeMethods.ENLINK ConvertFromENLINK64(NativeMethods.ENLINK64 es64)
		{
			NativeMethods.ENLINK enlink = new NativeMethods.ENLINK();
			fixed (byte* ptr = &es64.contents[0])
			{
				byte* ptr2 = ptr;
				enlink.nmhdr = default(NativeMethods.NMHDR);
				enlink.charrange = new NativeMethods.CHARRANGE();
				enlink.nmhdr.hwndFrom = Marshal.ReadIntPtr((IntPtr)((void*)ptr2));
				enlink.nmhdr.idFrom = Marshal.ReadIntPtr((IntPtr)((void*)(ptr2 + 8)));
				enlink.nmhdr.code = Marshal.ReadInt32((IntPtr)((void*)(ptr2 + 16)));
				enlink.msg = Marshal.ReadInt32((IntPtr)((void*)(ptr2 + 24)));
				enlink.wParam = Marshal.ReadIntPtr((IntPtr)((void*)(ptr2 + 28)));
				enlink.lParam = Marshal.ReadIntPtr((IntPtr)((void*)(ptr2 + 36)));
				enlink.charrange.cpMin = Marshal.ReadInt32((IntPtr)((void*)(ptr2 + 44)));
				enlink.charrange.cpMax = Marshal.ReadInt32((IntPtr)((void*)(ptr2 + 48)));
			}
			return enlink;
		}

		// Token: 0x060036B0 RID: 14000 RVA: 0x000F7334 File Offset: 0x000F5534
		private void WmSelectionChange(NativeMethods.SELCHANGE selChange)
		{
			int cpMin = selChange.chrg.cpMin;
			int cpMax = selChange.chrg.cpMax;
			short num = (short)selChange.seltyp;
			if (base.ImeMode == ImeMode.Hangul || base.ImeMode == ImeMode.HangulFull)
			{
				int num2 = (int)((long)base.SendMessage(1146, 0, 0));
				if (num2 != 0)
				{
					int windowTextLength = SafeNativeMethods.GetWindowTextLength(new HandleRef(this, base.Handle));
					if (cpMin == cpMax && windowTextLength == this.MaxLength)
					{
						base.SendMessage(8, 0, 0);
						base.SendMessage(7, 0, 0);
						UnsafeNativeMethods.PostMessage(new HandleRef(this, base.Handle), 177, cpMax - 1, cpMax);
					}
				}
			}
			if (cpMin != this.curSelStart || cpMax != this.curSelEnd || num != this.curSelType)
			{
				this.curSelStart = cpMin;
				this.curSelEnd = cpMax;
				this.curSelType = num;
				this.OnSelectionChanged(EventArgs.Empty);
			}
		}

		// Token: 0x060036B1 RID: 14001 RVA: 0x000F7418 File Offset: 0x000F5618
		private void WmSetFont(ref Message m)
		{
			try
			{
				this.SuppressTextChangedEvent = true;
				base.WndProc(ref m);
			}
			finally
			{
				this.SuppressTextChangedEvent = false;
			}
			this.InternalSetForeColor(this.ForeColor);
		}

		// Token: 0x060036B2 RID: 14002 RVA: 0x000F745C File Offset: 0x000F565C
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		protected override void WndProc(ref Message m)
		{
			int msg = m.Msg;
			if (msg <= 276)
			{
				if (msg <= 48)
				{
					if (msg != 32)
					{
						if (msg == 48)
						{
							this.WmSetFont(ref m);
							return;
						}
					}
					else
					{
						this.LinkCursor = false;
						this.DefWndProc(ref m);
						if (this.LinkCursor && !this.Cursor.Equals(Cursors.WaitCursor))
						{
							UnsafeNativeMethods.SetCursor(new HandleRef(Cursors.Hand, Cursors.Hand.Handle));
							m.Result = (IntPtr)1;
							return;
						}
						base.WndProc(ref m);
						return;
					}
				}
				else if (msg != 61)
				{
					if (msg == 135)
					{
						base.WndProc(ref m);
						m.Result = (IntPtr)(base.AcceptsTab ? ((int)((long)m.Result) | 2) : ((int)((long)m.Result) & -3));
						return;
					}
					if (msg == 276)
					{
						base.WndProc(ref m);
						int num = NativeMethods.Util.LOWORD(m.WParam);
						if (num == 5)
						{
							this.OnHScroll(EventArgs.Empty);
						}
						if (num == 4)
						{
							this.OnHScroll(EventArgs.Empty);
							return;
						}
						return;
					}
				}
				else
				{
					base.WndProc(ref m);
					if ((int)((long)m.LParam) == -12)
					{
						m.Result = (IntPtr)((Marshal.SystemDefaultCharSize == 1) ? 65565 : 65566);
						return;
					}
					return;
				}
			}
			else if (msg <= 517)
			{
				if (msg != 277)
				{
					if (msg == 517)
					{
						bool style = base.GetStyle(ControlStyles.UserMouse);
						base.SetStyle(ControlStyles.UserMouse, true);
						base.WndProc(ref m);
						base.SetStyle(ControlStyles.UserMouse, style);
						return;
					}
				}
				else
				{
					base.WndProc(ref m);
					int num = NativeMethods.Util.LOWORD(m.WParam);
					if (num == 5)
					{
						this.OnVScroll(EventArgs.Empty);
						return;
					}
					if (num == 4)
					{
						this.OnVScroll(EventArgs.Empty);
						return;
					}
					return;
				}
			}
			else
			{
				if (msg == 642)
				{
					this.OnImeChange(EventArgs.Empty);
					base.WndProc(ref m);
					return;
				}
				if (msg == 8270)
				{
					this.WmReflectNotify(ref m);
					return;
				}
				if (msg == 8465)
				{
					this.WmReflectCommand(ref m);
					return;
				}
			}
			base.WndProc(ref m);
		}

		// Token: 0x04001F78 RID: 8056
		private static TraceSwitch richTextDbg;

		// Token: 0x04001F79 RID: 8057
		private const int DV_E_DVASPECT = -2147221397;

		// Token: 0x04001F7A RID: 8058
		private const int DVASPECT_CONTENT = 1;

		// Token: 0x04001F7B RID: 8059
		private const int DVASPECT_THUMBNAIL = 2;

		// Token: 0x04001F7C RID: 8060
		private const int DVASPECT_ICON = 4;

		// Token: 0x04001F7D RID: 8061
		private const int DVASPECT_DOCPRINT = 8;

		// Token: 0x04001F7E RID: 8062
		internal const int INPUT = 1;

		// Token: 0x04001F7F RID: 8063
		internal const int OUTPUT = 2;

		// Token: 0x04001F80 RID: 8064
		internal const int DIRECTIONMASK = 3;

		// Token: 0x04001F81 RID: 8065
		internal const int ANSI = 4;

		// Token: 0x04001F82 RID: 8066
		internal const int UNICODE = 8;

		// Token: 0x04001F83 RID: 8067
		internal const int FORMATMASK = 12;

		// Token: 0x04001F84 RID: 8068
		internal const int TEXTLF = 16;

		// Token: 0x04001F85 RID: 8069
		internal const int TEXTCRLF = 32;

		// Token: 0x04001F86 RID: 8070
		internal const int RTF = 64;

		// Token: 0x04001F87 RID: 8071
		internal const int KINDMASK = 112;

		// Token: 0x04001F88 RID: 8072
		private static IntPtr moduleHandle;

		// Token: 0x04001F89 RID: 8073
		private static readonly string SZ_RTF_TAG = "{\\rtf";

		// Token: 0x04001F8A RID: 8074
		private const int CHAR_BUFFER_LEN = 512;

		// Token: 0x04001F8B RID: 8075
		private static readonly object EVENT_HSCROLL = new object();

		// Token: 0x04001F8C RID: 8076
		private static readonly object EVENT_LINKACTIVATE = new object();

		// Token: 0x04001F8D RID: 8077
		private static readonly object EVENT_IMECHANGE = new object();

		// Token: 0x04001F8E RID: 8078
		private static readonly object EVENT_PROTECTED = new object();

		// Token: 0x04001F8F RID: 8079
		private static readonly object EVENT_REQUESTRESIZE = new object();

		// Token: 0x04001F90 RID: 8080
		private static readonly object EVENT_SELCHANGE = new object();

		// Token: 0x04001F91 RID: 8081
		private static readonly object EVENT_VSCROLL = new object();

		// Token: 0x04001F92 RID: 8082
		private int bulletIndent;

		// Token: 0x04001F93 RID: 8083
		private int rightMargin;

		// Token: 0x04001F94 RID: 8084
		private string textRtf;

		// Token: 0x04001F95 RID: 8085
		private string textPlain;

		// Token: 0x04001F96 RID: 8086
		private Color selectionBackColorToSetOnHandleCreated;

		// Token: 0x04001F97 RID: 8087
		private RichTextBoxLanguageOptions languageOption = RichTextBoxLanguageOptions.AutoFont | RichTextBoxLanguageOptions.DualFont;

		// Token: 0x04001F98 RID: 8088
		private static int logPixelsX;

		// Token: 0x04001F99 RID: 8089
		private static int logPixelsY;

		// Token: 0x04001F9A RID: 8090
		private Stream editStream;

		// Token: 0x04001F9B RID: 8091
		private float zoomMultiplier = 1f;

		// Token: 0x04001F9C RID: 8092
		private int curSelStart;

		// Token: 0x04001F9D RID: 8093
		private int curSelEnd;

		// Token: 0x04001F9E RID: 8094
		private short curSelType;

		// Token: 0x04001F9F RID: 8095
		private object oleCallback;

		// Token: 0x04001FA0 RID: 8096
		private static int[] shortcutsToDisable;

		// Token: 0x04001FA1 RID: 8097
		private static int richEditMajorVersion = 3;

		// Token: 0x04001FA2 RID: 8098
		private BitVector32 richTextBoxFlags;

		// Token: 0x04001FA3 RID: 8099
		private static readonly BitVector32.Section autoWordSelectionSection = BitVector32.CreateSection(1);

		// Token: 0x04001FA4 RID: 8100
		private static readonly BitVector32.Section showSelBarSection = BitVector32.CreateSection(1, RichTextBox.autoWordSelectionSection);

		// Token: 0x04001FA5 RID: 8101
		private static readonly BitVector32.Section autoUrlDetectSection = BitVector32.CreateSection(1, RichTextBox.showSelBarSection);

		// Token: 0x04001FA6 RID: 8102
		private static readonly BitVector32.Section fInCtorSection = BitVector32.CreateSection(1, RichTextBox.autoUrlDetectSection);

		// Token: 0x04001FA7 RID: 8103
		private static readonly BitVector32.Section protectedErrorSection = BitVector32.CreateSection(1, RichTextBox.fInCtorSection);

		// Token: 0x04001FA8 RID: 8104
		private static readonly BitVector32.Section linkcursorSection = BitVector32.CreateSection(1, RichTextBox.protectedErrorSection);

		// Token: 0x04001FA9 RID: 8105
		private static readonly BitVector32.Section allowOleDropSection = BitVector32.CreateSection(1, RichTextBox.linkcursorSection);

		// Token: 0x04001FAA RID: 8106
		private static readonly BitVector32.Section suppressTextChangedEventSection = BitVector32.CreateSection(1, RichTextBox.allowOleDropSection);

		// Token: 0x04001FAB RID: 8107
		private static readonly BitVector32.Section callOnContentsResizedSection = BitVector32.CreateSection(1, RichTextBox.suppressTextChangedEventSection);

		// Token: 0x04001FAC RID: 8108
		private static readonly BitVector32.Section richTextShortcutsEnabledSection = BitVector32.CreateSection(1, RichTextBox.callOnContentsResizedSection);

		// Token: 0x04001FAD RID: 8109
		private static readonly BitVector32.Section allowOleObjectsSection = BitVector32.CreateSection(1, RichTextBox.richTextShortcutsEnabledSection);

		// Token: 0x04001FAE RID: 8110
		private static readonly BitVector32.Section scrollBarsSection = BitVector32.CreateSection(19, RichTextBox.allowOleObjectsSection);

		// Token: 0x04001FAF RID: 8111
		private static readonly BitVector32.Section enableAutoDragDropSection = BitVector32.CreateSection(1, RichTextBox.scrollBarsSection);

		// Token: 0x020007DB RID: 2011
		private class OleCallback : UnsafeNativeMethods.IRichEditOleCallback
		{
			// Token: 0x06006DB5 RID: 28085 RVA: 0x00192D85 File Offset: 0x00190F85
			internal OleCallback(RichTextBox owner)
			{
				this.owner = owner;
			}

			// Token: 0x06006DB6 RID: 28086 RVA: 0x00192D94 File Offset: 0x00190F94
			public int GetNewStorage(out UnsafeNativeMethods.IStorage storage)
			{
				if (!this.owner.AllowOleObjects)
				{
					storage = null;
					return -2147467259;
				}
				UnsafeNativeMethods.ILockBytes iLockBytes = UnsafeNativeMethods.CreateILockBytesOnHGlobal(NativeMethods.NullHandleRef, true);
				storage = UnsafeNativeMethods.StgCreateDocfileOnILockBytes(iLockBytes, 4114, 0);
				return 0;
			}

			// Token: 0x06006DB7 RID: 28087 RVA: 0x0003BE48 File Offset: 0x0003A048
			public int GetInPlaceContext(IntPtr lplpFrame, IntPtr lplpDoc, IntPtr lpFrameInfo)
			{
				return -2147467263;
			}

			// Token: 0x06006DB8 RID: 28088 RVA: 0x00011A20 File Offset: 0x0000FC20
			public int ShowContainerUI(int fShow)
			{
				return 0;
			}

			// Token: 0x06006DB9 RID: 28089 RVA: 0x00192DD4 File Offset: 0x00190FD4
			public int QueryInsertObject(ref Guid lpclsid, IntPtr lpstg, int cp)
			{
				try
				{
					IntSecurity.UnmanagedCode.Demand();
					return 0;
				}
				catch (SecurityException)
				{
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

			// Token: 0x06006DBA RID: 28090 RVA: 0x00011A20 File Offset: 0x0000FC20
			public int DeleteObject(IntPtr lpoleobj)
			{
				return 0;
			}

			// Token: 0x06006DBB RID: 28091 RVA: 0x00192E8C File Offset: 0x0019108C
			public int QueryAcceptData(IDataObject lpdataobj, IntPtr lpcfFormat, int reco, int fReally, IntPtr hMetaPict)
			{
				if (reco != 1)
				{
					return -2147467263;
				}
				if (!this.owner.AllowDrop && !this.owner.EnableAutoDragDrop)
				{
					this.lastDataObject = null;
					return -2147467259;
				}
				MouseButtons mouseButtons = Control.MouseButtons;
				Keys modifierKeys = Control.ModifierKeys;
				int num = 0;
				if ((mouseButtons & MouseButtons.Left) == MouseButtons.Left)
				{
					num |= 1;
				}
				if ((mouseButtons & MouseButtons.Right) == MouseButtons.Right)
				{
					num |= 2;
				}
				if ((mouseButtons & MouseButtons.Middle) == MouseButtons.Middle)
				{
					num |= 16;
				}
				if ((modifierKeys & Keys.Control) == Keys.Control)
				{
					num |= 8;
				}
				if ((modifierKeys & Keys.Shift) == Keys.Shift)
				{
					num |= 4;
				}
				this.lastDataObject = new DataObject(lpdataobj);
				if (!this.owner.EnableAutoDragDrop)
				{
					this.lastEffect = DragDropEffects.None;
				}
				DragEventArgs dragEventArgs = new DragEventArgs(this.lastDataObject, num, Control.MousePosition.X, Control.MousePosition.Y, DragDropEffects.All, this.lastEffect);
				if (fReally == 0)
				{
					dragEventArgs.Effect = (((num & 8) == 8) ? DragDropEffects.Copy : DragDropEffects.Move);
					this.owner.OnDragEnter(dragEventArgs);
				}
				else
				{
					this.owner.OnDragDrop(dragEventArgs);
					this.lastDataObject = null;
				}
				this.lastEffect = dragEventArgs.Effect;
				if (dragEventArgs.Effect == DragDropEffects.None)
				{
					return -2147467259;
				}
				return 0;
			}

			// Token: 0x06006DBC RID: 28092 RVA: 0x0003BE48 File Offset: 0x0003A048
			public int ContextSensitiveHelp(int fEnterMode)
			{
				return -2147467263;
			}

			// Token: 0x06006DBD RID: 28093 RVA: 0x0003BE48 File Offset: 0x0003A048
			public int GetClipboardData(NativeMethods.CHARRANGE lpchrg, int reco, IntPtr lplpdataobj)
			{
				return -2147467263;
			}

			// Token: 0x06006DBE RID: 28094 RVA: 0x00192FE0 File Offset: 0x001911E0
			public int GetDragDropEffect(bool fDrag, int grfKeyState, ref int pdwEffect)
			{
				if (this.owner.AllowDrop || this.owner.EnableAutoDragDrop)
				{
					if (fDrag && grfKeyState == 0)
					{
						if (this.owner.EnableAutoDragDrop)
						{
							this.lastEffect = DragDropEffects.All;
						}
						else
						{
							this.lastEffect = DragDropEffects.None;
						}
					}
					else if (!fDrag && this.lastDataObject != null && grfKeyState != 0)
					{
						DragEventArgs dragEventArgs = new DragEventArgs(this.lastDataObject, grfKeyState, Control.MousePosition.X, Control.MousePosition.Y, DragDropEffects.All, this.lastEffect);
						if (this.lastEffect != DragDropEffects.None)
						{
							dragEventArgs.Effect = (((grfKeyState & 8) == 8) ? DragDropEffects.Copy : DragDropEffects.Move);
						}
						this.owner.OnDragOver(dragEventArgs);
						this.lastEffect = dragEventArgs.Effect;
					}
					pdwEffect = (int)this.lastEffect;
				}
				else
				{
					pdwEffect = 0;
				}
				return 0;
			}

			// Token: 0x06006DBF RID: 28095 RVA: 0x001930B4 File Offset: 0x001912B4
			public int GetContextMenu(short seltype, IntPtr lpoleobj, NativeMethods.CHARRANGE lpchrg, out IntPtr hmenu)
			{
				ContextMenu contextMenu = this.owner.ContextMenu;
				if (contextMenu == null || !this.owner.ShortcutsEnabled)
				{
					hmenu = IntPtr.Zero;
				}
				else
				{
					contextMenu.sourceControl = this.owner;
					contextMenu.OnPopup(EventArgs.Empty);
					IntPtr handle = contextMenu.Handle;
					Menu menu = contextMenu;
					for (;;)
					{
						int i = 0;
						int itemCount = menu.ItemCount;
						while (i < itemCount)
						{
							if (menu.items[i].handle != IntPtr.Zero)
							{
								menu = menu.items[i];
								break;
							}
							i++;
						}
						if (i == itemCount)
						{
							menu.handle = IntPtr.Zero;
							menu.created = false;
							if (menu == contextMenu)
							{
								break;
							}
							menu = ((MenuItem)menu).Menu;
						}
					}
					hmenu = handle;
				}
				return 0;
			}

			// Token: 0x040042B7 RID: 17079
			private RichTextBox owner;

			// Token: 0x040042B8 RID: 17080
			private IDataObject lastDataObject;

			// Token: 0x040042B9 RID: 17081
			private DragDropEffects lastEffect;
		}
	}
}
