using System;

namespace System.Xml.Serialization
{
	// Token: 0x020001A1 RID: 417
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum | AttributeTargets.Interface | AttributeTargets.ReturnValue)]
	[__DynamicallyInvokable]
	public class XmlRootAttribute : Attribute
	{
		// Token: 0x06001B6C RID: 7020 RVA: 0x0007C2F8 File Offset: 0x0007A4F8
		[__DynamicallyInvokable]
		public XmlRootAttribute()
		{
		}

		// Token: 0x06001B6D RID: 7021 RVA: 0x0007C307 File Offset: 0x0007A507
		[__DynamicallyInvokable]
		public XmlRootAttribute(string elementName)
		{
			this.elementName = elementName;
		}

		// Token: 0x17000608 RID: 1544
		// (get) Token: 0x06001B6E RID: 7022 RVA: 0x0007C31D File Offset: 0x0007A51D
		// (set) Token: 0x06001B6F RID: 7023 RVA: 0x0007C333 File Offset: 0x0007A533
		[__DynamicallyInvokable]
		public string ElementName
		{
			[__DynamicallyInvokable]
			get
			{
				if (this.elementName != null)
				{
					return this.elementName;
				}
				return string.Empty;
			}
			[__DynamicallyInvokable]
			set
			{
				this.elementName = value;
			}
		}

		// Token: 0x17000609 RID: 1545
		// (get) Token: 0x06001B70 RID: 7024 RVA: 0x0007C33C File Offset: 0x0007A53C
		// (set) Token: 0x06001B71 RID: 7025 RVA: 0x0007C344 File Offset: 0x0007A544
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

		// Token: 0x1700060A RID: 1546
		// (get) Token: 0x06001B72 RID: 7026 RVA: 0x0007C34D File Offset: 0x0007A54D
		// (set) Token: 0x06001B73 RID: 7027 RVA: 0x0007C363 File Offset: 0x0007A563
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

		// Token: 0x1700060B RID: 1547
		// (get) Token: 0x06001B74 RID: 7028 RVA: 0x0007C36C File Offset: 0x0007A56C
		// (set) Token: 0x06001B75 RID: 7029 RVA: 0x0007C374 File Offset: 0x0007A574
		[__DynamicallyInvokable]
		public bool IsNullable
		{
			[__DynamicallyInvokable]
			get
			{
				return this.nullable;
			}
			[__DynamicallyInvokable]
			set
			{
				this.nullable = value;
				this.nullableSpecified = true;
			}
		}

		// Token: 0x1700060C RID: 1548
		// (get) Token: 0x06001B76 RID: 7030 RVA: 0x0007C384 File Offset: 0x0007A584
		internal bool IsNullableSpecified
		{
			get
			{
				return this.nullableSpecified;
			}
		}

		// Token: 0x1700060D RID: 1549
		// (get) Token: 0x06001B77 RID: 7031 RVA: 0x0007C38C File Offset: 0x0007A58C
		internal string Key
		{
			get
			{
				return string.Concat(new string[]
				{
					(this.ns == null) ? string.Empty : this.ns,
					":",
					this.ElementName,
					":",
					this.nullable.ToString()
				});
			}
		}

		// Token: 0x04000C20 RID: 3104
		private string elementName;

		// Token: 0x04000C21 RID: 3105
		private string ns;

		// Token: 0x04000C22 RID: 3106
		private string dataType;

		// Token: 0x04000C23 RID: 3107
		private bool nullable = true;

		// Token: 0x04000C24 RID: 3108
		private bool nullableSpecified;
	}
}
