using System;
using System.IO;

namespace System.Web.WebPages.Instrumentation
{
	// Token: 0x0200003B RID: 59
	public class InstrumentationService
	{
		// Token: 0x06000198 RID: 408 RVA: 0x000057F8 File Offset: 0x000039F8
		public InstrumentationService()
		{
			this.ExtractInstrumentationService = new Func<HttpContextBase, PageInstrumentationServiceAdapter>(this.GetInstrumentationServiceUncached);
			this.CreateContext = new Func<string, TextWriter, int, int, bool, PageExecutionContextAdapter>(InstrumentationService.CreateSystemWebContext);
		}

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x06000199 RID: 409 RVA: 0x00005844 File Offset: 0x00003A44
		// (set) Token: 0x0600019A RID: 410 RVA: 0x0000584C File Offset: 0x00003A4C
		public bool IsAvailable
		{
			get
			{
				return this._localIsAvailable;
			}
			internal set
			{
				this._localIsAvailable = value;
			}
		}

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x0600019B RID: 411 RVA: 0x00005855 File Offset: 0x00003A55
		// (set) Token: 0x0600019C RID: 412 RVA: 0x0000585D File Offset: 0x00003A5D
		internal Func<HttpContextBase, PageInstrumentationServiceAdapter> ExtractInstrumentationService { get; set; }

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x0600019D RID: 413 RVA: 0x00005866 File Offset: 0x00003A66
		// (set) Token: 0x0600019E RID: 414 RVA: 0x0000586E File Offset: 0x00003A6E
		internal Func<string, TextWriter, int, int, bool, PageExecutionContextAdapter> CreateContext { get; set; }

		// Token: 0x0600019F RID: 415 RVA: 0x00005878 File Offset: 0x00003A78
		public void BeginContext(HttpContextBase context, string virtualPath, TextWriter writer, int startPosition, int length, bool isLiteral)
		{
			if (this.IsAvailable)
			{
				PageInstrumentationServiceAdapter instrumentationService = this.GetInstrumentationService(context);
				if (instrumentationService != null && instrumentationService.ExecutionListeners.Count > 0)
				{
					PageExecutionContextAdapter context2 = this.CreateContext(virtualPath, writer, startPosition, length, isLiteral);
					foreach (PageExecutionListenerAdapter pageExecutionListenerAdapter in instrumentationService.ExecutionListeners)
					{
						pageExecutionListenerAdapter.BeginContext(context2);
					}
				}
			}
		}

		// Token: 0x060001A0 RID: 416 RVA: 0x000058FC File Offset: 0x00003AFC
		public void EndContext(HttpContextBase context, string virtualPath, TextWriter writer, int startPosition, int length, bool isLiteral)
		{
			if (this.IsAvailable)
			{
				PageInstrumentationServiceAdapter instrumentationService = this.GetInstrumentationService(context);
				if (instrumentationService != null && instrumentationService.ExecutionListeners.Count > 0)
				{
					PageExecutionContextAdapter context2 = this.CreateContext(virtualPath, writer, startPosition, length, isLiteral);
					foreach (PageExecutionListenerAdapter pageExecutionListenerAdapter in instrumentationService.ExecutionListeners)
					{
						pageExecutionListenerAdapter.EndContext(context2);
					}
				}
			}
		}

		// Token: 0x060001A1 RID: 417 RVA: 0x00005980 File Offset: 0x00003B80
		private static PageExecutionContextAdapter CreateSystemWebContext(string virtualPath, TextWriter writer, int startPosition, int length, bool isLiteral)
		{
			return new PageExecutionContextAdapter
			{
				VirtualPath = virtualPath,
				TextWriter = writer,
				StartPosition = startPosition,
				Length = length,
				IsLiteral = isLiteral
			};
		}

		// Token: 0x060001A2 RID: 418 RVA: 0x000059B8 File Offset: 0x00003BB8
		private PageInstrumentationServiceAdapter GetInstrumentationService(HttpContextBase context)
		{
			if (!this._isInstrumentationServiceAdapterInitialized)
			{
				this._instrumentationServiceAdapter = this.ExtractInstrumentationService(context);
				this._isInstrumentationServiceAdapterInitialized = true;
			}
			return this._instrumentationServiceAdapter;
		}

		// Token: 0x060001A3 RID: 419 RVA: 0x000059E4 File Offset: 0x00003BE4
		private PageInstrumentationServiceAdapter GetInstrumentationServiceUncached(HttpContextBase context)
		{
			HttpContextAdapter httpContextAdapter = new HttpContextAdapter(context);
			return httpContextAdapter.PageInstrumentation;
		}

		// Token: 0x04000084 RID: 132
		private static readonly bool _isAvailable = HttpContextAdapter.IsInstrumentationAvailable;

		// Token: 0x04000085 RID: 133
		private bool _localIsAvailable = InstrumentationService._isAvailable && PageInstrumentationServiceAdapter.IsEnabled;

		// Token: 0x04000086 RID: 134
		private PageInstrumentationServiceAdapter _instrumentationServiceAdapter;

		// Token: 0x04000087 RID: 135
		private bool _isInstrumentationServiceAdapterInitialized;
	}
}
