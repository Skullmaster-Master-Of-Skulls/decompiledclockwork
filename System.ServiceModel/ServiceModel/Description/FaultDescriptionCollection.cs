using System;
using System.Collections.ObjectModel;

namespace System.ServiceModel.Description
{
	// Token: 0x020003C8 RID: 968
	[__DynamicallyInvokable]
	public class FaultDescriptionCollection : Collection<FaultDescription>
	{
		// Token: 0x0600247D RID: 9341 RVA: 0x000842EE File Offset: 0x000824EE
		internal FaultDescriptionCollection()
		{
		}

		// Token: 0x0600247E RID: 9342 RVA: 0x000842F8 File Offset: 0x000824F8
		public FaultDescription Find(string action)
		{
			foreach (FaultDescription faultDescription in this)
			{
				if (faultDescription != null && action == faultDescription.Action)
				{
					return faultDescription;
				}
			}
			return null;
		}

		// Token: 0x0600247F RID: 9343 RVA: 0x00084354 File Offset: 0x00082554
		public Collection<FaultDescription> FindAll(string action)
		{
			Collection<FaultDescription> collection = new Collection<FaultDescription>();
			foreach (FaultDescription faultDescription in this)
			{
				if (faultDescription != null && action == faultDescription.Action)
				{
					collection.Add(faultDescription);
				}
			}
			return collection;
		}
	}
}
