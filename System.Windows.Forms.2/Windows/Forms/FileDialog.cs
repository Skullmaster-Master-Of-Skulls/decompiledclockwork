using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using System.Text;
using System.Threading;
using System.Windows.Forms.VisualStyles;

namespace System.Windows.Forms
{
	// Token: 0x0200024F RID: 591
	[DefaultEvent("FileOk")]
	[DefaultProperty("FileName")]
	public abstract class FileDialog : CommonDialog
	{
		// Token: 0x06002559 RID: 9561 RVA: 0x000AE5D5 File Offset: 0x000AC7D5
		internal FileDialog()
		{
			this.Reset();
		}

		// Token: 0x17000894 RID: 2196
		// (get) Token: 0x0600255A RID: 9562 RVA: 0x000AE5F5 File Offset: 0x000AC7F5
		// (set) Token: 0x0600255B RID: 9563 RVA: 0x000AE602 File Offset: 0x000AC802
		[SRCategory("CatBehavior")]
		[DefaultValue(true)]
		[SRDescription("FDaddExtensionDescr")]
		public bool AddExtension
		{
			get
			{
				return this.GetOption(int.MinValue);
			}
			set
			{
				IntSecurity.FileDialogCustomization.Demand();
				this.SetOption(int.MinValue, value);
			}
		}

		// Token: 0x17000895 RID: 2197
		// (get) Token: 0x0600255C RID: 9564 RVA: 0x000AE61A File Offset: 0x000AC81A
		// (set) Token: 0x0600255D RID: 9565 RVA: 0x000AE627 File Offset: 0x000AC827
		[SRCategory("CatBehavior")]
		[DefaultValue(false)]
		[SRDescription("FDcheckFileExistsDescr")]
		public virtual bool CheckFileExists
		{
			get
			{
				return this.GetOption(4096);
			}
			set
			{
				IntSecurity.FileDialogCustomization.Demand();
				this.SetOption(4096, value);
			}
		}

		// Token: 0x17000896 RID: 2198
		// (get) Token: 0x0600255E RID: 9566 RVA: 0x000AE63F File Offset: 0x000AC83F
		// (set) Token: 0x0600255F RID: 9567 RVA: 0x000AE64C File Offset: 0x000AC84C
		[SRCategory("CatBehavior")]
		[DefaultValue(true)]
		[SRDescription("FDcheckPathExistsDescr")]
		public bool CheckPathExists
		{
			get
			{
				return this.GetOption(2048);
			}
			set
			{
				IntSecurity.FileDialogCustomization.Demand();
				this.SetOption(2048, value);
			}
		}

		// Token: 0x17000897 RID: 2199
		// (get) Token: 0x06002560 RID: 9568 RVA: 0x000AE664 File Offset: 0x000AC864
		// (set) Token: 0x06002561 RID: 9569 RVA: 0x000AE67A File Offset: 0x000AC87A
		[SRCategory("CatBehavior")]
		[DefaultValue("")]
		[SRDescription("FDdefaultExtDescr")]
		public string DefaultExt
		{
			get
			{
				if (this.defaultExt != null)
				{
					return this.defaultExt;
				}
				return "";
			}
			set
			{
				if (value != null)
				{
					if (value.StartsWith("."))
					{
						value = value.Substring(1);
					}
					else if (value.Length == 0)
					{
						value = null;
					}
				}
				this.defaultExt = value;
			}
		}

		// Token: 0x17000898 RID: 2200
		// (get) Token: 0x06002562 RID: 9570 RVA: 0x000AE6A9 File Offset: 0x000AC8A9
		// (set) Token: 0x06002563 RID: 9571 RVA: 0x000AE6B9 File Offset: 0x000AC8B9
		[SRCategory("CatBehavior")]
		[DefaultValue(true)]
		[SRDescription("FDdereferenceLinksDescr")]
		public bool DereferenceLinks
		{
			get
			{
				return !this.GetOption(1048576);
			}
			set
			{
				IntSecurity.FileDialogCustomization.Demand();
				this.SetOption(1048576, !value);
			}
		}

		// Token: 0x17000899 RID: 2201
		// (get) Token: 0x06002564 RID: 9572 RVA: 0x000AE6D4 File Offset: 0x000AC8D4
		internal string DialogCaption
		{
			get
			{
				int windowTextLength = SafeNativeMethods.GetWindowTextLength(new HandleRef(this, this.dialogHWnd));
				StringBuilder stringBuilder = new StringBuilder(windowTextLength + 1);
				UnsafeNativeMethods.GetWindowText(new HandleRef(this, this.dialogHWnd), stringBuilder, stringBuilder.Capacity);
				return stringBuilder.ToString();
			}
		}

		// Token: 0x1700089A RID: 2202
		// (get) Token: 0x06002565 RID: 9573 RVA: 0x000AE71C File Offset: 0x000AC91C
		// (set) Token: 0x06002566 RID: 9574 RVA: 0x000AE76C File Offset: 0x000AC96C
		[SRCategory("CatData")]
		[DefaultValue("")]
		[SRDescription("FDfileNameDescr")]
		public string FileName
		{
			get
			{
				if (this.fileNames == null)
				{
					return "";
				}
				if (this.fileNames[0].Length > 0)
				{
					if (this.securityCheckFileNames)
					{
						IntSecurity.DemandFileIO(FileIOPermissionAccess.AllAccess, this.fileNames[0]);
					}
					return this.fileNames[0];
				}
				return "";
			}
			set
			{
				IntSecurity.FileDialogCustomization.Demand();
				if (value == null)
				{
					this.fileNames = null;
				}
				else
				{
					this.fileNames = new string[]
					{
						value
					};
				}
				this.securityCheckFileNames = false;
			}
		}

