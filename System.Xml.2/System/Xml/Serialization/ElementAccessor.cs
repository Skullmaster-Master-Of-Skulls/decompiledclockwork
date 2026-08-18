using System;

namespace System.Xml.Serialization
{
	// Token: 0x02000147 RID: 327
	internal class ElementAccessor : Accessor
	{
		// Token: 0x170004C6 RID: 1222
		// (get) Token: 0x0600173C RID: 5948 RVA: 0x000672D6 File Offset: 0x000654D6
		// (set) Token: 0x0600173D RID: 5949 RVA: 0x000672DE File Offset: 0x000654DE
		internal bool IsSoap
		{
			get
			{
				return this.isSoap;
			}
			set
			{
				this.isSoap = value;
			}
		}

		// Token: 0x170004C7 RID: 1223
		// (get) Token: 0x0600173E RID: 5950 RVA: 0x000672E7 File Offset: 0x000654E7
		// (set) Token: 0x0600173F RID: 5951 RVA: 0x000672EF File Offset: 0x000654EF
		internal bool IsNullable
		{
			get
			{
				return this.nullable;
			}
			set
			{
				this.nullable = value;
			}
		}

		// Token: 0x170004C8 RID: 1224
		// (get) Token: 0x06001740 RID: 5952 RVA: 0x000672F8 File Offset: 0x000654F8
		// (set) Token: 0x06001741 RID: 5953 RVA: 0x00067300 File Offset: 0x00065500
		internal bool IsUnbounded
		{
			get
			{
				return this.unbounded;
			}
			set
			{
				this.unbounded = value;
			}
		}

		// Token: 0x06001742 RID: 5954 RVA: 0x0006730C File Offset: 0x0006550C
		internal ElementAccessor Clone()
		{
			return new ElementAccessor
			{
				nullable = this.nullable,
				IsTopLevelInSchema = base.IsTopLevelInSchema,
				Form = base.Form,
				isSoap = this.isSoap,
				Name = this.Name,
				Default = base.Default,
				Namespace = base.Namespace,
				Mapping = base.Mapping,
				Any = base.Any
			};
		}

		// Token: 0x04000ACC RID: 2764
		private bool nullable;

		// Token: 0x04000ACD RID: 2765
		private bool isSoap;

		// Token: 0x04000ACE RID: 2766
		private bool unbounded;
	}
}
