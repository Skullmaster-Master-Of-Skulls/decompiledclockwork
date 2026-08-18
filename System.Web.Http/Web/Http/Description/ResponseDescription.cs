using System;

namespace System.Web.Http.Description
{
	// Token: 0x02000036 RID: 54
	public class ResponseDescription
	{
		// Token: 0x17000050 RID: 80
		// (get) Token: 0x06000144 RID: 324 RVA: 0x00007018 File Offset: 0x00005218
		// (set) Token: 0x06000145 RID: 325 RVA: 0x00007020 File Offset: 0x00005220
		public Type DeclaredType { get; set; }

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x06000146 RID: 326 RVA: 0x00007029 File Offset: 0x00005229
		// (set) Token: 0x06000147 RID: 327 RVA: 0x00007031 File Offset: 0x00005231
		public Type ResponseType { get; set; }

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x06000148 RID: 328 RVA: 0x0000703A File Offset: 0x0000523A
		// (set) Token: 0x06000149 RID: 329 RVA: 0x00007042 File Offset: 0x00005242
		public string Documentation { get; set; }
	}
}
