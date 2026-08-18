using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Xml;
using Microsoft.Exchange.WebServices.Data;

namespace Microsoft.Exchange.WebServices.Autodiscover
{
	// Token: 0x02000025 RID: 37
	[EditorBrowsable(EditorBrowsableState.Never)]
	internal sealed class OutlookProtocol
	{
		// Token: 0x06000182 RID: 386 RVA: 0x000077C7 File Offset: 0x000067C7
		internal OutlookProtocol()
		{
			this.internalOutlookWebAccessUrls = new WebClientUrlCollection();
			this.externalOutlookWebAccessUrls = new WebClientUrlCollection();
		}

		// Token: 0x06000183 RID: 387 RVA: 0x000077E8 File Offset: 0x000067E8
		internal void LoadFromXml(EwsXmlReader reader)
		{
			do
			{
				reader.Read();
				if (reader.NodeType == XmlNodeType.Element)
				{
					string localName;
					switch (localName = reader.LocalName)
					{
					case "Type":
						this.ProtocolType = OutlookProtocol.ProtocolNameToType(reader.ReadElementValue());
						goto IL_572;
					case "AuthPackage":
						this.authPackage = reader.ReadElementValue();
						goto IL_572;
					case "Server":
						this.server = reader.ReadElementValue();
						goto IL_572;
					case "ServerDN":
						this.serverDN = reader.ReadElementValue();
						goto IL_572;
					case "ServerVersion":
						reader.ReadElementValue();
						goto IL_572;
					case "AD":
						this.activeDirectoryServer = reader.ReadElementValue();
						goto IL_572;
					case "MdbDN":
						this.mailboxDN = reader.ReadElementValue();
						goto IL_572;
					case "EwsUrl":
						this.exchangeWebServicesUrl = reader.ReadElementValue();
						goto IL_572;
					case "EmwsUrl":
						this.exchangeManagementWebServicesUrl = reader.ReadElementValue();
						goto IL_572;
					case "ASUrl":
						this.availabilityServiceUrl = reader.ReadElementValue();
						goto IL_572;
					case "OOFUrl":
						reader.ReadElementValue();
						goto IL_572;
					case "UMUrl":
						this.unifiedMessagingUrl = reader.ReadElementValue();
						goto IL_572;
					case "OABUrl":
						this.offlineAddressBookUrl = reader.ReadElementValue();
						goto IL_572;
					case "PublicFolderServer":
						this.publicFolderServer = reader.ReadElementValue();
						goto IL_572;
					case "Internal":
						OutlookProtocol.LoadWebClientUrlsFromXml(reader, this.internalOutlookWebAccessUrls, reader.LocalName);
						goto IL_572;
					case "External":
						OutlookProtocol.LoadWebClientUrlsFromXml(reader, this.externalOutlookWebAccessUrls, reader.LocalName);
						goto IL_572;
					case "SSL":
					{
						string text = reader.ReadElementValue();
						this.sslEnabled = text.Equals("On", StringComparison.OrdinalIgnoreCase);
						goto IL_572;
					}
					case "SharingUrl":
						this.sharingEnabled = (reader.ReadElementValue().Length > 0);
						goto IL_572;
					case "EcpUrl":
						this.ecpUrl = reader.ReadElementValue();
						goto IL_572;
					case "EcpUrl-um":
						this.ecpUrlUm = reader.ReadElementValue();
						goto IL_572;
					case "EcpUrl-aggr":
						this.ecpUrlAggr = reader.ReadElementValue();
						goto IL_572;
					case "EcpUrl-sms":
						this.ecpUrlSms = reader.ReadElementValue();
						goto IL_572;
					case "EcpUrl-mt":
						this.ecpUrlMt = reader.ReadElementValue();
						goto IL_572;
					case "EcpUrl-ret":
						this.ecpUrlRet = reader.ReadElementValue();
						goto IL_572;
					case "EcpUrl-publish":
						this.ecpUrlPublish = reader.ReadElementValue();
						goto IL_572;
					case "EcpUrl-photo":
						this.ecpUrlPhoto = reader.ReadElementValue();
						goto IL_572;
					case "ExchangeRpcUrl":
						this.exchangeRpcUrl = reader.ReadElementValue();
						goto IL_572;
					case "EwsPartnerUrl":
						this.exchangeWebServicesPartnerUrl = reader.ReadElementValue();
						goto IL_572;
					case "EcpUrl-connect":
						this.ecpUrlConnect = reader.ReadElementValue();
						goto IL_572;
					case "EcpUrl-tm":
						this.ecpUrlTm = reader.ReadElementValue();
						goto IL_572;
					case "EcpUrl-tmCreating":
						this.ecpUrlTmCreating = reader.ReadElementValue();
						goto IL_572;
					case "EcpUrl-tmEditing":
						this.ecpUrlTmEditing = reader.ReadElementValue();
						goto IL_572;
					case "EcpUrl-tmHiding":
						this.ecpUrlTmHiding = reader.ReadElementValue();
						goto IL_572;
					case "SiteMailboxCreationURL":
						this.siteMailboxCreationURL = reader.ReadElementValue();
						goto IL_572;
					case "EcpUrl-extinstall":
						this.ecpUrlExtInstall = reader.ReadElementValue();
						goto IL_572;
					case "ServerExclusiveConnect":
					{
						string text2 = reader.ReadElementValue();
						this.serverExclusiveConnect = text2.Equals("On", StringComparison.OrdinalIgnoreCase);
						goto IL_572;
					}
					case "CertPrincipalName":
						this.certPrincipalName = reader.ReadElementValue();
						goto IL_572;
					case "GroupingInformation":
						this.groupingInformation = reader.ReadElementValue();
						goto IL_572;
					}
					reader.SkipCurrentElement();
				}
				IL_572:;
			}
			while (!reader.IsEndElement(XmlNamespace.NotSpecified, "Protocol"));
		}

