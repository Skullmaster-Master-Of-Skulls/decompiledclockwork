using System;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.InteropServices;
using System.Threading;

namespace System.Windows.Forms.ComponentModel.Com2Interop
{
	// Token: 0x020004B2 RID: 1202
	internal class Com2TypeInfoProcessor
	{
		// Token: 0x06004F54 RID: 20308 RVA: 0x00002843 File Offset: 0x00000A43
		private Com2TypeInfoProcessor()
		{
		}

		// Token: 0x17001375 RID: 4981
		// (get) Token: 0x06004F55 RID: 20309 RVA: 0x00146694 File Offset: 0x00144894
		private static ModuleBuilder ModuleBuilder
		{
			get
			{
				if (Com2TypeInfoProcessor.moduleBuilder == null)
				{
					AppDomain domain = Thread.GetDomain();
					AssemblyBuilder assemblyBuilder = domain.DefineDynamicAssembly(new AssemblyName
					{
						Name = "COM2InteropEmit"
					}, AssemblyBuilderAccess.Run);
					Com2TypeInfoProcessor.moduleBuilder = assemblyBuilder.DefineDynamicModule("COM2Interop.Emit");
				}
				return Com2TypeInfoProcessor.moduleBuilder;
			}
		}

		// Token: 0x06004F56 RID: 20310 RVA: 0x001466E4 File Offset: 0x001448E4
		public static UnsafeNativeMethods.ITypeInfo FindTypeInfo(object obj, bool wantCoClass)
		{
			UnsafeNativeMethods.ITypeInfo typeInfo = null;
			int num = 0;
			while (typeInfo == null && num < 2)
			{
				if (wantCoClass != (num == 0))
				{
					goto IL_28;
				}
				if (obj is NativeMethods.IProvideClassInfo)
				{
					NativeMethods.IProvideClassInfo provideClassInfo = (NativeMethods.IProvideClassInfo)obj;
					try
					{
						typeInfo = provideClassInfo.GetClassInfo();
						goto IL_49;
					}
					catch
					{
						goto IL_49;
					}
					goto IL_28;
				}
				IL_49:
				num++;
				continue;
				IL_28:
				if (obj is UnsafeNativeMethods.IDispatch)
				{
					UnsafeNativeMethods.IDispatch dispatch = (UnsafeNativeMethods.IDispatch)obj;
					try
					{
						typeInfo = dispatch.GetTypeInfo(0, SafeNativeMethods.GetThreadLCID());
					}
					catch
					{
					}
					goto IL_49;
				}
				goto IL_49;
			}
			return typeInfo;
		}

		// Token: 0x06004F57 RID: 20311 RVA: 0x00146764 File Offset: 0x00144964
		public static UnsafeNativeMethods.ITypeInfo[] FindTypeInfos(object obj, bool wantCoClass)
		{
			UnsafeNativeMethods.ITypeInfo[] array = null;
			int num = 0;
			UnsafeNativeMethods.ITypeInfo typeInfo = null;
			if (obj is NativeMethods.IProvideMultipleClassInfo)
			{
				NativeMethods.IProvideMultipleClassInfo provideMultipleClassInfo = (NativeMethods.IProvideMultipleClassInfo)obj;
				if (!NativeMethods.Succeeded(provideMultipleClassInfo.GetMultiTypeInfoCount(ref num)) || num == 0)
				{
					num = 0;
				}
				if (num > 0)
				{
					array = new UnsafeNativeMethods.ITypeInfo[num];
					for (int i = 0; i < num; i++)
					{
						if (!NativeMethods.Failed(provideMultipleClassInfo.GetInfoOfIndex(i, 1, ref typeInfo, 0, 0, IntPtr.Zero, IntPtr.Zero)))
						{
							array[i] = typeInfo;
						}
					}
				}
			}
			if (array == null || array.Length == 0)
			{
				typeInfo = Com2TypeInfoProcessor.FindTypeInfo(obj, wantCoClass);
				if (typeInfo != null)
				{
					array = new UnsafeNativeMethods.ITypeInfo[]
					{
						typeInfo
					};
				}
			}
			return array;
		}

		// Token: 0x06004F58 RID: 20312 RVA: 0x001467F8 File Offset: 0x001449F8
		public static int GetNameDispId(UnsafeNativeMethods.IDispatch obj)
		{
			int result = -1;
			string[] array = null;
			ComNativeDescriptor instance = ComNativeDescriptor.Instance;
			bool flag = false;
			instance.GetPropertyValue(obj, "__id", ref flag);
			if (flag)
			{
				array = new string[]
				{
					"__id"
				};
			}
			else
			{
				instance.GetPropertyValue(obj, -800, ref flag);
				if (flag)
				{
					result = -800;
				}
				else
				{
					instance.GetPropertyValue(obj, "Name", ref flag);
					if (flag)
					{
						array = new string[]
						{
							"Name"
						};
					}
				}
			}
			if (array != null)
			{
				int[] array2 = new int[]
				{
					-1
				};
				Guid empty = Guid.Empty;
				int idsOfNames = obj.GetIDsOfNames(ref empty, array, 1, SafeNativeMethods.GetThreadLCID(), array2);
				if (NativeMethods.Succeeded(idsOfNames))
				{
					result = array2[0];
				}
			}
			return result;
		}

