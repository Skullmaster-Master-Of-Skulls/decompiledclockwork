using System;
using System.Data.Metadata.Edm;

namespace System.Data.Query.PlanCompiler
{
	// Token: 0x0200004A RID: 74
	internal class ExtentPair
	{
		// Token: 0x17000081 RID: 129
		// (get) Token: 0x06000604 RID: 1540 RVA: 0x00019D68 File Offset: 0x00017F68
		internal EntitySetBase Left
		{
			get
			{
				return this.m_left;
			}
		}

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x06000605 RID: 1541 RVA: 0x00019D70 File Offset: 0x00017F70
		internal EntitySetBase Right
		{
			get
			{
				return this.m_right;
			}
		}

		// Token: 0x06000606 RID: 1542 RVA: 0x00019D78 File Offset: 0x00017F78
		public override bool Equals(object obj)
		{
			ExtentPair extentPair = obj as ExtentPair;
			return extentPair != null && extentPair.Left.Equals(this.Left) && extentPair.Right.Equals(this.Right);
		}

		// Token: 0x06000607 RID: 1543 RVA: 0x00019DB5 File Offset: 0x00017FB5
		public override int GetHashCode()
		{
			return this.Left.GetHashCode() << 4 ^ this.Right.GetHashCode();
		}

		// Token: 0x06000608 RID: 1544 RVA: 0x00019DD0 File Offset: 0x00017FD0
		internal ExtentPair(EntitySetBase left, EntitySetBase right)
		{
			this.m_left = left;
			this.m_right = right;
		}

		// Token: 0x04000769 RID: 1897
		private EntitySetBase m_left;

		// Token: 0x0400076A RID: 1898
		private EntitySetBase m_right;
	}
}
