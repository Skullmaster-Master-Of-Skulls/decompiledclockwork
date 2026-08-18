using System;
using System.Xml.Serialization;

namespace System.Xml.Schema
{
	// Token: 0x020002A6 RID: 678
	public abstract class XmlSchemaParticle : XmlSchemaAnnotated
	{
		// Token: 0x1700090B RID: 2315
		// (get) Token: 0x06002777 RID: 10103 RVA: 0x000CFAD1 File Offset: 0x000CDCD1
		// (set) Token: 0x06002778 RID: 10104 RVA: 0x000CFAEC File Offset: 0x000CDCEC
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

		// Token: 0x1700090C RID: 2316
		// (get) Token: 0x06002779 RID: 10105 RVA: 0x000CFB53 File Offset: 0x000CDD53
		// (set) Token: 0x0600277A RID: 10106 RVA: 0x000CFB8C File Offset: 0x000CDD8C
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

		// Token: 0x1700090D RID: 2317
		// (get) Token: 0x0600277B RID: 10107 RVA: 0x000CFC3A File Offset: 0x000CDE3A
		// (set) Token: 0x0600277C RID: 10108 RVA: 0x000CFC44 File Offset: 0x000CDE44
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

		// Token: 0x1700090E RID: 2318
		// (get) Token: 0x0600277D RID: 10109 RVA: 0x000CFC91 File Offset: 0x000CDE91
		// (set) Token: 0x0600277E RID: 10110 RVA: 0x000CFC9C File Offset: 0x000CDE9C
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

		// Token: 0x1700090F RID: 2319
		// (get) Token: 0x0600277F RID: 10111 RVA: 0x000CFD11 File Offset: 0x000CDF11
		internal virtual bool IsEmpty
		{
			get
			{
				return this.maxOccurs == 0m;
			}
		}

		// Token: 0x17000910 RID: 2320
		// (get) Token: 0x06002780 RID: 10112 RVA: 0x000CFD23 File Offset: 0x000CDF23
		internal bool IsMultipleOccurrence
		{
			get
			{
				return this.maxOccurs > 1m;
			}
		}

		// Token: 0x17000911 RID: 2321
		// (get) Token: 0x06002781 RID: 10113 RVA: 0x000CFD35 File Offset: 0x000CDF35
		internal virtual string NameString
		{
			get
			{
				return string.Empty;
			}
		}

		// Token: 0x06002782 RID: 10114 RVA: 0x000CFD3C File Offset: 0x000CDF3C
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

		// Token: 0x04001128 RID: 4392
		private decimal minOccurs = 1m;

		// Token: 0x04001129 RID: 4393
		private decimal maxOccurs = 1m;

		// Token: 0x0400112A RID: 4394
		private XmlSchemaParticle.Occurs flags;

		// Token: 0x0400112B RID: 4395
		internal static readonly XmlSchemaParticle Empty = new XmlSchemaParticle.EmptyParticle();

		// Token: 0x020004AB RID: 1195
		[Flags]
		private enum Occurs
		{
			// Token: 0x04001F15 RID: 7957
			None = 0,
			// Token: 0x04001F16 RID: 7958
			Min = 1,
			// Token: 0x04001F17 RID: 7959
			Max = 2
		}

		// Token: 0x020004AC RID: 1196
		private class EmptyParticle : XmlSchemaParticle
		{
			// Token: 0x17000A77 RID: 2679
			// (get) Token: 0x06003184 RID: 12676 RVA: 0x00120312 File Offset: 0x0011E512
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
