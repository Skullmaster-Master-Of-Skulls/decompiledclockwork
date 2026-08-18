using System;

namespace System.Xml.Serialization
{
	// Token: 0x0200031B RID: 795
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum | AttributeTargets.Interface | AttributeTargets.ReturnValue)]
	public class XmlRootAttribute : Attribute
	{
		// Token: 0x0600259E RID: 9630 RVA: 0x000B3654 File Offset: 0x000B2654
		public XmlRootAttribute()
		{
		}

		// Token: 0x0600259F RID: 9631 RVA: 0x000B3663 File Offset: 0x000B2663
		public XmlRootAttribute(string elementName)
		{
			this.elementName = elementName;
		}

		// Token: 0x17000943 RID: 2371
		// (get) Token: 0x060025A0 RID: 9632 RVA: 0x000B3679 File Offset: 0x000B2679
		// (set) Token: 0x060025A1 RID: 9633 RVA: 0x000B368F File Offset: 0x000B268F
		public string ElementName
		{
			get
			{
				if (this.elementName != null)
				{
					return this.elementName;
				}
				return string.Empty;
			}
			set
			{
				this.elementName = value;
			}
		}

		// Token: 0x17000944 RID: 2372
		// (get) Token: 0x060025A2 RID: 9634 RVA: 0x000B3698 File Offset: 0x000B2698
		// (set) Token: 0x060025A3 RID: 9635 RVA: 0x000B36A0 File Offset: 0x000B26A0
		public string Namespace
		{
			get
			{
				return this.ns;
			}
			set
			{
				this.ns = value;
			}
		}

		// Token: 0x17000945 RID: 2373
		// (get) Token: 0x060025A4 RID: 9636 RVA: 0x000B36A9 File Offset: 0x000B26A9
		// (set) Token: 0x060025A5 RID: 9637 RVA: 0x000B36BF File Offset: 0x000B26BF
		public string DataType
		{
			get
			{
				if (this.dataType != null)
				{
					return this.dataType;
				}
				return string.Empty;
			}
			set
			{
				this.dataType = value;
			}
		}

		// Token: 0x17000946 RID: 2374
		// (get) Token: 0x060025A6 RID: 9638 RVA: 0x000B36C8 File Offset: 0x000B26C8
		// (set) Token: 0x060025A7 RID: 9639 RVA: 0x000B36D0 File Offset: 0x000B26D0
		public bool IsNullable
		{
			get
			{
				return this.nullable;
			}
			set
			{
				this.nullable = value;
				this.nullableSpecified = true;
			}
		}

		// Token: 0x17000947 RID: 2375
		// (get) Token: 0x060025A8 RID: 9640 RVA: 0x000B36E0 File Offset: 0x000B26E0
		internal bool IsNullableSpecified
		{
			get
			{
				return this.nullableSpecified;
			}
		}

		// Token: 0x17000948 RID: 2376
		// (get) Token: 0x060025A9 RID: 9641 RVA: 0x000B36E8 File Offset: 0x000B26E8
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

		// Token: 0x040015AF RID: 5551
		private string elementName;

		// Token: 0x040015B0 RID: 5552
		private string ns;

		// Token: 0x040015B1 RID: 5553
		private string dataType;

		// Token: 0x040015B2 RID: 5554
		private bool nullable = true;

		// Token: 0x040015B3 RID: 5555
		private bool nullableSpecified;
	}
}
