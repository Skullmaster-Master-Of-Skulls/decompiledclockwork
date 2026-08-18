using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
using System.Data.Metadata.Edm;
using System.Data.Objects.DataClasses;
using System.Reflection;
using System.Reflection.Emit;

namespace System.Data.Objects.Internal
{
	// Token: 0x02000173 RID: 371
	internal class IPOCOImplementor
	{
		// Token: 0x06001B34 RID: 6964 RVA: 0x0005D344 File Offset: 0x0005B544
		public IPOCOImplementor(EntityType ospaceEntityType)
		{
			Type clrType = ospaceEntityType.ClrType;
			this._referenceProperties = new List<KeyValuePair<NavigationProperty, PropertyInfo>>();
			this._collectionProperties = new List<KeyValuePair<NavigationProperty, PropertyInfo>>();
			this._implementIEntityWithChangeTracker = (null == clrType.GetInterface(typeof(IEntityWithChangeTracker).Name));
			this._implementIEntityWithRelationships = (null == clrType.GetInterface(typeof(IEntityWithRelationships).Name));
			this.CheckType(ospaceEntityType);
			this._ospaceEntityType = ospaceEntityType;
		}

		// Token: 0x06001B35 RID: 6965 RVA: 0x0005D3C4 File Offset: 0x0005B5C4
		private void CheckType(EntityType ospaceEntityType)
		{
			this._scalarMembers = new HashSet<EdmMember>();
			this._relationshipMembers = new HashSet<EdmMember>();
			foreach (EdmMember edmMember in ospaceEntityType.Members)
			{
				PropertyInfo topProperty = EntityUtil.GetTopProperty(ospaceEntityType.ClrType, edmMember.Name);
				if (topProperty != null && EntityProxyFactory.CanProxySetter(topProperty))
				{
					if (edmMember.BuiltInTypeKind == BuiltInTypeKind.EdmProperty)
					{
						if (this._implementIEntityWithChangeTracker)
						{
							this._scalarMembers.Add(edmMember);
						}
					}
					else if (edmMember.BuiltInTypeKind == BuiltInTypeKind.NavigationProperty && this._implementIEntityWithRelationships)
					{
						NavigationProperty navigationProperty = (NavigationProperty)edmMember;
						RelationshipMultiplicity relationshipMultiplicity = navigationProperty.ToEndMember.RelationshipMultiplicity;
						if (relationshipMultiplicity == RelationshipMultiplicity.Many)
						{
							if (topProperty.PropertyType.IsGenericType && topProperty.PropertyType.GetGenericTypeDefinition() == typeof(ICollection<>))
							{
								this._relationshipMembers.Add(edmMember);
							}
						}
						else
						{
							this._relationshipMembers.Add(edmMember);
						}
					}
				}
			}
			if (ospaceEntityType.Members.Count != this._scalarMembers.Count + this._relationshipMembers.Count)
			{
				this._scalarMembers.Clear();
				this._relationshipMembers.Clear();
				this._implementIEntityWithChangeTracker = false;
				this._implementIEntityWithRelationships = false;
			}
		}

		// Token: 0x06001B36 RID: 6966 RVA: 0x0005D530 File Offset: 0x0005B730
		public void Implement(TypeBuilder typeBuilder, Action<FieldBuilder, bool> registerField)
		{
			if (this._implementIEntityWithChangeTracker)
			{
				this.ImplementIEntityWithChangeTracker(typeBuilder, registerField);
			}
			if (this._implementIEntityWithRelationships)
			{
				this.ImplementIEntityWithRelationships(typeBuilder, registerField);
			}
			this._resetFKSetterFlagField = typeBuilder.DefineField("_resetFKSetterFlag", typeof(Action<object>), FieldAttributes.Private | FieldAttributes.Static);
			this._compareByteArraysField = typeBuilder.DefineField("_compareByteArrays", typeof(Func<object, object, bool>), FieldAttributes.Private | FieldAttributes.Static);
		}