		// Token: 0x06004F59 RID: 20313 RVA: 0x001468A8 File Offset: 0x00144AA8
		public static Com2Properties GetProperties(object obj)
		{
			if (obj == null || !Marshal.IsComObject(obj))
			{
				return null;
			}
			UnsafeNativeMethods.ITypeInfo[] array = Com2TypeInfoProcessor.FindTypeInfos(obj, false);
			if (array == null || array.Length == 0)
			{
				return null;
			}
			int num = -1;
			int num2 = -1;
			ArrayList arrayList = new ArrayList();
			Guid[] array2 = new Guid[array.Length];
			for (int i = 0; i < array.Length; i++)
			{
				UnsafeNativeMethods.ITypeInfo typeInfo = array[i];
				if (typeInfo != null)
				{
					int[] array3 = new int[2];
					Guid guidForTypeInfo = Com2TypeInfoProcessor.GetGuidForTypeInfo(typeInfo, null, array3);
					PropertyDescriptor[] array4 = null;
					bool flag = guidForTypeInfo != Guid.Empty && Com2TypeInfoProcessor.processedLibraries != null && Com2TypeInfoProcessor.processedLibraries.Contains(guidForTypeInfo);
					if (flag)
					{
						Com2TypeInfoProcessor.CachedProperties cachedProperties = (Com2TypeInfoProcessor.CachedProperties)Com2TypeInfoProcessor.processedLibraries[guidForTypeInfo];
						if (array3[0] == cachedProperties.MajorVersion && array3[1] == cachedProperties.MinorVersion)
						{
							array4 = cachedProperties.Properties;
							if (i == 0 && cachedProperties.DefaultIndex != -1)
							{
								num = cachedProperties.DefaultIndex;
							}
						}
						else
						{
							flag = false;
						}
					}
					if (!flag)
					{
						array4 = Com2TypeInfoProcessor.InternalGetProperties(obj, typeInfo, -1, ref num2);
						if (i == 0 && num2 != -1)
						{
							num = num2;
						}
						if (Com2TypeInfoProcessor.processedLibraries == null)
						{
							Com2TypeInfoProcessor.processedLibraries = new Hashtable();
						}
						if (guidForTypeInfo != Guid.Empty)
						{
							Com2TypeInfoProcessor.processedLibraries[guidForTypeInfo] = new Com2TypeInfoProcessor.CachedProperties(array4, (i == 0) ? num : -1, array3[0], array3[1]);
						}
					}
					if (array4 != null)
					{
						arrayList.AddRange(array4);
					}
				}
			}
			Com2PropertyDescriptor[] array5 = new Com2PropertyDescriptor[arrayList.Count];
			arrayList.CopyTo(array5, 0);
			return new Com2Properties(obj, array5, num);
		}

		// Token: 0x06004F5A RID: 20314 RVA: 0x00146A38 File Offset: 0x00144C38
		private static Guid GetGuidForTypeInfo(UnsafeNativeMethods.ITypeInfo typeInfo, Com2TypeInfoProcessor.StructCache structCache, int[] versions)
		{
			IntPtr zero = IntPtr.Zero;
			int typeAttr = typeInfo.GetTypeAttr(ref zero);
			if (!NativeMethods.Succeeded(typeAttr))
			{
				throw new ExternalException(SR.GetString("TYPEINFOPROCESSORGetTypeAttrFailed", new object[]
				{
					typeAttr
				}), typeAttr);
			}
			Guid result = Guid.Empty;
			NativeMethods.tagTYPEATTR tagTYPEATTR = null;
			try
			{
				if (structCache == null)
				{
					tagTYPEATTR = new NativeMethods.tagTYPEATTR();
				}
				else
				{
					tagTYPEATTR = (NativeMethods.tagTYPEATTR)structCache.GetStruct(typeof(NativeMethods.tagTYPEATTR));
				}
				UnsafeNativeMethods.PtrToStructure(zero, tagTYPEATTR);
				result = tagTYPEATTR.guid;
				if (versions != null)
				{
					versions[0] = (int)tagTYPEATTR.wMajorVerNum;
					versions[1] = (int)tagTYPEATTR.wMinorVerNum;
				}
			}
			finally
			{
				typeInfo.ReleaseTypeAttr(zero);
				if (structCache != null && tagTYPEATTR != null)
				{
					structCache.ReleaseStruct(tagTYPEATTR);
				}
			}
			return result;
		}

