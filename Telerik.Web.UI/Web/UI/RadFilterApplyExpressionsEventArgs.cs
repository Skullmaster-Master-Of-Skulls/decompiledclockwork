using System;

namespace Telerik.Web.UI
{
	// Token: 0x020018C2 RID: 6338
	public class RadFilterApplyExpressionsEventArgs : EventArgs
	{
		// Token: 0x170049F5 RID: 18933
		// (get) Token: 0x0600F568 RID: 62824 RVA: 0x0037BD9F File Offset: 0x00379F9F
		public RadFilterGroupExpression ExpressionRoot
		{
			get
			{
				return this._expressionRoot;
			}
		}

		// Token: 0x0600F569 RID: 62825 RVA: 0x0037BDA7 File Offset: 0x00379FA7
		public RadFilterApplyExpressionsEventArgs(RadFilterGroupExpression expressionRoot)
		{
			this._expressionRoot = expressionRoot;
		}

		// Token: 0x04004648 RID: 17992
		private RadFilterGroupExpression _expressionRoot;
	}
}
