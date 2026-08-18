using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Security;
using System.Security.Permissions;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI.Common;

namespace Telerik.Web.UI
{
	// Token: 0x02000FD0 RID: 4048
	[ParseChildren(false)]
	[PersistChildren(true)]
	[ClientScriptResource("Telerik.Web.UI.RadAjaxControl", "Telerik.Web.UI.Ajax.Ajax.js")]
	[RequiredScript(typeof(Core))]
	public abstract class RadAjaxControl : WebControl, IPostBackEventHandler
	{
		// Token: 0x06009D2A RID: 40234 RVA: 0x0022FF10 File Offset: 0x0022E110
		public RadAjaxControl()
		{
			this.ajaxSettings = new AjaxSettingsCollection();
		}

		// Token: 0x06009D2B RID: 40235 RVA: 0x0022FF6C File Offset: 0x0022E16C
		internal void EnsureLicensing()
		{
			if (LicenseManager.UsageMode == LicenseUsageMode.Designtime)
			{
				try
				{
					LicenseManager.Validate(base.GetType());
				}
				catch
				{
				}
			}
		}

		// Token: 0x170031BA RID: 12730
		// (get) Token: 0x06009D2C RID: 40236 RVA: 0x0022FFA4 File Offset: 0x0022E1A4
		// (set) Token: 0x06009D2D RID: 40237 RVA: 0x0022FFD4 File Offset: 0x0022E1D4
		[Description("Whether to register the scripts automatically")]
		[NotifyParentProperty(true)]
		[DefaultValue(true)]
		[Category("Appearance")]
		public virtual bool EnableEmbeddedScripts
		{
			get
			{
				if (this.ViewState["EnableEmbeddedScripts"] == null)
				{
					return BaseClass.GetGlobalEnableEmbeddedScripts(this);
				}
				return (bool)this.ViewState["EnableEmbeddedScripts"];
			}
			set
			{
				this.ViewState["EnableEmbeddedScripts"] = value;
			}
		}

		// Token: 0x170031BB RID: 12731
		// (get) Token: 0x06009D2E RID: 40238 RVA: 0x0022FFEC File Offset: 0x0022E1EC
		// (set) Token: 0x06009D2F RID: 40239 RVA: 0x00230003 File Offset: 0x0022E203
		[TypeConverter(typeof(StringArrayConverter))]
		[Description("String array with filter strings. Ajax trigger control whose ID matches one of these values will perform a synchronous request.")]
		[DefaultValue(null)]
		[Category("Behavior")]
		[NotifyParentProperty(true)]
		public virtual string[] PostBackControls
		{
			get
			{
				return this.ViewState["PostBackControls"] as string[];
			}
			set
			{
				this.ViewState["PostBackControls"] = value;
			}
		}

		// Token: 0x170031BC RID: 12732
		// (get) Token: 0x06009D30 RID: 40240 RVA: 0x00230016 File Offset: 0x0022E216
		// (set) Token: 0x06009D31 RID: 40241 RVA: 0x00230041 File Offset: 0x0022E241
		[DefaultValue(false)]
		[Category("Appearance")]
		[Description("Determines whether the loading panel will be shown during a regular postback. This will work only if the loading panel is attached to the ajax control. Default value is false (disabled).")]
		[NotifyParentProperty(true)]
		public virtual bool ShowLoadingPanelForPostBackControls
		{
			get
			{
				return this.ViewState["ShowLoadingPanelForPostBackControls"] != null && (bool)this.ViewState["ShowLoadingPanelForPostBackControls"];
			}
			set
			{
				if (value && this.RequestQueueSize < 2)
				{
					this.RequestQueueSize = 2;
				}
				this.ViewState["ShowLoadingPanelForPostBackControls"] = value;
			}
		}

		// Token: 0x14000175 RID: 373
		// (add) Token: 0x06009D32 RID: 40242 RVA: 0x0023006C File Offset: 0x0022E26C
		// (remove) Token: 0x06009D33 RID: 40243 RVA: 0x002300A4 File Offset: 0x0022E2A4
		[Category("Action")]
		public event RadAjaxControl.AjaxSettingCreatingDelegate AjaxSettingCreating;

		// Token: 0x06009D34 RID: 40244 RVA: 0x002300D9 File Offset: 0x0022E2D9
		protected virtual void OnAjaxSettingCreating(AjaxSettingCreatingEventArgs args)
		{
			if (this.AjaxSettingCreating != null)
			{
				this.AjaxSettingCreating(this, args);
			}
		}

		// Token: 0x14000176 RID: 374
		// (add) Token: 0x06009D35 RID: 40245 RVA: 0x002300F0 File Offset: 0x0022E2F0
		// (remove) Token: 0x06009D36 RID: 40246 RVA: 0x00230128 File Offset: 0x0022E328
		[Category("Action")]
		public event RadAjaxControl.AjaxSettingCreatedDelegate AjaxSettingCreated;

		// Token: 0x06009D37 RID: 40247 RVA: 0x0023015D File Offset: 0x0022E35D
		protected virtual void OnAjaxSettingCreated(AjaxSettingCreatedEventArgs args)
		{
			if (this.AjaxSettingCreated != null)
			{
				this.AjaxSettingCreated(this, args);
			}
		}

		// Token: 0x14000177 RID: 375
		// (add) Token: 0x06009D38 RID: 40248 RVA: 0x00230174 File Offset: 0x0022E374
		// (remove) Token: 0x06009D39 RID: 40249 RVA: 0x002301AC File Offset: 0x0022E3AC
		[Category("Action")]
		public event RadAjaxControl.AjaxRequestDelegate AjaxRequest;

		// Token: 0x06009D3A RID: 40250 RVA: 0x002301E1 File Offset: 0x0022E3E1
		protected virtual void OnAjaxRequest(AjaxRequestEventArgs args)
		{
			if (this is RadAjaxPanel)
			{
				this.isExplicitUpdate = true;
			}
			if (this.AjaxRequest != null)
			{
				this.AjaxRequest(this, args);
			}
		}

		// Token: 0x14000178 RID: 376
		// (add) Token: 0x06009D3B RID: 40251 RVA: 0x00230208 File Offset: 0x0022E408
		// (remove) Token: 0x06009D3C RID: 40252 RVA: 0x00230240 File Offset: 0x0022E440
		[Category("Action")]
		public event RadAjaxControl.CommandEventDelegate Command;

		// Token: 0x06009D3D RID: 40253 RVA: 0x00230275 File Offset: 0x0022E475
		protected virtual void OnCommand(CommandEventArgs args)
		{
			if (this.Command != null)
			{
				this.Command(this, args);
			}
		}