		// Token: 0x1700054C RID: 1356
		// (get) Token: 0x06001B37 RID: 6967 RVA: 0x0005D598 File Offset: 0x0005B798
		public Type[] Interfaces
		{
			get
			{
				List<Type> list = new List<Type>();
				if (this._implementIEntityWithChangeTracker)
				{
					list.Add(typeof(IEntityWithChangeTracker));
				}
				if (this._implementIEntityWithRelationships)
				{
					list.Add(typeof(IEntityWithRelationships));
				}
				return list.ToArray();
			}
		}

		// Token: 0x06001B38 RID: 6968 RVA: 0x0005D5E4 File Offset: 0x0005B7E4
		public DynamicMethod CreateInitalizeCollectionMethod(Type proxyType)
		{
			if (this._collectionProperties.Count > 0)
			{
				DynamicMethod dynamicMethod = LightweightCodeGenerator.CreateDynamicMethod(proxyType.Name + "_InitializeEntityCollections", typeof(IEntityWrapper), new Type[]
				{
					typeof(IEntityWrapper)
				});
				ILGenerator ilgenerator = dynamicMethod.GetILGenerator();
				ilgenerator.DeclareLocal(proxyType);
				ilgenerator.DeclareLocal(typeof(RelationshipManager));
				ilgenerator.Emit(OpCodes.Ldarg_0);
				ilgenerator.Emit(OpCodes.Callvirt, IPOCOImplementor.s_IEntityWrapper_GetEntity);
				ilgenerator.Emit(OpCodes.Castclass, proxyType);
				ilgenerator.Emit(OpCodes.Stloc_0);
				ilgenerator.Emit(OpCodes.Ldloc_0);
				ilgenerator.Emit(OpCodes.Callvirt, IPOCOImplementor.s_GetRelationshipManager);
				ilgenerator.Emit(OpCodes.Stloc_1);
				foreach (KeyValuePair<NavigationProperty, PropertyInfo> keyValuePair in this._collectionProperties)
				{
					MethodInfo meth = IPOCOImplementor.s_GetRelatedCollection.MakeGenericMethod(new Type[]
					{
						EntityUtil.GetCollectionElementType(keyValuePair.Value.PropertyType)
					});
					ilgenerator.Emit(OpCodes.Ldloc_0);
					ilgenerator.Emit(OpCodes.Ldloc_1);
					ilgenerator.Emit(OpCodes.Ldstr, keyValuePair.Key.RelationshipType.FullName);
					ilgenerator.Emit(OpCodes.Ldstr, keyValuePair.Key.ToEndMember.Name);
					ilgenerator.Emit(OpCodes.Callvirt, meth);
					ilgenerator.Emit(OpCodes.Callvirt, keyValuePair.Value.GetSetMethod(true));
				}
				ilgenerator.Emit(OpCodes.Ldarg_0);
				ilgenerator.Emit(OpCodes.Ret);
				return dynamicMethod;
			}
			return null;
		}

		// Token: 0x06001B39 RID: 6969 RVA: 0x0005D7A4 File Offset: 0x0005B9A4
		public bool CanProxyMember(EdmMember member)
		{
			return this._scalarMembers.Contains(member) || this._relationshipMembers.Contains(member);
		}

		// Token: 0x06001B3A RID: 6970 RVA: 0x0005D7C4 File Offset: 0x0005B9C4
		public bool EmitMember(TypeBuilder typeBuilder, EdmMember member, PropertyBuilder propertyBuilder, PropertyInfo baseProperty, BaseProxyImplementor baseImplementor)
		{
			if (this._scalarMembers.Contains(member))
			{
				bool isKeyMember = this._ospaceEntityType.KeyMembers.Contains(member.Identity);
				this.EmitScalarSetter(typeBuilder, propertyBuilder, baseProperty, isKeyMember);
				return true;
			}
			if (this._relationshipMembers.Contains(member))
			{
				NavigationProperty navigationProperty = member as NavigationProperty;
				if (navigationProperty.ToEndMember.RelationshipMultiplicity == RelationshipMultiplicity.Many)
				{
					this.EmitCollectionProperty(typeBuilder, propertyBuilder, baseProperty, navigationProperty);
				}
				else
				{
					this.EmitReferenceProperty(typeBuilder, propertyBuilder, baseProperty, navigationProperty);
				}
				baseImplementor.AddBasePropertySetter(baseProperty);
				return true;
			}
			return false;
		}

