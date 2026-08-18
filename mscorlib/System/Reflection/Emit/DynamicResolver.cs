using System;
using System.Threading;

namespace System.Reflection.Emit
{
	// Token: 0x02000818 RID: 2072
	internal class DynamicResolver : Resolver
	{
		// Token: 0x0600499E RID: 18846 RVA: 0x001003EC File Offset: 0x000FF3EC
		internal DynamicResolver(DynamicILGenerator ilGenerator)
		{
			this.m_stackSize = ilGenerator.GetMaxStackSize();
			this.m_exceptions = ilGenerator.GetExceptions();
			this.m_code = ilGenerator.BakeByteArray();
			this.m_localSignature = ilGenerator.m_localSignature.InternalGetSignatureArray();
			this.m_scope = ilGenerator.m_scope;
			this.m_method = (DynamicMethod)ilGenerator.m_methodBuilder;
			this.m_method.m_resolver = this;
		}

		// Token: 0x0600499F RID: 18847 RVA: 0x00100460 File Offset: 0x000FF460
		internal DynamicResolver(DynamicILInfo dynamicILInfo)
		{
			this.m_stackSize = dynamicILInfo.MaxStackSize;
			this.m_code = dynamicILInfo.Code;
			this.m_localSignature = dynamicILInfo.LocalSignature;
			this.m_exceptionHeader = dynamicILInfo.Exceptions;
			this.m_scope = dynamicILInfo.DynamicScope;
			this.m_method = dynamicILInfo.DynamicMethod;
			this.m_method.m_resolver = this;
		}

		// Token: 0x060049A0 RID: 18848 RVA: 0x001004C8 File Offset: 0x000FF4C8
		protected override void Finalize()
		{
			try
			{
				DynamicMethod method = this.m_method;
				if (method != null)
				{
					if (!method.m_method.IsNullHandle())
					{
						DynamicResolver.DestroyScout destroyScout = null;
						try
						{
							destroyScout = new DynamicResolver.DestroyScout();
						}
						catch
						{
							if (!Environment.HasShutdownStarted && !AppDomain.CurrentDomain.IsFinalizingForUnload())
							{
								GC.ReRegisterForFinalize(this);
							}
							return;
						}
						destroyScout.m_method = method.m_method;
					}
				}
			}
			finally
			{
				base.Finalize();
			}
		}

		// Token: 0x060049A1 RID: 18849 RVA: 0x00100548 File Offset: 0x000FF548
		internal override void GetJitContext(ref int securityControlFlags, ref RuntimeTypeHandle typeOwner)
		{
			DynamicResolver.SecurityControlFlags securityControlFlags2 = DynamicResolver.SecurityControlFlags.Default;
			if (this.m_method.m_restrictedSkipVisibility)
			{
				securityControlFlags2 |= DynamicResolver.SecurityControlFlags.RestrictedSkipVisibilityChecks;
			}
			else if (this.m_method.m_skipVisibility)
			{
				securityControlFlags2 |= DynamicResolver.SecurityControlFlags.SkipVisibilityChecks;
			}
			typeOwner = ((this.m_method.m_typeOwner != null) ? this.m_method.m_typeOwner.TypeHandle : RuntimeTypeHandle.EmptyHandle);
			if (this.m_method.m_creationContext != null)
			{
				securityControlFlags2 |= DynamicResolver.SecurityControlFlags.HasCreationContext;
				if (this.m_method.m_creationContext.CanSkipEvaluation)
				{
					securityControlFlags2 |= DynamicResolver.SecurityControlFlags.CanSkipCSEvaluation;
				}
			}
			securityControlFlags = (int)securityControlFlags2;
		}

		// Token: 0x060049A2 RID: 18850 RVA: 0x001005D0 File Offset: 0x000FF5D0
		internal override byte[] GetCodeInfo(ref int stackSize, ref int initLocals, ref int EHCount)
		{
			stackSize = this.m_stackSize;
			if (this.m_exceptionHeader != null && this.m_exceptionHeader.Length != 0)
			{
				if (this.m_exceptionHeader.Length < 4)
				{
					throw new FormatException();
				}
				byte b = this.m_exceptionHeader[0];
				if ((b & 64) != 0)
				{
					byte[] array = new byte[4];
					for (int i = 0; i < 3; i++)
					{
						array[i] = this.m_exceptionHeader[i + 1];
					}
					EHCount = (BitConverter.ToInt32(array, 0) - 4) / 24;
				}
				else
				{
					EHCount = (int)((this.m_exceptionHeader[1] - 2) / 12);
				}
			}
			else
			{
				EHCount = ILGenerator.CalculateNumberOfExceptions(this.m_exceptions);
			}
			initLocals = (this.m_method.InitLocals ? 1 : 0);
			return this.m_code;
		}

