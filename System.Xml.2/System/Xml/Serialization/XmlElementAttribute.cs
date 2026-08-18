using System;
using System.Xml.Schema;

namespace System.Xml.Serialization
{
	// Token: 0x02000192 RID: 402
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter | AttributeTargets.ReturnValue, AllowMultiple = true)]
	[__DynamicallyInvokable]
	public class XmlElementAttribute : Attribute
	{
		// Token: 0x06001ABC RID: 6844 RVA: 0x00076C74 File Offset: 0x00074E74
		[__DynamicallyInvokable]
		public XmlElementAttribute()
		{
		}

		// Token: 0x06001ABD RID: 6845 RVA: 0x00076C83 File Offset: 0x00074E83
		[__DynamicallyInvokable]
		public XmlElementAttribute(string elementName)
		{
			this.elementName = elementName;
		}

		// Token: 0x06001ABE RID: 6846 RVA: 0x00076C99 File Offset: 0x00074E99
		[__DynamicallyInvokable]
		public XmlElementAttribute(Type type)
		{
			this.type = type;
		}

		// Token: 0x06001ABF RID: 6847 RVA: 0x00076CAF File Offset: 0x00074EAF
		[__DynamicallyInvokable]
		public XmlElementAttribute(string elementName, Type type)
		{
			this.elementName = elementName;
			this.type = type;
		}

		// Token: 0x170005D6 RID: 1494
		// (get) Token: 0x06001AC0 RID: 6848 RVA: 0x00076CCC File Offset: 0x00074ECC
		// (set) Token: 0x06001AC1 RID: 6849 RVA: 0x00076CD4 File Offset: 0x00074ED4
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

		// Token: 0x170005D7 RID: 1495
		// (get) Token: 0x06001AC2 RID: 6850 RVA: 0x00076CDD File Offset: 0x00074EDD
		// (set) Token: 0x06001AC3 RID: 6851 RVA: 0x00076CF3 File Offset: 0x00074EF3
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

		// Token: 0x170005D8 RID: 1496
		// (get) Token: 0x06001AC4 RID: 6852 RVA: 0x00076CFC File Offset: 0x00074EFC
		// (set) Token: 0x06001AC5 RID: 6853 RVA: 0x00076D04 File Offset: 0x00074F04
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

		// Token: 0x170005D9 RID: 1497
		// (get) Token: 0x06001AC6 RID: 6854 RVA: 0x00076D0D File Offset: 0x00074F0D
		// (set) Token: 0x06001AC7 RID: 6855 RVA: 0x00076D23 File Offset: 0x00074F23
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

		// Token: 0x170005DA RID: 1498
		// (get) Token: 0x06001AC8 RID: 6856 RVA: 0x00076D2C File Offset: 0x00074F2C
		// (set) Token: 0x06001AC9 RID: 6857 RVA: 0x00076D34 File Offset: 0x00074F34
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

		// Token: 0x170005DB RID: 1499
		// (get) Token: 0x06001ACA RID: 6858 RVA: 0x00076D44 File Offset: 0x00074F44
		internal bool IsNullableSpecified
		{
			get
			{
				return this.nullableSpecified;
			}
		}

		// Token: 0x170005DC RID: 1500
		// (get) Token: 0x06001ACB RID: 6859 RVA: 0x00076D4C File Offset: 0x00074F4C
		// (set) Token: 0x06001ACC RID: 6860 RVA: 0x00076D54 File Offset: 0x00074F54
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

		// Token: 0x170005DD RID: 1501
		// (get) Token: 0x06001ACD RID: 6861 RVA: 0x00076D5D File Offset: 0x00074F5D
		// (set) Token: 0x06001ACE RID: 6862 RVA: 0x00076D65 File Offset: 0x00074F65
		[__DynamicallyInvokable]
		public int Order
		{
			[__DynamicallyInvokable]
			get
			{
				return this.order;
			}
			[__DynamicallyInvokable]
			set
			{
				if (value < 0)
				{
					throw new ArgumentException(Res.GetString("XmlDisallowNegativeValues"), "Order");
				}
				this.order = value;
			}
		}

		// Token: 0x04000BEC RID: 3052
		private string elementName;

		// Token: 0x04000BED RID: 3053
		private Type type;

		// Token: 0x04000BEE RID: 3054
		private string ns;

		// Token: 0x04000BEF RID: 3055
		private string dataType;

		// Token: 0x04000BF0 RID: 3056
		private bool nullable;

		// Token: 0x04000BF1 RID: 3057
		private bool nullableSpecified;

		// Token: 0x04000BF2 RID: 3058
		private XmlSchemaForm form;

		// Token: 0x04000BF3 RID: 3059
		private int order = -1;
	}
}
