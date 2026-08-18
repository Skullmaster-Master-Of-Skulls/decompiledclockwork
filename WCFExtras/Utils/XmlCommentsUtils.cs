using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using WCFExtras.Wsdl.Documentation;

namespace WCFExtras.Utils
{
	// Token: 0x02000017 RID: 23
	public static class XmlCommentsUtils
	{
		// Token: 0x0600007D RID: 125 RVA: 0x00003F44 File Offset: 0x00002144
		private static XmlDocument TryLoadFromLocation(string fileName)
		{
			XmlDocument xmlDocument = null;
			XmlDocument result;
			if (XmlCommentsUtils.xmlDocCache.TryGetValue(fileName, out xmlDocument))
			{
				result = xmlDocument;
			}
			else
			{
				FileInfo fileInfo = new FileInfo(Path.ChangeExtension(fileName, "xml"));
				if (fileInfo.Exists)
				{
					xmlDocument = new XmlDocument();
					xmlDocument.PreserveWhitespace = true;
					xmlDocument.Load(fileInfo.FullName);
				}
				XmlCommentsUtils.xmlDocCache[fileName] = xmlDocument;
				result = xmlDocument;
			}
			return result;
		}

		// Token: 0x0600007E RID: 126 RVA: 0x00003FBC File Offset: 0x000021BC
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

		// Token: 0x0600007F RID: 127 RVA: 0x000040F8 File Offset: 0x000022F8
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

		// Token: 0x06000080 RID: 128 RVA: 0x00004138 File Offset: 0x00002338
		private static string RemoveCommonParts(string memberName, string reference)
		{
			reference = Regex.Replace(reference, "\\(.*\\)", "()");
			int num = Math.Min(reference.LastIndexOf('.'), memberName.Length);
			int num2 = 0;
			while (num2 <= num && memberName[num2] == reference[num2])
			{
				num2++;
			}
			return reference.Substring(num2);
		}

		// Token: 0x06000081 RID: 129 RVA: 0x0000419C File Offset: 0x0000239C
		private static string GetFullMemberName(MemberInfo member)
		{
			string result;
			if (member is Type)
			{
				result = XmlCommentsUtils.GetXmlCommentName((Type)member);
			}
			else
			{
				result = XmlCommentsUtils.GetXmlCommentName(member.DeclaringType) + "." + member.Name;
			}
			return result;
		}

		// Token: 0x06000082 RID: 130 RVA: 0x000041E8 File Offset: 0x000023E8
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

		// Token: 0x06000083 RID: 131 RVA: 0x000042D0 File Offset: 0x000024D0
		private static string GetTextFromNode(XmlNode commentNode, string query)
		{
			XmlNode xmlNode = commentNode.SelectSingleNode(query);
			string result;
			if (xmlNode != null)
			{
				result = XmlCommentsUtils.SingleLine(xmlNode.InnerXml);
			}
			else
			{
				result = null;
			}
			return result;
		}

		// Token: 0x06000084 RID: 132 RVA: 0x00004310 File Offset: 0x00002510
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

		// Token: 0x06000085 RID: 133 RVA: 0x00004348 File Offset: 0x00002548
		private static XmlNode GetCommentNodeForMember(XmlDocument commentsDoc, MemberInfo member)
		{
			string xmlCommentMemberName = XmlCommentsUtils.GetXmlCommentMemberName(member);
			string xpath = string.Format("doc/members/member[@name=\"{0}\"]", xmlCommentMemberName);
			return commentsDoc.SelectSingleNode(xpath);
		}

