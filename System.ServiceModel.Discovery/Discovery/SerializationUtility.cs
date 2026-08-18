using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Text;
using System.Xml;

namespace System.ServiceModel.Discovery
{
	// Token: 0x0200004E RID: 78
	internal static class SerializationUtility
	{
		// Token: 0x060003C5 RID: 965 RVA: 0x0000BB88 File Offset: 0x00009D88
		public static XmlQualifiedName ParseQName(string prefixedQName, XmlReader reader)
		{
			int num = prefixedQName.IndexOf(':');
			string text2;
			string text3;
			if (num != -1)
			{
				string text = prefixedQName.Substring(0, num);
				text2 = reader.LookupNamespace(text);
				if (text2 == null)
				{
					throw FxTrace.Exception.AsError(new XmlException(SR.DiscoveryXmlQNamePrefixNotDefined(text, prefixedQName)));
				}
				text3 = prefixedQName.Substring(num + 1);
				if (text3 == string.Empty)
				{
					throw FxTrace.Exception.AsError(new XmlException(SR.DiscoveryXmlQNameLocalnameNotDefined(prefixedQName)));
				}
			}
			else
			{
				text2 = string.Empty;
				text3 = prefixedQName;
			}
			text3 = XmlConvert.DecodeName(text3);
			return new XmlQualifiedName(text3, text2);
		}

		// Token: 0x060003C6 RID: 966 RVA: 0x0000BC10 File Offset: 0x00009E10
		public static void ParseQNameList(string listOfQNamesAsString, Collection<XmlQualifiedName> qNameCollection, XmlReader reader)
		{
			string[] array = listOfQNamesAsString.Split(SerializationUtility.whiteSpaceChars, StringSplitOptions.RemoveEmptyEntries);
			if (array.Length != 0)
			{
				for (int i = 0; i < array.Length; i++)
				{
					qNameCollection.Add(SerializationUtility.ParseQName(array[i], reader));
				}
			}
		}

		// Token: 0x060003C7 RID: 967 RVA: 0x0000BC4C File Offset: 0x00009E4C
		public static void ParseUriList(string listOfUrisAsString, Collection<Uri> uriCollection, UriKind uriKind)
		{
			string[] array = listOfUrisAsString.Split(SerializationUtility.whiteSpaceChars, StringSplitOptions.RemoveEmptyEntries);
			if (array.Length != 0)
			{
				for (int i = 0; i < array.Length; i++)
				{
					try
					{
						uriCollection.Add(new Uri(array[i], uriKind));
					}
					catch (FormatException innerException)
					{
						if (uriKind == UriKind.Absolute)
						{
							throw FxTrace.Exception.AsError(new XmlException(SR.DiscoveryXmlAbsoluteUriFormatError(array[i]), innerException));
						}
						throw FxTrace.Exception.AsError(new XmlException(SR.DiscoveryXmlUriFormatError(array[i]), innerException));
					}
				}
			}
		}

		// Token: 0x060003C8 RID: 968 RVA: 0x0000BCD4 File Offset: 0x00009ED4
		public static long ReadUInt(string uintString, string notFoundExceptionString, string exceptionString)
		{
			if (uintString == null)
			{
				throw FxTrace.Exception.AsError(new XmlException(notFoundExceptionString));
			}
			long result;
			try
			{
				result = (long)((ulong)XmlConvert.ToUInt32(uintString));
			}
			catch (FormatException innerException)
			{
				throw FxTrace.Exception.AsError(new XmlException(exceptionString, innerException));
			}
			catch (OverflowException innerException2)
			{
				throw FxTrace.Exception.AsError(new XmlException(exceptionString, innerException2));
			}
			return result;
		}

