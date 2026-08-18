using System;

namespace System.Web.Mvc
{
	// Token: 0x020001DE RID: 478
	public class EmptyResult : ActionResult
	{
		// Token: 0x17000326 RID: 806
		// (get) Token: 0x06000E45 RID: 3653 RVA: 0x00025C64 File Offset: 0x00023E64
		internal static EmptyResult Instance
		{
			get
			{
				return EmptyResult._singleton;
			}
		}

		// Token: 0x06000E46 RID: 3654 RVA: 0x00025C6B File Offset: 0x00023E6B
		public override void ExecuteResult(ControllerContext context)
		{
		}

		// Token: 0x040003C0 RID: 960
		private static readonly EmptyResult _singleton = new EmptyResult();
	}
}
