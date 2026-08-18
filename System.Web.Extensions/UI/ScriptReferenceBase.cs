using System;
using System.ComponentModel;
using System.Globalization;
using System.Web.Resources;
using System.Web.UI.WebControls;

namespace System.Web.UI
{
	// Token: 0x02000077 RID: 119
	[DefaultProperty("Path")]
	public abstract class ScriptReferenceBase
	{
		// Token: 0x1700015E RID: 350
		// (get) Token: 0x06000504 RID: 1284 RVA: 0x00017EC5 File Offset: 0x000160C5
		// (set) Token: 0x06000505 RID: 1285 RVA: 0x00017ECD File Offset: 0x000160CD
		internal bool AlwaysLoadBeforeUI
		{
			get
			{
				return this._alwaysLoadBeforeUI;
			}
			set
			{
				this._alwaysLoadBeforeUI = value;
			}
		}

		// Token: 0x1700015F RID: 351
		// (get) Token: 0x06000506 RID: 1286 RVA: 0x00017ED6 File Offset: 0x000160D6
		// (set) Token: 0x06000507 RID: 1287 RVA: 0x00017EDE File Offset: 0x000160DE
		internal IClientUrlResolver ClientUrlResolver
		{
			get
			{
				return this._clientUrlResolver;
			}
			set
			{
				this._clientUrlResolver = value;
			}
		}

		// Token: 0x17000160 RID: 352
		// (get) Token: 0x06000508 RID: 1288 RVA: 0x00017EE7 File Offset: 0x000160E7
		// (set) Token: 0x06000509 RID: 1289 RVA: 0x00017EEF File Offset: 0x000160EF
		internal Control ContainingControl
		{
			get
			{
				return this._containingControl;
			}
			set
			{
				this._containingControl = value;
			}
		}

		// Token: 0x17000161 RID: 353
		// (get) Token: 0x0600050A RID: 1290 RVA: 0x00017EF8 File Offset: 0x000160F8
		// (set) Token: 0x0600050B RID: 1291 RVA: 0x00017F00 File Offset: 0x00016100
		internal bool IsStaticReference
		{
			get
			{
				return this._isStaticReference;
			}
			set
			{
				this._isStaticReference = value;
			}
		}

		// Token: 0x17000162 RID: 354
		// (get) Token: 0x0600050C RID: 1292 RVA: 0x00017F09 File Offset: 0x00016109
		// (set) Token: 0x0600050D RID: 1293 RVA: 0x00017F11 File Offset: 0x00016111
		internal bool IsBundleReference { get; set; }

		// Token: 0x17000163 RID: 355
		// (get) Token: 0x0600050E RID: 1294 RVA: 0x00017F1A File Offset: 0x0001611A
		// (set) Token: 0x0600050F RID: 1295 RVA: 0x00017F22 File Offset: 0x00016122
		[Category("Behavior")]
		[DefaultValue(true)]
		[NotifyParentProperty(true)]
		[ResourceDescription("ScriptReference_NotifyScriptLoaded")]
		[Obsolete("NotifyScriptLoaded is no longer required in script references.")]
		public bool NotifyScriptLoaded
		{
			get
			{
				return this._notifyScriptLoaded;
			}
			set
			{
				this._notifyScriptLoaded = value;
			}
		}

		// Token: 0x17000164 RID: 356
		// (get) Token: 0x06000510 RID: 1296 RVA: 0x00017F2B File Offset: 0x0001612B
		// (set) Token: 0x06000511 RID: 1297 RVA: 0x00017F41 File Offset: 0x00016141
		[Category("Behavior")]
		[DefaultValue("")]
		[NotifyParentProperty(true)]
		[ResourceDescription("ScriptReference_Path")]
		[UrlProperty("*.js")]
		public string Path
		{
			get
			{
				if (this._path != null)
				{
					return this._path;
				}
				return string.Empty;
			}
			set
			{
				this._path = value;
			}
		}

