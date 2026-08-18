using System;
using System.Reflection;

namespace System.Runtime.InteropServices
{
	// Token: 0x020004F5 RID: 1269
	[AttributeUsage(AttributeTargets.Field | AttributeTargets.Parameter | AttributeTargets.ReturnValue, Inherited = false)]
	[ComVisible(true)]
	public sealed class MarshalAsAttribute : Attribute
	{
		// Token: 0x0600316A RID: 12650 RVA: 0x000A92CD File Offset: 0x000A82CD
		internal static Attribute GetCustomAttribute(ParameterInfo parameter)
		{
			return MarshalAsAttribute.GetCustomAttribute(parameter.MetadataToken, parameter.Member.Module);
		}

		// Token: 0x0600316B RID: 12651 RVA: 0x000A92E5 File Offset: 0x000A82E5
		internal static bool IsDefined(ParameterInfo parameter)
		{
			return MarshalAsAttribute.GetCustomAttribute(parameter) != null;
		}

		// Token: 0x0600316C RID: 12652 RVA: 0x000A92F3 File Offset: 0x000A82F3
		internal static Attribute GetCustomAttribute(RuntimeFieldInfo field)
		{
			return MarshalAsAttribute.GetCustomAttribute(field.MetadataToken, field.Module);
		}

		// Token: 0x0600316D RID: 12653 RVA: 0x000A9306 File Offset: 0x000A8306
		internal static bool IsDefined(RuntimeFieldInfo field)
		{
			return MarshalAsAttribute.GetCustomAttribute(field) != null;
		}

		// Token: 0x0600316E RID: 12654 RVA: 0x000A9314 File Offset: 0x000A8314
		internal static Attribute GetCustomAttribute(int token, Module scope)
		{
			int num = 0;
			int sizeConst = 0;
			string text = null;
			string marshalCookie = null;
			string text2 = null;
			int iidParamIndex = 0;
			ConstArray fieldMarshal = scope.ModuleHandle.GetMetadataImport().GetFieldMarshal(token);
			if (fieldMarshal.Length == 0)
			{
				return null;
			}
			UnmanagedType val;
			VarEnum safeArraySubType;
			UnmanagedType arraySubType;
			MetadataImport.GetMarshalAs(fieldMarshal, out val, out safeArraySubType, out text2, out arraySubType, out num, out sizeConst, out text, out marshalCookie, out iidParamIndex);
			Type safeArrayUserDefinedSubType = (text2 == null || text2.Length == 0) ? null : RuntimeTypeHandle.GetTypeByNameUsingCARules(text2, scope);
			Type marshalTypeRef = null;
			try
			{
				marshalTypeRef = ((text == null) ? null : RuntimeTypeHandle.GetTypeByNameUsingCARules(text, scope));
			}
			catch (TypeLoadException)
			{
			}
			return new MarshalAsAttribute(val, safeArraySubType, safeArrayUserDefinedSubType, arraySubType, (short)num, sizeConst, text, marshalTypeRef, marshalCookie, iidParamIndex);
		}

		// Token: 0x0600316F RID: 12655 RVA: 0x000A93CC File Offset: 0x000A83CC
		internal MarshalAsAttribute(UnmanagedType val, VarEnum safeArraySubType, Type safeArrayUserDefinedSubType, UnmanagedType arraySubType, short sizeParamIndex, int sizeConst, string marshalType, Type marshalTypeRef, string marshalCookie, int iidParamIndex)
		{
			this._val = val;
			this.SafeArraySubType = safeArraySubType;
			this.SafeArrayUserDefinedSubType = safeArrayUserDefinedSubType;
			this.IidParameterIndex = iidParamIndex;
			this.ArraySubType = arraySubType;
			this.SizeParamIndex = sizeParamIndex;
			this.SizeConst = sizeConst;
			this.MarshalType = marshalType;
			this.MarshalTypeRef = marshalTypeRef;
			this.MarshalCookie = marshalCookie;
		}

		// Token: 0x06003170 RID: 12656 RVA: 0x000A942C File Offset: 0x000A842C
		public MarshalAsAttribute(UnmanagedType unmanagedType)
		{
			this._val = unmanagedType;
		}

		// Token: 0x06003171 RID: 12657 RVA: 0x000A943B File Offset: 0x000A843B
		public MarshalAsAttribute(short unmanagedType)
		{
			this._val = (UnmanagedType)unmanagedType;
		}

		// Token: 0x170008C2 RID: 2242
		// (get) Token: 0x06003172 RID: 12658 RVA: 0x000A944A File Offset: 0x000A844A
		public UnmanagedType Value
		{
			get
			{
				return this._val;
			}
		}

		// Token: 0x04001987 RID: 6535
		internal UnmanagedType _val;

		// Token: 0x04001988 RID: 6536
		public VarEnum SafeArraySubType;

		// Token: 0x04001989 RID: 6537
		public Type SafeArrayUserDefinedSubType;

		// Token: 0x0400198A RID: 6538
		public int IidParameterIndex;

		// Token: 0x0400198B RID: 6539
		public UnmanagedType ArraySubType;

		// Token: 0x0400198C RID: 6540
		public short SizeParamIndex;

		// Token: 0x0400198D RID: 6541
		public int SizeConst;

		// Token: 0x0400198E RID: 6542
		[ComVisible(true)]
		public string MarshalType;

		// Token: 0x0400198F RID: 6543
		[ComVisible(true)]
		public Type MarshalTypeRef;

		// Token: 0x04001990 RID: 6544
		public string MarshalCookie;
	}
}
