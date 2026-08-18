using System;
using System.Collections.Generic;
using System.Configuration;

namespace System.Data
{
	// Token: 0x0200009A RID: 154
	internal sealed class SerializationConfig
	{
		// Token: 0x060007DA RID: 2010 RVA: 0x00056914 File Offset: 0x00055D14
		private SerializationConfig()
		{
			AllowedTypesSectionHandler.Data data = ((AllowedTypesSectionHandler.Data)ConfigurationManager.GetSection("system.data.dataset.serialization/allowedTypes")) ?? new AllowedTypesSectionHandler.Data();
			this.m_auditMode = data.AuditMode;
			foreach (string text in data.AllowedTypes)
			{
				if (!string.IsNullOrWhiteSpace(text))
				{
					this.m_allowedTypeList.Add(Type.GetType(text.Trim(), true));
				}
			}
		}

		// Token: 0x060007DB RID: 2011 RVA: 0x000569BC File Offset: 0x00055DBC
		private static void EnsureInitialized()
		{
			if (SerializationConfig.s_instance == null)
			{
				SerializationConfig.s_instance = new SerializationConfig();
			}
		}

		// Token: 0x060007DC RID: 2012 RVA: 0x000569E0 File Offset: 0x00055DE0
		public static bool IsAuditMode()
		{
			SerializationConfig.EnsureInitialized();
			return SerializationConfig.s_instance.m_auditMode;
		}

		// Token: 0x060007DD RID: 2013 RVA: 0x00056A00 File Offset: 0x00055E00
		public static bool IsTypeAllowed(Type type)
		{
			if (type == null)
			{
				return true;
			}
			SerializationConfig.EnsureInitialized();
			return SerializationConfig.s_instance.m_allowedTypeList.Contains(type);
		}

		// Token: 0x040002D3 RID: 723
		private static volatile SerializationConfig s_instance;

		// Token: 0x040002D4 RID: 724
		private readonly bool m_auditMode;

		// Token: 0x040002D5 RID: 725
		private readonly List<Type> m_allowedTypeList = new List<Type>();
	}
}
