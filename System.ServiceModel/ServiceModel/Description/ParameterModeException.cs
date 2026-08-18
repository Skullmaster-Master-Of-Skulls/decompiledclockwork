using System;
using System.Runtime.Serialization;

namespace System.ServiceModel.Description
{
	// Token: 0x02000417 RID: 1047
	internal class ParameterModeException : Exception
	{
		// Token: 0x06002811 RID: 10257 RVA: 0x00096E64 File Offset: 0x00095064
		public ParameterModeException()
		{
		}

		// Token: 0x06002812 RID: 10258 RVA: 0x00096E73 File Offset: 0x00095073
		public ParameterModeException(string message) : base(message)
		{
		}

		// Token: 0x06002813 RID: 10259 RVA: 0x00096E83 File Offset: 0x00095083
		public ParameterModeException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x170009F6 RID: 2550
		// (get) Token: 0x06002814 RID: 10260 RVA: 0x00096E94 File Offset: 0x00095094
		// (set) Token: 0x06002815 RID: 10261 RVA: 0x00096E9C File Offset: 0x0009509C
		public MessageContractType MessageContractType
		{
			get
			{
				return this.messageContractType;
			}
			set
			{
				this.messageContractType = value;
			}
		}

		// Token: 0x04002214 RID: 8724
		private MessageContractType messageContractType = MessageContractType.WrappedMessageContract;
	}
}
