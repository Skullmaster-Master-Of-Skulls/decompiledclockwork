using System;

namespace System.Xml.Serialization
{
	// Token: 0x02000196 RID: 406
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Method | AttributeTargets.Interface, AllowMultiple = true)]
	[__DynamicallyInvokable]
	public class XmlIncludeAttribute : Attribute
	{
		// Token: 0x06001ADD RID: 6877 RVA: 0x00076E37 File Offset: 0x00075037
		[__DynamicallyInvokable]
		public XmlIncludeAttribute(Type type)
		{
			this.type = type;
		}

		// Token: 0x170005E0 RID: 1504
		// (get) Token: 0x06001ADE RID: 6878 RVA: 0x00076E46 File Offset: 0x00075046
		// (set) Token: 0x06001ADF RID: 6879 RVA: 0x00076E4E File Offset: 0x0007504E
		[__DynamicallyInvokable]
		public Type Type
		{
			[__DynamicallyInvokable]
			get
			{
				return this.type;
			}
			[__DynamicallyInvokable]
			set
			{
				this.type = value;
			}
		}

		// Token: 0x04000BF5 RID: 3061
		private Type type;
	}
}
