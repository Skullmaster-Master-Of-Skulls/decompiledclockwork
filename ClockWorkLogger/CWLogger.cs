using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using NLog;

namespace ClockWorkLogger
{
	// Token: 0x02000006 RID: 6
	public class CWLogger : ILogger
	{
		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000084 RID: 132 RVA: 0x00002358 File Offset: 0x00000558
		// (set) Token: 0x06000085 RID: 133 RVA: 0x0000236F File Offset: 0x0000056F
		public static bool ThrowExceptions
		{
			get
			{
				return LogManager.ThrowExceptions;
			}
			set
			{
				LogManager.ThrowExceptions = value;
			}
		}

		// Token: 0x14000001 RID: 1
		// (add) Token: 0x06000086 RID: 134 RVA: 0x0000237C File Offset: 0x0000057C
		// (remove) Token: 0x06000087 RID: 135 RVA: 0x000023B4 File Offset: 0x000005B4
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event LoggerReconfiguredEventHandler OnLoggerReconfigured;

		// Token: 0x06000088 RID: 136 RVA: 0x000023E9 File Offset: 0x000005E9
		protected CWLogger()
		{
			this._internalLogger = LogManager.GetCurrentClassLogger();
			this._internalLogger.LoggerReconfigured += delegate(object sender, EventArgs args)
			{
				this._internalLogger = LogManager.GetCurrentClassLogger();
				bool flag = this.OnLoggerReconfigured != null;
				if (flag)
				{
					this.OnLoggerReconfigured(this);
				}
			};
		}

