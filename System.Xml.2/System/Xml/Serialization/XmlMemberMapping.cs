using System;
using System.CodeDom.Compiler;

namespace System.Xml.Serialization
{
	// Token: 0x02000199 RID: 409
	public class XmlMemberMapping
	{
		// Token: 0x06001AF3 RID: 6899 RVA: 0x00076FD1 File Offset: 0x000751D1
		internal XmlMemberMapping(MemberMapping mapping)
		{
			this.mapping = mapping;
		}

		// Token: 0x170005EB RID: 1515
		// (get) Token: 0x06001AF4 RID: 6900 RVA: 0x00076FE0 File Offset: 0x000751E0
		internal MemberMapping Mapping
		{
			get
			{
				return this.mapping;
			}
		}

		// Token: 0x170005EC RID: 1516
		// (get) Token: 0x06001AF5 RID: 6901 RVA: 0x00076FE8 File Offset: 0x000751E8
		internal Accessor Accessor
		{
			get
			{
				return this.mapping.Accessor;
			}
		}

		// Token: 0x170005ED RID: 1517
		// (get) Token: 0x06001AF6 RID: 6902 RVA: 0x00076FF5 File Offset: 0x000751F5
		public bool Any
		{
			get
			{
				return this.Accessor.Any;
			}
		}

		// Token: 0x170005EE RID: 1518
		// (get) Token: 0x06001AF7 RID: 6903 RVA: 0x00077002 File Offset: 0x00075202
		public string ElementName
		{
			get
			{
				return Accessor.UnescapeName(this.Accessor.Name);
			}
		}

		// Token: 0x170005EF RID: 1519
		// (get) Token: 0x06001AF8 RID: 6904 RVA: 0x00077014 File Offset: 0x00075214
		public string XsdElementName
		{
			get
			{
				return this.Accessor.Name;
			}
		}

		// Token: 0x170005F0 RID: 1520
		// (get) Token: 0x06001AF9 RID: 6905 RVA: 0x00077021 File Offset: 0x00075221
		public string Namespace
		{
			get
			{
				return this.Accessor.Namespace;
			}
		}

		// Token: 0x170005F1 RID: 1521
		// (get) Token: 0x06001AFA RID: 6906 RVA: 0x0007702E File Offset: 0x0007522E
		public string MemberName
		{
			get
			{
				return this.mapping.Name;
			}
		}

		// Token: 0x170005F2 RID: 1522
		// (get) Token: 0x06001AFB RID: 6907 RVA: 0x0007703B File Offset: 0x0007523B
		public string TypeName
		{
			get
			{
				if (this.Accessor.Mapping == null)
				{
					return string.Empty;
				}
				return this.Accessor.Mapping.TypeName;
			}
		}

		// Token: 0x170005F3 RID: 1523
		// (get) Token: 0x06001AFC RID: 6908 RVA: 0x00077060 File Offset: 0x00075260
		public string TypeNamespace
		{
			get
			{
				if (this.Accessor.Mapping == null)
				{
					return null;
				}
				return this.Accessor.Mapping.Namespace;
			}
		}

		// Token: 0x170005F4 RID: 1524
		// (get) Token: 0x06001AFD RID: 6909 RVA: 0x00077081 File Offset: 0x00075281
		public string TypeFullName
		{
			get
			{
				return this.mapping.TypeDesc.FullName;
			}
		}

		// Token: 0x170005F5 RID: 1525
		// (get) Token: 0x06001AFE RID: 6910 RVA: 0x00077093 File Offset: 0x00075293
		public bool CheckSpecified
		{
			get
			{
				return this.mapping.CheckSpecified > SpecifiedAccessor.None;
			}
		}

		// Token: 0x170005F6 RID: 1526
		// (get) Token: 0x06001AFF RID: 6911 RVA: 0x000770A3 File Offset: 0x000752A3
		internal bool IsNullable
		{
			get
			{
				return this.mapping.IsNeedNullable;
			}
		}

		// Token: 0x06001B00 RID: 6912 RVA: 0x000770B0 File Offset: 0x000752B0
		public string GenerateTypeName(CodeDomProvider codeProvider)
		{
			return this.mapping.GetTypeName(codeProvider);
		}

		// Token: 0x04000C01 RID: 3073
		private MemberMapping mapping;
	}
}
