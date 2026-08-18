using System;
using System.Net;
using System.Text;
using log4net.Core;
using log4net.Layout;
using log4net.Util;

namespace log4net.Appender
{
	// Token: 0x02000035 RID: 53
	public class RemoteSyslogAppender : UdpAppender
	{
		// Token: 0x060001E8 RID: 488 RVA: 0x000066E4 File Offset: 0x000048E4
		public RemoteSyslogAppender()
		{
			base.RemotePort = 514;
			base.RemoteAddress = IPAddress.Parse("127.0.0.1");
			base.Encoding = Encoding.ASCII;
		}

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x060001E9 RID: 489 RVA: 0x00006724 File Offset: 0x00004924
		// (set) Token: 0x060001EA RID: 490 RVA: 0x0000672C File Offset: 0x0000492C
		public PatternLayout Identity
		{
			get
			{
				return this.m_identity;
			}
			set
			{
				this.m_identity = value;
			}
		}

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x060001EB RID: 491 RVA: 0x00006735 File Offset: 0x00004935
		// (set) Token: 0x060001EC RID: 492 RVA: 0x0000673D File Offset: 0x0000493D
		public RemoteSyslogAppender.SyslogFacility Facility
		{
			get
			{
				return this.m_facility;
			}
			set
			{
				this.m_facility = value;
			}
		}

		// Token: 0x060001ED RID: 493 RVA: 0x00006746 File Offset: 0x00004946
		public void AddMapping(RemoteSyslogAppender.LevelSeverity mapping)
		{
			this.m_levelMapping.Add(mapping);
		}

		// Token: 0x060001EE RID: 494 RVA: 0x00006754 File Offset: 0x00004954
		protected override void Append(LoggingEvent loggingEvent)
		{
			try
			{
				int value = RemoteSyslogAppender.GeneratePriority(this.m_facility, this.GetSeverity(loggingEvent.Level));
				string value2;
				if (this.m_identity != null)
				{
					value2 = this.m_identity.Format(loggingEvent);
				}
				else
				{
					value2 = loggingEvent.Domain;
				}
				string text = base.RenderLoggingEvent(loggingEvent);
				int i = 0;
				StringBuilder stringBuilder = new StringBuilder();
				while (i < text.Length)
				{
					stringBuilder.Length = 0;
					stringBuilder.Append('<');
					stringBuilder.Append(value);
					stringBuilder.Append('>');
					stringBuilder.Append(value2);
					stringBuilder.Append(": ");
					while (i < text.Length)
					{
						char c = text[i];
						if (c >= ' ' && c <= '~')
						{
							stringBuilder.Append(c);
						}
						else if (c == '\r' || c == '\n')
						{
							if (text.Length > i + 1 && (text[i + 1] == '\r' || text[i + 1] == '\n'))
							{
								i++;
							}
							i++;
							break;
						}
						i++;
					}
					byte[] bytes = base.Encoding.GetBytes(stringBuilder.ToString());
					base.Client.Send(bytes, bytes.Length, base.RemoteEndPoint);
				}
			}
			catch (Exception e)
			{
				this.ErrorHandler.Error(string.Concat(new object[]
				{
					"Unable to send logging event to remote syslog ",
					base.RemoteAddress.ToString(),
					" on port ",
					base.RemotePort,
					"."
				}), e, ErrorCode.WriteFailure);
			}
		}

		// Token: 0x060001EF RID: 495 RVA: 0x00006910 File Offset: 0x00004B10
		public override void ActivateOptions()
		{
			base.ActivateOptions();
			this.m_levelMapping.ActivateOptions();
		}

		// Token: 0x060001F0 RID: 496 RVA: 0x00006924 File Offset: 0x00004B24
		protected virtual RemoteSyslogAppender.SyslogSeverity GetSeverity(Level level)
		{
			RemoteSyslogAppender.LevelSeverity levelSeverity = this.m_levelMapping.Lookup(level) as RemoteSyslogAppender.LevelSeverity;
			if (levelSeverity != null)
			{
				return levelSeverity.Severity;
			}
			if (level >= Level.Alert)
			{
				return RemoteSyslogAppender.SyslogSeverity.Alert;
			}
			if (level >= Level.Critical)
			{
				return RemoteSyslogAppender.SyslogSeverity.Critical;
			}
			if (level >= Level.Error)
			{
				return RemoteSyslogAppender.SyslogSeverity.Error;
			}
			if (level >= Level.Warn)
			{
				return RemoteSyslogAppender.SyslogSeverity.Warning;
			}
			if (level >= Level.Notice)
			{
				return RemoteSyslogAppender.SyslogSeverity.Notice;
			}
			if (level >= Level.Info)
			{
				return RemoteSyslogAppender.SyslogSeverity.Informational;
			}
			return RemoteSyslogAppender.SyslogSeverity.Debug;
		}

