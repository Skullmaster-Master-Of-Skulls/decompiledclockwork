using System;
using System.Runtime.Serialization;

namespace <CrtImplementationDetails>
{
	// Token: 0x0200001F RID: 31
	[Serializable]
	internal class ModuleLoadException : Exception
	{
		// Token: 0x060000A1 RID: 161 RVA: 0x000055FC File Offset: 0x000049FC
		protected ModuleLoadException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x000055E4 File Offset: 0x000049E4
		public ModuleLoadException(string message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x000055D0 File Offset: 0x000049D0
		public ModuleLoadException(string message) : base(message)
		{
		}

		// Token: 0x040000A1 RID: 161
		public const string Nested = "A nested exception occurred after the primary exception that caused the C++ module to fail to load.\n";
	}
}
