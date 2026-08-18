using System;
using log4net.Core;
using log4net.Plugin;
using log4net.Util;

namespace log4net.Config
{
	// Token: 0x02000053 RID: 83
	[AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true)]
	[Serializable]
	public sealed class PluginAttribute : Attribute, IPluginFactory
	{
		// Token: 0x060002B8 RID: 696 RVA: 0x000094B4 File Offset: 0x000076B4
		public PluginAttribute(string typeName)
		{
			this.m_typeName = typeName;
		}

		// Token: 0x060002B9 RID: 697 RVA: 0x000094C3 File Offset: 0x000076C3
		public PluginAttribute(Type type)
		{
			this.m_type = type;
		}

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x060002BA RID: 698 RVA: 0x000094D2 File Offset: 0x000076D2
		// (set) Token: 0x060002BB RID: 699 RVA: 0x000094DA File Offset: 0x000076DA
		public Type Type
		{
			get
			{
				return this.m_type;
			}
			set
			{
				this.m_type = value;
			}
		}

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x060002BC RID: 700 RVA: 0x000094E3 File Offset: 0x000076E3
		// (set) Token: 0x060002BD RID: 701 RVA: 0x000094EB File Offset: 0x000076EB
		public string TypeName
		{
			get
			{
				return this.m_typeName;
			}
			set
			{
				this.m_typeName = value;
			}
		}

		// Token: 0x060002BE RID: 702 RVA: 0x000094F4 File Offset: 0x000076F4
		public IPlugin CreatePlugin()
		{
			Type type = this.m_type;
			if (this.m_type == null)
			{
				type = SystemInfo.GetTypeFromString(this.m_typeName, true, true);
			}
			if (!typeof(IPlugin).IsAssignableFrom(type))
			{
				throw new LogException("Plugin type [" + type.FullName + "] does not implement the log4net.IPlugin interface");
			}
			return (IPlugin)Activator.CreateInstance(type);
		}

		// Token: 0x060002BF RID: 703 RVA: 0x0000955E File Offset: 0x0000775E
		public override string ToString()
		{
			if (this.m_type != null)
			{
				return "PluginAttribute[Type=" + this.m_type.FullName + "]";
			}
			return "PluginAttribute[Type=" + this.m_typeName + "]";
		}

		// Token: 0x0400014D RID: 333
		private string m_typeName;

		// Token: 0x0400014E RID: 334
		private Type m_type;
	}
}
