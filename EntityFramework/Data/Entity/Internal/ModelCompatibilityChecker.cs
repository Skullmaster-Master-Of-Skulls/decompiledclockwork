using System;
using System.Data.Entity.Migrations.Infrastructure;
using System.Data.Entity.Resources;

namespace System.Data.Entity.Internal
{
	// Token: 0x020006C6 RID: 1734
	internal class ModelCompatibilityChecker
	{
		// Token: 0x060044DB RID: 17627 RVA: 0x00144EA0 File Offset: 0x001430A0
		public virtual bool CompatibleWithModel(InternalContext internalContext, ModelHashCalculator modelHashCalculator, bool throwIfNoMetadata, DatabaseExistenceState existenceState = DatabaseExistenceState.Unknown)
		{
			if (internalContext.CodeFirstModel == null)
			{
				if (throwIfNoMetadata)
				{
					throw Error.Database_NonCodeFirstCompatibilityCheck();
				}
				return true;
			}
			else
			{
				VersionedModel versionedModel = internalContext.QueryForModel(existenceState);
				if (versionedModel != null)
				{
					return internalContext.ModelMatches(versionedModel);
				}
				string text = internalContext.QueryForModelHash();
				if (text != null)
				{
					return string.Equals(text, modelHashCalculator.Calculate(internalContext.CodeFirstModel), StringComparison.Ordinal);
				}
				if (throwIfNoMetadata)
				{
					throw Error.Database_NoDatabaseMetadata();
				}
				return true;
			}
		}
	}
}
