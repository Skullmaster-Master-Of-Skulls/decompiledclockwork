using System;
using Telerik.Web.Apoc.Fo;

namespace Telerik.Web.Apoc.DataTypes
{
	// Token: 0x0200137C RID: 4988
	internal interface ICompoundDatatype
	{
		// Token: 0x0600D021 RID: 53281
		void SetComponent(string componentName, Property componentValue, bool isDefault);

		// Token: 0x0600D022 RID: 53282
		Property GetComponent(string componentName);
	}
}
