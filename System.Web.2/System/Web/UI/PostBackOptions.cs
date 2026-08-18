using System;
using System.ComponentModel;

namespace System.Web.UI
{
	// Token: 0x020002F1 RID: 753
	public sealed class PostBackOptions
	{
		// Token: 0x060022E4 RID: 8932 RVA: 0x00071D3C File Offset: 0x0006FF3C
		public PostBackOptions(Control targetControl) : this(targetControl, null, null, false, false, false, true, false, null)
		{
		}

		// Token: 0x060022E5 RID: 8933 RVA: 0x00071D58 File Offset: 0x0006FF58
		public PostBackOptions(Control targetControl, string argument) : this(targetControl, argument, null, false, false, false, true, false, null)
		{
		}

		// Token: 0x060022E6 RID: 8934 RVA: 0x00071D74 File Offset: 0x0006FF74
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

		// Token: 0x170009C4 RID: 2500
		// (get) Token: 0x060022E7 RID: 8935 RVA: 0x00071DE1 File Offset: 0x0006FFE1
		// (set) Token: 0x060022E8 RID: 8936 RVA: 0x00071DE9 File Offset: 0x0006FFE9
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

		// Token: 0x170009C5 RID: 2501
		// (get) Token: 0x060022E9 RID: 8937 RVA: 0x00071DF2 File Offset: 0x0006FFF2
		// (set) Token: 0x060022EA RID: 8938 RVA: 0x00071DFA File Offset: 0x0006FFFA
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

		// Token: 0x170009C6 RID: 2502
		// (get) Token: 0x060022EB RID: 8939 RVA: 0x00071E03 File Offset: 0x00070003
		// (set) Token: 0x060022EC RID: 8940 RVA: 0x00071E0B File Offset: 0x0007000B
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

		// Token: 0x170009C7 RID: 2503
		// (get) Token: 0x060022ED RID: 8941 RVA: 0x00071E14 File Offset: 0x00070014
		// (set) Token: 0x060022EE RID: 8942 RVA: 0x00071E1C File Offset: 0x0007001C
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

		// Token: 0x170009C8 RID: 2504
		// (get) Token: 0x060022EF RID: 8943 RVA: 0x00071E25 File Offset: 0x00070025
		// (set) Token: 0x060022F0 RID: 8944 RVA: 0x00071E2D File Offset: 0x0007002D
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

		// Token: 0x170009C9 RID: 2505
		// (get) Token: 0x060022F1 RID: 8945 RVA: 0x00071E36 File Offset: 0x00070036
		// (set) Token: 0x060022F2 RID: 8946 RVA: 0x00071E3E File Offset: 0x0007003E
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

		// Token: 0x170009CA RID: 2506
		// (get) Token: 0x060022F3 RID: 8947 RVA: 0x00071E47 File Offset: 0x00070047
		// (set) Token: 0x060022F4 RID: 8948 RVA: 0x00071E4F File Offset: 0x0007004F
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

		// Token: 0x170009CB RID: 2507
		// (get) Token: 0x060022F5 RID: 8949 RVA: 0x00071E58 File Offset: 0x00070058
		[DefaultValue(null)]
		public Control TargetControl
		{
			get
			{
				return this._targetControl;
			}
		}

		// Token: 0x170009CC RID: 2508
		// (get) Token: 0x060022F6 RID: 8950 RVA: 0x00071E60 File Offset: 0x00070060
		// (set) Token: 0x060022F7 RID: 8951 RVA: 0x00071E68 File Offset: 0x00070068
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

		// Token: 0x04001C88 RID: 7304
		private string _actionUrl;

		// Token: 0x04001C89 RID: 7305
		private string _argument;

		// Token: 0x04001C8A RID: 7306
		private string _validationGroup;

		// Token: 0x04001C8B RID: 7307
		private bool _autoPostBack;

		// Token: 0x04001C8C RID: 7308
		private bool _requiresJavaScriptProtocol;

		// Token: 0x04001C8D RID: 7309
		private bool _performValidation;

		// Token: 0x04001C8E RID: 7310
		private bool _trackFocus;

		// Token: 0x04001C8F RID: 7311
		private bool _clientSubmit = true;

		// Token: 0x04001C90 RID: 7312
		private Control _targetControl;
	}
}
