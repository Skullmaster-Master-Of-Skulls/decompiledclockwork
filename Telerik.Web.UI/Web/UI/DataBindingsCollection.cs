using System;
using System.Collections.Generic;

namespace Telerik.Web.UI
{
	// Token: 0x02000BC8 RID: 3016
	internal class DataBindingsCollection : List<DataBindings>
	{
		// Token: 0x0600736C RID: 29548 RVA: 0x001AFE80 File Offset: 0x001AE080
		public static DataBindingsCollection FromStateManagedCollection(NavigationItemBindingCollection bindings)
		{
			DataBindingsCollection dataBindingsCollection = new DataBindingsCollection();
			foreach (object obj in bindings)
			{
				NavigationItemBinding navigationItemBinding = (NavigationItemBinding)obj;
				dataBindingsCollection.Add(new DataBindings
				{
					DataFieldID = navigationItemBinding.FieldID,
					DataFieldParentID = navigationItemBinding.FieldParentID,
					DataTextField = navigationItemBinding.TextField,
					DataValueField = navigationItemBinding.ValueField,
					DataModelID = navigationItemBinding.ModelID,
					Depth = navigationItemBinding.Depth
				});
			}
			dataBindingsCollection.Sort(delegate(DataBindings a, DataBindings b)
			{
				if (a.Depth - b.Depth < 0)
				{
					return -1;
				}
				if (a.Depth - b.Depth <= 0)
				{
					return 0;
				}
				return 1;
			});
			return dataBindingsCollection;
		}
	}
}
