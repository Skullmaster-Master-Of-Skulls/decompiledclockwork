using System;
using System.Collections.Generic;

namespace TechnoPro.Common.DynamicCompiler.CompilerArgs.Authentication
{
	// Token: 0x02000017 RID: 23
	public class AuthenticationCompilerParameters : ICompilerParameters
	{
		// Token: 0x1700003B RID: 59
		// (get) Token: 0x060000A0 RID: 160 RVA: 0x00003EB3 File Offset: 0x000020B3
		// (set) Token: 0x060000A1 RID: 161 RVA: 0x00003EBB File Offset: 0x000020BB
		public object AuthenticationContext { get; set; }

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x060000A2 RID: 162 RVA: 0x00003EC4 File Offset: 0x000020C4
		// (set) Token: 0x060000A3 RID: 163 RVA: 0x00003ECC File Offset: 0x000020CC
		public object AuthorizationContext { get; set; }

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x060000A4 RID: 164 RVA: 0x00003ED5 File Offset: 0x000020D5
		// (set) Token: 0x060000A5 RID: 165 RVA: 0x00003EDD File Offset: 0x000020DD
		public string UserName { get; set; }

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x060000A6 RID: 166 RVA: 0x00003EE6 File Offset: 0x000020E6
		// (set) Token: 0x060000A7 RID: 167 RVA: 0x00003EEE File Offset: 0x000020EE
		public string Password { get; set; }

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x060000A8 RID: 168 RVA: 0x00003EF7 File Offset: 0x000020F7
		// (set) Token: 0x060000A9 RID: 169 RVA: 0x00003EFF File Offset: 0x000020FF
		public IDictionary<string, string> AuthenticationArgs { get; set; }
	}
}
