using System;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Messaging;

namespace System.Runtime.Serialization.Formatters
{
	// Token: 0x020007B7 RID: 1975
	[ComVisible(true)]
	public interface ISoapMessage
	{
		// Token: 0x17000C5D RID: 3165
		// (get) Token: 0x06004668 RID: 18024
		// (set) Token: 0x06004669 RID: 18025
		string[] ParamNames { get; set; }

		// Token: 0x17000C5E RID: 3166
		// (get) Token: 0x0600466A RID: 18026
		// (set) Token: 0x0600466B RID: 18027
		object[] ParamValues { get; set; }

		// Token: 0x17000C5F RID: 3167
		// (get) Token: 0x0600466C RID: 18028
		// (set) Token: 0x0600466D RID: 18029
		Type[] ParamTypes { get; set; }

		// Token: 0x17000C60 RID: 3168
		// (get) Token: 0x0600466E RID: 18030
		// (set) Token: 0x0600466F RID: 18031
		string MethodName { get; set; }

		// Token: 0x17000C61 RID: 3169
		// (get) Token: 0x06004670 RID: 18032
		// (set) Token: 0x06004671 RID: 18033
		string XmlNameSpace { get; set; }

		// Token: 0x17000C62 RID: 3170
		// (get) Token: 0x06004672 RID: 18034
		// (set) Token: 0x06004673 RID: 18035
		Header[] Headers { get; set; }
	}
}
