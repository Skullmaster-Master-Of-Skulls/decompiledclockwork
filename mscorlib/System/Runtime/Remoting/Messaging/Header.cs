using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Messaging
{
	// Token: 0x020006DC RID: 1756
	[ComVisible(true)]
	[Serializable]
	public class Header
	{
		// Token: 0x06003F24 RID: 16164 RVA: 0x000D8445 File Offset: 0x000D7445
		public Header(string _Name, object _Value) : this(_Name, _Value, true)
		{
		}

		// Token: 0x06003F25 RID: 16165 RVA: 0x000D8450 File Offset: 0x000D7450
		public Header(string _Name, object _Value, bool _MustUnderstand)
		{
			this.Name = _Name;
			this.Value = _Value;
			this.MustUnderstand = _MustUnderstand;
		}

		// Token: 0x06003F26 RID: 16166 RVA: 0x000D846D File Offset: 0x000D746D
		public Header(string _Name, object _Value, bool _MustUnderstand, string _HeaderNamespace)
		{
			this.Name = _Name;
			this.Value = _Value;
			this.MustUnderstand = _MustUnderstand;
			this.HeaderNamespace = _HeaderNamespace;
		}

		// Token: 0x0400200B RID: 8203
		public string Name;

		// Token: 0x0400200C RID: 8204
		public object Value;

		// Token: 0x0400200D RID: 8205
		public bool MustUnderstand;

		// Token: 0x0400200E RID: 8206
		public string HeaderNamespace;
	}
}
