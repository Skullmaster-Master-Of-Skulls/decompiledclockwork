using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;

namespace System.ServiceModel.ComIntegration
{
	// Token: 0x0200021A RID: 538
	internal class DispatchProxy : IPseudoDispatch, IDisposable
	{
		// Token: 0x06001058 RID: 4184 RVA: 0x0003B650 File Offset: 0x00039850
		private DispatchProxy(ContractDescription contract, IProvideChannelBuilderSettings channelBuilderSettings)
		{
			if (channelBuilderSettings == null)
			{
				throw Fx.AssertAndThrow("channelBuilderSettings cannot be null cannot be null");
			}
			if (contract == null)
			{
				throw Fx.AssertAndThrow("contract cannot be null");
			}
			this.channelBuilderSettings = channelBuilderSettings;
			this.contract = contract;
			this.ProcessContractDescription();
			ComPlusDispatchMethodTrace.Trace(TraceEventType.Verbose, 327712, "TraceCodeComIntegrationDispatchMethod", this.dispToOperationDescription);
		}

		// Token: 0x06001059 RID: 4185 RVA: 0x0003B6CC File Offset: 0x000398CC
		internal static ComProxy Create(IntPtr outer, ContractDescription contract, IProvideChannelBuilderSettings channelBuilderSettings)
		{
			DispatchProxy dispatchProxy = null;
			IntPtr intPtr = IntPtr.Zero;
			ComProxy comProxy = null;
			ComProxy result;
			try
			{
				dispatchProxy = new DispatchProxy(contract, channelBuilderSettings);
				intPtr = OuterProxyWrapper.CreateDispatchProxy(outer, dispatchProxy);
				comProxy = new ComProxy(intPtr, dispatchProxy);
				result = comProxy;
			}
			finally
			{
				if (comProxy == null)
				{
					if (dispatchProxy != null)
					{
						((IDisposable)dispatchProxy).Dispose();
					}
					if (intPtr != IntPtr.Zero)
					{
						Marshal.Release(intPtr);
					}
				}
			}
			return result;
		}