		// Token: 0x1700089B RID: 2203
		// (get) Token: 0x06002567 RID: 9575 RVA: 0x000AE79C File Offset: 0x000AC99C
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[SRDescription("FDFileNamesDescr")]
		public string[] FileNames
		{
			get
			{
				string[] fileNamesInternal = this.FileNamesInternal;
				if (this.securityCheckFileNames)
				{
					foreach (string fileName in fileNamesInternal)
					{
						IntSecurity.DemandFileIO(FileIOPermissionAccess.AllAccess, fileName);
					}
				}
				return fileNamesInternal;
			}
		}

		// Token: 0x1700089C RID: 2204
		// (get) Token: 0x06002568 RID: 9576 RVA: 0x000AE7D5 File Offset: 0x000AC9D5
		internal string[] FileNamesInternal
		{
			get
			{
				if (this.fileNames == null)
				{
					return new string[0];
				}
				return (string[])this.fileNames.Clone();
			}
		}

		// Token: 0x1700089D RID: 2205
		// (get) Token: 0x06002569 RID: 9577 RVA: 0x000AE7F6 File Offset: 0x000AC9F6
		// (set) Token: 0x0600256A RID: 9578 RVA: 0x000AE80C File Offset: 0x000ACA0C
		[SRCategory("CatBehavior")]
		[DefaultValue("")]
		[Localizable(true)]
		[SRDescription("FDfilterDescr")]
		public string Filter
		{
			get
			{
				if (this.filter != null)
				{
					return this.filter;
				}
				return "";
			}
			set
			{
				if (value != this.filter)
				{
					if (value != null && value.Length > 0)
					{
						string[] array = value.Split(new char[]
						{
							'|'
						});
						if (array == null || array.Length % 2 != 0)
						{
							throw new ArgumentException(SR.GetString("FileDialogInvalidFilter"));
						}
					}
					else
					{
						value = null;
					}
					this.filter = value;
				}
			}
		}

		// Token: 0x1700089E RID: 2206
		// (get) Token: 0x0600256B RID: 9579 RVA: 0x000AE86C File Offset: 0x000ACA6C
		private string[] FilterExtensions
		{
			get
			{
				string text = this.filter;
				ArrayList arrayList = new ArrayList();
				if (this.defaultExt != null)
				{
					arrayList.Add(this.defaultExt);
				}
				if (text != null)
				{
					string[] array = text.Split(new char[]
					{
						'|'
					});
					if (this.filterIndex * 2 - 1 >= array.Length)
					{
						throw new InvalidOperationException(SR.GetString("FileDialogInvalidFilterIndex"));
					}
					if (this.filterIndex > 0)
					{
						string[] array2 = array[this.filterIndex * 2 - 1].Split(new char[]
						{
							';'
						});
						foreach (string text2 in array2)
						{
							int num = this.supportMultiDottedExtensions ? text2.IndexOf('.') : text2.LastIndexOf('.');
							if (num >= 0)
							{
								arrayList.Add(text2.Substring(num + 1, text2.Length - (num + 1)));
							}
						}
					}
				}
				string[] array4 = new string[arrayList.Count];
				arrayList.CopyTo(array4, 0);
				return array4;
			}
		}

		// Token: 0x1700089F RID: 2207
		// (get) Token: 0x0600256C RID: 9580 RVA: 0x000AE96D File Offset: 0x000ACB6D
		// (set) Token: 0x0600256D RID: 9581 RVA: 0x000AE975 File Offset: 0x000ACB75
		[SRCategory("CatBehavior")]
		[DefaultValue(1)]
		[SRDescription("FDfilterIndexDescr")]
		public int FilterIndex
		{
			get
			{
				return this.filterIndex;
			}
			set
			{
				this.filterIndex = value;
			}
		}

		// Token: 0x170008A0 RID: 2208
		// (get) Token: 0x0600256E RID: 9582 RVA: 0x000AE97E File Offset: 0x000ACB7E
		// (set) Token: 0x0600256F RID: 9583 RVA: 0x000AE994 File Offset: 0x000ACB94
		[SRCategory("CatData")]
		[DefaultValue("")]
		[SRDescription("FDinitialDirDescr")]
		public string InitialDirectory
		{
			get
			{
				if (this.initialDir != null)
				{
					return this.initialDir;
				}
				return "";
			}
			set
			{
				IntSecurity.FileDialogCustomization.Demand();
				this.initialDir = value;
			}
		}