		// Token: 0x06000184 RID: 388 RVA: 0x00007D78 File Offset: 0x00006D78
		private static OutlookProtocolType ProtocolNameToType(string protocolName)
		{
			OutlookProtocolType result;
			if (!OutlookProtocol.protocolNameToTypeMap.Member.TryGetValue(protocolName, out result))
			{
				result = OutlookProtocolType.Unknown;
			}
			return result;
		}

		// Token: 0x06000185 RID: 389 RVA: 0x00007D9C File Offset: 0x00006D9C
		private static void LoadWebClientUrlsFromXml(EwsXmlReader reader, WebClientUrlCollection webClientUrls, string elementName)
		{
			do
			{
				reader.Read();
				if (reader.NodeType == XmlNodeType.Element)
				{
					string localName;
					if ((localName = reader.LocalName) != null && localName == "OWAUrl")
					{
						string authenticationMethods = reader.ReadAttributeValue("AuthenticationMethod");
						string url = reader.ReadElementValue();
						WebClientUrl item = new WebClientUrl(authenticationMethods, url);
						webClientUrls.Urls.Add(item);
					}
					else
					{
						reader.SkipCurrentElement();
					}
				}
			}
			while (!reader.IsEndElement(XmlNamespace.NotSpecified, elementName));
		}

		// Token: 0x06000186 RID: 390 RVA: 0x00007E08 File Offset: 0x00006E08
		private string ConvertEcpFragmentToUrl(string fragment)
		{
			if (!string.IsNullOrEmpty(this.ecpUrl) && !string.IsNullOrEmpty(fragment))
			{
				return this.ecpUrl + fragment;
			}
			return null;
		}

