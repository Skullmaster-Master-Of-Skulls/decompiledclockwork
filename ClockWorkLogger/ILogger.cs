using System;

namespace ClockWorkLogger
{
	// Token: 0x02000004 RID: 4
	public interface ILogger
	{
		// Token: 0x06000008 RID: 8
		void Debug(object obj);

		// Token: 0x06000009 RID: 9
		void Debug(string message);

		// Token: 0x0600000A RID: 10
		void Debug(string message, bool argument);

		// Token: 0x0600000B RID: 11
		void Debug(string message, byte argument);

		// Token: 0x0600000C RID: 12
		void Debug(string message, char argument);

		// Token: 0x0600000D RID: 13
		void Debug(string message, decimal argument);

		// Token: 0x0600000E RID: 14
		void Debug(string message, double argument);

		// Token: 0x0600000F RID: 15
		void Debug(string message, int argument);

		// Token: 0x06000010 RID: 16
		void Debug(string message, long argument);

		// Token: 0x06000011 RID: 17
		void Debug(string message, object argument);

		// Token: 0x06000012 RID: 18
		void Debug(string message, sbyte argument);

		// Token: 0x06000013 RID: 19
		void Debug(string message, float argument);

		// Token: 0x06000014 RID: 20
		void Debug(string message, string argument);

		// Token: 0x06000015 RID: 21
		void Debug(string message, uint argument);

		// Token: 0x06000016 RID: 22
		void Debug(string message, ulong argument);

		// Token: 0x06000017 RID: 23
		void Debug(string message, params object[] args);

		// Token: 0x06000018 RID: 24
		void Debug(string message, object arg1, object arg2);

		// Token: 0x06000019 RID: 25
		void Debug(string message, object arg1, object arg2, object arg3);

		// Token: 0x0600001A RID: 26
		void DebugException(string message, Exception exception);

		// Token: 0x0600001B RID: 27
		void Error(object obj);

		// Token: 0x0600001C RID: 28
		void Error(string message);

		// Token: 0x0600001D RID: 29
		void Error(string message, bool argument);

		// Token: 0x0600001E RID: 30
		void Error(string message, byte argument);

		// Token: 0x0600001F RID: 31
		void Error(string message, char argument);

		// Token: 0x06000020 RID: 32
		void Error(string message, decimal argument);

		// Token: 0x06000021 RID: 33
		void Error(string message, double argument);

		// Token: 0x06000022 RID: 34
		void Error(string message, int argument);

		// Token: 0x06000023 RID: 35
		void Error(string message, string argument);

		// Token: 0x06000024 RID: 36
		void Error(string message, params object[] args);

		// Token: 0x06000025 RID: 37
		void Error(string message, long argument);

		// Token: 0x06000026 RID: 38
		void Error(string message, object argument);

		// Token: 0x06000027 RID: 39
		void Error(string message, sbyte argument);

		// Token: 0x06000028 RID: 40
		void Error(string message, float argument);

		// Token: 0x06000029 RID: 41
		void Error(string message, uint argument);

		// Token: 0x0600002A RID: 42
		void Error(string message, ulong argument);

		// Token: 0x0600002B RID: 43
		void Error(string message, object arg1, object arg2);

		// Token: 0x0600002C RID: 44
		void Error(string message, object arg1, object arg2, object arg3);

		// Token: 0x0600002D RID: 45
		void ErrorException(string message, Exception exception);

		// Token: 0x0600002E RID: 46
		void Fatal(object obj);

		// Token: 0x0600002F RID: 47
		void Fatal(string message);

		// Token: 0x06000030 RID: 48
		void Fatal(string message, bool argument);

		// Token: 0x06000031 RID: 49
		void Fatal(string message, char argument);

		// Token: 0x06000032 RID: 50
		void Fatal(string message, int argument);

		// Token: 0x06000033 RID: 51
		void Fatal(string message, params object[] args);

		// Token: 0x06000034 RID: 52
		void Fatal(string message, byte argument);

		// Token: 0x06000035 RID: 53
		void Fatal(string message, decimal argument);

		// Token: 0x06000036 RID: 54
		void Fatal(string message, double argument);

		// Token: 0x06000037 RID: 55
		void Fatal(string message, long argument);

		// Token: 0x06000038 RID: 56
		void Fatal(string message, object argument);

		// Token: 0x06000039 RID: 57
		void Fatal(string message, sbyte argument);

		// Token: 0x0600003A RID: 58
		void Fatal(string message, float argument);

		// Token: 0x0600003B RID: 59
		void Fatal(string message, string argument);

		// Token: 0x0600003C RID: 60
		void Fatal(string message, uint argument);

		// Token: 0x0600003D RID: 61
		void Fatal(string message, ulong argument);

		// Token: 0x0600003E RID: 62
		void Fatal(string message, object arg1, object arg2);

		// Token: 0x0600003F RID: 63
		void Fatal(string message, object arg1, object arg2, object arg3);

		// Token: 0x06000040 RID: 64
		void FatalException(string message, Exception exception);

		// Token: 0x06000041 RID: 65
		void Info(object obj);