		// Token: 0x06001B3B RID: 6971 RVA: 0x0005D84C File Offset: 0x0005BA4C
		private void EmitScalarSetter(TypeBuilder typeBuilder, PropertyBuilder propertyBuilder, PropertyInfo baseProperty, bool isKeyMember)
		{
			MethodInfo setMethod = baseProperty.GetSetMethod(true);
			MethodAttributes methodAttributes = setMethod.Attributes & MethodAttributes.MemberAccessMask;
			MethodBuilder methodBuilder = typeBuilder.DefineMethod("set_" + baseProperty.Name, methodAttributes | (MethodAttributes.Virtual | MethodAttributes.HideBySig | MethodAttributes.SpecialName), null, new Type[]
			{
				baseProperty.PropertyType
			});
			ILGenerator ilgenerator = methodBuilder.GetILGenerator();
			Label label = ilgenerator.DefineLabel();
			if (isKeyMember)
			{
				MethodInfo getMethod = baseProperty.GetGetMethod(true);
				if (getMethod != null)
				{
					Type propertyType = baseProperty.PropertyType;
					if (propertyType == typeof(int) || propertyType == typeof(short) || propertyType == typeof(long) || propertyType == typeof(bool) || propertyType == typeof(byte) || propertyType == typeof(uint) || propertyType == typeof(ulong) || propertyType == typeof(float) || propertyType == typeof(double) || propertyType.IsEnum)
					{
						ilgenerator.Emit(OpCodes.Ldarg_0);
						ilgenerator.Emit(OpCodes.Call, getMethod);
						ilgenerator.Emit(OpCodes.Ldarg_1);
						ilgenerator.Emit(OpCodes.Beq_S, label);
					}
					else if (propertyType == typeof(byte[]))
					{
						ilgenerator.Emit(OpCodes.Ldsfld, this._compareByteArraysField);
						ilgenerator.Emit(OpCodes.Ldarg_0);
						ilgenerator.Emit(OpCodes.Call, getMethod);
						ilgenerator.Emit(OpCodes.Ldarg_1);
						ilgenerator.Emit(OpCodes.Callvirt, IPOCOImplementor.s_Func_object_object_bool_Invoke);
						ilgenerator.Emit(OpCodes.Brtrue_S, label);
					}
					else
					{
						MethodInfo method = propertyType.GetMethod("op_Inequality", new Type[]
						{
							propertyType,
							propertyType
						});
						if (method != null)
						{
							ilgenerator.Emit(OpCodes.Ldarg_0);
							ilgenerator.Emit(OpCodes.Call, getMethod);
							ilgenerator.Emit(OpCodes.Ldarg_1);
							ilgenerator.Emit(OpCodes.Call, method);
							ilgenerator.Emit(OpCodes.Brfalse_S, label);
						}
						else
						{
							ilgenerator.Emit(OpCodes.Ldarg_0);
							ilgenerator.Emit(OpCodes.Call, getMethod);
							if (propertyType.IsValueType)
							{
								ilgenerator.Emit(OpCodes.Box, propertyType);
							}
							ilgenerator.Emit(OpCodes.Ldarg_1);
							if (propertyType.IsValueType)
							{
								ilgenerator.Emit(OpCodes.Box, propertyType);
							}
							ilgenerator.Emit(OpCodes.Call, IPOCOImplementor.s_ObjectEquals);
							ilgenerator.Emit(OpCodes.Brtrue_S, label);
						}
					}
				}
			}
			ilgenerator.BeginExceptionBlock();
			ilgenerator.Emit(OpCodes.Ldarg_0);
			ilgenerator.Emit(OpCodes.Ldstr, baseProperty.Name);
			ilgenerator.Emit(OpCodes.Call, this._entityMemberChanging);
			ilgenerator.Emit(OpCodes.Ldarg_0);
			ilgenerator.Emit(OpCodes.Ldarg_1);
			ilgenerator.Emit(OpCodes.Call, setMethod);
			ilgenerator.Emit(OpCodes.Ldarg_0);
			ilgenerator.Emit(OpCodes.Ldstr, baseProperty.Name);
			ilgenerator.Emit(OpCodes.Call, this._entityMemberChanged);
			ilgenerator.BeginFinallyBlock();
			ilgenerator.Emit(OpCodes.Ldsfld, this._resetFKSetterFlagField);
			ilgenerator.Emit(OpCodes.Ldarg_0);
			ilgenerator.Emit(OpCodes.Callvirt, IPOCOImplementor.s_Action_Invoke);
			ilgenerator.EndExceptionBlock();
			ilgenerator.MarkLabel(label);
			ilgenerator.Emit(OpCodes.Ret);
			propertyBuilder.SetSetMethod(methodBuilder);
		}

