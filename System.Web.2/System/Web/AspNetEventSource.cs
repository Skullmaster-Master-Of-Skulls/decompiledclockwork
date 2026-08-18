using System;
using System.Diagnostics.Tracing;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Web.Hosting;
using System.Web.Util;

namespace System.Web
{
	// Token: 0x02000019 RID: 25
	[EventSource(Name = "Microsoft-Windows-ASPNET", Guid = "ee799f41-cfa5-550b-bf2c-344747c1c668")]
	internal sealed class AspNetEventSource : EventSource
	{
		// Token: 0x060000B9 RID: 185 RVA: 0x0000397C File Offset: 0x00001B7C
		private unsafe AspNetEventSource()
		{
			if (AppDomain.CurrentDomain.IsHomogenous && AppDomain.CurrentDomain.IsFullyTrusted)
			{
				MethodInfo method = typeof(EventSource).GetMethod("WriteEventWithRelatedActivityIdCore", BindingFlags.Instance | BindingFlags.NonPublic, null, new Type[]
				{
					typeof(int),
					typeof(Guid*),
					typeof(int),
					typeof(EventSource.EventData*)
				}, null);
				if (method != null)
				{
					this._writeEventWithRelatedActivityIdCoreDel = (AspNetEventSource.WriteEventWithRelatedActivityIdCoreDelegate)Delegate.CreateDelegate(typeof(AspNetEventSource.WriteEventWithRelatedActivityIdCoreDelegate), this, method, false);
				}
			}
		}

		// Token: 0x060000BA RID: 186 RVA: 0x00003A24 File Offset: 0x00001C24
		[NonEvent]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void RequestEnteredAspNetPipeline(IIS7WorkerRequest wr, Guid childActivityId)
		{
			if (!base.IsEnabled())
			{
				return;
			}
			Guid requestTraceIdentifier = wr.RequestTraceIdentifier;
			this.RequestEnteredAspNetPipelineImpl(requestTraceIdentifier, childActivityId);
		}

		// Token: 0x060000BB RID: 187 RVA: 0x00003A4C File Offset: 0x00001C4C
		[NonEvent]
		private unsafe void RequestEnteredAspNetPipelineImpl(Guid iisActivityId, Guid aspNetActivityId)
		{
			if (ActivityIdHelper.Instance == null || this._writeEventWithRelatedActivityIdCoreDel == null || iisActivityId == Guid.Empty)
			{
				return;
			}
			Guid currentThreadActivityId = ActivityIdHelper.Instance.CurrentThreadActivityId;
			bool flag = currentThreadActivityId != iisActivityId;
			if (flag)
			{
				ActivityIdHelper.Instance.SetCurrentThreadActivityId(iisActivityId, out currentThreadActivityId);
			}
			this._writeEventWithRelatedActivityIdCoreDel(1, &aspNetActivityId, 0, null);
			if (flag)
			{
				Guid guid;
				ActivityIdHelper.Instance.SetCurrentThreadActivityId(currentThreadActivityId, out guid);
			}
		}

		// Token: 0x060000BC RID: 188 RVA: 0x00003ABB File Offset: 0x00001CBB
		[Event(1, Level = EventLevel.Informational, Task = (EventTask)1, Opcode = EventOpcode.Send, Version = 1)]
		private void RequestEnteredAspNetPipeline()
		{
			throw new NotImplementedException();
		}

		// Token: 0x060000BD RID: 189 RVA: 0x00003AC2 File Offset: 0x00001CC2
		[NonEvent]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void RequestStarted(IIS7WorkerRequest wr)
		{
			if (!base.IsEnabled())
			{
				return;
			}
			this.RequestStartedImpl(wr);
		}

