using System;
using System.ComponentModel.Design;
using System.Drawing;
using System.Text;

namespace System.Windows.Forms.PropertyGridInternal
{
	// Token: 0x0200050B RID: 1291
	internal class HotCommands : PropertyGrid.SnappableControl
	{
		// Token: 0x060054B0 RID: 21680 RVA: 0x00163062 File Offset: 0x00161262
		internal HotCommands(PropertyGrid owner) : base(owner)
		{
			this.Text = "Command Pane";
		}

		// Token: 0x1700144B RID: 5195
		// (get) Token: 0x060054B1 RID: 21681 RVA: 0x00163084 File Offset: 0x00161284
		// (set) Token: 0x060054B2 RID: 21682 RVA: 0x0016308C File Offset: 0x0016128C
		public virtual bool AllowVisible
		{
			get
			{
				return this.allowVisible;
			}
			set
			{
				if (this.allowVisible != value)
				{
					this.allowVisible = value;
					if (value && this.WouldBeVisible)
					{
						base.Visible = true;
						return;
					}
					base.Visible = false;
				}
			}
		}

		// Token: 0x060054B3 RID: 21683 RVA: 0x001630B8 File Offset: 0x001612B8
		protected override AccessibleObject CreateAccessibilityInstance()
		{
			if (AccessibilityImprovements.Level3)
			{
				return new HotCommandsAccessibleObject(this, this.ownerGrid);
			}
			return base.CreateAccessibilityInstance();
		}

		// Token: 0x1700144C RID: 5196
		// (get) Token: 0x060054B4 RID: 21684 RVA: 0x001630D4 File Offset: 0x001612D4
		public override Rectangle DisplayRectangle
		{
			get
			{
				Size clientSize = base.ClientSize;
				return new Rectangle(4, 4, clientSize.Width - 8, clientSize.Height - 8);
			}
		}

		// Token: 0x1700144D RID: 5197
		// (get) Token: 0x060054B5 RID: 21685 RVA: 0x00163104 File Offset: 0x00161304
		public LinkLabel Label
		{
			get
			{
				if (this.label == null)
				{
					this.label = new LinkLabel();
					this.label.Dock = DockStyle.Fill;
					this.label.LinkBehavior = LinkBehavior.AlwaysUnderline;
					this.label.DisabledLinkColor = SystemColors.ControlDark;
					this.label.LinkClicked += this.LinkClicked;
					base.Controls.Add(this.label);
				}
				return this.label;
			}
		}

		// Token: 0x1700144E RID: 5198
		// (get) Token: 0x060054B6 RID: 21686 RVA: 0x0016317A File Offset: 0x0016137A
		public virtual bool WouldBeVisible
		{
			get
			{
				return this.component != null;
			}
		}

		// Token: 0x060054B7 RID: 21687 RVA: 0x00163188 File Offset: 0x00161388
		public override int GetOptimalHeight(int width)
		{
			if (this.optimalHeight == -1)
			{
				int num = (int)(1.5 * (double)this.Font.Height);
				int num2 = 0;
				if (this.verbs != null)
				{
					num2 = this.verbs.Length;
				}
				this.optimalHeight = num2 * num + 8;
			}
			return this.optimalHeight;
		}

		// Token: 0x060054B8 RID: 21688 RVA: 0x0001B01A File Offset: 0x0001921A
		public override int SnapHeightRequest(int request)
		{
			return request;
		}

		// Token: 0x1700144F RID: 5199
		// (get) Token: 0x060054B9 RID: 21689 RVA: 0x000A8615 File Offset: 0x000A6815
		internal override bool SupportsUiaProviders
		{
			get
			{
				return AccessibilityImprovements.Level3;
			}
		}

		// Token: 0x060054BA RID: 21690 RVA: 0x001631DC File Offset: 0x001613DC
		private void LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			try
			{
				if (e.Link.Enabled)
				{
					((DesignerVerb)e.Link.LinkData).Invoke();
				}
			}
			catch (Exception ex)
			{
				RTLAwareMessageBox.Show(this, ex.Message, SR.GetString("PBRSErrorTitle"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1, (MessageBoxOptions)0);
			}
		}

