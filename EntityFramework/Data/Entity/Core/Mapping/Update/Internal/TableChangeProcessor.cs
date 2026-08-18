using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common;
using System.Data.Entity.Core.Common.Utils;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace System.Data.Entity.Core.Mapping.Update.Internal
{
	// Token: 0x02000410 RID: 1040
	internal class TableChangeProcessor
	{
		// Token: 0x06002645 RID: 9797 RVA: 0x000B5D76 File Offset: 0x000B3F76
		internal TableChangeProcessor(EntitySet table)
		{
			this.m_table = table;
			this.m_keyOrdinals = TableChangeProcessor.InitializeKeyOrdinals(table);
		}

		// Token: 0x06002646 RID: 9798 RVA: 0x000B5D91 File Offset: 0x000B3F91
		protected TableChangeProcessor()
		{
		}

		// Token: 0x17000543 RID: 1347
		// (get) Token: 0x06002647 RID: 9799 RVA: 0x000B5D99 File Offset: 0x000B3F99
		internal EntitySet Table
		{
			get
			{
				return this.m_table;
			}
		}

		// Token: 0x17000544 RID: 1348
		// (get) Token: 0x06002648 RID: 9800 RVA: 0x000B5DA1 File Offset: 0x000B3FA1
		internal int[] KeyOrdinals
		{
			get
			{
				return this.m_keyOrdinals;
			}
		}

		// Token: 0x06002649 RID: 9801 RVA: 0x000B5DAC File Offset: 0x000B3FAC
		internal bool IsKeyProperty(int propertyOrdinal)
		{
			foreach (int num in this.m_keyOrdinals)
			{
				if (propertyOrdinal == num)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600264A RID: 9802 RVA: 0x000B5DE0 File Offset: 0x000B3FE0
		private static int[] InitializeKeyOrdinals(EntitySet table)
		{
			EntityType elementType = table.ElementType;
			IList<EdmMember> keyMembers = elementType.KeyMembers;
			IBaseList<EdmMember> allStructuralMembers = TypeHelpers.GetAllStructuralMembers(elementType);
			int[] array = new int[keyMembers.Count];
			for (int i = 0; i < keyMembers.Count; i++)
			{
				EdmMember item = keyMembers[i];
				array[i] = allStructuralMembers.IndexOf(item);
			}
			return array;
		}

		// Token: 0x0600264B RID: 9803 RVA: 0x000B5E3C File Offset: 0x000B403C
		internal List<UpdateCommand> CompileCommands(ChangeNode changeNode, UpdateCompiler compiler)
		{
			Set<CompositeKey> set = new Set<CompositeKey>(compiler.m_translator.KeyComparer);
			Dictionary<CompositeKey, PropagatorResult> dictionary = this.ProcessKeys(compiler, changeNode.Deleted, set);
			Dictionary<CompositeKey, PropagatorResult> dictionary2 = this.ProcessKeys(compiler, changeNode.Inserted, set);
			List<UpdateCommand> list = new List<UpdateCommand>(dictionary.Count + dictionary2.Count);
			foreach (CompositeKey key in set)
			{
				PropagatorResult propagatorResult;
				bool flag = dictionary.TryGetValue(key, out propagatorResult);
				PropagatorResult propagatorResult2;
				bool flag2 = dictionary2.TryGetValue(key, out propagatorResult2);
				try
				{
					if (!flag)
					{
						list.Add(compiler.BuildInsertCommand(propagatorResult2, this));
					}
					else if (!flag2)
					{
						list.Add(compiler.BuildDeleteCommand(propagatorResult, this));
					}
					else
					{
						UpdateCommand updateCommand = compiler.BuildUpdateCommand(propagatorResult, propagatorResult2, this);
						if (updateCommand != null)
						{
							list.Add(updateCommand);
						}
					}
				}
				catch (Exception ex)
				{
					if (ex.RequiresContext())
					{
						List<IEntityStateEntry> list2 = new List<IEntityStateEntry>();
						if (propagatorResult != null)
						{
							list2.AddRange(SourceInterpreter.GetAllStateEntries(propagatorResult, compiler.m_translator, this.m_table));
						}
						if (propagatorResult2 != null)
						{
							list2.AddRange(SourceInterpreter.GetAllStateEntries(propagatorResult2, compiler.m_translator, this.m_table));
						}
						throw new UpdateException(Strings.Update_GeneralExecutionException, ex, list2.Cast<ObjectStateEntry>().Distinct<ObjectStateEntry>());
					}
					throw;
				}
			}
			return list;
		}

		// Token: 0x0600264C RID: 9804 RVA: 0x000B5FA0 File Offset: 0x000B41A0
		private Dictionary<CompositeKey, PropagatorResult> ProcessKeys(UpdateCompiler compiler, List<PropagatorResult> changes, Set<CompositeKey> keys)
		{
			Dictionary<CompositeKey, PropagatorResult> dictionary = new Dictionary<CompositeKey, PropagatorResult>(compiler.m_translator.KeyComparer);
			foreach (PropagatorResult propagatorResult in changes)
			{
				PropagatorResult propagatorResult2 = propagatorResult;
				CompositeKey compositeKey = new CompositeKey(this.GetKeyConstants(propagatorResult2));
				PropagatorResult other;
				if (dictionary.TryGetValue(compositeKey, out other))
				{
					this.DiagnoseKeyCollision(compiler, propagatorResult, compositeKey, other);
				}
				dictionary.Add(compositeKey, propagatorResult2);
				keys.Add(compositeKey);
			}
			return dictionary;
		}

		// Token: 0x0600264D RID: 9805 RVA: 0x000B6030 File Offset: 0x000B4230
		[SuppressMessage("Microsoft.Security", "CA2140:TransparentMethodsMustNotReferenceCriticalCode", Justification = "Based on Bug VSTS Pioneer #433188: IsVisibleOutsideAssembly is wrong on generic instantiations.")]
		private void DiagnoseKeyCollision(UpdateCompiler compiler, PropagatorResult change, CompositeKey key, PropagatorResult other)
		{
			KeyManager keyManager = compiler.m_translator.KeyManager;
			CompositeKey compositeKey = new CompositeKey(this.GetKeyConstants(other));
			bool flag = true;
			int num = 0;
			while (flag && num < key.KeyComponents.Length)
			{
				int identifier = key.KeyComponents[num].Identifier;
				int identifier2 = compositeKey.KeyComponents[num].Identifier;
				if (!keyManager.GetPrincipals(identifier).Intersect(keyManager.GetPrincipals(identifier2)).Any<int>())
				{
					flag = false;
				}
				num++;
			}
			if (flag)
			{
				IEnumerable<IEntityStateEntry> source = SourceInterpreter.GetAllStateEntries(change, compiler.m_translator, this.m_table).Concat(SourceInterpreter.GetAllStateEntries(other, compiler.m_translator, this.m_table));
				throw new UpdateException(Strings.Update_DuplicateKeys, null, source.Cast<ObjectStateEntry>().Distinct<ObjectStateEntry>());
			}
			HashSet<IEntityStateEntry> hashSet = null;
			foreach (PropagatorResult propagatorResult in key.KeyComponents.Concat(compositeKey.KeyComponents))
			{
				HashSet<IEntityStateEntry> hashSet2 = new HashSet<IEntityStateEntry>();
				foreach (int identifier3 in keyManager.GetDependents(propagatorResult.Identifier))
				{
					PropagatorResult propagatorResult2;
					if (keyManager.TryGetIdentifierOwner(identifier3, out propagatorResult2) && propagatorResult2.StateEntry != null)
					{
						hashSet2.Add(propagatorResult2.StateEntry);
					}
				}
				if (hashSet == null)
				{
					hashSet = new HashSet<IEntityStateEntry>(hashSet2);
				}
				else
				{
					hashSet.IntersectWith(hashSet2);
				}
			}
			throw new UpdateException(Strings.Update_GeneralExecutionException, new ConstraintException(Strings.Update_ReferentialConstraintIntegrityViolation), hashSet.Cast<ObjectStateEntry>().Distinct<ObjectStateEntry>());
		}

		// Token: 0x0600264E RID: 9806 RVA: 0x000B61EC File Offset: 0x000B43EC
		private PropagatorResult[] GetKeyConstants(PropagatorResult row)
		{
			PropagatorResult[] array = new PropagatorResult[this.m_keyOrdinals.Length];
			for (int i = 0; i < this.m_keyOrdinals.Length; i++)
			{
				PropagatorResult memberValue = row.GetMemberValue(this.m_keyOrdinals[i]);
				array[i] = memberValue;
			}
			return array;
		}

		// Token: 0x04000E53 RID: 3667
		private readonly EntitySet m_table;

		// Token: 0x04000E54 RID: 3668
		private readonly int[] m_keyOrdinals;
	}
}
