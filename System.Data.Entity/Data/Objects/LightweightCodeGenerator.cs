using System;
using System.Data.Common.Utils;
using System.Data.Entity;
using System.Data.Metadata.Edm;
using System.Data.Objects.DataClasses;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using System.Security;
using System.Security.Permissions;

namespace System.Data.Objects
{
	// Token: 0x02000135 RID: 309
	internal static class LightweightCodeGenerator
	{
		// Token: 0x0600167E RID: 5758 RVA: 0x0004B5C4 File Offset: 0x000497C4
		internal static Delegate GetConstructorDelegateForType(ClrComplexType clrType)
		{
			Delegate result;
			if ((result = clrType.Constructor) == null)
			{
				result = (clrType.Constructor = LightweightCodeGenerator.CreateConstructor(clrType.ClrType));
			}
			return result;
		}

		// Token: 0x0600167F RID: 5759 RVA: 0x0004B5F0 File Offset: 0x000497F0
		internal static Delegate GetConstructorDelegateForType(ClrEntityType clrType)
		{
			Delegate result;
			if ((result = clrType.Constructor) == null)
			{
				result = (clrType.Constructor = LightweightCodeGenerator.CreateConstructor(clrType.ClrType));
			}
			return result;
		}

		// Token: 0x06001680 RID: 5760 RVA: 0x0004B61C File Offset: 0x0004981C
		internal static object GetValue(EdmProperty property, object target)
		{
			Func<object, object> getterDelegateForProperty = LightweightCodeGenerator.GetGetterDelegateForProperty(property);
			return getterDelegateForProperty(target);
		}

		// Token: 0x06001681 RID: 5761 RVA: 0x0004B638 File Offset: 0x00049838
		internal static Func<object, object> GetGetterDelegateForProperty(EdmProperty property)
		{
			Func<object, object> result;
			if ((result = property.ValueGetter) == null)
			{
				result = (property.ValueGetter = LightweightCodeGenerator.CreatePropertyGetter(property.EntityDeclaringType, property.PropertyGetterHandle));
			}
			return result;
		}

		// Token: 0x06001682 RID: 5762 RVA: 0x0004B66C File Offset: 0x0004986C
		internal static void SetValue(EdmProperty property, object target, object value)
		{
			Action<object, object> setterDelegateForProperty = LightweightCodeGenerator.GetSetterDelegateForProperty(property);
			setterDelegateForProperty(target, value);
		}

		// Token: 0x06001683 RID: 5763 RVA: 0x0004B688 File Offset: 0x00049888
		internal static Action<object, object> GetSetterDelegateForProperty(EdmProperty property)
		{
			Action<object, object> action = property.ValueSetter;
			if (action == null)
			{
				action = LightweightCodeGenerator.CreatePropertySetter(property.EntityDeclaringType, property.PropertySetterHandle, property.Nullable);
				property.ValueSetter = action;
			}
			return action;
		}

		// Token: 0x06001684 RID: 5764 RVA: 0x0004B6C0 File Offset: 0x000498C0
		internal static RelatedEnd GetRelatedEnd(RelationshipManager sourceRelationshipManager, AssociationEndMember sourceMember, AssociationEndMember targetMember, RelatedEnd existingRelatedEnd)
		{
			Func<RelationshipManager, RelatedEnd, RelatedEnd> func = sourceMember.GetRelatedEnd;
			if (func == null)
			{
				func = LightweightCodeGenerator.CreateGetRelatedEndMethod(sourceMember, targetMember);
				sourceMember.GetRelatedEnd = func;
			}
			return func(sourceRelationshipManager, existingRelatedEnd);
		}

