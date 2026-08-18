using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Microsoft.Practices.Unity.Configuration.ConfigurationHelpers;
using Microsoft.Practices.Unity.Utility;

namespace Microsoft.Practices.Unity.Configuration
{
	// Token: 0x0200001B RID: 27
	public abstract class InjectionMemberElement : DeserializableConfigurationElement
	{
		// Token: 0x1700001C RID: 28
		// (get) Token: 0x060000D4 RID: 212
		public abstract string Key { get; }

		// Token: 0x060000D5 RID: 213
		public abstract IEnumerable<InjectionMember> GetInjectionMembers(IUnityContainer container, Type fromType, Type toType, string name);

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x060000D6 RID: 214 RVA: 0x0000490F File Offset: 0x00002B0F
		public virtual string ElementName
		{
			get
			{
				return ExtensionElementMap.GetTagForExtensionElement(this);
			}
		}

		// Token: 0x060000D7 RID: 215 RVA: 0x00004917 File Offset: 0x00002B17
		[SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods", Justification = "Validation done by Guard class")]
		public static string GetMemberElementName(InjectionMemberElement memberElement)
		{
			Guard.ArgumentNotNull(memberElement, "memberElement");
			return memberElement.ElementName;
		}
	}
}
