using System;
using System.Collections.Generic;

namespace System.ServiceModel.Channels
{
	// Token: 0x0200071A RID: 1818
	internal class ChainedOpenAsyncResult : ChainedAsyncResult
	{
		// Token: 0x060044F9 RID: 17657 RVA: 0x00102DCC File Offset: 0x00100FCC
		public ChainedOpenAsyncResult(TimeSpan timeout, AsyncCallback callback, object state, ChainedBeginHandler begin1, ChainedEndHandler end1, IList<ICommunicationObject> collection) : base(timeout, callback, state)
		{
			this.collection = collection;
			base.Begin(begin1, end1, new ChainedBeginHandler(this.BeginOpen), new ChainedEndHandler(this.EndOpen));
		}

		// Token: 0x060044FA RID: 17658 RVA: 0x00102E04 File Offset: 0x00101004
		public ChainedOpenAsyncResult(TimeSpan timeout, AsyncCallback callback, object state, ChainedBeginHandler begin1, ChainedEndHandler end1, params ICommunicationObject[] objs) : base(timeout, callback, state)
		{
			this.collection = new List<ICommunicationObject>();
			for (int i = 0; i < objs.Length; i++)
			{
				if (objs[i] != null)
				{
					this.collection.Add(objs[i]);
				}
			}
			base.Begin(begin1, end1, new ChainedBeginHandler(this.BeginOpen), new ChainedEndHandler(this.EndOpen));
		}

		// Token: 0x060044FB RID: 17659 RVA: 0x00102E6B File Offset: 0x0010106B
		private IAsyncResult BeginOpen(TimeSpan timeout, AsyncCallback callback, object state)
		{
			return new OpenCollectionAsyncResult(timeout, callback, state, this.collection);
		}

		// Token: 0x060044FC RID: 17660 RVA: 0x00102E7B File Offset: 0x0010107B
		private void EndOpen(IAsyncResult result)
		{
			OpenCollectionAsyncResult.End((OpenCollectionAsyncResult)result);
		}

		// Token: 0x04002D46 RID: 11590
		private IList<ICommunicationObject> collection;
	}
}
