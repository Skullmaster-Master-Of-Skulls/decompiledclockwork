using System;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Text;
using Microsoft.Win32;

namespace System.Diagnostics
{
	// Token: 0x02000751 RID: 1873
	[DesignTimeVisible(false)]
	[ToolboxItem(false)]
	[Serializable]
	public sealed class EventLogEntry : Component, ISerializable
	{
		// Token: 0x06003973 RID: 14707 RVA: 0x000F3D16 File Offset: 0x000F2D16
		internal EventLogEntry(byte[] buf, int offset, EventLog log)
		{
			this.dataBuf = buf;
			this.bufOffset = offset;
			this.owner = log;
			GC.SuppressFinalize(this);
		}

		// Token: 0x06003974 RID: 14708 RVA: 0x000F3D3C File Offset: 0x000F2D3C
		private EventLogEntry(SerializationInfo info, StreamingContext context)
		{
			this.dataBuf = (byte[])info.GetValue("DataBuffer", typeof(byte[]));
			string @string = info.GetString("LogName");
			string string2 = info.GetString("MachineName");
			this.owner = new EventLog(@string, string2, "");
			GC.SuppressFinalize(this);
		}

		// Token: 0x17000D4E RID: 3406
		// (get) Token: 0x06003975 RID: 14709 RVA: 0x000F3DA0 File Offset: 0x000F2DA0
		[MonitoringDescription("LogEntryMachineName")]
		public string MachineName
		{
			get
			{
				int num = this.bufOffset + 56;
				while (this.CharFrom(this.dataBuf, num) != '\0')
				{
					num += 2;
				}
				num += 2;
				char value = this.CharFrom(this.dataBuf, num);
				StringBuilder stringBuilder = new StringBuilder();
				while (value != '\0')
				{
					stringBuilder.Append(value);
					num += 2;
					value = this.CharFrom(this.dataBuf, num);
				}
				return stringBuilder.ToString();
			}
		}

		// Token: 0x17000D4F RID: 3407
		// (get) Token: 0x06003976 RID: 14710 RVA: 0x000F3E0C File Offset: 0x000F2E0C
		[MonitoringDescription("LogEntryData")]
		public byte[] Data
		{
			get
			{
				int num = this.IntFrom(this.dataBuf, this.bufOffset + 48);
				byte[] array = new byte[num];
				Array.Copy(this.dataBuf, this.bufOffset + this.IntFrom(this.dataBuf, this.bufOffset + 52), array, 0, num);
				return array;
			}
		}

		// Token: 0x17000D50 RID: 3408
		// (get) Token: 0x06003977 RID: 14711 RVA: 0x000F3E61 File Offset: 0x000F2E61
		[MonitoringDescription("LogEntryIndex")]
		public int Index
		{
			get
			{
				return this.IntFrom(this.dataBuf, this.bufOffset + 8);
			}
		}

		// Token: 0x17000D51 RID: 3409
		// (get) Token: 0x06003978 RID: 14712 RVA: 0x000F3E78 File Offset: 0x000F2E78
		[MonitoringDescription("LogEntryCategory")]
		public string Category
		{
			get
			{
				if (this.category == null)
				{
					string messageLibraryNames = this.GetMessageLibraryNames("CategoryMessageFile");
					string text = this.owner.FormatMessageWrapper(messageLibraryNames, (uint)this.CategoryNumber, null);
					if (text == null)
					{
						this.category = "(" + this.CategoryNumber.ToString(CultureInfo.CurrentCulture) + ")";
					}
					else
					{
						this.category = text;
					}
				}
				return this.category;
			}
		}

		// Token: 0x17000D52 RID: 3410
		// (get) Token: 0x06003979 RID: 14713 RVA: 0x000F3EE7 File Offset: 0x000F2EE7
		[MonitoringDescription("LogEntryCategoryNumber")]
		public short CategoryNumber
		{
			get
			{
				return this.ShortFrom(this.dataBuf, this.bufOffset + 28);
			}
		}

		// Token: 0x17000D53 RID: 3411
		// (get) Token: 0x0600397A RID: 14714 RVA: 0x000F3EFE File Offset: 0x000F2EFE
		[Obsolete("This property has been deprecated.  Please use System.Diagnostics.EventLogEntry.InstanceId instead.  http://go.microsoft.com/fwlink/?linkid=14202")]
		[MonitoringDescription("LogEntryEventID")]
		public int EventID
		{
			get
			{
				return this.IntFrom(this.dataBuf, this.bufOffset + 20) & 1073741823;
			}
		}

		// Token: 0x17000D54 RID: 3412
		// (get) Token: 0x0600397B RID: 14715 RVA: 0x000F3F1B File Offset: 0x000F2F1B
		[MonitoringDescription("LogEntryEntryType")]
		public EventLogEntryType EntryType
		{
			get
			{
				return (EventLogEntryType)this.ShortFrom(this.dataBuf, this.bufOffset + 24);
			}
		}

