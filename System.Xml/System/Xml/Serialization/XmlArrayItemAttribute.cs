using System;
using System.Xml.Schema;

namespace System.Xml.Serialization
{
	// Token: 0x02000301 RID: 769
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter | AttributeTargets.ReturnValue, AllowMultiple = true)]
	public class XmlArrayItemAttribute : Attribute
	{
		// Token: 0x060023F5 RID: 9205 RVA: 0x000AA49A File Offset: 0x000A949A
		public XmlArrayItemAttribute()
		{
		}

		// Token: 0x060023F6 RID: 9206 RVA: 0x000AA4A2 File Offset: 0x000A94A2
		public XmlArrayItemAttribute(string elementName)
		{
			this.elementName = elementName;
		}

		// Token: 0x060023F7 RID: 9207 RVA: 0x000AA4B1 File Offset: 0x000A94B1
		public XmlArrayItemAttribute(Type type)
		{
			this.type = type;
		}

		// Token: 0x060023F8 RID: 9208 RVA: 0x000AA4C0 File Offset: 0x000A94C0
		public XmlArrayItemAttribute(string elementName, Type type)
		{
			this.elementName = elementName;
			this.type = type;
		}

		// Token: 0x170008CE RID: 2254
		// (get) Token: 0x060023F9 RID: 9209 RVA: 0x000AA4D6 File Offset: 0x000A94D6
		// (set) Token: 0x060023FA RID: 9210 RVA: 0x000AA4DE File Offset: 0x000A94DE
		public Type Type
		{
			get
			{
				return this.type;
			}
			set
			{
				this.type = value;
			}
		}

		// Token: 0x170008CF RID: 2255
		// (get) Token: 0x060023FB RID: 9211 RVA: 0x000AA4E7 File Offset: 0x000A94E7
		// (set) Token: 0x060023FC RID: 9212 RVA: 0x000AA4FD File Offset: 0x000A94FD
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

		// Token: 0x170008D0 RID: 2256
		// (get) Token: 0x060023FD RID: 9213 RVA: 0x000AA506 File Offset: 0x000A9506
		// (set) Token: 0x060023FE RID: 9214 RVA: 0x000AA50E File Offset: 0x000A950E
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

		// Token: 0x170008D1 RID: 2257
		// (get) Token: 0x060023FF RID: 9215 RVA: 0x000AA517 File Offset: 0x000A9517
		// (set) Token: 0x06002400 RID: 9216 RVA: 0x000AA51F File Offset: 0x000A951F
		public int NestingLevel
		{
			get
			{
				return this.nestingLevel;
			}
			set
			{
				this.nestingLevel = value;
			}
		}

		// Token: 0x170008D2 RID: 2258
		// (get) Token: 0x06002401 RID: 9217 RVA: 0x000AA528 File Offset: 0x000A9528
		// (set) Token: 0x06002402 RID: 9218 RVA: 0x000AA53E File Offset: 0x000A953E
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

		// Token: 0x170008D3 RID: 2259
		// (get) Token: 0x06002403 RID: 9219 RVA: 0x000AA547 File Offset: 0x000A9547
		// (set) Token: 0x06002404 RID: 9220 RVA: 0x000AA54F File Offset: 0x000A954F
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

		// Token: 0x170008D4 RID: 2260
		// (get) Token: 0x06002405 RID: 9221 RVA: 0x000AA55F File Offset: 0x000A955F
		internal bool IsNullableSpecified
		{
			get
			{
				return this.nullableSpecified;
			}
		}

		// Token: 0x170008D5 RID: 2261
		// (get) Token: 0x06002406 RID: 9222 RVA: 0x000AA567 File Offset: 0x000A9567
		// (set) Token: 0x06002407 RID: 9223 RVA: 0x000AA56F File Offset: 0x000A956F
		public XmlSchemaForm Form
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

		// Token: 0x04001546 RID: 5446
		private string elementName;

		// Token: 0x04001547 RID: 5447
		private Type type;

		// Token: 0x04001548 RID: 5448
		private string ns;

		// Token: 0x04001549 RID: 5449
		private string dataType;

		// Token: 0x0400154A RID: 5450
		private bool nullable;

		// Token: 0x0400154B RID: 5451
		private bool nullableSpecified;

		// Token: 0x0400154C RID: 5452
		private XmlSchemaForm form;

		// Token: 0x0400154D RID: 5453
		private int nestingLevel;
	}
}