		// Token: 0x06001685 RID: 5765 RVA: 0x0004B6F0 File Offset: 0x000498F0
		internal static Action<object, object> CreateNavigationPropertySetter(Type declaringType, PropertyInfo navigationProperty)
		{
			MethodInfo setMethod = navigationProperty.GetSetMethod(true);
			Type propertyType = navigationProperty.PropertyType;
			if (null == setMethod)
			{
				LightweightCodeGenerator.ThrowPropertyNoSetter();
			}
			if (setMethod.IsStatic)
			{
				LightweightCodeGenerator.ThrowPropertyIsStatic();
			}
			if (setMethod.DeclaringType.IsValueType)
			{
				LightweightCodeGenerator.ThrowPropertyDeclaringTypeIsValueType();
			}
			DynamicMethod dynamicMethod = LightweightCodeGenerator.CreateDynamicMethod(setMethod.Name, typeof(void), new Type[]
			{
				typeof(object),
				typeof(object)
			});
			ILGenerator ilgenerator = dynamicMethod.GetILGenerator();
			LightweightCodeGenerator.GenerateNecessaryPermissionDemands(ilgenerator, setMethod);
			ilgenerator.Emit(OpCodes.Ldarg_0);
			ilgenerator.Emit(OpCodes.Castclass, declaringType);
			ilgenerator.Emit(OpCodes.Ldarg_1);
			ilgenerator.Emit(OpCodes.Castclass, navigationProperty.PropertyType);
			ilgenerator.Emit(OpCodes.Callvirt, setMethod);
			ilgenerator.Emit(OpCodes.Ret);
			return (Action<object, object>)dynamicMethod.CreateDelegate(typeof(Action<object, object>));
		}

		// Token: 0x06001686 RID: 5766 RVA: 0x0004B7DC File Offset: 0x000499DC
		internal static ConstructorInfo GetConstructorForType(Type type)
		{
			ConstructorInfo constructor = type.GetConstructor(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.CreateInstance, null, Type.EmptyTypes, null);
			if (null == constructor)
			{
				LightweightCodeGenerator.ThrowConstructorNoParameterless(type);
			}
			return constructor;
		}

		// Token: 0x06001687 RID: 5767 RVA: 0x0004B80C File Offset: 0x00049A0C
		internal static Delegate CreateConstructor(Type type)
		{
			ConstructorInfo constructorForType = LightweightCodeGenerator.GetConstructorForType(type);
			DynamicMethod dynamicMethod = LightweightCodeGenerator.CreateDynamicMethod(constructorForType.DeclaringType.Name, typeof(object), Type.EmptyTypes);
			ILGenerator ilgenerator = dynamicMethod.GetILGenerator();
			LightweightCodeGenerator.GenerateNecessaryPermissionDemands(ilgenerator, constructorForType);
			ilgenerator.Emit(OpCodes.Newobj, constructorForType);
			ilgenerator.Emit(OpCodes.Ret);
			return dynamicMethod.CreateDelegate(typeof(Func<object>));
		}

