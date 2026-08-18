using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using NLog.Common;
using NLog.Config;
using NLog.Internal;
using NLog.LayoutRenderers;

namespace NLog.Layouts
{
	// Token: 0x0200011B RID: 283
	[ThreadAgnostic]
	[AppDomainFixedOutput]
	[Layout("SimpleLayout")]
	public class SimpleLayout : Layout, IUsesStackTrace
	{
		// Token: 0x060007D3 RID: 2003 RVA: 0x00011924 File Offset: 0x0000FB24
		public SimpleLayout() : this(string.Empty)
		{
		}

		// Token: 0x060007D4 RID: 2004 RVA: 0x00011931 File Offset: 0x0000FB31
		public SimpleLayout(string txt) : this(txt, ConfigurationItemFactory.Default)
		{
		}

		// Token: 0x060007D5 RID: 2005 RVA: 0x0001193F File Offset: 0x0000FB3F
		public SimpleLayout(string txt, ConfigurationItemFactory configurationItemFactory)
		{
			this.configurationItemFactory = configurationItemFactory;
			this.Text = txt;
		}

		// Token: 0x060007D6 RID: 2006 RVA: 0x00011955 File Offset: 0x0000FB55
		internal SimpleLayout(LayoutRenderer[] renderers, string text, ConfigurationItemFactory configurationItemFactory)
		{
			this.configurationItemFactory = configurationItemFactory;
			this.SetRenderers(renderers, text);
		}

		// Token: 0x17000164 RID: 356
		// (get) Token: 0x060007D7 RID: 2007 RVA: 0x0001196C File Offset: 0x0000FB6C
		// (set) Token: 0x060007D8 RID: 2008 RVA: 0x00011974 File Offset: 0x0000FB74
		public string OriginalText { get; private set; }

		// Token: 0x17000165 RID: 357
		// (get) Token: 0x060007D9 RID: 2009 RVA: 0x0001197D File Offset: 0x0000FB7D
		// (set) Token: 0x060007DA RID: 2010 RVA: 0x00011988 File Offset: 0x0000FB88
		public string Text
		{
			get
			{
				return this.layoutText;
			}
			set
			{
				this.OriginalText = value;
				string text;
				LayoutRenderer[] renderers = LayoutParser.CompileLayout(this.configurationItemFactory, new SimpleStringReader(value), false, out text);
				this.SetRenderers(renderers, text);
			}
		}

		// Token: 0x17000166 RID: 358
		// (get) Token: 0x060007DB RID: 2011 RVA: 0x000119B9 File Offset: 0x0000FBB9
		public bool IsFixedText
		{
			get
			{
				return this.fixedText != null;
			}
		}

		// Token: 0x17000167 RID: 359
		// (get) Token: 0x060007DC RID: 2012 RVA: 0x000119C7 File Offset: 0x0000FBC7
		public string FixedText
		{
			get
			{
				return this.fixedText;
			}
		}

		// Token: 0x17000168 RID: 360
		// (get) Token: 0x060007DD RID: 2013 RVA: 0x000119CF File Offset: 0x0000FBCF
		// (set) Token: 0x060007DE RID: 2014 RVA: 0x000119D7 File Offset: 0x0000FBD7
		public ReadOnlyCollection<LayoutRenderer> Renderers { get; private set; }

		// Token: 0x17000169 RID: 361
		// (get) Token: 0x060007DF RID: 2015 RVA: 0x000119E0 File Offset: 0x0000FBE0
		// (set) Token: 0x060007E0 RID: 2016 RVA: 0x000119E8 File Offset: 0x0000FBE8
		public StackTraceUsage StackTraceUsage { get; private set; }

		// Token: 0x060007E1 RID: 2017 RVA: 0x000119F1 File Offset: 0x0000FBF1
		public new static implicit operator SimpleLayout(string text)
		{
			return new SimpleLayout(text);
		}

		// Token: 0x060007E2 RID: 2018 RVA: 0x000119F9 File Offset: 0x0000FBF9
		public static string Escape(string text)
		{
			return text.Replace("${", "${literal:text=${}");
		}

		// Token: 0x060007E3 RID: 2019 RVA: 0x00011A0C File Offset: 0x0000FC0C
		public static string Evaluate(string text, LogEventInfo logEvent)
		{
			SimpleLayout simpleLayout = new SimpleLayout(text);
			return simpleLayout.Render(logEvent);
		}

