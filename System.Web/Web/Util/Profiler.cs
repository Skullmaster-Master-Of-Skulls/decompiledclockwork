using System;
using System.Collections;

namespace System.Web.Util
{
	// Token: 0x0200077A RID: 1914
	internal class Profiler
	{
		// Token: 0x06005C71 RID: 23665 RVA: 0x00172C14 File Offset: 0x00171C14
		internal Profiler()
		{
			this._requestsToProfile = 10;
			this._outputMode = TraceMode.SortByTime;
			this._localOnly = true;
			this._mostRecent = false;
			this._requests = new Queue(this._requestsToProfile);
		}

		// Token: 0x170017C3 RID: 6083
		// (get) Token: 0x06005C72 RID: 23666 RVA: 0x00172C4A File Offset: 0x00171C4A
		// (set) Token: 0x06005C73 RID: 23667 RVA: 0x00172C52 File Offset: 0x00171C52
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

		// Token: 0x170017C4 RID: 6084
		// (get) Token: 0x06005C74 RID: 23668 RVA: 0x00172C62 File Offset: 0x00171C62
		// (set) Token: 0x06005C75 RID: 23669 RVA: 0x00172C87 File Offset: 0x00171C87
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

		// Token: 0x170017C5 RID: 6085
		// (get) Token: 0x06005C76 RID: 23670 RVA: 0x00172C90 File Offset: 0x00171C90
		// (set) Token: 0x06005C77 RID: 23671 RVA: 0x00172C98 File Offset: 0x00171C98
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

		// Token: 0x170017C6 RID: 6086
		// (get) Token: 0x06005C78 RID: 23672 RVA: 0x00172CA1 File Offset: 0x00171CA1
		// (set) Token: 0x06005C79 RID: 23673 RVA: 0x00172CA9 File Offset: 0x00171CA9
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

		// Token: 0x170017C7 RID: 6087
		// (get) Token: 0x06005C7A RID: 23674 RVA: 0x00172CB2 File Offset: 0x00171CB2
		// (set) Token: 0x06005C7B RID: 23675 RVA: 0x00172CBA File Offset: 0x00171CBA
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

		// Token: 0x170017C8 RID: 6088
		// (get) Token: 0x06005C7C RID: 23676 RVA: 0x00172CC3 File Offset: 0x00171CC3
		internal bool IsConfigEnabled
		{
			get
			{
				return this._oldEnabled;
			}
		}

		// Token: 0x170017C9 RID: 6089
		// (get) Token: 0x06005C7D RID: 23677 RVA: 0x00172CCB File Offset: 0x00171CCB
		// (set) Token: 0x06005C7E RID: 23678 RVA: 0x00172CD3 File Offset: 0x00171CD3
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

		// Token: 0x170017CA RID: 6090
		// (get) Token: 0x06005C7F RID: 23679 RVA: 0x00172CEB File Offset: 0x00171CEB
		internal int RequestsRemaining
		{
			get
			{
				return this._requestsToProfile - this._requests.Count;
			}
		}

		// Token: 0x06005C80 RID: 23680 RVA: 0x00172CFF File Offset: 0x00171CFF
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

		// Token: 0x06005C81 RID: 23681 RVA: 0x00172D2E File Offset: 0x00171D2E
		internal void StartRequest(HttpContext context)
		{
			context.Trace.VerifyStart();
		}

		// Token: 0x06005C82 RID: 23682 RVA: 0x00172D3C File Offset: 0x00171D3C
		internal void EndRequest(HttpContext context)
		{
			context.Trace.EndRequest();
			if (!this.IsEnabled)
			{
				return;
			}
			lock (this._requests)
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

		// Token: 0x06005C83 RID: 23683 RVA: 0x00172DE4 File Offset: 0x00171DE4
		internal void EndProfiling()
		{
			this._isEnabled = false;
		}

		// Token: 0x06005C84 RID: 23684 RVA: 0x00172DED File Offset: 0x00171DED
		internal IList GetData()
		{
			return this._requests.ToArray();
		}

		// Token: 0x0400316D RID: 12653
		private int _requestsToProfile;

		// Token: 0x0400316E RID: 12654
		private Queue _requests;

		// Token: 0x0400316F RID: 12655
		private bool _pageOutput;

		// Token: 0x04003170 RID: 12656
		private bool _isEnabled;

		// Token: 0x04003171 RID: 12657
		private bool _oldEnabled;

		// Token: 0x04003172 RID: 12658
		private bool _localOnly;

		// Token: 0x04003173 RID: 12659
		private bool _mostRecent;

		// Token: 0x04003174 RID: 12660
		private TraceMode _outputMode;
	}
}
