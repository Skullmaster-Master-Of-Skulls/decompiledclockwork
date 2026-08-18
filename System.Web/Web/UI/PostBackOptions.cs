using System;
using System.ComponentModel;
using System.Security.Permissions;

namespace System.Web.UI
{
	// Token: 0x0200045A RID: 1114
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public sealed class PostBackOptions
	{
		// Token: 0x060034D0 RID: 13520 RVA: 0x000E4C20 File Offset: 0x000E3C20
		public PostBackOptions(Control targetControl) : this(targetControl, null, null, false, false, false, true, false, null)
		{
		}

		// Token: 0x060034D1 RID: 13521 RVA: 0x000E4C3C File Offset: 0x000E3C3C
		public PostBackOptions(Control targetControl, string argument) : this(targetControl, argument, null, false, false, false, true, false, null)
		{
		}

		// Token: 0x060034D2 RID: 13522 RVA: 0x000E4C58 File Offset: 0x000E3C58
		public PostBackOptions(Control targetControl, string argument, string actionUrl, bool autoPostBack, bool requiresJavaScriptProtocol, bool trackFocus, bool clientSubmit, bool performValidation, string validationGroup)
		{
			if (targetControl == null)
			{
				throw new ArgumentNullException("targetControl");
			}
			this._actionUrl = actionUrl;
			this._argument = argument;
			this._autoPostBack = autoPostBack;
			this._clientSubmit = clientSubmit;
			this._requiresJavaScriptProtocol = requiresJavaScriptProtocol;
			this._performValidation = performValidation;
			this._trackFocus = trackFocus;
			this._targetControl = targetControl;
			this._validationGroup = validationGroup;
		}

		// Token: 0x17000BCB RID: 3019
		// (get) Token: 0x060034D3 RID: 13523 RVA: 0x000E4CC5 File Offset: 0x000E3CC5
		// (set) Token: 0x060034D4 RID: 13524 RVA: 0x000E4CCD File Offset: 0x000E3CCD
		[DefaultValue("")]
		public string ActionUrl
		{
			get
			{
				return this._actionUrl;
			}
			set
			{
				this._actionUrl = value;
			}
		}

		// Token: 0x17000BCC RID: 3020
		// (get) Token: 0x060034D5 RID: 13525 RVA: 0x000E4CD6 File Offset: 0x000E3CD6
		// (set) Token: 0x060034D6 RID: 13526 RVA: 0x000E4CDE File Offset: 0x000E3CDE
		[DefaultValue("")]
		public string Argument
		{
			get
			{
				return this._argument;
			}
			set
			{
				this._argument = value;
			}
		}

		// Token: 0x17000BCD RID: 3021
		// (get) Token: 0x060034D7 RID: 13527 RVA: 0x000E4CE7 File Offset: 0x000E3CE7
		// (set) Token: 0x060034D8 RID: 13528 RVA: 0x000E4CEF File Offset: 0x000E3CEF
		[DefaultValue(false)]
		public bool AutoPostBack
		{
			get
			{
				return this._autoPostBack;
			}
			set
			{
				this._autoPostBack = value;
			}
		}

		// Token: 0x17000BCE RID: 3022
		// (get) Token: 0x060034D9 RID: 13529 RVA: 0x000E4CF8 File Offset: 0x000E3CF8
		// (set) Token: 0x060034DA RID: 13530 RVA: 0x000E4D00 File Offset: 0x000E3D00
		[DefaultValue(true)]
		public bool ClientSubmit
		{
			get
			{
				return this._clientSubmit;
			}
			set
			{
				this._clientSubmit = value;
			}
		}

		// Token: 0x17000BCF RID: 3023
		// (get) Token: 0x060034DB RID: 13531 RVA: 0x000E4D09 File Offset: 0x000E3D09
		// (set) Token: 0x060034DC RID: 13532 RVA: 0x000E4D11 File Offset: 0x000E3D11
		[DefaultValue(true)]
		public bool RequiresJavaScriptProtocol
		{
			get
			{
				return this._requiresJavaScriptProtocol;
			}
			set
			{
				this._requiresJavaScriptProtocol = value;
			}
		}

		// Token: 0x17000BD0 RID: 3024
		// (get) Token: 0x060034DD RID: 13533 RVA: 0x000E4D1A File Offset: 0x000E3D1A
		// (set) Token: 0x060034DE RID: 13534 RVA: 0x000E4D22 File Offset: 0x000E3D22
		[DefaultValue(false)]
		public bool PerformValidation
		{
			get
			{
				return this._performValidation;
			}
			set
			{
				this._performValidation = value;
			}
		}

		// Token: 0x17000BD1 RID: 3025
		// (get) Token: 0x060034DF RID: 13535 RVA: 0x000E4D2B File Offset: 0x000E3D2B
		// (set) Token: 0x060034E0 RID: 13536 RVA: 0x000E4D33 File Offset: 0x000E3D33
		[DefaultValue("")]
		public string ValidationGroup
		{
			get
			{
				return this._validationGroup;
			}
			set
			{
				this._validationGroup = value;
			}
		}

		// Token: 0x17000BD2 RID: 3026
		// (get) Token: 0x060034E1 RID: 13537 RVA: 0x000E4D3C File Offset: 0x000E3D3C
		[DefaultValue(null)]
		public Control TargetControl
		{
			get
			{
				return this._targetControl;
			}
		}

		// Token: 0x17000BD3 RID: 3027
		// (get) Token: 0x060034E2 RID: 13538 RVA: 0x000E4D44 File Offset: 0x000E3D44
		// (set) Token: 0x060034E3 RID: 13539 RVA: 0x000E4D4C File Offset: 0x000E3D4C
		[DefaultValue(false)]
		public bool TrackFocus
		{
			get
			{
				return this._trackFocus;
			}
			set
			{
				this._trackFocus = value;
			}
		}

		// Token: 0x040024FC RID: 9468
		private string _actionUrl;

		// Token: 0x040024FD RID: 9469
		private string _argument;

		// Token: 0x040024FE RID: 9470
		private string _validationGroup;

		// Token: 0x040024FF RID: 9471
		private bool _autoPostBack;

		// Token: 0x04002500 RID: 9472
		private bool _requiresJavaScriptProtocol;

		// Token: 0x04002501 RID: 9473
		private bool _performValidation;

		// Token: 0x04002502 RID: 9474
		private bool _trackFocus;

		// Token: 0x04002503 RID: 9475
		private bool _clientSubmit = true;

		// Token: 0x04002504 RID: 9476
		private Control _targetControl;
	}
}
