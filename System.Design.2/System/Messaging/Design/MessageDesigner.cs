using System;
using System.Collections;
using System.ComponentModel.Design;
using System.Security.Permissions;

namespace System.Messaging.Design
{
	// Token: 0x0200000C RID: 12
	[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
	public class MessageDesigner : ComponentDesigner
	{
		// Token: 0x06000020 RID: 32 RVA: 0x000032B4 File Offset: 0x000014B4
		protected override void PreFilterProperties(IDictionary properties)
		{
			base.PreFilterProperties(properties);
			RuntimeComponentFilter.FilterProperties(properties, new string[]
			{
				"EncryptionAlgorithm",
				"BodyType",
				"DigitalSignature",
				"UseJournalQueue",
				"SenderCertificate",
				"ConnectorType",
				"TransactionStatusQueue",
				"UseDeadLetterQueue",
				"UseTracing",
				"UseAuthentication",
				"TimeToReachQueue",
				"HashAlgorithm",
				"Priority",
				"BodyStream",
				"DestinationSymmetricKey",
				"AppSpecific",
				"ResponseQueue",
				"AuthenticationProviderName",
				"Recoverable",
				"UseEncryption",
				"AttachSenderId",
				"CorrelationId",
				"AdministrationQueue",
				"AuthenticationProviderType",
				"TimeToBeReceived",
				"AcknowledgeType",
				"Label",
				"Extension"
			}, null);
		}
	}
}
