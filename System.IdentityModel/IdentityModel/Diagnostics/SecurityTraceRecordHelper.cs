using System;
using System.Diagnostics;
using System.IdentityModel.Tokens;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Authentication.ExtendedProtection;
using System.Xml;

namespace System.IdentityModel.Diagnostics
{
	// Token: 0x020001E9 RID: 489
	internal static class SecurityTraceRecordHelper
	{
		// Token: 0x06001056 RID: 4182 RVA: 0x000464AD File Offset: 0x000446AD
		internal static void TraceServiceNameBindingOnServer(string serviceBindingNameSentByClient, string defaultServiceBindingNameOfServer, ServiceNameCollection serviceNameCollectionConfiguredOnServer)
		{
			TraceUtility.TraceEvent(TraceEventType.Information, 786436, SR.GetString("TraceCodeServiceBindingCheck"), new SecurityTraceRecordHelper.ServiceBindingNameTraceRecord(serviceBindingNameSentByClient, defaultServiceBindingNameOfServer, serviceNameCollectionConfiguredOnServer), null, null);
		}

		// Token: 0x06001057 RID: 4183 RVA: 0x000464CE File Offset: 0x000446CE
		internal static void TraceChannelBindingInformation(ExtendedProtectionPolicyHelper policyHelper, bool isServer, ChannelBinding channelBinding)
		{
			TraceUtility.TraceEvent(TraceEventType.Information, 786437, SR.GetString("TraceCodeChannelBindingCheck"), new SecurityTraceRecordHelper.ChannelBindingNameTraceRecord(policyHelper, isServer, channelBinding), null, null);
		}

		// Token: 0x020002A8 RID: 680
		private class ServiceBindingNameTraceRecord : SecurityTraceRecord
		{
			// Token: 0x060013BF RID: 5055 RVA: 0x00053CAB File Offset: 0x00051EAB
			public ServiceBindingNameTraceRecord(string serviceBindingNameSentByClient, string defaultServiceBindingNameOfServer, ServiceNameCollection serviceNameCollectionConfiguredOnServer) : base("ServiceBindingCheckAfterSpNego")
			{
				this.serviceBindingNameSentByClient = serviceBindingNameSentByClient;
				this.defaultServiceBindingNameOfServer = defaultServiceBindingNameOfServer;
				this.serviceNameCollectionConfiguredOnServer = serviceNameCollectionConfiguredOnServer;
			}

			// Token: 0x060013C0 RID: 5056 RVA: 0x00053CD0 File Offset: 0x00051ED0
			internal override void WriteTo(XmlWriter xml)
			{
				if (xml == null)
				{
					return;
				}
				xml.WriteComment(SR.GetString("ServiceNameFromClient"));
				xml.WriteElementString("ServiceName", this.serviceBindingNameSentByClient);
				xml.WriteComment(SR.GetString("ServiceNameOnService"));
				xml.WriteStartElement("ServiceNameCollection");
				if (this.serviceNameCollectionConfiguredOnServer == null || this.serviceNameCollectionConfiguredOnServer.Count < 1)
				{
					xml.WriteElementString("ServiceName", this.defaultServiceBindingNameOfServer);
				}
				else
				{
					foreach (object obj in this.serviceNameCollectionConfiguredOnServer)
					{
						string value = (string)obj;
						xml.WriteElementString("ServiceName", value);
					}
				}
				xml.WriteFullEndElement();
			}

			// Token: 0x04001166 RID: 4454
			private string serviceBindingNameSentByClient;

			// Token: 0x04001167 RID: 4455
			private string defaultServiceBindingNameOfServer;

			// Token: 0x04001168 RID: 4456
			private ServiceNameCollection serviceNameCollectionConfiguredOnServer;
		}

		// Token: 0x020002A9 RID: 681
		private class ChannelBindingNameTraceRecord : SecurityTraceRecord
		{
			// Token: 0x060013C1 RID: 5057 RVA: 0x00053DA0 File Offset: 0x00051FA0
			public ChannelBindingNameTraceRecord(ExtendedProtectionPolicyHelper policyHelper, bool isServer, ChannelBinding channelBinding) : base("SpNegoChannelBindingInformation")
			{
				this.policyHelper = policyHelper;
				this.isServer = isServer;
				this.channelBindingUsed = false;
				this.channelBinding = channelBinding;
			}

