using System;
using System.Collections;
using System.Collections.Specialized;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Design;
using System.Drawing;
using System.Drawing.Design;
using System.Globalization;
using System.Resources;
using System.Web.Compilation;

namespace System.Web.UI.Design
{
	// Token: 0x02000089 RID: 137
	public abstract class WebFormsRootDesigner : IRootDesigner, IDesigner, IDisposable, IDesignerFilter
	{
		// Token: 0x1700010A RID: 266
		// (get) Token: 0x06000410 RID: 1040 RVA: 0x000135AB File Offset: 0x000117AB
		// (set) Token: 0x06000411 RID: 1041 RVA: 0x000135B3 File Offset: 0x000117B3
		public virtual IComponent Component
		{
			get
			{
				return this._component;
			}
			set
			{
				this._component = value;
			}
		}

		// Token: 0x06000412 RID: 1042 RVA: 0x000135BC File Offset: 0x000117BC
		~WebFormsRootDesigner()
		{
			this.Dispose(false);
		}

		// Token: 0x1700010B RID: 267
		// (get) Token: 0x06000413 RID: 1043 RVA: 0x000135EC File Offset: 0x000117EC
		public CultureInfo CurrentCulture
		{
			get
			{
				return CultureInfo.CurrentCulture;
			}
		}

		// Token: 0x1700010C RID: 268
		// (get) Token: 0x06000414 RID: 1044
		public abstract string DocumentUrl { get; }

		// Token: 0x1700010D RID: 269
		// (get) Token: 0x06000415 RID: 1045
		public abstract bool IsDesignerViewLocked { get; }

		// Token: 0x1700010E RID: 270
		// (get) Token: 0x06000416 RID: 1046
		public abstract bool IsLoading { get; }

		// Token: 0x1700010F RID: 271
		// (get) Token: 0x06000417 RID: 1047
		public abstract WebFormsReferenceManager ReferenceManager { get; }

		// Token: 0x17000110 RID: 272
		// (get) Token: 0x06000418 RID: 1048 RVA: 0x000135F3 File Offset: 0x000117F3
		protected ViewTechnology[] SupportedTechnologies
		{
			get
			{
				return new ViewTechnology[]
				{
					ViewTechnology.Default
				};
			}
		}

		// Token: 0x17000111 RID: 273
		// (get) Token: 0x06000419 RID: 1049 RVA: 0x000135FF File Offset: 0x000117FF
		protected DesignerVerbCollection Verbs
		{
			get
			{
				return new DesignerVerbCollection();
			}
		}

		// Token: 0x0600041A RID: 1050 RVA: 0x00013608 File Offset: 0x00011808
		protected internal virtual object GetService(Type serviceType)
		{
			if (this._component != null)
			{
				ISite site = this._component.Site;
				if (site != null)
				{
					return site.GetService(serviceType);
				}
			}
			return null;
		}

		// Token: 0x0600041B RID: 1051 RVA: 0x00003598 File Offset: 0x00001798
		protected object GetView(ViewTechnology viewTechnology)
		{
			return null;
		}

		// Token: 0x1400000F RID: 15
		// (add) Token: 0x0600041C RID: 1052 RVA: 0x00013635 File Offset: 0x00011835
		// (remove) Token: 0x0600041D RID: 1053 RVA: 0x0001364E File Offset: 0x0001184E
		public event EventHandler LoadComplete
		{
			add
			{
				this._loadCompleteHandler = (EventHandler)Delegate.Combine(this._loadCompleteHandler, value);
			}
			remove
			{
				this._loadCompleteHandler = (EventHandler)Delegate.Remove(this._loadCompleteHandler, value);
			}
		}

		// Token: 0x0600041E RID: 1054
		public abstract void AddClientScriptToDocument(ClientScriptItem scriptItem);

		// Token: 0x0600041F RID: 1055
		public abstract string AddControlToDocument(Control newControl, Control referenceControl, ControlLocation location);

		// Token: 0x06000420 RID: 1056 RVA: 0x00013667 File Offset: 0x00011867
		protected virtual DesignerActionService CreateDesignerActionService(IServiceProvider serviceProvider)
		{
			return new WebFormsDesignerActionService(serviceProvider);
		}

