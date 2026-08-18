using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.Http;

namespace System.Web.Http.Tracing
{
	// Token: 0x02000159 RID: 345
	[DebuggerDisplay("Category: {Category}, Operation: {Operation}, Level: {Level}, Kind: {Kind}")]
	public class TraceRecord
	{
		// Token: 0x0600089A RID: 2202 RVA: 0x0001C28C File Offset: 0x0001A48C
		public TraceRecord(HttpRequestMessage request, string category, TraceLevel level)
		{
			this.Timestamp = DateTime.UtcNow;
			this.Request = request;
			this.RequestId = ((request != null) ? request.GetCorrelationId() : Guid.Empty);
			this.Category = category;
			this.Level = level;
		}

		// Token: 0x17000283 RID: 643
		// (get) Token: 0x0600089B RID: 2203 RVA: 0x0001C2FD File Offset: 0x0001A4FD
		// (set) Token: 0x0600089C RID: 2204 RVA: 0x0001C305 File Offset: 0x0001A505
		public string Category { get; set; }

		// Token: 0x17000284 RID: 644
		// (get) Token: 0x0600089D RID: 2205 RVA: 0x0001C30E File Offset: 0x0001A50E
		// (set) Token: 0x0600089E RID: 2206 RVA: 0x0001C316 File Offset: 0x0001A516
		public Exception Exception { get; set; }

		// Token: 0x17000285 RID: 645
		// (get) Token: 0x0600089F RID: 2207 RVA: 0x0001C31F File Offset: 0x0001A51F
		// (set) Token: 0x060008A0 RID: 2208 RVA: 0x0001C327 File Offset: 0x0001A527
		public TraceKind Kind
		{
			get
			{
				return this._traceKind;
			}
			set
			{
				TraceKindHelper.Validate(value, "value");
				this._traceKind = value;
			}
		}

		// Token: 0x17000286 RID: 646
		// (get) Token: 0x060008A1 RID: 2209 RVA: 0x0001C33B File Offset: 0x0001A53B
		// (set) Token: 0x060008A2 RID: 2210 RVA: 0x0001C343 File Offset: 0x0001A543
		public TraceLevel Level
		{
			get
			{
				return this._traceLevel;
			}
			set
			{
				TraceLevelHelper.Validate(value, "value");
				this._traceLevel = value;
			}
		}

		// Token: 0x17000287 RID: 647
		// (get) Token: 0x060008A3 RID: 2211 RVA: 0x0001C357 File Offset: 0x0001A557
		// (set) Token: 0x060008A4 RID: 2212 RVA: 0x0001C35F File Offset: 0x0001A55F
		public string Message { get; set; }

		// Token: 0x17000288 RID: 648
		// (get) Token: 0x060008A5 RID: 2213 RVA: 0x0001C368 File Offset: 0x0001A568
		// (set) Token: 0x060008A6 RID: 2214 RVA: 0x0001C370 File Offset: 0x0001A570
		public string Operation { get; set; }

		// Token: 0x17000289 RID: 649
		// (get) Token: 0x060008A7 RID: 2215 RVA: 0x0001C379 File Offset: 0x0001A579
		// (set) Token: 0x060008A8 RID: 2216 RVA: 0x0001C381 File Offset: 0x0001A581
		public string Operator { get; set; }

		// Token: 0x1700028A RID: 650
		// (get) Token: 0x060008A9 RID: 2217 RVA: 0x0001C38A File Offset: 0x0001A58A
		public Dictionary<object, object> Properties
		{
			get
			{
				return this._properties.Value;
			}
		}

		// Token: 0x1700028B RID: 651
		// (get) Token: 0x060008AA RID: 2218 RVA: 0x0001C397 File Offset: 0x0001A597
		// (set) Token: 0x060008AB RID: 2219 RVA: 0x0001C39F File Offset: 0x0001A59F
		public HttpRequestMessage Request { get; private set; }

		// Token: 0x1700028C RID: 652
		// (get) Token: 0x060008AC RID: 2220 RVA: 0x0001C3A8 File Offset: 0x0001A5A8
		// (set) Token: 0x060008AD RID: 2221 RVA: 0x0001C3B0 File Offset: 0x0001A5B0
		public Guid RequestId { get; private set; }

		// Token: 0x1700028D RID: 653
		// (get) Token: 0x060008AE RID: 2222 RVA: 0x0001C3B9 File Offset: 0x0001A5B9
		// (set) Token: 0x060008AF RID: 2223 RVA: 0x0001C3C1 File Offset: 0x0001A5C1
		public HttpStatusCode Status { get; set; }

		// Token: 0x1700028E RID: 654
		// (get) Token: 0x060008B0 RID: 2224 RVA: 0x0001C3CA File Offset: 0x0001A5CA
		// (set) Token: 0x060008B1 RID: 2225 RVA: 0x0001C3D2 File Offset: 0x0001A5D2
		public DateTime Timestamp { get; private set; }

		// Token: 0x04000281 RID: 641
		private TraceKind _traceKind;

		// Token: 0x04000282 RID: 642
		private TraceLevel _traceLevel;

		// Token: 0x04000283 RID: 643
		private Lazy<Dictionary<object, object>> _properties = new Lazy<Dictionary<object, object>>(() => new Dictionary<object, object>());
	}
}
