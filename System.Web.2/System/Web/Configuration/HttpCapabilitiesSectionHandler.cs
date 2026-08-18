using System;
using System.Collections;
using System.Configuration;
using System.IO;
using System.Security;
using System.Security.Permissions;
using System.Text.RegularExpressions;
using System.Web.UI;
using System.Xml;

namespace System.Web.Configuration
{
	// Token: 0x020006F5 RID: 1781
	public class HttpCapabilitiesSectionHandler : IConfigurationSectionHandler
	{
		// Token: 0x060055FB RID: 22011 RVA: 0x0012D4AC File Offset: 0x0012B6AC
		public object Create(object parent, object configurationContext, XmlNode section)
		{
			if (!HandlerBase.IsServerConfiguration(configurationContext))
			{
				return null;
			}
			HttpCapabilitiesSectionHandler.ParseState parseState = new HttpCapabilitiesSectionHandler.ParseState();
			parseState.SectionName = section.Name;
			parseState.Evaluator = new HttpCapabilitiesDefaultProvider((HttpCapabilitiesDefaultProvider)parent);
			int num = 0;
			if (parent != null)
			{
				num = ((HttpCapabilitiesDefaultProvider)parent).UserAgentCacheKeyLength;
			}
			HandlerBase.GetAndRemovePositiveIntegerAttribute(section, "userAgentCacheKeyLength", ref num);
			if (num == 0)
			{
				num = 64;
			}
			parseState.Evaluator.UserAgentCacheKeyLength = num;
			string browserCapabilitiesProviderType = null;
			if (parent != null)
			{
				browserCapabilitiesProviderType = ((HttpCapabilitiesDefaultProvider)parent).BrowserCapabilitiesProviderType;
			}
			HandlerBase.GetAndRemoveNonEmptyStringAttribute(section, "provider", ref browserCapabilitiesProviderType);
			parseState.Evaluator.BrowserCapabilitiesProviderType = browserCapabilitiesProviderType;
			HandlerBase.CheckForUnrecognizedAttributes(section);
			ArrayList arrayList = HttpCapabilitiesSectionHandler.RuleListFromElement(parseState, section, true);
			if (arrayList.Count > 0)
			{
				parseState.RuleList.Add(new CapabilitiesSection(2, null, null, arrayList));
			}
			if (parseState.FileList.Count > 0)
			{
				parseState.IsExternalFile = true;
				HttpCapabilitiesSectionHandler.ResolveFiles(parseState, configurationContext);
			}
			parseState.Evaluator.AddRuleList(parseState.RuleList);
			return parseState.Evaluator;
		}

		// Token: 0x060055FC RID: 22012 RVA: 0x0012D5A4 File Offset: 0x0012B7A4
		private static CapabilitiesRule RuleFromElement(HttpCapabilitiesSectionHandler.ParseState parseState, XmlNode element)
		{
			int type;
			if (element.Name == "filter")
			{
				type = 2;
			}
			else if (element.Name == "case")
			{
				type = 3;
			}
			else
			{
				if (element.Name == "use")
				{
					HandlerBase.CheckForNonCommentChildNodes(element);
					string text = HandlerBase.RemoveRequiredAttribute(element, "var");
					string text2 = HandlerBase.RemoveAttribute(element, "as");
					HandlerBase.CheckForUnrecognizedAttributes(element);
					if (text2 == null)
					{
						text2 = string.Empty;
					}
					parseState.Evaluator.AddDependency(text);
					return new CapabilitiesUse(text, text2);
				}
				throw new ConfigurationErrorsException(SR.GetString("Unknown_tag_in_caps_config", new object[]
				{
					element.Name
				}), element);
			}
			string text3 = HandlerBase.RemoveAttribute(element, "match");
			string text4 = HandlerBase.RemoveAttribute(element, "with");
			HandlerBase.CheckForUnrecognizedAttributes(element);
			DelayedRegex regex;
			CapabilitiesPattern expr;
			if (text3 == null)
			{
				if (text4 != null)
				{
					throw new ConfigurationErrorsException(SR.GetString("Cannot_specify_test_without_match"), element);
				}
				regex = null;
				expr = null;
			}
			else
			{
				try
				{
					regex = new DelayedRegex(text3);
				}
				catch (Exception ex)
				{
					throw new ConfigurationErrorsException(ex.Message, ex, element);
				}
				if (text4 == null)
				{
					expr = CapabilitiesPattern.Default;
				}
				else
				{
					expr = new CapabilitiesPattern(text4);
				}
			}
			ArrayList rulelist = HttpCapabilitiesSectionHandler.RuleListFromElement(parseState, element, false);
			return new CapabilitiesSection(type, regex, expr, rulelist);
		}

