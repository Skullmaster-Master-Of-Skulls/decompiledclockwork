using System;
using System.Data;

namespace Databases
{
	// Token: 0x0200000A RID: 10
	public class CommandOverrideSettings
	{
		// Token: 0x17000014 RID: 20
		// (get) Token: 0x060000AA RID: 170 RVA: 0x00005E79 File Offset: 0x00004079
		// (set) Token: 0x060000AB RID: 171 RVA: 0x00005E81 File Offset: 0x00004081
		public int CommandTimeoutInSeconds { get; set; }

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x060000AC RID: 172 RVA: 0x00005E8A File Offset: 0x0000408A
		// (set) Token: 0x060000AD RID: 173 RVA: 0x00005E92 File Offset: 0x00004092
		public CommandBehavior CmdBehavior { get; set; }

		// Token: 0x060000AE RID: 174 RVA: 0x00005E9B File Offset: 0x0000409B
		public CommandOverrideSettings(CommandBehavior cmBehavior) : this(45, cmBehavior)
		{
		}

		// Token: 0x060000AF RID: 175 RVA: 0x00005EA8 File Offset: 0x000040A8
		public CommandOverrideSettings(int commandTimeoutInSeconds = 45) : this(commandTimeoutInSeconds, CommandBehavior.CloseConnection)
		{
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x00005EB5 File Offset: 0x000040B5
		public CommandOverrideSettings(int commandTimeoutInSeconds, CommandBehavior cmBehavior)
		{
			this.CommandTimeoutInSeconds = commandTimeoutInSeconds;
			this.CmdBehavior = cmBehavior;
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x060000B1 RID: 177 RVA: 0x00005ED0 File Offset: 0x000040D0
		public static CommandOverrideSettings CommandOverrideSettingsTimeout45
		{
			get
			{
				CommandOverrideSettings result;
				if ((result = CommandOverrideSettings._commandOverrideSettingsTimeout45) == null)
				{
					CommandOverrideSettings commandOverrideSettings = new CommandOverrideSettings(45);
					commandOverrideSettings.CommandTimeoutInSeconds = 45;
					result = commandOverrideSettings;
					CommandOverrideSettings._commandOverrideSettingsTimeout45 = commandOverrideSettings;
				}
				return result;
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x060000B2 RID: 178 RVA: 0x00005F04 File Offset: 0x00004104
		public static CommandOverrideSettings CommandOverrideSettingsTimeout120
		{
			get
			{
				CommandOverrideSettings result;
				if ((result = CommandOverrideSettings._commandOverrideSettingsTimeout120) == null)
				{
					CommandOverrideSettings commandOverrideSettings = new CommandOverrideSettings(45);
					commandOverrideSettings.CommandTimeoutInSeconds = 120;
					result = commandOverrideSettings;
					CommandOverrideSettings._commandOverrideSettingsTimeout120 = commandOverrideSettings;
				}
				return result;
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x060000B3 RID: 179 RVA: 0x00005F38 File Offset: 0x00004138
		public static CommandOverrideSettings CommandOverrideSettingsTimeout180
		{
			get
			{
				CommandOverrideSettings result;
				if ((result = CommandOverrideSettings._commandOverrideSettingsTimeout180) == null)
				{
					CommandOverrideSettings commandOverrideSettings = new CommandOverrideSettings(45);
					commandOverrideSettings.CommandTimeoutInSeconds = 180;
					result = commandOverrideSettings;
					CommandOverrideSettings._commandOverrideSettingsTimeout180 = commandOverrideSettings;
				}
				return result;
			}
		}

		// Token: 0x04000017 RID: 23
		private static CommandOverrideSettings _commandOverrideSettingsTimeout45;

		// Token: 0x04000018 RID: 24
		private static CommandOverrideSettings _commandOverrideSettingsTimeout120;

		// Token: 0x04000019 RID: 25
		private static CommandOverrideSettings _commandOverrideSettingsTimeout180;
	}
}
