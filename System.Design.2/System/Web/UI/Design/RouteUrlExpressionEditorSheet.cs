using System;
using System.ComponentModel;
using System.Design;
using System.Text;
using System.Web.Compilation;
using System.Web.Routing;

namespace System.Web.UI.Design
{
	// Token: 0x02000066 RID: 102
	public class RouteUrlExpressionEditorSheet : ExpressionEditorSheet
	{
		// Token: 0x06000303 RID: 771 RVA: 0x0001043C File Offset: 0x0000E63C
		public RouteUrlExpressionEditorSheet(string expression, IServiceProvider serviceProvider) : base(serviceProvider)
		{
			if (!string.IsNullOrEmpty(expression))
			{
				string routeName = null;
				RouteValueDictionary routeValueDictionary = new RouteValueDictionary();
				if (RouteUrlExpressionBuilder.TryParseRouteExpression(expression, routeValueDictionary, out routeName))
				{
					this.RouteName = routeName;
					StringBuilder stringBuilder = new StringBuilder();
					foreach (string text in routeValueDictionary.Keys)
					{
						if (stringBuilder.Length > 0)
						{
							stringBuilder.Append(",");
						}
						stringBuilder.Append(text).Append("=").Append(routeValueDictionary[text]);
					}
					this.RouteValues = stringBuilder.ToString();
				}
			}
		}

		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x06000304 RID: 772 RVA: 0x000104FC File Offset: 0x0000E6FC
		// (set) Token: 0x06000305 RID: 773 RVA: 0x00010512 File Offset: 0x0000E712
		[DefaultValue("")]
		[SRDescription("RouteUrlExpressionEditorSheet_RouteName")]
		public string RouteName
		{
			get
			{
				if (this._routeName == null)
				{
					return string.Empty;
				}
				return this._routeName;
			}
			set
			{
				this._routeName = value;
			}
		}

		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x06000306 RID: 774 RVA: 0x0001051B File Offset: 0x0000E71B
		// (set) Token: 0x06000307 RID: 775 RVA: 0x00010531 File Offset: 0x0000E731
		[DefaultValue("")]
		[SRDescription("RouteUrlExpressionEditorSheet_RouteValues")]
		public string RouteValues
		{
			get
			{
				if (this._routeValues == null)
				{
					return string.Empty;
				}
				return this._routeValues;
			}
			set
			{
				this._routeValues = value;
			}
		}

		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x06000308 RID: 776 RVA: 0x0001053C File Offset: 0x0000E73C
		public override bool IsValid
		{
			get
			{
				string text = null;
				RouteValueDictionary routeValues = new RouteValueDictionary();
				return RouteUrlExpressionBuilder.TryParseRouteExpression(this.GetExpression(), routeValues, out text);
			}
		}

		// Token: 0x06000309 RID: 777 RVA: 0x00010560 File Offset: 0x0000E760
		public override string GetExpression()
		{
			StringBuilder stringBuilder = new StringBuilder();
			if (!string.IsNullOrEmpty(this.RouteName))
			{
				stringBuilder.Append("RouteName=").Append(this.RouteName);
			}
			if (!string.IsNullOrEmpty(this.RouteValues))
			{
				if (stringBuilder.Length > 0)
				{
					stringBuilder.Append(",");
				}
				stringBuilder.Append(this.RouteValues);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x04000166 RID: 358
		private string _routeName;

		// Token: 0x04000167 RID: 359
		private string _routeValues;
	}
}
