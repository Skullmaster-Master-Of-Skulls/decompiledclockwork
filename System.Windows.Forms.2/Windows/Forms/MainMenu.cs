using System;
using System.ComponentModel;
using System.Security.Permissions;

namespace System.Windows.Forms
{
	// Token: 0x020002E6 RID: 742
	[ToolboxItemFilter("System.Windows.Forms.MainMenu")]
	public class MainMenu : Menu
	{
		// Token: 0x06002EC0 RID: 11968 RVA: 0x000D353C File Offset: 0x000D173C
		public MainMenu() : base(null)
		{
		}

		// Token: 0x06002EC1 RID: 11969 RVA: 0x000D354C File Offset: 0x000D174C
		public MainMenu(IContainer container) : this()
		{
			if (container == null)
			{
				throw new ArgumentNullException("container");
			}
			container.Add(this);
		}

		// Token: 0x06002EC2 RID: 11970 RVA: 0x000D3569 File Offset: 0x000D1769
		public MainMenu(MenuItem[] items) : base(items)
		{
		}

		// Token: 0x14000221 RID: 545
		// (add) Token: 0x06002EC3 RID: 11971 RVA: 0x000D3579 File Offset: 0x000D1779
		// (remove) Token: 0x06002EC4 RID: 11972 RVA: 0x000D3592 File Offset: 0x000D1792
		[SRDescription("MainMenuCollapseDescr")]
		public event EventHandler Collapse
		{
			add
			{
				this.onCollapse = (EventHandler)Delegate.Combine(this.onCollapse, value);
			}
			remove
			{
				this.onCollapse = (EventHandler)Delegate.Remove(this.onCollapse, value);
			}
		}

		// Token: 0x17000AFE RID: 2814
		// (get) Token: 0x06002EC5 RID: 11973 RVA: 0x000D35AB File Offset: 0x000D17AB
		// (set) Token: 0x06002EC6 RID: 11974 RVA: 0x000D35D4 File Offset: 0x000D17D4
		[Localizable(true)]
		[AmbientValue(RightToLeft.Inherit)]
		[SRDescription("MenuRightToLeftDescr")]
		public virtual RightToLeft RightToLeft
		{
			get
			{
				if (RightToLeft.Inherit != this.rightToLeft)
				{
					return this.rightToLeft;
				}
				if (this.form != null)
				{
					return this.form.RightToLeft;
				}
				return RightToLeft.Inherit;
			}
			set
			{
				if (!ClientUtils.IsEnumValid(value, (int)value, 0, 2))
				{
					throw new InvalidEnumArgumentException("RightToLeft", (int)value, typeof(RightToLeft));
				}
				if (this.rightToLeft != value)
				{
					this.rightToLeft = value;
					base.UpdateRtl(value == RightToLeft.Yes);
				}
			}
		}

		// Token: 0x17000AFF RID: 2815
		// (get) Token: 0x06002EC7 RID: 11975 RVA: 0x000D3621 File Offset: 0x000D1821
		internal override bool RenderIsRightToLeft
		{
			get
			{
				return this.RightToLeft == RightToLeft.Yes && (this.form == null || !this.form.IsMirrored);
			}
		}

		// Token: 0x06002EC8 RID: 11976 RVA: 0x000D3648 File Offset: 0x000D1848
		public virtual MainMenu CloneMenu()
		{
			MainMenu mainMenu = new MainMenu();
			mainMenu.CloneMenu(this);
			return mainMenu;
		}

		// Token: 0x06002EC9 RID: 11977 RVA: 0x000D3663 File Offset: 0x000D1863
		protected override IntPtr CreateMenuHandle()
		{
			return UnsafeNativeMethods.CreateMenu();
		}

		// Token: 0x06002ECA RID: 11978 RVA: 0x000D366A File Offset: 0x000D186A
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.form != null && (this.ownerForm == null || this.form == this.ownerForm))
			{
				this.form.Menu = null;
			}
			base.Dispose(disposing);
		}

		// Token: 0x06002ECB RID: 11979 RVA: 0x000D36A0 File Offset: 0x000D18A0
		[UIPermission(SecurityAction.Demand, Window = UIPermissionWindow.AllWindows)]
		public Form GetForm()
		{
			return this.form;
		}

		// Token: 0x06002ECC RID: 11980 RVA: 0x000D36A0 File Offset: 0x000D18A0
		internal Form GetFormUnsafe()
		{
			return this.form;
		}

		// Token: 0x06002ECD RID: 11981 RVA: 0x000D36A8 File Offset: 0x000D18A8
		internal override void ItemsChanged(int change)
		{
			base.ItemsChanged(change);
			if (this.form != null)
			{
				this.form.MenuChanged(change, this);
			}
		}

		// Token: 0x06002ECE RID: 11982 RVA: 0x000D36C6 File Offset: 0x000D18C6
		internal virtual void ItemsChanged(int change, Menu menu)
		{
			if (this.form != null)
			{
				this.form.MenuChanged(change, menu);
			}
		}

		// Token: 0x06002ECF RID: 11983 RVA: 0x000D36DD File Offset: 0x000D18DD
		protected internal virtual void OnCollapse(EventArgs e)
		{
			if (this.onCollapse != null)
			{
				this.onCollapse(this, e);
			}
		}

		// Token: 0x06002ED0 RID: 11984 RVA: 0x000D36F4 File Offset: 0x000D18F4
		internal virtual bool ShouldSerializeRightToLeft()
		{
			return RightToLeft.Inherit != this.RightToLeft;
		}

		// Token: 0x06002ED1 RID: 11985 RVA: 0x000D3702 File Offset: 0x000D1902
		public override string ToString()
		{
			return base.ToString();
		}

		// Token: 0x0400136D RID: 4973
		internal Form form;

		// Token: 0x0400136E RID: 4974
		internal Form ownerForm;

		// Token: 0x0400136F RID: 4975
		private RightToLeft rightToLeft = RightToLeft.Inherit;

		// Token: 0x04001370 RID: 4976
		private EventHandler onCollapse;
	}
}
