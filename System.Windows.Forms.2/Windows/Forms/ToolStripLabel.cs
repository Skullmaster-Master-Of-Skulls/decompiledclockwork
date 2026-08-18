using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Windows.Forms.Design;

namespace System.Windows.Forms
{
	// Token: 0x020003E0 RID: 992
	[ToolStripItemDesignerAvailability(ToolStripItemDesignerAvailability.ToolStrip)]
	public class ToolStripLabel : ToolStripItem
	{
		// Token: 0x0600436F RID: 17263 RVA: 0x0011D3D1 File Offset: 0x0011B5D1
		public ToolStripLabel()
		{
		}

		// Token: 0x06004370 RID: 17264 RVA: 0x0011D3FA File Offset: 0x0011B5FA
		public ToolStripLabel(string text) : base(text, null, null)
		{
		}

		// Token: 0x06004371 RID: 17265 RVA: 0x0011D426 File Offset: 0x0011B626
		public ToolStripLabel(Image image) : base(null, image, null)
		{
		}

		// Token: 0x06004372 RID: 17266 RVA: 0x0011D452 File Offset: 0x0011B652
		public ToolStripLabel(string text, Image image) : base(text, image, null)
		{
		}

		// Token: 0x06004373 RID: 17267 RVA: 0x0011D47E File Offset: 0x0011B67E
		public ToolStripLabel(string text, Image image, bool isLink) : this(text, image, isLink, null)
		{
		}

		// Token: 0x06004374 RID: 17268 RVA: 0x0011D48A File Offset: 0x0011B68A
		public ToolStripLabel(string text, Image image, bool isLink, EventHandler onClick) : this(text, image, isLink, onClick, null)
		{
		}

		// Token: 0x06004375 RID: 17269 RVA: 0x0011D498 File Offset: 0x0011B698
		public ToolStripLabel(string text, Image image, bool isLink, EventHandler onClick, string name) : base(text, image, onClick, name)
		{
			this.IsLink = isLink;
		}

		// Token: 0x17001076 RID: 4214
		// (get) Token: 0x06004376 RID: 17270 RVA: 0x0011D4CE File Offset: 0x0011B6CE
		public override bool CanSelect
		{
			get
			{
				return this.IsLink || base.DesignMode;
			}
		}

		// Token: 0x17001077 RID: 4215
		// (get) Token: 0x06004377 RID: 17271 RVA: 0x0011D4E0 File Offset: 0x0011B6E0
		// (set) Token: 0x06004378 RID: 17272 RVA: 0x0011D4E8 File Offset: 0x0011B6E8
		[DefaultValue(false)]
		[SRCategory("CatBehavior")]
		[SRDescription("ToolStripLabelIsLinkDescr")]
		public bool IsLink
		{
			get
			{
				return this.isLink;
			}
			set
			{
				if (this.isLink != value)
				{
					this.isLink = value;
					base.Invalidate();
				}
			}
		}

		// Token: 0x17001078 RID: 4216
		// (get) Token: 0x06004379 RID: 17273 RVA: 0x0011D500 File Offset: 0x0011B700
		// (set) Token: 0x0600437A RID: 17274 RVA: 0x0011D51C File Offset: 0x0011B71C
		[SRCategory("CatAppearance")]
		[SRDescription("ToolStripLabelActiveLinkColorDescr")]
		public Color ActiveLinkColor
		{
			get
			{
				if (this.activeLinkColor.IsEmpty)
				{
					return this.IEActiveLinkColor;
				}
				return this.activeLinkColor;
			}
			set
			{
				if (this.activeLinkColor != value)
				{
					this.activeLinkColor = value;
					base.Invalidate();
				}
			}
		}

		// Token: 0x17001079 RID: 4217
		// (get) Token: 0x0600437B RID: 17275 RVA: 0x000C28F1 File Offset: 0x000C0AF1
		private Color IELinkColor
		{
			get
			{
				return LinkUtilities.IELinkColor;
			}
		}

		// Token: 0x1700107A RID: 4218
		// (get) Token: 0x0600437C RID: 17276 RVA: 0x000C28F8 File Offset: 0x000C0AF8
		private Color IEActiveLinkColor
		{
			get
			{
				return LinkUtilities.IEActiveLinkColor;
			}
		}

		// Token: 0x1700107B RID: 4219
		// (get) Token: 0x0600437D RID: 17277 RVA: 0x000C28FF File Offset: 0x000C0AFF
		private Color IEVisitedLinkColor
		{
			get
			{
				return LinkUtilities.IEVisitedLinkColor;
			}
		}

