using System;
using System.ComponentModel;
using NLog.Config;
using NLog.Internal;

namespace NLog.Layouts
{
	// Token: 0x02000110 RID: 272
	[NLogConfigurationItem]
	public abstract class Layout : ISupportsInitialize, IRenderable
	{
		// Token: 0x17000151 RID: 337
		// (get) Token: 0x06000781 RID: 1921 RVA: 0x000107B5 File Offset: 0x0000E9B5
		internal bool IsThreadAgnostic
		{
			get
			{
				return this.threadAgnostic;
			}
		}

		// Token: 0x17000152 RID: 338
		// (get) Token: 0x06000782 RID: 1922 RVA: 0x000107BD File Offset: 0x0000E9BD
		// (set) Token: 0x06000783 RID: 1923 RVA: 0x000107C5 File Offset: 0x0000E9C5
		private protected LoggingConfiguration LoggingConfiguration { protected get; private set; }

		// Token: 0x06000784 RID: 1924 RVA: 0x000107CE File Offset: 0x0000E9CE
		public static implicit operator Layout([Localizable(false)] string text)
		{
			return Layout.FromString(text);
		}

		// Token: 0x06000785 RID: 1925 RVA: 0x000107D6 File Offset: 0x0000E9D6
		public static Layout FromString(string layoutText)
		{
			return Layout.FromString(layoutText, ConfigurationItemFactory.Default);
		}

		// Token: 0x06000786 RID: 1926 RVA: 0x000107E3 File Offset: 0x0000E9E3
		public static Layout FromString(string layoutText, ConfigurationItemFactory configurationItemFactory)
		{
			return new SimpleLayout(layoutText, configurationItemFactory);
		}

		// Token: 0x06000787 RID: 1927 RVA: 0x000107EC File Offset: 0x0000E9EC
		public virtual void Precalculate(LogEventInfo logEvent)
		{
			if (!this.threadAgnostic)
			{
				this.Render(logEvent);
			}
		}

		// Token: 0x06000788 RID: 1928 RVA: 0x000107FE File Offset: 0x0000E9FE
		public string Render(LogEventInfo logEvent)
		{
			if (!this.isInitialized)
			{
				this.isInitialized = true;
				this.InitializeLayout();
			}
			return this.GetFormattedMessage(logEvent);
		}

		// Token: 0x06000789 RID: 1929 RVA: 0x0001081C File Offset: 0x0000EA1C
		void ISupportsInitialize.Initialize(LoggingConfiguration configuration)
		{
			this.Initialize(configuration);
		}

		// Token: 0x0600078A RID: 1930 RVA: 0x00010825 File Offset: 0x0000EA25
		void ISupportsInitialize.Close()
		{
			this.Close();
		}

		// Token: 0x0600078B RID: 1931 RVA: 0x00010830 File Offset: 0x0000EA30
		internal void Initialize(LoggingConfiguration configuration)
		{
			if (!this.isInitialized)
			{
				this.LoggingConfiguration = configuration;
				this.isInitialized = true;
				this.threadAgnostic = true;
				foreach (object obj in ObjectGraphScanner.FindReachableObjects<object>(new object[]
				{
					this
				}))
				{
					if (!obj.GetType().IsDefined(typeof(ThreadAgnosticAttribute), true))
					{
						this.threadAgnostic = false;
						break;
					}
				}
				this.InitializeLayout();
			}
		}

		// Token: 0x0600078C RID: 1932 RVA: 0x000108CC File Offset: 0x0000EACC
		internal void Close()
		{
			if (this.isInitialized)
			{
				this.LoggingConfiguration = null;
				this.isInitialized = false;
				this.CloseLayout();
			}
		}

		// Token: 0x0600078D RID: 1933 RVA: 0x000108EA File Offset: 0x0000EAEA
		protected virtual void InitializeLayout()
		{
		}

		// Token: 0x0600078E RID: 1934 RVA: 0x000108EC File Offset: 0x0000EAEC
		protected virtual void CloseLayout()
		{
		}

		// Token: 0x0600078F RID: 1935
		protected abstract string GetFormattedMessage(LogEventInfo logEvent);

		// Token: 0x04000237 RID: 567
		private bool isInitialized;

		// Token: 0x04000238 RID: 568
		private bool threadAgnostic;
	}
}
