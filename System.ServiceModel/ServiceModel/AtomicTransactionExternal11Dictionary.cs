using System;
using System.Xml;

namespace System.ServiceModel
{
	// Token: 0x0200003E RID: 62
	internal class AtomicTransactionExternal11Dictionary
	{
		// Token: 0x060001F6 RID: 502 RVA: 0x00009770 File Offset: 0x00007970
		public AtomicTransactionExternal11Dictionary(XmlDictionary dictionary)
		{
			this.Namespace = dictionary.Add("http://docs.oasis-open.org/ws-tx/wsat/2006/06");
			this.CompletionUri = dictionary.Add("http://docs.oasis-open.org/ws-tx/wsat/2006/06/Completion");
			this.Durable2PCUri = dictionary.Add("http://docs.oasis-open.org/ws-tx/wsat/2006/06/Durable2PC");
			this.Volatile2PCUri = dictionary.Add("http://docs.oasis-open.org/ws-tx/wsat/2006/06/Volatile2PC");
			this.CommitAction = dictionary.Add("http://docs.oasis-open.org/ws-tx/wsat/2006/06/Commit");
			this.RollbackAction = dictionary.Add("http://docs.oasis-open.org/ws-tx/wsat/2006/06/Rollback");
			this.CommittedAction = dictionary.Add("http://docs.oasis-open.org/ws-tx/wsat/2006/06/Committed");
			this.AbortedAction = dictionary.Add("http://docs.oasis-open.org/ws-tx/wsat/2006/06/Aborted");
			this.PrepareAction = dictionary.Add("http://docs.oasis-open.org/ws-tx/wsat/2006/06/Prepare");
			this.PreparedAction = dictionary.Add("http://docs.oasis-open.org/ws-tx/wsat/2006/06/Prepared");
			this.ReadOnlyAction = dictionary.Add("http://docs.oasis-open.org/ws-tx/wsat/2006/06/ReadOnly");
			this.ReplayAction = dictionary.Add("http://docs.oasis-open.org/ws-tx/wsat/2006/06/Replay");
			this.FaultAction = dictionary.Add("http://docs.oasis-open.org/ws-tx/wsat/2006/06/fault");
			this.UnknownTransaction = dictionary.Add("UnknownTransaction");
		}

		// Token: 0x040001C1 RID: 449
		public XmlDictionaryString Namespace;

		// Token: 0x040001C2 RID: 450
		public XmlDictionaryString CompletionUri;

		// Token: 0x040001C3 RID: 451
		public XmlDictionaryString Durable2PCUri;

		// Token: 0x040001C4 RID: 452
		public XmlDictionaryString Volatile2PCUri;

		// Token: 0x040001C5 RID: 453
		public XmlDictionaryString CommitAction;

		// Token: 0x040001C6 RID: 454
		public XmlDictionaryString RollbackAction;

		// Token: 0x040001C7 RID: 455
		public XmlDictionaryString CommittedAction;

		// Token: 0x040001C8 RID: 456
		public XmlDictionaryString AbortedAction;

		// Token: 0x040001C9 RID: 457
		public XmlDictionaryString PrepareAction;

		// Token: 0x040001CA RID: 458
		public XmlDictionaryString PreparedAction;

		// Token: 0x040001CB RID: 459
		public XmlDictionaryString ReadOnlyAction;

		// Token: 0x040001CC RID: 460
		public XmlDictionaryString ReplayAction;

		// Token: 0x040001CD RID: 461
		public XmlDictionaryString FaultAction;

		// Token: 0x040001CE RID: 462
		public XmlDictionaryString UnknownTransaction;
	}
}
