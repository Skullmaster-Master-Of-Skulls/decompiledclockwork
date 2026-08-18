using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Web.Mvc.Properties;
using System.Web.Mvc.Routing.Constraints;
using System.Web.Routing;

namespace System.Web.Mvc.Routing
{
	// Token: 0x02000028 RID: 40
	internal class InlineRouteTemplateParser
	{
		// Token: 0x060000C3 RID: 195 RVA: 0x000048F8 File Offset: 0x00002AF8
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
				bool isOptional = defaultValue == UrlParameter.Optional;
				IRouteConstraint inlineConstraint = InlineRouteTemplateParser.GetInlineConstraint(constraintGroup, isOptional, constraintResolver);
				if (inlineConstraint != null)
				{
					constraints.Add(text, inlineConstraint);
				}
			}
			return InlineRouteTemplateParser._parameterRegex.Replace(routeTemplate, "{${parameterName}}");
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x000049F8 File Offset: 0x00002BF8
		private static object GetDefaultValue(Group defaultValueGroup)
		{
			if (!defaultValueGroup.Success)
			{
				return null;
			}
			string value = defaultValueGroup.Value;
			if (value == "?")
			{
				return UrlParameter.Optional;
			}
			return value.Substring(1);
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x00004A30 File Offset: 0x00002C30
		private static IRouteConstraint GetInlineConstraint(Group constraintGroup, bool isOptional, IInlineConstraintResolver constraintResolver)
		{
			List<IRouteConstraint> list = new List<IRouteConstraint>();
			foreach (object obj in constraintGroup.Captures)
			{
				Capture capture = (Capture)obj;
				string value = capture.Value;
				IRouteConstraint routeConstraint = constraintResolver.ResolveConstraint(value);
				if (routeConstraint == null)
				{
					throw Error.InvalidOperation(MvcResources.HttpRouteBuilder_CouldNotResolveConstraint, new object[]
					{
						constraintResolver.GetType().Name,
						value
					});
				}
				list.Add(routeConstraint);
			}
			if (list.Count > 0)
			{
				IRouteConstraint routeConstraint2 = (list.Count == 1) ? list[0] : new CompoundRouteConstraint(list);
				if (isOptional)
				{
					routeConstraint2 = new OptionalRouteConstraint(routeConstraint2);
				}
				return routeConstraint2;
			}
			return null;
		}

		// Token: 0x04000030 RID: 48
		private const string ParameterNameRegex = "(?<parameterName>.+?)";

		// Token: 0x04000031 RID: 49
		private const string ConstraintRegex = "(:(?<constraint>.*?(\\(.*?\\))?))*";

		// Token: 0x04000032 RID: 50
		private const string DefaultValueRegex = "(?<defaultValue>\\?|(=.*?))?";

		// Token: 0x04000033 RID: 51
		private static readonly Regex _parameterRegex = new Regex("{(?<parameterName>.+?)(:(?<constraint>.*?(\\(.*?\\))?))*(?<defaultValue>\\?|(=.*?))?}", RegexOptions.Compiled);
	}
}