		// Token: 0x1700107C RID: 4220
		// (get) Token: 0x0600437E RID: 17278 RVA: 0x0011D539 File Offset: 0x0011B739
		// (set) Token: 0x0600437F RID: 17279 RVA: 0x0011D544 File Offset: 0x0011B744
		[DefaultValue(LinkBehavior.SystemDefault)]
		[SRCategory("CatBehavior")]
		[SRDescription("ToolStripLabelLinkBehaviorDescr")]
		public LinkBehavior LinkBehavior
		{
			get
			{
				return this.linkBehavior;
			}
			set
			{
				if (!ClientUtils.IsEnumValid(value, (int)value, 0, 3))
				{
					throw new InvalidEnumArgumentException("LinkBehavior", (int)value, typeof(LinkBehavior));
				}
				if (this.linkBehavior != value)
				{
					this.linkBehavior = value;
					this.InvalidateLinkFonts();
					base.Invalidate();
				}
			}
		}

		// Token: 0x1700107D RID: 4221
		// (get) Token: 0x06004380 RID: 17280 RVA: 0x0011D593 File Offset: 0x0011B793
		// (set) Token: 0x06004381 RID: 17281 RVA: 0x0011D5AF File Offset: 0x0011B7AF
		[SRCategory("CatAppearance")]
		[SRDescription("ToolStripLabelLinkColorDescr")]
		public Color LinkColor
		{
			get
			{
				if (this.linkColor.IsEmpty)
				{
					return this.IELinkColor;
				}
				return this.linkColor;
			}
			set
			{
				if (this.linkColor != value)
				{
					this.linkColor = value;
					base.Invalidate();
				}
			}
		}

		// Token: 0x1700107E RID: 4222
		// (get) Token: 0x06004382 RID: 17282 RVA: 0x0011D5CC File Offset: 0x0011B7CC
		// (set) Token: 0x06004383 RID: 17283 RVA: 0x0011D5D4 File Offset: 0x0011B7D4
		[DefaultValue(false)]
		[SRCategory("CatAppearance")]
		[SRDescription("ToolStripLabelLinkVisitedDescr")]
		public bool LinkVisited
		{
			get
			{
				return this.linkVisited;
			}
			set
			{
				if (this.linkVisited != value)
				{
					this.linkVisited = value;
					base.Invalidate();
				}
			}
		}

		// Token: 0x1700107F RID: 4223
		// (get) Token: 0x06004384 RID: 17284 RVA: 0x0011D5EC File Offset: 0x0011B7EC
		// (set) Token: 0x06004385 RID: 17285 RVA: 0x0011D608 File Offset: 0x0011B808
		[SRCategory("CatAppearance")]
		[SRDescription("ToolStripLabelVisitedLinkColorDescr")]
		public Color VisitedLinkColor
		{
			get
			{
				if (this.visitedLinkColor.IsEmpty)
				{
					return this.IEVisitedLinkColor;
				}
				return this.visitedLinkColor;
			}
			set
			{
				if (this.visitedLinkColor != value)
				{
					this.visitedLinkColor = value;
					base.Invalidate();
				}
			}
		}

		// Token: 0x06004386 RID: 17286 RVA: 0x0011D628 File Offset: 0x0011B828
		private void InvalidateLinkFonts()
		{
			if (this.linkFont != null)
			{
				this.linkFont.Dispose();
			}
			if (this.hoverLinkFont != null && this.hoverLinkFont != this.linkFont)
			{
				this.hoverLinkFont.Dispose();
			}
			this.linkFont = null;
			this.hoverLinkFont = null;
		}

		// Token: 0x06004387 RID: 17287 RVA: 0x0011D677 File Offset: 0x0011B877
		protected override void OnFontChanged(EventArgs e)
		{
			this.InvalidateLinkFonts();
			base.OnFontChanged(e);
		}

		// Token: 0x06004388 RID: 17288 RVA: 0x0011D688 File Offset: 0x0011B888
		protected override void OnMouseEnter(EventArgs e)
		{
			if (this.IsLink)
			{
				ToolStrip parent = base.Parent;
				if (parent != null)
				{
					this.lastCursor = parent.Cursor;
					parent.Cursor = Cursors.Hand;
				}
			}
			base.OnMouseEnter(e);
		}

		// Token: 0x06004389 RID: 17289 RVA: 0x0011D6C8 File Offset: 0x0011B8C8
		protected override void OnMouseLeave(EventArgs e)
		{
			if (this.IsLink)
			{
				ToolStrip parent = base.Parent;
				if (parent != null)
				{
					parent.Cursor = this.lastCursor;
				}
			}
			base.OnMouseLeave(e);
		}

