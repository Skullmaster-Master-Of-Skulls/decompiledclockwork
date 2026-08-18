using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace System.Web.Http.Routing
{
	// Token: 0x02000106 RID: 262
	internal sealed class HttpParsedRoute
	{
		// Token: 0x06000660 RID: 1632 RVA: 0x00014E3B File Offset: 0x0001303B
		public HttpParsedRoute(List<PathSegment> pathSegments)
		{
			this.PathSegments = pathSegments;
		}

		// Token: 0x17000215 RID: 533
		// (get) Token: 0x06000661 RID: 1633 RVA: 0x00014E4A File Offset: 0x0001304A
		// (set) Token: 0x06000662 RID: 1634 RVA: 0x00014E52 File Offset: 0x00013052
		public List<PathSegment> PathSegments { get; private set; }

		// Token: 0x06000663 RID: 1635 RVA: 0x00014F74 File Offset: 0x00013174
		public BoundRouteTemplate Bind(IDictionary<string, object> currentValues, IDictionary<string, object> values, HttpRouteValueDictionary defaultValues, HttpRouteValueDictionary constraints)
		{
			if (currentValues == null)
			{
				currentValues = new HttpRouteValueDictionary();
			}
			if (values == null)
			{
				values = new HttpRouteValueDictionary();
			}
			if (defaultValues == null)
			{
				defaultValues = new HttpRouteValueDictionary();
			}
			HttpRouteValueDictionary acceptedValues = new HttpRouteValueDictionary();
			HashSet<string> unusedNewValues = new HashSet<string>(values.Keys, StringComparer.OrdinalIgnoreCase);
			HttpParsedRoute.ForEachParameter(this.PathSegments, delegate(PathParameterSubsegment parameterSubsegment)
			{
				string parameterName = parameterSubsegment.ParameterName;
				object obj2;
				bool flag6 = values.TryGetValue(parameterName, out obj2);
				if (flag6)
				{
					unusedNewValues.Remove(parameterName);
				}
				object obj3;
				bool flag7 = currentValues.TryGetValue(parameterName, out obj3);
				if (flag6 && flag7 && !HttpParsedRoute.RoutePartsEqual(obj3, obj2))
				{
					return false;
				}
				if (flag6)
				{
					if (HttpParsedRoute.IsRoutePartNonEmpty(obj2))
					{
						acceptedValues.Add(parameterName, obj2);
					}
				}
				else if (flag7)
				{
					acceptedValues.Add(parameterName, obj3);
				}
				return true;
			});
			foreach (KeyValuePair<string, object> keyValuePair in values)
			{
				if (HttpParsedRoute.IsRoutePartNonEmpty(keyValuePair.Value) && !acceptedValues.ContainsKey(keyValuePair.Key))
				{
					acceptedValues.Add(keyValuePair.Key, keyValuePair.Value);
				}
			}
			foreach (KeyValuePair<string, object> keyValuePair2 in currentValues)
			{
				string key = keyValuePair2.Key;
				if (!acceptedValues.ContainsKey(key) && HttpParsedRoute.GetParameterSubsegment(this.PathSegments, key) == null)
				{
					acceptedValues.Add(key, keyValuePair2.Value);
				}
			}
			HttpParsedRoute.ForEachParameter(this.PathSegments, delegate(PathParameterSubsegment parameterSubsegment)
			{
				object value2;
				if (!acceptedValues.ContainsKey(parameterSubsegment.ParameterName) && !HttpParsedRoute.IsParameterRequired(parameterSubsegment, defaultValues, out value2))
				{
					acceptedValues.Add(parameterSubsegment.ParameterName, value2);
				}
				return true;
			});
			if (!HttpParsedRoute.ForEachParameter(this.PathSegments, delegate(PathParameterSubsegment parameterSubsegment)
			{
				object obj2;
				return !HttpParsedRoute.IsParameterRequired(parameterSubsegment, defaultValues, out obj2) || acceptedValues.ContainsKey(parameterSubsegment.ParameterName);
			}))
			{
				return null;
			}
			HttpRouteValueDictionary otherDefaultValues = new HttpRouteValueDictionary(defaultValues);
			HttpParsedRoute.ForEachParameter(this.PathSegments, delegate(PathParameterSubsegment parameterSubsegment)
			{
				otherDefaultValues.Remove(parameterSubsegment.ParameterName);
				return true;
			});
			foreach (KeyValuePair<string, object> keyValuePair3 in otherDefaultValues)
			{
				object a;
				if (values.TryGetValue(keyValuePair3.Key, out a))
				{
					unusedNewValues.Remove(keyValuePair3.Key);
					if (!HttpParsedRoute.RoutePartsEqual(a, keyValuePair3.Value))
					{
						return null;
					}
				}
			}
			StringBuilder stringBuilder = new StringBuilder();
			StringBuilder stringBuilder2 = new StringBuilder();
			bool flag = false;
			bool flag2 = false;
			for (int i = 0; i < this.PathSegments.Count; i++)
			{
				PathSegment pathSegment = this.PathSegments[i];
				if (pathSegment is PathSeparatorSegment)
				{
					if (flag && stringBuilder2.Length > 0)
					{
						if (flag2)
						{
							return null;
						}
						stringBuilder.Append(stringBuilder2.ToString());
						stringBuilder2.Length = 0;
					}
					flag = false;
					if (stringBuilder2.Length > 0 && stringBuilder2[stringBuilder2.Length - 1] == '/')
					{
						if (flag2)
						{
							return null;
						}
						stringBuilder.Append(stringBuilder2.ToString(0, stringBuilder2.Length - 1));
						stringBuilder2.Length = 0;
						flag2 = true;
					}
					else
					{
						stringBuilder2.Append("/");
					}
				}
				else
				{
					PathContentSegment pathContentSegment = pathSegment as PathContentSegment;
					if (pathContentSegment != null)
					{
						bool flag3 = false;
						for (int j = 0; j < pathContentSegment.Subsegments.Count; j++)
						{
							PathSubsegment pathSubsegment = pathContentSegment.Subsegments[j];
							PathLiteralSubsegment pathLiteralSubsegment = pathSubsegment as PathLiteralSubsegment;
							if (pathLiteralSubsegment != null)
							{
								flag = true;
								stringBuilder2.Append(pathLiteralSubsegment.Literal);
							}
							else
							{
								PathParameterSubsegment pathParameterSubsegment = pathSubsegment as PathParameterSubsegment;
								if (pathParameterSubsegment != null)
								{
									if (flag && stringBuilder2.Length > 0)
									{
										if (flag2)
										{
											return null;
										}
										stringBuilder.Append(stringBuilder2.ToString());
										stringBuilder2.Length = 0;
										flag3 = true;
									}
									flag = false;
									object obj;
									bool flag4 = acceptedValues.TryGetValue(pathParameterSubsegment.ParameterName, out obj);
									if (flag4)
									{
										unusedNewValues.Remove(pathParameterSubsegment.ParameterName);
									}
									object b;
									defaultValues.TryGetValue(pathParameterSubsegment.ParameterName, out b);
									if (HttpParsedRoute.RoutePartsEqual(obj, b))
									{
										stringBuilder2.Append(Convert.ToString(obj, CultureInfo.InvariantCulture));
									}
									else
									{
										if (flag2)
										{
											return null;
										}
										if (stringBuilder2.Length > 0)
										{
											stringBuilder.Append(stringBuilder2.ToString());
											stringBuilder2.Length = 0;
										}
										stringBuilder.Append(Convert.ToString(obj, CultureInfo.InvariantCulture));
										flag3 = true;
									}
								}
							}
						}
						if (flag3 && stringBuilder2.Length > 0)
						{
							if (flag2)
							{
								return null;
							}
							stringBuilder.Append(stringBuilder2.ToString());
							stringBuilder2.Length = 0;
						}
					}
				}
			}
			if (flag && stringBuilder2.Length > 0)
			{
				if (flag2)
				{
					return null;
				}
				stringBuilder.Append(stringBuilder2.ToString());
			}
			if (constraints != null)
			{
				foreach (KeyValuePair<string, object> keyValuePair4 in constraints)
				{
					unusedNewValues.Remove(keyValuePair4.Key);
				}
			}
			StringBuilder stringBuilder3 = new StringBuilder();
			stringBuilder3.Append(HttpParsedRoute.UriEncode(stringBuilder.ToString()));
			stringBuilder = stringBuilder3;
			if (unusedNewValues.Count > 0)
			{
				bool flag5 = true;
				foreach (string text in unusedNewValues)
				{
					object value;
					if (acceptedValues.TryGetValue(text, out value))
					{
						stringBuilder.Append(flag5 ? '?' : '&');
						flag5 = false;
						stringBuilder.Append(Uri.EscapeDataString(text));
						stringBuilder.Append('=');
						stringBuilder.Append(Uri.EscapeDataString(Convert.ToString(value, CultureInfo.InvariantCulture)));
					}
				}
			}
			return new BoundRouteTemplate
			{
				BoundTemplate = stringBuilder.ToString(),
				Values = acceptedValues
			};
		}

		// Token: 0x06000664 RID: 1636 RVA: 0x000155B4 File Offset: 0x000137B4
		private static string EscapeReservedCharacters(Match m)
		{
			return Uri.HexEscape(m.Value[0]);
		}

		// Token: 0x06000665 RID: 1637 RVA: 0x000155C8 File Offset: 0x000137C8
		private static bool ForEachParameter(List<PathSegment> pathSegments, Func<PathParameterSubsegment, bool> action)
		{
			for (int i = 0; i < pathSegments.Count; i++)
			{
				PathSegment pathSegment = pathSegments[i];
				if (!(pathSegment is PathSeparatorSegment))
				{
					PathContentSegment pathContentSegment = pathSegment as PathContentSegment;
					if (pathContentSegment != null)
					{
						for (int j = 0; j < pathContentSegment.Subsegments.Count; j++)
						{
							PathSubsegment pathSubsegment = pathContentSegment.Subsegments[j];
							if (!(pathSubsegment is PathLiteralSubsegment))
							{
								PathParameterSubsegment pathParameterSubsegment = pathSubsegment as PathParameterSubsegment;
								if (pathParameterSubsegment != null && !action(pathParameterSubsegment))
								{
									return false;
								}
							}
						}
					}
				}
			}
			return true;
		}

		// Token: 0x06000666 RID: 1638 RVA: 0x00015674 File Offset: 0x00013874
		private static PathParameterSubsegment GetParameterSubsegment(List<PathSegment> pathSegments, string parameterName)
		{
			PathParameterSubsegment foundParameterSubsegment = null;
			HttpParsedRoute.ForEachParameter(pathSegments, delegate(PathParameterSubsegment parameterSubsegment)
			{
				if (string.Equals(parameterName, parameterSubsegment.ParameterName, StringComparison.OrdinalIgnoreCase))
				{
					foundParameterSubsegment = parameterSubsegment;
					return false;
				}
				return true;
			});
			return foundParameterSubsegment;
		}

		// Token: 0x06000667 RID: 1639 RVA: 0x000156AE File Offset: 0x000138AE
		private static bool IsParameterRequired(PathParameterSubsegment parameterSubsegment, HttpRouteValueDictionary defaultValues, out object defaultValue)
		{
			if (parameterSubsegment.IsCatchAll)
			{
				defaultValue = null;
				return false;
			}
			return !defaultValues.TryGetValue(parameterSubsegment.ParameterName, out defaultValue);
		}

		// Token: 0x06000668 RID: 1640 RVA: 0x000156D0 File Offset: 0x000138D0
		private static bool IsRoutePartNonEmpty(object routePart)
		{
			string text = routePart as string;
			if (text != null)
			{
				return text.Length > 0;
			}
			return routePart != null;
		}

		// Token: 0x06000669 RID: 1641 RVA: 0x000156F8 File Offset: 0x000138F8
		public HttpRouteValueDictionary Match(RoutingContext context, HttpRouteValueDictionary defaultValues)
		{
			List<string> pathSegments = context.PathSegments;
			if (defaultValues == null)
			{
				defaultValues = new HttpRouteValueDictionary();
			}
			HttpRouteValueDictionary httpRouteValueDictionary = new HttpRouteValueDictionary();
			bool flag = false;
			bool flag2 = false;
			for (int i = 0; i < this.PathSegments.Count; i++)
			{
				PathSegment pathSegment = this.PathSegments[i];
				if (pathSegments.Count <= i)
				{
					flag = true;
				}
				string text = flag ? null : pathSegments[i];
				if (pathSegment is PathSeparatorSegment)
				{
					if (!flag && !string.Equals(text, "/", StringComparison.Ordinal))
					{
						return null;
					}
				}
				else
				{
					PathContentSegment pathContentSegment = pathSegment as PathContentSegment;
					if (pathContentSegment != null)
					{
						if (pathContentSegment.IsCatchAll)
						{
							HttpParsedRoute.MatchCatchAll(pathContentSegment, pathSegments.Skip(i), defaultValues, httpRouteValueDictionary);
							flag2 = true;
						}
						else if (!HttpParsedRoute.MatchContentPathSegment(pathContentSegment, text, defaultValues, httpRouteValueDictionary))
						{
							return null;
						}
					}
				}
			}
			if (!flag2 && this.PathSegments.Count < pathSegments.Count)
			{
				for (int j = this.PathSegments.Count; j < pathSegments.Count; j++)
				{
					if (!RouteParser.IsSeparator(pathSegments[j]))
					{
						return null;
					}
				}
			}
			if (defaultValues != null)
			{
				foreach (KeyValuePair<string, object> keyValuePair in defaultValues)
				{
					if (!httpRouteValueDictionary.ContainsKey(keyValuePair.Key))
					{
						httpRouteValueDictionary.Add(keyValuePair.Key, keyValuePair.Value);
					}
				}
			}
			return httpRouteValueDictionary;
		}

		// Token: 0x0600066A RID: 1642 RVA: 0x0001586C File Offset: 0x00013A6C
		private static void MatchCatchAll(PathContentSegment contentPathSegment, IEnumerable<string> remainingRequestSegments, HttpRouteValueDictionary defaultValues, HttpRouteValueDictionary matchedValues)
		{
			string text = string.Join(string.Empty, remainingRequestSegments.ToArray<string>());
			PathParameterSubsegment pathParameterSubsegment = contentPathSegment.Subsegments[0] as PathParameterSubsegment;
			object value;
			if (text.Length > 0)
			{
				value = text;
			}
			else
			{
				defaultValues.TryGetValue(pathParameterSubsegment.ParameterName, out value);
			}
			matchedValues.Add(pathParameterSubsegment.ParameterName, value);
		}

		// Token: 0x0600066B RID: 1643 RVA: 0x000158C8 File Offset: 0x00013AC8
		private static bool MatchContentPathSegment(PathContentSegment routeSegment, string requestPathSegment, HttpRouteValueDictionary defaultValues, HttpRouteValueDictionary matchedValues)
		{
			if (string.IsNullOrEmpty(requestPathSegment))
			{
				if (routeSegment.Subsegments.Count > 1)
				{
					return false;
				}
				PathParameterSubsegment pathParameterSubsegment = routeSegment.Subsegments[0] as PathParameterSubsegment;
				if (pathParameterSubsegment == null)
				{
					return false;
				}
				object value;
				if (defaultValues.TryGetValue(pathParameterSubsegment.ParameterName, out value))
				{
					matchedValues.Add(pathParameterSubsegment.ParameterName, value);
					return true;
				}
				return false;
			}
			else
			{
				if (routeSegment.Subsegments.Count == 1)
				{
					return HttpParsedRoute.MatchSingleContentPathSegment(routeSegment.Subsegments[0], requestPathSegment, matchedValues);
				}
				int num = requestPathSegment.Length;
				int i = routeSegment.Subsegments.Count - 1;
				PathParameterSubsegment pathParameterSubsegment2 = null;
				PathLiteralSubsegment pathLiteralSubsegment = null;
				while (i >= 0)
				{
					int num2 = num;
					PathParameterSubsegment pathParameterSubsegment3 = routeSegment.Subsegments[i] as PathParameterSubsegment;
					if (pathParameterSubsegment3 != null)
					{
						pathParameterSubsegment2 = pathParameterSubsegment3;
					}
					else
					{
						PathLiteralSubsegment pathLiteralSubsegment2 = routeSegment.Subsegments[i] as PathLiteralSubsegment;
						if (pathLiteralSubsegment2 != null)
						{
							pathLiteralSubsegment = pathLiteralSubsegment2;
							int num3 = num - 1;
							if (pathParameterSubsegment2 != null)
							{
								num3--;
							}
							if (num3 < 0)
							{
								return false;
							}
							int num4 = requestPathSegment.LastIndexOf(pathLiteralSubsegment2.Literal, num3, StringComparison.OrdinalIgnoreCase);
							if (num4 == -1)
							{
								return false;
							}
							if (i == routeSegment.Subsegments.Count - 1 && num4 + pathLiteralSubsegment2.Literal.Length != requestPathSegment.Length)
							{
								return false;
							}
							num2 = num4;
						}
					}
					if (pathParameterSubsegment2 != null && ((pathLiteralSubsegment != null && pathParameterSubsegment3 == null) || i == 0))
					{
						int num5;
						int length;
						if (pathLiteralSubsegment == null)
						{
							if (i == 0)
							{
								num5 = 0;
							}
							else
							{
								num5 = num2;
							}
							length = num;
						}
						else if (i == 0 && pathParameterSubsegment3 != null)
						{
							num5 = 0;
							length = num;
						}
						else
						{
							num5 = num2 + pathLiteralSubsegment.Literal.Length;
							length = num - num5;
						}
						string value2 = requestPathSegment.Substring(num5, length);
						if (string.IsNullOrEmpty(value2))
						{
							return false;
						}
						matchedValues.Add(pathParameterSubsegment2.ParameterName, value2);
						pathParameterSubsegment2 = null;
						pathLiteralSubsegment = null;
					}
					num = num2;
					i--;
				}
				return num == 0 || routeSegment.Subsegments[0] is PathParameterSubsegment;
			}
		}

		// Token: 0x0600066C RID: 1644 RVA: 0x00015A9C File Offset: 0x00013C9C
		private static bool MatchSingleContentPathSegment(PathSubsegment pathSubsegment, string requestPathSegment, HttpRouteValueDictionary matchedValues)
		{
			PathParameterSubsegment pathParameterSubsegment = pathSubsegment as PathParameterSubsegment;
			if (pathParameterSubsegment == null)
			{
				PathLiteralSubsegment pathLiteralSubsegment = pathSubsegment as PathLiteralSubsegment;
				return pathLiteralSubsegment.Literal.Equals(requestPathSegment, StringComparison.OrdinalIgnoreCase);
			}
			matchedValues.Add(pathParameterSubsegment.ParameterName, requestPathSegment);
			return true;
		}

		// Token: 0x0600066D RID: 1645 RVA: 0x00015AD8 File Offset: 0x00013CD8
		private static bool RoutePartsEqual(object a, object b)
		{
			string text = a as string;
			string text2 = b as string;
			if (text != null && text2 != null)
			{
				return string.Equals(text, text2, StringComparison.OrdinalIgnoreCase);
			}
			if (a != null && b != null)
			{
				return a.Equals(b);
			}
			return a == b;
		}

		// Token: 0x0600066E RID: 1646 RVA: 0x00015B14 File Offset: 0x00013D14
		private static string UriEncode(string str)
		{
			string input = Uri.EscapeUriString(str);
			return Regex.Replace(input, "([#?])", new MatchEvaluator(HttpParsedRoute.EscapeReservedCharacters));
		}
	}
}
