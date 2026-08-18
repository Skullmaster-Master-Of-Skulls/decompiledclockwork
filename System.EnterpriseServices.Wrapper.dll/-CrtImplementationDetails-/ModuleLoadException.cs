using System;
using System.Runtime.Serialization;

namespace <CrtImplementationDetails>
{
	// Token: 0x020000AC RID: 172
	[Serializable]
	internal class ModuleLoadException : Exception
	{
		// Token: 0x06000111 RID: 273 RVA: 0x00006C30 File Offset: 0x00006030
		protected ModuleLoadException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06000112 RID: 274 RVA: 0x00006C18 File Offset: 0x00006018
		public ModuleLoadException(string message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06000113 RID: 275 RVA: 0x00006C04 File Offset: 0x00006004
		public ModuleLoadException(string message) : base(message)
		{
		}

		// Token: 0x0400016C RID: 364
		public const string Nested = "A nested exception occurred after the primary exception that caused the C++ module to fail to load.\n";
	}
}
