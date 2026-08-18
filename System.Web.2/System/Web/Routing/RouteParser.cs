using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace System.Web.Routing
{
	// Token: 0x0200014D RID: 333
	internal static class RouteParser
	{
		// Token: 0x0600135F RID: 4959 RVA: 0x000381CC File Offset: 0x000363CC
		private static string GetLiteral(string segmentLiteral)
		{
			string text = segmentLiteral.Replace("{{", "").Replace("}}", "");
			if (text.Contains("{") || text.Contains("}"))
			{
				return null;
			}
			return segmentLiteral.Replace("{{", "{").Replace("}}", "}");
		}

		// Token: 0x06001360 RID: 4960 RVA: 0x00038234 File Offset: 0x00036434
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

		// Token: 0x06001361 RID: 4961 RVA: 0x00038272 File Offset: 0x00036472
		internal static bool IsSeparator(string s)
		{
			return string.Equals(s, "/", StringComparison.Ordinal);
		}

		// Token: 0x06001362 RID: 4962 RVA: 0x00038280 File Offset: 0x00036480
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

		// Token: 0x06001363 RID: 4963 RVA: 0x000382C2 File Offset: 0x000364C2
		internal static bool IsInvalidRouteUrl(string routeUrl)
		{
			return routeUrl.StartsWith("~", StringComparison.Ordinal) || routeUrl.StartsWith("/", StringComparison.Ordinal) || routeUrl.IndexOf('?') != -1;
		}

		// Token: 0x06001364 RID: 4964 RVA: 0x000382F0 File Offset: 0x000364F0
		public static ParsedRoute Parse(string routeUrl)
		{
			if (routeUrl == null)
			{
				routeUrl = string.Empty;
			}
			if (RouteParser.IsInvalidRouteUrl(routeUrl))
			{
				throw new ArgumentException(SR.GetString("Route_InvalidRouteUrl"), "routeUrl");
			}
			IList<string> list = RouteParser.SplitUrlToPathSegmentStrings(routeUrl);
			Exception ex = RouteParser.ValidateUrlParts(list);
			if (ex != null)
			{
				throw ex;
			}
			IList<PathSegment> pathSegments = RouteParser.SplitUrlToPathSegments(list);
			return new ParsedRoute(pathSegments);
		}

		// Token: 0x06001365 RID: 4965 RVA: 0x00038344 File Offset: 0x00036544
		private static IList<PathSubsegment> ParseUrlSegment(string segment, out Exception exception)
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
						exception = new ArgumentException(string.Format(CultureInfo.CurrentUICulture, SR.GetString("Route_MismatchedParameter"), new object[]
						{
							segment
						}), "routeUrl");
						return null;
					}
					if (literal.Length > 0)
					{
						list.Add(new LiteralSubsegment(literal));
						break;
					}
					break;
				}
				else
				{
					int num2 = segment.IndexOf('}', num + 1);
					if (num2 == -1)
					{
						exception = new ArgumentException(string.Format(CultureInfo.CurrentUICulture, SR.GetString("Route_MismatchedParameter"), new object[]
						{
							segment
						}), "routeUrl");
						return null;
					}
					string literal2 = RouteParser.GetLiteral(segment.Substring(i, num - i));
					if (literal2 == null)
					{
						exception = new ArgumentException(string.Format(CultureInfo.CurrentUICulture, SR.GetString("Route_MismatchedParameter"), new object[]
						{
							segment
						}), "routeUrl");
						return null;
					}
					if (literal2.Length > 0)
					{
						list.Add(new LiteralSubsegment(literal2));
					}
					string parameterName = segment.Substring(num + 1, num2 - num - 1);
					list.Add(new ParameterSubsegment(parameterName));
					i = num2 + 1;
				}
			}
			exception = null;
			return list;
		}

		// Token: 0x06001366 RID: 4966 RVA: 0x0003848C File Offset: 0x0003668C
		private static IList<PathSegment> SplitUrlToPathSegments(IList<string> urlParts)
		{
			List<PathSegment> list = new List<PathSegment>();
			foreach (string text in urlParts)
			{
				bool flag = RouteParser.IsSeparator(text);
				if (flag)
				{
					list.Add(new SeparatorPathSegment());
				}
				else
				{
					Exception ex;
					IList<PathSubsegment> subsegments = RouteParser.ParseUrlSegment(text, out ex);
					list.Add(new ContentPathSegment(subsegments));
				}
			}
			return list;
		}

		// Token: 0x06001367 RID: 4967 RVA: 0x00038504 File Offset: 0x00036704
		internal static IList<string> SplitUrlToPathSegmentStrings(string url)
		{
			List<string> list = new List<string>();
			if (string.IsNullOrEmpty(url))
			{
				return list;
			}
			int i = 0;
			while (i < url.Length)
			{
				int num = url.IndexOf('/', i);
				if (num == -1)
				{
					string text = url.Substring(i);
					if (text.Length > 0)
					{
						list.Add(text);
						break;
					}
					break;
				}
				else
				{
					string text2 = url.Substring(i, num - i);
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

		// Token: 0x06001368 RID: 4968 RVA: 0x00038584 File Offset: 0x00036784
		private static Exception ValidateUrlParts(IList<string> pathSegments)
		{
			HashSet<string> usedParameterNames = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
			bool? flag = null;
			bool flag2 = false;
			foreach (string text in pathSegments)
			{
				if (flag2)
				{
					return new ArgumentException(string.Format(CultureInfo.CurrentCulture, SR.GetString("Route_CatchAllMustBeLast"), new object[0]), "routeUrl");
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
						return new ArgumentException(SR.GetString("Route_CannotHaveConsecutiveSeparators"), "routeUrl");
					}
					flag = new bool?(flag3);
				}
				if (!flag3)
				{
					Exception ex;
					IList<PathSubsegment> list = RouteParser.ParseUrlSegment(text, out ex);
					if (ex != null)
					{
						return ex;
					}
					ex = RouteParser.ValidateUrlSegment(list, usedParameterNames, text);
					if (ex != null)
					{
						return ex;
					}
					flag2 = list.Any((PathSubsegment seg) => seg is ParameterSubsegment && ((ParameterSubsegment)seg).IsCatchAll);
				}
			}
			return null;
		}

		// Token: 0x06001369 RID: 4969 RVA: 0x000386C0 File Offset: 0x000368C0
		private static Exception ValidateUrlSegment(IList<PathSubsegment> pathSubsegments, HashSet<string> usedParameterNames, string pathSegment)
		{
			bool flag = false;
			Type left = null;
			foreach (PathSubsegment pathSubsegment in pathSubsegments)
			{
				if (left != null && left == pathSubsegment.GetType())
				{
					return new ArgumentException(string.Format(CultureInfo.CurrentCulture, SR.GetString("Route_CannotHaveConsecutiveParameters"), new object[0]), "routeUrl");
				}
				left = pathSubsegment.GetType();
				if (!(pathSubsegment is LiteralSubsegment))
				{
					ParameterSubsegment parameterSubsegment = pathSubsegment as ParameterSubsegment;
					if (parameterSubsegment != null)
					{
						string parameterName = parameterSubsegment.ParameterName;
						if (parameterSubsegment.IsCatchAll)
						{
							flag = true;
						}
						if (!RouteParser.IsValidParameterName(parameterName))
						{
							return new ArgumentException(string.Format(CultureInfo.CurrentUICulture, SR.GetString("Route_InvalidParameterName"), new object[]
							{
								parameterName
							}), "routeUrl");
						}
						if (usedParameterNames.Contains(parameterName))
						{
							return new ArgumentException(string.Format(CultureInfo.CurrentUICulture, SR.GetString("Route_RepeatedParameter"), new object[]
							{
								parameterName
							}), "routeUrl");
						}
						usedParameterNames.Add(parameterName);
					}
				}
			}
			if (flag && pathSubsegments.Count != 1)
			{
				return new ArgumentException(string.Format(CultureInfo.CurrentCulture, SR.GetString("Route_CannotHaveCatchAllInMultiSegment"), new object[0]), "routeUrl");
			}
			return null;
		}
	}
}
