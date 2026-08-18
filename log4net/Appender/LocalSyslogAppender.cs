using System;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using log4net.Core;
using log4net.Util;

namespace log4net.Appender
{
	// Token: 0x0200002B RID: 43
	public class LocalSyslogAppender : AppenderSkeleton
	{
		// Token: 0x1700005E RID: 94
		// (get) Token: 0x0600019D RID: 413 RVA: 0x00005CB9 File Offset: 0x00003EB9
		// (set) Token: 0x0600019E RID: 414 RVA: 0x00005CC1 File Offset: 0x00003EC1
		public string Identity
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

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x0600019F RID: 415 RVA: 0x00005CCA File Offset: 0x00003ECA
		// (set) Token: 0x060001A0 RID: 416 RVA: 0x00005CD2 File Offset: 0x00003ED2
		public LocalSyslogAppender.SyslogFacility Facility
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

		// Token: 0x060001A1 RID: 417 RVA: 0x00005CDB File Offset: 0x00003EDB
		public void AddMapping(LocalSyslogAppender.LevelSeverity mapping)
		{
			this.m_levelMapping.Add(mapping);
		}

		// Token: 0x060001A2 RID: 418 RVA: 0x00005CEC File Offset: 0x00003EEC
		[SecuritySafeCritical]
		public override void ActivateOptions()
		{
			base.ActivateOptions();
			this.m_levelMapping.ActivateOptions();
			string text = this.m_identity;
			if (text == null)
			{
				text = SystemInfo.ApplicationFriendlyName;
			}
			this.m_handleToIdentity = Marshal.StringToHGlobalAnsi(text);
			LocalSyslogAppender.openlog(this.m_handleToIdentity, 1, this.m_facility);
		}

		// Token: 0x060001A3 RID: 419 RVA: 0x00005D38 File Offset: 0x00003F38
		[SecuritySafeCritical]
		[SecurityPermission(SecurityAction.Demand, UnmanagedCode = true)]
		protected override void Append(LoggingEvent loggingEvent)
		{
			int priority = LocalSyslogAppender.GeneratePriority(this.m_facility, this.GetSeverity(loggingEvent.Level));
			string message = base.RenderLoggingEvent(loggingEvent);
			LocalSyslogAppender.syslog(priority, "%s", message);
		}

		// Token: 0x060001A4 RID: 420 RVA: 0x00005D74 File Offset: 0x00003F74
		[SecuritySafeCritical]
		protected override void OnClose()
		{
			base.OnClose();
			try
			{
				LocalSyslogAppender.closelog();
			}
			catch (DllNotFoundException)
			{
			}
			if (this.m_handleToIdentity != IntPtr.Zero)
			{
				Marshal.FreeHGlobal(this.m_handleToIdentity);
			}
		}

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x060001A5 RID: 421 RVA: 0x00005DC0 File Offset: 0x00003FC0
		protected override bool RequiresLayout
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060001A6 RID: 422 RVA: 0x00005DC4 File Offset: 0x00003FC4
		protected virtual LocalSyslogAppender.SyslogSeverity GetSeverity(Level level)
		{
			LocalSyslogAppender.LevelSeverity levelSeverity = this.m_levelMapping.Lookup(level) as LocalSyslogAppender.LevelSeverity;
			if (levelSeverity != null)
			{
				return levelSeverity.Severity;
			}
			if (level >= Level.Alert)
			{
				return LocalSyslogAppender.SyslogSeverity.Alert;
			}
			if (level >= Level.Critical)
			{
				return LocalSyslogAppender.SyslogSeverity.Critical;
			}
			if (level >= Level.Error)
			{
				return LocalSyslogAppender.SyslogSeverity.Error;
			}
			if (level >= Level.Warn)
			{
				return LocalSyslogAppender.SyslogSeverity.Warning;
			}
			if (level >= Level.Notice)
			{
				return LocalSyslogAppender.SyslogSeverity.Notice;
			}
			if (level >= Level.Info)
			{
				return LocalSyslogAppender.SyslogSeverity.Informational;
			}
			return LocalSyslogAppender.SyslogSeverity.Debug;
		}

