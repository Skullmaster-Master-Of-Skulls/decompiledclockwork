using System;
using System.Collections.Generic;
using System.Data.Common.Utils;
using System.Data.Metadata.Edm;
using System.Data.Objects.DataClasses;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Security;
using System.Security.Permissions;
using System.Threading;
using System.Xml.Serialization;

namespace System.Data.Objects.Internal
{
	// Token: 0x02000170 RID: 368
	internal class EntityProxyFactory
	{
		// Token: 0x06001B08 RID: 6920 RVA: 0x0005C4A4 File Offset: 0x0005A6A4
		private static ModuleBuilder GetDynamicModule(EntityType ospaceEntityType)
		{
			Assembly assembly = ospaceEntityType.ClrType.Assembly;
			ModuleBuilder moduleBuilder;
			if (!EntityProxyFactory.s_ModuleBuilders.TryGetValue(assembly, out moduleBuilder))
			{
				AssemblyName assemblyName = new AssemblyName(string.Format(CultureInfo.InvariantCulture, "EntityFrameworkDynamicProxies-{0}", new object[]
				{
					assembly.FullName
				}));
				assemblyName.Version = new Version(1, 0, 0, 0);
				ConstructorInfo constructor = typeof(SecurityTransparentAttribute).GetConstructor(Type.EmptyTypes);
				ConstructorInfo constructor2 = typeof(SecurityRulesAttribute).GetConstructor(new Type[]
				{
					typeof(SecurityRuleSet)
				});
				CustomAttributeBuilder[] assemblyAttributes = new CustomAttributeBuilder[]
				{
					new CustomAttributeBuilder(constructor, new object[0]),
					new CustomAttributeBuilder(constructor2, new object[]
					{
						SecurityRuleSet.Level1
					})
				};
				AssemblyBuilder assemblyBuilder = AppDomain.CurrentDomain.DefineDynamicAssembly(assemblyName, EntityProxyFactory.s_ProxyAssemblyBuilderAccess, assemblyAttributes);
				if (EntityProxyFactory.s_ProxyAssemblyBuilderAccess == AssemblyBuilderAccess.RunAndSave)
				{
					moduleBuilder = assemblyBuilder.DefineDynamicModule("EntityProxyModule", "EntityProxyModule.dll");
				}
				else
				{
					moduleBuilder = assemblyBuilder.DefineDynamicModule("EntityProxyModule");
				}
				EntityProxyFactory.s_ModuleBuilders.Add(assembly, moduleBuilder);
			}
			return moduleBuilder;
		}

		// Token: 0x06001B09 RID: 6921 RVA: 0x0005C5B8 File Offset: 0x0005A7B8
		internal static bool TryGetProxyType(Type clrType, string entityTypeName, out EntityProxyTypeInfo proxyTypeInfo)
		{
			EntityProxyFactory.s_TypeMapLock.EnterReadLock();
			bool result;
			try
			{
				result = EntityProxyFactory.s_ProxyNameMap.TryGetValue(new Tuple<Type, string>(clrType, entityTypeName), out proxyTypeInfo);
			}
			finally
			{
				EntityProxyFactory.s_TypeMapLock.ExitReadLock();
			}
			return result;
		}

		// Token: 0x06001B0A RID: 6922 RVA: 0x0005C600 File Offset: 0x0005A800
		internal static bool TryGetProxyType(Type proxyType, out EntityProxyTypeInfo proxyTypeInfo)
		{
			EntityProxyFactory.s_TypeMapLock.EnterReadLock();
			bool result;
			try
			{
				result = EntityProxyFactory.s_ProxyTypeMap.TryGetValue(proxyType, out proxyTypeInfo);
			}
			finally
			{
				EntityProxyFactory.s_TypeMapLock.ExitReadLock();
			}
			return result;
		}

		// Token: 0x06001B0B RID: 6923 RVA: 0x0005C644 File Offset: 0x0005A844
		internal static bool TryGetProxyWrapper(object instance, out IEntityWrapper wrapper)
		{
			wrapper = null;
			EntityProxyTypeInfo entityProxyTypeInfo;
			if (EntityProxyFactory.IsProxyType(instance.GetType()) && EntityProxyFactory.TryGetProxyType(instance.GetType(), out entityProxyTypeInfo))
			{
				wrapper = entityProxyTypeInfo.GetEntityWrapper(instance);
			}
			return wrapper != null;
		}

