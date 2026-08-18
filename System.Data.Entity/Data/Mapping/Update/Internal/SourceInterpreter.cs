using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Metadata.Edm;

namespace System.Data.Mapping.Update.Internal
{
	// Token: 0x020002CD RID: 717
	internal class SourceInterpreter
	{
		// Token: 0x06002A43 RID: 10819 RVA: 0x000A5C5D File Offset: 0x000A3E5D
		private SourceInterpreter(UpdateTranslator translator, EntitySet sourceTable)
		{
			this.m_stateEntries = new List<IEntityStateEntry>();
			this.m_translator = translator;
			this.m_sourceTable = sourceTable;
		}

		// Token: 0x06002A44 RID: 10820 RVA: 0x000A5C80 File Offset: 0x000A3E80
		internal static ReadOnlyCollection<IEntityStateEntry> GetAllStateEntries(PropagatorResult source, UpdateTranslator translator, EntitySet sourceTable)
		{
			SourceInterpreter sourceInterpreter = new SourceInterpreter(translator, sourceTable);
			sourceInterpreter.RetrieveResultMarkup(source);
			return new ReadOnlyCollection<IEntityStateEntry>(sourceInterpreter.m_stateEntries);
		}

		// Token: 0x06002A45 RID: 10821 RVA: 0x000A5CA8 File Offset: 0x000A3EA8
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

		// Token: 0x06002A46 RID: 10822 RVA: 0x000A5DCC File Offset: 0x000A3FCC
		private bool ExtentInScope(EntitySetBase extent)
		{
			return extent != null && this.m_translator.ViewLoader.GetAffectedTables(extent, this.m_translator.MetadataWorkspace).Contains(this.m_sourceTable);
		}

		// Token: 0x040012D7 RID: 4823
		private readonly List<IEntityStateEntry> m_stateEntries;

		// Token: 0x040012D8 RID: 4824
		private readonly UpdateTranslator m_translator;

		// Token: 0x040012D9 RID: 4825
		private readonly EntitySet m_sourceTable;
	}
}