		// Token: 0x060049A3 RID: 18851 RVA: 0x0010067E File Offset: 0x000FF67E
		internal override byte[] GetLocalsSignature()
		{
			return this.m_localSignature;
		}

		// Token: 0x060049A4 RID: 18852 RVA: 0x00100686 File Offset: 0x000FF686
		internal override byte[] GetRawEHInfo()
		{
			return this.m_exceptionHeader;
		}

		// Token: 0x060049A5 RID: 18853 RVA: 0x00100690 File Offset: 0x000FF690
		internal unsafe override void GetEHInfo(int excNumber, void* exc)
		{
			for (int i = 0; i < this.m_exceptions.Length; i++)
			{
				int numberOfCatches = this.m_exceptions[i].GetNumberOfCatches();
				if (excNumber < numberOfCatches)
				{
					((Resolver.CORINFO_EH_CLAUSE*)exc)->Flags = this.m_exceptions[i].GetExceptionTypes()[excNumber];
					((Resolver.CORINFO_EH_CLAUSE*)exc)->Flags = (((Resolver.CORINFO_EH_CLAUSE*)exc)->Flags | 536870912);
					((Resolver.CORINFO_EH_CLAUSE*)exc)->TryOffset = this.m_exceptions[i].GetStartAddress();
					if ((((Resolver.CORINFO_EH_CLAUSE*)exc)->Flags & 2) != 2)
					{
						((Resolver.CORINFO_EH_CLAUSE*)exc)->TryLength = this.m_exceptions[i].GetEndAddress() - ((Resolver.CORINFO_EH_CLAUSE*)exc)->TryOffset;
					}
					else
					{
						((Resolver.CORINFO_EH_CLAUSE*)exc)->TryLength = this.m_exceptions[i].GetFinallyEndAddress() - ((Resolver.CORINFO_EH_CLAUSE*)exc)->TryOffset;
					}
					((Resolver.CORINFO_EH_CLAUSE*)exc)->HandlerOffset = this.m_exceptions[i].GetCatchAddresses()[excNumber];
					((Resolver.CORINFO_EH_CLAUSE*)exc)->HandlerLength = this.m_exceptions[i].GetCatchEndAddresses()[excNumber] - ((Resolver.CORINFO_EH_CLAUSE*)exc)->HandlerOffset;
					((Resolver.CORINFO_EH_CLAUSE*)exc)->ClassTokenOrFilterOffset = this.m_exceptions[i].GetFilterAddresses()[excNumber];
					return;
				}
				excNumber -= numberOfCatches;
			}
		}

		// Token: 0x060049A6 RID: 18854 RVA: 0x00100794 File Offset: 0x000FF794
		internal override string GetStringLiteral(int token)
		{
			return this.m_scope.GetString(token);
		}

		// Token: 0x060049A7 RID: 18855 RVA: 0x001007A4 File Offset: 0x000FF7A4
		private int GetMethodToken()
		{
			if (this.IsValidToken(this.m_methodToken) == 0)
			{
				int tokenFor = this.m_scope.GetTokenFor(this.m_method.GetMethodDescriptor());
				Interlocked.CompareExchange(ref this.m_methodToken, tokenFor, 0);
			}
			return this.m_methodToken;
		}

		// Token: 0x060049A8 RID: 18856 RVA: 0x001007EA File Offset: 0x000FF7EA
		internal override int IsValidToken(int token)
		{
			if (this.m_scope[token] == null)
			{
				return 0;
			}
			return 1;
		}

		// Token: 0x060049A9 RID: 18857 RVA: 0x001007FD File Offset: 0x000FF7FD
		internal override CompressedStack GetSecurityContext()
		{
			return this.m_method.m_creationContext;
		}

		// Token: 0x060049AA RID: 18858 RVA: 0x0010080C File Offset: 0x000FF80C
		internal unsafe override void* ResolveToken(int token)
		{
			object obj = this.m_scope[token];
			if (obj is RuntimeTypeHandle)
			{
				return (void*)((RuntimeTypeHandle)obj).Value;
			}
			if (obj is RuntimeMethodHandle)
			{
				return (void*)((RuntimeMethodHandle)obj).Value;
			}
			if (obj is RuntimeFieldHandle)
			{
				return (void*)((RuntimeFieldHandle)obj).Value;
			}
			if (obj is DynamicMethod)
			{
				DynamicMethod dynamicMethod = (DynamicMethod)obj;
				return (void*)dynamicMethod.GetMethodDescriptor().Value;
			}
			if (obj is GenericMethodInfo)
			{
				GenericMethodInfo genericMethodInfo = (GenericMethodInfo)obj;
				return (void*)genericMethodInfo.m_method.Value;
			}
			if (obj is GenericFieldInfo)
			{
				GenericFieldInfo genericFieldInfo = (GenericFieldInfo)obj;
				return (void*)genericFieldInfo.m_field.Value;
			}
			if (!(obj is VarArgMethod))
			{
				return null;
			}
			VarArgMethod varArgMethod = (VarArgMethod)obj;
			DynamicMethod dynamicMethod2 = varArgMethod.m_method as DynamicMethod;
			if (dynamicMethod2 == null)
			{
				return (void*)varArgMethod.m_method.MethodHandle.Value;
			}
			return (void*)dynamicMethod2.GetMethodDescriptor().Value;
		}

