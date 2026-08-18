using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.ServiceModel.Configuration;
using System.ServiceModel.Description;

namespace TechnoPro.Common.WCF.Configuration
{
	// Token: 0x02000015 RID: 21
	public class WCFConfigurationHelper
	{
		// Token: 0x06000079 RID: 121 RVA: 0x00003D04 File Offset: 0x00001F04
		public static BehaviorsSection GetBehaviorsSection()
		{
			return ConfigurationManager.GetSection("system.serviceModel/behaviors") as BehaviorsSection;
		}

		// Token: 0x0600007A RID: 122 RVA: 0x00003D28 File Offset: 0x00001F28
		public static IList<IServiceBehavior> GetServiceBehaviorsByName(string svcBehaviorName)
		{
			List<IServiceBehavior> list = new List<IServiceBehavior>();
			BehaviorsSection behaviorsSection = WCFConfigurationHelper.GetBehaviorsSection();
			bool flag = behaviorsSection != null;
			if (flag)
			{
				ServiceBehaviorElementCollection serviceBehaviors = behaviorsSection.ServiceBehaviors;
				bool flag2 = serviceBehaviors != null && serviceBehaviors.Count > 0;
				if (flag2)
				{
					ServiceBehaviorElement serviceBehaviorElement = serviceBehaviors.Cast<ServiceBehaviorElement>().FirstOrDefault((ServiceBehaviorElement svcBehavior) => svcBehavior.Name == svcBehaviorName);
					bool flag3 = serviceBehaviorElement != null;
					if (flag3)
					{
						list.AddRange((from behaviorExtension in serviceBehaviorElement
						select behaviorExtension.GetType().InvokeMember("CreateBehavior", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.InvokeMethod, null, behaviorExtension, null) into extension
						select extension).Cast<IServiceBehavior>());
					}
				}
			}
			return list;
		}
	}
}
