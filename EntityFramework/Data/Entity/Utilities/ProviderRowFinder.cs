using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace System.Data.Entity.Utilities
{
	// Token: 0x020006EA RID: 1770
	internal class ProviderRowFinder
	{
		// Token: 0x06004716 RID: 18198 RVA: 0x00150964 File Offset: 0x0014EB64
		[SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")]
		public virtual DataRow FindRow(Type hintType, Func<DataRow, bool> selector, IEnumerable<DataRow> dataRows)
		{
			AssemblyName assemblyName = (hintType == null) ? null : new AssemblyName(hintType.Assembly().FullName);
			foreach (DataRow dataRow in dataRows)
			{
				string typeName = (string)dataRow[3];
				AssemblyName rowProviderFactoryAssemblyName = null;
				Type.GetType(typeName, delegate(AssemblyName a)
				{
					rowProviderFactoryAssemblyName = a;
					return null;
				}, (Assembly _, string __, bool ___) => null);
				if (rowProviderFactoryAssemblyName != null)
				{
					if (!(hintType == null))
					{
						if (!string.Equals(assemblyName.Name, rowProviderFactoryAssemblyName.Name, StringComparison.OrdinalIgnoreCase))
						{
							continue;
						}
					}
					try
					{
						if (selector(dataRow))
						{
							return dataRow;
						}
					}
					catch (Exception)
					{
					}
				}
			}
			return null;
		}
	}
}
