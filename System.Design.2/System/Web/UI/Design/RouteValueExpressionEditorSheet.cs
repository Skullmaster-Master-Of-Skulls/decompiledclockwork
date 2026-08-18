using System;
using System.ComponentModel;
using System.Design;

namespace System.Web.UI.Design
{
	// Token: 0x02000068 RID: 104
	public class RouteValueExpressionEditorSheet : ExpressionEditorSheet
	{
		// Token: 0x0600030D RID: 781 RVA: 0x000105E2 File Offset: 0x0000E7E2
		public RouteValueExpressionEditorSheet(string expression, IServiceProvider serviceProvider) : base(serviceProvider)
		{
			if (!string.IsNullOrEmpty(expression))
			{
				this.RouteValue = expression;
			}
		}

		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x0600030E RID: 782 RVA: 0x000105FA File Offset: 0x0000E7FA
		// (set) Token: 0x0600030F RID: 783 RVA: 0x00010610 File Offset: 0x0000E810
		[DefaultValue("")]
		[SRDescription("RouteValueExpressionEditorSheet_RouteValue")]
		public string RouteValue
		{
			get
			{
				if (this._routeValue == null)
				{
					return string.Empty;
				}
				return this._routeValue;
			}
			set
			{
				this._routeValue = value;
			}
		}

		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x06000310 RID: 784 RVA: 0x00010619 File Offset: 0x0000E819
		public override bool IsValid
		{
			get
			{
				return !string.IsNullOrEmpty(this.RouteValue);
			}
		}

		// Token: 0x06000311 RID: 785 RVA: 0x00010629 File Offset: 0x0000E829
		public override string GetExpression()
		{
			return this.RouteValue;
		}

		// Token: 0x04000168 RID: 360
		private string _routeValue;
	}
}