		// Token: 0x06004F5B RID: 20315 RVA: 0x00146AF0 File Offset: 0x00144CF0
		private static Type GetValueTypeFromTypeDesc(NativeMethods.tagTYPEDESC typeDesc, UnsafeNativeMethods.ITypeInfo typeInfo, object[] typeData, Com2TypeInfoProcessor.StructCache structCache)
		{
			NativeMethods.tagVT vt = (NativeMethods.tagVT)typeDesc.vt;
			if (vt > NativeMethods.tagVT.VT_UNKNOWN)
			{
				IntPtr unionMember;
				if (vt != NativeMethods.tagVT.VT_PTR)
				{
					if (vt != NativeMethods.tagVT.VT_USERDEFINED)
					{
						goto IL_2A;
					}
					unionMember = typeDesc.unionMember;
				}
				else
				{
					NativeMethods.tagTYPEDESC tagTYPEDESC = (NativeMethods.tagTYPEDESC)structCache.GetStruct(typeof(NativeMethods.tagTYPEDESC));
					try
					{
						try
						{
							UnsafeNativeMethods.PtrToStructure(typeDesc.unionMember, tagTYPEDESC);
						}
						catch
						{
							tagTYPEDESC = new NativeMethods.tagTYPEDESC();
							tagTYPEDESC.unionMember = (IntPtr)Marshal.ReadInt32(typeDesc.unionMember);
							tagTYPEDESC.vt = Marshal.ReadInt16(typeDesc.unionMember, 4);
						}
						if (tagTYPEDESC.vt == 12)
						{
							return Com2TypeInfoProcessor.VTToType((NativeMethods.tagVT)tagTYPEDESC.vt);
						}
						unionMember = tagTYPEDESC.unionMember;
					}
					finally
					{
						structCache.ReleaseStruct(tagTYPEDESC);
					}
				}
				UnsafeNativeMethods.ITypeInfo typeInfo2 = null;
				int num = typeInfo.GetRefTypeInfo(unionMember, ref typeInfo2);
				if (!NativeMethods.Succeeded(num))
				{
					throw new ExternalException(SR.GetString("TYPEINFOPROCESSORGetRefTypeInfoFailed", new object[]
					{
						num
					}), num);
				}
				try
				{
					if (typeInfo2 != null)
					{
						IntPtr zero = IntPtr.Zero;
						num = typeInfo2.GetTypeAttr(ref zero);
						if (!NativeMethods.Succeeded(num))
						{
							throw new ExternalException(SR.GetString("TYPEINFOPROCESSORGetTypeAttrFailed", new object[]
							{
								num
							}), num);
						}
						NativeMethods.tagTYPEATTR tagTYPEATTR = (NativeMethods.tagTYPEATTR)structCache.GetStruct(typeof(NativeMethods.tagTYPEATTR));
						UnsafeNativeMethods.PtrToStructure(zero, tagTYPEATTR);
						try
						{
							Guid guid = tagTYPEATTR.guid;
							if (!Guid.Empty.Equals(guid))
							{
								typeData[0] = guid;
							}
							switch (tagTYPEATTR.typekind)
							{
							case 0:
								return Com2TypeInfoProcessor.ProcessTypeInfoEnum(typeInfo2, structCache);
							case 3:
							case 5:
								return Com2TypeInfoProcessor.VTToType(NativeMethods.tagVT.VT_UNKNOWN);
							case 4:
								return Com2TypeInfoProcessor.VTToType(NativeMethods.tagVT.VT_DISPATCH);
							case 6:
								return Com2TypeInfoProcessor.GetValueTypeFromTypeDesc(tagTYPEATTR.Get_tdescAlias(), typeInfo2, typeData, structCache);
							}
							return null;
						}
						finally
						{
							typeInfo2.ReleaseTypeAttr(zero);
							structCache.ReleaseStruct(tagTYPEATTR);
						}
					}
				}
				finally
				{
					typeInfo2 = null;
				}
				return null;
			}
			if (vt == NativeMethods.tagVT.VT_DISPATCH || vt == NativeMethods.tagVT.VT_UNKNOWN)
			{
				typeData[0] = Com2TypeInfoProcessor.GetGuidForTypeInfo(typeInfo, structCache, null);
				return Com2TypeInfoProcessor.VTToType((NativeMethods.tagVT)typeDesc.vt);
			}
			IL_2A:
			return Com2TypeInfoProcessor.VTToType((NativeMethods.tagVT)typeDesc.vt);
		}

		// Token: 0x06004F5C RID: 20316 RVA: 0x00146D4C File Offset: 0x00144F4C
		private static PropertyDescriptor[] InternalGetProperties(object obj, UnsafeNativeMethods.ITypeInfo typeInfo, int dispidToGet, ref int defaultIndex)
		{
			if (typeInfo == null)
			{
				return null;
			}
			Hashtable hashtable = new Hashtable();
			int nameDispId = Com2TypeInfoProcessor.GetNameDispId((UnsafeNativeMethods.IDispatch)obj);
			bool flag = false;
			Com2TypeInfoProcessor.StructCache structCache = new Com2TypeInfoProcessor.StructCache();
			try
			{
				Com2TypeInfoProcessor.ProcessFunctions(typeInfo, hashtable, dispidToGet, nameDispId, ref flag, structCache);
			}
			catch (ExternalException ex)
			{
			}
			try
			{
				Com2TypeInfoProcessor.ProcessVariables(typeInfo, hashtable, dispidToGet, nameDispId, structCache);
			}
			catch (ExternalException ex2)
			{
			}
			typeInfo = null;
			int num = hashtable.Count;
			if (flag)
			{
				num++;
			}
			PropertyDescriptor[] array = new PropertyDescriptor[num];
			int hr = 0;
			object[] retval = new object[1];
			ComNativeDescriptor instance = ComNativeDescriptor.Instance;
			foreach (object obj2 in hashtable.Values)
			{
				Com2TypeInfoProcessor.PropInfo propInfo = (Com2TypeInfoProcessor.PropInfo)obj2;
				if (!propInfo.NonBrowsable)
				{
					try
					{
						hr = instance.GetPropertyValue(obj, propInfo.DispId, retval);
					}
					catch (ExternalException ex3)
					{
						hr = ex3.ErrorCode;
					}
					if (!NativeMethods.Succeeded(hr))
					{
						propInfo.Attributes.Add(new BrowsableAttribute(false));
						propInfo.NonBrowsable = true;
					}
				}
				else
				{
					hr = 0;
				}
				Attribute[] array2 = new Attribute[propInfo.Attributes.Count];
				propInfo.Attributes.CopyTo(array2, 0);
				array[propInfo.Index] = new Com2PropertyDescriptor(propInfo.DispId, propInfo.Name, array2, propInfo.ReadOnly != 2, propInfo.ValueType, propInfo.TypeData, !NativeMethods.Succeeded(hr));
				if (propInfo.IsDefault)
				{
					int index = propInfo.Index;
				}
			}
			if (flag)
			{
				array[array.Length - 1] = new Com2AboutBoxPropertyDescriptor();
			}
			return array;
		}