		// Token: 0x06001688 RID: 5768 RVA: 0x0004B878 File Offset: 0x00049A78
		private static Func<object, object> CreatePropertyGetter(RuntimeTypeHandle entityDeclaringType, RuntimeMethodHandle rmh)
		{
			if (default(RuntimeMethodHandle).Equals(rmh))
			{
				LightweightCodeGenerator.ThrowPropertyNoGetter();
			}
			MethodInfo methodInfo = (MethodInfo)MethodBase.GetMethodFromHandle(rmh, entityDeclaringType);
			if (methodInfo.IsStatic)
			{
				LightweightCodeGenerator.ThrowPropertyIsStatic();
			}
			if (methodInfo.DeclaringType.IsValueType)
			{
				LightweightCodeGenerator.ThrowPropertyDeclaringTypeIsValueType();
			}
			if (methodInfo.GetParameters().Length != 0)
			{
				LightweightCodeGenerator.ThrowPropertyIsIndexed();
			}
			Type returnType = methodInfo.ReturnType;
			if (null == returnType || typeof(void) == returnType)
			{
				LightweightCodeGenerator.ThrowPropertyUnsupportedForm();
			}
			if (returnType.IsPointer)
			{
				LightweightCodeGenerator.ThrowPropertyUnsupportedType();
			}
			DynamicMethod dynamicMethod = LightweightCodeGenerator.CreateDynamicMethod(methodInfo.Name, typeof(object), new Type[]
			{
				typeof(object)
			});
			ILGenerator ilgenerator = dynamicMethod.GetILGenerator();
			LightweightCodeGenerator.GenerateNecessaryPermissionDemands(ilgenerator, methodInfo);
			ilgenerator.Emit(OpCodes.Ldarg_0);
			ilgenerator.Emit(OpCodes.Castclass, methodInfo.DeclaringType);
			ilgenerator.Emit(methodInfo.IsVirtual ? OpCodes.Callvirt : OpCodes.Call, methodInfo);
			if (returnType.IsValueType)
			{
				if (returnType.IsGenericType && typeof(Nullable<>) == returnType.GetGenericTypeDefinition())
				{
					Type cls = returnType.GetGenericArguments()[0];
					Label label = ilgenerator.DefineLabel();
					LocalBuilder local = ilgenerator.DeclareLocal(returnType);
					ilgenerator.Emit(OpCodes.Stloc_S, local);
					ilgenerator.Emit(OpCodes.Ldloca_S, local);
					ilgenerator.Emit(OpCodes.Call, returnType.GetMethod("get_HasValue"));
					ilgenerator.Emit(OpCodes.Brfalse_S, label);
					ilgenerator.Emit(OpCodes.Ldloca_S, local);
					ilgenerator.Emit(OpCodes.Call, returnType.GetMethod("get_Value"));
					ilgenerator.Emit(OpCodes.Box, returnType.GetGenericArguments()[0]);
					ilgenerator.Emit(OpCodes.Ret);
					ilgenerator.MarkLabel(label);
					ilgenerator.Emit(OpCodes.Ldnull);
				}
				else
				{
					Type cls = returnType;
					ilgenerator.Emit(OpCodes.Box, cls);
				}
			}
			ilgenerator.Emit(OpCodes.Ret);
			return (Func<object, object>)dynamicMethod.CreateDelegate(typeof(Func<object, object>));
		}

