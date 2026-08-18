using System;
using System.ComponentModel;
using NLog.Config;

namespace NLog.LayoutRenderers.Wrappers
{
	// Token: 0x020000FB RID: 251
	[AmbientProperty("ClearCache")]
	[AmbientProperty("Cached")]
	[LayoutRenderer("cached")]
	[ThreadAgnostic]
	public sealed class CachedLayoutRendererWrapper : WrapperLayoutRendererBase
	{
		// Token: 0x06000714 RID: 1812 RVA: 0x0000FCF9 File Offset: 0x0000DEF9
		public CachedLayoutRendererWrapper()
		{
			this.Cached = true;
			this.ClearCache = (CachedLayoutRendererWrapper.ClearCacheOption.OnInit | CachedLayoutRendererWrapper.ClearCacheOption.OnClose);
		}

		// Token: 0x17000134 RID: 308
		// (get) Token: 0x06000715 RID: 1813 RVA: 0x0000FD0F File Offset: 0x0000DF0F
		// (set) Token: 0x06000716 RID: 1814 RVA: 0x0000FD17 File Offset: 0x0000DF17
		[DefaultValue(true)]
		public bool Cached { get; set; }

		// Token: 0x17000135 RID: 309
		// (get) Token: 0x06000717 RID: 1815 RVA: 0x0000FD20 File Offset: 0x0000DF20
		// (set) Token: 0x06000718 RID: 1816 RVA: 0x0000FD28 File Offset: 0x0000DF28
		public CachedLayoutRendererWrapper.ClearCacheOption ClearCache { get; set; }

		// Token: 0x06000719 RID: 1817 RVA: 0x0000FD31 File Offset: 0x0000DF31
		protected override void InitializeLayoutRenderer()
		{
			base.InitializeLayoutRenderer();
			if ((this.ClearCache & CachedLayoutRendererWrapper.ClearCacheOption.OnInit) == CachedLayoutRendererWrapper.ClearCacheOption.OnInit)
			{
				this.cachedValue = null;
			}
		}

		// Token: 0x0600071A RID: 1818 RVA: 0x0000FD4B File Offset: 0x0000DF4B
		protected override void CloseLayoutRenderer()
		{
			base.CloseLayoutRenderer();
			if ((this.ClearCache & CachedLayoutRendererWrapper.ClearCacheOption.OnClose) == CachedLayoutRendererWrapper.ClearCacheOption.OnClose)
			{
				this.cachedValue = null;
			}
		}

		// Token: 0x0600071B RID: 1819 RVA: 0x0000FD65 File Offset: 0x0000DF65
		protected override string Transform(string text)
		{
			return text;
		}

		// Token: 0x0600071C RID: 1820 RVA: 0x0000FD68 File Offset: 0x0000DF68
		protected override string RenderInner(LogEventInfo logEvent)
		{
			if (this.Cached)
			{
				if (this.cachedValue == null)
				{
					this.cachedValue = base.RenderInner(logEvent);
				}
				return this.cachedValue;
			}
			return base.RenderInner(logEvent);
		}

		// Token: 0x04000206 RID: 518
		private string cachedValue;

		// Token: 0x020000FC RID: 252
		[Flags]
		public enum ClearCacheOption
		{
			// Token: 0x0400020A RID: 522
			None = 0,
			// Token: 0x0400020B RID: 523
			OnInit = 1,
			// Token: 0x0400020C RID: 524
			OnClose = 2
		}
	}
}
