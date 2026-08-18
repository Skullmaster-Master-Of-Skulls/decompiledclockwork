using System;
using System.Collections;
using System.Globalization;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Security.Permissions;
using System.Text;
using System.Threading;
using System.Xml;
using Microsoft.Win32;

namespace System.Security.Cryptography.Xml
{
	// Token: 0x02000065 RID: 101
	internal class Utils
	{
		// Token: 0x060003A9 RID: 937 RVA: 0x000044A9 File Offset: 0x000026A9
		private Utils()
		{
		}

		// Token: 0x060003AA RID: 938 RVA: 0x00011E35 File Offset: 0x00010035
		private static bool HasNamespace(XmlElement element, string prefix, string value)
		{
			return Utils.IsCommittedNamespace(element, prefix, value) || (element.Prefix == prefix && element.NamespaceURI == value);
		}

		// Token: 0x060003AB RID: 939 RVA: 0x00011E64 File Offset: 0x00010064
		internal static bool IsCommittedNamespace(XmlElement element, string prefix, string value)
		{
			if (element == null)
			{
				throw new ArgumentNullException("element");
			}
			string name = (prefix.Length > 0) ? ("xmlns:" + prefix) : "xmlns";
			return element.HasAttribute(name) && element.GetAttribute(name) == value;
		}

