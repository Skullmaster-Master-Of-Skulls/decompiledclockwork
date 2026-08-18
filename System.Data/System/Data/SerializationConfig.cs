using System;
using System.Collections.Generic;
using System.Configuration;

namespace System.Data
{
	// Token: 0x0200019E RID: 414
	internal sealed class SerializationConfig
	{
		// Token: 0x06001834 RID: 6196 RVA: 0x002509E8 File Offset: 0x0024FDE8
		private SerializationConfig()
		{
			AllowedTypesSectionHandler.Data data = ((AllowedTypesSectionHandler.Data)ConfigurationManager.GetSection("system.data.dataset.serialization/allowedTypes")) ?? new AllowedTypesSectionHandler.Data();
			this.m_auditMode = data.AuditMode;
			foreach (string text in data.AllowedTypes)
			{
				if (text != null && !(text.Trim() == ""))
				{
					this.m_allowedTypeList.Add(Type.GetType(text.Trim(), true));
				}
			}
		}

		// Token: 0x06001835 RID: 6197 RVA: 0x00250AA8 File Offset: 0x0024FEA8
		private static void EnsureInitialized()
		{
			if (SerializationConfig.s_instance == null)
			{
				SerializationConfig.s_instance = new SerializationConfig();
			}
		}

		// Token: 0x06001836 RID: 6198 RVA: 0x00250AD8 File Offset: 0x0024FED8
		public static bool IsAuditMode()
		{
			SerializationConfig.EnsureInitialized();
			return SerializationConfig.s_instance.m_auditMode;
		}

		// Token: 0x06001837 RID: 6199 RVA: 0x00250AF8 File Offset: 0x0024FEF8
		public static bool IsTypeAllowed(Type type)
		{
			if (type == null)
			{
				return true;
			}
			SerializationConfig.EnsureInitialized();
			return SerializationConfig.s_instance.m_allowedTypeList.Contains(type);
		}

		// Token: 0x04000D20 RID: 3360
		private static volatile SerializationConfig s_instance;

		// Token: 0x04000D21 RID: 3361
		private readonly bool m_auditMode;

		// Token: 0x04000D22 RID: 3362
		private readonly List<Type> m_allowedTypeList = new List<Type>();
	}
}
