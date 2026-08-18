using System;
using System.Collections;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Configuration.Provider;
using System.Web.Util;

namespace System.Web.UI.WebControls.WebParts
{
	// Token: 0x020005AB RID: 1451
	[TypeConverter(typeof(EmptyStringExpandableObjectConverter))]
	public class WebPartPersonalization
	{
		// Token: 0x06004965 RID: 18789 RVA: 0x000F3FCB File Offset: 0x000F21CB
		public WebPartPersonalization(WebPartManager owner)
		{
			if (owner == null)
			{
				throw new ArgumentNullException("owner");
			}
			this._owner = owner;
			this._enabled = true;
		}

		// Token: 0x1700158D RID: 5517
		// (get) Token: 0x06004966 RID: 18790 RVA: 0x000F3FF0 File Offset: 0x000F21F0
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public bool CanEnterSharedScope
		{
			get
			{
				IDictionary userCapabilities = this.UserCapabilities;
				return userCapabilities != null && userCapabilities.Contains(WebPartPersonalization.EnterSharedScopeUserCapability);
			}
		}

		// Token: 0x1700158E RID: 5518
		// (get) Token: 0x06004967 RID: 18791 RVA: 0x000F4017 File Offset: 0x000F2217
		// (set) Token: 0x06004968 RID: 18792 RVA: 0x000F4020 File Offset: 0x000F2220
		[DefaultValue(true)]
		[NotifyParentProperty(true)]
		[WebSysDescription("WebPartPersonalization_Enabled")]
		public virtual bool Enabled
		{
			get
			{
				return this._enabled;
			}
			set
			{
				if (!this.WebPartManager.DesignMode && this._initializedSet && value != this.Enabled)
				{
					throw new InvalidOperationException(SR.GetString("WebPartPersonalization_MustSetBeforeInit", new object[]
					{
						"Enabled",
						"WebPartPersonalization"
					}));
				}
				this._enabled = value;
			}
		}

		// Token: 0x1700158F RID: 5519
		// (get) Token: 0x06004969 RID: 18793 RVA: 0x000F4078 File Offset: 0x000F2278
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public virtual bool HasPersonalizationState
		{
			get
			{
				if (this._provider == null)
				{
					throw new InvalidOperationException(SR.GetString("WebPartPersonalization_CantUsePropertyBeforeInit", new object[]
					{
						"HasPersonalizationState",
						"WebPartPersonalization"
					}));
				}
				Page page = this.WebPartManager.Page;
				if (page == null)
				{
					throw new InvalidOperationException(SR.GetString("PropertyCannotBeNull", new object[]
					{
						"WebPartManager.Page"
					}));
				}
				HttpRequest requestInternal = page.RequestInternal;
				if (requestInternal == null)
				{
					throw new InvalidOperationException(SR.GetString("PropertyCannotBeNull", new object[]
					{
						"WebPartManager.Page.Request"
					}));
				}
				PersonalizationStateQuery personalizationStateQuery = new PersonalizationStateQuery();
				personalizationStateQuery.PathToMatch = requestInternal.AppRelativeCurrentExecutionFilePath;
				if (this.Scope == PersonalizationScope.User && requestInternal.IsAuthenticated)
				{
					personalizationStateQuery.UsernameToMatch = page.User.Identity.Name;
				}
				return this._provider.GetCountOfState(this.Scope, personalizationStateQuery) > 0;
			}
		}

		// Token: 0x17001590 RID: 5520
		// (get) Token: 0x0600496A RID: 18794 RVA: 0x000F4155 File Offset: 0x000F2355
		// (set) Token: 0x0600496B RID: 18795 RVA: 0x000F4160 File Offset: 0x000F2360
		[DefaultValue(PersonalizationScope.User)]
		[NotifyParentProperty(true)]
		[WebSysDescription("WebPartPersonalization_InitialScope")]
		public virtual PersonalizationScope InitialScope
		{
			get
			{
				return this._initialScope;
			}
			set
			{
				if (value < PersonalizationScope.User || value > PersonalizationScope.Shared)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				if (!this.WebPartManager.DesignMode && this._initializedSet && value != this.InitialScope)
				{
					throw new InvalidOperationException(SR.GetString("WebPartPersonalization_MustSetBeforeInit", new object[]
					{
						"InitialScope",
						"WebPartPersonalization"
					}));
				}
				this._initialScope = value;
			}
		}