		// Token: 0x17000D55 RID: 3413
		// (get) Token: 0x0600397C RID: 14716 RVA: 0x000F3F34 File Offset: 0x000F2F34
		[MonitoringDescription("LogEntryMessage")]
		[Editor("System.ComponentModel.Design.BinaryEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		public string Message
		{
			get
			{
				if (this.message == null)
				{
					string messageLibraryNames = this.GetMessageLibraryNames("EventMessageFile");
					int num = this.IntFrom(this.dataBuf, this.bufOffset + 20);
					string text = this.owner.FormatMessageWrapper(messageLibraryNames, (uint)num, this.ReplacementStrings);
					if (text == null)
					{
						StringBuilder stringBuilder = new StringBuilder(SR.GetString("MessageNotFormatted", new object[]
						{
							num,
							this.Source
						}));
						string[] replacementStrings = this.ReplacementStrings;
						for (int i = 0; i < replacementStrings.Length; i++)
						{
							if (i != 0)
							{
								stringBuilder.Append(", ");
							}
							stringBuilder.Append("'");
							stringBuilder.Append(replacementStrings[i]);
							stringBuilder.Append("'");
						}
						text = stringBuilder.ToString();
					}
					else
					{
						text = this.ReplaceMessageParameters(text, this.ReplacementStrings);
					}
					this.message = text;
				}
				return this.message;
			}
		}

		// Token: 0x17000D56 RID: 3414
		// (get) Token: 0x0600397D RID: 14717 RVA: 0x000F402C File Offset: 0x000F302C
		[MonitoringDescription("LogEntrySource")]
		public string Source
		{
			get
			{
				StringBuilder stringBuilder = new StringBuilder();
				int num = this.bufOffset + 56;
				for (char value = this.CharFrom(this.dataBuf, num); value != '\0'; value = this.CharFrom(this.dataBuf, num))
				{
					stringBuilder.Append(value);
					num += 2;
				}
				return stringBuilder.ToString();
			}
		}

		// Token: 0x17000D57 RID: 3415
		// (get) Token: 0x0600397E RID: 14718 RVA: 0x000F407C File Offset: 0x000F307C
		[MonitoringDescription("LogEntryReplacementStrings")]
		public string[] ReplacementStrings
		{
			get
			{
				string[] array = new string[(int)this.ShortFrom(this.dataBuf, this.bufOffset + 26)];
				int i = 0;
				int num = this.bufOffset + this.IntFrom(this.dataBuf, this.bufOffset + 36);
				StringBuilder stringBuilder = new StringBuilder();
				while (i < array.Length)
				{
					char c = this.CharFrom(this.dataBuf, num);
					if (c != '\0')
					{
						stringBuilder.Append(c);
					}
					else
					{
						array[i] = stringBuilder.ToString();
						i++;
						stringBuilder = new StringBuilder();
					}
					num += 2;
				}
				return array;
			}
		}

		// Token: 0x17000D58 RID: 3416
		// (get) Token: 0x0600397F RID: 14719 RVA: 0x000F4107 File Offset: 0x000F3107
		[MonitoringDescription("LogEntryResourceId")]
		[ComVisible(false)]
		public long InstanceId
		{
			get
			{
				return (long)((ulong)this.IntFrom(this.dataBuf, this.bufOffset + 20));
			}
		}

		// Token: 0x17000D59 RID: 3417
		// (get) Token: 0x06003980 RID: 14720 RVA: 0x000F4120 File Offset: 0x000F3120
		[MonitoringDescription("LogEntryTimeGenerated")]
		public DateTime TimeGenerated
		{
			get
			{
				return EventLogEntry.beginningOfTime.AddSeconds((double)this.IntFrom(this.dataBuf, this.bufOffset + 12)).ToLocalTime();
			}
		}

		// Token: 0x17000D5A RID: 3418
		// (get) Token: 0x06003981 RID: 14721 RVA: 0x000F4158 File Offset: 0x000F3158
		[MonitoringDescription("LogEntryTimeWritten")]
		public DateTime TimeWritten
		{
			get
			{
				return EventLogEntry.beginningOfTime.AddSeconds((double)this.IntFrom(this.dataBuf, this.bufOffset + 16)).ToLocalTime();
			}
		}

		// Token: 0x17000D5B RID: 3419
		// (get) Token: 0x06003982 RID: 14722 RVA: 0x000F4190 File Offset: 0x000F3190
		[MonitoringDescription("LogEntryUserName")]
		public string UserName
		{
			get
			{
				int num = this.IntFrom(this.dataBuf, this.bufOffset + 40);
				if (num == 0)
				{
					return null;
				}
				byte[] array = new byte[num];
				Array.Copy(this.dataBuf, this.bufOffset + this.IntFrom(this.dataBuf, this.bufOffset + 44), array, 0, array.Length);
				int[] sidNameUse = new int[1];
				char[] array2 = new char[1024];
				char[] array3 = new char[1024];
				int[] array4 = new int[]
				{
					1024
				};
				int[] array5 = new int[]
				{
					1024
				};
				if (!UnsafeNativeMethods.LookupAccountSid(this.MachineName, array, array2, array4, array3, array5, sidNameUse))
				{
					return "";
				}
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Append(array3, 0, array5[0]);
				stringBuilder.Append("\\");
				stringBuilder.Append(array2, 0, array4[0]);
				return stringBuilder.ToString();
			}
		}

		// Token: 0x06003983 RID: 14723 RVA: 0x000F428B File Offset: 0x000F328B
		private char CharFrom(byte[] buf, int offset)
		{
			return (char)this.ShortFrom(buf, offset);
		}

		// Token: 0x06003984 RID: 14724 RVA: 0x000F4298 File Offset: 0x000F3298
		public bool Equals(EventLogEntry otherEntry)
		{
			if (otherEntry == null)
			{
				return false;
			}
			int num = this.IntFrom(this.dataBuf, this.bufOffset);
			int num2 = this.IntFrom(otherEntry.dataBuf, otherEntry.bufOffset);
			if (num != num2)
			{
				return false;
			}
			int num3 = this.bufOffset;
			int num4 = this.bufOffset + num;
			int num5 = otherEntry.bufOffset;
			int i = num3;
			while (i < num4)
			{
				if (this.dataBuf[i] != otherEntry.dataBuf[num5])
				{
					return false;
				}
				i++;
				num5++;
			}
			return true;
		}

		// Token: 0x06003985 RID: 14725 RVA: 0x000F431B File Offset: 0x000F331B
		private int IntFrom(byte[] buf, int offset)
		{
			return (-16777216 & (int)buf[offset + 3] << 24) | (16711680 & (int)buf[offset + 2] << 16) | (65280 & (int)buf[offset + 1] << 8) | (int)(byte.MaxValue & buf[offset]);
		}

		// Token: 0x06003986 RID: 14726 RVA: 0x000F4354 File Offset: 0x000F3354
		internal string ReplaceMessageParameters(string msg, string[] insertionStrings)
		{
			int i = msg.IndexOf('%');
			if (i < 0)
			{
				return msg;
			}
			int num = 0;
			int length = msg.Length;
			StringBuilder stringBuilder = new StringBuilder();
			string messageLibraryNames = this.GetMessageLibraryNames("ParameterMessageFile");
			while (i >= 0)
			{
				string text = null;
				int num2 = i + 1;
				while (num2 < length && char.IsDigit(msg, num2))
				{
					num2++;
				}
				uint num3 = 0U;
				if (num2 != i + 1)
				{
					uint.TryParse(msg.Substring(i + 1, num2 - i - 1), out num3);
				}
				if (num3 != 0U)
				{
					text = this.owner.FormatMessageWrapper(messageLibraryNames, num3, insertionStrings);
				}
				if (text != null)
				{
					if (i > num)
					{
						stringBuilder.Append(msg, num, i - num);
					}
					stringBuilder.Append(text);
					num = num2;
				}
				i = msg.IndexOf('%', i + 1);
			}
			if (length - num > 0)
			{
				stringBuilder.Append(msg, num, length - num);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06003987 RID: 14727 RVA: 0x000F4434 File Offset: 0x000F3434
		private static RegistryKey GetSourceRegKey(string logName, string source, string machineName)
		{
			RegistryKey registryKey = null;
			RegistryKey registryKey2 = null;
			RegistryKey result;
			try
			{
				registryKey = EventLog.GetEventLogRegKey(machineName, false);
				if (registryKey == null)
				{
					result = null;
				}
				else
				{
					if (logName == null)
					{
						registryKey2 = registryKey.OpenSubKey("Application", false);
					}
					else
					{
						registryKey2 = registryKey.OpenSubKey(logName, false);
					}
					if (registryKey2 == null)
					{
						result = null;
					}
					else
					{
						result = registryKey2.OpenSubKey(source, false);
					}
				}
			}
			finally
			{
				if (registryKey != null)
				{
					registryKey.Close();
				}
				if (registryKey2 != null)
				{
					registryKey2.Close();
				}
			}
			return result;
		}

		// Token: 0x06003988 RID: 14728 RVA: 0x000F44A8 File Offset: 0x000F34A8
		private string GetMessageLibraryNames(string libRegKey)
		{
			string text = null;
			RegistryKey registryKey = null;
			try
			{
				registryKey = EventLogEntry.GetSourceRegKey(this.owner.Log, this.Source, this.owner.MachineName);
				if (registryKey != null)
				{
					if (this.owner.MachineName == ".")
					{
						text = (string)registryKey.GetValue(libRegKey);
					}
					else
					{
						text = (string)registryKey.GetValue(libRegKey, null, RegistryValueOptions.DoNotExpandEnvironmentNames);
					}
				}
			}
			finally
			{
				if (registryKey != null)
				{
					registryKey.Close();
				}
			}
			if (text == null)
			{
				return null;
			}
			if (!(this.owner.MachineName != "."))
			{
				return text;
			}
			if (text.EndsWith("EventLogMessages.dll", StringComparison.Ordinal))
			{
				return EventLog.GetDllPath(".");
			}
			if (string.Compare(text, 0, "%systemroot%", 0, 12, StringComparison.OrdinalIgnoreCase) == 0)
			{
				StringBuilder stringBuilder = new StringBuilder(text.Length + this.owner.MachineName.Length - 3);
				stringBuilder.Append("\\\\");
				stringBuilder.Append(this.owner.MachineName);
				stringBuilder.Append("\\admin$");
				stringBuilder.Append(text, 12, text.Length - 12);
				return stringBuilder.ToString();
			}
			if (text[1] == ':')
			{
				StringBuilder stringBuilder2 = new StringBuilder(text.Length + this.owner.MachineName.Length + 3);
				stringBuilder2.Append("\\\\");
				stringBuilder2.Append(this.owner.MachineName);
				stringBuilder2.Append("\\");
				stringBuilder2.Append(text[0]);
				stringBuilder2.Append("$");
				stringBuilder2.Append(text, 2, text.Length - 2);
				return stringBuilder2.ToString();
			}
			return null;
		}

		// Token: 0x06003989 RID: 14729 RVA: 0x000F4668 File Offset: 0x000F3668
		private short ShortFrom(byte[] buf, int offset)
		{
			return (short)((65280 & (int)buf[offset + 1] << 8) | (int)(byte.MaxValue & buf[offset]));
		}

		// Token: 0x0600398A RID: 14730 RVA: 0x000F4684 File Offset: 0x000F3684
		void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			int num = this.IntFrom(this.dataBuf, this.bufOffset);
			byte[] array = new byte[num];
			Array.Copy(this.dataBuf, this.bufOffset, array, 0, num);
			info.AddValue("DataBuffer", array, typeof(byte[]));
			info.AddValue("LogName", this.owner.Log);
			info.AddValue("MachineName", this.owner.MachineName);
		}

		// Token: 0x040032AD RID: 12973
		private const int OFFSETFIXUP = 56;

		// Token: 0x040032AE RID: 12974
		internal byte[] dataBuf;

		// Token: 0x040032AF RID: 12975
		internal int bufOffset;

		// Token: 0x040032B0 RID: 12976
		private EventLog owner;

		// Token: 0x040032B1 RID: 12977
		private string category;

		// Token: 0x040032B2 RID: 12978
		private string message;

		// Token: 0x040032B3 RID: 12979
		private static readonly DateTime beginningOfTime = new DateTime(1970, 1, 1, 0, 0, 0);

		// Token: 0x02000752 RID: 1874
		private static class FieldOffsets
		{
			// Token: 0x040032B4 RID: 12980
			internal const int LENGTH = 0;

			// Token: 0x040032B5 RID: 12981
			internal const int RESERVED = 4;

			// Token: 0x040032B6 RID: 12982
			internal const int RECORDNUMBER = 8;

			// Token: 0x040032B7 RID: 12983
			internal const int TIMEGENERATED = 12;

			// Token: 0x040032B8 RID: 12984
			internal const int TIMEWRITTEN = 16;

			// Token: 0x040032B9 RID: 12985
			internal const int EVENTID = 20;

			// Token: 0x040032BA RID: 12986
			internal const int EVENTTYPE = 24;

			// Token: 0x040032BB RID: 12987
			internal const int NUMSTRINGS = 26;

			// Token: 0x040032BC RID: 12988
			internal const int EVENTCATEGORY = 28;

			// Token: 0x040032BD RID: 12989
			internal const int RESERVEDFLAGS = 30;

			// Token: 0x040032BE RID: 12990
			internal const int CLOSINGRECORDNUMBER = 32;

			// Token: 0x040032BF RID: 12991
			internal const int STRINGOFFSET = 36;

			// Token: 0x040032C0 RID: 12992
			internal const int USERSIDLENGTH = 40;

			// Token: 0x040032C1 RID: 12993
			internal const int USERSIDOFFSET = 44;

			// Token: 0x040032C2 RID: 12994
			internal const int DATALENGTH = 48;

			// Token: 0x040032C3 RID: 12995
			internal const int DATAOFFSET = 52;

			// Token: 0x040032C4 RID: 12996
			internal const int RAWDATA = 56;
		}
	}
}
