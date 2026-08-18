using System;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x02000E82 RID: 3714
	public class ScriptReferenceGroup
	{
		// Token: 0x17002C81 RID: 11393
		// (get) Token: 0x06008CE5 RID: 36069 RVA: 0x001FFFDE File Offset: 0x001FE1DE
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public ScriptReferenceCollection Scripts
		{
			get
			{
				if (this._scripts == null)
				{
					this._scripts = new ScriptReferenceCollection();
				}
				return this._scripts;
			}
		}

		// Token: 0x04002790 RID: 10128
		private ScriptReferenceCollection _scripts;
	}
}
