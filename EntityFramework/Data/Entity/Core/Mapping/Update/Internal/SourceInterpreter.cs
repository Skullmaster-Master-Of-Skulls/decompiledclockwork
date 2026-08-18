using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Core.Metadata.Edm;

namespace System.Data.Entity.Core.Mapping.Update.Internal
{
	// Token: 0x0200040F RID: 1039
	internal class SourceInterpreter
	{
		// Token: 0x06002641 RID: 9793 RVA: 0x000B5BD8 File Offset: 0x000B3DD8
		private SourceInterpreter(UpdateTranslator translator, EntitySet sourceTable)
		{
			this.m_stateEntries = new List<IEntityStateEntry>();
			this.m_translator = translator;
			this.m_sourceTable = sourceTable;
		}

		// Token: 0x06002642 RID: 9794 RVA: 0x000B5BFC File Offset: 0x000B3DFC
		internal static ReadOnlyCollection<IEntityStateEntry> GetAllStateEntries(PropagatorResult source, UpdateTranslator translator, EntitySet sourceTable)
		{
			SourceInterpreter sourceInterpreter = new SourceInterpreter(translator, sourceTable);
			sourceInterpreter.RetrieveResultMarkup(source);
			return new ReadOnlyCollection<IEntityStateEntry>(sourceInterpreter.m_stateEntries);
		}

		// Token: 0x06002643 RID: 9795 RVA: 0x000B5C24 File Offset: 0x000B3E24
		private void RetrieveResultMarkup(PropagatorResult source)
		{
			if (source.Identifier != -1)
			{
				do
				{
					if (source.StateEntry != null)
					{
						this.m_stateEntries.Add(source.StateEntry);
						if (source.Identifier != -1)
						{
							PropagatorResult propagatorResult;
							if (this.m_translator.KeyManager.TryGetIdentifierOwner(source.Identifier, out propagatorResult) && propagatorResult.StateEntry != null && this.ExtentInScope(propagatorResult.StateEntry.EntitySet))
							{
								this.m_stateEntries.Add(propagatorResult.StateEntry);
							}
							foreach (IEntityStateEntry item in this.m_translator.KeyManager.GetDependentStateEntries(source.Identifier))
							{
								this.m_stateEntries.Add(item);
							}
						}
					}
					source = source.Next;
				}
				while (source != null);
				return;
			}
			if (!source.IsSimple && !source.IsNull)
			{
				foreach (PropagatorResult source2 in source.GetMemberValues())
				{
					this.RetrieveResultMarkup(source2);
				}
			}
		}

		// Token: 0x06002644 RID: 9796 RVA: 0x000B5D48 File Offset: 0x000B3F48
		private bool ExtentInScope(EntitySetBase extent)
		{
			return extent != null && this.m_translator.ViewLoader.GetAffectedTables(extent, this.m_translator.MetadataWorkspace).Contains(this.m_sourceTable);
		}

		// Token: 0x04000E50 RID: 3664
		private readonly List<IEntityStateEntry> m_stateEntries;

		// Token: 0x04000E51 RID: 3665
		private readonly UpdateTranslator m_translator;

		// Token: 0x04000E52 RID: 3666
		private readonly EntitySet m_sourceTable;
	}
}