		// Token: 0x06004F5D RID: 20317 RVA: 0x00146F24 File Offset: 0x00145124
		private static Com2TypeInfoProcessor.PropInfo ProcessDataCore(UnsafeNativeMethods.ITypeInfo typeInfo, IDictionary propInfoList, int dispid, int nameDispID, NativeMethods.tagTYPEDESC typeDesc, int flags, Com2TypeInfoProcessor.StructCache structCache)
		{
			string text = null;
			string text2 = null;
			int documentation = typeInfo.GetDocumentation(dispid, ref text, ref text2, null, null);
			ComNativeDescriptor instance = ComNativeDescriptor.Instance;
			if (!NativeMethods.Succeeded(documentation))
			{
				throw new COMException(SR.GetString("TYPEINFOPROCESSORGetDocumentationFailed", new object[]
				{
					dispid,
					documentation,
					instance.GetClassName(typeInfo)
				}), documentation);
			}
			if (text == null)
			{
				return null;
			}
			Com2TypeInfoProcessor.PropInfo propInfo = (Com2TypeInfoProcessor.PropInfo)propInfoList[text];
			if (propInfo == null)
			{
				propInfo = new Com2TypeInfoProcessor.PropInfo();
				propInfo.Index = propInfoList.Count;
				propInfoList[text] = propInfo;
				propInfo.Name = text;
				propInfo.DispId = dispid;
				propInfo.Attributes.Add(new DispIdAttribute(propInfo.DispId));
			}
			if (text2 != null)
			{
				propInfo.Attributes.Add(new DescriptionAttribute(text2));
			}
			if (propInfo.ValueType == null)
			{
				object[] array = new object[1];
				try
				{
					propInfo.ValueType = Com2TypeInfoProcessor.GetValueTypeFromTypeDesc(typeDesc, typeInfo, array, structCache);
				}
				catch (Exception ex)
				{
				}
				if (propInfo.ValueType == null)
				{
					propInfo.NonBrowsable = true;
				}
				if (propInfo.NonBrowsable)
				{
					flags |= 1024;
				}
				if (array[0] != null)
				{
					propInfo.TypeData = array[0];
				}
			}
			if ((flags & 1) != 0)
			{
				propInfo.ReadOnly = 1;
			}
			if ((flags & 64) != 0 || (flags & 1024) != 0 || propInfo.Name[0] == '_' || dispid == -515)
			{
				propInfo.Attributes.Add(new BrowsableAttribute(false));
				propInfo.NonBrowsable = true;
			}
			if ((flags & 512) != 0)
			{
				propInfo.IsDefault = true;
			}
			if ((flags & 4) != 0 && (flags & 16) != 0)
			{
				propInfo.Attributes.Add(new BindableAttribute(true));
			}
			if (dispid == nameDispID)
			{
				propInfo.Attributes.Add(new ParenthesizePropertyNameAttribute(true));
				propInfo.Attributes.Add(new MergablePropertyAttribute(false));
			}
			return propInfo;
		}

		// Token: 0x06004F5E RID: 20318 RVA: 0x00147124 File Offset: 0x00145324
		private static void ProcessFunctions(UnsafeNativeMethods.ITypeInfo typeInfo, IDictionary propInfoList, int dispidToGet, int nameDispID, ref bool addAboutBox, Com2TypeInfoProcessor.StructCache structCache)
		{
			IntPtr zero = IntPtr.Zero;
			int num = typeInfo.GetTypeAttr(ref zero);
			if (!NativeMethods.Succeeded(num) || zero == IntPtr.Zero)
			{
				throw new ExternalException(SR.GetString("TYPEINFOPROCESSORGetTypeAttrFailed", new object[]
				{
					num
				}), num);
			}
			NativeMethods.tagTYPEATTR tagTYPEATTR = (NativeMethods.tagTYPEATTR)structCache.GetStruct(typeof(NativeMethods.tagTYPEATTR));
			UnsafeNativeMethods.PtrToStructure(zero, tagTYPEATTR);
			if (tagTYPEATTR == null)
			{
				return;
			}
			NativeMethods.tagFUNCDESC tagFUNCDESC = null;
			NativeMethods.tagELEMDESC tagELEMDESC = null;
			try
			{
				tagFUNCDESC = (NativeMethods.tagFUNCDESC)structCache.GetStruct(typeof(NativeMethods.tagFUNCDESC));
				tagELEMDESC = (NativeMethods.tagELEMDESC)structCache.GetStruct(typeof(NativeMethods.tagELEMDESC));
				for (int i = 0; i < (int)tagTYPEATTR.cFuncs; i++)
				{
					IntPtr zero2 = IntPtr.Zero;
					num = typeInfo.GetFuncDesc(i, ref zero2);
					if (NativeMethods.Succeeded(num) && !(zero2 == IntPtr.Zero))
					{
						UnsafeNativeMethods.PtrToStructure(zero2, tagFUNCDESC);
						try
						{
							if (tagFUNCDESC.invkind == 1 || (dispidToGet != -1 && tagFUNCDESC.memid != dispidToGet))
							{
								if (tagFUNCDESC.memid == -552)
								{
									addAboutBox = true;
								}
							}
							else
							{
								bool flag = tagFUNCDESC.invkind == 2;
								NativeMethods.tagTYPEDESC tdesc;
								if (flag)
								{
									if (tagFUNCDESC.cParams != 0)
									{
										goto IL_194;
									}
									tdesc = tagFUNCDESC.elemdescFunc.tdesc;
								}
								else
								{
									if (tagFUNCDESC.lprgelemdescParam == IntPtr.Zero || tagFUNCDESC.cParams != 1)
									{
										goto IL_194;
									}
									Marshal.PtrToStructure(tagFUNCDESC.lprgelemdescParam, tagELEMDESC);
									tdesc = tagELEMDESC.tdesc;
								}
								Com2TypeInfoProcessor.PropInfo propInfo = Com2TypeInfoProcessor.ProcessDataCore(typeInfo, propInfoList, tagFUNCDESC.memid, nameDispID, tdesc, (int)tagFUNCDESC.wFuncFlags, structCache);
								if (propInfo != null && !flag)
								{
									propInfo.ReadOnly = 2;
								}
							}
						}
						finally
						{
							typeInfo.ReleaseFuncDesc(zero2);
						}
					}
					IL_194:;
				}
			}
			finally
			{
				if (tagFUNCDESC != null)
				{
					structCache.ReleaseStruct(tagFUNCDESC);
				}
				if (tagELEMDESC != null)
				{
					structCache.ReleaseStruct(tagELEMDESC);
				}
				typeInfo.ReleaseTypeAttr(zero);
				structCache.ReleaseStruct(tagTYPEATTR);
			}
		}