		// Token: 0x060001F1 RID: 497 RVA: 0x000069A8 File Offset: 0x00004BA8
		public static int GeneratePriority(RemoteSyslogAppender.SyslogFacility facility, RemoteSyslogAppender.SyslogSeverity severity)
		{
			if (facility < RemoteSyslogAppender.SyslogFacility.Kernel || facility > RemoteSyslogAppender.SyslogFacility.Local7)
			{
				throw new ArgumentException("SyslogFacility out of range", "facility");
			}
			if (severity < RemoteSyslogAppender.SyslogSeverity.Emergency || severity > RemoteSyslogAppender.SyslogSeverity.Debug)
			{
				throw new ArgumentException("SyslogSeverity out of range", "severity");
			}
			return (int)(facility * RemoteSyslogAppender.SyslogFacility.Uucp + (int)severity);
		}

		// Token: 0x040000D3 RID: 211
		private const int DefaultSyslogPort = 514;

		// Token: 0x040000D4 RID: 212
		private const int c_renderBufferSize = 256;

		// Token: 0x040000D5 RID: 213
		private const int c_renderBufferMaxCapacity = 1024;

		// Token: 0x040000D6 RID: 214
		private RemoteSyslogAppender.SyslogFacility m_facility = RemoteSyslogAppender.SyslogFacility.User;

		// Token: 0x040000D7 RID: 215
		private PatternLayout m_identity;

		// Token: 0x040000D8 RID: 216
		private LevelMapping m_levelMapping = new LevelMapping();

		// Token: 0x02000036 RID: 54
		public enum SyslogSeverity
		{
			// Token: 0x040000DA RID: 218
			Emergency,
			// Token: 0x040000DB RID: 219
			Alert,
			// Token: 0x040000DC RID: 220
			Critical,
			// Token: 0x040000DD RID: 221
			Error,
			// Token: 0x040000DE RID: 222
			Warning,
			// Token: 0x040000DF RID: 223
			Notice,
			// Token: 0x040000E0 RID: 224
			Informational,
			// Token: 0x040000E1 RID: 225
			Debug
		}

		// Token: 0x02000037 RID: 55
		public enum SyslogFacility
		{
			// Token: 0x040000E3 RID: 227
			Kernel,
			// Token: 0x040000E4 RID: 228
			User,
			// Token: 0x040000E5 RID: 229
			Mail,
			// Token: 0x040000E6 RID: 230
			Daemons,
			// Token: 0x040000E7 RID: 231
			Authorization,
			// Token: 0x040000E8 RID: 232
			Syslog,
			// Token: 0x040000E9 RID: 233
			Printer,
			// Token: 0x040000EA RID: 234
			News,
			// Token: 0x040000EB RID: 235
			Uucp,
			// Token: 0x040000EC RID: 236
			Clock,
			// Token: 0x040000ED RID: 237
			Authorization2,
			// Token: 0x040000EE RID: 238
			Ftp,
			// Token: 0x040000EF RID: 239
			Ntp,
			// Token: 0x040000F0 RID: 240
			Audit,
			// Token: 0x040000F1 RID: 241
			Alert,
			// Token: 0x040000F2 RID: 242
			Clock2,
			// Token: 0x040000F3 RID: 243
			Local0,
			// Token: 0x040000F4 RID: 244
			Local1,
			// Token: 0x040000F5 RID: 245
			Local2,
			// Token: 0x040000F6 RID: 246
			Local3,
			// Token: 0x040000F7 RID: 247
			Local4,
			// Token: 0x040000F8 RID: 248
			Local5,
			// Token: 0x040000F9 RID: 249
			Local6,
			// Token: 0x040000FA RID: 250
			Local7
		}

		// Token: 0x02000038 RID: 56
		public class LevelSeverity : LevelMappingEntry
		{
			// Token: 0x17000079 RID: 121
			// (get) Token: 0x060001F2 RID: 498 RVA: 0x000069E0 File Offset: 0x00004BE0
			// (set) Token: 0x060001F3 RID: 499 RVA: 0x000069E8 File Offset: 0x00004BE8
			public RemoteSyslogAppender.SyslogSeverity Severity
			{
				get
				{
					return this.m_severity;
				}
				set
				{
					this.m_severity = value;
				}
			}

			// Token: 0x040000FB RID: 251
			private RemoteSyslogAppender.SyslogSeverity m_severity;
		}
	}
}
