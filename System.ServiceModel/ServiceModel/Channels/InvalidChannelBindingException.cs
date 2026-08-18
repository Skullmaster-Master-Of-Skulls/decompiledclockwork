using System;
using System.Runtime.Serialization;

namespace System.ServiceModel.Channels
{
	// Token: 0x020009A4 RID: 2468
	[Serializable]
	public class InvalidChannelBindingException : Exception
	{
		// Token: 0x060060DA RID: 24794 RVA: 0x00169F33 File Offset: 0x00168133
		public InvalidChannelBindingException()
		{
		}

		// Token: 0x060060DB RID: 24795 RVA: 0x00169F3B File Offset: 0x0016813B
		public InvalidChannelBindingException(string message) : base(message)
		{
		}

		// Token: 0x060060DC RID: 24796 RVA: 0x00169F44 File Offset: 0x00168144
		public InvalidChannelBindingException(string message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x060060DD RID: 24797 RVA: 0x00169F4E File Offset: 0x0016814E
		protected InvalidChannelBindingException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
