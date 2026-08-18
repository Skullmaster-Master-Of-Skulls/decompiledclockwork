using System;
using System.Runtime.Serialization;

namespace System.IdentityModel.Metadata
{
	// Token: 0x020000FE RID: 254
	[Serializable]
	public class MetadataSerializationException : Exception
	{
		// Token: 0x060006C6 RID: 1734 RVA: 0x0001ABF9 File Offset: 0x00018DF9
		public MetadataSerializationException() : this(SR.GetString("ID3198"))
		{
		}

		// Token: 0x060006C7 RID: 1735 RVA: 0x0000544D File Offset: 0x0000364D
		public MetadataSerializationException(string message) : base(message)
		{
		}

		// Token: 0x060006C8 RID: 1736 RVA: 0x00005456 File Offset: 0x00003656
		public MetadataSerializationException(string message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x060006C9 RID: 1737 RVA: 0x00005473 File Offset: 0x00003673
		protected MetadataSerializationException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
