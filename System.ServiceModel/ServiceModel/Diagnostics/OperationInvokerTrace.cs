using System;
using System.Diagnostics;
using System.Reflection;

namespace System.ServiceModel.Diagnostics
{
	// Token: 0x02000A85 RID: 2693
	internal static class OperationInvokerTrace
	{
		// Token: 0x17001951 RID: 6481
		// (get) Token: 0x06006A40 RID: 27200 RVA: 0x0018C4A2 File Offset: 0x0018A6A2
		internal static SourceSwitch CodeGenerationSwitch
		{
			get
			{
				return OperationInvokerTrace.CodeGenerationTraceSource.Switch;
			}
		}

		// Token: 0x06006A41 RID: 27201 RVA: 0x0018C4AE File Offset: 0x0018A6AE
		internal static void WriteInstruction(int lineNumber, string instruction)
		{
			OperationInvokerTrace.CodeGenerationTraceSource.TraceInformation("{0:00000}: {1}", new object[]
			{
				lineNumber,
				instruction
			});
		}

		// Token: 0x17001952 RID: 6482
		// (get) Token: 0x06006A42 RID: 27202 RVA: 0x0018C4D2 File Offset: 0x0018A6D2
		internal static MethodInfo TraceInstructionMethod
		{
			get
			{
				if (OperationInvokerTrace.traceInstructionMethod == null)
				{
					OperationInvokerTrace.traceInstructionMethod = typeof(OperationInvokerTrace).GetMethod("TraceInstruction", BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
				}
				return OperationInvokerTrace.traceInstructionMethod;
			}
		}

		// Token: 0x06006A43 RID: 27203 RVA: 0x0018C501 File Offset: 0x0018A701
		internal static void TraceInstruction(string instruction)
		{
			OperationInvokerTrace.CodeGenerationTraceSource.TraceEvent(TraceEventType.Verbose, 0, instruction);
		}

		// Token: 0x17001953 RID: 6483
		// (get) Token: 0x06006A44 RID: 27204 RVA: 0x0018C511 File Offset: 0x0018A711
		private static TraceSource CodeGenerationTraceSource
		{
			get
			{
				if (OperationInvokerTrace.codeGenSource == null)
				{
					OperationInvokerTrace.codeGenSource = new TraceSource("System.ServiceModel.OperationInvoker.CodeGeneration");
				}
				return OperationInvokerTrace.codeGenSource;
			}
		}

		// Token: 0x04003CA6 RID: 15526
		private static TraceSource codeGenSource;

		// Token: 0x04003CA7 RID: 15527
		private static MethodInfo traceInstructionMethod;
	}
}
