using System;
using System.Xml.Schema;

namespace System.Xml.Serialization
{
	// Token: 0x0200018A RID: 394
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter | AttributeTargets.ReturnValue)]
	[__DynamicallyInvokable]
	public class XmlAttributeAttribute : Attribute
	{
		// Token: 0x060019DD RID: 6621 RVA: 0x00073427 File Offset: 0x00071627
		[__DynamicallyInvokable]
		public XmlAttributeAttribute()
		{
		}

		// Token: 0x060019DE RID: 6622 RVA: 0x0007342F File Offset: 0x0007162F
		[__DynamicallyInvokable]
		public XmlAttributeAttribute(string attributeName)
		{
			this.attributeName = attributeName;
		}

		// Token: 0x060019DF RID: 6623 RVA: 0x0007343E File Offset: 0x0007163E
		[__DynamicallyInvokable]
		public XmlAttributeAttribute(Type type)
		{
			this.type = type;
		}

		// Token: 0x060019E0 RID: 6624 RVA: 0x0007344D File Offset: 0x0007164D
		[__DynamicallyInvokable]
		public XmlAttributeAttribute(string attributeName, Type type)
		{
			this.attributeName = attributeName;
			this.type = type;
		}

		// Token: 0x1700059B RID: 1435
		// (get) Token: 0x060019E1 RID: 6625 RVA: 0x00073463 File Offset: 0x00071663
		// (set) Token: 0x060019E2 RID: 6626 RVA: 0x0007346B File Offset: 0x0007166B
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

		// Token: 0x1700059C RID: 1436
		// (get) Token: 0x060019E3 RID: 6627 RVA: 0x00073474 File Offset: 0x00071674
		// (set) Token: 0x060019E4 RID: 6628 RVA: 0x0007348A File Offset: 0x0007168A
		[__DynamicallyInvokable]
		public string AttributeName
		{
			[__DynamicallyInvokable]
			get
			{
				if (this.attributeName != null)
				{
					return this.attributeName;
				}
				return string.Empty;
			}
			[__DynamicallyInvokable]
			set
			{
				this.attributeName = value;
			}
		}

		// Token: 0x1700059D RID: 1437
		// (get) Token: 0x060019E5 RID: 6629 RVA: 0x00073493 File Offset: 0x00071693
		// (set) Token: 0x060019E6 RID: 6630 RVA: 0x0007349B File Offset: 0x0007169B
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

		// Token: 0x1700059E RID: 1438
		// (get) Token: 0x060019E7 RID: 6631 RVA: 0x000734A4 File Offset: 0x000716A4
		// (set) Token: 0x060019E8 RID: 6632 RVA: 0x000734BA File Offset: 0x000716BA
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

		// Token: 0x1700059F RID: 1439
		// (get) Token: 0x060019E9 RID: 6633 RVA: 0x000734C3 File Offset: 0x000716C3
		// (set) Token: 0x060019EA RID: 6634 RVA: 0x000734CB File Offset: 0x000716CB
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

		// Token: 0x04000BC2 RID: 3010
		private string attributeName;

		// Token: 0x04000BC3 RID: 3011
		private Type type;

		// Token: 0x04000BC4 RID: 3012
		private string ns;

		// Token: 0x04000BC5 RID: 3013
		private string dataType;

		// Token: 0x04000BC6 RID: 3014
		private XmlSchemaForm form;
	}
}
