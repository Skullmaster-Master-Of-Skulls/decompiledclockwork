using System;
using System.Xml.XPath;

namespace System.Xml
{
	// Token: 0x02000078 RID: 120
	internal static class ValidateNames
	{
		// Token: 0x060003F0 RID: 1008 RVA: 0x0000F47C File Offset: 0x0000D67C
		internal unsafe static int ParseNmtoken(string s, int offset)
		{
			int num = offset;
			while (num < s.Length && (ValidateNames.xmlCharType.charProperties[s[num]] & 8) != 0)
			{
				num++;
			}
			return num - offset;
		}

		// Token: 0x060003F1 RID: 1009 RVA: 0x0000F4B4 File Offset: 0x0000D6B4
		internal unsafe static int ParseNmtokenNoNamespaces(string s, int offset)
		{
			int num = offset;
			while (num < s.Length && ((ValidateNames.xmlCharType.charProperties[s[num]] & 8) != 0 || s[num] == ':'))
			{
				num++;
			}
			return num - offset;
		}

		// Token: 0x060003F2 RID: 1010 RVA: 0x0000F4F8 File Offset: 0x0000D6F8
		internal static bool IsNmtokenNoNamespaces(string s)
		{
			int num = ValidateNames.ParseNmtokenNoNamespaces(s, 0);
			return num > 0 && num == s.Length;
		}

		// Token: 0x060003F3 RID: 1011 RVA: 0x0000F51C File Offset: 0x0000D71C
		internal unsafe static int ParseNameNoNamespaces(string s, int offset)
		{
			int num = offset;
			if (num < s.Length)
			{
				if ((ValidateNames.xmlCharType.charProperties[s[num]] & 4) == 0 && s[num] != ':')
				{
					return 0;
				}
				num++;
				while (num < s.Length && ((ValidateNames.xmlCharType.charProperties[s[num]] & 8) != 0 || s[num] == ':'))
				{
					num++;
				}
			}
			return num - offset;
		}

		// Token: 0x060003F4 RID: 1012 RVA: 0x0000F590 File Offset: 0x0000D790
		internal static bool IsNameNoNamespaces(string s)
		{
			int num = ValidateNames.ParseNameNoNamespaces(s, 0);
			return num > 0 && num == s.Length;
		}

		// Token: 0x060003F5 RID: 1013 RVA: 0x0000F5B4 File Offset: 0x0000D7B4
		internal unsafe static int ParseNCName(string s, int offset)
		{
			int num = offset;
			if (num < s.Length)
			{
				if ((ValidateNames.xmlCharType.charProperties[s[num]] & 4) == 0)
				{
					return 0;
				}
				num++;
				while (num < s.Length && (ValidateNames.xmlCharType.charProperties[s[num]] & 8) != 0)
				{
					num++;
				}
			}
			return num - offset;
		}

		// Token: 0x060003F6 RID: 1014 RVA: 0x0000F612 File Offset: 0x0000D812
		internal static int ParseNCName(string s)
		{
			return ValidateNames.ParseNCName(s, 0);
		}

		// Token: 0x060003F7 RID: 1015 RVA: 0x0000F61B File Offset: 0x0000D81B
		internal static string ParseNCNameThrow(string s)
		{
			ValidateNames.ParseNCNameInternal(s, true);
			return s;
		}

		// Token: 0x060003F8 RID: 1016 RVA: 0x0000F628 File Offset: 0x0000D828
		private static bool ParseNCNameInternal(string s, bool throwOnError)
		{
			int num = ValidateNames.ParseNCName(s, 0);
			if (num == 0 || num != s.Length)
			{
				if (throwOnError)
				{
					ValidateNames.ThrowInvalidName(s, 0, num);
				}
				return false;
			}
			return true;
		}

		// Token: 0x060003F9 RID: 1017 RVA: 0x0000F658 File Offset: 0x0000D858
		internal static int ParseQName(string s, int offset, out int colonOffset)
		{
			colonOffset = 0;
			int num = ValidateNames.ParseNCName(s, offset);
			if (num != 0)
			{
				offset += num;
				if (offset < s.Length && s[offset] == ':')
				{
					int num2 = ValidateNames.ParseNCName(s, offset + 1);
					if (num2 != 0)
					{
						colonOffset = offset;
						num += num2 + 1;
					}
				}
			}
			return num;
		}

		// Token: 0x060003FA RID: 1018 RVA: 0x0000F6A4 File Offset: 0x0000D8A4
		internal static void ParseQNameThrow(string s, out string prefix, out string localName)
		{
			int num2;
			int num = ValidateNames.ParseQName(s, 0, out num2);
			if (num == 0 || num != s.Length)
			{
				ValidateNames.ThrowInvalidName(s, 0, num);
			}
			if (num2 != 0)
			{
				prefix = s.Substring(0, num2);
				localName = s.Substring(num2 + 1);
				return;
			}
			prefix = "";
			localName = s;
		}

