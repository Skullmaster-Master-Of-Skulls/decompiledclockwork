using System;

namespace System.Data.Entity.Core.Metadata.Edm
{
	// Token: 0x0200013B RID: 315
	internal static class RelationshipMultiplicityConverter
	{
		// Token: 0x06000A8D RID: 2701 RVA: 0x00035F28 File Offset: 0x00034128
		internal static string MultiplicityToString(RelationshipMultiplicity multiplicity)
		{
			switch (multiplicity)
			{
			case RelationshipMultiplicity.ZeroOrOne:
				return "0..1";
			case RelationshipMultiplicity.One:
				return "1";
			case RelationshipMultiplicity.Many:
				return "*";
			default:
				return string.Empty;
			}
		}

		// Token: 0x06000A8E RID: 2702 RVA: 0x00035F64 File Offset: 0x00034164
		internal static bool TryParseMultiplicity(string value, out RelationshipMultiplicity multiplicity)
		{
			if (value != null)
			{
				if (value == "*")
				{
					multiplicity = RelationshipMultiplicity.Many;
					return true;
				}
				if (value == "1")
				{
					multiplicity = RelationshipMultiplicity.One;
					return true;
				}
				if (value == "0..1")
				{
					multiplicity = RelationshipMultiplicity.ZeroOrOne;
					return true;
				}
			}
			multiplicity = (RelationshipMultiplicity)(-1);
			return false;
		}
	}
}
