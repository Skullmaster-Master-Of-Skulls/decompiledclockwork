using System;
using System.CodeDom.Compiler;
using System.Reflection;

namespace System.Xml.Serialization
{
	// Token: 0x02000156 RID: 342
	internal class MemberMapping : AccessorMapping
	{
		// Token: 0x060017BC RID: 6076 RVA: 0x000680AA File Offset: 0x000662AA
		internal MemberMapping()
		{
		}

		// Token: 0x060017BD RID: 6077 RVA: 0x000680BC File Offset: 0x000662BC
		private MemberMapping(MemberMapping mapping) : base(mapping)
		{
			this.name = mapping.name;
			this.checkShouldPersist = mapping.checkShouldPersist;
			this.checkSpecified = mapping.checkSpecified;
			this.isReturnValue = mapping.isReturnValue;
			this.readOnly = mapping.readOnly;
			this.sequenceId = mapping.sequenceId;
			this.memberInfo = mapping.memberInfo;
			this.checkSpecifiedMemberInfo = mapping.checkSpecifiedMemberInfo;
			this.checkShouldPersistMethodInfo = mapping.checkShouldPersistMethodInfo;
		}

		// Token: 0x17000501 RID: 1281
		// (get) Token: 0x060017BE RID: 6078 RVA: 0x00068143 File Offset: 0x00066343
		// (set) Token: 0x060017BF RID: 6079 RVA: 0x0006814B File Offset: 0x0006634B
		internal bool CheckShouldPersist
		{
			get
			{
				return this.checkShouldPersist;
			}
			set
			{
				this.checkShouldPersist = value;
			}
		}

		// Token: 0x17000502 RID: 1282
		// (get) Token: 0x060017C0 RID: 6080 RVA: 0x00068154 File Offset: 0x00066354
		// (set) Token: 0x060017C1 RID: 6081 RVA: 0x0006815C File Offset: 0x0006635C
		internal SpecifiedAccessor CheckSpecified
		{
			get
			{
				return this.checkSpecified;
			}
			set
			{
				this.checkSpecified = value;
			}
		}

		// Token: 0x17000503 RID: 1283
		// (get) Token: 0x060017C2 RID: 6082 RVA: 0x00068165 File Offset: 0x00066365
		// (set) Token: 0x060017C3 RID: 6083 RVA: 0x0006817B File Offset: 0x0006637B
		internal string Name
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

		// Token: 0x17000504 RID: 1284
		// (get) Token: 0x060017C4 RID: 6084 RVA: 0x00068184 File Offset: 0x00066384
		// (set) Token: 0x060017C5 RID: 6085 RVA: 0x0006818C File Offset: 0x0006638C
		internal MemberInfo MemberInfo
		{
			get
			{
				return this.memberInfo;
			}
			set
			{
				this.memberInfo = value;
			}
		}

		// Token: 0x17000505 RID: 1285
		// (get) Token: 0x060017C6 RID: 6086 RVA: 0x00068195 File Offset: 0x00066395
		// (set) Token: 0x060017C7 RID: 6087 RVA: 0x0006819D File Offset: 0x0006639D
		internal MemberInfo CheckSpecifiedMemberInfo
		{
			get
			{
				return this.checkSpecifiedMemberInfo;
			}
			set
			{
				this.checkSpecifiedMemberInfo = value;
			}
		}

		// Token: 0x17000506 RID: 1286
		// (get) Token: 0x060017C8 RID: 6088 RVA: 0x000681A6 File Offset: 0x000663A6
		// (set) Token: 0x060017C9 RID: 6089 RVA: 0x000681AE File Offset: 0x000663AE
		internal MethodInfo CheckShouldPersistMethodInfo
		{
			get
			{
				return this.checkShouldPersistMethodInfo;
			}
			set
			{
				this.checkShouldPersistMethodInfo = value;
			}
		}

		// Token: 0x17000507 RID: 1287
		// (get) Token: 0x060017CA RID: 6090 RVA: 0x000681B7 File Offset: 0x000663B7
		// (set) Token: 0x060017CB RID: 6091 RVA: 0x000681BF File Offset: 0x000663BF
		internal bool IsReturnValue
		{
			get
			{
				return this.isReturnValue;
			}
			set
			{
				this.isReturnValue = value;
			}
		}

		// Token: 0x17000508 RID: 1288
		// (get) Token: 0x060017CC RID: 6092 RVA: 0x000681C8 File Offset: 0x000663C8
		// (set) Token: 0x060017CD RID: 6093 RVA: 0x000681D0 File Offset: 0x000663D0
		internal bool ReadOnly
		{
			get
			{
				return this.readOnly;
			}
			set
			{
				this.readOnly = value;
			}
		}

		// Token: 0x17000509 RID: 1289
		// (get) Token: 0x060017CE RID: 6094 RVA: 0x000681D9 File Offset: 0x000663D9
		internal bool IsSequence
		{
			get
			{
				return this.sequenceId >= 0;
			}
		}

		// Token: 0x1700050A RID: 1290
		// (get) Token: 0x060017CF RID: 6095 RVA: 0x000681E7 File Offset: 0x000663E7
		// (set) Token: 0x060017D0 RID: 6096 RVA: 0x000681EF File Offset: 0x000663EF
		internal int SequenceId
		{
			get
			{
				return this.sequenceId;
			}
			set
			{
				this.sequenceId = value;
			}
		}

		// Token: 0x060017D1 RID: 6097 RVA: 0x000681F8 File Offset: 0x000663F8
		private string GetNullableType(TypeDesc td)
		{
			if (td.IsMappedType || (!td.IsValueType && (base.Elements[0].IsSoap || td.ArrayElementTypeDesc == null)))
			{
				return td.FullName;
			}
			if (td.ArrayElementTypeDesc != null)
			{
				return this.GetNullableType(td.ArrayElementTypeDesc) + "[]";
			}
			return "System.Nullable`1[" + td.FullName + "]";
		}

		// Token: 0x060017D2 RID: 6098 RVA: 0x00068267 File Offset: 0x00066467
		internal MemberMapping Clone()
		{
			return new MemberMapping(this);
		}

		// Token: 0x060017D3 RID: 6099 RVA: 0x0006826F File Offset: 0x0006646F
		internal string GetTypeName(CodeDomProvider codeProvider)
		{
			if (base.IsNeedNullable && codeProvider.Supports(GeneratorSupport.GenericTypeReference))
			{
				return this.GetNullableType(base.TypeDesc);
			}
			return base.TypeDesc.FullName;
		}

		// Token: 0x04000AFA RID: 2810
		private string name;

		// Token: 0x04000AFB RID: 2811
		private bool checkShouldPersist;

		// Token: 0x04000AFC RID: 2812
		private SpecifiedAccessor checkSpecified;

		// Token: 0x04000AFD RID: 2813
		private bool isReturnValue;

		// Token: 0x04000AFE RID: 2814
		private bool readOnly;

		// Token: 0x04000AFF RID: 2815
		private int sequenceId = -1;

		// Token: 0x04000B00 RID: 2816
		private MemberInfo memberInfo;

		// Token: 0x04000B01 RID: 2817
		private MemberInfo checkSpecifiedMemberInfo;

		// Token: 0x04000B02 RID: 2818
		private MethodInfo checkShouldPersistMethodInfo;
	}
}
