using System;
using System.Xml.Schema;

namespace System.Xml.XPath
{
	// Token: 0x020002E8 RID: 744
	public abstract class XPathItem
	{
		// Token: 0x170009A8 RID: 2472
		// (get) Token: 0x06002C4C RID: 11340
		public abstract bool IsNode { get; }

		// Token: 0x170009A9 RID: 2473
		// (get) Token: 0x06002C4D RID: 11341
		public abstract XmlSchemaType XmlType { get; }

		// Token: 0x170009AA RID: 2474
		// (get) Token: 0x06002C4E RID: 11342
		public abstract string Value { get; }

		// Token: 0x170009AB RID: 2475
		// (get) Token: 0x06002C4F RID: 11343
		public abstract object TypedValue { get; }

		// Token: 0x170009AC RID: 2476
		// (get) Token: 0x06002C50 RID: 11344
		public abstract Type ValueType { get; }

		// Token: 0x170009AD RID: 2477
		// (get) Token: 0x06002C51 RID: 11345
		public abstract bool ValueAsBoolean { get; }

		// Token: 0x170009AE RID: 2478
		// (get) Token: 0x06002C52 RID: 11346
		public abstract DateTime ValueAsDateTime { get; }

		// Token: 0x170009AF RID: 2479
		// (get) Token: 0x06002C53 RID: 11347
		public abstract double ValueAsDouble { get; }

		// Token: 0x170009B0 RID: 2480
		// (get) Token: 0x06002C54 RID: 11348
		public abstract int ValueAsInt { get; }

		// Token: 0x170009B1 RID: 2481
		// (get) Token: 0x06002C55 RID: 11349
		public abstract long ValueAsLong { get; }

		// Token: 0x06002C56 RID: 11350 RVA: 0x000E916A File Offset: 0x000E736A
		public virtual object ValueAs(Type returnType)
		{
			return this.ValueAs(returnType, null);
		}

		// Token: 0x06002C57 RID: 11351
		public abstract object ValueAs(Type returnType, IXmlNamespaceResolver nsResolver);
	}
}