		// Token: 0x0600438A RID: 17290 RVA: 0x0011D6FA File Offset: 0x0011B8FA
		private void ResetActiveLinkColor()
		{
			this.ActiveLinkColor = this.IEActiveLinkColor;
		}

		// Token: 0x0600438B RID: 17291 RVA: 0x0011D708 File Offset: 0x0011B908
		private void ResetLinkColor()
		{
			this.LinkColor = this.IELinkColor;
		}

		// Token: 0x0600438C RID: 17292 RVA: 0x0011D716 File Offset: 0x0011B916
		private void ResetVisitedLinkColor()
		{
			this.VisitedLinkColor = this.IEVisitedLinkColor;
		}

		// Token: 0x0600438D RID: 17293 RVA: 0x0011D724 File Offset: 0x0011B924
		[EditorBrowsable(EditorBrowsableState.Never)]
		private bool ShouldSerializeActiveLinkColor()
		{
			return !this.activeLinkColor.IsEmpty;
		}

		// Token: 0x0600438E RID: 17294 RVA: 0x0011D734 File Offset: 0x0011B934
		[EditorBrowsable(EditorBrowsableState.Never)]
		private bool ShouldSerializeLinkColor()
		{
			return !this.linkColor.IsEmpty;
		}

		// Token: 0x0600438F RID: 17295 RVA: 0x0011D744 File Offset: 0x0011B944
		[EditorBrowsable(EditorBrowsableState.Never)]
		private bool ShouldSerializeVisitedLinkColor()
		{
			return !this.visitedLinkColor.IsEmpty;
		}

		// Token: 0x06004390 RID: 17296 RVA: 0x0011D754 File Offset: 0x0011B954
		internal override ToolStripItemInternalLayout CreateInternalLayout()
		{
			return new ToolStripLabel.ToolStripLabelLayout(this);
		}

		// Token: 0x06004391 RID: 17297 RVA: 0x0011D75C File Offset: 0x0011B95C
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected override AccessibleObject CreateAccessibilityInstance()
		{
			return new ToolStripLabel.ToolStripLabelAccessibleObject(this);
		}

		// Token: 0x06004392 RID: 17298 RVA: 0x0011D764 File Offset: 0x0011B964
		protected override void OnPaint(PaintEventArgs e)
		{
			if (base.Owner != null)
			{
				ToolStripRenderer renderer = base.Renderer;
				renderer.DrawLabelBackground(new ToolStripItemRenderEventArgs(e.Graphics, this));
				if ((this.DisplayStyle & ToolStripItemDisplayStyle.Image) == ToolStripItemDisplayStyle.Image)
				{
					renderer.DrawItemImage(new ToolStripItemImageRenderEventArgs(e.Graphics, this, base.InternalLayout.ImageRectangle));
				}
				this.PaintText(e.Graphics);
			}
		}

		// Token: 0x06004393 RID: 17299 RVA: 0x0011D7C8 File Offset: 0x0011B9C8
		internal void PaintText(Graphics g)
		{
			ToolStripRenderer renderer = base.Renderer;
			if ((this.DisplayStyle & ToolStripItemDisplayStyle.Text) == ToolStripItemDisplayStyle.Text)
			{
				Font font = this.Font;
				Color textColor = this.ForeColor;
				if (this.IsLink)
				{
					LinkUtilities.EnsureLinkFonts(font, this.LinkBehavior, ref this.linkFont, ref this.hoverLinkFont);
					if (this.Pressed)
					{
						font = this.hoverLinkFont;
						textColor = this.ActiveLinkColor;
					}
					else if (this.Selected)
					{
						font = this.hoverLinkFont;
						textColor = (this.LinkVisited ? this.VisitedLinkColor : this.LinkColor);
					}
					else
					{
						font = this.linkFont;
						textColor = (this.LinkVisited ? this.VisitedLinkColor : this.LinkColor);
					}
				}
				Rectangle textRectangle = base.InternalLayout.TextRectangle;
				renderer.DrawItemText(new ToolStripItemTextRenderEventArgs(g, this, this.Text, textRectangle, textColor, font, base.InternalLayout.TextFormat));
			}
		}

		// Token: 0x06004394 RID: 17300 RVA: 0x0011D8A3 File Offset: 0x0011BAA3
		[UIPermission(SecurityAction.LinkDemand, Window = UIPermissionWindow.AllWindows)]
		protected internal override bool ProcessMnemonic(char charCode)
		{
			if (base.ParentInternal != null)
			{
				if (!this.CanSelect)
				{
					base.ParentInternal.SetFocusUnsafe();
					base.ParentInternal.SelectNextToolStripItem(this, true);
				}
				else
				{
					base.FireEvent(ToolStripItemEventType.Click);
				}
				return true;
			}
			return false;
		}