		// Token: 0x060003C9 RID: 969 RVA: 0x0000BD44 File Offset: 0x00009F44
		private static void PrepareQNameString(StringBuilder listOfQNamesString, ref bool emptyNsDeclared, ref int prefixCount, XmlWriter writer, XmlQualifiedName qname)
		{
			string text = XmlConvert.EncodeLocalName(qname.Name.Trim());
			string text2;
			if (qname.Namespace.Length == 0)
			{
				if (!emptyNsDeclared)
				{
					writer.WriteAttributeString("xmlns", string.Empty, null, string.Empty);
					emptyNsDeclared = true;
				}
				text2 = null;
			}
			else
			{
				text2 = writer.LookupPrefix(qname.Namespace);
				if (text2 == null)
				{
					string str = "dp";
					int num = prefixCount;
					prefixCount = num + 1;
					text2 = str + num.ToString();
					writer.WriteAttributeString("xmlns", text2, null, qname.Namespace);
				}
			}
			if (!string.IsNullOrEmpty(text2))
			{
				listOfQNamesString.AppendFormat(CultureInfo.InvariantCulture, "{0}:{1}", new object[]
				{
					text2,
					text
				});
				return;
			}
			listOfQNamesString.AppendFormat(CultureInfo.InvariantCulture, "{0}", new object[]
			{
				text
			});
		}

		// Token: 0x060003CA RID: 970 RVA: 0x0000BE14 File Offset: 0x0000A014
		public static void WriteQName(XmlWriter writer, XmlQualifiedName qname)
		{
			StringBuilder stringBuilder = new StringBuilder();
			int num = 0;
			bool flag = false;
			SerializationUtility.PrepareQNameString(stringBuilder, ref flag, ref num, writer, qname);
			writer.WriteString(stringBuilder.ToString());
		}

		// Token: 0x060003CB RID: 971 RVA: 0x0000BE44 File Offset: 0x0000A044
		public static void WriteListOfQNames(XmlWriter writer, Collection<XmlQualifiedName> qnames)
		{
			int num = 0;
			bool flag = false;
			StringBuilder stringBuilder = new StringBuilder();
			foreach (XmlQualifiedName qname in qnames)
			{
				if (stringBuilder.Length != 0)
				{
					stringBuilder.Append(' ');
				}
				SerializationUtility.PrepareQNameString(stringBuilder, ref flag, ref num, writer, qname);
			}
			writer.WriteString(stringBuilder.ToString());
		}

		// Token: 0x060003CC RID: 972 RVA: 0x0000BEBC File Offset: 0x0000A0BC
		public static void WriteListOfUris(XmlWriter writer, Collection<Uri> uris)
		{
			if (uris.Count > 0)
			{
				for (int i = 0; i < uris.Count - 1; i++)
				{
					writer.WriteString(uris[i].GetComponents(UriComponents.SerializationInfoString, UriFormat.UriEscaped));
					writer.WriteWhitespace(" ");
				}
				writer.WriteString(uris[uris.Count - 1].GetComponents(UriComponents.SerializationInfoString, UriFormat.UriEscaped));
			}
		}

		// Token: 0x060003CD RID: 973 RVA: 0x0000BF28 File Offset: 0x0000A128
		public static int ReadMaxResults(XmlReader reader)
		{
			int num = int.MaxValue;
			if (reader.IsEmptyElement)
			{
				reader.Read();
			}
			else
			{
				reader.ReadStartElement();
				num = reader.ReadContentAsInt();
				if (num <= 0)
				{
					throw FxTrace.Exception.AsError(new XmlException(SR.DiscoveryXmlMaxResultsLessThanZero(num)));
				}
				reader.ReadEndElement();
			}
			return num;
		}

		// Token: 0x060003CE RID: 974 RVA: 0x0000BF80 File Offset: 0x0000A180
		public static TimeSpan ReadDuration(XmlReader reader)
		{
			TimeSpan timeSpan = TimeSpan.MaxValue;
			if (reader.IsEmptyElement)
			{
				reader.Read();
			}
			else
			{
				reader.ReadStartElement();
				string text = reader.ReadString();
				timeSpan = SerializationUtility.ReadTimespan(text, SR.DiscoveryXmlDurationDeserializationError(text));
				if (timeSpan <= TimeSpan.Zero)
				{
					throw FxTrace.Exception.AsError(new XmlException(SR.DiscoveryXmlDurationLessThanZero(timeSpan)));
				}
				reader.ReadEndElement();
			}
			return timeSpan;
		}

