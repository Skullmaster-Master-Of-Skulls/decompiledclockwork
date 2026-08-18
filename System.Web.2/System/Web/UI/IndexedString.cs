using System;

namespace System.Web.UI
{
	// Token: 0x020002AD RID: 685
	[Serializable]
	public sealed class IndexedString
	{
		// Token: 0x06001FAC RID: 8108 RVA: 0x00065723 File Offset: 0x00063923
		public IndexedString(string s)
		{
			if (string.IsNullOrEmpty(s))
			{
				throw new ArgumentNullException("s");
			}
			this._value = s;
		}

		// Token: 0x170008CC RID: 2252
		// (get) Token: 0x06001FAD RID: 8109 RVA: 0x00065745 File Offset: 0x00063945
		public string Value
		{
			get
			{
				return this._value;
			}
		}

		// Token: 0x04001AB9 RID: 6841
		private string _value;
	}
}