		// Token: 0x06000042 RID: 66
		void Info(string message);

		// Token: 0x06000043 RID: 67
		void Info(string message, bool argument);

		// Token: 0x06000044 RID: 68
		void Info(string message, byte argument);

		// Token: 0x06000045 RID: 69
		void Info(string message, char argument);

		// Token: 0x06000046 RID: 70
		void Info(string message, decimal argument);

		// Token: 0x06000047 RID: 71
		void Info(string message, double argument);

		// Token: 0x06000048 RID: 72
		void Info(string message, int argument);

		// Token: 0x06000049 RID: 73
		void Info(string message, long argument);

		// Token: 0x0600004A RID: 74
		void Info(string message, object argument);

		// Token: 0x0600004B RID: 75
		void Info(string message, sbyte argument);

		// Token: 0x0600004C RID: 76
		void Info(string message, float argument);

		// Token: 0x0600004D RID: 77
		void Info(string message, string argument);

		// Token: 0x0600004E RID: 78
		void Info(string message, ulong argument);

		// Token: 0x0600004F RID: 79
		void Info(string message, params object[] args);

		// Token: 0x06000050 RID: 80
		void Info(string message, uint argument);

		// Token: 0x06000051 RID: 81
		void Info(string message, object arg1, object arg2);

		// Token: 0x06000052 RID: 82
		void Info(string message, object arg1, object arg2, object arg3);

		// Token: 0x06000053 RID: 83
		void InfoException(string message, Exception exception);

		// Token: 0x06000054 RID: 84
		void Trace(object obj);

		// Token: 0x06000055 RID: 85
		void Trace(string message);

		// Token: 0x06000056 RID: 86
		void Trace(string message, bool argument);

		// Token: 0x06000057 RID: 87
		void Trace(string message, byte argument);

		// Token: 0x06000058 RID: 88
		void Trace(string message, char argument);

		// Token: 0x06000059 RID: 89
		void Trace(string message, decimal argument);

		// Token: 0x0600005A RID: 90
		void Trace(string message, double argument);

		// Token: 0x0600005B RID: 91
		void Trace(string message, int argument);

		// Token: 0x0600005C RID: 92
		void Trace(string message, long argument);

		// Token: 0x0600005D RID: 93
		void Trace(string message, object argument);

		// Token: 0x0600005E RID: 94
		void Trace(string message, sbyte argument);

		// Token: 0x0600005F RID: 95
		void Trace(string message, float argument);

		// Token: 0x06000060 RID: 96
		void Trace(string message, string argument);

		// Token: 0x06000061 RID: 97
		void Trace(string message, uint argument);

		// Token: 0x06000062 RID: 98
		void Trace(string message, params object[] args);

		// Token: 0x06000063 RID: 99
		void Trace(string message, ulong argument);

		// Token: 0x06000064 RID: 100
		void Trace(string message, object arg1, object arg2);

		// Token: 0x06000065 RID: 101
		void Trace(string message, object arg1, object arg2, object arg3);

		// Token: 0x06000066 RID: 102
		void TraceException(string message, Exception exception);

		// Token: 0x06000067 RID: 103
		void Warn(object obj);

		// Token: 0x06000068 RID: 104
		void Warn(string message);

		// Token: 0x06000069 RID: 105
		void Warn(string message, bool argument);

		// Token: 0x0600006A RID: 106
		void Warn(string message, byte argument);

		// Token: 0x0600006B RID: 107
		void Warn(string message, char argument);

		// Token: 0x0600006C RID: 108
		void Warn(string message, decimal argument);

		// Token: 0x0600006D RID: 109
		void Warn(string message, double argument);

		// Token: 0x0600006E RID: 110
		void Warn(string message, int argument);

		// Token: 0x0600006F RID: 111
		void Warn(string message, long argument);

		// Token: 0x06000070 RID: 112
		void Warn(string message, ulong argument);

		// Token: 0x06000071 RID: 113
		void Warn(string message, params object[] args);

		// Token: 0x06000072 RID: 114
		void Warn(string message, string argument);

		// Token: 0x06000073 RID: 115
		void Warn(string message, uint argument);

		// Token: 0x06000074 RID: 116
		void Warn(string message, float argument);

		// Token: 0x06000075 RID: 117
		void Warn(string message, object argument);

		// Token: 0x06000076 RID: 118
		void Warn(string message, sbyte argument);

		// Token: 0x06000077 RID: 119
		void Warn(string message, object arg1, object arg2, object arg3);

		// Token: 0x06000078 RID: 120
		void WarnException(string message, Exception exception);

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000079 RID: 121
		bool IsDebugEnabled { get; }

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600007A RID: 122
		bool IsErrorEnabled { get; }

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600007B RID: 123
		bool IsFatalEnabled { get; }

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600007C RID: 124
		bool IsInfoEnabled { get; }

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600007D RID: 125
		bool IsTraceEnabled { get; }

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600007E RID: 126
		bool IsWarnEnabled { get; }

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600007F RID: 127
		string Name { get; }
	}
}
