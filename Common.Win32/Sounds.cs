using System;
using System.IO;
using System.Runtime.InteropServices;
using ClockWorkLogger;
using Microsoft.Win32;

namespace TechnoPro.Common.Win32
{
	// Token: 0x02000008 RID: 8
	public static class Sounds
	{
		// Token: 0x06000027 RID: 39
		[DllImport("winmm.dll", CharSet = CharSet.Auto)]
		private static extern int PlaySound(string pszSound, int hmod, int falgs);

		// Token: 0x06000028 RID: 40 RVA: 0x00003390 File Offset: 0x00001590
		public static void PlayWindowsSound(string soundNameInRegistry, string defaultSoundWavFile)
		{
			try
			{
				string[] registryBreakdown = new string[]
				{
					"AppEvents",
					"Schemes",
					"Apps",
					"ClockWork",
					soundNameInRegistry,
					".current"
				};
				RegistryKey registryKey = WindowsRegistry.GetRegistryKey(Registry.CurrentUser, registryBreakdown, true, true);
				if (registryKey != null)
				{
					object value = registryKey.GetValue("");
					string text;
					if (value != null)
					{
						text = value.ToString().Trim();
					}
					else
					{
						text = "";
					}
					if (text.Length < 1)
					{
						text = defaultSoundWavFile;
					}
					if (!Path.IsPathRooted(text))
					{
						text = Path.Combine(Path.Combine(Directory.GetParent(Environment.GetFolderPath(Environment.SpecialFolder.System)).FullName, "media"), text);
					}
					Sounds.PlaySound(text);
				}
			}
			catch (Exception ex)
			{
				CWLogger.Logger.Error("Common.Win32.Sounds.PlayWindowsSound:{0}", ex.ToString());
			}
		}

		// Token: 0x06000029 RID: 41 RVA: 0x0000346C File Offset: 0x0000166C
		private static void PlaySound(string pszSound)
		{
			if (File.Exists(pszSound))
			{
				Sounds.PlaySound(pszSound, 0, 139265);
			}
		}
	}
}
