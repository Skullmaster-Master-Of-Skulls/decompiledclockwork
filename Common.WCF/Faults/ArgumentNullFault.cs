using System;
using System.Runtime.Serialization;

namespace TechnoPro.Common.WCF.Faults
{
	// Token: 0x02000012 RID: 18
	[DataContract(Namespace = "http://tpro.ca")]
	public class ArgumentNullFault : ExceptionFault<ArgumentNullException>
	{
		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000071 RID: 113 RVA: 0x00003C25 File Offset: 0x00001E25
		// (set) Token: 0x06000072 RID: 114 RVA: 0x00003C2D File Offset: 0x00001E2D
		[DataMember]
		public string ParamName { get; set; }

		// Token: 0x06000073 RID: 115 RVA: 0x00003C36 File Offset: 0x00001E36
		public ArgumentNullFault()
		{
		}

		// Token: 0x06000074 RID: 116 RVA: 0x00003C40 File Offset: 0x00001E40
		public ArgumentNullFault(string message) : base(message)
		{
		}

		// Token: 0x06000075 RID: 117 RVA: 0x00003C4B File Offset: 0x00001E4B
		public override void ConvertFrom(ArgumentNullException exception)
		{
			this.ParamName = exception.ParamName;
			base.Message = exception.Message;
		}
	}
}
