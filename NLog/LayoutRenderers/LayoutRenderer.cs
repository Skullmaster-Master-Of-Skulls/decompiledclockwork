using System;
using System.Globalization;
using System.Text;
using NLog.Common;
using NLog.Config;
using NLog.Internal;

namespace NLog.LayoutRenderers
{
	// Token: 0x020000BF RID: 191
	[NLogConfigurationItem]
	public abstract class LayoutRenderer : ISupportsInitialize, IRenderable, IDisposable
	{
		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x0600058D RID: 1421 RVA: 0x0000C886 File Offset: 0x0000AA86
		// (set) Token: 0x0600058E RID: 1422 RVA: 0x0000C88E File Offset: 0x0000AA8E
		private protected LoggingConfiguration LoggingConfiguration { protected get; private set; }

		// Token: 0x0600058F RID: 1423 RVA: 0x0000C898 File Offset: 0x0000AA98
		public override string ToString()
		{
			LayoutRendererAttribute layoutRendererAttribute = (LayoutRendererAttribute)Attribute.GetCustomAttribute(base.GetType(), typeof(LayoutRendererAttribute));
			if (layoutRendererAttribute != null)
			{
				return "Layout Renderer: ${" + layoutRendererAttribute.Name + "}";
			}
			return base.GetType().Name;
		}

		// Token: 0x06000590 RID: 1424 RVA: 0x0000C8E4 File Offset: 0x0000AAE4
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06000591 RID: 1425 RVA: 0x0000C8F4 File Offset: 0x0000AAF4
		public string Render(LogEventInfo logEvent)
		{
			int num = this.maxRenderedLength;
			if (num > 16384)
			{
				num = 16384;
			}
			StringBuilder stringBuilder = new StringBuilder(num);
			this.Render(stringBuilder, logEvent);
			if (stringBuilder.Length > this.maxRenderedLength)
			{
				this.maxRenderedLength = stringBuilder.Length;
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000592 RID: 1426 RVA: 0x0000C945 File Offset: 0x0000AB45
		void ISupportsInitialize.Initialize(LoggingConfiguration configuration)
		{
			this.Initialize(configuration);
		}

		// Token: 0x06000593 RID: 1427 RVA: 0x0000C94E File Offset: 0x0000AB4E
		void ISupportsInitialize.Close()
		{
			this.Close();
		}

		// Token: 0x06000594 RID: 1428 RVA: 0x0000C956 File Offset: 0x0000AB56
		internal void Initialize(LoggingConfiguration configuration)
		{
			if (this.LoggingConfiguration == null)
			{
				this.LoggingConfiguration = configuration;
			}
			if (!this.isInitialized)
			{
				this.isInitialized = true;
				this.InitializeLayoutRenderer();
			}
		}

		// Token: 0x06000595 RID: 1429 RVA: 0x0000C97C File Offset: 0x0000AB7C
		internal void Close()
		{
			if (this.isInitialized)
			{
				this.LoggingConfiguration = null;
				this.isInitialized = false;
				this.CloseLayoutRenderer();
			}
		}

		// Token: 0x06000596 RID: 1430 RVA: 0x0000C99C File Offset: 0x0000AB9C
		internal void Render(StringBuilder builder, LogEventInfo logEvent)
		{
			if (!this.isInitialized)
			{
				this.isInitialized = true;
				this.InitializeLayoutRenderer();
			}
			try
			{
				this.Append(builder, logEvent);
			}
			catch (Exception ex)
			{
				InternalLogger.Warn(ex, "Exception in layout renderer.");
				if (ex.MustBeRethrown())
				{
					throw;
				}
			}
		}

		// Token: 0x06000597 RID: 1431
		protected abstract void Append(StringBuilder builder, LogEventInfo logEvent);

		// Token: 0x06000598 RID: 1432 RVA: 0x0000C9F0 File Offset: 0x0000ABF0
		protected virtual void InitializeLayoutRenderer()
		{
		}

		// Token: 0x06000599 RID: 1433 RVA: 0x0000C9F2 File Offset: 0x0000ABF2
		protected virtual void CloseLayoutRenderer()
		{
		}

		// Token: 0x0600059A RID: 1434 RVA: 0x0000C9F4 File Offset: 0x0000ABF4
		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				this.Close();
			}
		}

		// Token: 0x0600059B RID: 1435 RVA: 0x0000CA00 File Offset: 0x0000AC00
		protected IFormatProvider GetFormatProvider(LogEventInfo logEvent, IFormatProvider layoutCulture = null)
		{
			IFormatProvider formatProvider = logEvent.FormatProvider;
			if (formatProvider == null)
			{
				formatProvider = layoutCulture;
			}
			if (formatProvider == null && this.LoggingConfiguration != null)
			{
				formatProvider = this.LoggingConfiguration.DefaultCultureInfo;
			}
			return formatProvider;
		}

		// Token: 0x0600059C RID: 1436 RVA: 0x0000CA34 File Offset: 0x0000AC34
		protected CultureInfo GetCulture(LogEventInfo logEvent, CultureInfo layoutCulture = null)
		{
			CultureInfo cultureInfo = logEvent.FormatProvider as CultureInfo;
			if (cultureInfo == null)
			{
				cultureInfo = layoutCulture;
			}
			if (cultureInfo == null && this.LoggingConfiguration != null)
			{
				cultureInfo = this.LoggingConfiguration.DefaultCultureInfo;
			}
			return cultureInfo;
		}

		// Token: 0x04000146 RID: 326
		private const int MaxInitialRenderBufferLength = 16384;

		// Token: 0x04000147 RID: 327
		private int maxRenderedLength;

		// Token: 0x04000148 RID: 328
		private bool isInitialized;
	}
}
