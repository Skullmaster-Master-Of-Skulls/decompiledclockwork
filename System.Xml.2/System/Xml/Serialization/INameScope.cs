using System;

namespace System.Xml.Serialization
{
	// Token: 0x02000165 RID: 357
	internal interface INameScope
	{
		// Token: 0x1700052E RID: 1326
		object this[string name, string ns]
		{
			get;
			set;
		}
	}
}
