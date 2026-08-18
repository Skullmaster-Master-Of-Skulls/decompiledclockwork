using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Reflection.Emit;
using System.Security;
using System.Security.Permissions;
using System.Threading;

namespace System.Text.RegularExpressions
{
	// Token: 0x02000693 RID: 1683
	internal class RegexTypeCompiler : RegexCompiler
	{
		// Token: 0x06003EA5 RID: 16037 RVA: 0x00104758 File Offset: 0x00102958
		internal RegexTypeCompiler(AssemblyName an, CustomAttributeBuilder[] attribs, string resourceFile)
		{
			new ReflectionPermission(PermissionState.Unrestricted).Assert();
			try
			{
				List<CustomAttributeBuilder> list = new List<CustomAttributeBuilder>();
				ConstructorInfo constructor = typeof(SecurityTransparentAttribute).GetConstructor(Type.EmptyTypes);
				CustomAttributeBuilder item = new CustomAttributeBuilder(constructor, new object[0]);
				list.Add(item);
				ConstructorInfo constructor2 = typeof(SecurityRulesAttribute).GetConstructor(new Type[]
				{
					typeof(SecurityRuleSet)
				});
				CustomAttributeBuilder item2 = new CustomAttributeBuilder(constructor2, new object[]
				{
					SecurityRuleSet.Level2
				});
				list.Add(item2);
				this._assembly = AppDomain.CurrentDomain.DefineDynamicAssembly(an, AssemblyBuilderAccess.RunAndSave, list);
				this._module = this._assembly.DefineDynamicModule(an.Name + ".dll");
				if (attribs != null)
				{
					for (int i = 0; i < attribs.Length; i++)
					{
						this._assembly.SetCustomAttribute(attribs[i]);
					}
				}
				if (resourceFile != null)
				{
					this._assembly.DefineUnmanagedResource(resourceFile);
				}
			}
			finally
			{
				CodeAccessPermission.RevertAssert();
			}
		}

		// Token: 0x06003EA6 RID: 16038 RVA: 0x00104868 File Offset: 0x00102A68
		internal Type FactoryTypeFromCode(RegexCode code, RegexOptions options, string typeprefix)
		{
			this._code = code;
			this._codes = code._codes;
			this._strings = code._strings;
			this._fcPrefix = code._fcPrefix;
			this._bmPrefix = code._bmPrefix;
			this._anchors = code._anchors;
			this._trackcount = code._trackcount;
			this._options = options;
			string str = Interlocked.Increment(ref RegexTypeCompiler._typeCount).ToString(CultureInfo.InvariantCulture);
			string typename = typeprefix + "Runner" + str;
			string typename2 = typeprefix + "Factory" + str;
			this.DefineType(typename, false, typeof(RegexRunner));
			this.DefineMethod("Go", null);
			base.GenerateGo();
			this.BakeMethod();
			this.DefineMethod("FindFirstChar", typeof(bool));
			base.GenerateFindFirstChar();
			this.BakeMethod();
			this.DefineMethod("InitTrackCount", null);
			base.GenerateInitTrackCount();
			this.BakeMethod();
			Type newtype = this.BakeType();
			this.DefineType(typename2, false, typeof(RegexRunnerFactory));
			this.DefineMethod("CreateInstance", typeof(RegexRunner));
			this.GenerateCreateInstance(newtype);
			this.BakeMethod();
			return this.BakeType();
		}

		// Token: 0x06003EA7 RID: 16039 RVA: 0x001049A8 File Offset: 0x00102BA8
		internal void GenerateRegexType(string pattern, RegexOptions opts, string name, bool ispublic, RegexCode code, RegexTree tree, Type factory, TimeSpan matchTimeout)
		{
			FieldInfo ft = this.RegexField("pattern");
			FieldInfo ft2 = this.RegexField("roptions");
			FieldInfo ft3 = this.RegexField("factory");
			FieldInfo field = this.RegexField("caps");
			FieldInfo field2 = this.RegexField("capnames");
			FieldInfo ft4 = this.RegexField("capslist");
			FieldInfo ft5 = this.RegexField("capsize");
			FieldInfo ft6 = this.RegexField("internalMatchTimeout");
			Type[] array = new Type[0];
			this.DefineType(name, ispublic, typeof(Regex));
			this._methbuilder = null;
			MethodAttributes attributes = MethodAttributes.Public;
			ConstructorBuilder constructorBuilder = this._typebuilder.DefineConstructor(attributes, CallingConventions.Standard, array);
			this._ilg = constructorBuilder.GetILGenerator();
			base.Ldthis();
			this._ilg.Emit(OpCodes.Call, typeof(Regex).GetConstructor(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, new Type[0], new ParameterModifier[0]));
			base.Ldthis();
			base.Ldstr(pattern);
			base.Stfld(ft);
			base.Ldthis();
			base.Ldc((int)opts);
			base.Stfld(ft2);
			base.Ldthis();
			base.LdcI8(matchTimeout.Ticks);
			base.Call(typeof(TimeSpan).GetMethod("FromTicks", BindingFlags.Static | BindingFlags.Public));
			base.Stfld(ft6);
			base.Ldthis();
			base.Newobj(factory.GetConstructor(array));
			base.Stfld(ft3);
			if (code._caps != null)
			{
				this.GenerateCreateHashtable(field, code._caps);
			}
			if (tree._capnames != null)
			{
				this.GenerateCreateHashtable(field2, tree._capnames);
			}
			if (tree._capslist != null)
			{
				base.Ldthis();
				base.Ldc(tree._capslist.Length);
				this._ilg.Emit(OpCodes.Newarr, typeof(string));
				base.Stfld(ft4);
				for (int i = 0; i < tree._capslist.Length; i++)
				{
					base.Ldthisfld(ft4);
					base.Ldc(i);
					base.Ldstr(tree._capslist[i]);
					this._ilg.Emit(OpCodes.Stelem_Ref);
				}
			}
			base.Ldthis();
			base.Ldc(code._capsize);
			base.Stfld(ft5);
			base.Ldthis();
			base.Call(typeof(Regex).GetMethod("InitializeReferences", BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic));
			base.Ret();
			this._methbuilder = null;
			attributes = MethodAttributes.Public;
			ConstructorBuilder constructorBuilder2 = this._typebuilder.DefineConstructor(attributes, CallingConventions.Standard, new Type[]
			{
				typeof(TimeSpan)
			});
			this._ilg = constructorBuilder2.GetILGenerator();
			base.Ldthis();
			this._ilg.Emit(OpCodes.Call, constructorBuilder);
			this._ilg.Emit(OpCodes.Ldarg_1);
			base.Call(typeof(Regex).GetMethod("ValidateMatchTimeout", BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic));
			base.Ldthis();
			this._ilg.Emit(OpCodes.Ldarg_1);
			base.Stfld(ft6);
			base.Ret();
			this._typebuilder.CreateType();
			this._ilg = null;
			this._typebuilder = null;
		}

