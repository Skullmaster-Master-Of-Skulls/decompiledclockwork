using System;
using System.Collections.Generic;
using System.Globalization;
using System.Security.Cryptography;

namespace System.Data.Entity.Core.Mapping
{
	// Token: 0x020003A9 RID: 937
	internal class CompressingHashBuilder : StringHashBuilder
	{
		// Token: 0x0600221D RID: 8733 RVA: 0x0009F5BD File Offset: 0x0009D7BD
		internal CompressingHashBuilder(HashAlgorithm hashAlgorithm) : base(hashAlgorithm, 6144)
		{
		}

		// Token: 0x0600221E RID: 8734 RVA: 0x0009F5CB File Offset: 0x0009D7CB
		internal override void Append(string content)
		{
			base.Append(string.Empty.PadLeft(4 * this._indent, ' '));
			base.Append(content);
			this.CompressHash();
		}

		// Token: 0x0600221F RID: 8735 RVA: 0x0009F5F4 File Offset: 0x0009D7F4
		internal override void AppendLine(string content)
		{
			base.Append(string.Empty.PadLeft(4 * this._indent, ' '));
			base.AppendLine(content);
			this.CompressHash();
		}

		// Token: 0x06002220 RID: 8736 RVA: 0x0009F620 File Offset: 0x0009D820
		private static Dictionary<Type, string> InitializeLegacyTypeNames()
		{
			return new Dictionary<Type, string>
			{
				{
					typeof(AssociationSetMapping),
					"System.Data.Entity.Core.Mapping.StorageAssociationSetMapping"
				},
				{
					typeof(AssociationSetModificationFunctionMapping),
					"System.Data.Entity.Core.Mapping.StorageAssociationSetModificationFunctionMapping"
				},
				{
					typeof(AssociationTypeMapping),
					"System.Data.Entity.Core.Mapping.StorageAssociationTypeMapping"
				},
				{
					typeof(ComplexPropertyMapping),
					"System.Data.Entity.Core.Mapping.StorageComplexPropertyMapping"
				},
				{
					typeof(ComplexTypeMapping),
					"System.Data.Entity.Core.Mapping.StorageComplexTypeMapping"
				},
				{
					typeof(ConditionPropertyMapping),
					"System.Data.Entity.Core.Mapping.StorageConditionPropertyMapping"
				},
				{
					typeof(EndPropertyMapping),
					"System.Data.Entity.Core.Mapping.StorageEndPropertyMapping"
				},
				{
					typeof(EntityContainerMapping),
					"System.Data.Entity.Core.Mapping.StorageEntityContainerMapping"
				},
				{
					typeof(EntitySetMapping),
					"System.Data.Entity.Core.Mapping.StorageEntitySetMapping"
				},
				{
					typeof(EntityTypeMapping),
					"System.Data.Entity.Core.Mapping.StorageEntityTypeMapping"
				},
				{
					typeof(EntityTypeModificationFunctionMapping),
					"System.Data.Entity.Core.Mapping.StorageEntityTypeModificationFunctionMapping"
				},
				{
					typeof(MappingFragment),
					"System.Data.Entity.Core.Mapping.StorageMappingFragment"
				},
				{
					typeof(ModificationFunctionMapping),
					"System.Data.Entity.Core.Mapping.StorageModificationFunctionMapping"
				},
				{
					typeof(ModificationFunctionMemberPath),
					"System.Data.Entity.Core.Mapping.StorageModificationFunctionMemberPath"
				},
				{
					typeof(ModificationFunctionParameterBinding),
					"System.Data.Entity.Core.Mapping.StorageModificationFunctionParameterBinding"
				},
				{
					typeof(ModificationFunctionResultBinding),
					"System.Data.Entity.Core.Mapping.StorageModificationFunctionResultBinding"
				},
				{
					typeof(PropertyMapping),
					"System.Data.Entity.Core.Mapping.StoragePropertyMapping"
				},
				{
					typeof(ScalarPropertyMapping),
					"System.Data.Entity.Core.Mapping.StorageScalarPropertyMapping"
				},
				{
					typeof(EntitySetBaseMapping),
					"System.Data.Entity.Core.Mapping.StorageSetMapping"
				},
				{
					typeof(TypeMapping),
					"System.Data.Entity.Core.Mapping.StorageTypeMapping"
				}
			};
		}

		// Token: 0x06002221 RID: 8737 RVA: 0x0009F7D8 File Offset: 0x0009D9D8
		internal void AppendObjectStartDump(object o, int objectIndex)
		{
			base.Append(string.Empty.PadLeft(4 * this._indent, ' '));
			string s;
			if (!CompressingHashBuilder._legacyTypeNames.TryGetValue(o.GetType(), out s))
			{
				s = o.GetType().ToString();
			}
			base.Append(s);
			base.Append(" Instance#");
			base.AppendLine(objectIndex.ToString(CultureInfo.InvariantCulture));
			this.CompressHash();
			this._indent++;
		}

		// Token: 0x06002222 RID: 8738 RVA: 0x0009F857 File Offset: 0x0009DA57
		internal void AppendObjectEndDump()
		{
			this._indent--;
		}

		// Token: 0x06002223 RID: 8739 RVA: 0x0009F868 File Offset: 0x0009DA68
		private void CompressHash()
		{
			if (base.CharCount >= 2048)
			{
				string s = base.ComputeHash();
				base.Clear();
				base.Append(s);
			}
		}

		// Token: 0x04000C09 RID: 3081
		private const int HashCharacterCompressionThreshold = 2048;

		// Token: 0x04000C0A RID: 3082
		private const int SpacesPerIndent = 4;

		// Token: 0x04000C0B RID: 3083
		private int _indent;

		// Token: 0x04000C0C RID: 3084
		private static readonly Dictionary<Type, string> _legacyTypeNames = CompressingHashBuilder.InitializeLegacyTypeNames();
	}
}
