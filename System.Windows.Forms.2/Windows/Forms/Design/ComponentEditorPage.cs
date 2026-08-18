using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace System.Windows.Forms.Design
{
	// Token: 0x02000489 RID: 1161
	[ComVisible(true)]
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
	public abstract class ComponentEditorPage : Panel
	{
		// Token: 0x06004DF3 RID: 19955 RVA: 0x00142512 File Offset: 0x00140712
		public ComponentEditorPage()
		{
			this.commitOnDeactivate = false;
			this.firstActivate = true;
			this.loadRequired = false;
			this.loading = 0;
			base.Visible = false;
		}

		// Token: 0x1700132B RID: 4907
		// (get) Token: 0x06004DF4 RID: 19956 RVA: 0x000FFEE1 File Offset: 0x000FE0E1
		// (set) Token: 0x06004DF5 RID: 19957 RVA: 0x000FFEE9 File Offset: 0x000FE0E9
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

		// Token: 0x1400040A RID: 1034
		// (add) Token: 0x06004DF6 RID: 19958 RVA: 0x000FFEF2 File Offset: 0x000FE0F2
		// (remove) Token: 0x06004DF7 RID: 19959 RVA: 0x000FFEFB File Offset: 0x000FE0FB
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

		// Token: 0x1700132C RID: 4908
		// (get) Token: 0x06004DF8 RID: 19960 RVA: 0x0014253D File Offset: 0x0014073D
		// (set) Token: 0x06004DF9 RID: 19961 RVA: 0x00142545 File Offset: 0x00140745
		protected IComponentEditorPageSite PageSite
		{
			get
			{
				return this.pageSite;
			}
			set
			{
				this.pageSite = value;
			}
		}

		// Token: 0x1700132D RID: 4909
		// (get) Token: 0x06004DFA RID: 19962 RVA: 0x0014254E File Offset: 0x0014074E
		// (set) Token: 0x06004DFB RID: 19963 RVA: 0x00142556 File Offset: 0x00140756
		protected IComponent Component
		{
			get
			{
				return this.component;
			}
			set
			{
				this.component = value;
			}
		}

		// Token: 0x1700132E RID: 4910
		// (get) Token: 0x06004DFC RID: 19964 RVA: 0x0014255F File Offset: 0x0014075F
		// (set) Token: 0x06004DFD RID: 19965 RVA: 0x00142567 File Offset: 0x00140767
		protected bool FirstActivate
		{
			get
			{
				return this.firstActivate;
			}
			set
			{
				this.firstActivate = value;
			}
		}

		// Token: 0x1700132F RID: 4911
		// (get) Token: 0x06004DFE RID: 19966 RVA: 0x00142570 File Offset: 0x00140770
		// (set) Token: 0x06004DFF RID: 19967 RVA: 0x00142578 File Offset: 0x00140778
		protected bool LoadRequired
		{
			get
			{
				return this.loadRequired;
			}
			set
			{
				this.loadRequired = value;
			}
		}

		// Token: 0x17001330 RID: 4912
		// (get) Token: 0x06004E00 RID: 19968 RVA: 0x00142581 File Offset: 0x00140781
		// (set) Token: 0x06004E01 RID: 19969 RVA: 0x00142589 File Offset: 0x00140789
		protected int Loading
		{
			get
			{
				return this.loading;
			}
			set
			{
				this.loading = value;
			}
		}

		// Token: 0x17001331 RID: 4913
		// (get) Token: 0x06004E02 RID: 19970 RVA: 0x00142592 File Offset: 0x00140792
		// (set) Token: 0x06004E03 RID: 19971 RVA: 0x0014259A File Offset: 0x0014079A
		public bool CommitOnDeactivate
		{
			get
			{
				return this.commitOnDeactivate;
			}
			set
			{
				this.commitOnDeactivate = value;
			}
		}

		// Token: 0x17001332 RID: 4914
		// (get) Token: 0x06004E04 RID: 19972 RVA: 0x001425A4 File Offset: 0x001407A4
		protected override CreateParams CreateParams
		{
			[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
			get
			{
				CreateParams createParams = base.CreateParams;
				createParams.Style &= -12582913;
				return createParams;
			}
		}

		// Token: 0x17001333 RID: 4915
		// (get) Token: 0x06004E05 RID: 19973 RVA: 0x001425CB File Offset: 0x001407CB
		// (set) Token: 0x06004E06 RID: 19974 RVA: 0x001425F5 File Offset: 0x001407F5
		public Icon Icon
		{
			get
			{
				if (this.icon == null)
				{
					this.icon = new Icon(typeof(ComponentEditorPage), "ComponentEditorPage.ico");
				}
				return this.icon;
			}
			set
			{
				this.icon = value;
			}
		}

		// Token: 0x17001334 RID: 4916
		// (get) Token: 0x06004E07 RID: 19975 RVA: 0x00107424 File Offset: 0x00105624
		public virtual string Title
		{
			get
			{
				return base.Text;
			}
		}

		// Token: 0x06004E08 RID: 19976 RVA: 0x001425FE File Offset: 0x001407FE
		public virtual void Activate()
		{
			if (this.loadRequired)
			{
				this.EnterLoadingMode();
				this.LoadComponent();
				this.ExitLoadingMode();
				this.loadRequired = false;
			}
			base.Visible = true;
			this.firstActivate = false;
		}

		// Token: 0x06004E09 RID: 19977 RVA: 0x0014262F File Offset: 0x0014082F
		public virtual void ApplyChanges()
		{
			this.SaveComponent();
		}

		// Token: 0x06004E0A RID: 19978 RVA: 0x00034605 File Offset: 0x00032805
		public virtual void Deactivate()
		{
			base.Visible = false;
		}

		// Token: 0x06004E0B RID: 19979 RVA: 0x00142637 File Offset: 0x00140837
		protected void EnterLoadingMode()
		{
			this.loading++;
		}

		// Token: 0x06004E0C RID: 19980 RVA: 0x00142647 File Offset: 0x00140847
		protected void ExitLoadingMode()
		{
			this.loading--;
		}

		// Token: 0x06004E0D RID: 19981 RVA: 0x00006C59 File Offset: 0x00004E59
		public virtual Control GetControl()
		{
			return this;
		}

		// Token: 0x06004E0E RID: 19982 RVA: 0x0014254E File Offset: 0x0014074E
		protected IComponent GetSelectedComponent()
		{
			return this.component;
		}

		// Token: 0x06004E0F RID: 19983 RVA: 0x00142657 File Offset: 0x00140857
		public virtual bool IsPageMessage(ref Message msg)
		{
			return this.PreProcessMessage(ref msg);
		}

		// Token: 0x06004E10 RID: 19984 RVA: 0x0014255F File Offset: 0x0014075F
		protected bool IsFirstActivate()
		{
			return this.firstActivate;
		}

		// Token: 0x06004E11 RID: 19985 RVA: 0x00142660 File Offset: 0x00140860
		protected bool IsLoading()
		{
			return this.loading != 0;
		}

		// Token: 0x06004E12 RID: 19986
		protected abstract void LoadComponent();

		// Token: 0x06004E13 RID: 19987 RVA: 0x0014266B File Offset: 0x0014086B
		public virtual void OnApplyComplete()
		{
			this.ReloadComponent();
		}

		// Token: 0x06004E14 RID: 19988 RVA: 0x00142673 File Offset: 0x00140873
		protected virtual void ReloadComponent()
		{
			if (!base.Visible)
			{
				this.loadRequired = true;
			}
		}

		// Token: 0x06004E15 RID: 19989
		protected abstract void SaveComponent();

		// Token: 0x06004E16 RID: 19990 RVA: 0x00142684 File Offset: 0x00140884
		protected virtual void SetDirty()
		{
			if (!this.IsLoading())
			{
				this.pageSite.SetDirty();
			}
		}

		// Token: 0x06004E17 RID: 19991 RVA: 0x00142699 File Offset: 0x00140899
		public virtual void SetComponent(IComponent component)
		{
			this.component = component;
			this.loadRequired = true;
		}

		// Token: 0x06004E18 RID: 19992 RVA: 0x001426A9 File Offset: 0x001408A9
		public virtual void SetSite(IComponentEditorPageSite site)
		{
			this.pageSite = site;
			this.pageSite.GetControl().Controls.Add(this);
		}

		// Token: 0x06004E19 RID: 19993 RVA: 0x000072B6 File Offset: 0x000054B6
		public virtual void ShowHelp()
		{
		}

		// Token: 0x06004E1A RID: 19994 RVA: 0x00011A20 File Offset: 0x0000FC20
		public virtual bool SupportsHelp()
		{
			return false;
		}

		// Token: 0x040033F0 RID: 13296
		private IComponentEditorPageSite pageSite;

		// Token: 0x040033F1 RID: 13297
		private IComponent component;

		// Token: 0x040033F2 RID: 13298
		private bool firstActivate;

		// Token: 0x040033F3 RID: 13299
		private bool loadRequired;

		// Token: 0x040033F4 RID: 13300
		private int loading;

		// Token: 0x040033F5 RID: 13301
		private Icon icon;

		// Token: 0x040033F6 RID: 13302
		private bool commitOnDeactivate;
	}
}