		// Token: 0x06001B3C RID: 6972 RVA: 0x0005DBCC File Offset: 0x0005BDCC
		private void EmitReferenceProperty(TypeBuilder typeBuilder, PropertyBuilder propertyBuilder, PropertyInfo baseProperty, NavigationProperty navProperty)
		{
			MethodInfo setMethod = baseProperty.GetSetMethod(true);
			MethodAttributes methodAttributes = setMethod.Attributes & MethodAttributes.MemberAccessMask;
			MethodInfo meth = IPOCOImplementor.s_GetRelatedReference.MakeGenericMethod(new Type[]
			{
				baseProperty.PropertyType
			});
			MethodInfo method = typeof(EntityReference<>).MakeGenericType(new Type[]
			{
				baseProperty.PropertyType
			}).GetMethod("set_Value");
			MethodBuilder methodBuilder = typeBuilder.DefineMethod("set_" + baseProperty.Name, methodAttributes | (MethodAttributes.Virtual | MethodAttributes.HideBySig | MethodAttributes.SpecialName), null, new Type[]
			{
				baseProperty.PropertyType
			});
			ILGenerator ilgenerator = methodBuilder.GetILGenerator();
			ilgenerator.Emit(OpCodes.Ldarg_0);
			ilgenerator.Emit(OpCodes.Callvirt, this._getRelationshipManager);
			ilgenerator.Emit(OpCodes.Ldstr, navProperty.RelationshipType.FullName);
			ilgenerator.Emit(OpCodes.Ldstr, navProperty.ToEndMember.Name);
			ilgenerator.Emit(OpCodes.Callvirt, meth);
			ilgenerator.Emit(OpCodes.Ldarg_1);
			ilgenerator.Emit(OpCodes.Callvirt, method);
			ilgenerator.Emit(OpCodes.Ret);
			propertyBuilder.SetSetMethod(methodBuilder);
			this._referenceProperties.Add(new KeyValuePair<NavigationProperty, PropertyInfo>(navProperty, baseProperty));
		}