		// Token: 0x060003CF RID: 975 RVA: 0x0000BFF0 File Offset: 0x0000A1F0
		public static TimeSpan ReadTimespan(string timespanString, string exceptionString)
		{
			TimeSpan result;
			try
			{
				result = XmlConvert.ToTimeSpan(timespanString);
			}
			catch (FormatException innerException)
			{
				throw FxTrace.Exception.AsError(new XmlException(exceptionString, innerException));
			}
			catch (OverflowException innerException2)
			{
				throw FxTrace.Exception.AsError(new XmlException(exceptionString, innerException2));
			}
			return result;
		}

		// Token: 0x060003D0 RID: 976 RVA: 0x0000C04C File Offset: 0x0000A24C
		public static EndpointAddress ReadEndpointAddress(DiscoveryVersion discoveryVersion, XmlReader reader)
		{
			if (discoveryVersion == DiscoveryVersion.WSDiscoveryApril2005 || discoveryVersion == DiscoveryVersion.WSDiscoveryCD1)
			{
				EndpointAddressAugust2004 endpointAddressAugust = discoveryVersion.Implementation.EprSerializer.ReadObject(reader) as EndpointAddressAugust2004;
				if (endpointAddressAugust == null)
				{
					throw FxTrace.Exception.AsError(new XmlException(SR.DiscoveryXmlEndpointNull));
				}
				return endpointAddressAugust.ToEndpointAddress();
			}
			else
			{
				if (discoveryVersion != DiscoveryVersion.WSDiscovery11)
				{
					return null;
				}
				EndpointAddress10 endpointAddress = discoveryVersion.Implementation.EprSerializer.ReadObject(reader) as EndpointAddress10;
				if (endpointAddress == null)
				{
					throw FxTrace.Exception.AsError(new XmlException(SR.DiscoveryXmlEndpointNull));
				}
				return endpointAddress.ToEndpointAddress();
			}
		}

		// Token: 0x060003D1 RID: 977 RVA: 0x0000C0E0 File Offset: 0x0000A2E0
		public static void ReadContractTypeNames(Collection<XmlQualifiedName> contractTypeNames, XmlReader reader)
		{
			if (reader.IsEmptyElement)
			{
				reader.Read();
				return;
			}
			reader.ReadStartElement();
			string text = reader.ReadString();
			if (!string.IsNullOrEmpty(text))
			{
				SerializationUtility.ParseQNameList(text, contractTypeNames, reader);
			}
			reader.ReadEndElement();
		}

		// Token: 0x060003D2 RID: 978 RVA: 0x0000C120 File Offset: 0x0000A320
		public static Uri ReadScopes(Collection<Uri> scopes, XmlReader reader)
		{
			Uri result = null;
			if (reader.HasAttributes)
			{
				while (reader.MoveToNextAttribute())
				{
					if (reader.NamespaceURI.Length == 0 && reader.Name.Equals("MatchBy"))
					{
						string value = reader.Value;
						try
						{
							result = new Uri(value, UriKind.RelativeOrAbsolute);
							break;
						}
						catch (FormatException innerException)
						{
							throw FxTrace.Exception.AsError(new XmlException(SR.DiscoveryXmlUriFormatError(value), innerException));
						}
					}
				}
				reader.MoveToElement();
			}
			if (reader.IsEmptyElement)
			{
				reader.Read();
			}
			else
			{
				reader.ReadStartElement();
				string text = reader.ReadString();
				if (!string.IsNullOrEmpty(text))
				{
					SerializationUtility.ParseUriList(text, scopes, UriKind.Absolute);
				}
				reader.ReadEndElement();
			}
			return result;
		}

		// Token: 0x060003D3 RID: 979 RVA: 0x0000C1D8 File Offset: 0x0000A3D8
		public static void ReadListenUris(Collection<Uri> listenUris, XmlReader reader)
		{
			if (reader.IsEmptyElement)
			{
				reader.Read();
				return;
			}
			reader.ReadStartElement();
			string text = reader.ReadString();
			if (!string.IsNullOrEmpty(text))
			{
				SerializationUtility.ParseUriList(text, listenUris, UriKind.RelativeOrAbsolute);
			}
			reader.ReadEndElement();
		}

