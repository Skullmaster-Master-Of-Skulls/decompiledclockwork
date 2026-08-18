using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml;
using Microsoft.Exchange.WebServices.Data;

namespace Microsoft.Exchange.WebServices.Autodiscover
{
	// Token: 0x02000023 RID: 35
	[EditorBrowsable(EditorBrowsableState.Never)]
	internal sealed class OutlookAccount
	{
		// Token: 0x0600016D RID: 365 RVA: 0x0000722C File Offset: 0x0000622C
		internal OutlookAccount()
		{
			this.protocols = new Dictionary<OutlookProtocolType, OutlookProtocol>();
			this.alternateMailboxes = new AlternateMailboxCollection();
		}

		// Token: 0x0600016E RID: 366 RVA: 0x0000724C File Offset: 0x0000624C
		internal void LoadFromXml(EwsXmlReader reader)
		{
			for (;;)
			{
				reader.Read();
				if (reader.NodeType == XmlNodeType.Element)
				{
					string localName;
					switch (localName = reader.LocalName)
					{
					case "AccountType":
						this.AccountType = reader.ReadElementValue();
						goto IL_19B;
					case "Action":
					{
						string text = reader.ReadElementValue();
						string a;
						if ((a = text) != null)
						{
							if (a == "settings")
							{
								this.ResponseType = AutodiscoverResponseType.Success;
								goto IL_19B;
							}
							if (a == "redirectUrl")
							{
								this.ResponseType = AutodiscoverResponseType.RedirectUrl;
								goto IL_19B;
							}
							if (a == "redirectAddr")
							{
								this.ResponseType = AutodiscoverResponseType.RedirectAddress;
								goto IL_19B;
							}
						}
						this.ResponseType = AutodiscoverResponseType.Error;
						goto IL_19B;
					}
					case "Protocol":
					{
						OutlookProtocol outlookProtocol = new OutlookProtocol();
						outlookProtocol.LoadFromXml(reader);
						if (this.protocols.ContainsKey(outlookProtocol.ProtocolType))
						{
							goto Block_9;
						}
						this.protocols.Add(outlookProtocol.ProtocolType, outlookProtocol);
						goto IL_19B;
					}
					case "RedirectAddr":
					case "RedirectUrl":
						this.RedirectTarget = reader.ReadElementValue();
						goto IL_19B;
					case "AlternateMailboxes":
					{
						AlternateMailbox item = AlternateMailbox.LoadFromXml(reader);
						this.alternateMailboxes.Entries.Add(item);
						goto IL_19B;
					}
					}
					reader.SkipCurrentElement();
				}
				IL_19B:
				if (reader.IsEndElement(XmlNamespace.NotSpecified, "Account"))
				{
					return;
				}
			}
			Block_9:
			throw new ServiceLocalException(Strings.InvalidAutodiscoverServiceResponse);
		}

		// Token: 0x0600016F RID: 367 RVA: 0x00007408 File Offset: 0x00006408
		internal void ConvertToUserSettings(List<UserSettingName> requestedSettings, GetUserSettingsResponse response)
		{
			foreach (OutlookProtocol outlookProtocol in this.protocols.Values)
			{
				outlookProtocol.ConvertToUserSettings(requestedSettings, response);
			}
			if (requestedSettings.Contains(UserSettingName.AlternateMailboxes))
			{
				response.Settings[UserSettingName.AlternateMailboxes] = this.alternateMailboxes;
			}
		}

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x06000170 RID: 368 RVA: 0x00007480 File Offset: 0x00006480
		// (set) Token: 0x06000171 RID: 369 RVA: 0x00007488 File Offset: 0x00006488
		internal string AccountType { get; set; }

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x06000172 RID: 370 RVA: 0x00007491 File Offset: 0x00006491
		// (set) Token: 0x06000173 RID: 371 RVA: 0x00007499 File Offset: 0x00006499
		internal AutodiscoverResponseType ResponseType { get; set; }

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x06000174 RID: 372 RVA: 0x000074A2 File Offset: 0x000064A2
		// (set) Token: 0x06000175 RID: 373 RVA: 0x000074AA File Offset: 0x000064AA
		internal string RedirectTarget { get; set; }

		// Token: 0x04000080 RID: 128
		private const string Settings = "settings";

		// Token: 0x04000081 RID: 129
		private const string RedirectAddr = "redirectAddr";

		// Token: 0x04000082 RID: 130
		private const string RedirectUrl = "redirectUrl";

		// Token: 0x04000083 RID: 131
		private Dictionary<OutlookProtocolType, OutlookProtocol> protocols;

		// Token: 0x04000084 RID: 132
		private AlternateMailboxCollection alternateMailboxes;
	}
}
