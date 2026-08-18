using System;
using System.Collections;
using System.Reflection;
using System.Security.Permissions;

namespace System.ComponentModel.Design.Serialization
{
	// Token: 0x0200060F RID: 1551
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
	public sealed class InstanceDescriptor
	{
		// Token: 0x060038D5 RID: 14549 RVA: 0x000F1FBF File Offset: 0x000F01BF
		public InstanceDescriptor(MemberInfo member, ICollection arguments) : this(member, arguments, true)
		{
		}

		// Token: 0x060038D6 RID: 14550 RVA: 0x000F1FCC File Offset: 0x000F01CC
		public InstanceDescriptor(MemberInfo member, ICollection arguments, bool isComplete)
		{
			this.member = member;
			this.isComplete = isComplete;
			if (arguments == null)
			{
				this.arguments = new object[0];
			}
			else
			{
				object[] array = new object[arguments.Count];
				arguments.CopyTo(array, 0);
				this.arguments = array;
			}
			if (member is FieldInfo)
			{
				FieldInfo fieldInfo = (FieldInfo)member;
				if (!fieldInfo.IsStatic)
				{
					throw new ArgumentException(SR.GetString("InstanceDescriptorMustBeStatic"));
				}
				if (this.arguments.Count != 0)
				{
					throw new ArgumentException(SR.GetString("InstanceDescriptorLengthMismatch"));
				}
			}
			else if (member is ConstructorInfo)
			{
				ConstructorInfo constructorInfo = (ConstructorInfo)member;
				if (constructorInfo.IsStatic)
				{
					throw new ArgumentException(SR.GetString("InstanceDescriptorCannotBeStatic"));
				}
				if (this.arguments.Count != constructorInfo.GetParameters().Length)
				{
					throw new ArgumentException(SR.GetString("InstanceDescriptorLengthMismatch"));
				}
			}
			else if (member is MethodInfo)
			{
				MethodInfo methodInfo = (MethodInfo)member;
				if (!methodInfo.IsStatic)
				{
					throw new ArgumentException(SR.GetString("InstanceDescriptorMustBeStatic"));
				}
				if (this.arguments.Count != methodInfo.GetParameters().Length)
				{
					throw new ArgumentException(SR.GetString("InstanceDescriptorLengthMismatch"));
				}
			}
			else if (member is PropertyInfo)
			{
				PropertyInfo propertyInfo = (PropertyInfo)member;
				if (!propertyInfo.CanRead)
				{
					throw new ArgumentException(SR.GetString("InstanceDescriptorMustBeReadable"));
				}
				MethodInfo getMethod = propertyInfo.GetGetMethod();
				if (getMethod != null && !getMethod.IsStatic)
				{
					throw new ArgumentException(SR.GetString("InstanceDescriptorMustBeStatic"));
				}
			}
		}

		// Token: 0x17000D97 RID: 3479
		// (get) Token: 0x060038D7 RID: 14551 RVA: 0x000F2150 File Offset: 0x000F0350
		public ICollection Arguments
		{
			get
			{
				return this.arguments;
			}
		}

		// Token: 0x17000D98 RID: 3480
		// (get) Token: 0x060038D8 RID: 14552 RVA: 0x000F2158 File Offset: 0x000F0358
		public bool IsComplete
		{
			get
			{
				return this.isComplete;
			}
		}

		// Token: 0x17000D99 RID: 3481
		// (get) Token: 0x060038D9 RID: 14553 RVA: 0x000F2160 File Offset: 0x000F0360
		public MemberInfo MemberInfo
		{
			get
			{
				return this.member;
			}
		}

		// Token: 0x060038DA RID: 14554 RVA: 0x000F2168 File Offset: 0x000F0368
		public object Invoke()
		{
			object[] array = new object[this.arguments.Count];
			this.arguments.CopyTo(array, 0);
			for (int i = 0; i < array.Length; i++)
			{
				if (array[i] is InstanceDescriptor)
				{
					array[i] = ((InstanceDescriptor)array[i]).Invoke();
				}
			}
			if (this.member is ConstructorInfo)
			{
				return ((ConstructorInfo)this.member).Invoke(array);
			}
			if (this.member is MethodInfo)
			{
				return ((MethodInfo)this.member).Invoke(null, array);
			}
			if (this.member is PropertyInfo)
			{
				return ((PropertyInfo)this.member).GetValue(null, array);
			}
			if (this.member is FieldInfo)
			{
				return ((FieldInfo)this.member).GetValue(null);
			}
			return null;
		}

		// Token: 0x04002B7C RID: 11132
		private MemberInfo member;

		// Token: 0x04002B7D RID: 11133
		private ICollection arguments;

		// Token: 0x04002B7E RID: 11134
		private bool isComplete;
	}
}
