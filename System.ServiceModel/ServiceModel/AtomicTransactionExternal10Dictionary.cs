using System;
using System.Xml;

namespace System.ServiceModel
{
	// Token: 0x0200005C RID: 92
	internal class AtomicTransactionExternal10Dictionary
	{
		// Token: 0x0600025B RID: 603 RVA: 0x0000D19C File Offset: 0x0000B39C
		public AtomicTransactionExternal10Dictionary(ServiceModelDictionary dictionary)
		{
			this.Namespace = dictionary.CreateString("http://schemas.xmlsoap.org/ws/2004/10/wsat", 382);
			this.CompletionUri = dictionary.CreateString("http://schemas.xmlsoap.org/ws/2004/10/wsat/Completion", 384);
			this.Durable2PCUri = dictionary.CreateString("http://schemas.xmlsoap.org/ws/2004/10/wsat/Durable2PC", 385);
			this.Volatile2PCUri = dictionary.CreateString("http://schemas.xmlsoap.org/ws/2004/10/wsat/Volatile2PC", 386);
			this.CommitAction = dictionary.CreateString("http://schemas.xmlsoap.org/ws/2004/10/wsat/Commit", 395);
			this.RollbackAction = dictionary.CreateString("http://schemas.xmlsoap.org/ws/2004/10/wsat/Rollback", 396);
			this.CommittedAction = dictionary.CreateString("http://schemas.xmlsoap.org/ws/2004/10/wsat/Committed", 397);
			this.AbortedAction = dictionary.CreateString("http://schemas.xmlsoap.org/ws/2004/10/wsat/Aborted", 398);
			this.PrepareAction = dictionary.CreateString("http://schemas.xmlsoap.org/ws/2004/10/wsat/Prepare", 399);
			this.PreparedAction = dictionary.CreateString("http://schemas.xmlsoap.org/ws/2004/10/wsat/Prepared", 400);
			this.ReadOnlyAction = dictionary.CreateString("http://schemas.xmlsoap.org/ws/2004/10/wsat/ReadOnly", 401);
			this.ReplayAction = dictionary.CreateString("http://schemas.xmlsoap.org/ws/2004/10/wsat/Replay", 402);
			this.FaultAction = dictionary.CreateString("http://schemas.xmlsoap.org/ws/2004/10/wsat/fault", 403);
		}

		// Token: 0x04000505 RID: 1285
		public XmlDictionaryString Namespace;

		// Token: 0x04000506 RID: 1286
		public XmlDictionaryString CompletionUri;

		// Token: 0x04000507 RID: 1287
		public XmlDictionaryString Durable2PCUri;

		// Token: 0x04000508 RID: 1288
		public XmlDictionaryString Volatile2PCUri;

		// Token: 0x04000509 RID: 1289
		public XmlDictionaryString CommitAction;

		// Token: 0x0400050A RID: 1290
		public XmlDictionaryString RollbackAction;

		// Token: 0x0400050B RID: 1291
		public XmlDictionaryString CommittedAction;

		// Token: 0x0400050C RID: 1292
		public XmlDictionaryString AbortedAction;

		// Token: 0x0400050D RID: 1293
		public XmlDictionaryString PrepareAction;

		// Token: 0x0400050E RID: 1294
		public XmlDictionaryString PreparedAction;

		// Token: 0x0400050F RID: 1295
		public XmlDictionaryString ReadOnlyAction;

		// Token: 0x04000510 RID: 1296
		public XmlDictionaryString ReplayAction;

		// Token: 0x04000511 RID: 1297
		public XmlDictionaryString FaultAction;
	}
}
