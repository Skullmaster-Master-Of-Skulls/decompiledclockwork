using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc.Properties;

namespace System.Web.Mvc.Routing
{
	// Token: 0x0200000F RID: 15
	internal static class RouteParser
	{
		// Token: 0x06000065 RID: 101 RVA: 0x0000319C File Offset: 0x0000139C
		private static string GetLiteral(string segmentLiteral)
		{
			string text = segmentLiteral.Replace("{{", string.Empty).Replace("}}", string.Empty);
			if (text.Contains("{") || text.Contains("}"))
			{
				return null;
			}
			return segmentLiteral.Replace("{{", "{").Replace("}}", "}");
		}

		// Token: 0x06000066 RID: 102 RVA: 0x00003204 File Offset: 0x00001404
		private static int IndexOfFirstOpenParameter(string segment, int startIndex)
		{
			for (;;)
			{
				startIndex = segment.IndexOf('{', startIndex);
				if (startIndex == -1)
				{
					break;
				}
				if (startIndex + 1 == segment.Length || (startIndex + 1 < segment.Length && segment[startIndex + 1] != '{'))
				{
					return startIndex;
				}
				startIndex += 2;
			}
			return -1;
		}

		// Token: 0x06000067 RID: 103 RVA: 0x00003242 File Offset: 0x00001442
		internal static bool IsSeparator(string s)
		{
			return string.Equals(s, "/", StringComparison.Ordinal);
		}

