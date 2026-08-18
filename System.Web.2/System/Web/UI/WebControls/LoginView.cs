using System;
using System.Collections;
using System.ComponentModel;
using System.Security.Permissions;
using System.Security.Principal;

namespace System.Web.UI.WebControls
{
	// Token: 0x02000466 RID: 1126
	[Bindable(false)]
	[ParseChildren(true)]
	[PersistChildren(false)]
	[Designer("System.Web.UI.Design.WebControls.LoginViewDesigner,System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[DefaultProperty("CurrentView")]
	[DefaultEvent("ViewChanged")]
	[Themeable(true)]
	public class LoginView : Control, INamingContainer
	{
		// Token: 0x17000FEA RID: 4074
		// (get) Token: 0x060036B4 RID: 14004 RVA: 0x000B11C7 File Offset: 0x000AF3C7
		// (set) Token: 0x060036B5 RID: 14005 RVA: 0x000B11CF File Offset: 0x000AF3CF
		[Browsable(false)]
		[DefaultValue(null)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[TemplateContainer(typeof(LoginView))]
		public virtual ITemplate AnonymousTemplate
		{
			get
			{
				return this._anonymousTemplate;
			}
			set
			{
				this._anonymousTemplate = value;
			}
		}

		// Token: 0x17000FEB RID: 4075
		// (get) Token: 0x060036B6 RID: 14006 RVA: 0x00075E05 File Offset: 0x00074005
		// (set) Token: 0x060036B7 RID: 14007 RVA: 0x00075E0D File Offset: 0x0007400D
		[Browsable(true)]
		public override bool EnableTheming
		{
			get
			{
				return base.EnableTheming;
			}
			set
			{
				base.EnableTheming = value;
			}
		}

		// Token: 0x17000FEC RID: 4076
		// (get) Token: 0x060036B8 RID: 14008 RVA: 0x000B11D8 File Offset: 0x000AF3D8
		// (set) Token: 0x060036B9 RID: 14009 RVA: 0x000B11E0 File Offset: 0x000AF3E0
		[Browsable(true)]
		public override string SkinID
		{
			get
			{
				return base.SkinID;
			}
			set
			{
				base.SkinID = value;
			}
		}

		// Token: 0x17000FED RID: 4077
		// (get) Token: 0x060036BA RID: 14010 RVA: 0x000856CA File Offset: 0x000838CA
		public override ControlCollection Controls
		{
			get
			{
				this.EnsureChildControls();
				return base.Controls;
			}
		}

		// Token: 0x060036BB RID: 14011 RVA: 0x000906F4 File Offset: 0x0008E8F4
		public override void DataBind()
		{
			this.OnDataBinding(EventArgs.Empty);
			this.EnsureChildControls();
			this.DataBindChildren();
		}

		// Token: 0x17000FEE RID: 4078
		// (get) Token: 0x060036BC RID: 14012 RVA: 0x000B11E9 File Offset: 0x000AF3E9
		// (set) Token: 0x060036BD RID: 14013 RVA: 0x000B11F1 File Offset: 0x000AF3F1
		[Browsable(false)]
		[DefaultValue(null)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[TemplateContainer(typeof(LoginView))]
		public virtual ITemplate LoggedInTemplate
		{
			get
			{
				return this._loggedInTemplate;
			}
			set
			{
				this._loggedInTemplate = value;
			}
		}

		// Token: 0x17000FEF RID: 4079
		// (get) Token: 0x060036BE RID: 14014 RVA: 0x000B11FA File Offset: 0x000AF3FA
		[WebCategory("Behavior")]
		[MergableProperty(false)]
		[Themeable(false)]
		[Filterable(false)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[WebSysDescription("LoginView_RoleGroups")]
		public virtual RoleGroupCollection RoleGroups
		{
			get
			{
				if (this._roleGroups == null)
				{
					this._roleGroups = new RoleGroupCollection();
				}
				return this._roleGroups;
			}
		}

		// Token: 0x17000FF0 RID: 4080
		// (get) Token: 0x060036BF RID: 14015 RVA: 0x000B1215 File Offset: 0x000AF415
		// (set) Token: 0x060036C0 RID: 14016 RVA: 0x000B121D File Offset: 0x000AF41D
		private int TemplateIndex
		{
			get
			{
				return this._templateIndex;
			}
			set
			{
				if (value != this.TemplateIndex)
				{
					this.OnViewChanging(EventArgs.Empty);
					this._templateIndex = value;
					base.ChildControlsCreated = false;
					this.OnViewChanged(EventArgs.Empty);
				}
			}
		}

		// Token: 0x140000B8 RID: 184
		// (add) Token: 0x060036C1 RID: 14017 RVA: 0x000B124C File Offset: 0x000AF44C
		// (remove) Token: 0x060036C2 RID: 14018 RVA: 0x000B125F File Offset: 0x000AF45F
		[WebCategory("Action")]
		[WebSysDescription("LoginView_ViewChanged")]
		public event EventHandler ViewChanged
		{
			add
			{
				base.Events.AddHandler(LoginView.EventViewChanged, value);
			}
			remove
			{
				base.Events.RemoveHandler(LoginView.EventViewChanged, value);
			}
		}

		// Token: 0x140000B9 RID: 185
		// (add) Token: 0x060036C3 RID: 14019 RVA: 0x000B1272 File Offset: 0x000AF472
		// (remove) Token: 0x060036C4 RID: 14020 RVA: 0x000B1285 File Offset: 0x000AF485
		[WebCategory("Action")]
		[WebSysDescription("LoginView_ViewChanging")]
		public event EventHandler ViewChanging
		{
			add
			{
				base.Events.AddHandler(LoginView.EventViewChanging, value);
			}
			remove
			{
				base.Events.RemoveHandler(LoginView.EventViewChanging, value);
			}
		}

		// Token: 0x060036C5 RID: 14021 RVA: 0x000B1298 File Offset: 0x000AF498
		protected internal override void CreateChildControls()
		{
			this.Controls.Clear();
			Page page = this.Page;
			if (page != null && !page.IsPostBack && !base.DesignMode)
			{
				this._templateIndex = this.GetTemplateIndex();
			}
			int templateIndex = this.TemplateIndex;
			ITemplate template = null;
			if (templateIndex != 0)
			{
				if (templateIndex != 1)
				{
					int num = templateIndex - 2;
					RoleGroupCollection roleGroups = this.RoleGroups;
					if (0 <= num && num < roleGroups.Count)
					{
						template = roleGroups[num].ContentTemplate;
					}
				}
				else
				{
					template = this.LoggedInTemplate;
				}
			}
			else
			{
				template = this.AnonymousTemplate;
			}
			if (template != null)
			{
				Control control = new Control();
				template.InstantiateIn(control);
				this.Controls.Add(control);
			}
		}

		// Token: 0x060036C6 RID: 14022 RVA: 0x00061169 File Offset: 0x0005F369
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override void Focus()
		{
			throw new NotSupportedException(SR.GetString("NoFocusSupport", new object[]
			{
				base.GetType().Name
			}));
		}

		// Token: 0x060036C7 RID: 14023 RVA: 0x000B1344 File Offset: 0x000AF544
		protected internal override void LoadControlState(object savedState)
		{
			if (savedState != null)
			{
				Pair pair = (Pair)savedState;
				if (pair.First != null)
				{
					base.LoadControlState(pair.First);
				}
				if (pair.Second != null)
				{
					this._templateIndex = (int)pair.Second;
				}
			}
		}

		// Token: 0x060036C8 RID: 14024 RVA: 0x000B1388 File Offset: 0x000AF588
		protected internal override void OnInit(EventArgs e)
		{
			base.OnInit(e);
			if (this.Page != null)
			{
				this.Page.RegisterRequiresControlState(this);
			}
		}

		// Token: 0x060036C9 RID: 14025 RVA: 0x000B13A5 File Offset: 0x000AF5A5
		protected internal override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);
			this.TemplateIndex = this.GetTemplateIndex();
			this.EnsureChildControls();
		}

		// Token: 0x060036CA RID: 14026 RVA: 0x000B13C0 File Offset: 0x000AF5C0
		protected virtual void OnViewChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[LoginView.EventViewChanged];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x060036CB RID: 14027 RVA: 0x000B13F0 File Offset: 0x000AF5F0
		protected virtual void OnViewChanging(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[LoginView.EventViewChanging];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x060036CC RID: 14028 RVA: 0x000B141E File Offset: 0x000AF61E
		protected internal override void Render(HtmlTextWriter writer)
		{
			this.EnsureChildControls();
			base.Render(writer);
		}

		// Token: 0x060036CD RID: 14029 RVA: 0x000B1430 File Offset: 0x000AF630
		protected internal override object SaveControlState()
		{
			object obj = base.SaveControlState();
			if (obj != null || this._templateIndex != 0)
			{
				object y = null;
				if (this._templateIndex != 0)
				{
					y = this._templateIndex;
				}
				return new Pair(obj, y);
			}
			return null;
		}

		// Token: 0x060036CE RID: 14030 RVA: 0x000B1470 File Offset: 0x000AF670
		[SecurityPermission(SecurityAction.Demand, Unrestricted = true)]
		protected override void SetDesignModeState(IDictionary data)
		{
			if (data != null)
			{
				object obj = data["TemplateIndex"];
				if (obj != null)
				{
					this.TemplateIndex = (int)obj;
					base.ChildControlsCreated = false;
				}
			}
		}

		// Token: 0x060036CF RID: 14031 RVA: 0x000B14A4 File Offset: 0x000AF6A4
		private int GetTemplateIndex()
		{
			if (base.DesignMode || this.Page == null || !this.Page.Request.IsAuthenticated)
			{
				return 0;
			}
			IPrincipal user = LoginUtil.GetUser(this);
			int num = -1;
			if (user != null)
			{
				num = this.RoleGroups.GetMatchingRoleGroupInternal(user);
			}
			if (num >= 0)
			{
				return num + 2;
			}
			return 1;
		}

		// Token: 0x04002219 RID: 8729
		private RoleGroupCollection _roleGroups;

		// Token: 0x0400221A RID: 8730
		private ITemplate _loggedInTemplate;

		// Token: 0x0400221B RID: 8731
		private ITemplate _anonymousTemplate;

		// Token: 0x0400221C RID: 8732
		private int _templateIndex;

		// Token: 0x0400221D RID: 8733
		private const int anonymousTemplateIndex = 0;

		// Token: 0x0400221E RID: 8734
		private const int loggedInTemplateIndex = 1;

		// Token: 0x0400221F RID: 8735
		private const int roleGroupStartingIndex = 2;

		// Token: 0x04002220 RID: 8736
		private static readonly object EventViewChanging = new object();

		// Token: 0x04002221 RID: 8737
		private static readonly object EventViewChanged = new object();
	}
}
