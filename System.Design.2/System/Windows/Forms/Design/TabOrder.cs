using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Design;
using System.Drawing;
using System.Globalization;
using System.Text;

namespace System.Windows.Forms.Design
{
	// Token: 0x02000341 RID: 833
	[ToolboxItem(false)]
	[DesignTimeVisible(false)]
	internal class TabOrder : Control, IMouseHandler, IMenuStatusHandler
	{
		// Token: 0x06002108 RID: 8456 RVA: 0x000C9EB4 File Offset: 0x000C80B4
		public TabOrder(IDesignerHost host)
		{
			this.host = host;
			IUIService iuiservice = (IUIService)host.GetService(typeof(IUIService));
			if (iuiservice != null)
			{
				this.tabFont = (Font)iuiservice.Styles["DialogFont"];
			}
			else
			{
				this.tabFont = Control.DefaultFont;
			}
			this.tabFont = new Font(this.tabFont, FontStyle.Bold);
			this.selSize = DesignerUtils.GetAdornmentDimensions(AdornmentType.GrabHandle).Width;
			this.drawString = new StringBuilder(12);
			this.highlightTextBrush = new SolidBrush(SystemColors.HighlightText);
			this.highlightPen = new Pen(SystemColors.Highlight);
			NumberFormatInfo numberFormatInfo = (NumberFormatInfo)CultureInfo.CurrentCulture.GetFormat(typeof(NumberFormatInfo));
			if (numberFormatInfo != null)
			{
				this.decimalSep = numberFormatInfo.NumberDecimalSeparator;
			}
			else
			{
				this.decimalSep = ".";
			}
			this.tabProperties = new Hashtable();
			base.SetStyle(ControlStyles.Opaque, true);
			IOverlayService overlayService = (IOverlayService)host.GetService(typeof(IOverlayService));
			if (overlayService != null)
			{
				overlayService.PushOverlay(this);
			}
			IHelpService helpService = (IHelpService)host.GetService(typeof(IHelpService));
			if (helpService != null)
			{
				helpService.AddContextAttribute("Keyword", "TabOrderView", HelpKeywordType.FilterKeyword);
			}
			this.commands = new MenuCommand[]
			{
				new MenuCommand(new EventHandler(this.OnKeyCancel), MenuCommands.KeyCancel),
				new MenuCommand(new EventHandler(this.OnKeyDefault), MenuCommands.KeyDefaultAction),
				new MenuCommand(new EventHandler(this.OnKeyPrevious), MenuCommands.KeyMoveUp),
				new MenuCommand(new EventHandler(this.OnKeyNext), MenuCommands.KeyMoveDown),
				new MenuCommand(new EventHandler(this.OnKeyPrevious), MenuCommands.KeyMoveLeft),
				new MenuCommand(new EventHandler(this.OnKeyNext), MenuCommands.KeyMoveRight),
				new MenuCommand(new EventHandler(this.OnKeyNext), MenuCommands.KeySelectNext),
				new MenuCommand(new EventHandler(this.OnKeyPrevious), MenuCommands.KeySelectPrevious)
			};
			this.newCommands = new MenuCommand[]
			{
				new MenuCommand(new EventHandler(this.OnKeyDefault), MenuCommands.KeyTabOrderSelect)
			};
			IMenuCommandService menuCommandService = (IMenuCommandService)host.GetService(typeof(IMenuCommandService));
			if (menuCommandService != null)
			{
				foreach (MenuCommand command in this.newCommands)
				{
					menuCommandService.AddCommand(command);
				}
			}
			IEventHandlerService eventHandlerService = (IEventHandlerService)host.GetService(typeof(IEventHandlerService));
			if (eventHandlerService != null)
			{
				eventHandlerService.PushHandler(this);
			}
			IComponentChangeService componentChangeService = (IComponentChangeService)host.GetService(typeof(IComponentChangeService));
			if (componentChangeService != null)
			{
				componentChangeService.ComponentAdded += this.OnComponentAddRemove;
				componentChangeService.ComponentRemoved += this.OnComponentAddRemove;
				componentChangeService.ComponentChanged += this.OnComponentChanged;
			}
		}

