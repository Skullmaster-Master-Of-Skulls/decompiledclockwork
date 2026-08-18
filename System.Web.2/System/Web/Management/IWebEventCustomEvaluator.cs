using System;

namespace System.Web.Management
{
	// Token: 0x02000189 RID: 393
	public interface IWebEventCustomEvaluator
	{
		// Token: 0x06001523 RID: 5411
		bool CanFire(WebBaseEvent raisedEvent, RuleFiringRecord record);
	}
}
