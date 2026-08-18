using System;
using System.Collections;
using System.Globalization;
using System.IO;
using log4net.Util;

namespace log4net.ObjectRenderer
{
	// Token: 0x020000B8 RID: 184
	public class RendererMap
	{
		// Token: 0x06000535 RID: 1333 RVA: 0x00010AA2 File Offset: 0x0000ECA2
		public RendererMap()
		{
			this.m_map = Hashtable.Synchronized(new Hashtable());
		}

		// Token: 0x06000536 RID: 1334 RVA: 0x00010AC8 File Offset: 0x0000ECC8
		public string FindAndRender(object obj)
		{
			string text = obj as string;
			if (text != null)
			{
				return text;
			}
			StringWriter stringWriter = new StringWriter(CultureInfo.InvariantCulture);
			this.FindAndRender(obj, stringWriter);
			return stringWriter.ToString();
		}

		// Token: 0x06000537 RID: 1335 RVA: 0x00010AFC File Offset: 0x0000ECFC
		public void FindAndRender(object obj, TextWriter writer)
		{
			if (obj == null)
			{
				writer.Write(SystemInfo.NullText);
				return;
			}
			string text = obj as string;
			if (text != null)
			{
				writer.Write(text);
				return;
			}
			try
			{
				this.Get(obj.GetType()).RenderObject(this, obj, writer);
			}
			catch (Exception ex)
			{
				LogLog.Error(RendererMap.declaringType, "Exception while rendering object of type [" + obj.GetType().FullName + "]", ex);
				string str = "";
				if (obj != null && obj.GetType() != null)
				{
					str = obj.GetType().FullName;
				}
				writer.Write("<log4net.Error>Exception rendering object type [" + str + "]");
				if (ex != null)
				{
					string str2 = null;
					try
					{
						str2 = ex.ToString();
					}
					catch
					{
					}
					writer.Write("<stackTrace>" + str2 + "</stackTrace>");
				}
				writer.Write("</log4net.Error>");
			}
		}

		// Token: 0x06000538 RID: 1336 RVA: 0x00010BF4 File Offset: 0x0000EDF4
		public IObjectRenderer Get(object obj)
		{
			if (obj == null)
			{
				return null;
			}
			return this.Get(obj.GetType());
		}

		// Token: 0x06000539 RID: 1337 RVA: 0x00010C08 File Offset: 0x0000EE08
		public IObjectRenderer Get(Type type)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			IObjectRenderer objectRenderer = (IObjectRenderer)this.m_cache[type];
			if (objectRenderer == null)
			{
				Type type2 = type;
				while (type2 != null)
				{
					objectRenderer = this.SearchTypeAndInterfaces(type2);
					if (objectRenderer != null)
					{
						break;
					}
					type2 = type2.BaseType;
				}
				if (objectRenderer == null)
				{
					objectRenderer = RendererMap.s_defaultRenderer;
				}
				this.m_cache[type] = objectRenderer;
			}
			return objectRenderer;
		}

		// Token: 0x0600053A RID: 1338 RVA: 0x00010C78 File Offset: 0x0000EE78
		private IObjectRenderer SearchTypeAndInterfaces(Type type)
		{
			IObjectRenderer objectRenderer = (IObjectRenderer)this.m_map[type];
			if (objectRenderer != null)
			{
				return objectRenderer;
			}
			foreach (Type type2 in type.GetInterfaces())
			{
				objectRenderer = this.SearchTypeAndInterfaces(type2);
				if (objectRenderer != null)
				{
					return objectRenderer;
				}
			}
			return null;
		}

		// Token: 0x1700011F RID: 287
		// (get) Token: 0x0600053B RID: 1339 RVA: 0x00010CCC File Offset: 0x0000EECC
		public IObjectRenderer DefaultRenderer
		{
			get
			{
				return RendererMap.s_defaultRenderer;
			}
		}

		// Token: 0x0600053C RID: 1340 RVA: 0x00010CD3 File Offset: 0x0000EED3
		public void Clear()
		{
			this.m_map.Clear();
			this.m_cache.Clear();
		}

		// Token: 0x0600053D RID: 1341 RVA: 0x00010CEB File Offset: 0x0000EEEB
		public void Put(Type typeToRender, IObjectRenderer renderer)
		{
			this.m_cache.Clear();
			if (typeToRender == null)
			{
				throw new ArgumentNullException("typeToRender");
			}
			if (renderer == null)
			{
				throw new ArgumentNullException("renderer");
			}
			this.m_map[typeToRender] = renderer;
		}

		// Token: 0x04000235 RID: 565
		private static readonly Type declaringType = typeof(RendererMap);

		// Token: 0x04000236 RID: 566
		private Hashtable m_map;

		// Token: 0x04000237 RID: 567
		private Hashtable m_cache = new Hashtable();

		// Token: 0x04000238 RID: 568
		private static IObjectRenderer s_defaultRenderer = new DefaultRenderer();
	}
}
