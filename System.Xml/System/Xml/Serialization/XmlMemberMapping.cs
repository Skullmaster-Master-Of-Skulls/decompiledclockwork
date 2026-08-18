using System;
using System.CodeDom.Compiler;

namespace System.Xml.Serialization
{
	// Token: 0x02000312 RID: 786
	public class XmlMemberMapping
	{
		// Token: 0x06002525 RID: 9509 RVA: 0x000AE149 File Offset: 0x000AD149
		internal XmlMemberMapping(MemberMapping mapping)
		{
			this.mapping = mapping;
		}

		// Token: 0x17000926 RID: 2342
		// (get) Token: 0x06002526 RID: 9510 RVA: 0x000AE158 File Offset: 0x000AD158
		internal MemberMapping Mapping
		{
			get
			{
				return this.mapping;
			}
		}

		// Token: 0x17000927 RID: 2343
		// (get) Token: 0x06002527 RID: 9511 RVA: 0x000AE160 File Offset: 0x000AD160
		internal Accessor Accessor
		{
			get
			{
				return this.mapping.Accessor;
			}
		}

		// Token: 0x17000928 RID: 2344
		// (get) Token: 0x06002528 RID: 9512 RVA: 0x000AE16D File Offset: 0x000AD16D
		public bool Any
		{
			get
			{
				return this.Accessor.Any;
			}
		}

		// Token: 0x17000929 RID: 2345
		// (get) Token: 0x06002529 RID: 9513 RVA: 0x000AE17A File Offset: 0x000AD17A
		public string ElementName
		{
			get
			{
				return Accessor.UnescapeName(this.Accessor.Name);
			}
		}

		// Token: 0x1700092A RID: 2346
		// (get) Token: 0x0600252A RID: 9514 RVA: 0x000AE18C File Offset: 0x000AD18C
		public string XsdElementName
		{
			get
			{
				return this.Accessor.Name;
			}
		}

		// Token: 0x1700092B RID: 2347
		// (get) Token: 0x0600252B RID: 9515 RVA: 0x000AE199 File Offset: 0x000AD199
		public string Namespace
		{
			get
			{
				return this.Accessor.Namespace;
			}
		}

		// Token: 0x1700092C RID: 2348
		// (get) Token: 0x0600252C RID: 9516 RVA: 0x000AE1A6 File Offset: 0x000AD1A6
		public string MemberName
		{
			get
			{
				return this.mapping.Name;
			}
		}

		// Token: 0x1700092D RID: 2349
		// (get) Token: 0x0600252D RID: 9517 RVA: 0x000AE1B3 File Offset: 0x000AD1B3
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

		// Token: 0x1700092E RID: 2350
		// (get) Token: 0x0600252E RID: 9518 RVA: 0x000AE1D8 File Offset: 0x000AD1D8
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

		// Token: 0x1700092F RID: 2351
		// (get) Token: 0x0600252F RID: 9519 RVA: 0x000AE1F9 File Offset: 0x000AD1F9
		public string TypeFullName
		{
			get
			{
				return this.mapping.TypeDesc.FullName;
			}
		}

		// Token: 0x17000930 RID: 2352
		// (get) Token: 0x06002530 RID: 9520 RVA: 0x000AE20B File Offset: 0x000AD20B
		public bool CheckSpecified
		{
			get
			{
				return this.mapping.CheckSpecified != SpecifiedAccessor.None;
			}
		}

		// Token: 0x17000931 RID: 2353
		// (get) Token: 0x06002531 RID: 9521 RVA: 0x000AE21E File Offset: 0x000AD21E
		internal bool IsNullable
		{
			get
			{
				return this.mapping.IsNeedNullable;
			}
		}

		// Token: 0x06002532 RID: 9522 RVA: 0x000AE22B File Offset: 0x000AD22B
		public string GenerateTypeName(CodeDomProvider codeProvider)
		{
			return this.mapping.GetTypeName(codeProvider);
		}

		// Token: 0x0400158C RID: 5516
		private MemberMapping mapping;
	}
}
