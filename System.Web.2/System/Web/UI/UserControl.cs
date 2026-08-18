using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.ComponentModel.Design.Serialization;
using System.Web.Caching;
using System.Web.ModelBinding;
using System.Web.SessionState;

namespace System.Web.UI
{
	// Token: 0x02000320 RID: 800
	[ControlBuilder(typeof(UserControlControlBuilder))]
	[DefaultEvent("Load")]
	[Designer("System.Web.UI.Design.UserControlDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(IDesigner))]
	[Designer("Microsoft.VisualStudio.Web.WebForms.WebFormDesigner, Microsoft.VisualStudio.Web, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(IRootDesigner))]
	[DesignerCategory("ASPXCodeBehind")]
	[DesignerSerializer("Microsoft.VisualStudio.Web.WebForms.WebFormCodeDomSerializer, Microsoft.VisualStudio.Web, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.ComponentModel.Design.Serialization.TypeCodeDomSerializer, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[ParseChildren(true)]
	[ToolboxItem(false)]
	public class UserControl : TemplateControl, IAttributeAccessor, INonBindingContainer, INamingContainer, IUserControlDesignerAccessor
	{
		// Token: 0x17000A55 RID: 2645
		// (get) Token: 0x0600251F RID: 9503 RVA: 0x0007A810 File Offset: 0x00078A10
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

		// Token: 0x17000A56 RID: 2646
		// (get) Token: 0x06002520 RID: 9504 RVA: 0x0007A863 File Offset: 0x00078A63
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public HttpApplicationState Application
		{
			get
			{
				return this.Page.Application;
			}
		}

		// Token: 0x17000A57 RID: 2647
		// (get) Token: 0x06002521 RID: 9505 RVA: 0x0007A870 File Offset: 0x00078A70
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public TraceContext Trace
		{
			get
			{
				return this.Page.Trace;
			}
		}

		// Token: 0x17000A58 RID: 2648
		// (get) Token: 0x06002522 RID: 9506 RVA: 0x0007A87D File Offset: 0x00078A7D
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public HttpRequest Request
		{
			get
			{
				return this.Page.Request;
			}
		}

		// Token: 0x17000A59 RID: 2649
		// (get) Token: 0x06002523 RID: 9507 RVA: 0x0007A88A File Offset: 0x00078A8A
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public HttpResponse Response
		{
			get
			{
				return this.Page.Response;
			}
		}

		// Token: 0x17000A5A RID: 2650
		// (get) Token: 0x06002524 RID: 9508 RVA: 0x0007A897 File Offset: 0x00078A97
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public HttpServerUtility Server
		{
			get
			{
				return this.Page.Server;
			}
		}

		// Token: 0x17000A5B RID: 2651
		// (get) Token: 0x06002525 RID: 9509 RVA: 0x0007A8A4 File Offset: 0x00078AA4
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public Cache Cache
		{
			get
			{
				return this.Page.Cache;
			}
		}

		// Token: 0x17000A5C RID: 2652
		// (get) Token: 0x06002526 RID: 9510 RVA: 0x0007A8B4 File Offset: 0x00078AB4
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

		// Token: 0x17000A5D RID: 2653
		// (get) Token: 0x06002527 RID: 9511 RVA: 0x0007A8DC File Offset: 0x00078ADC
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public bool IsPostBack
		{
			get
			{
				return this.Page.IsPostBack;
			}
		}

		// Token: 0x17000A5E RID: 2654
		// (get) Token: 0x06002528 RID: 9512 RVA: 0x0007A8E9 File Offset: 0x00078AE9
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public HttpSessionState Session
		{
			get
			{
				return this.Page.Session;
			}
		}

		// Token: 0x06002529 RID: 9513 RVA: 0x00069957 File Offset: 0x00067B57
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void DesignerInitialize()
		{
			this.InitRecursive(null);
		}

		// Token: 0x0600252A RID: 9514 RVA: 0x0007A8F8 File Offset: 0x00078AF8
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

		// Token: 0x0600252B RID: 9515 RVA: 0x0007A945 File Offset: 0x00078B45
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void InitializeAsUserControl(Page page)
		{
			this._page = page;
			this.InitializeAsUserControlInternal();
		}

		// Token: 0x0600252C RID: 9516 RVA: 0x0007A954 File Offset: 0x00078B54
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

		// Token: 0x0600252D RID: 9517 RVA: 0x0007A974 File Offset: 0x00078B74
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

		// Token: 0x0600252E RID: 9518 RVA: 0x0007A9D0 File Offset: 0x00078BD0
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

		// Token: 0x0600252F RID: 9519 RVA: 0x0007AA0B File Offset: 0x00078C0B
		string IAttributeAccessor.GetAttribute(string name)
		{
			if (this.attributeStorage == null)
			{
				return null;
			}
			return (string)this.attributeStorage[name];
		}

		// Token: 0x06002530 RID: 9520 RVA: 0x0007AA28 File Offset: 0x00078C28
		void IAttributeAccessor.SetAttribute(string name, string value)
		{
			this.Attributes[name] = value;
		}

		// Token: 0x06002531 RID: 9521 RVA: 0x0007AA37 File Offset: 0x00078C37
		public string MapPath(string virtualPath)
		{
			return this.Request.MapPath(VirtualPath.CreateAllowNull(virtualPath), base.TemplateControlVirtualDirectory, true);
		}

		// Token: 0x17000A5F RID: 2655
		// (get) Token: 0x06002532 RID: 9522 RVA: 0x0007AA54 File Offset: 0x00078C54
		// (set) Token: 0x06002533 RID: 9523 RVA: 0x0007AA81 File Offset: 0x00078C81
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

		// Token: 0x17000A60 RID: 2656
		// (get) Token: 0x06002534 RID: 9524 RVA: 0x0007AA94 File Offset: 0x00078C94
		// (set) Token: 0x06002535 RID: 9525 RVA: 0x0007AAC1 File Offset: 0x00078CC1
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

		// Token: 0x06002536 RID: 9526 RVA: 0x0007AAD4 File Offset: 0x00078CD4
		public virtual void UpdateModel<TModel>(TModel model) where TModel : class
		{
			this.Page.UpdateModel<TModel>(model);
		}

		// Token: 0x06002537 RID: 9527 RVA: 0x0007AAE2 File Offset: 0x00078CE2
		public virtual void UpdateModel<TModel>(TModel model, IValueProvider valueProvider) where TModel : class
		{
			this.Page.UpdateModel<TModel>(model, valueProvider);
		}

		// Token: 0x06002538 RID: 9528 RVA: 0x0007AAF1 File Offset: 0x00078CF1
		public virtual bool TryUpdateModel<TModel>(TModel model) where TModel : class
		{
			return this.Page.TryUpdateModel<TModel>(model);
		}

		// Token: 0x06002539 RID: 9529 RVA: 0x0007AAFF File Offset: 0x00078CFF
		public virtual bool TryUpdateModel<TModel>(TModel model, IValueProvider valueProvider) where TModel : class
		{
			return this.Page.TryUpdateModel<TModel>(model, valueProvider);
		}

		// Token: 0x04001D70 RID: 7536
		private StateBag attributeStorage;

		// Token: 0x04001D71 RID: 7537
		private AttributeCollection attributes;

		// Token: 0x04001D72 RID: 7538
		private bool _fUserControlInitialized;
	}
}
