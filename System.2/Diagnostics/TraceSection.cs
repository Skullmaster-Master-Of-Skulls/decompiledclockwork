using System;
using System.Configuration;

namespace System.Diagnostics
{
	// Token: 0x020004B6 RID: 1206
	internal class TraceSection : ConfigurationElement
	{
		// Token: 0x06002D09 RID: 11529 RVA: 0x000CA428 File Offset: 0x000C8628
		static TraceSection()
		{
			TraceSection._properties = new ConfigurationPropertyCollection();
			TraceSection._properties.Add(TraceSection._propListeners);
			TraceSection._properties.Add(TraceSection._propAutoFlush);
			TraceSection._properties.Add(TraceSection._propIndentSize);
			TraceSection._properties.Add(TraceSection._propUseGlobalLock);
		}

		// Token: 0x17000AE7 RID: 2791
		// (get) Token: 0x06002D0A RID: 11530 RVA: 0x000CA4FA File Offset: 0x000C86FA
		[ConfigurationProperty("autoflush", DefaultValue = false)]
		public bool AutoFlush
		{
			get
			{
				return (bool)base[TraceSection._propAutoFlush];
			}
		}

		// Token: 0x17000AE8 RID: 2792
		// (get) Token: 0x06002D0B RID: 11531 RVA: 0x000CA50C File Offset: 0x000C870C
		[ConfigurationProperty("indentsize", DefaultValue = 4)]
		public int IndentSize
		{
			get
			{
				return (int)base[TraceSection._propIndentSize];
			}
		}

		// Token: 0x17000AE9 RID: 2793
		// (get) Token: 0x06002D0C RID: 11532 RVA: 0x000CA51E File Offset: 0x000C871E
		[ConfigurationProperty("listeners")]
		public ListenerElementsCollection Listeners
		{
			get
			{
				return (ListenerElementsCollection)base[TraceSection._propListeners];
			}
		}

		// Token: 0x17000AEA RID: 2794
		// (get) Token: 0x06002D0D RID: 11533 RVA: 0x000CA530 File Offset: 0x000C8730
		[ConfigurationProperty("useGlobalLock", DefaultValue = true)]
		public bool UseGlobalLock
		{
			get
			{
				return (bool)base[TraceSection._propUseGlobalLock];
			}
		}

		// Token: 0x17000AEB RID: 2795
		// (get) Token: 0x06002D0E RID: 11534 RVA: 0x000CA542 File Offset: 0x000C8742
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return TraceSection._properties;
			}
		}

		// Token: 0x04002702 RID: 9986
		private static readonly ConfigurationPropertyCollection _properties;

		// Token: 0x04002703 RID: 9987
		private static readonly ConfigurationProperty _propListeners = new ConfigurationProperty("listeners", typeof(ListenerElementsCollection), new ListenerElementsCollection(), ConfigurationPropertyOptions.None);

		// Token: 0x04002704 RID: 9988
		private static readonly ConfigurationProperty _propAutoFlush = new ConfigurationProperty("autoflush", typeof(bool), false, ConfigurationPropertyOptions.None);

		// Token: 0x04002705 RID: 9989
		private static readonly ConfigurationProperty _propIndentSize = new ConfigurationProperty("indentsize", typeof(int), 4, ConfigurationPropertyOptions.None);

		// Token: 0x04002706 RID: 9990
		private static readonly ConfigurationProperty _propUseGlobalLock = new ConfigurationProperty("useGlobalLock", typeof(bool), true, ConfigurationPropertyOptions.None);
	}
}