		// Token: 0x06001B0C RID: 6924 RVA: 0x0005C680 File Offset: 0x0005A880
		internal static EntityProxyTypeInfo GetProxyType(ClrEntityType ospaceEntityType)
		{
			EntityProxyTypeInfo entityProxyTypeInfo = null;
			if (EntityProxyFactory.TryGetProxyType(ospaceEntityType.ClrType, ospaceEntityType.CSpaceTypeName, out entityProxyTypeInfo))
			{
				if (entityProxyTypeInfo != null)
				{
					entityProxyTypeInfo.ValidateType(ospaceEntityType);
				}
				return entityProxyTypeInfo;
			}
			EntityProxyFactory.s_TypeMapLock.EnterUpgradeableReadLock();
			EntityProxyTypeInfo result;
			try
			{
				result = EntityProxyFactory.TryCreateProxyType(ospaceEntityType);
			}
			finally
			{
				EntityProxyFactory.s_TypeMapLock.ExitUpgradeableReadLock();
			}
			return result;
		}

		// Token: 0x06001B0D RID: 6925 RVA: 0x0005C6E0 File Offset: 0x0005A8E0
		internal static bool TryGetAssociationTypeFromProxyInfo(IEntityWrapper wrappedEntity, string relationshipName, string targetRoleName, out AssociationType associationType)
		{
			EntityProxyTypeInfo entityProxyTypeInfo = null;
			associationType = null;
			return EntityProxyFactory.TryGetProxyType(wrappedEntity.Entity.GetType(), out entityProxyTypeInfo) && entityProxyTypeInfo != null && entityProxyTypeInfo.TryGetNavigationPropertyAssociationType(relationshipName, targetRoleName, out associationType);
		}

		// Token: 0x06001B0E RID: 6926 RVA: 0x0005C714 File Offset: 0x0005A914
		internal static void TryCreateProxyTypes(IEnumerable<EntityType> ospaceEntityTypes)
		{
			EntityProxyFactory.s_TypeMapLock.EnterUpgradeableReadLock();
			try
			{
				foreach (EntityType ospaceEntityType in ospaceEntityTypes)
				{
					EntityProxyFactory.TryCreateProxyType(ospaceEntityType);
				}
			}
			finally
			{
				EntityProxyFactory.s_TypeMapLock.ExitUpgradeableReadLock();
			}
		}

		// Token: 0x06001B0F RID: 6927 RVA: 0x0005C780 File Offset: 0x0005A980
		private static EntityProxyTypeInfo TryCreateProxyType(EntityType ospaceEntityType)
		{
			ClrEntityType clrEntityType = (ClrEntityType)ospaceEntityType;
			Tuple<Type, string> key = new Tuple<Type, string>(clrEntityType.ClrType, clrEntityType.HashedDescription);
			EntityProxyTypeInfo entityProxyTypeInfo;
			if (!EntityProxyFactory.s_ProxyNameMap.TryGetValue(key, out entityProxyTypeInfo) && EntityProxyFactory.CanProxyType(ospaceEntityType))
			{
				ModuleBuilder dynamicModule = EntityProxyFactory.GetDynamicModule(ospaceEntityType);
				entityProxyTypeInfo = EntityProxyFactory.BuildType(dynamicModule, clrEntityType);
				EntityProxyFactory.s_TypeMapLock.EnterWriteLock();
				try
				{
					EntityProxyFactory.s_ProxyNameMap[key] = entityProxyTypeInfo;
					if (entityProxyTypeInfo != null)
					{
						EntityProxyFactory.s_ProxyTypeMap[entityProxyTypeInfo.ProxyType] = entityProxyTypeInfo;
					}
				}
				finally
				{
					EntityProxyFactory.s_TypeMapLock.ExitWriteLock();
				}
			}
			return entityProxyTypeInfo;
		}

		// Token: 0x06001B10 RID: 6928 RVA: 0x0005C814 File Offset: 0x0005AA14
		internal static bool IsProxyType(Type type)
		{
			return type != null && EntityProxyFactory.ProxyRuntimeAssemblies.Contains(type.Assembly);
		}

