using System;
using System.Configuration;

namespace System.Diagnostics
{
	// Token: 0x02000490 RID: 1168
	internal class AssertSection : ConfigurationElement
	{
		// Token: 0x06002B55 RID: 11093 RVA: 0x000C4F50 File Offset: 0x000C3150
		static AssertSection()
		{
			AssertSection._properties = new ConfigurationPropertyCollection();
			AssertSection._properties.Add(AssertSection._propAssertUIEnabled);
			AssertSection._properties.Add(AssertSection._propLogFile);
		}

		// Token: 0x17000A7A RID: 2682
		// (get) Token: 0x06002B56 RID: 11094 RVA: 0x000C4FC4 File Offset: 0x000C31C4
		[ConfigurationProperty("assertuienabled", DefaultValue = true)]
		public bool AssertUIEnabled
		{
			get
			{
				return (bool)base[AssertSection._propAssertUIEnabled];
			}
		}

		// Token: 0x17000A7B RID: 2683
		// (get) Token: 0x06002B57 RID: 11095 RVA: 0x000C4FD6 File Offset: 0x000C31D6
		[ConfigurationProperty("logfilename", DefaultValue = "")]
		public string LogFileName
		{
			get
			{
				return (string)base[AssertSection._propLogFile];
			}
		}

		// Token: 0x17000A7C RID: 2684
		// (get) Token: 0x06002B58 RID: 11096 RVA: 0x000C4FE8 File Offset: 0x000C31E8
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return AssertSection._properties;
			}
		}

		// Token: 0x04002682 RID: 9858
		private static readonly ConfigurationPropertyCollection _properties;

		// Token: 0x04002683 RID: 9859
		private static readonly ConfigurationProperty _propAssertUIEnabled = new ConfigurationProperty("assertuienabled", typeof(bool), true, ConfigurationPropertyOptions.None);

		// Token: 0x04002684 RID: 9860
		private static readonly ConfigurationProperty _propLogFile = new ConfigurationProperty("logfilename", typeof(string), string.Empty, ConfigurationPropertyOptions.None);
	}
}
