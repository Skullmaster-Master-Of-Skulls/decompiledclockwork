using System;
using TechnoPro.Common.Core.MarkedForDeletion.MarkedForDeletionImplementations;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.MarkedForDeletion;

namespace TechnoPro.Common.Core.MarkedForDeletion
{
	// Token: 0x020000BA RID: 186
	public static class MarkedForDeletionFactory
	{
		// Token: 0x06000700 RID: 1792 RVA: 0x00029210 File Offset: 0x00027410
		public static MarkedForDeletion_Base GetInstance(this MarkedForDeletionJob job, OperationContext opContext)
		{
			string str = job.MarkedForDeletionType.ToString();
			string typeName = "TechnoPro.Common.Core.MarkedForDeletion.MarkedForDeletionImplementations.MarkedForDeletion" + str;
			Type type = Type.GetType(typeName);
			bool flag = type == null;
			MarkedForDeletion_Base result;
			if (flag)
			{
				result = null;
			}
			else
			{
				MarkedForDeletion_Base markedForDeletion_Base = (MarkedForDeletion_Base)Activator.CreateInstance(type);
				markedForDeletion_Base.OpContext = opContext;
				result = markedForDeletion_Base;
			}
			return result;
		}
	}
}