		// Token: 0x06001B11 RID: 6929 RVA: 0x0005C834 File Offset: 0x0005AA34
		internal static IEnumerable<Type> GetKnownProxyTypes()
		{
			EntityProxyFactory.s_TypeMapLock.EnterReadLock();
			IEnumerable<Type> result;
			try
			{
				IEnumerable<Type> source = from info in EntityProxyFactory.s_ProxyNameMap.Values
				where info != null
				select info.ProxyType;
				result = source.ToArray<Type>();
			}
			finally
			{
				EntityProxyFactory.s_TypeMapLock.ExitReadLock();
			}
			return result;
		}

		// Token: 0x06001B12 RID: 6930 RVA: 0x0005C8C4 File Offset: 0x0005AAC4
		public Func<object, object> CreateBaseGetter(Type declaringType, PropertyInfo propertyInfo)
		{
			ParameterExpression parameterExpression;
			Func<object, object> nonProxyGetter = Expression.Lambda<Func<object, object>>(Expression.PropertyOrField(Expression.Convert(parameterExpression, declaringType), propertyInfo.Name), new ParameterExpression[]
			{
				parameterExpression
			}).Compile();
			string propertyName = propertyInfo.Name;
			return delegate(object entity)
			{
				Type type = entity.GetType();
				object result;
				if (EntityProxyFactory.IsProxyType(type) && EntityProxyFactory.TryGetBasePropertyValue(type, propertyName, entity, out result))
				{
					return result;
				}
				return nonProxyGetter(entity);
			};
		}

		// Token: 0x06001B13 RID: 6931 RVA: 0x0005C930 File Offset: 0x0005AB30
		private static bool TryGetBasePropertyValue(Type proxyType, string propertyName, object entity, out object value)
		{
			value = null;
			EntityProxyTypeInfo entityProxyTypeInfo;
			if (EntityProxyFactory.TryGetProxyType(proxyType, out entityProxyTypeInfo) && entityProxyTypeInfo.ContainsBaseGetter(propertyName))
			{
				value = entityProxyTypeInfo.BaseGetter(entity, propertyName);
				return true;
			}
			return false;
		}

		// Token: 0x06001B14 RID: 6932 RVA: 0x0005C968 File Offset: 0x0005AB68
		public Action<object, object> CreateBaseSetter(Type declaringType, PropertyInfo propertyInfo)
		{
			Action<object, object> nonProxySetter = LightweightCodeGenerator.CreateNavigationPropertySetter(declaringType, propertyInfo);
			string propertyName = propertyInfo.Name;
			return delegate(object entity, object value)
			{
				Type type = entity.GetType();
				if (EntityProxyFactory.IsProxyType(type) && EntityProxyFactory.TrySetBasePropertyValue(type, propertyName, entity, value))
				{
					return;
				}
				nonProxySetter(entity, value);
			};
		}

		// Token: 0x06001B15 RID: 6933 RVA: 0x0005C9A0 File Offset: 0x0005ABA0
		private static bool TrySetBasePropertyValue(Type proxyType, string propertyName, object entity, object value)
		{
			EntityProxyTypeInfo entityProxyTypeInfo;
			if (EntityProxyFactory.TryGetProxyType(proxyType, out entityProxyTypeInfo) && entityProxyTypeInfo.ContainsBaseSetter(propertyName))
			{
				entityProxyTypeInfo.BaseSetter(entity, propertyName, value);
				return true;
			}
			return false;
		}

