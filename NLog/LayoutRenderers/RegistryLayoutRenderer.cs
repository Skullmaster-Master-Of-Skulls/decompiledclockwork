using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Text;
using Microsoft.Win32;
using NLog.Common;
using NLog.Config;
using NLog.Internal;
using NLog.Layouts;

namespace NLog.LayoutRenderers
{
	// Token: 0x020000EB RID: 235
	[LayoutRenderer("registry")]
	public class RegistryLayoutRenderer : LayoutRenderer
	{
		// Token: 0x060006BC RID: 1724 RVA: 0x0000F1C2 File Offset: 0x0000D3C2
		public RegistryLayoutRenderer()
		{
			this.RequireEscapingSlashesInDefaultValue = true;
		}

		// Token: 0x1700011B RID: 283
		// (get) Token: 0x060006BD RID: 1725 RVA: 0x0000F1D1 File Offset: 0x0000D3D1
		// (set) Token: 0x060006BE RID: 1726 RVA: 0x0000F1D9 File Offset: 0x0000D3D9
		public Layout Value { get; set; }

		// Token: 0x1700011C RID: 284
		// (get) Token: 0x060006BF RID: 1727 RVA: 0x0000F1E2 File Offset: 0x0000D3E2
		// (set) Token: 0x060006C0 RID: 1728 RVA: 0x0000F1EA File Offset: 0x0000D3EA
		public Layout DefaultValue { get; set; }

		// Token: 0x1700011D RID: 285
		// (get) Token: 0x060006C1 RID: 1729 RVA: 0x0000F1F3 File Offset: 0x0000D3F3
		// (set) Token: 0x060006C2 RID: 1730 RVA: 0x0000F1FB File Offset: 0x0000D3FB
		[DefaultValue(true)]
		public bool RequireEscapingSlashesInDefaultValue { get; set; }

		// Token: 0x1700011E RID: 286
		// (get) Token: 0x060006C3 RID: 1731 RVA: 0x0000F204 File Offset: 0x0000D404
		// (set) Token: 0x060006C4 RID: 1732 RVA: 0x0000F20C File Offset: 0x0000D40C
		[DefaultValue("Default")]
		public RegistryView View { get; set; }

		// Token: 0x1700011F RID: 287
		// (get) Token: 0x060006C5 RID: 1733 RVA: 0x0000F215 File Offset: 0x0000D415
		// (set) Token: 0x060006C6 RID: 1734 RVA: 0x0000F21D File Offset: 0x0000D41D
		[RequiredParameter]
		public Layout Key { get; set; }

		// Token: 0x060006C7 RID: 1735 RVA: 0x0000F228 File Offset: 0x0000D428
		protected override void Append(StringBuilder builder, LogEventInfo logEvent)
		{
			object obj = null;
			string name = (this.Value != null) ? this.Value.Render(logEvent) : null;
			RegistryLayoutRenderer.ParseResult parseResult = RegistryLayoutRenderer.ParseKey(this.Key.Render(logEvent));
			try
			{
				using (RegistryKey registryKey = RegistryKey.OpenBaseKey(parseResult.Hive, this.View))
				{
					if (parseResult.HasSubKey)
					{
						using (RegistryKey registryKey2 = registryKey.OpenSubKey(parseResult.SubKey))
						{
							if (registryKey2 != null)
							{
								obj = registryKey2.GetValue(name);
							}
							goto IL_77;
						}
					}
					obj = registryKey.GetValue(name);
					IL_77:;
				}
			}
			catch (Exception exception)
			{
				InternalLogger.Error("Error when writing to registry");
				if (exception.MustBeRethrown())
				{
					throw;
				}
			}
			string text = null;
			if (obj != null)
			{
				text = Convert.ToString(obj, CultureInfo.InvariantCulture);
			}
			else if (this.DefaultValue != null)
			{
				text = this.DefaultValue.Render(logEvent);
				if (this.RequireEscapingSlashesInDefaultValue)
				{
					text = text.Replace("\\\\", "\\");
				}
			}
			builder.Append(text);
		}

		// Token: 0x060006C8 RID: 1736 RVA: 0x0000F34C File Offset: 0x0000D54C
		private static RegistryLayoutRenderer.ParseResult ParseKey(string key)
		{
			int num = key.IndexOfAny(new char[]
			{
				'\\',
				'/'
			});
			string text = null;
			string hiveName;
			if (num >= 0)
			{
				hiveName = key.Substring(0, num);
				text = key.Substring(num + 1).Replace('/', '\\');
				text = text.TrimStart(new char[]
				{
					'\\'
				});
				text = text.Replace("\\\\", "\\");
			}
			else
			{
				hiveName = key;
			}
			RegistryHive hive = RegistryLayoutRenderer.ParseHiveName(hiveName);
			return new RegistryLayoutRenderer.ParseResult
			{
				SubKey = text,
				Hive = hive
			};
		}

		// Token: 0x060006C9 RID: 1737 RVA: 0x0000F3E4 File Offset: 0x0000D5E4
		private static RegistryHive ParseHiveName(string hiveName)
		{
			RegistryHive result;
			if (RegistryLayoutRenderer.HiveAliases.TryGetValue(hiveName, out result))
			{
				return result;
			}
			throw new ArgumentException(string.Format("Key name is not supported. Root hive '{0}' not recognized.", hiveName));
		}

		// Token: 0x040001E5 RID: 485
		private static readonly Dictionary<string, RegistryHive> HiveAliases = new Dictionary<string, RegistryHive>(StringComparer.InvariantCultureIgnoreCase)
		{
			{
				"HKEY_LOCAL_MACHINE",
				RegistryHive.LocalMachine
			},
			{
				"HKLM",
				RegistryHive.LocalMachine
			},
			{
				"HKEY_CURRENT_USER",
				RegistryHive.CurrentUser
			},
			{
				"HKCU",
				RegistryHive.CurrentUser
			},
			{
				"HKEY_CLASSES_ROOT",
				RegistryHive.ClassesRoot
			},
			{
				"HKEY_USERS",
				RegistryHive.Users
			},
			{
				"HKEY_CURRENT_CONFIG",
				RegistryHive.CurrentConfig
			},
			{
				"HKEY_DYN_DATA",
				RegistryHive.DynData
			},
			{
				"HKEY_PERFORMANCE_DATA",
				RegistryHive.PerformanceData
			}
		};

		// Token: 0x020000EC RID: 236
		private class ParseResult
		{
			// Token: 0x17000120 RID: 288
			// (get) Token: 0x060006CB RID: 1739 RVA: 0x0000F4C2 File Offset: 0x0000D6C2
			// (set) Token: 0x060006CC RID: 1740 RVA: 0x0000F4CA File Offset: 0x0000D6CA
			public string SubKey { get; set; }

			// Token: 0x17000121 RID: 289
			// (get) Token: 0x060006CD RID: 1741 RVA: 0x0000F4D3 File Offset: 0x0000D6D3
			// (set) Token: 0x060006CE RID: 1742 RVA: 0x0000F4DB File Offset: 0x0000D6DB
			public RegistryHive Hive { get; set; }

			// Token: 0x17000122 RID: 290
			// (get) Token: 0x060006CF RID: 1743 RVA: 0x0000F4E4 File Offset: 0x0000D6E4
			public bool HasSubKey
			{
				get
				{
					return !string.IsNullOrEmpty(this.SubKey);
				}
			}
		}
	}
}