		// Token: 0x06000421 RID: 1057 RVA: 0x0001366F File Offset: 0x0001186F
		protected virtual IUrlResolutionService CreateUrlResolutionService()
		{
			return new WebFormsRootDesigner.UrlResolutionService(this);
		}

		// Token: 0x06000422 RID: 1058 RVA: 0x00013678 File Offset: 0x00011878
		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				IPropertyValueUIService propertyValueUIService = (IPropertyValueUIService)this.GetService(typeof(IPropertyValueUIService));
				if (propertyValueUIService != null)
				{
					propertyValueUIService.RemovePropertyValueUIHandler(new PropertyValueUIHandler(this.OnGetUIValueItem));
				}
				IServiceContainer serviceContainer = (IServiceContainer)this.GetService(typeof(IServiceContainer));
				if (serviceContainer != null)
				{
					if (this._urlResolutionService != null)
					{
						serviceContainer.RemoveService(typeof(IUrlResolutionService));
					}
					serviceContainer.RemoveService(typeof(IImplicitResourceProvider));
					if (this._designerActionService != null)
					{
						this._designerActionService.Dispose();
					}
					this._designerActionUIService.Dispose();
				}
				this._urlResolutionService = null;
				this._component = null;
			}
		}

		// Token: 0x06000423 RID: 1059 RVA: 0x00013723 File Offset: 0x00011923
		public virtual string GenerateEmptyDesignTimeHtml(Control control)
		{
			return this.GenerateErrorDesignTimeHtml(control, null, string.Empty);
		}

		// Token: 0x06000424 RID: 1060 RVA: 0x00013734 File Offset: 0x00011934
		public virtual string GenerateErrorDesignTimeHtml(Control control, Exception e, string errorMessage)
		{
			string name = control.Site.Name;
			if (errorMessage == null)
			{
				errorMessage = string.Empty;
			}
			else
			{
				errorMessage = HttpUtility.HtmlEncode(errorMessage);
			}
			if (e != null)
			{
				errorMessage = errorMessage + "<br />" + HttpUtility.HtmlEncode(e.Message);
			}
			return string.Format(CultureInfo.InvariantCulture, ControlDesigner.ErrorDesignTimeHtmlTemplate, new object[]
			{
				SR.GetString("ControlDesigner_DesignTimeHtmlError"),
				HttpUtility.HtmlEncode(name),
				errorMessage
			});
		}

		// Token: 0x06000425 RID: 1061
		public abstract ClientScriptItemCollection GetClientScriptsInDocument();

		// Token: 0x06000426 RID: 1062
		protected internal abstract void GetControlViewAndTag(Control control, out IControlDesignerView view, out IControlDesignerTag tag);

		// Token: 0x06000427 RID: 1063 RVA: 0x000137AC File Offset: 0x000119AC
		public virtual void Initialize(IComponent component)
		{
			ControlDesigner.VerifyInitializeArgument(component, typeof(TemplateControl));
			this._component = component;
			IServiceContainer serviceContainer = (IServiceContainer)this.GetService(typeof(IServiceContainer));
			if (serviceContainer != null)
			{
				this._urlResolutionService = this.CreateUrlResolutionService();
				if (this._urlResolutionService != null)
				{
					serviceContainer.AddService(typeof(IUrlResolutionService), this._urlResolutionService);
				}
				this._designerActionService = this.CreateDesignerActionService(this._component.Site);
				this._designerActionUIService = new DesignerActionUIService(this._component.Site);
				ServiceCreatorCallback callback = new ServiceCreatorCallback(this.OnCreateService);
				serviceContainer.AddService(typeof(IImplicitResourceProvider), callback);
			}
			IPropertyValueUIService propertyValueUIService = (IPropertyValueUIService)this.GetService(typeof(IPropertyValueUIService));
			if (propertyValueUIService != null)
			{
				propertyValueUIService.AddPropertyValueUIHandler(new PropertyValueUIHandler(this.OnGetUIValueItem));
			}
		}

		// Token: 0x06000428 RID: 1064 RVA: 0x0001388C File Offset: 0x00011A8C
		private object OnCreateService(IServiceContainer container, Type serviceType)
		{
			if (serviceType == typeof(IImplicitResourceProvider))
			{
				if (this._implicitResourceProvider == null)
				{
					DesignTimeResourceProviderFactory designTimeResourceProviderFactory = ControlDesigner.GetDesignTimeResourceProviderFactory(this.Component.Site);
					IResourceProvider resourceProvider = designTimeResourceProviderFactory.CreateDesignTimeLocalResourceProvider(this.Component.Site);
					this._implicitResourceProvider = (resourceProvider as IImplicitResourceProvider);
					if (this._implicitResourceProvider == null)
					{
						this._implicitResourceProvider = new WebFormsRootDesigner.ImplicitResourceProvider(this);
					}
				}
				return this._implicitResourceProvider;
			}
			return null;
		}

		// Token: 0x06000429 RID: 1065 RVA: 0x00013900 File Offset: 0x00011B00
		private void OnGetUIValueItem(ITypeDescriptorContext context, PropertyDescriptor propDesc, ArrayList valueUIItemList)
		{
			Control control = context.Instance as Control;
			if (control != null)
			{
				IDataBindingsAccessor dataBindingsAccessor = control;
				if (dataBindingsAccessor.HasDataBindings)
				{
					DataBinding dataBinding = dataBindingsAccessor.DataBindings[propDesc.Name];
					if (dataBinding != null)
					{
						valueUIItemList.Add(new WebFormsRootDesigner.DataBindingUIItem());
					}
				}
				IExpressionsAccessor expressionsAccessor = control;
				if (expressionsAccessor.HasExpressions)
				{
					ExpressionBinding expressionBinding = expressionsAccessor.Expressions[propDesc.Name];
					if (expressionBinding != null)
					{
						if (expressionBinding.Generated)
						{
							valueUIItemList.Add(new WebFormsRootDesigner.ImplicitExpressionUIItem());
							return;
						}
						valueUIItemList.Add(new WebFormsRootDesigner.ExpressionBindingUIItem());
					}
				}
			}
		}

		// Token: 0x0600042A RID: 1066 RVA: 0x0001398A File Offset: 0x00011B8A
		protected virtual void OnLoadComplete(EventArgs e)
		{
			if (this._loadCompleteHandler != null)
			{
				this._loadCompleteHandler(this, e);
			}
		}

		// Token: 0x0600042B RID: 1067 RVA: 0x00003937 File Offset: 0x00001B37
		protected virtual void PostFilterAttributes(IDictionary attributes)
		{
		}

		// Token: 0x0600042C RID: 1068 RVA: 0x00003937 File Offset: 0x00001B37
		protected virtual void PostFilterEvents(IDictionary events)
		{
		}

		// Token: 0x0600042D RID: 1069 RVA: 0x00003937 File Offset: 0x00001B37
		protected virtual void PostFilterProperties(IDictionary properties)
		{
		}

		// Token: 0x0600042E RID: 1070 RVA: 0x00003937 File Offset: 0x00001B37
		protected virtual void PreFilterAttributes(IDictionary attributes)
		{
		}

		// Token: 0x0600042F RID: 1071 RVA: 0x00003937 File Offset: 0x00001B37
		protected virtual void PreFilterEvents(IDictionary events)
		{
		}

		// Token: 0x06000430 RID: 1072 RVA: 0x00003937 File Offset: 0x00001B37
		protected virtual void PreFilterProperties(IDictionary properties)
		{
		}

		// Token: 0x06000431 RID: 1073
		public abstract void RemoveClientScriptFromDocument(string clientScriptId);

		// Token: 0x06000432 RID: 1074
		public abstract void RemoveControlFromDocument(Control control);

		// Token: 0x06000433 RID: 1075 RVA: 0x000139A4 File Offset: 0x00011BA4
		public string ResolveUrl(string relativeUrl)
		{
			if (relativeUrl == null)
			{
				throw new ArgumentNullException("relativeUrl");
			}
			string text = this.DocumentUrl;
			if (text == null || text.Length == 0 || WebFormsRootDesigner.IsAppRelativePath(relativeUrl) || WebFormsRootDesigner.IsRooted(relativeUrl) || !WebFormsRootDesigner.IsAppRelativePath(text))
			{
				return relativeUrl;
			}
			text = text.Replace("~", "file://foo");
			Uri baseUri = new Uri(text, true);
			Uri uri = new Uri(baseUri, relativeUrl);
			string text2 = uri.ToString();
			return text2.Replace("file://foo", "~");
		}

		// Token: 0x06000434 RID: 1076 RVA: 0x00013A28 File Offset: 0x00011C28
		public virtual void SetControlID(Control control, string id)
		{
			ISite site = control.Site;
			site.Name = id;
			control.ID = id.Trim();
		}

		// Token: 0x06000435 RID: 1077 RVA: 0x00013A4F File Offset: 0x00011C4F
		private static bool IsRooted(string basepath)
		{
			return basepath == null || basepath.Length == 0 || basepath[0] == '/' || basepath[0] == '\\';
		}

		// Token: 0x06000436 RID: 1078 RVA: 0x00013A74 File Offset: 0x00011C74
		private static bool IsAppRelativePath(string path)
		{
			return path.Length >= 2 && path[0] == '~' && (path[1] == '/' || path[1] == '\\');
		}

		// Token: 0x17000112 RID: 274
		// (get) Token: 0x06000437 RID: 1079 RVA: 0x00013AA4 File Offset: 0x00011CA4
		DesignerVerbCollection IDesigner.Verbs
		{
			get
			{
				return this.Verbs;
			}
		}

		// Token: 0x06000438 RID: 1080 RVA: 0x00003937 File Offset: 0x00001B37
		void IDesigner.DoDefaultAction()
		{
		}

		// Token: 0x06000439 RID: 1081 RVA: 0x00013AAC File Offset: 0x00011CAC
		void IDesignerFilter.PostFilterAttributes(IDictionary attributes)
		{
			this.PostFilterAttributes(attributes);
		}

		// Token: 0x0600043A RID: 1082 RVA: 0x00013AB5 File Offset: 0x00011CB5
		void IDesignerFilter.PostFilterEvents(IDictionary events)
		{
			this.PostFilterEvents(events);
		}

		// Token: 0x0600043B RID: 1083 RVA: 0x00013ABE File Offset: 0x00011CBE
		void IDesignerFilter.PostFilterProperties(IDictionary properties)
		{
			this.PostFilterProperties(properties);
		}

		// Token: 0x0600043C RID: 1084 RVA: 0x00013AC7 File Offset: 0x00011CC7
		void IDesignerFilter.PreFilterAttributes(IDictionary attributes)
		{
			this.PreFilterAttributes(attributes);
		}

		// Token: 0x0600043D RID: 1085 RVA: 0x00013AD0 File Offset: 0x00011CD0
		void IDesignerFilter.PreFilterEvents(IDictionary events)
		{
			this.PreFilterEvents(events);
		}

		// Token: 0x0600043E RID: 1086 RVA: 0x00013AD9 File Offset: 0x00011CD9
		void IDesignerFilter.PreFilterProperties(IDictionary properties)
		{
			this.PreFilterProperties(properties);
		}

		// Token: 0x0600043F RID: 1087 RVA: 0x00013AE2 File Offset: 0x00011CE2
		void IDisposable.Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x17000113 RID: 275
		// (get) Token: 0x06000440 RID: 1088 RVA: 0x00013AF1 File Offset: 0x00011CF1
		ViewTechnology[] IRootDesigner.SupportedTechnologies
		{
			get
			{
				return this.SupportedTechnologies;
			}
		}

		// Token: 0x06000441 RID: 1089 RVA: 0x00013AF9 File Offset: 0x00011CF9
		object IRootDesigner.GetView(ViewTechnology viewTechnology)
		{
			return this.GetView(viewTechnology);
		}

		// Token: 0x040001B3 RID: 435
		private const string dummyProtocolAndServer = "file://foo";

		// Token: 0x040001B4 RID: 436
		private IComponent _component;

		// Token: 0x040001B5 RID: 437
		private EventHandler _loadCompleteHandler;

		// Token: 0x040001B6 RID: 438
		private IUrlResolutionService _urlResolutionService;

		// Token: 0x040001B7 RID: 439
		private DesignerActionService _designerActionService;

		// Token: 0x040001B8 RID: 440
		private DesignerActionUIService _designerActionUIService;

		// Token: 0x040001B9 RID: 441
		private IImplicitResourceProvider _implicitResourceProvider;

		// Token: 0x040001BA RID: 442
		private const char appRelativeCharacter = '~';

		// Token: 0x020003BE RID: 958
		private sealed class DataBindingUIItem : PropertyValueUIItem
		{
			// Token: 0x06002689 RID: 9865 RVA: 0x000ED36B File Offset: 0x000EB56B
			public DataBindingUIItem() : base(WebFormsRootDesigner.DataBindingUIItem.DataBindingBitmap, new PropertyValueUIItemInvokeHandler(WebFormsRootDesigner.DataBindingUIItem.OnValueUIItemInvoke), WebFormsRootDesigner.DataBindingUIItem.DataBindingToolTip)
			{
			}

			// Token: 0x17000825 RID: 2085
			// (get) Token: 0x0600268A RID: 9866 RVA: 0x000ED389 File Offset: 0x000EB589
			private static Bitmap DataBindingBitmap
			{
				get
				{
					if (WebFormsRootDesigner.DataBindingUIItem._dataBindingBitmap == null)
					{
						WebFormsRootDesigner.DataBindingUIItem._dataBindingBitmap = BitmapSelector.CreateBitmap(typeof(WebFormsRootDesigner), "DataBindingGlyph.bmp");
						WebFormsRootDesigner.DataBindingUIItem._dataBindingBitmap.MakeTransparent(Color.Fuchsia);
					}
					return WebFormsRootDesigner.DataBindingUIItem._dataBindingBitmap;
				}
			}

			// Token: 0x17000826 RID: 2086
			// (get) Token: 0x0600268B RID: 9867 RVA: 0x000ED3BF File Offset: 0x000EB5BF
			private static string DataBindingToolTip
			{
				get
				{
					if (WebFormsRootDesigner.DataBindingUIItem._dataBindingToolTip == null)
					{
						WebFormsRootDesigner.DataBindingUIItem._dataBindingToolTip = SR.GetString("DataBindingGlyph_ToolTip");
					}
					return WebFormsRootDesigner.DataBindingUIItem._dataBindingToolTip;
				}
			}

			// Token: 0x0600268C RID: 9868 RVA: 0x00003937 File Offset: 0x00001B37
			private static void OnValueUIItemInvoke(ITypeDescriptorContext context, PropertyDescriptor propDesc, PropertyValueUIItem invokedItem)
			{
			}

			// Token: 0x04001BCF RID: 7119
			private static Bitmap _dataBindingBitmap;

			// Token: 0x04001BD0 RID: 7120
			private static string _dataBindingToolTip;
		}

		// Token: 0x020003BF RID: 959
		private sealed class ExpressionBindingUIItem : PropertyValueUIItem
		{
			// Token: 0x0600268D RID: 9869 RVA: 0x000ED3DC File Offset: 0x000EB5DC
			public ExpressionBindingUIItem() : base(WebFormsRootDesigner.ExpressionBindingUIItem.ExpressionBindingBitmap, new PropertyValueUIItemInvokeHandler(WebFormsRootDesigner.ExpressionBindingUIItem.OnValueUIItemInvoke), WebFormsRootDesigner.ExpressionBindingUIItem.ExpressionBindingToolTip)
			{
			}

			// Token: 0x17000827 RID: 2087
			// (get) Token: 0x0600268E RID: 9870 RVA: 0x000ED3FA File Offset: 0x000EB5FA
			private static Bitmap ExpressionBindingBitmap
			{
				get
				{
					if (WebFormsRootDesigner.ExpressionBindingUIItem._expressionBindingBitmap == null)
					{
						WebFormsRootDesigner.ExpressionBindingUIItem._expressionBindingBitmap = BitmapSelector.CreateBitmap(typeof(WebFormsRootDesigner), "ExpressionBindingGlyph.bmp");
						WebFormsRootDesigner.ExpressionBindingUIItem._expressionBindingBitmap.MakeTransparent(Color.Fuchsia);
					}
					return WebFormsRootDesigner.ExpressionBindingUIItem._expressionBindingBitmap;
				}
			}

			// Token: 0x17000828 RID: 2088
			// (get) Token: 0x0600268F RID: 9871 RVA: 0x000ED430 File Offset: 0x000EB630
			private static string ExpressionBindingToolTip
			{
				get
				{
					if (WebFormsRootDesigner.ExpressionBindingUIItem._expressionBindingToolTip == null)
					{
						WebFormsRootDesigner.ExpressionBindingUIItem._expressionBindingToolTip = SR.GetString("ExpressionBindingGlyph_ToolTip");
					}
					return WebFormsRootDesigner.ExpressionBindingUIItem._expressionBindingToolTip;
				}
			}

			// Token: 0x06002690 RID: 9872 RVA: 0x00003937 File Offset: 0x00001B37
			private static void OnValueUIItemInvoke(ITypeDescriptorContext context, PropertyDescriptor propDesc, PropertyValueUIItem invokedItem)
			{
			}

			// Token: 0x04001BD1 RID: 7121
			private static Bitmap _expressionBindingBitmap;

			// Token: 0x04001BD2 RID: 7122
			private static string _expressionBindingToolTip;
		}

		// Token: 0x020003C0 RID: 960
		private sealed class ImplicitExpressionUIItem : PropertyValueUIItem
		{
			// Token: 0x06002691 RID: 9873 RVA: 0x000ED44D File Offset: 0x000EB64D
			public ImplicitExpressionUIItem() : base(WebFormsRootDesigner.ImplicitExpressionUIItem.ImplicitExpressionBindingBitmap, new PropertyValueUIItemInvokeHandler(WebFormsRootDesigner.ImplicitExpressionUIItem.OnValueUIItemInvoke), WebFormsRootDesigner.ImplicitExpressionUIItem.ImplicitExpressionBindingToolTip)
			{
			}

			// Token: 0x17000829 RID: 2089
			// (get) Token: 0x06002692 RID: 9874 RVA: 0x000ED46B File Offset: 0x000EB66B
			private static Bitmap ImplicitExpressionBindingBitmap
			{
				get
				{
					if (WebFormsRootDesigner.ImplicitExpressionUIItem._expressionBindingBitmap == null)
					{
						WebFormsRootDesigner.ImplicitExpressionUIItem._expressionBindingBitmap = BitmapSelector.CreateBitmap(typeof(WebFormsRootDesigner), "ImplicitExpressionBindingGlyph.bmp");
						WebFormsRootDesigner.ImplicitExpressionUIItem._expressionBindingBitmap.MakeTransparent(Color.Fuchsia);
					}
					return WebFormsRootDesigner.ImplicitExpressionUIItem._expressionBindingBitmap;
				}
			}

			// Token: 0x1700082A RID: 2090
			// (get) Token: 0x06002693 RID: 9875 RVA: 0x000ED4A1 File Offset: 0x000EB6A1
			private static string ImplicitExpressionBindingToolTip
			{
				get
				{
					if (WebFormsRootDesigner.ImplicitExpressionUIItem._expressionBindingToolTip == null)
					{
						WebFormsRootDesigner.ImplicitExpressionUIItem._expressionBindingToolTip = SR.GetString("ImplicitExpressionBindingGlyph_ToolTip");
					}
					return WebFormsRootDesigner.ImplicitExpressionUIItem._expressionBindingToolTip;
				}
			}

			// Token: 0x06002694 RID: 9876 RVA: 0x00003937 File Offset: 0x00001B37
			private static void OnValueUIItemInvoke(ITypeDescriptorContext context, PropertyDescriptor propDesc, PropertyValueUIItem invokedItem)
			{
			}

			// Token: 0x04001BD3 RID: 7123
			private static Bitmap _expressionBindingBitmap;

			// Token: 0x04001BD4 RID: 7124
			private static string _expressionBindingToolTip;
		}

		// Token: 0x020003C1 RID: 961
		private sealed class UrlResolutionService : IUrlResolutionService
		{
			// Token: 0x06002695 RID: 9877 RVA: 0x000ED4BE File Offset: 0x000EB6BE
			public UrlResolutionService(WebFormsRootDesigner owner)
			{
				this._owner = owner;
			}

			// Token: 0x06002696 RID: 9878 RVA: 0x000ED4D0 File Offset: 0x000EB6D0
			string IUrlResolutionService.ResolveClientUrl(string relativeUrl)
			{
				if (relativeUrl == null)
				{
					throw new ArgumentNullException("relativeUrl");
				}
				if (!WebFormsRootDesigner.IsAppRelativePath(relativeUrl))
				{
					return relativeUrl;
				}
				string text = this._owner.DocumentUrl;
				if (text == null || text.Length == 0 || !WebFormsRootDesigner.IsAppRelativePath(text))
				{
					return relativeUrl.Substring(2);
				}
				text = text.Replace("~", "file://foo");
				Uri uri = new Uri(text, true);
				relativeUrl = relativeUrl.Replace("~", "file://foo");
				Uri uri2 = new Uri(relativeUrl, true);
				string text2 = uri.MakeRelativeUri(uri2).ToString();
				return text2.Replace("file://foo", string.Empty);
			}

			// Token: 0x04001BD5 RID: 7125
			private WebFormsRootDesigner _owner;
		}

		// Token: 0x020003C2 RID: 962
		private sealed class ImplicitResourceProvider : IImplicitResourceProvider
		{
			// Token: 0x06002697 RID: 9879 RVA: 0x000ED56C File Offset: 0x000EB76C
			public ImplicitResourceProvider(WebFormsRootDesigner owner)
			{
				this._owner = owner;
			}

			// Token: 0x06002698 RID: 9880 RVA: 0x0000C5AC File Offset: 0x0000A7AC
			object IImplicitResourceProvider.GetObject(ImplicitResourceKey key, CultureInfo culture)
			{
				throw new NotSupportedException();
			}

			// Token: 0x06002699 RID: 9881 RVA: 0x000ED57C File Offset: 0x000EB77C
			ICollection IImplicitResourceProvider.GetImplicitResourceKeys(string keyPrefix)
			{
				IDictionary pageResources = this.GetPageResources();
				return pageResources[keyPrefix] as ICollection;
			}

			// Token: 0x0600269A RID: 9882 RVA: 0x000ED59C File Offset: 0x000EB79C
			private IDictionary GetPageResources()
			{
				if (this._owner.Component == null)
				{
					return null;
				}
				IServiceProvider site = this._owner.Component.Site;
				if (site == null)
				{
					return null;
				}
				DesignTimeResourceProviderFactory designTimeResourceProviderFactory = ControlDesigner.GetDesignTimeResourceProviderFactory(site);
				if (designTimeResourceProviderFactory == null)
				{
					return null;
				}
				IResourceProvider resourceProvider = designTimeResourceProviderFactory.CreateDesignTimeLocalResourceProvider(site);
				if (resourceProvider == null)
				{
					return null;
				}
				IResourceReader resourceReader = resourceProvider.ResourceReader;
				if (resourceReader == null)
				{
					return null;
				}
				IDictionary dictionary = new HybridDictionary(true);
				if (resourceReader != null)
				{
					foreach (object obj in resourceReader)
					{
						string text = (string)((DictionaryEntry)obj).Key;
						string filter = string.Empty;
						if (text.IndexOf(':') > 0)
						{
							string[] array = text.Split(new char[]
							{
								':'
							});
							if (array.Length > 2)
							{
								continue;
							}
							filter = array[0];
							text = array[1];
						}
						int num = text.IndexOf('.');
						if (num > 0)
						{
							string text2 = text.Substring(0, num);
							string property = text.Substring(num + 1);
							ArrayList arrayList = (ArrayList)dictionary[text2];
							if (arrayList == null)
							{
								arrayList = new ArrayList();
								dictionary[text2] = arrayList;
							}
							arrayList.Add(new ImplicitResourceKey
							{
								Filter = filter,
								Property = property,
								KeyPrefix = text2
							});
						}
					}
				}
				return dictionary;
			}

			// Token: 0x04001BD6 RID: 7126
			private WebFormsRootDesigner _owner;
		}
	}
}