			// Token: 0x060013C2 RID: 5058 RVA: 0x00053DCC File Offset: 0x00051FCC
			internal override void WriteTo(XmlWriter xml)
			{
				if (xml == null)
				{
					return;
				}
				if (this.policyHelper != null)
				{
					xml.WriteElementString("PolicyEnforcement", this.policyHelper.PolicyEnforcement.ToString());
					xml.WriteElementString("ProtectionScenario", this.policyHelper.ProtectionScenario.ToString());
					xml.WriteStartElement("ServiceNameCollection");
					if (this.policyHelper.ServiceNameCollection != null && this.policyHelper.ServiceNameCollection.Count > 0)
					{
						foreach (object obj in this.policyHelper.ServiceNameCollection)
						{
							string value = (string)obj;
							xml.WriteElementString("ServiceName", value);
						}
					}
					xml.WriteFullEndElement();
					if (this.isServer)
					{
						this.channelBindingUsed = this.policyHelper.ShouldAddChannelBindingToASC();
					}
					else
					{
						this.channelBindingUsed = (this.policyHelper.ChannelBinding != null);
					}
					xml.WriteElementString("ChannelBindingUsed", this.channelBindingUsed.ToString());
					if (this.channelBinding != null && this.policyHelper.PolicyEnforcement != PolicyEnforcement.Never && this.channelBindingUsed)
					{
						ExtendedProtectionPolicy extendedProtectionPolicy = new ExtendedProtectionPolicy(this.policyHelper.PolicyEnforcement, this.channelBinding);
						xml.WriteElementString("ChannelBindingData", this.GetBase64EncodedChannelBindingData(extendedProtectionPolicy));
						return;
					}
				}
				else
				{
					if (this.channelBinding != null)
					{
						xml.WriteElementString("ChannelBindingUsed", "true");
						ExtendedProtectionPolicy extendedProtectionPolicy2 = new ExtendedProtectionPolicy(PolicyEnforcement.WhenSupported, this.channelBinding);
						xml.WriteElementString("ChannelBindingData", this.GetBase64EncodedChannelBindingData(extendedProtectionPolicy2));
						return;
					}
					xml.WriteElementString("ChannelBindingUsed", "false");
					xml.WriteElementString("ChannelBindingData", null);
				}
			}

			// Token: 0x060013C3 RID: 5059 RVA: 0x00053FA8 File Offset: 0x000521A8
			internal string GetBase64EncodedChannelBindingData(ExtendedProtectionPolicy extendedProtectionPolicy)
			{
				MemoryStream memoryStream = new MemoryStream();
				BinaryFormatter binaryFormatter = new BinaryFormatter();
				binaryFormatter.Serialize(memoryStream, extendedProtectionPolicy);
				byte[] buffer = memoryStream.GetBuffer();
				return Convert.ToBase64String(buffer, Base64FormattingOptions.None);
			}

			// Token: 0x04001169 RID: 4457
			private ExtendedProtectionPolicyHelper policyHelper;

			// Token: 0x0400116A RID: 4458
			private bool isServer;

			// Token: 0x0400116B RID: 4459
			private bool channelBindingUsed;

			// Token: 0x0400116C RID: 4460
			private ChannelBinding channelBinding;
		}

		// Token: 0x020002AA RID: 682
		internal class TokenTraceRecord : SecurityTraceRecord
		{
			// Token: 0x060013C4 RID: 5060 RVA: 0x00053FD7 File Offset: 0x000521D7
			public TokenTraceRecord(SecurityToken securityToken) : base("TokenTraceRecord")
			{
				this._securityToken = securityToken;
			}

			// Token: 0x060013C5 RID: 5061 RVA: 0x00053FEC File Offset: 0x000521EC
			private void WriteSessionToken(XmlWriter writer, SessionSecurityToken sessionToken)
			{
				SessionSecurityTokenHandler orCreateSessionSecurityTokenHandler = SecurityTraceRecordHelper.TokenTraceRecord.GetOrCreateSessionSecurityTokenHandler();
				XmlDictionaryWriter writer2 = XmlDictionaryWriter.CreateDictionaryWriter(writer);
				orCreateSessionSecurityTokenHandler.WriteToken(writer2, sessionToken);
			}

			// Token: 0x060013C6 RID: 5062 RVA: 0x00054010 File Offset: 0x00052210
			private static SessionSecurityTokenHandler GetOrCreateSessionSecurityTokenHandler()
			{
				SecurityTokenHandlerCollection securityTokenHandlerCollection = SecurityTokenHandlerCollection.CreateDefaultSecurityTokenHandlerCollection();
				SessionSecurityTokenHandler sessionSecurityTokenHandler = securityTokenHandlerCollection[typeof(SessionSecurityToken)] as SessionSecurityTokenHandler;
				if (sessionSecurityTokenHandler == null)
				{
					sessionSecurityTokenHandler = new SessionSecurityTokenHandler();
					securityTokenHandlerCollection.AddOrReplace(sessionSecurityTokenHandler);
				}
				return sessionSecurityTokenHandler;
			}

			// Token: 0x060013C7 RID: 5063 RVA: 0x0005404C File Offset: 0x0005224C
			internal override void WriteTo(XmlWriter writer)
			{
				writer.WriteStartElement("TokenTraceRecord");
				writer.WriteAttributeString("xmlns", this.EventId);
				writer.WriteStartElement("SecurityToken");
				writer.WriteAttributeString("Type", this._securityToken.GetType().ToString());
				if (this._securityToken is SessionSecurityToken)
				{
					this.WriteSessionToken(writer, this._securityToken as SessionSecurityToken);
				}
				else
				{
					SecurityTokenHandlerCollection securityTokenHandlerCollection = SecurityTokenHandlerCollection.CreateDefaultSecurityTokenHandlerCollection();
					if (securityTokenHandlerCollection.CanWriteToken(this._securityToken))
					{
						securityTokenHandlerCollection.WriteToken(writer, this._securityToken);
					}
					else
					{
						writer.WriteElementString("Warning", SR.GetString("TraceUnableToWriteToken", new object[]
						{
							this._securityToken.GetType().ToString()
						}));
					}
				}
				writer.WriteEndElement();
			}

			// Token: 0x0400116D RID: 4461
			private const string ElementName = "TokenTraceRecord";

			// Token: 0x0400116E RID: 4462
			private SecurityToken _securityToken;
		}
	}
}