		// Token: 0x06000068 RID: 104 RVA: 0x00003250 File Offset: 0x00001450
		private static bool IsValidParameterName(string parameterName)
		{
			if (parameterName.Length == 0)
			{
				return false;
			}
			foreach (char c in parameterName)
			{
				if (c == '/' || c == '{' || c == '}')
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000069 RID: 105 RVA: 0x00003292 File Offset: 0x00001492
		internal static bool IsInvalidRouteTemplate(string routeTemplate)
		{
			return routeTemplate.StartsWith("~", StringComparison.Ordinal) || routeTemplate.StartsWith("/", StringComparison.Ordinal) || routeTemplate.IndexOf('?') != -1;
		}

		// Token: 0x0600006A RID: 106 RVA: 0x000032C0 File Offset: 0x000014C0
		public static ParsedRoute Parse(string routeTemplate)
		{
			if (routeTemplate == null)
			{
				routeTemplate = string.Empty;
			}
			if (RouteParser.IsInvalidRouteTemplate(routeTemplate))
			{
				throw Error.Argument("routeTemplate", MvcResources.Route_InvalidRouteTemplate, new object[0]);
			}
			List<string> list = RouteParser.SplitUriToPathSegmentStrings(routeTemplate);
			Exception ex = RouteParser.ValidateUriParts(list);
			if (ex != null)
			{
				throw ex;
			}
			List<PathSegment> pathSegments = RouteParser.SplitUriToPathSegments(list);
			return new ParsedRoute(pathSegments);
		}

		// Token: 0x0600006B RID: 107 RVA: 0x00003318 File Offset: 0x00001518
		private static List<PathSubsegment> ParseUriSegment(string segment, out Exception exception)
		{
			int i = 0;
			List<PathSubsegment> list = new List<PathSubsegment>();
			while (i < segment.Length)
			{
				int num = RouteParser.IndexOfFirstOpenParameter(segment, i);
				if (num == -1)
				{
					string literal = RouteParser.GetLiteral(segment.Substring(i));
					if (literal == null)
					{
						exception = Error.Argument("routeTemplate", MvcResources.Route_MismatchedParameter, new object[]
						{
							segment
						});
						return null;
					}
					if (literal.Length > 0)
					{
						list.Add(new PathLiteralSubsegment(literal));
						break;
					}
					break;
				}
				else
				{
					int num2 = segment.IndexOf('}', num + 1);
					if (num2 == -1)
					{
						exception = Error.Argument("routeTemplate", MvcResources.Route_MismatchedParameter, new object[]
						{
							segment
						});
						return null;
					}
					string literal2 = RouteParser.GetLiteral(segment.Substring(i, num - i));
					if (literal2 == null)
					{
						exception = Error.Argument("routeTemplate", MvcResources.Route_MismatchedParameter, new object[]
						{
							segment
						});
						return null;
					}
					if (literal2.Length > 0)
					{
						list.Add(new PathLiteralSubsegment(literal2));
					}
					string parameterName = segment.Substring(num + 1, num2 - num - 1);
					list.Add(new PathParameterSubsegment(parameterName));
					i = num2 + 1;
				}
			}
			exception = null;
			return list;
		}

		// Token: 0x0600006C RID: 108 RVA: 0x00003444 File Offset: 0x00001644
		private static List<PathSegment> SplitUriToPathSegments(List<string> uriParts)
		{
			List<PathSegment> list = new List<PathSegment>();
			foreach (string text in uriParts)
			{
				bool flag = RouteParser.IsSeparator(text);
				if (flag)
				{
					list.Add(new PathSeparatorSegment());
				}
				else
				{
					Exception ex;
					List<PathSubsegment> subsegments = RouteParser.ParseUriSegment(text, out ex);
					list.Add(new PathContentSegment(subsegments));
				}
			}
			return list;
		}

		// Token: 0x0600006D RID: 109 RVA: 0x000034C4 File Offset: 0x000016C4
		internal static List<string> SplitUriToPathSegmentStrings(string uri)
		{
			List<string> list = new List<string>();
			if (string.IsNullOrEmpty(uri))
			{
				return list;
			}
			int i = 0;
			while (i < uri.Length)
			{
				int num = uri.IndexOf('/', i);
				if (num == -1)
				{
					string text = uri.Substring(i);
					if (text.Length > 0)
					{
						list.Add(text);
						break;
					}
					break;
				}
				else
				{
					string text2 = uri.Substring(i, num - i);
					if (text2.Length > 0)
					{
						list.Add(text2);
					}
					list.Add("/");
					i = num + 1;
				}
			}
			return list;
		}

		// Token: 0x0600006E RID: 110 RVA: 0x0000355C File Offset: 0x0000175C
		private static Exception ValidateUriParts(List<string> pathSegments)
		{
			HashSet<string> usedParameterNames = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
			bool? flag = null;
			bool flag2 = false;
			foreach (string text in pathSegments)
			{
				if (flag2)
				{
					return Error.Argument("routeTemplate", MvcResources.Route_CatchAllMustBeLast, new object[]
					{
						"routeTemplate"
					});
				}
				bool flag3;
				if (flag == null)
				{
					flag = new bool?(RouteParser.IsSeparator(text));
					flag3 = flag.Value;
				}
				else
				{
					flag3 = RouteParser.IsSeparator(text);
					if (flag3 && flag.Value)
					{
						return Error.Argument("routeTemplate", MvcResources.Route_CannotHaveConsecutiveSeparators, new object[0]);
					}
					flag = new bool?(flag3);
				}
				if (!flag3)
				{
					Exception ex;
					List<PathSubsegment> list = RouteParser.ParseUriSegment(text, out ex);
					if (ex != null)
					{
						return ex;
					}
					ex = RouteParser.ValidateUriSegment(list, usedParameterNames);
					if (ex != null)
					{
						return ex;
					}
					flag2 = list.Any((PathSubsegment seg) => seg is PathParameterSubsegment && ((PathParameterSubsegment)seg).IsCatchAll);
				}
			}
			return null;
		}

		// Token: 0x0600006F RID: 111 RVA: 0x00003698 File Offset: 0x00001898
		private static Exception ValidateUriSegment(List<PathSubsegment> pathSubsegments, HashSet<string> usedParameterNames)
		{
			bool flag = false;
			Type left = null;
			foreach (PathSubsegment pathSubsegment in pathSubsegments)
			{
				if (left != null && left == pathSubsegment.GetType())
				{
					return Error.Argument("routeTemplate", MvcResources.Route_CannotHaveConsecutiveParameters, new object[0]);
				}
				left = pathSubsegment.GetType();
				if (!(pathSubsegment is PathLiteralSubsegment))
				{
					PathParameterSubsegment pathParameterSubsegment = pathSubsegment as PathParameterSubsegment;
					if (pathParameterSubsegment != null)
					{
						string parameterName = pathParameterSubsegment.ParameterName;
						if (pathParameterSubsegment.IsCatchAll)
						{
							flag = true;
						}
						if (!RouteParser.IsValidParameterName(parameterName))
						{
							return Error.Argument("routeTemplate", MvcResources.Route_InvalidParameterName, new object[]
							{
								parameterName
							});
						}
						if (usedParameterNames.Contains(parameterName))
						{
							return Error.Argument("routeTemplate", MvcResources.Route_RepeatedParameter, new object[]
							{
								parameterName
							});
						}
						usedParameterNames.Add(parameterName);
					}
				}
			}
			if (flag && pathSubsegments.Count != 1)
			{
				return Error.Argument("routeTemplate", MvcResources.Route_CannotHaveCatchAllInMultiSegment, new object[0]);
			}
			return null;
		}
	}
}