		// Token: 0x06003EA8 RID: 16040 RVA: 0x00104CC8 File Offset: 0x00102EC8
		internal void GenerateCreateHashtable(FieldInfo field, Hashtable ht)
		{
			MethodInfo method = typeof(Hashtable).GetMethod("Add", BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
			base.Ldthis();
			base.Newobj(typeof(Hashtable).GetConstructor(new Type[0]));
			base.Stfld(field);
			IDictionaryEnumerator enumerator = ht.GetEnumerator();
			while (enumerator.MoveNext())
			{
				base.Ldthisfld(field);
				if (enumerator.Key is int)
				{
					base.Ldc((int)enumerator.Key);
					this._ilg.Emit(OpCodes.Box, typeof(int));
				}
				else
				{
					base.Ldstr((string)enumerator.Key);
				}
				base.Ldc((int)enumerator.Value);
				this._ilg.Emit(OpCodes.Box, typeof(int));
				base.Callvirt(method);
			}
		}

		// Token: 0x06003EA9 RID: 16041 RVA: 0x00104DAF File Offset: 0x00102FAF
		private FieldInfo RegexField(string fieldname)
		{
			return typeof(Regex).GetField(fieldname, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
		}

		// Token: 0x06003EAA RID: 16042 RVA: 0x00104DC3 File Offset: 0x00102FC3
		internal void Save()
		{
			this._assembly.Save(this._assembly.GetName().Name + ".dll");
		}

		// Token: 0x06003EAB RID: 16043 RVA: 0x00104DEA File Offset: 0x00102FEA
		internal void GenerateCreateInstance(Type newtype)
		{
			base.Newobj(newtype.GetConstructor(new Type[0]));
			base.Ret();
		}

		// Token: 0x06003EAC RID: 16044 RVA: 0x00104E04 File Offset: 0x00103004
		internal void DefineType(string typename, bool ispublic, Type inheritfromclass)
		{
			if (ispublic)
			{
				this._typebuilder = this._module.DefineType(typename, TypeAttributes.Public, inheritfromclass);
				return;
			}
			this._typebuilder = this._module.DefineType(typename, TypeAttributes.NotPublic, inheritfromclass);
		}

		// Token: 0x06003EAD RID: 16045 RVA: 0x00104E34 File Offset: 0x00103034
		internal void DefineMethod(string methname, Type returntype)
		{
			MethodAttributes attributes = MethodAttributes.FamANDAssem | MethodAttributes.Family | MethodAttributes.Virtual;
			this._methbuilder = this._typebuilder.DefineMethod(methname, attributes, returntype, null);
			this._ilg = this._methbuilder.GetILGenerator();
		}

		// Token: 0x06003EAE RID: 16046 RVA: 0x00104E6A File Offset: 0x0010306A
		internal void BakeMethod()
		{
			this._methbuilder = null;
		}

		// Token: 0x06003EAF RID: 16047 RVA: 0x00104E74 File Offset: 0x00103074
		internal Type BakeType()
		{
			Type result = this._typebuilder.CreateType();
			this._typebuilder = null;
			return result;
		}

		// Token: 0x04002DBF RID: 11711
		private static int _typeCount = 0;

		// Token: 0x04002DC0 RID: 11712
		private static LocalDataStoreSlot _moduleSlot = Thread.AllocateDataSlot();

		// Token: 0x04002DC1 RID: 11713
		private AssemblyBuilder _assembly;

		// Token: 0x04002DC2 RID: 11714
		private ModuleBuilder _module;

		// Token: 0x04002DC3 RID: 11715
		private TypeBuilder _typebuilder;

		// Token: 0x04002DC4 RID: 11716
		private MethodBuilder _methbuilder;
	}
}
