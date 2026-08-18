using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Core.Objects.DataClasses;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;
using System.Reflection;
using System.Reflection.Emit;

namespace System.Data.Entity.Core.Objects.Internal
{
	// Token: 0x02000589 RID: 1417
	internal class IPocoImplementor
	{
		// Token: 0x06003757 RID: 14167 RVA: 0x001064AC File Offset: 0x001046AC
		public IPocoImplementor(EntityType ospaceEntityType)
		{
			Type clrType = ospaceEntityType.ClrType;
			this._referenceProperties = new List<KeyValuePair<NavigationProperty, PropertyInfo>>();
			this._collectionProperties = new List<KeyValuePair<NavigationProperty, PropertyInfo>>();
			this._implementIEntityWithChangeTracker = (null == clrType.GetInterface(typeof(IEntityWithChangeTracker).Name));
			this._implementIEntityWithRelationships = (null == clrType.GetInterface(typeof(IEntityWithRelationships).Name));
			this.CheckType(ospaceEntityType);
			this._ospaceEntityType = ospaceEntityType;
		}

		// Token: 0x06003758 RID: 14168 RVA: 0x0010652C File Offset: 0x0010472C
		private void CheckType(EntityType ospaceEntityType)
		{
			this._scalarMembers = new HashSet<EdmMember>();
			this._relationshipMembers = new HashSet<EdmMember>();
			foreach (EdmMember edmMember in ospaceEntityType.Members)
			{
				PropertyInfo topProperty = ospaceEntityType.ClrType.GetTopProperty(edmMember.Name);
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
							if (topProperty.PropertyType.IsGenericType() && topProperty.PropertyType.GetGenericTypeDefinition() == typeof(ICollection<>))
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

		// Token: 0x06003759 RID: 14169 RVA: 0x00106698 File Offset: 0x00104898
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

		// Token: 0x1700084D RID: 2125
		// (get) Token: 0x0600375A RID: 14170 RVA: 0x00106700 File Offset: 0x00104900
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

		// Token: 0x0600375B RID: 14171 RVA: 0x00106749 File Offset: 0x00104949
		private static DynamicMethod CreateDynamicMethod(string name, Type returnType, Type[] parameterTypes)
		{
			return new DynamicMethod(name, returnType, parameterTypes, true);
		}

		// Token: 0x0600375C RID: 14172 RVA: 0x00106754 File Offset: 0x00104954
		public DynamicMethod CreateInitalizeCollectionMethod(Type proxyType)
		{
			if (this._collectionProperties.Count > 0)
			{
				DynamicMethod dynamicMethod = IPocoImplementor.CreateDynamicMethod(proxyType.Name + "_InitializeEntityCollections", typeof(IEntityWrapper), new Type[]
				{
					typeof(IEntityWrapper)
				});
				ILGenerator ilgenerator = dynamicMethod.GetILGenerator();
				ilgenerator.DeclareLocal(proxyType);
				ilgenerator.DeclareLocal(typeof(RelationshipManager));
				ilgenerator.Emit(OpCodes.Ldarg_0);
				ilgenerator.Emit(OpCodes.Callvirt, IPocoImplementor.GetEntityMethod);
				ilgenerator.Emit(OpCodes.Castclass, proxyType);
				ilgenerator.Emit(OpCodes.Stloc_0);
				ilgenerator.Emit(OpCodes.Ldloc_0);
				ilgenerator.Emit(OpCodes.Callvirt, IPocoImplementor.GetRelationshipManagerMethod);
				ilgenerator.Emit(OpCodes.Stloc_1);
				foreach (KeyValuePair<NavigationProperty, PropertyInfo> keyValuePair in this._collectionProperties)
				{
					MethodInfo meth = IPocoImplementor.GetRelatedCollectionMethod.MakeGenericMethod(new Type[]
					{
						EntityUtil.GetCollectionElementType(keyValuePair.Value.PropertyType)
					});
					ilgenerator.Emit(OpCodes.Ldloc_0);
					ilgenerator.Emit(OpCodes.Ldloc_1);
					ilgenerator.Emit(OpCodes.Ldstr, keyValuePair.Key.RelationshipType.FullName);
					ilgenerator.Emit(OpCodes.Ldstr, keyValuePair.Key.ToEndMember.Name);
					ilgenerator.Emit(OpCodes.Callvirt, meth);
					ilgenerator.Emit(OpCodes.Callvirt, keyValuePair.Value.Setter());
				}
				ilgenerator.Emit(OpCodes.Ldarg_0);
				ilgenerator.Emit(OpCodes.Ret);
				return dynamicMethod;
			}
			return null;
		}

		// Token: 0x0600375D RID: 14173 RVA: 0x0010691C File Offset: 0x00104B1C
		public bool CanProxyMember(EdmMember member)
		{
			return this._scalarMembers.Contains(member) || this._relationshipMembers.Contains(member);
		}

		// Token: 0x0600375E RID: 14174 RVA: 0x0010693C File Offset: 0x00104B3C
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

		// Token: 0x0600375F RID: 14175 RVA: 0x001069C4 File Offset: 0x00104BC4
		private void EmitScalarSetter(TypeBuilder typeBuilder, PropertyBuilder propertyBuilder, PropertyInfo baseProperty, bool isKeyMember)
		{
			MethodInfo methodInfo = baseProperty.Setter();
			MethodAttributes methodAttributes = methodInfo.Attributes & MethodAttributes.MemberAccessMask;
			MethodBuilder methodBuilder = typeBuilder.DefineMethod("set_" + baseProperty.Name, methodAttributes | (MethodAttributes.Virtual | MethodAttributes.HideBySig | MethodAttributes.SpecialName), null, new Type[]
			{
				baseProperty.PropertyType
			});
			ILGenerator ilgenerator = methodBuilder.GetILGenerator();
			Label label = ilgenerator.DefineLabel();
			if (isKeyMember)
			{
				MethodInfo methodInfo2 = baseProperty.Getter();
				if (methodInfo2 != null)
				{
					Type propertyType = baseProperty.PropertyType;
					if (propertyType == typeof(int) || propertyType == typeof(short) || propertyType == typeof(long) || propertyType == typeof(bool) || propertyType == typeof(byte) || propertyType == typeof(uint) || propertyType == typeof(ulong) || propertyType == typeof(float) || propertyType == typeof(double) || propertyType.IsEnum())
					{
						ilgenerator.Emit(OpCodes.Ldarg_0);
						ilgenerator.Emit(OpCodes.Call, methodInfo2);
						ilgenerator.Emit(OpCodes.Ldarg_1);
						ilgenerator.Emit(OpCodes.Beq_S, label);
					}
					else if (propertyType == typeof(byte[]))
					{
						ilgenerator.Emit(OpCodes.Ldsfld, this._compareByteArraysField);
						ilgenerator.Emit(OpCodes.Ldarg_0);
						ilgenerator.Emit(OpCodes.Call, methodInfo2);
						ilgenerator.Emit(OpCodes.Ldarg_1);
						ilgenerator.Emit(OpCodes.Callvirt, IPocoImplementor.FuncInvokeMethod);
						ilgenerator.Emit(OpCodes.Brtrue_S, label);
					}
					else
					{
						MethodInfo declaredMethod = propertyType.GetDeclaredMethod("op_Inequality", new Type[]
						{
							propertyType,
							propertyType
						});
						if (declaredMethod != null)
						{
							ilgenerator.Emit(OpCodes.Ldarg_0);
							ilgenerator.Emit(OpCodes.Call, methodInfo2);
							ilgenerator.Emit(OpCodes.Ldarg_1);
							ilgenerator.Emit(OpCodes.Call, declaredMethod);
							ilgenerator.Emit(OpCodes.Brfalse_S, label);
						}
						else
						{
							ilgenerator.Emit(OpCodes.Ldarg_0);
							ilgenerator.Emit(OpCodes.Call, methodInfo2);
							if (propertyType.IsValueType())
							{
								ilgenerator.Emit(OpCodes.Box, propertyType);
							}
							ilgenerator.Emit(OpCodes.Ldarg_1);
							if (propertyType.IsValueType())
							{
								ilgenerator.Emit(OpCodes.Box, propertyType);
							}
							ilgenerator.Emit(OpCodes.Call, IPocoImplementor.ObjectEqualsMethod);
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
			ilgenerator.Emit(OpCodes.Call, methodInfo);
			ilgenerator.Emit(OpCodes.Ldarg_0);
			ilgenerator.Emit(OpCodes.Ldstr, baseProperty.Name);
			ilgenerator.Emit(OpCodes.Call, this._entityMemberChanged);
			ilgenerator.BeginFinallyBlock();
			ilgenerator.Emit(OpCodes.Ldsfld, this._resetFKSetterFlagField);
			ilgenerator.Emit(OpCodes.Ldarg_0);
			ilgenerator.Emit(OpCodes.Callvirt, IPocoImplementor.InvokeMethod);
			ilgenerator.EndExceptionBlock();
			ilgenerator.MarkLabel(label);
			ilgenerator.Emit(OpCodes.Ret);
			propertyBuilder.SetSetMethod(methodBuilder);
		}

		// Token: 0x06003760 RID: 14176 RVA: 0x00106D50 File Offset: 0x00104F50
		private void EmitReferenceProperty(TypeBuilder typeBuilder, PropertyBuilder propertyBuilder, PropertyInfo baseProperty, NavigationProperty navProperty)
		{
			MethodInfo methodInfo = baseProperty.Setter();
			MethodAttributes methodAttributes = methodInfo.Attributes & MethodAttributes.MemberAccessMask;
			MethodInfo meth = IPocoImplementor.GetRelatedReferenceMethod.MakeGenericMethod(new Type[]
			{
				baseProperty.PropertyType
			});
			MethodInfo onlyDeclaredMethod = typeof(EntityReference<>).MakeGenericType(new Type[]
			{
				baseProperty.PropertyType
			}).GetOnlyDeclaredMethod("set_Value");
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
			ilgenerator.Emit(OpCodes.Callvirt, onlyDeclaredMethod);
			ilgenerator.Emit(OpCodes.Ret);
			propertyBuilder.SetSetMethod(methodBuilder);
			this._referenceProperties.Add(new KeyValuePair<NavigationProperty, PropertyInfo>(navProperty, baseProperty));
		}

		// Token: 0x06003761 RID: 14177 RVA: 0x00106E94 File Offset: 0x00105094
		private void EmitCollectionProperty(TypeBuilder typeBuilder, PropertyBuilder propertyBuilder, PropertyInfo baseProperty, NavigationProperty navProperty)
		{
			MethodInfo methodInfo = baseProperty.Setter();
			MethodAttributes methodAttributes = methodInfo.Attributes & MethodAttributes.MemberAccessMask;
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
			ilgenerator.Emit(OpCodes.Callvirt, IPocoImplementor.GetRelatedEndMethod);
			ilgenerator.Emit(OpCodes.Beq_S, label);
			ilgenerator.Emit(OpCodes.Ldstr, str);
			ilgenerator.Emit(OpCodes.Newobj, IPocoImplementor._invalidOperationConstructorMethod);
			ilgenerator.Emit(OpCodes.Throw);
			ilgenerator.MarkLabel(label);
			ilgenerator.Emit(OpCodes.Ldarg_0);
			ilgenerator.Emit(OpCodes.Ldarg_1);
			ilgenerator.Emit(OpCodes.Call, baseProperty.Setter());
			ilgenerator.Emit(OpCodes.Ret);
			propertyBuilder.SetSetMethod(methodBuilder);
			this._collectionProperties.Add(new KeyValuePair<NavigationProperty, PropertyInfo>(navProperty, baseProperty));
		}

		// Token: 0x06003762 RID: 14178 RVA: 0x00107004 File Offset: 0x00105204
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
			ilgenerator.Emit(OpCodes.Callvirt, IPocoImplementor.EntityMemberChangingMethod);
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
			ilgenerator.Emit(OpCodes.Callvirt, IPocoImplementor.EntityMemberChangedMethod);
			ilgenerator.MarkLabel(label);
			ilgenerator.Emit(OpCodes.Ret);
			MethodBuilder methodBuilder = typeBuilder.DefineMethod("IEntityWithChangeTracker.SetChangeTracker", MethodAttributes.Private | MethodAttributes.Final | MethodAttributes.Virtual | MethodAttributes.HideBySig | MethodAttributes.VtableLayoutMask, typeof(void), new Type[]
			{
				typeof(IEntityChangeTracker)
			});
			ilgenerator = methodBuilder.GetILGenerator();
			ilgenerator.Emit(OpCodes.Ldarg_0);
			ilgenerator.Emit(OpCodes.Ldarg_1);
			ilgenerator.Emit(OpCodes.Stfld, this._changeTrackerField);
			ilgenerator.Emit(OpCodes.Ret);
			typeBuilder.DefineMethodOverride(methodBuilder, IPocoImplementor.SetChangeTrackerMethod);
		}

		// Token: 0x06003763 RID: 14179 RVA: 0x00107228 File Offset: 0x00105428
		private void ImplementIEntityWithRelationships(TypeBuilder typeBuilder, Action<FieldBuilder, bool> registerField)
		{
			this._relationshipManagerField = typeBuilder.DefineField("_relationshipManager", typeof(RelationshipManager), FieldAttributes.Private);
			registerField(this._relationshipManagerField, true);
			PropertyBuilder propertyBuilder = typeBuilder.DefineProperty("RelationshipManager", PropertyAttributes.None, typeof(RelationshipManager), Type.EmptyTypes);
			this._getRelationshipManager = typeBuilder.DefineMethod("IEntityWithRelationships.get_RelationshipManager", MethodAttributes.Private | MethodAttributes.Final | MethodAttributes.Virtual | MethodAttributes.HideBySig | MethodAttributes.VtableLayoutMask | MethodAttributes.SpecialName, typeof(RelationshipManager), Type.EmptyTypes);
			ILGenerator ilgenerator = this._getRelationshipManager.GetILGenerator();
			Label label = ilgenerator.DefineLabel();
			ilgenerator.Emit(OpCodes.Ldarg_0);
			ilgenerator.Emit(OpCodes.Ldfld, this._relationshipManagerField);
			ilgenerator.Emit(OpCodes.Brtrue_S, label);
			ilgenerator.Emit(OpCodes.Ldarg_0);
			ilgenerator.Emit(OpCodes.Ldarg_0);
			ilgenerator.Emit(OpCodes.Call, IPocoImplementor.CreateRelationshipManagerMethod);
			ilgenerator.Emit(OpCodes.Stfld, this._relationshipManagerField);
			ilgenerator.MarkLabel(label);
			ilgenerator.Emit(OpCodes.Ldarg_0);
			ilgenerator.Emit(OpCodes.Ldfld, this._relationshipManagerField);
			ilgenerator.Emit(OpCodes.Ret);
			propertyBuilder.SetGetMethod(this._getRelationshipManager);
			typeBuilder.DefineMethodOverride(this._getRelationshipManager, IPocoImplementor.GetRelationshipManagerMethod);
		}

		// Token: 0x04001541 RID: 5441
		private readonly EntityType _ospaceEntityType;

		// Token: 0x04001542 RID: 5442
		private FieldBuilder _changeTrackerField;

		// Token: 0x04001543 RID: 5443
		private FieldBuilder _relationshipManagerField;

		// Token: 0x04001544 RID: 5444
		private FieldBuilder _resetFKSetterFlagField;

		// Token: 0x04001545 RID: 5445
		private FieldBuilder _compareByteArraysField;

		// Token: 0x04001546 RID: 5446
		private MethodBuilder _entityMemberChanging;

		// Token: 0x04001547 RID: 5447
		private MethodBuilder _entityMemberChanged;

		// Token: 0x04001548 RID: 5448
		private MethodBuilder _getRelationshipManager;

		// Token: 0x04001549 RID: 5449
		private readonly List<KeyValuePair<NavigationProperty, PropertyInfo>> _referenceProperties;

		// Token: 0x0400154A RID: 5450
		private readonly List<KeyValuePair<NavigationProperty, PropertyInfo>> _collectionProperties;

		// Token: 0x0400154B RID: 5451
		private bool _implementIEntityWithChangeTracker;

		// Token: 0x0400154C RID: 5452
		private bool _implementIEntityWithRelationships;

		// Token: 0x0400154D RID: 5453
		private HashSet<EdmMember> _scalarMembers;

		// Token: 0x0400154E RID: 5454
		private HashSet<EdmMember> _relationshipMembers;

		// Token: 0x0400154F RID: 5455
		internal static readonly MethodInfo EntityMemberChangingMethod = typeof(IEntityChangeTracker).GetDeclaredMethod("EntityMemberChanging", new Type[]
		{
			typeof(string)
		});

		// Token: 0x04001550 RID: 5456
		internal static readonly MethodInfo EntityMemberChangedMethod = typeof(IEntityChangeTracker).GetDeclaredMethod("EntityMemberChanged", new Type[]
		{
			typeof(string)
		});

		// Token: 0x04001551 RID: 5457
		internal static readonly MethodInfo CreateRelationshipManagerMethod = typeof(RelationshipManager).GetDeclaredMethod("Create", new Type[]
		{
			typeof(IEntityWithRelationships)
		});

		// Token: 0x04001552 RID: 5458
		internal static readonly MethodInfo GetRelationshipManagerMethod = typeof(IEntityWithRelationships).GetDeclaredProperty("RelationshipManager").Getter();

		// Token: 0x04001553 RID: 5459
		internal static readonly MethodInfo GetRelatedReferenceMethod = typeof(RelationshipManager).GetDeclaredMethod("GetRelatedReference", new Type[]
		{
			typeof(string),
			typeof(string)
		});

		// Token: 0x04001554 RID: 5460
		internal static readonly MethodInfo GetRelatedCollectionMethod = typeof(RelationshipManager).GetDeclaredMethod("GetRelatedCollection", new Type[]
		{
			typeof(string),
			typeof(string)
		});

		// Token: 0x04001555 RID: 5461
		internal static readonly MethodInfo GetRelatedEndMethod = typeof(RelationshipManager).GetDeclaredMethod("GetRelatedEnd", new Type[]
		{
			typeof(string),
			typeof(string)
		});

		// Token: 0x04001556 RID: 5462
		internal static readonly MethodInfo ObjectEqualsMethod = typeof(object).GetDeclaredMethod("Equals", new Type[]
		{
			typeof(object),
			typeof(object)
		});

		// Token: 0x04001557 RID: 5463
		private static readonly ConstructorInfo _invalidOperationConstructorMethod = typeof(InvalidOperationException).GetDeclaredConstructor(new Type[]
		{
			typeof(string)
		});

		// Token: 0x04001558 RID: 5464
		internal static readonly MethodInfo GetEntityMethod = typeof(IEntityWrapper).GetDeclaredProperty("Entity").Getter();

		// Token: 0x04001559 RID: 5465
		internal static readonly MethodInfo InvokeMethod = typeof(Action<object>).GetDeclaredMethod("Invoke", new Type[]
		{
			typeof(object)
		});

		// Token: 0x0400155A RID: 5466
		internal static readonly MethodInfo FuncInvokeMethod = typeof(Func<object, object, bool>).GetDeclaredMethod("Invoke", new Type[]
		{
			typeof(object),
			typeof(object)
		});

		// Token: 0x0400155B RID: 5467
		internal static readonly MethodInfo SetChangeTrackerMethod = typeof(IEntityWithChangeTracker).GetOnlyDeclaredMethod("SetChangeTracker");
	}
}
