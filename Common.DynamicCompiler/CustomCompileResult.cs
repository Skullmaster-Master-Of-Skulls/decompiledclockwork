using System;
using System.Collections.Generic;
using System.Reflection;

namespace TechnoPro.Common.DynamicCompiler
{
	// Token: 0x02000007 RID: 7
	public class CustomCompileResult
	{
		// Token: 0x06000033 RID: 51 RVA: 0x00002D9D File Offset: 0x00000F9D
		public CustomCompileResult()
		{
			this.Warnings = new List<CustomCompileMessage>();
			this.Errors = new List<CustomCompileMessage>();
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000034 RID: 52 RVA: 0x00002DBF File Offset: 0x00000FBF
		// (set) Token: 0x06000035 RID: 53 RVA: 0x00002DC7 File Offset: 0x00000FC7
		public bool Success { get; set; }

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000036 RID: 54 RVA: 0x00002DD0 File Offset: 0x00000FD0
		// (set) Token: 0x06000037 RID: 55 RVA: 0x00002DD8 File Offset: 0x00000FD8
		public Assembly Assembly { get; set; }

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000038 RID: 56 RVA: 0x00002DE1 File Offset: 0x00000FE1
		// (set) Token: 0x06000039 RID: 57 RVA: 0x00002DE9 File Offset: 0x00000FE9
		public string ErrorMessage { get; set; }

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x0600003A RID: 58 RVA: 0x00002DF2 File Offset: 0x00000FF2
		// (set) Token: 0x0600003B RID: 59 RVA: 0x00002DFA File Offset: 0x00000FFA
		public IList<CustomCompileMessage> Errors { get; set; }

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x0600003C RID: 60 RVA: 0x00002E03 File Offset: 0x00001003
		// (set) Token: 0x0600003D RID: 61 RVA: 0x00002E0B File Offset: 0x0000100B
		public IList<CustomCompileMessage> Warnings { get; set; }
	}
}
