using System;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using System.Text;
using Microsoft.Win32;

namespace System.Windows.Forms
{
	// Token: 0x0200029F RID: 671
	public sealed class InputLanguage
	{
		// Token: 0x06002A17 RID: 10775 RVA: 0x000BF59A File Offset: 0x000BD79A
		internal InputLanguage(IntPtr handle)
		{
			this.handle = handle;
		}

		// Token: 0x170009D5 RID: 2517
		// (get) Token: 0x06002A18 RID: 10776 RVA: 0x000BF5A9 File Offset: 0x000BD7A9
		public CultureInfo Culture
		{
			get
			{
				return new CultureInfo((int)((long)this.handle) & 65535);
			}
		}

		// Token: 0x170009D6 RID: 2518
		// (get) Token: 0x06002A19 RID: 10777 RVA: 0x000BF5C2 File Offset: 0x000BD7C2
		// (set) Token: 0x06002A1A RID: 10778 RVA: 0x000BF5D8 File Offset: 0x000BD7D8
		public static InputLanguage CurrentInputLanguage
		{
			get
			{
				Application.OleRequired();
				return new InputLanguage(SafeNativeMethods.GetKeyboardLayout(0));
			}
			set
			{
				IntSecurity.AffectThreadBehavior.Demand();
				Application.OleRequired();
				if (value == null)
				{
					value = InputLanguage.DefaultInputLanguage;
				}
				IntPtr value2 = SafeNativeMethods.ActivateKeyboardLayout(new HandleRef(value, value.handle), 0);
				if (value2 == IntPtr.Zero)
				{
					throw new ArgumentException(SR.GetString("ErrorBadInputLanguage"), "value");
				}
			}
		}

		// Token: 0x170009D7 RID: 2519
		// (get) Token: 0x06002A1B RID: 10779 RVA: 0x000BF634 File Offset: 0x000BD834
		public static InputLanguage DefaultInputLanguage
		{
			get
			{
				IntPtr[] array = new IntPtr[1];
				UnsafeNativeMethods.SystemParametersInfo(89, 0, array, 0);
				return new InputLanguage(array[0]);
			}
		}

		// Token: 0x170009D8 RID: 2520
		// (get) Token: 0x06002A1C RID: 10780 RVA: 0x000BF65B File Offset: 0x000BD85B
		public IntPtr Handle
		{
			get
			{
				return this.handle;
			}
		}

		// Token: 0x170009D9 RID: 2521
		// (get) Token: 0x06002A1D RID: 10781 RVA: 0x000BF664 File Offset: 0x000BD864
		public static InputLanguageCollection InstalledInputLanguages
		{
			get
			{
				int keyboardLayoutList = SafeNativeMethods.GetKeyboardLayoutList(0, null);
				IntPtr[] array = new IntPtr[keyboardLayoutList];
				SafeNativeMethods.GetKeyboardLayoutList(keyboardLayoutList, array);
				InputLanguage[] array2 = new InputLanguage[keyboardLayoutList];
				for (int i = 0; i < keyboardLayoutList; i++)
				{
					array2[i] = new InputLanguage(array[i]);
				}
				return new InputLanguageCollection(array2);
			}
		}

