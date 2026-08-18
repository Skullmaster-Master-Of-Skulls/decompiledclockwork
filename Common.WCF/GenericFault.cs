using System;
using System.Runtime.Serialization;

namespace TechnoPro.Common.WCF
{
	// Token: 0x02000009 RID: 9
	[DataContract(Namespace = "http://tpro.ca")]
	public class GenericFault
	{
		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600003F RID: 63 RVA: 0x00002F07 File Offset: 0x00001107
		// (set) Token: 0x06000040 RID: 64 RVA: 0x00002F0F File Offset: 0x0000110F
		[DataMember]
		public string Message { get; set; }

		// Token: 0x06000041 RID: 65 RVA: 0x00002F18 File Offset: 0x00001118
		public GenericFault()
		{
			this.Message = string.Empty;
		}

		// Token: 0x06000042 RID: 66 RVA: 0x00002F2E File Offset: 0x0000112E
		public GenericFault(string message)
		{
			this.Message = message;
		}
	}
}