		// Token: 0x06000187 RID: 391 RVA: 0x00007E4C File Offset: 0x00006E4C
		internal void ConvertToUserSettings(List<UserSettingName> requestedSettings, GetUserSettingsResponse response)
		{
			if (this.ConverterDictionary != null)
			{
				IEnumerable<KeyValuePair<UserSettingName, Func<OutlookProtocol, object>>> enumerable = Enumerable.Where<KeyValuePair<UserSettingName, Func<OutlookProtocol, object>>>(this.ConverterDictionary, (KeyValuePair<UserSettingName, Func<OutlookProtocol, object>> converter) => requestedSettings.Contains(converter.Key));
				foreach (KeyValuePair<UserSettingName, Func<OutlookProtocol, object>> keyValuePair in enumerable)
				{
					object obj = keyValuePair.Value.Invoke(this);
					if (obj != null)
					{
						response.Settings[keyValuePair.Key] = obj;
					}
				}
			}
		}

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x06000188 RID: 392 RVA: 0x00007EF0 File Offset: 0x00006EF0
		// (set) Token: 0x06000189 RID: 393 RVA: 0x00007EF8 File Offset: 0x00006EF8
		internal OutlookProtocolType ProtocolType { get; set; }

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x0600018A RID: 394 RVA: 0x00007F04 File Offset: 0x00006F04
		private Dictionary<UserSettingName, Func<OutlookProtocol, object>> ConverterDictionary
		{
			get
			{
				switch (this.ProtocolType)
				{
				case OutlookProtocolType.Rpc:
					return OutlookProtocol.internalProtocolConverterDictionary.Member;
				case OutlookProtocolType.RpcOverHttp:
					return OutlookProtocol.externalProtocolConverterDictionary.Member;
				case OutlookProtocolType.Web:
					return OutlookProtocol.webProtocolConverterDictionary.Member;
				default:
					return null;
				}
			}
		}

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x0600018B RID: 395 RVA: 0x00007F4E File Offset: 0x00006F4E
		internal static List<UserSettingName> AvailableUserSettings
		{
			get
			{
				return OutlookProtocol.availableUserSettings.Member;
			}
		}

		// Token: 0x0400008D RID: 141
		private const string EXCH = "EXCH";

		// Token: 0x0400008E RID: 142
		private const string EXPR = "EXPR";

		// Token: 0x0400008F RID: 143
		private const string WEB = "WEB";

		// Token: 0x04000090 RID: 144
		private static LazyMember<Dictionary<UserSettingName, Func<OutlookProtocol, object>>> commonProtocolSettings = new LazyMember<Dictionary<UserSettingName, Func<OutlookProtocol, object>>>(delegate()
		{
			Dictionary<UserSettingName, Func<OutlookProtocol, object>> dictionary = new Dictionary<UserSettingName, Func<OutlookProtocol, object>>();
			dictionary.Add(UserSettingName.EcpDeliveryReportUrlFragment, (OutlookProtocol p) => p.ecpUrlMt);
			dictionary.Add(UserSettingName.EcpEmailSubscriptionsUrlFragment, (OutlookProtocol p) => p.ecpUrlAggr);
			dictionary.Add(UserSettingName.EcpPublishingUrlFragment, (OutlookProtocol p) => p.ecpUrlPublish);
			dictionary.Add(UserSettingName.EcpPhotoUrlFragment, (OutlookProtocol p) => p.ecpUrlPhoto);
			dictionary.Add(UserSettingName.EcpRetentionPolicyTagsUrlFragment, (OutlookProtocol p) => p.ecpUrlRet);
			dictionary.Add(UserSettingName.EcpTextMessagingUrlFragment, (OutlookProtocol p) => p.ecpUrlSms);
			dictionary.Add(UserSettingName.EcpVoicemailUrlFragment, (OutlookProtocol p) => p.ecpUrlUm);
			dictionary.Add(UserSettingName.EcpConnectUrlFragment, (OutlookProtocol p) => p.ecpUrlConnect);
			dictionary.Add(UserSettingName.EcpTeamMailboxUrlFragment, (OutlookProtocol p) => p.ecpUrlTm);
			dictionary.Add(UserSettingName.EcpTeamMailboxCreatingUrlFragment, (OutlookProtocol p) => p.ecpUrlTmCreating);
			dictionary.Add(UserSettingName.EcpTeamMailboxEditingUrlFragment, (OutlookProtocol p) => p.ecpUrlTmEditing);
			dictionary.Add(UserSettingName.EcpExtensionInstallationUrlFragment, (OutlookProtocol p) => p.ecpUrlExtInstall);
			dictionary.Add(UserSettingName.SiteMailboxCreationURL, (OutlookProtocol p) => p.siteMailboxCreationURL);
			return dictionary;
		});

