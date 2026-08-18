using System;

namespace TechnoPro.Common.DynamicCompiler.CompilerArgs.DynamicForms
{
	// Token: 0x02000015 RID: 21
	public class DynamicFormCompileParameters : ICompilerParameters
	{
		// Token: 0x17000038 RID: 56
		// (get) Token: 0x06000098 RID: 152 RVA: 0x00003E80 File Offset: 0x00002080
		// (set) Token: 0x06000099 RID: 153 RVA: 0x00003E88 File Offset: 0x00002088
		public int WhoAmI { get; set; }

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x0600009A RID: 154 RVA: 0x00003E91 File Offset: 0x00002091
		// (set) Token: 0x0600009B RID: 155 RVA: 0x00003E99 File Offset: 0x00002099
		public CompileContext Context { get; set; }

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x0600009C RID: 156 RVA: 0x00003EA2 File Offset: 0x000020A2
		// (set) Token: 0x0600009D RID: 157 RVA: 0x00003EAA File Offset: 0x000020AA
		public object DynamicFormParentPanel { get; set; }
	}
}
