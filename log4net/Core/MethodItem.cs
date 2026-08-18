using System;
using System.Collections;
using System.Reflection;
using log4net.Util;

namespace log4net.Core
{
	// Token: 0x02000073 RID: 115
	[Serializable]
	public class MethodItem
	{
		// Token: 0x0600042D RID: 1069 RVA: 0x0000DE17 File Offset: 0x0000C017
		public MethodItem()
		{
			this.m_name = "?";
			this.m_parameters = new string[0];
		}

		// Token: 0x0600042E RID: 1070 RVA: 0x0000DE36 File Offset: 0x0000C036
		public MethodItem(string name) : this()
		{
			this.m_name = name;
		}

		// Token: 0x0600042F RID: 1071 RVA: 0x0000DE45 File Offset: 0x0000C045
		public MethodItem(string name, string[] parameters) : this(name)
		{
			this.m_parameters = parameters;
		}

		// Token: 0x06000430 RID: 1072 RVA: 0x0000DE55 File Offset: 0x0000C055
		public MethodItem(MethodBase methodBase) : this(methodBase.Name, MethodItem.GetMethodParameterNames(methodBase))
		{
		}

		// Token: 0x06000431 RID: 1073 RVA: 0x0000DE6C File Offset: 0x0000C06C
		private static string[] GetMethodParameterNames(MethodBase methodBase)
		{
			ArrayList arrayList = new ArrayList();
			try
			{
				ParameterInfo[] parameters = methodBase.GetParameters();
				int upperBound = parameters.GetUpperBound(0);
				for (int i = 0; i <= upperBound; i++)
				{
					arrayList.Add(parameters[i].ParameterType + " " + parameters[i].Name);
				}
			}
			catch (Exception exception)
			{
				LogLog.Error(MethodItem.declaringType, "An exception ocurred while retreiving method parameters.", exception);
			}
			return (string[])arrayList.ToArray(typeof(string));
		}

		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x06000432 RID: 1074 RVA: 0x0000DEF8 File Offset: 0x0000C0F8
		public string Name
		{
			get
			{
				return this.m_name;
			}
		}

		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x06000433 RID: 1075 RVA: 0x0000DF00 File Offset: 0x0000C100
		public string[] Parameters
		{
			get
			{
				return this.m_parameters;
			}
		}

		// Token: 0x040001C8 RID: 456
		private const string NA = "?";

		// Token: 0x040001C9 RID: 457
		private readonly string m_name;

		// Token: 0x040001CA RID: 458
		private readonly string[] m_parameters;

		// Token: 0x040001CB RID: 459
		private static readonly Type declaringType = typeof(MethodItem);
	}
}