		// Token: 0x060003D4 RID: 980 RVA: 0x0000C218 File Offset: 0x0000A418
		public static int ReadMetadataVersion(XmlReader reader)
		{
			reader.ReadStartElement();
			int num = reader.ReadContentAsInt();
			if (num < 0)
			{
				throw FxTrace.Exception.AsError(new XmlException(SR.DiscoveryXmlMetadataVersionLessThanZero(num)));
			}
			reader.ReadEndElement();
			return num;
		}

		// Token: 0x060003D5 RID: 981 RVA: 0x0000C258 File Offset: 0x0000A458
		public static void WriteEndPointAddress(DiscoveryVersion discoveryVersion, EndpointAddress endpointAddress, XmlWriter writer)
		{
			if (discoveryVersion == DiscoveryVersion.WSDiscoveryApril2005 || discoveryVersion == DiscoveryVersion.WSDiscoveryCD1)
			{
				EndpointAddressAugust2004 graph = EndpointAddressAugust2004.FromEndpointAddress(endpointAddress);
				discoveryVersion.Implementation.EprSerializer.WriteObject(writer, graph);
				return;
			}
			if (discoveryVersion == DiscoveryVersion.WSDiscovery11)
			{
				EndpointAddress10 graph2 = EndpointAddress10.FromEndpointAddress(endpointAddress);
				discoveryVersion.Implementation.EprSerializer.WriteObject(writer, graph2);
			}
		}

		// Token: 0x060003D6 RID: 982 RVA: 0x0000C2B0 File Offset: 0x0000A4B0
		public static void WriteContractTypeNames(DiscoveryVersion discoveryVersion, Collection<XmlQualifiedName> contractTypeNames, XmlWriter writer)
		{
			if (contractTypeNames != null && contractTypeNames.Count > 0)
			{
				writer.WriteStartElement("d", "Types", discoveryVersion.Namespace);
				SerializationUtility.WriteListOfQNames(writer, contractTypeNames);
				writer.WriteEndElement();
			}
		}

		// Token: 0x060003D7 RID: 983 RVA: 0x0000C2E4 File Offset: 0x0000A4E4
		public static void WriteScopes(DiscoveryVersion discoveryVersion, Collection<Uri> scopes, Uri scopeMatchBy, XmlWriter writer)
		{
			bool flag = true;
			if (scopes == null || scopes.Count == 0)
			{
				flag = (scopeMatchBy == FindCriteria.ScopeMatchByNone);
			}
			if (flag)
			{
				writer.WriteStartElement("Scopes", discoveryVersion.Namespace);
				if (scopeMatchBy != null)
				{
					Uri uri = discoveryVersion.Implementation.ToVersionDependentScopeMatchBy(scopeMatchBy);
					writer.WriteAttributeString("MatchBy", string.Empty, uri.GetComponents(UriComponents.SerializationInfoString, UriFormat.UriEscaped));
				}
				if (scopes != null)
				{
					SerializationUtility.WriteListOfUris(writer, scopes);
				}
				writer.WriteEndElement();
			}
		}

		// Token: 0x060003D8 RID: 984 RVA: 0x0000C360 File Offset: 0x0000A560
		public static void WriteListenUris(DiscoveryVersion discoveryVersion, Collection<Uri> listenUris, XmlWriter writer)
		{
			if (listenUris != null && listenUris.Count > 0)
			{
				writer.WriteStartElement("XAddrs", discoveryVersion.Namespace);
				SerializationUtility.WriteListOfUris(writer, listenUris);
				writer.WriteEndElement();
			}
		}

		// Token: 0x060003D9 RID: 985 RVA: 0x0000C38C File Offset: 0x0000A58C
		public static void WriteMetadataVersion(DiscoveryVersion discoveryVersion, int metadataVersion, XmlWriter writer)
		{
			writer.WriteStartElement("MetadataVersion", discoveryVersion.Namespace);
			writer.WriteValue(metadataVersion);
			writer.WriteEndElement();
		}

		// Token: 0x040000FF RID: 255
		private static char[] whiteSpaceChars = new char[]
		{
			' ',
			'\t',
			'\n',
			'\r'
		};
	}
}