		// Token: 0x06001689 RID: 5769 RVA: 0x0004BA88 File Offset: 0x00049C88
		private static Action<object, object> CreatePropertySetter(RuntimeTypeHandle entityDeclaringType, RuntimeMethodHandle rmh, bool allowNull)
		{
			MethodInfo methodInfo;
			Type type;
			LightweightCodeGenerator.ValidateSetterProperty(entityDeclaringType, rmh, out methodInfo, out type);
			DynamicMethod dynamicMethod = LightweightCodeGenerator.CreateDynamicMethod(methodInfo.Name, typeof(void), new Type[]
			{
				typeof(object),
				typeof(object)
			});
			ILGenerator ilgenerator = dynamicMethod.GetILGenerator();
			LightweightCodeGenerator.GenerateNecessaryPermissionDemands(ilgenerator, methodInfo);
			Type type2 = type;
			Label label = ilgenerator.DefineLabel();
			Label label2 = ilgenerator.DefineLabel();
			Label label3 = ilgenerator.DefineLabel();
			if (type.IsValueType)
			{
				if (type.IsGenericType && typeof(Nullable<>) == type.GetGenericTypeDefinition())
				{
					type2 = type.GetGenericArguments()[0];
				}
				else
				{
					allowNull = false;
				}
			}
			ilgenerator.Emit(OpCodes.Ldarg_0);
			ilgenerator.Emit(OpCodes.Castclass, methodInfo.DeclaringType);
			ilgenerator.Emit(OpCodes.Ldarg_1);
			ilgenerator.Emit(OpCodes.Isinst, type2);
			if (allowNull)
			{
				ilgenerator.Emit(OpCodes.Ldarg_1);
				if (type2 == type)
				{
					ilgenerator.Emit(OpCodes.Brfalse_S, label);
				}
				else
				{
					ilgenerator.Emit(OpCodes.Brtrue, label2);
					ilgenerator.Emit(OpCodes.Pop);
					LocalBuilder local = ilgenerator.DeclareLocal(type);
					ilgenerator.Emit(OpCodes.Ldloca_S, local);
					ilgenerator.Emit(OpCodes.Initobj, type);
					ilgenerator.Emit(OpCodes.Ldloc_0);
					ilgenerator.Emit(OpCodes.Br_S, label);
					ilgenerator.MarkLabel(label2);
				}
			}
			ilgenerator.Emit(OpCodes.Dup);
			ilgenerator.Emit(OpCodes.Brfalse_S, label3);
			if (type2.IsValueType)
			{
				ilgenerator.Emit(OpCodes.Unbox_Any, type2);
				if (type2 != type)
				{
					ilgenerator.Emit(OpCodes.Newobj, type.GetConstructor(new Type[]
					{
						type2
					}));
				}
			}
			ilgenerator.MarkLabel(label);
			ilgenerator.Emit(methodInfo.IsVirtual ? OpCodes.Callvirt : OpCodes.Call, methodInfo);
			ilgenerator.Emit(OpCodes.Ret);
			ilgenerator.MarkLabel(label3);
			ilgenerator.Emit(OpCodes.Pop);
			ilgenerator.Emit(OpCodes.Pop);
			ilgenerator.Emit(OpCodes.Ldarg_1);
			ilgenerator.Emit(OpCodes.Ldtoken, type2);
			ilgenerator.Emit(OpCodes.Call, typeof(Type).GetMethod("GetTypeFromHandle", BindingFlags.Static | BindingFlags.Public));
			ilgenerator.Emit(OpCodes.Ldstr, methodInfo.DeclaringType.Name);
			ilgenerator.Emit(OpCodes.Ldstr, methodInfo.Name.Substring(4));
			ilgenerator.Emit(OpCodes.Call, typeof(EntityUtil).GetMethod("ThrowSetInvalidValue", BindingFlags.Static | BindingFlags.NonPublic, null, new Type[]
			{
				typeof(object),
				typeof(Type),
				typeof(string),
				typeof(string)
			}, null));
			ilgenerator.Emit(OpCodes.Ret);
			return (Action<object, object>)dynamicMethod.CreateDelegate(typeof(Action<object, object>));
		}

		// Token: 0x0600168A RID: 5770 RVA: 0x0004BD70 File Offset: 0x00049F70
		internal static void ValidateSetterProperty(RuntimeTypeHandle entityDeclaringType, RuntimeMethodHandle setterMethodHandle, out MethodInfo setterMethodInfo, out Type realType)
		{
			if (default(RuntimeMethodHandle).Equals(setterMethodHandle))
			{
				LightweightCodeGenerator.ThrowPropertyNoSetter();
			}
			setterMethodInfo = (MethodInfo)MethodBase.GetMethodFromHandle(setterMethodHandle, entityDeclaringType);
			if (setterMethodInfo.IsStatic)
			{
				LightweightCodeGenerator.ThrowPropertyIsStatic();
			}
			if (setterMethodInfo.DeclaringType.IsValueType)
			{
				LightweightCodeGenerator.ThrowPropertyDeclaringTypeIsValueType();
			}
			ParameterInfo[] parameters = setterMethodInfo.GetParameters();
			if (parameters == null || 1 != parameters.Length)
			{
				LightweightCodeGenerator.ThrowPropertyIsIndexed();
			}
			realType = setterMethodInfo.ReturnType;
			if (null != realType && typeof(void) != realType)
			{
				LightweightCodeGenerator.ThrowPropertyUnsupportedForm();
			}
			realType = parameters[0].ParameterType;
			if (realType.IsPointer)
			{
				LightweightCodeGenerator.ThrowPropertyUnsupportedType();
			}
		}

		// Token: 0x0600168B RID: 5771 RVA: 0x0004BE1B File Offset: 0x0004A01B
		internal static bool RequiresPermissionDemands(MethodBase mi)
		{
			return !LightweightCodeGenerator.IsPublic(mi);
		}