		// Token: 0x06001B16 RID: 6934 RVA: 0x0005C9D4 File Offset: 0x0005ABD4
		private static EntityProxyTypeInfo BuildType(ModuleBuilder moduleBuilder, ClrEntityType ospaceEntityType)
		{
			EntityProxyFactory.ProxyTypeBuilder proxyTypeBuilder = new EntityProxyFactory.ProxyTypeBuilder(ospaceEntityType);
			Type type = proxyTypeBuilder.CreateType(moduleBuilder);
			EntityProxyTypeInfo entityProxyTypeInfo;
			if (type != null)
			{
				Assembly assembly = type.Assembly;
				if (!EntityProxyFactory.ProxyRuntimeAssemblies.Contains(assembly))
				{
					EntityProxyFactory.ProxyRuntimeAssemblies.Add(assembly);
					EntityProxyFactory.AddAssemblyToResolveList(assembly);
				}
				entityProxyTypeInfo = new EntityProxyTypeInfo(type, ospaceEntityType, proxyTypeBuilder.CreateInitalizeCollectionMethod(type), proxyTypeBuilder.BaseGetters, proxyTypeBuilder.BaseSetters);
				foreach (EdmMember member in proxyTypeBuilder.LazyLoadMembers)
				{
					EntityProxyFactory.InterceptMember(member, type, entityProxyTypeInfo);
				}
				EntityProxyFactory.SetResetFKSetterFlagDelegate(type, entityProxyTypeInfo);
				EntityProxyFactory.SetCompareByteArraysDelegate(type, entityProxyTypeInfo);
			}
			else
			{
				entityProxyTypeInfo = null;
			}
			return entityProxyTypeInfo;
		}

		// Token: 0x06001B17 RID: 6935 RVA: 0x0005CA98 File Offset: 0x0005AC98
		[SecuritySafeCritical]
		private static void AddAssemblyToResolveList(Assembly assembly)
		{
			if (EntityProxyFactory.ProxyRuntimeAssemblies.Contains(assembly))
			{
				ResolveEventHandler value = delegate(object sender, ResolveEventArgs args)
				{
					if (!(args.Name == assembly.FullName))
					{
						return null;
					}
					return assembly;
				};
				AppDomain.CurrentDomain.AssemblyResolve += value;
			}
		}

		// Token: 0x06001B18 RID: 6936 RVA: 0x0005CADC File Offset: 0x0005ACDC
		[MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.NoOptimization)]
		private static void InterceptMember(EdmMember member, Type proxyType, EntityProxyTypeInfo proxyTypeInfo)
		{
			PropertyInfo topProperty = EntityUtil.GetTopProperty(proxyType, member.Name);
			FieldInfo field = proxyType.GetField(LazyLoadImplementor.GetInterceptorFieldName(member.Name), BindingFlags.DeclaredOnly | BindingFlags.Static | BindingFlags.NonPublic);
			Delegate interceptorDelegate = typeof(LazyLoadBehavior).GetMethod("GetInterceptorDelegate", BindingFlags.Static | BindingFlags.NonPublic).MakeGenericMethod(new Type[]
			{
				proxyType,
				topProperty.PropertyType
			}).Invoke(null, new object[]
			{
				member,
				proxyTypeInfo.EntityWrapperDelegate
			}) as Delegate;
			EntityProxyFactory.AssignInterceptionDelegate(interceptorDelegate, field);
		}

