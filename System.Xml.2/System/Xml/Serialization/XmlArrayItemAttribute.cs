using System;
using System.Xml.Schema;

namespace System.Xml.Serialization
{
	// Token: 0x02000188 RID: 392
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter | AttributeTargets.ReturnValue, AllowMultiple = true)]
	[__DynamicallyInvokable]
	public class XmlArrayItemAttribute : Attribute
	{
		// Token: 0x060019C1 RID: 6593 RVA: 0x000732C9 File Offset: 0x000714C9
		[__DynamicallyInvokable]
		public XmlArrayItemAttribute()
		{
		}

		// Token: 0x060019C2 RID: 6594 RVA: 0x000732D1 File Offset: 0x000714D1
		[__DynamicallyInvokable]
		public XmlArrayItemAttribute(string elementName)
		{
			this.elementName = elementName;
		}

		// Token: 0x060019C3 RID: 6595 RVA: 0x000732E0 File Offset: 0x000714E0
		[__DynamicallyInvokable]
		public XmlArrayItemAttribute(Type type)
		{
			this.type = type;
		}

		// Token: 0x060019C4 RID: 6596 RVA: 0x000732EF File Offset: 0x000714EF
		[__DynamicallyInvokable]
		public XmlArrayItemAttribute(string elementName, Type type)
		{
			this.elementName = elementName;
			this.type = type;
		}

		// Token: 0x17000592 RID: 1426
		// (get) Token: 0x060019C5 RID: 6597 RVA: 0x00073305 File Offset: 0x00071505
		// (set) Token: 0x060019C6 RID: 6598 RVA: 0x0007330D File Offset: 0x0007150D
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

		// Token: 0x17000593 RID: 1427
		// (get) Token: 0x060019C7 RID: 6599 RVA: 0x00073316 File Offset: 0x00071516
		// (set) Token: 0x060019C8 RID: 6600 RVA: 0x0007332C File Offset: 0x0007152C
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

		// Token: 0x17000594 RID: 1428
		// (get) Token: 0x060019C9 RID: 6601 RVA: 0x00073335 File Offset: 0x00071535
		// (set) Token: 0x060019CA RID: 6602 RVA: 0x0007333D File Offset: 0x0007153D
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

		// Token: 0x17000595 RID: 1429
		// (get) Token: 0x060019CB RID: 6603 RVA: 0x00073346 File Offset: 0x00071546
		// (set) Token: 0x060019CC RID: 6604 RVA: 0x0007334E File Offset: 0x0007154E
		[__DynamicallyInvokable]
		public int NestingLevel
		{
			[__DynamicallyInvokable]
			get
			{
				return this.nestingLevel;
			}
			[__DynamicallyInvokable]
			set
			{
				this.nestingLevel = value;
			}
		}

		// Token: 0x17000596 RID: 1430
		// (get) Token: 0x060019CD RID: 6605 RVA: 0x00073357 File Offset: 0x00071557
		// (set) Token: 0x060019CE RID: 6606 RVA: 0x0007336D File Offset: 0x0007156D
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

		// Token: 0x17000597 RID: 1431
		// (get) Token: 0x060019CF RID: 6607 RVA: 0x00073376 File Offset: 0x00071576
		// (set) Token: 0x060019D0 RID: 6608 RVA: 0x0007337E File Offset: 0x0007157E
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

		// Token: 0x17000598 RID: 1432
		// (get) Token: 0x060019D1 RID: 6609 RVA: 0x0007338E File Offset: 0x0007158E
		internal bool IsNullableSpecified
		{
			get
			{
				return this.nullableSpecified;
			}
		}

		// Token: 0x17000599 RID: 1433
		// (get) Token: 0x060019D2 RID: 6610 RVA: 0x00073396 File Offset: 0x00071596
		// (set) Token: 0x060019D3 RID: 6611 RVA: 0x0007339E File Offset: 0x0007159E
		[__DynamicallyInvokable]
		public XmlSchemaForm Form
		{
			[__DynamicallyInvokable]
			get
			{
				return this.form;
			}
			[__DynamicallyInvokable]
			set
			{
				this.form = value;
			}
		}

		// Token: 0x04000BBA RID: 3002
		private string elementName;

		// Token: 0x04000BBB RID: 3003
		private Type type;

		// Token: 0x04000BBC RID: 3004
		private string ns;

		// Token: 0x04000BBD RID: 3005
		private string dataType;

		// Token: 0x04000BBE RID: 3006
		private bool nullable;

		// Token: 0x04000BBF RID: 3007
		private bool nullableSpecified;

		// Token: 0x04000BC0 RID: 3008
		private XmlSchemaForm form;

		// Token: 0x04000BC1 RID: 3009
		private int nestingLevel;
	}
}
