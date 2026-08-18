using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using NLog.Common;
using NLog.Conditions;
using NLog.Config;
using NLog.Internal;
using NLog.LayoutRenderers;
using NLog.LayoutRenderers.Wrappers;

namespace NLog.Layouts
{
	// Token: 0x02000118 RID: 280
	internal sealed class LayoutParser
	{
		// Token: 0x060007BE RID: 1982 RVA: 0x00010EF8 File Offset: 0x0000F0F8
		internal static LayoutRenderer[] CompileLayout(ConfigurationItemFactory configurationItemFactory, SimpleStringReader sr, bool isNested, out string text)
		{
			List<LayoutRenderer> list = new List<LayoutRenderer>();
			StringBuilder stringBuilder = new StringBuilder();
			int position = sr.Position;
			int num;
			while ((num = sr.Peek()) != -1)
			{
				if (isNested)
				{
					if (num == 92)
					{
						sr.Read();
						int num2 = sr.Peek();
						if (num2 == 125 || num2 == 58)
						{
							sr.Read();
							stringBuilder.Append((char)num2);
							continue;
						}
						stringBuilder.Append('\\');
						continue;
					}
					else if (num == 125 || num == 58)
					{
						break;
					}
				}
				sr.Read();
				if (num == 36 && sr.Peek() == 123)
				{
					if (stringBuilder.Length > 0)
					{
						list.Add(new LiteralLayoutRenderer(stringBuilder.ToString()));
						stringBuilder.Length = 0;
					}
					LayoutRenderer layoutRenderer = LayoutParser.ParseLayoutRenderer(configurationItemFactory, sr);
					if (LayoutParser.CanBeConvertedToLiteral(layoutRenderer))
					{
						layoutRenderer = LayoutParser.ConvertToLiteral(layoutRenderer);
					}
					list.Add(layoutRenderer);
				}
				else
				{
					stringBuilder.Append((char)num);
				}
			}
			if (stringBuilder.Length > 0)
			{
				list.Add(new LiteralLayoutRenderer(stringBuilder.ToString()));
				stringBuilder.Length = 0;
			}
			int position2 = sr.Position;
			LayoutParser.MergeLiterals(list);
			text = sr.Substring(position, position2);
			return list.ToArray();
		}

