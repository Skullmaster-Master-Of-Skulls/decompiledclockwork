using System;
using System.Collections.Generic;
using System.Reflection;
using NLog.Common;
using NLog.Internal;

namespace NLog.Config
{
	// Token: 0x02000053 RID: 83
	internal class MethodFactory<TClassAttributeType, TMethodAttributeType> : INamedItemFactory<MethodInfo, MethodInfo>, IFactory where TClassAttributeType : Attribute where TMethodAttributeType : NameBaseAttribute
	{
		// Token: 0x17000053 RID: 83
		// (get) Token: 0x060001BB RID: 443 RVA: 0x000065B5 File Offset: 0x000047B5
		public IDictionary<string, MethodInfo> AllRegisteredItems
		{
			get
			{
				return this.nameToMethodInfo;
			}
		}

		// Token: 0x060001BC RID: 444 RVA: 0x000065C0 File Offset: 0x000047C0
		public void ScanTypes(Type[] types, string prefix)
		{
			foreach (Type type in types)
			{
				try
				{
					this.RegisterType(type, prefix);
				}
				catch (Exception ex)
				{
					InternalLogger.Error(ex, "Failed to add type '{0}'.", new object[]
					{
						type.FullName
					});
					if (ex.MustBeRethrown())
					{
						throw;
					}
				}
			}
		}

		// Token: 0x060001BD RID: 445 RVA: 0x00006628 File Offset: 0x00004828
		public void RegisterType(Type type, string itemNamePrefix)
		{
			if (type.IsDefined(typeof(TClassAttributeType), false))
			{
				foreach (MethodInfo methodInfo in type.GetMethods())
				{
					TMethodAttributeType[] array = (TMethodAttributeType[])methodInfo.GetCustomAttributes(typeof(TMethodAttributeType), false);
					foreach (TMethodAttributeType tmethodAttributeType in array)
					{
						this.RegisterDefinition(itemNamePrefix + tmethodAttributeType.Name, methodInfo);
					}
				}
			}
		}

		// Token: 0x060001BE RID: 446 RVA: 0x000066B7 File Offset: 0x000048B7
		public void Clear()
		{
			this.nameToMethodInfo.Clear();
		}

		// Token: 0x060001BF RID: 447 RVA: 0x000066C4 File Offset: 0x000048C4
		public void RegisterDefinition(string name, MethodInfo methodInfo)
		{
			this.nameToMethodInfo[name] = methodInfo;
		}

		// Token: 0x060001C0 RID: 448 RVA: 0x000066D3 File Offset: 0x000048D3
		public bool TryCreateInstance(string name, out MethodInfo result)
		{
			return this.nameToMethodInfo.TryGetValue(name, out result);
		}

		// Token: 0x060001C1 RID: 449 RVA: 0x000066E4 File Offset: 0x000048E4
		public MethodInfo CreateInstance(string name)
		{
			MethodInfo result;
			if (this.TryCreateInstance(name, out result))
			{
				return result;
			}
			throw new NLogConfigurationException("Unknown function: '" + name + "'");
		}

		// Token: 0x060001C2 RID: 450 RVA: 0x00006713 File Offset: 0x00004913
		public bool TryGetDefinition(string name, out MethodInfo result)
		{
			return this.nameToMethodInfo.TryGetValue(name, out result);
		}

		// Token: 0x040000A7 RID: 167
		private readonly Dictionary<string, MethodInfo> nameToMethodInfo = new Dictionary<string, MethodInfo>();
	}
}
