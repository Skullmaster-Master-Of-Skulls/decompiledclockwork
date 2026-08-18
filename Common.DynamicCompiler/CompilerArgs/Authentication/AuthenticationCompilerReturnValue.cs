using System;
using System.Collections.Generic;

namespace TechnoPro.Common.DynamicCompiler.CompilerArgs.Authentication
{
	// Token: 0x02000018 RID: 24
	public class AuthenticationCompilerReturnValue : ICompilerReturnValue
	{
		// Token: 0x17000040 RID: 64
		// (get) Token: 0x060000AB RID: 171 RVA: 0x00003F08 File Offset: 0x00002108
		// (set) Token: 0x060000AC RID: 172 RVA: 0x00003F10 File Offset: 0x00002110
		public object AuthenticationContext { get; set; }

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x060000AD RID: 173 RVA: 0x00003F19 File Offset: 0x00002119
		// (set) Token: 0x060000AE RID: 174 RVA: 0x00003F21 File Offset: 0x00002121
		public object AuthorizationContext { get; set; }

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x060000AF RID: 175 RVA: 0x00003F2A File Offset: 0x0000212A
		// (set) Token: 0x060000B0 RID: 176 RVA: 0x00003F32 File Offset: 0x00002132
		public string UserName { get; set; }

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x060000B1 RID: 177 RVA: 0x00003F3B File Offset: 0x0000213B
		// (set) Token: 0x060000B2 RID: 178 RVA: 0x00003F43 File Offset: 0x00002143
		public string Password { get; set; }

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x060000B3 RID: 179 RVA: 0x00003F4C File Offset: 0x0000214C
		// (set) Token: 0x060000B4 RID: 180 RVA: 0x00003F54 File Offset: 0x00002154
		public IDictionary<string, string> AuthenticationArgs { get; set; }
	}
}
