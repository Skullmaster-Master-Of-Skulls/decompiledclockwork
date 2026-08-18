using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.Data.Common
{
	// Token: 0x020002F3 RID: 755
	[Serializable]
	public abstract class DbException : ExternalException
	{
		// Token: 0x0600302B RID: 12331 RVA: 0x0012E278 File Offset: 0x0012D678
		protected DbException()
		{
		}

		// Token: 0x0600302C RID: 12332 RVA: 0x0012E28C File Offset: 0x0012D68C
		protected DbException(string message) : base(message)
		{
		}

		// Token: 0x0600302D RID: 12333 RVA: 0x0012E2A0 File Offset: 0x0012D6A0
		protected DbException(string message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x0600302E RID: 12334 RVA: 0x0012E2B8 File Offset: 0x0012D6B8
		protected DbException(string message, int errorCode) : base(message, errorCode)
		{
		}

		// Token: 0x0600302F RID: 12335 RVA: 0x0012E2D0 File Offset: 0x0012D6D0
		protected DbException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
