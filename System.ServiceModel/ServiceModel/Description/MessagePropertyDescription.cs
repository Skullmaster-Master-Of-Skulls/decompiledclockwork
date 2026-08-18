using System;

namespace System.ServiceModel.Description
{
	// Token: 0x020003D5 RID: 981
	[__DynamicallyInvokable]
	public class MessagePropertyDescription : MessagePartDescription
	{
		// Token: 0x060024E4 RID: 9444 RVA: 0x00084C85 File Offset: 0x00082E85
		[__DynamicallyInvokable]
		public MessagePropertyDescription(string name) : base(name, "")
		{
		}

		// Token: 0x060024E5 RID: 9445 RVA: 0x00084C93 File Offset: 0x00082E93
		internal MessagePropertyDescription(MessagePropertyDescription other) : base(other)
		{
		}

		// Token: 0x060024E6 RID: 9446 RVA: 0x00084C9C File Offset: 0x00082E9C
		internal override MessagePartDescription Clone()
		{
			return new MessagePropertyDescription(this);
		}
	}
}
