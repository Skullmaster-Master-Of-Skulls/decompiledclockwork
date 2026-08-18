using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace System.ServiceModel.Security
{
	// Token: 0x02000353 RID: 851
	internal static class EmptyReadOnlyCollection<T>
	{
		// Token: 0x04001ED3 RID: 7891
		public static ReadOnlyCollection<T> Instance = new ReadOnlyCollection<T>(new List<T>());
	}
}