		// Token: 0x060055FD RID: 22013 RVA: 0x0012D6EC File Offset: 0x0012B8EC
		private static ArrayList RuleListFromElement(HttpCapabilitiesSectionHandler.ParseState parseState, XmlNode node, bool top)
		{
			ArrayList arrayList = new ArrayList();
			foreach (object obj in node.ChildNodes)
			{
				XmlNode xmlNode = (XmlNode)obj;
				XmlNodeType nodeType = xmlNode.NodeType;
				if (nodeType <= XmlNodeType.CDATA)
				{
					if (nodeType == XmlNodeType.Element)
					{
						string name = xmlNode.Name;
						if (!(name == "result"))
						{
							if (!(name == "file"))
							{
								arrayList.Add(HttpCapabilitiesSectionHandler.RuleFromElement(parseState, xmlNode));
							}
							else
							{
								if (parseState.IsExternalFile)
								{
									throw new ConfigurationErrorsException(SR.GetString("File_element_only_valid_in_config"), xmlNode);
								}
								HttpCapabilitiesSectionHandler.ProcessFile(parseState.FileList, xmlNode);
							}
						}
						else
						{
							if (!top)
							{
								throw new ConfigurationErrorsException(SR.GetString("Result_must_be_at_the_top_browser_section"), xmlNode);
							}
							HttpCapabilitiesSectionHandler.ProcessResult(parseState.Evaluator, xmlNode);
						}
						top = false;
						continue;
					}
					if (nodeType - XmlNodeType.Text <= 1)
					{
						top = false;
						HttpCapabilitiesSectionHandler.AppendLines(arrayList, xmlNode.Value, node);
						continue;
					}
				}
				else if (nodeType == XmlNodeType.Comment || nodeType == XmlNodeType.Whitespace)
				{
					continue;
				}
				HandlerBase.ThrowUnrecognizedElement(xmlNode);
			}
			return arrayList;
		}

		// Token: 0x060055FE RID: 22014 RVA: 0x0012D81C File Offset: 0x0012BA1C
		private static void ProcessFile(ArrayList fileList, XmlNode node)
		{
			string x = null;
			XmlNode andRemoveRequiredStringAttribute = HandlerBase.GetAndRemoveRequiredStringAttribute(node, "src", ref x);
			HandlerBase.CheckForUnrecognizedAttributes(node);
			HandlerBase.CheckForNonCommentChildNodes(node);
			fileList.Add(new Pair(x, andRemoveRequiredStringAttribute));
		}

		// Token: 0x060055FF RID: 22015 RVA: 0x0012D854 File Offset: 0x0012BA54
		private static void ProcessResult(HttpCapabilitiesDefaultProvider capabilitiesEvaluator, XmlNode node)
		{
			bool flag = true;
			HandlerBase.GetAndRemoveBooleanAttribute(node, "inherit", ref flag);
			if (!flag)
			{
				capabilitiesEvaluator.ClearParent();
			}
			Type type = null;
			XmlNode xmlNode = HandlerBase.GetAndRemoveTypeAttribute(node, "type", ref type);
			if (xmlNode != null && !type.Equals(capabilitiesEvaluator._resultType))
			{
				HandlerBase.CheckAssignableType(xmlNode, capabilitiesEvaluator._resultType, type);
				capabilitiesEvaluator._resultType = type;
			}
			int num = 0;
			xmlNode = HandlerBase.GetAndRemovePositiveIntegerAttribute(node, "cacheTime", ref num);
			if (xmlNode != null)
			{
				capabilitiesEvaluator.CacheTime = TimeSpan.FromSeconds((double)num);
			}
			HandlerBase.CheckForUnrecognizedAttributes(node);
			HandlerBase.CheckForNonCommentChildNodes(node);
		}

