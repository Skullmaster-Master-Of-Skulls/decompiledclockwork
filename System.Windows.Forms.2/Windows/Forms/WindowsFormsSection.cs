using System;
using System.Configuration;

namespace System.Windows.Forms
{
	// Token: 0x02000443 RID: 1091
	public sealed class WindowsFormsSection : ConfigurationSection
	{
		// Token: 0x06004BB9 RID: 19385 RVA: 0x0013B010 File Offset: 0x00139210
		internal static WindowsFormsSection GetSection()
		{
			WindowsFormsSection result = null;
			try
			{
				result = (WindowsFormsSection)PrivilegedConfigurationManager.GetSection("system.windows.forms");
			}
			catch
			{
				result = new WindowsFormsSection();
			}
			return result;
		}

		// Token: 0x06004BBA RID: 19386 RVA: 0x0013B04C File Offset: 0x0013924C
		private static ConfigurationPropertyCollection EnsureStaticPropertyBag()
		{
			if (WindowsFormsSection.s_properties == null)
			{
				WindowsFormsSection.s_propJitDebugging = new ConfigurationProperty("jitDebugging", typeof(bool), false, ConfigurationPropertyOptions.None);
				WindowsFormsSection.s_properties = new ConfigurationPropertyCollection
				{
					WindowsFormsSection.s_propJitDebugging
				};
			}
			return WindowsFormsSection.s_properties;
		}

		// Token: 0x06004BBB RID: 19387 RVA: 0x0013B09C File Offset: 0x0013929C
		public WindowsFormsSection()
		{
			WindowsFormsSection.EnsureStaticPropertyBag();
		}

		// Token: 0x1700126E RID: 4718
		// (get) Token: 0x06004BBC RID: 19388 RVA: 0x0013B0AA File Offset: 0x001392AA
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return WindowsFormsSection.EnsureStaticPropertyBag();
			}
		}

		// Token: 0x1700126F RID: 4719
		// (get) Token: 0x06004BBD RID: 19389 RVA: 0x0013B0B1 File Offset: 0x001392B1
		// (set) Token: 0x06004BBE RID: 19390 RVA: 0x0013B0C3 File Offset: 0x001392C3
		[ConfigurationProperty("jitDebugging", DefaultValue = false)]
		public bool JitDebugging
		{
			get
			{
				return (bool)base[WindowsFormsSection.s_propJitDebugging];
			}
			set
			{
				base[WindowsFormsSection.s_propJitDebugging] = value;
			}
		}

		// Token: 0x0400283F RID: 10303
		internal const bool JitDebuggingDefault = false;

		// Token: 0x04002840 RID: 10304
		private static ConfigurationPropertyCollection s_properties;

		// Token: 0x04002841 RID: 10305
		private static ConfigurationProperty s_propJitDebugging;
	}
}
