using System;
using System.ComponentModel;
using System.Net;

namespace Telerik.Licensing
{
	// Token: 0x0200042F RID: 1071
	internal abstract class Client : IDisposable
	{
		// Token: 0x0600267E RID: 9854 RVA: 0x0007DFD8 File Offset: 0x0007C1D8
		protected Client(Config config, ISerializationService serializationService)
		{
			this._serializationService = serializationService;
			this._config = config;
			this._worker = new BackgroundWorker();
			this._worker.DoWork += this.DoWork;
			this._worker.RunWorkerCompleted += this.RunWorkerCompleted;
		}

		// Token: 0x14000086 RID: 134
		// (add) Token: 0x0600267F RID: 9855 RVA: 0x0007E034 File Offset: 0x0007C234
		// (remove) Token: 0x06002680 RID: 9856 RVA: 0x0007E06C File Offset: 0x0007C26C
		public event RunWorkerCompletedEventHandler RequestCompleted;

		// Token: 0x17000C5C RID: 3164
		// (get) Token: 0x06002681 RID: 9857 RVA: 0x0007E0A1 File Offset: 0x0007C2A1
		protected BackgroundWorker Worker
		{
			get
			{
				return this._worker;
			}
		}

		// Token: 0x17000C5D RID: 3165
		// (get) Token: 0x06002682 RID: 9858 RVA: 0x0007E0A9 File Offset: 0x0007C2A9
		protected Config Config
		{
			get
			{
				return this._config;
			}
		}

		// Token: 0x17000C5E RID: 3166
		// (get) Token: 0x06002683 RID: 9859 RVA: 0x0007E0B1 File Offset: 0x0007C2B1
		protected ISerializationService Serialization
		{
			get
			{
				return this._serializationService;
			}
		}

		// Token: 0x17000C5F RID: 3167
		// (get) Token: 0x06002684 RID: 9860
		protected abstract HttpWebRequest WebClient { get; }

		// Token: 0x06002685 RID: 9861
		public abstract void Post(object payload);

		// Token: 0x06002686 RID: 9862 RVA: 0x0007E0B9 File Offset: 0x0007C2B9
		public void Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x06002687 RID: 9863
		protected abstract void DoWork(object sender, DoWorkEventArgs e);

		// Token: 0x06002688 RID: 9864 RVA: 0x0007E0C4 File Offset: 0x0007C2C4
		protected virtual WebRequest InitializeRequest(Uri endpoint)
		{
			HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(endpoint);
			httpWebRequest.Method = "POST";
			httpWebRequest.Accept = "application/json";
			httpWebRequest.ContentType = "application/json";
			return httpWebRequest;
		}

		// Token: 0x06002689 RID: 9865 RVA: 0x0007E0FF File Offset: 0x0007C2FF
		protected virtual void Dispose(bool disposing)
		{
			if (disposing && this.Worker != null)
			{
				this._worker.Dispose();
				this._worker = null;
			}
		}

		// Token: 0x0600268A RID: 9866 RVA: 0x0007E11E File Offset: 0x0007C31E
		protected void RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			if (this.RequestCompleted != null)
			{
				this.RequestCompleted(sender, e);
			}
		}

		// Token: 0x040009D5 RID: 2517
		protected const string JsonContentType = "application/json";

		// Token: 0x040009D6 RID: 2518
		private readonly ISerializationService _serializationService;

		// Token: 0x040009D7 RID: 2519
		private readonly Config _config;

		// Token: 0x040009D8 RID: 2520
		private BackgroundWorker _worker;
	}
}