		// Token: 0x17001591 RID: 5521
		// (get) Token: 0x0600496C RID: 18796 RVA: 0x000F41CB File Offset: 0x000F23CB
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public bool IsEnabled
		{
			get
			{
				return this.IsInitialized;
			}
		}

		// Token: 0x17001592 RID: 5522
		// (get) Token: 0x0600496D RID: 18797 RVA: 0x000F41D3 File Offset: 0x000F23D3
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		protected bool IsInitialized
		{
			get
			{
				return this._initialized;
			}
		}

		// Token: 0x17001593 RID: 5523
		// (get) Token: 0x0600496E RID: 18798 RVA: 0x000F41DC File Offset: 0x000F23DC
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public bool IsModifiable
		{
			get
			{
				IDictionary userCapabilities = this.UserCapabilities;
				return userCapabilities != null && userCapabilities.Contains(WebPartPersonalization.ModifyStateUserCapability);
			}
		}

		// Token: 0x17001594 RID: 5524
		// (get) Token: 0x0600496F RID: 18799 RVA: 0x000F4203 File Offset: 0x000F2403
		// (set) Token: 0x06004970 RID: 18800 RVA: 0x000F421C File Offset: 0x000F241C
		[DefaultValue("")]
		[NotifyParentProperty(true)]
		[WebSysDescription("WebPartPersonalization_ProviderName")]
		public virtual string ProviderName
		{
			get
			{
				if (this._providerName == null)
				{
					return string.Empty;
				}
				return this._providerName;
			}
			set
			{
				if (!this.WebPartManager.DesignMode && this._initializedSet && !string.Equals(value, this.ProviderName, StringComparison.Ordinal))
				{
					throw new InvalidOperationException(SR.GetString("WebPartPersonalization_MustSetBeforeInit", new object[]
					{
						"ProviderName",
						"WebPartPersonalization"
					}));
				}
				this._providerName = value;
			}
		}

		// Token: 0x17001595 RID: 5525
		// (get) Token: 0x06004971 RID: 18801 RVA: 0x000F427A File Offset: 0x000F247A
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public PersonalizationScope Scope
		{
			get
			{
				return this._currentScope;
			}
		}

		// Token: 0x17001596 RID: 5526
		// (get) Token: 0x06004972 RID: 18802 RVA: 0x000F4282 File Offset: 0x000F2482
		internal bool ScopeToggled
		{
			get
			{
				return this._scopeToggled;
			}
		}

		// Token: 0x17001597 RID: 5527
		// (get) Token: 0x06004973 RID: 18803 RVA: 0x000F428A File Offset: 0x000F248A
		// (set) Token: 0x06004974 RID: 18804 RVA: 0x000F4292 File Offset: 0x000F2492
		protected bool ShouldResetPersonalizationState
		{
			get
			{
				return this._shouldResetPersonalizationState;
			}
			set
			{
				this._shouldResetPersonalizationState = value;
			}
		}

		// Token: 0x17001598 RID: 5528
		// (get) Token: 0x06004975 RID: 18805 RVA: 0x000F429B File Offset: 0x000F249B
		protected virtual IDictionary UserCapabilities
		{
			get
			{
				if (this._userCapabilities == null)
				{
					this._userCapabilities = new HybridDictionary();
				}
				return this._userCapabilities;
			}
		}

		// Token: 0x17001599 RID: 5529
		// (get) Token: 0x06004976 RID: 18806 RVA: 0x000F42B6 File Offset: 0x000F24B6
		protected WebPartManager WebPartManager
		{
			get
			{
				return this._owner;
			}
		}

