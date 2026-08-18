using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.ComponentModel.Design.Serialization;
using System.Security.Permissions;
using System.Web.Caching;
using System.Web.SessionState;

namespace System.Web.UI
{
	// Token: 0x02000429 RID: 1065
	[DefaultEvent("Load")]
	[Designer("System.Web.UI.Design.UserControlDesigner, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(IDesigner))]
	[Designer("Microsoft.VisualStudio.Web.WebForms.WebFormDesigner, Microsoft.VisualStudio.Web, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(IRootDesigner))]
	[DesignerCategory("ASPXCodeBehind")]
	[DesignerSerializer("Microsoft.VisualStudio.Web.WebForms.WebFormCodeDomSerializer, Microsoft.VisualStudio.Web, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.ComponentModel.Design.Serialization.TypeCodeDomSerializer, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[ParseChildren(true)]
	[ToolboxItem(false)]
	[ControlBuilder(typeof(UserControlControlBuilder))]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class UserControl : TemplateControl, IAttributeAccessor, INonBindingContainer, INamingContainer, IUserControlDesignerAccessor
	{
		// Token: 0x17000B3B RID: 2875
		// (get) Token: 0x06003314 RID: 13076 RVA: 0x000DDF6C File Offset: 0x000DCF6C
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public AttributeCollection Attributes
		{
			get
			{
				if (this.attributes == null)
				{
					if (this.attributeStorage == null)
					{
						this.attributeStorage = new StateBag(true);
						if (base.IsTrackingViewState)
						{
							this.attributeStorage.TrackViewState();
						}
					}
					this.attributes = new AttributeCollection(this.attributeStorage);
				}
				return this.attributes;
			}
		}

		// Token: 0x17000B3C RID: 2876
		// (get) Token: 0x06003315 RID: 13077 RVA: 0x000DDFBF File Offset: 0x000DCFBF
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public HttpApplicationState Application
		{
			get
			{
				return this.Page.Application;
			}
		}

		// Token: 0x17000B3D RID: 2877
		// (get) Token: 0x06003316 RID: 13078 RVA: 0x000DDFCC File Offset: 0x000DCFCC
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public TraceContext Trace
		{
			get
			{
				return this.Page.Trace;
			}
		}

		// Token: 0x17000B3E RID: 2878
		// (get) Token: 0x06003317 RID: 13079 RVA: 0x000DDFD9 File Offset: 0x000DCFD9
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public HttpRequest Request
		{
			get
			{
				return this.Page.Request;
			}
		}

		// Token: 0x17000B3F RID: 2879
		// (get) Token: 0x06003318 RID: 13080 RVA: 0x000DDFE6 File Offset: 0x000DCFE6
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public HttpResponse Response
		{
			get
			{
				return this.Page.Response;
			}
		}

		// Token: 0x17000B40 RID: 2880
		// (get) Token: 0x06003319 RID: 13081 RVA: 0x000DDFF3 File Offset: 0x000DCFF3
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public HttpServerUtility Server
		{
			get
			{
				return this.Page.Server;
			}
		}

		// Token: 0x17000B41 RID: 2881
		// (get) Token: 0x0600331A RID: 13082 RVA: 0x000DE000 File Offset: 0x000DD000
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public Cache Cache
		{
			get
			{
				return this.Page.Cache;
			}
		}

		// Token: 0x17000B42 RID: 2882
		// (get) Token: 0x0600331B RID: 13083 RVA: 0x000DE010 File Offset: 0x000DD010
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public ControlCachePolicy CachePolicy
		{
			get
			{
				BasePartialCachingControl basePartialCachingControl = this.Parent as BasePartialCachingControl;
				if (basePartialCachingControl != null)
				{
					return basePartialCachingControl.CachePolicy;
				}
				return ControlCachePolicy.GetCachePolicyStub();
			}
		}

		// Token: 0x17000B43 RID: 2883
		// (get) Token: 0x0600331C RID: 13084 RVA: 0x000DE038 File Offset: 0x000DD038
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public bool IsPostBack
		{
			get
			{
				return this.Page.IsPostBack;
			}
		}

		// Token: 0x17000B44 RID: 2884
		// (get) Token: 0x0600331D RID: 13085 RVA: 0x000DE045 File Offset: 0x000DD045
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public HttpSessionState Session
		{
			get
			{
				return this.Page.Session;
			}
		}

		// Token: 0x0600331E RID: 13086 RVA: 0x000DE052 File Offset: 0x000DD052
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void DesignerInitialize()
		{
			this.InitRecursive(null);
		}

		// Token: 0x0600331F RID: 13087 RVA: 0x000DE05C File Offset: 0x000DD05C
		protected internal override void OnInit(EventArgs e)
		{
			bool designMode = base.DesignMode;
			if (!designMode && this.Page != null && this.Page.Site != null)
			{
				designMode = this.Page.Site.DesignMode;
			}
			if (!designMode)
			{
				this.InitializeAsUserControlInternal();
			}
			base.OnInit(e);
		}

		// Token: 0x06003320 RID: 13088 RVA: 0x000DE0A9 File Offset: 0x000DD0A9
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void InitializeAsUserControl(Page page)
		{
			this._page = page;
			this.InitializeAsUserControlInternal();
		}

		// Token: 0x06003321 RID: 13089 RVA: 0x000DE0B8 File Offset: 0x000DD0B8
		internal void InitializeAsUserControlInternal()
		{
			if (this._fUserControlInitialized)
			{
				return;
			}
			this._fUserControlInitialized = true;
			base.HookUpAutomaticHandlers();
			this.FrameworkInitialize();
		}

		// Token: 0x06003322 RID: 13090 RVA: 0x000DE0D8 File Offset: 0x000DD0D8
		protected override void LoadViewState(object savedState)
		{
			if (savedState != null)
			{
				Pair pair = (Pair)savedState;
				base.LoadViewState(pair.First);
				if (pair.Second != null)
				{
					if (this.attributeStorage == null)
					{
						this.attributeStorage = new StateBag(true);
						this.attributeStorage.TrackViewState();
					}
					this.attributeStorage.LoadViewState(pair.Second);
				}
			}
		}

		// Token: 0x06003323 RID: 13091 RVA: 0x000DE134 File Offset: 0x000DD134
		protected override object SaveViewState()
		{
			Pair result = null;
			object obj = base.SaveViewState();
			object obj2 = null;
			if (this.attributeStorage != null)
			{
				obj2 = this.attributeStorage.SaveViewState();
			}
			if (obj != null || obj2 != null)
			{
				result = new Pair(obj, obj2);
			}
			return result;
		}

		// Token: 0x06003324 RID: 13092 RVA: 0x000DE16F File Offset: 0x000DD16F
		string IAttributeAccessor.GetAttribute(string name)
		{
			if (this.attributeStorage == null)
			{
				return null;
			}
			return (string)this.attributeStorage[name];
		}

		// Token: 0x06003325 RID: 13093 RVA: 0x000DE18C File Offset: 0x000DD18C
		void IAttributeAccessor.SetAttribute(string name, string value)
		{
			this.Attributes[name] = value;
		}

		// Token: 0x06003326 RID: 13094 RVA: 0x000DE19B File Offset: 0x000DD19B
		public string MapPath(string virtualPath)
		{
			return this.Request.MapPath(VirtualPath.CreateAllowNull(virtualPath), base.TemplateControlVirtualDirectory, true);
		}

		// Token: 0x17000B45 RID: 2885
		// (get) Token: 0x06003327 RID: 13095 RVA: 0x000DE1B8 File Offset: 0x000DD1B8
		// (set) Token: 0x06003328 RID: 13096 RVA: 0x000DE1E5 File Offset: 0x000DD1E5
		string IUserControlDesignerAccessor.TagName
		{
			get
			{
				string text = (string)this.ViewState["!DesignTimeTagName"];
				if (text == null)
				{
					return string.Empty;
				}
				return text;
			}
			set
			{
				this.ViewState["!DesignTimeTagName"] = value;
			}
		}

		// Token: 0x17000B46 RID: 2886
		// (get) Token: 0x06003329 RID: 13097 RVA: 0x000DE1F8 File Offset: 0x000DD1F8
		// (set) Token: 0x0600332A RID: 13098 RVA: 0x000DE225 File Offset: 0x000DD225
		string IUserControlDesignerAccessor.InnerText
		{
			get
			{
				string text = (string)this.ViewState["!DesignTimeInnerText"];
				if (text == null)
				{
					return string.Empty;
				}
				return text;
			}
			set
			{
				this.ViewState["!DesignTimeInnerText"] = value;
			}
		}

		// Token: 0x040023E7 RID: 9191
		private StateBag attributeStorage;

		// Token: 0x040023E8 RID: 9192
		private AttributeCollection attributes;

		// Token: 0x040023E9 RID: 9193
		private bool _fUserControlInitialized;
	}
}