		// Token: 0x06000086 RID: 134 RVA: 0x00004374 File Offset: 0x00002574
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
					goto IL_7C;
				case MemberTypes.Constructor | MemberTypes.Event:
					goto IL_7C;
				case MemberTypes.Field:
					memberType = "F";
					goto IL_7C;
				default:
					if (memberType2 != MemberTypes.Method)
					{
						goto IL_7C;
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
			IL_7C:
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

		// Token: 0x06000087 RID: 135 RVA: 0x000044CC File Offset: 0x000026CC
		private static string GetXmlCommentName(Type type)
		{
			string text;
			if (type.IsGenericType)
			{
				text = type.GetGenericTypeDefinition().FullName;
				text = text.Substring(0, text.LastIndexOf('`'));
				if (!type.IsGenericTypeDefinition)
				{
					string str = string.Join(",", Enumerable.Select<Type, string>(type.GetGenericArguments(), (Type t) => XmlCommentsUtils.GetXmlCommentName(t)).ToArray<string>());
					text = text + "{" + str + "}";
				}
			}
			else
			{
				text = type.ToString();
			}
			return text.Replace('&', '@').Replace('+', '.');
		}

		// Token: 0x06000088 RID: 136 RVA: 0x00004580 File Offset: 0x00002780
		private static string FormatMemberName(string memberType, string mainType, string subType, string[] prms)
		{
			string result;
			if (string.IsNullOrEmpty(subType))
			{
				result = string.Format("{0}:{1}", memberType, mainType);
			}
			else if (prms == null || prms.Length == 0)
			{
				result = string.Format("{0}:{1}.{2}", memberType, mainType, subType);
			}
			else
			{
				result = string.Format("{0}:{1}.{2}({3})", new object[]
				{
					memberType,
					mainType,
					subType,
					string.Join(",", prms)
				});
			}
			return result;
		}

		// Token: 0x06000089 RID: 137 RVA: 0x00004600 File Offset: 0x00002800
		private static bool ContainsDotNetXMLCommentTags(string documentation)
		{
			return documentation.Contains("<summary>") || documentation.Contains("<param") || documentation.Contains("<returns>") || documentation.Contains("<remarks>");
		}

		// Token: 0x0600008A RID: 138 RVA: 0x00004648 File Offset: 0x00002848
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
						foreach (string item in XmlCommentsUtils.WordWrapLine(text, 105))
						{
							list.Add(item);
						}
					}
					else
					{
						list.Add(text);
					}
				}
			}
			return list;
		}

		// Token: 0x0600008B RID: 139 RVA: 0x0000471C File Offset: 0x0000291C
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

		// Token: 0x0600008C RID: 140 RVA: 0x000047CE File Offset: 0x000029CE
		public static void ClearCache()
		{
			XmlCommentsUtils.xmlDocCache.Clear();
			XmlCommentsUtils.memberCommentCache.Clear();
		}

		// Token: 0x0600008D RID: 141 RVA: 0x000047E8 File Offset: 0x000029E8
		public static XmlDocument LoadXmlComments(Type type)
		{
			return XmlCommentsUtils.LoadXmlComments(type, false);
		}

		// Token: 0x0600008E RID: 142 RVA: 0x00004804 File Offset: 0x00002A04
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

		// Token: 0x0600008F RID: 143 RVA: 0x0000489C File Offset: 0x00002A9C
		public static string GetFormattedComment(XmlDocument commentsDoc, MemberInfo member, XmlCommentFormat format)
		{
			string text;
			string result;
			if (XmlCommentsUtils.memberCommentCache.TryGetValue(member, out text))
			{
				result = text;
			}
			else
			{
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
				result = text;
			}
			return result;
		}

		// Token: 0x06000090 RID: 144 RVA: 0x000048F4 File Offset: 0x00002AF4
		public static IEnumerable<string> ParseAndReformatComment(string documentation, XmlCommentFormat format, bool wrapLongLines)
		{
			IEnumerable<string> result;
			if (format == XmlCommentFormat.Default || XmlCommentsUtils.ContainsDotNetXMLCommentTags(documentation))
			{
				result = XmlCommentsUtils.ExtractLines(documentation, wrapLongLines);
			}
			else
			{
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
				result = XmlCommentsUtils.ExtractLines(text + documentation, true);
			}
			return result;
		}

		// Token: 0x14000001 RID: 1
		// (add) Token: 0x06000091 RID: 145 RVA: 0x000049CD File Offset: 0x00002BCD
		// (remove) Token: 0x06000092 RID: 146 RVA: 0x000049E4 File Offset: 0x00002BE4
		public static event FormatComment FormatComment;

		// Token: 0x0400001D RID: 29
		private static Dictionary<string, XmlDocument> xmlDocCache = new Dictionary<string, XmlDocument>();

		// Token: 0x0400001E RID: 30
		private static Dictionary<MemberInfo, string> memberCommentCache = new Dictionary<MemberInfo, string>();
	}
}
