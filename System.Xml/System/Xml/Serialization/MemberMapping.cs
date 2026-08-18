using System;
using System.CodeDom.Compiler;

namespace System.Xml.Serialization
{
	// Token: 0x020002D1 RID: 721
	internal class MemberMapping : AccessorMapping
	{
		// Token: 0x17000844 RID: 2116
		// (get) Token: 0x06002212 RID: 8722 RVA: 0x0009FDCE File Offset: 0x0009EDCE
		// (set) Token: 0x06002213 RID: 8723 RVA: 0x0009FDD6 File Offset: 0x0009EDD6
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

		// Token: 0x17000845 RID: 2117
		// (get) Token: 0x06002214 RID: 8724 RVA: 0x0009FDDF File Offset: 0x0009EDDF
		// (set) Token: 0x06002215 RID: 8725 RVA: 0x0009FDE7 File Offset: 0x0009EDE7
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

		// Token: 0x17000846 RID: 2118
		// (get) Token: 0x06002216 RID: 8726 RVA: 0x0009FDF0 File Offset: 0x0009EDF0
		// (set) Token: 0x06002217 RID: 8727 RVA: 0x0009FE06 File Offset: 0x0009EE06
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

		// Token: 0x17000847 RID: 2119
		// (get) Token: 0x06002218 RID: 8728 RVA: 0x0009FE0F File Offset: 0x0009EE0F
		// (set) Token: 0x06002219 RID: 8729 RVA: 0x0009FE17 File Offset: 0x0009EE17
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

		// Token: 0x17000848 RID: 2120
		// (get) Token: 0x0600221A RID: 8730 RVA: 0x0009FE20 File Offset: 0x0009EE20
		// (set) Token: 0x0600221B RID: 8731 RVA: 0x0009FE28 File Offset: 0x0009EE28
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

		// Token: 0x17000849 RID: 2121
		// (get) Token: 0x0600221C RID: 8732 RVA: 0x0009FE31 File Offset: 0x0009EE31
		internal bool IsSequence
		{
			get
			{
				return this.sequenceId >= 0;
			}
		}

		// Token: 0x1700084A RID: 2122
		// (get) Token: 0x0600221D RID: 8733 RVA: 0x0009FE3F File Offset: 0x0009EE3F
		// (set) Token: 0x0600221E RID: 8734 RVA: 0x0009FE47 File Offset: 0x0009EE47
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

		// Token: 0x0600221F RID: 8735 RVA: 0x0009FE50 File Offset: 0x0009EE50
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

		// Token: 0x06002220 RID: 8736 RVA: 0x0009FEBF File Offset: 0x0009EEBF
		internal string GetTypeName(CodeDomProvider codeProvider)
		{
			if (base.IsNeedNullable && codeProvider.Supports(GeneratorSupport.GenericTypeReference))
			{
				return this.GetNullableType(base.TypeDesc);
			}
			return base.TypeDesc.FullName;
		}

		// Token: 0x04001494 RID: 5268
		private string name;

		// Token: 0x04001495 RID: 5269
		private bool checkShouldPersist;

		// Token: 0x04001496 RID: 5270
		private SpecifiedAccessor checkSpecified;

		// Token: 0x04001497 RID: 5271
		private bool isReturnValue;

		// Token: 0x04001498 RID: 5272
		private bool readOnly;

		// Token: 0x04001499 RID: 5273
		private int sequenceId = -1;
	}
}