		// Token: 0x060007BF RID: 1983 RVA: 0x0001101C File Offset: 0x0000F21C
		private static string ParseLayoutRendererName(SimpleStringReader sr)
		{
			StringBuilder stringBuilder = new StringBuilder();
			int num;
			while ((num = sr.Peek()) != -1 && num != 58 && num != 125)
			{
				stringBuilder.Append((char)num);
				sr.Read();
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060007C0 RID: 1984 RVA: 0x0001105C File Offset: 0x0000F25C
		private static string ParseParameterName(SimpleStringReader sr)
		{
			int num = 0;
			StringBuilder stringBuilder = new StringBuilder();
			int num2;
			while ((num2 = sr.Peek()) != -1 && ((num2 != 61 && num2 != 125 && num2 != 58) || num != 0))
			{
				if (num2 == 36)
				{
					sr.Read();
					stringBuilder.Append('$');
					if (sr.Peek() == 123)
					{
						stringBuilder.Append('{');
						num++;
						sr.Read();
					}
				}
				else
				{
					if (num2 == 125)
					{
						num--;
					}
					if (num2 == 92)
					{
						sr.Read();
						stringBuilder.Append((char)sr.Read());
					}
					else
					{
						stringBuilder.Append((char)num2);
						sr.Read();
					}
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060007C1 RID: 1985 RVA: 0x00011104 File Offset: 0x0000F304
		private static string ParseParameterValue(SimpleStringReader sr)
		{
			StringBuilder stringBuilder = new StringBuilder();
			int num;
			while ((num = sr.Peek()) != -1 && num != 58 && num != 125)
			{
				if (num == 92)
				{
					sr.Read();
					char c = (char)sr.Peek();
					char c2 = c;
					if (c2 <= 'U')
					{
						if (c2 <= '\'')
						{
							if (c2 != '"')
							{
								if (c2 == '\'')
								{
									sr.Read();
									stringBuilder.Append('\'');
								}
							}
							else
							{
								sr.Read();
								stringBuilder.Append('"');
							}
						}
						else if (c2 != '0')
						{
							if (c2 != ':')
							{
								if (c2 == 'U')
								{
									sr.Read();
									char unicode = LayoutParser.GetUnicode(sr, 8);
									stringBuilder.Append(unicode);
								}
							}
							else
							{
								sr.Read();
								stringBuilder.Append(':');
							}
						}
						else
						{
							sr.Read();
							stringBuilder.Append('\0');
						}
					}
					else if (c2 <= 'b')
					{
						if (c2 != '\\')
						{
							switch (c2)
							{
							case 'a':
								sr.Read();
								stringBuilder.Append('\a');
								break;
							case 'b':
								sr.Read();
								stringBuilder.Append('\b');
								break;
							}
						}
						else
						{
							sr.Read();
							stringBuilder.Append('\\');
						}
					}
					else if (c2 != 'f')
					{
						switch (c2)
						{
						case 'n':
							sr.Read();
							stringBuilder.Append('\n');
							break;
						case 'o':
						case 'p':
						case 'q':
						case 's':
						case 'w':
							break;
						case 'r':
							sr.Read();
							stringBuilder.Append('\r');
							break;
						case 't':
							sr.Read();
							stringBuilder.Append('\t');
							break;
						case 'u':
						{
							sr.Read();
							char unicode2 = LayoutParser.GetUnicode(sr, 4);
							stringBuilder.Append(unicode2);
							break;
						}
						case 'v':
							sr.Read();
							stringBuilder.Append('\v');
							break;
						case 'x':
						{
							sr.Read();
							char unicode3 = LayoutParser.GetUnicode(sr, 4);
							stringBuilder.Append(unicode3);
							break;
						}
						default:
							switch (c2)
							{
							case '{':
								sr.Read();
								stringBuilder.Append('{');
								break;
							case '}':
								sr.Read();
								stringBuilder.Append('}');
								break;
							}
							break;
						}
					}
					else
					{
						sr.Read();
						stringBuilder.Append('\f');
					}
				}
				else
				{
					stringBuilder.Append((char)num);
					sr.Read();
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060007C2 RID: 1986 RVA: 0x0001139C File Offset: 0x0000F59C
		private static char GetUnicode(SimpleStringReader sr, int maxDigits)
		{
			int num = 0;
			for (int i = 0; i < maxDigits; i++)
			{
				int num2 = sr.Peek();
				if (num2 >= 48 && num2 <= 57)
				{
					num2 -= 48;
				}
				else if (num2 >= 97 && num2 <= 102)
				{
					num2 = num2 - 97 + 10;
				}
				else
				{
					if (num2 < 65 || num2 > 70)
					{
						break;
					}
					num2 = num2 - 65 + 10;
				}
				sr.Read();
				num = num * 16 + num2;
			}
			return (char)num;
		}

		// Token: 0x060007C3 RID: 1987 RVA: 0x00011408 File Offset: 0x0000F608
		private static LayoutRenderer ParseLayoutRenderer(ConfigurationItemFactory configurationItemFactory, SimpleStringReader sr)
		{
			int num = sr.Read();
			string itemName = LayoutParser.ParseLayoutRendererName(sr);
			LayoutRenderer layoutRenderer = configurationItemFactory.LayoutRenderers.CreateInstance(itemName);
			Dictionary<Type, LayoutRenderer> dictionary = new Dictionary<Type, LayoutRenderer>();
			List<LayoutRenderer> list = new List<LayoutRenderer>();
			num = sr.Read();
			while (num != -1 && num != 125)
			{
				string text = LayoutParser.ParseParameterName(sr).Trim();
				PropertyInfo propertyInfo2;
				if (sr.Peek() == 61)
				{
					sr.Read();
					LayoutRenderer obj = layoutRenderer;
					PropertyInfo propertyInfo;
					Type key;
					if (!PropertyHelper.TryGetPropertyInfo(layoutRenderer, text, out propertyInfo) && configurationItemFactory.AmbientProperties.TryGetDefinition(text, out key))
					{
						LayoutRenderer layoutRenderer2;
						if (!dictionary.TryGetValue(key, out layoutRenderer2))
						{
							layoutRenderer2 = configurationItemFactory.AmbientProperties.CreateInstance(text);
							dictionary[key] = layoutRenderer2;
							list.Add(layoutRenderer2);
						}
						if (!PropertyHelper.TryGetPropertyInfo(layoutRenderer2, text, out propertyInfo))
						{
							propertyInfo = null;
						}
						else
						{
							obj = layoutRenderer2;
						}
					}
					if (propertyInfo == null)
					{
						LayoutParser.ParseParameterValue(sr);
					}
					else if (typeof(Layout).IsAssignableFrom(propertyInfo.PropertyType))
					{
						SimpleLayout simpleLayout = new SimpleLayout();
						string text2;
						LayoutRenderer[] renderers = LayoutParser.CompileLayout(configurationItemFactory, sr, true, out text2);
						simpleLayout.SetRenderers(renderers, text2);
						propertyInfo.SetValue(obj, simpleLayout, null);
					}
					else if (typeof(ConditionExpression).IsAssignableFrom(propertyInfo.PropertyType))
					{
						ConditionExpression value = ConditionParser.ParseExpression(sr, configurationItemFactory);
						propertyInfo.SetValue(obj, value, null);
					}
					else
					{
						string value2 = LayoutParser.ParseParameterValue(sr);
						PropertyHelper.SetPropertyFromString(obj, text, value2, configurationItemFactory);
					}
				}
				else if (PropertyHelper.TryGetPropertyInfo(layoutRenderer, string.Empty, out propertyInfo2))
				{
					if (typeof(SimpleLayout) == propertyInfo2.PropertyType)
					{
						propertyInfo2.SetValue(layoutRenderer, new SimpleLayout(text), null);
					}
					else
					{
						string value3 = text;
						PropertyHelper.SetPropertyFromString(layoutRenderer, propertyInfo2.Name, value3, configurationItemFactory);
					}
				}
				else
				{
					InternalLogger.Warn("{0} has no default property", new object[]
					{
						layoutRenderer.GetType().FullName
					});
				}
				num = sr.Read();
			}
			return LayoutParser.ApplyWrappers(configurationItemFactory, layoutRenderer, list);
		}

		// Token: 0x060007C4 RID: 1988 RVA: 0x00011604 File Offset: 0x0000F804
		private static LayoutRenderer ApplyWrappers(ConfigurationItemFactory configurationItemFactory, LayoutRenderer lr, List<LayoutRenderer> orderedWrappers)
		{
			for (int i = orderedWrappers.Count - 1; i >= 0; i--)
			{
				WrapperLayoutRendererBase wrapperLayoutRendererBase = (WrapperLayoutRendererBase)orderedWrappers[i];
				InternalLogger.Trace("Wrapping {0} with {1}", new object[]
				{
					lr.GetType().Name,
					wrapperLayoutRendererBase.GetType().Name
				});
				if (LayoutParser.CanBeConvertedToLiteral(lr))
				{
					lr = LayoutParser.ConvertToLiteral(lr);
				}
				wrapperLayoutRendererBase.Inner = new SimpleLayout(new LayoutRenderer[]
				{
					lr
				}, string.Empty, configurationItemFactory);
				lr = wrapperLayoutRendererBase;
			}
			return lr;
		}

		// Token: 0x060007C5 RID: 1989 RVA: 0x00011690 File Offset: 0x0000F890
		private static bool CanBeConvertedToLiteral(LayoutRenderer lr)
		{
			foreach (IRenderable renderable in ObjectGraphScanner.FindReachableObjects<IRenderable>(new object[]
			{
				lr
			}))
			{
				if (!(renderable.GetType() == typeof(SimpleLayout)) && !renderable.GetType().IsDefined(typeof(AppDomainFixedOutputAttribute), false))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060007C6 RID: 1990 RVA: 0x00011720 File Offset: 0x0000F920
		private static void MergeLiterals(List<LayoutRenderer> list)
		{
			int num = 0;
			while (num + 1 < list.Count)
			{
				LiteralLayoutRenderer literalLayoutRenderer = list[num] as LiteralLayoutRenderer;
				LiteralLayoutRenderer literalLayoutRenderer2 = list[num + 1] as LiteralLayoutRenderer;
				if (literalLayoutRenderer != null && literalLayoutRenderer2 != null)
				{
					LiteralLayoutRenderer literalLayoutRenderer3 = literalLayoutRenderer;
					literalLayoutRenderer3.Text += literalLayoutRenderer2.Text;
					list.RemoveAt(num + 1);
				}
				else
				{
					num++;
				}
			}
		}

		// Token: 0x060007C7 RID: 1991 RVA: 0x00011784 File Offset: 0x0000F984
		private static LayoutRenderer ConvertToLiteral(LayoutRenderer renderer)
		{
			return new LiteralLayoutRenderer(renderer.Render(LogEventInfo.CreateNullEvent()));
		}
	}
}
