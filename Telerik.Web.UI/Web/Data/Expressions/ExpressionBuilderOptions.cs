using System;

namespace Telerik.Web.Data.Expressions
{
	// Token: 0x02001BAC RID: 7084
	internal class ExpressionBuilderOptions
	{
		// Token: 0x17005399 RID: 21401
		// (get) Token: 0x06011219 RID: 70169 RVA: 0x003C7492 File Offset: 0x003C5692
		// (set) Token: 0x0601121A RID: 70170 RVA: 0x003C749A File Offset: 0x003C569A
		public bool LiftMemberAccessToNull { get; set; }

		// Token: 0x0601121B RID: 70171 RVA: 0x003C74A3 File Offset: 0x003C56A3
		public ExpressionBuilderOptions()
		{
			this.LiftMemberAccessToNull = true;
		}

		// Token: 0x0601121C RID: 70172 RVA: 0x003C74B2 File Offset: 0x003C56B2
		public void CopyFrom(ExpressionBuilderOptions other)
		{
			this.LiftMemberAccessToNull = other.LiftMemberAccessToNull;
		}
	}
}