		// Token: 0x0600168C RID: 5772 RVA: 0x0004BE28 File Offset: 0x0004A028
		private static void GenerateNecessaryPermissionDemands(ILGenerator gen, MethodBase mi)
		{
			if (!LightweightCodeGenerator.IsPublic(mi))
			{
				gen.Emit(OpCodes.Ldsfld, typeof(LightweightCodeGenerator).GetField("MemberAccessReflectionPermission", BindingFlags.Static | BindingFlags.NonPublic));
				gen.Emit(OpCodes.Callvirt, typeof(ReflectionPermission).GetMethod("Demand"));
			}
		}

		// Token: 0x0600168D RID: 5773 RVA: 0x0004BE7D File Offset: 0x0004A07D
		internal static bool IsPublic(MethodBase method)
		{
			return method.IsPublic && LightweightCodeGenerator.IsPublic(method.DeclaringType);
		}

		// Token: 0x0600168E RID: 5774 RVA: 0x0004BE94 File Offset: 0x0004A094
		internal static bool IsPublic(Type type)
		{
			return null == type || (type.IsPublic && LightweightCodeGenerator.IsPublic(type.DeclaringType));
		}

		// Token: 0x0600168F RID: 5775 RVA: 0x0004BEB8 File Offset: 0x0004A0B8
		[MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.NoOptimization)]
		private static Func<RelationshipManager, RelatedEnd, RelatedEnd> CreateGetRelatedEndMethod(AssociationEndMember sourceMember, AssociationEndMember targetMember)
		{
			EntityType entityTypeForEnd = MetadataHelper.GetEntityTypeForEnd(sourceMember);
			EntityType entityTypeForEnd2 = MetadataHelper.GetEntityTypeForEnd(targetMember);
			NavigationPropertyAccessor navigationPropertyAccessor = MetadataHelper.GetNavigationPropertyAccessor(entityTypeForEnd2, targetMember, sourceMember);
			NavigationPropertyAccessor navigationPropertyAccessor2 = MetadataHelper.GetNavigationPropertyAccessor(entityTypeForEnd, sourceMember, targetMember);
			MethodInfo method = typeof(LightweightCodeGenerator).GetMethod("CreateGetRelatedEndMethod", BindingFlags.Static | BindingFlags.NonPublic, null, new Type[]
			{
				typeof(AssociationEndMember),
				typeof(AssociationEndMember),
				typeof(NavigationPropertyAccessor),
				typeof(NavigationPropertyAccessor)
			}, null);
			MethodInfo methodInfo = method.MakeGenericMethod(new Type[]
			{
				entityTypeForEnd.ClrType,
				entityTypeForEnd2.ClrType
			});
			object obj = methodInfo.Invoke(null, new object[]
			{
				sourceMember,
				targetMember,
				navigationPropertyAccessor,
				navigationPropertyAccessor2
			});
			return (Func<RelationshipManager, RelatedEnd, RelatedEnd>)obj;
		}

		// Token: 0x06001690 RID: 5776 RVA: 0x0004BF84 File Offset: 0x0004A184
		private static Func<RelationshipManager, RelatedEnd, RelatedEnd> CreateGetRelatedEndMethod<TSource, TTarget>(AssociationEndMember sourceMember, AssociationEndMember targetMember, NavigationPropertyAccessor sourceAccessor, NavigationPropertyAccessor targetAccessor) where TSource : class where TTarget : class
		{
			RelationshipMultiplicity relationshipMultiplicity = targetMember.RelationshipMultiplicity;
			Func<RelationshipManager, RelatedEnd, RelatedEnd> result;
			if (relationshipMultiplicity > RelationshipMultiplicity.One)
			{
				if (relationshipMultiplicity != RelationshipMultiplicity.Many)
				{
					throw EntityUtil.InvalidEnumerationValue(typeof(RelationshipMultiplicity), (int)targetMember.RelationshipMultiplicity);
				}
				result = ((RelationshipManager manager, RelatedEnd relatedEnd) => manager.GetRelatedCollection<TSource, TTarget>(sourceMember.DeclaringType.FullName, sourceMember.Name, targetMember.Name, sourceAccessor, targetAccessor, sourceMember.RelationshipMultiplicity, relatedEnd));
			}
			else
			{
				result = ((RelationshipManager manager, RelatedEnd relatedEnd) => manager.GetRelatedReference<TSource, TTarget>(sourceMember.DeclaringType.FullName, sourceMember.Name, targetMember.Name, sourceAccessor, targetAccessor, sourceMember.RelationshipMultiplicity, relatedEnd));
			}
			return result;
		}