		// Token: 0x06004F5F RID: 20319 RVA: 0x00147338 File Offset: 0x00145538
		private static Type ProcessTypeInfoEnum(UnsafeNativeMethods.ITypeInfo enumTypeInfo, Com2TypeInfoProcessor.StructCache structCache)
		{
			if (enumTypeInfo == null)
			{
				return null;
			}
			try
			{
				IntPtr zero = IntPtr.Zero;
				int num = enumTypeInfo.GetTypeAttr(ref zero);
				if (!NativeMethods.Succeeded(num) || zero == IntPtr.Zero)
				{
					throw new ExternalException(SR.GetString("TYPEINFOPROCESSORGetTypeAttrFailed", new object[]
					{
						num
					}), num);
				}
				NativeMethods.tagTYPEATTR tagTYPEATTR = (NativeMethods.tagTYPEATTR)structCache.GetStruct(typeof(NativeMethods.tagTYPEATTR));
				UnsafeNativeMethods.PtrToStructure(zero, tagTYPEATTR);
				if (zero == IntPtr.Zero)
				{
					return null;
				}
				try
				{
					int cVars = (int)tagTYPEATTR.cVars;
					ArrayList arrayList = new ArrayList();
					ArrayList arrayList2 = new ArrayList();
					NativeMethods.tagVARDESC tagVARDESC = (NativeMethods.tagVARDESC)structCache.GetStruct(typeof(NativeMethods.tagVARDESC));
					object value = null;
					string text = null;
					string text2 = null;
					string text3 = null;
					enumTypeInfo.GetDocumentation(-1, ref text, ref text3, null, null);
					for (int i = 0; i < cVars; i++)
					{
						IntPtr zero2 = IntPtr.Zero;
						num = enumTypeInfo.GetVarDesc(i, ref zero2);
						if (NativeMethods.Succeeded(num) && !(zero2 == IntPtr.Zero))
						{
							try
							{
								UnsafeNativeMethods.PtrToStructure(zero2, tagVARDESC);
								if (tagVARDESC != null && tagVARDESC.varkind == 2 && !(tagVARDESC.unionMember == IntPtr.Zero))
								{
									text3 = (text2 = null);
									value = null;
									num = enumTypeInfo.GetDocumentation(tagVARDESC.memid, ref text2, ref text3, null, null);
									if (NativeMethods.Succeeded(num))
									{
										try
										{
											value = Marshal.GetObjectForNativeVariant(tagVARDESC.unionMember);
										}
										catch (Exception ex)
										{
										}
										arrayList2.Add(value);
										string value2;
										if (text3 != null)
										{
											value2 = text3;
										}
										else
										{
											value2 = text2;
										}
										arrayList.Add(value2);
									}
								}
							}
							finally
							{
								if (zero2 != IntPtr.Zero)
								{
									enumTypeInfo.ReleaseVarDesc(zero2);
								}
							}
						}
					}
					structCache.ReleaseStruct(tagVARDESC);
					if (arrayList.Count > 0)
					{
						IntPtr iunknownForObject = Marshal.GetIUnknownForObject(enumTypeInfo);
						try
						{
							text = iunknownForObject.ToString() + "_" + text;
							if (Com2TypeInfoProcessor.builtEnums == null)
							{
								Com2TypeInfoProcessor.builtEnums = new Hashtable();
							}
							else if (Com2TypeInfoProcessor.builtEnums.ContainsKey(text))
							{
								return (Type)Com2TypeInfoProcessor.builtEnums[text];
							}
							Type underlyingType = typeof(int);
							if (arrayList2.Count > 0 && arrayList2[0] != null)
							{
								underlyingType = arrayList2[0].GetType();
							}
							EnumBuilder enumBuilder = Com2TypeInfoProcessor.ModuleBuilder.DefineEnum(text, TypeAttributes.Public, underlyingType);
							for (int j = 0; j < arrayList.Count; j++)
							{
								enumBuilder.DefineLiteral((string)arrayList[j], arrayList2[j]);
							}
							Type type = enumBuilder.CreateType();
							Com2TypeInfoProcessor.builtEnums[text] = type;
							return type;
						}
						finally
						{
							if (iunknownForObject != IntPtr.Zero)
							{
								Marshal.Release(iunknownForObject);
							}
						}
					}
				}
				finally
				{
					enumTypeInfo.ReleaseTypeAttr(zero);
					structCache.ReleaseStruct(tagTYPEATTR);
				}
			}
			catch
			{
			}
			return null;
		}

