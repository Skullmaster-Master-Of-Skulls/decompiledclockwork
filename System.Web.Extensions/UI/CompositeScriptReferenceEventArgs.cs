using System;

namespace System.Web.UI
{
	// Token: 0x02000049 RID: 73
	public class CompositeScriptReferenceEventArgs : EventArgs
	{
		// Token: 0x060002CA RID: 714 RVA: 0x00011A10 File Offset: 0x0000FC10
		public CompositeScriptReferenceEventArgs(CompositeScriptReference compositeScript)
		{
			if (compositeScript == null)
			{
				throw new ArgumentNullException("compositeScript");
			}
			this._compositeScript = compositeScript;
		}

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x060002CB RID: 715 RVA: 0x00011A2D File Offset: 0x0000FC2D
		public CompositeScriptReference CompositeScript
		{
			get
			{
				return this._compositeScript;
			}
		}

		// Token: 0x0400010C RID: 268
		private readonly CompositeScriptReference _compositeScript;
	}
}
