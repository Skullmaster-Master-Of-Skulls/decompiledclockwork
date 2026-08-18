using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.IO
{
	// Token: 0x020005AF RID: 1455
	[ComVisible(true)]
	[Serializable]
	public class DirectoryNotFoundException : IOException
	{
		// Token: 0x0600359A RID: 13722 RVA: 0x000B2AC9 File Offset: 0x000B1AC9
		public DirectoryNotFoundException() : base(Environment.GetResourceString("Arg_DirectoryNotFoundException"))
		{
			base.SetErrorCode(-2147024893);
		}

		// Token: 0x0600359B RID: 13723 RVA: 0x000B2AE6 File Offset: 0x000B1AE6
		public DirectoryNotFoundException(string message) : base(message)
		{
			base.SetErrorCode(-2147024893);
		}

		// Token: 0x0600359C RID: 13724 RVA: 0x000B2AFA File Offset: 0x000B1AFA
		public DirectoryNotFoundException(string message, Exception innerException) : base(message, innerException)
		{
			base.SetErrorCode(-2147024893);
		}

		// Token: 0x0600359D RID: 13725 RVA: 0x000B2B0F File Offset: 0x000B1B0F
		protected DirectoryNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