		// Token: 0x06004F60 RID: 20320 RVA: 0x00147698 File Offset: 0x00145898
		private static void ProcessVariables(UnsafeNativeMethods.ITypeInfo typeInfo, IDictionary propInfoList, int dispidToGet, int nameDispID, Com2TypeInfoProcessor.StructCache structCache)
		{
			IntPtr zero = IntPtr.Zero;
			int num = typeInfo.GetTypeAttr(ref zero);
			if (!NativeMethods.Succeeded(num) || zero == IntPtr.Zero)
			{
				throw new ExternalException(SR.GetString("TYPEINFOPROCESSORGetTypeAttrFailed", new object[]
				{
					num
				}), num);
			}
			NativeMethods.tagTYPEATTR tagTYPEATTR = (NativeMethods.tagTYPEATTR)structCache.GetStruct(typeof(NativeMethods.tagTYPEATTR));
			UnsafeNativeMethods.PtrToStructure(zero, tagTYPEATTR);
			try
			{
				if (tagTYPEATTR != null)
				{
					NativeMethods.tagVARDESC tagVARDESC = (NativeMethods.tagVARDESC)structCache.GetStruct(typeof(NativeMethods.tagVARDESC));
					for (int i = 0; i < (int)tagTYPEATTR.cVars; i++)
					{
						IntPtr zero2 = IntPtr.Zero;
						num = typeInfo.GetVarDesc(i, ref zero2);
						if (NativeMethods.Succeeded(num) && !(zero2 == IntPtr.Zero))
						{
							UnsafeNativeMethods.PtrToStructure(zero2, tagVARDESC);
							try
							{
								if (tagVARDESC.varkind != 2 && (dispidToGet == -1 || tagVARDESC.memid == dispidToGet))
								{
									Com2TypeInfoProcessor.PropInfo propInfo = Com2TypeInfoProcessor.ProcessDataCore(typeInfo, propInfoList, tagVARDESC.memid, nameDispID, tagVARDESC.elemdescVar.tdesc, (int)tagVARDESC.wVarFlags, structCache);
									if (propInfo.ReadOnly != 1)
									{
										propInfo.ReadOnly = 2;
									}
								}
							}
							finally
							{
								if (zero2 != IntPtr.Zero)
								{
									typeInfo.ReleaseVarDesc(zero2);
								}
							}
						}
					}
					structCache.ReleaseStruct(tagVARDESC);
				}
			}
			finally
			{
				typeInfo.ReleaseTypeAttr(zero);
				structCache.ReleaseStruct(tagTYPEATTR);
			}
		}

		// Token: 0x06004F61 RID: 20321 RVA: 0x00147810 File Offset: 0x00145A10
		private static Type VTToType(NativeMethods.tagVT vt)
		{
			if (vt <= NativeMethods.tagVT.VT_VECTOR)
			{
				switch (vt)
				{
				case NativeMethods.tagVT.VT_EMPTY:
				case NativeMethods.tagVT.VT_NULL:
					return null;
				case NativeMethods.tagVT.VT_I2:
					return typeof(short);
				case NativeMethods.tagVT.VT_I4:
				case NativeMethods.tagVT.VT_INT:
					return typeof(int);
				case NativeMethods.tagVT.VT_R4:
					return typeof(float);
				case NativeMethods.tagVT.VT_R8:
					return typeof(double);
				case NativeMethods.tagVT.VT_CY:
					return typeof(decimal);
				case NativeMethods.tagVT.VT_DATE:
					return typeof(DateTime);
				case NativeMethods.tagVT.VT_BSTR:
				case NativeMethods.tagVT.VT_LPSTR:
				case NativeMethods.tagVT.VT_LPWSTR:
					return typeof(string);
				case NativeMethods.tagVT.VT_DISPATCH:
					return typeof(UnsafeNativeMethods.IDispatch);
				case NativeMethods.tagVT.VT_ERROR:
				case NativeMethods.tagVT.VT_HRESULT:
					return typeof(int);
				case NativeMethods.tagVT.VT_BOOL:
					return typeof(bool);
				case NativeMethods.tagVT.VT_VARIANT:
					return typeof(Com2Variant);
				case NativeMethods.tagVT.VT_UNKNOWN:
					return typeof(object);
				case NativeMethods.tagVT.VT_DECIMAL:
				case (NativeMethods.tagVT)15:
				case NativeMethods.tagVT.VT_VOID:
				case NativeMethods.tagVT.VT_PTR:
				case NativeMethods.tagVT.VT_SAFEARRAY:
				case NativeMethods.tagVT.VT_CARRAY:
				case (NativeMethods.tagVT)32:
				case (NativeMethods.tagVT)33:
				case (NativeMethods.tagVT)34:
				case (NativeMethods.tagVT)35:
				case NativeMethods.tagVT.VT_RECORD:
				case (NativeMethods.tagVT)37:
				case (NativeMethods.tagVT)38:
				case (NativeMethods.tagVT)39:
				case (NativeMethods.tagVT)40:
				case (NativeMethods.tagVT)41:
				case (NativeMethods.tagVT)42:
				case (NativeMethods.tagVT)43:
				case (NativeMethods.tagVT)44:
				case (NativeMethods.tagVT)45:
				case (NativeMethods.tagVT)46:
				case (NativeMethods.tagVT)47:
				case (NativeMethods.tagVT)48:
				case (NativeMethods.tagVT)49:
				case (NativeMethods.tagVT)50:
				case (NativeMethods.tagVT)51:
				case (NativeMethods.tagVT)52:
				case (NativeMethods.tagVT)53:
				case (NativeMethods.tagVT)54:
				case (NativeMethods.tagVT)55:
				case (NativeMethods.tagVT)56:
				case (NativeMethods.tagVT)57:
				case (NativeMethods.tagVT)58:
				case (NativeMethods.tagVT)59:
				case (NativeMethods.tagVT)60:
				case (NativeMethods.tagVT)61:
				case (NativeMethods.tagVT)62:
				case (NativeMethods.tagVT)63:
				case NativeMethods.tagVT.VT_BLOB:
				case NativeMethods.tagVT.VT_STREAM:
				case NativeMethods.tagVT.VT_STORAGE:
				case NativeMethods.tagVT.VT_STREAMED_OBJECT:
				case NativeMethods.tagVT.VT_STORED_OBJECT:
				case NativeMethods.tagVT.VT_BLOB_OBJECT:
				case NativeMethods.tagVT.VT_CF:
					break;
				case NativeMethods.tagVT.VT_I1:
					return typeof(sbyte);
				case NativeMethods.tagVT.VT_UI1:
					return typeof(byte);
				case NativeMethods.tagVT.VT_UI2:
					return typeof(ushort);
				case NativeMethods.tagVT.VT_UI4:
				case NativeMethods.tagVT.VT_UINT:
					return typeof(uint);
				case NativeMethods.tagVT.VT_I8:
					return typeof(long);
				case NativeMethods.tagVT.VT_UI8:
					return typeof(ulong);
				case NativeMethods.tagVT.VT_USERDEFINED:
					throw new ArgumentException(SR.GetString("COM2UnhandledVT", new object[]
					{
						"VT_USERDEFINED"
					}));
				case NativeMethods.tagVT.VT_FILETIME:
					return typeof(NativeMethods.FILETIME);
				case NativeMethods.tagVT.VT_CLSID:
					return typeof(Guid);
				default:
					if (vt - NativeMethods.tagVT.VT_BSTR_BLOB > 1)
					{
					}
					break;
				}
			}
			else if (vt != NativeMethods.tagVT.VT_ARRAY && vt != NativeMethods.tagVT.VT_BYREF && vt != NativeMethods.tagVT.VT_RESERVED)
			{
			}
			string name = "COM2UnhandledVT";
			object[] array = new object[1];
			int num = 0;
			int num2 = (int)vt;
			array[num] = num2.ToString(CultureInfo.InvariantCulture);
			throw new ArgumentException(SR.GetString(name, array));
		}

