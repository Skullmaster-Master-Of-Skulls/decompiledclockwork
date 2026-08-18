using System;
using System.Collections.Generic;
using System.Globalization;
using System.Web.Resources;
using System.Web.UI.WebControls;

namespace System.Web.UI
{
	// Token: 0x0200006F RID: 111
	public abstract class ScriptControl : WebControl, IScriptControl
	{
		// Token: 0x060003EA RID: 1002 RVA: 0x000145ED File Offset: 0x000127ED
		protected ScriptControl()
		{
		}

		// Token: 0x060003EB RID: 1003 RVA: 0x000145F5 File Offset: 0x000127F5
		internal ScriptControl(IScriptManagerInternal scriptManager, IPage page)
		{
			this._scriptManager = scriptManager;
			this._page = page;
		}

		// Token: 0x1700010B RID: 267
		// (get) Token: 0x060003EC RID: 1004 RVA: 0x0001460C File Offset: 0x0001280C
		private IPage IPage
		{
			get
			{
				if (this._page != null)
				{
					return this._page;
				}
				Page page = this.Page;
				if (page == null)
				{
					throw new InvalidOperationException(AtlasWeb.Common_PageCannotBeNull);
				}
				return new PageWrapper(page);
			}
		}

		// Token: 0x1700010C RID: 268
		// (get) Token: 0x060003ED RID: 1005 RVA: 0x00014644 File Offset: 0x00012844
		private IScriptManagerInternal ScriptManager
		{
			get
			{
				if (this._scriptManager == null)
				{
					Page page = this.Page;
					if (page == null)
					{
						throw new InvalidOperationException(AtlasWeb.Common_PageCannotBeNull);
					}
					this._scriptManager = System.Web.UI.ScriptManager.GetCurrent(page);
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

		// Token: 0x060003EE RID: 1006 RVA: 0x000146AC File Offset: 0x000128AC
		protected internal override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);
			this.ScriptManager.RegisterScriptControl<ScriptControl>(this);
		}

		// Token: 0x060003EF RID: 1007 RVA: 0x000146C1 File Offset: 0x000128C1
		protected internal override void Render(HtmlTextWriter writer)
		{
			base.Render(writer);
			this.IPage.VerifyRenderingInServerForm(this);
			if (!base.DesignMode)
			{
				this.ScriptManager.RegisterScriptDescriptors(this);
			}
		}

		// Token: 0x060003F0 RID: 1008
		protected abstract IEnumerable<ScriptDescriptor> GetScriptDescriptors();

		// Token: 0x060003F1 RID: 1009
		protected abstract IEnumerable<ScriptReference> GetScriptReferences();

		// Token: 0x060003F2 RID: 1010 RVA: 0x000146EA File Offset: 0x000128EA
		IEnumerable<ScriptDescriptor> IScriptControl.GetScriptDescriptors()
		{
			return this.GetScriptDescriptors();
		}

		// Token: 0x060003F3 RID: 1011 RVA: 0x000146F2 File Offset: 0x000128F2
		IEnumerable<ScriptReference> IScriptControl.GetScriptReferences()
		{
			return this.GetScriptReferences();
		}

		// Token: 0x0400017C RID: 380
		private IScriptManagerInternal _scriptManager;

		// Token: 0x0400017D RID: 381
		private new IPage _page;
	}
}
