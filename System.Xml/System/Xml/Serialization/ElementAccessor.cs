using System;

namespace System.Xml.Serialization
{
	// Token: 0x020002C0 RID: 704
	internal class ElementAccessor : Accessor
	{
		// Token: 0x17000809 RID: 2057
		// (get) Token: 0x06002192 RID: 8594 RVA: 0x0009F016 File Offset: 0x0009E016
		// (set) Token: 0x06002193 RID: 8595 RVA: 0x0009F01E File Offset: 0x0009E01E
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

		// Token: 0x1700080A RID: 2058
		// (get) Token: 0x06002194 RID: 8596 RVA: 0x0009F027 File Offset: 0x0009E027
		// (set) Token: 0x06002195 RID: 8597 RVA: 0x0009F02F File Offset: 0x0009E02F
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

		// Token: 0x1700080B RID: 2059
		// (get) Token: 0x06002196 RID: 8598 RVA: 0x0009F038 File Offset: 0x0009E038
		// (set) Token: 0x06002197 RID: 8599 RVA: 0x0009F040 File Offset: 0x0009E040
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

		// Token: 0x06002198 RID: 8600 RVA: 0x0009F04C File Offset: 0x0009E04C
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

		// Token: 0x04001467 RID: 5223
		private bool nullable;

		// Token: 0x04001468 RID: 5224
		private bool isSoap;

		// Token: 0x04001469 RID: 5225
		private bool unbounded;
	}
}
