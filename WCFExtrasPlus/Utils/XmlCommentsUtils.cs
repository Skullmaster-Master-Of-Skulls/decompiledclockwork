using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using WCFExtrasPlus.Wsdl.Documentation;

namespace WCFExtrasPlus.Utils
{
	// Token: 0x02000012 RID: 18
	public static class XmlCommentsUtils
	{
		// Token: 0x0600004D RID: 77 RVA: 0x000030F0 File Offset: 0x000012F0
		private static XmlDocument TryLoadFromLocation(string fileName)
		{
			XmlDocument xmlDocument = null;
			if (XmlCommentsUtils.xmlDocCache.TryGetValue(fileName, out xmlDocument))
			{
				return xmlDocument;
			}
			FileInfo fileInfo = new FileInfo(Path.ChangeExtension(fileName, "xml"));
			if (fileInfo.Exists)
			{
				xmlDocument = new XmlDocument();
				xmlDocument.PreserveWhitespace = true;
				xmlDocument.Load(fileInfo.FullName);
			}
			XmlCommentsUtils.xmlDocCache[fileName] = xmlDocument;
			return xmlDocument;
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00003150 File Offset: 0x00001350
		private static string GetFormattedComment(MemberInfo member, XmlNode commentNode, XmlCommentFormat format)
		{
			string result;
			if (format == XmlCommentFormat.Default)
			{
				result = commentNode.InnerXml.Trim(new char[]
				{
					'\n',
					'\r',
					' '
				});
			}
			else
			{
				string textFromNode = XmlCommentsUtils.GetTextFromNode(commentNode, "summary");
				List<string> @params = XmlCommentsUtils.GetParams(commentNode);
				string textFromNode2 = XmlCommentsUtils.GetTextFromNode(commentNode, "returns");
				List<string> list = new List<string>();
				if (!string.IsNullOrEmpty(textFromNode))
				{
					list.Add(textFromNode);
				}
				list.AddRange(@params);
				if (!string.IsNullOrEmpty(textFromNode2))
				{
					list.Add("@return " + textFromNode2);
				}
				result = string.Join("\n", list.ToArray());
			}
			bool flag = false;
			if (XmlCommentsUtils.FormatComment != null)
			{
				flag = XmlCommentsUtils.FormatComment(ref result);
			}
			if (!flag)
			{
				result = XmlCommentsUtils.FixReferences(member, result);
			}
			return result;
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00003260 File Offset: 0x00001460
		private static string FixReferences(MemberInfo member, string result)
		{
			string fullMemberName = XmlCommentsUtils.GetFullMemberName(member);
			result = Regex.Replace(result, "\"[P|T|M|F]:([^>]*)\"", delegate(Match match)
			{
				string value = match.Value;
				string value2 = match.Groups[1].Value;
				string newValue = XmlCommentsUtils.RemoveCommonParts(fullMemberName, value2);
				return value.Replace(value2, newValue);
			});
			return result;
		}

		// Token: 0x06000050 RID: 80 RVA: 0x0000329C File Offset: 0x0000149C
		private static string RemoveCommonParts(string memberName, string reference)
		{
			reference = Regex.Replace(reference, "\\(.*\\)", "()");
			int num = Math.Min(reference.LastIndexOf('.'), reference.Length - 1);
			int num2 = 0;
			while (num2 <= num && (memberName.Length < reference.Length || memberName[num2] == reference[num2]))
			{
				num2++;
			}
			return reference.Substring(num2);
		}

		// Token: 0x06000051 RID: 81 RVA: 0x00003302 File Offset: 0x00001502
		private static string GetFullMemberName(MemberInfo member)
		{
			if (member is Type)
			{
				return XmlCommentsUtils.GetXmlCommentName((Type)member);
			}
			return XmlCommentsUtils.GetXmlCommentName(member.DeclaringType) + "." + member.Name;
		}

		// Token: 0x06000052 RID: 82 RVA: 0x00003334 File Offset: 0x00001534
		private static List<string> GetParams(XmlNode commentNode)
		{
			List<string> list = new List<string>();
			foreach (object obj in commentNode.SelectNodes("param"))
			{
				XmlNode xmlNode = (XmlNode)obj;
				string innerXml = xmlNode.InnerXml;
				if (!string.IsNullOrEmpty(innerXml))
				{
					string str = string.Empty;
					if (xmlNode.Attributes["name"] != null)
					{
						str = xmlNode.Attributes["name"].Value;
					}
					list.Add("@param " + str + " " + XmlCommentsUtils.SingleLine(innerXml));
				}
			}
			return list;
		}

		// Token: 0x06000053 RID: 83 RVA: 0x000033F4 File Offset: 0x000015F4
		private static string GetTextFromNode(XmlNode commentNode, string query)
		{
			XmlNode xmlNode = commentNode.SelectSingleNode(query);
			if (xmlNode != null)
			{
				return XmlCommentsUtils.SingleLine(xmlNode.InnerXml);
			}
			return null;
		}

		// Token: 0x06000054 RID: 84 RVA: 0x00003428 File Offset: 0x00001628
		private static string SingleLine(string s)
		{
			string[] value = s.Split(new char[]
			{
				'\n',
				'\r',
				' '
			}, StringSplitOptions.RemoveEmptyEntries);
			return string.Join(" ", value);
		}

		// Token: 0x06000055 RID: 85 RVA: 0x0000345C File Offset: 0x0000165C
		private static XmlNode GetCommentNodeForMember(XmlDocument commentsDoc, MemberInfo member)
		{
			string xmlCommentMemberName = XmlCommentsUtils.GetXmlCommentMemberName(member);
			string xpath = string.Format("doc/members/member[@name=\"{0}\"]", xmlCommentMemberName);
			return commentsDoc.SelectSingleNode(xpath);
		}

		// Token: 0x06000056 RID: 86 RVA: 0x00003484 File Offset: 0x00001684
		private static string GetXmlCommentMemberName(MemberInfo mi)
		{
			string memberType = string.Empty;
			string subType = mi.Name;
			MemberTypes memberType2 = mi.MemberType;
			if (memberType2 <= MemberTypes.Method)
			{
				switch (memberType2)
				{
				case MemberTypes.Constructor:
					break;
				case MemberTypes.Event:
					memberType = "E";
					goto IL_79;
				case MemberTypes.Constructor | MemberTypes.Event:
					goto IL_79;
				case MemberTypes.Field:
					memberType = "F";
					goto IL_79;
				default:
					if (memberType2 != MemberTypes.Method)
					{
						goto IL_79;
					}
					break;
				}
				memberType = "M";
			}
			else if (memberType2 != MemberTypes.Property)
			{
				if (memberType2 == MemberTypes.TypeInfo || memberType2 == MemberTypes.NestedType)
				{
					memberType = "T";
					subType = null;
				}
			}
			else
			{
				memberType = "P";
			}
			IL_79:
			string[] array = null;
			string xmlCommentName;
			if (mi is MethodInfo)
			{
				ParameterInfo[] parameters = ((MethodInfo)mi).GetParameters();
				array = new string[parameters.Length];
				int num = 0;
				foreach (ParameterInfo parameterInfo in parameters)
				{
					array[num++] = XmlCommentsUtils.GetXmlCommentName(parameterInfo.ParameterType);
				}
				xmlCommentName = XmlCommentsUtils.GetXmlCommentName(mi.DeclaringType);
			}
			else if (mi is Type)
			{
				xmlCommentName = XmlCommentsUtils.GetXmlCommentName((Type)mi);
			}
			else
			{
				xmlCommentName = XmlCommentsUtils.GetXmlCommentName(mi.ReflectedType);
			}
			return XmlCommentsUtils.FormatMemberName(memberType, xmlCommentName, subType, array);
		}

		// Token: 0x06000057 RID: 87 RVA: 0x000035A4 File Offset: 0x000017A4
		private static string GetXmlCommentName(Type type)
		{
			string text;
			if (type.IsGenericType)
			{
				text = type.GetGenericTypeDefinition().FullName;
				text = text.Substring(0, text.LastIndexOf('`'));
				if (!type.IsGenericTypeDefinition)
				{
					string str = string.Join(",", (from t in type.GetGenericArguments()
					select XmlCommentsUtils.GetXmlCommentName(t)).ToArray<string>());
					text = text + "{" + str + "}";
				}
			}
			else
			{
				text = type.ToString();
			}
			return text.Replace('&', '@').Replace('+', '.');
		}

		// Token: 0x06000058 RID: 88 RVA: 0x00003644 File Offset: 0x00001844
		private static string FormatMemberName(string memberType, string mainType, string subType, string[] prms)
		{
			if (string.IsNullOrEmpty(subType))
			{
				return string.Format("{0}:{1}", memberType, mainType);
			}
			if (prms == null || prms.Length == 0)
			{
				return string.Format("{0}:{1}.{2}", memberType, mainType, subType);
			}
			return string.Format("{0}:{1}.{2}({3})", new object[]
			{
				memberType,
				mainType,
				subType,
				string.Join(",", prms)
			});
		}

		// Token: 0x06000059 RID: 89 RVA: 0x000036A8 File Offset: 0x000018A8
		private static bool ContainsDotNetXMLCommentTags(string documentation)
		{
			return documentation.Contains("<summary>") || documentation.Contains("<param") || documentation.Contains("<returns>") || documentation.Contains("<remarks>");
		}

		// Token: 0x0600005A RID: 90 RVA: 0x000036E0 File Offset: 0x000018E0
		private static IEnumerable<string> ExtractLines(string documentation, bool wrapLongLines)
		{
			List<string> list = new List<string>();
			using (TextReader textReader = new StringReader(documentation))
			{
				string text;
				while ((text = textReader.ReadLine()) != null)
				{
					text = text.Trim();
					if (wrapLongLines)
					{
						using (IEnumerator<string> enumerator = XmlCommentsUtils.WordWrapLine(text, 105).GetEnumerator())
						{
							while (enumerator.MoveNext())
							{
								string item = enumerator.Current;
								list.Add(item);
							}
							continue;
						}
					}
					list.Add(text);
				}
			}
			return list;
		}

		// Token: 0x0600005B RID: 91 RVA: 0x00003778 File Offset: 0x00001978
		private static IEnumerable<string> WordWrapLine(string line, int maxLen)
		{
			List<string> list = new List<string>();
			StringBuilder stringBuilder = new StringBuilder();
			string[] array = line.Split(new char[]
			{
				' '
			});
			foreach (string text in array)
			{
				if (stringBuilder.Length > 0 && stringBuilder.Length + text.Length >= maxLen)
				{
					list.Add(stringBuilder.ToString());
					stringBuilder = new StringBuilder();
				}
				stringBuilder.Append(text);
				stringBuilder.Append(' ');
			}
			list.Add(stringBuilder.ToString());
			return list;
		}

		// Token: 0x0600005C RID: 92 RVA: 0x0000380D File Offset: 0x00001A0D
		public static void ClearCache()
		{
			XmlCommentsUtils.xmlDocCache.Clear();
			XmlCommentsUtils.memberCommentCache.Clear();
		}

		// Token: 0x0600005D RID: 93 RVA: 0x00003823 File Offset: 0x00001A23
		public static XmlDocument LoadXmlComments(Type type)
		{
			return XmlCommentsUtils.LoadXmlComments(type, false);
		}

		// Token: 0x0600005E RID: 94 RVA: 0x0000382C File Offset: 0x00001A2C
		public static XmlDocument LoadXmlComments(Type type, bool throwIfNotFound)
		{
			Assembly assembly = type.Assembly;
			XmlDocument xmlDocument = null;
			if (!string.IsNullOrEmpty(assembly.Location))
			{
				string fileName = assembly.Location;
				xmlDocument = XmlCommentsUtils.TryLoadFromLocation(fileName);
				if (xmlDocument == null)
				{
					fileName = new Uri(assembly.CodeBase).LocalPath;
					xmlDocument = XmlCommentsUtils.TryLoadFromLocation(fileName);
				}
			}
			if (xmlDocument == null && throwIfNotFound)
			{
				throw new ApplicationException("XML documentation file for " + Path.GetFileName(assembly.Location) + " was not found. Make sure the XML documentation option is enabled in the project properties for that assembly.");
			}
			return xmlDocument;
		}

		// Token: 0x0600005F RID: 95 RVA: 0x000038A0 File Offset: 0x00001AA0
		public static string GetFormattedComment(XmlDocument commentsDoc, MemberInfo member, XmlCommentFormat format)
		{
			string text;
			if (XmlCommentsUtils.memberCommentCache.TryGetValue(member, out text))
			{
				return text;
			}
			XmlNode commentNodeForMember = XmlCommentsUtils.GetCommentNodeForMember(commentsDoc, member);
			if (commentNodeForMember != null)
			{
				text = XmlCommentsUtils.GetFormattedComment(member, commentNodeForMember, format);
			}
			else
			{
				text = null;
			}
			XmlCommentsUtils.memberCommentCache[member] = text;
			return text;
		}

		// Token: 0x06000060 RID: 96 RVA: 0x000038E4 File Offset: 0x00001AE4
		public static IEnumerable<string> ParseAndReformatComment(string documentation, XmlCommentFormat format, bool wrapLongLines)
		{
			if (format == XmlCommentFormat.Default || XmlCommentsUtils.ContainsDotNetXMLCommentTags(documentation))
			{
				return XmlCommentsUtils.ExtractLines(documentation, wrapLongLines);
			}
			string text = string.Empty;
			Match match = Regex.Match(documentation, "^@", RegexOptions.Multiline);
			if (match.Success)
			{
				text = documentation.Substring(0, match.Index);
				if (text.Length > 0)
				{
					text = "<summary>\r\n" + text + "\r\n</summary>\r\n";
				}
				documentation = documentation.Substring(match.Index);
				documentation = Regex.Replace(documentation, "^@return\\s+(?<text>.*)$", "<returns>${text}</returns>", RegexOptions.Multiline);
				documentation = Regex.Replace(documentation, "^@param\\s+(?<name>\\S+)\\s+(?<text>.*)$", "<param name=\"${name}\">${text}</param>", RegexOptions.Multiline);
			}
			else
			{
				documentation = "<summary>\r\n" + documentation + "\r\n</summary>\r\n";
			}
			return XmlCommentsUtils.ExtractLines(text + documentation, true);
		}

		// Token: 0x14000001 RID: 1
		// (add) Token: 0x06000061 RID: 97 RVA: 0x0000399C File Offset: 0x00001B9C
		// (remove) Token: 0x06000062 RID: 98 RVA: 0x000039D0 File Offset: 0x00001BD0
		public static event FormatComment FormatComment;

		// Token: 0x04000014 RID: 20
		private static Dictionary<string, XmlDocument> xmlDocCache = new Dictionary<string, XmlDocument>();

		// Token: 0x04000015 RID: 21
		private static Dictionary<MemberInfo, string> memberCommentCache = new Dictionary<MemberInfo, string>();
	}
}