		// Token: 0x04003463 RID: 13411
		private static TraceSwitch DbgTypeInfoProcessorSwitch;

		// Token: 0x04003464 RID: 13412
		private static ModuleBuilder moduleBuilder;

		// Token: 0x04003465 RID: 13413
		private static Hashtable builtEnums;

		// Token: 0x04003466 RID: 13414
		private static Hashtable processedLibraries;

		// Token: 0x02000855 RID: 2133
		internal class CachedProperties
		{
			// Token: 0x060070A3 RID: 28835 RVA: 0x0019D163 File Offset: 0x0019B363
			internal CachedProperties(PropertyDescriptor[] props, int defIndex, int majVersion, int minVersion)
			{
				this.props = this.ClonePropertyDescriptors(props);
				this.MajorVersion = majVersion;
				this.MinorVersion = minVersion;
				this.defaultIndex = defIndex;
			}

			// Token: 0x1700188A RID: 6282
			// (get) Token: 0x060070A4 RID: 28836 RVA: 0x0019D18E File Offset: 0x0019B38E
			public PropertyDescriptor[] Properties
			{
				get
				{
					return this.ClonePropertyDescriptors(this.props);
				}
			}

			// Token: 0x1700188B RID: 6283
			// (get) Token: 0x060070A5 RID: 28837 RVA: 0x0019D19C File Offset: 0x0019B39C
			public int DefaultIndex
			{
				get
				{
					return this.defaultIndex;
				}
			}

			// Token: 0x060070A6 RID: 28838 RVA: 0x0019D1A4 File Offset: 0x0019B3A4
			private PropertyDescriptor[] ClonePropertyDescriptors(PropertyDescriptor[] props)
			{
				PropertyDescriptor[] array = new PropertyDescriptor[props.Length];
				for (int i = 0; i < props.Length; i++)
				{
					if (props[i] is ICloneable)
					{
						array[i] = (PropertyDescriptor)((ICloneable)props[i]).Clone();
					}
					else
					{
						array[i] = props[i];
					}
				}
				return array;
			}

			// Token: 0x04004399 RID: 17305
			private PropertyDescriptor[] props;

			// Token: 0x0400439A RID: 17306
			public readonly int MajorVersion;

			// Token: 0x0400439B RID: 17307
			public readonly int MinorVersion;

			// Token: 0x0400439C RID: 17308
			private int defaultIndex;
		}

		// Token: 0x02000856 RID: 2134
		public class StructCache
		{
			// Token: 0x060070A7 RID: 28839 RVA: 0x0019D1F0 File Offset: 0x0019B3F0
			private Queue GetQueue(Type t, bool create)
			{
				object obj = this.queuedTypes[t];
				if (obj == null && create)
				{
					obj = new Queue();
					this.queuedTypes[t] = obj;
				}
				return (Queue)obj;
			}

			// Token: 0x060070A8 RID: 28840 RVA: 0x0019D22C File Offset: 0x0019B42C
			public object GetStruct(Type t)
			{
				Queue queue = this.GetQueue(t, true);
				object result;
				if (queue.Count == 0)
				{
					result = Activator.CreateInstance(t);
				}
				else
				{
					result = queue.Dequeue();
				}
				return result;
			}

