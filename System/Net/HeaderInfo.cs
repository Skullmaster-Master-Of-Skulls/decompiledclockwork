using System;

namespace System.Net
{
	// Token: 0x020004E0 RID: 1248
	internal class HeaderInfo
	{
		// Token: 0x060026E4 RID: 9956 RVA: 0x0009FEDA File Offset: 0x0009EEDA
		internal HeaderInfo(string name, bool requestRestricted, bool responseRestricted, bool multi, HeaderParser p)
		{
			this.HeaderName = name;
			this.IsRequestRestricted = requestRestricted;
			this.IsResponseRestricted = responseRestricted;
			this.Parser = p;
			this.AllowMultiValues = multi;
		}

		// Token: 0x04002665 RID: 9829
		internal readonly bool IsRequestRestricted;

		// Token: 0x04002666 RID: 9830
		internal readonly bool IsResponseRestricted;

		// Token: 0x04002667 RID: 9831
		internal readonly HeaderParser Parser;

		// Token: 0x04002668 RID: 9832
		internal readonly string HeaderName;

		// Token: 0x04002669 RID: 9833
		internal readonly bool AllowMultiValues;
	}
}