		// Token: 0x06004977 RID: 18807 RVA: 0x000F42BE File Offset: 0x000F24BE
		protected internal virtual void ApplyPersonalizationState()
		{
			if (this.IsEnabled)
			{
				this.EnsurePersonalizationState();
				this._personalizationState.ApplyWebPartManagerPersonalization();
			}
		}

		// Token: 0x06004978 RID: 18808 RVA: 0x000F42D9 File Offset: 0x000F24D9
		protected internal virtual void ApplyPersonalizationState(WebPart webPart)
		{
			if (webPart == null)
			{
				throw new ArgumentNullException("webPart");
			}
			if (this.IsEnabled)
			{
				this.EnsurePersonalizationState();
				this._personalizationState.ApplyWebPartPersonalization(webPart);
			}
		}

		// Token: 0x06004979 RID: 18809 RVA: 0x000F4304 File Offset: 0x000F2504
		private void ApplyPersonalizationState(Control control, WebPartPersonalization.PersonalizationInfo info)
		{
			ITrackingPersonalizable trackingPersonalizable = control as ITrackingPersonalizable;
			IPersonalizable personalizable = control as IPersonalizable;
			if (trackingPersonalizable != null)
			{
				trackingPersonalizable.BeginLoad();
			}
			if (personalizable != null && info.CustomProperties != null && info.CustomProperties.Count > 0)
			{
				personalizable.Load(info.CustomProperties);
			}
			if (info.Properties != null && info.Properties.Count > 0)
			{
				BlobPersonalizationState.SetPersonalizedProperties(control, info.Properties);
			}
			if (trackingPersonalizable != null)
			{
				trackingPersonalizable.EndLoad();
			}
		}

		// Token: 0x0600497A RID: 18810 RVA: 0x000F4379 File Offset: 0x000F2579
		protected virtual void ChangeScope(PersonalizationScope scope)
		{
			PersonalizationProviderHelper.CheckPersonalizationScope(scope);
			if (scope == this._currentScope)
			{
				return;
			}
			if (scope == PersonalizationScope.Shared && !this.CanEnterSharedScope)
			{
				throw new InvalidOperationException(SR.GetString("WebPartPersonalization_CannotEnterSharedScope"));
			}
			this._currentScope = scope;
			this._scopeToggled = true;
		}

		// Token: 0x0600497B RID: 18811 RVA: 0x000F43B8 File Offset: 0x000F25B8
		protected internal virtual void CopyPersonalizationState(WebPart webPartA, WebPart webPartB)
		{
			if (webPartA == null)
			{
				throw new ArgumentNullException("webPartA");
			}
			if (webPartB == null)
			{
				throw new ArgumentNullException("webPartB");
			}
			if (webPartA.GetType() != webPartB.GetType())
			{
				throw new ArgumentException(SR.GetString("WebPartPersonalization_SameType", new object[]
				{
					"webPartA",
					"webPartB"
				}));
			}
			this.CopyPersonalizationState(webPartA, webPartB);
			GenericWebPart genericWebPart = webPartA as GenericWebPart;
			GenericWebPart genericWebPart2 = webPartB as GenericWebPart;
			if (genericWebPart != null && genericWebPart2 != null)
			{
				Control childControl = genericWebPart.ChildControl;
				Control childControl2 = genericWebPart2.ChildControl;
				if (childControl == null)
				{
					throw new ArgumentException(SR.GetString("PropertyCannotBeNull", new object[]
					{
						"ChildControl"
					}), "webPartA");
				}
				if (childControl2 == null)
				{
					throw new ArgumentException(SR.GetString("PropertyCannotBeNull", new object[]
					{
						"ChildControl"
					}), "webPartB");
				}
				if (childControl.GetType() != childControl2.GetType())
				{
					throw new ArgumentException(SR.GetString("WebPartPersonalization_SameType", new object[]
					{
						"webPartA.ChildControl",
						"webPartB.ChildControl"
					}));
				}
				this.CopyPersonalizationState(childControl, childControl2);
			}
			this.SetDirty(webPartB);
		}