		// Token: 0x040025D8 RID: 9688
		private LinkBehavior linkBehavior;

		// Token: 0x040025D9 RID: 9689
		private bool isLink;

		// Token: 0x040025DA RID: 9690
		private bool linkVisited;

		// Token: 0x040025DB RID: 9691
		private Color linkColor = Color.Empty;

		// Token: 0x040025DC RID: 9692
		private Color activeLinkColor = Color.Empty;

		// Token: 0x040025DD RID: 9693
		private Color visitedLinkColor = Color.Empty;

		// Token: 0x040025DE RID: 9694
		private Font hoverLinkFont;

		// Token: 0x040025DF RID: 9695
		private Font linkFont;

		// Token: 0x040025E0 RID: 9696
		private Cursor lastCursor;

		// Token: 0x02000808 RID: 2056
		[ComVisible(true)]
		internal class ToolStripLabelAccessibleObject : ToolStripItem.ToolStripItemAccessibleObject
		{
			// Token: 0x06006F17 RID: 28439 RVA: 0x001978BC File Offset: 0x00195ABC
			public ToolStripLabelAccessibleObject(ToolStripLabel ownerItem) : base(ownerItem)
			{
				this.ownerItem = ownerItem;
			}

			// Token: 0x06006F18 RID: 28440 RVA: 0x001978CC File Offset: 0x00195ACC
			internal override void ClearOwnerItem()
			{
				this.ownerItem = null;
				base.ClearOwnerItem();
			}

			// Token: 0x17001848 RID: 6216
			// (get) Token: 0x06006F19 RID: 28441 RVA: 0x001978DB File Offset: 0x00195ADB
			public override string DefaultAction
			{
				get
				{
					if (base.IsOwnerItemCleared())
					{
						return string.Empty;
					}
					if (this.ownerItem.IsLink)
					{
						return SR.GetString("AccessibleActionClick");
					}
					return string.Empty;
				}
			}

			// Token: 0x06006F1A RID: 28442 RVA: 0x00197908 File Offset: 0x00195B08
			public override void DoDefaultAction()
			{
				if (base.IsOwnerItemCleared())
				{
					return;
				}
				if (this.ownerItem.IsLink)
				{
					base.DoDefaultAction();
				}
			}

			// Token: 0x06006F1B RID: 28443 RVA: 0x00197926 File Offset: 0x00195B26
			internal override object GetPropertyValue(int propertyID)
			{
				if (AccessibilityImprovements.Level3)
				{
					if (propertyID == 30003)
					{
						return 50020;
					}
					if (propertyID == 30096)
					{
						return this.State;
					}
				}
				return base.GetPropertyValue(propertyID);
			}

			// Token: 0x17001849 RID: 6217
			// (get) Token: 0x06006F1C RID: 28444 RVA: 0x00197960 File Offset: 0x00195B60
			public override AccessibleRole Role
			{
				get
				{
					if (base.IsOwnerItemCleared())
					{
						return AccessibleRole.StaticText;
					}
					AccessibleRole accessibleRole = base.Owner.AccessibleRole;
					if (accessibleRole != AccessibleRole.Default)
					{
						return accessibleRole;
					}
					if (!this.ownerItem.IsLink)
					{
						return AccessibleRole.StaticText;
					}
					return AccessibleRole.Link;
				}
			}

			// Token: 0x1700184A RID: 6218
			// (get) Token: 0x06006F1D RID: 28445 RVA: 0x0019799C File Offset: 0x00195B9C
			public override AccessibleStates State
			{
				get
				{
					return base.State | AccessibleStates.ReadOnly;
				}
			}

			// Token: 0x0400430B RID: 17163
			private ToolStripLabel ownerItem;
		}

		// Token: 0x02000809 RID: 2057
		private class ToolStripLabelLayout : ToolStripItemInternalLayout
		{
			// Token: 0x06006F1E RID: 28446 RVA: 0x001979A7 File Offset: 0x00195BA7
			public ToolStripLabelLayout(ToolStripLabel owner) : base(owner)
			{
				this.owner = owner;
			}

			// Token: 0x06006F1F RID: 28447 RVA: 0x001979B8 File Offset: 0x00195BB8
			protected override ToolStripItemInternalLayout.ToolStripItemLayoutOptions CommonLayoutOptions()
			{
				ToolStripItemInternalLayout.ToolStripItemLayoutOptions toolStripItemLayoutOptions = base.CommonLayoutOptions();
				toolStripItemLayoutOptions.borderSize = 0;
				return toolStripItemLayoutOptions;
			}

			// Token: 0x0400430C RID: 17164
			private ToolStripLabel owner;
		}
	}
}
