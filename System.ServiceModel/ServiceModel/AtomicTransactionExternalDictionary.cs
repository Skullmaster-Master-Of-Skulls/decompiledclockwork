using System;
using System.Xml;

namespace System.ServiceModel
{
	// Token: 0x0200005B RID: 91
	internal class AtomicTransactionExternalDictionary
	{
		// Token: 0x0600025A RID: 602 RVA: 0x0000D054 File Offset: 0x0000B254
		public AtomicTransactionExternalDictionary(ServiceModelDictionary dictionary)
		{
			this.Prefix = dictionary.CreateString("wsat", 383);
			this.Prepare = dictionary.CreateString("Prepare", 387);
			this.Prepared = dictionary.CreateString("Prepared", 388);
			this.ReadOnly = dictionary.CreateString("ReadOnly", 389);
			this.Commit = dictionary.CreateString("Commit", 390);
			this.Rollback = dictionary.CreateString("Rollback", 391);
			this.Committed = dictionary.CreateString("Committed", 392);
			this.Aborted = dictionary.CreateString("Aborted", 393);
			this.Replay = dictionary.CreateString("Replay", 394);
			this.CompletionCoordinatorPortType = dictionary.CreateString("CompletionCoordinatorPortType", 404);
			this.CompletionParticipantPortType = dictionary.CreateString("CompletionParticipantPortType", 405);
			this.CoordinatorPortType = dictionary.CreateString("CoordinatorPortType", 406);
			this.ParticipantPortType = dictionary.CreateString("ParticipantPortType", 407);
			this.InconsistentInternalState = dictionary.CreateString("InconsistentInternalState", 408);
		}

		// Token: 0x040004F7 RID: 1271
		public XmlDictionaryString Prefix;

		// Token: 0x040004F8 RID: 1272
		public XmlDictionaryString Prepare;

		// Token: 0x040004F9 RID: 1273
		public XmlDictionaryString Prepared;

		// Token: 0x040004FA RID: 1274
		public XmlDictionaryString ReadOnly;

		// Token: 0x040004FB RID: 1275
		public XmlDictionaryString Commit;

		// Token: 0x040004FC RID: 1276
		public XmlDictionaryString Rollback;

		// Token: 0x040004FD RID: 1277
		public XmlDictionaryString Committed;

		// Token: 0x040004FE RID: 1278
		public XmlDictionaryString Aborted;

		// Token: 0x040004FF RID: 1279
		public XmlDictionaryString Replay;

		// Token: 0x04000500 RID: 1280
		public XmlDictionaryString CompletionCoordinatorPortType;

		// Token: 0x04000501 RID: 1281
		public XmlDictionaryString CompletionParticipantPortType;

		// Token: 0x04000502 RID: 1282
		public XmlDictionaryString CoordinatorPortType;

		// Token: 0x04000503 RID: 1283
		public XmlDictionaryString ParticipantPortType;

		// Token: 0x04000504 RID: 1284
		public XmlDictionaryString InconsistentInternalState;
	}
}