		// Token: 0x04000091 RID: 145
		private static LazyMember<Dictionary<UserSettingName, Func<OutlookProtocol, object>>> internalProtocolSettings = new LazyMember<Dictionary<UserSettingName, Func<OutlookProtocol, object>>>(delegate()
		{
			Dictionary<UserSettingName, Func<OutlookProtocol, object>> dictionary = new Dictionary<UserSettingName, Func<OutlookProtocol, object>>();
			dictionary.Add(UserSettingName.ActiveDirectoryServer, (OutlookProtocol p) => p.activeDirectoryServer);
			dictionary.Add(UserSettingName.CrossOrganizationSharingEnabled, (OutlookProtocol p) => p.sharingEnabled.ToString());
			dictionary.Add(UserSettingName.InternalEcpUrl, (OutlookProtocol p) => p.ecpUrl);
			dictionary.Add(UserSettingName.InternalEcpDeliveryReportUrl, (OutlookProtocol p) => p.ConvertEcpFragmentToUrl(p.ecpUrlMt));
			dictionary.Add(UserSettingName.InternalEcpEmailSubscriptionsUrl, (OutlookProtocol p) => p.ConvertEcpFragmentToUrl(p.ecpUrlAggr));
			dictionary.Add(UserSettingName.InternalEcpPublishingUrl, (OutlookProtocol p) => p.ConvertEcpFragmentToUrl(p.ecpUrlPublish));
			dictionary.Add(UserSettingName.InternalEcpPhotoUrl, (OutlookProtocol p) => p.ConvertEcpFragmentToUrl(p.ecpUrlPhoto));
			dictionary.Add(UserSettingName.InternalEcpRetentionPolicyTagsUrl, (OutlookProtocol p) => p.ConvertEcpFragmentToUrl(p.ecpUrlRet));
			dictionary.Add(UserSettingName.InternalEcpTextMessagingUrl, (OutlookProtocol p) => p.ConvertEcpFragmentToUrl(p.ecpUrlSms));
			dictionary.Add(UserSettingName.InternalEcpVoicemailUrl, (OutlookProtocol p) => p.ConvertEcpFragmentToUrl(p.ecpUrlUm));
			dictionary.Add(UserSettingName.InternalEcpConnectUrl, (OutlookProtocol p) => p.ConvertEcpFragmentToUrl(p.ecpUrlConnect));
			dictionary.Add(UserSettingName.InternalEcpTeamMailboxUrl, (OutlookProtocol p) => p.ConvertEcpFragmentToUrl(p.ecpUrlTm));
			dictionary.Add(UserSettingName.InternalEcpTeamMailboxCreatingUrl, (OutlookProtocol p) => p.ConvertEcpFragmentToUrl(p.ecpUrlTmCreating));
			dictionary.Add(UserSettingName.InternalEcpTeamMailboxEditingUrl, (OutlookProtocol p) => p.ConvertEcpFragmentToUrl(p.ecpUrlTmEditing));
			dictionary.Add(UserSettingName.InternalEcpTeamMailboxHidingUrl, (OutlookProtocol p) => p.ConvertEcpFragmentToUrl(p.ecpUrlTmHiding));
			dictionary.Add(UserSettingName.InternalEcpExtensionInstallationUrl, (OutlookProtocol p) => p.ConvertEcpFragmentToUrl(p.ecpUrlExtInstall));
			dictionary.Add(UserSettingName.InternalEwsUrl, (OutlookProtocol p) => p.exchangeWebServicesUrl ?? p.availabilityServiceUrl);
			dictionary.Add(UserSettingName.InternalEmwsUrl, (OutlookProtocol p) => p.exchangeManagementWebServicesUrl);
			dictionary.Add(UserSettingName.InternalMailboxServerDN, (OutlookProtocol p) => p.serverDN);
			dictionary.Add(UserSettingName.InternalRpcClientServer, (OutlookProtocol p) => p.server);
			dictionary.Add(UserSettingName.InternalOABUrl, (OutlookProtocol p) => p.offlineAddressBookUrl);
			dictionary.Add(UserSettingName.InternalUMUrl, (OutlookProtocol p) => p.unifiedMessagingUrl);
			dictionary.Add(UserSettingName.MailboxDN, (OutlookProtocol p) => p.mailboxDN);
			dictionary.Add(UserSettingName.PublicFolderServer, (OutlookProtocol p) => p.publicFolderServer);
			dictionary.Add(UserSettingName.InternalServerExclusiveConnect, (OutlookProtocol p) => p.serverExclusiveConnect);
			return dictionary;
		});

