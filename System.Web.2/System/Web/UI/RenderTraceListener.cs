using System;
using System.Collections.Generic;
using System.IO;

namespace System.Web.UI
{
	// Token: 0x020002F5 RID: 757
	public abstract class RenderTraceListener
	{
		// Token: 0x170009D4 RID: 2516
		// (get) Token: 0x06002311 RID: 8977 RVA: 0x000723AD File Offset: 0x000705AD
		public static IList<Func<RenderTraceListener>> ListenerFactories
		{
			get
			{
				if (RenderTraceListener._factories == null)
				{
					RenderTraceListener._factories = new List<Func<RenderTraceListener>>();
				}
				return RenderTraceListener._factories;
			}
		}

		// Token: 0x170009D5 RID: 2517
		// (get) Token: 0x06002312 RID: 8978 RVA: 0x000723C8 File Offset: 0x000705C8
		internal static RenderTraceListener CurrentListeners
		{
			get
			{
				if (RenderTraceListener._factories != null && HttpContext.Current != null)
				{
					RenderTraceListener renderTraceListener = HttpContext.Current.Items[typeof(RenderTraceListener)] as RenderTraceListener;
					if (renderTraceListener == null)
					{
						renderTraceListener = RenderTraceListener.CreateListener(HttpContext.Current);
						HttpContext.Current.Items[typeof(RenderTraceListener)] = renderTraceListener;
					}
					return renderTraceListener;
				}
				return RenderTraceListener._nullListener;
			}
		}

		// Token: 0x06002313 RID: 8979 RVA: 0x00072434 File Offset: 0x00070634
		private static RenderTraceListener CreateListener(HttpContext context)
		{
			List<RenderTraceListener> list = new List<RenderTraceListener>();
			foreach (Func<RenderTraceListener> func in RenderTraceListener._factories)
			{
				RenderTraceListener renderTraceListener = func();
				if (renderTraceListener != null)
				{
					list.Add(renderTraceListener);
				}
			}
			RenderTraceListener.RenderTraceListenerList renderTraceListenerList = new RenderTraceListener.RenderTraceListenerList(list);
			renderTraceListenerList.Initialize(context);
			return renderTraceListenerList;
		}

		// Token: 0x06002314 RID: 8980 RVA: 0x00006164 File Offset: 0x00004364
		public virtual void Initialize(HttpContext context)
		{
		}

		// Token: 0x06002315 RID: 8981 RVA: 0x00006164 File Offset: 0x00004364
		public virtual void SetTraceData(object tracedObject, object traceDataKey, object traceDataValue)
		{
		}

		// Token: 0x06002316 RID: 8982 RVA: 0x00006164 File Offset: 0x00004364
		public virtual void ShareTraceData(object source, object destination)
		{
		}

		// Token: 0x06002317 RID: 8983 RVA: 0x00006164 File Offset: 0x00004364
		public virtual void BeginRendering(TextWriter writer, object renderedObject)
		{
		}

		// Token: 0x06002318 RID: 8984 RVA: 0x00006164 File Offset: 0x00004364
		public virtual void EndRendering(TextWriter writer, object renderedObject)
		{
		}

		// Token: 0x04001C9C RID: 7324
		private static readonly RenderTraceListener _nullListener = new RenderTraceListener.NullRenderTraceListener();

		// Token: 0x04001C9D RID: 7325
		private static List<Func<RenderTraceListener>> _factories;

		// Token: 0x02000983 RID: 2435
		private sealed class NullRenderTraceListener : RenderTraceListener
		{
		}

		// Token: 0x02000984 RID: 2436
		private sealed class RenderTraceListenerList : RenderTraceListener
		{
			// Token: 0x06006A44 RID: 27204 RVA: 0x0017BA3C File Offset: 0x00179C3C
			internal RenderTraceListenerList(List<RenderTraceListener> listeners)
			{
				this._listeners = listeners;
			}

			// Token: 0x06006A45 RID: 27205 RVA: 0x0017BA4C File Offset: 0x00179C4C
			public override void Initialize(HttpContext context)
			{
				foreach (RenderTraceListener renderTraceListener in this._listeners)
				{
					renderTraceListener.Initialize(context);
				}
			}

			// Token: 0x06006A46 RID: 27206 RVA: 0x0017BAA0 File Offset: 0x00179CA0
			public override void SetTraceData(object tracedObject, object traceDataKey, object traceDataValue)
			{
				foreach (RenderTraceListener renderTraceListener in this._listeners)
				{
					renderTraceListener.SetTraceData(tracedObject, traceDataKey, traceDataValue);
				}
			}

			// Token: 0x06006A47 RID: 27207 RVA: 0x0017BAF8 File Offset: 0x00179CF8
			public override void ShareTraceData(object source, object destination)
			{
				foreach (RenderTraceListener renderTraceListener in this._listeners)
				{
					renderTraceListener.ShareTraceData(source, destination);
				}
			}

			// Token: 0x06006A48 RID: 27208 RVA: 0x0017BB4C File Offset: 0x00179D4C
			public override void BeginRendering(TextWriter writer, object renderedObject)
			{
				foreach (RenderTraceListener renderTraceListener in this._listeners)
				{
					renderTraceListener.BeginRendering(writer, renderedObject);
				}
			}

			// Token: 0x06006A49 RID: 27209 RVA: 0x0017BBA0 File Offset: 0x00179DA0
			public override void EndRendering(TextWriter writer, object renderedObject)
			{
				for (int i = this._listeners.Count - 1; i >= 0; i--)
				{
					this._listeners[i].EndRendering(writer, renderedObject);
				}
			}

			// Token: 0x040038BB RID: 14523
			private readonly List<RenderTraceListener> _listeners;
		}
	}
}
