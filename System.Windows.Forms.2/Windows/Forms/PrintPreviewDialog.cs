using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Printing;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace System.Windows.Forms
{
	// Token: 0x0200044D RID: 1101
	[ComVisible(true)]
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[Designer("System.ComponentModel.Design.ComponentDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[DesignTimeVisible(true)]
	[DefaultProperty("Document")]
	[ToolboxItemFilter("System.Windows.Forms.Control.TopLevel")]
	[ToolboxItem(true)]
	[SRDescription("DescriptionPrintPreviewDialog")]
	public partial class PrintPreviewDialog : Form
	{
		// Token: 0x06004C85 RID: 19589 RVA: 0x0013E248 File Offset: 0x0013C448
		public PrintPreviewDialog()
		{
			base.AutoScaleBaseSize = new Size(5, 13);
			this.previewControl = new PrintPreviewControl();
			this.imageList = new ImageList();
			Bitmap bitmap = new Bitmap(typeof(PrintPreviewDialog), "PrintPreviewStrip.bmp");
			bitmap.MakeTransparent();
			this.imageList.Images.AddStrip(bitmap);
			this.InitForm();
		}

		// Token: 0x170012BB RID: 4795
		// (get) Token: 0x06004C86 RID: 19590 RVA: 0x0013E2B2 File Offset: 0x0013C4B2
		// (set) Token: 0x06004C87 RID: 19591 RVA: 0x0013E2BA File Offset: 0x0013C4BA
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new IButtonControl AcceptButton
		{
			get
			{
				return base.AcceptButton;
			}
			set
			{
				base.AcceptButton = value;
			}
		}

		// Token: 0x170012BC RID: 4796
		// (get) Token: 0x06004C88 RID: 19592 RVA: 0x0013E2C3 File Offset: 0x0013C4C3
		// (set) Token: 0x06004C89 RID: 19593 RVA: 0x0013E2CB File Offset: 0x0013C4CB
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new bool AutoScale
		{
			get
			{
				return base.AutoScale;
			}
			set
			{
				base.AutoScale = value;
			}
		}

		// Token: 0x170012BD RID: 4797
		// (get) Token: 0x06004C8A RID: 19594 RVA: 0x0013E2D4 File Offset: 0x0013C4D4
		// (set) Token: 0x06004C8B RID: 19595 RVA: 0x0013E2DC File Offset: 0x0013C4DC
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override bool AutoScroll
		{
			get
			{
				return base.AutoScroll;
			}
			set
			{
				base.AutoScroll = value;
			}
		}

		// Token: 0x170012BE RID: 4798
		// (get) Token: 0x06004C8C RID: 19596 RVA: 0x0010927E File Offset: 0x0010747E
		// (set) Token: 0x06004C8D RID: 19597 RVA: 0x00109286 File Offset: 0x00107486
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
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

		// Token: 0x140003F0 RID: 1008
		// (add) Token: 0x06004C8E RID: 19598 RVA: 0x0010928F File Offset: 0x0010748F
		// (remove) Token: 0x06004C8F RID: 19599 RVA: 0x00109298 File Offset: 0x00107498
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler AutoSizeChanged
		{
			add
			{
				base.AutoSizeChanged += value;
			}
			remove
			{
				base.AutoSizeChanged -= value;
			}
		}

		// Token: 0x170012BF RID: 4799
		// (get) Token: 0x06004C90 RID: 19600 RVA: 0x0013E2E5 File Offset: 0x0013C4E5
		// (set) Token: 0x06004C91 RID: 19601 RVA: 0x0013E2ED File Offset: 0x0013C4ED
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override AutoValidate AutoValidate
		{
			get
			{
				return base.AutoValidate;
			}
			set
			{
				base.AutoValidate = value;
			}
		}

		// Token: 0x140003F1 RID: 1009
		// (add) Token: 0x06004C92 RID: 19602 RVA: 0x0013E2F6 File Offset: 0x0013C4F6
		// (remove) Token: 0x06004C93 RID: 19603 RVA: 0x0013E2FF File Offset: 0x0013C4FF
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler AutoValidateChanged
		{
			add
			{
				base.AutoValidateChanged += value;
			}
			remove
			{
				base.AutoValidateChanged -= value;
			}
		}

		// Token: 0x170012C0 RID: 4800
		// (get) Token: 0x06004C94 RID: 19604 RVA: 0x0013E308 File Offset: 0x0013C508
		// (set) Token: 0x06004C95 RID: 19605 RVA: 0x0013E310 File Offset: 0x0013C510
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override Color BackColor
		{
			get
			{
				return base.BackColor;
			}
			set
			{
				base.BackColor = value;
			}
		}

		// Token: 0x140003F2 RID: 1010
		// (add) Token: 0x06004C96 RID: 19606 RVA: 0x00058DD2 File Offset: 0x00056FD2
		// (remove) Token: 0x06004C97 RID: 19607 RVA: 0x00058DDB File Offset: 0x00056FDB
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler BackColorChanged
		{
			add
			{
				base.BackColorChanged += value;
			}
			remove
			{
				base.BackColorChanged -= value;
			}
		}

		// Token: 0x170012C1 RID: 4801
		// (get) Token: 0x06004C98 RID: 19608 RVA: 0x0013E319 File Offset: 0x0013C519
		// (set) Token: 0x06004C99 RID: 19609 RVA: 0x0013E321 File Offset: 0x0013C521
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new IButtonControl CancelButton
		{
			get
			{
				return base.CancelButton;
			}
			set
			{
				base.CancelButton = value;
			}
		}

		// Token: 0x170012C2 RID: 4802
		// (get) Token: 0x06004C9A RID: 19610 RVA: 0x0013E32A File Offset: 0x0013C52A
		// (set) Token: 0x06004C9B RID: 19611 RVA: 0x0013E332 File Offset: 0x0013C532
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new bool ControlBox
		{
			get
			{
				return base.ControlBox;
			}
			set
			{
				base.ControlBox = value;
			}
		}

		// Token: 0x170012C3 RID: 4803
		// (get) Token: 0x06004C9C RID: 19612 RVA: 0x00011B4A File Offset: 0x0000FD4A
		// (set) Token: 0x06004C9D RID: 19613 RVA: 0x00112D8E File Offset: 0x00110F8E
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override ContextMenuStrip ContextMenuStrip
		{
			get
			{
				return base.ContextMenuStrip;
			}
			set
			{
				base.ContextMenuStrip = value;
			}
		}

		// Token: 0x140003F3 RID: 1011
		// (add) Token: 0x06004C9E RID: 19614 RVA: 0x00112D97 File Offset: 0x00110F97
		// (remove) Token: 0x06004C9F RID: 19615 RVA: 0x00112DA0 File Offset: 0x00110FA0
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler ContextMenuStripChanged
		{
			add
			{
				base.ContextMenuStripChanged += value;
			}
			remove
			{
				base.ContextMenuStripChanged -= value;
			}
		}

		// Token: 0x170012C4 RID: 4804
		// (get) Token: 0x06004CA0 RID: 19616 RVA: 0x0013E33B File Offset: 0x0013C53B
		// (set) Token: 0x06004CA1 RID: 19617 RVA: 0x0013E343 File Offset: 0x0013C543
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new FormBorderStyle FormBorderStyle
		{
			get
			{
				return base.FormBorderStyle;
			}
			set
			{
				base.FormBorderStyle = value;
			}
		}

		// Token: 0x170012C5 RID: 4805
		// (get) Token: 0x06004CA2 RID: 19618 RVA: 0x0013E34C File Offset: 0x0013C54C
		// (set) Token: 0x06004CA3 RID: 19619 RVA: 0x0013E354 File Offset: 0x0013C554
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new bool HelpButton
		{
			get
			{
				return base.HelpButton;
			}
			set
			{
				base.HelpButton = value;
			}
		}

		// Token: 0x170012C6 RID: 4806
		// (get) Token: 0x06004CA4 RID: 19620 RVA: 0x0013E35D File Offset: 0x0013C55D
		// (set) Token: 0x06004CA5 RID: 19621 RVA: 0x0013E365 File Offset: 0x0013C565
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new Icon Icon
		{
			get
			{
				return base.Icon;
			}
			set
			{
				base.Icon = value;
			}
		}

		// Token: 0x170012C7 RID: 4807
		// (get) Token: 0x06004CA6 RID: 19622 RVA: 0x0013E36E File Offset: 0x0013C56E
		// (set) Token: 0x06004CA7 RID: 19623 RVA: 0x0013E376 File Offset: 0x0013C576
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new bool IsMdiContainer
		{
			get
			{
				return base.IsMdiContainer;
			}
			set
			{
				base.IsMdiContainer = value;
			}
		}

		// Token: 0x170012C8 RID: 4808
		// (get) Token: 0x06004CA8 RID: 19624 RVA: 0x0013E37F File Offset: 0x0013C57F
		// (set) Token: 0x06004CA9 RID: 19625 RVA: 0x0013E387 File Offset: 0x0013C587
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new bool KeyPreview
		{
			get
			{
				return base.KeyPreview;
			}
			set
			{
				base.KeyPreview = value;
			}
		}

		// Token: 0x170012C9 RID: 4809
		// (get) Token: 0x06004CAA RID: 19626 RVA: 0x0013E390 File Offset: 0x0013C590
		// (set) Token: 0x06004CAB RID: 19627 RVA: 0x0013E398 File Offset: 0x0013C598
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new Size MaximumSize
		{
			get
			{
				return base.MaximumSize;
			}
			set
			{
				base.MaximumSize = value;
			}
		}

		// Token: 0x140003F4 RID: 1012
		// (add) Token: 0x06004CAC RID: 19628 RVA: 0x0013E3A1 File Offset: 0x0013C5A1
		// (remove) Token: 0x06004CAD RID: 19629 RVA: 0x0013E3AA File Offset: 0x0013C5AA
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler MaximumSizeChanged
		{
			add
			{
				base.MaximumSizeChanged += value;
			}
			remove
			{
				base.MaximumSizeChanged -= value;
			}
		}

		// Token: 0x170012CA RID: 4810
		// (get) Token: 0x06004CAE RID: 19630 RVA: 0x0013E3B3 File Offset: 0x0013C5B3
		// (set) Token: 0x06004CAF RID: 19631 RVA: 0x0013E3BB File Offset: 0x0013C5BB
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new bool MaximizeBox
		{
			get
			{
				return base.MaximizeBox;
			}
			set
			{
				base.MaximizeBox = value;
			}
		}

		// Token: 0x170012CB RID: 4811
		// (get) Token: 0x06004CB0 RID: 19632 RVA: 0x0013E3C4 File Offset: 0x0013C5C4
		// (set) Token: 0x06004CB1 RID: 19633 RVA: 0x0013E3CC File Offset: 0x0013C5CC
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new Padding Margin
		{
			get
			{
				return base.Margin;
			}
			set
			{
				base.Margin = value;
			}
		}

		// Token: 0x140003F5 RID: 1013
		// (add) Token: 0x06004CB2 RID: 19634 RVA: 0x0013E3D5 File Offset: 0x0013C5D5
		// (remove) Token: 0x06004CB3 RID: 19635 RVA: 0x0013E3DE File Offset: 0x0013C5DE
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler MarginChanged
		{
			add
			{
				base.MarginChanged += value;
			}
			remove
			{
				base.MarginChanged -= value;
			}
		}

		// Token: 0x170012CC RID: 4812
		// (get) Token: 0x06004CB4 RID: 19636 RVA: 0x0013E3E7 File Offset: 0x0013C5E7
		// (set) Token: 0x06004CB5 RID: 19637 RVA: 0x0013E3EF File Offset: 0x0013C5EF
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new MainMenu Menu
		{
			get
			{
				return base.Menu;
			}
			set
			{
				base.Menu = value;
			}
		}

		// Token: 0x170012CD RID: 4813
		// (get) Token: 0x06004CB6 RID: 19638 RVA: 0x0013E3F8 File Offset: 0x0013C5F8
		// (set) Token: 0x06004CB7 RID: 19639 RVA: 0x0013E400 File Offset: 0x0013C600
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new Size MinimumSize
		{
			get
			{
				return base.MinimumSize;
			}
			set
			{
				base.MinimumSize = value;
			}
		}

		// Token: 0x140003F6 RID: 1014
		// (add) Token: 0x06004CB8 RID: 19640 RVA: 0x0013E409 File Offset: 0x0013C609
		// (remove) Token: 0x06004CB9 RID: 19641 RVA: 0x0013E412 File Offset: 0x0013C612
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler MinimumSizeChanged
		{
			add
			{
				base.MinimumSizeChanged += value;
			}
			remove
			{
				base.MinimumSizeChanged -= value;
			}
		}

		// Token: 0x170012CE RID: 4814
		// (get) Token: 0x06004CBA RID: 19642 RVA: 0x00013656 File Offset: 0x00011856
		// (set) Token: 0x06004CBB RID: 19643 RVA: 0x0001365E File Offset: 0x0001185E
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
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

		// Token: 0x140003F7 RID: 1015
		// (add) Token: 0x06004CBC RID: 19644 RVA: 0x00013667 File Offset: 0x00011867
		// (remove) Token: 0x06004CBD RID: 19645 RVA: 0x00013670 File Offset: 0x00011870
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

		// Token: 0x170012CF RID: 4815
		// (get) Token: 0x06004CBE RID: 19646 RVA: 0x0013E41B File Offset: 0x0013C61B
		// (set) Token: 0x06004CBF RID: 19647 RVA: 0x0013E423 File Offset: 0x0013C623
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new Size Size
		{
			get
			{
				return base.Size;
			}
			set
			{
				base.Size = value;
			}
		}

		// Token: 0x140003F8 RID: 1016
		// (add) Token: 0x06004CC0 RID: 19648 RVA: 0x0013E42C File Offset: 0x0013C62C
		// (remove) Token: 0x06004CC1 RID: 19649 RVA: 0x0013E435 File Offset: 0x0013C635
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler SizeChanged
		{
			add
			{
				base.SizeChanged += value;
			}
			remove
			{
				base.SizeChanged -= value;
			}
		}

		// Token: 0x170012D0 RID: 4816
		// (get) Token: 0x06004CC2 RID: 19650 RVA: 0x0013E43E File Offset: 0x0013C63E
		// (set) Token: 0x06004CC3 RID: 19651 RVA: 0x0013E446 File Offset: 0x0013C646
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new FormStartPosition StartPosition
		{
			get
			{
				return base.StartPosition;
			}
			set
			{
				base.StartPosition = value;
			}
		}

		// Token: 0x170012D1 RID: 4817
		// (get) Token: 0x06004CC4 RID: 19652 RVA: 0x0013E44F File Offset: 0x0013C64F
		// (set) Token: 0x06004CC5 RID: 19653 RVA: 0x0013E457 File Offset: 0x0013C657
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new bool TopMost
		{
			get
			{
				return base.TopMost;
			}
			set
			{
				base.TopMost = value;
			}
		}

		// Token: 0x170012D2 RID: 4818
		// (get) Token: 0x06004CC6 RID: 19654 RVA: 0x0013E460 File Offset: 0x0013C660
		// (set) Token: 0x06004CC7 RID: 19655 RVA: 0x0013E468 File Offset: 0x0013C668
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new Color TransparencyKey
		{
			get
			{
				return base.TransparencyKey;
			}
			set
			{
				base.TransparencyKey = value;
			}
		}

		// Token: 0x170012D3 RID: 4819
		// (get) Token: 0x06004CC8 RID: 19656 RVA: 0x00139F17 File Offset: 0x00138117
		// (set) Token: 0x06004CC9 RID: 19657 RVA: 0x0013E471 File Offset: 0x0013C671
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new bool UseWaitCursor
		{
			get
			{
				return base.UseWaitCursor;
			}
			set
			{
				base.UseWaitCursor = value;
			}
		}

		// Token: 0x170012D4 RID: 4820
		// (get) Token: 0x06004CCA RID: 19658 RVA: 0x0013E47A File Offset: 0x0013C67A
		// (set) Token: 0x06004CCB RID: 19659 RVA: 0x0013E482 File Offset: 0x0013C682
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new FormWindowState WindowState
		{
			get
			{
				return base.WindowState;
			}
			set
			{
				base.WindowState = value;
			}
		}

		// Token: 0x170012D5 RID: 4821
		// (get) Token: 0x06004CCC RID: 19660 RVA: 0x0013E48B File Offset: 0x0013C68B
		// (set) Token: 0x06004CCD RID: 19661 RVA: 0x0013E493 File Offset: 0x0013C693
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new AccessibleRole AccessibleRole
		{
			get
			{
				return base.AccessibleRole;
			}
			set
			{
				base.AccessibleRole = value;
			}
		}

		// Token: 0x170012D6 RID: 4822
		// (get) Token: 0x06004CCE RID: 19662 RVA: 0x0013E49C File Offset: 0x0013C69C
		// (set) Token: 0x06004CCF RID: 19663 RVA: 0x0013E4A4 File Offset: 0x0013C6A4
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new string AccessibleDescription
		{
			get
			{
				return base.AccessibleDescription;
			}
			set
			{
				base.AccessibleDescription = value;
			}
		}

		// Token: 0x170012D7 RID: 4823
		// (get) Token: 0x06004CD0 RID: 19664 RVA: 0x0013E4AD File Offset: 0x0013C6AD
		// (set) Token: 0x06004CD1 RID: 19665 RVA: 0x0013E4B5 File Offset: 0x0013C6B5
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new string AccessibleName
		{
			get
			{
				return base.AccessibleName;
			}
			set
			{
				base.AccessibleName = value;
			}
		}

		// Token: 0x170012D8 RID: 4824
		// (get) Token: 0x06004CD2 RID: 19666 RVA: 0x000E2B53 File Offset: 0x000E0D53
		// (set) Token: 0x06004CD3 RID: 19667 RVA: 0x000E2B5B File Offset: 0x000E0D5B
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new bool CausesValidation
		{
			get
			{
				return base.CausesValidation;
			}
			set
			{
				base.CausesValidation = value;
			}
		}

		// Token: 0x140003F9 RID: 1017
		// (add) Token: 0x06004CD4 RID: 19668 RVA: 0x000E2B64 File Offset: 0x000E0D64
		// (remove) Token: 0x06004CD5 RID: 19669 RVA: 0x000E2B6D File Offset: 0x000E0D6D
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler CausesValidationChanged
		{
			add
			{
				base.CausesValidationChanged += value;
			}
			remove
			{
				base.CausesValidationChanged -= value;
			}
		}

		// Token: 0x170012D9 RID: 4825
		// (get) Token: 0x06004CD6 RID: 19670 RVA: 0x0013E4BE File Offset: 0x0013C6BE
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new ControlBindingsCollection DataBindings
		{
			get
			{
				return base.DataBindings;
			}
		}

		// Token: 0x170012DA RID: 4826
		// (get) Token: 0x06004CD7 RID: 19671 RVA: 0x0013E4C6 File Offset: 0x0013C6C6
		protected override Size DefaultMinimumSize
		{
			get
			{
				return new Size(375, 250);
			}
		}

		// Token: 0x170012DB RID: 4827
		// (get) Token: 0x06004CD8 RID: 19672 RVA: 0x0001A261 File Offset: 0x00018461
		// (set) Token: 0x06004CD9 RID: 19673 RVA: 0x0001A269 File Offset: 0x00018469
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new bool Enabled
		{
			get
			{
				return base.Enabled;
			}
			set
			{
				base.Enabled = value;
			}
		}

		// Token: 0x140003FA RID: 1018
		// (add) Token: 0x06004CDA RID: 19674 RVA: 0x001073F2 File Offset: 0x001055F2
		// (remove) Token: 0x06004CDB RID: 19675 RVA: 0x001073FB File Offset: 0x001055FB
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler EnabledChanged
		{
			add
			{
				base.EnabledChanged += value;
			}
			remove
			{
				base.EnabledChanged -= value;
			}
		}

		// Token: 0x170012DC RID: 4828
		// (get) Token: 0x06004CDC RID: 19676 RVA: 0x0013E4D7 File Offset: 0x0013C6D7
		// (set) Token: 0x06004CDD RID: 19677 RVA: 0x0013E4DF File Offset: 0x0013C6DF
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public new Point Location
		{
			get
			{
				return base.Location;
			}
			set
			{
				base.Location = value;
			}
		}

		// Token: 0x140003FB RID: 1019
		// (add) Token: 0x06004CDE RID: 19678 RVA: 0x0010003A File Offset: 0x000FE23A
		// (remove) Token: 0x06004CDF RID: 19679 RVA: 0x00100043 File Offset: 0x000FE243
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler LocationChanged
		{
			add
			{
				base.LocationChanged += value;
			}
			remove
			{
				base.LocationChanged -= value;
			}
		}

		// Token: 0x170012DD RID: 4829
		// (get) Token: 0x06004CE0 RID: 19680 RVA: 0x0013E4E8 File Offset: 0x0013C6E8
		// (set) Token: 0x06004CE1 RID: 19681 RVA: 0x0013E4F0 File Offset: 0x0013C6F0
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new object Tag
		{
			get
			{
				return base.Tag;
			}
			set
			{
				base.Tag = value;
			}
		}

		// Token: 0x170012DE RID: 4830
		// (get) Token: 0x06004CE2 RID: 19682 RVA: 0x000B90B9 File Offset: 0x000B72B9
		// (set) Token: 0x06004CE3 RID: 19683 RVA: 0x000B90C1 File Offset: 0x000B72C1
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override bool AllowDrop
		{
			get
			{
				return base.AllowDrop;
			}
			set
			{
				base.AllowDrop = value;
			}
		}

		// Token: 0x170012DF RID: 4831
		// (get) Token: 0x06004CE4 RID: 19684 RVA: 0x0001A23C File Offset: 0x0001843C
		// (set) Token: 0x06004CE5 RID: 19685 RVA: 0x0001A244 File Offset: 0x00018444
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override Cursor Cursor
		{
			get
			{
				return base.Cursor;
			}
			set
			{
				base.Cursor = value;
			}
		}

		// Token: 0x140003FC RID: 1020
		// (add) Token: 0x06004CE6 RID: 19686 RVA: 0x000463EF File Offset: 0x000445EF
		// (remove) Token: 0x06004CE7 RID: 19687 RVA: 0x000463F8 File Offset: 0x000445F8
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler CursorChanged
		{
			add
			{
				base.CursorChanged += value;
			}
			remove
			{
				base.CursorChanged -= value;
			}
		}

		// Token: 0x170012E0 RID: 4832
		// (get) Token: 0x06004CE8 RID: 19688 RVA: 0x00011A90 File Offset: 0x0000FC90
		// (set) Token: 0x06004CE9 RID: 19689 RVA: 0x00011A98 File Offset: 0x0000FC98
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

		// Token: 0x140003FD RID: 1021
		// (add) Token: 0x06004CEA RID: 19690 RVA: 0x00011AA1 File Offset: 0x0000FCA1
		// (remove) Token: 0x06004CEB RID: 19691 RVA: 0x00011AAA File Offset: 0x0000FCAA
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

		// Token: 0x170012E1 RID: 4833
		// (get) Token: 0x06004CEC RID: 19692 RVA: 0x00011AB3 File Offset: 0x0000FCB3
		// (set) Token: 0x06004CED RID: 19693 RVA: 0x00011ABB File Offset: 0x0000FCBB
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

		// Token: 0x140003FE RID: 1022
		// (add) Token: 0x06004CEE RID: 19694 RVA: 0x00011AC4 File Offset: 0x0000FCC4
		// (remove) Token: 0x06004CEF RID: 19695 RVA: 0x00011ACD File Offset: 0x0000FCCD
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

		// Token: 0x170012E2 RID: 4834
		// (get) Token: 0x06004CF0 RID: 19696 RVA: 0x0001A1ED File Offset: 0x000183ED
		// (set) Token: 0x06004CF1 RID: 19697 RVA: 0x0001A1F5 File Offset: 0x000183F5
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new ImeMode ImeMode
		{
			get
			{
				return base.ImeMode;
			}
			set
			{
				base.ImeMode = value;
			}
		}

		// Token: 0x140003FF RID: 1023
		// (add) Token: 0x06004CF2 RID: 19698 RVA: 0x0002410C File Offset: 0x0002230C
		// (remove) Token: 0x06004CF3 RID: 19699 RVA: 0x00024115 File Offset: 0x00022315
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler ImeModeChanged
		{
			add
			{
				base.ImeModeChanged += value;
			}
			remove
			{
				base.ImeModeChanged -= value;
			}
		}

		// Token: 0x170012E3 RID: 4835
		// (get) Token: 0x06004CF4 RID: 19700 RVA: 0x00011A23 File Offset: 0x0000FC23
		// (set) Token: 0x06004CF5 RID: 19701 RVA: 0x00011A2B File Offset: 0x0000FC2B
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new Size AutoScrollMargin
		{
			get
			{
				return base.AutoScrollMargin;
			}
			set
			{
				base.AutoScrollMargin = value;
			}
		}

		// Token: 0x170012E4 RID: 4836
		// (get) Token: 0x06004CF6 RID: 19702 RVA: 0x00011A34 File Offset: 0x0000FC34
		// (set) Token: 0x06004CF7 RID: 19703 RVA: 0x00011A3C File Offset: 0x0000FC3C
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new Size AutoScrollMinSize
		{
			get
			{
				return base.AutoScrollMinSize;
			}
			set
			{
				base.AutoScrollMinSize = value;
			}
		}

		// Token: 0x170012E5 RID: 4837
		// (get) Token: 0x06004CF8 RID: 19704 RVA: 0x000FFF04 File Offset: 0x000FE104
		// (set) Token: 0x06004CF9 RID: 19705 RVA: 0x000FFF0C File Offset: 0x000FE10C
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override AnchorStyles Anchor
		{
			get
			{
				return base.Anchor;
			}
			set
			{
				base.Anchor = value;
			}
		}

		// Token: 0x170012E6 RID: 4838
		// (get) Token: 0x06004CFA RID: 19706 RVA: 0x000FFFD1 File Offset: 0x000FE1D1
		// (set) Token: 0x06004CFB RID: 19707 RVA: 0x000FFFD9 File Offset: 0x000FE1D9
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new bool Visible
		{
			get
			{
				return base.Visible;
			}
			set
			{
				base.Visible = value;
			}
		}

		// Token: 0x14000400 RID: 1024
		// (add) Token: 0x06004CFC RID: 19708 RVA: 0x00100016 File Offset: 0x000FE216
		// (remove) Token: 0x06004CFD RID: 19709 RVA: 0x0010001F File Offset: 0x000FE21F
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler VisibleChanged
		{
			add
			{
				base.VisibleChanged += value;
			}
			remove
			{
				base.VisibleChanged -= value;
			}
		}

		// Token: 0x170012E7 RID: 4839
		// (get) Token: 0x06004CFE RID: 19710 RVA: 0x0001A283 File Offset: 0x00018483
		// (set) Token: 0x06004CFF RID: 19711 RVA: 0x00013238 File Offset: 0x00011438
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override Color ForeColor
		{
			get
			{
				return base.ForeColor;
			}
			set
			{
				base.ForeColor = value;
			}
		}

		// Token: 0x14000401 RID: 1025
		// (add) Token: 0x06004D00 RID: 19712 RVA: 0x0005AACE File Offset: 0x00058CCE
		// (remove) Token: 0x06004D01 RID: 19713 RVA: 0x0005AAD7 File Offset: 0x00058CD7
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler ForeColorChanged
		{
			add
			{
				base.ForeColorChanged += value;
			}
			remove
			{
				base.ForeColorChanged -= value;
			}
		}

		// Token: 0x170012E8 RID: 4840
		// (get) Token: 0x06004D02 RID: 19714 RVA: 0x000E34A7 File Offset: 0x000E16A7
		// (set) Token: 0x06004D03 RID: 19715 RVA: 0x000C619D File Offset: 0x000C439D
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override RightToLeft RightToLeft
		{
			get
			{
				return base.RightToLeft;
			}
			set
			{
				base.RightToLeft = value;
			}
		}

		// Token: 0x170012E9 RID: 4841
		// (get) Token: 0x06004D04 RID: 19716 RVA: 0x0013E4F9 File Offset: 0x0013C6F9
		// (set) Token: 0x06004D05 RID: 19717 RVA: 0x0013E501 File Offset: 0x0013C701
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override bool RightToLeftLayout
		{
			get
			{
				return base.RightToLeftLayout;
			}
			set
			{
				base.RightToLeftLayout = value;
			}
		}

		// Token: 0x14000402 RID: 1026
		// (add) Token: 0x06004D06 RID: 19718 RVA: 0x000E34AF File Offset: 0x000E16AF
		// (remove) Token: 0x06004D07 RID: 19719 RVA: 0x000E34B8 File Offset: 0x000E16B8
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler RightToLeftChanged
		{
			add
			{
				base.RightToLeftChanged += value;
			}
			remove
			{
				base.RightToLeftChanged -= value;
			}
		}

		// Token: 0x14000403 RID: 1027
		// (add) Token: 0x06004D08 RID: 19720 RVA: 0x0013E50A File Offset: 0x0013C70A
		// (remove) Token: 0x06004D09 RID: 19721 RVA: 0x0013E513 File Offset: 0x0013C713
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler RightToLeftLayoutChanged
		{
			add
			{
				base.RightToLeftLayoutChanged += value;
			}
			remove
			{
				base.RightToLeftLayoutChanged -= value;
			}
		}

		// Token: 0x170012EA RID: 4842
		// (get) Token: 0x06004D0A RID: 19722 RVA: 0x0013E51C File Offset: 0x0013C71C
		// (set) Token: 0x06004D0B RID: 19723 RVA: 0x0013E524 File Offset: 0x0013C724
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new bool TabStop
		{
			get
			{
				return base.TabStop;
			}
			set
			{
				base.TabStop = value;
			}
		}

		// Token: 0x14000404 RID: 1028
		// (add) Token: 0x06004D0C RID: 19724 RVA: 0x0013E52D File Offset: 0x0013C72D
		// (remove) Token: 0x06004D0D RID: 19725 RVA: 0x0013E536 File Offset: 0x0013C736
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler TabStopChanged
		{
			add
			{
				base.TabStopChanged += value;
			}
			remove
			{
				base.TabStopChanged -= value;
			}
		}

		// Token: 0x170012EB RID: 4843
		// (get) Token: 0x06004D0E RID: 19726 RVA: 0x0013E53F File Offset: 0x0013C73F
		// (set) Token: 0x06004D0F RID: 19727 RVA: 0x0013E547 File Offset: 0x0013C747
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override string Text
		{
			get
			{
				return base.Text;
			}
			set
			{
				base.Text = value;
			}
		}

		// Token: 0x14000405 RID: 1029
		// (add) Token: 0x06004D10 RID: 19728 RVA: 0x00046771 File Offset: 0x00044971
		// (remove) Token: 0x06004D11 RID: 19729 RVA: 0x0004677A File Offset: 0x0004497A
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler TextChanged
		{
			add
			{
				base.TextChanged += value;
			}
			remove
			{
				base.TextChanged -= value;
			}
		}

		// Token: 0x170012EC RID: 4844
		// (get) Token: 0x06004D12 RID: 19730 RVA: 0x000FC6F6 File Offset: 0x000FA8F6
		// (set) Token: 0x06004D13 RID: 19731 RVA: 0x000FFF26 File Offset: 0x000FE126
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override DockStyle Dock
		{
			get
			{
				return base.Dock;
			}
			set
			{
				base.Dock = value;
			}
		}

		// Token: 0x14000406 RID: 1030
		// (add) Token: 0x06004D14 RID: 19732 RVA: 0x00100028 File Offset: 0x000FE228
		// (remove) Token: 0x06004D15 RID: 19733 RVA: 0x00100031 File Offset: 0x000FE231
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler DockChanged
		{
			add
			{
				base.DockChanged += value;
			}
			remove
			{
				base.DockChanged -= value;
			}
		}

		// Token: 0x170012ED RID: 4845
		// (get) Token: 0x06004D16 RID: 19734 RVA: 0x0001A272 File Offset: 0x00018472
		// (set) Token: 0x06004D17 RID: 19735 RVA: 0x0001A27A File Offset: 0x0001847A
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override Font Font
		{
			get
			{
				return base.Font;
			}
			set
			{
				base.Font = value;
			}
		}

		// Token: 0x14000407 RID: 1031
		// (add) Token: 0x06004D18 RID: 19736 RVA: 0x0005AAE0 File Offset: 0x00058CE0
		// (remove) Token: 0x06004D19 RID: 19737 RVA: 0x0005AAE9 File Offset: 0x00058CE9
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler FontChanged
		{
			add
			{
				base.FontChanged += value;
			}
			remove
			{
				base.FontChanged -= value;
			}
		}

		// Token: 0x170012EE RID: 4846
		// (get) Token: 0x06004D1A RID: 19738 RVA: 0x00011B2D File Offset: 0x0000FD2D
		// (set) Token: 0x06004D1B RID: 19739 RVA: 0x0001A24D File Offset: 0x0001844D
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override ContextMenu ContextMenu
		{
			get
			{
				return base.ContextMenu;
			}
			set
			{
				base.ContextMenu = value;
			}
		}

		// Token: 0x14000408 RID: 1032
		// (add) Token: 0x06004D1C RID: 19740 RVA: 0x00112D7C File Offset: 0x00110F7C
		// (remove) Token: 0x06004D1D RID: 19741 RVA: 0x00112D85 File Offset: 0x00110F85
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler ContextMenuChanged
		{
			add
			{
				base.ContextMenuChanged += value;
			}
			remove
			{
				base.ContextMenuChanged -= value;
			}
		}

		// Token: 0x170012EF RID: 4847
		// (get) Token: 0x06004D1E RID: 19742 RVA: 0x00011BDA File Offset: 0x0000FDDA
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new ScrollableControl.DockPaddingEdges DockPadding
		{
			get
			{
				return base.DockPadding;
			}
		}

		// Token: 0x170012F0 RID: 4848
		// (get) Token: 0x06004D1F RID: 19743 RVA: 0x0013E550 File Offset: 0x0013C750
		// (set) Token: 0x06004D20 RID: 19744 RVA: 0x0013E55D File Offset: 0x0013C75D
		[SRCategory("CatBehavior")]
		[DefaultValue(false)]
		[SRDescription("PrintPreviewAntiAliasDescr")]
		public bool UseAntiAlias
		{
			get
			{
				return this.PrintPreviewControl.UseAntiAlias;
			}
			set
			{
				this.PrintPreviewControl.UseAntiAlias = value;
			}
		}

		// Token: 0x170012F1 RID: 4849
		// (get) Token: 0x06004D21 RID: 19745 RVA: 0x0013E56B File Offset: 0x0013C76B
		// (set) Token: 0x06004D22 RID: 19746 RVA: 0x000072B6 File Offset: 0x000054B6
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("This property has been deprecated. Use the AutoScaleDimensions property instead.  http://go.microsoft.com/fwlink/?linkid=14202")]
		public override Size AutoScaleBaseSize
		{
			get
			{
				return base.AutoScaleBaseSize;
			}
			set
			{
			}
		}

		// Token: 0x170012F2 RID: 4850
		// (get) Token: 0x06004D23 RID: 19747 RVA: 0x0013E573 File Offset: 0x0013C773
		// (set) Token: 0x06004D24 RID: 19748 RVA: 0x0013E580 File Offset: 0x0013C780
		[SRCategory("CatBehavior")]
		[DefaultValue(null)]
		[SRDescription("PrintPreviewDocumentDescr")]
		public PrintDocument Document
		{
			get
			{
				return this.previewControl.Document;
			}
			set
			{
				this.previewControl.Document = value;
			}
		}

		// Token: 0x170012F3 RID: 4851
		// (get) Token: 0x06004D25 RID: 19749 RVA: 0x0013E58E File Offset: 0x0013C78E
		// (set) Token: 0x06004D26 RID: 19750 RVA: 0x0013E596 File Offset: 0x0013C796
		[Browsable(false)]
		[DefaultValue(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new bool MinimizeBox
		{
			get
			{
				return base.MinimizeBox;
			}
			set
			{
				base.MinimizeBox = value;
			}
		}

		// Token: 0x170012F4 RID: 4852
		// (get) Token: 0x06004D27 RID: 19751 RVA: 0x0013E59F File Offset: 0x0013C79F
		[SRCategory("CatBehavior")]
		[SRDescription("PrintPreviewPrintPreviewControlDescr")]
		[Browsable(false)]
		public PrintPreviewControl PrintPreviewControl
		{
			get
			{
				return this.previewControl;
			}
		}

		// Token: 0x170012F5 RID: 4853
		// (get) Token: 0x06004D28 RID: 19752 RVA: 0x0013E5A7 File Offset: 0x0013C7A7
		// (set) Token: 0x06004D29 RID: 19753 RVA: 0x0013E5AF File Offset: 0x0013C7AF
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public new double Opacity
		{
			get
			{
				return base.Opacity;
			}
			set
			{
				base.Opacity = value;
			}
		}

		// Token: 0x170012F6 RID: 4854
		// (get) Token: 0x06004D2A RID: 19754 RVA: 0x0013E5B8 File Offset: 0x0013C7B8
		// (set) Token: 0x06004D2B RID: 19755 RVA: 0x0013E5C0 File Offset: 0x0013C7C0
		[Browsable(false)]
		[DefaultValue(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new bool ShowInTaskbar
		{
			get
			{
				return base.ShowInTaskbar;
			}
			set
			{
				base.ShowInTaskbar = value;
			}
		}

		// Token: 0x170012F7 RID: 4855
		// (get) Token: 0x06004D2C RID: 19756 RVA: 0x0013E5C9 File Offset: 0x0013C7C9
		// (set) Token: 0x06004D2D RID: 19757 RVA: 0x0013E5D1 File Offset: 0x0013C7D1
		[Browsable(false)]
		[DefaultValue(SizeGripStyle.Hide)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new SizeGripStyle SizeGripStyle
		{
			get
			{
				return base.SizeGripStyle;
			}
			set
			{
				base.SizeGripStyle = value;
			}
		}

		// Token: 0x06004D2E RID: 19758 RVA: 0x0013E5DC File Offset: 0x0013C7DC
		private void InitForm()
		{
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(PrintPreviewDialog));
			this.toolStrip1 = new ToolStrip();
			this.printToolStripButton = new ToolStripButton();
			this.zoomToolStripSplitButton = new ToolStripSplitButton();
			this.autoToolStripMenuItem = new ToolStripMenuItem();
			this.toolStripMenuItem1 = new ToolStripMenuItem();
			this.toolStripMenuItem2 = new ToolStripMenuItem();
			this.toolStripMenuItem3 = new ToolStripMenuItem();
			this.toolStripMenuItem4 = new ToolStripMenuItem();
			this.toolStripMenuItem5 = new ToolStripMenuItem();
			this.toolStripMenuItem6 = new ToolStripMenuItem();
			this.toolStripMenuItem7 = new ToolStripMenuItem();
			this.toolStripMenuItem8 = new ToolStripMenuItem();
			this.separatorToolStripSeparator = new ToolStripSeparator();
			this.onepageToolStripButton = new ToolStripButton();
			this.twopagesToolStripButton = new ToolStripButton();
			this.threepagesToolStripButton = new ToolStripButton();
			this.fourpagesToolStripButton = new ToolStripButton();
			this.sixpagesToolStripButton = new ToolStripButton();
			this.separatorToolStripSeparator1 = new ToolStripSeparator();
			this.closeToolStripButton = new ToolStripButton();
			this.pageCounter = new NumericUpDown();
			this.pageToolStripLabel = new ToolStripLabel();
			this.toolStrip1.SuspendLayout();
			((ISupportInitialize)this.pageCounter).BeginInit();
			base.SuspendLayout();
			componentResourceManager.ApplyResources(this.toolStrip1, "toolStrip1");
			this.toolStrip1.Items.AddRange(new ToolStripItem[]
			{
				this.printToolStripButton,
				this.zoomToolStripSplitButton,
				this.separatorToolStripSeparator,
				this.onepageToolStripButton,
				this.twopagesToolStripButton,
				this.threepagesToolStripButton,
				this.fourpagesToolStripButton,
				this.sixpagesToolStripButton,
				this.separatorToolStripSeparator1,
				this.closeToolStripButton
			});
			this.toolStrip1.Name = "toolStrip1";
			this.toolStrip1.RenderMode = ToolStripRenderMode.System;
			this.toolStrip1.GripStyle = ToolStripGripStyle.Hidden;
			this.printToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
			this.printToolStripButton.Name = "printToolStripButton";
			componentResourceManager.ApplyResources(this.printToolStripButton, "printToolStripButton");
			if (AccessibilityImprovements.Level5)
			{
				this.printToolStripButton.AccessibleName = componentResourceManager.GetString("printToolStripButton.AccessibleNameLevel5");
			}
			this.zoomToolStripSplitButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
			this.zoomToolStripSplitButton.DoubleClickEnabled = true;
			this.zoomToolStripSplitButton.DropDownItems.AddRange(new ToolStripItem[]
			{
				this.autoToolStripMenuItem,
				this.toolStripMenuItem1,
				this.toolStripMenuItem2,
				this.toolStripMenuItem3,
				this.toolStripMenuItem4,
				this.toolStripMenuItem5,
				this.toolStripMenuItem6,
				this.toolStripMenuItem7,
				this.toolStripMenuItem8
			});
			this.zoomToolStripSplitButton.Name = "zoomToolStripSplitButton";
			this.zoomToolStripSplitButton.SplitterWidth = 1;
			componentResourceManager.ApplyResources(this.zoomToolStripSplitButton, "zoomToolStripSplitButton");
			this.autoToolStripMenuItem.CheckOnClick = true;
			this.autoToolStripMenuItem.DoubleClickEnabled = true;
			this.autoToolStripMenuItem.Checked = true;
			this.autoToolStripMenuItem.Name = "autoToolStripMenuItem";
			componentResourceManager.ApplyResources(this.autoToolStripMenuItem, "autoToolStripMenuItem");
			this.toolStripMenuItem1.CheckOnClick = true;
			this.toolStripMenuItem1.DoubleClickEnabled = true;
			this.toolStripMenuItem1.Name = "toolStripMenuItem1";
			componentResourceManager.ApplyResources(this.toolStripMenuItem1, "toolStripMenuItem1");
			this.toolStripMenuItem2.CheckOnClick = true;
			this.toolStripMenuItem2.DoubleClickEnabled = true;
			this.toolStripMenuItem2.Name = "toolStripMenuItem2";
			componentResourceManager.ApplyResources(this.toolStripMenuItem2, "toolStripMenuItem2");
			this.toolStripMenuItem3.CheckOnClick = true;
			this.toolStripMenuItem3.DoubleClickEnabled = true;
			this.toolStripMenuItem3.Name = "toolStripMenuItem3";
			componentResourceManager.ApplyResources(this.toolStripMenuItem3, "toolStripMenuItem3");
			this.toolStripMenuItem4.CheckOnClick = true;
			this.toolStripMenuItem4.DoubleClickEnabled = true;
			this.toolStripMenuItem4.Name = "toolStripMenuItem4";
			componentResourceManager.ApplyResources(this.toolStripMenuItem4, "toolStripMenuItem4");
			this.toolStripMenuItem5.CheckOnClick = true;
			this.toolStripMenuItem5.DoubleClickEnabled = true;
			this.toolStripMenuItem5.Name = "toolStripMenuItem5";
			componentResourceManager.ApplyResources(this.toolStripMenuItem5, "toolStripMenuItem5");
			this.toolStripMenuItem6.CheckOnClick = true;
			this.toolStripMenuItem6.DoubleClickEnabled = true;
			this.toolStripMenuItem6.Name = "toolStripMenuItem6";
			componentResourceManager.ApplyResources(this.toolStripMenuItem6, "toolStripMenuItem6");
			this.toolStripMenuItem7.CheckOnClick = true;
			this.toolStripMenuItem7.DoubleClickEnabled = true;
			this.toolStripMenuItem7.Name = "toolStripMenuItem7";
			componentResourceManager.ApplyResources(this.toolStripMenuItem7, "toolStripMenuItem7");
			this.toolStripMenuItem8.CheckOnClick = true;
			this.toolStripMenuItem8.DoubleClickEnabled = true;
			this.toolStripMenuItem8.Name = "toolStripMenuItem8";
			componentResourceManager.ApplyResources(this.toolStripMenuItem8, "toolStripMenuItem8");
			this.separatorToolStripSeparator.Name = "separatorToolStripSeparator";
			this.onepageToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
			this.onepageToolStripButton.Name = "onepageToolStripButton";
			componentResourceManager.ApplyResources(this.onepageToolStripButton, "onepageToolStripButton");
			this.twopagesToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
			this.twopagesToolStripButton.Name = "twopagesToolStripButton";
			componentResourceManager.ApplyResources(this.twopagesToolStripButton, "twopagesToolStripButton");
			this.threepagesToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
			this.threepagesToolStripButton.Name = "threepagesToolStripButton";
			componentResourceManager.ApplyResources(this.threepagesToolStripButton, "threepagesToolStripButton");
			this.fourpagesToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
			this.fourpagesToolStripButton.Name = "fourpagesToolStripButton";
			componentResourceManager.ApplyResources(this.fourpagesToolStripButton, "fourpagesToolStripButton");
			this.sixpagesToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
			this.sixpagesToolStripButton.Name = "sixpagesToolStripButton";
			componentResourceManager.ApplyResources(this.sixpagesToolStripButton, "sixpagesToolStripButton");
			this.separatorToolStripSeparator1.Name = "separatorToolStripSeparator1";
			this.closeToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.closeToolStripButton.Name = "closeToolStripButton";
			componentResourceManager.ApplyResources(this.closeToolStripButton, "closeToolStripButton");
			componentResourceManager.ApplyResources(this.pageCounter, "pageCounter");
			this.pageCounter.Text = "1";
			this.pageCounter.TextAlign = HorizontalAlignment.Right;
			this.pageCounter.DecimalPlaces = 0;
			this.pageCounter.Minimum = new decimal(0.0);
			this.pageCounter.Maximum = new decimal(1000.0);
			this.pageCounter.ValueChanged += this.UpdownMove;
			this.pageCounter.Name = "pageCounter";
			this.pageToolStripLabel.Alignment = ToolStripItemAlignment.Right;
			this.pageToolStripLabel.Name = "pageToolStripLabel";
			componentResourceManager.ApplyResources(this.pageToolStripLabel, "pageToolStripLabel");
			this.previewControl.Size = new Size(792, 610);
			this.previewControl.Location = new Point(0, 43);
			this.previewControl.Dock = DockStyle.Fill;
			this.previewControl.StartPageChanged += this.previewControl_StartPageChanged;
			this.printToolStripButton.Click += this.OnprintToolStripButtonClick;
			this.autoToolStripMenuItem.Click += this.ZoomAuto;
			this.toolStripMenuItem1.Click += this.Zoom500;
			this.toolStripMenuItem2.Click += this.Zoom250;
			this.toolStripMenuItem3.Click += this.Zoom150;
			this.toolStripMenuItem4.Click += this.Zoom100;
			this.toolStripMenuItem5.Click += this.Zoom75;
			this.toolStripMenuItem6.Click += this.Zoom50;
			this.toolStripMenuItem7.Click += this.Zoom25;
			this.toolStripMenuItem8.Click += this.Zoom10;
			this.onepageToolStripButton.Click += this.OnonepageToolStripButtonClick;
			this.twopagesToolStripButton.Click += this.OntwopagesToolStripButtonClick;
			this.threepagesToolStripButton.Click += this.OnthreepagesToolStripButtonClick;
			this.fourpagesToolStripButton.Click += this.OnfourpagesToolStripButtonClick;
			this.sixpagesToolStripButton.Click += this.OnsixpagesToolStripButtonClick;
			this.closeToolStripButton.Click += this.OncloseToolStripButtonClick;
			this.closeToolStripButton.Paint += this.OncloseToolStripButtonPaint;
			this.toolStrip1.ImageList = this.imageList;
			this.printToolStripButton.ImageIndex = 0;
			this.zoomToolStripSplitButton.ImageIndex = 1;
			this.onepageToolStripButton.ImageIndex = 2;
			this.twopagesToolStripButton.ImageIndex = 3;
			this.threepagesToolStripButton.ImageIndex = 4;
			this.fourpagesToolStripButton.ImageIndex = 5;
			this.sixpagesToolStripButton.ImageIndex = 6;
			this.previewControl.TabIndex = 0;
			this.toolStrip1.TabIndex = 1;
			this.zoomToolStripSplitButton.DefaultItem = this.autoToolStripMenuItem;
			ToolStripDropDownMenu toolStripDropDownMenu = this.zoomToolStripSplitButton.DropDown as ToolStripDropDownMenu;
			if (toolStripDropDownMenu != null)
			{
				toolStripDropDownMenu.ShowCheckMargin = true;
				toolStripDropDownMenu.ShowImageMargin = false;
				toolStripDropDownMenu.RenderMode = ToolStripRenderMode.System;
			}
			ToolStripControlHost toolStripControlHost = new ToolStripControlHost(this.pageCounter);
			toolStripControlHost.Alignment = ToolStripItemAlignment.Right;
			this.toolStrip1.Items.Add(toolStripControlHost);
			this.toolStrip1.Items.Add(this.pageToolStripLabel);
			componentResourceManager.ApplyResources(this, "$this");
			base.Controls.Add(this.previewControl);
			base.Controls.Add(this.toolStrip1);
			base.ClientSize = new Size(400, 300);
			this.MinimizeBox = false;
			this.ShowInTaskbar = false;
			this.SizeGripStyle = SizeGripStyle.Hide;
			this.toolStrip1.ResumeLayout(false);
			((ISupportInitialize)this.pageCounter).EndInit();
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x06004D2F RID: 19759 RVA: 0x0013EFE9 File Offset: 0x0013D1E9
		protected override void OnClosing(CancelEventArgs e)
		{
			base.OnClosing(e);
			this.previewControl.InvalidatePreview();
		}

		// Token: 0x06004D30 RID: 19760 RVA: 0x0013EFFD File Offset: 0x0013D1FD
		protected override void CreateHandle()
		{
			if (this.Document != null && !this.Document.PrinterSettings.IsValid)
			{
				throw new InvalidPrinterException(this.Document.PrinterSettings);
			}
			base.CreateHandle();
		}

		// Token: 0x06004D31 RID: 19761 RVA: 0x0013F030 File Offset: 0x0013D230
		[UIPermission(SecurityAction.LinkDemand, Window = UIPermissionWindow.AllWindows)]
		protected override bool ProcessDialogKey(Keys keyData)
		{
			if ((keyData & (Keys.Control | Keys.Alt)) == Keys.None)
			{
				Keys keys = keyData & Keys.KeyCode;
				if (keys - Keys.Left <= 3)
				{
					return false;
				}
			}
			return base.ProcessDialogKey(keyData);
		}

		// Token: 0x06004D32 RID: 19762 RVA: 0x0013F05E File Offset: 0x0013D25E
		[UIPermission(SecurityAction.LinkDemand, Window = UIPermissionWindow.AllWindows)]
		protected override bool ProcessTabKey(bool forward)
		{
			if (base.ActiveControl == this.previewControl)
			{
				this.pageCounter.FocusInternal();
				return true;
			}
			return false;
		}

		// Token: 0x06004D33 RID: 19763 RVA: 0x00011A20 File Offset: 0x0000FC20
		internal override bool ShouldSerializeAutoScaleBaseSize()
		{
			return false;
		}

		// Token: 0x06004D34 RID: 19764 RVA: 0x0013F07D File Offset: 0x0013D27D
		internal override bool ShouldSerializeText()
		{
			return !this.Text.Equals(SR.GetString("PrintPreviewDialog_PrintPreview"));
		}

		// Token: 0x06004D35 RID: 19765 RVA: 0x0013F097 File Offset: 0x0013D297
		private void OncloseToolStripButtonClick(object sender, EventArgs e)
		{
			base.Close();
		}

		// Token: 0x06004D36 RID: 19766 RVA: 0x0013F09F File Offset: 0x0013D29F
		private void previewControl_StartPageChanged(object sender, EventArgs e)
		{
			this.pageCounter.Value = this.previewControl.StartPage + 1;
		}

		// Token: 0x06004D37 RID: 19767 RVA: 0x0013F0C0 File Offset: 0x0013D2C0
		private void CheckZoomMenu(ToolStripMenuItem toChecked)
		{
			foreach (object obj in this.zoomToolStripSplitButton.DropDownItems)
			{
				ToolStripMenuItem toolStripMenuItem = (ToolStripMenuItem)obj;
				toolStripMenuItem.Checked = (toChecked == toolStripMenuItem);
			}
		}

		// Token: 0x06004D38 RID: 19768 RVA: 0x0013F124 File Offset: 0x0013D324
		private void ZoomAuto(object sender, EventArgs eventargs)
		{
			ToolStripMenuItem toChecked = sender as ToolStripMenuItem;
			this.CheckZoomMenu(toChecked);
			this.previewControl.AutoZoom = true;
		}

		// Token: 0x06004D39 RID: 19769 RVA: 0x0013F14C File Offset: 0x0013D34C
		private void Zoom500(object sender, EventArgs eventargs)
		{
			ToolStripMenuItem toChecked = sender as ToolStripMenuItem;
			this.CheckZoomMenu(toChecked);
			this.previewControl.Zoom = 5.0;
		}

		// Token: 0x06004D3A RID: 19770 RVA: 0x0013F17C File Offset: 0x0013D37C
		private void Zoom250(object sender, EventArgs eventargs)
		{
			ToolStripMenuItem toChecked = sender as ToolStripMenuItem;
			this.CheckZoomMenu(toChecked);
			this.previewControl.Zoom = 2.5;
		}

		// Token: 0x06004D3B RID: 19771 RVA: 0x0013F1AC File Offset: 0x0013D3AC
		private void Zoom150(object sender, EventArgs eventargs)
		{
			ToolStripMenuItem toChecked = sender as ToolStripMenuItem;
			this.CheckZoomMenu(toChecked);
			this.previewControl.Zoom = 1.5;
		}

		// Token: 0x06004D3C RID: 19772 RVA: 0x0013F1DC File Offset: 0x0013D3DC
		private void Zoom100(object sender, EventArgs eventargs)
		{
			ToolStripMenuItem toChecked = sender as ToolStripMenuItem;
			this.CheckZoomMenu(toChecked);
			this.previewControl.Zoom = 1.0;
		}

		// Token: 0x06004D3D RID: 19773 RVA: 0x0013F20C File Offset: 0x0013D40C
		private void Zoom75(object sender, EventArgs eventargs)
		{
			ToolStripMenuItem toChecked = sender as ToolStripMenuItem;
			this.CheckZoomMenu(toChecked);
			this.previewControl.Zoom = 0.75;
		}

		// Token: 0x06004D3E RID: 19774 RVA: 0x0013F23C File Offset: 0x0013D43C
		private void Zoom50(object sender, EventArgs eventargs)
		{
			ToolStripMenuItem toChecked = sender as ToolStripMenuItem;
			this.CheckZoomMenu(toChecked);
			this.previewControl.Zoom = 0.5;
		}

		// Token: 0x06004D3F RID: 19775 RVA: 0x0013F26C File Offset: 0x0013D46C
		private void Zoom25(object sender, EventArgs eventargs)
		{
			ToolStripMenuItem toChecked = sender as ToolStripMenuItem;
			this.CheckZoomMenu(toChecked);
			this.previewControl.Zoom = 0.25;
		}

		// Token: 0x06004D40 RID: 19776 RVA: 0x0013F29C File Offset: 0x0013D49C
		private void Zoom10(object sender, EventArgs eventargs)
		{
			ToolStripMenuItem toChecked = sender as ToolStripMenuItem;
			this.CheckZoomMenu(toChecked);
			this.previewControl.Zoom = 0.1;
		}

		// Token: 0x06004D41 RID: 19777 RVA: 0x0013F2CC File Offset: 0x0013D4CC
		private void OncloseToolStripButtonPaint(object sender, PaintEventArgs e)
		{
			ToolStripItem toolStripItem = sender as ToolStripItem;
			if (toolStripItem != null && !toolStripItem.Selected)
			{
				Rectangle rect = new Rectangle(0, 0, toolStripItem.Bounds.Width - 1, toolStripItem.Bounds.Height - 1);
				using (Pen pen = new Pen(SystemColors.ControlDark))
				{
					e.Graphics.DrawRectangle(pen, rect);
				}
			}
		}

		// Token: 0x06004D42 RID: 19778 RVA: 0x0013F348 File Offset: 0x0013D548
		private void OnprintToolStripButtonClick(object sender, EventArgs e)
		{
			if (this.previewControl.Document != null)
			{
				this.previewControl.Document.Print();
			}
		}

		// Token: 0x06004D43 RID: 19779 RVA: 0x0013F367 File Offset: 0x0013D567
		private void OnzoomToolStripSplitButtonClick(object sender, EventArgs e)
		{
			this.ZoomAuto(null, EventArgs.Empty);
		}

		// Token: 0x06004D44 RID: 19780 RVA: 0x0013F375 File Offset: 0x0013D575
		private void OnonepageToolStripButtonClick(object sender, EventArgs e)
		{
			this.previewControl.Rows = 1;
			this.previewControl.Columns = 1;
		}

		// Token: 0x06004D45 RID: 19781 RVA: 0x0013F38F File Offset: 0x0013D58F
		private void OntwopagesToolStripButtonClick(object sender, EventArgs e)
		{
			this.previewControl.Rows = 1;
			this.previewControl.Columns = 2;
		}

		// Token: 0x06004D46 RID: 19782 RVA: 0x0013F3A9 File Offset: 0x0013D5A9
		private void OnthreepagesToolStripButtonClick(object sender, EventArgs e)
		{
			this.previewControl.Rows = 1;
			this.previewControl.Columns = 3;
		}

		// Token: 0x06004D47 RID: 19783 RVA: 0x0013F3C3 File Offset: 0x0013D5C3
		private void OnfourpagesToolStripButtonClick(object sender, EventArgs e)
		{
			this.previewControl.Rows = 2;
			this.previewControl.Columns = 2;
		}

		// Token: 0x06004D48 RID: 19784 RVA: 0x0013F3DD File Offset: 0x0013D5DD
		private void OnsixpagesToolStripButtonClick(object sender, EventArgs e)
		{
			this.previewControl.Rows = 2;
			this.previewControl.Columns = 3;
		}

		// Token: 0x06004D49 RID: 19785 RVA: 0x0013F3F8 File Offset: 0x0013D5F8
		private void UpdownMove(object sender, EventArgs eventargs)
		{
			int num = (int)this.pageCounter.Value - 1;
			if (num >= 0)
			{
				this.previewControl.StartPage = num;
				return;
			}
			this.pageCounter.Value = this.previewControl.StartPage + 1;
		}

		// Token: 0x040028A2 RID: 10402
		private PrintPreviewControl previewControl;

		// Token: 0x040028A3 RID: 10403
		private ToolStrip toolStrip1;

		// Token: 0x040028A4 RID: 10404
		private NumericUpDown pageCounter;

		// Token: 0x040028A5 RID: 10405
		private ToolStripButton printToolStripButton;

		// Token: 0x040028A6 RID: 10406
		private ToolStripSplitButton zoomToolStripSplitButton;

		// Token: 0x040028A7 RID: 10407
		private ToolStripMenuItem autoToolStripMenuItem;

		// Token: 0x040028A8 RID: 10408
		private ToolStripMenuItem toolStripMenuItem1;

		// Token: 0x040028A9 RID: 10409
		private ToolStripMenuItem toolStripMenuItem2;

		// Token: 0x040028AA RID: 10410
		private ToolStripMenuItem toolStripMenuItem3;

		// Token: 0x040028AB RID: 10411
		private ToolStripMenuItem toolStripMenuItem4;

		// Token: 0x040028AC RID: 10412
		private ToolStripMenuItem toolStripMenuItem5;

		// Token: 0x040028AD RID: 10413
		private ToolStripMenuItem toolStripMenuItem6;

		// Token: 0x040028AE RID: 10414
		private ToolStripMenuItem toolStripMenuItem7;

		// Token: 0x040028AF RID: 10415
		private ToolStripMenuItem toolStripMenuItem8;

		// Token: 0x040028B0 RID: 10416
		private ToolStripSeparator separatorToolStripSeparator;

		// Token: 0x040028B1 RID: 10417
		private ToolStripButton onepageToolStripButton;

		// Token: 0x040028B2 RID: 10418
		private ToolStripButton twopagesToolStripButton;

		// Token: 0x040028B3 RID: 10419
		private ToolStripButton threepagesToolStripButton;

		// Token: 0x040028B4 RID: 10420
		private ToolStripButton fourpagesToolStripButton;

		// Token: 0x040028B5 RID: 10421
		private ToolStripButton sixpagesToolStripButton;

		// Token: 0x040028B6 RID: 10422
		private ToolStripSeparator separatorToolStripSeparator1;

		// Token: 0x040028B7 RID: 10423
		private ToolStripButton closeToolStripButton;

		// Token: 0x040028B8 RID: 10424
		private ToolStripLabel pageToolStripLabel;

		// Token: 0x040028B9 RID: 10425
		private ImageList imageList;
	}
}