		// Token: 0x04000092 RID: 146
		private static LazyMember<Dictionary<UserSettingName, Func<OutlookProtocol, object>>> externalProtocolSettings = new LazyMember<Dictionary<UserSettingName, Func<OutlookProtocol, object>>>(delegate()
		{
			Dictionary<UserSettingName, Func<OutlookProtocol, object>> dictionary = new Dictionary<UserSettingName, Func<OutlookProtocol, object>>();
			dictionary.Add(UserSettingName.ExternalEcpDeliveryReportUrl, (OutlookProtocol p) => p.ConvertEcpFragmentToUrl(p.ecpUrlRet));
			dictionary.Add(UserSettingName.ExternalEcpEmailSubscriptionsUrl, (OutlookProtocol p) => p.ConvertEcpFragmentToUrl(p.ecpUrlAggr));
			dictionary.Add(UserSettingName.ExternalEcpPublishingUrl, (OutlookProtocol p) => p.ConvertEcpFragmentToUrl(p.ecpUrlPublish));
			dictionary.Add(UserSettingName.ExternalEcpPhotoUrl, (OutlookProtocol p) => p.ConvertEcpFragmentToUrl(p.ecpUrlPhoto));
			dictionary.Add(UserSettingName.ExternalEcpRetentionPolicyTagsUrl, (OutlookProtocol p) => p.ConvertEcpFragmentToUrl(p.ecpUrlRet));
			dictionary.Add(UserSettingName.ExternalEcpTextMessagingUrl, (OutlookProtocol p) => p.ConvertEcpFragmentToUrl(p.ecpUrlSms));
			dictionary.Add(UserSettingName.ExternalEcpUrl, (OutlookProtocol p) => p.ecpUrl);
			dictionary.Add(UserSettingName.ExternalEcpVoicemailUrl, (OutlookProtocol p) => p.ConvertEcpFragmentToUrl(p.ecpUrlUm));
			dictionary.Add(UserSettingName.ExternalEcpConnectUrl, (OutlookProtocol p) => p.ConvertEcpFragmentToUrl(p.ecpUrlConnect));
			dictionary.Add(UserSettingName.ExternalEcpTeamMailboxUrl, (OutlookProtocol p) => p.ConvertEcpFragmentToUrl(p.ecpUrlTm));
			dictionary.Add(UserSettingName.ExternalEcpTeamMailboxCreatingUrl, (OutlookProtocol p) => p.ConvertEcpFragmentToUrl(p.ecpUrlTmCreating));
			dictionary.Add(UserSettingName.ExternalEcpTeamMailboxEditingUrl, (OutlookProtocol p) => p.ConvertEcpFragmentToUrl(p.ecpUrlTmEditing));
			dictionary.Add(UserSettingName.ExternalEcpTeamMailboxHidingUrl, (OutlookProtocol p) => p.ConvertEcpFragmentToUrl(p.ecpUrlTmHiding));
			dictionary.Add(UserSettingName.ExternalEcpExtensionInstallationUrl, (OutlookProtocol p) => p.ConvertEcpFragmentToUrl(p.ecpUrlExtInstall));
			dictionary.Add(UserSettingName.ExternalEwsUrl, (OutlookProtocol p) => p.exchangeWebServicesUrl ?? p.availabilityServiceUrl);
			dictionary.Add(UserSettingName.ExternalEmwsUrl, (OutlookProtocol p) => p.exchangeManagementWebServicesUrl);
			dictionary.Add(UserSettingName.ExternalMailboxServer, (OutlookProtocol p) => p.server);
			dictionary.Add(UserSettingName.ExternalMailboxServerAuthenticationMethods, (OutlookProtocol p) => p.authPackage);
			dictionary.Add(UserSettingName.ExternalMailboxServerRequiresSSL, (OutlookProtocol p) => p.sslEnabled.ToString());
			dictionary.Add(UserSettingName.ExternalOABUrl, (OutlookProtocol p) => p.offlineAddressBookUrl);
			dictionary.Add(UserSettingName.ExternalUMUrl, (OutlookProtocol p) => p.unifiedMessagingUrl);
			dictionary.Add(UserSettingName.ExchangeRpcUrl, (OutlookProtocol p) => p.exchangeRpcUrl);
			dictionary.Add(UserSettingName.EwsPartnerUrl, (OutlookProtocol p) => p.exchangeWebServicesPartnerUrl);
			dictionary.Add(UserSettingName.ExternalServerExclusiveConnect, (OutlookProtocol p) => p.serverExclusiveConnect.ToString());
			dictionary.Add(UserSettingName.CertPrincipalName, (OutlookProtocol p) => p.certPrincipalName);
			dictionary.Add(UserSettingName.GroupingInformation, (OutlookProtocol p) => p.groupingInformation);
			return dictionary;
		});

