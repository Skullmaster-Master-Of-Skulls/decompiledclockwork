using System;
using System.Web.Management;

namespace System.Web.Hosting
{
	// Token: 0x020007CC RID: 1996
	internal class ISAPIWorkerRequestInProcForIIS7 : ISAPIWorkerRequestInProcForIIS6
	{
		// Token: 0x06005FCE RID: 24526 RVA: 0x0014A986 File Offset: 0x00148B86
		internal ISAPIWorkerRequestInProcForIIS7(IntPtr ecb) : base(ecb)
		{
			this._trySkipIisCustomErrors = true;
		}

		// Token: 0x17001B76 RID: 7030
		// (get) Token: 0x06005FCF RID: 24527 RVA: 0x0014A996 File Offset: 0x00148B96
		// (set) Token: 0x06005FD0 RID: 24528 RVA: 0x0014A99E File Offset: 0x00148B9E
		internal override bool TrySkipIisCustomErrors
		{
			get
			{
				return this._trySkipIisCustomErrors;
			}
			set
			{
				this._trySkipIisCustomErrors = value;
			}
		}

		// Token: 0x06005FD1 RID: 24529 RVA: 0x0014A9A8 File Offset: 0x00148BA8
		internal override void RaiseTraceEvent(IntegratedTraceType traceType, string eventData)
		{
			if (IntPtr.Zero != this._ecb)
			{
				int flag = (traceType < IntegratedTraceType.DiagCritical) ? 4 : 2;
				if (EtwTrace.IsTraceEnabled(EtwTrace.InferVerbosity(traceType), flag))
				{
					string eventData2 = string.IsNullOrEmpty(eventData) ? string.Empty : eventData;
					UnsafeNativeMethods.EcbEmitSimpleTrace(this._ecb, (int)traceType, eventData2);
				}
			}
		}

		// Token: 0x06005FD2 RID: 24530 RVA: 0x0014AA00 File Offset: 0x00148C00
		internal override void RaiseTraceEvent(WebBaseEvent webEvent)
		{
			if (IntPtr.Zero != this._ecb && EtwTrace.IsTraceEnabled(webEvent.InferEtwTraceVerbosity(), 1))
			{
				int webEventType;
				int fieldCount;
				string[] fieldNames;
				int[] fieldTypes;
				string[] fieldData;
				webEvent.DeconstructWebEvent(out webEventType, out fieldCount, out fieldNames, out fieldTypes, out fieldData);
				UnsafeNativeMethods.EcbEmitWebEventTrace(this._ecb, webEventType, fieldCount, fieldNames, fieldTypes, fieldData);
			}
		}
	}
}
