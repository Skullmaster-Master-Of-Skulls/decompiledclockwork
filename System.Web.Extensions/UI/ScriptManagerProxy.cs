using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Globalization;
using System.Web.Resources;

namespace System.Web.UI
{
	// Token: 0x02000074 RID: 116
	[DefaultProperty("Scripts")]
	[Designer("System.Web.UI.Design.ScriptManagerProxyDesigner, System.Web.Extensions.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35")]
	[NonVisualControl]
	[ParseChildren(true)]
	[PersistChildren(false)]
	[ToolboxBitmap(typeof(EmbeddedResourceFinder), "System.Web.Resources.ScriptManagerProxy.bmp")]
	public class ScriptManagerProxy : Control, IControl, IClientUrlResolver
	{
		// Token: 0x060004C2 RID: 1218 RVA: 0x00011E41 File Offset: 0x00010041
		public ScriptManagerProxy()
		{
		}

		// Token: 0x060004C3 RID: 1219 RVA: 0x0001732D File Offset: 0x0001552D
		internal ScriptManagerProxy(IScriptManagerInternal scriptManager)
		{
			this._scriptManager = scriptManager;
		}

		// Token: 0x17000147 RID: 327
		// (get) Token: 0x060004C4 RID: 1220 RVA: 0x0001733C File Offset: 0x0001553C
		[ResourceDescription("ScriptManager_AuthenticationService")]
		[Category("Behavior")]
		[DefaultValue(null)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[MergableProperty(false)]
		public AuthenticationServiceManager AuthenticationService
		{
			get
			{
				if (this._authenticationServiceManager == null)
				{
					this._authenticationServiceManager = new AuthenticationServiceManager();
				}
				return this._authenticationServiceManager;
			}
		}

		// Token: 0x17000148 RID: 328
		// (get) Token: 0x060004C5 RID: 1221 RVA: 0x00017357 File Offset: 0x00015557
		[ResourceDescription("ScriptManager_CompositeScript")]
		[Category("Behavior")]
		[DefaultValue(null)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[MergableProperty(false)]
		public CompositeScriptReference CompositeScript
		{
			get
			{
				if (this._compositeScript == null)
				{
					this._compositeScript = new CompositeScriptReference();
				}
				return this._compositeScript;
			}
		}

		// Token: 0x17000149 RID: 329
		// (get) Token: 0x060004C6 RID: 1222 RVA: 0x00017372 File Offset: 0x00015572
		internal bool HasProfileServiceManager
		{
			get
			{
				return this._profileServiceManager != null;
			}
		}

		// Token: 0x1700014A RID: 330
		// (get) Token: 0x060004C7 RID: 1223 RVA: 0x0001737D File Offset: 0x0001557D
		internal bool HasAuthenticationServiceManager
		{
			get
			{
				return this._authenticationServiceManager != null;
			}
		}

		// Token: 0x1700014B RID: 331
		// (get) Token: 0x060004C8 RID: 1224 RVA: 0x00017388 File Offset: 0x00015588
		internal bool HasRoleServiceManager
		{
			get
			{
				return this._roleServiceManager != null;
			}
		}

		// Token: 0x1700014C RID: 332
		// (get) Token: 0x060004C9 RID: 1225 RVA: 0x00017393 File Offset: 0x00015593
		internal EventHandler<HistoryEventArgs> NavigateEvent
		{
			get
			{
				return (EventHandler<HistoryEventArgs>)base.Events[ScriptManagerProxy._navigateEvent];
			}
		}

		// Token: 0x1700014D RID: 333
		// (get) Token: 0x060004CA RID: 1226 RVA: 0x000173AA File Offset: 0x000155AA
		[ResourceDescription("ScriptManager_ProfileService")]
		[Category("Behavior")]
		[DefaultValue(null)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[MergableProperty(false)]
		public ProfileServiceManager ProfileService
		{
			get
			{
				if (this._profileServiceManager == null)
				{
					this._profileServiceManager = new ProfileServiceManager();
				}
				return this._profileServiceManager;
			}
		}

		// Token: 0x1700014E RID: 334
		// (get) Token: 0x060004CB RID: 1227 RVA: 0x000173C5 File Offset: 0x000155C5
		[ResourceDescription("ScriptManager_RoleService")]
		[Category("Behavior")]
		[DefaultValue(null)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[MergableProperty(false)]
		public RoleServiceManager RoleService
		{
			get
			{
				if (this._roleServiceManager == null)
				{
					this._roleServiceManager = new RoleServiceManager();
				}
				return this._roleServiceManager;
			}
		}

		// Token: 0x1700014F RID: 335
		// (get) Token: 0x060004CC RID: 1228 RVA: 0x000173E0 File Offset: 0x000155E0
		private IScriptManagerInternal ScriptManager
		{
			get
			{
				if (this._scriptManager == null)
				{
					if (this.Page == null)
					{
						throw new InvalidOperationException(AtlasWeb.Common_PageCannotBeNull);
					}
					this._scriptManager = System.Web.UI.ScriptManager.GetCurrent(this.Page);
					if (this._scriptManager == null)
					{
						throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, AtlasWeb.Common_ScriptManagerRequired, new object[]
						{
							this.ID
						}));
					}
				}
				return this._scriptManager;
			}
		}

		// Token: 0x17000150 RID: 336
		// (get) Token: 0x060004CD RID: 1229 RVA: 0x0001744D File Offset: 0x0001564D
		[ResourceDescription("ScriptManager_Scripts")]
		[Category("Behavior")]
		[DefaultValue(null)]
		[Editor("System.Web.UI.Design.CollectionEditorBase, System.Web.Extensions.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35", typeof(UITypeEditor))]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[MergableProperty(false)]
		public ScriptReferenceCollection Scripts
		{
			get
			{
				if (this._scripts == null)
				{
					this._scripts = new ScriptReferenceCollection();
				}
				return this._scripts;
			}
		}

		// Token: 0x17000151 RID: 337
		// (get) Token: 0x060004CE RID: 1230 RVA: 0x00017468 File Offset: 0x00015668
		[ResourceDescription("ScriptManager_Services")]
		[Category("Behavior")]
		[DefaultValue(null)]
		[Editor("System.Web.UI.Design.ServiceReferenceCollectionEditor, System.Web.Extensions.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35", typeof(UITypeEditor))]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[MergableProperty(false)]
		public ServiceReferenceCollection Services
		{
			get
			{
				if (this._services == null)
				{
					this._services = new ServiceReferenceCollection();
				}
				return this._services;
			}
		}

		// Token: 0x17000152 RID: 338
		// (get) Token: 0x060004CF RID: 1231 RVA: 0x00011F1F File Offset: 0x0001011F
		// (set) Token: 0x060004D0 RID: 1232 RVA: 0x00002058 File Offset: 0x00000258
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override bool Visible
		{
			get
			{
				return base.Visible;
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x1400000F RID: 15
		// (add) Token: 0x060004D1 RID: 1233 RVA: 0x00017483 File Offset: 0x00015683
		// (remove) Token: 0x060004D2 RID: 1234 RVA: 0x00017496 File Offset: 0x00015696
		[Category("Action")]
		[ResourceDescription("ScriptManager_Navigate")]
		public event EventHandler<HistoryEventArgs> Navigate
		{
			add
			{
				base.Events.AddHandler(ScriptManagerProxy._navigateEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(ScriptManagerProxy._navigateEvent, value);
			}
		}

		// Token: 0x060004D3 RID: 1235 RVA: 0x000174AC File Offset: 0x000156AC
		internal void CollectScripts(List<ScriptReferenceBase> scripts)
		{
			if (this._compositeScript != null && this._compositeScript.Scripts.Count != 0)
			{
				this._compositeScript.ClientUrlResolver = this;
				this._compositeScript.ContainingControl = this;
				this._compositeScript.IsStaticReference = true;
				scripts.Add(this._compositeScript);
			}
			if (this._scripts != null)
			{
				foreach (ScriptReference scriptReference in this._scripts)
				{
					scriptReference.ClientUrlResolver = this;
					scriptReference.ContainingControl = this;
					scriptReference.IsStaticReference = true;
					scripts.Add(scriptReference);
				}
			}
		}

		// Token: 0x060004D4 RID: 1236 RVA: 0x00017560 File Offset: 0x00015760
		protected internal override void OnInit(EventArgs e)
		{
			base.OnInit(e);
			if (!base.DesignMode)
			{
				this.ScriptManager.RegisterProxy(this);
			}
		}

		// Token: 0x060004D5 RID: 1237 RVA: 0x00017580 File Offset: 0x00015780
		internal void RegisterServices(ScriptManager scriptManager)
		{
			if (this._services != null)
			{
				foreach (ServiceReference serviceReference in this._services)
				{
					serviceReference.Register(this, scriptManager);
				}
			}
		}

		// Token: 0x17000153 RID: 339
		// (get) Token: 0x060004D6 RID: 1238 RVA: 0x0001725C File Offset: 0x0001545C
		HttpContextBase IControl.Context
		{
			get
			{
				return new HttpContextWrapper(this.Context);
			}
		}

		// Token: 0x17000154 RID: 340
		// (get) Token: 0x060004D7 RID: 1239 RVA: 0x00017269 File Offset: 0x00015469
		bool IControl.DesignMode
		{
			get
			{
				return base.DesignMode;
			}
		}

		// Token: 0x060004D9 RID: 1241 RVA: 0x00017325 File Offset: 0x00015525
		string IClientUrlResolver.get_AppRelativeTemplateSourceDirectory()
		{
			return base.AppRelativeTemplateSourceDirectory;
		}

		// Token: 0x040001BC RID: 444
		private IScriptManagerInternal _scriptManager;

		// Token: 0x040001BD RID: 445
		private CompositeScriptReference _compositeScript;

		// Token: 0x040001BE RID: 446
		private ScriptReferenceCollection _scripts;

		// Token: 0x040001BF RID: 447
		private ServiceReferenceCollection _services;

		// Token: 0x040001C0 RID: 448
		private ProfileServiceManager _profileServiceManager;

		// Token: 0x040001C1 RID: 449
		private AuthenticationServiceManager _authenticationServiceManager;

		// Token: 0x040001C2 RID: 450
		private RoleServiceManager _roleServiceManager;

		// Token: 0x040001C3 RID: 451
		private static readonly object _navigateEvent = new object();
	}
}