		// Token: 0x06009D3E RID: 40254 RVA: 0x0023028C File Offset: 0x0022E48C
		public void RaisePostBackEvent(string eventArgument)
		{
			this.OnAjaxRequest(new AjaxRequestEventArgs(eventArgument));
		}

		// Token: 0x06009D3F RID: 40255 RVA: 0x0023029C File Offset: 0x0022E49C
		protected override bool OnBubbleEvent(object source, EventArgs args)
		{
			CommandEventArgs commandEventArgs = args as CommandEventArgs;
			if (commandEventArgs != null)
			{
				this.OnCommand(commandEventArgs);
				return true;
			}
			return false;
		}

		// Token: 0x170031BD RID: 12733
		// (get) Token: 0x06009D40 RID: 40256 RVA: 0x002302BD File Offset: 0x0022E4BD
		// (set) Token: 0x06009D41 RID: 40257 RVA: 0x002302E8 File Offset: 0x0022E4E8
		[Category("Behavior")]
		[DefaultValue(true)]
		[NotifyParentProperty(true)]
		public virtual bool RestoreOriginalRenderDelegate
		{
			get
			{
				return this.ViewState["rord"] == null || (bool)this.ViewState["rord"];
			}
			set
			{
				this.ViewState["rord"] = value;
			}
		}

		// Token: 0x170031BE RID: 12734
		// (get) Token: 0x06009D42 RID: 40258 RVA: 0x00230300 File Offset: 0x0022E500
		// (set) Token: 0x06009D43 RID: 40259 RVA: 0x0023032B File Offset: 0x0022E52B
		[DefaultValue(true)]
		[Category("Behavior")]
		[NotifyParentProperty(true)]
		public virtual bool EnableAJAX
		{
			get
			{
				return this.ViewState["EnableAJAX"] == null || (bool)this.ViewState["EnableAJAX"];
			}
			set
			{
				this.ViewState["EnableAJAX"] = value;
			}
		}

		// Token: 0x170031BF RID: 12735
		// (get) Token: 0x06009D44 RID: 40260 RVA: 0x00230343 File Offset: 0x0022E543
		// (set) Token: 0x06009D45 RID: 40261 RVA: 0x0023036E File Offset: 0x0022E56E
		[DefaultValue(false)]
		[Category("Behavior")]
		[NotifyParentProperty(true)]
		public virtual bool EnableHistory
		{
			get
			{
				return this.ViewState["EnableHistory"] != null && (bool)this.ViewState["EnableHistory"];
			}
			set
			{
				this.ViewState["EnableHistory"] = value;
			}
		}

		// Token: 0x170031C0 RID: 12736
		// (get) Token: 0x06009D46 RID: 40262 RVA: 0x00230386 File Offset: 0x0022E586
		// (set) Token: 0x06009D47 RID: 40263 RVA: 0x002303A7 File Offset: 0x0022E5A7
		[Description("When set to true enables support for WAI-ARIA")]
		[DefaultValue(false)]
		[Category("Behavior")]
		public bool EnableAriaSupport
		{
			get
			{
				return (bool)(this.ViewState["EnableAriaSupport"] ?? false);
			}
			set
			{
				this.ViewState["EnableAriaSupport"] = value;
			}
		}

		// Token: 0x170031C1 RID: 12737
		// (get) Token: 0x06009D48 RID: 40264 RVA: 0x002303BF File Offset: 0x0022E5BF
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public virtual StringCollection ResponseScripts
		{
			get
			{
				if (this.responseScripts == null)
				{
					this.responseScripts = new StringCollection();
				}
				return this.responseScripts;
			}
		}

		// Token: 0x170031C2 RID: 12738
		// (get) Token: 0x06009D49 RID: 40265 RVA: 0x002303DA File Offset: 0x0022E5DA
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[NotifyParentProperty(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[Category("Client")]
		public virtual AjaxClientEvents ClientEvents
		{
			get
			{
				if (this.clientEvents == null)
				{
					this.clientEvents = new AjaxClientEvents(this.ViewState);
				}
				return this.clientEvents;
			}
		}

		// Token: 0x170031C3 RID: 12739
		// (get) Token: 0x06009D4A RID: 40266 RVA: 0x002303FB File Offset: 0x0022E5FB
		// (set) Token: 0x06009D4B RID: 40267 RVA: 0x00230403 File Offset: 0x0022E603
		[Description("This property is overridden in order to support controls which implement INamingContainer")]
		[NotifyParentProperty(true)]
		[DefaultValue(ClientIDMode.AutoID)]
		public override ClientIDMode ClientIDMode
		{
			get
			{
				return this.ClientIDModeValue;
			}
			set
			{
				if (this.ClientIDModeValue != value)
				{
					base.ClearEffectiveClientIDMode();
					base.ClearCachedClientID();
				}
				this.ClientIDModeValue = value;
			}
		}

		// Token: 0x170031C4 RID: 12740
		// (get) Token: 0x06009D4C RID: 40268 RVA: 0x00230424 File Offset: 0x0022E624
		// (set) Token: 0x06009D4D RID: 40269 RVA: 0x0023044D File Offset: 0x0022E64D
		[Category("AJAX")]
		[NotifyParentProperty(true)]
		[DefaultValue(true)]
		public virtual bool EnablePageHeadUpdate
		{
			get
			{
				object obj = this.ViewState["_eph"];
				return obj == null || (bool)obj;
			}
			set
			{
				this.ViewState["_eph"] = value;
			}
		}

		// Token: 0x170031C5 RID: 12741
		// (get) Token: 0x06009D4E RID: 40270 RVA: 0x00230468 File Offset: 0x0022E668
		// (set) Token: 0x06009D4F RID: 40271 RVA: 0x00230493 File Offset: 0x0022E693
		[NotifyParentProperty(true)]
		[Category("AJAX")]
		[Description("Enables the queuing mechanism of RadAjax that allows it to complete the ongoing request and then initiate the pending requests in its queue.")]
		[DefaultValue(0)]
		public int RequestQueueSize
		{
			get
			{
				int result = 0;
				object obj = this.ViewState["QueueSize"];
				if (obj != null)
				{
					return (int)obj;
				}
				return result;
			}
			set
			{
				this.ViewState["QueueSize"] = value;
			}
		}

		// Token: 0x170031C6 RID: 12742
		// (get) Token: 0x06009D50 RID: 40272 RVA: 0x002304AB File Offset: 0x0022E6AB
		// (set) Token: 0x06009D51 RID: 40273 RVA: 0x002304B3 File Offset: 0x0022E6B3
		internal string PostbackTriggerEventName { get; set; }

		// Token: 0x170031C7 RID: 12743
		// (get) Token: 0x06009D52 RID: 40274 RVA: 0x002304BC File Offset: 0x0022E6BC
		// (set) Token: 0x06009D53 RID: 40275 RVA: 0x002304C4 File Offset: 0x0022E6C4
		internal string PostbackTriggerInitiatorUniqueID { get; set; }

		// Token: 0x06009D54 RID: 40276 RVA: 0x002304D0 File Offset: 0x0022E6D0
		public void Redirect(string location)
		{
			if (this.IsAjaxRequest)
			{
				string arg = location;
				if (this.Page != null && this.Page.Response != null)
				{
					arg = this.Page.Response.ApplyAppPathModifier(location);
				}
				this.ResponseScripts.Add(string.Format("window.location.href = '{0}';", arg));
				return;
			}
			if (this.Page != null && this.Page.Response != null)
			{
				this.Page.Response.Redirect(location);
			}
		}

		// Token: 0x06009D55 RID: 40277 RVA: 0x0023054C File Offset: 0x0022E74C
		public void Alert(string message)
		{
			string value = Regex.Replace(string.Format("alert(\"{0}\");", message), "((\r)?\n(\r)?)", "\\n");
			this.ResponseScripts.Add(value);
		}

		// Token: 0x06009D56 RID: 40278 RVA: 0x00230581 File Offset: 0x0022E781
		public string GetAjaxEventReference(string argument)
		{
			return string.Format("$find(\"{0}\").ajaxRequest(\"{1}\");", this.ClientID, argument);
		}

		// Token: 0x06009D57 RID: 40279 RVA: 0x00230594 File Offset: 0x0022E794
		public virtual void FocusControl(Control controlToFocus)
		{
			this.FocusControl(controlToFocus.ClientID);
		}

		// Token: 0x06009D58 RID: 40280 RVA: 0x002305A2 File Offset: 0x0022E7A2
		public virtual void FocusControl(string controlToFocusID)
		{
			this.ResponseScripts.Add(string.Format("Telerik.Web.UI.RadAjaxControl.FocusElement(\"{0}\");", controlToFocusID));
		}

		// Token: 0x06009D59 RID: 40281 RVA: 0x002305BB File Offset: 0x0022E7BB
		internal Control FindControlRecursive(string ID)
		{
			return ChildControlHelper.FindControlRecursive(this, ID, null);
		}

		// Token: 0x06009D5A RID: 40282 RVA: 0x002305C8 File Offset: 0x0022E7C8
		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
			if (ScriptManager.GetCurrent(this.Page) == null)
			{
				throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, "The control with ID '{0}' requires a ScriptManager on the page. The ScriptManager must appear before any controls that need it.", new object[]
				{
					this.ID
				}));
			}
			this.AttachHandlersToEvents();
		}