		// Token: 0x06000089 RID: 137 RVA: 0x00002416 File Offset: 0x00000616
		protected CWLogger(string targetName)
		{
			this._internalLogger = LogManager.GetLogger(targetName);
			this._internalLogger.LoggerReconfigured += delegate(object sender, EventArgs args)
			{
				this._internalLogger = LogManager.GetCurrentClassLogger();
				bool flag = this.OnLoggerReconfigured != null;
				if (flag)
				{
					this.OnLoggerReconfigured(this);
				}
			};
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600008A RID: 138 RVA: 0x00002444 File Offset: 0x00000644
		public static CWLogger Logger
		{
			get
			{
				bool flag = CWLogger._instance == null;
				if (flag)
				{
					CWLogger._instance = new CWLogger();
				}
				return CWLogger._instance;
			}
		}

		// Token: 0x0600008B RID: 139 RVA: 0x00002474 File Offset: 0x00000674
		public static CWLogger GetLogger(string targetName)
		{
			bool flag = !CWLogger._targetCache.ContainsKey(targetName);
			if (flag)
			{
				CWLogger._targetCache.Add(targetName, new CWLogger(targetName));
			}
			return CWLogger._targetCache[targetName];
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600008C RID: 140 RVA: 0x000024B8 File Offset: 0x000006B8
		public string LogsDirectory
		{
			get
			{
				string name = Assembly.GetEntryAssembly().GetName().Name;
				string text = Path.Combine(Path.Combine(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "TechnoPro"), name), "Logs");
				return Directory.Exists(text) ? text : string.Empty;
			}
		}

		// Token: 0x0600008D RID: 141 RVA: 0x0000250C File Offset: 0x0000070C
		public void Debug(object obj)
		{
			this._internalLogger.Debug(obj);
		}

		// Token: 0x0600008E RID: 142 RVA: 0x0000251C File Offset: 0x0000071C
		public void Debug(string message)
		{
			this._internalLogger.Debug(message);
		}

		// Token: 0x0600008F RID: 143 RVA: 0x0000252C File Offset: 0x0000072C
		public void Debug(string message, bool argument)
		{
			this._internalLogger.Debug(message, argument);
		}

		// Token: 0x06000090 RID: 144 RVA: 0x0000253D File Offset: 0x0000073D
		public void Debug(string message, byte argument)
		{
			this._internalLogger.Debug(message, argument);
		}

		// Token: 0x06000091 RID: 145 RVA: 0x0000254E File Offset: 0x0000074E
		public void Debug(string message, char argument)
		{
			this._internalLogger.Debug(message, argument);
		}

		// Token: 0x06000092 RID: 146 RVA: 0x0000255F File Offset: 0x0000075F
		public void Debug(string message, decimal argument)
		{
			this._internalLogger.Debug(message, argument);
		}

		// Token: 0x06000093 RID: 147 RVA: 0x00002570 File Offset: 0x00000770
		public void Debug(string message, double argument)
		{
			this._internalLogger.Debug(message, argument);
		}

		// Token: 0x06000094 RID: 148 RVA: 0x00002581 File Offset: 0x00000781
		public void Debug(string message, int argument)
		{
			this._internalLogger.Debug(message, argument);
		}

		// Token: 0x06000095 RID: 149 RVA: 0x00002592 File Offset: 0x00000792
		public void Debug(string message, long argument)
		{
			this._internalLogger.Debug(message, argument);
		}

		// Token: 0x06000096 RID: 150 RVA: 0x000025A3 File Offset: 0x000007A3
		public void Debug(string message, object argument)
		{
			this._internalLogger.Debug(message, argument);
		}

		// Token: 0x06000097 RID: 151 RVA: 0x000025B4 File Offset: 0x000007B4
		public void Debug(string message, sbyte argument)
		{
			this._internalLogger.Debug(message, argument);
		}

		// Token: 0x06000098 RID: 152 RVA: 0x000025C5 File Offset: 0x000007C5
		public void Debug(string message, float argument)
		{
			this._internalLogger.Debug(message, argument);
		}

		// Token: 0x06000099 RID: 153 RVA: 0x000025D6 File Offset: 0x000007D6
		public void Debug(string message, string argument)
		{
			this._internalLogger.Debug(message, argument);
		}

		// Token: 0x0600009A RID: 154 RVA: 0x000025E7 File Offset: 0x000007E7
		public void Debug(string message, uint argument)
		{
			this._internalLogger.Debug(message, argument);
		}

		// Token: 0x0600009B RID: 155 RVA: 0x000025F8 File Offset: 0x000007F8
		public void Debug(string message, ulong argument)
		{
			this._internalLogger.Debug(message, argument);
		}

		// Token: 0x0600009C RID: 156 RVA: 0x00002609 File Offset: 0x00000809
		public void Debug(string message, params object[] args)
		{
			this._internalLogger.Debug(message, args);
		}

		// Token: 0x0600009D RID: 157 RVA: 0x0000261A File Offset: 0x0000081A
		public void Debug(string message, object arg1, object arg2)
		{
			this._internalLogger.Debug(message, arg1, arg2);
		}

		// Token: 0x0600009E RID: 158 RVA: 0x0000262C File Offset: 0x0000082C
		public void Debug(string message, object arg1, object arg2, object arg3)
		{
			this._internalLogger.Debug(message, arg1, arg2, arg3);
		}

		// Token: 0x0600009F RID: 159 RVA: 0x00002640 File Offset: 0x00000840
		public void DebugException(string message, Exception exception)
		{
			this._internalLogger.DebugException(message, exception);
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x00002651 File Offset: 0x00000851
		public void Error(object obj)
		{
			this._internalLogger.Error(obj);
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x00002661 File Offset: 0x00000861
		public void Error(string message)
		{
			this._internalLogger.Error(message);
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x00002671 File Offset: 0x00000871
		public void Error(string message, bool argument)
		{
			this._internalLogger.Error(message, argument);
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x00002682 File Offset: 0x00000882
		public void Error(string message, byte argument)
		{
			this._internalLogger.Error(message, argument);
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x00002693 File Offset: 0x00000893
		public void Error(string message, char argument)
		{
			this._internalLogger.Error(message, argument);
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x000026A4 File Offset: 0x000008A4
		public void Error(string message, decimal argument)
		{
			this._internalLogger.Error(message, argument);
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x000026B5 File Offset: 0x000008B5
		public void Error(string message, double argument)
		{
			this._internalLogger.Error(message, argument);
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x000026C6 File Offset: 0x000008C6
		public void Error(string message, int argument)
		{
			this._internalLogger.Error(message, argument);
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x000026D7 File Offset: 0x000008D7
		public void Error(string message, string argument)
		{
			this._internalLogger.Error(message, argument);
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x000026E8 File Offset: 0x000008E8
		public void Error(string message, params object[] args)
		{
			this._internalLogger.Error(message, args);
		}

		// Token: 0x060000AA RID: 170 RVA: 0x000026F9 File Offset: 0x000008F9
		public void Error(string message, long argument)
		{
			this._internalLogger.Error(message, argument);
		}

		// Token: 0x060000AB RID: 171 RVA: 0x0000270A File Offset: 0x0000090A
		public void Error(string message, object argument)
		{
			this._internalLogger.Error(message, argument);
		}

		// Token: 0x060000AC RID: 172 RVA: 0x0000271B File Offset: 0x0000091B
		public void Error(string message, sbyte argument)
		{
			this._internalLogger.Error(message, argument);
		}

		// Token: 0x060000AD RID: 173 RVA: 0x0000272C File Offset: 0x0000092C
		public void Error(string message, float argument)
		{
			this._internalLogger.Error(message, argument);
		}

		// Token: 0x060000AE RID: 174 RVA: 0x0000273D File Offset: 0x0000093D
		public void Error(string message, uint argument)
		{
			this._internalLogger.Error(message, argument);
		}

		// Token: 0x060000AF RID: 175 RVA: 0x0000274E File Offset: 0x0000094E
		public void Error(string message, ulong argument)
		{
			this._internalLogger.Error(message, argument);
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x0000275F File Offset: 0x0000095F
		public void Error(string message, object arg1, object arg2)
		{
			this._internalLogger.Error(message, arg1, arg2);
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x00002771 File Offset: 0x00000971
		public void Error(string message, object arg1, object arg2, object arg3)
		{
			this._internalLogger.Error(message, arg1, arg2, arg3);
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x00002785 File Offset: 0x00000985
		public void ErrorException(string message, Exception exception)
		{
			this._internalLogger.ErrorException(message, exception);
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x00002796 File Offset: 0x00000996
		public void Fatal(object obj)
		{
			this._internalLogger.Fatal(obj);
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x000027A6 File Offset: 0x000009A6
		public void Fatal(string message)
		{
			this._internalLogger.Fatal(message);
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x000027B6 File Offset: 0x000009B6
		public void Fatal(string message, bool argument)
		{
			this._internalLogger.Fatal(message, argument);
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x000027C7 File Offset: 0x000009C7
		public void Fatal(string message, char argument)
		{
			this._internalLogger.Fatal(message, argument);
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x000027D8 File Offset: 0x000009D8
		public void Fatal(string message, int argument)
		{
			this._internalLogger.Fatal(message, argument);
		}

		// Token: 0x060000B8 RID: 184 RVA: 0x000027E9 File Offset: 0x000009E9
		public void Fatal(string message, params object[] args)
		{
			this._internalLogger.Fatal(message, args);
		}

		// Token: 0x060000B9 RID: 185 RVA: 0x000027FA File Offset: 0x000009FA
		public void Fatal(string message, byte argument)
		{
			this._internalLogger.Fatal(message, argument);
		}

		// Token: 0x060000BA RID: 186 RVA: 0x0000280B File Offset: 0x00000A0B
		public void Fatal(string message, decimal argument)
		{
			this._internalLogger.Fatal(message, argument);
		}

		// Token: 0x060000BB RID: 187 RVA: 0x0000281C File Offset: 0x00000A1C
		public void Fatal(string message, double argument)
		{
			this._internalLogger.Fatal(message, argument);
		}

		// Token: 0x060000BC RID: 188 RVA: 0x0000282D File Offset: 0x00000A2D
		public void Fatal(string message, long argument)
		{
			this._internalLogger.Fatal(message, argument);
		}

		// Token: 0x060000BD RID: 189 RVA: 0x0000283E File Offset: 0x00000A3E
		public void Fatal(string message, object argument)
		{
			this._internalLogger.Fatal(message, argument);
		}

		// Token: 0x060000BE RID: 190 RVA: 0x0000284F File Offset: 0x00000A4F
		public void Fatal(string message, sbyte argument)
		{
			this._internalLogger.Fatal(message, argument);
		}

		// Token: 0x060000BF RID: 191 RVA: 0x00002860 File Offset: 0x00000A60
		public void Fatal(string message, float argument)
		{
			this._internalLogger.Fatal(message, argument);
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x00002871 File Offset: 0x00000A71
		public void Fatal(string message, string argument)
		{
			this._internalLogger.Fatal(message, argument);
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x00002882 File Offset: 0x00000A82
		public void Fatal(string message, uint argument)
		{
			this._internalLogger.Fatal(message, argument);
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x00002893 File Offset: 0x00000A93
		public void Fatal(string message, ulong argument)
		{
			this._internalLogger.Fatal(message, argument);
		}

		// Token: 0x060000C3 RID: 195 RVA: 0x000028A4 File Offset: 0x00000AA4
		public void Fatal(string message, object arg1, object arg2)
		{
			this._internalLogger.Fatal(message, arg1, arg2);
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x000028B6 File Offset: 0x00000AB6
		public void Fatal(string message, object arg1, object arg2, object arg3)
		{
			this._internalLogger.Fatal(message, arg1, arg2, arg3);
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x000028CA File Offset: 0x00000ACA
		public void FatalException(string message, Exception exception)
		{
			this._internalLogger.FatalException(message, exception);
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x000028DB File Offset: 0x00000ADB
		public void Info(object obj)
		{
			this._internalLogger.Info(obj);
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x000028EB File Offset: 0x00000AEB
		public void Info(string message)
		{
			this._internalLogger.Info(message);
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x000028FB File Offset: 0x00000AFB
		public void Info(string message, bool argument)
		{
			this._internalLogger.Info(message, argument);
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x0000290C File Offset: 0x00000B0C
		public void Info(string message, byte argument)
		{
			this._internalLogger.Info(message, argument);
		}

		// Token: 0x060000CA RID: 202 RVA: 0x0000291D File Offset: 0x00000B1D
		public void Info(string message, char argument)
		{
			this._internalLogger.Info(message, argument);
		}

		// Token: 0x060000CB RID: 203 RVA: 0x0000292E File Offset: 0x00000B2E
		public void Info(string message, decimal argument)
		{
			this._internalLogger.Info(message, argument);
		}

		// Token: 0x060000CC RID: 204 RVA: 0x0000293F File Offset: 0x00000B3F
		public void Info(string message, double argument)
		{
			this._internalLogger.Info(message, argument);
		}

		// Token: 0x060000CD RID: 205 RVA: 0x00002950 File Offset: 0x00000B50
		public void Info(string message, int argument)
		{
			this._internalLogger.Info(message, argument);
		}

		// Token: 0x060000CE RID: 206 RVA: 0x00002961 File Offset: 0x00000B61
		public void Info(string message, long argument)
		{
			this._internalLogger.Info(message, argument);
		}

		// Token: 0x060000CF RID: 207 RVA: 0x00002972 File Offset: 0x00000B72
		public void Info(string message, object argument)
		{
			this._internalLogger.Info(message, argument);
		}

		// Token: 0x060000D0 RID: 208 RVA: 0x00002983 File Offset: 0x00000B83
		public void Info(string message, sbyte argument)
		{
			this._internalLogger.Info(message, argument);
		}

		// Token: 0x060000D1 RID: 209 RVA: 0x00002994 File Offset: 0x00000B94
		public void Info(string message, float argument)
		{
			this._internalLogger.Info(message, argument);
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x000029A5 File Offset: 0x00000BA5
		public void Info(string message, string argument)
		{
			this._internalLogger.Info(message, argument);
		}

		// Token: 0x060000D3 RID: 211 RVA: 0x000029B6 File Offset: 0x00000BB6
		public void Info(string message, ulong argument)
		{
			this._internalLogger.Info(message, argument);
		}

		// Token: 0x060000D4 RID: 212 RVA: 0x000029C7 File Offset: 0x00000BC7
		public void Info(string message, params object[] args)
		{
			this._internalLogger.Info(message, args);
		}

		// Token: 0x060000D5 RID: 213 RVA: 0x000029D8 File Offset: 0x00000BD8
		public void Info(string message, uint argument)
		{
			this._internalLogger.Info(message, argument);
		}

		// Token: 0x060000D6 RID: 214 RVA: 0x000029E9 File Offset: 0x00000BE9
		public void Info(string message, object arg1, object arg2)
		{
			this._internalLogger.Info(message, arg1, arg2);
		}

		// Token: 0x060000D7 RID: 215 RVA: 0x000029FB File Offset: 0x00000BFB
		public void Info(string message, object arg1, object arg2, object arg3)
		{
			this._internalLogger.Info(message, arg1, arg2, arg3);
		}

		// Token: 0x060000D8 RID: 216 RVA: 0x00002A0F File Offset: 0x00000C0F
		public void InfoException(string message, Exception exception)
		{
			this._internalLogger.InfoException(message, exception);
		}

		// Token: 0x060000D9 RID: 217 RVA: 0x00002A20 File Offset: 0x00000C20
		public void Trace(object obj)
		{
			this._internalLogger.Trace(obj);
		}

		// Token: 0x060000DA RID: 218 RVA: 0x00002A30 File Offset: 0x00000C30
		public void Trace(string message)
		{
			this._internalLogger.Trace(message);
		}

		// Token: 0x060000DB RID: 219 RVA: 0x00002A40 File Offset: 0x00000C40
		public void Trace(string message, bool argument)
		{
			this._internalLogger.Trace(message, argument);
		}

		// Token: 0x060000DC RID: 220 RVA: 0x00002A51 File Offset: 0x00000C51
		public void Trace(string message, byte argument)
		{
			this._internalLogger.Trace(message, argument);
		}

		// Token: 0x060000DD RID: 221 RVA: 0x00002A62 File Offset: 0x00000C62
		public void Trace(string message, char argument)
		{
			this._internalLogger.Trace(message, argument);
		}

		// Token: 0x060000DE RID: 222 RVA: 0x00002A73 File Offset: 0x00000C73
		public void Trace(string message, decimal argument)
		{
			this._internalLogger.Trace(message, argument);
		}

		// Token: 0x060000DF RID: 223 RVA: 0x00002A84 File Offset: 0x00000C84
		public void Trace(string message, double argument)
		{
			this._internalLogger.Trace(message, argument);
		}

		// Token: 0x060000E0 RID: 224 RVA: 0x00002A95 File Offset: 0x00000C95
		public void Trace(string message, int argument)
		{
			this._internalLogger.Trace(message, argument);
		}

		// Token: 0x060000E1 RID: 225 RVA: 0x00002AA6 File Offset: 0x00000CA6
		public void Trace(string message, long argument)
		{
			this._internalLogger.Trace(message, argument);
		}

		// Token: 0x060000E2 RID: 226 RVA: 0x00002AB7 File Offset: 0x00000CB7
		public void Trace(string message, object argument)
		{
			this._internalLogger.Trace(message, argument);
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x00002AC8 File Offset: 0x00000CC8
		public void Trace(string message, sbyte argument)
		{
			this._internalLogger.Trace(message, argument);
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x00002AD9 File Offset: 0x00000CD9
		public void Trace(string message, float argument)
		{
			this._internalLogger.Trace(message, argument);
		}

		// Token: 0x060000E5 RID: 229 RVA: 0x00002AEA File Offset: 0x00000CEA
		public void Trace(string message, string argument)
		{
			this._internalLogger.Trace(message, argument);
		}

		// Token: 0x060000E6 RID: 230 RVA: 0x00002AFB File Offset: 0x00000CFB
		public void Trace(string message, uint argument)
		{
			this._internalLogger.Trace(message, argument);
		}

		// Token: 0x060000E7 RID: 231 RVA: 0x00002B0C File Offset: 0x00000D0C
		public void Trace(string message, params object[] args)
		{
			this._internalLogger.Trace(message, args);
		}

		// Token: 0x060000E8 RID: 232 RVA: 0x00002B1D File Offset: 0x00000D1D
		public void Trace(string message, ulong argument)
		{
			this._internalLogger.Trace(message, argument);
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x00002B2E File Offset: 0x00000D2E
		public void Trace(string message, object arg1, object arg2)
		{
			this._internalLogger.Trace(message, arg1, arg2);
		}

		// Token: 0x060000EA RID: 234 RVA: 0x00002B40 File Offset: 0x00000D40
		public void Trace(string message, object arg1, object arg2, object arg3)
		{
			this._internalLogger.Trace(message, arg1, arg2, arg3);
		}

		// Token: 0x060000EB RID: 235 RVA: 0x00002B54 File Offset: 0x00000D54
		public void TraceException(string message, Exception exception)
		{
			this._internalLogger.TraceException(message, exception);
		}

		// Token: 0x060000EC RID: 236 RVA: 0x00002B65 File Offset: 0x00000D65
		public void Warn(object obj)
		{
			this._internalLogger.Warn(obj);
		}

		// Token: 0x060000ED RID: 237 RVA: 0x00002B75 File Offset: 0x00000D75
		public void Warn(string message)
		{
			this._internalLogger.Warn(message);
		}

		// Token: 0x060000EE RID: 238 RVA: 0x00002B85 File Offset: 0x00000D85
		public void Warn(string message, bool argument)
		{
			this._internalLogger.Warn(message, argument);
		}

		// Token: 0x060000EF RID: 239 RVA: 0x00002B96 File Offset: 0x00000D96
		public void Warn(string message, byte argument)
		{
			this._internalLogger.Warn(message, argument);
		}

		// Token: 0x060000F0 RID: 240 RVA: 0x00002BA7 File Offset: 0x00000DA7
		public void Warn(string message, char argument)
		{
			this._internalLogger.Warn(message, argument);
		}

		// Token: 0x060000F1 RID: 241 RVA: 0x00002BB8 File Offset: 0x00000DB8
		public void Warn(string message, decimal argument)
		{
			this._internalLogger.Warn(message, argument);
		}

		// Token: 0x060000F2 RID: 242 RVA: 0x00002BC9 File Offset: 0x00000DC9
		public void Warn(string message, double argument)
		{
			this._internalLogger.Warn(message, argument);
		}

		// Token: 0x060000F3 RID: 243 RVA: 0x00002BDA File Offset: 0x00000DDA
		public void Warn(string message, int argument)
		{
			this._internalLogger.Warn(message, argument);
		}

		// Token: 0x060000F4 RID: 244 RVA: 0x00002BEB File Offset: 0x00000DEB
		public void Warn(string message, long argument)
		{
			this._internalLogger.Warn(message, argument);
		}

		// Token: 0x060000F5 RID: 245 RVA: 0x00002BFC File Offset: 0x00000DFC
		public void Warn(string message, ulong argument)
		{
			this._internalLogger.Warn(message, argument);
		}

		// Token: 0x060000F6 RID: 246 RVA: 0x00002C0D File Offset: 0x00000E0D
		public void Warn(string message, params object[] args)
		{
			this._internalLogger.Warn(message, args);
		}

		// Token: 0x060000F7 RID: 247 RVA: 0x00002C1E File Offset: 0x00000E1E
		public void Warn(string message, string argument)
		{
			this._internalLogger.Warn(message, argument);
		}

		// Token: 0x060000F8 RID: 248 RVA: 0x00002C2F File Offset: 0x00000E2F
		public void Warn(string message, uint argument)
		{
			this._internalLogger.Warn(message, argument);
		}

		// Token: 0x060000F9 RID: 249 RVA: 0x00002C40 File Offset: 0x00000E40
		public void Warn(string message, float argument)
		{
			this._internalLogger.Warn(message, argument);
		}

		// Token: 0x060000FA RID: 250 RVA: 0x00002C51 File Offset: 0x00000E51
		public void Warn(string message, object argument)
		{
			this._internalLogger.Warn(message, argument);
		}

		// Token: 0x060000FB RID: 251 RVA: 0x00002C62 File Offset: 0x00000E62
		public void Warn(string message, sbyte argument)
		{
			this._internalLogger.Warn(message, argument);
		}

		// Token: 0x060000FC RID: 252 RVA: 0x00002C73 File Offset: 0x00000E73
		public void Warn(string message, object arg1, object arg2)
		{
			this._internalLogger.Warn(message, arg1, arg2);
		}

		// Token: 0x060000FD RID: 253 RVA: 0x00002C85 File Offset: 0x00000E85
		public void Warn(string message, object arg1, object arg2, object arg3)
		{
			this._internalLogger.Warn(message, arg1, arg2, arg3);
		}

		// Token: 0x060000FE RID: 254 RVA: 0x00002C99 File Offset: 0x00000E99
		public void WarnException(string message, Exception exception)
		{
			this._internalLogger.WarnException(message, exception);
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x060000FF RID: 255 RVA: 0x00002CAC File Offset: 0x00000EAC
		public bool IsDebugEnabled
		{
			get
			{
				return this._internalLogger.IsDebugEnabled;
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000100 RID: 256 RVA: 0x00002CCC File Offset: 0x00000ECC
		public bool IsErrorEnabled
		{
			get
			{
				return this._internalLogger.IsErrorEnabled;
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000101 RID: 257 RVA: 0x00002CEC File Offset: 0x00000EEC
		public bool IsFatalEnabled
		{
			get
			{
				return this._internalLogger.IsFatalEnabled;
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000102 RID: 258 RVA: 0x00002D0C File Offset: 0x00000F0C
		public bool IsInfoEnabled
		{
			get
			{
				return this._internalLogger.IsInfoEnabled;
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000103 RID: 259 RVA: 0x00002D2C File Offset: 0x00000F2C
		public bool IsTraceEnabled
		{
			get
			{
				return this._internalLogger.IsTraceEnabled;
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000104 RID: 260 RVA: 0x00002D4C File Offset: 0x00000F4C
		public bool IsWarnEnabled
		{
			get
			{
				return this._internalLogger.IsWarnEnabled;
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000105 RID: 261 RVA: 0x00002D6C File Offset: 0x00000F6C
		public string Name
		{
			get
			{
				return this._internalLogger.Name;
			}
		}

		// Token: 0x04000002 RID: 2
		private Logger _internalLogger;

		// Token: 0x04000003 RID: 3
		private static CWLogger _instance;

		// Token: 0x04000004 RID: 4
		private static IDictionary<string, CWLogger> _targetCache = new Dictionary<string, CWLogger>();
	}
}