		// Token: 0x06005600 RID: 22016 RVA: 0x0012D8DC File Offset: 0x0012BADC
		private static void ResolveFiles(HttpCapabilitiesSectionHandler.ParseState parseState, object configurationContext)
		{
			HttpConfigurationContext httpConfigurationContext = (HttpConfigurationContext)configurationContext;
			string path = null;
			bool flag = false;
			try
			{
				if (httpConfigurationContext.VirtualPath == null)
				{
					flag = true;
					new FileIOPermission(PermissionState.None)
					{
						AllFiles = FileIOPermissionAccess.PathDiscovery
					}.Assert();
				}
				Pair pair = (Pair)parseState.FileList[0];
				XmlNode node = (XmlNode)pair.Second;
				path = Path.GetDirectoryName(ConfigurationErrorsException.GetFilename(node));
			}
			finally
			{
				if (flag)
				{
					CodeAccessPermission.RevertAssert();
				}
			}
			foreach (object obj in parseState.FileList)
			{
				Pair pair2 = (Pair)obj;
				string path2 = (string)pair2.First;
				string text = Path.Combine(path, path2);
				XmlNode documentElement;
				try
				{
					if (flag)
					{
						InternalSecurityPermissions.FileReadAccess(text).Assert();
					}
					Exception ex = null;
					try
					{
						HttpConfigurationSystem.AddFileDependency(text);
					}
					catch (Exception ex2)
					{
						ex = ex2;
					}
					ConfigXmlDocument configXmlDocument = new ConfigXmlDocument();
					try
					{
						configXmlDocument.Load(text);
						documentElement = configXmlDocument.DocumentElement;
					}
					catch (Exception ex3)
					{
						throw new ConfigurationErrorsException(SR.GetString("Error_loading_XML_file", new object[]
						{
							text,
							ex3.Message
						}), ex3, (XmlNode)pair2.Second);
					}
					if (ex != null)
					{
						throw ex;
					}
				}
				finally
				{
					if (flag)
					{
						CodeAccessPermission.RevertAssert();
					}
				}
				if (documentElement.Name != parseState.SectionName)
				{
					throw new ConfigurationErrorsException(SR.GetString("Capability_file_root_element", new object[]
					{
						parseState.SectionName
					}), documentElement);
				}
				HandlerBase.CheckForUnrecognizedAttributes(documentElement);
				ArrayList arrayList = HttpCapabilitiesSectionHandler.RuleListFromElement(parseState, documentElement, true);
				if (arrayList.Count > 0)
				{
					parseState.RuleList.Add(new CapabilitiesSection(2, null, null, arrayList));
				}
			}
		}

		// Token: 0x06005601 RID: 22017 RVA: 0x0012DB10 File Offset: 0x0012BD10
		private static void AppendLines(ArrayList setlist, string text, XmlNode node)
		{
			int num = ConfigurationErrorsException.GetLineNumber(node);
			int num2 = 0;
			Match match;
			for (;;)
			{
				if ((match = HttpCapabilitiesSectionHandler.wsRegex.Match(text, num2)).Success)
				{
					num += Util.LineCount(text, num2, match.Index + match.Length);
					num2 = match.Index + match.Length;
				}
				if (num2 == text.Length)
				{
					return;
				}
				if (!(match = HttpCapabilitiesSectionHandler.lineRegex.Match(text, num2)).Success)
				{
					break;
				}
				setlist.Add(new CapabilitiesAssignment(match.Groups["var"].Value, new CapabilitiesPattern(match.Groups["pat"].Value)));
				num += Util.LineCount(text, num2, match.Index + match.Length);
				num2 = match.Index + match.Length;
			}
			match = HttpCapabilitiesSectionHandler.errRegex.Match(text, num2);
			throw new ConfigurationErrorsException(SR.GetString("Problem_reading_caps_config", new object[]
			{
				match.ToString()
			}), ConfigurationErrorsException.GetFilename(node), num);
		}

		// Token: 0x04002DA9 RID: 11689
		private const int _defaultUserAgentCacheKeyLength = 64;

		// Token: 0x04002DAA RID: 11690
		private static Regex lineRegex = new Regex("\\G(?<var>\\w+)\\s*=\\s*(?:\"(?<pat>[^\"\r\n\\\\]*(?:\\\\.[^\"\r\n\\\\]*)*)\"|(?!\")(?<pat>\\S+))\\s*");

		// Token: 0x04002DAB RID: 11691
		private static Regex wsRegex = new Regex("\\G\\s*");

		// Token: 0x04002DAC RID: 11692
		private static Regex errRegex = new Regex("\\G\\S {0,8}");

		// Token: 0x02000A44 RID: 2628
		private class ParseState
		{
			// Token: 0x06006E99 RID: 28313 RVA: 0x0018A177 File Offset: 0x00188377
			internal ParseState()
			{
			}

			// Token: 0x04003B0E RID: 15118
			internal string SectionName;

			// Token: 0x04003B0F RID: 15119
			internal HttpCapabilitiesDefaultProvider Evaluator;

			// Token: 0x04003B10 RID: 15120
			internal ArrayList RuleList = new ArrayList();

			// Token: 0x04003B11 RID: 15121
			internal ArrayList FileList = new ArrayList();

			// Token: 0x04003B12 RID: 15122
			internal bool IsExternalFile;
		}
	}
}
