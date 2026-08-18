using System;

namespace System.Xml.Serialization
{
	// Token: 0x020001BF RID: 447
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum | AttributeTargets.Interface)]
	[__DynamicallyInvokable]
	public class XmlTypeAttribute : Attribute
	{
		// Token: 0x06001EE7 RID: 7911 RVA: 0x000A8F5E File Offset: 0x000A715E
		[__DynamicallyInvokable]
		public XmlTypeAttribute()
		{
		}

		// Token: 0x06001EE8 RID: 7912 RVA: 0x000A8F6D File Offset: 0x000A716D
		[__DynamicallyInvokable]
		public XmlTypeAttribute(string typeName)
		{
			this.typeName = typeName;
		}

		// Token: 0x17000650 RID: 1616
		// (get) Token: 0x06001EE9 RID: 7913 RVA: 0x000A8F83 File Offset: 0x000A7183
		// (set) Token: 0x06001EEA RID: 7914 RVA: 0x000A8F8B File Offset: 0x000A718B
		[__DynamicallyInvokable]
		public bool AnonymousType
		{
			[__DynamicallyInvokable]
			get
			{
				return this.anonymousType;
			}
			[__DynamicallyInvokable]
			set
			{
				this.anonymousType = value;
			}
		}

		// Token: 0x17000651 RID: 1617
		// (get) Token: 0x06001EEB RID: 7915 RVA: 0x000A8F94 File Offset: 0x000A7194
		// (set) Token: 0x06001EEC RID: 7916 RVA: 0x000A8F9C File Offset: 0x000A719C
		[__DynamicallyInvokable]
		public bool IncludeInSchema
		{
			[__DynamicallyInvokable]
			get
			{
				return this.includeInSchema;
			}
			[__DynamicallyInvokable]
			set
			{
				this.includeInSchema = value;
			}
		}

		// Token: 0x17000652 RID: 1618
		// (get) Token: 0x06001EED RID: 7917 RVA: 0x000A8FA5 File Offset: 0x000A71A5
		// (set) Token: 0x06001EEE RID: 7918 RVA: 0x000A8FBB File Offset: 0x000A71BB
		[__DynamicallyInvokable]
		public string TypeName
		{
			[__DynamicallyInvokable]
			get
			{
				if (this.typeName != null)
				{
					return this.typeName;
				}
				return string.Empty;
			}
			[__DynamicallyInvokable]
			set
			{
				this.typeName = value;
			}
		}

		// Token: 0x17000653 RID: 1619
		// (get) Token: 0x06001EEF RID: 7919 RVA: 0x000A8FC4 File Offset: 0x000A71C4
		// (set) Token: 0x06001EF0 RID: 7920 RVA: 0x000A8FCC File Offset: 0x000A71CC
		[__DynamicallyInvokable]
		public string Namespace
		{
			[__DynamicallyInvokable]
			get
			{
				return this.ns;
			}
			[__DynamicallyInvokable]
			set
			{
				this.ns = value;
			}
		}

		// Token: 0x04000CF0 RID: 3312
		private bool includeInSchema = true;

		// Token: 0x04000CF1 RID: 3313
		private bool anonymousType;

		// Token: 0x04000CF2 RID: 3314
		private string ns;

		// Token: 0x04000CF3 RID: 3315
		private string typeName;
	}
}
