using System;
using System.Runtime.Serialization;

namespace TechnoPro.Common.WCF
{
	// Token: 0x02000011 RID: 17
	[DataContract(Namespace = "http://tpro.ca")]
	public class UnexpectedFault : GenericFault
	{
		// Token: 0x17000015 RID: 21
		// (get) Token: 0x0600006E RID: 110 RVA: 0x00003BF2 File Offset: 0x00001DF2
		// (set) Token: 0x0600006F RID: 111 RVA: 0x00003BFA File Offset: 0x00001DFA
		[DataMember]
		public string ExceptionName { get; set; }

		// Token: 0x06000070 RID: 112 RVA: 0x00003C03 File Offset: 0x00001E03
		public UnexpectedFault(Exception innerException) : base(innerException.Message)
		{
			this.ExceptionName = innerException.GetType().Name;
		}
	}
}
