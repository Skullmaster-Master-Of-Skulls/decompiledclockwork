using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Windows.Forms.Internal;

namespace System.Windows.Forms.Design
{
	// Token: 0x02000488 RID: 1160
	[ComVisible(true)]
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[ToolboxItem(false)]
	[PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
	[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
	public partial class ComponentEditorForm : Form
	{
		// Token: 0x06004DE0 RID: 19936 RVA: 0x00141AD0 File Offset: 0x0013FCD0
		public ComponentEditorForm(object component, Type[] pageTypes)
		{
			if (!(component is IComponent))
			{
				throw new ArgumentException(SR.GetString("ComponentEditorFormBadComponent"), "component");
			}
			this.component = (IComponent)component;
			this.pageTypes = pageTypes;
			this.dirty = false;
			this.firstActivate = true;
			this.activePage = -1;
			this.initialActivePage = 0;
			base.FormBorderStyle = FormBorderStyle.FixedDialog;
			base.MinimizeBox = false;
			base.MaximizeBox = false;
			base.ShowInTaskbar = false;
			base.Icon = null;
			base.StartPosition = FormStartPosition.CenterParent;
			this.OnNewObjects();
			this.OnConfigureUI();
		}

		// Token: 0x06004DE1 RID: 19937 RVA: 0x00141B7C File Offset: 0x0013FD7C
		internal virtual void ApplyChanges(bool lastApply)
		{
			if (this.dirty)
			{
				IComponentChangeService componentChangeService = null;
				if (this.component.Site != null)
				{
					componentChangeService = (IComponentChangeService)this.component.Site.GetService(typeof(IComponentChangeService));
					if (componentChangeService != null)
					{
						try
						{
							componentChangeService.OnComponentChanging(this.component, null);
						}
						catch (CheckoutException ex)
						{
							if (ex == CheckoutException.Canceled)
							{
								return;
							}
							throw ex;
						}
					}
				}
				for (int i = 0; i < this.pageSites.Length; i++)
				{
					if (this.pageSites[i].Dirty)
					{
						this.pageSites[i].GetPageControl().ApplyChanges();
						this.pageSites[i].Dirty = false;
					}
				}
				if (componentChangeService != null)
				{
					componentChangeService.OnComponentChanged(this.component, null, null, null);
				}
				this.applyButton.Enabled = false;
				this.cancelButton.Text = SR.GetString("CloseCaption");
				this.dirty = false;
				if (!lastApply)
				{
					for (int j = 0; j < this.pageSites.Length; j++)
					{
						this.pageSites[j].GetPageControl().OnApplyComplete();
					}
				}
			}
		}

		// Token: 0x1700132A RID: 4906
		// (get) Token: 0x06004DE2 RID: 19938 RVA: 0x0010927E File Offset: 0x0010747E
		// (set) Token: 0x06004DE3 RID: 19939 RVA: 0x00109286 File Offset: 0x00107486
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

		// Token: 0x14000409 RID: 1033
		// (add) Token: 0x06004DE4 RID: 19940 RVA: 0x0010928F File Offset: 0x0010748F
		// (remove) Token: 0x06004DE5 RID: 19941 RVA: 0x00109298 File Offset: 0x00107498
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

		// Token: 0x06004DE6 RID: 19942 RVA: 0x00141C9C File Offset: 0x0013FE9C
		private void OnButtonClick(object sender, EventArgs e)
		{
			if (sender == this.okButton)
			{
				this.ApplyChanges(true);
				base.DialogResult = DialogResult.OK;
				return;
			}
			if (sender == this.cancelButton)
			{
				base.DialogResult = DialogResult.Cancel;
				return;
			}
			if (sender == this.applyButton)
			{
				this.ApplyChanges(false);
				return;
			}
			if (sender == this.helpButton)
			{
				this.ShowPageHelp();
			}
		}

		// Token: 0x06004DE7 RID: 19943 RVA: 0x00141CF4 File Offset: 0x0013FEF4
		private void OnConfigureUI()
		{
			Font font = Control.DefaultFont;
			if (this.component.Site != null)
			{
				IUIService iuiservice = (IUIService)this.component.Site.GetService(typeof(IUIService));
				if (iuiservice != null)
				{
					font = (Font)iuiservice.Styles["DialogFont"];
				}
			}
			this.Font = font;
			this.okButton = new Button();
			this.cancelButton = new Button();
			this.applyButton = new Button();
			this.helpButton = new Button();
			this.selectorImageList = new ImageList();
			this.selectorImageList.ImageSize = new Size(16, 16);
			this.selector = new ComponentEditorForm.PageSelector();
			this.selector.ImageList = this.selectorImageList;
			this.selector.AfterSelect += this.OnSelChangeSelector;
			Label label = new Label();
			label.BackColor = SystemColors.ControlDark;
			int num = 90;
			if (this.pageSites != null)
			{
				for (int i = 0; i < this.pageSites.Length; i++)
				{
					ComponentEditorPage pageControl = this.pageSites[i].GetPageControl();
					string title = pageControl.Title;
					Graphics graphics = base.CreateGraphicsInternal();
					int num2 = (int)graphics.MeasureString(title, this.Font).Width;
					graphics.Dispose();
					this.selectorImageList.Images.Add(pageControl.Icon.ToBitmap());
					this.selector.Nodes.Add(new TreeNode(title, i, i));
					if (num2 > num)
					{
						num = num2;
					}
				}
			}
			num += 10;
			string text = string.Empty;
			ISite site = this.component.Site;
			if (site != null)
			{
				text = SR.GetString("ComponentEditorFormProperties", new object[]
				{
					site.Name
				});
			}
			else
			{
				text = SR.GetString("ComponentEditorFormPropertiesNoName");
			}
			this.Text = text;
			Rectangle rectangle = new Rectangle(12 + num, 16, this.maxSize.Width, this.maxSize.Height);
			this.pageHost.Bounds = rectangle;
			label.Bounds = new Rectangle(rectangle.X, 6, rectangle.Width, 4);
			if (this.pageSites != null)
			{
				Rectangle bounds = new Rectangle(0, 0, rectangle.Width, rectangle.Height);
				for (int j = 0; j < this.pageSites.Length; j++)
				{
					ComponentEditorPage pageControl2 = this.pageSites[j].GetPageControl();
					pageControl2.GetControl().Bounds = bounds;
				}
			}
			int width = SystemInformation.FixedFrameBorderSize.Width;
			Rectangle bounds2 = rectangle;
			Size size = new Size(bounds2.Width + 3 * (6 + width) + num, bounds2.Height + 4 + 24 + 23 + 2 * width + SystemInformation.CaptionHeight);
			base.Size = size;
			this.selector.Bounds = new Rectangle(6, 6, num, bounds2.Height + 4 + 12 + 23);
			bounds2.X = bounds2.Width + bounds2.X - 80;
			bounds2.Y = bounds2.Height + bounds2.Y + 6;
			bounds2.Width = 80;
			bounds2.Height = 23;
			this.helpButton.Bounds = bounds2;
			this.helpButton.Text = SR.GetString("HelpCaption");
			this.helpButton.Click += this.OnButtonClick;
			this.helpButton.Enabled = false;
			this.helpButton.FlatStyle = FlatStyle.System;
			bounds2.X -= 86;
			this.applyButton.Bounds = bounds2;
			this.applyButton.Text = SR.GetString("ApplyCaption");
			this.applyButton.Click += this.OnButtonClick;
			this.applyButton.Enabled = false;
			this.applyButton.FlatStyle = FlatStyle.System;
			bounds2.X -= 86;
			this.cancelButton.Bounds = bounds2;
			this.cancelButton.Text = SR.GetString("CancelCaption");
			this.cancelButton.Click += this.OnButtonClick;
			this.cancelButton.FlatStyle = FlatStyle.System;
			base.CancelButton = this.cancelButton;
			bounds2.X -= 86;
			this.okButton.Bounds = bounds2;
			this.okButton.Text = SR.GetString("OKCaption");
			this.okButton.Click += this.OnButtonClick;
			this.okButton.FlatStyle = FlatStyle.System;
			base.AcceptButton = this.okButton;
			base.Controls.Clear();
			base.Controls.AddRange(new Control[]
			{
				this.selector,
				label,
				this.pageHost,
				this.okButton,
				this.cancelButton,
				this.applyButton,
				this.helpButton
			});
			this.AutoScaleBaseSize = new Size(5, 14);
			base.ApplyAutoScaling();
		}

		// Token: 0x06004DE8 RID: 19944 RVA: 0x00142210 File Offset: 0x00140410
		protected override void OnActivated(EventArgs e)
		{
			base.OnActivated(e);
			if (this.firstActivate)
			{
				this.firstActivate = false;
				this.selector.SelectedNode = this.selector.Nodes[this.initialActivePage];
				this.pageSites[this.initialActivePage].Active = true;
				this.activePage = this.initialActivePage;
				this.helpButton.Enabled = this.pageSites[this.activePage].GetPageControl().SupportsHelp();
			}
		}

		// Token: 0x06004DE9 RID: 19945 RVA: 0x00142295 File Offset: 0x00140495
		protected override void OnHelpRequested(HelpEventArgs e)
		{
			base.OnHelpRequested(e);
			this.ShowPageHelp();
		}

		// Token: 0x06004DEA RID: 19946 RVA: 0x001422A4 File Offset: 0x001404A4
		private void OnNewObjects()
		{
			this.pageSites = null;
			this.maxSize = new Size(258, 24 * this.pageTypes.Length);
			this.pageSites = new ComponentEditorForm.ComponentEditorPageSite[this.pageTypes.Length];
			for (int i = 0; i < this.pageTypes.Length; i++)
			{
				this.pageSites[i] = new ComponentEditorForm.ComponentEditorPageSite(this.pageHost, this.pageTypes[i], this.component, this);
				ComponentEditorPage pageControl = this.pageSites[i].GetPageControl();
				Size size = pageControl.Size;
				if (size.Width > this.maxSize.Width)
				{
					this.maxSize.Width = size.Width;
				}
				if (size.Height > this.maxSize.Height)
				{
					this.maxSize.Height = size.Height;
				}
			}
			for (int j = 0; j < this.pageSites.Length; j++)
			{
				this.pageSites[j].GetPageControl().Size = this.maxSize;
			}
		}

		// Token: 0x06004DEB RID: 19947 RVA: 0x001423B0 File Offset: 0x001405B0
		protected virtual void OnSelChangeSelector(object source, TreeViewEventArgs e)
		{
			if (this.firstActivate)
			{
				return;
			}
			int index = this.selector.SelectedNode.Index;
			if (index == this.activePage)
			{
				return;
			}
			if (this.activePage != -1)
			{
				if (this.pageSites[this.activePage].AutoCommit)
				{
					this.ApplyChanges(false);
				}
				this.pageSites[this.activePage].Active = false;
			}
			this.activePage = index;
			this.pageSites[this.activePage].Active = true;
			this.helpButton.Enabled = this.pageSites[this.activePage].GetPageControl().SupportsHelp();
		}

		// Token: 0x06004DEC RID: 19948 RVA: 0x00142454 File Offset: 0x00140654
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		public override bool PreProcessMessage(ref Message msg)
		{
			return (this.pageSites != null && this.pageSites[this.activePage].GetPageControl().IsPageMessage(ref msg)) || base.PreProcessMessage(ref msg);
		}

		// Token: 0x06004DED RID: 19949 RVA: 0x00142481 File Offset: 0x00140681
		internal virtual void SetDirty()
		{
			this.dirty = true;
			this.applyButton.Enabled = true;
			this.cancelButton.Text = SR.GetString("CancelCaption");
		}

		// Token: 0x06004DEE RID: 19950 RVA: 0x001424AB File Offset: 0x001406AB
		public virtual DialogResult ShowForm()
		{
			return this.ShowForm(null, 0);
		}

		// Token: 0x06004DEF RID: 19951 RVA: 0x001424B5 File Offset: 0x001406B5
		public virtual DialogResult ShowForm(int page)
		{
			return this.ShowForm(null, page);
		}

		// Token: 0x06004DF0 RID: 19952 RVA: 0x001424BF File Offset: 0x001406BF
		public virtual DialogResult ShowForm(IWin32Window owner)
		{
			return this.ShowForm(owner, 0);
		}

		// Token: 0x06004DF1 RID: 19953 RVA: 0x001424C9 File Offset: 0x001406C9
		public virtual DialogResult ShowForm(IWin32Window owner, int page)
		{
			this.initialActivePage = page;
			base.ShowDialog(owner);
			return base.DialogResult;
		}

		// Token: 0x06004DF2 RID: 19954 RVA: 0x001424E0 File Offset: 0x001406E0
		private void ShowPageHelp()
		{
			if (this.pageSites[this.activePage].GetPageControl().SupportsHelp())
			{
				this.pageSites[this.activePage].GetPageControl().ShowHelp();
			}
		}

		// Token: 0x040033DB RID: 13275
		private IComponent component;

		// Token: 0x040033DC RID: 13276
		private Type[] pageTypes;

		// Token: 0x040033DD RID: 13277
		private ComponentEditorForm.ComponentEditorPageSite[] pageSites;

		// Token: 0x040033DE RID: 13278
		private Size maxSize = Size.Empty;

		// Token: 0x040033DF RID: 13279
		private int initialActivePage;

		// Token: 0x040033E0 RID: 13280
		private int activePage;

		// Token: 0x040033E1 RID: 13281
		private bool dirty;

		// Token: 0x040033E2 RID: 13282
		private bool firstActivate;

		// Token: 0x040033E3 RID: 13283
		private Panel pageHost = new Panel();

		// Token: 0x040033E4 RID: 13284
		private ComponentEditorForm.PageSelector selector;

		// Token: 0x040033E5 RID: 13285
		private ImageList selectorImageList;

		// Token: 0x040033E6 RID: 13286
		private Button okButton;

		// Token: 0x040033E7 RID: 13287
		private Button cancelButton;

		// Token: 0x040033E8 RID: 13288
		private Button applyButton;

		// Token: 0x040033E9 RID: 13289
		private Button helpButton;

		// Token: 0x040033EA RID: 13290
		private const int BUTTON_WIDTH = 80;

		// Token: 0x040033EB RID: 13291
		private const int BUTTON_HEIGHT = 23;

		// Token: 0x040033EC RID: 13292
		private const int BUTTON_PAD = 6;

		// Token: 0x040033ED RID: 13293
		private const int MIN_SELECTOR_WIDTH = 90;

		// Token: 0x040033EE RID: 13294
		private const int SELECTOR_PADDING = 10;

		// Token: 0x040033EF RID: 13295
		private const int STRIP_HEIGHT = 4;

		// Token: 0x0200084F RID: 2127
		private sealed class ComponentEditorPageSite : IComponentEditorPageSite
		{
			// Token: 0x06007082 RID: 28802 RVA: 0x0019C4F4 File Offset: 0x0019A6F4
			internal ComponentEditorPageSite(Control parent, Type pageClass, IComponent component, ComponentEditorForm form)
			{
				this.component = component;
				this.parent = parent;
				this.isActive = false;
				this.isDirty = false;
				if (form == null)
				{
					throw new ArgumentNullException("form");
				}
				this.form = form;
				try
				{
					this.pageControl = (ComponentEditorPage)SecurityUtils.SecureCreateInstance(pageClass);
				}
				catch (TargetInvocationException ex)
				{
					throw new TargetInvocationException(SR.GetString("ExceptionCreatingCompEditorControl", new object[]
					{
						ex.ToString()
					}), ex.InnerException);
				}
				this.pageControl.SetSite(this);
				this.pageControl.SetComponent(component);
			}

			// Token: 0x17001884 RID: 6276
			// (set) Token: 0x06007083 RID: 28803 RVA: 0x0019C59C File Offset: 0x0019A79C
			internal bool Active
			{
				set
				{
					if (value)
					{
						this.pageControl.CreateControl();
						this.pageControl.Activate();
					}
					else
					{
						this.pageControl.Deactivate();
					}
					this.isActive = value;
				}
			}

			// Token: 0x17001885 RID: 6277
			// (get) Token: 0x06007084 RID: 28804 RVA: 0x0019C5CB File Offset: 0x0019A7CB
			internal bool AutoCommit
			{
				get
				{
					return this.pageControl.CommitOnDeactivate;
				}
			}

			// Token: 0x17001886 RID: 6278
			// (get) Token: 0x06007085 RID: 28805 RVA: 0x0019C5D8 File Offset: 0x0019A7D8
			// (set) Token: 0x06007086 RID: 28806 RVA: 0x0019C5E0 File Offset: 0x0019A7E0
			internal bool Dirty
			{
				get
				{
					return this.isDirty;
				}
				set
				{
					this.isDirty = value;
				}
			}

			// Token: 0x06007087 RID: 28807 RVA: 0x0019C5E9 File Offset: 0x0019A7E9
			public Control GetControl()
			{
				return this.parent;
			}

			// Token: 0x06007088 RID: 28808 RVA: 0x0019C5F1 File Offset: 0x0019A7F1
			internal ComponentEditorPage GetPageControl()
			{
				return this.pageControl;
			}

			// Token: 0x06007089 RID: 28809 RVA: 0x0019C5F9 File Offset: 0x0019A7F9
			public void SetDirty()
			{
				if (this.isActive)
				{
					this.Dirty = true;
				}
				this.form.SetDirty();
			}

			// Token: 0x04004380 RID: 17280
			internal IComponent component;

			// Token: 0x04004381 RID: 17281
			internal ComponentEditorPage pageControl;

			// Token: 0x04004382 RID: 17282
			internal Control parent;

			// Token: 0x04004383 RID: 17283
			internal bool isActive;

			// Token: 0x04004384 RID: 17284
			internal bool isDirty;

			// Token: 0x04004385 RID: 17285
			private ComponentEditorForm form;
		}

		// Token: 0x02000850 RID: 2128
		internal sealed class PageSelector : TreeView
		{
			// Token: 0x0600708A RID: 28810 RVA: 0x0019C618 File Offset: 0x0019A818
			public PageSelector()
			{
				base.HotTracking = true;
				base.HideSelection = false;
				this.BackColor = SystemColors.Control;
				base.Indent = 0;
				base.LabelEdit = false;
				base.Scrollable = false;
				base.ShowLines = false;
				base.ShowPlusMinus = false;
				base.ShowRootLines = false;
				base.BorderStyle = BorderStyle.None;
				base.Indent = 0;
				base.FullRowSelect = true;
			}

			// Token: 0x17001887 RID: 6279
			// (get) Token: 0x0600708B RID: 28811 RVA: 0x0019C684 File Offset: 0x0019A884
			protected override CreateParams CreateParams
			{
				[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
				get
				{
					CreateParams createParams = base.CreateParams;
					createParams.ExStyle |= 131072;
					return createParams;
				}
			}

			// Token: 0x0600708C RID: 28812 RVA: 0x0019C6AC File Offset: 0x0019A8AC
			private void CreateDitherBrush()
			{
				short[] lpvBits = new short[]
				{
					-21846,
					21845,
					-21846,
					21845,
					-21846,
					21845,
					-21846,
					21845
				};
				IntPtr intPtr = SafeNativeMethods.CreateBitmap(8, 8, 1, 1, lpvBits);
				if (intPtr != IntPtr.Zero)
				{
					this.hbrushDither = SafeNativeMethods.CreatePatternBrush(new HandleRef(null, intPtr));
					SafeNativeMethods.DeleteObject(new HandleRef(null, intPtr));
				}
			}

			// Token: 0x0600708D RID: 28813 RVA: 0x0019C704 File Offset: 0x0019A904
			private void DrawTreeItem(string itemText, int imageIndex, IntPtr dc, NativeMethods.RECT rcIn, int state, int backColor, int textColor)
			{
				IntNativeMethods.SIZE size = new IntNativeMethods.SIZE();
				IntNativeMethods.RECT rect = default(IntNativeMethods.RECT);
				IntNativeMethods.RECT rect2 = new IntNativeMethods.RECT(rcIn.left, rcIn.top, rcIn.right, rcIn.bottom);
				ImageList imageList = base.ImageList;
				IntPtr intPtr = IntPtr.Zero;
				if ((state & 2) != 0)
				{
					intPtr = SafeNativeMethods.SelectObject(new HandleRef(null, dc), new HandleRef(base.Parent, base.Parent.FontHandle));
				}
				if ((state & 1) != 0 && this.hbrushDither != IntPtr.Zero)
				{
					this.FillRectDither(dc, rcIn);
					SafeNativeMethods.SetBkMode(new HandleRef(null, dc), 1);
				}
				else
				{
					SafeNativeMethods.SetBkColor(new HandleRef(null, dc), backColor);
					IntUnsafeNativeMethods.ExtTextOut(new HandleRef(null, dc), 0, 0, 6, ref rect2, null, 0, null);
				}
				IntUnsafeNativeMethods.GetTextExtentPoint32(new HandleRef(null, dc), itemText, size);
				rect.left = rect2.left + 16 + 8;
				rect.top = rect2.top + (rect2.bottom - rect2.top - size.cy >> 1);
				rect.bottom = rect.top + size.cy;
				rect.right = rect2.right;
				SafeNativeMethods.SetTextColor(new HandleRef(null, dc), textColor);
				IntUnsafeNativeMethods.DrawText(new HandleRef(null, dc), itemText, ref rect, 34820);
				SafeNativeMethods.ImageList_Draw(new HandleRef(imageList, imageList.Handle), imageIndex, new HandleRef(null, dc), 4, rect2.top + (rect2.bottom - rect2.top - 16 >> 1), 1);
				if ((state & 2) != 0)
				{
					int clr = SafeNativeMethods.SetBkColor(new HandleRef(null, dc), ColorTranslator.ToWin32(SystemColors.ControlLightLight));
					rect.left = rect2.left;
					rect.top = rect2.top;
					rect.bottom = rect2.top + 1;
					rect.right = rect2.right;
					IntUnsafeNativeMethods.ExtTextOut(new HandleRef(null, dc), 0, 0, 2, ref rect, null, 0, null);
					rect.bottom = rect2.bottom;
					rect.right = rect2.left + 1;
					IntUnsafeNativeMethods.ExtTextOut(new HandleRef(null, dc), 0, 0, 2, ref rect, null, 0, null);
					SafeNativeMethods.SetBkColor(new HandleRef(null, dc), ColorTranslator.ToWin32(SystemColors.ControlDark));
					rect.left = rect2.left;
					rect.right = rect2.right;
					rect.top = rect2.bottom - 1;
					rect.bottom = rect2.bottom;
					IntUnsafeNativeMethods.ExtTextOut(new HandleRef(null, dc), 0, 0, 2, ref rect, null, 0, null);
					rect.left = rect2.right - 1;
					rect.top = rect2.top;
					IntUnsafeNativeMethods.ExtTextOut(new HandleRef(null, dc), 0, 0, 2, ref rect, null, 0, null);
					SafeNativeMethods.SetBkColor(new HandleRef(null, dc), clr);
				}
				if (intPtr != IntPtr.Zero)
				{
					SafeNativeMethods.SelectObject(new HandleRef(null, dc), new HandleRef(null, intPtr));
				}
			}

			// Token: 0x0600708E RID: 28814 RVA: 0x0019C9F0 File Offset: 0x0019ABF0
			protected override void OnHandleCreated(EventArgs e)
			{
				base.OnHandleCreated(e);
				int num = (int)UnsafeNativeMethods.SendMessage(new HandleRef(this, base.Handle), 4380, 0, 0);
				num += 6;
				UnsafeNativeMethods.SendMessage(new HandleRef(this, base.Handle), 4379, num, 0);
				if (this.hbrushDither == IntPtr.Zero)
				{
					this.CreateDitherBrush();
				}
			}

			// Token: 0x0600708F RID: 28815 RVA: 0x0019CA58 File Offset: 0x0019AC58
			private void OnCustomDraw(ref Message m)
			{
				NativeMethods.NMTVCUSTOMDRAW nmtvcustomdraw = (NativeMethods.NMTVCUSTOMDRAW)m.GetLParam(typeof(NativeMethods.NMTVCUSTOMDRAW));
				int dwDrawStage = nmtvcustomdraw.nmcd.dwDrawStage;
				if (dwDrawStage == 1)
				{
					m.Result = (IntPtr)48;
					return;
				}
				if (dwDrawStage == 2)
				{
					m.Result = (IntPtr)4;
					return;
				}
				if (dwDrawStage != 65537)
				{
					m.Result = (IntPtr)0;
					return;
				}
				TreeNode treeNode = TreeNode.FromHandle(this, nmtvcustomdraw.nmcd.dwItemSpec);
				if (treeNode != null)
				{
					int num = 0;
					int uItemState = nmtvcustomdraw.nmcd.uItemState;
					if ((uItemState & 64) != 0 || (uItemState & 16) != 0)
					{
						num |= 2;
					}
					if ((uItemState & 1) != 0)
					{
						num |= 1;
					}
					this.DrawTreeItem(treeNode.Text, treeNode.ImageIndex, nmtvcustomdraw.nmcd.hdc, nmtvcustomdraw.nmcd.rc, num, ColorTranslator.ToWin32(SystemColors.Control), ColorTranslator.ToWin32(SystemColors.ControlText));
				}
				m.Result = (IntPtr)4;
			}

			// Token: 0x06007090 RID: 28816 RVA: 0x0019CB50 File Offset: 0x0019AD50
			protected override void OnHandleDestroyed(EventArgs e)
			{
				base.OnHandleDestroyed(e);
				if (!base.RecreatingHandle && this.hbrushDither != IntPtr.Zero)
				{
					SafeNativeMethods.DeleteObject(new HandleRef(this, this.hbrushDither));
					this.hbrushDither = IntPtr.Zero;
				}
			}

			// Token: 0x06007091 RID: 28817 RVA: 0x0019CB90 File Offset: 0x0019AD90
			private void FillRectDither(IntPtr dc, NativeMethods.RECT rc)
			{
				IntPtr value = SafeNativeMethods.SelectObject(new HandleRef(null, dc), new HandleRef(this, this.hbrushDither));
				if (value != IntPtr.Zero)
				{
					int crColor = SafeNativeMethods.SetTextColor(new HandleRef(null, dc), ColorTranslator.ToWin32(SystemColors.ControlLightLight));
					int clr = SafeNativeMethods.SetBkColor(new HandleRef(null, dc), ColorTranslator.ToWin32(SystemColors.Control));
					SafeNativeMethods.PatBlt(new HandleRef(null, dc), rc.left, rc.top, rc.right - rc.left, rc.bottom - rc.top, 15728673);
					SafeNativeMethods.SetTextColor(new HandleRef(null, dc), crColor);
					SafeNativeMethods.SetBkColor(new HandleRef(null, dc), clr);
				}
			}

			// Token: 0x06007092 RID: 28818 RVA: 0x0019CC48 File Offset: 0x0019AE48
			[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
			protected override void WndProc(ref Message m)
			{
				if (m.Msg == 8270)
				{
					NativeMethods.NMHDR nmhdr = (NativeMethods.NMHDR)m.GetLParam(typeof(NativeMethods.NMHDR));
					if (nmhdr.code == -12)
					{
						this.OnCustomDraw(ref m);
						return;
					}
				}
				base.WndProc(ref m);
			}

			// Token: 0x04004386 RID: 17286
			private const int PADDING_VERT = 3;

			// Token: 0x04004387 RID: 17287
			private const int PADDING_HORZ = 4;

			// Token: 0x04004388 RID: 17288
			private const int SIZE_ICON_X = 16;

			// Token: 0x04004389 RID: 17289
			private const int SIZE_ICON_Y = 16;

			// Token: 0x0400438A RID: 17290
			private const int STATE_NORMAL = 0;

			// Token: 0x0400438B RID: 17291
			private const int STATE_SELECTED = 1;

			// Token: 0x0400438C RID: 17292
			private const int STATE_HOT = 2;

			// Token: 0x0400438D RID: 17293
			private IntPtr hbrushDither;
		}
	}
}
