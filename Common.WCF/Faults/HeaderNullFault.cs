using System;
using System.Runtime.Serialization;

namespace TechnoPro.Common.WCF.Faults
{
	// Token: 0x02000013 RID: 19
	[DataContract(Namespace = "http://tpro.ca")]
	public class HeaderNullFault : GenericFault
	{
		// Token: 0x06000076 RID: 118 RVA: 0x00003C68 File Offset: 0x00001E68
		public HeaderNullFault() : this("Header values not specified for the operation.")
		{
		}

		// Token: 0x06000077 RID: 119 RVA: 0x00002EC3 File Offset: 0x000010C3
		public HeaderNullFault(string message) : base(message)
		{
		}
	}
}
