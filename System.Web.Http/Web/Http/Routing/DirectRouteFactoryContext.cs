using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http.Controllers;
using System.Web.Http.Properties;

namespace System.Web.Http.Routing
{
	// Token: 0x0200000E RID: 14
	public class DirectRouteFactoryContext
	{
		// Token: 0x06000071 RID: 113 RVA: 0x00003404 File Offset: 0x00001604
		public DirectRouteFactoryContext(string prefix, IReadOnlyCollection<HttpActionDescriptor> actions, IInlineConstraintResolver inlineConstraintResolver, bool targetIsAction)
		{
			if (actions == null)
			{
				throw new ArgumentNullException("actions");
			}
			if (inlineConstraintResolver == null)
			{
				throw new ArgumentNullException("inlineConstraintResolver");
			}
			this._prefix = prefix;
			this._actions = actions;
			this._inlineConstraintResolver = inlineConstraintResolver;
			HttpActionDescriptor httpActionDescriptor = actions.FirstOrDefault<HttpActionDescriptor>();
			if (httpActionDescriptor != null)
			{
				this._actionName = httpActionDescriptor.ActionName;
			}
			this._targetIsAction = targetIsAction;
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000072 RID: 114 RVA: 0x00003466 File Offset: 0x00001666
		public string Prefix
		{
			get
			{
				return this._prefix;
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000073 RID: 115 RVA: 0x0000346E File Offset: 0x0000166E
		public IReadOnlyCollection<HttpActionDescriptor> Actions
		{
			get
			{
				return this._actions;
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000074 RID: 116 RVA: 0x00003476 File Offset: 0x00001676
		public IInlineConstraintResolver InlineConstraintResolver
		{
			get
			{
				return this._inlineConstraintResolver;
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000075 RID: 117 RVA: 0x0000347E File Offset: 0x0000167E
		public bool TargetIsAction
		{
			get
			{
				return this._targetIsAction;
			}
		}

		// Token: 0x06000076 RID: 118 RVA: 0x00003486 File Offset: 0x00001686
		public IDirectRouteBuilder CreateBuilder(string template)
		{
			return this.CreateBuilderInternal(template);
		}

		// Token: 0x06000077 RID: 119 RVA: 0x0000348F File Offset: 0x0000168F
		internal virtual IDirectRouteBuilder CreateBuilderInternal(string template)
		{
			return this.CreateBuilder(template, this._inlineConstraintResolver);
		}

		// Token: 0x06000078 RID: 120 RVA: 0x000034A0 File Offset: 0x000016A0
		public IDirectRouteBuilder CreateBuilder(string template, IInlineConstraintResolver constraintResolver)
		{
			DirectRouteBuilder directRouteBuilder = new DirectRouteBuilder(this._actions, this._targetIsAction);
			string text = DirectRouteFactoryContext.BuildRouteTemplate(this._prefix, template);
			this.ValidateTemplate(text);
			if (constraintResolver != null)
			{
				HttpRouteValueDictionary defaults = new HttpRouteValueDictionary();
				HttpRouteValueDictionary constraints = new HttpRouteValueDictionary();
				string text2 = InlineRouteTemplateParser.ParseRouteTemplate(text, defaults, constraints, constraintResolver);
				HttpParsedRoute parsedRoute = RouteParser.Parse(text2);
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

		// Token: 0x06000079 RID: 121 RVA: 0x00003530 File Offset: 0x00001730
		private static string BuildRouteTemplate(string routePrefix, string routeTemplate)
		{
			if (string.IsNullOrEmpty(routeTemplate))
			{
				return routePrefix ?? string.Empty;
			}
			if (routeTemplate.StartsWith("~/", StringComparison.Ordinal))
			{
				return routeTemplate.Substring(2);
			}
			if (string.IsNullOrEmpty(routePrefix))
			{
				return routeTemplate;
			}
			return routePrefix + '/' + routeTemplate;
		}

		// Token: 0x0600007A RID: 122 RVA: 0x00003580 File Offset: 0x00001780
		private void ValidateTemplate(string template)
		{
			if (template != null && template.StartsWith("/", StringComparison.Ordinal))
			{
				string message = Error.Format(SRResources.AttributeRoutes_InvalidTemplate, new object[]
				{
					template,
					this._actionName
				});
				throw new InvalidOperationException(message);
			}
		}

		// Token: 0x04000019 RID: 25
		private readonly string _actionName;

		// Token: 0x0400001A RID: 26
		private readonly string _prefix;

		// Token: 0x0400001B RID: 27
		private readonly IReadOnlyCollection<HttpActionDescriptor> _actions;

		// Token: 0x0400001C RID: 28
		private readonly IInlineConstraintResolver _inlineConstraintResolver;

		// Token: 0x0400001D RID: 29
		private readonly bool _targetIsAction;
	}
}
