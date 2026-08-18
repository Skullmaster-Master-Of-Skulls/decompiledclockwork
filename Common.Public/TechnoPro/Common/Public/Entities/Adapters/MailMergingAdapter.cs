using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using TechnoPro.Common.Public.Entities.MailMergeEntities;
using TechnoPro.Common.Public.Entities.MailMergeEntities.MailMergeCodes;

namespace TechnoPro.Common.Public.Entities.Adapters
{
	// Token: 0x020005C3 RID: 1475
	public static class MailMergingAdapter
	{
		// Token: 0x06002F7C RID: 12156 RVA: 0x000363A4 File Offset: 0x000345A4
		public static bool ContainsTrueArg(this IDictionary<string, string> args, string argName)
		{
			bool flag = args == null || string.IsNullOrEmpty(argName) || !args.ContainsKey(argName);
			return !flag && "1yestrue".IndexOf(args[argName] ?? "") >= 0;
		}

		// Token: 0x06002F7D RID: 12157 RVA: 0x000363F8 File Offset: 0x000345F8
		public static eMailMergeCode? FindMailMergeCode(this string code)
		{
			Array values = Enum.GetValues(typeof(eMailMergeCode));
			string value = code.ToLower().Trim();
			foreach (object obj in values)
			{
				eMailMergeCode eMailMergeCode = (eMailMergeCode)obj;
				MailMergeCodeAttribute info = eMailMergeCode.GetInfo();
				bool flag = info.CodeText.Equals(value);
				if (flag)
				{
					return new eMailMergeCode?(eMailMergeCode);
				}
				bool flag2 = info.EquivalentCodeTexts != null;
				if (flag2)
				{
					foreach (string text in info.EquivalentCodeTexts)
					{
						bool flag3 = text.Equals(value);
						if (flag3)
						{
							return new eMailMergeCode?(eMailMergeCode);
						}
					}
				}
			}
			return null;
		}

		// Token: 0x06002F7E RID: 12158 RVA: 0x00036518 File Offset: 0x00034718
		public static MailMergeCodeAttribute GetInfo(this eMailMergeCode MailMergeCode)
		{
			Type type = MailMergeCode.GetType();
			FieldInfo field = type.GetField(MailMergeCode.ToString());
			MailMergeCodeAttribute[] array = field.GetCustomAttributes(typeof(MailMergeCodeAttribute), false) as MailMergeCodeAttribute[];
			return (array != null && array.Length != 0) ? array[0] : null;
		}

		// Token: 0x06002F7F RID: 12159 RVA: 0x00036570 File Offset: 0x00034770
		public static string GetHtmlDisplayString(this eMailMergeCode code)
		{
			MailMergeCodeAttribute info = code.GetInfo();
			string text = code.ToString();
			int num = text.IndexOf("_");
			bool flag = num > 0;
			if (flag)
			{
				text = text.Substring(num + 1);
			}
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendFormat("<b>{0}:</b> <i>{1}</i><br />{2}<br />Example output:<br />{3}", new object[]
			{
				text,
				info.CodeText,
				info.Description,
				info.ExampleOutput.Replace(Environment.NewLine, "<br />")
			});
			bool flag2 = info.EquivalentCodeTexts != null && info.EquivalentCodeTexts.Count > 0;
			if (flag2)
			{
				stringBuilder.AppendFormat("<br />Equivalent codes: {0}", string.Join(", ", info.EquivalentCodeTexts.ToArray<string>()));
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06002F80 RID: 12160 RVA: 0x00036648 File Offset: 0x00034848
		public static MailMergeContext GetMailMergeContextFromString(this string s)
		{
			MailMergeContext mailMergeContext = new MailMergeContext();
			bool flag = string.IsNullOrEmpty(s);
			MailMergeContext result;
			if (flag)
			{
				result = mailMergeContext;
			}
			else
			{
				Type typeFromHandle = typeof(MailMergeContext);
				List<PropertyInfo> source = typeFromHandle.GetProperties().ToList<PropertyInfo>();
				string[] array = s.Split(new string[]
				{
					Environment.NewLine
				}, StringSplitOptions.RemoveEmptyEntries);
				string[] array2 = array;
				int i = 0;
				while (i < array2.Length)
				{
					string text = array2[i];
					int num = text.IndexOf('=');
					bool flag2 = num > 0;
					if (flag2)
					{
						string name = text.Substring(0, num);
						PropertyInfo propertyInfo = source.FirstOrDefault((PropertyInfo g) => g.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
						bool flag3 = propertyInfo == null;
						if (!flag3)
						{
							string text2 = text.Substring(num + 1);
							bool flag4 = propertyInfo.PropertyType == typeof(int);
							if (flag4)
							{
								int num2;
								bool flag5 = int.TryParse(text2, out num2);
								if (flag5)
								{
									propertyInfo.SetValue(mailMergeContext, num2, null);
								}
							}
							else
							{
								bool flag6 = propertyInfo.PropertyType == typeof(List<int>);
								if (flag6)
								{
									List<int> value = (from h in text2.Split(new char[]
									{
										','
									}).Select(delegate(string g)
									{
										int result2;
										int.TryParse(g.Trim(), out result2);
										return result2;
									})
									where h > 0
									select h).Distinct<int>().ToList<int>();
									propertyInfo.SetValue(mailMergeContext, value, null);
								}
							}
						}
					}
					IL_197:
					i++;
					continue;
					goto IL_197;
				}
				result = mailMergeContext;
			}
			return result;
		}
	}
}
