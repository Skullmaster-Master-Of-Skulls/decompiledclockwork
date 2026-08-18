using System;

namespace System.Xml.Serialization
{
	// Token: 0x0200033F RID: 831
	public class XmlTypeMapping : XmlMapping
	{
		// Token: 0x060028A6 RID: 10406 RVA: 0x000D1D89 File Offset: 0x000D0D89
		internal XmlTypeMapping(TypeScope scope, ElementAccessor accessor) : base(scope, accessor)
		{
		}

		// Token: 0x1700099A RID: 2458
		// (get) Token: 0x060028A7 RID: 10407 RVA: 0x000D1D93 File Offset: 0x000D0D93
		internal TypeMapping Mapping
		{
			get
			{
				return base.Accessor.Mapping;
			}
		}

		// Token: 0x1700099B RID: 2459
		// (get) Token: 0x060028A8 RID: 10408 RVA: 0x000D1DA0 File Offset: 0x000D0DA0
		public string TypeName
		{
			get
			{
				return this.Mapping.TypeDesc.Name;
			}
		}

		// Token: 0x1700099C RID: 2460
		// (get) Token: 0x060028A9 RID: 10409 RVA: 0x000D1DB2 File Offset: 0x000D0DB2
		public string TypeFullName
		{
			get
			{
				return this.Mapping.TypeDesc.FullName;
			}
		}

		// Token: 0x1700099D RID: 2461
		// (get) Token: 0x060028AA RID: 10410 RVA: 0x000D1DC4 File Offset: 0x000D0DC4
		public string XsdTypeName
		{
			get
			{
				return this.Mapping.TypeName;
			}
		}

		// Token: 0x1700099E RID: 2462
		// (get) Token: 0x060028AB RID: 10411 RVA: 0x000D1DD1 File Offset: 0x000D0DD1
		public string XsdTypeNamespace
		{
			get
			{
				return this.Mapping.Namespace;
			}
		}
	}
}
