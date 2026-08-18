using System;
using System.Xml.Serialization;

namespace System.Xml.Schema
{
	// Token: 0x02000230 RID: 560
	public abstract class XmlSchemaParticle : XmlSchemaAnnotated
	{
		// Token: 0x1700069C RID: 1692
		// (get) Token: 0x06001AD4 RID: 6868 RVA: 0x00080A12 File Offset: 0x0007FA12
		// (set) Token: 0x06001AD5 RID: 6869 RVA: 0x00080A2C File Offset: 0x0007FA2C
		[XmlAttribute("minOccurs")]
		public string MinOccursString
		{
			get
			{
				if ((this.flags & XmlSchemaParticle.Occurs.Min) != XmlSchemaParticle.Occurs.None)
				{
					return XmlConvert.ToString(this.minOccurs);
				}
				return null;
			}
			set
			{
				if (value == null)
				{
					this.minOccurs = 1m;
					this.flags &= ~XmlSchemaParticle.Occurs.Min;
					return;
				}
				this.minOccurs = XmlConvert.ToInteger(value);
				if (this.minOccurs < 0m)
				{
					throw new XmlSchemaException("Sch_MinOccursInvalidXsd", string.Empty);
				}
				this.flags |= XmlSchemaParticle.Occurs.Min;
			}
		}

		// Token: 0x1700069D RID: 1693
		// (get) Token: 0x06001AD6 RID: 6870 RVA: 0x00080A95 File Offset: 0x0007FA95
		// (set) Token: 0x06001AD7 RID: 6871 RVA: 0x00080ACC File Offset: 0x0007FACC
		[XmlAttribute("maxOccurs")]
		public string MaxOccursString
		{
			get
			{
				if ((this.flags & XmlSchemaParticle.Occurs.Max) == XmlSchemaParticle.Occurs.None)
				{
					return null;
				}
				if (!(this.maxOccurs == 79228162514264337593543950335m))
				{
					return XmlConvert.ToString(this.maxOccurs);
				}
				return "unbounded";
			}
			set
			{
				if (value == null)
				{
					this.maxOccurs = 1m;
					this.flags &= ~XmlSchemaParticle.Occurs.Max;
					return;
				}
				if (value == "unbounded")
				{
					this.maxOccurs = decimal.MaxValue;
				}
				else
				{
					this.maxOccurs = XmlConvert.ToInteger(value);
					if (this.maxOccurs < 0m)
					{
						throw new XmlSchemaException("Sch_MaxOccursInvalidXsd", string.Empty);
					}
					if (this.maxOccurs == 0m && (this.flags & XmlSchemaParticle.Occurs.Min) == XmlSchemaParticle.Occurs.None)
					{
						this.minOccurs = 0m;
					}
				}
				this.flags |= XmlSchemaParticle.Occurs.Max;
			}
		}

		// Token: 0x1700069E RID: 1694
		// (get) Token: 0x06001AD8 RID: 6872 RVA: 0x00080B7D File Offset: 0x0007FB7D
		// (set) Token: 0x06001AD9 RID: 6873 RVA: 0x00080B88 File Offset: 0x0007FB88
		[XmlIgnore]
		public decimal MinOccurs
		{
			get
			{
				return this.minOccurs;
			}
			set
			{
				if (value < 0m || value != decimal.Truncate(value))
				{
					throw new XmlSchemaException("Sch_MinOccursInvalidXsd", string.Empty);
				}
				this.minOccurs = value;
				this.flags |= XmlSchemaParticle.Occurs.Min;
			}
		}

		// Token: 0x1700069F RID: 1695
		// (get) Token: 0x06001ADA RID: 6874 RVA: 0x00080BD6 File Offset: 0x0007FBD6
		// (set) Token: 0x06001ADB RID: 6875 RVA: 0x00080BE0 File Offset: 0x0007FBE0
		[XmlIgnore]
		public decimal MaxOccurs
		{
			get
			{
				return this.maxOccurs;
			}
			set
			{
				if (value < 0m || value != decimal.Truncate(value))
				{
					throw new XmlSchemaException("Sch_MaxOccursInvalidXsd", string.Empty);
				}
				this.maxOccurs = value;
				if (this.maxOccurs == 0m && (this.flags & XmlSchemaParticle.Occurs.Min) == XmlSchemaParticle.Occurs.None)
				{
					this.minOccurs = 0m;
				}
				this.flags |= XmlSchemaParticle.Occurs.Max;
			}
		}

		// Token: 0x170006A0 RID: 1696
		// (get) Token: 0x06001ADC RID: 6876 RVA: 0x00080C57 File Offset: 0x0007FC57
		internal virtual bool IsEmpty
		{
			get
			{
				return this.maxOccurs == 0m;
			}
		}

		// Token: 0x170006A1 RID: 1697
		// (get) Token: 0x06001ADD RID: 6877 RVA: 0x00080C6A File Offset: 0x0007FC6A
		internal bool IsMultipleOccurrence
		{
			get
			{
				return this.maxOccurs > 1m;
			}
		}

		// Token: 0x170006A2 RID: 1698
		// (get) Token: 0x06001ADE RID: 6878 RVA: 0x00080C7D File Offset: 0x0007FC7D
		internal virtual string NameString
		{
			get
			{
				return string.Empty;
			}
		}

		// Token: 0x06001ADF RID: 6879 RVA: 0x00080C84 File Offset: 0x0007FC84
		internal XmlQualifiedName GetQualifiedName()
		{
			XmlSchemaElement xmlSchemaElement = this as XmlSchemaElement;
			if (xmlSchemaElement != null)
			{
				return xmlSchemaElement.QualifiedName;
			}
			XmlSchemaAny xmlSchemaAny = this as XmlSchemaAny;
			if (xmlSchemaAny != null)
			{
				string text = xmlSchemaAny.Namespace;
				if (text != null)
				{
					text = text.Trim();
				}
				else
				{
					text = string.Empty;
				}
				return new XmlQualifiedName("*", (text.Length == 0) ? "##any" : text);
			}
			return XmlQualifiedName.Empty;
		}

		// Token: 0x040010D8 RID: 4312
		private decimal minOccurs = 1m;

		// Token: 0x040010D9 RID: 4313
		private decimal maxOccurs = 1m;

		// Token: 0x040010DA RID: 4314
		private XmlSchemaParticle.Occurs flags;

		// Token: 0x040010DB RID: 4315
		internal static readonly XmlSchemaParticle Empty = new XmlSchemaParticle.EmptyParticle();

		// Token: 0x02000231 RID: 561
		[Flags]
		private enum Occurs
		{
			// Token: 0x040010DD RID: 4317
			None = 0,
			// Token: 0x040010DE RID: 4318
			Min = 1,
			// Token: 0x040010DF RID: 4319
			Max = 2
		}

		// Token: 0x02000232 RID: 562
		private class EmptyParticle : XmlSchemaParticle
		{
			// Token: 0x170006A3 RID: 1699
			// (get) Token: 0x06001AE2 RID: 6882 RVA: 0x00080D11 File Offset: 0x0007FD11
			internal override bool IsEmpty
			{
				get
				{
					return true;
				}
			}
		}
	}
}
