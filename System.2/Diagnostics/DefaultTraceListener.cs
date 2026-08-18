using System;
using System.IO;
using System.Security.Permissions;
using Microsoft.Win32;

namespace System.Diagnostics
{
	// Token: 0x02000497 RID: 1175
	[HostProtection(SecurityAction.LinkDemand, Synchronization = true)]
	public class DefaultTraceListener : TraceListener
	{
		// Token: 0x06002B97 RID: 11159 RVA: 0x000C5545 File Offset: 0x000C3745
		public DefaultTraceListener() : base("Default")
		{
		}

		// Token: 0x17000A86 RID: 2694
		// (get) Token: 0x06002B98 RID: 11160 RVA: 0x000C5552 File Offset: 0x000C3752
		// (set) Token: 0x06002B99 RID: 11161 RVA: 0x000C5568 File Offset: 0x000C3768
		public bool AssertUiEnabled
		{
			get
			{
				if (!this.settingsInitialized)
				{
					this.InitializeSettings();
				}
				return this.assertUIEnabled;
			}
			set
			{
				if (!this.settingsInitialized)
				{
					this.InitializeSettings();
				}
				this.assertUIEnabled = value;
			}
		}

		// Token: 0x17000A87 RID: 2695
		// (get) Token: 0x06002B9A RID: 11162 RVA: 0x000C557F File Offset: 0x000C377F
		// (set) Token: 0x06002B9B RID: 11163 RVA: 0x000C5595 File Offset: 0x000C3795
		public string LogFileName
		{
			get
			{
				if (!this.settingsInitialized)
				{
					this.InitializeSettings();
				}
				return this.logFileName;
			}
			set
			{
				if (!this.settingsInitialized)
				{
					this.InitializeSettings();
				}
				this.logFileName = value;
			}
		}

		// Token: 0x06002B9C RID: 11164 RVA: 0x000C55AC File Offset: 0x000C37AC
		public override void Fail(string message)
		{
			this.Fail(message, null);
		}

		// Token: 0x06002B9D RID: 11165 RVA: 0x000C55B8 File Offset: 0x000C37B8
		public override void Fail(string message, string detailMessage)
		{
			StackTrace stackTrace = new StackTrace(true);
			int index = 0;
			bool uiPermission = DefaultTraceListener.UiPermission;
			string stackTrace2;
			try
			{
				stackTrace2 = stackTrace.ToString();
			}
			catch
			{
				stackTrace2 = "";
			}
			this.WriteAssert(stackTrace2, message, detailMessage);
			if (this.AssertUiEnabled && uiPermission)
			{
				AssertWrapper.ShowAssert(stackTrace2, stackTrace.GetFrame(index), message, detailMessage);
			}
		}

		// Token: 0x06002B9E RID: 11166 RVA: 0x000C5618 File Offset: 0x000C3818
		private void InitializeSettings()
		{
			this.assertUIEnabled = DiagnosticsConfiguration.AssertUIEnabled;
			this.logFileName = DiagnosticsConfiguration.LogFileName;
			this.settingsInitialized = true;
		}

		// Token: 0x06002B9F RID: 11167 RVA: 0x000C5638 File Offset: 0x000C3838
		private void WriteAssert(string stackTrace, string message, string detailMessage)
		{
			string message2 = string.Concat(new string[]
			{
				SR.GetString("DebugAssertBanner"),
				Environment.NewLine,
				SR.GetString("DebugAssertShortMessage"),
				Environment.NewLine,
				message,
				Environment.NewLine,
				SR.GetString("DebugAssertLongMessage"),
				Environment.NewLine,
				detailMessage,
				Environment.NewLine,
				stackTrace
			});
			this.WriteLine(message2);
		}

		// Token: 0x06002BA0 RID: 11168 RVA: 0x000C56B8 File Offset: 0x000C38B8
		private void WriteToLogFile(string message, bool useWriteLine)
		{
			try
			{
				FileInfo fileInfo = new FileInfo(this.LogFileName);
				using (Stream stream = fileInfo.Open(FileMode.OpenOrCreate))
				{
					using (StreamWriter streamWriter = new StreamWriter(stream))
					{
						stream.Position = stream.Length;
						if (useWriteLine)
						{
							streamWriter.WriteLine(message);
						}
						else
						{
							streamWriter.Write(message);
						}
					}
				}
			}
			catch (Exception ex)
			{
				this.WriteLine(SR.GetString("ExceptionOccurred", new object[]
				{
					this.LogFileName,
					ex.ToString()
				}), false);
			}
		}

		// Token: 0x06002BA1 RID: 11169 RVA: 0x000C5770 File Offset: 0x000C3970
		public override void Write(string message)
		{
			this.Write(message, true);
		}

		// Token: 0x06002BA2 RID: 11170 RVA: 0x000C577C File Offset: 0x000C397C
		private void Write(string message, bool useLogFile)
		{
			if (base.NeedIndent)
			{
				this.WriteIndent();
			}
			if (message == null || message.Length <= 16384)
			{
				this.internalWrite(message);
			}
			else
			{
				int i;
				for (i = 0; i < message.Length - 16384; i += 16384)
				{
					this.internalWrite(message.Substring(i, 16384));
				}
				this.internalWrite(message.Substring(i));
			}
			if (useLogFile && this.LogFileName.Length != 0)
			{
				this.WriteToLogFile(message, false);
			}
		}

		// Token: 0x06002BA3 RID: 11171 RVA: 0x000C5802 File Offset: 0x000C3A02
		private void internalWrite(string message)
		{
			if (Debugger.IsLogging())
			{
				Debugger.Log(0, null, message);
				return;
			}
			if (message == null)
			{
				SafeNativeMethods.OutputDebugString(string.Empty);
				return;
			}
			SafeNativeMethods.OutputDebugString(message);
		}

		// Token: 0x06002BA4 RID: 11172 RVA: 0x000C5828 File Offset: 0x000C3A28
		public override void WriteLine(string message)
		{
			this.WriteLine(message, true);
		}

		// Token: 0x06002BA5 RID: 11173 RVA: 0x000C5832 File Offset: 0x000C3A32
		private void WriteLine(string message, bool useLogFile)
		{
			if (base.NeedIndent)
			{
				this.WriteIndent();
			}
			this.Write(message + Environment.NewLine, useLogFile);
			base.NeedIndent = true;
		}

		// Token: 0x17000A88 RID: 2696
		// (get) Token: 0x06002BA6 RID: 11174 RVA: 0x000C585C File Offset: 0x000C3A5C
		private static bool UiPermission
		{
			get
			{
				bool result = false;
				try
				{
					new UIPermission(UIPermissionWindow.SafeSubWindows).Demand();
					result = true;
				}
				catch
				{
				}
				return result;
			}
		}

		// Token: 0x0400268C RID: 9868
		private bool assertUIEnabled;

		// Token: 0x0400268D RID: 9869
		private string logFileName;

		// Token: 0x0400268E RID: 9870
		private bool settingsInitialized;

		// Token: 0x0400268F RID: 9871
		private const int internalWriteSize = 16384;
	}
}
