using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;
using System.Linq;

namespace System.Data.Entity.Core.Mapping
{
	// Token: 0x020003DE RID: 990
	public class MappingFragment : StructuralTypeMapping
	{
		// Token: 0x0600243A RID: 9274 RVA: 0x000A6BC8 File Offset: 0x000A4DC8
		public MappingFragment(EntitySet storeEntitySet, TypeMapping typeMapping, bool makeColumnsDistinct)
		{
			Check.NotNull<EntitySet>(storeEntitySet, "storeEntitySet");
			Check.NotNull<TypeMapping>(typeMapping, "typeMapping");
			this.m_tableExtent = storeEntitySet;
			this.m_typeMapping = typeMapping;
			this.m_isSQueryDistinct = makeColumnsDistinct;
		}

		// Token: 0x170004ED RID: 1261
		// (get) Token: 0x0600243B RID: 9275 RVA: 0x000A6C2E File Offset: 0x000A4E2E
		internal IEnumerable<ColumnMappingBuilder> ColumnMappings
		{
			get
			{
				return this._columnMappings;
			}
		}

		// Token: 0x0600243C RID: 9276 RVA: 0x000A6C64 File Offset: 0x000A4E64
		internal void AddColumnMapping(ColumnMappingBuilder columnMappingBuilder)
		{
			Check.NotNull<ColumnMappingBuilder>(columnMappingBuilder, "columnMappingBuilder");
			if (!columnMappingBuilder.PropertyPath.Any<EdmProperty>() || this._columnMappings.Contains(columnMappingBuilder))
			{
				throw new ArgumentException(Strings.InvalidColumnBuilderArgument("columnBuilderMapping"));
			}
			this._columnMappings.Add(columnMappingBuilder);
			StructuralTypeMapping structuralTypeMapping = this;
			EdmProperty property;
			int i;
			for (i = 0; i < columnMappingBuilder.PropertyPath.Count - 1; i++)
			{
				property = columnMappingBuilder.PropertyPath[i];
				ComplexPropertyMapping complexPropertyMapping = structuralTypeMapping.PropertyMappings.OfType<ComplexPropertyMapping>().SingleOrDefault((ComplexPropertyMapping pm) => object.ReferenceEquals(pm.Property, property));
				ComplexTypeMapping complexTypeMapping = null;
				if (complexPropertyMapping == null)
				{
					complexTypeMapping = new ComplexTypeMapping(false);
					complexTypeMapping.AddType(property.ComplexType);
					complexPropertyMapping = new ComplexPropertyMapping(property);
					complexPropertyMapping.AddTypeMapping(complexTypeMapping);
					structuralTypeMapping.AddPropertyMapping(complexPropertyMapping);
				}
				structuralTypeMapping = (complexTypeMapping ?? complexPropertyMapping.TypeMappings.Single<ComplexTypeMapping>());
			}
			property = columnMappingBuilder.PropertyPath[i];
			ScalarPropertyMapping scalarPropertyMapping = structuralTypeMapping.PropertyMappings.OfType<ScalarPropertyMapping>().SingleOrDefault((ScalarPropertyMapping pm) => object.ReferenceEquals(pm.Property, property));
			if (scalarPropertyMapping == null)
			{
				scalarPropertyMapping = new ScalarPropertyMapping(property, columnMappingBuilder.ColumnProperty);
				structuralTypeMapping.AddPropertyMapping(scalarPropertyMapping);
				columnMappingBuilder.SetTarget(scalarPropertyMapping);
				return;
			}
			scalarPropertyMapping.Column = columnMappingBuilder.ColumnProperty;
		}

		// Token: 0x0600243D RID: 9277 RVA: 0x000A6DC9 File Offset: 0x000A4FC9
		internal void RemoveColumnMapping(ColumnMappingBuilder columnMappingBuilder)
		{
			this._columnMappings.Remove(columnMappingBuilder);
			MappingFragment.RemoveColumnMapping(this, columnMappingBuilder.PropertyPath);
		}