		// Token: 0x06001B3D RID: 6973 RVA: 0x0005DD00 File Offset: 0x0005BF00
		private void EmitCollectionProperty(TypeBuilder typeBuilder, PropertyBuilder propertyBuilder, PropertyInfo baseProperty, NavigationProperty navProperty)
		{
			MethodInfo setMethod = baseProperty.GetSetMethod(true);
			MethodAttributes methodAttributes = setMethod.Attributes & MethodAttributes.MemberAccessMask;
			string str = Strings.EntityProxyTypeInfo_CannotSetEntityCollectionProperty(propertyBuilder.Name, typeBuilder.Name);
			MethodBuilder methodBuilder = typeBuilder.DefineMethod("set_" + baseProperty.Name, methodAttributes | (MethodAttributes.Virtual | MethodAttributes.HideBySig | MethodAttributes.SpecialName), null, new Type[]
			{
				baseProperty.PropertyType
			});
			ILGenerator ilgenerator = methodBuilder.GetILGenerator();
			Label label = ilgenerator.DefineLabel();
			ilgenerator.Emit(OpCodes.Ldarg_1);
			ilgenerator.Emit(OpCodes.Ldarg_0);
			ilgenerator.Emit(OpCodes.Call, this._getRelationshipManager);
			ilgenerator.Emit(OpCodes.Ldstr, navProperty.RelationshipType.FullName);
			ilgenerator.Emit(OpCodes.Ldstr, navProperty.ToEndMember.Name);
			ilgenerator.Emit(OpCodes.Callvirt, IPOCOImplementor.s_GetRelatedEnd);
			ilgenerator.Emit(OpCodes.Beq_S, label);
			ilgenerator.Emit(OpCodes.Ldstr, str);
			ilgenerator.Emit(OpCodes.Newobj, IPOCOImplementor.s_InvalidOperationConstructor);
			ilgenerator.Emit(OpCodes.Throw);
			ilgenerator.MarkLabel(label);
			ilgenerator.Emit(OpCodes.Ldarg_0);
			ilgenerator.Emit(OpCodes.Ldarg_1);
			ilgenerator.Emit(OpCodes.Call, baseProperty.GetSetMethod(true));
			ilgenerator.Emit(OpCodes.Ret);
			propertyBuilder.SetSetMethod(methodBuilder);
			this._collectionProperties.Add(new KeyValuePair<NavigationProperty, PropertyInfo>(navProperty, baseProperty));
		}

		// Token: 0x06001B3E RID: 6974 RVA: 0x0005DE6C File Offset: 0x0005C06C
		private void ImplementIEntityWithChangeTracker(TypeBuilder typeBuilder, Action<FieldBuilder, bool> registerField)
		{
			this._changeTrackerField = typeBuilder.DefineField("_changeTracker", typeof(IEntityChangeTracker), FieldAttributes.Private);
			registerField(this._changeTrackerField, false);
			this._entityMemberChanging = typeBuilder.DefineMethod("EntityMemberChanging", MethodAttributes.Private | MethodAttributes.HideBySig, typeof(void), new Type[]
			{
				typeof(string)
			});
			ILGenerator ilgenerator = this._entityMemberChanging.GetILGenerator();
			Label label = ilgenerator.DefineLabel();
			ilgenerator.Emit(OpCodes.Ldarg_0);
			ilgenerator.Emit(OpCodes.Ldfld, this._changeTrackerField);
			ilgenerator.Emit(OpCodes.Brfalse_S, label);
			ilgenerator.Emit(OpCodes.Ldarg_0);
			ilgenerator.Emit(OpCodes.Ldfld, this._changeTrackerField);
			ilgenerator.Emit(OpCodes.Ldarg_1);
			ilgenerator.Emit(OpCodes.Callvirt, IPOCOImplementor.s_EntityMemberChanging);
			ilgenerator.MarkLabel(label);
			ilgenerator.Emit(OpCodes.Ret);
			this._entityMemberChanged = typeBuilder.DefineMethod("EntityMemberChanged", MethodAttributes.Private | MethodAttributes.HideBySig, typeof(void), new Type[]
			{
				typeof(string)
			});
			ilgenerator = this._entityMemberChanged.GetILGenerator();
			label = ilgenerator.DefineLabel();
			ilgenerator.Emit(OpCodes.Ldarg_0);
			ilgenerator.Emit(OpCodes.Ldfld, this._changeTrackerField);
			ilgenerator.Emit(OpCodes.Brfalse_S, label);
			ilgenerator.Emit(OpCodes.Ldarg_0);
			ilgenerator.Emit(OpCodes.Ldfld, this._changeTrackerField);
			ilgenerator.Emit(OpCodes.Ldarg_1);
			ilgenerator.Emit(OpCodes.Callvirt, IPOCOImplementor.s_EntityMemberChanged);
			ilgenerator.MarkLabel(label);
			ilgenerator.Emit(OpCodes.Ret);
			MethodBuilder methodBuilder = typeBuilder.DefineMethod("SetChangeTracker", MethodAttributes.FamANDAssem | MethodAttributes.Family | MethodAttributes.Final | MethodAttributes.Virtual | MethodAttributes.HideBySig | MethodAttributes.VtableLayoutMask, typeof(void), new Type[]
			{
				typeof(IEntityChangeTracker)
			});
			ilgenerator = methodBuilder.GetILGenerator();
			ilgenerator.Emit(OpCodes.Ldarg_0);
			ilgenerator.Emit(OpCodes.Ldarg_1);
			ilgenerator.Emit(OpCodes.Stfld, this._changeTrackerField);
			ilgenerator.Emit(OpCodes.Ret);
		}

