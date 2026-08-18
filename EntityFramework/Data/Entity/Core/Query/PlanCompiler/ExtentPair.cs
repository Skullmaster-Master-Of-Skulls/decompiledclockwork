using System;
using System.Data.Entity.Core.Metadata.Edm;

namespace System.Data.Entity.Core.Query.PlanCompiler
{
	// Token: 0x0200066C RID: 1644
	internal class ExtentPair
	{
		// Token: 0x170009E0 RID: 2528
		// (get) Token: 0x06004037 RID: 16439 RVA: 0x00125E9D File Offset: 0x0012409D
		internal EntitySetBase Left
		{
			get
			{
				return this.m_left;
			}
		}

		// Token: 0x170009E1 RID: 2529
		// (get) Token: 0x06004038 RID: 16440 RVA: 0x00125EA5 File Offset: 0x001240A5
		internal EntitySetBase Right
		{
			get
			{
				return this.m_right;
			}
		}

		// Token: 0x06004039 RID: 16441 RVA: 0x00125EB0 File Offset: 0x001240B0
		public override bool Equals(object obj)
		{
			ExtentPair extentPair = obj as ExtentPair;
			return extentPair != null && extentPair.Left.Equals(this.Left) && extentPair.Right.Equals(this.Right);
		}

		// Token: 0x0600403A RID: 16442 RVA: 0x00125EED File Offset: 0x001240ED
		public override int GetHashCode()
		{
			return this.Left.GetHashCode() << 4 ^ this.Right.GetHashCode();
		}

		// Token: 0x0600403B RID: 16443 RVA: 0x00125F08 File Offset: 0x00124108
		internal ExtentPair(EntitySetBase left, EntitySetBase right)
		{
			this.m_left = left;
			this.m_right = right;
		}

		// Token: 0x040017E5 RID: 6117
		private readonly EntitySetBase m_left;

		// Token: 0x040017E6 RID: 6118
		private readonly EntitySetBase m_right;
	}
}
