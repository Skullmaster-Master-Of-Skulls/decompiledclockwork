using System;
using System.CodeDom;

namespace System.ComponentModel.Design.Serialization
{
	// Token: 0x020001E4 RID: 484
	public abstract class MemberCodeDomSerializer : CodeDomSerializerBase
	{
		// Token: 0x06001227 RID: 4647
		public abstract void Serialize(IDesignerSerializationManager manager, object value, MemberDescriptor descriptor, CodeStatementCollection statements);

		// Token: 0x06001228 RID: 4648
		public abstract bool ShouldSerialize(IDesignerSerializationManager manager, object value, MemberDescriptor descriptor);
	}
}
