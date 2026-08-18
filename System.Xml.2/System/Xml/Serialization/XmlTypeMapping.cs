using System;

namespace System.Xml.Serialization
{
	// Token: 0x020001C0 RID: 448
	[__DynamicallyInvokable]
	public class XmlTypeMapping : XmlMapping
	{
		// Token: 0x06001EF1 RID: 7921 RVA: 0x000A8FD5 File Offset: 0x000A71D5
		internal XmlTypeMapping(TypeScope scope, ElementAccessor accessor) : base(scope, accessor)
		{
		}

		// Token: 0x17000654 RID: 1620
		// (get) Token: 0x06001EF2 RID: 7922 RVA: 0x000A8FDF File Offset: 0x000A71DF
		internal TypeMapping Mapping
		{
			get
			{
				return base.Accessor.Mapping;
			}
		}

		// Token: 0x17000655 RID: 1621
		// (get) Token: 0x06001EF3 RID: 7923 RVA: 0x000A8FEC File Offset: 0x000A71EC
		[__DynamicallyInvokable]
		public string TypeName
		{
			[__DynamicallyInvokable]
			get
			{
				return this.Mapping.TypeDesc.Name;
			}
		}

		// Token: 0x17000656 RID: 1622
		// (get) Token: 0x06001EF4 RID: 7924 RVA: 0x000A8FFE File Offset: 0x000A71FE
		[__DynamicallyInvokable]
		public string TypeFullName
		{
			[__DynamicallyInvokable]
			get
			{
				return this.Mapping.TypeDesc.FullName;
			}
		}

		// Token: 0x17000657 RID: 1623
		// (get) Token: 0x06001EF5 RID: 7925 RVA: 0x000A9010 File Offset: 0x000A7210
		[__DynamicallyInvokable]
		public string XsdTypeName
		{
			[__DynamicallyInvokable]
			get
			{
				return this.Mapping.TypeName;
			}
		}

		// Token: 0x17000658 RID: 1624
		// (get) Token: 0x06001EF6 RID: 7926 RVA: 0x000A901D File Offset: 0x000A721D
		[__DynamicallyInvokable]
		public string XsdTypeNamespace
		{
			[__DynamicallyInvokable]
			get
			{
				return this.Mapping.Namespace;
			}
		}
	}
}