			// Token: 0x060070A9 RID: 28841 RVA: 0x0019D260 File Offset: 0x0019B460
			public void ReleaseStruct(object str)
			{
				Type type = str.GetType();
				Queue queue = this.GetQueue(type, false);
				if (queue != null)
				{
					queue.Enqueue(str);
				}
			}

			// Token: 0x0400439D RID: 17309
			private Hashtable queuedTypes = new Hashtable();
		}

		// Token: 0x02000857 RID: 2135
		private class PropInfo
		{
			// Token: 0x1700188C RID: 6284
			// (get) Token: 0x060070AB RID: 28843 RVA: 0x0019D29A File Offset: 0x0019B49A
			// (set) Token: 0x060070AC RID: 28844 RVA: 0x0019D2A2 File Offset: 0x0019B4A2
			public string Name
			{
				get
				{
					return this.name;
				}
				set
				{
					this.name = value;
				}
			}

			// Token: 0x1700188D RID: 6285
			// (get) Token: 0x060070AD RID: 28845 RVA: 0x0019D2AB File Offset: 0x0019B4AB
			// (set) Token: 0x060070AE RID: 28846 RVA: 0x0019D2B3 File Offset: 0x0019B4B3
			public int DispId
			{
				get
				{
					return this.dispid;
				}
				set
				{
					this.dispid = value;
				}
			}

			// Token: 0x1700188E RID: 6286
			// (get) Token: 0x060070AF RID: 28847 RVA: 0x0019D2BC File Offset: 0x0019B4BC
			// (set) Token: 0x060070B0 RID: 28848 RVA: 0x0019D2C4 File Offset: 0x0019B4C4
			public Type ValueType
			{
				get
				{
					return this.valueType;
				}
				set
				{
					this.valueType = value;
				}
			}

			// Token: 0x1700188F RID: 6287
			// (get) Token: 0x060070B1 RID: 28849 RVA: 0x0019D2CD File Offset: 0x0019B4CD
			public ArrayList Attributes
			{
				get
				{
					return this.attributes;
				}
			}

			// Token: 0x17001890 RID: 6288
			// (get) Token: 0x060070B2 RID: 28850 RVA: 0x0019D2D5 File Offset: 0x0019B4D5
			// (set) Token: 0x060070B3 RID: 28851 RVA: 0x0019D2DD File Offset: 0x0019B4DD
			public int ReadOnly
			{
				get
				{
					return this.readOnly;
				}
				set
				{
					this.readOnly = value;
				}
			}

			// Token: 0x17001891 RID: 6289
			// (get) Token: 0x060070B4 RID: 28852 RVA: 0x0019D2E6 File Offset: 0x0019B4E6
			// (set) Token: 0x060070B5 RID: 28853 RVA: 0x0019D2EE File Offset: 0x0019B4EE
			public bool IsDefault
			{
				get
				{
					return this.isDefault;
				}
				set
				{
					this.isDefault = value;
				}
			}

			// Token: 0x17001892 RID: 6290
			// (get) Token: 0x060070B6 RID: 28854 RVA: 0x0019D2F7 File Offset: 0x0019B4F7
			// (set) Token: 0x060070B7 RID: 28855 RVA: 0x0019D2FF File Offset: 0x0019B4FF
			public object TypeData
			{
				get
				{
					return this.typeData;
				}
				set
				{
					this.typeData = value;
				}
			}

			// Token: 0x17001893 RID: 6291
			// (get) Token: 0x060070B8 RID: 28856 RVA: 0x0019D308 File Offset: 0x0019B508
			// (set) Token: 0x060070B9 RID: 28857 RVA: 0x0019D310 File Offset: 0x0019B510
			public bool NonBrowsable
			{
				get
				{
					return this.nonbrowsable;
				}
				set
				{
					this.nonbrowsable = value;
				}
			}

			// Token: 0x17001894 RID: 6292
			// (get) Token: 0x060070BA RID: 28858 RVA: 0x0019D319 File Offset: 0x0019B519
			// (set) Token: 0x060070BB RID: 28859 RVA: 0x0019D321 File Offset: 0x0019B521
			public int Index
			{
				get
				{
					return this.index;
				}
				set
				{
					this.index = value;
				}
			}

			// Token: 0x060070BC RID: 28860 RVA: 0x0019D32A File Offset: 0x0019B52A
			public override int GetHashCode()
			{
				if (this.name != null)
				{
					return this.name.GetHashCode();
				}
				return base.GetHashCode();
			}

			// Token: 0x0400439E RID: 17310
			public const int ReadOnlyUnknown = 0;

			// Token: 0x0400439F RID: 17311
			public const int ReadOnlyTrue = 1;

			// Token: 0x040043A0 RID: 17312
			public const int ReadOnlyFalse = 2;

			// Token: 0x040043A1 RID: 17313
			private string name;

			// Token: 0x040043A2 RID: 17314
			private int dispid = -1;

			// Token: 0x040043A3 RID: 17315
			private Type valueType;

			// Token: 0x040043A4 RID: 17316
			private readonly ArrayList attributes = new ArrayList();

			// Token: 0x040043A5 RID: 17317
			private int readOnly;

			// Token: 0x040043A6 RID: 17318
			private bool isDefault;

			// Token: 0x040043A7 RID: 17319
			private object typeData;

			// Token: 0x040043A8 RID: 17320
			private bool nonbrowsable;

			// Token: 0x040043A9 RID: 17321
			private int index;
		}
	}
}