		// Token: 0x0600497C RID: 18812 RVA: 0x000F44E0 File Offset: 0x000F26E0
		private void CopyPersonalizationState(Control controlA, Control controlB)
		{
			WebPartPersonalization.PersonalizationInfo info = this.ExtractPersonalizationState(controlA);
			this.ApplyPersonalizationState(controlB, info);
		}

		// Token: 0x0600497D RID: 18813 RVA: 0x000F4500 File Offset: 0x000F2700
		private void DeterminePersonalizationProvider()
		{
			string providerName = this.ProviderName;
			if (string.IsNullOrEmpty(providerName))
			{
				this._provider = PersonalizationAdministration.Provider;
				return;
			}
			PersonalizationProvider personalizationProvider = PersonalizationAdministration.Providers[providerName];
			if (personalizationProvider != null)
			{
				this._provider = personalizationProvider;
				return;
			}
			throw new ProviderException(SR.GetString("WebPartPersonalization_ProviderNotFound", new object[]
			{
				providerName
			}));
		}

		// Token: 0x0600497E RID: 18814 RVA: 0x000F4558 File Offset: 0x000F2758
		public void EnsureEnabled(bool ensureModifiable)
		{
			if (!(ensureModifiable ? this.IsModifiable : this.IsEnabled))
			{
				string @string;
				if (ensureModifiable)
				{
					@string = SR.GetString("WebPartPersonalization_PersonalizationNotModifiable");
				}
				else
				{
					@string = SR.GetString("WebPartPersonalization_PersonalizationNotEnabled");
				}
				throw new InvalidOperationException(@string);
			}
		}

		// Token: 0x0600497F RID: 18815 RVA: 0x000F459C File Offset: 0x000F279C
		private void EnsurePersonalizationState()
		{
			if (this._personalizationState == null)
			{
				throw new InvalidOperationException(SR.GetString("WebPartPersonalization_PersonalizationStateNotLoaded"));
			}
		}

		// Token: 0x06004980 RID: 18816 RVA: 0x000F45B6 File Offset: 0x000F27B6
		protected internal virtual void ExtractPersonalizationState()
		{
			if (this.IsEnabled && !this.ShouldResetPersonalizationState)
			{
				this.EnsurePersonalizationState();
				this._personalizationState.ExtractWebPartManagerPersonalization();
			}
		}

		// Token: 0x06004981 RID: 18817 RVA: 0x000F45D9 File Offset: 0x000F27D9
		protected internal virtual void ExtractPersonalizationState(WebPart webPart)
		{
			if (this.IsEnabled && !this.ShouldResetPersonalizationState)
			{
				this.EnsurePersonalizationState();
				this._personalizationState.ExtractWebPartPersonalization(webPart);
			}
		}

		// Token: 0x06004982 RID: 18818 RVA: 0x000F4600 File Offset: 0x000F2800
		private WebPartPersonalization.PersonalizationInfo ExtractPersonalizationState(Control control)
		{
			ITrackingPersonalizable trackingPersonalizable = control as ITrackingPersonalizable;
			IPersonalizable personalizable = control as IPersonalizable;
			if (trackingPersonalizable != null)
			{
				trackingPersonalizable.BeginSave();
			}
			WebPartPersonalization.PersonalizationInfo personalizationInfo = new WebPartPersonalization.PersonalizationInfo();
			if (personalizable != null)
			{
				personalizationInfo.CustomProperties = new PersonalizationDictionary();
				personalizable.Save(personalizationInfo.CustomProperties);
			}
			personalizationInfo.Properties = BlobPersonalizationState.GetPersonalizedProperties(control, PersonalizationScope.Shared);
			if (trackingPersonalizable != null)
			{
				trackingPersonalizable.EndSave();
			}
			return personalizationInfo;
		}

		// Token: 0x06004983 RID: 18819 RVA: 0x000F465B File Offset: 0x000F285B
		protected internal virtual string GetAuthorizationFilter(string webPartID)
		{
			if (string.IsNullOrEmpty(webPartID))
			{
				throw ExceptionUtil.ParameterNullOrEmpty("webPartID");
			}
			this.EnsureEnabled(false);
			this.EnsurePersonalizationState();
			return this._personalizationState.GetAuthorizationFilter(webPartID);
		}