		// Token: 0x06001691 RID: 5777 RVA: 0x0004C003 File Offset: 0x0004A203
		private static void ThrowConstructorNoParameterless(Type type)
		{
			throw EntityUtil.InvalidOperation(Strings.CodeGen_ConstructorNoParameterless(type.FullName));
		}

		// Token: 0x06001692 RID: 5778 RVA: 0x0004C015 File Offset: 0x0004A215
		private static void ThrowPropertyDeclaringTypeIsValueType()
		{
			throw EntityUtil.InvalidOperation(Strings.CodeGen_PropertyDeclaringTypeIsValueType);
		}

		// Token: 0x06001693 RID: 5779 RVA: 0x0004C021 File Offset: 0x0004A221
		private static void ThrowPropertyUnsupportedForm()
		{
			throw EntityUtil.InvalidOperation(Strings.CodeGen_PropertyUnsupportedForm);
		}

		// Token: 0x06001694 RID: 5780 RVA: 0x0004C02D File Offset: 0x0004A22D
		private static void ThrowPropertyUnsupportedType()
		{
			throw EntityUtil.InvalidOperation(Strings.CodeGen_PropertyUnsupportedType);
		}

		// Token: 0x06001695 RID: 5781 RVA: 0x0004C039 File Offset: 0x0004A239
		private static void ThrowPropertyStrongNameIdentity()
		{
			throw EntityUtil.InvalidOperation(Strings.CodeGen_PropertyStrongNameIdentity);
		}

		// Token: 0x06001696 RID: 5782 RVA: 0x0004C045 File Offset: 0x0004A245
		private static void ThrowPropertyIsIndexed()
		{
			throw EntityUtil.InvalidOperation(Strings.CodeGen_PropertyIsIndexed);
		}

		// Token: 0x06001697 RID: 5783 RVA: 0x0004C051 File Offset: 0x0004A251
		private static void ThrowPropertyIsStatic()
		{
			throw EntityUtil.InvalidOperation(Strings.CodeGen_PropertyIsStatic);
		}

		// Token: 0x06001698 RID: 5784 RVA: 0x0004C05D File Offset: 0x0004A25D
		private static void ThrowPropertyNoGetter()
		{
			throw EntityUtil.InvalidOperation(Strings.CodeGen_PropertyNoGetter);
		}

		// Token: 0x06001699 RID: 5785 RVA: 0x0004C069 File Offset: 0x0004A269
		private static void ThrowPropertyNoSetter()
		{
			throw EntityUtil.InvalidOperation(Strings.CodeGen_PropertyNoSetter);
		}

		// Token: 0x0600169A RID: 5786 RVA: 0x0004C078 File Offset: 0x0004A278
		internal static bool HasMemberAccessReflectionPermission()
		{
			bool result;
			try
			{
				LightweightCodeGenerator.MemberAccessReflectionPermission.Demand();
				result = true;
			}
			catch (SecurityException)
			{
				result = false;
			}
			return result;
		}

		// Token: 0x0600169B RID: 5787 RVA: 0x0004C0AC File Offset: 0x0004A2AC
		[SecuritySafeCritical]
		[ReflectionPermission(SecurityAction.Assert, MemberAccess = true)]
		internal static DynamicMethod CreateDynamicMethod(string name, Type returnType, Type[] parameterTypes)
		{
			return new DynamicMethod(name, returnType, parameterTypes, true);
		}

		// Token: 0x04000A5B RID: 2651
		internal static readonly ReflectionPermission MemberAccessReflectionPermission = new ReflectionPermission(ReflectionPermissionFlag.MemberAccess);
	}
}
