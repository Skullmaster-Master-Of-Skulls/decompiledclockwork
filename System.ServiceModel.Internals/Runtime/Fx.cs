using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Runtime.Diagnostics;
using System.Runtime.Serialization;
using System.Security;
using System.Threading;

namespace System.Runtime
{
	// Token: 0x02000019 RID: 25
	internal static class Fx
	{
		// Token: 0x17000016 RID: 22
		// (get) Token: 0x060000A2 RID: 162 RVA: 0x00003DA5 File Offset: 0x00001FA5
		public static ExceptionTrace Exception
		{
			get
			{
				if (Fx.exceptionTrace == null)
				{
					Fx.exceptionTrace = new ExceptionTrace("System.Runtime", Fx.Trace);
				}
				return Fx.exceptionTrace;
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x060000A3 RID: 163 RVA: 0x00003DC7 File Offset: 0x00001FC7
		public static EtwDiagnosticTrace Trace
		{
			get
			{
				if (Fx.diagnosticTrace == null)
				{
					Fx.diagnosticTrace = Fx.InitializeTracing();
				}
				return Fx.diagnosticTrace;
			}
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x00003DE0 File Offset: 0x00001FE0
		[SecuritySafeCritical]
		private static EtwDiagnosticTrace InitializeTracing()
		{
			EtwDiagnosticTrace etwDiagnosticTrace = new EtwDiagnosticTrace("System.Runtime", EtwDiagnosticTrace.DefaultEtwProviderId);
			if (etwDiagnosticTrace.EtwProvider != null)
			{
				EtwDiagnosticTrace etwDiagnosticTrace2 = etwDiagnosticTrace;
				etwDiagnosticTrace2.RefreshState = (Action)Delegate.Combine(etwDiagnosticTrace2.RefreshState, new Action(delegate()
				{
					Fx.UpdateLevel();
				}));
			}
			Fx.UpdateLevel(etwDiagnosticTrace);
			return etwDiagnosticTrace;
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x060000A5 RID: 165 RVA: 0x00003E41 File Offset: 0x00002041
		// (set) Token: 0x060000A6 RID: 166 RVA: 0x00003E48 File Offset: 0x00002048
		public static Fx.ExceptionHandler AsynchronousThreadExceptionHandler
		{
			[SecuritySafeCritical]
			[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
			get
			{
				return Fx.asynchronousThreadExceptionHandler;
			}
			[SecurityCritical]
			set
			{
				Fx.asynchronousThreadExceptionHandler = value;
			}
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x00003E50 File Offset: 0x00002050
		[Conditional("DEBUG")]
		public static void Assert(bool condition, string description)
		{
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x00003E54 File Offset: 0x00002054
		[Conditional("DEBUG")]
		public static void Assert(string description)
		{
			AssertHelper.FireAssert(description);
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x00003E5C File Offset: 0x0000205C
		public static void AssertAndThrow(bool condition, string description)
		{
			if (!condition)
			{
				Fx.AssertAndThrow(description);
			}
		}

		// Token: 0x060000AA RID: 170 RVA: 0x00003E68 File Offset: 0x00002068
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static Exception AssertAndThrow(string description)
		{
			TraceCore.ShipAssertExceptionMessage(Fx.Trace, description);
			throw new Fx.InternalException(description);
		}

		// Token: 0x060000AB RID: 171 RVA: 0x00003E7B File Offset: 0x0000207B
		public static void AssertAndThrowFatal(bool condition, string description)
		{
			if (!condition)
			{
				Fx.AssertAndThrowFatal(description);
			}
		}

		// Token: 0x060000AC RID: 172 RVA: 0x00003E87 File Offset: 0x00002087
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static Exception AssertAndThrowFatal(string description)
		{
			TraceCore.ShipAssertExceptionMessage(Fx.Trace, description);
			throw new Fx.FatalInternalException(description);
		}

		// Token: 0x060000AD RID: 173 RVA: 0x00003E9A File Offset: 0x0000209A
		public static void AssertAndFailFast(bool condition, string description)
		{
			if (!condition)
			{
				Fx.AssertAndFailFast(description);
			}
		}

		// Token: 0x060000AE RID: 174 RVA: 0x00003EA8 File Offset: 0x000020A8
		[SecuritySafeCritical]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static Exception AssertAndFailFast(string description)
		{
			string message = InternalSR.FailFastMessage(description);
			try
			{
				try
				{
					Fx.Exception.TraceFailFast(message);
				}
				finally
				{
					Environment.FailFast(message);
				}
			}
			catch
			{
				throw;
			}
			return null;
		}

		// Token: 0x060000AF RID: 175 RVA: 0x00003EF4 File Offset: 0x000020F4
		public static bool IsFatal(Exception exception)
		{
			while (exception != null)
			{
				if (exception is FatalException || (exception is OutOfMemoryException && !(exception is InsufficientMemoryException)) || exception is ThreadAbortException || exception is Fx.FatalInternalException)
				{
					return true;
				}
				if (exception is TypeInitializationException || exception is TargetInvocationException)
				{
					exception = exception.InnerException;
				}
				else
				{
					if (exception is AggregateException)
					{
						ReadOnlyCollection<Exception> innerExceptions = ((AggregateException)exception).InnerExceptions;
						using (IEnumerator<Exception> enumerator = innerExceptions.GetEnumerator())
						{
							while (enumerator.MoveNext())
							{
								Exception exception2 = enumerator.Current;
								if (Fx.IsFatal(exception2))
								{
									return true;
								}
							}
							break;
						}
						continue;
					}
					break;
				}
			}
			return false;
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x060000B0 RID: 176 RVA: 0x000031F5 File Offset: 0x000013F5
		internal static bool AssertsFailFast
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x060000B1 RID: 177 RVA: 0x00003FA8 File Offset: 0x000021A8
		internal static Type[] BreakOnExceptionTypes
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x060000B2 RID: 178 RVA: 0x000031F5 File Offset: 0x000013F5
		internal static bool FastDebug
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x060000B3 RID: 179 RVA: 0x000031F5 File Offset: 0x000013F5
		internal static bool StealthDebugger
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x00003FAB File Offset: 0x000021AB
		public static Action<T1> ThunkCallback<T1>(Action<T1> callback)
		{
			return new Fx.ActionThunk<T1>(callback).ThunkFrame;
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x00003FB8 File Offset: 0x000021B8
		public static AsyncCallback ThunkCallback(AsyncCallback callback)
		{
			return new Fx.AsyncThunk(callback).ThunkFrame;
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x00003FC5 File Offset: 0x000021C5
		public static WaitCallback ThunkCallback(WaitCallback callback)
		{
			return new Fx.WaitThunk(callback).ThunkFrame;
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x00003FD2 File Offset: 0x000021D2
		public static TimerCallback ThunkCallback(TimerCallback callback)
		{
			return new Fx.TimerThunk(callback).ThunkFrame;
		}

		// Token: 0x060000B8 RID: 184 RVA: 0x00003FDF File Offset: 0x000021DF
		public static WaitOrTimerCallback ThunkCallback(WaitOrTimerCallback callback)
		{
			return new Fx.WaitOrTimerThunk(callback).ThunkFrame;
		}

		// Token: 0x060000B9 RID: 185 RVA: 0x00003FEC File Offset: 0x000021EC
		public static SendOrPostCallback ThunkCallback(SendOrPostCallback callback)
		{
			return new Fx.SendOrPostThunk(callback).ThunkFrame;
		}

		// Token: 0x060000BA RID: 186 RVA: 0x00003FF9 File Offset: 0x000021F9
		[SecurityCritical]
		public static IOCompletionCallback ThunkCallback(IOCompletionCallback callback)
		{
			return new Fx.IOCompletionThunk(callback).ThunkFrame;
		}

		// Token: 0x060000BB RID: 187 RVA: 0x00004008 File Offset: 0x00002208
		public static Guid CreateGuid(string guidString)
		{
			bool flag = false;
			Guid result = Guid.Empty;
			try
			{
				result = new Guid(guidString);
				flag = true;
			}
			finally
			{
				if (!flag)
				{
					Fx.AssertAndThrow("Creation of the Guid failed.");
				}
			}
			return result;
		}

		// Token: 0x060000BC RID: 188 RVA: 0x00004048 File Offset: 0x00002248
		public static bool TryCreateGuid(string guidString, out Guid result)
		{
			bool result2 = false;
			result = Guid.Empty;
			try
			{
				result = new Guid(guidString);
				result2 = true;
			}
			catch (ArgumentException)
			{
			}
			catch (FormatException)
			{
			}
			catch (OverflowException)
			{
			}
			return result2;
		}

		// Token: 0x060000BD RID: 189 RVA: 0x000040A4 File Offset: 0x000022A4
		public static byte[] AllocateByteArray(int size)
		{
			byte[] result;
			try
			{
				result = new byte[size];
			}
			catch (OutOfMemoryException innerException)
			{
				throw Fx.Exception.AsError(new InsufficientMemoryException(InternalSR.BufferAllocationFailed(size), innerException));
			}
			return result;
		}

		// Token: 0x060000BE RID: 190 RVA: 0x000040E8 File Offset: 0x000022E8
		public static char[] AllocateCharArray(int size)
		{
			char[] result;
			try
			{
				result = new char[size];
			}
			catch (OutOfMemoryException innerException)
			{
				throw Fx.Exception.AsError(new InsufficientMemoryException(InternalSR.BufferAllocationFailed(size * 2), innerException));
			}
			return result;
		}

		// Token: 0x060000BF RID: 191 RVA: 0x00004130 File Offset: 0x00002330
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		private static void TraceExceptionNoThrow(Exception exception)
		{
			try
			{
				Fx.Exception.TraceUnhandledException(exception);
			}
			catch
			{
			}
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x00004160 File Offset: 0x00002360
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		private static bool HandleAtThreadBase(Exception exception)
		{
			if (exception == null)
			{
				return false;
			}
			Fx.TraceExceptionNoThrow(exception);
			try
			{
				Fx.ExceptionHandler exceptionHandler = Fx.AsynchronousThreadExceptionHandler;
				return exceptionHandler != null && exceptionHandler.HandleException(exception);
			}
			catch (Exception exception2)
			{
				Fx.TraceExceptionNoThrow(exception2);
			}
			return false;
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x000041AC File Offset: 0x000023AC
		private static void UpdateLevel(EtwDiagnosticTrace trace)
		{
			if (trace == null)
			{
				return;
			}
			if (TraceCore.ActionItemCallbackInvokedIsEnabled(trace) || TraceCore.ActionItemScheduledIsEnabled(trace))
			{
				trace.SetEnd2EndActivityTracingEnabled(true);
			}
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x000041C9 File Offset: 0x000023C9
		private static void UpdateLevel()
		{
			Fx.UpdateLevel(Fx.Trace);
		}

		// Token: 0x04000067 RID: 103
		private const string defaultEventSource = "System.Runtime";

		// Token: 0x04000068 RID: 104
		private static ExceptionTrace exceptionTrace;

		// Token: 0x04000069 RID: 105
		private static EtwDiagnosticTrace diagnosticTrace;

		// Token: 0x0400006A RID: 106
		[SecurityCritical]
		private static Fx.ExceptionHandler asynchronousThreadExceptionHandler;

		// Token: 0x0200005D RID: 93
		public abstract class ExceptionHandler
		{
			// Token: 0x06000384 RID: 900
			public abstract bool HandleException(Exception exception);
		}

		// Token: 0x0200005E RID: 94
		public static class Tag
		{
			// Token: 0x0200009B RID: 155
			public enum CacheAttrition
			{
				// Token: 0x040002C9 RID: 713
				None,
				// Token: 0x040002CA RID: 714
				ElementOnTimer,
				// Token: 0x040002CB RID: 715
				ElementOnGC,
				// Token: 0x040002CC RID: 716
				ElementOnCallback,
				// Token: 0x040002CD RID: 717
				FullPurgeOnTimer,
				// Token: 0x040002CE RID: 718
				FullPurgeOnEachAccess,
				// Token: 0x040002CF RID: 719
				PartialPurgeOnTimer,
				// Token: 0x040002D0 RID: 720
				PartialPurgeOnEachAccess
			}

			// Token: 0x0200009C RID: 156
			public enum ThrottleAction
			{
				// Token: 0x040002D2 RID: 722
				Reject,
				// Token: 0x040002D3 RID: 723
				Pause
			}

			// Token: 0x0200009D RID: 157
			public enum ThrottleMetric
			{
				// Token: 0x040002D5 RID: 725
				Count,
				// Token: 0x040002D6 RID: 726
				Rate,
				// Token: 0x040002D7 RID: 727
				Other
			}

			// Token: 0x0200009E RID: 158
			public enum Location
			{
				// Token: 0x040002D9 RID: 729
				InProcess,
				// Token: 0x040002DA RID: 730
				OutOfProcess,
				// Token: 0x040002DB RID: 731
				LocalSystem,
				// Token: 0x040002DC RID: 732
				LocalOrRemoteSystem,
				// Token: 0x040002DD RID: 733
				RemoteSystem
			}

			// Token: 0x0200009F RID: 159
			public enum SynchronizationKind
			{
				// Token: 0x040002DF RID: 735
				LockStatement,
				// Token: 0x040002E0 RID: 736
				MonitorWait,
				// Token: 0x040002E1 RID: 737
				MonitorExplicit,
				// Token: 0x040002E2 RID: 738
				InterlockedNoSpin,
				// Token: 0x040002E3 RID: 739
				InterlockedWithSpin,
				// Token: 0x040002E4 RID: 740
				FromFieldType
			}

			// Token: 0x020000A0 RID: 160
			[Flags]
			public enum BlocksUsing
			{
				// Token: 0x040002E6 RID: 742
				MonitorEnter = 0,
				// Token: 0x040002E7 RID: 743
				MonitorWait = 1,
				// Token: 0x040002E8 RID: 744
				ManualResetEvent = 2,
				// Token: 0x040002E9 RID: 745
				AutoResetEvent = 3,
				// Token: 0x040002EA RID: 746
				AsyncResult = 4,
				// Token: 0x040002EB RID: 747
				IAsyncResult = 5,
				// Token: 0x040002EC RID: 748
				PInvoke = 6,
				// Token: 0x040002ED RID: 749
				InputQueue = 7,
				// Token: 0x040002EE RID: 750
				ThreadNeutralSemaphore = 8,
				// Token: 0x040002EF RID: 751
				PrivatePrimitive = 9,
				// Token: 0x040002F0 RID: 752
				OtherInternalPrimitive = 10,
				// Token: 0x040002F1 RID: 753
				OtherFrameworkPrimitive = 11,
				// Token: 0x040002F2 RID: 754
				OtherInterop = 12,
				// Token: 0x040002F3 RID: 755
				Other = 13,
				// Token: 0x040002F4 RID: 756
				NonBlocking = 14
			}

			// Token: 0x020000A1 RID: 161
			public static class Strings
			{
				// Token: 0x040002F5 RID: 757
				internal const string ExternallyManaged = "externally managed";

				// Token: 0x040002F6 RID: 758
				internal const string AppDomain = "AppDomain";

				// Token: 0x040002F7 RID: 759
				internal const string DeclaringInstance = "instance of declaring class";

				// Token: 0x040002F8 RID: 760
				internal const string Unbounded = "unbounded";

				// Token: 0x040002F9 RID: 761
				internal const string Infinite = "infinite";
			}

			// Token: 0x020000A2 RID: 162
			[AttributeUsage(AttributeTargets.Class | AttributeTargets.Constructor | AttributeTargets.Method | AttributeTargets.Property, AllowMultiple = true, Inherited = false)]
			[Conditional("DEBUG")]
			public sealed class FriendAccessAllowedAttribute : Attribute
			{
				// Token: 0x0600046B RID: 1131 RVA: 0x00014102 File Offset: 0x00012302
				public FriendAccessAllowedAttribute(string assemblyName)
				{
					this.AssemblyName = assemblyName;
				}

				// Token: 0x170000C7 RID: 199
				// (get) Token: 0x0600046C RID: 1132 RVA: 0x00014111 File Offset: 0x00012311
				// (set) Token: 0x0600046D RID: 1133 RVA: 0x00014119 File Offset: 0x00012319
				public string AssemblyName { get; set; }
			}

			// Token: 0x020000A3 RID: 163
			public static class Throws
			{
				// Token: 0x020000B6 RID: 182
				[AttributeUsage(AttributeTargets.Constructor | AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
				[Conditional("CODE_ANALYSIS_CDF")]
				public sealed class TimeoutAttribute : Fx.Tag.ThrowsAttribute
				{
					// Token: 0x060004D3 RID: 1235 RVA: 0x00014881 File Offset: 0x00012A81
					public TimeoutAttribute() : this("The operation timed out.")
					{
					}

					// Token: 0x060004D4 RID: 1236 RVA: 0x0001488E File Offset: 0x00012A8E
					public TimeoutAttribute(string diagnosis) : base(typeof(TimeoutException), diagnosis)
					{
					}
				}
			}

			// Token: 0x020000A4 RID: 164
			[AttributeUsage(AttributeTargets.Field)]
			[Conditional("CODE_ANALYSIS_CDF")]
			public sealed class CacheAttribute : Attribute
			{
				// Token: 0x0600046E RID: 1134 RVA: 0x00014124 File Offset: 0x00012324
				public CacheAttribute(Type elementType, Fx.Tag.CacheAttrition cacheAttrition)
				{
					this.Scope = "instance of declaring class";
					this.SizeLimit = "unbounded";
					this.Timeout = "infinite";
					if (elementType == null)
					{
						throw Fx.Exception.ArgumentNull("elementType");
					}
					this.elementType = elementType;
					this.cacheAttrition = cacheAttrition;
				}

				// Token: 0x170000C8 RID: 200
				// (get) Token: 0x0600046F RID: 1135 RVA: 0x0001417F File Offset: 0x0001237F
				public Type ElementType
				{
					get
					{
						return this.elementType;
					}
				}

				// Token: 0x170000C9 RID: 201
				// (get) Token: 0x06000470 RID: 1136 RVA: 0x00014187 File Offset: 0x00012387
				public Fx.Tag.CacheAttrition CacheAttrition
				{
					get
					{
						return this.cacheAttrition;
					}
				}

				// Token: 0x170000CA RID: 202
				// (get) Token: 0x06000471 RID: 1137 RVA: 0x0001418F File Offset: 0x0001238F
				// (set) Token: 0x06000472 RID: 1138 RVA: 0x00014197 File Offset: 0x00012397
				public string Scope { get; set; }

				// Token: 0x170000CB RID: 203
				// (get) Token: 0x06000473 RID: 1139 RVA: 0x000141A0 File Offset: 0x000123A0
				// (set) Token: 0x06000474 RID: 1140 RVA: 0x000141A8 File Offset: 0x000123A8
				public string SizeLimit { get; set; }

				// Token: 0x170000CC RID: 204
				// (get) Token: 0x06000475 RID: 1141 RVA: 0x000141B1 File Offset: 0x000123B1
				// (set) Token: 0x06000476 RID: 1142 RVA: 0x000141B9 File Offset: 0x000123B9
				public string Timeout { get; set; }

				// Token: 0x040002FB RID: 763
				private readonly Type elementType;

				// Token: 0x040002FC RID: 764
				private readonly Fx.Tag.CacheAttrition cacheAttrition;
			}

			// Token: 0x020000A5 RID: 165
			[AttributeUsage(AttributeTargets.Field)]
			[Conditional("CODE_ANALYSIS_CDF")]
			public sealed class QueueAttribute : Attribute
			{
				// Token: 0x06000477 RID: 1143 RVA: 0x000141C2 File Offset: 0x000123C2
				public QueueAttribute(Type elementType)
				{
					this.Scope = "instance of declaring class";
					this.SizeLimit = "unbounded";
					if (elementType == null)
					{
						throw Fx.Exception.ArgumentNull("elementType");
					}
					this.elementType = elementType;
				}

				// Token: 0x170000CD RID: 205
				// (get) Token: 0x06000478 RID: 1144 RVA: 0x00014200 File Offset: 0x00012400
				public Type ElementType
				{
					get
					{
						return this.elementType;
					}
				}

				// Token: 0x170000CE RID: 206
				// (get) Token: 0x06000479 RID: 1145 RVA: 0x00014208 File Offset: 0x00012408
				// (set) Token: 0x0600047A RID: 1146 RVA: 0x00014210 File Offset: 0x00012410
				public string Scope { get; set; }

				// Token: 0x170000CF RID: 207
				// (get) Token: 0x0600047B RID: 1147 RVA: 0x00014219 File Offset: 0x00012419
				// (set) Token: 0x0600047C RID: 1148 RVA: 0x00014221 File Offset: 0x00012421
				public string SizeLimit { get; set; }

				// Token: 0x170000D0 RID: 208
				// (get) Token: 0x0600047D RID: 1149 RVA: 0x0001422A File Offset: 0x0001242A
				// (set) Token: 0x0600047E RID: 1150 RVA: 0x00014232 File Offset: 0x00012432
				public bool StaleElementsRemovedImmediately { get; set; }

				// Token: 0x170000D1 RID: 209
				// (get) Token: 0x0600047F RID: 1151 RVA: 0x0001423B File Offset: 0x0001243B
				// (set) Token: 0x06000480 RID: 1152 RVA: 0x00014243 File Offset: 0x00012443
				public bool EnqueueThrowsIfFull { get; set; }

				// Token: 0x04000300 RID: 768
				private readonly Type elementType;
			}

			// Token: 0x020000A6 RID: 166
			[AttributeUsage(AttributeTargets.Field)]
			[Conditional("CODE_ANALYSIS_CDF")]
			public sealed class ThrottleAttribute : Attribute
			{
				// Token: 0x06000481 RID: 1153 RVA: 0x0001424C File Offset: 0x0001244C
				public ThrottleAttribute(Fx.Tag.ThrottleAction throttleAction, Fx.Tag.ThrottleMetric throttleMetric, string limit)
				{
					this.Scope = "AppDomain";
					if (string.IsNullOrEmpty(limit))
					{
						throw Fx.Exception.ArgumentNullOrEmpty("limit");
					}
					this.throttleAction = throttleAction;
					this.throttleMetric = throttleMetric;
					this.limit = limit;
				}

				// Token: 0x170000D2 RID: 210
				// (get) Token: 0x06000482 RID: 1154 RVA: 0x0001428C File Offset: 0x0001248C
				public Fx.Tag.ThrottleAction ThrottleAction
				{
					get
					{
						return this.throttleAction;
					}
				}

				// Token: 0x170000D3 RID: 211
				// (get) Token: 0x06000483 RID: 1155 RVA: 0x00014294 File Offset: 0x00012494
				public Fx.Tag.ThrottleMetric ThrottleMetric
				{
					get
					{
						return this.throttleMetric;
					}
				}

				// Token: 0x170000D4 RID: 212
				// (get) Token: 0x06000484 RID: 1156 RVA: 0x0001429C File Offset: 0x0001249C
				public string Limit
				{
					get
					{
						return this.limit;
					}
				}

				// Token: 0x170000D5 RID: 213
				// (get) Token: 0x06000485 RID: 1157 RVA: 0x000142A4 File Offset: 0x000124A4
				// (set) Token: 0x06000486 RID: 1158 RVA: 0x000142AC File Offset: 0x000124AC
				public string Scope { get; set; }

				// Token: 0x04000305 RID: 773
				private readonly Fx.Tag.ThrottleAction throttleAction;

				// Token: 0x04000306 RID: 774
				private readonly Fx.Tag.ThrottleMetric throttleMetric;

				// Token: 0x04000307 RID: 775
				private readonly string limit;
			}

			// Token: 0x020000A7 RID: 167
			[AttributeUsage(AttributeTargets.Constructor | AttributeTargets.Method | AttributeTargets.Field, AllowMultiple = true, Inherited = false)]
			[Conditional("CODE_ANALYSIS_CDF")]
			public sealed class ExternalResourceAttribute : Attribute
			{
				// Token: 0x06000487 RID: 1159 RVA: 0x000142B5 File Offset: 0x000124B5
				public ExternalResourceAttribute(Fx.Tag.Location location, string description)
				{
					this.location = location;
					this.description = description;
				}

				// Token: 0x170000D6 RID: 214
				// (get) Token: 0x06000488 RID: 1160 RVA: 0x000142CB File Offset: 0x000124CB
				public Fx.Tag.Location Location
				{
					get
					{
						return this.location;
					}
				}

				// Token: 0x170000D7 RID: 215
				// (get) Token: 0x06000489 RID: 1161 RVA: 0x000142D3 File Offset: 0x000124D3
				public string Description
				{
					get
					{
						return this.description;
					}
				}

				// Token: 0x04000309 RID: 777
				private readonly Fx.Tag.Location location;

				// Token: 0x0400030A RID: 778
				private readonly string description;
			}

			// Token: 0x020000A8 RID: 168
			[AttributeUsage(AttributeTargets.Class | AttributeTargets.Field, Inherited = false)]
			[Conditional("CODE_ANALYSIS_CDF")]
			public sealed class SynchronizationObjectAttribute : Attribute
			{
				// Token: 0x0600048A RID: 1162 RVA: 0x000142DB File Offset: 0x000124DB
				public SynchronizationObjectAttribute()
				{
					this.Blocking = true;
					this.Scope = "instance of declaring class";
					this.Kind = Fx.Tag.SynchronizationKind.FromFieldType;
				}

				// Token: 0x170000D8 RID: 216
				// (get) Token: 0x0600048B RID: 1163 RVA: 0x000142FC File Offset: 0x000124FC
				// (set) Token: 0x0600048C RID: 1164 RVA: 0x00014304 File Offset: 0x00012504
				public bool Blocking { get; set; }

				// Token: 0x170000D9 RID: 217
				// (get) Token: 0x0600048D RID: 1165 RVA: 0x0001430D File Offset: 0x0001250D
				// (set) Token: 0x0600048E RID: 1166 RVA: 0x00014315 File Offset: 0x00012515
				public string Scope { get; set; }

				// Token: 0x170000DA RID: 218
				// (get) Token: 0x0600048F RID: 1167 RVA: 0x0001431E File Offset: 0x0001251E
				// (set) Token: 0x06000490 RID: 1168 RVA: 0x00014326 File Offset: 0x00012526
				public Fx.Tag.SynchronizationKind Kind { get; set; }
			}

			// Token: 0x020000A9 RID: 169
			[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, Inherited = true)]
			[Conditional("CODE_ANALYSIS_CDF")]
			public sealed class SynchronizationPrimitiveAttribute : Attribute
			{
				// Token: 0x06000491 RID: 1169 RVA: 0x0001432F File Offset: 0x0001252F
				public SynchronizationPrimitiveAttribute(Fx.Tag.BlocksUsing blocksUsing)
				{
					this.blocksUsing = blocksUsing;
				}

				// Token: 0x170000DB RID: 219
				// (get) Token: 0x06000492 RID: 1170 RVA: 0x0001433E File Offset: 0x0001253E
				public Fx.Tag.BlocksUsing BlocksUsing
				{
					get
					{
						return this.blocksUsing;
					}
				}

				// Token: 0x170000DC RID: 220
				// (get) Token: 0x06000493 RID: 1171 RVA: 0x00014346 File Offset: 0x00012546
				// (set) Token: 0x06000494 RID: 1172 RVA: 0x0001434E File Offset: 0x0001254E
				public bool SupportsAsync { get; set; }

				// Token: 0x170000DD RID: 221
				// (get) Token: 0x06000495 RID: 1173 RVA: 0x00014357 File Offset: 0x00012557
				// (set) Token: 0x06000496 RID: 1174 RVA: 0x0001435F File Offset: 0x0001255F
				public bool Spins { get; set; }

				// Token: 0x170000DE RID: 222
				// (get) Token: 0x06000497 RID: 1175 RVA: 0x00014368 File Offset: 0x00012568
				// (set) Token: 0x06000498 RID: 1176 RVA: 0x00014370 File Offset: 0x00012570
				public string ReleaseMethod { get; set; }

				// Token: 0x0400030E RID: 782
				private readonly Fx.Tag.BlocksUsing blocksUsing;
			}

			// Token: 0x020000AA RID: 170
			[AttributeUsage(AttributeTargets.Constructor | AttributeTargets.Method, Inherited = false)]
			[Conditional("CODE_ANALYSIS_CDF")]
			public sealed class BlockingAttribute : Attribute
			{
				// Token: 0x170000DF RID: 223
				// (get) Token: 0x0600049A RID: 1178 RVA: 0x00014381 File Offset: 0x00012581
				// (set) Token: 0x0600049B RID: 1179 RVA: 0x00014389 File Offset: 0x00012589
				public string CancelMethod { get; set; }

				// Token: 0x170000E0 RID: 224
				// (get) Token: 0x0600049C RID: 1180 RVA: 0x00014392 File Offset: 0x00012592
				// (set) Token: 0x0600049D RID: 1181 RVA: 0x0001439A File Offset: 0x0001259A
				public Type CancelDeclaringType { get; set; }

				// Token: 0x170000E1 RID: 225
				// (get) Token: 0x0600049E RID: 1182 RVA: 0x000143A3 File Offset: 0x000125A3
				// (set) Token: 0x0600049F RID: 1183 RVA: 0x000143AB File Offset: 0x000125AB
				public string Conditional { get; set; }
			}

			// Token: 0x020000AB RID: 171
			[AttributeUsage(AttributeTargets.Constructor | AttributeTargets.Method, Inherited = false)]
			[Conditional("CODE_ANALYSIS_CDF")]
			public sealed class GuaranteeNonBlockingAttribute : Attribute
			{
			}

			// Token: 0x020000AC RID: 172
			[AttributeUsage(AttributeTargets.Constructor | AttributeTargets.Method, Inherited = false)]
			[Conditional("CODE_ANALYSIS_CDF")]
			public sealed class NonThrowingAttribute : Attribute
			{
			}

			// Token: 0x020000AD RID: 173
			[AttributeUsage(AttributeTargets.Constructor | AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
			[Conditional("CODE_ANALYSIS_CDF")]
			public class ThrowsAttribute : Attribute
			{
				// Token: 0x060004A2 RID: 1186 RVA: 0x000143B4 File Offset: 0x000125B4
				public ThrowsAttribute(Type exceptionType, string diagnosis)
				{
					if (exceptionType == null)
					{
						throw Fx.Exception.ArgumentNull("exceptionType");
					}
					if (string.IsNullOrEmpty(diagnosis))
					{
						throw Fx.Exception.ArgumentNullOrEmpty("diagnosis");
					}
					this.exceptionType = exceptionType;
					this.diagnosis = diagnosis;
				}

				// Token: 0x170000E2 RID: 226
				// (get) Token: 0x060004A3 RID: 1187 RVA: 0x00014406 File Offset: 0x00012606
				public Type ExceptionType
				{
					get
					{
						return this.exceptionType;
					}
				}

				// Token: 0x170000E3 RID: 227
				// (get) Token: 0x060004A4 RID: 1188 RVA: 0x0001440E File Offset: 0x0001260E
				public string Diagnosis
				{
					get
					{
						return this.diagnosis;
					}
				}

				// Token: 0x04000315 RID: 789
				private readonly Type exceptionType;

				// Token: 0x04000316 RID: 790
				private readonly string diagnosis;
			}

			// Token: 0x020000AE RID: 174
			[AttributeUsage(AttributeTargets.Constructor | AttributeTargets.Method, Inherited = false)]
			[Conditional("CODE_ANALYSIS_CDF")]
			public sealed class InheritThrowsAttribute : Attribute
			{
				// Token: 0x170000E4 RID: 228
				// (get) Token: 0x060004A6 RID: 1190 RVA: 0x00014416 File Offset: 0x00012616
				// (set) Token: 0x060004A7 RID: 1191 RVA: 0x0001441E File Offset: 0x0001261E
				public Type FromDeclaringType { get; set; }

				// Token: 0x170000E5 RID: 229
				// (get) Token: 0x060004A8 RID: 1192 RVA: 0x00014427 File Offset: 0x00012627
				// (set) Token: 0x060004A9 RID: 1193 RVA: 0x0001442F File Offset: 0x0001262F
				public string From { get; set; }
			}

			// Token: 0x020000AF RID: 175
			[AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
			[Conditional("CODE_ANALYSIS_CDF")]
			public sealed class KnownXamlExternalAttribute : Attribute
			{
			}

			// Token: 0x020000B0 RID: 176
			[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple = false, Inherited = false)]
			[Conditional("CODE_ANALYSIS_CDF")]
			public sealed class XamlVisibleAttribute : Attribute
			{
				// Token: 0x060004AB RID: 1195 RVA: 0x00014438 File Offset: 0x00012638
				public XamlVisibleAttribute() : this(true)
				{
				}

				// Token: 0x060004AC RID: 1196 RVA: 0x00014441 File Offset: 0x00012641
				public XamlVisibleAttribute(bool visible)
				{
					this.Visible = visible;
				}

				// Token: 0x170000E6 RID: 230
				// (get) Token: 0x060004AD RID: 1197 RVA: 0x00014450 File Offset: 0x00012650
				// (set) Token: 0x060004AE RID: 1198 RVA: 0x00014458 File Offset: 0x00012658
				public bool Visible { get; private set; }
			}

			// Token: 0x020000B1 RID: 177
			[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Module | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum | AttributeTargets.Constructor | AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Event | AttributeTargets.Interface | AttributeTargets.Delegate, AllowMultiple = false, Inherited = false)]
			[Conditional("CODE_ANALYSIS_CDF")]
			public sealed class SecurityNoteAttribute : Attribute
			{
				// Token: 0x170000E7 RID: 231
				// (get) Token: 0x060004B0 RID: 1200 RVA: 0x00014461 File Offset: 0x00012661
				// (set) Token: 0x060004B1 RID: 1201 RVA: 0x00014469 File Offset: 0x00012669
				public string Critical { get; set; }

				// Token: 0x170000E8 RID: 232
				// (get) Token: 0x060004B2 RID: 1202 RVA: 0x00014472 File Offset: 0x00012672
				// (set) Token: 0x060004B3 RID: 1203 RVA: 0x0001447A File Offset: 0x0001267A
				public string Safe { get; set; }

				// Token: 0x170000E9 RID: 233
				// (get) Token: 0x060004B4 RID: 1204 RVA: 0x00014483 File Offset: 0x00012683
				// (set) Token: 0x060004B5 RID: 1205 RVA: 0x0001448B File Offset: 0x0001268B
				public string Miscellaneous { get; set; }
			}
		}

		// Token: 0x0200005F RID: 95
		private abstract class Thunk<T> where T : class
		{
			// Token: 0x06000386 RID: 902 RVA: 0x00011A03 File Offset: 0x0000FC03
			[SecuritySafeCritical]
			protected Thunk(T callback)
			{
				this.callback = callback;
			}

			// Token: 0x17000098 RID: 152
			// (get) Token: 0x06000387 RID: 903 RVA: 0x00011A12 File Offset: 0x0000FC12
			internal T Callback
			{
				[SecuritySafeCritical]
				get
				{
					return this.callback;
				}
			}

			// Token: 0x040001DA RID: 474
			[SecurityCritical]
			private T callback;
		}

		// Token: 0x02000060 RID: 96
		private sealed class ActionThunk<T1> : Fx.Thunk<Action<T1>>
		{
			// Token: 0x06000388 RID: 904 RVA: 0x00011A1A File Offset: 0x0000FC1A
			public ActionThunk(Action<T1> callback) : base(callback)
			{
			}

			// Token: 0x17000099 RID: 153
			// (get) Token: 0x06000389 RID: 905 RVA: 0x00011A23 File Offset: 0x0000FC23
			public Action<T1> ThunkFrame
			{
				get
				{
					return new Action<T1>(this.UnhandledExceptionFrame);
				}
			}

			// Token: 0x0600038A RID: 906 RVA: 0x00011A34 File Offset: 0x0000FC34
			[SecuritySafeCritical]
			private void UnhandledExceptionFrame(T1 result)
			{
				RuntimeHelpers.PrepareConstrainedRegions();
				try
				{
					base.Callback(result);
				}
				catch (Exception exception)
				{
					if (!Fx.HandleAtThreadBase(exception))
					{
						throw;
					}
				}
			}
		}

		// Token: 0x02000061 RID: 97
		private sealed class AsyncThunk : Fx.Thunk<AsyncCallback>
		{
			// Token: 0x0600038B RID: 907 RVA: 0x00011A74 File Offset: 0x0000FC74
			public AsyncThunk(AsyncCallback callback) : base(callback)
			{
			}

			// Token: 0x1700009A RID: 154
			// (get) Token: 0x0600038C RID: 908 RVA: 0x00011A7D File Offset: 0x0000FC7D
			public AsyncCallback ThunkFrame
			{
				get
				{
					return new AsyncCallback(this.UnhandledExceptionFrame);
				}
			}

			// Token: 0x0600038D RID: 909 RVA: 0x00011A8C File Offset: 0x0000FC8C
			[SecuritySafeCritical]
			private void UnhandledExceptionFrame(IAsyncResult result)
			{
				RuntimeHelpers.PrepareConstrainedRegions();
				try
				{
					base.Callback(result);
				}
				catch (Exception exception)
				{
					if (!Fx.HandleAtThreadBase(exception))
					{
						throw;
					}
				}
			}
		}

		// Token: 0x02000062 RID: 98
		private sealed class WaitThunk : Fx.Thunk<WaitCallback>
		{
			// Token: 0x0600038E RID: 910 RVA: 0x00011ACC File Offset: 0x0000FCCC
			public WaitThunk(WaitCallback callback) : base(callback)
			{
			}

			// Token: 0x1700009B RID: 155
			// (get) Token: 0x0600038F RID: 911 RVA: 0x00011AD5 File Offset: 0x0000FCD5
			public WaitCallback ThunkFrame
			{
				get
				{
					return new WaitCallback(this.UnhandledExceptionFrame);
				}
			}

			// Token: 0x06000390 RID: 912 RVA: 0x00011AE4 File Offset: 0x0000FCE4
			[SecuritySafeCritical]
			private void UnhandledExceptionFrame(object state)
			{
				RuntimeHelpers.PrepareConstrainedRegions();
				try
				{
					base.Callback(state);
				}
				catch (Exception exception)
				{
					if (!Fx.HandleAtThreadBase(exception))
					{
						throw;
					}
				}
			}
		}

		// Token: 0x02000063 RID: 99
		private sealed class TimerThunk : Fx.Thunk<TimerCallback>
		{
			// Token: 0x06000391 RID: 913 RVA: 0x00011B24 File Offset: 0x0000FD24
			public TimerThunk(TimerCallback callback) : base(callback)
			{
			}

			// Token: 0x1700009C RID: 156
			// (get) Token: 0x06000392 RID: 914 RVA: 0x00011B2D File Offset: 0x0000FD2D
			public TimerCallback ThunkFrame
			{
				get
				{
					return new TimerCallback(this.UnhandledExceptionFrame);
				}
			}

			// Token: 0x06000393 RID: 915 RVA: 0x00011B3C File Offset: 0x0000FD3C
			[SecuritySafeCritical]
			private void UnhandledExceptionFrame(object state)
			{
				RuntimeHelpers.PrepareConstrainedRegions();
				try
				{
					base.Callback(state);
				}
				catch (Exception exception)
				{
					if (!Fx.HandleAtThreadBase(exception))
					{
						throw;
					}
				}
			}
		}

		// Token: 0x02000064 RID: 100
		private sealed class WaitOrTimerThunk : Fx.Thunk<WaitOrTimerCallback>
		{
			// Token: 0x06000394 RID: 916 RVA: 0x00011B7C File Offset: 0x0000FD7C
			public WaitOrTimerThunk(WaitOrTimerCallback callback) : base(callback)
			{
			}

			// Token: 0x1700009D RID: 157
			// (get) Token: 0x06000395 RID: 917 RVA: 0x00011B85 File Offset: 0x0000FD85
			public WaitOrTimerCallback ThunkFrame
			{
				get
				{
					return new WaitOrTimerCallback(this.UnhandledExceptionFrame);
				}
			}

			// Token: 0x06000396 RID: 918 RVA: 0x00011B94 File Offset: 0x0000FD94
			[SecuritySafeCritical]
			private void UnhandledExceptionFrame(object state, bool timedOut)
			{
				RuntimeHelpers.PrepareConstrainedRegions();
				try
				{
					base.Callback(state, timedOut);
				}
				catch (Exception exception)
				{
					if (!Fx.HandleAtThreadBase(exception))
					{
						throw;
					}
				}
			}
		}

		// Token: 0x02000065 RID: 101
		private sealed class SendOrPostThunk : Fx.Thunk<SendOrPostCallback>
		{
			// Token: 0x06000397 RID: 919 RVA: 0x00011BD4 File Offset: 0x0000FDD4
			public SendOrPostThunk(SendOrPostCallback callback) : base(callback)
			{
			}

			// Token: 0x1700009E RID: 158
			// (get) Token: 0x06000398 RID: 920 RVA: 0x00011BDD File Offset: 0x0000FDDD
			public SendOrPostCallback ThunkFrame
			{
				get
				{
					return new SendOrPostCallback(this.UnhandledExceptionFrame);
				}
			}

			// Token: 0x06000399 RID: 921 RVA: 0x00011BEC File Offset: 0x0000FDEC
			[SecuritySafeCritical]
			private void UnhandledExceptionFrame(object state)
			{
				RuntimeHelpers.PrepareConstrainedRegions();
				try
				{
					base.Callback(state);
				}
				catch (Exception exception)
				{
					if (!Fx.HandleAtThreadBase(exception))
					{
						throw;
					}
				}
			}
		}

		// Token: 0x02000066 RID: 102
		[SecurityCritical]
		private sealed class IOCompletionThunk
		{
			// Token: 0x0600039A RID: 922 RVA: 0x00011C2C File Offset: 0x0000FE2C
			public IOCompletionThunk(IOCompletionCallback callback)
			{
				this.callback = callback;
			}

			// Token: 0x1700009F RID: 159
			// (get) Token: 0x0600039B RID: 923 RVA: 0x00011C3B File Offset: 0x0000FE3B
			public IOCompletionCallback ThunkFrame
			{
				get
				{
					return new IOCompletionCallback(this.UnhandledExceptionFrame);
				}
			}

			// Token: 0x0600039C RID: 924 RVA: 0x00011C4C File Offset: 0x0000FE4C
			private unsafe void UnhandledExceptionFrame(uint error, uint bytesRead, NativeOverlapped* nativeOverlapped)
			{
				RuntimeHelpers.PrepareConstrainedRegions();
				try
				{
					this.callback(error, bytesRead, nativeOverlapped);
				}
				catch (Exception exception)
				{
					if (!Fx.HandleAtThreadBase(exception))
					{
						throw;
					}
				}
			}

			// Token: 0x040001DB RID: 475
			private IOCompletionCallback callback;
		}

		// Token: 0x02000067 RID: 103
		[Serializable]
		private class InternalException : SystemException
		{
			// Token: 0x0600039D RID: 925 RVA: 0x00011C8C File Offset: 0x0000FE8C
			public InternalException(string description) : base(InternalSR.ShipAssertExceptionMessage(description))
			{
			}

			// Token: 0x0600039E RID: 926 RVA: 0x00003D9B File Offset: 0x00001F9B
			protected InternalException(SerializationInfo info, StreamingContext context) : base(info, context)
			{
			}
		}

		// Token: 0x02000068 RID: 104
		[Serializable]
		private class FatalInternalException : Fx.InternalException
		{
			// Token: 0x0600039F RID: 927 RVA: 0x00011C9A File Offset: 0x0000FE9A
			public FatalInternalException(string description) : base(description)
			{
			}

			// Token: 0x060003A0 RID: 928 RVA: 0x00011CA3 File Offset: 0x0000FEA3
			protected FatalInternalException(SerializationInfo info, StreamingContext context) : base(info, context)
			{
			}
		}
	}
}
