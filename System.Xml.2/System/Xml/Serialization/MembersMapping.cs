using System;

namespace System.Xml.Serialization
{
	// Token: 0x02000157 RID: 343
	internal class MembersMapping : TypeMapping
	{
		// Token: 0x1700050B RID: 1291
		// (get) Token: 0x060017D4 RID: 6100 RVA: 0x0006829E File Offset: 0x0006649E
		// (set) Token: 0x060017D5 RID: 6101 RVA: 0x000682A6 File Offset: 0x000664A6
		internal MemberMapping[] Members
		{
			get
			{
				return this.members;
			}
			set
			{
				this.members = value;
			}
		}

		// Token: 0x1700050C RID: 1292
		// (get) Token: 0x060017D6 RID: 6102 RVA: 0x000682AF File Offset: 0x000664AF
		// (set) Token: 0x060017D7 RID: 6103 RVA: 0x000682B7 File Offset: 0x000664B7
		internal MemberMapping XmlnsMember
		{
			get
			{
				return this.xmlnsMember;
			}
			set
			{
				this.xmlnsMember = value;
			}
		}

		// Token: 0x1700050D RID: 1293
		// (get) Token: 0x060017D8 RID: 6104 RVA: 0x000682C0 File Offset: 0x000664C0
		// (set) Token: 0x060017D9 RID: 6105 RVA: 0x000682C8 File Offset: 0x000664C8
		internal bool HasWrapperElement
		{
			get
			{
				return this.hasWrapperElement;
			}
			set
			{
				this.hasWrapperElement = value;
			}
		}

		// Token: 0x1700050E RID: 1294
		// (get) Token: 0x060017DA RID: 6106 RVA: 0x000682D1 File Offset: 0x000664D1
		// (set) Token: 0x060017DB RID: 6107 RVA: 0x000682D9 File Offset: 0x000664D9
		internal bool ValidateRpcWrapperElement
		{
			get
			{
				return this.validateRpcWrapperElement;
			}
			set
			{
				this.validateRpcWrapperElement = value;
			}
		}

		// Token: 0x1700050F RID: 1295
		// (get) Token: 0x060017DC RID: 6108 RVA: 0x000682E2 File Offset: 0x000664E2
		// (set) Token: 0x060017DD RID: 6109 RVA: 0x000682EA File Offset: 0x000664EA
		internal bool WriteAccessors
		{
			get
			{
				return this.writeAccessors;
			}
			set
			{
				this.writeAccessors = value;
			}
		}

		// Token: 0x04000B03 RID: 2819
		private MemberMapping[] members;

		// Token: 0x04000B04 RID: 2820
		private bool hasWrapperElement = true;

		// Token: 0x04000B05 RID: 2821
		private bool validateRpcWrapperElement;

		// Token: 0x04000B06 RID: 2822
		private bool writeAccessors = true;

		// Token: 0x04000B07 RID: 2823
		private MemberMapping xmlnsMember;
	}
}
