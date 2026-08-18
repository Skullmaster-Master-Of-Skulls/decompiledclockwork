using System;
using System.CodeDom;
using Microsoft.Internal.Web.Utils;

namespace System.Web.Razor.Generator
{
	// Token: 0x02000053 RID: 83
	public class CodeGenerationCompleteEventArgs : EventArgs
	{
		// Token: 0x060003D6 RID: 982 RVA: 0x00010F54 File Offset: 0x0000F154
		public CodeGenerationCompleteEventArgs(string virtualPath, string physicalPath, CodeCompileUnit generatedCode)
		{
			if (string.IsNullOrEmpty(virtualPath))
			{
				throw new ArgumentException(CommonResources.Argument_Cannot_Be_Null_Or_Empty, "virtualPath");
			}
			if (generatedCode == null)
			{
				throw new ArgumentNullException("generatedCode");
			}
			this.VirtualPath = virtualPath;
			this.PhysicalPath = physicalPath;
			this.GeneratedCode = generatedCode;
		}

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x060003D7 RID: 983 RVA: 0x00010FA2 File Offset: 0x0000F1A2
		// (set) Token: 0x060003D8 RID: 984 RVA: 0x00010FAA File Offset: 0x0000F1AA
		public CodeCompileUnit GeneratedCode { get; private set; }

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x060003D9 RID: 985 RVA: 0x00010FB3 File Offset: 0x0000F1B3
		// (set) Token: 0x060003DA RID: 986 RVA: 0x00010FBB File Offset: 0x0000F1BB
		public string VirtualPath { get; private set; }

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x060003DB RID: 987 RVA: 0x00010FC4 File Offset: 0x0000F1C4
		// (set) Token: 0x060003DC RID: 988 RVA: 0x00010FCC File Offset: 0x0000F1CC
		public string PhysicalPath { get; private set; }
	}
}
