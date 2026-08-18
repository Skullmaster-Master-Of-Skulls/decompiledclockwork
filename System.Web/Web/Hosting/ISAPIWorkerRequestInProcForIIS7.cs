using System;
using System.Web.Management;

namespace System.Web.Hosting
{
	// Token: 0x020002A9 RID: 681
	internal class ISAPIWorkerRequestInProcForIIS7 : ISAPIWorkerRequestInProcForIIS6
	{
		// Token: 0x060023B6 RID: 9142 RVA: 0x00099680 File Offset: 0x00098680
		internal ISAPIWorkerRequestInProcForIIS7(IntPtr ecb) : base(ecb)
		{
			this._trySkipIisCustomErrors = true;
		}

		// Token: 0x17000775 RID: 1909
		// (get) Token: 0x060023B7 RID: 9143 RVA: 0x00099690 File Offset: 0x00098690
		internal override bool IsRewriteModuleEnabled
		{
			get
			{
				if (this._rawUrl == null)
				{
					this.GetRawUrl();
				}
				return this._isRewriteModuleEnabled;
			}
		}

		// Token: 0x060023B8 RID: 9144 RVA: 0x000996A8 File Offset: 0x000986A8
		private string GetRequestUri()
		{
			if (base.GetUnicodeServerVariable("UNICODE_IIS_WasUrlRewritten") != "1")
			{
				return null;
			}
			string unicodeServerVariable = base.GetUnicodeServerVariable(7);
			if (unicodeServerVariable == null)
			{
				return null;
			}
			int num = 0;
			for (int i = 0; i < unicodeServerVariable.Length; i++)
			{
				if (unicodeServerVariable[i] == '/' && ++num == 3)
				{
					return unicodeServerVariable.Substring(i);
				}
			}
			return null;
		}

		// Token: 0x060023B9 RID: 9145 RVA: 0x0009970A File Offset: 0x0009870A
		public override string GetRawUrl()
		{
			if (this._rawUrl == null)
			{
				this._rawUrl = this.GetRequestUri();
				if (this._rawUrl != null)
				{
					this._isRewriteModuleEnabled = true;
					return this._rawUrl;
				}
				this._rawUrl = base.GetRawUrl();
			}
			return this._rawUrl;
		}

		// Token: 0x060023BA RID: 9146 RVA: 0x00099748 File Offset: 0x00098748
		internal override void SetRawUrl(string path)
		{
			this._rawUrl = path;
		}

		// Token: 0x17000776 RID: 1910
		// (get) Token: 0x060023BB RID: 9147 RVA: 0x00099751 File Offset: 0x00098751
		// (set) Token: 0x060023BC RID: 9148 RVA: 0x00099759 File Offset: 0x00098759
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

		// Token: 0x060023BD RID: 9149 RVA: 0x00099764 File Offset: 0x00098764
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

		// Token: 0x060023BE RID: 9150 RVA: 0x000997BC File Offset: 0x000987BC
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

		// Token: 0x04001C16 RID: 7190
		private string _rawUrl;

		// Token: 0x04001C17 RID: 7191
		private bool _isRewriteModuleEnabled;
	}
}