		// Token: 0x060049AB RID: 18859 RVA: 0x00100939 File Offset: 0x000FF939
		internal override byte[] ResolveSignature(int token, int fromMethod)
		{
			return this.m_scope.ResolveSignature(token, fromMethod);
		}

		// Token: 0x060049AC RID: 18860 RVA: 0x00100948 File Offset: 0x000FF948
		internal override int ParentToken(int token)
		{
			RuntimeTypeHandle type = RuntimeTypeHandle.EmptyHandle;
			object obj = this.m_scope[token];
			if (obj is RuntimeMethodHandle)
			{
				type = ((RuntimeMethodHandle)obj).GetDeclaringType();
			}
			else if (obj is RuntimeFieldHandle)
			{
				type = ((RuntimeFieldHandle)obj).GetApproxDeclaringType();
			}
			else if (obj is DynamicMethod)
			{
				DynamicMethod dynamicMethod = (DynamicMethod)obj;
				type = dynamicMethod.m_method.GetDeclaringType();
			}
			else if (obj is GenericMethodInfo)
			{
				GenericMethodInfo genericMethodInfo = (GenericMethodInfo)obj;
				type = genericMethodInfo.m_context;
			}
			else if (obj is GenericFieldInfo)
			{
				GenericFieldInfo genericFieldInfo = (GenericFieldInfo)obj;
				type = genericFieldInfo.m_context;
			}
			else if (obj is VarArgMethod)
			{
				VarArgMethod varArgMethod = (VarArgMethod)obj;
				DynamicMethod dynamicMethod2 = varArgMethod.m_method as DynamicMethod;
				if (dynamicMethod2 != null)
				{
					type = dynamicMethod2.GetMethodDescriptor().GetDeclaringType();
				}
				else if (varArgMethod.m_method.DeclaringType == null)
				{
					type = varArgMethod.m_method.MethodHandle.GetDeclaringType();
				}
				else
				{
					type = varArgMethod.m_method.DeclaringType.TypeHandle;
				}
			}
			if (type.IsNullHandle())
			{
				return -1;
			}
			return this.m_scope.GetTokenFor(type);
		}

		// Token: 0x060049AD RID: 18861 RVA: 0x00100A7D File Offset: 0x000FFA7D
		internal override MethodInfo GetDynamicMethod()
		{
			return this.m_method.GetMethodInfo();
		}

		// Token: 0x040025AF RID: 9647
		private __ExceptionInfo[] m_exceptions;

		// Token: 0x040025B0 RID: 9648
		private byte[] m_exceptionHeader;

		// Token: 0x040025B1 RID: 9649
		private DynamicMethod m_method;

		// Token: 0x040025B2 RID: 9650
		private byte[] m_code;

		// Token: 0x040025B3 RID: 9651
		private byte[] m_localSignature;

		// Token: 0x040025B4 RID: 9652
		private int m_stackSize;

		// Token: 0x040025B5 RID: 9653
		private DynamicScope m_scope;

		// Token: 0x040025B6 RID: 9654
		private int m_methodToken;

		// Token: 0x02000819 RID: 2073
		private class DestroyScout
		{
			// Token: 0x060049AE RID: 18862 RVA: 0x00100A8C File Offset: 0x000FFA8C
			~DestroyScout()
			{
				if (!this.m_method.IsNullHandle())
				{
					if (this.m_method.GetResolver() != null)
					{
						if (!Environment.HasShutdownStarted && !AppDomain.CurrentDomain.IsFinalizingForUnload())
						{
							GC.ReRegisterForFinalize(this);
						}
					}
					else
					{
						this.m_method.Destroy();
					}
				}
			}

			// Token: 0x040025B7 RID: 9655
			internal RuntimeMethodHandle m_method;
		}

		// Token: 0x0200081A RID: 2074
		[Flags]
		internal enum SecurityControlFlags
		{
			// Token: 0x040025B9 RID: 9657
			Default = 0,
			// Token: 0x040025BA RID: 9658
			SkipVisibilityChecks = 1,
			// Token: 0x040025BB RID: 9659
			RestrictedSkipVisibilityChecks = 2,
			// Token: 0x040025BC RID: 9660
			HasCreationContext = 4,
			// Token: 0x040025BD RID: 9661
			CanSkipCSEvaluation = 8
		}
	}
}
