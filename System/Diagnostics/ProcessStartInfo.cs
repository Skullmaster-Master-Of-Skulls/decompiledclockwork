using System;
using System.Collections;
using System.Collections.Specialized;
using System.ComponentModel;
using System.IO;
using System.Security;
using System.Security.Permissions;
using System.Text;
using Microsoft.Win32;

namespace System.Diagnostics
{
	// Token: 0x0200078A RID: 1930
	[TypeConverter(typeof(ExpandableObjectConverter))]
	[HostProtection(SecurityAction.LinkDemand, SharedState = true, SelfAffectingProcessMgmt = true)]
	[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
	public sealed class ProcessStartInfo
	{
		// Token: 0x06003B8B RID: 15243 RVA: 0x000FDF58 File Offset: 0x000FCF58
		public ProcessStartInfo()
		{
		}

		// Token: 0x06003B8C RID: 15244 RVA: 0x000FDF67 File Offset: 0x000FCF67
		internal ProcessStartInfo(Process parent)
		{
			this.weakParentProcess = new WeakReference(parent);
		}

		// Token: 0x06003B8D RID: 15245 RVA: 0x000FDF82 File Offset: 0x000FCF82
		public ProcessStartInfo(string fileName)
		{
			this.fileName = fileName;
		}

		// Token: 0x06003B8E RID: 15246 RVA: 0x000FDF98 File Offset: 0x000FCF98
		public ProcessStartInfo(string fileName, string arguments)
		{
			this.fileName = fileName;
			this.arguments = arguments;
		}

		// Token: 0x17000DF5 RID: 3573
		// (get) Token: 0x06003B8F RID: 15247 RVA: 0x000FDFB5 File Offset: 0x000FCFB5
		// (set) Token: 0x06003B90 RID: 15248 RVA: 0x000FDFCB File Offset: 0x000FCFCB
		[TypeConverter("System.Diagnostics.Design.VerbConverter, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[DefaultValue("")]
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

		// Token: 0x17000DF6 RID: 3574
		// (get) Token: 0x06003B91 RID: 15249 RVA: 0x000FDFD4 File Offset: 0x000FCFD4
		// (set) Token: 0x06003B92 RID: 15250 RVA: 0x000FDFEA File Offset: 0x000FCFEA
		[MonitoringDescription("ProcessArguments")]
		[DefaultValue("")]
		[TypeConverter("System.Diagnostics.Design.StringValueConverter, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[RecommendedAsConfigurable(true)]
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

		// Token: 0x17000DF7 RID: 3575
		// (get) Token: 0x06003B93 RID: 15251 RVA: 0x000FDFF3 File Offset: 0x000FCFF3
		// (set) Token: 0x06003B94 RID: 15252 RVA: 0x000FDFFB File Offset: 0x000FCFFB
		[NotifyParentProperty(true)]
		[DefaultValue(false)]
		[MonitoringDescription("ProcessCreateNoWindow")]
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

		// Token: 0x17000DF8 RID: 3576
		// (get) Token: 0x06003B95 RID: 15253 RVA: 0x000FE004 File Offset: 0x000FD004
		[Editor("System.Diagnostics.Design.StringDictionaryEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[MonitoringDescription("ProcessEnvironmentVariables")]
		[NotifyParentProperty(true)]
		[DefaultValue(null)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public StringDictionary EnvironmentVariables
		{
			get
			{
				if (this.environmentVariables == null)
				{
					this.environmentVariables = new StringDictionary();
					if (this.weakParentProcess == null || !this.weakParentProcess.IsAlive || ((Component)this.weakParentProcess.Target).Site == null || !((Component)this.weakParentProcess.Target).Site.DesignMode)
					{
						foreach (object obj in Environment.GetEnvironmentVariables())
						{
							DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
							this.environmentVariables.Add((string)dictionaryEntry.Key, (string)dictionaryEntry.Value);
						}
					}
				}
				return this.environmentVariables;
			}
		}

		// Token: 0x17000DF9 RID: 3577
		// (get) Token: 0x06003B96 RID: 15254 RVA: 0x000FE0DC File Offset: 0x000FD0DC
		// (set) Token: 0x06003B97 RID: 15255 RVA: 0x000FE0E4 File Offset: 0x000FD0E4
		[NotifyParentProperty(true)]
		[DefaultValue(false)]
		[MonitoringDescription("ProcessRedirectStandardInput")]
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

		// Token: 0x17000DFA RID: 3578
		// (get) Token: 0x06003B98 RID: 15256 RVA: 0x000FE0ED File Offset: 0x000FD0ED
		// (set) Token: 0x06003B99 RID: 15257 RVA: 0x000FE0F5 File Offset: 0x000FD0F5
		[MonitoringDescription("ProcessRedirectStandardOutput")]
		[NotifyParentProperty(true)]
		[DefaultValue(false)]
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

		// Token: 0x17000DFB RID: 3579
		// (get) Token: 0x06003B9A RID: 15258 RVA: 0x000FE0FE File Offset: 0x000FD0FE
		// (set) Token: 0x06003B9B RID: 15259 RVA: 0x000FE106 File Offset: 0x000FD106
		[NotifyParentProperty(true)]
		[MonitoringDescription("ProcessRedirectStandardError")]
		[DefaultValue(false)]
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

		// Token: 0x17000DFC RID: 3580
		// (get) Token: 0x06003B9C RID: 15260 RVA: 0x000FE10F File Offset: 0x000FD10F
		// (set) Token: 0x06003B9D RID: 15261 RVA: 0x000FE117 File Offset: 0x000FD117
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

		// Token: 0x17000DFD RID: 3581
		// (get) Token: 0x06003B9E RID: 15262 RVA: 0x000FE120 File Offset: 0x000FD120
		// (set) Token: 0x06003B9F RID: 15263 RVA: 0x000FE128 File Offset: 0x000FD128
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

		// Token: 0x17000DFE RID: 3582
		// (get) Token: 0x06003BA0 RID: 15264 RVA: 0x000FE131 File Offset: 0x000FD131
		// (set) Token: 0x06003BA1 RID: 15265 RVA: 0x000FE139 File Offset: 0x000FD139
		[MonitoringDescription("ProcessUseShellExecute")]
		[NotifyParentProperty(true)]
		[DefaultValue(true)]
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

		// Token: 0x17000DFF RID: 3583
		// (get) Token: 0x06003BA2 RID: 15266 RVA: 0x000FE144 File Offset: 0x000FD144
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

		// Token: 0x17000E00 RID: 3584
		// (get) Token: 0x06003BA3 RID: 15267 RVA: 0x000FE22C File Offset: 0x000FD22C
		// (set) Token: 0x06003BA4 RID: 15268 RVA: 0x000FE242 File Offset: 0x000FD242
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

		// Token: 0x17000E01 RID: 3585
		// (get) Token: 0x06003BA5 RID: 15269 RVA: 0x000FE24B File Offset: 0x000FD24B
		// (set) Token: 0x06003BA6 RID: 15270 RVA: 0x000FE253 File Offset: 0x000FD253
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

		// Token: 0x17000E02 RID: 3586
		// (get) Token: 0x06003BA7 RID: 15271 RVA: 0x000FE25C File Offset: 0x000FD25C
		// (set) Token: 0x06003BA8 RID: 15272 RVA: 0x000FE272 File Offset: 0x000FD272
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

		// Token: 0x17000E03 RID: 3587
		// (get) Token: 0x06003BA9 RID: 15273 RVA: 0x000FE27B File Offset: 0x000FD27B
		// (set) Token: 0x06003BAA RID: 15274 RVA: 0x000FE283 File Offset: 0x000FD283
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

		// Token: 0x17000E04 RID: 3588
		// (get) Token: 0x06003BAB RID: 15275 RVA: 0x000FE28C File Offset: 0x000FD28C
		// (set) Token: 0x06003BAC RID: 15276 RVA: 0x000FE2A2 File Offset: 0x000FD2A2
		[DefaultValue("")]
		[NotifyParentProperty(true)]
		[Editor("System.Diagnostics.Design.StartFileNameEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[MonitoringDescription("ProcessFileName")]
		[RecommendedAsConfigurable(true)]
		[TypeConverter("System.Diagnostics.Design.StringValueConverter, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
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

		// Token: 0x17000E05 RID: 3589
		// (get) Token: 0x06003BAD RID: 15277 RVA: 0x000FE2AB File Offset: 0x000FD2AB
		// (set) Token: 0x06003BAE RID: 15278 RVA: 0x000FE2C1 File Offset: 0x000FD2C1
		[RecommendedAsConfigurable(true)]
		[DefaultValue("")]
		[MonitoringDescription("ProcessWorkingDirectory")]
		[Editor("System.Diagnostics.Design.WorkingDirectoryEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[TypeConverter("System.Diagnostics.Design.StringValueConverter, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
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

		// Token: 0x17000E06 RID: 3590
		// (get) Token: 0x06003BAF RID: 15279 RVA: 0x000FE2CA File Offset: 0x000FD2CA
		// (set) Token: 0x06003BB0 RID: 15280 RVA: 0x000FE2D2 File Offset: 0x000FD2D2
		[DefaultValue(false)]
		[NotifyParentProperty(true)]
		[MonitoringDescription("ProcessErrorDialog")]
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

		// Token: 0x17000E07 RID: 3591
		// (get) Token: 0x06003BB1 RID: 15281 RVA: 0x000FE2DB File Offset: 0x000FD2DB
		// (set) Token: 0x06003BB2 RID: 15282 RVA: 0x000FE2E3 File Offset: 0x000FD2E3
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

		// Token: 0x17000E08 RID: 3592
		// (get) Token: 0x06003BB3 RID: 15283 RVA: 0x000FE2EC File Offset: 0x000FD2EC
		// (set) Token: 0x06003BB4 RID: 15284 RVA: 0x000FE2F4 File Offset: 0x000FD2F4
		[NotifyParentProperty(true)]
		[DefaultValue(ProcessWindowStyle.Normal)]
		[MonitoringDescription("ProcessWindowStyle")]
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

		// Token: 0x04003441 RID: 13377
		private string fileName;

		// Token: 0x04003442 RID: 13378
		private string arguments;

		// Token: 0x04003443 RID: 13379
		private string directory;

		// Token: 0x04003444 RID: 13380
		private string verb;

		// Token: 0x04003445 RID: 13381
		private ProcessWindowStyle windowStyle;

		// Token: 0x04003446 RID: 13382
		private bool errorDialog;

		// Token: 0x04003447 RID: 13383
		private IntPtr errorDialogParentHandle;

		// Token: 0x04003448 RID: 13384
		private bool useShellExecute = true;

		// Token: 0x04003449 RID: 13385
		private string userName;

		// Token: 0x0400344A RID: 13386
		private string domain;

		// Token: 0x0400344B RID: 13387
		private SecureString password;

		// Token: 0x0400344C RID: 13388
		private bool loadUserProfile;

		// Token: 0x0400344D RID: 13389
		private bool redirectStandardInput;

		// Token: 0x0400344E RID: 13390
		private bool redirectStandardOutput;

		// Token: 0x0400344F RID: 13391
		private bool redirectStandardError;

		// Token: 0x04003450 RID: 13392
		private Encoding standardOutputEncoding;

		// Token: 0x04003451 RID: 13393
		private Encoding standardErrorEncoding;

		// Token: 0x04003452 RID: 13394
		private bool createNoWindow;

		// Token: 0x04003453 RID: 13395
		private WeakReference weakParentProcess;

		// Token: 0x04003454 RID: 13396
		internal StringDictionary environmentVariables;
	}
}