		// Token: 0x06001B3F RID: 6975 RVA: 0x0005E078 File Offset: 0x0005C278
		private void ImplementIEntityWithRelationships(TypeBuilder typeBuilder, Action<FieldBuilder, bool> registerField)
		{
			this._relationshipManagerField = typeBuilder.DefineField("_relationshipManager", typeof(RelationshipManager), FieldAttributes.Private);
			registerField(this._relationshipManagerField, true);
			PropertyBuilder propertyBuilder = typeBuilder.DefineProperty("RelationshipManager", PropertyAttributes.None, typeof(RelationshipManager), Type.EmptyTypes);
			this._getRelationshipManager = typeBuilder.DefineMethod("get_RelationshipManager", MethodAttributes.FamANDAssem | MethodAttributes.Family | MethodAttributes.Final | MethodAttributes.Virtual | MethodAttributes.HideBySig | MethodAttributes.VtableLayoutMask | MethodAttributes.SpecialName, typeof(RelationshipManager), Type.EmptyTypes);
			ILGenerator ilgenerator = this._getRelationshipManager.GetILGenerator();
			Label label = ilgenerator.DefineLabel();
			ilgenerator.Emit(OpCodes.Ldarg_0);
			ilgenerator.Emit(OpCodes.Ldfld, this._relationshipManagerField);
			ilgenerator.Emit(OpCodes.Brtrue_S, label);
			ilgenerator.Emit(OpCodes.Ldarg_0);
			ilgenerator.Emit(OpCodes.Ldarg_0);
			ilgenerator.Emit(OpCodes.Call, IPOCOImplementor.s_CreateRelationshipManager);
			ilgenerator.Emit(OpCodes.Stfld, this._relationshipManagerField);
			ilgenerator.MarkLabel(label);
			ilgenerator.Emit(OpCodes.Ldarg_0);
			ilgenerator.Emit(OpCodes.Ldfld, this._relationshipManagerField);
			ilgenerator.Emit(OpCodes.Ret);
			propertyBuilder.SetGetMethod(this._getRelationshipManager);
		}

		// Token: 0x04000B46 RID: 2886
		private EntityType _ospaceEntityType;

		// Token: 0x04000B47 RID: 2887
		private FieldBuilder _changeTrackerField;

		// Token: 0x04000B48 RID: 2888
		private FieldBuilder _relationshipManagerField;

		// Token: 0x04000B49 RID: 2889
		private FieldBuilder _resetFKSetterFlagField;

		// Token: 0x04000B4A RID: 2890
		private FieldBuilder _compareByteArraysField;

		// Token: 0x04000B4B RID: 2891
		private MethodBuilder _entityMemberChanging;