		// Token: 0x060003FB RID: 1019 RVA: 0x0000F6F4 File Offset: 0x0000D8F4
		internal static void ParseNameTestThrow(string s, out string prefix, out string localName)
		{
			int num;
			if (s.Length != 0 && s[0] == '*')
			{
				string text;
				localName = (text = null);
				prefix = text;
				num = 1;
			}
			else
			{
				num = ValidateNames.ParseNCName(s, 0);
				if (num != 0)
				{
					localName = s.Substring(0, num);
					if (num < s.Length && s[num] == ':')
					{
						prefix = localName;
						int num2 = num + 1;
						if (num2 < s.Length && s[num2] == '*')
						{
							localName = null;
							num += 2;
						}
						else
						{
							int num3 = ValidateNames.ParseNCName(s, num2);
							if (num3 != 0)
							{
								localName = s.Substring(num2, num3);
								num += num3 + 1;
							}
						}
					}
					else
					{
						prefix = string.Empty;
					}
				}
				else
				{
					string text;
					localName = (text = null);
					prefix = text;
				}
			}
			if (num == 0 || num != s.Length)
			{
				ValidateNames.ThrowInvalidName(s, 0, num);
			}
		}

		// Token: 0x060003FC RID: 1020 RVA: 0x0000F7B0 File Offset: 0x0000D9B0
		internal static void ThrowInvalidName(string s, int offsetStartChar, int offsetBadChar)
		{
			if (offsetStartChar >= s.Length)
			{
				throw new XmlException("Xml_EmptyName", string.Empty);
			}
			if (ValidateNames.xmlCharType.IsNCNameSingleChar(s[offsetBadChar]) && !XmlCharType.Instance.IsStartNCNameSingleChar(s[offsetBadChar]))
			{
				throw new XmlException("Xml_BadStartNameChar", XmlException.BuildCharExceptionArgs(s, offsetBadChar));
			}
			throw new XmlException("Xml_BadNameChar", XmlException.BuildCharExceptionArgs(s, offsetBadChar));
		}

		// Token: 0x060003FD RID: 1021 RVA: 0x0000F824 File Offset: 0x0000DA24
		internal static Exception GetInvalidNameException(string s, int offsetStartChar, int offsetBadChar)
		{
			if (offsetStartChar >= s.Length)
			{
				return new XmlException("Xml_EmptyName", string.Empty);
			}
			if (ValidateNames.xmlCharType.IsNCNameSingleChar(s[offsetBadChar]) && !ValidateNames.xmlCharType.IsStartNCNameSingleChar(s[offsetBadChar]))
			{
				return new XmlException("Xml_BadStartNameChar", XmlException.BuildCharExceptionArgs(s, offsetBadChar));
			}
			return new XmlException("Xml_BadNameChar", XmlException.BuildCharExceptionArgs(s, offsetBadChar));
		}

		// Token: 0x060003FE RID: 1022 RVA: 0x0000F894 File Offset: 0x0000DA94
		internal static bool StartsWithXml(string s)
		{
			return s.Length >= 3 && (s[0] == 'x' || s[0] == 'X') && (s[1] == 'm' || s[1] == 'M') && (s[2] == 'l' || s[2] == 'L');
		}

		// Token: 0x060003FF RID: 1023 RVA: 0x0000F8F5 File Offset: 0x0000DAF5
		internal static bool IsReservedNamespace(string s)
		{
			return s.Equals("http://www.w3.org/XML/1998/namespace") || s.Equals("http://www.w3.org/2000/xmlns/");
		}

		// Token: 0x06000400 RID: 1024 RVA: 0x0000F911 File Offset: 0x0000DB11
		internal static void ValidateNameThrow(string prefix, string localName, string ns, XPathNodeType nodeKind, ValidateNames.Flags flags)
		{
			ValidateNames.ValidateNameInternal(prefix, localName, ns, nodeKind, flags, true);
		}

		// Token: 0x06000401 RID: 1025 RVA: 0x0000F920 File Offset: 0x0000DB20
		internal static bool ValidateName(string prefix, string localName, string ns, XPathNodeType nodeKind, ValidateNames.Flags flags)
		{
			return ValidateNames.ValidateNameInternal(prefix, localName, ns, nodeKind, flags, false);
		}