		// Token: 0x04000093 RID: 147
		private static LazyMember<Dictionary<UserSettingName, Func<OutlookProtocol, object>>> internalProtocolConverterDictionary = new LazyMember<Dictionary<UserSettingName, Func<OutlookProtocol, object>>>(delegate()
		{
			Dictionary<UserSettingName, Func<OutlookProtocol, object>> results = new Dictionary<UserSettingName, Func<OutlookProtocol, object>>();
			OutlookProtocol.commonProtocolSettings.Member.ToList<KeyValuePair<UserSettingName, Func<OutlookProtocol, object>>>().ForEach(delegate(KeyValuePair<UserSettingName, Func<OutlookProtocol, object>> kv)
			{
				results.Add(kv.Key, kv.Value);
			});
			OutlookProtocol.internalProtocolSettings.Member.ToList<KeyValuePair<UserSettingName, Func<OutlookProtocol, object>>>().ForEach(delegate(KeyValuePair<UserSettingName, Func<OutlookProtocol, object>> kv)
			{
				results.Add(kv.Key, kv.Value);
			});
			return results;
		});

		// Token: 0x04000094 RID: 148
		private static LazyMember<Dictionary<UserSettingName, Func<OutlookProtocol, object>>> externalProtocolConverterDictionary = new LazyMember<Dictionary<UserSettingName, Func<OutlookProtocol, object>>>(delegate()
		{
			Dictionary<UserSettingName, Func<OutlookProtocol, object>> results = new Dictionary<UserSettingName, Func<OutlookProtocol, object>>();
			OutlookProtocol.commonProtocolSettings.Member.ToList<KeyValuePair<UserSettingName, Func<OutlookProtocol, object>>>().ForEach(delegate(KeyValuePair<UserSettingName, Func<OutlookProtocol, object>> kv)
			{
				results.Add(kv.Key, kv.Value);
			});
			OutlookProtocol.externalProtocolSettings.Member.ToList<KeyValuePair<UserSettingName, Func<OutlookProtocol, object>>>().ForEach(delegate(KeyValuePair<UserSettingName, Func<OutlookProtocol, object>> kv)
			{
				results.Add(kv.Key, kv.Value);
			});
			return results;
		});

		// Token: 0x04000095 RID: 149
		private static LazyMember<Dictionary<UserSettingName, Func<OutlookProtocol, object>>> webProtocolConverterDictionary = new LazyMember<Dictionary<UserSettingName, Func<OutlookProtocol, object>>>(delegate()
		{
			Dictionary<UserSettingName, Func<OutlookProtocol, object>> dictionary = new Dictionary<UserSettingName, Func<OutlookProtocol, object>>();
			dictionary.Add(UserSettingName.InternalWebClientUrls, (OutlookProtocol p) => p.internalOutlookWebAccessUrls);
			dictionary.Add(UserSettingName.ExternalWebClientUrls, (OutlookProtocol p) => p.externalOutlookWebAccessUrls);
			return dictionary;
		});