		// Token: 0x04000B4C RID: 2892
		private MethodBuilder _entityMemberChanged;

		// Token: 0x04000B4D RID: 2893
		private MethodBuilder _getRelationshipManager;

		// Token: 0x04000B4E RID: 2894
		private List<KeyValuePair<NavigationProperty, PropertyInfo>> _referenceProperties;

		// Token: 0x04000B4F RID: 2895
		private List<KeyValuePair<NavigationProperty, PropertyInfo>> _collectionProperties;

		// Token: 0x04000B50 RID: 2896
		private bool _implementIEntityWithChangeTracker;

		// Token: 0x04000B51 RID: 2897
		private bool _implementIEntityWithRelationships;

		// Token: 0x04000B52 RID: 2898
		private HashSet<EdmMember> _scalarMembers;

		// Token: 0x04000B53 RID: 2899
		private HashSet<EdmMember> _relationshipMembers;

		// Token: 0x04000B54 RID: 2900
		private static readonly MethodInfo s_EntityMemberChanging = typeof(IEntityChangeTracker).GetMethod("EntityMemberChanging", new Type[]
		{
			typeof(string)
		});

		// Token: 0x04000B55 RID: 2901
		private static readonly MethodInfo s_EntityMemberChanged = typeof(IEntityChangeTracker).GetMethod("EntityMemberChanged", new Type[]
		{
			typeof(string)
		});

		// Token: 0x04000B56 RID: 2902
		private static readonly MethodInfo s_CreateRelationshipManager = typeof(RelationshipManager).GetMethod("Create", new Type[]
		{
			typeof(IEntityWithRelationships)
		});

		// Token: 0x04000B57 RID: 2903
		private static readonly MethodInfo s_GetRelationshipManager = typeof(IEntityWithRelationships).GetProperty("RelationshipManager").GetGetMethod();

		// Token: 0x04000B58 RID: 2904
		private static readonly MethodInfo s_GetRelatedReference = typeof(RelationshipManager).GetMethod("GetRelatedReference", new Type[]
		{
			typeof(string),
			typeof(string)
		});

		// Token: 0x04000B59 RID: 2905
		private static readonly MethodInfo s_GetRelatedCollection = typeof(RelationshipManager).GetMethod("GetRelatedCollection", new Type[]
		{
			typeof(string),
			typeof(string)
		});

		// Token: 0x04000B5A RID: 2906
		private static readonly MethodInfo s_GetRelatedEnd = typeof(RelationshipManager).GetMethod("GetRelatedEnd", new Type[]
		{
			typeof(string),
			typeof(string)
		});

		// Token: 0x04000B5B RID: 2907
		private static readonly MethodInfo s_ObjectEquals = typeof(object).GetMethod("Equals", new Type[]
		{
			typeof(object),
			typeof(object)
		});

		// Token: 0x04000B5C RID: 2908
		private static readonly ConstructorInfo s_InvalidOperationConstructor = typeof(InvalidOperationException).GetConstructor(new Type[]
		{
			typeof(string)
		});

		// Token: 0x04000B5D RID: 2909
		private static readonly MethodInfo s_IEntityWrapper_GetEntity = typeof(IEntityWrapper).GetProperty("Entity").GetGetMethod();

		// Token: 0x04000B5E RID: 2910
		private static readonly MethodInfo s_Action_Invoke = typeof(Action<object>).GetMethod("Invoke", new Type[]
		{
			typeof(object)
		});

		// Token: 0x04000B5F RID: 2911
		private static readonly MethodInfo s_Func_object_object_bool_Invoke = typeof(Func<object, object, bool>).GetMethod("Invoke", new Type[]
		{
			typeof(object),
			typeof(object)
		});

		// Token: 0x04000B60 RID: 2912
		private static readonly ConstructorInfo s_BrowsableAttributeConstructor = typeof(BrowsableAttribute).GetConstructor(new Type[]
		{
			typeof(bool)
		});
	}
}
