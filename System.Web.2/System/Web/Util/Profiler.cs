using System;
using System.Collections;

namespace System.Web.Util
{
	// Token: 0x02000215 RID: 533
	internal class Profiler
	{
		// Token: 0x060019BB RID: 6587 RVA: 0x000507A0 File Offset: 0x0004E9A0
		internal Profiler()
		{
			this._requestsToProfile = 10;
			this._outputMode = TraceMode.SortByTime;
			this._localOnly = true;
			this._mostRecent = false;
			this._requests = new Queue(this._requestsToProfile);
		}

		// Token: 0x17000767 RID: 1895
		// (get) Token: 0x060019BC RID: 6588 RVA: 0x000507D6 File Offset: 0x0004E9D6
		// (set) Token: 0x060019BD RID: 6589 RVA: 0x000507DE File Offset: 0x0004E9DE
		internal bool IsEnabled
		{
			get
			{
				return this._isEnabled;
			}
			set
			{
				this._isEnabled = value;
				this._oldEnabled = value;
			}
		}

		// Token: 0x17000768 RID: 1896
		// (get) Token: 0x060019BE RID: 6590 RVA: 0x000507EE File Offset: 0x0004E9EE
		// (set) Token: 0x060019BF RID: 6591 RVA: 0x00050813 File Offset: 0x0004EA13
		internal bool PageOutput
		{
			get
			{
				return this._pageOutput && (!this._localOnly || HttpContext.Current.Request.IsLocal);
			}
			set
			{
				this._pageOutput = value;
			}
		}

		// Token: 0x17000769 RID: 1897
		// (get) Token: 0x060019C0 RID: 6592 RVA: 0x0005081C File Offset: 0x0004EA1C
		// (set) Token: 0x060019C1 RID: 6593 RVA: 0x00050824 File Offset: 0x0004EA24
		internal TraceMode OutputMode
		{
			get
			{
				return this._outputMode;
			}
			set
			{
				this._outputMode = value;
			}
		}

		// Token: 0x1700076A RID: 1898
		// (get) Token: 0x060019C2 RID: 6594 RVA: 0x0005082D File Offset: 0x0004EA2D
		// (set) Token: 0x060019C3 RID: 6595 RVA: 0x00050835 File Offset: 0x0004EA35
		internal bool LocalOnly
		{
			get
			{
				return this._localOnly;
			}
			set
			{
				this._localOnly = value;
			}
		}

		// Token: 0x1700076B RID: 1899
		// (get) Token: 0x060019C4 RID: 6596 RVA: 0x0005083E File Offset: 0x0004EA3E
		// (set) Token: 0x060019C5 RID: 6597 RVA: 0x00050846 File Offset: 0x0004EA46
		internal bool MostRecent
		{
			get
			{
				return this._mostRecent;
			}
			set
			{
				this._mostRecent = value;
			}
		}

		// Token: 0x1700076C RID: 1900
		// (get) Token: 0x060019C6 RID: 6598 RVA: 0x0005084F File Offset: 0x0004EA4F
		internal bool IsConfigEnabled
		{
			get
			{
				return this._oldEnabled;
			}
		}

		// Token: 0x1700076D RID: 1901
		// (get) Token: 0x060019C7 RID: 6599 RVA: 0x00050857 File Offset: 0x0004EA57
		// (set) Token: 0x060019C8 RID: 6600 RVA: 0x0005085F File Offset: 0x0004EA5F
		internal int RequestsToProfile
		{
			get
			{
				return this._requestsToProfile;
			}
			set
			{
				if (value > 10000)
				{
					value = 10000;
				}
				this._requestsToProfile = value;
			}
		}

		// Token: 0x1700076E RID: 1902
		// (get) Token: 0x060019C9 RID: 6601 RVA: 0x00050877 File Offset: 0x0004EA77
		internal int RequestsRemaining
		{
			get
			{
				return this._requestsToProfile - this._requests.Count;
			}
		}

		// Token: 0x060019CA RID: 6602 RVA: 0x0005088B File Offset: 0x0004EA8B
		internal void Reset()
		{
			this._requests = new Queue(this._requestsToProfile);
			if (this._requestsToProfile != 0)
			{
				this._isEnabled = this._oldEnabled;
				return;
			}
			this._isEnabled = false;
		}

		// Token: 0x060019CB RID: 6603 RVA: 0x000508BA File Offset: 0x0004EABA
		internal void StartRequest(HttpContext context)
		{
			context.Trace.VerifyStart();
		}

		// Token: 0x060019CC RID: 6604 RVA: 0x000508C8 File Offset: 0x0004EAC8
		internal void EndRequest(HttpContext context)
		{
			context.Trace.EndRequest();
			if (!this.IsEnabled)
			{
				return;
			}
			Queue requests = this._requests;
			lock (requests)
			{
				this._requests.Enqueue(context.Trace.GetData());
				if (this.MostRecent && this._requests.Count > this._requestsToProfile)
				{
					this._requests.Dequeue();
				}
			}
			if (!this.MostRecent && this._requests.Count >= this._requestsToProfile)
			{
				this.EndProfiling();
			}
		}

		// Token: 0x060019CD RID: 6605 RVA: 0x00050974 File Offset: 0x0004EB74
		internal void EndProfiling()
		{
			this._isEnabled = false;
		}

		// Token: 0x060019CE RID: 6606 RVA: 0x0005097D File Offset: 0x0004EB7D
		internal IList GetData()
		{
			return this._requests.ToArray();
		}

		// Token: 0x040017E7 RID: 6119
		private int _requestsToProfile;

		// Token: 0x040017E8 RID: 6120
		private Queue _requests;

		// Token: 0x040017E9 RID: 6121
		private bool _pageOutput;

		// Token: 0x040017EA RID: 6122
		private bool _isEnabled;

		// Token: 0x040017EB RID: 6123
		private bool _oldEnabled;

		// Token: 0x040017EC RID: 6124
		private bool _localOnly;

		// Token: 0x040017ED RID: 6125
		private bool _mostRecent;

		// Token: 0x040017EE RID: 6126
		private TraceMode _outputMode;
	}
}
