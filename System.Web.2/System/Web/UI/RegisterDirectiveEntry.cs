using System;

namespace System.Web.UI
{
	// Token: 0x02000242 RID: 578
	internal abstract class RegisterDirectiveEntry : SourceLineInfo
	{
		// Token: 0x06001AE9 RID: 6889 RVA: 0x00054821 File Offset: 0x00052A21
		internal RegisterDirectiveEntry(string tagPrefix)
		{
			this._tagPrefix = tagPrefix;
		}

		// Token: 0x17000790 RID: 1936
		// (get) Token: 0x06001AEA RID: 6890 RVA: 0x00054830 File Offset: 0x00052A30
		internal string TagPrefix
		{
			get
			{
				return this._tagPrefix;
			}
		}

		// Token: 0x04001876 RID: 6262
		private string _tagPrefix;
	}
}
