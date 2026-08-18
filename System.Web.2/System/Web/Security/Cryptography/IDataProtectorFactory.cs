using System;
using System.Security.Cryptography;

namespace System.Web.Security.Cryptography
{
	// Token: 0x02000601 RID: 1537
	internal interface IDataProtectorFactory
	{
		// Token: 0x06004DA0 RID: 19872
		DataProtector GetDataProtector(Purpose purpose);
	}
}
