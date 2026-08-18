using System;
using System.Runtime.Serialization;

namespace <CrtImplementationDetails>
{
	// Token: 0x02000014 RID: 20
	[Serializable]
	internal class ModuleLoadException : Exception
	{
		// Token: 0x06000079 RID: 121 RVA: 0x001D692C File Offset: 0x001D5D2C
		protected ModuleLoadException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600007A RID: 122 RVA: 0x001D6914 File Offset: 0x001D5D14
		public ModuleLoadException(string message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x0600007B RID: 123 RVA: 0x001D6900 File Offset: 0x001D5D00
		public ModuleLoadException(string message) : base(message)
		{
		}

		// Token: 0x04000075 RID: 117
		public const string Nested = "A nested exception occurred after the primary exception that caused the C++ module to fail to load.\n";
	}
}
