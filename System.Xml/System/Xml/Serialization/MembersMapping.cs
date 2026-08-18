using System;

namespace System.Xml.Serialization
{
	// Token: 0x020002D2 RID: 722
	internal class MembersMapping : TypeMapping
	{
		// Token: 0x1700084B RID: 2123
		// (get) Token: 0x06002222 RID: 8738 RVA: 0x0009FEFD File Offset: 0x0009EEFD
		// (set) Token: 0x06002223 RID: 8739 RVA: 0x0009FF05 File Offset: 0x0009EF05
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

		// Token: 0x1700084C RID: 2124
		// (get) Token: 0x06002224 RID: 8740 RVA: 0x0009FF0E File Offset: 0x0009EF0E
		// (set) Token: 0x06002225 RID: 8741 RVA: 0x0009FF16 File Offset: 0x0009EF16
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

		// Token: 0x1700084D RID: 2125
		// (get) Token: 0x06002226 RID: 8742 RVA: 0x0009FF1F File Offset: 0x0009EF1F
		// (set) Token: 0x06002227 RID: 8743 RVA: 0x0009FF27 File Offset: 0x0009EF27
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

		// Token: 0x1700084E RID: 2126
		// (get) Token: 0x06002228 RID: 8744 RVA: 0x0009FF30 File Offset: 0x0009EF30
		// (set) Token: 0x06002229 RID: 8745 RVA: 0x0009FF38 File Offset: 0x0009EF38
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

		// Token: 0x1700084F RID: 2127
		// (get) Token: 0x0600222A RID: 8746 RVA: 0x0009FF41 File Offset: 0x0009EF41
		// (set) Token: 0x0600222B RID: 8747 RVA: 0x0009FF49 File Offset: 0x0009EF49
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

		// Token: 0x0400149A RID: 5274
		private MemberMapping[] members;

		// Token: 0x0400149B RID: 5275
		private bool hasWrapperElement = true;

		// Token: 0x0400149C RID: 5276
		private bool validateRpcWrapperElement;

		// Token: 0x0400149D RID: 5277
		private bool writeAccessors = true;

		// Token: 0x0400149E RID: 5278
		private MemberMapping xmlnsMember;
	}
}