		// Token: 0x06009D5B RID: 40283 RVA: 0x00230618 File Offset: 0x0022E818
		private void AttachHandlersToEvents()
		{
			AjaxSettingsCollection ajaxSettingsCollection = new AjaxSettingsCollection();
			RadAjaxManager radAjaxManager = this as RadAjaxManager;
			if (radAjaxManager != null)
			{
				RadAjaxControl.PopulateAjaxSetings(ajaxSettingsCollection, radAjaxManager);
				foreach (object obj in ajaxSettingsCollection)
				{
					AjaxSetting ajaxSetting = (AjaxSetting)obj;
					if (!string.IsNullOrEmpty(ajaxSetting.EventName))
					{
						this.AttachTriggers(ajaxSetting);
					}
				}
			}
		}

		// Token: 0x06009D5C RID: 40284 RVA: 0x00230694 File Offset: 0x0022E894
		internal void AttachTriggers(AjaxSetting setting)
		{
			Control control = this.FindControlRecursive(setting.AjaxControlID);
			if (control != null && !(control is RadAjaxPanel))
			{
				EventInfo @event = control.GetType().GetEvent(setting.EventName, BindingFlags.IgnoreCase | BindingFlags.Instance | BindingFlags.Public);
				if (@event != null)
				{
					MethodInfo method = @event.EventHandlerType.GetMethod("Invoke");
					ParameterInfo[] parameters = method.GetParameters();
					Type[] array = new Type[parameters.Length];
					for (int i = 0; i < parameters.Length; i++)
					{
						array[i] = parameters[i].ParameterType;
					}
					if (method.ReturnType.Equals(typeof(void)) && parameters.Length == 2 && typeof(EventArgs).IsAssignableFrom(parameters[1].ParameterType))
					{
						RadAjaxAsyncPostbackTrigger firstArgument = new RadAjaxAsyncPostbackTrigger(this, setting.EventName);
						Delegate handler = Delegate.CreateDelegate(@event.EventHandlerType, firstArgument, this.EventHandler);
						@event.AddEventHandler(control, handler);
					}
				}
			}
		}

		// Token: 0x170031C8 RID: 12744
		// (get) Token: 0x06009D5D RID: 40285 RVA: 0x00230783 File Offset: 0x0022E983
		private MethodInfo EventHandler
		{
			get
			{
				if (this._eventHandler == null)
				{
					this._eventHandler = typeof(RadAjaxAsyncPostbackTrigger).GetMethod("OnEvent");
				}
				return this._eventHandler;
			}
		}

		// Token: 0x06009D5E RID: 40286 RVA: 0x002307B4 File Offset: 0x0022E9B4
		private static void PopulateAjaxSetings(AjaxSettingsCollection ajaxSettings, RadAjaxManager thisManager)
		{
			foreach (object obj in thisManager.AjaxSettings)
			{
				AjaxSetting ajaxSetting = (AjaxSetting)obj;
				ajaxSettings.Add(ajaxSetting);
			}
		}

