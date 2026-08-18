using System;
using System.Linq;
using Microsoft.Win32;
using TechnoPro.Common.ICore;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Win32;

namespace TechnoPro.Common.Core
{
	// Token: 0x02000023 RID: 35
	[Obsolete("Deprecated, please use Common.Win32.RegistryHelper class instead")]
	public class RegistryManager : IRegistryManager, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x17000045 RID: 69
		// (get) Token: 0x0600011A RID: 282 RVA: 0x000067B8 File Offset: 0x000049B8
		public RegistryKey StartLocalMachineRegistryKey
		{
			get
			{
				RegistryKey registryKey = Registry.LocalMachine;
				string[] array = this._startSubKeyPath ?? RegistryManager.TechnoproSubKeyPath;
				foreach (string subkey in array)
				{
					bool flag = registryKey != null;
					if (flag)
					{
						registryKey = registryKey.CreateSubKey(subkey);
					}
				}
				return registryKey;
			}
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x0600011B RID: 283 RVA: 0x00006810 File Offset: 0x00004A10
		public RegistryKey StartCurrentUserRegistryKey
		{
			get
			{
				RegistryKey registryKey = Registry.CurrentUser;
				string[] array = this._startSubKeyPath ?? RegistryManager.TechnoproSubKeyPath;
				foreach (string subkey in array)
				{
					bool flag = registryKey != null;
					if (flag)
					{
						registryKey = registryKey.CreateSubKey(subkey);
					}
				}
				return registryKey;
			}
		}

		// Token: 0x0600011C RID: 284 RVA: 0x00006868 File Offset: 0x00004A68
		public RegistryKey GetStartLocalMachineRegistryKey(eRegWow64Options wow64Options)
		{
			RegistryHelper registryHelper = new RegistryHelper();
			return registryHelper.GetStartLocalMachineRegistryKey(wow64Options, false);
		}

		// Token: 0x0600011D RID: 285 RVA: 0x00006888 File Offset: 0x00004A88
		public RegistryKey GetStartCurrentUserRegistryKey(eRegWow64Options wow64Options)
		{
			RegistryHelper registryHelper = new RegistryHelper();
			return registryHelper.GetStartCurrentUserRegistryKey(wow64Options, false);
		}

		// Token: 0x0600011E RID: 286 RVA: 0x0000672B File Offset: 0x0000492B
		public RegistryManager()
		{
		}

		// Token: 0x0600011F RID: 287 RVA: 0x000068A8 File Offset: 0x00004AA8
		public RegistryManager(OperationContext opContext)
		{
			this.OpContext = opContext;
		}

		// Token: 0x06000120 RID: 288 RVA: 0x000068BA File Offset: 0x00004ABA
		public RegistryManager(params string[] subKeyPath)
		{
			this._startSubKeyPath = subKeyPath;
		}

		// Token: 0x06000121 RID: 289 RVA: 0x000068CB File Offset: 0x00004ACB
		public RegistryManager(OperationContext opContext, params string[] subKeyPath)
		{
			this.OpContext = opContext;
			this._startSubKeyPath = subKeyPath;
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x06000122 RID: 290 RVA: 0x000068E4 File Offset: 0x00004AE4
		// (set) Token: 0x06000123 RID: 291 RVA: 0x000068EC File Offset: 0x00004AEC
		public OperationContext OpContext { get; set; }

		// Token: 0x06000124 RID: 292 RVA: 0x000068F8 File Offset: 0x00004AF8
		public T ReadLocalMachineRegistry<T>(params string[] keypath)
		{
			RegistryKey startLocalMachineRegistryKey = this.StartLocalMachineRegistryKey;
			return this.ReadRegistry<T>(startLocalMachineRegistryKey, keypath);
		}

		// Token: 0x06000125 RID: 293 RVA: 0x0000691C File Offset: 0x00004B1C
		public void WriteLocalMachineRegistry<T>(T value, params string[] keypath)
		{
			RegistryKey startLocalMachineRegistryKey = this.StartLocalMachineRegistryKey;
			this.WriteRegistry<T>(startLocalMachineRegistryKey, value, keypath);
		}

		// Token: 0x06000126 RID: 294 RVA: 0x0000693C File Offset: 0x00004B3C
		public T ReadLocalMachineRegistry<T>(eRegWow64Options wow64Options, params string[] keypath)
		{
			RegistryHelper registryHelper = new RegistryHelper();
			return registryHelper.ReadLocalMachineRegistry<T>(wow64Options, keypath);
		}

		// Token: 0x06000127 RID: 295 RVA: 0x0000695C File Offset: 0x00004B5C
		public void WriteLocalMachineRegistry<T>(T value, eRegWow64Options wow64Options, params string[] keypath)
		{
			RegistryHelper registryHelper = new RegistryHelper();
			registryHelper.WriteLocalMachineRegistry<T>(wow64Options, value, keypath);
		}

		// Token: 0x06000128 RID: 296 RVA: 0x0000697C File Offset: 0x00004B7C
		public T ReadCurrentUserRegistry<T>(params string[] keypath)
		{
			RegistryKey startCurrentUserRegistryKey = this.StartCurrentUserRegistryKey;
			return this.ReadRegistry<T>(startCurrentUserRegistryKey, keypath);
		}

		// Token: 0x06000129 RID: 297 RVA: 0x000069A0 File Offset: 0x00004BA0
		public void WriteCurrentUserRegistry<T>(T value, params string[] keypath)
		{
			RegistryKey startCurrentUserRegistryKey = this.StartCurrentUserRegistryKey;
			this.WriteRegistry<T>(startCurrentUserRegistryKey, value, keypath);
		}

		// Token: 0x0600012A RID: 298 RVA: 0x000069C0 File Offset: 0x00004BC0
		public T ReadCurrentUserRegistry<T>(eRegWow64Options wow64Options, params string[] keypath)
		{
			RegistryHelper registryHelper = new RegistryHelper();
			return registryHelper.ReadCurrentUserRegistry<T>(wow64Options, keypath);
		}

		// Token: 0x0600012B RID: 299 RVA: 0x000069E0 File Offset: 0x00004BE0
		public void WriteCurrentUserRegistry<T>(T value, eRegWow64Options wow64Options, params string[] keypath)
		{
			RegistryHelper registryHelper = new RegistryHelper();
			registryHelper.WriteCurrentUserRegistry<T>(wow64Options, value, keypath);
		}

		// Token: 0x0600012C RID: 300 RVA: 0x00006A00 File Offset: 0x00004C00
		public string[] GetLocalMachineSubKeyNames(params string[] keypath)
		{
			RegistryKey startLocalMachineRegistryKey = this.StartLocalMachineRegistryKey;
			return this.GetSubKeyNames(startLocalMachineRegistryKey, keypath);
		}

		// Token: 0x0600012D RID: 301 RVA: 0x00006A24 File Offset: 0x00004C24
		public string[] GetCurrentUserSubKeyNames(params string[] keypath)
		{
			RegistryKey startCurrentUserRegistryKey = this.StartCurrentUserRegistryKey;
			return this.GetSubKeyNames(startCurrentUserRegistryKey, keypath);
		}

		// Token: 0x0600012E RID: 302 RVA: 0x00006A48 File Offset: 0x00004C48
		public string[] GetLocalMachineSubKeyNames(eRegWow64Options wow64Options, params string[] keypath)
		{
			RegistryHelper registryHelper = new RegistryHelper();
			return registryHelper.GetLocalMachineSubKeyNames(wow64Options, keypath);
		}

		// Token: 0x0600012F RID: 303 RVA: 0x00006A68 File Offset: 0x00004C68
		public string[] GetCurrentUserSubKeyNames(eRegWow64Options wow64Options, params string[] keypath)
		{
			RegistryHelper registryHelper = new RegistryHelper();
			return registryHelper.GetCurrentUserSubKeyNames(wow64Options, keypath);
		}

		// Token: 0x06000130 RID: 304 RVA: 0x00006A88 File Offset: 0x00004C88
		private T ReadRegistry<T>(RegistryKey regKey, params string[] keypath)
		{
			for (int i = 0; i < keypath.Length - 1; i++)
			{
				regKey = regKey.OpenSubKey(keypath[i]);
				bool flag = regKey == null;
				if (flag)
				{
					return default(T);
				}
			}
			string[] valueNames = regKey.GetValueNames();
			return valueNames.Contains(keypath.Last<string>()) ? ((T)((object)regKey.GetValue(keypath.Last<string>()))) : default(T);
		}

		// Token: 0x06000131 RID: 305 RVA: 0x00006B08 File Offset: 0x00004D08
		private void WriteRegistry<T>(RegistryKey regKey, T value, params string[] keypath)
		{
			for (int i = 0; i < keypath.Length - 1; i++)
			{
				regKey = regKey.CreateSubKey(keypath[i]);
			}
			regKey.SetValue(keypath.Last<string>(), value);
		}

		// Token: 0x06000132 RID: 306 RVA: 0x00006B4C File Offset: 0x00004D4C
		private string[] GetSubKeyNames(RegistryKey regKey, params string[] keypath)
		{
			foreach (string name in keypath)
			{
				regKey = regKey.OpenSubKey(name);
				bool flag = regKey == null;
				if (flag)
				{
					return null;
				}
			}
			return regKey.GetSubKeyNames();
		}

		// Token: 0x04000047 RID: 71
		public static string[] TechnoproSubKeyPath = new string[]
		{
			"Software",
			"TechnoPro"
		};

		// Token: 0x04000048 RID: 72
		private readonly string[] _startSubKeyPath;
	}
}
