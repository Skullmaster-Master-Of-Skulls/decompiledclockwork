using System;
using System.Collections;
using System.ComponentModel.Design;
using System.Security.Permissions;

namespace System.Messaging.Design
{
	// Token: 0x0200054C RID: 1356
	[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
	public class MessageDesigner : ComponentDesigner
	{
		// Token: 0x06002F8E RID: 12174 RVA: 0x0010EAE8 File Offset: 0x0010DAE8
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
