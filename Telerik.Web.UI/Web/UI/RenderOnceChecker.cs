using System;
using System.Collections;
using System.Collections.Generic;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x02000FD7 RID: 4055
	internal class RenderOnceChecker
	{
		// Token: 0x06009D99 RID: 40345 RVA: 0x002327C8 File Offset: 0x002309C8
		public RenderOnceChecker(IDictionary storage)
		{
			this._storage = storage;
		}

		// Token: 0x170031CF RID: 12751
		// (get) Token: 0x06009D9A RID: 40346 RVA: 0x002327D7 File Offset: 0x002309D7
		public Dictionary<string, bool> RenderedControls
		{
			get
			{
				if (this._storage["RadAjaxRenderedControls"] == null)
				{
					this._storage["RadAjaxRenderedControls"] = new Dictionary<string, bool>();
				}
				return (Dictionary<string, bool>)this._storage["RadAjaxRenderedControls"];
			}
		}

		// Token: 0x170031D0 RID: 12752
		// (get) Token: 0x06009D9B RID: 40347 RVA: 0x00232815 File Offset: 0x00230A15
		public Dictionary<string, bool> RenderedScripts
		{
			get
			{
				if (this._storage["RadAjaxRenderedScripts"] == null)
				{
					this._storage["RadAjaxRenderedScripts"] = new Dictionary<string, bool>();
				}
				return (Dictionary<string, bool>)this._storage["RadAjaxRenderedScripts"];
			}
		}

		// Token: 0x06009D9C RID: 40348 RVA: 0x00232853 File Offset: 0x00230A53
		public void ControlRendered(Control control)
		{
			this.RenderedControls[control.UniqueID] = true;
		}

		// Token: 0x06009D9D RID: 40349 RVA: 0x00232867 File Offset: 0x00230A67
		public bool ShouldRender(Control control)
		{
			return (!this.RenderedControls.ContainsKey(control.UniqueID) || !this.RenderedControls[control.UniqueID]) && control.Visible;
		}

		// Token: 0x06009D9E RID: 40350 RVA: 0x00232897 File Offset: 0x00230A97
		public void ScriptRendered(Control control)
		{
			this.RenderedScripts[control.UniqueID] = true;
		}

		// Token: 0x06009D9F RID: 40351 RVA: 0x002328AB File Offset: 0x00230AAB
		public bool ShouldRenderScripts(Control control)
		{
			return (!this.RenderedScripts.ContainsKey(control.UniqueID) || !this.RenderedScripts[control.UniqueID]) && control.Visible;
		}

		// Token: 0x04002C5D RID: 11357
		private IDictionary _storage;
	}
}