		// Token: 0x0600243E RID: 9278 RVA: 0x000A6E04 File Offset: 0x000A5004
		private static void RemoveColumnMapping(StructuralTypeMapping structuralTypeMapping, IEnumerable<EdmProperty> propertyPath)
		{
			PropertyMapping propertyMapping = structuralTypeMapping.PropertyMappings.Single((PropertyMapping pm) => object.ReferenceEquals(pm.Property, propertyPath.First<EdmProperty>()));
			if (propertyMapping is ScalarPropertyMapping)
			{
				structuralTypeMapping.RemovePropertyMapping(propertyMapping);
				return;
			}
			ComplexPropertyMapping complexPropertyMapping = (ComplexPropertyMapping)propertyMapping;
			ComplexTypeMapping complexTypeMapping = complexPropertyMapping.TypeMappings.Single<ComplexTypeMapping>();
			MappingFragment.RemoveColumnMapping(complexTypeMapping, propertyPath.Skip(1));
			if (!complexTypeMapping.PropertyMappings.Any<PropertyMapping>())
			{
				structuralTypeMapping.RemovePropertyMapping(complexPropertyMapping);
			}
		}

		// Token: 0x170004EE RID: 1262
		// (get) Token: 0x0600243F RID: 9279 RVA: 0x000A6E7F File Offset: 0x000A507F
		// (set) Token: 0x06002440 RID: 9280 RVA: 0x000A6E87 File Offset: 0x000A5087
		public EntitySet StoreEntitySet
		{
			get
			{
				return this.m_tableExtent;
			}
			internal set
			{
				this.m_tableExtent = value;
			}
		}

		// Token: 0x170004EF RID: 1263
		// (get) Token: 0x06002441 RID: 9281 RVA: 0x000A6E90 File Offset: 0x000A5090
		// (set) Token: 0x06002442 RID: 9282 RVA: 0x000A6E98 File Offset: 0x000A5098
		internal EntitySet TableSet
		{
			get
			{
				return this.StoreEntitySet;
			}
			set
			{
				this.StoreEntitySet = value;
			}
		}

		// Token: 0x170004F0 RID: 1264
		// (get) Token: 0x06002443 RID: 9283 RVA: 0x000A6EA1 File Offset: 0x000A50A1
		internal EntityType Table
		{
			get
			{
				return this.m_tableExtent.ElementType;
			}
		}

		// Token: 0x170004F1 RID: 1265
		// (get) Token: 0x06002444 RID: 9284 RVA: 0x000A6EAE File Offset: 0x000A50AE
		public TypeMapping TypeMapping
		{
			get
			{
				return this.m_typeMapping;
			}
		}

		// Token: 0x170004F2 RID: 1266
		// (get) Token: 0x06002445 RID: 9285 RVA: 0x000A6EB6 File Offset: 0x000A50B6
		public bool MakeColumnsDistinct
		{
			get
			{
				return this.m_isSQueryDistinct;
			}
		}

		// Token: 0x170004F3 RID: 1267
		// (get) Token: 0x06002446 RID: 9286 RVA: 0x000A6EBE File Offset: 0x000A50BE
		internal bool IsSQueryDistinct
		{
			get
			{
				return this.MakeColumnsDistinct;
			}
		}

		// Token: 0x170004F4 RID: 1268
		// (get) Token: 0x06002447 RID: 9287 RVA: 0x000A6EC8 File Offset: 0x000A50C8
		internal ReadOnlyCollection<PropertyMapping> AllProperties
		{
			get
			{
				List<PropertyMapping> list = new List<PropertyMapping>();
				list.AddRange(this.m_properties);
				list.AddRange(this.m_conditionProperties.Values);
				return new ReadOnlyCollection<PropertyMapping>(list);
			}
		}