		// Token: 0x060000BE RID: 190 RVA: 0x00003AD4 File Offset: 0x00001CD4
		[NonEvent]
		private unsafe void RequestStartedImpl(IIS7WorkerRequest wr)
		{
			string httpVerbName = wr.GetHttpVerbName();
			HTTP_COOKED_URL* cookedUrl = wr.GetCookedUrl();
			Guid requestTraceIdentifier = wr.RequestTraceIdentifier;
			Guid requestCorrelationId = wr.GetRequestCorrelationId();
			fixed (string text = httpVerbName)
			{
				char* ptr = text;
				if (ptr != null)
				{
					ptr += RuntimeHelpers.OffsetToStringData / 2;
				}
				EventSource.EventData* ptr2 = stackalloc EventSource.EventData[checked(unchecked((UIntPtr)3) * (UIntPtr)sizeof(EventSource.EventData))];
				AspNetEventSource.FillInEventData(ptr2, httpVerbName, ptr);
				ptr2[1].DataPointer = (IntPtr)((void*)cookedUrl->pFullUrl);
				ptr2[1].Size = (int)(checked(cookedUrl->FullUrlLength + 2));
				AspNetEventSource.FillInEventData(ptr2 + 2, &requestCorrelationId);
				base.WriteEventCore(2, 3, ptr2);
			}
		}

		// Token: 0x060000BF RID: 191 RVA: 0x00003ABB File Offset: 0x00001CBB
		[Event(2, Level = EventLevel.Informational, Task = (EventTask)1, Opcode = EventOpcode.Start, Version = 1)]
		private void RequestStarted(string HttpVerb, string FullUrl, Guid RequestCorrelationId)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x00003B7C File Offset: 0x00001D7C
		[Event(3, Level = EventLevel.Informational, Task = (EventTask)1, Opcode = EventOpcode.Stop, Version = 1)]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void RequestCompleted()
		{
			if (!base.IsEnabled())
			{
				return;
			}
			base.WriteEvent(3);
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x00003B8E File Offset: 0x00001D8E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private unsafe static void FillInEventData(EventSource.EventData* pEventData, string str, char* pStr)
		{
			if (pStr != null)
			{
				pEventData->DataPointer = (IntPtr)((void*)pStr);
				pEventData->Size = checked((str.Length + 1) * 2);
				return;
			}
			pEventData->DataPointer = AspNetEventSource.NullHelper.Instance.PtrToNullChar;
			pEventData->Size = 2;
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x00003BC9 File Offset: 0x00001DC9
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private unsafe static void FillInEventData(EventSource.EventData* pEventData, Guid* pGuid)
		{
			pEventData->DataPointer = (IntPtr)((void*)pGuid);
			pEventData->Size = sizeof(Guid);
		}

		// Token: 0x040000D9 RID: 217
		public static readonly AspNetEventSource Instance = new AspNetEventSource();

		// Token: 0x040000DA RID: 218
		private readonly AspNetEventSource.WriteEventWithRelatedActivityIdCoreDelegate _writeEventWithRelatedActivityIdCoreDel;

		// Token: 0x020008AF RID: 2223
		// (Invoke) Token: 0x060067A3 RID: 26531
		private unsafe delegate void WriteEventWithRelatedActivityIdCoreDelegate(int eventId, Guid* childActivityID, int eventDataCount, EventSource.EventData* data);

		// Token: 0x020008B0 RID: 2224
		private enum Events
		{
			// Token: 0x040035CE RID: 13774
			RequestEnteredAspNetPipeline = 1,
			// Token: 0x040035CF RID: 13775
			RequestStarted,
			// Token: 0x040035D0 RID: 13776
			RequestCompleted
		}

		// Token: 0x020008B1 RID: 2225
		public static class Tasks
		{
			// Token: 0x040035D1 RID: 13777
			public const EventTask Request = (EventTask)1;
		}

		// Token: 0x020008B2 RID: 2226
		private sealed class NullHelper : CriticalFinalizerObject
		{
			// Token: 0x060067A6 RID: 26534 RVA: 0x00170479 File Offset: 0x0016E679
			[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
			private unsafe NullHelper()
			{
				this.PtrToNullChar = Marshal.AllocHGlobal(2);
				*(short*)((void*)this.PtrToNullChar) = 0;
			}

			// Token: 0x060067A7 RID: 26535 RVA: 0x0017049C File Offset: 0x0016E69C
			[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
			~NullHelper()
			{
				if (this.PtrToNullChar != IntPtr.Zero)
				{
					Marshal.FreeHGlobal(this.PtrToNullChar);
				}
			}

			// Token: 0x040035D2 RID: 13778
			public static readonly AspNetEventSource.NullHelper Instance = new AspNetEventSource.NullHelper();

			// Token: 0x040035D3 RID: 13779
			public readonly IntPtr PtrToNullChar;
		}
	}
}