		// Token: 0x06009D5F RID: 40287 RVA: 0x00230810 File Offset: 0x0022EA10
		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);
			if (this.Page != null && ScriptManager.GetCurrent(this.Page) != null && ScriptManager.GetCurrent(this.Page).EnablePartialRendering)
			{
				this.Page.InitComplete += this.OnPageInit;
				this.Page.PreRender += this.OnPagePreRender;
				if (this.RestoreOriginalRenderDelegate)
				{
					this.Page.PreRenderComplete += this.OnPagePreRenderComplete;
					return;
				}
				this.Page.SetRenderMethodDelegate(new RenderMethod(this.RenderPageInAjaxMode));
			}
		}

		// Token: 0x06009D60 RID: 40288 RVA: 0x002308B8 File Offset: 0x0022EAB8
		internal virtual void OnPageInit(object sender, EventArgs e)
		{
			this.Page.InitComplete -= this.OnPageInit;
			if (this is RadAjaxManager)
			{
				if (RadAjaxManager.GetCurrent(this.Page) != null)
				{
					throw new InvalidOperationException("Only one instance of a RadAjaxManager can be added to the page!");
				}
				this.Page.Items[typeof(RadAjaxManager)] = this;
			}
		}

		// Token: 0x06009D61 RID: 40289 RVA: 0x00230918 File Offset: 0x0022EB18
		internal virtual void OnPagePreRenderComplete(object sender, EventArgs e)
		{
			if (!this.EnableAJAX)
			{
				return;
			}
			this.tooLateForAjaxification = true;
			this.Page.PreRenderComplete -= this.OnPagePreRenderComplete;
			this.AttachOnRender();
		}

		// Token: 0x06009D62 RID: 40290 RVA: 0x00230948 File Offset: 0x0022EB48
		internal void AttachOnRender()
		{
			if (!this.RestoreOriginalRenderDelegate)
			{
				this.Page.SetRenderMethodDelegate(new RenderMethod(this.RenderPageInAjaxMode));
				return;
			}
			if (RadAjaxControl.HasReflectionPermission())
			{
				this.ReadOriginalRenderMethod();
				this.Page.SetRenderMethodDelegate(new RenderMethod(this.RenderPageInAjaxMode));
				return;
			}
			if (this.Page is IRadAjaxPage)
			{
				IRadAjaxPage radAjaxPage = this.Page as IRadAjaxPage;
				radAjaxPage.AttachOnRender(new RenderMethod(this.OnPageRender));
				return;
			}
			throw new InvalidOperationException("Not enough permissions.\r\nInherit your page class from RadAjaxPage if you are running under Medium trust level.");
		}

		// Token: 0x06009D63 RID: 40291 RVA: 0x00230CEC File Offset: 0x0022EEEC
		private IEnumerable<Control> EnumerateControlsBFSRecursive(Control parent)
		{
			foreach (object obj in parent.Controls)
			{
				Control child = (Control)obj;
				yield return child;
			}
			foreach (object obj2 in parent.Controls)
			{
				Control child2 = (Control)obj2;
				foreach (Control descendant in this.EnumerateControlsBFSRecursive(child2))
				{
					yield return descendant;
				}
			}
			yield break;
		}

		// Token: 0x06009D64 RID: 40292 RVA: 0x00230D10 File Offset: 0x0022EF10
		internal virtual void OnPagePreRender(object sender, EventArgs e)
		{
			this.Page.PreRender -= this.OnPagePreRender;
			if (!this.EnableAJAX)
			{
				return;
			}
			RadAjaxManager radAjaxManager = this as RadAjaxManager;
			if (radAjaxManager != null)
			{
				foreach (KeyValuePair<string, RadAjaxManagerProxy> keyValuePair in radAjaxManager.proxies)
				{
					foreach (object obj in keyValuePair.Value.ajaxSettings)
					{
						AjaxSetting ajaxSetting = (AjaxSetting)obj;
						Control namingContainer = keyValuePair.Value.NamingContainer;
						if (namingContainer != null)
						{
							Control control = namingContainer.FindControl(ajaxSetting.AjaxControlID);
							if (control != null)
							{
								ajaxSetting.AjaxControlID = control.UniqueID;
							}
							foreach (object obj2 in ajaxSetting.UpdatedControls)
							{
								AjaxUpdatedControl ajaxUpdatedControl = (AjaxUpdatedControl)obj2;
								Control control2 = namingContainer.FindControl(ajaxUpdatedControl.ControlID);
								if (control2 != null)
								{
									ajaxUpdatedControl.ControlID = control2.UniqueID;
								}
							}
						}
						this.ajaxSettings.Add(ajaxSetting);
					}
				}
			}
			RadAjaxPanel radAjaxPanel = this as RadAjaxPanel;
			if (radAjaxPanel != null)
			{
				if (this.ajaxSettings == null)
				{
					this.ajaxSettings = new AjaxSettingsCollection();
				}
				AjaxSetting ajaxSetting2 = radAjaxPanel.GetAjaxSetting();
				if (ajaxSetting2 != null)
				{
					this.ajaxSettings.Add(ajaxSetting2);
				}
			}
			Dictionary<string, List<AjaxSetting>> dictionary = new Dictionary<string, List<AjaxSetting>>();
			bool flag = radAjaxManager != null && radAjaxManager.UpdateInitiatorPanelsOnly;
			if (ProxyScriptControl.GetKeepOriginalOrderOfScriptDescriptorsDuringAjax())
			{
				List<AjaxUpdatedControl> list = new List<AjaxUpdatedControl>();
				foreach (object obj3 in this.ajaxSettings)
				{
					AjaxSetting ajaxSetting3 = (AjaxSetting)obj3;
					foreach (object obj4 in ajaxSetting3.UpdatedControls)
					{
						AjaxUpdatedControl ajaxUpdatedControl2 = (AjaxUpdatedControl)obj4;
						ajaxUpdatedControl2.OwnerSetting = ajaxSetting3;
						bool flag2 = false;
						foreach (AjaxUpdatedControl ajaxUpdatedControl3 in list)
						{
							if (ajaxUpdatedControl3.ControlID == ajaxUpdatedControl2.ControlID)
							{
								flag2 = true;
								break;
							}
						}
						if (!flag2)
						{
							list.Add(ajaxUpdatedControl2);
						}
					}
				}
				int num = 0;
				foreach (Control control3 in this.EnumerateControlsBFSRecursive(this.Page))
				{
					int i = 0;
					while (i < list.Count)
					{
						if (control3.UniqueID == list[i].ControlID)
						{
							if (num <= i)
							{
								if (num < i)
								{
									AjaxUpdatedControl value = list[num];
									list[num] = list[i];
									list[i] = value;
								}
								Control initiator = this.FindControlRecursive(list[num].OwnerSetting.AjaxControlID);
								this.CreateUpdatePanel(initiator, list[num].OwnerSetting.EventName, control3, list[num].UpdatePanelRenderMode, list[num].UpdatePanelHeight, list[num].UpdatePanelCssClass);
								num++;
								break;
							}
							break;
						}
						else
						{
							i++;
						}
					}
				}
			}
			foreach (object obj5 in this.ajaxSettings)
			{
				AjaxSetting ajaxSetting4 = (AjaxSetting)obj5;
				Control control4 = this.FindControlRecursive(ajaxSetting4.AjaxControlID);
				if (control4 != null)
				{
					this.PopulatePlainPanels(control4, this.plainPanelsClientIDs, control4);
				}
				foreach (object obj6 in ajaxSetting4.UpdatedControls)
				{
					AjaxUpdatedControl ajaxUpdatedControl4 = (AjaxUpdatedControl)obj6;
					if (!string.IsNullOrEmpty(ajaxUpdatedControl4.ControlID))
					{
						Control control5 = this.FindControlRecursive(ajaxUpdatedControl4.ControlID);
						this.CreateUpdatePanel(control4, ajaxSetting4.EventName, control5, ajaxUpdatedControl4.UpdatePanelRenderMode, ajaxUpdatedControl4.UpdatePanelHeight, ajaxUpdatedControl4.UpdatePanelCssClass);
						if (control5 != null)
						{
							this.PopulatePlainPanels(control5, this.plainPanelsClientIDs, control5);
						}
					}
				}
				if (flag)
				{
					if (!dictionary.ContainsKey(ajaxSetting4.AjaxControlID))
					{
						dictionary.Add(ajaxSetting4.AjaxControlID, new List<AjaxSetting>());
					}
					dictionary[ajaxSetting4.AjaxControlID].Add(ajaxSetting4);
				}
			}
			if (flag && this.Page != null && this.Context != null)
			{
				List<AjaxSetting> list2 = null;
				for (Control control6 = this.GetPostBackControl(this.Page); control6 != null; control6 = control6.Parent)
				{
					if (control6.UniqueID != null && dictionary.ContainsKey(control6.UniqueID))
					{
						list2 = dictionary[control6.UniqueID];
						break;
					}
					if (control6.ID != null && dictionary.ContainsKey(control6.ID))
					{
						list2 = dictionary[control6.ID];
						break;
					}
				}
				if (list2 != null)
				{
					Dictionary<string, OurUpdatePanel> dictionary2 = (Dictionary<string, OurUpdatePanel>)this.Page.Items["AllUpdatePanels"];
					foreach (KeyValuePair<string, OurUpdatePanel> keyValuePair2 in dictionary2)
					{
						keyValuePair2.Value.Update();
						keyValuePair2.Value.ShouldUpdate = false;
					}
					foreach (AjaxSetting ajaxSetting5 in list2)
					{
						foreach (object obj7 in ajaxSetting5.UpdatedControls)
						{
							AjaxUpdatedControl ajaxUpdatedControl5 = (AjaxUpdatedControl)obj7;
							Control updated = this.FindControlRecursive(ajaxUpdatedControl5.ControlID);
							OurUpdatePanel updatePanel = this.GetUpdatePanel(updated, ajaxUpdatedControl5.UpdatePanelHeight, ajaxUpdatedControl5.UpdatePanelCssClass);
							updatePanel.Update();
						}
					}
				}
			}
			this.performImmediateAjaxification = true;
		}

		// Token: 0x06009D65 RID: 40293 RVA: 0x002314B8 File Offset: 0x0022F6B8
		private Control GetPostBackControl(Page page)
		{
			Control result = null;
			if (page.Request != null)
			{
				string text = page.Request.Params.Get("__EVENTTARGET");
				if (text != null && text != string.Empty)
				{
					result = this.FindControlRecursive(text);
				}
				else
				{
					foreach (object obj in page.Request.Form)
					{
						string id = (string)obj;
						Control control = page.FindControl(id);
						if (control is Button)
						{
							result = control;
							break;
						}
					}
				}
			}
			return result;
		}

		// Token: 0x06009D66 RID: 40294 RVA: 0x00231568 File Offset: 0x0022F768
		private void PopulatePlainPanels(Control parent, List<string> list, Control root)
		{
			if (!parent.Visible)
			{
				return;
			}
			foreach (object obj in parent.Controls)
			{
				Control control = (Control)obj;
				UpdatePanel updatePanel = control as UpdatePanel;
				if (updatePanel != null && !(control is OurUpdatePanel) && control.ID != string.Format("{0}SU", this.ID) && control.Visible && control.Parent != RadAjaxManager.GetCurrent(this.Page))
				{
					if (!this.plainPanelsClientIDs.Contains(control.ClientID))
					{
						this.plainPanelsClientIDs.Add(control.ClientID);
					}
					foreach (UpdatePanelTrigger updatePanelTrigger in updatePanel.Triggers)
					{
						UpdatePanelControlTrigger updatePanelControlTrigger = (UpdatePanelControlTrigger)updatePanelTrigger;
						if (updatePanelControlTrigger is AsyncPostBackTrigger && !this.plainPanelsClientIDs.Contains(updatePanelControlTrigger.ControlID))
						{
							this.plainPanelsClientIDs.Add(updatePanelControlTrigger.ControlID);
						}
					}
				}
				if (control.HasControls())
				{
					this.PopulatePlainPanels(control, list, root);
				}
			}
		}

		// Token: 0x06009D67 RID: 40295 RVA: 0x002316E0 File Offset: 0x0022F8E0
		private void ReadOriginalRenderMethod()
		{
			try
			{
				FieldInfo fieldInfo;
				try
				{
					fieldInfo = typeof(Control).GetField("_renderMethodDelegate", BindingFlags.Instance | BindingFlags.NonPublic);
					if (fieldInfo != null)
					{
						this.originalRenderDelegate = (RenderMethod)fieldInfo.GetValue(this.Page);
					}
				}
				catch (Exception)
				{
					fieldInfo = null;
				}
				if (fieldInfo == null)
				{
					PropertyInfo property = typeof(Control).GetProperty("RareFieldsEnsured", BindingFlags.Instance | BindingFlags.NonPublic);
					object value = property.GetValue(this.Page, null);
					fieldInfo = value.GetType().GetField("RenderMethod");
					this.originalRenderDelegate = (RenderMethod)fieldInfo.GetValue(value);
				}
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x06009D68 RID: 40296 RVA: 0x002317A0 File Offset: 0x0022F9A0
		internal static bool HasReflectionPermission()
		{
			bool result;
			try
			{
				ReflectionPermission reflectionPermission = new ReflectionPermission(ReflectionPermissionFlag.MemberAccess);
				reflectionPermission.Demand();
				result = true;
			}
			catch (SecurityException)
			{
				result = false;
			}
			return result;
		}

		// Token: 0x06009D69 RID: 40297 RVA: 0x002317D4 File Offset: 0x0022F9D4
		internal void CallOnPageRender(HtmlTextWriter writer, Control page)
		{
			this.OnPageRender(writer, page);
		}

		// Token: 0x06009D6A RID: 40298 RVA: 0x002317E0 File Offset: 0x0022F9E0
		internal void RenderPageInAjaxMode(HtmlTextWriter writer, Control page)
		{
			if (this.RestoreOriginalRenderDelegate)
			{
				page.SetRenderMethodDelegate(this.originalRenderDelegate);
				this.OnPageRender(writer, page);
			}
			else
			{
				page.SetRenderMethodDelegate(null);
				foreach (KeyValuePair<string, OurUpdatePanel> keyValuePair in this.updatePanels)
				{
					if (keyValuePair.Value.Visible)
					{
						keyValuePair.Value.AjaxControl.CallOnPageRender(writer, page);
					}
				}
			}
			if (RadAjaxControl.HasReflectionPermission())
			{
				foreach (object obj in this.ajaxSettings)
				{
					AjaxSetting ajaxSetting = (AjaxSetting)obj;
					Control control = this.FindControlRecursive(ajaxSetting.AjaxControlID);
					if (control != null)
					{
						this.SetParent(control);
					}
					foreach (object obj2 in ajaxSetting.UpdatedControls)
					{
						AjaxUpdatedControl ajaxUpdatedControl = (AjaxUpdatedControl)obj2;
						if (!string.IsNullOrEmpty(ajaxUpdatedControl.ControlID))
						{
							Control control2 = this.FindControlRecursive(ajaxUpdatedControl.ControlID);
							if (control2 != null)
							{
								this.SetParent(control2);
							}
						}
					}
				}
			}
			page.RenderControl(writer);
		}

		// Token: 0x06009D6B RID: 40299 RVA: 0x00231958 File Offset: 0x0022FB58
		private void SetParent(Control parent)
		{
			if (!parent.Visible)
			{
				return;
			}
			foreach (object obj in parent.Controls)
			{
				Control control = (Control)obj;
				if (control is UpdatePanel && !(control is OurUpdatePanel) && control.ID != string.Format("{0}SU", this.ID) && control.Visible && control.Parent != RadAjaxManager.GetCurrent(this.Page))
				{
					FieldInfo field = typeof(Control).GetField("_parent", BindingFlags.Instance | BindingFlags.NonPublic);
					field.SetValue(control, this.Page.Form);
				}
				if (control.HasControls())
				{
					this.SetParent(control);
				}
			}
		}

		// Token: 0x06009D6C RID: 40300 RVA: 0x00231A3C File Offset: 0x0022FC3C
		private bool IsChildOfInitiatorOrUpdated(Control control)
		{
			foreach (object obj in this.ajaxSettings)
			{
				AjaxSetting ajaxSetting = (AjaxSetting)obj;
				Control control2 = this.FindControlRecursive(ajaxSetting.AjaxControlID);
				if (control2 == control)
				{
					return true;
				}
				if (control2 != null && this.IsChildOf(control, control2))
				{
					return true;
				}
				foreach (object obj2 in ajaxSetting.UpdatedControls)
				{
					AjaxUpdatedControl ajaxUpdatedControl = (AjaxUpdatedControl)obj2;
					if (!string.IsNullOrEmpty(ajaxUpdatedControl.ControlID))
					{
						Control control3 = this.FindControlRecursive(ajaxUpdatedControl.ControlID);
						if (control3 == control)
						{
							return true;
						}
						if (control3 != null && this.IsChildOf(control, control3))
						{
							return true;
						}
					}
				}
			}
			return false;
		}

		// Token: 0x06009D6D RID: 40301 RVA: 0x00231B4C File Offset: 0x0022FD4C
		private bool IsChildOf(Control control, Control parent)
		{
			while (control != null)
			{
				if (control == parent)
				{
					return true;
				}
				control = control.Parent;
			}
			return false;
		}

		// Token: 0x170031C9 RID: 12745
		// (get) Token: 0x06009D6E RID: 40302 RVA: 0x00231B64 File Offset: 0x0022FD64
		[Browsable(false)]
		public virtual bool IsAjaxRequest
		{
			get
			{
				return this.Context != null && (this.Context.Request.Form["RadAJAXControlID"] == this.ClientID || this.Context.Request.Form["RadAJAXControlID"] == this.UniqueID.Replace("$", "_"));
			}
		}

		// Token: 0x06009D6F RID: 40303 RVA: 0x00231BDC File Offset: 0x0022FDDC
		private void OnPageRender(HtmlTextWriter writer, Control page)
		{
			if (this.Page.Header != null && this.IsAjaxRequest && this.EnablePageHeadUpdate)
			{
				HtmlTextWriter htmlTextWriter = new HtmlTextWriter(new StringWriter());
				this.Page.Header.RenderControl(htmlTextWriter);
				string input = htmlTextWriter.InnerWriter.ToString();
				MatchCollection matchCollection = RadAjaxControl.linkTagsMatcher.Matches(input);
				foreach (object obj in matchCollection)
				{
					Match match = (Match)obj;
					string str = (match.Groups["mediaBefore"] != null) ? match.Groups["mediaBefore"].Value : "";
					string str2 = (match.Groups["mediaAfter"] != null) ? match.Groups["mediaAfter"].Value : "";
					string value = (match.Groups["href"] != null) ? match.Groups["href"].Value : "";
					if ((str + str2).ToLower().IndexOf("print") < 0)
					{
						this._linksToAppend.Add(value);
					}
				}
				MatchCollection matchCollection2 = RadAjaxControl.styleTagsMatcher.Matches(input);
				foreach (object obj2 in matchCollection2)
				{
					Match match2 = (Match)obj2;
					string text = (match2.Groups["attributes"] != null) ? match2.Groups["attributes"].Value : "";
					string value2 = (match2.Groups["content"] != null) ? match2.Groups["content"].Value : "";
					bool flag = text.ToLower().IndexOf("print") >= 0;
					bool flag2 = text.ToLower().Contains("id=\"spthemehideforms\"");
					if (!flag && !flag2)
					{
						this._stylesToAppend.Add(value2);
					}
				}
			}
			RadAjaxManager radAjaxManager = this as RadAjaxManager;
			if (this.IsAjaxRequest && radAjaxManager != null && radAjaxManager.selfUpdatePanel.UpdateMode == UpdatePanelUpdateMode.Conditional)
			{
				radAjaxManager.selfUpdatePanel.Update();
			}
			this.PerformRender();
		}

		// Token: 0x06009D70 RID: 40304 RVA: 0x00231E88 File Offset: 0x00230088
		internal void MoveUpdatePanel(Control initiator, Control updated)
		{
			if (updated == null || initiator == null)
			{
				return;
			}
			OurUpdatePanel updatePanel = this.GetUpdatePanel(updated);
			PreControlToAjaxify child = new PreControlToAjaxify(updatePanel);
			ControlCollection controls = updated.Parent.Controls;
			int num = controls.IndexOf(updated);
			try
			{
				controls.AddAt(num, child);
			}
			catch (HttpException innerException)
			{
				throw new HttpException("Please, see whether wrapping the code block, generating the exception, within RadCodeBlock resolves the error.", innerException);
			}
			PostControlToAjaxify child2 = new PostControlToAjaxify(updated);
			controls.AddAt(num + 2, child2);
		}

		// Token: 0x06009D71 RID: 40305 RVA: 0x00231EFC File Offset: 0x002300FC
		internal void CreateUpdatePanel(Control initiator, string eventName, Control updated)
		{
			this.CreateUpdatePanel(initiator, eventName, updated, UpdatePanelRenderMode.Block, Unit.Empty, "");
		}

		// Token: 0x06009D72 RID: 40306 RVA: 0x00231F14 File Offset: 0x00230114
		[SuppressMessage("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
		internal void CreateUpdatePanel(Control initiator, string eventName, Control updated, UpdatePanelRenderMode panelRenderMode, Unit panelHeight, string panelCssClass)
		{
			if (initiator == null || updated == null)
			{
				return;
			}
			if (updated is RadAjaxPanel && this.Page.FindControl(string.Format("{0}Panel", updated.UniqueID)) != null)
			{
				return;
			}
			if (updated is RadAjaxManager)
			{
				return;
			}
			if (initiator.HasControls())
			{
				this.SetUseSubmitBehaviorToChildButtons(initiator);
			}
			if (updated.HasControls())
			{
				this.SetUseSubmitBehaviorToChildButtons(updated);
			}
			Button button = initiator as Button;
			if (button != null)
			{
				button.UseSubmitBehavior = false;
			}
			ImageButton imageButton = initiator as ImageButton;
			bool flag = false;
			Control control = updated;
			if (this.Page.Items["AllUpdatePanels"] != null)
			{
				this.updatePanels = (Dictionary<string, OurUpdatePanel>)this.Page.Items["AllUpdatePanels"];
			}
			RadAjaxManager radAjaxManager = this as RadAjaxManager;
			bool flag2 = radAjaxManager != null && radAjaxManager.UpdateInitiatorPanelsOnly;
			while (control != null && !flag2)
			{
				if (control.UniqueID != null && this.updatePanels.ContainsKey(control.UniqueID))
				{
					flag = true;
					break;
				}
				control = control.Parent;
			}
			OurUpdatePanel updatePanel = this.GetUpdatePanel(updated, panelHeight, panelCssClass);
			if (updatePanel.RenderMode != UpdatePanelRenderMode.Inline)
			{
				updatePanel.RenderMode = ((panelRenderMode == UpdatePanelRenderMode.Inline) ? UpdatePanelRenderMode.Inline : UpdatePanelRenderMode.Block);
			}
			AjaxSettingCreatingEventArgs ajaxSettingCreatingEventArgs = new AjaxSettingCreatingEventArgs(initiator, updated, updatePanel);
			this.OnAjaxSettingCreating(ajaxSettingCreatingEventArgs);
			if (ajaxSettingCreatingEventArgs.Canceled)
			{
				if (this.updatePanels.ContainsKey(updated.UniqueID))
				{
					this.updatePanels.Remove(updated.UniqueID);
				}
				return;
			}
			if (ajaxSettingCreatingEventArgs.Updated != null && !string.IsNullOrEmpty(ajaxSettingCreatingEventArgs.Updated.UniqueID) && ajaxSettingCreatingEventArgs.Updated.UniqueID != updated.UniqueID)
			{
				if (this.updatePanels.ContainsKey(updated.UniqueID))
				{
					this.updatePanels.Remove(updated.UniqueID);
				}
				updated = ajaxSettingCreatingEventArgs.Updated;
				updatePanel = this.GetUpdatePanel(updated, panelHeight, panelCssClass);
			}
			OurUpdatePanel ourUpdatePanel = null;
			if (flag)
			{
				ourUpdatePanel = this.GetUpdatePanel(control, panelHeight, panelCssClass);
			}
			if ((initiator is INamingContainer || initiator is IPostBackDataHandler || initiator is IPostBackEventHandler) && !(initiator is RadAjaxPanel) && !(initiator is RadMultiPage))
			{
				AsyncPostBackTrigger asyncPostBackTrigger = new AsyncPostBackTrigger();
				asyncPostBackTrigger.ControlID = initiator.UniqueID;
				asyncPostBackTrigger.EventName = eventName;
				updatePanel.Triggers.Add(asyncPostBackTrigger);
			}
			if (updatePanel.Parent == null)
			{
				if (this.Page.FindControl(updated.UniqueID + "Panel") == null)
				{
					updatePanel.ID = updated.UniqueID + "Panel";
				}
				updatePanel.UpdateMode = UpdatePanelUpdateMode.Conditional;
				if (ourUpdatePanel == null)
				{
					this.Controls.Add(updatePanel);
				}
				else
				{
					ourUpdatePanel.ContentTemplateContainer.Controls.Add(updatePanel);
				}
				if (initiator is RadAjaxPanel && this.isExplicitUpdate)
				{
					updatePanel.Update();
				}
				if (!flag2 && !(initiator is RadAjaxPanel) && updatePanel.Triggers.Count == 0)
				{
					updatePanel.Update();
				}
			}
			if (!string.IsNullOrEmpty(this.PostbackTriggerEventName) && this.PostbackTriggerEventName == eventName && this.PostbackTriggerInitiatorUniqueID == initiator.UniqueID)
			{
				updatePanel.Update();
			}
			AjaxSettingCreatedEventArgs args = new AjaxSettingCreatedEventArgs(initiator, updated, updatePanel);
			this.OnAjaxSettingCreated(args);
		}

		// Token: 0x06009D73 RID: 40307 RVA: 0x00232238 File Offset: 0x00230438
		private bool IsChildOfPlainUpdatePanel(Control control)
		{
			while (control != null)
			{
				if (control is UpdatePanel && !(control is OurUpdatePanel))
				{
					return true;
				}
				control = control.Parent;
			}
			return false;
		}

		// Token: 0x06009D74 RID: 40308 RVA: 0x0023225C File Offset: 0x0023045C
		private void FindTriggersIdsForInnerPlainPanelsWithConditionalUpdateMode(Control parent, List<string> list)
		{
			foreach (object obj in parent.Controls)
			{
				Control control = (Control)obj;
				UpdatePanel updatePanel = control as UpdatePanel;
				if (updatePanel != null && !(control is OurUpdatePanel) && updatePanel.UpdateMode == UpdatePanelUpdateMode.Conditional)
				{
					foreach (UpdatePanelTrigger updatePanelTrigger in updatePanel.Triggers)
					{
						UpdatePanelControlTrigger updatePanelControlTrigger = (UpdatePanelControlTrigger)updatePanelTrigger;
						if (updatePanelControlTrigger is AsyncPostBackTrigger && !list.Contains(updatePanelControlTrigger.ControlID))
						{
							list.Add(updatePanelControlTrigger.ControlID);
						}
					}
				}
				if (control.HasControls())
				{
					this.FindTriggersIdsForInnerPlainPanelsWithConditionalUpdateMode(control, list);
				}
			}
		}

		// Token: 0x06009D75 RID: 40309 RVA: 0x00232344 File Offset: 0x00230544
		private void SetUseSubmitBehaviorToChildButtons(Control initiator)
		{
			foreach (object obj in initiator.Controls)
			{
				Control control = (Control)obj;
				Button button = control as Button;
				if (button != null && control.GetType().Name != "DataControlButton" && button.Enabled)
				{
					button.UseSubmitBehavior = false;
				}
				if (control.HasControls())
				{
					this.SetUseSubmitBehaviorToChildButtons(control);
				}
			}
		}

		// Token: 0x06009D76 RID: 40310 RVA: 0x002323D8 File Offset: 0x002305D8
		private OurUpdatePanel GetUpdatePanel(Control updated)
		{
			return this.GetUpdatePanel(updated, Unit.Empty, "");
		}

		// Token: 0x06009D77 RID: 40311 RVA: 0x002323EC File Offset: 0x002305EC
		private OurUpdatePanel GetUpdatePanel(Control updated, Unit panelHeight, string panelCssClass = "")
		{
			if (this.Page.Items["AllUpdatePanels"] == null)
			{
				this.Page.Items["AllUpdatePanels"] = this.updatePanels;
			}
			else
			{
				this.updatePanels = (Dictionary<string, OurUpdatePanel>)this.Page.Items["AllUpdatePanels"];
			}
			if (!this.updatePanels.ContainsKey(updated.UniqueID))
			{
				this.updatePanels[updated.UniqueID] = new OurUpdatePanel(updated, this)
				{
					Height = panelHeight,
					CssClass = panelCssClass
				};
			}
			return this.updatePanels[updated.UniqueID];
		}

		// Token: 0x06009D78 RID: 40312 RVA: 0x0023249C File Offset: 0x0023069C
		internal void PerformRender()
		{
			if (ScriptManager.GetCurrent(this.Page) == null)
			{
				return;
			}
			foreach (object obj in this.ajaxSettings)
			{
				AjaxSetting ajaxSetting = (AjaxSetting)obj;
				Control initiator = this.FindControlRecursive(ajaxSetting.AjaxControlID);
				foreach (object obj2 in ajaxSetting.UpdatedControls)
				{
					AjaxUpdatedControl ajaxUpdatedControl = (AjaxUpdatedControl)obj2;
					if (!string.IsNullOrEmpty(ajaxUpdatedControl.ControlID))
					{
						Control updated = this.FindControlRecursive(ajaxUpdatedControl.ControlID);
						this.MoveUpdatePanel(initiator, updated);
					}
				}
			}
			if (!base.DesignMode)
			{
				foreach (string str in this.ResponseScripts)
				{
					string text = "setTimeout(function(){" + str + "}, 0);";
					ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), text, text, true);
				}
			}
		}

		// Token: 0x04002C3F RID: 11327
		internal bool isExplicitUpdate;

		// Token: 0x04002C40 RID: 11328
		internal AjaxSettingsCollection ajaxSettings;

		// Token: 0x04002C43 RID: 11331
		private StringCollection responseScripts;

		// Token: 0x04002C44 RID: 11332
		private AjaxClientEvents clientEvents;

		// Token: 0x04002C45 RID: 11333
		private ClientIDMode ClientIDModeValue = ClientIDMode.AutoID;

		// Token: 0x04002C46 RID: 11334
		private MethodInfo _eventHandler;

		// Token: 0x04002C47 RID: 11335
		internal bool performImmediateAjaxification;

		// Token: 0x04002C48 RID: 11336
		internal bool tooLateForAjaxification;

		// Token: 0x04002C49 RID: 11337
		internal List<string> plainPanelsClientIDs = new List<string>();

		// Token: 0x04002C4A RID: 11338
		private RenderMethod originalRenderDelegate;

		// Token: 0x04002C4B RID: 11339
		private static readonly string styleTagsPattern = "<style(?<attributes>[^>]*?)>(?<content>(.|\\n|\\r)*?)</style>";

		// Token: 0x04002C4C RID: 11340
		private static Regex styleTagsMatcher = new Regex(RadAjaxControl.styleTagsPattern, RegexOptions.IgnoreCase | RegexOptions.Compiled);

		// Token: 0x04002C4D RID: 11341
		private static readonly string linkTagsPattern = "<link[^>]*(media=(\"|')(?<mediaBefore>[^\"']*)(\"|'))?[^>]*href=(\"|')(?<href>[^\"']*)(\"|')[^>]*(media=(\"|')(?<mediaAfter>[^\"']*)(\"|'))?[^>]*/?>";

		// Token: 0x04002C4E RID: 11342
		private static Regex linkTagsMatcher = new Regex(RadAjaxControl.linkTagsPattern, RegexOptions.IgnoreCase | RegexOptions.Compiled);

		// Token: 0x04002C4F RID: 11343
		internal ArrayList _stylesToAppend = new ArrayList();

		// Token: 0x04002C50 RID: 11344
		internal ArrayList _linksToAppend = new ArrayList();

		// Token: 0x04002C51 RID: 11345
		internal Dictionary<string, OurUpdatePanel> renderedPanels = new Dictionary<string, OurUpdatePanel>();

		// Token: 0x04002C52 RID: 11346
		private Dictionary<string, OurUpdatePanel> updatePanels = new Dictionary<string, OurUpdatePanel>();

		// Token: 0x02000FD1 RID: 4049
		// (Invoke) Token: 0x06009D7B RID: 40315
		[SuppressMessage("Microsoft.Design", "CA1034:NestedTypesShouldNotBeVisible")]
		public delegate void AjaxSettingCreatingDelegate(object sender, AjaxSettingCreatingEventArgs e);

		// Token: 0x02000FD2 RID: 4050
		// (Invoke) Token: 0x06009D7F RID: 40319
		[SuppressMessage("Microsoft.Design", "CA1034:NestedTypesShouldNotBeVisible")]
		public delegate void AjaxSettingCreatedDelegate(object sender, AjaxSettingCreatedEventArgs e);

		// Token: 0x02000FD3 RID: 4051
		// (Invoke) Token: 0x06009D83 RID: 40323
		[SuppressMessage("Microsoft.Design", "CA1034:NestedTypesShouldNotBeVisible")]
		public delegate void AjaxRequestDelegate(object sender, AjaxRequestEventArgs e);

		// Token: 0x02000FD4 RID: 4052
		// (Invoke) Token: 0x06009D87 RID: 40327
		[SuppressMessage("Microsoft.Design", "CA1034:NestedTypesShouldNotBeVisible")]
		public delegate void CommandEventDelegate(object sender, CommandEventArgs e);
	}
}
