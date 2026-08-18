using System;

namespace System.Xml.Serialization
{
	// Token: 0x020001BE RID: 446
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter | AttributeTargets.ReturnValue)]
	[__DynamicallyInvokable]
	public class XmlTextAttribute : Attribute
	{
		// Token: 0x06001EE1 RID: 7905 RVA: 0x000A8F17 File Offset: 0x000A7117
		[__DynamicallyInvokable]
		public XmlTextAttribute()
		{
		}

		// Token: 0x06001EE2 RID: 7906 RVA: 0x000A8F1F File Offset: 0x000A711F
		[__DynamicallyInvokable]
		public XmlTextAttribute(Type type)
		{
			this.type = type;
		}

		// Token: 0x1700064E RID: 1614
		// (get) Token: 0x06001EE3 RID: 7907 RVA: 0x000A8F2E File Offset: 0x000A712E
		// (set) Token: 0x06001EE4 RID: 7908 RVA: 0x000A8F36 File Offset: 0x000A7136
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

		// Token: 0x1700064F RID: 1615
		// (get) Token: 0x06001EE5 RID: 7909 RVA: 0x000A8F3F File Offset: 0x000A713F
		// (set) Token: 0x06001EE6 RID: 7910 RVA: 0x000A8F55 File Offset: 0x000A7155
		[__DynamicallyInvokable]
		public string DataType
		{
			[__DynamicallyInvokable]
			get
			{
				if (this.dataType != null)
				{
					return this.dataType;
				}
				return string.Empty;
			}
			[__DynamicallyInvokable]
			set
			{
				this.dataType = value;
			}
		}

		// Token: 0x04000CEE RID: 3310
		private Type type;

		// Token: 0x04000CEF RID: 3311
		private string dataType;
	}
}
