using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common;
using System.Data.Entity.Core.Mapping;
using System.Data.Entity.Core.Mapping.Update.Internal;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Resources;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace System.Data.Entity.ModelConfiguration.Edm.Services
{
	// Token: 0x020002CB RID: 715
	internal class FunctionParameterMappingGenerator : StructuralTypeMappingGenerator
	{
		// Token: 0x0600194A RID: 6474 RVA: 0x0007DE10 File Offset: 0x0007C010
		public FunctionParameterMappingGenerator(DbProviderManifest providerManifest) : base(providerManifest)
		{
		}

		// Token: 0x0600194B RID: 6475 RVA: 0x0007E370 File Offset: 0x0007C570
		public IEnumerable<ModificationFunctionParameterBinding> Generate(ModificationOperator modificationOperator, IEnumerable<EdmProperty> properties, IList<ColumnMappingBuilder> columnMappings, IList<EdmProperty> propertyPath, bool useOriginalValues = false)
		{
			using (IEnumerator<EdmProperty> enumerator = properties.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					EdmProperty property = enumerator.Current;
					if (property.IsComplexType)
					{
						if (propertyPath.Any((EdmProperty p) => p.IsComplexType && p.ComplexType == property.ComplexType))
						{
							throw Error.CircularComplexTypeHierarchy();
						}
					}
					propertyPath.Add(property);
					if (property.IsComplexType)
					{
						foreach (ModificationFunctionParameterBinding parameterBinding in this.Generate(modificationOperator, property.ComplexType.Properties, columnMappings, propertyPath, useOriginalValues))
						{
							yield return parameterBinding;
						}
					}
					else if (property.GetStoreGeneratedPattern() != StoreGeneratedPattern.Identity || modificationOperator != ModificationOperator.Insert)
					{
						EdmProperty columnProperty = columnMappings.First((ColumnMappingBuilder cm) => cm.PropertyPath.SequenceEqual(propertyPath)).ColumnProperty;
						if (property.GetStoreGeneratedPattern() != StoreGeneratedPattern.Computed && (modificationOperator != ModificationOperator.Delete || property.IsKeyMember))
						{
							yield return new ModificationFunctionParameterBinding(new FunctionParameter(columnProperty.Name, columnProperty.TypeUsage, ParameterMode.In), new ModificationFunctionMemberPath(propertyPath, null), !useOriginalValues);
						}
						if (modificationOperator != ModificationOperator.Insert && property.ConcurrencyMode == ConcurrencyMode.Fixed)
						{
							yield return new ModificationFunctionParameterBinding(new FunctionParameter(columnProperty.Name + "_Original", columnProperty.TypeUsage, ParameterMode.In), new ModificationFunctionMemberPath(propertyPath, null), false);
						}
					}
					propertyPath.Remove(property);
				}
			}
			yield break;
		}

		// Token: 0x0600194C RID: 6476 RVA: 0x0007E518 File Offset: 0x0007C718
		[SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic")]
		public IEnumerable<ModificationFunctionParameterBinding> Generate(IEnumerable<Tuple<ModificationFunctionMemberPath, EdmProperty>> iaFkProperties, bool useOriginalValues = false)
		{
			return from iaFkProperty in iaFkProperties
			let functionParameter = new FunctionParameter(iaFkProperty.Item2.Name, iaFkProperty.Item2.TypeUsage, ParameterMode.In)
			select new ModificationFunctionParameterBinding(functionParameter, iaFkProperty.Item1, !useOriginalValues);
		}
	}
}