		// Token: 0x06001B19 RID: 6937 RVA: 0x0005CB5E File Offset: 0x0005AD5E
		[SecuritySafeCritical]
		[ReflectionPermission(SecurityAction.Assert, MemberAccess = true)]
		[MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.NoOptimization)]
		private static void AssignInterceptionDelegate(Delegate interceptorDelegate, FieldInfo interceptorField)
		{
			interceptorField.SetValue(null, interceptorDelegate);
		}

		// Token: 0x06001B1A RID: 6938 RVA: 0x0005CB68 File Offset: 0x0005AD68
		private static void SetResetFKSetterFlagDelegate(Type proxyType, EntityProxyTypeInfo proxyTypeInfo)
		{
			FieldInfo field = proxyType.GetField("_resetFKSetterFlag", BindingFlags.DeclaredOnly | BindingFlags.Static | BindingFlags.NonPublic);
			Action<object> resetFKSetterFlagDelegate = EntityProxyFactory.GetResetFKSetterFlagDelegate(proxyTypeInfo.EntityWrapperDelegate);
			EntityProxyFactory.AssignInterceptionDelegate(resetFKSetterFlagDelegate, field);
		}

		// Token: 0x06001B1B RID: 6939 RVA: 0x0005CB98 File Offset: 0x0005AD98
		private static Action<object> GetResetFKSetterFlagDelegate(Func<object, object> getEntityWrapperDelegate)
		{
			return delegate(object proxy)
			{
				EntityProxyFactory.ResetFKSetterFlag(getEntityWrapperDelegate(proxy));
			};
		}

		// Token: 0x06001B1C RID: 6940 RVA: 0x0005CBC0 File Offset: 0x0005ADC0
		private static void ResetFKSetterFlag(object wrappedEntityAsObject)
		{
			IEntityWrapper entityWrapper = (IEntityWrapper)wrappedEntityAsObject;
			if (entityWrapper != null && entityWrapper.Context != null)
			{
				entityWrapper.Context.ObjectStateManager.EntityInvokingFKSetter = null;
			}
		}

		// Token: 0x06001B1D RID: 6941 RVA: 0x0005CBF0 File Offset: 0x0005ADF0
		private static void SetCompareByteArraysDelegate(Type proxyType, EntityProxyTypeInfo proxyTypeInfo)
		{
			FieldInfo field = proxyType.GetField("_compareByteArrays", BindingFlags.DeclaredOnly | BindingFlags.Static | BindingFlags.NonPublic);
			EntityProxyFactory.AssignInterceptionDelegate(new Func<object, object, bool>(ByValueEqualityComparer.Default.Equals), field);
		}

		// Token: 0x06001B1E RID: 6942 RVA: 0x0005CC24 File Offset: 0x0005AE24
		private static bool CanProxyType(EntityType ospaceEntityType)
		{
			TypeAttributes typeAttributes = ospaceEntityType.ClrType.Attributes & TypeAttributes.VisibilityMask;
			ConstructorInfo constructor = ospaceEntityType.ClrType.GetConstructor(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.CreateInstance, null, Type.EmptyTypes, null);
			bool flag = constructor != null && ((constructor.Attributes & MethodAttributes.MemberAccessMask) == MethodAttributes.Public || (constructor.Attributes & MethodAttributes.MemberAccessMask) == MethodAttributes.Family || (constructor.Attributes & MethodAttributes.MemberAccessMask) == MethodAttributes.FamORAssem);
			return !ospaceEntityType.Abstract && !ospaceEntityType.ClrType.IsSealed && !typeof(IEntityWithRelationships).IsAssignableFrom(ospaceEntityType.ClrType) && flag && typeAttributes == TypeAttributes.Public;
		}

		// Token: 0x06001B1F RID: 6943 RVA: 0x0005CCC0 File Offset: 0x0005AEC0
		private static bool CanProxyMethod(MethodInfo method)
		{
			bool result = false;
			if (method != null)
			{
				MethodAttributes methodAttributes = method.Attributes & MethodAttributes.MemberAccessMask;
				result = (method.IsVirtual && !method.IsFinal && (methodAttributes == MethodAttributes.Public || methodAttributes == MethodAttributes.Family || methodAttributes == MethodAttributes.FamORAssem));
			}
			return result;
		}

		// Token: 0x06001B20 RID: 6944 RVA: 0x0005CD05 File Offset: 0x0005AF05
		internal static bool CanProxyGetter(PropertyInfo clrProperty)
		{
			return EntityProxyFactory.CanProxyMethod(clrProperty.GetGetMethod(true));
		}

		// Token: 0x06001B21 RID: 6945 RVA: 0x0005CD13 File Offset: 0x0005AF13
		internal static bool CanProxySetter(PropertyInfo clrProperty)
		{
			return EntityProxyFactory.CanProxyMethod(clrProperty.GetSetMethod(true));
		}

		// Token: 0x04000B38 RID: 2872
		private const string ProxyTypeNameFormat = "System.Data.Entity.DynamicProxies.{0}_{1}";

		// Token: 0x04000B39 RID: 2873
		internal const string ResetFKSetterFlagFieldName = "_resetFKSetterFlag";

		// Token: 0x04000B3A RID: 2874
		internal const string CompareByteArraysFieldName = "_compareByteArrays";

		// Token: 0x04000B3B RID: 2875
		private static AssemblyBuilderAccess s_ProxyAssemblyBuilderAccess = AssemblyBuilderAccess.Run;

		// Token: 0x04000B3C RID: 2876
		private static Dictionary<Tuple<Type, string>, EntityProxyTypeInfo> s_ProxyNameMap = new Dictionary<Tuple<Type, string>, EntityProxyTypeInfo>();

		// Token: 0x04000B3D RID: 2877
		private static Dictionary<Type, EntityProxyTypeInfo> s_ProxyTypeMap = new Dictionary<Type, EntityProxyTypeInfo>();

		// Token: 0x04000B3E RID: 2878
		private static Dictionary<Assembly, ModuleBuilder> s_ModuleBuilders = new Dictionary<Assembly, ModuleBuilder>();

		// Token: 0x04000B3F RID: 2879
		private static ReaderWriterLockSlim s_TypeMapLock = new ReaderWriterLockSlim();

		// Token: 0x04000B40 RID: 2880
		private static HashSet<Assembly> ProxyRuntimeAssemblies = new HashSet<Assembly>();

		// Token: 0x020004BB RID: 1211
		private class ProxyTypeBuilder
		{
			// Token: 0x06003C92 RID: 15506 RVA: 0x000E3654 File Offset: 0x000E1854
			public ProxyTypeBuilder(ClrEntityType ospaceEntityType)
			{
				this._ospaceEntityType = ospaceEntityType;
				this._baseImplementor = new BaseProxyImplementor();
				this._ipocoImplementor = new IPOCOImplementor(ospaceEntityType);
				this._lazyLoadImplementor = new LazyLoadImplementor(ospaceEntityType);
				this._dataContractImplementor = new DataContractImplementor(ospaceEntityType);
				this._iserializableImplementor = new ISerializableImplementor(ospaceEntityType);
			}

			// Token: 0x17000AF5 RID: 2805
			// (get) Token: 0x06003C93 RID: 15507 RVA: 0x000E36B5 File Offset: 0x000E18B5
			public Type BaseType
			{
				get
				{
					return this._ospaceEntityType.ClrType;
				}
			}

			// Token: 0x06003C94 RID: 15508 RVA: 0x000E36C2 File Offset: 0x000E18C2
			public DynamicMethod CreateInitalizeCollectionMethod(Type proxyType)
			{
				return this._ipocoImplementor.CreateInitalizeCollectionMethod(proxyType);
			}

			// Token: 0x17000AF6 RID: 2806
			// (get) Token: 0x06003C95 RID: 15509 RVA: 0x000E36D0 File Offset: 0x000E18D0
			public List<PropertyInfo> BaseGetters
			{
				get
				{
					return this._baseImplementor.BaseGetters;
				}
			}

			// Token: 0x17000AF7 RID: 2807
			// (get) Token: 0x06003C96 RID: 15510 RVA: 0x000E36DD File Offset: 0x000E18DD
			public List<PropertyInfo> BaseSetters
			{
				get
				{
					return this._baseImplementor.BaseSetters;
				}
			}

			// Token: 0x17000AF8 RID: 2808
			// (get) Token: 0x06003C97 RID: 15511 RVA: 0x000E36EA File Offset: 0x000E18EA
			public IEnumerable<EdmMember> LazyLoadMembers
			{
				get
				{
					return this._lazyLoadImplementor.Members;
				}
			}

			// Token: 0x06003C98 RID: 15512 RVA: 0x000E36F8 File Offset: 0x000E18F8
			public Type CreateType(ModuleBuilder moduleBuilder)
			{
				this._moduleBuilder = moduleBuilder;
				bool flag = false;
				if (this._iserializableImplementor.TypeIsSuitable)
				{
					foreach (EdmMember edmMember in this._ospaceEntityType.Members)
					{
						if (this._ipocoImplementor.CanProxyMember(edmMember) || this._lazyLoadImplementor.CanProxyMember(edmMember))
						{
							PropertyInfo topProperty = EntityUtil.GetTopProperty(this.BaseType, edmMember.Name);
							PropertyBuilder propertyBuilder = this.TypeBuilder.DefineProperty(edmMember.Name, PropertyAttributes.None, topProperty.PropertyType, Type.EmptyTypes);
							if (!this._ipocoImplementor.EmitMember(this.TypeBuilder, edmMember, propertyBuilder, topProperty, this._baseImplementor))
							{
								this.EmitBaseSetter(this.TypeBuilder, propertyBuilder, topProperty);
							}
							if (!this._lazyLoadImplementor.EmitMember(this.TypeBuilder, edmMember, propertyBuilder, topProperty, this._baseImplementor))
							{
								this.EmitBaseGetter(this.TypeBuilder, propertyBuilder, topProperty);
							}
							flag = true;
						}
					}
					if (this._typeBuilder != null)
					{
						this._baseImplementor.Implement(this.TypeBuilder, new Action<FieldBuilder, bool>(this.RegisterInstanceField));
						this._iserializableImplementor.Implement(this.TypeBuilder, this._serializedFields);
					}
				}
				if (!flag)
				{
					return null;
				}
				return this.TypeBuilder.CreateType();
			}

			// Token: 0x17000AF9 RID: 2809
			// (get) Token: 0x06003C99 RID: 15513 RVA: 0x000E3864 File Offset: 0x000E1A64
			private TypeBuilder TypeBuilder
			{
				get
				{
					if (this._typeBuilder == null)
					{
						TypeAttributes typeAttributes = TypeAttributes.Public | TypeAttributes.Sealed;
						if ((this.BaseType.Attributes & TypeAttributes.Serializable) == TypeAttributes.Serializable)
						{
							typeAttributes |= TypeAttributes.Serializable;
						}
						string text = (this.BaseType.Name.Length <= 20) ? this.BaseType.Name : this.BaseType.Name.Substring(0, 20);
						string name = string.Format(CultureInfo.InvariantCulture, "System.Data.Entity.DynamicProxies.{0}_{1}", new object[]
						{
							text,
							this._ospaceEntityType.HashedDescription
						});
						this._typeBuilder = this._moduleBuilder.DefineType(name, typeAttributes, this.BaseType, this._ipocoImplementor.Interfaces);
						this._typeBuilder.DefineDefaultConstructor(MethodAttributes.FamANDAssem | MethodAttributes.Family | MethodAttributes.HideBySig | MethodAttributes.SpecialName | MethodAttributes.RTSpecialName);
						Action<FieldBuilder, bool> registerField = new Action<FieldBuilder, bool>(this.RegisterInstanceField);
						this._ipocoImplementor.Implement(this._typeBuilder, registerField);
						this._lazyLoadImplementor.Implement(this._typeBuilder, registerField);
						if (!this._iserializableImplementor.TypeImplementsISerializable)
						{
							this._dataContractImplementor.Implement(this._typeBuilder, registerField);
						}
					}
					return this._typeBuilder;
				}
			}

			// Token: 0x06003C9A RID: 15514 RVA: 0x000E3990 File Offset: 0x000E1B90
			private void EmitBaseGetter(TypeBuilder typeBuilder, PropertyBuilder propertyBuilder, PropertyInfo baseProperty)
			{
				if (EntityProxyFactory.CanProxyGetter(baseProperty))
				{
					MethodInfo getMethod = baseProperty.GetGetMethod(true);
					MethodAttributes methodAttributes = getMethod.Attributes & MethodAttributes.MemberAccessMask;
					MethodBuilder methodBuilder = typeBuilder.DefineMethod("get_" + baseProperty.Name, methodAttributes | (MethodAttributes.Virtual | MethodAttributes.HideBySig | MethodAttributes.SpecialName), baseProperty.PropertyType, Type.EmptyTypes);
					ILGenerator ilgenerator = methodBuilder.GetILGenerator();
					ilgenerator.Emit(OpCodes.Ldarg_0);
					ilgenerator.Emit(OpCodes.Call, getMethod);
					ilgenerator.Emit(OpCodes.Ret);
					propertyBuilder.SetGetMethod(methodBuilder);
				}
			}

			// Token: 0x06003C9B RID: 15515 RVA: 0x000E3A10 File Offset: 0x000E1C10
			private void EmitBaseSetter(TypeBuilder typeBuilder, PropertyBuilder propertyBuilder, PropertyInfo baseProperty)
			{
				if (EntityProxyFactory.CanProxySetter(baseProperty))
				{
					MethodInfo setMethod = baseProperty.GetSetMethod(true);
					MethodAttributes methodAttributes = setMethod.Attributes & MethodAttributes.MemberAccessMask;
					MethodBuilder methodBuilder = typeBuilder.DefineMethod("set_" + baseProperty.Name, methodAttributes | (MethodAttributes.Virtual | MethodAttributes.HideBySig | MethodAttributes.SpecialName), null, new Type[]
					{
						baseProperty.PropertyType
					});
					ILGenerator ilgenerator = methodBuilder.GetILGenerator();
					ilgenerator.Emit(OpCodes.Ldarg_0);
					ilgenerator.Emit(OpCodes.Ldarg_1);
					ilgenerator.Emit(OpCodes.Call, setMethod);
					ilgenerator.Emit(OpCodes.Ret);
					propertyBuilder.SetSetMethod(methodBuilder);
				}
			}

			// Token: 0x06003C9C RID: 15516 RVA: 0x000E3A9F File Offset: 0x000E1C9F
			private void RegisterInstanceField(FieldBuilder field, bool serializable)
			{
				if (serializable)
				{
					this._serializedFields.Add(field);
					return;
				}
				EntityProxyFactory.ProxyTypeBuilder.MarkAsNotSerializable(field);
			}

			// Token: 0x06003C9D RID: 15517 RVA: 0x000E3AB8 File Offset: 0x000E1CB8
			private static Type TryGetScriptIgnoreAttributeType()
			{
				try
				{
					Assembly assembly = Assembly.Load("System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35");
					return assembly.GetType("System.Web.Script.Serialization.ScriptIgnoreAttribute");
				}
				catch
				{
				}
				return null;
			}

			// Token: 0x06003C9E RID: 15518 RVA: 0x000E3AF4 File Offset: 0x000E1CF4
			private static void MarkAsNotSerializable(FieldBuilder field)
			{
				object[] constructorArgs = new object[0];
				field.SetCustomAttribute(new CustomAttributeBuilder(EntityProxyFactory.ProxyTypeBuilder.s_NonSerializedAttributeConstructor, constructorArgs));
				if (field.IsPublic)
				{
					field.SetCustomAttribute(new CustomAttributeBuilder(EntityProxyFactory.ProxyTypeBuilder.s_IgnoreDataMemberAttributeConstructor, constructorArgs));
					field.SetCustomAttribute(new CustomAttributeBuilder(EntityProxyFactory.ProxyTypeBuilder.s_XmlIgnoreAttributeConstructor, constructorArgs));
					if (EntityProxyFactory.ProxyTypeBuilder.s_ScriptIgnoreAttributeConstructor != null)
					{
						field.SetCustomAttribute(new CustomAttributeBuilder(EntityProxyFactory.ProxyTypeBuilder.s_ScriptIgnoreAttributeConstructor, constructorArgs));
					}
				}
			}

			// Token: 0x04001A80 RID: 6784
			private TypeBuilder _typeBuilder;

			// Token: 0x04001A81 RID: 6785
			private BaseProxyImplementor _baseImplementor;

			// Token: 0x04001A82 RID: 6786
			private IPOCOImplementor _ipocoImplementor;

			// Token: 0x04001A83 RID: 6787
			private LazyLoadImplementor _lazyLoadImplementor;

			// Token: 0x04001A84 RID: 6788
			private DataContractImplementor _dataContractImplementor;

			// Token: 0x04001A85 RID: 6789
			private ISerializableImplementor _iserializableImplementor;

			// Token: 0x04001A86 RID: 6790
			private ClrEntityType _ospaceEntityType;

			// Token: 0x04001A87 RID: 6791
			private ModuleBuilder _moduleBuilder;

			// Token: 0x04001A88 RID: 6792
			private List<FieldBuilder> _serializedFields = new List<FieldBuilder>(3);

			// Token: 0x04001A89 RID: 6793
			private static readonly ConstructorInfo s_NonSerializedAttributeConstructor = typeof(NonSerializedAttribute).GetConstructor(Type.EmptyTypes);

			// Token: 0x04001A8A RID: 6794
			private static readonly ConstructorInfo s_IgnoreDataMemberAttributeConstructor = typeof(IgnoreDataMemberAttribute).GetConstructor(Type.EmptyTypes);

			// Token: 0x04001A8B RID: 6795
			private static readonly ConstructorInfo s_XmlIgnoreAttributeConstructor = typeof(XmlIgnoreAttribute).GetConstructor(Type.EmptyTypes);

			// Token: 0x04001A8C RID: 6796
			private static readonly ConstructorInfo s_ScriptIgnoreAttributeConstructor = EntityProxyFactory.ProxyTypeBuilder.TryGetScriptIgnoreAttributeType().GetConstructor(Type.EmptyTypes);
		}
	}
}
