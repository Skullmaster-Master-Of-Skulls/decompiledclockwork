using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.IO;
using System.Security;
using System.Security.Permissions;
using System.Text;
using Microsoft.Win32;

namespace System.Diagnostics
{
	// Token: 0x020004FF RID: 1279
	[TypeConverter(typeof(ExpandableObjectConverter))]
	[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
	[HostProtection(SecurityAction.LinkDemand, SharedState = true, SelfAffectingProcessMgmt = true)]
	public sealed class ProcessStartInfo
	{
		// Token: 0x06003070 RID: 12400 RVA: 0x000DBB47 File Offset: 0x000D9D47
		public ProcessStartInfo()
		{
		}

		// Token: 0x06003071 RID: 12401 RVA: 0x000DBB56 File Offset: 0x000D9D56
		internal ProcessStartInfo(Process parent)
		{
			this.weakParentProcess = new WeakReference(parent);
		}

		// Token: 0x06003072 RID: 12402 RVA: 0x000DBB71 File Offset: 0x000D9D71
		public ProcessStartInfo(string fileName)
		{
			this.fileName = fileName;
		}

		// Token: 0x06003073 RID: 12403 RVA: 0x000DBB87 File Offset: 0x000D9D87
		public ProcessStartInfo(string fileName, string arguments)
		{
			this.fileName = fileName;
			this.arguments = arguments;
		}

		// Token: 0x17000BD5 RID: 3029
		// (get) Token: 0x06003074 RID: 12404 RVA: 0x000DBBA4 File Offset: 0x000D9DA4
		// (set) Token: 0x06003075 RID: 12405 RVA: 0x000DBBBA File Offset: 0x000D9DBA
		[DefaultValue("")]
		[TypeConverter("System.Diagnostics.Design.VerbConverter, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[MonitoringDescription("ProcessVerb")]
		[NotifyParentProperty(true)]
		public string Verb
		{
			get
			{
				if (this.verb == null)
				{
					return string.Empty;
				}
				return this.verb;
			}
			set
			{
				this.verb = value;
			}
		}

		// Token: 0x17000BD6 RID: 3030
		// (get) Token: 0x06003076 RID: 12406 RVA: 0x000DBBC3 File Offset: 0x000D9DC3
		// (set) Token: 0x06003077 RID: 12407 RVA: 0x000DBBD9 File Offset: 0x000D9DD9
		[DefaultValue("")]
		[MonitoringDescription("ProcessArguments")]
		[SettingsBindable(true)]
		[TypeConverter("System.Diagnostics.Design.StringValueConverter, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[NotifyParentProperty(true)]
		public string Arguments
		{
			get
			{
				if (this.arguments == null)
				{
					return string.Empty;
				}
				return this.arguments;
			}
			set
			{
				this.arguments = value;
			}
		}

		// Token: 0x17000BD7 RID: 3031
		// (get) Token: 0x06003078 RID: 12408 RVA: 0x000DBBE2 File Offset: 0x000D9DE2
		// (set) Token: 0x06003079 RID: 12409 RVA: 0x000DBBEA File Offset: 0x000D9DEA
		[DefaultValue(false)]
		[MonitoringDescription("ProcessCreateNoWindow")]
		[NotifyParentProperty(true)]
		public bool CreateNoWindow
		{
			get
			{
				return this.createNoWindow;
			}
			set
			{
				this.createNoWindow = value;
			}
		}

		// Token: 0x17000BD8 RID: 3032
		// (get) Token: 0x0600307A RID: 12410 RVA: 0x000DBBF4 File Offset: 0x000D9DF4
		[Editor("System.Diagnostics.Design.StringDictionaryEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[DefaultValue(null)]
		[MonitoringDescription("ProcessEnvironmentVariables")]
		[NotifyParentProperty(true)]
		public StringDictionary EnvironmentVariables
		{
			get
			{
				if (this.environmentVariables == null)
				{
					this.environmentVariables = new StringDictionaryWithComparer();
					if (this.weakParentProcess == null || !this.weakParentProcess.IsAlive || ((Component)this.weakParentProcess.Target).Site == null || !((Component)this.weakParentProcess.Target).Site.DesignMode)
					{
						foreach (object obj in System.Environment.GetEnvironmentVariables())
						{
							DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
							this.environmentVariables.Add((string)dictionaryEntry.Key, (string)dictionaryEntry.Value);
						}
					}
				}
				return this.environmentVariables;
			}
		}

		// Token: 0x17000BD9 RID: 3033
		// (get) Token: 0x0600307B RID: 12411 RVA: 0x000DBCCC File Offset: 0x000D9ECC
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[DefaultValue(null)]
		[NotifyParentProperty(true)]
		public IDictionary<string, string> Environment
		{
			get
			{
				if (this.environment == null)
				{
					this.environment = this.EnvironmentVariables.AsGenericDictionary();
				}
				return this.environment;
			}
		}

		// Token: 0x17000BDA RID: 3034
		// (get) Token: 0x0600307C RID: 12412 RVA: 0x000DBCED File Offset: 0x000D9EED
		// (set) Token: 0x0600307D RID: 12413 RVA: 0x000DBCF5 File Offset: 0x000D9EF5
		[DefaultValue(false)]
		[MonitoringDescription("ProcessRedirectStandardInput")]
		[NotifyParentProperty(true)]
		public bool RedirectStandardInput
		{
			get
			{
				return this.redirectStandardInput;
			}
			set
			{
				this.redirectStandardInput = value;
			}
		}

		// Token: 0x17000BDB RID: 3035
		// (get) Token: 0x0600307E RID: 12414 RVA: 0x000DBCFE File Offset: 0x000D9EFE
		// (set) Token: 0x0600307F RID: 12415 RVA: 0x000DBD06 File Offset: 0x000D9F06
		[DefaultValue(false)]
		[MonitoringDescription("ProcessRedirectStandardOutput")]
		[NotifyParentProperty(true)]
		public bool RedirectStandardOutput
		{
			get
			{
				return this.redirectStandardOutput;
			}
			set
			{
				this.redirectStandardOutput = value;
			}
		}

		// Token: 0x17000BDC RID: 3036
		// (get) Token: 0x06003080 RID: 12416 RVA: 0x000DBD0F File Offset: 0x000D9F0F
		// (set) Token: 0x06003081 RID: 12417 RVA: 0x000DBD17 File Offset: 0x000D9F17
		[DefaultValue(false)]
		[MonitoringDescription("ProcessRedirectStandardError")]
		[NotifyParentProperty(true)]
		public bool RedirectStandardError
		{
			get
			{
				return this.redirectStandardError;
			}
			set
			{
				this.redirectStandardError = value;
			}
		}

		// Token: 0x17000BDD RID: 3037
		// (get) Token: 0x06003082 RID: 12418 RVA: 0x000DBD20 File Offset: 0x000D9F20
		// (set) Token: 0x06003083 RID: 12419 RVA: 0x000DBD28 File Offset: 0x000D9F28
		public Encoding StandardErrorEncoding
		{
			get
			{
				return this.standardErrorEncoding;
			}
			set
			{
				this.standardErrorEncoding = value;
			}
		}

		// Token: 0x17000BDE RID: 3038
		// (get) Token: 0x06003084 RID: 12420 RVA: 0x000DBD31 File Offset: 0x000D9F31
		// (set) Token: 0x06003085 RID: 12421 RVA: 0x000DBD39 File Offset: 0x000D9F39
		public Encoding StandardOutputEncoding
		{
			get
			{
				return this.standardOutputEncoding;
			}
			set
			{
				this.standardOutputEncoding = value;
			}
		}

		// Token: 0x17000BDF RID: 3039
		// (get) Token: 0x06003086 RID: 12422 RVA: 0x000DBD42 File Offset: 0x000D9F42
		// (set) Token: 0x06003087 RID: 12423 RVA: 0x000DBD4A File Offset: 0x000D9F4A
		[DefaultValue(true)]
		[MonitoringDescription("ProcessUseShellExecute")]
		[NotifyParentProperty(true)]
		public bool UseShellExecute
		{
			get
			{
				return this.useShellExecute;
			}
			set
			{
				this.useShellExecute = value;
			}
		}

		// Token: 0x17000BE0 RID: 3040
		// (get) Token: 0x06003088 RID: 12424 RVA: 0x000DBD54 File Offset: 0x000D9F54
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public string[] Verbs
		{
			get
			{
				ArrayList arrayList = new ArrayList();
				RegistryKey registryKey = null;
				string extension = Path.GetExtension(this.FileName);
				try
				{
					if (extension != null && extension.Length > 0)
					{
						registryKey = Registry.ClassesRoot.OpenSubKey(extension);
						if (registryKey != null)
						{
							string str = (string)registryKey.GetValue(string.Empty);
							registryKey.Close();
							registryKey = Registry.ClassesRoot.OpenSubKey(str + "\\shell");
							if (registryKey != null)
							{
								string[] subKeyNames = registryKey.GetSubKeyNames();
								for (int i = 0; i < subKeyNames.Length; i++)
								{
									if (string.Compare(subKeyNames[i], "new", StringComparison.OrdinalIgnoreCase) != 0)
									{
										arrayList.Add(subKeyNames[i]);
									}
								}
								registryKey.Close();
								registryKey = null;
							}
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
				string[] array = new string[arrayList.Count];
				arrayList.CopyTo(array, 0);
				return array;
			}
		}

		// Token: 0x17000BE1 RID: 3041
		// (get) Token: 0x06003089 RID: 12425 RVA: 0x000DBE3C File Offset: 0x000DA03C
		// (set) Token: 0x0600308A RID: 12426 RVA: 0x000DBE52 File Offset: 0x000DA052
		[NotifyParentProperty(true)]
		public string UserName
		{
			get
			{
				if (this.userName == null)
				{
					return string.Empty;
				}
				return this.userName;
			}
			set
			{
				this.userName = value;
			}
		}

		// Token: 0x17000BE2 RID: 3042
		// (get) Token: 0x0600308B RID: 12427 RVA: 0x000DBE5B File Offset: 0x000DA05B
		// (set) Token: 0x0600308C RID: 12428 RVA: 0x000DBE63 File Offset: 0x000DA063
		public SecureString Password
		{
			get
			{
				return this.password;
			}
			set
			{
				this.password = value;
			}
		}

		// Token: 0x17000BE3 RID: 3043
		// (get) Token: 0x0600308D RID: 12429 RVA: 0x000DBE6C File Offset: 0x000DA06C
		// (set) Token: 0x0600308E RID: 12430 RVA: 0x000DBE74 File Offset: 0x000DA074
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public string PasswordInClearText
		{
			get
			{
				return this.passwordInClearText;
			}
			set
			{
				this.passwordInClearText = value;
			}
		}

		// Token: 0x17000BE4 RID: 3044
		// (get) Token: 0x0600308F RID: 12431 RVA: 0x000DBE7D File Offset: 0x000DA07D
		// (set) Token: 0x06003090 RID: 12432 RVA: 0x000DBE93 File Offset: 0x000DA093
		[NotifyParentProperty(true)]
		public string Domain
		{
			get
			{
				if (this.domain == null)
				{
					return string.Empty;
				}
				return this.domain;
			}
			set
			{
				this.domain = value;
			}
		}

		// Token: 0x17000BE5 RID: 3045
		// (get) Token: 0x06003091 RID: 12433 RVA: 0x000DBE9C File Offset: 0x000DA09C
		// (set) Token: 0x06003092 RID: 12434 RVA: 0x000DBEA4 File Offset: 0x000DA0A4
		[NotifyParentProperty(true)]
		public bool LoadUserProfile
		{
			get
			{
				return this.loadUserProfile;
			}
			set
			{
				this.loadUserProfile = value;
			}
		}

		// Token: 0x17000BE6 RID: 3046
		// (get) Token: 0x06003093 RID: 12435 RVA: 0x000DBEAD File Offset: 0x000DA0AD
		// (set) Token: 0x06003094 RID: 12436 RVA: 0x000DBEC3 File Offset: 0x000DA0C3
		[DefaultValue("")]
		[Editor("System.Diagnostics.Design.StartFileNameEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[MonitoringDescription("ProcessFileName")]
		[SettingsBindable(true)]
		[TypeConverter("System.Diagnostics.Design.StringValueConverter, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[NotifyParentProperty(true)]
		public string FileName
		{
			get
			{
				if (this.fileName == null)
				{
					return string.Empty;
				}
				return this.fileName;
			}
			set
			{
				this.fileName = value;
			}
		}

		// Token: 0x17000BE7 RID: 3047
		// (get) Token: 0x06003095 RID: 12437 RVA: 0x000DBECC File Offset: 0x000DA0CC
		// (set) Token: 0x06003096 RID: 12438 RVA: 0x000DBEE2 File Offset: 0x000DA0E2
		[DefaultValue("")]
		[MonitoringDescription("ProcessWorkingDirectory")]
		[Editor("System.Diagnostics.Design.WorkingDirectoryEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[SettingsBindable(true)]
		[TypeConverter("System.Diagnostics.Design.StringValueConverter, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[NotifyParentProperty(true)]
		public string WorkingDirectory
		{
			get
			{
				if (this.directory == null)
				{
					return string.Empty;
				}
				return this.directory;
			}
			set
			{
				this.directory = value;
			}
		}

		// Token: 0x17000BE8 RID: 3048
		// (get) Token: 0x06003097 RID: 12439 RVA: 0x000DBEEB File Offset: 0x000DA0EB
		// (set) Token: 0x06003098 RID: 12440 RVA: 0x000DBEF3 File Offset: 0x000DA0F3
		[DefaultValue(false)]
		[MonitoringDescription("ProcessErrorDialog")]
		[NotifyParentProperty(true)]
		public bool ErrorDialog
		{
			get
			{
				return this.errorDialog;
			}
			set
			{
				this.errorDialog = value;
			}
		}

		// Token: 0x17000BE9 RID: 3049
		// (get) Token: 0x06003099 RID: 12441 RVA: 0x000DBEFC File Offset: 0x000DA0FC
		// (set) Token: 0x0600309A RID: 12442 RVA: 0x000DBF04 File Offset: 0x000DA104
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public IntPtr ErrorDialogParentHandle
		{
			get
			{
				return this.errorDialogParentHandle;
			}
			set
			{
				this.errorDialogParentHandle = value;
			}
		}

		// Token: 0x17000BEA RID: 3050
		// (get) Token: 0x0600309B RID: 12443 RVA: 0x000DBF0D File Offset: 0x000DA10D
		// (set) Token: 0x0600309C RID: 12444 RVA: 0x000DBF15 File Offset: 0x000DA115
		[DefaultValue(ProcessWindowStyle.Normal)]
		[MonitoringDescription("ProcessWindowStyle")]
		[NotifyParentProperty(true)]
		public ProcessWindowStyle WindowStyle
		{
			get
			{
				return this.windowStyle;
			}
			set
			{
				if (!Enum.IsDefined(typeof(ProcessWindowStyle), value))
				{
					throw new InvalidEnumArgumentException("value", (int)value, typeof(ProcessWindowStyle));
				}
				this.windowStyle = value;
			}
		}

		// Token: 0x0400289F RID: 10399
		private string fileName;

		// Token: 0x040028A0 RID: 10400
		private string arguments;

		// Token: 0x040028A1 RID: 10401
		private string directory;

		// Token: 0x040028A2 RID: 10402
		private string verb;

		// Token: 0x040028A3 RID: 10403
		private ProcessWindowStyle windowStyle;

		// Token: 0x040028A4 RID: 10404
		private bool errorDialog;

		// Token: 0x040028A5 RID: 10405
		private IntPtr errorDialogParentHandle;

		// Token: 0x040028A6 RID: 10406
		private bool useShellExecute = true;

		// Token: 0x040028A7 RID: 10407
		private string userName;

		// Token: 0x040028A8 RID: 10408
		private string domain;

		// Token: 0x040028A9 RID: 10409
		private SecureString password;

		// Token: 0x040028AA RID: 10410
		private string passwordInClearText;

		// Token: 0x040028AB RID: 10411
		private bool loadUserProfile;

		// Token: 0x040028AC RID: 10412
		private bool redirectStandardInput;

		// Token: 0x040028AD RID: 10413
		private bool redirectStandardOutput;

		// Token: 0x040028AE RID: 10414
		private bool redirectStandardError;

		// Token: 0x040028AF RID: 10415
		private Encoding standardOutputEncoding;

		// Token: 0x040028B0 RID: 10416
		private Encoding standardErrorEncoding;

		// Token: 0x040028B1 RID: 10417
		private bool createNoWindow;

		// Token: 0x040028B2 RID: 10418
		private WeakReference weakParentProcess;

		// Token: 0x040028B3 RID: 10419
		internal StringDictionary environmentVariables;

		// Token: 0x040028B4 RID: 10420
		private IDictionary<string, string> environment;
	}
}
