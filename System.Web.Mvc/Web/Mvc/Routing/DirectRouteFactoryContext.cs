using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc.Properties;
using System.Web.Routing;

namespace System.Web.Mvc.Routing
{
	// Token: 0x02000009 RID: 9
	public class DirectRouteFactoryContext
	{
		// Token: 0x06000049 RID: 73 RVA: 0x00002CF0 File Offset: 0x00000EF0
		public DirectRouteFactoryContext(string areaPrefix, string controllerPrefix, IReadOnlyCollection<ActionDescriptor> actions, IInlineConstraintResolver inlineConstraintResolver, bool targetIsAction)
		{
			if (actions == null)
			{
				throw new ArgumentNullException("actions");
			}
			if (inlineConstraintResolver == null)
			{
				throw new ArgumentNullException("inlineConstraintResolver");
			}
			this._areaPrefix = areaPrefix;
			this._controllerPrefix = controllerPrefix;
			this._actions = actions;
			this._inlineConstraintResolver = inlineConstraintResolver;
			ActionDescriptor actionDescriptor = actions.FirstOrDefault<ActionDescriptor>();
			if (actionDescriptor != null)
			{
				this._actionName = actionDescriptor.ActionName;
				ControllerDescriptor controllerDescriptor = actionDescriptor.ControllerDescriptor;
				if (controllerDescriptor != null)
				{
					this._controllerName = controllerDescriptor.ControllerName;
				}
			}
			this._targetIsAction = targetIsAction;
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x0600004A RID: 74 RVA: 0x00002D71 File Offset: 0x00000F71
		public string AreaPrefix
		{
			get
			{
				return this._areaPrefix;
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x0600004B RID: 75 RVA: 0x00002D79 File Offset: 0x00000F79
		public string ControllerPrefix
		{
			get
			{
				return this._controllerPrefix;
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x0600004C RID: 76 RVA: 0x00002D81 File Offset: 0x00000F81
		public IReadOnlyCollection<ActionDescriptor> Actions
		{
			get
			{
				return this._actions;
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x0600004D RID: 77 RVA: 0x00002D89 File Offset: 0x00000F89
		public IInlineConstraintResolver InlineConstraintResolver
		{
			get
			{
				return this._inlineConstraintResolver;
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x0600004E RID: 78 RVA: 0x00002D91 File Offset: 0x00000F91
		public bool TargetIsAction
		{
			get
			{
				return this._targetIsAction;
			}
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00002D99 File Offset: 0x00000F99
		public IDirectRouteBuilder CreateBuilder(string template)
		{
			return this.CreateBuilderInternal(template);
		}

		// Token: 0x06000050 RID: 80 RVA: 0x00002DA2 File Offset: 0x00000FA2
		internal virtual IDirectRouteBuilder CreateBuilderInternal(string template)
		{
			return this.CreateBuilder(template, this._inlineConstraintResolver);
		}

		// Token: 0x06000051 RID: 81 RVA: 0x00002DB4 File Offset: 0x00000FB4
		public IDirectRouteBuilder CreateBuilder(string template, IInlineConstraintResolver constraintResolver)
		{
			DirectRouteBuilder directRouteBuilder = new DirectRouteBuilder(this._actions, this._targetIsAction);
			string text = DirectRouteFactoryContext.BuildRouteTemplate(this._areaPrefix, this._controllerPrefix, template ?? string.Empty);
			this.ValidateTemplate(text);
			if (constraintResolver != null)
			{
				RouteValueDictionary defaults = new RouteValueDictionary();
				RouteValueDictionary constraints = new RouteValueDictionary();
				string text2 = InlineRouteTemplateParser.ParseRouteTemplate(text, defaults, constraints, constraintResolver);
				ParsedRoute parsedRoute = RouteParser.Parse(text2);
				decimal precedence = RoutePrecedence.Compute(parsedRoute, constraints);
				directRouteBuilder.Defaults = defaults;
				directRouteBuilder.Constraints = constraints;
				directRouteBuilder.Template = text2;
				directRouteBuilder.Precedence = precedence;
				directRouteBuilder.ParsedRoute = parsedRoute;
			}
			else
			{
				directRouteBuilder.Template = text;
			}
			return directRouteBuilder;
		}

		// Token: 0x06000052 RID: 82 RVA: 0x00002E54 File Offset: 0x00001054
		internal static string BuildRouteTemplate(string areaPrefix, string prefix, string template)
		{
			if (template != null && template.StartsWith("~/", StringComparison.Ordinal))
			{
				return template.Substring(2);
			}
			if (prefix == null && areaPrefix == null)
			{
				return template;
			}
			StringBuilder stringBuilder = new StringBuilder();
			if (areaPrefix != null)
			{
				stringBuilder.Append(areaPrefix);
			}
			if (!string.IsNullOrEmpty(prefix))
			{
				if (stringBuilder.Length > 0)
				{
					stringBuilder.Append('/');
				}
				stringBuilder.Append(prefix);
			}
			if (!string.IsNullOrEmpty(template))
			{
				if (stringBuilder.Length > 0)
				{
					stringBuilder.Append('/');
				}
				stringBuilder.Append(template);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000053 RID: 83 RVA: 0x00002EE0 File Offset: 0x000010E0
		private void ValidateTemplate(string template)
		{
			if (template != null && template.StartsWith("/", StringComparison.Ordinal))
			{
				string message = Error.Format(MvcResources.RouteTemplate_CannotStart_WithForwardSlash, new object[]
				{
					template,
					this._actionName,
					this._controllerName
				});
				throw new InvalidOperationException(message);
			}
		}

		// Token: 0x04000010 RID: 16
		private readonly string _actionName;

		// Token: 0x04000011 RID: 17
		private readonly string _controllerName;

		// Token: 0x04000012 RID: 18
		private readonly string _areaPrefix;

		// Token: 0x04000013 RID: 19
		private readonly string _controllerPrefix;

		// Token: 0x04000014 RID: 20
		private readonly IReadOnlyCollection<ActionDescriptor> _actions;

		// Token: 0x04000015 RID: 21
		private readonly IInlineConstraintResolver _inlineConstraintResolver;

		// Token: 0x04000016 RID: 22
		private readonly bool _targetIsAction;
	}
}
