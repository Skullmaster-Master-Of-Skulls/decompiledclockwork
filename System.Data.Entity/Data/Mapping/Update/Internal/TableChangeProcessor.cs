using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Common.Utils;
using System.Data.Entity;
using System.Data.Metadata.Edm;
using System.Linq;

namespace System.Data.Mapping.Update.Internal
{
	// Token: 0x020002CE RID: 718
	internal class TableChangeProcessor
	{
		// Token: 0x06002A47 RID: 10823 RVA: 0x000A5DFA File Offset: 0x000A3FFA
		internal TableChangeProcessor(EntitySet table)
		{
			EntityUtil.CheckArgumentNull<EntitySet>(table, "table");
			this.m_table = table;
			this.m_keyOrdinals = TableChangeProcessor.InitializeKeyOrdinals(table);
		}

		// Token: 0x17000844 RID: 2116
		// (get) Token: 0x06002A48 RID: 10824 RVA: 0x000A5E21 File Offset: 0x000A4021
		internal EntitySet Table
		{
			get
			{
				return this.m_table;
			}
		}

		// Token: 0x17000845 RID: 2117
		// (get) Token: 0x06002A49 RID: 10825 RVA: 0x000A5E29 File Offset: 0x000A4029
		internal int[] KeyOrdinals
		{
			get
			{
				return this.m_keyOrdinals;
			}
		}

		// Token: 0x06002A4A RID: 10826 RVA: 0x000A5E34 File Offset: 0x000A4034
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

		// Token: 0x06002A4B RID: 10827 RVA: 0x000A5E64 File Offset: 0x000A4064
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

		// Token: 0x06002A4C RID: 10828 RVA: 0x000A5EC0 File Offset: 0x000A40C0
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
					if (UpdateTranslator.RequiresContext(ex))
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
						throw EntityUtil.Update(Strings.Update_GeneralExecutionException, ex, list2);
					}
					throw;
				}
			}
			return list;
		}

		// Token: 0x06002A4D RID: 10829 RVA: 0x000A601C File Offset: 0x000A421C
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

		// Token: 0x06002A4E RID: 10830 RVA: 0x000A60B0 File Offset: 0x000A42B0
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
				IEnumerable<IEntityStateEntry> stateEntries = SourceInterpreter.GetAllStateEntries(change, compiler.m_translator, this.m_table).Concat(SourceInterpreter.GetAllStateEntries(other, compiler.m_translator, this.m_table));
				throw EntityUtil.Update(Strings.Update_DuplicateKeys, null, stateEntries);
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
			throw EntityUtil.Update(Strings.Update_GeneralExecutionException, EntityUtil.Constraint(Strings.Update_ReferentialConstraintIntegrityViolation), hashSet);
		}

		// Token: 0x06002A4F RID: 10831 RVA: 0x000A6258 File Offset: 0x000A4458
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

		// Token: 0x040012DA RID: 4826
		private readonly EntitySet m_table;

		// Token: 0x040012DB RID: 4827
		private readonly int[] m_keyOrdinals;
	}
}
