using System;

namespace TechnoPro.Common.DynamicCompiler.CompilerArgs.MagneticCard
{
	// Token: 0x02000013 RID: 19
	public class MagneticCardCompilerParameters : ICompilerParameters
	{
		// Token: 0x17000034 RID: 52
		// (get) Token: 0x0600008E RID: 142 RVA: 0x00003E3C File Offset: 0x0000203C
		// (set) Token: 0x0600008F RID: 143 RVA: 0x00003E44 File Offset: 0x00002044
		public int WhoAmI { get; set; }

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x06000090 RID: 144 RVA: 0x00003E4D File Offset: 0x0000204D
		// (set) Token: 0x06000091 RID: 145 RVA: 0x00003E55 File Offset: 0x00002055
		public CompileContext Context { get; set; }

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x06000092 RID: 146 RVA: 0x00003E5E File Offset: 0x0000205E
		// (set) Token: 0x06000093 RID: 147 RVA: 0x00003E66 File Offset: 0x00002066
		public string MagneticCardReaderOutput { get; set; }
	}
}
