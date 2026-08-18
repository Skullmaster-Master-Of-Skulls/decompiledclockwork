using System;
using System.Xml.Schema;

namespace System.Xml.Serialization
{
	// Token: 0x020002BF RID: 703
	internal abstract class Accessor
	{
		// Token: 0x06002178 RID: 8568 RVA: 0x0009EE31 File Offset: 0x0009DE31
		internal Accessor()
		{
		}

		// Token: 0x170007FE RID: 2046
		// (get) Token: 0x06002179 RID: 8569 RVA: 0x0009EE39 File Offset: 0x0009DE39
		// (set) Token: 0x0600217A RID: 8570 RVA: 0x0009EE41 File Offset: 0x0009DE41
		internal TypeMapping Mapping
		{
			get
			{
				return this.mapping;
			}
			set
			{
				this.mapping = value;
			}
		}

		// Token: 0x170007FF RID: 2047
		// (get) Token: 0x0600217B RID: 8571 RVA: 0x0009EE4A File Offset: 0x0009DE4A
		// (set) Token: 0x0600217C RID: 8572 RVA: 0x0009EE52 File Offset: 0x0009DE52
		internal object Default
		{
			get
			{
				return this.defaultValue;
			}
			set
			{
				this.defaultValue = value;
			}
		}

		// Token: 0x17000800 RID: 2048
		// (get) Token: 0x0600217D RID: 8573 RVA: 0x0009EE5B File Offset: 0x0009DE5B
		internal bool HasDefault
		{
			get
			{
				return this.defaultValue != null && this.defaultValue != DBNull.Value;
			}
		}

		// Token: 0x17000801 RID: 2049
		// (get) Token: 0x0600217E RID: 8574 RVA: 0x0009EE77 File Offset: 0x0009DE77
		// (set) Token: 0x0600217F RID: 8575 RVA: 0x0009EE8D File Offset: 0x0009DE8D
		internal virtual string Name
		{
			get
			{
				if (this.name != null)
				{
					return this.name;
				}
				return string.Empty;
			}
			set
			{
				this.name = value;
			}
		}

		// Token: 0x17000802 RID: 2050
		// (get) Token: 0x06002180 RID: 8576 RVA: 0x0009EE96 File Offset: 0x0009DE96
		// (set) Token: 0x06002181 RID: 8577 RVA: 0x0009EE9E File Offset: 0x0009DE9E
		internal bool Any
		{
			get
			{
				return this.any;
			}
			set
			{
				this.any = value;
			}
		}

		// Token: 0x17000803 RID: 2051
		// (get) Token: 0x06002182 RID: 8578 RVA: 0x0009EEA7 File Offset: 0x0009DEA7
		// (set) Token: 0x06002183 RID: 8579 RVA: 0x0009EEAF File Offset: 0x0009DEAF
		internal string AnyNamespaces
		{
			get
			{
				return this.anyNs;
			}
			set
			{
				this.anyNs = value;
			}
		}

		// Token: 0x17000804 RID: 2052
		// (get) Token: 0x06002184 RID: 8580 RVA: 0x0009EEB8 File Offset: 0x0009DEB8
		// (set) Token: 0x06002185 RID: 8581 RVA: 0x0009EEC0 File Offset: 0x0009DEC0
		internal string Namespace
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

		// Token: 0x17000805 RID: 2053
		// (get) Token: 0x06002186 RID: 8582 RVA: 0x0009EEC9 File Offset: 0x0009DEC9
		// (set) Token: 0x06002187 RID: 8583 RVA: 0x0009EED1 File Offset: 0x0009DED1
		internal XmlSchemaForm Form
		{
			get
			{
				return this.form;
			}
			set
			{
				this.form = value;
			}
		}

		// Token: 0x17000806 RID: 2054
		// (get) Token: 0x06002188 RID: 8584 RVA: 0x0009EEDA File Offset: 0x0009DEDA
		// (set) Token: 0x06002189 RID: 8585 RVA: 0x0009EEE2 File Offset: 0x0009DEE2
		internal bool IsFixed
		{
			get
			{
				return this.isFixed;
			}
			set
			{
				this.isFixed = value;
			}
		}

		// Token: 0x17000807 RID: 2055
		// (get) Token: 0x0600218A RID: 8586 RVA: 0x0009EEEB File Offset: 0x0009DEEB
		// (set) Token: 0x0600218B RID: 8587 RVA: 0x0009EEF3 File Offset: 0x0009DEF3
		internal bool IsOptional
		{
			get
			{
				return this.isOptional;
			}
			set
			{
				this.isOptional = value;
			}
		}

		// Token: 0x17000808 RID: 2056
		// (get) Token: 0x0600218C RID: 8588 RVA: 0x0009EEFC File Offset: 0x0009DEFC
		// (set) Token: 0x0600218D RID: 8589 RVA: 0x0009EF04 File Offset: 0x0009DF04
		internal bool IsTopLevelInSchema
		{
			get
			{
				return this.topLevelInSchema;
			}
			set
			{
				this.topLevelInSchema = value;
			}
		}

		// Token: 0x0600218E RID: 8590 RVA: 0x0009EF0D File Offset: 0x0009DF0D
		internal static string EscapeName(string name)
		{
			if (name == null || name.Length == 0)
			{
				return name;
			}
			return XmlConvert.EncodeLocalName(name);
		}

		// Token: 0x0600218F RID: 8591 RVA: 0x0009EF24 File Offset: 0x0009DF24
		internal static string EscapeQName(string name)
		{
			if (name == null || name.Length == 0)
			{
				return name;
			}
			int num = name.LastIndexOf(':');
			if (num < 0)
			{
				return XmlConvert.EncodeLocalName(name);
			}
			if (num == 0 || num == name.Length - 1)
			{
				throw new ArgumentException(Res.GetString("Xml_InvalidNameChars", new object[]
				{
					name
				}), "name");
			}
			return new XmlQualifiedName(XmlConvert.EncodeLocalName(name.Substring(num + 1)), XmlConvert.EncodeLocalName(name.Substring(0, num))).ToString();
		}

		// Token: 0x06002190 RID: 8592 RVA: 0x0009EFA6 File Offset: 0x0009DFA6
		internal static string UnescapeName(string name)
		{
			return XmlConvert.DecodeName(name);
		}

		// Token: 0x06002191 RID: 8593 RVA: 0x0009EFB0 File Offset: 0x0009DFB0
		internal string ToString(string defaultNs)
		{
			if (this.Any)
			{
				return ((this.Namespace == null) ? "##any" : this.Namespace) + ":" + this.Name;
			}
			if (!(this.Namespace == defaultNs))
			{
				return this.Namespace + ":" + this.Name;
			}
			return this.Name;
		}

		// Token: 0x0400145D RID: 5213
		private string name;

		// Token: 0x0400145E RID: 5214
		private object defaultValue;

		// Token: 0x0400145F RID: 5215
		private string ns;

		// Token: 0x04001460 RID: 5216
		private TypeMapping mapping;

		// Token: 0x04001461 RID: 5217
		private bool any;

		// Token: 0x04001462 RID: 5218
		private string anyNs;

		// Token: 0x04001463 RID: 5219
		private bool topLevelInSchema;

		// Token: 0x04001464 RID: 5220
		private bool isFixed;

		// Token: 0x04001465 RID: 5221
		private bool isOptional;

		// Token: 0x04001466 RID: 5222
		private XmlSchemaForm form;
	}
}