		// Token: 0x06000402 RID: 1026 RVA: 0x0000F930 File Offset: 0x0000DB30
		private static bool ValidateNameInternal(string prefix, string localName, string ns, XPathNodeType nodeKind, ValidateNames.Flags flags, bool throwOnError)
		{
			if ((flags & ValidateNames.Flags.NCNames) != (ValidateNames.Flags)0)
			{
				if (prefix.Length != 0 && !ValidateNames.ParseNCNameInternal(prefix, throwOnError))
				{
					return false;
				}
				if (localName.Length != 0 && !ValidateNames.ParseNCNameInternal(localName, throwOnError))
				{
					return false;
				}
			}
			if ((flags & ValidateNames.Flags.CheckLocalName) != (ValidateNames.Flags)0)
			{
				if (nodeKind != XPathNodeType.Element)
				{
					if (nodeKind != XPathNodeType.Attribute)
					{
						if (nodeKind != XPathNodeType.ProcessingInstruction)
						{
							if (localName.Length == 0)
							{
								goto IL_FA;
							}
							if (throwOnError)
							{
								throw new XmlException("XmlNoNameAllowed", nodeKind.ToString());
							}
							return false;
						}
						else
						{
							if (localName.Length != 0 && (localName.Length != 3 || !ValidateNames.StartsWithXml(localName)))
							{
								goto IL_FA;
							}
							if (throwOnError)
							{
								throw new XmlException("Xml_InvalidPIName", localName);
							}
							return false;
						}
					}
					else if (ns.Length == 0 && localName.Equals("xmlns"))
					{
						if (throwOnError)
						{
							throw new XmlException("XmlBadName", new string[]
							{
								nodeKind.ToString(),
								localName
							});
						}
						return false;
					}
				}
				if (localName.Length == 0)
				{
					if (throwOnError)
					{
						throw new XmlException("Xdom_Empty_LocalName", string.Empty);
					}
					return false;
				}
			}
			IL_FA:
			if ((flags & ValidateNames.Flags.CheckPrefixMapping) != (ValidateNames.Flags)0)
			{
				if (nodeKind - XPathNodeType.Element > 2)
				{
					if (nodeKind != XPathNodeType.ProcessingInstruction)
					{
						if (prefix.Length != 0 || ns.Length != 0)
						{
							if (throwOnError)
							{
								throw new XmlException("XmlNoNameAllowed", nodeKind.ToString());
							}
							return false;
						}
					}
					else if (prefix.Length != 0 || ns.Length != 0)
					{
						if (throwOnError)
						{
							throw new XmlException("Xml_InvalidPIName", ValidateNames.CreateName(prefix, localName));
						}
						return false;
					}
				}
				else if (ns.Length == 0)
				{
					if (prefix.Length != 0)
					{
						if (throwOnError)
						{
							throw new XmlException("Xml_PrefixForEmptyNs", string.Empty);
						}
						return false;
					}
				}
				else if (prefix.Length == 0 && nodeKind == XPathNodeType.Attribute)
				{
					if (throwOnError)
					{
						throw new XmlException("XmlBadName", new string[]
						{
							nodeKind.ToString(),
							localName
						});
					}
					return false;
				}
				else if (prefix.Equals("xml"))
				{
					if (!ns.Equals("http://www.w3.org/XML/1998/namespace"))
					{
						if (throwOnError)
						{
							throw new XmlException("Xml_XmlPrefix", string.Empty);
						}
						return false;
					}
				}
				else if (prefix.Equals("xmlns"))
				{
					if (throwOnError)
					{
						throw new XmlException("Xml_XmlnsPrefix", string.Empty);
					}
					return false;
				}
				else if (ValidateNames.IsReservedNamespace(ns))
				{
					if (throwOnError)
					{
						throw new XmlException("Xml_NamespaceDeclXmlXmlns", string.Empty);
					}
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000403 RID: 1027 RVA: 0x0000FB7D File Offset: 0x0000DD7D
		private static string CreateName(string prefix, string localName)
		{
			if (prefix.Length == 0)
			{
				return localName;
			}
			return prefix + ":" + localName;
		}

		// Token: 0x06000404 RID: 1028 RVA: 0x0000FB98 File Offset: 0x0000DD98
		internal static void SplitQName(string name, out string prefix, out string lname)
		{
			int num = name.IndexOf(':');
			if (-1 == num)
			{
				prefix = string.Empty;
				lname = name;
				return;
			}
			if (num == 0 || name.Length - 1 == num)
			{
				string name2 = "Xml_BadNameChar";
				object[] args = XmlException.BuildCharExceptionArgs(':', '\0');
				throw new ArgumentException(Res.GetString(name2, args), "name");
			}
			prefix = name.Substring(0, num);
			num++;
			lname = name.Substring(num, name.Length - num);
		}

		// Token: 0x040001CB RID: 459
		private static XmlCharType xmlCharType = XmlCharType.Instance;

		// Token: 0x02000311 RID: 785
		internal enum Flags
		{
			// Token: 0x0400147A RID: 5242
			NCNames = 1,
			// Token: 0x0400147B RID: 5243
			CheckLocalName,
			// Token: 0x0400147C RID: 5244
			CheckPrefixMapping = 4,
			// Token: 0x0400147D RID: 5245
			All = 7,
			// Token: 0x0400147E RID: 5246
			AllExceptNCNames = 6,
			// Token: 0x0400147F RID: 5247
			AllExceptPrefixMapping = 3
		}
	}
}
