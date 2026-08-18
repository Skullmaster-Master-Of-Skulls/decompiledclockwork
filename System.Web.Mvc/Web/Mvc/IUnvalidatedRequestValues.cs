using System;
using System.Collections.Specialized;

namespace System.Web.Mvc
{
	// Token: 0x020000A3 RID: 163
	internal interface IUnvalidatedRequestValues
	{
		// Token: 0x17000180 RID: 384
		// (get) Token: 0x0600047A RID: 1146
		NameValueCollection Form { get; }

		// Token: 0x17000181 RID: 385
		// (get) Token: 0x0600047B RID: 1147
		NameValueCollection QueryString { get; }

		// Token: 0x17000182 RID: 386
		string this[string key]
		{
			get;
		}
	}
}