		// Token: 0x17000165 RID: 357
		// (get) Token: 0x06000512 RID: 1298 RVA: 0x00017F4A File Offset: 0x0001614A
		// (set) Token: 0x06000513 RID: 1299 RVA: 0x00017F52 File Offset: 0x00016152
		[ResourceDescription("ScriptReference_ResourceUICultures")]
		[DefaultValue(null)]
		[Category("Behavior")]
		[MergableProperty(false)]
		[NotifyParentProperty(true)]
		[TypeConverter(typeof(StringArrayConverter))]
		public string[] ResourceUICultures
		{
			get
			{
				return this._resourceUICultures;
			}
			set
			{
				this._resourceUICultures = value;
			}
		}

		// Token: 0x17000166 RID: 358
		// (get) Token: 0x06000514 RID: 1300 RVA: 0x00017F5B File Offset: 0x0001615B
		// (set) Token: 0x06000515 RID: 1301 RVA: 0x00017F63 File Offset: 0x00016163
		[Category("Behavior")]
		[DefaultValue(ScriptMode.Auto)]
		[NotifyParentProperty(true)]
		[ResourceDescription("ScriptReference_ScriptMode")]
		public ScriptMode ScriptMode
		{
			get
			{
				return this._scriptMode;
			}
			set
			{
				if (value < ScriptMode.Auto || value > ScriptMode.Release)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this._scriptMode = value;
			}
		}

		// Token: 0x06000516 RID: 1302
		[Obsolete("Use IsAjaxFrameworkScript(ScriptManager)")]
		protected internal abstract bool IsFromSystemWebExtensions();

		// Token: 0x06000517 RID: 1303 RVA: 0x0001359B File Offset: 0x0001179B
		protected internal virtual bool IsAjaxFrameworkScript(ScriptManager scriptManager)
		{
			return false;
		}

		// Token: 0x17000167 RID: 359
		// (get) Token: 0x06000518 RID: 1304 RVA: 0x00017F7F File Offset: 0x0001617F
		// (set) Token: 0x06000519 RID: 1305 RVA: 0x00017F87 File Offset: 0x00016187
		internal virtual bool IsDefiningSys { get; set; }

		// Token: 0x0600051A RID: 1306 RVA: 0x00017F90 File Offset: 0x00016190
		internal static string GetDebugPath(string releasePath)
		{
			string text;
			string str;
			if (releasePath.IndexOf('?') >= 0)
			{
				int num = releasePath.IndexOf('?');
				text = releasePath.Substring(0, num);
				str = releasePath.Substring(num);
			}
			else
			{
				text = releasePath;
				str = null;
			}
			if (!text.EndsWith(".js", StringComparison.Ordinal))
			{
				throw new InvalidOperationException(string.Format(CultureInfo.CurrentUICulture, AtlasWeb.ScriptReference_InvalidReleaseScriptPath, new object[]
				{
					text
				}));
			}
			return ScriptReferenceBase.ReplaceExtension(text) + str;
		}

		// Token: 0x0600051B RID: 1307
		protected internal abstract string GetUrl(ScriptManager scriptManager, bool zip);

		// Token: 0x0600051C RID: 1308 RVA: 0x00018001 File Offset: 0x00016201
		protected static string ReplaceExtension(string pathOrName)
		{
			return pathOrName.Substring(0, pathOrName.Length - 2) + "debug.js";
		}

		// Token: 0x040001CF RID: 463
		private bool _alwaysLoadBeforeUI;

		// Token: 0x040001D0 RID: 464
		private IClientUrlResolver _clientUrlResolver;

		// Token: 0x040001D1 RID: 465
		private Control _containingControl;

		// Token: 0x040001D2 RID: 466
		private bool _isStaticReference;

		// Token: 0x040001D3 RID: 467
		private bool _notifyScriptLoaded = true;

		// Token: 0x040001D4 RID: 468
		private string _path;

		// Token: 0x040001D5 RID: 469
		private string[] _resourceUICultures;

		// Token: 0x040001D6 RID: 470
		private ScriptMode _scriptMode;
	}
}
