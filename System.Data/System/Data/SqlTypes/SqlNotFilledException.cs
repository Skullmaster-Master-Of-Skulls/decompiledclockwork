using System;
using System.Runtime.Serialization;

namespace System.Data.SqlTypes
{
	// Token: 0x02000375 RID: 885
	[Serializable]
	public sealed class SqlNotFilledException : SqlTypeException
	{
		// Token: 0x06002F36 RID: 12086 RVA: 0x002D3BA8 File Offset: 0x002D2FA8
		public SqlNotFilledException() : this(SQLResource.NotFilledMessage, null)
		{
		}

		// Token: 0x06002F37 RID: 12087 RVA: 0x002D3BC8 File Offset: 0x002D2FC8
		public SqlNotFilledException(string message) : this(message, null)
		{
		}

		// Token: 0x06002F38 RID: 12088 RVA: 0x002D3BE8 File Offset: 0x002D2FE8
		public SqlNotFilledException(string message, Exception e) : base(message, e)
		{
			base.HResult = -2146232015;
		}

		// Token: 0x06002F39 RID: 12089 RVA: 0x002D3C08 File Offset: 0x002D3008
		private SqlNotFilledException(SerializationInfo si, StreamingContext sc) : base(si, sc)
		{
		}
	}
}