		// Token: 0x170008A1 RID: 2209
		// (get) Token: 0x06002570 RID: 9584 RVA: 0x00027186 File Offset: 0x00025386
		protected virtual IntPtr Instance
		{
			[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
			[SecurityPermission(SecurityAction.InheritanceDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
			get
			{
				return UnsafeNativeMethods.GetModuleHandle(null);
			}
		}

		// Token: 0x170008A2 RID: 2210
		// (get) Token: 0x06002571 RID: 9585 RVA: 0x000AE9A7 File Offset: 0x000ACBA7
		protected int Options
		{
			get
			{
				return this.options & 1051421;
			}
		}

		// Token: 0x170008A3 RID: 2211
		// (get) Token: 0x06002572 RID: 9586 RVA: 0x000AE9B5 File Offset: 0x000ACBB5
		// (set) Token: 0x06002573 RID: 9587 RVA: 0x000AE9BE File Offset: 0x000ACBBE
		[SRCategory("CatBehavior")]
		[DefaultValue(false)]
		[SRDescription("FDrestoreDirectoryDescr")]
		public bool RestoreDirectory
		{
			get
			{
				return this.GetOption(8);
			}
			set
			{
				IntSecurity.FileDialogCustomization.Demand();
				this.SetOption(8, value);
			}
		}

		// Token: 0x170008A4 RID: 2212
		// (get) Token: 0x06002574 RID: 9588 RVA: 0x000AE9D2 File Offset: 0x000ACBD2
		// (set) Token: 0x06002575 RID: 9589 RVA: 0x000AE9DC File Offset: 0x000ACBDC
		[SRCategory("CatBehavior")]
		[DefaultValue(false)]
		[SRDescription("FDshowHelpDescr")]
		public bool ShowHelp
		{
			get
			{
				return this.GetOption(16);
			}
			set
			{
				this.SetOption(16, value);
			}
		}

		// Token: 0x170008A5 RID: 2213
		// (get) Token: 0x06002576 RID: 9590 RVA: 0x000AE9E7 File Offset: 0x000ACBE7
		// (set) Token: 0x06002577 RID: 9591 RVA: 0x000AE9EF File Offset: 0x000ACBEF
		[SRCategory("CatBehavior")]
		[DefaultValue(false)]
		[SRDescription("FDsupportMultiDottedExtensionsDescr")]
		public bool SupportMultiDottedExtensions
		{
			get
			{
				return this.supportMultiDottedExtensions;
			}
			set
			{
				this.supportMultiDottedExtensions = value;
			}
		}

		// Token: 0x170008A6 RID: 2214
		// (get) Token: 0x06002578 RID: 9592 RVA: 0x000AE9F8 File Offset: 0x000ACBF8
		// (set) Token: 0x06002579 RID: 9593 RVA: 0x000AEA0E File Offset: 0x000ACC0E
		[SRCategory("CatAppearance")]
		[DefaultValue("")]
		[Localizable(true)]
		[SRDescription("FDtitleDescr")]
		public string Title
		{
			get
			{
				if (this.title != null)
				{
					return this.title;
				}
				return "";
			}
			set
			{
				IntSecurity.FileDialogCustomization.Demand();
				this.title = value;
			}
		}

		// Token: 0x170008A7 RID: 2215
		// (get) Token: 0x0600257A RID: 9594 RVA: 0x000AEA21 File Offset: 0x000ACC21
		// (set) Token: 0x0600257B RID: 9595 RVA: 0x000AEA31 File Offset: 0x000ACC31
		[SRCategory("CatBehavior")]
		[DefaultValue(true)]
		[SRDescription("FDvalidateNamesDescr")]
		public bool ValidateNames
		{
			get
			{
				return !this.GetOption(256);
			}
			set
			{
				IntSecurity.FileDialogCustomization.Demand();
				this.SetOption(256, !value);
			}
		}

		// Token: 0x1400019A RID: 410
		// (add) Token: 0x0600257C RID: 9596 RVA: 0x000AEA4C File Offset: 0x000ACC4C
		// (remove) Token: 0x0600257D RID: 9597 RVA: 0x000AEA5F File Offset: 0x000ACC5F
		[SRDescription("FDfileOkDescr")]
		public event CancelEventHandler FileOk
		{
			add
			{
				base.Events.AddHandler(FileDialog.EventFileOk, value);
			}
			remove
			{
				base.Events.RemoveHandler(FileDialog.EventFileOk, value);
			}
		}

		// Token: 0x0600257E RID: 9598 RVA: 0x000AEA74 File Offset: 0x000ACC74
		private bool DoFileOk(IntPtr lpOFN)
		{
			NativeMethods.OPENFILENAME_I openfilename_I = (NativeMethods.OPENFILENAME_I)UnsafeNativeMethods.PtrToStructure(lpOFN, typeof(NativeMethods.OPENFILENAME_I));
			int num = this.options;
			int num2 = this.filterIndex;
			string[] array = this.fileNames;
			bool flag = this.securityCheckFileNames;
			bool flag2 = false;
			try
			{
				this.options = ((this.options & -2) | (openfilename_I.Flags & 1));
				this.filterIndex = openfilename_I.nFilterIndex;
				this.charBuffer.PutCoTaskMem(openfilename_I.lpstrFile);
				this.securityCheckFileNames = true;
				Thread.MemoryBarrier();
				if ((this.options & 512) == 0)
				{
					this.fileNames = new string[]
					{
						this.charBuffer.GetString()
					};
				}
				else
				{
					this.fileNames = this.GetMultiselectFiles(this.charBuffer);
				}
				if (this.ProcessFileNames())
				{
					CancelEventArgs cancelEventArgs = new CancelEventArgs();
					if (NativeWindow.WndProcShouldBeDebuggable)
					{
						this.OnFileOk(cancelEventArgs);
						flag2 = !cancelEventArgs.Cancel;
					}
					else
					{
						try
						{
							this.OnFileOk(cancelEventArgs);
							flag2 = !cancelEventArgs.Cancel;
						}
						catch (Exception t)
						{
							Application.OnThreadException(t);
						}
					}
				}
			}
			finally
			{
				if (!flag2)
				{
					this.securityCheckFileNames = flag;
					Thread.MemoryBarrier();
					this.fileNames = array;
					this.options = num;
					this.filterIndex = num2;
				}
			}
			return flag2;
		}

		// Token: 0x0600257F RID: 9599 RVA: 0x000AEBCC File Offset: 0x000ACDCC
		internal static bool FileExists(string fileName)
		{
			bool result = false;
			try
			{
				new FileIOPermission(FileIOPermissionAccess.Read, IntSecurity.UnsafeGetFullPath(fileName)).Assert();
				try
				{
					result = File.Exists(fileName);
				}
				finally
				{
					CodeAccessPermission.RevertAssert();
				}
			}
			catch (PathTooLongException)
			{
			}
			return result;
		}

		// Token: 0x06002580 RID: 9600 RVA: 0x000AEC20 File Offset: 0x000ACE20
		private string[] GetMultiselectFiles(UnsafeNativeMethods.CharBuffer charBuffer)
		{
			string text = charBuffer.GetString();
			string text2 = charBuffer.GetString();
			if (text2.Length == 0)
			{
				return new string[]
				{
					text
				};
			}
			if (text[text.Length - 1] != '\\')
			{
				text += "\\";
			}
			ArrayList arrayList = new ArrayList();
			do
			{
				if (text2[0] != '\\' && (text2.Length <= 3 || text2[1] != ':' || text2[2] != '\\'))
				{
					text2 = text + text2;
				}
				arrayList.Add(text2);
				text2 = charBuffer.GetString();
			}
			while (text2.Length > 0);
			string[] array = new string[arrayList.Count];
			arrayList.CopyTo(array, 0);
			return array;
		}

		// Token: 0x06002581 RID: 9601 RVA: 0x000AECD1 File Offset: 0x000ACED1
		internal bool GetOption(int option)
		{
			return (this.options & option) != 0;
		}

		// Token: 0x06002582 RID: 9602 RVA: 0x000AECE0 File Offset: 0x000ACEE0
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		protected override IntPtr HookProc(IntPtr hWnd, int msg, IntPtr wparam, IntPtr lparam)
		{
			if (msg == 78)
			{
				this.dialogHWnd = UnsafeNativeMethods.GetParent(new HandleRef(null, hWnd));
				try
				{
					UnsafeNativeMethods.OFNOTIFY ofnotify = (UnsafeNativeMethods.OFNOTIFY)UnsafeNativeMethods.PtrToStructure(lparam, typeof(UnsafeNativeMethods.OFNOTIFY));
					switch (ofnotify.hdr_code)
					{
					case -606:
						if (this.ignoreSecondFileOkNotification)
						{
							if (this.okNotificationCount != 0)
							{
								this.ignoreSecondFileOkNotification = false;
								UnsafeNativeMethods.SetWindowLong(new HandleRef(null, hWnd), 0, new HandleRef(null, NativeMethods.InvalidIntPtr));
								return NativeMethods.InvalidIntPtr;
							}
							this.okNotificationCount = 1;
						}
						if (!this.DoFileOk(ofnotify.lpOFN))
						{
							UnsafeNativeMethods.SetWindowLong(new HandleRef(null, hWnd), 0, new HandleRef(null, NativeMethods.InvalidIntPtr));
							return NativeMethods.InvalidIntPtr;
						}
						break;
					case -604:
						this.ignoreSecondFileOkNotification = true;
						this.okNotificationCount = 0;
						break;
					case -602:
					{
						NativeMethods.OPENFILENAME_I openfilename_I = (NativeMethods.OPENFILENAME_I)UnsafeNativeMethods.PtrToStructure(ofnotify.lpOFN, typeof(NativeMethods.OPENFILENAME_I));
						int num = (int)UnsafeNativeMethods.SendMessage(new HandleRef(this, this.dialogHWnd), 1124, IntPtr.Zero, IntPtr.Zero);
						if (num > openfilename_I.nMaxFile)
						{
							try
							{
								int num2 = num + 2048;
								UnsafeNativeMethods.CharBuffer charBuffer = UnsafeNativeMethods.CharBuffer.CreateBuffer(num2);
								IntPtr lpstrFile = charBuffer.AllocCoTaskMem();
								Marshal.FreeCoTaskMem(openfilename_I.lpstrFile);
								openfilename_I.lpstrFile = lpstrFile;
								openfilename_I.nMaxFile = num2;
								this.charBuffer = charBuffer;
								Marshal.StructureToPtr(openfilename_I, ofnotify.lpOFN, true);
								Marshal.StructureToPtr(ofnotify, lparam, true);
							}
							catch
							{
							}
						}
						this.ignoreSecondFileOkNotification = false;
						break;
					}
					case -601:
						CommonDialog.MoveToScreenCenter(this.dialogHWnd);
						break;
					}
				}
				catch
				{
					if (this.dialogHWnd != IntPtr.Zero)
					{
						UnsafeNativeMethods.EndDialog(new HandleRef(this, this.dialogHWnd), IntPtr.Zero);
					}
					throw;
				}
			}
			return IntPtr.Zero;
		}

		// Token: 0x06002583 RID: 9603 RVA: 0x000AEEFC File Offset: 0x000AD0FC
		private static string MakeFilterString(string s, bool dereferenceLinks)
		{
			if (s == null || s.Length == 0)
			{
				if (dereferenceLinks && Environment.OSVersion.Version.Major >= 5)
				{
					s = " |*.*";
				}
				else if (s == null)
				{
					return null;
				}
			}
			int length = s.Length;
			char[] array = new char[length + 2];
			s.CopyTo(0, array, 0, length);
			for (int i = 0; i < length; i++)
			{
				if (array[i] == '|')
				{
					array[i] = '\0';
				}
			}
			array[length + 1] = '\0';
			return new string(array);
		}

		// Token: 0x06002584 RID: 9604 RVA: 0x000AEF74 File Offset: 0x000AD174
		protected void OnFileOk(CancelEventArgs e)
		{
			CancelEventHandler cancelEventHandler = (CancelEventHandler)base.Events[FileDialog.EventFileOk];
			if (cancelEventHandler != null)
			{
				cancelEventHandler(this, e);
			}
		}

		// Token: 0x06002585 RID: 9605 RVA: 0x000AEFA4 File Offset: 0x000AD1A4
		private bool ProcessFileNames()
		{
			if ((this.options & 256) == 0)
			{
				string[] filterExtensions = this.FilterExtensions;
				for (int i = 0; i < this.fileNames.Length; i++)
				{
					string text = this.fileNames[i];
					if ((this.options & -2147483648) != 0 && !Path.HasExtension(text))
					{
						bool flag = (this.options & 4096) != 0;
						for (int j = 0; j < filterExtensions.Length; j++)
						{
							string extension = Path.GetExtension(text);
							string text2 = text.Substring(0, text.Length - extension.Length);
							if (filterExtensions[j].IndexOfAny(new char[]
							{
								'*',
								'?'
							}) == -1)
							{
								text2 = text2 + "." + filterExtensions[j];
							}
							if (!flag || FileDialog.FileExists(text2))
							{
								text = text2;
								break;
							}
						}
						this.fileNames[i] = text;
					}
					if (!this.PromptUserIfAppropriate(text))
					{
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x06002586 RID: 9606 RVA: 0x000AF09C File Offset: 0x000AD29C
		internal bool MessageBoxWithFocusRestore(string message, string caption, MessageBoxButtons buttons, MessageBoxIcon icon)
		{
			IntPtr focus = UnsafeNativeMethods.GetFocus();
			bool result;
			try
			{
				result = (RTLAwareMessageBox.Show(null, message, caption, buttons, icon, MessageBoxDefaultButton.Button1, (MessageBoxOptions)0) == DialogResult.Yes);
			}
			finally
			{
				UnsafeNativeMethods.SetFocus(new HandleRef(null, focus));
			}
			return result;
		}

		// Token: 0x06002587 RID: 9607 RVA: 0x000AF0E4 File Offset: 0x000AD2E4
		private void PromptFileNotFound(string fileName)
		{
			this.MessageBoxWithFocusRestore(SR.GetString("FileDialogFileNotFound", new object[]
			{
				fileName
			}), this.DialogCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
		}

		// Token: 0x06002588 RID: 9608 RVA: 0x000AF10A File Offset: 0x000AD30A
		internal virtual bool PromptUserIfAppropriate(string fileName)
		{
			if ((this.options & 4096) != 0 && !FileDialog.FileExists(fileName))
			{
				this.PromptFileNotFound(fileName);
				return false;
			}
			return true;
		}

		// Token: 0x06002589 RID: 9609 RVA: 0x000AF12C File Offset: 0x000AD32C
		public override void Reset()
		{
			this.options = -2147481596;
			this.title = null;
			this.initialDir = null;
			this.defaultExt = null;
			this.fileNames = null;
			this.filter = null;
			this.filterIndex = 1;
			this.supportMultiDottedExtensions = false;
			this._customPlaces.Clear();
		}

		// Token: 0x0600258A RID: 9610 RVA: 0x000AF180 File Offset: 0x000AD380
		protected override bool RunDialog(IntPtr hWndOwner)
		{
			if (Control.CheckForIllegalCrossThreadCalls && Application.OleRequired() != ApartmentState.STA)
			{
				throw new ThreadStateException(SR.GetString("DebuggingExceptionOnly", new object[]
				{
					SR.GetString("ThreadMustBeSTA")
				}));
			}
			this.EnsureFileDialogPermission();
			if (this.UseVistaDialogInternal)
			{
				return this.RunDialogVista(hWndOwner);
			}
			return this.RunDialogOld(hWndOwner);
		}

		// Token: 0x0600258B RID: 9611
		internal abstract void EnsureFileDialogPermission();

		// Token: 0x0600258C RID: 9612 RVA: 0x000AF1DC File Offset: 0x000AD3DC
		private bool RunDialogOld(IntPtr hWndOwner)
		{
			NativeMethods.WndProc lpfnHook = new NativeMethods.WndProc(this.HookProc);
			NativeMethods.OPENFILENAME_I openfilename_I = new NativeMethods.OPENFILENAME_I();
			bool result;
			try
			{
				this.charBuffer = UnsafeNativeMethods.CharBuffer.CreateBuffer(8192);
				if (this.fileNames != null)
				{
					this.charBuffer.PutString(this.fileNames[0]);
				}
				openfilename_I.lStructSize = Marshal.SizeOf(typeof(NativeMethods.OPENFILENAME_I));
				if (Environment.OSVersion.Platform != PlatformID.Win32NT || Environment.OSVersion.Version.Major < 5)
				{
					openfilename_I.lStructSize = 76;
				}
				openfilename_I.hwndOwner = hWndOwner;
				openfilename_I.hInstance = this.Instance;
				openfilename_I.lpstrFilter = FileDialog.MakeFilterString(this.filter, this.DereferenceLinks);
				openfilename_I.nFilterIndex = this.filterIndex;
				openfilename_I.lpstrFile = this.charBuffer.AllocCoTaskMem();
				openfilename_I.nMaxFile = 8192;
				openfilename_I.lpstrInitialDir = this.initialDir;
				openfilename_I.lpstrTitle = this.title;
				openfilename_I.Flags = (this.Options | 8912928);
				openfilename_I.lpfnHook = lpfnHook;
				openfilename_I.FlagsEx = 16777216;
				if (this.defaultExt != null && this.AddExtension)
				{
					openfilename_I.lpstrDefExt = this.defaultExt;
				}
				result = this.RunFileDialog(openfilename_I);
			}
			finally
			{
				this.charBuffer = null;
				if (openfilename_I.lpstrFile != IntPtr.Zero)
				{
					Marshal.FreeCoTaskMem(openfilename_I.lpstrFile);
				}
			}
			return result;
		}

		// Token: 0x0600258D RID: 9613
		internal abstract bool RunFileDialog(NativeMethods.OPENFILENAME_I ofn);

		// Token: 0x0600258E RID: 9614 RVA: 0x000AF35C File Offset: 0x000AD55C
		internal void SetOption(int option, bool value)
		{
			if (value)
			{
				this.options |= option;
				return;
			}
			this.options &= ~option;
		}

		// Token: 0x0600258F RID: 9615 RVA: 0x000AF380 File Offset: 0x000AD580
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(base.ToString() + ": Title: " + this.Title + ", FileName: ");
			try
			{
				stringBuilder.Append(this.FileName);
			}
			catch (Exception ex)
			{
				stringBuilder.Append("<");
				stringBuilder.Append(ex.GetType().FullName);
				stringBuilder.Append(">");
			}
			return stringBuilder.ToString();
		}

		// Token: 0x170008A8 RID: 2216
		// (get) Token: 0x06002590 RID: 9616 RVA: 0x000AF400 File Offset: 0x000AD600
		internal virtual bool SettingsSupportVistaDialog
		{
			get
			{
				return !this.ShowHelp && (Application.VisualStyleState & VisualStyleState.ClientAreaEnabled) == VisualStyleState.ClientAreaEnabled;
			}
		}

		// Token: 0x170008A9 RID: 2217
		// (get) Token: 0x06002591 RID: 9617 RVA: 0x000AF418 File Offset: 0x000AD618
		internal bool UseVistaDialogInternal
		{
			get
			{
				if (UnsafeNativeMethods.IsVista && this._autoUpgradeEnabled && this.SettingsSupportVistaDialog)
				{
					new EnvironmentPermission(PermissionState.Unrestricted).Assert();
					try
					{
						return SystemInformation.BootMode == BootMode.Normal;
					}
					finally
					{
						CodeAccessPermission.RevertAssert();
					}
					return false;
				}
				return false;
			}
		}

		// Token: 0x06002592 RID: 9618
		internal abstract FileDialogNative.IFileDialog CreateVistaDialog();

		// Token: 0x06002593 RID: 9619 RVA: 0x000AF46C File Offset: 0x000AD66C
		[SecurityPermission(SecurityAction.Assert, Flags = SecurityPermissionFlag.UnmanagedCode)]
		private bool RunDialogVista(IntPtr hWndOwner)
		{
			FileDialogNative.IFileDialog fileDialog = this.CreateVistaDialog();
			this.OnBeforeVistaDialog(fileDialog);
			FileDialog.VistaDialogEvents vistaDialogEvents = new FileDialog.VistaDialogEvents(this);
			uint dwCookie;
			fileDialog.Advise(vistaDialogEvents, out dwCookie);
			bool result;
			try
			{
				int num = fileDialog.Show(hWndOwner);
				result = (num == 0);
			}
			finally
			{
				fileDialog.Unadvise(dwCookie);
				GC.KeepAlive(vistaDialogEvents);
			}
			return result;
		}

		// Token: 0x06002594 RID: 9620 RVA: 0x000AF4C8 File Offset: 0x000AD6C8
		internal virtual void OnBeforeVistaDialog(FileDialogNative.IFileDialog dialog)
		{
			dialog.SetDefaultExtension(this.DefaultExt);
			dialog.SetFileName(this.FileName);
			if (!string.IsNullOrEmpty(this.InitialDirectory))
			{
				try
				{
					FileDialogNative.IShellItem shellItemForPath = FileDialog.GetShellItemForPath(this.InitialDirectory);
					dialog.SetDefaultFolder(shellItemForPath);
					dialog.SetFolder(shellItemForPath);
				}
				catch (FileNotFoundException)
				{
				}
			}
			dialog.SetTitle(this.Title);
			dialog.SetOptions(this.GetOptions());
			this.SetFileTypes(dialog);
			this._customPlaces.Apply(dialog);
		}

		// Token: 0x06002595 RID: 9621 RVA: 0x000AF554 File Offset: 0x000AD754
		private FileDialogNative.FOS GetOptions()
		{
			FileDialogNative.FOS fos = (FileDialogNative.FOS)(this.options & 1063690);
			fos |= FileDialogNative.FOS.FOS_DEFAULTNOMINIMODE;
			return fos | FileDialogNative.FOS.FOS_FORCEFILESYSTEM;
		}

		// Token: 0x06002596 RID: 9622
		internal abstract string[] ProcessVistaFiles(FileDialogNative.IFileDialog dialog);

		// Token: 0x06002597 RID: 9623 RVA: 0x000AF57C File Offset: 0x000AD77C
		private bool HandleVistaFileOk(FileDialogNative.IFileDialog dialog)
		{
			int num = this.options;
			int num2 = this.filterIndex;
			string[] array = this.fileNames;
			bool flag = this.securityCheckFileNames;
			bool flag2 = false;
			try
			{
				this.securityCheckFileNames = true;
				Thread.MemoryBarrier();
				uint num3;
				dialog.GetFileTypeIndex(out num3);
				this.filterIndex = (int)num3;
				this.fileNames = this.ProcessVistaFiles(dialog);
				if (this.ProcessFileNames())
				{
					CancelEventArgs cancelEventArgs = new CancelEventArgs();
					if (NativeWindow.WndProcShouldBeDebuggable)
					{
						this.OnFileOk(cancelEventArgs);
						flag2 = !cancelEventArgs.Cancel;
					}
					else
					{
						try
						{
							this.OnFileOk(cancelEventArgs);
							flag2 = !cancelEventArgs.Cancel;
						}
						catch (Exception t)
						{
							Application.OnThreadException(t);
						}
					}
				}
			}
			finally
			{
				if (!flag2)
				{
					this.securityCheckFileNames = flag;
					Thread.MemoryBarrier();
					this.fileNames = array;
					this.options = num;
					this.filterIndex = num2;
				}
				else if ((this.options & 4) != 0)
				{
					this.options &= -2;
				}
			}
			return flag2;
		}

		// Token: 0x06002598 RID: 9624 RVA: 0x000AF680 File Offset: 0x000AD880
		private void SetFileTypes(FileDialogNative.IFileDialog dialog)
		{
			FileDialogNative.COMDLG_FILTERSPEC[] filterItems = this.FilterItems;
			dialog.SetFileTypes((uint)filterItems.Length, filterItems);
			if (filterItems.Length != 0)
			{
				dialog.SetFileTypeIndex((uint)this.filterIndex);
			}
		}

		// Token: 0x170008AA RID: 2218
		// (get) Token: 0x06002599 RID: 9625 RVA: 0x000AF6AE File Offset: 0x000AD8AE
		private FileDialogNative.COMDLG_FILTERSPEC[] FilterItems
		{
			get
			{
				return FileDialog.GetFilterItems(this.filter);
			}
		}

		// Token: 0x0600259A RID: 9626 RVA: 0x000AF6BC File Offset: 0x000AD8BC
		private static FileDialogNative.COMDLG_FILTERSPEC[] GetFilterItems(string filter)
		{
			List<FileDialogNative.COMDLG_FILTERSPEC> list = new List<FileDialogNative.COMDLG_FILTERSPEC>();
			if (!string.IsNullOrEmpty(filter))
			{
				string[] array = filter.Split(new char[]
				{
					'|'
				});
				if (array.Length % 2 == 0)
				{
					for (int i = 1; i < array.Length; i += 2)
					{
						FileDialogNative.COMDLG_FILTERSPEC item;
						item.pszSpec = array[i];
						item.pszName = array[i - 1];
						list.Add(item);
					}
				}
			}
			return list.ToArray();
		}

		// Token: 0x0600259B RID: 9627 RVA: 0x000AF724 File Offset: 0x000AD924
		internal static FileDialogNative.IShellItem GetShellItemForPath(string path)
		{
			FileDialogNative.IShellItem result = null;
			IntPtr zero = IntPtr.Zero;
			uint num = 0U;
			if (0 <= UnsafeNativeMethods.Shell32.SHILCreateFromPath(path, out zero, ref num) && 0 <= UnsafeNativeMethods.Shell32.SHCreateShellItem(IntPtr.Zero, IntPtr.Zero, zero, out result))
			{
				return result;
			}
			throw new FileNotFoundException();
		}

		// Token: 0x0600259C RID: 9628 RVA: 0x000AF764 File Offset: 0x000AD964
		internal static string GetFilePathFromShellItem(FileDialogNative.IShellItem item)
		{
			string result;
			item.GetDisplayName((FileDialogNative.SIGDN)2147647488U, out result);
			return result;
		}

		// Token: 0x170008AB RID: 2219
		// (get) Token: 0x0600259D RID: 9629 RVA: 0x000AF77F File Offset: 0x000AD97F
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public FileDialogCustomPlacesCollection CustomPlaces
		{
			get
			{
				return this._customPlaces;
			}
		}

		// Token: 0x170008AC RID: 2220
		// (get) Token: 0x0600259E RID: 9630 RVA: 0x000AF787 File Offset: 0x000AD987
		// (set) Token: 0x0600259F RID: 9631 RVA: 0x000AF78F File Offset: 0x000AD98F
		[DefaultValue(true)]
		public bool AutoUpgradeEnabled
		{
			get
			{
				return this._autoUpgradeEnabled;
			}
			set
			{
				this._autoUpgradeEnabled = value;
			}
		}

		// Token: 0x04000F92 RID: 3986
		private const int FILEBUFSIZE = 8192;

		// Token: 0x04000F93 RID: 3987
		protected static readonly object EventFileOk = new object();

		// Token: 0x04000F94 RID: 3988
		internal const int OPTION_ADDEXTENSION = -2147483648;

		// Token: 0x04000F95 RID: 3989
		internal int options;

		// Token: 0x04000F96 RID: 3990
		private string title;

		// Token: 0x04000F97 RID: 3991
		private string initialDir;

		// Token: 0x04000F98 RID: 3992
		private string defaultExt;

		// Token: 0x04000F99 RID: 3993
		private string[] fileNames;

		// Token: 0x04000F9A RID: 3994
		private bool securityCheckFileNames;

		// Token: 0x04000F9B RID: 3995
		private string filter;

		// Token: 0x04000F9C RID: 3996
		private int filterIndex;

		// Token: 0x04000F9D RID: 3997
		private bool supportMultiDottedExtensions;

		// Token: 0x04000F9E RID: 3998
		private bool ignoreSecondFileOkNotification;

		// Token: 0x04000F9F RID: 3999
		private int okNotificationCount;

		// Token: 0x04000FA0 RID: 4000
		private UnsafeNativeMethods.CharBuffer charBuffer;

		// Token: 0x04000FA1 RID: 4001
		private IntPtr dialogHWnd;

		// Token: 0x04000FA2 RID: 4002
		private bool _autoUpgradeEnabled = true;

		// Token: 0x04000FA3 RID: 4003
		private FileDialogCustomPlacesCollection _customPlaces = new FileDialogCustomPlacesCollection();

		// Token: 0x0200068C RID: 1676
		private class VistaDialogEvents : FileDialogNative.IFileDialogEvents
		{
			// Token: 0x0600676E RID: 26478 RVA: 0x00183948 File Offset: 0x00181B48
			public VistaDialogEvents(FileDialog dialog)
			{
				this._dialog = dialog;
			}

			// Token: 0x0600676F RID: 26479 RVA: 0x00183957 File Offset: 0x00181B57
			public int OnFileOk(FileDialogNative.IFileDialog pfd)
			{
				if (!this._dialog.HandleVistaFileOk(pfd))
				{
					return 1;
				}
				return 0;
			}

			// Token: 0x06006770 RID: 26480 RVA: 0x00011A20 File Offset: 0x0000FC20
			public int OnFolderChanging(FileDialogNative.IFileDialog pfd, FileDialogNative.IShellItem psiFolder)
			{
				return 0;
			}

			// Token: 0x06006771 RID: 26481 RVA: 0x000072B6 File Offset: 0x000054B6
			public void OnFolderChange(FileDialogNative.IFileDialog pfd)
			{
			}

			// Token: 0x06006772 RID: 26482 RVA: 0x000072B6 File Offset: 0x000054B6
			public void OnSelectionChange(FileDialogNative.IFileDialog pfd)
			{
			}

			// Token: 0x06006773 RID: 26483 RVA: 0x0018396A File Offset: 0x00181B6A
			public void OnShareViolation(FileDialogNative.IFileDialog pfd, FileDialogNative.IShellItem psi, out FileDialogNative.FDE_SHAREVIOLATION_RESPONSE pResponse)
			{
				pResponse = FileDialogNative.FDE_SHAREVIOLATION_RESPONSE.FDESVR_DEFAULT;
			}

			// Token: 0x06006774 RID: 26484 RVA: 0x000072B6 File Offset: 0x000054B6
			public void OnTypeChange(FileDialogNative.IFileDialog pfd)
			{
			}

			// Token: 0x06006775 RID: 26485 RVA: 0x0018396A File Offset: 0x00181B6A
			public void OnOverwrite(FileDialogNative.IFileDialog pfd, FileDialogNative.IShellItem psi, out FileDialogNative.FDE_OVERWRITE_RESPONSE pResponse)
			{
				pResponse = FileDialogNative.FDE_OVERWRITE_RESPONSE.FDEOR_DEFAULT;
			}

			// Token: 0x04003AA7 RID: 15015
			private FileDialog _dialog;
		}
	}
}
