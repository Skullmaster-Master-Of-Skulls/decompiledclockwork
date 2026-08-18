using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Web.Http.Properties;
using System.Web.Http.Routing.Constraints;

namespace System.Web.Http.Routing
{
	// Token: 0x0200009B RID: 155
	internal class InlineRouteTemplateParser
	{
		// Token: 0x060003BA RID: 954 RVA: 0x0000BBA8 File Offset: 0x00009DA8
		public static string ParseRouteTemplate(string routeTemplate, IDictionary<string, object> defaults, IDictionary<string, object> constraints, IInlineConstraintResolver constraintResolver)
		{
			MatchCollection matchCollection = InlineRouteTemplateParser._parameterRegex.Matches(routeTemplate);
			foreach (object obj in matchCollection)
			{
				Match match = (Match)obj;
				string text = match.Groups["parameterName"].Value;
				if (text.StartsWith("*", StringComparison.OrdinalIgnoreCase))
				{
					text = text.Substring(1);
				}
				Group defaultValueGroup = match.Groups["defaultValue"];
				object defaultValue = InlineRouteTemplateParser.GetDefaultValue(defaultValueGroup);
				if (defaultValue != null)
				{
					defaults.Add(text, defaultValue);
				}
				Group constraintGroup = match.Groups["constraint"];
				bool isOptional = defaultValue == RouteParameter.Optional;
				IHttpRouteConstraint inlineConstraint = InlineRouteTemplateParser.GetInlineConstraint(constraintGroup, isOptional, constraintResolver);
				if (inlineConstraint != null)
				{
					constraints.Add(text, inlineConstraint);
				}
			}
			return InlineRouteTemplateParser._parameterRegex.Replace(routeTemplate, "{${parameterName}}");
		}

		// Token: 0x060003BB RID: 955 RVA: 0x0000BCA8 File Offset: 0x00009EA8
		private static object GetDefaultValue(Group defaultValueGroup)
		{
			if (!defaultValueGroup.Success)
			{
				return null;
			}
			string value = defaultValueGroup.Value;
			if (value == "?")
			{
				return RouteParameter.Optional;
			}
			return value.Substring(1);
		}

		// Token: 0x060003BC RID: 956 RVA: 0x0000BCE0 File Offset: 0x00009EE0
		private static IHttpRouteConstraint GetInlineConstraint(Group constraintGroup, bool isOptional, IInlineConstraintResolver constraintResolver)
		{
			List<IHttpRouteConstraint> list = new List<IHttpRouteConstraint>();
			foreach (object obj in constraintGroup.Captures)
			{
				Capture capture = (Capture)obj;
				string value = capture.Value;
				IHttpRouteConstraint httpRouteConstraint = constraintResolver.ResolveConstraint(value);
				if (httpRouteConstraint == null)
				{
					throw Error.InvalidOperation(SRResources.HttpRouteBuilder_CouldNotResolveConstraint, new object[]
					{
						constraintResolver.GetType().Name,
						value
					});
				}
				list.Add(httpRouteConstraint);
			}
			if (list.Count > 0)
			{
				IHttpRouteConstraint httpRouteConstraint2 = (list.Count == 1) ? list[0] : new CompoundRouteConstraint(list);
				if (isOptional)
				{
					httpRouteConstraint2 = new OptionalRouteConstraint(httpRouteConstraint2);
				}
				return httpRouteConstraint2;
			}
			return null;
		}

		// Token: 0x04000112 RID: 274
		private const string ParameterNameRegex = "(?<parameterName>.+?)";

		// Token: 0x04000113 RID: 275
		private const string ConstraintRegex = "(:(?<constraint>.*?(\\(.*?\\))?))*";

		// Token: 0x04000114 RID: 276
		private const string DefaultValueRegex = "(?<defaultValue>\\?|(=.*?))?";

		// Token: 0x04000115 RID: 277
		private static readonly Regex _parameterRegex = new Regex("{(?<parameterName>.+?)(:(?<constraint>.*?(\\(.*?\\))?))*(?<defaultValue>\\?|(=.*?))?}", RegexOptions.Compiled);
	}
}
