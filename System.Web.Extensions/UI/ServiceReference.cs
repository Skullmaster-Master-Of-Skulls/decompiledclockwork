using System;
using System.ComponentModel;
using System.Globalization;
using System.Web.Resources;
using System.Web.Script.Services;

namespace System.Web.UI
{
	// Token: 0x0200007F RID: 127
	[DefaultProperty("Path")]
	public class ServiceReference
	{
		// Token: 0x06000582 RID: 1410 RVA: 0x00002050 File Offset: 0x00000250
		public ServiceReference()
		{
		}

		// Token: 0x06000583 RID: 1411 RVA: 0x00019DCF File Offset: 0x00017FCF
		public ServiceReference(string path)
		{
			this._path = path;
		}

		// Token: 0x1700018B RID: 395
		// (get) Token: 0x06000584 RID: 1412 RVA: 0x00019DDE File Offset: 0x00017FDE
		// (set) Token: 0x06000585 RID: 1413 RVA: 0x00019DE6 File Offset: 0x00017FE6
		[ResourceDescription("ServiceReference_InlineScript")]
		[DefaultValue(false)]
		[Category("Behavior")]
		public virtual bool InlineScript
		{
			get
			{
				return this._inlineScript;
			}
			set
			{
				this._inlineScript = value;
			}
		}

		// Token: 0x1700018C RID: 396
		// (get) Token: 0x06000586 RID: 1414 RVA: 0x00019DEF File Offset: 0x00017FEF
		// (set) Token: 0x06000587 RID: 1415 RVA: 0x00019E00 File Offset: 0x00018000
		[ResourceDescription("ServiceReference_Path")]
		[DefaultValue("")]
		[Category("Behavior")]
		[UrlProperty]
		public virtual string Path
		{
			get
			{
				return this._path ?? string.Empty;
			}
			set
			{
				this._path = value;
			}
		}

		// Token: 0x06000588 RID: 1416 RVA: 0x00019E0C File Offset: 0x0001800C
		protected internal virtual string GetProxyScript(ScriptManager scriptManager, Control containingControl)
		{
			string text = this.GetServiceUrl(containingControl, false);
			try
			{
				text = VirtualPathUtility.Combine(containingControl.Context.Request.FilePath, text);
			}
			catch
			{
				throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, AtlasWeb.WebService_InvalidInlineVirtualPath, new object[]
				{
					text
				}));
			}
			return WebServiceClientProxyGenerator.GetInlineClientProxyScript(text, containingControl.Context, scriptManager.IsDebuggingEnabled);
		}

		// Token: 0x06000589 RID: 1417 RVA: 0x00019E80 File Offset: 0x00018080
		protected internal virtual string GetProxyUrl(ScriptManager scriptManager, Control containingControl)
		{
			return this.GetServiceUrl(containingControl, true) + ((scriptManager.DesignMode || scriptManager.IsDebuggingEnabled) ? "/jsdebug" : "/js");
		}

		// Token: 0x0600058A RID: 1418 RVA: 0x00019EAC File Offset: 0x000180AC
		private string GetServiceUrl(Control containingControl, bool encodeSpaces)
		{
			string text = this.Path;
			if (string.IsNullOrEmpty(text))
			{
				throw new InvalidOperationException(AtlasWeb.ServiceReference_PathCannotBeEmpty);
			}
			if (encodeSpaces)
			{
				text = containingControl.ResolveClientUrl(text);
			}
			else
			{
				text = containingControl.ResolveUrl(text);
			}
			return text;
		}

		// Token: 0x0600058B RID: 1419 RVA: 0x00019EEC File Offset: 0x000180EC
		internal void Register(Control containingControl, ScriptManager scriptManager)
		{
			if (this.InlineScript)
			{
				if (!scriptManager.IsRestMethodCall)
				{
					string proxyScript = this.GetProxyScript(scriptManager, containingControl);
					if (!string.IsNullOrEmpty(proxyScript))
					{
						scriptManager.RegisterClientScriptBlockInternal(scriptManager, typeof(ScriptManager), proxyScript, proxyScript, true);
						return;
					}
				}
			}
			else
			{
				string proxyUrl = this.GetProxyUrl(scriptManager, containingControl);
				if (!string.IsNullOrEmpty(proxyUrl))
				{
					scriptManager.RegisterClientScriptIncludeInternal(scriptManager, typeof(ScriptManager), proxyUrl, proxyUrl);
				}
			}
		}

		// Token: 0x0600058C RID: 1420 RVA: 0x00019F53 File Offset: 0x00018153
		public override string ToString()
		{
			if (!string.IsNullOrEmpty(this.Path))
			{
				return this.Path;
			}
			return base.GetType().Name;
		}

		// Token: 0x040001FF RID: 511
		private string _path;

		// Token: 0x04000200 RID: 512
		private bool _inlineScript;

		// Token: 0x04000201 RID: 513
		internal Control _containingControl;
	}
}