		// Token: 0x060001A7 RID: 423 RVA: 0x00005E48 File Offset: 0x00004048
		private static int GeneratePriority(LocalSyslogAppender.SyslogFacility facility, LocalSyslogAppender.SyslogSeverity severity)
		{
			return (int)(facility * LocalSyslogAppender.SyslogFacility.Uucp + (int)severity);
		}

		// Token: 0x060001A8 RID: 424
		[DllImport("libc")]
		private static extern void openlog(IntPtr ident, int option, LocalSyslogAppender.SyslogFacility facility);

		// Token: 0x060001A9 RID: 425
		[DllImport("libc", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
		private static extern void syslog(int priority, string format, string message);

		// Token: 0x060001AA RID: 426
		[DllImport("libc")]
		private static extern void closelog();

		// Token: 0x04000098 RID: 152
		private LocalSyslogAppender.SyslogFacility m_facility = LocalSyslogAppender.SyslogFacility.User;

		// Token: 0x04000099 RID: 153
		private string m_identity;

		// Token: 0x0400009A RID: 154
		private IntPtr m_handleToIdentity = IntPtr.Zero;

		// Token: 0x0400009B RID: 155
		private LevelMapping m_levelMapping = new LevelMapping();

		// Token: 0x0200002C RID: 44
		public enum SyslogSeverity
		{
			// Token: 0x0400009D RID: 157
			Emergency,
			// Token: 0x0400009E RID: 158
			Alert,
			// Token: 0x0400009F RID: 159
			Critical,
			// Token: 0x040000A0 RID: 160
			Error,
			// Token: 0x040000A1 RID: 161
			Warning,
			// Token: 0x040000A2 RID: 162
			Notice,
			// Token: 0x040000A3 RID: 163
			Informational,
			// Token: 0x040000A4 RID: 164
			Debug
		}

		// Token: 0x0200002D RID: 45
		public enum SyslogFacility
		{
			// Token: 0x040000A6 RID: 166
			Kernel,
			// Token: 0x040000A7 RID: 167
			User,
			// Token: 0x040000A8 RID: 168
			Mail,
			// Token: 0x040000A9 RID: 169
			Daemons,
			// Token: 0x040000AA RID: 170
			Authorization,
			// Token: 0x040000AB RID: 171
			Syslog,
			// Token: 0x040000AC RID: 172
			Printer,
			// Token: 0x040000AD RID: 173
			News,
			// Token: 0x040000AE RID: 174
			Uucp,
			// Token: 0x040000AF RID: 175
			Clock,
			// Token: 0x040000B0 RID: 176
			Authorization2,
			// Token: 0x040000B1 RID: 177
			Ftp,
			// Token: 0x040000B2 RID: 178
			Ntp,
			// Token: 0x040000B3 RID: 179
			Audit,
			// Token: 0x040000B4 RID: 180
			Alert,
			// Token: 0x040000B5 RID: 181
			Clock2,
			// Token: 0x040000B6 RID: 182
			Local0,
			// Token: 0x040000B7 RID: 183
			Local1,
			// Token: 0x040000B8 RID: 184
			Local2,
			// Token: 0x040000B9 RID: 185
			Local3,
			// Token: 0x040000BA RID: 186
			Local4,
			// Token: 0x040000BB RID: 187
			Local5,
			// Token: 0x040000BC RID: 188
			Local6,
			// Token: 0x040000BD RID: 189
			Local7
		}

		// Token: 0x0200002E RID: 46
		public class LevelSeverity : LevelMappingEntry
		{
			// Token: 0x17000061 RID: 97
			// (get) Token: 0x060001AB RID: 427 RVA: 0x00005E4F File Offset: 0x0000404F
			// (set) Token: 0x060001AC RID: 428 RVA: 0x00005E57 File Offset: 0x00004057
			public LocalSyslogAppender.SyslogSeverity Severity
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

			// Token: 0x040000BE RID: 190
			private LocalSyslogAppender.SyslogSeverity m_severity;
		}
	}
}