		// Token: 0x06002109 RID: 8457 RVA: 0x000CA1B0 File Offset: 0x000C83B0
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (this.region != null)
				{
					this.region.Dispose();
					this.region = null;
				}
				if (this.host != null)
				{
					IOverlayService overlayService = (IOverlayService)this.host.GetService(typeof(IOverlayService));
					if (overlayService != null)
					{
						overlayService.RemoveOverlay(this);
					}
					IEventHandlerService eventHandlerService = (IEventHandlerService)this.host.GetService(typeof(IEventHandlerService));
					if (eventHandlerService != null)
					{
						eventHandlerService.PopHandler(this);
					}
					IMenuCommandService menuCommandService = (IMenuCommandService)this.host.GetService(typeof(IMenuCommandService));
					if (menuCommandService != null)
					{
						foreach (MenuCommand command in this.newCommands)
						{
							menuCommandService.RemoveCommand(command);
						}
					}
					IComponentChangeService componentChangeService = (IComponentChangeService)this.host.GetService(typeof(IComponentChangeService));
					if (componentChangeService != null)
					{
						componentChangeService.ComponentAdded -= this.OnComponentAddRemove;
						componentChangeService.ComponentRemoved -= this.OnComponentAddRemove;
						componentChangeService.ComponentChanged -= this.OnComponentChanged;
					}
					IHelpService helpService = (IHelpService)this.host.GetService(typeof(IHelpService));
					if (helpService != null)
					{
						helpService.RemoveContextAttribute("Keyword", "TabOrderView");
					}
					this.host = null;
				}
			}
			base.Dispose(disposing);
		}

		// Token: 0x0600210A RID: 8458 RVA: 0x000CA310 File Offset: 0x000C8510
		private void DrawTabs(IList tabs, Graphics gr, bool fRegion)
		{
			IEnumerator enumerator = tabs.GetEnumerator();
			int num = 0;
			Rectangle rectangle = Rectangle.Empty;
			Size size = Size.Empty;
			Font font = this.tabFont;
			if (fRegion)
			{
				this.region = new Region(new Rectangle(0, 0, 0, 0));
			}
			if (this.ctlHover != null)
			{
				Rectangle convertedBounds = this.GetConvertedBounds(this.ctlHover);
				Rectangle rectangle2 = convertedBounds;
				rectangle2.Inflate(this.selSize, this.selSize);
				if (fRegion)
				{
					this.region = new Region(rectangle2);
					this.region.Exclude(convertedBounds);
				}
				else
				{
					Control parent = this.ctlHover.Parent;
					Color backColor = parent.BackColor;
					Region clip = gr.Clip;
					gr.ExcludeClip(convertedBounds);
					using (SolidBrush solidBrush = new SolidBrush(backColor))
					{
						gr.FillRectangle(solidBrush, rectangle2);
					}
					ControlPaint.DrawSelectionFrame(gr, false, rectangle2, convertedBounds, backColor);
					gr.Clip = clip;
				}
			}
			while (enumerator.MoveNext())
			{
				object obj = enumerator.Current;
				Control control = (Control)obj;
				rectangle = this.GetConvertedBounds(control);
				this.drawString.Length = 0;
				Control sitedParent = this.GetSitedParent(control);
				Control control2 = (Control)this.host.RootComponent;
				while (sitedParent != control2 && sitedParent != null)
				{
					this.drawString.Insert(0, this.decimalSep);
					this.drawString.Insert(0, sitedParent.TabIndex.ToString(CultureInfo.CurrentCulture));
					sitedParent = this.GetSitedParent(sitedParent);
				}
				this.drawString.Insert(0, ' ');
				this.drawString.Append(control.TabIndex.ToString(CultureInfo.CurrentCulture));
				this.drawString.Append(' ');
				if (((PropertyDescriptor)this.tabProperties[control]).IsReadOnly)
				{
					this.drawString.Append(SR.GetString("WindowsFormsTabOrderReadOnly"));
					this.drawString.Append(' ');
				}
				string text = this.drawString.ToString();
				size = Size.Ceiling(gr.MeasureString(text, font));
				rectangle.Width = size.Width + 2;
				rectangle.Height = size.Height + 2;
				this.tabGlyphs[num++] = rectangle;
				if (fRegion)
				{
					this.region.Union(rectangle);
				}
				else
				{
					Brush highlight;
					Pen highlightText;
					Color color;
					if (this.tabComplete.IndexOf(control) != -1)
					{
						highlight = this.highlightTextBrush;
						highlightText = this.highlightPen;
						color = SystemColors.Highlight;
					}
					else
					{
						highlight = SystemBrushes.Highlight;
						highlightText = SystemPens.HighlightText;
						color = SystemColors.HighlightText;
					}
					gr.FillRectangle(highlight, rectangle);
					gr.DrawRectangle(highlightText, rectangle.X, rectangle.Y, rectangle.Width - 1, rectangle.Height - 1);
					Brush brush = new SolidBrush(color);
					gr.DrawString(text, font, brush, (float)(rectangle.X + 1), (float)(rectangle.Y + 1));
					brush.Dispose();
				}
			}
			if (fRegion)
			{
				Control control = (Control)this.host.RootComponent;
				rectangle = this.GetConvertedBounds(control);
				this.region.Intersect(rectangle);
				base.Region = this.region;
			}
		}

		// Token: 0x0600210B RID: 8459 RVA: 0x000CA658 File Offset: 0x000C8858
		private Control GetControlAtPoint(IList tabs, int x, int y)
		{
			IEnumerator enumerator = tabs.GetEnumerator();
			Control result = null;
			while (enumerator.MoveNext())
			{
				object obj = enumerator.Current;
				Control control = (Control)obj;
				Control sitedParent = this.GetSitedParent(control);
				Rectangle bounds = control.Bounds;
				if (sitedParent.RectangleToScreen(bounds).Contains(x, y))
				{
					result = control;
				}
			}
			return result;
		}

		// Token: 0x0600210C RID: 8460 RVA: 0x000CA6AC File Offset: 0x000C88AC
		private Rectangle GetConvertedBounds(Control ctl)
		{
			Control parent = ctl.Parent;
			Rectangle r = ctl.Bounds;
			r = parent.RectangleToScreen(r);
			return base.RectangleToClient(r);
		}

		// Token: 0x0600210D RID: 8461 RVA: 0x000CA6D8 File Offset: 0x000C88D8
		private int GetMaxControlCount(Control ctl)
		{
			int num = 0;
			for (int i = 0; i < ctl.Controls.Count; i++)
			{
				if (this.GetTabbable(ctl.Controls[i]))
				{
					num++;
				}
			}
			return num;
		}

		// Token: 0x0600210E RID: 8462 RVA: 0x000CA718 File Offset: 0x000C8918
		private Control GetSitedParent(Control child)
		{
			Control parent;
			for (parent = child.Parent; parent != null; parent = parent.Parent)
			{
				ISite site = parent.Site;
				IContainer container = null;
				if (site != null)
				{
					container = site.Container;
				}
				container = DesignerUtils.CheckForNestedContainer(container);
				if (site != null && container == this.host)
				{
					break;
				}
			}
			return parent;
		}

		// Token: 0x0600210F RID: 8463 RVA: 0x000CA760 File Offset: 0x000C8960
		private void GetTabbing(Control ctl, IList tabs)
		{
			int count = ctl.Controls.Count;
			for (int i = count - 1; i >= 0; i--)
			{
				Control control = ctl.Controls[i];
				if (this.GetSitedParent(control) != null && this.GetTabbable(control))
				{
					tabs.Add(control);
				}
				if (control.Controls.Count > 0)
				{
					this.GetTabbing(control, tabs);
				}
			}
		}

		// Token: 0x06002110 RID: 8464 RVA: 0x000CA7C4 File Offset: 0x000C89C4
		private bool GetTabbable(Control control)
		{
			for (Control control2 = control; control2 != null; control2 = control2.Parent)
			{
				if (!control2.Visible)
				{
					return false;
				}
			}
			ISite site = control.Site;
			if (site == null || site.Container != this.host)
			{
				return false;
			}
			PropertyDescriptor propertyDescriptor = TypeDescriptor.GetProperties(control)["TabIndex"];
			if (propertyDescriptor == null || !propertyDescriptor.IsBrowsable)
			{
				return false;
			}
			this.tabProperties[control] = propertyDescriptor;
			return true;
		}

		// Token: 0x06002111 RID: 8465 RVA: 0x000CA830 File Offset: 0x000C8A30
		private void OnComponentAddRemove(object sender, ComponentEventArgs ce)
		{
			this.ctlHover = null;
			this.tabControls = null;
			this.tabGlyphs = null;
			if (this.tabComplete != null)
			{
				this.tabComplete.Clear();
			}
			if (this.tabNext != null)
			{
				this.tabNext.Clear();
			}
			if (this.region != null)
			{
				this.region.Dispose();
				this.region = null;
			}
			base.Invalidate();
		}

		// Token: 0x06002112 RID: 8466 RVA: 0x000CA898 File Offset: 0x000C8A98
		private void OnComponentChanged(object sender, ComponentChangedEventArgs ce)
		{
			this.tabControls = null;
			this.tabGlyphs = null;
			if (this.region != null)
			{
				this.region.Dispose();
				this.region = null;
			}
			base.Invalidate();
		}

		// Token: 0x06002113 RID: 8467 RVA: 0x000CA8C8 File Offset: 0x000C8AC8
		private void OnKeyCancel(object sender, EventArgs e)
		{
			IMenuCommandService menuCommandService = (IMenuCommandService)this.host.GetService(typeof(IMenuCommandService));
			if (menuCommandService != null)
			{
				MenuCommand menuCommand = menuCommandService.FindCommand(StandardCommands.TabOrder);
				if (menuCommand != null)
				{
					menuCommand.Invoke();
				}
			}
		}

		// Token: 0x06002114 RID: 8468 RVA: 0x000CA908 File Offset: 0x000C8B08
		private void OnKeyDefault(object sender, EventArgs e)
		{
			if (this.ctlHover != null)
			{
				this.SetNextTabIndex(this.ctlHover);
				this.RotateControls(true);
			}
		}

		// Token: 0x06002115 RID: 8469 RVA: 0x000CA925 File Offset: 0x000C8B25
		private void OnKeyNext(object sender, EventArgs e)
		{
			this.RotateControls(true);
		}

		// Token: 0x06002116 RID: 8470 RVA: 0x000CA92E File Offset: 0x000C8B2E
		private void OnKeyPrevious(object sender, EventArgs e)
		{
			this.RotateControls(false);
		}

		// Token: 0x06002117 RID: 8471 RVA: 0x00003937 File Offset: 0x00001B37
		public virtual void OnMouseDoubleClick(IComponent component)
		{
		}

		// Token: 0x06002118 RID: 8472 RVA: 0x000CA937 File Offset: 0x000C8B37
		public virtual void OnMouseDown(IComponent component, MouseButtons button, int x, int y)
		{
			if (this.ctlHover != null)
			{
				this.SetNextTabIndex(this.ctlHover);
			}
		}

		// Token: 0x06002119 RID: 8473 RVA: 0x000CA94D File Offset: 0x000C8B4D
		protected override void OnMouseDown(MouseEventArgs e)
		{
			base.OnMouseDown(e);
			if (this.ctlHover != null)
			{
				this.SetNextTabIndex(this.ctlHover);
			}
		}

		// Token: 0x0600211A RID: 8474 RVA: 0x00003937 File Offset: 0x00001B37
		public virtual void OnMouseHover(IComponent component)
		{
		}

		// Token: 0x0600211B RID: 8475 RVA: 0x000CA96C File Offset: 0x000C8B6C
		public virtual void OnMouseMove(IComponent component, int x, int y)
		{
			if (this.tabControls != null)
			{
				Control controlAtPoint = this.GetControlAtPoint(this.tabControls, x, y);
				this.SetNewHover(controlAtPoint);
			}
		}

		// Token: 0x0600211C RID: 8476 RVA: 0x000CA998 File Offset: 0x000C8B98
		protected override void OnMouseMove(MouseEventArgs e)
		{
			base.OnMouseMove(e);
			if (this.tabGlyphs != null)
			{
				Control newHover = null;
				for (int i = 0; i < this.tabGlyphs.Length; i++)
				{
					if (this.tabGlyphs[i].Contains(e.X, e.Y))
					{
						newHover = (Control)this.tabControls[i];
					}
				}
				this.SetNewHover(newHover);
			}
			this.SetAppropriateCursor();
		}

		// Token: 0x0600211D RID: 8477 RVA: 0x00003937 File Offset: 0x00001B37
		public virtual void OnMouseUp(IComponent component, MouseButtons button)
		{
		}

		// Token: 0x0600211E RID: 8478 RVA: 0x000CAA07 File Offset: 0x000C8C07
		private void SetAppropriateCursor()
		{
			if (this.ctlHover != null)
			{
				Cursor.Current = Cursors.Cross;
				return;
			}
			Cursor.Current = Cursors.Default;
		}

		// Token: 0x0600211F RID: 8479 RVA: 0x000CAA26 File Offset: 0x000C8C26
		public virtual void OnSetCursor(IComponent component)
		{
			this.SetAppropriateCursor();
		}

		// Token: 0x06002120 RID: 8480 RVA: 0x000CAA30 File Offset: 0x000C8C30
		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);
			if (this.tabControls == null)
			{
				this.tabControls = new ArrayList();
				this.GetTabbing((Control)this.host.RootComponent, this.tabControls);
				this.tabGlyphs = new Rectangle[this.tabControls.Count];
			}
			if (this.tabComplete == null)
			{
				this.tabComplete = new ArrayList();
			}
			if (this.tabNext == null)
			{
				this.tabNext = new Hashtable();
			}
			if (this.region == null)
			{
				this.DrawTabs(this.tabControls, e.Graphics, true);
			}
			this.DrawTabs(this.tabControls, e.Graphics, false);
		}

		// Token: 0x06002121 RID: 8481 RVA: 0x000CAAE0 File Offset: 0x000C8CE0
		public bool OverrideInvoke(MenuCommand cmd)
		{
			for (int i = 0; i < this.commands.Length; i++)
			{
				if (this.commands[i].CommandID.Equals(cmd.CommandID))
				{
					this.commands[i].Invoke();
					return true;
				}
			}
			return false;
		}

		// Token: 0x06002122 RID: 8482 RVA: 0x000CAB2C File Offset: 0x000C8D2C
		public bool OverrideStatus(MenuCommand cmd)
		{
			for (int i = 0; i < this.commands.Length; i++)
			{
				if (this.commands[i].CommandID.Equals(cmd.CommandID))
				{
					cmd.Enabled = this.commands[i].Enabled;
					return true;
				}
			}
			if (!cmd.CommandID.Equals(StandardCommands.TabOrder))
			{
				cmd.Enabled = false;
				return true;
			}
			return false;
		}

		// Token: 0x06002123 RID: 8483 RVA: 0x000CAB98 File Offset: 0x000C8D98
		private void RotateControls(bool forward)
		{
			Control control = this.ctlHover;
			Control control2 = (Control)this.host.RootComponent;
			if (control == null)
			{
				control = control2;
			}
			while ((control = control2.GetNextControl(control, forward)) != null && !this.GetTabbable(control))
			{
			}
			this.SetNewHover(control);
		}

		// Token: 0x06002124 RID: 8484 RVA: 0x000CABE0 File Offset: 0x000C8DE0
		private void SetNewHover(Control ctl)
		{
			if (this.ctlHover != ctl)
			{
				if (this.ctlHover != null)
				{
					if (this.region != null)
					{
						this.region.Dispose();
						this.region = null;
					}
					Rectangle convertedBounds = this.GetConvertedBounds(this.ctlHover);
					convertedBounds.Inflate(this.selSize, this.selSize);
					base.Invalidate(convertedBounds);
				}
				this.ctlHover = ctl;
				if (this.ctlHover != null)
				{
					if (this.region != null)
					{
						this.region.Dispose();
						this.region = null;
					}
					Rectangle convertedBounds2 = this.GetConvertedBounds(this.ctlHover);
					convertedBounds2.Inflate(this.selSize, this.selSize);
					base.Invalidate(convertedBounds2);
				}
			}
		}

		// Token: 0x06002125 RID: 8485 RVA: 0x000CAC94 File Offset: 0x000C8E94
		private void SetNextTabIndex(Control ctl)
		{
			if (this.tabControls != null)
			{
				Control sitedParent = this.GetSitedParent(ctl);
				object obj = this.tabNext[sitedParent];
				if (this.tabComplete.IndexOf(ctl) == -1)
				{
					this.tabComplete.Add(ctl);
				}
				int num;
				if (obj != null)
				{
					num = (int)obj;
				}
				else
				{
					num = 0;
				}
				try
				{
					PropertyDescriptor propertyDescriptor = (PropertyDescriptor)this.tabProperties[ctl];
					if (propertyDescriptor != null)
					{
						int num2 = num + 1;
						if (propertyDescriptor.IsReadOnly)
						{
							num2 = (int)propertyDescriptor.GetValue(ctl) + 1;
						}
						int maxControlCount = this.GetMaxControlCount(sitedParent);
						if (num2 >= maxControlCount)
						{
							num2 = 0;
						}
						this.tabNext[sitedParent] = num2;
						if (this.tabComplete.Count == this.tabControls.Count)
						{
							this.tabComplete.Clear();
						}
						if (!propertyDescriptor.IsReadOnly)
						{
							try
							{
								propertyDescriptor.SetValue(ctl, num);
								goto IL_EC;
							}
							catch (Exception)
							{
								goto IL_EC;
							}
						}
						base.Invalidate();
					}
					IL_EC:;
				}
				catch (Exception ex)
				{
					if (ClientUtils.IsCriticalException(ex))
					{
						throw;
					}
				}
			}
		}

		// Token: 0x04001915 RID: 6421
		private IDesignerHost host;

		// Token: 0x04001916 RID: 6422
		private Control ctlHover;

		// Token: 0x04001917 RID: 6423
		private ArrayList tabControls;

		// Token: 0x04001918 RID: 6424
		private Rectangle[] tabGlyphs;

		// Token: 0x04001919 RID: 6425
		private ArrayList tabComplete;

		// Token: 0x0400191A RID: 6426
		private Hashtable tabNext;

		// Token: 0x0400191B RID: 6427
		private Font tabFont;

		// Token: 0x0400191C RID: 6428
		private StringBuilder drawString;

		// Token: 0x0400191D RID: 6429
		private Brush highlightTextBrush;

		// Token: 0x0400191E RID: 6430
		private Pen highlightPen;

		// Token: 0x0400191F RID: 6431
		private int selSize;

		// Token: 0x04001920 RID: 6432
		private Hashtable tabProperties;

		// Token: 0x04001921 RID: 6433
		private Region region;

		// Token: 0x04001922 RID: 6434
		private MenuCommand[] commands;

		// Token: 0x04001923 RID: 6435
		private MenuCommand[] newCommands;

		// Token: 0x04001924 RID: 6436
		private string decimalSep;
	}
}