		// Token: 0x06004984 RID: 18820 RVA: 0x000F4689 File Offset: 0x000F2889
		internal void LoadInternal()
		{
			if (this.Enabled)
			{
				this._currentScope = this.Load();
				this._initialized = true;
			}
			this._initializedSet = true;
		}

		// Token: 0x06004985 RID: 18821 RVA: 0x000F46B0 File Offset: 0x000F28B0
		protected virtual PersonalizationScope Load()
		{
			if (!this.Enabled)
			{
				throw new InvalidOperationException(SR.GetString("WebPartPersonalization_PersonalizationNotEnabled"));
			}
			this.DeterminePersonalizationProvider();
			Page page = this.WebPartManager.Page;
			if (page == null)
			{
				throw new InvalidOperationException(SR.GetString("PropertyCannotBeNull", new object[]
				{
					"WebPartManager.Page"
				}));
			}
			HttpRequest requestInternal = page.RequestInternal;
			if (requestInternal == null)
			{
				throw new InvalidOperationException(SR.GetString("PropertyCannotBeNull", new object[]
				{
					"WebPartManager.Page.Request"
				}));
			}
			if (requestInternal.IsAuthenticated)
			{
				this._userCapabilities = this._provider.DetermineUserCapabilities(this.WebPartManager);
			}
			this._personalizationState = this._provider.LoadPersonalizationState(this.WebPartManager, false);
			if (this._personalizationState == null)
			{
				throw new ProviderException(SR.GetString("WebPartPersonalization_CannotLoadPersonalization"));
			}
			return this._provider.DetermineInitialScope(this.WebPartManager, this._personalizationState);
		}

		// Token: 0x06004986 RID: 18822 RVA: 0x000F4798 File Offset: 0x000F2998
		public virtual void ResetPersonalizationState()
		{
			this.EnsureEnabled(true);
			if (this._provider == null)
			{
				throw new InvalidOperationException(SR.GetString("WebPartPersonalization_CantCallMethodBeforeInit", new object[]
				{
					"ResetPersonalizationState",
					"WebPartPersonalization"
				}));
			}
			this._provider.ResetPersonalizationState(this.WebPartManager);
			this.ShouldResetPersonalizationState = true;
			Page page = this.WebPartManager.Page;
			if (page == null)
			{
				throw new InvalidOperationException(SR.GetString("PropertyCannotBeNull", new object[]
				{
					"WebPartManager.Page"
				}));
			}
			this.TransferToCurrentPage(page);
		}

		// Token: 0x06004987 RID: 18823 RVA: 0x000F4826 File Offset: 0x000F2A26
		internal void SaveInternal()
		{
			if (this.IsModifiable)
			{
				this.Save();
			}
		}

		// Token: 0x06004988 RID: 18824 RVA: 0x000F4838 File Offset: 0x000F2A38
		protected virtual void Save()
		{
			this.EnsureEnabled(true);
			this.EnsurePersonalizationState();
			if (this._provider == null)
			{
				throw new InvalidOperationException(SR.GetString("WebPartPersonalization_CantCallMethodBeforeInit", new object[]
				{
					"Save",
					"WebPartPersonalization"
				}));
			}
			if (this._personalizationState.IsDirty && !this.ShouldResetPersonalizationState)
			{
				this._provider.SavePersonalizationState(this._personalizationState);
			}
		}

		// Token: 0x06004989 RID: 18825 RVA: 0x000F48A6 File Offset: 0x000F2AA6
		protected internal virtual void SetDirty()
		{
			if (this.IsEnabled)
			{
				this.EnsurePersonalizationState();
				this._personalizationState.SetWebPartManagerDirty();
			}
		}

