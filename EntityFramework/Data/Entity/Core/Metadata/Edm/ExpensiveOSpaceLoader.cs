using System;
using System.Collections.Generic;
using System.Data.Entity.Utilities;
using System.Reflection;

namespace System.Data.Entity.Core.Metadata.Edm
{
	// Token: 0x020001F8 RID: 504
	internal class ExpensiveOSpaceLoader
	{
		// Token: 0x060011A4 RID: 4516 RVA: 0x0004B228 File Offset: 0x00049428
		public virtual Dictionary<string, EdmType> LoadTypesExpensiveWay(Assembly assembly)
		{
			KnownAssembliesSet knownAssemblies = new KnownAssembliesSet();
			Dictionary<string, EdmType> result;
			List<EdmItemError> list;
			AssemblyCache.LoadAssembly(assembly, false, knownAssemblies, out result, out list);
			if (list.Count != 0)
			{
				throw EntityUtil.InvalidSchemaEncountered(Helper.CombineErrorMessage(list));
			}
			return result;
		}

		// Token: 0x060011A5 RID: 4517 RVA: 0x0004B25C File Offset: 0x0004945C
		public virtual AssociationType GetRelationshipTypeExpensiveWay(Type entityClrType, string relationshipName)
		{
			Dictionary<string, EdmType> dictionary = this.LoadTypesExpensiveWay(entityClrType.Assembly());
			EdmType edmType;
			if (dictionary != null && dictionary.TryGetValue(relationshipName, out edmType) && Helper.IsRelationshipType(edmType))
			{
				return (AssociationType)edmType;
			}
			return null;
		}

		// Token: 0x060011A6 RID: 4518 RVA: 0x0004B458 File Offset: 0x00049658
		public virtual IEnumerable<AssociationType> GetAllRelationshipTypesExpensiveWay(Assembly assembly)
		{
			Dictionary<string, EdmType> typesInLoading = this.LoadTypesExpensiveWay(assembly);
			if (typesInLoading != null)
			{
				foreach (EdmType edmType in typesInLoading.Values)
				{
					if (Helper.IsAssociationType(edmType))
					{
						yield return (AssociationType)edmType;
					}
				}
			}
			yield break;
		}
	}
}