		// Token: 0x060054BB RID: 21691 RVA: 0x00163240 File Offset: 0x00161440
		private void OnCommandChanged(object sender, EventArgs e)
		{
			this.SetupLabel();
		}

		// Token: 0x060054BC RID: 21692 RVA: 0x00163248 File Offset: 0x00161448
		protected override void OnGotFocus(EventArgs e)
		{
			this.Label.FocusInternal();
			this.Label.Invalidate();
		}

		// Token: 0x060054BD RID: 21693 RVA: 0x00163261 File Offset: 0x00161461
		protected override void OnFontChanged(EventArgs e)
		{
			base.OnFontChanged(e);
			this.optimalHeight = -1;
		}

		// Token: 0x060054BE RID: 21694 RVA: 0x00163274 File Offset: 0x00161474
		internal void SetColors(Color background, Color normalText, Color link, Color activeLink, Color visitedLink, Color disabledLink)
		{
			this.Label.BackColor = background;
			this.Label.ForeColor = normalText;
			this.Label.LinkColor = link;
			this.Label.ActiveLinkColor = activeLink;
			this.Label.VisitedLinkColor = visitedLink;
			this.Label.DisabledLinkColor = disabledLink;
		}

		// Token: 0x060054BF RID: 21695 RVA: 0x001632CC File Offset: 0x001614CC
		public void Select(bool forward)
		{
			this.Label.FocusInternal();
		}

		// Token: 0x060054C0 RID: 21696 RVA: 0x001632DC File Offset: 0x001614DC
		public virtual void SetVerbs(object component, DesignerVerb[] verbs)
		{
			if (this.verbs != null)
			{
				for (int i = 0; i < this.verbs.Length; i++)
				{
					this.verbs[i].CommandChanged -= this.OnCommandChanged;
				}
				this.component = null;
				this.verbs = null;
			}
			if (component == null || verbs == null || verbs.Length == 0)
			{
				base.Visible = false;
				this.Label.Links.Clear();
				this.Label.Text = null;
			}
			else
			{
				this.component = component;
				this.verbs = verbs;
				for (int j = 0; j < verbs.Length; j++)
				{
					verbs[j].CommandChanged += this.OnCommandChanged;
				}
				if (this.allowVisible)
				{
					base.Visible = true;
				}
				this.SetupLabel();
			}
			this.optimalHeight = -1;
		}

		// Token: 0x060054C1 RID: 21697 RVA: 0x001633A8 File Offset: 0x001615A8
		private void SetupLabel()
		{
			this.Label.Links.Clear();
			StringBuilder stringBuilder = new StringBuilder();
			Point[] array = new Point[this.verbs.Length];
			int num = 0;
			bool flag = true;
			for (int i = 0; i < this.verbs.Length; i++)
			{
				if (this.verbs[i].Visible && this.verbs[i].Supported)
				{
					if (!flag)
					{
						stringBuilder.Append(Application.CurrentCulture.TextInfo.ListSeparator);
						stringBuilder.Append(" ");
						num += 2;
					}
					string text = this.verbs[i].Text;
					array[i] = new Point(num, text.Length);
					stringBuilder.Append(text);
					num += text.Length;
					flag = false;
				}
			}
			this.Label.Text = stringBuilder.ToString();
			for (int j = 0; j < this.verbs.Length; j++)
			{
				if (this.verbs[j].Visible && this.verbs[j].Supported)
				{
					LinkLabel.Link link = this.Label.Links.Add(array[j].X, array[j].Y, this.verbs[j]);
					if (!this.verbs[j].Enabled)
					{
						link.Enabled = false;
					}
				}
			}
		}

		// Token: 0x04003724 RID: 14116
		private object component;

		// Token: 0x04003725 RID: 14117
		private DesignerVerb[] verbs;

		// Token: 0x04003726 RID: 14118
		private LinkLabel label;

		// Token: 0x04003727 RID: 14119
		private bool allowVisible = true;

		// Token: 0x04003728 RID: 14120
		private int optimalHeight = -1;
	}
}