		// Token: 0x0600498A RID: 18826 RVA: 0x000F48C1 File Offset: 0x000F2AC1
		protected internal virtual void SetDirty(WebPart webPart)
		{
			if (this.IsEnabled)
			{
				this.EnsurePersonalizationState();
				this._personalizationState.SetWebPartDirty(webPart);
			}
		}

		// Token: 0x0600498B RID: 18827 RVA: 0x000F48E0 File Offset: 0x000F2AE0
		public virtual void ToggleScope()
		{
			this.EnsureEnabled(false);
			Page page = this.WebPartManager.Page;
			if (page == null)
			{
				throw new InvalidOperationException(SR.GetString("PropertyCannotBeNull", new object[]
				{
					"WebPartManager.Page"
				}));
			}
			if (page.IsExportingWebPart)
			{
				return;
			}
			Page previousPage = page.PreviousPage;
			if (previousPage != null && !previousPage.IsCrossPagePostBack)
			{
				WebPartManager currentWebPartManager = WebPartManager.GetCurrentWebPartManager(previousPage);
				if (currentWebPartManager != null && currentWebPartManager.Personalization.ScopeToggled)
				{
					return;
				}
			}
			if (this._currentScope == PersonalizationScope.Shared)
			{
				this.ChangeScope(PersonalizationScope.User);
			}
			else
			{
				this.ChangeScope(PersonalizationScope.Shared);
			}
			this.TransferToCurrentPage(page);
		}

		// Token: 0x0600498C RID: 18828 RVA: 0x000F4974 File Offset: 0x000F2B74
		private void TransferToCurrentPage(Page page)
		{
			HttpRequest requestInternal = page.RequestInternal;
			if (requestInternal == null)
			{
				throw new InvalidOperationException(SR.GetString("PropertyCannotBeNull", new object[]
				{
					"WebPartManager.Page.Request"
				}));
			}
			string text = requestInternal.CurrentExecutionFilePath;
			if (page.Form == null || string.Equals(page.Form.Method, "post", StringComparison.OrdinalIgnoreCase))
			{
				string clientQueryString = page.ClientQueryString;
				if (!string.IsNullOrEmpty(clientQueryString))
				{
					text = text + "?" + clientQueryString;
				}
			}
			IScriptManager scriptManager = page.ScriptManager;
			if (scriptManager != null && scriptManager.IsInAsyncPostBack)
			{
				requestInternal.Response.Redirect(text);
				return;
			}
			page.Server.Transfer(text, false);
		}

		// Token: 0x0400279F RID: 10143
		public static readonly WebPartUserCapability ModifyStateUserCapability = new WebPartUserCapability("modifyState");

		// Token: 0x040027A0 RID: 10144
		public static readonly WebPartUserCapability EnterSharedScopeUserCapability = new WebPartUserCapability("enterSharedScope");

		// Token: 0x040027A1 RID: 10145
		private WebPartManager _owner;

		// Token: 0x040027A2 RID: 10146
		private bool _enabled;

		// Token: 0x040027A3 RID: 10147
		private string _providerName;

		// Token: 0x040027A4 RID: 10148
		private PersonalizationScope _initialScope;

		// Token: 0x040027A5 RID: 10149
		private bool _initialized;

		// Token: 0x040027A6 RID: 10150
		private bool _initializedSet;

		// Token: 0x040027A7 RID: 10151
		private PersonalizationProvider _provider;

		// Token: 0x040027A8 RID: 10152
		private PersonalizationScope _currentScope;

		// Token: 0x040027A9 RID: 10153
		private IDictionary _userCapabilities;

		// Token: 0x040027AA RID: 10154
		private PersonalizationState _personalizationState;

		// Token: 0x040027AB RID: 10155
		private bool _scopeToggled;

		// Token: 0x040027AC RID: 10156
		private bool _shouldResetPersonalizationState;

		// Token: 0x020009FC RID: 2556
		private sealed class PersonalizationInfo
		{
			// Token: 0x04003A32 RID: 14898
			public IDictionary Properties;

			// Token: 0x04003A33 RID: 14899
			public PersonalizationDictionary CustomProperties;
		}
	}
}
