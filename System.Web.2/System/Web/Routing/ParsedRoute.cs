using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace System.Web.Routing
{
	// Token: 0x02000144 RID: 324
	internal sealed class ParsedRoute
	{
		// Token: 0x06001309 RID: 4873 RVA: 0x00036A2C File Offset: 0x00034C2C
		public ParsedRoute(IList<PathSegment> pathSegments)
		{
			this.PathSegments = pathSegments;
		}

		// Token: 0x170005CE RID: 1486
		// (get) Token: 0x0600130A RID: 4874 RVA: 0x00036A3B File Offset: 0x00034C3B
		// (set) Token: 0x0600130B RID: 4875 RVA: 0x00036A43 File Offset: 0x00034C43
		private IList<PathSegment> PathSegments { get; set; }

		// Token: 0x0600130C RID: 4876 RVA: 0x00036A4C File Offset: 0x00034C4C
		public BoundUrl Bind(RouteValueDictionary currentValues, RouteValueDictionary values, RouteValueDictionary defaultValues, RouteValueDictionary constraints)
		{
			if (currentValues == null)
			{
				currentValues = new RouteValueDictionary();
			}
			if (values == null)
			{
				values = new RouteValueDictionary();
			}
			if (defaultValues == null)
			{
				defaultValues = new RouteValueDictionary();
			}
			RouteValueDictionary acceptedValues = new RouteValueDictionary();
			HashSet<string> unusedNewValues = new HashSet<string>(values.Keys, StringComparer.OrdinalIgnoreCase);
			ParsedRoute.ForEachParameter(this.PathSegments, delegate(ParameterSubsegment parameterSubsegment)
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
				if (flag6 && flag7 && !ParsedRoute.RoutePartsEqual(obj3, obj2))
				{
					return false;
				}
				if (flag6)
				{
					if (ParsedRoute.IsRoutePartNonEmpty(obj2))
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
				if (ParsedRoute.IsRoutePartNonEmpty(keyValuePair.Value) && !acceptedValues.ContainsKey(keyValuePair.Key))
				{
					acceptedValues.Add(keyValuePair.Key, keyValuePair.Value);
				}
			}
			foreach (KeyValuePair<string, object> keyValuePair2 in currentValues)
			{
				string key = keyValuePair2.Key;
				if (!acceptedValues.ContainsKey(key) && ParsedRoute.GetParameterSubsegment(this.PathSegments, key) == null)
				{
					acceptedValues.Add(key, keyValuePair2.Value);
				}
			}
			ParsedRoute.ForEachParameter(this.PathSegments, delegate(ParameterSubsegment parameterSubsegment)
			{
				object value2;
				if (!acceptedValues.ContainsKey(parameterSubsegment.ParameterName) && !ParsedRoute.IsParameterRequired(parameterSubsegment, defaultValues, out value2))
				{
					acceptedValues.Add(parameterSubsegment.ParameterName, value2);
				}
				return true;
			});
			if (!ParsedRoute.ForEachParameter(this.PathSegments, delegate(ParameterSubsegment parameterSubsegment)
			{
				object obj2;
				return !ParsedRoute.IsParameterRequired(parameterSubsegment, defaultValues, out obj2) || acceptedValues.ContainsKey(parameterSubsegment.ParameterName);
			}))
			{
				return null;
			}
			RouteValueDictionary otherDefaultValues = new RouteValueDictionary(defaultValues);
			ParsedRoute.ForEachParameter(this.PathSegments, delegate(ParameterSubsegment parameterSubsegment)
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
					if (!ParsedRoute.RoutePartsEqual(a, keyValuePair3.Value))
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
				if (pathSegment is SeparatorPathSegment)
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
					ContentPathSegment contentPathSegment = pathSegment as ContentPathSegment;
					if (contentPathSegment != null)
					{
						bool flag3 = false;
						foreach (PathSubsegment pathSubsegment in contentPathSegment.Subsegments)
						{
							LiteralSubsegment literalSubsegment = pathSubsegment as LiteralSubsegment;
							if (literalSubsegment != null)
							{
								flag = true;
								stringBuilder2.Append(ParsedRoute.UrlEncode(literalSubsegment.Literal));
							}
							else
							{
								ParameterSubsegment parameterSubsegment2 = pathSubsegment as ParameterSubsegment;
								if (parameterSubsegment2 != null)
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
									bool flag4 = acceptedValues.TryGetValue(parameterSubsegment2.ParameterName, out obj);
									if (flag4)
									{
										unusedNewValues.Remove(parameterSubsegment2.ParameterName);
									}
									object b;
									defaultValues.TryGetValue(parameterSubsegment2.ParameterName, out b);
									if (ParsedRoute.RoutePartsEqual(obj, b))
									{
										stringBuilder2.Append(ParsedRoute.UrlEncode(Convert.ToString(obj, CultureInfo.InvariantCulture)));
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
										stringBuilder.Append(ParsedRoute.UrlEncode(Convert.ToString(obj, CultureInfo.InvariantCulture)));
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
			return new BoundUrl
			{
				Url = stringBuilder.ToString(),
				Values = acceptedValues
			};
		}

		// Token: 0x0600130D RID: 4877 RVA: 0x0003709C File Offset: 0x0003529C
		private static string EscapeReservedCharacters(Match m)
		{
			return "%" + Convert.ToUInt16(m.Value[0]).ToString("x2", CultureInfo.InvariantCulture);
		}

		// Token: 0x0600130E RID: 4878 RVA: 0x000370D8 File Offset: 0x000352D8
		private static bool ForEachParameter(IList<PathSegment> pathSegments, Func<ParameterSubsegment, bool> action)
		{
			for (int i = 0; i < pathSegments.Count; i++)
			{
				PathSegment pathSegment = pathSegments[i];
				if (!(pathSegment is SeparatorPathSegment))
				{
					ContentPathSegment contentPathSegment = pathSegment as ContentPathSegment;
					if (contentPathSegment != null)
					{
						foreach (PathSubsegment pathSubsegment in contentPathSegment.Subsegments)
						{
							if (!(pathSubsegment is LiteralSubsegment))
							{
								ParameterSubsegment parameterSubsegment = pathSubsegment as ParameterSubsegment;
								if (parameterSubsegment != null && !action(parameterSubsegment))
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

		// Token: 0x0600130F RID: 4879 RVA: 0x00037178 File Offset: 0x00035378
		private static ParameterSubsegment GetParameterSubsegment(IList<PathSegment> pathSegments, string parameterName)
		{
			ParameterSubsegment foundParameterSubsegment = null;
			bool flag = ParsedRoute.ForEachParameter(pathSegments, delegate(ParameterSubsegment parameterSubsegment)
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

		// Token: 0x06001310 RID: 4880 RVA: 0x000371B2 File Offset: 0x000353B2
		private static bool IsParameterRequired(ParameterSubsegment parameterSubsegment, RouteValueDictionary defaultValues, out object defaultValue)
		{
			if (parameterSubsegment.IsCatchAll)
			{
				defaultValue = null;
				return false;
			}
			return !defaultValues.TryGetValue(parameterSubsegment.ParameterName, out defaultValue);
		}

		// Token: 0x06001311 RID: 4881 RVA: 0x000371D4 File Offset: 0x000353D4
		private static bool IsRoutePartNonEmpty(object routePart)
		{
			string text = routePart as string;
			if (text != null)
			{
				return text.Length > 0;
			}
			return routePart != null;
		}

		// Token: 0x06001312 RID: 4882 RVA: 0x000371FC File Offset: 0x000353FC
		public RouteValueDictionary Match(string virtualPath, RouteValueDictionary defaultValues)
		{
			IList<string> list = RouteParser.SplitUrlToPathSegmentStrings(virtualPath);
			if (defaultValues == null)
			{
				defaultValues = new RouteValueDictionary();
			}
			RouteValueDictionary routeValueDictionary = new RouteValueDictionary();
			bool flag = false;
			bool flag2 = false;
			for (int i = 0; i < this.PathSegments.Count; i++)
			{
				PathSegment pathSegment = this.PathSegments[i];
				if (list.Count <= i)
				{
					flag = true;
				}
				string text = flag ? null : list[i];
				if (pathSegment is SeparatorPathSegment)
				{
					if (!flag && !string.Equals(text, "/", StringComparison.Ordinal))
					{
						return null;
					}
				}
				else
				{
					ContentPathSegment contentPathSegment = pathSegment as ContentPathSegment;
					if (contentPathSegment != null)
					{
						if (contentPathSegment.IsCatchAll)
						{
							this.MatchCatchAll(contentPathSegment, list.Skip(i), defaultValues, routeValueDictionary);
							flag2 = true;
						}
						else if (!this.MatchContentPathSegment(contentPathSegment, text, defaultValues, routeValueDictionary))
						{
							return null;
						}
					}
				}
			}
			if (!flag2 && this.PathSegments.Count < list.Count)
			{
				for (int j = this.PathSegments.Count; j < list.Count; j++)
				{
					if (!RouteParser.IsSeparator(list[j]))
					{
						return null;
					}
				}
			}
			if (defaultValues != null)
			{
				foreach (KeyValuePair<string, object> keyValuePair in defaultValues)
				{
					if (!routeValueDictionary.ContainsKey(keyValuePair.Key))
					{
						routeValueDictionary.Add(keyValuePair.Key, keyValuePair.Value);
					}
				}
			}
			return routeValueDictionary;
		}

		// Token: 0x06001313 RID: 4883 RVA: 0x00037374 File Offset: 0x00035574
		private void MatchCatchAll(ContentPathSegment contentPathSegment, IEnumerable<string> remainingRequestSegments, RouteValueDictionary defaultValues, RouteValueDictionary matchedValues)
		{
			string text = string.Join(string.Empty, remainingRequestSegments.ToArray<string>());
			ParameterSubsegment parameterSubsegment = contentPathSegment.Subsegments[0] as ParameterSubsegment;
			object value;
			if (text.Length > 0)
			{
				value = text;
			}
			else
			{
				defaultValues.TryGetValue(parameterSubsegment.ParameterName, out value);
			}
			matchedValues.Add(parameterSubsegment.ParameterName, value);
		}

		// Token: 0x06001314 RID: 4884 RVA: 0x000373D0 File Offset: 0x000355D0
		private bool MatchContentPathSegment(ContentPathSegment routeSegment, string requestPathSegment, RouteValueDictionary defaultValues, RouteValueDictionary matchedValues)
		{
			if (!string.IsNullOrEmpty(requestPathSegment))
			{
				int num = requestPathSegment.Length;
				int i = routeSegment.Subsegments.Count - 1;
				ParameterSubsegment parameterSubsegment = null;
				LiteralSubsegment literalSubsegment = null;
				while (i >= 0)
				{
					int num2 = num;
					ParameterSubsegment parameterSubsegment2 = routeSegment.Subsegments[i] as ParameterSubsegment;
					if (parameterSubsegment2 != null)
					{
						parameterSubsegment = parameterSubsegment2;
					}
					else
					{
						LiteralSubsegment literalSubsegment2 = routeSegment.Subsegments[i] as LiteralSubsegment;
						if (literalSubsegment2 != null)
						{
							literalSubsegment = literalSubsegment2;
							int num3 = num - 1;
							if (parameterSubsegment != null)
							{
								num3--;
							}
							if (num3 < 0)
							{
								return false;
							}
							int num4 = requestPathSegment.LastIndexOf(literalSubsegment2.Literal, num3, StringComparison.OrdinalIgnoreCase);
							if (num4 == -1)
							{
								return false;
							}
							if (i == routeSegment.Subsegments.Count - 1 && num4 + literalSubsegment2.Literal.Length != requestPathSegment.Length)
							{
								return false;
							}
							num2 = num4;
						}
					}
					if (parameterSubsegment != null && ((literalSubsegment != null && parameterSubsegment2 == null) || i == 0))
					{
						int num5;
						int length;
						if (literalSubsegment == null)
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
						else if (i == 0 && parameterSubsegment2 != null)
						{
							num5 = 0;
							length = num;
						}
						else
						{
							num5 = num2 + literalSubsegment.Literal.Length;
							length = num - num5;
						}
						string value = requestPathSegment.Substring(num5, length);
						if (string.IsNullOrEmpty(value))
						{
							return false;
						}
						matchedValues.Add(parameterSubsegment.ParameterName, value);
						parameterSubsegment = null;
						literalSubsegment = null;
					}
					num = num2;
					i--;
				}
				return num == 0 || routeSegment.Subsegments[0] is ParameterSubsegment;
			}
			if (routeSegment.Subsegments.Count > 1)
			{
				return false;
			}
			ParameterSubsegment parameterSubsegment3 = routeSegment.Subsegments[0] as ParameterSubsegment;
			if (parameterSubsegment3 == null)
			{
				return false;
			}
			object value2;
			if (defaultValues.TryGetValue(parameterSubsegment3.ParameterName, out value2))
			{
				matchedValues.Add(parameterSubsegment3.ParameterName, value2);
				return true;
			}
			return false;
		}

		// Token: 0x06001315 RID: 4885 RVA: 0x0003757C File Offset: 0x0003577C
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

		// Token: 0x06001316 RID: 4886 RVA: 0x000375B8 File Offset: 0x000357B8
		private static string UrlEncode(string str)
		{
			string input = Uri.EscapeUriString(str);
			return Regex.Replace(input, "([#;?:@&=+$,])", new MatchEvaluator(ParsedRoute.EscapeReservedCharacters));
		}
	}
}