		// Token: 0x170009DA RID: 2522
		// (get) Token: 0x06002A1E RID: 10782 RVA: 0x000BF6AC File Offset: 0x000BD8AC
		public string LayoutName
		{
			get
			{
				string text = null;
				IntPtr intPtr = this.handle;
				int num = (int)((long)intPtr) & 65535;
				int num2 = (int)((long)intPtr) >> 16 & 4095;
				new RegistryPermission(PermissionState.Unrestricted).Assert();
				try
				{
					if (num2 == num || num2 == 0)
					{
						string text2 = Convert.ToString(num, 16);
						text2 = InputLanguage.PadWithZeroes(text2, 8);
						RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("SYSTEM\\CurrentControlSet\\Control\\Keyboard Layouts\\" + text2);
						text = InputLanguage.GetLocalizedKeyboardLayoutName(registryKey.GetValue("Layout Display Name") as string);
						if (text == null)
						{
							text = (string)registryKey.GetValue("Layout Text");
						}
						registryKey.Close();
					}
					else
					{
						RegistryKey registryKey2 = Registry.CurrentUser.OpenSubKey("Keyboard Layout\\Substitutes");
						string[] array = null;
						if (registryKey2 != null)
						{
							array = registryKey2.GetValueNames();
							foreach (string text3 in array)
							{
								int num3 = Convert.ToInt32(text3, 16);
								if (num3 == (int)((long)intPtr) || (num3 & 268435455) == ((int)((long)intPtr) & 268435455) || (num3 & 65535) == num)
								{
									intPtr = (IntPtr)Convert.ToInt32((string)registryKey2.GetValue(text3), 16);
									num = ((int)((long)intPtr) & 65535);
									num2 = ((int)((long)intPtr) >> 16 & 4095);
									break;
								}
							}
							registryKey2.Close();
						}
						RegistryKey registryKey3 = Registry.LocalMachine.OpenSubKey("SYSTEM\\CurrentControlSet\\Control\\Keyboard Layouts");
						if (registryKey3 != null)
						{
							array = registryKey3.GetSubKeyNames();
							foreach (string text4 in array)
							{
								if (intPtr == (IntPtr)Convert.ToInt32(text4, 16))
								{
									RegistryKey registryKey4 = registryKey3.OpenSubKey(text4);
									text = InputLanguage.GetLocalizedKeyboardLayoutName(registryKey4.GetValue("Layout Display Name") as string);
									if (text == null)
									{
										text = (string)registryKey4.GetValue("Layout Text");
									}
									registryKey4.Close();
									break;
								}
							}
						}
						if (text == null)
						{
							foreach (string text5 in array)
							{
								if (num == (65535 & Convert.ToInt32(text5.Substring(4, 4), 16)))
								{
									RegistryKey registryKey5 = registryKey3.OpenSubKey(text5);
									string text6 = (string)registryKey5.GetValue("Layout Id");
									if (text6 != null)
									{
										int num4 = Convert.ToInt32(text6, 16);
										if (num4 == num2)
										{
											text = InputLanguage.GetLocalizedKeyboardLayoutName(registryKey5.GetValue("Layout Display Name") as string);
											if (text == null)
											{
												text = (string)registryKey5.GetValue("Layout Text");
											}
										}
									}
									registryKey5.Close();
									if (text != null)
									{
										break;
									}
								}
							}
						}
						registryKey3.Close();
					}
				}
				finally
				{
					CodeAccessPermission.RevertAssert();
				}
				if (text == null)
				{
					text = SR.GetString("UnknownInputLanguageLayout");
				}
				return text;
			}
		}

		// Token: 0x06002A1F RID: 10783 RVA: 0x000BF994 File Offset: 0x000BDB94
		private static string GetLocalizedKeyboardLayoutName(string layoutDisplayName)
		{
			if (layoutDisplayName != null && Environment.OSVersion.Version.Major >= 5)
			{
				StringBuilder stringBuilder = new StringBuilder(512);
				if (UnsafeNativeMethods.SHLoadIndirectString(layoutDisplayName, stringBuilder, (uint)stringBuilder.Capacity, IntPtr.Zero) == 0U)
				{
					return stringBuilder.ToString();
				}
			}
			return null;
		}

		// Token: 0x06002A20 RID: 10784 RVA: 0x000BF9DF File Offset: 0x000BDBDF
		internal static InputLanguageChangedEventArgs CreateInputLanguageChangedEventArgs(Message m)
		{
			return new InputLanguageChangedEventArgs(new InputLanguage(m.LParam), (byte)((long)m.WParam));
		}

		// Token: 0x06002A21 RID: 10785 RVA: 0x000BFA00 File Offset: 0x000BDC00
		internal static InputLanguageChangingEventArgs CreateInputLanguageChangingEventArgs(Message m)
		{
			InputLanguage inputLanguage = new InputLanguage(m.LParam);
			bool sysCharSet = !(m.WParam == IntPtr.Zero);
			return new InputLanguageChangingEventArgs(inputLanguage, sysCharSet);
		}

		// Token: 0x06002A22 RID: 10786 RVA: 0x000BFA36 File Offset: 0x000BDC36
		public override bool Equals(object value)
		{
			return value is InputLanguage && this.handle == ((InputLanguage)value).handle;
		}

		// Token: 0x06002A23 RID: 10787 RVA: 0x000BFA58 File Offset: 0x000BDC58
		public static InputLanguage FromCulture(CultureInfo culture)
		{
			int keyboardLayoutId = culture.KeyboardLayoutId;
			foreach (object obj in InputLanguage.InstalledInputLanguages)
			{
				InputLanguage inputLanguage = (InputLanguage)obj;
				if (((int)((long)inputLanguage.handle) & 65535) == keyboardLayoutId)
				{
					return inputLanguage;
				}
			}
			return null;
		}

		// Token: 0x06002A24 RID: 10788 RVA: 0x000BFAD0 File Offset: 0x000BDCD0
		public override int GetHashCode()
		{
			return (int)((long)this.handle);
		}

		// Token: 0x06002A25 RID: 10789 RVA: 0x000BFADE File Offset: 0x000BDCDE
		private static string PadWithZeroes(string input, int length)
		{
			return "0000000000000000".Substring(0, length - input.Length) + input;
		}

		// Token: 0x04001123 RID: 4387
		private readonly IntPtr handle;
	}
}