		// Token: 0x0600105A RID: 4186 RVA: 0x0003B734 File Offset: 0x00039934
		private void ProcessContractDescription()
		{
			uint num = 10U;
			Dictionary<string, DispatchProxy.ParamInfo> dictionary = null;
			foreach (OperationDescription operationDescription in this.contract.Operations)
			{
				this.dispToName[num] = operationDescription.Name;
				this.nameToDisp[operationDescription.Name] = num;
				DispatchProxy.MethodInfo methodInfo = null;
				methodInfo = new DispatchProxy.MethodInfo(operationDescription);
				this.dispToOperationDescription[num++] = methodInfo;
				dictionary = new Dictionary<string, DispatchProxy.ParamInfo>();
				bool flag = true;
				foreach (MessageDescription messageDescription in operationDescription.Messages)
				{
					int num2 = 0;
					if (messageDescription.Body.ReturnValue != null)
					{
						if (string.IsNullOrEmpty(messageDescription.Body.ReturnValue.BaseType))
						{
							throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new COMException(SR.GetString("CannotResolveTypeForParamInMessageDescription", new object[]
							{
								"ReturnValue",
								messageDescription.Body.WrapperName,
								messageDescription.Body.WrapperNamespace
							}), HR.DISP_E_MEMBERNOTFOUND));
						}
						messageDescription.Body.ReturnValue.Type = Type.GetType(messageDescription.Body.ReturnValue.BaseType);
					}
					foreach (MessagePartDescription messagePartDescription in messageDescription.Body.Parts)
					{
						uint key = 0U;
						DispatchProxy.ParamInfo paramInfo = null;
						paramInfo = null;
						if (!this.nameToDisp.TryGetValue(messagePartDescription.Name, out key))
						{
							this.dispToName[num] = messagePartDescription.Name;
							this.nameToDisp[messagePartDescription.Name] = num;
							key = num;
							num += 1U;
						}
						if (!dictionary.TryGetValue(messagePartDescription.Name, out paramInfo))
						{
							paramInfo = new DispatchProxy.ParamInfo();
							methodInfo.paramList.Add(paramInfo);
							methodInfo.dispIdToParamInfo[key] = paramInfo;
							if (string.IsNullOrEmpty(messagePartDescription.BaseType))
							{
								throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new COMException(SR.GetString("CannotResolveTypeForParamInMessageDescription", new object[]
								{
									messagePartDescription.Name,
									messageDescription.Body.WrapperName,
									messageDescription.Body.WrapperNamespace
								}), HR.DISP_E_MEMBERNOTFOUND));
							}
							paramInfo.type = Type.GetType(messagePartDescription.BaseType, true);
							paramInfo.name = messagePartDescription.Name;
							dictionary[messagePartDescription.Name] = paramInfo;
							messagePartDescription.Index = num2;
						}
						messagePartDescription.Type = paramInfo.type;
						if (flag)
						{
							paramInfo.inIndex = num2;
						}
						else
						{
							paramInfo.outIndex = num2;
						}
						num2++;
					}
					flag = false;
				}
			}
		}

		// Token: 0x0600105B RID: 4187 RVA: 0x0003BA70 File Offset: 0x00039C70
		void IPseudoDispatch.GetIDsOfNames(uint cNames, [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.LPWStr, SizeParamIndex = 0)] string[] rgszNames, IntPtr pDispID)
		{
			int num = 0;
			while ((long)num < (long)((ulong)cNames))
			{
				uint val;
				if (!this.nameToDisp.TryGetValue(rgszNames[num], out val))
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new COMException(SR.GetString("OperationNotFound", new object[]
					{
						rgszNames[num]
					}), HR.DISP_E_UNKNOWNNAME));
				}
				Marshal.WriteInt32(pDispID, num * 4, (int)val);
				num++;
			}
		}

		// Token: 0x0600105C RID: 4188 RVA: 0x0003BAD4 File Offset: 0x00039CD4
		int IPseudoDispatch.Invoke(uint dispIdMember, uint cArgs, uint cNamedArgs, IntPtr rgvarg, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)] uint[] rgdispidNamedArgs, IntPtr pVarResult, IntPtr pExcepInfo, out uint pArgErr)
		{
			pArgErr = 0U;
			int result;
			try
			{
				if (cNamedArgs > 0U)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new COMException(SR.GetString("NamedArgsNotSupported"), HR.DISP_E_BADPARAMCOUNT));
				}
				DispatchProxy.MethodInfo methodInfo = null;
				if (!this.dispToOperationDescription.TryGetValue(dispIdMember, out methodInfo))
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new COMException(SR.GetString("BadDispID", new object[]
					{
						dispIdMember
					}), HR.DISP_E_MEMBERNOTFOUND));
				}
				object[] array = null;
				object[] array2 = null;
				string action = null;
				if ((long)methodInfo.paramList.Count != (long)((ulong)cArgs))
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new COMException(SR.GetString("BadDispID", new object[]
					{
						dispIdMember
					}), HR.DISP_E_BADPARAMCOUNT));
				}
				array = new object[methodInfo.opDesc.Messages[0].Body.Parts.Count];
				array2 = new object[methodInfo.opDesc.Messages[1].Body.Parts.Count];
				if (cArgs > 0U)
				{
					if (methodInfo.opDesc.Messages[0].Body.Parts.Count > 0)
					{
						for (int i = 0; i < methodInfo.opDesc.Messages[0].Body.Parts.Count; i++)
						{
							array[i] = null;
						}
					}
					if (!methodInfo.opDesc.IsOneWay && methodInfo.opDesc.Messages[1].Body.Parts.Count > 0)
					{
						for (int j = 0; j < methodInfo.opDesc.Messages[1].Body.Parts.Count; j++)
						{
							array2[j] = null;
						}
					}
				}
				action = methodInfo.opDesc.Messages[0].Action;
				int num = 0;
				int num2 = 0;
				while ((long)num2 < (long)((ulong)cArgs))
				{
					if (methodInfo.paramList[num2].inIndex != -1)
					{
						try
						{
							object obj;
							if (!methodInfo.paramList[num2].type.IsArray)
							{
								obj = this.FetchVariant(rgvarg, (int)((ulong)cArgs - (ulong)((long)num2) - 1UL), methodInfo.paramList[num2].type);
							}
							else
							{
								obj = this.FetchVariants(rgvarg, (int)((ulong)cArgs - (ulong)((long)num2) - 1UL), methodInfo.paramList[num2].type);
							}
							array[methodInfo.paramList[num2].inIndex] = obj;
							num++;
						}
						catch (ArgumentNullException)
						{
							throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull(SR.GetString("VariantArrayNull", new object[]
							{
								(long)((ulong)cArgs - (ulong)((long)num2) - 1UL)
							}));
						}
					}
					num2++;
				}
				if (num != array.Length)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new COMException(SR.GetString("BadParamCount"), HR.DISP_E_BADPARAMCOUNT));
				}
				object obj2 = null;
				try
				{
					obj2 = this.SendMessage(methodInfo.opDesc, action, array, array2);
				}
				catch (Exception baseException)
				{
					if (Fx.IsFatal(baseException))
					{
						throw;
					}
					if (pExcepInfo != IntPtr.Zero)
					{
						System.Runtime.InteropServices.ComTypes.EXCEPINFO excepinfo = default(System.Runtime.InteropServices.ComTypes.EXCEPINFO);
						baseException = baseException.GetBaseException();
						excepinfo.bstrDescription = baseException.Message;
						excepinfo.bstrSource = baseException.Source;
						excepinfo.scode = Marshal.GetHRForException(baseException);
						Marshal.StructureToPtr(excepinfo, pExcepInfo, false);
					}
					return HR.DISP_E_EXCEPTION;
				}
				if (!methodInfo.opDesc.IsOneWay)
				{
					if (array2 != null)
					{
						bool[] array3 = new bool[array2.Length];
						uint num3 = 0U;
						while ((ulong)num3 < (ulong)((long)array3.Length))
						{
							array3[(int)num3] = false;
							num3 += 1U;
						}
						int num4 = 0;
						while ((long)num4 < (long)((ulong)cArgs))
						{
							if (methodInfo.paramList[num4].outIndex != -1)
							{
								try
								{
									if (this.IsByRef(rgvarg, (int)((ulong)cArgs - (ulong)((long)num4) - 1UL)))
									{
										this.PopulateByRef(rgvarg, (int)((ulong)cArgs - (ulong)((long)num4) - 1UL), array2[methodInfo.paramList[num4].outIndex]);
									}
								}
								catch (ArgumentNullException)
								{
									throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull(SR.GetString("VariantArrayNull", new object[]
									{
										(long)((ulong)cArgs - (ulong)((long)num4) - 1UL)
									}));
								}
								array3[methodInfo.paramList[num4].outIndex] = true;
							}
							num4++;
						}
					}
					if (obj2 != null && pVarResult != IntPtr.Zero)
					{
						if (!obj2.GetType().IsArray)
						{
							Marshal.GetNativeVariantForObject(obj2, pVarResult);
						}
						else
						{
							Array array4 = obj2 as Array;
							Array array5 = Array.CreateInstance(typeof(object), array4.Length);
							array4.CopyTo(array5, 0);
							Marshal.GetNativeVariantForObject(array5, pVarResult);
						}
					}
				}
				result = HR.S_OK;
			}
			catch (Exception baseException2)
			{
				if (Fx.IsFatal(baseException2))
				{
					throw;
				}
				baseException2 = baseException2.GetBaseException();
				result = Marshal.GetHRForException(baseException2);
			}
			return result;
		}

		// Token: 0x0600105D RID: 4189 RVA: 0x0003C01C File Offset: 0x0003A21C
		private object SendMessage(OperationDescription opDesc, string action, object[] ins, object[] outs)
		{
			ProxyOperationRuntime operationByName = this.channelBuilderSettings.ServiceChannel.ClientRuntime.GetRuntime().GetOperationByName(opDesc.Name);
			if (operationByName == null)
			{
				throw Fx.AssertAndThrow("Operation runtime should not be null");
			}
			return this.channelBuilderSettings.ServiceChannel.Call(action, opDesc.IsOneWay, operationByName, ins, outs);
		}

		// Token: 0x0600105E RID: 4190 RVA: 0x0003C074 File Offset: 0x0003A274
		private object FetchVariant(IntPtr baseArray, int index, Type type)
		{
			if (baseArray == IntPtr.Zero)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("baseArrray");
			}
			uint disp = (uint)(index * Marshal.SizeOf(typeof(TagVariant)));
			object obj = Marshal.GetObjectForNativeVariant(this.GetDisp(baseArray, disp));
			if (type == typeof(int))
			{
				if (obj.GetType() == typeof(short))
				{
					obj = (int)((short)obj);
				}
				else if (obj.GetType() != typeof(int))
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new COMException(SR.GetString("UnsupportedConversion", new object[]
					{
						obj.GetType(),
						type.GetElementType()
					}), HR.DISP_E_TYPEMISMATCH));
				}
			}
			else if (type == typeof(long))
			{
				if (obj.GetType() == typeof(short))
				{
					obj = (long)((short)obj);
				}
				else if (obj.GetType() == typeof(int))
				{
					obj = (long)((int)obj);
				}
				else if (obj.GetType() != typeof(long))
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new COMException(SR.GetString("UnsupportedConversion", new object[]
					{
						obj.GetType(),
						type
					}), HR.DISP_E_TYPEMISMATCH));
				}
			}
			return obj;
		}

		// Token: 0x0600105F RID: 4191 RVA: 0x0003C1F4 File Offset: 0x0003A3F4
		private object FetchVariants(IntPtr baseArray, int index, Type type)
		{
			if (baseArray == IntPtr.Zero)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("baseArrray");
			}
			uint disp = (uint)(index * Marshal.SizeOf(typeof(TagVariant)));
			TagVariant tagVariant = (TagVariant)Marshal.PtrToStructure(this.GetDisp(baseArray, disp), typeof(TagVariant));
			if ((tagVariant.vt & 16396) == 0)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new COMException(SR.GetString("OnlyVariantAllowedByRef"), HR.DISP_E_TYPEMISMATCH));
			}
			TagVariant tagVariant2 = (TagVariant)Marshal.PtrToStructure(tagVariant.ptr, typeof(TagVariant));
			if ((tagVariant2.vt & 24588) == 0)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new COMException(SR.GetString("OnlyByRefVariantSafeArraysAllowed"), HR.DISP_E_TYPEMISMATCH));
			}
			IntPtr ptr = tagVariant2.ptr;
			IntPtr pSafeArray = (IntPtr)Marshal.PtrToStructure(ptr, typeof(IntPtr));
			int num = SafeNativeMethods.SafeArrayGetDim(pSafeArray);
			if (num != 1)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new COMException(SR.GetString("OnlyOneDimensionalSafeArraysAllowed"), HR.DISP_E_TYPEMISMATCH));
			}
			int num2 = SafeNativeMethods.SafeArrayGetElemsize(pSafeArray);
			if (num2 != Marshal.SizeOf(typeof(TagVariant)))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new COMException(SR.GetString("OnlyVariantTypeElementsAllowed"), HR.DISP_E_TYPEMISMATCH));
			}
			int num3 = SafeNativeMethods.SafeArrayGetLBound(pSafeArray, 1);
			if (num3 > 0)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new COMException(SR.GetString("OnlyZeroLBoundAllowed"), HR.DISP_E_TYPEMISMATCH));
			}
			int num4 = SafeNativeMethods.SafeArrayGetUBound(pSafeArray, 1);
			IntPtr aSrcNativeVariant = SafeNativeMethods.SafeArrayAccessData(pSafeArray);
			object result;
			try
			{
				object[] objectsForNativeVariants = Marshal.GetObjectsForNativeVariants(aSrcNativeVariant, num4 + 1);
				Array array = Array.CreateInstance(type.GetElementType(), objectsForNativeVariants.Length);
				if (objectsForNativeVariants.Length == 0)
				{
					result = array;
				}
				else
				{
					if (type.GetElementType() != typeof(int) && type.GetElementType() != typeof(long))
					{
						try
						{
							objectsForNativeVariants.CopyTo(array, 0);
							goto IL_3F9;
						}
						catch (Exception exception)
						{
							if (Fx.IsFatal(exception))
							{
								throw;
							}
							throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new COMException(SR.GetString("UnsupportedConversion", new object[]
							{
								objectsForNativeVariants[0].GetType(),
								type.GetElementType()
							}), HR.DISP_E_TYPEMISMATCH));
						}
					}
					if (type.GetElementType() == typeof(int))
					{
						for (int i = 0; i < objectsForNativeVariants.Length; i++)
						{
							if (objectsForNativeVariants[i].GetType() == typeof(short))
							{
								array.SetValue((int)((short)objectsForNativeVariants[i]), i);
							}
							else
							{
								if (!(objectsForNativeVariants[i].GetType() == typeof(int)))
								{
									throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new COMException(SR.GetString("UnsupportedConversion", new object[]
									{
										objectsForNativeVariants[i].GetType(),
										type.GetElementType()
									}), HR.DISP_E_TYPEMISMATCH));
								}
								array.SetValue(objectsForNativeVariants[i], i);
							}
						}
					}
					else
					{
						for (int j = 0; j < objectsForNativeVariants.Length; j++)
						{
							if (objectsForNativeVariants[j].GetType() == typeof(short))
							{
								array.SetValue((long)((short)objectsForNativeVariants[j]), j);
							}
							else if (objectsForNativeVariants[j].GetType() == typeof(int))
							{
								array.SetValue((long)((int)objectsForNativeVariants[j]), j);
							}
							else
							{
								if (!(objectsForNativeVariants[j].GetType() == typeof(long)))
								{
									throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new COMException(SR.GetString("UnsupportedConversion", new object[]
									{
										objectsForNativeVariants[j].GetType(),
										type.GetElementType()
									}), HR.DISP_E_TYPEMISMATCH));
								}
								array.SetValue(objectsForNativeVariants[j], j);
							}
						}
					}
					IL_3F9:
					result = array;
				}
			}
			finally
			{
				SafeNativeMethods.SafeArrayUnaccessData(pSafeArray);
			}
			return result;
		}

		// Token: 0x06001060 RID: 4192 RVA: 0x0003C640 File Offset: 0x0003A840
		private IntPtr GetDisp(IntPtr baseAddress, uint disp)
		{
			long num = (long)baseAddress;
			num += (long)((ulong)disp);
			return (IntPtr)num;
		}

		// Token: 0x06001061 RID: 4193 RVA: 0x0003C660 File Offset: 0x0003A860
		private void PopulateByRef(IntPtr baseArray, int index, object val)
		{
			if (val != null)
			{
				if (baseArray == IntPtr.Zero)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("baseArrray");
				}
				uint disp = (uint)(index * Marshal.SizeOf(typeof(TagVariant)));
				TagVariant tagVariant = (TagVariant)Marshal.PtrToStructure(this.GetDisp(baseArray, disp), typeof(TagVariant));
				if ((tagVariant.vt & 12) == 0)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new COMException(SR.GetString("OnlyVariantAllowedByRef"), HR.DISP_E_TYPEMISMATCH));
				}
				if (!val.GetType().IsArray)
				{
					Marshal.GetNativeVariantForObject(val, tagVariant.ptr);
					return;
				}
				Array array = val as Array;
				Array array2 = Array.CreateInstance(typeof(object), array.Length);
				array.CopyTo(array2, 0);
				Marshal.GetNativeVariantForObject(array2, tagVariant.ptr);
			}
		}

		// Token: 0x06001062 RID: 4194 RVA: 0x0003C734 File Offset: 0x0003A934
		private bool IsByRef(IntPtr baseArray, int index)
		{
			if (baseArray == IntPtr.Zero)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("baseArrray");
			}
			uint disp = (uint)(index * Marshal.SizeOf(typeof(TagVariant)));
			ushort num = (ushort)Marshal.ReadInt16(this.GetDisp(baseArray, disp));
			return (num & 16384) != 0;
		}

		// Token: 0x06001063 RID: 4195 RVA: 0x0003C78B File Offset: 0x0003A98B
		void IDisposable.Dispose()
		{
			this.dispToName.Clear();
			this.nameToDisp.Clear();
			this.dispToOperationDescription.Clear();
		}

		// Token: 0x04001873 RID: 6259
		private ContractDescription contract;

		// Token: 0x04001874 RID: 6260
		private IProvideChannelBuilderSettings channelBuilderSettings;

		// Token: 0x04001875 RID: 6261
		private Dictionary<uint, string> dispToName = new Dictionary<uint, string>();

		// Token: 0x04001876 RID: 6262
		private Dictionary<string, uint> nameToDisp = new Dictionary<string, uint>();

		// Token: 0x04001877 RID: 6263
		private Dictionary<uint, DispatchProxy.MethodInfo> dispToOperationDescription = new Dictionary<uint, DispatchProxy.MethodInfo>();

		// Token: 0x02000B0F RID: 2831
		[Serializable]
		internal class ParamInfo
		{
			// Token: 0x06006F73 RID: 28531 RVA: 0x0019DCE1 File Offset: 0x0019BEE1
			public ParamInfo()
			{
				this.inIndex = -1;
				this.outIndex = -1;
			}

			// Token: 0x04003FA2 RID: 16290
			public int inIndex;

			// Token: 0x04003FA3 RID: 16291
			public int outIndex;

			// Token: 0x04003FA4 RID: 16292
			public string name;

			// Token: 0x04003FA5 RID: 16293
			public Type type;
		}

		// Token: 0x02000B10 RID: 2832
		internal class MethodInfo
		{
			// Token: 0x06006F74 RID: 28532 RVA: 0x0019DCF7 File Offset: 0x0019BEF7
			public MethodInfo(OperationDescription opDesc)
			{
				this.opDesc = opDesc;
				this.paramList = new List<DispatchProxy.ParamInfo>();
				this.dispIdToParamInfo = new Dictionary<uint, DispatchProxy.ParamInfo>();
			}

			// Token: 0x04003FA6 RID: 16294
			public OperationDescription opDesc;

			// Token: 0x04003FA7 RID: 16295
			public List<DispatchProxy.ParamInfo> paramList;

			// Token: 0x04003FA8 RID: 16296
			public Dictionary<uint, DispatchProxy.ParamInfo> dispIdToParamInfo;

			// Token: 0x04003FA9 RID: 16297
			public DispatchProxy.ParamInfo ReturnVal;
		}
	}
}
