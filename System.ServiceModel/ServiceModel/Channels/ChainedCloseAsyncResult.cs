using System;
using System.Collections.Generic;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000719 RID: 1817
	internal class ChainedCloseAsyncResult : ChainedAsyncResult
	{
		// Token: 0x060044F5 RID: 17653 RVA: 0x00102D0F File Offset: 0x00100F0F
		public ChainedCloseAsyncResult(TimeSpan timeout, AsyncCallback callback, object state, ChainedBeginHandler begin1, ChainedEndHandler end1, IList<ICommunicationObject> collection) : base(timeout, callback, state)
		{
			this.collection = collection;
			base.Begin(new ChainedBeginHandler(this.BeginClose), new ChainedEndHandler(this.EndClose), begin1, end1);
		}

		// Token: 0x060044F6 RID: 17654 RVA: 0x00102D44 File Offset: 0x00100F44
		public ChainedCloseAsyncResult(TimeSpan timeout, AsyncCallback callback, object state, ChainedBeginHandler begin1, ChainedEndHandler end1, params ICommunicationObject[] objs) : base(timeout, callback, state)
		{
			this.collection = new List<ICommunicationObject>();
			if (objs != null)
			{
				for (int i = 0; i < objs.Length; i++)
				{
					if (objs[i] != null)
					{
						this.collection.Add(objs[i]);
					}
				}
			}
			base.Begin(new ChainedBeginHandler(this.BeginClose), new ChainedEndHandler(this.EndClose), begin1, end1);
		}

		// Token: 0x060044F7 RID: 17655 RVA: 0x00102DAF File Offset: 0x00100FAF
		private IAsyncResult BeginClose(TimeSpan timeout, AsyncCallback callback, object state)
		{
			return new CloseCollectionAsyncResult(timeout, callback, state, this.collection);
		}

		// Token: 0x060044F8 RID: 17656 RVA: 0x00102DBF File Offset: 0x00100FBF
		private void EndClose(IAsyncResult result)
		{
			CloseCollectionAsyncResult.End((CloseCollectionAsyncResult)result);
		}

		// Token: 0x04002D45 RID: 11589
		private IList<ICommunicationObject> collection;
	}
}