		// Token: 0x060003AC RID: 940 RVA: 0x00011EB8 File Offset: 0x000100B8
		internal static bool IsRedundantNamespace(XmlElement element, string prefix, string value)
		{
			if (element == null)
			{
				throw new ArgumentNullException("element");
			}
			for (XmlNode parentNode = element.ParentNode; parentNode != null; parentNode = parentNode.ParentNode)
			{
				XmlElement xmlElement = parentNode as XmlElement;
				if (xmlElement != null && Utils.HasNamespace(xmlElement, prefix, value))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060003AD RID: 941 RVA: 0x00011F00 File Offset: 0x00010100
		internal static string GetAttribute(XmlElement element, string localName, string namespaceURI)
		{
			string text = element.HasAttribute(localName) ? element.GetAttribute(localName) : null;
			if (text == null && element.HasAttribute(localName, namespaceURI))
			{
				text = element.GetAttribute(localName, namespaceURI);
			}
			return text;
		}

		// Token: 0x060003AE RID: 942 RVA: 0x00011F38 File Offset: 0x00010138
		internal static bool HasAttribute(XmlElement element, string localName, string namespaceURI)
		{
			return element.HasAttribute(localName) || element.HasAttribute(localName, namespaceURI);
		}

		// Token: 0x060003AF RID: 943 RVA: 0x00011F4D File Offset: 0x0001014D
		internal static bool IsNamespaceNode(XmlNode n)
		{
			return n.NodeType == XmlNodeType.Attribute && (n.Prefix.Equals("xmlns") || (n.Prefix.Length == 0 && n.LocalName.Equals("xmlns")));
		}

		// Token: 0x060003B0 RID: 944 RVA: 0x00011F8D File Offset: 0x0001018D
		internal static bool IsXmlNamespaceNode(XmlNode n)
		{
			return n.NodeType == XmlNodeType.Attribute && n.Prefix.Equals("xml");
		}

		// Token: 0x060003B1 RID: 945 RVA: 0x00011FAC File Offset: 0x000101AC
		internal static bool IsDefaultNamespaceNode(XmlNode n)
		{
			bool flag = n.NodeType == XmlNodeType.Attribute && n.Prefix.Length == 0 && n.LocalName.Equals("xmlns");
			bool flag2 = Utils.IsXmlNamespaceNode(n);
			return flag || flag2;
		}

		// Token: 0x060003B2 RID: 946 RVA: 0x00011FED File Offset: 0x000101ED
		internal static bool IsEmptyDefaultNamespaceNode(XmlNode n)
		{
			return Utils.IsDefaultNamespaceNode(n) && n.Value.Length == 0;
		}

		// Token: 0x060003B3 RID: 947 RVA: 0x00012007 File Offset: 0x00010207
		internal static string GetNamespacePrefix(XmlAttribute a)
		{
			if (a.Prefix.Length != 0)
			{
				return a.LocalName;
			}
			return string.Empty;
		}

		// Token: 0x060003B4 RID: 948 RVA: 0x00012022 File Offset: 0x00010222
		internal static bool HasNamespacePrefix(XmlAttribute a, string nsPrefix)
		{
			return Utils.GetNamespacePrefix(a).Equals(nsPrefix);
		}

		// Token: 0x060003B5 RID: 949 RVA: 0x00012030 File Offset: 0x00010230
		internal static bool IsNonRedundantNamespaceDecl(XmlAttribute a, XmlAttribute nearestAncestorWithSamePrefix)
		{
			if (nearestAncestorWithSamePrefix == null)
			{
				return !Utils.IsEmptyDefaultNamespaceNode(a);
			}
			return !nearestAncestorWithSamePrefix.Value.Equals(a.Value);
		}

		// Token: 0x060003B6 RID: 950 RVA: 0x00004984 File Offset: 0x00002B84
		internal static bool IsXmlPrefixDefinitionNode(XmlAttribute a)
		{
			return false;
		}

		// Token: 0x060003B7 RID: 951 RVA: 0x00012053 File Offset: 0x00010253
		internal static string DiscardWhiteSpaces(string inputBuffer)
		{
			return Utils.DiscardWhiteSpaces(inputBuffer, 0, inputBuffer.Length);
		}

		// Token: 0x060003B8 RID: 952 RVA: 0x00012064 File Offset: 0x00010264
		internal static string DiscardWhiteSpaces(string inputBuffer, int inputOffset, int inputCount)
		{
			int num = 0;
			for (int i = 0; i < inputCount; i++)
			{
				if (char.IsWhiteSpace(inputBuffer[inputOffset + i]))
				{
					num++;
				}
			}
			char[] array = new char[inputCount - num];
			num = 0;
			for (int i = 0; i < inputCount; i++)
			{
				if (!char.IsWhiteSpace(inputBuffer[inputOffset + i]))
				{
					array[num++] = inputBuffer[inputOffset + i];
				}
			}
			return new string(array);
		}

		// Token: 0x060003B9 RID: 953 RVA: 0x000120D0 File Offset: 0x000102D0
		internal static void SBReplaceCharWithString(StringBuilder sb, char oldChar, string newString)
		{
			int i = 0;
			int length = newString.Length;
			while (i < sb.Length)
			{
				if (sb[i] == oldChar)
				{
					sb.Remove(i, 1);
					sb.Insert(i, newString);
					i += length;
				}
				else
				{
					i++;
				}
			}
		}

		// Token: 0x060003BA RID: 954 RVA: 0x00012118 File Offset: 0x00010318
		internal static XmlReader PreProcessStreamInput(Stream inputStream, XmlResolver xmlResolver, string baseUri)
		{
			XmlReaderSettings secureXmlReaderSettings = Utils.GetSecureXmlReaderSettings(xmlResolver);
			return XmlReader.Create(inputStream, secureXmlReaderSettings, baseUri);
		}

		// Token: 0x060003BB RID: 955 RVA: 0x00012138 File Offset: 0x00010338
		internal static XmlReaderSettings GetSecureXmlReaderSettings(XmlResolver xmlResolver)
		{
			return new XmlReaderSettings
			{
				XmlResolver = xmlResolver,
				DtdProcessing = DtdProcessing.Parse,
				MaxCharactersFromEntities = Utils.GetMaxCharactersFromEntities(),
				MaxCharactersInDocument = Utils.GetMaxCharactersInDocument()
			};
		}

		// Token: 0x060003BC RID: 956 RVA: 0x00012170 File Offset: 0x00010370
		[SecuritySafeCritical]
		[RegistryPermission(SecurityAction.Assert, Unrestricted = true)]
		internal static int GetXmlDsigSearchDepth()
		{
			if (Utils.xmlDsigSearchDepth != null)
			{
				return Utils.xmlDsigSearchDepth.Value;
			}
			long netFxSecurityRegistryValue = Utils.GetNetFxSecurityRegistryValue("SignedDigitalSignatureXmlMaxDepth", 20L);
			Utils.xmlDsigSearchDepth = new int?((int)netFxSecurityRegistryValue);
			return Utils.xmlDsigSearchDepth.Value;
		}

		// Token: 0x060003BD RID: 957 RVA: 0x000121B8 File Offset: 0x000103B8
		[SecuritySafeCritical]
		[RegistryPermission(SecurityAction.Assert, Unrestricted = true)]
		internal static long GetMaxCharactersFromEntities()
		{
			if (Utils.maxCharactersFromEntities != null)
			{
				return Utils.maxCharactersFromEntities.Value;
			}
			long netFxSecurityRegistryValue = Utils.GetNetFxSecurityRegistryValue("SignedXmlMaxCharactersFromEntities", 10000000L);
			Utils.maxCharactersFromEntities = new long?(netFxSecurityRegistryValue);
			return Utils.maxCharactersFromEntities.Value;
		}

		// Token: 0x060003BE RID: 958 RVA: 0x00012204 File Offset: 0x00010404
		[SecuritySafeCritical]
		[RegistryPermission(SecurityAction.Assert, Unrestricted = true)]
		internal static long GetMaxCharactersInDocument()
		{
			if (Utils.s_readMaxCharactersInDocument)
			{
				return Utils.s_maxCharactersInDocument;
			}
			long netFxSecurityRegistryValue = Utils.GetNetFxSecurityRegistryValue("SignedXmlMaxCharactersInDocument", 0L);
			Utils.s_maxCharactersInDocument = netFxSecurityRegistryValue;
			Thread.MemoryBarrier();
			Utils.s_readMaxCharactersInDocument = true;
			return Utils.s_maxCharactersInDocument;
		}

		// Token: 0x060003BF RID: 959 RVA: 0x00012244 File Offset: 0x00010444
		[SecuritySafeCritical]
		[RegistryPermission(SecurityAction.Assert, Unrestricted = true)]
		internal static bool AllowAmbiguousReferenceTargets()
		{
			if (Utils.s_allowAmbiguousReferenceTarget != null)
			{
				return Utils.s_allowAmbiguousReferenceTarget.Value;
			}
			long netFxSecurityRegistryValue = Utils.GetNetFxSecurityRegistryValue("SignedXmlAllowAmbiguousReferenceTargets", 0L);
			bool value = netFxSecurityRegistryValue != 0L;
			Utils.s_allowAmbiguousReferenceTarget = new bool?(value);
			return Utils.s_allowAmbiguousReferenceTarget.Value;
		}

		// Token: 0x060003C0 RID: 960 RVA: 0x00012290 File Offset: 0x00010490
		[SecuritySafeCritical]
		[RegistryPermission(SecurityAction.Assert, Unrestricted = true)]
		internal static bool AllowDetachedSignature()
		{
			if (Utils.s_allowDetachedSignature != null)
			{
				return Utils.s_allowDetachedSignature.Value;
			}
			long netFxSecurityRegistryValue = Utils.GetNetFxSecurityRegistryValue("SignedXmlAllowDetachedSignature", 0L);
			bool value = netFxSecurityRegistryValue != 0L;
			Utils.s_allowDetachedSignature = new bool?(value);
			return Utils.s_allowDetachedSignature.Value;
		}

		// Token: 0x060003C1 RID: 961 RVA: 0x000122DC File Offset: 0x000104DC
		[SecuritySafeCritical]
		[RegistryPermission(SecurityAction.Assert, Unrestricted = true)]
		internal static bool RequireNCNameIdentifier()
		{
			if (Utils.s_readRequireNCNameIdentifier)
			{
				return Utils.s_requireNCNameIdentifier;
			}
			long netFxSecurityRegistryValue = Utils.GetNetFxSecurityRegistryValue("SignedXmlRequireNCNameIdentifier", 1L);
			bool flag = netFxSecurityRegistryValue != 0L;
			Utils.s_requireNCNameIdentifier = flag;
			Thread.MemoryBarrier();
			Utils.s_readRequireNCNameIdentifier = true;
			return Utils.s_requireNCNameIdentifier;
		}

		// Token: 0x060003C2 RID: 962 RVA: 0x00012320 File Offset: 0x00010520
		[SecuritySafeCritical]
		[RegistryPermission(SecurityAction.Assert, Unrestricted = true)]
		internal static long GetMaxTransformsPerReference()
		{
			if (Utils.s_readMaxTransformsPerReference)
			{
				return Utils.s_maxTransformsPerReference;
			}
			long netFxSecurityRegistryValue = Utils.GetNetFxSecurityRegistryValue("SignedXmlMaxTransformsPerReference", 10L);
			Utils.s_maxTransformsPerReference = netFxSecurityRegistryValue;
			Thread.MemoryBarrier();
			Utils.s_readMaxTransformsPerReference = true;
			return Utils.s_maxTransformsPerReference;
		}

		// Token: 0x060003C3 RID: 963 RVA: 0x00012360 File Offset: 0x00010560
		[SecuritySafeCritical]
		[RegistryPermission(SecurityAction.Assert, Unrestricted = true)]
		internal static long GetMaxReferencesPerSignedInfo()
		{
			if (Utils.s_readMaxReferencesPerSignedInfo)
			{
				return Utils.s_maxReferencesPerSignedInfo;
			}
			long netFxSecurityRegistryValue = Utils.GetNetFxSecurityRegistryValue("SignedXmlMaxReferencesPerSignedInfo", 100L);
			Utils.s_maxReferencesPerSignedInfo = netFxSecurityRegistryValue;
			Thread.MemoryBarrier();
			Utils.s_readMaxReferencesPerSignedInfo = true;
			return Utils.s_maxReferencesPerSignedInfo;
		}

		// Token: 0x060003C4 RID: 964 RVA: 0x000123A0 File Offset: 0x000105A0
		[SecuritySafeCritical]
		[RegistryPermission(SecurityAction.Assert, Unrestricted = true)]
		internal static bool GetAllowAdditionalSignatureNodes()
		{
			if (Utils.s_readAllowAdditionalSignatureNodes)
			{
				return Utils.s_allowAdditionalSignatureNodes;
			}
			long netFxSecurityRegistryValue = Utils.GetNetFxSecurityRegistryValue("SignedXmlAllowAdditionalSignatureNodes", 0L);
			bool flag = netFxSecurityRegistryValue != 0L;
			Utils.s_allowAdditionalSignatureNodes = flag;
			Thread.MemoryBarrier();
			Utils.s_readAllowAdditionalSignatureNodes = true;
			return Utils.s_allowAdditionalSignatureNodes;
		}

		// Token: 0x060003C5 RID: 965 RVA: 0x000123E4 File Offset: 0x000105E4
		[SecuritySafeCritical]
		[RegistryPermission(SecurityAction.Assert, Unrestricted = true)]
		internal static bool GetSkipSignatureAttributeEnforcement()
		{
			if (Utils.s_readSkipSignatureAttributeEnforcement)
			{
				return Utils.s_skipSignatureAttributeEnforcement;
			}
			long netFxSecurityRegistryValue = Utils.GetNetFxSecurityRegistryValue("SignedXmlSkipSignatureAttributeEnforcement", 0L);
			bool flag = netFxSecurityRegistryValue != 0L;
			Utils.s_skipSignatureAttributeEnforcement = flag;
			Thread.MemoryBarrier();
			Utils.s_readSkipSignatureAttributeEnforcement = true;
			return Utils.s_skipSignatureAttributeEnforcement;
		}

		// Token: 0x060003C6 RID: 966 RVA: 0x00012427 File Offset: 0x00010627
		internal static bool VerifyAttributes(XmlElement element, string expectedAttrName)
		{
			string[] expectedAttrNames;
			if (expectedAttrName != null)
			{
				(expectedAttrNames = new string[1])[0] = expectedAttrName;
			}
			else
			{
				expectedAttrNames = null;
			}
			return Utils.VerifyAttributes(element, expectedAttrNames);
		}

		// Token: 0x060003C7 RID: 967 RVA: 0x00012440 File Offset: 0x00010640
		internal static bool VerifyAttributes(XmlElement element, string[] expectedAttrNames)
		{
			if (!Utils.GetSkipSignatureAttributeEnforcement())
			{
				foreach (object obj in element.Attributes)
				{
					XmlAttribute xmlAttribute = (XmlAttribute)obj;
					bool flag = xmlAttribute.Name == "xmlns" || xmlAttribute.Name.StartsWith("xmlns:") || xmlAttribute.Name == "xml:space" || xmlAttribute.Name == "xml:lang" || xmlAttribute.Name == "xml:base";
					int num = 0;
					while (!flag && expectedAttrNames != null && num < expectedAttrNames.Length)
					{
						flag = (xmlAttribute.Name == expectedAttrNames[num]);
						num++;
					}
					if (!flag)
					{
						return false;
					}
				}
				return true;
			}
			return true;
		}

		// Token: 0x060003C8 RID: 968 RVA: 0x00012530 File Offset: 0x00010730
		[SecuritySafeCritical]
		[RegistryPermission(SecurityAction.Assert, Unrestricted = true)]
		internal static bool GetAllowBareTypeReference()
		{
			if (Utils.s_readAllowBareTypeReference)
			{
				return Utils.s_allowBareTypeReference;
			}
			long netFxSecurityRegistryValue = Utils.GetNetFxSecurityRegistryValue("CryptoXmlAllowBareTypeReference", 0L);
			bool flag = netFxSecurityRegistryValue != 0L;
			Utils.s_allowBareTypeReference = flag;
			Thread.MemoryBarrier();
			Utils.s_readAllowBareTypeReference = true;
			return Utils.s_allowBareTypeReference;
		}

		// Token: 0x060003C9 RID: 969 RVA: 0x00012574 File Offset: 0x00010774
		[SecuritySafeCritical]
		[RegistryPermission(SecurityAction.Assert, Unrestricted = true)]
		internal static bool GetLeaveCipherValueUnchecked()
		{
			if (Utils.s_readLeaveCipherValueUnchecked)
			{
				return Utils.s_leaveCipherValueUnchecked;
			}
			long netFxSecurityRegistryValue = Utils.GetNetFxSecurityRegistryValue("EncryptedXmlLeaveCipherValueUnchecked", 0L);
			bool flag = netFxSecurityRegistryValue != 0L;
			Utils.s_leaveCipherValueUnchecked = flag;
			Thread.MemoryBarrier();
			Utils.s_readLeaveCipherValueUnchecked = true;
			return Utils.s_leaveCipherValueUnchecked;
		}

		// Token: 0x060003CA RID: 970 RVA: 0x000125B8 File Offset: 0x000107B8
		[SecuritySafeCritical]
		[RegistryPermission(SecurityAction.Assert, Unrestricted = true)]
		internal static int GetDangerousMaxRecursionDepth()
		{
			if (Utils.s_readDangerousMaxRecursionDepth)
			{
				return Utils.s_DangerousMaxRecursionDepth;
			}
			long netFxSecurityRegistryValue = Utils.GetNetFxSecurityRegistryValue("CryptoXmlDangerousMaxRecursionDepth", 64L);
			Utils.s_DangerousMaxRecursionDepth = (int)netFxSecurityRegistryValue;
			Thread.MemoryBarrier();
			Utils.s_readDangerousMaxRecursionDepth = true;
			return Utils.s_DangerousMaxRecursionDepth;
		}

		// Token: 0x060003CB RID: 971 RVA: 0x000125F8 File Offset: 0x000107F8
		[SecuritySafeCritical]
		[RegistryPermission(SecurityAction.Assert, Unrestricted = true)]
		internal static int GetMaxTransformsPerChain()
		{
			if (Utils.s_readMaxTransformsPerChain)
			{
				return Utils.s_maxTransformsPerChain;
			}
			long num = Utils.GetNetFxSecurityRegistryValue("CryptoXmlMaxTransformsPerChain", 20L);
			if (num < 0L)
			{
				num = 20L;
			}
			else if (num > 2147483647L)
			{
				num = 2147483647L;
			}
			Utils.s_maxTransformsPerChain = (int)num;
			Thread.MemoryBarrier();
			Utils.s_readMaxTransformsPerChain = true;
			return Utils.s_maxTransformsPerChain;
		}

		// Token: 0x060003CC RID: 972 RVA: 0x00012654 File Offset: 0x00010854
		[SecuritySafeCritical]
		[RegistryPermission(SecurityAction.Assert, Unrestricted = true)]
		internal static int GetMaxDecryptedDataElements()
		{
			if (Utils.s_readMaxDecryptedDataElements)
			{
				return Utils.s_maxDecryptedDataElements;
			}
			long num = Utils.GetNetFxSecurityRegistryValue("CryptoXmlMaxDecryptedDataElements", 100L);
			if (num < 0L)
			{
				num = 100L;
			}
			else if (num > 2147483647L)
			{
				num = 2147483647L;
			}
			Utils.s_maxDecryptedDataElements = (int)num;
			Thread.MemoryBarrier();
			Utils.s_readMaxDecryptedDataElements = true;
			return Utils.s_maxDecryptedDataElements;
		}

		// Token: 0x060003CD RID: 973 RVA: 0x000126B0 File Offset: 0x000108B0
		[SecuritySafeCritical]
		[RegistryPermission(SecurityAction.Assert, Unrestricted = true)]
		internal static bool GetEncryptedXmlAllowDangerousTransforms()
		{
			if (Utils.s_readEncryptedXmlAllowDangerousTransforms)
			{
				return Utils.s_encryptedXmlAllowDangerousTransforms;
			}
			long netFxSecurityRegistryValue = Utils.GetNetFxSecurityRegistryValue("EncryptedXmlAllowDangerousTransforms", 0L);
			bool flag = netFxSecurityRegistryValue != 0L;
			Utils.s_encryptedXmlAllowDangerousTransforms = flag;
			Thread.MemoryBarrier();
			Utils.s_readEncryptedXmlAllowDangerousTransforms = true;
			return Utils.s_encryptedXmlAllowDangerousTransforms;
		}

		// Token: 0x060003CE RID: 974 RVA: 0x000126F4 File Offset: 0x000108F4
		[SecuritySafeCritical]
		[RegistryPermission(SecurityAction.Assert, Unrestricted = true)]
		internal static bool GetAllowUnsafeTruncatedHmacSignatureVerification()
		{
			if (Utils.s_readAllowUnsafeTruncatedHmacSignatureVerification)
			{
				return Utils.s_allowUnsafeTruncatedHmacSignatureVerification;
			}
			long netFxSecurityRegistryValue = Utils.GetNetFxSecurityRegistryValue("SignedXmlAllowUnsafeTruncatedHmacSignatureVerification", 0L);
			bool flag = netFxSecurityRegistryValue != 0L;
			Utils.s_allowUnsafeTruncatedHmacSignatureVerification = flag;
			Thread.MemoryBarrier();
			Utils.s_readAllowUnsafeTruncatedHmacSignatureVerification = true;
			return Utils.s_allowUnsafeTruncatedHmacSignatureVerification;
		}

		// Token: 0x060003CF RID: 975 RVA: 0x00012738 File Offset: 0x00010938
		internal static T CreateFromName<T>(string key) where T : class
		{
			if (Utils.GetAllowBareTypeReference())
			{
				return CryptoConfig.CreateFromName(key) as T;
			}
			T result;
			if (key == null || key.IndexOfAny(Utils.s_invalidChars) >= 0)
			{
				result = default(T);
				return result;
			}
			try
			{
				result = (CryptoConfig.CreateFromName(key) as T);
			}
			catch (Exception)
			{
				result = default(T);
			}
			return result;
		}

		// Token: 0x060003D0 RID: 976 RVA: 0x000127AC File Offset: 0x000109AC
		private static long GetNetFxSecurityRegistryValue(string regValueName, long defaultValue)
		{
			try
			{
				using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\.NETFramework\\Security", false))
				{
					if (registryKey != null)
					{
						object value = registryKey.GetValue(regValueName);
						if (value != null)
						{
							RegistryValueKind valueKind = registryKey.GetValueKind(regValueName);
							if (valueKind == RegistryValueKind.DWord || valueKind == RegistryValueKind.QWord)
							{
								return Convert.ToInt64(value, CultureInfo.InvariantCulture);
							}
						}
					}
				}
			}
			catch (SecurityException)
			{
			}
			return defaultValue;
		}

		// Token: 0x060003D1 RID: 977 RVA: 0x00012828 File Offset: 0x00010A28
		internal static XmlDocument PreProcessDocumentInput(XmlDocument document, XmlResolver xmlResolver, string baseUri)
		{
			if (document == null)
			{
				throw new ArgumentNullException("document");
			}
			MyXmlDocument myXmlDocument = new MyXmlDocument();
			myXmlDocument.PreserveWhitespace = document.PreserveWhitespace;
			using (TextReader textReader = new StringReader(document.OuterXml))
			{
				XmlReader reader = XmlReader.Create(textReader, new XmlReaderSettings
				{
					XmlResolver = xmlResolver,
					DtdProcessing = DtdProcessing.Parse,
					MaxCharactersFromEntities = Utils.GetMaxCharactersFromEntities(),
					MaxCharactersInDocument = Utils.GetMaxCharactersInDocument()
				}, baseUri);
				myXmlDocument.Load(reader);
			}
			return myXmlDocument;
		}

		// Token: 0x060003D2 RID: 978 RVA: 0x000128B8 File Offset: 0x00010AB8
		internal static XmlDocument PreProcessElementInput(XmlElement elem, XmlResolver xmlResolver, string baseUri)
		{
			if (elem == null)
			{
				throw new ArgumentNullException("elem");
			}
			MyXmlDocument myXmlDocument = new MyXmlDocument();
			myXmlDocument.PreserveWhitespace = true;
			using (TextReader textReader = new StringReader(elem.OuterXml))
			{
				XmlReader reader = XmlReader.Create(textReader, new XmlReaderSettings
				{
					XmlResolver = xmlResolver,
					DtdProcessing = DtdProcessing.Parse,
					MaxCharactersFromEntities = Utils.GetMaxCharactersFromEntities(),
					MaxCharactersInDocument = Utils.GetMaxCharactersInDocument()
				}, baseUri);
				myXmlDocument.Load(reader);
			}
			return myXmlDocument;
		}

		// Token: 0x060003D3 RID: 979 RVA: 0x00012944 File Offset: 0x00010B44
		internal static XmlDocument DiscardComments(XmlDocument document)
		{
			XmlNodeList xmlNodeList = document.SelectNodes("//comment()");
			if (xmlNodeList != null)
			{
				foreach (object obj in xmlNodeList)
				{
					XmlNode xmlNode = (XmlNode)obj;
					xmlNode.ParentNode.RemoveChild(xmlNode);
				}
			}
			return document;
		}

		// Token: 0x060003D4 RID: 980 RVA: 0x000129B0 File Offset: 0x00010BB0
		internal static XmlNodeList AllDescendantNodes(XmlNode node, bool includeComments)
		{
			CanonicalXmlNodeList canonicalXmlNodeList = new CanonicalXmlNodeList();
			CanonicalXmlNodeList canonicalXmlNodeList2 = new CanonicalXmlNodeList();
			CanonicalXmlNodeList canonicalXmlNodeList3 = new CanonicalXmlNodeList();
			CanonicalXmlNodeList canonicalXmlNodeList4 = new CanonicalXmlNodeList();
			int num = 0;
			canonicalXmlNodeList2.Add(node);
			do
			{
				XmlNode xmlNode = canonicalXmlNodeList2[num];
				XmlNodeList childNodes = xmlNode.ChildNodes;
				if (childNodes != null)
				{
					foreach (object obj in childNodes)
					{
						XmlNode xmlNode2 = (XmlNode)obj;
						if (includeComments || !(xmlNode2 is XmlComment))
						{
							canonicalXmlNodeList2.Add(xmlNode2);
						}
					}
				}
				XmlAttributeCollection attributes = xmlNode.Attributes;
				if (attributes != null)
				{
					foreach (object obj2 in xmlNode.Attributes)
					{
						XmlNode xmlNode3 = (XmlNode)obj2;
						if (xmlNode3.LocalName == "xmlns" || xmlNode3.Prefix == "xmlns")
						{
							canonicalXmlNodeList4.Add(xmlNode3);
						}
						else
						{
							canonicalXmlNodeList3.Add(xmlNode3);
						}
					}
				}
				num++;
			}
			while (num < canonicalXmlNodeList2.Count);
			foreach (object obj3 in canonicalXmlNodeList2)
			{
				XmlNode value = (XmlNode)obj3;
				canonicalXmlNodeList.Add(value);
			}
			foreach (object obj4 in canonicalXmlNodeList3)
			{
				XmlNode value2 = (XmlNode)obj4;
				canonicalXmlNodeList.Add(value2);
			}
			foreach (object obj5 in canonicalXmlNodeList4)
			{
				XmlNode value3 = (XmlNode)obj5;
				canonicalXmlNodeList.Add(value3);
			}
			return canonicalXmlNodeList;
		}

		// Token: 0x060003D5 RID: 981 RVA: 0x00012BE4 File Offset: 0x00010DE4
		internal static bool NodeInList(XmlNode node, XmlNodeList nodeList)
		{
			foreach (object obj in nodeList)
			{
				XmlNode xmlNode = (XmlNode)obj;
				if (xmlNode == node)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060003D6 RID: 982 RVA: 0x00012C3C File Offset: 0x00010E3C
		internal static string GetIdFromLocalUri(string uri, out bool discardComments)
		{
			string text = uri.Substring(1);
			discardComments = true;
			if (text.StartsWith("xpointer(id(", StringComparison.Ordinal))
			{
				int num = text.IndexOf("id(", StringComparison.Ordinal);
				int num2 = text.IndexOf(")", StringComparison.Ordinal);
				if (num2 < 0 || num2 < num + 3)
				{
					throw new CryptographicException(SecurityResources.GetResourceString("Cryptography_Xml_InvalidReference"));
				}
				text = text.Substring(num + 3, num2 - num - 3);
				text = text.Replace("'", "");
				text = text.Replace("\"", "");
				discardComments = false;
			}
			return text;
		}

		// Token: 0x060003D7 RID: 983 RVA: 0x00012CCC File Offset: 0x00010ECC
		internal static string ExtractIdFromLocalUri(string uri)
		{
			string text = uri.Substring(1);
			if (text.StartsWith("xpointer(id(", StringComparison.Ordinal))
			{
				int num = text.IndexOf("id(", StringComparison.Ordinal);
				int num2 = text.IndexOf(")", StringComparison.Ordinal);
				if (num2 < 0 || num2 < num + 3)
				{
					throw new CryptographicException(SecurityResources.GetResourceString("Cryptography_Xml_InvalidReference"));
				}
				text = text.Substring(num + 3, num2 - num - 3);
				text = text.Replace("'", "");
				text = text.Replace("\"", "");
			}
			return text;
		}

		// Token: 0x060003D8 RID: 984 RVA: 0x00012D58 File Offset: 0x00010F58
		internal static void RemoveAllChildren(XmlElement inputElement)
		{
			XmlNode nextSibling;
			for (XmlNode xmlNode = inputElement.FirstChild; xmlNode != null; xmlNode = nextSibling)
			{
				nextSibling = xmlNode.NextSibling;
				inputElement.RemoveChild(xmlNode);
			}
		}

		// Token: 0x060003D9 RID: 985 RVA: 0x00012D84 File Offset: 0x00010F84
		internal static long Pump(Stream input, Stream output)
		{
			MemoryStream memoryStream = input as MemoryStream;
			if (memoryStream != null && memoryStream.Position == 0L)
			{
				memoryStream.WriteTo(output);
				return memoryStream.Length;
			}
			byte[] buffer = new byte[4096];
			long num = 0L;
			int num2;
			while ((num2 = input.Read(buffer, 0, 4096)) > 0)
			{
				output.Write(buffer, 0, num2);
				num += (long)num2;
			}
			return num;
		}

		// Token: 0x060003DA RID: 986 RVA: 0x00012DE4 File Offset: 0x00010FE4
		internal static Hashtable TokenizePrefixListString(string s)
		{
			Hashtable hashtable = new Hashtable();
			if (s != null)
			{
				string[] array = s.Split(null);
				foreach (string text in array)
				{
					if (text.Equals("#default"))
					{
						hashtable.Add(string.Empty, true);
					}
					else if (text.Length > 0)
					{
						hashtable.Add(text, true);
					}
				}
			}
			return hashtable;
		}

		// Token: 0x060003DB RID: 987 RVA: 0x00012E54 File Offset: 0x00011054
		internal static string EscapeWhitespaceData(string data)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(data);
			Utils.SBReplaceCharWithString(stringBuilder, '\r', "&#xD;");
			return stringBuilder.ToString();
		}

		// Token: 0x060003DC RID: 988 RVA: 0x00012E84 File Offset: 0x00011084
		internal static string EscapeTextData(string data)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(data);
			stringBuilder.Replace("&", "&amp;");
			stringBuilder.Replace("<", "&lt;");
			stringBuilder.Replace(">", "&gt;");
			Utils.SBReplaceCharWithString(stringBuilder, '\r', "&#xD;");
			return stringBuilder.ToString();
		}

		// Token: 0x060003DD RID: 989 RVA: 0x00012EE5 File Offset: 0x000110E5
		internal static string EscapeCData(string data)
		{
			return Utils.EscapeTextData(data);
		}

		// Token: 0x060003DE RID: 990 RVA: 0x00012EF0 File Offset: 0x000110F0
		internal static string EscapeAttributeValue(string value)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(value);
			stringBuilder.Replace("&", "&amp;");
			stringBuilder.Replace("<", "&lt;");
			stringBuilder.Replace("\"", "&quot;");
			Utils.SBReplaceCharWithString(stringBuilder, '\t', "&#x9;");
			Utils.SBReplaceCharWithString(stringBuilder, '\n', "&#xA;");
			Utils.SBReplaceCharWithString(stringBuilder, '\r', "&#xD;");
			return stringBuilder.ToString();
		}

		// Token: 0x060003DF RID: 991 RVA: 0x00012F6C File Offset: 0x0001116C
		internal static XmlDocument GetOwnerDocument(XmlNodeList nodeList)
		{
			foreach (object obj in nodeList)
			{
				XmlNode xmlNode = (XmlNode)obj;
				if (xmlNode.OwnerDocument != null)
				{
					return xmlNode.OwnerDocument;
				}
			}
			return null;
		}

		// Token: 0x060003E0 RID: 992 RVA: 0x00012FD0 File Offset: 0x000111D0
		internal static void AddNamespaces(XmlElement elem, CanonicalXmlNodeList namespaces)
		{
			if (namespaces != null)
			{
				foreach (object obj in namespaces)
				{
					XmlNode xmlNode = (XmlNode)obj;
					string text = (xmlNode.Prefix.Length > 0) ? (xmlNode.Prefix + ":" + xmlNode.LocalName) : xmlNode.LocalName;
					if (!elem.HasAttribute(text) && (!text.Equals("xmlns") || elem.Prefix.Length != 0))
					{
						XmlAttribute xmlAttribute = elem.OwnerDocument.CreateAttribute(text);
						xmlAttribute.Value = xmlNode.Value;
						elem.SetAttributeNode(xmlAttribute);
					}
				}
			}
		}

		// Token: 0x060003E1 RID: 993 RVA: 0x0001309C File Offset: 0x0001129C
		internal static void AddNamespaces(XmlElement elem, Hashtable namespaces)
		{
			if (namespaces != null)
			{
				foreach (object obj in namespaces.Keys)
				{
					string text = (string)obj;
					if (!elem.HasAttribute(text))
					{
						XmlAttribute xmlAttribute = elem.OwnerDocument.CreateAttribute(text);
						xmlAttribute.Value = (namespaces[text] as string);
						elem.SetAttributeNode(xmlAttribute);
					}
				}
			}
		}

		// Token: 0x060003E2 RID: 994 RVA: 0x00013124 File Offset: 0x00011324
		internal static CanonicalXmlNodeList GetPropagatedAttributes(XmlElement elem)
		{
			if (elem == null)
			{
				return null;
			}
			CanonicalXmlNodeList canonicalXmlNodeList = new CanonicalXmlNodeList();
			XmlNode xmlNode = elem;
			if (xmlNode == null)
			{
				return null;
			}
			bool flag = true;
			while (xmlNode != null)
			{
				XmlElement xmlElement = xmlNode as XmlElement;
				if (xmlElement == null)
				{
					xmlNode = xmlNode.ParentNode;
				}
				else
				{
					if (!Utils.IsCommittedNamespace(xmlElement, xmlElement.Prefix, xmlElement.NamespaceURI) && !Utils.IsRedundantNamespace(xmlElement, xmlElement.Prefix, xmlElement.NamespaceURI))
					{
						string name = (xmlElement.Prefix.Length > 0) ? ("xmlns:" + xmlElement.Prefix) : "xmlns";
						XmlAttribute xmlAttribute = elem.OwnerDocument.CreateAttribute(name);
						xmlAttribute.Value = xmlElement.NamespaceURI;
						canonicalXmlNodeList.Add(xmlAttribute);
					}
					if (xmlElement.HasAttributes)
					{
						XmlAttributeCollection attributes = xmlElement.Attributes;
						foreach (object obj in attributes)
						{
							XmlAttribute xmlAttribute2 = (XmlAttribute)obj;
							if (flag && xmlAttribute2.LocalName == "xmlns")
							{
								XmlAttribute xmlAttribute3 = elem.OwnerDocument.CreateAttribute("xmlns");
								xmlAttribute3.Value = xmlAttribute2.Value;
								canonicalXmlNodeList.Add(xmlAttribute3);
								flag = false;
							}
							else if (xmlAttribute2.Prefix == "xmlns" || xmlAttribute2.Prefix == "xml")
							{
								canonicalXmlNodeList.Add(xmlAttribute2);
							}
							else if (xmlAttribute2.NamespaceURI.Length > 0 && !Utils.IsCommittedNamespace(xmlElement, xmlAttribute2.Prefix, xmlAttribute2.NamespaceURI) && !Utils.IsRedundantNamespace(xmlElement, xmlAttribute2.Prefix, xmlAttribute2.NamespaceURI))
							{
								string name2 = (xmlAttribute2.Prefix.Length > 0) ? ("xmlns:" + xmlAttribute2.Prefix) : "xmlns";
								XmlAttribute xmlAttribute4 = elem.OwnerDocument.CreateAttribute(name2);
								xmlAttribute4.Value = xmlAttribute2.NamespaceURI;
								canonicalXmlNodeList.Add(xmlAttribute4);
							}
						}
					}
					xmlNode = xmlNode.ParentNode;
				}
			}
			return canonicalXmlNodeList;
		}

		// Token: 0x060003E3 RID: 995 RVA: 0x00013354 File Offset: 0x00011554
		internal static byte[] ConvertIntToByteArray(int dwInput)
		{
			byte[] array = new byte[8];
			int num = 0;
			if (dwInput == 0)
			{
				return new byte[1];
			}
			int i = dwInput;
			while (i > 0)
			{
				int num2 = i % 256;
				array[num] = (byte)num2;
				i = (i - num2) / 256;
				num++;
			}
			byte[] array2 = new byte[num];
			for (int j = 0; j < num; j++)
			{
				array2[j] = array[num - j - 1];
			}
			return array2;
		}

		// Token: 0x060003E4 RID: 996 RVA: 0x000133C0 File Offset: 0x000115C0
		internal static int GetHexArraySize(byte[] hex)
		{
			int num = hex.Length;
			while (num-- > 0 && hex[num] == 0)
			{
			}
			return num + 1;
		}

		// Token: 0x060003E5 RID: 997 RVA: 0x000133E4 File Offset: 0x000115E4
		[SecuritySafeCritical]
		internal static X509Certificate2Collection BuildBagOfCerts(KeyInfoX509Data keyInfoX509Data, CertUsageType certUsageType)
		{
			X509Certificate2Collection x509Certificate2Collection = new X509Certificate2Collection();
			ArrayList arrayList = (certUsageType == CertUsageType.Decryption) ? new ArrayList() : null;
			if (keyInfoX509Data.Certificates != null)
			{
				foreach (object obj in keyInfoX509Data.Certificates)
				{
					X509Certificate2 x509Certificate = (X509Certificate2)obj;
					if (certUsageType != CertUsageType.Verification)
					{
						if (certUsageType == CertUsageType.Decryption)
						{
							arrayList.Add(new X509IssuerSerial(x509Certificate.IssuerName.Name, x509Certificate.SerialNumber));
						}
					}
					else
					{
						x509Certificate2Collection.Add(x509Certificate);
					}
				}
			}
			if (keyInfoX509Data.SubjectNames == null && keyInfoX509Data.IssuerSerials == null && keyInfoX509Data.SubjectKeyIds == null && arrayList == null)
			{
				return x509Certificate2Collection;
			}
			StorePermission storePermission = new StorePermission(StorePermissionFlags.OpenStore);
			storePermission.Assert();
			X509Store[] array = new X509Store[2];
			string storeName = (certUsageType == CertUsageType.Verification) ? "AddressBook" : "My";
			array[0] = new X509Store(storeName, StoreLocation.CurrentUser);
			array[1] = new X509Store(storeName, StoreLocation.LocalMachine);
			for (int i = 0; i < array.Length; i++)
			{
				if (array[i] != null)
				{
					X509Certificate2Collection x509Certificate2Collection2 = null;
					try
					{
						array[i].Open(OpenFlags.OpenExistingOnly);
						x509Certificate2Collection2 = array[i].Certificates;
						array[i].Close();
						if (keyInfoX509Data.SubjectNames != null)
						{
							foreach (object obj2 in keyInfoX509Data.SubjectNames)
							{
								string findValue = (string)obj2;
								x509Certificate2Collection2 = x509Certificate2Collection2.Find(X509FindType.FindBySubjectDistinguishedName, findValue, false);
							}
						}
						if (keyInfoX509Data.IssuerSerials != null)
						{
							foreach (object obj3 in keyInfoX509Data.IssuerSerials)
							{
								X509IssuerSerial x509IssuerSerial = (X509IssuerSerial)obj3;
								x509Certificate2Collection2 = x509Certificate2Collection2.Find(X509FindType.FindByIssuerDistinguishedName, x509IssuerSerial.IssuerName, false);
								x509Certificate2Collection2 = x509Certificate2Collection2.Find(X509FindType.FindBySerialNumber, x509IssuerSerial.SerialNumber, false);
							}
						}
						if (keyInfoX509Data.SubjectKeyIds != null)
						{
							foreach (object obj4 in keyInfoX509Data.SubjectKeyIds)
							{
								byte[] sArray = (byte[])obj4;
								string findValue2 = X509Utils.EncodeHexString(sArray);
								x509Certificate2Collection2 = x509Certificate2Collection2.Find(X509FindType.FindBySubjectKeyIdentifier, findValue2, false);
							}
						}
						if (arrayList != null)
						{
							foreach (object obj5 in arrayList)
							{
								X509IssuerSerial x509IssuerSerial2 = (X509IssuerSerial)obj5;
								x509Certificate2Collection2 = x509Certificate2Collection2.Find(X509FindType.FindByIssuerDistinguishedName, x509IssuerSerial2.IssuerName, false);
								x509Certificate2Collection2 = x509Certificate2Collection2.Find(X509FindType.FindBySerialNumber, x509IssuerSerial2.SerialNumber, false);
							}
						}
					}
					catch (CryptographicException)
					{
					}
					if (x509Certificate2Collection2 != null)
					{
						x509Certificate2Collection.AddRange(x509Certificate2Collection2);
					}
				}
			}
			return x509Certificate2Collection;
		}

		// Token: 0x04000490 RID: 1168
		private static int? xmlDsigSearchDepth = null;

		// Token: 0x04000491 RID: 1169
		private static long? maxCharactersFromEntities = null;

		// Token: 0x04000492 RID: 1170
		private static bool s_readMaxCharactersInDocument = false;

		// Token: 0x04000493 RID: 1171
		private static long s_maxCharactersInDocument = 0L;

		// Token: 0x04000494 RID: 1172
		private static bool? s_allowAmbiguousReferenceTarget = null;

		// Token: 0x04000495 RID: 1173
		private static bool? s_allowDetachedSignature = null;

		// Token: 0x04000496 RID: 1174
		private static bool s_readRequireNCNameIdentifier = false;

		// Token: 0x04000497 RID: 1175
		private static bool s_requireNCNameIdentifier = true;

		// Token: 0x04000498 RID: 1176
		private static bool s_readMaxTransformsPerReference = false;

		// Token: 0x04000499 RID: 1177
		private static long s_maxTransformsPerReference = 10L;

		// Token: 0x0400049A RID: 1178
		private static bool s_readMaxReferencesPerSignedInfo = false;

		// Token: 0x0400049B RID: 1179
		private static long s_maxReferencesPerSignedInfo = 100L;

		// Token: 0x0400049C RID: 1180
		private static bool s_readAllowAdditionalSignatureNodes = false;

		// Token: 0x0400049D RID: 1181
		private static bool s_allowAdditionalSignatureNodes = false;

		// Token: 0x0400049E RID: 1182
		private static bool s_readSkipSignatureAttributeEnforcement = false;

		// Token: 0x0400049F RID: 1183
		private static bool s_skipSignatureAttributeEnforcement = false;

		// Token: 0x040004A0 RID: 1184
		private static bool s_readAllowBareTypeReference = false;

		// Token: 0x040004A1 RID: 1185
		private static bool s_allowBareTypeReference = false;

		// Token: 0x040004A2 RID: 1186
		private static bool s_readLeaveCipherValueUnchecked = false;

		// Token: 0x040004A3 RID: 1187
		private static bool s_leaveCipherValueUnchecked = false;

		// Token: 0x040004A4 RID: 1188
		private static bool s_readDangerousMaxRecursionDepth = false;

		// Token: 0x040004A5 RID: 1189
		private static int s_DangerousMaxRecursionDepth = 64;

		// Token: 0x040004A6 RID: 1190
		private static bool s_readMaxTransformsPerChain = false;

		// Token: 0x040004A7 RID: 1191
		private static int s_maxTransformsPerChain = 20;

		// Token: 0x040004A8 RID: 1192
		private static bool s_readMaxDecryptedDataElements = false;

		// Token: 0x040004A9 RID: 1193
		private static int s_maxDecryptedDataElements = 100;

		// Token: 0x040004AA RID: 1194
		private static bool s_readEncryptedXmlAllowDangerousTransforms = false;

		// Token: 0x040004AB RID: 1195
		private static bool s_encryptedXmlAllowDangerousTransforms = false;

		// Token: 0x040004AC RID: 1196
		private static bool s_readAllowUnsafeTruncatedHmacSignatureVerification = false;

		// Token: 0x040004AD RID: 1197
		private static bool s_allowUnsafeTruncatedHmacSignatureVerification = false;

		// Token: 0x040004AE RID: 1198
		private static readonly char[] s_invalidChars = new char[]
		{
			',',
			'`',
			'[',
			'*',
			'&',
			'+'
		};
	}
}