		// Token: 0x170004F5 RID: 1269
		// (get) Token: 0x06002448 RID: 9288 RVA: 0x000A6EFE File Offset: 0x000A50FE
		public override ReadOnlyCollection<PropertyMapping> PropertyMappings
		{
			get
			{
				return new ReadOnlyCollection<PropertyMapping>(this.m_properties);
			}
		}

		// Token: 0x170004F6 RID: 1270
		// (get) Token: 0x06002449 RID: 9289 RVA: 0x000A6F0B File Offset: 0x000A510B
		public override ReadOnlyCollection<ConditionPropertyMapping> Conditions
		{
			get
			{
				return new ReadOnlyCollection<ConditionPropertyMapping>(new List<ConditionPropertyMapping>(this.m_conditionProperties.Values));
			}
		}

		// Token: 0x170004F7 RID: 1271
		// (get) Token: 0x0600244A RID: 9290 RVA: 0x000A6F22 File Offset: 0x000A5122
		internal IEnumerable<ColumnMappingBuilder> FlattenedProperties
		{
			get
			{
				return MappingFragment.GetFlattenedProperties(this.m_properties, new List<EdmProperty>());
			}
		}

		// Token: 0x0600244B RID: 9291 RVA: 0x000A7220 File Offset: 0x000A5420
		private static IEnumerable<ColumnMappingBuilder> GetFlattenedProperties(IEnumerable<PropertyMapping> propertyMappings, List<EdmProperty> propertyPath)
		{
			foreach (PropertyMapping propertyMapping in propertyMappings)
			{
				propertyPath.Add(propertyMapping.Property);
				ComplexPropertyMapping storageComplexPropertyMapping = propertyMapping as ComplexPropertyMapping;
				if (storageComplexPropertyMapping != null)
				{
					foreach (ColumnMappingBuilder columnMappingBuilder in MappingFragment.GetFlattenedProperties(storageComplexPropertyMapping.TypeMappings.Single<ComplexTypeMapping>().PropertyMappings, propertyPath))
					{
						yield return columnMappingBuilder;
					}
				}
				else
				{
					ScalarPropertyMapping storageScalarPropertyMapping = propertyMapping as ScalarPropertyMapping;
					if (storageScalarPropertyMapping != null)
					{
						yield return new ColumnMappingBuilder(storageScalarPropertyMapping.Column, propertyPath.ToList<EdmProperty>());
					}
				}
				propertyPath.Remove(propertyMapping.Property);
			}
			yield break;
		}

		// Token: 0x170004F8 RID: 1272
		// (get) Token: 0x0600244C RID: 9292 RVA: 0x000A7244 File Offset: 0x000A5444
		internal IEnumerable<ConditionPropertyMapping> ColumnConditions
		{
			get
			{
				return this.m_conditionProperties.Values;
			}
		}

		// Token: 0x170004F9 RID: 1273
		// (get) Token: 0x0600244D RID: 9293 RVA: 0x000A7251 File Offset: 0x000A5451
		// (set) Token: 0x0600244E RID: 9294 RVA: 0x000A7259 File Offset: 0x000A5459
		internal int StartLineNumber { get; set; }

		// Token: 0x170004FA RID: 1274
		// (get) Token: 0x0600244F RID: 9295 RVA: 0x000A7262 File Offset: 0x000A5462
		// (set) Token: 0x06002450 RID: 9296 RVA: 0x000A726A File Offset: 0x000A546A
		internal int StartLinePosition { get; set; }

		// Token: 0x170004FB RID: 1275
		// (get) Token: 0x06002451 RID: 9297 RVA: 0x000A7273 File Offset: 0x000A5473
		internal string SourceLocation
		{
			get
			{
				return this.m_typeMapping.SetMapping.EntityContainerMapping.SourceLocation;
			}
		}

		// Token: 0x06002452 RID: 9298 RVA: 0x000A728A File Offset: 0x000A548A
		public override void AddPropertyMapping(PropertyMapping propertyMapping)
		{
			Check.NotNull<PropertyMapping>(propertyMapping, "propertyMapping");
			base.ThrowIfReadOnly();
			this.m_properties.Add(propertyMapping);
		}

