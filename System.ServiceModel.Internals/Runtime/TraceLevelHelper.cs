using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace System.Runtime
{
	// Token: 0x02000032 RID: 50
	internal class TraceLevelHelper
	{
		// Token: 0x0600018D RID: 397 RVA: 0x00006CE8 File Offset: 0x00004EE8
		internal static TraceEventType GetTraceEventType(byte level, byte opcode)
		{
			if (opcode <= 2)
			{
				if (opcode == 1)
				{
					return TraceEventType.Start;
				}
				if (opcode == 2)
				{
					return TraceEventType.Stop;
				}
			}
			else
			{
				if (opcode == 7)
				{
					return TraceEventType.Resume;
				}
				if (opcode == 8)
				{
					return TraceEventType.Suspend;
				}
			}
			return TraceLevelHelper.EtwLevelToTraceEventType[(int)level];
		}

		// Token: 0x0600018E RID: 398 RVA: 0x00006D21 File Offset: 0x00004F21
		internal static TraceEventType GetTraceEventType(TraceEventLevel level)
		{
			return TraceLevelHelper.EtwLevelToTraceEventType[(int)level];
		}

		// Token: 0x0600018F RID: 399 RVA: 0x00006D21 File Offset: 0x00004F21
		internal static TraceEventType GetTraceEventType(byte level)
		{
			return TraceLevelHelper.EtwLevelToTraceEventType[(int)level];
		}

		// Token: 0x06000190 RID: 400 RVA: 0x00006D2C File Offset: 0x00004F2C
		internal static string LookupSeverity(TraceEventLevel level, TraceEventOpcode opcode)
		{
			if (opcode <= TraceEventOpcode.Stop)
			{
				if (opcode == TraceEventOpcode.Start)
				{
					return "Start";
				}
				if (opcode == TraceEventOpcode.Stop)
				{
					return "Stop";
				}
			}
			else
			{
				if (opcode == TraceEventOpcode.Resume)
				{
					return "Resume";
				}
				if (opcode == TraceEventOpcode.Suspend)
				{
					return "Suspend";
				}
			}
			string result;
			switch (level)
			{
			case TraceEventLevel.Critical:
				result = "Critical";
				break;
			case TraceEventLevel.Error:
				result = "Error";
				break;
			case TraceEventLevel.Warning:
				result = "Warning";
				break;
			case TraceEventLevel.Informational:
				result = "Information";
				break;
			case TraceEventLevel.Verbose:
				result = "Verbose";
				break;
			default:
				result = level.ToString();
				break;
			}
			return result;
		}

		// Token: 0x06000192 RID: 402 RVA: 0x00006DC6 File Offset: 0x00004FC6
		// Note: this type is marked as 'beforefieldinit'.
		static TraceLevelHelper()
		{
			TraceEventType[] array = new TraceEventType[6];
			RuntimeHelpers.InitializeArray(array, fieldof(<PrivateImplementationDetails>.1812FFD58290AC7DDA7A88832F32082655D69F735E8B764AD679F9A0D19AE462).FieldHandle);
			TraceLevelHelper.EtwLevelToTraceEventType = array;
		}

		// Token: 0x040000C5 RID: 197
		private static TraceEventType[] EtwLevelToTraceEventType;
	}
}