		// Token: 0x04000096 RID: 150
		private static LazyMember<List<UserSettingName>> availableUserSettings = new LazyMember<List<UserSettingName>>(delegate()
		{
			List<UserSettingName> list = new List<UserSettingName>();
			list.AddRange(OutlookProtocol.commonProtocolSettings.Member.Keys);
			list.AddRange(OutlookProtocol.internalProtocolSettings.Member.Keys);
			list.AddRange(OutlookProtocol.externalProtocolSettings.Member.Keys);
			list.AddRange(OutlookProtocol.webProtocolConverterDictionary.Member.Keys);
			return list;
		});

		// Token: 0x04000097 RID: 151
		private static LazyMember<Dictionary<string, OutlookProtocolType>> protocolNameToTypeMap = new LazyMember<Dictionary<string, OutlookProtocolType>>(() => new Dictionary<string, OutlookProtocolType>
		{
			{
				"EXCH",
				OutlookProtocolType.Rpc
			},
			{
				"EXPR",
				OutlookProtocolType.RpcOverHttp
			},
			{
				"WEB",
				OutlookProtocolType.Web
			}
		});

		// Token: 0x04000098 RID: 152
		private string activeDirectoryServer;

		// Token: 0x04000099 RID: 153
		private string authPackage;

		// Token: 0x0400009A RID: 154
		private string availabilityServiceUrl;

		// Token: 0x0400009B RID: 155
		private string ecpUrl;

		// Token: 0x0400009C RID: 156
		private string ecpUrlAggr;

		// Token: 0x0400009D RID: 157
		private string ecpUrlMt;

		// Token: 0x0400009E RID: 158
		private string ecpUrlPublish;

		// Token: 0x0400009F RID: 159
		private string ecpUrlPhoto;

		// Token: 0x040000A0 RID: 160
		private string ecpUrlConnect;

		// Token: 0x040000A1 RID: 161
		private string ecpUrlRet;

		// Token: 0x040000A2 RID: 162
		private string ecpUrlSms;

		// Token: 0x040000A3 RID: 163
		private string ecpUrlUm;

		// Token: 0x040000A4 RID: 164
		private string ecpUrlTm;

		// Token: 0x040000A5 RID: 165
		private string ecpUrlTmCreating;

		// Token: 0x040000A6 RID: 166
		private string ecpUrlTmEditing;

		// Token: 0x040000A7 RID: 167
		private string ecpUrlTmHiding;

		// Token: 0x040000A8 RID: 168
		private string siteMailboxCreationURL;

		// Token: 0x040000A9 RID: 169
		private string ecpUrlExtInstall;

		// Token: 0x040000AA RID: 170
		private string exchangeWebServicesUrl;

		// Token: 0x040000AB RID: 171
		private string exchangeManagementWebServicesUrl;

		// Token: 0x040000AC RID: 172
		private string mailboxDN;

		// Token: 0x040000AD RID: 173
		private string offlineAddressBookUrl;

		// Token: 0x040000AE RID: 174
		private string exchangeRpcUrl;

		// Token: 0x040000AF RID: 175
		private string exchangeWebServicesPartnerUrl;

		// Token: 0x040000B0 RID: 176
		private string publicFolderServer;

		// Token: 0x040000B1 RID: 177
		private string server;

		// Token: 0x040000B2 RID: 178
		private string serverDN;

		// Token: 0x040000B3 RID: 179
		private string unifiedMessagingUrl;

		// Token: 0x040000B4 RID: 180
		private bool sharingEnabled;

		// Token: 0x040000B5 RID: 181
		private bool sslEnabled;

		// Token: 0x040000B6 RID: 182
		private bool serverExclusiveConnect;

		// Token: 0x040000B7 RID: 183
		private string certPrincipalName;

		// Token: 0x040000B8 RID: 184
		private string groupingInformation;

		// Token: 0x040000B9 RID: 185
		private WebClientUrlCollection externalOutlookWebAccessUrls;

		// Token: 0x040000BA RID: 186
		private WebClientUrlCollection internalOutlookWebAccessUrls;
	}
}
