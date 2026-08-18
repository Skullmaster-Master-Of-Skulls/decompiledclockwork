using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http.Properties;

namespace System.Web.Http.Routing
{
	// Token: 0x02000015 RID: 21
	internal static class RouteParser
	{
		// Token: 0x0600008D RID: 141 RVA: 0x00003840 File Offset: 0x00001A40
		private static string GetLiteral(string segmentLiteral)
		{
			string text = segmentLiteral.Replace("{{", string.Empty).Replace("}}", string.Empty);
			if (text.Contains("{") || text.Contains("}"))
			{
				return null;
			}
			return segmentLiteral.Replace("{{", "{").Replace("}}", "}");
		}

		// Token: 0x0600008E RID: 142 RVA: 0x000038A8 File Offset: 0x00001AA8
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

		// Token: 0x0600008F RID: 143 RVA: 0x000038E6 File Offset: 0x00001AE6
		internal static bool IsSeparator(string s)
		{
			return string.Equals(s, "/", StringComparison.Ordinal);
		}

		// Token: 0x06000090 RID: 144 RVA: 0x000038F4 File Offset: 0x00001AF4
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

		// Token: 0x06000091 RID: 145 RVA: 0x00003936 File Offset: 0x00001B36
		internal static bool IsInvalidRouteTemplate(string routeTemplate)
		{
			return routeTemplate.StartsWith("~", StringComparison.Ordinal) || routeTemplate.StartsWith("/", StringComparison.Ordinal) || routeTemplate.IndexOf('?') != -1;
		}

		// Token: 0x06000092 RID: 146 RVA: 0x00003964 File Offset: 0x00001B64
		public static HttpParsedRoute Parse(string routeTemplate)
		{
			if (routeTemplate == null)
			{
				routeTemplate = string.Empty;
			}
			if (RouteParser.IsInvalidRouteTemplate(routeTemplate))
			{
				throw Error.Argument("routeTemplate", SRResources.Route_InvalidRouteTemplate, new object[0]);
			}
			List<string> list = RouteParser.SplitUriToPathSegmentStrings(routeTemplate);
			Exception ex = RouteParser.ValidateUriParts(list);
			if (ex != null)
			{
				throw ex;
			}
			List<PathSegment> pathSegments = RouteParser.SplitUriToPathSegments(list);
			return new HttpParsedRoute(pathSegments);
		}

		// Token: 0x06000093 RID: 147 RVA: 0x000039BC File Offset: 0x00001BBC
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
						exception = Error.Argument("routeTemplate", SRResources.Route_MismatchedParameter, new object[]
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
						exception = Error.Argument("routeTemplate", SRResources.Route_MismatchedParameter, new object[]
						{
							segment
						});
						return null;
					}
					string literal2 = RouteParser.GetLiteral(segment.Substring(i, num - i));
					if (literal2 == null)
					{
						exception = Error.Argument("routeTemplate", SRResources.Route_MismatchedParameter, new object[]
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

		// Token: 0x06000094 RID: 148 RVA: 0x00003AE8 File Offset: 0x00001CE8
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

		// Token: 0x06000095 RID: 149 RVA: 0x00003B68 File Offset: 0x00001D68
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

		// Token: 0x06000096 RID: 150 RVA: 0x00003C00 File Offset: 0x00001E00
		private static Exception ValidateUriParts(List<string> pathSegments)
		{
			HashSet<string> usedParameterNames = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
			bool? flag = null;
			bool flag2 = false;
			foreach (string text in pathSegments)
			{
				if (flag2)
				{
					return Error.Argument("routeTemplate", SRResources.Route_CatchAllMustBeLast, new object[]
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
						return Error.Argument("routeTemplate", SRResources.Route_CannotHaveConsecutiveSeparators, new object[0]);
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

		// Token: 0x06000097 RID: 151 RVA: 0x00003D3C File Offset: 0x00001F3C
		private static Exception ValidateUriSegment(List<PathSubsegment> pathSubsegments, HashSet<string> usedParameterNames)
		{
			bool flag = false;
			Type left = null;
			foreach (PathSubsegment pathSubsegment in pathSubsegments)
			{
				if (left != null && left == pathSubsegment.GetType())
				{
					return Error.Argument("routeTemplate", SRResources.Route_CannotHaveConsecutiveParameters, new object[0]);
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
							return Error.Argument("routeTemplate", SRResources.Route_InvalidParameterName, new object[]
							{
								parameterName
							});
						}
						if (usedParameterNames.Contains(parameterName))
						{
							return Error.Argument("routeTemplate", SRResources.Route_RepeatedParameter, new object[]
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
				return Error.Argument("routeTemplate", SRResources.Route_CannotHaveCatchAllInMultiSegment, new object[0]);
			}
			return null;
		}
	}
}