		// Token: 0x06002453 RID: 9299 RVA: 0x000A72AA File Offset: 0x000A54AA
		public override void RemovePropertyMapping(PropertyMapping propertyMapping)
		{
			Check.NotNull<PropertyMapping>(propertyMapping, "propertyMapping");
			base.ThrowIfReadOnly();
			this.m_properties.Remove(propertyMapping);
		}

		// Token: 0x06002454 RID: 9300 RVA: 0x000A72CB File Offset: 0x000A54CB
		public override void AddCondition(ConditionPropertyMapping condition)
		{
			Check.NotNull<ConditionPropertyMapping>(condition, "condition");
			base.ThrowIfReadOnly();
			this.AddConditionProperty(condition);
		}

		// Token: 0x06002455 RID: 9301 RVA: 0x000A72E6 File Offset: 0x000A54E6
		public override void RemoveCondition(ConditionPropertyMapping condition)
		{
			Check.NotNull<ConditionPropertyMapping>(condition, "condition");
			base.ThrowIfReadOnly();
			this.RemoveConditionProperty(condition);
		}

		// Token: 0x06002456 RID: 9302 RVA: 0x000A7301 File Offset: 0x000A5501
		internal void ClearConditions()
		{
			this.m_conditionProperties.Clear();
		}

		// Token: 0x06002457 RID: 9303 RVA: 0x000A730E File Offset: 0x000A550E
		internal override void SetReadOnly()
		{
			this.m_properties.TrimExcess();
			MappingItem.SetReadOnly(this.m_properties);
			MappingItem.SetReadOnly(this.m_conditionProperties.Values);
			base.SetReadOnly();
		}

		// Token: 0x06002458 RID: 9304 RVA: 0x000A733C File Offset: 0x000A553C
		internal void RemoveConditionProperty(ConditionPropertyMapping condition)
		{
			EdmProperty key = condition.Property ?? condition.Column;
			this.m_conditionProperties.Remove(key);
		}

		// Token: 0x06002459 RID: 9305 RVA: 0x000A7369 File Offset: 0x000A5569
		internal void AddConditionProperty(ConditionPropertyMapping conditionPropertyMap)
		{
			this.AddConditionProperty(conditionPropertyMap, delegate(EdmMember _)
			{
			});
		}

		// Token: 0x0600245A RID: 9306 RVA: 0x000A7390 File Offset: 0x000A5590
		internal void AddConditionProperty(ConditionPropertyMapping conditionPropertyMap, Action<EdmMember> duplicateMemberConditionError)
		{
			EdmProperty edmProperty = conditionPropertyMap.Property ?? conditionPropertyMap.Column;
			if (!this.m_conditionProperties.ContainsKey(edmProperty))
			{
				this.m_conditionProperties.Add(edmProperty, conditionPropertyMap);
				return;
			}
			duplicateMemberConditionError(edmProperty);
		}

		// Token: 0x04000D1F RID: 3359
		private readonly List<ColumnMappingBuilder> _columnMappings = new List<ColumnMappingBuilder>();

		// Token: 0x04000D20 RID: 3360
		private EntitySet m_tableExtent;

		// Token: 0x04000D21 RID: 3361
		private readonly TypeMapping m_typeMapping;

		// Token: 0x04000D22 RID: 3362
		private readonly Dictionary<EdmProperty, ConditionPropertyMapping> m_conditionProperties = new Dictionary<EdmProperty, ConditionPropertyMapping>(EqualityComparer<EdmProperty>.Default);

		// Token: 0x04000D23 RID: 3363
		private readonly List<PropertyMapping> m_properties = new List<PropertyMapping>();

		// Token: 0x04000D24 RID: 3364
		private readonly bool m_isSQueryDistinct;
	}
}
