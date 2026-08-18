using System;
using System.Collections.Generic;
using System.Web.UI;

namespace Telerik.Web.UI.Common
{
	// Token: 0x02001829 RID: 6185
	internal class RadControlRenderHelper : RadWebControl
	{
		// Token: 0x170048B4 RID: 18612
		// (get) Token: 0x0600F07E RID: 61566 RVA: 0x0036AC01 File Offset: 0x00368E01
		public override bool EnableEmbeddedBaseStylesheet
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170048B5 RID: 18613
		// (get) Token: 0x0600F07F RID: 61567 RVA: 0x0036AC04 File Offset: 0x00368E04
		public override bool EnableEmbeddedSkins
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170048B6 RID: 18614
		// (get) Token: 0x0600F080 RID: 61568 RVA: 0x0036AC07 File Offset: 0x00368E07
		// (set) Token: 0x0600F081 RID: 61569 RVA: 0x0036AC18 File Offset: 0x00368E18
		public List<ScriptReference> ScriptReferences
		{
			get
			{
				return this._scriptReferences ?? new List<ScriptReference>();
			}
			set
			{
				this._scriptReferences = value;
			}
		}

		// Token: 0x0600F082 RID: 61570 RVA: 0x0036AC21 File Offset: 0x00368E21
		protected override IEnumerable<ScriptReference> GetScriptReferences()
		{
			return this.ScriptReferences;
		}

		// Token: 0x0400454A RID: 17738
		private List<ScriptReference> _scriptReferences;
	}
}
