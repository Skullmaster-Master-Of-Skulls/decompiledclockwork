using System;

namespace System.Configuration
{
	// Token: 0x0200003E RID: 62
	public abstract class ConfigurationValidatorBase
	{
		// Token: 0x060002D9 RID: 729 RVA: 0x00008751 File Offset: 0x00006951
		public virtual bool CanValidate(Type type)
		{
			return false;
		}

		// Token: 0x060002DA RID: 730
		public abstract void Validate(object value);
	}
}