		// Token: 0x060007E4 RID: 2020 RVA: 0x00011A27 File Offset: 0x0000FC27
		public static string Evaluate(string text)
		{
			return SimpleLayout.Evaluate(text, LogEventInfo.CreateNullEvent());
		}

		// Token: 0x060007E5 RID: 2021 RVA: 0x00011A34 File Offset: 0x0000FC34
		public override string ToString()
		{
			return "'" + this.Text + "'";
		}

		// Token: 0x060007E6 RID: 2022 RVA: 0x00011A58 File Offset: 0x0000FC58
		internal void SetRenderers(LayoutRenderer[] renderers, string text)
		{
			this.Renderers = new ReadOnlyCollection<LayoutRenderer>(renderers);
			if (this.Renderers.Count == 0)
			{
				this.fixedText = null;
				this.StackTraceUsage = StackTraceUsage.None;
			}
			else if (this.Renderers.Count == 1 && this.Renderers[0] is LiteralLayoutRenderer)
			{
				this.fixedText = ((LiteralLayoutRenderer)this.Renderers[0]).Text;
				this.StackTraceUsage = StackTraceUsage.None;
			}
			else
			{
				this.fixedText = null;
				this.StackTraceUsage = this.Renderers.OfType<IUsesStackTrace>().DefaultIfEmpty<IUsesStackTrace>().Max(delegate(IUsesStackTrace usage)
				{
					if (usage != null)
					{
						return usage.StackTraceUsage;
					}
					return StackTraceUsage.None;
				});
			}
			this.layoutText = text;
		}

		// Token: 0x060007E7 RID: 2023 RVA: 0x00011B20 File Offset: 0x0000FD20
		protected override void InitializeLayout()
		{
			for (int i = 0; i < this.Renderers.Count; i++)
			{
				LayoutRenderer layoutRenderer = this.Renderers[i];
				try
				{
					layoutRenderer.Initialize(base.LoggingConfiguration);
				}
				catch (Exception ex)
				{
					if (InternalLogger.IsWarnEnabled || InternalLogger.IsErrorEnabled)
					{
						InternalLogger.Warn(ex, "Exception in '{0}.InitializeLayout()'", new object[]
						{
							layoutRenderer.GetType().FullName
						});
					}
					if (ex.MustBeRethrown())
					{
						throw;
					}
				}
			}
			base.InitializeLayout();
		}

		// Token: 0x060007E8 RID: 2024 RVA: 0x00011BB0 File Offset: 0x0000FDB0
		protected override string GetFormattedMessage(LogEventInfo logEvent)
		{
			if (this.IsFixedText)
			{
				return this.fixedText;
			}
			string result;
			if (logEvent.TryGetCachedLayoutValue(this, out result))
			{
				return result;
			}
			int num = this.maxRenderedLength;
			if (num > 16384)
			{
				num = 16384;
			}
			StringBuilder stringBuilder = new StringBuilder(num);
			for (int i = 0; i < this.Renderers.Count; i++)
			{
				LayoutRenderer layoutRenderer = this.Renderers[i];
				try
				{
					layoutRenderer.Render(stringBuilder, logEvent);
				}
				catch (Exception ex)
				{
					if (InternalLogger.IsWarnEnabled || InternalLogger.IsErrorEnabled)
					{
						InternalLogger.Warn(ex, "Exception in '{0}.Append()'", new object[]
						{
							layoutRenderer.GetType().FullName
						});
					}
					if (ex.MustBeRethrown())
					{
						throw;
					}
				}
			}
			if (stringBuilder.Length > this.maxRenderedLength)
			{
				this.maxRenderedLength = stringBuilder.Length;
			}
			string text = stringBuilder.ToString();
			logEvent.AddCachedLayoutValue(this, text);
			return text;
		}

		// Token: 0x04000253 RID: 595
		private const int MaxInitialRenderBufferLength = 16384;

		// Token: 0x04000254 RID: 596
		private int maxRenderedLength;

		// Token: 0x04000255 RID: 597
		private string fixedText;

		// Token: 0x04000256 RID: 598
		private string layoutText;

		// Token: 0x04000257 RID: 599
		private ConfigurationItemFactory configurationItemFactory;
	}
}
